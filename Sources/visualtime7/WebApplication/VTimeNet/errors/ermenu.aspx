<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjErrors As eErrors.Main


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjErrors = New eErrors.Main
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 5/11/03 17:48 $|$$Author: Nvaplat7 $"
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("ERROR"))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM>
	<%mobjErrors.sSche_code = Session("sSche_code")
Response.Write(mobjErrors.Makemenu("ERROR"))
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjErrors = Nothing
%>




