var inactivateUserSupport = new function () {

    // Carga la grid con los registros de inactivación del usuario conectado
    this.loadGrid = function () {
        $('#grdInactivate').bootstrapTable('destroy');
        $('#grdInactivate').bootstrapTable({
            search: false,
            pagination: true,
            smartDisplay: true,
            sidePagination: 'server',
            searchOnEnterKey: false,
            showColumns: false,
            showRefresh: false,
            pageSize: 5,
            pageList: [5, 10, 20],
            locale: generalSupport.LanguageName() == 'es' ? 'es-CR' : 'en-US',
            columns: [
                { field: 'Id', visible: false },
                { field: 'StartingDate', title: dict.From[generalSupport.LanguageName()], formatter: 'schedulerSupport.dateFormatter', halign: 'center', align: 'center' },
                { field: 'EndingDate', title: dict.To[generalSupport.LanguageName()], formatter: 'schedulerSupport.dateFormatter', halign: 'center', align: 'center' },
                { halign: 'center', align: 'center', switchable: false, formatter: 'inactivateUserSupport.removeColumn', width: '3,5%' }
            ],
            ajax: function (params) {
                $.LoadingOverlay("show");
                ajaxJsonHelper.get(constants.fasiApi.diary + 'RetrieveInactivates?offset=' + params.data.offset + '&limit=' + params.data.limit, null,
                    function (data) {
                        $.LoadingOverlay("hide");
                        params.success({
                            total: data.Total,
                            rows: data.Items
                        });
                    });
            }
        });
    };

    // Crea la columna con el ícono de eliminar
    this.removeColumn = function (value, row, index) {
        return '<a href="javascript:inactivateUserSupport.remove(\'' + row.Id + '\');" data-toggle="tooltip-grid" title="' + dict.Delete[generalSupport.LanguageName()] + '" style="color: #d8482f;"><i class="fa fa-trash"></i></a>';
    };

    // Se inicia el plugin de fecha
    this.initializeDateTimePlugin = function () {
        $('#startingInactivate').datetimepicker({
            format: generalSupport.DateFormatWithHour(),
            locale: generalSupport.LanguageName(),
            minDate: new Date()
        });
        $('#endingInactivate').datetimepicker({
            format: generalSupport.DateFormatWithHour(),
            locale: generalSupport.LanguageName(),
            minDate: new Date(),
            useCurrent: false
        });

        $("#startingInactivate").on("dp.change", function (e) {
            $('#endingInactivate').data("DateTimePicker").minDate(e.date);
        });
        $("#endingInactivate").on("dp.change", function (e) {
            $('#startingInactivate').data("DateTimePicker").maxDate(e.date);
        });
    };

    // Configuración de jquery.validate
    this.validateSetup = function () {
        var requiredMesage = dict.RequiredField[generalSupport.LanguageName()];        

        $("#inactivateUserForm").validate({
            rules: {
                startingInactivate: { required: true },
                endingInactivate: { required: true }
            },
            messages: {
                startingInactivate: { required: requiredMesage },
                endingInactivate: { required: requiredMesage }
            }
        });
    };

    // Borra los datos de los campos
    this.clearAll = function () {
        $("#startingInactivate").val(null);
        $("#endingInactivate").val(null);
    };

    // Verifica los datos informados y guarda el registro de inactivación
    this.save = function (event) {
        var formInstance = $("#inactivateUserForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var startingDate = generalSupport.DatePickerWithHourValueInputToObject('#startingInactivate');
            var endingDate = generalSupport.DatePickerWithHourValueInputToObject('#endingInactivate');
            $.LoadingOverlay("show");
            // Verifica si hay usuarios inactivados en el periodo
            ajaxJsonHelper.get(constants.fasiApi.diary + "IsUserInactive", { startingDate: startingDate, endingDate: endingDate },
                function (data) {
                    $.LoadingOverlay("hide");
                                       
                    // Si hay usuario inactivado se presenta un mensaje preguntando si se desea continuar de todos modos
                    if (data)
                        notification.swal.continueConfirmation(dict.InactiveUserIndicator[generalSupport.LanguageName()], dict.ContinueAnyway[generalSupport.LanguageName()], function () { inactivateUserSupport.saveSend(startingDate, endingDate); });
                    else inactivateUserSupport.saveSend(startingDate, endingDate);
                });                      
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);

        event.preventDefault();
    };

    // Guarda el registro de inactivación en la base de datos
    this.saveSend = function (startingDate, endingDate) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.post(constants.fasiApi.diary + "InactivateUser",
            JSON.stringify({ StartingDate: startingDate, EndingDate: endingDate }),
            function (data) {
                $.LoadingOverlay("hide");
                notification.toastr.success('', dict.InactivateUserSuccess[generalSupport.LanguageName()]);
                $('#grdInactivate').bootstrapTable('refresh');
                inactivateUserSupport.clearAll();
            });
    };

    // Elimina un registro
    this.remove = function (id) {        
        notification.swal.deleteConfirmation(null,
            function () {
                $.LoadingOverlay("show");
                ajaxJsonHelper.delete(constants.fasiApi.diary + "DeleteInactivateUser/" + id, null,
                    function (data) {
                        $.LoadingOverlay("hide");                                         
                        $('#grdInactivate').bootstrapTable('refresh');
                    });
            });
    };
};

$(document).ready(function () {
    generalSupport.UserContext();
    inactivateUserSupport.initializeDateTimePlugin();
    inactivateUserSupport.validateSetup();
    inactivateUserSupport.loadGrid();    
    $('#btnSaveInactivate').click(inactivateUserSupport.save);
});