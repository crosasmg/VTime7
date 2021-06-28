var WidgetsByRoleManagerSupport = new function () {
    this.currentRow = {};

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#WidgetsByRoleManagerFormId').val(),
            RoleWidget_RoleWidget: generalSupport.NormalizeProperties($('#RoleWidgetTbl').bootstrapTable('getData'), ''),
            languageId: generalSupport.NumericValue('#languageId', -99999, 99999),
            RolFilter: $('#Filter').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#WidgetsByRoleManagerFormId').val(data.InstanceFormId);
        AutoNumeric.set('#languageId', data.languageId);

        WidgetsByRoleManagerSupport.LookUpForWidgetId(source);
        WidgetsByRoleManagerSupport.LookUpForFilter(data.RolFilter, source);

        if (data.RoleWidget_RoleWidget !== null)
            $('#RoleWidgetTbl').bootstrapTable('load', data.RoleWidget_RoleWidget);
    };

    this.ControlBehaviour = function () {
        new AutoNumeric('#Id', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
        new AutoNumeric('#RoleId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
        new AutoNumeric('#Secuense', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
        new AutoNumeric('#languageId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                WidgetsByRoleManagerSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.RoleWidget_BeforeShowPopup = function (row, $modal) {
        var LanguageIdValue;
        $('#RoleId').toggleClass('hidden', true);
        $('#RoleIdLabel').toggleClass('hidden', true);
        $('#RoleIdRequired').toggleClass('hidden', true);
        $('#Id').toggleClass('hidden', true);
        $('#IdLabel').toggleClass('hidden', true);
        $('#IdRequired').toggleClass('hidden', true);
        if (row.Id == 0) {
            AutoNumeric.set('#RoleId', $('#Filter').val());

            $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsIndex',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },
                success: function (data) {
                    if (data.Successfully === true) {
                        AutoNumeric.set('#Id', data.Data);
                    }
                    else
                        generalSupport.NotifyFail(data.Reason, data.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
    };
    this.RoleWidget_update = function (row, $modal) {
        var UpdateResult;
        var errors;
        $.ajax({
            type: "PUT",
            url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsUpdate',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -999999999, 999999999), WidgetId: parseInt(0 + $('#WidgetId').val(), 10), IsDefault: $('#IsDefault').is(':checked'), IsEditAllow: $('#IsEditAllow').is(':checked'), IsEditAlowTitle: $('#IsEditAlowTitle').is(':checked'), Secuense: generalSupport.NumericValue('#Secuense', -99999, 99999), RoleId: parseInt(0 + $('#Filter').val(), 10) }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                if (data.Successfully === true) {
                    UpdateResult = data.Successfully;

                    if (UpdateResult == true) {
                        notification.toastr.success('Actualización de widget asociados a un role', 'Se actualizó correctamente');
                        $('#RoleWidgetTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
                        $modal.modal('hide');
                    }
                    else {
                        notification.swal.error('Actualización de widget asociados a un role', 'No se actualizo correctamente');
                    }
                }
                else
                    generalSupport.NotifyFail(data.Reason, data.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.RoleWidget_insert = function (row, $modal) {
        var AddResult;
        var errors;
        $.ajax({
            type: "POST",
            url: constants.fasiApi.base + 'fasi/v1/RoleWidgetsAdd',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -999999999, 999999999), WidgetId: parseInt(0 + $('#WidgetId').val(), 10), IsDefault: $('#IsDefault').is(':checked'), IsEditAllow: $('#IsEditAllow').is(':checked'), Secuense: generalSupport.NumericValue('#Secuense', -99999, 99999), RoleId: parseInt(0 + $('#Filter').val(), 10), IsEditAlowTitle: $('#IsEditAlowTitle').is(':checked') }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            headers: {
                'Accept-Language': localStorage.getItem('languageName')
            },
            success: function (data) {
                if (data.Successfully === true) {
                    AddResult = data.Successfully;

                    if (AddResult == true) {
                        $('#RoleWidgetTbl').bootstrapTable('append', row);
                        $modal.modal('hide');
                        notification.toastr.success('Asignación de widget al role', 'Se asignó correctamente el widget al role');
                    }
                    else {
                        notification.swal.error('Asignación de widget al role', 'No se pudo asignar el widget al role');
                    }
                }
                else
                    generalSupport.NotifyFail(data.Reason, data.Code, true);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.RoleWidget_delete = function (row, $modal) {
        var DeleteResult;
        var errors;
        $.ajax({
            type: "DELETE",
            url: constants.fasiApi.base + 'fasi/v1/RoleWidgetDelete?Id=' + generalSupport.NumericValue('#Id', -999999999, 999999999),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                DeleteResult = data.Successfully;

                if (DeleteResult == true) {
                    $('#RoleWidgetTbl').bootstrapTable('remove', { field: 'Id', values: [generalSupport.NumericValue('#Id', -999999999, 999999999)] });
                    notification.toastr.success('Remoción de la relación de widget y role', 'Se elimino correctamente la relación de widget con role');
                }
                else {
                    notification.toastr.error('Remoción de la relación de widget y role', 'No se puedo eliminar la relación de widget con role');
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

    this.ControlActions = function () {
        $('#Filter').change(function () {
            $('#RoleWidgetCreateBtn').prop('disabled', true);
            $('#RoleWidgetTbl').bootstrapTable('load', []);
        });
        $('#button1').click(function (event) {
            var formInstance = $("#WidgetsByRoleManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var errors;
                var btnLoading = Ladda.create(document.querySelector('#button1'));
                btnLoading.start();
                if (parseInt(0 + $('#Filter').val(), 10) == 0) {
                    notification.swal.error('', 'De indicar un rol');
                }
                else {
                    $.ajax({
                        type: "GET",
                        url: constants.fasiApi.base + 'fasi/v1/WidgetByRole?RoleId=' + parseInt(0 + $('#Filter').val(), 10) + '&languageId=' + generalSupport.NumericValue('#languageId', -99999, 99999),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({}),
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                        },
                        success: function (data) {
                            if (data.Successfully === true) {
                                $('#RoleWidgetTbl').bootstrapTable('load', data.Data.Items);
                            }
                            else
                                generalSupport.NotifyFail(data.Reason, data.Code);
                        },
                        error: function (qXHR, textStatus, errorThrown) {
                            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                        }
                    }); $('#RoleWidgetCreateBtn').prop('disabled', false);
                }
                btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $("#WidgetsByRoleManagerMainForm").validate({
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
                languageId: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }
            },
            messages: {
                languageId: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }
            }
        });
        $("#RoleWidgetEditForm").validate({
            rules: {
                Id: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                RoleId: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999,
                    required: true
                },
                WidgetId: {
                    required: true
                },
                Secuense: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                }
            },
            messages: {
                Id: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido'
                },
                RoleId: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999',
                    required: 'El campo es requerido'
                },
                WidgetId: {
                    required: 'El campo es requerido'
                },
                Secuense: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999',
                    required: 'El campo es requerido'
                }
            }
        });
    };
    this.LookUpForWidgetIdFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#WidgetId>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForWidgetId = function (defaultValue, source) {
        var ctrol = $('#WidgetId');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'fasi/v1/WidgetsLkp?widgetIds=*&' + 'languageId=-1',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                        if (source !== 'Initialization')
                            ctrol.change();
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForFilter = function (defaultValue, source) {
        var ctrol = $('#Filter');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'Members/v1/RolesLkp',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        ctrol.append($('<option />').val(0).text('Indique un rol'));
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                        if (source !== 'Initialization')
                            ctrol.change();
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };

    this.RoleWidgetTblSetup = function (table) {
        WidgetsByRoleManagerSupport.LookUpForWidgetId('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#RoleWidgettoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'Id',
                title: 'Identificador',
                formatter: 'WidgetsByRoleManagerSupport.Id_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'RoleId',
                title: 'Role',
                formatter: 'WidgetsByRoleManagerSupport.RoleId_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'WidgetId',
                title: 'Widget',
                events: 'RoleWidgetActionEvents',
                formatter: 'WidgetsByRoleManagerSupport.LookUpForWidgetIdFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Secuense',
                title: 'Orden',
                formatter: 'WidgetsByRoleManagerSupport.Secuense_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'IsDefault',
                title: 'Seleccionado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsEditAllow',
                title: 'Configurar',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsEditAlowTitle',
                title: 'Título',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center'
            }]
        });

        $('#RoleWidgetTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#RoleWidgetTbl');
            $('#RoleWidgetRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#RoleWidgetRemoveBtn').click(function () {
            notification.swal.deleteRowConfirmation(
                function () {
                    var ids = $.map($('#RoleWidgetTbl').bootstrapTable('getSelections'), function (row) {
                        WidgetsByRoleManagerSupport.RoleWidgetRowToInput(row);
                        WidgetsByRoleManagerSupport.RoleWidget_delete(row, null);

                        return row.Id;
                    });

                    $('#RoleWidgetRemoveBtn').prop('disabled', true);
                });
            event.preventDefault(); // cancel default behavior
        });

        $('#RoleWidgetCreateBtn').click(function () {
            var formInstance = $("#RoleWidgetEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            WidgetsByRoleManagerSupport.RoleWidgetShowModal($('#RoleWidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#RoleWidgetPopup').find('#RoleWidgetSaveBtn').click(function () {
            var formInstance = $("#RoleWidgetEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#RoleWidgetPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#RoleWidgetSaveBtn').html();
                $('#RoleWidgetSaveBtn').html('Procesando...');
                $('#RoleWidgetSaveBtn').prop('disabled', true);

                WidgetsByRoleManagerSupport.currentRow.Id = generalSupport.NumericValue('#Id', -999999999, 999999999);
                WidgetsByRoleManagerSupport.currentRow.RoleId = generalSupport.NumericValue('#RoleId', -999999999, 999999999);
                WidgetsByRoleManagerSupport.currentRow.WidgetId = parseInt(0 + $('#WidgetId').val(), 10);
                WidgetsByRoleManagerSupport.currentRow.Secuense = generalSupport.NumericValue('#Secuense', -99999, 99999);
                WidgetsByRoleManagerSupport.currentRow.IsDefault = $('#IsDefault').is(':checked');
                WidgetsByRoleManagerSupport.currentRow.IsEditAllow = $('#IsEditAllow').is(':checked');
                WidgetsByRoleManagerSupport.currentRow.IsEditAlowTitle = $('#IsEditAlowTitle').is(':checked');

                $('#RoleWidgetSaveBtn').prop('disabled', false);
                $('#RoleWidgetSaveBtn').html(caption);

                if (wm === 'Update') {
                    WidgetsByRoleManagerSupport.RoleWidget_update(WidgetsByRoleManagerSupport.currentRow, $modal);
                }
                else {
                    WidgetsByRoleManagerSupport.RoleWidget_insert(WidgetsByRoleManagerSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.RoleWidgetShowModal = function (md, title, row) {
        row = row || { Id: 0, RoleId: 0, WidgetId: 0, Secuense: 1, IsDefault: null, IsEditAllow: null, IsEditAlowTitle: null };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        WidgetsByRoleManagerSupport.RoleWidgetRowToInput(row);
        $('#Id').prop('disabled', true);
        $('#RoleId').prop('disabled', true);
        WidgetsByRoleManagerSupport.RoleWidget_BeforeShowPopup(row, md);
        md.modal('show');
    };

    this.RoleWidgetRowToInput = function (row) {
        WidgetsByRoleManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        AutoNumeric.set('#RoleId', row.RoleId);
        WidgetsByRoleManagerSupport.LookUpForWidgetId(row.WidgetId, '');
        AutoNumeric.set('#Secuense', row.Secuense);
        $('#IsDefault').prop("checked", row.IsDefault);
        $('#IsEditAllow').prop("checked", row.IsEditAllow);
        $('#IsEditAlowTitle').prop("checked", row.IsEditAlowTitle);
    };

    this.Id_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
    };
    this.RoleId_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
    };
    this.Secuense_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
    };
};
$(function ($) {
    securitySupport.ValidateAccessRoles(['Administrador']);
});
$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    generalSupport.getUser();

    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Mantenimiento de widgets por rol');

    WidgetsByRoleManagerSupport.ControlBehaviour();
    WidgetsByRoleManagerSupport.ControlActions();
    WidgetsByRoleManagerSupport.ValidateSetup();

    AutoNumeric.set('#languageId', generalSupport.URLNumericValue('languageId'));

    $("#RoleWidgetTblPlaceHolder").replaceWith('<table id="RoleWidgetTbl"></table>');
    WidgetsByRoleManagerSupport.RoleWidgetTblSetup($('#RoleWidgetTbl'));

    WidgetsByRoleManagerSupport.LookUpForFilter(0);

    AutoNumeric.set('#languageId', constants.defaultLanguageId);
});

window.RoleWidgetActionEvents = {
    'click .update': function (e, value, row, index) {
        WidgetsByRoleManagerSupport.RoleWidgetShowModal($('#RoleWidgetPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};