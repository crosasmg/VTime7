var IMAGES_DECISION = {
    0: "/Underwriting/Images/pending.png", // Pending
    1: "/Underwriting/Images/pending.png", // Pending
    2: "/Underwriting/Images/rejected.png", // Rejected
    3: "/Underwriting/Images/approved.png", // Approved
    4: "/Underwriting/Images/approvedChanges.png", // Approved with changes
    5: "/Underwriting/Images/manualReview.png" // Manual review
};

var IMAGES_WF = {
    1: "/Underwriting/Images/pending.png",
    2: "/Underwriting/Images/approved.png"
}

var MENU_ITEMS = {
    editCaseItem: "editCaseItem",
    editCancelCaseItem: "editCancelCaseItem",
    saveCaseItem: "saveCaseItem",
    saveCloseCaseItem: "saveCloseCaseItem",
    acceptCaseItem: "acceptCaseItem",
    acceptEndorsementItem: "acceptEndorsementItem",
    declineCaseItem: "declineCaseItem",
    declineEndorsementItem: "declineEndorsementItem",
    reopenCaseItem: "reopenCaseItem",
    editPolicyToBeIsued: "editPolicyToBeIsued",
    editPolicyToBeEndorse: "editPolicyToBeEndorse",
    openFormItem: "openFormItem"
}

var ENUM_UNDERWRITING_CASE_STATUS = {
    None: 0,
    InProcess: 1,
    MissingRequirements: 2,
    Decided: 3,
    DelegatedUnderwriter: 4,
    Void: 5,
    AllRequirementsReceived: 6,
    DelegatedCommittee: 7,
    EnteredThroughInterface: 8,
    Consultation: 9
};

var ProxyAsyncHeader = new serviceProxy("controls/partials/_header.aspx/", true);
var ProxySyncHeader = new serviceProxy("controls/partials/_header.aspx/", false);
var gridCaseSearch;
var gridColNamesCaseSearch;
var gridColModelCaseSearch;
var enabledEditPolicyButton;



function OnSelectedChanged() {
    RiskClassificationDropDown.SetValue(RiskClassificationListBox.GetSelectedItem().text);
    RiskClassificationDropDown.HideDropDown();
}

function OnSelectedChanged_stage() {
    StageDropDown.SetValue(StageListBox.GetSelectedItem().text);
    StageDropDown.HideDropDown();
}

function disableAllMenuOptions() {
    for (var item in MENU_ITEMS) {
        $("#" + MENU_ITEMS[item]).prop('disabled', true)
    }
}

function enableMenuOptions(options) {
    for (var i = 0; i < options.length; i++) {
        $("#" + MENU_ITEMS[options[i]]).prop('disabled', false)
    }
}

function showMenuOptions(options) {
    for (var i = 0; i < options.length; i++) {
        $("#" + MENU_ITEMS[options[i]]).show();
    }
}

function hideMenuOptions(options) {
    for (var i = 0; i < options.length; i++) {
        $("#" + MENU_ITEMS[options[i]]).hide();
    }
}

function getCaseId() {
    return $("#dpeCaseId_I").val();
}

function setCaseId(caseId) {
    $("#dpeCaseId_I").val(caseId);
}

function hideOptionsBaseOnCaseType(underwritingCaseType) {
    if (underwritingCaseType === 1) {
        hideMenuOptions([
			MENU_ITEMS.acceptEndorsementItem,
			MENU_ITEMS.declineEndorsementItem,
			MENU_ITEMS.editPolicyToBeEndorse
        ]);
    	
        showMenuOptions([
    		MENU_ITEMS.acceptCaseItem,
    		MENU_ITEMS.declineCaseItem,
    		MENU_ITEMS.editPolicyToBeIsued
        ]);
    } else {
        hideMenuOptions([
    		MENU_ITEMS.acceptCaseItem,
    		MENU_ITEMS.declineCaseItem,
    		MENU_ITEMS.editPolicyToBeIsued
        ]);
    	
        showMenuOptions([
			MENU_ITEMS.acceptEndorsementItem,
			MENU_ITEMS.declineEndorsementItem,
			MENU_ITEMS.editPolicyToBeEndorse
        ]);    	
    }
}

