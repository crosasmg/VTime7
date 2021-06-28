<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia

Dim mclsSequence As eFunctions.Sequence
Dim lobjAgent As eAgent.Intermedia


</script>
<%Response.Expires = 0
mclsSequence = New eFunctions.Sequence
%>
<HTML>
<HEAD>
   <TITLE>Información general</TITLE>
   <META http-equiv="Content-Language" content="es">
   <BASE TARGET="fraFolder">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
   <%
With Response
	.Write("<SCRIPT>")
	.Write("var pblnQuery = false")
	.Write("</script>")
End With
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+Si la acción pasada como parámetro posee algún valor, se carga la secuencia del cliente seleccionado
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	lobjAgent = New eAgent.Intermedia
	Response.Write(lobjAgent.LoadTabs(Session("nIntermed"), CInt(Request.QueryString.Item("nAction")), Session("sSche_code"), Session("nUsercode")))
	If Request.QueryString.Item("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	End If
Else
	
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	
	%>      <SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> <%	
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = '" & Request.QueryString.Item("nAction") & "';</SCRIPT>")

If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
End If

%>
</BODY>
</HTML>
<%
mclsSequence = Nothing
lobjAgent = Nothing
%>





