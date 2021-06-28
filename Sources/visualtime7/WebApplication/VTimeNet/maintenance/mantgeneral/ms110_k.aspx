<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MS110"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MS110", "MS110_K.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
       
//% insStateZone: se controla el estado de los campos
//---------------------------------------------------------------------------------
function insStateZone(){
//---------------------------------------------------------------------------------
	with(self.document){
		with(forms[0]){
			tcnCompany.disabled = false
		}
		btntcnCompany.disabled = false 
	} 
}

//% insCancel: Ejecuta las rutinas necesarias para la cancelación de la transacción
//---------------------------------------------------------------------------------
function insCancel(){
//---------------------------------------------------------------------------------
	return true
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="MS110_K" ACTION="valMantGeneral.aspx?sTime=1">
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD WIDTH="15%"><LABEL ID=11935><%= GetLocalResourceObject("tcnCompanyCaption") %></LABEL></TD>
	            <TD><%=mobjValues.CompanyControl("tcnCompany", Session("nCompany"),  , GetLocalResourceObject("tcnCompanyToolTip"),  , True, "tctCompanyName")%></TD>
			</TR>
	    </TABLE>
	</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




