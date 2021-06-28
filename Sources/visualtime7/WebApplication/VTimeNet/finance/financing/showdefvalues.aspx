<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% FI003Upd: Actualiza los valores de la PoPup una vez que se haya colocado  el contrato
' y giro a refinanciar 
'--------------------------------------------------------------------------------------------
Private Sub Exchange()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	Dim nExchange As Double
	
	lclsExchange = New eGeneral.Exchange
	
	'+ Calculando el factor de cambio
	
	'Call lclsExchange.Convert(Null, nAuxAmount, nCurrency, nCurr_cont, Date, 0)
	    Call lclsExchange.Convert(0, 12360000, CInt(Request.Form.Item("cbeCurr_cont")), CInt(Request.QueryString.Item("nCurrency")), Today, 0)
	
	If lclsExchange.pdblExchange = -1 Then
		nExchange = 1
	Else
		nExchange = lclsExchange.pdblExchange
	End If
	
	Response.Write("	opener.document.forms[0].tcnExchange.value='" & nExchange & "';")
	
	lclsExchange = Nothing
End Sub


'%ExchangeMake: Función que actuliza los valores según el factor de cambio
'%              Se utiliza en la forma 'FI012'
'--------------------------------------------------------------------------------------------
Private Sub ExchangeMake()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange_2 As eGeneral.Exchange
	Dim lclsFinanceDraft As eFinance.FinanceDraft
	Dim lclsFinanceCO As eFinance.financeCO
	Dim nExchange As Double
	Dim nAmount As Double
	Dim nInterest As Double
	Dim nDscto_amo As Double
	Dim nExpenses As Double
	Dim nTotAmount As Double
	
	lclsExchange_2 = New eGeneral.Exchange
	lclsFinanceDraft = New eFinance.FinanceDraft
	lclsFinanceCO = New eFinance.financeCO
	
	Call lclsFinanceDraft.Find(CDbl(Request.QueryString.Item("nContrat")), CInt(Request.QueryString.Item("nQ_Draft")))
	Call lclsFinanceCO.Find_Contrat(CDbl(Request.QueryString.Item("nContrat")), True)
	
	'+Se hace el cambio de El Monto del Importe
	
	With lclsExchange_2
		
            Call .Convert(0, lclsFinanceDraft.nAmount, lclsFinanceCO.nCurrency, CInt(Request.QueryString.Item("nCurr_cont")), Today, 0)
		
		If .pdblExchange = -1 Then
			nExchange = 1
		Else
			nExchange = .pdblExchange
		End If
		
		nAmount = .pdblResult
		Response.Write("opener.document.forms[0].tcnExchange.value='" & nExchange & "';")
		Response.Write("opener.document.forms[0].tcnAmount.value='" & .pdblResult & "';")
		
		'+Se hace el cambio de El interes de MORA			
		
            Call .Convert(0, CDbl(Request.QueryString.Item("nInterest")), lclsFinanceCO.nCurrency, CInt(Request.QueryString.Item("nCurr_cont")), Today, 0)
		
		nInterest = .pdblResult
		Response.Write("opener.document.forms[0].tcnInterest.value='" & .pdblResult & "';")
		
		'+Se hace el cambio de El Dscto por Pronto Pago		
		
            Call .Convert(0, CDbl(Request.QueryString.Item("nDscto_amo")), lclsFinanceCO.nCurrency, CInt(Request.QueryString.Item("nCurr_cont")), Today, 0)
		
		nDscto_amo = .pdblResult
		Response.Write("opener.document.forms[0].tcnDscto_amo.value='" & .pdblResult & "';")
		
		'+Se hace el cambio de Los GASTOS
		
            Call .Convert(0, CDbl(Request.QueryString.Item("nExpenses")), lclsFinanceCO.nCurrency, CInt(Request.QueryString.Item("nCurr_cont")), Today, 0)
		
		nExpenses = .pdblResult
		Response.Write("opener.document.forms[0].tcnExpenses.value='" & .pdblResult & "';")
		
		nTotAmount = (nAmount + nInterest + nExpenses) - nDscto_amo
		Response.Write("opener.document.forms[0].tcnTotalAmo.value='" & nTotAmount & "';")
	End With
	
	lclsExchange_2 = Nothing
	lclsFinanceDraft = Nothing
	lclsFinanceCO = Nothing
	
