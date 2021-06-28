//-------------------------------------------------------------------------------------------
// tmenu.js: Este archivo contiene las funciones utilizadas por el menu de acciones de la aplicación
//-------------------------------------------------------------------------------------------

    var pstrCodispl

//-------------------------------------------------------------------------------------------
function ShowLastValues(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/LastValues.aspx", "GoTo", 320, 320, "no"); 
}
//-------------------------------------------------------------------------------------------
function insMainMenu(){
//-------------------------------------------------------------------------------------------
    top.location.href= "/VTimeNet/VisualTime/VisualTime.htm";
}
//-------------------------------------------------------------------------------------------
function insPrevious(){
//-------------------------------------------------------------------------------------------
    top.location.href= "/VTimeNet/Common/GoTo.aspx?sPopUp=2&sCodispl=-1";
}
//-------------------------------------------------------------------------------------------
function insGeneralQue(){
//-------------------------------------------------------------------------------------------
    top.location.href= "/VTimeNet/Common/GoTo.aspx?sPopUp=2&sCodispl=GE099";
}
//-------------------------------------------------------------------------------------------
function insShowGoTo(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/GoTo.aspx?sPopUp=1", "GoTo", 370, 350, "no", "no", 100, 100); 
} 
//-------------------------------------------------------------------------------------------
function insShowBatchProcess() {
    //-------------------------------------------------------------------------------------------
    //  if(top.frames["fraHeader"].pstrCodispl!='BTC001' && top.frames["fraHeader"].pstrCodispl!='BTC001_1')
    ShowPopUp("/VTimeNet/Common/GoTo.aspx?sPopUp=2&ActionByToolbar=396&sCodispl=BTC001", "BTC001_TB", 0, 0, "no", "no", 0, 0, "no", "no", true);
}
//-------------------------------------------------------------------------------------------
function insChangeConnect(sCodispl){
//-------------------------------------------------------------------------------------------
	if (typeof(sCodispl)=='undefined') sCodispl = '';
    insDefValues('', 'sChangeLogin=1&sCodispl=' + sCodispl, '/VTimeNet/VisualTime', 'Login');
//    "/VTimeNet/VisualTime/Login.asp?sChangeLogin=1&sCodispl=" + sCodispl, "GoTo", 370, 280, "no", "no", 100, 100); 
    
} 
//-------------------------------------------------------------------------------------------
function insHandImage(lstrName,lblnStatus){
//-------------------------------------------------------------------------------------------
    try{
        top.fraHeader.document.images[lstrName].disabled = !lblnStatus;
        if (!top.fraHeader.document.images[lstrName].disabled)
            top.fraHeader.insChangeImage(lstrName,2)
    }
    catch(x){
    }
    finally{
    }
}

//% insDisabledAction: habilita/deshabilita el toolbar de la transacción
//-------------------------------------------------------------------------------------------
function insDisabledAction(nAction, bEnabled, nZone, nWindowType){
//-------------------------------------------------------------------------------------------
    if(nAction!=390)
        bEnabled=true;

	insHandImage("A301", (nZone==2)?false:bEnabled);
	insHandImage("A302", (nZone==2)?false:bEnabled);
	insHandImage("A303", (nZone==2)?false:bEnabled);
    insHandImage("A304", (nZone == 2) ? false : bEnabled);
    insHandImage("A396", (nZone == 2) ? false : bEnabled);
	insHandImage("A401", (nZone==2)?false:bEnabled);
	insHandImage("A402", (nZone==2)?false:bEnabled);
	insHandImage("A391", (nZone==2)?false:bEnabled);
	insHandImage("A393", (nAction==390)?false:!bEnabled);
	insHandImage("A390", (nZone==2)?false:((nWindowType==5)?!bEnabled:bEnabled));
    insHandImage("A392", (nAction == 390) ? false : ((nWindowType == 5) ? bEnabled : !bEnabled));
    insHandImage("A208", (top.frames["fraHeader"].pstrCodispl != 'BTC001' && top.frames["fraHeader"].pstrCodispl != 'BTC001_1') ? true : false);
}
//% 
//-------------------------------------------------------------------------------------------
function ShowHelp(lstrCodispl){
//-------------------------------------------------------------------------------------------
    var lintIndex=0;
    var lstrFraName='fraFolder';
    
    if (top.location.href.indexOf("SpeWOHeader")!=-1)
       lstrFraName='fraHeader';
    if(typeof(top.frames[lstrFraName])=='undefined')
       lstrFraName='fraHeader';
    if (typeof(top.frames['fraSequence'].pstrOnSeq)!='undefined')
        if (top.frames['fraSequence'].pstrOnSeq=='1'){
            lintIndex = top.frames[lstrFraName].document.location.href.indexOf("sCodispl=");
            lstrCodispl = top.frames[lstrFraName].document.location.href.substr(lintIndex,17);
            lstrCodispl = lstrCodispl.replace(/&.*/,"");
            lstrCodispl = lstrCodispl.replace("sCodispl=","");
        }
    ShowPopUp("/VTimeNet/Common/Help.aspx?sCodispl=" + lstrCodispl,"Help",600,500,"Yes","Yes",50,20);
}

