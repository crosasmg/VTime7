<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mclsGeneralForm As eGeneralForm.LedCompSel


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mclsGeneralForm = New eGeneralForm.LedCompSel

mobjValues.sCodisplPage = "cp099"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%=mobjValues.StyleSheet()%>
	<SCRIPT>var nMainAction=0</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="post" ID="FORM" NAME="CP099">
<%
With Response
	.Write(mobjValues.ShowWindowsName("CP099"))
	.Write(mobjValues.WindowsTitle("CP099"))
	.Write("<BR>")
	.Write(mclsGeneralForm.LoadLedCompInfo("LedCompan", Request.QueryString.Item("OnChange")))
End With
%>
	</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
mclsGeneralForm = Nothing
%>




