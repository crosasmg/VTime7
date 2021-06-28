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
Dim mcolPremiums As eCollection.Premiums


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co633a"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "table10", 1, vbNullString,  ,  ,  ,  , "self.document.forms[0].valProduct.Parameters.Param1.sValue=this.value;", True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", 2,  , True,  ,  ,  ,  , True)
		mobjGrid.Columns("valProduct").Parameters.Add("nBranch", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString,  ,  , False,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString,  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, vbNullString,  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContratColumnCaption"), "tcnContrat", 10, vbNullString,  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, vbNullString,  ,  ,  ,  ,  ,  , "ShowDocument();", True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", 1,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 19, CStr(0),  ,  , True, 6,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStatDateColumnCaption"), "tcdStatDate", vbNullString,  , GetLocalResourceObject("tcdStatDateColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", vbNullString,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinsColumnCaption"), "tcnBulletins", 10, vbNullString,  , GetLocalResourceObject("tcnBulletinsColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdLimitDateColumnCaption"), "tcdLimitDate", vbNullString,  , GetLocalResourceObject("tcdLimitDateColumnToolTip"),  ,  ,  , True)
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CO633"
		.ActionQuery = True
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
	End With
End Sub

'% insPreCO633A: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCO633A()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As Object
	Dim lstrReceiptBulle As String
	
	mcolPremiums = New eCollection.Premiums
	
	If mcolPremiums.FindCO633(mobjValues.StringToType(Request.QueryString.Item("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dCollSus_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dCollSus_end"), eFunctions.Values.eTypeData.etdDate), True) Then
		lstrReceiptBulle = vbNullString
		Response.Write(mobjValues.HiddenControl("nItems", CStr(mcolPremiums.Count)))
		For	Each lclsPremium In mcolPremiums
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsPremium.nBranch
				.Columns("valProduct").Parameters.Add("nBranch", lclsPremium.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = lclsPremium.nProduct
				.Columns("tcnPolicy").DefValue = lclsPremium.nPolicy
				.Columns("tcnCertif").DefValue = lclsPremium.nCertif
				.Columns("tcnReceipt").DefValue = lclsPremium.nReceipt
				.Columns("tcnContrat").DefValue = lclsPremium.nContrat
				.Columns("tcnDraft").DefValue = lclsPremium.nDraft
				.Columns("cbeCurrency").DefValue = lclsPremium.nCurrency
				.Columns("tcnAmount").DefValue = lclsPremium.nAmount
				.Columns("tcdStatdate").DefValue = lclsPremium.dStatdate
				.Columns("tcdExpirdat").DefValue = lclsPremium.dExpirdat
				.Columns("tcnBulletins").DefValue = lclsPremium.nBulletins
				If lclsPremium.nBulletins <> eRemoteDB.Constants.intNull Then
					If lstrReceiptBulle = vbNullString Then
						If lclsPremium.nContrat > 0 Then
							lstrReceiptBulle = lclsPremium.nReceipt & "-" & lclsPremium.nDraft
						Else
							lstrReceiptBulle = lclsPremium.nReceipt
						End If
					Else
						If lclsPremium.nContrat > 0 Then
							lstrReceiptBulle = lstrReceiptBulle & ", " & lclsPremium.nReceipt & "-" & lclsPremium.nDraft
						Else
							lstrReceiptBulle = lstrReceiptBulle & ", " & lclsPremium.nReceipt
						End If
						
					End If
				End If
				.Columns("tcdLimitdate").DefValue = lclsPremium.dLimitdate
				Response.Write(.DoRow)
			End With
		Next lclsPremium
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("hddReceiptBulle", lstrReceiptBulle))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co633a")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co633a"
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


	<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO633A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO633A" ACTION="valCollectionTra.aspx?sMode=2&<%=Request.Params.Get("Query_String")%>">
    <%Response.Write(mobjValues.ShowWindowsName("CO633A", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCO633A()
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co633a")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




