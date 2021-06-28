<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mstrQuote = """"

mobjValues.sCodisplPage = "dp002_k"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP002", "DP002_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insCancel: Se cancela la página invocada.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Permite habilitar los objetos e imágenes de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++)
         document.images[lintIndex].disabled = false
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP002_k" ACTION="valProduct.aspx?mode=1">
<BR> <BR>
<!--Se define la tabla que contendrá los objetos del encabezado de la página-->
    <TABLE WIDTH="100%">
		<TR>
            <TD WIDTH=10%><LABEL ID=14228><A NAME="Ramo"><%= GetLocalResourceObject("cbeBranchCaption") %></A></LABEL></TD>
            <TD> <%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchToolTip"),  , 1)%></TD>
            <TD WIDTH=12%=> <LABEL ID=14229><A NAME="Fecha"><%= GetLocalResourceObject("tcdDateCaption") %></A></LABEL></TD>
            <TD> <%=mobjValues.DateControl("tcdDate", CStr(Now),  , GetLocalResourceObject("tcdDateToolTip"),  ,  ,  ,  , True, 2)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
Session("DP003_dEffecdate") = vbNullString
Session("DP003_nBrancht") = vbNullString
Session("DP003_nBranch") = vbNullString
Session("DP003_nProduct") = vbNullString
Session("DP003_sLinkSpecial") = vbNullString
%>




