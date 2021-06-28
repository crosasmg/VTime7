var InitializationSupport = new function () {

    //*** NOTA: CODIGO ADAPTADO DE FORMA MANUAL, QUEDA PEDIENTE DE VERIFICAR VERSION GENERADA. ***

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#InitializationFormId').val(),
            CleanWorkflows: $('#CleanWorkflows').is(':checked'),
            CleanTasks: $('#CleanTasks').is(':checked'),
            CleanRoleAndUsers: $('#CleanRoleAndUsers').is(':checked'),
            AdminPassword: $('#AdminPassword').val(),
            CleanPendingDoc: $('#CleanPendingDoc').is(':checked'),
            CleanNavegation: $('#CleanNavegation').is(':checked'),
            CleanAnonimoUsers: $('#CleanAnonimoUsers').is(':checked'),
            RolesConfiguration: $('#checkbox3').is(':checked'),
            RoleInit: $('#checkbox1').is(':checked'),
            RoleMode: generalSupport.RadioNumericValue('RolesRadiobuttonlist'),
            RolesListList: generalSupport.Select2GetValue('RolesCheckcombobox', true),
            InitConfigWidgetByRol: $('#checkbox5').is(':checked'),
            VTUsers: $('#CrearUsuariosBO').is(':checked'),
            UserMode: generalSupport.RadioNumericValue('UsuariosRadiobuttonlist'),
            UsersBackOfficeList: generalSupport.Select2GetValue('UsersBOCheckcombobox', true),
            SendCredetialsForEmail: $('#EnviarCredencialesEmail').is(':checked'),
            CopyConfig: $('#CopyConfigCheckbox').is(':checked'),
            CopyConfigMode: generalSupport.RadioNumericValue('ConfigurationRadiobuttonlist'),
            UserToCopyConfig: parseInt(0 + $('#dropdownlist10').val(), 10),
            UsuariosDestino: generalSupport.Select2GetValue('UsuariosDestinoCheckcombobox', true)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#InitializationFormId').val(data.InstanceFormId);
        $('#CleanWorkflows').prop("checked", data.CleanWorkflows);
        $('#CleanTasks').prop("checked", data.CleanTasks);
        $('#CleanRoleAndUsers').prop("checked", data.CleanRoleAndUsers);
        $('#AdminPassword').val(data.AdminPassword);
        $('#CleanPendingDoc').prop("checked", data.CleanPendingDoc);
        $('#CleanNavegation').prop("checked", data.CleanNavegation);
        $('#CleanAnonimoUsers').prop("checked", data.CleanAnonimoUsers);
        $('#checkbox3').prop("checked", data.RolesConfiguration);
        $('#checkbox1').prop("checked", data.RoleInit);
        if ($('input:radio[name=RolesRadiobuttonlist][value=' + data.RoleMode + ']').length === 0) {
            $('input:radio[name=RolesRadiobuttonlist]').prop('checked', false);
            $('input:radio[name=RolesRadiobuttonlist].default').prop('checked', true);
        }
        else
            $($('input:radio[name=RolesRadiobuttonlist][value=' + data.RoleMode + ']')).prop('checked', true);
        $('#RolesRadiobuttonlist').data('oldValue', data.RoleMode);
        $('#RolesRadiobuttonlist').val(data.RoleMode);

        $('#checkbox5').prop("checked", data.InitConfigWidgetByRol);
        $('#CrearUsuariosBO').prop("checked", data.VTUsers);
        if ($('input:radio[name=UsuariosRadiobuttonlist][value=' + data.UserMode + ']').length === 0) {
            $('input:radio[name=UsuariosRadiobuttonlist]').prop('checked', false);
            $('input:radio[name=UsuariosRadiobuttonlist].default').prop('checked', true);
        }
        else
            $($('input:radio[name=UsuariosRadiobuttonlist][value=' + data.UserMode + ']')).prop('checked', true);
        $('#UsuariosRadiobuttonlist').data('oldValue', data.UserMode);
        $('#UsuariosRadiobuttonlist').val(data.UserMode);

        $('#EnviarCredencialesEmail').prop("checked", data.SendCredetialsForEmail);
        $('#CopyConfigCheckbox').prop("checked", data.CopyConfig);
        if ($('input:radio[name=ConfigurationRadiobuttonlist][value=' + data.CopyConfigMode + ']').length === 0) {
            $('input:radio[name=ConfigurationRadiobuttonlist]').prop('checked', false);
            $('input:radio[name=ConfigurationRadiobuttonlist].default').prop('checked', true);
        }
        else
            $($('input:radio[name=ConfigurationRadiobuttonlist][value=' + data.CopyConfigMode + ']')).prop('checked', true);
        $('#ConfigurationRadiobuttonlist').data('oldValue', data.CopyConfigMode);
        $('#ConfigurationRadiobuttonlist').val(data.CopyConfigMode);

        InitializationSupport.LookUpForRolesCheckcombobox(data.RolesListList, source);
        InitializationSupport.LookUpForUsersBOCheckcombobox(data.UsersBackOfficeList, source);
        InitializationSupport.LookUpFordropdownlist10(data.UserToCopyConfig, source);

        InitializationSupport.LookUpForUsuariosDestinoCheckcombobox(data.UsuariosDestino, source, true);
    };

    this.ControlBehaviour = function () {
        this.LookUpForRolesCheckcombobox = function (defaultValue, source) {
            var ctrol = $('#RolesCheckcombobox');
            if (ctrol.children().length === 0) {
                ctrol.children().remove();
                ctrol.append($('<option />').val('0').text(' Cargando...'));

                app.core.AsyncWebMethod('/fasi/dli/forms/InitializationActions.aspx/LookUpForRolesCheckcombobox', false,
                    JSON.stringify({
                        id: $('#InitializationFormId').val(),
                        value: defaultValue
                    }),
                    function (data) {
                        ctrol.children().remove();
                        generalSupport.Select2Load('RolesCheckcombobox', data.d.Data, 'Code', 'SSCHE_CODE', defaultValue, null, null);
                    });
            }
            else
                if (defaultValue !== null) {
                    generalSupport.Select2ItemsRefresh('RolesCheckcombobox', defaultValue);
                }
                else {
                    ctrol.val('').trigger('change')
                }
        };

        this.LookUpForUsuariosDestinoCheckcombobox = function (defaultValue, source, forceRefresh) {
            var ctrol = $('#UsuariosDestinoCheckcombobox');
            if (ctrol.children().length === 0 || forceRefresh) {
                ctrol.children().remove();
                ctrol.append($('<option />').val('0').text(' Cargando...'));
                $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/UsersLkp?userType=4&' + 'Ids=*',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({}),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                        ctrol.children().remove();
                        if (data.Successfully === true) {
                            ctrol.children().remove();
                            generalSupport.Select2Load('UsuariosDestinoCheckcombobox', data.Data, 'Code', 'Description', defaultValue, null, null);
                        }
                        else
                            generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                if (defaultValue !== null) {
                    generalSupport.Select2ItemsRefresh('UsuariosDestinoCheckcombobox', defaultValue);
                }
                else {
                    ctrol.val('').trigger('change')
                }
        };
        this.LookUpForUsersBOCheckcombobox = function (defaultValue, source) {
            var ctrol = $('#UsersBOCheckcombobox');
            if (ctrol.children().length === 0 || forceRefresh) {
                ctrol.children().remove();
                ctrol.append($('<option />').val('0').text(' Cargando...'));
                $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Backoffice/v1/LookupUsers',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({}),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                        ctrol.children().remove();
                        if (data.Successfully === true) {
                            ctrol.children().remove();
                            generalSupport.Select2Load('UsersBOCheckcombobox', data.Data, 'Code', 'Description', defaultValue, null, null);
                        }
                        else
                            generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                if (defaultValue !== null) {
                    generalSupport.Select2ItemsRefresh('UsuariosDestinoCheckcombobox', defaultValue);
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
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                if (source == 'Initialization')
                    InitializationSupport.ObjectToInput(data.d.Data.Instance, source);
                else
                    InitializationSupport.ObjectToInput(data.d.Data, source);

            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.ControlActions = function () {

        $('#CleanAction').click(function (event) {
            var formInstance = $("#InitializationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var errors;
                var adminPass;
                var btnLoading = Ladda.create(document.querySelector('#CleanAction'));
                btnLoading.start();
                $('#AdminPassword').toggleClass('hidden', true);
                $('#AdminPasswordLabel').toggleClass('hidden', true);
                if ($('#CleanWorkflows').is(':checked') === true || $('#CleanRoleAndUsers').is(':checked') === true || $('#CleanPendingDoc').is(':checked') === true || $('#CleanAnonimoUsers').is(':checked') === true || $('#CleanTasks').is(':checked') === true || $('#CleanNavegation').is(':checked') === true) {
                    $.ajax({
                        type: "DELETE",
                        url: constants.fasiApi.base + 'initialization/v1/InitialCleaning?cleanWorkflows=' + $('#CleanWorkflows').is(':checked') + '&cleanRoleAndUsers=' + $('#CleanRoleAndUsers').is(':checked') + '&cleanPendingDoc=' + $('#CleanPendingDoc').is(':checked') + '&cleanAnonimousUsers=' + $('#CleanAnonimoUsers').is(':checked') + '&cleanNavigationDirectory=' + $('#CleanNavegation').is(':checked') + '&cleanTasks=' + $('#CleanTasks').is(':checked'),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({}),
                        headers: {
                            'Accept-Language': localStorage.getItem('languageName')
                        },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                        },
                        success: function (data) {
                            $('#CleanWorkflows').prop("checked", data.Data.WorkflowsCleaned);
                            $('#CleanRoleAndUsers').prop("checked", data.Data.RoleAndUsersCleaned);
                            $('#CleanPendingDoc').prop("checked", data.Data.PendingDocCleaned);
                            $('#CleanAnonimoUsers').prop("checked", data.Data.AnonimousUsersCleaned);
                            $('#CleanNavegation').prop("checked", data.Data.NavigationDirectoryCleaned);

                            if (data.Successfully && !data.Data.RoleAndUsersCleaned)
                                notification.swal.success('', 'Se completó el proceso satisfactoriamente');
                            else if (data.Successfully && data.Data.RoleAndUsersCleaned && data.Data.AdminPass != "")
                                $("#CleanMessage").find(".alert").html('Se completó el proceso satisfactoriamente, ingrese nuevamente con la cuenta de administrador y contraseña: <b>' + data.Data.AdminPass + '</b>');
                            else if (data.Successfully && data.Data.RoleAndUsersCleaned && data.Data.AdminPass == "")
                                $("#CleanMessage").find(".alert").html('Se completó el proceso satisfactoriamente, ingrese nuevamente con la cuenta de administrador.');



                            if (data.Successfully && data.Data.RoleAndUsersCleaned) {
                                $("#CleanMessage").find(".alert").append("<br>Será redireccionado en <b id='countDown'>10</b> segundos.");
                                $("#CleanMessage").removeClass("hide");
                                var timeToRedirect = 10;
                                setInterval(function () {
                                    timeToRedirect--;
                                    if (timeToRedirect < 0) {
                                        app.security.Logout(app.user.userId, false);
                                        window.location.replace(constants.logInPage);
                                    }
                                    else
                                        $("#CleanMessage").find("#countDown").html(timeToRedirect);
                                }, 1000);
                            }

                        },
                        error: function (qXHR, textStatus, errorThrown) {
                            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                        }
                    }).done(function () {
                        btnLoading.stop();
                        app.core.AsyncWebMethod("/fasi/dli/forms/InitializationActions.aspx/CleanAppCaching", false, JSON.stringify({}));
                    });
                }
                else {
                    btnLoading.stop();
                    notification.swal.error('', 'Debe indicar por lo menos una opción');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

        $('#btnInitialize').click(function (event) {
            var formInstance = $("#InitializationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var errors;
                var btnLoading = Ladda.create(document.querySelector('#btnInitialize'));
                btnLoading.start();
                var initSecuritySchema = ($("input[name=RolesRadiobuttonlist]:checked").val() == 0)
                if (($('#checkbox1').is(':checked') === true && initSecuritySchema) || ($('#checkbox1').is(':checked') === true && (initSecuritySchema == false && generalSupport.Select2GetValue('RolesCheckcombobox', true) != "")) || $('#checkbox3').is(':checked') === true || $('#checkbox5').is(':checked') === true) {
                    $.ajax({
                        type: "PUT",
                        url: constants.fasiApi.base + 'initialization/v1/InitializingRoles?initBasicAppRoles=' + $('#checkbox3').is(':checked') + '&initWidgetConfiguration=' + $('#checkbox5').is(':checked') + '&initSecuritySchema=' + initSecuritySchema + '&rolesToInitialize=' + generalSupport.Select2GetValue('RolesCheckcombobox', true).replace(/ /g, ""),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({}),
                        headers: {
                            'Accept-Language': localStorage.getItem('languageName')
                        },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                        },
                        success: function (data) {
                            $('#checkbox3').prop("checked", data.Data.BasicAppRolesCompleted);
                            $('#checkbox1').prop("checked", data.Data.BORolesCompleted);
                            $('#checkbox5').prop("checked", data.Data.WidgetConfigurationCompleted);

                            if (data.Successfully) {
                                notification.swal.success('', 'Se completó el proceso satisfactoriamente');
                            }
                        },
                        error: function (qXHR, textStatus, errorThrown) {
                            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                        }
                    }).done(function () {
                        btnLoading.stop();
                        app.core.AsyncWebMethod("/fasi/dli/forms/InitializationActions.aspx/CleanAppCaching", false, JSON.stringify({}));
                    });
                }
                else {
                    btnLoading.stop();
                    notification.swal.error('', 'Debe indicar por lo menos una opción de inicialización de roles');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);

            event.preventDefault();
        });

        $('#button5').click(function (event) {
            var formInstance = $("#InitializationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var errors;
                var btnLoading = Ladda.create(document.querySelector('#button5'));
                btnLoading.start();
                if ($('#CopyConfigCheckbox').is(':checked') === true || $('#CrearUsuariosBO').is(':checked') === true) {
                    if ($('#CopyConfigCheckbox').is(':checked') === true) {
                        if (generalSupport.Select2GetValue('UsuariosDestinoCheckcombobox', true) != "" && $('#dropdownlist10').val() > 0) {
                            $.ajax({
                                type: "PUT",
                                url: constants.fasiApi.base + 'initialization/v1/CopyUserConfigurationToAll?baseUserCode=' + parseInt(0 + $('#dropdownlist10').val(), 10) + '&copyConfigurationOption=' + generalSupport.RadioNumericValue('ConfigurationRadiobuttonlist') + '&targetUsers=' + generalSupport.Select2GetValue('UsuariosDestinoCheckcombobox', true).replace(/ /g, ""),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({}),
                                headers: {
                                    'Accept-Language': localStorage.getItem('languageName')
                                },
                                beforeSend: function (xhr) {
                                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                                },
                                success: function (data) {
                                    $('#CopyConfigCheckbox').prop("checked", data.Successfully);
                                    if (data.Successfully) {
                                        notification.swal.success('', 'Se configuraron satisfactoriamente todos los usuarios');
                                    }
                                    else {
                                        notification.swal.warning('', data.Reason);
                                    }
                                },
                                error: function (qXHR, textStatus, errorThrown) {
                                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                                }
                            }).done(function () {
                                btnLoading.stop();
                                app.core.AsyncWebMethod("/fasi/dli/forms/InitializationActions.aspx/CleanAppCaching", false, JSON.stringify({}));
                            });
                        } else {
                            notification.swal.error('', 'Debe indicar los usuarios requeridos para la operación');
                            btnLoading.stop();
                        }

                    }
                    if ($('#CrearUsuariosBO').is(':checked') === true) {
                        if (generalSupport.RadioNumericValue('UsuariosRadiobuttonlist') === 0) {
                            $.ajax({
                                type: "PUT",
                                url: constants.fasiApi.base + 'initialization/v1/InitializeBackOfficeUsers?inicializeAll=true&notifyByEmail=' + $('#EnviarCredencialesEmail').is(':checked') + '&usersToInitialize=',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({}),
                                headers: {
                                    'Accept-Language': localStorage.getItem('languageName')
                                },
                                beforeSend: function (xhr) {
                                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                                },
                                success: function (data) {
                                    $('#CrearUsuariosBO').prop("checked", data.Successfully);
                                    if (data.Successfully) {
                                        notification.swal.success('', 'Se iniciaron satisfactoriamente todos los usuarios');
                                        InitializationSupport.LookUpFordropdownlist10(null, null, true);
                                        InitializationSupport.LookUpForUsuariosDestinoCheckcombobox(null, null, true);
                                    }
                                    else {
                                        notification.swal.warning('', data.Reason);
                                    }
                                },
                                error: function (qXHR, textStatus, errorThrown) {
                                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                                }
                            }).done(function () {
                                btnLoading.stop();
                                app.core.AsyncWebMethod("/fasi/dli/forms/InitializationActions.aspx/CleanAppCaching", false, JSON.stringify({}));
                            });
                        }
                        else {
                            var usersToInitialize = generalSupport.Select2GetDescription('UsersBOCheckcombobox', true, generalSupport.Select2GetValue('UsersBOCheckcombobox', true)).replace(/, /g, ";");
                            $.ajax({
                                type: "PUT",
                                url: constants.fasiApi.base + 'initialization/v1/InitializeBackOfficeUsers?inicializeAll=false&notifyByEmail=' + $('#EnviarCredencialesEmail').is(':checked') + '&usersToInitialize=' + usersToInitialize.replace(/ /g, ""),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({}),
                                headers: {
                                    'Accept-Language': localStorage.getItem('languageName')
                                },
                                beforeSend: function (xhr) {
                                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                                },
                                success: function (data) {
                                    $('#CrearUsuariosBO').prop("checked", data.Successfully);
                                    if (data.Successfully) {
                                        notification.swal.success('', 'Se iniciaron satisfactoriamente todos los usuarios');
                                        InitializationSupport.LookUpFordropdownlist10(null, null, true);
                                        InitializationSupport.LookUpForUsuariosDestinoCheckcombobox(null, null, true);
                                    } else {
                                        notification.swal.warning('', data.Reason);
                                    }
                                },
                                error: function (qXHR, textStatus, errorThrown) {
                                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                                }
                            }).done(function () {
                                btnLoading.stop();
                                app.core.AsyncWebMethod("/fasi/dli/forms/InitializationActions.aspx/CleanAppCaching", false, JSON.stringify({}));
                            });
                        }
                    }
                }
                else {
                    notification.swal.error('', 'Debe indicar por lo menos una opción de inicialización de usuarios');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);

            event.preventDefault();
        });


        $('#checkbox1').change(function () {
            var data;
            if ($('#checkbox1').is(':checked') === true) {
                $('#RolesRadiobuttonlistWrap').toggleClass('hidden', false);
                $('#RolesRadiobuttonlistLabel').toggleClass('hidden', false);
                $('#RolesCheckcomboboxWrap').toggleClass('hidden', true);
                $('#RolesCheckcomboboxLabel').toggleClass('hidden', true);
                $('#RolesCheckcomboboxRequired').toggleClass('hidden', true);
                $('input:radio[name=RolesRadiobuttonlist][value=0]').prop('checked', true);
            }
            else {
                $('#RolesRadiobuttonlistWrap').toggleClass('hidden', true);
                $('#RolesRadiobuttonlistLabel').toggleClass('hidden', true);
                $('#RolesCheckcomboboxWrap').toggleClass('hidden', true);
                $('#RolesCheckcomboboxLabel').toggleClass('hidden', true);
                $('#RolesCheckcomboboxRequired').toggleClass('hidden', true);
                generalSupport.Select2ItemsRefresh('RolesCheckcombobox', null);
            }
        });

        $('input:radio[name=RolesRadiobuttonlist]').change(function () {
            var data;
            if (generalSupport.RadioNumericValue('RolesRadiobuttonlist') == '0') {
                $('#RolesCheckcomboboxWrap').toggleClass('hidden', true);
                $('#RolesCheckcomboboxLabel').toggleClass('hidden', true);
                $('#RolesCheckcomboboxRequired').toggleClass('hidden', true);
                generalSupport.Select2ItemsRefresh('RolesCheckcombobox', null);
            }
            else {
                $('#RolesCheckcomboboxWrap').toggleClass('hidden', false);
                $('#RolesCheckcomboboxLabel').toggleClass('hidden', false);
                $('#RolesCheckcomboboxRequired').toggleClass('hidden', false);
            }
        });

        $('#CrearUsuariosBO').change(function () {
            var data;
            if ($('input[name="CrearUsuariosBO"]:checked').length > 0) {
                $('#UsuariosRadiobuttonlistWrap').toggleClass('hidden', false);
                $('#UsuariosRadiobuttonlistLabel').toggleClass('hidden', false);
                $('#checkbox0Wrap').toggleClass('hidden', false);
                $('#checkbox0Label').toggleClass('hidden', false);
                $('#UsersBOCheckcomboboxWrap').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxLabel').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxRequired').toggleClass('hidden', true);
                $('input:radio[name=UsuariosRadiobuttonlist][value=0]').prop('checked', true);
                $('#EnviarCredencialesEmailWrap').toggleClass('hidden', false);
                generalSupport.Select2ItemsRefresh('UsersBOCheckcombobox', null);
            }
            else {
                $('#UsuariosRadiobuttonlistWrap').toggleClass('hidden', true);
                $('#UsuariosRadiobuttonlistLabel').toggleClass('hidden', true);
                $('#checkbox0Wrap').toggleClass('hidden', true);
                $('#checkbox0Label').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxWrap').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxLabel').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxRequired').toggleClass('hidden', true);
                $('#EnviarCredencialesEmailWrap').toggleClass('hidden', true);
                generalSupport.Select2ItemsRefresh('UsersBOCheckcombobox', null);
            }
        });

        $('input:radio[name=UsuariosRadiobuttonlist]').change(function () {
            var data;
            if (generalSupport.RadioNumericValue('UsuariosRadiobuttonlist') == '0') {
                $('#UsersBOCheckcomboboxWrap').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxLabel').toggleClass('hidden', true);
                $('#UsersBOCheckcomboboxRequired').toggleClass('hidden', true);
                generalSupport.Select2ItemsRefresh('UsersBOCheckcombobox', null);
            }
            else {
                $('#UsersBOCheckcomboboxWrap').toggleClass('hidden', false);
                $('#UsersBOCheckcomboboxLabel').toggleClass('hidden', false);
                $('#UsersBOCheckcomboboxRequired').toggleClass('hidden', false);
            }
        });

        $('#UsersBOCheckcombobox').change(function () {
            var data;
            if (generalSupport.RadioNumericValue('UsuariosRadiobuttonlist') == '0') {
                $('#UsuariosCheckcomboboxWrap').toggleClass('hidden', true);
                $('#UsuariosCheckcomboboxLabel').toggleClass('hidden', true);
            }
            else {
                $('#UsuariosCheckcomboboxWrap').toggleClass('hidden', false);
                $('#UsuariosCheckcomboboxLabel').toggleClass('hidden', false);
            }
        });

        $('#CopyConfigCheckbox').change(function () {
            var data;
            if ($('#CopyConfigCheckbox').is(':checked') === true) {
                $('#ConfigurationRadiobuttonlistWrap').toggleClass('hidden', false);
                $('#ConfigurationRadiobuttonlistLabel').toggleClass('hidden', false);
                $($('input:radio[name=ConfigurationRadiobuttonlist][value=0]')).prop('checked', true);
                $('#UsuariosDestinoCheckcomboboxWrap').toggleClass('hidden', false);
                $('#dropdownlist10Wrap').toggleClass('hidden', false);
            }
            else {
                $('#ConfigurationRadiobuttonlistWrap').toggleClass('hidden', true);
                $('#ConfigurationRadiobuttonlistLabel').toggleClass('hidden', true);
                $('#UsuariosDestinoCheckcomboboxWrap').toggleClass('hidden', true);
                $('#dropdownlist10Wrap').toggleClass('hidden', true);
            }
        });

        $('#button3').click(function (event) {
            var formInstance = $("#InitializationMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var successful;
                var btnLoading = Ladda.create(document.querySelector('#button3'));
                btnLoading.start();
                $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'initialization/v1/InitializationSecuritySync',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({}),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                        successful = data.Successfully;

                        if (successful === true) {
                            var message3 = 'Se sincronizó satisfactoriamente a todos los usuarios';
                            notification.swal.success('', message3);
                        }
                        else {
                            var message4 = 'Ocurrio un error en el proceso, intente de nuevo o contacte al administrador';
                            notification.swal.error('', message4);
                        }
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () {
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

        $("#InitializationMainForm").validate({
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

    this.LookUpFordropdownlist10 = function (defaultValue, source, forceRefresh) {
        var ctrol = $('#dropdownlist10');
        if (ctrol.children().length === 0 || forceRefresh) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'Members/v1/UsersLkp?userType=2&' + 'Ids=*',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({}),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
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

    this.Init = function () {
        moment.locale(app.user.languageName);

        generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
            if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
                masterSupport.setPageTitle($.i18n.t('app.title'));
            }

            InitializationSupport.ValidateSetup();

        });


        InitializationSupport.ControlBehaviour();
        InitializationSupport.ControlActions();

        $('#AdminPassword').val(generalSupport.URLStringValue('AdminPassword'));
        InitializationSupport.LookUpFordropdownlist10(generalSupport.URLNumericValue('UserToCopyConfig'));



        InitializationSupport.LookUpForRolesCheckcombobox(null);
        InitializationSupport.LookUpForUsersBOCheckcombobox(null);
        InitializationSupport.LookUpForUsuariosDestinoCheckcombobox(null, null, true);
    };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#InitializationMainForm"),
        CallBack: InitializationSupport.Init
    });
});