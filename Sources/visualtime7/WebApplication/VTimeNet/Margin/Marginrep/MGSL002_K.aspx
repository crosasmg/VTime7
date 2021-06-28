<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGSL002"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

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
	.Write(mobjMenu.MakeMenu("MGSL002", "MGSL002_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL002" ACTION="valmarginrep.aspx?sMode=1">
    <BR><BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%" >
        <TR> 
            <TD WIDTH="30%" ></TD>
        	<TD WIDTH="20%" ><LABEL ID=0><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdProcessDate", CStr(Today),  , GetLocalResourceObject("tcdProcessDateToolTip"))%></TD>
            <TD WIDTH="30%" ></TD>
		</TR>
		
		
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>




