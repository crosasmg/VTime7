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
	
	mobjGrid.sCodisplPage = "vic014"
	
	With mobjGrid.Columns
		.AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctEntryColumnCaption"), "tctEntry", 30, "",  , GetLocalResourceObject("tctEntryColumnToolTip"),  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsColumnCaption"), "tcnUnits", 18, "",  , GetLocalResourceObject("tcnUnitsColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, "",  , GetLocalResourceObject("tcnBalanceColumnToolTip"), True, 6,  ,  ,  , True)
	End With
	
	'**+ The general properties of the grid are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "VIC014"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreVIC014: This function allows to show in the grid the read values.
'% insPreVIC014: Esta función permite mostrar en el grid los valores leídos.
'--------------------------------------------------------------------------------------------
Private Sub insPreVIC014()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lclsFund_stock As Object
	Dim lcolFund_stocks As ePolicy.Fund_stocks
	Dim ldblBalance As Double
	
	lcolFund_stocks = New ePolicy.Fund_stocks
	
	If lcolFund_stocks.Find_AllUnits(Session("nFund"), Session("dDate")) Then
		lintCount = 0
		ldblBalance = 0
		
		For	Each lclsFund_stock In lcolFund_stocks
			With lclsFund_stock
				mobjGrid.Columns("tcdEffecdate").DefValue = .dEffecdate
				mobjGrid.Columns("tctEntry").DefValue = .sMove_type
				mobjGrid.Columns("tcnUnits").DefValue = .nUnits
				
				If lintCount < 1 Then
					ldblBalance = .nUnits
				Else
					If .nMove_type = 1 Or .nMove_type = 2 Then
						ldblBalance = ldblBalance + .nUnits
					Else
						ldblBalance = ldblBalance - .nUnits
					End If
				End If
				
				mobjGrid.Columns("tcnBalance").DefValue = CStr(ldblBalance)
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsFund_stock
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolFund_stocks = Nothing
	lclsFund_stock = Nothing
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "vic014"
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
Response.Write(mobjMenu.setZone(2, "VIC014", "VIC014.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIC014" ACTION="ValBranchQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("VIC014"))

Call insDefineHeader()
Call insPreVIC014()

mobjGrid = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>     
</FORM>
</BODY>
</HTML>




