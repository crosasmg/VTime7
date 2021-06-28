<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Se define la cosntante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjProdLifeSeq As eProduct.ProdLifeSeq
Dim mclsEffect_dat As eProduct.Effect_dat
Dim mclsSurr_retention As eProduct.Surr_retention
Dim mstrQueryString As String
Dim mobjFundValue As eBranches.Tab_Ord_Origin
Dim lclsObject As Object
Dim lclsGeneral As eGeneral.GeneralFunction

Dim mclsTab_ActiveLife As eProduct.Tab_ActiveLife


'% insValProdLifeSeq: Se realizan las validaciones de las páginas
'--------------------------------------------------------------------------------------------
Function insValProdLifeSeq() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ DP020: Beneficios de inversiones		
		Case "DP020"
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP020("DP020", mobjValues.StringToType(.Form.Item("tcnBenefiltr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBenefApl"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP021: Datos para fondos de Inversiones
		Case "DP021"
			mobjProdLifeSeq = New eProduct.ProdLifeSeq
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP021("DP021", mobjValues.StringToType(.Form.Item("tcnUlsschar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlsmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlscharg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlrschar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlrmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlrcharg"), eFunctions.Values.eTypeData.etdDouble))
				Session("nUlfmaxqu") = .Form.Item("gmnUlsmaxqu")
			End With
			
			'+ DP025: Operaciones de pago de Siniestro
		Case "DP025"
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP025(mobjValues.StringToType(.Form.Item("tcnClaim_Pres"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClSimpai"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClnoprei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClSurrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClAllpre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClpaypri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClTransi"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClannpei"), .Form.Item("tctCllifeai"), mobjValues.StringToType(.Form.Item("tcnClaim_Notice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim_Pay"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP026: Consideraciones especiales sobre edades
		Case "DP026"
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP026("DP026", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnSuageMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSuageMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnReageMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearminw"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearMors"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearMins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSmoke"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNSmoke"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP043A: Información de préstamos/anticipos
		Case "DP043A"
			With Request
                    insValProdLifeSeq = mobjProdLifeSeq.insValDP043A(.QueryString.Item("sCodispl"), .Form.Item("tctRouadvan"), mobjValues.StringToType(.Form.Item("cbeAnlifint"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayInter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMEPLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMMLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMYLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAMinLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAMaxLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerVSLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercTol"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTaxes"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRouInterest"), mobjValues.StringToType(.Form.Item("cbeBill_item"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP043B: Información de Rescates
		Case "DP043B"
			mobjProdLifeSeq = New eProduct.ProdLifeSeq
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP043B("DP043B", .Form.Item("tctRousurre"), .Form.Item("chkSurrenpi"), .Form.Item("chkSurrenti"), mobjValues.StringToType(.Form.Item("cbeFreqSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQmepsurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQmmsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQmysurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAminsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmaxsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPervssurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapminsurr"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP043D: Saldado y Prorrogado
		Case "DP043D"
			insValProdLifeSeq = vbNullString
			
			'+ DP044: Pago de Inversiones asociados a un plan
		Case "DP044"
			mobjProdLifeSeq = New eProduct.ProdLifeSeq
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValProdLifeSeq = mobjProdLifeSeq.insValDP044("DP044", Request.QueryString.Item("Action"), Request.QueryString.Item("WindowType"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nproduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Request.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnBuy_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCountReg") + 1, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUlfmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(1), eFunctions.Values.eTypeData.etdDouble))
			Else
				insValProdLifeSeq = mobjProdLifeSeq.insValMsvDP044("DP044", mobjValues.StringToType(Session("nCountReg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUlfmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTotparticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTotCtas"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
			'+ DP024: Opciones de pago de primas
		Case "DP024"
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP024(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnInitialPay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRevalFact"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerMul"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoPerMul"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNoPerMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNoPerMax"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPerPay"), .Form.Item("chkPerUni"), .Form.Item("chkNoPerPay"), .Form.Item("chkNoPerUni"), mobjValues.StringToType(.Form.Item("cbePerFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRevalType"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP047: Fecha efectiva del aporte
		Case "DP047"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdLifeSeq = mclsEffect_dat.insValDP047(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnDaynumin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDaynumen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDayadd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeValuesty"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeValuesmo"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'**+ DP7000: Percentage retention according to surrender reason
			'+ DP7000: Porcentaje de retención según razón del rescate
			
		Case "DP7000"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        insValProdLifeSeq = mclsSurr_retention.insValDP7000(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeReason"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTyp_profitworker"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("nAmountfree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("nOrigin"), eFunctions.Values.eTypeData.etdInteger))
				End If
			End With
			
			'+ [APV2] DP7001: Reglas de Capitalización
			
		Case "DP7001"
			With Request
				insValProdLifeSeq = mobjProdLifeSeq.insValDP7001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnSaving_pct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeIndex_table"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkAccount_mirror"), mobjValues.StringToType(.Form.Item("valWarrn_table_mirror"), eFunctions.Values.eTypeData.etdInteger, True))
			End With
			
			'+ DP607A: Condiciciones generales de planes de VidActiva
			
		Case "DP607A"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdLifeSeq = mclsTab_ActiveLife.InsValDP607A(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnCapMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMChanInves"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnErrRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					insValProdLifeSeq = mclsTab_ActiveLife.InsValDP607AMsg(.QueryString.Item("sCodispl"), .Form.Item("hddOption").Length, .Form.Item("hddOption"))
				End If
			End With
			
			'+ Cargos por Rescate		
		Case "DP607C"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdLifeSeq = mclsTab_ActiveLife.InsValDP607C(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMonthIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQMonthEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerTotSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerParSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChargTSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChargPSurr"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ Porcentajes de rentabilidad		
		Case "DP607D"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdLifeSeq = mclsTab_ActiveLife.InsValDP607D(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrMin"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ MVI7002: Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
			'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		Case "MVI7002"
			mobjFundValue = New eBranches.Tab_Ord_Origin
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValProdLifeSeq = mobjFundValue.InsValMVI7002(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPrimary"), mobjValues.StringToType(Request.Form.Item("tcnPerc_Collect"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
			'+ DP8005: ahorros garantizados permitidos
		Case "DP8005"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					lclsObject = New eBranches.Guar_saving_allow
					
					insValProdLifeSeq = lclsObject.insValDP8005(mobjValues.StringToType(.Form.Item("hddGuarSavMax"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nPolicy_year_ini"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nPolicy_year_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdLong))
					lclsObject = Nothing
				Else
					lclsObject = New eBranches.Guar_saving_prod
					
					insValProdLifeSeq = lclsObject.insValDP8005(mobjValues.StringToType(.Form.Item("nGuarSavMax"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nLower_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nValDate_Issue"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nValDate_Last"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nQmin_prem"), eFunctions.Values.eTypeData.etdLong), .Form.Item("sIndRenewal"), .Form.Item("sRouReserve"), .Form.Item("sRouGuarSafe"))
					
					lclsObject = Nothing
					
				End If
			End With
			
			'+ Porcentajes de Valor Poliza permitido Rescatar	
		Case "DP8006"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lclsObject = New eProduct.Surr_percent
					
					insValProdLifeSeq = lclsObject.InsValDP8006(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQSurrIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQSurrEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True))
					lclsObject = Nothing
				End If
			End With
			
		Case Else
			insValProdLifeSeq = "insValProdLifeSeq: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostProdLifeSeq: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostProdLifeSeq() As Boolean
	Dim nAction As Byte
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ DP8005: ahorros garantizados permitidos
		Case "DP8005"
			With Request
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					lclsObject = New eBranches.Guar_saving_allow
					nAction = 1
					
					If .QueryString.Item("Action") <> "Add" Then
						nAction = 2
					End If
					
					lblnPost = lclsObject.insPostDP8005(nAction, Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.Form.Item("nPolicy_year_ini"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nPolicy_year_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode"))
					
					lclsObject = Nothing
				Else
					lclsObject = New eBranches.Guar_saving_prod
					
					lblnPost = lclsObject.insPostDP8005(1, Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("nGuarSavMax"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nLower_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nValDate_Issue"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nValDate_Last"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nQmin_prem"), eFunctions.Values.eTypeData.etdLong), .Form.Item("sIndRenewal"), .Form.Item("sRouReserve"), .Form.Item("sRouGuarSafe"), Session("nUsercode"))
					
					lclsObject = Nothing
				End If
				
			End With
			
			'+ DP020: Beneficios de inversiones
		Case "DP020"
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP020("DP020", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnBenefiltr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBenefexc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBenefApl"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("OptResBenef"))
			End With
			
			'+ DP021: Datos para fondos de inversiones
		Case "DP021"
			With Request
                    lblnPost = mobjProdLifeSeq.insPostDP021(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnUlfmaxqu"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkUlfchani"), mobjValues.StringToType(.Form.Item("tcnUlsmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeUlswiper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeULswmaxper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnUlsschar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlscharg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnULswchPerc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnULmmsw"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnULSwmqt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeULswmqtper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnUlrmaxqu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeUlredper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeULrdmaxper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnUlrschar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlrcharg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUlrdchperc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnULmmrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnULrdmqt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeULrdmqtper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeInfType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyperateproy"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP025: Operaciones de pago de Siniestro
		Case "DP025"
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP025("DP025", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("chkClSimpai"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClnoprei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClSurrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClAllpre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClpaypri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClTransi"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCllifeai"), .Form.Item("tctClannpei"), mobjValues.StringToType(.Form.Item("tcnClaim_Pres"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnClaim_Notice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkClAmountap"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP026: Consideraciones especiales sobre edades
		Case "DP026"
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP026("DP026", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnSuageMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSuageMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnReageMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearminw"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearMors"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearMins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSmoke"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNSmoke"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP043A: Información de préstamos/anticipos
		Case "DP043A"
			With Request
                    lblnPost = mobjProdLifeSeq.insPostDP043A(.QueryString("sCodispl"), .Form.Item("tctRouadvan"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAnlifint"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayInter"), eFunctions.Values.eTypeData.etdDouble, True), Session("nBranch"), Session("nProduct"), mobjValues.StringToDate(Session("dEffecdate")), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnQmepLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMMLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMYLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAMinLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAMaxLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerVSLoans"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercTol"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTaxes"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRouInterest"), mobjValues.StringToType(.Form.Item("cbeBill_item"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOrigin_Loan"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP043B: Información de Rescates
		Case "DP043B"
			mobjProdLifeSeq = New eProduct.ProdLifeSeq
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP043B("DP043B", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRousurre"), .Form.Item("chkSurrenpi"), .Form.Item("chkSurrenti"), mobjValues.StringToType(.Form.Item("cbeFreqSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurcashv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCharge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnChargeAmo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQmepsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQmmsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQmysurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAminsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmaxsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPervssurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapminsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxchargsurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrigin_Surr"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoutineSurr"), .Form.Item("chkApplyRouSurr"), mobjValues.StringToType(.Form.Item("tcnQMMPsurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBalminsurr"), eFunctions.Values.eTypeData.etdDouble))
				
			End With
			
			'+ DP043D: Saldado y Prorrogado
		Case "DP043D"
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP043D("DP043D", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nUsercode"), .Form.Item("tctRoureduc"), .Form.Item("tctRouredcc"))
			End With
			
			'+ Fondo de inversión asociado a un plan
		Case "DP044"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lblnPost = mobjProdLifeSeq.insPostDP044(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnExist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnBuy_cost"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, mobjValues.StringToType(Request.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnIntProyVarMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnIntProyVarCle"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkVigen"))
			Else
				lblnPost = True
			End If
			
			'+ DP024: Opciones de pago de primas
		Case "DP024"
			mobjProdLifeSeq = New eProduct.ProdLifeSeq
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP024(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnInitialPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAnnual"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRevalFact"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerMul"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoPerMul"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoPerMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoPerMax"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPerPay"), .Form.Item("chkPerUni"), .Form.Item("chkNoPerPay"), .Form.Item("chkNoPerUni"), mobjValues.StringToType(.Form.Item("cbePerFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRevalType"), eFunctions.Values.eTypeData.etdDouble), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
			End With
			
			'+ DP047: Fecha efectiva del aporte
		Case "DP047"
			With Request
				lblnPost = True
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsEffect_dat.insPostDP047(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeValuesty"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeValuesmo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDaynumin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDaynumen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDayadd"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				End If
			End With
			
			'**+ DP7000: Percentage retention according to surrender reason
			'+ DP7000: Porcentaje de retención según razón del rescate
			
		Case "DP7000"
			With Request
				lblnPost = True
				
				If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mclsSurr_retention.insPostDP7000(.QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeReason"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTyp_profitworker"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("nAmountfree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("nOrigin"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"))
				End If
			End With
			
			'+ [APV2] DP7001: Reglas de Capitalización
			
		Case "DP7001"
			With Request
				lblnPost = mobjProdLifeSeq.insPostDP7001(.QueryString.Item("sCodispl"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnSaving_pct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeIndex_table"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkS_allwchng"), .Form.Item("chkIx_allwchng"), .Form.Item("chkW_allwchng"), Session("nUsercode"), .Form.Item("chkAccount_mirror"), mobjValues.StringToType(.Form.Item("valWarrn_table_mirror"), eFunctions.Values.eTypeData.etdInteger, True))
			End With
			
			'+ DP607A: Condiciciones generales de planes de VidActiva
		Case "DP607A"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mclsTab_ActiveLife.InsPostDP607A(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMChanInves"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnErrRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_prembas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_prembas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_premmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_premmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_premexc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_premexc"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("tcnMin_prempacmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_prempacmin"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Cargos por Rescate
		Case "DP607C"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsTab_ActiveLife.InsPostDP607C(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMonthIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQMonthEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerTotSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPerParSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChargTSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChargPSurr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQFree_Surr"), eFunctions.Values.eTypeData.etdDouble, True))
					
					mstrQueryString = "&nModulec=" & Request.Form.Item("hdnModulec")
				Else
					lblnPost = True
				End If
			End With
			
			'+ Porcentajes de rentabilidad
		Case "DP607D"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsTab_ActiveLife.InsPostDP607D(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrClear"), eFunctions.Values.eTypeData.etdDouble, True))
					
					mstrQueryString = "&nModulec=" & Request.Form.Item("hdnModulec")
				Else
					lblnPost = True
				End If
			End With
			
			'+ MVI7002: Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
			'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		Case "MVI7002"
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjFundValue = New eBranches.Tab_Ord_Origin
                        lblnPost = mobjFundValue.InsPostMVI7002Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPrimary"), mobjValues.StringToType(Request.Form.Item("tcnPerc_Collect"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSell_cost"), mobjValues.StringToDate(Request.Form.Item("tcdExpirdat")), mobjValues.StringToType(Request.Form.Item("cbeOrigen_dep"), eFunctions.Values.eTypeData.etdDouble))
					
				Else
					lblnPost = True
				End If
			Else
				lblnPost = True
			End If
			
			'+ Porcentajes de Valor Poliza permitido Rescatar
		Case "DP8006"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lclsObject = New eProduct.Surr_percent
					
					lblnPost = lclsObject.InsPostDP8006(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQSurrIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQSurrEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
					lclsObject = Nothing
				Else
					lblnPost = True
				End If
			End With
			
	End Select
	
	insPostProdLifeSeq = lblnPost
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjProdLifeSeq = New eProduct.ProdLifeSeq
mclsEffect_dat = New eProduct.Effect_dat
mclsSurr_retention = New eProduct.Surr_retention

mstrCommand = "&sModule=Product&sProject=ProductSeq&sSubProject=ProdLifeSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>



	
<SCRIPT>
//% CancelErrors: se realiza el manejo en caso de Cancelar la ventana de errores
//-------------------------------------------------------------------------------------------
function CancelErrors(){
//-------------------------------------------------------------------------------------------
	self.history.go(-1)
}
//% NewLocation: se recalcula el URL de la página
//-------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
//% insvalTabs: se verifica la existencia de ventanas requeridas en la secuencia
//-------------------------------------------------------------------------------------------
function insvalTabs(){
//-------------------------------------------------------------------------------------------
	<%lclsGeneral = New eGeneral.GeneralFunction
%>
	var lblnTabs = false;
	var Array = top.frames['fraSequence'].sequence;
	for(var lintIndex=0; lintIndex<Array.length; lintIndex++)
		if(Array[lintIndex].Require=="2" ||
		   Array[lintIndex].Require=="5")
			lblnTabs = true;

	if(lblnTabs){
//+ Se envía un error indicando que faltan ventanas requeridas por llenar en la secuencia
		top.frames["fraFolder"].document.location.reload();
		alert("<%=lclsGeneral.insLoadMessage(3902)%>");
	}
	else
		top.close();
	
<%
lclsGeneral = Nothing%>
}
</SCRIPT>
</HEAD>
<%
mclsTab_ActiveLife = New eProduct.Tab_ActiveLife

If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	Response.Write("<BODY>")
Else
	Response.Write("<BODY CLASS=""Header"">")
End If

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValProdLifeSeq
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
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProdLifeSeqError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostProdLifeSeq Then
			
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
				
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				Select Case Request.QueryString.Item("sCodispl")
					Case "DP047", "DP044"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Case "DP7000"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nAction=" & Request.QueryString.Item("nMainAction") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Case "DP607A"
						If Request.QueryString.Item("Action") = "Add" Then
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=0&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						End If
					Case Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
						End If
				End Select
			End If
		End If
	End If
Else
	Response.Write("<SCRIPT>insvalTabs()</SCRIPT>")
End If

mclsTab_ActiveLife = Nothing
mobjValues = Nothing
mobjProdLifeSeq = Nothing
mclsEffect_dat = Nothing
mclsSurr_retention = Nothing
mobjFundValue = Nothing
%>
</BODY>
</HTML>





