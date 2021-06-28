<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "ms002_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("MS002", "MS002_K.aspx", 1, "")%>
 <SCRIPT>
function insStateZone()
{
	with (self.document.forms[0])
	{ 
	tctCodispl.disabled = false
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" ID="FORM" NAME="frmMesWin" ACTION="valmantsys.aspx?mode=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=15%><LABEL ID=100035><%= GetLocalResourceObject("tctCodisplCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctCodispl", 8, "",  , "",  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>




