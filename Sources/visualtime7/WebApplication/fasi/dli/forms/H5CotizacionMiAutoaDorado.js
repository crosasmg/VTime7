var H5CotizacionMiAutoaDoradoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5CotizacionMiAutoaDoradoFormId').val(),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationEffectiveDate: generalSupport.DatePickerValueInputToObject('#EffectiveDate'),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            BirthdateIncluded: generalSupport.DatePickerValueInputToObject('#BirthdateIncluded'),
            RiskInformationAutomobileLineOfBusinessYearOfManufactured: generalSupport.NumericValue('#YearOfManufactured', 0, 99999),
            RiskInformationAutomobileLineOfBusinessVehicleCode: $('#VehicleCode').val(),
            RiskInformationAutomobileLineOfBusinessValueOfTheVehicle: generalSupport.NumericValue('#ValueOfTheVehicle', 0, 999999999999999999),
            RiskInformationCurrency: parseInt(0 + $('#Currency').val(), 10),
            RiskInformationAutomobileLineOfBusinessDrivingZone: parseInt(0 + $('#DrivingZone').val(), 10),
            RiskInformationAutomobileLineOfBusinessUseOfVehicle: parseInt(0 + $('#UseOfVehicle').val(), 10),
            CoverageWithCalculatedPremium_CoverageWithCalculatedPremium: generalSupport.NormalizeProperties($('#CoverageWithCalculatedPremiumTbl').bootstrapTable('getData'), ''),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            RiskInformationPrimaryInsuredClientFirstName: $('#FirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#LastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#LastName2').val(),
            RiskInformationPrimaryInsuredClientGender: $('input:radio[name=Gender]:checked').val(),
            RiskInformationAutomobileLineOfBusinessLicensePlateType: $('#LicensePlateType').val(),
            RiskInformationAutomobileLineOfBusinessLicensePlate: $('#LicensePlate').val(),
            RiskInformationAutomobileLineOfBusinessColor: $('#Color').val(),
            RiskInformationAutomobileLineOfBusinessChassis: $('#Chassis').val(),
            RiskInformationAutomobileLineOfBusinessEngineSerialNumber: $('#EngineSerialNumber').val(),
            RiskInformationAutomobileLineOfBusinessAutomobileInformationMileage: generalSupport.NumericValue('#Mileage', -999999999999, 999999999999),
            BusinessAddress: AddressSupport.GetLocalAddressBySelector("physicaladdress1"),
            RiskInformationAutomaticPaymentPolicyCreditCardType: $('input:radio[name=CreditCardType]:checked').val(),
            RiskInformationAutomaticPaymentPolicyBankCode: parseInt(0 + $('#BankCode').val(), 10),
            RiskInformationAutomaticPaymentPolicyCreditCardNumber: $('#CreditCardNumber').val(),
            MonthCard: generalSupport.NumericValue('#MonthCard', -99, 99),
            YearCard: generalSupport.NumericValue('#YearCard', -9999, 9999),
            RiskInformationAutomaticPaymentPolicyAuthorizationNumber: generalSupport.NumericValue('#AuthorizationNumber', -99999, 99999),
            eMail: $('#eMail').val(),
            OnLinePrintIndicator: $('#OnLinePrintIndicator').is(':checked'),
            MailToProducerIndicator: $('#MailToProducerIndicator').is(':checked'),
            eMail: $('#eMailSend').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5CotizacionMiAutoaDoradoFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, generalSupport.DateFormat()));
        $('#uwcaseid').html(data.uwcaseid);
        $('#BirthdateIncluded').val(generalSupport.ToJavaScriptDateCustom(data.BirthdateIncluded, generalSupport.DateFormat()));
        AutoNumeric.set('#YearOfManufactured', data.RiskInformationAutomobileLineOfBusinessYearOfManufactured);
        AutoNumeric.set('#ValueOfTheVehicle', data.RiskInformationAutomobileLineOfBusinessValueOfTheVehicle);
        $('#ClientID').val(data.RiskInformationPrimaryInsuredClientClientID);
        $('#FirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#LastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#LastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        if($('input:radio[name=Gender][value=' + data.RiskInformationPrimaryInsuredClientGender +']').length===0)
           $('input:radio[name=Gender]').prop('checked', false);
        else
           $($('input:radio[name=Gender][value=' + data.RiskInformationPrimaryInsuredClientGender +']')).prop('checked', true);
        $('#Gender').data('oldValue', data.RiskInformationPrimaryInsuredClientGender);
        $('#Gender').val(data.RiskInformationPrimaryInsuredClientGender);

        $('#LicensePlate').val(data.RiskInformationAutomobileLineOfBusinessLicensePlate);
        $('#Color').val(data.RiskInformationAutomobileLineOfBusinessColor);
        $('#Chassis').val(data.RiskInformationAutomobileLineOfBusinessChassis);
        $('#EngineSerialNumber').val(data.RiskInformationAutomobileLineOfBusinessEngineSerialNumber);
        AutoNumeric.set('#Mileage', data.RiskInformationAutomobileLineOfBusinessAutomobileInformationMileage);
        AddressSupport.Initialization('physicaladdress1', data.BusinessAddress, true, false);
        H5CotizacionMiAutoaDoradoSupport.LookUpForCreditCardType(data.RiskInformationAutomaticPaymentPolicyCreditCardType, source);

        $('#CreditCardNumber').val(data.RiskInformationAutomaticPaymentPolicyCreditCardNumber);
        AutoNumeric.set('#MonthCard', data.MonthCard);
        AutoNumeric.set('#YearCard', data.YearCard);
        AutoNumeric.set('#AuthorizationNumber', data.RiskInformationAutomaticPaymentPolicyAuthorizationNumber);
        $('#eMail').val(data.eMail);
        $('#OnLinePrintIndicator').prop("checked", data.OnLinePrintIndicator);
        $('#MailToProducerIndicator').prop("checked", data.MailToProducerIndicator);
        $('#eMailSend').val(data.eMail);

        H5CotizacionMiAutoaDoradoSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForVehicleCode(data.RiskInformationAutomobileLineOfBusinessVehicleCode, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForCurrency(data.RiskInformationCurrency, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForDrivingZone(data.RiskInformationAutomobileLineOfBusinessDrivingZone, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForUseOfVehicle(data.RiskInformationAutomobileLineOfBusinessUseOfVehicle, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForLicensePlateType(data.RiskInformationAutomobileLineOfBusinessLicensePlateType, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForBankCode(data.RiskInformationAutomaticPaymentPolicyBankCode, source);
        H5CotizacionMiAutoaDoradoSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness, source);

        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#YearOfManufactured', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#ValueOfTheVehicle', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: 0
        });
      new AutoNumeric('#InsuredAmountCover', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      new AutoNumeric('#AnnualPremiumCover', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999
        });
      new AutoNumeric('#Mileage', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      new AutoNumeric('#MonthCard', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      new AutoNumeric('#YearCard', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: -9999
        });
      new AutoNumeric('#AuthorizationNumber', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    H5CotizacionMiAutoaDoradoSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#BirthdateIncluded_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5CotizacionMiAutoaDoradoSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#H5CotizacionMiAutoaDoradoFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/H5CotizacionMiAutoaDorado.aspx?id=' + $('#H5CotizacionMiAutoaDoradoFormId').val());
              
          

            });
    };




    this.ControlActions = function () {

        $('#YearOfManufactured').change(function () {
         if ($('#YearOfManufactured').val() !== null && $('#YearOfManufactured').val() !== $('#YearOfManufactured').data('oldValue')) {
             $('#YearOfManufactured').data('oldValue', $('#YearOfManufactured').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/YearOfManufacturedChange", false,
                 JSON.stringify({
                     instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                 }),
                 function (data) {
                     H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'YearOfManufacturedChange');
           });
      }          
    });
        $('#VehicleCode').change(function () {
         if ($('#VehicleCode').val() !== null && $('#VehicleCode').val() !== ($('#VehicleCode').data('oldValue') || '0').toString()) {
             $('#VehicleCode').data('oldValue', $('#VehicleCode').val() );
             app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/VehicleCodeChange", false,
                 JSON.stringify({
                     instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                 }),
                 function (data) {
                     H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'VehicleCodeChange');
             });
      }          
    });
        $('#Cotizar').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Cotizar'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/CotizarClick", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'CotizarClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#Continuar').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Continuar'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/ContinuarClick", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'ContinuarClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#ClientID').change(function () {
         if ($('#ClientID').val() !== null && $('#ClientID').val() !== $('#ClientID').data('oldValue')) {
             $('#ClientID').data('oldValue', $('#ClientID').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/ClientIDChange", false,
                 JSON.stringify({
                     instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                 }),
                 function (data) {
                     H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'ClientIDChange');
           });
      }          
    });
        $('#FirstName').change(function () {
                      var data;
$('#FirstName').val($('#FirstName').val().toUpperCase());


        });
        $('#LastName').change(function () {
                      var data;
$('#LastName').val($('#LastName').val().toUpperCase());


        });
        $('#LastName2').change(function () {
                      var data;
$('#LastName2').val($('#LastName2').val().toUpperCase());


        });
        $('#LicensePlate').change(function () {
                      var data;
$('#LicensePlate').val($('#LicensePlate').val().toUpperCase());


        });
        $('#Color').change(function () {
                      var data;
$('#Color').val($('#Color').val().toUpperCase());


        });
        $('#Chassis').change(function () {
                      var data;
$('#Chassis').val($('#Chassis').val().toUpperCase());


        });
        $('#EngineSerialNumber').change(function () {
                      var data;
$('#EngineSerialNumber').val($('#EngineSerialNumber').val().toUpperCase());


        });
        $('#button3').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button3'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/button3Click", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'button3Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#buttonGSdireccion').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#buttonGSdireccion'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/buttonGSdireccionClick", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'buttonGSdireccionClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#Accept').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Accept'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/AcceptClick", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'AcceptClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button1').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button1'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/button1Click", false,
                JSON.stringify({
                    instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'button1Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#eMail').change(function () {
                      var data;
$('#eMail').val($('#eMail').val().toUpperCase());


        });
        $('#button0').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button0'));
                btnLoading.start();

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/button0Click", false,
                JSON.stringify({
                    instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'button0Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#button2').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button2'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/button2Click", false,
                JSON.stringify({
                    instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'button2Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#button4').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button4'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/button4Click", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'button4Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#MailToProducerIndicator').change(function () {
         if ($('#MailToProducerIndicator').is(':checked') !== null && $('#MailToProducerIndicator').is(':checked') !== $('#MailToProducerIndicator').data('oldValue')){         
             $('#MailToProducerIndicator').data('oldValue', $('#MailToProducerIndicator').is(':checked') );
             
             app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/MailToProducerIndicatorChange", false,
                 JSON.stringify({
                     instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                 }),
                 function (data) {
                     H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'MailToProducerIndicatorChange');
             });
         }
        });
        $('#Cerrar').click(function (event) {
                var formInstance = $("#H5CotizacionMiAutoaDoradoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Cerrar'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/CerrarClick", false,
                          JSON.stringify({
                                        instance: H5CotizacionMiAutoaDoradoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5CotizacionMiAutoaDoradoSupport.ActionProcess(data, 'CerrarClick');
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
    
        $("#H5CotizacionMiAutoaDoradoMainForm").validate({
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
                EffectiveDate: {
                    DatePicker: true
                },
                uwcaseid: {
                    maxlength: 15
                },
                LineOfBusiness: {
                    required: true
                },
                BirthdateIncluded: {
                    required: true,
                    DatePicker: true
                },
                YearOfManufactured: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                },
                VehicleCode: {
                    required: true
                },
                ValueOfTheVehicle: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999999999
                },
                DrivingZone: {
                    required: true
                },
                UseOfVehicle: {
                    required: true
                },
                ClientID: {
                    required: true,
                    maxlength: 14
                },
                FirstName: {
                    required: true,
                    maxlength: 20
                },
                LastName: {
                    required: true,
                    maxlength: 20
                },
                LastName2: {
                    required: true,
                    maxlength: 20
                },
                Gender: {
                    required: true
                },
                LicensePlateType: {
                    required: true
                },
                LicensePlate: {
                    required: true,
                    maxlength: 10
                },
                Color: {
                    required: true,
                    maxlength: 15
                },
                Chassis: {
                    required: true,
                    maxlength: 40
                },
                EngineSerialNumber: {
                    required: true,
                    maxlength: 40
                },
                Mileage: {
                    AutoNumericMinValue: -999999999999,
                    AutoNumericMaxValue: 999999999999
                },
                CreditCardType: {
                    required: true
                },
                BankCode: {
                    required: true
                },
                CreditCardNumber: {
                    required: true,
                    maxlength: 20
                },
                MonthCard: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -99,
                    AutoNumericMaxValue: 99
                },
                YearCard: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -9999,
                    AutoNumericMaxValue: 9999
                },
                AuthorizationNumber: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                eMail: {
                    required: true,
                    maxlength: 60
                },
                eMailSend: {
                    required: true,
                    maxlength: 15
                }
            },
            messages: {
                EffectiveDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                uwcaseid: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                BirthdateIncluded: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                YearOfManufactured: {
                    AutoNumericRequired: 'El campo es requerido.',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                VehicleCode: {
                    required: 'El campo es requerido.'
                },
                ValueOfTheVehicle: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999999'
                },
                DrivingZone: {
                    required: 'El campo es requerido'
                },
                UseOfVehicle: {
                    required: 'El campo es requerido'
                },
                ClientID: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 14 caracteres máximo'
                },
                FirstName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName2: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                Gender: {
                    required: 'El campo es requerido'
                },
                LicensePlateType: {
                    required: 'El campo es requerido'
                },
                LicensePlate: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 10 caracteres máximo'
                },
                Color: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Chassis: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                EngineSerialNumber: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                Mileage: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                CreditCardType: {
                    required: 'El campo es requerido'
                },
                BankCode: {
                    required: 'El campo es requerido'
                },
                CreditCardNumber: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                MonthCard: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -99',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99'
                },
                YearCard: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                AuthorizationNumber: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                eMail: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                eMailSend: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 15 caracteres máximo'
                }
            }
        });
        $("#CoverageWithCalculatedPremiumEditForm").validate({
            rules: {
                DescriptionCover: {
                    maxlength: 120
                },
                InsuredAmountCover: {
                    AutoNumericMinValue: -999999999999,
                    AutoNumericMaxValue: 999999999999
                },
                AnnualPremiumCover: {
                    AutoNumericMinValue: -999999999999,
                    AutoNumericMaxValue: 999999999999
                }

            },
            messages: {
                DescriptionCover: {
                    maxlength: 'El campo permite 120 caracteres máximo'
                },
                InsuredAmountCover: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                AnnualPremiumCover: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                }

            }
        });

    };
    this.LookUpForProductCode = function (defaultValue, value1, source) {
        var ctrol = $('#ProductCode');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForProductCode", false,
                JSON.stringify({
                                        id: $('#H5CotizacionMiAutoaDoradoFormId').val(),
                    RiskInformationLineOfBusiness: value1
                }),
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
					      if(source !== 'Initialization')
                    ctrol.change();
            }
    };
    this.LookUpForLineOfBusiness = function (defaultValue, source) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForVehicleCode = function (defaultValue, source) {
        var ctrol = $('#VehicleCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForVehicleCode", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForCurrency = function (defaultValue, source) {
        var ctrol = $('#Currency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForCurrency", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForDrivingZone = function (defaultValue, source) {
        var ctrol = $('#DrivingZone');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForDrivingZone", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForUseOfVehicle = function (defaultValue, source) {
        var ctrol = $('#UseOfVehicle');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForUseOfVehicle", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForLicensePlateType = function (defaultValue, source) {
        var ctrol = $('#LicensePlateType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForLicensePlateType", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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
    this.LookUpForCreditCardType = function (defaultValue, source) {
        var ctrol = $('#CreditCardType_Dynamic');
        
        if (ctrol.children().length === 0) {
            app.core.AsyncWebMethod('/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForCreditCardType', false,
				       JSON.stringify({
					       id: $('#H5CotizacionMiAutoaDoradoFormId').val(),
					       value: defaultValue
				       }),
				       function (data) {             
                    ctrol.children().remove();                    
                    $.each(data.d.Data, function () {
                          ctrol.append("<div class='radio'><label><input type='radio' name='CreditCardType' id='CreditCardType_" + this['Code'] + "' value='" + this['Code'] + "'/>" + this['Description'] + "</label></div>");
                    });
                    if (defaultValue !== null)
                        $($('input:radio[name=CreditCardType][value=' + defaultValue + ']')).prop('checked', true);
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== ctrol.val())
                    $($('input:radio[name=CreditCardType][value=' + defaultValue + ']')).prop('checked', true);
    };
    this.LookUpForBankCode = function (defaultValue, source) {
        var ctrol = $('#BankCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionMiAutoaDoradoActions.aspx/LookUpForBankCode", false,
                JSON.stringify({ id: $('#H5CotizacionMiAutoaDoradoFormId').val() }),
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

    this.CoverageWithCalculatedPremiumTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'CoverageCode',
        toolbar: '#CoverageWithCalculatedPremiumtoolbar',
            columns: [{
                field: 'SelectedByDefault',
                title: 'Sel',
                checkbox: true,
                sortable: false,
                halign: 'center'
            }, {
                field: 'Description',
                title: 'Cobertura',
                events: 'CoverageWithCalculatedPremiumActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Capital asegurado',
                formatter: 'H5CotizacionMiAutoaDoradoSupport.InsuredAmountCover_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'H5CotizacionMiAutoaDoradoSupport.AnnualPremiumCover_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#CoverageWithCalculatedPremiumTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#CoverageWithCalculatedPremiumTbl');
            $('#CoverageWithCalculatedPremiumRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#CoverageWithCalculatedPremiumRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#CoverageWithCalculatedPremiumTbl').bootstrapTable('getSelections'), function (row) {		
                H5CotizacionMiAutoaDoradoSupport.CoverageWithCalculatedPremiumRowToInput(row);
                
                
                return row.CoverageCode;
            });
            
          $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('remove', {
                field: 'CoverageCode',
                values: ids
           });

            $('#CoverageWithCalculatedPremiumRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#CoverageWithCalculatedPremiumCreateBtn').click(function () {
            var formInstance = $("#CoverageWithCalculatedPremiumEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5CotizacionMiAutoaDoradoSupport.CoverageWithCalculatedPremiumShowModal($('#CoverageWithCalculatedPremiumPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#CoverageWithCalculatedPremiumPopup').find('#CoverageWithCalculatedPremiumSaveBtn').click(function () {
            var formInstance = $("#CoverageWithCalculatedPremiumEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#CoverageWithCalculatedPremiumPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#CoverageWithCalculatedPremiumSaveBtn').html();
                $('#CoverageWithCalculatedPremiumSaveBtn').html('Procesando...');
                $('#CoverageWithCalculatedPremiumSaveBtn').prop('disabled', true);

                H5CotizacionMiAutoaDoradoSupport.currentRow.SelectedByDefault = $('#SelectedByDefault').is(':checked');
                H5CotizacionMiAutoaDoradoSupport.currentRow.Description = $('#DescriptionCover').val();
                H5CotizacionMiAutoaDoradoSupport.currentRow.InsuredAmount = generalSupport.NumericValue('#InsuredAmountCover', -999999999999, 999999999999);
                H5CotizacionMiAutoaDoradoSupport.currentRow.AnnualPremium = generalSupport.NumericValue('#AnnualPremiumCover', -999999999999, 999999999999);

                $('#CoverageWithCalculatedPremiumSaveBtn').prop('disabled', false);
                $('#CoverageWithCalculatedPremiumSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('updateByUniqueId', { id: H5CotizacionMiAutoaDoradoSupport.currentRow.CoverageCode, row: H5CotizacionMiAutoaDoradoSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('append', H5CotizacionMiAutoaDoradoSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.CoverageWithCalculatedPremiumShowModal = function (md, title, row) {
        row = row || { SelectedByDefault: null, Description: null, InsuredAmount: 0, AnnualPremium: 0 };

        md.data('id', row.CoverageCode);
        md.find('.modal-title').text(title);

        H5CotizacionMiAutoaDoradoSupport.CoverageWithCalculatedPremiumRowToInput(row);
        $('#DescriptionCover').prop('disabled', (row.Description !== null));
        $('#InsuredAmountCover').prop('disabled', (row.InsuredAmount !== null));
        $('#AnnualPremiumCover').prop('disabled', (row.AnnualPremium !== null));

        md.modal('show');
    };

    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        H5CotizacionMiAutoaDoradoSupport.currentRow = row;
        $('#SelectedByDefault').prop("checked", row.SelectedByDefault);
        $('#DescriptionCover').val(row.Description);
        AutoNumeric.set('#InsuredAmountCover', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremiumCover', row.AnnualPremium);

    };


    this.InsuredAmountCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.AnnualPremiumCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Mi Auto aDorado');
        

    H5CotizacionMiAutoaDoradoSupport.ControlBehaviour();
    H5CotizacionMiAutoaDoradoSupport.ControlActions();
    H5CotizacionMiAutoaDoradoSupport.ValidateSetup();
    H5CotizacionMiAutoaDoradoSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"></table>');
    H5CotizacionMiAutoaDoradoSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));





});

window.CoverageWithCalculatedPremiumActionEvents = {
    'click .update': function (e, value, row, index) {
        H5CotizacionMiAutoaDoradoSupport.CoverageWithCalculatedPremiumShowModal($('#CoverageWithCalculatedPremiumPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
