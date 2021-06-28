var token = "";
var gridHistoryPremium;
var gridColNamesHistoryPremium;
var gridColModelHistoryPremium;
var previousRowIdHistoryPremium = 0;
var detailButton;
function ReloadHistoryPremiumGrid(editProperty) {
    $loading.hide();
    // $.jgrid.gridUnload("grid-history-premium");
    gridHistoryPremium = $("#grid-history-premium");
    gridHistoryPremium.jqGrid("GridUnload");

    gridHistoryPremium.jqGrid($.extend(GeneralGridOptions, {
    	url: "services/PolicyHistory.aspx/GetPremiumHistory",
    	serializeGridData: function (data) {
    	    return JSON.stringify({ caseId: $('#dpeCaseId_I').val().length === 0 ? 0 : $('#dpeCaseId_I').val() });
    		data = {};
    	},
        colNames: gridColNamesHistoryPremium,
        colModel: gridColModelHistoryPremium,
        pager: '#pager-history-premium',
        subGrid: false,
        viewrecords: false,
        pgbuttons: true
    }));
    gridHistoryPremium.jqGrid('navGrid', '#pager-history-premium', {
        edit: false,
        add: false,
        del: false,
        search: false,
        rowList: [],        
        pgtext: null,
        viewrecords: false,
        refresh: false
    });
    gridHistoryPremium.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridHistoryPremium.trigger("reloadGrid");

    gridHistoryPremium.delegate("a.HistoryDetailWindowBtn", "click", function () {
        var selRowId = $(this).closest('tr').attr('id');
        var rowData = gridHistoryPremium.getRowData(selRowId);
        var releaseNumber = rowData['Release'];
        var underwritingCaseID = rowData['UnderwritingCaseID'];
        GetViewPolicyUrlByUWCaseID(underwritingCaseID, releaseNumber);
    });
}
/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    ProxySynPolicyHistory.invoke("GetButtonValue", "", function (data) { detailButton = data.d; })
    ProxySynPolicyHistory.invoke("GetHeaderValues", "", function (data) { gridColNamesHistoryPremium = data.d; });
    gridColModelHistoryPremium = [{ name: 'UnderwritingCaseID', index: 'UnderwritingCaseID', resizable: true, editable: false, width: 15, sorttype: 'number', },
                                  { name: 'Release', index: 'Release', resizable: true, editable: false, width: 15, sorttype: 'number', },
                                  { name: 'ReleaseDate', index: 'ReleaseDate', resizable: true, editable: false, width: 35, sorttype: 'date', formatter: 'date', formatoptions: { newformat: 'd/m/Y h:i A' } },
						          { name: 'RequirementId', index: 'RequirementId', resizable: true, editable: false, width: 25 },
                                  { name: 'Description', index: 'Description', resizable: true, editable: false},
                                  { name: 'DetailBtn', index: 'DetailBtn', resizable: true, editable: false, width: 25, formatter: function (cellvalue, options, rowobject) { return '<a class="HistoryDetailWindowBtn fm-button btn btn-default fm-button-icon-left"><span class="fa fa-binoculars"></span>  ' + detailButton + ' </a>'; } }];
});