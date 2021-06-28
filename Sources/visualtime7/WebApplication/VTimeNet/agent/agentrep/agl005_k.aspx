<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl005_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl005_k"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGL005", "AGL005_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL005" ACTION="valAgentRep.aspx?sMode=1">
	<BR><BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
	<TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted" WIDTH=30%><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD COLSPAN="3">&nbsp;</TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="3"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitdateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitdate", vbNullString, True, GetLocalResourceObject("tcdInitdateToolTip"))%></TD>
			<TD WIDTH=10%>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEnddate", vbNullString, True, GetLocalResourceObject("tcdEnddateToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl005_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




