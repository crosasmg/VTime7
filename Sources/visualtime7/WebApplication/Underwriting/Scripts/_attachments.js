var token = "";
var gridAttachments;

function loadAttachment() {
	$("#case-attachment-iframe").attr("src", "controls/partials/_attachments.aspx?caseId=" + $('#dpeCaseId_I').val());
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    //$.jgrid.defaults.styleUI = 'Bootstrap';
    //"use strict";

    //// Removes the 'Loading...' text of the grid
    //$.jgrid.defaults.loadtext = '';

    //// The next snippet will hide all elements with the class specified in data-hide,
    //// i.e: data-hide="alert" will hide all elements with the alert property.
    //$("[data-hide]").on("click", function () {
    //    $("." + $(this).attr("data-hide")).hide();
    //});

    //// Creates a new token
    ////token = $('#token').data('token');

    //// Sets the grid of attachments to a variable for easy handling
    //gridAttachments = $("#grid-attachments");
    //var gridColNamesAttachments;
    //var gridColModelAttachments;

    //// Saves the previous open row on the jqgrid
    //var previousRowIdAttachments = 0;

    //gridColNamesAttachments = ['Fecha', 'Nombre', 'Descripción', 'AttachmentId'];
    //gridColModelAttachments = [{ name: 'UploadedDate', index: 'UploadedDate', resizable: true, editable: false, width: 140 },
    //                           { name: 'FileName', index: 'FileName', resizable: true, editable: false, width: 360 },
    //                           { name: 'FileDescription', index: 'FileDescription', resizable: true, editable: false, width: 400 },
    //                           { name: 'AttachmentId', index: 'AttachmentId', hidden: true }];

    //// Grid object options
    //gridAttachments.jqGrid({
    //    url: "services/Attachment.aspx/GetAttachments",
    //    mtype: "POST",
    //    datatype: 'json',
    //    postData: {},
    //    ajaxGridOptions: { contentType: "application/json", timeout: 30000 },
    //    serializeGridData: function (data) {
    //        return {};
    //        data = {};
    //    },
    //    jsonReader: {
    //        repeatitems: false,
    //        root: function (obj) { return obj.d; }
    //    },
    //    loadBeforeSend: function (jqXHR) {
    //        //jqXHR.setRequestHeader("Authorization", "Bearer " + token);
    //    },
    //    loadError: function (jqXHR, textStatus, errorThrown) {
    //        if (textStatus === "timeout") {
    //            alert("Ha ocurrido un error con el servicio, por favor, contacte a su administrador");
    //        }
    //        // alert('HTTP status code: ' + jqXHR.status + '\n' + 'textStatus: ' + textStatus + '\n' + 'errorThrown: ' + errorThrown);
    //        // alert('HTTP message body (jqXHR.responseText): ' + '\n' + jqXHR.responseText);
    //    },
    //    colNames: gridColNamesAttachments,
    //    colModel: gridColModelAttachments,
    //    //rowNum: 10,
    //    //rowList: [10, 20, 30],
    //    pager: '#pager-attachments',
    //    sortname: 'id',
    //    //viewrecords: true,
    //    sortorder: "desc",
    //    //width: "800",
    //    autowidth: true,
    //    //shrinkToFit: true,
    //    height: "auto !important",
    //    multiselect: false,
    //    editurl: "services/Attachment.aspx/AddAttachment",
    //    subGrid: true,
    //    loadComplete: function (data) {
    //        $loading.hide();
    //    },
    //    subGridRowExpanded: function (subgridId, rowId) {
    //        // we pass two parameters
    //        // subgrid_id is a id of the div tag created within a table data
    //        // the id of this element is a combination of the "sg_" + id of the row
    //        // the row_id is the id of the row
    //        // If we wan to pass additional parameters to the URL we can use
    //        // a method getRowData(row_id) - which returns associative array in type name-value
    //        // here we can easy construct the flowing
    //        //var subgridTableId, pagerId;
    //        //subgridTableId = subgridId + "_t";
    //        //pagerId = "p_" + subgridTableId;

    //        //// Closes any other previous subGridRow
    //        //if (previousRowIdAttachments !== 0) {
    //        //    $(this).collapseSubGridRow(previousRowIdAttachments);
    //        //}
    //        //// Saves the actual row_id
    //        //previousRowIdAttachments = rowId;

    //        ////var selectedUploadedDate = $("tr#" + rowId + " > td[aria-describedby=grid-attachments_UploadedDate]").text();
    //        ////var selectedFileName = $("tr#" + rowId + " > td[aria-describedby=grid-attachments_FileName]").text();
    //        ////var selectedFileDescription = $("tr#" + rowId + " > td[aria-describedby=grid-attachments_FileDescription]").text();
    //        ////var selectedAttachmentId = $("tr#" + rowId + " > td[aria-describedby=grid-attachments_AttachmentId]").text();

    //        //// Renders all the HTML related to the attachments detail     
    //        //$("#" + subgridId)
    //        //    .html("<table id='" + subgridTableId + "' class='scroll'></table><div id='" + pagerId + "' class='scroll'></div>")
    //        //    .load("controls/partials/_attachmentDetail.aspx", function () {
    //        //        $(this).append();
    //        //        //$(this).append("<span id='attachmentId' data-attachment-id='" + selectedAttachmentId + "' />");

    //        //        //$(this).find("#btn-insertar-anexo").hide();
    //        //        //$(this).find("#btn-actualizar-anexo").show();
    //        //        //$(this).find("#btn-borrar-anexo").show();

    //        //        //$("select#ddlAttachments").val(selectedAttachments);
    //        //        //$("input#txtCodigoDelCliente").val(selectedClientID);
    //        //        //$("input#txtNombre").val(selectedClientName);
    //        //    });
    //    },
    //    subGridRowColapsed: function (subgridId, rowId) {
    //        // this function is called before removing the data
    //        // var subgrid_table_id;
    //        // subgrid_table_id = subgrid_id+"_t";
    //        // jQuery("#"+subgrid_table_id).remove();
    //    }
    //});

    //$("#pager-attachments").find("#pager-attachments_center").hide();
    //gridAttachments.jqGrid('navGrid', '#pager-attachments', {
    //    edit: false,
    //    add: true,
    //    del: false,
    //    search: false,
    //    rowList: [], // disable page size drop down
    //    pgbuttons: false, // disable page control like next, back button
    //    pgtext: null, // disable pager text like 'Page 0 of 10'
    //    viewrecords: false, // disable current view record text like 'View 1-10 of 100'
    //    pgbuttons: false   //Arrows to go back and forth between pages
    //});
});