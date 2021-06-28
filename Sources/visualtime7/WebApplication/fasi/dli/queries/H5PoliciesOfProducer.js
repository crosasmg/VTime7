var H5PoliciesOfProducerSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5PoliciesOfProducerFormId').val(),
            Intermediario: generalSupport.NumericValue('#Intermediario', -9999999999, 9999999999),
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5PoliciesOfProducerFormId').val(data.InstanceFormId);
        AutoNumeric.set('#Intermediario', data.Intermediario);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));


        H5PoliciesOfProducerSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_RolesNPOLICY_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/ChangeGeneralDataOfClient.aspx?ClientKey='+ row.SCLIENT +'&QueryIndicator=1';

            return true;
        };
       this.Items_RolesNPOLICY_Item2 = function (row) {
           window.location.href = '/fasi/dli/queries/NNConsultaDePoliza.aspx?PolicyID='+ row.NPOLICY +'&accept='+ 'true' +'';

            return true;
        };


      new AutoNumeric('#Intermediario', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });




        $('#RecordEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5PoliciesOfProducerSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };




    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#H5PoliciesOfProducerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                H5PoliciesOfProducerSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5PoliciesOfProducerMainForm").validate({
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
                Intermediario: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999
                },
                RecordEffectiveDate: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                Intermediario: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                },
                RecordEffectiveDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            columns: [{
                field: 'SCLIENT',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBRANCH',
                title: 'Ramo',
                formatter: 'H5PoliciesOfProducerSupport.RolesNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCHDesc',
                title: 'Ramo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPRODUCT',
                title: 'Producto',
                formatter: 'H5PoliciesOfProducerSupport.RolesNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCTDesc',
                title: 'Producto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPOLICY',
                title: 'Póliza',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCERTIF',
                title: 'Certificado',
                formatter: 'H5PoliciesOfProducerSupport.RolesNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DDATE_ORIGI',
                title: 'Inicio Vigencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DNEXTRECEIP',
                title: 'Próxima factura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DNULLDATE',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NNULLCODE',
                title: 'Causa de anulación',
                formatter: 'H5PoliciesOfProducerSupport.RolesNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NNULLCODEDesc',
                title: 'Causa de anulación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINTERMED',
                title: 'Código de Productor',
                formatter: 'H5PoliciesOfProducerSupport.RolesNINTERMED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-NPOLICY',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5PoliciesOfProducerSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-NPOLICY')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_RolesNPOLICYContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5PoliciesOfProducerSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_RolesNPOLICY_Item1':
                        H5PoliciesOfProducerSupport.Items_RolesNPOLICY_Item1(row);
                        break;
                    case 'Items_RolesNPOLICY_Item2':
                        H5PoliciesOfProducerSupport.Items_RolesNPOLICY_Item2(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        H5PoliciesOfProducerSupport.currentRow = row;
        $('#RolesSCLIENT').val(row.SCLIENT);
        $('#RolesSCLIENTDesc').val(row.SCLIENTDesc);
        AutoNumeric.set('#RolesNBRANCH', row.NBRANCH);
        $('#RolesNBRANCHDesc').val(row.NBRANCHDesc);
        AutoNumeric.set('#RolesNPRODUCT', row.NPRODUCT);
        $('#RolesNPRODUCTDesc').val(row.NPRODUCTDesc);
        $('#RolesNPOLICY').val(row.NPOLICY);
        AutoNumeric.set('#RolesNCERTIF', row.NCERTIF);
        $('#RolesDDATE_ORIGI').val(generalSupport.ToJavaScriptDateCustom(row.DDATE_ORIGI, generalSupport.DateFormat()));
        $('#RolesDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        $('#RolesDNEXTRECEIP').val(generalSupport.ToJavaScriptDateCustom(row.DNEXTRECEIP, generalSupport.DateFormat()));
        $('#RolesDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#RolesNNULLCODE', row.NNULLCODE);
        $('#RolesNNULLCODEDesc').val(row.NNULLCODEDesc);
        AutoNumeric.set('#RolesNINTERMED', row.NINTERMED);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5PoliciesOfProducerActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                filter: '',
                INTERMEDIARIONINTERMED4: generalSupport.NumericValue('#Intermediario', -9999999999, 9999999999)
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };


    this.RolesNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesNINTERMED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('H5Pólizas de un intermediario');
        

    H5PoliciesOfProducerSupport.ControlBehaviour();
    H5PoliciesOfProducerSupport.ControlActions();
    H5PoliciesOfProducerSupport.ValidateSetup();

    AutoNumeric.set('#Intermediario', generalSupport.URLNumericValue('Intermediario'));
    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Pólizas</caption></table>');
    H5PoliciesOfProducerSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        H5PoliciesOfProducerSupport.ItemsTblRequest();



});

