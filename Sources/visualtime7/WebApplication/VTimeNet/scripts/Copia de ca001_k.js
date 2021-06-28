//--------------------------------------------------------------------
//- $$Workfile: ca001_k.js $ 
//- $$Author: Lpizarro $ 
//- $$Date: 21/03/06 17:50 $ 
//- $$Revision: 3 $ 
//--------------------------------------------------------------------

//- Variable global que almacena el tipo de poliza Individual, Colectivo, Multilocalidad 
var sPolitype
//- Variable que almacena la transación original
var nTransaction_ori

//% ShowVerifyData: Llama a la pantalla de verificacion de datos
//-------------------------------------------------------------------------------------------
function ShowVerifyData(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms["CA001"]){
        ShowPolicyData('2', cbeBranch.value, valProduct.value, tcnPolicy.value,  tcnCertificat.value)
    }
}

//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeAgency.value="";
        cbeOfficeAgen_nBran_off.value = "";
        cbeAgency_nBran_off.value = "";
        cbeAgency_nOfficeAgen.value = "";
        cbeAgency_sDesAgen.value = "";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
    UpdateDiv('cbeAgencyDesc','');
}

//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%                      producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
//- Esta variable se define para asignar el sCertype a utilizar en la búsqueda de los datos de la póliza
    var lstrCertype
    
    setPointer('wait');

    with(self.document.forms[0]){
        switch(cbeTransactio.value){
//+Cotización de Modificación de póliza
            case ePolTransac.clngPolicyQuotAmendent:
//+Cotización de Modificación de certificado            
            case ePolTransac.clngCertifQuotAmendent:    
//+Propuesta de Modificación de póliza            
            case ePolTransac.clngPolicyPropAmendent:
//+Propuesta de Modificación de certificado                
            case ePolTransac.clngCertifPropAmendent:
//+Cotización de Renovación de póliza            
            case ePolTransac.clngPolicyQuotRenewal:             
//+Cotización de Renovación de certificado            
            case ePolTransac.clngCertifQuotRenewal:             
//+Propuesta de Renovación de póliza            
            case ePolTransac.clngPolicyPropRenewal:             
//+Propuesta de Renovación de Certificado            
            case ePolTransac.clngCertifPropRenewal:            
//+Conversión Cotizacion de Modificación a modificación            
            case ePolTransac.clngQuotAmendConvertion:        
//+Conversión Propuesta de Modificación a modificación            
            case ePolTransac.clngPropAmendConvertion:        
//+Conversión Cotización de Renovación a póliza            
            case ePolTransac.clngQuotRenewalConvertion:      
//+Conversión Propuesta de Renovación a póliza            
            case ePolTransac.clngPropRenewalConvertion:      
//+Conversión Cotizacion de Modificación a Propuesta de Modificación             
            case ePolTransac.clngQuotPropAmendentConvertion: 
//+Conversión Cotizacion de Renovación a Propuesta de Renovación            
            case ePolTransac.clngQuotPropRenewalConvertion:  
//+Consulta de Cotización de Modificación            
            case ePolTransac.clngQuotAmendentQuery:          
//+Consulta de Propuesta de Modificación            
            case ePolTransac.clngPropAmendentQuery:             
//+Consulta de Cotización de Renovación            
            case ePolTransac.clngQuotRenewalQuery:           
//+Consulta de Propuesta de Renovación                
            case ePolTransac.clngPropRenewalQuery:           
//+rehabilitacion
            case "43":             
            case "44":
                lstrCertype = 2;
                break;
            case "4":
                lstrCertype = 3;    
                break;  
//+Duplicar Poliza
            case "45":
                  lstrCertype = 2;
                  break;                
//+Traspaso de asegurado
            case "46":
                  lstrCertype = 2;
                  break;
            default:
                lstrCertype = sCertype.value;
        }
        switch(sField){
            case "nBranch":
                document.forms[0].tcnPolicy.value = '';
                document.forms[0].tcnPolicy.disabled = true;        
                document.forms[0].btntcnPolicy.disabled = true;
            case "Product":
                if (document.forms[0].valProduct.value != ''){
                    document.forms[0].tcnPolicy.disabled = false;        
                    document.forms[0].btntcnPolicy.disabled = false;
                }
//                else{
//                    document.forms[0].tcnPolicy.value = '';
//                    document.forms[0].tcnPolicy.disabled = true;        
//                }
                insDefValues(sField, "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&dEffecdate=" + tcdEffecdate.value + "&nTransaction=" + cbeTransactio.value,'/VTime/Policy/PolicySeq');
                break;
            case "Certificat":
                if(tcnCertificat.value!="")
                    if (cbeBranch.value!="0" &&
                        valProduct.value!="" &&
                        tcnPolicy.value!="")
                        insDefValues(sField, "sCertype=" + sCertype.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value  + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value + "&nQuotProp=" + tcnQuotProp.value,'/VTime/Policy/PolicySeq')
                break; 
            case "Policy":
//                if (cbeBranch.value!="0" &&
//                    valProduct.value!="" &&
//                    tcnPolicy.value!="")
                    insDefValues(sField, "sCertype=" + lstrCertype + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value + "&nQuotProp=" + tcnQuotProp.value + "&dEffecdate=" + tcdEffecdate.value,'/VTime/Policy/PolicySeq')
//                    if (self.document.forms[0].cbeTransactio.value == 45)

                break;

            case "Endoso":
                if(cbeBranch.value!="0" &&
                   valProduct.value!="" &&
                   tcnPolicy.value!="" &&
                   tcnCertificat.value!="" &&
                   valType_amend.value!="")
                    insDefValues(sField, "sCertype=" + sCertype.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value  + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value + "&dEffecdate=" + tcdEffecdate.value + "&nType_amend=" + valType_amend.value + "&nServ_order=" + tcnServ_order.value + "&nQuotProp=" + tcnQuotProp.value,'/VTime/Policy/PolicySeq')
                else
					setPointer('');
                break;
            case "Agency":
                if(cbeAgency.value!="")
                    insDefValues(sField, "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value +"&nOffice=" + cbeOffice.value,'/VTime/Policy/PolicySeq')
                break;
            case "nServ_order":
                insDefValues(sField, "sCertype=" + sCertype.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value  + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value + "&dEffecdate=" + tcdEffecdate.value + "&nType_amend=" + valType_amend.value + "&nServ_order=" + tcnServ_order.value,'/VTime/Policy/PolicySeq')
                break;
        }
    }
    
}
        
