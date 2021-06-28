<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "cr766_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
 	with (self.document.forms[0]){
 		cbenBranch_rei.disabled= false
		tcnNumber.disabled= false
		cbeType.disabled= false
		valCovergen.disabled= false
		btnvalCovergen.disabled= false
		tcdEffecdate.disabled= false
		btn_tcdEffecdate.disabled= false
		tcnDeductible.disabled = false
		tcnCapital.disabled = false
		tcnQfamily.disabled = false
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CR766", "CR766.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $" 
</SCRIPT>   
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="CR766" ACTION="ValCoReinsuranTra.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
            <TD><label ID="100601"><%= GetLocalResourceObject("cbenBranch_reiCaption") %></label></TD>
            <TD><%=mobjValues.PossiblesValues("cbenBranch_rei", "table5000", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenBranch_reiToolTip"))%></TD>
            <TD><label ID="0"><%= GetLocalResourceObject("tcnNumberCaption") %></label></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5, vbNullString,  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><label ID="100600"><%= GetLocalResourceObject("cbeTypeCaption") %></label></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.List = "1"
Response.Write(mobjValues.PossiblesValues("cbeType", "table173", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeToolTip")))%></TD>
			<TD><label ID="0"><%= GetLocalResourceObject("valCovergenCaption") %></label></TD>
            <TD><%=mobjValues.PossiblesValues("valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valCovergenToolTip"))%> </TD>
        <TR>    
		</TR>            
			<TD><label ID="0"><%= GetLocalResourceObject("tcnDeductibleCaption") %></label></TD>
			<TD><%=mobjValues.NumericControl("tcnDeductible", 18, vbNullString,  , GetLocalResourceObject("tcnDeductibleToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD><label ID="0"><%= GetLocalResourceObject("tcnCapitalCaption") %></label></TD>
			<TD><%=mobjValues.NumericControl("tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        <TR>    
		</TR>			
			<TD><label ID="0"><%= GetLocalResourceObject("tcnQfamilyCaption") %></label></TD>
			<TD><%=mobjValues.NumericControl("tcnQfamily", 3, vbNullString,  , GetLocalResourceObject("tcnQfamilyToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
            <TD><label ID="0"><%= GetLocalResourceObject("tcdEffecdateCaption") %></label></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>			
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




