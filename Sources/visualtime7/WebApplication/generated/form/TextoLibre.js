function AsyncPostBack(s, e) {
    Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}
function button1Click(s, e) {
   button1_Actions(s, e); 

}



function button1_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton1InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton1InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
