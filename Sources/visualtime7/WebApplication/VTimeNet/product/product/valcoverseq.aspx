<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values

    Dim mclsTab_LifCov As eProduct.Tab_lifcov
    Dim mclsTab_GenCov As eProduct.Tab_gencov

    Dim mstrErrors As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String


    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String
        '--------------------------------------------------------------------------------------------
        Dim lstrInsurIni As String

        insvalSequence = vbNullString
        lstrInsurIni = vbNullString

        With Request
            Select Case .QueryString.Item("sCodispl")
            '+GE101: Cancelación del proceso
                Case "GE101"
                    insvalSequence = vbNullString

                '+ DP018G_K: Datos de referencia de la cobertura
                Case "DP018G_K"
                    insvalSequence = mclsTab_LifCov.InsValDP018G_K("DP018", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True))

                '+ DP018G: Información general de la cobertura
                Case "DP018G"
                    If .Form.Item("cbeInsurini") <> "0" Then
                        lstrInsurIni = .Form.Item("cbeInsurini")
                    End If
                    insvalSequence = mclsTab_LifCov.insValDP018G("DP018G", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), lstrInsurIni, .Form.Item("cbeStatregt"), mobjValues.StringToType(.Form.Item("cbeBranch_est"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_gen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_led"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctsCondSVS"), .Form.Item("tctsProvider"))

                '+ DP019G: Capital, Prima y siniestros
                Case "DP019G"
                    insvalSequence = mclsTab_LifCov.InsValDP019G("DP019G", CInt(.QueryString.Item("nMainAction")), .Form.Item("tctRutin"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("OptCapital"), mobjValues.StringToType(.Form.Item("tcnPrice"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDeath"), .Form.Item("tctTriIndem"), .Form.Item("tctInability"), .Form.Item("tctDoubleIndem"), .Form.Item("tctSurvival"), .Form.Item("tctInvalid"), .Form.Item("tctClillness"))


                '+ DP050G: Duración y condiciones de renovación
                Case "DP050G"
                    insvalSequence = mclsTab_LifCov.InsValDP050G("DP050G", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optSecure"), mobjValues.StringToType(.Form.Item("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optPay"), mobjValues.StringToType(.Form.Item("tcnQuantityPays"), eFunctions.Values.eTypeData.etdDouble, True))


                '+ DP029_K: Solicitud de cobertura a procesar
                Case "DP029_K"
                    insvalSequence = mclsTab_GenCov.InsValDP029_K("DP029_K", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True))
                '+ DP029: Información general de la cobertura (Ramos generales)
                Case "DP029"
                    insvalSequence = mclsTab_GenCov.insValDP029("DP029", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_led"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_est"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_gen"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctsCondSVS"), .Form.Item("chksInforProv"), .Form.Item("tctsProvider"), .Form.Item("tctsProvider_Digit"))

                '+ DP030A: Condiciones del capital (Coberturas Genéricas)
                Case "DP030A"
                    insvalSequence = mclsTab_GenCov.insValDP030A("DP030A", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optCapital"), mobjValues.StringToType(.Form.Item("tcnCapitalFix"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCapitalRou"))

                '+ DP030B: Condiciones de la prima (Coberturas Genéricas)
                Case "DP030B"
                    insvalSequence = mclsTab_GenCov.insValDP030B("DP030B", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCoverIn"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctPremiumRou"), mobjValues.StringToType(.Form.Item("tcnPremiumFix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseFix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optType"), .Form.Item("tctFranchiseRou"), mobjValues.StringToType(.Form.Item("tcnFranchiseRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseRateClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseFixClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMinClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMaxClaim"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctFranchiseRouClaim"), .Form.Item("optAplied"))
                Case Else
                    insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
            End Select
        End With
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lstrInsurIni As String

        lstrInsurIni = vbNullString
        lblnPost = True

        With Request
            Select Case Request.QueryString.Item("sCodispl")

            '+GE101: Cancelación del proceso
                Case "GE101"
                    lblnPost = insCancel

                '+ DP018G_K: Datos de referencia de la cobertura
                Case "DP018G_K"
                    lblnPost = mclsTab_LifCov.InsPostDP018G_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), 0)
                    Session("nCover") = .Form.Item("valCover")
                    '+ Cobertura de vida
                    Session("nTypeCover") = "1"

                '+ DP018G: Información general de la cobertura
                Case "DP018G"
                    If .Form.Item("cbeInsurini") <> "0" Then
                        lstrInsurIni = .Form.Item("cbeInsurini")
                    End If
                    lblnPost = mclsTab_LifCov.InsPostDP018G(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Session("nCover"), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRescue"), .Form.Item("tctReser"), lstrInsurIni, .Form.Item("optClas"), mobjValues.StringToType(.Form.Item("cbeBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch_gen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctsCondSVS"), .Form.Item("chksInforProv"), .Form.Item("tctsProvider"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeClaimType"), eFunctions.Values.eTypeData.etdDouble, True))

                '+ DP019G: Capital, Prima y siniestros
                Case "DP019G"
                    lblnPost = mclsTab_LifCov.InsPostDP019G(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDeath"), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDoubleIndem"), .Form.Item("tctInability"), .Form.Item("tctInvalid"), .Form.Item("tctRutin"), .Form.Item("tctSurvival"), .Form.Item("tctTriIndem"), mobjValues.StringToType(.Form.Item("tcnPrice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("OptCapital"), .Form.Item("ChkCapital"), .Form.Item("ChkPremium"), .Form.Item("tctClillness"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                '+ DP050G: Duración y condiciones de renovación
                Case "DP050G"
                    lblnPost = mclsTab_LifCov.InsPostDP050G(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRouPremium"), .Form.Item("tctRoutine"), .Form.Item("chkAgeReach"), .Form.Item("chkRenew"), .Form.Item("chkRevalue"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optSecure"), mobjValues.StringToType(.Form.Item("tcnQuantityPays"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optPay"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                '+ DP029_K: Solicitud de cobertura a procesar
                Case "DP029_K"
                    lblnPost = mclsTab_GenCov.InsPostDP029_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), 0)
                    '+ Cobertura de ramos generales
                    Session("nCover") = .Form.Item("valCover")
                    Session("nTypeCover") = "2"
                    Session("sOriginalForm") = "DP029"
                    Session("sLinkSpecial") = "1"
                    Session("sLinkControl") = Request.Form.Item("tctReserveRou")

                '+ DP029: Información general de la cobertura (Ramos generales)
                Case "DP029"
                    lblnPost = mclsTab_GenCov.insPostDP029(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShortDes"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkAutomaticRep"), mobjValues.StringToType(.Form.Item("tcnMediumValue"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctReserveRou"), mobjValues.StringToType(.Form.Item("cbeBranch_led"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_est"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch_gen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctsCondSVS"), .Form.Item("chksInforProv"), .Form.Item("tctsProvider"), .Form.Item("chkRisk"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                '+ DP030A: Condiciones del capital (Coberturas Genéricas)
                Case "DP030A"
                    lblnPost = mclsTab_GenCov.InsPostDP030A(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkIndex"), .Form.Item("optCapital"), mobjValues.StringToType(.Form.Item("tcnCapitalFix"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCapitalRou"), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                '+ DP030B: Condiciones de la prima (Coberturas Genéricas)
                Case "DP030B"
                    lblnPost = mclsTab_GenCov.InsPostDP030B(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPremiumRou"), mobjValues.StringToType(.Form.Item("valCoverIn"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumFix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumMax"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optType"), .Form.Item("optAplied"), .Form.Item("tctFranchiseRou"), mobjValues.StringToType(.Form.Item("tcnFranchiseRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseFix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseRateClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseFixClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMinClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFranchiseMaxClaim"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctFranchiseRouClaim"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
            End Select
        End With
        insPostSequence = lblnPost
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        insFinish = True
        If CStr(Session("nTypeCover")) = "1" Then
            insFinish = InsFinishTab_LifCov
        Else
            If CStr(Session("nTypeCover")) = "2" Then
                insFinish = InsFinishTab_GenCov
            End If
        End If
    End Function

    '% insCancel: Función que se ejecuta al cancelar la secuencia
    '--------------------------------------------------------------------------------------------
    Private Function insCancel() As Boolean
        '--------------------------------------------------------------------------------------------
        insCancel = False
        If Request.Form.Item("optElim") = "Delete" Then
            If CStr(Session("nTypeCover")) = "1" Then
                mclsTab_LifCov.Delete(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
            Else
                If CStr(Session("nTypeCover")) = "2" Then
                    mclsTab_GenCov.Delete(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
                End If
            End If
        End If
        Response.Write("<SCRIPT>opener.top.location.reload();window.close()</" & "Script>")
    End Function

    '% InsFinishTab_LifCov: Finalizar en caso de coberturas genericas de vida
    '--------------------------------------------------------------------------------------------
    Private Function InsFinishTab_LifCov() As Boolean
        '--------------------------------------------------------------------------------------------

        Dim lclsErrors As eGeneralForm.GeneralForm
        Dim llngAction As String
        Dim lstrError As String = String.Empty
        InsFinishTab_LifCov = True

        llngAction = Request.QueryString.Item("nMainAction")

        If llngAction.Length > 3 Then
            llngAction = llngAction.Trim.Substring(0, 3)
        End If

        Select Case llngAction
            Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionUpdate
                If mclsTab_GenCov.ValContent("DP018", mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble)) Then
                    If mclsTab_GenCov.WithInformation.Trim <> "DP018G  DP019G  DP050G" Then
                        lclsErrors = New eGeneralForm.GeneralForm
                        Session("sErrorTable") = lclsErrors.insValGE101("ClientSeq")
                        Session("sForm") = Request.Form.ToString
                        With Response
                            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & "&sValPage=CoverSeq" & """, ""CoverSeqError"",660,330);")
                            .Write("self.history.go(-1)")
                            .Write("</" & "Script>")
                        End With
                        InsFinishTab_LifCov = False
                        lclsErrors = Nothing
                    Else
                        mclsTab_LifCov.UpdateStatus(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), "1")
                    End If
                End If

            Case eFunctions.Menues.TypeActions.clngActioncut
                mclsTab_LifCov.Delete(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))

            Case eFunctions.Menues.TypeActions.clngActionDuplicate
                If Request.QueryString.Item("sDup") = "1" Then
                    lstrError = mclsTab_LifCov.InsValDP018G_K("DP018G", eFunctions.Menues.TypeActions.clngActionDuplicate, mobjValues.StringToType(Request.Form.Item("tcnCoverNew"), eFunctions.Values.eTypeData.etdDouble, True), True)
                    If lstrError <> vbNullString Then
                        lclsErrors = New eGeneralForm.GeneralForm
                        Session("sErrorTable") = lstrError
                        Session("sForm") = Request.Form.ToString
                        With Response
                            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);")
                            .Write("self.history.go(-1)")
                            .Write("</" & "Script>")
                        End With
                        InsFinishTab_LifCov = False
                        lclsErrors = Nothing
                    Else
                        mclsTab_LifCov.InsPostDP018G_K(eFunctions.Menues.TypeActions.clngActionDuplicate, mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCoverNew"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                Else
                    With Response
                        .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                        .Write("ShowPopUp(""DP018Upd.aspx?sCodispl=DP018Upd"", ""DupCover"",400,180);")
                        .Write("self.history.go(-1)")
                        .Write("</" & "Script>")
                    End With
                End If
        End Select
    End Function

    '% InsFinishTab_GenCov: Finalizar en caso de coberturas genericas de ramos generales
    '--------------------------------------------------------------------------------------------
    Private Function InsFinishTab_GenCov() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsErrors As eGeneralForm.GeneralForm
        Dim llngAction As String
        Dim lstrError As String=String.Empty

        InsFinishTab_GenCov = True
        llngAction = Request.QueryString.Item("nMainAction")


        If llngAction.Length > 3 Then
            llngAction = llngAction.trim.substring(0, 3)
        End If

        Select Case llngAction
            Case eFunctions.Menues.TypeActions.clngActionAdd, eFunctions.Menues.TypeActions.clngActionUpdate
                If mclsTab_GenCov.ValContent("DP029_K", mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble)) Then
                    If mclsTab_GenCov.WithInformation <> "DP029   DP030A  DP030B" Then
                        lclsErrors = New eGeneralForm.GeneralForm
                        Session("sErrorTable") = lclsErrors.insValGE101("ClientSeq")
                        Session("sForm") = Request.Form.ToString
                        With Response
                            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);")
                            .Write("self.history.go(-1)")
                            .Write("</" & "Script>")
                        End With
                        InsFinishTab_GenCov = False
                        lclsErrors = Nothing
                    Else
                        mclsTab_GenCov.UpdateStatus(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), "1")
                    End If
                End If

            Case eFunctions.Menues.TypeActions.clngActioncut
                mclsTab_GenCov.Delete(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))

            Case eFunctions.Menues.TypeActions.clngActionDuplicate
                If Request.QueryString.Item("sDup") = "1" Then
                    lstrError = mclsTab_GenCov.InsValDP029_K("DP029_K", eFunctions.Menues.TypeActions.clngActionDuplicate, mobjValues.StringToType(Request.Form.Item("tcnCoverNew"), eFunctions.Values.eTypeData.etdDouble, True), True)
                    If lstrError <> vbNullString Then
                        lclsErrors = New eGeneralForm.GeneralForm
                        Session("sErrorTable") = lstrError
                        Session("sForm") = Request.Form.ToString
                        With Response
                            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);")
                            .Write("self.history.go(-1)")
                            .Write("</" & "Script>")
                        End With
                        InsFinishTab_GenCov = False
                        lclsErrors = Nothing
                    Else
                        mclsTab_GenCov.InsPostDP029_K(eFunctions.Menues.TypeActions.clngActionDuplicate, mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCoverNew"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                Else
                    With Response
                        .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                        .Write("ShowPopUp(""DP018Upd.aspx?sCodispl=DP018Upd"", ""DupCover"",400,180);")
                        .Write("self.history.go(-1)")
                        .Write("</" & "Script>")
                    End With
                End If
        End Select
    End Function

</script>
<%Response.Expires = -1

mclsTab_LifCov = New eProduct.Tab_lifcov
mclsTab_GenCov = New eProduct.Tab_gencov

mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Product&sProject=Product&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 21/10/03 15:39 $|$$Author: Nvaplat26 $"
</SCRIPT>
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
    '+ Si no se han validado los campos de la página
    If Request.Form.Item("sCodisplReload") = vbNullString Then
        mstrErrors = insvalSequence
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
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CoverSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If insPostSequence Then
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                    '+ se mueve automaticamente a la siguiente página
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Product/Product/Sequence.aspx?nAction=" & IIf(IsNothing(Request.QueryString.Item("nMainAction")), Request.QueryString.Item("nAction"), Request.QueryString.Item("nMainAction")) & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/Product/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                    End If

                    If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                        Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
                    End If
                End If
            End If
        End If
    Else
        '+ Se recarga la página principal de la secuencia			
        If insFinish() Then
            If Request.QueryString.Item("sDup") = "1" Then
                Response.Write("<SCRIPT>opener.top.location.reload();</SCRIPT>")
                Response.Write("<SCRIPT>window.close();</SCRIPT>")
            Else
                If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
                    Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
                End If
            End If
        End If
    End If
    mobjValues = Nothing
    mclsTab_LifCov = Nothing
    mclsTab_GenCov = Nothing
%>
</FORM>
</BODY>
</HTML>





