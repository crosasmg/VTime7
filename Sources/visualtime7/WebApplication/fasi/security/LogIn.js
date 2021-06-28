var urlBase = '/fasi/security/LogIn.aspx';
var isolated;
var urlSource = '';
var settings = {};

IsAuthenticated();

$(function ($) {
    settings = generalSupport.Settings();
    SettingUI(settings);
});

$(document).ready(function () {
    $("#btnLogIn").click(function (e) {
        LogIn(e);
        e.preventDefault();
    });

    $('#Password').keyup(function (e) {
        if (e.keyCode === 13) {
            LogIn(e);
        }
        e.preventDefault();
    });

    var callback = function (data) {
        if (!localStorage.getItem('languageName')) {
            localStorage.setItem('languageName', app.security.UserContext().languageName);
        }

        if ($('#UserNameHiddenField').val() == undefined || $('#UserNameHiddenField').val() == '') {
            $('#ContinerCompanyId').hide();
        }

        $('#ContinergRecaptcha').hide();
        isolated = generalSupport.GetParameterByName('isolated');
        if (isolated === null) {
            isolated = true;
        }
        else {
            isolated = isolated.toLowerCase() === 'true';
        }
        urlSource = generalSupport.GetParameterByName('urlsource');
        EmailInCookie();

        //Translate Configuration Start
        $.i18n.init({
            resGetPath: 'locales/__lng__.LogIn.json',
            load: 'unspecific',
            fallbackLng: false,
            lng: localStorage.getItem('languageName') ? localStorage.getItem('languageName') : constants.defaultLanguageName
        }, function (t) {
            $('#app').i18n();
            ValidateSetup();

            $("#UserName").attr("placeholder", Message('app.form.Email'));
        });
        //Translate Configuration End

        $("input:checkbox").on('change', function () {
            if (!$(this).is(':checked'))
                EmailClean();
        });
    };

    app.security.IsALive(undefined, callback);

    if ($('#UserNameHiddenField').val() !== undefined && $('#UserNameHiddenField').val() !== '') {
        $('#UserName').val($('#UserNameHiddenField').val());
        $('#UserName').prop("disabled", true);
        $('#Password').prop("disabled", true);

        $('#ContinerCompanyId').show();
        PupulationCompanyCombox();
        $('#btnLogIn').prop("disabled", false);
    }
});

function SettingUI(setting) {
    switch (setting.Security.Mode) {
        case "Windows":
        case "Database":
            $('#aPasswordRecovery').show();
            $('#aNothaveaccount').show();
            $('#btnregister').show();
            $('#divRememberMe').show();

            break;

        case "HeaderAuthentication":
        case "ActiveDirectory":
        case "Sesame":
            $('#aPasswordRecovery').hide();
            $('#aNothaveaccount').hide();
            $('#btnregister').hide();
            $('#divRememberMe').hide();
            if (setting.Security.Mode == "Sesame") {
                $('#UserName').prop("disabled", true);
                $('#Password').prop("disabled", true);
            }
            break;

        default:
    }
}

function IsAuthenticated() {
    $.ajax({
        type: "GET",
        url: '/fasi/wmethods/User.aspx/IsAuthenticated',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d) window.location.href = constants.defaultPage;
        }
    });
}

