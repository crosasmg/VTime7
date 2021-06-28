<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- The object to handling the general function for the load of values is define
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MCO002_K"
%>

	<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//**% insStateZone:It allows to qualify the objects and images of the page.
//%insStateZone: Permite habilitar los objetos e imágenes de la página.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        tcdEffecdate.disabled=false;
        btn_tcdEffecdate.disabled=false;
    }
}
//**%insCancel: It allows to cancel the page.    
//%insCancel: Permite cancelar la página.		
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>

<HTML>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCO002_K.aspx", 1, ""))
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu = Nothing
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="MCO002" ACTION="ValMantCollection.aspx?sMode=1">
	    <BR><BR>
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD><LABEL ID=105853><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
	            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
	            <TD>&nbsp;</TD>
	        </TR>
	    </TABLE>
	</FORM>
</BODY>
</HTML>








