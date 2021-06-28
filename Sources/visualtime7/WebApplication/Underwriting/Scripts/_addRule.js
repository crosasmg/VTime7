var gridAlarm;
var gridRestriction;
var gridColNamesAlarm = "";
var gridColModelAlarm = "";
var AlarmType;
var ruleAnswer;
var DiscountTypeExtend;
var gridColNamesRestriction;
var gridColModelRestriction;
var allCoverageType;
var coverageType;
var UnderId;
var editMode;
var validationText;
var clientExclude;
var clientExcludeIllness;
var exclusionIllnessInsuredDescription;
//Saves the previous open row on the jqgrid
var previousRowIdRole = 0;

//Parte superior del form
$(document).on('input', '#rangoPuntos', function () {
    $('#lblPuntos').html($(this).val());
});

$("#txtEnfermedad").on('input', function (e) {
    if ($(this).val().length > 0) {
        var paramData = JSON.stringify({ filter: "%" + $(this).val() + "%" });
        var illness = [];
        ProxySyncLookUps.invoke("GetAllIllnessTypeLkp", paramData, function (data) {
            $.each(data.d, function (index, item) {
                illness.push(item.Code + " | " + item.Description);
            });
        });
        $('#txtEnfermedad').autocomplete({
            source: illness,
            minLength: 0,
            scroll: true
        }).focus(function () {
            $(this).autocomplete("search", "");
        });
        $("#ddlNivelEnfermedad").removeProp('disabled');
    } else {
        $("#ddlNivelEnfermedad").val(-1);
        $("#ddlNivelEnfermedad").prop('disabled', true);
    }
});

$("#txtPregunta").on('input', function (e) {
    if ($(this).val().length > 0) {
        var paramData = JSON.stringify({ filter: "%" + $(this).val() + "%" });
        var question = [];
        ProxySyncLookUps.invoke("GetQuestionsFromRequirementFilterLkp", paramData, function (data) {
            $.each(data.d, function (index, item) {
                question.push(item.Code + " | " + item.Description);
            });
        });
        $('#txtPregunta').autocomplete({
            source: question,
            minLength: 0,
            scroll: true
        }).focus(function () {
            $(this).autocomplete("search", "");
        });
    }
});

$("#txtEnfermedad").blur(function () {
    if ($("#txtEnfermedad").val().indexOf('|') >= 0) {
        $("#ddlNivelEnfermedad").removeProp('disabled');
    } else {
        $("#txtEnfermedad").val('');
        $("#ddlNivelEnfermedad").prop('disabled', true);
    }
});

$("#txtPregunta").blur(function () {
    if ($("#txtPregunta").val().indexOf('|') < 0)
        $("#txtPregunta").val('');
});


function GetSelectedColumn(grid, column) {
    var sel_id = grid.jqGrid('getGridParam', 'selrow');
    var value = grid.jqGrid('getCell', sel_id, column);
    return value;
}

function ReloadAlarmGrid(isEditMode, UnderRuleId, Answer) {
    // $.jgrid.gridUnload("grid-alarm");
    $("#grid-alarm").jqGrid("GridUnload");

    UnderId = UnderRuleId;
    editMode = isEditMode;
    InitControlGeneralInformation();
    ProxySyncUnderwritingRule.invoke("GetListValidationText", "", function (data) { validationText = data.d });

    if (typeof UnderId === "undefined") {
        var param = JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
        ProxySyncUnderwritingRule.invoke("CleanAllAlarm", param, function () { });
        LoadDefaultValues("ddlReqAreaDeSuscripcion", "GetUnderwritingAreaTypeActive", ProxyAsyncLookUps).done(function () { });
        LoadDefaultValues("ddlNivelEnfermedad", "GetAllDegreeLkp", ProxyAsyncLookUps).done(function () { });
        ProxyAsyncUnderwritingRule.invoke("GetUnderwriter", "", function (data) { $("#txtCreadoPor").val(data.d) });
        LoadDefaultValues("ddlClientId", "GetClientByUnderwritingCase", ProxyAsyncLookUps).done(function () { });
    } else {
        LoadDefaultValues("ddlReqAreaDeSuscripcion", "GetUnderwritingAreaTypeActive", ProxySyncLookUps).done(function () { });
        LoadDefaultValues("ddlNivelEnfermedad", "GetAllDegreeLkp", ProxySyncLookUps).done(function () { });
        LoadDefaultValues("ddlClientId", "GetClientByUnderwritingCase", ProxySyncLookUps).done(function () { });
        var paramData = JSON.stringify({
            caseId: $('#dpeCaseId_I').val(),
            requirementId: $("#txtRequirementID").val(),
            UnderRuleId: UnderId
        });
        ruleAnswer = Answer
        ProxySyncUnderwritingRule.invoke("GetUnderwritingRulesByUnderRuleId", paramData, function (data) { LoadFieldEditForm(data.d); });
    }
    if (!isEditMode)
        DisabledField();

    ///////////////////**************  Edit, Delete, Add and Update  ****************////////////////////////
    var addSettingsAlarm = $.extend({}, GeneralAddSettings, {
        width: 400,
        serializeEditData: function (postData) {
            var AlarmTypeDesc;
            if (AlarmType.indexOf(postData.AlarmType) > -1) {
                AlarmTypeDesc = AlarmType.substr(AlarmType.indexOf(postData.AlarmType) + 2)
                if (AlarmTypeDesc.indexOf(';') > 0)
                    AlarmTypeDesc = AlarmTypeDesc.substr(0, AlarmTypeDesc.indexOf(';'))
            }
            return JSON.stringify({
                caseId: $('#dpeCaseId_I').val(),
                AlarmType: postData.AlarmType,
                AlarmTypeDescription: AlarmTypeDesc
            });
        },
        afterShowForm: function (form) {
            var modal = form.parents(".ui-jqdialog");

            modal.css("position", "fixed");
            modal.css("top", 180);
            if (isExplorer()) {
                modal.css("left", (window.innerWidth - modal.width()) / 2);
            } else {
                modal.css("left", (window.innerWidth - modal.width()) / 6);
            }
        },
        afterSubmit: function (response, postdata) {
            if (response.responseJSON.d.length > 0) {
                $loading.hide();
                return [false, response.responseJSON.d];
            }
            $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
            return [true];
        },
        url: "services/UnderwritingRule.aspx/AddAlarm"
    });
    var delSettingsAlarm = $.extend({}, GeneralDelSettings, {
        serializeDelData: function (postData) {
            return JSON.stringify({ caseId: $('#dpeCaseId_I').val(), requirementId: $("#txtRequirementID").val(), alarmType: GetSelectedColumn(gridAlarm, "AlarmType") });
        },
        afterSubmit: function (response, postdata) {
            $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
            // $.jgrid.gridUnload("grid-restriction");
            $("#grid-restriction").jqGrid("GridUnload");

            return [true, "", ""]
        },
        url: "services/UnderwritingRule.aspx/RemoveAlarm"
    });

    ///////////////////**************  Grid definition  ****************////////////////////////
    gridAlarm = $("#grid-alarm");
    gridAlarm.jqGrid($.extend({}, GeneralGridOptions, {
        url: "services/UnderwritingRule.aspx/GetAllAlarm",
        serializeGridData: function (data) {
            return JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
            data = {};
        },
        subGridRowExpanded: function (subgridId, rowId) {
            if (false) {
                var subgridTableId, pagerId;
                subgridTableId = subgridId + "_t";
                pagerId = "p_" + subgridTableId;

                if (previousRowIdRole !== 0) {
                    $(this).collapseSubGridRow(previousRowIdRole);
                }

            }
        },
        colNames: gridColNamesAlarm,
        colModel: gridColModelAlarm,
        pager: '#pager-alarm',
        subGrid: false,
        viewrecords: false,
        pgbuttons: false,
        onSelectRow: function (id) {
            var dataFromTheRow = gridAlarm.jqGrid('getRowData', id);
            $('#tabLimites').addClass('hide');
            $('#tabRecargos').addClass('hide');
            $('#tabExclusion').addClass('hide');
            if (dataFromTheRow.AlarmType == 5) { //Recargos
                $('#tabRecargos').removeClass('hide');
                ReloadRestrictionGrid(dataFromTheRow.AlarmType);
            }
            else if (dataFromTheRow.AlarmType == 6) { //Exclusion
                $('#tabExclusion').removeClass('hide');
                ReloadRestrictionGrid(dataFromTheRow.AlarmType);
            }
            else if (dataFromTheRow.AlarmType == 8) { //Limite suma asegurada
                $('#tabLimites').removeClass('hide');
                ReloadRestrictionGrid(dataFromTheRow.AlarmType);
            } else {
                // $.jgrid.gridUnload("grid-restriction");
                $("#grid-restriction").jqGrid("GridUnload");
            }
        }
    }));

    gridAlarm.jqGrid(
		"navGrid",
		"#pager-alarm",
		{
		    edit: false,
		    add: isEditMode,
		    del: isEditMode,
		    search: false,
		    rowList: [],
		    pgbuttons: false,
		    pgtext: null,
		    viewrecords: false,
		    refresh: false
		},
		{},
		addSettingsAlarm,
		delSettingsAlarm
	);

    gridAlarm.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridAlarm.trigger("reloadGrid");

}

