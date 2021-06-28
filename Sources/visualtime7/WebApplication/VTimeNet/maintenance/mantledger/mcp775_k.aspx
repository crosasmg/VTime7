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
mobjValues.sCodisplPage = "MCP775"
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        valLed_compan.disabled=false;
        btnvalLed_compan.disabled=false;
        cbeArea_led.disabled=false;
        cbeGroup.disabled=false;
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}


 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MCP775", "MCP775_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCP775" ACTION="valMantLedger.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valLed_companCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valLed_compan", "TabLed_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valLed_companToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeArea_ledCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeArea_led", "Table178", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeArea_ledToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeGroupCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeGroup", "Table641", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeGroupToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>






