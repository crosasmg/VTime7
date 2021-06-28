var requirementRowIndex = -1;
var underwritingRuleRowIndex = -1;
var lastRoleSelected = -1;
var urlWebApplication = '<asp:Literal runat="server" Text="<%$appSettings:Url.WebApplication%>" />';

function btnInfo_Click(s, e) {
    popupInformation.Hide();
}

function SavingEndCallback(s, e) {
    hdnUPanel.Set('IsSaving', false);
}

function SetEditButtonEnabled(s, e) {
    if (hdnUPanel.Contains('IsSelected') != null & hdnUPanel.Get('IsSelected') == true) {
        s.SetEnabled(true);
    }
}

function Save(s, e) {
    hdnUPanel.Set('IsSaving', true);
    SavingCallbackPanel.PerformCallback();
}

function SaveAndClose(s, e) {
    hdnUPanel.Set('IsSaveAndClose', true);
    SavingCallbackPanel.PerformCallback();
}

function SetRoleValueOnDataSource(s, e) {
    var performCallback = false;

    //This is going to execute the perform call back only if the last selected role was provider (code=12 on the databse)
    //or if the selected role is provider, otherwise it wont execute the perform call back, this to get performance on the site
    if (!(lastRoleSelected < 0) && lastRoleSelected == 12)
        performCallback = true;

    lastRoleSelected = s.GetSelectedItem().value;

    if (s.GetSelectedItem().value == 12) {
        hdnGeneralInformation.Set("IsProviderSelected", true);
        performCallback = true;
    }
    else
        hdnGeneralInformation.Set("IsProviderSelected", false);

    //if (performCallback)
    //    GeneralInformationCallbackPanel.PerformCallback();
}

function btnYes_Click(s, e) {
    popupExpired.Hide();
    window.location.reload(true);
}

function CollapseRequirementDetailView() {
    dGrid.CollapseDetailRow(requirementRowIndex);
}

function RequirementExpandRow(s, e) {
    dGrid.SetFocusedRowIndex(e.visibleIndex);
    requirementRowIndex = e.visibleIndex;
    hdnIsEditingMode.Set("RowExpandedIndex", e.visibleIndex);

    if (dGrid.IsEditing() || dGrid.IsNewRowEditing()) {
        requirementRowIndex = -1
        e.cancel = true;
    }
}

function CollapseUnderwritingRuleDetailView() {
    rulesGrid.CollapseDetailRow(underwritingRuleRowIndex);
}

function OnClientDropDownInit(s, e) {
    if (!gridX.IsEditing()) {
        s.SetEnabled(false);
    }
}

function ClearFilters(s, e) {
    gSearchCase.ClearFilter();
}

function DropDownHandler(s, e) {
    SynchronizeFocusedRow();
}

function GridViewInitHandler(s, e) {
    $loading.show();
    var active = $("#tabContent .tab-pane.active").attr("id");
    gSearchCase.PerformCallback(s.GetFocusedRowIndex());
    LoadControl(active);
}

function ValueChangedHandler(s, e) {
    var caseid = CaseIdComboBox.GetValue();
    if (caseid != null) {
        var typeoflline = CaseIdComboBox.GetSelectedItem().GetColumnText('TypeOfLineOfBusiness');
        var islocked = CaseIdComboBox.GetSelectedItem().GetColumnText('IsLocked');
        dpeCaseId.SetKeyValue(caseid);
        dpeCaseId.SetText(caseid);

        //if (typeoflline == 1)
        //    tabPages.GetTab(2).SetVisible(false);
        //else
        //    tabPages.GetTab(2).SetVisible(true);

        hdnIsTaken.Set("IsTaken", islocked);
        if (document.getElementById('hdnIsCaseTakenDecision') != null)
            hdnIsCaseTakenDecision.Set("IsTaken", islocked);
        dpeCaseId.HideDropDown();
        $loading.show();
        HeaderCallbackPanel.PerformCallback(caseid);
        //GeneralInformationCallbackPanel.PerformCallback();

        hdnUPanel.Set('IsSelected', true);

        if (islocked == true) {
            MainMenu.GetItemByName('EditCaseItem').SetEnabled(false);
            MainMenu.GetItemByName('EditCancelCaseItem').SetEnabled(true);
            MainMenu.GetItemByName('SaveCaseItem').SetEnabled(true);
            MainMenu.GetItemByName('SaveCloseCaseItem').SetEnabled(true);
        }
        else {
            MainMenu.GetItemByName('EditCaseItem').SetEnabled(true);
            MainMenu.GetItemByName('EditCancelCaseItem').SetEnabled(false);
            MainMenu.GetItemByName('SaveCaseItem').SetEnabled(false);
            MainMenu.GetItemByName('SaveCloseCaseItem').SetEnabled(false);
        }
    }

    e.processOnServer = false;
}

