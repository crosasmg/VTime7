<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mintLoansId As Object
Dim mstrCodispl As String
Dim lintPay_Type As Object

Dim mstrErrors As Object
Dim mobjValues As eFunctions.Values
Dim mobjAgent As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'+ Se define la variable para el pase de valores
Dim mstrString As String


'% insvalAgent: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalAgent() As Object
	'--------------------------------------------------------------------------------------------
	Dim lstrPerType As String
	Dim lstrInforType As String
	Select Case Request.QueryString.Item("sCodispl")
		'+AG004: Anticipos/préstamos de intermediarios
		Case "AG004"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mintLoansId = insGetNewLoans(mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeLoanId"), eFunctions.Values.eTypeData.etdDouble))
				insvalAgent = mobjAgent.insValAG004_k(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintLoansId, eFunctions.Values.eTypeData.etdDouble))
			Else
				If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
					insvalAgent = True
				Else
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
						insvalAgent = mobjAgent.insValAG004(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeLoanType"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeLoanSta"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLoanBalance"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLoanAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbePayForm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercent_ant"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInterest_ant"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMonthly"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeMode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCommBase"), eFunctions.Values.eTypeData.etdDouble), Session("hddCurrCommBase"), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sCodispl"))
					End If
				End If
			End If
		Case "AG005"
			mobjAgent = New eAgent.Goals
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				If Request.Form.Item("cbePerType") = "0" Then
					lstrPerType = ""
				Else
					lstrPerType = Request.Form.Item("cbePerType")
				End If
				If Request.Form.Item("cbeInforType") = "0" Then
					lstrInforType = ""
				Else
					lstrInforType = Request.Form.Item("cbeInforType")
				End If
				insvalAgent = mobjAgent.InsValAG005_K("AG005", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPerNumber"), eFunctions.Values.eTypeData.etdDouble), lstrInforType, lstrPerType, mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True))
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					insvalAgent = mobjAgent.insValAG005Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPeriodnum"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), Session("sPeriodtyp"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("ValProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnGoal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End If
			mobjAgent = Nothing
			
		Case "AG011"
			mobjAgent = New eAgent.Intermedia
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalAgent = mobjAgent.insValAG011_K(mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("optIntStatus"))
				Session("nIntermed") = Request.Form.Item("valIntermed")
				Session("optIntStatus") = Request.Form.Item("optIntStatus")
			Else
				insvalAgent = mobjAgent.insValAG011(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("gmdNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeNullCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("optIntStatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("dtmInputDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnCircular_doc"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
		Case "AG781"
			mobjAgent = New eAgent.Intermedia
			insvalAgent = mobjAgent.insValAG781("AG781", mobjValues.StringToType(Request.Form.Item("valInterOld"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valInterNew"), eFunctions.Values.eTypeData.etdDouble, True))
			
		Case "AGC001"
			mobjAgent = New eAgent.commis_his
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjAgent = New eAgent.Intermedia
				insvalAgent = mobjAgent.insValAGC001_K("AGC001", mobjValues.StringToType(Request.Form.Item("tcnIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Request.Form.Item("tcdEffecdate")))
				Session("sCliename") = mobjAgent.sCliename
			Else
				insvalAgent = ""
			End If
		Case "AGC002"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjAgent = New eAgent.Intermedia
				
				insvalAgent = mobjAgent.insValAGC002_K("AGC002", mobjValues.StringToType(Request.Form.Item("tcnIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeStatLoan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLoan"), eFunctions.Values.eTypeData.etdDouble))
				Session("sCliename") = mobjAgent.sCliename
			Else
				insvalAgent = True
			End If
			
			'+ AGC006: Consulta alfabética de intermediarios		
		Case "AGC006"
			mobjAgent = New eAgent.Intermedia
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalAgent = mobjAgent.insValAGC006(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClientCode"), .Form.Item("tcnAgent"), .Form.Item("tctAgentName"), .Form.Item("tcnAgentOrg"), .Form.Item("tctOrgName"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctAnull"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCommidate"), eFunctions.Values.eTypeData.etdDate))
				Else
					insvalAgent = vbNullString
				End If
			End With
			
		Case "AGC574"
			mobjAgent = New eAgent.Interm_bud
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				If Request.Form.Item("cbePerType") = "0" Then
					lstrPerType = ""
				Else
					lstrPerType = Request.Form.Item("cbePerType")
				End If
				If Request.Form.Item("cbeInforType") = "0" Then
					lstrInforType = ""
				Else
					lstrInforType = Request.Form.Item("cbeInforType")
				End If
				insvalAgent = mobjAgent.InsValAGC574_K("AG574", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valGoals"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), lstrPerType, mobjValues.StringToType(Request.Form.Item("tcnPerNumber"), eFunctions.Values.eTypeData.etdDouble), lstrInforType, mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
			Else
				insvalAgent = True
			End If
			mobjAgent = Nothing
			
			'+AGC621: Datos de la liquidación de comisiones
			
		Case "AGC621"
			mobjAgent = New eAgent.pay_comm
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nIntermed") = .Form.Item("valIntermed")
					Session("dEffecdateIni") = .Form.Item("tcdEffecdateIni")
					Session("dEffecdateEnd") = .Form.Item("tcdEffecdateEnd")
					Session("nPay_comm") = .Form.Item("tcnPay_comm")
					insvalAgent = mobjAgent.InsValAGC621(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPay_comm"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalAgent = True
				End If
			End With
			
		Case "AGC621a"
			insvalAgent = True
			'+ AG954: Contratos de estipendios 
		Case "AG954"
			mobjAgent = New eAgent.Contrat_Pay
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insvalAgent = mobjAgent.InsValAG954_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If Request.QueryString.Item("nMainAction") <> "401" Then
                            insvalAgent = mobjAgent.InsValAG954(Request.QueryString.Item("sCodispl"), Request.Form.Item("nContrat_Pay"), Request.Form.Item("sClient"), Request.Form.Item("sDescript"), mobjValues.StringToType(Request.Form.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("nType_Calc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nAply"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("sTaxin"), Request.Form.Item("sStatregt"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nTyp_acco"), eFunctions.Values.eTypeData.etdInteger, True))
					End If
				End If
			Else
				insvalAgent = mobjAgent.InsValAG954Upd(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Request.QueryString.Item("nContrat_Pay"), Request.Form.Item("hddClient"), Request.Form.Item("hddDescript"), mobjValues.StringToType(Request.Form.Item("hddStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("hddType_Calc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddAply"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("hddTaxin"), Request.Form.Item("hddStatregt"), mobjValues.StringToType(Request.Form.Item("nSeq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nInit_Dur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nEnd_Dur"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("nPercent_detail"), eFunctions.Values.eTypeData.etdDouble, True))
                End If
                '+ AG954: Contratos de estipendios Por Producto/Ramo     
            Case "AG955"
                mobjAgent = New eAgent.Contrat_Pay
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insvalAgent = mobjAgent.InsValAG955_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong))
                Else
                    insvalAgent = mobjAgent.InsValAG955(Request.QueryString.Item("sCodispl"),
                                  mobjValues.StringToType(Request.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.Form.Item("nType_Calc"), eFunctions.Values.eTypeData.etdDouble),
                                  Request.QueryString.Item("sClient"),
                                  mobjValues.StringToType(Request.Form.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate),
                                  mobjValues.StringToType(Request.Form.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.Form.Item("nAge_Init"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.Form.Item("nAge_End"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.Form.Item("nPolicy_Dur"), eFunctions.Values.eTypeData.etdDouble),
                                  mobjValues.StringToType(Request.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                mobjAgent = Nothing
            Case Else
                insvalAgent = "insvalAgent: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
End Function

'% insPostAgent: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostAgent() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsAgents As eAgent.Agents
	Dim mobjAgent As Object

	mobjAgent = New eAgent.Loans_int
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		
		'+AG004: Anticipos/préstamos de intermediarios
		Case "AG004"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("valIntermedia") = Request.Form.Item("valIntermedia")
					Session("cbeLoanId") = mintLoansId
					
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
						lblnPost = True
					End If
					
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Request.QueryString.Item("sCodispl") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</" & "Script>")
					
				ElseIf CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then 
					lblnPost = mobjAgent.Delete(Session("valIntermedia"), Session("cbeLoanId"), Session("tcdEffecdate"), Session("tcnLoanAmount"), Session("cbeCurrency"), Session("nUsercode"))
				Else
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
						lclsAgents = New eAgent.Agents
						mstrCodispl = "AG004"
						
						Select Case Request.Form.Item("cbePayOrder")
							Case CStr(1), CStr(2)
								mstrCodispl = "OP06-2"
								lintPay_Type = 2
							Case CStr(4)
								mstrCodispl = "OP06-4"
								lintPay_Type = 1
							Case CStr(3), CStr(5)
								mstrCodispl = "OP06-3"
								lintPay_Type = 3
							Case CStr(8)
								mstrCodispl = "OP06-6"
								lintPay_Type = 4
							Case Else
								mstrCodispl = "AG004"
								lintPay_Type = 1
						End Select
						Session("OP006_nCurrency") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_sCodispl") = Request.QueryString.Item("sCodispl")
						Session("OP006_nMonthly") = mobjValues.StringToType(Request.Form.Item("tcnMonthly"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nAmountPay") = mobjValues.StringToType(Request.Form.Item("tcnLoanAmount"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nBalance") = mobjValues.StringToType(Request.Form.Item("tcnLoanBalance"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nInterest") = mobjValues.StringToType(Request.Form.Item("tcnInterest_ant"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nPercent_ant") = mobjValues.StringToType(Request.Form.Item("tcnPercent_ant"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_sReqCheq") = Request.Form.Item("tctReqCheq")
						Session("OP006_nLoanType") = mobjValues.StringToType(Request.Form.Item("cbeLoanType"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nPayOrder") = mobjValues.StringToType(Request.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nPayOrderTyp") = mobjValues.StringToType(lintPay_Type, eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_dEffecDate") = mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate)
						Session("OP006_nPay_Type") = mobjValues.StringToType(Request.Form.Item("cbePayForm"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nBranch") = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
						Session("OP006_nProduct") = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True)
						Session("OP006_nPolicy") = mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True)
						Session("tcdEffecdate") = mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate)
						Session("tcnLoanAmount") = mobjValues.StringToType(Request.Form.Item("tcnLoanAmount"), eFunctions.Values.eTypeData.etdDouble)
						Session("cbeCurrency") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_dReqDate") = Session("dEffecdate")
						Session("OP006_nConcept") = 9
						Session("nBranch") = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
						Session("nProduct") = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
						Session("nPolicy") = mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_tcnCommBase") = Request.Form.Item("tcnCommBase")
						Session("OP006_nMode") = Request.Form.Item("cbeMode")
						Session("OP006_nPercent") = Request.Form.Item("tcnPercent")
						Session("OP006_Codispl") = mstrCodispl
						
						If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then ' - Registrar
							Session("OP006_nLoanSta") = "2"
						Else
							Session("OP006_nLoanSta") = Request.Form.Item("cbeLoanSta")
						End If
						
						If lclsAgents.Find(Session("valIntermedia")) Then
							Session("OP006_sBenef") = lclsAgents.sClient
						End If
						
						lclsAgents = Nothing
						lblnPost = True
					Else
						lblnPost = True
					End If
				End If
			End With
		Case "AG005"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("nCode") = mobjValues.StringToType(Request.Form.Item("valTable"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCurrency") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
					Session("sType_infor") = Request.Form.Item("cbeInforType")
					Session("sPeriodtyp") = Request.Form.Item("cbePerType")
					Session("nPeriodnum") = mobjValues.StringToType(Request.Form.Item("tcnPerNumber"), eFunctions.Values.eTypeData.etdDouble)
					Session("dEffecdate") = mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
					Session("nYear") = mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				ElseIf Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then 
					mobjAgent = New eAgent.Goals
					
					lblnPost = mobjAgent.InsPostAG005Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nCode"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPeriodnum"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), Session("sPeriodtyp"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnGoal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					mobjAgent = Nothing
				Else
					lblnPost = True
				End If
			End With
			
		Case "AG781"
			mobjAgent = New eAgent.Intermedia
			
			lblnPost = mobjAgent.insPostAG781(Request.Form.Item("optIntermed"), mobjValues.StringToType(Request.Form.Item("valInterOld"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valInterNew"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
			
			
		Case "AG011"
			mobjAgent = New eAgent.Intermedia
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				Session("nIntermed") = Request.Form.Item("valIntermed")
				Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Request.QueryString.Item("sCodispl") & ".aspx"";</" & "Script>")
				lblnPost = True
			Else
				lblnPost = mobjAgent.insPostAG011(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("gmdNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("optIntStatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCircular_doc"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
		Case "AGC001"
			lblnPost = True
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				Session("tcnIntermed") = Request.Form.Item("tcnIntermed")
				Session("tcdEffecdate") = Request.Form.Item("tcdEffecdate")
			End If
		Case "AGC002"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				Session("insvalAgent") = insvalAgent
				Session("nIntermed") = Request.Form.Item("tcnIntermed")
				Session("dStardate") = Request.Form.Item("tcdStardate")
				Session("dEnddate") = Request.Form.Item("tcdEnddate")
				Session("sStatloan") = Request.Form.Item("cbeStatloan")
				Session("nBranch") = Request.Form.Item("cbeBranch")
				Session("nProduct") = Request.Form.Item("valProduct")
				Session("nPolicy") = Request.Form.Item("tcnPolicy")
				Session("nLoan") = Request.Form.Item("tcnLoan")
				lblnPost = True
			Else
				lblnPost = True
			End If
			
		Case "AGC006"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mstrString = "&sType=" & .Form.Item("cbeType") & "&sClient=" & .Form.Item("tctClientCode") & "&sAgent=" & .Form.Item("tcnAgent") & "&sAgentName=" & Server.URLEncode(.Form.Item("tctAgentName")) & "&sStatus=" & .Form.Item("cbeStatus") & "&sAgentOrg=" & .Form.Item("tcnAgentOrg") & "&sOffice=" & .Form.Item("cbeOffice") & "&sDateAnull=" & .Form.Item("tctAnull") & "&sCommidate=" & .Form.Item("tcdCommidate") & "&sAgentOrgName=" & Server.URLEncode(.Form.Item("tctOrgName")) & "&nfirst=1"
				End If
			End With
		Case "AGC574"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("nIntermed") = mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble)
					Session("nGoals") = mobjValues.StringToType(Request.Form.Item("valGoals"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCurrency") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
					Session("sType_infor") = Request.Form.Item("cbeInforType")
					Session("sPeriodtyp") = Request.Form.Item("cbePerType")
					Session("nPeriodnum") = mobjValues.StringToType(Request.Form.Item("tcnPerNumber"), eFunctions.Values.eTypeData.etdDouble)
					Session("dEffecdate") = mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
					Session("nYear") = mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			'+AGC621: Datos de la liquidación de comisiones
		Case "AGC621"
			lblnPost = True
		Case "AGC621a"
			lblnPost = True
			'+ AG954: Contratos de estipendios 
		Case "AG954"
			mobjAgent = New eAgent.Contrat_Pay
			'   lblnPost = True
			With Request
				If .QueryString.Item("WindowType") <> "PopUp" Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrString = "&nContrat_Pay=" & .Form.Item("nContrat_Pay")
						lblnPost = True
					Else
                            lblnPost = mobjAgent.InsPostAG954(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("nContrat_Pay"), .Form.Item("sClient"), .Form.Item("sDescript"), mobjValues.StringToType(.Form.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("nType_Calc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nAply"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sTaxin"), .Form.Item("sStatregt"), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("nTyp_acco"), eFunctions.Values.eTypeData.etdInteger, True))
					End If
				Else
					mstrString = "&nContrat_Pay=" & .QueryString.Item("nContrat_Pay")
					
					lblnPost = mobjAgent.InsPostAG954Upd(.QueryString("Action"), .QueryString("nContrat_Pay"), Request.Form.Item("hddClient"), Request.Form.Item("hddDescript"), mobjValues.StringToType(Request.Form.Item("hddStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("hddType_Calc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddAply"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("hddTaxin"), Request.Form.Item("hddStatregt"), mobjValues.StringToType(.Form.Item("nSeq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nInit_Dur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nEnd_Dur"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nPercent_detail"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
                    End If
                End With
                '+ AG955: Contratos de estipendios Por Producto
            Case "AG955"
                mobjAgent = New eAgent.Contrat_Pay
                '   lblnPost = True
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            mstrString = "&nContrat_Pay=" & .Form.Item("nContrat_Pay") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct")
                            lblnPost = True
                        Else
                            lblnPost = mobjAgent.InsPostAG955(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("nContrat_Pay"), .Form.Item("sClient"), .Form.Item("sDescript"), mobjValues.StringToType(.Form.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("nType_Calc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nAply"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sTaxin"), .Form.Item("sStatregt"), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("nTyp_acco"), eFunctions.Values.eTypeData.etdInteger, True))
                        End If
                    Else
                        mstrString = "&nContrat_Pay=" & .QueryString.Item("nContrat_Pay") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct")
					    
                        lblnPost = mobjAgent.InsPostAG955Upd(.QueryString("Action"), _
                                                             .QueryString("nBranch"), _
                                                             .QueryString("nProduct"), _
                                                             .QueryString("nContrat_Pay"), _
                                                             mobjValues.StringToType(Request.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate, True), _
                                                             eRemoteDB.Constants.dtmNull, _
                                                             Request.Form.Item("sClient"), _
                                                             Request.QueryString("sDescript"), _
                                                             mobjValues.StringToType(Request.Form.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate, True), _
                                                             Request.QueryString("nType_Calc"), _
                                                             mobjValues.StringToType(Request.Form.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                             Request.QueryString("nAmount"), _
                                                             Request.QueryString("nCurrency"), _
                                                             Request.QueryString("nAply"), _
                                                             Request.QueryString("sTaxin"), _
                                                             Request.QueryString("sStatregt"), _
                                                             Request.QueryString("nUsercode"), _
                                                             Request.QueryString("nTyp_Acco"), _
                                                             Request.QueryString("nAmount_Ini"), _
                                                             Request.QueryString("sRoutine"), _
                                                             Request.QueryString("nType_Contrat"), _
                                                             mobjValues.StringToType(Request.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdInteger, True), _
                                                             mobjValues.StringToType(Request.Form.Item("nPolicy_Dur"), eFunctions.Values.eTypeData.etdInteger, True), _
                                                             mobjValues.StringToType(.Form.Item("nAge_Init"), eFunctions.Values.eTypeData.etdDouble), _
                                                             mobjValues.StringToType(.Form.Item("nAge_End"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With
			
        End Select
	insPostAgent = lblnPost
	mobjAgent = Nothing
	
End Function

'%insGetNewClient. Esta función se encarga de conseguir un código de cliente
'% para los clientes nuevos (Provisionales).
'--------------------------------------------------------------------------
Private Function insGetNewLoans(ByRef lintIntermedia As String, ByRef nLoanId As Object) As Object
        'dim eRemoteDB.Constants.intNull As Object
	'--------------------------------------------------------------------------
	Dim lclsAgent As eAgent.Loans_int
	
	'+Si la acción es registrar, se busca automáticamente el numro del prestamo
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
            If nLoanId = "0" Or nLoanId = vbNullString Or nLoanId = eRemoteDB.Constants.intNull Then
                lclsAgent = New eAgent.Loans_int
                lclsAgent.nIntermed = mobjValues.StringToType(lintIntermedia, eFunctions.Values.eTypeData.etdDouble)
                nLoanId = lclsAgent.New_Number
                lclsAgent = Nothing
            End If
	End If
	insGetNewLoans = nLoanId
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valagent")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



		
<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 8 $|$$Date: 31/05/04 20:14 $|$$Author: Nvaplat22 $"
</SCRIPT>
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//------------------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//------------------------------------------------------------------------------------------------

//------------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//------------------------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mstrCommand = "&sModule=Agent&sProject=Agent&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mstrString = ""

mobjAgent = New eAgent.Loans_int
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valagent"

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalAgent
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""AgentErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostAgent Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				
				'+ AG004 : Según la acción se determina cual es la página siguiente del proceso.
				If Request.QueryString.Item("sCodispl") = "AG004" Then
					'+ Si la acción es consulta o eliminación se recarga el top de la misma página. 
					If Request.QueryString.Item("nMainaction") = CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
						Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/common/GoTo.aspx?sCodispl=" & Session("OP006_Codispl") & "&nCurrencypay=" & Session("OP006_nCurrency") & "&nAmountpay=" & Session("OP006_nAmountPay") & "&nBranch=" & Session("OP006_nBranch") & "&nProduct=" & Session("OP006_nProduct") & "&nPolicy=" & Session("OP006_nPolicy") & "&nTypesupport=" & Request.Form.Item("hddTypesupport") & "&nMainaction=" & Request.QueryString.Item("nMainaction") & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					End If
					
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location= '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					End If
				End If
			Else
				If Request.QueryString.Item("sCodispl") <> "AGC006" Then
					If Request.QueryString.Item("sCodispl") = "AG781" Then
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					End If
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(True,False)</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "AG004"
					Response.Write("<SCRIPT>top.opener.document.location.href='AG004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case "AG005"
					Response.Write("<SCRIPT>top.opener.document.location.href='AG005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "AG011"
					Response.Write("<SCRIPT>top.opener.document.location.href='AG011.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case "AGC006"
					Response.Write("<SCRIPT>top.opener.document.location.href='AGC006_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
				Case "AGC621"
					Response.Write("<SCRIPT>top.opener.document.location.href='AGC621_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrString & "'</SCRIPT>")
				Case "AGC621a"
					Response.Write("<SCRIPT>top.opener.document.location.href='AGC621a.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrString & "'</SCRIPT>")
				Case "AG954"
                        Response.Write("<SCRIPT>top.opener.document.location.href='AG954.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
                    Case "AG955"
                        Response.Write("<SCRIPT>top.opener.document.location.href='AG955.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
                End Select
		End If
	End If
End If
mobjValues = Nothing
mobjAgent = Nothing
%>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("valagent")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