//% ShowAbout: se muestra la ventana "Acerca de..."
//-------------------------------------------------------------------------------------------
function ShowAbout(lstrCodispl, lstrCodisp, lintWindowType){
//-------------------------------------------------------------------------------------------
    var lintIndex=0
    var lstrZone=""
    var lstrFraName='fraFolder'
    var lstrVersion=''
    var lstrComplement=''

    if (top.location.href.indexOf("SpeWOHeader")!=-1)
       lstrFraName='fraHeader'
    if(typeof(top.frames[lstrFraName])=='undefined')
       lstrFraName='fraHeader'
    lstrZone = (top.frames["fraSequence"].pintZone==1)?"fraHeader":"fraFolder";
    if (typeof(top.frames['fraSequence'].pstrOnSeq)!='undefined')
        if (top.frames['fraSequence'].pstrOnSeq=='1'){
            lintIndex   = top.frames[lstrFraName].document.location.href.indexOf("sCodispl=")
            lstrCodispl = top.frames[lstrFraName].document.location.href.substr(lintIndex,17)
            lstrCodispl = lstrCodispl.replace(/&.*/,"")
            lstrCodispl = lstrCodispl.replace("sCodispl=","")
            lintIndex   = top.frames[lstrFraName].document.location.href.indexOf("sCodisp=")
            lstrCodisp  = top.frames[lstrFraName].document.location.href.substr(lintIndex,17)
            lstrCodisp  = lstrCodispl.replace(/&.*/,"")
            lstrCodisp  = lstrCodispl.replace("sCodisp=","")
 
            if(typeof(top.frames[lstrFraName].document.VssVersion)!='undefined')
               lstrVersion = top.frames[lstrFraName].document.VssVersion
        }
    else
        if(typeof(top.frames[lstrZone].document.VssVersion)!='undefined')
           lstrVersion = top.frames[lstrZone].document.VssVersion

    lstrVersion = lstrVersion.replace("\$\$\Revision: ","&VSSVersion=")
    lstrVersion = lstrVersion.replace(" \$\|\$\$\Date:","&VSSVersionDate=")
    lstrVersion = lstrVersion.replace(/ .*/,'')

    if (lintWindowType != 'undefined')
        if (top.frames["fraSequence"].pintZone==1)
            if (lintWindowType == 1 ||
                lintWindowType == 6) {
                lstrCodispl = lstrCodispl.toUpperCase();
//+ Se verifica si el código lógico no viene con "_K", para agregárselo.
                if (lstrCodispl.search("_K")==-1)
					lstrComplement = '_K'
                }
    ShowPopUp("/VTimeNet/Common/about.aspx?sCodispl=" + lstrCodispl + "&sComplement=" + lstrComplement + "&sCodisp=" + lstrCodisp + lstrVersion,"HelpAbout",300,160,"No","No",50,30);
}

//-------------------------------------------------------------------------------------------
function Logout(lintWindowType){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/LogOut.aspx","EndSession",280,70);
}

