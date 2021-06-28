<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "op092_k"
%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 27/07/04 9:50 $|$$Author: Nvaplat28 $"

//% LockControl: Bloquea el combo de tipo de negocio
//-------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------
function LockControl(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].cbeTypeAccount.value == 2 ||
		self.document.forms[0].cbeTypeAccount.value == 3 ||
		self.document.forms[0].cbeTypeAccount.value == 8)
		{
		//self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = false;
		}
	else
		{
		//self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = true;
		}
}

//% ShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function ShowChangeCurrency(objField){
//-------------------------------------------------------------------------------------------
    var objCurrParams
    var objIntermedia  

    objCurrParams = self.document.forms[0].cbeCurrency.Parameters 
    objCurrParams.Param1.sValue = self.document.forms[0].cbeTypeAccount.value;
    objCurrParams.Param2.sValue = self.document.forms[0].cbeBussiType.value;
    objCurrParams.Param4.sValue = self.document.forms[0].valClient.value;
    
    objIntermedia = self.document.forms[0].valIntermedia.Parameters 
    
    objIntermedia.Param1.sValue = self.document.forms[0].cbeTypeAccount.value;
    objIntermedia.Param2.sValue = self.document.forms[0].valClient.value;    
    
    self.document.forms[0].cbeCurrency.disabled = false;
    self.document.forms[0].btncbeCurrency.disabled = false;
    self.document.forms[0].cbeCurrency.value = '';
    
    UpdateDiv('cbeCurrencyDesc','');
    
	if (self.document.forms[0].valClient.value != '' &&
		self.document.forms[0].cbeTypeAccount.value != 0){
	    insDefValues('BussiType',
					 'nTypeAccount=' + self.document.forms[0].cbeTypeAccount.value + 
				      '&sBussiType=' + self.document.forms[0].cbeBussiType.value + 
				      '&sClient=' + self.document.forms[0].valClient.value + 
					  '&sZone=fraHeader&nCurrency=' + self.document.forms[0].cbeCurrency.value +
					  '&nAction=' + top.fraSequence.plngMainAction + 
					  '&dOperDate=' + self.document.forms[0].gmdEffecdate.value, 
					 '/VTimeNet/CashBank/CashBank', 'showdefvalues');
        }

	insDefValues('Intermed', 'nTypeAccount=' + self.document.forms[0].cbeTypeAccount.value + '&sClient=' + self.document.forms[0].valClient.value + '&nIntermed=' + self.document.forms[0].valIntermedia.value, '/VTimeNet/CashBank/CashBank', 'showdefvalues');
	
}	

//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		gmdEffecdate.disabled = false
		btn_gmdEffecdate.disabled = false
		cbeTypeAccount.disabled = false
		cbeCurrency.disabled = false	
		valClient.disabled = false
		btnvalClient.disabled = false;
		gmnTransact.value = 0
	}
	
	if (top.fraSequence.plngMainAction== 301 ||
	    top.fraSequence.plngMainAction== 305){
		with (self.document.forms[0]){
			cbeTypeAccount.value = 0;
			cbeCurrency.value = 0;
			valClient.value = '';
            valClient_Digit.value = '';			
			//cbeBussiType.value = 0;
			UpdateDiv('lblCliename','')

		}
	}
	
	if (top.fraSequence.plngMainAction== 401 ||
		top.fraSequence.plngMainAction== 302 ||
		top.fraSequence.plngMainAction== 303 ||
		top.fraSequence.plngMainAction== 305)
		self.document.forms[0].gmnTransact.disabled = false
	else
		self.document.forms[0].gmnTransact.disabled = true
}
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true;
}   

//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
    return true;
}

//----------------------------------------------------------------------------------------------------------------
function ShowMovementNumber()
//----------------------------------------------------------------------------------------------------------------

{
	if(top.fraSequence.plngMainAction==301)
	    insDefValues('MovementNumber',
					 'nTypeAccount=' + self.document.forms[0].cbeTypeAccount.value + '&sBussiType=' + self.document.forms[0].cbeBussiType.value+ "&sClient=" + self.document.forms[0].valClient.value + 
					"&nCurrency=" + self.document.forms[0].cbeCurrency.value + '&dOperDate=' + self.document.forms[0].gmdEffecdate.value,
					'/VTimeNet/CashBank/CashBank');

}
//----------------------------------------------------------------------------------------------------------------
function ShowChangeMove()
//----------------------------------------------------------------------------------------------------------------
{
	if(top.fraSequence.plngMainAction==301)
		ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=MovAcc" +  "&nTypeAcco=" + self.document.forms[0].cbeTypeAccount.value + '&sType_acc=' + self.document.forms[0].cbeBussiType.value+ "&sClient=" + self.document.forms[0].valClient.value + 
		          "&nCurrency=" + self.document.forms[0].cbeCurrency.value + "&dOperdate=" + self.document.forms[0].gmdEffecdate.value ,"ShowDefValuesCashBank", 1, 1,"no","no",2000,2000);	

}

</SCRIPT>

    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP092_K"))
	.Write(mobjMenu.MakeMenu("OP092_K", "OP092_K.aspx", 1, ""))
	.Write("<BR><BR>")
	.Write(mobjValues.ShowWindowsName("OP092_k"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCurrenAccounMove" ACTION="ValCashBank.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
			<TD><LABEL ID=8782><%= GetLocalResourceObject("gmdEffecdateCaption") %></LABEL></TD>   
			<TD><%=mobjValues.DateControl("gmdEffecdate", CStr(Today),  , GetLocalResourceObject("gmdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=8786><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeTypeAccount", "table400", eFunctions.Values.eValuesType.clngComboType, Session("nTypeAccount"),  ,  ,  ,  ,  , "LockControl(this.value);ShowChangeCurrency()", True,  , GetLocalResourceObject("cbeTypeAccountToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8779><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
            <TD COLSPAN=3><% =mobjValues.PossiblesValues("cbeBussiType", "Table20", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "ShowChangeCurrency()", True,  , GetLocalResourceObject("cbeBussiTypeToolTip"), eFunctions.Values.eTypeCode.eString)%></TD>
        </TR>
        <TR>
        
   			<TD><LABEL ID=8780><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("valClient", Session("sClient"),  , GetLocalResourceObject("valClientToolTip"), "ShowChangeCurrency();", True, "lblCliename", False)%></TD>
        	<TD><LABEL><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
        	<TD><%mobjValues.Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valIntermedia", "tabintermedia_op092", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valIntermediaToolTip")))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8781><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%
mobjValues.Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_Acc", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TABCURR_CLI_INTER", eFunctions.Values.eValuesType.clngWindowType, Session("nCurrency"), True,  ,  ,  ,  , "ShowChangeMove(this.value);", True,  , GetLocalResourceObject("cbeCurrencyToolTip")))
%></TD>	
			<TD><LABEL ID=19449><%= GetLocalResourceObject("gmnTransactCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("gmnTransact", 5, Session("nTransact"),  , GetLocalResourceObject("gmnTransactToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>
        <TR>
			<TD></TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>	
        </TR>		
    </TABLE>
<%
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




