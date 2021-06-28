<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mObjBatchProcess As eSchedule.BatchProcess


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "BTC003_K"
Response.Write(mobjValues.StyleSheet)
mObjBatchProcess = New eSchedule.BatchProcess

'Dim mObjSendFiles
'Set mObjSendFiles = Server.CreateObject("eSendFile.SendFiles")

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    


<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 22/10/04 1:22p $|$$Author: Nvaplat40 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="btc003">
	</FORM>
</BODY>
</HTML>
<%
'Response.Write(mObjBatchProcess.VerifyProcess(Session("nUserCode")))
'Response.Write mObjSendFiles.Send_Mail("jorge.rivero@consorcio.cl","prueba","hola")
Response.Write("<SCRIPT>window.close();</SCRIPT>")

mobjValues = Nothing
mObjBatchProcess = Nothing
%>

<%
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("BTC003_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




