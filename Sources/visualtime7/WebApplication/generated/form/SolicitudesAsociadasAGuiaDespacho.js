function AsyncPostBack(s, e) {
    Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}
function button16Click(s, e) {
   button16_Actions(s, e); 

}



function button16_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton16InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton16InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
