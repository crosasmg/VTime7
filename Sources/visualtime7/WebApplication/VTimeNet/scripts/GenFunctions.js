//--------------------------------------------------------------------
//- $$Workfile: GenFunctions.js $ 
//- $$Author: Nvaplat53 $ 
//- $$Date: 3/09/04 13:19 $ 
//- $$Revision: 21 $ 
//--------------------------------------------------------------------

//- Variable global que almacena el tipo simbolo utilizado moco decila (este es tomado del registro el cual se almacena en una variable de session)
var  mstrSrvDecSep
var  mstrUsrDecSep

//-------------------------------------------------------------------------------------------
//+ GenFunctions.js:  Se definen las rutinas generales del proyecto
//-------------------------------------------------------------------------------------------

	var marrControls = new Array();	
	var windows = new Array();
	var mblnShowValues=true;

	//var moMSGGenFunctions = new Object;

//-	Variable utilizada para el manejo de error de la imagen
	var mstrpath_src = ""

//	moMSGGenFunctions.c_10100 = resValues.moMSGGenFunctions_c_10100;  //"Se deben indicar todos los parámetros";
//	moMSGGenFunctions.c_10101 = resValues.moMSGGenFunctions_c_10101;  //"El campo ";
//	moMSGGenFunctions.c_10102 = resValues.moMSGGenFunctions_c_10102;  //" debe ser numérico";

//+ Se captura la tecla ENTER y ESC del teclado.
//+ El ENTER no ejecuta ninguna acción
//+ El ESC ejecuta la acción Cancelar de la página.  Si se trata de una ventana PopUp, se 
//+ cierra la ventana.
	if (document.layers)
		document.captureEvents(Event.KEYPRESS);
	document.onkeypress = function (evt){
							  var key = document.all ? event.keyCode : evt.which ? evt.which : evt.keyCode;
							  if((document.activeElement.tagName!='TEXTAREA')&&(document.activeElement.tagName!='A'))
							      if (typeof(top.fraHeader)=='undefined'){
							          if (key == 13)
								          return(false);
                                      if (key == 27)
                                          window.close();
							      }
							      else{
								      if (key == 13)
								          if(typeof(top.fraHeader.document.images["A390"])!='undefined')
										      if(!top.fraHeader.document.images["A390"].disabled)
											      return(false);
                                      if (key == 27)
                                          if(typeof(top.fraHeader.document.images["A391"])!='undefined')
                                              if(!top.fraHeader.document.images["A391"].disabled){
                                                  event.cancelBubble=true
                                                  top.fraHeader.ClientRequest(391,1);
                                              }
                                  }            
                          }

//-------------------------------------------------------------------------------------------
function insDisableHeader(){
//-------------------------------------------------------------------------------------------
    var lintIndex;
    var error;
    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=true;
			if(self.document.images.length>0){
			    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled
			 
			    if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled
			}
		}

	} catch(error){}
}

//-------------------------------------------------------------------------------------------
function insAbout(sCodispl, sCodisp){
//-------------------------------------------------------------------------------------------
    var lstrVersion;
    
    if(typeof(self.document.VssVersion)!='undefined'){
		lstrVersion = self.document.VssVersion
		lstrVersion = lstrVersion.replace("\$\$\Revision: ","&VSSVersion=")
		lstrVersion = lstrVersion.replace(" \$\|\$\$\Date:","&VSSVersionDate=")
		lstrVersion = lstrVersion.replace(/ .*/,'')           
	}	
    ShowPopUp("/VTimeNet/Common/about.aspx?sCodispl=" + sCodispl + "&sCodisp=" + sCodisp + lstrVersion,"HelpAbout",300,160,"No","No",50,30);
    

}

//-------------------------------------------------------------------------------------------
function insReloadTop(bOpener){
//-------------------------------------------------------------------------------------------
    if (!bOpener)
        top.document.location.href = top.document.location.href.replace("InSequence","");
    else
        opener.top.document.location.href =  opener.top.document.location.href.replace("InSequence","");
}

//--------------------------------------------------------------
function setObjectValue(control, value){
//--------------------------------------------------------------
    control.value = value;
    $(control).change();
}    
//--------------------------------------------------------------
//% closeWindows: Cierra todas las ventanas abiertas con addWindow.
//				  Para cerrar una ventana concreta se llama a su método close.
//				  Esta función debe ser llamada durante el evento ONUNLOAD para cerrar automáticamente
//                todas las ventanas abiertas. 
//--------------------------------------------------------------------------------------------------------------------------------
function closeWindows() {
//--------------------------------------------------------------------------------------------------------------------------------
	for (frmWinName in windows){
        if (!windows[frmWinName].closed)
            windows[frmWinName].close();
    }
}
//% ShowPopUp: Muestra una ventana PopUp
//--------------------------------------------------------------------------------------------------------------------------------
function ShowPopUp(Url, Name, Width , Height, ScrollOn, Sizable, Left, Top, Status, Toolbar) {
//--------------------------------------------------------------------------------------------------------------------------------
	if(typeof(Top)=='undefined'){
		Top = 200
	}
	if(typeof(Left)=='undefined'){
		Left = 100
	}	
	if(typeof(Status)=='undefined'){
		Status = 'no'
	}	
	if(typeof(Toolbar)=='undefined'){
		Toolbar = 'no'
	}
	windows[Name] = window.open(Url, Name, 
	"toolbar=" + Toolbar + ",location=no,directories=no,status=" + Status + ",menubar=no,scrollbars=" + ScrollOn + ",copyhistory=no,resizable=" + Sizable + ",width=" + Width + ",height=" + Height + ",left=" + Left + ",top=" + Top,false);
}

//% EnabledControl: habilita todos los controles de la forma
//-------------------------------------------------------------------------------------------
function EnabledControl(lstrZone){
//-------------------------------------------------------------------------------------------	
	var lintlenght
	var lobjelement
    try {
		lobjelement = top.frames[lstrZone].document.forms[0].elements;
		lintlenght = lobjelement.length;
        for(var lintIndex=0;lintIndex<lintlenght;lintIndex++){
	    	lobjelement[lintIndex].disabled=false;
        }    
    }
    catch(e){
		lobjelement = self.document.forms[0].elements
		lintlenght = lobjelement.length-1;
        for(var lintIndex=0;lintIndex<lintlenght;lintIndex++){
	    	lobjelement[lintIndex].disabled=false;
	    }    
    }
} 

//% ShowValues: Carga la ventana de Valores posibles
//--------------------------------------------------------------------------------------------------------------------------------
function ShowValues(Control, bCheckCode, bShowDescript, bAllowInvalid){
//--------------------------------------------------------------------------------------------------------------------------------
    var lstrQueryString;
    var lstrShowDescript;
    var lstrAllowInvalid
    var lobjParam;
    var lintIndex;

    if (bShowDescript == 'undefined') bShowDescript = true;
    if (bAllowInvalid == 'undefined') bAllowInvalid = false;
    if (!mblnShowValues) return 0;
    if (typeof(Control.CanShowValues)=='undefined') return 0;

	if (bShowDescript) lstrShowDescript = "1";
	else lstrShowDescript = "0";

	if (bAllowInvalid) lstrAllowInvalid = "1"
	else lstrAllowInvalid = "0"

    lstrQueryString = "?sName=" + Control.name + "&nCount=" + Control.Parameters.nCount + "&nRCount=" + Control.RParameters.nCount + "&sTabname=" + Control.sTabName + "&sShowDescript=" + lstrShowDescript + "&List=" + Control.List + "&TypeList=" + Control.TypeList + "&TypeOrder=" + Control.TypeOrder + "&sAllowInvalid=" + lstrAllowInvalid;
    
    for (lintIndex=1;lintIndex<=Control.Parameters.nCount;lintIndex++){
        try{eval("lobjParam = Control.Parameters.Param" + lintIndex + ";");}
        catch(e){alert('falló');return false;}
        finally{}
        if (lobjParam.sValue == "VT_EMPTY") {
           alert("Se deben indicar todos los parámetros");
           return false;
        }
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sName=" + lobjParam.sName;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sValue=" + lobjParam.sValue;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sDirection=" + lobjParam.sDirection;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sParType=" + lobjParam.sParType;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sSize=" + lobjParam.sSize;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sNumericScale=" + lobjParam.sNumericScale;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sPrecision=" + lobjParam.sPrecision;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sAttributes=" + lobjParam.sAttributes;
    }
    
    for (lintIndex=1;lintIndex<=Control.RParameters.nCount;lintIndex++){
        try{
            eval("lobjParam = Control.RParameters.Param" + lintIndex + ";");
           }
        catch(e){
			alert('falló');
			return false;
		}
        finally{}
        lstrQueryString = lstrQueryString + "&RParam" + lintIndex + "Name=" + lobjParam.Name;
        lstrQueryString = lstrQueryString + "&RParam" + lintIndex + "Visible=" + lobjParam.Visible;
        lstrQueryString = lstrQueryString + "&RParam" + lintIndex + "Title=" + lobjParam.Title;
        lstrQueryString = lstrQueryString + "&RParam" + lintIndex + "Create=" + lobjParam.Create;
    }    
    
    if (bCheckCode){
        if ((Control.value!='')&&(Control.value!='0')){
            lstrQueryString = lstrQueryString + "&sCode=" + Control.value;
            ShowPopUp('/VTimeNet/Common/Values.aspx'+ lstrQueryString, 'Valpos' + Control.name, 1, 1,'no','no',2000,2000);
        }
        else {
            Control.value='';
            UpdateDiv(Control.name + 'Desc','','Normal');
            eval("if (typeof(" + Control.name + "_onblur)!='undefined')" + Control.name + "_onblur();")
            for (lintIndex=1;lintIndex<=Control.RParameters.nCount;lintIndex++){
            	try{
				    eval("lobjParam = Control.RParameters.Param" + lintIndex + ";");
				   }
				catch(e){
					alert('falló');
					return false;
				}
				finally{}
			    self.document.forms[0].elements[Control.name + "_" + lobjParam.Name].value = "";
            }
        }
    }
    else
    ShowPopUp('/VTimeNet/Common/Values.aspx' + lstrQueryString, 'Valpos', 483, 300)
	return true;
}

