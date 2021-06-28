var PasswordChangeSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#PasswordChangeFormId').val(),
            PasswordExpiration: $('#PasswordExpiration').is(':checked'),
            OldPassword: $('#OldPassword').val(),
            NewPassword: $('#NewPassword').val(),
            RePassword: $('#RePassword').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#PasswordChangeFormId').val(data.InstanceFormId);
        $('#PasswordExpiration').prop("checked", data.PasswordExpiration);
        $('#OldPassword').val(data.OldPassword);
        $('#NewPassword').val(data.NewPassword);
        $('#RePassword').val(data.RePassword);



    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               PasswordChangeSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };


    this.CallRenderLookUps = function (data) {
          if (data.d.Success === true && data.d.Data.LookUps) {

              data.d.Data.LookUps.forEach(function (elementSource) {
              generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, false);
 
              });
          }
     };




    this.ControlActions =   function () {

        $('#btnSave').click(function (event) {
            var formInstance = $("#PasswordChangeMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
            var ValidacionPassword;
            var ValidacionPassowrdMessage;
            var languageId;
            var PassworChangeResult;
            var UserId;
            var errors;
            var UrlRedirect;
            var MessageResult;
                var btnLoading = Ladda.create(document.querySelector('#btnSave'));
                btnLoading.start();
                languageId = constants.defaultLanguageId;
                UserId = app.user.userId;
                UrlRedirect = constants.defaultPage;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'security/v1/UserPasswordChange?PasswordOld=' + $('#OldPassword').val() + '&PasswordNew=' + $('#NewPassword').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         PassworChangeResult = data.Successfully;
MessageResult = data.Reason;

                    if (PassworChangeResult === true){
                        var message4 = $.i18n.t('app.form.btnSave_Message_Notify_click4');
                        notification.swal.success($.i18n.t('app.form.btnSave_Title_Notify_click4'), message4);
                        $('#btnSave').toggleClass('hidden', true);
                        $('#btnGo').toggleClass('hidden', false);
                        $('#zone2').toggleClass('hidden', true);
                        $('#zone0').toggleClass('hidden', true);
                        $('#zone5').toggleClass('hidden', true);
                        }                        
                        else {
                        var message7 = $.i18n.t('app.form.btnSave_Message_Notify_click7');
                        message7 = message7.replace(/{{MessageResult}}/g, MessageResult);
                        notification.swal.error($.i18n.t('app.form.btnSave_Title_Notify_click7'), message7);

                            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnGo').click(function (event) {
                     var data;
            var UrlRedirect;
                var btnLoading = Ladda.create(document.querySelector('#btnGo'));
                btnLoading.start();
                if ($('#PasswordExpiration').is(':checked') === false){
                    UrlRedirect = "/fasi/default.aspx";
                    app.security.Logout(app.user.userId, true);
                    }                    
                    else {
                    UrlRedirect = "/fasi/security/logoff.ashx";

                        }
                window.open(UrlRedirect, '_self');
                btnLoading.stop();



            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("OldPassword_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
            var PaswordCurrentValid;
            var languageId;
            var UserId;
            var UrlRedirect;
                languageId = constants.defaultLanguageId;
                UserId = app.user.userId;
                UrlRedirect = constants.defaultPage;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/ValidateCurrentPassword?Password=' + $('#OldPassword').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         PaswordCurrentValid = data.Successfully;

                if (PaswordCurrentValid === false){
                    result = false;
            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
            }
            return result;
        });
        $.validator.addMethod("RePassword_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if ($('#RePassword').val() != $('#NewPassword').val()){
                    result = false;
            }

            }
            return result;
        });
        $.validator.addMethod("RePassword_Validate2", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
            var ValidacionPassword;
            var ValidacionPasswordMessage;
            var languageId;
                languageId = constants.defaultLanguageId;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/ValidatePasswordByUserId?Password=' + $('#NewPassword').val() + '&languageId=' + languageId,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         ValidacionPassword = data.Successfully;
ValidacionPasswordMessage = data.Reason;

                if (ValidacionPassword === false){
                    result = false;
                    var messageRePassword = $.i18n.t('app.form.RePassword_Validate2');
                    messageRePassword = messageRePassword.replace(/{{ValidacionPasswordMessage}}/g, ValidacionPasswordMessage);
                    $('#RePassword').rules('add', { messages: { RePassword_Validate2: messageRePassword }});
            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
            }
            return result;
        });

        $("#PasswordChangeMainForm").validate({
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
           onkeyup: function(element) {
                if ($(element).attr('name') !== 'OldPassword' && $(element).attr('name') !== 'RePassword') {
                    $.validator.defaults.onkeyup.apply(this,arguments);
                }
            },
            rules: {
                OldPassword: {
                    required: true,
                    maxlength: 15,
                    OldPassword_Validate1: true
                },
                NewPassword: {
                    required: true,
                    maxlength: 15
                },
                RePassword: {
                    required: true,
                    maxlength: 15,
                    RePassword_Validate1: true,
                    RePassword_Validate2: true
                }
            },
            messages: {
                OldPassword: {
                    required: $.i18n.t('app.form.OldPassword_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.OldPassword_maxlength'),
                    OldPassword_Validate1: $.i18n.t('app.form.OldPassword_Validate1')
                },
                NewPassword: {
                    required: $.i18n.t('app.form.NewPassword_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.NewPassword_maxlength')
                },
                RePassword: {
                    required: $.i18n.t('app.form.RePassword_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.RePassword_maxlength'),
                    RePassword_Validate1: $.i18n.t('app.form.RePassword_Validate1'),
                    RePassword_Validate2: $.i18n.t('app.form.RePassword_Validate2')
                }
            }
        });

    };











  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('PasswordChange', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        PasswordChangeSupport.ValidateSetup();
        
        

    PasswordChangeSupport.ControlBehaviour();
    PasswordChangeSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/PasswordChangeActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#PasswordChangeFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  PasswordChangeSupport.CallRenderLookUps(data);
                
                $('#OldPassword').val(generalSupport.URLStringValue('OldPassword'));
    $('#NewPassword').val(generalSupport.URLStringValue('NewPassword'));
    $('#RePassword').val(generalSupport.URLStringValue('RePassword'));

            
            
            
            
                   var PasswordExpirationValue = generalSupport.GetParameterByName('PasswordExpiration');
   if (PasswordExpirationValue  === null) { 
      PasswordExpirationValue  = false;
    }
    if (PasswordExpirationValue === true || PasswordExpirationValue === "True"){
        PasswordExpirationValue = true;
    }
    $('#PasswordExpiration').prop("checked", PasswordExpirationValue );

    if ($('#PasswordExpiration').is(':checked') === true){
        $('#zone5').toggleClass('hidden', false);

        }

             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        IsConnected: true,
        Element: $("#PasswordChangeMainForm"),
        CallBack: PasswordChangeSupport.Init
    });
});

