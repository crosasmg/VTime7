<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
'~End Body Block VisualTimer Utility

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsCases_win As eClaim.Cases_win


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sequence")
mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.46
mclsSequence.sSessionID = Session.SessionID
mclsSequence.nUsercode = Session("nUsercode")
mclsCases_win = New eClaim.Cases_win

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="MICROSOFT FRONTPAGE 4.0">
	<META http-equiv="Content-Language" content="es">
    <BASE TARGET="fraFolder">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Sequence.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
	<%Response.Write("<SCRIPT>")
If CBool(Session("bQuery")) Then
	Response.Write("var pblnQuery=true;")
Else
	Response.Write("var pblnQuery=false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+ Se carga la secuencia sólo si el query string tiene valor; es decir fue llamada desde la SI099_K.
If Request.QueryString.ToString <> vbNullString Then
	Response.Write(mclsCases_win.LoadTabs(CStr(Session("nTransaction")), CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), CInt(Session("nBene_type")), CStr(Session("sBrancht")), CStr(Session("sSche_code")), CInt(Session("nUsercode"))))
	
	If Request.QueryString("sGoToNext") = "Yes" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString("nOpener") & "')</SCRIPT>")
	End If
End If
'UPGRADE_NOTE: Object mclsSequence may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSequence = Nothing
'UPGRADE_NOTE: Object mclsCases_win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsCases_win = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.46
Call mobjNetFrameWork.FinishPage("sequence")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