function enableOptionsBaseOnDecision(selectedCase) {
    switch(selectedCase.Decision) {
        case 1:
            enableMenuOptions([
				MENU_ITEMS.editCaseItem,
				MENU_ITEMS.acceptCaseItem,
				MENU_ITEMS.acceptEndorsementItem,
				MENU_ITEMS.declineCaseItem,
				MENU_ITEMS.declineEndorsementItem
            ]);
            hideOptionsBaseOnCaseType(selectedCase.UnderwritingCaseType)
            break;
        case 2:
            enableMenuOptions([MENU_ITEMS.reopenCaseItem]);
            break;
        case 3:
        case 4:
            disableAllMenuOptions();
            break;
        case 5:
            enableMenuOptions([
				MENU_ITEMS.editCaseItem,
				MENU_ITEMS.acceptCaseItem,
				MENU_ITEMS.acceptEndorsementItem,
				MENU_ITEMS.declineCaseItem,
				MENU_ITEMS.declineEndorsementItem
            ]);
            hideOptionsBaseOnCaseType(selectedCase.UnderwritingCaseType)
            break;
        default:
            disableAllMenuOptions();
            break;
    }
}

function changeBehaviorCaseMenu(selectedCase, userId) {
    if (selectedCase) {
        hideOptionsBaseOnCaseType(selectedCase.UnderwritingCaseType);
        disableAllMenuOptions();

        if (selectedCase.StatusEnum == ENUM_UNDERWRITING_CASE_STATUS.Consultation) {
            disableAllMenuOptions();
        } else if (selectedCase.LockedBy != 0
			&& selectedCase.LockedOn != new Date(0001, 0, 1, 0, 0, 0, 0)
			&& selectedCase.StatusEnum != ENUM_UNDERWRITING_CASE_STATUS.Decided ) {

            if (selectedCase.LockedBy == userId) {
                enableMenuOptions([
                    MENU_ITEMS.editCancelCaseItem,
                    MENU_ITEMS.saveCaseItem,
                    MENU_ITEMS.saveCloseCaseItem,
                    MENU_ITEMS.acceptEndorsementItem,
                    MENU_ITEMS.declineEndorsementItem,
                    MENU_ITEMS.editPolicyToBeEndorse,
                    MENU_ITEMS.openFormItem
                ]);
            }
        } else {
            enableOptionsBaseOnDecision(selectedCase);
        }
    }
}

function setImgDecision(decision) {
    if (decision) {
        $("#imgDecision").show().attr("src", IMAGES_DECISION[decision]);
    } else {
        $("#imgDecision").hide().attr("src", "");
    }
}

function setImgWFProgress(wfInProgress) {
    if (wfInProgress) {
        $("#imgWFProgress").show().attr("src", IMAGES_WF[wfInProgress]);
    } else {
        $("#imgWFProgress").hide().attr("src", "");
    }
}

function clearFields() {    
    setImgDecision();
    setImgWFProgress();
    $("#invalidCase").hide();
    $("#underwritingCaseStatus").text("");
    $("#wFProgressText").text("");
    $("#fullproposal").text("");
    $("#decision").text("");
    $("#stage").html("");
    $("#stage").hide();
    $("#stage").val("");
}

function enableDisableControls(selectedCase, state) {
    if (!selectedCase || selectedCase.StatusEnum != ENUM_UNDERWRITING_CASE_STATUS.Decided) {
        $("#stage").prop('disabled', !state);
        // $("#dpeCaseId_I").prop('disabled', state);
        // $("#caseIdAddon").prop('disabled', state);
    }
}

function setToReadOnlyViewMode(selectedCase) {
    enableDisableControls(selectedCase, false);
}

function setToEditViewMode(selectedCase) {
    if (selectedCase && selectedCase.Status == ENUM_UNDERWRITING_CASE_STATUS.Consultation) {
        setToReadOnlyViewMode(selectedCase);
    } else {
        enableDisableControls(selectedCase, true);
    }
}

/*
function loadListValues(ddlName, list) {
    var selectedOptions = $("select#" + ddlName);
    $.each(list, function () {
        selectedOptions.append($("<option />").val(this.Code).text(this.Description));
    });
}
*/

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function setValues(data, selectedCase) {

    $("#underwritingCaseStatus").text(data.caseStatusText || "");
    $("#wFProgressText").text(data.wFProgressText || "");
    $("#fullproposal").text(selectedCase.FullProposalId || "");
    $("#decision").text(data.decisionText || "");
    LoadDropdownOptions("stage", data.stageList)
    $("#stage").show().val(selectedCase.Stage);

    setImgDecision(selectedCase.Decision);
    setImgWFProgress(selectedCase.WFInProgress);
}

function reloadBottomPanel() {
    var active = $("#tabContent .tab-pane.active").attr("id");
    LoadControl(active);
}

