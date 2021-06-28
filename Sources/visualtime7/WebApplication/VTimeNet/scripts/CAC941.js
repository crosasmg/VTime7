var cbeCober = document.forms[0].elements['P_COD_COBER'];
var objParams = new Object;

objParams.Param1 = creObjParam('nArea', '2', '1', '3', '0', '0', '5')
objParams.nCount = 1;

cbeCober.Parameters = objParams;

var objRetParams = new Object;
objRetParams.nCount = 0

cbeCober.RParameters = objRetParams;
