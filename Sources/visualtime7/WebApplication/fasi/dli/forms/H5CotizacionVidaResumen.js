var H5CotizacionVidaResumenSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5CotizacionVidaResumenFormId').val(),
            RiskInformationEffectiveDate: generalSupport.DatePickerValueInputToObject('#EffectiveDate'),
            ProductMasterDescription: $('#ProductMasterDescription').val(),
            RiskInformationPrimaryInsuredClientFirstName: $('#PrimaryInsuredClientFirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#PrimaryInsuredClientLastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#PrimaryInsuredClientLastName2').val(),
            RiskInformationPrimaryInsuredClientBirthDate: generalSupport.DatePickerValueInputToObject('#BirthDate'),
            RiskInformationPrimaryInsuredClientSmokerIndicator: $('input:radio[name=SmokerIndicator]:checked').val(),
            RiskInformationPrimaryInsuredClientGender: $('#Gender').val(),
            RiskInformationPrimaryInsuredClientHeight: generalSupport.NumericValue('#Height', -9999, 9999),
            RiskInformationPrimaryInsuredClientWeight: generalSupport.NumericValue('#Weight', -99999, 99999),
            AddresseMailDLIeMailAddresseMail: $('#eMail').val(),
            DireccionMostrar: $('#DireccionMostrar').val(),
            RiskInformationInsuredAmount: generalSupport.NumericValue('#InsuredAmount', -999999999999999999, 999999999999999999),
            RiskInformationCurrency: parseInt(0 + $('#Currency').val(), 10),
            RiskInformationTotalOriginalAnnualPremium: generalSupport.NumericValue('#TotalOriginalAnnualPremium', -99999, 99999),
            BeneficiaryType: $('input:radio[name=BeneficiaryType]:checked').val(),
            QuestionnairesAvailableDiabetes: $('input:radio[name=radiobuttonlist1]:checked').val(),
            QuestionnairesAvailableHeart: $('input:radio[name=radiobuttonlist2]:checked').val(),
            QuestionnairesAvailableCancer: $('input:radio[name=Radiobutton3]:checked').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5CotizacionVidaResumenFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, generalSupport.DateFormat()));
        $('#ProductMasterDescription').val(data.ProductMasterDescription);
        $('#PrimaryInsuredClientFirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#PrimaryInsuredClientLastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#PrimaryInsuredClientLastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationPrimaryInsuredClientBirthDate, generalSupport.DateFormat()));
        if($('input:radio[name=SmokerIndicator][value=' + data.RiskInformationPrimaryInsuredClientSmokerIndicator +']').length===0)
           $('input:radio[name=SmokerIndicator]').prop('checked', false);
        else
           $($('input:radio[name=SmokerIndicator][value=' + data.RiskInformationPrimaryInsuredClientSmokerIndicator +']')).prop('checked', true);
        $('#SmokerIndicator').data('oldValue', data.RiskInformationPrimaryInsuredClientSmokerIndicator);
        $('#SmokerIndicator').val(data.RiskInformationPrimaryInsuredClientSmokerIndicator);

        AutoNumeric.set('#Height', data.RiskInformationPrimaryInsuredClientHeight);
        AutoNumeric.set('#Weight', data.RiskInformationPrimaryInsuredClientWeight);
        $('#eMail').val(data.AddresseMailDLIeMailAddresseMail);
        $('#DireccionMostrar').val(data.DireccionMostrar);
        AutoNumeric.set('#InsuredAmount', data.RiskInformationInsuredAmount);
        AutoNumeric.set('#TotalOriginalAnnualPremium', data.RiskInformationTotalOriginalAnnualPremium);
        if($('input:radio[name=BeneficiaryType][value=' + data.BeneficiaryType +']').length===0)
           $('input:radio[name=BeneficiaryType]').prop('checked', false);
        else
           $($('input:radio[name=BeneficiaryType][value=' + data.BeneficiaryType +']')).prop('checked', true);
        $('#BeneficiaryType').data('oldValue', data.BeneficiaryType);
        $('#BeneficiaryType').val(data.BeneficiaryType);

        if($('input:radio[name=radiobuttonlist1][value=' + data.QuestionnairesAvailableDiabetes +']').length===0)
           $('input:radio[name=radiobuttonlist1]').prop('checked', false);
        else
           $($('input:radio[name=radiobuttonlist1][value=' + data.QuestionnairesAvailableDiabetes +']')).prop('checked', true);
        $('#radiobuttonlist1').data('oldValue', data.QuestionnairesAvailableDiabetes);
        $('#radiobuttonlist1').val(data.QuestionnairesAvailableDiabetes);

        if($('input:radio[name=radiobuttonlist2][value=' + data.QuestionnairesAvailableHeart +']').length===0)
           $('input:radio[name=radiobuttonlist2]').prop('checked', false);
        else
           $($('input:radio[name=radiobuttonlist2][value=' + data.QuestionnairesAvailableHeart +']')).prop('checked', true);
        $('#radiobuttonlist2').data('oldValue', data.QuestionnairesAvailableHeart);
        $('#radiobuttonlist2').val(data.QuestionnairesAvailableHeart);

        if($('input:radio[name=Radiobutton3][value=' + data.QuestionnairesAvailableCancer +']').length===0)
           $('input:radio[name=Radiobutton3]').prop('checked', false);
        else
           $($('input:radio[name=Radiobutton3][value=' + data.QuestionnairesAvailableCancer +']')).prop('checked', true);
        $('#Radiobutton3').data('oldValue', data.QuestionnairesAvailableCancer);
        $('#Radiobutton3').val(data.QuestionnairesAvailableCancer);


        H5CotizacionVidaResumenSupport.LookUpForGender(data.RiskInformationPrimaryInsuredClientGender, source);
        H5CotizacionVidaResumenSupport.LookUpForCurrency(data.RiskInformationCurrency, source);
        H5CotizacionVidaResumenSupport.LookUpForRelationship(source);

        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return filterColumns.SelectedByDefault == true;}));
        if (data.Beneficiary_Beneficiary !== null)
            $('#BeneficiaryTbl').bootstrapTable('load', data.Beneficiary_Beneficiary);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Height', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 2,
            minimumValue: -9999
        });
      new AutoNumeric('#Weight', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      new AutoNumeric('#InsuredAmount', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      new AutoNumeric('#TotalOriginalAnnualPremium', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
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
                H5CotizacionVidaResumenSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/H5CotizacionVidaResumenActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#H5CotizacionVidaResumenFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                H5CotizacionVidaResumenSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/H5CotizacionVidaResumen.aspx?id=' + $('#H5CotizacionVidaResumenFormId').val());
              
          

            });
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5CotizacionVidaResumenMainForm").validate({
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
                ProductMasterDescription: {
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
                Height: {
                    AutoNumericMinValue: -9999,
                    AutoNumericMaxValue: 9999
                },
                Weight: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                eMail: {
                    required: true,
                    maxlength: 50
                },
                InsuredAmount: {
                    AutoNumericMinValue: -999999999999999999,
                    AutoNumericMaxValue: 999999999999999999
                },
                TotalOriginalAnnualPremium: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }
            },
            messages: {
                EffectiveDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                ProductMasterDescription: {
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
                Height: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                Weight: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                eMail: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 50 caracteres máximo'
                },
                InsuredAmount: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999999999999'
                },
                TotalOriginalAnnualPremium: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }
            }
        });

    };
    this.LookUpForGender = function (defaultValue, source) {
        var ctrol = $('#Gender');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionVidaResumenActions.aspx/LookUpForGender", false,
                JSON.stringify({ id: $('#H5CotizacionVidaResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionVidaResumenActions.aspx/LookUpForCurrency", false,
                JSON.stringify({ id: $('#H5CotizacionVidaResumenFormId').val() }),
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

            app.core.SyncWebMethod("/fasi/dli/forms/H5CotizacionVidaResumenActions.aspx/LookUpForRelationship", false,
                JSON.stringify({ id: $('#H5CotizacionVidaResumenFormId').val() }),
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
            uniqueId: 'CoverageCode',
            columns: [{
                field: 'CoverageCode',
                title: 'Cobertura',
                formatter: 'H5CotizacionVidaResumenSupport.CoverageCode_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Description',
                title: 'Plan/Producto',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Suma asegurada',
                formatter: 'H5CotizacionVidaResumenSupport.CoverageWithCalculatedPremiumInsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'H5CotizacionVidaResumenSupport.CoverageWithCalculatedPremiumAnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        H5CotizacionVidaResumenSupport.currentRow = row;
        AutoNumeric.set('#CoverageCode', row.CoverageCode);
        $('#Description1').val(row.Description);
        AutoNumeric.set('#CoverageWithCalculatedPremiumInsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#CoverageWithCalculatedPremiumAnnualPremium', row.AnnualPremium);

    };
    this.BeneficiaryTblSetup = function (table) {
        H5CotizacionVidaResumenSupport.LookUpForRelationship('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
            columns: [{
                field: 'ClientCompleteClientName',
                title: 'Beneficiario',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación',
                formatter: 'H5CotizacionVidaResumenSupport.PercentageShare_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Relationship',
                title: 'Relación',
                formatter: 'H5CotizacionVidaResumenSupport.LookUpForRelationshipFormatter',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.BeneficiaryRowToInput = function (row) {
        H5CotizacionVidaResumenSupport.currentRow = row;
        $('#CompleteClientName').val(row.ClientCompleteClientName);
        AutoNumeric.set('#PercentageShare', row.PercentageShare);
        H5CotizacionVidaResumenSupport.LookUpForRelationship(row.Relationship, '');

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
    this.PercentageShare_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Mi Vida Vale - Resumen');
        

    H5CotizacionVidaResumenSupport.ControlBehaviour();
    H5CotizacionVidaResumenSupport.ControlActions();
    H5CotizacionVidaResumenSupport.ValidateSetup();
    H5CotizacionVidaResumenSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"><caption >Coberturas</caption></table>');
    H5CotizacionVidaResumenSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));
    $("#BeneficiaryTblPlaceHolder").replaceWith('<table id="BeneficiaryTbl"></table>');
    H5CotizacionVidaResumenSupport.BeneficiaryTblSetup($('#BeneficiaryTbl'));




});

