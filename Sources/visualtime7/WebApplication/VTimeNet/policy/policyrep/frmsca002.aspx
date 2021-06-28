<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("frmsca002")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "frmsca002"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmXXXXXX" ACTION="XXXXXX.aspx?sCodispl=XXXXXX">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=19653><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><LABEL ID=19654><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD><LABEL ID=19650><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><LABEL ID=19655><%= GetLocalResourceObject("tctDetailCaption") %></LABEL></TD>
            <TD></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.TextAreaControl("tctDetail", 2, 12, "DefaultValue")%></TD>
            <TD></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.ButtonControl("cmdAccept", "Aceptar")%></TD>
            <TD><%=mobjValues.ButtonControl("cmdCancel", "Cancelar")%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("frmsca002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




