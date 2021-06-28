<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

Dim mobjGeneral As eGeneral.GeneralFunction


</script>

<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjGeneral = New eGeneral.GeneralFunction
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGSL009"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 28/11/03 18:52 $|$$Author: Nvaplat37 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MGSL009", "MGSL009_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL009" ACTION="valmarginrep.aspx?sMode=1">
    <BR><BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitDate", CStr(mobjGeneral.GetLastFistDay("FIRST")),  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate", CStr(mobjGeneral.GetLastFistDay("LAST")),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
        </TR>
        
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
mobjGeneral = Nothing
%>