function onSuccess(response) {
    var data = response.d;    
    var selectedCase = data.selectedCase;

    if (selectedCase) {

        setValues(data, selectedCase);

        changeBehaviorCaseMenu(selectedCase, data.userId);

        if(selectedCase.LockedBy != 0 && selectedCase.LockedBy == data.userId) {
            setToEditViewMode(selectedCase);
            changeEditPolicyButton();
        } else {
            setToReadOnlyViewMode(selectedCase);
        }
        reloadBottomPanel();
    }
}

function LoadDropdownOptions(ddlName, list) {
    var selectedOptions = $("select#" + ddlName);
    $.each(list, function () {
        selectedOptions.append($("<option />").val(this.Code).text(this.Description));
    });
}

function loadUnderwritingCase(caseId) {        
        var data = {
            caseId: caseId
        }

        setCaseId(caseId)

        clearFields();
        
        $loading.show();
        ProxyAsyncHeader.invoke("LoadUnderwritingCase", JSON.stringify(data), onSuccess)
            .error(function (response) {
                $("#invalidCase").show();
                reloadBottomPanel();
            });
}

function editCaseClick (e) {
    e.preventDefault();
    $loading.show();

    var data = {
        caseId: getCaseId()
    }

    ProxyAsyncHeader.invoke("EditInformation", JSON.stringify(data), function (response) {
        var selectedCase = response.d;
        disableAllMenuOptions();
        setToEditViewMode(selectedCase);
        
        enableMenuOptions([
            MENU_ITEMS.editCancelCaseItem,
            MENU_ITEMS.saveCaseItem,
            MENU_ITEMS.saveCloseCaseItem,
            MENU_ITEMS.openFormItem
        ]);
        changeBehaviorCaseMenu(selectedCase, selectedCase.LockedBy);
        changeEditPolicyButton();
        reloadBottomPanel();
    });
    
}

function editCancelClick(e) {
    $loading.show();
    e.preventDefault();

    var data = {
        caseId: getCaseId()
    }

    ProxyAsyncHeader.invoke("CancelEditInformation", JSON.stringify(data), function (response) {
        disableAllMenuOptions();
        loadUnderwritingCase(data.caseId);
        enableMenuOptions([
            MENU_ITEMS.editCaseItem
        ]);
    });
}

function saveCaseClick(e) {
    $loading.show();
    e.preventDefault();

    var data = {
        caseId: getCaseId(),
        stage: $("#stage").val()
    };
    disableAllMenuOptions();
    ProxyAsyncHeader.invoke("SaveCase", JSON.stringify(data), function () { loadUnderwritingCase(data.caseId) });

}

function saveAndCloseCaseClick(e) {
    $loading.show();
    e.preventDefault();

    var data = {
        caseId: getCaseId(),
        stage: $("#stage").val()
    };

    disableAllMenuOptions();

    ProxyAsyncHeader.invoke("SaveAndCloseCase", JSON.stringify(data), function (response) {
        loadUnderwritingCase(getCaseId());
        showWorkflowInformation(response.d);
    });

}

function showWorkflowInformation(workflowResponse) {
    $("#workflowInformation").html(workflowResponse.workflowInformation || "");
    $("#workflowStackTrace").html(workflowResponse.workflowStackTrace || "");
    $("#modal-workflow-response").modal();

}

function acceptCaseClick(e) {
    $loading.show();
    e.preventDefault();

    var data = {
        caseId: getCaseId()
    };

    ProxyAsyncHeader.invoke("AcceptCase", JSON.stringify(data), function (response) {
        loadUnderwritingCase(getCaseId());
        showWorkflowInformation(response.d);
    });
}

function reopenCaseClick(e) {
    $loading.show();
    e.preventDefault();

    var data = {
        caseId: getCaseId()
    };

    ProxyAsyncHeader.invoke("ReopenCase", JSON.stringify(data), function (response) {
        loadUnderwritingCase(getCaseId());
        showWorkflowInformation(response.d);
    });
}

function caseIdChange() {
    var caseId = $(this).val();

    if (caseId) {
        if (isFinite(caseId)) {
            loadUnderwritingCase(caseId);
        } else {
            $("#invalidCase").show();
            // reloadBottomPanel();
        }
    }   
}

