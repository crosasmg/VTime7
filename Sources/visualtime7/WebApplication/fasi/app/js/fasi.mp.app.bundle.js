app.core = (function () {
    /**
     * Valor numérico de un control de entrada según limites mínimo y máximo
     * @param {string} url .
     * @param {number} data .
     * @param {number} async .
     * @param {number} overlay .
     * @param {number} success .
     * @param {number} fail .
     * @returns {number} .
     */
    AjaxCallWebMethod = function (url, data, async, overlay, success, fail) {
        var result = null;
        if (overlay)
            $.LoadingOverlay("show");

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: data,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
            }
        }).done(function (data) {
            var Success = true;
            result = data;
            if (overlay)
                $.LoadingOverlay("hide");
            if (typeof data.d !== 'undefined' && typeof data.d.Success !== 'undefined') {
                if (data.d.Success === false && data.d.Reason !== undefined) {
                    NotifyFail(data.d.Code, data.d.Reason);
                    Success = false;
                }
            }
            if (typeof success !== 'undefined')
                success(data);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (overlay)
                $.LoadingOverlay("hide");
            if (typeof fail !== 'undefined')
                fail();
            ErrorHandler(jqXHR, textStatus, errorThrown);
        });
        return result;
    };

    AjaxCall = function (type, url, isSendToken, data, async, overlay, success) {
        if (overlay)
            $.LoadingOverlay("show");
        return $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: async,
            cache: false,
            data: data,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                if (isSendToken == undefined || isSendToken == true) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                }
            },
            success: function (data) {
                if (overlay)
                    $.LoadingOverlay("hide");
                if (typeof data.d !== 'undefined' && typeof data.d.Success !== 'undefined') {
                    if (data.d.Success === false && data.d.Reason !== undefined)
                        NotifyFail(data.d.Code, data.d.Reason);
                    success(data);
                }
                else
                    success(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (overlay)
                    $.LoadingOverlay("hide");
                ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
    };

    ErrorHandler = function (jqXHR, textStatus, errorThrown) {
        if (jqXHR.status !== 401) {
            var message = '';
            var title = '';
            if (jqXHR.responseJSON !== undefined)
                console.log(jqXHR.responseJSON);
            else
                if (jqXHR.responseText !== undefined) {
                    var result = this.ResponseTextHandler(jqXHR.responseText);
                    if (result.compilation) {
                        title = 'Error de compilación';
                        message = result.detail;
                    }
                }
                else
                    console.log(jqXHR);
            if (message === '' && jqXHR.responseJSON && jqXHR.responseJSON.Code) {
                message = this.ErrorDescription(jqXHR.responseJSON.Code);
            }
            if (message !== '') {
                notification.swal.error(title, message);
            } else {
                notification.swal.error('Ha ocurrido una falla, por favor intente nuevamente', 'si la falla persiste, contacte al personal técnico');
            }
        } else {
            var languajeName = generalSupport.LanguageName();
            if (languajeName == null || languajeName == undefined)
                languajeName = constants.defaultLanguageName;
            notification.swal.infoCallback(dict.NotAuthorized[languajeName], dict.ClickToRefreshPage[languajeName], function () { window.location.reload(true); });
        }
    };

    ResponseTextHandler = function (text) {
        var index = -1;
        var result = { error: null, detail: null, source: null, compilation: false };
        try {
            var compilationError = text.indexOf('<title>Compilation Error</title>') > -1;
            if (compilationError) {
                result.compilation = true;
                result.error = 'Compilation error';
                index = text.indexOf('<b> Compiler Error Message: </b>');
                if (index > -1) {
                    text = text.substring(index + 32);
                }
                index = text.indexOf('<br><br>');
                if (index > -1) {
                    result.detail = $('<div/>').html(text.substring(0, index)).text();
                    text = text.substring(index + 8);
                }
                index = text.indexOf('<code><pre>');
                if (index > -1) {
                    text = text.substring(index + 11);
                }
                index = text.indexOf('Line ');
                if (index > -1) {
                    text = text.substring(index);
                }
                index = text.indexOf('</pre></code>');
                if (index > -1) {
                    result.source = text.substring(0, index);
                    result.source = $('<div/>').html(result.source).text();
                }
                console.error(result);
            }
            else
                console.error(text);
        }
        catch (err) {
            console.error(text);
        }
        return result;
    };

    ErrorDescription = function (code) {
        var result = '';
        $.ajax({
            type: "GET",
            url: constants.fasiApi.base + 'fasi/v1/ErrorMessage?code=' + code + '&languageId=' + generalSupport.LanguageId(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
            },
            success: function (data) {
                result = data.Data;
            },
            error: function (qXHR, textStatus, errorThrown) {
                result = "Ha ocurrido una falla, por favor intente nuevamente";
            }
        });

        return result;
    };

    NotifyFail = function (code, message) {
        if (code != "401") {
            console.error({ code: code, detail: message });
            if (code === '' || code === undefined) {
                code = '1';
            }

            function ShowMessage() {
                var messageByCatalog = ErrorDescription(code);
                if (messageByCatalog !== '') {
                    notification.swal.error('', messageByCatalog);
                } else {
                    notification.swal.error('', message);
                }
            };

            if (code != "901") {
                ShowMessage();
            } else {
                if (app.user.isAnonymous) {
                    if (!window.location.href.toLowerCase().toString().includes('fasi/dli/queries')) {
                        window.location = '/fasi/default.aspx?GSCode=' + data.authorization.Code;
                    } else {
                        location.reload();
                    }
                }
            }
        } else {
            notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { window.location.reload(true); });
        }
    };

    Setting = function (name) {
        return app.core.SyncWebMethod('/fasi/wmethods/general.aspx/SettingValue', false, JSON.stringify({ name: name })).d;
    };

    return {
        ErrorHandler: function (jqXHR, textStatus, errorThrown) {
            return ErrorHandler(jqXHR, textStatus, errorThrown);
        },
        AsyncWebMethod: function (url, overlay, data, success, fail) {
            return AjaxCallWebMethod(url, data, true, overlay, success, fail);
        },
        SyncWebMethod: function (url, overlay, data, success, fail) {
            return AjaxCallWebMethod(url, data, false, overlay, success, fail);
        },
        AsyncGet: function (url, token, overlay, data, success) {
            return AjaxCall('GET', url, token, data, true, overlay, success);
        },
        SyncGet: function (url, token, overlay, data, success) {
            return AjaxCall('GET', url, token, data, false, overlay, success);
        },
        Get: function (url, overlay, async, token, data, success, fail) {
            return AjaxCall('GET', url, token, data, async, overlay, success);
        },
        /**
         *  Método que realiza el post.
         * @param {any} url URL del operación.
         * @param {any} overlay Si se muerta un ventana de loading.
         * @param {any} async Si el método se dispara async o no.
         * @param {any} token Envía un token para validar la operación.
         * @param {any} data Datos a enviar al method.
         * @param {any} success Función de success.
         * @param {any} fail Función de error.
         * @returns {any} Función que realiza el done
         */
        Post: function (url, overlay, async, token, data, success, fail) {
            return AjaxCall('POST', url, token, data, async, overlay, success);
        },
        Put: function (url, overlay, async, data, success, fail) {
            return AjaxCall('PUT', url, token, data, async, overlay, success);
        },
        Delete: function (url, overlay, async, data, success, fail) {
            return AjaxCall('DELETE', url, token, data, async, overlay, success);
        },
        Setting: function (name) {
            return Setting(name);
        }
    };
})();
var generalSupport = new function () {
    /** Método que obtiene los settings de configuración del web.config - configuration
     * @returns {number} Objeto con los setting de fasi
     */
    this.Settings = function () {
        var result;
        generalSupport.Operation("settings", { id: "" }, function (data) {
            result = data.d;
        });
        return result;
    };

    /**
     * Método que busca un key en objeto dynamic
     * @param {any} root Objecto dynamic
     * @param {any} key Valor a buscar en los keys
     * @returns {any} El valor del key.
     */
    this.findKey = function (root, key) {
        var result;
        if (Array.isArray(root)) {
            result = $.grep(root, function (v) {
                return v.Key === key;
            })[0].Value;
        } else {
            result = root[key];
        }
        return result;
    };

    this.ResourceByKey = function (key) {
        var result = "";
        if (!localStorage.getItem("GeneralResource_" + generalSupport.LanguageName().toLowerCase())) {
            $.ajax({
                url: '/fasi/locales/General.' + generalSupport.LanguageName().toLowerCase() + ".json",
                dataType: 'json',
                data: data,
                async: false,
                success: function (json) {
                    var data = json;
                    localStorage.setItem("GeneralResource_" + generalSupport.LanguageName().toLowerCase(), JSON.stringify(data));
                    result = data.app[key];
                }
            });
        } else {
            var data = JSON.parse(localStorage.getItem("GeneralResource_" + generalSupport.LanguageName().toLowerCase()));
            result = data.app[key];
        }
        return result;
    };

    this.Operation = function (operation, parameters, callBack) {
        app.core.Post('/fasi/wmethods/User.aspx/Operation',
            false,
            false,
            false,
            JSON.stringify({
                operation: operation,
                body: JSON.stringify(parameters)
            }),
            function (data) {
                callBack(data);
            },
            null
        );
    };

    /**
     * Extensiones de jQuery Validate para controles usando en FASI.
     */

    //Initialization
    if (!String.prototype.endsWith) {
        String.prototype.endsWith = function (suffix) {
            return this.indexOf(suffix, this.length - suffix.length) !== -1;
        };
    }

    this.ExtendValidators = function () {
        $.validator.addMethod("AutoNumericRequired", function (value, element) {
            return this.optional(element) || AutoNumeric.getNumber('#' + element.id) !== 0;
        }, "El campo es requerido");
        $.validator.addMethod("AutoNumericMinValue", function (value, element, arg) {
            var result = true;
            if (AutoNumeric.getNumber('#' + element.id) < arg)
                result = false;
            return result;
        });
        $.validator.addMethod("AutoNumericMaxValue", function (value, element, arg) {
            var result = true;
            if (AutoNumeric.getNumber('#' + element.id) > arg)
                result = false;
            return result;
        });
        $.validator.addMethod("DatePicker", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#' + element.id) <= moment(new Date('0001-01-01T00:00:00')).utc())
                    result = false;
            }
            return result;
        });
        $.validator.addMethod("OnlyNumber", function (value, element, arg) {
            var result = true;
            if (!this.optional(element))
                if (!/^[0-9]+$/.test(value))
                    result = false;
            return result;
        }, 'Formato invalido, solo se aceptan dígitos numéricos');
    };

    /**
     * Valor numérico de un control de entrada según limites mínimo y máximo
     * @param {string} inputId Identificador del control de entrada de html
     * @param {number} minimumValueAllowed Valor mínimo permitido
     * @param {number} maximumValueAllowed Valor máximo permitido
     * @returns {number} Valor numérico valido según limites mínimo y máximo
     */
    this.NumericValue = function (inputId, minimumValueAllowed, maximumValueAllowed) {
        var value = AutoNumeric.getNumber(inputId);
        if (value < minimumValueAllowed)
            value = minimumValueAllowed;
        if (value > maximumValueAllowed)
            value = maximumValueAllowed;
        return value;
    };

    Array.prototype.contains = function (obj) {
        var i = this.length;
        while (i--) {
            if (this[i] === obj) {
                return true;
            }
        }
        return false;
    };

    this.LocalStorageRemoveStartWith = function (key) {
        var result = []; // Array to hold the keys
        // Iterate over localStorage and insert the keys that meet the condition into arr
        for (var i = 0; i < localStorage.length; i++) {
            if (localStorage.key(i).indexOf(key) !== -1) {
                result.push(localStorage.key(i));
            }
        }

        // Iterate over arr and remove the items by key
        for (var i = 0; i < result.length; i++) {
            localStorage.removeItem(result[i]);
        }
        //return result;
    };

    this.NormalizeProperties = function (source, properties) {
        var propertyArray;
        if (properties !== "") {
            propertyArray = properties.split(",");
            source.forEach(function (elementSource) {
                for (var prop in elementSource) {
                    var exitProperty = propertyArray.contains(prop);
                    if (exitProperty === true) {
                        var value = elementSource[prop];
                        if (Object.prototype.toString.call(value) === '[object String]' && value.indexOf("Date") !== -1) {
                            elementSource[prop] = generalSupport.ToJavaScriptDate2(value);
                        }
                    }
                }
            });
        }
        return source;
    };

    /** Remover método
     * @return {SessionContext} return el object SessionContext
     */
    this.SessionContext = function () {
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }
        return {
            languageId: localStorage.getItem('languageId'),
            clientId: app.user.clientId,
            producerId: app.user.producerId
        };
    };

    /** Remover método
     * @return {UserContext} return el object UserContext
     */
    this.UserContext = function () {
        if (typeof masterSupport !== "undefined" && window.location.pathname.toLowerCase().endsWith('/fasi/default.aspx') == false) {
            masterSupport.Init();
            var data = generalSupport.Settings();
            constantsSupport.setup(data);
        }

        console.warn('Opción sin optimizar');
        if (generalSupport.user === undefined) {
            if (typeof masterSupport !== 'undefined' && typeof masterSupport.user !== 'undefined')
                generalSupport.user = masterSupport.user;
            else
                generalSupport.getUser();
        }

        if (generalSupport.user.languageName.toLowerCase().indexOf("-") !== -1) {
            generalSupport.user.languageName = generalSupport.user.languageName.split('-')[0];
        }

        app.user = generalSupport.user;
        return generalSupport.user;
    };

    /** Remover método
     * @param {any} data data a convertir
     * @return {UserConvert} return el object UserConvert
     */
    this.UserConvert = function (data) {
        return {
            userName: data.username,
            userId: data.userId,
            companyId: data.companyId,
            isAnonymous: data.isAnonymous,
            isAdministrator: data.isAdministrator,
            schemeCode: data.schemeCode,
            token: data.token,
            clientId: data.clientId,
            producerId: data.producerId,
            firstNameAndSecondLastName: data.firstNameAndSecondLastName,
            languageID: data.languageID,
            languageName: data.languageName,
            type: data.type,
            utcOffset: data.utcOffset,
            isEmployee: data.IsEmployee,
            sessionId: data.sessionId,
            expiration: data.expiration,
            timeZoneVT: data.TimeZoneVT
        };
    };

    /** Remover método */
    this.getUser = function () {
        if (window.location.pathname.toLowerCase().endsWith('default.aspx')) {
            generalSupport.user = generalSupport.UserConvert(app.user);
            app.security.TokenInit(app.user.token, true);
        } else {
            console.warn('Opción sin optimizar');
            // Obtiene el código del usuario
            var url = '/fasi/wmethods/User.aspx/GetUserInformation';
            languageName = generalSupport.GetParameterByName('culture');

            if (languageName) {
                url = url + '?culture=' + languageName;
            }

            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json"
            }).done(function (data) {
                    generalSupport.user = generalSupport.UserConvert(data.d);
                    app.user = generalSupport.user;
                    app.security.TokenInit(app.user.token, true);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 401) {
                    app.security.EndSession();
                }
            });
        }
        generalSupport.CreateVTTimeZone();
    };

    this.URLDateValue = function (key) {
        var value = generalSupport.URLValue(key);
        if (value !== null)
            value = moment(value, generalSupport.DateFormat()).format(generalSupport.DateFormat());
        return value;
    };

    this.URLNumericValue = function (key) {
        var value = generalSupport.URLValue(key);
        if (value === null)
            value = 0;
        else
            if (isNaN(value))
                value = 0;
            else
                value = parseInt(value);
        return value;
    };

    this.URLStringValue = function (key) {
        var value = generalSupport.URLValue(key);
        if (value === null)
            value = '';
        return value;
    };

    this.URLValue = function (key) {
        key = key.replace(/[\[]/, '\\[');
        key = key.replace(/[\]]/, '\\]');
        var pattern = "[\\?&]" + key + "=([^&#]*)";
        var regex = new RegExp(pattern, "i");
        var url = unescape(window.location.href);
        var results = regex.exec(url);
        if (results === null) {
            return null;
        } else {
            return results[1];
        }
    };

    this.ErrorHandler = function (jqXHR, textStatus, errorThrown, IsDelete) {
        var message = '';
        var isShow = false;
        var title = '';
        if (jqXHR.status !== 401) {
            if (jqXHR.responseJSON !== undefined) {
                //console.log(jqXHR.responseJSON);
                if (jqXHR.responseJSON.Code || jqXHR.responseJSON.Reason) {
                    isShow = true;
                    generalSupport.NotifyFail(jqXHR.responseJSON.Reason, jqXHR.responseJSON.Code, IsDelete);
                }
            }
            else
                if (jqXHR.responseText !== undefined) {
                    var result = this.ResponseTextHandler(jqXHR.responseText);
                    if (result.compilation) {
                        title = 'Error de compilación';
                        message = result.detail;
                    }
                }
                else
                    console.log(jqXHR);
            if (message === '' && jqXHR.responseJSON && jqXHR.responseJSON.Code) {
                message = this.ErrorDescription(jqXHR.responseJSON.Code);
            }
            if (!isShow) {
                if (message !== '') {
                    notification.swal.error(title, message);
                } else {
                    notification.swal.error('Ha ocurrido una falla, por favor intente nuevamente', 'si la falla persiste, contacte al personal técnico');
                }
            }
        } else {
            var languajeName = generalSupport.LanguageName();
            if (languajeName == null || languajeName == undefined)
                languajeName = constants.defaultLanguageName;
            notification.swal.infoCallback(dict.NotAuthorized[languajeName], dict.ClickToRefreshPage[languajeName], function () { window.location.reload(true); });
        }
    };

    this.ResponseTextHandler = function (text) {
        var index = -1;
        var result = { error: null, detail: null, source: null, compilation: false };
        try {
            var compilationError = text.indexOf('<title>Compilation Error</title>') > -1;
            if (compilationError) {
                result.compilation = true;
                result.error = 'Compilation error';
                index = text.indexOf('<b> Compiler Error Message: </b>');
                if (index > -1) {
                    text = text.substring(index + 32);
                }
                index = text.indexOf('<br><br>');
                if (index > -1) {
                    result.detail = $('<div/>').html(text.substring(0, index)).text();
                    text = text.substring(index + 8);
                }
                index = text.indexOf('<code><pre>');
                if (index > -1) {
                    text = text.substring(index + 11);
                }
                index = text.indexOf('Line ');
                if (index > -1) {
                    text = text.substring(index);
                }
                index = text.indexOf('</pre></code>');
                if (index > -1) {
                    result.source = text.substring(0, index);
                    result.source = $('<div/>').html(result.source).text();
                }
                console.error(result);
            }
            else
                console.error(text);
        }
        catch (err) {
            console.error(text);
        }
        return result;
    };

    this.ErrorDescription = function (code) {
        //if (masterSupport.user === null || masterSupport.user === undefined) {
        //    this.getUser();
        //}

        if (code === null) {
            code = "";
        }

        var result = "";

        app.core.Get(constants.fasiApi.base + 'fasi/v1/ErrorMessage?code=' + code + '&languageId=' + generalSupport.LanguageId(),
            false,
            false,
            undefined,
            null,
            function (data) {
                result = data.Data;
            },
            null
        );

        //$.ajax({
        //    type: "GET",
        //    url: constants.fasiApi.base + 'fasi/v1/ErrorMessage?code=' + code + '&languageId=' + generalSupport.LanguageId(),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    data: JSON.stringify({}),
        //    beforeSend: function (xhr) {
        //        xhr.setRequestHeader("Authorization", "Bearer " + app.security.Token());
        //    },
        //    success: function (data) {
        //        result = data.Data;
        //    },
        //    error: function (qXHR, textStatus, errorThrown) {
        //        result = "Ha ocurrido una falla, por favor intente nuevamente";
        //    }
        //});

        return result;
    };

    this.NotifyErrorValidate = function (fvalidate) {
        var errorHtml = '';

        if (fvalidate.numberOfInvalids() > 1) {
            errorHtml = this.ResourceByKey("HasPruralsErrors");
        }
        else {
            errorHtml = this.ResourceByKey("HasSingularErrors");
        }

        errorHtml = errorHtml.replace("{0}", fvalidate.numberOfInvalids());

        notification.toastr.error('', errorHtml);

        var count = fvalidate.errorList.length;
        for (var i = 0; i < count; i++) {
            console.log(fvalidate.errorList[i]['message']);
        }
    };

    this.ToJavaScriptDate2 = function (value) {
        if (value === null)
            return null;
        else {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            if (results !== null) {
                var dt = moment(value);

                if (dt.year() <= 1)
                    return '';
                else
                    return new Date(dt.year(), dt.month(), dt.date());
            }
            else {
                return moment(value, 'YYYY-MM-DD').toDate();
            }
        }
    };

    this.ToJavaScriptDateCustom = function (value, format) {
        var pattern = /Date\(([^)]+)\)/;
        if (value) {
            var results = pattern.exec(value);
            var dt;

            if (results !== null) {
                var x = moment(value);
                if (x.year() <= 1)
                    return '';
                else {
                    //var t = (parseInt(app.user.utcOffset.substring(1, 3), 10) * 60 +
                    //    parseInt(app.user.utcOffset.substring(4, 6), 10) + x.utcOffset()) * -1;
                    //x.add(t, 'm');
                    return x.format(format);
                }
            }
            else {
                var dateFormated;
                var dateValue;
                if (value._isAMomentObject)
                    dateValue = moment(value);
                else {
                    var splitValue = value.split('/')
                    dateValue = moment(splitValue[2] + '-' + splitValue[1] + '-' + splitValue[0])
                }
                if (format !== undefined && format !== null) {
                    dateFormated = moment(value).utc().format(format);
                    if (dateValue.year() === 1) {
                        return '';
                    } else {
                        return dateFormated;
                    }
                }
                else {
                    dateFormated = moment(value);
                    if (dateValue.year() === 1) {
                        return '';
                    } else {
                        return dateFormated;
                    }
                }
            }
        } else {
            return '';
        }
    };

    this.ServerBehavior = function (DataBehavior) {
        if (typeof DataBehavior !== 'undefined' && DataBehavior !== null) {
            if (typeof DataBehavior.controlbehavior !== 'undefined' && DataBehavior.controlbehavior !== null) {
                var ctrolId;
                $.each(DataBehavior.controlbehavior, function () {
                    ctrolId = this['id'];
                    switch (this['property']) {
                        case 'hide':
                            if (ctrolId.endsWith(":Grid")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);
                                $('#' + ctrolId + 'Container').toggleClass('hidden', true);
                            }
                            else if (ctrolId.endsWith(":Date")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);

                                $('#' + ctrolId).toggleClass('hidden', true);
                                $('#' + ctrolId + '_group').toggleClass('hidden', true);
                                $('#' + ctrolId + 'Label').toggleClass('hidden', true);
                            }
                            else if (ctrolId.endsWith(":Address")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 8);

                                AddressSupport.Visible(ctrolId, false);                                
                            }
                            else {
                                if (ctrolId.endsWith("Wrap"))
                                    ctrolId = ctrolId.substring(0, ctrolId.length - 4);
                                $('#' + ctrolId + 'Label').toggleClass('hidden', true);
                                $('#' + ctrolId + 'Required').toggleClass('hidden', true);
                                if ($('#' + this['id']).next(".select2-container").length > 0) {
                                    $('#' + this['id']).hide();
                                    $('#' + this['id']).next(".select2-container").hide();
                                }
                                else
                                    if ($('#' + this['id']).length > 0)
                                        $('#' + this['id']).toggleClass('hidden', true);
                                    else
                                        $('input:radio[name=' + this['id'] + ']').parent().toggleClass('hidden', true);
                            }
                            break;
                        case 'show':
                            if (ctrolId.endsWith(":Grid")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);
                                $('#' + ctrolId + 'Container').toggleClass('hidden', false);
                            }
                            else if (ctrolId.endsWith(":Date")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);

                                $('#' + ctrolId).toggleClass('hidden', false);
                                $('#' + ctrolId + '_group').toggleClass('hidden', false);
                                $('#' + ctrolId + 'Label').toggleClass('hidden', false);
                            }
                            else if (ctrolId.endsWith(":Address")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 8);

                                AddressSupport.Visible(ctrolId, true);
                            }
                            else {
                                if (ctrolId.endsWith("Wrap"))
                                    ctrolId = ctrolId.substring(0, ctrolId.length - 4);
                                $('#' + ctrolId + 'Label').toggleClass('hidden', false);
                                $('#' + ctrolId + 'Required').toggleClass('hidden', false);
                                if ($('#' + this['id']).next(".select2-container").length > 0) {
                                    $('#' + this['id']).show();
                                    $('#' + this['id']).next(".select2-container").show();
                                }
                                else
                                    if ($('#' + this['id']).length > 0)
                                        $('#' + this['id']).toggleClass('hidden', false);
                                    else
                                        $('input:radio[name=' + this['id'] + ']').parent().toggleClass('hidden', false);
                            }
                            break;
                        case 'disabled':
                            if (ctrolId.endsWith(":Grid")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);
                                $('#' + ctrolId + 'Tbl *, #' + ctrolId + 'Popup .modal-body *, #' + ctrolId + 'Popup .modal-footer *').prop('disabled', true);
                            }
                            else if (ctrolId.endsWith(":Address")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 8);

                                AddressSupport.Enable(ctrolId, false);
                            }
                            else
                                if ($('.nav-tabs a[href="#' + this['id'] + 'Panel"]').length > 0) {
                                    $('#' + this['id']).toggleClass('disabled', true);
                                    $('#' + this['id'] + 'Panel :input').prop('disabled', true);
                                }
                                else
                                    if ($('#' + this['id']).is('div'))
                                        $('#' + this['id'] + ' :input').prop('disabled', true);
                                    else
                                        if ($('#' + this['id']).length > 0)
                                            $('#' + this['id']).prop('disabled', true);
                                        else
                                            $('input:radio[name=' + this['id'] + ']').prop('disabled', true);
                            break;
                        case 'enabled':
                            if (ctrolId.endsWith(":Grid")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 5);
                                $('#' + ctrolId + 'Tbl *, #' + ctrolId + 'Popup .modal-body *, #' + ctrolId + 'Popup .modal-footer *').prop('disabled', false);
                            }
                            else if (ctrolId.endsWith(":Address")) {
                                ctrolId = ctrolId.substring(0, ctrolId.length - 8);

                                AddressSupport.Enable(ctrolId, true);
                            }
                            else
                                if ($('.nav-tabs a[href="#' + this['id'] + 'Panel"]').length > 0) {
                                    $('#' + this['id']).toggleClass('disabled', false);
                                    $('#' + this['id'] + 'Panel :input').prop('disabled', false);
                                }
                                else
                                    if ($('#' + this['id']).is('div'))
                                        $('#' + this['id'] + ' :input').prop('disabled', false);
                                    else
                                        if ($('#' + this['id']).length > 0)
                                            $('#' + this['id']).prop('disabled', false);
                                        else
                                            $('input:radio[name=' + this['id'] + ']').prop('disabled', false);
                            break;

                        case 'active':
                            $('.nav-tabs a[href="#' + this['id'] + 'Panel"]').tab('show');
                            break;

                        case 'click':
                            $('#' + this['id']).click();
                            break;

                        case 'enableColumnLink':
                            $('#' + this['id'] + 'Tbl a').removeClass('DisableColumnLink');
                            break;

                        case 'disableColumnLink':
                            $('#' + this['id'] + 'Tbl a').addClass('DisableColumnLink');
                            break;
                    }
                });
            }
            if (typeof DataBehavior.redirect !== 'undefined' && DataBehavior.redirect !== null && DataBehavior.redirect !== '')
                if (DataBehavior.redirectsetting === null)
                    window.location.href = DataBehavior.redirect;
                else
                    window.open(DataBehavior.redirect.replace("Popup.aspx", "Popup.html"), '_blank', DataBehavior.redirectsetting);
            if (typeof DataBehavior.notify !== 'undefined' && DataBehavior.notify !== null && DataBehavior.notify !== '') {
                if (DataBehavior.notify.splash !== null && DataBehavior.notify.splash !== '')
                    notification.splash.info('', DataBehavior.notify.splash);
                else
                    if (DataBehavior.notify.popup !== null)
                        $.each(DataBehavior.notify.popup, function (index, value) {
                            notification.swal.info('', value);
                        });
                    else {
                        if (DataBehavior.notify.mode !== null || DataBehavior.notify.mode === 'alert') {
                            $("#alerts-container").html('');
                        }
                        if (DataBehavior.notify.messages !== null)
                            if (DataBehavior.notify.messages.length > 1) {
                                if (DataBehavior.notify.mode !== null || DataBehavior.notify.mode === 'alert')
                                    for (i = 0; i < DataBehavior.notify.messages.length; i++) {
                                        notification.alert.error(null, DataBehavior.notify.messages[i].message);
                                    }
                                else {
                                    var errorHtml = '';
                                    for (i = 0; i < DataBehavior.notify.messages.length; i++) {
                                        errorHtml += '<li class=""><label id="' + DataBehavior.notify.messages[i].controlname + '-error" for="' + DataBehavior.notify.messages[i].controlname + '">' + DataBehavior.notify.messages[i].message + '</label></li>';
                                    }
                                    toastr.error(errorHtml, null, {
                                        closeButton: true,
                                        preventDuplicates: true,
                                        progressBar: true,
                                        timeOut: 30000,
                                        extendedTimeOut: 20000,
                                        positionClass: 'toast-top-right'
                                    }).attr('style', 'width: 400px !important');
                                }
                            }
                            else
                                $.each(DataBehavior.notify.messages, function (index, value) {
                                    switch (value.type) {
                                        case 'Error':
                                            notification.swal.error('', value.message);
                                            break;
                                        case 'Warning':
                                            notification.swal.warning('', value.message);
                                            break;
                                        case 'Message':
                                            notification.swal.info('', value.message);
                                            break;
                                    }
                                });
                    }
            }
        }
    };

    this.GetParameterByName = function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i");
        results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    };

    this.base64ToArrayBuffer = function (arrayBuffer, filename) {
        var sliceSize = 1024;
        var bytesLength = arrayBuffer.length;
        var sliceCount = Math.ceil(bytesLength / sliceSize);
        var stringFromCharCode = '';
        var begin, end, bytes, sliceIndex, offset;

        for (sliceIndex = 0; sliceIndex < sliceCount; sliceIndex++) {
            begin = sliceIndex * sliceSize;
            end = Math.min(begin + sliceSize, bytesLength);
            bytes = new Array(end - begin);

            for (offset = begin, i = 0; offset < end; ++i, ++offset) {
                bytes[i] = arrayBuffer[offset];
            }
            stringFromCharCode += String.fromCharCode.apply(null, bytes);
        }

        var base64 = btoa(stringFromCharCode);
        var binaryString = atob(base64);
        var binaryLen = binaryString.length;
        sliceCount = Math.ceil(binaryLen / sliceSize);
        var byteArrays = new Array(sliceCount);

        for (sliceIndex = 0; sliceIndex < sliceCount; sliceIndex++) {
            begin = sliceIndex * sliceSize;
            end = Math.min(begin + sliceSize, binaryLen);
            bytes = new Array(end - begin);

            for (offset = begin, i = 0; offset < end; ++i, ++offset) {
                bytes[i] = binaryString.charCodeAt(offset);
            }
            byteArrays[sliceIndex] = new Uint8Array(bytes);
        }

        generalSupport.saveByteArray(byteArrays, filename);
    };

    this.saveByteArray = function (byte, filename) {
        var blob = new Blob(byte);
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = filename;
        link.click();
    };

    // Transform the date to a format that WCF understands
    this.jsDateToWCF = function (momentDate, dateFormat) {
        return '\/Date(' + moment(momentDate, dateFormat).utc().valueOf() + ')\/';
    };

    this.Select2GetValue = function (name, formated) {
        var result = [];
        var control = $('#' + name);
        var allItem = [],
            adapter = control.data('select2').dataAdapter;
        adapter.$element.children().each(function () {
            if (!$(this).is('option') && !$(this).is('optgroup')) {
                return true;
            }
            allItem.push(adapter.item($(this)));
        });

        if (control.data('select2').options.options.multiple === true) {
            var vales = $('#' + name).select2('data');
            $.each(vales, function (keyDefaul, value) {
                result.push(value);
            });
        }
        $.each(result, function (keyDefaulSelected, valueSelected) {
            valueSelected.selected = true;
        });

        if (formated) {
            var ids = [];
            $.each(result, function (keyDefaulSelected, valueSelected) {
                ids.push(valueSelected.id);
            });
            control.select2('val', '');
            return ids.join(";");
        }
        control.select2('val', '');
        return result;
    };

    this.Select2GetDescription = function (name, formatted, value) {
        var result = [];
        var control = $('#' + name);
        var allItem = [];
        adapter = control.data('select2').dataAdapter;
        adapter.$element.children().each(function () {
            if (!$(this).is('option') && !$(this).is('optgroup')) {
                return true;
            }
            allItem.push(adapter.item($(this)));
        });

        if (formatted) {
            var ids = [];

            if (typeof value === "string") {
                $.each(value.split(";"), function (keyDefaul, valueSplit) {
                    $.each(allItem, function (keyDefaulSelected, valueSelected) {
                        if (valueSelected.id === valueSplit) {
                            ids.push(valueSelected.text);
                        }
                    });
                });
            }

            if (Array.isArray(value)) {
                $.each(value, function (keyDefaul, valueItem) {
                    $.each(allItem, function (key, valueData) {
                        if (valueItem.id === valueData.id) {
                            ids.push(valueData.text);
                        }
                    });
                });
            }

            return ids.join(", ");
        }

        $.each(result, function (keyDefaulSelected, valueSelected) {
            value;
        });

        return result;
    };

    /**
     *
     * @param {any} data array data
     * @param {any} code código a representar el valor de code en el array
     * @param {any} description descripción del código
     * @returns {any} se retorna el array con el formato adecuado para el select2
    */
    this.LookUpConvertArray = function (data, code, description) {
        $.each(data, function (key, value) {
            if (!value.hasOwnProperty('id')) {
                value.id = this[code];
            }
            if (!value.hasOwnProperty('text')) {
                value.text = this[description];
            }
            if (!value.hasOwnProperty('selected')) {
                value.selected = false;
            }
        });
        return data;
    };

    this.Select2Load = function (name, data, code, description, defaultValue, templateResultMethod, templateSelectionMethod) {
        var ctrol = $('#' + name);
        data = generalSupport.LookUpConvertArray(data, code, description);
        $.each(data, function (key, value) {
            ctrol.append($('<option />').val(this['id']).text(this['text']));
        });

        generalSupport.Select2AssignedValue(name, code, data, defaultValue);

        if (templateResultMethod !== null) {
            var fnTemplateResultMethod = window[templateResultMethod];
            var fnTemplateSelectionMethod = window[templateResultMethod];

            ctrol.select2({
                data: data,
                multiple: true,
                allowClear: true,
                templateResult: fnTemplateResultMethod,
                templateSelection: fnTemplateSelectionMethod,
                width: '100%'
            });
        } else {
            ctrol.select2({
                data: data,
                multiple: true,
                allowClear: true,
                width: '100%'
            });
        }

        ctrol.trigger('change');
    };

    this.Select2ItemsRefresh = function (name, defaultValue) {
        var control = $('#' + name);

        var allItem = [],
            adapter = control.data('select2').dataAdapter;
        adapter.$element.children().each(function () {
            if (!$(this).is('option') && !$(this).is('optgroup')) {
                return true;
            }
            allItem.push(adapter.item($(this)));
        });

        var valueSelect = [];

        if (typeof defaultValue === "string") {
            $.each(defaultValue.split(";"), function (keyDefaul, value) {
                valueSelect.push(value.toString());
                $.each(allItem, function (key, valueData) {
                    if (value === valueData.id) {
                        valueData.selected = true;
                        valueSelect.push(valueData.id);
                    }
                });
            });
        }

        if (Array.isArray(defaultValue)) {
            $.each(defaultValue, function (keyDefaul, value) {
                valueSelect.push(value.id.toString());
                $.each(allItem, function (key, valueData) {
                    if (value.id === valueData.id) {
                        valueData.selected = true;
                        valueSelect.push(valueData.id);
                    }
                });
            });
        }

        control.val(valueSelect);

        control.trigger('change');
    };

    this.Select2AssignedValue = function (name, code, data, defaultValue) {
        var ctrol = $('#' + name);

        if (defaultValue !== null) {
            var valueSelect = [];

            if (typeof defaultValue === "string") {
                $.each(defaultValue.split(";"), function (keyDefaul, value) {
                    valueSelect.push(value.toString());
                    $.each(data, function (key, valueData) {
                        if (value === valueData.id) {
                            valueData.selected = true;
                        }
                    });
                });
            }

            if (Array.isArray(defaultValue)) {
                $.each(defaultValue, function (keyDefaul, value) {
                    valueSelect.push(value.Code.toString());
                    $.each(data, function (key, valueData) {
                        if (value[code] === valueData.id) {
                            valueData.selected = true;
                        }
                    });
                });
            }

            ctrol.val(valueSelect);
        }
    };

    this.parseStringToDecimal = function (value, decimalSeparator) {
        if (decimalSeparator === ',') {
            value = value.replace(/\./g, '');
            value = value.replace(',', '.');
        }
        else value = value.replace(/,/g, '');

        return value.replace(/[^0-9\.[^0-9]]/g, '');
    };

    // Este función debe ser eliminada y cambiar el form para que haga el llamado directo usando el notification.js
    this.NotifyFail = function (message) {
        console.error({ code: null, detail: message });
        notification.swal.error('', message);
    };

    /**
     * Método que permite mostrar el manejo de error de forma gráfica
     * @param {any} message Mensaje a mostrar al usuario
     * @param {any} Code Código del error a buscar en el repositorio de lookup de errores
     * @param {any} IsDelete Si es un llamado de un delete se muestra como up pop-up en parte inferior derecha de contrario de muestra un pop-up con swal
     */
    this.NotifyFail = function (message, Code, IsDelete) {
        //Importante por favor revisar con detallle
        var messageByCatalog;
        //console.error({ code: Code, detail: message });
        if (Code != '401') {
            if (Code === '' || Code === undefined || Code === null) {
                Code = '1';
            }

            function ShowMessage() {
                var messageByCatalog = ErrorDescription(Code);
                if (messageByCatalog !== '') {
                    notification.swal.error('', messageByCatalog);
                } else {
                    notification.swal.error('', message);
                }
            };

            if (Code === '777') {
                notification.toastr.error('', message);
            } else if (Code != "901") {
                ShowMessage();
            } else {
                app.security.Logout(app.user.userId, true);
            }
        } else {
            notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { window.location.reload(true); });
        }
    };

    this.TranslateInit = function (name, callback, path) {
        //Translate Configuration Start
        var urlResource = "";
        if (path !== null && path !== undefined) {
            urlResource = "/fasi/" + path + '/locales/__lng__.' + name + '.json';
        } else {
            urlResource = location.href.substring(0, location.href.lastIndexOf("/") + 1) + 'locales/__lng__.' + name + '.json';
        }
        $.i18n.init({
            resGetPath: urlResource,
            load: 'unspecific',
            fallbackLng: false,
            lng: generalSupport.LanguageName() ? generalSupport.LanguageName() : constants.defaultLanguageName
        }, function (t) {
            $('#app').i18n();
            callback.call();
        });
    };

    this.GetCurrentName = function () {
        var url = window.location.pathname;
        var filename = url.substring(url.lastIndexOf('/') + 1);
        filename = filename.replace(".aspx", "");
        filename = filename.replace(".html", "");
        filename = filename.replace("Popup", "");
        return filename;
    };

    this.CallBackOfficePage = function (codispl, parameters) {
        $.ajax({
            type: 'POST',
            url: '/fasi/wmethods/backoffice.aspx/MakeURL',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            data: JSON.stringify({
                codispl: codispl
            }),
            success: function (data) {
                var win = open(data.d + parameters, '', 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                if (win !== null) {
                    win.moveTo(0, 0);
                    win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
    };

    this.DatePickerValue = function (id) {
        return $(id).val() !== '' ? moment($(id).val(), generalSupport.DateFormat()) : moment('0001-01-01').format(generalSupport.DateFormat());
    };

    /**
    * Returns the date on UTC and with the given format
    * @param {any} value Object to normalize
    */
    this.DatePickerValueWithValue = function (value) {
        if (value && value.toString().indexOf("Date(") > -1)
            value = generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat());
        return value !== '' ? moment(value, generalSupport.DateFormat()).format('YYYY-MM-DDT00:00:00Z') : moment('0001-01-01').format('YYYY-MM-DDT00:00:00Z');
    };

    this.DatePickerValueInputToObject = function (id, withTime) {
        var result = $(id).val() !== '' ? moment($(id).val(), generalSupport.DateFormat() + 'THH:mm:ss') : moment('0001-01-01');
        var format = 'YYYY-MM-DD';
        if (withTime)
            format = 'YYYY-MM-DDTHH:mm:ssZ';
        return result.format(format);
    };

    this.CreateVTTimeZone = function () {
        if (typeof moment != "undefined" && typeof app.user.timeZoneVT != "undefined") {
            moment.tz.add(moment.tz.pack(app.user.timeZoneVT));
            moment.tz.setDefault("VisualTimeTZ");
        }
    };

    this.DateFormat = function () {
        var result = 'DD/MM/YYYY';
        if (localStorage.getItem('languageName') !== null)
            if (localStorage.getItem('languageName').toLowerCase() === 'en')
                result = 'MM/DD/YYYY';
        return result;
    };

    /**
     * Método que permite darle formato de fecha a un input datepicker incluyendo la hora en el formato.
     * @param {any} id Identificador del input datepicker que va a ser tratado.
     */
    this.DatePickerValueWithHour = function (id) {
        return $(id).val() !== '' ? moment($(id).val(), generalSupport.DateFormatWithHour()) : moment(new Date('0001-01-01T00:00:00Z')).utc();
    };

    /**
     * Método que permite darle formato de fecha a un input datepicker incluyendo la hora en el formato.
     * @param {any} id Identificador del input datepicker que va a ser tratado.
     */
    this.DatePickerWithHourValueInputToObject = function (id) {
        return generalSupport.DatePickerValueWithHour(id).format('YYYY-MM-DDTHH:mm:00');
    };

    /**
     * Método que permite obtener el formato de fecha incluida la hora dependiendo del idioma en el que se encuentra trabajando la aplicación.
     */
    this.DateFormatWithHour = function () {
        var result = 'DD/MM/YYYY HH:mm';
        if (localStorage.getItem('languageName') !== null)
            if (localStorage.getItem('languageName').toLowerCase() === 'en')
                result = 'MM/DD/YYYY HH:mm';
        return result;
    };

    this.DecimalCharacter = function () {
        var result = ',';
        if (localStorage.getItem('languageName') !== null)
            if (localStorage.getItem('languageName').toLowerCase() === 'en' || localStorage.getItem('languageName').toLowerCase() === 'es-mx')
                result = '.';
        return result;
    };

    this.DigitGroupSeparator = function () {
        var result = '.';
        if (localStorage.getItem('languageName') !== null)
            if (localStorage.getItem('languageName').toLowerCase() === 'en' || localStorage.getItem('languageName').toLowerCase() === 'es-mx')
                result = ',';
        return result;
    };

    this.settingsSet = function (settings) {
        localStorage.setItem('settings', JSON.stringify(settings));
    }

    this.settings = function () {
        return JSON.parse(localStorage.getItem('settings'));
    }

    this.LanguageName = function () {
        var result = "";
        if (localStorage.getItem('languageName') !== null) {
            if (localStorage.getItem('languageName').toLowerCase().indexOf("-") !== -1) {
                result = localStorage.getItem('languageName').split('-')[0];
            }
            else {
                result = localStorage.getItem('languageName');
            }
        } else {
            result = constants.defaultLanguageName;
        }

        return result.toLowerCase();
    };

    this.LanguageNameSet = function (languageName) {
        localStorage.setItem('languageName', languageName);
    };

    this.LanguageIdSet = function (languageId) {
        localStorage.setItem('languageId', languageId);
    };

    this.LanguageId = function () {
        if (localStorage.getItem('languageId') !== null) {
            return localStorage.getItem('languageId');
        } else {
            return constants.defaultLanguageId;
        }
    };

    this.LanguageSet = function (languageId, languageName) {
        generalSupport.LanguageIdSet(languageId);
        generalSupport.LanguageNameSet(languageName);
        if (app.user) {
            app.user.languageID = languageId;
            app.user.languageName = languageName;
        }
    };

    this.LanguageSynchronization = function (languageId, languageName) {
        var result = "";
        $.ajax({
            type: 'POST',
            url: '/fasi/wmethods/User.aspx/LanguageSynchronization',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            data: JSON.stringify({
                languageId: languageId,
                languageName: languageName
            }),
            success: function (data) {
                result = data;
                generalSupport.LanguageSet(data.d.LanguageId, data.d.LanguageName);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    this.LanguageByCultureName = function (languageName) {
        var result = "";
        $.ajax({
            type: 'POST',
            url: '/fasi/wmethods/User.aspx/LanguageByCultureName',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            data: JSON.stringify({
                languageName: languageName
            }),
            success: function (data) {
                if (data.d) {
                    generalSupport.LanguageIdSet(parseInt(data.d.Code));
                    generalSupport.LanguageNameSet(data.d.CultureCode);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
        return result;
    };

    /**
     * Normalize the json dates of the given object. Returns then in UTC
     * @param {any} objectToExamine Object to normalize
     */
    this.NormalizeDatesInObject = function (objectToExamine) {
        for (var propertyToExamine in objectToExamine) {
            if ((objectToExamine[propertyToExamine] !== null) && (objectToExamine[propertyToExamine] !== undefined) && (objectToExamine[propertyToExamine].toString().indexOf("Date") !== -1)) {
                if (objectToExamine[propertyToExamine].toString().indexOf("Date(-") !== -1) {
                    objectToExamine[propertyToExamine] = "0001-01-01T00:00:00" + moment().format('Z');
                } else {
                    //objectToExamine[propertyToExamine] = generalSupport.ToJavaScriptDateCustom(objectToExamine[propertyToExamine], generalSupport.DateFormat());
                    objectToExamine[propertyToExamine] = generalSupport.DatePickerValueWithValue(generalSupport.ToJavaScriptDateCustom(objectToExamine[propertyToExamine], generalSupport.DateFormat()));
                }
            }
        }
        return objectToExamine;
    };

    /**
     * Método que permite achatar el excepciones no controladas.
     * @param {any} errMsg Mensage del error
     * @param {any} url Url de origen
     * @param {any} line Linea de origen
     * @param {any} column Columna de origen
     * @param {any} error Objeto error.
     */
    this.OnError = function (errMsg, url, line, column, error) {
        var objectTrace = {
            Message: errMsg,
            Url: url,
            Line: line,
            Column: column,
            Error: error
        };
        if (url.indexOf("fasi/app/js") !== -1 || url.indexOf("fasi/dli") !== -1) {
            console.error("Uncontrolled error in fasi", objectTrace);
            var key = "";
            try {
                var urlTrace = '/fasi/wmethods/General.aspx/Error';
                $.ajax({
                    type: "POST",
                    async: false,
                    url: urlTrace,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        Message: objectTrace.Message,
                        Url: objectTrace.Url,
                        Line: objectTrace.Line,
                        Column: objectTrace.Column
                    }),
                    dataType: "json",
                    success: function (data) {
                        key = data.d;
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            } catch (e) {
                console.error("Uncontrolled error in fasi", "Error al realizar trace");
            }
            var item = this.ResourceByKey("ShowDetail");
            item = item.replace("@@key@@", key);
            swal({
                title: this.ResourceByKey("UnexpectedError"),
                html: true,
                text: item,
                type: "error"
            });
        } else {
            console.warn("Uncontrolled error in fasi", objectTrace);
        }
        return true;
    };

    /**
     * Orders the keys in the object
     * @param {any} unordered Unordered object.
     */
    this.OrderKeysOnObject = function (unordered) {
        var ordered = {};
        Object.keys(unordered).sort().forEach(function (key) {
            ordered[key] = unordered[key];
        });

        return ordered;
    };

    /**
     *
     * @param {any} name Nombre del control
     * @param {any} defaultValue Valor de default del select
     * @param {any} source Source que solicita la carga de items
     * @param {any} items Items a popular el select
     * @param {any} isSelect2 Define si es convertible a plugin 'Selected2'
     */
    this.RenderLookUp = function (name, defaultValue, source, items, isSelect2) {
        if (isSelect2 == null) {
            isSelect2 = false;
        }
        var ctrol = $('#' + name);
        if (ctrol.length > 0) {
            if (ctrol.children().length !== 0)
                ctrol.children().remove();
            items = generalSupport.LookUpConvertArray(items, 'Code', 'Description');
            $.each(items, function () {
                ctrol.append($('<option />').val(this['id']).text(this['text']));
            });
            if (defaultValue !== null)
                ctrol.val(defaultValue);
            else
                ctrol.val(0);

            if (isSelect2) {
                ctrol.select2({
                    data: items,
                    multiple: true,
                    allowClear: true,
                    width: '100%'
                });
            }
        } else {
            ctrol = $('#' + name + "_Dynamic");
            if (ctrol.length > 0) {
                ctrol.children().remove();
                items = generalSupport.LookUpConvertArray(items, 'Code', 'Description');
                $.each(items, function () {
                    ctrol.append("<div class='radio'><label><input type='radio' name='" + name + "' id='" + name + "_" + this['Code'] + "' value='" + this['Code'] + "'/>" + this['Description'] + "</label></div>");
                });

                if (isSelect2) {
                    ctrol.select2({
                        sdata: items,
                        multiple: true,
                        allowClear: true,
                        width: '100%'
                    });
                }

                if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                    if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                        $($("input:radio[name=" + name + "][value=' + defaultValue + ']")).prop('checked', true);
                    }
            }
        }
    };

    this.SetCalendarPosition = function (calendarSelector) {
        $(calendarSelector).on('dp.show', function () {
            if ($(this).parents(".ibox-content").length > 0) {
                var datePickerChild = $(this).find(".bootstrap-datetimepicker-widget");
                datePickerChild.css({ top: datePickerChild.offset().top, left: datePickerChild.offset().left, height: datePickerChild.outerHeight() });
                datePickerChild.appendTo("body");
                $(this).parents(".ibox-content").css({ overflow: "hidden" });
            }
        }).on('dp.hide', function () {
            if ($(this).parents(".ibox-content").length > 0)
                $(this).parents(".ibox-content").css({ overflow: "" });
        });
    };

    /**
     * Recupera el valor seleccionado de un radiobutton, en caso de no haber nada seleccionado devuelve cero.
     * @param {string} name Nombre del control
     * @return {number} valor seleccionado
     */
    this.RadioNumericValue = function (name) {
        var value = $('input:radio[name=' + name + ']:checked').val();
        if (typeof value === 'undefined' || value === null)
            value = 0;
        return parseInt(0 + value, 10);
    };

    this.CreateNewModal = function (options) {
        //Size to css
        options.cssWidth = options.width > 0 ? { "min-width": (options.width * 0.96) + "%" } : {};
        options.cssHeight = options.height > 0 ? { "height": (options.height) + "%" } : {};

        //Create the modal structure
        var $newModal = $("<div />").attr({ "role": "dialog" }).addClass("modal fade dynamicModal");
        var $modalDialog = $("<div />").attr({ "role": "document" }).addClass("modal-dialog");
        var $modalContent = $("<div />").addClass("modal-content");
        var $modalHeader = $("<div />").addClass("modal-header").html('<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>');
        var $modalBody = $("<div />").addClass("modal-body clearfix");

        //Apply the custom options
        $modalDialog.css(options.cssWidth);
        $modalContent.css(options.cssHeight);
        $modalHeader.append(typeof options.title != "undefined" ? "<h4>" + options.title + "</h4>" : "");

        //Build the modal
        $modalContent.append($modalHeader).append($modalBody);
        $modalDialog.append($modalContent);
        $newModal.append($modalDialog);

        //load teh content
        if (options.isExternal) {
            $modalContent.addClass(options.height > 0 ? "" : "default-size");
            $modalBody.html($("<iframe />").attr({ frameBorder: 0, src: options.url }));
        }
        else {
            $internalContent = $("<div />").addClass("internalContent").load(options.url);
            $internalContent.addClass(options.height > 0 ? "overflow-auto" : "");
            $modalBody.append($internalContent);
        }

        //Display the modal
        $newModal.modal("show");
        $newModal.on('hidden.bs.modal', function () { $newModal.remove() });
    };

    this.CreateNewPopover = function (options, element) {
        //Check that the popover is not on the screen
        if ($("#" + $(element).attr("aria-describedby")).length == 0) {
            //Size to css
            options.cssWidth = options.width > 0 ? { "min-width": options.width } : {};
            options.cssHeight = options.height > 0 ? { "height": options.height } : {};
            options.title = typeof options.title != "undefined" ? options.title : " ";

            if (!options.isExternal)
                $.extend(options.cssHeight, { overflow: "auto" });

            //Create the Popover structure
            var $newPopover = $("<div />").attr({ "role": "tooltip" }).addClass("popover dynamicPopover").css(options.cssWidth).html('<div class="arrow"></div>');
            var $popTitle = $("<h3 />").addClass("popover-title").text(options.title);
            var $popContent = $("<div />").addClass("popover-content").css(options.cssHeight);

            //Build the Popover html
            $newPopover.append($popTitle).append($popContent);

            $(element).attr({
                'data-toggle': "popover",
                'title': options.title,
                'data-content': ""
            });

            $(element).popover({
                template: $newPopover[0].outerHTML
            });

            //Build the Popover
            $(element).popover("show");

            //load teh content
            var popOverInstance = $("#" + $(element).attr("aria-describedby"));
            if (options.isExternal)
                popOverInstance.find(".popover-content").append($("<iframe />").attr({ frameBorder: 0, src: options.url }));
            else
                popOverInstance.find(".popover-content").load(options.url);
        }
    };

    this.ShowOffCanvas = function (options) {
        var $offCanvas;
        var $offCanvasHeader = $("<div />").attr({ id: "off-canvas-header" }).addClass("border-bottom");

        if ($("#off-canvas").length > 0) {
            $offCanvas = $("#off-canvas");
            $offCanvas.empty();
        }
        else
            $offCanvas = $("<div />").attr({ id: "off-canvas" }).addClass("col-sm-3 col-xs-10");

        $offCanvasHeader.append('<button type="button" class="close" id="closeoffCanvas" aria-hidden="true">×</button>');
        if (typeof options.title != "undefined")
            $offCanvasHeader.append('<h2><i class="fa fa-info-circle"></i>' + options.title + '</h2>');

        $offCanvas.append($offCanvasHeader);

        $offCanvas.appendTo("body").show("slide", { direction: "right" }, 300, function () {
            if (options.isExternal)
                $offCanvas.append($("<iframe />").attr({ frameBorder: 0, src: options.url }));
            else
                $offCanvas.append($("<div/>").attr({ id: "off-canvas-content" }).load(options.url));
        });

        $offCanvasHeader.find("#closeoffCanvas").click(function (event) {
            event.preventDefault();
            $offCanvas.hide("slide", { direction: "right" }, 300, function () {
                $offCanvas.remove();
            });
        });
    };

    /**
     * Método que permite hacer un render de un template y dicho valor asignar a un control HTML.
     * @param {any} options Opciones que permite el renda rizado de plantilla
     *                      1. name: define cual es la librería que se va utilizar la rendarizado
     *                      2. context: define la fuente de datos para el remplazo del rendarizado
     *                      3. template: define cual es el cuerpo de la plantilla a utilizar en el rendarizado
     *                      4. element: define sobre cual elemento HTML se va asignar o hacer append del resultado del rendarizado.
     *                      5. IsReplacement: define si se hace un append al HTML del control o se hace un remplazo del body del control a asignar el rendarizado
     */
    this.Render = function (options) {
        var result = RenderBody(options);
        if (!options.IsReplacement) {
            options.IsReplacement = true;
        }
        if (options.IsReplacement) {
            $('#' + options.element).html(result);
        } else {
            $('#' + options.element).append(result);
        }
    };

    /**
     * Método que permite hacer un render de un template y el retorno del body
     * @param {any} options Opciones que permite el renda rizado de plantilla
     *                      1. name: define cual es la librería que se va utilizar la rendarizado
     *                      2. context: define la fuente de datos para el remplazo del rendarizado
     *                      3. template: define cual es el cuerpo de la plantilla a utilizar en el rendarizado
     * @return {result} retorna el valor del rendarizado
     */
    this.RenderBody = function (options) {
        var result = "";
        switch (options.name) {
            case "Template7":
                var compiledTemplate = Template7.compile(options.template);
                result = compiledTemplate(options.context);
                break;
            default:
                break;
        }
        return result;
    };
};

$(function ($) {
    window.onerror = function (errMsg, url, line, column, error) {
        return generalSupport.OnError(errMsg, url, line, column, error);
    };
});
var ajaxJsonHelper = new function () {
    this.ajax = function (url, type, data, success, error, complete, async) {
        // Si no está definido la function de error, entonces le agrega una generica
        if (!error) {
            error = function (jqXHR, textStatus, errorThrown) {
                // Si el error es de no autenticado
                $.LoadingOverlay("hide");
                if (jqXHR.status == 401)
                    notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { securitySupport.Logout(masterSupport.user.userId, true); });
                else if (jqXHR.status != 200)
                    generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            };
        }

        if (async == undefined) {
            async = true;
        }

        $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: async,
            cache: false,
            data: data,
            success: success,
            error: error,
            complete: complete,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + app.security.Token());
            }
        });
    };

    this.get = function (url, data, success, error, complete) {
        this.ajax(url, 'GET', data, success, error, complete);
    };

    this.post = function (url, data, success, error, complete, async) {
        this.ajax(url, 'POST', data, success, error, complete, async);
    };

    this.put = function (url, data, success, error, complete) {
        this.ajax(url, 'PUT', data, success, error, complete);
    };

    this.delete = function (url, data, success, error, complete) {
        this.ajax(url, 'DELETE', data, success, error, complete);
    };
};

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
!function(e,t,n){"use strict";!function o(e,t,n){function a(s,l){if(!t[s]){if(!e[s]){var i="function"==typeof require&&require;if(!l&&i)return i(s,!0);if(r)return r(s,!0);var u=new Error("Cannot find module '"+s+"'");throw u.code="MODULE_NOT_FOUND",u}var c=t[s]={exports:{}};e[s][0].call(c.exports,function(t){var n=e[s][1][t];return a(n?n:t)},c,c.exports,o,e,t,n)}return t[s].exports}for(var r="function"==typeof require&&require,s=0;s<n.length;s++)a(n[s]);return a}({1:[function(o){var a,r,s,l,i=function(e){return e&&e.__esModule?e:{"default":e}},u=o("./modules/handle-dom"),c=o("./modules/utils"),d=o("./modules/handle-swal-dom"),f=o("./modules/handle-click"),p=o("./modules/handle-key"),m=i(p),v=o("./modules/default-params"),y=i(v),h=o("./modules/set-params"),g=i(h);s=l=function(){function o(e){var t=s;return t[e]===n?y["default"][e]:t[e]}var s=arguments[0];if(u.addClass(t.body,"stop-scrolling"),d.resetInput(),s===n)return c.logStr("SweetAlert expects at least 1 attribute!"),!1;var l=c.extend({},y["default"]);switch(typeof s){case"string":l.title=s,l.text=arguments[1]||"",l.type=arguments[2]||"";break;case"object":if(s.title===n)return c.logStr('Missing "title" argument!'),!1;l.title=s.title;for(var i in y["default"])l[i]=o(i);l.confirmButtonText=l.showCancelButton?"Confirm":y["default"].confirmButtonText,l.confirmButtonText=o("confirmButtonText"),l.doneFunction=arguments[1]||null;break;default:return c.logStr('Unexpected type of argument! Expected "string" or "object", got '+typeof s),!1}g["default"](l),d.fixVerticalPosition(),d.openModal(arguments[1]);for(var p=d.getModal(),v=p.querySelectorAll("button"),h=["onclick","onmouseover","onmouseout","onmousedown","onmouseup","onfocus"],b=function(e){return f.handleButton(e,l,p)},w=0;w<v.length;w++)for(var C=0;C<h.length;C++){var S=h[C];v[w][S]=b}d.getOverlay().onclick=b,a=e.onkeydown;var x=function(e){return m["default"](e,l,p)};e.onkeydown=x,e.onfocus=function(){setTimeout(function(){r!==n&&(r.focus(),r=n)},0)}},s.setDefaults=l.setDefaults=function(e){if(!e)throw new Error("userParams is required");if("object"!=typeof e)throw new Error("userParams has to be a object");c.extend(y["default"],e)},s.close=l.close=function(){var o=d.getModal();u.fadeOut(d.getOverlay(),5),u.fadeOut(o,5),u.removeClass(o,"showSweetAlert"),u.addClass(o,"hideSweetAlert"),u.removeClass(o,"visible");var s=o.querySelector(".sa-icon.sa-success");u.removeClass(s,"animate"),u.removeClass(s.querySelector(".sa-tip"),"animateSuccessTip"),u.removeClass(s.querySelector(".sa-long"),"animateSuccessLong");var l=o.querySelector(".sa-icon.sa-error");u.removeClass(l,"animateErrorIcon"),u.removeClass(l.querySelector(".sa-x-mark"),"animateXMark");var i=o.querySelector(".sa-icon.sa-warning");return u.removeClass(i,"pulseWarning"),u.removeClass(i.querySelector(".sa-body"),"pulseWarningIns"),u.removeClass(i.querySelector(".sa-dot"),"pulseWarningIns"),setTimeout(function(){var e=o.getAttribute("data-custom-class");u.removeClass(o,e)},300),u.removeClass(t.body,"stop-scrolling"),e.onkeydown=a,e.previousActiveElement&&e.previousActiveElement.focus(),r=n,clearTimeout(o.timeout),!0},s.showInputError=l.showInputError=function(e){var t=d.getModal(),n=t.querySelector(".sa-input-error");u.addClass(n,"show");var o=t.querySelector(".sa-error-container");u.addClass(o,"show"),o.querySelector("p").innerHTML=e,t.querySelector("input").focus()},s.resetInputError=l.resetInputError=function(e){if(e&&13===e.keyCode)return!1;var t=d.getModal(),n=t.querySelector(".sa-input-error");u.removeClass(n,"show");var o=t.querySelector(".sa-error-container");u.removeClass(o,"show")},"undefined"!=typeof e?e.sweetAlert=e.swal=s:c.logStr("SweetAlert is a frontend module!")},{"./modules/default-params":2,"./modules/handle-click":3,"./modules/handle-dom":4,"./modules/handle-key":5,"./modules/handle-swal-dom":6,"./modules/set-params":8,"./modules/utils":9}],2:[function(e,t,n){Object.defineProperty(n,"__esModule",{value:!0});var o={title:"",text:"",type:null,allowOutsideClick:!1,showConfirmButton:!0,showCancelButton:!1,closeOnConfirm:!0,closeOnCancel:!0,confirmButtonText:"OK",confirmButtonColor:"#AEDEF4",cancelButtonText:"Cancel",imageUrl:null,imageSize:null,timer:null,customClass:"",html:!1,animation:!0,allowEscapeKey:!0,inputType:"text",inputPlaceholder:"",inputValue:""};n["default"]=o,t.exports=n["default"]},{}],3:[function(t,n,o){Object.defineProperty(o,"__esModule",{value:!0});var a=t("./utils"),r=(t("./handle-swal-dom"),t("./handle-dom")),s=function(t,n,o){function s(e){m&&n.confirmButtonColor&&(p.style.backgroundColor=e)}var u,c,d,f=t||e.event,p=f.target||f.srcElement,m=-1!==p.className.indexOf("confirm"),v=-1!==p.className.indexOf("sweet-overlay"),y=r.hasClass(o,"visible"),h=n.doneFunction&&"true"===o.getAttribute("data-has-done-function");switch(m&&n.confirmButtonColor&&(u=n.confirmButtonColor,c=a.colorLuminance(u,-.04),d=a.colorLuminance(u,-.14)),f.type){case"mouseover":s(c);break;case"mouseout":s(u);break;case"mousedown":s(d);break;case"mouseup":s(c);break;case"focus":var g=o.querySelector("button.confirm"),b=o.querySelector("button.cancel");m?b.style.boxShadow="none":g.style.boxShadow="none";break;case"click":var w=o===p,C=r.isDescendant(o,p);if(!w&&!C&&y&&!n.allowOutsideClick)break;m&&h&&y?l(o,n):h&&y||v?i(o,n):r.isDescendant(o,p)&&"BUTTON"===p.tagName&&sweetAlert.close()}},l=function(e,t){var n=!0;r.hasClass(e,"show-input")&&(n=e.querySelector("input").value,n||(n="")),t.doneFunction(n),t.closeOnConfirm&&sweetAlert.close()},i=function(e,t){var n=String(t.doneFunction).replace(/\s/g,""),o="function("===n.substring(0,9)&&")"!==n.substring(9,10);o&&t.doneFunction(!1),t.closeOnCancel&&sweetAlert.close()};o["default"]={handleButton:s,handleConfirm:l,handleCancel:i},n.exports=o["default"]},{"./handle-dom":4,"./handle-swal-dom":6,"./utils":9}],4:[function(n,o,a){Object.defineProperty(a,"__esModule",{value:!0});var r=function(e,t){return new RegExp(" "+t+" ").test(" "+e.className+" ")},s=function(e,t){r(e,t)||(e.className+=" "+t)},l=function(e,t){var n=" "+e.className.replace(/[\t\r\n]/g," ")+" ";if(r(e,t)){for(;n.indexOf(" "+t+" ")>=0;)n=n.replace(" "+t+" "," ");e.className=n.replace(/^\s+|\s+$/g,"")}},i=function(e){var n=t.createElement("div");return n.appendChild(t.createTextNode(e)),n.innerHTML},u=function(e){e.style.opacity="",e.style.display="block"},c=function(e){if(e&&!e.length)return u(e);for(var t=0;t<e.length;++t)u(e[t])},d=function(e){e.style.opacity="",e.style.display="none"},f=function(e){if(e&&!e.length)return d(e);for(var t=0;t<e.length;++t)d(e[t])},p=function(e,t){for(var n=t.parentNode;null!==n;){if(n===e)return!0;n=n.parentNode}return!1},m=function(e){e.style.left="-9999px",e.style.display="block";var t,n=e.clientHeight;return t="undefined"!=typeof getComputedStyle?parseInt(getComputedStyle(e).getPropertyValue("padding-top"),10):parseInt(e.currentStyle.padding),e.style.left="",e.style.display="none","-"+parseInt((n+t)/2)+"px"},v=function(e,t){if(+e.style.opacity<1){t=t||16,e.style.opacity=0,e.style.display="block";var n=+new Date,o=function(e){function t(){return e.apply(this,arguments)}return t.toString=function(){return e.toString()},t}(function(){e.style.opacity=+e.style.opacity+(new Date-n)/100,n=+new Date,+e.style.opacity<1&&setTimeout(o,t)});o()}e.style.display="block"},y=function(e,t){t=t||16,e.style.opacity=1;var n=+new Date,o=function(e){function t(){return e.apply(this,arguments)}return t.toString=function(){return e.toString()},t}(function(){e.style.opacity=+e.style.opacity-(new Date-n)/100,n=+new Date,+e.style.opacity>0?setTimeout(o,t):e.style.display="none"});o()},h=function(n){if("function"==typeof MouseEvent){var o=new MouseEvent("click",{view:e,bubbles:!1,cancelable:!0});n.dispatchEvent(o)}else if(t.createEvent){var a=t.createEvent("MouseEvents");a.initEvent("click",!1,!1),n.dispatchEvent(a)}else t.createEventObject?n.fireEvent("onclick"):"function"==typeof n.onclick&&n.onclick()},g=function(t){"function"==typeof t.stopPropagation?(t.stopPropagation(),t.preventDefault()):e.event&&e.event.hasOwnProperty("cancelBubble")&&(e.event.cancelBubble=!0)};a.hasClass=r,a.addClass=s,a.removeClass=l,a.escapeHtml=i,a._show=u,a.show=c,a._hide=d,a.hide=f,a.isDescendant=p,a.getTopMargin=m,a.fadeIn=v,a.fadeOut=y,a.fireClick=h,a.stopEventPropagation=g},{}],5:[function(t,o,a){Object.defineProperty(a,"__esModule",{value:!0});var r=t("./handle-dom"),s=t("./handle-swal-dom"),l=function(t,o,a){var l=t||e.event,i=l.keyCode||l.which,u=a.querySelector("button.confirm"),c=a.querySelector("button.cancel"),d=a.querySelectorAll("button[tabindex]");if(-1!==[9,13,32,27].indexOf(i)){for(var f=l.target||l.srcElement,p=-1,m=0;m<d.length;m++)if(f===d[m]){p=m;break}9===i?(f=-1===p?u:p===d.length-1?d[0]:d[p+1],r.stopEventPropagation(l),f.focus(),o.confirmButtonColor&&s.setFocusStyle(f,o.confirmButtonColor)):13===i?("INPUT"===f.tagName&&(f=u,u.focus()),f=-1===p?u:n):27===i&&o.allowEscapeKey===!0?(f=c,r.fireClick(f,l)):f=n}};a["default"]=l,o.exports=a["default"]},{"./handle-dom":4,"./handle-swal-dom":6}],6:[function(n,o,a){var r=function(e){return e&&e.__esModule?e:{"default":e}};Object.defineProperty(a,"__esModule",{value:!0});var s=n("./utils"),l=n("./handle-dom"),i=n("./default-params"),u=r(i),c=n("./injected-html"),d=r(c),f=".sweet-alert",p=".sweet-overlay",m=function(){var e=t.createElement("div");for(e.innerHTML=d["default"];e.firstChild;)t.body.appendChild(e.firstChild)},v=function(e){function t(){return e.apply(this,arguments)}return t.toString=function(){return e.toString()},t}(function(){var e=t.querySelector(f);return e||(m(),e=v()),e}),y=function(){var e=v();return e?e.querySelector("input"):void 0},h=function(){return t.querySelector(p)},g=function(e,t){var n=s.hexToRgb(t);e.style.boxShadow="0 0 2px rgba("+n+", 0.8), inset 0 0 0 1px rgba(0, 0, 0, 0.05)"},b=function(n){var o=v();l.fadeIn(h(),10),l.show(o),l.addClass(o,"showSweetAlert"),l.removeClass(o,"hideSweetAlert"),e.previousActiveElement=t.activeElement;var a=o.querySelector("button.confirm");a.focus(),setTimeout(function(){l.addClass(o,"visible")},500);var r=o.getAttribute("data-timer");if("null"!==r&&""!==r){var s=n;o.timeout=setTimeout(function(){var e=(s||null)&&"true"===o.getAttribute("data-has-done-function");e?s(null):sweetAlert.close()},r)}},w=function(){var e=v(),t=y();l.removeClass(e,"show-input"),t.value=u["default"].inputValue,t.setAttribute("type",u["default"].inputType),t.setAttribute("placeholder",u["default"].inputPlaceholder),C()},C=function(e){if(e&&13===e.keyCode)return!1;var t=v(),n=t.querySelector(".sa-input-error");l.removeClass(n,"show");var o=t.querySelector(".sa-error-container");l.removeClass(o,"show")},S=function(){var e=v();e.style.marginTop=l.getTopMargin(v())};a.sweetAlertInitialize=m,a.getModal=v,a.getOverlay=h,a.getInput=y,a.setFocusStyle=g,a.openModal=b,a.resetInput=w,a.resetInputError=C,a.fixVerticalPosition=S},{"./default-params":2,"./handle-dom":4,"./injected-html":7,"./utils":9}],7:[function(e,t,n){Object.defineProperty(n,"__esModule",{value:!0});var o='<div class="sweet-overlay" tabIndex="-1"></div><div class="sweet-alert"><div class="sa-icon sa-error">\n      <span class="sa-x-mark">\n        <span class="sa-line sa-left"></span>\n        <span class="sa-line sa-right"></span>\n      </span>\n    </div><div class="sa-icon sa-warning">\n      <span class="sa-body"></span>\n      <span class="sa-dot"></span>\n    </div><div class="sa-icon sa-info"></div><div class="sa-icon sa-success">\n      <span class="sa-line sa-tip"></span>\n      <span class="sa-line sa-long"></span>\n\n      <div class="sa-placeholder"></div>\n      <div class="sa-fix"></div>\n    </div><div class="sa-icon sa-custom"></div><h2>Title</h2>\n    <p>Text</p>\n    <fieldset>\n      <input type="text" tabIndex="3" />\n      <div class="sa-input-error"></div>\n    </fieldset><div class="sa-error-container">\n      <div class="icon">!</div>\n      <p>Not valid!</p>\n    </div><div class="sa-button-container">\n      <button class="cancel" tabIndex="2">Cancel</button>\n      <button class="confirm" tabIndex="1">OK</button>\n    </div></div>';n["default"]=o,t.exports=n["default"]},{}],8:[function(e,t,o){Object.defineProperty(o,"__esModule",{value:!0});var a=e("./utils"),r=e("./handle-swal-dom"),s=e("./handle-dom"),l=["error","warning","info","success","input","prompt"],i=function(e){var t=r.getModal(),o=t.querySelector("h2"),i=t.querySelector("p"),u=t.querySelector("button.cancel"),c=t.querySelector("button.confirm");if(o.innerHTML=e.html?e.title:s.escapeHtml(e.title).split("\n").join("<br>"),i.innerHTML=e.html?e.text:s.escapeHtml(e.text||"").split("\n").join("<br>"),e.text&&s.show(i),e.customClass)s.addClass(t,e.customClass),t.setAttribute("data-custom-class",e.customClass);else{var d=t.getAttribute("data-custom-class");s.removeClass(t,d),t.setAttribute("data-custom-class","")}if(s.hide(t.querySelectorAll(".sa-icon")),e.type&&!a.isIE8()){var f=function(){for(var o=!1,a=0;a<l.length;a++)if(e.type===l[a]){o=!0;break}if(!o)return logStr("Unknown alert type: "+e.type),{v:!1};var i=["success","error","warning","info"],u=n;-1!==i.indexOf(e.type)&&(u=t.querySelector(".sa-icon.sa-"+e.type),s.show(u));var c=r.getInput();switch(e.type){case"success":s.addClass(u,"animate"),s.addClass(u.querySelector(".sa-tip"),"animateSuccessTip"),s.addClass(u.querySelector(".sa-long"),"animateSuccessLong");break;case"error":s.addClass(u,"animateErrorIcon"),s.addClass(u.querySelector(".sa-x-mark"),"animateXMark");break;case"warning":s.addClass(u,"pulseWarning"),s.addClass(u.querySelector(".sa-body"),"pulseWarningIns"),s.addClass(u.querySelector(".sa-dot"),"pulseWarningIns");break;case"input":case"prompt":c.setAttribute("type",e.inputType),c.value=e.inputValue,c.setAttribute("placeholder",e.inputPlaceholder),s.addClass(t,"show-input"),setTimeout(function(){c.focus(),c.addEventListener("keyup",swal.resetInputError)},400)}}();if("object"==typeof f)return f.v}if(e.imageUrl){var p=t.querySelector(".sa-icon.sa-custom");p.style.backgroundImage="url("+e.imageUrl+")",s.show(p);var m=80,v=80;if(e.imageSize){var y=e.imageSize.toString().split("x"),h=y[0],g=y[1];h&&g?(m=h,v=g):logStr("Parameter imageSize expects value with format WIDTHxHEIGHT, got "+e.imageSize)}p.setAttribute("style",p.getAttribute("style")+"width:"+m+"px; height:"+v+"px")}t.setAttribute("data-has-cancel-button",e.showCancelButton),e.showCancelButton?u.style.display="inline-block":s.hide(u),t.setAttribute("data-has-confirm-button",e.showConfirmButton),e.showConfirmButton?c.style.display="inline-block":s.hide(c),e.cancelButtonText&&(u.innerHTML=s.escapeHtml(e.cancelButtonText)),e.confirmButtonText&&(c.innerHTML=s.escapeHtml(e.confirmButtonText)),e.confirmButtonColor&&(c.style.backgroundColor=e.confirmButtonColor,r.setFocusStyle(c,e.confirmButtonColor)),t.setAttribute("data-allow-outside-click",e.allowOutsideClick);var b=e.doneFunction?!0:!1;t.setAttribute("data-has-done-function",b),e.animation?"string"==typeof e.animation?t.setAttribute("data-animation",e.animation):t.setAttribute("data-animation","pop"):t.setAttribute("data-animation","none"),t.setAttribute("data-timer",e.timer)};o["default"]=i,t.exports=o["default"]},{"./handle-dom":4,"./handle-swal-dom":6,"./utils":9}],9:[function(t,n,o){Object.defineProperty(o,"__esModule",{value:!0});var a=function(e,t){for(var n in t)t.hasOwnProperty(n)&&(e[n]=t[n]);return e},r=function(e){var t=/^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(e);return t?parseInt(t[1],16)+", "+parseInt(t[2],16)+", "+parseInt(t[3],16):null},s=function(){return e.attachEvent&&!e.addEventListener},l=function(t){e.console&&e.console.log("SweetAlert: "+t)},i=function(e,t){e=String(e).replace(/[^0-9a-f]/gi,""),e.length<6&&(e=e[0]+e[0]+e[1]+e[1]+e[2]+e[2]),t=t||0;var n,o,a="#";for(o=0;3>o;o++)n=parseInt(e.substr(2*o,2),16),n=Math.round(Math.min(Math.max(0,n+n*t),255)).toString(16),a+=("00"+n).substr(n.length);return a};o.extend=a,o.hexToRgb=r,o.isIE8=s,o.logStr=l,o.colorLuminance=i},{}]},{},[1]),"function"==typeof define&&define.amd?define(function(){return sweetAlert}):"undefined"!=typeof module&&module.exports&&(module.exports=sweetAlert)}(window,document);
!function(e){e(["jquery"],function(e){return function(){function t(e,t,n){return f({type:O.error,iconClass:g().iconClasses.error,message:e,optionsOverride:n,title:t})}function n(t,n){return t||(t=g()),v=e("#"+t.containerId),v.length?v:(n&&(v=c(t)),v)}function i(e,t,n){return f({type:O.info,iconClass:g().iconClasses.info,message:e,optionsOverride:n,title:t})}function o(e){w=e}function s(e,t,n){return f({type:O.success,iconClass:g().iconClasses.success,message:e,optionsOverride:n,title:t})}function a(e,t,n){return f({type:O.warning,iconClass:g().iconClasses.warning,message:e,optionsOverride:n,title:t})}function r(e){var t=g();v||n(t),l(e,t)||u(t)}function d(t){var i=g();return v||n(i),t&&0===e(":focus",t).length?void h(t):void(v.children().length&&v.remove())}function u(t){for(var n=v.children(),i=n.length-1;i>=0;i--)l(e(n[i]),t)}function l(t,n){return t&&0===e(":focus",t).length?(t[n.hideMethod]({duration:n.hideDuration,easing:n.hideEasing,complete:function(){h(t)}}),!0):!1}function c(t){return v=e("<div/>").attr("id",t.containerId).addClass(t.positionClass).attr("aria-live","polite").attr("role","alert"),v.appendTo(e(t.target)),v}function p(){return{tapToDismiss:!0,toastClass:"toast",containerId:"toast-container",debug:!1,showMethod:"fadeIn",showDuration:300,showEasing:"swing",onShown:void 0,hideMethod:"fadeOut",hideDuration:1e3,hideEasing:"swing",onHidden:void 0,extendedTimeOut:1e3,iconClasses:{error:"toast-error",info:"toast-info",success:"toast-success",warning:"toast-warning"},iconClass:"toast-info",positionClass:"toast-top-right",timeOut:5e3,titleClass:"toast-title",messageClass:"toast-message",target:"body",closeHtml:'<button type="button">&times;</button>',newestOnTop:!0,preventDuplicates:!1,progressBar:!1}}function m(e){w&&w(e)}function f(t){function i(t){return!e(":focus",l).length||t?(clearTimeout(O.intervalId),l[r.hideMethod]({duration:r.hideDuration,easing:r.hideEasing,complete:function(){h(l),r.onHidden&&"hidden"!==b.state&&r.onHidden(),b.state="hidden",b.endTime=new Date,m(b)}})):void 0}function o(){(r.timeOut>0||r.extendedTimeOut>0)&&(u=setTimeout(i,r.extendedTimeOut),O.maxHideTime=parseFloat(r.extendedTimeOut),O.hideEta=(new Date).getTime()+O.maxHideTime)}function s(){clearTimeout(u),O.hideEta=0,l.stop(!0,!0)[r.showMethod]({duration:r.showDuration,easing:r.showEasing})}function a(){var e=(O.hideEta-(new Date).getTime())/O.maxHideTime*100;f.width(e+"%")}var r=g(),d=t.iconClass||r.iconClass;if("undefined"!=typeof t.optionsOverride&&(r=e.extend(r,t.optionsOverride),d=t.optionsOverride.iconClass||d),r.preventDuplicates){if(t.message===C)return;C=t.message}T++,v=n(r,!0);var u=null,l=e("<div/>"),c=e("<div/>"),p=e("<div/>"),f=e("<div/>"),w=e(r.closeHtml),O={intervalId:null,hideEta:null,maxHideTime:null},b={toastId:T,state:"visible",startTime:new Date,options:r,map:t};return t.iconClass&&l.addClass(r.toastClass).addClass(d),t.title&&(c.append(t.title).addClass(r.titleClass),l.append(c)),t.message&&(p.append(t.message).addClass(r.messageClass),l.append(p)),r.closeButton&&(w.addClass("toast-close-button").attr("role","button"),l.prepend(w)),r.progressBar&&(f.addClass("toast-progress"),l.prepend(f)),l.hide(),r.newestOnTop?v.prepend(l):v.append(l),l[r.showMethod]({duration:r.showDuration,easing:r.showEasing,complete:r.onShown}),r.timeOut>0&&(u=setTimeout(i,r.timeOut),O.maxHideTime=parseFloat(r.timeOut),O.hideEta=(new Date).getTime()+O.maxHideTime,r.progressBar&&(O.intervalId=setInterval(a,10))),l.hover(s,o),!r.onclick&&r.tapToDismiss&&l.click(i),r.closeButton&&w&&w.click(function(e){e.stopPropagation?e.stopPropagation():void 0!==e.cancelBubble&&e.cancelBubble!==!0&&(e.cancelBubble=!0),i(!0)}),r.onclick&&l.click(function(){r.onclick(),i()}),m(b),r.debug&&console&&console.log(b),l}function g(){return e.extend({},p(),b.options)}function h(e){v||(v=n()),e.is(":visible")||(e.remove(),e=null,0===v.children().length&&(v.remove(),C=void 0))}var v,w,C,T=0,O={error:"error",info:"info",success:"success",warning:"warning"},b={clear:r,remove:d,error:t,getContainer:n,info:i,options:{},subscribe:o,success:s,version:"2.1.0",warning:a};return b}()})}("function"==typeof define&&define.amd?define:function(e,t){"undefined"!=typeof module&&module.exports?module.exports=t(require("jquery")):window.toastr=t(window.jQuery)});
var notification = {
    swal: {
        success: function (title, message) {
            swal(title, message, "success");
        },
        success: function (title, message, timer, callback) {
            swal({
                title: title,
                text: message,
                type: "success",
                timer: timer
            }, callback);
        },
        info: function (title, message) {
            swal({
                title: title,
                text: message,
                type: "info",
                html: true
            });
        },
        infoCallback: function (title, message, callback) {
            swal({
                title: title,
                text: message,
                type: "info",
                showCancelButton: false,
                confirmButtonText: 'OK',
                closeOnConfirm: true
            }, callback);
        },
        warning: function (title, message) {
            swal(title, message, "warning");
        },
        error: function (title, message) {
            swal(title, message, "error");
        },
        deleteRowConfirmation: function (callback) {
            swal({
                title: dict.DeleteRowConfirmation[generalSupport.LanguageName()],
                text: null,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.CancelNo[generalSupport.LanguageName()],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.DeleteYes[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        },
        deleteConfirmation: function (text, callback) {
            swal({
                title: dict.AreYouSure[generalSupport.LanguageName()],
                text: text,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.Cancel[generalSupport.LanguageName()],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.Delete[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        },
        continueConfirmation: function (title, message, callback) {
            swal({
                title: title,
                text: message,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.No[generalSupport.LanguageName()],
                confirmButtonColor: "#18a689",
                confirmButtonText: dict.Yes[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        }
    },
    toastr: {
        success: function (title, message) {
            toastr.success(message, title, { positionClass: "toast-bottom-right" });
        },
        info: function (title, message) {
            toastr.info(message, title, { positionClass: "toast-bottom-right" });
        },
        warning: function (title, message) {
            toastr.warning(message, title, { positionClass: "toast-bottom-right" });
        },
        error: function (title, message) {
            toastr.error(message, title, { positionClass: "toast-bottom-right" });
        }
    },
    control: {
        success: function (ctrolId, message) {
            toastr.success(message, null, { positionClass: "toast-bottom-right" });
        },
        info: function (ctrolId, message) {
            toastr.info(message, null, { positionClass: "toast-bottom-right" });
        },
        warning: function (ctrolId, message) {
            toastr.warning(message, null, { positionClass: "toast-bottom-right" });
        },
        error: function (ctrolId, message) {
            if (ctrolId !== null) {
                var options = {};
                options[ctrolId] = message;
                $('#' + ctrolId).closest("form").validate().showErrors(options);
            }
            else
                toastr.error(message, null, { positionClass: "toast-bottom-right" });
        }
    },
    alert: {
        success: function (title, message) {
            notification.alert.showAlert(message, 'success', title);
        },
        info: function (title, message) {
            notification.alert.showAlert(message, 'info', title);
        },
        warning: function (title, message) {
            notification.alert.showAlert(message, 'warning', title);
        },
        error: function (title, message) {
            notification.alert.showAlert(message, 'danger', title);
        },
        showAlert: function (message, type, title, closeDelay) {
            if ($("#alerts-container").length == 0)
                $("body").append($('<div id="alerts-container" style="position: fixed; width: 50%; left: 25%; top: 10%;">'));

            // default to alert-info; other options include success, warning, danger
            type = type || "info";

            // create the alert div
            var alert = $('<div class="alert alert-' + type + ' fade in">').append($('<button type="button" class="close" data-dismiss="alert">').append("&times;"));

            if (title)
                alert.append('<strong>' + title + ' </strong>');
            alert.append(message);

            // add the alert div to top of alerts-container, use append() to add to bottom
            $("#alerts-container").prepend(alert);

            // if closeDelay was passed - set a timeout to close the alert
            if (closeDelay)
                window.setTimeout(function () { alert.alert("close") }, closeDelay);
        }
    },
    splash: {
        success: function (title, message) {
            notification.splash.showSplash(message, 'success', title);
        },
        info: function (title, message) {
            notification.splash.showSplash(message, 'info', title);
        },
        warning: function (title, message) {
            notification.splash.showSplash(message, 'warning', title);
        },
        error: function (title, message) {
            notification.splash.showSplash(message, 'danger', title);
        },
        showSplash: function (message, type, title) {
            var mainElement = $("form[id$='MainForm']");
            mainElement.css("display", "none");

            // default to alert-info; other options include success, warning, danger
            type = type || "info";

            // create the alert div
            var alert = $('<div class="alert alert-' + type + ' fade in">');

            if (title)
                alert.append('<strong>' + title + ' </strong>');
            alert.append(message);
            mainElement.parent().prepend(alert);
        }
    }
};
// i18next, v1.11.1
// Copyright (c)2015 Jan Mühlemann (jamuhl).
// Distributed under MIT license
// http://i18next.com
!function(a){function b(a,b){if(!b||"function"==typeof b)return a;for(var c in b)a[c]=b[c];return a}function c(a,b,d){for(var e in b)e in a?"string"==typeof a[e]||a[e]instanceof String||"string"==typeof b[e]||b[e]instanceof String?d&&(a[e]=b[e]):c(a[e],b[e],d):a[e]=b[e];return a}function d(a,b,c){var d,e=0,f=a.length,g=void 0===f||"[object Array]"!==Object.prototype.toString.apply(a)||"function"==typeof a;if(c)if(g){for(d in a)if(b.apply(a[d],c)===!1)break}else for(;f>e&&b.apply(a[e++],c)!==!1;);else if(g){for(d in a)if(b.call(a[d],d,a[d])===!1)break}else for(;f>e&&b.call(a[e],e,a[e++])!==!1;);return a}function e(a){return"string"==typeof a?a.replace(/[&<>"'\/]/g,function(a){return V[a]}):a}function f(a){var b=function(a){if(window.XMLHttpRequest)return a(null,new XMLHttpRequest);if(window.ActiveXObject)try{return a(null,new ActiveXObject("Msxml2.XMLHTTP"))}catch(b){return a(null,new ActiveXObject("Microsoft.XMLHTTP"))}return a(new Error)},c=function(a){if("string"==typeof a)return a;var b=[];for(var c in a)a.hasOwnProperty(c)&&b.push(encodeURIComponent(c)+"="+encodeURIComponent(a[c]));return b.join("&")},d=function(a){a=a.replace(/\r\n/g,"\n");for(var b="",c=0;c<a.length;c++){var d=a.charCodeAt(c);128>d?b+=String.fromCharCode(d):d>127&&2048>d?(b+=String.fromCharCode(d>>6|192),b+=String.fromCharCode(63&d|128)):(b+=String.fromCharCode(d>>12|224),b+=String.fromCharCode(d>>6&63|128),b+=String.fromCharCode(63&d|128))}return b},e=function(a){var b="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";a=d(a);var c,e,f,g,h,i,j,k="",l=0;do c=a.charCodeAt(l++),e=a.charCodeAt(l++),f=a.charCodeAt(l++),g=c>>2,h=(3&c)<<4|e>>4,i=(15&e)<<2|f>>6,j=63&f,isNaN(e)?i=j=64:isNaN(f)&&(j=64),k+=b.charAt(g)+b.charAt(h)+b.charAt(i)+b.charAt(j),c=e=f="",g=h=i=j="";while(l<a.length);return k},f=function(){for(var a=arguments[0],b=1;b<arguments.length;b++){var c=arguments[b];for(var d in c)c.hasOwnProperty(d)&&(a[d]=c[d])}return a},g=function(a,d,e,h){"function"==typeof e&&(h=e,e={}),e.cache=e.cache||!1,e.data=e.data||{},e.headers=e.headers||{},e.jsonp=e.jsonp||!1,e.async=void 0===e.async?!0:e.async;var i,j=f({accept:"*/*","content-type":"application/x-www-form-urlencoded;charset=UTF-8"},g.headers,e.headers);if(i="application/json"===j["content-type"]?JSON.stringify(e.data):c(e.data),"GET"===a){var k=[];if(i&&(k.push(i),i=null),e.cache||k.push("_="+(new Date).getTime()),e.jsonp&&(k.push("callback="+e.jsonp),k.push("jsonp="+e.jsonp)),k=k.join("&"),k.length>1&&(d+=d.indexOf("?")>-1?"&"+k:"?"+k),e.jsonp){var l=document.getElementsByTagName("head")[0],m=document.createElement("script");return m.type="text/javascript",m.src=d,void l.appendChild(m)}}b(function(b,c){if(b)return h(b);c.open(a,d,e.async);for(var f in j)j.hasOwnProperty(f)&&c.setRequestHeader(f,j[f]);c.onreadystatechange=function(){if(4===c.readyState){var a=c.responseText||"";if(!h)return;h(c.status,{text:function(){return a},json:function(){try{return JSON.parse(a)}catch(b){return Y.error("Can not parse JSON. URL: "+d),{}}}})}},c.send(i)})},h={authBasic:function(a,b){g.headers.Authorization="Basic "+e(a+":"+b)},connect:function(a,b,c){return g("CONNECT",a,b,c)},del:function(a,b,c){return g("DELETE",a,b,c)},get:function(a,b,c){return g("GET",a,b,c)},head:function(a,b,c){return g("HEAD",a,b,c)},headers:function(a){g.headers=a||{}},isAllowed:function(a,b,c){this.options(a,function(a,d){c(-1!==d.text().indexOf(b))})},options:function(a,b,c){return g("OPTIONS",a,b,c)},patch:function(a,b,c){return g("PATCH",a,b,c)},post:function(a,b,c){return g("POST",a,b,c)},put:function(a,b,c){return g("PUT",a,b,c)},trace:function(a,b,c){return g("TRACE",a,b,c)}},i=a.type?a.type.toLowerCase():"get";h[i](a.url,a,function(b,c){200===b||0===b&&c.text()?a.success(c.json(),b,null):a.error(c.text(),b,null)})}function g(a,b){"function"==typeof a&&(b=a,a={}),a=a||{},Y.extend(U,a),delete U.fixLng,U.functions&&(delete U.functions,Y.extend(Y,a.functions)),"string"==typeof U.ns&&(U.ns={namespaces:[U.ns],defaultNs:U.ns}),"string"==typeof U.fallbackNS&&(U.fallbackNS=[U.fallbackNS]),("string"==typeof U.fallbackLng||"boolean"==typeof U.fallbackLng)&&(U.fallbackLng=[U.fallbackLng]),U.interpolationPrefixEscaped=Y.regexEscape(U.interpolationPrefix),U.interpolationSuffixEscaped=Y.regexEscape(U.interpolationSuffix),U.lng||(U.lng=Y.detectLanguage()),Q=Y.toLanguages(U.lng),L=Q[0],Y.log("currentLng set to: "+L),U.useCookie&&Y.cookie.read(U.cookieName)!==L&&Y.cookie.create(U.cookieName,L,U.cookieExpirationTime,U.cookieDomain),U.detectLngFromLocalStorage&&"undefined"!=typeof document&&window.localStorage&&Y.localStorage.setItem("i18next_lng",L);var c=F;a.fixLng&&(c=function(a,b){return b=b||{},b.lng=b.lng||c.lng,F(a,b)},c.lng=L),_.setCurrentLng(L),M&&U.setJqueryExt?x&&x():y&&y();var d;if(M&&M.Deferred&&(d=M.Deferred()),!U.resStore){var e=Y.toLanguages(U.lng);"string"==typeof U.preload&&(U.preload=[U.preload]);for(var f=0,g=U.preload.length;g>f;f++)for(var h=Y.toLanguages(U.preload[f]),i=0,j=h.length;j>i;i++)e.indexOf(h[i])<0&&e.push(h[i]);return N.sync.load(e,U,function(a,e){O=e,R=!0,b&&b(a,c),d&&(a?d.reject:d.resolve)(a||c)}),d?d.promise():void 0}return O=U.resStore,R=!0,b&&b(null,c),d&&d.resolve(c),d?d.promise():void 0}function h(){return R}function i(a,b){"string"==typeof a&&(a=[a]);for(var c=0,d=a.length;d>c;c++)U.preload.indexOf(a[c])<0&&U.preload.push(a[c]);return g(b)}function j(a,b,c,d,e){"string"!=typeof b?(c=b,b=U.ns.defaultNs):U.ns.namespaces.indexOf(b)<0&&U.ns.namespaces.push(b),O[a]=O[a]||{},O[a][b]=O[a][b]||{},d?Y.deepExtend(O[a][b],c,e):Y.extend(O[a][b],c),U.useLocalStorage&&S._storeLocal(O)}function k(a,b){"string"!=typeof b&&(b=U.ns.defaultNs),O[a]=O[a]||{};var c=O[a][b]||{},d=!1;for(var e in c)c.hasOwnProperty(e)&&(d=!0);return d}function l(a,b){return"string"!=typeof b&&(b=U.ns.defaultNs),O[a]=O[a]||{},Y.extend({},O[a][b])}function m(a,b){"string"!=typeof b&&(b=U.ns.defaultNs),O[a]=O[a]||{},O[a][b]={},U.useLocalStorage&&S._storeLocal(O)}function n(a,b,c,d){"string"!=typeof b?(resource=b,b=U.ns.defaultNs):U.ns.namespaces.indexOf(b)<0&&U.ns.namespaces.push(b),O[a]=O[a]||{},O[a][b]=O[a][b]||{};for(var e=c.split(U.keyseparator),f=0,g=O[a][b];e[f];)f==e.length-1?g[e[f]]=d:(null==g[e[f]]&&(g[e[f]]={}),g=g[e[f]]),f++;U.useLocalStorage&&S._storeLocal(O)}function o(a,b,c){"string"!=typeof b?(c=b,b=U.ns.defaultNs):U.ns.namespaces.indexOf(b)<0&&U.ns.namespaces.push(b);for(var d in c)"string"==typeof c[d]&&n(a,b,d,c[d])}function p(a){U.ns.defaultNs=a}function q(a,b){r([a],b)}function r(a,b){var c={dynamicLoad:U.dynamicLoad,resGetPath:U.resGetPath,getAsync:U.getAsync,customLoad:U.customLoad,ns:{namespaces:a,defaultNs:""}},d=Y.toLanguages(U.lng);"string"==typeof U.preload&&(U.preload=[U.preload]);for(var e=0,f=U.preload.length;f>e;e++)for(var g=Y.toLanguages(U.preload[e]),h=0,i=g.length;i>h;h++)d.indexOf(g[h])<0&&d.push(g[h]);for(var j=[],k=0,l=d.length;l>k;k++){var m=!1,n=O[d[k]];if(n)for(var o=0,p=a.length;p>o;o++)n[a[o]]||(m=!0);else m=!0;m&&j.push(d[k])}j.length?N.sync._fetch(j,c,function(c,d){var e=a.length*j.length;Y.each(a,function(a,c){U.ns.namespaces.indexOf(c)<0&&U.ns.namespaces.push(c),Y.each(j,function(a,f){O[f]=O[f]||{},O[f][c]=d[f][c],e--,0===e&&b&&(U.useLocalStorage&&N.sync._storeLocal(O),b())})})}):b&&b()}function s(a,b,c){return"function"==typeof b?(c=b,b={}):b||(b={}),b.lng=a,g(b,c)}function t(){return L}function u(){var a=["ar","shu","sqr","ssh","xaa","yhd","yud","aao","abh","abv","acm","acq","acw","acx","acy","adf","ads","aeb","aec","afb","ajp","apc","apd","arb","arq","ars","ary","arz","auz","avl","ayh","ayl","ayn","ayp","bbz","pga","he","iw","ps","pbt","pbu","pst","prp","prd","ur","ydd","yds","yih","ji","yi","hbo","men","xmn","fa","jpr","peo","pes","prs","dv","sam"];return a.some(function(a){return new RegExp("^"+a).test(L)})?"rtl":"ltr"}function v(a){O={},s(L,a)}function w(){window.i18next=window.i18n,T?window.i18n=T:delete window.i18n}function x(){function a(a,b,c){if(0!==b.length){var d="text";if(0===b.indexOf("[")){var e=b.split("]");b=e[1],d=e[0].substr(1,e[0].length-1)}b.indexOf(";")===b.length-1&&(b=b.substr(0,b.length-2));var f;if("html"===d)f=U.defaultValueFromContent?M.extend({defaultValue:a.html()},c):c,a.html(M.t(b,f));else if("text"===d)f=U.defaultValueFromContent?M.extend({defaultValue:a.text()},c):c,a.text(M.t(b,f));else if("prepend"===d)f=U.defaultValueFromContent?M.extend({defaultValue:a.html()},c):c,a.prepend(M.t(b,f));else if("append"===d)f=U.defaultValueFromContent?M.extend({defaultValue:a.html()},c):c,a.append(M.t(b,f));else if(0===d.indexOf("data-")){var g=d.substr("data-".length);f=U.defaultValueFromContent?M.extend({defaultValue:a.data(g)},c):c;var h=M.t(b,f);a.data(g,h),a.attr(d,h)}else f=U.defaultValueFromContent?M.extend({defaultValue:a.attr(d)},c):c,a.attr(d,M.t(b,f))}}function b(b,c){var d=b.attr(U.selectorAttr);if(d||"undefined"==typeof d||d===!1||(d=b.text()||b.val()),d){var e=b,f=b.data("i18n-target");if(f&&(e=b.find(f)||b),c||U.useDataAttrOptions!==!0||(c=b.data("i18n-options")),c=c||{},d.indexOf(";")>=0){var g=d.split(";");M.each(g,function(b,d){""!==d&&a(e,d,c)})}else a(e,d,c);if(U.useDataAttrOptions===!0){var h=M.extend({lng:"non",lngs:[],_origLng:"non"},c);delete h.lng,delete h.lngs,delete h._origLng,b.data("i18n-options",h)}}}M.t=M.t||F,M.fn.i18n=function(a){return this.each(function(){b(M(this),a);var c=M(this).find("["+U.selectorAttr+"]");c.each(function(){b(M(this),a)})})}}function y(){function a(a,b,c){if(0!==b.length){var d="text";if(0===b.indexOf("[")){var e=b.split("]");b=e[1],d=e[0].substr(1,e[0].length-1)}b.indexOf(";")===b.length-1&&(b=b.substr(0,b.length-2)),"html"===d?a.innerHTML=F(b,c):"text"===d?a.textContent=F(b,c):"prepend"===d?a.insertAdjacentHTML(F(b,c),"afterbegin"):"append"===d?a.insertAdjacentHTML(F(b,c),"beforeend"):a.setAttribute(d,F(b,c))}}function b(b,c){var d=b.getAttribute(U.selectorAttr);if(d||"undefined"==typeof d||d===!1||(d=b.textContent||b.value),d){var e=b,f=b.getAttribute("i18n-target");if(f&&(e=b.querySelector(f)||b),d.indexOf(";")>=0)for(var g=d.split(";"),h=0,i=g.length;i>h;h++)""!==g[h]&&a(e,g[h],c);else a(e,d,c)}}N.translateObject=function(a,c){for(var d=a.querySelectorAll("["+U.selectorAttr+"]"),e=0,f=d.length;f>e;e++)b(d[e],c)}}function z(a,b,c,d){if(!a)return a;if(d=d||b,a.indexOf(d.interpolationPrefix||U.interpolationPrefix)<0)return a;var e=d.interpolationPrefix?Y.regexEscape(d.interpolationPrefix):U.interpolationPrefixEscaped,f=d.interpolationSuffix?Y.regexEscape(d.interpolationSuffix):U.interpolationSuffixEscaped,g=d.keyseparator||U.keyseparator,h=b.replace&&"object"==typeof b.replace?b.replace:b,i=new RegExp([e,"(.+?)","(HTML)?",f].join(""),"g"),j=d.escapeInterpolation||U.escapeInterpolation;return a.replace(i,function(a,b,c){for(var d=h,e=b;e.indexOf(g)>=0&&"object"==typeof d&&d;){var f=e.slice(0,e.indexOf(g));e=e.slice(e.indexOf(g)+1),d=d[f]}if(d&&"object"==typeof d&&d.hasOwnProperty(e)){{d[e]}return j&&!c?Y.escape(d[e]):d[e]}return a})}function A(a,b){var c=",",d="{",e="}",f=Y.extend({},b);for(delete f.postProcess,delete f.isFallbackLookup;-1!=a.indexOf(U.reusePrefix)&&(P++,!(P>U.maxRecursion));){var g=a.lastIndexOf(U.reusePrefix),h=a.indexOf(U.reuseSuffix,g)+U.reuseSuffix.length,i=a.substring(g,h),j=i.replace(U.reusePrefix,"").replace(U.reuseSuffix,"");if(g>=h)return Y.error("there is an missing closing in following translation value",a),"";if(-1!=j.indexOf(c)){var k=j.indexOf(c);if(-1!=j.indexOf(d,k)&&-1!=j.indexOf(e,k)){var l=j.indexOf(d,k),m=j.indexOf(e,l)+e.length;try{f=Y.extend(f,JSON.parse(j.substring(l,m))),j=j.substring(0,k)}catch(n){}}}var o=I(j,f);a=a.replace(i,Y.regexReplacementEscape(o))}return a}function B(a){return a.context&&("string"==typeof a.context||"number"==typeof a.context)}function C(a){return void 0!==a.count&&"string"!=typeof a.count}function D(a){return void 0!==a.indefinite_article&&"string"!=typeof a.indefinite_article&&a.indefinite_article}function E(a,b){b=b||{};var c=G(a,b),d=J(a,b);return void 0!==d||d===c}function F(a,b){return R?(P=0,I.apply(null,arguments)):(Y.log("i18next not finished initialization. you might have called t function before loading resources finished."),b&&b.defaultValue?b.detaultValue:"")}function G(a,b){return void 0!==b.defaultValue?b.defaultValue:a}function H(){for(var a=[],b=1;b<arguments.length;b++)a.push(arguments[b]);return{postProcess:"sprintf",sprintf:a}}function I(a,b){if("undefined"!=typeof b&&"object"!=typeof b?"sprintf"===U.shortcutFunction?b=H.apply(null,arguments):"defaultValue"===U.shortcutFunction&&(b={defaultValue:b}):b=b||{},"object"==typeof U.defaultVariables&&(b=Y.extend({},U.defaultVariables,b)),void 0===a||null===a||""===a)return"";"number"==typeof a&&(a=String(a)),"string"==typeof a&&(a=[a]);var c=a[0];if(a.length>1)for(var d=0;d<a.length&&(c=a[d],!E(c,b));d++);var e,f=G(c,b),g=J(c,b),h=b.nsseparator||U.nsseparator,i=b.lng?Y.toLanguages(b.lng,b.fallbackLng):Q,j=b.ns||U.ns.defaultNs;c.indexOf(h)>-1&&(e=c.split(h),j=e[0],c=e[1]),void 0===g&&U.sendMissing&&"function"==typeof U.missingKeyHandler&&(b.lng?U.missingKeyHandler(i[0],j,c,f,i):U.missingKeyHandler(U.lng,j,c,f,i));var k;k="string"==typeof U.postProcess&&""!==U.postProcess?[U.postProcess]:"array"==typeof U.postProcess||"object"==typeof U.postProcess?U.postProcess:[],"string"==typeof b.postProcess&&""!==b.postProcess?k=k.concat([b.postProcess]):("array"==typeof b.postProcess||"object"==typeof b.postProcess)&&(k=k.concat(b.postProcess)),void 0!==g&&k.length&&k.forEach(function(a){aa[a]&&(g=aa[a](g,c,b))});var l=f;return f.indexOf(h)>-1&&(e=f.split(h),l=e[1]),l===c&&U.parseMissingKey&&(f=U.parseMissingKey(f)),void 0===g&&(f=z(f,b),f=A(f,b),k.length&&(g=G(c,b),k.forEach(function(a){aa[a]&&(g=aa[a](g,c,b))}))),void 0!==g?g:f}function J(a,b){b=b||{};var c,d,e=G(a,b),f=Q;if(!O)return e;if("cimode"===f[0].toLowerCase())return e;if(b.lngs&&(f=b.lngs),b.lng&&(f=Y.toLanguages(b.lng,b.fallbackLng),!O[f[0]])){var g=U.getAsync;U.getAsync=!1,N.sync.load(f,U,function(a,b){Y.extend(O,b),U.getAsync=g})}var h=b.ns||U.ns.defaultNs,i=b.nsseparator||U.nsseparator;if(a.indexOf(i)>-1){var j=a.split(i);h=j[0],a=j[1]}if(B(b)){c=Y.extend({},b),delete c.context,c.defaultValue=U.contextNotFound;var k=h+i+a+"_"+b.context;if(d=F(k,c),d!=U.contextNotFound)return z(d,{context:b.context})}if(C(b,f[0])){c=Y.extend({lngs:[f[0]]},b),delete c.count,c._origLng=c._origLng||c.lng||f[0],delete c.lng,c.defaultValue=U.pluralNotFound;var l;if(_.needsPlural(f[0],b.count)){l=h+i+a+U.pluralSuffix;var m=_.get(f[0],b.count);m>=0?l=l+"_"+m:1===m&&(l=h+i+a)}else l=h+i+a;if(d=F(l,c),d!=U.pluralNotFound)return z(d,{count:b.count,interpolationPrefix:b.interpolationPrefix,interpolationSuffix:b.interpolationSuffix});if(!(f.length>1))return c.lng=c._origLng,delete c._origLng,d=F(h+i+a,c),z(d,{count:b.count,interpolationPrefix:b.interpolationPrefix,interpolationSuffix:b.interpolationSuffix});var n=f.slice();if(n.shift(),b=Y.extend(b,{lngs:n}),b._origLng=c._origLng,delete b.lng,d=F(h+i+a,b),d!=U.pluralNotFound)return d}if(D(b)){var o=Y.extend({},b);delete o.indefinite_article,o.defaultValue=U.indefiniteNotFound;var p=h+i+a+(b.count&&!C(b,f[0])||!b.count?U.indefiniteSuffix:"");if(d=F(p,o),d!=U.indefiniteNotFound)return d}for(var q,r=b.keyseparator||U.keyseparator,s=a.split(r),t=0,u=f.length;u>t&&void 0===q;t++){for(var v=f[t],w=0,x=O[v]&&O[v][h];s[w];)x=x&&x[s[w]],w++;if(void 0!==x&&(!U.showKeyIfEmpty||""!==x)){var y=Object.prototype.toString.apply(x);if("string"==typeof x)x=z(x,b),x=A(x,b);else if("[object Array]"!==y||U.returnObjectTrees||b.returnObjectTrees){if(null===x&&U.fallbackOnNull===!0)x=void 0;else if(null!==x)if(U.returnObjectTrees||b.returnObjectTrees){if("[object Number]"!==y&&"[object Function]"!==y&&"[object RegExp]"!==y){var E="[object Array]"===y?[]:{};Y.each(x,function(c){E[c]=I(h+i+a+r+c,b)}),x=E}}else U.objectTreeKeyHandler&&"function"==typeof U.objectTreeKeyHandler?x=U.objectTreeKeyHandler(a,x,v,h,b):(x="key '"+h+":"+a+" ("+v+")' returned an object instead of string.",Y.log(x))}else x=x.join("\n"),x=z(x,b),x=A(x,b);"string"==typeof x&&""===x.trim()&&U.fallbackOnEmpty===!0&&(x=void 0),q=x}}if(void 0===q&&!b.isFallbackLookup&&(U.fallbackToDefaultNS===!0||U.fallbackNS&&U.fallbackNS.length>0)){if(b.isFallbackLookup=!0,U.fallbackNS.length){for(var H=0,K=U.fallbackNS.length;K>H;H++)if(q=J(U.fallbackNS[H]+i+a,b),q||""===q&&U.fallbackOnEmpty===!1){var L=q.indexOf(i)>-1?q.split(i)[1]:q,M=e.indexOf(i)>-1?e.split(i)[1]:e;if(L!==M)break}}else b.ns=U.ns.defaultNs,q=J(a,b);b.isFallbackLookup=!1}return q}function K(){var a,b=U.lngWhitelist||[],c=[];if("undefined"!=typeof window&&!function(){for(var a=window.location.search.substring(1),b=a.split("&"),d=0;d<b.length;d++){var e=b[d].indexOf("=");if(e>0){var f=b[d].substring(0,e);f==U.detectLngQS&&c.push(b[d].substring(e+1))}}}(),U.useCookie&&"undefined"!=typeof document){var d=Y.cookie.read(U.cookieName);d&&c.push(d)}if(U.detectLngFromLocalStorage&&"undefined"!=typeof window&&window.localStorage){var e=Y.localStorage.getItem("i18next_lng");e&&c.push(e)}if("undefined"!=typeof navigator){if(navigator.languages)for(var f=0;f<navigator.languages.length;f++)c.push(navigator.languages[f]);navigator.userLanguage&&c.push(navigator.userLanguage),navigator.language&&c.push(navigator.language)}return function(){for(var d=0;d<c.length;d++){var e=c[d];if(e.indexOf("-")>-1){var f=e.split("-");e=U.lowerCaseLng?f[0].toLowerCase()+"-"+f[1].toLowerCase():f[0].toLowerCase()+"-"+f[1].toUpperCase()}if(0===b.length||b.indexOf(e)>-1){a=e;break}}}(),a||(a=U.fallbackLng[0]),a}Array.prototype.indexOf||(Array.prototype.indexOf=function(a){"use strict";if(null==this)throw new TypeError;var b=Object(this),c=b.length>>>0;if(0===c)return-1;var d=0;if(arguments.length>0&&(d=Number(arguments[1]),d!=d?d=0:0!=d&&d!=1/0&&d!=-(1/0)&&(d=(d>0||-1)*Math.floor(Math.abs(d)))),d>=c)return-1;for(var e=d>=0?d:Math.max(c-Math.abs(d),0);c>e;e++)if(e in b&&b[e]===a)return e;return-1}),Array.prototype.lastIndexOf||(Array.prototype.lastIndexOf=function(a){"use strict";if(null==this)throw new TypeError;var b=Object(this),c=b.length>>>0;if(0===c)return-1;var d=c;arguments.length>1&&(d=Number(arguments[1]),d!=d?d=0:0!=d&&d!=1/0&&d!=-(1/0)&&(d=(d>0||-1)*Math.floor(Math.abs(d))));for(var e=d>=0?Math.min(d,c-1):c-Math.abs(d);e>=0;e--)if(e in b&&b[e]===a)return e;return-1}),"function"!=typeof String.prototype.trim&&(String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g,"")});var L,M=a.jQuery||a.Zepto,N={},O={},P=0,Q=[],R=!1,S={},T=null;"undefined"!=typeof module&&module.exports?module.exports=N:(M&&(M.i18n=M.i18n||N),a.i18n&&(T=a.i18n),a.i18n=N),S={load:function(a,b,c){b.useLocalStorage?S._loadLocal(a,b,function(d,e){for(var f=[],g=0,h=a.length;h>g;g++)e[a[g]]||f.push(a[g]);f.length>0?S._fetch(f,b,function(a,b){Y.extend(e,b),S._storeLocal(b),c(a,e)}):c(d,e)}):S._fetch(a,b,function(a,b){c(a,b)})},_loadLocal:function(a,b,c){var d={},e=(new Date).getTime();if(window.localStorage){var f=a.length;Y.each(a,function(a,g){var h=Y.localStorage.getItem("res_"+g);h&&(h=JSON.parse(h),h.i18nStamp&&h.i18nStamp+b.localStorageExpirationTime>e&&(d[g]=h)),f--,0===f&&c(null,d)})}},_storeLocal:function(a){if(window.localStorage)for(var b in a)a[b].i18nStamp=(new Date).getTime(),Y.localStorage.setItem("res_"+b,JSON.stringify(a[b]))},_fetch:function(a,b,c){var d=b.ns,e={};if(b.dynamicLoad){var f=function(a,b){c(a,b)};if("function"==typeof b.customLoad)b.customLoad(a,d.namespaces,b,f);else{var g=z(b.resGetPath,{lng:a.join("+"),ns:d.namespaces.join("+")});Y.ajax({url:g,cache:b.cache,success:function(a){Y.log("loaded: "+g),f(null,a)},error:function(a,b,c){Y.log("failed loading: "+g),f("failed loading resource.json error: "+c)},dataType:"json",async:b.getAsync,timeout:b.ajaxTimeout})}}else{var h,i=d.namespaces.length*a.length;Y.each(d.namespaces,function(d,f){Y.each(a,function(a,d){var g=function(a,b){a&&(h=h||[],h.push(a)),e[d]=e[d]||{},e[d][f]=b,i--,0===i&&c(h,e)};"function"==typeof b.customLoad?b.customLoad(d,f,b,g):S._fetchOne(d,f,b,g)})})}},_fetchOne:function(a,b,c,d){var e=z(c.resGetPath,{lng:a,ns:b});Y.ajax({url:e,cache:c.cache,success:function(a){Y.log("loaded: "+e),d(null,a)},error:function(a,b,c){if(b&&200==b||a&&a.status&&200==a.status)Y.error("There is a typo in: "+e);else if(b&&404==b||a&&a.status&&404==a.status)Y.log("Does not exist: "+e);else{var f=b?b:a&&a.status?a.status:null;Y.log(f+" when loading "+e)}d(c,{})},dataType:"json",async:c.getAsync,timeout:c.ajaxTimeout,headers:c.headers})},postMissing:function(a,b,c,d,e){var f={};f[c]=d;var g=[];if("fallback"===U.sendMissingTo&&U.fallbackLng[0]!==!1)for(var h=0;h<U.fallbackLng.length;h++)g.push({lng:U.fallbackLng[h],url:z(U.resPostPath,{lng:U.fallbackLng[h],ns:b})});else if("current"===U.sendMissingTo||"fallback"===U.sendMissingTo&&U.fallbackLng[0]===!1)g.push({lng:a,url:z(U.resPostPath,{lng:a,ns:b})});else if("all"===U.sendMissingTo)for(var h=0,i=e.length;i>h;h++)g.push({lng:e[h],url:z(U.resPostPath,{lng:e[h],ns:b})});for(var j=0,k=g.length;k>j;j++){var l=g[j];Y.ajax({url:l.url,type:U.sendType,data:f,success:function(){Y.log("posted missing key '"+c+"' to: "+l.url);for(var a=c.split("."),e=0,f=O[l.lng][b];a[e];)f=f[a[e]]=e===a.length-1?d:f[a[e]]||{},e++},error:function(){Y.log("failed posting missing key '"+c+"' to: "+l.url)},dataType:"json",async:U.postAsync,timeout:U.ajaxTimeout})}},reload:v};var U={lng:void 0,load:"all",preload:[],lowerCaseLng:!1,returnObjectTrees:!1,fallbackLng:["dev"],fallbackNS:[],detectLngQS:"setLng",detectLngFromLocalStorage:!1,ns:{namespaces:["translation"],defaultNs:"translation"},fallbackOnNull:!0,fallbackOnEmpty:!1,fallbackToDefaultNS:!1,showKeyIfEmpty:!1,nsseparator:":",keyseparator:".",selectorAttr:"data-i18n",debug:!1,resGetPath:"locales/__lng__/__ns__.json",resPostPath:"locales/add/__lng__/__ns__",getAsync:!0,postAsync:!0,resStore:void 0,useLocalStorage:!1,localStorageExpirationTime:6048e5,dynamicLoad:!1,sendMissing:!1,sendMissingTo:"fallback",sendType:"POST",interpolationPrefix:"__",interpolationSuffix:"__",defaultVariables:!1,reusePrefix:"$t(",reuseSuffix:")",pluralSuffix:"_plural",pluralNotFound:["plural_not_found",Math.random()].join(""),contextNotFound:["context_not_found",Math.random()].join(""),escapeInterpolation:!1,indefiniteSuffix:"_indefinite",indefiniteNotFound:["indefinite_not_found",Math.random()].join(""),setJqueryExt:!0,defaultValueFromContent:!0,useDataAttrOptions:!1,cookieExpirationTime:void 0,useCookie:!0,cookieName:"i18next",cookieDomain:void 0,objectTreeKeyHandler:void 0,postProcess:void 0,parseMissingKey:void 0,missingKeyHandler:S.postMissing,ajaxTimeout:0,shortcutFunction:"sprintf"},V={"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;","/":"&#x2F;"},W={create:function(a,b,c,d){var e;if(c){var f=new Date;f.setTime(f.getTime()+60*c*1e3),e="; expires="+f.toGMTString()}else e="";d=d?"domain="+d+";":"",document.cookie=a+"="+b+e+";"+d+"path=/"},read:function(a){for(var b=a+"=",c=document.cookie.split(";"),d=0;d<c.length;d++){for(var e=c[d];" "==e.charAt(0);)e=e.substring(1,e.length);if(0===e.indexOf(b))return e.substring(b.length,e.length)}return null},remove:function(a){this.create(a,"",-1)}},X={create:function(){},read:function(){return null},remove:function(){}},Y={extend:M?M.extend:b,deepExtend:c,each:M?M.each:d,ajax:M?M.ajax:"undefined"!=typeof document?f:function(){},cookie:"undefined"!=typeof document?W:X,detectLanguage:K,escape:e,log:function(a){U.debug&&"undefined"!=typeof console&&console.log(a)},error:function(a){"undefined"!=typeof console&&console.error(a)},getCountyIndexOfLng:function(a){var b=0;return("nb-NO"===a||"nn-NO"===a||"nb-no"===a||"nn-no"===a)&&(b=1),b},toLanguages:function(a,b){function c(a){var b=a;if("string"==typeof a&&a.indexOf("-")>-1){var c=a.split("-");b=U.lowerCaseLng?c[0].toLowerCase()+"-"+c[1].toLowerCase():c[0].toLowerCase()+"-"+c[1].toUpperCase()}else b=U.lowerCaseLng?a.toLowerCase():a;return b}var d=this.log;b=b||U.fallbackLng,"string"==typeof b&&(b=[b]);var e=[],f=U.lngWhitelist||!1,g=function(a){!f||f.indexOf(a)>-1?e.push(a):d("rejecting non-whitelisted language: "+a)};if("string"==typeof a&&a.indexOf("-")>-1){var h=a.split("-");"unspecific"!==U.load&&g(c(a)),"current"!==U.load&&g(c(h[this.getCountyIndexOfLng(a)]))}else g(c(a));for(var i=0;i<b.length;i++)-1===e.indexOf(b[i])&&b[i]&&e.push(c(b[i]));return e},regexEscape:function(a){return a.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g,"\\$&")},regexReplacementEscape:function(a){return"string"==typeof a?a.replace(/\$/g,"$$$$"):a},localStorage:{setItem:function(a,b){if(window.localStorage)try{window.localStorage.setItem(a,b)}catch(c){Y.log('failed to set value for key "'+a+'" to localStorage.')}},getItem:function(a,b){if(window.localStorage)try{return window.localStorage.getItem(a,b)}catch(c){return void Y.log('failed to get value for key "'+a+'" from localStorage.')}}}};Y.applyReplacement=z;var Z=[["ach","Acholi",[1,2],1],["af","Afrikaans",[1,2],2],["ak","Akan",[1,2],1],["am","Amharic",[1,2],1],["an","Aragonese",[1,2],2],["ar","Arabic",[0,1,2,3,11,100],5],["arn","Mapudungun",[1,2],1],["ast","Asturian",[1,2],2],["ay","Aymará",[1],3],["az","Azerbaijani",[1,2],2],["be","Belarusian",[1,2,5],4],["bg","Bulgarian",[1,2],2],["bn","Bengali",[1,2],2],["bo","Tibetan",[1],3],["br","Breton",[1,2],1],["bs","Bosnian",[1,2,5],4],["ca","Catalan",[1,2],2],["cgg","Chiga",[1],3],["cs","Czech",[1,2,5],6],["csb","Kashubian",[1,2,5],7],["cy","Welsh",[1,2,3,8],8],["da","Danish",[1,2],2],["de","German",[1,2],2],["dev","Development Fallback",[1,2],2],["dz","Dzongkha",[1],3],["el","Greek",[1,2],2],["en","English",[1,2],2],["eo","Esperanto",[1,2],2],["es","Spanish",[1,2],2],["es_ar","Argentinean Spanish",[1,2],2],["et","Estonian",[1,2],2],["eu","Basque",[1,2],2],["fa","Persian",[1],3],["fi","Finnish",[1,2],2],["fil","Filipino",[1,2],1],["fo","Faroese",[1,2],2],["fr","French",[1,2],9],["fur","Friulian",[1,2],2],["fy","Frisian",[1,2],2],["ga","Irish",[1,2,3,7,11],10],["gd","Scottish Gaelic",[1,2,3,20],11],["gl","Galician",[1,2],2],["gu","Gujarati",[1,2],2],["gun","Gun",[1,2],1],["ha","Hausa",[1,2],2],["he","Hebrew",[1,2],2],["hi","Hindi",[1,2],2],["hr","Croatian",[1,2,5],4],["hu","Hungarian",[1,2],2],["hy","Armenian",[1,2],2],["ia","Interlingua",[1,2],2],["id","Indonesian",[1],3],["is","Icelandic",[1,2],12],["it","Italian",[1,2],2],["ja","Japanese",[1],3],["jbo","Lojban",[1],3],["jv","Javanese",[0,1],13],["ka","Georgian",[1],3],["kk","Kazakh",[1],3],["km","Khmer",[1],3],["kn","Kannada",[1,2],2],["ko","Korean",[1],3],["ku","Kurdish",[1,2],2],["kw","Cornish",[1,2,3,4],14],["ky","Kyrgyz",[1],3],["lb","Letzeburgesch",[1,2],2],["ln","Lingala",[1,2],1],["lo","Lao",[1],3],["lt","Lithuanian",[1,2,10],15],["lv","Latvian",[1,2,0],16],["mai","Maithili",[1,2],2],["mfe","Mauritian Creole",[1,2],1],["mg","Malagasy",[1,2],1],["mi","Maori",[1,2],1],["mk","Macedonian",[1,2],17],["ml","Malayalam",[1,2],2],["mn","Mongolian",[1,2],2],["mnk","Mandinka",[0,1,2],18],["mr","Marathi",[1,2],2],["ms","Malay",[1],3],["mt","Maltese",[1,2,11,20],19],["nah","Nahuatl",[1,2],2],["nap","Neapolitan",[1,2],2],["nb","Norwegian Bokmal",[1,2],2],["ne","Nepali",[1,2],2],["nl","Dutch",[1,2],2],["nn","Norwegian Nynorsk",[1,2],2],["no","Norwegian",[1,2],2],["nso","Northern Sotho",[1,2],2],["oc","Occitan",[1,2],1],["or","Oriya",[2,1],2],["pa","Punjabi",[1,2],2],["pap","Papiamento",[1,2],2],["pl","Polish",[1,2,5],7],["pms","Piemontese",[1,2],2],["ps","Pashto",[1,2],2],["pt","Portuguese",[1,2],2],["pt_br","Brazilian Portuguese",[1,2],2],["rm","Romansh",[1,2],2],["ro","Romanian",[1,2,20],20],["ru","Russian",[1,2,5],4],["sah","Yakut",[1],3],["sco","Scots",[1,2],2],["se","Northern Sami",[1,2],2],["si","Sinhala",[1,2],2],["sk","Slovak",[1,2,5],6],["sl","Slovenian",[5,1,2,3],21],["so","Somali",[1,2],2],["son","Songhay",[1,2],2],["sq","Albanian",[1,2],2],["sr","Serbian",[1,2,5],4],["su","Sundanese",[1],3],["sv","Swedish",[1,2],2],["sw","Swahili",[1,2],2],["ta","Tamil",[1,2],2],["te","Telugu",[1,2],2],["tg","Tajik",[1,2],1],["th","Thai",[1],3],["ti","Tigrinya",[1,2],1],["tk","Turkmen",[1,2],2],["tr","Turkish",[1,2],1],["tt","Tatar",[1],3],["ug","Uyghur",[1],3],["uk","Ukrainian",[1,2,5],4],["ur","Urdu",[1,2],2],["uz","Uzbek",[1,2],1],["vi","Vietnamese",[1],3],["wa","Walloon",[1,2],1],["wo","Wolof",[1],3],["yo","Yoruba",[1,2],2],["zh","Chinese",[1],3]],$={1:function(a){return Number(a>1)},2:function(a){return Number(1!=a)},3:function(){return 0},4:function(a){return Number(a%10==1&&a%100!=11?0:a%10>=2&&4>=a%10&&(10>a%100||a%100>=20)?1:2)},5:function(a){return Number(0===a?0:1==a?1:2==a?2:a%100>=3&&10>=a%100?3:a%100>=11?4:5)},6:function(a){return Number(1==a?0:a>=2&&4>=a?1:2)},7:function(a){return Number(1==a?0:a%10>=2&&4>=a%10&&(10>a%100||a%100>=20)?1:2)},8:function(a){return Number(1==a?0:2==a?1:8!=a&&11!=a?2:3)},9:function(a){return Number(a>=2)},10:function(a){return Number(1==a?0:2==a?1:7>a?2:11>a?3:4)},11:function(a){return Number(1==a||11==a?0:2==a||12==a?1:a>2&&20>a?2:3)},12:function(a){return Number(a%10!=1||a%100==11)},13:function(a){return Number(0!==a)},14:function(a){return Number(1==a?0:2==a?1:3==a?2:3)},15:function(a){return Number(a%10==1&&a%100!=11?0:a%10>=2&&(10>a%100||a%100>=20)?1:2)},16:function(a){return Number(a%10==1&&a%100!=11?0:0!==a?1:2)},17:function(a){return Number(1==a||a%10==1?0:1)},18:function(a){return Number(0==a?0:1==a?1:2)},19:function(a){return Number(1==a?0:0===a||a%100>1&&11>a%100?1:a%100>10&&20>a%100?2:3)},20:function(a){return Number(1==a?0:0===a||a%100>0&&20>a%100?1:2)},21:function(a){return Number(a%100==1?1:a%100==2?2:a%100==3||a%100==4?3:0)}},_={rules:function(){var a,b={};for(a=Z.length;a--;)b[Z[a][0]]={name:Z[a][1],numbers:Z[a][2],plurals:$[Z[a][3]]};return b}(),addRule:function(a,b){_.rules[a]=b},setCurrentLng:function(a){if(!_.currentRule||_.currentRule.lng!==a){var b=a.split("-");_.currentRule={lng:a,rule:_.rules[b[0]]}}},needsPlural:function(a,b){var c,d=a.split("-");return c=_.currentRule&&_.currentRule.lng===a?_.currentRule.rule:_.rules[d[Y.getCountyIndexOfLng(a)]],c&&c.numbers.length<=1?!1:1!==this.get(a,b)},get:function(a,b){function c(b,c){var d;if(d=_.currentRule&&_.currentRule.lng===a?_.currentRule.rule:_.rules[b]){var e;e=d.plurals(d.noAbs?c:Math.abs(c));var f=d.numbers[e];return 2===d.numbers.length&&1===d.numbers[0]&&(2===f?f=-1:1===f&&(f=1)),f}return 1===c?"1":"-1"}var d=a.split("-");return c(d[Y.getCountyIndexOfLng(a)],b)}},aa={},ba=function(a,b){aa[a]=b},ca=function(){function a(a){return Object.prototype.toString.call(a).slice(8,-1).toLowerCase()}function b(a,b){for(var c=[];b>0;c[--b]=a);return c.join("")}var c=function(){return c.cache.hasOwnProperty(arguments[0])||(c.cache[arguments[0]]=c.parse(arguments[0])),c.format.call(null,c.cache[arguments[0]],arguments)};return c.format=function(c,d){var e,f,g,h,i,j,k,l=1,m=c.length,n="",o=[];for(f=0;m>f;f++)if(n=a(c[f]),"string"===n)o.push(c[f]);else if("array"===n){if(h=c[f],h[2])for(e=d[l],g=0;g<h[2].length;g++){if(!e.hasOwnProperty(h[2][g]))throw ca('[sprintf] property "%s" does not exist',h[2][g]);e=e[h[2][g]]}else e=h[1]?d[h[1]]:d[l++];if(/[^s]/.test(h[8])&&"number"!=a(e))throw ca("[sprintf] expecting number but found %s",a(e));switch(h[8]){case"b":e=e.toString(2);break;case"c":e=String.fromCharCode(e);break;case"d":e=parseInt(e,10);break;case"e":e=h[7]?e.toExponential(h[7]):e.toExponential();break;case"f":e=h[7]?parseFloat(e).toFixed(h[7]):parseFloat(e);break;case"o":e=e.toString(8);break;case"s":e=(e=String(e))&&h[7]?e.substring(0,h[7]):e;break;case"u":e=Math.abs(e);break;case"x":e=e.toString(16);break;case"X":e=e.toString(16).toUpperCase()}e=/[def]/.test(h[8])&&h[3]&&e>=0?"+"+e:e,j=h[4]?"0"==h[4]?"0":h[4].charAt(1):" ",k=h[6]-String(e).length,i=h[6]?b(j,k):"",o.push(h[5]?e+i:i+e)}return o.join("")},c.cache={},c.parse=function(a){for(var b=a,c=[],d=[],e=0;b;){if(null!==(c=/^[^\x25]+/.exec(b)))d.push(c[0]);else if(null!==(c=/^\x25{2}/.exec(b)))d.push("%");else{if(null===(c=/^\x25(?:([1-9]\d*)\$|\(([^\)]+)\))?(\+)?(0|'[^$])?(-)?(\d+)?(?:\.(\d+))?([b-fosuxX])/.exec(b)))throw"[sprintf] huh?";

    if(c[2]){e|=1;var f=[],g=c[2],h=[];if(null===(h=/^([a-z_][a-z_\d]*)/i.exec(g)))throw"[sprintf] huh?";for(f.push(h[1]);""!==(g=g.substring(h[0].length));)if(null!==(h=/^\.([a-z_][a-z_\d]*)/i.exec(g)))f.push(h[1]);else{if(null===(h=/^\[(\d+)\]/.exec(g)))throw"[sprintf] huh?";f.push(h[1])}c[2]=f}else e|=2;if(3===e)throw"[sprintf] mixing positional and named placeholders is not (yet) supported";d.push(c)}b=b.substring(c[0].length)}return d},c}(),da=function(a,b){return b.unshift(a),ca.apply(null,b)};ba("sprintf",function(a,b,c){return c.sprintf?"[object Array]"===Object.prototype.toString.apply(c.sprintf)?da(a,c.sprintf):"object"==typeof c.sprintf?ca(a,c.sprintf):a:a}),N.init=g,N.isInitialized=h,N.setLng=s,N.preload=i,N.addResourceBundle=j,N.hasResourceBundle=k,N.getResourceBundle=l,N.addResource=n,N.addResources=o,N.removeResourceBundle=m,N.loadNamespace=q,N.loadNamespaces=r,N.setDefaultNamespace=p,N.t=F,N.translate=F,N.exists=E,N.detectLanguage=Y.detectLanguage,N.pluralExtensions=_,N.sync=S,N.functions=Y,N.lng=t,N.dir=u,N.addPostProcessor=ba,N.applyReplacement=Y.applyReplacement,N.options=U,N.noConflict=w}("undefined"==typeof exports?window:exports);
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
var dict = {
    NewNotification: {
        en: "Have a new notification",
        es: "Tiene una nueva notificación",
        pt: "Tenha uma nova notificação"
    },
    Action: {
        en: "Action",
        es: "Acción",
        pt: "Ação"
    },
    Actions: {
        en: "Actions",
        es: "Acciones"
    },
    ActionTitle: {
        en: "Action title",
        es: "Título de la acción",
        pt: "Título da ação"
    },
    Add: {
        en: "Add",
        es: "Agregar",
        pt: "Adicionar"
    },
    AddTaskSuccess: {
        en: "Task has been added successfully!",
        es: "¡Tarea agregada con éxito!",
        pt: "Tarefa adicionada com sucesso!"
    },
    Transaction: {
        en: "Transaction",
        es: "Transacción",
        pt: "Transação"
    },
    TransactionTitle: {
        en: "Assign a Back Office transaction for a shortcut",
        es: "Asignar una transacción de Back Office para un acceso directo",
        pt: "Atribuir uma transação do Back Office para um atalho"
    },
    AccessDenied: {
        en: "Access denied",
        es: "Acceso denegado",
        pt: "Acesso negado"
    },
    AddWidgetSuccess: {
        en: "Widget has been added successfully!",
        es: "¡Widget agregado con éxito!",
        pt: "Widget adicionado com sucesso!"
    },
    Alerts: {
        en: "Alerts",
        es: "Alertas",
        pt: "Alertas"
    },
    AreYouSure: {
        en: "Are you sure?",
        es: "¿Estás seguro?",
        pt: "Você tem certeza?"
    },
    AssignedTo: {
        en: "Assigned to",
        es: "Asignado a",
        pt: "Atribuído a"
    },
    AssignedToTitle: {
        en: "Is to assign the appointment to one or more users",
        es: "Es para asignar la cita a uno o varios usuarios",
        pt: "É atribuir o compromisso a um ou mais usuários"
    },
    AssignSelectedTasks: {
        en: "Assign selected tasks",
        es: "Asignar tareas seleccionadas",
        pt: "Atribuir tarefas selecionadas"
    },
    AssignSelectedTasksTo: {
        en: "Assign selected tasks to",
        es: "Asignar tareas seleccionadas a",
        pt: "Atribuir tarefas selecionadas a"
    },
    Assignment: {
        en: "Assignment",
        es: "Asignación",
        pt: "Atribuição"
    },
    Available: {
        en: "Available",
        es: "Disponible",
        pt: "Disponível"
    },
    BestTimeToCall: {
        en: "Best time to call",
        es: "Mejor momento para llamar"
    },
    Cancel: {
        en: "Cancel",
        es: "Cancelar",
        pt: "Cancelar"
    },
    CancelNo: {
        en: "Do not",
        es: "No",
        pt: "Não"
    },
    Canceled: {
        en: "Canceled",
        es: "Cancelada",
        pt: "Cancelada"
    },
    DeleteRowConfirmation: {
        en: "Are you sure you want to delete the selected records?",
        es: "¿Está usted seguro de querer eliminar los registros seleccionados?",
        pt: "Tem certeza de que deseja excluir os registros selecionados?"
    },
    ClickToRefreshPage: {
        en: "Click on 'OK' to refresh the page.",
        es: "Haz clic en 'OK' para refrescar la página.",
        pt: "Clique em 'OK' para atualizar a página."
    },
    Close: {
        en: "Close",
        es: "Cerrar",
        pt: "Fechar"
    },
    Code: {
        en: "Code",
        es: "Código",
        pt: "Código"
    },
    Columns: {
        en: "Columns",
        es: "Columnas",
        pt: "Colunas"
    },
    Conditions: {
        en: "Conditions",
        es: "Condiciones",
        pt: "Condições"
    },
    Configuration: {
        en: "Configuration",
        es: "Configuración",
        pt: "Configuração"
    },
    ContinueAnyway: {
        en: "Do you want to continue anyway?",
        es: "¿Deseas continuar de todos modos?",
        pt: "Deseja continuar assim mesmo?"
    },
    Copyright: {
        en: "@2018 InMotion GIT, Inc. All rights reserved",
        es: "@2018 InMotion GIT. Todos los derechos reservados",
        pt: "@2018 InMotion GIT. Todos os direitos reservados"
    },
    Created: {
        en: "Created",
        es: "Creado",
        pt: "Creado"
    },
    CreationDate: {
        en: "Creation date",
        es: "Fecha de creación"
    },
    CreationUser: {
        en: "Creation user",
        es: "Usuario creador"
    },
    DateAndTime: {
        en: "Date and time",
        es: "Fecha y hora",
        pt: "Data e hora"
    },
    Day: {
        en: "Day",
        es: "Día",
        pt: "Dia"
    },
    Deferred: {
        en: "Postponed",
        es: "Pospuesta",
        pt: "Adiada"
    },
    Delete: {
        en: "Delete",
        es: "Eliminar",
        pt: "Excluir"
    },
    DeleteYes: {
        en: "Yes",
        es: "Si",
        pt: "Sim"
    },
    Deleted: {
        en: "Deleted",
        es: "Eliminado",
        pt: "Excluído"
    },
    DeleteTask: {
        en: "Delete task",
        es: "Eliminar la tarea",
        pt: "Excluir a tarefa"
    },
    DeleteTaskSuccess: {
        en: "Task has been deleted successfully!",
        es: "¡Tarea eliminada con éxito!",
        pt: "Tarefa excluída com sucesso!"
    },
    DeleteWidgetSuccess: {
        en: "Widget has been deleted successfully!",
        es: "¡Widget eliminado con éxito!",
        pt: "Widget removido com sucesso!"
    },
    DragAndDropColumnsTitle: {
        en: "Drag the columns you want to select ",
        es: "Arrastre las columnas que desea seleccionar ",
        pt: "Arraste as colunas que deseja selecionar "
    },
    DragAndDropColumnsDescription: {
        en: "(the list of selected columns on the right are those that will be visible in the query).",
        es: "(la lista de columnas seleccionas a la derecha son las que estarán visibles en la consulta).",
        pt: "(a lista de colunas selecionadas à direita são aquelas que serão visíveis na consulta)."
    },
    Edit: {
        en: "Edit",
        es: "Editar"
    },
    EnableSemaphore: {
        en: "Enable semaphore",
        es: "Habilitar semáforo",
        pt: "Habilitar semáforo"
    },
    English: {
        en: "English",
        es: "Inglés",
        pt: "Inglês"
    },
    Error: {
        en: "Error",
        es: "Error"
    },
    ErrorForm: {
        en: "There has been an error sending the form",
        es: "Ha ocurrido un error enviando el formulario"
    },
    ExecuteTransaction: {
        en: "Execute transaction",
        es: "Ejecutar transacción",
        pt: "Executar transação"
    },
    Expiration: {
        en: "Expiration",
        es: "Vencimiento",
        pt: "Vencimento"
    },
    ExpirationDate: {
        en: "Expiration date",
        es: "Fecha de vencimiento"
    },
    ExpiredSession: {
        en: "The session has expired.",
        es: "La sesión ha expirado.",
        pt: "A sessão expirou."
    },
    Extension: {
        en: "Ext.",
        es: "Ext."
    },
    ExtensionNotAllowed: {
        en: "Extension not allowed.",
        es: "Extensión no permitida.",
        pt: "Extensão não permitida."
    },
    ExtensionOne: {
        en: "Ext. 1",
        es: "Ext. 1"
    },
    ExtensionTwo: {
        en: "Ext. 2",
        es: "Ext. 2"
    },
    Field: {
        en: "Field",
        es: "Campo",
        pt: "Campo"
    },
    From: {
        en: "From",
        es: "Desde",
        pt: "De"
    },
    LessThenOrEqualTo100: {
        en: "Enter a value less than or equal to 100.",
        es: "Ingrese un valor menor o igual a 100.",
        pt: "Insira um valor menor ou igual a 100."
    },
    Help: {
        en: "Help",
        es: "Ayuda",
        pt: "Ajuda"
    },
    Home: {
        en: "Home",
        es: "Inicio",
        pt: "Início"
    },
    Hour: {
        en: "Hour",
        es: "Hora",
        pt: "Hora"
    },
    InactivateUser: {
        en: "Inactivate user",
        es: "Inactivar usuario",
        pt: "Inativar usuário"
    },
    InactivateUserSuccess: {
        en: "User inactivated successfully!",
        es: "¡Usuario inactivado con éxito!",
        pt: "Usuário inativado com sucesso!"
    },
    InactiveUserIndicator: {
        en: "The user is already inactive in the period or in part of it!",
        es: "¡El usuario ya está inactivo en el período o en parte de él!",
        pt: "O usuário já está inativado no período ou em parte dele!"
    },
    InvalidExpirationDate: {
        es: "La fecha de expiración no puede ser igual o menor a hoy.",
        en: "The expiration date cannot be the same or less than today.",
        pt: "A data de validade não pode ser igual ou menor que hoje."
    },
    Language: {
        en: "Language",
        es: "Idioma",
        pt: "Idioma"
    },
    LineOfBusiness: {
        en: "Line of business",
        es: "Línea de negocio",
        pt: "Linha de negócio"
    },
    LogIn: {
        en: "Log In",
        es: "Conectar",
        pt: "Entrar"
    },
    LogOut: {
        en: "Log Out",
        es: "Desconectar",
        pt: "Sair"
    },
    Minute: {
        en: "Minute",
        es: "Minuto",
        pt: "Minuto"
    },
    NewPage: {
        en: "New page",
        es: "Nueva página",
        pt: "Nova página"
    },
    NewTask: {
        en: "New task",
        es: "Nueva tarea",
        pt: "Nova tarefa"
    },
    TaskTitle: {
        en: "Task",
        es: "Tarea",
        pt: "Tarefa"
    },
    No: {
        en: "No",
        es: "No",
        pt: "Não"
    },
    NoDataFound: {
        en: "No data found",
        es: "No se han encontrado registros"
    },
    NotAuthorized: {
        en: "Your user is not authorized to access this operation.",
        es: "Su usuario no está autorizado a acceder a esta operación.",
        pt: "Seu usuário não está autorizado a acessar essa operação."
    },
    NotInitiated: {
        en: "Not initiated",
        es: "No iniciada",
        pt: "Não iniciada"
    },
    NoteRequired: {
        en: "Note required",
        es: "Nota requerida"
    },
    NumberDays: {
        en: "Number of days without performing the task",
        es: "Cantidad de días sin atender la tarea",
        pt: "Quantidade de dias sen atender a tarefa"
    },
    Operator: {
        en: "Operator",
        es: "Operador",
        pt: "Operador"
    },
    Operation: {
        en: "Operation",
        es: "Operación",
        pt: "Operação"
    },
    Pending: {
        en: "Pending",
        es: "Pendiente",
        pt: "Pendente"
    },
    PhoneNumber: {
        en: "Phone n°",
        es: "N° telefónico"
    },
    PhoneNumberRequired: {
        en: "Phone number required",
        es: "Número telefónico requerido"
    },
    Phones: {
        en: "Phones",
        es: "Teléfonos"
    },
    Portuguese: {
        en: "Portuguese",
        es: "Portugués",
        pt: "Português"
    },
    PrivacyPolicy: {
        en: "Privacy Policy",
        es: "Política de privacidad",
        pt: "Política de Privacidade"
    },
    Profile: {
        en: "Profile",
        es: "Perfil",
        pt: "Perfil"
    },
    PasswordChange: {
        en: "Password Change",
        es: "Cambio de contraseña",
        pt: "Mudança de senha"
    },
    RecordOwnerRequired: {
        en: "Record owner is required",
        es: "Propietario es requerido"
    },
    RecordOwnerValid: {
        en: "Record owner is invalid",
        es: "Propietario no es válido"
    },
    RequiredField: {
        en: "The field is required.",
        es: "El campo es requerido.",
        pt: "O campo é requerido."
    },
    PageNameMaxLength: {
        en: "The maximum length of the title of the page is 45 characters.",
        es: "La longitud máxima del titulo de la página es de 45 caracteres.",
        pt: "O comprimento máximo do título da página é de 45 caracteres."
    },
    PageOrderMaxLength: {
        en: "The maximum value for ordering the pages is 999.",
        es: "El valor máximo para ordenar las páginas es de 999.",
        pt: "O valor máximo para encomendar as páginas é 999."
    },
    OnlyDigit: {
        en: "The field only allows numerics.",
        es: "El campo sólo permite numéricos.",
        pt: "O campo só permite valores numéricos."
    },
    ResourceNotFound: {
        en: "Resource not found.",
        es: "Recurso no encontrado.",
        pt: "Recurso não encontrado."
    },
    ResourceNotFoundDetail: {
        en: "Sorry, but the resource you are looking for has not been found.",
        es: "Lo sentimos, pero el recurso que está buscando no se ha encontrado.",
        pt: "Desculpe, mas o recurso que está procurando não foi encontrado."
    },
    ResourceUnauthorized: {
        en: "Unauthorized User.",
        es: "Usuario no autorizado.",
        pt: "Usuário não autorizado."
    },
    ResourceUnauthorizedDetail: {
        en: "Sorry, but the resource you are looking for has not been found.",
        es: "Su cuenta no posee los roles necesarios para poder acceder la página indicada.",
        pt: "Sua conta não possui as funções necessárias para acessar a página indicada."
    },
    Save: {
        en: "Save",
        es: "Guardar",
        pt: "Salvar"
    },
    SearchTransaction: {
        en: "Go to",
        es: "Ir a",
        pt: "Ir a"
    },
    Selected: {
        en: "Selected",
        es: "Seleccionado",
        pt: "Selecionado"
    },
    SelectedNodeConditions: {
        en: "(Conditions of the selected node)",
        es: "(Condiciones de la rama seleccionada)",
        pt: "(Condições do nó selecionado)"
    },
    Semaphore: {
        en: "Semaphore",
        es: "Semáforo",
        pt: "Semáforo"
    },
    SemaphoreConfig: {
        en: "Semaphore configuration",
        es: "Configuración del semáforo",
        pt: "Configuração do semáforo"
    },
    Spanish: {
        en: "Spanish",
        es: "Español",
        pt: "Espanhol"
    },
    Status: {
        en: "Status",
        es: "Estado",
        pt: "Estado"
    },
    ///Translate by Scheduler
    TransactionSearch: {
        en: "Click to search transaction",
        es: "Hacer clic para buscar transacción",
        pt: "Clique para pesquisar a transação"
    },
    Subject: {
        en: "Subject",
        es: "Asunto",
        pt: "Assunto"
    },
    SubjectTitle: {
        en: "Name of the appointment or a brief description",
        es: "Nombre de la cita o una breve descripción",
        pt: "Nome do compromisso ou uma breve descrição"
    },
    Location: {
        en: "Location",
        es: "Ubicación",
        pt: "Localização"
    },
    LocationTitle: {
        en: "Meeting place of appointment",
        es: "Lugar de encuentro de la cita",
        pt: "Lugar de reunião de compromisso"
    },
    StartingTime: {
        en: "Starting date",
        es: "Fecha de inicio",
        pt: "Data de início"
    },
    StartingTimeTitle: {
        en: "Time and date where the appointment begins",
        es: "Hora y fecha donde comienza la cita",
        pt: "Hora e data em que o compromisso começa"
    },
    EndingTime: {
        en: "Ending date",
        es: "Fecha de vencimiento",
        pt: "Data de vencimento"
    },
    EndingTimeTitle: {
        en: "Appointment end time and date",
        es: "Hora y fecha de finalización de la cita",
        pt: "Hora e data de término do compromisso"
    },
    Reminder: {
        en: "Reminder",
        es: "Recordatorio",
        pt: "Lembrete"
    },
    ReminderTitle: {
        en: "Time interval to remind the user (s) of the pending appointment",
        es: "Intervalo de tiempo para recordarle al usuario(s) la cita pendiente",
        pt: "Intervalo de tempo para lembrar o (s) usuário (s) do compromisso pendente"
    },
    IndividualTaskIndicator: {
        en: "The task can only be performed by one person",
        es: "La tarea sólo puede ser realizada por una persona",
        pt: "A tarefa pode ser realizada somente por uma pessoa"
    },
    IndividualTaskIndicatorTitle: {
        en: "If this option is enabled, only one user can accept the appointment as completed",
        es: "Si esta opción está habilitada solo un usuario puede aceptar como completada dicha cita",
        pt: "Se esta opção estiver habilitada, somente um usuário pode aceitar o compromisso como concluído"
    },
    WarningWhenCompleted: {
        en: "Send message when the task is completed",
        es: "Mandar mensaje cuando la tarea sea completada",
        pt: "Enviar mensagem quando a tarefa estiver concluída"
    },
    WarningWhenCompletedTitle: {
        en: "If this option is enabled an email will be sent to the user who assigned the appointment",
        es: "Si esta opción está habilitada se le enviará un correo electrónico al usuario que asignó la cita",
        pt: "Se essa opção for ativada, um email será enviado ao usuário que atribuiu o compromisso"
    },
    AllDayActivity: {
        en: "Activate all day",
        es: "Activar todo el dia",
        pt: "Ativar o dia todo"
    },
    AllDayActivityTitle: {
        en: "If this option is enabled, the user must make the appointment throughout the day (24 hours) and the start and end time will not be taken into account",
        es: "Si esta opción está habilitada el usuario deberá de realizar la cita durante todo el día (24 horas) y no se tomará en cuenta la hora de inicio y fin",
        pt: "Se essa opção for ativada, o usuário deverá fazer o compromisso durante o dia (24 horas) e as horas de início e término não serão consideradas"
    },
    OperatorEquals: {
        en: "Equals",
        es: "Es igual a",
        pt: "É igual a"
    },
    OperatorGreaterThan: {
        en: "Greater than",
        es: "Mayor que",
        pt: "Melhor que"
    },
    OperatorGreaterThanOrEqual: {
        en: "Greater than or equal",
        es: "Mayor o igual que",
        pt: "Maior que ou igual"
    },
    OperatorLessThan: {
        en: "Less than",
        es: "Menor que",
        pt: "Menos que"
    },
    OperatorLessThanOrEqual: {
        en: "Less than or equal",
        es: "Menor o igual que ",
        pt: "Menor ou igual"
    },
    Priority: {
        en: "Priority",
        es: "Prioridad",
        pt: "Prioridade"
    },
    PriorityTitle: {
        en: "This appointment will have preference according to the value. The possible values are standard, low and high",
        es: "Esta cita tendrá preferencia según el valor. Los posibles valores son estándar, baja y alta",
        pt: "Esta nomeação terá preferência de acordo com o valor. Os valores possíveis são padrão, baixo e alto"
    },
    Completed: {
        en: "Completed",
        es: "Completada",
        pt: "Concluída"
    },
    CompletedTitle: {
        en: "Calculation of the percentage of completion of the appointment",
        es: "Cálculo de porcentaje de finalización de la cita",
        pt: "Cálculo do percentual de conclusão da consulta"
    },
    See: {
        en: "See",
        es: "Ver"
    },
    TaskMailboxes: {
        en: "Mailboxes",
        es: "Bandejas de tareas",
        pt: "Diretórios de tarefas"
    },
    TaskMailboxesConfig: {
        en: "Mailboxes configuration",
        es: "Configuración de bandejas de tareas",
        pt: "Configuração de diretórios de tarefas"
    },
    TaskMailboxesConfigAdmin: {
        en: "Default mailboxes configuration",
        es: "Configuración de bandejas de tareas por defecto",
        pt: "Configuração de diretórios de tarefas"
    },
    Tasks: {
        en: "Tasks",
        es: "Tareas",
        pt: "Tarefas"
    },
    TransactionNotAllowed: {
        en: "The transaction is not allowed regarding its scheme.",
        es: "La transacción no está permitida para su esquema.",
        pt: "A transação não está permitida para o seu esquema."
    },
    TaskActionTitleDescription: {
        en: "Describe an action",
        es: "Describe una acción",
        pt: "Descreve uma ação"
    },
    TaskAssignmentDescription: {
        en: "Date and time of task creation",
        es: "Fecha y hora de creación de la tarea",
        pt: "Data e hora da criação da tarefa"
    },
    TaskCompletedDescription: {
        en: "Indicates the percentage completed",
        es: "Indica el porcentaje completado",
        pt: "Indica o percentual completado"
    },
    TaskDetail: {
        en: "See task detail",
        es: "Ver detalle de la tarea",
        pt: "Ver detalhe da tarefa"
    },
    TaskExpirationDescription: {
        en: "Date and time of task expiration",
        es: "Fecha y hora de vencimiento de la tarea",
        pt: "Data e hora de vencimento da tarefa"
    },
    TaskHistory: {
        en: "See task history",
        es: "Ver historia de la tarea",
        pt: "Ver historico da tarefa"
    },
    TaskIdentifier: {
        en: "Task identifier",
        es: "Identificador de la tarea",
        pt: "Identificador da tarefa"
    },
    TaskLineOfBusinessDescription: {
        en: "Identify the line of business",
        es: "Identifica la línea de negocio",
        pt: "Identifica a linha de negócio"
    },
    TaskPriorityDescription: {
        en: "Indicates the task importance",
        es: "Indica la importancia de la tarea",
        pt: "Indica a importância da tarefa"
    },
    TaskStatus: {
        en: "Task status",
        es: "Estado de la tarea",
        pt: "Estado da tarefa"
    },
    taskLongDescriptionTitle: {
        en: "A description of the task to be performed",
        es: "Una descripción de la tarea a realizar",
        pt: "Uma descrição da tarefa a ser executada"
    },
    TaskStatusTitle: {
        en: "Possible values are pending, completed, waiting, postponed, canceled",
        es: "Los posibles valores son pendiente, completada, en espera, pospuesta, cancelada",
        pt: "Os valores possíveis estão pendentes, concluídos, aguardando, adiados, cancelados"
    },
    TaskStatusDescription: {
        en: "Current task status",
        es: "Estado actual de la tarea",
        pt: "Estado atual da tarefa"
    },
    TaskSubjectDescription: {
        en: "Describe the task main subject",
        es: "Describe el asunto principal de la tarea",
        pt: "Descreve o assunto principal da tarefa"
    },
    TaskTypeDescription: {
        en: "Indicates the task type",
        es: "Indica el tipo de la tarea",
        pt: "Indica o tipo da tarefa"
    },
    TermsService: {
        en: "Terms of Service",
        es: "Términos de servicio",
        pt: "Termos de Serviço"
    },
    To: {
        en: "To",
        es: "Hasta",
        pt: "Até"
    },
    Type: {
        en: "Type",
        es: "Tipo",
        pt: "Tipo"
    },
    TypeOfPhone: {
        en: "Type of phone",
        es: "Tipo de teléfono"
    },
    //Depreciado se paso a general
    UnexpectedError: {
        en: "Something unexpected has occurred.",
        es: "Ha ocurrido algo inesperado.",
        pt: "Ocorreu algo inesperado."
    },
    UnexpectedErrorDetail: {
        en: "The server encountered something unexpected that didn't allow it to complete the request. We apologize.",
        es: "El servidor encontró algo inesperado que no le permitió completar la solicitud. Pedimos disculpas.",
        pt: "O servidor encontrou algo inesperado que não permitiu completar a solicitação. Pedimos desculpas."
    },
    Update: {
        en: "Update",
        es: "Actualizar",
        pt: "Editar"
    },
    UpdateDate: {
        en: "Update date",
        es: "Fecha de actualización"
    },
    UpdateUser: {
        en: "Update user",
        es: "Usario que actualiza"
    },
    Updated: {
        en: "Updated",
        es: "Actualizado",
        pt: "Editado"
    },
    UpdateTaskStatus: {
        en: "Change task status",
        es: "Cambiar estado de la tarea",
        pt: "Alterar status da tarefa"
    },
    UpdateTaskSuccess: {
        en: "Task has been updated successfully!",
        es: "¡Tarea actualizada con éxito!",
        pt: "Tarefa alterada com sucesso!"
    },
    User: {
        en: "User",
        es: "Usuario",
        pt: "Usuário"
    },
    Value: {
        en: "Value",
        es: "Valor",
        pt: "Valor"
    },
    Yes: {
        en: "Yes",
        es: "Sí",
        pt: "Sim"
    },
    Waiting: {
        en: "Waiting",
        es: "En espera",
        pt: "Em espera"
    },
    WaitingTime: {
        en: "Waiting time",
        es: "Tiempo de espera",
        pt: "Tempo de espera"
    },
    WaitingTimeDescription: {
        en: "Time since the task creation",
        es: "Tiempo transcurrido desde la creación de la tarea",
        pt: "Tempo decorrido desde a criacao da tarefa"
    },
    Week: {
        en: "Week",
        es: "Semana",
        pt: "Semana"
    },
    STSFailureMessage: {
        en: "The STS service is not available.",
        es: "No está disponible el servicio de STS.",
        pt: "O serviço STS não está disponível."
    },
    STSFailureTitle: {
        en: "The STS service is not available.",
        es: "No está disponible el servicio de STS.",
        pt: "O serviço STS não está disponível."
    },
    AutoLogInTitle: {
        en: "Start automatic session",
        es: "Inicio sesión automático",
        pt: "Iniciar sessão automática"
    },
    AutoLogInBody: {
        en: "It will load the current page with find to contextualize the user, it will be automatically recharged",
        es: "Se va cargar la página actual con find de contextualizar el usuario, se va recargara automaticamente",
        pt: "Ele irá carregar a página atual com find para contextualizar o usuário, ele será automaticamente recarregado"
    },
    ConfirmationDeletionPage: {
        en: "Are you sure you want to delete the page?",
        es: "¿Está usted seguro de querer eliminar la página?",
        pt: "Are you sure you want to delete the page?"
    },
    ConfirmationDeletionPageWithWigets: {
        en: "Are you sure you want to delete the page and the widgets it contains?",
        es: "¿Está usted seguro de querer eliminar la página y los widgets que contiene?",
        pt: "Tem certeza de que deseja excluir a página e os widgets que ela contém?"
    },
    ExpiredSessionTitle: {
        en: "Expired session",
        es: "Sesión vencida",
        pt: "Sessão expirada"
    },
    ExpiredSessionBody: {
        en: "Your session has expired, due to safety issues your user is automatically disconnected",
        es: "Su sesión ha vencido, por asunto de seguridad se desconecta automaticamente su usuario",
        pt: "Sua sessão expirou, devido a problemas de segurança, seu usuário é desconectado automaticamente"
    },
    //Depreciado se paso a general
    ExpirationSectionTitle: {
        en: "Session expiration",
        es: "Expiración de sesión",
        pt: "Expiração da sessão"
    },
    //Depreciado se paso a general
    ExpirationSectionBody: {
        en: "Your session will expire and it will redirect in",
        es: "Su sesión se va a vencer y se va a redireccionar al inicio en",
        pt: "Sua sessão expirará e será redirecionada"
    },
    //Depreciado se paso a general
    ExpirationSectionBtnCancel: {
        en: "Close and go to start",
        es: "Cerrar e ir a inicio",
        pt: "Feche e vá para começar"
    },
    //Depreciado se paso a general
    ExpirationSectionBtnSessionKeep: {
        en: "Keep session",
        es: "Mantener sesión",
        pt: "Feche e vá para começar"
    },
    //Depreciado se paso a general
    Seconds: {
        en: "seconds",
        es: "segundos",
        pt: "segundos"
    }
}

var translator = new function () {
    this.translatorObj = null;

    this.initialize = function (language) {
        translator.translatorObj = $('body').translate({ lang: language, t: dict });
    }

    this.changeLanguage = function (language) {
        translator.translatorObj.lang(language);
    }
};
function localStorageSupport(){return"localStorage"in window&&window.localStorage!==null}function SmoothlyMenu(){!$("body").hasClass("mini-navbar")||$("body").hasClass("body-small")?($("#side-menu").hide(),setTimeout(function(){$("#side-menu").fadeIn(400)},200)):$("body").hasClass("fixed-sidebar")?($("#side-menu").hide(),setTimeout(function(){$("#side-menu").fadeIn(400)},100)):$("#side-menu").removeAttr("style")}$(document).ready(function(){function n(){var i=$("body > #wrapper").height()-61,n,t;$(".sidebar-panel").css("min-height",i+"px");n=$("nav.navbar-default").height();t=$("#page-wrapper").height();n>t&&$("#page-wrapper").css("min-height",n+"px");n<t&&$("#page-wrapper").css("min-height",$(window).height()+"px");$("body").hasClass("fixed-nav")&&(n>t?$("#page-wrapper").css("min-height",n-60+"px"):$("#page-wrapper").css("min-height",$(window).height()-60+"px"))}$(this).width()<769?$("body").addClass("body-small"):$("body").removeClass("body-small");$("#side-menu").metisMenu();$(".collapse-link").on("click",function(){var n=$(this).closest("div.ibox"),t=$(this).find("i"),i=n.children(".ibox-content");i.slideToggle(200);t.toggleClass("fa-chevron-up").toggleClass("fa-chevron-down");n.toggleClass("").toggleClass("border-bottom");setTimeout(function(){n.resize();n.find("[id^=map-]").resize()},50)});$(".close-link").on("click",function(){var n=$(this).closest("div.ibox");n.remove()});$(".fullscreen-link").on("click",function(){var n=$(this).closest("div.ibox"),t=$(this).find("i");$("body").toggleClass("fullscreen-ibox-mode");t.toggleClass("fa-expand").toggleClass("fa-compress");n.toggleClass("fullscreen");setTimeout(function(){$(window).trigger("resize")},100)});$(".close-canvas-menu").on("click",function(){$("body").toggleClass("mini-navbar");SmoothlyMenu()});$("body.canvas-menu .sidebar-collapse").slimScroll({height:"100%",railOpacity:.9});$(".right-sidebar-toggle").on("click",function(){$("#right-sidebar").toggleClass("sidebar-open")});$(".sidebar-container").slimScroll({height:"100%",railOpacity:.4,wheelStep:10});$(".open-small-chat").on("click",function(){$(this).children().toggleClass("fa-comments").toggleClass("fa-remove");$(".small-chat-box").toggleClass("active")});$(".small-chat-box .content").slimScroll({height:"234px",railOpacity:.4});$(".check-link").on("click",function(){var n=$(this).find("i"),t=$(this).next("span");return n.toggleClass("fa-check-square").toggleClass("fa-square-o"),t.toggleClass("todo-completed"),!1});$(".navbar-minimalize").on("click",function(n){n.preventDefault();$("body").toggleClass("mini-navbar");SmoothlyMenu()});n();$(window).bind("load",function(){$("body").hasClass("fixed-sidebar")&&$(".sidebar-collapse").slimScroll({height:"100%",railOpacity:.9})});$(window).scroll(function(){$(window).scrollTop()>0&&!$("body").hasClass("fixed-nav")?$("#right-sidebar").addClass("sidebar-top"):$("#right-sidebar").removeClass("sidebar-top")});$(window).bind("load resize scroll",function(){$("body").hasClass("body-small")||n()});$("[data-toggle=popover]").popover();$(".full-height-scroll").slimscroll({height:"100%"})});$(window).bind("resize",function(){$(this).width()<769?$("body").addClass("body-small"):$("body").removeClass("body-small")});$(document).ready(function(){if(localStorageSupport()){var t=localStorage.getItem("collapse_menu"),i=localStorage.getItem("fixedsidebar"),r=localStorage.getItem("fixednavbar"),u=localStorage.getItem("boxedlayout"),f=localStorage.getItem("fixedfooter"),n=$("body");i==="on"&&(n.addClass("fixed-sidebar"),$(".sidebar-collapse").slimScroll({height:"100%",railOpacity:.9}));t==="on"&&(n.hasClass("fixed-sidebar")?n.hasClass("body-small")||n.addClass("mini-navbar"):n.hasClass("body-small")||n.addClass("mini-navbar"));r==="on"&&($(".navbar-static-top").removeClass("navbar-static-top").addClass("navbar-fixed-top"),n.addClass("fixed-nav"));u==="on"&&n.addClass("boxed-layout");f==="on"&&$(".footer").addClass("fixed")}});