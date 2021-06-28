var taskSupport = new function () {

    // Carga la lista de lineas de negocio o ramos
    this.loadLookUpLineOfBusiness = function () {
        if ($('#LineOfBusiness option').length === 0) {
            ajaxJsonHelper.get(constants.fasiApi.backoffice + 'LineOfBusinessLkp', null,
                function (response) {
                    if (response && response.Successfully) {
                        //Se agrega opción vacía
                        $('#LineOfBusiness').append($('<option></option>').val(0).html('&nbsp;'));
                        $.each(response.Data, function (i, item) {                            
                            $('#LineOfBusiness').append($('<option></option>').val(item.Code).html(item.Description));
                        });
                        $('#LineOfBusiness').select2({ language: generalSupport.LanguageName() });
                    }
                });
        }
    };

    // Carga datos en los dropdowns
    this.loadLookUp = function (key, parameter) {
        if ($('#' + key + ' option').length === 0) {
            ajaxJsonHelper.get(constants.fasiApi.diary + 'Lookups?key=' + parameter + '&languageId=' + localStorage.getItem('languageId'), null,
                function (response) {
                    if (response && response.Successfully) {
                        $.each(response.Data, function (i, item) {
                            $('#' + key).append($('<option></option>').val(item.Code).html(item.Description));
                        });
                        $('#' + key).select2({ language: generalSupport.LanguageName()});
                    }
                });
        }
    };

    // Carga datos en el dropdown de usuario y grupos
    this.loadLookUpUsersAndGroups = function () {
        if ($('#owners option').length === 0) {
            ajaxJsonHelper.get(constants.fasiApi.members + 'UsersAndGroups?userType=3', null,
                function (response) {
                    if (response && response.Successfully) {
                        $.each(response.Data, function (i, item) {
                            $('#owners').append($('<option></option>').val(item.Code).html(item.Description));
                            $('#ownersMassive').append($('<option></option>').val(item.Code).html(item.Description));
                        });
                        $('#owners').select2({ multiple: true, language: generalSupport.LanguageName() });
                        $('#ownersMassive').select2({ multiple: true, language: generalSupport.LanguageName()});
                    }
                });
        }
    };

    //Carga las transacciones disponibles
    this.loadLookUpTransaction = function () {
        $('#Transaction').select2({
            placeholder: dict.TransactionSearch[generalSupport.LanguageName()],
            language: generalSupport.LanguageName(),
            maximumInputLength: 20,
            minimumInputLength: 3,
            ajax: {
                type: "GET",
                url:  constants.fasiApi.backoffice + 'GetTransaction',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                delay: 250,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
                },
                data: function (params) {
                    // Se formatean los datos que se envía por parámetro
                    var query = {
                        prefix: params.term ? params.term : '',
                        userSchema: masterSupport.user.schemeCode,
                        transactionCode: ''
                    };
                    return query;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (textStatus !== "abort") {
                        if (jqXHR.status === 401)
                            notification.swal.infoCallback(dict.ExpiredSession[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { window.location.reload(true); });
                        else generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                    }
                },
                processResults: function (response) {
                    if (response.Successfully) {
                        var data = new Array();
                        // Se formatea los datos que recibe el componente
                        $.each(response.Data.Items, function (index, obj) {
                            data.push({ id: obj.split('-')[0].trim(), text: obj });
                        });

                        return {
                            results: data,
                            pagination: {
                                more: false
                            }
                        };
                    }
                }
            }
        });
    };

    // Carga el drop-down con los tiempo
    this.loadLookUpReminder = function () {
        if ($('#alarmDatetime option').length === 0) {
            $('#alarmDatetime').append($('<option></option>').val("0-M").html("0 " + dict.Minute[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("5-M").html("5 " + dict.Minute[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("10-M").html("10 " + dict.Minute[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("15-M").html("15 " + dict.Minute[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("30-M").html("30 " + dict.Minute[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("1-H").html("1 " + dict.Hour[generalSupport.LanguageName()].toLowerCase()));
            $('#alarmDatetime').append($('<option></option>').val("2-H").html("2 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("3-H").html("3 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("4-H").html("4 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("5-H").html("5 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("6-H").html("6 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("7-H").html("7 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("8-H").html("8 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("9-H").html("9 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("10-H").html("10 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("11-H").html("11 " + dict.Hour[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("12-H").html("0,5 " + dict.Day[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("1-D").html("1 " + dict.Day[generalSupport.LanguageName()].toLowerCase()));
            $('#alarmDatetime').append($('<option></option>').val("2-D").html("2 " + dict.Day[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("3-D").html("3 " + dict.Day[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("4-D").html("4 " + dict.Day[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').append($('<option></option>').val("1-S").html("1 " + dict.Week[generalSupport.LanguageName()].toLowerCase()));
            $('#alarmDatetime').append($('<option></option>').val("2-S").html("2 " + dict.Week[generalSupport.LanguageName()].toLowerCase() + "s"));
            $('#alarmDatetime').select2({ language: generalSupport.LanguageName()});
        }
    };

    // Evento de checkbox recordatorio
    this.reminderCheckChanged = function (el) {
        if (el.checked)
            $('#alarmDatetime').removeProp('disabled');
        else
            $('#alarmDatetime').prop('disabled', 'disabled');
    };

    // Verifica si hay todos los datos necesarios y guarda la tarea
    this.saveTask = function (event) {
        var formInstance = $("#schedulerForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var parameters = taskSupport.formToObject();

            $.LoadingOverlay("show");
            var userId = parameters.owners.join(',');
            // Verifica si hay usuarios in activados en el periodo
            ajaxJsonHelper.get(constants.fasiApi.diary + "IsUsersInactive", { users: userId, startingDate: parameters.startingDatetime, endingDate: parameters.endingDatetime },
                function (data) {
                    $.LoadingOverlay("hide");

                    // Si hay usuario in activado se presenta un mensaje preguntando si se desea continuar de todos modos
                    if (data)
                        notification.swal.continueConfirmation(dict.InactiveUserIndicator[generalSupport.LanguageName()], dict.ContinueAnyway[generalSupport.LanguageName()], function () { taskSupport.saveTaskSend(parameters); });
                    else taskSupport.saveTaskSend(parameters);
                });
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);

        event.preventDefault();
    };

    // Guarda la tarea en la base de datos
    this.saveTaskSend = function (parameters) {
        var treeView = $('#treeView').jstree(true);
        parameters.sourceType = treeView.get_node(treeView.get_selected(0)).data.sourceType;

        $.LoadingOverlay("show");
        ajaxJsonHelper.post(constants.fasiApi.diary + "SynchronizeTask?languageId=" + localStorage.getItem('languageId'),
            JSON.stringify(parameters), function (data) {
                $.LoadingOverlay("hide");

                if (parameters.taskId)
                    notification.toastr.success('', dict.UpdateTaskSuccess[generalSupport.LanguageName()]);
                else notification.toastr.success('', dict.AddTaskSuccess[generalSupport.LanguageName()]);

                // Cierra el modal y refresca la grid
                $('#taskModal').modal('hide');
                $('#grdTable').bootstrapTable('refresh');
            });
    };

    this.GetTransactionName = function (code) {
        var result = "";
        ajaxJsonHelper.ajax(constants.fasiApi.backoffice + 'GetTransaction?prefix=' + code + '&userSchema=' + masterSupport.user.schemeCode + '&transactionCode=' + '', 'GET', null,
            function (data) {
                if (data.Successfully === true) {
                    result = data.Data.Items[0].split("-")[1].trim();
                }
            }
            , null, null, false);
        if (result === "") {
            result = code;
        } else {
            result = code + ' - ' +  result;
        }
        return result;
    };

    // Asigna multiples tareas a los usuarios o grupos seleccionados
    this.saveAssignMultiple = function (event) {
        var formInstance = $("#assinedsForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var selections = $('#grdTable').bootstrapTable('getSelections');

            // Se agrega en un array los códigos de las tareas checkeadas
            var taskIds = new Array();
            $.each(selections, function (index, task) {
                taskIds.push(task.TaskID);
            });

            $.LoadingOverlay("show");
            ajaxJsonHelper.put(constants.fasiApi.diary + "UpdateTaskMultipleAssign",
                JSON.stringify({ TaskIds: taskIds, Owners: $("#ownersMassive").val() }), function (data) {
                    $.LoadingOverlay("hide");
                    notification.toastr.success('', dict.UpdateTaskSuccess[generalSupport.LanguageName()]);
                    $('#assignedToModal').modal('hide');
                });
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);

        event.preventDefault();
    };

    // Obtiene los datos de una tarea a través de su código
    this.getTaskById = function (taskId) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.diary + 'RetrieveTaskById/' + taskId, null,
            function (data) {
                $.LoadingOverlay("hide");
                taskSupport.disableOrEnableAllFields(true);
                var editable = false;
                
                if (data.IsOwner) {
                    if (data.Status === 3){
                        $("#status")[0].disabled = false;// Si la tarea está completada y el usuario es el dueño puede editar el estado
                        ("#percentageCompleted")[0].disabled = false;
                    }else  
                        taskSupport.disableOrEnableAllFields(false);//Si es el owner, puede editar todo
                    editable = true;
                }
                else if (data.IsAssigned) {
                    if (data.Status != 3) {
                        taskSupport.disableOrEnableAllFields(false);
                        $("#owners")[0].disabled = true;
                        $("#individualTaskIndicator")[0].disabled = true;
                        $("#warningWhenCompleted")[0].disabled = true;
                        $("#status")[0].disabled = !data.IsWorkingOnIt;
                        $("#percentageCompleted")[0].disabled = !data.IsWorkingOnIt;
                    } else {
                        $("#status")[0].disabled = false
                        $("#percentageCompleted")[0].disabled = false;
                    }
                    editable = true;
                }
                else if (data.IsWorkingOnIt && data.Status != 3) {
                    if (!data.IndividualTaskIndicator) {
                        $("#status")[0].disabled = false;
                        $("#percentageCompleted")[0].disabled = false;
                        editable = true;
                    }
                }

                editable ? $('#btnSave').css('display', '') : $('#btnSave').css('display', 'none');

                $("#taskId").val(data.TaskId);
                $("#taskShortDescription").val(data.TaskShortDescription);
                $("#allDayActivity")[0].checked = data.AllDayActivity;
                $("#startingDatetime").val(data.StartingDatetime ? moment(data.StartingDatetime).format(generalSupport.DateFormatWithHour()) : null);
                $("#endingDatetime").val(data.EndingDatetime ? moment(data.EndingDatetime).format(generalSupport.DateFormatWithHour()) : null);
                $("#individualTaskIndicator")[0].checked = data.IndividualTaskIndicator;
                $("#warningWhenCompleted")[0].checked = data.WarningWhenCompleted;
                $("#taskLongDescription").val(data.TaskLongDescription);
                $("#percentageCompleted").val(data.PercentageCompleted);
                $("#location").val(data.Location);
                $("#priority").val(data.Priority);
                $('#priority').trigger('change');

                $("#status").val(data.Status);
                $('#status').trigger('change');

                $("#owners").val(data.Owners);
                $('#owners').trigger('change');
                $("#LineOfBusiness").val(data.LineOfBusiness);
                $('#LineOfBusiness').trigger('change');

                if (data.VisualtimeTransaction !== "") {
                    var description = taskSupport.GetTransactionName(data.VisualtimeTransaction);
                    var option = new Option(description, data.VisualtimeTransaction, true, true);
                    $('#Transaction').append(option).trigger('change');
                    $('#Transaction').trigger({
                        type: 'select2:select',
                        params: {
                            data: [{ id: data.VisualtimeTransaction, text: description }]
                        }
                    });
                } else {
                    $('#Transaction').val(null).trigger('change');
                }

                $("#alarmActive")[0].checked = data.AlarmActive;
                taskSupport.reminderCheckChanged($("#alarmActive")[0]);
                $("#alarmDatetime").val(data.AlarmDatetime);
                $('#alarmDatetime').trigger('change');

                // Las tareas manuales pueden ser eliminadas por su dueño
                if (data.OriginType == 1 && data.IsOwner) {
                    $("#btnDelete").unbind("click");
                    $('#btnDelete').click(function (event) { taskSupport.removeTaskById(event, data.TaskId); });
                    $('#btnDelete').css('display', '');
                }
                else $('#btnDelete').css('display', 'none');
            });
    };

    // Habilita o bloquea edición de los campos en pantalla
    this.disableOrEnableAllFields = function (disable) {
        $("#taskShortDescription")[0].disabled = disable;
        $("#allDayActivity")[0].disabled = disable;
        $("#startingDatetime")[0].disabled = disable;
        $("#endingDatetime")[0].disabled = disable;
        $("#individualTaskIndicator")[0].disabled = disable;
        $("#warningWhenCompleted")[0].disabled = disable;
        $("#taskLongDescription")[0].disabled = disable;
        $("#percentageCompleted")[0].disabled = disable;
        $("#location")[0].disabled = disable;
        $("#priority")[0].disabled = disable;
        $("#status")[0].disabled = disable;
        $("#LineOfBusiness")[0].disabled = disable;
        $("#owners")[0].disabled = disable;
        $("#alarmActive")[0].disabled = disable;
        $("#alarmDatetime")[0].disabled = disable;
        $("#Transaction")[0].disabled = disable;

        if (disable)
            $('#btnSave').css('display', 'none');
        else
            $('#btnSave').css('display', '');
    };

    // Elimina una tarea a traves de su codigo
    this.removeTaskById = function (event, taskId) {
        // Confirmación
        notification.swal.deleteConfirmation(null,
            function () {
                $.LoadingOverlay("show");
                ajaxJsonHelper.delete(constants.fasiApi.diary + "DeleteTask/" + taskId, null,
                    function (data) {
                        $.LoadingOverlay("hide");
                        notification.toastr.success('', dict.DeleteTaskSuccess[generalSupport.LanguageName()]);

                        // Cierra el modal y actualiza la grid
                        $('#taskModal').modal('hide');
                        $('#grdTable').bootstrapTable('refresh');
                    });
            });
        event.preventDefault();
    };

    // Actualiza el estado de una tarea
    this.updateTaskStatus = function (taskId, status) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.diary + "UpdateTaskStatus", JSON.stringify({ taskId: taskId, status: status }),
            function (data) {
                $.LoadingOverlay("hide");
                notification.toastr.success('', dict.UpdateTaskSuccess[generalSupport.LanguageName()]);
                $('#grdTable').bootstrapTable('refresh');
            });
    };

    // Configuración de jquery validate
    this.validateSetup = function () {
        var requiredMesage = dict.RequiredField[generalSupport.LanguageName()];
        var lessThenOrEqualTo100Message = dict.LessThenOrEqualTo100[generalSupport.LanguageName()];

        $("#schedulerForm").validate({
            rules: {
                startingDatetime: { required: false },
                endingDatetime: { required: false },
                priority: { required: true },
                taskShortDescription: { required: true },
                status: { required: true },
                owners: { required: true },
                percentageCompleted: {
                    required: true,
                    max: 100
                },
                ownersMassive: { required: true },
                alarmDatetime: { required: '#alarmActive:checked' }
            },
            messages: {
                startingDatetime: { required: requiredMesage },
                endingDatetime: { required: requiredMesage },
                priority: { required: requiredMesage },
                taskShortDescription: { required: requiredMesage },
                status: { required: requiredMesage },
                owners: { required: requiredMesage },
                percentageCompleted: {
                    required: requiredMesage,
                    max: lessThenOrEqualTo100Message
                },
                ownersMassive: { required: requiredMesage },
                alarmDatetime: { required: requiredMesage }
            }
        });

        $("#assinedsForm").validate({
            rules: {
                ownersMassive: { required: true }
            },
            messages: {
                ownersMassive: { required: requiredMesage }
            }
        });
    };

    // Obtiene los datos en pantalla y los agrega a un objeto
    this.formToObject = function () {

        var task = {
            taskId: $("#taskId").val(),
            taskShortDescription: $("#taskShortDescription").val(),
            allDayActivity: $("#allDayActivity")[0].checked,
            startingDatetime: $("#startingDatetime").val() !== '' ? moment($("#startingDatetime").val(), generalSupport.DateFormatWithHour()).format('YYYY-MM-DD HH:mm') : moment('0001-01-01').format('YYYY-MM-DDT00:00:00'),
            endingDatetime: $("#endingDatetime").val() !== '' ? moment($("#endingDatetime").val(), generalSupport.DateFormatWithHour()).format('YYYY-MM-DD HH:mm') : moment('0001-01-01').format('YYYY-MM-DDT00:00:00'),
            individualTaskIndicator: $("#individualTaskIndicator")[0].checked,
            warningWhenCompleted: $("#warningWhenCompleted")[0].checked,
            taskLongDescription: $("#taskLongDescription").val(),
            percentageCompleted: $("#percentageCompleted").val(),
            location: $("#location").val(),
            priority: $("#priority").val(),
            status: $("#status").val(),
            owners: $("#owners").val(),
            alarmActive: $("#alarmActive")[0].checked,
            alarmDatetime: $("#alarmDatetime").val(),
            visualtimeTransaction: $('#Transaction').val(),
            LineOfBusiness: $('#LineOfBusiness').val()
        };
        return task;
    };

    // Borra todos los datos de los campos en pantalla
    this.clearAll = function () {
        taskSupport.disableOrEnableAllFields(false);

        $("#taskId").val(null);
        $("#taskShortDescription").val(null);
        $("#allDayActivity")[0].checked = false;
        $("#startingDatetime").val(null);
        $("#endingDatetime").val(null);
        $("#individualTaskIndicator")[0].checked = false;
        $("#warningWhenCompleted")[0].checked = false;
        $("#taskLongDescription").val(null);
        $("#percentageCompleted").val(0);
        $("#location").val(null);
        $("#priority").val(1);
        $('#priority').trigger('change');
        $("#status").val(1);
        $('#status').trigger('change');
        $("#owners").val(null);
        $('#owners').trigger('change');
        $("#alarmActive")[0].checked = false;
        taskSupport.reminderCheckChanged($("#alarmActive")[0]);
        $("#alarmDatetime").val("0-M");
        $('#alarmDatetime').trigger('change');
        $('#Transaction').val(null).trigger('change');

        $("#LineOfBusiness").val(0);
        $('#LineOfBusiness').trigger('change');

        $('#btnDelete').css('display', 'none');
        $("#btnDelete").unbind("click");

        $('#btnSave').css('display', '');
    };

    // Borra el campo de multiple asignación
    this.clearAllAssingModal = function () {
        $("#ownersMassive").val(null);
        $('#ownersMassive').trigger('change');
    };

    // Carga los datos de traducción dinámicos
    this.loadTranslation = function () {
        $('#taskShortDescription')[0].placeholder = dict.Subject[generalSupport.LanguageName()];
        $('#taskShortDescription').attr("title", dict.SubjectTitle[generalSupport.LanguageName()]);

        $('#location')[0].placeholder = dict.Location[generalSupport.LanguageName()];
        $('#location').attr("title", dict.LocationTitle[generalSupport.LanguageName()]);

        $('#startingDatetime')[0].placeholder = dict.StartingTime[generalSupport.LanguageName()];
        $('#startingDatetime').attr("title", dict.StartingTimeTitle[generalSupport.LanguageName()]);

        $('#endingDatetime')[0].placeholder = dict.EndingTime[generalSupport.LanguageName()];
        $('#endingDatetime').attr("title", dict.EndingTimeTitle[generalSupport.LanguageName()]);

        $('#reminder').append(dict.Reminder[generalSupport.LanguageName()]);
        $('#reminder').attr("title", dict.ReminderTitle[generalSupport.LanguageName()]);

        $('#individualIndicator').append(dict.IndividualTaskIndicator[generalSupport.LanguageName()]);
        $('#individualIndicator').attr("title", dict.IndividualTaskIndicatorTitle[generalSupport.LanguageName()]);

        $('#warningCompleted').append(dict.WarningWhenCompleted[generalSupport.LanguageName()]);
        $('#warningCompleted').attr("title", dict.WarningWhenCompletedTitle[generalSupport.LanguageName()]);

        $('#allDay').append(dict.AllDayActivity[generalSupport.LanguageName()]);
        $('#allDay').attr("title", dict.AllDayActivityTitle[generalSupport.LanguageName()]);

        $('#lblPriority').attr("title", dict.PriorityTitle[generalSupport.LanguageName()]);
        $('#lblTaskStatus').attr("title", dict.TaskStatusTitle[generalSupport.LanguageName()]);
        $('#lblAssignedTo').attr("title", dict.AssignedToTitle[generalSupport.LanguageName()]);
        $('#lblCompleted').attr("title", dict.CompletedTitle[generalSupport.LanguageName()]);

        $('#taskLongDescription').attr("title", dict.taskLongDescriptionTitle[generalSupport.LanguageName()]);

        $('#lblTransaction').attr("title", dict.TransactionTitle[generalSupport.LanguageName()]);

        $('#btnSave').append(dict.Save[generalSupport.LanguageName()]);
    };

    // Se inicializa el plugin de fecha
    this.initializeDateTimePlugin = function () {
        $('#startingDatetime').datetimepicker({
            format: generalSupport.DateFormatWithHour(),
            locale: generalSupport.LanguageName(),
            minDate: new Date()
        });
        $('#endingDatetime').datetimepicker({
            format: generalSupport.DateFormatWithHour(),
            locale: generalSupport.LanguageName(),
            minDate: new Date(),
            useCurrent: false
        });

        $("#startingDatetime").on("dp.change", function (e) {
            $('#endingDatetime').data("DateTimePicker").minDate(e.date);
        });
        $("#endingDatetime").on("dp.change", function (e) {
            $('#startingDatetime').data("DateTimePicker").maxDate(e.date);
        });
    };

    this.Init = function () {
        taskSupport.loadTranslation();
        taskSupport.loadLookUp('priority', 'taskpriority');
        taskSupport.loadLookUp('status', 'taskstatus');
        taskSupport.loadLookUpLineOfBusiness();
        taskSupport.loadLookUpUsersAndGroups();
        taskSupport.loadLookUpReminder();
        taskSupport.loadLookUpTransaction();

        taskSupport.validateSetup();
        $('#btnSave').click(taskSupport.saveTask);
        $('#btnSaveAssignMultiple').click(taskSupport.saveAssignMultiple);

        // Para que el combo funcione en el modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () { };

        taskSupport.initializeDateTimePlugin();
    };
};