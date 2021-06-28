var HT5NNCotizacionVidaInversionResumenSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionVidaInversionResumenFormId').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationPrimaryInsuredClientBirthDate: $('#BirthDate').val() !== '' ? moment($('#BirthDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationPrimaryInsuredClientGender: $('#Gender').val(),
            RiskInformationPrimaryInsuredClientSmokerIndicator: $('input:radio[name=SmokerIndicator]:checked').val(),
            DireccionMostrar: $('#DireccionMostrar').val(),
            Currency: parseInt(0 + $('#Currency').val(), 10),
            BeneficiaryType: $('input:radio[name=BeneficiaryType1]:checked').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionVidaInversionResumenFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#Description').html(data.ProductMasterDescription);
        $('#PrimaryInsuredClientFirstName').html(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#PrimaryInsuredClientLastName').html(data.RiskInformationPrimaryInsuredClientLastName);
        $('#PrimaryInsuredClientLastName2').html(data.RiskInformationPrimaryInsuredClientLastName2);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationPrimaryInsuredClientBirthDate, 'DD/MM/YYYY'));
        AutoNumeric.set('#Height', data.RiskInformationPrimaryInsuredClientHeight);
        AutoNumeric.set('#Weight', data.RiskInformationPrimaryInsuredClientWeight);
        if($('input:radio[name=SmokerIndicator][value=' + data.RiskInformationPrimaryInsuredClientSmokerIndicator +']').length===0)
           $('input:radio[name=SmokerIndicator]').prop('checked', false);
        else
           $($('input:radio[name=SmokerIndicator][value=' + data.RiskInformationPrimaryInsuredClientSmokerIndicator +']')).prop('checked', true);
        $('#SmokerIndicator').data('oldValue', data.RiskInformationPrimaryInsuredClientSmokerIndicator);
        $('#SmokerIndicator').val(data.RiskInformationPrimaryInsuredClientSmokerIndicator);

        $('#eMail').html(data.AddresseMailDLIeMailAddresseMail);
        $('#DireccionMostrar').val(data.DireccionMostrar);
        AutoNumeric.set('#InsuredAmount', data.RiskInformationInsuredAmount);
        if($('input:radio[name=BeneficiaryType1][value=' + data.BeneficiaryType +']').length===0)
           $('input:radio[name=BeneficiaryType1]').prop('checked', false);
        else
           $($('input:radio[name=BeneficiaryType1][value=' + data.BeneficiaryType +']')).prop('checked', true);
        $('#BeneficiaryType1').data('oldValue', data.BeneficiaryType);
        $('#BeneficiaryType1').val(data.BeneficiaryType);


        HT5NNCotizacionVidaInversionResumenSupport.LookUpForGender(data.RiskInformationPrimaryInsuredClientGender);
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForCurrency(data.Currency);
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForInvestmentFund();
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForRelationship1();

        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return filterColumns.SelectedByDefault == true;}));
        if (data.InvestmentFundRelatedToPolicy_InvestmentFundRelatedToPolicy !== null)
            $('#InvestmentFundRelatedToPolicyTbl').bootstrapTable('load', data.InvestmentFundRelatedToPolicy_InvestmentFundRelatedToPolicy);
        if (data.Beneficiary_Beneficiary !== null)
            $('#BeneficiaryTbl').bootstrapTable('load', data.Beneficiary_Beneficiary);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Height', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "9999",
            decimalPlaces: 2,
            minimumValue: "-9999"
        });
      new AutoNumeric('#Weight', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      new AutoNumeric('#InsuredAmount', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });




        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#BirthDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionVidaInversionResumenSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionVidaInversionResumenActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionVidaInversionResumenFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionVidaInversionResumenSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionVidaInversionResumen.aspx?id=' + $('#HT5NNCotizacionVidaInversionResumenFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5NNCotizacionVidaInversionResumenMainForm").validate({
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
                Height: {

                },
                Weight: {

                },
                eMail: {
                    required: true
                },
                InsuredAmount: {

                }
            },
            messages: {
                Height: {

                },
                Weight: {

                },
                eMail: {
                    required: 'El campo es requerido.'
                },
                InsuredAmount: {

                }
            }
        });

    };
    this.LookUpForGender = function (defaultValue) {
        var ctrol = $('#Gender');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionVidaInversionResumenActions.aspx/LookUpForGender",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionVidaInversionResumenFormId').val()}),
                
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
                url: "/fasi/dli/forms/HT5NNCotizacionVidaInversionResumenActions.aspx/LookUpForCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionVidaInversionResumenFormId').val()}),
                
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
    this.LookUpForInvestmentFundFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#InvestmentFund>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForInvestmentFund = function (defaultValue) {
        var ctrol = $('#InvestmentFund');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionVidaInversionResumenActions.aspx/LookUpForInvestmentFund",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionVidaInversionResumenFormId').val()}),
                
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
    this.LookUpForRelationship1Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Relationship1>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRelationship1 = function (defaultValue) {
        var ctrol = $('#Relationship1');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionVidaInversionResumenActions.aspx/LookUpForRelationship1",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionVidaInversionResumenFormId').val()}),
                
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

    this.CoverageWithCalculatedPremiumTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'CoverageCode',
            columns: [{
                field: 'CoverageCode',
                title: 'Cobertura',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.CoverageCode_FormatterMaskData',
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
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.CoverageWithCalculatedPremiumInsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.CoverageWithCalculatedPremiumAnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        HT5NNCotizacionVidaInversionResumenSupport.currentRow = row;
        AutoNumeric.set('#CoverageCode', row.CoverageCode);
        $('#Description1').val(row.Description);
        AutoNumeric.set('#CoverageWithCalculatedPremiumInsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#CoverageWithCalculatedPremiumAnnualPremium', row.AnnualPremium);

    };
    this.InvestmentFundRelatedToPolicyTblSetup = function (table) {
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForInvestmentFund();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'InvestmentFund',
            columns: [{
                field: 'InvestmentFund',
                title: 'Fondo',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.LookUpForInvestmentFundFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PercentageShare',
                title: '%Participación',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.PercentageShare_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ProjectedProfitability',
                title: '%Rentabilidad',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.ProjectedProfitability_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.InvestmentFundRelatedToPolicyRowToInput = function (row) {
        HT5NNCotizacionVidaInversionResumenSupport.currentRow = row;
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForInvestmentFund(row.InvestmentFund);
        AutoNumeric.set('#PercentageShare', row.PercentageShare);
        AutoNumeric.set('#ProjectedProfitability', row.ProjectedProfitability);

    };
    this.BeneficiaryTblSetup = function (table) {
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForRelationship1();
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
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.PercentageShare1_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Relationship',
                title: 'Relación',
                formatter: 'HT5NNCotizacionVidaInversionResumenSupport.LookUpForRelationship1Formatter',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.BeneficiaryRowToInput = function (row) {
        HT5NNCotizacionVidaInversionResumenSupport.currentRow = row;
        $('#CompleteClientName1').val(row.ClientCompleteClientName);
        AutoNumeric.set('#PercentageShare1', row.PercentageShare);
        HT5NNCotizacionVidaInversionResumenSupport.LookUpForRelationship1(row.Relationship);

    };


    this.CoverageCode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CoverageWithCalculatedPremiumInsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.CoverageWithCalculatedPremiumAnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
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
    this.PercentageShare1_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };


};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Mi Inversión Segura - Resumen');
        

    HT5NNCotizacionVidaInversionResumenSupport.ControlBehaviour();
    HT5NNCotizacionVidaInversionResumenSupport.ControlActions();
    HT5NNCotizacionVidaInversionResumenSupport.ValidateSetup();
    HT5NNCotizacionVidaInversionResumenSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"><caption >Coberturas y Prima</caption></table>');
    HT5NNCotizacionVidaInversionResumenSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));
    $("#InvestmentFundRelatedToPolicyTblPlaceHolder").replaceWith('<table id="InvestmentFundRelatedToPolicyTbl"></table>');
    HT5NNCotizacionVidaInversionResumenSupport.InvestmentFundRelatedToPolicyTblSetup($('#InvestmentFundRelatedToPolicyTbl'));
    $("#BeneficiaryTblPlaceHolder").replaceWith('<table id="BeneficiaryTbl"><caption >Beneficiary</caption></table>');
    HT5NNCotizacionVidaInversionResumenSupport.BeneficiaryTblSetup($('#BeneficiaryTbl'));




});

