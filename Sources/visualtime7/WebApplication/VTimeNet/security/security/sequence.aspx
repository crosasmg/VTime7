<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
Dim lobjSecurity As Object


</script>
<%
Response.Expires = -1
mclsSequence = New eFunctions.Sequence
%>
<HTML>
<HEAD>
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
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%

'+ Si la acción pasada como parámetro posee algún valor, se carga la secuencia 
'+ de la transacción del sistema.
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	Select Case Request.QueryString.Item("nOpener")
		Case "SG005_k", "SG005", "SG006", "SG009", "SG016"
			lobjSecurity = New eSecurity.Windows
			Response.Write(lobjSecurity.LoadTabs(Request.QueryString.Item("nAction"), Session("sCodispLog"), Session("sPseudo"), Session("nWindowty"), Session("sSche_code"), Session("nUsercode")))
			
			If Request.QueryString.Item("sGoToNext") <> "NO" Then
				Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
			End If
			
		Case "SG013_k", "SG013", "SG014", "SG003", "SG017", "SG002", "SG100", "SG020", "SG021", "SG855"
			lobjSecurity = New eSecurity.Secur_sche
			Response.Write(lobjSecurity.LoadTabs(Request.QueryString.Item("nAction"), Session("sSche_codeWin"), Session("sSche_code")))
			
			If Request.QueryString.Item("sGoToNext") <> "NO" Then
				Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
			End If
	End Select
Else
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	Response.Write("<SCRIPT>top.fraFolder.document.location = '/VTimeNet/Common/Blank.htm'</SCRIPT>")
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = '" & Request.QueryString.Item("nAction") & "';</SCRIPT>")
If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
End If
mclsSequence = Nothing
lobjSecurity = Nothing
%>
</BODY>
</HTML>