//% LockControl: Habilita/Deshabilita los controles dependientes de la página
//-------------------------------------------------------------------------------------------
function LockControl(Control){
//-------------------------------------------------------------------------------------------
    switch(Control){
        case 'Policy':
            with(document.forms[0]){
                if((tcnPolicy.value=='0') ||
                   (tcnPolicy.value=='')){
                    tcnCertificat.disabled = true
                    tcnCertificat.value='0'
                    btnPolicyValues.disabled = true
                }
                else
                {
                   if (((valProduct.value=='0')||
                        (valProduct.value==''))&&
                       ((cbeBranch.value=='0')||
                        (cbeBranch.value==''))) {
                       tcnCertificat.disabled = true
                       tcnCertificat.value='0'
                       btnPolicyValues.disabled = false
                      }
                   else {
                       tcnCertificat.disabled = false
                       tcnCertificat.value='0'
                       btnPolicyValues.disabled = false
                    }
                }
            }
    }
}

//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd) {
//-------------------------------------------------------------------------------------------
if (self.document.forms[0].cbeTransactio.value == 1  ||
    self.document.forms[0].cbeTransactio.value == 4  ||
    self.document.forms[0].cbeTransactio.value == 6	 ||
    self.document.forms[0].cbeTransactio.value == 30 ||
    self.document.forms[0].cbeTransactio.value == 31) {
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            if (typeof(cbeOffice)!='undefined'){
                if (cbeOffice.value != 0){
                    if (typeof(cbeOfficeAgen)!='undefined'){
                        if (cbeTransactio.value == 1  ||
                            cbeTransactio.value == 4  ||
                            cbeTransactio.value == 6  ||
                            cbeTransactio.value == 30 ||
                            cbeTransactio.value == 31) {

                            cbeOfficeAgen.disabled = false;
                            btncbeOfficeAgen.disabled = false;
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeOfficeAgen.Parameters.Param2.sValue = 0;
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
                        else
                            cbeAgency.Parameters.Param2.sValue = 0;
                    }
                }
                else{
                      if(typeof(cbeOfficeAgen)!='undefined'){
                          if (cbeTransactio.value == 1  ||
                              cbeTransactio.value == 4  ||
                              cbeTransactio.value == 6  ||
                              cbeTransactio.value == 30 ||
                              cbeTransactio.value == 31) {
                            cbeOfficeAgen.disabled = false;
                            btncbeOfficeAgen.disabled = false;
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeOfficeAgen.Parameters.Param2.sValue = 0;
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0){
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);}
                        else{
                            cbeAgency.Parameters.Param2.sValue = 0;}
                    }
                }
            }
        }
//+ Cambia la oficina 
        else{
            if (nInd == 2){
                if(cbeOfficeAgen.value != ''){
                    cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                    cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
                    cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                    cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                }
                else{
                    cbeAgency.Parameters.Param1.sValue = 0;    
                    cbeAgency.Parameters.Param2.sValue = 0;
                }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
                    if(cbeAgency.value != ""){
                        cbeOffice.value = cbeAgency_nBran_off.value;
                        if (cbeOfficeAgen.value == ''){
                            cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                            UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
                    }
                }
            }
        }
    }
} // prueba    
}

//% insInitialAction: inicializa los campos de la página cuando ésta es llamada desde otra 
//%                   transacción
//---------------------------------------------------------------
function insInitialAction(bdisabled, sCodisplOrig){
//---------------------------------------------------------------
    with (self.document.forms["CA001"]) {
        
        cbeTransactio.value = mstrTransaction; 
        if (mstrTransaction == 6 ||
            mstrTransaction == 7){
            cbeTransactio.value = 1
            insSelTransaction();
            tcdEffecdate.value = "";
            cbeBranch.value = "";
            valProduct.Parameters.Param1.sValue = "";
            valProduct.value = "";
            valProduct.disabled = true;
            btnvalProduct.disabled = true;
            tcnPolicy.value = "";
            tcnCertificat.value = "";
            insDefValues("Propolcer", "sCertype=" + sCertype.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value  + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value,'/VTime/Policy/PolicySeq')
        }
        else{
            insSelTransaction();
            tcdEffecdate.value = mdtmEffecdate
            cbeBranch.value = mintBranch
            valProduct.Parameters.Param1.sValue = cbeBranch.value;
            valProduct.value = mintProduct
            tcnPolicy.value = mintPolicy
            tcnCertificat.value = mintCertificat
            insDefValues("Propolcer", "sCertype=" + sCertype.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value  + "&nCertif=" + tcnCertificat.value + "&nTransaction=" + cbeTransactio.value,'/VTime/Policy/PolicySeq')
			if (sCodisplOrig == 'CAC011')
				cbeTransactio.disabled = true;
        }

//+ Deshabilitacion de llave
        if (bdisabled) {
            //tcdEffecdate.disabled = true
            btn_tcdEffecdate.disabled = true
            cbeBranch.disabled = true
            valProduct.disabled = true
            btnvalProduct.disabled = true
            tcnPolicy.disabled = true
            btntcnPolicy.disabled = true;            
            tcnCertificat.disabled = true
            optBussines[0].disabled = true
            optBussines[1].disabled = true
            optBussines[2].disabled = true
            optType[0].disabled = true
            optType[1].disabled = true
            optType[2].disabled = true
        }
        else
            tcnPolicy.onblur();   
    }
}


