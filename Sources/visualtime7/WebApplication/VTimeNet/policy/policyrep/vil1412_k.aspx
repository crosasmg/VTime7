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

mobjValues.sCodisplPage = "VIL1412"
%>
<html>
<head>
	<meta NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></script>



<script>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 1 $|$$Date: 31/10/03 17:16 $"
//------------------------------------------------------------------------------------------

//% insStateZone: updates the status of the fields in the page
//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]) {
        cbebranch.disabled=false;
        valproduct.disabled=false;
        btnvalproduct.disabled=false;
        tcdfecha.disabled=false;
        btn_tcdfecha.disabled=false;
        opttipo[0].disabled=false;
        opttipo[1].disabled=false;
		opttipo[2].disabled=false;                        
    }
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}


</script>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("VIL1412", "VIL1412_k.aspx", 1, vbNullString))
Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
mobjMenu = Nothing
%>    
</head>
	<body ONUNLOAD="closeWindows();">
		<form METHOD="POST" ID="FORM" NAME="VIL1412" ACTION="valpolicyrep.aspx?smode=2">
			<br><br>
			<table WIDTH="100%">
				<tr>
					<td><label><%= GetLocalResourceObject("cbebranchCaption") %></label></td>
					<td><%=mobjValues.BranchControl("cbebranch", GetLocalResourceObject("cbebranchToolTip"),  , "valproduct",  ,  ,  ,  , True)%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("valproductCaption") %></label></td>
					<td><%=mobjValues.ProductControl("valproduct", GetLocalResourceObject("valproductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True)%></td>
				</tr>
				<tr>
				    <td><label><%= GetLocalResourceObject("tcdfechaCaption") %></label></td>
				    <td><%=mobjValues.DateControl("tcdfecha", "",  , GetLocalResourceObject("tcdfechaToolTip"),  ,  ,  ,  , True)%></td>
				    <td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("AnchorCaption") %></label></td>
					<td><%=mobjValues.OptionControl(0, "opttipo", GetLocalResourceObject("opttipo_1Caption"), CStr(1), "1",  , True)%></td>
					<td><%=mobjValues.OptionControl(0, "opttipo", GetLocalResourceObject("opttipo_2Caption"),  , "2",  , True)%></td>
					<td><%=mobjValues.OptionControl(0, "opttipo", GetLocalResourceObject("opttipo_3Caption"),  , "3",  , True)%></td>				    									    
				</tr>
			</table>
		</form>
	</body>
</html>

<%
mobjValues = Nothing
%>




