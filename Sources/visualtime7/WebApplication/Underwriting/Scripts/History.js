var token = "";
var gridHistory;
var gridColNamesHistory;
var gridColModelHistory;
var previousRowIdHistory = 0;

function ReloadHistoryGrid(editProperty) {
	// $.jgrid.gridUnload("grid-history");
    gridHistory = $("#grid-history");
    gridHistory.jqGrid("GridUnload");

    var numCaso = $('#dpeCaseId_I').val().length === 0 ? 0 : $('#dpeCaseId_I').val();
    $loading.hide();
    gridHistory.jqGrid($.extend({}, GeneralGridOptions, {
    	url: "services/CaseHistory.aspx/GetCaseHistory",
        colNames: gridColNamesHistory,
        colModel: gridColModelHistory,
        serializeGridData: function (data) {
        	return JSON.stringify({ caseId: numCaso });
        	data = {};
        },
        pager: '#pager-history',
        subGrid: false,
        viewrecords: true,
        pgbuttons: true,
        sortname: 'CaseHistoryId',
        firstsortorder: 'desc',
        rowNum: 10,
        sortorder: 'desc',
        loadComplete: function () {
            var $self = $(this);
            if ($self.jqGrid("getGridParam", "datatype") === "json") {
                setTimeout(function () {
                    $self.trigger("reloadGrid"); // Call to fix client-side sorting
                }, 50);
            }
            if ($(this).getGridParam("reccount") == 0) {
            	if ($.trim($("#dpeCaseId_I").val()).length > 0)
            		$("#textNoDataAvailableDiv").show();
            	//$(this).parents(".ui-jqgrid").hide();
            }
            else {
            	$("#textNoDataAvailableDiv").hide();
            	$(this).parents(".ui-jqgrid").show();
            }
        }
    }));
    gridHistory.jqGrid('navGrid', '#pager-history', {
        edit: false,
        add: false,
        del: false,        
        rowList: [],
        search: false,        
        viewrecords: false,
        refresh: false
    });
    gridHistory.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    
    gridHistory.trigger('reloadGrid');
}
/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    ProxySyncHistory.invoke("GetHeaderValues", "", function (data) { gridColNamesHistory = data.d; });
    gridColModelHistory = [{ name: 'CaseHistoryId', index: 'CaseHistoryId', resizable: true, editable: false, width: 50, sorttype: 'number', },
						   { name: 'CreationDate', index: 'CreationDate', resizable: true, editable: false, width: 140, sorttype: 'date', formatter: 'date', formatoptions: { newformat: 'd/m/Y h:i A' } },
                           { name: 'EntryTypeEnumText', index: 'EntryTypeEnumText', resizable: true, width: 1, editable: false, hidden: true },
						   { name: 'EntryTypeByLanguage', index: 'EntryTypeByLanguage', resizable: true, editable: false, width: 160, },
                           { name: 'RequirementTypeEnumText', index: 'RequirementTypeEnumText', resizable: true, editable: false, width: 1, hidden: true },
                           { name: 'RequirementTypeByLanguage', index: 'RequirementTypeByLanguage', resizable: true, editable: false, width: 180 },
                           { name: 'StageTypeByLanguage', index: 'StageTypeByLanguage', resizable: true, editable: false, width: 140 },
                           { name: 'StatusTypeByLanguage', index: 'StatusTypeByLanguage', resizable: true, editable: false, width: 140 },
                           { name: 'ManualOrAutomaticEnumText', index: 'ManualOrAutomaticEnumText', resizable: true, editable: false, width: 1, hidden: true },
						   { name: 'ManualOrAutomaticByLanguage', index: 'ManualOrAutomaticByLanguage', resizable: true, editable: false, width: 160 },
                           { name: 'AlarmTypeEnumText', index: 'AlarmTypeEnumText', resizable: true, editable: false, width: 1, hidden: true },
						   { name: 'AlarmTypeByLanguage', index: 'AlarmTypeByLanguage', resizable: true, editable: false, hidden: true, width: 160 },
						   { name: 'Underwriter', index: 'Underwriter', resizable: true, editable: false, width: 180,formatter: function (cellvalue, options, rowobject) { if (cellvalue == 0 || cellvalue == null){ return '';} else return cellvalue; } },
						   { name: 'Remarks', index: 'Remarks', resizable: true, editable: false, width: 200 }];
});