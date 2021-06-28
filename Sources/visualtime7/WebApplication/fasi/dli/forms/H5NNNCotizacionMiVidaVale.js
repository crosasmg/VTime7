var H5NNNCotizacionMiVidaValeSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5NNNCotizacionMiVidaValeFormId').val(),
            RiskInformationEffectiveDate: generalSupport.DatePickerValueInputToObject('#RiskInformationEffectiveDate'),
            uwcaseid: parseInt(0 + $('#uwcaseid').val(), 10),
            ClientIncludedGender: $('input:radio[name=Gender]:checked').val(),
            ClientIncludedSmokerIndicator: $('input:radio[name=SmokerIndicator]:checked').val(),
            ClientIncludedBirthDate: generalSupport.DatePickerValueInputToObject('#BirthDate'),
            RiskInformationLifeLineOfBusinessInsuredAmountForCalculation: generalSupport.NumericValue('#InsuredAmountForCalculation', 0, 999999999999),
            Currency: parseInt(0 + $('#Currency').val(), 10),
            InsuredAmountSelected: parseInt(0 + $('#InsuredAmountSelected').val(), 10),
            RiskInformationPolicyPaymentFrequency: parseInt(0 + $('#PaymentFrequency').val(), 10),
            RiskInformationPaymentMethod: parseInt(0 + $('#PaymentMethod').val(), 10),
            CoverageForAmendment_CoverageForAmendment: generalSupport.NormalizeProperties($('#CoverageForAmendmentTbl').bootstrapTable('getData'), ''),
            ClienteInformaEsUsuario: $('input:radio[name=ClienteInformaEsUsuario]:checked').val(),
            ClienteInformaExiste: $('input:radio[name=ClienteInformaExiste]:checked').val(),
            UsuarioClaveEntrada: $('#UsuarioClaveEntrada').val(),
            ClientInsuredPersonClientID: $('#ClientIDAut').val(),
            ClientInsuredPersonFirstName: $('#FirstNameAut').val(),
            ClientInsuredPersonLastName: $('#LastNameAut').val(),
            ClientInsuredPersonLastName2: $('#LastName2Aut').val(),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            RiskInformationPrimaryInsuredClientHeight: generalSupport.NumericValue('#Height', 0, 9),
            RiskInformationPrimaryInsuredClientWeight: generalSupport.NumericValue('#Weight', 0, 999),
            RiskInformationPrimaryInsuredClientFirstName: $('#FirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#LastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#LastName2').val(),
            BusinessAddress: AddressSupport.GetLocalAddressBySelector("physicaladdress0"),
            QuestionnairesAvailableDiabetes: $('input:radio[name=Diabetes]:checked').val(),
            QuestionnairesAvailableHeart: $('input:radio[name=Heart]:checked').val(),
            QuestionnairesAvailableCancer: $('input:radio[name=Cancer]:checked').val(),
            BeneficiaryType: generalSupport.RadioNumericValue('BeneficiaryType'),
            Beneficiary_Beneficiary: generalSupport.NormalizeProperties($('#BeneficiaryTbl').bootstrapTable('getData'), ''),
            BeneficiaryRelationship: parseInt(0 + $('#RelationshipBD').val(), 10),
            BeneficiaryPercentageShare: generalSupport.NumericValue('#PercentageShareBP', -99999, 99999),
            TypeOfPersonBenef: generalSupport.RadioNumericValue('TypeOfPersonBenef'),
            BeneficiaryClientID: $('#ClientIDBD').val(),
            BeneficiaryClientFirstName: $('#FirstNameBP').val(),
            BeneficiaryClientLastName: $('#LastNameBP').val(),
            BeneficiaryClientLastName2: $('#LastName2BP').val(),
            BeneficiaryClientLegalName: $('#LegalNameBP').val(),
            RiskInformationAutomaticPaymentPolicyCreditCardType: generalSupport.RadioNumericValue('CreditCardType'),
            RiskInformationAutomaticPaymentPolicyBankCode: parseInt(0 + $('#BankCode').val(), 10),
            RiskInformationAutomaticPaymentPolicyCreditCardNumber: generalSupport.NumericValue('#CreditCardNumber', -99999999999999999999, 99999999999999999999),
            MesTarjeta: generalSupport.NumericValue('#MesTarjeta', -99, 99),
            AnoTarjeta: generalSupport.NumericValue('#AnoTarjeta', -9999, 9999),
            RiskInformationAutomaticPaymentPolicyAuthorizationNumber: generalSupport.NumericValue('#AuthorizationNumber', 0, 99999),
            eMail: $('#eMail').val(),
            OnLinePrintIndicator: $('#OnLinePrintIndicator').is(':checked'),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationProductCode: parseInt(0 + $('#RiskInformationProductCode').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5NNNCotizacionMiVidaValeFormId').val(data.InstanceFormId);
        $('#ProductMasterDescription').html(data.ProductMasterDescription);
        $('#RiskInformationEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, generalSupport.DateFormat()));
        $('#uwcaseid').val(data.uwcaseid);
        if($('input:radio[name=Gender][value=' + data.ClientIncludedGender +']').length===0)
           $('input:radio[name=Gender]').prop('checked', false);
        else
           $($('input:radio[name=Gender][value=' + data.ClientIncludedGender +']')).prop('checked', true);
        $('#Gender').data('oldValue', data.ClientIncludedGender);
        $('#Gender').val(data.ClientIncludedGender);

        if($('input:radio[name=SmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']').length===0)
           $('input:radio[name=SmokerIndicator]').prop('checked', false);
        else
           $($('input:radio[name=SmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']')).prop('checked', true);
        $('#SmokerIndicator').data('oldValue', data.ClientIncludedSmokerIndicator);
        $('#SmokerIndicator').val(data.ClientIncludedSmokerIndicator);

        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.ClientIncludedBirthDate, generalSupport.DateFormat()));
        AutoNumeric.set('#InsuredAmountForCalculation', data.RiskInformationLifeLineOfBusinessInsuredAmountForCalculation);
        $('#InsuredAmountSelected').data('oldValue', data.InsuredAmountSelected);
        $('#InsuredAmountSelected').val(data.InsuredAmountSelected);
        AutoNumeric.set('#AccumulatedAmount', data.AccumulatedAmount);
        if($('input:radio[name=ClienteInformaEsUsuario][value=' + data.ClienteInformaEsUsuario +']').length===0)
           $('input:radio[name=ClienteInformaEsUsuario]').prop('checked', false);
        else
           $($('input:radio[name=ClienteInformaEsUsuario][value=' + data.ClienteInformaEsUsuario +']')).prop('checked', true);
        $('#ClienteInformaEsUsuario').data('oldValue', data.ClienteInformaEsUsuario);
        $('#ClienteInformaEsUsuario').val(data.ClienteInformaEsUsuario);

        if($('input:radio[name=ClienteInformaExiste][value=' + data.ClienteInformaExiste +']').length===0)
           $('input:radio[name=ClienteInformaExiste]').prop('checked', false);
        else
           $($('input:radio[name=ClienteInformaExiste][value=' + data.ClienteInformaExiste +']')).prop('checked', true);
        $('#ClienteInformaExiste').data('oldValue', data.ClienteInformaExiste);
        $('#ClienteInformaExiste').val(data.ClienteInformaExiste);

        $('#UsuarioClaveEntrada').val(data.UsuarioClaveEntrada);
        $('#ClientIDAut').val(data.ClientInsuredPersonClientID);
        $('#FirstNameAut').val(data.ClientInsuredPersonFirstName);
        $('#LastNameAut').val(data.ClientInsuredPersonLastName);
        $('#LastName2Aut').val(data.ClientInsuredPersonLastName2);
        $('#ClientID').val(data.RiskInformationPrimaryInsuredClientClientID);
        AutoNumeric.set('#Height', data.RiskInformationPrimaryInsuredClientHeight);
        AutoNumeric.set('#Weight', data.RiskInformationPrimaryInsuredClientWeight);
        $('#FirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#LastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#LastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        AddressSupport.Initialization('physicaladdress0', data.BusinessAddress, true, false, true, true, true, false, 1, false, true);
        if($('input:radio[name=Diabetes][value=' + data.QuestionnairesAvailableDiabetes +']').length===0)
           $('input:radio[name=Diabetes]').prop('checked', false);
        else
           $($('input:radio[name=Diabetes][value=' + data.QuestionnairesAvailableDiabetes +']')).prop('checked', true);
        $('#Diabetes').data('oldValue', data.QuestionnairesAvailableDiabetes);
        $('#Diabetes').val(data.QuestionnairesAvailableDiabetes);

        if($('input:radio[name=Heart][value=' + data.QuestionnairesAvailableHeart +']').length===0)
           $('input:radio[name=Heart]').prop('checked', false);
        else
           $($('input:radio[name=Heart][value=' + data.QuestionnairesAvailableHeart +']')).prop('checked', true);
        $('#Heart').data('oldValue', data.QuestionnairesAvailableHeart);
        $('#Heart').val(data.QuestionnairesAvailableHeart);

        if($('input:radio[name=Cancer][value=' + data.QuestionnairesAvailableCancer +']').length===0)
           $('input:radio[name=Cancer]').prop('checked', false);
        else
           $($('input:radio[name=Cancer][value=' + data.QuestionnairesAvailableCancer +']')).prop('checked', true);
        $('#Cancer').data('oldValue', data.QuestionnairesAvailableCancer);
        $('#Cancer').val(data.QuestionnairesAvailableCancer);

        if($('input:radio[name=BeneficiaryType][value=' + data.BeneficiaryType +']').length===0)
           $('input:radio[name=BeneficiaryType]').prop('checked', false);
        else
           $($('input:radio[name=BeneficiaryType][value=' + data.BeneficiaryType +']')).prop('checked', true);
        $('#BeneficiaryType').data('oldValue', data.BeneficiaryType);
        $('#BeneficiaryType').val(data.BeneficiaryType);

        AutoNumeric.set('#PercentageShareBP', data.BeneficiaryPercentageShare);
        if($('input:radio[name=TypeOfPersonBenef][value=' + data.TypeOfPersonBenef +']').length===0)
           $('input:radio[name=TypeOfPersonBenef]').prop('checked', false);
        else
           $($('input:radio[name=TypeOfPersonBenef][value=' + data.TypeOfPersonBenef +']')).prop('checked', true);
        $('#TypeOfPersonBenef').data('oldValue', data.TypeOfPersonBenef);
        $('#TypeOfPersonBenef').val(data.TypeOfPersonBenef);

        $('#ClientIDBD').val(data.BeneficiaryClientID);
        $('#FirstNameBP').val(data.BeneficiaryClientFirstName);
        $('#LastNameBP').val(data.BeneficiaryClientLastName);
        $('#LastName2BP').val(data.BeneficiaryClientLastName2);
        $('#LegalNameBP').val(data.BeneficiaryClientLegalName);
        H5NNNCotizacionMiVidaValeSupport.LookUpForCreditCardType(data.RiskInformationAutomaticPaymentPolicyCreditCardType, source);

        AutoNumeric.set('#CreditCardNumber', data.RiskInformationAutomaticPaymentPolicyCreditCardNumber);
        AutoNumeric.set('#MesTarjeta', data.MesTarjeta);
        AutoNumeric.set('#AnoTarjeta', data.AnoTarjeta);
        AutoNumeric.set('#AuthorizationNumber', data.RiskInformationAutomaticPaymentPolicyAuthorizationNumber);
        $('#eMail').val(data.eMail);
        $('#OnLinePrintIndicator').prop("checked", data.OnLinePrintIndicator);

        H5NNNCotizacionMiVidaValeSupport.LookUpForCurrency(data.Currency, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForPaymentMethod(data.RiskInformationPaymentMethod, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForRelationship(source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForRelationshipBD(data.BeneficiaryRelationship, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForBankCode(data.RiskInformationAutomaticPaymentPolicyBankCode, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForRiskInformationProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness, source);
        H5NNNCotizacionMiVidaValeSupport.LookUpForPaymentFrequency(data.RiskInformationPolicyPaymentFrequency, data.RiskInformationLineOfBusiness, data.RiskInformationProductCode, data.RiskInformationPaymentMethod, source);

        if (data.CoverageForAmendment_CoverageForAmendment !== null)
            $('#CoverageForAmendmentTbl').bootstrapTable('load', data.CoverageForAmendment_CoverageForAmendment.filter(function(filterColumns) {return (filterColumns.Selected === true);}));
        if (data.Beneficiary_Beneficiary !== null)
            $('#BeneficiaryTbl').bootstrapTable('load', data.Beneficiary_Beneficiary);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#InsuredAmountForCalculation', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AccumulatedAmount', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#InsuredAmount', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      new AutoNumeric('#AnnualPremium', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999999999,
            decimalPlaces: 2,
            minimumValue: -9999999999999999
        });
      new AutoNumeric('#Height', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 2,
            minimumValue: 0
        });
      new AutoNumeric('#Weight', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 2,
            minimumValue: 0
        });
      new AutoNumeric('#PercentageShare', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: 0
        });
      new AutoNumeric('#PercentageShareBP', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      new AutoNumeric('#CreditCardNumber', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999999999999999999,
            decimalPlaces: 0,
            minimumValue: -99999999999999999999
        });
      new AutoNumeric('#MesTarjeta', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      new AutoNumeric('#AnoTarjeta', {
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
            minimumValue: 0
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    H5NNNCotizacionMiVidaValeSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#RiskInformationProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#RiskInformationProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
        $('#RiskInformationProductCode').on('change', function () {
            var value = $('#RiskInformationProductCode').val();

            if (value !== null && value !== '0') {
                var skipData = $('#RiskInformationProductCode').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#RiskInformationProductCode').data("skip", false);
                else
                    H5NNNCotizacionMiVidaValeSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#RiskInformationProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#RiskInformationProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
        $('#PaymentMethod').on('change', function () {
            var value = $('#PaymentMethod').val();

            if (value !== null && value !== '0') {
                var skipData = $('#PaymentMethod').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#PaymentMethod').data("skip", false);
                else
                    H5NNNCotizacionMiVidaValeSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#RiskInformationProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#RiskInformationProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    H5NNNCotizacionMiVidaValeSupport.LookUpForRiskInformationProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#RiskInformationProductCode').data("parentId1"))
                   $('#RiskInformationProductCode').children().remove();
        });

        $('#RiskInformationEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#RiskInformationEffectiveDate_group');
        $('#BirthDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#BirthDate_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         H5NNNCotizacionMiVidaValeSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   H5NNNCotizacionMiVidaValeSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#H5NNNCotizacionMiVidaValeFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                

                H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#H5NNNCotizacionMiVidaValeFormId').val());
				AddressSupport.Visible('physicaladdress0', true);
				AddressSupport.Enable('physicaladdress0', true);
				AddressSupport.Required('physicaladdress0', false);
 
              
          

            });
    };



    this.Beneficiary_delete = function (row, $modal) {
          var data;
        $('#BeneficiaryTbl').bootstrapTable('remove', {field: 'ClientID', values: [$('#ClientIDBenef').val()]});
        $('#text3').toggleClass('hidden', false);
        $('#text3Label').toggleClass('hidden', false);
        if ('[text3]' !== null){

        }

    };
    this.Beneficiary_update = function (row, $modal) {
          var data;
        $('#BeneficiaryTbl').bootstrapTable('updateByUniqueId', { id: row.ClientID, row: row });
        $modal.modal('hide');
        $('#text3').toggleClass('hidden', false);
        $('#text3Label').toggleClass('hidden', false);

    };

    this.ControlActions =   function () {

        $('#InsuredAmountForCalculation').change(function () {
                      var data;
                $("#Acepto" ).hide();


        });
        $('#InsuredAmountSelected').change(function () {
         if ($('#InsuredAmountSelected').val() !== null && $('#InsuredAmountSelected').val() !== ($('#InsuredAmountSelected').data('oldValue') || '0').toString()) {
             $('#InsuredAmountSelected').data('oldValue', $('#InsuredAmountSelected').val() );
             app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/InsuredAmountSelectedChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'InsuredAmountSelectedChange');
             });
      }          
    });
        $('#Cotizar').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Cotizar'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/CotizarClick", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'CotizarClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button2').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button2'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button2Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button2Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
       $('input:radio[name=ClienteInformaEsUsuario]').change(function () {
                     var data;
                if ($('input:radio[name=ClienteInformaEsUsuario]:checked').val() === 'true'){
                    $('#zone38').toggleClass('hidden', false);
                    }                    
                    else {
                    $('#zone38').toggleClass('hidden', true);

                        }


        });
        $('#UsuarioClaveEntrada').change(function () {
                      var data;
$('#UsuarioClaveEntrada').val($('#UsuarioClaveEntrada').val().toUpperCase());


        });
        $('#FirstNameAut').change(function () {
                      var data;
$('#FirstNameAut').val($('#FirstNameAut').val().toUpperCase());


        });
        $('#LastNameAut').change(function () {
                      var data;
$('#LastNameAut').val($('#LastNameAut').val().toUpperCase());


        });
        $('#LastName2Aut').change(function () {
                      var data;
$('#LastName2Aut').val($('#LastName2Aut').val().toUpperCase());


        });
        $('#btnAutenticar').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#btnAutenticar'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/btnAutenticarClick", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'btnAutenticarClick');
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
           
            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/ClientIDChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'ClientIDChange');
           });
      }          
    });
        $('#LastName').change(function () {
                      var data;
$('#LastName').val($('#LastName').val().toUpperCase());


        });
        $('#LastName2').change(function () {
                      var data;
$('#LastName2').val($('#LastName2').val().toUpperCase());


        });
        $('#button8').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button8'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button8Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button8Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button14').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button14'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button14Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button14Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('input:radio[name=BeneficiaryType]').change(function () {
         if ($('input:radio[name=BeneficiaryType]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/BeneficiaryTypeChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'BeneficiaryTypeChange');
             });
      }          
    });
        $('#button1').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button1'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button1Click", false,
                JSON.stringify({
                    instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button1Click');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#CompleteClientName').change(function () {
                      var data;
$('#CompleteClientName').val($('#CompleteClientName').val().toUpperCase());


        });
        $('input:radio[name=TypeOfPersonBenef]').change(function () {
         if ($('input:radio[name=TypeOfPersonBenef]:checked').val() !== null) {
           app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/TypeOfPersonBenefChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'TypeOfPersonBenefChange');
             });
      }          
    });
        $('#ClientIDBD').change(function () {
         if ($('#ClientIDBD').val() !== null && $('#ClientIDBD').val() !== $('#ClientIDBD').data('oldValue')) {
             $('#ClientIDBD').data('oldValue', $('#ClientIDBD').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/ClientIDBDChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'ClientIDBDChange');
           });
      }          
    });
        $('#FirstNameBP').change(function () {
                      var data;
$('#FirstNameBP').val($('#FirstNameBP').val().toUpperCase());


        });
        $('#LastNameBP').change(function () {
                      var data;
$('#LastNameBP').val($('#LastNameBP').val().toUpperCase());


        });
        $('#LastName2BP').change(function () {
                      var data;
$('#LastName2BP').val($('#LastName2BP').val().toUpperCase());


        });
        $('#LegalNameBP').change(function () {
                      var data;
$('#LegalNameBP').val($('#LegalNameBP').val().toUpperCase());


        });
        $('#button33').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button33'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button33Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button33Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#Cancelar').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#Cancelar'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/CancelarClick", false,
                JSON.stringify({
                    instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'CancelarClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });
        $('#button12').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button12'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button12Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button12Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#buttonGPago').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#buttonGPago'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/buttonGPagoClick", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'buttonGPagoClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button0').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button0'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button0Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button0Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button19').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button19'));
                    btnLoading.start();

                    app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/button19Click", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'button19Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#EnviarCotizacionEmail').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#EnviarCotizacionEmail'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/EnviarCotizacionEmailClick", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'EnviarCotizacionEmailClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#eMail').change(function () {
         if ($('#eMail').val() !== null && $('#eMail').val() !== $('#eMail').data('oldValue')) {
             $('#eMail').data('oldValue', $('#eMail').val() );             
           
            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/eMailChange", false,
                 JSON.stringify({
                     instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                 }),
                 function (data) {
                     H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'eMailChange');
           });
      }          
    });
        $('#Accept').click(function (event) {
                var formInstance = $("#H5NNNCotizacionMiVidaValeMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#Accept'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/AcceptClick", false,
                          JSON.stringify({
                                        instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'AcceptClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#Rechazar').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#Rechazar'));
                btnLoading.start();

            app.core.AsyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/RechazarClick", false,
                JSON.stringify({
                    instance: H5NNNCotizacionMiVidaValeSupport.InputToObject()
                }),
                function (data) {
                    btnLoading.stop();

                    H5NNNCotizacionMiVidaValeSupport.ActionProcess(data, 'RechazarClick');
                },
                function () {
                    btnLoading.stop();
                });
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#H5NNNCotizacionMiVidaValeMainForm").validate({
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
                ProductMasterDescription: {
                    maxlength: 60
                },
                RiskInformationEffectiveDate: {
                    DatePicker: true
                },
                uwcaseid: {
                    maxlength: 15
                },
                Gender: {
                    required: true
                },
                SmokerIndicator: {
                    required: true
                },
                BirthDate: {
                    required: true,
                    DatePicker: true
                },
                InsuredAmountForCalculation: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                InsuredAmountSelected: {
                    required: true
                },
                AccumulatedAmount: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999999999999
                },
                UsuarioClaveEntrada: {
                    required: true,
                    maxlength: 60,
                    email: true
                },
                ClientIDAut: {
                    required: true,
                    maxlength: 14
                },
                FirstNameAut: {
                    required: true,
                    maxlength: 20
                },
                LastNameAut: {
                    required: true,
                    maxlength: 20
                },
                LastName2Aut: {
                    required: true,
                    maxlength: 20
                },
                ClientID: {
                    required: true,
                    maxlength: 14
                },
                Height: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9
                },
                Weight: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 999
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
                Diabetes: {
                    required: true
                },
                Heart: {
                    required: true
                },
                Cancer: {
                    required: true
                },
                RelationshipBD: {
                    required: true
                },
                PercentageShareBP: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                TypeOfPersonBenef: {
                    required: true
                },
                ClientIDBD: {
                    required: true,
                    maxlength: 14
                },
                FirstNameBP: {
                    required: true,
                    maxlength: 20
                },
                LastNameBP: {
                    required: true,
                    maxlength: 20
                },
                LastName2BP: {
                    required: true,
                    maxlength: 20
                },
                LegalNameBP: {
                    required: true,
                    maxlength: 60
                },
                CreditCardType: {
                    required: true
                },
                BankCode: {
                    required: true
                },
                CreditCardNumber: {
                    AutoNumericMinValue: -99999999999999999999,
                    AutoNumericMaxValue: 99999999999999999999
                },
                MesTarjeta: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -99,
                    AutoNumericMaxValue: 99
                },
                AnoTarjeta: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -9999,
                    AutoNumericMaxValue: 9999
                },
                AuthorizationNumber: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                },
                eMail: {
                    required: true,
                    maxlength: 60,
                    email: true
                },
                LineOfBusiness: {
                    required: true
                },
                RiskInformationProductCode: {
                    required: true
                }
            },
            messages: {
                ProductMasterDescription: {
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                RiskInformationEffectiveDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                uwcaseid: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Gender: {
                    required: 'El campo es requerido.'
                },
                SmokerIndicator: {
                    required: 'El campo es requerido.'
                },
                BirthDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                InsuredAmountForCalculation: {
                    AutoNumericRequired: 'Incluya suma asegurada',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                InsuredAmountSelected: {
                    required: 'El campo es requerido.'
                },
                AccumulatedAmount: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                UsuarioClaveEntrada: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 60 caracteres máximo',
                    email: 'Debes ingresar una dirección de correo electrónico válido'
                },
                ClientIDAut: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 14 caracteres máximo'
                },
                FirstNameAut: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastNameAut: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName2Aut: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                ClientID: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 14 caracteres máximo'
                },
                Height: {
                    AutoNumericRequired: 'El campo es requerido.',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9'
                },
                Weight: {
                    AutoNumericRequired: 'El campo es requerido.',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999'
                },
                FirstName: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName2: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                Diabetes: {
                    required: 'El campo es requerido'
                },
                Heart: {
                    required: 'El campo es requerido'
                },
                Cancer: {
                    required: 'El campo es requerido'
                },
                RelationshipBD: {
                    required: 'El campo es requerido.'
                },
                PercentageShareBP: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                TypeOfPersonBenef: {
                    required: 'El campo es requerido'
                },
                ClientIDBD: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 14 caracteres máximo'
                },
                FirstNameBP: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastNameBP: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LastName2BP: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                LegalNameBP: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                CreditCardType: {
                    required: 'El campo es requerido'
                },
                BankCode: {
                    required: 'El campo es requerido'
                },
                CreditCardNumber: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999999999999999999'
                },
                MesTarjeta: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -99',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99'
                },
                AnoTarjeta: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                AuthorizationNumber: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                eMail: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 60 caracteres máximo',
                    email: 'Debes ingresar una dirección de correo electrónico válido'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                RiskInformationProductCode: {
                    required: 'El campo es requerido.'
                }
            }
        });
        $("#CoverageForAmendmentEditForm").validate({
            rules: {
                DescriptionOfCoverage: {
                    maxlength: 25
                },
                InsuredAmount: {
                    AutoNumericMinValue: -999999999999999999,
                    AutoNumericMaxValue: 999999999999999999
                },
                AnnualPremium: {
                    AutoNumericMinValue: -9999999999999999,
                    AutoNumericMaxValue: 9999999999999999
                },
                Consecutive: {
                    required: true,
                    maxlength: 5
                }

            },
            messages: {
                DescriptionOfCoverage: {
                    maxlength: 'El campo permite 25 caracteres máximo'
                },
                InsuredAmount: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999999'
                },
                AnnualPremium: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999999999'
                },
                Consecutive: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 5 caracteres máximo'
                }

            }
        });
        $("#BeneficiaryEditForm").validate({
            rules: {
                ClientIDBenef: {
                    required: true,
                    maxlength: 14
                },
                CompleteClientName: {
                    maxlength: 63
                },
                Relationship: {
                    required: true
                },
                PercentageShare: {
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 99999
                }

            },
            messages: {
                ClientIDBenef: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 14 caracteres máximo'
                },
                CompleteClientName: {
                    maxlength: 'El campo permite 63 caracteres máximo'
                },
                Relationship: {
                    required: 'El campo es requerido.'
                },
                PercentageShare: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }

            }
        });

    };
    this.LookUpForCurrency = function (defaultValue, source) {
        var ctrol = $('#Currency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForCurrency", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
    this.LookUpForPaymentFrequency = function (defaultValue, value1, value2, value3, source) {
        var ctrol = $('#PaymentFrequency');
        var parentId1 = ctrol.data("parentId1");
        var parentId2 = ctrol.data("parentId2");
        var parentId3 = ctrol.data("parentId3");
        
        if (((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) || ((typeof parentId2 == 'undefined' && typeof value2 !== 'undefined') || parentId2 !== value2) || ((typeof parentId3 == 'undefined' && typeof value3 !== 'undefined') || parentId3 !== value3)) {
            ctrol.data("parentId1", value1);
            ctrol.data("parentId2", value2);
            ctrol.data("parentId3", value3);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForPaymentFrequency", false,
                JSON.stringify({
                                        id: $('#H5NNNCotizacionMiVidaValeFormId').val(),
                    RiskInformationLineOfBusiness: value1,
                     RiskInformationProductCode: value2,
                     RiskInformationPaymentMethod: value3
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
    this.LookUpForPaymentMethod = function (defaultValue, source) {
        var ctrol = $('#PaymentMethod');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForPaymentMethod", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
    this.LookUpForRelationshipFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Relationship>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRelationship = function (defaultValue, source) {
        var ctrol = $('#Relationship');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForRelationship", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
    this.LookUpForRelationshipBD = function (defaultValue, source) {
        var ctrol = $('#RelationshipBD');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForRelationshipBD", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
            app.core.AsyncWebMethod('/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForCreditCardType', false,
				       JSON.stringify({
					       id: $('#H5NNNCotizacionMiVidaValeFormId').val(),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForBankCode", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
    this.LookUpForLineOfBusiness = function (defaultValue, source) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForLineOfBusiness", false,
                JSON.stringify({ id: $('#H5NNNCotizacionMiVidaValeFormId').val() }),
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
    this.LookUpForRiskInformationProductCode = function (defaultValue, value1, source) {
        var ctrol = $('#RiskInformationProductCode');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/H5NNNCotizacionMiVidaValeActions.aspx/LookUpForRiskInformationProductCode", false,
                JSON.stringify({
                                        id: $('#H5NNNCotizacionMiVidaValeFormId').val(),
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

    this.CoverageForAmendmentTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Consecutive',
            showFooter: true,
            toolbar: '#CoverageForAmendmenttoolbar',
            columns: [{
                field: 'DescriptionOfCoverage',
                title: 'Cobertura',
                events: 'CoverageForAmendmentActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Suma asegurada',
                formatter: 'H5NNNCotizacionMiVidaValeSupport.InsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima Anual',
                formatter: 'H5NNNCotizacionMiVidaValeSupport.AnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                falign: 'right',
                footerFormatter: 'H5NNNCotizacionMiVidaValeSupport.AnnualPremium_FooterFormatter'
            }, {
                field: 'Consecutive',
                title: 'Consecutivo',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });


        $('#CoverageForAmendmentTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#CoverageForAmendmentTbl');
            $('#CoverageForAmendmentRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#CoverageForAmendmentRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#CoverageForAmendmentTbl').bootstrapTable('getSelections'), function (row) {		
                H5NNNCotizacionMiVidaValeSupport.CoverageForAmendmentRowToInput(row);
                
                
                return row.Consecutive;
            });
            
          $('#CoverageForAmendmentTbl').bootstrapTable('remove', {
                field: 'Consecutive',
                values: ids
           });

            $('#CoverageForAmendmentRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#CoverageForAmendmentCreateBtn').click(function () {
            var formInstance = $("#CoverageForAmendmentEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5NNNCotizacionMiVidaValeSupport.CoverageForAmendmentShowModal($('#CoverageForAmendmentPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#CoverageForAmendmentPopup').find('#CoverageForAmendmentSaveBtn').click(function () {
            var formInstance = $("#CoverageForAmendmentEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#CoverageForAmendmentPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#CoverageForAmendmentSaveBtn').html();
                $('#CoverageForAmendmentSaveBtn').html('Procesando...');
                $('#CoverageForAmendmentSaveBtn').prop('disabled', true);

                H5NNNCotizacionMiVidaValeSupport.currentRow.DescriptionOfCoverage = $('#DescriptionOfCoverage').val();
                H5NNNCotizacionMiVidaValeSupport.currentRow.InsuredAmount = generalSupport.NumericValue('#InsuredAmount', -999999999999999999, 999999999999999999);
                H5NNNCotizacionMiVidaValeSupport.currentRow.AnnualPremium = generalSupport.NumericValue('#AnnualPremium', -9999999999999999, 9999999999999999);
                H5NNNCotizacionMiVidaValeSupport.currentRow.Consecutive = parseInt(0 + $('#Consecutive').val(), 10);

                $('#CoverageForAmendmentSaveBtn').prop('disabled', false);
                $('#CoverageForAmendmentSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#CoverageForAmendmentTbl').bootstrapTable('updateByUniqueId', { id: H5NNNCotizacionMiVidaValeSupport.currentRow.Consecutive, row: H5NNNCotizacionMiVidaValeSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#CoverageForAmendmentTbl').bootstrapTable('append', H5NNNCotizacionMiVidaValeSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.CoverageForAmendmentShowModal = function (md, title, row) {
        row = row || { DescriptionOfCoverage: null, InsuredAmount: 0, AnnualPremium: 0, Consecutive: 0 };

        md.data('id', row.Consecutive);
        md.find('.modal-title').text(title);

        H5NNNCotizacionMiVidaValeSupport.CoverageForAmendmentRowToInput(row);
        $('#DescriptionOfCoverage').prop('disabled', (row.Consecutive !== 0));
        $('#InsuredAmount').prop('disabled', (row.Consecutive !== 0));
        $('#AnnualPremium').prop('disabled', true);
        $('#Consecutive').prop('disabled', true);

        md.appendTo("body");
        md.modal('show');
    };

    this.CoverageForAmendmentRowToInput = function (row) {
        H5NNNCotizacionMiVidaValeSupport.currentRow = row;
        $('#DescriptionOfCoverage').val(row.DescriptionOfCoverage);
        AutoNumeric.set('#InsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremium', row.AnnualPremium);
        $('#Consecutive').val(row.Consecutive);

    };
    this.BeneficiaryTblSetup = function (table) {
        H5NNNCotizacionMiVidaValeSupport.LookUpForRelationship('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
            showFooter: true,
            toolbar: '#Beneficiarytoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ClientID',
                title: 'Código de Cliente',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientCompleteClientName',
                title: 'Nombre',
                events: 'BeneficiaryActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Relationship',
                title: 'Relación con el asegurado',
                formatter: 'H5NNNCotizacionMiVidaValeSupport.LookUpForRelationshipFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación en la póliza',
                formatter: 'H5NNNCotizacionMiVidaValeSupport.PercentageShare_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                falign: 'right',
                footerFormatter: 'H5NNNCotizacionMiVidaValeSupport.PercentageShare_FooterFormatter'
            }]
        });


        $('#BeneficiaryTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#BeneficiaryTbl');
            $('#BeneficiaryRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#BeneficiaryRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#BeneficiaryTbl').bootstrapTable('getSelections'), function (row) {		
                H5NNNCotizacionMiVidaValeSupport.BeneficiaryRowToInput(row);
                H5NNNCotizacionMiVidaValeSupport.Beneficiary_delete(row, null);
                
                return row.ClientID;
            });

            $('#BeneficiaryRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#BeneficiaryCreateBtn').click(function () {
            var formInstance = $("#BeneficiaryEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            H5NNNCotizacionMiVidaValeSupport.BeneficiaryShowModal($('#BeneficiaryPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#BeneficiaryPopup').find('#BeneficiarySaveBtn').click(function () {
            var formInstance = $("#BeneficiaryEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#BeneficiaryPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#BeneficiarySaveBtn').html();
                $('#BeneficiarySaveBtn').html('Procesando...');
                $('#BeneficiarySaveBtn').prop('disabled', true);

                H5NNNCotizacionMiVidaValeSupport.currentRow.ClientID = $('#ClientIDBenef').val();
                H5NNNCotizacionMiVidaValeSupport.currentRow.ClientCompleteClientName = $('#CompleteClientName').val();
                H5NNNCotizacionMiVidaValeSupport.currentRow.Relationship = parseInt(0 + $('#Relationship').val(), 10);
                H5NNNCotizacionMiVidaValeSupport.currentRow.PercentageShare = generalSupport.NumericValue('#PercentageShare', 0, 99999);

                $('#BeneficiarySaveBtn').prop('disabled', false);
                $('#BeneficiarySaveBtn').html(caption);

                if (wm === 'Update') {
                    H5NNNCotizacionMiVidaValeSupport.Beneficiary_update(H5NNNCotizacionMiVidaValeSupport.currentRow, $modal);
                }
                else {                    
                    $('#BeneficiaryTbl').bootstrapTable('append', H5NNNCotizacionMiVidaValeSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.BeneficiaryShowModal = function (md, title, row) {
        row = row || { ClientID: null, ClientCompleteClientName: null, Relationship: 0, PercentageShare: 0 };

        md.data('id', row.ClientID);
        md.find('.modal-title').text(title);

        H5NNNCotizacionMiVidaValeSupport.BeneficiaryRowToInput(row);
        $('#CompleteClientName').prop('disabled', (row.ClientID !== ''));

        md.appendTo("body");
        md.modal('show');
    };

    this.BeneficiaryRowToInput = function (row) {
        H5NNNCotizacionMiVidaValeSupport.currentRow = row;
        $('#ClientIDBenef').val(row.ClientID);
        $('#CompleteClientName').val(row.ClientCompleteClientName);
        H5NNNCotizacionMiVidaValeSupport.LookUpForRelationship(row.Relationship, '');
        $('#Relationship').trigger('change');
        AutoNumeric.set('#PercentageShare', row.PercentageShare);

    };



    this.AnnualPremium_FooterFormatter = function (data) {
        var value = data.reduce(function (sum, row) { return sum + row.AnnualPremium; }, 0);
        return 'Total  ' + AutoNumeric.format(isNaN(value) ? 0 : value + '', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999999999,
            decimalPlaces: 2,
            minimumValue: -9999999999999999
        });
    };
    this.PercentageShare_FooterFormatter = function (data) {
        var value = data.reduce(function (sum, row) { return sum + row.PercentageShare; }, 0);
        return '% Total participación ' + AutoNumeric.format(isNaN(value) ? 0 : value + '', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: 0
        });
    };


    this.InsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.AnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999999999,
            decimalPlaces: 2,
            minimumValue: -9999999999999999
        });
      };
    this.PercentageShare_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: 0
        });
      };



  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Mi Vida vale');
        

    H5NNNCotizacionMiVidaValeSupport.ControlBehaviour();
    H5NNNCotizacionMiVidaValeSupport.ControlActions();
    H5NNNCotizacionMiVidaValeSupport.ValidateSetup();
    H5NNNCotizacionMiVidaValeSupport.Initialization();

    $("#CoverageForAmendmentTblPlaceHolder").replaceWith('<table id="CoverageForAmendmentTbl"><caption >COBERTURAS</caption></table>');
    H5NNNCotizacionMiVidaValeSupport.CoverageForAmendmentTblSetup($('#CoverageForAmendmentTbl'));
    $("#BeneficiaryTblPlaceHolder").replaceWith('<table id="BeneficiaryTbl"><caption >Beneficiarios</caption></table>');
    H5NNNCotizacionMiVidaValeSupport.BeneficiaryTblSetup($('#BeneficiaryTbl'));




    $('.nav-tabs li').click(function () {
   if ($(this).children('a').prop('id') === 'tab18') {
      $('#tab0').addClass('hidden');
      $('#zonegeneral').addClass('hidden');
   }
});

  };
};

$(document).ready(function () {
   H5NNNCotizacionMiVidaValeSupport.Init();
});

window.CoverageForAmendmentActionEvents = {
    'click .update': function (e, value, row, index) {
        H5NNNCotizacionMiVidaValeSupport.CoverageForAmendmentShowModal($('#CoverageForAmendmentPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.BeneficiaryActionEvents = {
    'click .update': function (e, value, row, index) {
        H5NNNCotizacionMiVidaValeSupport.BeneficiaryShowModal($('#BeneficiaryPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
