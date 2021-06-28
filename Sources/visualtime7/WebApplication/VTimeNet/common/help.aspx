<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim lstrHelpPath As String
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
Response.Buffer = True
mobjValues = New eFunctions.Values
lstrHelpPath = mobjValues.GetHelpPath(Request.QueryString.Item("sCodispl"))
If lstrHelpPath <> vbNullString Then
	Response.Redirect((lstrHelpPath))
End If
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <TITLE>Ayuda</TITLE>
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
<%=Request.QueryString.Item("sCodispl")%>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





