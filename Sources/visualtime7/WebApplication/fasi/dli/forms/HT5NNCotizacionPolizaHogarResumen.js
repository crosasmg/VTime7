var HT5NNCotizacionPolizaHogarResumenSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogarResumenFormId').val(),
            RiskInformationPrimaryInsuredClientBirthDate: generalSupport.DatePickerValue('#BirthDate'),
            DireccionMostrar: $('#DireccionMostrar').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5NNCotizacionPolizaHogarResumenFormId').val(data.InstanceFormId);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationPrimaryInsuredClientBirthDate, generalSupport.DateFormat()));
        $('#FirstName').html(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#LastName').html(data.RiskInformationPrimaryInsuredClientLastName);
        $('#LastName2').html(data.RiskInformationPrimaryInsuredClientLastName2);
        $('#eMailAddressDefault').html(data.RiskInformationPrimaryInsuredClienteMailAddressDefault);
        $('#DireccionMostrar').val(data.DireccionMostrar);
        AutoNumeric.set('#InsuredValueEstructura', data.BasicInsuredAmountEstructuraInsuredValue);
        AutoNumeric.set('#InsuredValueContenido', data.BasicInsuredAmountContenidoInsuredValue);
        AutoNumeric.set('#TotalOriginalAnnualPremium', data.RiskInformationTotalOriginalAnnualPremium);


        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#InsuredValueEstructura', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      new AutoNumeric('#InsuredValueContenido', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      new AutoNumeric('#TotalOriginalAnnualPremium', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });




        $('#BirthDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogarResumenSupport.ObjectToInput(data.d.Data, source);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarResumenActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogarResumenFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogarResumenSupport.ActionProcess(data, 'Initialization');
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogarResumen.aspx?id=' + $('#HT5NNCotizacionPolizaHogarResumenFormId').val());
              
          

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
    
        $("#HT5NNCotizacionPolizaHogarResumenMainForm").validate({
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
                BirthDate: {
                    required: true,
                    DatePicker: true
                },
                FirstName: {
                    required: true,
                    maxlength: 19
                },
                LastName: {
                    required: true,
                    maxlength: 19
                },
                LastName2: {
                    required: true,
                    maxlength: 19
                },
                eMailAddressDefault: {
                    maxlength: 60
                },
                InsuredValueEstructura: {

                },
                InsuredValueContenido: {

                },
                TotalOriginalAnnualPremium: {

                }
            },
            messages: {
                BirthDate: {
                    required: 'El campo es requerido.',
                    DatePicker: 'La fecha indicada no es válida'
                },
                FirstName: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 19 caracteres máximo'
                },
                LastName: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 19 caracteres máximo'
                },
                LastName2: {
                    required: 'El campo es requerido.',
                    maxlength: 'El campo permite 19 caracteres máximo'
                },
                eMailAddressDefault: {
                    maxlength: 'El campo permite 60 caracteres máximo'
                },
                InsuredValueEstructura: {

                },
                InsuredValueContenido: {

                },
                TotalOriginalAnnualPremium: {

                }
            }
        });

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
                formatter: 'HT5NNCotizacionPolizaHogarResumenSupport.InsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'HT5NNCotizacionPolizaHogarResumenSupport.AnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        HT5NNCotizacionPolizaHogarResumenSupport.currentRow = row;
        $('#Description').val(row.Description);
        AutoNumeric.set('#InsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremium', row.AnnualPremium);

    };


    this.InsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.AnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro - Resumen');
        

    HT5NNCotizacionPolizaHogarResumenSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogarResumenSupport.ControlActions();
    HT5NNCotizacionPolizaHogarResumenSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogarResumenSupport.Initialization();

    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"></table>');
    HT5NNCotizacionPolizaHogarResumenSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));




});

