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
	
	mobjGrid.sCodisplPage = "op716"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRequestNuColumnCaption"), "tcnRequestNu", 10, CStr(0),  , GetLocalResourceObject("tcnRequestNuColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptColumnToolTip"))
		Call .AddClientColumn(0, GetLocalResourceObject("valBenefColumnCaption"), "valBenef", "",  , GetLocalResourceObject("valBenefColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountOriColumnCaption"), "tcnAmountOri", 18, CStr(0),  , GetLocalResourceObject("tcnAmountOriColumnToolTip"), True, 6)
        Call .AddTextColumn(0, GetLocalResourceObject("tctSTSResultCaption"), "tctSTSResult", 20, vbNullString,  , GetLocalResourceObject("tctSTSResultToolTip"))
            
		Call .AddHiddenColumn("nRequestNu", CStr(0))
	End With
	With mobjGrid
		.Codispl = "OP716"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreOP716: Se realiza el manejo del grid y se cargan los datos del Folder
'----------------------------------------------------------------------------------------------
Private Sub insPreOP716()
	'----------------------------------------------------------------------------------------------
	Dim lobjCheques As eCashBank.Cheques
    Dim lclsCheque As eCashBank.Cheque
	Dim ldtmEndDate As Date
	Dim ldtmStartDate As Date
	Dim lintCount As Integer
	
	lobjCheques = New eCashBank.Cheques
    lclsCheque = New eCashBank.Cheque
	
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
	
        If lobjCheques.InsPreOP716(ldtmStartDate, ldtmEndDate, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
		
            For lintCount = 1 To lobjCheques.Count
                With mobjGrid
				
                    .Columns("tcnRequestNu").DefValue = CStr(lobjCheques.Item(lintCount).nRequest_nu)
                    .Columns("cbeConcept").DefValue = CStr(lobjCheques.Item(lintCount).nConcept)
                    .Columns("valBenef").DefValue = lobjCheques.Item(lintCount).sClient
                    .Columns("tcnAmountOri").DefValue = CStr(lobjCheques.Item(lintCount).nAmount)
                    
                    lclsCheque.insNotificationNewCheque("CORPVIDA", "VISUALTIME", "Principal", _
                                                        lobjCheques.Item(lintCount).sClient, lobjCheques.Item(lintCount).sDigit, lobjCheques.Item(lintCount).sCliename, _
                                                        String.Empty, lobjCheques.Item(lintCount).dStat_date, lobjCheques.Item(lintCount).sRequest_ty, _
                                                        mobjValues.StringToType(lobjCheques.Item(lintCount).nBankExt, eFunctions.Values.eTypeData.etdDouble), lobjCheques.Item(lintCount).sBankAccount, mobjValues.StringToType(lobjCheques.Item(lintCount).nAcc_type, eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(lobjCheques.Item(lintCount).nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(lobjCheques.Item(lintCount).nAmountPay, eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(lobjCheques.Item(lintCount).nCurrencyPay, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lobjCheques.Item(lintCount).nConcept, eFunctions.Values.eTypeData.etdDouble), _
                                                        lobjCheques.Item(lintCount).sDescript, mobjValues.StringToType(lobjCheques.Item(lintCount).nExternal_Concept, eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(lobjCheques.Item(lintCount).nRequest_nu, eFunctions.Values.eTypeData.etdDouble), lobjCheques.Item(lintCount).sCheque, lobjCheques.Item(lintCount).nId_ExternalSystem,
                                                        mobjValues.StringToType(lobjCheques.Item(lintCount).nBranch, eFunctions.Values.eTypeData.etdInteger),mobjValues.StringToType(lobjCheques.Item(lintCount).nProduct, eFunctions.Values.eTypeData.etdInteger))
                    
                    .Columns("tctSTSResult").DefValue = CStr(lclsCheque.sMessage_sts)
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
	Response.Write(mobjMenu.setZone(2, "OP716", "OP716.aspx"))
End If

mobjValues.sCodisplPage = "op716"
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%Response.Write(mobjValues.ShowWindowsName("OP716"))
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OP716", "OP716.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAprove" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
Call insPreOP716()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




