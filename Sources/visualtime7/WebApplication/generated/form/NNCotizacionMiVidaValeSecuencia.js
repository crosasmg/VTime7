
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function button31Click(s, e) {
   button31_Actions(s, e); 

}


function button32Click(s, e) {
   button32_Actions(s, e); 

}


function button34Click(s, e) {
   button34_Actions(s, e); 

}


function button35Click(s, e) {
   button35_Actions(s, e); 

}


function button36Click(s, e) {
   button36_Actions(s, e); 

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


function btnSalirSinGuardarClick(s, e) {
   btnSalirSinGuardar_Actions(s, e); 

}



function button31_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton31InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton31InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button32_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton32InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton32InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button34_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton34InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton34InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button35_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton35InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton35InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button36_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton36InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton36InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button19_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton19InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton19InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function EnviarCotizacionEmail_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgEnviarCotizacionEmailInformationMessageResource;
        popupWindow.SetHeaderText(titleEnviarCotizacionEmailInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


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
function btnSalirSinGuardar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnSalirSinGuardarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnSalirSinGuardarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
