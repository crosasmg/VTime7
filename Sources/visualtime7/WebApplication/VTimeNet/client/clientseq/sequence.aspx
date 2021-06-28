<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
Dim lobjClient As eClient.ClientWin


</script>
<%Response.Expires = 0

mclsSequence = New eFunctions.Sequence
%>
<HTML>
<HEAD>
    <TITLE>Información general</TITLE>
    <META HTTP-EQUIV="Content-Language" CONTENT="es">
    <BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+Si la acción pasada como parámetro posee algún valor, se carga la secuencia del cliente seleccionado
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	lobjClient = New eClient.ClientWin
	
	Response.Write(lobjClient.insLoadTabs(Session("sClient"), CInt(Request.QueryString.Item("nAction")), Session("nUsercode")))
	lobjClient = Nothing
	If Request.QueryString.Item("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "');</SCRIPT>")
	End If
Else
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	
	%>      <SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> <%	
End If
Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction =" & Request.QueryString.Item("nAction") & "</SCRIPT>")
If CDbl(Request.QueryString.Item("nAction")) = 401 Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
End If
%>
</BODY>
</HTML>




