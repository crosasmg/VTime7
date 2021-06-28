<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolBills As eCollection.Billss


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co700a"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		'+ Modificar los parámetros "Title" y "FieldName" de cada columna
		Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"))
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valproduct",  ,  ,  ,  , True)
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, vbNullString,  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinsColumnCaption"), "tcnBulletins", 10, vbNullString,  , GetLocalResourceObject("tcnBulletinsColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContratColumnCaption"), "tcnContrat", 10, vbNullString,  , GetLocalResourceObject("tcnContratColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 10, vbNullString,  , GetLocalResourceObject("tcnDraftColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountAfeColumnCaption"), "tcnAmountAfe", 18, vbNullString,  , GetLocalResourceObject("tcnAmountAfeColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountExeColumnCaption"), "tcnAmountExe", 18, vbNullString,  , GetLocalResourceObject("tcnAmountExeColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountIvaColumnCaption"), "tcnAmountIva", 18, vbNullString,  , GetLocalResourceObject("tcnAmountIvaColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecDateColumnCaption"), "tcdEffecDate", CStr(Today),  , GetLocalResourceObject("tcdEffecDateColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", CStr(Today),  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		Call .AddHiddenColumn("tcnId", "0")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CO700"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.AddButton = False
		.DeleteButton = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCO700: se realiza el manejo del grid
'---------------------------------------------------------------------------------------------
Private Sub insPreCO700()
	'---------------------------------------------------------------------------------------------
	Dim lclsBills As eCollection.Bills
	Dim lintIndex As Short
	
	lclsBills = New eCollection.Bills
	mcolBills = New eCollection.Billss
	
	If mcolBills.Find_CO700(Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sDocType"), Request.QueryString.Item("sBillType"), mobjValues.StringToType(Request.QueryString.Item("nBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("dDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dDateEnd"), eFunctions.Values.eTypeData.etdDate), "2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		'+ Solo se carga el grid si el proceso es puntual
		If Request.QueryString.Item("sProcess") = "1" Then
			lintIndex = 0
			For	Each lclsBills In mcolBills
				With mobjGrid
					.Columns("Sel").OnClick = "insCheckSelClick(this,""" & CStr(lintIndex) & """,""" & Request.QueryString.Item("sKey") & """)"
					.Columns("Sel").checked = CShort(lclsBills.sSel)
					'+ Si se trata de una nota de crédito: SdocType = 2
					If Request.QueryString.Item("sDocType") = "2" Then
						.Columns("Sel").disabled = True
					Else
						.Columns("Sel").disabled = False
					End If
					.Columns("tcnId").DefValue = CStr(lclsBills.nId)
					.Columns("dtcClient").DefValue = lclsBills.sClient
					.Columns("cbeBranch").DefValue = CStr(lclsBills.nBranch)
					.Columns("valProduct").DefValue = CStr(lclsBills.nProduct)
					.Columns("tcnPolicy").DefValue = CStr(lclsBills.nPolicy)
					.Columns("tcnReceipt").DefValue = CStr(lclsBills.nReceipt)
					.Columns("tcnBulletins").DefValue = CStr(lclsBills.nBulletins)
					.Columns("tcnContrat").DefValue = CStr(lclsBills.nContrat)
					.Columns("tcnDraft").DefValue = CStr(lclsBills.nDraft)
					.Columns("cbeCurrency").DefValue = CStr(lclsBills.nCurrency)
					.Columns("tcnAmountAfe").DefValue = CStr(lclsBills.nAmo_afec)
					.Columns("tcnAmountExe").DefValue = CStr(lclsBills.nAmo_exen)
					.Columns("tcnAmountIva").DefValue = CStr(lclsBills.nIva)
					.Columns("tcdEffecdate").DefValue = CStr(lclsBills.dStatdate)
					.Columns("tcdExpirDat").DefValue = CStr(lclsBills.dExpirDat)
					'					.Columns("tcdBillAsig").DefValue = lclsBills.nBillnum
					lintIndex = lintIndex + 1
					Response.Write(.DoRow)
				End With
			Next lclsBills
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co700a")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co700a"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 4 $|$$Date: 10/03/04 16:12 $|$$Author: Nvaplat40 $"
	     
//% insCheckSelClick: Actualiza la columna sel del grid a la hora de seleccionar o desseleccionar un registro
//--------------------------------------------------------------------------------------------
function insCheckSelClick(Field, nIndex, sKey){
//--------------------------------------------------------------------------------------------
	insDefValues("ShowDataCO700", "sField=" + "UpdTmp_CO700sSel" + "&sKey=" + sKey + "&nId=" + marrArray[nIndex].tcnId + "&nIndex=" + nIndex + "&sSel=" + (Field.checked?'1':'0'))
}

</SCRIPT>

	<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO700A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO700" ACTION="valCollectionTra.aspx?sMode=2&<%=Request.Params.Get("Query_String")%>">

    <%Response.Write(mobjValues.ShowWindowsName("CO700A", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCO700()
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co700a")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