//% InsMoveRecord: Se posiciona en el valor del arreglo de la forma
//-------------------------------------------------------------------------------------------
function InsMoveRecord(sOption, nWindowType, sFrames){
//-------------------------------------------------------------------------------------------
    var lstrAction
    var ldblIndex
    var ldblLast
    var ldblTotal
    if(nWindowType==3 || nWindowType==6){
        with(top.frames[sFrames].document.forms[0]){
            if(typeof(hddFirstRecord)!='undefined'){
                if(hddFirstRecord.value=='')
                    hddFirstRecord.value=1;

                ldblIndex = parseFloat(hddFirstRecord.value);
                ldblTotal = parseFloat(hddTotalRecord.value);
                
                switch (sOption){
                    case "First":
                        ldblIndex=1;
                        break;
                    case "Previous":
                        ldblIndex=(ldblIndex<=1)?1:ldblIndex-20;
                        break;
                    case "Next":
                        ldblIndex=ldblIndex+20;
                        break;
                    case "Last":
                        ldblIndex=ldblTotal-19;
                        if(ldblIndex<=0)
                            ldblIndex=1;
                }
                ldblLast = ldblIndex + 19;
                
                lstrAction = top.frames[sFrames].document.location.href;
                lstrAction = lstrAction.replace(/\?.*/, "") + "?" + hddQueryString.value;
                lstrAction = lstrAction.replace(/&nFirstRecord=[0-9]*/,"&nFirstRecord=" + ldblIndex);
                lstrAction = lstrAction.replace(/&nLastRecord=[0-9]*/,"&nLastRecord=" + ldblLast);
                
                top.frames[sFrames].document.location.href = lstrAction;
            }
        }
    }
    else{
        ldblIndex = mlngCurrentIndex
        switch (sOption){
            case "First":
                ldblIndex = 0;
                break;
            case "Previous":
                ldblIndex--;
                break;
            case "Next":
                ldblIndex++;
                break;
            case "Last":
                ldblIndex = mArray.length - 1;
        }
                
        if (ldblIndex >= 0)
            if (ldblIndex < mArray.length){
                ShowFields(ldblIndex);
                mlngCurrentIndex = ldblIndex
            }
    }
}

//% InsDoSubmit: Ejecuta el submit de la forma
//-------------------------------------------------------------------------------------------
function InsDoSubmit(lstrZone, bEnabledControl, llngAction, lintWindowType){
//-------------------------------------------------------------------------------------------
	var lstrDoSubmit = '1';
    var lblnDoSubmit = true;
	var lintPos;
	var lstrAction;
	var ref;
	var lstrAux;

	if (typeof(top.frames[lstrZone].mstrDoSubmit) != 'undefined')
		lstrDoSubmit = top.frames[lstrZone].mstrDoSubmit;

	if (lstrDoSubmit == '1'){
        if (llngAction==392 && typeof(top.frames["fraHeader"].insFinish) != 'undefined')
            lblnDoSubmit = top.frames["fraHeader"].insFinish();
        if (lblnDoSubmit){
            lintPos = top.frames[lstrZone].document.forms[0].action.search("&TIMEINFO");
            if (lintPos==-1){lintPos=top.frames[lstrZone].document.forms[0].action.length}
            lstrAction = top.frames[lstrZone].document.forms[0].action.substr(0,lintPos);
            ref = /sCodispl=[A-Z]*[0-9]*\&*/g;
            lstrAction = lstrAction.replace(ref, "");
            lstrAux = lstrAction.substr(lstrAction.length-1, 1);
            if (lstrAux == '&') lstrAction = lstrAction.substr(0, lstrAction.length - 1);
            lstrAction = lstrAction + "&TIMEINFO=1&sCodispl=" + top.frames["fraHeader"].pstrCodispl + "&nAction=" + llngAction + "&nZone=" + top.frames["fraSequence"].pintZone 

            if (top.frames["fraSequence"].pintZone == 1 || top.frames["fraSequence"].plngMainAction==306)
                lstrAction = lstrAction + "&nMainAction=" + top.frames["fraSequence"].plngMainAction;
            top.frames[lstrZone].document.forms[0].action = lstrAction;
            StatusControl(true, top.frames["fraSequence"].pintZone)
            if (top.fraHeader.document.location.href.indexOf("InSequence")>=0){
                insHandImage("A301", false);
                insHandImage("A302", false);
                insHandImage("A303", false);
                insHandImage("A304", false);
                insHandImage("A390", false);
                insHandImage("A391", false);
                insHandImage("A392", false);
                insHandImage("A393", false);
                insHandImage("A401", false);
                insHandImage("A396", false);
            }
            else
                insDisabledAction(llngAction, false, top.frames["fraSequence"].pintZone, lintWindowType)
			if (bEnabledControl) EnabledControl(lstrZone);
			setPointer('wait');
			top.frames[lstrZone].document.forms[0].target = 'fraSubmit';
			top.frames[lstrZone].document.forms[0].submit();
		}
	}
}

