<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de la página.
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas.
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de las zonas de la pantalla.
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Configura los títulos del encabezado del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(40010, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  , "document.forms[0].valProduct.Parameters.Param1.sValue=this.value", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchColumnCaption"))
		Call .AddPossiblesColumn(40011, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitRangeColumnCaption"), "tcnInitRange", 18, "",  , GetLocalResourceObject("tcnInitRangeColumnCaption"),  , 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndRangeColumnCaption"), "tcnEndRange", 18, "",  , GetLocalResourceObject("tcnEndRangeColumnCaption"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommPercentColumnCaption"), "tcnCommPercent", 5, "",  , GetLocalResourceObject("tcnCommPercentColumnCaption"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommAmountColumnCaption"), "tcnCommAmount", 18, "",  , GetLocalResourceObject("tcnCommAmountColumnCaption"),  , 6)
		Call .AddPossiblesColumn(40387, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMinAmountColumnCaption"), "tcnMinAmount", 18, "",  , GetLocalResourceObject("tcnMinAmountColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMaxAmountColumnCaption"), "tcnMaxAmount", 18, "",  , GetLocalResourceObject("tcnMaxAmountColumnToolTip"),  , 6)
		Call .AddHiddenColumn("nbranch_a", CStr(eRemoteDB.Constants.intNull))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MCO678"
		.Codisp = "MCO678"
		.sCodisplPage = "MCO678"
		.Columns("valProduct").Parameters.Add("nBranch", Request.QueryString.Item("nBranch_a"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.ActionQuery = mobjValues.ActionQuery
		.Height = 400
		.Width = 420
		.Top = 100
		.Left = 200
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeBranch").EditRecord = Not .ActionQuery
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nCollectorType=" & mobjValues.TypeToString(Session("nCollectorType"), eFunctions.Values.eTypeData.etdDouble) & "&nContype=" & mobjValues.TypeToString(Session("nContype"), eFunctions.Values.eTypeData.etdDouble) & "&sCollecAsig=" & Session("sCollecAsig") & "&nDaysIni=" & mobjValues.TypeToString(Session("nDaysIni"), eFunctions.Values.eTypeData.etdDouble) & "&nDaysEnd=" & mobjValues.TypeToString(Session("nDaysEnd"), eFunctions.Values.eTypeData.etdDouble) & "&nCode=" & mobjValues.TypeToString(Session("nCode"), eFunctions.Values.eTypeData.etdDouble) & "&nInchannel=" & mobjValues.TypeToString(Session("nInchannel"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nBranch='        + marrArray[lintIndex].cbeBranch + '" & "&nProduct='       + marrArray[lintIndex].valProduct + '" & "&nInitRange='     + marrArray[lintIndex].tcnInitRange + '" & "&nEndRange='      + marrArray[lintIndex].tcnEndRange + '" & "&nCommPercent='   + marrArray[lintIndex].tcnCommPercent + '" & "&nCommAmount='    + marrArray[lintIndex].tcnCommAmount + '" & "&nCurrency='      + marrArray[lintIndex].cbeCurrency + '" & "&nMinAmount='     + marrArray[lintIndex].tcnMinAmount + '" & "&nMaxAmount='     + marrArray[lintIndex].tcnMaxAmount + '"
		
		'+ Permite continuar si el check está marcado
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreMCO678: Se cargan los datos repetitivos de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO678()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lclsCollect_comm As eCollection.Collect_comm
	Dim lclsCollect_comms As eCollection.Collect_comms
	lclsCollect_comm = New eCollection.Collect_comm
	lclsCollect_comms = New eCollection.Collect_comms
	lintCount = 0
	If lclsCollect_comms.Find(Session("nCollectorType"), Session("nContype"), Session("sCollecAsig"), Session("nDaysIni"), Session("nDaysEnd"), Session("nCode"), Session("nInchannel"), Session("dEffecdate")) Then
		For	Each lclsCollect_comm In lclsCollect_comms
			With mobjGrid
				.Columns("cbeBranch").DefValue = CStr(lclsCollect_comm.nBranch)
				.Columns("valProduct").Parameters.Add("nBranch", lclsCollect_comm.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = CStr(lclsCollect_comm.nProduct)
				.Columns("tcnInitRange").DefValue = CStr(lclsCollect_comm.nInitRange)
				.Columns("tcnEndRange").DefValue = CStr(lclsCollect_comm.nEndRange)
				.Columns("tcnCommPercent").DefValue = CStr(lclsCollect_comm.nCommPercent)
				.Columns("tcnCommAmount").DefValue = CStr(lclsCollect_comm.nCommAmount)
				.Columns("cbeCurrency").DefValue = CStr(lclsCollect_comm.nCurrency)
				.Columns("nBranch_a").DefValue = CStr(lclsCollect_comm.nBranch)
				.Columns("tcnMinAmount").DefValue = CStr(lclsCollect_comm.nMinAmount)
				.Columns("tcnMaxAmount").DefValue = CStr(lclsCollect_comm.nMaxAmount)
				.sEditRecordParam = "nBranch_a='+ marrArray[" & CStr(lintCount) & "].cbeBranch+'"
				lintCount = lintCount + 1
			End With
			' DoRow se encarga de mostrar los elementos del grid
			Response.Write(mobjGrid.DoRow())
		Next lclsCollect_comm
	End If
	Response.Write(mobjGrid.closeTable())
	' Boton de inicio
	Response.Write(mobjValues.BeginPageButton)
	lclsCollect_comm = Nothing
	lclsCollect_comms = Nothing
End Sub
'% insPreMCO678Upd : Permite realizar las actualizaciones sobre los aranceles Fonasa.
'-------------------------------------------------------------------------------------------
Private Sub insPreMCO678Upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsCollect_commDel As eCollection.Collect_comm
	lclsCollect_commDel = New eCollection.Collect_comm
	' Accion para eliminacion de datos del grid
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsCollect_commDel.insPostMCO678(.QueryString.Item("Action"), CInt(.QueryString.Item("nCollectorType")), CInt(.QueryString.Item("nContype")), .QueryString.Item("sCollecAsig"), CInt(.QueryString.Item("nDaysIni")), CInt(.QueryString.Item("nDaysEnd")), CInt(.QueryString.Item("nCode")), CInt(.QueryString.Item("nInchannel")), CDate(.QueryString.Item("dEffecdate")), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nInitRange"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nEndRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), "", "") Then
			End If
			lclsCollect_commDel = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", "MCO678", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MCO678"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:57 $|$$Author: Nvaplat61 $"
    </SCRIPT>


    <%Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT> var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCO678", "MCO678.aspx"))
	mobjMenu = Nothing
End If
Response.Write(mobjValues.ShowWindowsName("MCO678"))
Response.Write(mobjValues.WindowsTitle("MCO678"))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frm689" ACTION="valMantCollection.aspx?sZone=2">
<%
'+ Se configura la estructura del grid, deacuerdo al tipo de ventana.
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCO678Upd()
Else
	Call insPreMCO678()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





