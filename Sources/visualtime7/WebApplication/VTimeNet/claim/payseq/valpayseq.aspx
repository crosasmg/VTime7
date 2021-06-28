<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eGeneral" %>
<%@ Import Namespace="eGeneralForm" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    Dim mobjValues As eFunctions.Values
    Dim mstrErrors As String
    Dim mobjClaim As Object
    Dim mobjClaimbenef As eClaim.ClaimBenef
    Dim mstrCodispl As String
    Dim mintCase_num As String
    Dim mintDeman_type As String


    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String
    Dim mintAction As Object

    '-Variable que guarda el query string a pasar a la ventana asociada a la popup
    Dim mstrQueryString As String


    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String
        '--------------------------------------------------------------------------------------------
        Dim ldblShareTotal As Object
        Dim lintCount As Short
        Dim mintChange As String

        Dim lclsCl_Coinsuran As eClaim.Cl_Coinsuran
        Select Case Request.QueryString("sCodispl")
            Case "SI008_K"
                mobjClaim = New eClaim.T_PayCla
                With Request
                    Session("nBranch") = mobjValues.StringToType(Request.Form("hddBranch"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nProduct") = mobjValues.StringToType(Request.Form("hddProduct"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nCertif") = mobjValues.StringToType(Request.Form("hddCertif"), eFunctions.Values.eTypeData.etdDouble)
                    insvalSequence = mobjClaim.insValSI008_K("SI008_K", mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form("cbeCase"), mobjValues.StringToType(Request.Form("cbePay_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddCertif"), eFunctions.Values.eTypeData.etdDouble))
                    Session("nServ_orderLast") = ""
                    Session("dOccurdate_l") = mobjClaim.dOccurdate_l
                End With

            Case "SI008"
                mobjClaim = New eClaim.T_PayCla

                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        ldblShareTotal = (mobjValues.StringToType(CStr(Session("SI008_tcnAmountPay")), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("nAmountBef"), eFunctions.Values.eTypeData.etdDouble)) + mobjValues.StringToType(.Form("tcnAmountPayCover"), eFunctions.Values.eTypeData.etdDouble)
                        insvalSequence = mobjClaim.insValSI008Upd("SI008", Session("sSche_code"), .QueryString("nMainAction"), Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), mobjValues.StringToType(.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form("nCoverCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Paycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmountPayCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnLocAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPay_Type")), eFunctions.Values.eTypeData.etdDouble), Session("dOccurdate_l"), mobjValues.StringToType(.Form("hddValdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("hddPayDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnId_Settle"), Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnFra_amount"), eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(.Form("tcnDDR"), eFunctions.Values.eTypeData.etdDouble)  )

                    Else
                        mobjClaim.nUsercode = Session("nUsercode")
                        ldblShareTotal = Session("SI008_tcnAmountPay")
                        insvalSequence = mobjClaim.insValSI008("SI008", Session("nClaim"), Session("nCase_num"), Session("nDeman_type"), Session("dEffecdate"), mobjValues.StringToType(.Form("tcdValdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdPayDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form("cbeRole"), .Form("hddvalClient_rep"), .Form("cbeCurrency"), mobjValues.StringToType(.Form("cbePayForm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPay_Type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnInvoice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("cbeDoc_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdBillDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(ldblShareTotal, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI008_Premium")), eFunctions.Values.eTypeData.etdDouble),, mobjValues.StringToType(Request.Form("cbeDeductible_Met"), Values.eTypeData.etdInteger))
                        ', 														   '.Form("hddCover_TPC"))
                        Session("nServ_orderLast") = ""
                    End If
                End With


                '+ SI754: Distribución de coaseguro de un siniestro - ACM - 01/07/2002
            Case "SI754"
                lclsCl_Coinsuran = New eClaim.Cl_Coinsuran

                If Request.QueryString("WindowType") = "PopUp" Then
                    insvalSequence = lclsCl_Coinsuran.insValSI754(mobjValues.StringToType(Request.Form("tcnShare_Percentage"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valCompany"), eFunctions.Values.eTypeData.etdDouble), CInt(Session("nCompanyUser")))
                Else
                    ldblShareTotal = 0
                    lintCount = 0
                    If Not IsNothing(Request.Form.Item("Sel")) Then
                        For Each mintChange In Request.Form.GetValues("Sel")
                            lintCount = mintChange + 1
                            ldblShareTotal = mobjValues.StringToType(ldblShareTotal, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(Request.Form.GetValues("tcnShare_Percentage_AUX").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble)
                        Next mintChange
                    End If
                    insvalSequence = lclsCl_Coinsuran.insValShareTotal("SI54", mobjValues.StringToType(ldblShareTotal, eFunctions.Values.eTypeData.etdDouble))
                End If
                'UPGRADE_NOTE: Object lclsCl_Coinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsCl_Coinsuran = Nothing
                'UPGRADE_NOTE: Object mintChange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mintChange = Nothing

            Case "SI762"
                insvalSequence = ""

            Case "SI749"
                mobjClaim = New eClaim.cl_Reinsuran
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjClaim.insValSI749Upd("SI749", mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddType_Rein"), eFunctions.Values.eTypeData.etdDouble))

                    Else
                        insvalSequence = mobjClaim.insValSI749("SI749", mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If insvalSequence = "" Then
                            ldblShareTotal = 0
                            lintCount = 0
                            If Not IsNothing(Request.Form.Item("Sel")) Then
                                For Each mintChange In Request.Form.GetValues("Sel")
                                    lintCount = CDbl(mintChange) + 1
                                    ldblShareTotal = mobjValues.StringToType(ldblShareTotal, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(Request.Form.GetValues("hddShare").GetValue(lintCount - 1), eFunctions.Values.eTypeData.etdDouble)
                                Next mintChange
                            End If
                            insvalSequence = mobjClaim.insValShareTotal("SI749", mobjValues.StringToType(ldblShareTotal, eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString("sCodispl") & ")"
        End Select
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lobjT_ConcilClaim As eClaim.T_ConcilClaim
        Dim lintPay_Type As Object
        Dim lclsT_Paycla As eClaim.T_PayCla
        Dim ldblAmount As Double
        Dim lintCount As Integer
        Dim lstrFirstCase As String
        Dim lstrCase() As String

        lblnPost = False

        Dim lclsCl_Coinsuran As eClaim.Cl_Coinsuran
        Dim lobjcl_Reinsuran As eClaim.cl_Reinsuran
        Select Case Request.QueryString("sCodispl")

            '+SI008: Pago de siniestro
            Case "SI008_K"

                lstrFirstCase = vbNullString
                lstrFirstCase = Request.Form("cbeCase")

                If lstrFirstCase <> vbNullString Then
                    lstrCase = lstrFirstCase.Split("/")
                    mintCase_num = lstrCase(0)
                    mintDeman_type = lstrCase(1)
                End If

                With Request
                    lblnPost = True
                    Session("SI008_Required") = "1"
                    Session("nClaim") = .Form("tcnClaim")
                    Session("nCase_num") = mintCase_num
                    Session("nDeman_type") = mintDeman_type
                    Session("dEffecdate") = Request.Form("tcdEffecdate")
                    Session("dPaydate") = Request.Form("tcdEffecdate")
                    Session("SI008_tcdValdate") = Request.Form("tcdEffecdate")
                    Session("sCase") = .Form("cbeCase")
                    Session("nPay_Type") = mobjValues.StringToType(.Form("cbePay_Type"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nPremium") = mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdDouble)
                    Session("SI008_cbeRole") = 0
                    Session("SI008_valClient") = ""
                    Session("SI008_valClient_rep") = ""
                    Session("nBranch") = Request.Form("hddBranch")
                    Session("nProduct") = Request.Form("hddProduct")
                    Session("nPolicy") = Request.Form("hddPolicy")
                    Session("SI008_nDeductible_Met") = 0
                    If mobjValues.StringToType(CStr(Session("SI738_nOficepay")), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                        Session("SI008_cbeOffice_pay") = Session("SI738_nOficepay")
                    End If

                    If mobjValues.StringToType(CStr(Session("SI738_nPayForm")), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        Session("SI008_cbePayForm") = 0
                    Else
                        Session("SI008_cbePayForm") = Session("SI738_nPayForm")
                    End If

                    Session("SI008_valServ_order") = eRemoteDB.Constants.intNull
                    Session("SI008_tcnInvoice") = ""
                    If mobjValues.StringToType(CStr(Session("SI738_nCurrency")), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        Session("SI008_cbeCurrency") = 0
                    Else
                        Session("SI008_cbeCurrency") = mobjValues.StringToType(CStr(Session("SI738_nCurrency")), eFunctions.Values.eTypeData.etdDouble)
                    End If

                    If mobjValues.StringToType(CStr(Session("SI738_nExchange")), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        Session("SI008_tcnExchange") = 0
                    Else
                        Session("SI008_tcnExchange") = mobjValues.StringToType(CStr(Session("SI738_nExchange")), eFunctions.Values.eTypeData.etdDouble)
                    End If
                    Session("SI008_tcnAmountPay") = 0
                    Session("SI008_tcdPayDate") = ""
                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                    Session("SI008_tcdValdate") = Today
                    Session("SI008_tcdBillDate") = ""
                    Session("SI008_cbeDoc_type") = 0
                    Session("SI008_Cessi_rei") = 2
                    Session("SI008_Cessi_coi") = 2
                    Session("SI008_Premium") = 0
                    Session("nCurrPaySI008") = ""
                    Session("SI008_LocPremium") = 0
                    Session("SI738_nNetAmount") = 0
                    Session("SI008_nBodereaux") = eRemoteDB.Constants.intNull
                    Session("OP006_nAmountPay") = 0

                    lclsT_Paycla = New eClaim.T_PayCla
                    lblnPost = lclsT_Paycla.DeleteByCase(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")))
                    'UPGRADE_NOTE: Object lclsT_Paycla may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsT_Paycla = Nothing
                End With

            Case "SI008"
                mobjClaimbenef = New eClaim.ClaimBenef
                mobjClaim = New eClaim.T_PayCla
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjClaim.insPostSI008Upd(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nCoverCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmountPayCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nGroup_insu"), eFunctions.Values.eTypeData.etdDouble), .Form("nIndAutomatic"), Session("SI008_cbeCurrency"), mobjValues.StringToType(.Form("tcnAmount_Paycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddnTotcov_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDepreciateamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDepreciaterate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDepreciatebase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFra_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnRasa"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddRasaAnnual"), eFunctions.Values.eTypeData.etdDouble), .Form("hddRASA_routine"), mobjValues.StringToType(.Form("tcnDDR"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble),  mobjValues.StringToType(.Form("tcnId_Settle"), eFunctions.Values.eTypeData.etdDouble))

                        Session("nCover") = mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nPay_concep") = mobjValues.StringToType(.Form("tcnConcept"), eFunctions.Values.eTypeData.etdDouble)

                        Session("SI738_nNetAmount") = Session("SI738_nNetAmount") + mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble)
                        mstrQueryString = "&sClient=" & .Form("hddClientupd") & "&nServ_order=" & .Form("hddServ_ord") & "&nOffice=" & .Form("hddOffice") & "&nOfficeAgen=" & .Form("hddOfficeAgen") & "&nAgency=" & .Form("hddAgency")


                        If Request.QueryString("nDeductible_Met") = "4" Then
                            With mobjValues
                                If mobjClaim.InsApplyDDR(.StringToType(Session("nClaim"), Values.eTypeData.etdDouble), .StringToType(Session("nCase_num"), Values.eTypeData.etdDouble), .StringToType(Session("nDeman_type"), Values.eTypeData.etdDouble), "1") Then

                                    Response.Write("top.fraFolder.insSubmitPage();")

                                End If
                            End With
                        End If

                    Else
                        Session("SI008_Required") = "2"
                        mstrCodispl = "SI008"
                        Select Case Session("SI008_cbePayForm")
                            Case 1, 10
                                mstrCodispl = "OP06-2"
                                lintPay_Type = 2
                            Case 4
                                mstrCodispl = "OP06-4"
                                lintPay_Type = 1
                            Case 5
                                mstrCodispl = "OP06-3"
                                lintPay_Type = 3
                            Case 8
                                mstrCodispl = "OP06-6"
                                lintPay_Type = 5
                            Case Else
                                mstrCodispl = "SI008"
                                lintPay_Type = 1
                        End Select

                        '+Se escriben las variables de Session para la conexión con ordenes de pago

                        Session("OP006_nCurrencyPay") = mobjValues.StringToType(Request.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nCurrency") = mobjValues.StringToType(CStr(Session("SI008_cbeCurrencyCover")), eFunctions.Values.eTypeData.etdInteger)
                        Session("OP006_sBenef") = Request.Form("hddvalClient_rep")
                        If CDbl(Session("nPay_Type")) = 3 Then
                            Session("OP006_nConcept") = 7
                        Else
                            If CDbl(Session("nPay_Type")) = 5 Then
                                Session("OP006_nConcept") = 8
                            Else
                                Session("OP006_nConcept") = 6
                            End If
                        End If

                        Session("OP006_sCodispl") = Request.QueryString("sCodispl")
                        Session("OP006_nRole") = mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nServ_order") = mobjValues.StringToType(.Form("valServ_order"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_sCoinsuNet") = .Form("chkCoinsuNet")
                        Session("OP006_nInvoice") = .Form("tcnInvoice")
                        Session("OP006_nExchange") = mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble)
                        ldblAmount = mobjValues.StringToType(CStr(Session("SI008_Premium")), eFunctions.Values.eTypeData.etdDouble)
                        ldblAmount = (mobjValues.StringToType(CStr(Session("SI008_tcnAmount_Paycov_sum")), eFunctions.Values.eTypeData.etdDouble)) -  mobjValues.StringToType(ldblAmount, eFunctions.Values.eTypeData.etdDouble)

                        Session("OP006_nAmount") = Session("SI008_tcnAmount_Paycov_sum")
                        Session("OP006_nAmountPay") = ldblAmount
                        Session("SI008_nAmountPay") = mobjValues.StringToType(CStr(Session("SI008_tcnAmountPay")), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nPay_Type") = mobjValues.StringToType(CStr(Session("nPay_Type")), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nPayOrderTyp") = mobjValues.StringToType(lintPay_Type, eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_Codispl") = mstrCodispl
                        Session("sCase") = vbNullString

                        Session("SI008_cbeOffice_pay") = mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nOfficeAgen") = mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_nAgency") = mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble)
                        Session("OP006_dReqDate") = .Form("tcdPaydate")
                        Session("SI008_tcdValdate") = .Form("tcdValdate")
                        Session("SI008_nDeductible_Met") = mobjValues.StringToType(Request.Form.Item("cbeDeductible_Met"), Values.eTypeData.etdDouble)
                        Session("SI008_cbeDoc_type") = mobjValues.StringToType(Request.Form.Item("cbeDoc_Type"), Values.eTypeData.etdDouble)
                        lblnPost = mobjClaimbenef.UpdBenefPay(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), .Form("valClient"), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), CInt(Session("SI008_cbeOffice_pay")), CInt(Session("OP006_nAgency")), CInt(Session("OP006_nOfficeAgen")))

                        '+ Si se registra un pago total se genera la solicitud de impresión del finiquito
                        'If lblnPost And CDbl(Session("nPay_Type")) = 2 Then
                        'insPrintDocuments("SI008")
                        'End If

                    End If

                End With

                '+ SI754: Distribución de coaseguro de un siniestro - ACM - 01/07/2002
            Case "SI754"
                lclsCl_Coinsuran = New eClaim.Cl_Coinsuran
                If Request.QueryString("WindowType") = "PopUp" Then
                    lblnPost = lclsCl_Coinsuran.insPostSI754(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), Request.Form("valCompany"), Request.Form("tcnShare_Percentage"), Request.Form("hddExpenses"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.Form("hddSel"))
                    If lblnPost Then
                        Session("SI008_Cessi_coi") = "1"
                    End If
                Else
                    lblnPost = lclsCl_Coinsuran.insPostSI754("", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), Request.Form("valCompany_AUX"), Request.Form("tcnShare_Percentage_AUX"), Request.Form("hddExpenses"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.Form("hddSel"))
                End If
                'UPGRADE_NOTE: Object lclsCl_Coinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsCl_Coinsuran = Nothing


                '+SI749: Distribución de reasguro de un siniestro
            Case "SI749"
                lobjcl_Reinsuran = New eClaim.cl_Reinsuran

                mstrQueryString = "&nModulec=" & Request.Form("hddModulec") & "&nCover=" & Request.QueryString("nCover") & "&nCurrency=" & Request.Form("hddCurrency") & "&sClient=" & Request.Form("hddClient")

                If Request.QueryString("WindowType") = "PopUp" Then

                    lblnPost = lobjcl_Reinsuran.InsPostSI749(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("hddCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddBranch_Rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddType_Rein"), eFunctions.Values.eTypeData.etdDouble), Request.Form("hddClient"), mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddAccedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("hddCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddCommissi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.Form("hddHeap_code"), mobjValues.StringToType(Request.Form("hddInter_Rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddReser_Rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddAcep_Code"), eFunctions.Values.eTypeData.etdDouble), Request.Form("hddSel"))
                    lblnPost = True
                    If lblnPost Then
                        Session("SI008_Cessi_rei") = 1
                    End If
                Else
                    If Request.Form("Sel") <> Nothing Then
                        Dim lstrSel As String() = Request.Form("Sel").Split(New [Char]() {","c})
                        For Each mintChang As String In lstrSel
                            lintCount = CInt(mintChang)
                            lblnPost = lobjcl_Reinsuran.InsPostSI749(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.GetValues("hddCompany").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddShare").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddBranch_Rei").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddModulec").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddType_Rein").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), Request.Form.GetValues("hddClient").GetValue(lintCount), mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddAccedate").GetValue(lintCount), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.GetValues("hddCapital").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddCommissi").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddCurrency").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), Request.Form.GetValues("hddHeap_code").GetValue(lintCount), mobjValues.StringToType(Request.Form.GetValues("hddInter_Rate").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddNumber").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddReser_Rate").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddChange").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("hddAcep_Code").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), Request.Form.GetValues("hddSel").GetValue(lintCount))
                        Next
                    Else
                        lblnPost = True
                    End If
                End If
                'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lintCount = Nothing
                'UPGRADE_NOTE: Object lobjcl_Reinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lobjcl_Reinsuran = Nothing
                '+SI762: Recibos pendientes para conciliación de siniestros
            Case "SI762"
                lobjT_ConcilClaim = New eClaim.T_ConcilClaim

                If Request.Form("hddReceipt") <> vbNullString Then
                    lblnPost = lobjT_ConcilClaim.InsPostSI762(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), Request.Form("hddReceipt"), Request.Form("hddBalance"), Request.Form("hddSel"), Request.Form("hddContrat"), Request.Form("hddDraft"), CInt(Session("nUserCode")))
                Else
                    lblnPost = True
                End If
                'UPGRADE_NOTE: Object lobjT_ConcilClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lobjT_ConcilClaim = Nothing

                If lblnPost Then
                    Response.Write("<script>top.fraHeader.document.forms[0].tcnPremium.value = '" & Session("SI008_Premium") & "';</" & "Script>")
                    Session("OP006_nAmountPay") =  Session("OP006_nAmount") -   Session("SI008_Premium")
                End If

                '+ Ventana de Fin de proceso		
        End Select
        insPostSequence = lblnPost
    End Function

    '% insFinish: Se activa cuando la acción es Finalizar
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        '+ Se verifica que no existan páginas marcadas como requeridas
        Dim lclsT_Paycla As eClaim.T_PayCla
        Dim lclsErrors As eGeneralForm.GeneralForm
        Dim lclsT_ConcilClaim As eClaim.T_ConcilClaim
        Dim lclscl_Reinsuran As eClaim.cl_Reinsuran
        Dim lclsGeneralF As eGeneral.GeneralFunction
        Dim lcolReport_prod As eProduct.report_prods
        Dim lclsReport_prod As eProduct.report_prod
        Dim mobjDocuments As eReports.Report

        mobjDocuments = New eReports.Report
        lclsT_ConcilClaim = New eClaim.T_ConcilClaim
        lclsGeneralF = New eGeneral.GeneralFunction

        Dim mstrErrors As String

        insFinish = True

        lclsT_Paycla = New eClaim.T_PayCla
        lclsErrors = New eGeneralForm.GeneralForm
        lclscl_Reinsuran = New eClaim.cl_Reinsuran

        If CStr(Session("SI008_REQUIRED")) <> "2" Then
            insFinish = False
        End If

        If Not insFinish Then
            mstrErrors = lclsErrors.insValGE101("ClientSeq")

            If (mstrErrors > vbNullString) Then

                Session("sErrorTable") = mstrErrors
                Session("sForm") = Request.Form.ToString
                With Response
                    .Write("<script type='text/javascript'>")
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.QueryString.ToString) & """, ""PaySeqError"",660,330);")
                    .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
                    .Write("</" & "Script>")
                End With

            End If
        End If

        If insFinish And CDbl(Session("nPay_Type")) = 4 Then
            insFinish = lclscl_Reinsuran.insValidate_Cl_Reinsuran(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull)
            If Not insFinish Then
                Response.Write("<script>alert('Verifique la distribución de reaseguro');</" & "Script>")
            End If
        End If

        If insFinish Then
            If lclsT_ConcilClaim.InsExists(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type"))) Then
                Session("SI008_nBodereaux") = lclsGeneralF.Find_Numerator(20, 0, CInt(Session("nUsercode")))
                Response.Write("<script>alert('El número de relación asignado es: " & Session("SI008_nBodereaux") & "');</" & "Script>")
            End If
        End If

        If CStr(Session("OP006_Codispl")) = "SI008" And insFinish Then
            '	   	Session("OP006_nAgency")
            insFinish = lclsT_Paycla.insFinish(CDbl(Session("nClaim")), CInt(Session("nCase_Num")), CInt(Session("nDeman_type")), CInt(Session("OP006_nRole")), CStr(Session("SI008_valClient")), CInt(Session("SI008_cbePayForm")), CInt(Session("OP006_nPay_type")), CDbl(Session("OP006_nServ_order")), CStr(Session("SI008_Cessi_coi")), mobjValues.StringToType(CStr(Session("OP006_nInvoice")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), CInt(Session("OP006_nCurrencypay")), CDbl(Session("OP006_nExchange")), CDbl(Session("OP006_nAmountPay")), CInt(Session("nUsercode")), mobjValues.StringToType(CStr(Session("SI008_cbeOffice")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("OP006_nOfficeAgen")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("OP006_nAgency")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("OP006_nAmountPay")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "", 0, CDbl(Session("SI008_nAmountPay")), mobjValues.StringToType(CStr(Session("OP006_nConcept")), eFunctions.Values.eTypeData.etdDouble, True), CStr(Session("OP006_sBenef")), mobjValues.StringToType(CStr(Session("OP006_dReqDate")), eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType(CStr(Session("OP006_dReqDate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("OP006_dReqDate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("OP006_nPayOrderTyp")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("OP006_dReqDate")), eFunctions.Values.eTypeData.etdDate), CInt(Session("nUsercode")), eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", CInt(Session("SI008_cbeDoc_type")), mobjValues.StringToType(CStr(Session("SI008_tcdBillDate")), eFunctions.Values.eTypeData.etdDate), , , , , mobjValues.StringToType(Request.Form("cbeCompany"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("OP006_nAmountPay")), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(Session("SI008_Cessi_rei")), CInt(Session("SI008_nBodereaux")), mobjValues.StringToType(CStr(Session("SI008_tcdValdate")), eFunctions.Values.eTypeData.etdDate))
            If insFinish Then
                With mobjDocuments
                    lcolReport_prod = New eProduct.report_prods
                    Session("sCertype")="2"
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
                            If lclsReport_prod.sCodCodispl = "SI008" Then
                                .sCodispl = "SI008"
                                .ReportFilename = lclsReport_prod.sReport
                                .setStorProcParam(1, Session("nClaim"))
                                .setStorProcParam(2, Session("nUserCode"))
                                Response.Write(.Command)
                            End If
                        Next
                    End If
                End With
            end if
        End If

        'UPGRADE_NOTE: Object lclsT_Paycla may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsT_Paycla = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsT_ConcilClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsT_ConcilClaim = Nothing
        'UPGRADE_NOTE: Object lclsGeneralF may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsGeneralF = Nothing
        'UPGRADE_NOTE: Object lclscl_Reinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclscl_Reinsuran = Nothing
    End Function

    Private Sub insPrintDocuments(ByVal sCodispl As String)
        Dim mobjDocuments2 As eReports.Report
        mobjDocuments2 = New eReports.Report

        Select Case sCodispl
            Case "SI008"
                Dim lclsValClaimRep As eClaim.ValClaimRep
                lclsValClaimRep = New eClaim.ValClaimRep
                Dim lintCount As Integer

                If lclsValClaimRep.insReaSIL005("SIL008", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), ,
                                                mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble),
                                                mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble),
                                                mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), ,
                                                Session("nCover"),
                                                Session("nPay_concep")) Then

                    For lintCount = 1 To lclsValClaimRep.Count_SIL005 - 1
                        With mobjDocuments2
                            If lclsValClaimRep.Item_SIL005(lintCount) Then
                                If lclsValClaimRep.sFormatname.ToUpper() = "SIL005.RPT" Then
                                    .sCodispl = "SIL005"
                                    .ReportFilename = "SIL005.rpt"
                                    .setStorProcParam(1, 0)
                                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                                    If mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(3, 0)
                                    Else
                                        .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                    If mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(4, 0)
                                    Else
                                        .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                    If mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(5, 0)
                                    Else
                                        .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                    If mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(6, 0)
                                    Else
                                        .setStorProcParam(6, mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                    If mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(7, 0)
                                    Else
                                        .setStorProcParam(7, mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                    If mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                                        .setStorProcParam(8, 0)
                                    Else
                                        .setStorProcParam(8, mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble))
                                    End If

                                    .setStorProcParam(9, 0)

                                    .setStorProcParam(10, Session("nUsercode"))
                                    .Merge = True
                                    .MergeCertype = "2"
                                    .MergeBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergeProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergePolicy = mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergeCertif = mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
                                    Response.Write((.Command))
                                    .Reset()
                                    '.bTimeOut = True

                                End If

                            End If
                        End With
                    Next
                    lclsValClaimRep = Nothing
                    mobjDocuments2 = Nothing

                End If

        End Select

    End Sub

</script>
<%
    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("valpayseq")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "valpayseq"
    mstrCommand = "&sModule=Claim&sProject=PaySeq&sCodisplReload=" & Request.QueryString("sCodispl")

%>
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%=mobjValues.StyleSheet()%>
</head>
<body>
    <%Response.Write("<script>")%>
    function CancelErrors(){self.history.go(-1)} function NewLocation(Source,Codisp){
    var lstrLocation = ""; lstrLocation += Source.location; lstrLocation = lstrLocation.replace(/&OPENER=.*/,"")
    + "&OPENER=" + Codisp; Source.location = lstrLocation; } </script>
    <%
        If Request.QueryString("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
            '+ Si no se han validado los campos de la página
            If Request.Form("sCodisplReload") = vbNullString Then
                mstrErrors = insvalSequence()
                Session("sErrorTable") = mstrErrors
                Session("sForm") = Request.Form.ToString
            Else
                Session("sErrorTable") = vbNullString
                Session("sForm") = vbNullString
            End If
	
	
            If mstrErrors > vbNullString Then
                With Response
                    .Write("<script type='text/javascript'>")
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.QueryString.ToString) & """,""ClaimPaySeqErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                    .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
                    .Write("</script>")
                End With
            Else
                If insPostSequence() Then
                    If Request.QueryString("WindowType") <> "PopUp" Then
				
                        '+ Se mueve automaticamente a la siguiente página
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            If Request.QueryString.Item("sCodispl") = "SI008_K" Then
                                Response.Write("<script>top.frames['fraSequence'].document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes';</script>")
                            Else
                                Response.Write("<script>top.frames['fraSequence'].document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</script>")
                            End If
                        Else
                            If Request.QueryString("nMainAction").Count > 1 Then
                                mintAction = Request.QueryString.GetValues("nMainAction").GetValue(0)
                            Else
                                Response.Write("<script>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & mintAction & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</script>")
                            End If
                            If Request.QueryString("sCodispl") = "SI008_K" Then
                                Response.Write("<script>window.close();opener.top.frames('fraSequence').document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=Yes';</script>")
                            Else
                                Response.Write("<script>window.close();opener.top.frames('fraSequence').document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & mintAction & "&sGoToNext=Yes&nOpener=" & Request.QueryString("sCodispl") & "';</script>")
                            End If
                        End If
                        If Request.QueryString("nZone") = 1 Then
                            Response.Write("<script language =javascript> self.history.go(-1) </script>")
                        End If
                    Else
                        Response.Write("<script>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/PaySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</script>")
                        '+ Se recarga la página que invocó la PopUp
                        Select Case Request.QueryString("sCodispl")
                            Case "SI008"
                                Response.Write("<script>top.opener.document.location.href='SI008.aspx?sCodispl=SI008&sCodisp=SI008&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=304" & "&Index=" & Request.QueryString("Index") & mstrQueryString & "'</script>")
                            Case "SI754"
                                Response.Write("<script>top.opener.document.location.href='SI754.aspx?sCodispl=SI754&sCodisp=SI754&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & "&Index=" & Request.QueryString("Index") & "'</script>")
                            Case "SI749"
                                Response.Write("<script>top.opener.document.location.href='SI749.aspx?sCodispl=SI749&sCodisp=SI749&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & "&Index=" & Request.QueryString("Index") & mstrQueryString & "' </script>")
                            Case "SI762"
                                Response.Write("<script>top.opener.document.location.href='SI762.aspx?sCodispl=SI762&sCodisp=SI762&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & "&Index=" & Request.QueryString("Index") & "' </script>")
						
                        End Select
                    End If
                Else
                    Response.Write("<script>alert('No se realizó actualización (" & Request.QueryString("sCodispl") & ")')</script>")
                End If
            End If
        Else
	
            If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
                Session("MenuOption") = Request.QueryString("nMainAction")
                Response.Write("<script>top.location.reload();</script>")
            Else
                If insFinish() Then
                    Session("sOriginalForm") = vbNullString
                    If CStr(Session("OP006_Codispl")) <> "SI008" Then
                        Response.Write("<script>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Session("OP006_Codispl") & "&nCurrencypay=" & Session("OP006_nCurrencyPay") & "&nCurrency=" & Session("OP006_nCurrency") & "&nOfficepay=" & Session("SI008_cbeOffice_pay") & "&nAmountpay=" & Session("OP006_nAmountPay") & "&nAmount=" & Session("OP006_nAmountPay") & "&nOffice=" & Session("SI008_cbeOffice_pay") & "&nOfficeAgen=" & Session("OP006_nOfficeAgen") & "&nAgency=" & Session("OP006_nAgency") & "&nPayOrderTyp=" & Session("OP006_nPayOrderTyp") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nTypesupport=" & Session("SI008_cbeDoc_Type") & "&nDoc_support=" & Session("SI008_tcnInvoice") & "';</script>")
                    Else
                        If mobjValues.StringToType(CStr(Session("SI738_Benetype")), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                            Response.Write("<script>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI738" & "&nCurrencypay=" & Session("OP006_nCurrency") & "&nOfficepay=" & Session("SI008_cbeOffice_pay") & "&nAmountpay=" & "&nOffice=" & Session("SI008_cbeOffice_pay") & "&nOfficeAgen=" & Session("OP006_nOfficeAgen") & "&nAgency=" & Session("OP006_nAgency") & "';</script>")
                        Else
                            Response.Write("<script>top.document.location.reload();</script>")
                        End If
                    End If
                End If
            End If
        End If
        
        mobjClaim = Nothing
        mobjValues = Nothing

    %>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("valpayseq")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