//% insInitialFields: blanquea todos los campos de la página
//-------------------------------------------------------------------------------------------
function insInitialFields(lblnDefValue, sCodisplOrig){
//-------------------------------------------------------------------------------------------
//+ Si la función se invoca cuando se carga la página por primera vez...    
    if(!(lblnDefValue)){
        lblnDefValue=false
    }
    
    with (self.document.forms["CA001"]) {
        cbeOffice.value = mintUserOffice
        if(sCodisplOrig="")
			if (mintMenu != 1) {     
			   cbeBranch.value = 0
			   valProduct.value = ""
			   UpdateDiv("valProductDesc","")   
			   tcnPolicy.value = ""       
			   tcnCertificat.value = 0       
			}
        optBussines[0].checked = true
        optBussines[1].checked = false
        optBussines[2].checked = false

        optType[0].checked = true
        optType[1].checked = false
        optType[2].checked = false

        tcdLedgerDate.value = GetDateSystem()

        ShowDiv('divConvertion', 'hide');

//+ Se bloquean los controles de la página (acción por defecto)
        if(lblnDefValue){
            if(cbeBranch.value <= 0) {
                valProduct.disabled = true;
                btnvalProduct.disabled = true;
            }
            tcnCertificat.disabled = true;
            ShowDiv('divExpireDate', 'hide');
            ShowDiv('divType_amend', 'hide');
            ShowDiv('divProp_Reg', 'hide');
            ShowDiv('divProp_Reg2', 'hide');
            ShowDiv('divRenewalNum', 'hide');
            ShowDiv('divRenewalNum2', 'hide');
        }
    }    
    
}

//% InsOfficeca001c: Oculta campos de la página
//-------------------------------------------------------------------------------------------
function InsOfficeca001c(Value){
//-------------------------------------------------------------------------------------------
    with (self.document.forms["CA001"]) {
       tcdEffecdate.value = GetDateSystem()
       cbeOffice.value = Value;
       cbeOffice.disabled = true;
       cbeOfficeAgen.onblur()       
              
    }    
}

//% InsOfficeAgenca001c: Oculta campos de la página
//-------------------------------------------------------------------------------------------
function InsOfficeAgenca001c(Value){
//-------------------------------------------------------------------------------------------
    with (self.document.forms["CA001"]) {
       cbeOfficeAgen.value = Value;
       cbeOfficeAgen.disabled = true;
       cbeAgency.onblur();
    }    
}

//% InsAgencyca001c: Oculta campos de la página
//-------------------------------------------------------------------------------------------
function InsAgencyca001c(Value){
//-------------------------------------------------------------------------------------------
    with (self.document.forms["CA001"]) {
       cbeAgency.value = Value;
       cbeAgency.disabled = true;
    }    
}


//% insHideFields: Oculta campos de la página
//-------------------------------------------------------------------------------------------
function insHideFields(valueofficeagen,valueagency){
//-------------------------------------------------------------------------------------------
    with (self.document.forms["CA001"]) {
       tcdEffecdate.value = GetDateSystem();
       cbeTransactio.value="4";    
       cbeOfficeAgen.value = valueofficeagen;
       cbeAgency.value = valueagency;                         
       //cbeSellchannel.value="1";
    }    
    ShowDiv('divEffecdate', 'hide');
    ShowDiv('divEffecdate2', 'hide');
//    ShowDiv('divOffice', 'hide');
//    ShowDiv('divOffice2', 'hide');                            
//    ShowDiv('divOfficeA', 'hide');    
//    ShowDiv('divOfficeA2', 'hide');    
//    ShowDiv('divAgency', 'hide');
//    ShowDiv('divAgency2', 'hide');
    ShowDiv('divChannel', 'hide');
    ShowDiv('divChannel2', 'hide');
    ShowDiv('divServOrder', 'hide');
    ShowDiv('divServOrder2', 'hide');
    ShowDiv('divHorline', 'hide');
    ShowDiv('divHorline2', 'hide');
    ShowDiv('divPoliType', 'hide');    
    ShowDiv('divPoliType2', 'hide');        
    ShowDiv('divLedgerdate0', 'hide');    
    ShowDiv('divLedgerdate', 'hide');
    ShowDiv('divLedgerdate2', 'hide');                                                                                                                                                                                                                                                            
    ShowDiv('divCotProp', 'hide');
    ShowDiv('divCotProp2', 'hide');                                                                                                                                                                                                                                                            
    ShowDiv('divExpireDate', 'hide');
    ShowDiv('divType_amend', 'hide');
    ShowDiv('divProp_Reg', 'hide');
    ShowDiv('divProp_Reg2', 'hide');
    ShowDiv('divRenewalNum', 'hide');
    ShowDiv('divRenewalNum2', 'hide');
             
}


