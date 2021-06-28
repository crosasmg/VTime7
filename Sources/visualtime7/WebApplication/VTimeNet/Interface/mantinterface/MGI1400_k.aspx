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

mobjValues.sCodisplPage = "MGI1400_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 4 $|$$Date: 31/10/03 17:16 $"
//------------------------------------------------------------------------------------------

//% insStateZone: updates the status of the fields in the page
//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]) {
        cbeSystem.disabled=false;
        valTable.disabled=false;
        btnvalTable.disabled=false;
    }
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MGI1400", "MGI1400_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="MGI1400" ACTION="valmantinterface.aspx?sMode=1">
			<BR><BR>
			<TABLE WIDTH="100%">
				<TR>
					<TD>
						<LABEL><%= GetLocalResourceObject("cbeSystemCaption") %></LABEL>
					</TD>
					<TD>
						<%=mobjValues.PossiblesValues("cbeSystem", "Table5705", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSystemToolTip"),  , 14)%>
					</TD>
					<TD>&nbsp;</TD>
					<TD> 
						<LABEL><%= GetLocalResourceObject("valTableCaption") %></LABEL> 
					</TD>
					<TD>
						<%=mobjValues.PossiblesValues("valTable", "Table5706", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valTableToolTip"),  , 14)%>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

<%
mobjValues = Nothing
%>




