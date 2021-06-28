<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

Dim mobjGeneral As eGeneral.GeneralFunction


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MGSL008_K")

mobjValues = New eFunctions.Values
mobjGeneral = New eGeneral.GeneralFunction

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MGSL008_K"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("MGSL008", "MGSL008_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    

<SCRIPT> 
//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------
function insStateZone()
//-----------------------------------------------------------------------------
{
    //self.document.forms[0].tcnYear.disabled=false
    //-self.document.forms[0].cbeBranch.disabled=false
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel()
//-----------------------------------------------------------------------------
{
   return true
}
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 3/12/03 15:03 $|$$Author: Nvaplat37 $"
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL008" ACTION="valMarginRep.aspx?sMode=1">
    <BR><BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <BR><BR>
        <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitDate", CStr(mobjGeneral.GetLastFistDay("FIRST")),  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate", CStr(mobjGeneral.GetLastFistDay("LAST")),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
        </TR>
    </TABLE>

</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGeneral = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("MGSL008_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




