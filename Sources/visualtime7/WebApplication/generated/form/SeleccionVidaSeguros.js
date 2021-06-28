
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  

