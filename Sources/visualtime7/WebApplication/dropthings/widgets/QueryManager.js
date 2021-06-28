var varvisibleIndex;
var varvisibleIndexImage;
var htmlEventX;
var htmlEventY;
var valueNote;
var windowsChildrens = new Array();

jQuery(document).ready(function () {
    $(window).on('unload', function () {
        if (windowsChildrens.length != 0) {
            windowsChildrens.forEach(function (element) {
                if (element && !element.closed) {
                    element.close();
                }
            });
        }
    });
});

function CustomizeColumnsBtn_Click(s, e) {
    if (GridViewQueries.IsCustomizationWindowVisible())
        GridViewQueries.HideCustomizationWindow();
    else
        GridViewQueries.ShowCustomizationWindow();
    UpdateButtonText();
}
function GridView_CustomizationWindowCloseUp(s, e) {
    UpdateButtonText();
}
function UpdateButtonText() {
    var text = GridViewQueries.IsCustomizationWindowVisible() ? "Add" : "Add";
    text += " Columns";
    CustomizeColumnsBtn.SetText(text);
}

function insGoToQuery(RefUrl) {
    var lstrURL = RefUrl.substr(RefUrl.indexOf('sCodispl=') + 9);
    var lintLength = lstrURL.indexOf('&');
    var lstrCodispl = lstrURL.substr(0, lintLength);
    var win = open(RefUrl, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
    win.moveTo(0, 0);
    windowsChildrens.push(win);
    win.resizeTo(window.screen.availWidth, window.screen.availHeight);
}

function windowOpenUrl(url, target, width, height, resizable, scrollbars) {
    var left = (screen.width) ? (screen.width - width) / 2 : 0;
    var top = (screen.height) ? (screen.height - height) / 2 : 0;
    var windowsPopup;
    if (width > 0 && height > 0) {
        windowsPopup = window.open(url, target, "width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + ",resizable=" + resizable + ",scrollbars=" + scrollbars);
    }
    else {
        windowsPopup = window.open(url, target, "resizable=" + resizable + ",scrollbars=" + scrollbars);
    }
    if (target !== '_self')
        windowsChildrens.push(windowsPopup);
    return;
}

function WindowOpenUrl2(url, target, left, top, width, height, resizable, scrollbars) {
    if (left == 0) {
        left = htmlEventX + 5;
    }
    if (top == 0) {
        top = htmlEventY + 5;
    }

    if (target == 'popup') {
        PopUpImageDetail.SetWidth(width);
        PopUpImageDetail.SetHeight(height);
        PopUpImageDetail.ShowAtPos(left, top);
        PopUpImageDetail.SetContentUrl(url);
        PopUpImageDetail.Show();
    }
    else {
        if (width > 0 && height > 0)
            window.open(url, target, "width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + ",resizable=" + resizable + ",scrollbars=" + scrollbars);
        else
            window.open(url, target);
    }
    return;
}

function ShowImageDetail(element, imageId) {
    if (PopUpNoteDetail.IsVisible()) PopUpNoteDetail.Hide();
    GridViewImageDetail.PerformCallback(imageId);
    PopUpImageDetail.Show();
}

// Es llamada desde el grid que está en la PopUp del ImageDetail
function ShowImagePreview(visibleIndex, ColumnName) {
    if (varvisibleIndex != null) visibleIndex = varvisibleIndex;
    //GridViewQueries.GetRowValues(visibleIndex, 'nConsec;sDescript;', OnGetRowValues);
    PopupImagePreview.SetHeaderText('');
    PopupImagePreview.ShowAtPos(0, 0);
    cpPopupImagePreview.PerformCallback(visibleIndex + '-' + 'ShowImagePreview' + '-' + ColumnName);
}

function ShowNoteDetail(element, noteId) {
    if (PopUpImageDetail.IsVisible()) PopUpImageDetail.Hide();
    GridViewNoteDetail.PerformCallback(noteId);
    PopUpNoteDetail.Show();
}

function ShowNotePreview(visibleIndex, value, ColumnName) {
    if (varvisibleIndex != null) visibleIndex = varvisibleIndex;
    GridViewQueries.GetRowValues(visibleIndex, ColumnName + ';', OnGetRowValues);
    PopupNotePreview.SetHeaderText('');
    PopupNotePreview.SetContentHTML('<br /><br />');
    PopupNotePreview.ShowAtPos(0, 0);
}

function OnGetRowValues(values) {
    PopupNotePreview.SetContentHTML(values[0]);
}

function ShowNotePreviewRTF(visibleIndex, ColumnName) {
    if (varvisibleIndex != null) visibleIndex = varvisibleIndex;
    GridViewQueries.GetRowValues(visibleIndex, ColumnName + ';', OnGetRowValuesRTF);
}

function OnGetRowValuesRTF(values) {
    valueNote = values[0];
    OpenWord();
}

function setVisibleIndex(s, e) {
    varvisibleIndex = e.visibleIndex;
    htmlEventX = ASPxClientUtils.GetEventX(e.htmlEvent);
    htmlEventY = ASPxClientUtils.GetEventY(e.htmlEvent);
}

// Es llamada desde el grid que está en la PopUp del ImageDetail
function ShowImageFull(ColumnName) {
    //GridViewImageDetail.GetRowValues(varvisibleIndexImage, 'nConsec;sDescript;', OnGetRowValues);
    PopupImagePreview.SetHeaderText('');
    PopupImagePreview.ShowAtPos(0, 0);
    cpPopupImagePreview.PerformCallback(varvisibleIndexImage + '-' + 'ShowImageFull' + '-' + ColumnName);
}

function setVisibleIndexImage(s, e) {
    varvisibleIndexImage = e.visibleIndex;
}

/*+ Begin: MenuActions */
function ShowPopupMenuActions(el, q, e) {
    GridViewQueries.PerformCallback('Actions' + ',' + varvisibleIndex + ',' + q + ',' + e);
}

function GridViewQueries_EndCallback(s, e) {
    if (GridViewQueries.cp_WithActions) {
        SetItem(PopupMenuActions.GetItemByName("Item0"), GridViewQueries.cp_Item0_Name, GridViewQueries.cp_Item0_Url, GridViewQueries.cp_Item0_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item1"), GridViewQueries.cp_Item1_Name, GridViewQueries.cp_Item1_Url, GridViewQueries.cp_Item1_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item2"), GridViewQueries.cp_Item2_Name, GridViewQueries.cp_Item2_Url, GridViewQueries.cp_Item2_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item3"), GridViewQueries.cp_Item3_Name, GridViewQueries.cp_Item3_Url, GridViewQueries.cp_Item3_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item4"), GridViewQueries.cp_Item4_Name, GridViewQueries.cp_Item4_Url, GridViewQueries.cp_Item4_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item5"), GridViewQueries.cp_Item5_Name, GridViewQueries.cp_Item5_Url, GridViewQueries.cp_Item5_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item6"), GridViewQueries.cp_Item6_Name, GridViewQueries.cp_Item6_Url, GridViewQueries.cp_Item6_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item7"), GridViewQueries.cp_Item7_Name, GridViewQueries.cp_Item7_Url, GridViewQueries.cp_Item7_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item8"), GridViewQueries.cp_Item8_Name, GridViewQueries.cp_Item8_Url, GridViewQueries.cp_Item8_Visible);
        SetItem(PopupMenuActions.GetItemByName("Item9"), GridViewQueries.cp_Item9_Name, GridViewQueries.cp_Item9_Url, GridViewQueries.cp_Item9_Visible);

        GridViewQueries.cp_WithActions = false;
        PopupMenuActions.ShowAtPos(htmlEventX, htmlEventY);
    }
}

