var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/ViewLogs.aspx';
var isEnableValue;

$(function ($) {
    isEnableValue = IsEnable();

    //General

    $("#loading").dialog({
        hide: 'slide',
        show: 'slide',
        autoOpen: false
    });
}(jQuery));

$(document).ready(function () {
    if (isEnableValue == true) {
        //Viewer logs
        $('#Files').bootstrapTable({});

        FilesLogs();

        $("#btnQueryLog").click(function () {
            FilesLogs();
        });
    } else {
        $("#WrappperContent").hide();

        var template = '<div class="wrapper wrapper-content gray-bg text-center">' +
                        '	<div class="panel-body">' +
                        '		<div class="col-md-12">' +
                        '			<h2>El ViewLog está deshabilitada. Contacte su administrador</h2>' +
                        '		</div>' +
                        '	</div>' +
                        '</div>';

        $("#PageContent").append(template);
    }
});

//Log Files
function FilesLogs() {
    var urlAction = urlBase + "/FileLogs";
    $.ajax({
        url: urlAction,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loading").dialog('open').html("<p>Please Wait...</p>");
        },
        success: function (result) {
            $('#Files').bootstrapTable('removeAll');
            var Index = 0;
            if (result.d.length !== 0) {
                result.d.forEach(
                    function Formate(value) {
                        var d = new moment(value.LastWrite).format('YYYY/MM/DD HH:mm SSSS');
                        var row = {
                            Index: Index,
                            Name: value.Name,
                            Path: Encrypted(value.PathFullName),
                            Fecha: d
                        };

                        $('#Files').bootstrapTable('insertRow', {
                            index: Index,
                            row: row
                        });
                        Index = Index + 1;
                    }
                );
            }
            $("#loading").dialog("close");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#Files').bootstrapTable('removeAll');
            $("#loading").dialog("close");
            alert("fail: " + errorThrown);
        }
    });
};

function IsEnable() {
    var resultValue = "";
    var urlAction = urlBase + "/IsEnable";
    $.ajax({
        url: urlAction,
        async: false,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            resultValue = result.d;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail: " + errorThrown);
        }
    });
    return resultValue;
}

function Encrypted(keyValue) {
    var urlAction = urlBase + "/Encrypted";
    var result = "";
    var param = JSON.stringify({ value: keyValue });
    $.ajax({
        url: urlAction,
        async: false,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (item) {
            result = item.d;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").dialog("close");
        }
    });
    return result;
};

function LinkFormatter(value, row, index) {
    var htmlValue = '<a class="text-center" href="' + window.location.protocol + '//' + window.location.host + '/dropthings/download.ashx?path=' + value + '&IsFolder=False' + '"><i class="fa fa-download"></i></a>';
    return htmlValue;
};