//% ClientRequest: se controlan las acciones del menú
//-------------------------------------------------------------------------------------------
function ClientRequest(llngAction, lintWindowType){
//-------------------------------------------------------------------------------------------
    var lstrAction 
    var lintPos
    var lstrZone 
    var lobjImage 
    var lblnOnSeq
    var lblnValid
    var lblnRefresh = false
    var lblnAccept = false
    var lstrURL = self.document.location.href
    
    try{lblnValid=(mblnValid);
    }
    catch(x){
        lblnValid=true;
    }
    finally{
        if (!lblnValid){
            mblnValid = true
            return;
        }
    }
    lblnOnSeq = (lintWindowType==2 || lintWindowType==4);
    if  (top.frames["fraHeader"].document.images["A" + llngAction] == null){
        lobjImage = new Object;
        lobjImage.disabled = false;
    }
    else{ 
        lobjImage = top.frames["fraHeader"].document.images["A" + llngAction]; 
    } 
    if (typeof(lobjImage.disabled) =="undefined") 
        lobjImage.disabled = false
       if (!lobjImage.disabled) {
       	if (top.frames["fraSequence"].pintZone == 1) {
       		lstrZone = "fraHeader";
       	}
       	else
       		lstrZone = "fraFolder";
       	if ((llngAction > 300 && llngAction < 380) || llngAction == 401 || llngAction == 402)
       	    top.frames["fraSequence"].plngMainAction = llngAction;
       	    top.plngMainAction = llngAction;
       	if (((llngAction > 300 && llngAction < 380) || llngAction == 401 || llngAction == 402) && lstrZone == "fraHeader") {
       		if (top.frames["fraSequence"].pintZone == 1) {
       			if (typeof (top.frames[lstrZone].insPreZone) != 'undefined')
       				top.frames[lstrZone].insPreZone(llngAction)
       			insDisabledAction(llngAction, true, 1, lintWindowType)
       		}
       		//+ Se coloca la acción a ejecutar en la barra de estado
       		if (llngAction != 402)
       			window.status = "Acción: " + top.fraHeader.document.images["A" + llngAction].alt
       		if (typeof (top.frames[lstrZone].insStateZone) != 'undefined')
       			top.frames[lstrZone].insStateZone(llngAction);
       	}
       	else if (llngAction == 391) {
       		top.frames[lstrZone].mblnValid = false
       		//            if (confirm("¿Desea cancelar la transacción?")){
       		if (confirm("¿Desea cancelar la transacción?")) {
       		    if (top.frames["fraHeader"].insCancel()) {
       		        insReloadTop(true, false);
       		    }
       		}
       	}
       	else if (llngAction == 393) {
       		top.frames[lstrZone].document.location.reload();
       	}
       	else if (llngAction == 394) {
       		top.frames[lstrZone].focus();
       		parent.frames[lstrZone].print()
       	}
       	else if (llngAction == 390 || (llngAction == 392 && !lblnOnSeq)) {
       		InsDoSubmit(lstrZone, true, llngAction, lintWindowType);
       	}
       	else if (llngAction == 392) {
       		InsDoSubmit(lstrZone, top.frames["fraSequence"].plngMainAction != 401, llngAction, lintWindowType);
       	}
       	else if (llngAction == 201) {
       		insMainMenu();
       	}
       	else if (llngAction == 203) {
       		lstrURL = lstrURL.toUpperCase();
       		if (lstrURL.search("GE099") == -1)
       			insPrevious();
       		else
       			insMainMenu();
       	}
       	else if (llngAction == 207) {
       		insGeneralQue();
        }
        else if (llngAction == 396) {
            insShowBatchProcess();
        }
       	else if (llngAction == 204) {
       		insShowGoTo();
       	}
       	else if (llngAction == 490) {
       		InsMoveRecord("First", lintWindowType, lstrZone);
       	}
       	else if (llngAction == 491) {
       		InsMoveRecord("Previous", lintWindowType, lstrZone);
       	}
       	else if (llngAction == 492) {
       		InsMoveRecord("Next", lintWindowType, lstrZone);
       	}
       	else if (llngAction == 493) {
       		InsMoveRecord("Last", lintWindowType, lstrZone);
       	}
       	else if (llngAction == 306) {
       		top.frames[lstrZone].ShowDiv('DivHeaderDup', 'show');
       		insHandImage("A306", false);
       		insHandImage("A391", true);
       	}
       }
       else {
       	alert(resValues.noActionRunMessage);
       	//        alert("La acción no se puede ejecutar en este momento")
       }
}