function reloadCaseSearchGrid() {
    // $.jgrid.gridUnload("grid-case-search");

    gridCaseSearch = $("#grid-case-search");
    gridCaseSearch.jqGrid("GridUnload");

    gridCaseSearch.jqGrid($.extend({}, GeneralGridOptions, {
        url: "controls/partials/_header.aspx/GetUnderwritingCasesSearch",
        colNames: gridColNamesCaseSearch,
        colModel: gridColModelCaseSearch,
        serializeGridData: function (data) {
            var filter = {};

            for (var i = 0; i < gridColModelCaseSearch.length; i++) {
                var columnIndex = gridColModelCaseSearch[i].index
                if (data.hasOwnProperty(columnIndex)) {
                    filter[columnIndex] = data[columnIndex];
                }
            }         

            data.filter = jQuery.param(filter)

            return JSON.stringify(data);
        },
        loadonce: false,
        jsonReader: {
            repeatitems: false,
            root: function (obj) { return obj.d.rows; },
            page: function (obj) { return obj.d.page; },
            total: function (obj) { return obj.d.total; },
            records: function (obj) { return obj.d.records; }
        },
        pager: '#pager-case-search',
        subGrid: false,
        viewrecords: true,
        pgbuttons: true,
        sortname: 'UnderwritingCaseID',
        firstsortorder: 'desc',
        rowNum: 10,
        sortorder: 'desc',
        loadComplete: function () {
            
        },
        onSelectRow: function (rowId) {
            if (rowId) {
                row = gridCaseSearch.getRowData(rowId);
                $("#modal-case-search").modal("hide");
                loadUnderwritingCase(row.UnderwritingCaseID)
            }
        },
    }));

    gridCaseSearch.jqGrid('navGrid', '#pager-case-search', {
        edit: false,
        add: false,
        del: false,
        rowList: [],
        search: false,
        viewrecords: true,
        refresh: false
    });

    gridCaseSearch.jqGrid("filterToolbar", {
        searchOnEnter: false,
        enableClear: false,
        searchOperators: false,
        defaultSearch: 'cn',
        autosearch: true
    });

    gridCaseSearch.trigger('reloadGrid');
}


