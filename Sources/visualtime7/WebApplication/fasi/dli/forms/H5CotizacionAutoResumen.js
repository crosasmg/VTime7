var H5CotizacionAutoResumenSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5CotizacionAutoResumenFormId').val(),
            RiskInformationEffectiveDate: generalSupport.DatePickerValueInputToObject('#EffectiveDate'),
            ProductMasterDescription: $('#Description').val(),
            RiskInformationPrimaryInsuredClientFirstName: $('#PrimaryInsuredClientFirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#PrimaryInsuredClientLastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#PrimaryInsuredClientLastName2').val(),
            RiskInformationPrimaryInsuredClientBirthDate: generalSupport.DatePickerValueInputToObject('#BirthDate'),
            RiskInformationPrimaryInsuredClientGender: $('#Gender').val(),
            DireccionMostrar: $('#DireccionMostar').val(),
            RiskInformationAutomobileLineOfBusinessValueOfTheVehicle: generalSupport.NumericValue('#ValueOfTheVehicle', -999999999999999999, 999999999999999999),
            RiskInformationCurrency: parseInt(0 + $('#Currency').val(), 10),
            RiskInformationAutomobileLineOfBusinessLicensePlateType: $('#LicensePlateType').val(),
            RiskInformationAutomobileLineOfBusinessLicensePlate: $('#LicensePlate').val(),
            RiskInformationAutomobileLineOfBusinessColor: $('#Color').val(),
            RiskInformationAutomobileLineOfBusinessDrivingZone: parseInt(0 + $('#DrivingZone').val(), 10),
            RiskInformationAutomobileLineOfBusinessUseOfVehicle: parseInt(0 + $('#UseOfVehicle').val(), 10),
            RiskInformationAutomobileLineOfBusinessChassis: $('#Chassis').val(),
            RiskInformationAutomobileLineOfBusinessEngineSerialNumber: $('#EngineSerialNumber').val(),
            RiskInformationAutomobileLineOfBusinessAutomobileInformationMileage: generalSupport.NumericValue('#Mileage', -999999999999, 999999999999)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5CotizacionAutoResumenFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, generalSupport.DateFormat()));
        $('#Description').val(data.ProductMasterDescription);
        $('#PrimaryInsuredClientFirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#PrimaryInsuredClientLastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#PrimaryInsuredClientLastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationPrimaryInsuredClientBirthDate, generalSupport.DateFormat()));
        $('#eMailAddressDefault').html(data.RiskInformationPrimaryInsuredClienteMailAddressDefault);
        $('#DireccionMostar').val(data.DireccionMostrar);
        AutoNumeric.set('#ValueOfTheVehicle', data.RiskInformationAutomobileLineOfBusinessValueOfTheVehicle);
        $('#LicensePlate').val(data.RiskInformationAutomobileLineOfBusinessLicensePlate);
        $('#Color').val(data.RiskInformationAutomobileLineOfBusinessColor);
        $('#Chassis').val(data.RiskInformationAutomobileLineOfBusinessChassis);
        $('#EngineSerialNumber').val(data.RiskInformationAutomobileLineOfBusinessEngineSerialNumber);
        AutoNumeric.set('#Mileage', data.RiskInformationAutomobileLineOfBusinessAutomobileInformationMileage);
        AutoNumeric.set('#TotalOriginalAnnualPremium', data.RiskInformationTotalOriginalAnnualPremium);

        H5CotizacionAutoResumenSupport.LookUpForGender(data.RiskInformationPrimaryInsuredClientGender, source);
        H5CotizacionAutoResumenSupport.LookUpForCurrency(data.RiskInformationCurrency, source);
        H5CotizacionAutoResumenSupport.LookUpForLicensePlateType(data.RiskInformationAutomobileLineOfBusinessLicensePlateType, source);
        H5CotizacionAutoResumenSupport.LookUpForDrivingZone(data.RiskInformationAutomobileLineOfBusinessDrivingZone, source);
        H5CotizacionAutoResumenSupport.LookUpForUseOfVehicle(data.RiskInformationAutomobileLineOfBusinessUseOfVehicle, source);

        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return (filterColumns.SelectedByDefault == true);}));

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#ValueOfTheVehicle', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      new AutoNumeric('#Mileage', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      new AutoNumeric('#TotalOriginalAnnualPremium', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999
        });




        $('#EffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#BirthDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5CotizacionAutoResumenSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#H5CotizacionAutoResumenFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                H5CotizacionAutoResumenSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/H5CotizacionAutoResumen.aspx?id=' + $('#H5CotizacionAutoResumenFormId').val());
              
          

            });
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5CotizacionAutoResumenMainForm").validate({
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
                Description: {
                    maxlength: 30
                },
                PrimaryInsuredClientFirstName: {
                    maxlength: 20
                },
                PrimaryInsuredClientLastName: {
                    maxlength: 20
                },
                PrimaryInsuredClientLastName2: {
                    maxlength: 20
                },
                BirthDate: {
                    DatePicker: true
                },
                eMailAddressDefault: {
                    maxlength: 60
                },
                ValueOfTheVehicle: {
                    AutoNumericMinValue: -999999999999999999,
                    AutoNumericMaxValue: 999999999999999999
                },
                LicensePlate: {
                    maxlength: 10
                },
                Color: {
                    maxlength: 15
                },
                Chassis: {
                    maxlength: 40
                },
                EngineSerialNumber: {
                    maxlength: 40
                },
                Mileage: {
                    AutoNumericMinValue: -999999999999,
                    AutoNumericMaxValue: 999999999999
                },
                TotalOriginalAnnualPremium: {
                    AutoNumericMinValue: -999999999999,
                    AutoNumericMaxValue: 999999999999
                }
            },
            messages: {
                EffectiveDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                Description: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                PrimaryInsuredClientFirstName: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                PrimaryInsuredClientLastName: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                PrimaryInsuredClientLastName2: {
                    maxlength: 'El campo permite 20 caracteres máximo'
                },
                BirthDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                eMailAddressDefault: {
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                ValueOfTheVehicle: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999999'
                },
                LicensePlate: {
                    maxlength: 'El campo permite 10 caracteres máximo'
                },
                Color: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                Chassis: {
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                EngineSerialNumber: {
                    maxlength: 'El campo permite 40 caracteres máximo'
                },
                Mileage: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                },
                TotalOriginalAnnualPremium: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999'
                }
            }
        });

    };
    this.LookUpForGender = function (defaultValue, source) {
        var ctrol = $('#Gender');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/LookUpForGender", false,
                JSON.stringify({ id: $('#H5CotizacionAutoResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/LookUpForCurrency", false,
                JSON.stringify({ id: $('#H5CotizacionAutoResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/LookUpForLicensePlateType", false,
                JSON.stringify({ id: $('#H5CotizacionAutoResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/LookUpForDrivingZone", false,
                JSON.stringify({ id: $('#H5CotizacionAutoResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionAutoResumenActions.aspx/LookUpForUseOfVehicle", false,
                JSON.stringify({ id: $('#H5CotizacionAutoResumenFormId').val() }),
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
            pagination: true,
            pageSize: 10,
            columns: [{
                field: 'CoverageCode',
                title: 'Cobertura',
                formatter: 'H5CotizacionAutoResumenSupport.CoverageCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Description',
                title: 'Cobertura',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Suma asegurada',
                formatter: 'H5CotizacionAutoResumenSupport.CoverageWithCalculatedPremiumInsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Monto de prima',
                formatter: 'H5CotizacionAutoResumenSupport.CoverageWithCalculatedPremiumAnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        H5CotizacionAutoResumenSupport.currentRow = row;
        AutoNumeric.set('#CoverageCode', row.CoverageCode);
        $('#Description2').val(row.Description);
        AutoNumeric.set('#CoverageWithCalculatedPremiumInsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#CoverageWithCalculatedPremiumAnnualPremium', row.AnnualPremium);

    };


    this.CoverageCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CoverageWithCalculatedPremiumInsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.CoverageWithCalculatedPremiumAnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Mi Auto aDorado - Resumen');
        

    H5CotizacionAutoResumenSupport.ControlBehaviour();
    H5CotizacionAutoResumenSupport.ControlActions();
    H5CotizacionAutoResumenSupport.ValidateSetup();
    H5CotizacionAutoResumenSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"></table>');
    H5CotizacionAutoResumenSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));




});