//% ShowProp_Reg: se encarga de hacer visible el campo propuesta regularizada
//--------------------------------------------------------------------------------------------
function ShowProp_Reg(ntransactio){
//--------------------------------------------------------------------------------------------
    if ((ntransactio != ePolTransac.clngCertifProposal)&&
        (ntransactio != ePolTransac.clngPolicyProposal)&&
        (ntransactio != ePolTransac.clngProposalQuery)){
        ShowDiv('divProp_Reg', 'hide');
        ShowDiv('divProp_Reg2', 'hide');
    }
    else{
        ShowDiv('divProp_Reg', 'show');
        ShowDiv('divProp_Reg2', 'show');
    }
}
//% ShowdivPol_dest: se encarga de hacer visible el campo póliza destino
//--------------------------------------------------------------------------------------------
function ShowdivPol_des(ntransactio){
//--------------------------------------------------------------------------------------------
    if ((ntransactio != ePolTransac.clngTransHolder )){
        ShowDiv('divPol_dest', 'hide');
        ShowDiv('divPol_dest2', 'hide');
    }
    else{
        ShowDiv('divPol_dest', 'show');
        ShowDiv('divPol_dest2', 'show');
    }
}	
//% insSelTransaction: Habilita/Deshabilita los controles de la página, dependiendo de la 
//%                    acción que se seleccione.
//--------------------------------------------------------------------------------------------
function insSelTransaction(){
//--------------------------------------------------------------------------------------------
    var lstrPolicyDescript = ""
    mstrTransaction = document.forms["CA001"].elements["cbeTransactio"].value;

    insInitialFields();
    
   
//+ Deja en blanco los campos oficina y agencia cuando se cambia la operación

    with(document.forms["CA001"]){
        if (mstrTransaction != 4){ 
            cbeOfficeAgen.value = "";
            cbeOfficeAgen.Parameters.Param1.sValue = 0;
            cbeAgency.value = "";
//+        
            cbeAgency.Parameters.Param1.sValue = 0;
            cbeAgency.Parameters.Param2.sValue = 0;
            cbeAgency_nBran_off.value = "";
            cbeAgency_nOfficeAgen.value = "";
            cbeOfficeAgen.value = "";
            cbeAgency_sDesAgen.value = "";
         }     
//+        
        tcnProp_Reg.value  = "";
        tcnPolicy_Digit.value = "0";
        tcnRenewalNum.value = "";
        if (mstrTransaction != 4)        
           tcdEffecdate.value = "";
        cbeBranch.value = '';
        valType_amend.value = '';
        UpdateDiv('valType_amendDesc','');
        if (mstrTransaction != 40)
            tcnQuotProp.value = '';
        
        
//+ Se inhabilitan los campos ramo y producto        
        cbeBranch.disabled = true;
        valProduct.disabled = true;
        valProduct.value = "";
        tcnPolicy.disabled = true;
        btntcnPolicy.disabled = true;        
        UpdateDiv('valProductDesc','');
        tcnPolicy.value = '';
        tcnCertificat.value = '';
        
        UpdateDiv("cbeOfficeAgenDesc","")   
        UpdateDiv("cbeAgencyDesc","")   
    
        ShowProp_Reg(mstrTransaction);
        if (mstrTransaction == '1' ||
            mstrTransaction == '4'||
            mstrTransaction == '6')
            cbeSellchannel.value = 1;
        else
            cbeSellchannel.value = '';
        
    }
    switch(mstrTransaction){
        case ePolTransac.clngPolicyQuotAmendent:
        case ePolTransac.clngCertifQuotAmendent:
        case ePolTransac.clngPolicyQuotRenewal:
        case ePolTransac.clngCertifQuotRenewal:
        case ePolTransac.clngQuotAmendConvertion:
        case ePolTransac.clngQuotRenewalConvertion:
        case ePolTransac.clngQuotAmendentQuery:
        case ePolTransac.clngQuotRenewalQuery:
        case ePolTransac.clngQuotPropAmendentConvertion:
        case ePolTransac.clngQuotPropRenewalConvertion:
            UpdateDiv('divCotProp','Cotización');
            break;
        case ePolTransac.clngPolicyPropAmendent:
        case ePolTransac.clngCertifPropAmendent:
        case ePolTransac.clngPolicyPropRenewal:
        case ePolTransac.clngCertifPropRenewal:
        case ePolTransac.clngPropAmendConvertion:
        case ePolTransac.clngPropRenewalConvertion:
        case ePolTransac.clngPropAmendentQuery:
        case ePolTransac.clngTransHolder:
        case ePolTransac.clngPropRenewalQuery:
            UpdateDiv('divCotProp','Propuesta');
            break;
        case "43":
        case "44":
            UpdateDiv('divCotProp','Propuesta Rehabilitación');
            break;
        default:
            UpdateDiv('divCotProp','Cotización/Propuesta');
    }

    switch(mstrTransaction){
//+ Propuesta de Rehabilitación de póliza
		case "43":
//+ Consult de propuesta de Rehabiliatción de poliza		
		case "44":            
		    mstrCertype = "8";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = false
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Propuesta
        case ePolTransac.clngPolicyProposal:
            document.forms["CA001"].cbeBranch.disabled = false;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            mstrCertype = "1";
            lstrPolicyDescript = mstrPolicyDescript6 // "<%= mclsPolicy.TransactionCA001(6,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = false;
            break;
        case ePolTransac.clngCertifProposal:
            mstrCertype = "1";
            document.forms["CA001"].cbeBranch.disabled = false;
            lstrPolicyDescript = mstrPolicyDescript6 // "<%= mclsPolicy.TransactionCA001(6,True)%>";
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;
        case ePolTransac.clngProposalQuery:
        case ePolTransac.clngProposalConvertion:
            mstrCertype = "1";
            lstrPolicyDescript = mstrPolicyDescript6 //"<%= mclsPolicy.TransactionCA001(6,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;
//+ conversión de Cotización a propuesta
        case ePolTransac.clngPropQuotConvertion:
            mstrCertype = "3";
            lstrPolicyDescript = mstrPolicyDescript4 //"<%= mclsPolicy.TransactionCA001(4,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;
//+ modificación de póliza            
        case ePolTransac.clngPolicyAmendment:            
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = false;
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;        
//+Traspasar asegurado          
        case ePolTransac.clngTransHolder:            
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            document.forms["CA001"].tcnPolicyDest.disabled = false;            
            break;          
//+ modificación de certificado        
        case ePolTransac.clngCertifAmendment:
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = false;
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;        
//+ modificación temporal de póliza            
        case ePolTransac.clngTempPolicyAmendment:
//+ modificación temporal de certificado        
        case ePolTransac.clngTempCertifAmendment:            
        case ePolTransac.clngCertifAmendment:
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = true;
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;        
//+ Póliza
        case ePolTransac.clngPolicyIssue:
            document.forms["CA001"].cbeBranch.disabled = false;
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            break;        
        case ePolTransac.clngCertifIssue:
        case ePolTransac.clngRecuperation:
        case ePolTransac.clngPolicyQuery:
        case ePolTransac.clngCertifQuery:
        case ePolTransac.clngPolicyReissue:
        case ePolTransac.clngCertifReissue:
        case ePolTransac.clngReprint:
        case ePolTransac.clngDeclarations:
        case ePolTransac.clngCoverNote:
        case ePolTransac.clngInspections:
//        case ePolTransac.clngDuplPolicy:
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;
//+ Cotización 
        case ePolTransac.clngPolicyQuotation:
            
            document.forms["CA001"].cbeBranch.disabled = false;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            mstrCertype = "3"
            lstrPolicyDescript = mstrPolicyDescript4 //"<%= mclsPolicy.TransactionCA001(4,True)%>"
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            break;            
        case ePolTransac.clngCertifQuotation:
        case ePolTransac.clngQuotationQuery:
        case ePolTransac.clngQuotationConvertion:
            mstrCertype = "3"
            lstrPolicyDescript = mstrPolicyDescript4 //"<%= mclsPolicy.TransactionCA001(4,True)%>"
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break
//+ Conversión de cotización de modificación a modificación        
        case ePolTransac.clngQuotAmendConvertion:
//+ Conversión de cotización de modificación a propuesta de cotización        
        case ePolTransac.clngQuotPropAmendentConvertion:
//+ Consulta de cotización de modificación                    
        case ePolTransac.clngQuotAmendentQuery:
            mstrCertype = "4";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcdFer.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Cotización de modificación de poliza
        case ePolTransac.clngPolicyQuotAmendent:
//+ Cotización de modificación de certificado        
        case ePolTransac.clngCertifQuotAmendent:
            mstrCertype = "4";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = false;
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Cotización de renovación
        case ePolTransac.clngPolicyQuotRenewal:
        case ePolTransac.clngCertifQuotRenewal:
        case ePolTransac.clngQuotRenewalQuery:
        case ePolTransac.clngQuotRenewalConvertion:
        case ePolTransac.clngQuotPropRenewalConvertion:
            mstrCertype = "5";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Propuesta de modificación de poliza
        case ePolTransac.clngPolicyPropAmendent:
//+ Propuesta de modificación de certificado        
        case ePolTransac.clngCertifPropAmendent:
            mstrCertype = "6";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = false
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Consulta de modificación de poliza        
        case ePolTransac.clngPropAmendentQuery:
//+ Consulta de modificación de certificado
        case ePolTransac.clngPropAmendConvertion:
            mstrCertype = "6";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcdFer.disabled = true
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;            
//+ Propuesta de renovación
        case ePolTransac.clngPolicyPropRenewal:
        case "31":     //+Propuesta de Renovación de Certificado
        case ePolTransac.clngPropRenewalQuery:
        case ePolTransac.clngPropRenewalConvertion:
            mstrCertype = "7";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            break;
//+ Duplicar Poliza            
        case ePolTransac.clngDuplPolicy:
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 // "<%= mclsPolicy.TransactionCA001(1,True)%>";
            document.forms["CA001"].tcnProp_Reg.disabled = true;
            document.forms["CA001"].tcnPolicy.disabled = false;
            document.forms["CA001"].btntcnPolicy.disabled = false;
            document.forms["CA001"].tcnCertificat.disabled = true;
            document.forms["CA001"].tcnCertificat.value='0';
            break;            
        default:
            mstrCertype = "2";
            lstrPolicyDescript = mstrPolicyDescript1 //"<%= mclsPolicy.TransactionCA001(1,True)%>";
    }
    
    
    with(document.forms["CA001"]){    

        sCertype.value = mstrCertype;
        tcnPolicy.CertypeQuery=mstrCertype;

    //(mstrCertype);
	//(lstrPolicyDescript);

//+ Se asignan valores a las etiquetas que indican el tipo de documento 
//+ (Solicitud, Cotización, Póliza)  que se está tratando.
        UpdateDiv ("lblPolicyNum", lstrPolicyDescript)
    
//+ Se desbloquean todos los controles

        cbeTransactio.disabled = false;
        if (mstrCertype!=3)
            cbeOffice.disabled = false;
//        cbeBranch.disabled = false;
//        valProduct.disabled = false;
        
//+ Campos asociados a la compañía de reaseguro
        if(mstrCompanyType==eCompanyType.cstrBrokerOrBrokerageFirm){
            //valInsuranceCompany.disabled = false
            //valOriginalOffice.disabled = false
            //tctOriginalPolicy.disabled = false
        }
    
//        tcnPolicy.disabled = true
        optBussines[0].disabled = false
        optBussines[1].disabled = false
        optBussines[2].disabled = false
        optType[0].disabled = false
        optType[1].disabled = false
        optType[2].disabled = false
        tcdEffecdate.disabled = false
        tcdLedgerDate.disabled = false
        btn_tcdLedgerDate.disabled = false
        tcnCertificat.disabled = true
        btnPolicyValues.disabled = true;
    }
    switch(mstrTransaction){
//+Emision de pólizas
        case ePolTransac.clngPolicyIssue:        
//+Cotizacion de pólizas        
        case ePolTransac.clngPolicyQuotation:    
//+Propuesta de pólizas        
        case ePolTransac.clngPolicyProposal:    
            with(document.forms["CA001"]){
                cbeSellchannel.disabled = false
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = false
                btncbeAgency.disabled = false
                if((mstrTransaction==ePolTransac.clngPolicyQuotation) ||
                   (mstrTransaction==ePolTransac.clngPolicyProposal)){
                    tcdLedgerDate.value = ""
                    tcdLedgerDate.disabled = true
                    btn_tcdLedgerDate.disabled = true
                }
            }
            break; 
//+Re-Emision de pólizas                       
        case "18": 
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                tcdLedgerDate.disabled = true
                btn_tcdLedgerDate.disabled = true
                cbeSellchannel.disabled = false            
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
				cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
            }
            break;
//+Re-Emision de Certificado            
        case "19": 
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                tcdLedgerDate.disabled = true
                btn_tcdLedgerDate.disabled = true
                tcnCertificat.disabled = false
                cbeSellchannel.disabled = false 
                cbeOfficeAgen.disabled = true            
                btncbeOfficeAgen.disabled = true            
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
				cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false

            }
            break;
