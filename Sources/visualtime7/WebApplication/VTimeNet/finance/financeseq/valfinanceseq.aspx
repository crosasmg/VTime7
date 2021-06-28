<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- varible para almacenar querystring
Dim mstrQueryString As Object

'- Se define la variable para almacenar la nueva dirección de la FI001
Dim mstrLocationFI001 As String

Dim mstrErrors As Object
Dim mobjFinanceSeq As Object


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalFinanceSeq() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsFinancePre As eFinance.FinancePre
	Dim lclsFinanceDraft As eFinance.FinanceDraft
	Select Case Request.QueryString.Item("sCodispl")
		Case "FI001_K"
			mobjFinanceSeq = New eFinance.financeCO
			With Request
				insvalFinanceSeq = mobjFinanceSeq.insValFI001_K(mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLedger_dat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nledCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("tctClient_Digit"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQ_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFrequency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFirst_draf"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("hddFirst_draf"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDscto_pag"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLast_draf"), eFunctions.Values.eTypeData.etdDate))
				
				Session("sCliename") = mobjFinanceSeq.sClientName
				Session("sClient") = .Form.Item("tctClient")
				Session("nCurrency") = .Form.Item("cbeCurrency")
				Session("nAuxInterest") = .Form.Item("tcnInterest")
				Session("nAuxQ_draft") = .Form.Item("tcnQ_draft")
				Session("nAuxFrequency") = .Form.Item("cbeFrequency")
				Session("AmountInitial") = .Form.Item("tcnInitial")
				Session("nOffice") = .Form.Item("cbeOffice")
				Session("nWay_pay") = .Form.Item("cbeWay_pay")
				
			End With
			
			'+ FI002:Datos de recibos financiados
		Case "FI002"
			With Request
				lclsFinancePre = New eFinance.FinancePre
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalFinanceSeq = lclsFinancePre.insValFI002Upd(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), Session("nCompanyUser"), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnIntermed").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nCompanyUser"))
				Else
					insvalFinanceSeq = lclsFinancePre.insValFI002(.QueryString.Item("sCodispl"), mobjValues.StringToType(CStr(.Form.Item("tcnAuxReceipt").Length), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnTotalAmount"), eFunctions.Values.eTypeData.etdDouble))
				End If
				lclsFinancePre = Nothing
			End With
			
			'+ Refinanciamiento de Giro
		Case "FI003"
			mobjFinanceSeq = New eFinance.RefinanceDraft
			If Request.QueryString.Item("Action") <> vbNullString Then
				insvalFinanceSeq = mobjFinanceSeq.insValFI003(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContrat_d"), eFunctions.Values.eTypeData.etdDouble), Session("deffecdate"), mobjValues.StringToType(Request.Form.Item("tcndraft_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeDraftValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"))
			Else
				insvalFinanceSeq = True
			End If
			
			'+ FI004: Cuotas de un contrato
		Case "FI004"
			
			lclsFinanceDraft = New eFinance.FinanceDraft
			
			With Request
				insvalFinanceSeq = lclsFinanceDraft.insValFI004(.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			lclsFinanceDraft = Nothing
			
			
			'+ Generación manual de cuotas
		Case "FI011"
			mobjFinanceSeq = New eFinance.FinanceDraft
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalFinanceSeq = mobjFinanceSeq.insValFI011(.QueryString("sCodispl"), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdPrevExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddLengthArray"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddIndex"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalFinanceSeq = ""
				End If
			End With
			
			'+ Ventana de cancelacion de proceso o de fin de proceso
		Case "GE101", "FI008", "FI007"
			insvalFinanceSeq = ""
			
		Case Else
			insvalFinanceSeq = "insvalFinanceSeq: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostFinanceSeq() As Boolean
	Dim lintIndex As Integer
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Dim lclsFinancePre As eFinance.FinancePre
	Dim lclsFinanceDraft As eFinance.FinanceDraft
	Select Case Request.QueryString.Item("sCodispl")
		Case "FI001_K"
			With Request
				mobjFinanceSeq = New eFinance.financeCO
				lblnPost = mobjFinanceSeq.insPostFI001_K(mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLedger_dat"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbePay_com"), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkPayment_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQ_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFrequency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFirst_draf"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnDscto_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDscto_pag"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
				If lblnPost Then
					If Request.Form.Item("tcnContrat") <> vbNullString Then
						Session("nContrat") = Request.Form.Item("tcnContrat")
					Else
						Session("nContrat") = mobjFinanceSeq.nContrat
					End If
					Session("dEffecdate") = .Form.Item("tcdEffecdate")
					Session("nTransaction") = .Form.Item("cbeTransactio")
					Session("nCurrency") = .Form.Item("cbeCurrency")
					Session("nWay_pay") = .Form.Item("cbeWay_pay")
					If mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
						Session("bQuery") = True
					Else
						Session("bQuery") = False
					End If
					
					If .Form.Item("optType") = "2" Then
						Session("nBranch") = ""
						Session("nPolicy") = ""
						Session("nProduct") = ""
						Session("optType") = "2"
					Else
						Session("optType") = "1"
					End If
					
					mstrLocationFI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=FI001&sProject=FinanceSeq&sModule=Finance&sConfig=InSequence'"
				End If
			End With
			
			'+ FI002:Datos de recibos financiados
		Case "FI002"
			With Request
				lclsFinancePre = New eFinance.FinancePre
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = lclsFinancePre.insPostFI002Upd(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctProductDes"), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcnIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble), Session("nCompanyUser"), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					
				Else
					lblnPost = lclsFinancePre.insPostFI002(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTotalAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
				lclsFinancePre = Nothing
			End With
			
			'+ Refinanciamiento de Giro
		Case "FI003"
			mobjFinanceSeq = New eFinance.RefinanceDraft
			If Request.QueryString.Item("Action") = "Update" Then
				lblnPost = mobjFinanceSeq.insPostFI003(1, 1, Request.Form.Item("tctClient"), mobjValues.StringToType(Request.Form.Item("tcnDraft_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tcdExpirdat"), mobjValues.StringToType(Request.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Request.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContrat_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeDraftValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				
			ElseIf Request.QueryString.Item("Action") = "Add" Then 
				lblnPost = mobjFinanceSeq.insPostFI003(0, 0, Request.Form.Item("tctClient"), mobjValues.StringToType(Request.Form.Item("tcnDraft_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tcdExpirdat"), mobjValues.StringToType(Request.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Request.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContrat_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeDraftValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			Else
				lblnPost = mobjFinanceSeq.insPostFI003(1, 0, "", 0, mobjValues.StringToType(Request.Form.Item("hddTotRef"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Request.Form.Item("hddTotcom"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				
				
			End If
			
			'+ FI004: Cuotas de un contrato
		Case "FI004"
			With Request
				If Session("nTransaction") <> 2 Then
					lclsFinanceDraft = New eFinance.FinanceDraft
					
					If .Form.Item("tcnAuxDraft").Length > 1 Then
						For lintIndex = 1 To .Form.Item("tcnAuxDraft").Length
							lblnPost = lclsFinanceDraft.insPostFI004(.QueryString.Item("sCodispl"), lintIndex, mobjValues.StringToType(.Form.GetValues("tcnIndicator").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxDraft").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnStat_draft").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxAmount").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxAmount_Net").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxCommission").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcdAuxExpirdat").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.GetValues("tcdAuxLimitdat").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("tcnAuxCom_afec").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxCom_exen").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble))
						Next 
					Else
						If .Form.Item("tcnAuxDraft").Length = 0 Then
							lblnPost = True
						Else
							lblnPost = lclsFinanceDraft.insPostFI004(.QueryString.Item("sCodispl"), 1, mobjValues.StringToType(.Form.Item("tcnIndicator"), eFunctions.Values.eTypeData.etdDouble), Session("nContrat"), mobjValues.StringToType(.Form.Item("tcnAuxDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnStat_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxAmount_Net"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdAuxExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), Session("nUsercode"), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcdAuxLimitdat"), eFunctions.Values.eTypeData.etdDate))
						End If
					End If
					lclsFinanceDraft = Nothing
				Else
					lblnPost = True
				End If
			End With
			
			'+ Generación manual de cuotas		
		Case "FI011"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjFinanceSeq.insPostFI011(.QueryString("Action"), Session("nContrat"), Session("dEffecdate"), Session("nTransaction"), Session("nCurrency"), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_net"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntammou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
			
			
			'+ Ventana de Fin de proceso		
		Case "FI007"
			lblnPost = True
			
			'+ Ventana de Fin de proceso		
		Case "GE101"
			lblnPost = insCancel
			
	End Select
	insPostFinanceSeq = lblnPost
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsFinance_co As eFinance.financeCO
	lclsFinance_co = New eFinance.financeCO
	Dim lintStatPrint As Byte
	
	If Request.Form.Item("chkPrintNow") = vbNullString Then
		lintStatPrint = 2
	Else
		lintStatPrint = 1
	End If
	mstrLocationFI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=FI001&sProject=FinanceSeq&sModule=Finance'"
	insFinish = lclsFinance_co.InsExecuteFI008(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Request.Form.Item("cboWaitCode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	lclsFinance_co = Nothing
End Function

'% insCancel: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Private Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceCO As eFinance.financeCO
	lclsFinanceCO = New eFinance.financeCO
	
	insCancel = True
	mstrLocationFI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=FI001&sProject=FinanceSeq&sModule=Finance'"
	If Request.Form.Item("optElim") = "Delete" Then
		Call lclsFinanceCO.DeleteAll(Session("nContrat"), Session("nUserCode"))
	Else
		Call lclsFinanceCO.Find(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"))
		With lclsFinanceCO
			.nStat_contr = 1
			.nUsercode = Session("nUsercode")
		End With
	End If
	lclsFinanceCO = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Finance&sProject=FinanceSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mstrLocationFI001 = vbNullString
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 16 $|$$Date: 29/09/04 17:23 $|$$Author: Nvaplat40 $"
</SCRIPT>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalFinanceSeq
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""FinanceError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			If Request.QueryString.Item("scodispl") <> "FI000" Then
				.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
				If Request.QueryString.Item("scodispl") = "FI010" Then
					.Write("self.history.go(-1);")
				End If
			Else
				.Write("self.history.go(-1);")
			End If
			.Write("</SCRIPT>")
		End With
	Else
		If insPostFinanceSeq Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If mstrLocationFI001 = vbNullString Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Finance/FinanceSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Finance/FinanceSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					End If
				Else
					
					'+ Se carga nuevamente la ventana principal de la secuencia
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.document.location=" & mstrLocationFI001 & ";</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.document.location=" & mstrLocationFI001 & ";</SCRIPT>")
					End If
				End If
			Else
				Select Case Request.QueryString.Item("sCodispl")
					Case "GE101"
						Response.Write("<SCRIPT>top.opener.top.document.location.href=" & mstrLocationFI001 & ";</SCRIPT>")
					Case "FI002", "FI003"
						'                			 If Request.Form("sCodisplReload") = vbNullString Then	
						'                                Response.Write "<NOTSCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("Index") & "&nBranch=" & Request.Form("cbeBranch") & """;</SCRIPT>"
						'                            Else
						'                                Response.Write "<NOTSCRIPT>window.close();top.opener.top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("Index") & "&nBranch=" & Request.Form("cbeBranch") & """;</SCRIPT>"
						'                            End If
						'                            Response.Write "<NOTSCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("Index") & "&nBranch=" & Request.Form("cbeBranch") & "'</SCRIPT>"
						
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&nBranch=" & Request.Form.Item("cbeBranch") & """;</SCRIPT>")
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&nBranch=" & Request.Form.Item("cbeBranch") & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&nBranch=" & Request.Form.Item("cbeBranch") & """;</SCRIPT>")
							Response.Write("<SCRIPT>top.opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&nBranch=" & Request.Form.Item("cbeBranch") & "'</SCRIPT>")
						End If
						
						
					Case "FI011"
						Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&nBranch=" & Request.Form.Item("cbeBranch") & """;</SCRIPT>")
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOption=" & Request.Form.Item("hddCalc") & "&nInitial=" & Request.Form.Item("tcnAuxInitial") & "';</SCRIPT>")
				End Select
				'Response.Write "<NOTSCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("Index") & "&sCodispl=" & Request.QueryString("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString("nMainAction") & "'</SCRIPT>"
			End If
		Else
			Response.Write("<SCRIPT>alert('No se pudo realizar actualización de " & Request.QueryString.Item("sCodispl") & "')</SCRIPT>")
		End If
	End If
Else
	'+ Se recarga la página principal de la secuencia			
	If insFinish() Then
		Response.Write("<SCRIPT>opener.top.document.location=" & mstrLocationFI001 & ";</SCRIPT>")
	End If
End If
mobjFinanceSeq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




