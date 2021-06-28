var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/Admin/WorkFlowManager.aspx';
var idValue;

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });

    $('#Files').bootstrapTable({
        onDblClickRow: function (row, $element) {
            $("#ModalViewer").modal();
        }
    });

    $("#btnSummaryShow").click(function (e) {
        var $this = $("#frmgSummary");

        if (!$this.hasClass('collapsed')) {
            $("#btnSummaryShow").find('i').removeClass('fa fa-chevron-up').addClass('fa fa-chevron-down');
        }
        else {
            $("#btnSummaryShow").find('i').removeClass('fa fa-chevron-down').addClass('fa fa-chevron-up');
        }

        if (!$this.hasClass('collapsed')) {
            $this.slideUp();
            $this.addClass('collapsed');
        } else {
            $this.slideDown();
            $this.removeClass('collapsed');
        }

        $this = $("#frmgStack");

        if (!$this.hasClass('collapsed')) {
            $this.slideUp();
            $this.addClass('collapsed');
        } else {
            $this.slideDown();
            $this.removeClass('collapsed');
        }
    });

    ViewDetail(idValue);

    ViewHistory(idValue);

    $(".btnReason").on('click', function () {
        var WorkflowinstanceId = $(this).data("workflowinstanceid");
    });
    
});



function CallModel(current) {
    var data = current.attributes["data-pro"].value;
    if (data !== "") {
        data = decodeURI(data);
    }
    var mymodal = $('#myModal');
    mymodal.find('.modal-body #lblData').text(data);
    mymodal.modal('show');
};

function ViewDetail(id) {
    var urlAction = urlBase + "/ViewDetail";
    var keyValue1 = id;
    var param = JSON.stringify({ Id: keyValue1 });
    $.ajax({
        url: urlAction,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.d.ExistData) {
                $("#lblSummary").text(result.d.Reason);
                $("#lblStack").text(result.d.Data);

                $("#frmgStack").removeClass('collapsed');
                $("#frmgStack").slideDown();
                $("#frmgSummary").removeClass('collapsed');
                $("#frmgSummary").slideDown();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail: " + errorThrown);
        }
    });
};

function ViewHistory(id) {
    var urlAction = urlBase + "/ViewHistory";
    var keyValue1 = id;
    var param = JSON.stringify({ Id: keyValue1 });
    $.ajax({
        url: urlAction,
        data: param,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#Files').bootstrapTable('removeAll');
            if (result.d.length !== 0) {
                var Index = 0;
                var data = new Array();
                result.d.forEach(
                    function Formate(value) {
                        var row = {
                            RecordNumber: value.RecordNumber,
                            ActivityName: value.ActivityName,
                            ActivityType: value.ActivityType,
                            State: value.State,
                            TimeStart: new moment(value.TimeStart).format('YYYY/MM/DD HH:mm SS'),
                            TimeFinish: new moment(value.TimeFinish).format('YYYY/MM/DD HH:mm SS'),
                            Duration: value.Duration,
                            Data: value.Data
                        };

                        data.push(row);
                    }
                );

                $('#Files').bootstrapTable('append', data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail: " + errorThrown);
        }
    });
};

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function FormaterWorkflowState(value, row, index) {
    var src = "";

    if (value === "Closed") {
        src = 'src="../../images/dropthings/Correct.png"';
    }

    if (value === "Faulted") {
        src = 'src="../../images/dropthings/Error.png"';
    }

    if (src === "") {
        src = 'src="../../images/dropthings/Undefined.png"';
    }

    return '<img ' + src + ' height="24" width="24" ' + (value == 'Terminated' ? 'data - toggle="tooltip" title= "' + row.Reason + '"' : '') + ' >';
};

function FormaterDuration(value, row, index) {
    var html = '<i>Inicio:' + row.TimeStart + '</i><br/><i>Fin:' + row.TimeFinish + '</i>';
    return '<i data-toggle="tooltip" title="' + html + '" >' + value + '</i>';
};

function FormaterActivityName(value, row, index) {
    return '<i data-toggle="tooltip" title="' + row.ActivityType + '" >' + value + '</i>';
};

function FormatterData(value, row, index) {
    return '<button onClick="return CallModel(this)" data-pro="' + encodeURI(row.Data) +'"  ' + (row.Data == "" ? 'disabled' : '') + ' type="button" class="btnReason btn btn-primary btn-xs" ><i class="fa fa-search-plus" aria-hidden="true"></i></button>';
};

(function ($) {
    idValue = getParameterByName('Id');
    $("#frmgStack").addClass('collapsed');
    $("#frmgStack").slideUp();
    $("#frmgSummary").addClass('collapsed');
    $("#frmgSummary").slideUp();
}(jQuery));