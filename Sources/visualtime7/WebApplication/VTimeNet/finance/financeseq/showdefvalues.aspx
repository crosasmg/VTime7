<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFinance" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insGetExchange: Se busca el factor de cambio.
'-----------------------------------------------------------------------------------
Private Sub insGetExchange()
	Dim dReqDate As Object
        Dim nCurrency_ing As Object
        Dim nCurrency As Object
	Dim lclsExchanges As Object
	'-----------------------------------------------------------------------------------
	Dim lobjGeneral As eGeneral.Exchange
	Dim ldtmValuedate As Date
	Dim ldblExchange As Byte
	Dim ldblAmount As Byte
	Dim ldblAmountConvert As Object
	
	lobjGeneral = New eGeneral.Exchange
	
	
	If Not IsNothing(Request.QueryString.Item("dValuedate")) Then
		ldtmValuedate = mclsValues.StringToType(Request.QueryString.Item("dValuedate"), eFunctions.Values.eTypeData.etdDate)
	End If
	
	If lobjGeneral.Find(CInt(Request.QueryString.Item("nCurrency")), ldtmValuedate) Then
		ldblExchange = mclsValues.TypeToString(lobjGeneral.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6)
	Else
		ldblExchange = 1
	End If
	
	If Not IsNothing(Request.QueryString.Item("nAmount")) Then
		ldblAmount = mclsValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
	Else
		ldblAmount = 0
	End If
	
	Call lobjGeneral.Convert(0, ldblAmount, nCurrency_ing, nCurrency, dReqDate, 0)
	
	
	ldblAmountConvert = ldblAmount * ldblExchange
	
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnInitial.value='" & mclsValues.StringToType(ldblAmountConvert, eFunctions.Values.eTypeData.etdDouble) & "';")
	
	lobjGeneral = Nothing
End Sub


