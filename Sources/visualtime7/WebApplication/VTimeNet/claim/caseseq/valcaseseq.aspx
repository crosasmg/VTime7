<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisuaglTimer Utility

    Dim mobjValues As eFunctions.Values

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    Dim mstrErrors As String
    Dim mobjCaseSeq As Object


    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String
        Dim nLine As Integer
    Dim nAmountAdjustCapital As Double 
    Dim nIndAdjustCapital As Integer     
        '--------------------------------------------------------------------------------------------
	
        '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
        mobjNetFrameWork.BeginProcess("insvalSequence")
        '~End Header Block VisualTimer Utility
	
	
	
        Dim oOrigin As eClaim.Claim_origin
        Select Case Request.QueryString("sCodispl")
            '+ Secuencia de actualización de casos
		
            '+ SI019 : Datos de terceros de automovil
            Case "SI019"
                mobjCaseSeq = New eClaim.Claim_thir
                insvalSequence = mobjCaseSeq.insValSI019(Request.QueryString("sCodispl"), 
                                                         Request.Form("tctRegister"), 
                                                         Request.Form("tctChassis"), 
                                                         Request.Form("tctMotor"), 
                                                         Request.Form("cbeLicense_ty"), 
                                                         Request.Form("valVehCode"), 
                                                         mobjValues.StringToType(Request.Form("tcnProvider"), eFunctions.Values.eTypeData.etdDouble), 
                                                         Session("nClaim"), 
                                                         Session("nCase_num"), 
                                                         Session("nDeman_type"), 
                                                         mobjValues.StringToType(Request.Form("cbeBlame"), eFunctions.Values.eTypeData.etdDouble, True), 
                                                         Request.Form("chkRecov_ind"), 
                                                         mobjValues.StringToType(Request.Form("tcnRecov_per"), eFunctions.Values.eTypeData.etdDouble, True), 
                                                         Session("sBrancht"), Request.Form("tctDigit"))
			
                '+ SI023 : Datos generales de terceros
            Case "SI023"
                mobjCaseSeq = New eClaim.Claim_thir
                insvalSequence = mobjCaseSeq.insValSI023(Request.QueryString("sCodispl"), Request.Form("cboBlame"))
			
                '+ SI024 - Datos de siniestros de vida - ACM - 06/02/2001
            Case "SI024"
                mobjCaseSeq = New eClaim.Life_claim
                With Request
                    If Not IsNothing(.Form.GetValues("hddOrigin")) Then
                        For nLine = 0 To Request.Form.GetValues("hddOrigin").Count - 1
                            oOrigin = New eClaim.Claim_origin
                            oOrigin.nClaim = Session("nClaim")
                            oOrigin.nCase_num = Session("nCase_num")
                            oOrigin.nDeman_type = Session("nDeman_type")
					    oOrigin.nOrigin = mobjValues.StringToType(Request.Form.GetValues("hddOrigin").GetValue(nLine), eFunctions.Values.eTypeData.etdLong)
					    oOrigin.nVP = mobjValues.StringToType(Request.Form.GetValues("hddVP").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
					    oOrigin.nTax_benefit = mobjValues.StringToType(Request.Form.GetValues("hddTaxBenefit").GetValue(nLine), eFunctions.Values.eTypeData.etdLong)
					    oOrigin.nTransf_percent = mobjValues.StringToType(Request.Form.GetValues("tcnTransPercent").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
					    oOrigin.nTransf_amount = mobjValues.StringToType(Request.Form.GetValues("tcnTransfAmount").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
					    oOrigin.nTax_amount = mobjValues.StringToType(Request.Form.GetValues("tcnTax_Amount").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
					    oOrigin.nBalance = mobjValues.StringToType(Request.Form.GetValues("tcnBalance").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
                            oOrigin.nUsercode = Session("nUserCode")
                            mobjCaseSeq.Origins.Add(oOrigin)
                        Next
                    End If
				
                    mobjCaseSeq.nCoverCapital = mobjValues.StringToType(Request.Form("tcnCapitalAPV"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nTransf_amount = mobjValues.StringToType(Request.Form("tcnTransf_amount"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nApv_tax = mobjValues.StringToType(Request.Form("tcnApv_tax"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nApv_benef_balance = mobjValues.StringToType(Request.Form("tcnApv_benef_balance"), eFunctions.Values.eTypeData.etdDouble, True)
                    mobjCaseSeq.nOption = mobjValues.StringToType(Request.Form("cbeOption"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nAFP = mobjValues.StringToType(Request.Form("cbeAFP"), eFunctions.Values.eTypeData.etdDouble, True)
                    mobjCaseSeq.nCurrency = mobjValues.StringToType(Request.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nStay_Bonus = mobjValues.StringToType(Request.Form("tcnStayBonus"), eFunctions.Values.eTypeData.etdDouble)
				
                nAmountAdjustCapital = mobjValues.StringToType(.Form("gmnCapital"), eFunctions.Values.eTypeData.etdDouble)
                nIndAdjustCapital = mobjValues.StringToType(.Form("chkEnabledCapital"), eFunctions.Values.eTypeData.etdInteger)

                If nAmountAdjustCapital <= 0 Then
                    nAmountAdjustCapital = 0
                End If

                If nIndAdjustCapital <= 0 Then
                    nIndAdjustCapital = 0
                End If

				insvalSequence = mobjCaseSeq.insValSI024("SI024", Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), mobjValues.StringToType(.Form("cbeClaimType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeIndemnity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdEnd_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmnMonth_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnIndemn"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("ldblIndemni"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form("cbePayFreq"), mobjValues.StringToType(.Form("gmnDisabilityRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnGrowth_RateI"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnGrowth_RateE"), eFunctions.Values.eTypeData.etdDouble), nIndAdjustCapital, nAmountAdjustCapital)
                End With
			
                '+SI018: Datos de siniestro de automóvil
            Case "SI018"
                mobjCaseSeq = New eClaim.Claim_auto
                With Request
				
                    insvalSequence = mobjCaseSeq.insValSI018("SI018", mobjValues.StringToType(CStr(eFunctions.Menues.TypeActions.clngActionadd), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboBlame"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboInfraction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAutoQuant"), eFunctions.Values.eTypeData.etdDouble), .Form("tctDriverCod"), mobjValues.StringToType(.Form("cbeWorksh"), eFunctions.Values.eTypeData.etdDouble), .Form("tctDriver"), .Form("tctFatherLastName"), .Form("tctNames"), .Form("chkDenunc"), .Form("chkSummary"), .Form("chkIntervEIR"), mobjValues.StringToType(.Form("tcnPartNumber"), eFunctions.Values.eTypeData.etdDouble), .Form("tctTribunal"), mobjValues.StringToType(.Form("dtcAccusationDate"), eFunctions.Values.eTypeData.etdDate), .Form("tctPoliceStation"), mobjValues.StringToType(.Form("tcnFolio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnParagraph"), eFunctions.Values.eTypeData.etdDouble), .Form("tctPoliceStation2"), mobjValues.StringToType(.Form("tcnEnduranceNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("dtcEnduranceDate"), eFunctions.Values.eTypeData.etdDate), .Form("chkAlcohol"), mobjValues.StringToType(.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), .Form("tctWitness"), .Form("tctFatherLastNameWitness"), .Form("tctNamesWitness"))
                End With
                '+ Datos de Persona siniestrada
            Case "SI070"
                mobjCaseSeq = New eClaim.Claim_peop
                insvalSequence = mobjCaseSeq.insValSI070(mobjValues.StringToType(Request.Form("cboDamagesTy"), eFunctions.Values.eTypeData.etdDouble))
                'UPGRADE_NOTE: Object mobjCaseSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjCaseSeq = Nothing
			
                '+SI020: Daños ocurridos en el siniestro
            Case "SI020"
                mobjCaseSeq = New eClaim.Claim_Dama
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        insvalSequence = mobjCaseSeq.insValSI020("SI020", .QueryString("Action"), mobjValues.StringToType(.Form("valDamage_cod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeMag_dam"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
			
                '+SI028: Datos del siniestro de coberturas de salud
            Case "SI028"
                mobjCaseSeq = New eClaim.Claim_attm
                With Request
				
                    If .QueryString("WindowType") <> "PopUp" Then
					
                        If CStr(Session("LastName")) = "" Then
                            Session("LastName") = .Form("tctLastName")
                        ElseIf CStr(Session("FirstName")) = "" Then
                            Session("FirstName") = .Form("tctFirstName")
                        ElseIf CStr(Session("LastNameProf")) = "" Then
                            Session("LastNameProf") = .Form("tctLastNameProf")
                        ElseIf CStr(Session("FirstNameProf")) = "" Then
                            Session("FirstNameProf") = .Form("tctFirstNameProf")
                        End If
					
                        insvalSequence = mobjCaseSeq.insValSI028(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), .Form("valIllness"), mobjValues.StringToType(.Form("valClinic"), eFunctions.Values.eTypeData.etdDouble), .Form("dtcClient"), Session("LastName"), Session("FirstName"), mobjValues.StringToType(.Form("cbeService"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProf"), eFunctions.Values.eTypeData.etdDouble), .Form("dtcClientProf"), Session("LastNameProf"), Session("FirstNameProf"), mobjValues.StringToType(.Form("tcdInitIlldate"), eFunctions.Values.eTypeData.etdDate), .Form("hddHealth_system"), .Form("tctHealth_sys_other"))
                    Else
                        insvalSequence = mobjCaseSeq.insValSI028Upd(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdDate"), eFunctions.Values.eTypeData.etdDate), .Form("tctDescript"), mobjValues.StringToType(.Form("cbeStatus"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"))
                    End If
				
                End With
		Case "SI090"
                insvalSequence = ""

            Case "si700"
                insvalSequence = ""
			
            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString("sCodispl") & ")"
        End Select
	
	
	
        '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
        mobjNetFrameWork.FinishProcess("insvalSequence")
        '~End Header Block VisualTimer Utility
	
	
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        Dim nLine As Integer
        Dim lintRecov_ind As String
        Dim lintHealth_System As Object
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        lblnPost = False
	Dim nAmtInd As Double 
        Dim nGrothwRateI As Double
        Dim nGrothwRateE As Double
    Dim nAmountAdjustCapital As Double 
    Dim nIndAdjustCapital As Integer     
	
        '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
        mobjNetFrameWork.BeginProcess("insPostSequence")
        '~End Header Block VisualTimer Utility
	
	
        Dim oOrigin As eClaim.Claim_origin
        Dim mobjClaimPeop As eClaim.Claim_peop
        Select Case Request.QueryString("sCodispl")
            '+ Nombre de la ventana
		
            '+ SI019 : Datos de terceros de automovil
            Case "SI019"
                mobjCaseSeq = New eClaim.Claim_thir
			
                If Request.Form("chkRecov_ind") <> "1" Then
                    lintRecov_ind = "2"
                Else
                    lintRecov_ind = "1"
                End If
			
                lblnPost = mobjCaseSeq.insPostSI019(Request.QueryString("sCodispl"), Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), mobjValues.StringToType(Request.Form("cbeBlame"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form("cbeLicense_ty"), Request.Form("tctRegister"), Request.Form("tctThir_claim"), mobjValues.StringToType(Request.Form("cbeThir_comp"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form("tctThir_polic"), mobjValues.StringToType(Request.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctMotor"), Request.Form("tctChassis"), lintRecov_ind, mobjValues.StringToType(Request.Form("tcnRecov_Per"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctColor"), Request.Form("valVehCode"), Request.Form("tctDigit"), Session("nUsercode"), Request.Form("ValVehMark"), Request.Form("ValVehModel"), Request.Form("tcnYear"))
			
			
                '+ SI023 : Datos generales de terceros
            Case "SI023"
                mobjCaseSeq = New eClaim.Claim_thir
                lblnPost = mobjCaseSeq.insPostSI023(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), Request.Form("cboBlame"), Request.Form("gmtThirCLaim"), Request.Form("cboComp"), Request.Form("gmtThirPolicy"), Request.Form("tcnNotenum"), Request.QueryString("sCodispl"), Session("nUsercode"))
			
                '+ SI024 - Datos de siniestros de vida - ACM - 06/02/2001
            Case "SI024"
                mobjCaseSeq = New eClaim.Life_claim
			
                With Request
                    Session("nPayFreq") = mobjValues.StringToType(.Form("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble)
                    If mobjCaseSeq.Origins.Count = 0 Then
                        'For nLine = 1 To Request.Form("hddOrigin").Count
                        If Not IsNothing(.Form.GetValues("hddOrigin")) Then
                            For nLine = 0 To Request.Form.GetValues("hddOrigin").Count - 1
                                oOrigin = New eClaim.Claim_origin
                                oOrigin.nClaim = Session("nClaim")
                                oOrigin.nCase_num = Session("nCase_num")
                                oOrigin.nDeman_type = Session("nDeman_type")
						    oOrigin.nOrigin = mobjValues.StringToType(Request.Form.GetValues("hddOrigin").GetValue(nLine), eFunctions.Values.eTypeData.etdLong)
						    oOrigin.nVP = mobjValues.StringToType(Request.Form.GetValues("hddVP").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
						    oOrigin.nTax_benefit = mobjValues.StringToType(Request.Form.GetValues("hddTaxBenefit").GetValue(nLine), eFunctions.Values.eTypeData.etdLong)
						    oOrigin.nTransf_percent = mobjValues.StringToType(Request.Form.GetValues("tcnTransPercent").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
						    oOrigin.nTransf_amount = mobjValues.StringToType(Request.Form.GetValues("tcnTransfAmount").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
						    oOrigin.nTax_amount = mobjValues.StringToType(Request.Form.GetValues("tcnTax_Amount").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
						    oOrigin.nBalance = mobjValues.StringToType(Request.Form.GetValues("tcnBalance").GetValue(nLine), eFunctions.Values.eTypeData.etdDouble)
                                oOrigin.nUsercode = Session("nUserCode")
                                mobjCaseSeq.Origins.Add(oOrigin)
                            Next
                        End If
                    End If
				
                    mobjCaseSeq.nCoverCapital = mobjValues.StringToType(Request.Form("tcnCapitalAPV"), eFunctions.Values.eTypeData.etdDouble)
				mobjCaseSeq.nApv_balance_ac2052 = 0
				mobjCaseSeq.nApv_balance_bc2052 = 0
                    mobjCaseSeq.nTransf_amount = mobjValues.StringToType(Request.Form("tcnTransf_amount"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nApv_tax = mobjValues.StringToType(Request.Form("tcnApv_tax"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nApv_benef_balance = mobjValues.StringToType(Request.Form("tcnApv_benef_balance"), eFunctions.Values.eTypeData.etdDouble, True)
                    mobjCaseSeq.nOption = mobjValues.StringToType(Request.Form("cbeOption"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nAFP = mobjValues.StringToType(Request.Form("cbeAFP"), eFunctions.Values.eTypeData.etdDouble, True)
                    mobjCaseSeq.nCurrency = mobjValues.StringToType(Request.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
                    mobjCaseSeq.nStay_Bonus = mobjValues.StringToType(Request.Form("tcnStayBonus"), eFunctions.Values.eTypeData.etdDouble)
				
				'If Request.Form("hddAPV") = "1" Then
				'	nAmtInd = mobjValues.StringToType(Request.Form("tcnApv_benef_balance"), eFunctions.Values.eTypeData.etdDouble)
				'Else
				'	nAmtInd = mobjValues.StringToType(.Form("gmnIndemn"), eFunctions.Values.eTypeData.etdDouble)
				'End If
                    
                        nAmtInd = mobjValues.StringToType(.Form("gmnIndemn"), eFunctions.Values.eTypeData.etdDouble)
				
				
                    nGrothwRateI = mobjValues.StringToType(.Form("gmnGrowth_RateI"), eFunctions.Values.eTypeData.etdDouble)
                    nGrothwRateE = mobjValues.StringToType(.Form("gmnGrowth_RateE"), eFunctions.Values.eTypeData.etdDouble)
                    If nGrothwRateE <= 0 Then
                        nGrothwRateE = 0
                    End If
				
                    If nGrothwRateI <= 0 Then
                        nGrothwRateI = 0
                    End If
				
                nAmountAdjustCapital = mobjValues.StringToType(.Form("gmnCapital"), eFunctions.Values.eTypeData.etdDouble)
                nIndAdjustCapital = mobjValues.StringToType(.Form("chkEnabledCapital"), eFunctions.Values.eTypeData.etdInteger)

                If nAmountAdjustCapital <= 0 Then
                    nAmountAdjustCapital = 0
                End If

                If nIndAdjustCapital <= 0 Then
                    nIndAdjustCapital = 0
                End If
				
				lblnPost = mobjCaseSeq.insPostSI024("SI024", Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), mobjValues.StringToType(.Form("cbeClaimType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeIndemnity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdEnd_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnMonth_amo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnAdv_paymen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnSalvage"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmnCapital"), eFunctions.Values.eTypeData.etdDouble), nAmtInd, mobjValues.StringToType(CStr(Session("dDecladat")), eFunctions.Values.eTypeData.etdDate), Session("nMovement"), Session("dPosted"), Session("nTransaction"), .Form("tcnBranchT"), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(.Form("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble, True), nGrothwRateI, nGrothwRateE, nIndAdjustCapital, nAmountAdjustCapital)
                End With
			
			
                '+SI018: Datos de siniestro de automóvil
            Case "SI018"
                mobjCaseSeq = New eClaim.Claim_auto
                With Request
                    lblnPost = mobjCaseSeq.insPostSI018("SI018", 
                                        mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), 
                                        mobjValues.StringToType(.Form("tcnAutoQuant"), eFunctions.Values.eTypeData.etdDouble), 
										mobjValues.StringToType(.Form("cboBlame"), eFunctions.Values.eTypeData.etdDouble, true), 
                                        .Form("tctDriverCod"), 
																		 mobjValues.StringToType(.Form("cboInfraction"), eFunctions.Values.eTypeData.etdDouble, true), 
                                        .Form("chkSummary"), 
                                        .Form("chkDenunc"), 
                                        .Form("chkIntervEIR"), 
                                         mobjValues.StringToType(.Form("cbeWorksh"), eFunctions.Values.eTypeData.etdDouble), 
                                         mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), 
                                         mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), 
                                         mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), 
                                         .Form("tctDriver"), 
                                         .Form("tctDriver_Digit"), 
                                         .Form("tctNames"), 
                                         .Form("tctFatherLastName"), 
                                         .Form("tctMotherLastName"), 
                                         mobjValues.StringToType(.Form("tcnPartNumber"), eFunctions.Values.eTypeData.etdDouble), 
                                         .Form("tctTribunal"), 
                                         mobjValues.StringToType(.Form("dtcAccusationDate"), eFunctions.Values.eTypeData.etdDate), 
                                         .Form("tctPoliceStation"), 
                                         mobjValues.StringToType(.Form("tcnFolio"), eFunctions.Values.eTypeData.etdDouble), 
                                         mobjValues.StringToType(.Form("tcnParagraph"), eFunctions.Values.eTypeData.etdDouble), 
                                         .Form("tctPoliceStation2"), 
                                         mobjValues.StringToType(.Form("tcnEnduranceNumber"), eFunctions.Values.eTypeData.etdDouble), 
                                         mobjValues.StringToType(.Form("dtcEnduranceDate"), eFunctions.Values.eTypeData.etdDate), 
                                         .Form("chkAlcohol"), 
                                         mobjValues.StringToType(.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), 
                                         .Form("tctLicense"), 
                                         mobjValues.StringToType(.Form("tcdDriverDate"), eFunctions.Values.eTypeData.etdDate), 
                                         .Form("tctWitness"), 
                                         .Form("tctWitness_Digit"), 
                                         .Form("tctFatherLastNameWitness"), 
                                         .Form("tctMotherLastNameWitness"), 
                                         .Form("tctNamesWitness"), 
                                         mobjValues.StringToType(.Form("dtcBirthdayDate"), eFunctions.Values.eTypeData.etdDate))
                End With
			
                '+ Datos de Persona siniestrada
            Case "SI070"
                mobjClaimPeop = New eClaim.Claim_peop
                With Request
                    lblnPost = mobjClaimPeop.insPostSI070(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), .Form("gmtClient"), CInt(Session("nId")), mobjValues.StringToType(.Form("cboDamagesTy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), CInt(Session("nUserCode")))
                End With
                'UPGRADE_NOTE: Object mobjClaimPeop may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimPeop = Nothing
			
                '+SI020: Daños ocurridos en el siniestro
            Case "SI020"
                mobjCaseSeq = New eClaim.Claim_Dama
                With Request
                    If .QueryString("WindowType") <> "PopUp" Then
                        lblnPost = True
                    Else
                        lblnPost = mobjCaseSeq.insPostSI020("SI020", .QueryString("Action"), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valDamage_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeMag_dam"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
			
                '+SI028: Datos del siniestro de coberturas de salud
			
            Case "SI028"
                mobjCaseSeq = New eClaim.Claim_attm
                With Request
                    If .Form("hddHealth_system") = "" Then
                        lintHealth_System = .Form("optHealth_system")
                    Else
                        lintHealth_System = .Form("hddHealth_system")
                    End If
				
                    If .QueryString("WindowType") <> "PopUp" Then
					
                        If CStr(Session("LastName")) = "" Then
                            Session("LastName") = .Form("tctLastName")
                        ElseIf CStr(Session("LastName2")) = "" Then
                            Session("LastName2") = .Form("tctLastName2")
                        ElseIf CStr(Session("FirstName")) = "" Then
                            Session("FirstName") = .Form("tctFirstName")
                        ElseIf CStr(Session("LastNameProf")) = "" Then
                            Session("LastNameProf") = .Form("tctLastNameProf")
                        ElseIf CStr(Session("LastNameProf2")) = "" Then
                            Session("LastName2Prof") = .Form("tctLastName2Prof")
                        ElseIf CStr(Session("FirstNameProf")) = "" Then
                            Session("FirstNameProf") = .Form("tctFirstNameProf")
                        End If
					
                        lblnPost = mobjCaseSeq.insPostSI028(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), .Form("dtcClient"), Session("LastName"), Session("LastName2"), Session("FirstName"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), .Form("valIllness"), mobjValues.StringToType(.Form("tcdInitIlldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeService"), eFunctions.Values.eTypeData.etdDouble), .Form("dtcClientProf"), .Form("tctLastNameProf"), .Form("tctLastName2Prof"), .Form("tctFirstNameProf"), lintHealth_System, .Form("tctHealth_sys_other"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        Session("LastName") = .Form("hddLastName")
                        Session("LastName2") = .Form("hddLastName2")
                        Session("FirstName") = .Form("hddFirstName")
                        Session("FirstNameProf") = .Form("hddFirstNameProf")
                        Session("LastNameProf") = .Form("hddLastNameProf")
                        Session("LastName2Prof") = .Form("hddLastName2Prof")
					
                        lblnPost = mobjCaseSeq.insPostSI028Upd(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("Action"), .Form("tctDescript"), mobjValues.StringToType(.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeStatus"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
		Case "SI090"
                lblnPost = True

            Case "si700"
                Session.Remove("sSearch_EM")
                Session.Remove("sSearch_RM")
                Session.Remove("sSearch_EE")
                lblnPost = True
        End Select
        insPostSequence = lblnPost
	
        '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
        mobjNetFrameWork.FinishProcess("insPostSequence")
        '~End Header Block VisualTimer Utility
	
    End Function

    '% insCancel: Esta rutina es activada cuando el usuario cancela la transacción que este
    '%			  ejecutando.
    '--------------------------------------------------------------------------------------------
    Function insCancel() As Object
        '--------------------------------------------------------------------------------------------
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim_case As eClaim.Claim_case
	
        insFinish = True
	
        '+ Si no se han validado los campos de la página
        If Request.Form("sCodisplReload") = vbNullString Then
            mobjCaseSeq = New eClaim.Claim_cases
            '+ Se verifica que no existan ventanas requeridas en la ventana
            mstrErrors = mobjCaseSeq.insValSI099(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"))
        End If
	
        If mstrErrors > vbNullString Then
            insFinish = False
            Session("sErrorTable") = mstrErrors
            With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """,""CaseSeqErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write("</" & "Script>")
            End With
        Else
            If CBool(Session("bQuery")) = False Then
                lclsClaim_case = New eClaim.Claim_case
			
                With lclsClaim_case
                    If .Find(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type"))) Then
                        '+Se actualiza estado de secuencia a "con contenido"
                        If .sStaReserve <> "2" And .sStaReserve <> "13" Then
                            .nUsercode = Session("nUsercode")
                            insFinish = .UpdatesStareserve(.nClaim, .nDeman_type, .nCase_num, "2")
                        End If
                    End If
                End With
                'UPGRADE_NOTE: Object lclsClaim_case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsClaim_case = Nothing
            End If
        End If
    End Function

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("valcaseseq")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "valcaseseq"
    mstrCommand = "&sModule=Claim&sProject=CaseSeq&sCodisplReload=" & Request.QueryString("sCodispl") & "&sCodispl=" & Request.QueryString("sCodispl")

%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
	<%=mobjValues.StyleSheet()%>
	
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 8 $|$$Date: 25-03-13 7:33 $|$$Author: Jrengifo $"

        function NewLocation(Source, Codisp) {
            var lstrLocation = "";
            lstrLocation += Source.location;
            lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp;
            Source.location = lstrLocation;
        }
    </script>
    <%
        '+ Si no se han validado los campos de la página
        If Request.Form("sCodisplReload") = vbNullString Then
            mstrErrors = insvalSequence()
            Session("sErrorTable") = mstrErrors
        Else
            Session("sErrorTable") = vbNullString
        End If

If Request.QueryString("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdataFinish Then
            If mstrErrors > vbNullString Then
                With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """,""CaseSeqErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                    .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
			.Write("</SCRIPT>")
                End With
            Else
                If insPostSequence() Then
                    If Request.QueryString("WindowType") <> "PopUp" Then
                        '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                        '+ se mueve automaticamente a la siguiente página
                        If Request.Form("sCodisplReload") = vbNullString Then
                            If CStr(Session("nCase_num")) = "" Then
						Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "&sGoToNext=Yes" & "';</SCRIPT>")
                            Else
						Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/CaseSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "&sGoToNext=Yes" & "';</SCRIPT>")
                            End If
                        Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/CaseSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "&sGoToNext=Yes" & "';</SCRIPT>")
                        End If
				
                        If Request.QueryString("nZone") = 1 Then
					Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
                        End If
                    Else
                        '+ Se recarga la página que invocó la PopUp
                        Select Case Request.QueryString("sCodispl")
                            Case "SI020"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrCommand & "'</SCRIPT>")
                            Case "SI028"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & "&valIllness=" & Request.Form("valIllness") & "&valClinic=" & Request.Form("valClinic") & "&dtcClient=" & Request.Form("dtcClient") & "&cbeService=" & Request.Form("cbeService") & "&valProf=" & Request.Form("valProf") & "&dtcClientProf=" & Request.Form("dtcClientProf") & "&optHealth_system=" & Request.Form("hddHealth_system_2") & "&tctHealth_sys_other=" & Request.Form("hddHealth_sys_other") & "&tcdInitIlldate=" & Request.Form("tcdInitIlldate") & "&LastName=" & Request.Form("hddLastName") & "&tctLastNameProf=" & Request.Form("tctLastNameProf") & "&tctLastName2Prof=" & Request.Form("tctLastName2Prof") & "&tctFirstNameProf=" & Request.Form("tctFirstNameProf") & "&dtcClientProf_Digit=" & Request.Form("dtcClientProf_Digit") & "'</SCRIPT>")
						
                        End Select
                    End If
                End If
            End If
        Else
            If CBool(Session("bQuery")) = True Then
                With Response
                    .Write("<script>")
                    .Write("top.opener.document.location.href='/VTimeNet/Claim/ClaimSeq/SI016.aspx?sOnSeq=1&ReloadBySeqCase=True';")
                    .Write("top.close()")
                    .Write("</script>")
                End With
            Else
                '+ Se recarga la página principal de la secuencia		
                If insFinish() Then
                    With Response
                        .Write("<script>")
                        .Write("top.opener.document.location.href='/VTimeNet/Claim/ClaimSeq/SI016.aspx?sOnSeq=1&ReloadBySeqCase=True';")
				.Write("top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=SI016&sGoToNext=NO" & "';")
                        .Write("top.close()")
                        .Write("</script>")
                    End With
                End If
            End If
        End If
        'UPGRADE_NOTE: Object mobjCaseSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjCaseSeq = Nothing
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
    Call mobjNetFrameWork.FinishPage("valcaseseq")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