//+Emision de Certificado            
        case ePolTransac.clngCertifIssue:        
//+Cotizacion de Certificado        
        case ePolTransac.clngCertifQuotation:    
//+Propuesta de Certificado        
        case ePolTransac.clngCertifProposal:    
//+Propuesta de Certificado        
        case ePolTransac.clngCertifProposal:    
//+Cotización de Modificación de certificado        
        case ePolTransac.clngCertifQuotAmendent:            
//+Propuesta de Modificación de certificado        
        case ePolTransac.clngCertifPropAmendent:            
//+Cotización de Renovación de certificado        
        case ePolTransac.clngCertifQuotRenewal:                
//+Propuesta de Renovación de Certificado        
        case ePolTransac.clngCertifPropRenewal:                
//+Conversión Cotizacion de Modificación a modificación        
        case ePolTransac.clngQuotAmendConvertion:            
//+Conversión Propuesta de Modificación a modificación        
        case ePolTransac.clngPropAmendConvertion:            
//+Conversión Cotización de Renovación a póliza        
        case ePolTransac.clngQuotRenewalConvertion:            
//+Conversión Propuesta de Renovación a póliza        
        case ePolTransac.clngPropRenewalConvertion:            
//+Conversión Cotizacion de Modificación a Propuesta de Modificación         
        case ePolTransac.clngQuotPropAmendentConvertion:    
