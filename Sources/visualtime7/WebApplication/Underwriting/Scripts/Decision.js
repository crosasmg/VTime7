var token = "";
var gridDecision;
var gridColNamesDecision;
var gridColModelDecision;
var previousRowIdDecision = 0;
var explanationMessage;
var commentaryTitle;
var selectedRestrictionType;
var selectedAlarmTypeDescription;
var selectedUnderwritingRuleId;
var numCaso;
var objectRestriction;

function ReloadDecicionGrid(editProperty) {
    ProxySyncRequirement.invoke("GetExplanationMessage", "", function (data) { explanationMessage = data.d });
    ProxySyncRequirement.invoke("GetCommentaryTitle", "", function (data) { commentaryTitle = data.d });
	// $.jgrid.gridUnload("grid-decision");
    gridDecision = $("#grid-decision");
    gridDecision.jqGrid("GridUnload");

	numCaso = $('#dpeCaseId_I').val().length === 0 ? 0 : $('#dpeCaseId_I').val();
    gridDecision.jqGrid($.extend({}, GeneralGridOptions, {
    	url: "services/Requirement.aspx/GetAllDecisions",
    	serializeGridData: function (data) {
    		return JSON.stringify({ caseId: numCaso });
    		data = {};
    	},
        colNames: gridColNamesDecision,
        colModel: gridColModelDecision,
        pager: '#pager-decision',
        subGrid: true,
        viewrecords: true,
        rowNum: 10,
        pgbuttons: true,
        subGridRowExpanded: function (subgridId, rowId) {
            // we pass two parameters
            // subgrid_id is a id of the div tag created within a table data
            // the id of this element is a combination of the "sg_" + id of the row
            // the row_id is the id of the row
            // If we wan to pass additional parameters to the URL we can use
            // a method getRowData(row_id) - which returns associative array in type name-value
            // here we can easy construct the flowing
        	SetRestrictioninCache($('#dpeCaseId_I').val(), rowId.substring(3));
            var subgridTableId, pagerId;
            subgridTableId = subgridId + "_t";
            pagerId = "p_" + subgridTableId;

            // Closes any other previous subGridRow
            if (previousRowIdRestrictions !== 0) {
                $(this).collapseSubGridRow(previousRowIdRestrictions);
            }
            // Saves the actual row_id
            previousRowIdRestrictions = rowId;

            selectedAlarmTypeDescription = $("tr#" + rowId + " > td[aria-describedby=grid-decision_AlarmTypeByLanguage]").text();
             selectedRestrictionType = $("tr#" + rowId + " > td[aria-describedby=grid-decision_AlarmType]").text();
             selectedUnderwritingRuleId = $("tr#" + rowId + " > td[aria-describedby=grid-decision_UnderwritingRuleId]").text();
             var selectedRequirementType = $("tr#" + rowId + " > td[aria-describedby=grid-decision_RequirementTypeByLanguage]").text();
             var selectedQuestion = $("tr#" + rowId + " > td[aria-describedby=grid-decision_QuestionTextByLanguage]").text();
             var selectedClientID = $("tr#" + rowId + " > td[aria-describedby=grid-decision_ClientId]").text();
             var selectedExclusionClientID = $("tr#" + rowId + " > td[aria-describedby=grid-decision_ExclusionClientDescription]").text();
             var selectedRequirementID = $("tr#" + rowId + " > td[aria-describedby=grid-decision_RequirementID]").text();
             var selectedUnderwritingArea = $("tr#" + rowId + " > td[aria-describedby=grid-decision_UnderwritingAreaByLanguage]").text();
             var selectedStatus = $("tr#" + rowId + " > td[aria-describedby=grid-decision_StatusByLanguage]").text();
             var selectedManualOrAutomatic = $("tr#" + rowId + " > td[aria-describedby=grid-decision_ManualOrAutomaticRestriction]").text();
             var selectedCommentary = $("tr#" + rowId + " > td[aria-describedby=grid-decision_Commentary]").text();
             var selectedDate = $("tr#" + rowId + " > td[aria-describedby=grid-decision_CreationDate]").text();
             //var selectedDate = new Date(eval(selectedDateRow.slice(6, -2))).format(window.__cultureInfo.dateTimeFormat.ShortDatePattern);
             var selectedTotalDebits = $("tr#" + rowId + " > td[aria-describedby=grid-decision_TotalDebits]").text();
             var selectedTotalCredits = $("tr#" + rowId + " > td[aria-describedby=grid-decision_TotalCredits]").text();
             var selectedTotalBalance = $("tr#" + rowId + " > td[aria-describedby=grid-decision_Balance]").text();
             var selectedCreatedBy = $("tr#" + rowId + " > td[aria-describedby=grid-decision_CreatedByRestriction]").text();
             var selectedExplanation = $("tr#" + rowId + " > td[aria-describedby=grid-decision_Explanation]").text();
             var selectedAnswer = $("tr#" + rowId + " > td[aria-describedby=grid-decision_Answer]").text();
            try {
                var dataJson = JSON.stringify({ caseId: numCaso, requirementId: selectedRequirementID, UnderwritingRuleId: selectedUnderwritingRuleId, AlarmType: selectedRestrictionType });
            	ProxySyncRestrictions.invoke("GetRestrictionByUnderwritingRuleId", dataJson, function (data) {
            	    objectRestriction = data.d;
                    selectedDate = jsonToDateTime(data.d.CreationDate);
                });
            } catch (ex) { }

            $("#" + subgridId).html("<div id='PrincipalDecision" + subgridId + "'><hr></div><div id='Restriccion" + subgridId + "'></div>")

            $("#PrincipalDecision" + subgridId)
              .html("<table id='principalTable" + subgridTableId + "' class='scroll'></table><div id='principal" + pagerId + "' class='scroll'></div>")
                    .load("controls/partials/_decisionDetail.aspx", function () {
                        $("#txtRequerimiento").val(selectedRequirementType);
                        $("#txtPregunta").val(selectedQuestion);
                        $("#txtCliente").val(selectedClientID);
                        $("#txtAreaSuscripcion").val(selectedUnderwritingArea);
                        $("#txtEstadoCaso").val(selectedStatus);
                        $("#txtComentario").val(selectedCommentary);
                        $("#txtFechaHora").val($.trim(selectedDate));
                        $("#txtTotalDebitos").val(selectedTotalDebits);
                        $("#txtTotalCreditos").val(selectedTotalCredits);
                        $("#txtBalance").val(selectedTotalBalance)
                        $("#txtCreadaPor").val(selectedCreatedBy)
                        $("#txtExplicacion").val(selectedExplanation)
                        $("#txtRespuesta").val(selectedAnswer)
                        $("#txtTipoAlarma").val(selectedAlarmTypeDescription)
                        if (selectedManualOrAutomatic == 2)
                        {
                            $("#checkManual").attr("checked","checked")
                        }
                    });

            if (selectedRestrictionType == "6")
                selectedRestrictionsTemplate = "_restrictionsDetailExclusion.aspx"
            else if (selectedRestrictionType == "8")
                selectedRestrictionsTemplate = "_restrictionsDetailInsuranceLimit.aspx"
            else if (selectedRestrictionType == "5")
                selectedRestrictionsTemplate = "_restrictionsDetailExtraPremium.aspx"
            else
                selectedRestrictionsTemplate = ""

            if (selectedRestrictionsTemplate != "") {
                // Renders all the HTML related to the requirement detail     
                $("#Restriccion" + subgridId)
                    .append("<br><table id='" + subgridTableId + "' class='scroll'></table><div id='" + pagerId + "' class='scroll'></div>")
                    .load("controls/partials/" + selectedRestrictionsTemplate, function () {
                        if (selectedRestrictionType == "6") {
                            $("#txtTipo").val(objectRestriction.ExclusionTypeByLanguage);
                            if (objectRestriction.RateByLanguage == null || objectRestriction.RateByLanguage == 0)
                                $("#divTarifa").hide()
                            else
                                $("#txtTarifa").val(objectRestriction.RateByLanguage);
                            if (objectRestriction.ModuleByLanguage == null || objectRestriction.ModuleByLanguage.trim().length == 0)
                                $("#divModulo").hide()
                            else
                                $("#txtModulo").val(objectRestriction.ModuleByLanguage);
                            if (objectRestriction.CoverageByLanguage == null || objectRestriction.CoverageByLanguage.trim().length == 0)
                                $("#divCobertura").hide()
                            else
                                $("#txtCobertura").val(objectRestriction.CoverageByLanguage);
                            if (objectRestriction.CauseByLanguage == null || objectRestriction.CauseByLanguage.trim().length == 0)
                                $("#divCausa").hide()
                            else
                                $("#txtCausa").val(objectRestriction.CauseByLanguage);
                            if (objectRestriction.ExclusionPeriodTypeDescription == null || objectRestriction.ExclusionPeriodTypeDescription.trim().length == 0)
                                $("#divPeriodo").hide()
                            else
                                $("#txtPeriodo").val(objectRestriction.ExclusionPeriodTypeDescription);
                            if (objectRestriction.WaitingPeriodDays == null || objectRestriction.WaitingPeriodDays == 0 )
                                $("#divDias").hide()
                            else
                                $("#txtDias").val(objectRestriction.WaitingPeriodDays);
                            if (objectRestriction.WaitingPeriodMonths == null || objectRestriction.WaitingPeriodMonths == 0)
                                $("#divMeses").hide()
                            else
                                $("#txtMeses").val(objectRestriction.WaitingPeriodMonths);
                            if (objectRestriction.WaitingPeriodYears == null || objectRestriction.WaitingPeriodYears == 0 )
                                $("#divYear").hide()
                            else
                                $("#txtYear").val(objectRestriction.WaitingPeriodYears);
                            if (objectRestriction.ExcludedIllnessDescription == null || objectRestriction.ExcludedIllnessDescription.trim().length == 0)
                                $("#divEnfermedad").hide()
                            else
                                $("#txtEnfermedad").val(objectRestriction.ExcludedIllnessDescription)
                            if (objectRestriction.ExclusionClientID == null || objectRestriction.ExclusionClientID == -1|| selectedExclusionClientID.trim().length == 0 || selectedExclusionClientID == "")
                                $("#divAseguradoExcluir").hide()
                            else
                                $("#txtAseguradoExcluir").val(selectedExclusionClientID)
                        }
                        else if (selectedRestrictionType == "8") {
                            if (objectRestriction.ModuleByLanguage == null || objectRestriction.ModuleByLanguage.trim().length == 0)
                                $("#divModulo").hide();
                            else
                            $("#txtModulo").val(objectRestriction.ModuleByLanguage);
                            if (objectRestriction.CoverageByLanguage == null || objectRestriction.CoverageByLanguage.trim().length == 0)
                                $("#divCobertura").hide();
                            else
                                $("#txtCobertura").val(objectRestriction.CoverageByLanguage);
                            $("#txtMontoFijoAgregar").val(objectRestriction.MaximumInsuredAmount);
                        }
                        else if (selectedRestrictionType == "5") {
                            $("#txtDescription").val(objectRestriction.DiscountOrExtraPremiumDescription);
                            $("#txtTipo").val(objectRestriction.DiscountOrExtraPremiumTypeDescription);
                            $("#txtMoneda").val(objectRestriction.CurrencyCodeDescription);
                            if (objectRestriction.ExtraPremiumPercentage == null || objectRestriction.ExtraPremiumPercentage == 0)
                                $("#divFactor").hide();
                            else
                                $("#txtFactor").val(objectRestriction.ExtraPremiumPercentage);
                            if (objectRestriction.FlatExtraPremium == null || objectRestriction.FlatExtraPremium == 0)
                                $("#divMontoFijoAgregar").hide();
                            else
                                $("#txtMontoFijoAgregar").val(objectRestriction.FlatExtraPremium);
                            $("#txtPeriodo").val(objectRestriction.ExclusionPeriodTypeDescription);
                            if (objectRestriction.DurationOfFlatExtraPremiumDays == null || objectRestriction.DurationOfFlatExtraPremiumDays == 0 )
                                $("#divDias").hide()
                            else
                                $("#txtDias").val(objectRestriction.DurationOfFlatExtraPremiumDays);
                            if (objectRestriction.DurationOfFlatExtraPremiumMonths == null || objectRestriction.DurationOfFlatExtraPremiumMonths == 0)
                                $("#divMeses").hide()
                            else
                                $("#txtMeses").val(objectRestriction.DurationOfFlatExtraPremiumMonths);
                            if (objectRestriction.DurationOfFlatExtraPremiumYears == null || objectRestriction.DurationOfFlatExtraPremiumYears == 0 )
                                $("#divYear").hide()
                            else
                                $("#txtYear").val(objectRestriction.DurationOfFlatExtraPremiumYears);
                        }
                    });
            }
        },
    }));

    gridDecision.jqGrid('navGrid', '#pager-decision', {
        edit: false,
        add: false,
        del: false,
        search: false,
        refresh: false
    });
    gridDecision.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridDecision.trigger("reloadGrid");
}