function InitControlGeneralInformation() {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";



    ProxySyncLookUps.invoke("GetAlarmType", "", function (data) {
        AlarmType = "";
        $.each(data.d, function (index, item) {
            if (item.Code === "5" || item.Code === "6" || item.Code === "8") {
                AlarmType = AlarmType + item.Code + ":" + item.Description + ";";
            }
        });
        AlarmType = AlarmType.slice(0, -1);
    });

    ProxySyncUnderwritingRule.invoke("GetHeaderAlarmValues", JSON.stringify({ editMode: editMode }), function (data) { gridColNamesAlarm = data.d; });

    gridColModelAlarm = [{ name: 'AlarmType', index: 'AlarmType', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: AlarmType, defaultValue: "1", } },
                        { name: 'AlarmTypeDescription', index: 'AlarmTypeDescription', resizable: true, editable: false, hidden: false, search: false }];

}

function CleanAndExit() {
    var param = JSON.stringify({ caseId: $('#dpeCaseId_I').val() });
    ProxySyncUnderwritingRule.invoke("CleanAllAlarm", param, function () { });
    $('#addRule').modal('toggle');
}

//// RESTRICCIONES
function ReloadRestrictionGrid(typeAlarm) {
    // $.jgrid.gridUnload("grid-restriction");
    $("#grid-restriction").jqGrid("GridUnload");

    InitControlRestriction(typeAlarm);

    var addSettingsRestriction = LoadAddGridSettings(typeAlarm);

    var editSettingsRestriction = LoadEditGridSettings(typeAlarm);

    var delSettingsRestriction = $.extend({}, GeneralDelSettings, {
        serializeDelData: function (postData) {
            return JSON.stringify({ caseId: $('#dpeCaseId_I').val(), requirementId: $("#txtRequirementID").val(), alarmType: typeAlarm, restrictionId: GetSelectedColumn(gridRestriction, "RestrictionId") });
        },
        url: "services/UnderwritingRule.aspx/DeleteRestriction"
    });

    ///////////////////**************  Grid definition  ****************////////////////////////
    gridRestriction = $("#grid-restriction");
    gridRestriction.jqGrid($.extend({}, GeneralGridOptions, {
        url: "services/UnderwritingRule.aspx/GetAllRestriction",
        serializeGridData: function (data) {
            return JSON.stringify({ caseId: $('#dpeCaseId_I').val(), AlarmType: typeAlarm });
        },
        subGridRowExpanded: function (subgridId, rowId) {
            if (false) {
                var subgridTableId, pagerId;
                subgridTableId = subgridId + "_t";
                pagerId = "p_" + subgridTableId;

                if (previousRowIdRole !== 0) {
                    $(this).collapseSubGridRow(previousRowIdRole);
                }

            }
        },
        colNames: gridColNamesRestriction,
        colModel: gridColModelRestriction,
        pager: '#pager-restriction',
        subGrid: false,
        viewrecords: false,
        pgbuttons: false,
    }));
    gridRestriction.jqGrid(
		"navGrid",
		"#pager-restriction",
		{
		    edit: editMode,
		    add: editMode,
		    del: editMode,
		    search: false,
		    rowList: [],
		    pgbuttons: false,
		    pgtext: null,
		    viewrecords: false,
		    refresh: false
		},
		editSettingsRestriction,
		addSettingsRestriction,
		delSettingsRestriction
	);

    gridRestriction.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });
    gridRestriction.trigger("reloadGrid");

}


