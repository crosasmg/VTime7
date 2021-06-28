<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">

'- Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia

Dim mclsSequence As eFunctions.Sequence

'- Objeto para el manejo de las páginas que forman la secuencia	

Dim mclsLetter_win As eLetter.LettRequestWin
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout


</script>
<%

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("Sequence")
'~End Header Block VisualTimer Utility
mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mclsSequence.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
mclsLetter_win = New eLetter.LettRequestWin
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" content="Microsoft FrontPage 4.0">
	<META NAME="ProgId" content="FrontPage.Editor.Document">
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
	<BASE TARGET="fraFolder">
	<%
Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery = true;")
Else
	Response.Write("var pblnQuery = false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
Response.Write(mclsLetter_win.LoadTabs(Session("nLettRequest"), Request.QueryString.Item("nAction"), Session("sSche_code"), Session("nUsercode"), Session("sClient")))

If Request.QueryString.Item("sGoToNext") <> "NO" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction ='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")

mclsSequence = Nothing
mclsLetter_win = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("Sequence")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







