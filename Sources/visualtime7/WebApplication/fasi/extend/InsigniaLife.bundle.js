var masterSupport = new function () {
    //Obtiene la lista de menús del usuario
    this.getMenu = function () {
        $.ajax({
            type: "GET",
            url: constants.fasiApi.fasi + 'PagesByUserId?userId=' + masterSupport.user.userId + '&languageId=' + localStorage.getItem('languageId'),
            dataType: "json",
            async: false,
            cache: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
            },
            success: function (data) {
                masterSupport.loadMenu(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
    };

    // Carga el menú en la página con los datos del servidor
    this.loadMenu = function (data) {
        if (data !== undefined && data !== null && data.Successfully) {
            var sideMenu = $('#side-menu');

            var pageId = masterSupport.getUrlParameter('pageId');
            $.each(data.Data, function (index, item) {
                sideMenu.append(masterSupport.makeMenuItem(index, item, pageId, 1));
            });
        }
    };

    // Crea un item en el menú, su llamada puede ser recursiva para menús que continen sub menú
    this.makeMenuItem = function (index, item, pageId, level) {
        var htmlItem = '';
        var isActive = false;
        // Verifica se el menú contiene sub menús
        var hasSubItems = item.Children && item.Children.length > 0;

        // Cuando el id es mayor que zero significa que es de la página default
        var href = (item.Id > 0)
            ? constants.defaultPage + '?pageId=' + item.Id + '"'
            : (hasSubItems)
                ? '#'
                : item.Url;

        // Verifica si el menú está activo (seleccionado)
        if ((index === 0 && level === 1 && pageId === '' && window.location.pathname === constants.defaultPage)
            || (pageId !== '' && pageId === item.Id.toString())
            || (item.Url && window.location.pathname === item.Url.split('?')[0])) {
            isActive = true;

            $('#menuItemName').val(item.Title);
            $('#menuOrder').val(item.Order);
            $('#btnSaveMenuItem').click(function (event) { masterSupport.btnSaveMenuItemClick(event, item.Id); });
            $('#btnDeleteMenuItem').click(function (event) { masterSupport.btnDeleteMenuItemClick(event, item.Id); });

            if (item.LayoutType < 1 || item.LayoutType > 3 || !item.LayoutType)
                item.LayoutType = 3;

            $("#columns" + item.LayoutType).attr('checked', true);
        }

        htmlItem = '<li id="' + item.Id + '" {activeClass} data-layout="' + item.LayoutType + '">' +
            '<a href="' + href + '">' +
            (item.Icon ? '<i class="' + item.Icon + '"></i>' : '') +
            '<span class="nav-label">' + item.Title + '</span>' +
            (hasSubItems ? '<span class="fa arrow"></span>' : '') +
            '</a>';

        if (hasSubItems) {
            var classLevel = (level === 1 ? "nav-second-level" : "nav-third-level");

            htmlItem += '<ul class="nav ' + classLevel + (isActive ? '' : ' collapse') + '">';

            $.each(item.Children, function (indexSubItem, subItem) {
                htmlItem += masterSupport.makeMenuItem(indexSubItem, subItem, pageId, ++level);
            });
            htmlItem += '</ul>';
        }
        else htmlItem += '</li>';

        htmlItem = htmlItem.replace('{activeClass}', (isActive || htmlItem.indexOf('class="active"') !== -1 ? 'class="active"' : ''));
        return htmlItem;
    };

    // Obtiene el usuario, sea autenticado o anónimo
    this.getUser = function () {
        // Si no existe lenguaje seleccionada le pone la default
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }

        // Obtiene el código del usuario
        $.ajax({
            type: "GET",
            url: '/fasi/wmethods/User.aspx/GetUserInformation',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            success: function (data) {
                masterSupport.user = {
                    userId: data.d.userId,
                    companyId: data.d.companyId,
                    isAnonymous: data.d.isAnonymous,
                    schemeCode: data.d.schemeCode,
                    token: data.d.token,
                    clientId: data.d.clientId,
                    producerId: data.d.producerId,
                    firstNameAndSecondLastName: data.d.firstNameAndSecondLastName,
                    languageID: data.d.languageID,
                    languageName: data.d.languageName,
                    type: data.d.type
                };

                if (!masterSupport.user.isAnonymous) {
                    if (!localStorage.getItem('languageId')) {
                        localStorage.setItem('languageId', masterSupport.user.languageID);
                        localStorage.setItem('languageName', masterSupport.user.languageName);
                    }

                    $('#profileDropdown').css('display', '');
                    $('#userName').html(masterSupport.user.firstNameAndSecondLastName);
                    $('#userType').html(masterSupport.user.type);

                    // Habilita las funcionalidades de usuario autenticado
                    $('#signIcon').prop('class', 'fa fa-sign-out');
                    $('#signLink').on('click', function () { securitySupport.Logout(masterSupport.user.userId, true); });
                    $('#signIcon').prop('title', dict.LogOut[localStorage.getItem('languageName')]);

                    if (window.location.pathname === constants.defaultPage) {
                        $("#small-chat").css('display', '');
                        $('#menuConfiguration').css('display', '');
                    }
                    $(".theme-config-box").prop('title', dict.Configuration[localStorage.getItem('languageName')]);
                } else {
                    // Si es usuario anónimo se crea un nuevo usuario
                    if (masterSupport.user.isAnonymous && masterSupport.user.token !== "") {
                        $.ajax({
                            type: "POST",
                            url: constants.fasiApi.members + 'UserAnonymous',
                            async: false,
                            cache: false,
                            dataType: "json",
                            beforeSend: function (xhr) {
                                xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
                            },
                            success: function (data) {
                                masterSupport.user.userId = data.Data;
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                            }
                        });
                    }

                    $('#signLink').on('click', function () { window.location.replace(constants.logInPage); });
                    $('#signIcon').prop('title', dict.LogIn[localStorage.getItem('languageName')]);
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

    // Carga las opciones de lenguaje
    this.loadLanguage = function () {
        ajaxJsonHelper.get(constants.fasiApi.fasi + 'Language?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                if (data && data.length > 0) {
                    var languageSelection = $('#languageSelection');
                    languageSelection.html('');

                    $.each(data, function (index, item) {
                        if (index !== 0)
                            languageSelection.append('<li class="divider"></li>');

                        languageSelection.append('<li>' +
                            '<a href="javascript:masterSupport.changeLanguage(' + item.Code + ',\'' + item.CultureCode + '\')">' +
                            '<div>' + item.Description + '</div>' +
                            '</a>' +
                            '</li>');
                    });
                }
                translator.initialize(localStorage.getItem('languageName'));
            },
            function () {
                translator.initialize(localStorage.getItem('languageName'));
            });
    };

    // Cambia la lenguaje
    this.changeLanguage = function (languageId, languageName) {
        if (languageId != localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', languageId);
            localStorage.setItem('languageName', languageName);
            window.location.reload(true);
        }
    };

    // Obtiene el valor de parámetros de la url (por nombre)
    this.getUrlParameter = function (name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    };

    // Control de visibilidad de la configuración de página
    this.controlActions = function () {
        $('.spin-icon').click(function () {
            $(".theme-config-box").toggleClass("show");
        });
    };

    // Agrega un nuevo item al menú
    this.createMenuItem = function () {
        ajaxJsonHelper.post(constants.fasiApi.fasi + 'PageAdd?userId=' + masterSupport.user.userId + '&languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                if (data !== undefined && data !== null && data.Successfully) {
                    window.location.replace(constants.defaultPage + '?pageId=' + data.Data.Id);
                }
            });
    };

    // Guarda los datos de la página
    this.btnSaveMenuItemClick = function (event, pageId) {
        var formInstance = $("#menuItemMainForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var layoutType = $('input:radio[name=Columns]:checked').val();
            if ($('#' + pageId).data('layout') != layoutType)
                defaultPage.resetGridStackColumns(layoutType);

            ajaxJsonHelper.post(constants.fasiApi.fasi + "PageChange?pageId=" + pageId + "&newTitle=" + encodeURIComponent($('#menuItemName').val()) + "&languageId=" + localStorage.getItem('languageId') + "&layoutType=" + layoutType + '&order=' + $('#menuOrder').val(), null,
                function (data) {
                    if (data !== undefined && data !== null && data.Successfully) {
                        window.location.replace(constants.defaultPage + '?pageId=' + pageId);
                    }
                });
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);
        event.preventDefault();
    };

    // Elimina una página
    this.btnDeleteMenuItemClick = function (event, pageId) {
        ajaxJsonHelper.delete(constants.fasiApi.fasi + 'PageDelete?pageId=' + pageId, null,
            function (data) {
                if (data !== undefined && data !== null && data.Successfully) {
                    window.location.replace(constants.defaultPage);
                }
            });
        event.preventDefault();
    };

    // Configuración del jquery.validate
    this.validateSetup = function () {
        var requiredMesage = dict.RequiredField[localStorage.getItem('languageName')];

        $("#menuItemMainForm").validate({
            rules: {
                PageName: { required: true },
                Columns: { required: true },
                MenuOrder: { required: true }
            },
            messages: {
                PageName: { required: requiredMesage },
                Columns: { required: requiredMesage },
                MenuOrder: { required: requiredMesage }
            }
        });
    };

    // Inicializa los tooltips con el texto traducido
    this.initializeToolTips = function () {
        $('#languageIcon').prop('title', dict.Language[localStorage.getItem('languageName')]);
        $('#helpIcon').prop('title', dict.Help[localStorage.getItem('languageName')]);
        //$('#alertsIcon').prop('title', dict.Alerts[localStorage.getItem('languageName')]);

        $('[data-toggle="tooltip"]').tooltip();

        // Carga la versión del archivo upgrade
        $.ajax({
            type: "GET",
            url: '/upgrade.inf',
            contentType: "text/html; charset=utf-8",
            success: function (data) {
                $('[data-trn-key="Copyright"]').prop('title', data);
                $('[data-trn-key="Copyright"]').tooltip();
            }
        });
    };

    // Define el título de la página
    this.setPageTitle = function (pageTitle) {
        var headerPageTitle = $('#pageTitle');
        headerPageTitle.html(pageTitle);
        headerPageTitle[0].parentNode.parentNode.style.display = '';
        $('.navbar')[0].classList.remove('white-bg');
    };

    // Se inicializa la busca por transacciones
    this.initializeTransactionSearch = function () {
        if (!masterSupport.user.isAnonymous) {
            $('#top-search').select2({
                placeholder: dict.SearchTransaction[localStorage.getItem('languageName')] + '...',
                language: localStorage.getItem('languageName'),
                maximumInputLength: 20,
                minimumInputLength: 3,
                ajax: {
                    type: "GET",
                    url: constants.fasiApi.backoffice + 'GetTransaction',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    delay: 250,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
                    },
                    data: function (params) {
                        // Se formatan los datos que se envía por parámetro
                        var query = {
                            userCode: params.term ? params.term : '',
                            userSchema: masterSupport.user.schemeCode,
                            transactionCode: ''
                        }
                        return query;
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.status == 401)
                            notification.swal.infoCallback(dict.ExpiredSession[localStorage.getItem('languageName')], dict.ClickToRefreshPage[localStorage.getItem('languageName')], function () { window.location.reload(true); });
                        else generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
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
            }).on('change.select2', function (e) {
                if ($('#top-search').val() != null) {
                    masterSupport.openSelectedTransaction();
                    $("#top-search").val(null);
                    $('#top-search').trigger('change');
                }
            });
        }
    };

    // Abre la transacción seleccionada
    this.openSelectedTransaction = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.backoffice + 'MakeURLIfAllowed',
            {
                windowLogicalCode: $('#top-search').val(),
                schemaCode: masterSupport.user.schemeCode,
                companyId: masterSupport.user.companyId
            },
            function (response) {
                $.LoadingOverlay("hide");

                if (response.Successfully) {
                    var lstrURL = response.Data.Url.substr(response.Data.Url.indexOf('sCodispl=') + 9);
                    var lintLength = lstrURL.indexOf('&');
                    var lstrCodispl = lstrURL.substr(0, lintLength);
                    var win = open(window.location.protocol + '//' + window.location.host + response.Data.Url, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                    if (win != null) {
                        win.moveTo(0, 0);
                        win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                    }
                }
                else if (response.Reason.length > 0)
                    notification.swal.error('', response.Reason);
            });
    };

    this.NotificationClean = function (type, subType) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.notifications + 'NotificationsUpdate',
            JSON.stringify({
                Type: type,
                SubType: subType
            }),
            function (response) {
                $.LoadingOverlay("hide");
                if (response.Successfully) {
                    masterSupport.makeMessageMenu();
                }
            });
    };

    this.refreshMenu = function () {
        masterSupport.makeMessageMenu();
        //Timer
        try {
            $.periodic({ period: 30000, decay: 1.2, max_period: 60000 }, function () {
                masterSupport.makeMessageMenu();
            });
        } catch (e) {
            console.log(e);
        }
    };

    // Monta el menu de mensajes
    this.makeMessageMenu = function () {
        if (!masterSupport.user.isAnonymous) {
            $('#divNotifications').show();
            var messageContainer = $('#NotificationMessageContainer');
            $('#NotificationMessageContainer li').remove();
            ajaxJsonHelper.get(constants.fasiApi.notifications + 'GetNotifications', null,
                function (response) {
                    if (response && response.Count > 0) {
                        var count = parseInt($('#alertsCount').html(), 0);
                        if (isNaN(count)) {
                            count = 0;
                        }
                        if (count !== parseInt(response.Count)) {
                            $('#alertsCount').addClass('animated rubberBand');
                            window.setTimeout(function () {
                                $('#alertsCount').removeClass('animated rubberBand');
                            }, 3000);
                        }
                        $('#OptionCountInfo').removeClass('disabled');
                        $('#alertsIcon').css({ 'color': 'red' });
                        $('#alertsCount').show();
                    } else {
                        $('#OptionCountInfo').addClass('disabled');
                        $('#alertsCount').addClass('animated rubberBand');
                        $('#alertsIcon').css({ 'color': 'grey' });
                        $('#alertsCount').hide();
                    }

                    $('#alertsCount').html(response.Count);

                    $.each(response.Items, function (index, item) {
                        if (index > 0)
                            messageContainer.append('<li class="divider"></li>');

                        // Notificación
                        var html = '<li>' +
                            '<a href="{url}">' +
                            '<div>' +
                            '<i class="fa {icon}"></i> {message}' +
                            '<span class="pull-right text-muted small">{datetime}</span>' +
                            '</div>' +
                            '</a>' +
                            '</li>'

                        var lastUpdateOn = moment(new Date(item.LastUpdateOn));
                        var CurrentDate = moment();

                        var days = CurrentDate.diff(lastUpdateOn, 'days', false)

                        var messageDay = days + " " + dict.Day[localStorage.getItem('languageName')] + (days <= 1 ? "" : 's');

                        html = html.replace('{url}', item.Url ? (item.Url !== '@clear@' ? item.Url : 'javascript:masterSupport.NotificationClean(' + item.Type + ',' + item.SubType + ')') : '#');
                        html = html.replace('{icon}', item.Icon);
                        html = html.replace('{message}', item.Count && item.Count >= 0 ? item.Message.replace('{count}', item.Count) : item.Message);
                        html = html.replace('{datetime}', messageDay);

                        messageContainer.append(html);
                    });

                    // Agrega el último articulo con el link de ver todas las notificaciones
                    //if (response && response.Count > 0) {
                    //    messageContainer.append('<li class="divider"></li>' +
                    //        '<li>' +
                    //        '<div class="text-center link-block">' +
                    //        '<a href="#">' +
                    //        '<strong>See All Alerts</strong>' +
                    //        '<i class="fa fa-angle-right"></i>' +
                    //        '</a>' +
                    //        '</div>' +
                    //        '</li>');
                    //}
                });
        }
    };
};