function InitControlRestriction(typeAlarm) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    "use strict";
    var result;
    var editModeClient = typeof UnderId === "undefined"? false : true;
    ProxySyncUnderwritingRule.invoke("GetHeaderRestrictionValues", "{ AlarmType: " + typeAlarm + " }", function (data) { gridColNamesRestriction = data.d; });
    ProxySyncLookUps.invoke("GetAllCoverageByRiskInformation", "{ uwCaseId: " + $('#dpeCaseId_I').val() + "}", function (data) { allCoverageType = data.d; });
    ProxySyncLookUps.invoke("GetCoverageByRiskInformation", "{ uwCaseId: " + $('#dpeCaseId_I').val() + "}", function (data) { coverageType = data.d; });
    ProxySyncLookUps.invoke("GetClientForExclusionLkp", "{ editMode: " + editModeClient + ", isExcludeInsured: true }", function (data) { clientExclude = data.d; });
    ProxySyncLookUps.invoke("GetClientForExclusionLkp", "{ editMode: false, isExcludeInsured: false }", function (data) { clientExcludeIllness = data.d; });

    var CoverageNotRequired = LookUpsByObjectWithInitValue(coverageType);
    var CoverageTypeSelect = LookUpsByObjectWithInitValue(allCoverageType);

    if (typeAlarm == 5) {
        var ExclusionPeriodType = GetLookUpsWithInitValue("GetAllExclusionPeriodType");
        var DiscountType = GetLookUpsWithInitValue("GetAllDiscountOrExtraPremiumByProductLkp");
        var ModuleLkp = GetLookUpsWithInitValue("GetModuleByRiskInformation");
        ProxyAsyncLookUps.invoke("GetAllDiscountOrExtraPremiumInfoByProductsLkp", "", function (data) { DiscountTypeExtend = data.d; });

        gridColModelRestriction = [{ name: 'RestrictionId', index: 'RestrictionId', resizable: true, editable: true, hidden: true },
								{ name: 'Discountorextrapremiumcode', index: 'Discountorextrapremiumcode', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: DiscountType, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { LoadFieldByChange(e); } }] } },
								{ name: 'DiscountOrExtraPremiumDescription', index: 'DiscountOrExtraPremiumDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'DiscountorExtraPremiumType', index: 'DiscountorExtraPremiumType', resizable: true, editable: true, hidden: true, editoptions: { disabled: "true" } },
								{ name: 'DiscountorExtraPremiumTypeDescription', index: 'DiscountorExtraPremiumTypeDescription', resizable: true, editable: true, hidden: false, editoptions: { disabled: "true" }, search: false },
								{ name: 'CurrencyCode', index: 'CurrencyCode', resizable: true, editable: true, hidden: true, editoptions: { disabled: "true" } },
								{ name: 'CurrencyCodeDescription', index: 'CurrencyCodeDescription', resizable: true, editable: true, hidden: false, editoptions: { disabled: "true" }, search: false },
								{ name: 'ExtraPremiumPercentage', index: 'ExtraPremiumPercentage', resizable: true, editable: true, hidden: false, editrules: { required: false }, editoptions: { dataInit: function (element) { DecimalMask(element) }, maxlength: 18 }, search: false },
								{ name: 'FlatExtraPremium', index: 'FlatExtraPremium', resizable: true, editable: true, hidden: false, editrules: { required: false }, editoptions: { dataInit: function (element) { DecimalMask(element) }, maxlength: 18 }, search: false },
                                {
                                    name: 'XPremiumDiscountOnlyInsured', index: 'XPremiumDiscountOnlyInsured', align: "center", width: "250", resizable: true, editable: true, hidden: false, search: false, edittype: 'checkbox',
                                    editoptions: {
                                        value: "true:false",
                                        dataEvents: [{
                                            type: "change",
                                            fn: function (e) {
                                                if ($(this).is(':checked')) {
                                                    ChangeExtraPremiumApply(true);
                                                } else {
                                                    ChangeExtraPremiumApply(false);
                                                }
                                            }
                                        }]
                                    }, formatter: function (cellvalue, options, rowobject) { if (cellvalue == 1 || cellvalue == "1") { return "<input id='XPremiumDiscountOnlyInsuredCheck' type='checkbox' disabled checked='checked' />" } else { return "<input id='XPremiumDiscountOnlyInsuredCheck' type='checkbox' disabled />" }; }
                                },
                                { name: 'ProductModule', index: 'ProductModule', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: ModuleLkp, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { LoadAllCoverageByModule(e); } }], disabled: "true" } },
								{ name: 'ProductModuleDescription', index: 'ProductModuleDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'CoverageCode', index: 'CoverageCode', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: CoverageTypeSelect, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { } }], disabled: "true" } },
								{ name: 'CoverageDescription', index: 'CoverageDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'ExclusionPeriodType', index: 'ExclusionPeriodType', hidden: true, editable: true, editrules: { required: false, edithidden: true }, edittype: 'select', editoptions: { value: ExclusionPeriodType, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { DisabledFieldChange(typeAlarm, UnderId); } }], disabled: "true" } },
								{ name: 'ExclusionPeriodTypeDescription', index: 'ExclusionPeriodTypeDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'DOfFlatExtraPremiumDays', index: 'DOfFlatExtraPremiumDays', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'DOfFlatExtraPremiumMonths', index: 'DOfFlatExtraPremiumMonths', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'DOfFlatExtraPremiumYears', index: 'DOfFlatExtraPremiumYears', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'IsNew', index: 'IsNew', resizable: true, editable: true, hidden: true }];
    } else if (typeAlarm == 6) {

        var RatingTable = GetLookUpsRating("GetAllRatingByProductLkp");
        var ExclusionType = GetLookUpsWithInitValue("GetAllExclusionType");
        var ExclusionPeriodType = GetLookUpsWithInitValue("GetAllExclusionPeriodType");
        var ModuleLkp = GetLookUpsWithInitValue("GetModuleByRiskInformation");
        var CauseLkp = GetLookUpsWithInitValue("GetReasonForExclusionOfIllnessLkp");
        ProxyAsyncUnderwritingRule.invoke("GetHeaderExclusionIllnessInsured", "", function (data) { exclusionIllnessInsuredDescription = data.d; });

        gridColModelRestriction = [{ name: 'RestrictionId', index: 'RestrictionId', resizable: true, editable: true, hidden: true },
								{ name: 'ExclusionType', index: 'ExclusionType', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: ExclusionType, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { ExclusionValueChange(e); } }] } },
								{ name: 'ExclusionTypeDescription', index: 'ExclusionTypeDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'RatingTable', index: 'RatingTable', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: RatingTable, defaultValue: "-1", disabled: "true" } },
								{ name: 'RatingTableDescription', index: 'RatingTableDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'ProductModule', index: 'ProductModule', hidden: true, editable: true, editrules: { required: false, edithidden: true }, edittype: 'select', editoptions: { value: ModuleLkp, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { LoadAllCoverageByModule(e); } }], disabled: "true" } },
								{ name: 'ProductModuleDescription', index: 'ProductModuleDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'CoverageCode', index: 'CoverageCode', hidden: true, editable: true, editrules: { required: false, edithidden: true }, edittype: 'select', editoptions: { value: CoverageNotRequired, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { } }], disabled: "true" } },
								{ name: 'CoverageDescription', index: 'CoverageDescription', resizable: true, editable: true, hidden: false, search: false },
								{ name: 'ExclusionClientID', index: 'ExclusionClientID', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: LookUpsByObjectWithInitValue(clientExclude), defaultValue: "-1", disabled: "true" } },
								{ name: 'ExclusionClientName', index: 'ExclusionClientName', resizable: true, editable: true, hidden: false, search: false },
								{ name: 'ImpairmentCode', index: 'ImpairmentCode', hidden: true, editable: true, editrules: { edithidden: true }, editoptions: { dataEvents: [{ type: 'keyup', fn: function (e) { ImpairmentCodeChange(e); } }, { type: 'change', fn: function () { ValidImpairmentCode(); } }], disabled: "true" } },
								{ name: 'ImpairmentCodeDescription', index: 'ImpairmentCodeDescription', resizable: true, editable: true, hidden: false, search: false },
								{ name: 'Cause', index: 'Cause', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: CauseLkp, defaultValue: "-1", disabled: "true" } },
								{ name: 'CauseDescription', index: 'CauseDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'ExclusionPeriodType', index: 'ExclusionPeriodType', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: ExclusionPeriodType, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { DisabledFieldChange(typeAlarm, UnderId); } }] } },
								{ name: 'ExclusionPeriodTypeDescription', index: 'ExclusionPeriodTypeDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'WaitingPeriodDays', index: 'WaitingPeriodDays', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'WaitingPeriodMonths', index: 'WaitingPeriodMonths', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'WaitingPeriodYears', index: 'WaitingPeriodYears', resizable: true, editable: true, hidden: false, editoptions: { dataInit: function (element) { DecimalMask(element) }, disabled: "true", maxlength: 3 }, search: false },
								{ name: 'IsNew', index: 'IsNew', resizable: true, editable: true, hidden: true }];
    } else if (typeAlarm == 8) {

        var ModuleLkp = GetLookUpsWithInitValue("GetModuleByRiskInformation");

        gridColModelRestriction = [{ name: 'RestrictionId', index: 'RestrictionId', resizable: true, editable: true, hidden: true },
								{ name: 'ProductModule', index: 'ProductModule', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: ModuleLkp, defaultValue: "-1", dataEvents: [{ type: 'change', fn: function (e) { LoadAllCoverageByModule(e); } }] } },
								{ name: 'ProductModuleDescription', index: 'ProductModuleDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'CoverageCode', index: 'CoverageCode', hidden: true, editable: true, editrules: { required: true, edithidden: true }, edittype: 'select', editoptions: { value: CoverageTypeSelect, defaultValue: "-1"} },
								{ name: 'CoverageDescription', index: 'CoverageDescription', resizable: true, editable: false, hidden: false, search: false },
								{ name: 'MaximumInsuredAmount', index: 'MaximumInsuredAmount', resizable: true, editable: true, hidden: false, editrules: { required: true }, editoptions: { dataInit: function (element) { DecimalMask(element) }, maxlength: 18 }, search: false },
								{ name: 'IsNew', index: 'IsNew', resizable: true, editable: true, hidden: true }];
    }
}

function ImpairmentCodeChange(e) {
    if ($('#ImpairmentCode').val().length > 0) {
        var paramData = JSON.stringify({ filter: "%" + $('#ImpairmentCode').val() + "%" });
        var illness = [];
        ProxySyncLookUps.invoke("GetIllnessByFilterAndProductLkp", paramData, function (data) {
            $.each(data.d, function (index, item) {
                illness.push(item.Code + " | " + item.Description);
            });
        });
        $('#ImpairmentCode').autocomplete({
            source: illness,
            minLength: 0,
            scroll: true
        }).focus(function () {
            $(this).autocomplete("search", "");
        });
    }
}

function ChangeExtraPremiumApply(valor) {
    if ($("#XPremiumDiscountOnlyInsured").prop('checked') == false) {
        $("#ExclusionPeriodType").val('-1');
        $("#ExclusionPeriodTypeDescription").val('');
        $("#ProductModule").val('-1');
        $("#ProductModuleDescription").val('');
        $("#CoverageCode").val('-1');
        $("#CoverageDescription").val('');
        $("#DOfFlatExtraPremiumDays").val('');
        $("#DOfFlatExtraPremiumMonths").val('');
        $("#DOfFlatExtraPremiumYears").val('');
        $("#ExclusionPeriodType").prop('disabled', true);
        $("#ProductModule").prop('disabled', true);
        $("#CoverageCode").prop('disabled', true);
        $("#DOfFlatExtraPremiumDays").prop('disabled', true);
        $("#DOfFlatExtraPremiumMonths").prop('disabled', true);
        $("#DOfFlatExtraPremiumYears").prop('disabled', true);
        $("#tr_ExclusionPeriodType").hide();
        $("#tr_DOfFlatExtraPremiumDays").hide();
        $("#tr_DOfFlatExtraPremiumMonths").hide();
        $("#tr_DOfFlatExtraPremiumYears").hide();
        $("#tr_ProductModule").hide();
        $("#tr_CoverageCode").hide();
    } else {
        $("#tr_ExclusionPeriodType").show();
        $("#tr_DOfFlatExtraPremiumDays").show();
        $("#tr_DOfFlatExtraPremiumMonths").show();
        $("#tr_DOfFlatExtraPremiumYears").show();
        $("#tr_ProductModule").show();
        $("#tr_CoverageCode").show();
        $("#ExclusionPeriodType").val('-1');
        $("#ExclusionPeriodTypeDescription").val('');
        $("#ProductModule").val('-1');
        $("#ProductModuleDescription").val('');
        $("#CoverageCode").val('-1');
        $("#CoverageDescription").val('');
        $("#DOfFlatExtraPremiumDays").val('0');
        $("#DOfFlatExtraPremiumMonths").val('0');
        $("#DOfFlatExtraPremiumYears").val('0');
        $("#ExclusionPeriodType").removeAttr('disabled');
        $("#ProductModule").removeAttr('disabled');
        $("#CoverageCode").removeAttr('disabled');
        $("#DOfFlatExtraPremiumDays").removeAttr('disabled');
        $("#DOfFlatExtraPremiumMonths").removeAttr('disabled');
        $("#DOfFlatExtraPremiumYears").removeAttr('disabled');
    }
}

function ValidImpairmentCode() {
    if ($("#ImpairmentCode").val().indexOf('|') < 0)
        $("#ImpairmentCode").val('');
}

function LoadFieldByChange(e) {
    var value = $.grep(DiscountTypeExtend, function (n, i) {
        return n.ExtraPremiumDiscountOrTaxCode == $("#Discountorextrapremiumcode option:selected").val();
    });
    if (value.length !== 0) {
        $("#DiscountorExtraPremiumTypeDescription").val(value[0].TypeOfItemDescription);
        $("#CurrencyCodeDescription").val(value[0].CurrencyDescription);
        $("#CurrencyCode").val(value[0].Currency);
        $("#DiscountorExtraPremiumType").val(value[0].TypeOfItem);
    } else {
        $("#DiscountorExtraPremiumTypeDescription").val('');
        $("#CurrencyCodeDescription").val('');
        $("#CurrencyCode").val('');
        $("#DiscountorExtraPremiumType").val('');
    }
}

function DisabledFieldChange(typeAlarm, UnderRuleId) {
    if (typeAlarm == 5) {
        if ($("#ExclusionPeriodType option:selected").val() == 2) {
            $("#DOfFlatExtraPremiumDays").removeProp('disabled');
            $("#DOfFlatExtraPremiumMonths").removeProp('disabled');
            $("#DOfFlatExtraPremiumYears").removeProp('disabled');
        } else {
            $("#DOfFlatExtraPremiumDays").val('');
            $("#DOfFlatExtraPremiumMonths").val('');
            $("#DOfFlatExtraPremiumYears").val('');
            $("#DOfFlatExtraPremiumDays").prop('disabled', true);
            $("#DOfFlatExtraPremiumMonths").prop('disabled', true);
            $("#DOfFlatExtraPremiumYears").prop('disabled', true);
        }
        if (typeof UnderRuleId !== "undefined" && $("#IsNew").val() == 'false') {
            $("#Discountorextrapremiumcode").prop('disabled', true);
            $("#XPremiumDiscountOnlyInsured").prop('disabled', true);
            if ($("#XPremiumDiscountOnlyInsuredCheck").prop('checked') == true) {
                $("#ExclusionPeriodType").removeProp('disabled');
                $("#XPremiumDiscountOnlyInsured").prop('checked', true);
            }
        } else {
            if ($("#XPremiumDiscountOnlyInsured").prop('checked') == true || $("#XPremiumDiscountOnlyInsuredCheck").prop('checked') == true) {
                $("#XPremiumDiscountOnlyInsured").prop('checked', true);
                $("#ProductModule").removeProp('disabled');
                $("#CoverageCode").removeProp('disabled');
                $("#ExclusionPeriodType").removeProp('disabled');
            } 
        }
    } else if (typeAlarm == 6) {
        if ($("#ExclusionPeriodType option:selected").val() == 2) {
            $("#WaitingPeriodDays").removeProp('disabled');
            $("#WaitingPeriodMonths").removeProp('disabled');
            $("#WaitingPeriodYears").removeProp('disabled');
        } else {
            $("#WaitingPeriodDays").val('');
            $("#WaitingPeriodMonths").val('');
            $("#WaitingPeriodYears").val('');
            $("#WaitingPeriodDays").prop('disabled', true);
            $("#WaitingPeriodMonths").prop('disabled', true);
            $("#WaitingPeriodYears").prop('disabled', true);
        }
        if (typeof UnderRuleId !== "undefined" && $("#IsNew").val() == 'false') {
            $("#ExclusionType").prop('disabled', true);
            if ($("#ExclusionType option:selected").val() == 1) {
                $("#ImpairmentCode").prop('disabled', true);
            } else if ($("#ExclusionType option:selected").val() == 2) {
                $("#ProductModule").prop('disabled', true);
                $("#CoverageCode").prop('disabled', true);
            } else if ($("#ExclusionType option:selected").val() == 3) {
                $("#ProductModule").prop('disabled', true);
                $("#CoverageCode").prop('disabled', true);
                $("#ImpairmentCode").prop('disabled', true);
            } else if ($("#ExclusionType option:selected").val() == 4) {
                $("#ImpairmentCode").prop('disabled', true);
                $("#RatingTable").prop('disabled', true);
            }
        }
    } else {
        if (typeof UnderRuleId !== "undefined" && $("#IsNew").val() == 'false') {
            $("#ProductModule").prop('disabled', true);
            $("#CoverageCode").prop('disabled', true);
        }
    }
}

function LoadAllCoverageByModule(e) {
    if ($("#ProductModule option").size() > 1) {   
        $('#CoverageCode').empty();
        $('#CoverageCode').append($('<option>', { value: -1, text: '' }));
        var value = $.grep(allCoverageType, function (n, i) {
            return n.Module == $("#ProductModule option:selected").val();
        });
        $.each(value, function (i, item) {
            $('#CoverageCode').append($('<option>', {
                value: item.Code,
                text: item.Description
            }));
        });
    }
}

function LoadCoverageByValue(coverageValue) {
    $('#CoverageCode').empty();
    $('#CoverageCode').append($('<option>', { value: -1, text: '' }));
    var value = $.grep(allCoverageType, function (n, i) {
        return n.Module == $("#ProductModule option:selected").val();
    });
    $.each(value, function (i, item) {
        $('#CoverageCode').append($('<option>', {
            value: item.Code,
            text: item.Description
        }));
    });
    $('#CoverageCode').val(coverageValue);
}

function ExclusionValueChange(e) {
    if ($("#ExclusionType option:selected").val() == 1) { //Excluir enfermedad
        $("#RatingTable").removeProp('disabled');
        if ($("#RatingTable option").size() == 1) {
            $('#tr_RatingTable').hide();
        } else {
            $('#tr_RatingTable').show();
        }
        $("#ProductModule").removeProp('disabled');
        $("#CoverageCode").removeProp('disabled');
        $("#ExclusionClientID").removeProp('disabled');
        $('#tr_ExclusionClientID').show();
        $('#tr_ExclusionClientID').find('td.CaptionTD').html(exclusionIllnessInsuredDescription);
        $('#tr_CoverageCode').show();
        $('#tr_ProductModule').show();
        $('#tr_ExclusionClientID').show();
        $("#tr_ImpairmentCode").show();
        $("#tr_ExclusionPeriodType").show();
        $("#tr_Cause").show();
        $("#tr_WaitingPeriodDays").show();
        $("#tr_WaitingPeriodMonths").show();
        $("#tr_WaitingPeriodYears").show();
        $("#RatingTable").val('-1');
        $("#ProductModule").val('-1');
        $("#CoverageCode").empty();
        if ($("#ProductModule option").size() == 1) {
            $('#CoverageCode').append($('<option>', { value: -1, text: '' }));
            $.each(allCoverageType, function (key, value) {
                $('#CoverageCode').append($('<option>', {
                    value: value.Code,
                    text: value.Description
                }));
            });
        }
        $('#ExclusionClientID').empty();
        $('#ExclusionClientID').append($('<option>', { value: -1, text: '' }));
        $.each(clientExcludeIllness, function (key, value) {
            $('#ExclusionClientID').append($('<option>', {
                value: value.Code,
                text: value.Description
            }));
        });
        $("#CoverageCode").val('-1');
        $("#ExclusionClientID").val('-1');
        $("#ImpairmentCode").removeProp('disabled');
        $("#Cause").removeProp('disabled');
    } else if ($("#ExclusionType option:selected").val() == 2) { //Excluir cobertura
        $('#tr_RatingTable').hide();
        $('#tr_ImpairmentCode').hide();
        $('#tr_ExclusionClientID').hide();
        $('#tr_Cause').hide();
        $("#tr_WaitingPeriodDays").hide();
        $("#tr_WaitingPeriodMonths").hide();
        $("#tr_WaitingPeriodYears").hide();
        $("#tr_ExclusionPeriodType").hide();
        $("#tr_ProductModule").show();
        $("#tr_CoverageCode").show();
        $("#RatingTable").val('-1');
        $("#ImpairmentCode").val('');
        $("#Cause").val('-1');
        $("#ExclusionClientID").val('-1');
        $('#ExclusionPeriodType').val(-1);
        $("#ProductModule").val(-1);
        $("#ProductModule").removeProp('disabled');
        $("#CoverageCode").removeProp('disabled');
        $("#CoverageCode").empty();
        if ($("#ProductModule option").size() == 1) {
            $('#CoverageCode').append($('<option>', { value: -1, text: '' }));
            var coverageToAppend = (coverageType.length == 0) ? allCoverageType : coverageType;

            $.each(coverageToAppend, function (key, value) {
                $('#CoverageCode').append($('<option>', {
                    value: value.Code,
                    text: value.Description
                }));
            });
        }
        $("#CoverageCode").val('-1');
    } else if ($("#ExclusionType option:selected").val() == 5) { //Excluir asegurado
        $('#tr_ExclusionClientID').show();
        $('#tr_ProductModule').hide();
        $('#tr_CoverageCode').hide();
        $('#tr_RatingTable').hide();
        $('#tr_ImpairmentCode').hide();
        $('#tr_Cause').hide();
        $("#tr_WaitingPeriodDays").hide();
        $("#tr_WaitingPeriodMonths").hide();
        $("#tr_WaitingPeriodYears").hide();
        $("#tr_ExclusionPeriodType").hide();
        $("#ProductModule").val('-1');
        $("#CoverageCode").val('-1');
        $("#RatingTable").val('-1');
        $("#ImpairmentCode").val('');
        $("#Cause").val('-1');
        $('#ExclusionPeriodType').val(-1);
        $("#ExclusionClientID").removeProp('disabled');
        $('#ExclusionClientID').empty();
        $('#ExclusionClientID').append($('<option>', { value: -1, text: '' }));
        $.each(clientExclude, function (key, value) {
            $('#ExclusionClientID').append($('<option>', {
                value: value.Code,
                text: value.Description
            }));
        });
        $("#ExclusionClientID").val('-1');
    } else { //Ninguna
        $("#RatingTable").prop('disabled', true);
        $("#ProductModule").prop('disabled', true);
        $("#CoverageCode").prop('disabled', true);
        $("#ImpairmentCode").prop('disabled', true);
        $("#Cause").prop('disabled', true);
        $("#ExclusionClientID").prop('disabled', true);
        $("#RatingTable").val('-1');
        $("#ProductModule").val('-1');
        $("#CoverageCode").val('-1');
        $("#ImpairmentCode").val('');
        $("#Cause").val('-1');
        $("#ExclusionClientID").val('-1');
    }
}

function ExclusionValueEdit() {
    if ($("#ExclusionType option:selected").val() == 1) { //Excluir enfermedad
        $('#tr_RatingTable').hide();
        $('#tr_ExclusionClientName').hide();
        $('#tr_ExclusionClientID').find('td.CaptionTD').html(exclusionIllnessInsuredDescription);
        $("#Cause").removeProp('disabled');
        $('#ExclusionClientID').empty();
        $('#ExclusionClientID').append($('<option>', { value: -1, text: '' }));
        $.each(clientExcludeIllness, function (key, value) {
            $('#ExclusionClientID').append($('<option>', {
                value: value.Code,
                text: value.Description
            }));
        });
        if ($("#ExclusionClientName").val().length !== 0) {
            $("#ExclusionClientID option:contains('" + $("#ExclusionClientName").val() + "')").attr('selected', true);
        }       
        if ($("#ProductModule option").size() == 1) {
            $("#CoverageCode").empty();
            $('#CoverageCode').append($('<option>', { value: -1, text: '' }));
            $.each(allCoverageType, function (key, value) {
                $('#CoverageCode').append($('<option>', {
                    value: value.Code,
                    text: value.Description
                }));
            });
        }
        if ($("#CoverageDescription").val().length !== 0) {
            $("#CoverageCode option:contains('" + $("#CoverageDescription").val() + "')").attr('selected', true);
        }         
    } else if ($("#ExclusionType option:selected").val() == 2) { //Excluir cobertura
        $('#tr_RatingTable').hide();
        $('#tr_ExclusionClientName').hide();
        $('#tr_ImpairmentCode').hide();
        $('#tr_Cause').hide();
        $("#tr_WaitingPeriodDays").hide();
        $("#tr_WaitingPeriodMonths").hide();
        $("#tr_WaitingPeriodYears").hide();
        $("#tr_ExclusionPeriodType").hide();
        $('#tr_ExclusionClientID').hide();
    } else if ($("#ExclusionType option:selected").val() == 5) { //Excluir asegurado
        $('#tr_ExclusionClientID').show();
        $('#tr_ProductModule').hide();
        $('#tr_CoverageCode').hide();
        $('#tr_RatingTable').hide();
        $('#tr_ImpairmentCode').hide();
        $('#tr_Cause').hide();
        $("#tr_WaitingPeriodDays").hide();
        $("#tr_WaitingPeriodMonths").hide();
        $("#tr_WaitingPeriodYears").hide();
        $("#tr_ExclusionPeriodType").hide();
    } else { //Ninguna
        $("#RatingTable").prop('disabled', true);
        $("#ProductModule").prop('disabled', true);
        $("#CoverageCode").prop('disabled', true);
        $("#ImpairmentCode").prop('disabled', true);
        $("#Cause").prop('disabled', true);
        $("#ExclusionClientID").prop('disabled', true);
        $("#RatingTable").val('-1');
        $("#ProductModule").val('-1');
        $("#CoverageCode").val('-1');
        $("#ImpairmentCode").val('');
        $("#Cause").val('-1');
        $("#ExclusionClientID").val('-1');
    }
}

function GetLookUpsRating(methodName) {
    var rolesTypes = "-1:;";
    $.ajax({
        url: "services/LookUps.aspx/" + methodName,
        contentType: "application/json; charset=utf-8",
        type: "POST",
        datatype: "application/json",
        async: false,
        success: function (response) {
            $.each(response.d, function (index, item) {
                rolesTypes = rolesTypes + item.RatingTable + ":" + item.RateDescription + ";";
            });
        }
    });
    return rolesTypes.slice(0, -1);
}

function LoadAddGridSettings(typeAlarm) {
    if (typeAlarm == 5) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                postData.DiscountOrExtraPremiumDescription = $("#Discountorextrapremiumcode option:selected").text();
                postData.ExclusionPeriodTypeDescription = $("#ExclusionPeriodType option:selected").text();
                postData.RestrictionId = (postData.RestrictionId == "" ? 0 : postData.RestrictionId);
                postData.DOfFlatExtraPremiumDays = (postData.DOfFlatExtraPremiumDays == "" ? 0 : postData.DOfFlatExtraPremiumDays);
                postData.DOfFlatExtraPremiumMonths = (postData.DOfFlatExtraPremiumMonths == "" ? 0 : postData.DOfFlatExtraPremiumMonths);
                postData.DOfFlatExtraPremiumYears = (postData.DOfFlatExtraPremiumYears == "" ? 0 : postData.DOfFlatExtraPremiumYears);
                postData.ExtraPremiumPercentage = (postData.ExtraPremiumPercentage == "" ? 0 : postData.ExtraPremiumPercentage);
                postData.FlatExtraPremium = (postData.FlatExtraPremium == "" ? 0 : postData.FlatExtraPremium);
                postData.XPremiumDiscountOnlyInsured = postData.XPremiumDiscountOnlyInsured;
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ExclusionPeriodType = (postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType);
                postData.RestrictionType = 2;
                postData.IsNew = true;
                postData.AlarmType = typeAlarm;
                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    requirementId: $("#txtRequirementID").val(),
                    newRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ExclusionPeriodType == 2 && postData.DOfFlatExtraPremiumDays == 0 && postData.DOfFlatExtraPremiumMonths == 0 && postData.DOfFlatExtraPremiumYears == 0) {
                    return [false, validationText.validationTemporal];
                }
                if (postData.Discountorextrapremiumcode == -1) {
                    return [false, validationText.validationDescription];
                }
                if (postData.ExclusionPeriodType == -1 && postData.XPremiumDiscountOnlyInsured === 'true') {
                    return [false, validationText.validationPeriod];
                }
                if (postData.ExtraPremiumPercentage.trim().length == 0 && postData.FlatExtraPremium.trim().length == 0 || parseInt(postData.ExtraPremiumPercentage) > 0 && parseInt(postData.FlatExtraPremium) > 0) {
                    return [false, validationText.validationFactor];
                }
                return [true, ''];
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/AddRestriction"
        });
    } else if (typeAlarm == 6) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                postData.AlarmType = typeAlarm;
                postData.RestrictionType = 1;
                postData.IsNew = true;
                postData.RestrictionId = (postData.RestrictionId == "" ? 0 : postData.RestrictionId);
                postData.ExclusionTypeDescription = $("#ExclusionType option:selected").text();
                postData.RatingTableDescription = $("#RatingTable option:selected").text();
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();

                if (postData.ImpairmentCode.length > 0) {
                    var data = postData.ImpairmentCode.split('|');
                    postData.ImpairmentCode = data[0].trim();
                    postData.ImpairmentCodeDescription = data[1].trim();
                }
                if ($("#ExclusionClientID option:selected").text().length > 0) {
                    var data = $("#ExclusionClientID option:selected").text().split('|');
                    postData.ExclusionClientID = data[0].trim();
                    postData.ExclusionClientName = data[1].trim();
                } else {
                    postData.ExclusionClientID = (postData.ExclusionClientID == "-1" ? "" : postData.ExclusionClientID);
                }

                postData.ExclusionPeriodTypeDescription = $("#ExclusionPeriodType option:selected").text();
                postData.CauseDescription = $("#Cause option:selected").text();
                postData.ExclusionType = (postData.ExclusionType == "-1" ? 0 : postData.ExclusionType);
                postData.RatingTable = (postData.RatingTable == "-1" ? 0 : postData.RatingTable);
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ExclusionPeriodType = (postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType);

                postData.WaitingPeriodDays = (postData.WaitingPeriodDays == "" ? 0 : postData.WaitingPeriodDays);
                postData.WaitingPeriodMonths = (postData.WaitingPeriodMonths == "" ? 0 : postData.WaitingPeriodMonths);
                postData.WaitingPeriodYears = (postData.WaitingPeriodYears == "" ? 0 : postData.WaitingPeriodYears);
                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    requirementId: $("#txtRequirementID").val(),
                    newRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ExclusionType == -1) {
                    return [false, validationText.validationExclusion];
                }
                //if (postData.ExclusionPeriodType == -1) {
                //	return [false, validationText.validationPeriod];
                //}
                if (postData.ExclusionType == 1 && (postData.ImpairmentCode.length <= 0 || postData.Cause == -1)) {
                    return [false, validationText.validationIllnessCause];
                }
                if (postData.ExclusionType == 2 && (postData.CoverageCode == -1 || typeof postData.CoverageCode === 'undefined')) {
                    return [false, validationText.validationModuleCoverage];
                }
                if (postData.ExclusionType == 3 && (postData.CoverageCode == -1 || postData.ImpairmentCode.length <= 0 || postData.Cause == -1)) {
                    return [false, validationText.validationModCovIllCause];
                }
                if (postData.ExclusionType == 4 && (postData.ImpairmentCode.length <= 0 || postData.Cause == -1 || postData.RatingTable == -1)) {
                    return [false, validationText.validationIllCauseRate];
                }
                if (postData.ExclusionType == 5 && postData.ExclusionClientID == -1) {
                    return [false, validationText.validationInsured];
                }
                if (postData.ExclusionPeriodType == 2 && postData.WaitingPeriodDays == 0 && postData.WaitingPeriodMonths == 0 && postData.WaitingPeriodYears == 0) {
                    return [false, validationText.validationTemporal];
                }
                return [true, ''];
            },
            beforeShowForm: function (formid) {
                $("#tr_ImpairmentCodeDescription").addClass('hide');
                $("#tr_ExclusionClientName").addClass('hide');
                $("#tr_CoverageDescription").addClass('hide');
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/AddRestriction"
        });
    } else if (typeAlarm == 8) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            serializeEditData: function (postData) {
                postData.AlarmType = typeAlarm;
                postData.RestrictionType = 3;
                postData.IsNew = true;
                postData.RestrictionId = (postData.RestrictionId == "" ? 0 : postData.RestrictionId);
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();
                postData.MaximumInsuredAmount = $("#MaximumInsuredAmount").val();
                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    requirementId: $("#txtRequirementID").val(),
                    newRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ProductModule == -1 && $('#ProductModule > option').length > 1) {
                    return [false, validationText.validationModuleCoverage];
                }
                if (postData.CoverageCode == -1) {
                    return [false, validationText.validationCoverage];
                }
                return [true, ''];
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/AddRestriction"
        });
    }
}

