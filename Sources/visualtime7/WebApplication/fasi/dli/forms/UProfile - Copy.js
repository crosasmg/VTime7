var UProfileSupport = new function () {
    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#UProfileFormId').val(),
            userNameTemporal: $('#userNameTemporal').val(),
            userEmailTemporal: $('#userEmail').val(),
            FrontOfficeMembershipUserFirstName: $('#FirstName').val(),
            FrontOfficeMembershipUserSurName: $('#SurName').val(),
            FrontOfficeMembershipUserLastName: $('#LastName').val(),
            FrontOfficeMembershipUserSecondLastName: $('#SecondLastName').val(),
            FrontOfficeMembershipUserDateofBirth: generalSupport.DatePickerValueInputToObject('#DateofBirth'),
            FrontOfficeMembershipUserGender: $('#Gender').val(),
            FrontOfficeMembershipUserAddressHome: $('#Address').val(),
            countryAuxiliar: parseInt(0 + $('#Country').val(), 10),
            FrontOfficeMembershipUserCity: $('#City').val(),
            FrontOfficeMembershipUserState: $('#Status').val(),
            FrontOfficeMembershipUserTelephoneNumber: generalSupport.NumericValue('#TelephoneNumber', 0, 999999999),
            FrontOfficeMembershipUserLanguageID: parseInt(0 + $('#LanguageID').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#UProfileFormId').val(data.InstanceFormId);
        $('#UserName').html(data.FrontOfficeMembershipUserUserName);
        $('#Email').html(data.FrontOfficeMembershipUserEmail);
        $('#userNameTemporal').val(data.userNameTemporal);
        $('#userEmail').val(data.userEmailTemporal);
        $('#FirstName').val(data.FrontOfficeMembershipUserFirstName);
        $('#SurName').val(data.FrontOfficeMembershipUserSurName);
        $('#LastName').val(data.FrontOfficeMembershipUserLastName);
        $('#SecondLastName').val(data.FrontOfficeMembershipUserSecondLastName);
        $('#DateofBirth').val(generalSupport.ToJavaScriptDateCustom(data.FrontOfficeMembershipUserDateofBirth, generalSupport.DateFormat()));
        $('#Gender').data('oldValue', data.FrontOfficeMembershipUserGender);
        $('#Gender').val(data.FrontOfficeMembershipUserGender);
        $('#Address').val(data.FrontOfficeMembershipUserAddressHome);
        $('#City').val(data.FrontOfficeMembershipUserCity);
        $('#Status').val(data.FrontOfficeMembershipUserState);
        AutoNumeric.set('#TelephoneNumber', data.FrontOfficeMembershipUserTelephoneNumber);
        $('#LastLoginDater').html(generalSupport.ToJavaScriptDateCustom(data.FrontOfficeMembershipUserLastLoginDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#LastActivityDate').html(generalSupport.ToJavaScriptDateCustom(data.FrontOfficeMembershipUserLastActivityDate, generalSupport.DateFormat() + ' HH:mm:ss'));
        $('#LastLockoutDate').html(generalSupport.ToJavaScriptDateCustom(data.FrontOfficeMembershipUserLastLockoutDate, generalSupport.DateFormat() + ' HH:mm:ss'));

        UProfileSupport.LookUpForCountry(data.countryAuxiliar, source);
        UProfileSupport.LookUpForLanguageID(data.FrontOfficeMembershipUserLanguageID, source);

        if (data.UserSecurityTrace_UserSecurityTrace !== null)
            $('#UserSecurityTraceTbl').bootstrapTable('load', data.UserSecurityTrace_UserSecurityTrace);
    };

    this.ControlBehaviour = function () {
        new AutoNumeric('#TelephoneNumber', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });

        $('#DateofBirth_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateofBirth_group');
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                if (source == 'Initialization')
                    UProfileSupport.ObjectToInput(data.d.Data.Instance, source);
                else
                    UProfileSupport.ObjectToInput(data.d.Data, source);

            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/UProfileActions.aspx/Initialization", true,
            JSON.stringify({
                id: $('#UProfileFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }

                $("#UserSecurityTraceTblPlaceHolder").replaceWith('<table id="UserSecurityTraceTbl"><caption data-i18n="app.form.UserSecurityTrace_Title" >Records</caption></table>');
                UProfileSupport.UserSecurityTraceTblSetup($('#UserSecurityTraceTbl'));

                UProfileSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#UProfileFormId').val());
            });
    };

    this.ControlActions = function () {
        $('#btnCan').click(function (event) {
            var formInstance = $("#UProfileMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnCan'));
                btnLoading.start();
                window.open('/fasi/default.aspx', '_blank');
                btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnSa').click(function (event) {
            var formInstance = $("#UProfileMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnSa'));
                btnLoading.start();

                app.core.AsyncWebMethod("/fasi/dli/forms/UProfileActions.aspx/btnSaClick", false,
                    JSON.stringify({
                        instance: UProfileSupport.InputToObject()
                    }),
                    function (data) {
                        btnLoading.stop();

                        UProfileSupport.ActionProcess(data, 'btnSaClick');
                    },
                    function () {
                        btnLoading.stop();
                    });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("DateofBirth_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#DateofBirth') > new Date()) {
                    result = false;
                }
            }
            return result;
        });

        $("#UProfileMainForm").validate({
            errorPlacement: function (error, element) {
                var name = $(element).attr("name");
                var $obj = $("#" + name + "_validate");
                if ($obj.length) {
                    error.appendTo($obj);
                }
                else {
                    error.insertAfter(element);
                }
            },

            rules: {
                UserName: {
                    maxlength: 15
                },
                Email: {
                    maxlength: 15
                },
                userNameTemporal: {
                    maxlength: 15
                },
                userEmail: {
                    maxlength: 10
                },
                FirstName: {
                    maxlength: 15
                },
                SurName: {
                    maxlength: 15
                },
                LastName: {
                    maxlength: 15
                },
                SecondLastName: {
                    maxlength: 15
                },
                DateofBirth: {
                    DatePicker: true
                    ,
                    DateofBirth_Validate1: true
                },
                Gender: {
                },
                Country: {
                },
                City: {
                    maxlength: 15
                },
                Status: {
                    maxlength: 15
                },
                TelephoneNumber: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999
                },
                LanguageID: {
                },
                LastLoginDater: {
                    DatePicker: true
                },
                LastActivityDate: {
                    DatePicker: true
                },
                LastLockoutDate: {
                    DatePicker: true
                }
            },
            messages: {
                UserName: {
                    maxlength: $.i18n.t('app.validation.UserName.maxlength')
                },
                Email: {
                    maxlength: $.i18n.t('app.validation.Email.maxlength')
                },
                userNameTemporal: {
                    maxlength: $.i18n.t('app.validation.userNameTemporal.maxlength')
                },
                userEmail: {
                    maxlength: $.i18n.t('app.validation.userEmail.maxlength')
                },
                FirstName: {
                    maxlength: $.i18n.t('app.validation.FirstName.maxlength')
                },
                SurName: {
                    maxlength: $.i18n.t('app.validation.SurName.maxlength')
                },
                LastName: {
                    maxlength: $.i18n.t('app.validation.LastName.maxlength')
                },
                SecondLastName: {
                    maxlength: $.i18n.t('app.validation.SecondLastName.maxlength')
                },
                DateofBirth: {
                    DatePicker: $.i18n.t('app.validation.DateofBirth.DatePicker')
                    ,
                    DateofBirth_Validate1: 'The value cant be greater than the date of the current day'
                },
                Gender: {
                },
                Country: {
                },
                City: {
                    maxlength: $.i18n.t('app.validation.City.maxlength')
                },
                Status: {
                    maxlength: $.i18n.t('app.validation.Status.maxlength')
                },
                TelephoneNumber: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999'
                },
                LanguageID: {
                },
                LastLoginDater: {
                    DatePicker: $.i18n.t('app.validation.LastLoginDater.DatePicker')
                },
                LastActivityDate: {
                    DatePicker: $.i18n.t('app.validation.LastActivityDate.DatePicker')
                },
                LastLockoutDate: {
                    DatePicker: $.i18n.t('app.validation.LastLockoutDate.DatePicker')
                }
            }
        });
    };
    this.LookUpForCountry = function (defaultValue, source) {
        var ctrol = $('#Country');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/UProfileActions.aspx/LookUpForCountry", false,
                JSON.stringify({ id: $('#UProfileFormId').val() }),
                function (data) {
                    ctrol.children().remove();

                    $.each(data.d.Data, function () {
                        ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                    });
                    if (defaultValue !== null)
                        ctrol.val(defaultValue);
                    else
                        ctrol.val(0);

                    if (source !== 'Initialization')
                        ctrol.change();
                });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForLanguageID = function (defaultValue, source) {
        var ctrol = $('#LanguageID');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'fasi/v1/Language?languageId=2',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                },
                success: function (data) {
                    ctrol.children().remove();

                    data.forEach(function (element) {
                        ctrol.append($('<option />').val(element.Code).text(element.Description));
                    });

                    if (defaultValue !== null)
                        ctrol.val(defaultValue);
                    else
                        ctrol.val(0);
                    if (source !== 'Initialization')
                        ctrol.change();
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };

    this.UserSecurityTraceTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            columns: [{
                field: 'EffectDate',
                title: 'Effect date',
                formatter: 'tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Host',
                title: 'Host',
                sortable: false,
                halign: 'center'
            }, {
                field: 'State',
                title: 'State',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Result',
                title: 'Result',
                sortable: false,
                halign: 'center'
            }]
        });
        tableHelperSupport.Translate('#UserSecurityTraceTbl', '#UserSecurityTraceTbl');
    };

    this.Init = function () {
        moment.locale(app.user.languageName);

        generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
            if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
                masterSupport.setPageTitle($.i18n.t('app.title'));
            }

            UProfileSupport.ValidateSetup();
        });

        UProfileSupport.ControlBehaviour();
        UProfileSupport.ControlActions();

        UProfileSupport.Initialization();
    };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        IsConnected: true,
        Element: $("#UProfileMainForm"),
        CallBack: UProfileSupport.Init
    });
});