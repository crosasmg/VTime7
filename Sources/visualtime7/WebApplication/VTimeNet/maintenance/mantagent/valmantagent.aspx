<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
Dim mobjMantAgent As Object
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mclscommiss_agree As eAgent.commiss_agree
Dim mclsinterm_param As eAgent.interm_param

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrQueryString As String
Dim mstrCommand As String


'% insValMantAgent: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantAgent() As String
	'--------------------------------------------------------------------------------------------
	
	Dim mclsbonus_gen As eAgent.bonus_gen
	Dim mclsaccomp_factor As eAgent.accomp_factor
	Dim mclsexcess_maint As eAgent.excess_maint
	Dim lclsPercentAdvanc As eAgent.PercentAdvanc
	Dim mclstab_goals As eAgent.tab_goals
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MAG001: Tipos de Intermediarios		
		
		Case "MAG001"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				mobjMantAgent = New eAgent.Interm_typ
				With Request
					insValMantAgent = mobjMantAgent.insValMAG001(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), .Form.Item("cbeParticin"), .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeTyp_Acco"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				insValMantAgent = vbNullString
			End If
			
			'+MAG002: Tabla de comisiones de vida
		Case "MAG002"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Det_comlif
				With Request
					insValMantAgent = mobjMantAgent.insValMAG002_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("valComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Det_comlif
					With Request
						insValMantAgent = mobjMantAgent.insValMAG002(.QueryString("sCodispl"), .Form.Item("sAction"), mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_durat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_dur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_durat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valWay_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSellChannell"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			'+MAG003: Tabla de comisiones de generales
		Case "MAG003"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Det_comgen
				With Request
					insValMantAgent = mobjMantAgent.insValMAG003_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmtComtabge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Det_comgen
					With Request
                            insValMantAgent = mobjMantAgent.insValMAG003(.QueryString("sCodispl"), .Form.Item("sAction"), mobjValues.StringToType(Session("nComtabge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInit_Month"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFinal_Month"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valProduct_sBrancht"), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG004: Tabla de comisiones de generales
		Case "MAG004"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Tab_comrat
				With Request
					insValMantAgent = mobjMantAgent.insValMAG004_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("valTable_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("gmdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cboType_infor"))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Tab_comrat
					With Request
						
						insValMantAgent = mobjMantAgent.insValMAG004(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("nTable_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComrate"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG005: Creación de movimientos automáticos de cta/cte
		Case "MAG005"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Tab_compro
				With Request
					insValMantAgent = mobjMantAgent.insValMAG005_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("cboTransacType"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Tab_compro
					With Request
						insValMantAgent = mobjMantAgent.insValMAG005(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("nType_tran"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLine"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_acco"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeDebitSide"), mobjValues.StringToType(.Form.Item("cbeTyp_amount"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG006: Tablas de convenios económicos
		Case "MAG006"
			mobjMantAgent = New eAgent.Tab_Commission
			
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insValMantAgent = mobjMantAgent.insValMAG006_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("optCommType"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				
				If Request.Form.Item("hddAction") = "306" Then
					insValMantAgent = mobjMantAgent.insValMAG006dup(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nTable_cod"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctDesc"))
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						mobjMantAgent = New eAgent.Tab_Commission
						With Request
							insValMantAgent = mobjMantAgent.insValMAG006(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTable_cod"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeType_assig"), .Form.Item("cbeStatregt"))
						End With
					Else
						insValMantAgent = vbNullString
					End If
				End If
			End If
			
			
			'+MAG007: Tabla de esquema económico de intermediarios
		Case "MAG007"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Disex_int_d
				With Request
					insValMantAgent = mobjMantAgent.insValMAG007_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("valEco_sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Disex_int_d
					With Request
						insValMantAgent = mobjMantAgent.insValMAG007(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("nEco_sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeDisexpri"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG008: Cargos fijos de intermediarios
		Case "MAG008"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Int_fixval
				With Request
					insValMantAgent = mobjMantAgent.insValMAG008_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Int_fixval
					With Request
						insValMantAgent = mobjMantAgent.insValMAG008(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG554: Tablas de convenios de intermediarios
		Case "MAG554"
			mclscommiss_agree = New eAgent.commiss_agree
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantAgent = mclscommiss_agree.insValMAG554(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_Date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPerc_Comm"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+MAG573: Tabla de comisiones de supervisores
		Case "MAG573"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantAgent = New eAgent.Supervis_commis
				With Request
					insValMantAgent = mobjMantAgent.insValMAG573_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInterTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.Supervis_commis
					With Request
						insValMantAgent = mobjMantAgent.insValMAG573(.QueryString("sCodispl"), .Form.Item("sAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInterTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeLower_level"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCommiss"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypPort"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantAgent = vbNullString
				End If
			End If
			
			
			'+MAG597: Tabla de Participación en Bonos/Incentivos Generales		
		Case "MAG597"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mclsbonus_gen = New eAgent.bonus_gen
					
					insValMantAgent = mclsbonus_gen.insValMAG597_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPersist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReal_Goal"), eFunctions.Values.eTypeData.etdDouble))
					mclsbonus_gen = Nothing
				End If
			End With
			
			'+MAG598: Factores de cumplimientos de metas
		Case "MAG598"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mclsaccomp_factor = New eAgent.accomp_factor
					
					insValMantAgent = mclsaccomp_factor.insValMAG598_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmo_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmo_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
					mclsaccomp_factor = Nothing
				End If
			End With
			
			
			'+MAG751: Tabla de productividad promedio
			
		Case "MAG751"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.average_prod
					
					insValMantAgent = mobjMantAgent.insValMAG751_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
			'+MAG750: Tabla para bono de supervisores
			
		Case "MAG750"
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMantAgent = New eAgent.gen_bonsup
					insValMantAgent = mobjMantAgent.insValMAG750_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
			'+MAG576: Parámetros para intermediarios
		Case "MAG576"
			
			mclsinterm_param = New eAgent.interm_param
			With Request
				If .QueryString.Item("nMainAction") <> "401" Then
					insValMantAgent = mclsinterm_param.insValMAG576_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnInsu_Assist"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insValMantAgent = vbNullString
				End If
			End With
			
			'+MAG582: Tabla de incentivos de agentes de mantención
			
		Case "MAG582"
			mclsexcess_maint = New eAgent.excess_maint
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nIntertyp") = .Form.Item("cboIntertyp")
					Session("nBranch") = .Form.Item("cboBranch")
					Session("nProduct") = .Form.Item("valProduct")
					insValMantAgent = mclsexcess_maint.insValMAG582_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cboIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						
						insValMantAgent = mclsexcess_maint.insValMAG582(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cboType_hist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeDet_transac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInterType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValMantAgent = vbNullString
					End If
				End If
				mclsexcess_maint = Nothing
			End With
			
			'+MAG770: Modalidades de anticipos a otorgar
			
		Case "MAG770"
			mobjMantAgent = New eAgent.Advance_users
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantAgent = mobjMantAgent.InsValMAG770_K("MAG770", .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCodModPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+MAG780: Tablas de Porcentajes Máximos y minimos para anticipos
			
		Case "MAG780"
			lclsPercentAdvanc = New eAgent.PercentAdvanc
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insValMantAgent = lclsPercentAdvanc.insValMAG780_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeInterType"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantAgent = lclsPercentAdvanc.insValMAG780(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnIntermtyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentmax"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			lclsPercentAdvanc = Nothing
			
			'+MAG7780: Tablas de Metas Base
			
		Case "MAG7780"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mclstab_goals = New eAgent.tab_goals
					insValMantAgent = mclstab_goals.insValMAG7780_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctshort_des"), .Form.Item("cbeStatRegt"))
				End If
			End With
			mclstab_goals = Nothing
			
			'+MAG800: Tabla de presupuesto de dotación por agencia
			
		Case "MAG800"
			mobjMantAgent = New eAgent.Bud_agen
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantAgent = mobjMantAgent.InsValMAG800_K("MAG800", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAgent_quan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgency"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
			'+MAG801: Tabla de factor de cumplimiento por dotación de agentes
			
		Case "MAG801"
			mobjMantAgent = New eAgent.Agent_rate
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantAgent = mobjMantAgent.InsValMAG801_K("MAG801", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnInit_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ MAG7000: Comisiones especiales de vida.
		Case "MAG7000"
			mobjMantAgent = New eAgent.Tab_Spec_Comm
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantAgent = mobjMantAgent.InsValMAG7000_K(.QueryString("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valTab_ComLif"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantAgent = mobjMantAgent.InsValMAG7000("MAG7000", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nSlc_Tab_nr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommiss_Pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nPolicy_year_end_Aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType_comm"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctExist_Modul"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_Amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTypetable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdLong))
					End If
				End If
			End With
			
		Case Else
			insValMantAgent = "insValMantAgent: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantAgent: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantAgent() As Boolean
	'--------------------------------------------------------------------------------------------
	
	Dim lblnPost As Boolean
	Dim lclsTab_comLif As Object
	
	lblnPost = False
	
	Dim mclsbonus_gen As eAgent.bonus_gen
	Dim mclsaccomp_factor As eAgent.accomp_factor
	Dim mclsexcess_maint_p As eAgent.excess_maint
	Dim lclsPercentAdvanc As eAgent.PercentAdvanc
	Dim mclstab_goals As eAgent.tab_goals
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MAG001: Tipos de Intermediarios		
		Case "MAG001"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG001(.Form.Item("sAction"), 1, mobjValues.StringToType(.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), .Form.Item("cbeParticin"), .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeTyp_Acco"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkInd_FECU"), .Form.Item("chkGen_Certif"))
					
				End With
			Else
				lblnPost = True
			End If
			
			'+MAG002: Tabla de comisiones de vida
		Case "MAG002"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG002(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy_dur"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMin_durat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSellchannell"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMax_durat"), eFunctions.Values.eTypeData.etdDouble, True))
					
				End With
			Else
				lblnPost = True
				lclsTab_comLif = Nothing
				Session("nComtabli") = Request.Form.Item("valComtabli")
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
			End If
			
			mobjMantAgent = Nothing
			
			'+MAG003: Tabla de comisiones de generales
            Case "MAG003"
                mobjMantAgent = New eAgent.Det_comgen
                
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjMantAgent.insPostMAG003(.Form.Item("sAction"), mobjValues.StringToType(Session("nComtabge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInit_Month"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFinal_Month"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayfreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInstallments"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    lblnPost = True
                    Session("nComtabge") = Request.Form.Item("gmtComtabge")
                    Session("dEffecdate") = Request.Form.Item("gmdEffecdate")
                End If
			
                '+MAG004: Tabla de comisiones de generales
		Case "MAG004"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG004(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nTable_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComrate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
				Session("nTable_cod") = Request.Form.Item("valTable_cod")
				Session("nCurrency") = Request.Form.Item("cboCurrency")
				Session("sType_infor") = Request.Form.Item("cboType_infor")
				Session("dEffecdate") = Request.Form.Item("gmdEffecdate")
			End If
			
			'+MAG005: Creación de movimientos automáticos de cta/cte
		Case "MAG005"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG005(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nType_tran"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLine"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeDebitSide"), mobjValues.StringToType(.Form.Item("cbeTyp_acco"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
				Session("nType_tran") = Request.Form.Item("cboTransacType")
			End If
			
			
			'+MAG006: Tablas de convenios económicos
		Case "MAG006"
			If Request.Form.Item("hddAction") = "306" Then
				lblnPost = mobjMantAgent.insPostMAG006dup(mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnTableDup"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctDesc"), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("nTable_cod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					With Request
						lblnPost = mobjMantAgent.insPostMAG006(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTable_cod"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeType_assig"), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					lblnPost = True
					Session("nCommType") = Request.Form.Item("optCommType")
				End If
			End If
			
			'+MAG007: Tabla de esquema económico de intermediarios
		Case "MAG007"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG007(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nEco_sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeDisexpri"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
				Session("nEco_sche") = Request.Form.Item("valEco_sche")
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
			End If
			
			'+MAG008: Cargos fijos de intermediarios
		Case "MAG008"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG008(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
			End If
			
			'+MAG554: Tablas de convenios de intermediarios
		Case "MAG554"
			mclscommiss_agree = New eAgent.commiss_agree
			With Request
				lblnPost = mclscommiss_agree.insPostMAG554(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_Date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPerc_Comm"), eFunctions.Values.eTypeData.etdDouble))
			End With
			mclscommiss_agree = Nothing
			
			'+MAG573: Tabla de comisiones de supervisores
		Case "MAG573"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					lblnPost = mobjMantAgent.insPostMAG573(.Form.Item("sAction"), mobjValues.StringToType(Session("nInterTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeLower_level"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommiss"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypPort"), eFunctions.Values.eTypeData.etdDouble, True))
				End With
			Else
				lblnPost = True
				Session("nInterTyp") = Request.Form.Item("cbeInterTyp")
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
			End If
			
			'+MAG576: Parámetros para intermediarios
		Case "MAG576"
			With Request
				mclsinterm_param = New eAgent.interm_param
				If .QueryString.Item("nMainAction") <> "401" Then
					lblnPost = mclsinterm_param.insPostMAG576_K("MAG576", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsu_Assist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBonus_Curr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_Bonus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_Accomp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDay_discloan"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
			
			
			'+MAG597: Tabla de Participación en Bonos/Incentivos Generales
		Case "MAG597"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					mclsbonus_gen = New eAgent.bonus_gen
					lblnPost = mclsbonus_gen.insPostMAG597_K(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPersist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReal_Goal"), eFunctions.Values.eTypeData.etdDouble))
					mclsbonus_gen = Nothing
				End With
			Else
				lblnPost = True
			End If
			
			
			'+MAG598: Factores de cumplimientos de metas
		Case "MAG598"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					mclsaccomp_factor = New eAgent.accomp_factor
					lblnPost = mclsaccomp_factor.insPostMAG598_K(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmo_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmo_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
					mclsaccomp_factor = Nothing
				End With
			Else
				lblnPost = True
			End If
			
			
			'+MAG751: Tabla de productividad promedio
		Case "MAG751"
			With Request
				mobjMantAgent = New eAgent.average_prod
				lblnPost = mobjMantAgent.insPostMAG751_K(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			
			'+MAG750: Tabla para bono de supervisores          
		Case "MAG750"
			With Request
				mobjMantAgent = New eAgent.gen_bonsup
				lblnPost = mobjMantAgent.insPostMAG750_K(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			
			'+MAG582: Tabla de incentivos de agentes de mantención
		Case "MAG582"
			mclsexcess_maint_p = New eAgent.excess_maint
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nBranch") = mobjValues.StringToType(.Form.Item("cboBranch"), eFunctions.Values.eTypeData.etdDouble)
					Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
					Session("nInterType") = mobjValues.StringToType(.Form.Item("cboIntertyp"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mclsexcess_maint_p.insPostMAG582(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboType_hist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeDet_transac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+MAG770: Modalidades de anticipos a otorgar
			
		Case "MAG770"
			mobjMantAgent = New eAgent.Advance_users
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantAgent.InsPostMAG770_K("MAG770", .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCodModPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				Else
					lblnPost = True
				End If
			End With
			
			'+MAG780: Tablas de Porcentajes Máximos y minimos para anticipos
			
		Case "MAG780"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nIntermtyp=" & mobjValues.StringToType(.Form.Item("cbeInterType"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lclsPercentAdvanc = New eAgent.PercentAdvanc
						mstrQueryString = "&nIntermtyp=" & mobjValues.StringToType(.Form.Item("hddnIntermtyp"), eFunctions.Values.eTypeData.etdDouble)
						lblnPost = lclsPercentAdvanc.inspostMAG780(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnIntermtyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentmax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			lclsPercentAdvanc = Nothing
			
			'+MAG7780: Tablas de Metas Base
		Case "MAG7780"
			mclstab_goals = New eAgent.tab_goals
			With Request
				lblnPost = mclstab_goals.insPostMAG7780_K(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctshort_des"), .Form.Item("cbeStatRegt"))
			End With
			mclstab_goals = Nothing
			
			'+MAG800: Tabla de presupuesto de dotación por agencia
			
		Case "MAG800"
			mobjMantAgent = New eAgent.Bud_agen
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantAgent.InsPostMAG800_k(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAgent_quan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				Else
					lblnPost = True
				End If
			End With
			
			'+MAG801: Tabla de factor de cumplimiento por dotación de agentes
			
		Case "MAG801"
			mobjMantAgent = New eAgent.Agent_rate
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantAgent.InsPostMAG801_k(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnInit_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				Else
					lblnPost = True
				End If
			End With
			
			'+ MAG7000: Comisiones especiales de vida.
			'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
			
		Case "MAG7000"
			mobjMantAgent = New eAgent.Tab_Spec_Comm
                
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nBranch") = Request.Form.Item("cbeBranch")
					Session("nProduct") = Request.Form.Item("valProduct")
                        Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                        Session("nSlc_Tab_nr") = Request.Form.Item("valTab_ComLif")
					
					mstrQueryString = "&nSlc_Tab_nr=" & mobjValues.StringToType(.Form.Item("valTab_ComLif"), eFunctions.Values.eTypeData.etdDouble) & "&nTypetable=" & mobjValues.StringToType(.Form.Item("cbeTypetable"), eFunctions.Values.eTypeData.etdDouble)
					
					lblnPost = True
				Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            
                            
                            mstrQueryString = "&nSlc_Tab_nr=" & mobjValues.StringToType(.QueryString.Item("nSlc_Tab_nr"), eFunctions.Values.eTypeData.etdDouble) & "&nTypetable=" & mobjValues.StringToType(.QueryString.Item("nTypetable"), eFunctions.Values.eTypeData.etdDouble)
                            lblnPost = mobjMantAgent.InsPostMAG7000(.QueryString("Action"), mobjValues.StringToType(Session("nSlc_Tab_nr"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommiss_Pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeType_comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMax_Amount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypetable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdLong))
						
                        Else
                            lblnPost = True
                        End If
                    End If
                    'Response.Write("<SCRIPT>alert('Tabla: " & Session("nSlc_Tab_nr") & "');</" & "Script>")
                    'Response.Write("<SCRIPT>alert('Fecha: " & Session("dEffecdate") & "');</" & "Script>")
                    'Response.Write("<SCRIPT>alert('nProduct: " & Session("nProduct") & "');</" & "Script>")
			End With
			
	End Select
	insPostMantAgent = lblnPost
	
	mclsinterm_param = Nothing
	mclsexcess_maint_p = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "sModule=Maintenance&sProject=MantAgent&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



	 
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 5/07/04 22:25 $|$$Author: Nvaplat22 $"

//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantAgent
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantAgentError"",660,330);")
		'+ Solo si la acción es de duplicar para la tx MAG006
		If Request.QueryString.Item("sCodispl") = "MAG006" And CDbl(Request.Form.Item("hddAction")) = 306 Then
			.Write("self.history.go(-1);")
		Else
			.Write("document.location.href='/VTimeNet/common/blank.htm';")
		End If
		
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantAgent Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Select Case Request.QueryString.Item("sCodispl")
							Case "MAG750"
								Response.Write("<SCRIPT>;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & "_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							Case "MAG751"
								Response.Write("<SCRIPT>;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & "_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							Case "MAG597"
								Response.Write("<SCRIPT>;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & "_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							Case "MAG598"
								Response.Write("<SCRIPT>;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & "_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							Case "MAG576"
								Response.Write("<SCRIPT>;top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & "_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							Case Else
								Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
						End Select
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
                Select Case Request.QueryString.Item("sCodispl")
                    Case "MAG001"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG002"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG003"
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.opener.document.location.href='MAG003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.close();opener.top.opener.document.location.href='MAG003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        End If
                    Case "MAG004"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG005"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG006"
                        '+ Si la accion es duplicar
                        If CDbl(Request.Form.Item("hddAction")) = 306 Then
                            Response.Write("<SCRIPT>window.close(); top.opener.document.location.href='MAG006.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=306'</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.opener.document.location.href='MAG006.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        End If
                    Case "MAG007"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG007.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG008"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG008.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG554"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG554_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG573"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG573.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MAG597"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG597_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG598"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG598_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG751"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG751_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG750"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG750_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG582"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG582.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG770"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG770_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG780"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG780.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302" & mstrQueryString & "' </SCRIPT>")
                    Case "MAG7780"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG7780_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG576"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG576_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG800"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG800_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MAG801"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG801_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
					
                        '+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
                    Case "MAG7000"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAG7000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "' </SCRIPT>")
                End Select
		End If
	End If
End If
mobjMantAgent = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
</BODY>
</HTML>