function LoadEditGridSettings(typeAlarm) {
    if (typeAlarm == 5) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            closeAfterEdit: true,
            serializeEditData: function (postData) {
                postData.DiscountOrExtraPremiumDescription = $("#Discountorextrapremiumcode option:selected").text();
                postData.ExclusionPeriodTypeDescription = $("#ExclusionPeriodType option:selected").text();
                postData.ExtraPremiumPercentage = (postData.ExtraPremiumPercentage == "" ? 0 : postData.ExtraPremiumPercentage);
                postData.FlatExtraPremium = (postData.FlatExtraPremium == "" ? 0 : postData.FlatExtraPremium);
                postData.DOfFlatExtraPremiumDays = (postData.DOfFlatExtraPremiumDays == "" ? 0 : postData.DOfFlatExtraPremiumDays);
                postData.DOfFlatExtraPremiumMonths = (postData.DOfFlatExtraPremiumMonths == "" ? 0 : postData.DOfFlatExtraPremiumMonths);
                postData.DOfFlatExtraPremiumYears = (postData.DOfFlatExtraPremiumYears == "" ? 0 : postData.DOfFlatExtraPremiumYears);
                postData.XPremiumDiscountOnlyInsured = postData.XPremiumDiscountOnlyInsured;
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ExclusionPeriodType = (postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType);
                postData.RestrictionType = 2;
                postData.AlarmType = typeAlarm;
                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    ediRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ExclusionPeriodType == 2 && postData.DOfFlatExtraPremiumDays == 0 && postData.DOfFlatExtraPremiumMonths == 0 && postData.DOfFlatExtraPremiumYears == 0) {
                    return [false, validationText.validationTemporal];
                }
                if (postData.Discountorextrapremiumcode == -1) {
                    return [false, validationText.validationDescription];
                }
                if (postData.ExclusionPeriodType == -1 && postData.XPremiumDiscountOnlyInsured === 'true') {
                    return [false, validationText.validationPeriod];
                }
                if (postData.ExtraPremiumPercentage.trim().length == 0 && postData.FlatExtraPremium.trim().length == 0 || parseInt(postData.ExtraPremiumPercentage) > 0 && parseInt(postData.FlatExtraPremium) > 0) {
                    return [false, validationText.validationFactor];
                }
                return [true, ''];
            },
            beforeShowForm: function (formid) {
                DisabledFieldChange(typeAlarm, UnderId);
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/EditRestriction"
        });
    } else if (typeAlarm == 6) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            closeAfterEdit: true,
            serializeEditData: function (postData) {
                postData.AlarmType = typeAlarm;
                postData.RestrictionType = 1;
                postData.ExclusionTypeDescription = $("#ExclusionType option:selected").text();
                postData.RatingTableDescription = $("#RatingTable option:selected").text();
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();
                if (postData.ImpairmentCode.length > 0) {
                    var data = postData.ImpairmentCode.split('|');
                    postData.ImpairmentCode = data[0].trim();
                    postData.ImpairmentCodeDescription = data[1].trim();
                }
                if ($("#ExclusionClientID option:selected").text().length > 0) {
                    var data = $("#ExclusionClientID option:selected").text().split('|');
                    postData.ExclusionClientID = data[0].trim();
                    postData.ExclusionClientName = data[1].trim();
                } else {
                    postData.ExclusionClientID = (postData.ExclusionClientID == "-1" ? "" : postData.ExclusionClientID);
                }
                postData.CauseDescription = $("#Cause option:selected").text();
                postData.ExclusionPeriodTypeDescription = $("#ExclusionPeriodType option:selected").text();
                postData.ExclusionType = (postData.ExclusionType == "-1" ? 0 : postData.ExclusionType);
                postData.RatingTable = (postData.RatingTable == "-1" ? 0 : postData.RatingTable);
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ExclusionPeriodType = (postData.ExclusionPeriodType == "-1" ? 0 : postData.ExclusionPeriodType);

                postData.WaitingPeriodDays = (postData.WaitingPeriodDays == "" ? 0 : postData.WaitingPeriodDays);
                postData.WaitingPeriodMonths = (postData.WaitingPeriodMonths == "" ? 0 : postData.WaitingPeriodMonths);
                postData.WaitingPeriodYears = (postData.WaitingPeriodYears == "" ? 0 : postData.WaitingPeriodYears);

                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    ediRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ExclusionType == -1) {
                    return [false, validationText.validationExclusion];
                }
                //if (postData.ExclusionPeriodType == -1) {
                //	return [false, validationText.validationPeriod];
                //}
                if (postData.ExclusionType == 1 && (postData.ImpairmentCode.length <= 0 || postData.Cause == -1)) {
                    return [false, validationText.validationIllnessCause];
                }
                if (postData.ExclusionType == 2 && (postData.CoverageCode == -1 || typeof postData.CoverageCode === 'undefined')) {
                    return [false, validationText.validationModuleCoverage];
                }
                if (postData.ExclusionType == 3 && (postData.CoverageCode == -1 || postData.ImpairmentCode.length <= 0 || postData.Cause == -1)) {
                    return [false, validationText.validationModCovIllCause];
                }
                if (postData.ExclusionType == 4 && (postData.ImpairmentCode.length <= 0 || postData.Cause == -1 || postData.RatingTable == -1)) {
                    return [false, validationText.validationIllCauseRate];
                }
                if (postData.ExclusionType == 5 && postData.ExclusionClientID == -1) {
                    return [false, validationText.validationInsured];
                }
                if (postData.ExclusionPeriodType == 2 && postData.WaitingPeriodDays == 0 && postData.WaitingPeriodMonths == 0 && postData.WaitingPeriodYears == 0) {
                    return [false, validationText.validationTemporal];
                }
                return [true, ''];
            },
            beforeShowForm: function (formid) {
                ExclusionValueEdit();
                DisabledFieldChange(typeAlarm, UnderId);
                $("#tr_CoverageDescription").addClass('hide');
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
                $("#tr_ImpairmentCodeDescription").addClass('hide');
                if ($("#ImpairmentCode").val().length > 0)
                    $("#ImpairmentCode").val($("#ImpairmentCode").val() + ' | ' + $("#ImpairmentCodeDescription").val());
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/EditRestriction"
        });
    } else if (typeAlarm == 8) {
        return $.extend({}, GeneralAddSettings, {
            width: 400,
            closeAfterEdit: true,
            serializeEditData: function (postData) {
                postData.AlarmType = typeAlarm;
                postData.RestrictionType = 3;
                postData.CoverageCode = (postData.CoverageCode == "-1" ? 0 : postData.CoverageCode);
                postData.ProductModule = (postData.ProductModule == "-1" ? 0 : postData.ProductModule);
                postData.ProductModuleDescription = $("#ProductModule option:selected").text();
                postData.CoverageDescription = $("#CoverageCode option:selected").text();
                postData = {
                    caseId: $('#dpeCaseId_I').val(),
                    ediRestriction: postData
                };
                return JSON.stringify(postData);
            },
            beforeSubmit: function (postData, formid) {
                if (postData.ProductModule == -1 && $('#ProductModule > option').length > 1) {
                    return [false, validationText.validationModuleCoverage];
                }
                if (postData.CoverageCode == -1) {
                    return [false, validationText.validationCoverage];
                }
                return [true, ''];
            },
            beforeShowForm: function (formid) {
                $("#pData").addClass('hide');
                $("#nData").addClass('hide');
                LoadCoverageByValue($("#CoverageCode option:selected").val());
                DisabledFieldChange(typeAlarm, UnderId);
            },
            afterShowForm: function (form) {
                var modal = form.parents(".ui-jqdialog");
                var grilla = form.parents(".ui-jqgrid");

                modal.css("position", "fixed");
                modal.css("top", 70);
                if (isExplorer()) {
                    modal.css("left", (window.innerWidth - modal.width()) / 2);
                } else {
                    modal.css("left", (window.innerWidth - modal.width()) / 6);
                }
            },
            afterSubmit: function (response, postdata) {
                var res = $.parseJSON(response.responseText);
                if (res.d.length > 0) {
                    $loading.hide();
                    return [false, res.d];
                }
                $(this).jqGrid("setGridParam", { datatype: 'json' }).trigger("reloadGrid");
                return [true, ''];
            },
            url: "services/UnderwritingRule.aspx/EditRestriction"
        });
    }
}

