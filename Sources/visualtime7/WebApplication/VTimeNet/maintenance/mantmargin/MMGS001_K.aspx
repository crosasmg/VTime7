<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MMGS001"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 27/11/03 17:35 $|$$Author: Nvaplat15 $"

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        cbeInsur_area.disabled=false;
        cbeFactor.disabled=false;
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MMGS001", "MMGS001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), session("sDesMultiCompany"), session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MMGS001" ACTION="valMantMargin.aspx?sMode=1">
    <BR><BR><BR>
    <TABLE WIDTH="70%">
        <TR>
            <%=mobjValues.HiddenControl("cbeInsur_area", session("nInsur_area"))%>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeFactorCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeFactor", "Table5606", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeFactorToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>




