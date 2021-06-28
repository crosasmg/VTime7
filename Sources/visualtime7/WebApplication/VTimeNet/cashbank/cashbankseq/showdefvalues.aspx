<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim nAmounting As Object
Dim mdtmValuedate As Object
Dim sw_Amounting As Byte
Dim mobjValues As eFunctions.Values



'%Find_Provider: Se obtiene el documento asociado al proveedor (OP006)
'------------------------------------------------------------------------------------
Sub Find_Provider()
	'------------------------------------------------------------------------------------
	Dim lclsTab_Provider As eClaim.Tab_Provider
	Dim nAfect As Object
	Dim nExcent As Object
	Dim nTax_Amount As Object
	Dim nCode As Object
	Dim nIndic As Object
	Dim nAmountpay As String
	Dim nTypeSupport As Object
	
	lclsTab_Provider = New eClaim.Tab_Provider
	nAmountpay = Request.QueryString.Item("nAmountpay")
	
	nTypeSupport = mobjValues.StringToType(Request.QueryString.Item("nTypeSupport"), eFunctions.Values.eTypeData.etdInteger)
	
	Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.value='" & nTypeSupport & "';")
	'    If lclsTab_Provider.FindProvider(mobjValues.StringToType(Request.QueryString("nProvider"),eFunctions.Values.eTypeData.etdDouble),True)Then        
	'       Response.Write "top.fraHeader.document.forms[0].cbeTypeSupport.value='" & lclsTab_Provider.nTypeSupport & "';" 
	'  Else
	'     Response.Write "top.fraHeader.document.forms[0].cbeTypeSupport.value=0;"        
	'	End If
	
	Dim lobjTax_FixVal As eAgent.tax_fixval
	If nTypeSupport = "0" Or nTypeSupport = eRemoteDB.Constants.intNull Then
		Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.disabled=false;")
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.disabled=true;")
		
		lobjTax_FixVal = New eAgent.tax_fixval
		
		With lobjTax_FixVal
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.Parameters.Param1.sValue='" & nTypeSupport & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.Parameters.Param2.sValue='" & mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "';")
			If .Find_nTypesupport(mobjValues.StringToType(nTypeSupport, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.value='" & mobjValues.TypeToString(.nCode, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnPercent.value='" & mobjValues.TypeToString(.nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.disabled=false;")
				Response.Write("top.frames['fraHeader'].document.forms[0].btncbeTax_code.disabled=false;")
				Response.Write("top.fraHeader.document.forms[0].tcnAfect.value='" & mobjValues.TypeToString(nAmountpay, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				Response.Write("top.fraHeader.document.forms[0].tcnExcent.value=0;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.value='';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnPercent.value='';")
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.disabled=true;")
				Response.Write("top.frames['fraHeader'].document.forms[0].btncbeTax_code.disabled=true;")
				Response.Write("top.fraHeader.document.forms[0].tcnTax_amount.value=0;")
				Response.Write("top.fraHeader.document.forms[0].tcnExcent.value='" & mobjValues.TypeToString(nAmountpay, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				Response.Write("top.fraHeader.document.forms[0].tcnAfect.value=0;")
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value=top.fraHeader.document.forms[0].tcnAmountpay.value;")
			End If
		End With
	End If
	
	Response.Write("top.frames['fraHeader'].$('#cbeTax_code').change();")
	lclsTab_Provider = Nothing
	lobjTax_FixVal = Nothing
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


'% ActivateOnBlur: Activa el evento "onBlur" de los "PossiblesValues" de la OP001
'-----------------------------------------------------------------------------------------------------------------------
Sub ActivateOnBlur()
	'-----------------------------------------------------------------------------------------------------------------------
	If Not IsNothing(Request.QueryString.Item("nCompany")) Then
		Response.Write("opener.document.forms[0].elements[""valConcept""].Parameters.Param1.sValue='" & Request.QueryString.Item("nCompany") & "';")
        Response.Write("opener.$('#valConcept').change();")
	End If
	Response.Write("opener.$('#valCurrAcc').change();")
	Response.Write("opener.$('#valAccBank').change();")
	Response.Write("opener.$('#valProduct').change();")
End Sub

'% ActivateOnBlur1: Activa el evento "onBlur" de los "PossiblesValues" de la OP006
'-----------------------------------------------------------------------------------------------------------------------
Sub ActivateOnBlur1()
	'-----------------------------------------------------------------------------------------------------------------------
	Response.Write("opener.$(""#valConcept"").change();")
	Response.Write("opener.$(""#cbeTax_code"").change();")
	Response.Write("opener.$(""#dtcBenef"").change();")
	Response.Write("opener.$(""#dtcBenef_Digit"").change();")
	Response.Write("opener.$(""#valReqUser"").change();")
	Response.Write("opener.$(""#valBranch_Led"").change();")
	Response.Write("opener.document.forms[0].elements[""valProduct""].Parameters.Param1.sValue='" & Request.QueryString.Item("nBranch") & "';")
	Response.Write("opener.$(""#valProduct"").change();")
End Sub

'% insShowRequest_num: Obtiene y muestra el número de solicitud de la orden de pago
'--------------------------------------------------------------------------------------------
Sub insShowRequest_num()
	'--------------------------------------------------------------------------------------------	
	Dim lobjNumerator As eGeneral.GeneralFunction
	Dim llngRequest As Double
	lobjNumerator = New eGeneral.GeneralFunction
	llngRequest = lobjNumerator.Find_Numerator(10, 0, Session("nUsercode"),  ,  ,  ,  ,  ,  ,  , Request.QueryString.Item("sCheque"), 0)
	Response.Write("opener.document.forms[0].tcnRequestNu.value='" & llngRequest & "';")
	lobjNumerator = Nothing
End Sub

'% insShowCurrency:Muestra la moneda asociada a la cuenta bancaria
'--------------------------------------------------------------------------------------------
Sub insShowCurrency()
	'--------------------------------------------------------------------------------------------
	Dim lobjBank_acc As eCashBank.Bank_acc
	Dim lintBank_acc As Object
	lobjBank_acc = New eCashBank.Bank_acc
	
	lintBank_acc = Request.QueryString.Item("nAcc_bank")
	If lobjBank_acc.Find_O(lintBank_acc) Then
		Response.Write("opener.document.forms[0].cbeCurrency.value='" & lobjBank_acc.nCurrency & "';")
	End If
	
	lobjBank_acc = Nothing
End Sub

'% insShowValuedate:Muestra la fecha de valorización por defecto según las condiciones
'--------------------------------------------------------------------------------------------
Sub insShowValuedate()
	'--------------------------------------------------------------------------------------------
	Dim llngConcept As Integer
	Dim lobjValdatconditions As eCashBank.Valdatconditions
	lobjValdatconditions = New eCashBank.Valdatconditions
	
	llngConcept = mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble, True)
	
	Call lobjValdatconditions.InsFind_ValdatconditionCollect(llngConcept, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate))
	
	If lobjValdatconditions.dValueDate <> eRemoteDB.Constants.dtmnull Then
		mdtmValuedate = mobjValues.TypeToString(lobjValdatconditions.dValueDate, eFunctions.Values.eTypeData.etdDate)
	Else
		If lobjValdatconditions.nChangesDat <> 1 Then
			mdtmValuedate = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
		Else
			mdtmValuedate = Request.QueryString.Item("dReqDate")
		End If
	End If
	
	Response.Write("top.fraHeader.document.forms[0].tcdValorDate.value='" & mdtmValuedate & "';")
	
	If CDbl(Request.QueryString.Item("nAction")) <> 402 Then
		If mdtmValuedate <> eRemoteDB.Constants.dtmnull Then
			If lobjValdatconditions.nChangesDat = 2 Then
				Response.Write("top.fraHeader.document.forms[0].tcdValorDate.disabled=true;")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcdValorDate.disabled=false;")
			End If
		Else
			Response.Write("top.fraHeader.document.forms[0].tcdValorDate.disabled=false;")
		End If
	End If
	
	Call insConvertAmounting()
	
	Response.Write("top.fraHeader.$('#valCurrAcc').change();")
	
	lobjValdatconditions = Nothing
End Sub

'% insCalcTax_amount: Se calculan los porcentajes
'--------------------------------------------------------------------------------------------
Sub insCalcTax_amount()
	'--------------------------------------------------------------------------------------------	
	Dim lclsTaxFix_val As eAgent.tax_fixval
	Dim nCode As Object
	Dim nAmount As Object
	Dim nAfect As Object
	Dim nExcent As Object
	Dim nAmounttax As Double
	
	lclsTaxFix_val = New eAgent.tax_fixval
	
	nCode = Request.QueryString.Item("nCode")
	nAmount = Request.QueryString.Item("nAmount")
	nAfect = Request.QueryString.Item("nAfect")
	
	If nCode = "" Then
		nCode = 0
	End If
	If nAmount = "" Then
		nAmount = 0
	End If
	If nAfect = "" Then
		nAfect = 0
	End If
	nExcent = Request.QueryString.Item("nExcent")
	If nExcent = "" Then
		nExcent = 0
	End If
	
	If nCode <> "" And nCode <> "0" Then
		If lclsTaxFix_val.Find(nCode, CDate(Request.QueryString.Item("dEffecdate"))) Then
			Response.Write("top.fraHeader.document.forms[0].tcnPercent.value='" & mobjValues.TypeToString(lclsTaxFix_val.nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			Response.Write("top.fraHeader.document.forms[0].tcnTax_amount.value='" & mobjValues.TypeToString(nAmount * (lclsTaxFix_val.nPercent / 100), eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			If nCode = "1" Then
				nAfect = mobjValues.TypeToString(nAmount * (100 / (lclsTaxFix_val.nPercent + 100)), eFunctions.Values.eTypeData.etdDouble, True, 2)
				nAmounttax = nAmount - nAfect
				Response.Write("top.fraHeader.document.forms[0].tcnAfect.value='" & mobjValues.TypeToString(nAmount * (100 / (lclsTaxFix_val.nPercent + 100)), eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				Response.Write("top.fraHeader.document.forms[0].tcnTax_amount.value='" & mobjValues.TypeToString(nAmount - nAfect, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(CDbl(nAfect) + CDbl(nExcent) + CDbl(nAmounttax), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			ElseIf nCode = "2" Then 
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString((CDbl(nAfect) + CDbl(nExcent)) - (CDbl(nAmount) * (CDbl(lclsTaxFix_val.nPercent) / 100)), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString((CDbl(nAfect) + CDbl(nExcent)) + (CDbl(nAmount) * (CDbl(lclsTaxFix_val.nPercent) / 100)), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			End If
		End If
	Else
		Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString((CDbl(nAfect) + CDbl(nExcent)) + (CDbl(nAmount) * (CDbl(lclsTaxFix_val.nPercent) / 100)), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	End If
	
	lclsTaxFix_val = Nothing
End Sub


'% insConvertAmount: Busca el factor de cambio para la conversión del monto
'--------------------------------------------------------------------------------------------
Sub insConvertAmount()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As Integer
	Dim nCurrency_targ As Integer
	Dim nAmount As Double
	Dim dReqDate As Object
	Dim nResult As Object
	Dim lclsTaxFix_val As eAgent.tax_fixval
	Dim nAmount_tax As Double
	Dim nAmoun_tot As Double
	Dim nAfect As Object
	Dim nExcent As Object
	Dim nTax_Amount As Object
	Dim nCode As Object
	Dim nAmounttotal As Double
	
	lclsExchanges = New eGeneral.Exchange
	lclsTaxFix_val = New eAgent.tax_fixval
	
	nCode = Request.QueryString.Item("nCode")
	
	nCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	nCurrency_targ = mobjValues.StringToType(Request.QueryString.Item("nCurrency_targ"), eFunctions.Values.eTypeData.etdDouble)
	
	If IsNothing(Request.QueryString.Item("dReqDate")) Then
		dReqDate = eRemoteDB.Constants.dtmnull
	Else
		dReqDate = mobjValues.StringToType(Request.QueryString.Item("dReqDate"), eFunctions.Values.eTypeData.etdDate)
	End If
	
	If Request.QueryString.Item("nAmount") = vbNullString Then
		nAmount = 0
	Else
		nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	Call lclsExchanges.Convert(0, nAmount, nCurrency, nCurrency_targ, dReqDate, 0)
	
	Response.Write("top.fraHeader.document.forms[0].tcnAmountpay.value='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
	If Not IsNothing(Request.QueryString.Item("nTypeSupport")) And Request.QueryString.Item("nTypeSupport") <> "0" Then
		If Request.QueryString.Item("nTypeSupport") = "2" Or Request.QueryString.Item("nTypeSupport") = "4" Then
			nExcent = lclsExchanges.pdblResult
			Response.Write("top.fraHeader.document.forms[0].tcnExcent.value='" & mobjValues.TypeToString(nExcent, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
		Else
			nAfect = lclsExchanges.pdblResult
			Response.Write("top.fraHeader.document.forms[0].tcnAfect.value='" & mobjValues.TypeToString(nAfect, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
		End If
	Else
		Response.Write("top.fraHeader.document.forms[0].tcnAfect.value = 0;")
		Response.Write("top.fraHeader.document.forms[0].tcnExcent.value= 0;")
		nExcent = 0
		nAfect = 0
	End If
	If nCode <> "" And nCode <> "0" Then
		If lclsTaxFix_val.Find(nCode, CDate(Request.QueryString.Item("dEffecdate"))) Then
			Response.Write("top.fraHeader.document.forms[0].tcnPercent.value='" & mobjValues.TypeToString(lclsTaxFix_val.nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			nTax_Amount = lclsExchanges.pdblResult * (lclsTaxFix_val.nPercent / 100)
			Response.Write("top.fraHeader.document.forms[0].tcnTax_amount.value='" & mobjValues.TypeToString(nTax_Amount, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			nAmount_tax = CDbl(lclsExchanges.pdblResult) * (CDbl(lclsTaxFix_val.nPercent) / 100)
			nAmoun_tot = CDbl(lclsExchanges.pdblResult)
			If nCode = "1" Then
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(CDbl(nAmoun_tot), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			ElseIf nCode = "2" Then 
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(CDbl(nAmoun_tot) - CDbl(nAmount_tax), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(CDbl(nAmoun_tot) + CDbl(nAmount_tax), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			End If
		End If
	Else
		Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(CDbl(lclsExchanges.pdblResult), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
	End If
	
	nCode = Request.QueryString.Item("nCode")
	If nCode <> "" And nCode <> "0" Then
		nAmounttotal = 0
		If nAfect = eRemoteDB.Constants.intNull Or nAfect = "" Then
			nAfect = 0
		End If
		If nExcent = eRemoteDB.Constants.intNull Or nExcent = "" Then
			nExcent = 0
		End If
		If nTax_Amount = eRemoteDB.Constants.intNull Or nTax_Amount = "" Then
			nTax_Amount = 0
		End If
		If nCode <> "2" Then
			nAmounttotal = mobjValues.StringToType(nAfect, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(nExcent, eFunctions.Values.eTypeData.etdDouble) ' + mobjValues.StringToType(nTax_Amount,eFunctions.Values.eTypeData.etdDouble)
			Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(System.Math.Round(nAmounttotal, 2), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			Response.Write("top.fraHeader.document.forms[0].tcnAfect.value='" & mobjValues.TypeToString(System.Math.Round(nAfect / (1 + lclsTaxFix_val.nPercent / 100), 2), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
			Response.Write("top.fraHeader.document.forms[0].tcnTax_amount.value='" & mobjValues.TypeToString(System.Math.Round(CDbl(nAfect - (nAfect / (1 + lclsTaxFix_val.nPercent / 100))), 2), eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
		Else
			nAmounttotal = mobjValues.StringToType(nAfect, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(nExcent, eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(nTax_Amount, eFunctions.Values.eTypeData.etdDouble)
			Response.Write("top.fraHeader.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(nAmounttotal, eFunctions.Values.eTypeData.etdDouble, True, 0) & "';")
		End If
	End If
	
	lclsExchanges = Nothing
End Sub


'% insCalTotalAmount: Se calcula el monto total de la orden de pago.
'--------------------------------------------------------------------------------------------
Sub insCalTotalAmount()
	'--------------------------------------------------------------------------------------------
	Dim nAfect As Object
	Dim nExcent As Object
	Dim nTax_Amount As Object
	Dim nCode As Object
	Dim nAmounttotal As Double
	
	nAfect = Request.QueryString.Item("nAfect")
	nExcent = Request.QueryString.Item("nExcent")
	nTax_Amount = Request.QueryString.Item("nTax_amount")
	nCode = Request.QueryString.Item("nCode")
	
	nAmounttotal = 0
	If nAfect < "" Then
		nAfect = 0
	End If
	If nExcent < "" Then
		nExcent = 0
	End If
	If nTax_Amount < "" Then
		nTax_Amount = 0
	End If
	If nCode <> 2 Then
		nAmounttotal = mobjValues.StringToType(nAfect, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(nExcent, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(nTax_Amount, eFunctions.Values.eTypeData.etdDouble)
	Else
		nAmounttotal = mobjValues.StringToType(nAfect, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(nExcent, eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(nTax_Amount, eFunctions.Values.eTypeData.etdDouble)
	End If
	
	Response.Write("opener.document.forms[0].tcnAmounttotal.value='" & mobjValues.TypeToString(nAmounttotal, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
End Sub

'% insConvertAmounting: Convierte un monto utilizando el factor de cambio.
'--------------------------------------------------------------------------------------------
Sub insConvertAmounting()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As Integer
	Dim nCurrency_ing As Integer
	Dim nAmount As Object
	Dim dReqDate As Object
	
	lclsExchanges = New eGeneral.Exchange
	
	sw_Amounting = 1
	
	nCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	nCurrency_ing = mobjValues.StringToType(Request.QueryString.Item("nCurrency_ing"), eFunctions.Values.eTypeData.etdDouble)
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
	
	Response.Write("top.fraHeader.document.forms[0].tcnAmounting.value='" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Response.Write("top.fraHeader.document.forms[0].tcnDiference.value='" & mobjValues.TypeToString(0, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Response.Write("top.fraHeader.document.forms[0].tcnAmount.value='" & mobjValues.TypeToString(nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	nAmounting = mobjValues.StringToType(CStr(lclsExchanges.pdblResult), eFunctions.Values.eTypeData.etdDouble)
	
	lclsExchanges = Nothing
End Sub

'% insShowDifference: Muestra la dIferencia entre el monto de ingreso y el introducido por el usuario
'----------------------------------------------------------------------------------------------------
Sub insShowDifference()
	'----------------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As String
	Dim nCurrency_ing As String
	Dim nAmount As String
	
	lclsExchanges = New eGeneral.Exchange
	
	sw_Amounting = 1
	
	nCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	nCurrency_ing = mobjValues.StringToType(Request.QueryString.Item("nCurrency_ing"), eFunctions.Values.eTypeData.etdDouble)
	
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
	nAmounting = mobjValues.StringToType(Request.QueryString.Item("nAmounting"), eFunctions.Values.eTypeData.etdDouble, True)
	
	Call lclsExchanges.Convert(0, mobjValues.StringToType(nAmount, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCurrency, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCurrency_ing, eFunctions.Values.eTypeData.etdDouble), Today, 0)
	
	If Not lclsExchanges.pdblResult = 0 Then
		Response.Write("top.fraHeader.document.forms[0].tcnDiference.value='" & mobjValues.TypeToString(lclsExchanges.pdblResult - nAmounting, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
		Response.Write("top.fraHeader.document.forms[0].tcnAmounting.value='" & mobjValues.TypeToString(nAmounting, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	Else
		Response.Write("top.fraHeader.document.forms[0].tcnDiference.value='';")
	End If
	
	lclsExchanges = Nothing
End Sub

'% insCalcInter: Calcula el interes genererado por un cheque a fecha
'--------------------------------------------------------------------------------------------
Sub insCalcInter()
	'--------------------------------------------------------------------------------------------
	Dim dDocDate As Date
	Dim dEffecDate As Date
	Dim int_che As Double
	Dim lclsValues As eFunctions.Values
	Dim dIf_day As Integer
	
	lclsValues = New eFunctions.Values
	
	dDocDate = mobjValues.StringToType(Request.QueryString.Item("dDocDate"), eFunctions.Values.eTypeData.etdDate)
	dEffecDate = mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)
	
	If sw_Amounting <> 1 Then
		nAmounting = mobjValues.StringToType(Request.QueryString.Item("nAmounting"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	sw_Amounting = 0
	
	If dDocDate <> vbNullString Then
		dIf_day = lclsValues.Date_DIff("d",dEffecDate, dDocDate)
		int_che = nAmounting * dIf_day / 30 / 100
		Response.Write("top.fraHeader.document.forms[0].tcnFinancInt.value='" & mobjValues.TypeToString(int_che, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	End If
	
	lclsValues = Nothing
End Sub

'% insOP002: Muestra la moneda asociada a la cuenta bancaria
'--------------------------------------------------------------------------------------------
Sub insOP002()
	'--------------------------------------------------------------------------------------------
	Dim lclsCash_mov As eCashBank.Cash_mov
	Dim lclsBank_mov As Object
	Dim lintBank_acc As String
	Dim lstrDepositNum As String
	Dim lstrFieldName As String
	Dim lstrQueryString As String
	
	lintBank_acc = Request.QueryString.Item("nAccCash")
	lstrDepositNum = Request.QueryString.Item("sDepositNum")
	lstrFieldName = Request.QueryString.Item("sFieldName")
	
	If lstrFieldName = "valAccCash" Then
		insShowCurrencyOP002()
	End If
	
	If lintBank_acc <> vbNullString And lstrDepositNum <> vbNullString Then
		lclsCash_mov = New eCashBank.Cash_mov
		If lclsCash_mov.FindByDeposit(eRemoteDB.Constants.intNull, lstrDepositNum, mobjValues.StringToType(lintBank_acc, eFunctions.Values.eTypeData.etdDouble), vbNullString) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.value='" & mobjValues.TypeToString(lclsCash_mov.dDoc_date, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdRealEffecDate.value='" & mobjValues.TypeToString(lclsCash_mov.dRealDep, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeCompany.value='" & mobjValues.TypeToString(lclsCash_mov.nCompany, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeChequeLocat.value='" & mobjValues.TypeToString(lclsCash_mov.nChequeLocat, eFunctions.Values.eTypeData.etdDouble) & "';")
			If lclsCash_mov.nMov_type = 1 Then
				Response.Write("top.frames['fraHeader'].document.forms[0].optToDeposit[0].checked =true;")
			ElseIf lclsCash_mov.nMov_type = 2 Then 
				Response.Write("top.frames['fraHeader'].document.forms[0].optToDeposit[1].checked =true;")
			ElseIf lclsCash_mov.nMov_type = 5 Then 
				Response.Write("top.frames['fraHeader'].document.forms[0].optToDeposit[2].checked =true;")
			ElseIf lclsCash_mov.nMov_type = 10 Then 
				Response.Write("top.frames['fraHeader'].document.forms[0].optToDeposit[3].checked =true;")
			ElseIf lclsCash_mov.nMov_type = 8 Then 
				Response.Write("top.frames['fraHeader'].document.forms[0].optToDeposit[4].checked =true;")
			End If
			
			If Request.QueryString.Item("sLinkSpecial") = "1" Then
				lstrQueryString = "nMainAction=401&nOptDeposit=" & Request.QueryString.Item("nOptDeposit") & "&dEffecDate=" & mobjValues.TypeToString(lclsCash_mov.dDoc_date, eFunctions.Values.eTypeData.etdDate) & "&dRealEffecDate=" & mobjValues.TypeToString(lclsCash_mov.dRealDep, eFunctions.Values.eTypeData.etdDate) & "&sDeposit=" & lstrDepositNum & "&nAccCash=" & lintBank_acc & "&nCompany=" & mobjValues.TypeToString(lclsCash_mov.nCompany, eFunctions.Values.eTypeData.etdDouble) & "&sLinkSpecial=1" & "&nCashNum=" & Request.QueryString.Item("nCashNum")
				Response.Write("top.frames['fraFolder'].location='OP002.aspx?" & lstrQueryString & "';")
			End If
			
		End If
		lclsCash_mov = Nothing
	End If
End Sub

'% insShowCurrencyOP002: Muestra la moneda asociada a la cuenta bancaria
'--------------------------------------------------------------------------------------------
Sub insShowCurrencyOP002()
	'--------------------------------------------------------------------------------------------
	Dim lobjBank_acc As eCashBank.Bank_acc
	Dim lobjTabGen As eGeneralForm.TabGen
	Dim lintBank_acc As Object
	
	lobjBank_acc = New eCashBank.Bank_acc
	lintBank_acc = Request.QueryString.Item("nAccCash")
	
	If lobjBank_acc.Find(lintBank_acc) Then
		lobjTabGen = New eGeneralForm.TabGen
		If lobjTabGen.Find("Table11", CStr(lobjBank_acc.nCurrency)) Then
			Response.Write("top.fraHeader.UpdateDiv('lblCurrency','" & lobjTabGen.sDescript & "','Normal');")
			Session("nCurrency") = lobjBank_acc.nCurrency
		End If
		lobjTabGen = Nothing
	Else
		Response.Write("top.fraHeader.UpdateDiv(""lblCurrency"",'" & "" & "','Normal');")
		Session("nCurrency") = 0
	End If
	
	lobjBank_acc = Nothing
End Sub

'% ShowReportCheq: Invoca el reporte "Listado de Cheques depositados"
'--------------------------------------------------------------------------------------------
Sub ShowReportCheq()
	'--------------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	mobjDocuments = New eReports.Report
	
	With mobjDocuments
		'+ OPL002: Listado de Cheques depositados
		.sCodispl = "OPL002"
		.ReportFilename = "OPL002.rpt"
		'.Tittle = "Listado de Cheques depositados"
		.setStorProcParam(1, Request.Form.Item("sDeposit"))
		.setStorProcParam(2, Request.Form.Item("nAcc_Bank"))
		Response.Write((.Command))
	End With
	mobjDocuments = Nothing
End Sub

'% ShowTax_FixVal: Obtiene la información del impuesto según el tipo del mismo
'-------------------------------------------------------------------------------------------- 
Private Sub ShowTax_FixVal()
	'-------------------------------------------------------------------------------------------- 	
	Dim lobjTax_FixVal As eAgent.tax_fixval
	lobjTax_FixVal = New eAgent.tax_fixval
	
	With lobjTax_FixVal
		If .Find_nTypesupport(mobjValues.StringToType(Request.QueryString.Item("nTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.Parameters.Param1.sValue='" & Request.QueryString.Item("nTypeSupport") & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.Parameters.Param2.sValue='" & mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.value='" & mobjValues.TypeToString(.nCode, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].$('#cbeTax_code').change();")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnPercent.value='" & mobjValues.TypeToString(.nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].btncbeTax_code.disabled=false;")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeTax_code.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].btncbeTax_code.disabled=true;")
			Response.Write("top.frames['fraHeader'].$('#cbeTax_code').change();")
		End If
	End With
	
	lobjTax_FixVal = Nothing
End Sub

'%insShowFinanInt: Calcula y muestra el porcentaje de interés financiero.
'----------------------------------------------------------------------------------------------------- 
Sub insShowFinanInt()
	'-----------------------------------------------------------------------------------------------------
	Dim lclsCash_mov As eCashBank.Cash_mov
	lclsCash_mov = New eCashBank.Cash_mov
	
	Dim nAmount As Integer
	Dim nPercent As Object
	Dim nAmounting As Double
	
	nPercent = lclsCash_mov.GetFinanInt(mobjValues.StringToType(Request.QueryString.Item("nAmount_Cheq"), eFunctions.Values.eTypeData.etdDouble), CDate(Request.QueryString.Item("dDoc_date")), CDate(Request.QueryString.Item("dEffecdate")))
	
	If mobjValues.StringToType(nPercent, eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
		nPercent = 0
	End If
	
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount_Cheq"), eFunctions.Values.eTypeData.etdDouble, True)
	If nAmount = eRemoteDB.Constants.intNull Then
		nAmount = 0
	End If
	nAmounting = mobjValues.StringToType(nPercent, eFunctions.Values.eTypeData.etdDouble, True)
	If nAmounting = eRemoteDB.Constants.intNull Then
		nAmounting = 0
	End If
	
	nAmounting = nAmounting + nAmount
	
	Response.Write("top.fraHeader.document.forms[0].tcnFinancInt.value='" & mobjValues.TypeToString(nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	Response.Write("top.fraHeader.document.forms[0].tcnAmounting.value='" & mobjValues.TypeToString(nAmounting, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	lclsCash_mov = Nothing
	
End Sub

'%FindTab_Provider: Se obtiene el documento asociado al proveedor (OP006)
'------------------------------------------------------------------------------------
Sub FindTab_Provider()
	'------------------------------------------------------------------------------------
	Dim lclsTab_Provider As eClaim.Tab_Provider
	Dim lclsClient As eClient.Client
	Dim mstrClient As String
	
	lclsTab_Provider = New eClaim.Tab_Provider
	lclsClient = New eClient.Client
	
	mstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	If lclsTab_Provider.FindClient(mstrClient, 0, True) Then
		Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.value='" & lclsTab_Provider.nTypeSupport & "';")
	End If
	
	If lclsTab_Provider.nTypeSupport = CDbl("0") Or lclsTab_Provider.nTypeSupport = eRemoteDB.Constants.intNull Then
		Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.disabled=false;")
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeTypeSupport.disabled=true;")
	End If
	
	lclsTab_Provider = Nothing
	lclsClient = Nothing
End Sub

'% ShowOffice: Obtiene la sucursal asociada al usuario y las opciones de instalacion cobranzas/ caja.
'--------------------------------------------------------------------------------------------
Sub ShowOffice()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsers As eSecurity.User
	Dim lobjCashBank As eCashBank.Cash_mov
	lclsUsers = New eSecurity.User
	lobjCashBank = New eCashBank.Cash_mov
	
	If lclsUsers.Find(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), True) Then
		Response.Write("top.fraHeader.document.forms[0].cbeOffice.value='" & lclsUsers.nOffice & "';")
	End If
	
	If lobjCashBank.Find_optBank() Then
		Session("nCollect_P") = lobjCashBank.nCollect_P
	Else
		Session("nCollect_P") = ""
	End If
	
	lclsUsers = Nothing
	lobjCashBank = Nothing
	
End Sub

'%ShowCod_Agree: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
'------------------------------------------------------------------------------------------------------
Sub ShowCod_Agree()
	'------------------------------------------------------------------------------------------------------
	Dim sType_BankAgree As Byte
	Dim lclsQuery As eRemoteDB.Query
	lclsQuery = New eRemoteDB.Query
	
	If CDbl(Request.QueryString.Item("nConcept")) = 29 Then
		sType_BankAgree = 1
	Else
		sType_BankAgree = 2
	End If
	
	If IsNothing(Request.QueryString.Item("nBank_Agree")) Then
		Response.Write("top.fraHeader.document.forms[0].cbeBank.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].cbeBank.value='';")
		Response.Write("top.fraHeader.document.forms[0].valAccBank.value='';")
		Response.Write("top.fraHeader.UpdateDiv('valAccBankDesc','','Normal');")
		Response.Write("top.fraHeader.document.forms[0].valAccBank.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].btnvalAccBank.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].tcnCod_Agree.value='';")
		Response.Write("top.fraHeader.document.forms[0].tcnCod_Agree.disabled=false;")
	Else
		If lclsQuery.OpenQuery("Bank_Agree", "nAccount", "nBank='" & Request.QueryString.Item("nBank_Agree") & "'" & " and sType_BankAgree='" & sType_BankAgree & "'") Then
			Response.Write("top.fraHeader.document.forms[0].tcnCod_Agree.value='" & lclsQuery.FieldToClass("nAccount") & "';")
			Response.Write("top.fraHeader.document.forms[0].tcnCod_Agree.disabled=true;")
			Response.Write("top.fraHeader.document.forms[0].cbeBank.value='" & Request.QueryString.Item("nBank_Agree") & "';")
			Response.Write("top.fraHeader.document.forms[0].cbeBank.disabled=true;")
			Response.Write("top.fraHeader.document.forms[0].valAccBank.value='" & lclsQuery.FieldToClass("nAccount") & "';")
			Response.Write("top.fraHeader.$('#valAccBank').change();")
			Response.Write("top.fraHeader.document.forms[0].valAccBank.disabled=true;")
			Response.Write("top.fraHeader.document.forms[0].btnvalAccBank.disabled=true;")
		End If
	End If
	
	lclsQuery = Nothing
	
End Sub

'% UpdateCase:Actualiza el combo de los Casos
'--------------------------------------------------------------------------------------------
Private Sub UpdateCase()
	'--------------------------------------------------------------------------------------------
	Dim lobjTables As eFunctions.Tables
	lobjTables = New eFunctions.Tables
	lobjTables.Parameters.Add("nClaim", Request.QueryString.Item("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If lobjTables.reaTable("tabClaim_cases") Then
		Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.options.length=0;")
		If Not Request.QueryString.Item("nPage") = "OP001" Then
			Response.Write("var option = new Option('','0');")
			Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.options.add(option,0);")
		End If
		Do While Not lobjTables.EOF
			Response.Write("var option = new Option('" & lobjTables.Fields("sDescript") & "','" & lobjTables.Fields("sKey") & "');")
			Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.options.add(option," & lobjTables.Fields("sKey") & ");")
			lobjTables.NextRecord()
		Loop 
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.options.length=0;")
		Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].cbeCaseNumber_AUX.value=-32768;")
		Response.Write("top.fraHeader.document.forms[0].tcnDeman_type_h.value=-32768;")
	End If
	lobjTables = Nothing
End Sub

'% ValCash_dEffecdate:Valida si la caja esta abierta
'--------------------------------------------------------------------------------------------
Private Sub ValCash_dEffecdate()
	'--------------------------------------------------------------------------------------------
	Dim lstrMessage As String
	Dim lobjCash_Stat As eCashBank.Cash_stat
    Dim lclsGeneral As eGeneral.GeneralFunction
    Dim lobjUser_CashNum As eCashBank.User_cashnum
        
	lobjCash_Stat = New eCashBank.Cash_stat
    lclsGeneral = New eGeneral.GeneralFunction
    lobjUser_CashNum = New eCashBank.User_cashnum
	
    If lobjUser_CashNum.Find_nUser(Session("nUserCode")) Then        
	    If Request.QueryString.Item("dEffecdate") <> vbNullString Then
		    If lobjCash_Stat.valCash_statClosed(Session("nCashNum"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			    lstrMessage = lclsGeneral.insLoadMessage(60129)
			    Response.Write("alert(""Err 60129:  " & lstrMessage & """);")
		    End If
	    End If
    End If
        
    lobjCash_Stat = Nothing
    lclsGeneral = Nothing
    lobjUser_CashNum = Nothing
End Sub

'% insreaPolicy_by_Policy: lee los datos de una propuesta/cotización/póliza, basado solo 
'%                         en el número de la misma
'--------------------------------------------------------------------------------------------
Private Sub insreaPolicy_by_Policy()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	With Response
		.Write("with(top.fraHeader.document.forms[0]){")
		If lclsPolicy.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
			.Write("top.fraHeader.$('#valProduct').change();")
		End If
		.Write("}")
	End With
	lclsPolicy = Nothing
End Sub

'% insReaOP001: Lectura de movimientos de caja
'--------------------------------------------------------------------------------------------
Sub insReaOP001()
	'--------------------------------------------------------------------------------------------
	Dim lcolCash_mov As eCashBank.Cash_movs
	Dim lclsCash_mov As eCashBank.Cash_mov
	Dim lclsCash_movDummy As eCashBank.Cash_mov
	Dim lobjValues As eFunctions.Values
	Dim lintCount As Integer
	Dim lintTotal As Integer
	
	lobjValues = New eFunctions.Values
	lcolCash_mov = New eCashBank.Cash_movs
	lclsCash_mov = New eCashBank.Cash_mov
	With lclsCash_mov
		.nTransac = lobjValues.StringToType(Request.QueryString.Item("nTransac"), eFunctions.Values.eTypeData.etdInteger, True)
		.nMov_type = lobjValues.StringToType(Request.QueryString.Item("nMov_type"), eFunctions.Values.eTypeData.etdInteger, True)
		.dEffecDate = lobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
		.nCash_id = lobjValues.StringToType(Request.QueryString.Item("nCash_Id"), eFunctions.Values.eTypeData.etdDouble)
		.nOffice = lobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdInteger, True)
		.dValDate = lobjValues.StringToType(Request.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate)
		.nOri_Curr = lobjValues.StringToType(Request.QueryString.Item("nOri_Curr"), eFunctions.Values.eTypeData.etdInteger, True)
		.nOri_Amount = lobjValues.StringToType(Request.QueryString.Item("nOri_Amount"), eFunctions.Values.eTypeData.etdDouble)
		.nCurrency = lobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger, True)
		.nAmount = lobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
		.nCompany = lobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdInteger, True)
		.nConcept = lobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdInteger, True)
		.nAcc_bank = lobjValues.StringToType(Request.QueryString.Item("nAcc_Bank"), eFunctions.Values.eTypeData.etdInteger, True)
		.sDocnumbe = Request.QueryString.Item("nDocNumber")
		.sCard_num = Request.QueryString.Item("nCreditCardNumber")
		.nCard_typ = lobjValues.StringToType(Request.QueryString.Item("nCreditCardType"), eFunctions.Values.eTypeData.etdInteger, True)
		.nChequeLocat = lobjValues.StringToType(Request.QueryString.Item("nChequelocat"), eFunctions.Values.eTypeData.etdInteger, True)
		.nInputChannel = lobjValues.StringToType(Request.QueryString.Item("nInputChannel"), eFunctions.Values.eTypeData.etdInteger, True)
		.nBank_code = lobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdInteger, True)
		.nBordereaux = lobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdInteger, True)
	End With
	With lcolCash_mov
		If .FindOP001(lclsCash_mov.nMov_type, lclsCash_mov.dEffecDate, lclsCash_mov.nCash_id, lclsCash_mov.nOffice, lclsCash_mov.dValDate, lclsCash_mov.nOri_Curr, lclsCash_mov.nOri_Amount, lclsCash_mov.nCurrency, lclsCash_mov.nAmount, lclsCash_mov.nCompany, lclsCash_mov.nConcept, lclsCash_mov.nAcc_bank, lclsCash_mov.sDocnumbe, lclsCash_mov.sCard_num, lclsCash_mov.nCard_typ, lclsCash_mov.nChequeLocat, lclsCash_mov.nInputChannel, lclsCash_mov.nBank_code, lclsCash_mov.nBordereaux, lclsCash_mov.nTransac, eRemoteDB.Constants.intNull, lclsCash_mov.nInsur_area) Then
			
			lintTotal = .Count
			Response.Write("with(top.fraHeader){")
			For lintCount = 1 To lintTotal
				lclsCash_movDummy = .Item(lintCount)
				With lclsCash_movDummy
					Response.Write("insAddOP001('" & lobjValues.TypeToString(.dEffecDate, eFunctions.Values.eTypeData.etdDate) & "'," & "'" & lobjValues.TypeToString(.nTransac, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nCompanyc, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nIntermed, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & .sClient & "'," & "'" & lobjValues.TypeToString(.nAcc_bank, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & .sDocnumbe & "'," & "'" & lobjValues.TypeToString(.dDoc_date, eFunctions.Values.eTypeData.etdDate) & "'," & "'" & lobjValues.TypeToString(.nBank_code, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & .sCard_num & "'," & "'" & lobjValues.TypeToString(.nCard_typ, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.dCard_expir, eFunctions.Values.eTypeData.etdDate) & "'," & "'" & lobjValues.TypeToString(.nTyp_acco, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & .sType_acc & "'," & "'" & .sNumForm & "'," & "'" & lobjValues.TypeToString(.nBordereaux, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nClaim, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nContrat, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nDraft, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nConcept, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nOffice, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nMov_type, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nChequeLocat, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nCompany, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nOri_Amount, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nFin_int, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nInputChannel, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.nCash_id, eFunctions.Values.eTypeData.etdInteger) & "'," & "'" & lobjValues.TypeToString(.dValDate, eFunctions.Values.eTypeData.etdDate) & "'," & "'" & lobjValues.TypeToString(.nNoteNum, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nProponum, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nCod_Agree, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nBank_Agree, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.dCollection, eFunctions.Values.eTypeData.etdDate) & "'," & "'" & lobjValues.TypeToString(.nOri_Curr, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nTypeSupport, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & lobjValues.TypeToString(.nSupport_Id, eFunctions.Values.eTypeData.etdDouble) & "'," & "'" & .sDigit & "'," & "'" & .sCliename & "'," & "'" & .sConcept & "'," & "'" & .sCurrAcc & "'," & "'" & .sBank_descript & "'," & "'" & .sProduct & "'," & "'" & .sInter_name & "'," & "'" & .sCompany & "');" & vbCrLf)
				End With
				lclsCash_movDummy = Nothing
			Next 
			Response.Write("mlngCurrentIndex = 0; ShowFields(mlngCurrentIndex);")
			Response.Write("}")
		End If
	End With
	lcolCash_mov = Nothing
	lclsCash_mov = Nothing
	lobjValues = Nothing
End Sub

'%Find_nProponum_Amount: Busca el monto y moneda de pago de Primera Prima.
'----------------------------------------------------------------------------------------------------- 
Sub Find_nProponum_Amount()
	'-----------------------------------------------------------------------------------------------------
	Dim lclsCash_mov As eCashBank.Move_acc
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency As Object
	
	lclsCash_mov = New eCashBank.Move_acc
	lclsExchanges = New eGeneral.Exchange
	
	If Request.QueryString.Item("nProponum") <> "undefined" And Not IsNothing(Request.QueryString.Item("nProponum")) Then
		If mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
			Call lclsCash_mov.Find_nProponum_Amount(CDbl(Request.QueryString.Item("nProponum")))
			
			If lclsCash_mov.nAmount > 0 Then
				Response.Write("top.fraHeader.document.forms[0].tcnAmount.value='" & mobjValues.TypeToString(lclsCash_mov.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value='" & mobjValues.TypeToString(lclsCash_mov.nCurrency, eFunctions.Values.eTypeData.etdLong) & "';")
				Response.Write("top.fraHeader.document.forms[0].cbeCurrencyPay.value='1';")
				Response.Write("top.fraHeader.document.forms[0].tcnAmountpay.value='" & mobjValues.TypeToString(lclsCash_mov.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnAmount.value='0';")
				Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value='0';")
				Response.Write("top.fraHeader.document.forms[0].cbeCurrencyPay.value='0';")
				Response.Write("top.fraHeader.document.forms[0].tcnAmountpay.value='0';")
			End If
		Else
			Response.Write("top.fraHeader.document.forms[0].tcnAmount.value='0';")
			Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value='0';")
			Response.Write("top.fraHeader.document.forms[0].cbeCurrencyPay.value='0';")
			Response.Write("top.fraHeader.document.forms[0].tcnAmountpay.value='0';")
		End If
	End If
	
	lclsExchanges = Nothing
	lclsCash_mov = Nothing
End Sub


'% ShowClient_Agree: Obtiene el cliente asociado al convenio
'--------------------------------------------------------------------------------------------
Private Sub ShowClient_Agree()
	'--------------------------------------------------------------------------------------------
	Dim lclsAgreement As eCollection.Agreement
	lclsAgreement = New eCollection.Agreement
	With Response
		.Write("with(top.fraHeader.document.forms[0]){")
		If lclsAgreement.Find_sClient(CInt(Request.QueryString.Item("sAgreement"))) Then
			.Write("dtcClient_Agree.value= '" & lclsAgreement.sClient & "';")
			.Write("dtcClient_Agree_Digit.value='" & lclsAgreement.sDigit & "';")
			.Write("top.fraHeader.UpdateDiv('lblCliename_Agree','" & lclsAgreement.sCliename & "');")
		End If
		.Write("}")
	End With
	lclsAgreement = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues|" & Request.QueryString.Item("Field"))
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.22
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"

Response.Write(mobjValues.StyleSheet)

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



	
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 21 $|$$Date: 12/05/04 20:39 $|$$Author: Nvaplat40 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
</HTML>
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "PayOrderTyp"
		insShowRequest_num()
	Case "AccountNum"
		insShowCurrency()
	Case "Tax_amount"
		insCalcTax_amount()
	Case "ConvertAmount"
		insConvertAmount()
	Case "ConvertAmounting"
		insConvertAmounting()
	Case "CalTotalAmount"
		insCalTotalAmount()
	Case "ShowDifference"
		insShowDifference()
	Case "insCalcInter"
		insCalcInter()
	Case "Currency"
		insShowCurrencyOP002()
	Case "OP002"
		insOP002()
	Case "ReportCheq"
		ShowReportCheq()
	Case "Tax_FixVal"
		ShowTax_FixVal()
	Case "FinanInt"
		insShowFinanInt()
	Case "ActivateOnBlur"
		ActivateOnBlur()
	Case "ActivateOnBlur1"
		ActivateOnBlur1()
	Case "Tab_Provider"
		FindTab_Provider()
	Case "Office"
		ShowOffice()
	Case "Cod_Agree"
		ShowCod_Agree()
	Case "UpdateCase"
		UpdateCase()
	Case "PolicyByPolicy"
		insreaPolicy_by_Policy()
	Case "Client"
		insShowClient()
	Case "Valuedate"
		insShowValuedate()
	Case "ValCash_dEffecdate"
		ValCash_dEffecdate()
	Case "insReaOP001"
		insReaOP001()
	Case "ClientAgreement"
		ShowClient_Agree()
	Case "Find_Provider"
		Find_Provider()
	Case "nProponum"
		Find_nProponum_Amount()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.22
Call mobjNetFrameWork.FinishPage("showdefvalues|" & Request.QueryString.Item("Field"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