//% GetDateSystem: toma la fecha del sistema
//-------------------------------------------------------------------------------------------
function GetDateSystem() {
//-------------------------------------------------------------------------------------------
////+ Se toma la fecha del sistema
//    var DateValue = new Date();
//    var lintYear = 0; 
//    var lintMonth = 0; 
//    var lintDay = 0; 
//    var lTempMonth = 0; 
//    var lTempDay = 0;
//    var ldtmDateSystem = "";

////+ Se calcula la fecha del sistema
//    lTempDay = DateValue.getDate();
//    lTempMonth = DateValue.getMonth() + 1;
//    lintYear = DateValue.getYear();
//    if(lTempDay<10){
//   	lintDay = "0" + DateValue.getDate() + "/"; }
//    else { lintDay = lTempDay + "/"; }
//    if(lTempMonth<10){lintMonth = "0" + (DateValue.getMonth() + 1) + "/"; }
//    else {lintMonth = (DateValue.getMonth() + 1) + "/"; }
//    
////+ Se concatena el día, mes y año
//    ldtmDateSystem = lintDay + lintMonth + lintYear;

    return resValues.todayValue;
}

//% GetDateYYYYMMDD: Devuelve la fecha en formato yyyymmdd.
//-------------------------------------------------------------------------------------------
function GetDateYYYYMMDD(sDate) {
//-------------------------------------------------------------------------------------------
//+ Se toma la fecha del sistema
    var lstrYear = "0"; 
    var lstrMonth = "0"; 
    var lstrDay = "0"; 
    var lstrDate = "";
    var lintDate = 0;

	lstrYear    = sDate.substr(6, 5);
	lstrMonth   = sDate.substr(3, 2);
	lstrDay   = sDate.substr(0, 2);
	
	lstrDate = lstrYear + lstrMonth + lstrDay
	lintDate = lstrDate
    return lintDate;
}

//% insStateControls. Esta función se encarga de habilitar o deshabilitar los campos del
//% documento de la ventana que se pasa como parametro.
//-------------------------------------------------------------------------------------------
function insStateControls(ZoneDocument,Enabled) {
//-------------------------------------------------------------------------------------------
    var lintFields = 0;
    var lintAux = 0;
    lintFields = ZoneDocument.forms[0].elements.length
    for (lintAux=0;lintAux<=lintFields - 1;lintAux++) {
        //alert(ZoneDocument.forms[0].element[lintAux].name)
        ZoneDocument.forms[0].element[lintAux].disabled = Enabled
    }
}
//-------------------------------------------------------------------------------------------
function insShowClientQuery(ParentName,QueryType,DescriptName,sClientRole,nTypeList,sQueryString,bAllowInvalid) {
//-------------------------------------------------------------------------------------------
    if (typeof(bAllowInvalid)=='undefined') bAllowInvalid = false;

    if (!document.forms[0].elements[ParentName].disabled){
		switch (QueryType) {
			case 1 : 
			    ShowPopUp("/VTimeNet/Common/ClientQuery.aspx?ControlName=" + ParentName + "&ControlClieName=" + DescriptName + '&AllowInvalid='+(bAllowInvalid?'1':'2') + sQueryString, "ClientQuery", 650, 300, "yes")
			    break;
			case 3 :
				ShowPopUp("/VTimeNet/Claim/CaseSeq/SI018A.aspx?ControlName=" + ParentName + "&ControlClieName=" + DescriptName + '&AllowInvalid='+(bAllowInvalid?'1':'2') + sQueryString, "ClientClaimSel", 650, 180, "yes")
			    break;			
			default :
			    ShowPopUp("/VTimeNet/Policy/PolicySeq/CA003A.aspx?ControlName=" + ParentName + "&ControlClieName=" + DescriptName + "&sClientRole=" + sClientRole + "&nTypeList=" + nTypeList + '&AllowInvalid='+(bAllowInvalid?'1':'2') + sQueryString, "ClientPolicySel", 650, 180, "yes")
		}
    }
}

//-------------------------------------------------------------------------------------------
function insShowClientCustomPage(sCustomPage,ParentName,QueryType,DescriptName,sClientRole,nTypeList,sQueryString,bAllowInvalid) {
//-------------------------------------------------------------------------------------------
    if (typeof(bAllowInvalid)=='undefined') bAllowInvalid = false;

    if ((!document.forms[0].elements[ParentName].disabled)  && (document.forms[0].elements[ParentName].value!=''))
 		ShowPopUp(sCustomPage + "?ControlName=" + ParentName + "&sClient=" + document.forms[0].elements[ParentName].value + "&sClientRole=" + sClientRole + "&ControlClieName=" + DescriptName + '&AllowInvalid='+(bAllowInvalid?'1':'2') + sQueryString, "ClientQuery", 650, 300, "yes");
}

//% ShowNotesPopUp: Muestra la ventana de Notas como PopUp
//--------------------------------------------------------------------------------------------------------------------------------
function ShowNotesPopUp(Codispl, nNotenum, Action, nIndexNotenum, nOriginalNotenum, nCopyNotenum, sQueryString){
//--------------------------------------------------------------------------------------------------------------------------------
	var lstrQueryString
	var lstrErr

	if(typeof(Action)=='undefined')
		Action = 401

	if((typeof(sQueryString)=='undefined') ||
	   (sQueryString==''))
		lstrQueryString = ''
	else
		lstrQueryString = '&' + sQueryString

	if((typeof(nNotenum)=='undefined') ||
	   (nNotenum<=0)){
	    try{
	   	    if(nIndexNotenum==0){
		    	if(Action!=401){
		    		if (self.document.forms[0].tcnNotenum.length>0)
		    			nNotenum=self.document.forms[0].tcnNotenum[0].value
		    		else 
		    			nNotenum=self.document.forms[0].tcnNotenum.value
		    	}
		    }
		    else
		    	    nNotenum=self.document.forms[0].tcnNotenum[nIndexNotenum].value
		}
		catch(lstrErr){}
	}
	
	if ((nOriginalNotenum==0) &&
	    (nCopyNotenum==0))
	    ShowPopUp("/VTimeNet/Common/SCA002.aspx?sCodispl=" + Codispl + "&WindowType=PopUp&nNotenum=" + nNotenum + "&nMainAction=" + Action + "&nIndexNotenum=" + nIndexNotenum + lstrQueryString, "NotesPopUp", 600, 300, "yes")
    else
        ShowPopUp("/VTimeNet/Common/SCA002.aspx?sCodispl=" + Codispl + "&WindowType=PopUp&nNotenum=" + nNotenum + "&nMainAction=" + Action + "&nIndexNotenum=" + nIndexNotenum + "&nOriginalNotenum=" + nOriginalNotenum + "&nCopyNotenum=" + nCopyNotenum + lstrQueryString, "NotesPopUp", 600, 300, "yes")
}

