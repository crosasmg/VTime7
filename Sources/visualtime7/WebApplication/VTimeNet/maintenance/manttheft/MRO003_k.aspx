<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">    

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
    Dim mobjMenu As eFunctions.Menues

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO003_K")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO003_K"
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
    function insStateZone() {
//-----------------------------------------------------------------------------
        with (document.forms[0]) {
            tcnTar_Theft.disabled = false;
            tcdEffecDate.disabled = false;
            btn_tcdEffecDate.disabled = false;
        }
    }

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MRO003", "MRO003_k.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MRO003" ACTION="valmanttheft.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8612><%= GetLocalResourceObject("tcnTar_theftCaption")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnTar_Theft", 4, Session("nTar_Theft"), True, GetLocalResourceObject("tcnTar_theftTooltip"), , , , , , , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8613><%= GetLocalResourceObject("tcdEffecdateCaption")%></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", Session("dEffecDate"), True, GetLocalResourceObject("tcdEffecdateTooltip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MRO003_K")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








