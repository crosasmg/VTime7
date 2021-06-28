<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "opc014"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddDateColumn(40187, GetLocalResourceObject("tcdOperdateColumnCaption"), "tcdOperdate")
		Call .AddPossiblesColumn(40181, GetLocalResourceObject("cbeMovementColumnCaption"), "cbeMovement", "Table137", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(40183, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  ,  , True, 6)
		'+ Código de la cuenta interna y el nombre del banco al que pertenece
		Call .AddTextColumn(40185, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(40184, GetLocalResourceObject("tcnRequest_nuColumnCaption"), "tcnRequest_nu", 9, CStr(0))
		Call .AddPossiblesColumn(40182, GetLocalResourceObject("cbeProposalColumnCaption"), "cbeProposal", "Table187", eFunctions.Values.eValuesType.clngComboType)
		Call .AddTextColumn(40186, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 10, CStr(eRemoteDB.Constants.strnull))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC014"
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
	End With
End Sub

'% insPreOPC010: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC014()
	'--------------------------------------------------------------------------------------------
	Dim lclsMove_acc As eCashBank.Move_acc
	Dim lcolMove_accs As eCashBank.Move_accs
	
	With Server
		lclsMove_acc = New eCashBank.Move_acc
		lcolMove_accs = New eCashBank.Move_accs
	End With
	If lcolMove_accs.Find_QPayOrderMov(mobjValues.StringToType(Request.QueryString.Item("nTyp_acco"), eFunctions.Values.eTypeData.etdInteger), Request.QueryString.Item("sType_acc"), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToDate(Request.QueryString.Item("dEffecdate"))) Then
		
		For	Each lclsMove_acc In lcolMove_accs
			With mobjGrid
				.Columns("tcdOperdate").DefValue = CStr(lclsMove_acc.dOperdate)
				.Columns("cbeMovement").DefValue = lclsMove_acc.sManualMov
				.Columns("tcnAmount").DefValue = CStr(lclsMove_acc.nAmount)
				.Columns("tctDescript").DefValue = lclsMove_acc.sAcc_number & " " & lclsMove_acc.sBank_des
				.Columns("tcnRequest_nu").DefValue = mobjValues.TypeToString(lclsMove_acc.nRequest_nu, eFunctions.Values.eTypeData.etdDouble)
				.Columns("cbeProposal").DefValue = CStr(lclsMove_acc.nSta_cheque)
				.Columns("tctCheque").DefValue = lclsMove_acc.sCheque
				Response.Write(.DoRow)
			End With
		Next lclsMove_acc
	End If
	Response.Write(mobjGrid.closeTable())
	
	'+ Se reasignan los valores del ancabezado de la forma
	With Response
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdEffecdate.value='" & Request.QueryString.Item("dEffecdate") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeTypeAccount.value=" & Request.QueryString.Item("nTyp_acco") & ";</" & "Script>")
		'.Write "<NOTSCRIPT>top.fraHeader.document.forms[0].cbeBussType.value=" & Request.QueryString("sType_acc") & ";</" & "Script>"
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeBussType.options[3].selected=true;</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].dtcClient.value='" & Request.QueryString.Item("sClient") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & Request.QueryString.Item("nCurrency") & ";</" & "Script>")
	End With
	lclsMove_acc = Nothing
	lcolMove_accs = Nothing
End Sub

</script>
<%Response.Expires = 0
With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "opc014"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC014", "OPC014.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?mode=2">
<TABLE WIDTH="100%">
    <BR></BR>
        <%Response.Write(mobjValues.ShowWindowsName("OPC014"))
Call insDefineHeader()
Call insPreOPC014()%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




