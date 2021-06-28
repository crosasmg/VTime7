var HT5NNCotizacionMiInversionSeguraIISupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val(),
            RangeAge: $('input:radio[name=RangeAge]:checked').val(),
            Parametro2: $('input:radio[name=Parametro2]:checked').val(),
            Parametro3: $('input:radio[name=Parametro3]:checked').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            ClientIncludedGender: $('input:radio[name=ClientGender]:checked').val(),
            ClientIncludedSmokerIndicator: $('input:radio[name=ClientSmokerIndicator]:checked').val(),
            ClientIncludedBirthDate: $('#ClientBirthDate').val() !== '' ? moment($('#ClientBirthDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            InvestmentFundRelatedToPolicy_InvestmentFundRelatedToPolicy: generalSupport.NormalizeProperties($('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('getData'), ''),
            Currency: parseInt(0 + $('#Currency').val(), 10),
            InsuredAmountSelected: $('input:radio[name=InsuredAmountSelected]:checked').val(),
            PaymentsPlanProjectedAnnualPayments: AutoNumeric.getNumber('#ProjectedAnnualPayments'),
            RiskInformationLifeLineOfBusinessGuaranteedInterestInInvestment: AutoNumeric.getNumber('#GuaranteedInterestInInvestment'),
            RiskInformationLifeLineOfBusinessTypeOfIndemnity: parseInt(0 + $('#TypeOfIndemnity').val(), 10),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            RiskInformationPrimaryInsuredClientFirstName: $('#ClientFirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#ClientLastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#ClientLastName2').val(),
            AddressPhysicalAddressDLI: AddressSupport.GetLocalAddressBySelector("physicaladdress21"),
            AddresseMailDLIeMailAddresseMail: $('#eMailservice').val(),
            QuestionnairesAvailableDiabetes: $('input:radio[name=Diabetes]:checked').val(),
            QuestionnairesAvailableHeart: $('input:radio[name=Heart]:checked').val(),
            QuestionnairesAvailableCancer: $('input:radio[name=Cancer]:checked').val(),
            BeneficiaryType: $('input:radio[name=BeneficiaryTypeBeneficia]:checked').val(),
            BeneficiaryBeneficia_Beneficiary: generalSupport.NormalizeProperties($('#BeneficiaryBeneficiaTbl').bootstrapTable('getData'), ''),
            AgregarBeneficiario: $('#AgregarBeneficiarioBeneficia').is(':checked'),
            BeneficiaryRelationship: parseInt(0 + $('#RelationshipBDBeneficia').val(), 10),
            BeneficiaryPercentageShare: AutoNumeric.getNumber('#PercentageShareBPBeneficia'),
            TypeOfPersonBenef: $('input:radio[name=TypeOfPersonBenefBeneficia]:checked').val(),
            BeneficiaryClientID: $('#ClientIDBDBeneficia').val(),
            BeneficiaryClientFirstName: $('#FirstNameBPBeneficia').val(),
            BeneficiaryClientLastName: $('#LastNameBPBeneficia').val(),
            BeneficiaryClientLastName2: $('#LastName2BPBeneficia').val(),
            BeneficiaryClientLegalName: $('#LegalNameBPBeneficia').val(),
            eMail: $('#eMail').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionMiInversionSeguraIIFormId').val(data.InstanceFormId);
        if($('input:radio[name=RangeAge][value=' + data.RangeAge +']').length===0)
           $('input:radio[name=RangeAge]').prop('checked', false);
        else
           $($('input:radio[name=RangeAge][value=' + data.RangeAge +']')).prop('checked', true);
        $('#RangeAge').data('oldValue', data.RangeAge);
        $('#RangeAge').val(data.RangeAge);

        if($('input:radio[name=Parametro2][value=' + data.Parametro2 +']').length===0)
           $('input:radio[name=Parametro2]').prop('checked', false);
        else
           $($('input:radio[name=Parametro2][value=' + data.Parametro2 +']')).prop('checked', true);
        $('#Parametro2').data('oldValue', data.Parametro2);
        $('#Parametro2').val(data.Parametro2);

        if($('input:radio[name=Parametro3][value=' + data.Parametro3 +']').length===0)
           $('input:radio[name=Parametro3]').prop('checked', false);
        else
           $($('input:radio[name=Parametro3][value=' + data.Parametro3 +']')).prop('checked', true);
        $('#Parametro3').data('oldValue', data.Parametro3);
        $('#Parametro3').val(data.Parametro3);

        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').html(data.uwcaseid);
        if($('input:radio[name=ClientGender][value=' + data.ClientIncludedGender +']').length===0)
           $('input:radio[name=ClientGender]').prop('checked', false);
        else
           $($('input:radio[name=ClientGender][value=' + data.ClientIncludedGender +']')).prop('checked', true);
        $('#ClientGender').data('oldValue', data.ClientIncludedGender);
        $('#ClientGender').val(data.ClientIncludedGender);

        if($('input:radio[name=ClientSmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']').length===0)
           $('input:radio[name=ClientSmokerIndicator]').prop('checked', false);
        else
           $($('input:radio[name=ClientSmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']')).prop('checked', true);
        $('#ClientSmokerIndicator').data('oldValue', data.ClientIncludedSmokerIndicator);
        $('#ClientSmokerIndicator').val(data.ClientIncludedSmokerIndicator);

        $('#ClientBirthDate').val(generalSupport.ToJavaScriptDateCustom(data.ClientIncludedBirthDate, 'DD/MM/YYYY'));
        if($('input:radio[name=InsuredAmountSelected][value=' + data.InsuredAmountSelected +']').length===0)
           $('input:radio[name=InsuredAmountSelected]').prop('checked', false);
        else
           $($('input:radio[name=InsuredAmountSelected][value=' + data.InsuredAmountSelected +']')).prop('checked', true);
        $('#InsuredAmountSelected').data('oldValue', data.InsuredAmountSelected);
        $('#InsuredAmountSelected').val(data.InsuredAmountSelected);

        AutoNumeric.set('#TotalOriginalAnnualPremium', data.RiskInformationTotalOriginalAnnualPremium);
        AutoNumeric.set('#ProjectedAnnualPayments', data.PaymentsPlanProjectedAnnualPayments);
        AutoNumeric.set('#GuaranteedInterestInInvestment', data.RiskInformationLifeLineOfBusinessGuaranteedInterestInInvestment);
        chartSupport.Initialization('chart0', {
            type: 'line',
            Title: 'Proyección',
            LabelsPropertiesName: 'YearOfThePolicy',
            YAxesTitle: 'Valores',
            XAxesTitle: 'Año póliza',
            Series: {
                Data: data.PROJECTVULCollection,
                Definitions: [
                     {
                        label: 'Aporte',
                        argument: 'AccumulatdAmount',
                        backgroundColor: null
                    },                     {
                        label: 'Valor póliza',
                        argument: 'PolicyValueOfProjectedProfitablity',
                        backgroundColor: null
                    },                     {
                        label: 'Rescate',
                        argument: 'SurrenderAvailableAmount',
                        backgroundColor: null
                    },                     {
                        label: 'Capital',
                        argument: 'DeathCoverageAmount',
                        backgroundColor: null
                    }                
                ]
            }
        });
        $('#ClientID').val(data.RiskInformationPrimaryInsuredClientClientID);
        $('#ClientFirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#ClientLastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#ClientLastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        AddressSupport.Initialization('physicaladdress21', data.AddressPhysicalAddressDLI, true, false);
        $('#eMailservice').val(data.AddresseMailDLIeMailAddresseMail);
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

        if($('input:radio[name=BeneficiaryTypeBeneficia][value=' + data.BeneficiaryType +']').length===0)
           $('input:radio[name=BeneficiaryTypeBeneficia]').prop('checked', false);
        else
           $($('input:radio[name=BeneficiaryTypeBeneficia][value=' + data.BeneficiaryType +']')).prop('checked', true);
        $('#BeneficiaryTypeBeneficia').data('oldValue', data.BeneficiaryType);
        $('#BeneficiaryTypeBeneficia').val(data.BeneficiaryType);

        $('#AgregarBeneficiarioBeneficia').prop("checked", data.AgregarBeneficiario);
        AutoNumeric.set('#PercentageShareBPBeneficia', data.BeneficiaryPercentageShare);
        if($('input:radio[name=TypeOfPersonBenefBeneficia][value=' + data.TypeOfPersonBenef +']').length===0)
           $('input:radio[name=TypeOfPersonBenefBeneficia]').prop('checked', false);
        else
           $($('input:radio[name=TypeOfPersonBenefBeneficia][value=' + data.TypeOfPersonBenef +']')).prop('checked', true);
        $('#TypeOfPersonBenefBeneficia').data('oldValue', data.TypeOfPersonBenef);
        $('#TypeOfPersonBenefBeneficia').val(data.TypeOfPersonBenef);

        $('#ClientIDBDBeneficia').val(data.BeneficiaryClientID);
        $('#FirstNameBPBeneficia').val(data.BeneficiaryClientFirstName);
        $('#LastNameBPBeneficia').val(data.BeneficiaryClientLastName);
        $('#LastName2BPBeneficia').val(data.BeneficiaryClientLastName2);
        $('#LegalNameBPBeneficia').val(data.BeneficiaryClientLegalName);
        $('#eMail').val(data.eMail);

        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund();
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund2();
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForCurrency(data.Currency);
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForTypeOfIndemnity(data.RiskInformationLifeLineOfBusinessTypeOfIndemnity);
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForRelationshipBeneficia();
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForRelationshipBDBeneficia(data.BeneficiaryRelationship);
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);

        if (data.InvestmentFundProfile_InvestmentFundRelatedToPolicy !== null)
            $('#InvestmentFundProfileTbl').bootstrapTable('load', data.InvestmentFundProfile_InvestmentFundRelatedToPolicy);
        if (data.InvestmentFundRelatedToPolicy_InvestmentFundRelatedToPolicy !== null)
            $('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('load', data.InvestmentFundRelatedToPolicy_InvestmentFundRelatedToPolicy);
        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return filterColumns.SelectedByDefault == true;}));
        if (data.PROJECTVUL_PROJECTVUL !== null)
            $('#PROJECTVULTbl').bootstrapTable('load', data.PROJECTVUL_PROJECTVUL);
        if (data.BeneficiaryBeneficia_Beneficiary !== null)
            $('#BeneficiaryBeneficiaTbl').bootstrapTable('load', data.BeneficiaryBeneficia_Beneficiary);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#PercentageShare2', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      new AutoNumeric('#ProjectedProfitability2', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      new AutoNumeric('#TotalOriginalAnnualPremium', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      new AutoNumeric('#ProjectedAnnualPayments', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#GuaranteedInterestInInvestment', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999999",
            decimalPlaces: 2,
            minimumValue: "-99999999"
        });
      new AutoNumeric('#PercentageShareBeneficia', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#PercentageShareBPBeneficia', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionMiInversionSeguraIISupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#ClientBirthDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionMiInversionSeguraIISupport.ObjectToInput(data.d.Data);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        $.LoadingOverlay("show");
        $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraII.aspx?id=' + $('#HT5NNCotizacionMiInversionSeguraIIFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("id");

            switch (target) {
                case "tab1":
                    chartSupport.Update('chart0');
                    break;

            }
        });
        $('#ProfileBoton').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#ProfileBoton'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/ProfileBotonClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#button0').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button0'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button0Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#button16').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button16'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button16Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#Cotizar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Cotizar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/CotizarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#Acepto').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Acepto'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/AceptoClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#Proyectar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Proyectar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/ProyectarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#AceptarProyeccion').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#AceptarProyeccion'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/AceptarProyeccionClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#ClientID').change(function () {
         if ($('#ClientID').val() !== null && $('#ClientID').val() !== $('#ClientID').data('oldValue')) {
             $('#ClientID').data('oldValue', $('#ClientID').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/ClientIDChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button23').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button23'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button23Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#button22').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button22'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button22Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('input:radio[name=BeneficiaryTypeBeneficia]').change(function () {
         if ($('input:radio[name=BeneficiaryTypeBeneficia]:checked').val() !== null) {
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/BeneficiaryTypeBeneficiaChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#AgregarBeneficiarioBeneficia').change(function () {
         if ($('#AgregarBeneficiarioBeneficia').is(':checked') !== null && $('#AgregarBeneficiarioBeneficia').is(':checked') !== $('#AgregarBeneficiarioBeneficia').data('oldValue')){         
             $('#AgregarBeneficiarioBeneficia').data('oldValue', $('#AgregarBeneficiarioBeneficia').is(':checked') );           
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/AgregarBeneficiarioBeneficiaChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
         }
        });
        $('input:radio[name=TypeOfPersonBenefBeneficia]').change(function () {
         if ($('input:radio[name=TypeOfPersonBenefBeneficia]:checked').val() !== null) {
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/TypeOfPersonBenefBeneficiaChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#ClientIDBDBeneficia').change(function () {
         if ($('#ClientIDBDBeneficia').val() !== null && $('#ClientIDBDBeneficia').val() !== $('#ClientIDBDBeneficia').data('oldValue')) {
             $('#ClientIDBDBeneficia').data('oldValue', $('#ClientIDBDBeneficia').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/ClientIDBDBeneficiaChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button33Beneficia').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button33Beneficia'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button33BeneficiaClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#button12Beneficia').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button12Beneficia'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button12BeneficiaClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnVerResumen').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnVerResumen'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/btnVerResumenClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnEnviaMail').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnEnviaMail'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/btnEnviaMailClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#Accept').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Accept'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/AcceptClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#button2').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button2'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/button2Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#eMail').change(function () {
         if ($('#eMail').val() !== null && $('#eMail').val() !== $('#eMail').data('oldValue')) {
             $('#eMail').data('oldValue', $('#eMail').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/eMailChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#Rechazar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiInversionSeguraIIMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Rechazar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/RechazarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiInversionSeguraIISupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiInversionSeguraIISupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5NNCotizacionMiInversionSeguraIIMainForm").validate({
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
                RangeAge: {
                    required: true
                },
                Parametro2: {
                    required: true
                },
                Parametro3: {
                    required: true
                },
                EffectiveDate: {
                    required: true
                },
                ProductCode: {
                    required: true
                },
                LineOfBusiness: {
                    required: true
                },
                ClientGender: {
                    required: true
                },
                ClientSmokerIndicator: {
                    required: true
                },
                ClientBirthDate: {
                    required: true
                },
                InsuredAmountSelected: {
                    required: true
                },
                TotalOriginalAnnualPremium: {

                },
                ProjectedAnnualPayments: {
                    AutoNumericRequired: true                },
                GuaranteedInterestInInvestment: {
                    AutoNumericRequired: true                },
                TypeOfIndemnity: {
                    required: true
                },
                ClientID: {
                    required: true
                },
                ClientFirstName: {
                    required: true
                },
                ClientLastName: {
                    required: true
                },
                ClientLastName2: {
                    required: true
                },
                eMailservice: {
                    required: true
                },
                RelationshipBDBeneficia: {
                    required: true
                },
                PercentageShareBPBeneficia: {
                    AutoNumericRequired: true                },
                TypeOfPersonBenefBeneficia: {
                    required: true
                },
                ClientIDBDBeneficia: {
                    required: true
                },
                FirstNameBPBeneficia: {
                    required: true
                },
                LastNameBPBeneficia: {
                    required: true
                },
                LastName2BPBeneficia: {
                    required: true
                },
                LegalNameBPBeneficia: {
                    required: true
                },
                eMail: {
                    required: true
                }
            },
            messages: {
                RangeAge: {
                    required: 'El campo es requerido'
                },
                Parametro2: {
                    required: 'El campo es requerido'
                },
                Parametro3: {
                    required: 'El campo es requerido'
                },
                EffectiveDate: {
                    required: 'El campo es requerido.'
                },
                ProductCode: {
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                ClientGender: {
                    required: 'El campo es requerido.'
                },
                ClientSmokerIndicator: {
                    required: 'El campo es requerido.'
                },
                ClientBirthDate: {
                    required: 'El campo es requerido'
                },
                InsuredAmountSelected: {
                    required: 'El campo es requerido.'
                },
                TotalOriginalAnnualPremium: {

                },
                ProjectedAnnualPayments: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                GuaranteedInterestInInvestment: {
                    AutoNumericRequired: 'El campo es requerido'                },
                TypeOfIndemnity: {
                    required: 'El campo es requerido'
                },
                ClientID: {
                    required: 'El campo es requerido.'
                },
                ClientFirstName: {
                    required: 'El campo es requerido'
                },
                ClientLastName: {
                    required: 'El campo es requerido'
                },
                ClientLastName2: {
                    required: 'El campo es requerido'
                },
                eMailservice: {
                    required: 'El campo es requerido.'
                },
                RelationshipBDBeneficia: {
                    required: 'El campo es requerido.'
                },
                PercentageShareBPBeneficia: {
                    AutoNumericRequired: 'El campo es requerido'                },
                TypeOfPersonBenefBeneficia: {
                    required: 'El campo es requerido'
                },
                ClientIDBDBeneficia: {
                    required: 'El campo es requerido.'
                },
                FirstNameBPBeneficia: {
                    required: 'El campo es requerido'
                },
                LastNameBPBeneficia: {
                    required: 'El campo es requerido'
                },
                LastName2BPBeneficia: {
                    required: 'El campo es requerido'
                },
                LegalNameBPBeneficia: {
                    required: 'El campo es requerido'
                },
                eMail: {
                    required: 'El campo es requerido'
                }
            }
        });
        $("#InvestmentFundRelatedToPolicyEditForm").validate({
            rules: {
                InvestmentFund2: {
                    required: true
                },
                PercentageShare2: {

                },
                ProjectedProfitability2: {

                }

            },
            messages: {
                InvestmentFund2: {
                    required: 'El campo es requerido.'
                },
                PercentageShare2: {

                },
                ProjectedProfitability2: {

                }

            }
        });
        $("#BeneficiaryBeneficiaEditForm").validate({
            rules: {
                PercentageShareBeneficia: {

                },
                RelationshipBeneficia: {
                    required: true
                }

            },
            messages: {
                PercentageShareBeneficia: {

                },
                RelationshipBeneficia: {
                    required: 'El campo es requerido.'
                }

            }
        });

    };
    this.LookUpForInvestmentFundFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#InvestmentFund>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForInvestmentFund = function (defaultValue) {
        var ctrol = $('#InvestmentFund');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForInvestmentFund",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForProductCode = function (defaultValue, value1) {
        var ctrol = $('#ProductCode');
        var parentId1 = ctrol.data("parentId1");
        
        if (typeof parentId1 == 'undefined' || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val(),
                    RiskInformationLineOfBusiness: value1
                }),
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForLineOfBusiness = function (defaultValue) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForInvestmentFund2Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#InvestmentFund2>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForInvestmentFund2 = function (defaultValue) {
        var ctrol = $('#InvestmentFund2');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForInvestmentFund2",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForCurrency = function (defaultValue) {
        var ctrol = $('#Currency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForTypeOfIndemnity = function (defaultValue) {
        var ctrol = $('#TypeOfIndemnity');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForTypeOfIndemnity",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForRelationshipBeneficiaFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RelationshipBeneficia>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRelationshipBeneficia = function (defaultValue) {
        var ctrol = $('#RelationshipBeneficia');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForRelationshipBeneficia",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForRelationshipBDBeneficia = function (defaultValue) {
        var ctrol = $('#RelationshipBDBeneficia');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiInversionSeguraIIActions.aspx/LookUpForRelationshipBDBeneficia",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiInversionSeguraIIFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };

    this.InvestmentFundProfileTblSetup = function (table) {
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            columns: [{
                field: 'InvestmentFund',
                title: 'Fondo',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFundFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.PercentageShare_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ProjectedProfitability',
                title: '%Rentabilidad',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.ProjectedProfitability_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.InvestmentFundProfileRowToInput = function (row) {
        HT5NNCotizacionMiInversionSeguraIISupport.currentRow = row;
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund(row.InvestmentFund);
        AutoNumeric.set('#PercentageShare', row.PercentageShare);
        AutoNumeric.set('#ProjectedProfitability', row.ProjectedProfitability);

    };
    this.InvestmentFundRelatedToPolicyTblSetup = function (table) {
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund2();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'InvestmentFund',
        toolbar: '#InvestmentFundRelatedToPolicytoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'InvestmentFund',
                title: 'Fondo',
                events: 'InvestmentFundRelatedToPolicyActionEvents',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund2Formatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.PercentageShare2_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ProjectedProfitability',
                title: '%Rentabilidad',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.ProjectedProfitability2_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });


        $('#InvestmentFundRelatedToPolicyTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#InvestmentFundRelatedToPolicyTbl');
            $('#InvestmentFundRelatedToPolicyRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#InvestmentFundRelatedToPolicyRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('getSelections'), function (row) {		
                HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundRelatedToPolicyRowToInput(row);
                
                
                return row.InvestmentFund;
            });
            
          $('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('remove', {
                field: 'InvestmentFund',
                values: ids
           });

            $('#InvestmentFundRelatedToPolicyRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#InvestmentFundRelatedToPolicyCreateBtn').click(function () {
            var formInstance = $("#InvestmentFundRelatedToPolicyEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundRelatedToPolicyShowModal($('#InvestmentFundRelatedToPolicyPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#InvestmentFundRelatedToPolicyPopup').find('#InvestmentFundRelatedToPolicySaveBtn').click(function () {
            var formInstance = $("#InvestmentFundRelatedToPolicyEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#InvestmentFundRelatedToPolicyPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#InvestmentFundRelatedToPolicySaveBtn').html();
                $('#InvestmentFundRelatedToPolicySaveBtn').html('Procesando...');
                $('#InvestmentFundRelatedToPolicySaveBtn').prop('disabled', true);

                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.InvestmentFund = parseInt(0 + $('#InvestmentFund2').val(), 10);
                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.PercentageShare = AutoNumeric.getNumber('#PercentageShare2');
                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.ProjectedProfitability = AutoNumeric.getNumber('#ProjectedProfitability2');

                $('#InvestmentFundRelatedToPolicySaveBtn').prop('disabled', false);
                $('#InvestmentFundRelatedToPolicySaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('updateByUniqueId', { id: HT5NNCotizacionMiInversionSeguraIISupport.currentRow.InvestmentFund, row: HT5NNCotizacionMiInversionSeguraIISupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('append', HT5NNCotizacionMiInversionSeguraIISupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.InvestmentFundRelatedToPolicyShowModal = function (md, title, row) {
        row = row || { InvestmentFund: 0, PercentageShare: 0, ProjectedProfitability: 0 };

        md.data('id', row.InvestmentFund);
        md.find('.modal-title').text(title);

        HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundRelatedToPolicyRowToInput(row);
        $('#ProjectedProfitability2').prop('disabled', (row.ProjectedProfitability !== null));

        md.modal('show');
    };

    this.InvestmentFundRelatedToPolicyRowToInput = function (row) {
        HT5NNCotizacionMiInversionSeguraIISupport.currentRow = row;
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForInvestmentFund2(row.InvestmentFund);
        AutoNumeric.set('#PercentageShare2', row.PercentageShare);
        AutoNumeric.set('#ProjectedProfitability2', row.ProjectedProfitability);

    };
    this.CoverageWithCalculatedPremiumTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            columns: [{
                field: 'Description',
                title: 'Cobertura',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Suma asegurada',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.InsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.AnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        HT5NNCotizacionMiInversionSeguraIISupport.currentRow = row;
        $('#Description').val(row.Description);
        AutoNumeric.set('#InsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremium', row.AnnualPremium);

    };
    this.PROJECTVULTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 20,
            uniqueId: 'YearOfThePolicy',
            columns: [{
                field: 'YearOfThePolicy',
                title: 'Año-Póliza',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.YearOfThePolicy_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'InsuredAge',
                title: 'Edad',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.InsuredAge_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AccumulatdAmount',
                title: 'Aporte',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.AccumulatdAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'PolicyValueOfProjectedProfitablity',
                title: 'Valor Póliza',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.PolicyValueOfProjectedProfitablity_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SurrenderAvailableAmount',
                title: 'Monto de Rescate',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.SurrenderAvailableAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DeathCoverageAmount',
                title: 'Monto asegurado',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.DeathCoverageAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.PROJECTVULRowToInput = function (row) {
        HT5NNCotizacionMiInversionSeguraIISupport.currentRow = row;
        AutoNumeric.set('#YearOfThePolicy', row.YearOfThePolicy);
        AutoNumeric.set('#InsuredAge', row.InsuredAge);
        AutoNumeric.set('#AccumulatdAmount', row.AccumulatdAmount);
        AutoNumeric.set('#PolicyValueOfProjectedProfitablity', row.PolicyValueOfProjectedProfitablity);
        AutoNumeric.set('#SurrenderAvailableAmount', row.SurrenderAvailableAmount);
        AutoNumeric.set('#DeathCoverageAmount', row.DeathCoverageAmount);

    };
    this.BeneficiaryBeneficiaTblSetup = function (table) {
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForRelationshipBeneficia();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
        toolbar: '#BeneficiaryBeneficiatoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ClientCompleteClientName',
                title: 'Nombre',
                events: 'BeneficiaryBeneficiaActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación en la póliza',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.PercentageShareBeneficia_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Relationship',
                title: 'Relación con el asegurado',
                formatter: 'HT5NNCotizacionMiInversionSeguraIISupport.LookUpForRelationshipBeneficiaFormatter',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#BeneficiaryBeneficiaTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#BeneficiaryBeneficiaTbl');
            $('#BeneficiaryBeneficiaRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#BeneficiaryBeneficiaRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#BeneficiaryBeneficiaTbl').bootstrapTable('getSelections'), function (row) {		
                HT5NNCotizacionMiInversionSeguraIISupport.BeneficiaryBeneficiaRowToInput(row);
                
                
                return row.ClientID;
            });
            
          $('#BeneficiaryBeneficiaTbl').bootstrapTable('remove', {
                field: 'ClientID',
                values: ids
           });

            $('#BeneficiaryBeneficiaRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#BeneficiaryBeneficiaCreateBtn').click(function () {
            var formInstance = $("#BeneficiaryBeneficiaEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5NNCotizacionMiInversionSeguraIISupport.BeneficiaryBeneficiaShowModal($('#BeneficiaryBeneficiaPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#BeneficiaryBeneficiaPopup').find('#BeneficiaryBeneficiaSaveBtn').click(function () {
            var formInstance = $("#BeneficiaryBeneficiaEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#BeneficiaryBeneficiaPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#BeneficiaryBeneficiaSaveBtn').html();
                $('#BeneficiaryBeneficiaSaveBtn').html('Procesando...');
                $('#BeneficiaryBeneficiaSaveBtn').prop('disabled', true);

                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.ClientCompleteClientName = $('#CompleteClientNameBeneficia').val();
                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.PercentageShare = AutoNumeric.getNumber('#PercentageShareBeneficia');
                HT5NNCotizacionMiInversionSeguraIISupport.currentRow.Relationship = parseInt(0 + $('#RelationshipBeneficia').val(), 10);

                $('#BeneficiaryBeneficiaSaveBtn').prop('disabled', false);
                $('#BeneficiaryBeneficiaSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#BeneficiaryBeneficiaTbl').bootstrapTable('updateByUniqueId', { id: HT5NNCotizacionMiInversionSeguraIISupport.currentRow.ClientID, row: HT5NNCotizacionMiInversionSeguraIISupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#BeneficiaryBeneficiaTbl').bootstrapTable('append', HT5NNCotizacionMiInversionSeguraIISupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.BeneficiaryBeneficiaShowModal = function (md, title, row) {
        row = row || { ClientCompleteClientName: null, PercentageShare: 0, Relationship: 0 };

        md.data('id', row.ClientID);
        md.find('.modal-title').text(title);

        HT5NNCotizacionMiInversionSeguraIISupport.BeneficiaryBeneficiaRowToInput(row);
        $('#CompleteClientNameBeneficia').prop('disabled', (row.ClientCompleteClientName !== null));

        md.modal('show');
    };

    this.BeneficiaryBeneficiaRowToInput = function (row) {
        HT5NNCotizacionMiInversionSeguraIISupport.currentRow = row;
        $('#CompleteClientNameBeneficia').val(row.ClientCompleteClientName);
        AutoNumeric.set('#PercentageShareBeneficia', row.PercentageShare);
        HT5NNCotizacionMiInversionSeguraIISupport.LookUpForRelationshipBeneficia(row.Relationship);

    };


    this.PercentageShare_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.ProjectedProfitability_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.PercentageShare2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.ProjectedProfitability2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.InsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.AnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999"
        });
      };
    this.YearOfThePolicy_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.InsuredAge_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "-999"
        });
      };
    this.AccumulatdAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.PolicyValueOfProjectedProfitablity_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.SurrenderAvailableAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.DeathCoverageAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.PercentageShareBeneficia_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      };


};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Mi Inversión Segura');
        

    HT5NNCotizacionMiInversionSeguraIISupport.ControlBehaviour();
    HT5NNCotizacionMiInversionSeguraIISupport.ControlActions();
    HT5NNCotizacionMiInversionSeguraIISupport.ValidateSetup();
    HT5NNCotizacionMiInversionSeguraIISupport.Initialization();

    $("#InvestmentFundProfileTblPlaceHolder").replaceWith('<table id="InvestmentFundProfileTbl"><caption >Fondos propuestos</caption></table>');
    HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundProfileTblSetup($('#InvestmentFundProfileTbl'));
    $("#InvestmentFundRelatedToPolicyTblPlaceHolder").replaceWith('<table id="InvestmentFundRelatedToPolicyTbl"></table>');
    HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundRelatedToPolicyTblSetup($('#InvestmentFundRelatedToPolicyTbl'));
    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"></table>');
    HT5NNCotizacionMiInversionSeguraIISupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));
    $("#PROJECTVULTblPlaceHolder").replaceWith('<table id="PROJECTVULTbl"></table>');
    HT5NNCotizacionMiInversionSeguraIISupport.PROJECTVULTblSetup($('#PROJECTVULTbl'));
    $("#BeneficiaryBeneficiaTblPlaceHolder").replaceWith('<table id="BeneficiaryBeneficiaTbl"><caption >Beneficiarios</caption></table>');
    HT5NNCotizacionMiInversionSeguraIISupport.BeneficiaryBeneficiaTblSetup($('#BeneficiaryBeneficiaTbl'));




});

window.InvestmentFundRelatedToPolicyActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5NNCotizacionMiInversionSeguraIISupport.InvestmentFundRelatedToPolicyShowModal($('#InvestmentFundRelatedToPolicyPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.BeneficiaryBeneficiaActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5NNCotizacionMiInversionSeguraIISupport.BeneficiaryBeneficiaShowModal($('#BeneficiaryBeneficiaPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
