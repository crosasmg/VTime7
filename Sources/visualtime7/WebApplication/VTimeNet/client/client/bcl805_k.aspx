<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
Response.Buffer = True
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
		
//%insStateZone: Permite habilitar/deshabilitar los campos de la ventana
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("BCL805", "BCL805_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDirPayBank" ACTION="valClient.aspx?mode=1">
	<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>            
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




