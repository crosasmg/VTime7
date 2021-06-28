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

//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd) {
//-------------------------------------------------------------------------------------------

    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            if (typeof(cbeOffice)!='undefined'){
                if (cbeOffice.value != 0){
                    if (typeof(cbeOfficeAgen)!='undefined'){
                        cbeOfficeAgen.disabled = false;
                        btncbeOfficeAgen.disabled = false;

                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeOfficeAgen.Parameters.Param2.sValue = 0;
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
                    }
                }
                else{
                    if(typeof(cbeOfficeAgen)!='undefined'){
                        cbeOfficeAgen.disabled = false;
                        btncbeOfficeAgen.disabled = false;
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeOfficeAgen.Parameters.Param2.sValue = 0;
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
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
