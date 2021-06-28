<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim llngAction As Object
Dim mclsSecurity As eSecurity.Windows


</script>
<%Response.Expires = -1

mclsSecurity = New eSecurity.Windows
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG006"

llngAction = Request.QueryString.Item("nMainAction")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SG006"))
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.setZone(2, "SG006", "SG006.aspx"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SG006" ACTION="valSecuritySeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("SG006"))

If CStr(Session("sCodispLog")) = vbNullString Then
	Call mclsSecurity.insReaWindowsPseudo("", Session("sPseudo"))
Else
	Call mclsSecurity.reaWindows(Session("sCodispLog"))
End If

mobjValues.ActionQuery = llngAction = eFunctions.Menues.TypeActions.clngActionQuery
%>
	<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="30%"><LABEL ID=15010><%= GetLocalResourceObject("tcnG_identiCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnG_identi", 4, CStr(mclsSecurity.nG_identi), False, GetLocalResourceObject("tcnG_identiToolTip"),  ,  ,  ,  ,  ,  ,  , 1)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mclsSecurity = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>




