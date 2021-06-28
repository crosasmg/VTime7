<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- Object for the handling of the zones of the page.
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG7000"
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//**% insStateZone: This function updates the status of field (Enable/Disable) of the zone in process.
//% insStateZone: Esta función actualiza el estado (habilita o disabilita) de los campos de la zona 
//% en proceso.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    if (top.fraSequence.plngMainAction == 302 ||
        top.fraSequence.plngMainAction == 401)
    {
        with(self.document.forms[0]){
	        valTab_ComLif.disabled    = false
	        btnvalTab_ComLif.disabled = false
	        cbeBranch.disabled        = false
	        valProduct.disabled       = false
	        btnvalProduct.disabled    = false
	        tcdEffecdate.disabled     = false
	        btn_tcdEffecdate.disabled = false
	    }
	}
}

//**% insCancel:This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true
}
</SCRIPT>
<HTML>
<HEAD>

    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>    
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 3 $|$$Date: 24/09/03 16:29 $"
//------------------------------------------------------------------------------
</SCRIPT>    

<%
Response.Write(mobjValues.StyleSheet())

mobjMenu = New eFunctions.Menues

Response.Write(mobjMenu.MakeMenu("MAG7000", "MAG7000_K.aspx", 1, vbNullString))

mobjMenu = Nothing
%>    

</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MSI021" ACTION="valMantAgent.aspx?">
    <BR>
    <TABLE WIDTH="100%" CELLPADDING=6>
		<TR>
			<TD>&nbsp;</TD>			
		</TR>
		
<!--[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003-->
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valTab_ComLifCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valTab_ComLif", "tab_ComLif", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valTab_ComLifToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), False, GetLocalResourceObject("tcdEffecdateToolTip"),  , CStr(False),  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType, True, vbNullString)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypetableCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeTypetable", "Table8005", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypetableToolTip")))%></TD>
        </TR>

	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





