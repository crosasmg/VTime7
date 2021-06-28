<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
Dim mobjCover As eProduct.Tab_gencov


</script>
<%Response.Expires = -1

mclsSequence = New eFunctions.Sequence
%>
<HTML>
<HEAD>
   <TITLE>Información general</TITLE>
   <META HTTP-EQUIV="Content-Language" CONTENT="es">
   <BASE TARGET="fraFolder">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
<%
With Response
	.Write("<SCRIPT>")
	.Write("var pblnQuery = false")
	.Write("</SCRIPT>")
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	mobjCover = New eProduct.Tab_gencov
	Response.Write(mobjCover.LoadTabs(Request.QueryString.Item("nOpener"), CInt(Request.QueryString.Item("nAction")), Session("sSche_code"), Session("nCover")))
	If Request.QueryString.Item("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	End If
	mobjCover = Nothing
Else
	
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	
	%>      <SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> <%	
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = " & Request.QueryString.Item("nAction") & "</SCRIPT>")

Session("bQuery") = False
If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Session("bQuery") = True
End If
mclsSequence = Nothing
%>
</BODY>
</HTML>




