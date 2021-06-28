var token = "";
var gridColNamesRestrictions;
var gridColModelRestrictions;
var previousRowIdRestrictions = 0;
var gridRestrictions;
var selectedRestrictionsTemplate = "";
var numCaso;

function SetRestrictioninCache(idCaso, requirementId) {
	var dataJson = JSON.stringify({ caseId: idCaso, requirementID: requirementId });
    ProxySyncUnderwritingCase.invoke("SetCurrentRequirementID", dataJson, function (result) {
        return true;
    });
}


function ReloadRestrictionsGrid(editProperty) {
	// $.jgrid.gridUnload("grid-restrictions");
    gridRestrictions = $("#grid-restrictions");
    gridRestrictions.jqGrid("GridUnload");

	numCaso = $('#dpeCaseId_I').val();
    gridRestrictions.jqGrid($.extend({}, GeneralGridOptions, {
    	url: "services/Restriction.aspx/GetRestrictions",
    	serializeGridData: function (data) {
    		return JSON.stringify({ caseId: numCaso });
    		data = {};
    	},
        colNames: gridColNamesRestrictions,
        colModel: gridColModelRestrictions,
        pager: '#pager-restrictions',
        subGrid: true,
        viewrecords: false,
        rowNum: 15,
        pgbuttons: true,
        pgtext: "{0} of {1}",
        subGridRowExpanded: function (subgridId, rowId) {
            // we pass two parameters
            // subgrid_id is a id of the div tag created within a table data
            // the id of this element is a combination of the "sg_" + id of the row
            // the row_id is the id of the row
            // If we wan to pass additional parameters to the URL we can use
            // a method getRowData(row_id) - which returns associative array in type name-value
            // here we can easy construct the flowing
        	SetRestrictioninCache($('#dpeCaseId_I').val(), rowId);
            var subgridTableId, pagerId;
            subgridTableId = subgridId + "_t";
            pagerId = "p_" + subgridTableId;

            // Closes any other previous subGridRow
            if (previousRowIdRestrictions !== 0) {
                $(this).collapseSubGridRow(previousRowIdRestrictions);
            }
            // Saves the actual row_id
            previousRowIdRestrictions = rowId;

            var selectedRestrictionType = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_AlarmType]").text();
            var selectedModule = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_ModuleByLanguage]").text();
            var selectedCoverage = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_CoverageByLanguage]").text();
            var selectedImpairmentCodeDescription = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_ImpairmentCodeDescription]").text();
            var selectedWaitingPeriodDays = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_WaitingPeriodDays]").text();
            var selectedWaitingPeriodMonths = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_WaitingPeriodMonths]").text();
            var selectedWaitingPeriodYears = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_WaitingPeriodYears]").text();
            var selectedExclusionPeriodTypeEnumText = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_ExclusionPeriodTypeDescription]").text();
            var selectedCurrency = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_CurrencyCodeDescription]").text();
            var selectedCause = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_CauseByLanguage]").text();
            var selectedExclusionTypeByLanguage = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_ExclusionTypeByLanguage]").text();
            var selectedAlarmTypeDescription = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_AlarmTypeDescription]").text();
            var selectedFlatExtraPremium = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_FlatExtraPremium]").text();
            var selectedExtraPremiumPercentage = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_ExtraPremiumPercentage]").text();
            var selectedMaximumInsuredAmount = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_MaximumInsuredAmount]").text();
            var selectedRateByLanguage = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_RateByLanguage]").text();
            var selectedDiscountOrExtraPremiumTypeDescription = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_DiscountOrExtraPremiumTypeDescription]").text();            
            var selectedDiscountOrExtraPremiumDescription = $("tr#" + rowId + " > td[aria-describedby=grid-restrictions_DiscountOrExtraPremiumDescription]").text();
            
            if (selectedRestrictionType == "6")
                selectedRestrictionsTemplate = "_restrictionsDetailExclusion.aspx"
            else if (selectedRestrictionType == "8")
                selectedRestrictionsTemplate = "_restrictionsDetailInsuranceLimit.aspx"
            else if (selectedRestrictionType == "5")
                selectedRestrictionsTemplate = "_restrictionsDetailExtraPremium.aspx"

            if (selectedRestrictionsTemplate != "") {
                // Renders all the HTML related to the requirement detail     
                $("#" + subgridId)
                    .html("<table id='" + subgridTableId + "' class='scroll'></table><div id='" + pagerId + "' class='scroll'></div>")
                    .load("controls/partials/" + selectedRestrictionsTemplate, function () {

                        if (selectedRestrictionType == "6") {
                            $("#txtTipo").val(selectedExclusionTypeByLanguage);
                            $("#txtTarifa").val(selectedRateByLanguage);
                            $("#txtModulo").val(selectedModule);
                            $("#txtCobertura").val(selectedCoverage);
                            $("#txtCausa").val(selectedCause);
                            $("#txtPeriodo").val(selectedExclusionPeriodTypeEnumText);
                            $("#txtDias").val(selectedWaitingPeriodDays);
                            $("#txtMeses").val(selectedWaitingPeriodMonths);
                            $("#txtYear").val(selectedWaitingPeriodYears);
                            $("#txtEnfermedad").val(selectedImpairmentCodeDescription)
                        }
                        else if (selectedRestrictionType == "8") {
                            $("#txtModulo").val(selectedModule);
                            $("#txtCobertura").val(selectedCoverage);
                            $("#txtMoneda").val(selectedCurrency);
                            $("#txtMontoFijoAgregar").val(selectedMaximumInsuredAmount);
                        }
                        else if (selectedRestrictionType == "5") {
                            $("#txtDescription").val(selectedDiscountOrExtraPremiumDescription);
                            $("#txtTipo").val(selectedDiscountOrExtraPremiumTypeDescription);
                            $("#txtMoneda").val(selectedCurrency);
                            $("#txtFactor").val(selectedExtraPremiumPercentage);
                            $("#txtMontoFijoAgregar").val(selectedFlatExtraPremium);
                            $("#txtPeriodo").val(selectedExclusionPeriodTypeEnumText);
                            $("#txtDias").val(selectedWaitingPeriodDays);
                            $("#txtMeses").val(selectedWaitingPeriodMonths);
                            $("#txtYear").val(selectedWaitingPeriodYears);
                        }
                    });
            }
        },

    }));
    gridRestrictions.jqGrid('navGrid', '#pager-restrictions', {
        edit: false,
        add: false,
        del: false,
        search: false,                
        pgtext: null,
        viewrecords: false,
        refresh: false
    });
    gridRestrictions.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridRestrictions.trigger("reloadGrid");
}

