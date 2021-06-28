<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
    Dim mobjValues As eFunctions.Values
    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String
    Dim lclsGeneral As eGeneral.GeneralFunction
    Dim mstrErrors As String
    Dim mobjCoverSeq As Object


    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String
        '--------------------------------------------------------------------------------------------
        Dim lclsSumcov_apl As eProduct.Sumcov_apl
        Select Case Request.QueryString.Item("sCodispl")
            '+ DP034: Información general de la cobertura
            Case "DP034"
                mobjCoverSeq = New eProduct.Gen_cover
                insvalSequence = mobjCoverSeq.insValDP034("DP034", mobjValues.StringToType(Request.Form.Item("valBillitem"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkCoverUse"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) ,    mobjValues.StringToType(request.Form.Item("tcnPrint_order"), eFunctions.Values.eTypeData.etdDouble) )
			
                '+ DP035: Primas
            Case "DP035"
                mobjCoverSeq = New eProduct.Precov_apl
                With Request
                    insvalSequence = mobjCoverSeq.insValDP035("DP035", mobjValues.StringToType(.Form.Item("valCover_in"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctroupremi"), mobjValues.StringToType(.Form.Item("tcnPremiFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiMax"), eFunctions.Values.eTypeData.etdDouble), InStr(1, .Form.Item("tcnSelected"), "1"), mobjValues.StringToType(.Form.Item("valCoverapl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkOwnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnApply_Perc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valid_table"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ DP035A: Franquicia/Deducible            
            Case "DP035A"
                mobjCoverSeq = New eProduct.Gen_cover
                With Request
                    insvalSequence = mobjCoverSeq.insValDP035A(mobjValues.StringToType(.Form.Item("cbeFranchiseTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFranchiseRou"), mobjValues.StringToType(.Form.Item("chkFranchiseReq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFranchiseApl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMediumValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseRateClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseFixClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMinClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMaxClaim"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFranchiseRouClaim"))
                End With
			
                '+ GE101: Cancelación del proceso
            Case "GE101"
                insvalSequence = vbNullString
			
                '+ Conceptos de Pagos/Indemnización
            Case "DP049"
                mobjCoverSeq = New eProduct.Cl_cov_bil
			
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                Dim nCount as Integer = 0
                    If Not IsNothing(Request.Form.Item("Sel")) Then
                    nCount = Request.Form.Item("Sel").length
                    End If
                    insvalSequence = mobjCoverSeq.insValDP049("DP049", nCount, "1")
                Else
                    insvalSequence = mobjCoverSeq.insValDP049("DP049", 1, Request.Form.Item("cboStatregt"))
                End If
			
                '+ DP50BP: Valores Garantizados
            Case "DP50BP"
                With Request
                    mobjCoverSeq = New eProduct.Life_cover
                    insvalSequence = mobjCoverSeq.InsValDP50BP("DP50BP", .Form.Item("tctMortacom"), .Form.Item("tctMortacof"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrintexp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCaintexp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrextexp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCaextexp"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ DP052: Condiciones del capital asegurado
            Case "DP052"
                With Request
                    mobjCoverSeq = New eProduct.Gen_cover
                    insvalSequence = mobjCoverSeq.insValDP052("DP052", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("sCacalili"), mobjValues.StringToType(.Form.Item("tcnCapitalmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalmax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalAddCh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalSubCh"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optCapital"), .Form.Item("optReinsu"), .Form.Item("optTax"))
                    mobjCoverSeq = Nothing
                End With
			
                '+ DP052A: Determinación del capital asegurado
            Case "DP052A"
                lclsSumcov_apl = New eProduct.Sumcov_apl
                mobjCoverSeq = New eProduct.Gen_cover
                With Request
                    If .QueryString.Item("Action") = "Update" Then
                        insvalSequence = lclsSumcov_apl.insValDP052A(mobjValues.StringToType(.Form.Item("tcnSumins_rat"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = mobjCoverSeq.insValDP052A("DP052A", .Form.Item("optCapital"), .Form.Item("tctCapitalRou"), mobjValues.StringToType(.Form.Item("tcnCapitalFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOtherCover"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("valOtherCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					
                    End If
                    lclsSumcov_apl = Nothing
                End With
			
                '+ DP018P: Información general de la cobertura
            Case "DP018P"
                With Request
                    mobjCoverSeq = New eProduct.Life_cover
                    insvalSequence = mobjCoverSeq.InsValDP018P(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valBillitem"), eFunctions.Values.eTypeData.etdDouble, True))
				
                End With
            Case "DP7002"                                    
                    insvalSequence = ""			                
            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        lblnPost = False
	
        Dim lclsSumcov_apl As eProduct.Sumcov_apl
        Select Case Request.QueryString.Item("sCodispl")
            '+ DP034: Información general de la cobertura
            Case "DP034"
                With Request
                    lblnPost = mobjCoverSeq.insPostDP034(.QueryString("nMainAction"), Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valBillitem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchGeneric"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchReinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchLedger"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchStatis"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPreSel"), .Form.Item("chkRequired"), .Form.Item("chkInd_Med_Exp"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeRetarif"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkReinOrigCond"), .Form.Item("tctCondSVS"), Request.Form.Item("chkCoverUse"), Request.Form.Item("chkPrint_capital") ,    mobjValues.StringToType(Request.Form.Item("tcnPrint_order"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ DP035: Primas
            Case "DP035"
                With Request
				
                    lblnPost = mobjCoverSeq.insPostDP035(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, mobjValues.StringToType(.Form.Item("valCover_in"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoupremi"), mobjValues.StringToType(.Form.Item("tcnPremiFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCoverapl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatepreadd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatepresub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCheprelev"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPremiumAdd"), Request.Form.Item("chkPremiumSub"), mobjValues.StringToType(.Form.Item("tcnApply_Perc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctsRou_verify"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkOwnCapital"), mobjValues.StringToType(.Form.Item("valid_table"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tcnSelected"), .Form.Item("tcnCapitalCode"), .Form.Item("tcnTarifSel"), .Form.Item("tcnTarifCapitalCode"))
                End With
			
                '+ DP035A: Franquicia/Deducible
            Case "DP035A"
                mobjCoverSeq = New eProduct.Gen_cover
                With Request
                    lblnPost = mobjCoverSeq.insPostDP035A(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkAutomaticRep"), .Form.Item("cbeFranchiseApl"), mobjValues.StringToType(.Form.Item("tcnFranchiseFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeFranchiseTyp"), mobjValues.StringToType(.Form.Item("tcnMediumValue"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFranchiseRou"), .Form.Item("tctReserveRou"), .Form.Item("chkFranchiseReq"), mobjValues.StringToType(.Form.Item("chkFranchiseAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkFranchiseSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseLev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseRateClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseFixClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMinClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMaxClaim"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFranchiseRouClaim"), .Form.Item("tctRASA_routine"), .Form.Item("chkIndManualDeductible"), mobjValues.StringToType(.Form.Item("tcnFrancDays"),Values.eTypeData.etdDouble ))
                End With
			
                '+ GE101: Cancelación del proceso
            Case "GE101"
                lblnPost = insCancel()
			
                '+ Conceptos de Pagos/Indemnización
            Case "DP049"
                lblnPost = True
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjCoverSeq = New eProduct.Cl_cov_bil
                    lblnPost = mobjCoverSeq.insPostDP049(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cboStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				
                End If
			
                '+ DP50BP: Valores Garantizados
            Case "DP50BP"
                With Request
                    lblnPost = mobjCoverSeq.InsPostDP50BP(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctMortacom"), .Form.Item("tctMortacof"), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPer_tabmor"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPrintexp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPrextexp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCaintexp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCaextexp"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRoureser"), .Form.Item("tctRousurre"), .Form.Item("tctRouClaTec"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctsRoutresrisk"))
                End With
			
                '+ DP052: Condiciones del capital asegurado
            Case "DP052"
                With Request
                    mobjCoverSeq = New eProduct.Gen_cover
                    lblnPost = mobjCoverSeq.insPostDP052(.QueryString("nMainAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkIndex"), mobjValues.StringToType(.Form.Item("tcnCapitalmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalmax"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkCapitalAddCh"), .Form.Item("chkCapitalSubCh"), mobjValues.StringToType(.Form.Item("tcnCapitalAddCh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalSubCh"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optCapital"), .Form.Item("optReinsu"), .Form.Item("optTax"), mobjValues.StringToType(.Form.Item("tcnCapitalLev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ DP052A: Determinación del capital asegurado
            Case "DP052A"
                lclsSumcov_apl = New eProduct.Sumcov_apl
                With Request
				
                    If .QueryString.Item("Action") = "Update" Then
                        mobjCoverSeq = New eProduct.Gen_cover
                        lblnPost = mobjCoverSeq.insPostDP052A(.QueryString("nMainAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "6", .Form.Item("tctCapitalRou"), mobjValues.StringToType(.Form.Item("tcnCapitalFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOtherCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOtherCover"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					
					
                        lblnPost = lclsSumcov_apl.insPostDP052A(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSumins_co"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnSumins_rat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
					
                        mobjCoverSeq = New eProduct.Gen_cover
                        lblnPost = mobjCoverSeq.insPostDP052A(.QueryString("nMainAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optCapital"), .Form.Item("tctCapitalRou"), mobjValues.StringToType(.Form.Item("tcnCapitalFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOtherCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOtherCover"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
                    End If
                    lclsSumcov_apl = Nothing
                End With
			
                '+ DP018P: Información general de la cobertura
            Case "DP018P"
                With Request
                    lblnPost = mobjCoverSeq.insPostDP018P(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valBillitem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRetarif"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkCover_use"), .Form.Item("chkControl"), .Form.Item("chkCalrein"), .Form.Item("chkDepend"), mobjValues.StringToType(.Form.Item("cbeBranch_led"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_est"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_gen"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("OptAddsuini"), .Form.Item("OptAddreini"), .Form.Item("OptAddtaxin"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), .Form.Item("chkSurv"), .Form.Item("chkReinOrigCond"), .Form.Item("tctCondSVS"))
                End With
            Case "DP7002"
                lblnPost = True 
        End Select
        insPostSequence = lblnPost
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Sub insFinish()
        '--------------------------------------------------------------------------------------------
        Response.Write("<script>insvalTabs(" & Request.QueryString.Item("nAction") & ")</" & "Script>")
    End Sub

    '% insCancel: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Private Function insCancel() As Boolean
        '--------------------------------------------------------------------------------------------
        insCancel = False
	
        If Request.Form.Item("optElim") = "Delete" Then
            mobjCoverSeq = New eProduct.Gen_cover
            With Request
                insCancel = mobjCoverSeq.insPostDP033Upd("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "", Session("sBrancht"))
            End With
        End If
	
        With Response
            .Write("<script>")
            .Write("var lstrHref = '/VTimeNet/Product/ProductSeq/DP033.aspx?sOnSeq=1&sCodispl=DP033&nMainAction=302&nModulec=" & Session("nModulec") & "';")
            .Write("opener.top.opener.top.frames['fraFolder'].location.href=lstrHref;")
            .Write("</" & "Script>")
        End With
	
        mobjCoverSeq = Nothing
    End Function

</script>
<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mstrCommand = "&sModule=Product&sProject=ProductSeq/CoverSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%=mobjValues.StyleSheet()%>
    <script>
//% insvalTabs: se verifica la existencia de ventanas requeridas en la secuencia
//-------------------------------------------------------------------------------------------
function insvalTabs(nAction){
//-------------------------------------------------------------------------------------------
<%lclsGeneral = New eGeneral.GeneralFunction
%>
	var lblnTabs = false;
	var lstrSRC = '';

	if (nAction==392){
		var Array = top.frames['fraSequence'].sequence;
		for(var lintIndex=0; lintIndex<Array.length; lintIndex++)
			if(Array[lintIndex].Require=="2" ||
			   Array[lintIndex].Require=="5")
				lblnTabs = true;
	}

	if(lblnTabs){
//+ Se muestra un mensaje de error al usuario
		alert("<%=lclsGeneral.insLoadMessage(3902)%>");
		
//+ Se habilitan las acciones del ToolBar al usuario
		for(var lintIndex=1; lintIndex<5; lintIndex++){
			lstrSRC = top.frames['fraHeader'].document.images[lintIndex].src
			lstrSRC = lstrSRC.replace("On.","Off.")
			top.frames['fraHeader'].document.images[lintIndex].src = lstrSRC
			top.frames['fraHeader'].document.images[lintIndex].disabled = false;
		}
	}
	else
		insDefValues("Finish");
	
<%
lclsGeneral = Nothing%>
}
    </script>
</head>
<body>
    <form id="FORM1" name="FORM1">
    <script>

        //% NewLocation: se recalcula el URL de la página
        //-------------------------------------------------------------------------------------------
        function NewLocation(Source, Codisp) {
            //-------------------------------------------------------------------------------------------
            var lstrLocation = "";
            lstrLocation += Source.location;
            lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp
            Source.location = lstrLocation
        }
    </script>
    <%
        '+ Si no se han validado los campos de la página
        If Request.Form.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insvalSequence()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
        Else
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If

        If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
            If mstrErrors > vbNullString Then
                With Response
                    .Write("<script type='text/javascript'>")
                    '				.Write "ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString) & """, ""CoverSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';"
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);")
                    .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                    .Write("</script>")
                End With
            Else
                If insPostSequence() Then
                    If Request.QueryString.Item("WindowType") <> "PopUp" Then
                        '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                        '+ se mueve automaticamente a la siguiente página
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<script>top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/CoverSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</script>")
                        Else
                            Response.Write("<script>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</script>")
                        End If
                    Else
                        '+ Se recarga la página que invocó la PopUp
                        Select Case Request.QueryString.Item("sCodispl")
                            Case "GE101"
                                Response.Write("<script>top.opener.top.close();</script>")
                            Case "DP052A", "DP049"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</script>")
                        End Select
                    End If
                End If
            End If
        Else
            '+ Se recarga la página principal de la secuencia			
            Call insFinish()
        End If
        mobjCoverSeq = Nothing
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
