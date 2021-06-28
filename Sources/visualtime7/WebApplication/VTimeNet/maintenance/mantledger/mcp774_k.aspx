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
mobjValues.sCodisplPage = "MCP774"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        valLed_compan.disabled=false;
        btnvalLed_compan.disabled=false;
        cboTypecode.disabled=false;
    }

}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
   return true
}

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MCP774", "MCP774_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCP774" ACTION="valMantLedger.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>            
            <TD WIDTH="15%"><LABEL ID="0"><%= GetLocalResourceObject("valLed_companCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.PossiblesValues("valLed_compan", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valLed_companToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD WIDTH="10%"><LABEL ID="0"><%= GetLocalResourceObject("cboTypecodeCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.PossiblesValues("cboTypecode", "Table5565", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboTypecodeToolTip"))%></TD>            
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>






