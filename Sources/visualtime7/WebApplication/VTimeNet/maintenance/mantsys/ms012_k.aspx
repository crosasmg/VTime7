<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Object for the handling of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Object for the handling of the areas of the page
'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms012_k"
%>

	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<HTML>
<HEAD>

<SCRIPT>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------------------

//% insStateZone: updates the status of the fields in the page
//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]) {
        cbeEcon_area.disabled = false;
    }
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

//% insShowvalue: Shown the value of "None" in case of field "Economic area" is empty
//% insShowvalue: Muestra el valor de "No aplica" en caso de que se deje vacío el campo "Área económica"
//------------------------------------------------------------------------------------------
function insShowvalue(field){
//------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if (field.value == 0)
			cbeEcon_area.value = 1;
    }
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MS012", "MS012_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="MS012" ACTION="ValMantSys.aspx?sMode=1">
			<BR><BR>
			<TABLE WIDTH="100%">
				<TR>
					<TD WIDTH=25%> </TD>
					<TD WIDTH=25%>
						<LABEL ID=0><%= GetLocalResourceObject("cbeEcon_areaCaption") %></LABEL>
					</TD>
					<TD WIDTH=25%>
						<%=mobjValues.PossiblesValues("cbeEcon_area", "Table640", eFunctions.Values.eValuesType.clngComboType, Session("cbeEcon_area"),  ,  ,  ,  ,  , "insShowvalue(this)", True, 2, GetLocalResourceObject("cbeEcon_areaToolTip"), eFunctions.Values.eTypeCode.eNumeric)%>
					</TD>
					<TD WIDTH=25%> </TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>