/*---------------------------------------------------- On Dom ready -----------------------------------------------------*/
$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    ProxySyncRestrictions.invoke("GetHeaderValues", "", function (data) { gridColNamesRestrictions = data.d; });
    gridColModelRestrictions = [{ name: 'RequirementType', index: 'RequirementType', resizable: true, editable: false, width: 1, hidden: true },
                                { name: 'AlarmType', index: 'AlarmType', resizable: true, editable: false, width: 1, hidden: true },
								{ name: 'RequirementTypeByLanguage', index: 'RequirementTypeByLanguage', resizable: true, editable: false, width: 180 },
                                { name: 'AlarmTypeDescription', index: 'AlarmTypeDescription', resizable: true, editable: false, width: 1, width: 180 },
                                { name: 'FlatExtraPremium', index: 'FlatExtraPremium', resizable: true, editable: false, width: 140, hidden: true },
                                { name: 'DurationOfFlatExtraPremiumYears', index: 'DurationOfFlatExtraPremiumYears', resizable: true, editable: false, width: 140, hidden: true },
                                { name: 'DurationOfFlatExtraPremiumMonths', index: 'DurationOfFlatExtraPremiumMonths', resizable: true, editable: false, width: 140, hidden: true },
                                { name: 'DurationOfFlatExtraPremiumDays', index: 'DurationOfFlatExtraPremiumDays', resizable: true, editable: false, width: 140, hidden: true },
                                { name: 'DiscountOrExtraPremiumDescription', index: 'Recharge', resizable: true, editable: false, width: 120 },
                                { name: 'ExclusionTypeEnumText', index: 'ExclusionTypeEnumText', resizable: true, editable: false, width: 1, hidden: true },
								{ name: 'ExclusionTypeByLanguage', index: 'ExclusionTypeByLanguage', resizable: true, editable: false, width: 180 },
						        { name: 'ModuleByLanguage', index: 'ModuleByLanguage', resizable: true, editable: false, width: 220 },
						        { name: 'CoverageByLanguage', index: 'CoverageByLanguage', resizable: true, editable: false, width: 120 },
						        { name: 'ImpairmentCodeDescription', index: 'ImpairmentCodeDescription', resizable: true, editable: false },
                                { name: 'CurrencyCodeDescription', index: 'CurrencyCodeDescription', resizable: true, editable: false, hidden:true },
                                { name: 'WaitingPeriodDays', index: 'WaitingPeriodDays', resizable: true, editable: false, hidden: true },
                                { name: 'WaitingPeriodMonths', index: 'WaitingPeriodMonths', resizable: true, editable: false, hidden: true },
                                { name: 'WaitingPeriodYears', index: 'WaitingPeriodYears', resizable: true, editable: false, hidden: true },
                                { name: 'ExclusionPeriodTypeDescription', index: 'ExclusionPeriodTypeDescription', resizable: true, editable: false, hidden: true },
                                { name: 'CauseByLanguage', index: 'CauseByLanguage', resizable: true, editable: false, hidden: true },
                                { name: 'RateByLanguage', index: 'RateByLanguage', resizable: true, editable: false, hidden: true },
                                { name: 'MaximumInsuredAmount', index: 'MaximumInsuredAmount', resizable: true, editable: false, hidden: true },                               
                                { name: 'ExtraPremiumPercentage', index: 'ExtraPremiumPercentage', resizable: true, editable: false, width: 140, hidden: true },
                                { name: 'DiscountOrExtraPremiumTypeDescription', index: 'DiscountOrExtraPremiumTypeDescription', resizable: true, editable: false, width: 140, hidden: true }];
    
});