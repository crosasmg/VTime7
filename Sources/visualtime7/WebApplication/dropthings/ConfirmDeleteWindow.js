// BEGIN CONFIRMDELETE
var dontAskConfirmation;
var RowIndex;
var grid;
var popupDelete;
function popupDelete_Init(popup) {
    popupDelete = popup;    
    dontAskConfirmation = cbDontAsk.GetChecked();
}

function cbDontAsk_CheckedChanged(cbDontAsk) {
    dontAskConfirmation = cbDontAsk.GetChecked();
}

function DeleteButton_Click(index, gridView) {
    RowIndex = index;
    grid = gridView;
    if (dontAskConfirmation)
        grid.DeleteRow(index);
    else {
        popupDelete.Show();
    }
}

function btnYes_Click() {
    grid.DeleteRow(RowIndex)
    popupDelete.Hide();
}

function btnNo_Click(s, e) {
    popupDelete.Hide();
}
// END CONFIRMDELETE