function SaveAndExit() {
    if (FieldValidator()) {
        var impairmentCode = 0;
        var question = 0;
        if ($("#txtEnfermedad").val().length > 0) {
            var data = $("#txtEnfermedad").val().split('|');
            impairmentCode = data[0].trim();
        }
        if ($("#txtPregunta").val().length > 0) {
            var data = $("#txtPregunta").val().split('|');
            question = data[0].trim();
        }
        var createBy = $("#txtCreadoPor").val().split('|');
        var postData = JSON.stringify({
            newRule: {
                Description: $("#txtReglaManual").val(),
                Answer: $("#txtRespuesta").val(),
                ImpairmentCode: impairmentCode,
                DegreeId: impairmentCode == 0 ? 0 : $("#ddlNivelEnfermedad option:selected").val(),
                Explanation: $("#txtExplicacion").val(),
                UnderwritingArea: $("#ddlReqAreaDeSuscripcion option:selected").val(),
                QuestionId: question,
                AutomaticPoints: $("#rangoPuntos").val(),
                RequirementType: $("#ddlTipoDeRequerimiento").val(),
                CreatorUserDescription: createBy[1].trim(),
                ClientID: $("#ddlClientId option:selected").val(),
            },
            clientId: $('#ClientId').val(),
            caseId: $('#dpeCaseId_I').val()
        });
        ProxySyncUnderwritingRule.invoke("SaveRule", postData, function () {
            ReloadUnderwritingRulesGrid(true, $("#txtRequirementID").val());
        });
        $('#addRule').modal('toggle');
    }
}

