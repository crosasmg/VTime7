<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
    Dim mobjValues As eFunctions.Values
    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String
    Dim mintCollector As String
    Dim mstrCodispl As String
    '+ Variable auxiliar para pase de valores del encabezado al folder
    Dim mstrString As String
    Dim mstrQueryString As String
    Dim mstrKey As String
    Dim mdtmDateIni As String
    Dim mdtmDateEnd As String
    Dim mdtmDatePrint As String
    Dim mstrMode As String
    '+ Variable para el refresco de la pagina en la generación del reporte
    Dim mblnTimeOut As Boolean

    Dim mstrErrors As Object
    Dim mobjCollectionTra As Object


    '% insValCollectionTra: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insValCollectionTra() As Object
        Dim lstrclient As String
        Dim lintCount As Object
        '--------------------------------------------------------------------------------------------   			
        Select Case Request.QueryString.Item("sCodispl")
            '+ CO003: Convenio de pago
            Case "CO003"

                mobjCollectionTra = New eCollection.Premium
                With Request
                    If .QueryString.Item("nZone") = "1" Then

                        insValCollectionTra = mobjCollectionTra.insValCO003_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valPay_form"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAction"), eFunctions.Values.eTypeData.etdDouble))
                    Else

                        insValCollectionTra = mobjCollectionTra.insValCO003(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdPaydate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcdEffecdate_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnIntammou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAction_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tctPay_form_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnReceipt_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), .QueryString("Windowtype"))
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO004_K: Via de cobro  
            Case "CO004"
                mobjCollectionTra = New eCollection.Premium



                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then


                        insValCollectionTra = mobjCollectionTra.insValCO004_k("CO004", .Form.Item("optGenera"), .Form.Item("hddsCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffectDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble))

                        mstrString = "&nReceipt=" & .Form.Item("tcnReceiptNum") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&sCertype=" & .Form.Item("hddsCertype") & "&nWayPay=" & mobjCollectionTra.nWay_Pay & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("hddnCertif") & "&nDigit=" & .Form.Item("hddnDigit") & "&nPaynumbe=" & .Form.Item("hddnPaynumbe") & "&nDraft=" & .Form.Item("tcnDraft") & "&nStatus_pre=" & .Form.Item("hddnStatus_pre") & "&nContrat=" & .Form.Item("tcnContrat") & "&sTypeDoc=" & .Form.Item("optGenera")
                    Else
                        If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionquery) Then

                            mstrString = "&nReceipt=" & .Form.Item("tcnReceiptNum") & "&nBranch=" & .Form.Item("cbeBranch")
                            mstrString = mstrString & "&nProduct=" & .Form.Item("valProduct") & "&sCertype=" & .Form.Item("tctCertype")

                            If IsNothing(Request.Form.Item("tctTitular")) Then
                                lstrclient = Request.Form.Item("hddsTitular")
                            Else
                                lstrclient = Request.Form.Item("tctTitular")
                            End If



                            insValCollectionTra = mobjCollectionTra.insValCO004("CO004", "2", mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdLong), .Form.Item("optChangeway"), .Form.Item("optChangepremium"), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeBankPAC"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctTitularPAC"), .Form.Item("tctAccountPAC"), .Form.Item("tctBankAuthPAC"), mobjValues.StringToType(.Form.Item("cbeCardTypeTbk"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctCardNumberTbk"), mobjValues.StringToType(.Form.Item("tcdCardExpirTbk"), eFunctions.Values.eTypeData.etdDate), .Form.Item("dtcClient"), .Form.Item("optWayNewPay"), mobjValues.StringToType(.Form.Item("cbeBankPACNew"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctClientPACNew"), .Form.Item("tctAccountPACNew"), .Form.Item("tctBankAuthPACNew"), mobjValues.StringToType(.Form.Item("cbeCardTypeNew"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdCardExpirNew"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctClientCreditNew"), .Form.Item("tctCardNumberNew"), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDigit"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddnPaynumbe"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDraft"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsWay_Pay"), .Form.Item("hddsTypeDoc"), mobjValues.StringToType(Request.Form.Item("valAgreementNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valOriginNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAFPCommiNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrencyNew"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsApv"), mobjValues.StringToType(.Form.Item("hddnprodclas"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClientPayNew"), .Form.Item("dtcClientEmpNew"), mobjValues.StringToType(Request.Form.Item("cbePayfreq"), eFunctions.Values.eTypeData.etdDouble))

                        End If
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO005: Anulación/Reinstalacion de recibo
            Case "CO005"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    insValCollectionTra = mobjCollectionTra.insValCO005("CO005", mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnReceipt"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optAnul"), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble, True))
                    mobjCollectionTra = Nothing
                End With

            '+ CO009: Reverso de cobro o devolución
            Case "CO009", "CO09"
                mobjCollectionTra = New eCollection.Premium_mo
                With Request
                    mobjCollectionTra.nUsercode = Session("nUserCode")
                    insValCollectionTra = mobjCollectionTra.insValCO009(.QueryString("sCodispl"), mobjValues.StringToDate(.Form.Item("tcdDate")), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), Session("sReceiptNum"), mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRelAll"), .Form.Item("optTypOper"))
                End With
                mobjCollectionTra = Nothing

            '+ CO632: Generación manual de boletines.
            Case "CO632"
                mobjCollectionTra = New eCollection.T_bulletins_det
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValCollectionTra = True
                        insValCollectionTra = mobjCollectionTra.insValCO632_K("CO632", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optQueryOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdCollectDate")), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClientK"), mobjValues.StringToType(.Form.Item("optStyle_bull"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrencyBul"), eFunctions.Values.eTypeData.etdDouble, True))

                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO632A: Generación manual de boletines (Detalle).
            Case "CO632A"
                '+ Si existen registros a procesar.		
                mobjCollectionTra = New eCollection.T_bulletins_det
                With Request
                    '+ Se efectúan la validación puntual.
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValCollectionTra = mobjCollectionTra.insValCO632AUpd("CO632A", .QueryString("Action"), Session("sReceiptNum"), .Form.Item("Sel"), mobjValues.StringToType(.Form.Item("nBulletinsHdr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnStatus_pre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdnStat_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nInsur_areaHdr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrencyBul"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        '+ Se efectúan la validación masiva.
                        If .QueryString.Item("nMainAction") <> "401" Then
                            If mobjValues.StringToType(Request.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                                insValCollectionTra = mobjCollectionTra.insValCO632A("CO632A", mobjValues.StringToType(Session("nBulletins"), eFunctions.Values.eTypeData.etdDouble), Session("sStyle_bull"))
                            End If
                        End If
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO633: Suspensión/Reactivación de cobranzas
            Case "CO633"
                mobjCollectionTra = New eCollection.Premium

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValCollectionTra = mobjCollectionTra.insValCO633_K("CO633", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdOperation"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTypOper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollSus_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCollSus_end"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeSus_reason"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO633A: Suspensión/Reactivación de cobranzas (masiva)
            Case "CO633A"
                '+ Si existen registros a procesar.	

                If mobjValues.StringToType(Request.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                    mobjCollectionTra = New eCollection.Premium
                    With Request
                        insValCollectionTra = mobjCollectionTra.insValCO633A("CO633", .Form.Item("hddReceiptBulle"))
                    End With
                    mobjCollectionTra = Nothing
                End If

            '+ CO634: Traspaso de pago
            Case "CO634"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    insValCollectionTra = mobjCollectionTra.insValCO634("CO634", mobjValues.StringToType(.Form.Item("tcdStatDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTypTras"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponumOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponumDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrencyOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrencyDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountTrasOri"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjCollectionTra = Nothing

            '+ CO675: Cambio de fecha de generación de cobranzas de un recibo
            Case "CO675"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    insValCollectionTra = mobjCollectionTra.insValCO675("CO675", mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnStatusPre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdLimitDate")), mobjValues.StringToDate(.Form.Item("tcdNewLimitDate")), mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddnType"), CBool(IIf(IsNothing(.Form.Item("tctPremiumExist")), False, .Form.Item("tctPremiumExist"))))
                End With
                mobjCollectionTra = Nothing

            '+ CO685: Cobradores
            Case "CO685"
                mobjCollectionTra = New eCollection.Collector
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
                            mintCollector = mobjValues.StringToType(insGetNewCollector(.Form.Item("tcnCollector")), eFunctions.Values.eTypeData.etdDouble)
                        Else
                            mintCollector = mobjValues.StringToType(.Form.Item("tcnCollector"), eFunctions.Values.eTypeData.etdDouble)
                        End If
                        mstrString = "&nCollector=" & mintCollector
                        Session("nCollector") = mintCollector
                        insValCollectionTra = mobjCollectionTra.insValCO685_K("CO685", .QueryString("nMainAction"), Session("nCollector"))
                    Else
                        insValCollectionTra = mobjCollectionTra.insValCO685("CO685", .QueryString("nMainAction"), Session("nCollector"), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcnColType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dtInputDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnConType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnInsur_area"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLegal_Sch"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO722: Actualización de mandatos por póliza
            Case "CO722"
                mobjCollectionTra = New ePolicy.DirDebit
                With Request
                    insValCollectionTra = mobjCollectionTra.insValCO722("CO722", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToDate(.Form.Item("tcdDate")), .Form.Item("tctBankAuthOld"), .Form.Item("tctBankAuthNew"))
                End With
                mobjCollectionTra = Nothing

            '+ CO501: Rechazo de pagos PAC/Transbank
            Case "CO501"
                With Request
                    mobjCollectionTra = New eCollection.Bulletin
                    If .QueryString.Item("nZone") = "1" Then
                        insValCollectionTra = mobjCollectionTra.insValCO501_K("CO501", mobjValues.StringToType(.Form.Item("tcdExpiriDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValCollectionTra = mobjCollectionTra.insValCO501Upd("CO501", mobjValues.StringToType(.Form.Item("tctbulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctCause"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sKey"), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"))
                        Else
                            insValCollectionTra = mobjCollectionTra.insValCO501("CO501", .Form.Item("sKey"))
                        End If
                    End If

                End With
                mobjCollectionTra = Nothing

            '+ CO514: Mantenimiento de boletines

            Case "CO514"
                With Request
                    mobjCollectionTra = New eCollection.Bulletins_det

                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValCollectionTra = mobjCollectionTra.insValCO514_K("CO514", mobjValues.StringToType(.Form.Item("tctBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))

                        Session("nBulletins") = mobjValues.StringToType(.Form.Item("tctBulletins"), eFunctions.Values.eTypeData.etdDouble)
                    Else
                        insValCollectionTra = mobjCollectionTra.insValCO514("CO514", .Form.Item("tctNullCode"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                    End If

                End With
                mobjCollectionTra = Nothing

            '+ CO635: Asignación de cartera a cobradores.
            Case "CO635"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mobjCollectionTra = New eCollection.Premium
                        insValCollectionTra = mobjCollectionTra.insValCO635_K("CO635", mobjValues.StringToType(.Form.Item("valCollectorPre"), eFunctions.Values.eTypeData.etdDouble))

                        mstrString = "&nCollector=" & .Form.Item("valCollectorPre")
                    End If
                    mobjCollectionTra = Nothing
                End With

            '+ CO700: Generación de facturas/notas de crédito

            Case "CO700"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    mobjCollectionTra = New eCollection.Bills
                    mobjCollectionTra.nUsercode = Session("nUsercode")
                    Dim nInsur_Area As Int32
                    Dim nBranch As Int32
                    Dim nProduct As Int32
                    nBranch = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
                    nProduct = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True)
                    nInsur_Area = mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble)
                    'If nBranch = 71 And nProduct = 1 Then
                    '    nInsur_Area = 12
                    'End If
                    'insValCollectionTra = mobjCollectionTra.insValCO700_K("CO700", Request.QueryString.Item("nMainAction"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("optDocType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optBillType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDatePrint"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("optMode"), mobjValues.StringToType(Request.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcClient"), "2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("chkBill_Ind"))
                    insValCollectionTra = mobjCollectionTra.insValCO700_K("CO700", Request.QueryString.Item("nMainAction"), nInsur_Area, mobjValues.StringToType(Request.Form.Item("optDocType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optBillType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDatePrint"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("optMode"), mobjValues.StringToType(Request.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcClient"), "2", nBranch, nProduct, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("chkBill_Ind"))
                    If insValCollectionTra = vbNullString Then
                        mstrKey = mobjCollectionTra.skey
                    End If

                    mobjCollectionTra = Nothing
                End If

            '+ CO700A: Generación de facturas/notas de crédito (grid)
            Case "CO700A"
                mobjCollectionTra = New eCollection.Bills

                insValCollectionTra = mobjCollectionTra.insValCO700A("CO700A", Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                mobjCollectionTra = Nothing

            '+ CO788: Devolución de Cobro
            Case "CO788", "CO788C"
                mobjCollectionTra = New eCollection.ColformRef
                With Request
                    insValCollectionTra = mobjCollectionTra.insValCO788(mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeTypeDoc"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnNumDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIncrease"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("dtcClient"), mobjValues.StringToType(Request.Form.Item("OptDev"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("OptDocRev"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("hddSequence"), eFunctions.Values.eTypeData.etdLong), Session("nUserCode"))
                End With
                mobjCollectionTra = Nothing

            Case "CO982"
                mobjCollectionTra = New eCollection.Reject_cause
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        insValCollectionTra = mobjCollectionTra.InsValCO982_k("CO982", mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        insValCollectionTra = True
                    End If

                End With
                mobjCollectionTra = Nothing

            Case Else
                insValCollectionTra = "insValCollectionTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '%insPostCollectionTra: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Function insPostCollectionTra() As Boolean
        Dim lstrhddReceipt As String
        Dim lstrhddPolicy As String
        Dim sAction As String
        Dim lstrhddSelAux As String
        Dim llngsize As Integer
        Dim ldblAmount As Object
        Dim lstrDocument As String
        Dim llngnPayOrderTyp As Byte
        Dim lstrhddBranch As String
        Dim ldblAmountPayCO788 As Object
        Dim lstrhddProduct As String
        Dim llngCurrencyOri As Object
        Dim lstrtcnDraft As String
        Dim lstrtctCertype As String
        Dim llngBulletins As Double
        Dim lstrhddEffecdate As String
        Dim lstrhddBulletins As String
        Dim lstrtcnContrat As String
        Dim ldtmDateIncrease As Date
        Dim llngselected As Short
        Dim sAccountPACTBK As String
        Dim ldblAmountPay As Double
        Dim lintAction As Object
        Dim llngCurrencyBul As Object
        Dim ldtmDateIncreaseCO788 As String
        '--------------------------------------------------------------------------------------------

        Dim lblnPost As Boolean
        Dim lintCountAux As Integer
        Dim lobjGeneral As eGeneral.GeneralFunction

        lblnPost = False

        Dim lobjColFormRef As eCollection.ColformRef
        Dim lobjNumerator As eGeneral.GeneralFunction
        Dim lobjBills As eCollection.Bills
        Dim mobjDocuments As eReports.Report
        Dim lcolReject_causes As eCollection.Reject_causes
        Select Case Request.QueryString.Item("sCodispl")

            '+ CO003: Convenio de pago
            Case "CO003"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&nHeight=225" & "&nReceipt=" & .Form.Item("tcnReceipt") & "&sSelect=" & .Form.Item("tcnSelect") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sPay_form=" & .Form.Item("valPay_form") & "&nAction=" & .Form.Item("valAction") & "&nStatus_pre=" & .Form.Item("tcnStatus_pre") & "&nRate=" & .Form.Item("tcnRate") & "&nPremium=" & .Form.Item("tcnPremium")
                        lblnPost = True
                    Else

                        If .QueryString.Item("WindowType") = "PopUp" Or .QueryString.Item("nMainAction") = "391" Then

                            '+ Se cambios la accion del tipo 1 a 6 de forma que cuando se registren los pago del convenio, se haga posible modificar
                            '+ modificar los mimos.
                            If Request.QueryString.Item("Action") = "Update" And CDbl(.QueryString.Item("nAction")) = 1 Then
                                lintAction = 6
                            Else
                                lintAction = .QueryString.Item("nAction")
                            End If


                            lblnPost = mobjCollectionTra.insPostCO003(mobjValues.StringToType(.Form.GetValues("tcnReceipt_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdPaydate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcdEffecdate_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnIntammou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tctPay_form_hdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble))
                            If lblnPost Then
                                mstrString = "&nHeight=225" & "&nReceipt=" & .Form.GetValues("tcnReceipt_hdr").GetValue(1 - 1) & "&sSelect=" & .QueryString.Item("nSelect") & "&dEffecdate=" & .Form.GetValues("tcdEffecdate_hdr").GetValue(1 - 1) & "&sPay_form=" & .Form.GetValues("tctPay_form_hdr").GetValue(1 - 1) & "&nAction=" & .Form.GetValues("tcnAction_hdr").GetValue(1 - 1) & "&nStatus_pre=" & .QueryString.Item("nStatus_pre") & "&nRate=" & .QueryString.Item("nRate") & "&nPremium=" & .Form.GetValues("tcnPremium_hdr").GetValue(1 - 1)
                            End If
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

                mobjCollectionTra = Nothing
            '+CO004			
            Case "CO004"
                mobjCollectionTra = New eCollection.Dir_debit
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                    Else
                        If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
                            sAction = "Query"
                        Else
                            sAction = "Update"
                        End If
                        'PAC	
                        If mobjValues.StringToType(Request.Form.Item("optWayNewPay"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            sAccountPACTBK = Request.Form.Item("tctAccountPACNew")
                        Else
                            'TBK 
                            sAccountPACTBK = Request.Form.Item("tctCardNumberNew")
                        End If


                        lblnPost = mobjCollectionTra.insPostCO004("CO004", mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBankPACNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnWay_Pay"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctClientPACNew"), sAccountPACTBK, Request.Form.Item("tctBankAuthPACNew"), mobjValues.StringToType(Request.Form.Item("cbeCardTypeNew"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcdCardExpirNew"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctClientCreditNew"), Request.Form.Item("cbeCause"), Request.Form.Item("hddsTypeDoc"), Request.Form.Item("optChangeway"), Request.Form.Item("optChangepremium"), mobjValues.StringToType(Request.Form.Item("optWayNewPay"), eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("valAgreementNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valOriginNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAFPCommiNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrencyNew"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("dtcClientPayNew"), Request.Form.Item("dtcClientEmpNew"), mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))

                    End If
                End With
                mobjCollectionTra = Nothing
            '+ CO005			
            Case "CO005"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    lblnPost = mobjCollectionTra.insPostCO005(mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("gmnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optAnul"), "2", mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble))
                    mstrCodispl = Request.QueryString.Item("sCodispl")
                    mstrQueryString = ""
                End With
                mobjCollectionTra = Nothing

            '+ CO009: Reverso de cobro o devolución
            Case "CO009", "CO09"
                mobjCollectionTra = New eCollection.Premium_mo
                With Request
                    If Request.Form.Item("optTypOper") <> "2" Then
                        lblnPost = mobjCollectionTra.insPostCO009(mobjValues.StringToDate(.Form.Item("tcdDate")), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), 0, .Form.Item("chkRelAll"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypOper"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdDateIncrease"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClient"))
                        mstrCodispl = Request.QueryString.Item("sCodispl")
                        mstrQueryString = ""
                        If lblnPost Then
                            Response.Write("<SCRIPT>")
                            Response.Write("alert (""" & "El proceso se realizó exitosamente " & """);")
                            Response.Write("</" & "Script>")
                        Else
                            Response.Write("<SCRIPT>")
                            Response.Write("alert (""" & "Ocurrió una falla en el proceso " & """);")
                            Response.Write("</" & "Script>")
                        End If
                    Else
                        lblnPost = True
                        ldtmDateIncrease = mobjValues.StringToType(.Form.Item("tcdDateIncrease"), eFunctions.Values.eTypeData.etdDate)

                        lobjColFormRef = New eCollection.ColformRef

                        If .Form.Item("chkRelAll") = "1" Then
                            ldblAmountPay = lobjColFormRef.Rea_RelAmount_1(mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), ldtmDateIncrease, eRemoteDB.Constants.intNull, 0, eRemoteDB.Constants.intNull)
                            llngCurrencyOri = 1
                            Session("OP006_Amount") = ldblAmountPay


                        Else
                            ldblAmountPay = lobjColFormRef.Rea_RelAmount_1(mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), ldtmDateIncrease, mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble))

                            If mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                                llngCurrencyOri = 1
                                Session("OP006_Amount") = ldblAmountPay
                            Else
                                llngCurrencyOri = .Form.Item("tcnCurrency")
                                Session("OP006_Amount") = .Form.Item("hddRel_amoun")
                            End If


                        End If

                        ldtmDateIncrease = mobjValues.TypeToString(lobjColFormRef.dValueDate, eFunctions.Values.eTypeData.etdDate)
                        lobjColFormRef = Nothing

                        Session("tcdDate") = mobjValues.StringToDate(.Form.Item("tcdDate"))
                        Session("cbeBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                        Session("valProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnReceiptNum") = mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnDigit") = mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnPaynumbe") = mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnContrat") = mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnDraft") = mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcnBordereaux") = mobjValues.StringToType(.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble)
                        Session("chkRelAll") = .Form.Item("chkRelAll")
                        Session("optTypOper") = mobjValues.StringToType(.Form.Item("optTypOper"), eFunctions.Values.eTypeData.etdInteger)

                        '+Se llama a la OP06-2 si la opción de ejecución es definitiva


                        '+ Tipo de orden de pago Table193

                        llngnPayOrderTyp = 2
                        Session("OP006_sCodispl") = "CO009"
                        mstrCodispl = "OP06-2"
                        Session("OP006_dReqDate") = Request.Form.Item("tcdDate")

                        mstrQueryString = "&sCodisplOri=CO009" & "&sBenef=" & .Form.Item("hddClient") & "&nConcept=24" & "&nTypesupport=0" & "&dEffecdate=" & .Form.Item("tcdDate") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & Session("OP006_Amount") & "&nAmountPay=" & ldblAmountPay & "&nPayOrderTyp=" & llngnPayOrderTyp & "&nBranch=" & .Form.Item("hddnBranch") & "&nProduct=" & .Form.Item("hddnProduct") & "&nPolicy=" & .Form.Item("hddnPolicy") & "&nCertif=" & .Form.Item("hddnCertif") & "&dRescdate=" & .Form.Item("hdddEffecdate") & "&sSurrType=" & .Form.Item("hddsSurrType") & "&sProcessType=" & .Form.Item("hddsProcessType") & "&sRequest=" & .Form.Item("chkRequest") & "&sSurrPayWay=" & .Form.Item("hddsSurrPayWay") & "&nSurrAmount=" & .Form.Item("tcnSurrAmount") & "&nCurrency=" & llngCurrencyOri & "&nCurrencypay=1" & "&sClient=" & .Form.Item("tctClient") & "&nBranchPay=" & .Form.Item("cbeBranch") & "&nProductPay=" & .Form.Item("valProduct") & "&nPolicyPay=" & .Form.Item("tcnPolicy") & "&nCertifPay=" & .Form.Item("tcnCertif") & "&nProponum=" & .Form.Item("hddnProponum") & "&nBalance=" & .Form.Item("hddnBalance") & "&nOperat=" & .Form.Item("hddnOperat") & "&dDateIncrease=" & ldtmDateIncrease & "&sAnulReceipt=" & .Form.Item("hddsAnulReceipt")
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO501: Rechazo de pagos de PAC/Transbank
            Case "CO501"
                mobjCollectionTra = New eCollection.Bulletin


                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&dExpirdat=" & .Form.Item("tcdExpiriDate") & "&nWay_pay=" & .Form.Item("cbeWay_pay") & "&nBank=" & .Form.Item("cbeBank") & "&ncod_agree=" & .Form.Item("valAgreement") & "&sProcess=1&sKey=" & .QueryString.Item("sKey")
                        lblnPost = True
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            If mobjValues.StringToType(.Form.Item("tctnBank"), eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
                                lstrDocument = .Form.Item("tctDocument")
                            Else
                                lstrDocument = .Form.Item("tctAccount")
                            End If
                            lblnPost = mobjCollectionTra.insPostCO501Upd(.QueryString("sKey"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tctBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tctnBank"), eFunctions.Values.eTypeData.etdDouble), lstrDocument, mobjValues.StringToType(.Form.Item("tctAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sProcess"), mobjValues.StringToType(.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdLong))
                        Else
                            If Not IsNothing(Request.Form.Item("tcnString")) Then
                                lblnPost = mobjCollectionTra.insPostCO501(.QueryString("sKey"), mobjValues.StringToType(.Form.GetValues("tcnWay_payHdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            Else
                                lblnPost = True
                            End If

                            If lblnPost = True Then
                                Call insPrintCollectionTra("CO501")
                            End If
                        End If

                        mstrString = "&dExpirdat=" & .Form.Item("tcdExpirDatHdr") & "&nWay_pay=" & .Form.Item("tcnWay_payHdr") & "&nBank=" & .Form.Item("tcnBankHdr") & "&nCauseNull=" & .Form.Item("tcnCauseNullHdr") & "&ncod_agree=" & .Form.Item("ncod_agree") & "&sProcess=" & .QueryString.Item("sProcess") & "&sKey=" & .QueryString.Item("sKey")

                        If mobjValues.StringToType(.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdLong) <> eRemoteDB.Constants.intNull Then
                            mstrString = mstrString & "&nRow=" & .QueryString.Item("nRow")
                        End If
                    End If
                End With

                mobjCollectionTra = Nothing

            '+ CO514: Mantenimiento de boletines
            Case "CO514"
                mobjCollectionTra = New eCollection.Bulletins_det

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                    Else
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            If .QueryString.Item("nMainAction") <> "401" Then
                                lblnPost = mobjCollectionTra.insPostC0514("CO514", mobjValues.StringToType(Request.Form.Item("tctNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            Else
                                lblnPost = True
                            End If
                        End If
                    End If
                End With

                mobjCollectionTra = Nothing

            '+ CO632: Generación manual de boletines.
            Case "CO632"

                lobjNumerator = New eGeneral.GeneralFunction
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        '+ Si la acción es registrar.				
                        If mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = 301 Then
                            '+ Se obtiene el número del boletín.
                            llngBulletins = lobjNumerator.Find_Numerator(60, 0, Session("nUsercode"))
                        Else
                            llngBulletins = mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble)
                        End If

                        Session("nBulletins") = llngBulletins
                        Session("dCollectDate") = .Form.Item("tcdCollectDate")
                        Session("nOneTime") = "1"
                        Session("sStyle_bull") = .Form.Item("optStyle_bull")

                        If CDbl(.Form.Item("cbeCurrencyBul")) = 0 Then
                            llngCurrencyBul = 1
                        Else
                            llngCurrencyBul = .Form.Item("cbeCurrencyBul")
                        End If

                        mstrString = "&nHeight=225" & "&nInsur_area=" & .Form.Item("cbeInsur_area") & "&nBulletins=" & llngBulletins & "&dCollectDate=" & .Form.Item("tcdCollectDate") & "&sIndColl_exp=" & .Form.Item("chkCollect_exp") & "&sStyle_bull=" & .Form.Item("optStyle_bull") & "&sQueryOption=" & .Form.Item("optQueryOption") & "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&sClient=" & .Form.Item("dtcClientK") & "&nReceipt=" & .Form.Item("tcnReceipt") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sStatus=" & .Form.Item("tctStatus") & "&nCurrencyBul=" & llngCurrencyBul

                        lblnPost = True
                    End If
                End With

                lobjNumerator = Nothing

            '+ CO632A: Generación manual de boletines (Detalle).
            Case "CO632A"
                mobjCollectionTra = New eCollection.T_bulletins_det

                With Request
                    '+ Ventana puntual
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjCollectionTra.insPostCO632AUpd(.QueryString("Action"), 1, mobjValues.StringToType(.Form.Item("nBulletinsHdr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dCollectDateHdr"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcdStatDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdLimitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTratypei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCod_agree"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sIndColl_expHdr"), .Form.Item("sStyle_bullHdr"), .Form.Item("sQueryOptionHdr"), .Form.Item("tctCollector"), mobjValues.StringToType(.Form.Item("nInsur_areaHdr"), eFunctions.Values.eTypeData.etdDouble))

                        If lblnPost Then
                            mstrString = "&nInsur_area=" & .Form.Item("nInsur_areaHdr") & "&nBulletins=" & .Form.Item("nBulletinsHdr") & "&dCollectDate=" & .Form.Item("dCollectDateHdr") & "&sIndColl_exp=" & .Form.Item("sIndColl_expHdr") & "&sQueryOption=" & .Form.Item("sQueryOptionHdr") & "&sCertype=2" & "&nBranch=" & .Form.Item("nBranchHdr") & "&nProduct=" & .Form.Item("nProductHdr") & "&nPolicy=" & .Form.Item("nPolicyHdr") & "&sClient=" & .Form.Item("sClientHdr") & "&nReceipt=" & .Form.Item("nReceiptHdr") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sStatus=" & .Form.Item("sStatusHdr") & "&nCurrencyBul=" & .QueryString.Item("nCurrencyBul")
                        End If
                        '+ Ventana masiva
                    Else

                        '+ Si la acción a ejecutar es diferente de consulta.
                        If .QueryString.Item("nMainAction") = "401" Then
                            lblnPost = True
                        Else
                            '+ Si existen registros a procesar.
                            If mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                                If mobjCollectionTra.insPostCO632A(mobjValues.StringToType(.Form.GetValues("nBulletinsHdr").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("sQueryOptionHdr").GetValue(1 - 1), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then

                                    lblnPost = True
                                    lobjGeneral = New eGeneral.GeneralFunction
                                    mstrKey = lobjGeneral.getsKey(Session("nUsercode"))
                                    lobjGeneral = Nothing
                                    Call insPrintCollectionTra("COL701")
                                Else
                                    lblnPost = False
                                End If
                            End If
                        End If
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO633: Suspensión/Reactivación de cobranzas.
            Case "CO633"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        mstrString = "&nHeight=225" & "&nInsur_area=" & .Form.Item("cbeInsur_area") & "&nTypOper=" & .Form.Item("optTypOper") & "&nTypDoc=1&sSus_origi=1&dOperation=" & .Form.Item("tcdOperation") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nReceipt=" & .Form.Item("tcnReceipt") & "&nContrat=" & .Form.Item("tcnContrat") & "&nDraft=" & .Form.Item("tcnDraft") & "&dCollSus_ini=" & .Form.Item("tcdCollSus_ini") & "&dCollSus_end=" & .Form.Item("tcdCollSus_end") & "&nSus_reason=" & .Form.Item("cbeSus_reason")
                        lblnPost = True
                    End If
                End With

            '+ CO633A: Suspensión/Reactivación de cobranzas.
            Case "CO633A"
                '+ Si existen registros a procesar.
                mobjCollectionTra = New eCollection.Premium
                'lblnPost = false
                lblnPost = mobjCollectionTra.insPostCO633A("CO633A", mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dOperation"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSus_origi"), mobjValues.StringToType(Request.QueryString.Item("nTypDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dCollSus_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dCollSus_end"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nSus_reason"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))


                mobjCollectionTra = Nothing

            '+ CO634: Traspaso de pago.
            Case "CO634"
                mobjCollectionTra = New eCollection.Premium

                With Request
                    lblnPost = mobjCollectionTra.insPostCO634(mobjValues.StringToType(.Form.Item("optTypTras"), eFunctions.Values.eTypeData.etdDouble), "2", "2", mobjValues.StringToType(.Form.Item("cbeBranchOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponumOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponumDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReceiptDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountTrasOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnContratOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnContratDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraftOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDraftDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjCollectionTra = Nothing

            '+ CO675: Cambio de fecha de generación de cobranzas de un recibo
            Case "CO675"
                mobjCollectionTra = New eCollection.Premium
                With Request
                    lblnPost = mobjCollectionTra.insPostCO675("CO675", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("tcnReceiptNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPaynumbe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNewLimitDate")), mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valDraft"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjCollectionTra = Nothing

            '+ CO685: Cobradores  
            Case "CO685"
                mobjCollectionTra = New eCollection.Collector


                If Request.QueryString.Item("nZone") = "2" Then

                    If Session("nMainAction") <> 401 Then

                        With Request
                            lblnPost = mobjCollectionTra.insPostCO685(Session("nMainAction"), Session("nCollector"), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcnColType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dtInputDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnConType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnInsur_area"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLegal_Sch"), eFunctions.Values.eTypeData.etdDouble))
                        End With
                    Else
                        lblnPost = True
                    End If
                Else
                    lblnPost = True
                End If

                '+ Si la acción es consultar se buscan los valores.

                mobjCollectionTra = Nothing

            '+ CO700 Generación de facturas/notas de crédito
            Case "CO700"
                mobjCollectionTra = New eCollection.Billss
                Dim nInsur_Area As Int32
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        If mstrKey = vbNullString Then
                            lobjGeneral = New eGeneral.GeneralFunction

                            mstrKey = lobjGeneral.getsKey(Session("nUsercode"))

                            lobjGeneral = Nothing
                        End If
                        nInsur_Area = Session("nInsur_area")
                        If .Form.Item("cbeBranch") = 71 And .Form.Item("valProduct") = 1 Then
                            nInsur_Area = 12
                        End If

                        'mstrString = "&nHeight=220" & "&nInsur_area=" & Session("nInsur_area") & "&sDocType=" & .Form.Item("optDocType") & "&sBillType=" & .Form.Item("optBillType") & "&sProcess=" & .Form.Item("optProcess") & "&sModeT=" & .Form.Item("optMode") & "&dDateIni=" & .Form.Item("tcdDateIni") & "&dDateEnd=" & .Form.Item("tcdDateEnd") & "&dDatePrint=" & .Form.Item("tcdDatePrint") & "&nBill=" & .Form.Item("tcnBill") & "&sClient=" & .Form.Item("dtcClient") & "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nAgency=" & .Form.Item("tcnAgency") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&dValDate=" & .Form.Item("tcdValDate") & "&sKey=" & mstrKey
                        mstrString = "&nHeight=220" & "&nInsur_area=" & nInsur_Area & "&sDocType=" & .Form.Item("optDocType") & "&sBillType=" & .Form.Item("optBillType") & "&sProcess=" & .Form.Item("optProcess") & "&sModeT=" & .Form.Item("optMode") & "&dDateIni=" & .Form.Item("tcdDateIni") & "&dDateEnd=" & .Form.Item("tcdDateEnd") & "&dDatePrint=" & .Form.Item("tcdDatePrint") & "&nBill=" & .Form.Item("tcnBill") & "&sClient=" & .Form.Item("dtcClient") & "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nAgency=" & .Form.Item("tcnAgency") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&dValDate=" & .Form.Item("tcdValDate") & "&sKey=" & mstrKey

                        '+ Si el proceso es masivo
                        If .Form.Item("optProcess") = "2" Then
                            If mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True) = 301 Then
                                'If mobjCollectionTra.Find_CO700(mstrKey, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optDocType"), .Form.Item("optBillType"), mobjValues.StringToType(.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("dtcClient"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate)) Then
                                If mobjCollectionTra.Find_CO700(mstrKey, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optDocType"), .Form.Item("optBillType"), mobjValues.StringToType(.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("dtcClient"), nInsur_Area, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate)) Then

                                    lobjBills = New eCollection.Bills
                                    'If lobjBills.insPostCO700A(mstrKey, .Form.Item("optProcess"), .Form.Item("optMode"), mobjValues.StringToType(.Form.Item("tcdDatePrint"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optDocType"), .Form.Item("optBillType"), mobjValues.StringToType(.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("dtcClient"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate)) Then
                                    If lobjBills.insPostCO700A(mstrKey, .Form.Item("optProcess"), .Form.Item("optMode"), mobjValues.StringToType(.Form.Item("tcdDatePrint"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optDocType"), .Form.Item("optBillType"), mobjValues.StringToType(.Form.Item("tcnBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("dtcClient"), nInsur_Area, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate)) Then

                                        mdtmDateIni = mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate)
                                        mdtmDateEnd = mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate)
                                        mdtmDatePrint = mobjValues.StringToType(.Form.Item("tcdDatePrint"), eFunctions.Values.eTypeData.etdDate)
                                        mstrMode = .Form.Item("optMode")
                                        Call insPrintCollectionTra("CO700")
                                    End If
                                    lobjBills = Nothing
                                End If
                            End If
                        End If
                        lblnPost = True
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO700A: Generación de facturas/notas de crédito
            Case "CO700A"
                mobjCollectionTra = New eCollection.Bills
                With Request
                    lblnPost = True
                    If Request.QueryString.Item("nMainAction") = "301" Then

                        lblnPost = mobjCollectionTra.insPostCO700A(Request.QueryString.Item("sKey"), Request.QueryString.Item("sProcess"), Request.QueryString.Item("sModeT"), mobjValues.StringToType(Request.QueryString.Item("dDatePrint"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sDocType"), Request.QueryString.Item("sBillType"), mobjValues.StringToType(Request.QueryString.Item("nBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("dtcClient"), mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate))
                        If lblnPost Then
                            mstrKey = Request.QueryString.Item("sKey")
                            mdtmDateIni = mobjValues.StringToType(Request.QueryString.Item("dDateIni"), eFunctions.Values.eTypeData.etdDate)
                            mdtmDateEnd = mobjValues.StringToType(Request.QueryString.Item("dDateEnd"), eFunctions.Values.eTypeData.etdDate)
                            mdtmDatePrint = mobjValues.StringToType(Request.QueryString.Item("dDatePrint"), eFunctions.Values.eTypeData.etdDate)
                            mstrMode = Request.QueryString.Item("sModeT")
                            'Call insPrintCollectionTra("CO700")
                        End If
                    End If
                End With
                mobjCollectionTra = Nothing

            '+ CO0722: Actualización de mandatos por póliza
            Case "CO722"
                mobjCollectionTra = New ePolicy.DirDebit
                With Request
                    lblnPost = mobjCollectionTra.insPostCO722(.Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToDate(.Form.Item("tcdDate")), .Form.Item("tctBankAuthNew"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjCollectionTra = Nothing

            '+ CO635: Asignación de cartera a un cobrador
            Case "CO635"


                lintCountAux = 0
                llngselected = 0

                mobjCollectionTra = New eCollection.Premium

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&nCollector=" & .Form.Item("valCollectorPre") & "&sColltype=" & .Form.Item("optColltype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nAgency=" & mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True) & "&nWay_Pay=" & .Form.Item("cbeWay_Pay") & "&dLimitdate=" & .Form.Item("tcdLimitdate")

                        lblnPost = True
                    Else
                        If CDbl(.QueryString.Item("nMainAction")) = 401 Then
                            lblnPost = True
                        End If

                        If Not IsNothing(.Form.Item("hddSelAux")) Then
                            llngsize = .Form.Item("hddSelAux").Split(",").Count
                        Else
                            llngsize = 0
                        End If

                        If llngsize >= 1 Then
                            For lintCountAux = 1 To llngsize
                                If llngsize = 1 Then
                                    lstrhddBranch = .Form.Item("hddBranch")
                                    lstrhddEffecdate = .Form.Item("hddEffecdate")
                                    lstrtctCertype = .Form.Item("tctCertype")
                                    lstrhddProduct = .Form.Item("hddProduct")
                                    lstrhddPolicy = .Form.Item("hddPolicy")
                                    lstrhddReceipt = .Form.Item("hddReceipt")
                                    lstrtcnContrat = .Form.Item("tcnContrat")
                                    lstrtcnDraft = .Form.Item("tcnDraft")
                                    lstrhddBulletins = .Form.Item("hddBulletins")
                                    lstrhddSelAux = .Form.Item("hddSelAux")
                                ElseIf llngsize > 1 Then
                                    lstrhddBranch = .Form.GetValues("hddBranch").GetValue(lintCountAux - 1)
                                    lstrhddEffecdate = .Form.GetValues("hddEffecdate").GetValue(lintCountAux - 1)
                                    lstrtctCertype = .Form.GetValues("tctCertype").GetValue(lintCountAux - 1)
                                    lstrhddProduct = .Form.GetValues("hddProduct").GetValue(lintCountAux - 1)
                                    lstrhddPolicy = .Form.GetValues("hddPolicy").GetValue(lintCountAux - 1)
                                    lstrhddReceipt = .Form.GetValues("hddReceipt").GetValue(lintCountAux - 1)
                                    lstrtcnContrat = .Form.GetValues("tcnContrat").GetValue(lintCountAux - 1)
                                    lstrtcnDraft = .Form.GetValues("tcnDraft").GetValue(lintCountAux - 1)
                                    lstrhddBulletins = .Form.GetValues("hddBulletins").GetValue(lintCountAux - 1)
                                    lstrhddSelAux = .Form.GetValues("hddSelAux").GetValue(lintCountAux - 1)
                                End If
                                If (CDbl(.QueryString.Item("nMainAction")) = 301 And lstrhddSelAux = "1") Or (CDbl(.QueryString.Item("nMainAction")) = 302 And lstrhddSelAux = "2") Then
                                    llngselected = llngselected + 1
                                    lblnPost = mobjCollectionTra.insPostCO635("CO635", "Update", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCollector"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrhddEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(lstrhddBranch, eFunctions.Values.eTypeData.etdDouble), lstrtctCertype, mobjValues.StringToType(lstrhddProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrhddPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrhddReceipt, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrtcnContrat, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrtcnDraft, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrhddBulletins, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrhddSelAux, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lintCountAux), eFunctions.Values.eTypeData.etdDouble))
                                End If
                            Next
                            If Request.Form.Item("chkPrint") = "1" Then
                                mobjDocuments = New eReports.Report
                                With mobjDocuments
                                    .sCodispl = "CO635"
                                    .ReportFilename = "COL635.rpt"
                                    .SetStorProcParam(1, Request.Form.Item("hddnCollector"))
                                    Response.Write((.Command))
                                End With
                                mobjDocuments = Nothing
                            End If
                        Else
                            lblnPost = True
                        End If
                    End If

                    If (llngselected = 0) Then
                        lblnPost = True
                    End If
                End With

                mobjCollectionTra = Nothing

            '+ CO788: Devolución de Cobro
            Case "CO788", "CO788C"
                mobjCollectionTra = New eCollection.ColformRef
                '+ Devolución con Cargo en cuenta corriente o No Tiene			
                If Request.Form.Item("OptDev") <> "1" Then
                    lblnPost = mobjCollectionTra.insPostCO788(mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeTypeDoc"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnNumDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIncrease"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("dtcClient"), mobjValues.StringToType(Request.Form.Item("OptDev"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("OptDocRev"), eFunctions.Values.eTypeData.etdLong), Session("nUserCode"), mobjValues.StringToType(Request.Form.Item("hddSequence"), eFunctions.Values.eTypeData.etdLong))
                    mstrCodispl = Request.QueryString.Item("sCodispl")
                Else
                    '+ Devolución por orden de pago		
                    lblnPost = True

                    ldtmDateIncreaseCO788 = Request.Form.Item("tcdDateIncrease")
                    mstrCodispl = "OP06-2"

                    If Request.Form.Item("OptDocRev") = "1" Then
                        Session("OP006_Amount") = Request.Form.Item("hddDoc_amount")
                    Else
                        ldblAmountPayCO788 = mobjCollectionTra.Rea_RelAmoun(mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, 0, eRemoteDB.Constants.intNull)
                        Session("OP006_Amount") = FormatNumber(ldblAmountPayCO788, 0)
                    End If

                    Session("OP006_sCodispl") = "CO788"
                    Session("OP006_dReqDate") = Request.Form.Item("tcdDate")
                    Session("dDate") = mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate)
                    Session("nTypeDoc") = mobjValues.StringToType(Request.Form.Item("cbeTypeDoc"), eFunctions.Values.eTypeData.etdLong, True)
                    Session("nNumDoc") = mobjValues.StringToType(Request.Form.Item("tcnNumDoc"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nDraft") = mobjValues.StringToType(Request.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdLong)
                    Session("nBordereaux") = mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble)
                    Session("dDateIncrease") = mobjValues.StringToType(Request.Form.Item("tcdDateIncrease"), eFunctions.Values.eTypeData.etdDate)
                    Session("sClient") = Request.Form.Item("dtcClient")
                    Session("OptDev") = mobjValues.StringToType(Request.Form.Item("OptDev"), eFunctions.Values.eTypeData.etdLong)
                    Session("OptDocRev") = mobjValues.StringToType(Request.Form.Item("OptDocRev"), eFunctions.Values.eTypeData.etdLong)
                    Session("nUserCode") = Session("nUserCode")
                    Session("nSequence") = mobjValues.StringToType(Request.Form.Item("hddSequence"), eFunctions.Values.eTypeData.etdLong)

                    mstrQueryString = "&sCodisplOri=CO788" & "&sBenef=" & Request.Form.Item("dtcClient") & "&nConcept=24" & "&dEffecdate=" & Request.Form.Item("tcdDate") & "&nAmount=" & Session("OP006_Amount") & "&nAmountPay=" & Session("OP006_Amount") & "&nPayOrderTyp=2" & "&nCurrency=1" & "&nCurrencypay=1" & "&sClient=" & Request.Form.Item("dtcClient") & "&dDateIncrease=" & ldtmDateIncreaseCO788

                End If
                mobjCollectionTra = Nothing

            Case "CO982"

                mobjCollectionTra = New eCollection.Reject_cause
                lcolReject_causes = New eCollection.Reject_causes
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&nBank_code=" & .Form.Item("cbeBankExt") & "&nRejectcause=" & .Form.Item("cbeCodRej") & "&nYear=" & .Form.Item("tcnYear") & "&nMonth=" & .Form.Item("cboMonth")
                        lobjGeneral = New eGeneral.GeneralFunction
                        mstrKey = lobjGeneral.getsKey(Session("nUsercode"))
                        Session("mstrKey") = mstrKey
                        lobjGeneral = Nothing
                        If lcolReject_causes.InsPreCO982(mstrKey, Session("nUserCode"), mobjValues.StringToType(Request.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cboMonth"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cbeCodRej"), eFunctions.Values.eTypeData.etdDouble)) Then
                            lblnPost = True
                        End If
                    Else
                        If Not IsNothing(Request.Form.Item("tcnString")) Then
                            lblnPost = mobjCollectionTra.insPostCO982(Request.Form.Item("tcnString"))
                        End If
                        If lblnPost = True Then
                            Call insPrintCollectionTra("CO982")
                        End If
                    End If
                End With
                mobjCollectionTra = Nothing

        End Select

        insPostCollectionTra = lblnPost
    End Function

    '%insGetNewClient. Esta función se encarga de conseguir un código de cliente 
    '% para los clientes nuevos (Provisionales). 
    '-------------------------------------------------------------------------- 
    Private Function insGetNewCollector(ByVal llngCollector As Object) As Object
        '-------------------------------------------------------------------------- 
        Dim lclsCollector As eCollection.Collector

        '+Si la acción es registrar, se busca automáticamente el código del Cobrador  
        If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
            If llngCollector = "0" Or llngCollector = vbNullString Then
                lclsCollector = New eCollection.Collector
                llngCollector = lclsCollector.GetNewCollectorCode(Session("nUsercode"))
                Do While (lclsCollector.Find(llngCollector, Request.Form.Item("dtcClient")))
                    llngCollector = lclsCollector.GetNewCollectorCode(Session("nUsercode"))
                Loop
                Response.Write("<SCRIPT>")
                Response.Write("alert (""" & "Nuevo Codigo de Cobrador: " & llngCollector & """);")
                Response.Write("</" & "Script>")
                lclsCollector = Nothing
            End If
        End If
        insGetNewCollector = llngCollector
    End Function

    '% insPrintCollectionTra: Se encarga de generar el reporte correspondiente de la transacción pasada como parametro del módulo.  
    '--------------------------------------------------------------------------------------------  
    Private Sub insPrintCollectionTra(ByRef sCodispl As Object)
        '--------------------------------------------------------------------------------------------  
        Dim lobjDocuments As eReports.Report
        lobjDocuments = New eReports.Report
        Select Case sCodispl

            '+ CO700: Listado preliminar y definitivo de facturas.
            Case "CO700"
                With lobjDocuments
                    If mstrMode = "1" Then
                        .ReportFilename = "COL700_A.rpt"
                        .sCodispl = "CO700"
                        .SetStorProcParam(1, mstrKey)
                        .SetStorProcParam(2, .setdate(mdtmDateIni))
                        .SetStorProcParam(3, .setdate(mdtmDateEnd))
                        .setStorProcParam(4, mstrMode)
                        Response.Write((.Command))
                        .Reset()
                    End If

                    '+ Si el modo de ejecución es definitivo            
                    If mstrMode = "2" Then
                        .ReportFilename = "COL700_A.rpt"
                        .sCodispl = "CO700"
                        .setStorProcParam(1, mstrKey)
                        .setStorProcParam(2, .setdate(mdtmDateIni))
                        .setStorProcParam(3, .setdate(mdtmDateEnd))
                        .setStorProcParam(4, mstrMode)
                        Response.Write((.Command))
                        .Reset()

                        .ReportFilename = "COL700_B.rpt"
                        .sCodispl = "CO700"
                        .bTimeOut = True
                        .nTimeOut = 3000
                        .nTop = 250
                        .setStorProcParam(1, mstrKey)
                        .setStorProcParam(2, .setdate(mdtmDatePrint))
                        Response.Write((.Command))
                        mblnTimeOut = True
                    End If
                End With
            '+ Se elimina la información procesada de la tabla temporal.
            'Set lobjDocuments = Server.CreateObject("eCollection.Bills")
            'lobjDocuments.Delete(mstrKey)
            'Set lobjDocuments = Nothing

            '+ COL701: Impresión de boletines
            Case "COL701"

                With lobjDocuments
                    .sCodispl = "COL701"
                    .ReportFilename = "COL701A.rpt"
                    .SetStorProcParam(1, 4)
                    .SetStorProcParam(2, Request.Form.GetValues("nInsur_areaHdr").GetValue(1 - 1))
                    .SetStorProcParam(3, 0)
                    .SetStorProcParam(4, .setdate(Request.Form.GetValues("dCollectDateHdr").GetValue(1 - 1)))
                    .SetStorProcParam(5, Request.Form.GetValues("nBulletinsHdr").GetValue(1 - 1))
                    .SetStorProcParam(6, Request.Form.GetValues("nBulletinsHdr").GetValue(1 - 1))
                    .SetStorProcParam(7, "1")
                    .SetStorProcParam(8, "2") 'Impresión Manual de Boletines
                    .SetStorProcParam(9, mstrKey)
                    .SetStorProcParam(10, Session("nUsercode"))
                    Response.Write((.Command))
                    .Reset()

                    .ReportFilename = "COL701B.rpt"
                    .sCodispl = "COL701"
                    .SetStorProcParam(11, mstrKey)
                    .bTimeOut = True
                    .nTimeOut = 10000
                    Response.Write((.Command))
                    .Reset()

                    .ReportFilename = "COL701C.rpt"
                    .sCodispl = "COL701"
                    .SetStorProcParam(12, mstrKey)
                    .bTimeOut = True
                    .nTimeOut = 19000
                    Response.Write((.Command))
                    mblnTimeOut = True
                End With

            '+ CO501: Listado de boletines rechazados
            Case "CO501"
                With lobjDocuments
                    .ReportFilename = "COL501.rpt"
                    .sCodispl = "CO501"
                    .SetStorProcParam(1, Request.QueryString.Item("nWay_pay"))
                    .SetStorProcParam(2, .setdate(Request.QueryString.Item("dExpirdat")))
                    .SetStorProcParam(3, Request.QueryString.Item("nBank"))
                    .SetStorProcParam(4, Request.QueryString.Item("ncod_agree"))
                    Response.Write((.Command))
                    .Reset()
                End With

            '+ CO501: Cambio de via de pago por rechazo de cobranza
            Case "CO982"
                With lobjDocuments
                    .ReportFilename = "CO982.rpt"
                    .sCodispl = "CO501"
                    '.SetStorProcParam 1, Request.Form("tcnString")
                    .SetStorProcParam(1, Session("mstrKey"))
                    Response.Write((.Command))
                    .Reset()
                End With

                Server.ScriptTimeOut = 90
        End Select
        lobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Collection&sProject=CollectionTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mblnTimeOut = False
%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
End With
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>



	
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 7 $|$$Date: 8/10/09 3:34p $|$$Author: Gletelier $"
	     
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>

<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValCollectionTra
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CollectionTraErrors"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	
	If insPostCollectionTra Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If mblnTimeOut Then
					Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Select Case Request.QueryString.Item("sCodispl")
							Case "CO09"
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							Case "CO009"
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							Case "CO633A"
								Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO633'</SCRIPT>")
							Case "CO788"
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							Case "CO788C"
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							Case "CO632A"
								If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionquery) Then
									Response.Write("<SCRIPT>setTimeout(""" & "top.document.location.href=" & "'/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO632'" & """,20000);</SCRIPT>")
								Else
									Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO632';</SCRIPT>")
								End If
							Case "CO700A"
								Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO700'</SCRIPT>")
							Case Else
								Response.Write("<SCRIPT>window.close();top.document.location.href= '' + top.document.location.href;</SCRIPT>")
						End Select
					Else
						If Request.Form.Item("sCodisplReload") = "CO675" Or Request.Form.Item("sCodisplReload") = "CO634" Then
							Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = "CO009" Or Request.Form.Item("sCodisplReload") = "CO788" Or Request.Form.Item("sCodisplReload") = "CO788C" Or Request.Form.Item("sCodisplReload") = "CO09" Or Request.Form.Item("sCodisplReload") = "CO005" Then
								Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/Common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							ElseIf Request.Form.Item("sCodisplReload") = "CO633A" Then 
								Response.Write("<SCRIPT>window.close();opener.top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO633'</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
							End If
						End If
					End If
				End If
			Else
				If mblnTimeOut Then
					Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Select Case Request.QueryString.Item("sCodispl")
							Case "CO004"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case "CO514"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case "CO685"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case "CO003", "CO501"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case "CO632"
								Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO632&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sConfig=InSequence" & mstrString & "'</SCRIPT>")
							Case "CO633"
								Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO633&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sConfig=InSequence" & mstrString & "'</SCRIPT>")
							Case "CO635"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case "CO700"
								'+ Si el proceso es puntual se llama a la CO700A, sino se recarga la CO700 (proceso masivo)
								If Request.Form.Item("optProcess") = "1" Then
									Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Collection&sProject=CollectionTra&sCodispl=CO700&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sConfig=InSequence" & mstrString & "'</SCRIPT>")
								Else
									Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
								End If
							Case "CO982"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Case Else
								Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						End Select
					Else
						If Request.Form.Item("sCodisplReload") = "CO009" Or Request.Form.Item("sCodisplReload") = "CO788" Or Request.Form.Item("sCodisplReload") = "CO788C" Or Request.Form.Item("sCodisplReload") = "CO09" Or Request.Form.Item("sCodisplReload") = "CO005" Then
							Response.Write("<SCRIPT>window.close();top.fraHeader.document.location=""" & Request.QueryString.Item("sCodispl") & "_K" & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
						Else
							If Request.QueryString.Item("sCodispl") = "CO004" Then
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
							Else
								If Request.Form.Item("sCodisplReload") = "CO634" Then
									Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
								Else
									Response.Write("<SCRIPT>window.close();top.document.location.reload();</SCRIPT>")
								End If
							End If
						End If
					End If
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "CO003"
					Response.Write("<SCRIPT>top.opener.document.location.href='CO003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & "'</SCRIPT>")
				Case "CO514", "CO632", "CO633"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302" & mstrString & "'</SCRIPT>")
				Case "CO501"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=302" & mstrString & "'</SCRIPT>")
				Case "CO632A"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrString & "'</SCRIPT>")
				Case "CO633A"
					Response.Write("<SCRIPT>top.opener.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=CO633';</SCRIPT>")
				Case "CO685"
					Response.Write("<SCRIPT>top.opener.document.location.href='CO685_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=301'</SCRIPT>")
				Case "CO700A"
					Response.Write("<SCRIPT>top.opener.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=CO700';</SCRIPT>")
			End Select
		End If
	Else
		Response.Write("<SCRIPT>alert('No se pudo realizar la actualización');</SCRIPT>")
	End If
	
End If
mobjValues = Nothing
mobjCollectionTra = Nothing

%>
</BODY>
</HTML>







