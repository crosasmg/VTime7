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