//--------------------------------------------------------------------------------------------------------------------------------
function insChangeImage(ImageName,Fase){
//--------------------------------------------------------------------------------------------------------------------------------
	var lstrSRC = ''
    if (typeof(document.images[ImageName].disabled) == 'undefined') document.images[ImageName].disabled = false
    if (!document.images[ImageName].disabled){
        lstrSRC = document.images[ImageName].src
// Cambio a On
        if (Fase == 1) {
            lstrSRC = lstrSRC.replace("Off.","On.")
        }
// Cambio a Off
        else
            lstrSRC = lstrSRC.replace("On.","Off.")
        document.images[ImageName].src = lstrSRC
    }
}

//% MouseMoveImage: se realizan las acciones al pasar el mouse sobre la imagen
//--------------------------------------------------------------------------------------------------------------------------------
function MouseMoveImage(Field, OverImage){
//--------------------------------------------------------------------------------------------------------------------------------
//+ Se modifica la descripción de la barra de estado del browser	
	top.window.status = (OverImage)?Field.alt:'';
}

//% UpdateDiv: Modifica la descripción del DIV que recibe como parámetro
//--------------------------------------------------------------------------------------------------------------------------------
function UpdateDiv(DivName,lstrValue,WindowType){
//--------------------------------------------------------------------------------------------------------------------------------
    if(WindowType=='PopUp'){
		opener.$("#"+DivName).html(lstrValue);
	}
	else{
		$("#"+DivName).html(lstrValue);
	}	
}

//%	InsValuesCero: llena con cero el codigo del cliente
//-------------------------------------------------------------------------------------------
function InsValuesCero(sCodClient) {
//-------------------------------------------------------------------------------------------
	var lintLengthcode
	var lintLengthClient
	var lintCodeClient
	
    if (sCodClient.value.indexOf("%")==-1 && 
        sCodClient.value != ''){
        
        lintLengthcode   = sCodClient.value.length;
        lintLengthClient = 14 - lintLengthcode;
        lintCodeClient   = sCodClient.value

		for (j=0;j<lintLengthClient;j++)
			lintCodeClient = "0" + lintCodeClient;
		}	
	else		
		lintCodeClient = sCodClient.value;
		
	return(lintCodeClient)
       
}

//% ValidateClient: Activa la ventana que valida el código del cliente.
//-------------------------------------------------------------------------------------------------------------------------------------------------------
function ValidateClient(sClientCode, sDIVControlName, bCreateClient, nTypeForm, sClientRole, nTypeList, sQueryString, bAllowInvalid, bAllowInvalidFormat) {
//-------------------------------------------------------------------------------------------------------------------------------------------------------
	if (typeof(bCreateClient)=='undefined') bCreateClient = false;
    if (typeof (bAllowInvalid) == 'undefined') bAllowInvalid = false;
    //bAllowInvalidFormat: se usa para el caso especifico de la transaccion de Cambio o unificacion del rut BC005, en donde puede llegar un rut alfanumerico
    if (typeof (bAllowInvalidFormat) == 'undefined') bAllowInvalidFormat = false;

    if (typeof (bAllowInvalid) == 'undefined') bAllowInvalid = false;

	if (typeof (sClientCode.OnChangeCustomHandler) != 'undefined') {
	
		bKeepGoing = sClientCode.OnChangeCustomHandler();
	}
    
	while (sClientCode.value.length < 14)
	    sClientCode.value = '0' + sClientCode.value;
    
    ShowPopUp('/VTimeNet/Common/ClientQueryValidate.aspx?ControlName=' + sClientCode.name + '&sClientCode=' + sClientCode.value + '&sDIVControlName=' + sDIVControlName + '&CreateClient=' + (bCreateClient ? '1' : '2') + '&nTypeForm=' + nTypeForm + '&sClientRole=' + sClientRole + '&nTypeList=' + nTypeList + '&AllowInvalid=' + (bAllowInvalid ? '1' : '2') + '&bAllowInvalidFormat=' + (bAllowInvalidFormat ? '1' : '2') + sQueryString, 'ValidateClient', 800, 300, 'no', 'yes', 3000)
}

//% ValidateDigit: Activa la ventana que valida el dígito verificador del cliente.
//------------------------------------------------------------------------------------------------------------------------------------------------------
function ValidateDigit(sDigit, sClientCode, bCreateClient, nTypeForm, sClientRole, nTypeList, sQueryString, bAllowInvalid, bAllowInvalidFormat,sDivName) {
//------------------------------------------------------------------------------------------------------------------------------------------------------
    if (typeof(bCreateClient)=='undefined') bCreateClient = false;
    if (typeof (bAllowInvalid) == 'undefined') bAllowInvalid = false;
    //bAllowInvalidFormat: se usa para el caso especifico de la transaccion de Cambio o unificacion del rut BC005, en donde puede llegar un rut alfanumerico
    if (typeof (bAllowInvalidFormat) == 'undefined') bAllowInvalidFormat = false;
    if (typeof(sDigit)=='undefined') sDigit = '';
    ShowPopUp('/VTimeNet/Common/ClientQueryValidate.aspx?ControlName=' + sClientCode.name + '&sField=Digit&sDigit=' + sDigit.value + '&sClientCode=' + sClientCode.value + '&CreateClient=' + (bCreateClient ? '1' : '2') + '&nTypeForm=' + nTypeForm + '&sClientRole=' + sClientRole + '&nTypeList=' + nTypeList + '&AllowInvalid=' + (bAllowInvalid ? '1' : '2') + '&bAllowInvalidFormat=' + (bAllowInvalidFormat ? '1' : '2') + sQueryString + '&sDivName=' + sDivName, 'ValidateDigit', 300, 300, 'no', 'yes', 3000)
}

//% insDisabledButton: Determina si un botón se encuentra activo o no.
//--------------------------------------------------------------------------------------------------------------------------------
function insDisabledButton(control, nIndex){
//--------------------------------------------------------------------------------------------------------------------------------
	var lblnDisabled;

	if (typeof(control.length)=='undefined'){
		if (typeof(control.disabled)=='undefined'){
			control.disabled=false;
		}
		lblnDisabled=control.disabled;
	}
	else{
		if (typeof(control[nIndex].disabled)=='undefined'){
			control[nIndex].disabled=false;
		}
		lblnDisabled=control[nIndex].disabled;
	}

	return(!lblnDisabled)
}

//% ShowDiv: Muestra/Oculta una división específica dentro de la página
//--------------------------------------------------------------------------------------------------------------------------------
function ShowDiv(DivName, Display){
//--------------------------------------------------------------------------------------------------------------------------------
//+ Se asigna valor a la variable para el manejo de la acción según el explorador.
//+ Los valores posibles son: show y hide

//+ Se asigna el valor por defecto
	if(typeof(Display)=='undefined'){
		Display = 'hide'
    }

    
    if (Display == 'show') {
        document.getElementById(DivName).style.display="";
	}
	else{
	    document.getElementById(DivName).style.display = "none";
	}
}

//% ShowPolicyData: Muestra la ventana de datos de verificación de la póliza s
//--------------------------------------------------------------------------------------------------------------------------------
function ShowPolicyData(sCertype, nBranch, nProduct, nPolicy, nCertif) {
//--------------------------------------------------------------------------------------------------------------------------------
	ShowPopUp('/VTimeNet/Common/PolData.aspx?sCertype=' + sCertype + "&nBranch=" + nBranch + "&nProduct=" + nProduct + "&nPolicy=" + nPolicy + "&nCertif=" + nCertif, 'PolicyData', 600, 450, "yes", "no", 100, 50)
}

//%insConvertNumber. Esta funcion se encarga de tomar un string y convertirlo en numerico
//% El parámetro bFormatJS indica si el valor a tratar está en formato JS (sin miles y con punto como decimal); por defecto el valor es falso; es decir,
//% se considera el valor pasado como parámetro con formato.
//--------------------------------------------------------------------------------------------------------------------------------
function insConvertNumber(lstrAmount,lstrThousandSep,lstrDecimalSep, bFormatJS){
//--------------------------------------------------------------------------------------------------------------------------------
	var lstrSimbol_dec = mstrSrvDecSep;	
	var lstrPaternPoint
	
//alert(mstrSrvDecSep);

	lstrAmount = lstrAmount + "";
	
	if (lstrSimbol_dec==",")
		lstrPaternPoint = /\./g
	else
		lstrPaternPoint = /\,/g

	lstrAmount = lstrAmount.replace(lstrPaternPoint, "");
    lstrAmount = lstrAmount.replace(lstrSimbol_dec,".")
    return parseFloat(lstrAmount);
}

