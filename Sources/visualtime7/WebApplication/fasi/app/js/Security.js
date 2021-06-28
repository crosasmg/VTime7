
var securitySupport = new function () {
   
    /**Variable para poder utilizar el servido de google recaptcha */
    var keyRecaptcha = "6LfTaj8UAAAAACq2GGmDoAYlEnitqry9_SCHA4gB";

    //Valida los roles de los usuario de estar asignado deja entrar a la planilla
    this.ValidateAccessRoles = function (roles) {
        var urlSource = encodeURI(window.location.pathname);
        var urlUnaunthorizedUser = '/fasi/dli/forms/UnauthorizedUser.aspx?urlsource=' + urlSource;
        var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
        var user = generalSupport.UserContext();
        if (user.isAnonymous && InMotionGITToken !== null) {
            var resultValidateRole = securitySupport.ValidateRoleByToken(InMotionGITToken, roles);
            if (resultValidateRole) {
                var result = securitySupport.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
                if (!result) {
                    window.location = urlUnaunthorizedUser;
                }
            } else {
                window.location = urlUnaunthorizedUser;
            }
        } else if (!user.isAnonymous) {
            $.ajax({
                type: "POST",
                async: false,
                url: constants.fasiApi.base + 'Authentication/v1/ValidateAccessRoles',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(roles),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                },
                success: function (data) {
                    if (!data.Successfully) {
                        window.location = urlUnaunthorizedUser;
                    }
                },
                error: function (qXHR, textStatus, errorThrown) {
                    window.location = urlUnaunthorizedUser;
                }
            });
        } else if (user.isAnonymous) {
            if (!securitySupport.IsRoleExpecial(roles)) {
                window.location = '/fasi/security/logIn.aspx?urlsource=' + encodeURI(window.location.href);
                //window.location = urlUnaunthorizedUser;
            }
        } else {
            window.location = urlUnaunthorizedUser;
        }
    };

    this.IsRoleExpecial = function (roles) {
        var result = false;
        if (!constants.SpecialRoles) {
            $.ajax({
                type: "GET",
                async: false,
                url: '/fasi/wmethods/User.aspx/SpecialRoles',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    constants.SpecialRoles = data.d;
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        roles.forEach(function (elementSource) {
            if (elementSource == constants.SpecialRoles.AnonymousRole) {
                result = true;
            }
        });
        return result;
    };

    //Define si esta o no conectado el usuario en el aplicación
    this.IsConnected = function () {
        if (generalSupport.UserContext().isAnonymous)
            window.location = '/fasi/security/logIn.aspx?urlsource=' + encodeURI(window.location.href);
        //window.location = constants.defaultPage;
    };

    this.AutoLogIn = function (token, languageId) {
        var result = false;
        $.ajax({
            type: "POST",
            async: false,
            url: '/fasi/wmethods/User.aspx/AutoLogin',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Token: token, LanguageId: languageId }),
            dataType: "json",
            success: function (data) {
                result = data.d;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    this.ValidateRoleByToken = function (token, roles) {
        var result = false;
        var relesValue = roles.join(",");
        $.ajax({
            type: "POST",
            async: false,
            url: constants.fasiApi.base + 'Authentication/v1/ValidateRoleByToken?Token=' + token + "&Roles=" + relesValue,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ Roles: roles }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.UserContext().token);
            },
            success: function (data) {
                result = data.Successfully;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    this.UserCheckEquals = function (token, userId) {
        var result = false;
        $.ajax({
            type: "POST",
            async: false,
            url: '/fasi/wmethods/User.aspx/UserCheckEquals',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Token: token, UserId: userId }),
            dataType: "json",
            success: function (data) {
                result = data.d;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    this.PasswordRecovery = function () {
        var userName = $('#UserName').val();
        if (userName !== null && userName !== '') {
            $.ajax({
                type: "GET",
                url: constants.fasiApi.members + '/PasswordRecoveryByEmail?email=' + userName,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({}),
                headers: {
                    'Accept-Language': generalSupport.LanguageName()
                },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                },
                success: function (data) {
                    if (data.Successfully === true) {
                        notification.control.success(null, $.i18n.t('app.form.RecoverPasswordSuccessfully'));
                    }
                    else {
                        if (data.Reason != '') {
                            notification.control.error(null, data.Reason);
                        } else {
                            notification.control.error(null, $.i18n.t('app.form.RecoverPasswordIncorrect'));
                        }
                    }
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        } else {
            notification.control.error(null, $.i18n.t('app.form.RequiredEmail'));
        }
    };

    // Desconecta el usuario
    this.Logout = function (userId, IsRedirect, code) {
        //Cleaning the master menu to load the new users one
        localStorage.removeItem("masterMenu");
        ajaxJsonHelper.post(constants.fasiApi.members + 'UserLogOff', null,
            function (data) {
                if (data && data.Successfully) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: '/fasi/wmethods/User.aspx/LogOut',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            UserClean();
                            if (IsRedirect) {
                                if (data.d.Url !== "") {
                                    window.location.href = data.d.Url;
                                } else {
                                    if (!code) {
                                        window.location.replace(constants.defaultPage);
                                    }
                                    else {
                                        window.location.href = constants.defaultPage + '?GSCode=' + code;
                                    }
                                }
                            }
                        }
                    });
                }
            }, null, null, false);
    };

    this.SessionLive = function () {
        $.ajax({
            type: "GET",
            async: false,
            url: '/fasi/wmethods/User.aspx/SessionLive',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
            }
        });
    };

    this.Messages = function (code) {
        var title = "", message = "";
        switch (code) {
            case "GS001":
                title = generalSupport.ResourceByKey("CodeGS001Title");
                message = generalSupport.ResourceByKey("CodeGS001Body");
                break;

            case "901":
                title = generalSupport.ResourceByKey("Code901Title");
                message = generalSupport.ResourceByKey("Code901Body");

                break;
            default:
                title = generalSupport.ResourceByKey("UndefinedTitle");
                message = generalSupport.ResourceByKey("UndefinedMessage");
        }
        notification.swal.info(title, message);
    };

    /**
     * Method de validación por recaptcha de Google
     * @param {any} callbackFuntion Funciona realizar posterior al llamado correcto
     */
    this.CreateCaptcha = function (callbackFuntion) {
        captchaContainer = grecaptcha.render('captcha_container', {
            'sitekey': keyRecaptcha,
            'callback': callbackFuntion,
            "hl": generalSupport.LanguageName()
        });
    };
};

app.security = (function () {
    if (!constants.fields) {
        constants.fields = {};
    }

    if (!constants.fields.security) {
        constants.fields.security = {};
    }

    if (!constants.fields.security.userProfile) {
        constants.fields.security.userProfile = 'userProfile';
    }

    if (!constants.fields.security.masterMenu) {
        constants.fields.security.masterMenu = 'masterMenu';
    }

    if (!constants.fields.security.FASITokenDate) {
        constants.fields.security.FASITokenDate = 'FASITokenDate';
    }

    if (!constants.fields.security.FASIToken) {
        constants.fields.security.FASIToken = 'FASIToken';
    }

    if (!constants.fields.security.unauthorized) {
        constants.fields.security.unauthorized = ' <div class="middle-box-error text-center animated fadeInDown">' +
            '   <h2 id="ResourceNotFound" class="font-bold trn" >{{ResourceUnauthorized}}</h3>' +
            '   <div id="ResourceNotFoundDetail" class="error-desc trn" >{{ResourceUnauthorizedDetail}}</div>' +
            ' </div>';
    }

    UnAuthorized = function () {
        var ResourceUnauthorized = dict.ResourceUnauthorized[app.user.languageName];
        var ResourceUnauthorizedDetail = dict.ResourceUnauthorizedDetail[app.user.languageName];
        var result = constants.fields.security.unauthorized.replace('{{ResourceUnauthorized}}', ResourceUnauthorized).replace('{{ResourceUnauthorizedDetail}}', ResourceUnauthorizedDetail);
        return result;
    };

    Fields = function () {
        return constants.fields;
    };

    UserConvert = function (data) {
        return {
            userName: data.d.user.username,
            userId: data.d.user.userId,
            companyId: data.d.user.companyId,
            isAnonymous: data.d.user.isAnonymous,
            isAdministrator: data.d.user.isAdministrator,
            schemeCode: data.d.user.schemeCode,
            token: data.d.user.token,
            clientId: data.d.user.clientId,
            producerId: data.d.user.producerId,
            firstNameAndSecondLastName: data.d.user.firstNameAndSecondLastName,
            languageID: data.d.user.languageID,
            languageName: data.d.user.languageName,
            type: data.d.user.type,
            utcOffset: data.d.user.utcOffset,
            isEmployee: data.d.user.IsEmployee,
            sessionId: data.d.user.sessionId,
            expiration: data.d.user.expiration,
            timeZoneVT: data.d.user.TimeZoneVT
        };
    };

    WorkerManager = function () {
        if (app.workers) {
            var WorkerRemove = [];
            app.workers.forEach(function (item) {
                WorkerRemove.push(item);
                item();
            });

            WorkerRemove.forEach(function (item) {
                for (var index = app.workers.length - 1; index >= 0; --index) {
                    if (app.workers[index] === item) {
                        app.workers.splice(index, 1);
                    }
                }
            });
            WorkerRemove = [];
        }
    };

    GetUser = function (options, callback) {
        var callBackUser = function (callback) {
            $.ajax({
                url: url,
                type: 'GET',
                contentType: "application/json; charset=utf-8",
            })
                .done(function (data) {
                    app.user = UserConvert(data);
                    TokenInit(app.user.token);
                    callback(data);
                    WorkerManager();
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    serrorFunction();
                });
        };

        if (options == undefined) {
            options = { reset: false };
        }

        if (options.reset === undefined) {
            reset = false;
        }

        if (options.reset === true) {
            localStorage.removeItem(constants.fields.security.userProfile);
        }

        // Obtiene el código del usuario
        var url = '/fasi/wmethods/User.aspx/GetUserInformation';
        languageName = generalSupport.GetParameterByName('culture');

        if (languageName) {
            url = url + '?culture=' + languageName;
        }

        if (!localStorage.getItem(constants.fields.security.userProfile)) {
            callBackUser(callback);
        }
        else {
            app.user = JSON.parse(localStorage.getItem(constants.fields.security.userProfile));
            if (!app.security.SessionIdCheck(app.user.sessionId)) {
                callBackUser(callback);
            }
            else {
                if (!app.security.TokenIsValid()) {
                    TokenInit(app.security.Token(true));
                    callback(app.user);
                } else {
                    callback(app.user);
                }
            }
        }

        /**Se debe remover**/
        if (typeof masterSupport !== 'undefined') {
            masterSupport.user = app.user;
        }

        return app.user;
    };

    UserClean = function () {
        generalSupport.LocalStorageRemoveStartWith(constants.fields.security.masterMenu);
        localStorage.removeItem(constants.fields.security.userProfile);
        generalSupport.LocalStorageRemoveStartWith('Page_');
    };

    MasterMenuKey = function () {
        return constants.fields.security.masterMenu + "_" + generalSupport.LanguageId() + "_" + app.user.userId;
    };

    UserContext = function (options, callback) {
        if (app.user === undefined) {
            if (typeof masterSupport !== 'undefined')
                if (masterSupport !== undefined && masterSupport.user !== undefined) {
                    /** remover ***/
                    app.user = masterSupport.user;
                    generalSupport.user = masterSupport.user;
                }
                else
                    GetUser(options, callback);
            else
                GetUser(options, callback);
        }
        return app.user;
    };

    /**
     * Método para verificar el Id de la session guardado en cache y el que esta en session
     * @param {any} sessionId SessionId actual.
     * @returns {any} Si es igual a actual
     */
    SessionIdCheck = function (sessionId) {
        var result = false;
        var url = '/fasi/wmethods/User.aspx/SessionIdCheck';
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            async: false,
            data: JSON.stringify({
                sessionId: sessionId
            }),
            dataType: "json",
            success: function (data) {
                result = data.d.State;
            },
            error: function (qXHR, textStatus, errorThrown) {
                result = "Ha ocurrido una falla, por favor intente nuevamente";
            }
        });
        return result;
    };

    SessionCheck = function () {
        if (!app.user.isAnonymous) {
            if (!localStorage.getItem("endTime") || localStorage.getItem("endTime") == "undefined") {
                SessionReset(localStorage.getItem("Interval"));
            }

            localStorage.Timer = setInterval(function () {
                var remaining = Date.parse(localStorage.getItem("endTime")) - new Date();
                if (localStorage.getItem("IsShow") === 'false' && Math.floor(remaining / 1000) <= 30) {
                    var SessionId = "";
                    if (app.user !== null && app.user.sessionId !== null) {
                        SessionId = app.user.sessionId;
                    }
                    generalSupport.Operation("CheckSessionSyncro", { SessionId: SessionId }, function (data) {
                        if (data.d.Valid == false) {
                            swal({
                                title: generalSupport.ResourceByKey("ExpirationSectionTitle"),
                                html: true,
                                text: '<p>' + generalSupport.ResourceByKey("ExpirationSectionBody") + ' <b id="pCounter" name="pCounter"></b> ' + generalSupport.ResourceByKey("Seconds") + '.</p>',
                                type: "warning",
                                buttons: true,
                                dangerMode: true,
                                showCancelButton: true,
                                cancelButtonText: generalSupport.ResourceByKey("ExpirationSectionBtnCancel"),
                                confirmButtonColor: "#18a689",
                                confirmButtonText: generalSupport.ResourceByKey("ExpirationSectionBtnSessionKeep"),
                                closeOnConfirm: true
                            }, function (isConfirm) {
                                if (isConfirm) {
                                    swal.close();
                                    location.reload();
                                } else {
                                    app.security.Logout(app.user.userId, true);
                                    clearInterval(localStorage.Timer);
                                }
                            });

                            localStorage.IsShow = true;
                        } else {
                            app.security.SessionLive();
                            SessionReset(localStorage.getItem("Interval"));
                        }
                    });
                } else if (remaining >= 0) {
                    if ($('#pCounter').length) {
                        $('#pCounter').html(Math.floor(remaining / 1000));
                    }
                } else {
                    app.security.Logout(app.user.userId, true, 'GS001');
                    SessionReset(localStorage.Interval);
                    swal.close();
                    clearInterval(localStorage.Timer);
                }
            }, 1000);
        }
    };

    SessionReset = function (interval) {
        var dt = new Date();
        dt.setSeconds(dt.getSeconds() + parseInt(localStorage.getItem("Interval")) / 1000);
        localStorage.endTime = dt;
        localStorage.IsShow = false;
    };

    Reset = function(interval) {
        localStorage.endTime = + new Date + interval;
        localStorage.IsShow = false;
    };

    SessionSetUp = function (timeout) {
        if (localStorage.getItem("Interval") == "undefined" || localStorage.getItem("Interval") == null) {
            localStorage.setItem("Interval", 60 * 1000 * parseInt(timeout));
            localStorage.setItem("Interval", localStorage.getItem("Interval") - (60 * 400));
        }
    };

    IsALive = function (options, callBack) {
        var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
        var isFirst = false;
        var roles = [];
        var tokenRenew = false;
        var sessionId = "";
        var isConnected;
        if (options === undefined) {
            options = { reset: false };
        }

        if (options.reset === undefined) {
            reset = false;
        }

        if (options.roles === undefined) {
            roles = [];
        } else {
            roles = options.roles;
        }

        if (options.IsConnected === undefined) {
            isConnected = false;
        } else {
            isConnected = options.IsConnected;
        }

        if (options.reset === true) {
            localStorage.removeItem(constants.fields.security.userProfile);
        }

        if (!localStorage.getItem(constants.fields.security.userProfile)) {
            isFirst = true;
        } else {
            app.user = JSON.parse(localStorage.getItem(constants.fields.security.userProfile));
            sessionId = app.user.sessionId;
            tokenRenew = !TokenIsValid();
        }

        $.ajax({
            url: '/fasi/wmethods/User.aspx/IsALive',
            type: 'POST',
            data: JSON.stringify({
                IsFirst: isFirst,
                SessionId: sessionId,
                TokenRenew: tokenRenew,
                Roles: roles,
                InMotionGITToken: InMotionGITToken,
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            constantsSupport.setup(data.d);
            var FirstPasswordChange = generalSupport.findKey(data.d.settings, "FirstPasswordChange");
            if (data.d.user) {
                app.user = UserConvert(data);
                TokenInit(app.user.token);
            }
            else if (data.d.token) {
                TokenInit(data.d.token);
            }

            var timeout = generalSupport.findKey(data.d.settings, "timeout");
            var languageId = generalSupport.findKey(data.d.settings, "LanguageId");
            var languageName = generalSupport.findKey(data.d.settings, "LanguageName"); 

            generalSupport.settingsSet(data.d.settings); 

            SessionSetUp(timeout);

            generalSupport.LanguageSet(languageId, languageName);

            if (!app.user.isAnonymous) {
                SessionReset(timeout);
            }

            if (typeof masterSupport !== 'undefined') {
                masterSupport.user = app.user;
                generalSupport.user = app.user;
                if (!isFirst && sessionId !== app.user.sessionId) {
                    UserClean();
                }
                masterSupport.Enviroment();
            }

            if (!data.d.user) {
                data.d.user = app.user;
            }

            function LocalInit() {
                if (callBack !== undefined) {
                    if (isConnected) {
                        app.security.IsConnected();
                        callBack(data);
                        app.security.SessionCheck();
                    }
                    else {
                        if (data.d.authorization.Successfully) {
                            callBack(data);
                        } else {
                            ValidateAccess(data.d);
                        }
                        app.security.SessionCheck();
                    }
                } else {
                    if (isConnected) {
                        app.security.IsConnected();
                    } else {
                        if (data.d.authorization) {
                            ValidateAccess(data.d);
                        }
                    }
                    app.security.SessionCheck();
                }

                WorkerManager();
                generalSupport.CreateVTTimeZone();
            }

            if (FirstPasswordChange == false) {
                LocalInit();
            }
            else {
                var FirstPasswordChangeUrl = generalSupport.findKey(data.d.settings, "FirstPasswordChangeUrl");
                if (window.location.pathname.toLowerCase().indexOf(FirstPasswordChangeUrl.toLowerCase()) != 0) {
                    window.location = FirstPasswordChangeUrl;
                } else {
                    LocalInit();
                }
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                EndSession();
            } else {
                app.core.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
    };

    ValidateAccess = function (data) {
        var urlSource = encodeURI(window.location.pathname);
        var urlUnaunthorizedUser = '/fasi/dli/forms/UnauthorizedUser.aspx?urlsource=' + urlSource;
        var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
        if (data.user.isAnonymous && InMotionGITToken !== null) {
            var resultValidateRole = securitySupport.ValidateRoleByToken(InMotionGITToken, roles);
            if (resultValidateRole) {
                var result = securitySupport.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
                if (!result) {
                    window.location = urlUnaunthorizedUser;
                }
            } else {
                window.location = urlUnaunthorizedUser;
            }
        } else if (!data.user.isAnonymous) {
            if (!data.authorization.Successfully) {
                window.location = urlUnaunthorizedUser;
            }
        } else if (data.user.isAnonymous) {
            var code = generalSupport.GetParameterByName('GSCode');
            if (!data.authorization.Successfully && code == null) {
                window.location = '/fasi/default.aspx?GSCode=' + data.authorization.Code;
            }
        } else {
            window.location = urlUnaunthorizedUser;
        }
    };

    Authorization = function (options, callBack) {
        if (options.IsConnected !== undefined) {
            if (app.user.isAnonymous) {
                options.Element.html(UnAuthorized());
            } else {
                callBack();
            }
        } else {
            if (options.roles === undefined) {
                options.roles = [];
            }
            $.ajax({
                url: '/fasi/wmethods/User.aspx/Authorization',
                type: 'POST',
                data: JSON.stringify({
                    Roles: options.roles
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                }
            }).done(function (data) {
                if (!data.d.Successfully) {
                    options.Element.html(UnAuthorized());
                }
                else {
                    callBack();
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                serrorFunction();
            });
        }
    };

    PageSetup = function (options) {
        if (options.Pathname.toLowerCase().endsWith('default.aspx')) {
            app.security.Authorization(options, options.CallBack);
        }
        else
            if (options.Pathname.toLowerCase().endsWith('popup.html'))
                app.security.IsALive(options, options.CallBack);
            else {
                if (!options.Custom && options.Custom == false) {
                    app.workers.push(options.CallBack);
                    masterSupport.Init(options);
                } else {
                    app.security.IsALive(options, options.CallBack);
                }
            }
    };

    EndSession = function () {
        clearInterval(localStorage.Timer);
        localStorage.clear();
        window.location.replace(constants.defaultPage);
    };

    Logout = function (userId, IsRedirect, code) {
        //Cleaning the master menu to load the new users one
        app.core.Post(constants.fasiApi.members + 'UserLogOff',
            true,
            false,
            undefined,
            false,
            function (data) {
                if (data && data.Successfully) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: '/fasi/wmethods/User.aspx/LogOut',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            UserClean();
                            clearInterval(localStorage.Timer);
                            localStorage.clear();
                            if (IsRedirect) {
                                if (data.d.Url !== "") {
                                    window.location.href = data.d.Url;
                                } else {
                                    if (!code) {
                                        window.location.replace(constants.defaultPage);
                                    }
                                    else {
                                        window.location.href = constants.defaultPage + '?GSCode=' + code;
                                    }
                                }
                            }
                        }
                    });
                }
            },
            null
        );
    };

    IsConnected = function () {
        if (app.user.isAnonymous)
            window.location = '/fasi/security/logIn.aspx?urlsource=' + encodeURI(window.location.href);
    };

    IsRoleExpecial = function (roles) {
        var result = false;
        if (!constants.SpecialRoles) {
            $.ajax({
                type: "GET",
                async: false,
                url: '/fasi/wmethods/User.aspx/SpecialRoles',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    constants.SpecialRoles = data.d;
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        roles.forEach(function (elementSource) {
            if (elementSource == constants.SpecialRoles.AnonymousRole) {
                result = true;
            }
        });
        return result;
    };

    UserCheckEquals = function (token, userId) {
        var result = false;
        $.ajax({
            type: "POST",
            async: false,
            url: '/fasi/wmethods/User.aspx/UserCheckEquals',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Token: token, UserId: userId }),
            dataType: "json",
            success: function (data) {
                result = data.d;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    AutoLogIn = function (token, languageId) {
        var result = false;
        $.ajax({
            type: "POST",
            async: false,
            url: '/fasi/wmethods/User.aspx/AutoLogin',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Token: token, LanguageId: languageId }),
            dataType: "json",
            success: function (data) {
                result = data.d;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    SessionLive = function () {
        $.ajax({
            type: "GET",
            async: false,
            url: '/fasi/wmethods/User.aspx/SessionLive',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
            }
        });
    };

    /**
     * Devuelve el token de acceso de FASI tomando en cuenta la vigencia de 30 min del mismo.
     * @param {boolean} force define si se reset el token o no
     * @returns {string} Token de acceso de BeAware
     */
    Token = function (force) {
        var result = '';

        //if (force === undefined) {
        //    force = false;
        //}

        //var callBack = function () {
        //    result = CreateToken();
        //    TokenInit(result);
        //    return result;
        //};

        //if (!TokenIsValid()) {
        //    result = callBack();
        //}
        //else {
        result = localStorage.getItem(constants.fields.security.FASIToken);
        //    if (result === "") {
        //        result = callBack();
        //    }
        //}
        return result;
    };

    /**
    * Define si un token es valido o no
    * @returns {string} retorna el estado del token
    */
    TokenIsValid = function () {
        var result = false;
        if (localStorage.getItem(constants.fields.security.FASITokenDate)) {
            result = (new Date().getTime() - new Date(localStorage.getItem(constants.fields.security.FASITokenDate)).getTime()) / 60000 < 5;
        }
        return result;
    };

    /**
    * Inicia-liza la variables para el localStorage de token
    * @param {string} token Token de acceso de FASI
    * @param {boolean} reset Resetea la feccah
    */
    TokenInit = function (token, reset) {
        localStorage.setItem(constants.fields.security.FASITokenDate, new Date());
        localStorage.setItem(constants.fields.security.FASIToken, token);
        app.user.token = token;
        localStorage.setItem(constants.fields.security.userProfile, JSON.stringify(app.user));
    };

    /**
    * Devuelve el token de acceso de FASI sin manejo de cache.
    * @returns {string} Token de acceso de Fasi
    */
    CreateToken = function () {
        var result = '';
        $.ajax({
            type: "POST",
            async: false,
            url: '/fasi/wmethods/User.aspx/Token',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                result = data.d.Token;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        TokenInit(result);
        return result;
    };

    AuthorizationProcess = function (data) {
        if (!app.user.isAnonymous) {
            var ee = window.location;
        } else {
            var rr = window.location;
        }
    }


    return {
        //GetUser: function (options) {
        //    return GetUser(options);
        //},
        GetUser: function (options, callback) {
            GetUser(options, callback);
        },
        GetUser2: function (options) {
            var callback = function () {
                masterSupport.Enviroment();
            };
            return GetUser2(options, callback);
        },
        Fields: function () {
            return constants.fields;
        },
        UserClean: function () {
            UserClean();
        },
        EndSession: function () {
            return EndSession();
        },
        UserContext: function (options) {
            return app.user;
            //var callback = function () {
            //    if (typeof masterSupport !== 'undefined') {
            //        masterSupport.Enviroment();
            //    }
            //};
            //return UserContext(options, callback);
        },
        SessionIdCheck: function (sessionId) {
            return SessionIdCheck(sessionId);
        },
        SessionCheck: function () {
            return SessionCheck();
        },
        SessionReset: function (interval) {
            return SessionReset(interval);
        },
        SessionSetUp: function (timeout) {
            SessionSetUp(timeout);
        },
        Logout: function (userId, IsRedirect, code) {
            Logout(userId, IsRedirect, code);
        },
        IsConnected: function () {
            IsConnected();
        },
        IsRoleExpecial: function (roles) {
            return IsRoleExpecial(roles);
        },
        UserCheckEquals: function (token, userId) {
            return UserCheckEquals(token, userId);
        },
        AutoLogIn: function (token, languageId) {
            return AutoLogIn(token, languageId);
        },
        SessionLive: function () {
            return SessionLive();
        },
        Token: function (force) {
            return Token(force);
        },
        TokenIsValid: function () {
            return TokenIsValid();
        },
        TokenInit: function (token, reset) {
            TokenInit(token, reset);
        },
        IsALive: function (options, callBack) {
            return IsALive(options, callBack);
        },
        Authorization: function (options, callBack) {
            return Authorization(options, callBack);
        },
        PageSetup: function (options) {
            PageSetup(options);
        },
        MasterMenuKey: function () {
            return MasterMenuKey();
        },
        AuthorizationProcess: function (data) {
            return AuthorizationProcess(data);
        }
    };
})();