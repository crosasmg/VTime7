var cbeContr = document.forms[0].elements['P_CONTRATO'];
var objParams = new Object;

objParams.Param1 = creObjParam('ncompany', '2', '1', '3', '0', '0', '5')
objParams.nCount = 1;

cbeContr.Parameters = objParams;

var objRetParams = new Object;
objRetParams.nCount = 0

cbeContr.RParameters = objRetParams;