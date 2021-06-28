<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "op090_k"
%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">
//-------------------------------------------------------------------------------------------
//% LockControl: Bloquea el combo de tipo de negocio
//-------------------------------------------------------------------------------------------
function LockControl(nTypeAccount){

	if (nTypeAccount == 2 ||
		nTypeAccount == 3 ||
		nTypeAccount == 8)
		{
		self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = false;
		}
	else
		{
		self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = true;
		}
}
	
function insStateZone(){
	self.document.forms[0].cbeTypeAccount.disabled = false
	self.document.forms[0].valClient.disabled = false
	self.document.btnvalClient.disabled = false;
	self.document.forms[0].cbeCurrency.disabled = false		
	self.document.btncbeCurrency.disabled = false;
        }
function insCancel(){
	return true;
}   
function insFinish(){
    return true;
}
	
</SCRIPT>

	<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP090"))
	.Write(mobjMenu.MakeMenu("OP090", "OP090_k.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>    
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmCreaCurrAcc" ACTION="ValCashBank.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8740><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTypeAccount", "Table400", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "LockControl(this.value)", True,  , GetLocalResourceObject("cbeTypeAccountToolTip"))%></TD>
            <TD><LABEL ID=8734><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.BlankPosition = False
	Response.Write(.PossiblesValues("cbeBussiType", "Table20", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBussiTypeToolTip"), eFunctions.Values.eTypeCode.eString))
End With
%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8735><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
			<TD COLSPAN=3><%=mobjValues.ClientControl("valClient", "",  , GetLocalResourceObject("valClientToolTip"),  , True, "lblCliename", False)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8736><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD COLSPAN=3><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>            
    </TABLE>
<%
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