/*+ End: MenuActions */

/*+ Begin: MenuMailTo */
function ShowPopupMenuMailTo(el, text) {
    GridViewQueries.GetRowValues(varvisibleIndex, 'SE_MAIL;', OnGetMenuMailToValues);
    PopupMenuMailTo.ShowAtPos(htmlEventX, htmlEventY);
}

function OnGetMenuMailToValues(values) {
    SetItem(PopupMenuMailTo.GetItemByName("ItemMailTo"), values[0], "mailto:" + values[0], true);
}
/*+ End: MenuMailTo */

/*+ Begin: MenuCallTo */
/* 'TODO: se debe corregir la consulta para que se pueda colocar la tabla PHONES como hija de CLIENT, luego de eso se debe probar este manejo */
function ShowPopupMenuCallTo(el, text) {
    GridViewQueries.GetRowValues(varvisibleIndex, 'NCOUNTRY_CODE;NAREA_CODE;SPHONE;', OnGetMenuMessengerValues);
    PopupMenuCallTo.ShowAtPos(htmlEventX, htmlEventY);
}

function OnGetMenuCallToValues(values) {
    SetItem(PopupMenuCallTo.GetItemByName("ItemCallSkype"), "Skype", "callto:+" + values[0] + values[1] + values[2], true);
    SetItem(PopupMenuCallTo.GetItemByName("ItemCallMsn"), "Msn", "livecall:+" + values[0] + values[1] + values[2], true);
}
/*+ End: MenuCallTo */

