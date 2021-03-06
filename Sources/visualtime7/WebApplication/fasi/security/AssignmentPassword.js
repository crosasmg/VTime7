var urlBase = '/fasi/wmethods/PasswordChange.aspx';
var token;
var settings = {};
var message = "";

$.validator.addMethod("PasswordStrength", function (value, element) {

    var isSuccess = false;
    var $valid = false;

    var url = constants.fasiApi.members + 'ValidatePassword?UserName=' + settings.User.UserName + '&Password=' + value + '&languageId=' + constants.defaultLanguageId;

    $.ajax({
        type: "GET",
        url: url,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
        },
        success: function (data) {
            if (data.Successfully) {
                $valid = true;
                message = "";
            } else {
                $valid =  false;
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

var AssignmentPasswordSupport = new function () {
    this.Init = function () {
        //Translate Configuration Start
        $.i18n.init({
            resGetPath: 'locales/__lng__.AssignmentPassword.json',
            load: 'unspecific',
            fallbackLng: false,
            lng: localStorage.getItem('languageName') ? localStorage.getItem('languageName') : constants.defaultLanguageName
        }, function (t) {
            $('#app').i18n();
            ValidateSetup();
        });

        //Translate Configuration End

        token = generalSupport.GetParameterByName('token');
        if (token === null) {
            settings.IsValid = false;
            Setup(settings);
        }
        else {
            $.when(IsValidToken(token)).done(function () {
                Setup(settings);
            });
        }

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

        function IsValidToken(value) {
            var param = JSON.stringify({
                'tokeValue': value
            });
            $.ajax({
                type: "POST",
                async: false,
                url: urlBase + '/IsValidToken',
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    settings = data.d;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    settings.IsValid = false;
                    generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                }
            });
        }


        function ChangePassword() {

            var url = constants.fasiApi.members + 'PasswordChange?UserId=' + settings.User.UserId + '&Password=' + $('#Password').val();


            $.ajax({
                type: "GET",
                async: false,
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                },
                success: function (data) {
                    if (data.Successfully) {
                        $('#containerForms').hide();
                        $('#containerHome').show();
                        notification.swal.success($.i18n.t('app.form.SuccessfullyTitle'), $.i18n.t('app.form.SuccessfullyBody'));
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
    }
}

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Custom: true,
        Element: $("#FASIReleaseLetterMainForm"),
        CallBack: AssignmentPasswordSupport.Init
    });
});

