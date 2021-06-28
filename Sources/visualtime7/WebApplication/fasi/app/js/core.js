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