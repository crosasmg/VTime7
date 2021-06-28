<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrString As String

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjeLedGe As Object


'% insValLedGerTra: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValLedGerTra() As String
	Dim strAux As String
	Dim lintIndex As Integer
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+CP001: Instalación de Compañía Contable
		Case "CP001"
			mobjeLedGe = New eLedge.Led_compan
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedGerTra = mobjeLedGe.insValCP001_k("CP001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLedCompan"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insValLedGerTra = mobjeLedGe.insValCP001("CP001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFromLedCompan"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdInitLedDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnUnit1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnUnit2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnUnit3"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("lblEndLedDate1"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("lblTo1"), eFunctions.Values.eTypeData.etdDate), .Form.Item("gmtLossProfit"), .Form.Item("gmtGenBal"), mobjValues.StringToType(.Form.Item("gmnCode1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode3"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode4"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode5"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode6"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnCode7"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLedCompanAux"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+CP002: Actualizaciòn de Cuentas
			
		Case "CP002"
			mobjeLedGe = New eLedge.Ledger
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedGerTra = mobjeLedGe.insValCP002_k(.Form.Item("valAccount"), mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), .Form.Item("valAux"))
				Else
					insValLedGerTra = mobjeLedGe.insValCP002(mobjValues.StringToType(Request.QueryString.Item("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sAccount"), Request.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcthidAuxType"), .Form.Item("cbeType"), .Form.Item("tctDescript"), .Form.Item("cbeAux"), Request.QueryString.Item("sAux_Account"), .Form.Item("chkBudget"))
					
				End If
			End With
			'+CP003: Saldos Historicos
			
		Case "CP003"
			mobjeLedGe = New eLedge.Bal_histor
			With Request
				If .Form.Item("valAux") = "0" Then
					strAux = CStr(" ")
				Else
					strAux = .Form.Item("valAux")
				End If
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedGerTra = mobjeLedGe.insValCP003_k(.Form.Item("valAccount"), mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP003", strAux, mobjValues.StringToType(.Form.Item("cboCompare"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optSel"), .Form.Item("optStyle"), .Form.Item("valUnit"), .Form.Item("tcnLedger_Year"))
				Else
					insValLedGerTra = mobjeLedGe.insValCP003(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP003", Session("sAux_Account"), mobjValues.StringToType(Session("nLed_Year"), eFunctions.Values.eTypeData.etdDouble), Session("sAccount"), Session("sCost_Cente"), mobjValues.StringToType(.Form.Item("tcnAuxYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctAuxPer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDeb"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxCred"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnAuxInd"))
				End If
			End With
			'+CP005: Actualizaciòn de asientos contables
		Case "CP005"
			mobjeLedGe = New eLedge.Acc_lines
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedGerTra = mobjeLedGe.insValCP005_k(mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP005", mobjValues.StringToType(.Form.Item("tcnNumOffi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("gmdDate")), .Form.Item("chkFutureMonth"))
				Else
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						If .Form.Item("tcnAuxLine").Length <> 0 Then
							For lintIndex = 1 To .Form.Item("tcnAuxLine").Length
								insValLedGerTra = mobjeLedGe.insValCP005(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP005", mobjValues.StringToDate(Session("dDate")), .Form.GetValues("valAuxAux").GetValue(lintIndex - 1), mobjValues.StringToDate(.Form.GetValues("tcdAuxDateDoc").GetValue(lintIndex - 1)), "1", mobjValues.StringToType(.Form.GetValues("tcnAuxCredit").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxDebit").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("cboAuxCurrency").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("cboAuxDoc_Type").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxDocNumber").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.GetValues("valAuxAccount").GetValue(lintIndex - 1))
								lintIndex = lintIndex + 1
							Next 
						Else
							insValLedGerTra = .Form.Item("tcnAuxLine").Length & "Andrew2 " & mobjeLedGe.insValCP005(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP005", mobjValues.StringToDate(Session("dDate")), .Form.Item("valAuxAux"), mobjValues.StringToDate(.Form.Item("tcdAuxDateDoc")), "1", mobjValues.StringToType(.Form.Item("tcnAuxCredit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboAuxCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboAuxDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDocNumber"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("valAuxAccount"))
							
						End If
					Else
						mobjeLedGe = New eLedge.Acc_lines
						insValLedGerTra = mobjeLedGe.insValCP005(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("WindowType"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP005", mobjValues.StringToType(Session("dDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("valAux"), mobjValues.StringToType(.Form.Item("tcdDateDoc"), eFunctions.Values.eTypeData.etdDate), "1", mobjValues.StringToType(.Form.Item("tcnCredit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDocNumber"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("valAccount"), .Form.Item("sUnMat"))
						
					End If
				End If
			End With
			
			'+CP009: Actualizaciòn de Unidades Organizativas
		Case "CP009"
			Session("nLedCompan") = Request.Form.Item("nLedCompan")
			mobjeLedGe = New eLedge.Tab_cost_c
			With Request
				insValLedGerTra = mobjeLedGe.insValCP009(mobjValues.StringToType(.Form.Item("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "CP009", "1", .Form.Item("cboStratregt"), .Form.Item("tctCost_cente"), .Form.Item("chkBlock_deb"), .Form.Item("chkBlock_cre"), .Form.Item("tctDescript"))
				
				
				
			End With
		Case "CP8000"
			mobjeLedGe = New eGeneral.Ctrol_date
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValLedGerTra = mobjeLedGe.insValCP8000_K("CP8000", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnType_proce"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcndate_close"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
		Case Else
			insValLedGerTra = "insValLedGerTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostLedGeTra: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostLedGeTra() As Boolean
	Dim lintIndex As Integer
	Dim lintNotenum As Byte
	Dim strBlock_cre As String
	Dim mintNoteNum As Object
	Dim strBlock_deb As String
	Dim sStratregt As String
	'--------------------------------------------------------------------------------------------
	
	Dim lblnPost As Boolean
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		
		'+CP001: Instalacion de Compañia Contable		
		
		Case "CP001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nLedCompan") = .Form.Item("valLedCompan")
					lblnPost = True
				Else
					mobjeLedGe = New eLedge.Led_compan
					lblnPost = mobjeLedGe.insPostCP001("CP001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("gmtGenBal"), .Form.Item("gmtLossProfit"), .Form.Item("chkUpdate"), .Form.Item("chkClose"), .Form.Item("gmnUnit1"), .Form.Item("gmnUnit2"), .Form.Item("gmnUnit3"), .Form.Item("gmnCode1"), .Form.Item("gmnCode2"), .Form.Item("gmnCode3"), .Form.Item("gmnCode4"), .Form.Item("gmnCode5"), .Form.Item("gmnCode6"), .Form.Item("gmnCode7"), mobjValues.StringToType(.Form.Item("tcdFromLedCompan"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("lblTo1"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitLedDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("lblEndLedDate1"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valLedCompanAux"), eFunctions.Values.eTypeData.etdDouble), "1")
				End If
				
			End With
			'+CP002: Actualizaciòn de Cuentas
			
		Case "CP002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nLedCompan=" & mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble) & "&sAccount=" & .Form.Item("valAccount") & "&sAux_Account=" & .Form.Item("valAux")
					lblnPost = True
				Else
					mobjeLedGe = New eLedge.Ledger
					lblnPost = mobjeLedGe.insPostCP002(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcthidAuxType"), Request.QueryString.Item("nLedCompan"), Request.QueryString.Item("sAccount"), Request.QueryString.Item("sAux_Account"), .Form.Item("chkOrgUnit"), .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("cbeAux"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkCredit"), .Form.Item("chkDebit"), .Form.Item("chkBudget"), .Form.Item("tctDescript"), .Form.Item("cbeType"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("lblTDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("lblTCredit"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+CP003: Saldos Historicos
			
		Case "CP003"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If .Form.Item("optSel") = "1" Then
						Session("optSel0") = "1"
						Session("optSel1") = "2"
					Else
						Session("optSel0") = "2"
						Session("optSel1") = "1"
					End If
					If .Form.Item("optStyle") = "1" Then
						Session("optStyle0") = "1"
						Session("optStyle1") = "2"
					Else
						Session("optStyle0") = "2"
						Session("optStyle1") = "1"
					End If
					If .Form.Item("optType") = "1" Then
						Session("optType0") = "1"
						Session("optType1") = "2"
						Session("optType2") = "2"
					End If
					If .Form.Item("optType") = "2" Then
						Session("optType0") = "2"
						Session("optType1") = "1"
						Session("optType2") = "2"
					End If
					If .Form.Item("optType") = "3" Then
						Session("optType0") = "2"
						Session("optType1") = "2"
						Session("optType2") = "1"
					End If
					Session("cboCompare") = .Form.Item("cboCompare")
					Session("sAccount") = .Form.Item("valAccount")
					If .Form.Item("valAux") = "0" Or IsNothing(.Form.Item("valAux")) Then
						Session("sAux_Account") = "                    "
					Else
						Session("sAux_Account") = .Form.Item("valAux")
					End If
					Session("sCost_Cente") = .Form.Item("valUnit")
					Session("nLed_Year") = .Form.Item("tcnLedger_Year")
					Session("nLedCompan") = .Form.Item("tcnLedCompan")
					lblnPost = True
				Else
					lblnPost = mobjeLedGe.insPostCP003(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctAuxPer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDeb"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxCred"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxOrg_Deb"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxOrg_Cred"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Session("sAccount"), Session("sAux_Account"), Session("sCost_Cente"), mobjValues.StringToType(Session("nLed_Year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sAuxSel"))
					
					
				End If
			End With
			
			'+CP005: Asientos contables
			
		Case "CP005"
			mobjeLedGe = New eLedge.Acc_lines
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("chkProcess") = .Form.Item("chkProcess")
					Session("dDate") = .Form.Item("gmdDate")
					Session("nVoucher") = .Form.Item("tcnNumber")
					Session("nOffiNum") = .Form.Item("tcnNumOffi")
					Session("nLedCompan") = .Form.Item("tcnLedCompan")
					Session("nDebit") = .Form.Item("lblDebit")
					Session("nCredit") = .Form.Item("lblCredit")
					lblnPost = True
				Else
					If Request.QueryString.Item("sAction") = "Cut" Then
						Request.QueryString.Item("Action") = "Cut"
					ElseIf Request.QueryString.Item("sAction") = "Reverse" Then 
						Request.QueryString.Item("Action") = "Reverse"
					End If
					If .Form.Item("tcnNotenum") Is System.DBNull.Value Or CDbl(.Form.Item("tcnNotenum")) = -32768 Then
						lintNotenum = 0
					End If
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						sStratregt = "1"
						If .Form.Item("tcnAuxLine").Length > 0 Then
							For lintIndex = 1 To .Form.Item("tcnAuxLine").Length
								lblnPost = mobjeLedGe.insPostCP005(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToDate(Session("dDate")), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("btnAuxNotenum").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxDebit").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxCredit").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), Session("chkProcess"), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("valAuxAccount").GetValue(lintIndex - 1), .Form.GetValues("valAuxAux").GetValue(lintIndex - 1), .Form.GetValues("valAuxClient").GetValue(lintIndex - 1), mobjValues.StringToDate(.Form.GetValues("tcdAuxDateDoc").GetValue(lintIndex - 1)), .Form.GetValues("tctAuxDescript").GetValue(lintIndex - 1), mobjValues.StringToType(.Form.GetValues("cboAuxDoc_Type").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxDocNumber").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("cboAuxCurrency").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), sStratregt, .QueryString("Action"), mobjValues.StringToType(.Form.GetValues("tcnAuxLine").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxExchange").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxOri_amo").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(CStr(lintNotenum), eFunctions.Values.eTypeData.etdDouble))
								lintIndex = lintIndex + 1
							Next 
						Else
							lblnPost = mobjeLedGe.insPostCP005(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToDate(Session("dDate")), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("btnAuxNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxCredit"), eFunctions.Values.eTypeData.etdDouble), Session("chkProcess"), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAuxAccount"), .Form.Item("valAuxAux"), .Form.Item("valAuxClient"), mobjValues.StringToDate(.Form.Item("tcdAuxDateDoc")), .Form.Item("tctAuxDescript"), mobjValues.StringToType(.Form.Item("cboAuxDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxDocNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboAuxCurrency"), eFunctions.Values.eTypeData.etdDouble), sStratregt, .QueryString("Action"), .Form.Item("tcnAuxLine"), mobjValues.StringToType(.Form.Item("tcnAuxExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuxOri_amo"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sDescript"), mobjValues.StringToType(CStr(lintNotenum), eFunctions.Values.eTypeData.etdDouble))
						End If
					Else
						sStratregt = "3"
						mobjeLedGe = New eLedge.Acc_lines
						lblnPost = mobjeLedGe.insPostCP005(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToDate(Session("dDate")), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("btnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCredit"), eFunctions.Values.eTypeData.etdDouble), Session("chkProcess"), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAccount"), .Form.Item("valAux"), .Form.Item("valClient"), mobjValues.StringToDate(.Form.Item("tcdDateDoc")), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cboDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDocNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble), sStratregt, .QueryString("Action"), .Form.Item("tcnLine"), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOri_amo"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sDescript"), mobjValues.StringToType(CStr(lintNotenum), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nOffinum"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("WindowType"), .Form.Item("sCost_cente"), .Form.Item("sUnmat"))
						
					End If
				End If
			End With
			
			'+CP009: Actualizaciòn de Unidades Organizativas
		Case "CP009"
			mobjeLedGe = New eLedge.Tab_cost_c
			If Request.Form.Item("btnNotenum") Is System.DBNull.Value Or IsNothing(Request.Form.Item("btnNotenum")) Then
				mintNoteNum = 0
			Else
				mintNoteNum = Request.Form.Item("btnNotenum")
			End If
			If Request.Form.Item("chkBlock_cre") = "1" Then
				strBlock_cre = "1"
			Else
				strBlock_cre = "0"
			End If
			If Request.Form.Item("chkBlock_deb") = "1" Then
				strBlock_deb = "1"
			Else
				strBlock_deb = "0"
			End If
			
			With Request
				lblnPost = mobjeLedGe.insPostCP009(.QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintNoteNum, eFunctions.Values.eTypeData.etdDouble), strBlock_cre, strBlock_deb, .Form.Item("tctDescript"), .Form.Item("cboStratregt"), mobjValues.StringToDate(Session("dEffecdate")), .Form.Item("tctCost_cente"))
			End With
		Case "CP8000"
			mobjeLedGe = New eGeneral.Ctrol_date
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjeLedGe.insPostCP8000(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnType_proce"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcndate_close"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
	End Select
	insPostLedGeTra = lblnPost
End Function

</script>
<%Response.Expires = -1

mstrCommand = "&sModule=GeneralLedGer&sProject=LedGerTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mobjValues = New eFunctions.Values

If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) And Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
Else
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValLedGerTra
		Session("sErrorTable") = mstrErrors
	Else
		Session("sErrorTable") = vbNullString
	End If
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """,""LedgerTraErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostLedGeTra Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
					End If
				Else
					If Request.QueryString.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "CP003"
						Response.Write("<SCRIPT>top.opener.document.location.href='CP003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
					Case "CP005"
						Response.Write("<SCRIPT>top.opener.document.location.href='CP005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CP009"
						Response.Write("<SCRIPT>top.opener.document.location.href='CP009_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CP8000"
						Response.Write("<SCRIPT>top.opener.document.location.href='cp8000_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case Else
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & "?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
End If
mobjValues = Nothing
mobjeLedGe = Nothing
%>
</BODY>
</HTML>