//+Conversión Cotizacion de Renovación a Propuesta de Renovación        
        case ePolTransac.clngQuotPropRenewalConvertion:        
//+Consulta de Cotización de Modificación        
        case ePolTransac.clngQuotAmendentQuery:                
//+Consulta de Propuesta de Modificación        
        case ePolTransac.clngPropAmendentQuery:    
//+Consulta de Cotización de Renovación                    
        case ePolTransac.clngQuotRenewalQuery:                
//+Consulta de Propuesta de Renovación            
        case ePolTransac.clngPropRenewalQuery:                
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                tcnCertificat.disabled = false
                cbeSellchannel.disabled = false
                if((mstrTransaction==ePolTransac.clngCertifQuotation) ||
                   (mstrTransaction==ePolTransac.clngCertifProposal)){
                    tcdLedgerDate.value = "" 
                    tcdLedgerDate.disabled = true
                    btn_tcdLedgerDate.disabled = true
                }
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                cbeSellchannel.disabled = false
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
				cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
            }
            break;
//+ Recuperación            
        case "3":
        
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                            
                cbeSellchannel.disabled = false
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
				cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
            }
            break;
//+Convertir Cotizacion a póliza            
        case "16": 
//+Convertir solicitud a póliza                    
        case "17": 
//+Duplicar póliza Matriz        
        case "45":
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                cbeSellchannel.disabled = true
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
                cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false

                if((mstrTransaction==ePolTransac.clngPolicyQuery) ||
                   (mstrTransaction==ePolTransac.clngQuotationQuery) ||
                   (mstrTransaction==ePolTransac.clngProposalQuery) ||
                   (mstrTransaction==ePolTransac.clngReprint)){
                    tcdLedgerDate.value = ""
                    tcdLedgerDate.disabled = true
                    btn_tcdLedgerDate.disabled = true
                }
            }        
            break; 
//+Consulta de póliza                       
        case "8":  
//+Consulta de Cotizacion        
        case "10": 
//+Consulta de Solicitud        
        case "11": 
//+Re-impresion        
        case "20": 
//+Declaraciones        
        case "21": 
//+Nota de cobertura        
        case "22": 
