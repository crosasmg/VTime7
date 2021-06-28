var PaymentFrequency_ForSet = 0;
var RiskInformationProductCode_ForSet = 0;

function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function PaymentFrequencyBeginCallback(s, e) {
    
    PaymentFrequencyBeginCallbackDependency(s, e);
}


function PaymentFrequencyEndCallback(s, e) {
    PaymentFrequencyEndCallbackDependency(s, e);
}


function PaymentMethodSelectedIndexChanged(s, e) {
   PaymentMethodDependencySelectedIndexChanged(s, e);
}


function CotizarClick(s, e) {
   Cotizar_Actions(s, e); 

}


function AceptoClick(s, e) {
   Acepto_Actions(s, e); 

}


function LineOfBusinessSelectedIndexChanged(s, e) {
   LineOfBusinessDependencySelectedIndexChanged(s, e);
}


function RiskInformationProductCodeBeginCallback(s, e) {
    
    RiskInformationProductCodeBeginCallbackDependency(s, e);
}


function RiskInformationProductCodeEndCallback(s, e) {
    RiskInformationProductCodeEndCallbackDependency(s, e);
}



function PaymentFrequencyBeginCallbackDependency(s, e) { 
    s.ClearItems();
    s.SetEnabled(false); 
    s.SetText(msgControlsDependencyResource); 
} 


function PaymentFrequencyEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
    if (PaymentFrequency_ForSet != 0){ 
        s.SetValue(PaymentFrequency_ForSet); 
        PaymentFrequency_ForSet = 0; 
    } 
} 


    function PaymentMethodDependencySelectedIndexChanged(s, e) {       
     PaymentFrequency.PerformCallback(LineOfBusiness.GetValue().toString() + ',' + RiskInformationProductCode.GetValue().toString() + ',' + PaymentMethod.GetValue().toString());



}

function Cotizar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgCotizarInformationMessageResource;
        popupWindow.SetHeaderText(titleCotizarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function Acepto_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgAceptoInformationMessageResource;
        popupWindow.SetHeaderText(titleAceptoInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
    function LineOfBusinessDependencySelectedIndexChanged(s, e) {       
     RiskInformationProductCode.PerformCallback(LineOfBusiness.GetValue().toString());



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


