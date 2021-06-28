<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout


'% insShowRequest_num:
'--------------------------------------------------------------------------------------------
Sub insCancel()
	'--------------------------------------------------------------------------------------------
	Dim lclsLettRequest As eLetter.LettRequest
	Dim lclsLettValues As eLetter.LettValues
	
	lclsLettRequest = New eLetter.LettRequest
	lclsLettValues = New eLetter.LettValues
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
		lclsLettRequest.Delete(Session("nLettRequest"))
		lclsLettValues.nLettRequest = Session("nLettRequest")
		lclsLettValues.Delete()
	End If
	
	lclsLettRequest = Nothing
	lclsLettValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("ShowDefValues")
'~End Header Block VisualTimer Utility
%>
<HTML>
<HEAD>
</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
insCancel()
Response.Write("opener.insReloadTop(true, false);window.close()")
Response.Write("</SCRIPT>")
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("ShowDefValues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







