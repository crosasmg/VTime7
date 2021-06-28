<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
Dim mstrMarca As String


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MSO008"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        tcdEffecdate.disabled=false;
		btn_tcdEffecdate.disabled=false;        
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

</SCRIPT>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT= "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MSO008", "MSO008_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MSO008" ACTION="valMantAuto.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
		<TR>        
			<TD WIDTH=25%> </TD>
			<TD WIDTH=25%>
				<LABEL ID=LABEL1><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL>
			</TD>
			<TD WIDTH=25%>
				<%=mobjValues.DateControl("tcdEffecdate",  , True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%>
			</TD>
			<TD WIDTH=25%> </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>






