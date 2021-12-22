<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

    Dim mlngExternalConcept As Integer
    Dim mlngCash_id As Integer
    Dim mlngVoucher As Integer
    Dim mstrLocation As String
    Dim mstrQueryString As String
    Dim mstrkey_AG004 As String
    Dim lintString As Integer
    Dim lstrError As String=String.Empty

    Dim mstrErrors As String
    Dim mobjValues As eFunctions.Values
    Dim mobjCash_mov As eCashBank.Cash_mov
    Dim mobjCheque As eCashBank.Cheque
    Dim mobjProf_ords As eClaim.Prof_ords
    Dim mobjProf_ord As eClaim.Prof_ord
    Dim mstrScript As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid
    Dim mclsCL_Cover As eClaim.Cl_Cover
    Dim mcolCL_Covers As eClaim.CL_Covers
    '% insvalCashBank: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------------------------------
    Function insvalCashBank() As String
        Dim lngCase_num As String
        '--------------------------------------------------------------------------------------------------------------------

        Select Case Request.QueryString.Item("sCodispl")

            '+ OP001: Entrada de dinero en caja.
            Case "OP001"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    If Not IsNothing(Request.Form.Item("cbeCaseNumber")) Then
                        lngCase_num = Mid(Request.Form.Item("cbeCaseNumber"), 1, InStr(1, Request.Form.Item("cbeCaseNumber"), "/") - 1)
                    Else
                        lngCase_num = Request.Form.Item("tcnCaseNumber")
                    End If

                    insvalCashBank = mobjCash_mov.insValOP001("OP001", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeMovtype"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valCurrAcc"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("cbeBussiType"), Request.Form.Item("tctDocNumbe"), mobjValues.StringToType(Request.Form.Item("tcdDocDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valAccBank"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCardType"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctCardNum"), mobjValues.StringToType(Request.Form.Item("tcdCardExpir"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("dtcClient"), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valCompanyCR"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContract"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nCashNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcdValordate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCurrencying"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmounting"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCod_Agree"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valBank_agree"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdDateCollect"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeChequelocat"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeInWay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeDocSupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnFolioSupport"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnFinancInt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lngCase_num, eFunctions.Values.eTypeData.etdDouble, True))
                End If

            Case "OP002"
                If Request.QueryString.Item("nMainAction") <> "401" Then
                    If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                        insvalCashBank = mobjCash_mov.InsValOP002_K(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctDepositNum"), mobjValues.StringToType(Request.Form.Item("valAccCash"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnCash"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("optToDeposit"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("optSelection"), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdInteger))

                    Else
                        insvalCashBank = mobjCash_mov.insValOP002(mobjValues.StringToType(Session("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nAvailable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nMinAmount"), eFunctions.Values.eTypeData.etdDouble))

                    End If
                Else
                    insvalCashBank = vbNullString
                End If

            Case "OP06-1", "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    If Request.QueryString.Item("nMainAction") <> "402" Then
                        insvalCashBank = mobjCheque.insValOP006(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbePayOrderTyp"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valConcept_Enabled"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("dtcBenef"), Request.Form.Item("dtcInterm"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valAccount"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("dtcAccountHolder"), mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("valBankAccount"))

                    Else
                        insvalCashBank = vbNullString
                    End If
                End If

            Case Else
                insvalCashBank = "insvalCashBank: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostSequence: Se realizan las actualizaciones a las tablas
    '---------------------------------------------------------------------------------------------------------------------
    Private Function insPostCashBank() As Boolean
        Dim lintCase_num As String
        Dim lstrCase() As String
        Dim lstrFirstCase As String
        Dim lintDeman_type As String
        Dim lstrMessage As String
        '---------------------------------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim mobjCash_mov As eCashBank.Cash_mov
        mobjCash_mov = New eCashBank.Cash_mov
        Dim mobjCash_bank As eCashBank.Curr_acc
        mobjCash_bank = New eCashBank.Curr_acc


        lblnPost = False

        Dim lclsAgent As eAgent.Loans_int
        Select Case Request.QueryString.Item("sCodispl")

            '+ OP001: Entrada de dinero en caja.
            Case "OP001"
                lstrFirstCase = Request.Form.Item("cbeCaseNumber")

                If lstrFirstCase <> vbNullString Then
                    lstrCase = lstrFirstCase.Split("/")
                    lintCase_num = lstrCase(0)
                    lintDeman_type = lstrCase(1)
                End If

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition) Then
                            lblnPost = mobjCash_mov.InsPostOP001(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnTransac"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeMovtype"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeCurrencying"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valCurrAcc"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("cbeBussiType"), .Form.Item("tctDocNumbe"), mobjValues.StringToType(.Form.Item("tcdDocDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valAccBank"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCardType"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("tctCardNum"), mobjValues.StringToType(.Form.Item("tcdCardExpir"), eFunctions.Values.eTypeData.etdDate), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCompanyCR"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContract"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nCashNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcdValordate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnAmounting"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCod_Agree"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valBank_agree"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcdDateCollect"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeChequelocat"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeInWay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeTypeDocSupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnFolioSupport"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnFinancInt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(lintCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(lintDeman_type, eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeArea"), eFunctions.Values.eTypeData.etdInteger, True))
                            If lblnPost Then
                                mstrLocation = vbNullString
                                mlngCash_id = mobjCash_mov.nCash_Id
                                mlngVoucher = mobjCash_mov.nVoucher

                                If .Form.Item("cbeMovtype") = "1" Or .Form.Item("cbeMovtype") = "2" Or .Form.Item("cbeMovtype") = "5" Or .Form.Item("cbeMovtype") = "10" Or .Form.Item("cbeMovtype") = "16" Then
                                    Call insPrintDocuments()
                                End If
                            End If
                        Else
                            mstrLocation = "/VTimeNet/CashBank/CashBankSeq/OP001_K.aspx?nMainAction=" & .QueryString.Item("nMainAction") & "&nTransac=" & .Form.Item("tcnTransac") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nCash_Id=" & .Form.Item("tcncomprob") & "&nMov_type=" & .Form.Item("cbeMovtype") & "&nOffice=" & .Form.Item("cbeOffice") & "&dValDate=" & .Form.Item("tcdValorDate") & "&nOri_Curr=" & .Form.Item("cbeCurrency") & "&nOri_Amount=" & .Form.Item("tcnAmount") & "&nCurrency=" & .Form.Item("cbeCurrencying") & "&nAmount=" & .Form.Item("tcnAmounting") & "&nCompany=" & .Form.Item("cbeCompany") & "&nConcept=" & .Form.Item("valConcept") & "&nAcc_Bank=" & .Form.Item("valCurr_Acc") & "&nDocNumber=" & .Form.Item("tctDocNumbe") & "&nCreditCardNumber=" & .Form.Item("tctCardNum") & "&nCreditCardType=" & .Form.Item("cbeCardType") & "&nChequelocat=" & .Form.Item("cbeChequelocat") & "&nInputChannel=" & .Form.Item("cbeinway") & "&nBank=" & .Form.Item("cbeBank") & "&nBordereaux=" & .Form.Item("tcnBordereaux") & "&nNoteNum=" & mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble)
                            lblnPost = True
                        End If
                    End If
                End With

            Case "OP002"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nOptDeposit=" & mobjValues.StringToType(.Form.Item("OptToDeposit"), eFunctions.Values.eTypeData.etdInteger) & "&dEffecDate=" & mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate) & "&dRealEffecDate=" & mobjValues.StringToType(.Form.Item("tcdRealEffecDate"), eFunctions.Values.eTypeData.etdDate) & "&sDeposit=" & .Form.Item("tctDepositNum") & "&nAccCash=" & mobjValues.StringToType(.Form.Item("valAccCash"), eFunctions.Values.eTypeData.etdDouble) & "&nCompany=" & mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True) & "&nCashNum=" & mobjValues.StringToType(.Form.Item("tcnCash"), eFunctions.Values.eTypeData.etdDouble) & "&nChequeLocat=" & mobjValues.StringToType(.Form.Item("cbeChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True) & "&nIntermed=" & mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble) & "&nOptSelection=" & mobjValues.StringToType(.Form.Item("optSelection"), eFunctions.Values.eTypeData.etdInteger)
                        lblnPost = True
                    Else
                        If Request.QueryString.Item("nMainAction") <> "401" Then
                            lblnPost = True
                            If .QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
                                If Request.QueryString.Item("nOptDeposit") = "1" Then
                                    lblnPost = mobjCash_mov.insPostOP002(Request.QueryString.Item("sDeposit"), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), 9998, mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nAccCash"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCashNum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dRealEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("nTransac"), .Form.Item("hddEffecdate"), .Form.Item("hddSel"), mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddCashnum"), .Form.Item("hddOffice"))
                                    If lblnPost = True Then
                                        Call insPrintDocuments()
                                    End If
                                Else
                                    lblnPost = mobjCash_mov.insPostOP002(Request.QueryString.Item("sDeposit"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.GetValues("nAcc_cash").GetValue(1 - 1), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nAccCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCashNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dRealEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("nTransac"), .Form.Item("dEffecdate"), .Form.Item("hddSel"), mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddCashnum"), .Form.Item("hddOffice"))
                                End If
                            Else
                                lblnPost = mobjCash_mov.insPostOP002(Request.QueryString.Item("sDeposit"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, System.DateTime.FromOADate(eRemoteDB.Constants.intNull), mobjValues.StringToType(Request.QueryString.Item("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nAccCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCashNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dRealEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), vbNullString, vbNullString, vbNullString, mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdInteger), vbNullString)
                            End If
                            If lblnPost = True Then
                                Call insPrintDocuments()
                            End If
                        Else
                            If CDbl(Request.Form.Item("chkPrint")) = 1 Then
                                Call insPrintDocuments()
                            End If
                            lblnPost = True
                        End If
                    End If
                End With

            Case "OP06-1", "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition) And Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
                            '+Se llama a la funcion inspostPayment, que se encarga de realizar el llamado a la rutina correspondiente de pagos
                            lblnPost = insPostPayment()
                        Else
                            Response.Write("<SCRIPT>")
                            Response.Write("top.frames['fraHeader'].document.location.href=""/VTimeNet/CashBank/CashBankSeq/OP006_k.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nRequest_nu=" & Request.Form.Item("tcnRequestNu") & "&sCheque=" & Request.Form.Item("tctChequeNum") & "&nCompany=" & mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble) & "&nConcept=" & mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble) & "&sDescript=" & Request.Form.Item("tctDescript") & "&nCurrencyOri=" & mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&nAmount=" & mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble) & "&nOffice=" & mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble) & "&nOfficeAgen=" & mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble) & "&nAgency=" & mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble) & "&nCurrencyPay=" & mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdDouble) & "&nAmountpay=" & mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble) & "&nTypesupport=" & mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdDouble) & "&nDocSupport=" & mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble) & "&nTax_code=" & mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdDouble) & "&nTax_percent=" & mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble) & "&nTax_amount=" & mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble) & "&nAfect=" & mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble) & "&nExcent=" & mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble) & "&sClient=" & Request.Form.Item("dtcBenef") & "&dDat_propos=" & Request.Form.Item("tcdReqDate") & "&dLedger_dat=" & Request.Form.Item("tcdAccDate") & "&nUser_sol=" & mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble) & "&sRequest_ty=" & Request.Form.Item("cbePayOrderTyp") & "&dIssue_dat=" & Request.Form.Item("tcdChequeDate") & "&nBranch=" & mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nBranch_Led=" & mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nPolicy=" & mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) & "&nOfficepay=" & mobjValues.StringToType(Request.Form.Item("cbeOfficepay"), eFunctions.Values.eTypeData.etdDouble) & """;")
                            Response.Write("</" & "Script>")
                        End If
                    End If
                End With

                If CStr(Session("OP006_sCodispl")) = "AG004" And lblnPost Then
                    lclsAgent = New eAgent.Loans_int
                    If lclsAgent.InsCreTmp_Agl004(Session("valIntermedia"), Session("cbeLoanId")) Then
                        mstrkey_AG004 = lclsAgent.sKey
                        Call insPrintDocuments()
                    End If

                    lclsAgent = Nothing
                End If

                If mobjValues.insGetSetting("Active", "No", "ExtensionSTS") = "Yes" Then
                    If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition) And Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And lblnPost Then
                        Dim lclsCheque = New eCashBank.Cheque
                        If mlngExternalConcept <= 0 Then
                            mlngExternalConcept = mobjValues.StringToType(Request.Form.Item("valConcept"),eFunctions.Values.eTypeData.etdDouble)
                        End If
                        'cls.NotifyNewCheque("CORPVIDA", "1", "Principal", "21566593", "3", "Gilmer", "21566593", "20/08/2013", "Deposito", "001", "000116565841", "Corriente", "", 100, 1, "Prueba")
                        lclsCheque.insNotificationNewCheque("CORPVIDA", "VISUALTIME", "Principal", _
                                                             Request.Form.Item("dtcBenef"), Request.Form.Item("dtcBenef_Digit"), Request.Form.Item("lblBenefname"), _
                                                             String.Empty, Request.Form.Item("tcdReqDate"), Request.Form.Item("cbePayOrderTyp"), _
                                                             mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valBankAccount"), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdDouble), _
                                                             mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrencyPay"), eFunctions.Values.eTypeData.etdDouble), _
                                                             mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), _
                                                             Request.Form.Item("tctDescript"), mlngExternalConcept, 0, mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctChequeNum"), 0, _
                                                             mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger, True))

                        Response.Write("<SCRIPT>alert("" " & lclsCheque.sMessage_sts  & """);</" & "Script>")

                        'Response.Write("<SCRIPT>")
                        'Response.Write("callwebservice(" & Request.Form.Item("tcnRequestNu") & ");")
                        'Response.Write("</" & "Script>")
                    End If
                End If

        End Select
        mobjCash_mov = Nothing
        insPostCashBank = lblnPost
    End Function

    '% insPostPayment: se realizan las actualizaciones correspondientes a la orden de pago
    '--------------------------------------------------------------------------------------------------------------
    Private Function insPostPayment() As Boolean
        '--------------------------------------------------------------------------------------------------------------
        Dim lclsPostPayment As Object
        Dim lclstPayCla As eClaim.T_PayCla
        Dim lclsPolicyTra As Object
        Dim lclsPremium_mo As eCollection.Premium_mo
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsErrors As eFunctions.Errors
        Dim ldtmEffecdate As String
        Dim lstrCodispl As String
        Dim lstrMessage As String
        Dim lblnResult As Boolean
        Dim lintCount As Object
        Dim lstrRequest As String
        Dim nClaim As Double
        Dim nIdCasuality As Integer
        Dim nPolicy As Double
        Dim nCover As Integer
        Dim mobjProf_ords As New eClaim.Prof_ords
        Dim mobjProf_ord As New eClaim.Prof_ord
        Dim lcolReport_prod As eProduct.report_prods
        Dim lclsReport_prod As eProduct.report_prod
        Dim mobjDocuments As eReports.Report
        Dim lclsProduct As eProduct.Product
        mobjDocuments = New eReports.Report

        ldtmEffecdate = mobjValues.StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate)

        Dim mobjCurr_acc As eCashBank.Curr_acc
        Dim lclsT_PayCla As eClaim.T_PayCla
        Dim mobjCollectionTra As eCollection.ColformRef
        Dim lclsClaim_his As eClaim.Claim_his
        Select Case Session("OP006_sCodispl")
            Case "OP091"
                With Request
                    mobjCurr_acc = New eCashBank.Curr_acc
                    mlngExternalConcept = 1016
                    insPostPayment = mobjCurr_acc.insPostOP091(mobjValues.StringToType(.QueryString.Item("nTyp_Acco"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("sType_Acc"), .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRemNum"), eFunctions.Values.eTypeData.etdDouble), 2, mobjValues.StringToType(.Form.Item("tcnRequestnu"), eFunctions.Values.eTypeData.etdDouble), "0", mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProcess"), eFunctions.Values.eTypeData.etdInteger), 0, mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("tctDescript"), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("nPayOrderTyp"), 1, mobjValues.StringToType(ldtmEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), 9998, .QueryString.Item("sClient"), eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmounttotal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnTax_code"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficepay"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjCurr_acc = Nothing

            Case "SI008"
                lclstPayCla = New eClaim.T_PayCla
                lclsProduct = New eProduct.Product
                lclsProduct.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

                insPostPayment = True
                lclstPayCla.nCashNum = mobjValues.StringToType(Session("nCashNum"), eFunctions.Values.eTypeData.etdInteger)
                If Session("nBranch") <> 8 Then
                    mlngExternalConcept = 1001
                Else
                    mlngExternalConcept = 1000
                End If
                insPostPayment = lclstPayCla.InsPostSI008(Session("nClaim"), Session("nCase_Num"), Session("nDeman_type"), Session("OP006_nRole"), Session("SI008_valClient"), Session("SI008_CBEPAYFORM"), Session("OP006_nPay_type"), Session("OP006_nServ_order"), Session("SI008_Cessi_coi"), mobjValues.StringToType(Session("OP006_nInvoice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("OP006_nCurrencypay"), Session("OP006_nExchange"), Session("SI008_nAmountPay"), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctChequeNum"), 0, mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), Session("OP006_sBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbePayOrderTyp"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdInteger), "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", mobjValues.StringToType(Session("OP006_nDoc_type"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Session("SI008_tcdBillDate"), eFunctions.Values.eTypeData.etdDate), , , , , mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), Session("SI008_Cessi_rei"), Session("SI008_nBodereaux"), mobjValues.StringToType(Session("SI008_tcdValdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("dtcAccountHolder"), mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("valBankAccount"), mlngExternalConcept)

                Dim serverPath As String = ConfigurationManager.AppSettings("Mutual.GastosMedicos").ToString()
                If serverPath = "true" Then
                    ''Se valida la varible si es true se procede al llamado del sp de cierro
                    If insPostPayment Then
                        'Se llama al SP que finaliza el caso 

                        nCover = lclstPayCla.nCover
                        mobjValues = New eFunctions.Values
                        'mobjProf_ords.Find(CStr(Session("nClaim")))
                        mobjProf_ords.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                        'mobjProf_ords.Find(Session("nClaim"))              

                        nCover = mobjProf_ord.nCase_Num
                        nIdCasuality = mobjProf_ord.nServ_Order

                        nPolicy = Session("nPolicyGM")
                        If nPolicy = eRemoteDB.dblNull Then
                            nPolicy = Session("nPolicy")
                            'nPolicy = Session("nPolicyGM")
                        End If
                        If nPolicy = 0 Then
                            nPolicy = Session("nPolicy")
                        End If

                        nClaim = Session("nClaim")

                        ''
                        If mobjProf_ords.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)) Then
                            For lintCount = 1 To mobjProf_ords.Count
                                mobjProf_ord = mobjProf_ords.Item(lintCount)

                                With mobjGrid
                                    'Session("nServ_Order_GM2") = mobjProf_ord.nServ_Order
                                    nIdCasuality = mobjProf_ord.nServ_Order

                                End With
                            Next
                        End If
                        ''
                        'nIdCasuality = Session("nIdCasuality")
                        'nIdCasuality = nClaim

                        nCover = Session("nConver")
                        If nCover = eRemoteDB.intNull Then
                            nCover = Session("nCoverGM")
                        End If
                        If nCover = 0 Then
                            nCover = Session("nCoverGM")
                        End If
                        If nCover = 0 Then
                            nCover = Session("nCover")
                        End If
                        If nCover = 0 Then
                            nCover = 1004
                        End If

                        Call lclstPayCla.insPostCloseClaim(nClaim, nIdCasuality, nPolicy, nCover)
                    End If
                End If

                If CStr(Session("SI738_sCodispl")) <> "" Then
                    Session("OP006_sCodispl") = "SI738"
                    Session("SI738_sCodispl") = ""
                Else
                    Session("OP006_sCodispl") = "SI008_K"
                End If
                lclstPayCla = Nothing
                With mobjDocuments
                    If insPostPayment Then
                        lcolReport_prod = New eProduct.report_prods
                        If lcolReport_prod.FindReport_prod_By_Transac("2", _
                                                                        Session("nBranch"), _
                                                                        Session("nProduct"), _
                                                                        0, _
                                                                        0, _
                                                                        1, _
                                                                        4, _
                                                                        Session("dEffecdate"), _
                                                                        True) Then

                            For Each lclsReport_prod In lcolReport_prod
                                .Reset()
                                .sCodispl = "SIL006"
                                .ReportFilename = lclsReport_prod.sReport
                                Select Case lclsProduct.sBrancht
                                    '+SOAP
                                    Case "6"
                                        .setStorProcParam(1, Session("nClaim"))
                                        .setStorProcParam(2, Session("nCase_Num"))
                                        .setStorProcParam(3, Session("nDeman_type"))
                                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdLong))
                                        .setStorProcParam(5, Session("SI008_valClient"))
                                        .setStorProcParam(6, Session("nUserCode"))
                                    Case Else
                                        .setStorProcParam(1, Session("nClaim"))
                                        .setStorProcParam(2, Session("nUserCode"))
                                End Select
                                Response.Write(.Command)
                            Next
                        End If
                    End If
                End With
            '+ AG004: Anticipos y préstamos de intermediarios
            Case "AG004"
                lclsPostPayment = New eAgent.Loans_int
                insPostPayment = mobjCheque.insPostOP006("OP06-1", mobjValues.StringToType(Request.QueryString.Item("nMainaction_op006"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("cbePayOrderTyp"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valAccount"), eFunctions.Values.eTypeData.etdInteger))

                If insPostPayment Then
                    insPostPayment = lclsPostPayment.insPostAG004(mobjValues.StringToType(Request.QueryString.Item("nMainaction_op006"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("valIntermedia"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("cbeLoanId"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("OP006_nMonthly"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("OP006_dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("OP006_nPay_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nPercent_ant"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nLoanType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("OP006_nPayOrder"), Session("OP006_nLoanSta"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nMode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_tcnCommBase"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("OP006_nPercent"), eFunctions.Values.eTypeData.etdDouble))
                End If

            Case "SI773"
                lclsT_PayCla = New eClaim.T_PayCla
                mlngExternalConcept = 1001
                If Request.QueryString.Item("sCodispl") <> "OP06-6" Then
                    insPostPayment = lclsT_PayCla.insPostSI773(Session("OP006_nClaim"), Session("OP006_nCase_Num"), Session("OP006_nDeman_type"), Session("OP006_sClient"), Session("OP006_nId"), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate, True), Request.Form.Item("cbePayOrderTyp"), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUserCode"), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("OP006_nOffice_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), Session("OP006_sKey"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), Session("OP006_nOfficeAgen"), Session("OP006_nAgency"), mlngExternalConcept)
                    lclsT_PayCla = Nothing
                Else
                    insPostPayment = True
                    Session("OP006_sCodispl") = Request.QueryString.Item("sCodispl")
                    Session("TypTransf") = "2"
                    Session("nAmountTransf") = Request.Form.Item("tcnAmount")
                    Session("nOriAccount") = Request.Form.Item("valAccountNum")
                    Session("nCurrencyOri") = Request.Form.Item("cbeCurrency")
                    Session("sClientName") = Request.Form.Item("dtcBenef")
                    Session("OP006_nCompany") = mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_nOffice") = mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_nConcept") = mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_sDescript") = Request.Form.Item("tctDescript")
                    Session("OP006_nCurrencyOri") = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_nAmountPay") = mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_nCurrencyPay") = mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("OP006_sRequest_ty") = Request.Form.Item("cbePayOrderTyp")
                    Session("OP006_nRequest_nu") = mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True)
                End If
                Session("OP006_sCodispl") = "SI773"
            Case "CO009"
                lclsPremium_mo = New eCollection.Premium_mo
                lclsProduct = New eProduct.Product
                Call lclsProduct.FindProduct_li(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToDate(Session("tcdDate")))

                If lclsProduct.nProdClas = 4 Then
                    If lclsProduct.sApv = "1" Then
                        mlngExternalConcept = 1011
                    Else
                        mlngExternalConcept = 1013
                    End If
                Else
                    If mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) = 7 Or _
                        mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) = 1 Then

                        If mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdInteger) = 0 Then
                            mlngExternalConcept = 1014
                        Else
                            mlngExternalConcept = 1015
                        End If
                    Else
                        mlngExternalConcept = 1012
                    End If
                End If
                lclsProduct = Nothing
                insPostPayment = mobjCheque.insPostOP006("OP06-1", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("cbePayOrderTyp"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, Session("OP006_sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnAmounttotal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("dtcAccountHolder"), mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("valBankAccount"), mlngExternalConcept)

                If insPostPayment Then
                    insPostPayment = lclsPremium_mo.insPostCO009(mobjValues.StringToDate(Session("tcdDate")), "2", mobjValues.StringToType(Session("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), Session("chkRelAll"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("optTypOper"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.dtmNull, "")

                    If insPostPayment Then
                        lclsErrors = New eFunctions.Errors
                        lstrError = lclsErrors.ErrorMessage("CO009_K", 4327, , , , True)
                        lintString = InStr(1, lstrError, "Err.")
                        If lintString > 0 Then
                            lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                        End If
                        Response.Write(lstrError)
                        lclsErrors = Nothing
                    End If

                End If
                lclsPremium_mo = Nothing
            Case "CO788"
                mobjCollectionTra = New eCollection.ColformRef
                mlngExternalConcept = 1003

                insPostPayment = mobjCheque.insPostOP006("OP06-1", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("cbePayOrderTyp"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger, True), eRemoteDB.Constants.intNull, Session("OP006_sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnAmounttotal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), "", 0, 0, "", mlngExternalConcept)

                If insPostPayment Then
                    insPostPayment = mobjCollectionTra.InsPostCO788(mobjValues.StringToType(Session("dDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTypeDoc"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("nNumDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDraft"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dDateIncrease"), eFunctions.Values.eTypeData.etdDate), Session("sClient"), mobjValues.StringToType(Session("OptDev"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("OptDocRev"), eFunctions.Values.eTypeData.etdLong), Session("nUserCode"), mobjValues.StringToType(Session("nSequence"), eFunctions.Values.eTypeData.etdLong))

                    If insPostPayment Then
                        lclsErrors = New eFunctions.Errors
                        lstrError = lclsErrors.ErrorMessage("CO788_K", 4327, , , , True)
                        lintString = InStr(1, lstrError, "Err.")
                        If lintString > 0 Then
                            lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                        End If
                        Response.Write(lstrError)
                        lclsErrors = Nothing
                    End If
                End If
                mobjCollectionTra = Nothing
            Case Else
                '+ Si se trata de una transferencia no se efectua el post de la OP006
                If Request.Form.Item("cbePayOrderTyp") = "4" Then
                    insPostPayment = True
                    If CStr(Session("OP006_sCodispl")) = "VI009" Or CStr(Session("OP006_sCodispl")) = "VI011" Then
                        mstrQueryString = "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dRescdate=" & Request.QueryString.Item("dRescdate") & "&sSurrType=" & Request.QueryString.Item("sSurrType") & "&sProcessType=" & Request.QueryString.Item("sProcessType") & "&sRequest=" & Request.QueryString.Item("sRequest") & "&sSurrPayWay=" & Request.QueryString.Item("sSurrPayWay") & "&nSurrAmount=" & Request.QueryString.Item("nSurrAmount") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&sClient=" & Request.QueryString.Item("sClient") & "&nBranchPay=" & Request.QueryString.Item("nBranchPay") & "&nProductPay=" & Request.QueryString.Item("nProductPay") & "&nPolicyPay=" & Request.QueryString.Item("nPolicyPay") & "&nCertifPay=" & Request.QueryString.Item("nCertifPay") & "&nProponum=" & Request.QueryString.Item("nProponum") & "&nBalance=" & Request.QueryString.Item("nBalance") & "&nOperat=" & Request.QueryString.Item("nOperat") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&nAmotax=" & Request.QueryString.Item("nAmotax") & "&nInterest=" & Request.QueryString.Item("nInterest") & "&sProcessType=" & Request.QueryString.Item("sProcessType") & "&nPayOrderTyp=" & mobjValues.StringToType(Request.Form.Item("cbePayOrderTyp"), eFunctions.Values.eTypeData.etdDouble) & "&nAmount=" & mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble) & "&nRequestNu=" & mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True)
                    End If
                    Session("TypTransf") = "2"
                    Session("nAmountTransf") = Request.Form.Item("tcnAmount")
                    Session("nOriAccount") = Request.Form.Item("valAccountNum")
                    Session("nCurrencyOri") = Request.Form.Item("cbeCurrency")
                    Session("sClientName") = Request.Form.Item("dtcBenef")
                Else
                    If CStr(Session("OP006_sCodispl")) = "CA099A" Or CStr(Session("OP006_sCodispl")) = "VI009" Or CStr(Session("OP006_sCodispl")) = "VI011" Or CStr(Session("OP006_sCodispl")) = "VI7000" Or CStr(Session("OP006_sCodispl")) = "VI7004" Or CStr(Session("OP006_sCodispl")) = "CA028" Or CStr(Session("OP006_sCodispl")) = "SI777" Then
                        lstrCodispl = "OP06-1"
                    Else
                        lstrCodispl = Request.QueryString.Item("sCodispl")
                        Session("OP006_sCodispl") = "OP06-1"
                    End If

                    If CStr(Session("OP006_sCodispl")) = "CA099A" Then


                        lclsProduct = New eProduct.Product

                        Call lclsProduct.FindProduct_li(mobjValues.StringToType(Session("OP006_nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                             mobjValues.StringToType(Session("OP006_nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                             mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True))

                        If lclsProduct.nProdClas = 4 Then
                            If lclsProduct.sApv = "1" Then
                                mlngExternalConcept = 1017
                            Else
                                mlngExternalConcept = 1018
                            End If
                        Else
                            If mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) = 7 Or _
                                mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) = 1 Then

                                If mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdInteger) = 0 Then
                                    mlngExternalConcept = 1021
                                Else
                                    mlngExternalConcept = 1020
                                End If
                            Else
                                mlngExternalConcept = 1019
                            End If
                        End If
                        lclsProduct = Nothing

                        mlngExternalConcept = 1004


                    ElseIf CStr(Session("OP006_sCodispl")) = "VI7000" Then
                        If Request.QueryString("sSurrTot") = "1" Then
                            mlngExternalConcept = 1005
                        Else
                            mlngExternalConcept = 1006
                        End If
                    ElseIf CStr(Session("OP006_sCodispl")) = "VI009" Then
                        If Request.QueryString("sSurrType") = "1" Then
                            mlngExternalConcept = 1007
                        Else
                            mlngExternalConcept = 1008
                        End If
                    ElseIf CStr(Session("OP006_sCodispl")) = "VI7004" Then

                        If Request.QueryString.Item("nSurrReas") = "2" Then
                            If Request.QueryString("sSurrTot") = "1" Then
                                mlngExternalConcept = 1008
                            Else
                                mlngExternalConcept = 1007
                            End If
                        Else
                            If Request.QueryString("sSurrTot") = "1" Then
                                mlngExternalConcept = 1009
                            Else
                                mlngExternalConcept = 1010
                            End If
                        End If
                    End If
                    insPostPayment = mobjCheque.insPostOP006(lstrCodispl, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctChequeNum"), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeCurrencypay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnAmountpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnDoc_support"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTax_code"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnTax_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAfect"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnExcent"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcBenef"), mobjValues.StringToType(Request.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdAccDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("valReqUser"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("cbePayOrderTyp"), mobjValues.StringToType(Request.Form.Item("tcdChequeDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valBranch_Led"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, Session("OP006_sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnAmounttotal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("dtcAccountHolder"), mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeAcc_Type"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("valBankAccount"), mlngExternalConcept)
                    '+ Se realizan las llamdas a las transacciones tras crear/actualizar cheques
                    If insPostPayment Then
                        Select Case Session("OP006_sCodispl")
                            '+ Tratamiento de cotizacion propuestas	    
                            Case "CA099A"
                                lclsPostPayment = New ePolicy.TConvertions
                                '+ Se actualiza el indicador de orden pago generada	        
                                Call lclsPostPayment.InsUpdPayOrder(Session("OP006_sCertype"), mobjValues.StringToType(Session("OP006_nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nCertif"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(Session("OP006_nConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                            '+ Se generan los datos asociados al préstamo/anticipo
                            Case "VI011"

                                lclsPolicyTra = New ePolicy.Loans
                                With Request
                                    insPostPayment = lclsPolicyTra.insPostVI011(.QueryString("sCodisplOri"), eFunctions.Menues.TypeActions.clngAcceptdatafinish, "2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sProcessType"), mobjValues.StringToType(.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInterest"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, .QueryString("sClient"), mobjValues.StringToType(.Form.Item("cbePayOrderTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAmoTax"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, vbNullString, Session("sTypeCompanyUser"), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True), Session("SessionID"), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSurrVal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMaxAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLoans"), eFunctions.Values.eTypeData.etdDouble))
                                    If insPostPayment Then
                                        lclsGeneral = New eGeneral.GeneralFunction
                                        lstrMessage = lclsGeneral.insLoadMessage(55907) & " Nro.: " & lclsPolicyTra.nCode
                                        Response.Write("<SCRIPT>alert(""Men. 55907: " & lstrMessage & """);</" & "Script>")
                                        lclsGeneral = Nothing
                                        ' Call insPrintDocuments()
                                    End If
                                End With

                            '+ Se generan los datos asociados al rescate
                            Case "VI009"
                                With Request
                                    lclsPostPayment = New ePolicy.ValPolicyTra

                                    Call lclsPostPayment.InsPostVI009("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dRescdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("sSurrType"), .QueryString("sProcessType"), .QueryString("sRequest"), Session("sSurrPayWay"), mobjValues.StringToType(.QueryString.Item("nSurrAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.QueryString.Item("nBranchPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProductPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicyPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertifPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBalance"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeOfficepay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnRequestnu"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, .QueryString("sAnulReceipt"), mobjValues.StringToType(.QueryString.Item("hddTaxSurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddSurrValue_Tax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcdPaymentDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnSurrCostPar"), eFunctions.Values.eTypeData.etdDouble))

                                    'mobjValues.StringToType(.QueryString("sSurrPayWay"),eFunctions.Values.eTypeData.etdDouble), 
                                    If .QueryString.Item("sReport") = "1" Then
                                        Call insPrintDocuments()
                                    End If
                                End With

                            '**+ VI7000: Surrender of Policies (Unit Linked).                        
                            '+ VI7000: Rescate de pólizas (Unit Linked).
                            Case "VI7000"
                                lclsPolicyTra = New ePolicy.ValPolicyTra
                                If IsNothing(Request.Form.Item("tcnProponum")) Then
                                    lstrRequest = "2"
                                Else
                                    lstrRequest = "1"
                                End If

                                With Request
                                    Call lclsPolicyTra.InsPostVI7000(.QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("sSurrTot"), mobjValues.StringToType(.QueryString.Item("nSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("cbePmtOrd"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClientEnt"), mobjValues.StringToType(.QueryString.Item("nEntity"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRequestnu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble), lstrRequest, eRemoteDB.Constants.dtmNull, vbNullString, mobjValues.StringToType(.QueryString.Item("norigin_apv"), eFunctions.Values.eTypeData.etdDouble), Session("sInd_Insur"))
                                End With
                                '+Se invoca el reporte de finiquito                            
                                Call insPrintDocuments()

                            '+ VI7004: Rescate de pólizas (APV).
                            Case "VI7004"

                                lclsPolicyTra = New ePolicy.ValPolicyTra
                                If IsNothing(Request.Form.Item("tcnProponum")) Then
                                    lstrRequest = "2"
                                Else
                                    lstrRequest = "1"
                                End If

                                With Request
                                    Call lclsPolicyTra.InsPostVI7004(.QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("sSurrTot"), mobjValues.StringToType(.QueryString.Item("nSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("cbePmtOrd"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClientEnt"), mobjValues.StringToType(.QueryString.Item("nEntity"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRequestnu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble), lstrRequest, eRemoteDB.Constants.dtmNull, vbNullString, mobjValues.StringToType(.QueryString.Item("norigin_apv"), eFunctions.Values.eTypeData.etdDouble), Session("sInd_Insur"))
                                End With
                                '+Se invoca el reporte de finiquito                            
                                Call insPrintDocuments()

                            Case "SI777"
                                lclsClaim_his = New eClaim.Claim_his
                                Call lclsClaim_his.reaCall_InsPostSI777(Session("lstrclaim"), mobjValues.StringToType(Session("nStatus_Payment"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("lstrmovement"), Session("lstrconsecutive"), Request.Form.Item("tctChequeNum"), Session("lstrtransactio"), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Session("schek_rel"), Session("mstrKey"), mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdDouble, True))
                                lclsClaim_his = Nothing

                                '									If lblnPost = True Then
                                Call insPrintDocuments()
                            '									End If


                            Case "CA028"
                                lclsPolicyTra = New ePolicy.TDetail_pre
                                With Request
                                    lblnResult = lclsPolicyTra.insPostCA028(.QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("dExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypeReceipt"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sOrigReceipt"), Session("nUsercode"), Session("OptExecute"), .QueryString("sAnulReceipt"), .QueryString("sKey"), .QueryString("sAdjust"), mobjValues.StringToType(.QueryString.Item("nAdjReceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypePay"), eFunctions.Values.eTypeData.etdLong, True), .QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranchPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProductPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicyPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertifPay"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"))
                                End With

                                If lblnResult And Request.QueryString.Item("sAnulReceipt") <> "1" And mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                    '+ Se envia alerta con número de recibo generado 
                                    lclsGeneral = New eGeneral.GeneralFunction
                                    lstrMessage = lclsGeneral.insLoadMessage(5064) & " con Nro.: " & lclsPolicyTra.nReceipt
                                    Response.Write("<SCRIPT>alert(""Men. 5064: " & lstrMessage & """);</" & "Script>")
                                    lclsGeneral = Nothing
                                End If
                        End Select
                    End If
                End If
        End Select
        lclsPostPayment = Nothing
        lclsPolicyTra = Nothing
    End Function


    '**% insPrintDocuments: Document printing
    '%   insPrintDocuments: Impresión de los documentos
    '-----------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-----------------------------------------------------------------------------------------
        Dim mobjDocuments As eReports.Report
        Dim lobjPolicy_His As Object
        Dim mclsProduct_li As Object

        mobjDocuments = New eReports.Report
        With mobjDocuments
            Select Case Request.QueryString.Item("sCodispl")
                Case "OP002"

                    If CDbl(Request.Form.Item("chkPrint")) = 1 Then
                        '+ Si el tipo de depósito es cheque o cheque a fecha
                        If Request.QueryString.Item("nOptDeposit") = "2" Or Request.QueryString.Item("nOptDeposit") = "4" Or Request.QueryString.Item("nOptDeposit") = "1" Then
                            '+ OPL002: Listado de Cheques depositados
                            .sCodispl = "OPL002"
                            If Request.QueryString.Item("nOptDeposit") = "1" Then
                                .ReportFilename = "OPL002_a.rpt"
                            Else
                                .ReportFilename = "OPL002.rpt"
                            End If
                            .setStorProcParam(1, Request.QueryString.Item("sDeposit"))
                            .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nAccCash"), eFunctions.Values.eTypeData.etdDouble))
                            Response.Write((.Command))
                        End If
                    End If

                Case "OPL002"

                    If CDbl(Request.Form.Item("chkPrint")) = 1 Then
                        '+ OPL002: Listado de Cheques depositados
                        .sCodispl = "OPL002"
                        .ReportFilename = "OPL002.rpt"
                        .setStorProcParam(1, Session("sDeposit"))
                        .setStorProcParam(2, mobjValues.StringToType(Session("nAccCash"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write((.Command))
                    End If

                    '+ OPL102: Comprobante de cheque solicitado.
                    If mobjValues.StringToType(Session("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger) = 2 Or mobjValues.StringToType(Session("nOptDeposit"), eFunctions.Values.eTypeData.etdInteger) = 5 Then
                        .Reset()
                        .sCodispl = "OPL002"
                        .ReportFilename = "OPL102.rpt"
                        .setStorProcParam(1, Session("nOptDeposit"))
                        .setStorProcParam(2, Session("sDeposit"))
                        .setStorProcParam(3, Session("nUserCode"))
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("lblTotDeposit"), eFunctions.Values.eTypeData.etdDouble))
                        .bTimeOut = True
                        Response.Write((.Command))
                    End If

                '+ OPL022: Comprobante de ingreso a caja
                Case "OP001"
                    .Reset()
                    .sCodispl = "OPL001"
                    .ReportFilename = "OPL022.rpt"
                    .setStorProcParam(1, mlngCash_id)
                    Response.Write((.Command))

                Case Else
                    Select Case Session("OP006_sCodispl")
                        '+ VIL009: Impresión de rescate de póliza/certificado
                        Case "VI009"
                            .ReportFilename = "VI009.rpt"
                            .sCodispl = "VI009"
                            .setStorProcParam(1, "2")
                            .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                            .setStorProcParam(3, mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                            .setStorProcParam(4, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                            .setStorProcParam(5, mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                            .setStorProcParam(7, "VI009")
                            .setStorProcParam(8, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                            .setStorProcParam(9, "2")
                            Response.Write((.Command))
                        '+ VI7000: Impresión de rescate de póliza/certificado Vida no tradicional
                        Case "VI7000", "VI7004"
                            With mobjDocuments
                                lobjPolicy_His = New ePolicy.Policy
                                lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

                                If Session("OP006_sCodispl") = "VI7004" Then
                                    .ReportFilename = "VI7004_1.rpt"
                                    .sCodispl = "VI7004"
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                                    .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(8, "2")

                                Else
                                    .ReportFilename = "VI009.rpt"
                                    .sCodispl = "VI7000"
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(3, mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(4, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(5, mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                                    .setStorProcParam(7, "VI7000")
                                    .setStorProcParam(8, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .setStorProcParam(9, "2")

                                End If

                                .Merge = False
                                .nGenPolicy = 1
                                .nMovement = lobjPolicy_His.nMov_histor
                                .nForzaRep = 1
                                .nTratypep = 2
                                .nCopyPolicy = 1
                                .MergeCertype = "2"
                                .MergeBranch = Session("nBranch")
                                .MergeProduct = Session("nProduct")
                                .MergePolicy = Session("nPolicy")
                                .MergeCertif = Session("nCertif")
                                Response.Write((.Command))
                                .Reset()
                                .ReportFilename = "VI7000_PolicyValue.rpt"
                                .sCodispl = "VI7000"
                                .setStorProcParam(1, "2")
                                .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                                .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setParamField(1, "nproponum", mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setParamField(1, "sprocess", "2")
                                .Merge = False
                                .nGenPolicy = 1
                                .nMovement = lobjPolicy_His.nMov_histor
                                .nForzaRep = 1
                                .nTratypep = 2
                                .nCopyPolicy = 1
                                .MergeCertype = "2"
                                .MergeBranch = Session("nBranch")
                                .MergeProduct = Session("nProduct")
                                .MergePolicy = Session("nPolicy")
                                .MergeCertif = Session("nCertif")
                                Response.Write((.Command))
                            End With
                        '+ VI7004: Impresión de rescate de póliza/certificado APV
                        'Case "VI7004"
                        '	With mobjDocuments
                        '                       lobjPolicy_His = New ePolicy.Policy
                        '                       lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

                        '		.ReportFilename = "VI7004_1.rpt"
                        '		.sCodispl = "VI7004"
                        '		.setStorProcParam(1, "2")
                        '		.setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        '		.setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        '		.setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        '		.setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                        '		.setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                        '		.setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                        '		.setStorProcParam(8, "2")
                        '                       .Merge = False
                        '                       .nGenPolicy = 1
                        '                       .nMovement = lobjPolicy_His.nMov_histor
                        '                       .nForzaRep = 1
                        '                       .nTratypep = 2
                        '                       .nCopyPolicy = 1
                        '                       .MergeCertype = "2"
                        '                       .MergeBranch = Session("nBranch")
                        '                       .MergeProduct = Session("nProduct")
                        '                       .MergePolicy = Session("nPolicy")
                        '                       .MergeCertif = Session("nCertif")
                        '                       Response.Write((.Command))
                        '                       .Reset()
                        '                       .ReportFilename = "VI7000_PolicyValue.rpt"
                        '                       .sCodispl = "VI7004" 
                        '                       .setStorProcParam(1, "2")
                        '                       .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setStorProcParam(6, .setdate(Request.QueryString.Item("dRescdate")))
                        '                       .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setParamField(1, "nproponum", mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True))
                        '                       .setParamField(1, "sprocess", "2")
                        '                       .Merge = False
                        '                       .nGenPolicy = 1
                        '                       .nMovement = lobjPolicy_His.nMov_histor
                        '                       .nForzaRep = 1
                        '                       .nTratypep = 2
                        '                       .nCopyPolicy = 1
                        '                       .MergeCertype = "2"
                        '                       .MergeBranch = Session("nBranch")
                        '                       .MergeProduct = Session("nProduct")
                        '                       .MergePolicy = Session("nPolicy")
                        '                       .MergeCertif = Session("nCertif")
                        '                       Response.Write((.Command))
                        '	End With
                        Case "VI011"
                            .Reset()
                            .sCodispl = "VIL011"
                            .ReportFilename = "VIL011.rpt"
                            .setStorProcParam(1, "TMP" & Session("SessionID") & Session("nUsercode"))
                            Response.Write((.Command))

                        Case "AG004"
                            .ReportFilename = "AGL004.rpt"
                            .sCodispl = "AGL004"
                            .setStorProcParam(1, mstrkey_AG004)
                            Response.Write((.Command))

                        '+ Impresión de órdenes de pago aprobadas
                        Case "SI777"
                            .sCodispl = "OPL714"
                            .ReportFilename = "OPL714.rpt"
                            If CStr(Session("dInitial_date")) = "" Then
                                .setParamField(1, "Desde", "01/01/2000")
                            Else
                                .setParamField(1, "Desde", Session("dInitial_date"))
                            End If

                            If CStr(Session("dFinal_date")) = "" Then
                                .setParamField(2, "Hasta", "01/01/2000")
                            Else
                                .setParamField(2, "Hasta", Session("dFinal_date"))
                            End If
                            If Session("nPolicy") = eRemoteDB.Constants.intNull Then
                                .setParamField(3, "Poliza", "9999999999")
                            Else
                                .setParamField(3, "Poliza", Session("nPolicy"))
                            End If
                            .setStorProcParam(1, Session("mstrKey"))

                            Response.Write((.Command))
                    End Select
            End Select
        End With
        mobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1
mstrQueryString = vbNullString
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <script type="text/javascript" src="/VTimeNet/Scripts/jquery-1.6.1.min.js"></script>

</HEAD>
<BODY>
<form id="form1" runat="server">
    <script>
        function callwebservice(nRequestNum) {
            $.get("IntegrationSTS.aspx", { nRequestNum: nRequestNum }, function (data) { alert('page content: ' + data); });
        }

        //------------------------------------------------------------------------------------------
        function CancelErrors(){
        //------------------------------------------------------------------------------------------
            self.history.go(-1)
            }

        //------------------------------------------------------------------------------------------
        function NewLocation(Source,Codisp){
        //------------------------------------------------------------------------------------------
            var lstrLocation = "";
            lstrLocation += Source.location;
            lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
            Source.location = lstrLocation
        }
    //- Variable para el control de versiones
        document.VssVersion="$$Revision: 7 $|$$Date: 6/10/09 12:49p $|$$Author: Gletelier $"
    </script>

    <%mstrCommand = "&sModule=CashBank&sProject=CashBankSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")

    mobjCash_mov = New eCashBank.Cash_mov
    mobjCheque = New eCashBank.Cheque
    mobjValues = New eFunctions.Values

    '+ Si no se han validado los campos de la página
    If Request.Form.Item("sCodisplReload") = vbNullString Then
	    mstrErrors = insvalCashBank
	    Session("sErrorTable") = mstrErrors
	    Session("sForm") = Request.Form.ToString
    Else
	    Session("sErrorTable") = vbNullString
	    Session("sForm") = vbNullString
    End If

    If mstrErrors > vbNullString Then
	    Response.Write("<SCRIPT>if(typeof(top.fraHeader)!='undefined') if(typeof(top.fraHeader.mstrInSubmit)!='undefined') top.fraHeader.mstrInSubmit='2';</SCRIPT>")
	    With Response
		    .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CashBankSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		    .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		    .Write("</SCRIPT>")
	    End With
    Else
	    If insPostCashBank() Then
		    If Request.QueryString.Item("WindowType") <> "PopUp" Then
			    If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptDataAccept) Or Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptDataFinish) Then
				    If Request.QueryString.Item("sCodispl") = "OP06-2" Or Request.QueryString.Item("sCodispl") = "OP06-3" Or Request.QueryString.Item("sCodispl") = "OP06-4" Or Request.QueryString.Item("sCodispl") = "OP06-5" Or Request.QueryString.Item("sCodispl") = "OP06-6" Then
					    If CStr(Session("OP006_sCodispl")) = "SI773" Then
						    If Request.Form.Item("sCodisplReload") = vbNullString Then
							    Response.Write("<SCRIPT>top.opener.document.location.reload();</SCRIPT>")
							    Response.Write("<SCRIPT>top.close();</SCRIPT>")
						    Else
							    Response.Write("<SCRIPT>window.close();</SCRIPT>")
							    Response.Write("<SCRIPT>top.opener.top.opener.document.location.reload();</SCRIPT>")
						    End If
					    Else
						    If Request.Form.Item("cbePayOrderTyp") = "4" Then
							    If CStr(Session("OP006_sCodispl")) = "VI011" Then
								    Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/GoTo.aspx?sCodispl=OP012I" & mstrQueryString & "';</SCRIPT>")
							    Else
								    Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & """;</SCRIPT>")
							    End If
						    Else
							    If CStr(Session("OP006_sCodispl")) = "AG004" Or CStr(Session("OP006_sCodispl")) = "OP091" Or CStr(Session("OP006_sCodispl")) = "SI008_K" Or CStr(Session("OP006_sCodispl")) = "VI009" Or CStr(Session("OP006_sCodispl")) = "VI011" Or CStr(Session("OP006_sCodispl")) = "CO009" Or CStr(Session("OP006_sCodispl")) = "CO788" Or CStr(Session("OP006_sCodispl")) = "VI7000" Or CStr(Session("OP006_sCodispl")) = "VI7004" Then
								    If Request.Form.Item("sCodisplReload") = vbNullString Then
									    Response.Write("<SCRIPT>top.document.location.href=""/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & """;</SCRIPT>")
								    Else
									    Response.Write("<SCRIPT>window.close();opener.top.document.location.href=""/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & """;</SCRIPT>")
								    End If
							    Else
								    If CStr(Session("OP006_sCodispl")) = "CA099A" Then
									    If Request.Form.Item("sCodisplReload") = vbNullString Then
										    Response.Write("<SCRIPT>" & "if(typeof(top.opener.top.frames[""fraFolder""].document.forms[0].hddPay_order)!='undefined')" & "top.opener.top.frames[""fraFolder""].document.forms[0].hddPay_order.value=1;" & "</SCRIPT>")
									    Else
										    Response.Write("<SCRIPT>" & "if(typeof(top.opener.top.opener.top.frames[""fraFolder""].document.forms[0].hddPay_order)!='undefined')" & "top.opener.top.opener.top.frames[""fraFolder""].document.forms[0].hddPay_order.value=1;" & "</SCRIPT>")
									    End If
								    End If
								    If Request.Form.Item("sCodisplReload") = vbNullString Then
									    Response.Write("<SCRIPT>top.close();top.document.location.href=""/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & """;</SCRIPT>")
								    Else
									    If CStr(Session("OP006_sCodispl")) = "CA099A" Then
										    Response.Write("<SCRIPT>window.close();opener.top.close();</SCRIPT>")
									    Else
										    Response.Write("<SCRIPT>window.close();opener.top.document.location.href=""/VTimeNet/Common/GoTo.aspx?sCodispl=" & Session("OP006_sCodispl") & """;</SCRIPT>")
									    End If
								    End If
							    End If
						    End If
					    End If
				    ElseIf Request.QueryString.Item("sCodispl") = "OP06-1" Or Request.QueryString.Item("sCodispl") = "OP06-6" Then 
					    Session("OP006_sCodispl") = vbNullString
					    '+ Se mueve automáticamente a la forma de tranferencias bancarias externas
					    If CDbl(Request.Form.Item("cbePayOrderTyp")) = 4 Then
						    Session("OP006_sCodispl") = Request.QueryString.Item("sCodispl")
						    Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=OP012I" & mstrQueryString & "';</SCRIPT>")
					    Else
						    If Request.Form.Item("sCodisplReload") = vbNullString Then
							    Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						    Else
							    Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
						    End If
					    End If
					    '+ Si se trata de entrada de dinero en caja, se recarga la ventana
				    ElseIf Request.QueryString.Item("sCodispl") = "OP001" Then 
					    mstrScript = "<SCRIPT>"
					    If Request.Form.Item("sCodisplReload") > vbNullString Then
						    mstrScript = mstrScript & "window.close();opener."
					    End If
					    If mstrLocation = vbNullString Then
						    mstrScript = mstrScript & "top.document.location.reload();"
					    Else
						    mstrScript = mstrScript & "top.frames['fraHeader'].document.location.href='" & mstrLocation & "';"
					    End If
					    mstrScript = mstrScript & "</SCRIPT>"
					    Response.Write(mstrScript)
				    ElseIf Request.QueryString.Item("sCodispl") = "OP002" And Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptDataFinish) Then 
					    If Request.QueryString.Item("sLinkSpecial") = "1" Then
						    Response.Write("<SCRIPT>top.close();</SCRIPT>")
					    Else
						    Response.Write("<SCRIPT>setTimeout('top.document.location.reload();',5000);</SCRIPT>")
					    End If
				    Else
					    If Request.Form.Item("sCodisplReload") = vbNullString Then
						    Response.Write("<SCRIPT>top.fraFolder.document.location=""/VTimeNet/Cashbank/Cashbankseq/" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					    Else
						    Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""/VTimeNet/Cashbank/Cashbankseq/" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					    End If
				    End If
			    Else
				    Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			    End If
		    End If
	    End If
    End If
    mobjValues = Nothing
    mobjCash_mov = Nothing
    mobjCheque = Nothing
    %>
</form>
</BODY>
</HTML>





