<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'- Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsClient As eClient.Client


</script>
<%Response.Expires = -1

With Server
	mclsSequence = New eFunctions.Sequence
	mclsClient = New eClient.Client
End With
%>
<HTML>
<HEAD>
	
	<META NAME="ProgId" CONTENT="FrontPage.Editor.Document">
	<BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>	
<SCRIPT>
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $" 
</SCRIPT>
	<%Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery=true;")
Else
	Response.Write("var pblnQuery=false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters%>>
<%
'+ Se invoca el método que genera la secuencia de ventanas
Response.Write(mclsClient.LoadTabsDocument(Session("sClient"), Session("bQuery")))

If Request.QueryString.Item("sGoToNext") <> "NO" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsClient = Nothing
%>
</BODY>
</HTML>





