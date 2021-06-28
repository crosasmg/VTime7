function AsyncPostBack(s, e) {
    Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}
function btnPrintClick(s, e) {
   btnPrint_Actions(s, e); 

}



function btnPrint_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnPrintInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnPrintInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);
window.print();

        popupWindow.popupControl.HideWindow(popupWindow);
} else
        e.processOnServer = false;

}
