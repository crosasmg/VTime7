var AccessTrackingSupport = new function () {
    this.currentRow = {};

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#AccessTrackingFormId').val(),
            Start: $('#StartDP').val() !== '' ? moment($('#StartDP').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            Finish: $('#FinishDP').val() !== '' ? moment($('#FinishDP').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#AccessTrackingFormId').val(data.InstanceFormId);
        $('#StartDP').val(generalSupport.ToJavaScriptDateCustom(data.Start, 'DD/MM/YYYY'));
        $('#FinishDP').val(generalSupport.ToJavaScriptDateCustom(data.Finish, 'DD/MM/YYYY'));

        if (data.Client_Client !== null)
            $('#ClientTbl').bootstrapTable('load', data.Client_Client);
    };

    this.ControlBehaviour = function () {
        var dpStarDP = $('#StartDP_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });

        dpStarDP.on('dp.change', function (e) {            
            try {
                if (moment(new Date('0001-01-01T00:00:00')).diff(moment(e.date, 'DD/MM/YYYY')) === 0) {
                    e.date = moment(new Date()).utc();
                } else {
                    e.date = moment(e.date, 'DD/MM/YYYY').utc();
                }
            } catch (e) {
                e.date = e.date = moment(new Date()).utc();
            }
        });

        var dpFinishDP =  $('#FinishDP_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });

        dpFinishDP.on('dp.change', function (e) {
            try {
                if (moment(new Date('0001-01-01T00:00:00')).diff(moment(e.date, 'DD/MM/YYYY')) === 0) {
                    e.date = moment(new Date()).utc();
                } else {
                    e.date = moment(e.date, 'DD/MM/YYYY').utc();
                }
            } catch (e) {
                e.date = e.date = moment(new Date()).utc();
            }
        });

    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                AccessTrackingSupport.ObjectToInput(data.d.Data);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.ControlActions = function () {
        $('#OkButton').click(function (event) {
            var formInstance = $("#AccessTrackingMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#OkButton'));
                btnLoading.start();
                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/AccessTrackingActions.aspx/OkButtonSelectCommandActionUSERSSECURITYTRACE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        USERSSECURITYTRACEEFFECTDATE3: $('#StartDP').val() !== '' ? moment($('#StartDP').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
                        USERSSECURITYTRACEEFFECTDATE4: $('#FinishDP').val() !== '' ? moment($('#FinishDP').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
                    }),
                    success: function (data) {
                        btnLoading.stop();
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                $('#ClientTbl').bootstrapTable('load', data.d.Data);
                            else
                                $('#ClientTbl').bootstrapTable('load', []);
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
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
        $.validator.addMethod("StartDP_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if ( moment($('#StartDP').val(), 'DD/MM/YYYY') > moment($('#FinishDP').val(), 'DD/MM/YYYY') ) {
                    result = false;
                }
            }
            return result;
        });
        $.validator.addMethod("FinishDP_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                if ( moment($('#FinishDP').val(), 'DD/MM/YYYY') < moment($('#StartDP').val(), 'DD/MM/YYYY') ) {
                    result = false;
                }
            }
            return result;
        });

        $("#AccessTrackingMainForm").validate({
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
                StartDP: {
                    required: true,
                    StartDP_Validate1: true
                },
                FinishDP: {
                    required: true,
                    FinishDP_Validate1: true
                }
            },
            messages: {
                StartDP: {
                    required: 'El campo es requerido',
                    StartDP_Validate1: 'La fecha de inicio debe ser menor o igual a la fecha de fin'
                },
                FinishDP: {
                    required: 'El campo es requerido',
                    FinishDP_Validate1: 'La fecha de fin debe ser mayor o igual a la fecha de fin'
                }
            }
        });
    };

    this.ClientTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            columns: [{
                field: 'DateOfIngress',
                title: 'Acceso',
                formatter: 'AccessTrackingSupport.ClientDateOfIngress_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CustomString',
                title: 'Fuente',
                sortable: false,
                halign: 'center'
            }, {
                field: 'eMailAddressDefault',
                title: 'Correo electrónico',
                sortable: false,
                halign: 'center'
            }, {
                field: 'LanguageDescription',
                title: 'Estado',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CustomStringEx',
                title: 'Observación',
                sortable: false,
                halign: 'center'
            }]
        });
    };

    this.ClientRowToInput = function (row) {
        AccessTrackingSupport.currentRow = row;
        $('#DateOfIngress').val(generalSupport.ToJavaScriptDateCustom(row.DateOfIngress, 'DD/MM/YYYY'));
        $('#CustomString').val(row.CustomString);
        $('#eMailAddressDefault').val(row.eMailAddressDefault);
        $('#LanguageDescription').val(row.LanguageDescription);
        $('#CustomStringEx').val(row.CustomStringEx);
    };

    this.ClientDateOfIngress_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row, index);
    };
};
$(function ($) {
    securitySupport.ValidateAccessRoles(['Administrador']);
});
$(document).ready(function () {
    moment.locale('es');

    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Rastro de acceso');

    AccessTrackingSupport.ControlBehaviour();
    AccessTrackingSupport.ControlActions();
    AccessTrackingSupport.ValidateSetup();

    $('#StartDP').val(generalSupport.URLDateValue('Start'));
    $('#FinishDP').val(generalSupport.URLDateValue('Finish'));

    $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"></table>');
    AccessTrackingSupport.ClientTblSetup($('#ClientTbl'));

    $('#StartDP').val(moment().format('DD/MM/YYYY'));
    $('#FinishDP').val(moment().format('DD/MM/YYYY'));
});