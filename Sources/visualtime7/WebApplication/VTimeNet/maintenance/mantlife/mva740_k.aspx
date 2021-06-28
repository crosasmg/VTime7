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
mobjValues.sCodisplPage = "MVA740"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("MVA740", "MVA740_K.aspx", 1, vbNullString))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
				valProduct.Parameters.Param1.sValue=sValue;
				valProduct.disabled = (sValue == '0');
				btnvalProduct.disabled = valProduct.disabled;
				break;
		}
		valProduct.value = '';
		UpdateDiv('valProductDesc','');
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" NAME="MVA740" ACTION="valMantLife.aspx?sMode=2">
	<BR>
	<BR>
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD><LABEL ID="0"><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
	            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)", True,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>
				<TD><LABEL ID="0"><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
				</TD>
	        </TR>
			<TR>
				<TD><LABEL ID="0"><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
				<TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
			</TR>
	    </TABLE>
	</FORM> 
</BODY>
</HTML>




