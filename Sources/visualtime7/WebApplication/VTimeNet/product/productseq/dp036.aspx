<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mintCount As Integer


'% insDefineHeader: se definen las caracteristicas del grid
'----------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------------------------
	With mobjGrid.Columns
		Call .AddNumericColumn(41342, GetLocalResourceObject("tcnElementColumnCaption"), "tcnElement", 5, "",  , GetLocalResourceObject("tcnElementColumnToolTip"))
		Call .AddTextColumn(41346, GetLocalResourceObject("tctElementDescriptionColumnCaption"), "tctElementDescription", 30, "",  , GetLocalResourceObject("tctElementDescriptionColumnToolTip"))
		Call .AddTextColumn(41347, GetLocalResourceObject("tctShortDescriptionColumnCaption"), "tctShortDescription", 12, "",  , GetLocalResourceObject("tctShortDescriptionColumnToolTip"))
		Call .AddPossiblesColumn(41340, GetLocalResourceObject("cbeStateColumnCaption"), "cbeState", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeStateColumnToolTip"))
		Call .AddNumericColumn(100412, GetLocalResourceObject("tcnDisrateColumnCaption"), "tcnDisrate", 4, "",  , GetLocalResourceObject("tcnDisrateColumnToolTip"), True, 2)
		Call .AddPossiblesColumn(41341, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(41343, GetLocalResourceObject("tcnFixAmountColumnCaption"), "tcnFixAmount", 18, "",  , GetLocalResourceObject("tcnFixAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(41344, GetLocalResourceObject("tcnMinAmountColumnCaption"), "tcnMinAmount", 18, "",  , GetLocalResourceObject("tcnMinAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(41345, GetLocalResourceObject("tcnMaxAmountColumnCaption"), "tcnMaxAmount", 18, "",  , GetLocalResourceObject("tcnMaxAmountColumnToolTip"), True, 6)
		Call .AddTextColumn(41348, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, "",  , GetLocalResourceObject("tctRoutineColumnToolTip"))
		Call .AddHiddenColumn("tcnInitialSelection", CStr(0))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("tcnMainAction", "")
	End With
	
	With mobjGrid
		.Codispl = "DP036"
		.Codisp = "DP036"
		.Top = 100
		.Height = 410
		.Width = 350
		.Columns("tcnElement").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tctElementDescription").EditRecord = True
		.Columns("Sel").OnClick = "MarkRecord(this);"
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If Request.QueryString.Item("Action") = "Add" Then
			.Columns("tcnInitialSelection").DefValue = CStr(1)
		Else
			.Columns("tcnInitialSelection").DefValue = CStr(2)
		End If
	End With
End Sub

'% insPreDP036: se controlan los campos de la forma cuando no es PopUp
'----------------------------------------------------------------------------------------------------------------
Private Sub insPreDP036()
	'----------------------------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	
	lclsProduct = New eProduct.Product
	
	If lclsProduct.insReaDP036(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For mintCount = 1 To lclsProduct.CountItemDP036
			If lclsProduct.ItemDP036(mintCount) Then
				With mobjGrid
					.Columns("tcnElement").DefValue = CStr(lclsProduct.nElement)
					.Columns("tctElementDescription").DefValue = lclsProduct.sDescript
					.Columns("tctShortDescription").DefValue = lclsProduct.sShort_des
					.Columns("cbeState").DefValue = CStr(lclsProduct.sStatregt)
					.Columns("cbeCurrency").DefValue = CStr(lclsProduct.nCurrency)
					.Columns("tcnFixAmount").DefValue = CStr(lclsProduct.nDiscount)
					.Columns("tcnMinAmount").DefValue = CStr(lclsProduct.nDisminin)
					.Columns("tcnMaxAmount").DefValue = CStr(lclsProduct.nDismaxim)
					.Columns("tctRoutine").DefValue = lclsProduct.sRoutine
					.Columns("tcnDisrate").DefValue = CStr(lclsProduct.nDisrate)
					'+ Se "arma" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostDP036 cuando se eliminen los registros seleccionados - ACM - 18/04/2001
					.Columns("sParam").DefValue = "nAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Session("nBranch") & "&nElement=" & lclsProduct.nElement & "&nProduct=" & Session("nProduct") & "&dEffecdate=" & Session("dEffecdate") & "&nCurrency=" & lclsProduct.nCurrency & "&sDescript=" & lclsProduct.sDescript & "&nDiscount=" & lclsProduct.nDiscount & "&nDisMaxim=" & lclsProduct.nDismaxim & "&nDisMinin=" & lclsProduct.nDisminin & "&nDisrate=" & lclsProduct.nDisrate & "&dNulldate=" & lclsProduct.dNulldate & "&sShort_des=" & lclsProduct.sShort_des & "&sStatregt=" & lclsProduct.sStatregt & "&nUserCode=" & Session("nUsercode") & "&sRoutine=" & lclsProduct.sRoutine & "&nInitialSelection=1"
					
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	Response.Write(mobjGrid.CloseTable())
	lclsProduct = Nothing
End Sub

'% insPreDP036Upd: se controlan los campos de la forma cuando es PopUp
'----------------------------------------------------------------------------------------------
Private Sub insPreDP036Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsPost As eProduct.Product
	Dim lblnPost As Boolean
	Dim lintAction As String
	
	If Request.QueryString.Item("Action") = "Del" Then
		lintAction = "2"
		lclsPost = New eProduct.Product
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			lblnPost = lclsPost.insPostDP036(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nElement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDescript"), mobjValues.StringToType(.QueryString.Item("nDiscount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDisMaxim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDisMinin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDisrate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sShort_des"), .QueryString.Item("sStatregt"), mobjValues.StringToType(.QueryString.Item("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sRoutine"), mobjValues.StringToType(.QueryString.Item("nInitialSelection"), eFunctions.Values.eTypeData.etdDouble))
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End With
	End If
	
	lclsPost = Nothing
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP036", .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.actionQuery = Session("bQuery")
mobjGrid.actionQuery = Session("bQuery")

mobjGrid.sCodisplPage = "DP036"
mobjValues.sCodisplPage = "DP036"

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP036"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP036", "DP036.aspx"))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName("DP036")%>
<FORM METHOD="POST" ID="FORM" NAME="frmDP036" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP036()
Else
	Call insPreDP036Upd()
End If
%>    
</FORM>
</BODY>
</HTML>