function showModalInformation(value) {
    $('#textBody').html(value);
    $('#textTitle').html(explanationMessage);
    $('#myModal').modal('show');
}

function showModalCommentary(value) {
    $('#textBody').html(value);
    $('#textTitle').html(commentaryTitle);
    $('#myModal').modal('show');
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";

    ProxySyncRequirement.invoke("GetHeaderValuesDecision", "", function (data) { gridColNamesDecision = data.d; });
    gridColModelDecision = [
                            { name: 'CreationDate', index: 'CreationDate', resizable: true, editable: false, width: 100, sorttype: 'date', formatter: 'date', formatoptions: { newformat: 'd/m/Y h:i A' } },
                            { name: 'AlarmTypeEnumText', index: 'AlarmTypeEnumText', resizable: true, editable: false, width: 1, hidden: true },
                            { name: 'AlarmType', index: 'AlarmType', resizable: true, editable: false, hidden: true, width: 100 },
							{ name: 'AlarmTypeByLanguage', index: 'AlarmTypeByLanguage', resizable: true, editable: false, width: 100 },
                            { name: 'RequirementID', index: 'RequirementID', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'RequirementTypeEnumText', index: 'RequirementTypeEnumText', resizable: true, editable: false, width: 1, hidden: true },
							{ name: 'RequirementTypeByLanguage', index: 'RequirementTypeByLanguage', resizable: true, editable: false, width: 180 },
                            { name: 'QuestionId', index: 'QuestionId', resizable: true, editable: false, width: 1, hidden: true },
							{ name: 'QuestionTextByLanguage', index: 'QuestionTextByLanguage', resizable: true, editable: false, width: 180 },
                            { name: 'Answer', index: 'Answer', resizable: true, editable: false, width: 120 },
                            { name: 'EncodedExplanation', index: 'EncodedExplanation', hidden: false, editable: true, width: 70, align: 'center', formatter: function (cellvalue, options, rowobject) { return '<button type="button" class="btn btn-info btn-circle" onclick="showModalInformation(' + '\'' + cellvalue + '\'' + ');"><i class="fa fa-question"></i></button>'; }, edittype: "textarea" },
                            { name: 'UnderwritingAreaEnumText', index: 'UnderwritingAreaEnumText', resizable: true, editable: false, width: 1, hidden: true },
							{ name: 'UnderwritingAreaByLanguage', index: 'UnderwritingAreaByLanguage', resizable: true, editable: false, width: 120, hidden: true },
                            { name: 'TotalCredits', index: 'TotalCredits', resizable: true, editable: false, width: 85, hidden: true },
				 		    { name: 'TotalDebits', index: 'TotalDebits', resizable: true, editable: false, width: 85, hidden: true },
                            { name: 'Balance', index: 'Balance', resizable: true, editable: false, width: 85 },
                            { name: 'Commentary', index: 'Commentary', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'EncodedCommentary', index: 'EncodedCommentary', resizable: true, editable: false, hidden: false, width: 70, align: 'center', formatter: function (cellvalue, options, rowobject) { return '<button type="button" class="btn btn-info btn-circle" onclick="showModalCommentary(' + '\'' + cellvalue + '\'' + ');"><i class="fa fa-question"></i></button>'; }, edittype: "textarea" },
                            { name: 'Status', index: 'Status', resizable: true, editable: false, width: 1, hidden: true },
							{ name: 'StatusByLanguage', index: 'StatusByLanguage', resizable: true, editable: false, width: 100, hidden:true },
                            { name: 'UnderwritingRuleId', index: 'UnderwritingRuleId', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'ClientId', index: 'ClientId', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'ManualOrAutomaticRestriction', index: 'ManualOrAutomaticRestriction', resizable: true, editable: false, align: 'center', width: 60, hidden:true},
                            { name: 'ManualOrAutomaticRestriction', index: 'ManualOrAutomaticRestrictionTitle', resizable: true, editable: false, align: 'center', width: 60, formatter: function (cellvalue, options, rowobject) { if (cellvalue == 1) { return "<input type='checkbox' disabled checked='checked' />" } else { return "<input type='checkbox' disabled />" }; } },
                            { name: 'Explanation', index: 'Explanation', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'ExclusionClientDescription', index: 'ExclusionClientDescription', resizable: true, editable: false, hidden: true, width: 100 },
                            { name: 'CreatedByRestriction', index: 'CreatedByRestriction', resizable: true, editable: false, hidden: true, width: 100 }];
});