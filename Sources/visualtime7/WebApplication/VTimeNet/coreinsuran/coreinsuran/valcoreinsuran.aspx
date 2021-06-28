<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrLocation As String

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjCoReinsuran As Object
Dim mobjCoReinsuran_np As eCoReinsuran.Contrnpro
Dim mobjCoReinsuran_pc As eCoReinsuran.Part_contr
Dim mobjCoReinsuran_rt As eCoReinsuran.Retention
Dim mobjCoReinsuran_ct As eCoReinsuran.Cuentecn
Dim mclsCoReinsuran_win As eCoReinsuran.CoReinsuran_win
    Dim mobjCoReinsuran_risks As eCoReinsuran.Contrnp_Risks

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String
Dim lclsQuery As eRemoteDB.Query


'% insValCoReinsuran: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValCoReinsuran() As String
	Dim lintAction As Integer
	'--------------------------------------------------------------------------------------------
	Dim lclsContr_LimCov As eCoReinsuran.Contr_LimCov
	Dim lclsContr_comm As eCoReinsuran.Contr_comm
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CR301_k: Contratos Proporcionales (Header)
		Case "CR301_k", "CR301_K"
			With Request
				insValCoReinsuran = mobjCoReinsuran.insValCR301_k("CR301_k", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			'+ CR301: Contratos Proporcionales (Folder)
		Case "CR301"
			With Request
				insValCoReinsuran = mobjCoReinsuran.insValCR301("CR301", mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnReten"), eFunctions.Values.eTypeData.etdDouble), Session("nType"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLines"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReten_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuota_sha"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_even"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ChkLimCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("chkRetCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("chkretZone"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpiredate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ CR302: Comisiones y reservas	
		Case "CR302"
			With Request
				
				insValCoReinsuran = mobjCoReinsuran.insValCR302("CR302", mobjValues.StringToType(.Form.Item("tcnFixed_prat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGroup_co"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnTab_commi"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPrem_dep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFact_reser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInt_prem"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkReser_clai"), mobjValues.StringToType(.Form.Item("tcnInt_claim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboFqcy_acc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkCessprcov"), .Form.Item("chkCesscia"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCessprfix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkCommcov"), mobjValues.StringToType(.Form.Item("valCurrpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboFreqpay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkInd_age"), mobjValues.StringToType(.Form.Item("valInd_Age"), eFunctions.Values.eTypeData.etdDouble))
			End With
			'+ CR303: Participación de beneficios			
		Case "CR303"
			With Request
				insValCoReinsuran = mobjCoReinsuran.insValCR303("CR303", Session("nNumber"), mobjValues.StringToType(.Form.Item("tcnYear_begin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTran_prem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_claim"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CR304_k: Contratos No Proporcionales (Header)
		Case "CR304_K"
			With Request
                    insValCoReinsuran = mobjCoReinsuran_np.insValCR304_k("CR304_k", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cboContraType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cboBranch"), eFunctions.Values.eTypeData.etdInteger))
			End With
			
			'+ CR304: Contratos No Proporcionales (Folder)
		Case "CR304"
                insValCoReinsuran = mobjCoReinsuran_np.insValCR304("CR304", mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cboCurrencyContract"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnNumber_rep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPorc_rep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMax_even"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cboCurrencyPayment"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("dEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnNumberRepEven"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLifeNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnSpcpriority"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnSpclimit"), eFunctions.Values.eTypeData.etdInteger))
			
			'+ CR305: Tasas y primas
		Case "CR305"
			With Request
				insValCoReinsuran = mobjCoReinsuran_np.insValCR305("CR305", Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnPrem_dep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_fij"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_fij"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPaySess"), eFunctions.Values.eTypeData.etdDouble), .QueryString("WindowType"))
			End With
			
			'+ CR307: Compañías participantes
		Case "CR307"
			With Request
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					insValCoReinsuran = mobjCoReinsuran_pc.insValCR307("CR307", Session("sCodispl_CR"), "Normal", Session("nNumber"), Session("nType"), Session("dEffecdate"), Session("nBranch_rei"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, Session("sCessprcov"), Session("sCesscia"))
				Else
					If Request.QueryString.Item("Action") = "Add" Then
						lintAction = 1
					Else
						lintAction = 2
					End If
					insValCoReinsuran = mobjCoReinsuran_pc.insValCR307("CR307", Session("sCodispl_CR"), "PopUp", Session("nNumber"), Session("nType"), Session("dEffecdate"), Session("nBranch_rei"), lintAction, mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComision"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnArr_perd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_bene"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPr_inOut"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCl_inOut"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_fix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_fix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoucess"), Session("sCessprcov"), Session("sCesscia"))
				End If
			End With
                
                '+ CR309: Riesgos aceptados
            Case "CR309"
                With Request
                    insValCoReinsuran = mobjCoReinsuran_risks.insvalCR309(.QueryString.Item("Action"), .QueryString.Item("sCodispl"), Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate"), Session("nType"), .Form.Item("tctCode"), mobjValues.StringToType(.Form.Item("tcnSumInsured"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ CR020: Plenos y límites de retención
		Case "CR020"
			With Request
				If .QueryString.Item("WindowType") <> "PopUp" Then
					insValCoReinsuran = vbNullString
				Else
					insValCoReinsuran = mobjCoReinsuran_rt.insValCR020("CR020", "PopUp", mobjValues.StringToType(.Form.Item("cboRisk_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMin_Capita"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_Capita"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_rate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkExclusion"), mobjValues.StringToType(.Form.Item("tcnNew_retent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent_Ced"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+CR572 Retención por coberturas			
		Case "CR572"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjCoReinsuran = New eCoReinsuran.Retentioncov
					If Request.QueryString.Item("Action") <> "Update" Then
						insValCoReinsuran = mobjCoReinsuran.insvalCR572("CR572", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cboInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoutine"), mobjValues.StringToType(.Form.Item("cboCovPropor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComlim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCoverCL"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					End If
				End If
			End With
			
			'+ CR724: Límites por cobertura de un contrato proporcional - ACM - 11/09/2002
		Case "CR724"
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsContr_LimCov = New eCoReinsuran.Contr_LimCov
				
				insValCoReinsuran = lclsContr_LimCov.ValCR724(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valRelatedCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercentage_RelatedCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnnMaxAmount_RelatedCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCuota_Parte"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctRoutine"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
			End If
			lclsContr_LimCov = Nothing
			
			'+CR725 Cálculo de cesiones de prima por cobertura
		Case "CR725"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjCoReinsuran = New eCoReinsuran.contr_cescov
					insValCoReinsuran = mobjCoReinsuran.InsValCR725(Request.QueryString.Item("Action"), "CR725", mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctRouCess"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCessPrFix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("opcInOtherCov"), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optnTypecap"),mobjValues.StringToType(.Form.Item("ValCoverOther"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ CR731: Comision por cobertura de un contrato proporcional
		Case "CR731"
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsContr_comm = New eCoReinsuran.Contr_comm
				
                    insValCoReinsuran = lclsContr_comm.InsValCR731(Request.QueryString.Item("sCodispl"), _
                                                                   mobjValues.StringToType(Request.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   mobjValues.StringToType(Request.Form.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   Request.Form.Item("tctRoutine"), _
                                                                   mobjValues.StringToType(Request.Form.Item("tcnFirstYear"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   mobjValues.StringToType(Request.Form.Item("tcnNextYear"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   Session("nNumber"), Session("nBranch_rei"), Session("nType"), Session("dEffecdate"), Request.QueryString.Item("Action"))
                End If
                lclsContr_comm = Nothing
			
                '+CR758 Retención de cumulo por ramo/producto
            Case "CR758"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjCoReinsuran = New eCoReinsuran.Contr_Cumul
                        If Request.QueryString.Item("Action") <> "Update" Then
                            insValCoReinsuran = mobjCoReinsuran.insvalCR758("CR758", _
                                                                            mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                            mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), _
                                                                            mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                            mobjValues.StringToType(.Form.Item("valBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                            mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                            mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                            mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
			
                '+CR760 Retención por zonas 
            Case "CR760"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjCoReinsuran = New eCoReinsuran.Retentionzone
                        insValCoReinsuran = mobjCoReinsuran.insvalCR760(.QueryString("Action"), "CR760", mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSeismiczone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With
			
                '+ CRC001: Consulta de compañías de Co/Reaseguro
            Case "CRC001"
                If Request.QueryString.Item("nZone") = "1" Then
                    insValCoReinsuran = vbNullString
                Else
                    insValCoReinsuran = vbNullString
                End If
			
                '+ CRC003: Consulta de contratos de Co/Reaseguro
            Case "CRC003"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insValCoReinsuran = vbNullString
                Else
                    insValCoReinsuran = vbNullString
                End If
			
                '+ GE101: Ventana cancelación de proceso	
            Case "GE101"
                insValCoReinsuran = ""
			
            Case Else
                insValCoReinsuran = "insValCoReinsuran: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
End Function

'% insPostCoReinsuran: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostCoReinsuran() As Boolean
	Dim ldtmNullDat As Object
	Dim lstrAction As String
	Dim lintAction As Integer
	Dim ldtmCompDate As Date
	Dim ldtmNullDate As Object
	Dim nAction As Object
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	Dim lclsContr_LimCov As eCoReinsuran.Contr_LimCov
	Dim lclsContr_comm As eCoReinsuran.Contr_comm
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CR301_k: Contratos Proporcionales (Header)
		Case "CR301_k", "CR301_K"
			With Request
				If mobjCoReinsuran.insPostCR301_k("CR301_k", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
					
					Session("dEffecdate") = mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
					Session("nNumber") = mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble)
					Session("nType") = mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("nBranch_rei") = mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sCodispl_CR") = .QueryString.Item("sCodispl")
					Session("nMainAction") = mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger)
					
					If Session("nType") <> 1 Then
						If mobjCoReinsuran.Find(0, 1, Session("nBranch_rei"), Session("dEffecdate")) Then
							Session("sRetZone") = mobjCoReinsuran.sRetZone
							Session("sRetCover") = mobjCoReinsuran.sRetCover
							Session("dblRetention") = mobjCoReinsuran.nAmount
						End If
					End If
					lblnPost = True
				End If
			End With
			
			'+ CR301: Contratos Proporcionales (Folder)
		Case "CR301"
			With Request
				mobjCoReinsuran = New eCoReinsuran.Contrproc
                    lblnPost = mobjCoReinsuran.insPostCR301("CR301", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnReten"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLines"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReten_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuota_sha"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_even"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdExpiredate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkRetCover"), .Form.Item("chkretZone"), .Form.Item("ChkLimCover"), .Form.Item("cbeCumulo"), .Form.Item("cbeMethod"), .Form.Item("OptCumulpol"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxRetAmount"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CR302: Comisiones y reservas
		Case "CR302"
			With Request
				lblnPost = mobjCoReinsuran.insPostCR302("CR302", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFixed_prat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGroup_co"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnTab_commi"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPrem_dep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFact_reser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInt_prem"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkReser_clai"), mobjValues.StringToType(.Form.Item("tcnInt_claim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboFqcy_acc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optCap_nom_ri"), .Form.Item("chkCessprcov"), .Form.Item("chkCesscia"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCessprfix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkExtraprem"), .Form.Item("chkGencess"), .Form.Item("chkCommcov"), mobjValues.StringToType(.Form.Item("tcnextmonthc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNextyearc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboFreqpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNextmonthpa"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNextyearpa"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrpay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optFormpay"), mobjValues.StringToType(.Form.Item("tcnMinimumCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkInd_age"), mobjValues.StringToType(.Form.Item("valInd_Age"), eFunctions.Values.eTypeData.etdDouble))
				
				
				
				If lblnPost Then
					Session("sCessprcov") = .Form.Item("chkCessprcov")
					Session("sCesscia") = .Form.Item("chkCesscia")
				End If
				
				
			End With
			
			'+ CR303: Participación de beneficios			
		Case "CR303"
			With Request
				lblnPost = mobjCoReinsuran.insPostCR303("CR303", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(.Form.Item("tcnYear_begin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTran_prem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_claim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProfit_sh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGroup_bene"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnExpenses"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExcess"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CR304_k: Contratos No Proporcionales (Header)
		Case "CR304_K"
			With Request
                    lblnPost = mobjCoReinsuran_np.insPostCR304_k("CR304_k", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdInteger), CDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("cboContraType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cboBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger))
				If lblnPost Then
					Session("dEffecdate") = mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
					Session("nNumber") = mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble)
					Session("nType") = mobjValues.StringToType(.Form.Item("cboContraType"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("nBranch_rei") = mobjValues.StringToType(.Form.Item("cboBranch"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sCodispl_CR") = .QueryString.Item("sCodispl")
					Session("nMainAction") = mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger)
				End If
			End With
			
			'+ CR304: Contratos No Proporcionales (Folder)(Límites)
		Case "CR304"
			With Request
                    lblnPost = mobjCoReinsuran_np.insPostCR304("CR304", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(.Form.Item("cboCurrencyContract"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber_rep"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPorc_rep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_even"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDeducible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCurrencyPayment"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMora"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboFrequency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthPF"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYearPF"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthCT"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYearCT"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoutine"), mobjValues.StringToType(.Form.Item("dEndDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcnMaxRespEven"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumberRepEven"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optProrateRep"), mobjValues.StringToType(.Form.Item("tcnLifeNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnSpcpriority"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnSpclimit"), eFunctions.Values.eTypeData.etdInteger))
			End With
			
			'+ CR305: Tasas y primas
		Case "CR305"
			With Request
				
				If Request.QueryString.Item("Action") = "Add" Then
					nAction = "301"
				Else
					nAction = "302"
				End If
				
				
				lblnPost = mobjCoReinsuran_np.insPostCR305("CR305", nAction, Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), .Form.Item("optReinsuran"), mobjValues.StringToType(.Form.Item("tcnPrem_dep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_fij"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_fij"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnPaySess"), eFunctions.Values.eTypeData.etdDouble), .QueryString("WindowType"), mobjValues.StringToType(.Form.Item("chkPlan_pay"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctRoutine"),,,,mobjValues.StringToType(.Form.Item("tcnEpi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEpi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcntax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaimadj"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalref"), eFunctions.Values.eTypeData.etdDouble))
				
				mstrString = "&nPrem_dep=" & .Form.Item("hddPrem_dep") & "&nPlan_pay=1"
				
			End With
			
			'+ CR307: Compañías participantes
		Case "CR307"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjCoReinsuran_pc.insPostCR307("CR307", Session("sCodispl_CR"), 3, Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComision"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnArr_perd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_bene"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPr_inOut"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCl_inOut"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_fix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_fix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoucess"), .Form.Item("tctRouProfit"), mobjValues.StringToType(.Form.Item("tcnAmoProfit"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeFreqProfit"))
				Else
					lblnPost = True
				End If
			End With

                '+ CR309: Riesgos aceptados
            Case "CR309"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjCoReinsuran_risks.insPostCR309(.QueryString.Item("Action"), Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate"), Session("nType"), .Form.Item("tctCode"), mobjValues.StringToType(.Form.Item("tcnSumInsured"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("chkSpApply"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        lblnPost = True
                    End If
                End With

                '+ CR020: Plenos y límites de retención
		Case "CR020"
			With Request
				If .QueryString.Item("WindowType") <> "PopUp" Then
					lblnPost = True
				Else
					lblnPost = mobjCoReinsuran_rt.insPostCR020("CR020", CInt(.Form.Item("tcnSel")), "2", Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboRisk_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMin_Capita"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_Capita"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_rate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkExclusion"), mobjValues.StringToType(.Form.Item("tcnLines_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNew_retent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent_Ced"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				End If
			End With
			
			'+ CR572 Retención por cobertura
		Case "CR572"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjCoReinsuran = New eCoReinsuran.Retentioncov
					lblnPost = mobjCoReinsuran.InspostCR572Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cboInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoutine"), mobjValues.StringToType(.Form.Item("cboCovPropor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComlim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboCoverCL"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					mobjCoReinsuran = Nothing
				End If
			End With
			
			'+ CR724: Límites por cobertura de un contrato proporcional - JRG - 24/09/2002
		Case "CR724"
			
			Select Case Request.QueryString.Item("Action")
				Case "Add"
					lintAction = 1
					ldtmNullDate = Nothing
					ldtmCompDate = Today
				Case "Update"
					lintAction = 2
					ldtmNullDate = Session("dEffecdate")
					ldtmCompDate = Today
				Case "Del", "Delete"
					lintAction = 3
					ldtmNullDate = Session("dEffecdate")
					ldtmCompDate = Today
			End Select
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsContr_LimCov = New eCoReinsuran.Contr_LimCov
				
				lblnPost = lclsContr_LimCov.PostCR724(lintAction, Session("nNumber"), Session("nBranch_rei"), Session("nType"), mobjValues.StringToType(Request.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), ldtmNullDate, mobjValues.StringToType(Request.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCuota_parte"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctRoutine"), mobjValues.StringToType(Request.Form.Item("valRelatedCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercentage_RelatedCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnnMaxAmount_RelatedCover"), eFunctions.Values.eTypeData.etdDouble), ldtmCompDate, Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
			Else
				lblnPost = True
			End If
			lclsContr_LimCov = Nothing
			lclsContr_LimCov = Nothing
			lintAction = Nothing
			ldtmNullDate = Nothing
			ldtmCompDate = Nothing
			
			'+ CR725: Cálculo de cesión de prima por cobertura
		Case "CR725"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjCoReinsuran = New eCoReinsuran.contr_cescov
					
					
                        lblnPost = mobjCoReinsuran.InsPostCR725(.QueryString("Action"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctRoucess"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCessPrFix"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("opcInOtherCov"), mobjValues.StringToType(.Form.Item("optnTypeCap"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ValCoverOther"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ CR726: Tasas primas de reaseguro de un contrato I
		Case "CR726"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nBranch_rei=" & .Form.Item("cbenBranch_rei") & "&nNumber=" & .Form.Item("tcnNumber") & "&nType=" & .Form.Item("cbeType") & "&dStartdate=" & .Form.Item("tcdStartdate") & "&nCovergen=" & .Form.Item("valCovergen") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
				End If
			End With
			
			'+ CR731: Comision por cobertura de un contrato proporcional
		Case "CR731"
			
			
			Select Case Request.QueryString.Item("Action")
				Case "Add"
					lstrAction = "Add"
					ldtmNullDat = Nothing
				Case "Update"
					lstrAction = "Update"
					ldtmNullDat = Session("dEffecdate")
			End Select
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsContr_comm = New eCoReinsuran.Contr_comm
				
				lblnPost = lclsContr_comm.insPostCR731(lstrAction, mobjValues.StringToType(Request.Form.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctRoutine"), mobjValues.StringToType(Request.Form.Item("tcnFirstYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnNextYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPermExp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnTempExp"), eFunctions.Values.eTypeData.etdDouble), Session("nNumber"), Session("nBranch_rei"), Session("nType"), Session("dEffecdate"), ldtmNullDat, Session("nUsercode"))
			Else
				lblnPost = True
			End If
			
			'+ CR758: Control de cúmulo por ramo/producto		
		Case "CR758"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjCoReinsuran = New eCoReinsuran.Contr_Cumul
                        lblnPost = mobjCoReinsuran.InspostCR758Upd(.QueryString("Action"), _
                                                     mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                     mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), _
                                                     mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), _
                                                     mobjValues.StringToType(.Form.Item("valBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                     mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                     mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                     mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ CR760: Retención por Zona		
		Case "CR760"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjCoReinsuran = New eCoReinsuran.Retentionzone
					lblnPost = mobjCoReinsuran.InspostCR760Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnSeismicZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
			lclsContr_comm = Nothing
			lstrAction = Nothing
			ldtmNullDate = Nothing
			ldtmCompDate = Nothing
			
			'+ CRC001: Consulta de compañías de Co/Reaseguro
		Case "CRC001"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
			Else
				lblnPost = True
			End If
			
			'+ CRC003: Consulta de contratos de Co/Reaseguro
			
		Case "CRC003"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
			Else
				lblnPost = True
			End If
			
			'+ GE101: Ventana cancelación de proceso	
		Case "GE101"
			lblnPost = insCancel
			
	End Select
	insPostCoReinsuran = lblnPost
End Function

'% insCancel: Esta rutina es activada cuando el usuario cancela la transacción que este
'% ejecutando.
'--------------------------------------------------------------------------------------------
Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsValues As eFunctions.Values
	Dim lclsContrmaster As eCoReinsuran.Contrmaster
	Dim sCodispl_CR As String
	
	lclsContrmaster = New eCoReinsuran.Contrmaster
	lclsValues = New eFunctions.Values
	
	If CStr(Session("sCodispl_CR")) = "CR301_K" Or CStr(Session("sCodispl_CR")) = "CR301_k" Then
		sCodispl_CR = "CR301"
	ElseIf CStr(Session("sCodispl_CR")) = "CR304_K" Then 
		sCodispl_CR = "CR304"
	End If
	
	insCancel = True
	mstrLocation = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=" & sCodispl_CR & "&sProject=CoReinsuran&sModule=CoReinsuran'"
	
	If Request.Form.Item("optElim") = "Delete" Then
		Call lclsContrmaster.Delete(Session("sCodispl_CR"), Session("nNumber"), Session("nType"), Session("dStartdate"), Session("nBranch_rei"))
	Else
		Call lclsContrmaster.updContrMasterStatregt(Session("sCodispl_CR"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), "2")
	End If
	
	lclsContrmaster = Nothing
	
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=CoReinsuran&sProject=CoReinsuran&sCodisplReload=" & Request.QueryString.Item("sCodispl")


%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>

<%
If Request.QueryString.Item("nZone") = "1" Then
	%>
		<BODY>
<%	
Else
	%>
		<BODY CLASS="Header">
<%	
End If
%>

<SCRIPT>
//----------------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//----------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//----------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%

mobjValues = New eFunctions.Values
mobjCoReinsuran = New eCoReinsuran.Contrproc
mobjCoReinsuran_np = New eCoReinsuran.Contrnpro
mobjCoReinsuran_pc = New eCoReinsuran.Part_contr
mobjCoReinsuran_rt = New eCoReinsuran.Retention
mobjCoReinsuran_ct = New eCoReinsuran.Cuentecn
mclsCoReinsuran_win = New eCoReinsuran.CoReinsuran_win
    mobjCoReinsuran_risks = New eCoReinsuran.Contrnp_Risks
    
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValCoReinsuran
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
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoReinsuranSeqError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostCoReinsuran Then
			
			
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("sCodispl") <> "CRC001" And Request.QueryString.Item("sCodispl") <> "CRC001_K" And Request.QueryString.Item("sCodispl") <> "CRC003_K" And Request.QueryString.Item("sCodispl") <> "CRC003" Then
					'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
					'+ se mueve automaticamente a la siguiente página
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						If Request.QueryString.Item("nMainAction") <> vbNullString Then
							Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Session("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&nYear_contr=" & Session("nYear_contr") & "&nBranch=" & Session("nBranch") & "&dContrDate=" & Session("dEffecdate") & "&nYearSer=" & Session("nYearSer") & "&nCompany=" & Session("nCompany") & "&nPerType=" & Session("nPerType") & "&nPerNum=" & Session("nPerNum") & "&sBussiType=" & Session("sBussiType") & "&nCurrency=" & Session("nCurrency") & "&sCodispl=" & Session("sCodispl") & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&nYear_contr=" & Session("nYear_contr") & "&nBranch=" & Session("nBranch") & "&dContrDate=" & Session("dEffecdate") & "&nYearSer=" & Session("nYearSer") & "&nCompany=" & Session("nCompany") & "&nPerType=" & Session("nPerType") & "&nPerNum=" & Session("nPerNum") & "&sBussiType=" & Session("sBussiType") & "&nCurrency=" & Session("nCurrency") & "&sCodispl=" & Session("sCodispl") & """;</SCRIPT>")
					End If
				End If
			Else
				
				'+ Se reacerga la secuencia para verificar el contenido de las ventanas
				If Request.QueryString.Item("sCodispl") <> "CRC001" And Request.QueryString.Item("sCodispl") <> "CRC001_K" And Request.QueryString.Item("sCodispl") <> "CRC003_K" And Request.QueryString.Item("sCodispl") <> "CRC003" Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No&nOpener=" & Request.QueryString.Item("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&nYear_contr=" & Session("nYear_contr") & "&nBranch=" & Session("nBranch") & "&dContrDate=" & Session("dEffecdate") & "&nYearSer=" & Session("nYearSer") & "&nCompany=" & Session("nCompany") & "&nPerType=" & Session("nPerType") & "&nPerNum=" & Session("nPerNum") & "&sBussiType=" & Session("sBussiType") & "&nCurrency=" & Session("nCurrency") & "&sCodispl=" & Session("sCodispl") & """;</SCRIPT>")
				End If
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "GE101"
						Response.Write("<SCRIPT>opener.top.document.location.href=" & mstrLocation & ";</SCRIPT>")
					Case "CR305"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR305.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
					Case "CR307"
						'Response.Write "<NOTSCRIPT>window.close();opener.top.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&nYear_contr=" & Session("nYear_contr") & "&nBranch=" & Session("nBranch") & "&dContrDate=" & Session("dEffecdate") & "&nYearSer=" & Session("nYearSer") & "&nCompany=" & Session("nCompany") & "&nPerType=" & Session("nPerType") & "&nPerNum=" & Session("nPerNum") & "&sBussiType=" & Session("sBussiType") & "&nCurrency=" & Session("nCurrency") & "&sCodispl=" & Session("sCodispl") & """;</SCRIPT>"
						'Response.Write "<NOTSCRIPT>window.close();opener.top.close();</SCRIPT>"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR307.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                        Case "CR309"
                            Response.Write("<SCRIPT>top.opener.document.location.href='CR309.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                        Case "CR020"
                            Response.Write("<SCRIPT>top.opener.document.location.href='CR020.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CRC001"
						Response.Write("<SCRIPT>top.opener.document.location.href='CRC001_k.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nCompany=" & Server.URLEncode(Request.Form.Item("tcnCompany")) & "&sCliename=" & Server.URLEncode(Request.Form.Item("tctCompanyName")) & "&sType=" & Server.URLEncode(Request.Form.Item("cbeType")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CRC003"
						Response.Write("<SCRIPT>top.opener.document.location.href='CRC003_k.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&dStartdate=" & Server.URLEncode(Request.Form.Item("dStartdate")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR572"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR572.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR758"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR758.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR760"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR760.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR725"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR725.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR724"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR724.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "CR731"
						Response.Write("<SCRIPT>top.opener.document.location.href='CR731.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNumber=" & Server.URLEncode(Request.Form.Item("tcnNumber")) & "&nType=" & Server.URLEncode(Request.Form.Item("cboType")) & "&nBranch=" & Server.URLEncode(Request.Form.Item("cboBranch")) & "&nCurrency=" & Server.URLEncode(Request.Form.Item("cboCurrency")) & "&nYear_contr=" & Server.URLEncode(Request.Form.Item("tcnYear_contr")) & "&continue=" & Server.URLEncode("S") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				End Select
			End If
			If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
				Session("bQuery") = True
			Else
				Session("bQuery") = False
			End If
		End If
	End If
Else
	If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		'+ Se recarga la página principal de la secuencia			
		If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And Request.QueryString.Item("sCodispl") <> "CRC001" And Request.QueryString.Item("sCodispl") <> "CRC003" Then
			If Not mclsCoReinsuran_win.UpdContrMasterState(CInt(Request.QueryString.Item("nAction")), Session("nType"), UCase(Session("sCodispl_CR")), Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate")) Then
				lclsQuery = New eRemoteDB.Query
				
				Call lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum=3902")
				
				Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & "')</SCRIPT>")
				
				lclsQuery = Nothing
				
				Response.Write("<SCRIPT>top.frames['fraFolder'].document.location.reload();</SCRIPT>")
			Else
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			End If
		Else
			Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
		End If
	End If
End If
mobjValues = Nothing
mobjCoReinsuran = Nothing
mobjCoReinsuran_np = Nothing
mobjCoReinsuran_pc = Nothing
mobjCoReinsuran_rt = Nothing
mobjCoReinsuran_ct = Nothing
%>
</BODY>
</HTML>





