var token = "";
var gridRules;
var gridColNamesRules;
var gridColModelRules;
var previousRowIdRules = 0;
var underwriter;
var DeniedRule;
var arrayDescription;
var caseIsAllowed = false;

function ReloadUnderwritingRulesGrid(isEditMode, requirementId, requirementStatus) {
    // $.jgrid.gridUnload("grid-rules");
    $("#grid-rules").jqGrid("GridUnload");

	ProxySyncUnderwritingRule.invoke("GetTextDescription", "", function (data) { arrayDescription = data.d });
	DeniedRule = arrayDescription.DeniedUpdate;

	var alarmType;
	ProxySyncLookUps.invoke("GetAlarmType", param, function (data) { alarmType = data.d; });
	var value = $.grep(alarmType, function (n, i) {
	    return n.Code == "5";
	});
	caseIsAllowed = (value.length !== 0)
	if (caseIsAllowed) {
	    var param = JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
	    ProxySyncUnderwritingRule.invoke("CaseIsAllowed", param, function (data) { caseIsAllowed = data.d; });
	} else {
	    DeniedRule = arrayDescription.DeniedUnderwritingRule;
	}
	
	var delSettings = $.extend({}, GeneralDelSettings, {
		serializeDelData: function (postData) {
			var dataFromTheRow = gridRules.jqGrid('getRowData', postData.id);
			return JSON.stringify({ caseId: $('#dpeCaseId_I').val(), requirementId: requirementId, UnderRuleId: dataFromTheRow.UnderRuleId, consequenceId: dataFromTheRow.ConsequenceId });
		},
		beforeSubmit: function (postData, formid) {
			var dataFromTheRow = gridRules.jqGrid('getRowData', postData);
			if (dataFromTheRow.IsManualRule.toLowerCase() != 'true') {
				if (dataFromTheRow.RuleDescription.trim().length > 0)
				    return [false, arrayDescription.AppliedRule];
				else
				    return [false, arrayDescription.NoManualRule];
			}
			return [true, ''];
		},
		url: "services/UnderwritingRule.aspx/RemoveUnderwritingRule"
	});

	gridRules = $("#grid-rules");

	gridRules.jqGrid($.extend({}, GeneralGridOptions, {
		url: "services/UnderwritingRule.aspx/GetAllUnderwritingRules",
		serializeGridData: function (data) {
			return JSON.stringify({ caseId: $('#dpeCaseId_I').val(), requirementId: requirementId });
			data = {};
		},
		colNames: gridColNamesRules,
		colModel: gridColModelRules,
		pager: '#pager-rules',
		viewrecords: true,
		rowNum: 10,
		pgbuttons: true,
		subGrid: false,
		sortorder: "desc",
		loadComplete: function (data) { },
		subGridRowExpanded: function (subgridId, rowId) {
			if (false) {
				var subgridTableId, pagerId;
				subgridTableId = subgridId + "_t";
				pagerId = "p_" + subgridTableId;

				if (previousRowIdRole !== 0) {
					$(this).collapseSubGridRow(previousRowIdRole);
				}

			}
		}
	}));

	gridRules.jqGrid(
		'navGrid',
		'#pager-rules',
		{
			edit: false,
			add: false,
			del: (isEditMode && caseIsAllowed),
			search: false,
			rowList: [],
			pgbuttons: false,
			pgtext: null,
			viewrecords: false,
			refresh: false
		},
		{},
		{},
		delSettings);

	gridRules.jqGrid('navButtonAdd', '#pager-rules', {
	    caption: "",
	    title: caseIsAllowed ? isEditMode ? arrayDescription.UpdateRule : arrayDescription.ViewRule : arrayDescription.ViewRule,
	    buttonicon: caseIsAllowed ? isEditMode ? "glyphicon glyphicon-edit icon-high" : "glyphicon glyphicon-search icon-high" : "glyphicon glyphicon-search icon-high",
	    onClickButton: function () {
	        var selRowId = gridRules.jqGrid('getGridParam', 'selrow');
	        if (selRowId != null) {
	            var dataFromTheRow = gridRules.jqGrid('getRowData', selRowId);
	            if (dataFromTheRow.IsManualRule.toLowerCase() == 'true') {
	                $("#btn-guardar-regla").removeProp('disabled');
	                $("#modal-addrule-content").load("controls/partials/_addRule.aspx", function () { ReloadAlarmGrid(isEditMode, dataFromTheRow.UnderRuleId, dataFromTheRow.Answer); });
	            } else {
	            	$("#modal-addrule-content").load("controls/partials/_addRule.aspx", function () { ReloadAlarmGrid(false, dataFromTheRow.UnderRuleId, dataFromTheRow.Answer); });
	            }
	            $('#addRule').modal('show');
	        }
	    },
	    position: "first",
	    cursor: "pointer"
	});

	if (isEditMode && requirementStatus != "5") {
		if (caseIsAllowed) {
			gridRules.jqGrid('navButtonAdd', '#pager-rules', {
				caption: "",
				title: arrayDescription.AddRule,
				buttonicon: "glyphicon glyphicon-plus",
				onClickButton: function () {
					$("#btn-guardar-regla").removeProp('disabled');
					$("#modal-addrule-content").load("controls/partials/_addRule.aspx", function () { ReloadAlarmGrid(isEditMode); });
					$('#addRule').modal('show');
				},
				position: "first",
				cursor: "pointer"
			});
		} else {
		    gridRules.append('<td colspan="5" class=" subgrid-data"><div>' + DeniedRule + '</div></td>');
		}
	} 

	gridRules.jqGrid("filterToolbar", {
		searchOnEnter: false,
		enableClear: false,
		searchOperators: false,
		defaultSearch: 'cn',
		autosearch: true
	});
	gridRules.trigger("reloadGrid");

	$("#jqgh_grid-rules_EncodedExplanation").removeClass("ui-th-div");
}



