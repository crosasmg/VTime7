<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    


    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
    
//% insCancel: Controla la acción cancelar de la página
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return (true);
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
    with (self.document.forms[0])
    {
        cbeBranchType.disabled = false
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MDP001_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<!--<FORM METHOD="post" ID="FORM" NAME="frmWinsequen" ACTION="valMantProduct.aspx?mode=1">-->
<FORM METHOD="post" ID="FORM" NAME="frmWinsequen" ACTION="valMantProduct.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=101931><%= GetLocalResourceObject("cbeBranchTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchType", "table37", eFunctions.Values.eValuesType.clngComboType, Session("sBrancht"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchTypeToolTip"))%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