'% Contrat: Recarga los campoa de la FI001_K 
'--------------------------------------------------------------------------------------------
Private Sub Contrat()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceCO As eFinance.financeCO
	Dim Days As Object
	Dim Months As Object
	Dim dEffecdat As Object
	Dim lstrFirstDraft_date As String
	
	If CDbl(Request.QueryString.Item("nTransaction")) = 4 Then
		dEffecdat = Today
	Else
		dEffecdat = Request.QueryString.Item("deffecdate")
	End If
	
	
	If dEffecdat <> eRemoteDB.Constants.dtmnull And IsNothing(Request.QueryString.Item("ncontrat")) Then
		lclsFinanceCO = New eFinance.financeCO
		
		'+Se busca el contrato ingresado
		If lclsFinanceCO.Find(CDbl(Request.QueryString.Item("ncontrat")), dEffecdat) Then
			
			lstrFirstDraft_date = mclsValues.TypeToString(lclsFinanceCO.dFirst_draf, eFunctions.Values.eTypeData.etdDate)
			
			With Response
				.Write("var frm = top.frames['fraHeader'].document.forms[0];")
				
				'+ Si la acción es Recuperacion de un contrato s deja la fecha de efecto del registro y se deshabilita			
				If CDbl(Request.QueryString.Item("nTransaction")) = 4 Then
					.Write("    frm.tcdEffecdate.value='" & mclsValues.TypeToString(lclsFinanceCO.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
					.Write("    frm.tcdEffecdate.disabled= true;")
				End If
				If CDbl(Request.QueryString.Item("nTransaction")) = 3 Then
					.Write("    frm.tcdEffecdate.value='" & mclsValues.TypeToString(lclsFinanceCO.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
				.Write("    frm.cbeCurrency.value='" & lclsFinanceCO.nCurrency & "';")
				.Write("    frm.tcnInterest.value='" & mclsValues.TypeToString(lclsFinanceCO.nInterest, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				.Write("    frm.cbePay_com.value='" & lclsFinanceCO.sOpt_commi & "';")
				.Write("    frm.cbeOffice.value='" & lclsFinanceCO.nOffice & "';")
				.Write("	if  (" & lclsFinanceCO.sPayment_in & " == 1) ")
				.Write("        frm.chkPayment_in.checked = true; ")
				.Write("	else ")
				.Write("        frm.chkPayment_in.checked = false;")
				.Write("    frm.tcnDscto_pag.value='" & mclsValues.TypeToString(lclsFinanceCO.nDscto_pag, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				.Write("    frm.tcdLedger_dat.value='" & mclsValues.TypeToString(lclsFinanceCO.dLedger_dat, eFunctions.Values.eTypeData.etdDate) & "';")
				.Write("    frm.tctclient.value='" & lclsFinanceCO.sClient & "';")
				.Write("    frm.tctclient_Digit.value='" & lclsFinanceCO.sDigit & "';")
				.Write("    frm.tcnInitial.value='" & mclsValues.TypeToString(lclsFinanceCO.nInitial_or, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				.Write("    frm.cbeFrequency.value='" & lclsFinanceCO.nFrequency & "';")
				.Write("    frm.tcnQ_draft.value='" & lclsFinanceCO.nQ_Draft & "';")
				.Write("    frm.tcdFirst_draf.value='" & lstrFirstDraft_date & "';")
				.Write("    frm.hddFirst_draf.value='" & lstrFirstDraft_date & "';")
				.Write("    top.frames['fraHeader'].UpdateDiv(""lblCliename"",'" & Replace(lclsFinanceCO.sClientName, "'", "´") & "','Normal');")
				.Write("    frm.tcnBillDay.value='" & lclsFinanceCO.nBill_Day & "';")
				.Write("    frm.cbeWay_pay.value='" & lclsFinanceCO.nWay_Pay & "';")
				.Write("    frm.tcnPolicy.value='" & mclsValues.TypeToString(lclsFinanceCO.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
				If lclsFinanceCO.sType_Contr = "1" Then
					Response.Write("frm.optType[0].checked=1;")
					insShowPolicy(lclsFinanceCO.nPolicy)
				Else
					Response.Write("frm.optType[1].checked=1;")
				End If
				
			End With
			
			Call insLastDateDraft(lstrFirstDraft_date, CStr(lclsFinanceCO.nQ_Draft), CStr(lclsFinanceCO.nFrequency), CStr(lclsFinanceCO.nBill_Day))
			
		Else
			If CDbl(Request.QueryString.Item("nTransaction")) <> 1 Then
				With Response
					.Write("var frm = top.frames['fraHeader'].document.forms[0];")
					.Write("    frm.cbeCurrency.value='';")
					.Write("    frm.tcnInterest.value='';")
					.Write("    frm.cbePay_com.value='';")
					.Write("    frm.cbeOffice.value='';")
					.Write("    frm.chkPayment_in.checked = false;")
					.Write("    frm.tcnDscto_pag.value='';")
					.Write("    frm.tcdLedger_dat.value='';")
					.Write("    frm.tctclient.value='';")
					.Write("    frm.tctclient_Digit.value='';")
					.Write("    top.frames['fraHeader'].UpdateDiv(""tctclient_Name"",'','Normal');")
					.Write("    frm.tcnInitial.value='';")
					.Write("    frm.cbeFrequency.value='';")
					.Write("    frm.tcnQ_draft.value='';")
					.Write("    frm.tcdFirst_draf.value='';")
					.Write("    frm.tcnBillDay.value='';")
					.Write("    frm.cbeWay_pay.value='';")
				End With
			End If
		End If
		lclsFinanceCO = Nothing
	End If
End Sub

'% Frecuency: Calcula la fecha de vencimiento del primer giro y actualiza el campo
'--------------------------------------------------------------------------------------------
Private Sub Frequency(ByVal sQ_Draft As Object, ByVal sFrequency As String, ByVal sEffecdate As String, ByVal sBillDay As Object)
	'--------------------------------------------------------------------------------------------
	Dim dEffecdate As Date
	Dim nFrec As Double
	Dim nQ_Quot As Object
	Dim nBillday As Object
	
	
	With Request
		If CDbl(.QueryString.Item("nTransaction")) = 2 Then Exit Sub
		
		'+Se almacena la cantidad de cuotas
		If sQ_Draft = "" Then
			nQ_Quot = 0
		Else
			nQ_Quot = sQ_Draft
		End If
		
		nFrec = getFrequencyFactor(sFrequency)
		
		If sEffecdate <> "" Then
			
			'+Se obtiene fecha de efecto del contrato
			dEffecdate = mclsValues.StringToType(sEffecdate, eFunctions.Values.eTypeData.etdDate)
			
			'+Se obtiene dia de pago. Por omision es el mismo de la fecha de efecto del contrato	        
			If sBillDay = "" Then
				nBillday = Microsoft.VisualBasic.Day(dEffecdate)
			Else
				nBillday = sBillDay
			End If
			
			'+Se ajusta fecha a la de efecto
			dEffecdate = DateSerial(Year(dEffecdate), Month(dEffecdate), nBillday)
			
			'+Fecha de primera cuota es un mes después de la de efecto
			dEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, nFrec, dEffecdate)
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdFirst_draf.value='" & mclsValues.TypeToString(dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddFirst_draf.value='" & mclsValues.TypeToString(dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
			
			'+Se recalcula fecha de ultima cuota
			Call insLastDateDraft(mclsValues.TypeToString(dEffecdate, eFunctions.Values.eTypeData.etdDate), sQ_Draft, sFrequency, sBillDay)
			
		End If
	End With
	
End Sub

'%insLastDateDraft: Obtiene la fecha de la última cuota
'--------------------------------------------------------------------------------------------
Private Sub insLastDateDraft(ByVal sDateIni As String, ByVal sQ_Draft As Object, ByVal sFreq As String, ByVal sBillDay As Object)
	'--------------------------------------------------------------------------------------------
	Dim dEffecdate As Date
	Dim nBillday As Object
	Dim nQ_Draft As Double
	Dim nFreq As Double
	
	'+Se almacena la cantidad de cuotas
	If sQ_Draft = "" Then
		nQ_Draft = 0
	Else
		nQ_Draft = sQ_Draft - 1
	End If
	
	dEffecdate = mclsValues.StringToType(sDateIni, eFunctions.Values.eTypeData.etdDate)
	
	nFreq = getFrequencyFactor(sFreq)
	
	'+Se obtiene dia de pago. Por omision es el mismo de la fecha de efecto del contrato	        
	If sBillDay = "" Then
		nBillday = Microsoft.VisualBasic.Day(dEffecdate)
	Else
		nBillday = sBillDay
	End If
	
	'+Se ajusta inicial para ajustar dia
	dEffecdate = DateSerial(Year(dEffecdate), Month(dEffecdate), nBillday)
	
	dEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, nFreq * nQ_Draft, dEffecdate)
	
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdLast_draf.value='" & mclsValues.TypeToString(dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
	
End Sub

'%getFrequencyFactor : Recupera un factor segun frecuencia para multiplicar los meses
'--------------------------------------------------------------------------------------------
Private Function getFrequencyFactor(ByVal sFrequency As String) As Double
	'--------------------------------------------------------------------------------------------
	'+ La opción seleccionada es No uniforme
	If sFrequency = "1" Then
		getFrequencyFactor = 0
		
		'+ La opción seleccionada es Mensual
	ElseIf sFrequency = "2" Then 
		getFrequencyFactor = 1
		
		'+ La opción seleccionada es Trimestral
	ElseIf sFrequency = "3" Then 
		getFrequencyFactor = 3
		
	End If
	
End Function


'% Accept: Recarga los valores correspondientes a Interes,Giro, Frecuencia y cuota inicial
'--------------------------------------------------------------------------------------------
Private Sub Accept()
	'--------------------------------------------------------------------------------------------
	Session("Continue") = "Yes"
	Session("nAuxAmount_fi") = ""
End Sub

'% FI003Upd: Actualiza los valores de la PoPup una vez que se haya colocado  el contrato
' y giro a refinanciar 
'--------------------------------------------------------------------------------------------
Private Sub FI003Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsReFinanDraft As eFinance.RefinanceDraft
	
	If Request.QueryString.Item("ncontrat_d") > CStr(eRemoteDB.Constants.strNull) And Request.QueryString.Item("ndraft_d") > CStr(eRemoteDB.Constants.strNull) Then
		
		lclsReFinanDraft = New eFinance.RefinanceDraft
		
		Call lclsReFinanDraft.AsignValPoPup(mclsValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("ncontrat_d"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("ndraft_d"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		Response.Write("	top.frames['fraFolder'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsReFinanDraft.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("	top.frames['fraFolder'].document.forms[0].tcnPremium.value='" & mclsValues.TypeToString(lclsReFinanDraft.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
		Response.Write("	top.frames['fraFolder'].document.forms[0].tcnCommission.value='" & lclsReFinanDraft.nCommission & "';")
		Response.Write("	top.frames['fraFolder'].document.forms[0].tctCurrency.value='" & lclsReFinanDraft.sCurrency & "';")
		Response.Write("	top.frames['fraFolder'].document.forms[0].tcnCurrency.value='" & lclsReFinanDraft.nCurrency & "';")
		Response.Write("    top.frames['fraFolder'].document.forms[0].tctClient.value='" & lclsReFinanDraft.sClient & "';")
		Response.Write("    top.frames['fraFolder'].UpdateDiv(""tctCliename"",""" & lclsReFinanDraft.sCliename & """);")
		Response.Write("    top.frames['fraFolder'].document.forms[0].tctClient_Digit.value='" & lclsReFinanDraft.sDigit & "';")
		Response.Write("	top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & mclsValues.TypeToString(lclsReFinanDraft.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
		
		lclsReFinanDraft = Nothing
	End If
End Sub

'%insAsignRowPos: Se asigna a las columna del grid los valores leídos de Premium
'--------------------------------------------------------------------------------------------
Private Sub insAsignRowPos()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	
	If Request.QueryString.Item("nReceipt") <> vbNullString And Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nProduct") <> vbNullString Then
		lclsPremium = New eCollection.Premium
		With lclsPremium
			If .Find("2", CDbl(Request.QueryString.Item("nReceipt")), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), 0, 0) Then
				
				Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.value='" & .nCurrency & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='" & .nExchange & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPolicy.value='" & .nPolicy & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntermed.value='" & .nIntermed & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctProductDes.value='" & .sDescProd & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnOffice.value='" & .nOffice & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdStartdate.value='" & .dStatDate & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirdat.value='" & .dExpirdat & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnCommission.value='" & .nComamou & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctClient.value='" & .sClient & "';")
				Response.Write("top.frames['fraFolder'].UpdateDiv(""tctCliename"",""" & .sCliename & """);")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctClient_Digit.value='" & .sDigit & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremium.value='" & .nBalance & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremium.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctClient.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPolicy.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntermed.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctProductDes.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnOffice.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdStartdate.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirdat.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnCommission.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tctClient_Digit.value='';")
				Response.Write("top.frames['fraFolder'].UpdateDiv(""tctCliename"","""");")
			End If
		End With
		lclsPremium = Nothing
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremium.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnExchange.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctClient.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnPolicy.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntermed.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctProductDes.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnOffice.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdStartdate.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirdat.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnCommission.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctClient_Digit.value='';")
		Response.Write("top.frames['fraFolder'].UpdateDiv(""tctCliename"","""");")
	End If
End Sub

'%AddFI007: Se asigna los valores por defecto cuando se esta insertando
'--------------------------------------------------------------------------------------------
Private Sub AddFI007()
	'--------------------------------------------------------------------------------------------
	Call nCurrency()
	With Response
		.Write("opener.document.forms[0].tctCliename.value='" & Session("scliename") & "';")
		.Write("opener.document.forms[0].tctCliename.disabled=true;")
		.Write("opener.document.forms[0].tcnExchange.disabled=true;")
		.Write("opener.document.forms[0].tctClient.value='" & Session("sclient") & "';")
		.Write("opener.document.forms[0].tcnContrat.value='" & Session("ncontrat") & "';")
		.Write("opener.document.forms[0].cbeCurrency.value='" & Session("nCurrency") & "';")
	End With
End Sub

'%nCurrency: Actualiza el valor del factor de cambio en el FI007
'--------------------------------------------------------------------------------------------
Private Sub nCurrency()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinancePre As eFinance.FinancePre
	lclsFinancePre = New eFinance.FinancePre
	Response.Write("opener.document.forms[0].tcnExchange.value='" & lclsFinancePre.FindnExchange(Session("ncontrat"), Today, CInt(Request.QueryString.Item("nCurrency"))) & "';")
	lclsFinancePre = Nothing
End Sub

'%Dates: Asigna los valores de las fecha
'--------------------------------------------------------------------------------------------
Private Sub Dates()
	'--------------------------------------------------------------------------------------------
	With Response
		.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
		If CDbl(Request.QueryString.Item("ntransaction")) = 1 Then
			.Write("top.frames['fraHeader'].document.forms[0].tcdLedger_dat.value='" & mclsValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
		End If
	End With
End Sub

'% insdelAll_draft: se eliminan todos las cuotas asociadas al contrato
'--------------------------------------------------------------------------------------------
Private Sub insdelAll_draft()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanc_dra As eFinance.FinanceDraft
	Dim lclsFinanceWin As eFinance.FinanceWin
	lclsFinanceWin = New eFinance.FinanceWin
	lclsFinanc_dra = New eFinance.FinanceDraft
	
	If lclsFinanc_dra.Delete_All(Session("nContrat")) Then
		With Response
			Call lclsFinanceWin.Add_Finan_win(Session("nContrat"), Session("dEffecdate"), "FI011", "1", Session("nUsercode"), Session("nTransaction"))
			.Write("var nOption=(top.frames['fraFolder'].document.forms[0].optCalc[0].checked==1)?'':2;")
			.Write("top.frames['fraFolder'].document.location.href='/VTimeNet/Finance/FinanceSeq/FI011.aspx?sCodispl=FI011&sCodisp=FI011&nMainAction=302&sOnSeq=1&sOption=' + nOption + '&nInitial=' + top.frames['fraFolder'].document.forms[0].tcnInitial.value;")
		End With
	End If
	lclsFinanc_dra = Nothing
	lclsFinanceWin = Nothing
End Sub

'% inscalInterest: se calcula el interés de la cuota a financiar
'--------------------------------------------------------------------------------------------
Private Sub inscalInterest()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinance_co As eFinance.financeCO
	Dim ldblInterest As Double
	Dim ldblAmount As Byte
	Dim ldblAmount_int As Object
	Dim ldblAmount_net As Object
	
	ldblInterest = 0
	ldblAmount = 0
	ldblAmount_int = 0
	ldblAmount_net = 0
	
	lclsFinance_co = New eFinance.financeCO
	
	With lclsFinance_co
		.nInterest = mclsValues.StringToType(Request.QueryString.Item("nInterest"), eFunctions.Values.eTypeData.etdDouble)
		'+ Interés a cobrar por la cuota
		ldblInterest = .CalInterest(mclsValues.StringToType(Request.QueryString.Item("dFirstDate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("dLastDate"), eFunctions.Values.eTypeData.etdDate))
		
		'+ Monto a financiar en la cuota
		ldblAmount = mclsValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
		'+ Monto a cobrar por el interés
		ldblAmount_int = (ldblAmount * ldblInterest) / 100
		'+ Monto a amortizar de la deuda
		ldblAmount_net = ldblAmount - ldblAmount_int
		
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmount_net.value=" & mclsValues.StringToType(ldblAmount_net, eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntammou.value=" & mclsValues.StringToType(ldblAmount_int, eFunctions.Values.eTypeData.etdDouble) & ";")
	End With
	lclsFinance_co = Nothing
End Sub

'%insLoadReceiptPolicy: Carga el grid de recibos de la póliza
'--------------------------------------------------------------------------
Private Function insLoadReceiptPolicy() As Object
	'--------------------------------------------------------------------------
	'-Constante para indicar que se deben regenerar datos existentes (borrar y crear)    
	Const NREGEN_DATA As Short = 1
	
	Dim lclsFinanceObj As Object
	
	
	lclsFinanceObj = New eFinance.FinancePre
	
	Call lclsFinanceObj.insPreLoadFI002(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, Session("nContrat"), Session("dEffecdate"), Session("nUsercode"), NREGEN_DATA)
	lclsFinanceObj = Nothing
	
	'+Se deja requerida la ventana de FI003-Contratos a refinanciar 
	lclsFinanceObj = New eFinance.FinanceWin
	Call lclsFinanceObj.Add_Finan_win(Session("nContrat"), Session("dEffecdate"), "FI003", "3", Session("nUsercode"), Session("nTransaction"))
	lclsFinanceObj = Nothing
	
	Response.Write("top.frames[""fraSequence""].location.reload();")
	Response.Write("top.frames[""fraFolder""].location.reload();")
	
End Function

'%insLoadDraftFinanc_pre: Carga el grid de las cuotas de refinancimiento
'--------------------------------------------------------------------------
Private Function insLoadDraftFinanc_pre() As Object
	'--------------------------------------------------------------------------
	'-Constante para indicar que se deben regenerar datos existentes (borrar y crear)    
	Const NREGEN_DATA As Short = 1
	
	Dim lclsRefinance_draft As eFinance.RefinanceDraft
	
	lclsRefinance_draft = New eFinance.RefinanceDraft
	
	Call lclsRefinance_draft.insPreLoadFI003(Session("nContrat"), Session("dEffecdate"), Session("nUsercode"), NREGEN_DATA)
	
	'Response.Write "top.frames[""fraSequence""].location.reload();"
	Response.Write("top.frames[""fraFolder""].location.reload();")
	
	lclsRefinance_draft = Nothing
	
End Function

'%insShowPolicy: Muestra los datos de la póliza a la cual pertenece el contrato
'--------------------------------------------------------------------------
Private Function insShowPolicy(ByVal nPolicy As Object) As Object
	'--------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Certificat
	lclsPolicy = New ePolicy.Certificat
	
	
	'+ Se busca la información de la póliza
	If lclsPolicy.Find_PolicyFI001("2", nPolicy) Then
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblnBranch"",'" & lclsPolicy.nBranch & "-" & lclsPolicy.sDesBranch & "','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblnProduct"",'" & lclsPolicy.nProduct & "-" & lclsPolicy.sDesProduct & "','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lbldEffecdate"",'" & lclsPolicy.dStartdate & "','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lbldExpirdate"",'" & lclsPolicy.dExpirdat & "','Normal');")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctclient.value='" & lclsPolicy.sClient & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctclient_Digit.value='" & lclsPolicy.sDigit & "';")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblCliename"",'" & Replace(lclsPolicy.sCliename, "'", "´") & "','Normal');")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsPolicy.nOffice & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWay_pay.value='" & lclsPolicy.nWay_Pay & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBillDay.value='" & lclsPolicy.nBill_Day & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.value='" & lclsPolicy.nCurrency & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBillDay.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWay_pay.disabled=true;")
		Session("nBranch") = lclsPolicy.nBranch
		Session("nProduct") = lclsPolicy.nProduct
		Session("nPolicy") = nPolicy
	Else
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblnBranch"",'','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblnProduct"",'','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lbldEffecdate"",'','Normal');")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lbldExpirdate"",'','Normal');")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctclient.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctclient_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv(""lblCliename"",'','Normal');")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value=top.frames['fraHeader'].objOpt_Financ.nOffice;")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.value=top.frames['fraHeader'].objOpt_Financ.nCurrency;")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWay_pay.value=0;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBillDay.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBillDay.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWay_pay.disabled=false;")
		Session("nBranch") = ""
		Session("nProduct") = ""
		Session("nPolicy") = ""
	End If
	lclsPolicy = Nothing
End Function

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



    <SCRIPT>
//- Variable para el control de versiones
        document.VssVersion="$$Revision: 12 $|$$Date: 6/10/04 15:41 $|$$Author: Nvaplat40 $"
    </SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "CalInterest"
		Call inscalInterest()
	Case "DelAllDraft"
		Call insdelAll_draft()
	Case "Contrat"
		Call Contrat()
	Case "Frequency"
		Call Frequency(Request.QueryString.Item("nQ_Draft"), Request.QueryString.Item("nFrequency"), Request.QueryString.Item("dEffecdate"), Request.QueryString.Item("nBillDay"))
	Case "LastDateDraft"
		Call insLastDateDraft(Request.QueryString.Item("dEffecdate"), Request.QueryString.Item("nQ_Draft"), Request.QueryString.Item("nFrequency"), Request.QueryString.Item("nBillDay"))
	Case "Accept"
		Call Accept()
	Case "FI003Upd"
		Call FI003Upd()
	Case "Receipt"
		Call insAsignRowPos()
	Case "AddFI007"
		Call AddFI007()
	Case "nCurrency"
		Call nCurrency()
	Case "Dates"
		Call Dates()
	Case "Policy"
		Call insShowPolicy(Request.QueryString.Item("nPolicy"))
	Case "GetExchange"
		Call insGetExchange()
	Case "ReceiptPolicy"
		Call insLoadReceiptPolicy()
	Case "DraftFinanc_pre"
		Call insLoadDraftFinanc_pre()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>





