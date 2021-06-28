<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "fic008"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateColumnCaption"), "tcdDate",  ,  , GetLocalResourceObject("tcdDateColumnCaption"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table260", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContratColumnCaption"), "tcnContrat", 10, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, CStr(0))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnCaption"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInterestColumnCaption"), "tcnInterest", 18, CStr(0),  , GetLocalResourceObject("tcnInterestColumnCaption"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExpensiveColumnCaption"), "tcnExpensive", 18, CStr(0),  , GetLocalResourceObject("tcnExpensiveColumnCaption"),  , 6)
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "FIC008"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'% insPreFIC008: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreFIC008()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lcolDraftHists As eFinance.DraftHists
	Dim lobjObject As Object
	Dim lintIndex As Object
	
	lcolDraftHists = New eFinance.DraftHists
	
	If lcolDraftHists.Find(mobjValues.StringToType(Session("dInit_Date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		lintCount = 0
		
		For	Each lobjObject In lcolDraftHists
			With lobjObject
				mobjGrid.Columns("tcdDate").DefValue = .dStat_date
				mobjGrid.Columns("cbeType").DefValue = .nType
				mobjGrid.Columns("tcnContrat").DefValue = .nContrat
				mobjGrid.Columns("tcnDraft").DefValue = .nDraft
				mobjGrid.Columns("cbeCurrency").DefValue = .nCurrency
				mobjGrid.Columns("tcnAmount").DefValue = .nAmount
				mobjGrid.Columns("tcnInterest").DefValue = .nInterest
				mobjGrid.Columns("tcnExpensive").DefValue = .nExpensive
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolDraftHists = Nothing
	lobjObject = Nothing
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "fic008"
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "FIC008", "FIC008.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="FIC008" ACTION="ValFinanceQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("FIC008"))

Call insDefineHeader()
Call insPreFIC008()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




