$(function () {
    $("#btnExecute").button({
        icons: {
            primary: 'ui-icon-eye'
        },
        label: 'Execute'
    })

    $("#btnExecute").click(function () {
        var query = $("#txtQuery").val();
        var isCheckBackOffice = $("#cbxBackOffice").is(':checked')

        var param = "{ query: " + "'" + query + "'," + " environment:'" + isCheckBackOffice + "' }";
        var urlBase = window.location.protocol + '//' + window.location.host + '/Support/uet.aspx/Execute';
        $.ajax({
            url: urlBase,
            data: param,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                var objdata = $.parseJSON(data.d);
                CreateTable(objdata);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(jQuery.parseJSON(XMLHttpRequest.responseText).Message);
            }
        });
    })

    function CreateTable(arrayData) {
      //  Create a HTML Table element.
                var table = $("<table />").addClass('table-hover');
                table[0].border = "1";
                var columnCount = arrayData.GENERIC[0].Columns.length;
                var generalLength = arrayData.GENERIC.length;
                //Add the header row.
                var row = $(table[0].insertRow(-1));
                for (var i = 0; i < columnCount; i++) {
                    var headerCell = $("<th />");
                    headerCell.html(arrayData.GENERIC[0].Columns[i]);
                    row.append(headerCell);
                }

                //Add the data rows.
                for (var i = 1; i < generalLength; i++) {
                    row = $(table[0].insertRow(-1));
                    for (var j = 0; j < columnCount; j++) {
                        var cell = $("<td />").addClass('bar').text(arrayData.GENERIC[i].Container[j]);
                        row.append(cell);
                    }
                }

                var dvTable = $("#dvTable");
                dvTable.html("");
                dvTable.append(table);

     
    }
});

function ShowControl() {
    var valueKey = $("#keyValid").val();
    if (valueKey == "True") {
        $("#btnExecute").removeAttr('disabled');
        $("#btnExecute").button("refresh");
    } else {
        $("#btnExecute").prop("disabled", true);
        $("#btnExecute").button("refresh");
    }
}