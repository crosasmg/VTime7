var HT5EstadisticaPorProductoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5EstadisticaPorProductoFormId').val(),
            Ramo: parseInt(0 + $('#Ramo').val(), 10),
            Producto: parseInt(0 + $('#Producto').val(), 10),
            AnoProceso: generalSupport.NumericValue('#AnoProceso', 0, 9999),
            AnoAnterior: generalSupport.NumericValue('#AnoAnterior', -99999, 99999),
            AnoActual: generalSupport.NumericValue('#AnoActual', -99999, 99999),
            SiniestralidadAnterior: generalSupport.NumericValue('#SiniestralidadAnterior', -9999999999, 9999999999),
            SiniestralidadActual: generalSupport.NumericValue('#SiniestralidadActual', -9999999999, 9999999999)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5EstadisticaPorProductoFormId').val(data.InstanceFormId);
        AutoNumeric.set('#AnoProceso', data.AnoProceso);
        AutoNumeric.set('#AnoAnterior', data.AnoAnterior);
        AutoNumeric.set('#AnoActual', data.AnoActual);
        chartSupport.Initialization('chart11', {
            type: 'bar',
            Title: 'Siniestralidad anterior',
            LabelsPropertiesName: 'YearOfThePolicy',
            YAxesTitle: 'Valores',
            XAxesTitle: 'Mes',
            Series: {
                Data: data.PROJECTVUL_DETCollection,
                Definitions: [
                     {
                        label: 'Prima anual',
                        argument: 'Premium',
                        backgroundColor: null
                    },                     {
                        label: 'Siniestros',
                        argument: 'AccumulatdAmount',
                        backgroundColor: null
                    }                
                ]
            }
        });
        chartSupport.Initialization('chart4', {
            type: 'bar',
            Title: 'Siniestralidad actual',
            LabelsPropertiesName: 'YearOfThePolicy',
            YAxesTitle: 'Valores',
            XAxesTitle: 'Mes',
            Series: {
                Data: data.PROJECTVULCollection,
                Definitions: [
                     {
                        label: 'Prima anual',
                        argument: 'SurrenderAvailableAmount',
                        backgroundColor: null
                    },                     {
                        label: 'Siniestros',
                        argument: 'ExcessAmount',
                        backgroundColor: null
                    }                
                ]
            }
        });
        AutoNumeric.set('#SiniestralidadAnterior', data.SiniestralidadAnterior);
        AutoNumeric.set('#SiniestralidadActual', data.SiniestralidadActual);

        HT5EstadisticaPorProductoSupport.LookUpForRamo(data.Ramo, source);
        HT5EstadisticaPorProductoSupport.LookUpForProducto(data.Producto, data.Ramo, source);

        if (data.PROJECTVUL_DET_PROJECTVUL_DET !== null)
            $('#PROJECTVUL_DETTbl').bootstrapTable('load', data.PROJECTVUL_DET_PROJECTVUL_DET);
        if (data.PROJECTVUL_PROJECTVUL !== null)
            $('#PROJECTVULTbl').bootstrapTable('load', data.PROJECTVUL_PROJECTVUL);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#AnoProceso', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: 0
        });
      new AutoNumeric('#AnoAnterior', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#AnoActual', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#SiniestralidadAnterior', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 6,
            minimumValue: -9999999999
        });
      new AutoNumeric('#SiniestralidadActual', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 6,
            minimumValue: -9999999999
        });



        $('#Ramo').on('change', function () {
            var value = $('#Ramo').val();

            if (value !== null && value !== '0') {
                var skipData = $('#Ramo').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#Ramo').data("skip", false);
                else
                    HT5EstadisticaPorProductoSupport.LookUpForProducto(null, parseInt(0 + $('#Ramo').val(), 10));
            }
            else
                if($('#Ramo').val() !== $('#Producto').data("parentId1"))
                   $('#Producto').children().remove();
        });



    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5EstadisticaPorProductoSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5EstadisticaPorProductoActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5EstadisticaPorProductoFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                HT5EstadisticaPorProductoSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5EstadisticaPorProducto.aspx?id=' + $('#HT5EstadisticaPorProductoFormId').val());
              
          

            });
    };




    this.ControlActions = function () {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("id");

            switch (target) {
                case "tab14":
                    chartSupport.Update('chart11');
                    break;
                case "tab14":
                    chartSupport.Update('chart4');
                    break;

            }
        });
        $('#button0').click(function (event) {
                var formInstance = $("#HT5EstadisticaPorProductoMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button0'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5EstadisticaPorProductoActions.aspx/button0Click", false,
                          JSON.stringify({
                                        instance: HT5EstadisticaPorProductoSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5EstadisticaPorProductoSupport.ActionProcess(data, 'button0Click');
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
    
        $("#HT5EstadisticaPorProductoMainForm").validate({
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
                Ramo: {
                    required: true
                },
                Producto: {
                    required: true
                },
                AnoProceso: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: 0,
                    AutoNumericMaxValue: 9999
                },
                AnoAnterior: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                AnoActual: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                SiniestralidadAnterior: {
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999
                },
                SiniestralidadActual: {
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999
                }
            },
            messages: {
                Ramo: {
                    required: 'El campo es requerido'
                },
                Producto: {
                    required: 'El campo es requerido'
                },
                AnoProceso: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es 0',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999'
                },
                AnoAnterior: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                AnoActual: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                SiniestralidadAnterior: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                },
                SiniestralidadActual: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                }
            }
        });

    };
    this.LookUpForRamo = function (defaultValue, source) {
        var ctrol = $('#Ramo');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5EstadisticaPorProductoActions.aspx/LookUpForRamo", false,
                JSON.stringify({ id: $('#HT5EstadisticaPorProductoFormId').val() }),
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
    this.LookUpForProducto = function (defaultValue, value1, source) {
        var ctrol = $('#Producto');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/HT5EstadisticaPorProductoActions.aspx/LookUpForProducto", false,
                JSON.stringify({
                                        id: $('#HT5EstadisticaPorProductoFormId').val(),
                    Ramo: value1
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
    this.LookUpForYearOfThePolicyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#YearOfThePolicy>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForYearOfThePolicy2Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#YearOfThePolicy2>option[value='" + value + "']").text();
        }
        return result;
    };

    this.PROJECTVUL_DETTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 12,
            uniqueId: 'YearOfThePolicy',
            columns: [{
                field: 'YearOfThePolicy',
                title: 'Mes',
                formatter: 'HT5EstadisticaPorProductoSupport.LookUpForYearOfThePolicyFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Premium',
                title: 'Prima',
                formatter: 'HT5EstadisticaPorProductoSupport.Premium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AccumulatdAmount',
                title: 'Siniestros',
                formatter: 'HT5EstadisticaPorProductoSupport.AccumulatdAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.PROJECTVUL_DETRowToInput = function (row) {
        HT5EstadisticaPorProductoSupport.currentRow = row;
        AutoNumeric.set('#Premium', row.Premium);
        AutoNumeric.set('#AccumulatdAmount', row.AccumulatdAmount);

    };
    this.PROJECTVULTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 12,
            uniqueId: 'YearOfThePolicy',
            columns: [{
                field: 'YearOfThePolicy',
                title: 'Mes',
                formatter: 'HT5EstadisticaPorProductoSupport.LookUpForYearOfThePolicy2Formatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'SurrenderAvailableAmount',
                title: 'Prima',
                formatter: 'HT5EstadisticaPorProductoSupport.SurrenderAvailableAmount2_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ExcessAmount',
                title: 'Siniestros',
                formatter: 'HT5EstadisticaPorProductoSupport.ExcessAmount2_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.PROJECTVULRowToInput = function (row) {
        HT5EstadisticaPorProductoSupport.currentRow = row;
        AutoNumeric.set('#SurrenderAvailableAmount2', row.SurrenderAvailableAmount);
        AutoNumeric.set('#ExcessAmount2', row.ExcessAmount);

    };


    this.Premium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.AccumulatdAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.SurrenderAvailableAmount2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.ExcessAmount2_FormatterMaskData = function (value, row, index) {          
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
        masterSupport.setPageTitle('HT5 Estadística por producto');
        

    HT5EstadisticaPorProductoSupport.ControlBehaviour();
    HT5EstadisticaPorProductoSupport.ControlActions();
    HT5EstadisticaPorProductoSupport.ValidateSetup();
    HT5EstadisticaPorProductoSupport.Initialization();

    $("#PROJECTVUL_DETTblPlaceHolder").replaceWith('<table id="PROJECTVUL_DETTbl"></table>');
    HT5EstadisticaPorProductoSupport.PROJECTVUL_DETTblSetup($('#PROJECTVUL_DETTbl'));
    $("#PROJECTVULTblPlaceHolder").replaceWith('<table id="PROJECTVULTbl"></table>');
    HT5EstadisticaPorProductoSupport.PROJECTVULTblSetup($('#PROJECTVULTbl'));




});