function changeFormConditions(controlId) {
	var seletedAlarm = $(controlId).val();
	var requiredDataFlatExtra = true;
	var requiredDataExclusion = true;
	var $tableForm = $("#TblGrid_grid-rules");

	//Panel de flat extra premium
	if (seletedAlarm == 5) {
		$tableForm.find(".FormData[rowpos=5]").show();
		$tableForm.find(".FormData[rowpos=6]").show();
		requiredDataFlatExtra = true;
	}
	else {
		$tableForm.find(".FormData[rowpos=5]").hide();
		$tableForm.find(".FormData[rowpos=6]").hide();
		requiredDataFlatExtra = false;
	}
	//Panel de exclusiones
	if (seletedAlarm == 6) {
		$tableForm.find(".FormData[rowpos=7]").show();
		$tableForm.find(".FormData[rowpos=8]").show();
		requiredDataExclusion = true;
	}
	else {
		$tableForm.find(".FormData[rowpos=7]").hide();
		$tableForm.find(".FormData[rowpos=8]").hide();
		requiredDataExclusion = false;
	}
	gridRules.jqGrid('setColProp', 'FlatExtraPremium', { editrules: { required: requiredDataFlatExtra } });
	gridRules.jqGrid('setColProp', 'DurationOfFlatExtraPremiumDays', { editrules: { required: requiredDataFlatExtra } });
	gridRules.jqGrid('setColProp', 'DurationOfFlatExtraPremiumMonths', { editrules: { required: requiredDataFlatExtra } });
	gridRules.jqGrid('setColProp', 'DurationOfFlatExtraPremiumYears', { editrules: { required: requiredDataFlatExtra } });
	gridRules.jqGrid('setColProp', 'ExclusionType', { editrules: { required: requiredDataExclusion } });
	gridRules.jqGrid('setColProp', 'ExclusionPeriodType', { editrules: { required: requiredDataExclusion } });
}

function showModalInformation(value) {
	$('#textBody').html(value);
	$('#textTitle').html('Explicaci&oacute;n de la regla.');
	$('#myModal').modal('show');
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
	$.jgrid.defaults.styleUI = 'Bootstrap';
	"use strict";
	// Removes the 'Loading...' text of the grid
	$.jgrid.defaults.loadtext = '';

	var UnderwritingAreaType = GetLookUps("GetUnderwritingAreaType");
	var QuestionLkp = GetLookUps("GetQuestionsFromRequirement");
	ProxySyncUnderwritingRule.invoke("GetHeaderValues", "", function (data) { gridColNamesRules = data.d; });

	gridColModelRules = [{ name: 'UnderRuleId', index: 'UnderRuleId', hidden: true, width: 180, resizable: true, editable: false },
						{ name: 'RuleDescription', index: 'RuleDescription', width: 180, resizable: true, editable: false },
						{ name: 'QuestionId', index: 'QuestionId', hidden: true, width: 140, resizable: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: QuestionLkp, defaultValue: "1" } },
						{ name: 'QuestionIdDescription', index: 'QuestionIdDescription', width: 180, resizable: true, hidden: false, editable: false },
						{ name: 'UnderwritingArea', index: 'UnderwritingArea', width: 140, resizable: true, hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: UnderwritingAreaType, defaultValue: "1" } },
						{ name: 'UnderwritingAreaDescription', index: 'UnderwritingAreaDescription', width: 180, resizable: true, hidden: false, editable: false },
						{ name: 'EncodedExplanation', index: 'EncodedExplanation', hidden: false, editable: true, width: 64, align: 'center', formatter: function (cellvalue, options, rowobject) { return '<button type="button" class="btn btn-info btn-circle" onclick="showModalInformation(' + '\'' + cellvalue + '\'' + ');"><i class="fa fa-question"></i></button>'; }, edittype: "textarea" },
						{ name: 'IsManualRule', index: 'IsManualRule', width: 43, resizable: true, hidden: false, align: 'center', editable: true, formatter: function (cellvalue, options, rowobject) { return '<input type="checkbox"' + (cellvalue ? ' checked="checked" ' : ' ') + 'offval="no" disabled="disabled">'; }, edittype: 'checkbox', editoptions: { value: "True:False", defaultValue: "True", disabled: "true" }, formatter: "checkbox" },                        
	                    { name: 'Answer', index: 'Answer', hidden: true, width: 180, resizable: true, editable: false },
	                    { name: 'ConsequenceId', index: 'ConsequenceId', hidden: true, width: 180, resizable: true, editable: false }];

});