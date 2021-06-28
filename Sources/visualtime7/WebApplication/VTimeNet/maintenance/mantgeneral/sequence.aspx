<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia

Dim mclsSequence As eFunctions.Sequence

'- Objeto para el manejo de las páginas que forman la secuencia	

Dim mclsOptInstallWin As eGeneral.OptionsInstallation


</script>
<%Response.Expires = 0

mclsSequence = New eFunctions.Sequence
mclsOptInstallWin = New eGeneral.OptionsInstallation
%>
<HTML>
<HEAD>
	
	<META NAME="ProgId" content="FrontPage.Editor.Document">


<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
	<BASE TARGET="fraFolder">
	<%Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery = true;")
Else
	Response.Write("var pblnQuery = false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
Response.Write(mclsOptInstallWin.LoadTabs(CInt(Request.QueryString.Item("nAction")), Session("sSche_code")))

If Request.QueryString.Item("sGoToNext") <> "NO" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsOptInstallWin = Nothing
%>
</BODY>
</HTML>




