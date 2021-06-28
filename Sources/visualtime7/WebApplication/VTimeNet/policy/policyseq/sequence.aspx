<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence
Dim mobjValues As eFunctions.Values


'% insSequence. Este procedimiento se encarga de mostrar la sequencia de ventanas de poliza
'----------------------------------------------------------------------------------------
Private Sub insSequence()
	'----------------------------------------------------------------------------------------
	Dim lclsPolicy_Win As ePolicy.Policy_Win
	
	'+ Se arma la secuencia
	lclsPolicy_Win = New ePolicy.Policy_Win
	Response.Write(lclsPolicy_Win.LoadTabs(Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Session("sPolitype"), Session("sBussityp"), Session("sTypeCompanyUser"), Request.QueryString.Item("nOpener"), Session("sBrancht"), Session("sSche_code"), Session("nType_amend")))
	If Request.QueryString.Item("sGoToNext") = "Yes" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</" & "Script>")
	End If
	lclsPolicy_Win = Nothing
	mclsSequence = Nothing
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("Sequence")

mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mclsSequence.sSessionID = Session.SessionID
mclsSequence.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "Sequence"
%>
<HTML>
<HEAD>
    <META http-equiv="Content-Language" content="es">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>


<BASE TARGET="fraFolder">    
<%
Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery=true;")
Else
	Response.Write("var pblnQuery=false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%Call insSequence()%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("Sequence")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




