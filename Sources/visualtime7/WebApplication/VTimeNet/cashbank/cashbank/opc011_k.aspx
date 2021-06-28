<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "OPC011_K"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//% ShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function ShowChangeCurrency(){
//-------------------------------------------------------------------------------------------
 	if (self.document.forms[0].valClient.value != '' &&
		self.document.forms[0].cbeTypeAccount.value != 0)
		{ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=BussiTypeParam" + "&nTypeAccount=" + self.document.forms[0].cbeTypeAccount.value + "&sBussiType=0" + "&sClient=" + self.document.forms[0].valClient.value, "ShowDefValuesCurrency", 1, 1,"no","no",2000,2000);
		}
}	

function insStateZone(){
	self.document.forms[0].gmdEffecdate.disabled = false
	self.document.forms[0].btn_gmdEffecdate.disabled = false
	self.document.forms[0].cbeTypeAccount.disabled = false
	self.document.forms[0].valClient.disabled = false
	self.document.forms[0].btnvalClient.disabled = false
	self.document.forms[0].cbeCurrency.disabled = false
	self.document.forms[0].btncbeCurrency.disabled = false
	

}
	
function insCancel(){
	return true;
}   
function insFinish(){
    return true;
}
</SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
	<%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OPC011"))
	.Write(mobjMenu.MakeMenu("OPC011", "OPC011_K.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>        
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQPremiumMov" ACTION="valCashBank.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8876><%= GetLocalResourceObject("gmdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("gmdEffecdate", CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)),  , "",  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=8873><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.TypeList = 2
	.List = "2,3,8"
	Response.Write(.PossiblesValues("cbeTypeAccount", "table400", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "ShowChangeCurrency();", True,  , ""))
End With%>
        </TR>
        <TR>
            <TD><LABEL ID=8874><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
            <TD><%=mobjValues.ClientControl("valClient", "",  , "", "ShowChangeCurrency();", True, "lblCliename", True)%></TD>
            	<%=mobjValues.HiddenControl("HsClient", "")%>
            <TD><LABEL ID=8875><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>

<%mobjValues.Parameters.Add("nTyp_acco", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
                <TD><%=mobjValues.PossiblesValues("cbeCurrency", "TabCurr_Cli_Inter", 2, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>


        </TR>
        <TR>
            <TD></TD>        
            <TD><%=mobjValues.DIVControl("lblCliename", False)%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





