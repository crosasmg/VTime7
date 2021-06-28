<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "dp039_k"
%>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP039", "DP039_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"

//%insCancel: Permite cancelar la página invocada.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: Permite habilitar los objetos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP039_k" ACTION="valProduct.aspx?mode=1">
<BR> <BR>
<!--Se define la tabla que contendrá los objetos de la ventana de duplicado -->
    <TABLE WIDTH="100%">
		<TR>
            <TD WIDTH=10%><LABEL ID=14244><A NAME="Moneda"><%= GetLocalResourceObject("cbeCurrencyCaption") %></A></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(41220, "optTypCov", GetLocalResourceObject("optTypCov_CStr1Caption"), eFunctions.Values.vbUnChecked, CStr(1),  , True,  , GetLocalResourceObject("optTypCov_CStr1ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(41221, "optTypCov", GetLocalResourceObject("optTypCov_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2),  , True,  , GetLocalResourceObject("optTypCov_CStr2ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(41222, "optTypCov", GetLocalResourceObject("optTypCov_CStr3Caption"), eFunctions.Values.vbChecked, CStr(3),  , True,  , GetLocalResourceObject("optTypCov_CStr3ToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





