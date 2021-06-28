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
mobjValues.sCodisplPage = "CRL847_1"
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
	        valTab_79.disabled      = false
	        valTab_gencov.disabled= false
	        ValMaxRet.disabled = false
			IndSoloCorredores.disabled = false
	        cbeBranch.disabled        = false
	        btnvalTab_gencov.disabled    = false
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
	document.VssVersion="$$Revision: 3 $|$$Date: 22/12/03 16:23 $"
//------------------------------------------------------------------------------
</SCRIPT>    

<%
Response.Write(mobjValues.StyleSheet())

mobjMenu = New eFunctions.Menues

Response.Write(mobjMenu.MakeMenu("CRL847_1", "CRL847_1_K.aspx", 1, vbNullString))

mobjMenu = Nothing
%>    

</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL847_1" ACTION="valCoReinsuranRep.aspx?sMode=1">
    <BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN=4>&nbsp;</TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valTab_79Caption") %></LABEL></TD>
			<TD><%
mobjValues.TypeList = 2
mobjValues.List = "1,4"
Response.Write(mobjValues.PossiblesValues("valTab_79", "table79", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valTab_79ToolTip"), eFunctions.Values.eTypeCode.eNumeric, 7, False))
%></TD>
			<TD COLSPAN=2></TD>
		</TR>
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valTab_gencovCaption") %></LABEL></TD>
			<TD COLSPAN=3><%=mobjValues.PossiblesValues("valTab_gencov", "REAGEN_COVER_6", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valTab_gencovToolTip"), eFunctions.Values.eTypeCode.eNumeric,  , True)%></TD>
        </TR>
        <TR>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), False, GetLocalResourceObject("tcdEffecdateToolTip"),  , CStr(False),  ,  , True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  ,  , True)%></TD>

        </TR>
        <TR>        
            <TD COLSPAN=2><%=mobjValues.CheckControl("IndSoloCorredores", GetLocalResourceObject("IndSoloCorredoresCaption"), "", "1",  , True,  , GetLocalResourceObject("IndSoloCorredoresToolTip"))%></TD>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("ValMaxRetCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("ValMaxRet", 18, CStr(0), False, GetLocalResourceObject("ValMaxRetToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





