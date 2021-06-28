<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "cr726_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE="JavaScript">
// Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].cbenBranch_rei.disabled=false;
	self.document.forms[0].tcnNumber.disabled=false;
	self.document.forms[0].cbeType.disabled=false;
	self.document.forms[0].valCovergen.disabled=false;
	self.document.forms[0].tcdEffecdate.disabled=false;
	self.document.forms[0].btnvalCovergen.disabled=false;
	self.document.forms[0].btn_tcdEffecdate.disabled=false;
	
	if(top.frames['fraSequence'].plngMainAction==401)
		self.document.images['A306'].disabled=true;
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CR726", "CR726_k.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="CR726" ACTION="ValCoReinsuranTra.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID="100601"><%= GetLocalResourceObject("cbenBranch_reiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenBranch_rei", "table5000", eFunctions.Values.eValuesType.clngComboType, Session("nBranch_rei"),  ,  ,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID="0"><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5, Session("nNumber"),  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID="100600"><%= GetLocalResourceObject("cbeTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.List = "1"
Response.Write(mobjValues.PossiblesValues("cbeType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType"),  ,  ,  ,  ,  ,  , True))%></TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("valCovergenCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType, Session("nPriorCoverGen"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCovergenToolTip"))%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




