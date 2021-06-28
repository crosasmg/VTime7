var itemsSelected = [];

$(document).ready(function () {
    var availableColumns = $('#availableColumns');
    var selectedColumns = $('#selectedColumns');

    var htmlItem = '<li class="success-element" id="{id}">{title}<div class="agile-detail">{description}</div></li>';

    var itemsAll = [];

    itemsAll.push({ "id": 1, "title": "Lunes", "description": "Primer día" });
    itemsAll.push({ "id": 2, "title": "Martes", "description": "Segundo día" });
    itemsAll.push({ "id": 3, "title": "Miércoles", "description": "Tercer día" });
    itemsAll.push({ "id": 4, "title": "Jueves", "description": "Cuarto día" });

    $.each(itemsAll, function (index, item) {
        availableColumns.append(htmlItem.replace("{id}", item.id).replace("{title}", item.title).replace("{description}", item.description));
    });

    itemsSelected.push({ "id": 1, "title": "Lunes", "description": "Primer día" });

    $.each(itemsSelected, function (index, item) {
        selectedColumns.append(htmlItem.replace("{id}", item.id).replace("{title}", item.title).replace("{description}", item.description));
    });
    availableColumns.sortable({
        connectWith: ".connectList",
        disabled: false
    }).disableSelection();

    selectedColumns.sortable({
        connectWith: ".connectList",
        disabled: false,
        update: function (event, ui) {
            var columnsList = $("#selectedColumns").sortable("toArray");
            var arrayNew = [];
            $.each(columnsList, function (index, itemIndex) {
                $.each(itemsAll, function (index, item) {
                    if (item.id.toString() === itemIndex) {
                        arrayNew.push(item);
                    }
                });
            });
            itemsSelected = arrayNew;
        }
    }).disableSelection();
});