<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mlngAction As Object
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen las propiedades del grid
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------	
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "op714"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRequestNuColumnCaption"), "tcnRequestNu", 10, CStr(0),  , GetLocalResourceObject("tcnRequestNuColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeAgenColumnCaption"), "cbeOfficeAgen", "Table5556", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeAgenColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAgencyColumnCaption"), "cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAgencyColumnToolTip"))
		Call .AddClientColumn(0, GetLocalResourceObject("valBenefColumnCaption"), "valBenef", "",  , GetLocalResourceObject("valBenefColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyOriColumnCaption"), "cbeCurrencyOri", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyOriColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountOriColumnCaption"), "tcnAmountOri", 18, CStr(0),  , GetLocalResourceObject("tcnAmountOriColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_LocalColumnCaption"), "tcnAmount_Local", 18, CStr(0),  , GetLocalResourceObject("tcnAmount_LocalColumnToolTip"), True)
		Call .AddHiddenColumn("nRequestNu", CStr(0))
		Call .AddHiddenColumn("sCheque", CStr(0))
		Call .AddHiddenColumn("nConsec", CStr(0))
            Call .AddHiddenColumn("tcnSwitch", "")
            Call .AddHiddenColumn("nAcc_Bank", Request.QueryString.Item("nAcc_Bank"))
	End With
	With mobjGrid
		.Codispl = "OP714"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = mlngAction = eFunctions.Menues.TypeActions.clngActionUpdate
	End With
End Sub

'% insPreOP714: Se realiza el manejo del grid y se cargan los datos del Folder
'----------------------------------------------------------------------------------------------
Private Sub insPreOP714()
	'----------------------------------------------------------------------------------------------
	Dim lobjCheques As eCashBank.Cheques
	Dim ldtmEndDate As Date
	Dim ldtmStartDate As Date
	Dim lintCount As Integer
	
	lobjCheques = New eCashBank.Cheques
	
	If String.IsNullOrEmpty(Request.QueryString.Item("dStartDate")) Then
		ldtmStartDate = eRemoteDB.Constants.dtmNull
	Else
		ldtmStartDate = mobjValues.StringToType(Request.QueryString.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate)
	End If
	
	If String.IsNullOrEmpty(Request.QueryString.Item("dEndDate")) Then
		ldtmEndDate = eRemoteDB.Constants.dtmNull
	Else
		ldtmEndDate = mobjValues.StringToType(Request.QueryString.Item("dEndDate"), eFunctions.Values.eTypeData.etdDate)
	End If
	
	If lobjCheques.insPreOP714(mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble, True), ldtmStartDate, ldtmEndDate) Then
		
		For lintCount = 1 To lobjCheques.Count
			With mobjGrid
				If Request.QueryString.Item("nTypeOper") = "2" Then
					.Columns("Sel").Checked = 1
					.Columns("Sel").disabled = True
				End If
				
				.Columns("tcnRequestNu").DefValue = CStr(lobjCheques.Item(lintCount).nRequest_nu)
				.Columns("cbeConcept").DefValue = CStr(lobjCheques.Item(lintCount).nConcept)
				.Columns("cbeOffice").DefValue = lobjCheques.Item(lintCount).sDesOffice
				.Columns("cbeOfficeAgen").DefValue = lobjCheques.Item(lintCount).sDesOfficeAgen
				.Columns("cbeAgency").DefValue = lobjCheques.Item(lintCount).sDesAgency
				.Columns("valBenef").DefValue = lobjCheques.Item(lintCount).sClient
				.Columns("cbeCurrencyOri").DefValue = CStr(lobjCheques.Item(lintCount).nCurrencyOri)
				.Columns("tcnAmountOri").DefValue = CStr(lobjCheques.Item(lintCount).nAmount)
				.Columns("tcnAmount_Local").DefValue = CStr(lobjCheques.Item(lintCount).nAmount_Local)
				.Columns("sCheque").DefValue = lobjCheques.Item(lintCount).sCheque
				.Columns("nConsec").DefValue = CStr(lobjCheques.Item(lintCount).nConsec)
				.Columns("nRequestNu").DefValue = CStr(lobjCheques.Item(lintCount).nRequest_nu)
				.Columns("tcnSwitch").DefValue = CStr(1)
				Response.Write(mobjGrid.DoRow())
			End With
		Next 
	End If
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	mobjGrid = Nothing
	lobjCheques = Nothing
	Response.Write(mobjValues.HiddenControl("HddStartDate", Request.QueryString.Item("dStartDate")))
	Response.Write(mobjValues.HiddenControl("HddEndDate", Request.QueryString.Item("dEndDate")))
End Sub

</script>
<%Response.Expires = -1

mlngAction = Request.QueryString.Item("nMainAction")
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "OP714", "OP714.aspx"))
End If

mobjValues.sCodisplPage = "op714"
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%Response.Write(mobjValues.ShowWindowsName("OP714"))
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OP714", "OP714.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAprove" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
Call insPreOP714()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




