<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG004"
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//% insStateZone: Inhabilita determinados campos de acuerdo a la acción en tratamiento.
//----------------------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------------------
	with (self.document.forms[0]){ 
		valTable_cod.disabled = false
		gmdEffecdate.disabled = false
		cboCurrency.disabled = false
		cboType_infor.disabled = false
		btnvalTable_cod.disabled = false
		btn_gmdEffecdate.disabled = false
	}
}

//% insCancel: Ejecuta la acción Cancelar de la página
//----------------------------------------------------------------------------------------------
function insCancel(){
//----------------------------------------------------------------------------------------------
	return(true);
}

//% insFinish: Ejecuta la acción Finalizar de la página
//----------------------------------------------------------------------------------------------
function insFinish(){
//----------------------------------------------------------------------------------------------
	return(true);
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG004_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTabExtComm" ACTION="valMantAgent.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">
            
        <TR>
            <TD><LABEL ID=11737><%= GetLocalResourceObject("valTable_codCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valTable_cod", "tab_excomm", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valTable_codToolTip"),  ,  ,  , False)%></TD>
            <TD><LABEL ID=11734><%= GetLocalResourceObject("gmdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("gmdEffecdate", CStr(Today),  , GetLocalResourceObject("gmdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>        
            <TD><LABEL ID=11735><%= GetLocalResourceObject("cboCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboCurrencyToolTip"))%></TD>
            <TD><LABEL ID=11736><%= GetLocalResourceObject("cboType_inforCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cboType_infor", "table276", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboType_inforToolTip")))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>




