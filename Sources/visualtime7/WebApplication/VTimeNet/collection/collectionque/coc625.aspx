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

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct", vbNullString)
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, vbNullString,  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContratColumnCaption"), "tcnContrat", 10, vbNullString,  , GetLocalResourceObject("tcnContratColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 10, vbNullString,  , GetLocalResourceObject("tcnDraftColumnToolTip"))
		Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdLimit_dateColumnCaption"), "tcdLimit_date", vbNullString,  , GetLocalResourceObject("tcdLimit_dateColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatus_preColumnCaption"), "cbeStatus_pre", "Table19", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatus_preColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "COC625"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% inspreCOC625: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub inspreCOC625()
	'--------------------------------------------------------------------------------------------
	Dim lclsAgreement As Object
	Dim lcolAgreement As eCollection.Agreements
	
	lcolAgreement = New eCollection.Agreements
	
	If lcolAgreement.Find_COC625(CInt(Request.QueryString.Item("nCod_agree")), mobjValues.StringToType(Request.QueryString.Item("dInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dEnd_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), CInt(Request.QueryString.Item("sTypeReceipt"))) Then
		For	Each lclsAgreement In lcolAgreement
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsAgreement.nBranch
				.Columns("valProduct").DefValue = lclsAgreement.nProduct
				.Columns("tcnPolicy").DefValue = lclsAgreement.nPolicy
				.Columns("tcnReceipt").DefValue = lclsAgreement.nReceipt
				.Columns("tcnContrat").DefValue = lclsAgreement.nContrat
				.Columns("tcnDraft").DefValue = lclsAgreement.nDraft
				.Columns("dtcClient").DefValue = lclsAgreement.sClient
				.Columns("cbeCurrency").DefValue = lclsAgreement.nCurrency
				.Columns("tcnAmount").DefValue = lclsAgreement.nAmount
				.Columns("tcdLimit_date").DefValue = lclsAgreement.dLimitdate
				.Columns("cbeStatus_pre").DefValue = lclsAgreement.nStatus_pre
				Response.Write(.DoRow)
			End With
		Next lclsAgreement
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsAgreement = Nothing
	lcolAgreement = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc625")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc625"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "coc625"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "COC625", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COC625" ACTION="valCollectionQue.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("COC625", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
Call inspreCOC625()
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc625")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




