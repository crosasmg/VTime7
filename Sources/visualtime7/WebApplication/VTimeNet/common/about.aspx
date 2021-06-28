<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>
    <%=mobjValues.styleSheet()%>
	<SCRIPT>document.title = 'Acerca de "' + document.title + '"'</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME="frmAbout">
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=30%><LABEL ID=40510><%= GetLocalResourceObject("lblCodisplCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblCodispl", 30, Request.QueryString.Item("sCodispl") & Request.QueryString.Item("sComplement"),  , GetLocalResourceObject("lblCodisplToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL ID=40511><%= GetLocalResourceObject("lblCodispCaption") %></LABEL></TD>
	        <TD><%=mobjValues.TextControl("lblCodisp", 30, Request.QueryString.Item("sCodisp") & Request.QueryString.Item("sComplement"),  , GetLocalResourceObject("lblCodispToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL ID=40512><%= GetLocalResourceObject("lblVersionCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblVersion", 3, Request.QueryString.Item("VssVersion"),  , GetLocalResourceObject("lblVersionToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="2" CLASS="HorLine"></TD>
	    </TR>
	    <TR>
	        <TD><%=mobjValues.AnimatedButtonControl("cmdErrors", "/VTimeNet/Images/btnErrorSystemOff.png", GetLocalResourceObject("cmdErrorsToolTip"),  , "ShowPopUp(""/VTimeNet/errors/ermenu.aspx"", ""Errors"", 300, 200)")%></TD>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
	    </TR>
	</TABLE>
</FORM>

</BODY>
</HTML>
<%
'+ Se toma el valor del código lógico de la ventana para utilizarlo posteriormente
'+ en las demás ventanas del módulo de errores
Session("sCodispl_log") = Request.QueryString.Item("sCodispl")
mobjValues = Nothing
%>