//--------------------------------------------------------
function insReloadTop(bMainPage, bOverOpener){
//--------------------------------------------------------
    var lstrURL=''; var lstrOpener; var lintPos; var lobjTop;
    lobjTop = (bOverOpener?opener.top:self.top)
    lstrURL += lobjTop.location.href
    lintPos = lstrURL.search("sConfig");
    if (lintPos==-1)
        lintPos=lstrURL.length;
    lstrURL = lstrURL.substr(0, lintPos);
    lobjTop.location.href = lstrURL;
}

//% insConfirmDelete: se realizan las acciones al aceptar la ventana de Eliminar del grid
//-------------------------------------------------------------------------------------------
function insConfirmDelete(sQueryString){
//-------------------------------------------------------------------------------------------
   var lstrLocation=''
   lstrLocation += top.opener.document.location.href
   lstrLocation = lstrLocation.replace("Reload=1","Reload=2")
   lstrLocation = lstrLocation.replace(/ReloadIndex=[0-9]*&/,"")
   if (typeof(sQueryString) == 'undefined') sQueryString = ''
   top.opener.document.location.href = lstrLocation + sQueryString
   self.close()
}

//-------------------------------------------------------------------------------------------
function insNavigation(Codispl,LinkSpecial,LinkSpecialAction,LinkSpecialParams){
//-------------------------------------------------------------------------------------------
    if (typeof(LinkSpecial)=='undefined') LinkSpecial=false
    LinkSpecialAction = typeof(LinkSpecialAction)=='undefined'?LinkSpecialAction= '':'&LinkSpecialAction=' + LinkSpecialAction
    LinkSpecialParams = typeof(LinkSpecialParams)=='undefined'?LinkSpecialParams= '':'&LinkSpecialParams=' + LinkSpecialParams   
    Codispl = (typeof(Codispl)=='undefined'?'':'?sCodispl='+Codispl)
    ShowPopUp('/VTimeNet/Common/GoTo.aspx' + Codispl + (LinkSpecial?'&LinkSpecial=1':'&LinkSpecial=2') + LinkSpecialAction + LinkSpecialParams, "GoTo", 750, 450,"no","no"); 
}

//% VTFormat: Se encarga de darle formato a los valores numéricos.
//% nDecimals: El parámetro de decimales REDONDEA
//% El parámetro bFormatJS indica si el valor a tratar está en formato JS (sin miles y con punto como decimal); por defecto el valor es falso; es decir,
//% se considera el valor pasado como parámetro con formato.
//-----------------------------------------------------------------------------------------------------------
function VTFormat(sValue, sCurDecimalPoint, sNewDecimalPoint, sThousandsChar, nDecimals, bFormatJS){
//-----------------------------------------------------------------------------------------------------------
	var lintPoint;
    var lintIndex;
    var bNegative = false;
    var sValue_dec = '';
    var sValue_ent = '';
    var sSimbol_dec = mstrSrvDecSep;
    var sSimbol_dec_Loc = '';    
    var sThousandsChar = '';
    var sSimbol_ent;
    var sValue_aux

	if (typeof(bFormatJS) == 'undefined') bFormatJS = false;

	if (!bFormatJS)
		sValue = insConvertNumber(sValue);
	else
		sValue = parseFloat(sValue);

	if (typeof(sValue) == 'number'){
		sValue_aux = sValue.toString();  //sValue.toLocaleString(10);
		if (sValue_aux.indexOf('e')>0 ||
			sValue_aux.indexOf('E')>0){
			sValue = (Math.round(sValue * 100000000) / 100000000);
			sValue = sValue.toString();  //sValue.toLocaleString(10);
		}
		else{
			sValue = sValue_aux
		}
		sValue = '' + sValue;
	}

	
//+ Se busca el separador decimal que usa el cliente.	
	for (lintIndex=sValue.length-1;lintIndex>=0;lintIndex--)
        if ((sValue.substr(lintIndex,1)=='.' || sValue.substr(lintIndex,1) == ',') && sSimbol_dec_Loc=='')
	        sSimbol_dec_Loc = sValue.substr(lintIndex,1);		    		    
	        
//+ Se busca el separador de miles que usa el cliente.		        
	if (sSimbol_dec_Loc=='.')        
	    sThousandsChar = ','
	else
	    sThousandsChar = '.';            
        
//+ Se obliga a que el símbolo decimal sel el mismo que el servidor.
    if (sSimbol_dec!=mstrSrvDecSep)
        sSimbol_dec = mstrSrvDecSep;

    if (sSimbol_dec==",")
		sSimbol_ent = ".";
	else
		sSimbol_ent = ",";

//+ Siempre el símbolo decimal es el punto (no existe símbolo de miles).
    lintPoint = sValue.indexOf(sSimbol_dec_Loc);
    if (lintPoint > 0) {
        sValue_ent = sValue.substr(0, lintPoint);
        sValue_dec = sValue.substr(lintPoint + 1);
    }       
    else
    {
        sValue_ent = sValue;
        sValue_dec = '';
	}

//+ Se verifica si el valor es negativo.
	if (parseFloat(sValue_ent)<0) {
		bNegative = true;
		sValue_ent = sValue_ent.substr(1);//, sValue_ent.length);
	}

//+ Se agregan los ceros faltantes a la derecha de los decimales.	
	if (nDecimals > 0){
		if (sValue_dec.length < nDecimals){
			lintDif = (nDecimals - sValue_dec.length);
			for (lintIndex = 1; lintIndex <= lintDif; lintIndex++){
				sValue_dec = sValue_dec + '0'
			}
		}
		else{
		    if (sValue_dec.length > nDecimals){
			    lintDif = (sValue_dec.length - nDecimals);
	            lintDif = Math.pow(10, lintDif);
	            sValue_dec = parseFloat(sValue_dec)/lintDif;
	            sValue_dec = Math.round(sValue_dec);
	            sValue_dec = sValue_dec + "";
	            lintDif = sValue_dec.length;
	            
//+ Si el redondeo implica aumentar la parte entera.
	            if (lintDif > nDecimals) {
	                sValue_ent = "" + (parseFloat(sValue_ent) + 1);

	                sValue_dec = "";	                
//+ Se asigna como decimal ceros.
	                for (lintIndex = 1; lintIndex <= nDecimals; lintIndex++)
				        sValue_dec = '0' + sValue_dec;

	            }
	                
	            if (lintDif < nDecimals)
	                for (lintIndex = lintDif; lintIndex < nDecimals; lintIndex++)
				        sValue_dec = '0' + sValue_dec;				        
		    }
		}
		sValue = sSimbol_dec + sValue_dec;
	}
	else {
		sValue = "";
	}

//+ Si existe parte entera.
	if (sValue_ent!='')
//+ Si no tiene seprador de miles se agrega 
	    if (sValue_ent.indexOf(sThousandsChar)>0)   
	        sValue_ent = sValue_ent.replace(sThousandsChar,'');
		for (lintIndex = sValue_ent.length - 3; lintIndex >= 1; lintIndex = lintIndex - 3)
			sValue_ent = sValue_ent.substr(0, lintIndex) + sSimbol_ent + sValue_ent.substr(lintIndex);

//+ Se agrega el símbolo negativo a la parte entera en caso de que su valor sea menor que cero
	if (bNegative==true) {
	    sValue_ent = '-' + sValue_ent;
	}

    sValue = sValue_ent + sValue;

	return(sValue)
}