$(document).ready(function () {
    masterSupport.getUser();
    if (masterSupport.user.token !== "") {
        var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
        if (InMotionGITToken !== null) {
            if (!securitySupport.UserCheckEquals(InMotionGITToken, masterSupport.user.userId)) {
                securitySupport.Logout(masterSupport.user.userId, false);
                securitySupport.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
                masterSupport.getUser();
            }
        }
        // masterSupport.getMenu();
        // masterSupport.controlActions();
        // masterSupport.loadLanguage();
        // masterSupport.validateSetup();
        // masterSupport.initializeToolTips();
        // masterSupport.initializeTransactionSearch();
        // masterSupport.refreshMenu();
    } else {
        notification.swal.error(dict.STSFailureTitle[localStorage.getItem('languageName')],
            dict.STSFailureMessage[localStorage.getItem('languageName')]);
    }
});
var dict = {
    NewNotification: {
        en: "Have a new notification",
        es: "Tiene una nueva notificación",
        pt: "Tenha uma nova notificação"
    },
    Action: {
        en: "Action",
        es: "Acción",
        pt: "Ação"
    },
    Actions: {
        en: "Actions",
        es: "Acciones"
    },
    ActionTitle: {
        en: "Action title",
        es: "Título de la acción",
        pt: "Título da ação"
    },
    Add: {
        en: "Add",
        es: "Agregar",
        pt: "Adicionar"
    },
    AddTaskSuccess: {
        en: "Task has been added successfully!",
        es: "¡Tarea agregada con éxito!",
        pt: "Tarefa adicionada com sucesso!"
    },
    AddWidgets: {
        en: "Add widgets",
        es: "Agregar widgets",
        pt: "Adicionar widgets"
    },
    AddWidgetsTitle: {
        en: "Click on any of the item to add it to your page:",
        es: "Haga click en cualquiera de los ítems para agregarlo a la página:",
        pt: "Clique em qualquer um dos itens para adicionar na página:"
    },
    AddWidgetSuccess: {
        en: "Widget has been added successfully!",
        es: "¡Widget agregado con éxito!",
        pt: "Widget adicionado com sucesso!"
    },
    Alerts: {
        en: "Alerts",
        es: "Alertas",
        pt: "Alertas"
    },
    AllDayActivity: {
        en: "Activate all day",
        es: "Activar todo el dia",
        pt: "Ativar o dia todo"
    },
    AreYouSure: {
        en: "Are you sure?",
        es: "¿Estás seguro?",
        pt: "Você tem certeza?"
    },
    AssignedTo: {
        en: "Assigned to",
        es: "Asignado a",
        pt: "Atribuído a"
    },
    AssignSelectedTasks: {
        en: "Assign selected tasks",
        es: "Asignar tareas seleccionadas",
        pt: "Atribuir tarefas selecionadas"
    },
    AssignSelectedTasksTo: {
        en: "Assign selected tasks to",
        es: "Asignar tareas seleccionadas a",
        pt: "Atribuir tarefas selecionadas a"
    },
    Assignment: {
        en: "Assignment",
        es: "Asignación",
        pt: "Atribuição"
    },
    Available: {
        en: "Available",
        es: "Disponible",
        pt: "Disponível"
    },
    BestTimeToCall: {
        en: "Best time to call",
        es: "Mejor momento para llamar"
    },
    Cancel: {
        en: "Cancel",
        es: "Cancelar",
        pt: "Cancelar"
    },
    CancelNo: {
        en: "Do not",
        es: "No",
        pt: "Não"
    },
    Canceled: {
        en: "Canceled",
        es: "Cancelada",
        pt: "Cancelada"
    },
    DeleteRowConfirmation: {
        en: "Are you sure you want to delete the selected records?",
        es: "¿Está usted seguro de querer eliminar los registros seleccionados?",
        pt: "Tem certeza de que deseja excluir os registros selecionados?"
    },
    ChangeColumns: {
        en: "Reset widgets by columns",
        es: "Restablecer los widgets por columnas",
        pt: "Reajustar widgets por colunas"
    },
    ClickToRefreshPage: {
        en: "Click on 'OK' to refresh the page.",
        es: "Haz clic en 'OK' para refrescar la página.",
        pt: "Clique em 'OK' para atualizar a página."
    },
    Close: {
        en: "Close",
        es: "Cerrar",
        pt: "Fechar"
    },
    Code: {
        en: "Code",
        es: "Código",
        pt: "Código"
    },
    Columns: {
        en: "Columns",
        es: "Columnas",
        pt: "Colunas"
    },
    Completed: {
        en: "Completed",
        es: "Completado",
        pt: "Concluído"
    },
    Conditions: {
        en: "Conditions",
        es: "Condiciones",
        pt: "Condições"
    },
    Configuration: {
        en: "Configuration",
        es: "Configuración",
        pt: "Configuração"
    },
    ContinueAnyway: {
        en: "Do you want to continue anyway?",
        es: "¿Deseas continuar de todos modos?",
        pt: "Deseja continuar assim mesmo?"
    },
    Copyright: {
        en: "@2018 InMotion GIT, Inc. All rights reserved",
        es: "@2018 InMotion GIT. Todos los derechos reservados",
        pt: "@2018 InMotion GIT. Todos os direitos reservados"
    },
    Created: {
        en: "Created",
        es: "Creado",
        pt: "Creado"
    },
    DateAndTime: {
        en: "Date and time",
        es: "Fecha y hora",
        pt: "Data e hora"
    },
    Day: {
        en: "Day",
        es: "Día",
        pt: "Dia"
    },
    Deferred: {
        en: "Deferred",
        es: "Propuesta",
        pt: "Proposta"
    },
    Delete: {
        en: "Delete",
        es: "Eliminar",
        pt: "Excluir"
    },
    DeleteYes: {
        en: "Yes",
        es: "Si",
        pt: "Sim"
    },
    Deleted: {
        en: "Deleted",
        es: "Eliminado",
        pt: "Excluído"
    },
    DeletePage: {
        en: "Delete page",
        es: "Borrar página",
        pt: "Excluir página"
    },
    DeleteTask: {
        en: "Delete task",
        es: "Eliminar la tarea",
        pt: "Excluir a tarefa"
    },
    DeleteTaskSuccess: {
        en: "Task has been deleted successfully!",
        es: "¡Tarea eliminada con éxito!",
        pt: "Tarefa excluída com sucesso!"
    },
    DeleteWidgetSuccess: {
        en: "Widget has been deleted successfully!",
        es: "¡Widget eliminado con éxito!",
        pt: "Widget removido com sucesso!"
    },
    DragAndDropColumnsTitle: {
        en: "Drag the columns you want to select ",
        es: "Arrastre las columnas que deseas seleccionar ",
        pt: "Arraste as colunas que deseja selecionar "
    },
    DragAndDropColumnsDescription: {
        en: "(the list of selected columns on the right are those that will be visible in the query).",
        es: "(la lista de columnas seleccionas a la derecha son las que estaran visibles en la consulta).",
        pt: "(a lista de colunas selecionadas à direita são aquelas que serão visíveis na consulta)."
    },
    Edit: {
        en: "Edit",
        es: "Editar"
    },
    EnableSemaphore: {
        en: "Enable semaphore",
        es: "Habilitar semáforo",
        pt: "Habilitar semáforo"
    },
    EndingTime: {
        en: "Ending time",
        es: "Hora de fin",
        pt: "Hora de término"
    },
    English: {
        en: "English",
        es: "Inglés",
        pt: "Inglês"
    },
    ExecuteTransaction: {
        en: "Execute transaction",
        es: "Ejecutar transacción",
        pt: "Executar transação"
    },
    Expiration: {
        en: "Expiration",
        es: "Vencimiento",
        pt: "Vencimento"
    },
    ExpiredSession: {
        en: "The session has expired.",
        es: "La sesión ha expirado.",
        pt: "A sessão expirou."
    },
    Extension: {
        en: "Ext.",
        es: "Ext."
    },
    ExtensionOne: {
        en: "Ext. 1",
        es: "Ext. 1"
    },
    ExtensionTwo: {
        en: "Ext. 2",
        es: "Ext. 2"
    },
    Field: {
        en: "Field",
        es: "Campo",
        pt: "Campo"
    },
    From: {
        en: "From",
        es: "Desde",
        pt: "De"
    },
    LessThenOrEqualTo100: {
        en: "Enter a value less than or equal to 100.",
        es: "Ingrese un valor menor o igual a 100.",
        pt: "Insira um valor menor ou igual a 100."
    },
    Help: {
        en: "Help",
        es: "Ayuda",
        pt: "Ajuda"
    },
    Home: {
        en: "Home",
        es: "Inicio",
        pt: "Início"
    },
    Hour: {
        en: "Hour",
        es: "Hora",
        pt: "Hora"
    },
    InactivateUser: {
        en: "Inactivate user",
        es: "Inactivar usuario",
        pt: "Inativar usuário"
    },
    InactivateUserSuccess: {
        en: "User inactivated successfully!",
        es: "¡Usuario inactivado con éxito!",
        pt: "Usuário inativado com sucesso!"
    },
    InactiveUserIndicator: {
        en: "The user is already inactive in the period or in part of it!",
        es: "¡El usuario ya está inactivo en el período o en parte de él!",
        pt: "O usuário já está inativado no período ou em parte dele!"
    },
    IndividualTaskIndicator: {
        en: "The task can only be performed by one person",
        es: "La tarea sólo puede ser realizada por una persona",
        pt: "A tarefa pode ser realizada somente por uma pessoa"
    },
    Language: {
        en: "Language",
        es: "Idioma",
        pt: "Idioma"
    },
    LineOfBusiness: {
        en: "Line of business",
        es: "Línea de negocio",
        pt: "Linha de negócio"
    },
    Location: {
        en: "Location",
        es: "Ubicación",
        pt: "Localização"
    },
    LogIn: {
        en: "Log In",
        es: "Conectar",
        pt: "Entrar"
    },
    LogOut: {
        en: "Log Out",
        es: "Desconectar",
        pt: "Sair"
    },    
    MenuItemOrder: {
        en: "Page order",
        es: "Orden de la página",
        pt: "Ordem da página"
    },
    MenuItemTitle: {
        en: "Page title",
        es: "Título de la página",
        pt: "Título da página"
    },
    Minute: {
        en: "Minute",
        es: "Minuto",
        pt: "Minuto"
    },
    NewPage: {
        en: "New page",
        es: "Nueva página",
        pt: "Nova página"
    },
    NewTask: {
        en: "New task",
        es: "Nueva tarea",
        pt: "Nova tarefa"
    },
    No: {
        en: "No",
        es: "No",
        pt: "Não"
    },
    NoDataFound: {
        en: "No data found",
        es: "No se han encontrado registros"
    },
    NotAuthorized: {
        en: "Your user is not authorized to access this operation.",
        es: "Su usuario no está autorizado a acceder a esta operación.",
        pt: "Seu usuário não está autorizado a acessar essa operação."
    },
    NotInitiated: {
        en: "Not initiated",
        es: "No iniciada",
        pt: "Não iniciada"
    },
    NumberDays: {
        en: "Number of days without performing the task",
        es: "Cantidad de días sin atender la tarea",
        pt: "Quantidade de dias sen atender a tarefa"
    },
    Operator: {
        en: "Operator",
        es: "Operador",
        pt: "Operador"
    },
    Operation:{
        en: "Operation",
        es: "Operación",
        pt: "Operação"
    },
    Pending: {
        en: "Pending",
        es: "Pendiente",
        pt: "Pendente"
    },
    PhoneNumber: {
        en: "Phone n°",
        es: "N° telefónico"
    },
    PhoneNumberRequired: {
        en: "Phone number required",
        es: "Número telefónico requerido"
    },
    Phones: {
        en: "Phones",
        es: "Teléfonos"
    },
    Portuguese: {
        en: "Portuguese",
        es: "Portugués",
        pt: "Português"
    },
    Priority: {
        en: "Priority",
        es: "Prioridad",
        pt: "Prioridade"
    },
    PrivacyPolicy: {
        en: "Privacy Policy",
        es: "Política de privacidad",
        pt: "Política de Privacidade"
    },
    Profile: {
        en: "Profile",
        es: "Perfil",
        pt: "Perfil"
    },
    PasswordChange: {
        en: "Password Change",
        es: "Cambio de contraseña",
        pt: "Mudança de senha"
    },
    RecordOwnerRequired: {
        en: "Record owner is required",
        es: "Propietario es requerido"
    },
    RecordOwnerValid: {
        en: "Record owner is invalid",
        es: "Propietario no es válido"
    },
    Reminder: {
        en: "Reminder",
        es: "Recordatorio",
        pt: "Lembrete"
    },
    RequiredField: {
        en: "The field is required.",
        es: "El campo es requerido.",
        pt: "O campo é requerido."
    },
    ResourceNotFound: {
        en: "Resource not found.",
        es: "Recurso no encontrado.",
        pt: "Recurso não encontrado."
    },
    ResourceNotFoundDetail: {
        en: "Sorry, but the resource you are looking for has not been found.",
        es: "Lo sentimos, pero el recurso que está buscando no se ha encontrado.",
        pt: "Desculpe, mas o recurso que está procurando não foi encontrado."
    },
    SavePage: {
        en: "Save",
        es: "Guardar",
        pt: "Salvar"
    },
    Save: {
        en: "Save",
        es: "Guardar",
        pt: "Salvar"
    },
    SearchTransaction: {
        en: "Go to",
        es: "Ir a",
        pt: "Ir a"
    },
    Selected: {
        en: "Selected",
        es: "Seleccionado",
        pt: "Selecionado"
    },
    SelectedNodeConditions: {
        en: "(Conditions of the selected node)",
        es: "(Condiciones de la rama seleccionada)",
        pt: "(Condições do nó selecionado)"
    },
    Semaphore: {
        en: "Semaphore",
        es: "Semáforo",
        pt: "Semáforo"
    },
    SemaphoreConfig: {
        en: "Semaphore configuration",
        es: "Configuración del semáforo",
        pt: "Configuração do semáforo"
    },
    Spanish: {
        en: "Spanish",
        es: "Español",
        pt: "Espanhol"
    },
    StartingTime: {
        en: "Starting time",
        es: "Hora de inicio",
        pt: "Hora de início"
    },
    Status: {
        en: "Status",
        es: "Estado",
        pt: "Estado"
    },
    Subject: {
        en: "Subject",
        es: "Asunto",
        pt: "Assunto"
    },
    TaskMailboxes: {
        en: "Mailboxes",
        es: "Bandejas de tareas",
        pt: "Diretórios de tarefas"
    },
    TaskMailboxesConfig: {
        en: "Mailboxes configuration",
        es: "Configuración de bandejas de tareas",
        pt: "Configuração de diretórios de tarefas"
    },
    Tasks: {
        en: "Tasks",
        es: "Tareas",
        pt: "Tarefas"
    },
    TransactionNotAllowed: {
        en: "The transaction is not allowed regarding its scheme.",
        es: "La transacción no está permitida para su esquema.",
        pt: "A transação não está permitida para o seu esquema."
    },
    TaskActionTitleDescription: {
        en: "Describe an action",
        es: "Describe una acción",
        pt: "Descreve uma ação"
    },
    TaskAssignmentDescription: {
        en: "Date and time of task creation",
        es: "Fecha y hora de creación de la tarea",
        pt: "Data e hora da criação da tarefa"
    },
    TaskCompletedDescription: {
        en: "Indicates the percentage completed",
        es: "Indica la porcentaje completada",
        pt: "Indica o percentual completado"
    },
    TaskDetail: {
        en: "See task detail",
        es: "Ver detalle de la tarea",
        pt: "Ver detalhe da tarefa"
    },
    TaskExpirationDescription: {
        en: "Date and time of task expiration",
        es: "Fecha y hora de vencimiento de la tarea",
        pt: "Data e hora de vencimento da tarefa"
    },
    TaskHistory: {
        en: "See task history",
        es: "Ver historia de la tarea",
        pt: "Ver historico da tarefa"
    },
    TaskIdentifier: {
        en: "Task identifier",
        es: "Identificador de la tarea",
        pt: "Identificador da tarefa"
    },
    TaskLineOfBusinessDescription: {
        en: "Identify the line of business",
        es: "Identifica la línea de negócio",
        pt: "Identifica a linha de negócio"
    },
    TaskPriorityDescription: {
        en: "Indicates the task importance",
        es: "Indica la importancia de la tarea",
        pt: "Indica a importância da tarefa"
    },
    TaskStatus: {
        en: "Task status",
        es: "Estado de la tarea",
        pt: "Estado da tarefa"
    },
    TaskStatusDescription: {
        en: "Current task status",
        es: "Estado actual de la tarea",
        pt: "Estado atual da tarefa"
    },
    TaskSubjectDescription: {
        en: "Describe the task main subject",
        es: "Describe el asunto principal de la tarea",
        pt: "Descreve o assunto principal da tarefa"
    },
    TaskTypeDescription: {
        en: "Indicates the task type",
        es: "Indica el tipo de la tarea",
        pt: "Indica o tipo da tarefa"
    },
    TermsService: {
        en: "Terms of Service",
        es: "Términos de servicio",
        pt: "Termos de Serviço"
    },
    To: {
        en: "To",
        es: "Hasta",
        pt: "Até"
    },
    Type: {
        en: "Type",
        es: "Tipo",
        pt: "Tipo"
    },
    TypeOfPhone: {
        en: "Type of phone",
        es: "Tipo de teléfono"
    },
    UnexpectedError: {
        en: "Something unexpected has occurred.",
        es: "Ha ocurrido algo inesperado.",
        pt: "Ocorreu algo inesperado."
    },
    UnexpectedErrorDetail: {
        en: "The server encountered something unexpected that didn't allow it to complete the request. We apologize.",
        es: "El servidor encontró algo inesperado que no le permitió completar la solicitud. Pedimos disculpas.",
        pt: "O servidor encontrou algo inesperado que não permitiu completar a solicitação. Pedimos desculpas."
    },
    Update: {
        en: "Update",
        es: "Actualizar",
        pt: "Editar"
    },
    Updated: {
        en: "Updated",
        es: "Actualizado",
        pt: "Editado"
    },
    UpdateTaskStatus: {
        en: "Change task status",
        es: "Cambiar estado de la tarea",
        pt: "Alterar status da tarefa"
    },
    UpdateTaskSuccess: {
        en: "Task has been updated successfully!",
        es: "¡Tarea actualizada con éxito!",
        pt: "Tarefa alterada com sucesso!"
    },
    User: {
        en: "User",
        es: "Usuario",
        pt: "Usuário"
    },
    Value: {
        en: "Value",
        es: "Valor",
        pt: "Valor"
    },
    Yes: {
        en: "Yes",
        es: "Sí",
        pt: "Sim"
    },
    WarningWhenCompleted: {
        en: "Send message when the task is completed",
        es: "Mandar mensaje cuando la tarea sea completada",
        pt: "Enviar mensagem quando a tarefa estiver concluída"
    },
    Waiting: {
        en: "Waiting",
        es: "En espera",
        pt: "Em espera"
    },
    WaitingTime: {
        en: "Waiting time",
        es: "Tiempo de espera",
        pt: "Tempo de espera"
    },
    WaitingTimeDescription: {
        en: "Time since the task creation",
        es: "Tiempo transcurrido desde la creación de la tarea",
        pt: "Tempo decorrido desde a criacao da tarefa"
    },
    Week: {
        en: "Week",
        es: "Semana",
        pt: "Semana"
    },
    STSFailureMessage: {
        en: "The STS service is not available.",
        es: "No esta disponible el servicio de STS.",
        pt: "O serviço STS não está disponível."
    },
    STSFailureTitle: {
        en: "The STS service is not available.",
        es: "No esta disponible el servicio de STS.",
        pt: "O serviço STS não está disponível."
    },
    AutoLogInTitle: {
        en: "Start automatic session",
        es: "Inicio sesión automático",
        pt: "Iniciar sessão automática"
    },
    AutoLogInBody: {
        en: "It will load the current page with find to contextualize the user, it will be automatically recharged",
        es: "Se va cargar la pagina actual con find de contextualizar el usuario, se va recargara automaticamente",
        pt: "Ele irá carregar a página atual com find para contextualizar o usuário, ele será automaticamente recarregado"
    }
}

var translator = new function () {
    this.translatorObj = null;

    this.initialize = function (language) {
        translator.translatorObj = $('body').translate({ lang: language, t: dict });
    }

    this.changeLanguage = function (language) {
        translator.translatorObj.lang(language);
    }
};