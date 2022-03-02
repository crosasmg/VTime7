<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '- Variable para el manejo de los errores de la página, devueltos por insvalCoReinsuranTra
    Dim mstrErrors As String

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjCoReinsuranTra As Object

    '- Variable auxiliar para pase de valores del encabezado al folder
    Dim mstrString As String
    Dim mstrQueryString As String

    '- Variables auxiliares

    Dim lintReinsuran As Object
    Dim lintNumber As Object


    '% insvalCoReinsuranTra: Se realizan las validaciones masivas de las páginas
    '--------------------------------------------------------------------------------------------
    Function insvalCoReinsuranTra() As String
        Dim lintCoverGen As Object
        '--------------------------------------------------------------------------------------------
        Select Case Request.QueryString.Item("sCodispl")

            '+ CR006_k: Ajuste de cuentas técnicas
            Case "CR006_k", "CR006_K"
                With Request
                    mobjCoReinsuranTra = New eCoReinsuran.Cuentecn

                    If mobjValues.StringToType(.Form.Item("optReinsurance"), eFunctions.Values.eTypeData.etdDouble) = 3 Then
                        ' Contrato no proporcional
                        lintReinsuran = 2
                    Else
                        'Contrato proporcional-facultativo
                        lintReinsuran = 1
                    End If

                    If mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                        lintNumber = mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble)
                    Else
                        If mobjValues.StringToType(.Form.Item("optReinsurance"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                            lintNumber = eRemoteDB.Constants.intNull
                        Else
                            lintNumber = 0
                        End If
                    End If
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
                    insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR006_k("CR006_k", mobjValues.StringToType(lintReinsuran, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintNumber, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePerType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussiType"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIdConsec"), eFunctions.Values.eTypeData.etdDouble))
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
                End With

            '+ CR006H: A favor del asegurador
            Case "CR006H"
                insvalCoReinsuranTra = vbNullString

            '+ CR006D: A favor del reasegurador
            Case "CR006D"
                insvalCoReinsuranTra = vbNullString

            '+ Tasas/Primas de un reasuguro de un contrato I
            Case "CR726"
                With Request
                    mobjCoReinsuranTra = New eCoReinsuran.Contr_rate_I
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR726_k("CR726", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 2)
                        Else
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR726("CR726", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    Else
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR726_k("CR726", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 1)
                        End If
                    End If
                    mobjCoReinsuranTra = Nothing
                End With

            '+ Tasas/Primas de un reasuguro de un contrato II			
            Case "CR765"
                With Request
                    mobjCoReinsuranTra = New eCoReinsuran.Contr_rate_II
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR765_k("CR765", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksmoking"), .Form.Item("optperiodpol"), mobjValues.StringToType(.Form.Item("cbetyperisk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 2)
                        Else
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR765("CR765", .QueryString("Action"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("sPeriodpol"), Session("nTyperisk"), mobjValues.StringToType(Session("nCapini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    Else
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR765_k("CR765", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksmoking"), .Form.Item("optperiodpol"), mobjValues.StringToType(.Form.Item("cbetyperisk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 1)
                        End If
                    End If
                End With

            '+ Tasas/Primas de un reasuguro de un contrato III		
            Case "CR766"
                With Request
                    mobjCoReinsuranTra = New eCoReinsuran.contr_rate_III
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR766_k("CR766", mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDeductible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQfamily"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                            insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR766("CR766", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDeductible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQfamily"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            Case "CR781"

                With Request
                    'UPGRADE_NOTE: The 'eCoReinsuran.Tar_Hospitaliz' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjCoReinsuranTra = Server.CreateObject("eCoReinsuran.Tar_Hospitaliz")
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                        insvalCoReinsuranTra = mobjCoReinsuranTra.insValCR781("CR781", .QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPrem_Aseg"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPrem_Adic"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
            Case "CR782"
                Dim lclsProfit As New eCoReinsuran.Contr_Rate_Profit
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insvalCoReinsuranTra = lclsProfit.insValCR782_k("CR782", mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        With Request
                            insvalCoReinsuranTra = lclsProfit.insValCR782("CR782", mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnIni_Policy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnEnd_Policy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                          mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          .QueryString.Item("Action"))
                        End With
                    End If
                End If
                lclsProfit = Nothing
            Case "CR783"
                Dim lclsProfit As New eCoReinsuran.Commiss_contr
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insvalCoReinsuranTra = lclsProfit.InsValCR783_K("CR783", mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        With Request
                            insvalCoReinsuranTra = lclsProfit.InsValCR783("CR783", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeTypeVal"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnFromValue"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                          mobjValues.StringToType(.Form.Item("tcnToValue"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnAmountFix"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeTypeCom"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          .QueryString.Item("Action"))
                        End With
                    End If
                End If
                lclsProfit = Nothing
            Case Else
                insvalCoReinsuranTra = "insvalCoReinsuranTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostCoReinsuranTra: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostCoReinsuranTra() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lintZero As Object
        lblnPost = False

        Select Case Request.QueryString.Item("sCodispl")

            '+ CR006_k: Ajuste de cuentas técnicas
            Case "CR006_k", "CR006_K"
                With Request
                    If Request.Form.Item("sCodisplReload") <> vbNullString Then
                        mobjCoReinsuranTra = New eCoReinsuran.Cuentecn
                    End If

                    If mobjValues.StringToType(.Form.Item("optReinsurance"), eFunctions.Values.eTypeData.etdDouble) = 3 Then
                        ' Contrato no proporcional
                        lintReinsuran = 2
                    Else
                        'Contrato proporcional-facultativo
                        lintReinsuran = 1
                    End If

                    If mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                        lintNumber = mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble)
                    Else
                        If mobjValues.StringToType(.Form.Item("optReinsurance"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                            lintNumber = eRemoteDB.Constants.intNull
                        Else
                            lintNumber = 0
                        End If
                    End If
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
                    mstrQueryString = "&nReinsuran=" & lintReinsuran & "&nNumber=" & lintNumber & "&nBranch=" & mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble) & "&nType=" & mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble) & "&nYearSer=" & mobjValues.StringToType(.Form.Item("tcnYearSer"), eFunctions.Values.eTypeData.etdDouble) & "&nCompany=" & mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble) & "&nPerType=" & mobjValues.StringToType(.Form.Item("cbePerType"), eFunctions.Values.eTypeData.etdDouble) & "&nPerNum=" & mobjValues.StringToType(.Form.Item("tcnPerNum"), eFunctions.Values.eTypeData.etdDouble) & "&sBussiType=" & .Form.Item("cbeBussiType") & "&nCurrency=" & mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&nIdConsec=" & mobjValues.StringToType(.Form.Item("tcnIdConsec"), eFunctions.Values.eTypeData.etdDouble)

                    lblnPost = mobjCoReinsuranTra.insPostCR006_k("CR006_k", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintNumber, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYearSer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePerType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBussiType"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIdConsec"), eFunctions.Values.eTypeData.etdDouble))
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
                End With

                If lblnPost Then

                    '+ Dependiendo de los totales generados, el sistema va a llamar a favor del asegurado o a favor del reasegurador.
                    If mobjCoReinsuranTra.nSal_f_rein > mobjCoReinsuranTra.nSal_f_comp Then

                        '+ A favor del reasegurador.
                        mstrString = "CR006D"
                    ElseIf mobjCoReinsuranTra.nSal_f_comp >= mobjCoReinsuranTra.nSal_f_rein Then

                        '+ A favor del asegurador.
                        mstrString = "CR006H"
                    End If
                End If

            '+ CR006D: A favor del reasegurador
            Case "CR006D"
                With Request
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
                    mobjCoReinsuranTra = New eCoReinsuran.Cuentecn
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
                    lblnPost = mobjCoReinsuranTra.insPostCR006D("CR006D", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("nReinsurance"), Request.QueryString.Item("nNumber"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nType"), Request.QueryString.Item("nYearSer"), Request.QueryString.Item("nCompany"), Request.QueryString.Item("nPerType"), Request.QueryString.Item("nPerNum"), Request.QueryString.Item("sBussiType"), Request.QueryString.Item("nCurrency"), mobjValues.StringToType(.Form.Item("tcnPremCed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartbenef"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDevResPre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDevResCla"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterSin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnECarPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnECarSin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddPayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdConsec"), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"))
                    'lblnPost = False
                End With

            '+ CR006H: A favor del asegurador
            Case "CR006H"
                With Request
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
                    mobjCoReinsuranTra = New eCoReinsuran.Cuentecn
                    'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
                    lblnPost = mobjCoReinsuranTra.insPostCR006H("CR006H", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("nReinsurance"), Request.QueryString.Item("nNumber"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nType"), Request.QueryString.Item("nYearSer"), Request.QueryString.Item("nCompany"), Request.QueryString.Item("nPerType"), Request.QueryString.Item("nPerNum"), Request.QueryString.Item("sBussiType"), Request.QueryString.Item("nCurrency"), mobjValues.StringToType(.Form.Item("tcnRetResPre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnResSinPen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRCarPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRCarSin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGastoReas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnImpuesto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaimCed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddPayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdConsec"), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"))
                End With

            '+ CR726: Tasas primas de reaseguro de un contrato I
            Case "CR726"
                With Request
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            mstrString = "&nBranch_rei=" & .Form.Item("cbenBranch_rei") & "&nNumber=" & .Form.Item("tcnNumber") & "&nType=" & .Form.Item("cbeType") & "&nCovergen=" & .Form.Item("valCovergen") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
                            Session("nBranch_rei") = .Form.Item("cbenBranch_rei")
                            Session("nNumber") = .Form.Item("tcnNumber")
                            Session("nType") = .Form.Item("cbeType")

                            If CStr(Session("nPriorCoverGen")) = vbNullString Or Session("nPriorCoverGen") <= 0 Then
                                Session("nPriorCoverGen") = .Form.Item("valCovergen")
                            End If
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            lblnPost = True
                        Else
                            If Request.QueryString.Item("WindowType") = "PopUp" Then
                                mobjCoReinsuranTra = New eCoReinsuran.Contr_rate_I
                                lblnPost = mobjCoReinsuranTra.insPostCR726("CR726", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                mstrString = "&nBranch_rei=" & .QueryString.Item("nBranch_rei") & "&nNumber=" & .QueryString.Item("nNumber") & "&nType=" & .QueryString.Item("nType") & "&nCovergen=" & .QueryString.Item("nCovergen") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
                                mobjCoReinsuranTra = Nothing
                            End If
                        End If
                    Else
                        If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                            mobjCoReinsuranTra = New eCoReinsuran.Contr_rate_I

                            Session("nBranch_reiNEW") = .Form.Item("cbenBranch_rei")
                            Session("nNumberNEW") = .Form.Item("tcnNumber")
                            Session("nTypeNEW") = .Form.Item("cbeType")
                            If CStr(Session("nCoverGenNEW")) = vbNullString Or Session("nCoverGenNEW") <= 0 Then
                                Session("nCoverGenNEW") = .Form.Item("valCovergen")
                            End If
                            Session("dEffecdateNEW") = .Form.Item("tcdEffecdate")

                            lblnPost = mobjCoReinsuranTra.InsDupContr_rate_I(mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPriorCoverGen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nNumberNEW"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_reiNEW"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTypeNEW"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCoverGenNEW"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdateNEW"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            mobjCoReinsuranTra = Nothing
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '+ CR765: Tasas primas de reaseguro de un contrato II
            Case "CR765"
                With Request
                    mobjCoReinsuranTra = New eCoReinsuran.Contr_rate_II
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        '+ Se toma el ultimo valor consultado a ser duplicado 
                        If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
                            Session("nLastNumber") = .Form.Item("tcnNumber")
                            Session("nLastBranch") = .Form.Item("cbenBranch_rei")
                            Session("nLastType") = .Form.Item("cbeType")
                            Session("nLastCoverGen") = .Form.Item("valCovergen")

                            If IsNothing(.Form.Item("chksmoking")) Or .Form.Item("chksmoking") = "2" Then
                                Session("sLastSmoking") = "2"
                            Else
                                Session("sLastSmoking") = .Form.Item("chksmoking")
                            End If

                            Session("sLastPeriodPol") = .Form.Item("optperiodpol")
                            Session("nLastTypeRisk") = mobjValues.StringToType(.Form.Item("cbetyperisk"), eFunctions.Values.eTypeData.etdDouble)
                            Session("nLastCapIni") = .Form.Item("tcnCap_ini")
                            Session("dLastEffecdate") = .Form.Item("tcdEffecdate")
                        End If
                        '+ Se toma el valor que se va al que se va a duplicar.
                        Session("nNumber") = .Form.Item("tcnNumber")
                        Session("nBranch") = .Form.Item("cbenBranch_rei")
                        Session("nType") = .Form.Item("cbeType")
                        Session("nCoverGen") = .Form.Item("valCovergen")

                        If IsNothing(.Form.Item("chksmoking")) Or .Form.Item("chksmoking") = "2" Then
                            Session("sSmoking") = "2"
                        Else
                            Session("sSmoking") = .Form.Item("chksmoking")
                        End If

                        Session("sPeriodPol") = .Form.Item("optperiodpol")
                        Session("nTypeRisk") = mobjValues.StringToType(.Form.Item("cbetyperisk"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nCapIni") = .Form.Item("tcnCap_ini")
                        Session("nCapEnd") = .Form.Item("tcnCap_end")
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")

                        lblnPost = True
                        If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                            lblnPost = mobjCoReinsuranTra.InsDupContr_rate_II(mobjValues.StringToType(Session("nLastNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastCoverGen"), eFunctions.Values.eTypeData.etdDouble), Session("sLastSmoking"), Session("sLastPeriodPol"), Session("nLastTypeRisk"), mobjValues.StringToType(Session("nLastCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastCapEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dLastEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCoverGen"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("sPeriodPol"), Session("nTypeRisk"), mobjValues.StringToType(Session("nCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCapEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            Session("nAge_reinsu") = .Form.Item("tcnAge_reinsu")

                            lblnPost = mobjCoReinsuranTra.insPostCR765(.QueryString("Action"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCoverGen"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("sPeriodPol"), Session("nTypeRisk"), mobjValues.StringToType(Session("nCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCapEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With

            '+ CR766: Tasas primas de reaseguro de un contrato III
            Case "CR766"
                lblnPost = True
                mobjCoReinsuranTra = New eCoReinsuran.contr_rate_III
                With Request
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            '+ Se toma el ultimo valor consultado a ser duplicado 
                            Session("tcnNumber_new") = .Form.Item("tcnNumber")
                            Session("cbenBranch_rei_new") = .Form.Item("cbenBranch_rei")
                            Session("cbeType_new") = .Form.Item("cbeType")
                            Session("valCovergen_new") = .Form.Item("valCovergen")
                            Session("tcdEffecdate_new") = .Form.Item("tcdEffecdate")
                            Session("tcnDeductible_new") = .Form.Item("tcnDeductible")
                            Session("tcnCapital_new") = .Form.Item("tcnCapital")
                            Session("tcnQfamily_new") = .Form.Item("tcnQfamily")

                            '+ Se toma el valor que se va al que se va a duplicar.

                        Else
                            If Request.QueryString.Item("WindowType") = "PopUp" Then
                                lblnPost = mobjCoReinsuranTra.insPostCR766(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDeductible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQfamily"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                mstrString = "&nBranch_rei=" & .QueryString.Item("nBranch_rei") & "&nNumber=" & .QueryString.Item("nNumber") & "&nType=" & .QueryString.Item("nType") & "&nCovergen=" & .QueryString.Item("nCovergen") & "&nDeductible=" & .QueryString.Item("nDeductible") & "&nQfamily=" & .QueryString.Item("nQfamily") & "&nCapital=" & .QueryString.Item("nCapital") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
                            End If
                        End If
                    Else
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            lblnPost = mobjCoReinsuranTra.InsDupContr_rate_III(mobjValues.StringToType(Session("tcnNumber_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("cbenBranch_rei_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("cbeType_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("valCovergen_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnDeductible_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnQfamily_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnCapital_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcdEffecdate_new"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDeductible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQfamily"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            '+ CR781: Tasas primas de reaseguro de Hospitalizacion
            Case "CR781"

                'UPGRADE_NOTE: The 'eCoReinsuran.tar_hospitaliz' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjCoReinsuranTra = Server.CreateObject("eCoReinsuran.tar_hospitaliz")

                If Request.Form.Item("tcnPrem_Adic") = CStr(eRemoteDB.Constants.intNull) Or IsNothing(Request.Form.Item("tcnPrem_Adic")) Then
                    lintZero = 0
                Else
                    lintZero = Request.Form.Item("tcnPrem_Adic")
                End If

                With Request
                    lblnPost = mobjCoReinsuranTra.insPostCR781("CR781", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPrem_Aseg"), eFunctions.Values.eTypeData.etdDouble), lintZero, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
            '+ CR782:
            Case "CR782"
                lblnPost = True
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    mstrString = "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
                    Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                    lblnPost = True
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        Dim lclsProfit As New eCoReinsuran.Contr_Rate_Profit
                        'mobjCoReinsuranTra = Server.CreateObject("eCoReinsuran.Contr_Rate_Profit")
                        With Request
                            lblnPost = lclsProfit.InspostCR782Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnIni_Policy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnEnd_Policy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                          mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End With
                        lclsProfit = Nothing
                    End If
                End If
            Case "CR783"
                lblnPost = True
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    mstrString = "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
                    Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                    lblnPost = True
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        Dim lclsCommiss_contr As New eCoReinsuran.Commiss_contr
                        'mobjCoReinsuranTra = Server.CreateObject("eCoReinsuran.Contr_Rate_Profit")
                        With Request
                            lblnPost = lclsCommiss_contr.InspostCR783Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeTypeVal"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnFromValue"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                          mobjValues.StringToType(.Form.Item("tcnToValue"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnAmountFix"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), _
                                                                          .Form.Item("cbeTypeCom"), _
                                                                          mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End With
                        lclsCommiss_contr = Nothing
                    End If
                End If
        End Select
        insPostCoReinsuranTra = lblnPost
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        insFinish = True
    End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=CoReinsuran&sProject=CoReinsuranTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")


%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=form1 NAME=form1>
<SCRIPT>

//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//------------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<%
    If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then

        '+ Si no se han validado los campos de la página
        If Request.Form.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insvalCoReinsuranTra
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
        Else
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoReinsuranTraError"",660,330);")
                .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If insPostCoReinsuranTra Then
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                        Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
                    Else
                        If Request.QueryString.Item("sCodispl") = "CR006_k" Or Request.QueryString.Item("sCodispl") = "CR006_K" Then
                            If mstrString > vbNullString Then
                                If Request.QueryString.Item("sCodisplReload") = vbNullString Then
                                    Response.Write("<SCRIPT>top.fraFolder.document.location=""/VTimeNet/CoReinsuran/CoReinsuranTra/" & mstrString & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>window.close();opener.top.frames['fraFolder'].document.location=""/VTimeNet/CoReinsuran/CoReinsuranTra/" & mstrString & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                End If
                            Else
                                Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            End If
                        Else
                            If Request.QueryString.Item("sCodispl") = "CR781" Then
                                Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
                            End If
                        End If
                    End If
                Else
                    '+ Se recarga la página que invocó la PopUp
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "CR726"
                            Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
                        Case "CR765"
                            Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
                        Case "CR766"
                            Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
                        Case "CR782"
                            Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
                        Case "CR783"
                            Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
                    End Select
                End If
            Else
                If Request.QueryString.Item("sCodispl") = "CR765" Then
                    Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
                End If

                If Request.QueryString.Item("sCodispl") = "CR006_k" Or Request.QueryString.Item("sCodispl") = "CR006_K" Then
                    Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                End If
            End If
        End If
    Else
        '+ Se recarga la página principal de la secuencia
        'LAMC se comenta para incluir en el case
        'If insFinish() Then
        '    Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
        'End If

        'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
        Select Case Request.QueryString.Item("sCodispl")
            Case "CR006D", "CR006H"
                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    If insPostCoReinsuranTra() Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        Response.Write("<script>alert('Se realizó la operación correctamente.');</script>")
                    Else
                        'Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        'Response.Write("<SCRIPT>window.close();opener.top.frames['fraFolder'].document.location=""/VTimeNet/CoReinsuran/CoReinsuranTra/" & Request.QueryString.Item("sCodispl") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                        Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
                        Response.Write("<script>alert('No se pudo realizar la operación.');</script>")
                    End If
                End If
            Case Else
                If insFinish() Then
                    Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
                End If
        End Select
        'PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN

    End If
    mobjCoReinsuranTra = Nothing
    mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






