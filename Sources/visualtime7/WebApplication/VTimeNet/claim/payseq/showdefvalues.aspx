<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    Dim mobjValues As eFunctions.Values


    '% insShowExchange: se busca el factor de cambio para una moneda
    '%				    Se utiliza para el campo Moneda de la página SI008.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insShowExchange()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange
        Dim lstrValdate As Object

        lclsExchange = New eGeneral.Exchange

        With lclsExchange
            If .Find(mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Session("SI008_tcnExchange") = .nExchange
            Else
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '1';")
            End If
        End With
        'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsExchange = Nothing

        Session("SI008_tcdValdate") = Request.QueryString("dValdate")
        Response.Write("top.fraFolder.insSubmitPage();")


    End Sub

    '% insShowExchange_1: Se busca el factor de cambio para una moneda dada una fecha de valoración.
    '%				      Se utiliza para el campo "Factor de Cambio" de la página SI738.aspx
    '-----------------------------------------------------------------------------------------------
    Private Sub insShowExchange_1()
        '-----------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange

        lclsExchange = New eGeneral.Exchange

        With lclsExchange
            If .Find(mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                If .nExchange = 1 Then
                    Response.Write("top.fraFolder.document.forms[0].tcnExchange.disabled = true;")
                Else
                    Response.Write("top.fraFolder.document.forms[0].tcnExchange.disabled = false;")
                End If
            Else
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '1';")
            End If
        End With
        'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsExchange = Nothing
    End Sub

    '% insParamToConcept: se pasan los párametros al campo Concepto de pago
    '%				      Se utiliza para el campo Cobertura de la página SI008.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insParamToConcept()
        '--------------------------------------------------------------------------------------------
        Dim lclsCl_cover As eClaim.Cl_Cover
        Dim lclsClaim As eClaim.Claim
        Dim lclsT_Payclas As eClaim.T_PayClas
        Dim ldblTotalDev As Double
        Dim lclsExchange As eGeneral.Exchange
        Dim ldblAmount As Object
        Dim ldblAmount1 As Double
        Dim lintCurrency As Integer
        Dim lintCoverCur As Integer
        Dim ldblTax As Integer
        Dim ldblLoc_Amount As Object
        Dim dValdate As Date
        Dim lclsOpt_sinies As Object
        Dim lstrAlert As String
        Dim lclsErrors As eGeneral.GeneralFunction
        Dim lclsClaimBenef As eClaim.ClaimBenef
        Dim nParticip As Double
        Dim lclsGen_cover As eProduct.Gen_cover
        Dim lclsProf_ord As eClaim.Prof_ord
        Dim AmountServ_order As Double = 0
        lclsProf_ord = New eClaim.Prof_ord
        lclsGen_cover = New eProduct.Gen_cover
        lclsClaim = New eClaim.Claim
        lclsCl_cover = New eClaim.Cl_Cover
        lclsExchange = New eGeneral.Exchange
        lclsT_Payclas = New eClaim.T_PayClas
        lclsErrors = New eGeneral.GeneralFunction
        lclsClaimBenef = New eClaim.ClaimBenef

        If lclsClaim.Find(CDbl(Session("nClaim"))) Then
            If lclsCl_cover.Find_Policy(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdInteger), "", mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdInteger), lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.nPolicy, lclsClaim.nCertif, Session("dOccurdate_l")) Then
                '    								lclsClaim.dOccurdat) Then

                If  lclsProf_ord.Find_nServ(mobjValues.StringToType(CStr(Request.Querystring("Serv_order")), eFunctions.Values.eTypeData.etdDouble)) Then
                    AmountServ_order = lclsProf_ord.nAmount
                End if

                With Response
                    '+ Se pasa el parámetro a Concepto de pago: nModulec  
                    .Write("top.fraFolder.document.forms[0].tcnConcept.Parameters.Param1.sValue=" & lclsCl_cover.nModulec & ";")
                    .Write("top.fraFolder.document.forms[0].tcnConcept.Parameters.Param2.sValue=" & Request.QueryString("nCover") & ";")
                    .Write("top.fraFolder.document.forms[0].tcnId_Settle.Parameters.Param4.sValue=" & lclsCl_cover.nModulec & ";")
                    .Write("top.fraFolder.document.forms[0].tcnId_Settle.Parameters.Param5.sValue=" & Request.QueryString("nCover") & ";")

                    .Write("top.fraFolder.document.forms[0].nModulec.value=" & lclsCl_cover.nModulec & ";")
                    '+ Se asigna la moneda de la cobertura al campo Hidden
                    .Write("top.fraFolder.document.forms[0].tcnFra_amount.value='" & lclsCl_cover.nFra_amount & "';")
                    .Write("top.fraFolder.document.forms[0].nCoverCurrency.value=" & lclsCl_cover.nCurrency & ";")
                    .Write("top.fraFolder.document.forms[0].cbeCurrency_cov.value=" & lclsCl_cover.nCurrency & ";")
                    If  (  Session("nPay_Type")  <>  4   and    Session("nPay_Type")  <>  5 )  then
                        .Write("top.fraFolder.document.forms[0].tcnAmountPayedCover.value='" & lclsCl_cover.nPay_amount & "';")
                    end if
                    '+ Se asigna el grupo asegurado	al que pertenece la	cobertura  
                    dValdate = mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)
                    .Write("top.fraFolder.document.forms[0].nGroup_insu.value=" & lclsCl_cover.nGroup_insu & ";")

                    Call lclsExchange.Convert(eRemoteDB.Constants.intNull, 0, lclsCl_cover.nCurrency, CInt(Session("SI008_cbeCurrency")), CDate(Session("dPaydate")), eRemoteDB.Constants.intNull)
                    If lclsExchange.pdblExchange > 0 Then
                        'If lclsExchange.Find(lclsCl_cover.nCurrency , dValdate) Then  
                        .Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(lclsExchange.pdblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    End If

                    '+ Se obtiene el cálculo de la provisión pendiente si el rol corresponde a "Beneficiario" y el ramo es "Vida".  
                    If CStr(Session("SI008_sBrancht")) = "1" And CDbl(Session("SI008_cbeRole")) = 16 Then

                        'Busca el porcentaje de participación
                        nParticip = lclsClaimBenef.insCalBenefPercent(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdLong), CStr(Session("SI008_valClient")))

                        .Write("top.fraFolder.document.forms[0].tcnParticip.value='" & FormatNumber(mobjValues.StringToType(CStr(nParticip), eFunctions.Values.eTypeData.etdDouble)) & "';")

                        ldblLoc_Amount = lclsT_Payclas.insCalnLoc_Amount(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdLong), CStr(Session("SI008_valClient")), lclsCl_cover.nReserve, 2, Session("SI008_cbeRole"))

                        If (Session("nPay_Type") <> 4 And Session("nPay_Type") <> 5 And Session("nPay_Type") <> 6) Then
                            .Write("top.fraFolder.document.forms[0].tcnLocAmount.value='" & FormatNumber(mobjValues.StringToType(ldblLoc_Amount, eFunctions.Values.eTypeData.etdDouble), 6) & "';")
                            .Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value='" & FormatNumber(mobjValues.StringToType(ldblLoc_Amount, eFunctions.Values.eTypeData.etdDouble), 6) & "';")

                            ldblAmount = mobjValues.StringToType(ldblLoc_Amount, eFunctions.Values.eTypeData.etdDouble)
                            ldblAmount1 = mobjValues.StringToType(ldblLoc_Amount, eFunctions.Values.eTypeData.etdDouble, True)
                            Session("SI008_tcnAmountPay_Aux") = FormatNumber(ldblAmount, 6)

                            '+ Se envía advertencia si el monto de provisión pendiente es igual a cero (0), es decir que ya fue pagado el porcentaje del beneficiario.
                        end if
                        If ldblAmount <= 0 Then
                            lstrAlert = "Adv. 60580 - " & lclsErrors.insLoadMessage(60580)
                            Response.Write("alert('" & lstrAlert & "');")
                        End If
                        .Write("top.fraFolder.document.forms[0].tcnLocAmount_Pay.value='" & FormatNumber(lclsCl_cover.nReserve, 6) & "';")
                        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                        lclsErrors = Nothing
                    Else

                        If CDbl(Session("SI008_cbeRole")) = 16 Then
                            'Busca el porcentaje de participación
                            nParticip = lclsClaimBenef.insCalBenefPercent(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdLong), CStr(Session("SI008_valClient")))

                            .Write("top.fraFolder.document.forms[0].tcnParticip.value='" & FormatNumber(mobjValues.StringToType(CStr(nParticip), eFunctions.Values.eTypeData.etdDouble)) & "';")
                        End If

                        If (Session("nPay_Type") <> 4 And Session("nPay_Type") <> 5) Then
                            If nParticip > 0 Then
                                .Write("top.fraFolder.document.forms[0].tcnLocAmount.value='" & FormatNumber((lclsCl_cover.nAmount2 * nParticip) / 100, 6) & "';")
                                .Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value='" & FormatNumber((lclsCl_cover.nAmount2 * nParticip) / 100, 6) & "';")
                                ldblAmount = FormatNumber(mobjValues.StringToType(CStr((lclsCl_cover.nAmount2 * nParticip) / 100), eFunctions.Values.eTypeData.etdDouble, True), 6)
                                ldblAmount1 = mobjValues.StringToType(CStr((lclsCl_cover.nAmount2 * nParticip) / 100), eFunctions.Values.eTypeData.etdDouble, True)
                                Session("SI008_tcnAmountPay_Aux") = ldblLoc_Amount

                            Else
                                If AmountServ_order = 0 Or AmountServ_order < 0 Then
                                    .Write("top.fraFolder.document.forms[0].tcnLocAmount.value='" & FormatNumber(lclsCl_cover.nReserve, 6) & "';")
                                    .Write("top.fraFolder.document.forms[0].tcnLocAmount_Pay.value='" & FormatNumber(lclsCl_cover.nReserve, 6) & "';")
                                    .Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value='" & FormatNumber(lclsCl_cover.nReserve, 6) & "';")
                                    ldblAmount = FormatNumber(mobjValues.StringToType(CStr(lclsCl_cover.nReserve), eFunctions.Values.eTypeData.etdDouble, True), 6)
                                    ldblAmount1 = mobjValues.StringToType(CStr(lclsCl_cover.nReserve), eFunctions.Values.eTypeData.etdDouble, True)
                                    Session("SI008_tcnAmountPay_Aux") = ldblLoc_Amount
                                Else
                                    .Write("top.fraFolder.document.forms[0].tcnLocAmount.value='" & FormatNumber(lclsCl_cover.nReserve, 6) & "';")
                                    .Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value='" & FormatNumber(AmountServ_order, 6) & "';")
                                    .Write("top.fraFolder.document.forms[0].tcnLocAmount_Pay.value='" & FormatNumber(AmountServ_order, 6) & "';")
                                    ldblAmount = FormatNumber(mobjValues.StringToType(CStr(AmountServ_order), eFunctions.Values.eTypeData.etdDouble, True), 6)
                                    ldblAmount1 = mobjValues.StringToType(CStr(AmountServ_order), eFunctions.Values.eTypeData.etdDouble, True)
                                    Session("SI008_tcnAmountPay_Aux") = AmountServ_order
                                End If
                            End If
                        End If
                    End If

                    lintCurrency = mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdLong)

                    lintCoverCur = mobjValues.StringToType(CStr(lclsCl_cover.nCurrency), eFunctions.Values.eTypeData.etdLong)

                    ldblTax = mobjValues.StringToType(Request.QueryString("nTaxamo"), eFunctions.Values.eTypeData.etdDouble)

                    Call InsConvertAmount(ldblAmount1, lintCurrency, lintCoverCur, ldblTax, dValdate, 1)

                    If lclsClaim.nOffice_pay > 0 Then
                        Session("SI008_cbeOffice_pay") = lclsClaim.nOffice_pay
                    Else
                        If CDbl(Session("nOffice")) > 0 Then
                            Session("SI008_cbeOffice_pay") = Session("nOffice")
                        End If
                    End If

                    If lclsClaim.nOfficeAgen_pay > 0 Then
                        Session("nOfficeAgen_pay") = lclsClaim.nOfficeAgen_pay
                    Else
                        If CDbl(Session("nOfficeAgen")) > 0 Then
                            Session("nOfficeAgen_pay") = Session("nOfficeAgen")
                        End If
                    End If
                End With

                If CDbl(Session("nPay_Type")) = 7 And CStr(Session("SI008_sBrancht")) = "3" Then

                    ldblTotalDev = lclsT_Payclas.insDevDed(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), lclsCl_cover.nModulec, mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(lclsCl_cover.nCurrency), eFunctions.Values.eTypeData.etdLong), Session("SI008_valClient"))

                    Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.disabled=true;")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmountPayCover.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnTax.disabled=true;")
                    Call InsConvertAmount(ldblTotalDev, lintCurrency, lintCoverCur, ldblTax, dValdate, 1)

                    lclsT_Payclas = Nothing
                ElseIf CDbl(Session("nPay_Type")) = 7 And CStr(Session("SI008_sBrancht")) = "1" Then
                    ldblTotalDev = lclsCl_cover.insGetFra_Amount(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), lclsCl_cover.nModulec, mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(lclsCl_cover.nCurrency), eFunctions.Values.eTypeData.etdLong), Session("SI008_valClient"))

                    Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.disabled=true;")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmountPayCover.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount.value=" & ldblTotalDev & ";")
                    Response.Write("top.fraFolder.document.forms[0].tcnTax.disabled=true;")
                    Call InsConvertAmount(ldblTotalDev, lintCurrency, lintCoverCur, ldblTax, dValdate, 1)
                End If

                If lclsGen_cover.Find(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), lclsCl_cover.nModulec, mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate)) then
                    Response.Write("top.fraFolder.document.forms[0].hddRASA_routine.value='" & lclsGen_cover.sRASA_routine & "';")
                    If lclsGen_cover.sIndManualDeductible = "1" Then
                        Response.Write("top.fraFolder.document.forms[0].tcnFra_amount.disabled = false;")
                    Else
                        Response.Write("top.fraFolder.document.forms[0].tcnFra_amount.disabled = true;")
                    End If
                End If
                If Session("nPay_Type") = 2 Then
                    If AmountServ_order = 0 Or AmountServ_order < 0 Then
                        AmountServ_order = lclsCl_cover.nReserve + lclsCl_cover.nFra_amount
                    End if

                    If lclsCl_cover.InsCalSi008(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), lclsCl_cover.nModulec, lclsCl_cover.nGroup_insu, mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dOccurdate_l"), eFunctions.Values.eTypeData.etdDate), 0, 0, 0, lclsCl_cover.nFra_amount, lclsCl_cover.nPay_amount, AmountServ_order , lclsGen_cover.sRASA_routine, "2") Then
                        Response.Write("top.fraFolder.document.forms[0].tcnRasa.value = '" & mobjValues.TypeToString(lclsCl_cover.nRasa, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].tcnFra_amount.value = '" & mobjValues.TypeToString(lclsCl_cover.nFra_amount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].hddRasaAnnual.value = '" & mobjValues.TypeToString(lclsCl_cover.nRasaAnnual, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].tcnDepreciatebase.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciatebase, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].tcnDepreciaterate.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciaterate, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].tcnDepreciateamount.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciateamount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Response.Write("top.fraFolder.document.forms[0].tcnDDR.value = '" & mobjValues.TypeToString((lclsCl_cover.nDepreciateamount + lclsCl_cover.nFra_amount + lclsCl_cover.nRasa), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")

                        '  Response.Write("top.fraFolder.document.forms[0].tcnAmountPayCover.value = " & )

                        '    self.document.forms[0].tcnAmountPayCover.value = eval(self.document.forms[0].tcnAmountPayCover.value.replace('.', '').replace(',', '.')) - eval(self.document.forms[0].tcnDDR.value.replace('.', '').replace(',', '.'));

                        'self.document.forms[0].tcnLocAmount_Pay.value = eval(self.document.forms[0].tcnLocAmount_Pay.value.replace('.', '').replace(',', '.')) - eval(self.document.forms[0].tcnDDR.value.replace('.', '').replace(',', '.'));


                    End If
                End if
                'Response.Write("top.fraFolder.document.forms[0].hddCl_Cover_Reserve.value = '" & mobjValues.TypeToString(lclsCl_cover.nReserve, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Response.Write("top.fraFolder.document.forms[0].hddCl_Cover_Reserve.value = '" & lclsCl_cover.nReserve.ToString() & "';")
            End If
        End If

        lclsCl_cover = Nothing
        lclsClaim = Nothing
        lclsExchange = Nothing
        lclsT_Payclas = Nothing
        lclsClaimBenef = Nothing
        lclsGen_cover  = Nothing
    End Sub

    '% insDeletePayCla: Se eliminan los datos asociados al pago del siniestro
    '%				    Se utiliza para el campo Moneda y Tipo de Pago de la página SI008.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insDeletePayCla()
        '--------------------------------------------------------------------------------------------
        Dim lclsT_PayCla As eClaim.T_PayCla
        Dim lintCurrency As Integer
        Dim lclsExchange As eGeneral.Exchange
        Dim lstrValdate As Object
        Dim lclsOpt_sinies As eClaim.Opt_sinies
        Dim lintOpt_cur As Integer
        Dim lintTax As Double
        Dim lobjTax_FixVal As eAgent.tax_fixval
        Dim stypeTax As String
        Dim lintCurrPol As Integer
        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lclsClaim As eClaim.Claim

        lclsClaim = New eClaim.Claim
        lclsCurren_pol = New ePolicy.Curren_pol
        lclsExchange = New eGeneral.Exchange
        lclsT_PayCla = New eClaim.T_PayCla

        If Request.QueryString("nCalTaxFix") = "Yes" Then
            lobjTax_FixVal = New eAgent.tax_fixval

            With lobjTax_FixVal
                If .Find_nTypesupport(mobjValues.StringToType(Request.QueryString("nDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
                    stypeTax = .stypeTax
                    lintTax = .nPercent
                    Session("stypeTax") = stypeTax
                    If stypeTax = "2" Then
                        lintTax = lintTax * (-1)
                    End If
                End If
            End With
            'UPGRADE_NOTE: Object lobjTax_FixVal may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            lobjTax_FixVal = Nothing
        Else
            lintTax = Request.QueryString("nTax_TPC")
        End If

        lintCurrency = Request.QueryString("nCurrency")

        Session("SI008_cbeDoc_Type") = mobjValues.StringToType(Request.QueryString("nDoc_Type"), eFunctions.Values.eTypeData.etdDouble)
        Session("SI008_cbeCurrency") = lintCurrency
        Session("SI008_tcdValdate") = mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)

        Session("nCurrPaySI008") = lintCurrency

        If lclsClaim.Find(CDbl(Session("nClaim"))) Then
            If lclsCurren_pol.Find_Currency_Sel(lclsClaim.sCertype, lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.nPolicy, lclsClaim.nCertif, CDate(Session("dOccurdate_l"))) Then
                lintCurrPol = lclsCurren_pol.nCurrency
            Else
                lintCurrPol = 1
            End If
        End If

        With lclsExchange
            Call lclsExchange.Convert(eRemoteDB.Constants.intNull, 0, lintCurrPol, CInt(Session("SI008_cbeCurrency")), CDate(Session("SI008_tcdValdate")), eRemoteDB.Constants.intNull)

            If .pdblExchange > 0 Then
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(.pdblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                Session("SI008_tcnExchange") = .pdblExchange
            Else
                Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '0';")
            End If
        End With

        If lclsT_PayCla.Find(mobjValues.StringToType(Request.QueryString("nClaim_TPC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nDeman_type_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_curr_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPay_concep_TPC"), eFunctions.Values.eTypeData.etdLong), True) Then
            With lclsExchange
                If lclsT_PayCla.DeleteByCase(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type"))) Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].hddCurrency.value=" & lintCurrency & ";")
                    Session("nCurrPaySI008") = lintCurrency

                    If Request.QueryString("dValdate") <> vbNullString Then
                        lstrValdate = Request.QueryString("dValdate")
                    Else
                        lstrValdate = Session("dEffecdate")
                    End If

                    Call .Convert(eRemoteDB.Constants.intNull, 0, lintCurrPol, CInt(Session("SI008_cbeCurrency")), CDate(Session("SI008_tcdValdate")), eRemoteDB.Constants.intNull)

                    If .pdblExchange > 0 Then
                        Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(.pdblExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        Session("SI008_tcnExchange") = .pdblExchange
                    Else
                        Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '0';")
                    End If

                    If CDbl(FormatNumber(mobjValues.StringToType(CStr(Session("SI008_LocPremium")), eFunctions.Values.eTypeData.etdDouble), 6)) > 0 Then
                        lclsOpt_sinies = New eClaim.Opt_sinies
                        If lclsOpt_sinies.Find() Then
                            lintOpt_cur = lclsOpt_sinies.nCurrency
                        Else
                            lintOpt_cur = 1
                        End If
                        'UPGRADE_NOTE: Object lclsOpt_sinies may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                        lclsOpt_sinies = Nothing
                        Call .Convert(0, CDbl(Session("SI008_LocPremium")), lintOpt_cur, lintCurrency, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), 0)
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnPremium.value=" & FormatNumber(mobjValues.StringToType(CStr(.pdblResult), eFunctions.Values.eTypeData.etdDouble), 6) & ";")
                    End If

                    Call lclsT_PayCla.insChangeValdate_Currency(mobjValues.StringToType(Request.QueryString("nClaim_TPC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nDeman_type_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_curr_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nModulec_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPay_concep_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPaycov_amount_TPC"), eFunctions.Values.eTypeData.etdDouble), lintTax, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nGroup_insu_TPC"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString("nIndAutomatic_TPC"), eFunctions.Values.eTypeData.etdInteger), lintCurrency, mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate))

                End If
                'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsExchange = Nothing
            End With
            Response.Write("top.fraFolder.insSubmitPage();")
        Else
            If CDbl(Session("nPay_Type")) = 7 And mobjValues.StringToType(Request.QueryString("nClaim_TPC"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then

                Call lclsT_PayCla.insChangeValdate_Currency(mobjValues.StringToType(Request.QueryString("nClaim_TPC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nDeman_type_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_curr_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nCover_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nModulec_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPay_concep_TPC"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPaycov_amount_TPC"), eFunctions.Values.eTypeData.etdDouble), lintTax, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nGroup_insu_TPC"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString("nIndAutomatic_TPC"), eFunctions.Values.eTypeData.etdInteger), lintCurrency, mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate))
                Response.Write("top.fraFolder.insSubmitPage();")
            End If
        End If
        lclsT_PayCla = Nothing
        lclsClaim = Nothing
        lclsCurren_pol = Nothing
    End Sub

    '% insExpandCodeClient: Se expande el código del cliente
    '%				        Se utiliza para el campo Cliente de la página SI008.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insExpandCodeClient()
        '--------------------------------------------------------------------------------------------
        Dim lstrClient As String
        Dim lclsClient As eClient.Client
        lclsClient = New eClient.Client

        lstrClient = lclsClient.ExpandCode(Request.QueryString("sClient"))
        Response.Write("opener.document.forms[0].valClient.value=""" & lstrClient & """;")
        lclsClient = Nothing
    End Sub

    '% insReaClientRole:
    '--------------------------------------------------------------------------------------------
    Private Sub insReaClientRole()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Claim
        Dim lclsClient_Aux As eClient.Client
        Dim lclsClaimBenef As eClaim.ClaimBenef

        lclsClaim = New eClaim.Claim
        lclsClient_Aux = New eClient.Client

        If mobjValues.StringToType(Request.QueryString("nRole"), eFunctions.Values.eTypeData.etdLong, 0) = 0 Then
            Session("SI008_cbeRole") = "0"
            Session("SI008_valClient") = ""
            Session("SI008_valClient_rep") = ""
        Else
            If lclsClaim.Find(CDbl(Session("nClaim"))) Then
                '+Se busca el cliente asociado al siniestro con la figura indicada.
                '+Titular de la orden de pago y el destino del cheque.
                lclsClaimBenef = New eClaim.ClaimBenef
                Session("SI008_nCountBenef") = 0
                Session("SI008_sino") = 0
                With lclsClaimBenef
                    If .FindClaimBenef_1(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nRole"), eFunctions.Values.eTypeData.etdLong), "1", "") Then
                        If .nCountBenef > 1 Then
                            Response.Write("top.fraFolder.document.forms[0].valClient.disabled=false;")
                            Response.Write("top.fraFolder.document.forms[0].btnvalClient.disabled=false;")
                        Else
                            Response.Write("top.fraFolder.document.forms[0].valClient.disabled=true;")
                            Response.Write("top.fraFolder.document.forms[0].btnvalClient.disabled=true;")
                        End If

                        Call .FindBenef(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), Request.QueryString("nRole"))

                        Session("SI008_cbeRole") = Request.QueryString("nRole")

                        If CStr(Session("SI008_cbeOffice_payAux")) = vbNullString Then
                            If .nOffice_pay > 0 Then
                                Response.Write("top.fraFolder.document.forms[0].cbeOffice.value= '" & .nOffice_pay & "';")
                                Session("SI008_cbeOffice_pay") = .nOffice_pay
                                Response.Write("top.fraFolder.BlankOfficeDepend();top.fraFolder.insInitialAgency(1,0);")
                                If .nOfficeAgen_pay > 0 Then
                                    Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.value= '" & .nOfficeAgen_pay & "';")
                                    Response.Write("top.fraFolder.$('#cbeOfficeAgen').change();")
                                    Session("SI008_cbeOfficeAgen_pay") = .nOfficeAgen_pay
                                End If
                                If .nAgency_pay > 0 Then
                                    Response.Write("top.fraFolder.document.forms[0].cbeAgency.value= '" & .nAgency_pay & "';")
                                    Response.Write("top.fraFolder.$('#cbeAgency').change();")
                                    Session("SI008_cbeOfficeAgen_pay") = .nAgency_pay
                                End If
                            Else
                                If lclsClaim.nOffice_pay > 0 Then
                                    Response.Write("top.fraFolder.document.forms[0].cbeOffice.value= '" & lclsClaim.nOffice_pay & "';")
                                    Session("SI008_cbeOffice_pay") = lclsClaim.nOffice_pay
                                    Response.Write("top.fraFolder.BlankOfficeDepend();top.fraFolder.insInitialAgency(1,0);")
                                End If
                                If lclsClaim.nOfficeAgen_pay > 0 Then
                                    Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.value= '" & lclsClaim.nOfficeAgen_pay & "';")
                                    Session("SI008_cbeOfficeAgen_pay") = lclsClaim.nOfficeAgen_pay
                                End If
                                If lclsClaim.nAgency_pay > 0 Then
                                    Response.Write("top.fraFolder.document.forms[0].cbeAgency.value= '" & lclsClaim.nAgency_pay & "';")
                                    Session("SI008_cbeOfficeAgen_pay") = lclsClaim.nAgency_pay
                                End If
                            End If
                        End If
                        'If .nCountBenef > 1 then

                        If Request.QueryString("sClient") = "" Then
                            Response.Write("top.fraFolder.document.forms[0].valClient.value= '" & lclsClaimBenef.sClient & "';")
                            Response.Write(" top.frames['fraFolder'].UpdateDiv('valClient','" &  Replace(lclsClaimBenef.sCliename, "'", "´") & "');")
                            ' Response.Write("top.fraFolder.document.forms[0].valClient.onblur();")
                            Session("SI008_valClient") = lclsClaimBenef.sClient
                        Else
                            Session("SI008_valClient") = Request.QueryString("sClient")
                        End If
                        Session("SI008_cbeOffice_payAux") = ""
                        Call lclsClaimBenef.Find_client(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), True, mobjValues.StringToType(Request.QueryString("nRole"), eFunctions.Values.eTypeData.etdLong))

                        If lclsClaimBenef.sClient_rep <> vbNullString Then
                            Call lclsClient_Aux.FindClientName(lclsClaimBenef.sClient_rep)
                            Response.Write(" top.frames['fraFolder'].UpdateDiv('valClient_rep','" & .sClient_rep & " - " & Replace(lclsClient_Aux.sCliename, "'", "´") & "');")
                            Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value= '" & .sClient_rep & "';")
                            Session("SI008_valClient_rep") = .sClient_rep
                        Else
                            If Request.QueryString("sClient") <> "" Then
                                Call lclsClient_Aux.FindClientName(Request.QueryString("sClient"))
                                Response.Write("top.frames['fraFolder'].UpdateDiv('valClient_rep','" & Request.QueryString("sClient") & " - " & Replace(lclsClient_Aux.sCliename, "'", "´") & "');")
                                Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value= '" & Request.QueryString("sClient") & "';")
                            Else
                                Call lclsClient_Aux.FindClientName(CStr(Session("SI008_valClient")))
                                Response.Write("top.frames['fraFolder'].UpdateDiv('valClient_rep','" & Session("SI008_valClient") & " - " & Replace(lclsClient_Aux.sCliename, "'", "´") & "');")
                                Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value= '" & Session("SI008_valClient") & "';")
                                Session("SI008_valClient_rep") = Session("SI008_valClient")
                            End If
                        End If

                        If Request.QueryString("sClient") = "" Then
                            Response.Write(" top.frames['fraFolder'].UpdateDiv('valClientDesc','" & .sCliename & "' );")
                        End If

                        Response.Write("top.fraFolder.document.forms[0].valServ_order.Parameters.Param4.sValue='" & Session("SI008_valClient") & "';")
                        Response.Write("top.fraFolder.$('#valServ_order').change();")
                    Else
                        Response.Write("top.fraFolder.document.forms[0].valClient.value= '';")
                        Response.Write(" top.frames['fraFolder'].UpdateDiv('valClientDesc','' );")
                        Response.Write(" top.frames['fraFolder'].UpdateDiv('valClient_rep','' );")
                        Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value= '';")
                    End If
                End With
                lclsClaimBenef = Nothing
            End If
        End If
        lclsClaim = Nothing
        lclsClient_Aux = Nothing
    End Sub

    '% insDP051_Claim: Se encarga de guardar en la variable de session DP051_nClaim el 
    '                  siniestro de le pantalla SI051 y llevarlo como consulta a la SI001_K
    '--------------------------------------------------------------------------------------------
    Private Sub insDP051_Claim()
        '--------------------------------------------------------------------------------------------
        Session("DP051_nClaim") = Request.QueryString("nClaim")
    End Sub

    '% insSI021: Se encarga de guardar en variables session los datos para la SI_007_2
    '--------------------------------------------------------------------------------------------
    Private Sub insSI021()
        '--------------------------------------------------------------------------------------------
        Session("SI021_nClaim") = Request.QueryString("nClaim")
        Session("nClaim") = Request.QueryString("nClaim")
        Session("nPolicy") = Request.QueryString("nPolicy")
        Session("nBranch") = Request.QueryString("nBranch")
        Session("nCertIf") = Request.QueryString("nCertIf")
        Session("nProduct") = Request.QueryString("nProduct")
        Session("dEffecdate") = Today
    End Sub

    '% insProvider:
    '--------------------------------------------------------------------------------------------
    Private Sub insProvider()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Prof_ord
        lclsClaim = New eClaim.Prof_ord

        With lclsClaim
            If .FindProviderOrder(CShort(Request.QueryString("nZone"))) Then
                If .nProvider <> 0 Then
                    Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
                    Response.Write("    cbeProvider.value= " & mobjValues.TypeToString(.nProvider, eFunctions.Values.eTypeData.etdLong) & ";")
                    Response.Write("    cbeProvider.disabled=true;")
                    Response.Write("    document.btncbeProvider.disabled=true;")
                    Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','" & .sProviderName & "');")
                    Response.Write("}")
                Else
                    Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
                    Response.Write("    cbeProvider.value=0;")
                    Response.Write("    cbeProvider.disabled=false;")
                    Response.Write("    document.btncbeProvider.disabled=false;")
                    Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','');")
                    Response.Write("}")
                End If
            End If

        End With
        lclsClaim = Nothing
    End Sub

    '% insSI008_K: Se encarga de buscar los valores que deben ser mostrados por el sistema
    '%             asociados al siniestro.
    '--------------------------------------------------------------------------------------------
    Private Sub insSI008_K()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Claim
        Dim lclsProducts As eProduct.Product

        lclsClaim = New eClaim.Claim
        With lclsClaim
            If .FindControl(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
                Response.Write("top.frames['fraHeader'].UpdateDiv('cbeBranch','" & .sBranchDesc & "');")
                Response.Write("top.frames['fraHeader'].UpdateDiv('valProduct','" & .nProduct & " - " & .sProductDesc & "');")
                Response.Write("top.frames['fraHeader'].UpdateDiv('tcnPolicy','" & .nPolicy & "');")

                '+ se asignan los valores a los campos ocultos
                Response.Write("top.fraHeader.document.forms[0].hddBranch.value = '" & .nBranch & "';")
                Response.Write("top.fraHeader.document.forms[0].hddProduct.value = '" & .nProduct & "';")
                Response.Write("top.fraHeader.document.forms[0].hddPolicy.value = '" & .nPolicy & "';")
                Response.Write("top.fraHeader.document.forms[0].hddCertif.value = '" & .nCertif & "';")


                If .nOffice_pay > 0 Then
                    Session("SI008_cbeOffice_pay") = .nOffice_pay
                Else
                    If CDbl(Session("nOffice")) > 0 Then
                        Session("SI008_cbeOffice_pay") = Session("nOffice")
                    End If
                End If

                If .nOfficeAgen_pay > 0 Then
                    Session("SI008_cbeOfficeAgen_pay") = .nOfficeAgen_pay
                Else
                    If CDbl(Session("cbeOfficeAgen")) > 0 Then
                        Session("SI008_cbeOfficeAgen_pay") = Session("nOfficeAgen")
                    End If
                End If

                If .nAgency_pay > 0 Then
                    Session("SI008_cbeAgency_pay") = .nAgency_pay
                Else
                    If CDbl(Session("nAgency")) > 0 Then
                        Session("SI008_cbeAgency_pay") = Session("nAgency")
                    End If
                End If

                If .nBranch > 0 And .nProduct > 0 Then
                    lclsProducts = New eProduct.Product
                    If lclsProducts.FindProdMaster(.nBranch, .nProduct, True) Then
                        Session("SI008_sBrancht") = lclsProducts.sBrancht
                    End If
                    lclsProducts = Nothing
                End If

            End If
        End With
        lclsClaim = Nothing
    End Sub

    '%insShowDemandat: Se obtiene el código del denunciante y la sucursal asociada
    '%----------------------------------------------------------------------------------------------------------
    Private Sub insShowDemandat()
        '%----------------------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Claim
        Dim lclsClaimBenef As eClaim.ClaimBenef

        lclsClaim = New eClaim.Claim
        lclsClaimBenef = New eClaim.ClaimBenef

        '+ Se obtiene el código del denunciante (reclamante).
        If lclsClaimBenef.Find_Demandant(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
            Response.Write("top.fraFolder.document.forms[0].tctClientCode.value = '" & lclsClaimBenef.sClient & "';")
        End If

        '+ Se obtiene la sucursal asociada al reclamante de la relación.
        If lclsClaim.Find(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
            Response.Write("top.fraFolder.document.forms[0].cbeOfficePay.value =" & lclsClaim.nOffice & ";")
        End If

    End Sub
    '% ShowServiceOrderData: Muestra la data relacionada con una orden de servicio específica - ACM - 18/06/2002
    '-----------------------------------------------------------------------------------------------------------
    Private Sub ShowServiceOrderData()
        '-----------------------------------------------------------------------------------------------------------
        Dim lclsQuot_parts As eClaim.Quot_parts
        Dim lclsProf_ord As eClaim.Prof_ord
        Dim lclsValues As eFunctions.Values

        If Request.QueryString.GetValues("nServiceOrder").GetValue(1) <> vbNullString And Request.QueryString.GetValues("nServiceOrder").GetValue(1) > 0 Then
            lclsQuot_parts = New eClaim.Quot_parts
            lclsValues = New eFunctions.Values
            '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
            lclsValues.sSessionID = Session.SessionID
            lclsValues.nUsercode = Session("nUsercode")
            '~End Body Block VisualTimer Utility

            lclsValues.sCodisplPage = "showdefvalues"
            If lclsQuot_parts.Find(lclsValues.StringToType(Request.QueryString("nClaimNumber"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Request.QueryString("nCaseNumber"), eFunctions.Values.eTypeData.etdLong), lclsValues.StringToType(Request.QueryString("nDemandantType"), eFunctions.Values.eTypeData.etdLong), lclsValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdLong)) Then
                If lclsQuot_parts.Item(1) Then
                    'Response.Write("opener.document.forms[0].elements['cbeMark'].value=" & lclsQuot_parts.nVehBrand & ";")
                    'Response.Write("opener.document.forms[0].elements['cbeModel'].value='" & lclsQuot_parts.sVehModel & "';")
                    Response.Write("opener.document.forms[0].elements['tcnYear'].value=" & lclsQuot_parts.nYear & ";")
                    Response.Write("opener.document.forms[0].elements['tctChasisCode'].value='" & lclsQuot_parts.sChassis & "';")
                End If
            End If
            If Request.QueryString("sForm") <> "SI011" Then
                lclsProf_ord = New eClaim.Prof_ord
                If lclsProf_ord.Find_nServ(Request.QueryString("nServiceOrder")) Then
                    Response.Write("opener.document.forms[0].elements['tcnTypeOrder'].value=" & lclsProf_ord.nOrderType & ";")
                    'Response.Write("opener.document.forms[0].elements['tctStateOrder'].value='" & lclsProf_ord.sStatus_ord & "';")
                    Response.Write("opener.document.forms[0].elements['tcnDemandantType'].value=" & lclsProf_ord.nDeman_type & ";")
                    Response.Write("opener.document.forms[0].elements['tcnTransaction'].value=" & lclsProf_ord.nTransac & ";")
                End If
                lclsProf_ord = Nothing
            End If
            lclsQuot_parts = Nothing
            lclsValues = Nothing
        End If
    End Sub

    '% insReaClient_rep: Se realiza la busqueda del representante del beneficiario y la sucursal destino
    '--------------------------------------------------------------------------------------------
    Private Sub insReaClient_rep()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaimBenef As eClaim.ClaimBenef
        Dim lclsClaim As eClaim.Claim
        Dim lclsT_PayCla As eClaim.T_PayCla

        lclsClaimBenef = New eClaim.ClaimBenef
        lclsClaim = New eClaim.Claim
        lclsT_PayCla = New eClaim.T_PayCla

        With lclsClaimBenef

            If .FindBenef(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), Request.QueryString("nRole")) Then

                '+ Se verifica si se cambió el cliente (id) para borrar la tabla T_Paycla.
                If Request.QueryString("sClient") <> "" And CStr(Session("SI008_valClient")) <> "" Then
                    If Request.QueryString("sClient") <> Session("SI008_valClient") Then
                        Call lclsT_PayCla.DeleteByCase(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")))
                        Response.Write("top.frames['fraFolder'].document.location.reload();")
                    End If
                    If Request.QueryString("nId") <> Session("SI008_nId") Then
                        Call lclsT_PayCla.DeleteByCase(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")))
                    End If
                    lclsT_PayCla = Nothing
                End If

                Session("SI008_valClient") = Request.QueryString("sClient")
                Session("SI008_nId") = Request.QueryString("nId")

                If lclsClaimBenef.nOffice_pay <> eRemoteDB.Constants.intNull Then
                    Response.Write("top.fraFolder.document.forms[0].cbeOffice.value=""" & lclsClaimBenef.nOffice_pay & """;")
                    Session("SI008_cbeOffice_pay") = lclsClaimBenef.nOffice_pay
                Else
                    lclsClaim = New eClaim.Claim
                    With lclsClaim
                        If .Find(CDbl(Session("nClaim"))) Then
                            Response.Write("top.fraFolder.document.forms[0].cbeOffice.value=""" & lclsClaim.nOffice_pay & """;")
                            Session("SI008_cbeOffice_pay") = lclsClaim.nOffice_pay
                        End If
                    End With
                    lclsClaim = Nothing
                End If

                If lclsClaimBenef.sClient_rep <> vbNullString Then
                    Response.Write(" top.frames['fraFolder'].UpdateDiv('valClient_rep','" & lclsClaimBenef.sClient_rep & " - " & Replace(lclsClaimBenef.sCliename, "'", "´") & "' );")
                    Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value=""" & lclsClaimBenef.sClient_rep & """;")
                    Session("SI008_valClient_rep") = lclsClaimBenef.sClient_rep
                Else
                    Response.Write("top.frames['fraFolder'].UpdateDiv('valClient_rep','" & Request.QueryString("sClient") & " - " & Replace(lclsClaimBenef.sCliename, "'", "´") & "' );")
                    Response.Write("top.fraFolder.document.forms[0].hddvalClient_rep.value=""" & Request.QueryString("sClient") & """;")
                End If
            End If
        End With

        lclsClaimBenef = Nothing
    End Sub
    '% ShowAccount: Se realiza la busqueda de la cuenta y el numero de credito asociado a la poliza en tratamiento
    '--------------------------------------------------------------------------------------------
    Private Sub ShowAccount()
        '--------------------------------------------------------------------------------------------
        Dim lclsLIfe As ePolicy.Life

        lclsLIfe = New ePolicy.Life

        With lclsLIfe
            If .Find("2", Request.QueryString("nBranch"), Request.QueryString("nProduct"), Request.QueryString("nPolicy"), Request.QueryString("nCertIf"), Today) Then
                Response.Write("top.fraFolder.document.forms[0].tctCredit.value = " & .sCreditnum & ";")
                Response.Write("top.fraFolder.document.forms[0].tctAccount.value = " & .sAccnum & ";")
            Else
                Response.Write("top.fraFolder.document.forms[0].tctCredit.value = '0';")
                Response.Write("top.fraFolder.document.forms[0].tctAccount.value = '0';")
            End If
        End With
        lclsLIfe = Nothing
    End Sub
    '%ShowProvider: Se realiza la busqueda del proveedor asociado a la cobertura generica
    '--------------------------------------------------------------------------------------------
    Private Sub ShowProvider()
        '--------------------------------------------------------------------------------------------
        Dim lclsCover As eClaim.Cl_Cover
        Dim lstrClient As String

        lclsCover = New eClaim.Cl_Cover
        lstrClient = lclsCover.Find_CoverProvider(mobjValues.StringToDate(Request.QueryString("dEffecdate")), Request.QueryString("nBranch"), Request.QueryString("nProduct"), Request.QueryString("nCover"))
        If lstrClient <> "" Then
            Response.Write("top.fraHeader.document.forms[0].tctClientCollect.value = '" & lstrClient & "';")
        End If
        lclsCover = Nothing
    End Sub

    '%ShowCurrency(). Muestra la moneda asociada a la poliza en tratamiento
    '--------------------------------------------------------------------------------------------
    Private Sub ShowCurrency()
        '--------------------------------------------------------------------------------------------
        Dim lclsCurrenpol As eClaim.Claim_Master
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrCurrency As String

        lclsCurrenpol = New eClaim.Claim_Master
        lstrCurrency = lclsCurrenpol.ShowCurrency(mobjValues.StringToDate(Request.QueryString("dEffecdate")), Request.QueryString("nBranch"), Request.QueryString("nProduct"), Request.QueryString("nPolicy"), Request.QueryString("nCertIf"))
        If lstrCurrency <> "" Then
            Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value = '" & lstrCurrency & "';")
        End If
        lclsCurrenpol = Nothing
        lclsPolicy = New ePolicy.Policy
        If lclsPolicy.Find("2", Request.QueryString("nBranch"), Request.QueryString("nProduct"), Request.QueryString("nPolicy"), True) Then
            Response.Write("top.fraHeader.document.forms[0].hddPoliType.value = '" & lclsPolicy.sPolitype & "';")
        End If
        lclsPolicy = Nothing
    End Sub
    '%ShowBrancht(). Realiza la busqueda del tipo de ramo asociado al ramo producto en tratamiento
    '--------------------------------------------------------------------------------------------
    Private Sub ShowBrancht()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        Dim ldtmEffecdate As Date

        ldtmEffecdate = Today
        lclsProduct = New eProduct.Product
        If lclsProduct.Find(Request.QueryString("nBranch"), Request.QueryString("nProduct"), ldtmEffecdate) Then
            Response.Write("top.fraHeader.document.forms[0].hddBrancht.value = '" & lclsProduct.sBrancht & "';")
        End If
        lclsProduct = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub insSI008_value()
        '--------------------------------------------------------------------------------------------      
        Select Case Request.QueryString("FieldControl")
            Case "cbeRole"
                Session("SI008_cbeRole") = Request.QueryString("Value")
            Case "valClient"
                Session("SI008_valClient") = Request.QueryString("Value")
            Case "valClient_rep"
                Session("SI008_valClient_rep") = Request.QueryString("Value")
            Case "cbeOffice"
                Session("SI008_cbeOffice_pay") = Request.QueryString("Value")
            Case "cbeOfficeAgen"
                Session("SI008_cbeOfficeAgen_pay") = Request.QueryString("Value")
            Case "cbeAgency"
                Session("SI008_cbeAgency_pay") = Request.QueryString("Value")
            Case "cbePayForm"
                Session("SI008_cbePayForm") = Request.QueryString("Value")
            Case "valServ_order"
                Session("SI008_valServ_order") = Request.QueryString("Value")
            Case "tcnInvoice"
                Session("SI008_tcnInvoice") = Request.QueryString("Value")
            Case "cbeCurrency"
                Session("SI008_cbeCurrency") = Request.QueryString("Value")
            Case "tcnExchange"
                Session("SI008_tcnExchange") = Request.QueryString("Value")
            Case "tcnAmountPay"
                Session("SI008_tcnAmountPay") = Request.QueryString("Value")
            Case "tcdPaydate"
                Session("SI008_tcdPayDate") = Request.QueryString("Value")
            Case "tcdValdate"
                Session("SI008_tcdValdate") = Request.QueryString("Value")
            Case "tcdBillDate"
                Session("SI008_tcdBillDate") = Request.QueryString("Value")
            Case "cbeDoc_Type"
                Session("SI008_cbeDoc_Type") = Request.QueryString("Value")
            Case "cbeOfficeAux"
                Session("SI008_cbeOffice_payAux") = Request.QueryString("Value")
            Case "cbeDeductible_Met"
                Session("SI008_cbeDeductible_Met") = Request.QueryString("Value")

        End Select

    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub insShowPremium()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange
        Dim lclsOpt_sinies As eClaim.Opt_sinies
        Dim lintCurrency As Integer

        lclsExchange = New eGeneral.Exchange
        lclsOpt_sinies = New eClaim.Opt_sinies

        Session("SI008_Premium") = Request.QueryString("nAmount")
        If lclsOpt_sinies.Find() Then
            lintCurrency = lclsOpt_sinies.nCurrency
        Else
            lintCurrency = 1
        End If

        Call lclsExchange.Convert(0, mobjValues.StringToType(CStr(Session("SI008_Premium")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCurrPaySI008")), eFunctions.Values.eTypeData.etdLong), lintCurrency, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), 0)

        Session("SI008_LocPremium") = mobjValues.TypeToString(lclsExchange.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 6)
        'UPGRADE_NOTE: Object lclsOpt_sinies may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsOpt_sinies = Nothing
        'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsExchange = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub InsCalAgencyPay()
        '--------------------------------------------------------------------------------------------
        Dim mobjClaimbenef As eClaim.ClaimBenef
        mobjClaimbenef = New eClaim.ClaimBenef

        Call mobjClaimbenef.UpdBenefPay(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("nOffice"), Request.QueryString("nAgency"), Request.QueryString("nOfficeAgen"))
        mobjClaimbenef = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub InsCalAmountPay()
        '--------------------------------------------------------------------------------------------
        Dim ldblAmount As Double
        Dim lintCurrency As Integer
        Dim lintCoverCur As Integer
        Dim ldblTax As Integer
        Dim dValdate As Date
        Dim lintTyp As Byte
        Dim lclsCl_cover As eClaim.Cl_cover
        lclsCl_cover = new eClaim.Cl_cover
        '+ lintTyp = 1, de moneda cobertura a moneda pago 	
        '+ lintTyp = 2, de moneda pago      a moneda cobertura 	

        Session("SI008_tcnAmountPay_Aux") = FormatNumber(mobjValues.StringToType(Request.QueryString("nAmount"), eFunctions.Values.eTypeData.etdDouble), 6)

        ldblAmount = mobjValues.StringToType(Request.QueryString("nAmount"), eFunctions.Values.eTypeData.etdDouble)
        ldblTax = mobjValues.StringToType(Request.QueryString("nTaxamo"), eFunctions.Values.eTypeData.etdLong)
        dValdate = mobjValues.StringToType(Request.QueryString("dValdate"), eFunctions.Values.eTypeData.etdDate)
        lintTyp = mobjValues.StringToType(Request.QueryString("nTyp"), eFunctions.Values.eTypeData.etdInteger)

        If lintTyp = 1 Then
            lintCurrency = mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdLong)
            lintCoverCur = mobjValues.StringToType(Request.QueryString("nCoverCurr"), eFunctions.Values.eTypeData.etdLong)
        Else
            lintCoverCur = mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdLong)
            lintCurrency = mobjValues.StringToType(Request.QueryString("nCoverCurr"), eFunctions.Values.eTypeData.etdLong)
        End If

        Call InsConvertAmount(ldblAmount, lintCurrency, lintCoverCur, ldblTax, dValdate, lintTyp)

    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub InsConvertAmount(ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nCoverCur As Integer, ByVal nTax As Integer, ByVal dValdate As Date, ByVal lintTyp As Byte)
        '--------------------------------------------------------------------------------------------
        Dim lclsConvert As eGeneral.Exchange
        Dim ldblCurAmount As Double
        Dim ldblCurAmount_Aux As Double
        Dim nAmount_Aux As Double
        lclsConvert = New eGeneral.Exchange

        If nAmount = eRemoteDB.Constants.intNull Then
            nAmount = 0
        End If

        ldblCurAmount = nAmount
        If nCurrency > 0 And nCoverCur > 0 Then
            If nCurrency <> nCoverCur Then
                Call lclsConvert.Convert(0, nAmount, nCoverCur, nCurrency, dValdate, 0)
                ldblCurAmount = lclsConvert.pdblResult
            End If
        End If

        If lintTyp = 1 Then
            If nCurrency = 1 Then
                ldblCurAmount_Aux = Decimal.Round(ldblCurAmount)
            Else
                ldblCurAmount_Aux = ldblCurAmount
            End If
            If nCoverCur = 1 Then
                nAmount_Aux = Decimal.Round(nAmount)
            Else
                nAmount_Aux = nAmount
            End If
            '+ valor con formato 
            Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value = '" & mobjValues.TypeToString(nAmount_Aux, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
            '+ valor convertido
            Response.Write("top.fraFolder.document.forms[0].tcnAmount.value = '" & mobjValues.TypeToString(ldblCurAmount_Aux, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
            Response.Write("top.fraFolder.document.forms[0].hddtcnAmount.value = '" & mobjValues.TypeToString(ldblCurAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")

        Else
            'Response.Write("alert('lintTyp:" & lintTyp & "');")    
            'Response.Write("alert('nCurrency:" & nCurrency & "');")
            'Response.Write("alert('nCoverCur:" & nCoverCur & "');")    
            'Response.Write("alert('nAmount:" & nAmount & "');")        
            'Response.Write("alert('ldblCurAmount:" & ldblCurAmount & "');")

            If nCoverCur = 1 Then
                nAmount_Aux = Decimal.Round(nAmount)
            Else
                nAmount_Aux = nAmount
            End If

            If nCurrency = 1 Then
                ldblCurAmount_Aux = Decimal.Round(ldblCurAmount)
            Else
                ldblCurAmount_Aux = ldblCurAmount
            End If
            '+ valor con formato 
            Response.Write("top.fraFolder.document.forms[0].tcnAmount.value = '" & mobjValues.TypeToString(nAmount_Aux, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
            Response.Write("top.fraFolder.document.forms[0].hddtcnAmount.value = '" & mobjValues.TypeToString(nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
            '+ valor convertido
            Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value = '" & mobjValues.TypeToString(ldblCurAmount_Aux, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
        End If

        If nTax <> 0 And nTax <> eRemoteDB.Constants.intNull Then
            If lintTyp = 1 Then
                ldblCurAmount = ldblCurAmount + ldblCurAmount * nTax / 100
            Else
                ldblCurAmount = nAmount + nAmount * nTax / 100
            End If
        Else
            If lintTyp = 1 Then
                ldblCurAmount = ldblCurAmount
            Else
                ldblCurAmount = nAmount
            End If
        End If

        If nCurrency = 1 Then
            ldblCurAmount = Decimal.Round(ldblCurAmount)
        End If


        Response.Write("top.fraFolder.document.forms[0].tcnAmountPayCover.value = '" & mobjValues.TypeToString(ldblCurAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")


        'UPGRADE_NOTE: Object lclsConvert may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsConvert = Nothing
    End Sub


    '--------------------------------------------------------------------------------------------
    Private Sub InsCalSI008()
        '--------------------------------------------------------------------------------------------
        Dim ldblAmount As Double
        Dim lclsCl_cover As eClaim.Cl_cover
        lclsCl_cover = new eClaim.Cl_cover
        Dim lclsClaim As eClaim.Claim
        lclsClaim = new eClaim.Claim
        With mobjValues
            If Session("nPay_Type") = 2 Then
                If lclsCl_cover.InsCalSi008(.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nGroup_insu"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString("nDepreciateamount"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nDepreciatebase"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nDepreciaterate"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nFra_amount"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nAmountPayedCover"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nAmountPayCover"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sRASA_routine"), Request.QueryString("sOrigin")) Then
                    Response.Write("top.fraFolder.document.forms[0].tcnRasa.value = '" & mobjValues.TypeToString(lclsCl_cover.nRasa, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnFra_amount.value = '" & mobjValues.TypeToString(lclsCl_cover.nFra_amount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].hddRasaAnnual.value = '" & mobjValues.TypeToString(lclsCl_cover.nRasaAnnual, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnDepreciatebase.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciatebase, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnDepreciaterate.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciaterate, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnDepreciateamount.value = '" & mobjValues.TypeToString(lclsCl_cover.nDepreciateamount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnDDR.value = '" & mobjValues.TypeToString((lclsCl_cover.nDepreciateamount + lclsCl_cover.nFra_amount + lclsCl_cover.nRasa), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount_Paycov.value = '" & mobjValues.TypeToString(Request.QueryString("nAmountPayCover"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                    Response.Write("top.fraFolder.document.forms[0].tcnAmount.value = '" & mobjValues.TypeToString(Request.QueryString("nAmountPayCover"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                End If
            Else
                Response.Write("top.fraFolder.document.forms[0].tcnAmount.value = '" & mobjValues.TypeToString(Request.QueryString("nAmountPayCover"), eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")

            End if
        End With
    End Sub

    Public Sub InsApplyDDR()
        Dim lclsT_PayCla As New eClaim.T_PayCla
        With mobjValues
            If lclsT_PayCla.InsApplyDDR(.StringToType(Request.QueryString("nClaim"), Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nCase_Num"), Values.eTypeData.etdDouble), .StringToType(Request.QueryString("nDeman_Type"), Values.eTypeData.etdDouble), Request.QueryString("sApplyDDR")) Then

                Response.Write("top.fraFolder.insSubmitPage();")

            End If
        End With
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42agency
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"
%>
<html>
<HEAD>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

</HEAD>
<body>
	<FORM NAME="ShowValue">
	</FORM>
</body>
</html>
<%Response.Write("<script>")

    Select Case Request.QueryString("Field")

        Case "Exchange"
            Call insShowExchange()
        Case "Exchange_1"
            Call insShowExchange_1()
        Case "Cover"  
            Call insParamToConcept()
        Case "PayType"
            Call insDeletePayCla()
        Case "Currency"
            Call insDeletePayCla()
        Case "Client"
            Call insExpandCodeClient()
        Case "Role"
            Call insReaClientRole()
        Case "DP051_Claim"
            Call insDP051_Claim()
        Case "cbeZone"
            Call insProvider()
        Case "Claim"
            Call insSI008_K()
        Case "Demandant"
            Call insShowDemandat()
        Case "ServiceOrder"
            Call ShowServiceOrderData()
        Case "Client_rep"
            Call insReaClient_rep()
        Case "SI008"
            Call insSI008_value()
        Case "Premium"
            Call insShowPremium()
        Case "AmountPay"
            Call InsCalAmountPay()
        Case "Agency"
            Call InsCalAgencyPay()
        Case "CalSi008"
            Call InsCalSI008()
        Case "insApply_DDR"
            Call InsApplyDDR()
            Call insSI008_value()
    End Select

    Response.Write(mobjValues.CloseShowDefValues(Request.QueryString("sFrameCaller")))
    Response.Write("</script>")

    'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
Call mobjNetFrameWork.FinishPage("showdefvalues")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>