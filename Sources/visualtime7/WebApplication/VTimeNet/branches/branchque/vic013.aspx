<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.    
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'**% insDefineHeader: The columns del grid are defined.
'% insDefineHeader: Se definen las columnas del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "vic013"
	
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctFundColumnCaption"), "tctFund", 38, "",  , GetLocalResourceObject("tctFundColumnToolTip"),  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnInitialBalanceColumnCaption"), "tcnInitialBalance", 18, "",  , GetLocalResourceObject("tcnInitialBalanceColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsPurchasedColumnCaption"), "tcnUnitsPurchased", 18, "",  , GetLocalResourceObject("tcnUnitsPurchasedColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsSoldColumnCaption"), "tcnUnitsSold", 18, "",  , GetLocalResourceObject("tcnUnitsSoldColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCurrentBalanceColumnCaption"), "tcnCurrentBalance", 18, "",  , GetLocalResourceObject("tcnCurrentBalanceColumnToolTip"), True, 6,  ,  ,  , True)
	End With
	
	'**+ The general properties of the grid are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "VIC013"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreVIC013: This function allows to show in the grid the read values.
'% insPreVIC013: Esta función permite mostrar en el grid los valores leídos.
'--------------------------------------------------------------------------------------------
Private Sub insPreVIC013()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	
	Dim lintInitialBalance As Double
	Dim lintUnitsPurch As Byte
	Dim lintUnitsSold As Byte
	Dim lintLastFund As Integer
	Dim lstrLastFund As String
	
	Dim lclsFund_Stock As Object
	Dim lclsFund_Stocks As ePolicy.Fund_stocks
	Dim lclsFunds As ePolicy.Funds
	
	lclsFund_Stocks = New ePolicy.Fund_stocks
	lclsFunds = New ePolicy.Funds
	
	If lclsFund_Stocks.Find_AllTrans(Session("dDate")) Then
		lintCount = 0
		lintUnitsPurch = 0
		lintUnitsSold = 0
		lintInitialBalance = 0
		
		For	Each lclsFund_Stock In lclsFund_Stocks
			With lclsFund_Stock
				If lintCount = 0 Then
					If .nMove_type = 1 Or .nMove_type = 2 Then
						lintUnitsPurch = lintUnitsPurch + .nUnits
					Else
						lintUnitsSold = lintUnitsSold + .nUnits
					End If
				Else
					If lintLastFund = .nFunds Then
						If .nMove_type = 1 Or .nMove_type = 2 Then
							lintUnitsPurch = lintUnitsPurch + .nUnits
						Else
							lintUnitsSold = lintUnitsSold + .nUnits
						End If
					Else
						lintInitialBalance = lclsFunds.insCalInitialBalance(Session("dDate"), lintLastFund)
						
						mobjGrid.Columns("tctFund").DefValue = lintLastFund & " - " & lstrLastFund
						mobjGrid.Columns("tcnInitialBalance").DefValue = CStr(lintInitialBalance)
						mobjGrid.Columns("tcnUnitsPurchased").DefValue = CStr(lintUnitsPurch)
						mobjGrid.Columns("tcnUnitsSold").DefValue = CStr(lintUnitsSold)
						mobjGrid.Columns("tcnCurrentBalance").DefValue = CStr(lintInitialBalance + (cdbl(lintUnitsPurch) - cdbl(lintUnitsSold)))
						
						Response.Write(mobjGrid.DoRow())
						
						lintUnitsPurch = 0
						lintUnitsSold = 0
						lintInitialBalance = 0
						
						If .nMove_type = 1 Or .nMove_type = 2 Then
							lintUnitsPurch = lintUnitsPurch + .nUnits
						Else
							lintUnitsSold = lintUnitsSold + .nUnits
						End If
					End If
				End If
				
				lintLastFund = .nFunds
				lstrLastFund = .sFunds
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsFund_Stock
		
		lintInitialBalance = lclsFunds.insCalInitialBalance(Session("dDate"), lintLastFund)
		
		mobjGrid.Columns("tctFund").DefValue = lintLastFund & " - " & lstrLastFund
		mobjGrid.Columns("tcnInitialBalance").DefValue = CStr(lintInitialBalance)
		mobjGrid.Columns("tcnUnitsPurchased").DefValue = CStr(lintUnitsPurch)
		mobjGrid.Columns("tcnUnitsSold").DefValue = CStr(lintUnitsSold)
		mobjGrid.Columns("tcnCurrentBalance").DefValue = CStr(lintInitialBalance + (cdbl(lintUnitsPurch) - cdbl(lintUnitsSold)))
		
		Response.Write(mobjGrid.DoRow())
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsFund_Stocks = Nothing
	lclsFund_Stock = Nothing
	lclsFunds = Nothing
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "vic013"
%>
<SCRIPT LANGUAGE="JavaScript">

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//**% insCancel: This function executes the cancel action of the page.
//% insCancel: Ejecuta la acción cancelar de la página.
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
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "VIC013", "VIC013.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIC013" ACTION="ValBranchQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("VIC013"))

Call insDefineHeader()
Call insPreVIC013()

mobjGrid = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>     
</FORM>
</BODY>
</HTML>





