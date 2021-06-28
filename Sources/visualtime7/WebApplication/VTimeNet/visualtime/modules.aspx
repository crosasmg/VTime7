<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values

Dim mobjSecurity As eSecurity.Menu


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("modules")
mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "modules"
mobjSecurity = New eSecurity.Menu
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <BASE TARGET="FraHeader">
</HEAD>
<% 
    Response.Write("<BODY id=""left_frame""><img src=""/VTimeNet/images/Logos/CompanyLogo.gif"" hspace=""3"" vspace=""5"">")
    If CStr(Session("SessionId")) <> vbNullString Then
    %>
    <BR><BR><BR><BR><BR><BR>
	<IFRAME NAME="fraGrid" SRC="/VTimeNet/VisualTime/Menu.aspx" WIDTH="105%" HEIGHT="75%" SCROLLING=AUTO FRAMEBORDER="0">
	</IFRAME>
    <%
    End If

Response.Write("</BODY></HTML>")

mobjSecurity = Nothing
mobjValues = Nothing
Call mobjNetFrameWork.FinishPage("modules")
mobjNetFrameWork = Nothing
%>