function RowClickHandler(s, e) {
    var caseid = gSearchCase.cpKeyValues[e.visibleIndex];
    dpeCaseId.SetKeyValue(caseid);
    dpeCaseId.SetText(caseid);

    var typeofline = gSearchCase.cpTypeOfLineOfBusinessValues[e.visibleIndex];

    //if (typeofline == 1)
    //    tabPages.GetTab(2).SetVisible(false);
    //else
    //    tabPages.GetTab(2).SetVisible(true);

    hdnIsTaken.Set("IsTaken", gSearchCase.cplockedCaseValues[e.visibleIndex]);
    if (document.getElementById('hdnIsCaseTakenDecision') != null)
        hdnIsCaseTakenDecision.Set("IsTaken", gSearchCase.cplockedCaseValues[e.visibleIndex]);
    dpeCaseId.HideDropDown();
    $loading.show();
    HeaderCallbackPanel.PerformCallback(caseid);

    //GeneralInformationCallbackPanel.PerformCallback();

    hdnUPanel.Set('IsSelected', true);

    if (gSearchCase.cplockedCaseValues[e.visibleIndex] == true) {
        MainMenu.GetItemByName('EditCaseItem').SetEnabled(false);
        MainMenu.GetItemByName('EditCancelCaseItem').SetEnabled(true);
        MainMenu.GetItemByName('SaveCaseItem').SetEnabled(true);
        MainMenu.GetItemByName('SaveCloseCaseItem').SetEnabled(true);
    }
    else {
        MainMenu.GetItemByName('EditCaseItem').SetEnabled(true);
        MainMenu.GetItemByName('EditCancelCaseItem').SetEnabled(false);
        MainMenu.GetItemByName('SaveCaseItem').SetEnabled(false);
        MainMenu.GetItemByName('SaveCloseCaseItem').SetEnabled(false);

        if (gSearchCase.cpStatusValues[e.visibleIndex] != 3) {
            MainMenu.GetItemByName('EditCaseItem').SetEnabled(true);
        }
        else {
            MainMenu.GetItemByName('EditCaseItem').SetEnabled(false);
        }
    }

    if ((gSearchCase.cpManualOrAutomaticValues[e.visibleIndex] != 2) && (gSearchCase.cpStatusValues[e.visibleIndex] != 3)) {
        MainMenu.GetItemByName('AcceptCaseItem').SetEnabled(true);
    }
    else {
        MainMenu.GetItemByName('AcceptCaseItem').SetEnabled(false);
    }

    if (gSearchCase.cpStatusValues[e.visibleIndex] != 3) {
        MainMenu.GetItemByName('DeclineCaseItem').SetEnabled(true);
        MainMenu.GetItemByName('ReopenCaseItem').SetEnabled(false);
    }
    else {
        MainMenu.GetItemByName('DeclineCaseItem').SetEnabled(false);
        MainMenu.GetItemByName('ReopenCaseItem').SetEnabled(true);
    }

}

function SynchronizeFocusedRow() {

    var keyValue = dpeCaseId.GetKeyValue();
    var index = -1;
    if (keyValue != null)
        index = ASPxClientUtils.ArrayIndexOf(gSearchCase.cpKeyValues, keyValue);
    gSearchCase.SetFocusedRowIndex(index);
    gSearchCase.MakeRowVisible(index);
}

function ClientGridViewInitHandler(s, e) {
    ClientSynchronizeFocusedRow();
}

function ClientRowClickHandler(s, e) {
    dpeClient.SetKeyValue(gSearchClient.cpClientKeyValues[e.visibleIndex]);
    dpeClient.SetText(gSearchClient.cpClientKeyValues[e.visibleIndex]);
    lblEditClientName.SetText(gSearchClient.cpClientNames[e.visibleIndex])

    dpeClient.HideDropDown();

    pnlLoadingInformation.ShowInElementByID(SavingCallbackPanel.uniqueID);
    roleGridOnEdit.GetValuesOnCustomCallback(cmbRoles.GetValue() + ";" + gSearchClient.cpClientKeyValues[e.visibleIndex], LoadRoleInformation);
}

