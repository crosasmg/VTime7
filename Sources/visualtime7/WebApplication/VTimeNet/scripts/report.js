//--------------------------------------------------------------------
// $$Workfile: $ 
// $$Author: $ 
// $$Date: $ 
// $$Revision: $ 
//--------------------------------------------------------------------
//% getDigit: Se muestra el dígito verificador de la patente
//-------------------------------------------------------------------------------------------
function getDigit(objRegister, objDigit){
//-------------------------------------------------------------------------------------------
		if(objRegister.value!=''){
			insDefValues('getDigit', 'sDigitName=' + objDigit.name + '&sRegist=' + objRegister.value + '&sLicense_ty=1', '/VTimeNet/reports/repmach', 'showdefvalues');
		}
}

//% disableModul:Se deshabilita el campo modulo para los producto que no son modulares
//-------------------------------------------------------------------------------------------
function disableModul(objBranch, objProduct, objModulec){
//-------------------------------------------------------------------------------------------
		if(objProduct.value!=''){
			insDefValues('disModul', 'nModulecName=' + objModulec.name + '&nbranch=' + objBranch.value + '&nproduct=' + objProduct.value , '/VTimeNet/reports/repmach', 'showdefvalues');
		}
}


//% disableCertifProp:Se deshabilita el campo certificado si la Propuesta no es colectiva
//-------------------------------------------------------------------------------------------
function disableCertifProp(objsCertype, objBranch, objProduct, objPolicy, objCertif){
//-------------------------------------------------------------------------------------------
		if ((objProduct.value!='') && (objPolicy.value!='')) {
			insDefValues('disCertifPro', 'nCertifName=' + objCertif.name + '&scertype=' + objsCertype.value + '&nbranch=' + objBranch.value + '&nproduct=' + objProduct.value + '&npolicy=' + objPolicy.value , '/VTimeNet/reports/repmach', 'showdefvalues');
		}
}

//% disableCertif:Se deshabilita el campo certificado si la poliza no es colectiva
//-------------------------------------------------------------------------------------------
function disableCertif(objBranch, objProduct, objPolicy, objCertif){
//-------------------------------------------------------------------------------------------
		if ((objProduct.value!='') && (objPolicy.value!='')) {
			insDefValues('disCertif', 'nCertifName=' + objCertif.name  + '&nbranch=' + objBranch.value + '&nproduct=' + objProduct.value + '&npolicy=' + objPolicy.value , '/VTimeNet/reports/repmach', 'showdefvalues');
		}
}

//% disableCertifClie:Se deshabilita el campo certificado si la poliza no es colectiva
//-------------------------------------------------------------------------------------------
function disableCertifClie(objBranch, objProduct, objPolicy,  objClient, objCertif){
//-------------------------------------------------------------------------------------------
		if ((objProduct.value!='') && (objPolicy.value!='')) {
			insDefValues('disCertifClie', 'nCertifName=' + objCertif.name + '&sClieName=' +objClient.name + '&nbranch=' + objBranch.value + '&nproduct=' + objProduct.value + '&npolicy=' + objPolicy.value , '/VTimeNet/reports/repmach', 'showdefvalues');
		}
}

//% getClaim: Se muestra el dígito verificador de la patente
//-------------------------------------------------------------------------------------------
function getClaim(objClaim, objOffice, objOfficeAgen, objAgency ){
//-------------------------------------------------------------------------------------------
		if(objClaim.value!=''){
			insDefValues('getClaim', 'nOfficeName=' + objOffice.name + '&nOfficeAgenName=' + objOfficeAgen.name +  '&nAgencyName=' + objAgency.name +'&nClaimName=' + objClaim.name + '&nClaim=' + objClaim.value , '/VTimeNet/reports/repmach', 'showdefvalues');			
		}
}

//-------------------------------------------------------------------------------------
function BlankClaim()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        P_COD_SUCURSAL.value=0;
        P_COD_OFICINA.value="";
        P_COD_AGENCIA.value="";
    }
    UpdateDiv('P_COD_OFICINADesc','');
    UpdateDiv('P_COD_AGENCIADesc','');
    
}