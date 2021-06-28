var HT5SpecialQueryOfClientSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5SpecialQueryOfClientFormId').val(),
            TipoDeBusqueda: generalSupport.RadioNumericValue('TipoDeBusqueda'),
            eMail: $('#eMail').val(),
            ClientBankAccountBankAccount: $('#BankAccount').val(),
            CreditCardCreditCardNumber: $('#CreditCardNumber').val(),
            ClientBirthDate: generalSupport.DatePickerValueInputToObject('#BirthDate'),
            AddressPhysicalAddressDLIPhysicalAddressCountryCode: $('#Code').val(),
            AddressPhysicalAddressDLIPhysicalAddressZipCode: $('#ZipCode').val(),
            ClientID: $('#ClientID').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#HT5SpecialQueryOfClientFormId').val(data.InstanceFormId);
        if($('input:radio[name=TipoDeBusqueda][value=' + data.TipoDeBusqueda +']').length===0)
           $('input:radio[name=TipoDeBusqueda]').prop('checked', false);
        else
           $($('input:radio[name=TipoDeBusqueda][value=' + data.TipoDeBusqueda +']')).prop('checked', true);
        $('#TipoDeBusqueda').data('oldValue', data.TipoDeBusqueda);
        $('#TipoDeBusqueda').val(data.TipoDeBusqueda);

        $('#eMail').val(data.eMail);
        $('#BankAccount').val(data.ClientBankAccountBankAccount);
        $('#CreditCardNumber').val(data.CreditCardCreditCardNumber);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.ClientBirthDate, generalSupport.DateFormat()));
        $('#ZipCode').val(data.AddressPhysicalAddressDLIPhysicalAddressZipCode);

        HT5SpecialQueryOfClientSupport.LookUpForCode(data.AddressPhysicalAddressDLIPhysicalAddressCountryCode, source);
        HT5SpecialQueryOfClientSupport.LookUpForClientID(data.ClientID, source);


    };

    this.ControlBehaviour = function () {






  this.LookUpForClientID = function (defaultValue, source) {
        var ctrol = $('#ClientID');
        var oldvalue = ctrol.val();
     
        if (oldvalue === null)
            oldvalue = 0;
            
        ctrol.data('oldValue', oldvalue );
		
        if (defaultValue === null)
            defaultValue = 0;    

           ctrol.children().remove();
           ctrol.append($('<option />').val('0').text(' Cargando...'));

           app.core.AsyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/LookUpForClientID", false,
               JSON.stringify({
                   formId: $('#HT5SpecialQueryOfClientFormId').val()
               }),
               function (data) {
                   ctrol.children().remove();
                   $.each(data.d.Data, function () {
                       ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                   });

                   ctrol.val(defaultValue);

               ctrol.data('oldValue', defaultValue);
               });
    };

        $('#BirthDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#BirthDate_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               HT5SpecialQueryOfClientSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5SpecialQueryOfClientFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#HT5SpecialQueryOfClientFormId').val(data.d.Data.Instance.InstanceFormId);
                
                HT5SpecialQueryOfClientSupport.CallRenderLookUps(data);               
                







                HT5SpecialQueryOfClientSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#HT5SpecialQueryOfClientFormId').val());
 
              
          

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

       $('input:radio[name=TipoDeBusqueda]').change(function () {
                     var data;
                if (generalSupport.RadioNumericValue('TipoDeBusqueda') === 1){
                    $('#eMail').toggleClass('hidden', false);
                    $('#eMailLabel').toggleClass('hidden', false);
                    $('#eMailRequired').toggleClass('hidden', false);
                    $('#BankAccount').toggleClass('hidden', true);
                    $('#BankAccountLabel').toggleClass('hidden', true);
                    $('#BankAccountRequired').toggleClass('hidden', true);
                    $('#CreditCardNumber').toggleClass('hidden', true);
                    $('#CreditCardNumberLabel').toggleClass('hidden', true);
                    $('#CreditCardNumberRequired').toggleClass('hidden', true);
                    $('#BirthDate').toggleClass('hidden', true);
                    $('#BirthDate_group').toggleClass('hidden', true);
                    $('#BirthDateLabel').toggleClass('hidden', true);
                    $('#BirthDateRequired').toggleClass('hidden', true);
                    $('#ZipCode').toggleClass('hidden', true);
                    $('#ZipCodeLabel').toggleClass('hidden', true);
                    $('#ZipCodeRequired').toggleClass('hidden', true);
                    $('#Code').toggleClass('hidden', true);
                    $('#CodeLabel').toggleClass('hidden', true);
                    $('#CodeRequired').toggleClass('hidden', true);

                    }
                if (generalSupport.RadioNumericValue('TipoDeBusqueda') === 2){
                    $('#eMail').toggleClass('hidden', true);
                    $('#eMailLabel').toggleClass('hidden', true);
                    $('#eMailRequired').toggleClass('hidden', true);
                    $('#BankAccount').toggleClass('hidden', false);
                    $('#BankAccountLabel').toggleClass('hidden', false);
                    $('#BankAccountRequired').toggleClass('hidden', false);
                    $('#CreditCardNumber').toggleClass('hidden', true);
                    $('#CreditCardNumberLabel').toggleClass('hidden', true);
                    $('#CreditCardNumberRequired').toggleClass('hidden', true);
                    $('#BirthDate').toggleClass('hidden', true);
                    $('#BirthDate_group').toggleClass('hidden', true);
                    $('#BirthDateLabel').toggleClass('hidden', true);
                    $('#BirthDateRequired').toggleClass('hidden', true);
                    $('#ZipCode').toggleClass('hidden', true);
                    $('#ZipCodeLabel').toggleClass('hidden', true);
                    $('#ZipCodeRequired').toggleClass('hidden', true);
                    $('#Code').toggleClass('hidden', true);
                    $('#CodeLabel').toggleClass('hidden', true);
                    $('#CodeRequired').toggleClass('hidden', true);

                        }
                if (generalSupport.RadioNumericValue('TipoDeBusqueda') === 3){
                    $('#eMail').toggleClass('hidden', true);
                    $('#eMailLabel').toggleClass('hidden', true);
                    $('#eMailRequired').toggleClass('hidden', true);
                    $('#BankAccount').toggleClass('hidden', true);
                    $('#BankAccountLabel').toggleClass('hidden', true);
                    $('#BankAccountRequired').toggleClass('hidden', true);
                    $('#CreditCardNumber').toggleClass('hidden', false);
                    $('#CreditCardNumberLabel').toggleClass('hidden', false);
                    $('#CreditCardNumberRequired').toggleClass('hidden', false);
                    $('#BirthDate').toggleClass('hidden', true);
                    $('#BirthDate_group').toggleClass('hidden', true);
                    $('#BirthDateLabel').toggleClass('hidden', true);
                    $('#BirthDateRequired').toggleClass('hidden', true);
                    $('#ZipCode').toggleClass('hidden', true);
                    $('#ZipCodeLabel').toggleClass('hidden', true);
                    $('#ZipCodeRequired').toggleClass('hidden', true);
                    $('#Code').toggleClass('hidden', true);
                    $('#CodeLabel').toggleClass('hidden', true);
                    $('#CodeRequired').toggleClass('hidden', true);

                            }
                if (generalSupport.RadioNumericValue('TipoDeBusqueda') === 4){
                    $('#eMail').toggleClass('hidden', true);
                    $('#eMailLabel').toggleClass('hidden', true);
                    $('#eMailRequired').toggleClass('hidden', true);
                    $('#BankAccount').toggleClass('hidden', true);
                    $('#BankAccountLabel').toggleClass('hidden', true);
                    $('#BankAccountRequired').toggleClass('hidden', true);
                    $('#CreditCardNumber').toggleClass('hidden', true);
                    $('#CreditCardNumberLabel').toggleClass('hidden', true);
                    $('#CreditCardNumberRequired').toggleClass('hidden', true);
                    $('#BirthDate').toggleClass('hidden', false);
                    $('#BirthDate_group').toggleClass('hidden', false);
                    $('#BirthDateLabel').toggleClass('hidden', false);
                    $('#BirthDateRequired').toggleClass('hidden', false);
                    $('#ZipCode').toggleClass('hidden', true);
                    $('#ZipCodeLabel').toggleClass('hidden', true);
                    $('#ZipCodeRequired').toggleClass('hidden', true);
                    $('#Code').toggleClass('hidden', true);
                    $('#CodeLabel').toggleClass('hidden', true);
                    $('#CodeRequired').toggleClass('hidden', true);

                                }
                if (generalSupport.RadioNumericValue('TipoDeBusqueda') === 5){
                    $('#eMail').toggleClass('hidden', true);
                    $('#eMailLabel').toggleClass('hidden', true);
                    $('#eMailRequired').toggleClass('hidden', true);
                    $('#BankAccount').toggleClass('hidden', true);
                    $('#BankAccountLabel').toggleClass('hidden', true);
                    $('#BankAccountRequired').toggleClass('hidden', true);
                    $('#CreditCardNumber').toggleClass('hidden', true);
                    $('#CreditCardNumberLabel').toggleClass('hidden', true);
                    $('#CreditCardNumberRequired').toggleClass('hidden', true);
                    $('#BirthDate').toggleClass('hidden', true);
                    $('#BirthDate_group').toggleClass('hidden', true);
                    $('#BirthDateLabel').toggleClass('hidden', true);
                    $('#BirthDateRequired').toggleClass('hidden', true);
                    $('#ZipCode').toggleClass('hidden', false);
                    $('#ZipCodeLabel').toggleClass('hidden', false);
                    $('#ZipCodeRequired').toggleClass('hidden', false);
                    $('#Code').toggleClass('hidden', false);
                    $('#CodeLabel').toggleClass('hidden', false);
                    $('#CodeRequired').toggleClass('hidden', false);

                                    }
                $('#button1').toggleClass('hidden', false);


        });
        $('#ClientID').change(function () {
         if ($('#ClientID').val() !== null && $('#ClientID').val() !== ($('#ClientID').data('oldValue') || '0').toString()) {
             $('#ClientID').data('oldValue', $('#ClientID').val() );
             app.core.SyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/ClientIDChange", false,
                 JSON.stringify({
                     instance: HT5SpecialQueryOfClientSupport.InputToObject()
                 }),
                 function (data) {
                     
                     HT5SpecialQueryOfClientSupport.ActionProcess(data, 'ClientIDChange');
             });
      }          
    });
        $('#button1').click(function (event) {
                var formInstance = $("#HT5SpecialQueryOfClientMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button1'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/button1Click", false,
                          JSON.stringify({
                                        instance: HT5SpecialQueryOfClientSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();
                    
                    HT5SpecialQueryOfClientSupport.ActionProcess(data, 'button1Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button3').click(function (event) {
                var formInstance = $("#HT5SpecialQueryOfClientMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button3'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/button3Click", false,
                          JSON.stringify({
                                        instance: HT5SpecialQueryOfClientSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();
                    
                    HT5SpecialQueryOfClientSupport.ActionProcess(data, 'button3Click');
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


        $("#HT5SpecialQueryOfClientMainForm").validate({
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
                TipoDeBusqueda: {
                    required: true,
                },
                eMail: {
                    required: true,
                    maxlength: 60,
                    email: true
                },
                BankAccount: {
                    required: true,
                    maxlength: 25
                },
                CreditCardNumber: {
                    required: true,
                    maxlength: 20
                },
                BirthDate: {
                    required: true,
                    DatePicker: true
                },
                Code: {
                    required: true                },
                ZipCode: {
                    required: true,
                    maxlength: 10
                },
                ClientID: {
                }
            },
            messages: {
                TipoDeBusqueda: {
                    required: $.i18n.t('app.form.TipoDeBusqueda_RequiredMessage'),
                },
                eMail: {
                    required: $.i18n.t('app.form.eMail_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.eMail_maxlength'),
                    email: $.i18n.t('app.form.eMail_Email')
                },
                BankAccount: {
                    required: $.i18n.t('app.form.BankAccount_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.BankAccount_maxlength')
                },
                CreditCardNumber: {
                    required: $.i18n.t('app.form.CreditCardNumber_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.CreditCardNumber_maxlength')
                },
                BirthDate: {
                    required: $.i18n.t('app.form.BirthDate_RequiredMessage'),
                    DatePicker: $.i18n.t('app.form.BirthDate_DatePicker')
                },
                Code: {
                    required: $.i18n.t('app.form.Code_RequiredMessage')                },
                ZipCode: {
                    required: $.i18n.t('app.form.ZipCode_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.ZipCode_maxlength')
                },
                ClientID: {
                }
            }
        });

    };
    this.LookUpForCode = function (defaultValue, source) {
        var ctrol = $('#Code');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5SpecialQueryOfClientActions.aspx/LookUpForCode", false,
                JSON.stringify({ id: $('#HT5SpecialQueryOfClientFormId').val() }),
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
    securitySupport.ValidateAccessRoles(['EASE1', 'Suscriptor']);
    moment.locale(generalSupport.UserContext().languageName);
    
   generalSupport.TranslateInit('HT5SpecialQueryOfClient', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        HT5SpecialQueryOfClientSupport.ValidateSetup();
        
        

    HT5SpecialQueryOfClientSupport.ControlBehaviour();
    HT5SpecialQueryOfClientSupport.ControlActions();
    

    HT5SpecialQueryOfClientSupport.Initialization();

   });
  };
};

$(document).ready(function () {
   HT5SpecialQueryOfClientSupport.Init();
});

