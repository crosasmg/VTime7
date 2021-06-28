
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function btnCotizarFinalClick(s, e) {
   btnCotizarFinal_Actions(s, e); 

}


function button12Click(s, e) {
   button12_Actions(s, e); 

}


function button3Click(s, e) {
   button3_Actions(s, e); 

}


function button5Click(s, e) {
   button5_Actions(s, e); 

}


function button13Click(s, e) {
   button13_Actions(s, e); 

}


function button14Click(s, e) {
   button14_Actions(s, e); 

}



function btnCotizarFinal_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnCotizarFinalInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnCotizarFinalInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);


}
function button12_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 

} else
        e.processOnServer = false;

}
function button3_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 
zone2daParte.SetVisible(true);
zone1raParte.SetVisible(false);


} else
        e.processOnServer = false;

}
function button5_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 
zone2daParte.SetVisible(false);
zone1raParte.SetVisible(true);


} else
        e.processOnServer = false;

}
function button13_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton13InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton13InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button14_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 

} else
        e.processOnServer = false;

}
