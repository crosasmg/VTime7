var HT5NNCotizacionPolizaHogar3AdicionalSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationHomeLineOfBusinessOwnership: parseInt(0 + $('#Ownership').val(), 10),
            RiskInformationHomeLineOfBusinessHomePurchasedCoverage: $('#HomePurchasedCoverage').is(':checked'),
            RiskInformationHomeLineOfBusinessPurchasePrice: AutoNumeric.getNumber('#PurchasePrice'),
            RiskInformationHomeLineOfBusinessCurrencyOfPurchasePrice: parseInt(0 + $('#CurrencyOfPurchasePrice').val(), 10),
            RiskInformationHomeLineOfBusinessDateOfPurchase: $('#DateOfPurchase').val() !== '' ? moment($('#DateOfPurchase').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationHomeLineOfBusinessOtherConstructionMaterials: $('#OtherConstructionMaterials').val(),
            RiskInformationHomeLineOfBusinessRoofType: parseInt(0 + $('#RoofType').val(), 10),
            RiskInformationHomeLineOfBusinessRoofYear: parseInt(0 + $('#RoofYear').val(), 10),
            RiskInformationHomeLineOfBusinessAnotherPolicyIndicator: $('input:radio[name=AnotherPolicyIndicator]:checked').val(),
            RiskInformationHomeLineOfBusinessInsuredAmountOfTheOtherPolicy: AutoNumeric.getNumber('#InsuredAmountOfTheOtherPolicy'),
            RiskInformationHomeLineOfBusinessCurrencyOtherPolicy: parseInt(0 + $('#CurrencyOtherPolicy').val(), 10),
            RiskInformationHomeLineOfBusinessExpirationOfTheOtherPolicy: $('#ExpirationOfTheOtherPolicy').val() !== '' ? moment($('#ExpirationOfTheOtherPolicy').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationHomeLineOfBusinessNumberOfCars: AutoNumeric.getNumber('#NumberOfCars'),
            RiskInformationHomeLineOfBusinessChimneys: AutoNumeric.getNumber('#Chimneys'),
            RiskInformationHomeLineOfBusinessBedrooms: AutoNumeric.getNumber('#Bedrooms'),
            RiskInformationHomeLineOfBusinessFullBathrooms: AutoNumeric.getNumber('#FullBathrooms'),
            RiskInformationHomeLineOfBusinessHalfBathrooms: AutoNumeric.getNumber('#HalfBathrooms'),
            RiskInformationHomeLineOfBusinessAirConditioningType: parseInt(0 + $('#AirConditioningType').val(), 10),
            RiskInformationHomeLineOfBusinessGasolineTank: $('#GasolineTank').is(':checked'),
            RiskInformationHomeLineOfBusinessHeating: parseInt(0 + $('#Heating').val(), 10),
            RiskInformationHomeLineOfBusinessSprinklers: $('#Sprinklers').is(':checked'),
            RiskInformationHomeLineOfBusinessDistanceToFireHydrant: AutoNumeric.getNumber('#DistanceToFireHydrant'),
            RiskInformationHomeLineOfBusinessCompanyMonitoringTheAlarm: $('#CompanyMonitoringTheAlarm').val(),
            RiskInformationHomeLineOfBusinessSmokingAllowed: $('#SmokingAllowed').is(':checked'),
            RiskInformationHomeLineOfBusinessDistanceToFireDepartment: AutoNumeric.getNumber('#DistanceToFireDepartment'),
            RiskInformationHomeLineOfBusinessNearestFireDepartmentName: $('#NearestFireDepartmentName').val(),
            RiskInformationHomeLineOfBusinessUbicationOfSwimmingPool: parseInt(0 + $('#UbicationOfSwimmingPool').val(), 10),
            RiskInformationHomeLineOfBusinessFencedPool: $('#FencedPool').is(':checked'),
            RiskInformationHomeLineOfBusinessFenceHeight: AutoNumeric.getNumber('#FenceHeight'),
            RiskInformationHomeLineOfBusinessTrampoline: $('#Trampoline').is(':checked'),
            RiskInformationHomeLineOfBusinessPetsOrLivestock: $('#PetsOrLivestock').is(':checked'),
            CantidadMascotas: AutoNumeric.getNumber('#CantidadMascotas'),
            RiskInformationHomeLineOfBusinessAnimalsDescriptions: $('#AnimalsDescriptions').val(),
            RiskInformationHomeLineOfBusinessPreviousAttack: $('#PreviousAttack').is(':checked'),
            RolesRole_Role: generalSupport.NormalizeProperties($('#RolesRoleTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').html(data.uwcaseid);
        $('#HomePurchasedCoverage').prop("checked", data.RiskInformationHomeLineOfBusinessHomePurchasedCoverage);
        AutoNumeric.set('#PurchasePrice', data.RiskInformationHomeLineOfBusinessPurchasePrice);
        $('#DateOfPurchase').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationHomeLineOfBusinessDateOfPurchase, 'DD/MM/YYYY'));
        $('#OtherConstructionMaterials').val(data.RiskInformationHomeLineOfBusinessOtherConstructionMaterials);
        if($('input:radio[name=AnotherPolicyIndicator][value=' + data.RiskInformationHomeLineOfBusinessAnotherPolicyIndicator +']').length===0)
           $('input:radio[name=AnotherPolicyIndicator]').prop('checked', false);
        else
           $($('input:radio[name=AnotherPolicyIndicator][value=' + data.RiskInformationHomeLineOfBusinessAnotherPolicyIndicator +']')).prop('checked', true);
        $('#AnotherPolicyIndicator').data('oldValue', data.RiskInformationHomeLineOfBusinessAnotherPolicyIndicator);
        $('#AnotherPolicyIndicator').val(data.RiskInformationHomeLineOfBusinessAnotherPolicyIndicator);

        AutoNumeric.set('#InsuredAmountOfTheOtherPolicy', data.RiskInformationHomeLineOfBusinessInsuredAmountOfTheOtherPolicy);
        $('#ExpirationOfTheOtherPolicy').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationHomeLineOfBusinessExpirationOfTheOtherPolicy, 'DD/MM/YYYY'));
        AutoNumeric.set('#NumberOfCars', data.RiskInformationHomeLineOfBusinessNumberOfCars);
        AutoNumeric.set('#Chimneys', data.RiskInformationHomeLineOfBusinessChimneys);
        AutoNumeric.set('#Bedrooms', data.RiskInformationHomeLineOfBusinessBedrooms);
        AutoNumeric.set('#FullBathrooms', data.RiskInformationHomeLineOfBusinessFullBathrooms);
        AutoNumeric.set('#HalfBathrooms', data.RiskInformationHomeLineOfBusinessHalfBathrooms);
        $('#GasolineTank').prop("checked", data.RiskInformationHomeLineOfBusinessGasolineTank);
        $('#Sprinklers').prop("checked", data.RiskInformationHomeLineOfBusinessSprinklers);
        AutoNumeric.set('#DistanceToFireHydrant', data.RiskInformationHomeLineOfBusinessDistanceToFireHydrant);
        $('#CompanyMonitoringTheAlarm').val(data.RiskInformationHomeLineOfBusinessCompanyMonitoringTheAlarm);
        $('#SmokingAllowed').prop("checked", data.RiskInformationHomeLineOfBusinessSmokingAllowed);
        AutoNumeric.set('#DistanceToFireDepartment', data.RiskInformationHomeLineOfBusinessDistanceToFireDepartment);
        $('#NearestFireDepartmentName').val(data.RiskInformationHomeLineOfBusinessNearestFireDepartmentName);
        $('#FencedPool').prop("checked", data.RiskInformationHomeLineOfBusinessFencedPool);
        AutoNumeric.set('#FenceHeight', data.RiskInformationHomeLineOfBusinessFenceHeight);
        $('#Trampoline').prop("checked", data.RiskInformationHomeLineOfBusinessTrampoline);
        $('#PetsOrLivestock').prop("checked", data.RiskInformationHomeLineOfBusinessPetsOrLivestock);
        AutoNumeric.set('#CantidadMascotas', data.CantidadMascotas);
        $('#AnimalsDescriptions').val(data.RiskInformationHomeLineOfBusinessAnimalsDescriptions);
        $('#PreviousAttack').prop("checked", data.RiskInformationHomeLineOfBusinessPreviousAttack);

        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForOwnership(data.RiskInformationHomeLineOfBusinessOwnership);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForCurrencyOfPurchasePrice(data.RiskInformationHomeLineOfBusinessCurrencyOfPurchasePrice);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForRoofType(data.RiskInformationHomeLineOfBusinessRoofType);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForRoofYear(data.RiskInformationHomeLineOfBusinessRoofYear);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForCurrencyOtherPolicy(data.RiskInformationHomeLineOfBusinessCurrencyOtherPolicy);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForAirConditioningType(data.RiskInformationHomeLineOfBusinessAirConditioningType);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForHeating(data.RiskInformationHomeLineOfBusinessHeating);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForUbicationOfSwimmingPool(data.RiskInformationHomeLineOfBusinessUbicationOfSwimmingPool);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRole(data.RiskInformationRolesClientRole, data.RiskInformationLineOfBusiness, data.RiskInformationProductCode);

        if (data.RolesRole_Role !== null)
            $('#RolesRoleTbl').bootstrapTable('load', data.RolesRole_Role.filter(function(filterColumns) {return filterColumns.ClientID === null;}));

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#PurchasePrice', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#InsuredAmountOfTheOtherPolicy', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#NumberOfCars', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Chimneys', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Bedrooms', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#FullBathrooms', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#HalfBathrooms', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#DistanceToFireHydrant', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#DistanceToFireDepartment', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#FenceHeight', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#CantidadMascotas', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });
   this.LookUpForRoofYear = function (defaultValue) {
        var ctrol = $('#RoofYear');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForRoofYear",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()
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
       //}
       //else
       //    if (typeof defaultValue !== 'undefined' && defaultValue !== null)
       //        if (defaultValue.toString() !== ctrol.val())
       //            ctrol.val(defaultValue).change();
    };
        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRole(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ClientRole').data("parentId1") || $('#ProductCode').val() !== $('#ClientRole').data("parentId2"))
                   $('#ClientRole').children().remove();
        });
        $('#ProductCode').on('change', function () {
            var value = $('#ProductCode').val();

            if (value !== null && value !== '0') {
                var skipData = $('#ProductCode').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#ProductCode').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRole(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ClientRole').data("parentId1") || $('#ProductCode').val() !== $('#ClientRole').data("parentId2"))
                   $('#ClientRole').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#DateOfPurchase_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#ExpirationOfTheOtherPolicy_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogar3AdicionalSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogar3AdicionalSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogar3Adicional.aspx?id=' + $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('input:radio[name=AnotherPolicyIndicator]').change(function () {
         if ($('input:radio[name=AnotherPolicyIndicator]:checked').val() !== null) {
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/AnotherPolicyIndicatorChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionPolizaHogar3AdicionalSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionPolizaHogar3AdicionalSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button3').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogar3AdicionalMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button3'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/button3Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogar3AdicionalSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogar3AdicionalSupport.ActionProcess(data);
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
    
        $("#HT5NNCotizacionPolizaHogar3AdicionalMainForm").validate({
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
                ProductCode: {
                    required: true
                },
                LineOfBusiness: {
                    required: true
                },
                Ownership: {
                    required: true
                },
                PurchasePrice: {
                    AutoNumericRequired: true                },
                CurrencyOfPurchasePrice: {
                    required: true
                },
                DateOfPurchase: {
                    required: true
                },
                RoofType: {
                    required: true
                },
                RoofYear: {
                    required: true
                },
                InsuredAmountOfTheOtherPolicy: {
                    AutoNumericRequired: true                },
                CurrencyOtherPolicy: {
                    required: true
                },
                ExpirationOfTheOtherPolicy: {
                    required: true
                },
                NumberOfCars: {

                },
                Chimneys: {

                },
                Bedrooms: {

                },
                FullBathrooms: {

                },
                HalfBathrooms: {

                },
                DistanceToFireHydrant: {

                },
                DistanceToFireDepartment: {

                },
                FenceHeight: {

                },
                CantidadMascotas: {

                }
            },
            messages: {
                ProductCode: {
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                Ownership: {
                    required: 'El campo es requerido.'
                },
                PurchasePrice: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                CurrencyOfPurchasePrice: {
                    required: 'El campo es requerido.'
                },
                DateOfPurchase: {
                    required: 'El campo es requerido.'
                },
                RoofType: {
                    required: 'El campo es requerido.'
                },
                RoofYear: {
                    required: 'El campo es requerido.'
                },
                InsuredAmountOfTheOtherPolicy: {
                    AutoNumericRequired: 'El campo es requerido'                },
                CurrencyOtherPolicy: {
                    required: 'El campo es requerido.'
                },
                ExpirationOfTheOtherPolicy: {
                    required: 'El campo es requerido.'
                },
                NumberOfCars: {

                },
                Chimneys: {

                },
                Bedrooms: {

                },
                FullBathrooms: {

                },
                HalfBathrooms: {

                },
                DistanceToFireHydrant: {

                },
                DistanceToFireDepartment: {

                },
                FenceHeight: {

                },
                CantidadMascotas: {

                }
            }
        });
        $("#RolesRoleEditForm").validate({
            rules: {
                ClientRole: {
                    required: true
                }

            },
            messages: {
                ClientRole: {
                    required: 'El campo es requerido.'
                }

            }
        });

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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val(),
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForOwnership = function (defaultValue) {
        var ctrol = $('#Ownership');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForOwnership",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForCurrencyOfPurchasePrice = function (defaultValue) {
        var ctrol = $('#CurrencyOfPurchasePrice');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForCurrencyOfPurchasePrice",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForRoofType = function (defaultValue) {
        var ctrol = $('#RoofType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForRoofType",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForCurrencyOtherPolicy = function (defaultValue) {
        var ctrol = $('#CurrencyOtherPolicy');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForCurrencyOtherPolicy",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForAirConditioningType = function (defaultValue) {
        var ctrol = $('#AirConditioningType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForAirConditioningType",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForHeating = function (defaultValue) {
        var ctrol = $('#Heating');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForHeating",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForUbicationOfSwimmingPool = function (defaultValue) {
        var ctrol = $('#UbicationOfSwimmingPool');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForUbicationOfSwimmingPool",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val()}),
                
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
    this.LookUpForClientRoleFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRole(null, row.LineOfBusiness, row.ProductCode);
            result = $("#ClientRole>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForClientRole = function (defaultValue, value1, value2) {
        var ctrol = $('#ClientRole');
        var parentId1 = ctrol.data("parentId1");
        var parentId2 = ctrol.data("parentId2");
        
        if ((typeof parentId1 == 'undefined' || parentId1 !== value1) || (typeof parentId2 == 'undefined' || parentId2 !== value2)) {
            ctrol.data("parentId1", value1);
            ctrol.data("parentId2", value2);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar3AdicionalActions.aspx/LookUpForClientRole",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionPolizaHogar3AdicionalFormId').val(),
                    RiskInformationLineOfBusiness: value1,
                     RiskInformationProductCode: value2
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

    this.RolesRoleTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
        toolbar: '#RolesRoletoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'ClientFirstName',
                title: 'Primer nombre',
                events: 'RolesRoleActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientLastName',
                title: 'Apellido paterno',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientLastName2',
                title: 'Apellido materno',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientRole',
                title: 'Figura',
                formatter: 'HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRoleFormatter',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#RolesRoleTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#RolesRoleTbl');
            $('#RolesRoleRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#RolesRoleRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#RolesRoleTbl').bootstrapTable('getSelections'), function (row) {		
                HT5NNCotizacionPolizaHogar3AdicionalSupport.RolesRoleRowToInput(row);
                
                
                return row.ClientID;
            });
            
          $('#RolesRoleTbl').bootstrapTable('remove', {
                field: 'ClientID',
                values: ids
           });

            $('#RolesRoleRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#RolesRoleCreateBtn').click(function () {
            var formInstance = $("#RolesRoleEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            HT5NNCotizacionPolizaHogar3AdicionalSupport.RolesRoleShowModal($('#RolesRolePopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#RolesRolePopup').find('#RolesRoleSaveBtn').click(function () {
            var formInstance = $("#RolesRoleEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#RolesRolePopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#RolesRoleSaveBtn').html();
                $('#RolesRoleSaveBtn').html('Procesando...');
                $('#RolesRoleSaveBtn').prop('disabled', true);

                HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow.ClientFirstName = $('#RoleClientFirstName').val();
                HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow.ClientLastName = $('#RoleClientLastName').val();
                HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow.ClientLastName2 = $('#RoleClientLastName2').val();
                HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow.ClientRole = parseInt(0 + $('#ClientRole').val(), 10);

                $('#RolesRoleSaveBtn').prop('disabled', false);
                $('#RolesRoleSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#RolesRoleTbl').bootstrapTable('updateByUniqueId', { id: HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow.ClientID, row: HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#RolesRoleTbl').bootstrapTable('append', HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.RolesRoleShowModal = function (md, title, row) {
        row = row || { ClientFirstName: null, ClientLastName: null, ClientLastName2: null, ClientRole: 0 };

        md.data('id', row.ClientID);
        md.find('.modal-title').text(title);

        HT5NNCotizacionPolizaHogar3AdicionalSupport.RolesRoleRowToInput(row);


        md.modal('show');
    };

    this.RolesRoleRowToInput = function (row) {
        HT5NNCotizacionPolizaHogar3AdicionalSupport.currentRow = row;
        $('#RoleClientFirstName').val(row.ClientFirstName);
        $('#RoleClientLastName').val(row.ClientLastName);
        $('#RoleClientLastName2').val(row.ClientLastName2);
        HT5NNCotizacionPolizaHogar3AdicionalSupport.LookUpForClientRole(row.ClientRole, row.LineOfBusiness, row.ProductCode);

    };




};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro');
        

    HT5NNCotizacionPolizaHogar3AdicionalSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogar3AdicionalSupport.ControlActions();
    HT5NNCotizacionPolizaHogar3AdicionalSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogar3AdicionalSupport.Initialization();

    $("#RolesRoleTblPlaceHolder").replaceWith('<table id="RolesRoleTbl"></table>');
    HT5NNCotizacionPolizaHogar3AdicionalSupport.RolesRoleTblSetup($('#RolesRoleTbl'));




});

window.RolesRoleActionEvents = {
    'click .update': function (e, value, row, index) {
        HT5NNCotizacionPolizaHogar3AdicionalSupport.RolesRoleShowModal($('#RolesRolePopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
