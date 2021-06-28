var masterSupport = new function () {
    this.NotificationTimer = null;

    //Obtiene la lista de menús del usuario
    this.getMenu = function () {
        var callback = function (data) {
            if (app.user.isAnonymous) {
                $('.metismenu li').not('li:first').remove();
            }
            masterSupport.loadMenu(data);
            if (typeof defaultPage !== 'undefined') {
                defaultPage.createGridStack(masterSupport.getUrlParameter('pageId'));
            }
            $('#side-menu').metisMenu();
        };
        var key = app.security.MasterMenuKey();
        if (!localStorage.getItem(key)) {
            $.ajax({
                type: 'GET',
                url: constants.fasiApi.fasi + 'PagesByUserId?userId=' + app.user.userId + '&languageId=' + generalSupport.LanguageId(),
                datatype: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                }
            })
                .done(function (data) {
                    localStorage.setItem(key, JSON.stringify(data));
                    callback(data);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    serrorFunction();
                });
        }
        else {
            var data = JSON.parse(localStorage.getItem(key));
            callback(data);
        }
    };

    // Carga el menú en la página con los datos del servidor
    this.loadMenu = function (data) {
        if (data !== undefined && data !== null && data.Successfully) {
            var sideMenu = $('#side-menu');
            var pageId = masterSupport.getUrlParameter('pageId');
            $.each(data.Data, function (index, item) {
                var isFound = false;
                $.each(sideMenu.children(), function (indexUI, itemUI) {
                    if ($(itemUI).hasClass("nav-header") == false) {
                        if (item.Id == 0) {
                            if (item.Title == itemUI.title) {
                                isFound = true;
                                return;
                            }
                        } else {
                            if (item.Id == itemUI.id) {
                                isFound = true;
                                return;
                            }
                        }
                    }
                });

                if (isFound == false) {
                    if (pageId === undefined || pageId === "") {
                        sideMenu.append(masterSupport.makeMenuItem(index, item, item.Id, 1));
                    }
                    else {
                        sideMenu.append(masterSupport.makeMenuItem(index, item, pageId, 1));
                    }
                }
            });
        }
    };

    // Crea un item en el menú, su llamada puede ser recursiva para menús que continen sub menú
    this.makeMenuItem = function (index, item, pageId, level) {
        var htmlItem = '';
        var isActive = false;
        // Verifica se el menú contiene sub menús
        var hasSubItems = item.Children && item.Children.length > 0;

        // Cuando el id es mayor que zero significa que es de la página default
        var href = (item.Id > 0)
            ? constants.defaultPage + '?pageId=' + item.Id + '"'
            : (hasSubItems)
                ? '#'
                : item.Url;

        // Verifica si el menú está activo (seleccionado)
        if ((index === 0 && level === 1 && pageId === '' && window.location.pathname === constants.defaultPage)
            || (pageId !== '' && pageId === item.Id.toString())
            || (item.Url && window.location.pathname === item.Url.split('?')[0])
            || (pageId !== '' && pageId === item.Id && item.Order === 1 && level === 1 && window.location.pathname === constants.defaultPage)) {
            isActive = true;

            $('#menuItemName').val(item.Title);
            $('#menuOrder').val(item.Order);
            $('#btnSaveMenuItem').click(function (event) { masterSupport.btnSaveMenuItemClick(event, item.Id); });
            $('#btnDeleteMenuItem').click(function (event) { masterSupport.btnDeleteMenuItemClick(event, item.Id); });

            if (item.LayoutType < 1 || item.LayoutType > 3 || !item.LayoutType)
                item.LayoutType = 3;

            $("#columns" + item.LayoutType).attr('checked', true);
        }

        htmlItem = '<li id="' + item.Id + '" {activeClass} data-layout="' + item.LayoutType + '" title="' + item.Title + '" >' +
            '<a href="' + href + '">' +
            (item.Icon ? '<i class="' + item.Icon + '"></i>' : '') +
            '<span class="nav-label">' + item.Title + '</span>' +
            (hasSubItems ? '<span class="fa arrow"></span>' : '') +
            '</a>';

        if (hasSubItems) {
            var classLevel = (level === 1 ? "nav-second-level" : "nav-third-level");

            htmlItem += '<ul class="nav ' + classLevel + (isActive ? '' : ' collapse') + '">';

            $.each(item.Children, function (indexSubItem, subItem) {
                htmlItem += masterSupport.makeMenuItem(indexSubItem, subItem, pageId, ++level);
            });
            htmlItem += '</ul>';
        }
        else htmlItem += '</li>';

        htmlItem = htmlItem.replace('{activeClass}', (isActive || htmlItem.indexOf('class="active"') !== -1 ? 'class="active"' : ''));
        return htmlItem;
    };

    // Obtiene el usuario, sea autenticado o anónimo
    this.getUser = function () {
        // Si no existe lenguaje seleccionada le pone la default
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }
        else if (localStorage.getItem('languageId') !== constants.defaultLanguageId) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }

        app.security.UserContext();

        if (!app.security.SessionIdCheck(app.user.sessionId) || app.security.TokenIsValid()) {
            app.security.UserContext({ reset: true });
        }

        localStorage.setItem('languageName', app.user.languageName);
        localStorage.setItem('languageId', app.user.languageID);

        if (!app.user.isAnonymous) {
            $('#profileDropdown').css('display', '');
            $('#userName').html(app.user.firstNameAndSecondLastName);
            $('#userType').html(app.user.type);

            // Habilita las funcionalidades de usuario autenticado
            $('#signIcon').prop('class', 'fa fa-sign-out');
            $('#signLink').on('click', function () {
                app.security.Logout(app.user.userId, true);
            });
            $('#signIcon').prop('title', dict.LogOut[generalSupport.LanguageName()]);

            if (window.location.pathname === constants.defaultPage) {
                $("#small-chat").css('display', '');
                $('#menuConfiguration').css('display', '');
            }
            $(".theme-config-box").prop('title', dict.Configuration[generalSupport.LanguageName()]);
        } else {
            $('#signLink').on('click', function () {
                window.location.replace(constants.logInPage);
            });
            $('#signIcon').prop('title', dict.LogIn[generalSupport.LanguageName()]);
        }
        return app.user;
    };

    // Carga las opciones de lenguaje
    this.LanguageInit = function () {
        var callback = function (data) {
            try {
                if (data && data.length > 0) {
                    var languageSelection = $('#languageSelection');
                    languageSelection.html('');

                    $.each(data, function (index, item) {
                        if (index !== 0)
                            languageSelection.append('<li class="divider"></li>');

                        languageSelection.append('<li>' +
                            '<a href="javascript:masterSupport.changeLanguage(' + item.Code + ',\'' + item.CultureCode + '\')">' +
                            '<div>' + item.Description + '</div>' +
                            '</a>' +
                            '</li>');
                    });
                }
                translator.initialize(generalSupport.LanguageName());
            } catch (e) {
                console.log("Error Langauage init" + e);
            }
        };

        var languageId = generalSupport.LanguageId();
        var key = 'languageId_' + languageId;
        var itemsLanguage = null;
        if (!localStorage.getItem(key)) {
            $.ajax({
                url: constants.fasiApi.fasi + 'Language?languageId=' + languageId,
                type: 'GET',
                datatype: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                }
            })
                .done(function (data) {
                    itemsLanguage = data;
                    localStorage.setItem(key, JSON.stringify(data));
                    callback(data);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    serrorFunction();
                });
        } else {
            itemsLanguage = JSON.parse(localStorage.getItem(key));
            callback(itemsLanguage);
        }
    };

    // Cambia la lenguaje
    this.changeLanguage = function (languageId, languageName) {
        generalSupport.LanguageSynchronization(languageId, languageName);
        generalSupport.LanguageIdSet(languageId);
        generalSupport.LanguageNameSet(languageName);
        window.location.reload(true);
    };

    // Obtiene el valor de parámetros de la url (por nombre)
    this.getUrlParameter = function (name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    };

    // Control de visibilidad de la configuración de página
    this.controlActions = function () {
        $('.spin-icon').click(function () {
            $(".theme-config-box").toggleClass("show");
        });
    };

    // Agrega un nuevo item al menú
    this.createMenuItem = function () {
        ajaxJsonHelper.post(constants.fasiApi.fasi + 'PageAdd?languageId=' + generalSupport.LanguageId(), null,
            function (data) {
                if (data !== undefined && data !== null && data.Successfully) {
                    generalSupport.LocalStorageRemoveStartWith(constants.fields.security.masterMenu);
                    window.location.replace(constants.defaultPage + '?pageId=' + data.Data.Id);
                }
            });
    };

    // Guarda los datos de la página
    this.btnSaveMenuItemClick = function (event, pageId) {
        var formInstance = $("#menuItemMainForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var layoutType = $('input:radio[name=Columns]:checked').val();
            if ($('#' + pageId).data('layout') != layoutType)
                defaultPage.resetGridStackColumns(layoutType);

            ajaxJsonHelper.post(constants.fasiApi.fasi + "PageChange?pageId=" + pageId + "&newTitle=" + encodeURIComponent($('#menuItemName').val()) + "&languageId=" + generalSupport.LanguageId() + "&layoutType=" + layoutType + '&order=' + $('#menuOrder').val(), null,
                function (data) {
                    if (data !== undefined && data !== null && data.Successfully) {
                        generalSupport.LocalStorageRemoveStartWith(constants.fields.security.masterMenu);
                        window.location.replace(constants.defaultPage + '?pageId=' + pageId);
                    }
                });
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);
        event.preventDefault();
    };

    // Elimina una página
    this.btnDeleteMenuItemClick = function (event, pageId) {
        var nodes = $('.grid-stack').data('gridstack').grid.nodes;
        var title = "";

        if (nodes.length === 0) {
            title = dict.ConfirmationDeletionPage[generalSupport.LanguageName()];
        }
        else {
            title = dict.ConfirmationDeletionPageWithWigets[generalSupport.LanguageName()];
        }
        swal({
            title: title,
            text: null,
            type: "warning",
            showCancelButton: true,
            cancelButtonText: dict.CancelNo[generalSupport.LanguageName()],
            confirmButtonColor: "#ec4758",
            confirmButtonText: dict.DeleteYes[generalSupport.LanguageName()],
            closeOnConfirm: true
        }, function () {
            ajaxJsonHelper.delete(constants.fasiApi.fasi + 'PageDelete?pageId=' + pageId, null,
                function (data) {
                    if (data !== undefined && data !== null && data.Successfully) {
                        generalSupport.LocalStorageRemoveStartWith(constants.fields.security.masterMenu);
                        window.location.replace(constants.defaultPage);
                    }
                });
        });
        event.preventDefault();
    };

    // Configuración del jquery.validate
    this.validateSetup = function () {
        var requiredMesage = dict.RequiredField[generalSupport.LanguageName()];
        var maxNameMessage = dict.PageNameMaxLength[generalSupport.LanguageName()];
        var maxOrderMessage = dict.PageOrderMaxLength[generalSupport.LanguageName()];
        var onlyDigitsMessage = dict.OnlyDigit[generalSupport.LanguageName()];
        $("#menuItemMainForm").validate({
            rules: {
                PageName: { required: true, maxlength: 45 },
                Columns: { required: true },
                MenuOrder: { required: true, maxlength: 3, digits: true }
            },
            messages: {
                PageName: { required: requiredMesage, maxlength: maxNameMessage },
                Columns: { required: requiredMesage },
                MenuOrder: { required: requiredMesage, maxlength: maxOrderMessage, digits: onlyDigitsMessage }
            }
        });
    };

    // Inicializa los tooltips con el texto traducido
    this.initializeToolTips = function () {
        $('#helpIcon').prop('title', dict.Help[generalSupport.LanguageName()]);
        $('[data-toggle="tooltip"]').tooltip();
        $('[data-trn-key="Copyright"]').tooltip();

        //Translate Configuration Start
        $.i18n.init({
            resGetPath: location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/fasi/locales/MasterPage.__lng__.json',
            load: 'unspecific',
            fallbackLng: false,
            lng: generalSupport.LanguageName() ? generalSupport.LanguageName() : constants.defaultLanguageName
        }, function (t) {
            $('#app').i18n();
        });
    };

    // Define el título de la página
    this.setPageTitle = function (pageTitle) {
        var headerPageTitle = $('#pageTitle');
        headerPageTitle.html(pageTitle);
        headerPageTitle[0].parentNode.parentNode.style.display = '';
        $('.navbar')[0].classList.remove('white-bg');
    };

    // Se inicializa la busca por transacciones
    this.initializeTransactionSearch = function () {
        if (!app.user.isAnonymous && app.user.isEmployee) {
            $('#top-search').select2({
                placeholder: dict.SearchTransaction[generalSupport.LanguageName()] + '...',
                language: generalSupport.LanguageName(),
                maximumInputLength: 20,
                minimumInputLength: 3,
                ajax: {
                    type: "GET",
                    url: constants.fasiApi.backoffice + 'GetTransaction',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    delay: 250,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    data: function (params) {
                        // Se formatan los datos que se envía por parámetro
                        var query = {
                            prefix: params.term ? params.term : '',
                            userSchema: app.user.schemeCode,
                            transactionCode: ''
                        };
                        return query;
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.status === 401) {
                            notification.swal.infoCallback(dict.ExpiredSession[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { window.location.reload(true); });
                        } if (textStatus != "abort") {
                            generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                        }
                    },
                    processResults: function (response) {
                        if (response.Successfully) {
                            var data = new Array();
                            // Se formatea los datos que recibe el componente
                            $.each(response.Data.Items, function (index, obj) {
                                data.push({ id: obj.split('-')[0].trim(), text: obj });
                            });

                            return {
                                results: data,
                                pagination: {
                                    more: false
                                }
                            };
                        }
                    }
                }
            }).on('change.select2', function (e) {
                if ($('#top-search').val() != null) {
                    masterSupport.openSelectedTransaction();
                    $("#top-search").val(null);
                    $('#top-search').trigger('change');
                }
            });
        }
    };

    // Abre la transacción seleccionada
    this.openSelectedTransaction = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.backoffice + 'MakeURLIfAllowed',
            {
                windowLogicalCode: $('#top-search').val(),
                schemaCode: app.user.schemeCode,
                companyId: app.user.companyId
            },
            function (response) {
                $.LoadingOverlay("hide");

                if (response.Successfully) {
                    var lstrURL = response.Data.Url.substr(response.Data.Url.indexOf('sCodispl=') + 9);
                    var lintLength = lstrURL.indexOf('&');
                    var lstrCodispl = lstrURL.substr(0, lintLength);
                    var win = open(window.location.protocol + '//' + window.location.host + response.Data.Url, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                    if (win != null) {
                        win.moveTo(0, 0);
                        win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                    }
                }
                else if (response.Reason.length > 0)
                    notification.swal.error('', response.Reason);
            });
    };

    this.NotificationClean = function (type, subType) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.notifications + 'NotificationsUpdate',
            JSON.stringify({
                Type: type,
                SubType: subType
            }),
            function (response) {
                $.LoadingOverlay("hide");
                if (response.Successfully) {
                    masterSupport.makeMessageMenu();
                }
            });
    };

    this.refreshMenu = function (app) {
        try {
            masterSupport.NotificationTimer = $.periodic({ period: 30000, decay: 1.2, max_period: 60000 }, function () {
                masterSupport.makeMessageMenu(app);
            });
        } catch (e) {
            console.log(e);
        }
    };

    // Monta el menu de mensajes
    this.makeMessageMenu = function (app) {
        if (!app.user.isAnonymous) {
            $('#divNotifications').show();
            var messageContainer = $('#NotificationMessageContainer');
            $('#NotificationMessageContainer li').remove();

            $.ajax({
                url: constants.fasiApi.notifications + 'GetNotifications',
                type: 'GET',
                contentType: "application/json; charset=utf-8",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                }
            })
                .done(function (response) {
                    if (response && response.Count > 0) {
                        var count = parseInt($('#alertsCount').html(), 0);
                        if (isNaN(count)) {
                            count = 0;
                        }
                        if (count !== parseInt(response.Count)) {
                            $('#alertsCount').addClass('animated rubberBand');
                            window.setTimeout(function () {
                                $('#alertsCount').removeClass('animated rubberBand');
                            }, 3000);
                        }
                        $('#OptionCountInfo').removeClass('disabled');
                        $('#alertsIcon').css({ 'color': 'red' });
                        $('#alertsCount').show();
                    } else {
                        $('#OptionCountInfo').addClass('disabled');
                        $('#alertsCount').addClass('animated rubberBand');
                        $('#alertsIcon').css({ 'color': 'grey' });
                        $('#alertsCount').hide();
                    }

                    $('#alertsCount').html(response.Count);

                    $.each(response.Items, function (index, item) {
                        if (index > 0)
                            messageContainer.append('<li class="divider"></li>');

                        // Notificación
                        var html = '<li>' +
                            '<a href="{url}">' +
                            '<div>' +
                            '<i class="fa {icon}"></i> {message}' +
                            '<span class="pull-right text-muted small">{datetime}</span>' +
                            '</div>' +
                            '</a>' +
                            '</li>';

                        var lastUpdateOn = moment(new Date(item.LastUpdateOn));
                        var CurrentDate = moment();

                        var days = CurrentDate.diff(lastUpdateOn, 'days', false);

                        var messageDay = days + " " + dict.Day[generalSupport.LanguageName()] + (days <= 1 ? "" : 's');

                        html = html.replace('{url}', item.Url ? (item.Url !== '@clear@' ? item.Url : 'javascript:masterSupport.NotificationClean(' + item.Type + ',' + item.SubType + ')') : '#');
                        html = html.replace('{icon}', item.Icon);
                        html = html.replace('{message}', item.Count && item.Count >= 0 ? item.Message.replace('{count}', item.Count) : item.Message);
                        html = html.replace('{datetime}', messageDay);

                        messageContainer.append(html);
                    });
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $.LoadingOverlay("hide");
                    if (typeof masterSupport.NotificationTimer != "undefined")
                        masterSupport.NotificationTimer.cancel();
                    if (jqXHR.status === 401)
                        notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { securitySupport.Logout(masterSupport.user.userId, true); });
                    else
                        generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                });
        }
    };

    this.Enviroment = function () {
        if (app.user.token !== "") {
            //var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
            //if (InMotionGITToken !== null) {
            //    if (!app.security.UserCheckEquals(InMotionGITToken, app.user.userId)) {
            //        app.security.Logout(app.user.userId, false);
            //        app.security.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
            //        app.security.UserContext();
            //    }
            //}

            generalSupport.LanguageNameSet(app.user.languageName);
            generalSupport.LanguageIdSet(app.user.languageID);

            var SessionTimeOut = generalSupport.GetParameterByName('SessionTimeOut');
            if (SessionTimeOut !== null) {
                console.log("Exit");
                clearInterval(localStorage.Timer);
                localStorage.clear();
                window.location.replace(constants.defaultPage + "?GSCode=GS001");
            }

            masterSupport.getMenu();
            masterSupport.controlActions();
            masterSupport.LanguageInit();
            masterSupport.validateSetup();
            masterSupport.initializeToolTips();
            masterSupport.initializeTransactionSearch();
            masterSupport.refreshMenu(app);

            var valGSCode = generalSupport.GetParameterByName('GSCode');
            if (valGSCode !== null) {
                securitySupport.Messages(valGSCode);
            }

            this.UI();

            if (!app.user.isAnonymous) {
               
                
                $('#profileDropdown').css('display', '');
                $('#userName').html(app.user.firstNameAndSecondLastName);
                $('#userType').html(app.user.type);

                // Habilita las funcionalidades de usuario autenticado
                $('#signIcon').prop('class', 'fa fa-sign-out');
                $('#signLink').on('click', function () {
                    app.security.Logout(app.user.userId, true);
                });
                $('#signIcon').prop('title', dict.LogOut[generalSupport.LanguageName()]);

                if (window.location.pathname === constants.defaultPage) {
                    $("#small-chat").css('display', '');
                    $('#menuConfiguration').css('display', '');
                }
                $(".theme-config-box").prop('title', dict.Configuration[generalSupport.LanguageName()]);

                var settings = generalSupport.settings(); 
                var security = generalSupport.findKey(settings, "Security");
                switch (security.Mode)
                {
                    case "HeaderAuthentication":
                    case "ActiveDirectory":
                    case "Sesame":
                        $('.PasswordChange').hide();
                        $('.dividerPasswordChange').hide();
                        
                        break;

                    default:
                }

            } else {
                $('#signLink').on('click', function () {
                    window.location.replace(constants.logInPage);
                });
                $('#signIcon').prop('title', dict.LogIn[generalSupport.LanguageName()]);
            }
        } else {
            notification.swal.error(dict.STSFailureTitle[generalSupport.LanguageName()],
                dict.STSFailureMessage[generalSupport.LanguageName()]);
        }
    };

    this.UI = function () {
        $('.navbar-minimalize').on('click', function (event) {
            event.preventDefault();
            $("body").toggleClass("mini-navbar");
            if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
                // Hide menu in order to smoothly turn on when maximize menu
                $('#side-menu').hide();
                // For smoothly turn on menu
                setTimeout(
                    function () {
                        $('#side-menu').fadeIn(400);
                    }, 200);
            } else if ($('body').hasClass('fixed-sidebar')) {
                $('#side-menu').hide();
                setTimeout(
                    function () {
                        $('#side-menu').fadeIn(400);
                    }, 100);
            } else {
                // Remove all inline style from jquery fadeIn function to reset menu state
                $('#side-menu').removeAttr('style');
            }
        });
    };
    if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        $('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 200);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        $('#side-menu').removeAttr('style');
    }

    this.Init = function (options) {
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }
        else if (parseInt(localStorage.getItem('languageId')) !== constants.defaultLanguageId) {
            localStorage.setItem('languageId', generalSupport.LanguageId());
            localStorage.setItem('languageName', generalSupport.LanguageName());
        }
        app.security.IsALive(options, undefined);
    };
};
$(document).ready(function () {
});