//% VTFormat: Se encarga de darle formato a los valores numéricos.
//% nDecimals: El parámetro de decimales REDONDEA
//% El parámetro bFormatJS indica si el valor a tratar está en formato JS (sin miles y con punto como decimal); por defecto el valor es falso; es decir,
//% se considera el valor pasado como parámetro con formato.
//-----------------------------------------------------------------------------------------------------------
function VTFormatT(sValue, sCurDecimalPoint, sNewDecimalPoint, sThousandsChar, nDecimals, bFormatJS){
//-----------------------------------------------------------------------------------------------------------
	var lintPoint;
    var lintIndex;
    var bNegative = false;
    var sValue_dec = '';
    var sValue_ent = '';
    var sSimbol_dec = mstrSrvDecSep;
    var sSimbol_dec_Loc = '';    
    var sThousandsChar = '';
    var sSimbol_ent;
    var sValue_aux

	if (typeof(bFormatJS) == 'undefined') bFormatJS = false;

	if (!bFormatJS)
		sValue = insConvertNumber(sValue);
	else
		sValue = parseFloat(sValue);

	if (typeof(sValue) == 'number'){
		sValue_aux = sValue.toString();  //sValue.toLocaleString(10);
		if (sValue_aux.indexOf('e')>0 ||
			sValue_aux.indexOf('E')>0){
			sValue = (Math.round(sValue * 100000000) / 100000000);
			sValue = sValue.toString();  //sValue.toLocaleString(10);
		}
		else{
			sValue = sValue_aux
		}
		sValue = '' + sValue;
	}

	
//+ Se busca el separador decimal que usa el cliente.	
	for (lintIndex=sValue.length-1;lintIndex>=0;lintIndex--)
        if ((sValue.substr(lintIndex,1)=='.' || sValue.substr(lintIndex,1) == ',') && sSimbol_dec_Loc=='')
	        sSimbol_dec_Loc = sValue.substr(lintIndex,1);		    		    
	        
//+ Se busca el separador de miles que usa el cliente.		        
	if (sSimbol_dec_Loc=='.')        
	    sThousandsChar = ','
	else
	    sThousandsChar = '.';            
        
//+ Se obliga a que el símbolo decimal sel el mismo que el servidor.
    if (sSimbol_dec!=mstrSrvDecSep)
        sSimbol_dec = mstrSrvDecSep;

    if (sSimbol_dec==",")
		sSimbol_ent = ".";
	else
		sSimbol_ent = ",";

//+ Siempre el símbolo decimal es el punto (no existe símbolo de miles).
    lintPoint = sValue.indexOf(sSimbol_dec_Loc);
    if (lintPoint > 0) {
        sValue_ent = sValue.substr(0, lintPoint);
        sValue_dec = sValue.substr(lintPoint + 1);
    }       
    else
    {
        sValue_ent = sValue;
        sValue_dec = '';
	}

//+ Se verifica si el valor es negativo.
	if (parseFloat(sValue_ent)<0) {
		bNegative = true;
		sValue_ent = sValue_ent.substr(1);
	}

//+ Se agregan los ceros faltantes a la derecha de los decimales.	
	if (nDecimals > 0){
		if (sValue_dec.length < nDecimals){
			lintDif = (nDecimals - sValue_dec.length);
			for (lintIndex = 1; lintIndex <= lintDif; lintIndex++){
				sValue_dec = sValue_dec + '0'
			}
		}
		else{
		    
	            sValue_dec = sValue_dec.substr(0, 6)
		    
		}
		sValue = sSimbol_dec + sValue_dec;
	}
	else {
		sValue = "";
	}

//+ Si existe parte entera.
	if (sValue_ent!='')
//+ Si no tiene seprador de miles se agrega 
	    if (sValue_ent.indexOf(sThousandsChar)>0)   
	        sValue_ent = sValue_ent.replace(sThousandsChar,'');
		for (lintIndex = sValue_ent.length - 3; lintIndex >= 1; lintIndex = lintIndex - 3)
			sValue_ent = sValue_ent.substr(0, lintIndex) + sSimbol_ent + sValue_ent.substr(lintIndex);

//+ Se agrega el símbolo negativo a la parte entera en caso de que su valor sea menor que cero
	if (bNegative==true) {
	    sValue_ent = '-' + sValue_ent;
	}

    sValue = sValue_ent + sValue;
    //sValue = sValue;

	return(sValue)
}

//% ShowImagePopUp: Muestra la ventana de imagenes como PopUp
//--------------------------------------------------------------------------------------------------------------------------------
function ShowImagePopUp(Codispl, nImagenum, Action, nIndexImagenum){
//--------------------------------------------------------------------------------------------------------------------------------
	if(typeof(Action)=='undefined')
		Action = 401
	if((typeof(nImagenum)=='undefined') ||
	   (nImagenum<=0)){
	   	if(nIndexImagenum==0){
			if(Action!=401)	nImagenum=(self.document.forms[0].tcnImagenum.value==''?0:self.document.forms[0].tcnImagenum.value)
		}
		else
			nImagenum=self.document.forms[0].tcnImagenum[nIndexImagenum].value
	}
    ShowPopUp("/VTimeNet/Common/SCA010.aspx?sCodispl=" + Codispl + "&WindowType=PopUp&nImagenum=" + nImagenum + "&nMainAction=" + Action + "&nIndexImagenum=" + nIndexImagenum, "ImagePopUp", 600, 300, "yes")
}

//% StatusControl: captura/asigna el estado de cada uno de los controles de la página
//-------------------------------------------------------------------------------------------
function StatusControl(bFirst, nZone){
//-------------------------------------------------------------------------------------------
	var lstrZone
	var lintlegth
	var lobjcontrol
	var lobjelement
	if (nZone == 1)
        lstrZone = "fraHeader";
    else
        lstrZone = "fraFolder";
	if(bFirst){
		lobjcontrol = top.frames[lstrZone].marrControls;
		lobjelement = top.frames[lstrZone].document.forms[0].elements;
		lintlegth = top.frames[lstrZone].document.forms[0].length;
		for(lintIndex=0;lintIndex<lintlegth;lintIndex++){
			lobjcontrol[lintIndex] = lobjelement[lintIndex].disabled;
		}
	}
	else{
		if (typeof(top.frames[lstrZone])!='undefined'){
			lobjelement = top.frames[lstrZone].document.forms[0].elements;
			lobjcontrol = top.frames[lstrZone].marrControls;
			lintlegth = top.frames[lstrZone].document.forms[0].length;
			for(lintIndex=0;lintIndex<lintlegth;lintIndex++){
				lobjelement[lintIndex].disabled = lobjcontrol[lintIndex];
			}
		}
		else{
			if (typeof(opener.top.frames[lstrZone])!='undefined'){
				lobjelement = opener.top.frames[lstrZone].document.forms[0].elements;
				lobjcontrol = opener.top.frames[lstrZone].marrControls;
				lintlegth = opener.top.frames[lstrZone].document.forms[0].length;
				for(lintIndex=0;lintIndex<lintlegth;lintIndex++){
					lobjelement[lintIndex].disabled = lobjcontrol[lintIndex];
				}
			}
			else{
				lobjelement = self.document.forms[0].elements;
				lobjcontrol	= self.marrControls;
				lintlegth = self.document.forms[0].length;
				for(lintIndex=0;lintIndex<lintlegth;lintIndex++){
					lobjelement[lintIndex].disabled = lobjcontrol[lintIndex];
				}
			}
		}
	}
}

//%insDefValues: Esta funcion se encarga de realizar el código Javascript para realizar el 
//%				 llamado a la ventana que carga los valores posibles.
//-------------------------------------------------------------------------------------------
function insDefValues(sKey, sParameters, sPath, sNameAsp){
//-------------------------------------------------------------------------------------------
	var lstrLocation="";
	var lstrframeCaller="fraFolder";

    lstrLocation = self.document.location.href ;	
	
	if (lstrLocation.indexOf("Type=PopUp")==-1){
		if (top.frames["fraSequence"].pintZone == 1) {
		    lstrframeCaller = "fraHeader";         
		}
	}

    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof (sPath) == 'undefined' ? '' : sPath + '/')
                sPath = sPath.replace('//', '/');
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                
                if (typeof(top.frames[lstrframeCaller])!='undefined')
					if (typeof(top.frames[lstrframeCaller].mstrDoSubmit)!='undefined')
						top.frames[lstrframeCaller].mstrDoSubmit = "2";
                    if (typeof (resValues) != 'undefined')
                        UpdateDiv('lblWaitProcess', '<MARQUEE>' + resValues.marqueeMessage +  '</MARQUEE>', ''); 						
                	else
                      	UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>',''); 						
				        
                				
                

                
                if (typeof(sNameAsp)!='undefined') 
					top.frames["fraGeneric"].location.href = sPath + sNameAsp + '.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters;
                else 
					top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters;
            }
            else{
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sPath = sPath.replace('//', '/');

                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
				
                if (typeof(sNameAsp)!='undefined') 
					opener.top.frames["fraGeneric"].location.href = sPath + sNameAsp + '.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters;
                else 
					opener.top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters;
            }
}


