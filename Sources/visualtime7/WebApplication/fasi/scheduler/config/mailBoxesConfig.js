var mailBoxesConfigSupport = new function () {

    this.selectedNodeOwnerId = 0;

    // Crea el arból MailBoxes
    this.createTree = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.diary + 'MailBoxes?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                $.LoadingOverlay("hide");

                var items = new Array();

                if (data && data.Successfully) {
                    items = mailBoxesConfigSupport.createTreeItems(data.Data.ChildFilters);

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
                            mailBoxesConfigSupport.selectedNodeOwnerId = dataNode.ownerId;
                            mailBoxesConfigSupport.loadMailBoxDetail(dataNode.id);
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
                treeNode.children = mailBoxesConfigSupport.createTreeItems(item.ChildFilters);
            }
            items.push(treeNode);
        });
        return items;
    };

    // Crea nuevo artículo en el árbol
    this.createNode = function () {
        if (mailBoxesConfigSupport.selectedNodeOwnerId == 0) {
            var treeView = $('#treeView').jstree(true);
            var selected = treeView.get_selected();

            if (!selected.length) return false;

            selected = selected[0];
            var selectedNode = treeView.get_node(selected);

            // Si es una rama hija de otra rama
            if (selectedNode.parents.length == 1) {
                $.LoadingOverlay("show");
                ajaxJsonHelper.post(constants.fasiApi.diary + 'CreateMailBox/' + selectedNode.data.id + '?languageId=' + localStorage.getItem('languageId'), null,
                    function (response) {
                        $.LoadingOverlay("hide");

                        selected = treeView.create_node(selected, {
                            data: {
                                id: response.Id,
                                ownerId: response.OwnerId,
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
        if (mailBoxesConfigSupport.selectedNodeOwnerId > 0) {
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
        if (mailBoxesConfigSupport.selectedNodeOwnerId > 0) {
            var treeView = $('#treeView').jstree(true);
            var selected = treeView.get_selected();

            if (!selected.length) return false;

            selected = selected[0];
            notification.swal.deleteConfirmation(null,
                function () {
                    ajaxJsonHelper.delete(constants.fasiApi.diary + 'DeleteMailBox/' + treeView.get_node(selected).data.id, null,
                        function (response) {
                            $.LoadingOverlay("hide");
                            // Se elimina de pantalla después que el servidor eliminar de la base de datos
                            treeView.delete_node(selected);
                            treeView.select_node('j1_1');
                        });
                });
        }
    };

    // Carga los detalles de una bandeja de tareas
    this.loadMailBoxDetail = function (id) {
        var treeView = $('#treeView').jstree(true);
        var selected = treeView.get_selected();

        if (mailBoxesConfigSupport.selectedNodeOwnerId == 0) {
            if (treeView.get_node(selected).parents.length == 1)
                $('#lnkCreate').removeProp('disabled').css('color', '');
            else
                $('#lnkCreate').prop('disabled', 'disabled').css('color', 'silver');

            $('#lnkUpdate').prop('disabled', 'disabled').css('color', 'silver');
            $('#lnkDelete').prop('disabled', 'disabled').css('color', 'silver');
        }
        else {
            $('#lnkCreate').prop('disabled', 'disabled').css('color', 'silver');
            $('#lnkUpdate').removeProp('disabled').css('color', '');
            $('#lnkDelete').removeProp('disabled').css('color', '');
        }

        ajaxJsonHelper.get(constants.fasiApi.diary + 'RetrieveMailBoxById/' + id + '?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                $('#title').html(data.Title);

                mailBoxesConfigSupport.createColumnsList(data.ColumnsToShow);
                mailBoxesConfigSupport.loadFilter(data);

                $('#semaphoreActive')[0].checked = data.SemaphoreActive;
                if (data.SemaphoreActive) {
                    mailBoxesConfigSupport.loadRangerSlider(data.Semaphore);
                    document.getElementById('sliderSelection').removeAttribute('disabled');
                }
                else {
                    mailBoxesConfigSupport.loadRangerSlider([1, 3]);
                    document.getElementById('sliderSelection').setAttribute('disabled', true);
                }
            });
    };

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
            //disabled: mailBoxesConfigSupport.selectedNodeOwnerId == 0
        }).disableSelection();

        selectedColumns.sortable({
            connectWith: ".connectList",
            //disabled: mailBoxesConfigSupport.selectedNodeOwnerId == 0,
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
        mailBoxesConfigSupport.selectedNodeOwnerId == 0 ? $('#formCondition').css('display', 'none') : $('#formCondition').css('display', '');
        mailBoxesConfigSupport.selectedNodeOwnerId == 0 ? $('#addCondition').css('display', 'none') : $('#addCondition').css('display', '');

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
                { field: 'FieldDescription', formatter: 'mailBoxesConfigSupport.fieldDescription', title: dict.Field[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'Field', visible: false },
                { field: 'Operator', visible: false },
                { field: 'OperatorDescription', formatter: 'mailBoxesConfigSupport.operatorFormatter', title: dict.Operator[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'ValueDescription', formatter: 'mailBoxesConfigSupport.valueDescription', title: dict.Value[generalSupport.LanguageName()], halign: 'center', align: 'center', width: '32%' },
                { field: 'Value', visible: false },
                { halign: 'center', align: 'center', switchable: false, formatter: 'mailBoxesConfigSupport.remove', width: '3,5%' }
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
        var href = mailBoxesConfigSupport.selectedNodeOwnerId == 0 ? 'javascript:void(0);' : 'javascript:mailBoxesConfigSupport.deleteCondition(' + index + ');';
        var colour = mailBoxesConfigSupport.selectedNodeOwnerId == 0 ? 'silver' : '#d8482f';
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
            mailBoxesConfigSupport.saveConditions();
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
        mailBoxesConfigSupport.saveConditions();
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

    // Habilita o inhabilita el semáforo
    this.enableSemaphoreClick = function (el) {
        mailBoxesConfigSupport.loadRangerSlider([1, 3]);
        if (el.checked)
            document.getElementById('sliderSelection').removeAttribute('disabled');
        else {
            document.getElementById('sliderSelection').setAttribute('disabled', true);
        }
        mailBoxesConfigSupport.updateSemaphore(el.checked, [1, 3]);
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

    // Configuración del slider con los rangos del semáforo
    this.loadRangerSlider = function (values) {
        var sliderSelection = document.getElementById('sliderSelection');

        if (sliderSelection.innerHTML == '') {
            noUiSlider.create(sliderSelection, {
                // Si no exist valor se pasa 1, 3 por defecto
                start: values ? values : [1, 3],
                // Minimo 1 y máximo 9
                padding: [1, 1],
                connect: [true, true, true],
                // de 1 en 1
                step: 1,
                // Desde 0 hasta 10
                range: {
                    'min': 0,
                    'max': 10
                },
                tooltips: [true, true],
                format: {
                    to: function (value) {
                        return value;
                    },
                    from: function (value) {
                        return value;
                    }
                }
            });

            // Configura los colores
            var connect = sliderSelection.querySelectorAll('.noUi-connect');
            var classes = ['green-bg', 'yellow-bg', 'red-bg'];

            for (var i = 0; i < connect.length; i++) {
                connect[i].classList.add(classes[i]);
            }

            sliderSelection.noUiSlider.on('change', function (values, handle) {
                // Se guarda después de cambiar
                mailBoxesConfigSupport.updateSemaphore(true, values);
            });
        }
        // Si ya exist el Slider entonces a penas se setea el valor
        else sliderSelection.noUiSlider.set(values);
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
            masterSupport.setPageTitle(dict.TaskMailboxesConfig[generalSupport.LanguageName()]);

        mailBoxesConfigSupport.loadLookUp('cbxStatus', 'taskstatus');
        mailBoxesConfigSupport.loadLookUp('cbxPriority', 'taskpriority');
        mailBoxesConfigSupport.loadLookUp('cbxOriginType', 'originType');
        mailBoxesConfigSupport.loadLookUpLineOfBusiness('cbxLOB');

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

        mailBoxesConfigSupport.loadTranslation();
        mailBoxesConfigSupport.validateConditionSetup();
        mailBoxesConfigSupport.createTree();
        mailBoxesConfigSupport.loadRangerSlider([1, 3]);
    };
};
$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: null,
        CallBack: mailBoxesConfigSupport.Init
    });
});
//$(function ($) {
//    var urlUnaunthorizedUser = '/fasi/dli/forms/UnauthorizedUser.aspx';
//    var IsAllow = false;
//    generalSupport.getUser();

//    if (!generalSupport.user.isAnonymous) {
//        $.ajax({
//            type: "GET",
//            url: constants.fasiApi.base + 'Members/v1/UserIsAllowDiary',
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            async: false,
//            data: JSON.stringify({}),
//            beforeSend: function (xhr) {
//                xhr.setRequestHeader("Authorization", "Bearer " + generalSupport.user.token);
//            },
//            success: function (data) {
//                if (data.Successfully === true) {
//                    if (data.Data === true) {
//                        IsAllow = true;
//                    }
//                }
//                else
//                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
//            },
//            error: function (qXHR, textStatus, errorThrown) {
//                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
//            }
//        });
//    }
//    if (!IsAllow) {
//        window.location = urlUnaunthorizedUser;
//    }
//});