function FieldValidator() {
    var fields = "";
    if ($("#txtReglaManual").val().trim() == "") {
        fields += $("label[for=txtReglaManual]").text().slice(0, -1) + ", ";
    }
    if ($("#txtExplicacion").val().trim() == "") {
        fields += $("label[for=txtExplicacion]").text().slice(0, -1) + ", ";
    }
    if ($("#txtEnfermedad").val().length > 0 && (typeof $("#ddlNivelEnfermedad option:selected").val() === "undefined" || $("#ddlNivelEnfermedad option:selected").val() == "")) {
        fields += $("label[for=ddlNivelEnfermedad]").text().slice(0, -1) + ", ";
    }
    if ($("#ddlReqAreaDeSuscripcion option:selected").val() == "") {
        fields += $("label[for=ddlReqAreaDeSuscripcion]").text().slice(0, -1) + ", ";
    }
    if ($("#txtPregunta").val().length == 0) {
        fields += $("label[for=txtPregunta]").text().slice(0, -1) + ", ";
    }
    if (gridAlarm.getGridParam("reccount") == 0) {
        fields += validationText.validationAlarms
    }
    if (typeof gridRestriction === "undefined" || gridRestriction.getGridParam("reccount") == 0) {
        fields += validationText.validationRestrictions
    }

    fields = fields.slice(0, -2);
    if (fields.length > 0) {
        $(".divError").html(validationText.validationFieldRequired + " " + fields);
        setTimeout(function () {
            $(".divError").fadeIn(1500);
        }, 0);
        setTimeout(function () {
            $(".divError").fadeOut(1500);
        }, 5000);
        return false;
    }
    return true;
}