function LogIn(e) {
    var formInstance = $("#loginForm");
    var fvalidate = formInstance.validate();

    if (formInstance.valid()) {
        if (!isolated) {
            window.parent.ShowLoadingPanel(true);
        }
        var btnLoading = Ladda.create(document.querySelector('#btnLogIn'));
        btnLoading.start();

        if ($('#UserNameHiddenField').val() == undefined && $('#UserNameHiddenField').val() !== '') {
            SetSetting(false);
        }

        var urlAction = urlBase + "/LogIn";
        var isVisibleCompany = !$('#ContinerCompanyId').is(':hidden');
        var companyId = 0;

        if (isVisibleCompany === true) {
            companyId = $("#CompanyId option:selected").val();
        }

        if (companyId == undefined) {
            companyId = 0;
        }

        var param = JSON.stringify({
            user: $('#UserName').val(),
            password: $('#Password').val(),
            isVisibleCompany: isVisibleCompany,
            companyId: companyId,
            languageId: localStorage.getItem('languageId') ? localStorage.getItem('languageId') : constants.defaultLanguageId
        });
        $.ajax({
            url: urlAction,
            data: param,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                btnLoading.stop();
                if (data.d.State === true) {
                    SetSetting(true);

                    $('#ContinerCompanyId').hide();

                    if (data.d.Message === "") {
                        if (data.d.IsAuthenticated === true) {
                            app.security.UserClean();
                            localStorage.endTime = undefined;
                            localStorage.IsShow = false;

                            if ($('#RememberMe').is(":checked")) {
                                $.cookie('EmailAddress', $('#UserName').val());
                            }

                            if (data.d.ShowStartUpMessage === true) {
                                window.location.href = data.d.Url;
                            } else {
                                if (!urlSource) {
                                    window.location.href = data.d.Url;
                                } else {
                                    window.location.href = urlSource;
                                }
                            }
                        } else {
                            if (data.d.IsMultiCompany === true) {
                                $('#ContinerCompanyId').show();
                                PupulationCompanyCombox();
                                $('#btnLogIn').prop("disabled", false);
                            }
                            else {
                                $('#ContinerCompanyId').hide();
                                SetSetting(false);
                            }
                        }
                    }
                    else {
                        SetSetting(false);
                        if (data.d.RecaptchaShow) {
                            $('#ContinergRecaptcha').show();
                            securitySupport.CreateCaptcha(function () {
                                $('#btnLogIn').prop("disabled", false);
                            });
                            $('#btnLogIn').prop("disabled", true);
                        } else {
                            notification.swal.error('', data.d.Message);
                            SettingUI(data.d.Settings);
                            $('#btnLogIn').prop("disabled", false);
                        }
                    }
                }
                else {
                    if (!isolated) {
                        window.parent.ShowLoadingPanel(false);
                    }
                    $('#btnLogIn').prop("disabled", false);
                    notification.swal.error('', data.d.Message);
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                btnLoading.stop();
                SetSetting(true);
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }
    else
        generalSupport.NotifyErrorValidate(fvalidate);
}

function PupulationCompanyCombox() {
    var urlAction = urlBase + "/CompanyLookUp";
    $.ajax({
        url: urlAction,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
        },
        success: function (data) {
            if ($('#CompanyId option').size() === 0) {
                if (data.d.length !== 0) {
                    data.d.forEach(function (element) {
                        $('#CompanyId').append($("<option/>", {
                            value: element.Identification,
                            text: element.Name
                        }));
                    });
                    $('#CompanyId option')[0].selected = true;
                }
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function SetSetting(isEnable) {
    $('#Password').prop("disabled", isEnable);
    $('#UserName').prop("disabled", isEnable);
    $('#btnLogIn').prop("disabled", isEnable);

    if (!isolated) {
        window.parent.HideRegisterOption();
    }
}

function EmailClean() {
    $('#UserName').val("");
    $.removeCookie('EmailAddress', { path: '/fasi/security' });
    $.removeCookie('EmailAddress', { path: '' });
}

function EmailInCookie() {
    if ($('#UserNameHiddenField').val() == undefined || $('#UserNameHiddenField').val() == '') {
        var value = $.cookie('EmailAddress');

        if (value !== undefined) {
            $('#UserName').val(value);
            $('#Password').focus();
            $('#RememberMe').prop('checked', true);
        }
        else {
            $('#UserName').val('');
            $('#Password').val('');
            $('#RememberMe').prop('checked', false);
            $('#UserName').focus();
        }
    }
}

function Message(key) {
    var result = "";
    switch (settings.Security.Mode) {
        case "Windows":
        case "Database":
            result = $.i18n.t(key);
            break;

        case "HeaderAuthentication":
        case "ActiveDirectory":
        case "Sesame":
            result = $.i18n.t(key + 'int');
            break;

        default:
    }
    return result;
}

function ValidateSetup() {
    $("#loginForm").validate({
        rules: {
            UserName: {
                required: true,
                email: {
                    depends: function (element) {
                        switch (settings.Security.Mode) {
                            case "Windows":
                            case "Database":
                                return true;

                            case "HeaderAuthentication":
                            case "ActiveDirectory":
                            case "Sesame":
                                return false;

                            default:
                                return true;
                        }
                    }
                }
            },
            Password: {
                required: true
            }
        },
        messages: {
            UserName: {
                required: function () {
                    return Message('app.required.username');
                },
                email: function () {
                    return Message('app.required.Email');
                }
            },
            Password: {
                required: $.i18n.t('app.required.password')
            }
        }
    });
}