var cbeCober = document.forms[0].elements['P_NUM_PLAN'];
var objParams = new Object;

objParams.Param1 = creObjParam('nbranch', '2', '1', '3', '0', '0', '5')
objParams.Param2 = creObjParam('nproduct', '2', '1', '3', '0', '0', '5')
objParams.nCount = 2;

cbeCober.Parameters = objParams;

var objRetParams = new Object;
objRetParams.nCount = 0

cbeCober.RParameters = objRetParams;