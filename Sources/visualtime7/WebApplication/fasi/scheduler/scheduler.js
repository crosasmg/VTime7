var schedulerSupport = new function () {
    // Crea el arból MailBoxes
    this.createTree = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.diary + 'MailBoxes?languageId=' + generalSupport.LanguageId(), null,
            function (data) {
                $.LoadingOverlay("hide");

                var items = new Array();

                if (data && data.Successfully) {
                    items = schedulerSupport.createTreeItems(data.Data.ChildFilters);

                    $('#treeView').jstree({
                        "plugins": ["wholerow"],
                        'core': {
                            'themes': { 'name': 'default' },
                            "check_callback": true,
                            'data': items
                        }
                    }).on("changed.jstree", function (e, data) {
                        // Evento de clic en una rama del árbol
                        if (data.selected.length) {
                            schedulerSupport.createGrid(data.instance.get_node(data.selected[0]).data);
                        }
                    }).on("ready.jstree", function (e, data) {
                        // Selecciona la primera rama
                        data.instance.select_node('j1_1');
                    });
                }
            });
    };

    // Crea los artículos del arból MailBoxes
    this.createTreeItems = function (data) {
        var items = new Array();
        $.each(data, function (index, item) {
            var treeNode = {
                data: { id: item.Id, sourceType: item.Type, columnsToShow: item.ColumnsToShow, semaphore: item.Semaphore },
                text: item.Title,
                icon: 'fa fa-folder'
            };

            // Si un artículo contiene artículos hijos, entonces se llama el método de forma recursiva
            if (item.ChildFilters && item.ChildFilters.length > 0) {
                treeNode.state = { opened: true };
                treeNode.children = schedulerSupport.createTreeItems(item.ChildFilters);
            }
            items.push(treeNode);
        });
        return items;
    };

    // Crea el grid con la lista de tareas
    this.createGrid = function (data) {
        $('#grdTable').bootstrapTable('destroy');
        $('#grdTable').bootstrapTable({
            toolbar: '#toolbar',
            sortName: 'endingDateTime',
            sortOrder: 'asc',
            search: true,
            pagination: true,
            smartDisplay: true,
            sidePagination: 'server',
            searchOnEnterKey: false,
            showColumns: false,
            showRefresh: true,
            pageSize: 10,
            pageList: [5, 10, 20, 50],
            locale: generalSupport.LanguageName() == 'es' ? 'es-CR' : 'en-US',
            columns: schedulerSupport.createGridColumnsConfig(data.columnsToShow),
            ajax: function (params) {
                var search = (params.data.search !== undefined) ? params.data.search : '';
                var sort = (params.data.sort !== undefined) ? params.data.sort : '';
                var order = (params.data.order !== undefined) ? params.data.order : '';
                var beginIndex = params.data.offset;
                var endIndex = params.data.limit;
                var urlRequest = constants.fasiApi.diary + 'Tasks?' +
                    'mailBoxId=' + data.id +
                    '&sourceType=' + data.sourceType +
                    '&beginIndex=' + beginIndex +
                    '&endIndex=' + endIndex +
                    '&search=' + search +
                    '&languageId=' + localStorage.getItem('languageId') +
                    '&filterId=-1' +
                    '&sort=' + sort +
                    '&order=' + order;
                $.LoadingOverlay("show");
                ajaxJsonHelper.get(urlRequest, null,
                    function (data) {
                        $.LoadingOverlay("hide");

                        params.success({
                            total: data != null ? data.Count : 0,
                            rows: data != null ? data.Items : []
                        });
                    });
            },
            onClickRow: function (row, $element, field) {
            }
        });

        $('#grdTable').on('check.bs.table uncheck.bs.table ' +
            'check-all.bs.table uncheck-all.bs.table', function () {
                if ($('#grdTable').bootstrapTable('getSelections').length > 0) {
                    $('#btnAssignMultiple').show();
                }
                else {
                    $('#btnAssignMultiple').hide();
                }
            });
    };

    // Crea el Array de columns que configura el grid
    this.createGridColumnsConfig = function (columnsToShow) {
        var columns = new Array();
        $.each(JSON.parse(columnsToShow), function (index, columnName) {
            if (columnName == 'priority')
                columns.push({ field: 'Priority', formatter: 'schedulerSupport.priority', sortable: true, order: "desc", title: dict.Priority[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'shortDescription')
                columns.push({ field: 'ShortDescription', title: dict.Subject[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'originType')
                columns.push({ field: 'OriginTypeDescription', title: dict.Type[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'creationDate')
                columns.push({ field: 'CreationDate', formatter: 'schedulerSupport.dateFormatter', sortable: true, order: "asc", title: dict.Assignment[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'endingDateTime')
                columns.push({ field: 'EndingDateTime', formatter: 'schedulerSupport.dateFormatter', sortable: true, order: "desc", title: dict.Expiration[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'percentageCompleted')
                columns.push({ field: 'PercentageCompleted', formatter: 'schedulerSupport.progress', title: '% ' + dict.Completed[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'status')
                columns.push({ field: 'Status', title: dict.Status[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            //else if (columnName == 'actionTitle')
            //    columns.push({ field: 'ActionTitle', title: dict.Action[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'lineOfBussiness')
                columns.push({ field: 'LineOfBussiness', title: dict.LineOfBussiness[generalSupport.LanguageName()], halign: 'center', align: 'center' });
            else if (columnName == 'waitingTime')
                columns.push({ field: 'WaitingTime', formatter: 'schedulerSupport.waitingTime', title: dict.WaitingTime[generalSupport.LanguageName()], halign: 'center', align: 'center' });
        });

        columns.push({ halign: 'center', align: 'center', switchable: false, formatter: 'schedulerSupport.taskDetail' });
        columns.push({ field: 'state', checkbox: true, halign: 'center', align: 'center', switchable: false });
        columns.push({ field: 'IconAction', switchable: false, formatter: 'schedulerSupport.iconAction', halign: 'center', align: 'center', title: dict.Action[generalSupport.LanguageName()] });

        return columns;
    };

    // Crea la columna con el menú de opciones
    this.iconAction = function (value, row, index) {
        indexId = row.TaskID.replace(/-/gi, "");
        return '<a data-toggle="tooltip" id="menu' + indexId + '" name="menu' + indexId + '" href="#" onclick="schedulerSupport.actions(event, \'' + indexId + '\','+index+'); return false" ><i class="fa fa-cogs" aria-hidden="true"></i></a>';
    };

    // Crea el menú de opciones al dar clic en el ícono
    this.actions = function (event, value, index) {
        var name = '#menu' + value;
        // Si el dropdown aún no exist en la página
        if (typeof $('body').find('#dropdown-' + value)[0] === 'undefined') {
            var allData = $('#grdTable').bootstrapTable('getData');
            var row = allData[index];
            currentRow = row;

            // Obtiene la rama seleccionada en el árbol
            var treeView = $('#treeView').jstree(true);
            var sourceType = treeView.get_node(treeView.get_selected(0)).data.sourceType;

            // Si exist transacción en la tarea
            if (row.VisualTimeTransaction && row.VisualTimeTransaction != '') {
                $.LoadingOverlay("show");
                ajaxJsonHelper.get('/fasi/scheduler/scheduler.aspx/GetTransactionLink?taskId=' + row.TaskID + '&visualTimeTransaction=' + row.VisualTimeTransaction + '&completedAction=' + row.CompletedAction + '&languageId=' + localStorage.getItem('languageId'), null,
                    function (response) {
                        $.LoadingOverlay("hide");
                        context.attach(name, { id: value, data: schedulerSupport.getActionsList(row, sourceType, response.d.Title, response.d.Url) });
                        $(name).trigger('click', [event.pageX, event.pageY]);
                    });
            }
            else {
                context.attach(name, { id: value, data: schedulerSupport.getActionsList(row, sourceType) });
                //Fix temporal, se debe revisar la asignacion a los eventos del click.
                setTimeout(function () {
                    $(name).trigger('click', [event.pageX, event.pageY]);
                }, 100);
            }
        }
        // Si el dropdown ya exist en la página
        else context.attach(name, { id: value, data: {} });
    };

    // Monta la lista de acciones con base en el registro
    this.getActionsList = function (row, sourceType, transactionName, url) {
        var actions = new Array();
        // Si hay transacción se agrega la opción de ejecutar
        if (transactionName && url && transactionName != '' && url != '')
            actions.push({
                icon: 'fa fa-arrow-circle-right',
                text: dict.ExecuteTransaction[generalSupport.LanguageName()] + ': ' + transactionName, action: function (e, selector) {
                    if (url.indexOf('/fasi/dli/') !== -1)
                        window.location.href = url;
                    else {
                        var win = open(url, 'Transaccion', 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                        if (win != null) {
                            win.moveTo(0, 0);
                            win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                        }
                    }
                }
            });

        // Si está en Mis tareas o en Tareas creadas por mi o en Tareas de mis supervisados
        if (sourceType == 1 || sourceType == 3 || sourceType == 4) {
            actions.push({
                icon: 'fa fa-refresh',
                text: dict.UpdateTaskStatus[generalSupport.LanguageName()], subMenu: [
                    {
                        icon: 'fa fa-cog',
                        text: dict.NotInitiated[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 1);
                        }
                    },
                    {
                        icon: 'fa fa-cog',
                        text: dict.Pending[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 2);
                        }
                    },
                    {
                        icon: 'fa fa-cog',
                        text: dict.Completed[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 3);
                        }
                    },
                    {
                        icon: 'fa fa-cog',
                        text: dict.Waiting[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 4);
                        }
                    },
                    {
                        icon: 'fa fa-cog',
                        text: dict.Deferred[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 5);
                        }
                    },
                    {
                        icon: 'fa fa-cog',
                        text: dict.Canceled[generalSupport.LanguageName()], action: function (e) {
                            taskSupport.updateTaskStatus(row.TaskID, 6);
                        }
                    },
                ]
            });
        }

        actions.push({ divider: true });
        actions.push({
            icon: 'fa fa-history',
            text: dict.TaskHistory[generalSupport.LanguageName()], action: function (e) {
                schedulerSupport.openTaskHistory(row.TaskID, row.ShortDescription)
            }
        });

        actions.push({
            icon: 'fa fa-history',
            text: dict.TaskDetail[generalSupport.LanguageName()], action: function (e) {
                taskSupport.getTaskById(row.TaskID);
                $('#taskModal').modal('show');
            }
        });

        // Si es una tarea manual y la tarea fue creada por el usuario conectado
        if (row.OriginType == 1 && row.CreatorUserCode == masterSupport.user.userId) {
            actions.push({
                icon: 'fa fa-trash',
                text: dict.DeleteTask[generalSupport.LanguageName()], action: function (e) {
                    taskSupport.removeTaskById(e, row.TaskID);
                }
            });
        }

        return actions;
    };

    // Crea la columna de prioridad poniendo una etiqueta de colores
    this.priority = function (value, row, index) {
        if (value === 0)
            value = '';

        if (row.Priority == '3') // Alta
            return '<span class="label label-danger">' + row.PriorityDescription + '</span>';

        if (row.Priority == '2') // Medio
            return '<span class="label label-warning">' + row.PriorityDescription + '</span>';

        if (row.Priority == '1') // Baja
            return '<span class="label label-info">' + row.PriorityDescription + '</span>';
    };

    // Formata la fecha
    this.dateFormatter = function (value, row, index) {
        if (!value || value == null || value === 0)
            return '-';
        return moment(value).utc().format(generalSupport.DateFormatWithHour());
    };

    // Crea la columna de progreso y convierte el valor percentual en una barra de progreso
    this.progress = function (value, row, index) {
        return '<div class="progress"> ' +
            '<div style="width: ' + value + '%" aria-valuemax="100" aria-valuemin="0" aria-valuenow="35" role="progressbar" class="progress-bar progress-bar-success"></div>' +
            '</div>';
    };

    // Crea la comumna con el tiempo de espera de una tarea
    this.waitingTime = function (value, row, index) {
        var treeView = $('#treeView').jstree(true);
        var semaphore = treeView.get_node(treeView.get_selected(0)).data.semaphore;

        var waitingTimeDescription;
        if (value <= 59) { // Minutos
            if (value == 1)
                waitingTimeDescription = value + ' ' + dict.Minute[generalSupport.LanguageName()];
            else
                waitingTimeDescription = value + ' ' + dict.Minute[generalSupport.LanguageName()] + 's';
        }
        else if (parseInt(value / 60) < 24) { // Horas
            value = parseInt(value / 60);

            if (value == 1)
                waitingTimeDescription = value + ' ' + dict.Hour[generalSupport.LanguageName()];
            else
                waitingTimeDescription = value + ' ' + dict.Hour[generalSupport.LanguageName()] + 's';
        }
        else {
            value = parseInt(value / 1440); // Días

            if (value == 1)
                waitingTimeDescription = value + ' ' + dict.Day[generalSupport.LanguageName()];
            else
                waitingTimeDescription = value + ' ' + dict.Day[generalSupport.LanguageName()] + 's';

            // Si exist semáforo configurado se pone los colores
            if (semaphore && semaphore != null) {
                if (value >= semaphore[1]) // Rojo
                    waitingTimeDescription = '<span class="label label-danger">' + waitingTimeDescription + '</span>';

                if (value >= semaphore[0] && value < semaphore[1]) // Amarillo
                    waitingTimeDescription = '<span class="label label-warning">' + waitingTimeDescription + '</span>';

                if (value < semaphore[0]) // Verde
                    waitingTimeDescription = '<span class="label label-info">' + waitingTimeDescription + '</span>';
            }
        }
        return waitingTimeDescription;
    };

    // Crea la columna con el checkbox de selección
    this.gridCheckBox = function (value, row, index) {
        var disabled = 'disabled="disabled"';

        var treeView = $('#treeView').jstree(true);
        var sourceType = treeView.get_node(treeView.get_selected(0)).data.sourceType;

        // Verifica si el usuario conectado puede asignar una tarea
        if ((row.IsOwner || sourceType == 2) && row.Status != 3)
            disabled = '';

        return '<div class="checkbox c-checkbox"><label><input type="checkbox" ' + disabled + ' data-index="' + index + '" name="btSelectItem" class="gridcheckbox" onchange="schedulerSupport.checkChanged();"/><span class="fa fa-check"></span></label></div>';
    };

    // Evento de los checkboxes de la grid
    this.checkChanged = function () {
        if ($('#grdTable').bootstrapTable('getSelections').length > 0) {
            $('#btnAssignMultiple').show();
        }
        else {
            $('#btnAssignMultiple').hide();
        }
    };

    // Crea la columna con el ícono de detalle de la tarea
    this.taskDetail = function (value, row, index) {
        return '<a href="#" onclick="taskSupport.getTaskById(\'' + row.TaskID + '\');" data-toggle="modal" data-target="#taskModal"><i class="fa fa-edit"></i></a>';
    };

    // Abre popup con las informaciones de histórico de la tarea
    this.openTaskHistory = function (taskId, description) {
        $('#modalHistory').modal('show');
        taskHistorySupport.loadHistory(taskId, description);
    };

    this.Init = function () {
        if (masterSupport && constants && window.location.pathname !== constants.defaultPage)
            masterSupport.setPageTitle(dict.Tasks[generalSupport.LanguageName()]);

        taskSupport.Init();

        $('#mailBoxConfig').prop('title', dict.TaskMailboxesConfig[generalSupport.LanguageName()]);
        $('[data-toggle="tooltip-task"]').tooltip();

        context.init({ preventDoubleContext: false, menuEvent: 'click' });
        schedulerSupport.createTree();

    };

};

$(function ($) {
    //var urlUnaunthorizedUser = '/fasi/dli/forms/UnauthorizedUser.aspx';
    //var IsAllow = false;
    //generalSupport.getUser();

    //if (!generalSupport.user.isAnonymous) {
    //    $.ajax({
    //        type: "GET",
    //        url: constants.fasiApi.base + 'Members/v1/UserIsAllowDiary',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        async: false,
    //        data: JSON.stringify({}),
    //        beforeSend: function (xhr) {
    //            xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
    //        },
    //        success: function (data) {
    //            if (data.Successfully === true) {
    //                if (data.Data === true) {
    //                    IsAllow = true;
    //                }
    //            }
    //            else
    //                generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    //        },
    //        error: function (qXHR, textStatus, errorThrown) {
    //            generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
    //        }
    //    });
    //}

    //if (!IsAllow) {
    //    window.location = urlUnaunthorizedUser;
    //}

});

$(document).ready(function () {

    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#NewAccountMainForm"),
        CallBack: schedulerSupport.Init
    });

});