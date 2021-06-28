var RiskInformationProductCode_ForSet = 0;

function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function RiskInformationProductCodeBeginCallback(s, e) {
    
    RiskInformationProductCodeBeginCallbackDependency(s, e);
}


function RiskInformationProductCodeEndCallback(s, e) {
    RiskInformationProductCodeEndCallbackDependency(s, e);
}


function LineOfBusinessSelectedIndexChanged(s, e) {
   LineOfBusinessDependencySelectedIndexChanged(s, e);
}


function button0FinalizaClick(s, e) {
   button0Finaliza_Actions(s, e); 

}



function RiskInformationProductCodeBeginCallbackDependency(s, e) { 
    s.ClearItems();
    s.SetEnabled(false); 
    s.SetText(msgControlsDependencyResource); 
} 


function RiskInformationProductCodeEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
    if (RiskInformationProductCode_ForSet != 0){ 
        s.SetValue(RiskInformationProductCode_ForSet); 
        RiskInformationProductCode_ForSet = 0; 
    } 
} 


    function LineOfBusinessDependencySelectedIndexChanged(s, e) {       
     RiskInformationProductCode.PerformCallback(LineOfBusiness.GetValue().toString());



}

function button0Finaliza_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton0FinalizaInformationMessageResource;
        popupWindow.SetHeaderText(titlebutton0FinalizaInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