function setUpGridCaseSearch() {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    

    if (!gridColNamesCaseSearch) {
        ProxySyncHeader.invoke("GetHeaderValues", "", function (data) { gridColNamesCaseSearch = data.d;});
    }
    

    gridColModelCaseSearch = [{ name: 'UnderwritingCaseID', index: 'UnderwritingCaseID', resizable: true, editable: false, width: 100, sorttype: 'number', },
						   { name: 'OpenDate', index: 'OpenDate', resizable: true, editable: false, width: 90, sorttype: 'date', formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
                           { name: 'UnderwriterID', index: 'UnderwriterID', resizable: true, width: 200, editable: false, formatter: function (cellvalue, options, rowobject) { return cellvalue == 0 ? "" : cellvalue; } },
						   { name: 'Role', index: 'Role', resizable: true, editable: false, width: 120, },
                           { name: 'ClientID', index: 'ClientID', resizable: true, editable: false, width: 120 },
                           { name: 'ClientName', index: 'ClientName', resizable: true, editable: false, width: 200 },
                           { name: 'Decision', index: 'Decision', resizable: true, editable: false, width: 160 },
                           { name: 'LineOfBusiness', index: 'LineOfBusiness', resizable: true, editable: false, width: 120 },
                           { name: 'Product', index: 'Product', resizable: true, editable: false, width: 130, },
						   { name: 'FullProposalId', index: 'FullProposalId', resizable: true, editable: false, width: 160, classes: "text-right" },
                           { name: 'BatchNumber', index: 'BatchNumber', resizable: true, editable: false, width: 130, classes: "text-right" },
						   { name: 'PolicyID', index: 'PolicyID', resizable: true, editable: false, width: 90, classes: "text-right" },
						   { name: 'FaceAmount', index: 'FaceAmount', resizable: true, editable: false, width: 130, classes: "text-right", formatter: 'number', formatoptions: { decimalSeparator: ",", decimalPlaces: 2 } },
						   { name: 'LockedBy', index: 'LockedBy', resizable: true, editable: false, width: 200, classes: "text-right" },
                           { name: 'IsLocked', index: 'IsLocked', resizable: true, editable: false, hidden: true, width: 100 },
                           { name: 'CompositeKey', index: 'CompositeKey', resizable: true, editable: false, width: 100, hidden: true },
                           { name: 'TypeOfLineOfBusiness', index: 'TypeOfLineOfBusiness', resizable: true, editable: false, width: 100, hidden: true },
                           { name: 'ManualOrAutomatic', index: 'ManualOrAutomatic', resizable: true, editable: false, width: 100, hidden: true },
                           { name: 'Status', index: 'Status', resizable: true, editable: false, width: 100, hidden: true },
    ];
}

function clearDeclineCaseFields() {
    $("#errorMessages").hide();
    $("#comboRejectionReason").val("");
    $("#freeTextReason").val("");
}


function declineCaseClick(e) {
    e.preventDefault();

    clearDeclineCaseFields();
    $("#modal-decline-case").modal();
}

function comboRejectionReasonChange(e)
{
    $('#errorMessages').hide();
    $("#errorComboRejectionReason").hide();
}

function declineCaseModalButtonClick(e) {
    e.preventDefault();

    var comboRejectionValue = $("#comboRejectionReason").val();
    var rejectionReasonText = $("#comboRejectionReason :selected").text();
    var freeTextReasonValue = $("#freeTextReason").val();
    

    if (comboRejectionValue) {
        if (comboRejectionValue == 5 && !freeTextReasonValue) {
            $("#errorComboRejectionReason").hide();
            $("#errorfreeTextReason").show();
            $("#errorMessages").show();
            return false;
        } else {
            $('#errorMessages').hide();
            $("#modal-decline-case").modal("hide");
            $loading.show();
            var data = {
                caseId: getCaseId(),
                rejectionReason: comboRejectionValue,
                rejectionReasonText: rejectionReasonText,
                freeTextReason: freeTextReasonValue
            };

            ProxyAsyncHeader.invoke("DeclineCase", JSON.stringify(data), function (response) {
                loadUnderwritingCase(getCaseId())
                showWorkflowInformation(response.d)
            });
            
            return true;
        }
    } else {
        $('#errorMessages').show();
        $('#errorComboRejectionReason').show();
        $('#errorfreeTextReason').hide();
        return false;
    }
}

function editPolicyClick(e) {
    e.preventDefault();
    GetEditPolicyUrl(getCaseId());
}

function setUpMenuEvents() {
    $("#" + MENU_ITEMS.editCaseItem).click(editCaseClick);
    $("#" + MENU_ITEMS.editCancelCaseItem).click(editCancelClick);
    $("#" + MENU_ITEMS.saveCaseItem).click(saveCaseClick);
    $("#" + MENU_ITEMS.saveCloseCaseItem).click(saveAndCloseCaseClick);
    $("#" + MENU_ITEMS.acceptCaseItem).click(acceptCaseClick);
    $("#" + MENU_ITEMS.acceptEndorsementItem).click(acceptCaseClick);
    $("#" + MENU_ITEMS.declineCaseItem).click(declineCaseClick);
    $("#" + MENU_ITEMS.declineEndorsementItem).click(declineCaseClick);
    $("#" + MENU_ITEMS.reopenCaseItem).click(reopenCaseClick);
    $("#" + MENU_ITEMS.editPolicyToBeIsued).click(editPolicyClick);
    $("#" + MENU_ITEMS.editPolicyToBeEndorse).click(editPolicyClick);
    $("#" + MENU_ITEMS.openFormItem).click(function () { });

}

function changeEditPolicyButton() {
    var postData = {
        caseId: getCaseId()
    }
    ProxySyncUnderwritingPanel.invoke("GetEditPolicyUrl", JSON.stringify(postData),
        function (result) {
            if (result.d === "") {
                $("#" + MENU_ITEMS.editPolicyToBeIsued).prop('disabled', true);
            } else {
                $("#" + MENU_ITEMS.editPolicyToBeIsued).prop('disabled', false);
            }
    });
}



$(document).on('ready', function () {

    clearFields();

    hideMenuOptions([
        MENU_ITEMS.acceptEndorsementItem,
        MENU_ITEMS.declineEndorsementItem,
        MENU_ITEMS.editPolicyToBeEndorse,
        MENU_ITEMS.openFormItem
    ]);

    disableAllMenuOptions();

    if (getUrlParameter('uwcaseid')) {
        loadUnderwritingCase(getUrlParameter('uwcaseid'));
    }
    
    $("#dpeCaseId_I").on('change', caseIdChange);

    $("#caseIdAddon").click(function () {
        if (!$("#dpeCaseId_I").prop("disabled")) {
            reloadCaseSearchGrid();
            $("#modal-case-search").modal();
        }
    });

    LoadDefaultValues("comboRejectionReason", "GetRejectionReasonTypes", ProxySyncLookUps).done(function () { });

    $("#declineCaseModalButton").click(declineCaseModalButtonClick);

    $("#comboRejectionReason").on("change", comboRejectionReasonChange)

    setUpMenuEvents();
    setUpGridCaseSearch();
});