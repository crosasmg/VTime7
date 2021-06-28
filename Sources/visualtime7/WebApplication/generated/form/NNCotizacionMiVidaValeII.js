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


function ClienteInformaEsUsuarioValueChanged(s, e) {
   ClienteInformaEsUsuario_Actions(s, e); 

}


function btnAutenticarClick(s, e) {
   btnAutenticar_Actions(s, e); 

}


function button8Click(s, e) {
   button8_Actions(s, e); 

}


function button14Click(s, e) {
   button14_Actions(s, e); 

}


function button1Click(s, e) {
   button1_Actions(s, e); 

}


function button33Click(s, e) {
   button33_Actions(s, e); 

}


function button12Click(s, e) {
   button12_Actions(s, e); 

}


function buttonGPagoClick(s, e) {
   buttonGPago_Actions(s, e); 

}


function button0Click(s, e) {
   button0_Actions(s, e); 

}


function button19Click(s, e) {
   button19_Actions(s, e); 

}


function EnviarCotizacionEmailClick(s, e) {
   EnviarCotizacionEmail_Actions(s, e); 

}


function AcceptClick(s, e) {
   Accept_Actions(s, e); 

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
function ClienteInformaEsUsuario_Actions(s,e) {
    
        if (ClienteInformaEsUsuario.GetValue() == true){
zone38.SetVisible(true);

} 
 else { 
zone38.SetVisible(false);

    } 

}
function btnAutenticar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnAutenticarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnAutenticarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button8_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton8InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton8InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button14_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton14InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton14InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button1_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton1InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton1InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button33_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton33InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton33InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button12_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton12InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton12InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function buttonGPago_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbuttonGPagoInformationMessageResource;
        popupWindow.SetHeaderText(titlebuttonGPagoInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button0_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton0InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton0InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button1918_WebCall(urlForm, formId) {

    var win = window.open(urlForm + '/NNCotizacionVidaResumenPopup.aspx?fromid='+ formId, 'button1918', 'scrollbars=no,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=1000,height=1000,left=100,top=100');
    $('body').append(win);
    $(win).submit();
}
function button19_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton19InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton19InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function EnviarCotizacionEmail_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgEnviarCotizacionEmailInformationMessageResource;
        popupWindow.SetHeaderText(titleEnviarCotizacionEmailInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function Accept_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgAcceptInformationMessageResource;
        popupWindow.SetHeaderText(titleAcceptInformationMessageResource);
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


