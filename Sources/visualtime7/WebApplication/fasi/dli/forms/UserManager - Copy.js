var UserManagerSupport = new function () {
    this.currentRow = {};
    this.ClientIdLkp = [];
    this.ProducerIdLkp = [];

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#UserManagerFormId').val(),
            User_User: generalSupport.NormalizeProperties($('#UserTbl').bootstrapTable('getData'), 'CreationDate,LastLoginDate,LastLockedOutDate'),
            EmailOld: $('#EmailOld').val(),
            EmailChangeResult: $('#EmailChangeResult').is(':checked'),
            Type: generalSupport.NumericValue('#Type', -99999, 99999)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#UserManagerFormId').val(data.InstanceFormId);
        $('#EmailOld').val(data.EmailOld);
        $('#EmailChangeResult').prop("checked", data.EmailChangeResult);
        AutoNumeric.set('#Type', data.Type);

        UserManagerSupport.LookUpForSupervisorId(source);
        UserManagerSupport.LookUpForRolAssiged(source);
        UserManagerSupport.LookUpForGroupAssiged(source);
        UserManagerSupport.LookUpForClientId(source);
        UserManagerSupport.LookUpForProducerId(source);

        $('#UserTbl').bootstrapTable('refreshOptions', { ajax: UserManagerSupport.UserTblRequest });
        if (data.User_User !== null)
            $('#UserTbl').bootstrapTable('load', data.User_User);
    };

    this.ControlBehaviour = function () {
        $('#ClientId').select2({
            dropdownParent: $("#UserPopup"),
            placeholder: '',
            ajax: {
                type: "GET",
                url: constants.fasiApi.base + 'BackOffice/v1/ClientsLkpPagination',
                contentType: "application/json; charset=utf-8",
                delay: 250,
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },

                data: function (params) {
                    var count = 10;
                    // Se formatan los datos que se envía por parámetro
                    var query = {
                        startIndex: params.page ? (((params.page * count) - count) + 1) : 0 + 1,
                        endIndex: params.page ? (params.page * count) : count,
                        filter: params.term ? params.term : ''
                    };
                    return $.param(query);
                },
                processResults: function (response) {
                    // Se formatea los datos que recibe el componente
                    var data = $.map(response.Data.Items, function (obj) {
                        obj.id = obj.Code;
                        obj.text = obj.Description;

                        return obj;
                    });

                    return {
                        results: data,
                        pagination: {
                            more: data.length >= 9
                        }
                    };
                }
            },
            templateResult: function (item) {
                if (item.id) return item.id + ' ' + item.text;
                return item.text;
            },
            templateSelection: function (item) {
                if (item.id) return item.id + ' ' + item.text;
                return item.text;
            }
        });
        $('#ProducerId').select2({
            dropdownParent: $("#UserPopup"),
            placeholder: '',
            ajax: {
                type: "GET",
                url: constants.fasiApi.base + 'BackOffice/v1/ProducerLkpPagination',
                contentType: "application/json; charset=utf-8",
                delay: 250,
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },

                data: function (params) {
                    var count = 10;
                    // Se formatan los datos que se envía por parámetro
                    var query = {
                        startIndex: params.page ? (((params.page * count) - count) + 1) : 0 + 1,
                        endIndex: params.page ? (params.page * count) : count,
                        filter: params.term ? params.term : ''
                    };
                    return $.param(query);
                },
                processResults: function (response) {
                    // Se formatea los datos que recibe el componente
                    var data = $.map(response.Data.Items, function (obj) {
                        obj.id = obj.Code;
                        obj.text = obj.Description;

                        return obj;
                    });

                    return {
                        results: data,
                        pagination: {
                            more: data.length >= 9
                        }
                    };
                }
            },
            templateResult: function (item) {
                if (item.id) return item.id + ' ' + item.text;
                return item.text;
            },
            templateSelection: function (item) {
                if (item.id) return item.id + ' ' + item.text;
                return item.text;
            }
        });

        new AutoNumeric('#UserId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
        new AutoNumeric('#SecurityLevel', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
        new AutoNumeric('#Type', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });

        this.LookUpForRolAssiged = function (defaultValue, source) {
            var ctrol = $('#RolAssiged');
            if (ctrol.children().length === 0) {
                ctrol.children().remove();
                ctrol.append($('<option />').val('0').text(' Cargando...'));
                return $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/UserManagerActions.aspx/LookUpForRolAssiged",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",

                    success: function (data) {
                        ctrol.children().remove();
                        if (data.d.Success === true) {
                            generalSupport.Select2Load('RolAssiged', data.d.Data, 'ROLEID', 'ROLENAME', defaultValue, null, null);
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
                if (defaultValue !== null) {
                    generalSupport.Select2ItemsRefresh('RolAssiged', defaultValue);
                }
                else {
                    ctrol.val('').trigger('change')
                }
        };
        this.LookUpForGroupAssiged = function (defaultValue, source) {
            var ctrol = $('#GroupAssiged');
            if (ctrol.children().length === 0) {
                ctrol.children().remove();
                ctrol.append($('<option />').val('0').text(' Cargando...'));
                return $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/UserManagerActions.aspx/LookUpForGroupAssiged",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",

                    success: function (data) {
                        ctrol.children().remove();
                        if (data.d.Success === true) {
                            generalSupport.Select2Load('GroupAssiged', data.d.Data, 'GROUPID', 'DESCRIPTION', defaultValue, null, null);
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
                if (defaultValue !== null) {
                    generalSupport.Select2ItemsRefresh('GroupAssiged', defaultValue);
                }
                else {
                    ctrol.val('').trigger('change')
                }
        };
        this.GetCheckComboxSelectedValues = function (componentId) {
            var selectedItems = new Array();
            $('#' + componentId + ' option:selected').each(function (index, item) {
                selectedItems.push({ Text: $(item).text(), Value: $(item).val() });
            })
            return selectedItems;
        };

        $('#CreationDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#LastLoginDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        $('#LastLockedOutDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                UserManagerSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.User_Item1_Actions = function (row, $modal) {
        var UnloockedUserResult;
        $.ajax({
            type: "GET",
            url: constants.fasiApi.base + 'Members/v1/PasswordRecovery?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                PasswordRecoveryResult = data.Successfully;
                PasswordRecoveryMessage = data.Reason;

                if (PasswordRecoveryResult == true) {
                    notification.control.message(null, 'Se realizó la petición correctamente, en pocos minutos llega una email a su cuenta!!');
                }
                else {
                    notification.control.error(null, 'No se puede realizar la solicitud de cambio de contraseña');
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.User_Item2_Actions = function (row, $modal) {
        var localEmailOld;
        var ChangeEmail;
        $('#EmailOld').val($('#Email').val());

        $('#popup0Popup').modal('show');
    };
    this.User_Item3_Actions = function (row, $modal) {
        var ResultApproval;
        $.ajax({
            type: "POST",
            url: constants.fasiApi.base + 'Members/v1/UserApproval?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                ResultApproval = data.Successfully;

                if (ResultApproval == true) {
                    notification.swal.success('Aprobación de usuario', 'Se aprobó correctamente el usuario');
                }
                else {
                    notification.swal.error('Aprobación de usuario', 'No se puede aprobar el usuario');
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.User_Item4_Actions = function (row, $modal) {
        var UnlookedUser;
        var errors;
        $.ajax({
            type: "POST",
            url: constants.fasiApi.base + 'Members/v1/UserLockedChangeByUserId?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&state=false',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                UnlookedUser = data.Successfully;

                if (UnlookedUser == true) {
                    notification.swal.success('Desbloqueo de usuario', 'Se desbloquea el usuario correctamente');
                    row.IsLockedOut = false;
                    $('#UserTbl').bootstrapTable('updateByUniqueId', { id: row.UserId, row: row });
                }
                else {
                    notification.swal.error('Desbloqueo de usuario', 'No se puede desbloquear el usuario');
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.User_update = function (row, $modal) {
        var UpdateResult;
        var errors;
        $.ajax({
            type: "PUT",
            url: constants.fasiApi.base + 'Members/v1/UserUpdate',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ UserId: generalSupport.NumericValue('#UserId', -999999999, 999999999), UserName: $('#UserName').val(), Email: $('#Email').val(), RolAssiged: generalSupport.Select2GetValue('RolAssiged', true), ClientId: $('#ClientId').val(), ProducerId: $('#ProducerId').val(), IsAdministrator: $('#IsAdministrator').is(':checked'), AllowScheduler: $('#AllowScheduler').is(':checked'), SupervisorId: parseInt(0 + $('#SupervisorId').val(), 10), GroupAssiged: generalSupport.Select2GetValue('GroupAssiged', true), IsLockedOut: $('#IsLockedOut').is(':checked'), PasswordNeverExpires: $('#PasswordNeverExpires').is(':checked') }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                if (data.Successfully === true) {
                    UpdateResult = data.Successfully;

                    if (UpdateResult == true) {
                        $('#UserTbl').bootstrapTable('updateByUniqueId', { id: row.UserId, row: row });
                        $modal.modal('hide');
                        notification.swal.success('Actualización de usuario', 'Se actualizó correctamente el usuario');
                    }
                    else {
                        notification.swal.error('Actualización de usuario', 'No se pudo actualizar correctamente el usuario');
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
    this.User_BeforeShowPopup = function (row, $modal) {
        $('#UserId').prop('disabled', true);
        $('#UserIdLabel').prop('disabled', true);
        $('#IsEmployee').prop('disabled', true);
        $('#IsEmployeeLabel').prop('disabled', true);
        $('#SecurityLevel').prop('disabled', true);
        $('#SecurityLevelLabel').prop('disabled', true);
        if (row.UserId != 0) {
            $('#UserName').prop('disabled', true);
            $('#UserNameLabel').prop('disabled', true);
            $('#Email').prop('disabled', true);
            $('#EmailLabel').prop('disabled', true);
        }
    };
    this.User_delete = function (row, $modal) {
        var UserDeleteResult;
        var errors;
        $.ajax({
            type: "DELETE",
            url: constants.fasiApi.base + 'Members/v1/UserDeleteById?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&DeleteAllRelatedData=true',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                if (data.Successfully === true) {
                    UserDeleteResult = data.Successfully;

                    if (UserDeleteResult == true) {
                        notification.swal.success('Eliminación de usuario', 'Se eliminó el usuario correctamente');
                        $('#UserTbl').bootstrapTable('remove', { field: 'UserId', values: [generalSupport.NumericValue('#UserId', -999999999, 999999999)] });
                    }
                    else {
                        notification.swal.error('Eliminación de usuario', 'No se pudo eliminar el usuario correctamente');
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

    this.ControlActions = function () {
        $('#btnEmailChange').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var EmailChangeMessage;
                var errors;
                var EmailChangeResult;
                var btnLoading = Ladda.create(document.querySelector('#btnEmailChange'));
                btnLoading.start();
                $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/EmailChange?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&email=' + $('#EmailOld').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({}),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
                        EmailChangeMessage = data.Reason;
                        EmailChangeResult = data.Successfully;

                        if (EmailChangeResult == EmailChangeResult) {
                            $('#popup0Popup').modal('hide');
                            $('#UserTbl').bootstrapTable('refresh');
                            notification.swal.success('Cambio de email', 'Se cambió correctamente el email');
                        }
                        else {
                            notification.swal.error('Cambio de email', 'No cambió correctamente el email');
                        }
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }); btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnAllUser').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnAllUser'));
                btnLoading.start();
                AutoNumeric.set('#Type', 1);

                $('#UserTbl').bootstrapTable('refresh');
                btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnPendingApproval').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnPendingApproval'));
                btnLoading.start();
                AutoNumeric.set('#Type', 2);

                $('#UserTbl').bootstrapTable('refresh');
                btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
        $.validator.addMethod("EmailOld_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
                var EmailExit;
                $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/UserNameEmailExist?email=' + $('#EmailOld').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({}),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                    },
                    success: function (data) {
                        EmailExit = data.Successfully;

                        if (EmailExit == true) {
                            result = false;
                        }
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            return result;
        });

        $("#UserManagerMainForm").validate({
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
            onkeyup: function (element) {
                if ($(element).attr('name') !== 'EmailOld') {
                    $.validator.defaults.onkeyup.apply(this, arguments);
                }
            },
            rules: {
                EmailOld: {
                    required: true,
                    maxlength: 80,
                    EmailOld_Validate1: true
                },
                Type: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }
            },
            messages: {
                EmailOld: {
                    required: 'El campo es requerido',
                    maxlength: 'El campo permite 80 caracteres máximo',
                    EmailOld_Validate1: 'Ya existe un email registrado'
                },
                Type: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }
            }
        });
        $("#UserEditForm").validate({
            rules: {
                UserId: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999
                },
                UserName: {
                    maxlength: 80
                },
                Email: {
                    maxlength: 80
                },
                RolAssiged: {
                    required: true
                },
                SecurityLevel: {
                    AutoNumericMinValue: 1,
                    AutoNumericMaxValue: 9
                },
                CreationDate: {
                    DatePicker: true
                },
                LastLoginDate: {
                    DatePicker: true
                },
                LastLockedOutDate: {
                    DatePicker: true
                }
            },
            messages: {
                UserId: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 999999999'
                },
                UserName: {
                    maxlength: 'El campo permite 80 caracteres máximo'
                },
                Email: {
                    maxlength: 'El campo permite 80 caracteres máximo'
                },
                RolAssiged: {
                    required: 'El campo es requerido'
                },
                SecurityLevel: {
                    AutoNumericMinValue: 'El valor mínimo permitido es 1',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9'
                },
                CreationDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                LastLoginDate: {
                    DatePicker: 'La fecha indicada no es válida'
                },
                LastLockedOutDate: {
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });
    };
    this.LookUpForSupervisorIdFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#SupervisorId>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForSupervisorId = function (defaultValue, source) {
        var ctrol = $('#SupervisorId');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'Members/v1/UsersLkp?userType=2&' + 'Ids=*',
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
    this.LookUpForRolAssigedFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = generalSupport.Select2GetDescription('RolAssiged', true, value);
        }
        return result;
    };
    this.LookUpForGroupAssigedFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = generalSupport.Select2GetDescription('GroupAssiged', true, value);
        }
        return result;
    };
    this.LookUpForClientIdFormatter = function (value, row, index) {
        var result = '';
        if (value === null || value === undefined || value === 0 || value === '') {
            result = '';
        } else {
            UserManagerSupport.ClientIdLkp.forEach(function (elementSource) {
                if (elementSource.Key === value)
                    result = value + ' - ' + elementSource.Value;
            });
            if (result === '') {
                result = '<div id="ClientIdLkp_' + row.UserId + '" >' +
                    '<div class="sk-spinner sk-spinner-wave" style="height:15px !important;" > ' +
                    '<div class="sk-rect1" ></div> ' +
                    '<div class="sk-rect2" ></div > ' +
                    '<div class="sk-rect3" ></div > ' +
                    '<div class="sk-rect4" ></div > ' +
                    '<div class="sk-rect5" ></div > ' +
                    '</div>' +
                    '</div>';

                var name = "ClientIdLkp_" + row.UserId;

                app.core.AsyncGet(constants.fasiApi.backoffice + 'ClientByIdLkp?Id=' + row.ClientId, true, false,
                    JSON.stringify({}),
                    function (data) {
                        if (data !== '') {
                            result = data;
                            $('#' + name + ' div').replaceWith(value + ' - ' + result);
                        } else {
                            $('#' + name + ' div').replaceWith('');
                            data = "";
                        }
                        UserManagerSupport.ClientIdLkp.push({ Key: value, Value: data });
                    });
            }
        }
        return result;
    };
    this.LookUpForClientId = function (defaultValue, source) {
        if (defaultValue) {
            var select = $('#ClientId');

            $.ajax({
                type: "GET",
                url: constants.fasiApi.backoffice + 'ClientByIdLkp?Id=' + defaultValue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },
            }).then(function (response) {
                if (response) {
                    // Se crea el "option" y lo agrega
                    var option = new Option(response, defaultValue, true, true);
                    select.append(option).trigger('change');

                    // Se llama de forma manual el evento de selección
                    select.trigger({
                        type: 'select2:select',
                        params: {
                            data: option
                        }
                    });
                }
            });
        }
    };
    this.LookUpForProducerIdFormatter = function (value, row, index) {
        var result = '';
        if (value === null || value === undefined || value === 0 || value === '') {
            result = '';
        } else {
            UserManagerSupport.ProducerIdLkp.forEach(function (elementSource) {
                if (elementSource.Key === value)
                    result = value + ' - ' + elementSource.Value;
            });
            if (result === '') {
                result = '<div id="ProducerIdLkp_' + row.UserId + '" >' +
                    '<div class="sk-spinner sk-spinner-wave" style="height:15px !important;" > ' +
                    '<div class="sk-rect1" ></div> ' +
                    '<div class="sk-rect2" ></div > ' +
                    '<div class="sk-rect3" ></div > ' +
                    '<div class="sk-rect4" ></div > ' +
                    '<div class="sk-rect5" ></div > ' +
                    '</div>' +
                    '</div>';

                var name = "ProducerIdLkp_" + row.UserId;

                app.core.AsyncGet(constants.fasiApi.backoffice + 'ProducerByIdLkp?Id=' + row.ProducerId, true, false,
                    JSON.stringify({}),
                    function (data) {
                        if (data !== '') {
                            result = data;
                            $('#' + name + ' div').replaceWith(value + ' - ' + result);
                        } else {
                            $('#' + name + ' div').replaceWith('');
                            data = "";
                        }
                        UserManagerSupport.ProducerIdLkp.push({ Key: value, Value: data });
                    });
            }
        }
        return result;
    };
    this.LookUpForProducerId = function (defaultValue, source) {
        if (defaultValue) {
            var select = $('#ProducerId');

            $.ajax({
                type: "GET",
                url: constants.fasiApi.backoffice + 'ProducerByIdLkp?Id=' + defaultValue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
                },
            }).then(function (response) {
                if (response) {
                    // Se crea el "option" y lo agrega
                    var option = new Option(response, defaultValue, true, true);
                    select.append(option).trigger('change');

                    // Se llama de forma manual el evento de selección
                    select.trigger({
                        type: 'select2:select',
                        params: {
                            data: option
                        }
                    });
                }
            });
        }
    };

    this.UserTblRequest = function (params) {
        $.ajax({
            type: "GET",
            url: constants.fasiApi.base + 'Members/v1/UserAllByPage?type=' + generalSupport.NumericValue('#Type', -99999, 99999) + '&startIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0) + 1) + '&endIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0) + ((params.data.limit !== undefined) ? params.data.limit : 0)) + '&filter=' + ((params.data.search !== undefined) ? params.data.search : ''),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
            },
            success: function (data) {
                if (data.Successfully === true) {
                    params.success({
                        total: data.Data.Count,
                        rows: data.Data.Items
                    });
                }
                else
                    generalSupport.NotifyFail(data.Reason, data.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.UserTblSetup = function (table) {
        UserManagerSupport.LookUpForSupervisorId('');
        UserManagerSupport.LookUpForRolAssiged('');
        UserManagerSupport.LookUpForGroupAssiged('');
        UserManagerSupport.LookUpForClientId('');
        UserManagerSupport.LookUpForProducerId('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UserId',
            sidePagination: 'server',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#Usertoolbar',
            columns: [{
                field: 'selected',
                checkbox: true
            }, {
                field: 'UserId',
                title: $.i18n.t('app.form.UserId_Label'),
                formatter: 'UserManagerSupport.UserId_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UserName',
                title: $.i18n.t('app.form.UserName_Label'),
                events: 'UserActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Email',
                title: 'Correo electrónico',
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsEmployee',
                title: 'Empleado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsApproved',
                title: 'Aprobado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'IsAdministrator',
                title: 'Administrador',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'AllowScheduler',
                title: 'Agenda',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'IsLockedOut',
                title: 'Bloqueado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'PasswordNeverExpires',
                title: 'Contraseña nunca expira',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'SupervisorId',
                title: 'Supervisor',
                formatter: 'UserManagerSupport.LookUpForSupervisorIdFormatter',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'RolAssiged',
                title: 'Roles',
                formatter: 'UserManagerSupport.LookUpForRolAssigedFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'GroupAssiged',
                title: 'Equipos',
                formatter: 'UserManagerSupport.LookUpForGroupAssigedFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'SecurityLevel',
                title: 'Nivel seguridad',
                formatter: 'UserManagerSupport.SecurityLevel_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'ClientId',
                title: 'Código cliente',
                formatter: 'UserManagerSupport.LookUpForClientIdFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ProducerId',
                title: 'Código productor',
                formatter: 'UserManagerSupport.LookUpForProducerIdFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CreationDate',
                title: 'Creado',
                formatter: 'UserManagerSupport.UserCreationDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LastLoginDate',
                title: 'Último acceso',
                formatter: 'UserManagerSupport.UserLastLoginDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LastLockedOutDate',
                title: 'Último bloqueo',
                formatter: 'UserManagerSupport.UserLastLockedOutDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#UserContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                UserManagerSupport.UserRowToInput(row);
                if (!buttonElement) {
                    if (row.IsApproved == true)
                        $('#UserContextMenu').find('[data-item="User_Item1"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item1"]').hide();
                    if (row.IsApproved == true)
                        $('#UserContextMenu').find('[data-item="User_Item2"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item2"]').hide();
                    if (row.IsApproved == false)
                        $('#UserContextMenu').find('[data-item="User_Item3"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item3"]').hide();
                    if (row.IsLockedOut == true)
                        $('#UserContextMenu').find('[data-item="User_Item4"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item4"]').hide();

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#UserContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                UserManagerSupport.UserRowToInput(row);
                switch ($el.data("item")) {
                    case 'User_Item1':
                        UserManagerSupport.User_Item1_Actions(row, null);
                        break;
                    case 'User_Item2':
                        UserManagerSupport.User_Item2_Actions(row, null);
                        break;
                    case 'User_Item3':
                        UserManagerSupport.User_Item3_Actions(row, null);
                        break;
                    case 'User_Item4':
                        UserManagerSupport.User_Item4_Actions(row, null);
                        break;
                }
            }
        });

        $('#UserTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#UserTbl');
            $('#UserRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#UserRemoveBtn').click(function () {
            notification.swal.deleteRowConfirmation(
                function () {
                    var ids = $.map($('#UserTbl').bootstrapTable('getSelections'), function (row) {
                        UserManagerSupport.UserRowToInput(row);
                        UserManagerSupport.User_delete(row, null);

                        return row.UserId;
                    });

                    $('#UserRemoveBtn').prop('disabled', true);
                });
            event.preventDefault(); // cancel default behavior
        });

        $('#UserCreateBtn').click(function () {
            var formInstance = $("#UserEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            UserManagerSupport.UserShowModal($('#UserPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#UserPopup').find('#UserSaveBtn').click(function () {
            var formInstance = $("#UserEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#UserPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#UserSaveBtn').html();
                $('#UserSaveBtn').html('Procesando...');
                $('#UserSaveBtn').prop('disabled', true);

                UserManagerSupport.currentRow.UserId = generalSupport.NumericValue('#UserId', -999999999, 999999999);
                UserManagerSupport.currentRow.UserName = $('#UserName').val();
                UserManagerSupport.currentRow.Email = $('#Email').val();
                UserManagerSupport.currentRow.IsEmployee = $('#IsEmployee').is(':checked');
                UserManagerSupport.currentRow.IsApproved = $('#IsApproved').is(':checked');
                UserManagerSupport.currentRow.IsAdministrator = $('#IsAdministrator').is(':checked');
                UserManagerSupport.currentRow.AllowScheduler = $('#AllowScheduler').is(':checked');
                UserManagerSupport.currentRow.IsLockedOut = $('#IsLockedOut').is(':checked');
                UserManagerSupport.currentRow.PasswordNeverExpires = $('#PasswordNeverExpires').is(':checked');
                UserManagerSupport.currentRow.SupervisorId = parseInt(0 + $('#SupervisorId').val(), 10);
                UserManagerSupport.currentRow.RolAssiged = generalSupport.Select2GetValue('RolAssiged', true);
                UserManagerSupport.currentRow.GroupAssiged = generalSupport.Select2GetValue('GroupAssiged', true);
                UserManagerSupport.currentRow.SecurityLevel = generalSupport.NumericValue('#SecurityLevel', 1, 9);
                UserManagerSupport.currentRow.ClientId = $('#ClientId').val();
                UserManagerSupport.currentRow.ProducerId = $('#ProducerId').val();
                UserManagerSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate');
                UserManagerSupport.currentRow.LastLoginDate = generalSupport.DatePickerValue('#LastLoginDate');
                UserManagerSupport.currentRow.LastLockedOutDate = generalSupport.DatePickerValue('#LastLockedOutDate');

                $('#UserSaveBtn').prop('disabled', false);
                $('#UserSaveBtn').html(caption);

                if (wm === 'Update') {
                    UserManagerSupport.User_update(UserManagerSupport.currentRow, $modal);
                }
                else {
                    $('#UserTbl').bootstrapTable('append', UserManagerSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });
    };

    this.UserShowModal = function (md, title, row) {
        row = row || { UserId: 0, UserName: null, Email: null, IsEmployee: null, IsApproved: null, IsAdministrator: null, AllowScheduler: null, IsLockedOut: null, PasswordNeverExpires: null, SupervisorId: 0, RolAssiged: null, GroupAssiged: null, SecurityLevel: 0, ClientId: null, ProducerId: null, CreationDate: null, LastLoginDate: null, LastLockedOutDate: null };

        md.data('id', row.UserId);
        md.find('.modal-title').text(title);

        UserManagerSupport.UserRowToInput(row);
        $('#IsApproved').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#LastLoginDate').prop('disabled', true);
        $('#LastLockedOutDate').prop('disabled', true);
        UserManagerSupport.User_BeforeShowPopup(row, md);
        md.modal('show');
    };

    this.UserRowToInput = function (row) {
        UserManagerSupport.currentRow = row;
        AutoNumeric.set('#UserId', row.UserId);
        $('#UserName').val(row.UserName);
        $('#Email').val(row.Email);
        $('#IsEmployee').prop("checked", row.IsEmployee);
        $('#IsApproved').prop("checked", row.IsApproved);
        $('#IsAdministrator').prop("checked", row.IsAdministrator);
        $('#AllowScheduler').prop("checked", row.AllowScheduler);
        $('#IsLockedOut').prop("checked", row.IsLockedOut);
        $('#PasswordNeverExpires').prop("checked", row.PasswordNeverExpires);
        UserManagerSupport.LookUpForSupervisorId(row.SupervisorId, '');
        UserManagerSupport.LookUpForRolAssiged(row.RolAssiged, '');
        UserManagerSupport.LookUpForGroupAssiged(row.GroupAssiged, '');
        AutoNumeric.set('#SecurityLevel', row.SecurityLevel);
        UserManagerSupport.LookUpForClientId(row.ClientId, '');
        UserManagerSupport.LookUpForProducerId(row.ProducerId, '');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat()));
        $('#LastLoginDate').val(generalSupport.ToJavaScriptDateCustom(row.LastLoginDate, generalSupport.DateFormat()));
        $('#LastLockedOutDate').val(generalSupport.ToJavaScriptDateCustom(row.LastLockedOutDate, generalSupport.DateFormat()));
    };

    this.UserId_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
    };
    this.SecurityLevel_FormatterMaskData = function (value, row, index) {
        return AutoNumeric.format(value, {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
    };

    this.UserCreationDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row, index);
    };
    this.UserLastLoginDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row, index);
    };
    this.UserLastLockedOutDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row, index);
    };
};
$(function ($) {
    securitySupport.ValidateAccessRoles(['Administrador']);
});
$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    generalSupport.getUser();

    generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }

        tableHelperSupport.TranslateColumns('#UserTbl');
    });

    UserManagerSupport.ControlBehaviour();
    UserManagerSupport.ControlActions();
    UserManagerSupport.ValidateSetup();

    $('#EmailOld').val(generalSupport.URLStringValue('EmailOld'));
    AutoNumeric.set('#Type', generalSupport.URLNumericValue('Type'));

    $("#UserTblPlaceHolder").replaceWith('<table id="UserTbl"></table>');
    UserManagerSupport.UserTblSetup($('#UserTbl'));

    new AutoNumeric('#Type', 1);
    $('#UserTbl').bootstrapTable('refreshOptions', { ajax: UserManagerSupport.UserTblRequest });
});

window.UserActionEvents = {
    'click .update': function (e, value, row, index) {
        UserManagerSupport.UserShowModal($('#UserPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};