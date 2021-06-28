<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjGeneral As eGeneral.Associate


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGeneral = New eGeneral.Associate
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME=Associated>
<%With Response
	.Write(mobjValues.ShowWindowsName("GE777"))
	.Write("<BR>")
	.Write(mobjGeneral.Makemenu(CInt(Request.QueryString.Item("nKeynum")), Request.QueryString.Item("sStringCa")))
End With
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGeneral = Nothing
%>




