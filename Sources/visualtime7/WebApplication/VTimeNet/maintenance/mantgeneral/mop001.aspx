<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim mobjOptionInstall As eGeneral.OptionsInstallation


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjOptionInstall = New eGeneral.OptionsInstallation
Call mobjOptionInstall.insPreMOP001()
Session("dInstalldateCash") = mobjOptionInstall.dInstalldate
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MOP001"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE=JavaScript>
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
    
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;  
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         if (lintIndex != 2) {
             document.forms[0].elements[lintIndex].disabled = false
             }
	return true;
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
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.SetZone(2, "MOP001", "MOP001.aspx"))
		mobjMenu = Nothing
	End If
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
End With
%>
</HEAD>
<BR>
<BR>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" NAME="MOP001_K" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<TABLE WIDTH="100%">
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBalanceCaption") %></LABEL></TD>
		<TD><%Response.Write(mobjValues.PossiblesValues("cbeBalance", "Table187", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nSta_chequeCash),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeBalanceToolTip")))%></td>
	</TR>
    <TR>
    </TR>
    <TR> 
        <TD><%Response.Write(mobjValues.CheckControl("chkPartialCol", GetLocalResourceObject("chkPartialColCaption"), CStr(mobjOptionInstall.nCollect_pCash), CStr(1),  , False))%></td>
    </TR>
	<TR>
		<TD COLSPAN="2" CLASS="HIGHLIGHTED"><LABEL ID="0"><%= GetLocalResourceObject("AnchorCaption") %></LABEL></td>
	</TR>
	<TR>
	    <TD COLSPAN="2" CLASS="HorLine"></TD>
	</TR>
	<TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeinsur_areaCaption") %></LABEL></TD>
		<TD><%=mobjValues.PossiblesValues("cbeinsur_area", "table5001", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("cbeinsur_areaToolTip"))%></td>
	</TR>
	<TR>
        <TD><LABEL ID="0"><%= GetLocalResourceObject("tcnExpensesCaption") %></LABEL></td>
    	<TD><%=mobjValues.NumericControl("tcnExpenses", 18, CStr(mobjOptionInstall.nExpensesCash),  , GetLocalResourceObject("tcnExpensesToolTip"), False, 6,  ,  ,  ,  , False)%></td>
    </TR>	
	<TR>
        <TD><LABEL ID="11735"><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrencyCash),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
    </TR>
	<TR>
        <TD><LABEL ID="0"><%= GetLocalResourceObject("tcnFinanIntCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnFinanInt", 5, CStr(mobjOptionInstall.nFinanInt),  , GetLocalResourceObject("tcnFinanIntToolTip"),  , 2,  ,  ,  ,  , False)%></TD>
    </TR>
</TABLE>
</FORM> 
</BODY>
</HTML>




