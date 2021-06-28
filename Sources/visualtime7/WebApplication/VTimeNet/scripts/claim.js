//-Se definen las constantes globales para el manejo de las opciones de los siniestros

function eClaimTransac(){
     this.clngClaimIssue = "1"            //'Declaración de Siniestro
     this.clngRecovery = "2"              //'Recobro de siniestro
     this.clngApproval = "3"              //'Aprobación
     this.clngClaimAmendment = "4"        //'Modificar siniestro
     this.clngClaimQuery = "5"            //'Consultar Siniestro
     this.clngClaimRecovery = "6"         //'Recuperar siniestro
     this.clngClaimCancellation = "7"     //'Anular Siniestro
     this.clngClaimRever = "8"            //'Reverso  de Siniestro
     this.clngClaimPayme = "9"            //'Registro de Pago
     this.clngPaymeQuery = "10"           //'Consulta de Pago
     this.clngClaimRelease = "11"         //'Finiquito
     this.clngRequeDoc = "12"             //'Recaudos
     this.clngServiceProf = "13"          //'Servicios Profesionales
     this.clngLetterReq = "14"            //'Carta Aval
     this.clngClaimRejection = "15"       //'Rechazo de Siniestros
     this.clngClaimReopening = "16"       //'Reapertura de Siniestros
     this.clngCaratula = "17"             //'Desistimiento de Siniestros
}

//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd,nTransacDepend){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            if (typeof(cbeOffice)!='undefined'){
                if (cbeOffice.value != 0){
                    if (typeof(cbeOfficeAgen)!='undefined'){
                        if (nTransacDepend == 1){
                            if (cbeTransactio.value == 1){
                                cbeOfficeAgen.disabled = false;
                                btncbeOfficeAgen.disabled = false;
                                cbeAgency.disabled = false;
                                btncbeAgency.disabled = false;
                            }
                        }
                        else{
                             cbeOfficeAgen.disabled = false;
                             btncbeOfficeAgen.disabled = false;
                             cbeAgency.disabled = false;
                             btncbeAgency.disabled = false;                            
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
                          if (nTransacDepend == 1) {
                              if (cbeTransactio.value == 1){                                                        
                                  cbeOfficeAgen.disabled = false;
                                  btncbeOfficeAgen.disabled = false;
                                  cbeAgency.disabled = false;
                                  btncbeAgency.disabled = false;                            
                              }
                              else{
                                   cbeOfficeAgen.disabled = false;
                                   btncbeOfficeAgen.disabled = false;
                                   cbeAgency.disabled = false;
                                   btncbeAgency.disabled = false;                              
                              }
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
}
    
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend(){
//-------------------------------------------------------------------------------------
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeAgency.value="";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
    UpdateDiv('cbeAgencyDesc','');
}

//% BlankAgency: Blanquea en campo Agencia si y sólo si el valor del
//%                 campo Oficina cambia
//-------------------------------------------------------------------------------------
function BlankAgencyDepend(){
//-------------------------------------------------------------------------------------
    with(document.forms[0]){
        cbeAgency.value="";
    }
    UpdateDiv('cbeAgencyDesc','');
}
    