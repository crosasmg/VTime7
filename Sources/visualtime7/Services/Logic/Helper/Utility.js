function RenderTable() {
    var urlBase = window.location.protocol + '//' + window.location.host + '/api/Helper';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            var items = new Array();

            $.each(data, function (i, item) {
                var url = window.location.host + '/Helper/index.aspx?Key=' + item.Key;
                var link = '<a target="_blank" href="http://' + url + '">Ir</a>';
                items.push({
                    "Id": item.Id,
                    "Key": item.Key,
                    "Summary": item.Summary,
                    "Go": link
                });
            });

            $('#table').bootstrapTable({
                columns: [{
                    field: 'Id',
                    title: 'Id',
                    visible: false
                }, {
                    field: 'Key',
                    align: 'center',
                    title: 'Key'
                }, {
                    field: 'Summary',
                    align: 'center',
                    title: 'Summary'
                }, {
                    field: 'Go',
                    align: 'center',
                    title: 'Ir',
                    type: 'html'
                }],
                data: items
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function SeveFile(key) {
    if (key !== undefined) {
        var urlBase = window.location.protocol + '//' + window.location.host + window.location.pathname + '/get';
        $.ajax({
            type: "POST",
            url: urlBase,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify({ Id: key }),
            success: function (data) {
                var rr = data;
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }
};

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');

    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
};