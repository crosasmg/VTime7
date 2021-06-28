<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "opl002_k"
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
    try {
		with (self.document.forms[0]){
			valAccCash.disabled = false;
			btnvalAccCash.disabled = valAccCash.disabled;
			tctDepositNum.disabled = false;
		}
	} catch(error){}
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   

//% insCancel: Ejecuta rutinas necesarias en el momento de Finalizar la página
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>

<META http-equiv="Content-Language" content="es">
    <%mobjMenu = New eFunctions.Menues
        Response.Write(mobjMenu.MakeMenu("OPL002", "OPL002_K.aspx", 1, ""))
        Response.Write(mobjValues.WindowsTitle("OPL002", Request.QueryString.Item("sWindowDescript")))
mobjMenu = Nothing
%>
    <BR>
</HEAD>

<BODY  ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmChequesReport" ACTION="ValCashBankRep.aspx?X=1">
<BR>
<%Response.Write(mobjValues.ShowWindowsName("OPL002", Request.QueryString.Item("sWindowDescript")))%>
<BR>
   <TABLE WIDTH="100%">
   <TR>
		<TD><LABEL><%= GetLocalResourceObject("valAccCashCaption") %></LABEL></TD>
		<TD><%=mobjValues.PossiblesValues("valAccCash", "tabBank_acc_com", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valAccCashToolTip"))%></TD>
        <TD><LABEL><%= GetLocalResourceObject("tctDepositNumCaption") %></LABEL></TD>
		<TD><%=mobjValues.TextControl("tctDepositNum", 12, Request.QueryString.Item("sVoucherNumber"),  , GetLocalResourceObject("tctDepositNumToolTip"),  ,  ,  ,  , True)%></TD>
   </TR>

   </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>




