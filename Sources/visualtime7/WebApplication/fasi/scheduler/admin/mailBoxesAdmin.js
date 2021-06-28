var mailBoxesAdminSupport = new function () {

    this.selectedNodeOwnerId = 0;
    this.isAdministrator = true;

    // Crea el arból MailBoxes
    this.createTree = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.diary + 'DefaultMailBoxes?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                $.LoadingOverlay("hide");

                var items = new Array();
                var tempItems = new Array();

                if (data && data.Successfully) {
                    items = mailBoxesAdminSupport.createTreeItems(data.Data.ChildFilters);
                    /*tempItems = mailBoxesAdminSupport.createTreeItems(data.Data.ChildFilters);
                    tempItems.forEach(function (item) {
                        if (item.data.ownerId == 0) {
                            var childs = new Array();
                            item.children.forEach(function (child) {
                                if (child.data.ownerId == 0)
                                    childs.push(child);
                            });
                            item.children = childs;
                            items.push(item);
                        }
                    });*/

                    $('#treeView').jstree({
                        "plugins": ["wholerow", "dnd"],
                        'core': {
                            'themes': { 'name': 'default' },
                            'check_callback': function (operation, node, node_parent, node_position, more) {
                                if (operation === "move_node") {
                                    // Verifica si una rama puede ser arrastrada
                                    return more.ref == null
                                        || (node_parent.parent != null
                                            && node_parent.parent !== node.parent
                                            && node.parents.length === 2
                                            && more.ref.parent === node.parent
                                            && more.ref.data.ownerId > 0
                                            && node.data.ownerId > 0);
                                }
                                return true;  //allow all other operations
                            },
                            'data': items
                        },
                        "dnd": {
                            "check_while_dragging": true,
                            "is_draggable": function (node) {
                                // Solo deja arrastrar las ramas creadas por el usuario
                                return node[0].data.ownerId > 0;
                            }
                        }
                    }).on("changed.jstree", function (e, data) {
                        // Al seleccionar una rama del árbol
                        if (data.selected.length) {
                            var dataNode = data.instance.get_node(data.selected[0]).data;
                            mailBoxesAdminSupport.selectedNodeOwnerId = dataNode.ownerId;
                            mailBoxesAdminSupport.loadMailBoxDetail(dataNode.id);
                        }
                    }).on("ready.jstree", function (e, data) {
                        // Selecciona la primera rama por defecto
                        data.instance.select_node('j1_1');
                    }).on("rename_node.jstree", function (e, data) {
                        $.LoadingOverlay("show");
                        ajaxJsonHelper.put(constants.fasiApi.diary + 'UpdateMailBox/' + data.node.data.id + '?languageId=' + localStorage.getItem('languageId'),
                            JSON.stringify({ key: 'Title', value: data.text }),
                            function (response) {
                                $.LoadingOverlay("hide");
                            });
                    }).on("move_node.jstree", function (e, data) {
                        // Al mover una rama
                        var nodesToUpdate = new Array();
                        var treeView = $('#treeView').jstree(true);
                        var parent = treeView.get_node(data.parent);
                        // Las pone en orden
                        $.each(parent.children, function (index, nodeKey) {
                            var node = treeView.get_node(nodeKey);
                            if (node.data.ownerId != 0)
                                nodesToUpdate.push({ key: node.data.id, value: index });
                        });
                        // Se envía a la base de datos
                        $.LoadingOverlay("show");
                        ajaxJsonHelper.put(constants.fasiApi.diary + 'UpdateMailBoxChildrenPosition/' + parent.data.id,
                            JSON.stringify(nodesToUpdate),
                            function (response) {
                                $.LoadingOverlay("hide");
                            });
                    });
                }
            });
    };

    // Crea los artículos del arból MailBoxes
    this.createTreeItems = function (data) {
        var items = new Array();
        $.each(data, function (index, item) {
            var treeNode = {
                data: {
                    id: item.Id,
                    ownerId: item.OwnerId,
                    sourceType: item.Type
                },
                text: item.Title,
                icon: 'fa fa-folder'
            };

            // Si un artículo contiene artículos hijos, entonces se llama el método de forma recursiva
            if (item.ChildFilters && item.ChildFilters.length > 0) {
                treeNode.state = { opened: true };
                treeNode.children = mailBoxesAdminSupport.createTreeItems(item.ChildFilters);
            }
            items.push(treeNode);
        });
        return items;
    };

    // Crea nuevo artículo en el árbol
    this.createNode = function () {
        if (mailBoxesAdminSupport.selectedNodeOwnerId == 0) {
            var treeView = $('#treeView').jstree(true);
            var selected = treeView.get_selected();

            if (!selected.length) return false;

            selected = selected[0];
            var selectedNode = treeView.get_node(selected);

            // Si es una rama hija de otra rama
            if (selectedNode.parents.length == 1) {
                $.LoadingOverlay("show");
                ajaxJsonHelper.post(constants.fasiApi.diary + 'CreateDefaultMailBox/' + selectedNode.data.id + '?languageId=' + localStorage.getItem('languageId'), null,
                    function (response) {
                        $.LoadingOverlay("hide");

                        selected = treeView.create_node(selected, {
                            data: {
                                id: response.Id,
                                ownerId: 0,
                                sourceType: response.Type
                            },
                            text: response.Title,
                            icon: 'fa fa-folder'
                        });
                        treeView.edit(selected);
                        $('.jstree-rename-input').attr('maxLength', 30);
                    });
            }
        }
    };

    // Cambia nombre de un artículo del árbol
    this.renameNode = function () {
        if (mailBoxesAdminSupport.selectedNodeOwnerId > 0 || mailBoxesAdminSupport.isAdministrator) {
            var treeView = $('#treeView').jstree(true);
            var selected = treeView.get_selected();

            if (!selected.length) return false;

            selected = selected[0];

            treeView.edit(selected);
            $('.jstree-rename-input').attr('maxLength', 30);
        }
    };

    // Elimina un artículo del árbol
    this.deleteNode = function () {
        //if (mailBoxesAdminSupport.selectedNodeOwnerId > 0) {
        nodeObject = mailBoxesAdminSupport.getNodeObject();
        var treeView = $('#treeView').jstree(true);

        if (nodeObject !== null && nodeObject.parents.length > 1) {
            notification.swal.deleteConfirmation(null,
                function () {
                    ajaxJsonHelper.delete(constants.fasiApi.diary + 'ForceMailBoxDeletion/' + nodeObject.data.id, null,
                        function (response) {
                            $.LoadingOverlay("hide");
                            // Se elimina de pantalla después que el servidor eliminar de la base de datos
                            treeView.delete_node(nodeObject.id);
                            treeView.select_node('j1_1');
                        });
                });
        }
        //}
    };

    // Carga los detalles de una bandeja de tareas
    this.loadMailBoxDetail = function (id) {
        
        nodeObject = mailBoxesAdminSupport.getNodeObject();
        if (nodeObject === null || nodeObject.parents.length == 1) {
            $('#lnkDelete').prop('disabled', 'disabled').css('color', 'silver');
            $('#lnkDelete').css('cursor', 'default');
        }
        else {
            $('#lnkDelete').removeProp('disabled').css('color', '');
            $('#lnkDelete').css('cursor', 'pointer');
        }
        /*if (mailBoxesAdminSupport.selectedNodeOwnerId == 0) {
            if (treeView.get_node(selected).parents.length == 1)
                $('#lnkCreate').removeProp('disabled').css('color', '');
            else
                $('#lnkCreate').prop('disabled', 'disabled').css('color', 'silver');

            $('#lnkUpdate').prop('disabled', 'disabled').css('color', 'silver');
            $('#lnkDelete').prop('disabled', 'disabled').css('color', 'silver');
        }
        else {*/
        //$('#lnkCreate').prop('disabled', 'disabled').css('color', 'silver');
        //$('#lnkUpdate').removeProp('disabled').css('color', '');
        //$('#lnkDelete').removeProp('disabled').css('color', '');
        //}

        ajaxJsonHelper.get(constants.fasiApi.diary + 'RetrieveMailBoxById/' + id + '?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                $('#title').html(data.Title);

                mailBoxesAdminSupport.createColumnsList(data.ColumnsToShow);
                mailBoxesAdminSupport.loadFilter(data);
            });
    };

    // Retorna el id del nodo seleccionado.
    this.getNodeObject = function ()
    {
        var treeView = $('#treeView').jstree(true);
        var selected = treeView.get_selected();

        if (!selected.length) return null;

        selected = selected[0];
        nodeObject = treeView.get_node(selected);

        return nodeObject;
    }

    // Llena las listas de columns (disponibles y seleccionadas)
    this.createColumnsList = function (columnsToShow) {
        var availableColumns = $('#availableColumns');
        var selectedColumns = $('#selectedColumns');
        availableColumns.html(null);
        selectedColumns.html(null);
        var htmlItem = '<li class="success-element" id="{id}">{title}<div class="agile-detail">{description}</div></li>'

        var priorityColumn = htmlItem.replace('{id}', 'priority').replace('{title}', dict.Priority[generalSupport.LanguageName()]).replace('{description}', dict.TaskPriorityDescription[generalSupport.LanguageName()]);
        var shortDescriptionColumn = htmlItem.replace('{id}', 'shortDescription').replace('{title}', dict.Subject[generalSupport.LanguageName()]).replace('{description}', dict.TaskSubjectDescription[generalSupport.LanguageName()]);
        var originTypeColumn = htmlItem.replace('{id}', 'originType').replace('{title}', dict.Type[generalSupport.LanguageName()]).replace('{description}', dict.TaskTypeDescription[generalSupport.LanguageName()]);
        var creationDateColumn = htmlItem.replace('{id}', 'creationDate').replace('{title}', dict.Assignment[generalSupport.LanguageName()]).replace('{description}', dict.TaskAssignmentDescription[generalSupport.LanguageName()]);
        var endingDateTimeColumn = htmlItem.replace('{id}', 'endingDateTime').replace('{title}', dict.Expiration[generalSupport.LanguageName()]).replace('{description}', dict.TaskExpirationDescription[generalSupport.LanguageName()]);
        var percentageCompletedColumn = htmlItem.replace('{id}', 'percentageCompleted').replace('{title}', '% ' + dict.Completed[generalSupport.LanguageName()]).replace('{description}', dict.TaskCompletedDescription[generalSupport.LanguageName()]);
        var statusColumn = htmlItem.replace('{id}', 'status').replace('{title}', dict.Status[generalSupport.LanguageName()]).replace('{description}', dict.TaskStatusDescription[generalSupport.LanguageName()]);
        //var actionTitleColumn = htmlItem.replace('{id}', 'actionTitle').replace('{title}', dict.ActionTitle[generalSupport.LanguageName()]).replace('{description}', dict.TaskActionTitleDescription[generalSupport.LanguageName()]);
        var lineOfBusinessColumn = htmlItem.replace('{id}', 'lineOfBusiness').replace('{title}', dict.LineOfBusiness[generalSupport.LanguageName()]).replace('{description}', dict.TaskLineOfBusinessDescription[generalSupport.LanguageName()]);
        var waitingTimeColumn = htmlItem.replace('{id}', 'waitingTime').replace('{title}', dict.WaitingTime[generalSupport.LanguageName()]).replace('{description}', dict.WaitingTimeDescription[generalSupport.LanguageName()]);

        $.each(columnsToShow, function (index, column) {
            if (column == 'priority') selectedColumns.append(priorityColumn);
            if (column == 'shortDescription') selectedColumns.append(shortDescriptionColumn);
            if (column == 'originType') selectedColumns.append(originTypeColumn);
            if (column == 'creationDate') selectedColumns.append(creationDateColumn);
            if (column == 'endingDateTime') selectedColumns.append(endingDateTimeColumn);
            if (column == 'percentageCompleted') selectedColumns.append(percentageCompletedColumn);
            if (column == 'status') selectedColumns.append(statusColumn);
            if (column == 'waitingTime') selectedColumns.append(waitingTimeColumn);
            //if (column == 'actionTitle') selectedColumns.append(actionTitleColumn);
            if (column == 'lineOfBusiness') selectedColumns.append(lineOfBusinessColumn);
        });

        if (columnsToShow.indexOf('priority') == -1) availableColumns.append(priorityColumn);
        if (columnsToShow.indexOf('shortDescription') == -1) availableColumns.append(shortDescriptionColumn);
        if (columnsToShow.indexOf('originType') == -1) availableColumns.append(originTypeColumn);
        if (columnsToShow.indexOf('creationDate') == -1) availableColumns.append(creationDateColumn);
        if (columnsToShow.indexOf('endingDateTime') == -1) availableColumns.append(endingDateTimeColumn);
        if (columnsToShow.indexOf('percentageCompleted') == -1) availableColumns.append(percentageCompletedColumn);
        if (columnsToShow.indexOf('status') == -1) availableColumns.append(statusColumn);
        if (columnsToShow.indexOf('waitingTime') == -1) availableColumns.append(waitingTimeColumn);
        //if (columnsToShow.indexOf('actionTitle') == -1) availableColumns.append(actionTitleColumn);
        if (columnsToShow.indexOf('lineOfBusiness') == -1) availableColumns.append(lineOfBusinessColumn);

        // Solo habilita se el owner no es zero
        availableColumns.sortable({
            connectWith: ".connectList",
            //disabled: mailBoxesAdminSupport.selectedNodeOwnerId == 0
        }).disableSelection();

        selectedColumns.sortable({
            connectWith: ".connectList",
            //disabled: mailBoxesAdminSupport.selectedNodeOwnerId == 0,
            update: function (event, ui) {
                var columnsList = $("#selectedColumns").sortable("toArray");

                var treeView = $('#treeView').jstree(true);
                var selected = treeView.get_selected();

                if (!selected.length) return false;
                selected = selected[0];

                $.LoadingOverlay("show");
                ajaxJsonHelper.put(constants.fasiApi.diary + 'UpdateMailBox/' + treeView.get_node(selected).data.id + '?languageId=' + localStorage.getItem('languageId'),
                    JSON.stringify({ key: 'ColumnsToShow', value: JSON.stringify(columnsList) }),
                    function (response) {
                        $.LoadingOverlay("hide");
                    });
            }
        }).disableSelection();
    };

    // Crea la tabla de filtros de la rama seleccionada
    this.loadFilter = function (mailBoxDetail) {
        //mailBoxesAdminSupport.selectedNodeOwnerId == 0 ? $('#formCondition').css('display', 'none') : $('#formCondition').css('display', '');
        //mailBoxesAdminSupport.selectedNodeOwnerId == 0 ? $('#addCondition').css('display', 'none') : $('#addCondition').css('display', '');

        $('#formCondition').css('display', '');
        $('#addCondition').css('display', '');

        $('#grdFilter').bootstrapTable('destroy');
        $('#grdFilter').bootstrapTable({
            toggle: 'table',
            search: false,
            pagination: false,
            smartDisplay: true,
            sidePagination: 'server',
            searchOnEnterKey: false,
            showColumns: false,
            showRefresh: false,
            locale: generalSupport.LanguageName() == 'es' ? 'es-CR' : 'en-US',
            columns: [
                { field: 'Id', visible: false },
                { field: 'FieldDescription', formatter: 'mailBoxesAdminSupport.fieldDescription', title: dict.Field[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'Field', visible: false },
                { field: 'Operator', visible: false },
                { field: 'OperatorDescription', formatter: 'mailBoxesAdminSupport.operatorFormatter', title: dict.Operator[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'ValueDescription', formatter: 'mailBoxesAdminSupport.valueDescription', title: dict.Value[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'Value', visible: false },
                { halign: 'center', align: 'center', switchable: false, formatter: 'mailBoxesAdminSupport.remove', width: '3,5%' }
            ],
            data: mailBoxDetail && mailBoxDetail.Filter ? mailBoxDetail.Filter.Conditions : new Array(),
            onPostBody: function (data) {
                $('[data-toggle="tooltip-grid"]').tooltip();
            }
        });
    };

    // Crea la columna con la descripción del campo seleccionado
    this.operatorFormatter = function (value, row, index) {
        return $('#cbxOperator option[value=' + row.Operator + ']').html();
    };

    // Crea la columna con la descripción del campo seleccionado
    this.fieldDescription = function (value, row, index) {
        row.Id = row.Field + row.Operator + row.Value;
        row.FieldDescription = $('select[name=field] option[value=' + row.Field + ']').html();
        return row.FieldDescription;
    };

    // Crea la columna con la descripción del valor seleccionado
    this.valueDescription = function (value, row, index) {
        if (row.Field == 'STATUS')
            row.ValueDescription = $('#cbxStatus option[value=' + row.Value + ']').html();
        else if (row.Field == 'PRIORITY')
            row.ValueDescription = $('#cbxPriority option[value=' + row.Value + ']').html();
        else if (row.Field == 'ORIGINTYPE')
            row.ValueDescription = $('#cbxOriginType option[value=' + row.Value + ']').html();
        else if (row.Field == 'LINEOFBUSINESS')
            row.ValueDescription = $('#cbxLOB option[value=' + row.Value + ']').html();

        return row.ValueDescription;
    };

    // Crea la columna con el ícono de eliminar
    this.remove = function (value, row, index) {
        var href = 'javascript:mailBoxesAdminSupport.deleteCondition(' + index + ');';
        var colour = '#d8482f';
        return '<a href="' + href + '" data-toggle="tooltip-grid" title="' + dict.Delete[generalSupport.LanguageName()] + '" style="color: ' + colour + ';"><i class="fa fa-trash"></i></a>';
    };

    // Crea una nueva condición
    this.createCondition = function (event) {
        var formInstance = $("#formCondition");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var newRow = {
                Field: $('select[name=field]').val(),
                Operator: $('select[name=operator]').val()
            };

            if (newRow.Field == 'STATUS')
                newRow.Value = $('#cbxStatus').val();
            else if (newRow.Field == 'PRIORITY')
                newRow.Value = $('#cbxPriority').val();
            else if (newRow.Field == 'ORIGINTYPE')
                newRow.Value = $('#cbxOriginType').val();
            else if (newRow.Field == 'LINEOFBUSINESS')
                newRow.Value = $('#cbxLOB').val();

            newRow.Id = newRow.Field + newRow.Operator + newRow.Value;

            // se verifica si ya no existe
            var exits = false;
            var rows = $('#grdFilter').bootstrapTable('getData');
            $.each(rows, function (index, row) {
                if (row.Id == newRow.Id) exits = true;
            });
            if (exits) return;

            $('#grdFilter').bootstrapTable('append', newRow);
            mailBoxesAdminSupport.saveConditions();
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);

        event.preventDefault();
    };

    // Elimina una condición
    this.deleteCondition = function (index) {
        var rows = $('#grdFilter').bootstrapTable('getData');
        var row = rows[index];

        $('#grdFilter').bootstrapTable('remove', { field: 'Id', values: [row.Id] });
        mailBoxesAdminSupport.saveConditions();
    };

    // Guarda los cambios en las condiciones en el servidor
    this.saveConditions = function () {
        var conditions = new Array();

        var rows = $('#grdFilter').bootstrapTable('getData');
        $.each(rows, function (index, row) {
            conditions.push({ Field: row.Field, Operator: row.Operator, Value: row.Value });
        });

        var treeView = $('#treeView').jstree(true);
        var selected = treeView.get_selected();

        if (!selected.length) return false;
        selected = selected[0];

        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.diary + 'UpdateMailBox/' + treeView.get_node(selected).data.id + '?languageId=' + localStorage.getItem('languageId'),
            JSON.stringify({ key: 'Condition', value: JSON.stringify({ Conditions: conditions }) }),
            function (response) {
                $.LoadingOverlay("hide");
            });
    };

    // Configuración de jquery.validate
    this.validateConditionSetup = function () {
        var requiredMesage = dict.RequiredField[generalSupport.LanguageName()];

        $("#formCondition").validate({
            rules: {
                field: { required: true },
                operator: { required: true },
                value: { required: true }
            },
            messages: {
                field: { required: requiredMesage },
                operator: { required: requiredMesage },
                value: { required: requiredMesage }
            }
        });
    };

    // Carga los lookups
    this.loadLookUp = function (key, parameter) {
        if ($('#' + key + ' option').length === 0) {
            ajaxJsonHelper.get(constants.fasiApi.diary + 'Lookups?key=' + parameter + '&languageId=' + localStorage.getItem('languageId'), null,
                function (response) {
                    if (response && response.Successfully) {
                        $.each(response.Data, function (i, item) {
                            $('#' + key).append($('<option></option>').val(item.Code).html(item.Description));
                        });
                        if (key == 'cbxStatus') $('#' + key).select2({});
                    }
                });
        }
    };

    // Carga la lista de lineas de negocio o ramos
    this.loadLookUpLineOfBusiness = function (key) {
        if ($('#' + key + ' option').length === 0) {
            ajaxJsonHelper.get(constants.fasiApi.backoffice + 'LineOfBusinessLkp', null,
                function (response) {
                    if (response && response.Successfully) {
                        $.each(response.Data, function (i, item) {
                            $('#' + key).append($('<option></option>').val(item.Code).html(item.Description));
                        });
                    }
                });
        }
    };

    // Se cambia el campo de valor con base en el field
    this.onChangeField = function (el) {
        if ($('#cbxStatus').hasClass("select2-hidden-accessible")) {
            $('#cbxStatus').select2('destroy');
            $('#cbxStatus').prop('disabled', 'disabled');
        }

        if ($('#cbxPriority').hasClass("select2-hidden-accessible")) {
            $('#cbxPriority').select2('destroy');
            $('#cbxPriority').prop('disabled', 'disabled');
        }

        if ($('#cbxOriginType').hasClass("select2-hidden-accessible")) {
            $('#cbxOriginType').select2('destroy');
            $('#cbxOriginType').prop('disabled', 'disabled');
        }

        if ($('#cbxLOB').hasClass("select2-hidden-accessible")) {
            $('#cbxLOB').select2('destroy');
            $('#cbxLOB').prop('disabled', 'disabled');
        }

        if (el.value == 'STATUS') {
            $('#cbxStatus').select2({});
            $('#cbxStatus').removeProp('disabled');
        }
        else if (el.value == 'PRIORITY') {
            $('#cbxPriority').select2({});
            $('#cbxPriority').removeProp('disabled');
        }
        else if (el.value == 'ORIGINTYPE') {
            $('#cbxOriginType').select2({});
            $('#cbxOriginType').removeProp('disabled');
        }
        else if (el.value == 'LINEOFBUSINESS') {
            $('#cbxLOB').select2({});
            $('#cbxLOB').removeProp('disabled');
        }
    };

    // Carga los datos de traducción dinámicos
    this.loadTranslation = function () {
        $('#addCondition').prop('title', dict.Add[generalSupport.LanguageName()]);
        $('#lnkCreate').prop('title', dict.Add[generalSupport.LanguageName()]);
        $('#lnkUpdate').prop('title', dict.Update[generalSupport.LanguageName()]);
        $('#lnkDelete').prop('title', dict.Delete[generalSupport.LanguageName()]);

        $('[data-toggle="tooltip-task"]').tooltip();

        $('#enableSemaphore').append(dict.EnableSemaphore[generalSupport.LanguageName()]);
        $('#enableSemaphore').append('&nbsp;<small>(' + dict.NumberDays[generalSupport.LanguageName()] + ')</small>');
    };

    // Actualiza el semáforo
    this.updateSemaphore = function (enabled, values) {
        var treeView = $('#treeView').jstree(true);
        var selected = treeView.get_selected();

        if (!selected.length) return false;

        selected = selected[0];
        var selectedNode = treeView.get_node(selected);

        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.diary + 'UpdateSemaphore/' + selectedNode.data.id,
            enabled ? JSON.stringify(values) : null, function (data) {
                $.LoadingOverlay("hide");
            });
    };

    //Establece el comportamiento de la página
    this.Init = function () {

        if (masterSupport && constants && window.location.pathname !== constants.defaultPage)
            masterSupport.setPageTitle(dict.TaskMailboxesConfigAdmin[generalSupport.LanguageName()]);

        mailBoxesAdminSupport.loadLookUp('cbxStatus', 'taskstatus');
        mailBoxesAdminSupport.loadLookUp('cbxPriority', 'taskpriority');
        mailBoxesAdminSupport.loadLookUp('cbxOriginType', 'originType');
        mailBoxesAdminSupport.loadLookUpLineOfBusiness('cbxLOB');

        $('#cbxField').select2({
            data: [
                { id: 'STATUS', text: dict.Status[generalSupport.LanguageName()] },
                { id: 'PRIORITY', text: dict.Priority[generalSupport.LanguageName()] },
                { id: 'ORIGINTYPE', text: dict.Type[generalSupport.LanguageName()] },
                { id: 'LINEOFBUSINESS', text: dict.LineOfBusiness[generalSupport.LanguageName()] }
            ]
        });
        $('#cbxOperator').select2({
            data: [
                { id: 'Equals', text: dict.OperatorEquals[generalSupport.LanguageName()] },
                { id: 'GreaterThan', text: dict.OperatorGreaterThan[generalSupport.LanguageName()] },
                { id: 'GreaterThanOrEqual', text: dict.OperatorGreaterThanOrEqual[generalSupport.LanguageName()] },
                { id: 'LessThan', text: dict.OperatorLessThan[generalSupport.LanguageName()] },
                { id: 'LessThanOrEqual', text: dict.OperatorLessThanOrEqual[generalSupport.LanguageName()] }
            ]
        });

        mailBoxesAdminSupport.loadTranslation();
        mailBoxesAdminSupport.validateConditionSetup();
        mailBoxesAdminSupport.createTree();
    };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: null,
        CallBack: mailBoxesAdminSupport.Init
    });
});