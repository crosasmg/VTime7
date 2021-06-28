
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function AcceptanceIndicatorValueChanged(s, e) {
   AcceptanceIndicator_Actions(s, e); 

}


function submitClick(s, e) {
   submit_Actions(s, e); 

}


function saveClick(s, e) {
   save_Actions(s, e); 

}



function AcceptanceIndicator_Actions(s,e) {
    
        if (AcceptanceIndicator.GetValue() === false){
ScriptCode
save.SetEnabled(false);

} 
 else { 
save.SetEnabled(true);

    } 

}
function submit_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 

} else
        e.processOnServer = false;

}
function save_Actions(s,e) {
    
        if(ASPxClientEdit.ValidateGroup(null)){ 

} else
        e.processOnServer = false;

}
