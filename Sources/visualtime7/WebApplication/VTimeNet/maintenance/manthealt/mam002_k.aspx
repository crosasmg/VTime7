<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página.
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAM002"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT> 
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
	
//% insStateZone: Habilitación/Deshabilitación de campos de la forma según la acción a procesar.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    document.forms[0].tcdEffecdate.disabled=false
    document.forms[0].btn_tcdEffecdate.disabled=false
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insFinish: controla la acción de Finalizar de la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MAM002", "MAM002_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MAM002" ACTION="ValMantHealt.aspx?sMode=1">
<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="10%"><LABEL ID=104865><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>






