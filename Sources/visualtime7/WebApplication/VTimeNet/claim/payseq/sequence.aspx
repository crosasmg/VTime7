<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
'~End Body Block VisualTimer Utility

Dim mobjClaim As eClaim.T_PayCla


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sequence")
mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
mclsSequence.sSessionID = Session.SessionID
mclsSequence.nUsercode = Session("nUsercode")
%>
<html>
<HEAD>
   <TITLE>Información general</TITLE>
   <META HTTP-EQUIV="Content-Language" CONTENT="es">
   <BASE TARGET="fraFolder">
   <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

   <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

   <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Sequence.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<script type="text/javascript" src="/VTimeNet/Scripts/Sequence.js"></script>
   <%
With Response
	.Write("<script>var pblnQuery = false</script>")
End With
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+Si la acción pasada como parámetro posee algún valor, se carga la secuencia
If Request.QueryString("nAction") <> "" Then
	mobjClaim = New eClaim.T_PayCla
	Response.Write(mobjClaim.LoadTabs(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), Request.QueryString("nAction"), CStr(Session("sSche_code")), CInt(Session("nUsercode")), Request.QueryString("nOpener"), CInt(Session("nPay_Type")), CStr(Session("SI008_Required")), CDate(Session("dEffecdate"))))
	
	If Request.QueryString("sGoToNext") <> "NO" Then
		Response.Write("<script>NextWindows('" & Request.QueryString("nOpener") & "')</script>")
	End If
	
Else
	
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	
	%>      <script>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</script> <%	
End If
Response.Write("<script>top.frames['fraSequence'].plngMainAction = '" & Request.QueryString("nAction") & "';</script>")
If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
End If
'UPGRADE_NOTE: Object mclsSequence may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSequence = Nothing
'UPGRADE_NOTE: Object mobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjClaim = Nothing
%>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
Call mobjNetFrameWork.FinishPage("sequence")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




