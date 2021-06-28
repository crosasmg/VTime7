app.core = (function () {

    AjaxCall = function (type, url, token, data, async, overlay, success) {
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
            beforeSend: function (xhr) {
                if (token)
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
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
        if (generalSupport.user === null || generalSupport.user === undefined) {
            generalSupport.getUser();
        }
        var result = '';
        $.ajax({
            type: "GET",
            url: constants.fasiApi.base + 'fasi/v1/ErrorMessage?code=' + code + '&languageId=' + localStorage.getItem('languageId'),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
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
        console.error({ code: code, detail: message });
        if (code === '' || code === undefined) {
            code = '1';
        }
        var messageByCatalog = ErrorDescription(code);
        if (messageByCatalog !== '') {
            notification.swal.error('', messageByCatalog);
        } else {
            notification.swal.error('', message);
        }
    };

    return {
        AsyncWebMethod: function (url, overlay, data, success) {
            return AjaxCall('POST', url, false, data, true, overlay, success);
        },
        SyncWebMethod: function (url, overlay, data, success) {
            return AjaxCall('POST', url, false, data, false, overlay, success);
        },
        AsyncGet: function (url, token, overlay, data, success) {
            return AjaxCall('GET', url, token, data, true, overlay, success);
        },
        SyncGet: function (url, token, overlay, data, success) {
            return AjaxCall('GET', url, token, data, false, overlay, success);
        }
    };

})();

var generalSupport = new function () {
    Array.prototype.contains = function (obj) {
        var i = this.length;
        while (i--) {
            if (this[i] === obj) {
                return true;
            }
        }
        return false;
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
                        if (value.indexOf("Date") !== -1) {
                            elementSource[prop] = generalSupport.ToJavaScriptDate2(value);
                        }
                    }
                }
            });
        }
        return source;
    };

    this.SessionContext = function () {
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }
        return {
            languageId: localStorage.getItem('languageId'),
            clientId: generalSupport.UserContext().clientId,
            producerId: generalSupport.UserContext().producerId
        };
    };

    this.UserContext = function () {
        if (generalSupport.user === undefined) {
            if (typeof masterSupport !== 'undefined')
                if (masterSupport !== undefined && masterSupport.user !== undefined) {
                    generalSupport.user = {
                        userId: masterSupport.user.userId,
                        isAnonymous: masterSupport.user.isAnonymous,
                        token: masterSupport.user.token,
                        companyId: masterSupport.user.companyId,
                        schemeCode: masterSupport.user.schemeCode,
                        clientId: masterSupport.user.clientId,
                        producerId: masterSupport.user.producerId
                    };
                }
                else
                    generalSupport.getUser();
            else
                generalSupport.getUser();
        }
        return generalSupport.user;
    };

    this.URLDateValue = function (key) {
        var value = generalSupport.URLValue(key);
        if (value !== null)
            value = moment(value, 'DD/MM/YYYY').format('DD/MM/YYYY');
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

   
    this.ErrorHandler = function (jqXHR, textStatus, errorThrown) {
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
        if (generalSupport.user === null || generalSupport.user === undefined) {
            this.getUser();
        }
        var result = "";
        $.ajax({
            type: "GET",
            url: constants.fasiApi.base + 'fasi/v1/ErrorMessage?code=' + code + '&languageId=' + localStorage.getItem('languageId'),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
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

    this.NotifyErrorValidate = function (fvalidate) {
        var errorHtml = '';
        if (fvalidate.numberOfInvalids() > 1)
            errorHtml = 'Existen ' + fvalidate.numberOfInvalids() + ' errores';
        else
            errorHtml = 'Existe ' + fvalidate.numberOfInvalids() + ' error';

        errorHtml += ' que necesitan su atención';

        notification.toastr.error('', errorHtml);

        var count = fvalidate.errorList.length;
        for (var i = 0; i < count; i++) {
            console.log(fvalidate.errorList[i]['message']);
        }
    };

    this.ToJavaScriptDate2 = function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        if (results !== null) {
            var dt = new Date(parseFloat(results[1]));

            if (dt.getYear() <= 1)
                null;
            else
                return new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
        }
        else {
            return moment(value, 'YYYY-MM-DD').toDate();
        }
    };

    this.ToJavaScriptDateCustom = function (value, format) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt;

        if (results !== null) {
            dt = new Date(parseFloat(results[1]));
            var x = moment(dt).utc().format(format);
            if (dt.getYear() <= 1)
                return '';
            else
                return x;
        }
        else {
            var dateFormated;
            var dateValue = moment(value);
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
    };

    this.ServerBehavior = function (DataBehavior) {
        if (DataBehavior.controlbehavior !== null) {
            $.each(DataBehavior.controlbehavior, function () {
                switch (this['property']) {
                    case 'hide':
                        $('#' + this['id'] + 'Label').toggleClass('hidden', true);
                        if ($('#' + this['id']).length > 0)
                            $('#' + this['id']).toggleClass('hidden', true);
                        else
                            $('input:radio[name=' + this['id'] + ']').parent().toggleClass('hidden', true);

                        break;
                    case 'show':
                        $('#' + this['id'] + 'Label').toggleClass('hidden', false);
                        if ($('#' + this['id']).length > 0)
                            $('#' + this['id']).toggleClass('hidden', false);
                        else
                            $('input:radio[name=' + this['id'] + ']').parent().toggleClass('hidden', false);
                        break;
                    case 'disabled':
                        if ($('#' + this['id']).is('div'))
                            $('#' + this['id'] + ' :input').prop('disabled', true);
                        else
                            if ($('#' + this['id']).length > 0)
                                $('#' + this['id']).prop('disabled', true);
                            else
                                $('input:radio[name=' + this['id'] + ']').prop('disabled', true);
                        break;
                    case 'enabled':
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
                }
            });
        }
        if (DataBehavior.redirect !== null && DataBehavior.redirect !== '')
            if (DataBehavior.redirectsetting === null)
                window.location.href = DataBehavior.redirect;
            else 
                window.open(DataBehavior.redirect.replace(".aspx", "Popup.html"), '_blank', DataBehavior.redirectsetting);        
        if (DataBehavior.notify !== null && DataBehavior.notify !== '')
            if (DataBehavior.notify.splash !== null && DataBehavior.notify.splash !== '')
                notification.splash.info('', DataBehavior.notify.splash);
            else
                if (DataBehavior.notify.popup !== null)
                    $.each(DataBehavior.notify.popup, function (index, value) {
                        notification.swal.info('', value);
                    });
                else
                    if (DataBehavior.notify.messages !== null)
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

    this.getUser = function () {
        // Obtiene el código del usuario
        $.ajax({
            type: "GET",
            url: '/fasi/wmethods/User.aspx/GetUserInformation',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            success: function (data) {
                generalSupport.user = {
                    userName: data.d.username,
                    userId: data.d.userId,
                    isAnonymous: data.d.isAnonymous,
                    token: data.d.token,
                    companyId: data.d.companyId,
                    schemeCode: data.d.schemeCode,
                    clientId: data.d.clientId,
                    producerId: data.d.producerId
                };

                // Si es usuario anónimo se crea un nuevo usuário
                if (generalSupport.user.isAnonymous) {
                    $.ajax({
                        type: "POST",
                        url: constants.fasiApi.members + 'UserAnonymous',
                        async: false,
                        cache: false,
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                        },
                        success: function (data) {
                            generalSupport.user.userId = data.Data;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                        }
                    });
                }
            }
        });
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

    this.Select2Load = function (name, data, code, description, defaultValue, templateResultMethod, templateSelectionMethod) {
        var ctrol = $('#' + name);

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
            ctrol.append($('<option />').val(this[code]).text(this[description]));
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
                templateSelection: fnTemplateSelectionMethod
            });
        } else {
            ctrol.select2({
                data: data,
                multiple: true,
                allowClear: true
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

    this.NotifyFail = function (message, Code) {
        console.error({ code: Code, detail: message });
        if (Code === '' || Code === undefined) {
            Code = '1';
        }
        var messageByCatalog = this.ErrorDescription(Code);
        if (messageByCatalog !== '') {
            notification.swal.error('', messageByCatalog);
        } else {
            notification.swal.error('', message);
        }
    };

    this.TranslateInit = function (name, callback) {
        //Translate Configuration Start
        $.i18n.init({
            resGetPath: 'locales/__lng__.' + name + '.json',
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

    this.ExtendValidators = function () {
        $.validator.addMethod("AutoNumericRequired",
            function (value, element) { return this.optional(element) || AutoNumeric.getNumber('#' + element.id) !== 0; }, "El campo es requerido");
    };
};
var ajaxJsonHelper = new function () {
    this.ajax = function (url, type, data, success, error, complete, async) {
        // Si no está definido la function de error, entonces le agrega una generica
        if (!error) {
            error = function (jqXHR, textStatus, errorThrown) {
                // Si el error es de no autenticado
                $.LoadingOverlay("hide");
                if (jqXHR.status == 401)
                    notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { masterSupport.logout(); });
                else
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
                xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
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
    //Valida los roles de los usuario de estar asignado deja entrar a la planilla
    this.ValidateAccessRoles = function (roles) {
        var urlUnaunthorizedUser = '/fasi/dli/forms/UnauthorizedUser.aspx';
        $.ajax({
            type: "POST",
            async: false,
            url: constants.fasiApi.base + 'Authentication/v1/ValidateAccessRoles',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(roles),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.UserContext().token);
            },
            success: function (data) {
                if (!data.Successfully) {
                    var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
                    if (InMotionGITToken !== null) {
                        var resultValidateRole = securitySupport.ValidateRoleByToken(InMotionGITToken, roles);
                        if (resultValidateRole) {
                            var result = securitySupport.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
                            if (result) {
                                var title = dict.AutoLogInTitle[generalSupport.LanguageName()];
                                var body = dict.AutoLogInBody[generalSupport.LanguageName()];

                                notification.swal.success(
                                    title,
                                    body,
                                    3000, function () {
                                        window.location.href = window.location.href;
                                    });
                            } else {
                                window.location = urlUnaunthorizedUser;
                            }
                        } else {
                            window.location = urlUnaunthorizedUser;
                        }
                    }
                    else {
                        window.location = urlUnaunthorizedUser;
                    }
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

    //Define si esta o no conectado el usuario en el aplicación
    this.IsConnected = function () {
        if (generalSupport.UserContext().isAnonymous)
            window.location = constants.defaultPage;
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
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.UserContext().token);
                },
                success: function (data) {
                    if (data.Successfully === true) {
                        notification.control.success(null, $.i18n.t('app.form.RecoverPasswordSuccessfully'));
                    }
                    else {
                        notification.control.error(null, $.i18n.t('app.form.RecoverPasswordIncorrect'));
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
    this.Logout = function (userId, IsRedirect) {
        ajaxJsonHelper.post(constants.fasiApi.members + 'UserLogOff', null,
            function (data) {
                if (data && data.Successfully) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        data: JSON.stringify({ UserId: userId }),
                        url: '/fasi/wmethods/User.aspx/LogOut',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (IsRedirect) {
                                window.location.replace(constants.defaultPage);
                            }
                        }
                    });
                }
            }, null, null, false);
    };

};
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
            swal(title, message, "info");
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
                title: dict.DeleteRowConfirmation[localStorage.getItem("languageName")],
                text: null,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.CancelNo[localStorage.getItem("languageName")],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.DeleteYes[localStorage.getItem("languageName")],
                closeOnConfirm: true
            }, callback);
        },
        deleteConfirmation: function (text, callback) {
            swal({
                title: dict.AreYouSure[localStorage.getItem("languageName")],
                text: text,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.Cancel[localStorage.getItem("languageName")],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.Delete[localStorage.getItem("languageName")],
                closeOnConfirm: true
            }, callback);
        },
        continueConfirmation: function (title, message, callback) {
            swal({
                title: title,
                text: message,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.No[localStorage.getItem("languageName")],
                confirmButtonColor: "#18a689",
                confirmButtonText: dict.Yes[localStorage.getItem("languageName")],
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