//%insDefValues: Esta funcion se encarga de realizar el código Javascript para realizar el 
//%				 llamado a la ventana que carga los valores posibles.
//-------------------------------------------------------------------------------------------
function insDefValuesNR(sKey, sParameters1, sParameters2, sParameters3, sPath, sNameAsp){
//-------------------------------------------------------------------------------------------
	var lstrLocation="";
	var lstrframeCaller="fraFolder";

	lstrLocation = self.document.location.href;	
	
	if (lstrLocation.indexOf("Type=PopUp")==-1){
		if (top.frames["fraSequence"].pintZone == 1) {
		    lstrframeCaller = "fraHeader";         
		}
	}

    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters1 = (typeof(sParameters1)=='undefined'?'':'&' + sParameters1)
                sParameters2 = (typeof(sParameters2)=='undefined'?'':'&' + sParameters2)
                sParameters3 = (typeof(sParameters3)=='undefined'?'':'&' + sParameters3)
                
                if (typeof(top.frames[lstrframeCaller])!='undefined')
					if (typeof(top.frames[lstrframeCaller].mstrDoSubmit)!='undefined')
						top.frames[lstrframeCaller].mstrDoSubmit = "2";
					
//				UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>',''); 						
                UpdateDiv('lblWaitProcess', '<MARQUEE>' + resValues.marqueeMessage +  '</MARQUEE>', ''); 						
                if (typeof(sNameAsp)!='undefined'){
					top.frames["fraGeneric"].location.href = sPath + sNameAsp + '.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters1 + sParameters2 + sParameters3;
				}
                else {
					top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey + '&sFrameCaller=' + lstrframeCaller + sParameters1 + sParameters2 + sParameters3;
				}
            }
              
}



//% StatePossibleValues: inicializa el valor de un campo que dependa de otro
//-------------------------------------------------------------------------------------------
function StatePossibleValues(Name, Field, WindowType){
//-------------------------------------------------------------------------------------------
	with(self.document){
		forms[0].elements[Name].value="";
		
		if(typeof(forms[0].elements["btn" + Name])=='undefined'){
			if(typeof(forms[0].elements["btn_" + Name])!='undefined')
				forms[0].elements["btn_" + Name].disabled = forms[0].elements[Name].disabled;
		}
		else{
			forms[0].elements[Name].disabled = (Field.value==0 || Field.value=="")?true:false;
			forms[0].elements["btn" + Name].disabled = forms[0].elements[Name].disabled;
		}
			
		UpdateDiv(Name + "Desc","",(WindowType=='undefined')?"Normal":WindowType);
	}
}

//% insShowCompanyQuery: muestra la ventana de compañías del sistema
//-------------------------------------------------------------------------------------------
function insShowCompanyQuery(ParentName,QueryType,DescriptName) {
//-------------------------------------------------------------------------------------------
    if (!document.forms[0].elements[ParentName].disabled){
	    ShowPopUp("/VTimeNet/Common/CompanyQuery.aspx?ControlName=" + ParentName + "&ControlCompanyName=" + DescriptName, "CompanyQuery", 650, 300, "yes")
    }
}

//%ValidateCompany: Activa la ventana que valida el código de la compañía y muestra el nombre
//--------------------------------------------------------------------------------------------------------------------------------
function ValidateCompany(sCompanyCode,sDIVControlName){
//--------------------------------------------------------------------------------------------------------------------------------
	if(sCompanyCode.value<=0 || 
	   isNaN(sCompanyCode.value)){
		if(sCompanyCode.value!=""){
			alert(resValues.moMSGGenFunctions_c_10101 + sCompanyCode.Alias + resValues.moMSGGenFunctions_c_10102);
			UpdateDiv(sDIVControlName,'','Normal')
			sCompanyCode.value = "";	
		}
	}	
	if(sCompanyCode.value>0)
		ShowPopUp("/VTimeNet/CoReinsuran/CoReinsuran/ShowDefValues.aspx?Field=" + 'CompanyQuery' +  "&nCompany=" + sCompanyCode.value,"ShowDefValuesCompanyQuery", 1, 1,"no","no",2000,2000);	
}

// getImageDimension: Busca las dimenciones de la imagen
//--------------------------------------------------------------------------------------------
function getImageDimension (imgURL, loadHandler, message) {
//--------------------------------------------------------------------------------------------
    var img = new Image();
    mstrpath_src = "1";
    img.onerror = loadHandler;
    img.src = imgURL;
    
	if (mstrpath_src == "") {
		alert(message);
    }
    else{
		mstrpath_src == "";
    }
}

// checkImageDimensions: crea la ruta y verifica dimención
//--------------------------------------------------------------------------------------------
function checkImageDimensions (fileName, message) {
//--------------------------------------------------------------------------------------------
    var imgURL = 'file:///' + fileName;
    getImageDimension(imgURL, showImageDimensions, message);
}

// showImageDimensions: Se asigna valor a variable de ruta aceptada 
//--------------------------------------------------------------------------------------------
function showImageDimensions () {
//--------------------------------------------------------------------------------------------
	mstrpath_src = "";
}

// confirmClosed: Informa el cierre de la ventana
//--------------------------------------------------------------------------------------------
function confirmClosed(nProcess) {
//--------------------------------------------------------------------------------------------
	if (nProcess=='1'){
		//alert('Se cerrara la ventana, y el proceso seguirá ejecutándose');
		alert(resValues.confirmClosedMessage);
	}	
}

// cancelEditRecord: Al cancelar la ventana de errores se llama a la ventana en forma de Popup
//--------------------------------------------------------------------------------------------
function cancelEditRecord(sQueryString,lintIndex,nMainAction,nZone) {
//--------------------------------------------------------------------------------------------
	var lstrURL
	lstrURL = sQueryString.substr(sQueryString.indexOf("nIndex="), sQueryString.length);
	if (nZone==1)
		opener.top.frames["fraHeader"].EditRecord(lintIndex,nMainAction,'Update',lstrURL)
	else
		opener.top.frames["fraFolder"].EditRecord(lintIndex,nMainAction,'Update',lstrURL)
}

//% setPointerDoc: Define puntero de cursor para 
//%                los controles de un objeto document especifico
//-------------------------------------------------------------------------------------------
function setPointerDoc(dDoc, sValue){
//-------------------------------------------------------------------------------------------

    try{
        with(dDoc){
//+Se deja puntero indicado en todo el cuerpo del documento.
//+Como esto no afecta a elementos HTML (textos, check, links, imagenes, etc.)
//+luego se procede con cada uno de ellos individualmente
            body.style.cursor = sValue;

//+Por cada imagen del documento
            for (var k=0;k < images.length; k++) 
                images(k).style.cursor = sValue;

//+Por cada link del documento
            for (var k=0;k < links.length; k++) 
                links[k].style.cursor = sValue;


//+Por cada elemento de cada formulario del documento
            for (var k=0;k < forms.length; k++)
                with(forms[k]){
                    for (var i=0;i < elements.length; i++)
                        with(elements[i]){
                            if (type!='hidden')
                                style.cursor = sValue;
                        }
                }
        }
    }
    catch(x){
    }
    finally{
    }
}

//% setPointer: Define puntero de cursor de todas las ventanas activas de la aplicacion 
//-------------------------------------------------------------------------------------------
function setPointer(sValue){
//-------------------------------------------------------------------------------------------
    var lstsMessage = '<MARQUEE>Procesando, por favor espere...</MARQUEE>';

	if (sValue == '') lstsMessage = '';
    UpdateDiv('lblWaitProcess',lstsMessage,'');
}

//% getCookieValue: Retorna el valor de un cookie o 'undefined' si no existe o no tiene valor
//-------------------------------------------------------------------------------------------
function getCookieValue(sName) {
//-------------------------------------------------------------------------------------------
//+Se agregan marcadores de inicio para encontrar exactamente la cadena
//+De lo contrario, si la variable se llama 'elDato' y si se busca por 'dato'
//+retornaría el mismo valor
//+De esta forma ahora buscamos por '; dato' dentro de '; elDato=elvalor'
//+y no lo encuentra

    var sSearch = '; ' + sName + '=';
    var sCookies = '; ' + document.cookie
    var i, j;
    
    i = sCookies.indexOf(sSearch);
    if (i != -1) {
        i += sSearch.length;
        j = sCookies.indexOf(';', i);
        if (j == -1)
            j = sCookies.length;
        return sCookies.substring(i,j);
    }
}

//% setCookie: Establece el valor de una cookie
//-------------------------------------------------------------------------------------------
function setCookie(sName, sValue, dExpirdat) {
//-------------------------------------------------------------------------------------------    
    document.cookie = sName  + "=" + 
                      sValue + ((dExpirdat == null) ? "" : ("; expires=" + dExpirdat.toUTCString()))
}

