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
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL600"
%>
<HTML>
<HEAD>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></script>



<SCRIPT>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 2 $|$$Date: 24/02/06 11:18a $"
//------------------------------------------------------------------------------------------

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	if (typeof(document.forms[0])!='undefined'){
		with (self.document.forms[0]){
			cbebranch.disabled=false;
			valproduct.disabled=false;
			btnvalproduct.disabled=false;
			opttipo[0].disabled=false;
			opttipo[1].disabled = false;
			tcdEffecdate.disabled = false;
			btn_tcdEffecdate.disabled = false;   
		}
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

</SCRIPT>
<%

Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CAL600", "CAL600_K.aspx", 1, vbNullString))
Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
mobjMenu = Nothing
%>    
</head>
	<body ONUNLOAD="closeWindows();">
		<form METHOD="POST" ID="FORM" NAME="CAL600" ACTION="valpolicyrep.aspx?smode=2">
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
				    <td><label><%= GetLocalResourceObject("tcdEffecdateCaption") %></label></td>
				    <td><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
				</tr>
				<tr>
					<td><label><%= GetLocalResourceObject("Anchor2Caption") %></label></td>
					<td><%=mobjValues.OptionControl(0, "opttipo", GetLocalResourceObject("opttipo_1Caption"), CStr(1), "1",  , True)%></td>
					<td><%=mobjValues.OptionControl(0, "opttipo", GetLocalResourceObject("opttipo_2Caption"),  , "2",  , True)%></td>				    
				</tr>
			</table>
		</form>
	</body>
</html>

<%
mobjValues = Nothing
%>



