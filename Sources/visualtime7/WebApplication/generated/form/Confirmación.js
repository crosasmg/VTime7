
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}
function button3Click(s, e) {
   button3_Actions(s, e); 

}


function button5Click(s, e) {
   button5_Actions(s, e); 

}



function button3_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton3InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton3InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button5_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton5InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton5InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);
window.close();

        popupWindow.popupControl.HideWindow(popupWindow);
} else
        e.processOnServer = false;

}