//+Convertir Solicitud a Cotizacion        
        case "23": 
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true 
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                cbeSellchannel.disabled = true
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
                cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
            
                if((mstrTransaction==ePolTransac.clngPolicyQuery) ||
                   (mstrTransaction==ePolTransac.clngQuotationQuery) ||
                   (mstrTransaction==ePolTransac.clngProposalQuery) ||
                   (mstrTransaction==ePolTransac.clngReprint)){
                    tcdLedgerDate.value = ""
                    tcdLedgerDate.disabled = true
                    btn_tcdLedgerDate.disabled = true
                }
            }
            break;
//+ Modificación normal de pólizas            
        case "12":     
//+ Modificación temporal de pólizas        
        case "13":     
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                cbeSellchannel.disabled = true            
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
				cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false

            }
            break;
//+ cotización modificación de póliza         
        case "24":                    
//+ propuesta modificación de certificado
        case "25":            
//+ Propuesta Modificación de póliza            
        case "26":
//+ Propuesta renovacion de certificado                    
        case "27":                       
//+ Propuesta Modificación de póliza                     
        case "28":                                                        
//+ Propuesta Modificación de certificado                     
        case "29":            
//+ Consulta de Certificado            
        case "9":     
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                tcdLedgerDate.disabled = true
                btn_tcdLedgerDate.disabled = true
                tcdLedgerDate.value = ""
                tcnCertificat.disabled = false
            
// Se agrega control de campos segun hoja 17            
                cbeSellchannel.disabled = true                        
                cbeOfficeAgen.disabled = true
                btncbeOfficeAgen.disabled = true
                cbeAgency.disabled = true
                btncbeAgency.disabled = true
                cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
                }
                break;
//+         
        case "30":            
        case "31":                            
            with(document.forms["CA001"]){
                cbeOffice.disabled = true
                cbeOffice.value = ""
                optBussines[0].disabled = true
                optBussines[1].disabled = true
                optBussines[2].disabled = true
                optType[0].disabled = true
                optType[1].disabled = true
                optType[2].disabled = true
                tcdLedgerDate.disabled = true
                btn_tcdLedgerDate.disabled = true
                tcdLedgerDate.value = ""
                  tcnCertificat.disabled = false
            
				cbeOffice.disabled = false
                cbeSellchannel.disabled = false                        
                cbeOfficeAgen.disabled = false
                btncbeOfficeAgen.disabled = false
                cbeAgency.disabled = false
                btncbeAgency.disabled = false
                cbeBranch.disabled = false
				valProduct.disabled = false
				btnvalProduct.disabled = false
                break;                
            }
            break;
//+ Modificación normal de certificado            
        case "14":     
//+ Modificación temporal de certificado        
        case "15":     
                with(document.forms["CA001"]){
                    cbeOffice.disabled = true
                    cbeOffice.value = ""
                    optBussines[0].disabled = true
                    optBussines[1].disabled = true
                    optBussines[2].disabled = true
                    optType[0].disabled = true
                    optType[1].disabled = true
                    optType[2].disabled = true
                    tcnCertificat.disabled = false
            
                    cbeSellchannel.disabled = true
                    cbeOfficeAgen.disabled = true
                    btncbeOfficeAgen.disabled = true
                    cbeAgency.disabled = true
                    btncbeAgency.disabled = true
					cbeBranch.disabled = false
					valProduct.disabled = false
					btnvalProduct.disabled = false

               }
    }    
    
//+ Se muestra el frame Fecha de vencimiento sólo en caso de modificaciones temporales
    if((mstrTransaction==ePolTransac.clngTempPolicyAmendment) ||
       (mstrTransaction==ePolTransac.clngTempCertifAmendment)){
        ShowDiv('divExpireDate', 'show')
    }
    else{
        ShowDiv('divExpireDate', 'hide')
    }
    
//+ Se muestra el frame "Modificaciones" sólo en caso de modificaciones a la Propuesta/Cotización/Póliza/Certificado
    if(mstrTransaction==ePolTransac.clngPolicyAmendment ||
       mstrTransaction==ePolTransac.clngTempPolicyAmendment ||
       mstrTransaction==ePolTransac.clngCertifAmendment ||
       mstrTransaction==ePolTransac.clngTempCertifAmendment ||
       mstrTransaction==ePolTransac.clngPolicyQuotAmendent ||
       mstrTransaction==ePolTransac.clngCertifQuotAmendent ||
       mstrTransaction==ePolTransac.clngPolicyPropAmendent ||
       mstrTransaction==ePolTransac.clngCertifPropAmendent ||
       mstrTransaction==ePolTransac.clngQuotAmendentQuery ||
       mstrTransaction==ePolTransac.clngPropAmendentQuery ||
       mstrTransaction==ePolTransac.clngQuotAmendConvertion ||
       mstrTransaction==ePolTransac.clngPropAmendConvertion ||
       mstrTransaction==ePolTransac.clngQuotPropAmendentConvertion){
        ShowDiv('divType_amend', 'show')
    }
    else{
        ShowDiv('divType_amend', 'hide')
    }

