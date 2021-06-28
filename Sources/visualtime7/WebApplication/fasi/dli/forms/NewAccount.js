var NewAccountSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#NewAccountFormId').val(),
            userType: $('input:radio[name=chkUserType]:checked').val(),
            UserInformationUserName: $('#UserName').val(),
            UserInformationEmail: $('#Email').val(),
            EmailVerification: $('#EmailVerification').val(),
            UserInformationLanguageID: parseInt(0 + $('#LanguageID').val(), 10),
            UserInformationFirstName: $('#FirstName').val(),
            UserInformationSurName: $('#SurName').val(),
            UserInformationLastName: $('#LastName').val(),
            UserInformationSecondLastName: $('#SecondLastName').val(),
            UserInformationDateOfBirth: generalSupport.DatePickerValueInputToObject('#DateOfBirth'),
            UserInformationGender: $('#Gender').val(),
            UserInformationAddressHome: $('#AddressHome').val(),
            UserInformationCountry: $('#Country').val(),
            UserInformationCity: $('#City').val(),
            UserInformationState: $('#State').val(),
            UserInformationTelephoneNumber: generalSupport.NumericValue('#TelephoneNumber', 0, 999999999),
            identificatorAgent: generalSupport.NumericValue('#numeric0', -999999999999999, 999999999999999),
            identificatorClient: $('#text3').val(),
            AgreeTerms: $('#AcceptConditions').is(':checked')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#NewAccountFormId').val(data.InstanceFormId);
        if($('input:radio[name=chkUserType][value=' + data.userType +']').length===0){
           $('input:radio[name=chkUserType]').prop('checked', false);
           $('input:radio[name=chkUserType].default').prop('checked', true);
        }
        else
           $($('input:radio[name=chkUserType][value=' + data.userType +']')).prop('checked', true);
        $('#chkUserType').data('oldValue', data.userType);
        $('#chkUserType').val(data.userType);

        $('#UserName').val(data.UserInformationUserName);
        $('#Email').val(data.UserInformationEmail);
        $('#EmailVerification').val(data.EmailVerification);
        $('#FirstName').val(data.UserInformationFirstName);
        $('#SurName').val(data.UserInformationSurName);
        $('#LastName').val(data.UserInformationLastName);
        $('#SecondLastName').val(data.UserInformationSecondLastName);
        $('#DateOfBirth').val(generalSupport.ToJavaScriptDateCustom(data.UserInformationDateOfBirth, generalSupport.DateFormat()));
        $('#Gender').data('oldValue', data.UserInformationGender);
        $('#Gender').val(data.UserInformationGender);
        $('#AddressHome').val(data.UserInformationAddressHome);
        $('#City').val(data.UserInformationCity);
        $('#State').val(data.UserInformationState);
        AutoNumeric.set('#TelephoneNumber', data.UserInformationTelephoneNumber);
        AutoNumeric.set('#numeric0', data.identificatorAgent);
        $('#text3').val(data.identificatorClient);
        $('#AcceptConditions').prop("checked", data.AgreeTerms);

        NewAccountSupport.LookUpForLanguageID(data.UserInformationLanguageID, source);
        NewAccountSupport.LookUpForCountry(data.UserInformationCountry, source);


    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#TelephoneNumber', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#numeric0', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 999999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999999
        });




        $('#DateOfBirth_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateOfBirth_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               NewAccountSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/NewAccountActions.aspx/Initialization", true,
            JSON.stringify({
                id: $('#NewAccountFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#NewAccountFormId').val(data.d.Data.Instance.InstanceFormId);
                
                NewAccountSupport.CallRenderLookUps(data);               
                







                NewAccountSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#NewAccountFormId').val());
 
              
          

            });
    };



    this.CallRenderLookUps = function (data) {
          if (data.d.Success === true && data.d.Data.LookUps) {

              data.d.Data.LookUps.forEach(function (elementSource) {
              generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, false);
 
              });
          }
     };




    this.ControlActions =   function () {

       $('input:radio[name=chkUserType]').change(function () {
                     var data;
            var typeUserChange;
                typeUserChange = $('input:radio[name=chkUserType]:checked').val();
                if (typeUserChange === '1'){
                    $('#zoneIntermediary').toggleClass('hidden', true);
                    $('#zoneClient').toggleClass('hidden', false);
                    }                    
                    else {
                    $('#zoneIntermediary').toggleClass('hidden', false);
                    $('#zoneClient').toggleClass('hidden', true);

                        }


        });
        $('#CancelBtn').click(function (event) {
                     var data;
                var btnLoading = Ladda.create(document.querySelector('#CancelBtn'));
                btnLoading.start();
                window.open('/fasi/default.aspx', '_blank');
                btnLoading.stop();



            event.preventDefault();
        });
        $('#ContinueBtn').click(function (event) {
                     var data;
                var btnLoading = Ladda.create(document.querySelector('#ContinueBtn'));
                btnLoading.start();
                $('#UserTypeSelection').toggleClass('hidden', true);
                $('#UserInfo').toggleClass('hidden', false);
                btnLoading.stop();



            event.preventDefault();
        });
        $('#AcceptConditions').change(function () {
         if ($('#AcceptConditions').is(':checked') !== null && $('#AcceptConditions').is(':checked') !== $('#AcceptConditions').data('oldValue')){         
             $('#AcceptConditions').data('oldValue', $('#AcceptConditions').is(':checked') );
             
                if (($('#AcceptConditions').is(':checked') === true)){
                    $('#btnRegister').prop('disabled', false);
                    }                    
                    else {
                    $('#btnRegister').prop('disabled', true);

                        }
          
         }
        });
        $('#btnSeeTerms').click(function (event) {
                     var data;
                var btnLoading = Ladda.create(document.querySelector('#btnSeeTerms'));
                btnLoading.start();
                window.open('/fasi/dli/forms/TermsPopup.html','_blank','scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=600,left=30,top=30');
                btnLoading.stop();



            event.preventDefault();
        });
        $('#btnRegister').click(function (event) {
            var formInstance = $("#NewAccountMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
            var UserCreateResult;
            var UserTypeInternal;
            var RolesDescription;
            var errors;
                var btnLoading = Ladda.create(document.querySelector('#btnRegister'));
                btnLoading.start();
                if (generalSupport.RadioNumericValue('chkUserType') === 1){
                    UserTypeInternal = "Client";
                    RolesDescription = "Client";
                    }                    
                    else {
                    UserTypeInternal = "Agent";
                    RolesDescription = "Agent";

                        }
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'fasi/v1/CreatePortalUser',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: 0, City: $('#City').val(), Country: parseInt(0 + $('#Country').val(), 10), DateOfBirth: generalSupport.DatePickerValue('#DateOfBirth'), Email: $('#Email').val(), FirstName: $('#FirstName').val(), Gender: $('#Gender').val(), LastName: $('#LastName').val(), PasswordAnswer: ' ', PasswordQuestion: ' ', SecondLastName: $('#SecondLastName').val(), State: $('#State').val(), SurName: $('#SurName').val(), TelephoneNumber: generalSupport.NumericValue('#TelephoneNumber', 0, 999999999), UserName: $('#UserName').val(), LanguageID: parseInt(0 + $('#LanguageID').val(), 10), ClientID: $('#text3').val(), ProducerID: generalSupport.NumericValue('#numeric0', -999999999999999, 999999999999999), UserType: UserTypeInternal, AddressHome: $('#AddressHome').val() }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         UserCreateResult = data.Successfully;

                    if (UserCreateResult === true){
                        $('#UserInfo').toggleClass('hidden', true);
                        $('#finalZone').toggleClass('hidden', false);
                                }                                
                                else {
                        var message7 = $.i18n.t('app.form.btnRegister_Message_Notify_click7');
                        notification.swal.error($.i18n.t('app.form.btnRegister_Title_Notify_click7'), message7);

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
        $('#btnStart').click(function (event) {
            var formInstance = $("#NewAccountMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnStart'));
                btnLoading.start();
                window.open('/fasi/default.aspx', '_blank');
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("UserName_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
            var existe;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/UsersExistUsername?username=' + $('#UserName').val(),
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
                         existe = data.Data;

                if (existe === true){
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
        $.validator.addMethod("Email_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
            var existe;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/UsersExistEMail?email=' + $('#Email').val(),
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
                         existe = data.Data;

                if (existe === true){
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
        $.validator.addMethod("EmailVerification_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if ($('#EmailVerification').val() != $('#Email').val()){
                    result = false;
            }

            }
            return result;
        });
        $.validator.addMethod("DateOfBirth_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if (generalSupport.DatePickerValue('#DateOfBirth') > new Date()){
                    result = false;
            }

            }
            return result;
        });

        $("#NewAccountMainForm").validate({
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
                if ($(element).attr('name') !== 'UserName' && $(element).attr('name') !== 'Email') {
                    $.validator.defaults.onkeyup.apply(this,arguments);
                }
            },
            rules: {
                chkUserType: {
                },
                UserName: {
                    required: true,
                    maxlength: 15,
                    UserName_Validate1: true
                },
                Email: {
                    required: true,
                    maxlength: 80,
                    email: true,
                    Email_Validate1: true
                },
                EmailVerification: {
                    required: true,
                    maxlength: 80,
                    email: true,
                    EmailVerification_Validate1: true
                },
                LanguageID: {
                    required: true                },
                FirstName: {
                    required: true,
                    maxlength: 15
                },
                SurName: {
                    maxlength: 15
                },
                LastName: {
                    required: true,
                    maxlength: 15
                },
                SecondLastName: {
                    maxlength: 15
                },
                DateOfBirth: {
                    DatePicker: true,
                    DateOfBirth_Validate1: true
                },
                Gender: {
                },
                AddressHome: {
                    maxlength: 70
                },
                Country: {
                    required: true                },
                City: {
                    maxlength: 15
                },
                State: {
                    maxlength: 15
                },
                TelephoneNumber: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999
                },
                numeric0: {
                    AutoNumericMinValue: -999999999999999,
                    AutoNumericMaxValue: 999999999999999
                },
                text3: {
                    required: true,
                    maxlength: 15
                }
            },
            messages: {
                chkUserType: {
                },
                UserName: {
                    required: $.i18n.t('app.form.UserName_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.UserName_maxlength'),
                    UserName_Validate1: $.i18n.t('app.form.UserName_Validate1')
                },
                Email: {
                    required: $.i18n.t('app.form.Email_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.Email_maxlength'),
                    email: $.i18n.t('app.form.Email_Email'),
                    Email_Validate1: $.i18n.t('app.form.Email_Validate1')
                },
                EmailVerification: {
                    required: $.i18n.t('app.form.EmailVerification_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.EmailVerification_maxlength'),
                    email: $.i18n.t('app.form.EmailVerification_Email'),
                    EmailVerification_Validate1: $.i18n.t('app.form.EmailVerification_Validate1')
                },
                LanguageID: {
                    required: $.i18n.t('app.form.LanguageID_RequiredMessage')                },
                FirstName: {
                    required: $.i18n.t('app.form.FirstName_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.FirstName_maxlength')
                },
                SurName: {
                    maxlength: $.i18n.t('app.form.SurName_maxlength')
                },
                LastName: {
                    required: $.i18n.t('app.form.LastName_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.LastName_maxlength')
                },
                SecondLastName: {
                    maxlength: $.i18n.t('app.form.SecondLastName_maxlength')
                },
                DateOfBirth: {
                    DatePicker: $.i18n.t('app.form.DateOfBirth_DatePicker'),
                    DateOfBirth_Validate1: $.i18n.t('app.form.DateOfBirth_Validate1')
                },
                Gender: {
                },
                AddressHome: {
                    maxlength: $.i18n.t('app.form.AddressHome_maxlength')
                },
                Country: {
                    required: $.i18n.t('app.form.Country_RequiredMessage')                },
                City: {
                    maxlength: $.i18n.t('app.form.City_maxlength')
                },
                State: {
                    maxlength: $.i18n.t('app.form.State_maxlength')
                },
                TelephoneNumber: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999'
                },
                numeric0: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999'
                },
                text3: {
                    required: $.i18n.t('app.form.text3_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.text3_maxlength')
                }
            }
        });

    };
    this.LookUpForLanguageID = function (defaultValue, source) {
        var ctrol = $('#LanguageID');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/NewAccountActions.aspx/LookUpForLanguageID", false,
                JSON.stringify({ id: $('#NewAccountFormId').val() }),
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
    this.LookUpForCountry = function (defaultValue, source) {
        var ctrol = $('#Country');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/NewAccountActions.aspx/LookUpForCountry", false,
                JSON.stringify({ id: $('#NewAccountFormId').val() }),
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











  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('NewAccount', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        NewAccountSupport.ValidateSetup();
        
        

    NewAccountSupport.ControlBehaviour();
    NewAccountSupport.ControlActions();
    

    NewAccountSupport.Initialization();

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#NewAccountMainForm"),
        CallBack: NewAccountSupport.Init
    });
});

