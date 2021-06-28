function AsyncPostBack(s, e) {
    Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}
function IrClick(s, e) {
   Ir_Actions(s, e); 

}



function Ir0_WebCall(specialParameterUrl) {

    var win = window.open(@@URL@@, 'Ir0', 'scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=900,height=900,left=0,top=0');
    $('body').append(win);
    $(win).submit();
}
function Ir1_WebCall(specialParameterUrl) {

    var win = window.open(@@URL@@, 'Ir1', 'scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=900,height=900,left=0,top=0');
    $('body').append(win);
    $(win).submit();
}
function Ir2_WebCall(specialParameterUrl) {

    var win = window.open(@@URL@@, 'Ir2', 'scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=900,height=900,left=0,top=0');
    $('body').append(win);
    $(win).submit();
}
function Ir3_WebCall(specialParameterUrl) {

    var win = window.open(@@URL@@, 'Ir3', 'scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=900,height=900,left=0,top=0');
    $('body').append(win);
    $(win).submit();
}
function Ir_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgIrInformationMessageResource;
        popupWindow.SetHeaderText(titleIrInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
