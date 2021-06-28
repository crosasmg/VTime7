<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBudget" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsBudget As eBudget.Budget



'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "cp008"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuantityColumnCaption"), "tcnQuantity", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  ,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiferenceColumnCaption"), "tcnDiference", 18, CStr(0),  ,  , True, 6,  ,  ,  , True)
		'*********************************************************************		
		Call .AddHiddenColumn("tcnAuxMonth", CStr(0))
		Call .AddHiddenColumn("tcnAuxQuantity", CStr(0))
		Call .AddHiddenColumn("tcnAuxAmount", CStr(0))
		Call .AddHiddenColumn("tcnAuxDiference", CStr(0))
		Call .AddHiddenColumn("sAuxSel", "2")
		Call .AddHiddenColumn("chkAuxExist", CStr(2))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "CP008"
		.Width = 320
		.Height = 250
		.Top = 100
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
			.DeleteButton = False
			.Columns("Sel").GridVisible = False
			.AddButton = False
		Else
			.DeleteButton = True
			.Columns("Sel").GridVisible = True
			.AddButton = True
		End If
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		.Columns("tcnMonth").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
	
End Sub

'% insPreCP008: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP008()
	'--------------------------------------------------------------------------------------------
	Dim lclsBudget As eBudget.Budget
	Dim lintMonth As Object
	Dim lintValue As Object
	Dim lclsBudget_amo As eBudget.Budget_amo
	Dim lcolBudget_amos As eBudget.Budget_amos
	Dim lintIndex As Long
	Dim ldblQuantity As Object
	Dim ldblDiference As Byte
	
	With Server
		lclsBudget_amo = New eBudget.Budget_amo
		lcolBudget_amos = New eBudget.Budget_amos
	End With
	If lcolBudget_amos.Find_All(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), Session("sAccount"), Session("sAuxAccount"), Session("sCost_Cente")) Then
		lintIndex = 0
		For	Each lclsBudget_amo In lcolBudget_amos
			With mobjGrid
				.Columns("tcnMonth").DefValue = mobjValues.StringToType(CStr(lclsBudget_amo.nMonth), eFunctions.Values.eTypeData.etdDouble)
				ldblQuantity = lclsBudget_amo.insCalc_QuantityBudget(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("chktotAnnual"), Session("tcnAnnualBudget"))
				.Columns("tcnQuantity").DefValue = mobjValues.StringToType(CStr(lclsBudget_amo.nBalance), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAmount").DefValue = mobjValues.StringToType(ldblQuantity, eFunctions.Values.eTypeData.etdDouble)
				If lclsBudget_amo.nBalance = 0 Then
					ldblDiference = 0
				Else
					ldblDiference = lclsBudget_amo.insCalc_Diference(mobjValues.StringToType(CStr(lclsBudget_amo.nBalance), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldblQuantity, eFunctions.Values.eTypeData.etdDouble))
				End If
				.Columns("tcnDiference").DefValue = CStr(ldblDiference)
				'-------------------------------------------------------------------------------------------------------------------------------------------------------------------		    	    
				.Columns("tcnAuxMonth").DefValue = mobjValues.StringToType(CStr(lclsBudget_amo.nMonth), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxQuantity").DefValue = mobjValues.StringToType(CStr(lclsBudget_amo.nBalance), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxAmount").DefValue = mobjValues.StringToType(ldblQuantity, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxDiference").DefValue = CStr(ldblDiference)
				.Columns("chkAuxExist").Checked = 1
				
				mobjGrid.sDelRecordParam = "nMonth=' + marrArray[lintIndex].tcnMonth + '&nBalance' + marrArray[lintIndex].tcnAmount + '"
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
				ldblQuantity = 0
				ldblDiference = 0
			End With
		Next lclsBudget_amo
	Else
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
			lclsBudget = New eBudget.Budget
			lintMonth = lclsBudget.insCalc_Month(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble))
			lintValue = lintMonth
			Do While lintMonth > 0
				With mobjGrid
					.Columns("tcnMonth").DefValue = mobjValues.StringToType(lintMonth, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnQuantity").DefValue = CStr(0)
					.Columns("tcnAmount").DefValue = CStr(0)
					.Columns("tcnDiference").DefValue = CStr(0)
					'----------------------------------------------------------------------------------------------------------------------------		    	    
					.Columns("tcnAuxMonth").DefValue = mobjValues.StringToType(lintMonth, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnAuxQuantity").DefValue = CStr(0)
					.Columns("tcnAuxAmount").DefValue = CStr(0)
					.Columns("tcnAuxDiference").DefValue = CStr(0)
					.Columns("chkAuxExist").Checked = 2
					
					Response.Write(.DoRow)
					lintIndex = lintIndex + 1
					lintMonth = lintMonth - 1
					lintValue = lintValue + 1
				End With
			Loop 
		End If
	End If
	
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnLedCompan.value='" & Session("nLedCompan") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].optMonth.value='" & Session("optMonth") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnYearWork.value='" & Session("nYear") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrencyWork.value='" & Session("nCurrency") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valBudgetWork.value='" & Session("sBud_Code") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnYearComp.value='" & Session("tcnYearComp") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrencyComp.value='" & Session("cbeCurrencyComp") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valBudgetComp.value='" & Session("valBudgetComp") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAccount.value='" & Session("sAccount") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAux.value='" & Session("sAuxAccount") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tctDescript.value='" & Session("tctDescript") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valUnit.value='" & Session("sCost_Cente") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnAnnualBudget.value='" & Session("tcnAnnualBudget") & "';</" & "Script>")
	
	lclsBudget_amo = Nothing
	lcolBudget_amos = Nothing
End Sub

'% insPreCP008Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreCP008Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsBudget_amo As eBudget.Budget_amo
	Dim lclsErrors As eFunctions.Errors
	Dim lblnPost As Boolean
	Dim lstrSum As String
	
	If CStr(Session("optMonth")) = "1" Then
		lstrSum = "2"
	Else
		lstrSum = "1"
	End If
	lclsBudget_amo = New eBudget.Budget_amo
	lclsErrors = New eFunctions.Errors
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		lblnPost = lclsBudget_amo.insPostCP008(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "Delete", lstrSum, mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sBud_Code"), Session("sAccount"), Session("sAuxAccount"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBalance"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sCost_Cente"))
		
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valBudget.aspx", "CP008", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	
	lclsBudget_amo = Nothing
	lclsErrors = Nothing
End Sub

</script>
<%
Response.Expires = 0



With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	lclsBudget = New eBudget.Budget
End With


If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "cp008"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "CP008", "CP008.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insSelected(Field){
//---------------------------------------------------------------------------
    Field.checked = !Field.checked
}


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("CP008"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmBudInqUpd" ACTION="ValBudget.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
        <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCP008()
Else
	Call insPreCP008Upd()
End If

%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




