var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/Admin/WorkFlowManager.aspx';
var urlDetail = window.location.protocol + '//' + window.location.host + '/dropthings/WorkFlowManagerDetails.html';

$(document).ready(function () {
    $('input[name="dpDateStart"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'YYYY-MM-DD'
        }
    });

    $('input[name="dpDateEnd"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'YYYY-MM-DD'
        }
    });

    $('#Files').bootstrapTable({
        height: 400,
        onDblClickRow: function (row, $element) {
            $("#ModalViewer").modal();
        }
    });

    
    $("#btnQuery").click(function () {
        var urlAction = urlBase + "/Workflows";
        var keyValue1 = new Date($('input[name="dpDateStart"]').val());
        var keyValue2 = new Date($('input[name="dpDateEnd"]').val());
        var keyValue3 = $('#txtFilter').val();
        var param = JSON.stringify({ dateStart: keyValue1, dateEnd: keyValue2, filter: keyValue3 });
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
                                WorkflowinstanceId: value.WorkflowinstanceId,
                                TimeCreated: new moment(value.TimeCreated).format('YYYY/MM/DD HH:mm SS'),
                                Identify: value.Identify,
                                Name: value.Name,
                                WorkflowState: value.WorkflowState,
                                Reason: value.Reason,
                                //StartDate: new moment(value.StartDate).format('YYYY/MM/DD HH:mm SS'),
                                //FinishDate: new moment(value.FinishDate).format('YYYY/MM/DD HH:mm SS'),
                                Duration: value.Duration
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
    });

    $("#btnRefresh").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('#ifWindows').attr("src", $('#ifWindows').attr("src"));
    });

    $(".btnReason").live("click", function () {
        var WorkflowinstanceId = $(this).data("workflowinstanceid");
        var reason = $(this).data("reason");

        var mymodal = $('#myModal');
        mymodal.find('.modal-body #ifWindows').attr("src", urlDetail + '?Id=' + WorkflowinstanceId );
        mymodal.modal('show');

    });

    $('.modal-content').resizable({
        //alsoResize: ".modal-dialog",
        minHeight: 300,
        minWidth: 300
    });

    $('.modal-dialog').draggable();

    $('#myModal').on('show.bs.modal', function (e) {
        //$('.modal .modal-body').css('overflow-y', 'auto');
        //$('.modal-dialog').css('max-height', $(window).height() * 0.7);
        //$('.modal .modal-body').css('height', $(window).height() * 0.7);
        ////$('.modal .modal-body').css('max-width', $(window).width() * 0.7);
        ////$('.modal .modal-body').css('width', $(window).width() * 0.7);

        ////$('.modal').css('max-height', $(window).height() * 0.9);
        ////$('.modal').css('height', $(window).height() * 0.9);
        ////$('.modal').css('max-width', $(window).width() * 0.7);
        ////$('.modal').css('width', $(window).width() * 0.7);

    })

});

function FormaterWorkflowState(value, row, index) {
    var src = "";

    if (value === "Completed") {
        src = 'src="../../images/dropthings/Correct.png"';
    }

    if (value === "Terminated") {
        src = 'src="../../images/dropthings/Error.png"';
    }

    if (src === "") {
        src = 'src="../../images/dropthings/Undefined.png"';
    }

    return '<img ' + src + ' height="24" width="24" ' + (value == 'Terminated' ? 'data - toggle="tooltip" title= "' + row.Reason +'"' : '' ) +' >';
};

function FormaterIdentify(value, row, index) {
    return '<i data-toggle="tooltip" title="'+ row.Name +'" >'+ value +'</i>';
};

function FormatterReason(value, row, index) {
    return '<button data-reason="' + (row.Reason == "" ? '' : row.Reason ) +'" data-WorkflowinstanceId="' + row.WorkflowinstanceId + '" type="button" class="btnReason btn btn-primary btn-xs" ><i class="fa fa-search-plus" aria-hidden="true"></i></button>';
 };