//Loads the client information gathered from server side by calling GetValuesOnCustomCallback from grid
function LoadRoleInformation(result) {
    for (var i = 0; i < roleGridOnEdit.GetColumnsCount() - 1; i++) {
        if (!(i == 0) && !(i == 2) && !(i == 3)) {
            var editor = roleGridOnEdit.GetEditor(i);
            if (editor) {
                editor.SetValue(result[i]);
            }
        }
    }
    pnlLoadingInformation.Hide();
}

//Loads the client information gathered from server side by calling GetValuesOnCustomCallback from grid
function BeginSelectedProviderCallback() {
    try {
        if (roleGridOnEdit.IsEditing()) {
            for (var i = 0; i < roleGridOnEdit.GetColumnsCount() - 1; i++) {
                var editor = roleGridOnEdit.GetEditor(i);
                if (editor) {
                    hdnGeneralInformation.Set(editor.uniqueID, editor.GetValue());
                }
            }
        }
    }
    catch (err) {
    }
}

function PanelPerformCallback(s, e) {
    $loading.show();
    var caseid = "";
    if (s.GetValue() == null) {
        caseid = "";
        SetEmptyCase();
    } else
        caseid = s.GetValue();

    dpeCaseId.SetKeyValue(caseid);
    dpeCaseId.SetText(caseid);
    HeaderCallbackPanel.PerformCallback(caseid);
}

//Loads the client information gathered from server side by calling GetValuesOnCustomCallback from grid
function EndSelectedProviderCallback() {
    //ojo con esto
    //if (headerValues.Get("TypeOfLineOfBusiness") == 1)
    //    tabPages.GetTab(2).SetVisible(false);
    //else
    //    tabPages.GetTab(2).SetVisible(true);

    try {
        if (roleGridOnEdit.IsEditing()) {
            for (var i = 0; i < roleGridOnEdit.GetColumnsCount() - 1; i++) {
                var editor = roleGridOnEdit.GetEditor(i);
                if (editor) {
                    editor.SetValue(hdnGeneralInformation.Get(editor.uniqueID))
                }
            }
        }
    }
    catch (err) {
    }
}

function ClientEndCallbackHandler(s, e) {
    dpeClient.AdjustDropDownWindow();
}

function ClientSynchronizeFocusedRow() {
    var keyValue = dpeClient.GetKeyValue();
    var index = -1;
    if (keyValue != null)
        index = ASPxClientUtils.ArrayIndexOf(gSearchClient.cpClientKeyValues, keyValue);
    gSearchClient.SetFocusedRowIndex(index);
    gSearchClient.MakeRowVisible(index);
}

function ClientDropDownHandler(s, e) {
    ClientSynchronizeFocusedRow();
}

function BehaviorPopupMenuItems() {
    MainMenu.GetItemByName('EditCaseItem').SetEnabled(true);
    MainMenu.GetItemByName('AcceptCaseItem').SetEnabled(false);
    MainMenu.GetItemByName('DeclineCaseItem').SetEnabled(false);
}

function GetEditModeStatus() {
    var isEditMode = "";
    var caseId = $("#dpeCaseId_I").val()

    if (!caseId) {
        return "False";
    }
        
    var postData = {
        caseId: caseId
    }

    $.ajax({
        url: "UnderwritingPanel.aspx/GetEditModeStatus",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        type: "POST",
        datatype: "application/json",
        async: false,
        success: function (response) {
            isEditMode = response.d;
        },
        error: function (err) {
            console.log(err);
            if (err.status === 401) {
                popupExpired.Show();
            }
        }
    });
    return isEditMode;
}

function IsSessionTimeOut() {
    var sessionTimeout = "";
    $.ajax({
        url: "UnderwritingPanel.aspx/IsSessionTimeOut",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        datatype: "application/json",
        async: false,
        success: function (response) {
            sessionTimeout = response.d;
        }
    });
    return sessionTimeout;
}

function SetEmptyCase() {
    $.ajax({
        url: "UnderwritingPanel.aspx/SetEmptyCase",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        datatype: "application/json",
        async: false
    });
}

$(document).ready(function () {

    isReloaded = true;

    //Se verifica si existe un tab seleccionado para la edición de un caso.
    if (GetEditModeStatus() == 'True') {
        if (sessionStorage.getItem('SelectedTab') != null) {
            selectedTab = sessionStorage.getItem('SelectedTab');
            $('.nav-tabs a[href="#' + selectedTab + '"]').tab('show')
            location.href = "#" + selectedTab
        }
    }
    else {
        sessionStorage.removeItem('SelectedTab');
    }

});

function EndCallbackHandler(s, e) { }
