<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mdtmValuedate As Date
Dim mlngBranch As Integer
Dim mlngProduct As Integer
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 12.00.00
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values




'% insShowValuedate:Muestra la fecha de valorización por defecto según las condiciones
'--------------------------------------------------------------------------------------------
Sub insShowValuedate()
	'--------------------------------------------------------------------------------------------
	Dim llngConcept As Integer
	Dim lobjValdatconditions As eCashBank.Valdatconditions
	lobjValdatconditions = New eCashBank.Valdatconditions
	
	llngConcept = mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble, True)
	
	Call lobjValdatconditions.InsFind_ValdatconditionCollect(llngConcept, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, session("dCollectDate"))

	If lobjValdatconditions.dValueDate <> eRemoteDB.Constants.dtmnull Then
		mdtmValuedate = lobjValdatconditions.dValueDate
	Else
		mdtmValuedate = Today
	End If
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.value='" & mdtmValuedate & "';")
	
	If session("CO001_nAction") <> 2 Then
		If mdtmValuedate <> eRemoteDB.Constants.dtmnull Then
			If lobjValdatconditions.nChangesDat = 2 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=true;")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
			End If
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
		End If
	End If
	
	lobjValdatconditions = Nothing
End Sub


'% insConvertAmounting: Convierte un monto utilizando el factor de cambio.
'--------------------------------------------------------------------------------------------
Sub insConvertAmounting()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As Integer
	Dim nCurrency_ing As Integer
	Dim nAmount As Double
	Dim dReqDate As Date
	
	lclsExchanges = New eGeneral.Exchange
	
	
	'+Moneda de la relacion    
	nCurrency = session("nCurrencyCollect")
	'+Moneda del ingreso o moneda origen	
	nCurrency_ing = mobjValues.StringToType(Request.QueryString.Item("nCurrency_ing"), eFunctions.Values.eTypeData.etdDouble)
	'+Monto a convertir	
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
	
	If mdtmValuedate <> eRemoteDB.Constants.dtmnull Then
		dReqDate = mdtmValuedate
	Else
		If IsNothing(Request.QueryString.Item("dReqDate")) Then
			dReqDate = eRemoteDB.Constants.dtmnull
		Else
			dReqDate = mobjValues.StringToType(Request.QueryString.Item("dReqDate"), eFunctions.Values.eTypeData.etdDate)
		End If
	End If
	
	If Request.QueryString.Item("nAmount") = vbNullString Then
		nAmount = 0
	Else
		nAmount = mobjValues.StringToType(nAmount, eFunctions.Values.eTypeData.etdDouble, True)
	End If
	
	Call lclsExchanges.Convert(0, nAmount, nCurrency_ing, nCurrency, dReqDate, 0)
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
	Response.Write("top.frames['fraFolder'].nAmountPayLocJS='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lclsExchanges.pdblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	lclsExchanges = Nothing
End Sub

'% insConvertAmountingLoc: Convierte un monto utilizando el factor de cambio.
'--------------------------------------------------------------------------------------------
Sub insConvertAmountingLoc()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As Integer
	Dim nCurrency_ing As Integer
	Dim nAmount As Double
	Dim dReqDate As Date
	
	lclsExchanges = New eGeneral.Exchange
	
	
	'+Moneda de la relacion    
	nCurrency = session("nCurrencyCollect")
	'+Moneda del ingreso o moneda origen	
	nCurrency_ing = mobjValues.StringToType(Request.QueryString.Item("nCurrency_ing"), eFunctions.Values.eTypeData.etdDouble)
	'+Monto a convertir	
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
	
	If mdtmValuedate <> eRemoteDB.Constants.dtmnull Then
		dReqDate = mdtmValuedate
	Else
		If IsNothing(Request.QueryString.Item("dReqDate")) Then
			dReqDate = eRemoteDB.Constants.dtmnull
		Else
			dReqDate = mobjValues.StringToType(Request.QueryString.Item("dReqDate"), eFunctions.Values.eTypeData.etdDate)
		End If
	End If
	
	If Request.QueryString.Item("nAmount") = vbNullString Then
		nAmount = 0
	Else
		nAmount = mobjValues.StringToType(nAmount, eFunctions.Values.eTypeData.etdDouble, True)
	End If
	
	Call lclsExchanges.Convert(0, nAmount, nCurrency, nCurrency_ing, dReqDate, 0)
	
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountOrig.value='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Response.Write("top.frames['fraFolder'].nAmountPayJS='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lclsExchanges.pdblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	lclsExchanges = Nothing
End Sub



'%ShowCod_Agree: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
'------------------------------------------------------------------------------------------------------
Sub ShowCod_Agree()
	'------------------------------------------------------------------------------------------------------
	Dim sType_BankAgree As String
	Dim lclsQuery As eRemoteDB.Query
	Dim lclsBank_Acc As eCashBank.Bank_acc
	Dim lclsClient As eClient.Client
	
	lclsQuery = New eRemoteDB.Query
	lclsBank_Acc = New eCashBank.Bank_acc
	lclsClient = New eClient.Client
	
	If CDbl(Request.QueryString.Item("nConcept")) = 29 Then
		sType_BankAgree = 1
	Else
		sType_BankAgree = 2
	End If
	
	If lclsQuery.OpenQuery("Bank_Agree", "Distinct(nBank), nAccount, sClient", "nBank='" & Request.QueryString.Item("nBank_Agree") & "'" & " and sType_BankAgree='" & sType_BankAgree & "'") Then
		Response.Write("top.frames['fraFolder'].document.forms[0].valAccount_Agree.Parameters.Param1.sValue=" & lclsQuery.FieldToClass("nBank") & ";")
		Response.Write("top.frames['fraFolder'].document.forms[0].valAccount_Agree.value='" & lclsQuery.FieldToClass("nAccount") & "';")
		Response.Write("top.frames['fraFolder'].$('#valAccount_Agree').change();")
		If lclsQuery.FieldToClass("sClient") <> vbNullString Then
			If lclsClient.Find(lclsQuery.FieldToClass("sClient")) Then
				Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & lclsQuery.FieldToClass("sClient") & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient_Digit.value='" & lclsClient.sDigit & "';")
				Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & lclsClient.sCliename & """);")
			End If
		End If
	End If
	
	If IsNothing(Request.QueryString.Item("nBank_Agree")) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].valAccount_Agree.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient_Digit.value='';")
		Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"","""");")
		Response.Write("top.frames['fraFolder'].UpdateDiv(""valAccount_AgreeDesc"","""");")
	End If
	
	lclsQuery = Nothing
	lclsBank_Acc = Nothing
	lclsClient = Nothing
End Sub

'%ShowCod_Agree2: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
'------------------------------------------------------------------------------------------------------
Sub ShowCod_Agree2()
	'------------------------------------------------------------------------------------------------------
	Dim sType_BankAgree As String
	Dim lclsQuery As eRemoteDB.Query
	Dim lclsClient As eClient.Client
	
	lclsQuery = New eRemoteDB.Query
	lclsClient = New eClient.Client
	
	If CDbl(Request.QueryString.Item("sRelType")) = 3 Then
		sType_BankAgree = 2
	Else
		sType_BankAgree = 1
	End If
	
	If lclsQuery.OpenQuery("Bank_Agree", "Distinct(nBank), nAccount, sClient", "nBank='" & Request.QueryString.Item("nBank_Agree") & "'" & " and sType_BankAgree='" & sType_BankAgree & "'") Then
		Response.Write("top.frames['fraHeader'].document.forms[0].valBank_Agree.Parameters.Param1.sValue=" & lclsQuery.FieldToClass("nBank") & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valBank_Agree.value='" & lclsQuery.FieldToClass("nAccount") & "';")
		Response.Write("top.frames['fraHeader'].$('#valBank_Agree').change();")
	End If
	
	If IsNothing(Request.QueryString.Item("nBank_Agree")) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].valAccount_Agree.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""valAccount_AgreeDesc"","""");")
	End If
	
	lclsQuery = Nothing
	lclsClient = Nothing
End Sub


'%GetCase_Info: Obtiene la informacion necesaria de el caso
'------------------------------------------------------------------------------------------------------
Sub GetCase_Info()
	'------------------------------------------------------------------------------------------------------
	Dim lintCase_num As String
	Dim lintDeman_type As String
	Dim lclsClaim As eClaim.Claim
	
	lclsClaim = New eClaim.Claim
	
	lintCase_num = mobjValues.StringToType(Request.QueryString.Item("sCase_num"), eFunctions.Values.eTypeData.etdDouble)
	lintDeman_type = mobjValues.StringToType(Request.QueryString.Item("sDeman_type"), eFunctions.Values.eTypeData.etdDouble)
	
	
	Response.Write("top.frames['fraFolder'].document.forms[0].hddCase_num.value='" & lintCase_num & "';")
	Response.Write("top.frames['fraFolder'].document.forms[0].hddDeman_type.value='" & lintDeman_type & "';")
	
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].hddnProponum.value='" & lclsClaim.nPolicy & "';")
	End If
	
	lclsClaim = Nothing
End Sub

'% ValCash_dEffecdate:Valida si la caja esta abierta
'--------------------------------------------------------------------------------------------
Private Sub ValCash_dEffecdate()
	'--------------------------------------------------------------------------------------------
	Dim lstrMessage As String
	Dim lobjCash_Stat As eCashBank.Cash_stat
	Dim lclsGeneral As eGeneral.GeneralFunction
	lobjCash_Stat = New eCashBank.Cash_stat
	lclsGeneral = New eGeneral.GeneralFunction
	
	If Request.QueryString.Item("dEffecdate") <> vbNullString And CStr(session("nCashNum")) <> "" And session("nCashNum") <> 0 Then
		If lobjCash_Stat.valCash_statClosed(session("nCashNum"), session("dCollectDate")) Then
			lstrMessage = lclsGeneral.insLoadMessage(60129)
			'Response.Write "alert(""Err 60129:  " & lstrMessage & """);" 
		End If
	End If
	
	lobjCash_Stat = Nothing
	lclsGeneral = Nothing
End Sub


'% ValCashCO008:Valida si la caja esta abierta
'--------------------------------------------------------------------------------------------
Private Sub ValCashCO008()
	'--------------------------------------------------------------------------------------------
	Dim lstrMessage As String
	Dim lobjCash_Stat As eCashBank.Cash_stat
	Dim lclsGeneral As eGeneral.GeneralFunction
	lobjCash_Stat = New eCashBank.Cash_stat
	lclsGeneral = New eGeneral.GeneralFunction
	
	If CStr(session("nCashNum")) <> "" And session("nCashNum") <> 0 Then
		If Request.QueryString.Item("dEffecdate") <> vbNullString Then
			If lobjCash_Stat.valCash_statClosed(session("nCashNum"), session("dCollectDate")) Then
				lstrMessage = lclsGeneral.insLoadMessage(60129)
				Response.Write("alert(""Err 60129:  " & lstrMessage & """);")
			End If
		End If
	ElseIf Request.QueryString.Item("sTypPay") = "1" Or Request.QueryString.Item("sTypPay") = "2" Or Request.QueryString.Item("sTypPay") = "5" Or Request.QueryString.Item("sTypPay") = "10" Then 
		
		lstrMessage = lclsGeneral.insLoadMessage(60104)
		Response.Write("alert(""Err 60104:  " & lstrMessage & """);")
	End If
	
	lobjCash_Stat = Nothing
	lclsGeneral = Nothing
