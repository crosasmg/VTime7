
function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function OtherDiagnosisCheckedChanged(s, e) { 
   OtherDiagnosis_Actions(s, e); 

}


function SymptomsAccompaniedByOtherValueChanged(s, e) {
   SymptomsAccompaniedByOther_Actions(s, e); 

}


function StillReceivingTreatmentValueChanged(s, e) {
   StillReceivingTreatment_Actions(s, e); 

}


function OtherCheckedChanged(s, e) { 
   Other_Actions(s, e); 

}


function button8Click(s, e) {
   button8_Actions(s, e); 

}


function button7Click(s, e) {
   button7_Actions(s, e); 

}



function OtherDiagnosis_Actions(s,e) {
    
        if (OtherDiagnosis.GetChecked() === true){
       DetailsSpecificDiagnosis.SetEnabled(true);
       DetailsSpecificDiagnosisLabel.SetEnabled(true);
} 
 else { 
       DetailsSpecificDiagnosis.SetEnabled(false);
       DetailsSpecificDiagnosisLabel.SetEnabled(false);
    } 

}
function SymptomsAccompaniedByOther_Actions(s,e) {
    
        if (SymptomsAccompaniedByOther.GetValue() === true){
       DescribeBodySymptoms.SetEnabled(true);
       DescribeBodySymptomsLabel.SetEnabled(true);
} 
 else { 
       DescribeBodySymptoms.SetEnabled(false);
       DescribeBodySymptomsLabel.SetEnabled(false);
    } 

}
function StillReceivingTreatment_Actions(s,e) {
    
        if (StillReceivingTreatment.GetValue() === true){
       DetailsStillRecievingTreatment.SetEnabled(true);
       DetailsStillRecievingTreatmentLabel.SetEnabled(true);
} 
 else { 
       DetailsStillRecievingTreatment.SetEnabled(false);
       DetailsStillRecievingTreatmentLabel.SetEnabled(false);
    } 

}
function Other_Actions(s,e) {
    
        if (Other.GetChecked() === true){
       SpecifyOther.SetEnabled(true);
       SpecifyOtherLabel.SetEnabled(true);
} 
 else { 
       SpecifyOther.SetEnabled(false);
       SpecifyOtherLabel.SetEnabled(false);
    } 

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
function button7_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup(null)){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton7InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton7InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
