<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mobjUserValidate As eSecurity.UserValidate


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjUserValidate = New eSecurity.UserValidate

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/GenFunctions.js"></SCRIPT>


	<%=mobjValues.WindowsTitle("GE012")%>  
    <%=mobjValues.styleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName("GE012")%>
<FORM NAME="frmAboutQuote">
<%Call mobjUserValidate.GetVersionInfo(Session("sSche_code"))%>
	<TABLE WIDTH=100%>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("lblLastDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblLastDate", 12, CStr(mobjUserValidate.dLastdate),  , GetLocalResourceObject("lblLastDateToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("lblDuratioCaption") %></LABEL></TD>
	        <TD><%=mobjValues.TextControl("lblDuratio", 5, CStr(mobjUserValidate.nDuration),  , GetLocalResourceObject("lblDuratioToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("lblSysExpiredCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblSysExpired", 12, CStr(mobjUserValidate.dSysExpired),  , GetLocalResourceObject("lblSysExpiredToolTip"), True)%></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="3" CLASS="HorLine"></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="2"></TD>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
	    </TR>
	</TABLE>
</FORM>

</BODY>
</HTML>
<%
mobjValues = Nothing
mobjUserValidate = Nothing
%>




