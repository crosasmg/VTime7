
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function submitClick(s, e) {
   submit_Actions(s, e); 

}



function submit_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgsubmitInformationMessageResource;
        popupWindow.SetHeaderText(titlesubmitInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
