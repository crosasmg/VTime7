var HT5NNCotizacionPolizaHogar1BasicoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationHomeLineOfBusinessYearBuilt: parseInt(0 + $('#YearBuilt').val(), 10),
            RiskInformationHomeLineOfBusinessDwellingType: parseInt(0 + $('#DwellingType').val(), 10),
            RiskInformationHomeLineOfBusinessConstructionMaterial: parseInt(0 + $('#ConstructionMaterial').val(), 10),
            RiskInformationHomeLineOfBusinessFoundation: parseInt(0 + $('#Foundation').val(), 10),
            RiskInformationHomeLineOfBusinessRoofType: parseInt(0 + $('#RoofType').val(), 10),
            RiskInformationHomeLineOfBusinessStories: AutoNumeric.getNumber('#Stories'),
            RiskInformationHomeLineOfBusinessArea: AutoNumeric.getNumber('#Area'),
            RiskInformationHomeLineOfBusinessLandArea: AutoNumeric.getNumber('#LandArea'),
            RiskInformationCurrency: parseInt(0 + $('#Currency').val(), 10),
            BasicInsuredAmountEstructuraInsuredValue: AutoNumeric.getNumber('#InsuredValueEstructura'),
            BasicInsuredAmountContenidoInsuredValue: AutoNumeric.getNumber('#InsuredValueContenido'),
            ModuleCollection: HT5NNCotizacionPolizaHogar1BasicoSupport.ModuleSelectedCheckBoxListBehaviour.getCheckedItems(),
            FinalMessage: $('#FinalMessageLabel').html()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').html(data.uwcaseid);
        AutoNumeric.set('#Stories', data.RiskInformationHomeLineOfBusinessStories);
        AutoNumeric.set('#Area', data.RiskInformationHomeLineOfBusinessArea);
        AutoNumeric.set('#LandArea', data.RiskInformationHomeLineOfBusinessLandArea);
        AutoNumeric.set('#InsuredValueEstructura', data.BasicInsuredAmountEstructuraInsuredValue);
        AutoNumeric.set('#InsuredValueContenido', data.BasicInsuredAmountContenidoInsuredValue);
        AutoNumeric.set('#TotalOriginalAnnualPremium', data.RiskInformationTotalOriginalAnnualPremium);
        $('#FinalMessageLabel').html(data.FinalMessage);

        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForYearBuilt(data.RiskInformationHomeLineOfBusinessYearBuilt);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForDwellingType(data.RiskInformationHomeLineOfBusinessDwellingType);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForConstructionMaterial(data.RiskInformationHomeLineOfBusinessConstructionMaterial);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForFoundation(data.RiskInformationHomeLineOfBusinessFoundation);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForRoofType(data.RiskInformationHomeLineOfBusinessRoofType);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForCurrency(data.RiskInformationCurrency);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForModuleSelected(data.ModuleCollection, data.RiskInformationLineOfBusiness, data.RiskInformationProductCode);

        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return filterColumns.SelectedByDefault == true;}));

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Stories', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });
      new AutoNumeric('#Area', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#LandArea', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#InsuredValueEstructura', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#InsuredValueContenido', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "0"
        });
      new AutoNumeric('#TotalOriginalAnnualPremium', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9999999999",
            decimalPlaces: 2,
            minimumValue: "-9999999999"
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });
   this.LookUpForYearBuilt = function (defaultValue) {
        var ctrol = $('#YearBuilt');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForYearBuilt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()
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
                    HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForModuleSelected(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ModuleSelected').data("parentId1") || $('#ProductCode').val() !== $('#ModuleSelected').data("parentId2"))
                   $('#ModuleSelected').children().remove();
        });
        $('#ProductCode').on('change', function () {
            var value = $('#ProductCode').val();

            if (value !== null && value !== '0') {
                var skipData = $('#ProductCode').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#ProductCode').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar1BasicoSupport.LookUpForModuleSelected(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ModuleSelected').data("parentId1") || $('#ProductCode').val() !== $('#ModuleSelected').data("parentId2"))
                   $('#ModuleSelected').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogar1BasicoSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogar1BasicoSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogar1Basico.aspx?id=' + $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#button0').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogar1BasicoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button0'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/button0Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogar1BasicoSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogar1BasicoSupport.ActionProcess(data);
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
        $('#button5').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogar1BasicoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button5'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/button5Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogar1BasicoSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogar1BasicoSupport.ActionProcess(data);
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
    
        $("#HT5NNCotizacionPolizaHogar1BasicoMainForm").validate({
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
                YearBuilt: {
                    required: true
                },
                DwellingType: {
                    required: true
                },
                ConstructionMaterial: {
                    required: true
                },
                Foundation: {
                    required: true
                },
                RoofType: {
                    required: true
                },
                Stories: {
                    AutoNumericRequired: true                },
                Area: {
                    AutoNumericRequired: true                },
                LandArea: {
                    AutoNumericRequired: true                },
                InsuredValueEstructura: {

                },
                InsuredValueContenido: {

                },
                TotalOriginalAnnualPremium: {

                }
            },
            messages: {
                ProductCode: {
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                YearBuilt: {
                    required: 'El campo es requerido.'
                },
                DwellingType: {
                    required: 'El campo es requerido.'
                },
                ConstructionMaterial: {
                    required: 'El campo es requerido.'
                },
                Foundation: {
                    required: 'El campo es requerido.'
                },
                RoofType: {
                    required: 'El campo es requerido.'
                },
                Stories: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                Area: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                LandArea: {
                    AutoNumericRequired: 'El campo es requerido.'                },
                InsuredValueEstructura: {

                },
                InsuredValueContenido: {

                },
                TotalOriginalAnnualPremium: {

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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val(),
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
    this.LookUpForDwellingType = function (defaultValue) {
        var ctrol = $('#DwellingType');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForDwellingType",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
    this.LookUpForConstructionMaterial = function (defaultValue) {
        var ctrol = $('#ConstructionMaterial');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForConstructionMaterial",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
    this.LookUpForFoundation = function (defaultValue) {
        var ctrol = $('#Foundation');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForFoundation",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForRoofType",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar1BasicoFormId').val()}),
                
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
    this.LookUpForModuleSelected = function (defaultValues, value1, value2) {
        var ctrol = $('#ModuleSelected');
        var parentId1 = ctrol.data("parentId1");
        var parentId2 = ctrol.data("parentId2");
        
        if ((typeof parentId1 !== 'undefined' || parentId1 !== value1) || (typeof parentId2 !== 'undefined' || parentId2 !== value2)) {
            ctrol.data("parentId1", value1);
            ctrol.data("parentId2", value2);

            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar1BasicoActions.aspx/LookUpForModuleSelected",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    RiskInformationLineOfBusiness: value1,
                     RiskInformationProductCode: value2
                }),
                success: function (data) {
                    if (data.d.Success === true) {
                        var control = $('#ModuleSelected');

                        var options = {
                            items: data.d.Data,
                            // Lista de elementos seleccionados
                            checkedItems: defaultValues,
                            valuePath: 'CoverageModule',
                            textPath: 'SDESCRIPT'
                        };
                        HT5NNCotizacionPolizaHogar1BasicoSupport.ModuleSelectedCheckBoxListBehaviour.checkList(options);
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }

    };

    this.CoverageWithCalculatedPremiumTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'CoverageCode',
            columns: [{
                field: 'Description',
                title: 'Cobertura',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Capital asegurado',
                formatter: 'HT5NNCotizacionPolizaHogar1BasicoSupport.InsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'HT5NNCotizacionPolizaHogar1BasicoSupport.AnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        HT5NNCotizacionPolizaHogar1BasicoSupport.currentRow = row;
        $('#Description').val(row.Description);
        AutoNumeric.set('#InsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremium', row.AnnualPremium);

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
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };


};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro');
        
    HT5NNCotizacionPolizaHogar1BasicoSupport.ModuleSelectedCheckBoxListBehaviour = $('#ModuleSelected');

    HT5NNCotizacionPolizaHogar1BasicoSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogar1BasicoSupport.ControlActions();
    HT5NNCotizacionPolizaHogar1BasicoSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogar1BasicoSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"></table>');
    HT5NNCotizacionPolizaHogar1BasicoSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));




});

