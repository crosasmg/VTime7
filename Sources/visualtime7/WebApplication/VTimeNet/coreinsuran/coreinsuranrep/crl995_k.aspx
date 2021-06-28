<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

    mobjValues.sCodisplPage = "crl995_k"
%>
<HTML>
<HEAD>

<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tMenu.js"></SCRIPT>
<meta http-equiv="Content-Language" content="es">

<%=mobjValues.StyleSheet()%>
<% mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.MakeMenu("CRL995", "CRL995_K.aspx", 1, ""))
    mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<BR></BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
<BR></BR>
<FORM METHOD="POST" ID="FORM" NAME="frmPrintRCessClaim" ACTION="ValCoReinsuranRep.aspx?X=1">
<TABLE WIDTH="100%">
    <TR>
        <TD><LABEL ID=101668><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdEnddate", CStr(Today),  , GetLocalResourceObject("tcdEnddateToolTip"),  ,  ,  ,  ,  , 2)%></TD>
        <TD>&nbsp;</TD>
    </TR>  
	<TR>
	    <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Tipo de Ejecución"><%= GetLocalResourceObject("AnchorCaption") %></a></LABEL></TD>
    </TR>
	<TR>
	    <TD COLSPAN="4" CLASS="HorLine"></TD>
    </TR>
    <TR>
        <TD><% Response.Write(mobjValues.OptionControl(40670, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))%></TD><!-- PRELIMINAR-->
        <TD><% Response.Write(mobjValues.OptionControl(40671, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"), , "1"))%></TD><!-- DEFINITIVO-->
    </TR>
</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>