End Sub

'% insShowClient: Procedimiento que verifica la existencia del contrato y muestra el
'%                cliente asociado
'--------------------------------------------------------------------------------------------
Private Sub insShowClient()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceCO As eFinance.financeCO
	Dim lobjValues As eFunctions.Values
	Dim lTotDraft As Object
	
	lclsFinanceCO = New eFinance.financeCO
	lobjValues = New eFunctions.Values
	
	'+ Validando que el valor del contrato tenga valor
	
	If Request.QueryString.Item("nContrat") <> vbNullString And lobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		
		'+ Apertura de la variable que contiene la información del contrato
		
		If lclsFinanceCO Is Nothing Then
			lclsFinanceCO = New eFinance.financeCO
		End If
		
		If lclsFinanceCO.Find_Contrat(CDbl(Request.QueryString.Item("nContrat"))) Then
			Response.Write("opener.document.forms[0].tctClient.value='" & lclsFinanceCO.sClient & "';")
			Response.Write("opener.UpdateDiv('tctClieName','" & lclsFinanceCO.sClientName & "','Normal');")
		Else
			Response.Write("opener.document.forms[0].tctClient.value='';")
			Response.Write("opener.UpdateDiv('tctClieName','','Normal');")
			Response.Write("opener.document.forms[0].tcnFirstDra.value = '';")
			Response.Write("opener.document.forms[0].tcnLastDra.value = '';")
			Response.Write("opener.document.forms[0].tcnTotDraf.value = '';")
		End If
	End If
	
	lclsFinanceCO = Nothing
	lobjValues = Nothing
End Sub

'%insSumDraft: Procedimiento que suma el monto entre el primer y último giro
'--------------------------------------------------------------------------------------------
Private Sub insSumDraft()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceDraft As eFinance.FinanceDraft
	Dim lclsFinanceCO As eFinance.financeCO
	Dim lobjValues As eFunctions.Values
	Dim lTotDraft As Double
	
	lobjValues = New eFunctions.Values
	lclsFinanceCO = New eFinance.financeCO
	lclsFinanceDraft = New eFinance.FinanceDraft
	
	'+ Validando que los valores de ambos giros tengan valor
	
	If Request.QueryString.Item("ncontrat") <> vbNullString And lobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble) > 0 And Request.QueryString.Item("nFirstDra") <> vbNullString And lobjValues.StringToType(Request.QueryString.Item("nFirstDra"), eFunctions.Values.eTypeData.etdDouble) > 0 And Request.QueryString.Item("nLastDra") <> vbNullString And lobjValues.StringToType(Request.QueryString.Item("nLastDra"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		
		'+ Apertura de la variable que contiene la información del giro
		
		If lclsFinanceDraft Is Nothing Then
			lclsFinanceDraft = New eFinance.FinanceDraft
		End If
		If lclsFinanceCO Is Nothing Then
			lclsFinanceCO = New eFinance.financeCO
		End If
		
		If lclsFinanceCO.Find_Contrat(CDbl(Request.QueryString.Item("nContrat"))) Then
			If Request.QueryString.Item("sCodispl") = "FI013" Then
				lTotDraft = lclsFinanceDraft.reaSumUpdDraftPeriod(CDbl(Request.QueryString.Item("nContrat")), CInt(Request.QueryString.Item("nFirstDra")), CInt(Request.QueryString.Item("nLastDra")), CInt(Request.QueryString.Item("chkPayment")), lclsFinanceCO.nDscto_amo)
			Else
				lTotDraft = lclsFinanceDraft.reaSumUpdDraftPeriod(CDbl(Request.QueryString.Item("nContrat")), CInt(Request.QueryString.Item("nFirstDra")), CInt(Request.QueryString.Item("nLastDra")))
			End If
		End If
	Else
		lTotDraft = 0
	End If
	
	Response.Write("opener.document.forms[0].tcnTotDraf.value='" & lTotDraft & "';")
	Response.Write("opener.$('#tcnTotDraf').change();")
	
	lclsFinanceDraft = Nothing
	lclsFinanceCO = Nothing
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Exchange"
		Call Exchange()
	Case "Exchange_2"
		Call ExchangeMake()
	Case "Contrat"
		Call insShowClient()
	Case "FirstDraft"
		Call insSumDraft()
	Case "LastDraft"
		Call insSumDraft()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