End Sub

'% insShowClient: se muestra la ventana de siniestros del cliente (PolicyClaims.aspx)
'%Enlace NovaRed.
'--------------------------------------------------------------------------------------------
Sub insShowClient()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	
	lclsClient = New eClient.Client
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	With Request
		'+ Se invoca la ventana PopUp que contiene todos los siniestros del cliente.     
		If Not lclsClient.Find(lstrClient) Then
			Response.Write("ShowPopUp(""/VTimeNet/Common/NRClientApp.aspx?sClient=" & lstrClient & "&sDigit=" & Request.QueryString.Item("sDigit") & "&sForm=" & Request.QueryString.Item("sForm") & "&nDeman_type=" & .QueryString.Item("nDeman_type") & """, ""ClientNR"", 500, 250,""no"",""no"",270,200);")
		End If
	End With
	lclsClient = Nothing
	
	
	
End Sub


'% insGetExchange: Se busca el factor de cambio.
'-----------------------------------------------------------------------------------
Private Sub insGetExchange()
	'-----------------------------------------------------------------------------------
	Dim lobjGeneral As eGeneral.Exchange
	Dim ldtmValuedate As Date
	
	lobjGeneral = New eGeneral.Exchange
	
	
	If Not IsNothing(Request.QueryString.Item("dValuedate")) Then
		ldtmValuedate = mobjValues.StringToType(Request.QueryString.Item("dValuedate"), eFunctions.Values.eTypeData.etdDate)
	Else
		ldtmValuedate = session("dValueDate")
	End If
	
	If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), ldtmValuedate) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='1';")
	End If
	
	If Request.QueryString.Item("sCodispl") = "CO001" Then
		
		Response.Write("if (top.frames['fraFolder'].document.forms[0].tcnAmountPay.value!=0){")
		Response.Write("top.frames['fraFolder'].nAmountPayJS=-1;")
		Response.Write("top.frames['fraFolder'].nAmountPayLocJS=-1;")
		
		Response.Write("if (top.frames['fraFolder'].nLastAmountModify=='1'){")
		Response.Write("top.frames['fraFolder'].insCalculateLocal(""AmountLoc""); }")
		Response.Write("else{")
		Response.Write("top.frames['fraFolder'].insCalculateLocal(""Amount""); }")
		Response.Write("}")
		
		Response.Write("if (top.frames['fraFolder'].document.forms[0].tcnInterest_rate.value!=0){")
		Response.Write("top.frames['fraFolder'].nInterestPayJS=-1;")
		Response.Write("top.frames['fraFolder'].nInterestPayLocJS=-1;")
		
		Response.Write("if (top.frames['fraFolder'].nLastAmountModify=='1'){")
		Response.Write("top.frames['fraFolder'].insCalculateLocal(""InterestLoc""); }")
		Response.Write("else{")
		Response.Write("top.frames['fraFolder'].insCalculateLocal(""Interest""); }")
		Response.Write("}")
		
	End If
	
	lobjGeneral = Nothing
End Sub

'% inscalExchange: Se busca el factor de cambio.
'-----------------------------------------------------------------------------------
Private Sub insCalExchange()
	'-----------------------------------------------------------------------------------
	Dim ldblResult As Double
	Dim lobjGeneral As eGeneral.Exchange
	
	lobjGeneral = New eGeneral.Exchange
	
	If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), session("dValueDate")) Then
		ldblResult = lobjGeneral.nExchange * mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
	Else
		ldblResult = 0
	End If
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	If ldblResult <> eRemoteDB.Constants.intNull Then
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='';")
	End If
	
	lobjGeneral = Nothing
End Sub

'% insfindReceipt: Se buscan los datos del recibo.
'-----------------------------------------------------------------------------------
Private Sub insfindReceipt()
	'-----------------------------------------------------------------------------------
	Dim lobjPremium As eCollection.Premium
	
	lobjPremium = New eCollection.Premium
	
	With lobjPremium
		If .Find("2", mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), 0, 0) Then
			Response.Write("opener.document.forms[0].cbeBranch.value=" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].valProduct.value=" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.$('#valProduct').change();")
			Response.Write("opener.document.forms[0].nOutStandPremium.value='" & mobjValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].cbeCurrency.value=" & mobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].nPremium.value='" & mobjValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].nPremiuml.value='" & mobjValues.TypeToString(.nBalance * .nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].nInterest.value='" & mobjValues.TypeToString(.nInt_mora, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			Response.Write("opener.document.forms[0].tcnPolicy.value=" & mobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].tcnProponum.value=" & mobjValues.TypeToString(.nBordereaux, eFunctions.Values.eTypeData.etdDouble) & ";")
		Else
			Response.Write("opener.document.forms[0].cbeBranch.value='0';")
			Response.Write("opener.document.forms[0].valProduct.value='0';")
			Response.Write("opener.document.forms[0].nOutStandPremium.value='0';")
			Response.Write("opener.document.forms[0].cbeCurrency.value='0';")
			Response.Write("opener.document.forms[0].tcnExchange.value='0';")
			Response.Write("opener.document.forms[0].nPremium.value='0';")
			Response.Write("opener.document.forms[0].nPremiuml.value='0';")
			Response.Write("opener.document.forms[0].nInterest.value='0';")
			Response.Write("opener.document.forms[0].tcnPolicy.value='0';")
			Response.Write("opener.document.forms[0].tcnProponum.value='0';")
		End If
	End With
	
	lobjPremium = Nothing
End Sub
'-----------------------------------------------------------------------------------
Private Sub insCashNumID()
	'-----------------------------------------------------------------------------------
	Dim lobjCashNumId As eCollection.CashBankAccMov
	lobjCashNumId = New eCollection.CashBankAccMov
	
	If mobjValues.StringToType(Request.QueryString.Item("nCashId"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		If lobjCashNumId.Find_CashNumId(CDbl(Request.QueryString.Item("nCashId")), session("dValuedate")) Then
			
			Response.Write("top.frames['fraFolder'].document.forms[0].nTypPay.value      ='" & mobjValues.TypeToString(lobjCashNumId.nTypPay, eFunctions.Values.eTypeData.etdLong) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].dDoc_date.value    ='" & mobjValues.TypeToString(lobjCashNumId.dDoc_date, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].sClient.value      ='" & lobjCashNumId.sClient & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nBankAcc.value     ='" & mobjValues.TypeToString(lobjCashNumId.nBankAcc, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nCurrency.value    ='" & mobjValues.TypeToString(lobjCashNumId.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value  ='" & mobjValues.TypeToString(lobjCashNumId.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nAmount.value      ='" & mobjValues.TypeToString(lobjCashNumId.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value ='" & mobjValues.TypeToString(lobjCashNumId.nAmountLoc, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			'Response.Write "top.frames['fraFolder'].document.forms[0].tcnAmountUF.value = '" & mobjValues.TypeToString(lobjCashNumId.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';"
			Response.Write("top.frames['fraFolder'].document.forms[0].nBank.value        ='" & mobjValues.TypeToString(lobjCashNumId.nBank, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].sDocNumber.value   ='" & lobjCashNumId.sDocNumber & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nTypCreCard.value  ='" & mobjValues.TypeToString(lobjCashNumId.nTypCreCard, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nIntermed.value    ='" & mobjValues.TypeToString(lobjCashNumId.nIntermed, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nTransac.value     ='" & mobjValues.TypeToString(lobjCashNumId.nTransac, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nChequeLocat.value ='" & mobjValues.TypeToString(lobjCashNumId.nChequeLocat, eFunctions.Values.eTypeData.etdDouble) & "';")
			
			session("nCashnumOrd") = lobjCashNumId.nCashNumOrd
			session("nReceiptOrd") = lobjCashNumId.nReceiptOrd
			session("nBranchOrd") = lobjCashNumId.nBranchOrd
			session("nProductOrd") = lobjCashNumId.nProductOrd
			
			'+Si viene tipo de pago es cheque o cheque a fecha se deshabilita el boton continuar            
			
			If lobjCashNumId.nTypPay = 2 Or lobjCashNumId.nTypPay = 10 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].chkContinue.checked = 0;")
				Response.Write("top.frames['fraFolder'].document.forms[0].chkContinue.disabled = true;")
			End If
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCashId.value  ='';")
			Response.Write(" alert('Error 36108: Número de comprobante de caja no se encuentra registrado en el sistema');")
			
		End If
	End If
	
	lobjCashNumId = Nothing
	
End Sub
'% insCalLocalAmount: Se Calcula el importe en la moneda local.
'-----------------------------------------------------------------------------------
Private Function insCalLocalAmount() As Object
	'-----------------------------------------------------------------------------------
	Dim ldblResult As Double
	Dim ldblResultUF As Double
	Dim ldblAmount As Double
	Dim lobjGeneral As eGeneral.Exchange
	lobjGeneral = New eGeneral.Exchange
	Select Case Request.QueryString.Item("sType")
		Case "Normal"
			If lobjGeneral.Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate)) Then
				ldblResult = lobjGeneral.nExchange * mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
			Else
				ldblResult = 0
			End If
			If CDbl(Request.QueryString.Item("nCurrency")) = 1 Then
				ldblResult = System.Math.Round(ldblResult, 0)
				ldblAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
				ldblAmount = System.Math.Round(ldblAmount, 0)
			End If
			If CDbl(Request.QueryString.Item("nCurrency")) = 1 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].nAmount.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].nAmount.value='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			End If
			
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			
			If Request.QueryString.Item("sCodispl") = "CO008" Then
				If lobjGeneral.Find(4, CDate(Request.QueryString.Item("dValDate"))) Then
					ldblResultUF = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True) / lobjGeneral.nExchange
				Else
					ldblResultUF = 0
				End If
				Response.Write("top.frames['fraFolder'].document.forms[0].nAmountDec.value='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				If CDbl(Request.QueryString.Item("nCurrency")) <> 4 Then
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountUF.value='" & mobjValues.TypeToString(ldblResultUF, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				Else
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountUF.value='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				End If
			End If
		Case "Amount"
                If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), Session("dValueDate")) Then
                    If Request.QueryString.Item("sCodispl") = "CO008" Then
                        ldblResult = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True) / lobjGeneral.nExchange
                    Else
                        ldblResult = lobjGeneral.nExchange * mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
                    End If
                    
                Else
                    ldblResult = 0
                End If
                If CDbl(Request.QueryString.Item("nCurrency")) = 1 Then
                    ldblResult = System.Math.Round(ldblResult, 0)
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
                    Response.Write("top.frames['fraFolder'].nAmountPayJS='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.frames['fraFolder'].nAmountPayLocJS='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Else
                    If Request.QueryString.Item("sCodispl") = "CO008" Then
                        ldblResult = System.Math.Round(ldblResult, 6)
                        Response.Write("top.frames['fraFolder'].document.forms[0].nAmount.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Else
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
                        Response.Write("top.frames['fraFolder'].nAmountPayJS='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.frames['fraFolder'].nAmountPayLocJS='" & mobjValues.TypeToString(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    
                    End If
                End If
            Case "Interest"
                If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), Session("dValueDate")) Then
                    ldblResult = lobjGeneral.nExchange * mobjValues.StringToType(Request.QueryString.Item("nInterest"), eFunctions.Values.eTypeData.etdDouble, True)
                Else
                    ldblResult = 0
                End If
                If CDbl(Request.QueryString.Item("nCurrency")) = 1 Then
                    ldblResult = System.Math.Round(ldblResult, 0)
                End If
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnInterestLoc.value='" & mobjValues.TypeToString(ldblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
                Response.Write("top.frames['fraFolder'].nInterestPayJS='" & mobjValues.TypeToString(Request.QueryString.Item("nInterest"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
        End Select
	lobjGeneral = Nothing
End Function

'% insfindBulletin: Se buscan los datos del Boletin.
'-----------------------------------------------------------------------------------
Private Sub insfindBulletin()
	'-----------------------------------------------------------------------------------
	Dim lobjBulletin As eCollection.Bulletin
	
	lobjBulletin = New eCollection.Bulletin
	
	With lobjBulletin
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nBulletin"), eFunctions.Values.eTypeData.etdDouble, True), True) Then
			Response.Write("opener.document.forms[0].sClient.value='" & .sClient & "';")
			Response.Write("opener.document.forms[0].dLimitDate.value='" & mobjValues.StringToType(CStr(.dLimit_pay), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("opener.document.forms[0].nAmount.value='" & mobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].cbeCurrency.value=" & mobjValues.StringToType(CStr(.nCurrency), eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("opener.document.forms[0].tcnAmountLoc.value='" & mobjValues.TypeToString(.nLocalamount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
		Else
			Response.Write("opener.document.forms[0].sClient.value=' ';")
			Response.Write("opener.document.forms[0].dLimitDate.value=' ';")
			Response.Write("opener.document.forms[0].nAmount.value='0';")
			Response.Write("opener.document.forms[0].cbeCurrency.value='0';")
			Response.Write("opener.document.forms[0].tcnExchange.value='0';")
			Response.Write("opener.document.forms[0].tcnAmountLoc.value='0';")
		End If
	End With
	
	lobjBulletin = Nothing
End Sub

'% insFindCO001_K: Se busca los datos de una relación
'-----------------------------------------------------------------------------------
Private Sub insFindCO001_K()
	'-----------------------------------------------------------------------------------
	Dim lobjProduct As Integer
	Dim lobjCollection As eCollection.ColformRef
	Dim lintCurrency As Integer
	Dim ldblAmount As Double
	
	lobjCollection = New eCollection.ColformRef
	
	With lobjCollection
		If .FindColFormRef(mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeRel_Type.value='" & .sRel_Type & "';")
			Response.Write("top.frames['fraHeader'].insSetOperation();")
			Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
			If .sStatus = "1" Then
				Response.Write("tctStatus.value='" & "Completa" & "';")
			ElseIf .sStatus = "2" Then 
				Response.Write("tctStatus.value='" & "Incompleta" & "';")
			Else
				Response.Write("tctStatus.value='" & "Anulada" & "';")
			End If
			session("sStatus") = .sStatus
			Response.Write("cbeInputTyp.value='" & mobjValues.TypeToString(.nInputtyp, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("cbeRel_Type.value='" & .sRel_Type & "';")
			Response.Write("dtcClient.value='" & .sClient & "';")
			Response.Write("dtcClient_Digit.value='" & .sDigit & "';")
			Response.Write("UpdateDiv(""lblCliename"",""" & .sCliename & """);")
			Response.Write("tcnPolicy.value='" & .nPolicy & "';")
			
			If .sInd_Annuity = "1" Then
				Response.Write("chkRentVital.checked=true;")
				Response.Write("chkRentVital.value=1;")
			Else
				Response.Write("chkRentVital.checked=false;")
				Response.Write("chkRentVital.value=2;")
			End If
			Response.Write("top.frames['fraHeader'].$('#tcnPolicy').change();")
			Response.Write("cbeBranch.value='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("valProduct.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("valProduct.value='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
			Response.Write("tcnCertif.value='" & mobjValues.TypeToString(.nCertif, eFunctions.Values.eTypeData.etdDouble) & "';")
			
			If mobjValues.StringToType(CStr(.nCollector), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
				Response.Write("valCollector.value='" & mobjValues.TypeToString(.nCollector, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("UpdateDiv(""valCollectorDesc"",""" & .sCollector_Name & """);")
			Else
				Response.Write("valCollector.value='';")
				Response.Write("UpdateDiv(""valCollectorDesc"",""" & " " & """);")
			End If
			
			Response.Write("tcdCollectDate.value='" & mobjValues.TypeToString(.dCollect, eFunctions.Values.eTypeData.etdDate) & "';")
			
			If .sRel_Type = "3" Then '+ Pago en Ventanilla
				Response.Write("tcdValueDate.value='" & .dValueDate & "';")
				Response.Write("cbeBank.value='" & mobjValues.TypeToString(.nBank, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("valBank_Agree.Parameters.Param1.sValue=" & mobjValues.TypeToString(.nBank, eFunctions.Values.eTypeData.etdDouble) & ";")
				Response.Write("valBank_Agree.value='" & mobjValues.TypeToString(.nAgreement, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("top.frames['fraHeader'].$('#valBank_Agree').change();")
				Response.Write("valAgreement.value='';")
				Response.Write("UpdateDiv('valAgreementDesc', '');")
			ElseIf .sRel_Type = "1" Then  '+ Descuento por planilla
				Response.Write("tcdValueDate.value='" & .dValueDate & "';")
				Response.Write("tcdCollect.value='" & mobjValues.TypeToString(.dCollectDate, eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("valAgreement.value='" & mobjValues.TypeToString(.nAgreement, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("top.frames['fraHeader'].$('#valAgreement').change();")
				Response.Write("cbeBank.value='';")
				Response.Write("valBank_Agree.value='';")
				Response.Write("UpdateDiv('valBank_AgreeDesc', '');")
			End If
			
			If .sRelOrigi = "1" Then
				Response.Write("optRelOrigi[0].checked=1;")
			Else
				Response.Write("optRelOrigi[1].checked=1;")
			End If
			Response.Write("}")
		End If
		
	End With
	
	lobjCollection = Nothing
End Sub

'% insFindDocuments: Se buscan los datos de los dIferentes documentos a procesar.
'-----------------------------------------------------------------------------------
Private Sub insFindDocuments(ByRef nTypDoc As Object, ByRef sCertype As String, ByRef nCertif As Double, ByRef nReceipt As Double, ByRef nContrat As Double)
	Dim llngCode As String
	'-----------------------------------------------------------------------------------
	Dim lobjGeneral As eGeneral.Exchange
	Dim lobjDocument As Object
	Dim lobjDraft As eFinance.FinanceDraft
	Dim ldblExchange As Double
	Dim lintCurrency As Integer
	Dim ldblAmount As Double
	Dim ldblAmountLoc As Integer
	Dim lblnFind As Boolean
	Dim lintTypDoc As Object
	Dim llngCertif As Object
	Dim lstrCertype As String
	Dim lobjProduct As eProduct.Product
	Dim lintBranch As Integer
	Dim lintBranch_Aux As Integer
	Dim lintProduct As Integer
	Dim lobjValdatconditions As eCashBank.Valdatconditions
	Dim ldtmValuedate As Object
	Dim lintId As String
	Dim lobjFinance As eFinance.FinanceDraft
	Dim lobjClient As eClient.Client
	Dim lblnFoundDraft As Boolean
	Dim lobjUl_Mov_Acc_Pol As ePolicy.ul_move_acc_pol
	
	lobjProduct = New eProduct.Product
	'+ Si el parámetro no tiene valor se toma el del QueryString.
	If nTypDoc < 0 Then
		nTypDoc = mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True)
	End If
	
	If sCertype <> vbNullString Then
		lstrCertype = sCertype
	Else
		lstrCertype = Request.QueryString.Item("sCertype")
	End If
	
	If nCertif < 0 Then
		llngCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
	Else
		llngCertif = 0
	End If
	
	'+ Si el parámetro no tiene valor se toma el del QueryString.
	If nReceipt < 0 Then
		nReceipt = mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble, True)
	End If
	
	'+ Si el parámetro no tiene valor se toma el del QueryString.
	If nContrat < 0 Then
		nContrat = mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble, True)
	End If
	
	
	lblnFind = False
	lintCurrency = 1
	ldblExchange = 1
	ldblAmount = 0
	ldblAmountLoc = 0
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsLoansClient As ePolicy.Policy
	Dim lclsPolicyClient As ePolicy.Policy
	Select Case nTypDoc
		Case 1, 2, 11, 12, 16 'Recibos/Cuotas,Cuenta individual,Reliquidación de prima,Prima renta privada
			
			lobjDocument = New eCollection.Premium
			With lobjDocument
				'+ Si el campo contrato no tiene valor; se trata como un recibo.                                
				If nContrat <= 0 Then
					If .Find("2", nReceipt, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), 0, 0) Then
						
						lintBranch = mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble)
						lintProduct = mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble)
						
						If lobjProduct.FindProduct_li(lintBranch, lintProduct, session("dValueDate"), True) Then
							
							If lobjProduct.nProdClas = 4 Then
								lobjUl_Mov_Acc_Pol = New ePolicy.ul_move_acc_pol
								'+ Origen
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[48].style.display='';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[49].style.display='';")
								
								'+ Fecha Original
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[52].style.display='';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[53].style.display='';")
								
								'+ Entidad finaciera
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[54].style.display='';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[55].style.display='';")
								
                                    '+ regimen tributario
                                    Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[60].style.display='';")
                                    Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[61].style.display='';")
								
								Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.disabled=false;")
								Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.disabled=false;")
								Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.disabled=false;")
								
								Response.Write("top.frames['fraFolder'].document.forms[0].btn_tcdOriginDate.disabled=false;")
								Response.Write("top.frames['fraFolder'].document.forms[0].btnvalInstitution.disabled=false;")
                                    
                                    Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_Profit.disabled=false;")
                                    Response.Write("top.frames['fraFolder'].document.forms[0].btncbeTyp_Profit.disabled=false;")
                                    
								If lobjUl_Mov_Acc_Pol.Find_Ul_Move_Acc_PolCollect(.sCertype, .nBranch, .nProduct, .nPolicy, nCertif, nReceipt, session("dCollectDate")) Then
									Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdLong) & "';")
									Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.Parameters.Param2.sValue='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdLong) & "';")
									Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.value='" & mobjValues.TypeToString(lobjUl_Mov_Acc_Pol.nOrigin, eFunctions.Values.eTypeData.etdLong) & "';")
									Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.value='" & mobjValues.TypeToString(lobjUl_Mov_Acc_Pol.dDate_Origin, eFunctions.Values.eTypeData.etdDate) & "';")
									Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.value='" & mobjValues.TypeToString(lobjUl_Mov_Acc_Pol.nInstitution, eFunctions.Values.eTypeData.etdLong) & "';")
									Response.Write("top.frames['fraFolder'].$('#valOrigin').change();")
									Response.Write("top.frames['fraFolder'].$('#valInstitution').change();")
								End If
								lobjUl_Mov_Acc_Pol = Nothing
							Else
								'+ Origen
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[48].style.display='none';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[49].style.display='none';")
								
								'+ Fecha Original
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[52].style.display='none';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[53].style.display='none';")
								
								'+ Entidad finaciera
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[54].style.display='none';")
								Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[55].style.display='none';")
								
								Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.disabled=true;")
								Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.disabled=true;")
								Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.disabled=true;")
								
								Response.Write("top.frames['fraFolder'].document.forms[0].btn_tcdOriginDate.disabled=true;")
								Response.Write("top.frames['fraFolder'].document.forms[0].btnvalInstitution.disabled=true;")
								
								Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.value='';")
								Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.value='';")
								Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.value='';")
								
								Response.Write("top.frames['fraFolder'].UpdateDiv(""valInstitutionDesc"",""" & " " & """);")
							End If
						Else
							Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.disabled=true;")
							Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.disabled=true;")
							Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.disabled=true;")
							
							Response.Write("top.frames['fraFolder'].document.forms[0].btn_tcdOriginDate.disabled=true;")
							Response.Write("top.frames['fraFolder'].document.forms[0].btnvalInstitution.disabled=true;")
							
							Response.Write("top.frames['fraFolder'].document.forms[0].valOrigin.value='';")
							Response.Write("top.frames['fraFolder'].document.forms[0].tcdOriginDate.value='';")
							Response.Write("top.frames['fraFolder'].document.forms[0].valInstitution.value='';")
							
							Response.Write("top.frames['fraFolder'].UpdateDiv(""valInstitutionDesc"",""" & " " & """);")
						End If
						
						Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
						Response.Write("cbeBranch.value='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("valProduct.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("valProduct.value='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("top.frames['fraFolder'].$('#valProduct').change();")
						Response.Write("tcnPolicy.value='" & mobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("tcnPolicy.disabled=true;")
						Response.Write("tcnCertif.value='" & mobjValues.TypeToString(.nCertif, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("dtcClient.value='" & .sClient & "';")
						Response.Write("dtcClient_Digit.value='" & .sDigit & "';")
						Response.Write("hdddExpirDat.value='" & mobjValues.StringToType(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("hddnType.value='" & .nType & "';")
						Response.Write("hddnBulletins_aux.value='" & mobjValues.TypeToString(.nBulletins, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("hddnProponum_aux.value='" & mobjValues.TypeToString(.nProponum, eFunctions.Values.eTypeData.etdDouble) & "';")
						'+ Si el tipo de movimiento es devolución (hddnType=2)se deshabilita el campo de intereses.
						If .nType = 2 Then
							Response.Write("tcnInterest_rate.disabled=true;")
						End If
						
						'+ Si el área de seguros es Vida se permite realizar pagos parciales a los recibos
						If mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
							Response.Write("tcnAmountPay.disabled=false;")
						Else
							Response.Write("tcnAmountPay.disabled=true;")
						End If
						
						lblnFind = True
						'+ Se verIfica si dicho recibo posee giros.
						If .nContrat > 0 Then
							nContrat = .nContrat
							'+ Si el tipo de documento es recibo y dicho recibo tiene contrato se cambia a 2) Cuota de financiamiento.
							Response.Write("hddnContrat.value=" & mobjValues.TypeToString(.nContrat, eFunctions.Values.eTypeData.etdDouble) & ";")
							Response.Write("hddnReceipt.value=" & mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble) & ";")
							If nTypDoc = 1 Then
								Response.Write("cbeCollecDocTyp.value='2';")
							End If
							lblnFoundDraft = True
						Else
							'+ Si el tipo de documento es cuota y dicho recibo no tiene contrato se cambia a 1) Recibos.
							If nTypDoc = 2 Then
								Response.Write("cbeCollecDocTyp.value='1';")
							End If
							Response.Write("tcnDraft.disabled=true;")
							
							ldblAmount = .nBalance
							lintCurrency = .nCurrency
						End If
						Response.Write("}")
						Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & mobjValues.HTMLDecode(.sCliename) & """);")
					End If
				Else
					lblnFoundDraft = True
				End If
				
				If lblnFoundDraft Then
					'+ Si es cuota no se permite realizar pagos parciales.
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountPay.disabled=true;")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmountLoc.disabled=true;")
					lobjDraft = New eFinance.FinanceDraft
					With lobjDraft
						'+ Si el campo contrato tiene valor.  
						If nContrat > 0 Then
							If .Find(nContrat, mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble), True) Then
								
								'+ Si el estado de la cuota es Pendiente se procesa sino, no.
								If .nStat_draft = 1 Then
									lblnFind = True
									
									
									
									ldblAmount = .nAmount
									lintCurrency = .nCurrency
									
									Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
									If nTypDoc = 1 Then
										Response.Write("cbeCollecDocTyp.value='2';")
									End If
									Response.Write("tcnDocument.value=" & nContrat & ";")
									Response.Write("tcnDraft.value='" & mobjValues.TypeToString(.nDraft, eFunctions.Values.eTypeData.etdDouble) & "';")
									Response.Write("hddnContrat.value='" & nContrat & "';")
									Response.Write("hddnReceipt.value='" & mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble, False, 0) & "';")
									Response.Write("tcnDraft.disabled=false;")
									Response.Write("hdddExpirDat.value='" & mobjValues.StringToType(CStr(.dExpirdat), eFunctions.Values.eTypeData.etdDate) & "';")
									Response.Write("hddnBulletins_aux.value='" & mobjValues.TypeToString(.nBulletins, eFunctions.Values.eTypeData.etdDouble) & "';")
									Response.Write("dtcClient.disabled=true;")
									Response.Write("dtcClient_Digit.value='" & .sDigit & "';")
									Response.Write("dtcClient.value='" & .sClient & "';")
									Response.Write("}")
									Response.Write("top.frames['fraFolder'].mintDraft='" & mobjValues.TypeToString(.nDraft, eFunctions.Values.eTypeData.etdDouble) & "';")
									Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & .sIntermName & """);")
								End If
							Else
								Response.Write("top.frames['fraFolder'].document.forms[0].tcnDraft.value=0;")
							End If
						End If
					End With
					lobjDraft = Nothing
				End If
			End With
		Case 3 'Boletín
			lobjDocument = New eCollection.Bulletin
			With lobjDocument
				If .Find(mobjValues.StringToType(Request.QueryString.Item("nBulletin"), eFunctions.Values.eTypeData.etdDouble, True), True) Then
					lblnFind = True
					ldblAmount = .nAmount
					lintCurrency = .nCurrency
					Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & mobjValues.TypeToString(.sClient, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].$('#dtcClient').change();")
					Response.Write("top.frames['fraFolder'].document.forms[0].hddnBulletins_aux.value='" & mobjValues.StringToType(Request.QueryString.Item("nBulletin"), eFunctions.Values.eTypeData.etdDouble, True) & "';")
				End If
			End With
			
		Case 4, 5, 9, 24 'Prima adicional, prima exceso , Reliquidación de primas y Abono a polizas.
			
			lclsPolicy = New ePolicy.Policy
			lobjDocument = New eCollection.T_DocTyp
			With lobjDocument
				lintId = .getT_DocTypId(mobjValues.StringToType(session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True))
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnDocument.value='" & lintId & "';")
				
				If lclsPolicy.FindPolicyClient("2", mobjValues.StringToType(session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nCertif"), eFunctions.Values.eTypeData.etdDouble), session("dCollectDate")) Then
					
					Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & lclsPolicy.sClient & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient_Digit.value='" & lclsPolicy.sDigit & "';")
					Response.Write("top.frames['fraFolder'].UpdateDiv('lblCliename','" & Replace(lclsPolicy.sCliename, "'", "") & "');")
					lintCurrency = lclsPolicy.nCurrency
					lblnFind = True
				End If
			End With
			lclsPolicy = Nothing
			
		Case 6 'Préstamos        
			lobjDocument = New ePolicy.Loans
			With lobjDocument
				
				llngCode = mobjValues.StringToType(Request.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble, True)
				
				If .Find_Rel(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), llngCode, True) Then
					lblnFind = True
					ldblAmount = .nBalance
					lintCurrency = .nCurrency
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("tcnDocument.value='" & llngCode & "';")
					
					lclsLoansClient = New ePolicy.Policy
					
					If lclsLoansClient.FindPolicyClient("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), session("dCollectDate")) Then
						Response.Write("dtcClient.value='" & lclsLoansClient.sClient & "';")
						Response.Write("dtcClient_Digit.value='" & lclsLoansClient.sDigit & "';")
						Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & lclsLoansClient.sCliename & """);")
					End If
					lclsLoansClient = Nothing
					
					Response.Write("}")
				End If
			End With
			
		Case 7 'Propuesta            
			lobjDocument = New eCollection.Premium
			With lobjDocument
				If .Find_PremiumProp(lstrCertype, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), True) Then
					lblnFind = True
					ldblAmount = .nBalance
					lintCurrency = .nCurrency
					
					'+ En caso de que la propuesta este financiada
					If .nContrat > 0 Then
						lobjFinance = New eFinance.FinanceDraft
						'+ Se obtiene los datos de la primera cuota.
						If lobjFinance.Find(.nContrat, 1, True) Then
							ldblAmount = lobjFinance.nAmount
							lintCurrency = lobjFinance.nCurrency
						End If
						lobjFinance = Nothing
					End If
					
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("cbeBranch.value='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("valProduct.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("valProduct.value='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].$('#valProduct').change();")
					Response.Write("hddnProponum_aux.value='" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True) & "';")
					Response.Write("tcnCertif.value='" & mobjValues.TypeToString(.nCertif, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("cbeCurrency.value='" & mobjValues.TypeToString(lintCurrency, eFunctions.Values.eTypeData.etdLong) & "';")
					Response.Write("cbeBranch.disabled=true;")
					Response.Write("valProduct.disabled=true;")
					Response.Write("btnvalProduct.disabled=true;")
					Response.Write("tcnPolicy.disabled=true;")
					Response.Write("tcnCertif.disabled=true;")
					
					lclsPolicyClient = New ePolicy.Policy
					
					
					If lclsPolicyClient.FindPolicyClient(lstrCertype, mobjValues.StringToType(.nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.nCertif, eFunctions.Values.eTypeData.etdDouble), session("dCollectDate")) Then
						Response.Write("dtcClient.value='" & lclsPolicyClient.sClient & "';")
						Response.Write("dtcClient_Digit.value='" & lclsPolicyClient.sDigit & "';")
						Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & lclsPolicyClient.sCliename & """);")
						If lclsPolicyClient.sClient <> "" Then
							Response.Write("dtcClient.disabled=true;")
							Response.Write("dtcClient_Digit.disabled=true;")
							Response.Write("btndtcClient.disabled=true;")
						Else
							Response.Write("dtcClient.disabled=false;")
							Response.Write("dtcClient_Digit.disabled=false;")
							Response.Write("btndtcClient.disabled=false;")
						End If
					End If
					lclsPolicyClient = Nothing
					
					If ldblAmount = eRemoteDB.Constants.intNull Then
						Response.Write("tcnAmountPay.disabled=false;")
						ldblAmount = 0
					Else
						If mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
							Response.Write("tcnAmountPay.disabled=false;")
						Else
							Response.Write("tcnAmountPay.disabled=true;")
						End If
					End If
					Response.Write("hddnType.value='" & .nType & "';")
					Response.Write("}")
				Else
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("cbeBranch.value='';")
					Response.Write("valProduct.value='';")
					Response.Write("tcnCertif.value='';")
					Response.Write("cbeBranch.disabled=false;")
					Response.Write("valProduct.disabled=false;")
					Response.Write("btnvalProduct.disabled=false;")
					Response.Write("tcnCertif.disabled=false;")
					Response.Write("dtcClient.disabled=false;")
					Response.Write("dtcClient_Digit.disabled=false;")
					Response.Write("btndtcClient.disabled=false;")
					Response.Write("dtcClient_Digit.value='';")
					Response.Write("dtcClient.value='';")
					Response.Write("top.frames['fraFolder'].UpdateDiv('lblCliename','');")
					
					If mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
						Response.Write("tcnAmountPay.disabled=false;")
					Else
						Response.Write("tcnAmountPay.disabled=true;")
					End If
					Response.Write("hddnType.value='1';")
					Response.Write("}")
				End If
			End With
			
			'+ Abono APV - Traspasos APV - Transferencias APV
		Case 18, 19, 20
			If Not IsNothing(Request.QueryString.Item("nBranch")) And Not IsNothing(Request.QueryString.Item("nProduct")) Then
				lobjDocument = New eCollection.T_DocTyp
				With lobjDocument
					lintId = .getT_DocTypId(mobjValues.StringToType(session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True))
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnDocument.value='" & lintId & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnDocument.disabled=true;")
					lblnFind = True
				End With
			End If
			
			'+ Abono Propuesta APV - Traspasos Propuesta APV - Transferencias Propuesta APV
		Case 21, 22, 23
			If Not IsNothing(Request.QueryString.Item("nProponum")) Then
				lobjDocument = New eCollection.Premium
				With lobjDocument
					If .Find_PremiumProp(lstrCertype, mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble, True), True) Then
						lblnFind = True
						ldblAmount = .nBalance
						lintCurrency = .nCurrency
						
						'+ En caso de que la propuesta este financiada
						If .nContrat > 0 Then
							lobjFinance = New eFinance.FinanceDraft
							'+ Se obtiene los datos de la primera cuota.
							If lobjFinance.Find(.nContrat, 1, True) Then
								ldblAmount = lobjFinance.nAmount
								lintCurrency = lobjFinance.nCurrency
							End If
							lobjFinance = Nothing
						End If
						lobjClient = New eClient.Client
						
						Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
						Response.Write("hddnExist.value='1';")
						Response.Write("cbeBranch.value='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("valProduct.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("valProduct.value='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("top.frames['fraFolder'].$('#valProduct').change();")
						Response.Write("hddnProponum_aux.value='" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True) & "';")
						Response.Write("tcnCertif.value='" & mobjValues.TypeToString(.nCertif, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("dtcClient.value='" & lobjClient.ExpandCode(mobjValues.TypeToString(.sClient, eFunctions.Values.eTypeData.etdDouble)) & "';")

						Response.Write("valOrigin.Parameters.Param1.sValue='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdLong) & "';")
						Response.Write("valOrigin.Parameters.Param2.sValue='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdLong) & "';")
						Response.Write("valOrigin.value='" & mobjValues.TypeToString(.nOrigin, eFunctions.Values.eTypeData.etdLong) & "';")
						Response.Write("top.frames['fraFolder'].$('#valOrigin').change();")
                            
						Response.Write("cbeBranch.disabled=true;")
						Response.Write("valProduct.disabled=true;")
						Response.Write("btnvalProduct.disabled=true;")
						Response.Write("tcnPolicy.disabled=true;")
						Response.Write("tcnCertif.disabled=true;")
						If mobjValues.TypeToString(.sClient, eFunctions.Values.eTypeData.etdDouble) <> "" Then
							Response.Write("dtcClient.disabled=true;")
							Response.Write("dtcClient_Digit.disabled=true;")
							Response.Write("btndtcClient.disabled=true;")
						Else
							Response.Write("dtcClient.disabled=false;")
							Response.Write("dtcClient_Digit.disabled=false;")
							Response.Write("btndtcClient.disabled=false;")
						End If
						Response.Write("dtcClient_Digit.value='" & .sDigit & "';")
						Response.Write("top.frames['fraFolder'].$('#dtcClient_Digit').change();")
						lobjClient = Nothing
						If ldblAmount = eRemoteDB.Constants.intNull Then
							Response.Write("tcnAmountPay.disabled=false;")
							ldblAmount = 0
						Else
							If mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
								Response.Write("tcnAmountPay.disabled=false;")
							Else
								Response.Write("tcnAmountPay.disabled=true;")
							End If
						End If
						Response.Write("hddnType.value='" & .nType & "';")
						Response.Write("}")
					Else
						Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
						Response.Write("hddnExist.value='0';")
						Response.Write("cbeBranch.disabled=false;")
						Response.Write("valProduct.disabled=false;")
						Response.Write("btnvalProduct.disabled=false;")
						Response.Write("tcnCertif.disabled=false;")
						Response.Write("tcnCertif.value='0';")
						Response.Write("dtcClient.disabled=false;")
						Response.Write("dtcClient_Digit.disabled=false;")
						Response.Write("dtcClient.value='';")
						Response.Write("top.frames['fraFolder'].$('#dtcClient').change();")
						Response.Write("btndtcClient.disabled=false;")
						Response.Write("tcnAmountPay.disabled=false;")
						Response.Write("hddnType.value='1';")
						Response.Write("}")
					End If
				End With
			End If
	End Select
	
	'+ Si se encontró información se procede a realizar el cálculo según el factor de cambio a la fecha de valorización.
	If lblnFind Then
		If nTypDoc = 7 Or nTypDoc = 18 Or nTypDoc = 19 Or nTypDoc = 20 Or nTypDoc = 21 Or nTypDoc = 22 Or nTypDoc = 23 Then
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.disabled=true;")
		End If
		
            If Request.QueryString.Item("dValuedateProp") <> "" And (nTypDoc = 7 Or nTypDoc = 21) Then
                                
                If Not mobjValues.StringToType(Request.QueryString.Item("dValuedateProp"), eFunctions.Values.eTypeData.etdDate, True) Is Nothing Then
                    ldtmValuedate = Request.QueryString.Item("dValuedateProp")
                Else
                    ldtmValuedate = Session("dValueDate")
                End If
                
            Else
                '+ Se buscan las condiciones para mostrar la fecha de valorización por defecto
                lobjValdatconditions = New eCashBank.Valdatconditions
		
                If lintBranch > 0 Then
                    lintBranch_Aux = lintBranch
                Else
                    lintBranch_Aux = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True)
                End If
		
                If lobjValdatconditions.InsFind_ValdatconditionCollect(eRemoteDB.Constants.intNull, lintBranch_Aux, nTypDoc, Session("dCollectDate")) Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.value='" & mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate) & "';")
                    If lobjValdatconditions.nChangesDat = 1 Or lobjValdatconditions.nChangesDat = 3 Or lobjValdatconditions.nChangesDat = 4 Then
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
                    Else
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=true;")
                    End If
                End If
		
                If lobjValdatconditions.dValueDate = dtmNull Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.value='" & Session("dValueDate") & "';")
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
                    ldtmValuedate = Session("dValueDate")
                Else
                    ldtmValuedate = mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate)
                End If
            End If
            If nTypDoc <> 18 And nTypDoc <> 19 And nTypDoc <> 20 Then
                lobjGeneral = New eGeneral.Exchange
                '+ Se calcula factor de cambio de acuerdo a fecha de valorización.
                If lintCurrency = 1 Then
                    ldblExchange = 1
                Else
                    If lobjGeneral.Find(lintCurrency, ldtmValuedate) Then
                        ldblExchange = lobjGeneral.nExchange
                    Else
                        ldblExchange = 1
                    End If
                End If
			
                ldblAmountLoc = mobjValues.TypeToString(System.Math.Round(ldblAmount * ldblExchange, 0), eFunctions.Values.eTypeData.etdDouble, True, 0)
			
                Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
                Response.Write("tcnAmountCol.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("cbeCurrency.value='" & lintCurrency & "';")
                Response.Write("tcnExchange.value='" & mobjValues.TypeToString(ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("tcnAmountPay.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("tcnAmountLoc.value='" & mobjValues.TypeToString(System.Math.Round(ldblAmount * ldblExchange, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
                Response.Write("tcnInterestLoc.value=VTFormat('0', '', '', '', 6, true);")
                Response.Write("tcnInterest_rate.value=VTFormat('0', '', '', '', 6, true);")
                Response.Write("}")
                Response.Write("top.frames['fraFolder'].nAmountPayJS='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("top.frames['fraFolder'].nAmountPayLocJS='" & mobjValues.TypeToString(ldblAmountLoc, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			
                '        If nTypDoc = 7 Then        
                '            Response.Write "with (top.frames['fraFolder'].document.forms[0]){"
                '            Response.Write "tcnAmountPay.disabled=true;"
                '            Response.Write "tcnAmountLoc.disabled=true;"
                '            Response.Write "}"
                '        End If
			
                lobjGeneral = Nothing
            End If
            lobjValdatconditions = Nothing
		
        Else
            Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
            If nTypDoc <> 2 And nTypDoc <> 7 And nTypDoc <> 21 And nTypDoc <> 22 And nTypDoc <> 23 Then
                Response.Write("cbeBranch.value='';")
                Response.Write("valProduct.value='';")
                Response.Write("top.frames['fraFolder'].$('#valProduct').change();")
                Response.Write("tcnPolicy.value='';")
                Response.Write("tcnCertif.value='0';")
                '+ Se borra el número de documento sólo en caso de que el tipo de documento no sea 7)Propuesta.
                Response.Write("tcnDocument.value='';")
                Response.Write("dtcClient.value='';")
                Response.Write("top.frames['fraFolder'].$('#dtcClient').change();")
            End If
		
            If nTypDoc = 7 Or nTypDoc = 21 Or nTypDoc = 22 Or nTypDoc = 23 Then
                Response.Write("cbeCurrency.disabled=false;")
            End If
            If nTypDoc = 7 Then
                '+ Se buscan las condiciones para mostrar la fecha de valorización por defecto
                If Request.QueryString.Item("dValuedateProp") = "" Then
                    lobjValdatconditions = New eCashBank.Valdatconditions
			
                    If lobjValdatconditions.InsFind_ValdatconditionCollect(eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nTypDoc, Session("dCollectDate")) Then
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.value='" & mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate) & "';")
                        If lobjValdatconditions.nChangesDat = 1 Or lobjValdatconditions.nChangesDat = 3 Or lobjValdatconditions.nChangesDat = 4 Then
                            Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
                        Else
                            Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=true;")
                        End If
                    End If
			
                    If mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.value='" & Session("dValueDate") & "';")
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdValuedate.disabled=false;")
                        ldtmValuedate = Session("dValueDate")
                    Else
                        ldtmValuedate = mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate)
                    End If
                Else
                    ldtmValuedate = Session("dValueDate")
                End If
                lobjGeneral = New eGeneral.Exchange
                '+ Se calcula factor de cambio de acuerdo a fecha de valorización.
                If lintCurrency = 1 Then
                    ldblExchange = 1
                Else
                    If lobjGeneral.Find(lintCurrency, ldtmValuedate) Then
                        ldblExchange = lobjGeneral.nExchange
                    Else
                        ldblExchange = 1
                    End If
                End If
			
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			
                lobjGeneral = Nothing
                lobjValdatconditions = Nothing
            Else
                Response.Write("tcdValuedate.value='';")
            End If

            Response.Write("hddnContrat.value='';")
            Response.Write("tcnDraft.value='';")
            Response.Write("tcnAmountCol.value=VTFormat('0', '', '', '', 6, true);")
            Response.Write("cbeCurrency.value= '1';")
            Response.Write("tcnExchange.value='1';")
            Response.Write("tcnAmountPay.value=VTFormat('0', '', '', '', 6, true);")
            Response.Write("tcnAmountLoc.value=VTFormat('0', '', '', '', 0, true);")
            Response.Write("tcnInterest_rate.value=VTFormat('0', '', '', '', 6, true);")
            Response.Write("tcnInterestLoc.value=VTFormat('0', '', '', '', 6, true);")
            Response.Write("}")
            Response.Write("top.frames['fraFolder'].nAmountPayJS=VTFormat('0', '', '', '', 6, true);")
            Response.Write("top.frames['fraFolder'].nAmountPayLocJS=VTFormat('0', '', '', '', 6, true);")
        End If
	
        lobjProduct = Nothing
        lobjDocument = Nothing
    End Sub

'% insDisabledCertif: Habilita o deshabilita el campo nCertif dependiendo del tipo de póliza pasada como parámetro.
'--------------------------------------------------------------------------------------------
Private Sub insDisabledCertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrPoliType As String
	Dim lblnOk As Boolean
	Dim lstrCertype As String
	
	lblnOk = False
	
	lstrCertype = "2"
	
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		'+ Si es tipo de documento se trata de una propuesta.
		If Request.QueryString.Item("nCollecDocTyp") = "7" Then
			
			If .Find_Proponum(eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				
				lblnOk = True
				lstrPoliType = .sPolitype
				lstrCertype = .sCertype
				Response.Write("top.frames['fraFolder'].document.forms[0].hddsCertype.value='" & .sCertype & "';")
			End If
		Else
			If .Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				lblnOk = True
				lstrPoliType = .sPolitype
				lstrCertype = .sCertype
				Response.Write("top.frames['fraFolder'].document.forms[0].hddsCertype.value='" & .sCertype & "';")
			End If
		End If
	End With
	
	If lblnOk Then
		If lstrPoliType = "1" Then
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value='0';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.disabled=true;")
			Call insFindDocuments(-1, lstrCertype, 1, -1, -1)
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.disabled=false;")
		End If
	Else
		If Request.QueryString.Item("nCollecDocTyp") = "7" Then
			Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
			Response.Write("cbeBranch.disabled=false;")
			Response.Write("valProduct.disabled=false;")
			Response.Write("btnvalProduct.disabled=false;")
			Response.Write("tcnPolicy.value='';")
			Response.Write("cbeCurrency.value=1;")
			Response.Write("cbeCurrency.disabled=false;")
			Response.Write("tcnCertif.disabled=false;")
			Response.Write("tcnCertif.value='0';")
			Response.Write("dtcClient.disabled=false;")
			Response.Write("dtcClient_Digit.disabled=false;")
			Response.Write("btndtcClient.disabled=false;")
			Response.Write("tcnAmountPay.value='0';")
			Response.Write("tcnAmountPay.disabled=false;")
			Response.Write("tcnAmountLoc.value='0';")
			Response.Write("tcnAmountCol.value='0';")
			Response.Write("tcnExchange.value='1';")
			
			Response.Write("dtcClient_Digit.value='';")
			Response.Write("dtcClient.value='';")
			Response.Write("top.frames['fraFolder'].UpdateDiv('lblCliename','');")
			Response.Write("}")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value='';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.disabled=true;")
		End If
	End If
	lclsPolicy = Nothing
End Sub

'% insShowCertif: Habilita o deshabilita el campo nCertif dependiendo del tipo de póliza pasada como parámetro.
'--------------------------------------------------------------------------------------------
Private Sub insShowCertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	With Response
		.Write("with(top.fraHeader.document.forms[0]){")
            If lclsPolicy.FindPolicybyPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                    mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                    mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
                .Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
                .Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                .Write("valProduct.value=" & lclsPolicy.nProduct & ";")
                .Write("valProduct.disabled=false;")
                .Write("top.fraHeader.$('#valProduct').change();")
                '.Write("valProduct.disabled=true;")
                
                If lclsPolicy.sPolitype = "1" Or (lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "1") Then
                    Response.Write("tcnCertif.disabled=true;")
                    Response.Write("tcnCertif.value='0';")
                Else
                    Response.Write("tcnCertif.disabled=false;")
                    Response.Write("tcnCertif.value='';")
                End If
            Else
                .Write("cbeBranch.value='';")
                .Write("valProduct.Parameters.Param1.sValue=0;")
                .Write("valProduct.value='';")
                .Write("UpdateDiv('valProductDesc', '');")
                .Write("tcnCertif.disabled=true;")
                .Write("tcnCertif.value='';")
            End If
		.Write("}")
	End With
	lclsPolicy = Nothing
End Sub

'% insShowPolicyInf: Muestra la informacion de la poliza
'--------------------------------------------------------------------------------------------
Private Sub insShowPolicyInf()
	'--------------------------------------------------------------------------------------------
	Dim lobjPremium As eCollection.Premium
	Dim lclsPolicy As ePolicy.Policy
	Dim sCertype As String
	Dim lobjDocument As eCollection.T_DocTyp
	Dim lintId As Integer
	
	sCertype = Request.QueryString.Item("sCertype")
	
	lclsPolicy = New ePolicy.Policy
	With Response
		.Write("with(top.frames['fraFolder'].document.forms[0]){")
		If lclsPolicy.FindPolicyClient(sCertype, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble, True), session("dCollectDate")) Then
			.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
             
            .Write("cbeTyp_Profit.Parameters.Param1.sValue=" & sCertype & ";")    
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=" & lclsPolicy.nBranch & ";")    
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=" & lclsPolicy.nProduct & ";")    
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble) & ";")    
                
			.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
			.Write("valProduct.disabled=false;")
			.Write("top.frames['fraFolder'].$('#valProduct').change();")
			.Write("valProduct.disabled=true;")
			.Write("tcnAmountPay.disabled=false;")
			.Write("dtcClient.value='" & lclsPolicy.sClient & "';")
			.Write("dtcClient_Digit.value='" & lclsPolicy.sDigit & "';")
                .Write("top.frames['fraFolder'].UpdateDiv('lblCliename','" & Replace(lclsPolicy.sCliename, "'", "") & "');")
                If Request.QueryString.Item("nCollecDocTyp") = "19" Then
                    .Write("valOrigin.value=" & lclsPolicy.nOrigin & ";")
                    .Write("top.frames['fraFolder'].$('#valOrigin').change();")
                End If
                
                If Request.QueryString.Item("nCollecDocTyp") = "4" Or Request.QueryString.Item("nCollecDocTyp") = "5" Or Request.QueryString.Item("nCollecDocTyp") = "8" Or Request.QueryString.Item("nCollecDocTyp") = "9" Or Request.QueryString.Item("nCollecDocTyp") = "18" Or Request.QueryString.Item("nCollecDocTyp") = "19" Or Request.QueryString.Item("nCollecDocTyp") = "20" Or Request.QueryString.Item("nCollecDocTyp") = "21" Or Request.QueryString.Item("nCollecDocTyp") = "22" Or Request.QueryString.Item("nCollecDocTyp") = "23" Or Request.QueryString.Item("nCollecDocTyp") = "24" Then
                    lobjDocument = New eCollection.T_DocTyp
                    lintId = lobjDocument.getT_DocTypId(mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True))
                    .Write("tcnDocument.value='" & lintId & "';")
                    .Write("cbeCurrency.value='" & mobjValues.TypeToString(lclsPolicy.NCURRENCY, eFunctions.Values.eTypeData.etdInteger, False, 0) & "';")
                    .Write("tcnExchange.value='" & mobjValues.TypeToString(lclsPolicy.nExchange, eFunctions.Values.eTypeData.etdDouble, False, 6) & "';")
                    .Write("tcdValuedate.value='" & Session("dCollectDate") & "';")
                    .Write("cbeCurrency.disabled=true;")
                ElseIf Request.QueryString.Item("nCollecDocTyp") = "11" Or Request.QueryString.Item("nCollecDocTyp") = "12" Or Request.QueryString.Item("nCollecDocTyp") = "16" Then
				
                    lobjPremium = New eCollection.Premium
				
                    If lobjPremium.Find_DocumentOld(sCertype, mobjValues.StringToType(CStr(lclsPolicy.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)) Then
					
                        If mobjValues.TypeToString(lobjPremium.nDocument, eFunctions.Values.eTypeData.etdDouble) > 0 Then
                            .Write("tcnDocument.value=" & lobjPremium.nDocument & ";")
						
                            mlngBranch = mobjValues.StringToType(CStr(lclsPolicy.nBranch), eFunctions.Values.eTypeData.etdDouble)
                            mlngProduct = mobjValues.StringToType(CStr(lclsPolicy.nProduct), eFunctions.Values.eTypeData.etdDouble)
						
                            Call insFindDocuments(mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True), sCertype, -1, lobjPremium.nDocument, lobjPremium.nContrat)
                        Else
                            If Request.QueryString.Item("nCollecDocTyp") = "12" Then
                                lobjDocument = New eCollection.T_DocTyp
                                lintId = lobjDocument.getT_DocTypId(mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True))
                                .Write("tcnDocument.value='" & lintId & "';")
                                .Write("cbeCurrency.value='" & mobjValues.TypeToString(lclsPolicy.NCURRENCY, eFunctions.Values.eTypeData.etdInteger, False, 0) & "';")
                                .Write("tcnExchange.value='" & mobjValues.TypeToString(lclsPolicy.nExchange, eFunctions.Values.eTypeData.etdDouble, False, 6) & "';")
                                .Write("tcdValuedate.value='" & Session("dCollectDate") & "';")
                                .Write("cbeCurrency.disabled=true;")
                            End If
                        End If
                    Else
                        If Request.QueryString.Item("nCollecDocTyp") = "12" Then
                            lobjDocument = New eCollection.T_DocTyp
                            lintId = lobjDocument.getT_DocTypId(mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True))
                            .Write("tcnDocument.value='" & lintId & "';")
                            .Write("cbeCurrency.value='" & mobjValues.TypeToString(lclsPolicy.NCURRENCY, eFunctions.Values.eTypeData.etdInteger, False, 0) & "';")
                            .Write("tcnExchange.value='" & mobjValues.TypeToString(lclsPolicy.nExchange, eFunctions.Values.eTypeData.etdDouble, False, 6) & "';")
                            .Write("tcdValuedate.value='" & Session("dCollectDate") & "';")
                            .Write("cbeCurrency.disabled=true;")
                        End If
                    End If
				
                    lobjPremium = Nothing
                End If
			
                If lclsPolicy.sPolitype = "1" Or (lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "1") Then
                    Response.Write("tcnCertif.disabled=true;")
                    Response.Write("tcnCertif.value='0';")
                Else
                    Response.Write("tcnCertif.disabled=false;")
                    Response.Write("tcnCertif.value='';")
                End If
            Else
                .Write("cbeBranch.value='';")
                .Write("valProduct.Parameters.Param1.sValue=0;")
                .Write("valProduct.value='';")
                .Write("top.frames['fraFolder'].UpdateDiv('valProductDesc', '');")
                
                .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
                .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
                .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
                .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
                .Write("cbeTyp_Profit.value='';")
                .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
                
                .Write("dtcClient.value='';")
                .Write("dtcClient_Digit.value='';")
                .Write("top.frames['fraFolder'].UpdateDiv('lblCliename','');")
                .Write("tcnCertif.disabled=true;")
                .Write("tcnCertif.value='';")
                .Write("tcnAmountPay.value='';")
                .Write("tcnAmountPay.disabled=true;")
            End If
            .Write("}")
        End With
	lclsPolicy = Nothing
	lobjDocument = Nothing
End Sub

'% insShowDocumentInf: Muestra los documentos pendientes mas antiguos de una poliza
'--------------------------------------------------------------------------------------------
Private Sub insShowDocumentInf()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lblnOk As Boolean
	Dim sCertype As String
	Dim lobjPremium As eCollection.Premium
	Dim lobjDocument As Object
	Dim lobjProduct As eProduct.Product
	Dim lintId As Object
	Dim lobjGeneral As Object
	Dim lobjDraft As Object
	Dim ldblExchange As Byte
	Dim lintCurrency As Byte
	Dim ldblAmount As Byte
	Dim lblnFind As Object
	Dim lintTypDoc As Object
	Dim llngCertif As Object
	Dim lstrCertype As String
	Dim lintBranch As Object
	Dim lintBranch_Aux As Object
	Dim lintProduct As Object
	Dim lobjValdatconditions As Object
	Dim ldtmValuedate As Object
	Dim lobjFinance As Object
	Dim lobjClient As Object
	Dim nTypDoc As Object
	
	sCertype = Request.QueryString.Item("sCertype")
	
	nTypDoc = mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True)
	
	lclsPolicy = New ePolicy.Policy
	lobjProduct = New eProduct.Product
	
	lintCurrency = 1
	ldblExchange = 1
	ldblAmount = 0
	
	lblnOk = False
	
	With Response
		.Write("with(top.frames['fraFolder'].document.forms[0]){")
		If lclsPolicy.FindPolicyClient(sCertype, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble, True), session("dCollectDate")) Then
			
			.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")

            .Write("cbeTyp_Profit.Parameters.Param1.sValue=" & sCertype & ";")    
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=" & lclsPolicy.nBranch & ";")    
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=" & lclsPolicy.nProduct & ";")    
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble) & ";")    
                
                
			.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
			.Write("valProduct.disabled=false;")
			.Write("top.frames['fraFolder'].$('#valProduct').change();")
			.Write("valProduct.disabled=true;")
			.Write("tcnAmountPay.disabled=false;")
			lobjPremium = New eCollection.Premium
			
			If lobjPremium.Find_DocumentOld(sCertype, mobjValues.StringToType(CStr(lclsPolicy.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)) Then
				
				.Write("tcnDocument.value=" & lobjPremium.nDocument & ";")
				
				Call insFindDocuments(nTypDoc, lstrCertype, -1, lobjPremium.nDocument, lobjPremium.nContrat)
				
				lblnOk = True
			End If
		End If
		
		If Not lblnOk Then
			.Write("cbeBranch.value='';")
			.Write("valProduct.Parameters.Param1.sValue=0;")
			.Write("valProduct.value='';")
			.Write("UpdateDiv('valProductDesc', '');")
            .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
            .Write("cbeTyp_Profit.value='';")
            .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
    		.Write("UpdateDiv('lblCliename', '');")
			.Write("tcnCertif.disabled=true;")
			.Write("tcnCertif.value='';")
			.Write("tcnAmountPay.value='';")
			.Write("tcnAmountPay.disabled=true;")
		End If
		.Write("}")
	End With
	
	lclsPolicy = Nothing
	lobjDraft = Nothing
	lobjPremium = Nothing
	lobjProduct = Nothing
	
End Sub


'% insShowPolicyInf: Muestra la informacion de la poliza
'--------------------------------------------------------------------------------------------
Private Sub insShowPolicyLoansInf()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsCurren_pol As ePolicy.Curren_pol
	Dim lclsLoans As ePolicy.Loans
	Dim sCertype As String
	
	sCertype = Request.QueryString.Item("sCertype")
	
	lclsPolicy = New ePolicy.Policy
	lclsCurren_pol = New ePolicy.Curren_pol
	lclsLoans = New ePolicy.Loans
	
	With Response
		.Write("with(top.frames['fraFolder'].document.forms[0]){")
		If lclsPolicy.FindPolicybyPolicy(sCertype, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")

            .Write("cbeTyp_Profit.Parameters.Param1.sValue=" & sCertype & ";")    
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=" & lclsPolicy.nBranch & ";")    
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=" & lclsPolicy.nProduct & ";")    
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble) & ";")    
                
			.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
			.Write("valProduct.disabled=false;")
			.Write("top.frames['fraFolder'].$('#valProduct').change();")
			.Write("valProduct.disabled=true;")
			
			If lclsPolicy.sPolitype = "1" Or (lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "1") Then
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value='0';")
			Else
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value='';")
			End If
			
			If lclsLoans.Find_loansA(lclsPolicy.nBranch, lclsPolicy.nProduct, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0) Then
				
				If lclsLoans.nCode > 0 Then
					.Write("valCode.disabled=false;")
					.Write("btnvalCode.disabled=false;")
					.Write("valCode.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
					.Write("valCode.Parameters.Param2.sValue=" & lclsPolicy.nProduct & ";")
					.Write("valCode.Parameters.Param3.sValue=" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble) & ";")
					.Write("valCode.Parameters.Param4.sValue=" & mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble) & ";")
					.Write("valCode.Parameters.Param5.sValue=" & session("CO001_nAction") & ";")
					.Write("valCode.value=" & lclsLoans.nCode & ";")
					.Write("top.frames['fraFolder'].$('#valCode').change();")
				Else
					.Write("valCode.value='';")
					.Write("top.frames['fraFolder'].UpdateDiv('valCodeDesc','');")
					.Write("valCode.Parameters.Param1.sValue=0;")
					.Write("valCode.Parameters.Param2.sValue=0;")
					.Write("valCode.Parameters.Param3.sValue=0;")
					.Write("valCode.Parameters.Param4.sValue=0;")
					.Write("valCode.Parameters.Param5.sValue=0;")
					
					.Write("cbeBranch.value='';")
					.Write("valProduct.Parameters.Param1.sValue=0;")
					.Write("valProduct.value='';")
					.Write("top.frames['fraFolder'].UpdateDiv('valProductDesc','');")
                        
                    .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
                    .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
                    .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
                    .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
                    .Write("cbeTyp_Profit.value='';")
                    .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
                        
					.Write("tcnCertif.disabled=true;")
					.Write("tcnCertif.value='';")
					.Write("valCode.disabled=true;")
					.Write("btnvalCode.disabled=true;")
					
					
					.Write("with (top.frames['fraFolder'].document.forms[0]){")
					.Write("tcnAmountCol.value=VTFormat('0', '', '', '', 6, true);")
					.Write("cbeCurrency.value= '1';")
					.Write("tcnExchange.value='1';")
					.Write("tcnAmountPay.value=VTFormat('0', '', '', '', 6, true);")
					.Write("tcnAmountLoc.value=VTFormat('0', '', '', '', 0, true);")
					.Write("tcnInterest_rate.value=VTFormat('0', '', '', '', 6, true);")
					.Write("tcnInterestLoc.value=VTFormat('0', '', '', '', 6, true);")
					.Write("}")
					.Write("top.frames['fraFolder'].nAmountPayJS=VTFormat('0', '', '', '', 6, true);")
					.Write("top.frames['fraFolder'].nAmountPayLocJS=VTFormat('0', '', '', '', 6, true);")
					
					
				End If
			Else
				.Write("valCode.value='';")
				.Write("top.frames['fraFolder'].UpdateDiv('valCodeDesc','');")
				.Write("valCode.Parameters.Param1.sValue=0;")
				.Write("valCode.Parameters.Param2.sValue=0;")
				.Write("valCode.Parameters.Param3.sValue=0;")
				.Write("valCode.Parameters.Param4.sValue=0;")
				.Write("valCode.Parameters.Param5.sValue=0;")
				
				.Write("cbeBranch.value='';")
				.Write("valProduct.Parameters.Param1.sValue=0;")
				.Write("valProduct.value='';")
				.Write("UpdateDiv('valProductDesc', '');")
                .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
                .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
                .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
                .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
                .Write("cbeTyp_Profit.value='';")
                .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
                    
				.Write("tcnCertif.disabled=true;")
				.Write("tcnCertif.value='';")
				.Write("valCode.disabled=true;")
				.Write("btnvalCode.disabled=true;")
			End If
		Else
			.Write("cbeBranch.value='';")
			.Write("valProduct.Parameters.Param1.sValue=0;")
			.Write("valProduct.value='';")
			.Write("UpdateDiv('valProductDesc', '');")

            .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
            .Write("cbeTyp_Profit.value='';")
            .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
                
                
			.Write("tcnCertif.disabled=true;")
			.Write("tcnCertif.value='';")
		End If
		.Write("}")
	End With
	lclsPolicy = Nothing
	lclsCurren_pol = Nothing
End Sub
'% insUpdateCheck: Se encarga de actualizar el campo sel en la tabla temporal t_docTyp.
'--------------------------------------------------------------------------------------------
Private Sub insUpdateCheck()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_DocTyp As eCollection.T_DocTyp
	Dim lobjColFormref As eCollection.ColformRef
	Dim ShowTotals As Double
	lclsT_DocTyp = New eCollection.T_DocTyp
	'+ Se actualiza el campo sSel de la tabla temporal t_doctyp para seleccionar o deseleccionar el registro.
	With lclsT_DocTyp
		.nBordereaux = session("nBordereaux")
		.nCollecDocTyp = mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)
		.nSequence = CInt(Request.QueryString.Item("nSequence"))
		.sSel = Request.QueryString.Item("sSel")
		.insUpdT_DocTyp(4)
	End With
	
	lobjColFormref = New eCollection.ColformRef
	'+ Se realizan los respectivos cálculos de la relación debido a la selección o des-selección de la información.
	With lobjColFormref
		.nBordereaux = session("nBordereaux")
		.sStatus = session("sStatus")
		.dCollect = session("dCollectDate")
		.dValueDate = session("dValueDate")
		.nAction = session("CO001_nAction")
		.sRelOrigi = session("sRelOrigi")
		.calTotals()
		ShowTotals = System.Math.Round(.nTotalAmount + .nDifference - .nPaidAmount, 6)
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev', '" & mobjValues.TypeToString(System.Math.Round(.nTotalAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn', '" & mobjValues.TypeToString(System.Math.Round(.nPaidAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo', '" & mobjValues.TypeToString(System.Math.Round(ShowTotals, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		
	End With
	lobjColFormref = Nothing
	lclsT_DocTyp = Nothing
End Sub
'% insGetDocNumber: Obtiene los datos del número de documento pasado como parámetro.
'-----------------------------------------------------------------------------------
Private Sub insGetDocNumber()
	'-----------------------------------------------------------------------------------
	Dim lclsBank_mov As eCashBank.Bank_mov
	Dim lobjGeneral As eGeneral.Exchange
	Dim ldblExchange As Double
	
	lobjGeneral = New eGeneral.Exchange
	lclsBank_mov = New eCashBank.Bank_mov
	
	'+ Se verIfica si existe un depósito registrado en el sistema.    
	If lclsBank_mov.Find_sDep_number(Request.QueryString.Item("sDocNumber"), CInt(Request.QueryString.Item("nType_mov"))) Then
		
		Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
		
		Response.Write("nBankAcc.value='" & lclsBank_mov.nAcc_bank & "';")
		Response.Write("top.frames['fraFolder'].$('#nBankAcc').change();")
		Response.Write("nCurrency.value='" & lclsBank_mov.nCurrency & "';")
		
		If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), session("dValueDate")) Then
			ldblExchange = lobjGeneral.nExchange
		Else
			ldblExchange = 1
		End If
		
		Response.Write("tcnExchange.value='" & mobjValues.TypeToString(ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
		Response.Write("nAmount.value='" & lclsBank_mov.nCash_amoun & "';")
		Response.Write("tcnAmountLoc.value='" & System.Math.Round(CDbl(mobjValues.TypeToString(lclsBank_mov.nCash_amoun * ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 0)), 0) & "';")
		Response.Write("dDoc_date.value='" & mobjValues.TypeToString(lclsBank_mov.dDoc_date, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("nTransac.value='" & lclsBank_mov.nMovement & "';")
		Response.Write("nBankAcc.disabled=true;")
		Response.Write("btnnBankAcc.disabled=true;")
		Response.Write("nAmount.disabled=true;")
		Response.Write("nCurrency.disabled=true;")
		Response.Write("dDoc_date.disabled=true;")
		Response.Write("btn_dDoc_date.disabled=true;")
		Response.Write("};")
		
	Else
		
		Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
		Response.Write("nBankAcc.disabled=false;")
		Response.Write("btnnBankAcc.disabled=false;")
		Response.Write("nAmount.disabled=false;")
		Response.Write("nCurrency.disabled=false;")
		Response.Write("dDoc_date.disabled=false;")
		Response.Write("btn_dDoc_date.disabled=false;")
		Response.Write("}")
		
	End If
	
	lobjGeneral = Nothing
	lclsBank_mov = Nothing
End Sub

'% insRent_Values: muestra por default los valores para renta vitalicia si el tipo de pago es "primera renta"
'-------------------------------------------------------------------------------------------------------------------------
Private Sub insRent_Values()
	'-------------------------------------------------------------------------------------------------------------------------
	Dim lclsT_DocTyps As eCollection.T_DocTyps
	Dim lclsT_DocTyp As eCollection.T_DocTyp
	
	lclsT_DocTyps = New eCollection.T_DocTyps
	lclsT_DocTyp = New eCollection.T_DocTyp
	
	If lclsT_DocTyps.FindT_DocTypAll(session("nBordereaux"), mobjValues.StringToType(Request.QueryString.Item("nPayment"), eFunctions.Values.eTypeData.etdLong)) Then
		For	Each lclsT_DocTyp In lclsT_DocTyps
			If lclsT_DocTyp.nCollecDocTyp = 13 Or lclsT_DocTyp.nCollecDocTyp = 14 Or lclsT_DocTyp.nCollecDocTyp = 15 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].dtExpirDate.value='" & lclsT_DocTyp.dExpirdatbon & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].dtEmiDate.value='" & lclsT_DocTyp.dIssuedatbon & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnRate.value='" & lclsT_DocTyp.nRate_disc & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnNominalVal.value='" & mobjValues.TypeToString(lclsT_DocTyp.nNom_Valbon, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			End If
		Next lclsT_DocTyp
	End If
	
	lclsT_DocTyps = Nothing
	lclsT_DocTyp = Nothing
End Sub
'% insShowClient_Agree: Se busca el cliente asociado al Convenio-Descuento por Planilla.
'-----------------------------------------------------------------------------------
Private Sub insShowClient_Agree()
	'-----------------------------------------------------------------------------------
	Dim lclsAgreement As eCollection.Agreement
	
	lclsAgreement = New eCollection.Agreement
	
	With lclsAgreement
		If .Find_sClient(CInt(Request.QueryString.Item("nAgreement"))) Then
			Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & lclsAgreement.sClient & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient_Digit.value='" & lclsAgreement.sDigit & "';")
			Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"",""" & lclsAgreement.sCliename & """);")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='';")
			Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient_Digit.value='';")
			Response.Write("top.frames['fraFolder'].UpdateDiv(""lblCliename"","""");")
		End If
	End With
	
	lclsAgreement = Nothing
End Sub

'% ShowPolicyRentVital: Rescata recibo pendiente de rentas vitalicias
'-----------------------------------------------------------------------------------
Private Sub ShowPolicyRentVital()
	'-----------------------------------------------------------------------------------
	Dim lclsCollection As eCollection.Premium
	Dim sCertype As String
	Dim lobjGeneral As eGeneral.Exchange
	Dim ldtmValuedate As Object
	
	sCertype = Request.QueryString.Item("sCertype")
	
	
	lclsCollection = New eCollection.Premium
	With Response
		.Write("with(top.frames['fraFolder'].document.forms[0]){")
		If lclsCollection.FindPolicyRentVital(sCertype, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble, True), session("dCollectDate"), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)) Then
			.Write("cbeBranch.value=" & lclsCollection.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsCollection.nBranch & ";")
                
            .Write("cbeTyp_Profit.Parameters.Param1.sValue=" & sCertype & ";")    
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=" & lclsCollection.nBranch & ";")    
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=" & lclsCollection.nProduct & ";")    
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=" & mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble) & ";")    
                
			.Write("valProduct.value=" & lclsCollection.nProduct & ";")
			.Write("valProduct.disabled=false;")
			.Write("top.frames['fraFolder'].$('#valProduct').change();")
			.Write("valProduct.disabled=true;")
			.Write("cbeCurrency.value='" & lclsCollection.nCurrency & "';")
			.Write("tcnAmountPay.value='" & lclsCollection.nPremium & "';")
			.Write("tcnAmountCol.value='" & lclsCollection.nPremium & "';")
			.Write("dtcClient.value='" & lclsCollection.sClient & "';")
			.Write("dtcClient_Digit.value='" & lclsCollection.sDigit & "';")
			.Write("top.frames['fraFolder'].UpdateDiv('lblCliename','" & Replace(lclsCollection.sCliename, "'", "") & "');")
			.Write("tcdValuedate.value='" & session("dValueDate") & "';")
			
			If lclsCollection.nDocument > 0 Then
				.Write("tcnDocument.value=" & lclsCollection.nDocument & ";")
			End If
			
			If lclsCollection.nRate_disc > 0 Then
				.Write("tcnTax_discount.value='" & lclsCollection.nRate_disc & "';")
			End If
			
			If lclsCollection.nNom_Valbon > 0 Then
				.Write("tcnface_value.value='" & mobjValues.TypeToString(lclsCollection.nNom_Valbon, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			End If
			
			.Write("tcdIssuedate.value='" & mobjValues.TypeToString(lclsCollection.dIssuedatbon, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("tcdExpirdate.value='" & mobjValues.TypeToString(lclsCollection.dExpirdatbon, eFunctions.Values.eTypeData.etdDate) & "';")
			
			If lclsCollection.sPolitype = "1" Or (lclsCollection.sPolitype = "2" And lclsCollection.sColinvot = "1") Then
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value='0';")
			Else
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value='';")
			End If
			
			'+ Se calcula el factor de cambio según la fecha de valorización
			lobjGeneral = New eGeneral.Exchange
			ldtmValuedate = session("dValueDate")
			
			If lobjGeneral.Find(lclsCollection.nCurrency, ldtmValuedate) Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='1';")
			End If
			
			Response.Write("if (top.frames['fraFolder'].document.forms[0].tcnAmountPay.value!=0){")
			Response.Write("top.frames['fraFolder'].nAmountPayJS=-1;")
			Response.Write("top.frames['fraFolder'].nAmountPayLocJS=-1;")
			Response.Write("top.frames['fraFolder'].insCalculateLocal(""Amount""); }")
			
			Response.Write("if (top.frames['fraFolder'].document.forms[0].tcnInterest_rate.value!=0){")
			Response.Write("top.frames['fraFolder'].nInterestPayJS=-1;")
			Response.Write("top.frames['fraFolder'].insCalculateLocal(""Interest""); }")
			
			lobjGeneral = Nothing
		Else
			.Write("cbeBranch.value='';")
			.Write("valProduct.Parameters.Param1.sValue=0;")
			.Write("valProduct.value='';")
			.Write("top.frames['fraFolder'].UpdateDiv('valProductDesc', '');")
            .Write("cbeTyp_Profit.Parameters.Param1.sValue=2;")
            .Write("cbeTyp_Profit.Parameters.Param2.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param3.sValue=0;")
            .Write("cbeTyp_Profit.Parameters.Param4.sValue=0;")
            .Write("cbeTyp_Profit.value='';")
            .Write("top.frames['fraFolder'].UpdateDiv('cbeTyp_ProfitDesc', '');")
                
			.Write("dtcClient.value='';")
			.Write("dtcClient_Digit.value='';")
			.Write("top.frames['fraFolder'].UpdateDiv('lblCliename','');")
			.Write("tcnCertif.disabled=true;")
			.Write("tcnCertif.value='';")
			.Write("tcnAmountPay.value='';")
		End If
		.Write("}")
	End With
	lclsCollection = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = session.SessionID
mobjNetFrameWork.nUsercode = session("nUsercode")
Call mobjNetFrameWork.BeginPage("ShowDefValues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.01
mobjValues.sSessionID = session.SessionID
mobjValues.nUsercode = session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "ShowDefValues"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




        <%=mobjValues.StyleSheet()%>
        <SCRIPT>
//+ Variable para el control de versiones
             document.VssVersion="$$Revision: 70 $|$$Date: 14/07/04 10:41 $|$$Author: Nvaplat40 $"
        </SCRIPT>
<%

Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Exchange"
		insCalExchange()
	Case "Receipt"
		insfindReceipt()
	Case "Bulletin"
		insfindBulletin()
	Case "LocalAmount"
		insCalLocalAmount()
	Case "CO001_K"
		insFindCO001_K()
	Case "Documents"
		Call insFindDocuments(-1, "", -1, -1, -1)
	Case "Certif"
		insDisabledCertif()
	Case "UpdateCheck"
		insUpdateCheck()
	Case "getExchange"
		insGetExchange()
	Case "getDocNumber"
		insGetDocNumber()
	Case "ShowCertif"
		insShowCertif()
	Case "ShowPolicyInf"
		insShowPolicyInf()
	Case "ShowPolicyRentVital"
		ShowPolicyRentVital()
	Case "CashNumID"
		insCashNumID()
	Case "Client"
		insShowClient()
	Case "ValCash_dEffecdate"
		ValCash_dEffecdate()
	Case "ValCashCO008"
		ValCashCO008()
	Case "Valuedate"
		insShowValuedate()
	Case "ConvertAmounting"
		insConvertAmounting()
	Case "ConvertAmountingLoc"
		insConvertAmountingLoc()
	Case "Cod_Agree"
		ShowCod_Agree()
	Case "Cod_Agree2"
		ShowCod_Agree2()
	Case "Case_Info"
		GetCase_Info()
	Case "ShowPolicyLoansInf"
		insShowPolicyLoansInf()
	Case "ShowDocumentInf"
		insShowDocumentInf()
	Case "Client_Agree"
		insShowClient_Agree()
	Case "Rent_Values"
		insRent_Values()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>
</HEAD>
<BODY>
        <FORM NAME="ShowValues">
        </FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 12.00.01
Call mobjNetFrameWork.FinishPage("ShowDefValues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




