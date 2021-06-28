var TestPolicyExportSupport = new function () {
    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#TestPolicyExportFormId').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#TestPolicyExportFormId').val(data.InstanceFormId);

        TestPolicyExportSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);
    };

    this.ControlBehaviour = function () {
        this.Items_ROLESNPOLICY_Item1 = function (row) {
            window.open('/fasi/dli/forms/NSF0110A.aspx', '_blank', 'scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

            return true;
        };
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                if (source == 'Initialization')
                    TestPolicyExportSupport.ObjectToInput(data.d.Data.Instance, source);
                else
                    TestPolicyExportSupport.ObjectToInput(data.d.Data, source);

            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.ControlActions = function () {
    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $("#TestPolicyExportMainForm").validate({
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
            },
            messages: {
            }
        });
    };

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
                onCellHtmlData: function (cell, row, col, data) {
                    var result = "";
                    if (data != "") {
                        var html = $.parseHTML(data);

                        $.each(html, function () {
                            if (typeof $(this).html() === 'undefined')
                                result += $(this).text();
                            else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('th-inner') === true)
                                result += $(this).html();
                        });
                    }
                    return result;
                },
                maxNestedTables: 0,
                pdfmake: {
                    enabled: true,
                    docDefinition: {
                        pageOrientation: 'landscape',
                        content: [{
                            layout: {
                                hLineWidth: function (i, node) {
                                    return (i === 0 || i === 1) ? 1 : 0;
                                },
                                vLineWidth: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 2 : 0;
                                },
                                hLineColor: function (i, node) {
                                    return (i === 0 || i === 1) ? 'black' : 'gray';
                                },
                                vLineColor: function (i, node) {
                                    return (i === 0 || i === node.table.widths.length) ? 'white' : 'gray';
                                },
                                fillColor: function (rowIndex, node, columnIndex) {
                                    return (rowIndex % 2 === 0) ? '#DDEBF7' : null;
                                }
                            }
                        }]
                    }
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel', 'pdf'],
            columns: [{
                field: 'NPOLICY',
                title: 'Número de la Póliza',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBRANCH',
                title: 'Ramo Comercial',
                formatter: 'TestPolicyExportSupport.ROLESNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPRODUCT',
                title: 'Código del Producto',
                formatter: 'TestPolicyExportSupport.ROLESNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCERTIF',
                title: 'Número del certificado',
                formatter: 'TestPolicyExportSupport.ROLESNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NROLE',
                title: 'Figura del Cliente',
                formatter: 'TestPolicyExportSupport.ROLESNROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLIENT',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#Items_ROLESNPOLICYContextMenu',
            contextMenuButton: '.menu-NPOLICY',
            beforeContextMenuRow: function (e, row, buttonElement) {
                if (buttonElement && $(buttonElement).hasClass('menu-NPOLICY')) {
                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_ROLESNPOLICYContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                switch ($el.data("item")) {
                    case 'Items_ROLESNPOLICY_Item1':
                        TestPolicyExportSupport.Items_ROLESNPOLICY_Item1(row);
                        break;
                }
            }
        });
    };

    this.ItemsTblRequest = function (params) {
        if ($("#TestPolicyExportMainForm").validate().checkForm()) {
            app.core.AsyncWebMethod("/fasi/dli/queries/TestPolicyExportActions.aspx/ItemsTblDataLoad", false,
                JSON.stringify({
                }),
                function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);
                });
        }
    };

    this.ROLESNBRANCH_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
    };
    this.ROLESNPRODUCT_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
    };
    this.ROLESNCERTIF_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
    };
    this.ROLESNROLE_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
    };

    this.Init = function () {
        moment.locale(generalSupport.UserContext().languageName);

        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
            masterSupport.setPageTitle('Test Policy Export');

        TestPolicyExportSupport.ControlBehaviour();
        TestPolicyExportSupport.ControlActions();
        TestPolicyExportSupport.ValidateSetup();

        $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"></table>');
        TestPolicyExportSupport.ItemsTblSetup($('#ItemsTbl'));

        TestPolicyExportSupport.ItemsTblRequest();

        if (generalSupport.URLStringValue('notheader') === 'y') $('#zoneHeader').toggleClass('hidden', true);
    };
};

$(document).ready(function () {
    TestPolicyExportSupport.Init();
});