//+ El campo Cotización/Propuesta se habilita sólo si se trata de cotizaciones/propuestas de modificación/renovación
    if(mstrTransaction==ePolTransac.clngPolicyQuotAmendent ||
       mstrTransaction==ePolTransac.clngCertifQuotAmendent ||
       mstrTransaction==ePolTransac.clngPolicyPropAmendent ||
       mstrTransaction==ePolTransac.clngCertifPropAmendent ||
       mstrTransaction==ePolTransac.clngPolicyQuotRenewal ||
       mstrTransaction==ePolTransac.clngCertifQuotRenewal ||
       mstrTransaction==ePolTransac.clngPolicyPropRenewal ||
       mstrTransaction==ePolTransac.clngCertifPropRenewal || 
       mstrTransaction==ePolTransac.clngQuotAmendConvertion || 
       mstrTransaction==ePolTransac.clngPropAmendConvertion || 
       mstrTransaction==ePolTransac.clngQuotRenewalConvertion || 
       mstrTransaction==ePolTransac.clngPropRenewalConvertion || 
       mstrTransaction==ePolTransac.clngQuotPropAmendentConvertion || 
       mstrTransaction==ePolTransac.clngQuotPropRenewalConvertion || 
       mstrTransaction==ePolTransac.clngQuotAmendentQuery || 
       mstrTransaction==ePolTransac.clngPropAmendentQuery || 
       mstrTransaction==ePolTransac.clngQuotRenewalQuery || 
       mstrTransaction==ePolTransac.clngPropRenewalQuery ||
       mstrTransaction=="43" ||
       mstrTransaction=="44") {
        document.forms["CA001"].elements["tcnQuotProp"].disabled = true
    }
    else{
        with(document.forms["CA001"]){
            tcnQuotProp.disabled = true
            tcnQuotProp.value = ""
        }
    }
    
    switch(mstrCertype){
        case "1":
            if(mstrTransaction==ePolTransac.clngPropQuotConvertion){
                if(mstrCompanyType==eCompanyType.cstrBrokerOrBrokerageFirm){
                    with(document.forms["CA001"]){
                        //valInsuranceCompany.disabled = false
                        //valOriginalOffice.disabled = false
                    }
                }
            }
            break;
        case "2":
            if(mstrTransaction==ePolTransac.clngPolicyIssue){
                if(mstrCompanyType==eCompanyType.cstrBrokerOrBrokerageFirm){
                    with(document.forms["CA001"]){
                        //valInsuranceCompany.disabled = false
                        //valOriginalOffice.disabled = false
                        //tctOriginalPolicy.disabled = false
                    }
                }
            }
            break;
        case "3":
            if(mstrTransaction==ePolTransac.clngPolicyQuotation){
                if(mstrCompanyType==eCompanyType.cstrBrokerOrBrokerageFirm){
                    with(document.forms["CA001"]){
                        //valInsuranceCompany.disabled = false
                        //valOriginalOffice.disabled = false
                    }
                }
            }
            else{
                if(mstrTransaction==ePolTransac.clngQuotationConvertion){
                    if(mstrCompanyType==eCompanyType.cstrBrokerOrBrokerageFirm){
                        //document.forms["CA001"].elements["tctOriginalPolicy"].disabled = false
                    }
                }
            }
    }
if (mstrTransaction != 4)    
    insInitialAgency(1);
}

//% insStateControls: Habilita/Deshabilita los controles de la página
//--------------------------------------------------------------------------------------------
function insStateControls(lblnEnabled, lblnClear){
//--------------------------------------------------------------------------------------------

    if(lblnEnabled){
        document.forms["CA001"].elements["cbeTransactio"].focus()
    }
//+Se blanquean los campos
    if(lblnClear){  
        if((mstrTransaction=="2") ||
           (mstrTransaction=="5") ||
           (mstrTransaction=="7")){ 
            if(mblnCleanField){  
                insInitialFields()
                mblnCleanField = false
            }  
            else {  
                document.forms["CA001"].elements["tcnCertificat"].value = 0
            } 
        }  
        else{ 
            if ((mstrTransaction=="3") &&
                (nTransaction_ori!=''))  {
               document.forms["CA001"].elements["cbeTransactio"].value = "1";
               document.forms["CA001"].elements["tcnCertificat"].value = 0;
            }
            else{    
                if(((sPolitype==2)) &&
                   ((mstrTransaction=="1") ||
                    (mstrTransaction=="18") || 
                    (mstrTransaction=="3"))){ 
    //+ Si se trata de una póliza colectiva, la última transacción ha 
    //+ sido emisión, recuperación o reemisiòn                          
    //+ y la póliza está completa, se coloca por defecto emisión de certificado
                    mstrTransaction = "2"
                    document.forms["CA001"].elements["tcnCertificat"].value = 0
                }         
                else{  
                    if(((sPolitype==2)) &&
                    (mstrTransaction=="4")){ 
                    document.forms["CA001"].elements["cbeTransactio"].value = "5"
                    mstrTransaction = "2"
                    document.forms["CA001"].elements["tcnCertificat"].value = 0
                    }  
                    else{
                        if(((sPolitype==2)) &&
                           (mstrTransaction=="6")){ 
                            document.forms["CA001"].elements["cbeTransactio"].value = "7"
                            document.forms["CA001"].elements["tcnCertificat"].value = 0
                        } 
                        else{ 
                            if (document.forms["CA001"].elements["cbeTransactio"].value!="2"){
                                document.forms["CA001"].elements["cbeTransactio"].value = "1"
                                mstrCertype = "2"
                                insInitialFields()
                            }
                        }
                    }
                }
            }
        }
    }
    
    with(document.forms["CA001"]){
        if((cbeTransactio.value==ePolTransac.clngPolicyIssue) ||
           (cbeTransactio.value==ePolTransac.clngPolicyQuotation) ||
           (cbeTransactio.value==ePolTransac.clngPolicyProposal)){
            optType[0].disabled = false
            optType[1].disabled = false
            optType[2].disabled = false
        }
    }
    mstrTransaction = document.forms["CA001"].elements["cbeTransactio"].value 

}
