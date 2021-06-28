
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function TelephoneTypeOnValidation(s, e) {
        TelephoneTypeValidations_Actions(s, e); 
}


function AreaCodeOnValidation(s, e) {
        AreaCodeValidations_Actions(s, e); 
}


function PhoneNumberOnValidation(s, e) {
        PhoneNumberValidations_Actions(s, e); 
}



function TelephoneTypeValidations_Actions(s,e) {
    
        if (Phone.GetEditValue("TelephoneType") == null){
e.isValid = false;
e.errorText = "Debe ingresar el tipo de teléfono.";
    } 

}
function AreaCodeValidations_Actions(s,e) {
    
        if (Phone.GetEditValue("AreaCode") == null || Phone.GetEditValue("AreaCode") == 0){
e.isValid = false;
e.errorText = "Debe de ingresar el area del telefono";
    } 

}
function PhoneNumberValidations_Actions(s,e) {
    
        if (Phone.GetEditValue("PhoneNumber") == null){
e.isValid = false;
e.errorText = "Debe de ingresar el numero de telefono";
    } 

}
