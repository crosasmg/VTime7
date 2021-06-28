<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBudget" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-----------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "cpc003"
	
	'+ Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 10, "",  , GetLocalResourceObject("tctAccountColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctAux_accounColumnCaption"), "tctAux_accoun", 10, "",  , GetLocalResourceObject("tctAux_accounColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCost_centeColumnCaption"), "tctCost_cente", 10, "",  , GetLocalResourceObject("tctCost_centeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, CStr(0),  , GetLocalResourceObject("tcnBalanceColumnToolTip"), True, 6)
		Call .AddTextColumn(0, GetLocalResourceObject("ColumnCaption"), "", 10, "tcnPercentageDif",  , GetLocalResourceObject("ColumnToolTip"))
	End With
	
	With mobjGrid
		.Codispl = "CPC003"
		.AddButton = False
		.DeleteButton = False
		.Top = 70
		.Width = 330
		.Height = 400
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCPC003: Carga los datos en el grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreCPC003()
	'---------------------------------------------------------------
	
	Dim lclsBudget As eBudget.Budget
	Dim lclsBudget_amo As eBudget.Budget_amo
	Dim lcolBudget_amo As eBudget.Budget_amos
	Dim ldtmInitDate As Date
	Dim ldtmEndDate As Date
	Dim lintMonth As Integer
	Dim ldblAmount As Double
	Dim ldblDifPercent As Double
	
	lclsBudget = New eBudget.Budget
	lclsBudget_amo = New eBudget.Budget_amo
	lcolBudget_amo = New eBudget.Budget_amos
	
	lintMonth = mobjValues.StringToType(Request.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble)
	
	Call lclsBudget.Find(mobjValues.StringToType(Session("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBud_code"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble))
	
	'+ Si la opción Saldo es Mensual
	
	If Request.QueryString.Item("optBalance") = "1" Then
		
		'+ Se calcula la fecha inicial
		
		If lintMonth < 10 Then
			ldtmInitDate = CDate("01" & "/0" & Request.QueryString.Item("nMonth") & "/" & CStr(lclsBudget.nYear))
		Else
			ldtmInitDate = CDate("01" & "/" & Request.QueryString.Item("nMonth") & "/" & CStr(lclsBudget.nYear))
		End If
	Else
		
		'+ Si la opción Saldo es Acumulado
		'*************
		'		If Mid(CStr(lclsBudget.nInit_month), 5, 1) < 10 Then
		'		    ldtmInitDate = CDate("01" & "/0" & CStr(Mid(CStr(lclsBudget.nInit_month), 5, 1)) & "/" & CStr(lclsBudget.nYear))
		'		Else
		'		    ldtmInitDate = CDate("01" & "/" & CStr(Mid(CStr(lclsBudget.nInit_month), 5, 1)) & "/" & CStr(lclsBudget.nYear))
		'		End If	
	End If
	
	'+ Se calcula la fecha final
	
	'    If lintMonth < 9 Then
	'        ldtmEndDate = CDate("01" & "/0" & CStr(lintMonth + 1) & "/" & CStr(lclsBudget.nYear))
	'    Else
	'        ldtmEndDate = CDate("01" & "/" & CStr(lintMonth) & "/" & CStr(lclsBudget.nYear))
	'    End If
	
	'+ Se leen los datos a mostrar en la zona de detalle
	
	Call lcolBudget_amo.Find(mobjValues.StringToType(Session("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), lclsBudget.nCurrency, lclsBudget.sBud_code, lclsBudget.nYear, lintMonth, ldtmInitDate, ldtmEndDate, mobjValues.StringToType(Request.QueryString.Item("optBalance"), eFunctions.Values.eTypeData.etdDate))
	
	If lcolBudget_amo.Count > 0 Then
		For	Each lclsBudget_amo In lcolBudget_amo
			With mobjGrid
				.Columns("tctAccount").DefValue = lclsBudget_amo.sAccount
				.Columns("tctAux_accoun").DefValue = lclsBudget_amo.sAux_accoun
				.Columns("tctCost_cente").DefValue = lclsBudget_amo.sCost_cente
				
				'+ Se calcula el saldo real
				
				ldblAmount = lclsBudget_amo.nDebit - lclsBudget_amo.nCredit
				
				'+ Se calcula la diferencia porcentual
				
				ldblDifPercent = ldblAmount - lclsBudget_amo.nBalance
				
				.Columns("tcnAmount").DefValue = CStr(ldblAmount)
				
				'+ Si la diferencia porcentual es negativa se le cambia el signo
				
				If ldblDifPercent < 0 Then
					ldblDifPercent = ldblDifPercent * -1
				End If
				
				'+ Si el valor del presupuesto es cero(0), el valor de la dif. porcentual es cero(0) también
				
				If lclsBudget_amo.nBalance = 0 Then
					ldblDifPercent = 0
				Else
					ldblDifPercent = ldblDifPercent * 100 / lclsBudget_amo.nBalance
				End If
				
				If lclsBudget_amo.nBalance = 0 Then
					.Columns("tcnBalance").DefValue = "0,00"
				Else
					.Columns("tcnBalance").DefValue = CStr(lclsBudget_amo.nBalance)
				End If
				
				If ldblDifPercent = 0 Then
					.Columns("tcnPercentageDif").DefValue = "0,00 %"
				Else
					.Columns("tcnPercentageDif").DefValue = ldblDifPercent & " %"
				End If
				
				Response.Write(.DoRow)
			End With
		Next lclsBudget_amo
	End If
	
	'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
	
	Response.Write(mobjGrid.CloseTable())
	
	lcolBudget_amo = Nothing
	lclsBudget_amo = Nothing
	lclsBudget = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "cpc003"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

	
<%
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="post" ID="FORM" NAME="CPC003" ACTION="valBudget.aspx">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("<BR>")

Call insDefineHeader()
Call insPreCPC003()
%>
	</FORM>
</BODY>
</HTML>

<%
mobjGrid = Nothing
mobjValues = Nothing
%>




