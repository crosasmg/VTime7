<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(40425, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 4, CStr(0))
		Call .AddTextColumn(40430, GetLocalResourceObject("tctProductorColumnCaption"), "tctProductor", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(40426, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(40427, GetLocalResourceObject("tcnAmoCommColumnCaption"), "tcnAmoComm", 18, CStr(0),  ,  , True, 6)
		Call .AddTextColumn(40431, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(40432, GetLocalResourceObject("tctTratypeiColumnCaption"), "tctTratypei", 30, CStr(eRemoteDB.Constants.strnull))
		'+Cobro,Devolución
		Call .AddTextColumn(40433, GetLocalResourceObject("tctTypeColumnCaption"), "tctType", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(40434, GetLocalResourceObject("tctStatus_preColumnCaption"), "tctStatus_pre", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddDateColumn(40436, GetLocalResourceObject("tcdStatdateColumnCaption"), "tcdStatdate")
		Call .AddTextColumn(40435, GetLocalResourceObject("tctCard_TypeColumnCaption"), "tctCard_Type", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddPossiblesColumn(40422, GetLocalResourceObject("cbePay_FormColumnCaption"), "cbePay_Form", "Table182", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(40428, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 4, CStr(0))
		Call .AddDateColumn(40437, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate")
		Call .AddPossiblesColumn(40423, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType)
		Call .AddPossiblesColumn(40424, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdMaster1", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valProductColumnCaption"))
		Call .AddNumericColumn(40429, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 8, CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "COC006"
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
		.DeleteButton = False
		.AddButton = False
	End With
End Sub

'% insPreCOC006: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC006()
	'--------------------------------------------------------------------------------------------
	Dim lblnGridvisible As Boolean
	Dim lclsPremium As eCollection.Premium
	Dim lcolPremiums As eCollection.Premiums
	
	With Server
		lclsPremium = New eCollection.Premium
		lcolPremiums = New eCollection.Premiums
	End With
	If Request.QueryString.Item("nTypeSearch") = "2" Then
		lblnGridvisible = True
	Else
		lblnGridvisible = False
	End If
	
	If lcolPremiums.Find_IntermedCommiss_pr(Request.QueryString.Item("sUnderw"), Request.QueryString.Item("sRenew"), Request.QueryString.Item("sAll"), mobjValues.StringToType(Request.QueryString.Item("nReceiptListTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCardType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(CStr(Today)), mobjValues.StringToType(Request.QueryString.Item("nDays"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nSupCode"), eFunctions.Values.eTypeData.etdDouble), CStr(2), CStr(1), CShort(0), CShort(0)) Then '
		
		For	Each lclsPremium In lcolPremiums
			With mobjGrid
				.Columns("tcnReceipt").DefValue = CStr(lclsPremium.nReceipt)
				.Columns("tctProductor").GridVisible = lblnGridvisible
				.Columns("tctProductor").DefValue = lclsPremium.sCliename
				.Columns("tcnPremium").DefValue = CStr(lclsPremium.nPremium)
				.Columns("tcnAmoComm").DefValue = CStr(lclsPremium.nAmountP)
				.Columns("tctCurrency").DefValue = lclsPremium.sDescCurrency
				.Columns("tctTratypei").DefValue = lclsPremium.sDescTratypei
				.Columns("tctType").DefValue = lclsPremium.sDesType
				.Columns("tctStatus_pre").DefValue = lclsPremium.sDescStatus_pre
				.Columns("tcdStatdate").DefValue = CStr(lclsPremium.dStatDate)
				.Columns("tctCard_Type").DefValue = lclsPremium.sDescCard_type
				.Columns("cbePay_Form").DefValue = lclsPremium.sPay_form
				.Columns("tcnBordereaux").DefValue = mobjValues.TypeToString(lclsPremium.nBordereaux, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcdEffecdate").DefValue = CStr(lclsPremium.dEffecdate)
				.Columns("cbeBranch").DefValue = CStr(lclsPremium.nBranch)
				.Columns("valProduct").Parameters.Add("nBranch", lclsPremium.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = CStr(lclsPremium.nProduct)
				.Columns("tcnPolicy").DefValue = CStr(lclsPremium.nPolicy)
				Response.Write(.DoRow)
			End With
		Next lclsPremium
	End If
	Response.Write(mobjGrid.closeTable())
	
	'+ Se reasignan los valores del ancabezado de la forma
	With Response
		If Request.QueryString.Item("sUnderw") = "1" Then
			.Write("<SCRIPT>top.fraHeader.document.forms[0].chkUnderw.checked=true;</" & "Script>")
		End If
		If Request.QueryString.Item("chkRenew") = "1" Then
			.Write("<SCRIPT>top.fraHeader.document.forms[0].chkRenew.checked=true;</" & "Script>")
		End If
		If Request.QueryString.Item("chkAll") = "1" Then
			.Write("<SCRIPT>top.fraHeader.document.forms[0].chkAll.checked=true;</" & "Script>")
		End If
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeReceiptListTyp.value=" & Request.QueryString.Item("nReceiptListTyp") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & Request.QueryString.Item("nCurrency") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCardType.value=" & Request.QueryString.Item("nCardType") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnDays.value=" & Request.QueryString.Item("nDays") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdDate.value='" & Request.QueryString.Item("dDate") & "';</" & "Script>")
		'.Write "<NOTSCRIPT>top.fraHeader.document.forms[0].optClient.value='" & Request.QueryString("nTypeSearch") & "';</" & "Script>"
		If Request.QueryString.Item("nIntermed") <> vbNullString Then
			.Write("<SCRIPT>top.fraHeader.document.forms[0].valAgentCode.value=" & Request.QueryString.Item("nIntermed") & ";</" & "Script>")
		End If
		If Request.QueryString.Item("nSupCode") <> vbNullString Then
			.Write("<SCRIPT>top.fraHeader.document.forms[0].valSupCode.value=" & Request.QueryString.Item("nSupCode") & ";</" & "Script>")
		End If
	End With
	lclsPremium = Nothing
	lcolPremiums = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc006")
'With  Server
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc006"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "coc006"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
'End with
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "COC006", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCollectionQue.aspx?mode=2">
<TABLE WIDTH="100%">
    <BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COC006", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCOC006()%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc006")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




