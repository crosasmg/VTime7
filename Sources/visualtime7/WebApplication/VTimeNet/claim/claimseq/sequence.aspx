<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
'~End Body Block VisualTimer Utility

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsClaim_win As eClaim.Claim_win

'+ Objeto para el manejo de los datos de la póliza
Dim mclsPolicy As ePolicy.Policy


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sequence")
mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mclsSequence.sSessionID = Session.SessionID
mclsSequence.nUsercode = Session("nUsercode")
mclsClaim_win = New eClaim.Claim_win
mclsPolicy = New ePolicy.Policy
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft FrontPage 4.0">
	<META NAME="ProgId" CONTENT="FrontPage.Editor.Document">
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
'+ Se arma la secuencia
If mclsPolicy.Find("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy"))) Then
	Response.Write(mclsClaim_win.LoadTabs(CStr(Session("nTransaction")), CStr(Session("nClaim")), CStr(Session("sBrancht")), mclsPolicy.sBussityp, CStr(Session("sSche_code")), CStr(Session("nUsercode")), CBool(Session("bPolicyVigency"))))
	If Request.QueryString("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString("nOpener") & "')</SCRIPT>")
	End If
End If

'UPGRADE_NOTE: Object mclsSequence may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSequence = Nothing
'UPGRADE_NOTE: Object mclsClaim_win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClaim_win = Nothing
'UPGRADE_NOTE: Object mclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsPolicy = Nothing
%>

</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sequence")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




