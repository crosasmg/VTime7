<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "ms004_k"
%>

<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<SCRIPT>

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------

	with(self.document){
		with(forms[0]){
			valCurrency.disabled = false
		}
		btnvalCurrency.disabled = false 
	} 
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}


</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MS004", "MS004_K.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="MS004_K" ACTION="valMantSys.aspx?mode=1">
	    <BR><BR>
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD WIDTH=120pcx><LABEL ID=102116><%= GetLocalResourceObject("valCurrencyCaption") %></LABEL></TD>
	            <TD><%=mobjValues.PossiblesValues("valCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCurrencyToolTip"))%></TD>
			</TR>
	    </TABLE>
	</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>




