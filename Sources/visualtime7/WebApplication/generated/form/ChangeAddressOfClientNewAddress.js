function ClientIDValidation(s, e) {
   ClientIDBigDataSet(s, e); 
}


function SendClick(s, e) {
   Send_Actions(s, e); 

}



function ClientIDBigDataSet(s, e) { 
    if (s.GetText() != '' && s.GetSelectedItem() == null) { 
        e.isValid = false; 
        e.errorText = FormResources.Get('ComboBoxErrorText').replace('@Value@', e.value); 
        e.value = ''; 
    }} 


function ClientIDEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
} 


function Send_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgSendInformationMessageResource;
        popupWindow.SetHeaderText(titleSendInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
