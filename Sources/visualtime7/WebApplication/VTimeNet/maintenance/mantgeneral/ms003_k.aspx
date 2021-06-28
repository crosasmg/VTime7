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

mobjValues.sCodisplPage = "MS003"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<HTML>
<HEAD>

<SCRIPT>

//% insCancel: Ejecuta rutinas necesarias en el momento de finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].cbeBank.disabled = false

}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()		
//------------------------------------------------------------------------------------------
{
	return true
}
</SCRIPT>
		<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
		<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MS003", "MS003_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<TD><BR></TD>
		<TD><BR></TD>
		<FORM METHOD="post" ID="FORM" NAME="MS003" ACTION="valMantGeneral.aspx?x=1">
			<TR>
				<TD WIDTH="15%"><LABEL ID=101880><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"))%></TD>
			</TR>
		</FORM>
	</BODY>
</HTML>




