<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<TITLE>Imagen amplificada</TITLE>
	<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
	<IMG NAME="iImage" SRC=''>
	<SCRIPT>
		self.document.images["iImage"].src = opener.mstrImageSrc
	</SCRIPT>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




