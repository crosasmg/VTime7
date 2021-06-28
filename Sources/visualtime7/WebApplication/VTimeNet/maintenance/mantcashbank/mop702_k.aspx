<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP702"
%>
<HTML>
<HEAD> 
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//--------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------
    document.forms[0].cbeClass_concept.disabled=false
}    
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu("MOP702", "MOP702_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MOP702" ACTION="ValMantCashBank.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">            
        <TR>
            <TD width="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeClass_conceptCaption") %></LABEL></TD>
            <TD width="50%"><%=mobjValues.PossiblesValues("cbeClass_concept", "Table5650", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 30, GetLocalResourceObject("cbeClass_conceptToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
<%
mobjValues = Nothing
%>
</BODY>
</HTML>





