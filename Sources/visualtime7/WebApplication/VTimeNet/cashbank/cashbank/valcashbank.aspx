<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjCurr_acc As eCashBank.Curr_acc
Dim mobjCash_mov As eCashBank.Cash_mov
Dim mobjCheque As eCashBank.Cheque
Dim mobjCash_acc As eCashBank.Cash_acc
Dim mobjCheq_book As eCashBank.Cheq_book
Dim mobjBank_Trans As Object
Dim mobjMove_acc As eCashBank.Move_acc
Dim mobjBank_acc As eCashBank.Bank_acc
Dim mobjCash_stat As eCashBank.Cash_stat
Dim mobjDocuments As Object
Dim lclsCurr_acc As eCashBank.Curr_acc
Dim mblnSwitch As Boolean
Dim mintChange As String
Dim mintIndex As Double
Dim mintIndex2 As Byte
Dim mstrKey As String
Dim Sql As Object

'- Contador para uso genérico.
Dim mintCount As Integer

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'+ Auxiliar que contiene el número del elemento seleccionado de la colección.	
Dim mintAux As Byte

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalCashBank: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insvalCashBank() As String
	'dim eRemoteDB.Constants.intNull As String
	Dim lblnWarning As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsUser_CashNum As eCashBank.User_cashnum
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualización de cuentas bancarias y de caja		
		Case "OP004"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_acc.insValOP004(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkAccCash"), mobjValues.StringToType(.Form.Item("valAccBankCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAvailable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAccType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valBk_agency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAvailType"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctAccNumber"), mobjValues.StringToType(.Form.Item("valLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAccLedger"), .Form.Item("valAuxAccount"), mobjValues.StringToType(.Form.Item("tcnCash"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmountMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"))
				End If
			End With
			
			'+ Cheques devueltos			
		Case "OP005"
			With Request
				insvalCashBank = vbNullString
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_mov.insValOP005_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBankCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctChequeNum"))
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					insvalCashBank = mobjCash_mov.insValOP005(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdRetDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hdAccountDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ Control de cheques.
		Case "OP009"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP009_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optChequeStat"), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valClient"))
				Else
					insvalCashBank = mobjCheque.insValOP009()
				End If
			End With
			
			'+OPC013: Movimientos de cta/cte de Intermediarios
		Case "OPC013"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lclsCurr_acc = New eCashBank.Curr_acc
					insvalCashBank = mobjCurr_acc.insValOPC013_K ("OPC013",mobjValues.StringToDate(.Form("tcdEffecdate")) ,mobjValues.StringToType(.Form("cboTypeAccount"),eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(.Form("valIntermed"),eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(.Form("cboCurrency"),eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
			'+ Creación de cuentas corrientes	    
		Case "OP090"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCurr_acc.insValOP090_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussiType"), .Form.Item("valClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					insvalCashBank = mobjCurr_acc.insValOP090(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valLedgerAcc"), .Form.Item("valLedgerAux"))
				End If
			End With
			
			'+ Remesa de pago para una cuenta corriente			
		Case "OP091"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCurr_acc.insValOP091_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTypeTrans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnRemNum"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If mobjValues.StringToType(.QueryString.Item("nTypeTrans"), eFunctions.Values.eTypeData.etdDouble) <> 1 Then
						insvalCashBank = vbNullString
					Else
						insvalCashBank = mobjCurr_acc.insValOP091(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussiType"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("valClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("optCreDebAux"), mobjValues.StringToType(.Form.Item("cbeTypePay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnPayAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate))
					End If
				End If
			End With
			
			'+ Movimientos manuales para cuentas corrientes			
		Case "OP092_K"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCurr_acc.insValOP092_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussiType"), .Form.Item("valClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnTransact"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If CDbl(.QueryString.Item("nMainAction")) <> 303 And CDbl(.QueryString.Item("nMainAction")) <> 401 Then
						insvalCashBank = mobjCurr_acc.insValOP092(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTypeMov"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("txtDescript"), mobjValues.StringToType(.Form.Item("gmdValDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnCredit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("sBussiType"), eFunctions.Values.eTypeData.etdDouble, True), Session("sClient"), mobjValues.StringToType(Session("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ OP010: Actualización de chequeras
		Case "OP010"
			With Request
				lblnWarning = False
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheq_book.insValOP010_K("OP010", mobjValues.StringToType(.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdChequeDate")))
				ElseIf CDbl(.QueryString.Item("nZone")) <> 1 And CDbl(.QueryString.Item("nMainAction")) <> 401 Then 
					insvalCashBank = mobjCheq_book.insValOP010("OP010", mobjValues.StringToType(.Form.Item("tcnCheqInit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqLast"), eFunctions.Values.eTypeData.etdDouble), CBool(lblnWarning))
				End If
			End With
			
			'+ OP012: Transferencias Bancarias
		Case "OP012", "OP012I", "OP012E"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjBank_Trans.insValOP012_k("OP012I", mobjValues.StringToType(.Form.Item("tcdTransDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valOriAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountTransf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					
					If Session("TypTransf") = 1 Then
						If Session("InsPreOP012I") = False Then
							insvalCashBank = vbNullString
						Else
							insvalCashBank = mobjBank_Trans.insValFolderOP012I("OP012I", mobjValues.StringToType(.Form.Item("valIntAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOriAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchangeToLocal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrencyOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dTransdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnExchangeFromLocal"), eFunctions.Values.eTypeData.etdDouble))
						End If
					Else
						insvalCashBank = mobjBank_Trans.insValFolderOP012E("OP012I", mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctExtAccount"), .Form.Item("dtcClient"), .Form.Item("tctAbaNum"))
					End If
				End If
			End With
			
			'+ OP008: Anulación de cheques/solicitud
		Case "OP008"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP008_K("OP008", _
                                                              mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                              mobjValues.StringToType(.Form.Item("gmnCheNum"), eFunctions.Values.eTypeData.etdDouble), _
                                                              mobjValues.StringToType(.Form.Item("optNull"), eFunctions.Values.eTypeData.etdDouble), _
                                                              .Form.Item("gmtCheque"), _
                                                              CDbl(.Form.Item("gmnBordereaux")))
				Else
					insvalCashBank = mobjCheque.insValOP008("OP008", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdNullDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("optNull"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("gmtChekNumber"), eFunctions.Values.eTypeData.etdDouble, True), Session("gmtCheque"), Session("gmnBordereaux"))
				End If
			End With
			
			'+ OP015: Cambio de fecha de cheque dIferido
		Case "OP015"
			With Request
				insvalCashBank = ""
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_mov.insValOP015_k("OP015", mobjValues.StringToType(.Form.Item("cboBank"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("gmtChekNumber"))
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					insvalCashBank = mobjCash_mov.insValOP015("OP015", mobjValues.StringToDate(.Form.Item("gmdNewCollectDate")), mobjValues.StringToDate(.Form.Item("gmdOrigCollecDate")))
				End If
			End With
			
			'+ OP007: Solicitud de cheques para gastos fijos
		Case "OP007"
			With Request
				insvalCashBank = vbNullString
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP007_K("OP007", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRequeNum"), eFunctions.Values.eTypeData.etdDouble))
				ElseIf CDbl(.QueryString.Item("nMainAction")) <> 401 Then 
					insvalCashBank = mobjCheque.insValOP007("OP007", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.Form.Item("tcdDatPropos")), mobjValues.StringToType(.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDescript"), .Form.Item("dtcBenef"), .Form.Item("tctBenefname"), .Form.Item("dtcInterm"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQuanPay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayFrec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.Form.Item("tcdIssueDat")), mobjValues.StringToDate(.Form.Item("tcdLedgerDat")), mobjValues.StringToType(.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Consultas
		Case "OPC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_mov.insValOPC001_K("OPC001", mobjValues.StringToType(.Form.Item("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_end"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCashnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeMovType"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
			'+ OPC010: Consulta de cuentas corrientes
		Case "OPC010"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjMove_acc.insValOPC010_K("OPC010", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBussType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("tctTypeCurracc"), "2", mobjValues.StringToType(.Form.Item("tcnCertIf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), CInt("1"))
					
				End If
			End With
			'+ Consulta de movimientos de prima de una cuenta corriente
			
		Case "OPC011"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjMove_acc.insValOPC011_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ OPC012 : Movimientos de c/ctes asociados a sin
		Case "OPC012"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjMove_acc.insValOPC012_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cboTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
			'+ Consulta de movimientos de pagos recibidos por la compañía de seguros
			
		Case "OPC015"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjMove_acc.insValOPC015_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussType"), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ OPC014: Mov. de pagos realizados por la compañía
		Case "OPC014"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjMove_acc.insValOPC014_k("OPC014", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussType"), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					
				End If
			End With
			'+ OPC002: Consulta de Cheques/Solicitudes
		Case "OPC002"
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					If CDbl(.Form.Item("hoptInfType")) = 1 Then
						Session("opt") = 1
					Else
						Session("opt") = 2
					End If
					Session("nAccountNumber") = .Form.Item("gmnAccountAUX")
					insvalCashBank = mobjBank_acc.insValOPC002("OPC002", 402, mobjValues.StringToType(.Form.Item("hgmnAccount"), eFunctions.Values.eTypeData.etdDouble), Session("opt"), .Form.Item("tctcheque"), mobjValues.StringToType(.Form.Item("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboSta_cheque"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDat_propos"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdIssue_dat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cboConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("tctCliename"))
					
					
					Session("optInfType") = mobjValues.StringToType(.Form.Item("hoptInfType"), eFunctions.Values.eTypeData.etdDouble)
					Session("tctAccountNumber") = .Form.Item("gmnAccountAUX") '.Form("hgmnAccount")
					
					mstrString = "&nAcc_bank=" & mobjValues.StringToType(.Form.Item("tctAccountNumber"), eFunctions.Values.eTypeData.etdDouble) & "&sCheque=" & .Form.Item("tctcheque") & "&nRequest_nu=" & mobjValues.StringToType(.Form.Item("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble) & "&nSta_cheque=" & mobjValues.StringToType(.Form.Item("cboSta_cheque"), eFunctions.Values.eTypeData.etdDouble) & "&nAmount=" & mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble) & "&dDat_propos=" & mobjValues.StringToType(.Form.Item("tcdDat_propos"), eFunctions.Values.eTypeData.etdDate) & "&dIssue_dat=" & mobjValues.StringToType(.Form.Item("tcdIssue_dat"), eFunctions.Values.eTypeData.etdDate) & "&nConcept=" & mobjValues.StringToType(.Form.Item("cboConcept"), eFunctions.Values.eTypeData.etdDouble) & "&sClient=" & .Form.Item("tctClient")
					
					
					mobjBank_acc = Nothing
				End If
			End With
			
			'+ Consulta de cheques a fecha
		Case "OPC717"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_mov.insValOPC717_K("OPC717", mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeChequeLocat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCheque_stat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDocnumbe"), .Form.Item("OptTypeInfo"))
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
			'+ Cartera de Cheques							
		Case "OP752"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCash_mov.insValOP752_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valMoveType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateMove"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctChequeNum"), .Form.Item("tctDep_number"), mobjValues.StringToType(.Form.Item("valBankAccount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate))
				Else
					'+ Si se trata de un depósito o un redepósito
					If Session("nMoveType") = 7 Or Session("nMoveType") = 8 Then
                        Dim countSel as integer = 0
                        If Not IsNothing(.Form.GetValues("Sel")) Then
                            countSel = .Form("Sel").Split(",").Length
                        End If
						insvalCashBank = mobjCash_mov.insValOP752Msg(.QueryString.Item("sCodispl"), countSel)
					Else
						insvalCashBank = mobjCash_mov.insValOP752(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Session("nMoveType"), Session("dDateMove"), Session("nBank_Code"), Session("sChequeNum"), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateDoc"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("chkPostCheque"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBeneficiary"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCashNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeReason"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypeReplace"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBankReplace"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctChequeNumReplace"), mobjValues.StringToType(.Form.Item("tcdDatePro"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
		Case "OPC720"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalCashBank = mobjCash_stat.insValOPC720(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnCashnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCash_id"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitCloseCash"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndCloseCash"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCloseOkCash"), eFunctions.Values.eTypeData.etdDate))
					
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
			'+ Aprobación de Ordenes de Pago
		Case "OP714"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP714_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valAccBank"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If IsNothing(.Form.Item("tcnSwitch")) Then
						mblnSwitch = False
					Else
						mblnSwitch = True
					End If
					
					If Not IsNothing(.Form.Item("Sel")) Then
						For	Each mintChange In .Form.GetValues("Sel")
							mintIndex = mintIndex + 1
							mintIndex2 = CDbl(mintChange) + 1
							If mintChange <> eRemoteDB.Constants.intNull Then
								mblnSwitch = False
							End If
						Next mintChange
					End If
					insvalCashBank = mobjCheque.insValOP714(.QueryString.Item("sCodispl"), mblnSwitch)
				End If
			End With
			
			'+ Relación de Ordenes de Pago
		Case "OP715"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP715_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPayOrdBord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
						If IsNothing(.Form.Item("tcnSwitch")) Then
							mblnSwitch = False
						Else
							mblnSwitch = True
						End If
						
						If Not IsNothing(.Form.Item("Sel")) Then
							For	Each mintChange In .Form.GetValues("Sel")
								mintIndex = mintIndex + 1
								mintIndex2 = CDbl(mintChange) + 1
								If mintChange <> eRemoteDB.Constants.intNull Then
									mblnSwitch = False
								End If
							Next mintChange
						End If
						insvalCashBank = mobjCheque.insValOP715(.QueryString.Item("sCodispl"), mblnSwitch)
					Else
						insvalCashBank = vbNullString
					End If
				End If
			End With
			
			'+ Notificación STS
		Case "OP716"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalCashBank = mobjCheque.insValOP716_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.insGetSetting("Active", "No", "ExtensionSTS"))
				Else
					insvalCashBank = ""
				End If
			End With

			'+OPC824: Consulta de relaciones por caja
		Case "OPC824"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lclsUser_CashNum = New eCashBank.User_cashnum
					insvalCashBank = lclsUser_CashNum.insValOPC824(.QueryString.Item("sCodispl"), mobjValues.StringToDate(.Form.Item("tcdCollect")), mobjValues.StringToType(.Form.Item("valCashNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatus"))
					lclsUser_CashNum = Nothing
				Else
					insvalCashBank = vbNullString
				End If
			End With
			
		Case Else
			insvalCashBank = "insvalCashBank: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostCashBank: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostCashBank() As Boolean
	'dim eRemoteDB.Constants.intNull As String
	Dim lblnCheqRangeChange As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lintCount As Byte
	Dim minvChange As Object
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lstrMessage As String
	
	lblnPost = False
	Dim lclsT_PayCla As eClaim.T_PayCla
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualización de cuentas bancarias y de caja	
		Case "OP004"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If CDbl(.QueryString.Item("nMainAction")) = 401 Then
						lblnPost = True
					Else
						lblnPost = mobjCash_acc.insPostOP004(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAccBankCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAccType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAvailable"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOldCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOldOffice"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAccNumber"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valBk_agency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAvailType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTransit1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTransit2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTransit3"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTransit4"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTransit5"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAccLedger"), .Form.Item("valAuxAccount"), mobjValues.StringToType(.Form.Item("tcnCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble))
						
					End If
					Session("nAccBankCash") = .Form.Item("valAccBankCash")
					Session("nOffice") = .Form.Item("cbeOffice")
					Session("nCurrency") = .Form.Item("cbeCurrency")
					Session("nCash_num") = .Form.Item("tcnCash")
					Session("nCompany") = .Form.Item("cbeCompany")
				End If
			End With
			
			'+ Cheques devueltos			
		Case "OP005"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nBankCode") = .Form.Item("tcnBankCode")
					Session("sChequeNum") = .Form.Item("tctChequeNum")
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					lblnPost = mobjCash_mov.insPostOP005(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hnAccountCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hnOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hnTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdRetDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCashAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Control de cheques.
		Case "OP009"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("dStartDate") = mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate)
					Session("dEndDate") = mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
					Session("optChequeStat") = .Form.Item("optChequeStat")
					Session("nConcept") = mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sClient") = .Form.Item("valClient")
					
					lblnPost = True
				Else
					lblnPost = True
                    If Not IsNothing(.Form.Item("Sel")) Then
					    For mintCount = 1 To .Form.Item("Sel").Split(",").Length
						    '+ Se obtiene el índice correspondiente a la selección.	                    
						    mintAux = CDbl(.Form.GetValues("Sel").GetValue(mintCount - 1)) + 1
						
						    lblnPost = mobjCheque.insPostOP009(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), Session("optChequeStat"), mobjValues.StringToType(.Form.GetValues("nRequest_nu").GetValue(mintAux - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("sCheck").GetValue(mintAux - 1), mobjValues.StringToType(.Form.GetValues("nConsec").GetValue(mintAux - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					    Next
                    End If 
				End If
			End With
			
		Case "OP090"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nTypeAccount") = .Form.Item("cbeTypeAccount")
					Session("sBussiType") = .Form.Item("cbeBussiType")
					Session("sClient") = .Form.Item("valClient")
					Session("nCurrency") = .Form.Item("cbeCurrency")
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					lblnPost = mobjCurr_acc.insPostOP090(mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Session("sBussiType"), Session("sClient"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valLedgerAcc"), CDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valLedgerAux"))
				End If
			End With
			
		Case "OP091"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nTypeTrans=" & mobjValues.StringToType(.Form.Item("cbeTypeTrans"), eFunctions.Values.eTypeData.etdDouble) & "&nRemNum=" & mobjValues.StringToType(.Form.Item("gmnRemNum"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					' Los parámetros que se pasan a partir del "nRequest_nu" deben ser obtenidos de la OP006 (por enlace)
					Select Case mobjValues.StringToType(.QueryString.Item("nTypeTrans"), eFunctions.Values.eTypeData.etdDouble)
						Case 1
							If CStr(Session("sCheque")) = vbNullString Then
								Session("sCheque") = " "
							End If
							
							If Session("nRequest_nu") = eRemoteDB.Constants.intNull Or CStr(Session("nRequest_nu")) = vbNullString Then
								Session("nRequest_nu") = 0
							End If
							Session("OP006_nConcept") = 23 'Remesa de cuenta corriente
							Session("OP006_sCodispl") = "OP091"
							Session("OP006_sBenef") = .Form.Item("valClient")
							Session("OP006_dReqDate") = .Form.Item("tcdEffecdate")
							
							Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/common/GoTo.aspx?sCodispl=" & "OP06-2" & "&nTyp_Acco=" & mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble, True) & "&sType_Acc=" & .Form.Item("cbeBussiType") & "&sClient=" & .Form.Item("valClient") & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True) & "&nCurrencypay=1" & "&nAmount=" & mobjValues.StringToType(.Form.Item("gmnPayAmount"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecDate=" & mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate) & "&nProcess=" & .QueryString.Item("nTypeTrans") & "&nPayOrderTyp=" & mobjValues.StringToType(.Form.Item("cbeTypePay"), eFunctions.Values.eTypeData.etdDouble, True) & "&nTypeTrans=" & .QueryString.Item("nTypeTrans") & "&nRemNum=" & .QueryString.Item("nRemNum") & "&nConcept=" & Session("OP006_nConcept") & "&dDateIncrease=" & mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate) & "&sCodispl_Origi=" & Session("OP006_sCodispl") & """;</" & "Script>")
							
						Case 3
							lblnPost = True
						Case Else
							Call mobjMove_acc.Find_document(4, mobjValues.StringToType(Request.QueryString.Item("nRemNum"), eFunctions.Values.eTypeData.etdDouble))
							lblnPost = mobjCurr_acc.insPostOP091(mobjMove_acc.nTyp_acco, mobjMove_acc.sType_acc, mobjMove_acc.sClient, mobjMove_acc.nCurrency, mobjMove_acc.nAmount, mobjMove_acc.dEffecdate, mobjValues.StringToType(Request.QueryString.Item("nRemNum"), eFunctions.Values.eTypeData.etdDouble), 2, mobjMove_acc.nRequest_nu, 0, mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypeTrans"), eFunctions.Values.eTypeData.etdDouble), 0, 0, "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, "", 1, mobjValues.StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), 9998, mobjMove_acc.sClient, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
					End Select
				End If
			End With
			
		Case "OP092_K"
			With Request
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nTypeAccount") = .Form.Item("cbeTypeAccount")
					Session("sBussiType") = .Form.Item("cbeBussiType")
					Session("sClient") = .Form.Item("valClient")
					Session("sCliename") = .Form.Item("lblCliename")
					Session("nCurrency") = .Form.Item("cbeCurrency")
					Session("nTransact") = .Form.Item("gmnTransact")
					Session("dEffecdate") = .Form.Item("gmdEffecdate")
					Session("nIntermed") = .Form.Item("valIntermedia")
					lblnPost = True
				Else
					lblnPost = mobjCurr_acc.insPostOP092(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Session("sBussiType"), Session("sClient"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransact"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeMov"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("txtDescript"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdValDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnCredit"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optAmoCreDeb"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
					If lblnPost Then
						Session("nTypeAccount") = ""
						Session("sBussiType") = ""
						Session("sClient") = ""
						Session("sCliename") = ""
						Session("nCurrency") = ""
						Session("nTransact") = ""
						Session("dEffecdate") = ""
						Session("nIntermed") = ""
					End If
				End If
			End With
			
			'+ OP010: Actualización de chequeras
		Case "OP010"
			lblnPost = True
			With Request
				If CShort(.Form.Item("tcnCheqRangeChange")) = 1 Then
					lblnCheqRangeChange = True
				Else
					lblnCheqRangeChange = False
				End If
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nAcc_bank") = .Form.Item("valAccountNum")
					Session("dEffecdate") = .Form.Item("tcdChequeDate")
				ElseIf CDbl(.QueryString.Item("nZone")) <> 1 And CDbl(.QueryString.Item("nMainAction")) <> 401 Then 
					lblnPost = mobjCheq_book.insPostOP010("OP010", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqInit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqLast"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqDan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheqCancel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), CBool(lblnCheqRangeChange))
				End If
			End With
			
			'+ OP012: Transferencias Bancarias
		Case "OP012", "OP012I", "OP012E"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("dTransdate") = mobjValues.StringToType(.Form.Item("tcdTransDate"), eFunctions.Values.eTypeData.etdDate)
					Session("TypTransf") = mobjValues.StringToType(.Form.Item("optTrans"), eFunctions.Values.eTypeData.etdDouble)
					Session("nOriAccount") = mobjValues.StringToType(.Form.Item("valOriAccount"), eFunctions.Values.eTypeData.etdDouble)
					Session("nAmountTransf") = mobjValues.StringToType(.Form.Item("tcnAmountTransf"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCurrencyOri") = mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
					mstrString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&sProcessType=" & .QueryString.Item("sProcessType") & "&nAmount=" & .QueryString.Item("nAmount") & "&nInterest=" & .QueryString.Item("nInterest") & "&sClient=" & .QueryString.Item("sClient") & "&nPayOrderTyp=" & .QueryString.Item("nPayOrderTyp") & "&nAmoTax=" & .QueryString.Item("nAmoTax") & "&nAgency=" & .QueryString.Item("nAgency") & "&nRequestNu=" & .QueryString.Item("nRequestNu")
				Else
					If Session("TypTransf") = 1 Then
						If Session("InsPreOP012I") = False Then
							lblnPost = True
						Else
							lblnPost = mobjBank_Trans.insPostFolderOP012(mobjValues.StringToType(Session("nOriAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dTransdate"), eFunctions.Values.eTypeData.etdDate), 0, "", 0, 0, "", mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nAmountTransf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountNew"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAbaNum"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), Session("TypTransf"))
						End If
					Else
						If CStr(Session("OP006_sCodispl")) <> "OP06-6" Then
							lblnPost = mobjBank_Trans.insPostFolderOP012(Session("nOriAccount"), 0, Session("dTransdate"), mobjValues.StringToType(.Form.Item("cbeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctExtAccount"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), 0, Session("nAmountTransf"), 0, "", Session("nUserCode"), Session("TypTransf"))
							If lblnPost Then
								If CStr(Session("OP006_sCodispl")) = "VI011" Then
									mobjBank_Trans = Nothing
									mobjBank_Trans = New ePolicy.Loans
									With Request
										lblnPost = mobjBank_Trans.insPostVI011(Session("OP006_sCodispl"), eFunctions.Menues.TypeActions.clngAcceptDataFinish, "2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInterest"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, .QueryString("sClient"), mobjValues.StringToType(.QueryString.Item("nPayOrderTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAmoTax"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, vbNullString, Session("sTypeCompanyUser"), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.QueryString.Item("nRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Session("SessionID"))
										
										If lblnPost Then
											lclsGeneral = New eGeneral.GeneralFunction
											lstrMessage = lclsGeneral.insLoadMessage(55907) & " Nro.: " & mobjBank_Trans.nCode
											Response.Write("<SCRIPT>alert(""Men. 55907: " & lstrMessage & """);</" & "Script>")
											lclsGeneral = Nothing
											
											Call insPrintDocuments()
										End If
									End With
								End If
							End If
						Else
							lclsT_PayCla = New eClaim.T_PayCla
							                            lblnPost = lclsT_PayCla.insPostSI773(Session("OP006_nClaim"), Session("OP006_nCase_Num"), Session("OP006_nDeman_type"), Session("OP006_sClient"), Session("OP006_nId"), Session("OP006_nRequest_nu"), eRemoteDB.Constants.intNull, Session("nAmountTransf"), Session("OP006_nConcept"), .Form.Item("dtcClient"), Session("dTransdate"), Session("OP006_sDescript"), Session("dTransdate"), Session("dTransdate"), Session("OP006_sRequest_ty"), eRemoteDB.Constants.intNull, Session("nUserCode"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("OP006_nCurrencyOri"), Session("OP006_nCurrencyPay"), Session("OP006_nAmountPay"), Session("OP006_nOffice"), Session("OP006_nCompany"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nAmountTransf"), Session("OP006_nOffice"), Session("OP006_sKey"), Session("nOriAccount"), mobjValues.StringToType(.Form.Item("cbeAccount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctExtAccount"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAbaNum"), String.Empty, String.Empty)
							lclsT_PayCla = New eClaim.T_PayCla
						End If
					End If
				End If
			End With
			
			'+ OP008: Anulación de cheques/Solicitudes
		Case "OP008"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("gmnCheque") = mobjValues.StringToType(.Form.Item("gmnCheNum"), eFunctions.Values.eTypeData.etdDouble)
					Session("gmnBordereaux") = mobjValues.StringToType(.Form.Item("gmnBordereaux"), eFunctions.Values.eTypeData.etdDouble)
					Session("gmtCheque") = .Form.Item("gmtCheque")
					Session("optNull") = .Form.Item("optNull")
					
					lblnPost = True
				Else
					lintCount = 0
					If Not IsNothing(Request.Form.Item("Sel")) Then
						For	Each mintChange In Request.Form.GetValues("Sel")
							lintCount = CDbl(mintChange) + 1
							
							lblnPost = mobjCheque.insPostOP008(mobjValues.StringToType(.QueryString.Item("nMainaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCause"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.Form.Item("gmdNullDate")), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddRequest").GetValue(lintCount - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddCheque").GetValue(lintCount - 1), Session("gmnBordereaux"))
						Next mintChange
                    Else
                        lblnPost = True
					End If
				End If
				lintCount = Nothing
			End With
			
			'+ OPC013: Movimientos de cta/cte de Intermediarios
		Case "OPC013"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&sCliename=" & lclsCurr_acc.sClienames & "&sClient=" & lclsCurr_acc.sClient & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sTypeAcco=" & .Form.Item("cboTypeAccount") & "&nIntermed=" & .Form.Item("valIntermed") & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			
			'+ OP015: Cambio de fecha de cheque dIferido
		Case "OP015"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("gmnCheNum") = .Form.Item("gmtChekNumber")
					Session("cboBank") = .Form.Item("cboBank")
				ElseIf .QueryString.Item("nMainAction") <> "401" Then 
					lblnPost = mobjCash_mov.insPostOP015(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAcc_cash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmtCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("gmdNewCollectDate")), mobjValues.StringToDate(.Form.Item("dEffecdate")), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ OP007: Solicitud de cheques para gastos fijos
		Case "OP007"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nRequest_nu") = .Form.Item("tcnRequeNum")
				ElseIf CDbl(.QueryString.Item("nMainAction")) <> 401 Then 
					lblnPost = mobjCheque.insPostOP007(CShort(.QueryString.Item("nMainAction")), mobjValues.StringToType(Session("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcBenef"), mobjValues.StringToDate(.Form.Item("tcdDatPropos")), .Form.Item("tctDescript"), mobjValues.StringToDate(.Form.Item("tcdIssueDat")), mobjValues.StringToDate(.Form.Item("tcdLedgerDat")), .Form.Item("cbePayFrec"), mobjValues.StringToType(.Form.Item("tcnQuanPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcInterm"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nVoucher_le"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Consultas			
			'+ OPC001: Consulta de Caja
		Case "OPC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nType_mov=" & mobjValues.StringToType(.Form.Item("cbeMovType"), eFunctions.Values.eTypeData.etdDouble, True) & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&nOffice=" & mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble) & "&dDate_ini=" & mobjValues.StringToType(.Form.Item("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate) & "&dDate_end=" & mobjValues.StringToType(.Form.Item("tcdDate_end"), eFunctions.Values.eTypeData.etdDate) & "&nCashnum=" & mobjValues.StringToType(.Form.Item("tcnCashnum"), eFunctions.Values.eTypeData.etdDouble) & "&nConcept=" & mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble)
					'    			Session("nType_mov") = mobjValues.StringToType(.Form("cbeMovType"),eFunctions.Values.eTypeData.etdDouble,True) 
					'   			Session("nCurrency") = mobjValues.StringToType(.Form("cbeCurrency"),eFunctions.Values.eTypeData.etdDouble)
					'  			Session("nOffice")   = mobjValues.StringToType(.Form("cbeOffice"),eFunctions.Values.eTypeData.etdDouble)
					' 			Session("dDate_ini") = mobjValues.StringToType(.Form("tcdDate_ini"),eFunctions.Values.eTypeData.etdDate)
					'			Session("dDate_end") = mobjValues.StringToType(.Form("tcdDate_end"),eFunctions.Values.eTypeData.etdDate)
					'			Session("nCashnum")  = mobjValues.StringToType(.Form("tcnCashnum"),eFunctions.Values.eTypeData.etdDouble)
					'			Session("nConcept")  = mobjValues.StringToType(.Form("cbeConcept"),eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			
			'+ OPC010: Consulta de cuentas corrientes
		Case "OPC010"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nTyp_acco=" & .Form.Item("cbeTypeAccount") & "&sType_acc=" & .Form.Item("cbeBussType") & "&sClient=" & .Form.Item("dtcClient") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertIf=" & .Form.Item("tcnCertIf") & "&nCurrency=" & .Form.Item("cbeCurrency")
				End If
			End With
			'+ OPC011: Consulta de movimientos de prima de cuentas corrientes
		Case "OPC011"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dEffecdate=" & .Form.Item("gmdEffecdate") & "&nTypeAccount=" & mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble) & "&sClient=" & .Form.Item("valClient") & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			
			'+ OPC012 : Movimientos de c/ctes asociados a sin
		Case "OPC012"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("nType_acco") = .Form.Item("cboTypeAccount")
					Session("sType_acc") = 0
					Session("sClient") = .Form.Item("tctClient")
					Session("nCurrency") = .Form.Item("cboCurrency")
					Session("dOperdate") = .Form.Item("tcdOperdate")
				End If
				lblnPost = True
			End With
			
			'+ OPC015: Consulta de movimientos de pagos recibidos por la compañía de seguros
		Case "OPC015"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nTypeAccount=" & mobjValues.StringToType(.Form.Item("cbeTypeAccount"), eFunctions.Values.eTypeData.etdDouble) & "&sBussiType=" & .Form.Item("cbeBussType") & "&sClient=" & .Form.Item("dtcClient") & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			
			'+ OPC014: Mov. de pagos realizados por la compañía
		Case "OPC014"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nTyp_acco=" & .Form.Item("cbeTypeAccount") & "&sType_acc=" & .Form.Item("cbeBussType") & "&sClient=" & .Form.Item("dtcClient") & "&nCurrency=" & .Form.Item("cbeCurrency")
				End If
			End With
			
			'+ OPC002: Consulta de Cheques/Solicitudes.
			
		Case "OPC002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					Session("sql") = Sql
					lblnPost = True
				Else
					lblnPost = True
					If Request.Form.Item("optInfType") = "2" Then
						Session("optInfType") = 2
					End If
				End If
				
				mobjBank_acc = Nothing
			End With
			
			'+ OPC717: Consulta de Cheques a Fecha
		Case "OPC717"
			lblnPost = True
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				Session("dStartDate") = mobjValues.StringToType(Request.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate)
				Session("dEndDate") = mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
				Session("nCurrency") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
				Session("nBank") = mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("nChequeLocat") = mobjValues.StringToType(Request.Form.Item("cbeChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("nCheque_stat") = mobjValues.StringToType(Request.Form.Item("cbeCheque_stat"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("sDocnumbe") = Request.Form.Item("tctDocnumbe")
				Session("sTypeInfo") = Request.Form.Item("optTypeInfo")
				Session("nCard_Type") = mobjValues.StringToType(Request.Form.Item("cbeCard_Type"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("sSupervisor") = Request.Form.Item("chkSupervisor")
			Else
				insPrintDocuments()
			End If
			
			'+ OP752: Cartera de Cheques
		Case "OP752"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nMoveType") = mobjValues.StringToType(.Form.Item("valMoveType"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("dDateMove") = mobjValues.StringToType(.Form.Item("tcdDateMove"), eFunctions.Values.eTypeData.etdDate)
					Session("nBank_Code") = mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sChequeNum") = .Form.Item("tctChequeNum")
					mstrString = "&nTypeDocu=" & .Form.Item("optTypeDocu") & "&sDep_number=" & .Form.Item("tctDep_number") & "&dExpirdat=" & .Form.Item("tcdExpirdat") & "&nChequeLocat=" & .Form.Item("cbeChequeLocat") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&nBank_code=" & .Form.Item("valBankAccount_nBank_code") & "&nBankAccount=" & .Form.Item("valBankAccount")
					lblnPost = True
				Else
					mobjCash_mov = New eCashBank.Cash_mov
					'+ Si se trata de un depósito o un redepósito
					If Session("nMoveType") = 7 Or Session("nMoveType") = 8 Then
						lblnPost = mobjCash_mov.insPostOP752Msg(mobjValues.StringToType(Request.QueryString.Item("nTypeDocu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dDateMove"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDep_number"), mobjValues.StringToType(Request.QueryString.Item("nBankAccount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nMoveType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddSel"), Request.Form.Item("hddAmount"), Request.Form.Item("hddCheque"), Request.Form.Item("hddBordereaux"), Request.Form.Item("hddDoc_date"), Request.Form.Item("hddTransac"), Request.Form.Item("hddOffice"), Request.Form.Item("hddCashnum"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddChequeLocat"), Request.Form.Item("hddBank"))
					Else
						lblnPost = mobjCash_mov.insPostOP752(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Session("nMoveType"), Session("dDateMove"), Session("nBank_Code"), Session("sChequeNum"), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateDoc"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("chkPostCheque"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBeneficiary"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCashNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeReason"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypeReplace"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBankReplace"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctChequeNumReplace"), mobjValues.StringToType(.Form.Item("tcdDatePro"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
					mstrKey = mobjCash_mov.sKey
					Call insPrintDocuments()
					
				End If
			End With
			
			'+ OPC720: Estados de cajas
		Case "OPC720"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mstrString = "&sCashnum=" & .Form.Item("tcnCashnum") & "&sStartdate=" & .Form.Item("tcdStartdate") & "&sStatus=" & .Form.Item("cbeStatus") & "&sCash_id=" & .Form.Item("tcnCash_id") & "&sOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&sInitCloseCash=" & .Form.Item("tcdInitCloseCash") & "&sEndCloseCash=" & .Form.Item("tcdEndCloseCash") & "&sCloseOkCash=" & .Form.Item("tcdCloseOkCash")
				End If
			End With
			
			
			'+OP714: Aprobación de Ordenes de Pago			
		Case "OP714"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nCompany=" & mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True) & "&nAcc_Bank=" & .Form.Item("valAccBank") & "&nConcept=" & mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True) & "&dStartDate=" & .Form.Item("tcdStartDate") & "&dEndDate=" & .Form.Item("tcdEndDate") & "&nTypeOper=" & mobjValues.StringToType(.Form.Item("optTypeOper"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					mintIndex = 0
					
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
                        lblnPost = True
						lclsGeneral = New eGeneral.GeneralFunction
						mstrKey = lclsGeneral.getsKey(Session("nUserCode"))
						lclsGeneral = Nothing
						If Not IsNothing(.Form.Item("Sel")) Then
							For	Each mintChange In .Form.GetValues("Sel")
								mintIndex = mintIndex + 1
                                    mintIndex2 = CDbl(mintChange) + 1
								If mintChange <> eRemoteDB.Constants.intNull Then
									lblnPost = mobjCheque.insPostOP714(mobjValues.StringToType(.Form.GetValues("nRequestNu").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("sCheque").GetValue(mintIndex2 - 1), mobjValues.StringToType(.Form.GetValues("nConsec").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mstrKey,mobjValues.StringToType(.Form.GetValues("nAcc_Bank").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble))
								End If
							Next mintChange
						End If
						Call insPrintDocuments()
					End If
				End If
			End With
			
			'+OP715: Relación de Ordenes de Pago			
		Case "OP715"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nPayOrdBord=" & mobjValues.StringToType(.Form.Item("tcnPayOrdBord"), eFunctions.Values.eTypeData.etdDouble) & "&nCompany=" & mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True) & "&nConcept=" & mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True) & "&dStartDate=" & mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate) & "&dEndDate=" & mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate) & "&nTypeOper=" & mobjValues.StringToType(.Form.Item("optTypeOper"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					mintIndex = 0
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
						If Not IsNothing(.Form.Item("Sel")) Then
							For	Each mintChange In .Form.GetValues("Sel")
								mintIndex = mintIndex + 1
								mintIndex2 = CDbl(mintChange) + 1
								If mintChange <> eRemoteDB.Constants.intNull Then
									lblnPost = mobjCheque.insPostOP715(mobjValues.StringToType(.Form.Item("HddPayOrdBord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("nRequestNu").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("sCheque").GetValue(mintIndex2 - 1), mobjValues.StringToType(.Form.GetValues("nConsec").GetValue(mintIndex2 - 1), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
								End If
							Next mintChange
						End If
						If mblnSwitch = False Then
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+OP716: Notificación STS
		Case "OP716"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&dStartDate=" & mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate) & "&dEndDate=" & mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate) 
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
					    lblnPost = True
					Else
						lblnPost = True
					End If
				End If
			End With

			'+ OPC824: Consulta de Relaciones por caja
		Case "OPC824"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nCashnum=" & .Form.Item("valCashNum") & "&dCollect=" & .Form.Item("tcdCollect") & "&sStatus=" & .Form.Item("cbeStatus")
				End If
			End With
			
	End Select
	insPostCashBank = lblnPost
End Function

'**% insPrintDocuments: Document printing
'%   insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	
	mobjDocuments = New eReports.Report
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			
			'+ Listado de Cheques a Fecha
			Case "OPC717"
				
				
				.sCodispl = "OPL717"
				.ReportFilename = "OPL717.rpt"
				
				If Session("dStartDate") = eRemoteDB.Constants.dtmNull Then
					.SetStorProcParam(1, vbNullString)
				Else
					.SetStorProcParam(1, .setdate(Session("dStartDate")))
				End If
				
				If Session("dEndDate") = eRemoteDB.Constants.dtmNull Then
					.SetStorProcParam(2, vbNullString)
				Else
					.SetStorProcParam(2, .setdate(Session("dEndDate")))
				End If
				
				.SetStorProcParam(3, Session("nCurrency"))
				
				If Session("nBank") = eRemoteDB.Constants.intNull Then
					.SetStorProcParam(4, 0)
				Else
					.SetStorProcParam(4, Session("nBank"))
				End If
				
				If Session("nChequeLocat") = eRemoteDB.Constants.intNull Then
					.SetStorProcParam(5, 0)
				Else
					.SetStorProcParam(5, Session("nChequeLocat"))
				End If
				
				If CStr(Session("sDocnumbe")) = vbNullString Then
					.SetStorProcParam(6, vbNullString)
				Else
					.SetStorProcParam(6, Session("sDocnumbe"))
				End If
				
				If Session("nCheque_stat") = eRemoteDB.Constants.intNull Then
					.SetStorProcParam(7, 0)
				Else
					.SetStorProcParam(7, Session("nCheque_stat"))
				End If
				.SetStorProcParam(8, Session("sTypeInfo"))
				If Session("nCard_Type") = eRemoteDB.Constants.intNull Then
					.SetStorProcParam(9, 0)
				Else
					.SetStorProcParam(9, Session("nCard_Type"))
				End If
				.SetStorProcParam(10, Session("sSupervisor"))
				Response.Write((.Command))
				
				'+ Impresión de órdenes de pago aprobadas
			Case "OP714"
				.sCodispl = "OPL714"
				.ReportFilename = "OPL714.rpt"
				.setParamField(1, "Desde", Request.Form.Item("HddStartDate"))
				.setParamField(2, "Hasta", Request.Form.Item("HddEndDate"))
				.SetStorProcParam(1, mstrKey)
				Response.Write((.Command))
				
				'+ Operaciones de cartera de cheques
			Case "OP752"
				.sCodispl = "OP752"
				.ReportFilename = "OPL752.rpt"
				.SetStorProcParam(1, mstrKey)
				Response.Write((.Command))
				
			Case Else
				If CStr(Session("OP006_sCodispl")) = "VI011" Then
					.ReportFilename = "VIL011.rpt"
					.sCodispl = "VIL011"
					.SetStorProcParam(1, "TMP" & Session("SessionID") & Session("nUsercode"))
					Response.Write((.Command))
				End If
		End Select
	End With
	mobjDocuments = Nothing
End Sub

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



		
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>
</HEAD>
<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 14/10/04 9:48 $|$$Author: Mmmiola $"

function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mstrString = ""
mstrCommand = "&sModule=CashBank&sProject=CashBank&sCodisplReload=" & Request.QueryString.Item("sCodispl")

With Server
	mobjCurr_acc = New eCashBank.Curr_acc
	mobjValues = New eFunctions.Values
	mobjCash_mov = New eCashBank.Cash_mov
	mobjCheque = New eCashBank.Cheque
	mobjCash_acc = New eCashBank.Cash_acc
	mobjCheq_book = New eCashBank.Cheq_book
	mobjBank_Trans = New eCashBank.Bank_trans
	mobjMove_acc = New eCashBank.Move_acc
	mobjBank_acc = New eCashBank.Bank_acc
	mobjCash_stat = New eCashBank.Cash_stat
End With

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalCashBank
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CashBankError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostCashBank() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptDataFinish) Then
				If Request.QueryString.Item("sCodispl") = "OP752" And Session("nMoveType") = 4 Then
					'Session("nCurrency_OP752") = Request.Form("cbeCurrency")
					'Session("nAmount_OP752") = Request.Form("tcnAmount")
					'Session("nCompany_OP752") = Request.Form("valCompany")
					'Response.Write "<NOTSCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=OP001';</SCRIPT>"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();top.opener.top.document.location.reload();</SCRIPT>")
					End If
				Else
					If CStr(Session("OP006_sCodispl")) = "OP06-1" Or CStr(Session("OP006_sCodispl")) = "OP012I" Then
						If Request.QueryString.Item("sCodisplReload") = "OP012I" Then
							Response.Write("<SCRIPT>opener.top.document.frames['fraHeader'].location.href=""/VTimeNet/common/GoTo.aspx?sCodispl=OP06-1"";</SCRIPT>")
							Response.Write("<SCRIPT>window.close();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/common/GoTo.aspx?sCodispl=OP06-1"";</SCRIPT>")
							Session("OP006_sCodispl") = vbNullString
						End If
					Else
						If CStr(Session("OP006_sCodispl")) = "OP06-6" Then
							Response.Write("<SCRIPT>top.close();</SCRIPT>")
						Else
							If CStr(Session("OP006_sCodispl")) = "VI011" Then
								Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & "';</SCRIPT>")
							Else
								If Request.Form.Item("sCodisplReload") = vbNullString Then
									Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
								Else
									Response.Write("<SCRIPT>window.close();top.opener.top.document.location.reload();</SCRIPT>")
								End If
							End If
						End If
					End If
				End If
			Else
				If Request.QueryString.Item("sCodisplReload") = "OP004" Then
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				ElseIf Request.QueryString.Item("sCodispl") = "OP004" Then 
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				End If
				If Request.QueryString.Item("sCodispl") <> "OPC720" Then
					If Request.QueryString.Item("sCodispl") <> "OP002" Then
						If Request.QueryString.Item("sCodispl") <> "OP004" Then
							If Request.QueryString.Item("sCodispl") = "OP012" Then
								If Session("TypTransf") = 1 Then
									Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase("OP012I"), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=OP012I"";</SCRIPT>")
									Response.Write("<SCRIPT>top.frames['fraSequence'].pstrOnSeq = '1'</SCRIPT>")
								Else
									Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase("OP012I"), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=OP012E"";</SCRIPT>")
									Response.Write("<SCRIPT>top.frames['fraSequence'].pstrOnSeq = '1'</SCRIPT>")
								End If
							Else
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							End If
						End If
					Else
						If Request.QueryString.Item("sCodispl") <> "OP004" Then
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
						End If
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false);</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "OP004"
					'Response.Write "<NOTSCRIPT>opener.document.location.href='OP004_K.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("ReloadIndex") & "'</SCRIPT>"
				Case "OP090"
					Response.Write("<SCRIPT>opener.document.location.href='OP090.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case "OP092_K"
					Response.Write("<SCRIPT>opener.document.location.href='OP092_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case "OPC002"
					Response.Write("<SCRIPT>top.opener.document.location.href='OPC002_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sql=" & Server.URLEncode(Session("Sql")) & "&nAccount=" & Request.Form.Item("hgmnAccount") & "&hoptInfType=" & Request.Form.Item("hoptInfType") & "&optInfType=" & Request.Form.Item("hoptInfType") & mstrString & "'</SCRIPT>")
				Case "OPC720"
					Response.Write("<SCRIPT>top.opener.document.location.href='OPC720_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrString & "'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjCurr_acc = Nothing
mobjCash_stat = Nothing
lclsCurr_acc = Nothing
mobjCurr_acc = Nothing
mobjValues = Nothing
mobjCash_mov = Nothing
mobjCheque = Nothing
mobjCash_acc = Nothing
mobjBank_Trans = Nothing
mobjMove_acc = Nothing
mobjBank_acc = Nothing
mobjCheq_book = Nothing

%>
</BODY>
</HTML>




