<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjSecurity As eSecurity.Menu

Dim mobjValues As eFunctions.Values

</script>
<%Response.Expires = -1
mobjSecurity = New eSecurity.Menu
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//% LoadModules: Coloca el mensaje de espera carga el modulo solicitadoRGB(0,128,177)
//-------------------------------------------------------------------------------------------
function LoadModules(sModule){
//-------------------------------------------------------------------------------------------
    top.frames['FraHeader'].UpdateDiv('lblWaitProcess', '<MARQUEE>' + resValues.marqueeMessage + '</MARQUEE>', '');
    top.frames['FraHeader'].document.location = "MenuName.aspx?sModule=" + sModule;
} 
</SCRIPT>
</HEAD>
<BODY id="left_frameii">
<%
Response.Write(mobjSecurity.Modules(Session("sHistory"), vbNullString))
mobjSecurity = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>