function LoadFieldEditForm(data) {
    $("#txtReglaManual").val(data.Description);
    $("#txtEnfermedad").val(data.ImpairmentCodeDescription);
    $('#ddlNivelEnfermedad').val(data.DegreeId);
    $("#txtExplicacion").val(data.Explanation);
    $("#ddlReqAreaDeSuscripcion").val(data.UnderwritingArea);
    $('#txtPregunta').val(data.QuestionIdDescription);
    $("#rangoPuntos").val(data.AutomaticPoints);
    $("#txtCreadoPor").val(data.CreatorUserDescription);
    $("#txtRespuesta").val(ruleAnswer);
    $('#lblPuntos').html(data.AutomaticPoints);
    $("#ddlClientId").val(data.ClientID);
    if ($("#txtEnfermedad").val().length > 0 && data.ImpairmentCodeDescription.indexOf('|') >= 0)
        $('#ddlNivelEnfermedad').removeProp('disabled');
    $("#txtReglaManual").prop('disabled', true);
    $("#txtPregunta").prop('disabled', true);
    $("#ddlReqAreaDeSuscripcion").prop('disabled', true);
    $("#ddlClientId").prop('disabled', true);
    if (!data.IsManualRule) {
        $("#chkIsManual").prop('checked', false);
    } else {
        $("#chkIsManual").prop('checked', true);
    }

}

function DisabledField() {
    $("#txtReglaManual").prop('disabled', true);
    $("#txtEnfermedad").prop('disabled', true);
    $('#ddlNivelEnfermedad').prop('disabled', true);
    $("#txtExplicacion").prop('disabled', true);
    $("#ddlReqAreaDeSuscripcion").prop('disabled', true);
    $('#txtPregunta').prop('disabled', true);
    $("#rangoPuntos").prop('disabled', true);
    $("#txtCreadoPor").prop('disabled', true);
    $("#btn-guardar-regla").prop('disabled', true);
}