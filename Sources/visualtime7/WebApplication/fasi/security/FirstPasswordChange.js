var urlBase = '/fasi/wmethods/PasswordChange.aspx';
var settings = {};
var message = "";

$.validator.addMethod("PasswordStrength", function (value, element) {
    var isSuccess = false;
    var $valid = false;

    var url = constants.fasiApi.members + 'ValidatePassword?UserName=' + app.user.userName + '&Password=' + value + '&languageId=' + constants.defaultLanguageId;

    $.ajax({
        type: "GET",
        url: url,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            'Accept-Language': generalSupport.LanguageName()
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
        },
        success: function (data) {
            if (data.Successfully) {
                $valid = true;
                message = "";
            } else {
                $valid = false;
                message = data.Reason;
                $('#' + element.id + '-error').text(message);
            }
        },
        error: function (qXHR, textStatus, errorThrown) {
            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
        }
    });
    $.validator.messages.PasswordStrength = message;
    return $valid;
}, "");

$.validator.addMethod("PasswordOld", function (value, element) {
    var isSuccess = false;
    var $valid = false;

    var url = constants.fasiApi.members + 'ValidateCurrentPassword?Password=' + value;

    $.ajax({
        type: "GET",
        url: url,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            'Accept-Language': generalSupport.LanguageName()
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
        },
        success: function (data) {
            if (data.Successfully) {
                $valid = true;
                message = "";
            } else {
                $valid = false;
                message = $.i18n.t('app.required.OldPassword');
                $('#' + element.id + '-error').val(message);
            }
        },
        error: function (qXHR, textStatus, errorThrown) {
            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
        }
    });
    $.validator.messages.PasswordOld = message;
    return $valid;
}, "");

var FirstPasswordChangeSupport = new function () {
    this.Init = function () {
        //Translate Configuration Start
        $.i18n.init({
            resGetPath: 'locales/__lng__.FirstPasswordChange.json',
            load: 'unspecific',
            fallbackLng: false,
            lng: localStorage.getItem('languageName') ? localStorage.getItem('languageName') : constants.defaultLanguageName
        }, function (t) {
            $('#app').i18n();
            ValidateSetup();
        });

        settings.IsValid = true;
        Setup(settings);

        function Setup(settings) {
            $('#containerWait').hide();
            if (settings.IsValid) {
                $('#containerForms').show();
                $('#containerInvalid').hide();
            } else {
                $('#containerForms').hide();
                $('#containerInvalid').show();
            }
        }

        $("#btnPaswordChange").click(function (e) {
            var formInstance = $("#loginForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                ChangePassword();
            }

            e.preventDefault();
        });

        $("#btnCancel").click(function (e) {
            var urlAction = "/fasi/wmethods/User.aspx/LogOut";
            $.ajax({
                url: urlAction,
                dataType: "json",
                type: "POST",
                headers: {
                    'Accept-Language': generalSupport.LanguageName()
                },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    window.location.href = constants.defaultPage;
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });

            e.preventDefault();
        });

        function ChangePassword() {
            var url = constants.fasiApi.members + 'PasswordChange?UserId=' + app.user.userId + '&Password=' + $('#Password').val();

            $.ajax({
                type: "GET",
                async: false,
                url: url,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Accept-Language': generalSupport.LanguageName()
                },
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                },
                success: function (data) {
                    if (data.Successfully) {
                        memberSupport.UserFirstVisitChange(false);
                        $('#containerForms').hide();
                        $('#containerHome').show();
                        generalSupport.Operation("FirstPasswordChange", {}, function (data) {
                            var dad = data;
                        });
                        notification.swal.success(
                            $.i18n.t('app.form.SuccessfullyTitle'),
                            $.i18n.t('app.form.SuccessfullyBody'),
                            3000, function () {
                                window.location.href = constants.defaultPage;
                            });
                    } else {
                        notification.swal.error($.i18n.t('app.form.SuccessfullyTitle'), data.Reason);
                    }
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }

        function ValidateSetup() {
            $("#loginForm").validate({
                rules: {
                    OldPassword: {
                        required: true,
                        PasswordOld: true
                    },
                    Password: {
                        required: true,
                        PasswordStrength: true
                    },
                    RePassword: {
                        required: true,
                        equalTo: "#Password"
                    }
                },
                messages: {
                    OldPassword: {
                        required: $.i18n.t('app.required.password'),
                        PasswordOld: $.i18n.t('app.required.OldPassword')
                    },
                    Password: {
                        required: $.i18n.t('app.required.password')
                    },
                    RePassword: {
                        required: $.i18n.t('app.required.password'),
                        equalTo: $.i18n.t('app.required.RePassword')
                    }
                }
            });
        }
    };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Custom: true,
        Element: $("#FASIReleaseLetterMainForm"),
        CallBack: FirstPasswordChangeSupport.Init
    });
});