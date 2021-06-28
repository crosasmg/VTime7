<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>
<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="ePolicy" %>
<%@ Import Namespace="eProduct" %>
<%@ Import Namespace="eReports" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '-Variable objeto para el manejo de las funciones generales
    Dim mobjValues As eFunctions.Values

    '-Variable para almacenar los errores que retorna el método insvalSequence
    Dim mstrErrors As String

    '-Variable para el manejo de los métodos Val y Post
    Dim mobjClaimSeq As Object

    '- Se define la variable para almacenar la nueva dirección de la SI001
    Dim mstrLocationSI001 As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '-Variable que guarda el query string a pasar a la ventana asociada a la popup
    Dim mstrQueryString As String

    '+ Se definen variables auxiliares usadas en el cálculo de reservas SI007
    Dim mdblTotal As Double
    Dim mdblTotal2 As Double
    Dim lintValue As  Integer

    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String
        Dim lintCounter As Integer
        '--------------------------------------------------------------------------------------------
        '- Variables auxiliares usadas en la SI007    
        Dim lintCase_num As String
        Dim lintDeman_type As String
        Dim lstrClient As String
        Dim lintCover As Object
        Dim nCountAux As Integer

        Select Case Request.QueryString("sCodispl")

            '+ Solicitud de clave para tratamiento de siniestros          
            Case "SI001"
                mobjClaimSeq = New eClaim.Claim

                With Request
                    insvalSequence = mobjClaimSeq.insValSI001(mobjValues.StringToType(.Form("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), .Form("tctRequest_nu"), mobjValues.StringToType(.Form("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnReference"), eFunctions.Values.eTypeData.etdDouble), Session("nCompanyUser"), Session("nUsercode"), mobjValues.StringToType(.Form("tcdOccurrdat"), eFunctions.Values.eTypeData.etdDate))
                    Session("bPolicyVigency") = mobjClaimSeq.bPolicyVigency
                    Session("dOccurdate_l") = mobjClaimSeq.dOccurdate_l
                End With
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing

                '+ SI004: Datos Generales del Siniestro
            Case "SI004"
                mobjClaimSeq = New eClaim.Claim
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        insvalSequence = mobjClaimSeq.insValSI004(Request.QueryString("sCodispl"),
                                                              Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClientCode"), mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("dPrescDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("dDeclaDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("dLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("dOccurDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("nClaimCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboRType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("sBrancht")), eFunctions.Values.eTypeData.etdDouble), .Form("tctLastName"), .Form("tctFirstName"), .Form("cbeReclaim") = 1, mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), False)
                    Else
                        If Request.Form("hddTotalLoss") <> 2 Then
                            mobjClaimSeq.sClaimTyp = "1"
                        Else
                            mobjClaimSeq.sClaimTyp = "2"
                        End If

                        mobjClaimSeq.nOfficeAgen = mobjValues.StringToType(Request.Form("cbeOfficeAgen"), Values.eTypeData.etdInteger)

                        insvalSequence = mobjClaimSeq.insValSI004(Request.QueryString("sCodispl"), Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, .Form("tctClient"), mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("gmdPrescDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdDeclaDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdOccurDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeClaimCaus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboRType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("sBrancht")), eFunctions.Values.eTypeData.etdDouble), vbNullString, vbNullString, eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), True)


                    End If
                End With

                '+ Reservas de Siniestros            
            Case "SI007"
                mobjClaimSeq = New eClaim.Cl_Cover
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        insvalSequence = mobjClaimSeq.insValSI007Upd("SI007", mobjValues.StringToType(.Form("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeReservstat"), mobjValues.StringToType(.Form("tcnDamages"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFra_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), "", "", mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFrandeda"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDamProf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nExchange_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), .Form("tctCaren_type"), mobjValues.StringToType(.Form("tcnCaren_quan"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), .Form("tcdCover"))
                        Session("mblnExcess") = mobjClaimSeq.mblnExcess
                        lintCase_num = .Form("tcnCase_num")
                        lintDeman_type = .Form("tcnDeman_type")
                        lstrClient = .Form("tctClient")
                    Else
                        lintCase_num = Mid(.Form("cbeCase"), 1, InStr(1, .Form("cbeCase"), "/", 1) - 1)
                        lintDeman_type = Mid(.Form("cbeCase"), InStr(1, .Form("cbeCase"), "/", 1) + 1, (InStr(InStr(1, .Form("cbeCase"), "/", 1) + 1, .Form("cbeCase"), "/", 1)) - (InStr(1, .Form("cbeCase"), "/", 1) + 1))
                        lstrClient = Mid(.Form("cbeCase"), InStr(1, .Form("cbeCase"), "/", 1) + 1 + ((InStr(InStr(1, .Form("cbeCase"), "/", 1) + 1, .Form("cbeCase"), "/", 1))))
                        insvalSequence = mobjClaimSeq.insValSI007(Session("nClaim"), lintCase_num, lintDeman_type, Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
                    End If
                End With
                mstrQueryString = "&nCase_num=" & lintCase_num & "&nDeman_type=" & lintDeman_type & "&sClient=" & lstrClient
                Session("InsvalSI007") = insvalSequence
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing

                '+ Rechazo/Anulacion/Desistimiento de un siniestro
            Case "SI006"
                mobjClaimSeq = New eClaim.Claim
                With Request
                    insvalSequence = mobjClaimSeq.insValSI006(mobjValues.StringToType(.Form("cboNullClaim"), eFunctions.Values.eTypeData.etdDouble, True), .Form("lblClaimType"), mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCase"), eFunctions.Values.eTypeData.etdDouble))
                End With
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing

                '+ Solicitud de orden profesional.        
            Case "SI011"
                mobjClaimSeq = New eClaim.Prof_ord
                If Request.QueryString("WindowType") = "PopUp" Then
                    With Request
                        insvalSequence = mobjClaimSeq.insValSI011(Request.QueryString("sCodispl"),
                                                                  .Form("cbeCase"),
                                                                  mobjValues.StringToType(.Form("tcnOrder"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form("cbeType"), eFunctions.Values.eTypeData.etdInteger),
                                                                  Session("nClaim"),
                                                                  mobjValues.StringToType(.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble),
                                                                  Session("sBrancht"),
                                                                  Session("nBranch"),
                                                                  Session("dEffecdate"),
                                                                  mobjValues.StringToType(.Form("dDateDesing"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(.Form("dOldDateDesing"), eFunctions.Values.eTypeData.etdDate),
                                                                  .Form("dOldHourDesing"),
                                                                  .Form("dHourDesing"),
                                                                  mobjValues.StringToType(.Form("cbeState"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  mobjValues.StringToType(.Form("valWorksh"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  LCase(Request.QueryString("Action")),
                                                                  mobjValues.StringToType(.Form("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(.Form("valNumber"), eFunctions.Values.eTypeData.etdInteger),
                                                                  mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                Else
                    insvalSequence = vbNullString
                End If

                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing

                '+ Trámite de recobro
            Case "SI012"
                mobjClaimSeq = New eClaim.Recover
                With Request
                    insvalSequence = mobjClaimSeq.insValSI012(Request.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeRecoverCase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("dPresDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("dEstDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnIncome"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnExpense"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form("tctCourtCase"), .Form("tctThird"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(.Form("cbeStatus"), eFunctions.Values.eTypeData.etdDouble))
                End With
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing

                '+ SI013: Ingresos por recobro
            Case "SI013"
                mobjClaimSeq = New eClaim.Recover
                With Request
                    If Request.QueryString("WindowType") = "PopUp" Then
                        insvalSequence = mobjClaimSeq.insValSI013Upd(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnRecamount"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = mobjClaimSeq.insValSI013(.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

                '+ Solicitud de recaudos
            Case "SI015"
                mobjClaimSeq = New eClaim.Documents
                If Request.QueryString("WindowType") = "PopUp" Then
                    insvalSequence = mobjClaimSeq.insValSI015Upd("SI015", mobjValues.StringToType(Request.Form("tcdRecepdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcdPropo_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcdPrescdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If String.IsNullOrEmpty(Request.Form("Sel")) Then
                        nCountAux = 0
                    Else
                        nCountAux = Request.Form("Sel").Count
                    End If

                    If nCountAux = 0 Then
                        insvalSequence = mobjClaimSeq.insValSI015(True)
                    End If
                End If

                '+ Información de casos
            Case "SI016"
                insvalSequence = vbNullString

                '+ Solicitud de Finiquitos
            Case "SI017"
                If Request.QueryString("WindowType") = "PopUp" Then
                    mobjClaimSeq = New eClaim.Settlement
                    insvalSequence = mobjClaimSeq.insValSI017("SI017", mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate))
                End If
            Case "SI764"
                mobjClaimSeq = New eClaim.Settlement
                If Request.QueryString("WindowType") = "PopUp" Then

                    insvalSequence = ""
                Else
                    insvalSequence = ""
                End If
                '+ Ventana de fin de proceso        
            Case "GE101"
                insvalSequence = vbNullString

                '+ Coberturas afectadas por el siniestro
            Case "SI813"
                mobjClaimSeq = New ePolicy.Cover
                With Request
                    If Request.QueryString("WindowType") = "PopUp" Then
                        insvalSequence = mobjClaimSeq.InsValSI813("SI813", .QueryString("WindowType") <> "PopUp", 1, mobjValues.StringToType(.Form("cbeActioncov"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        If String.IsNullOrEmpty(Request.Form("hddSel")) Then
                            nCountAux = 0
                        Else
                            nCountAux = Request.Form("hddSel").Count
                        End If

                        insvalSequence = mobjClaimSeq.InsValSI813("SI813", .QueryString("WindowType") <> "PopUp", nCountAux, mobjValues.StringToType(.Form("cbeActioncov"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

                '+ Beneficiarios de un siniestro (Vida)
            Case "SI629"
                mobjClaimSeq = New eClaim.ClaimBenef

                With Request
                    If .QueryString("WindowType") <> "PopUp" Then
                        If .Form("cbeCase") <> vbNullString Then
                            If Not String.IsNullOrEmpty(.Form("hddClientCode")) Then
                                If .Form("hddClientCode").Count > 0 Then
                                    lintCase_num = Mid(.Form("cbeCase"), 1, InStr(1, .Form("cbeCase"), "/", 1) - 1)
                                    lintDeman_type = Mid(.Form("cbeCase"), InStr(1, .Form("cbeCase"), "/", 1) + 1, (InStr(InStr(1, .Form("cbeCase"), "/", 1) + 1, .Form("cbeCase"), "/", 1)) - (InStr(1, .Form("cbeCase"), "/", 1) + 1))
                                    For lintCounter = 0 To Request.Form.GetValues("hddClientCode").Count - 1
                                        insvalSequence = mobjClaimSeq.insValSI629(.QueryString("sCodispl"), Request.QueryString("Action"), .Form.GetValues("hddSel").GetValue(lintCounter), .Form.GetValues("hddCover").GetValue(lintCounter), .Form.GetValues("hddClientCode").GetValue(lintCounter), .Form.GetValues("hddLastName").GetValue(lintCounter), .Form.GetValues("hddLastName2").GetValue(lintCounter), .Form.GetValues("hddFirstName").GetValue(lintCounter), mobjValues.StringToType(.Form.GetValues("hddBirthdat").GetValue(lintCounter), Values.eTypeData.etdDate), .Form.GetValues("hddRelaship").GetValue(lintCounter), .Form.GetValues("hddParticip").GetValue(lintCounter), .Form.GetValues("hddRepresentCode").GetValue(lintCounter), .Form.GetValues("hddRLastName").GetValue(lintCounter), .Form.GetValues("hddRLastName2").GetValue(lintCounter), .Form.GetValues("hddRFirstName").GetValue(lintCounter), .Form.GetValues("hddOffice_pay").GetValue(lintCounter), .Form.GetValues("hddOfficeAgen_pay").GetValue(lintCounter), .Form.GetValues("hddAgency_pay").GetValue(lintCounter), .Form.GetValues("hddRent").GetValue(lintCounter), mobjValues.StringToType(.Form.GetValues("hddInitDate").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hddEndDate").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDate), lintCase_num, lintDeman_type, .Form("optTypeBenef"), "2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nClaim"), Session("dEffecdate"))
                                    Next
                                Else
                                    insvalSequence = ""
                                End If
                            Else
                                mobjClaimSeq.nPerson_typ = mobjValues.StringToType(.Form("cbePersonTyp"), eFunctions.Values.eTypeData.etdInteger)
                                insvalSequence = mobjClaimSeq.insValSI629(.QueryString("sCodispl"),
                                                                                                  Request.QueryString("Action"),
                                                                                                  string.Empty,
                                                                                                  0,
                                                                                                  string.Empty,
                                                                                                  string.Empty,
                                                                                                  string.Empty,
                                                                                                  string.Empty,
                                                                                                  eRemoteDB.dtmNull ,
                                                                                                  0,
                                                                                                  0,
                                                                                                  string.Empty,
                                                                                                  string.Empty,
                                                                                                  string.Empty ,
                                                                                                  string.Empty ,
                                                                                                  0,
                                                                                                  0,
                                                                                                  0,
                                                                                                  0,
                                                                                                  eRemoteDB.dtmNull,
                                                                                                  eRemoteDB.dtmNull,
                                                                                                  mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble),
                                                                                                  mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble),
                                                                                                  "",
                                                                                                  "2",
                                                                                                  Session("nBranch"),
                                                                                                  Session("nProduct"),
                                                                                                  Session("nPolicy"),
                                                                                                  Session("nClaim"),
                                                                                                  Session("dEffecdate"))
                            End If
                        Else
                            insvalSequence = ""
                        End If
                    Else
                        insvalSequence = mobjClaimSeq.insValSI629(.QueryString("sCodispl"),
                                                                                           Request.QueryString("Action"),
                                                                                           .Form("hddSel"),
                                                                                           mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           .Form("tctClientCode"),
                                                                                           .Form("tctLastName"),
                                                                                           .Form("tctLastName2"),
                                                                                           .Form("tctFirstName"),
                                                                                           mobjValues.StringToType(.Form("tcdBirthdat"), eFunctions.Values.eTypeData.etdDate),
                                                                                           mobjValues.StringToType(.Form("cbeRelaship"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("tcnParticip"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           .Form("tctRepresentCode"),
                                                                                           .Form("tctRLastName"),
                                                                                           .Form("tctRLastName2"),
                                                                                           .Form("tctRFirstName"),
                                                                                           mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("tcnRent"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("tcdInitDate"), eFunctions.Values.eTypeData.etdDate),
                                                                                           mobjValues.StringToType(.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate),
                                                                                           mobjValues.StringToType(.Form("hddCasenum"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           mobjValues.StringToType(.Form("hddDeman_type"), eFunctions.Values.eTypeData.etdDouble),
                                                                                           "",
                                                                                           "1",
                                                                                           Session("nBranch"),
                                                                                           Session("nProduct"),
                                                                                           Session("nPolicy"),
                                                                                           Session("nClaim"),
                                                                                           Session("dEffecdate"))

                    End If
                End With

                '+ Datos del negocio aceptado
            Case "SI003"
                mobjClaimSeq = New eClaim.Claim
                With Request
                    insvalSequence = mobjClaimSeq.insValSI003(.QueryString("sCodispl"), .Form("tctClaim"))
                End With
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing
            Case "SI025"
                insvalSequence = ""
            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString("sCodispl") & ")"
        End Select
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        Dim C_MESSAGE_98033 As Object
        Dim C_MESSAGE_99041 As Object
        Dim sTotalLoss As String
        Dim S As Object
        Dim lintCounter As Integer
        Dim lstrMessage As String
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lstrClaim As String
        Dim lclsProduct As eProduct.Product

        '- Variables auxiliares usadas en la SI007    
        Dim lintCase_num As String
        Dim lintDeman_type As String
        Dim lstrClient As String
        Dim ldbnAmountDed As String
        Dim lintCover As String


        lblnPost = False

        Dim lclsProf_ord As eClaim.Prof_ord
        Dim lclsRecover As eClaim.Recover
        Dim lclsClaim_Win As eClaim.Claim_win
        Select Case Request.QueryString("sCodispl")

            '+ Solicitud de clave para tratamiento de siniestros
            Case "SI001"
                lblnPost = True

                mobjClaimSeq = New eClaim.Claim
                With Request
                    If .Form("cbeTransactio") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
                        Session("bQuery") = True
                        lstrClaim = .Form("tcnClaim")
                    Else
                        Session("bQuery") = False
                        lblnPost = mobjClaimSeq.insPostSI001(mobjValues.StringToType(.Form("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), .Form("tctRequest_nu"), mobjValues.StringToType(.Form("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnReference"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdOccurrdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("valIdCatas"), eFunctions.Values.eTypeData.etdDouble))
                        lstrClaim = mobjClaimSeq.nClaim
                    End If

                    If lblnPost Then
                        Session("sCertype") = .Form("sCertype")
                        Session("nClaim") = lstrClaim
                        Session("nBranch") = .Form("cbeBranch")
                        Session("nProduct") = .Form("valProduct")
                        Session("nTransaction") = .Form("cbeTransactio")
                        Session("nPolicy") = .Form("tcnPolicy")
                        Session("nCertif") = .Form("tcnCertificat")
                        Session("dEffecdate") = .Form("tcdOccurrdat")
                        Session("nReference") = .Form("tcnReference")
                        Session("dLedgerDate") = .Form("tcdLedgerDate")
                        Session("sKey") = vbNullString
                        lclsProduct = New eProduct.Product
                        Call lclsProduct.Find(CInt(Session("nBranch")), CInt(Session("nProduct")), CDate(Session("dEffecdate")), True)
                        If CStr(lclsProduct.sBrancht) = "1" Then
                            Call lclsProduct.FindProduct_li(CInt(Session("nBranch")), CInt(Session("nProduct")), CDate(Session("dEffecdate")), True)
                            Session("nProdClas") = lclsProduct.nProdClas
                        Else
                            Session("nProdClas") = ""
                        End If
                        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                        lclsProduct = Nothing

                        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimAmendment or  Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimRecovery  Then

                            lclsClaim_Win = New eClaim.Claim_win
                            Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI007", "3", CInt(Session("nUsercode")))
                            lclsClaim_Win = Nothing
                        End If

                    Else
                        If mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble) < 0 Then
                            '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla
                            Response.Write("<SCRIPT>alert('" & "Err. - " & C_MESSAGE_99041 & "');</" & "Script>")
                            lblnPost = False
                        Else
                            '+ Si la longitud de la variable es mayor a 10, se asume que existe un error, y se muestra al usuario
                            If Len(Trim(lstrClaim)) > 10 Then
                                Response.Write("<SCRIPT>alert('" & "Err. - " & C_MESSAGE_98033 & "');</" & "Script>")
                                lblnPost = False
                            End If
                        End If
                    End If
                End With
                mstrLocationSI001 = "/VTimeNet/Common/SecWHeader.aspx?sCodispl=SI001&sModule=Claim&sProject=ClaimSeq&sConfig=InSequence"

                '+ SI004: Datos Generales del Siniestro
            Case "SI004"
                mobjClaimSeq = New eClaim.Claim

                With Request
                    Session("nCase_num") = .Form("tcnCaseNum")
                    Session("nDeman_type") = .Form("cbeReclaimVal")
                    If .QueryString("WindowType") = "PopUp" Then
                        If Request.Form("nTotalLoss") <> 2 Then
                            sTotalLoss = "1"
                        Else
                            sTotalLoss = "2"
                        End If
                        S = mobjValues.StringToType(.QueryString("cbeClaimCaus"), eFunctions.Values.eTypeData.etdDouble)
                        lblnPost = mobjClaimSeq.insPostSI004(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboRType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hdnId"), eFunctions.Values.eTypeData.etdDouble), .Form("hddStatReserv"), mobjValues.StringToType(.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeInsured"), .Form("tctClientCode"), .Form("tctClientCode_Digit"), .Form("cbeReclaim"), mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble), .Form("dOccurDat") & " " & .Form("nHour"), mobjValues.StringToType(.Form("dPrescdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("dLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("nOffice_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nOfficeAgen_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nAgency_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeRelaship"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nClaimCause"), eFunctions.Values.eTypeData.etdDouble), .Form("tctLastName"), .Form("tctLastName2"), Mid(.Form("tctFirstName"), 1, 19), .QueryString("Action"), .QueryString("WindowType"), sTotalLoss, False, mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nProdClas")), eFunctions.Values.eTypeData.etdLong, True))

                        mstrQueryString = "&nClaim_Caus=" & .Form("nClaimCause") & "&nHour=" & .Form("nHour") & "&nTotalLoss=" & .Form("nTotalLoss") & "&dPrescdat=" & .Form("dPrescdat") & "&dLimit_pay=" & .Form("dLimit_pay") & "&nClaimCause=" & .Form("nClaimCause") & "&nOffice_pay=" & .QueryString("nOffice_pay") & "&nOfficeAgen_pay=" & .QueryString("nOfficeAgen_pay") & "&nAgency_pay=" & .QueryString("nAgency_pay") & "&sReload=1" & "&dDecladat=" & .Form("dDecladat") & "&dOccurdat=" & .Form("dOccurdat") & "&nClaimParent=" & .Form("nClaimParent")
                    Else
                        If Request.Form("hddTotalLoss") <> 2 Then
                            sTotalLoss = "1"
                        Else
                            sTotalLoss = "2"
                        End If

                        mobjClaimSeq.nClaimParent = mobjValues.StringToType(Request.Form("cbeClaimParent"), eFunctions.Values.eTypeData.etdLong)
                        lblnPost = mobjClaimSeq.insPostSI004(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, vbNullString, vbNullString, vbNullString, vbNullString, eRemoteDB.Constants.intNull, .Form("gmdOccurDat") & " " & .Form("gmnHour"), mobjValues.StringToType(.Form("gmdPrescDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("gmdLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeClaimCaus"), eFunctions.Values.eTypeData.etdDouble), vbNullString, vbNullString, vbNullString, vbNullString, .QueryString("WindowType"), sTotalLoss, False, mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nProdClas")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("cbeClaimParent"), eFunctions.Values.eTypeData.etdDouble))


                        Session("sTotalLoss") = sTotalLoss
                        Session("sCause") = .Form("cbeClaimCaus")
                    End If
                End With

                '+ Reservas de Sinietros            
            Case "SI007"
                With Request
                    mobjClaimSeq = New eClaim.Cl_Cover
                    If .QueryString("WindowType") = "PopUp" Then


                        If .Form("cbeFrantype_aux") = "1" Then
                            ldbnAmountDed = FormatNumber(mobjValues.StringToType(.Form("hddnFranAmount"), eFunctions.Values.eTypeData.etdDouble), 6)
                        Else
                            ldbnAmountDed = .Form("tcnFra_amount")
                        End If


                        'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                        lblnPost = mobjClaimSeq.insPostSI007(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), .Form("tctClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), vbNullString, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), .Form("cbeCurrency"), .Form("nCurrency_o"), mobjValues.StringToType(.Form("tcnCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dLedgerDate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDamages"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldbnAmountDed, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFrandeda"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDamProf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeReservstat"), .Form("cbeFrantype"), .Form("sAutomRep"), "1", mobjValues.StringToType(.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnReserveAnt"), eFunctions.Values.eTypeData.etdDouble), Today, .Form("hddBill_ind"), Session("SI007_Codispl"), Session("sProcess_SI021"))

                        Session("nTransac") = mobjClaimSeq.nTransact

                        If CStr(Session("SI007_Codispl")) <> vbNullString Then
                            Session("sProcess_SI021") = 1
                        End If

                        If mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                            mdblTotal = mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble) + (mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnOldReserve"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            mdblTotal = (mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnOldReserve"), eFunctions.Values.eTypeData.etdDouble))
                            mdblTotal2 = mdblTotal2 + mdblTotal

                        End If
                        lintCase_num = .Form("tcnCase_num")
                        lintDeman_type = .Form("tcnDeman_type")
                        lstrClient = .Form("hddClient")

                        mstrQueryString = "&nCase_num=" & lintCase_num & "&nDeman_type=" & lintDeman_type & "&sClient=" & lstrClient



                    Else
                        lintCase_num = Mid(.Form("cbeCase"), 1, InStr(1, .Form("cbeCase"), "/", 1) - 1)
                        lintDeman_type = Mid(.Form("cbeCase"), InStr(1, .Form("cbeCase"), "/", 1) + 1, (InStr(InStr(1, .Form("cbeCase"), "/", 1) + 1, .Form("cbeCase"), "/", 1)) - (InStr(1, .Form("cbeCase"), "/", 1) + 1))

                        lblnPost = mobjClaimSeq.insPostSI007_total(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), lintCase_num, lintDeman_type, mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnOldCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))

                        Session("nTotal") = 0
                    End If
                End With

                '+ Rechazo/Anulacion de un siniestro
            Case "SI006"
                Session("LetterTypeId") = Request.Form("cboLetter")
                Session("NullClaimId") = Request.Form("cboNullClaim")

                mobjClaimSeq = New eClaim.Claim
                With Request
                    lblnPost = mobjClaimSeq.insPostSI006(mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboNullClaim"), eFunctions.Values.eTypeData.etdDouble, True),"" , mobjValues.StringToType(.Form("tcnCase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble))
                    Session("nTransac") = mobjClaimSeq.nMovement

                End With

                '+ Solicitud de orden profesional
            Case "SI011"
                lclsProf_ord = New eClaim.Prof_ord

                With Request
                    If .QueryString("WindowType") = "PopUp" Then

                        lblnPost = lclsProf_ord.insPostSI011(mobjValues.StringToType(.Form("nTransac"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDemanType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("dDateDesing"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeState"), eFunctions.Values.eTypeData.etdDouble, True), .Form("dHourDesing"), mobjValues.StringToType(.Form("valWorksh"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), .Form("chkWsDeduc"), mobjValues.StringToType(.Form("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CInt(Session("nCertif")), mobjValues.StringToType(.Form("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate) , mobjValues.StringToType(.Form("valInspector"), eFunctions.Values.eTypeData.etdDouble, True)  , mobjValues.StringToType(.Form("hddModulec"), eFunctions.Values.eTypeData.etdDouble, True) , mobjValues.StringToType(.Form("hddCover"), eFunctions.Values.eTypeData.etdDouble, True) )
                    Else
                        lblnPost = True
                    End If
                End With
                'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsProf_ord = Nothing

                '+ Trámite de recobro
            Case "SI012"
                lclsRecover = New eClaim.Recover

                With Request
                    lblnPost = lclsRecover.insPostSI012(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeRecoverCase"), eFunctions.Values.eTypeData.etdDouble), .Form("tctCourtCase"), .Form("tctThird"), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnExpense"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnIncome"), eFunctions.Values.eTypeData.etdDouble), .Form("dEstDate"), .Form("dPresDate"), .Form("tctClient"), mobjValues.StringToType(.Form("cbeRecoverTy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeProvider"), eFunctions.Values.eTypeData.etdDouble) ,mobjValues.StringToType(.Form("cbeStatus"), eFunctions.Values.eTypeData.etdDouble) )
                    If lblnPost Then
                        '+ Se indica el nro. de recobro generado
                        If mobjValues.StringToType(.Form("cbeTransac"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            lstrMessage = "  Tome nota del número de recobro generado: " & lclsRecover.nRecoverNumber
                            Response.Write("<SCRIPT>alert(""" & lstrMessage & """);</" & "Script>")
                        End If
                        Session("RecoveryTransac") = lclsRecover.nRecoverNumber
                    End If
                    If lblnPost Then
                        lclsClaim_Win = New eClaim.Claim_win
                        Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI012", "2", CInt(Session("nUsercode")))
                        lclsClaim_Win = Nothing
                    End If
                End With
                lclsRecover = Nothing

                '+ SI013: Ingresos por recobro
            Case "SI013"
                mobjClaimSeq = New eClaim.Recover
                With Request
                    If Request.QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjClaimSeq.insPostSI013Upd(Session("sKey"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddnTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnRecAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnCostRecu"), eFunctions.Values.eTypeData.etdDouble), Request.Form("sClient"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = mobjClaimSeq.insPostSI013(Session("sKey"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("cbeTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nReference")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnBordereaux"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost Then
                            lclsClaim_Win = New eClaim.Claim_win
                            Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI013", "2", CInt(Session("nUsercode")))
                            lclsClaim_Win = Nothing
                        End If
                    End If
                End With

                '+ Solicitud de recaudos
            Case "SI015"
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjClaimSeq.insPostSI015(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.TypeToString(Request.QueryString("nCasenum"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.TypeToString(Request.QueryString("nDemantype"), eFunctions.Values.eTypeData.etdDouble),
                                                             .QueryString("sClient"),
                                                             .QueryString("Action"),
                                                             mobjValues.StringToType(.Form("tcnCode"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcnDoc_code"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcnDocnumbe"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcdPropo_date"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("tcdPrescdate"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("tcdRecepdate"), eFunctions.Values.eTypeData.etdDate),
                                                             ,
                                                             Request.Form("tctDescdocu"),
                                                             mobjValues.StringToType(Request.Form("tcnConsec"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(Request.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With

                '+ Información de casos
            Case "SI016"
                mobjClaimSeq = New eClaim.Claim_cases
                lblnPost = mobjClaimSeq.insPostSI016(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), Request.Form("nCase_num"), Request.Form("nDeman_type"), Request.Form("nAuxDcto"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.Form("sStatusCod"))
                If lblnPost Then
                    lclsClaim_Win = New eClaim.Claim_win
                    Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI007", "3", CInt(Session("nUsercode")))
                    lclsClaim_Win = Nothing
                End If
                '+ Solicitud de Finiquitos
            Case "SI017"
                lblnPost = True
                If Request.QueryString("WindowType") = "PopUp" Then
                    mobjClaimSeq = New eClaim.Settlement
                    lblnPost = mobjClaimSeq.InsPostSI017(Request.QueryString("Action"), Session("nClaim"), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnBeforeAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form("chkPrinted"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("hddId"), eFunctions.Values.eTypeData.etdDouble))
                    lclsClaim_Win = New eClaim.Claim_win
                    Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI017", "2", CInt(Session("nUsercode")))

                End If
                '+ Solicitud de Finiquitos
            Case "SI764"
                lblnPost = True
                With Request
                    lclsClaim_Win = New eClaim.Claim_win
                    Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI764", "2", CInt(Session("nUsercode")))
                End With
                '+ Coberturas afectadas por el siniestro
            Case "SI813"
                mobjClaimSeq = New ePolicy.Cover
                With Request
                    If .QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjClaimSeq.InsPostSI813Upd("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddnCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), .Form("valClient"), mobjValues.StringToType(.Form("hddnRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeActioncov"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), Session("SessionId").GetHashCode(), .Form("hddsDepend"), "1")
                        mstrQueryString = "&sInd=2"
                    Else
                        lblnPost = mobjClaimSeq.InsPostSI813("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nClaim"), mobjValues.StringToType(.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("SessionId").GetHashCode())

                        If lblnPost Then
                            lclsClaim_Win = New eClaim.Claim_win
                            Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI813", "2", CInt(Session("nUsercode")))
                            'UPGRADE_NOTE: Object lclsClaim_Win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                            lclsClaim_Win = Nothing
                        End If
                    End If
                End With

                '+ Beneficiarios de un siniestro (Vida)
            Case "SI629"
                mobjClaimSeq = New eClaim.ClaimBenef

                With Request
                    If .QueryString("WindowType") <> "PopUp" Then
                        If .Form("cbeCase") <> vbNullString Then
                            If Not String.IsNullOrEmpty(.Form("hddClientCode")) Then
                                If .Form("hddClientCode").Count > 0 Then
                                    lintCase_num = Mid(.Form("cbeCase"), 1, InStr(1, .Form("cbeCase"), "/", 1) - 1)
                                    lintDeman_type = Mid(.Form("cbeCase"), InStr(1, .Form("cbeCase"), "/", 1) + 1, (InStr(InStr(1, .Form("cbeCase"), "/", 1) + 1, .Form("cbeCase"), "/", 1)) - (InStr(1, .Form("cbeCase"), "/", 1) + 1))
                                    'For lintCounter = 1 To .Form("hddClientCode").Count
                                    For lintCounter = 0 To Request.Form.GetValues("hddClientCode").Count - 1

                                        lblnPost = mobjClaimSeq.insPostSI629(.QueryString("sCodispl"),
                                                                             Request.QueryString("Action"),
                                                                             Session("nClaim"),
                                                                             lintCase_num,
                                                                             lintDeman_type,
                                                                             .Form.GetValues("hddSel").GetValue(lintCounter),
                                                                             .Form.GetValues("hddCover").GetValue(lintCounter),
                                                                             .Form.GetValues("hddModulec").GetValue(lintCounter),
                                                                             .Form.GetValues("hddCurrency").GetValue(lintCounter),
                                                                             .Form.GetValues("hddClientCode").GetValue(lintCounter),
                                                                             1,
                                                                             .Form.GetValues("hddLastName").GetValue(lintCounter),
                                                                             .Form.GetValues("hddLastName2").GetValue(lintCounter),
                                                                             .Form.GetValues("hddFirstName").GetValue(lintCounter),
                                                                             mobjvalues.StringToType(.Form.GetValues("hddBirthdat").GetValue(lintCounter), Values.eTypeData.etdDate),
                                                                             mobjValues.StringToType(.Form.GetValues("hddRelaship").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDouble, True),
                                                                             .Form.GetValues("hddParticip").GetValue(lintCounter),
                                                                             .Form.GetValues("hddRepresentCode").GetValue(lintCounter),
                                                                             1,
                                                                             .Form.GetValues("hddRLastName").GetValue(lintCounter),
                                                                             .Form.GetValues("hddRLastName2").GetValue(lintCounter),
                                                                             .Form.GetValues("hddRFirstName").GetValue(lintCounter),
                                                                             .Form.GetValues("hddOffice_pay").GetValue(lintCounter),
                                                                             .Form.GetValues("hddOfficeAgen_pay").GetValue(lintCounter),
                                                                             .Form.GetValues("hddAgency_pay").GetValue(lintCounter),
                                                                             .Form.GetValues("hddRent").GetValue(lintCounter),
                                                                             mobjValues.StringToType(.Form.GetValues("hddInitDate").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDate),
                                                                             mobjValues.StringToType(.Form.GetValues("hddEndDate").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDate),
                                                                             Session("nUsercode"),
                                                                             .Form.GetValues("hddId").GetValue(lintCounter),
                                                                             .Form("chkHas_Surv_Pension_Benefs"),
                                                                             mobjValues.StringToType(.Form("gmdSummon"), eFunctions.Values.eTypeData.etdDate),
                                                                             mobjValues.StringToType(.Form("gmdSummon_Limit"), eFunctions.Values.eTypeData.etdDate),
                                                                             mobjValues.StringToType(.Form.GetValues("hddShowDate").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDate),
                                                                             mobjValues.StringToType(.Form.GetValues("hddNoteNum").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDouble),
                                                                             "2",
                                                                             mobjValues.StringToType(.Form.GetValues("hddPaymentAddress").GetValue(lintCounter), eFunctions.Values.eTypeData.etdLong))
                                        If Not lblnPost Then
                                            Exit For
                                        End If
                                    Next
                                Else
                                    lblnPost = True
                                End If
                            Else
                                lblnPost = True
                            End If
                        Else
                            lblnPost = True
                        End If
                    Else
                        mobjClaimSeq.nPerson_typ = mobjValues.StringToType(.Form("cbePersonTyp"), eFunctions.Values.eTypeData.etdInteger)
                        lblnPost = mobjClaimSeq.insPostSI629(.QueryString("sCodispl"),
                                                             Request.QueryString("Action"),
                                                             mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("hddCasenum"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("hddDeman_type"), eFunctions.Values.eTypeData.etdDouble), .Form("hddSel"),
                                                             mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("valCover_nModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("hddCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                             .Form("tctClientCode"),
                                                             .Form("tctClientCode_Digit"),
                                                             .Form("tctLastName"),
                                                             .Form("tctLastName2"),
                                                             .Form("tctFirstName"),
                                                             mobjValues.StringToType(.Form("tcdBirthdat"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("cbeRelaship"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcnParticip"), eFunctions.Values.eTypeData.etdDouble),
                                                             .Form("tctRepresentCode"),
                                                             .Form("tctRepresentCode_Digit"),
                                                             .Form("tctRLastName"),
                                                             .Form("tctRLastName2"),
                                                             .Form("tctRFirstName"),
                                                             mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcnRent"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(.Form("tcdInitDate"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate),
                                                             Session("nUsercode"), mobjValues.StringToType(.Form("hddId"), eFunctions.Values.eTypeData.etdDouble),
                                                             .Form("hddHas_Surv_Pension_Benefs"),
                                                             mobjValues.StringToType(.Form("hddSummon"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("hddSummon_Limit"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("tcdShowDate"), eFunctions.Values.eTypeData.etdDate),
                                                             mobjValues.StringToType(.Form("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble),
                                                             "1", mobjValues.StringToType(.Form("cbePaymentAddress"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

                '+ Datos del negocio aceptado
            Case "SI003"
                mobjClaimSeq = New eClaim.Claim
                lblnPost = mobjClaimSeq.insPostSI003(Session("nClaim"), Request.Form("tctClaim"), Session("nUserCode"))
                'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimSeq = Nothing
            Case "SI025"
                If Request.QueryString("WindowType") = "PopUp" Then
                    With Request
                        mobjClaimSeq = New eClaim.cl_coverma
                        If .QueryString("WindowType") = "PopUp" Then


                            If .Form("cbeFrantype_aux") = "1" Then
                                ldbnAmountDed = FormatNumber(mobjValues.StringToType(.Form("hddnFranAmount"), eFunctions.Values.eTypeData.etdDouble), 6)
                            Else
                                ldbnAmountDed = .Form("tcnFra_amount")
                            End If
                            'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                            lblnPost = mobjClaimSeq.insPostSI025(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), .Form("txtClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), vbNullString, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), .Form("cbeCurrency"), .Form("nCurrency_o"), mobjValues.StringToType(.Form("tcnCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dLedgerDate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldbnAmountDed, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFrandeda"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDamProf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeReservstat"), .Form("cbeFrantype"), .Form("sAutomRep"), "1", mobjValues.StringToType(.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnReserveAnt"), eFunctions.Values.eTypeData.etdDouble), Today, .Form("hddBill_ind"), mobjValues.StringToType(CStr(Session("SI007_Codispl")), eFunctions.Values.eTypeData.etdDouble, True), Session("sProcess_SI021"), mobjValues.StringToType(.Form("tcnPrestac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDed_Percen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnImport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble))

                            If CStr(Session("SI007_Codispl")) <> vbNullString Then
                                Session("sProcess_SI021") = 1
                            End If

                            If mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                                mdblTotal = mobjValues.StringToType(CStr(Session("nTotal")), eFunctions.Values.eTypeData.etdDouble) + (mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnOldReserve"), eFunctions.Values.eTypeData.etdDouble))
                            Else
                                mdblTotal = (mobjValues.StringToType(.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(.Form("tcnOldReserve"), eFunctions.Values.eTypeData.etdDouble))
                                mdblTotal2 = mdblTotal2 + mdblTotal

                            End If

                            lintCase_num = .Form("tcnCase_num")
                            lintDeman_type = .Form("tcnDeman_type")
                            lstrClient = .Form("hddClient")
                            lintCover = .Form("nCover")

                            mstrQueryString = "&nCase_num=" & lintCase_num & "&nDeman_type=" & lintDeman_type & "&sClient=" & lstrClient & "&nCover=" & lintCover
                        End If
                    End With
                Else
                    'Dim lclsClaim_Win
                    lclsClaim_Win = New eClaim.Claim_win
                    Call lclsClaim_Win.Add_Claim_win(CDbl(Session("nClaim")), "SI025", "2", CInt(Session("nUsercode")))
                    'UPGRADE_NOTE: Object lclsClaim_Win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsClaim_Win = Nothing
                    lblnPost = True
                End If
        End Select
        insPostSequence = lblnPost
    End Function

    '% FindOTNumber: Proceso de ejecución de WS de generación y actualización de Ordenes de trabajo
    Function FindOTNumber()

        'Definición de clase para obtener información para WS
        Dim mobjProf_ord As New eClaim.Prof_ord
        Dim mobjClaimBenef As New eClaim.ClaimBenefs

        'Variable de resultado WS
        Dim ErrorLocal As String = "No se logró realizar la operación, recuerde completar la información de la secuencia para la generación de OT."

        'Proceso de extracción de información de siniestro para WS
        If mobjProf_ord.Find_webServiceInfo(Session("nClaim")) Then

            'Validación para control de servicio.
            'Para evitar errores en el almacenamientos del sistema de OT de Mutual se incorpora el valor " " en las variables vacias evitando la falta de datos
            'de la ejecución del servicio.

            If mobjProf_ord.sLastNameRec = "" Then
                mobjProf_ord.sLastNameRec = " "
            End If

            If mobjProf_ord.sLastName2Rec = "" Then
                mobjProf_ord.sLastName2Rec = " "
            End If

            If mobjProf_ord.sLastNameSin = "" Then
                mobjProf_ord.sLastNameSin = " "
            End If

            If mobjProf_ord.sLastName2Sin = "" Then
                mobjProf_ord.sLastName2Sin = " "
            End If

            If mobjProf_ord.sLastNameTit = "" Then
                mobjProf_ord.sLastNameTit = " "
            End If

            If mobjProf_ord.sLastName2Tit = "" Then
                mobjProf_ord.sLastName2Tit = " "
            End If

            'Si el usuario no ingresa valor se ejecuta la Generación de Orden de trabajo
            'If mobjValues.StringToType(Request.QueryString("tcnOrder"), eFunctions.Values.eTypeData.etdInteger) <= 0 Then


            'End If
        End If

    End Function

    '% insCancel: Esta rutina es activada cuando el usuario cancela la transacción que este
    '% ejecutando.
    '--------------------------------------------------------------------------------------------
    Function insCancel() As Object
        '--------------------------------------------------------------------------------------------
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        '- Objeto para el manejo de siniestro
        Dim lobjClaim As eClaim.Claim
        Dim lintPrint As New Integer
        Dim lrecProf_ord As eClaim.Prof_ord
        Dim lrecProf_ords As eClaim.Prof_ords
        Dim lintCount As New Integer

        lrecProf_ord = New eClaim.Prof_ord
        lrecProf_ords = New eClaim.Prof_ords

        lobjClaim = New eClaim.Claim

        insFinish = True

        mstrLocationSI001 = vbNullString

        Session("lintPrint")  = 0

        Select Case Session("nTransaction")

            '+Si se trata de Fin de Emisión (SI050)


            Case eClaim.Claim_win.eClaimTransac.clngClaimIssue,
                eClaim.Claim_win.eClaimTransac.clngClaimRecovery,
                eClaim.Claim_win.eClaimTransac.clngClaimAmendment,
                eClaim.Claim_win.eClaimTransac.clngApproval,
                eClaim.Claim_win.eClaimTransac.clngClaimReopening,
                eClaim.Claim_win.eClaimTransac.clngClaimRejection,
                eClaim.Claim_win.eClaimTransac.clngCaratula
                If lobjClaim.insExecuteSI050(CInt(Session("nTransaction")), CDbl(Session("nClaim")), Request.Form("cboWaitCode"), Request.Form("lblnEnabledcboWaitCode"), mobjValues.StringToType(CStr(Session("nReference")), eFunctions.Values.eTypeData.etdDouble), CInt(Session("nUsercode")), mobjValues.StringToType(CStr(Session("nProdClas")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form("cboNullClaim"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nNotenum")), eFunctions.Values.eTypeData.etdLong, True)) Then
                    insFinish = True
                End If

                '+ Se muestra la página principal de la secuencia        
                If CStr(Session("SI021_nClaim")) <> vbNullString Then
                    mstrLocationSI001 = "'/VTimeNet/common/GoTo.aspx?sCodispl=SI021'"
                Else
                    mstrLocationSI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim'"
                End If

                If lrecProf_ords.Find(Session("nClaim")) Then
                    For lintCount = 1 To lrecProf_ords.Count
                        lrecProf_ord = lrecProf_ords.Item(lintCount)
                        If (lrecProf_ord.nOrdertype = "17" Or lrecProf_ord.nOrdertype = "18") Then
                            Session("lintPrint") = 1
                        End If
                        If Session("lintPrint") = 1 Then
                            Exit For
                        End If
                    Next
                End If


                '+ Se realiza el llamado para la impresion del documento(Reporte)
                If Request.Form("chkDenPrint") = "1" Or Request.Form("chkPrintNow") = "1" Or Session("lintPrint") = 1 Then
                    insPrintDocuments()
                End If
            Case eClaim.Claim_win.eClaimTransac.clngClaimRejection
                insFinish = True
                insPrintDocuments()
                mstrLocationSI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim'"
            Case eClaim.Claim_win.eClaimTransac.clngCaratula '+Desistimiento
                insFinish = True
                mstrLocationSI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim'"
            Case eClaim.Claim_win.eClaimTransac.clngClaimCancellation
                insFinish = True
                mstrLocationSI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim'"

            Case Else
                '+ Se muestra la página principal de la secuencia
                If CStr(Session("SI021_nClaim")) <> vbNullString Then
                    mstrLocationSI001 = "'/VTimeNet/common/GoTo.aspx?sCodispl=SI021'"
                Else
                    mstrLocationSI001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim'"
                End If
        End Select

        'UPGRADE_NOTE: Object lobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lobjClaim = Nothing
    End Function

    '%insPrintDocuments : Realiza la ejecución del reporte SIL762(Denuncio de siniestro)
    '-------------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-------------------------------------------------------------------------------------------
        Dim nBranch As Integer
        Dim nProduct As Integer
        Dim nClaim As Object
        Dim nPolicy As Object
        Dim nCertif As Object
        Dim nCase As String
        Dim nDeman_type As String
        Dim nPrint As Boolean
        Dim lblnPreviousReport As Boolean
        Dim mobjDocuments As eReports.Report
        Dim lcolReport_prod As eProduct.report_prods
        Dim lclsReport_prod As eProduct.report_prod
        Dim bProcessCase As Boolean
        Dim lrecProf_ord As eClaim.Prof_ord
        Dim lrecProf_ords As eClaim.Prof_ords
        Dim lintCount As New Integer
        Dim lclsProdmaster As eProduct.Product


        lrecProf_ord = New eClaim.Prof_ord
        lrecProf_ords = New eClaim.Prof_ords

        mobjDocuments = New eReports.Report
        lblnPreviousReport = False

        nBranch = Session("nBranch")
        nClaim = Session("nClaim")
        nProduct = Session("nProduct")
        nPolicy = Session("nPolicy")
        nCertif = Session("nCertif")

        If Request.Form("chkDenPrint") = "1" _
        And (Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimRejection Or
             Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngCaratula) Then
            If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimRejection Then

                If Session("nCase_num") Is Nothing Then
                    Session("nCase_num") = Request.Form("tcnCaseNum")
                End If

                If Session("nDeman_Type") Is Nothing Then
                    Session("nDeman_Type") = Request.Form("tcnDeman_Type")
                End If

                With mobjDocuments
                    .sCodispl = "SIL961"
                    .ReportFilename = "SIL961.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(CStr(Session("nNotenum")), eFunctions.Values.eTypeData.etdDouble))
                    .bTimeOut = True
                    Response.Write(.Command)
                End With
            End If

            'Desistimiento de siniestro: Impresión de carta.
            If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngCaratula Then
                'Se busca nombre del reporte a imprimir.
                Dim lintSettlementTypeId_waiver As Integer = 3 'Tipo de finiquito: Desistimiento.
                Dim lclsTab_Settlements As New eClaim.Tab_Settlements
                Dim lstrReportFilename As String = Nothing

                If lclsTab_Settlements.MSI7000_Find(nBranch, 0, "", lintSettlementTypeId_waiver) Then
                    For Each lclsTab_Settlement As eClaim.Tab_Settlement In lclsTab_Settlements
                        lstrReportFilename = String.Format("{0}.rpt", lclsTab_Settlement.sFormatname.Trim())
                    Next lclsTab_Settlement
                End If
                lclsTab_Settlements = Nothing

                If Session("nCase_num") Is Nothing Then
                    Session("nCase_num") = Request.Form("tcnCaseNum")
                End If

                If Session("nDeman_Type") Is Nothing Then
                    Session("nDeman_Type") = Request.Form("tcnDeman_Type")
                End If

                With mobjDocuments
                    .sCodispl = "SIL961"
                    .ReportFilename = lstrReportFilename
                    .setStorProcParam(1, mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble))
                    .bTimeOut = True
                    Response.Write(.Command)
                End With
            End If
        Else
            With mobjDocuments
                '+ Si se requiere la impresion de denuncio.    
                If Request.Form("chkDenPrint") = "1" Then
                    lblnPreviousReport = True

                    lclsProdmaster = New eProduct.Product
                    If lclsProdmaster.FindProdMaster(nBranch, nProduct) Then
                        Dim lobjCases As New eClaim.claim_Cases
                        lobjCases.OnlyDemandant = True
                        If lobjCases.Find(nClaim) Then
                            Dim lobjCase As eClaim.claim_Case
                            For Each lobjCase In lobjCases
                                bProcessCase = False
                                '+ Si el usuario indica que se deben imprimir todos los casos 
                                If Request.Form("cbeCase") = "-74796976" Then
                                    nCase = lobjCase.nCase_Num
                                    nDeman_type = lobjCase.nDeman_Type
                                    bProcessCase = True
                                Else
                                    nCase = Mid(Request.Form("cbeCase"), 1, InStr(1, Request.Form("cbeCase"), "/", 1) - 1)
                                    nDeman_type = Mid(Request.Form("cbeCase"), InStr(1, Request.Form("cbeCase"), "/", 1) + 1, (InStr(InStr(1, Request.Form("cbeCase"), "/", 1) + 1, Request.Form("cbeCase"), "/", 1)) - (InStr(1, Request.Form("cbeCase"), "/", 1) + 1))
                                    If nCase = lobjCase.nCase_Num And nDeman_type = lobjCase.nDeman_Type Then
                                        bProcessCase = True
                                    End If
                                End If
                                If bProcessCase Then
                                    Select Case lclsProdmaster.sBrancht
                                        '+ Denuncio Vida/Oncologico.
                                        Case 1
                                            .sCodispl = "SIL762"
                                            If mobjValues.StringToType(Request.Form("optClaim"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                                                .ReportFilename = "SIL762_O.rpt"
                                            Else
                                                .ReportFilename = "SIL762_V.rpt" 'listo			
                                            End If
                                            nPrint = True
                                            '+ Denuncio FullCar.
                                        Case 3
                                            .sCodispl = "SIL762"
                                            .ReportFilename = "SIL762_FC.rpt"
                                            nPrint = True
                                            '+ Denuncio FullHouse.
                                        Case 4
                                            .sCodispl = "SIL762"
                                            .ReportFilename = "SIL762_FH.rpt"
                                            nPrint = True
                                            '+ Denuncio SOAP.
                                        Case 6
                                            .sCodispl = "SIL762"
                                            .ReportFilename = "SIL762_SOAP_2.rpt"
                                            nPrint = True
                                        Case Else
                                            nPrint = False
                                    End Select
                                    If nPrint Then
                                        .setStorProcParam(1, nClaim)
                                        .setStorProcParam(2, nBranch)
                                        .setStorProcParam(3, nProduct)
                                        .setStorProcParam(4, nPolicy)
                                        .setStorProcParam(5, nCertif)
                                        .setStorProcParam(6, nCase)
                                        .setStorProcParam(7, nDeman_type)
                                        .setStorProcParam(8, "1")
                                        .setStorProcParam(9, vbNullString)
                                        .setStorProcParam(10, vbNullString)
                                        Response.Write(.Command)
                                        .Reset()
                                    End If
                                End If
                            Next
                        End If
                    End If

                    'nCase = Mid(Request.Form("cbeCase"), 1, InStr(1, Request.Form("cbeCase"), "/", 1) - 1)
                    'UPGRADE_NOTE: Object lclsProdmaster may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsProdmaster = Nothing
                End If

                '+ Si se requiere la impresión de la carátula del siniestro.
                If Request.Form("chkPrintNow") = "1" Then
                    lcolReport_prod = New eProduct.report_prods

                    If Session("sBrancht") <> "6" AndAlso lcolReport_prod.FindReport_prod_By_Transac(Session("sCertype"), _
                                                                  Session("nBranch"), _
                                                                  Session("nProduct"), _
                                                                  0, _
                                                                  0, _
                                                                  1, _
                                                                  4, _
                                                                  Session("dEffecdate"), _
                                                                  True) Then

                        For Each lclsReport_prod In lcolReport_prod
                            If lclsReport_prod.sCodCodispl = "SIL006" Then
                                .sCodispl = "SIL006"
                                .ReportFilename = lclsReport_prod.sReport
                                .setStorProcParam(1, Session("nClaim"))
                                .setStorProcParam(2, Session("nUserCode"))
                                .bTimeOut = True
                                .nTimeOut = 5000
                                Response.Write(.Command)
                            End If
                        Next

                    Else
                        .sCodispl = "SIL006"
                        .ReportFilename = "SIL006.rpt"
                        .setStorProcParam(1, Session("nClaim"))
                        .setStorProcParam(2, Session("nUserCode"))
                        .bTimeOut = True
                        .nTimeOut = 5000
                        Response.Write(.Command)
                    End If
                End if

                '+ carta de Orden de servicio.
                If Session("lintPrint") = 1 Then


                    If lrecProf_ords.Find(Session("nClaim")) Then
                        For lintCount = 1 To lrecProf_ords.Count
                            lrecProf_ord = lrecProf_ords.Item(lintCount)
                            If (lrecProf_ord.nOrdertype = "17" Or lrecProf_ord.nOrdertype = "18") Then
                                If lrecProf_ord.nOrdertype = "17" and Request.Form("chkPrintProfOrdProv_Work") = "1" Then
                                    .Reset()
                                    .sCodispl = "SIL050"
                                    .ReportFilename = "PROFORD_WORKSH.rpt"
                                    .setStorProcParam(1, Session("nClaim"))
                                    .setStorProcParam(2, lrecProf_ord.nCase_Num)
                                    .setStorProcParam(3, lrecProf_ord.nDeman_Type)
                                    .setStorProcParam(4, lrecProf_ord.nServ_Order)
                                    .setStorProcParam(5, lrecProf_ord.nOrdertype)
                                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                    .setStorProcParam(7, Session("nUsercode"))
                                    nPrint = True
                                    Response.Write(.Command)

                                    .Reset()

                                    .sCodispl = "SIL050"
                                    .ReportFilename = "PROFORD_PROVIDER.rpt"
                                    .setStorProcParam(1, Session("nClaim"))
                                    .setStorProcParam(2, lrecProf_ord.nCase_Num)
                                    .setStorProcParam(3, lrecProf_ord.nDeman_Type)
                                    .setStorProcParam(4, lrecProf_ord.nServ_Order)
                                    .setStorProcParam(5, lrecProf_ord.nOrdertype)
                                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                    .setStorProcParam(7, Session("nUsercode"))
                                    nPrint = True
                                    Response.Write(.Command)
                                ElseIf lrecProf_ord.nOrdertype = "18" and Request.Form("chkPrintProfOrdAjust") = "1" Then
                                    .Reset()
                                    .sCodispl = "SIL050"
                                    .ReportFilename = "PROFORD_AJUST.rpt"
                                    .setStorProcParam(1, Session("nClaim"))
                                    .setStorProcParam(2, lrecProf_ord.nCase_Num)
                                    .setStorProcParam(3, lrecProf_ord.nDeman_Type)
                                    .setStorProcParam(4, lrecProf_ord.nServ_Order)
                                    .setStorProcParam(5, lrecProf_ord.nOrdertype)
                                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                    .setStorProcParam(7, Session("nUsercode"))
                                    nPrint = True
                                    Response.Write(.Command)
                                End If
                            End If
                        Next
                    End If
                End If
            End With
        End If

        'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("valclaimseq")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.40
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "valclaimseq"
    mdblTotal = 0
    mdblTotal2 = 0
    mstrLocationSI001 = vbNullString
    mstrCommand = "&sModule=Claim&sProject=ClaimSeq&sCodisplReload=" & Request.QueryString("sCodispl")
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/ConstLanguage.aspx" -->
    <%=mobjValues.StyleSheet()%>
</head>
<body>
    <form id="FORM1" name="FORM1">
    <script>
        //+ Variable para el control de versiones 
        document.VssVersion = "$$Revision: 8 $|$$Date: 2-05-13 9:20 $|$$Author: Jrengifo $"

        //%NewLocation: se recalcula el URL de la página
        //---------------------------------------------------------------------------------------
        function NewLocation(Source, Codisp)
        //---------------------------------------------------------------------------------------
        {
            var lstrLocation = "";
            lstrLocation += Source.location;
            lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp
            Source.location = lstrLocation
        }

    </script>
    <%

        '+ Si no se han validado los campos de la página
        If Request.Form("sCodisplReload") = vbNullString Then
            mstrErrors = insvalSequence()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
        Else

            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If


        If Request.QueryString("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
            If mstrErrors > vbNullString Then
                With Response
                    .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """, ""ClaimSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                    '.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""ClaimSeqError"",660,330);")
                    .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
                    .Write("</SCRIPT>")
                End With

            Else
                Dim resultPost = insPostSequence()
                If resultPost Then
                    If Request.QueryString("WindowType") <> "PopUp" Then
                        '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                        '+ se mueve automaticamente a la siguiente página
                        If mstrLocationSI001 = vbNullString Then
                            If CStr(Session("SI021_nClaim")) <> vbNullString Then
                                Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI021';</SCRIPT>")
                            ElseIf Request.Form("sCodisplReload") = vbNullString Then
                                If Request.QueryString("sCodispl") = "SI007" And CStr(Session("SI007_Codispl")) = "SI021" Then
                                    Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI021';</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "';</SCRIPT>")
                                End If
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "';</SCRIPT>")
                            End If
                        Else
                            '+ Se carga nuevamente la ventana principal de la secuencia
                            If Request.Form("sCodisplReload") = vbNullString Then
                                    Response.Write("<SCRIPT>top.document.location='" & mstrLocationSI001 & "';</SCRIPT>")
                            Else
                                    Response.Write("<SCRIPT>window.close();opener.top.document.location='" & mstrLocationSI001 & "';</SCRIPT>")
                                End If
                            End If

                        If Request.QueryString("nZone") = 1 Then
                            Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
                        End If
                    Else

                        '+ Se recarga la página que invocó la PopUp
                        Select Case Request.QueryString("sCodispl")
                            Case "SI007"
                                If Request.Form("sCodisplReload") = vbNullString Then
                                    Response.Write("<SCRIPT>top.opener.document.location.href='SI007.aspx?sCodispl=SI007&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nTotal=" & mdblTotal & mstrQueryString & "'</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.close();top.opener.top.opener.document.location.href='SI007.aspx?sCodispl=SI007&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nTotal=" & mdblTotal & mstrQueryString & "'</SCRIPT>")
                                End If
                            Case "SI013"
                                Response.Write("<SCRIPT>top.opener.document.location.href='SI013.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nTransac=" & Request.Form("hddnTransac") & "&nBordereaux=" & Request.Form("hddnBordereaux") & "&sDescriptCurrency=" & Request.QueryString("sDescriptCurrency") & "&nPreviousAmou=" & mobjValues.TypeToString(Request.QueryString("nPreviousAmou"), eFunctions.Values.eTypeData.etdDouble) & "&nPreviousExpense=" & mobjValues.TypeToString(Request.QueryString("nPreviousExpense"), eFunctions.Values.eTypeData.etdDouble) & "'</SCRIPT>")
                            Case "SI015"
                                Response.Write("<SCRIPT>top.opener.document.location.href='SI015.aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCase=" & Request.QueryString("sCase") & mstrQueryString & "'</SCRIPT>")
                            Case "SI017"
                                Response.Write("<SCRIPT>top.opener.document.location.href='SI017.aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nCase_num=" & Request.QueryString("nCase_num") & "&nDeman_type=" & Request.QueryString("nDeman_type") & "&sClient=" & Request.QueryString("sClient") & mstrQueryString & "'</SCRIPT>")
                            Case "SI764"
                                Response.Write("<SCRIPT>top.opener.document.location.href='SI764.aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nCase_num=" & Request.QueryString("nCase_num") & "&nDeman_type=" & Request.QueryString("nDeman_type") & "&sClient=" & Request.QueryString("sClient") & mstrQueryString & "'</SCRIPT>")
                            Case "SI629"
                                If Request.Form("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.document.location.href='SI629.aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nCase_num=" & Request.QueryString("nCase_num") & "&nDeman_type=" & Request.QueryString("nDeman_type") & mstrQueryString & "'</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.close();top.opener.top.opener.document.location.href='SI629.aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nCase_num=" & Request.QueryString("nCase_num") & "&nDeman_type=" & Request.QueryString("nDeman_type") & mstrQueryString & "'</SCRIPT>")
                                End If
                            Case "SI004"
                                If Request.Form("sCodisplReload") = vbNullString Then
                                    Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & mstrQueryString & "'</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & mstrQueryString & "'</SCRIPT>")
                                End If

                            Case Else
                                Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & mstrQueryString & "'</SCRIPT>")
                        End Select
                    End If
                Else
                    Response.Write("<SCRIPT>alert('No se realizó el proceso')</SCRIPT>")
                End If
            End If
        Else
            '+ Se recarga la página principal de la secuencia            
            If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                Session("LetterTypeId") = Nothing
                Session("NullClaimId") = Nothing

                If insFinish() Then
                    If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimIssue And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRecovery And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimAmendment And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngApproval And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimReopening And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection And Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngCaratula Then

                        If Request.QueryString("sCodispl") = "SI007" And CStr(Session("SI007_Codispl")) = "SI021" Then
                            If insPostSequence() Then
                                Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI021';</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>alert('No se realizó el proceso')</SCRIPT>")
                            End If
                        Else
                            Response.Write("<SCRIPT>top.document.location=" & mstrLocationSI001 & ";</SCRIPT>")
                        End If
                    Else
                        If Request.QueryString("sCodispl") = "SI003" Then
                            Response.Write("<SCRIPT>top.document.location='/VTimeNet/Common/secWHeader.aspx?sCodispl=SI001&sProject=ClaimSeq&sModule=Claim';</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>setTimeout(""top.opener.top.document.location=" & mstrLocationSI001 & ";"",8000);</SCRIPT>")
                        End If
                    End If
                End If
            End If
        End If
        'UPGRADE_NOTE: Object mobjClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjClaimSeq = Nothing
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.40
    Call mobjNetFrameWork.FinishPage("valclaimseq")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
