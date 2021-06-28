var ProductCode_ForSet = 0;
var CityCode_ForSet = 0;
var MunicipalityCode_ForSet = 0;

function AsyncPostBack(s, e) {
   Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    
    popupWindow.popupControl.HideWindow(popupWindow);
       
}
  
function ProductCodeBeginCallback(s, e) {
    
    ProductCodeBeginCallbackDependency(s, e);
}


function ProductCodeEndCallback(s, e) {
    ProductCodeEndCallbackDependency(s, e);
}


function LineOfBusinessSelectedIndexChanged(s, e) {
   LineOfBusinessDependencySelectedIndexChanged(s, e);
}


function StateOrProvinceSelectedIndexChanged(s, e) {
   StateOrProvinceDependencySelectedIndexChanged(s, e);
}


function CityCodeBeginCallback(s, e) {
    
    CityCodeBeginCallbackDependency(s, e);
}


function CityCodeEndCallback(s, e) {
    CityCodeEndCallbackDependency(s, e);
}


function CityCodeSelectedIndexChanged(s, e) {
   CityCodeDependencySelectedIndexChanged(s, e);
}


function MunicipalityCodeBeginCallback(s, e) {
    
    MunicipalityCodeBeginCallbackDependency(s, e);
}


function MunicipalityCodeEndCallback(s, e) {
    MunicipalityCodeEndCallbackDependency(s, e);
}


function btnAgregarClick(s, e) {
   btnAgregar_Actions(s, e); 

}


function btnEditarClick(s, e) {
   btnEditar_Actions(s, e); 

}


function btnEliminarClick(s, e) {
   btnEliminar_Actions(s, e); 

}


function button2Click(s, e) {
   button2_Actions(s, e); 

}


function button3Click(s, e) {
   button3_Actions(s, e); 

}


function btnAplicarClick(s, e) {
   btnAplicar_Actions(s, e); 

}


function button13Click(s, e) {
   button13_Actions(s, e); 

}


function button14Click(s, e) {
   button14_Actions(s, e); 

}


function cerrarClick(s, e) {
   cerrar_Actions(s, e); 

}



function ProductCodeBeginCallbackDependency(s, e) { 
    s.ClearItems();
    s.SetEnabled(false); 
    s.SetText(msgControlsDependencyResource); 
} 


function ProductCodeEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
    if (ProductCode_ForSet != 0){ 
        s.SetValue(ProductCode_ForSet); 
        ProductCode_ForSet = 0; 
    } 
} 


    function LineOfBusinessDependencySelectedIndexChanged(s, e) {       
     ProductCode.PerformCallback(LineOfBusiness.GetValue().toString());



}

    function StateOrProvinceDependencySelectedIndexChanged(s, e) {       
     CityCode.PerformCallback(StateOrProvince.GetValue().toString());



}

    function CityCodeDependencyCallback(s, e) {       
     MunicipalityCode.PerformCallback(CityCode.GetValue().toString());
   }

    function CityCodeDependencySelectedIndexChanged(s, e) {       
     MunicipalityCode.PerformCallback(CityCode.GetValue().toString());



}

function CityCodeBeginCallbackDependency(s, e) { 
    s.ClearItems();
    s.SetEnabled(false); 
    s.SetText(msgControlsDependencyResource); 
} 


function CityCodeEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
    if (CityCode_ForSet != 0){ 
        s.SetValue(CityCode_ForSet); 
        CityCode_ForSet = 0; 
    } 
} 


function MunicipalityCodeBeginCallbackDependency(s, e) { 
    s.ClearItems();
    s.SetEnabled(false); 
    s.SetText(msgControlsDependencyResource); 
} 


function MunicipalityCodeEndCallbackDependency(s, e) { 
    s.SetEnabled(true);
    if (s.GetText() == msgControlsDependencyResource) 
        s.SetText(''); 
    if (MunicipalityCode_ForSet != 0){ 
        s.SetValue(MunicipalityCode_ForSet); 
        MunicipalityCode_ForSet = 0; 
    } 
} 


function btnAgregar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('DontValidate')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnAgregarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnAgregarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function btnEditar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('zone8')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnEditarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnEditarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function btnEliminar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('zone8')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnEliminarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnEliminarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button2_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('NoteAddEdit')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton2InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton2InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button3_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('zone9')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton3InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton3InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function btnAplicar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('RecargosMedicos')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnAplicarInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnAplicarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button13_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('zone12')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton13InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton13InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function button14_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
    if(ASPxClientEdit.ValidateGroup('zone12')){ 
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbutton14InformationMessageResource;
        popupWindow.SetHeaderText(titlebutton14InformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);

} else
        e.processOnServer = false;

}
function cerrar_Actions(s,e) {
    
    var popupWindow = popControl.GetWindowByName('pwUno');
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgcerrarInformationMessageResource;
        popupWindow.SetHeaderText(titlecerrarInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);
var result = confirm("¿Desea cerrar el formulario sin guardar la información?");
if (result) {
    window.close();
}

        popupWindow.popupControl.HideWindow(popupWindow);

}
