var taskHistorySupport = new function () {

    // Carga la grid con los hitoricos de la tarea
    this.loadHistory = function (id, description) {
        $('#modalHistoryLabel').html(description);

        $('#grdHistory').bootstrapTable('destroy');
        $('#grdHistory').bootstrapTable({           
            search: false,
            pagination: true,
            smartDisplay: true,
            sidePagination: 'server',
            searchOnEnterKey: false,
            showColumns: false,
            cache:false,
            showRefresh: false,
            pageSize: 5,
            pageList: [5, 10, 20],
            locale: generalSupport.LanguageName() == 'es' ? 'es-CR' : 'en-US',
            columns: [               
                { field: 'Operation', title: dict.Operation[generalSupport.LanguageName()], formatter: 'taskHistorySupport.operation', halign: 'center', align: 'center' },
                { field: 'EntryTime', title: dict.DateAndTime[generalSupport.LanguageName()], formatter: 'schedulerSupport.dateFormatter', halign: 'center', align: 'center' },
                { field: 'UpdateUserCode', title: dict.User[generalSupport.LanguageName()], halign: 'center', align: 'center' },
                { field: 'PercentageCompleted', title: '% ' + dict.Completed[generalSupport.LanguageName()], formatter: 'schedulerSupport.progress', halign: 'center', align: 'center' },
                { field: 'StatusDescription', title: dict.Status[generalSupport.LanguageName()], halign: 'center', align: 'center' }
            ],
            ajax: function (params) {                                                
                $.LoadingOverlay("show");
                ajaxJsonHelper.get(constants.fasiApi.diary + 'RetrieveTaskHistoryById/' + id + '?offset=' + params.data.offset + '&limit=' + params.data.limit + '&languageId=' + localStorage.getItem('languageId'), null,
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

    // Crea la columna de operation poniendo una etiqueta de colores
    this.operation = function (value, row, index) {
        if (value === 0)
            value = '';

        if (value == 'Deleted')
            return '<span class="label label-danger">' + dict.Deleted[generalSupport.LanguageName()] + '</span>';

        if (value == 'Updated')
            return '<span class="label label-warning">' + dict.Updated[generalSupport.LanguageName()] + '</span>';

        if (value == 'Created')
            return '<span class="label label-info">' + dict.Created[generalSupport.LanguageName()] + '</span>';
    };

    // Crea el detail de la grid
    this.detailFormatter = function (index, row) {
        var html = '';        
        html += '<p><b>' + dict.Location[generalSupport.LanguageName()] + ':</b> ' + (row.Location != null ? row.Location : '') + '</p>';
        html += '<p><b>' + dict.StartingTime[generalSupport.LanguageName()] + ':</b> ' + schedulerSupport.dateFormatter(row.StartingDateTime) + '</p>';
        html += '<p><b>' + dict.EndingTime[generalSupport.LanguageName()] + ':</b> ' + (row.EndingDateTime != null ? schedulerSupport.dateFormatter(row.EndingDateTime) : '') + '</p>';
        html += '<p><b>' + dict.Priority[generalSupport.LanguageName()] + ':</b> ' + schedulerSupport.priority(row.Priority, row) + '</p>';
        html += '<p><b>' + dict.AssignedTo[generalSupport.LanguageName()] + ':</b> ' + row.OwnersDescription + '</p>';
        html += '<p><b>' + dict.Reminder[generalSupport.LanguageName()] + '?</b> ' + taskHistorySupport.convertBoolToDescription(row.AlarmActive) + '</p>';
        html += '<p><b>' + dict.IndividualTaskIndicator[generalSupport.LanguageName()] + '?</b> ' + taskHistorySupport.convertBoolToDescription(row.IndividualTaskIndicator) + '</p>';
        html += '<p><b>' + dict.WarningWhenCompleted[generalSupport.LanguageName()] + '?</b> ' + taskHistorySupport.convertBoolToDescription(row.WarningWhenCompleted) + '</p>';
        html += '<p><b>' + dict.AllDayActivity[generalSupport.LanguageName()] + '?</b> ' + taskHistorySupport.convertBoolToDescription(row.AllDayActivity) + '</p>';
        html += '<p>' + row.TaskLongDescription + '</p>';
        return html;      
    };

    // Convierte un campo boolean a un texto
    this.convertBoolToDescription = function (value) {
        return value ? dict.Yes[generalSupport.LanguageName()] : dict.No[generalSupport.LanguageName()];
    };
};