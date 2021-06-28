function btnSaveQuestionClick(s, e) {
   btnSaveQuestion_Actions(s, e); 

}


function btnCanClick(s, e) {
   btnCan_Actions(s, e); 

}


function btnSaClick(s, e) {
   btnSa_Actions(s, e); 

}



function btnSaveQuestion_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnSaveQuestionInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnSaveQuestionInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function btnCan_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnCanInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnCanInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function btnSa_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnSaInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnSaInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