//% NumberJS: Función que se encarga de llevar una cadena numérica a una cadena numérica pero con formato numérico JS "#######.##"; es decir,
//% sin símbolo de miles y con simbolo decimal el punto en caso de tenerlo.
//% Parámetros: sValue -> Valor a procesar (puede venir con cualquier formato); bFormatJS -> true)indica si el valor a procesar se encuentra en formato JS.
//-----------------------------------------------------------------------------------------------------------
function NumberJS(sValue, bFormatJS) {
//-----------------------------------------------------------------------------------------------------------
	var lintComma
	var lintComma_aux
    var lintPoint
    var lintPoint_aux
    var sValue_aux = ""
    var lintLength
	var sPaternPoint
	var sPaternComma

	sPaternPoint = /\./g
	sPaternComma = /\,/g
		
//	alert("sValue (NumberJS-Ini): " + sValue);
	
//+ Si el valor es considerado en formato JS (controlado por programa); no se efectua ninguna operación.
//	if (bFormatJS!=true) {
		sValue = sValue += "";
		lintComma = sValue.indexOf(",")
		lintPoint = sValue.indexOf(".")

//	alert("lintComma: " + lintComma);
//	alert("lintPoint: " + lintPoint);
	
		if (lintComma>0) {
			if (lintPoint>0) {
				if (lintComma>lintPoint) {
					sValue = sValue.replace(sPaternPoint, "");
					sValue = sValue.replace(sPaternComma, ".");
				}
				else {
					sValue = sValue.replace(sPaternComma, "");
				}
			}
//+ Caso cuando el valor tiene el símbolo "," y se desea saber si es decimal o es miles.
			else {
				lintLength = sValue.length
//+ Se verifica si existe repetición del símbolo en cuyo caso representa el de miles.
				if (lintLength > lintComma) {
					sValue_aux = sValue.substr(lintComma + 1, sValue.length)
					lintComma_aux = sValue_aux.indexOf(",")
//+ Si se detecta que el símbolo esta repetido se elimina ya que se considera como miles.
					if (lintComma_aux > 0) {
						sValue = sValue.replace(sPaternComma, "");
					}
//+ Se deduce si el símbolo "," es miles o decimal apoyándose en el parámetro bFormatJS y/o en la variable global de la configuración del servidor (mstrSrvDecSep)
					else {
//alert("mstrSrvDecSep(,): " + mstrSrvDecSep);
//+ Si el símbolo es igual al del servidor; se cambia por punto para llevarlo al formato JS
						if (mstrSrvDecSep==",") {
							sValue = sValue.replace(sPaternComma, ".");
						}
//+ Si el símbolo no es igual al del servidor; se elimina ya que se considera como miles.
						else {
							sValue = sValue.replace(sPaternComma, "");
						}
					}
				}
			}
		}
		else {
//+ En caso de que el valor no posea símbolos de miles y decimales.
		    if (lintPoint<=0) {
//+ Esto es por si se incluye decimales (,) sin parte entera; es decir partiendo del símbolo decimal. Entonces se agrega cero a la parte entera.
				if (sValue.substr(0, 1)==',') {
					sValue = '0' + sValue
					sValue = sValue.replace(sPaternComma, ".");
				}
		    }
//+ Caso para cuando exista el simbolo "." y se desea saber si corresponde a miles o a decimales.
		    else {
				if (bFormatJS!=true) {
//+ Si el símbolo no es igual al del servidor; se considera miles y por lo tanto se eliminan. 
//+ (Esto debido a que el control los formatea según configuración; lo que garantiza que es miles)
					if (mstrSrvDecSep!=".") {
						sValue = sValue.replace(sPaternPoint, "");
					}
				}
		    }
		}	
//	}		
//alert("sValue(NumberJS-sin formato): " + sValue);	
	return(sValue)
}

//-----------------------------------------------------------------------------------------------------------
function getRoundedNF(val)
//-----------------------------------------------------------------------------------------------------------
{
	var factor;
	var i;

	// round to a certain precision
	factor = 1;
	for (i=0; i<this.places; i++)
	{	factor *= 10; }
	val *= factor;
	val = Math.round(val);
	val /= factor;

	return (val);
}

//% creObjParam: Crea un objeto parametro para usar en valores posibles
//-----------------------------------------------------------------------------------------------------------
function creObjParam(sName, sValue, sDirection, sType, sSize, sScale, sPrecision, sAttrib)
//-----------------------------------------------------------------------------------------------------------
{
    var lobjPar = new Object;
     
    lobjPar.sName           = sName;
    lobjPar.sValue          = sValue;
    lobjPar.sDirection      = sDirection;
    lobjPar.sParType        = sType;
    lobjPar.sSize           = sSize;
    lobjPar.sNumericScale   = sScale;
    lobjPar.sPrecision      = sPrecision;
    lobjPar.sAttributes     = (isNaN(sAttrib))?'64':sAttrib;
    
    return lobjPar;
}

//% creObjParamRet: Crea un objeto parametro de retorno para usar en valores posibles
//-----------------------------------------------------------------------------------------------------------
function creObjParamRet(sName, sVisible, sTitle, sCreate)
//-----------------------------------------------------------------------------------------------------------
{
    var lobjParRet = new Object;

    lobjParRet.Name     = sName;
    lobjParRet.Visible  = sVisible;
    lobjParRet.Title    = sTitle;
    lobjParRet.Create   = sCreate;
    
    return lobjParRet;
}


//% insInitialAgencyGen: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgencyRepmach(nInd) {
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            if (typeof(P_COD_SUCURSAL)!='undefined'){
                if (P_COD_SUCURSAL.value != 0){
                    if (typeof(P_COD_OFICINA)!='undefined'){
                        P_COD_OFICINA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        P_COD_OFICINA.Parameters.Param2.sValue = 0;
                        P_COD_AGENCIA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        if(P_COD_OFICINA.value!="" && P_COD_OFICINA.value>0)
                            P_COD_AGENCIA.Parameters.Param2.sValue = (P_COD_OFICINA.value==''?0:P_COD_OFICINA.value);
                        else
                            P_COD_AGENCIA.Parameters.Param2.sValue = 0;
                    }
                }
                else{
                      if(typeof(P_COD_OFICINA)!='undefined'){
                        P_COD_OFICINA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        P_COD_OFICINA.Parameters.Param2.sValue = 0;
                        P_COD_AGENCIA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        if(P_COD_OFICINA.value!="" && P_COD_OFICINA.value>0){
                            P_COD_AGENCIA.Parameters.Param2.sValue = (P_COD_OFICINA.value==''?0:P_COD_OFICINA.value);}
                        else{
                            P_COD_AGENCIA.Parameters.Param2.sValue = 0;}
                    }
                }
            }
        }
//+ Cambia la oficina 
        else{
            if (nInd == 2){
                if(P_COD_OFICINA.value != ''){
                    P_COD_AGENCIA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                    P_COD_AGENCIA.Parameters.Param2.sValue = (P_COD_OFICINA.value==''?0:P_COD_OFICINA.value);
                    P_COD_SUCURSAL.value = P_COD_OFICINA_nBran_off.value;
                    P_COD_OFICINA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                }
                else{
                    P_COD_AGENCIA.Parameters.Param1.sValue = 0;    
                    P_COD_AGENCIA.Parameters.Param2.sValue = 0;
                }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
                    if(P_COD_AGENCIA.value != ""){
                        P_COD_SUCURSAL.value = P_COD_AGENCIA_nBran_off.value;
                        if (P_COD_OFICINA.value == ''){
                            P_COD_OFICINA.value = P_COD_AGENCIA_nOfficeAgen.value;
                            UpdateDiv('P_COD_OFICINADesc',P_COD_AGENCIA_sDesAgen.value);
                        }
                        P_COD_OFICINA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        P_COD_AGENCIA.Parameters.Param1.sValue = (P_COD_SUCURSAL.value==''?0:P_COD_SUCURSAL.value);
                        P_COD_AGENCIA.Parameters.Param2.sValue = (P_COD_OFICINA.value==''?0:P_COD_OFICINA.value);
                    }
                }
            }
        }
    }
}
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDependRepmach()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_COD_OFICINA.value="";
        P_COD_AGENCIA.value="";
        P_COD_OFICINA_nBran_off.value = "";
        P_COD_AGENCIA_nBran_off.value = "";
        P_COD_AGENCIA_nOfficeAgen.value = "";
        P_COD_AGENCIA_sDesAgen.value = "";
    }
    UpdateDiv('P_COD_OFICINADesc','');
    UpdateDiv('P_COD_AGENCIADesc','');
}