/*+ Begin: MenuMessenger */
function ShowPopupMenuMessenger(el) {
    GridViewQueries.GetRowValues(varvisibleIndex, 'SE_MAIL;', OnGetMenuMessengerValues);
    PopupMenuMessenger.ShowAtPos(htmlEventX, htmlEventY);
}

function OnGetMenuMessengerValues(values) {
    SetItem(PopupMenuMessenger.GetItemByName("ItemSkype"), "Skype", "skype:" + values[0], true);
    SetItem(PopupMenuMessenger.GetItemByName("ItemMsn"), "Msn", "msnim:chat?contact=" + values[0], true);
    SetItem(PopupMenuMessenger.GetItemByName("ItemAim"), "Aim", "aim:goim?screenname=" + values[0], false);
    SetItem(PopupMenuMessenger.GetItemByName("ItemYmsgr"), "ItemYmsgr", "ymsgr:sendim?" + values[0], false);
}
/*+ End: MenuMessenger */

function SetItem(itm, text, url, visible) {
    itm.SetText(text);
    itm.SetNavigateUrl(url);
    itm.SetVisible(visible);
}

var textSeparator = ";";
function OnListBoxSelectionChanged(listBox, args) {
    UpdateText();
}
function UpdateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(GetSelectedItemsText(selectedItems));
}
function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        texts.push(ASPxClientUtils.TrimEnd(items[i].text));
    return texts.join(textSeparator);
}

function OpenWord() {
    strFileName = '';
    clsFileSystem = new ActiveXObject('Scripting.FileSystemObject');
    strFileName = clsFileSystem.GetSpecialFolder(2).Path + '\\' + clsFileSystem.GetTempName() + '.rtf';
    clsFileSystem.CreateTextFile(strFileName, true);
    clsFile = clsFileSystem.OpenTextFile(strFileName, 2, true)
    clsFile.write(valueNote);
    clsFile.close();

    var clsWorkApplication = new ActiveXObject("Word.Application");
    clsWorkApplication.visible = true;
    clsWorkApplication.activate();

    try {
        clsWorkApplication.Documents.open(strFileName, false, true)
    }
    catch (oCatch) {
    }
    finally { };
}

function CheckUncheckRows() {
    var isSelected = DataRowsCheckBox.GetValue();

    if (isSelected == true) {
        GridViewQueries.SelectRows();
        DataRowsCheckBox.SetText('<%=GetGlobalResourceObject("Resource", "UnCheckAll") %>');
    }
    else {
        GridViewQueries.UnselectRows();
        DataRowsCheckBox.SetText('<%=GetGlobalResourceObject("Resource", "CheckAll") %>');
    }
}