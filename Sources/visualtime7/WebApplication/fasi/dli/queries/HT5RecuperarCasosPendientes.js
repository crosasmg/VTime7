var HT5RecuperarCasosPendientesSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5RecuperarCasosPendientesFormId').val(),
            Desde: generalSupport.DatePickerValueInputToObject('#Desde'),
            Hasta: generalSupport.DatePickerValueInputToObject('#Hasta')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5RecuperarCasosPendientesFormId').val(data.InstanceFormId);
        $('#Desde').val(generalSupport.ToJavaScriptDateCustom(data.Desde, generalSupport.DateFormat()));
        $('#Hasta').val(generalSupport.ToJavaScriptDateCustom(data.Hasta, generalSupport.DateFormat()));

        HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASELineOfBusiness(source);
        HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASEProduct(data.RootItemsProduct, data.RootItemsLineOfBusiness, source);

        HT5RecuperarCasosPendientesSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_UNDERWRITINGCASEUnderwritingCaseID_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/RecoveryRequestCase.aspx?uwcaseid='+ row.UnderwritingCaseID +'';

            return true;
        };





        $('#UNDERWRITINGCASELineOfBusiness').on('change', function () {
            var value = $('#UNDERWRITINGCASELineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#UNDERWRITINGCASELineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#UNDERWRITINGCASELineOfBusiness').data("skip", false);
                else
                    HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASEProduct(null, parseInt(0 + $('#UNDERWRITINGCASELineOfBusiness').val(), 10));
            }
            else
                if($('#UNDERWRITINGCASELineOfBusiness').val() !== $('#UNDERWRITINGCASEProduct').data("parentId1"))
                   $('#UNDERWRITINGCASEProduct').children().remove();
        });

        $('#Desde_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#Hasta_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5RecuperarCasosPendientesSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };




    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#HT5RecuperarCasosPendientesMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                HT5RecuperarCasosPendientesSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5RecuperarCasosPendientesMainForm").validate({
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
                Desde: {
                    required: true,
                    DatePicker: true
                },
                Hasta: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                Desde: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                },
                Hasta: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };
    this.LookUpForUNDERWRITINGCASELineOfBusinessFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#UNDERWRITINGCASELineOfBusiness>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUNDERWRITINGCASELineOfBusiness = function (defaultValue, source) {
        var ctrol = $('#UNDERWRITINGCASELineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/HT5RecuperarCasosPendientesActions.aspx/LookUpForUNDERWRITINGCASELineOfBusiness", false,
                JSON.stringify({ id: $('#HT5RecuperarCasosPendientesFormId').val() }),
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
    this.LookUpForUNDERWRITINGCASEProductFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASEProduct(null, row.LineOfBusiness);
            result = $("#UNDERWRITINGCASEProduct>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForUNDERWRITINGCASEProduct = function (defaultValue, value1, source) {
        var ctrol = $('#UNDERWRITINGCASEProduct');
        var parentId1 = ctrol.data("parentId1");
        
        if ((typeof parentId1 == 'undefined' && typeof value1 !== 'undefined') || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/queries/HT5RecuperarCasosPendientesActions.aspx/LookUpForUNDERWRITINGCASEProduct", false,
                JSON.stringify({
                                        id: $('#HT5RecuperarCasosPendientesFormId').val(),
                    RootItemsLineOfBusiness: value1
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

    this.ItemsTblSetup = function (table) {
        HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASELineOfBusiness('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OpenDate',
                title: 'Creación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASELineOfBusinessFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Product',
                title: 'Producto',
                formatter: 'HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASEProductFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ClientName',
                title: 'Asegurado',
                sortable: true,
                halign: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-UnderwritingCaseID',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5RecuperarCasosPendientesSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-UnderwritingCaseID')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_UNDERWRITINGCASEUnderwritingCaseIDContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5RecuperarCasosPendientesSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_UNDERWRITINGCASEUnderwritingCaseID_Item1':
                        HT5RecuperarCasosPendientesSupport.Items_UNDERWRITINGCASEUnderwritingCaseID_Item1(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        HT5RecuperarCasosPendientesSupport.currentRow = row;
        $('#UNDERWRITINGCASEUnderwritingCaseID').val(row.UnderwritingCaseID);
        $('#UNDERWRITINGCASEOpenDate').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));
        HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASELineOfBusiness(row.LineOfBusiness, '');
        HT5RecuperarCasosPendientesSupport.LookUpForUNDERWRITINGCASEProduct(row.Product, row.LineOfBusiness, '');
        $('#UNDERWRITINGCASEClientName').val(row.ClientName);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/HT5RecuperarCasosPendientesActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                UNDERWRITINGCASEOPENDATE1: generalSupport.DatePickerValueInputToObject('#Desde'),
                UNDERWRITINGCASEOPENDATE2: generalSupport.DatePickerValueInputToObject('#Hasta')
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };




};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Recuperar casos pendientes');
        

    HT5RecuperarCasosPendientesSupport.ControlBehaviour();
    HT5RecuperarCasosPendientesSupport.ControlActions();
    HT5RecuperarCasosPendientesSupport.ValidateSetup();

    $('#Desde').val(generalSupport.URLDateValue('Desde'));
    $('#Hasta').val(generalSupport.URLDateValue('Hasta'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Casos</caption></table>');
    HT5RecuperarCasosPendientesSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#Desde').val(moment().format(generalSupport.DateFormat()));
        HT5RecuperarCasosPendientesSupport.ItemsTblRequest();



});