//% insInitialAgencyGen: manejo de sucursal/oficina/agencia/Intermediario
//-------------------------------------------------------------------------------------------
function insInitialIntermedRepmach(nInd) {
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            if (typeof(P_COD_SUCURSAL)!='undefined'){
                if (P_COD_SUCURSAL.value != 0)
                     P_NUM_INTERMED.Parameters.Param1.sValue = P_COD_SUCURSAL.value;
                else
                     P_NUM_INTERMED.Parameters.Param1.sValue = 0;
                
            }
        }
//+ Cambia la oficina 
        else{
            if (nInd == 2){
               if (typeof(P_COD_OFICINA)!='undefined'){
					if (P_COD_OFICINA.value != 0){
					     P_NUM_INTERMED.Parameters.Param2.sValue = P_COD_OFICINA.value;
					}     
					else
					     P_NUM_INTERMED.Parameters.Param2.sValue = 0;
               
               }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
				   if (typeof(P_COD_AGENCIA)!='undefined'){
						if (P_COD_AGENCIA.value != 0)
						     P_NUM_INTERMED.Parameters.Param3.sValue = P_COD_AGENCIA.value;
						else
						     P_NUM_INTERMED.Parameters.Param3.sValue = 0;
					   
				   }
				}
				else{
			        if (nInd == 4){
				       if (typeof(P_NUM_INTERMED)!='undefined'){
						  if (P_NUM_INTERMED.value != ""){
        					 P_COD_SUCURSAL.value = P_NUM_INTERMED_nOffice.value;
							 P_COD_OFICINA.value = P_NUM_INTERMED_nOfficeAgen.value;
							 setTimeout('$(self.document.forms[0].P_COD_OFICINA).change();',50);
							 P_COD_AGENCIA.value = P_NUM_INTERMED_nAgency.value;
							 setTimeout('$(self.document.forms[0].P_COD_AGENCIA).change();',50);
					      }
				       }
				   }
			   }
            }
        }
    }
}
//% BlankIntermedDepend: Blanquea los campos OFICINA AGENCIA E INTERMEDIARIO si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankIntermedDependRepmach()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_COD_OFICINA.value="";
        P_COD_AGENCIA.value="";
        P_NUM_INTERMED.value="";
        P_COD_OFICINA_nBran_off.value = "";
        P_COD_AGENCIA_nBran_off.value = "";
        P_COD_AGENCIA_nOfficeAgen.value = "";
        P_COD_AGENCIA_sDesAgen.value = "";
        P_NUM_INTERMED_nOffice.value="";
        P_NUM_INTERMED_nOfficeAgen.value="";
        P_NUM_INTERMED_nAgency.value="";
        P_NUM_INTERMED_sCliename.value="";
        P_NUM_INTERMED.Parameters.Param1.sValue = 0;
        P_NUM_INTERMED.Parameters.Param2.sValue = 0; 
        P_NUM_INTERMED.Parameters.Param3.sValue = 0;       
        
    }
    UpdateDiv('P_COD_OFICINADesc','');
    UpdateDiv('P_COD_AGENCIADesc','');
    UpdateDiv('P_NUM_INTERMEDDesc','');
    
}
//% BlankOfficeAgenDepend: Blanquea los campos AGENCIA E INTERMEDIARIO si y sólo si el valor del
//%                 campo OFICINA cambia
//-------------------------------------------------------------------------------------
function BlankOfficeAgenDependRepmach()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_COD_AGENCIA.value="";
        P_NUM_INTERMED.value="";
        P_COD_AGENCIA_nBran_off.value = "";
        P_COD_AGENCIA_nOfficeAgen.value = "";
        P_COD_AGENCIA_sDesAgen.value = "";
        P_NUM_INTERMED_nOffice.value="";
        P_NUM_INTERMED_nOfficeAgen.value="";
        P_NUM_INTERMED_nAgency.value="";
        P_NUM_INTERMED_sCliename.value="";
        P_NUM_INTERMED.Parameters.Param1.sValue = 0;
        P_NUM_INTERMED.Parameters.Param2.sValue = 0; 
        P_NUM_INTERMED.Parameters.Param3.sValue = 0;       
        
    }
    UpdateDiv('P_COD_AGENCIADesc','');
    UpdateDiv('P_NUM_INTERMEDDesc','');
    
}
//% AddClaimParameter: Actualiza el Valor del Parametro para el control de Casos 
//%                    de Siniestros y la Ubicación
//-----------------------------------------------------------------------------
function AddClaimParameter(nValue){
//-----------------------------------------------------------------------------

	with(self.document.forms[0]){
		P_NUM_CASO.Parameters.Param1.sValue = (P_NUM_SINIESTRO.value==''?0:P_NUM_SINIESTRO.value);
    }
}
//-------------------------------------------------------------------------------------
function BlankCaseNum()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_NUM_CASO.value="";
    }
    UpdateDiv('P_NUM_CASODesc','');
    
}
//% AddModulParameter: Actualiza el Valor del Parametro para el control de Modulos 
//%                    del Producto y Ramo
//-----------------------------------------------------------------------------
function AddModulParameter(nValue){
//-----------------------------------------------------------------------------

	with(self.document.forms[0]){
        if (nValue == 1){
 		         P_NUM_PLAN.Parameters.Param1.sValue = (P_RAMO.value==''?0:P_RAMO.value);
 		         P_COD_COB.Parameters.Param1.sValue = (P_RAMO.value==''?0:P_RAMO.value);
		         P_NUM_PLAN.Parameters.Param2.sValue = (P_PRODUCTO.value==''?0:P_PRODUCTO.value);
		         P_COD_COB.Parameters.Param2.sValue = (P_PRODUCTO.value==''?0:P_PRODUCTO.value);
  		        P_COD_COB.Parameters.Param3.sValue = 0;
 		}         
       else{ 
           if (nValue == 2){		         
  		        P_COD_COB.Parameters.Param3.sValue = (P_NUM_PLAN.value==''?0:P_NUM_PLAN.value); 
		   }  
		   
		}
		         
  		}
    
}

//-------------------------------------------------------------------------------------
function Blanknmodulec()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_NUM_PLAN.value="";
        P_COD_COB.value="";
    }
    UpdateDiv('P_NUM_PLANDesc','');
    UpdateDiv('P_COD_COBDesc','');
    
}


//-------------------------------------------------------------------------------------
function PValMes()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0])
        {
           if(P_MES.value <= 0 || P_MES.value > 12) 
             {
              alert('Mes no Válido.');
              P_MES.value= '';
              P_MES.focus(true);
             }
        }
}

//%padLeft: Rellena con caracteres a la izquierda
//-------------------------------------------------------------------------------------
function padLeft(val, ch, num) {
//-------------------------------------------------------------------------------------
    var re = RegExp(".{" + num + "}$");
    var pad = "";

    do  {
        pad += ch;
    }while(pad.length < num)

    return re.exec(pad + val);
}

//%padRight: Rellena con caracteres a la derecha
//-------------------------------------------------------------------------------------
function padRight(val, ch, num){
//-------------------------------------------------------------------------------------
    var re = RegExp("^.{" + num + "}");
    var pad = "";

    do {
        pad += ch;
    } while (pad.length < num)

    return re.exec(val + pad);
}

//-------------------------------------------------------------------------------------
function SetValues(ControlName,ControlValue)
//-------------------------------------------------------------------------------------
{
    if (document.getElementById(ControlName).type == 'radio') {
        var Elementos = document.getElementsByName(ControlName);
        Elementos[ControlValue].checked = true;
    }
    else if (document.getElementById(ControlName).type == 'checkbox') {
        document.getElementById(ControlName).checked;
    }else
        document.getElementById(ControlName).value = ControlValue;
}

//-------------------------------------------------------------------------------------
function ExecEvent(ControlName,ControlValue)
//-------------------------------------------------------------------------------------
{
    if (document.getElementById(ControlName).type == 'select-one') {
            $('#' + ControlName).change();
    }

    if (document.getElementById(ControlName).type == 'text') {
            $('#' + ControlName).change();
    }

    if (document.getElementById(ControlName).type == 'radio') {
        var Elementos = document.getElementsByName(ControlName);
        Elementos[ControlValue].click(); 
    }

    if (document.getElementById(ControlName).type == 'checkbox') {
        if (document.getElementById(ControlName).click != null)
            document.getElementById(ControlName).click();
    }

}

function qs(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


function htmlDecode(input) {
    var e = document.createElement('div');
    e.innerHTML = input;
    return e.childNodes.length === 0 ? "" : e.childNodes[0].nodeValue;
}