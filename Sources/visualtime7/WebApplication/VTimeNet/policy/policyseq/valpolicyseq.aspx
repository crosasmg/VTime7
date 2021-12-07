<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eApvc" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eSaapv" %>

<script language="VB" runat="Server">
    Dim mobjPolicySeq As Object
    Dim mstrErrors As String
    Dim mstrLocationCA001 As String
    Dim mobjValues As eFunctions.Values
    Dim lclsPolicy As Object
    Dim mstrScript As String
    Dim lintCurrency As Object
    Dim llngPayfreq As Integer
    Dim mobjPolicyseqAviat_Marit As ePolicy.Aviat_marit

    '- Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '- Variable para el manejo del QueryString  
    Dim mstrQueryString As String

    Dim mblnCreateInsured As Boolean

    '-Variable para indicar si ya se ejecutaron las validaciones
    Dim mblnReload As Boolean
    Dim lclsRefresh As ePolicy.ValPolicySeq
    Dim mstrTotalPrima As Double

    Dim mstrsPolitype As String

    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalSequence() As String

        Dim ldblPercent As Object
        Dim llngTariff As Object
        Dim lintnMaxRole As String
        Dim sCopiar As String = ""
        Dim nModulec As Object
        Dim sActivefound As String
        Dim lintnExist As String
        Dim lclsFunds_CO_P As Object
        Dim sActivefound_P As String
        Dim optDirect As Byte
        Dim nCover As Object
        Dim lclsCliallopro As Object
        Dim mclsErrors As Object
        Dim lintDescript As Integer
        Dim lintGrid As Integer
        Dim liFabYear As Object
        '--------------------------------------------------------------------------------------------
        Dim lintIntermedia As Integer
        Dim lintIntermediaOld As Integer
        Dim lstrClient As String
        Dim lstrClientOld As String
        Dim lclsPolicy_Win As ePolicy.Policy_Win

        '    mobjNetFrameWork.BeginProcess "ValSequence|" & Request.QueryString("sCodispl")
        Dim lclsRefresh As ePolicy.ValPolicySeq
        Dim mobjApvc As eApvc.Life_Apvc
        Dim lclsFunds_Pol As ePolicy.Funds_Pol
        Dim lclsDecla_benef As ePolicy.Decla_benef
        Dim mobjPolicySeq1 As ePolicy.ValPolicySeq
        Dim lintCountSel As Integer
        Dim lintCountCA013 As Integer
        Dim mobjsAapv As eSaapv.Saapv
        Dim lclsInsured_expdis As ePolicy.Insured_expdis

        Select Case Request.QueryString.Item("sCodispl")
            '+ VI641: Criterios para seleción de riesgo
            Case "VI641"
                mobjPolicySeq = New ePolicy.Life_p_speci
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.InsValVI641Upd(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"),
                                                                      Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                                      mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                      mobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdDouble),
                                                                      .Form.Item("cbeSexinsur"),
                                                                      mobjValues.StringToType(.Form.Item("tcnAgestart"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(.Form.Item("tcnAgeend"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(.Form.Item("tcnCapstart"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(.Form.Item("tcnCapend"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(.Form.Item("cbeCrthecni"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                      mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With
            Case "CA061"
                mobjPolicySeq = New ePolicy.Creditor_information
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA061(Session("sCertype"), Session("nBranch"),
                                                                    Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    Session("nUsercode"), .QueryString("Action"),
                                                                    mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("hddConsecutive"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("cbeDetail_Item"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("hddgridID"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = mobjPolicySeq.insValCA061_k(.Form.Item("tctText"),
                                                                    Session("sCertype"), Session("nBranch"),
                                                                    Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate, True),
                                                                    mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True))
                    End If

                End With


            '+ CA727: Reportes automáticos de la póliza
            Case "CA727"
                mobjPolicySeq = New ePolicy.PolReport

                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA727("CA727", Session("scertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cbeCodispl"), mobjValues.StringToType(Request.Form.Item("cbeTransactype"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ VI665: Recargo por actividad del grupo (Vida Colectivo).
            Case "VI665"
                mobjPolicySeq = New ePolicy.Activ_Group
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValVI665("VI665", Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With
            '+ VI8000: Ahorros garantizados.
            Case "VI8000"
                mobjPolicySeq = New ePolicy.Guar_Saving_Pol
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValVI8000("VI8000", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnGuarSavid"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboGuarSav_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStart_GuarSav"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_GuarSav_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnGuarSav_value"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGuarSav_stat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRen_guarSav"), eFunctions.Values.eTypeData.etdDouble), "1", Request.QueryString.Item("Action"))
                    Else
                        insvalSequence = mobjPolicySeq.insValVI8000("VI8000", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, Today, Today, 0, 0, 0, 0, 0, "2", Request.QueryString.Item("Action"))
                    End If


                    lclsRefresh = New ePolicy.ValPolicySeq

                    Response.Write(lclsRefresh.RefreshSequence("VI8000", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "no"))
                    lclsRefresh = Nothing

                End With

            '+ CA054: Capital Despreciados.
            Case "CA054"
                mobjPolicySeq = New ePolicy.DepreciatedCapital
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA054Upd("CA054", mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitialCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = Nothing
                    End If
                End With
            '+ VI681: Recargos/Descuentos de los asegurados (VIDA).
            Case "VI681"
                lclsInsured_expdis = New ePolicy.Insured_expdis
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = lclsInsured_expdis.insValVI681Upd("VI681", Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Request.Form.Item("hddsClient"), mobjValues.StringToType(.Form.Item("cboDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cboDisexpri"), .Form.Item("chkUnit"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPermTemp"), mobjValues.StringToType(.Form.Item("tcdDate_Fr"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("btnNoteNum"), mobjValues.StringToType(.Form.Item("hddoldnDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnCover"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = lclsInsured_expdis.insValVI681("VI681", Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With
                lclsInsured_expdis = Nothing

            '+ Tratamiento de pólizas
            Case "CA001"
                Session("PageRetCA050") = "CA001"
                With Request
                    'ValPolicySec.vb
                    insvalSequence = mobjPolicySeq.insValCA001(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicyDest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optType"), mobjValues.StringToType(.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSellChannel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProp_reg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFolio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat_Associated"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnProcess_num"), mobjValues.StringToType(.Form.Item("tcnPolicy_Transfer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("NTYPEACCOUNT"), eFunctions.Values.eTypeData.etdDouble, True))

                    'sPolitype 
                    Session("sPolitype") = mobjPolicySeq.mstrsPolitype

                    ' 
                    ' + *********Inicio modificación apvc********* 
                    ' + invocación validación producto apvc 
                    'If insvalSequence = vbNullString Then
                    ' mobjApvc = New eApvc.Life_Apvc
                    'insvalSequence = mobjApvc.insValCA001("0", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sCodispl"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble, True))

                    ' mobjApvc = Nothing
                    'End If
                End With
            ' + *********Fin  modificación apvc********* 


            Case "CA001C"
                With Request
                    Session("PageRetCA050") = "CA001C"
                    insvalSequence = mobjPolicySeq.insValCA001(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optType"), mobjValues.StringToType(.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSellChannel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProp_reg"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ CA003: Vía de cobro 
            Case "CA003"
                With Request
                    insvalSequence = mobjPolicySeq.insValCA003("CA003", .Form.Item("optBank"), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valAccount"), mobjValues.StringToType(.Form.Item("cbeTyp_Account"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTyp_crecard"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateExpir"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctBankAuth"), .Form.Item("valCredi_card"), mobjValues.StringToType(.Form.Item("hddWay_pay"), eFunctions.Values.eTypeData.etdLong), .Form.Item("hddDirind"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                End With
            '+ CA004: Datos para la facturación
            Case "CA004"
                With Request
                    insvalSequence = mobjPolicySeq.insValCA004("CA004", mobjValues.StringToType(.Form.Item("tcdIssuedat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcdExpirDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optFreq"), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeQuota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIndexApl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIndexType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndexRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), Session("sPolitype"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("Action"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nHolder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optDirTyp"), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSendAddr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAFPCommi"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDepreciationtable"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chksInd_Multiannual"), .Form.Item("chksInd_IFI"), mobjValues.StringToType(.Form.Item("tcnExtraDay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenFormPay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbenPromissory_Note"), eFunctions.Values.eTypeData.etdInteger,  True))
                End With

            '+ CA006: Grupos de colectivos
            '+ Inf. general del colectivo     
            Case "CA006"
                With Request
                    insvalSequence = mobjPolicySeq.insValCA006(Session("sCertype"), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), "CA006", .Form.Item("sColtimre"), .Form.Item("cbeColInvot"), .Form.Item("cbeColReint"), .Form.Item("cbeTypModule"), .Form.Item("cbeTypClause"), .Form.Item("cbeTypDiscxp"), .Form.Item("cbeDocuTyp"), mobjValues.StringToType(.Form.Item("tcnQCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbenTypeExc"), eFunctions.Values.eTypeData.etdInteger))
                End With
            '+ VA1410: Ilustración del valor póliza VUL
            Case "VI1410"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Per_deposit

                        insvalSequence = mobjPolicySeq.InsValVA595Upd("VI1410", .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremiumbas"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"))
                    Else
                        mobjPolicySeq = New ePolicy.Projectvul

                        insvalSequence = mobjPolicySeq.InsValVI1410("VI1410", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremiumbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), mobjValues.StringToType(.Form.Item("tcnPayiniti"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With
            Case "VI1410A"
                mobjPolicySeq = New ePolicy.Per_deposit_month
                With Request
                    insvalSequence = mobjPolicySeq.InsValVI1410AUpd("VI1410A", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAmountdep_aux"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
                End With

            Case "VI7006"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Per_deposit
                        insvalSequence = mobjPolicySeq.InsValVA595Upd("VI1410", .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        mobjPolicySeq = New ePolicy.Projectvul

                        insvalSequence = mobjPolicySeq.InsValVI1410("VI1410", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"))
                    End If
                End With
            '**+ CA008: Risk Situations.
            '+ CA008: Situaciones de riesgo.

            Case "CA008"
                mobjPolicySeq = New ePolicy.Situation
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request

                        insvalSequence = mobjPolicySeq.insValCA008(.QueryString("Action"), "CA008", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSituation"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("cbePolicyHolder"),
                                                                   mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                   mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                Else
                    insvalSequence = mobjPolicySeq.insValCA008_K("CA008", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))

                End If
                mobjPolicySeq = Nothing

            '+ CA002: Convenios de la cobranza de la póliza
            Case "CA002"
                mobjPolicySeq = New ePolicy.Agreement_pol
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request

                        insvalSequence = mobjPolicySeq.insValCA002(.QueryString("Action"), "PopUp", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("ncertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCod_Agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    With Request
                        insvalSequence = mobjPolicySeq.insValCA002(.QueryString("Action"), "Form", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("ncertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCod_Agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                End If
                mobjPolicySeq = Nothing

            '+ CA009: Capitales básicos Asegurados
            Case "CA009"
                mobjPolicySeq = New ePolicy.Sum_insur
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        insvalSequence = mobjPolicySeq.insValCA009Upd("CA009", .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnSumins_real"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCoinsuran"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSum_insur"), eFunctions.Values.eTypeData.etdDouble, True))

                    End With
                Else
                    insvalSequence = mobjPolicySeq.insValCA009(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                End If

            '+ CA010: Bienes asegurables de la póliza
            Case "CA010"
                With Request
                    mobjPolicySeq = New ePolicy.Property_Renamed
                    If Not Request.QueryString.Item("WindowType") = vbNullString Then
                        insvalSequence = mobjPolicySeq.insValCA010("CA010", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFixamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOriginalRateProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOriginalPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFrandedi"), eFunctions.Values.eTypeData.etdDouble), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("nId"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = mobjPolicySeq.insValCA010All("CA010", Session("nBranch"), Session("nProduct"), Session("sCertype"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ CA010: Bienes asegurables de la póliza
            Case "CA060"
                With Request
                    mobjPolicySeq = New ePolicy.Cover_Detail
                    If Not Request.QueryString.Item("WindowType") = vbNullString Then
                        insvalSequence = mobjPolicySeq.insValCA060("CA060", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
                    Else
                        insvalSequence = vbNullString
                    End If
                End With
            Case "CA060"
                With Request
                    mobjPolicySeq = New ePolicy.Cover_Detail
                    If Not Request.QueryString.Item("WindowType") = vbNullString Then
                        insvalSequence = mobjPolicySeq.insValCA060("CA060", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
                    Else
                        insvalSequence = vbNullString
                    End If
                End With
            Case "CA011"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        insvalSequence = mobjPolicySeq.insValCA011("CA011", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeGroupStat"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End With
                Else
                    insvalSequence = vbNullString
                End If

            '+ CA012: Elementos de Protección            
            Case "CA012"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        mobjPolicySeq = New ePolicy.Protection
                        insvalSequence = mobjPolicySeq.insValCA012("CA012", mobjValues.StringToType(.Form.Item("tcnElement"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnDisrate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                Else
                    insvalSequence = vbNullString
                End If

            '+ CA013: Módulos de Póliza Individual o Certificado
            Case "CA013", "CA013A"
                With Request
                    If Request.QueryString.Item("WindowType") <> "PopUp" Then
                        If Request.QueryString.Item("sCodispl") = "CA013A" Then
                            mobjPolicySeq = New ePolicy.ValPolicySeq
                            insvalSequence = mobjPolicySeq.InsValCA013A(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Request.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTyp_module"))
                        Else
                            mobjPolicySeq = New ePolicy.Modules
                            lintCountSel = 0
                            If Not IsNothing(.Form.GetValues("hddsChecked")) Then
                                For lintCountCA013 = 0 To .Form.GetValues("hddsChecked").Count - 1
                                    If .Form.GetValues("hddsChecked").GetValue(lintCountCA013) = "1" Then
                                        lintCountSel = lintCountCA013 + 1
                                    End If
                                Next
                            End If
                            insvalSequence = mobjPolicySeq.InsValCA013(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("hddnModulec"), .Form.Item("hddsChecked"), .Form.Item("cbeCurrency"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), lintCountSel)
                        End If
                    Else
                        mobjPolicySeq = New ePolicy.Modules
                        insvalSequence = mobjPolicySeq.InsValCA013Upd(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddstyp_rat"))
                    End If
                End With

            '+ CA014: Coberturas de la póliza 
            Case "CA014", "CA014A"
                mobjPolicySeq = New ePolicy.Cover
                With Request
                    If .QueryString.Item("ActionType") = "Check" Then
                        If .Form.Item("hddsExist").Length = 1 Then
                            insvalSequence = mobjPolicySeq.InsValCA014Upd(.QueryString("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRatecove"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsFrandedi"), .Form.Item("hddsFrancApl"), mobjValues.StringToType(.Form.Item("hddnFraRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnFixamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnMinamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddsWait_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRatecove_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnMaxamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDiscount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDisc_amoun"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRole"), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hddnWaitQ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgeIns"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgeminins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxper"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.Item("hddnCauseupd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsKeyGrid"), mobjValues.StringToType(.Form.Item("hddnAgemininsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDurinsur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTypdurins"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsExist"),
                                                                          mobjValues.StringToType(.Form.Item("tcnFraRateClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnFixamountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnMinAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnMaxAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnDiscountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnDisc_amounClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnFrancdaysClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                      , , , mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), .Form.Item("hddsChange"), Session("sSche_code"), .Form.Item("hddsVIP"), , mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))

                            If insvalSequence > vbNullString Then
                                mstrScript = "top.frames['fraFolder'].document.forms[0].Sel.checked=" & .QueryString.Item("sChecked") & "false;" & "top.frames['fraFolder'].marrArray[0].Sel =" & .QueryString.Item("sChecked") & "false;"
                                mstrQueryString = "&sKey=" & Request.Form.Item("hddsKeyGrid")
                            End If
                        Else
                            'ORIGINAL
                            'insvalSequence = mobjPolicySeq.InsValCA014Upd(.QueryString("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hddnModulec").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCover").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCapital").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRatecove").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsFrandedi").GetValue(Cint(.QueryString("nIndex")) - 1), .Form.GetValues("hddsFrancApl").GetValue(Cint(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnFraRate").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnFixamount").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnMinamount").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddsWait_Type").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCapital_o").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRatecove_o").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium_o").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnMaxamount").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDiscount").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDisc_amoun").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRole").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"), mobjValues.StringToType(.Form.GetValues("hddnWaitQ").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgeIns").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgeminins").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxins").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxper").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.GetValues("hddnCauseupd").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsKeyGrid").GetValue(Cint(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnAgemininsf").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxinsf").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxperf").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnBranch_rei").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDurinsur").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnTypdurins").GetValue(Cint(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsExist").GetValue(Cint(.QueryString("nIndex")) - 1),  ,  ,  , mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), .Form.GetValues("hddsChange").GetValue(Cint(.QueryString("nIndex")) - 1), Session("sSche_code"), .Form.Item("hddsVIP"),  , mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))
                            insvalSequence = mobjPolicySeq.InsValCA014Upd(.QueryString("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hddnModulec").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCover").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCapital").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRatecove").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsFrandedi").GetValue(CInt(.QueryString("nIndex")) - 1), .Form.GetValues("hddsFrancApl").GetValue(CInt(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnFraRate").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnFixamount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnMinamount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddsWait_Type").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCapital_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRatecove_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnMaxamount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDiscount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDisc_amoun").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRole").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"), mobjValues.StringToType(.Form.GetValues("hddnWaitQ").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgeIns").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgeminins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxper").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.GetValues("hddnCauseupd").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsKeyGrid").GetValue(CInt(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnAgemininsf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxinsf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxperf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnBranch_rei").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDurinsur").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnTypdurins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsExist").GetValue(CInt(.QueryString("nIndex")) - 1),
                                                                          mobjValues.StringToType(.Form.GetValues("hddFraRateClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddFixamountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddMinAmountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddMaxAmountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddDiscountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddDisc_amounClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.GetValues("hddFrancdays").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                      , , , mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), .Form.GetValues("hddsChange").GetValue(CInt(.QueryString("nIndex")) - 1), Session("sSche_code"), .Form.Item("hddsVIP"), , mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))

                            If insvalSequence > vbNullString Then
                                mstrScript = "top.frames['fraFolder'].document.forms[0].Sel[" & CStr(CShort(.QueryString.Item("nIndex")) - 1) & "].checked=" & .QueryString.Item("sChecked") & "false;" & "top.frames['fraFolder'].marrArray[" & CStr(CShort(.QueryString.Item("nIndex")) - 1) & "].Sel =" & .QueryString.Item("sChecked") & "false;"
                                'ORIGINAL
                                'mstrQueryString = "&sKey=" & Request.Form.GetValues("hddsKeyGrid").GetValue(Cint(Request.QueryString.Item("nIndex")) - 1)
                                mstrQueryString = "&sKey=" & Request.Form.GetValues("hddsKeyGrid").GetValue(CInt(Request.QueryString.Item("nIndex")) - 1)
                            End If
                        End If
                        Response.Write("<SCRIPT>")
                        Response.Write(mstrScript)
                        Response.Write("top.frames['fraFolder'].document.forms[0].action='ValPolicySeq.aspx?nRole=" & Request.QueryString.Item("nRole") & "&sClient=" & Request.QueryString.Item("sClient") & "&nIndexCover=" & Request.QueryString.Item("nIndexCover") & "';")
                        Response.Write("top.frames['fraFolder'].mstrDoSubmit = '1';")
                        Response.Write("</" & "Script>")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insvalSequence = mobjPolicySeq.InsValCA014Upd(.QueryString("sCodispl"),
                                                                          Session("sCertype"),
                                                                          mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                          mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnRatecove"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble),
                                                                          .Form.Item("cbeFrandedi"),
                                                                          .Form.Item("cbeFrancApl"),
                                                                          mobjValues.StringToType(.Form.Item("tcnFraRate"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnFixamount"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnMinamount"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("cbeWait_Type"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("hddnRatecove_o"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("hddnPremium_o"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnMaxamount"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("tcnDisc_amoun"), eFunctions.Values.eTypeData.etdDouble),
                                                                          mobjValues.StringToType(.Form.Item("hddnRole"), eFunctions.Values.eTypeData.etdDouble),
                                                                          Session("sBrancht"),
                                                                          mobjValues.StringToType(.Form.Item("tcnWaitQ"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("hddnAgeIns"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgeminins"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgemaxins"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgemaxper"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          .Form.Item("hddsClient"),
                                                                          mobjValues.StringToType(.Form.Item("cbeCauseupd"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble),
                                                                          .Form.Item("hddsKeyGrid"),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgemininsf"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("valBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnDurinsur"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("cbeTypdurins"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          .Form.Item("hddsExist"),
                                                                          mobjValues.StringToType(.Form.Item("tcnFraRateClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnFixamountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnMinAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnMaxAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnDiscountClaim"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnDisc_amounClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancdays"), eFunctions.Values.eTypeData.etdDouble, True), , , ,
                                                                          mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble), ,
                                                                          .Form.Item("hddsChange"),
                                                                          Session("sSche_code"), .Form.Item("hddsVIP"), ,
                                                                          mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnPremimax"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnPremimin"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnCacalmax"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                          mobjValues.StringToType(.Form.Item("tcnCacalmin"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            If mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                                sCopiar = "1" 'VERDADERO
                            End If

                            insvalSequence = mobjPolicySeq.InsValCA014(.Form.Item("hddsKey"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrencDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hddnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), Session("sSche_code"), sCopiar)
                        End If
                    End If
                End With

            '+ CA015: Franquicia/Deducible de la Póliza
            Case "CA015", "CA15-1"
                mobjPolicySeq = New ePolicy.Franchise
                With Request
                    insvalSequence = mobjPolicySeq.insValCA015(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CStr(Session("nTransaction")), Session("sTypeCompanyUser"), .Form.Item("optFranchiseType"), .Form.Item("cbeFranqApl"), mobjValues.StringToType(.Form.Item("cbeCurrencyFD"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscountPerc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscountAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchisePerc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ CA016: Recargos/descuentos/impuestos de una póliza individual o certificado
            Case "CA016", "CA016A"
                mobjPolicySeq = New ePolicy.Disc_xprem
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.InsValCA016Upd(.QueryString("sCodispl"), .Form.Item("hddsChanallo"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnOriPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnOriAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDisexaddper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnDisexsubper"), eFunctions.Values.eTypeData.etdDouble, True), Session("sSche_code"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSpecialbusiness"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        insvalSequence = mobjPolicySeq.InsValCA016(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddnDisc_code"), .Form.Item("hddsSel"), .Form.Item("hddnPercent"), .Form.Item("hddsDisexpri"), mobjValues.StringToType(Session("nSpecialbusiness"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            '+ CA017: Emisión de Recibos de una póliza
            Case "CA017"
                With Request

                    insvalSequence = mobjPolicySeq.insValCA017("CA017", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cboReceipts"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsList"), mobjValues.StringToType(.Form.Item("hddPremium"), eFunctions.Values.eTypeData.etdDouble))
                End With


            '+ CA017A: Cuotas del recibo 
            Case "CA017A"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        '+ se envia a validar el encabezado (sin datos de la grilla)  
                        '+ Se valida solo la pagina de cuotas posee datos 
                        If CBool(.Form.Item("hddbValCa017a")) Then
                            insvalSequence = mobjPolicySeq.insValCA017A(Request.QueryString.Item("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeQuota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuoPend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValQuot"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumP"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            '+ CA017B: Emisión de Recibos de una póliza solicitud de endoso           
            Case "CA017B"
                With Request

                    insvalSequence = mobjPolicySeq.insValCA017("CA017B", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cboReceipts"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsList"), mobjValues.StringToType(.Form.Item("hddPremium"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ CA020: Distribución de Coaseguro
            Case "CA020"
                With Request
                    mobjPolicySeq = New ePolicy.Coinsuran
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA020(.QueryString("WindowType"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        insvalSequence = mobjPolicySeq.insValCA020(.QueryString("WindowType"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), Session("nCompanyUser"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnOwnShare"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddRecordCount"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With


            '+ CA021: Cesión de Reaseguro - 
            Case "CA021"

                If Not Session("bQuery") Then
                    mobjPolicySeq = New ePolicy.Reinsuran

                    With Request
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            If .Form.Item("blnContract") = "True" Then
                                lintGrid = 2
                            ElseIf Request.Form.Item("tctPopUpT") = "Cov" Then
                                lintGrid = 1
                            ElseIf Request.Form.Item("tctPopUpT") = "F" Then
                                lintGrid = 3
                            End If

                            insvalSequence = mobjPolicySeq.insValCA021Upd(lintGrid, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnComission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnReser_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInter_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdAcceptdate"), eFunctions.Values.eTypeData.etdDate), Session("nCompanyUser"), mobjValues.StringToType(.Form.Item("cbeBranchrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valClient"), Request.QueryString.Item("Action"))
                        Else

                            '+ Se efectúan las validaciones masivas cuando es diferente a popup(Validación de las sumas distribuidas).
                            insvalSequence = mobjPolicySeq.insValCA021(mobjValues.StringToType(.Form.Item("hddRest"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End With
                    mobjPolicySeq = Nothing
                Else
                    insvalSequence = vbNullString
                End If


            '+ CA021A: Cesión de Reaseguro Póliza Matriz
            Case "CA021A"

                If Not Session("bQuery") Then
                    mobjPolicySeq = New ePolicy.Reinsuran

                    With Request
                        If .QueryString.Item("WindowType") = "PopUp" Then

                            mstrQueryString = "&sPriority=" & .QueryString.Item("sPriority") & "&tcnFacAmount=" & .QueryString.Item("tcnFacAmount") & "&sIsCOB=" & .QueryString.Item("sIsCOB") & "&sIsFACOB=" & .QueryString.Item("sIsFACOB") & "&tcnFacPer=" & .QueryString.Item("tcnFacPer") & "&tcnContPer=" & .QueryString.Item("tcnContPer") & "&chkPercen=" & .QueryString.Item("chkPercen") & "&chkAmount=" & .QueryString.Item("chkAmount")

                            If Request.QueryString.Item("sIsCOB") <> "1" Then
                                mstrQueryString = mstrQueryString & "&nContAmount=" & .Form.Item("tcnContAmount")

                                insvalSequence = mobjPolicySeq.insValCA021AUpd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnComission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnReser_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInter_rate"), eFunctions.Values.eTypeData.etdDouble), System.Math.Round(CDbl(mobjValues.StringToType(Request.Form.Item("tcnPercentage"), eFunctions.Values.eTypeData.etdDouble)), 6), mobjValues.StringToType(Request.Form.Item("tcdAcceptDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdLong))
                            Else

                                insvalSequence = mobjPolicySeq.insValCA021AUpd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, 0, 0, System.Math.Round(CDbl(mobjValues.StringToType(Request.Form.Item("tcnQuota_sha"), eFunctions.Values.eTypeData.etdDouble)), 6), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("hddnType"), eFunctions.Values.eTypeData.etdLong))
                            End If

                        Else

                            '+ Se efectúan las validaciones masivas cuando es diferente a popup(Validación de las sumas distribuidas).
                            insvalSequence = mobjPolicySeq.insValCA021A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnFacPer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnFacAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContPer"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End With
                    mobjPolicySeq = Nothing
                Else
                    insvalSequence = vbNullString
                End If

            '+ CA022: Cláusula/descriptivo/condición especial        
            Case "CA022"
                mobjPolicySeq = New ePolicy.Clause
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.InsValCA022Upd(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("valClause"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valInsured"), mobjValues.StringToType(Request.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCover"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddSel"), Session("nTransaction"), .QueryString("Action"))
                    Else
                        insvalSequence = mobjPolicySeq.InsValCA022(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), .QueryString("Action"))
                    End If
                End With

            '+ CA022A: Cláusulas de la póliza matriz
            Case "CA022A"
                mobjPolicySeq = New ePolicy.Claus_co_gp
                insvalSequence = mobjPolicySeq.insValCA022A(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("hddnClause"), Request.Form.Item("hddnSelClause"), mobjValues.StringToType(Request.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble))

            '+ CA023: Beneficiarios identificados por código            
            Case "CA023"
                mobjPolicySeq = New ePolicy.Beneficiar
                With Request
                    nCover = .Form.Item("valCover")
                    If nCover = "" Then
                        nCover = 0
                    End If
                    nModulec = .Form.Item("valModulec")
                    If nModulec = "" Then
                        nModulec = 0
                    End If
                    insvalSequence = mobjPolicySeq.insValCA023(.QueryString("Action"), .QueryString("sCodispl"), .QueryString("WindowType"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("dtcClient"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRelation"), eFunctions.Values.eTypeData.etdDouble, True), Session("sPolitype"), Session("nUsercode"))
                End With

            '+ CA024: Intermediarios 
            Case "CA024"
                With Request
                    mobjPolicySeq = New ePolicy.Commission
                    If CBool(IIf(IsNothing(.QueryString.Item("bAll")), False, .QueryString.Item("bAll"))) Then
                        ldblPercent = mobjValues.StringToType(.Form.Item("tcnPercentCF"), eFunctions.Values.eTypeData.etdDouble, True)
                    Else
                        ldblPercent = mobjValues.StringToType(.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True)
                    End If
                    insvalSequence = mobjPolicySeq.insValCA024(.QueryString("Action"), .QueryString("bAll"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeType"), mobjValues.StringToType(ldblPercent, eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddConColl"), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInstallCom"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent_Ce"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            '+ CA025: Cliente de la póliza
            Case "CA025"

                With Request
                    mobjPolicySeq = New ePolicy.Roles
                    mblnCreateInsured = True
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble) = 13 Then
                            lstrClient = vbNullString
                            lstrClientOld = vbNullString
                            lintIntermedia = mobjValues.StringToType(.Form.Item("tctCode"), eFunctions.Values.eTypeData.etdDouble, True)
                            lintIntermediaOld = mobjValues.StringToType(.Form.Item("hddsOldCode"), eFunctions.Values.eTypeData.etdDouble, True)
                        Else
                            lstrClient = .Form.Item("tctCode")
                            lstrClientOld = .Form.Item("hddsOldCode")
                            lintIntermedia = eRemoteDB.Constants.intNull
                            lintIntermediaOld = eRemoteDB.Constants.intNull
                        End If
                        lintnExist = .Form.Item("hddnExist")
                        lintnMaxRole = .Form.Item("hddnMaxRole")
                        insvalSequence = mobjPolicySeq.InsValCA025Upd("CA025", mobjValues.StringToType(lintnExist, eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), lstrClient, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), lintIntermedia, mobjValues.StringToType(.Form.Item("cbeStatusrol"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(lintnMaxRole, eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"), lstrClientOld, lintIntermediaOld, mobjValues.StringToType(.Form.Item("hddsOldRole"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctCode_digit"), mobjValues.StringToType(.Form.Item("tcdBirthdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeSexclien"), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("tcnRating"), eFunctions.Values.eTypeData.etdDouble, True), Session("sPolitype"), Session("nTransaction"), mobjValues.StringToType(.Form.Item("cbeTypename"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctPrintName"), .Form.Item("chkVIP"), .Form.Item("chksContinued"))
                        mblnCreateInsured = mobjPolicySeq.bCreateInsured
                    Else
                        insvalSequence = mobjPolicySeq.InsValCA025("CA025", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), .Form.Item("hddsCompon"), Session("nTransaction"), Session("sBrancht"))
                    End If
                End With

            '+ FR001: Instrumentos Financieros
            Case "FR001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = ePolicy.FinancialInstrument.Validate(.QueryString.Item("sCodispl"),
                                                                              .QueryString.Item("Action"),
                                                                              Session("sCertype"),
                                                                              Session("nBranch"),
                                                                              Session("nProduct"),
                                                                              Session("nPolicy"),
                                                                              Session("nCertif"),
                                                                              mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                              mobjValues.StringToType(.Form.Item("NCONSECUTIVE"), eFunctions.Values.eTypeData.etdInteger),
                                                                              mobjValues.StringToType(.Form.Item("NBANK_CODE"), eFunctions.Values.eTypeData.etdInteger),
                                                                              mobjValues.StringToType(.Form.Item("NINSTRUMENT_TY"), eFunctions.Values.eTypeData.etdInteger),
                                                                              mobjValues.StringToType(.Form.Item("NCARD_TYPE"), eFunctions.Values.eTypeData.etdInteger),
                                                                              .Form.Item("SNUMBER"),
                                                                              mobjValues.StringToType(.Form.Item("DCARDEXPIR"), eFunctions.Values.eTypeData.etdDate),
                                                                              mobjValues.StringToType(.Form.Item("DSTARTDATE"), eFunctions.Values.eTypeData.etdDate),
                                                                              mobjValues.StringToType(.Form.Item("DTERM_DATE"), eFunctions.Values.eTypeData.etdDate),
                                                                              mobjValues.StringToType(.Form.Item("NQUOTA"), eFunctions.Values.eTypeData.etdInteger),
                                                                              mobjValues.StringToType(.Form.Item("NAMOUNT"), eFunctions.Values.eTypeData.etdDouble),
                                                                              mobjValues.StringToType(.Form.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdInteger),
                                                                              mobjValues.StringToType(.Form.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        insvalSequence = String.Empty
                    End If
                End With

            '+ CA027, CA027A: Emisión de recibo automático
            Case "CA027", "CA027A"
                insvalSequence = vbNullString

            '+ VI021: documentos solicitados
            Case "VI021"
                With Request

                    mobjPolicySeq = New ePolicy.Life_docu
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If Request.QueryString.Item("Action") = "Add" Then
                            lintDescript = mobjValues.StringToType(.Form.Item("tctDescript"), eFunctions.Values.eTypeData.etdDouble, True)
                        Else
                            lintDescript = 0
                        End If
                        insvalSequence = mobjPolicySeq.InsValVI021Upd("VI021", mobjValues.StringToType(.Form.Item("cbeStat_docreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdRecep_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDatevig"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDatefree"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctClient"), lintDescript, Request.QueryString.Item("Action"), Session("sKey"))
                    End If
                End With

            '+ IN010: Datos particulares de incendio
            Case "IN010"
                With Request
                    insvalSequence = mobjPolicySeq.InsValIN010("IN010", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("cboArticle"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboDetailArt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFloor_quan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDep_prem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDecla_Freq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDecla_Type"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            '+ VI001: Interés asegurable
            Case "VI001"
                With Request
                    insvalSequence = mobjPolicySeq.InsValVI001("VI001", Session("sCertype"), Session("nBranch"),
                                                               Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                               Session("dEffecdate"), .Form.Item("cbovalgroup"),
                                                               mobjValues.StringToType(.Form.Item("cbovalsituation"), eFunctions.Values.eTypeData.etdInteger, True),
                                                               mobjValues.StringToType(.Form.Item("cbeTypDurins"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcnInsur_Time"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcnPerNunMi"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcnCapitalCalc"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcdexpirdat"), eFunctions.Values.eTypeData.etdDate, True),
                                                               mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble),
                                                               mobjValues.StringToType(.Form.Item("tcnPay_Time"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("tcdDate_pay"), eFunctions.Values.eTypeData.etdDate),
                                                               mobjValues.StringToType(.Form.Item("tcnrentamount"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(.Form.Item("cbocurrrent"), eFunctions.Values.eTypeData.etdDouble, True),
                                                               mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            '**+ VI7001 - Life Assurance - Unit Linked
            '+ VI7001 - Interés asegurable Unit Linked
            '+ VI7001 - Interes Asegurable - Unit Linked
            Case "VI7001"
                With Request
                    insvalSequence = mobjPolicySeq.InsValVI7001("VI7001", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbovalgroup"), .Form.Item("cbovalsituation"), .Form.Item("tctIduraind"), .Form.Item("tctPduraind"), mobjValues.StringToType(.Form.Item("tcnInsurTimeAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInsurTimeAgeLimit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeReinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeLimit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapitalCalc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSaving_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_save_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_unit_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIndex_table"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntwarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPay_Time"), eFunctions.Values.eTypeData.etdDouble, True))
                End With
            '+ VI7010 - Información general VUL
            Case "VI7010"
                With Request
                    insvalSequence = mobjPolicySeq.InsValVI7010(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("tctFirstname"), .Form.Item("tctLastname"), .Form.Item("tctLastname2"), mobjValues.StringToType(.Form.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tctAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeSex"), mobjValues.StringToType(.Form.Item("cbeOccupat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSmoking"), .Form.Item("cbeTyperisk"), mobjValues.StringToType(.Form.Item("cbeCivilsta"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenCurrency"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ VI7011 - Información general VUL
            Case "VI7011"
                mobjPolicySeq = New ePolicy.Cover
                With Request
                    insvalSequence = mobjPolicySeq.InsValCA014(.Form.Item("hddsKey"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrencDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), "CA014", mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hddnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), Session("sSche_code"))
                End With
            '+ AU001: Información del vehículo
            Case "AU001"
                With Request
                    insvalSequence = mobjPolicySeq.insValAU001("AU001", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRegister"), .Form.Item("tctDigit"), .Form.Item("valVehcode"), mobjValues.StringToType(.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeLicense_ty"), .Form.Item("tctMotor"), .Form.Item("tctChassis"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCollectedPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdInteger))
                    Session("valGroup") = mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble)
                End With

            '+ CA041: Selección de monedas
            Case "CA041"
                With Request
                    Dim nLength As Integer = 0

                    If Not IsNothing(.Form.Item("Sel")) Then
                        nLength = mobjValues.StringToType(CStr(.Form.Item("Sel").Length), eFunctions.Values.eTypeData.etdInteger)
                    End If

                    insvalSequence = mobjPolicySeq.insValCA041("CA041", .Form.Item("hddExist"), nLength, .Form.Item("hddExchange"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddCurrency"), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                End With

            '+ CA639: Condición de capitales
            Case "CA639"
                mobjPolicySeq = New ePolicy.Cond_cover
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA639(.QueryString("Action"), "CA639", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTipcap"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnID"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthI"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthE"), eFunctions.Values.eTypeData.etdDouble))
                        '+ Variable de control nCharge para realizar para volver al grupo actual que se esta agregando condicion  de capitales
                        mstrQueryString = "&cbeGroup=" & Request.Form.Item("cbeGroup") & "&cbeModulec=" & Request.Form.Item("cbeModulec") & "&nCharge=1"
                    Else
                        If mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                            sCopiar = "1" 'VERDADERO
                        End If

                        insvalSequence = mobjPolicySeq.insValCA639Upd("CA639", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), sCopiar)
                    End If
                End With
                mobjPolicySeq = Nothing
            '+ Ventana de fin de proceso
            Case "GE101"
                insvalSequence = vbNullString

            Case "CA047"
                With Request
                    insvalSequence = mobjPolicySeq.insValCA047("CA047", mobjValues.StringToType(.Form.Item("tcdStayDate"), eFunctions.Values.eTypeData.etdDate))
                End With

            '**+ VI006:	Investments Funds.
            '+ VI006: Fondos de inversiones.

            Case "VI006"


                lclsFunds_Pol = New ePolicy.Funds_Pol

                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("chkActivFound"), eFunctions.Values.eTypeData.etdBoolean) Then
                            sActivefound = "1"
                        Else
                            sActivefound = "2"
                        End If

                        insvalSequence = vbNullString

                        insvalSequence = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), .Form.Item("Sel"), "Popup", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivefound, mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")
                        lclsPolicy_Win = Nothing
                    Else

                        insvalSequence = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), .Form.Item("Sel"), , , , , Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString)
                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")
                        lclsPolicy_Win = Nothing

                    End If

                    lclsFunds_Pol = Nothing
                End With

            '+ VI732: Cuadro de valores garantizados 
            Case "VI732"
                mobjPolicySeq = New ePolicy.Guarant_val
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insvalVI732("VI732", Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnResc_val"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

            Case "VI769"
                lclsDecla_benef = New ePolicy.Decla_benef
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = lclsDecla_benef.InsValVI769("VI769", .QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNumdecla"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDatedecla"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkIrrevoc"), .Form.Item("hddIrrevoc_old"))
                    End If
                End With
                lclsDecla_benef = Nothing

            '+ CA748: Observaciones de una propuesta
            Case "CA748"
                mobjPolicySeq = New ePolicy.obs_proposal
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insvalCA748(mobjValues.StringToType(.Form.Item("cbeObservation"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

            '+ VA589: Datos particulares de Vida activa
            Case "VA589"
                mobjPolicySeq = New ePolicy.Activelife
                With Request
                    insvalSequence = mobjPolicySeq.insValVA589(.QueryString("sCodispl"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeIduraind"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInsurtime"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitaldeath"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCalPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypeinvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntproject"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWarminint"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremMin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAgreement_sLevelint"))
                End With

            '+ VI701: Datos particulares de VidActiva
            Case "VI701"
                mobjPolicySeq = New ePolicy.Life
                With Request
                    insvalSequence = mobjPolicySeq.insValVI701("VI701", Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount_cre"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount_act"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurren_cre"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCalcapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTyppremium"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQ_Quot"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcdEnd_Cre"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctCreditnum"))

                End With

            '+ CA658: Nómina de cotización (vida colectivo)
            Case "CA658"
                mobjPolicySeq = New ePolicy.Client_tmp
                With Request
                    insvalSequence = mobjPolicySeq.insvalCA658(Request.QueryString.Item("WindowType"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdBirthDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRentAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("OptAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddExistGroups"), .Form.Item("optType"))
                End With

            '+ VI811: Asegurados por coberturas (Pólizas innominadas)
            Case "VI811"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Nopayroll
                        insvalSequence = mobjPolicySeq.insvalVI811Upd(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nGroups"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQLifes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        mobjPolicySeq = New ePolicy.Nopayroll
                        insvalSequence = mobjPolicySeq.insvalVI811(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModule"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

            '+ VI662: Datos particulares vida colectivo (Educacional)
            Case "VI662"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.life_levels
                        If .QueryString.Item("sInBasUni") = "1" Then
                            insvalSequence = mobjPolicySeq.InsValvi662(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sInBasUni"), mobjValues.StringToType(.Form.Item("tcnLevel_b"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital_b"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsured_b"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient_b"), Session("nUsercode"), .QueryString("Action"))
                        Else
                            insvalSequence = mobjPolicySeq.InsValvi662(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sInBasUni"), mobjValues.StringToType(.Form.Item("tcnLevel_u"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital_u"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsured_u"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient_b"), Session("nUsercode"), .QueryString("Action"))
                        End If
                    Else
                        '+ Para la parte puntual de la pagina
                        mobjPolicySeq = New ePolicy.life_educ
                        insvalSequence = mobjPolicySeq.InsValvi662(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optTyp"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSituation"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ AM002: Tarifas de atención médica
            Case "AM002"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValAM002Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeInit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroupComp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGroupDed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insvalSequence = mobjPolicySeq.insValAM002(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), Session("sDefaulti"), mobjValues.StringToType(.Form.Item("cbeTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeWait_type"), mobjValues.StringToType(.Form.Item("tcnWait_quan"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            '+ AM003: Conceptos de pago por coberturas de atención médica
            Case "AM003"
                With Request
                    mobjPolicySeq1 = New ePolicy.ValPolicySeq
                    insvalSequence = vbNullString
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq1.insValAM003Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), .QueryString.Item("sIllness"), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrestac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Percen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Quanti"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimit_exe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLimitH"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIndem_Rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTyplim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsCaren_Type"), mobjValues.StringToType(.Form.Item("tcnCaren_Dur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Quanti_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIndem_Rate_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLimit_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTyplim_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCount_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLimit_Exe_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPunish_2"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chksOtherLim"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        insvalSequence = mobjPolicySeq1.insValAM003(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    mobjPolicySeq1 = Nothing
                End With

            '+ AM006: Exclusión de enfermedades
            Case "AM006"
                With Request
                    mobjPolicySeq = New ePolicy.Tab_am_exc
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        Dim lstrTypeExclu
                        lstrTypeExclu = .Form.Item("chkExclud")
                        If lstrTypeExclu = vbNullString Then
                            lstrTypeExclu = "2"
                        End If

                        If CStr(Session("sPolitype")) = "2" And CStr(Session("nCertif")) = "0" Then
                            llngTariff = "0"
                        Else
                            If .QueryString.Item("nTariff") = "0" Then
                                llngTariff = 0
                            Else
                                llngTariff = .QueryString.Item("nTariff")
                            End If
                        End If
                        insvalSequence = mobjPolicySeq.InsValAM006Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(llngTariff, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddsClient"), .Form.Item("cbeIllness"), mobjValues.StringToType(.Form.Item("cbeExc_Code"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), Session("dStartdate"), Session("dExpirdat"), lstrTypeExclu, .Form.Item("hddOptType_exc"))
                    Else
                        insvalSequence = mobjPolicySeq.InsValAM006(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ VI666: Cotización
            Case "VI666"
                mobjPolicySeq = New ePolicy.Cover_quota
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insvalVI666(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnUtilMar"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddUtilMarOrig"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnPremiumOrig"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("WindowType"))
                        mstrQueryString = "&nGroup=" & .QueryString.Item("nGroup")
                    Else
                        insvalSequence = mobjPolicySeq.insvalVI666(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .QueryString("WindowType"))
                        mstrQueryString = "&nGroup=" & .Form.Item("valGroup")
                    End If
                End With


            '+ VA595: Ilustración del valor póliz
            Case "VA595"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Per_deposit
                        insvalSequence = mobjPolicySeq.InsValVA595Upd("VA595", .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        mobjPolicySeq = New ePolicy.Projectlife
                        insvalSequence = mobjPolicySeq.InsValVA595("VA595", Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremAnu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremdep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPrsugest"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"))
                    End If
                End With

            '+ RV778: Datos particulares de rentas vitalicias
            Case "RV778"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        mobjPolicySeq = New ePolicy.Annuities
                        insvalSequence = mobjPolicySeq.insValRV778(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnPremiumbas"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        mobjPolicySeq = New ePolicy.Prem_annuities
                        insvalSequence = mobjPolicySeq.InsValRV778Upd(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeIndrecdep"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPrem_quot"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate_disc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNom_valbon"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIssuedatbon"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdatbon"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

            '+ CA048: Fin de proceso
            Case "CA048"
                insvalSequence = mobjPolicySeq.insValCA048("CA048", Request.Form.Item("chkPendenStat"), mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), Session("nTransaction"))

            Case "CA050"

                insvalSequence = mobjPolicySeq.insValCA050("CA050", Request.Form.Item("chkDetailedEntryPrinted"), mobjValues.StringToType(Request.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            '+ CA072: Cuadro de rescate de poliza
            Case "CA072"
                insvalSequence = String.Empty
            '+ CA072: Estimación de contrato
            Case "CA073"
                mobjPolicySeq = New ePolicy.Reconocimiento_ingresos
                insvalSequence = mobjPolicySeq.InsValCA073(mobjValues.StringToType(Request.Form.Item("tcnPrimNetaDC"), eFunctions.Values.eTypeData.etdDouble))
            '+ OS001: Solicitud de ordenes de servicio
            Case "OS001", "OS001_K"
                mobjPolicySeq = New eClaim.Prof_ord
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mstrQueryString = "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPolicy=" & Request.Form.Item("nPolicy") & "&nProponum=" & Request.Form.Item("nProponum") & "&nCertif=" & Request.Form.Item("nCertif") & "&nClaim=" & Request.Form.Item("nClaim") & "&nCase_num=" & Request.Form.Item("nCase_num")
                        insvalSequence = mobjPolicySeq.insValOS001(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("hddnOrdClass"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFec_prog"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctTime_prog"), .Form.Item("tctPlace"), mobjValues.StringToType(.Form.Item("cbeWorksh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMunicipality"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctName_cont"), .Form.Item("tctAdd_contact"), .Form.Item("tctPhone_cont"), mobjValues.StringToType(.Form.Item("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrd_TypeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.Form.Item("tcdMade_date"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctMade_time"))
                    End If
                End With

            '**+ CA829: Resumen de coberturas
            '+ CA829: Resumen de coberturas.

            Case "CA829"
                insvalSequence = vbNullString

            '+ CA830: Certificado de cobertura
            Case "CA830"
                insvalSequence = mobjPolicySeq.insValCA830("CA830", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCoverageCertificate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("ValIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

            '**+ VI7003: Savings Plan.
            '+ VI7003: Plan de Ahorros.
            Case "VI7003"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Per_deposit
                        insvalSequence = mobjPolicySeq.InsValVI7003Upd("VI7003", .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '**+ VI7005: Transference information.
            '+ VI7005: Información de transferencia.

            Case "VI7005"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.APV_Transfer

                        insvalSequence = mobjPolicySeq.InsValVI7005Upd("VI7005", .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_peso"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_Profit"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ VI849: Criterios de selección de riesgo (Asegurado).
            Case "VI849"
                insvalSequence = vbNullString
            '+ CA851: Vía de pago
            Case "CA851"
                mobjPolicySeq = New ePolicy.ValPolicySeq
                With Request
                    'insvalSequence= VbNullstring
                    insvalSequence = mobjPolicySeq.insValCA851(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAFPCommi"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optDirTyp"), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True))
                End With
            '+ CA960: Límite por prestaciones.
            Case "CA960"
                mobjPolicySeq = New ePolicy.Franchise
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        mstrQueryString = "&nGroup=" & Request.QueryString.Item("nGroup")
                        insvalSequence = mobjPolicySeq.insValCA960(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcnFixAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcsFrancApl"), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSeq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDed_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLevel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True))

                    End With
                End If
                mobjPolicySeq = Nothing

            '+ CA100: convenios asociados a prestaciones
            Case "CA100"
                mobjPolicySeq = New eProduct.Lend_Agree_Pres
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        insvalSequence = mobjPolicySeq.insValCA100(Request.QueryString.Item("Action"), Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCod_Agree"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdLong))

                    End With
                End If

                mobjPolicySeq = Nothing
            '+ CA659: Secciones reporte automático para póliza
            Case "CA659"
                insvalSequence = ""

            '+ VI006A: Fondos de inversiones por póliza matríz.
            Case "VI006A"


                'UPGRADE_NOTE: The 'ePolicy.Funds_CO_P' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lclsFunds_CO_P = Server.CreateObject("ePolicy.Funds_CO_P")

                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("chkActivFound"), eFunctions.Values.eTypeData.etdBoolean) Then
                            'If CBool(.Form.Item("chkActivFound")) Then
                            sActivefound_P = "1"
                        Else
                            sActivefound_P = "2"
                        End If

                        insvalSequence = vbNullString

                        insvalSequence = lclsFunds_CO_P.insValVI006A(.QueryString("sCodispl"), .Form.Item("Sel"), "Popup", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivefound_P, mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))


                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
                        lclsPolicy_Win = Nothing

                    Else

                        insvalSequence = lclsFunds_CO_P.insValVI006A(.QueryString("sCodispl"), .Form.Item("Sel"), , , , , Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString)

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
                        lclsPolicy_Win = Nothing
                    End If

                End With
                lclsFunds_CO_P = Nothing

            '+ VI8001: Prima base
            Case "VI8001"
                insvalSequence = ""
            Case "VI8002"
                mobjPolicySeq = New ePolicy.Apv_origin
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If .Form.Item("hddoptDirecta") = "1" Then
                            optDirect = 1
                        Else
                            optDirect = 0
                        End If
                        insvalSequence = mobjPolicySeq.insValVI8002Upd("VI8002", Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremDeal_anu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPayfreq"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nWaypay"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate"), optDirect)
                    Else
                        insvalSequence = mobjPolicySeq.InsValVI8002("VI8002", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valTyp_ProfitWorker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddYearMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), .Form.Item("tctFolio"), mobjValues.StringToType(.Form.Item("chkDepend"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("chkIndep"), eFunctions.Values.eTypeData.etdLong), Session("nTransaction"), mobjValues.StringToType(.Form.Item("cbeAFP"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate_origi"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("optDirecta"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("optDirectb"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With


            '+ AP004: Clasificación de riesgos AP
            Case "AP004"
                mobjPolicySeq = New ePolicy.Class_ap
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValAP004Upd("AP004", Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate"))
                    End If
                End With
            Case "CT001"
                mobjPolicySeq = New ePolicy.Credit
                With Request
                    insvalSequence = mobjPolicySeq.insValCT001("CT001", mobjValues.StringToType(.Form.Item("tcnLimitRequest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimitCurrent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMateria"), eFunctions.Values.eTypeData.etdLong), Session("sPolitype"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeClassClient"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeAjustType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimitNoPayroll"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "CT002"
                mobjPolicySeq = New ePolicy.CreditSales
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCT002("CT002", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdDocdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeDocType"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctNumDoc"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("Notas"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2")
                    Else
                        insvalSequence = mobjPolicySeq.insValCT002("CT002", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, vbNullString, 0, 0, 0, 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "1")
                    End If
                End With

            Case "WT001"
                mobjPolicySeq = New ePolicy.Warranty
                With Request
                    insvalSequence = mobjPolicySeq.insValWT001("WT001", Session("sPolitype"), Session("nCertif"), .Form.Item("tctProjectName"), .Form.Item("tctIndentify"))
                End With

            Case "WT002"
                mobjPolicySeq = New ePolicy.WarrantyQuotes
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValWT002("WT002", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQuote"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), 0, 0, .Form.Item("tctComment"), "2")
                    Else
                        insvalSequence = mobjPolicySeq.insValWT002("WT002", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nQuote"), eFunctions.Values.eTypeData.etdDouble), Today, 0, mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), vbNullString, "1")
                    End If
                End With

            '*+RO001: Particular information on Theft
            '+RO001: Datos Particulares de Robo
            Case "RO001"
                With Request
                    mobjPolicySeq = New ePolicy.Theft
                    insvalSequence = mobjPolicySeq.InsValRO001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnEmployees"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnVigilance"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeBusinessTy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCommerGrp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctDescBussi"), mobjValues.StringToType(.Form.Item("valConstCat"), eFunctions.Values.eTypeData.etdInteger))

                End With

            '*+TR001: particular information on transport
            '+TR001: Información particular de Transporte
            Case "TR001"
                With Request
                    mobjPolicySeq = New ePolicy.transport
                    insvalSequence = mobjPolicySeq.InsValTR001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnMaxLimTrip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDep_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenDecla_freq"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnEstAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOverLine"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModalitySumins"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnDep_prem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_Apply"), eFunctions.Values.eTypeData.etdDouble), Session("sSche_code"))

                End With

            '*+TR002: Covered routes
            '+TR002: Rutas aseguradas
            Case "TR002"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.tran_route
                        insvalSequence = mobjPolicySeq.InsValTR002(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnRoute"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeTypRoute"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeTranspType"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            '*+TR003: Shipped mechandise
            '+TR003: Mercancías transportadas
            Case "TR003"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Tran_merch

                        insvalSequence = mobjPolicySeq.InsValTR003(.QueryString("sCodispl"), _
                                                                    mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                   .QueryString("Action"), _
                                                                   Session("sCertype"), _
                                                                   Session("nBranch"), _
                                                                   Session("nProduct"), _
                                                                   Session("nPolicy"), _
                                                                   Session("nCertif"), _
                                                                   Session("dEffecdate"), _
                                                                   mobjValues.StringToType(.Form.Item("cbeClassMerch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                   mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger), _
                                                                   .Form.Item("tctDescript"), _
                                                                   mobjValues.StringToType(.Form.Item("tcnQuanTrans"), eFunctions.Values.eTypeData.etdInteger), _
                                                                   mobjValues.StringToType(.Form.Item("cbeUnit"), eFunctions.Values.eTypeData.etdInteger), _
                                                                   mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   .Form.Item("cbeFranDedi"), _
                                                                   mobjValues.StringToType(.Form.Item("tcnFranDedRate"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   .Form.Item("tcnMinAmount"), _
                                                                   mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            '+TR004: Transportation Modes
            '+TR004: Medios de transporte
            Case "TR004"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        mobjPolicySeq = New ePolicy.tran_way

                        insvalSequence = mobjPolicySeq.InsValTR004(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnWay"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctName_licen"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong))
                    End If
                End With
            Case "TR6000"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        mobjPolicySeq = New ePolicy.Tran_rate

                        insvalSequence = mobjPolicySeq.InsValTR6000Upd(.QueryString("sCodispl"), _
                                                                       mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                       .QueryString("Action"), _
                                                                       Session("sCertype"), _
                                                                       Session("nBranch"), _
                                                                       Session("nProduct"), _
                                                                       Session("nPolicy"), _
                                                                       Session("nCertif"), _
                                                                       Session("dEffecdate"), _
                                                                       mobjValues.StringToType(.Form.Item("cbeClassmerch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                       mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger), _
                                                                       mobjValues.StringToType(.QueryString.Item("tcnLimitCapital"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnAmo_deduc"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), _
                                                                       .Form.Item("cbeType"))
                    Else
                        mobjPolicySeq = New ePolicy.Tran_rate
                        insvalSequence = mobjPolicySeq.InsValTR6000(.QueryString("sCodispl"), _
                                                                    mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                    Session("sCertype"), _
                                                                    Session("nBranch"), _
                                                                    Session("nProduct"), _
                                                                    Session("nPolicy"), _
                                                                    Session("nCertif"), _
                                                                    Session("dEffecdate"), _
                                                                    mobjValues.StringToType(.Form.Item("tcnLimitCapital"), eFunctions.Values.eTypeData.etdDouble))

                    End If
                End With
            '*+TR009: Itinerario de transporte
            '+TR009: Transport Itineraries
            Case "TR009"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If CDbl(.QueryString.Item("nInd")) = 1 Then
                            mobjPolicySeq = New ePolicy.Tran_stage
                            insvalSequence = mobjPolicySeq.InsValTR009_Itin(.QueryString("Action"), Session("sPolitype"), .QueryString("sCodispl"), .QueryString("nCurrency"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnStage"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDestindat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdOrigindat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valRoute"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valTransport"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctName"), .Form.Item("tctOrigen"), .Form.Item("tctDestiny"))
                        Else
                            mobjPolicySeq = New ePolicy.Tran_stagedet
                            insvalSequence = mobjPolicySeq.InsValTR009_Merch(.QueryString("Action"), .QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnMerchandise"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valClass"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valPacking"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMerchrate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCostUnit"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    Else
                        mobjPolicySeq = New ePolicy.Tran_stage
                        insvalSequence = mobjPolicySeq.InsValTR009(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"))
                    End If
                End With

            '+ SH001: Datos Particulares de Marítimo Cascos
            Case "SH001"
                mobjPolicySeq = New ePolicy.Ship
                With Request
                    insvalSequence = mobjPolicySeq.insValSH001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeShipUse"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctName"), .Form.Item("tctRegist"), mobjValues.StringToType(.Form.Item("valMaterial"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("cbeShipType"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctConstructor"), mobjValues.StringToType(.Form.Item("tcnConsYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnEquivYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdLastCareDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctLastCarePlace"), mobjValues.StringToType(.Form.Item("tcnDepth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLength"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWaters"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumMotors"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctModelMotors"), .Form.Item("tctSerialMotors"), mobjValues.StringToType(.Form.Item("tcnPower"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTRB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTRN"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapacity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeUnitMesureCode"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctSeaPort"), .Form.Item("tctDotation"), .Form.Item("tctActionZone"))
                End With

            '+HO001: Datos Particulares de Hogar

            Case "HO001"
                With Request
                    mobjPolicySeq = New ePolicy.HomeOwner
                    insvalSequence = mobjPolicySeq.InsValHO001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeDwellingType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeOwnerShip"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnYear_built"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkCov_purc"), mobjValues.StringToType(.Form.Item("tcnPrice_purch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency_purch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdDate_purch"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkPolicy_other"), mobjValues.StringToType(.Form.Item("tcnCap_other"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency_other"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdExpir_other"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeExterConstr"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctOther_constr"), mobjValues.StringToType(.Form.Item("tcnStories"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeRoofType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRoofYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnHomeSuper"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnLandSuper"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnGarage"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnFirePlace"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnBedrooms"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnFullBath"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnHalfBath"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeAirType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeAlt_heating"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkGas"), .Form.Item("chkSprinkSys"), .Form.Item("tctAlarm_comp"), mobjValues.StringToType(.Form.Item("tcnDist_Hydr"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkNon_smok"), mobjValues.StringToType(.Form.Item("tcnDist_fire"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctFireDepart"), mobjValues.StringToType(.Form.Item("cbeFloodZone"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeSeismicZone"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkFloodInd"), mobjValues.StringToType(.Form.Item("cbeSwimPool"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkFencePool"), mobjValues.StringToType(.Form.Item("tcnFenceHeight"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkTrampoline"), .Form.Item("chkAnimalsInd"), .Form.Item("tctAnimalsDes"), .Form.Item("chkAttackedInd"), mobjValues.StringToType(.Form.Item("cbeFoundType"), eFunctions.Values.eTypeData.etdInteger))

                End With

            '+ RM001: Datos Particulares de Rotura de Maquinaria
            Case "RM001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If Trim(.Form.Item("tcnFabYear")) = "" Then
                            liFabYear = 0
                        Else
                            liFabYear = .Form.Item("tcnFabYear")
                        End If
                        mobjPolicySeq = New ePolicy.Detail_Machine
                        insvalSequence = mobjPolicySeq.insValDetail_Machine(.QueryString("sCodispl"), .QueryString("nMainAction"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valMachineCode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(liFabYear, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnQuantityMachine"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        mobjPolicySeq = New ePolicy.Machine
                        insvalSequence = mobjPolicySeq.InsValRM001(.QueryString("sCodispl"), .QueryString("nMainAction"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+RC001: Información particular de Resp. Civil
            Case "RC001"
                With Request
                    mobjPolicySeq = New ePolicy.Civil
                    insvalSequence = mobjPolicySeq.InsValRC001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeUnit_type"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnUnit_quan"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeBusinessTy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCommerGrp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctDescBussi"), mobjValues.StringToType(.Form.Item("valConstCat"), eFunctions.Values.eTypeData.etdInteger))

                End With
            '+ VI7500: SAAPV
            Case "VI7500"

                mobjsAapv = New eSaapv.Saapv

                insvalSequence = mobjsAapv.insvalvi7500(CStr(Session("sCertype")), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddInstitution"), eFunctions.Values.eTypeData.etdLong))
                mobjsAapv = Nothing

            '+CC001: Crédito y Caución
            Case "CC001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Warranty
                        insvalSequence = mobjPolicySeq.InsValCC001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valtypewarranty"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctdocwarranty"), mobjValues.StringToType(.Form.Item("valcurrency_wrr"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcncapacity"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdMaturity"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        mobjPolicySeq = New ePolicy.Credit
                        insvalSequence = mobjPolicySeq.InsValCC001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valinsmodality"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnguar_type"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctcontracnum"), mobjValues.StringToType(.Form.Item("tcdcontracdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valtime_unit"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdterm_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcntime_eject"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcncredcau"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valStatusbond"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnindemper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnmoraallow"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcntransmon1"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcntransmon2"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnindper1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnindper2"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            Case "GI1408"
                insvalSequence = ""

            '+ CA635: Condicines de prima
            Case "CA635"
                mobjPolicySeq = New ePolicy.Cond_cover_premium
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjPolicySeq.insValCA635(.QueryString("Action"), "CA635", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"),
                                                                   mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   Session("nCertif"),
                                                                   mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                   mobjValues.StringToType(.Form.Item("cbeTipPrem"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("tcnCapital_min"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("tcnCapital_max"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                   .Form.Item("cbeRoutine"),
                                                                   mobjValues.StringToType(.Form.Item("valId_table"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                   mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble, True))
                        '+ Variable de control nCharge para realizar para volver al grupo actual que se esta agregando condicion  de capitales
                        mstrQueryString = "&cbeGroup=" & Request.Form.Item("cbeGroup") &
                                          "&cbeModulec=" & Request.Form.Item("cbeModulec") &
                                          "&nCharge=1"
                    Else
                        If mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                            sCopiar = "1" 'VERDADERO
                        End If

                        insvalSequence = mobjPolicySeq.insValCA635Upd("CA635",
                                                                      Session("sCertype"),
                                                                      Session("nBranch"),
                                                                      Session("nProduct"),
                                                                      Session("nPolicy"),
                                                                      mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                      Session("nCertif"),
                                                                      mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                      sCopiar)
                    End If
                End With
                mobjPolicySeq = Nothing
            Case "MU700"
                mobjPolicySeq = New ValPolicySeq_MU700
                With Request
                    If Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipElect" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_EquipElect("MU700",
                                                                 mobjValues.StringToType(.Form.Item("NTYPE_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 mobjValues.StringToType(.Form.Item("NSECTION_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 .Form.Item("SDESCRIPTION_EquipElect"),
                                                                 mobjValues.StringToType(.Form.Item("NCAPITAL_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 mobjValues.StringToType(.Form.Item("NRATE_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 mobjValues.StringToType(.Form.Item("NPREMIUM_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "RotMaqui" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_RotMaqui("MU700",
                                                                               .Form.Item("STRADEMARK_RotMaqui"),
                                                                               .Form.Item("SMODEL_RotMaqui"),
                                                                                mobjValues.StringToType(.Form.Item("NYEAR_RotMaqui"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                               .Form.Item("SORIGIN_RotMaqui"),
                                                                               .Form.Item("SSERIALNUMBER_RotMaqui"),
                                                                                mobjValues.StringToType(.Form.Item("NCAPITAL_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                mobjValues.StringToType(.Form.Item("NRATE_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                mobjValues.StringToType(.Form.Item("NPREMIUM_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipMaquiContr" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_EquipMaquiContr("MU700",
                                                                                       .Form.Item("STRADEMARK_EquipMaquiContr"),
                                                                                       .Form.Item("SMODEL_EquipMaquiContr"),
                                                                                        mobjValues.StringToType(.Form.Item("NYEAR_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                                       .Form.Item("SORIGIN_EquipMaquiContr"),
                                                                                       .Form.Item("SSERIALNUMBER_EquipMaquiContr"),
                                                                                        mobjValues.StringToType(.Form.Item("NCAPITAL_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                        mobjValues.StringToType(.Form.Item("NRATE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                        mobjValues.StringToType(.Form.Item("NPREMIUM_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "Fidelity" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_Fidelity("MU700",
                                                                               .Form.Item("SCLIENT_Fidelity"),
                                                                               .Form.Item("SCLIENT_Fidelity_Digit"),
                                                                               .Form.Item("SFIRSTNAME_Fidelity"),
                                                                               .Form.Item("SMIDDLENAME_Fidelity"),
                                                                               .Form.Item("SLASTNAME_Fidelity"),
                                                                               .Form.Item("SLASTNAME2_Fidelity"),
                                                                               mobjValues.StringToType(.Form.Item("NPOSITION_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                               mobjValues.StringToType(.Form.Item("NSALARY_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               mobjValues.StringToType(.Form.Item("NFACTOR_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               mobjValues.StringToType(.Form.Item("NVALUE_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               Request.QueryString("nFI_POLICYTYPE"))


                    ElseIf Request.QueryString("WindowType") <> "PopUp" Then

                        Dim mobjCertificat = New ePolicy.Certificat
                        If mobjCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
                            insvalSequence = mobjPolicySeq.insValMU700(sCertype:=Session("sCerType"),
                                                                        nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                        nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                        dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                        nCapital:=mobjCertificat.nCapital,
                                                                        dExpirDat:=mobjCertificat.dExpirdat,
                                                                        dIssueDat:=mobjCertificat.dIssuedat,
                                                                        nNullCode:=0,
                                                                        dNullDate:=Date.MinValue,
                                                                        nPremium:=mobjCertificat.nPremium,
                                                                        dStartDate:=mobjCertificat.dStartdate,
                                                                        nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nTransactio:=mobjCertificat.nTransactio,
                                                                        nSituation:=mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nGroup:=mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        sClient:=mobjCertificat.sClient,
                                                                        nConstCat:=mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nCodKind:=mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nPayFreq:=mobjCertificat.nPayfreq,
                                                                        nSismicZone:=mobjValues.StringToType(.Form.Item("cbeSismicZone"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nFi_PolicyType:=mobjValues.StringToType(.Form.Item("tcnFi_PolicyType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nInsurType:=mobjValues.StringToType(.Form.Item("tcnInsurType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nNumberOfEmployees:=mobjValues.StringToType(.Form.Item("tcnNumberOfEmployees"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nInsured:=mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        nTheftCapital:=mobjValues.StringToType(.Form.Item("tcnTheftCapital"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        nSecurityMen:=mobjValues.StringToType(.Form.Item("tcnSecurityMen"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nArea:=mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        sInd_Fidelity:=.Form.Item("sInd_Fidelity"),
                                                                        sInd_Electronic:=.Form.Item("sInd_Electronic"),
                                                                        sInd_Machine:=.Form.Item("sInd_Machine"),
                                                                        sInd_Contractor:=.Form.Item("sInd_Contractor"),
                                                                        sRequieredSections:=.Form.Item("sRequieredSections"),
                                                                        NMONEY_TRANSIT:=mobjValues.StringToType(.Form.Item("tcnMoney_Transit"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        NMONEY_PERMANENCE:=mobjValues.StringToType(.Form.Item("tcnMoney_Permanence"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If

                    End If

                End With
            Case "AV001"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    insvalSequence = mobjPolicyseqAviat_Marit.insValAV001_SH010("AV001", mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), .Form.Item("tcnYear"), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), mobjValues.StringToType(.Form.Item("tcnTakeoff_maxwei"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctGeographical"), mobjValues.StringToType(.Form.Item("cbeUse"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSeatnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCrewnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPassengersnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNibranumber"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            Case "SH010"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    insvalSequence = mobjPolicyseqAviat_Marit.insValAV001_SH010("SH010", mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), .Form.Item("tctYear"), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), , , , , , , , .Form.Item("tctName"), .Form.Item("tctSeries"), .Form.Item("tctOrigin"), .Form.Item("tctNavigationcertificate"), mobjValues.StringToType(.Form.Item("tcnQualificationship"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPortdeparture"), .Form.Item("tctPortarrival"), .Form.Item("tctDimensions"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            Case "CM001"
                mobjPolicySeq = New ePolicy.TRCM
                With Request
                    insvalSequence = mobjPolicySeq.insValCM001("CM001", .Form.Item("tctWorkname"), mobjValues.StringToType(.Form.Item("cbeTypeWork"), eFunctions.Values.eTypeData.etdInteger),
                                                                 mobjValues.StringToType(.Form.Item("dInitialdate_work"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("dEnddate_work"), eFunctions.Values.eTypeData.etdDate))
                End With
                mobjPolicySeq = Nothing
                mobjPolicySeq = Nothing
            Case "CA069"
                With Request
                    insvalSequence = ""
                End With
            Case "MU700"
                mobjPolicySeq = New ValPolicySeq_MU700
                With Request
                    If Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipElect" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_EquipElect("MU700",
                                                                 mobjValues.StringToType(.Form.Item("NTYPE_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 mobjValues.StringToType(.Form.Item("NSECTION_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 .Form.Item("SDESCRIPTION_EquipElect"),
                                                                 mobjValues.StringToType(.Form.Item("NCAPITAL_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 mobjValues.StringToType(.Form.Item("NRATE_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 mobjValues.StringToType(.Form.Item("NPREMIUM_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "RotMaqui" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_RotMaqui("MU700",
                                                                               .Form.Item("STRADEMARK_RotMaqui"),
                                                                               .Form.Item("SMODEL_RotMaqui"),
                                                                                mobjValues.StringToType(.Form.Item("NYEAR_RotMaqui"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                               .Form.Item("SORIGIN_RotMaqui"),
                                                                               .Form.Item("SSERIALNUMBER_RotMaqui"),
                                                                                mobjValues.StringToType(.Form.Item("NCAPITAL_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                mobjValues.StringToType(.Form.Item("NRATE_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                mobjValues.StringToType(.Form.Item("NPREMIUM_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipMaquiContr" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_EquipMaquiContr("MU700",
                                                                                       .Form.Item("STRADEMARK_EquipMaquiContr"),
                                                                                       .Form.Item("SMODEL_EquipMaquiContr"),
                                                                                        mobjValues.StringToType(.Form.Item("NYEAR_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                                       .Form.Item("SORIGIN_EquipMaquiContr"),
                                                                                       .Form.Item("SSERIALNUMBER_EquipMaquiContr"),
                                                                                        mobjValues.StringToType(.Form.Item("NCAPITAL_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                        mobjValues.StringToType(.Form.Item("NRATE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                                        mobjValues.StringToType(.Form.Item("NPREMIUM_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True))


                    ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "Fidelity" Then
                        insvalSequence = mobjPolicySeq.insValMU700Upd_Fidelity("MU700",
                                                                               .Form.Item("SCLIENT_Fidelity"),
                                                                               .Form.Item("SCLIENT_Fidelity_Digit"),
                                                                               .Form.Item("SFIRSTNAME_Fidelity"),
                                                                               .Form.Item("SMIDDLENAME_Fidelity"),
                                                                               .Form.Item("SLASTNAME_Fidelity"),
                                                                               .Form.Item("SLASTNAME2_Fidelity"),
                                                                               mobjValues.StringToType(.Form.Item("NPOSITION_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                               mobjValues.StringToType(.Form.Item("NSALARY_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               mobjValues.StringToType(.Form.Item("NFACTOR_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               mobjValues.StringToType(.Form.Item("NVALUE_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                               Request.QueryString("nFI_POLICYTYPE"))


                    ElseIf Request.QueryString("WindowType") <> "PopUp" Then

                        Dim mobjCertificat = New ePolicy.Certificat
                        If mobjCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
                            insvalSequence = mobjPolicySeq.insValMU700(sCertype:=Session("sCerType"),
                                                                        nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                        nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                        dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                        nCapital:=mobjCertificat.nCapital,
                                                                        dExpirDat:=mobjCertificat.dExpirdat,
                                                                        dIssueDat:=mobjCertificat.dIssuedat,
                                                                        nNullCode:=0,
                                                                        dNullDate:=Date.MinValue,
                                                                        nPremium:=mobjCertificat.nPremium,
                                                                        dStartDate:=mobjCertificat.dStartdate,
                                                                        nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                        nTransactio:=mobjCertificat.nTransactio,
                                                                        nSituation:=mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nGroup:=mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        sClient:=mobjCertificat.sClient,
                                                                        nConstCat:=mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nCodKind:=mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nPayFreq:=mobjCertificat.nPayfreq,
                                                                        nSismicZone:=mobjValues.StringToType(.Form.Item("cbeSismicZone"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nFi_PolicyType:=mobjValues.StringToType(.Form.Item("tcnFi_PolicyType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nInsurType:=mobjValues.StringToType(.Form.Item("tcnInsurType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nNumberOfEmployees:=mobjValues.StringToType(.Form.Item("tcnNumberOfEmployees"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nInsured:=mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        nTheftCapital:=mobjValues.StringToType(.Form.Item("tcnTheftCapital"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        nSecurityMen:=mobjValues.StringToType(.Form.Item("tcnSecurityMen"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        nArea:=mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        sInd_Fidelity:=.Form.Item("sInd_Fidelity"),
                                                                        sInd_Electronic:=.Form.Item("sInd_Electronic"),
                                                                        sInd_Machine:=.Form.Item("sInd_Machine"),
                                                                        sInd_Contractor:=.Form.Item("sInd_Contractor"),
                                                                        sRequieredSections:=.Form.Item("sRequieredSections"),
                                                                        NMONEY_TRANSIT:=mobjValues.StringToType(.Form.Item("tcnMoney_Transit"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        NMONEY_PERMANENCE:=mobjValues.StringToType(.Form.Item("tcnMoney_Permanence"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If

                    End If

                End With
            Case "AV001"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    insvalSequence = mobjPolicyseqAviat_Marit.insValAV001_SH010("AV001", mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), .Form.Item("tcnYear"), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), mobjValues.StringToType(.Form.Item("tcnTakeoff_maxwei"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctGeographical"), mobjValues.StringToType(.Form.Item("cbeUse"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSeatnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCrewnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPassengersnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNibranumber"), eFunctions.Values.eTypeData.etdDouble))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            Case "SH010"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    insvalSequence = mobjPolicyseqAviat_Marit.insValAV001_SH010("SH010", mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), .Form.Item("tctYear"), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), , , , , , , , .Form.Item("tctName"), .Form.Item("tctSeries"), .Form.Item("tctOrigin"), .Form.Item("tctNavigationcertificate"), mobjValues.StringToType(.Form.Item("tcnQualificationship"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPortdeparture"), .Form.Item("tctPortarrival"), .Form.Item("tctDimensions"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            '+ AP010: Accidentes Personales.

            'Case "AP010"
            '    mobjPolicySeq = New ePolicy.AccidentPerson
            '    If Request.QueryString.Item("WindowType") = "PopUp" Then
            '        With Request

            '            insvalSequence = mobjPolicySeq.insValAP010UPD(.QueryString("Action"), "AP010", .Form.Item("cbeEmployeeCode"), .Form.Item("tctLastName"), .Form.Item("tctLastName2"), _
            '                                                        .Form.Item("tctFirstName"), .Form.Item("tctMiddleName"), mobjValues.StringToType(.Form.Item("DateBirthdate"), eFunctions.Values.eTypeData.etdDate), _
            '                                                         mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctBeneficiarnotes"), _
            '                                                         mobjValues.StringToType(.Form.Item("DateNulldate"), eFunctions.Values.eTypeData.etdDate))

            '        End With
            '    Else
            '        insvalSequence = mobjPolicySeq.insValAP010("AP010", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
            '                                                     mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))

            '    End If
            Case "CM001"
                mobjPolicySeq = New ePolicy.TRCM
                With Request
                    insvalSequence = mobjPolicySeq.insValCM001("CM001", .Form.Item("tctWorkname"), mobjValues.StringToType(.Form.Item("cbeTypeWork"), eFunctions.Values.eTypeData.etdInteger),
                                                                 mobjValues.StringToType(.Form.Item("dInitialdate_work"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("dEnddate_work"), eFunctions.Values.eTypeData.etdDate))
                End With
                mobjPolicySeq = Nothing
                mobjPolicySeq = Nothing

            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"

        End Select
        'mobjNetFrameWork.FinishProcess "ValSequence|" & Request.QueryString("sCodispl")
    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean

        Dim sActivefound As String
        Dim ldtmAcce As Object
        Dim lintCapital As String
        Dim ldblInterRate As String
        Dim lintInsured As String
        Dim lblnNotPopUp As Object
        Dim lintCountCA012 As Integer
        Dim lstrTyplevels As String
        Dim ldblCommiss As String
        Dim lblnDocExist As Boolean
        Dim llngType As String
        Dim lintGrid As Integer
        Dim llngCompany As Object
        Dim lstrsreturn As String
        Dim lstrsn_infrac As String
        Dim ldblReserRate As String
        Dim lstrCheck As String
        Dim sActivefound_P As String
        Dim ldblPercent As Object
        Dim lintCount As Object
        Dim lintAfp As Object
        Dim lstrIrrevoc As String
        Dim nModulec As Object
        Dim lblnP_data As Boolean
        Dim lclsFunds_CO_P As Object
        Dim lintLevels As String
        Dim lclsRoles As ePolicy.Roles
        Dim lstrConting As String
        Dim lstrDesign As String
        Dim lstrContent As String
        Dim nCover As Object
        Dim lintRole As Integer
        Dim lstrAction As String
        Dim lstrsrelapsing As String
        Dim nCapital_rei As Double
        Dim liFabYear As Object
        '--------------------------------------------------------------------------------------------
        Dim lintIntermedia As Integer
        Dim lintIntermediaOld As Integer
        Dim lstrClient As String
        Dim lstrClientOld As String
        Dim lblnPost As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsErrors As eFunctions.Errors
        Dim lobjDocuments As eReports.Report
        Dim llngTariff As Object

        lblnPost = True


        '    mobjNetFrameWork.BeginProcess "PostSequence|" & Request.QueryString("sCodispl")
        Dim lcolRoles As ePolicy.Roleses
        Dim lclsLife_docu As ePolicy.Life_docu
        Dim lclsRefresh As ePolicy.ValPolicySeq
        Dim lclsReinsuran As ePolicy.Reinsuran
        Dim mobjtRehabilitate As ePolicy.TRehabilitate
        Dim mobjPolicyTra As ePolicy.ValPolicyTra
        Dim mobjCertificat As ePolicy.Certificat
        Dim lclsFunds_Pol As ePolicy.Funds_Pol
        Dim lclsTab_ord_origin As eBranches.Tab_Ord_Origin
        Dim lcolTab_ord_origin As eBranches.Tab_Ord_Origins
        Dim lclsDecla_benef As ePolicy.Decla_benef
        Dim mobjPolicySeq_educ As ePolicy.life_educ
        Dim mobjPolicySeq_lev As ePolicy.life_levels
        Dim mobjsAapv As eSaapv.Saapv
        Dim lclsInsured_expdis As ePolicy.Insured_expdis

        Select Case Request.QueryString.Item("sCodispl")

            '+ VI641: Criterios para seleción de riesgo
            Case "VI641"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.InsPostVI641Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"),
                                                                 Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                                 mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                 mobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdDouble),
                                                                 Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), eRemoteDB.Constants.intNull,
                                                                 .Form.Item("cbeSexinsur"),
                                                                 mobjValues.StringToType(.Form.Item("tcnAgestart"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form.Item("tcnAgeend"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form.Item("tcnCapstart"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form.Item("tcnCapend"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form.Item("cbeCrthecni"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdInteger))

                        mstrQueryString = "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")
                    Else
                        lblnPost = mobjPolicySeq.InsPostVI641(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"),
                                                              Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                              Session("nUsercode"))
                    End If
                End With
            '+ VI641: Criterios para seleción de riesgo
            Case "CA061"
                mobjPolicySeq = New ePolicy.Creditor_information
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostCA061(Session("sCertype"), Session("nBranch"),
                                                            Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                            mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                            Session("nUsercode"), .QueryString("Action"),
                                                            mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("hddConsecutive"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("cbeDetail_Item"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble))


                        mstrQueryString = "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover")
                    Else
                        lblnPost = mobjPolicySeq.InsPostCA061_k(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"),
                                                                Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate),
                                                                mobjValues.StringToType(.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate),
                                                                mobjValues.StringToType(.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble),
                                                                .Form.Item("tctText"),
                                                                Session("nUsercode"))


                    End If
                End With

            '+ CA727 - Reportes automáticos de la póliza
            Case "CA727"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lblnPost = mobjPolicySeq.insPostCA727(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cbeCodispl"), mobjValues.StringToType(Request.Form.Item("cbeTransactype"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))

                    If lblnPost Then
                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA727", "2")
                    End If
                End If
            '+ CA054 - Capitales Depreciados
            Case "CA054"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lblnPost = mobjPolicySeq.insPostCA054(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(Session("nType_amend"), Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    lblnPost = True
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA054", "2")
                End If
            '+ CA054 - Capitales Depreciados
            Case "CA054"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lblnPost = mobjPolicySeq.insPostCA054(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddCCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(Session("nType_amend"), Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    lblnPost = True
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA054", "2")
                End If
            '+ VI665 - Recargo por actividad del grupo (Vida colectivo).
            Case "VI665"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjPolicySeq = New ePolicy.Activ_Group
                    lblnPost = mobjPolicySeq.insPostVI665Upd(Request.QueryString.Item("Action"), 0, Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    If lblnPost Then
                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI665", "2")
                    End If
                End If


            '+ VI681 - Recargos/Descuentos de los asegurados (Vida)
            Case "VI681"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lclsInsured_expdis = New ePolicy.Insured_expdis
                    With Request
                        lblnPost = lclsInsured_expdis.InsPostVI681Upd(.QueryString("Action"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddsClient"), mobjValues.StringToType(.Form.Item("cboDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cboDisexpri"), .Form.Item("chkUnit"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPermTemp"), mobjValues.StringToType(.Form.Item("tcdDate_Fr"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnNotenum"), mobjValues.StringToType(.Form.Item("hddnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkAgree"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboDisexprc_nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddTotalRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddDisexpri_old"), .Form.Item("hddCoverUse_old"), .Form.Item("hddUnit_old"), mobjValues.StringToType(.Form.Item("hddCause_old"), eFunctions.Values.eTypeData.etdLong), .Form.Item("hddCoverUse"), .Form.Item("chkDateEffec"), mobjValues.StringToType(.Form.Item("valActvity"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valSport"), eFunctions.Values.eTypeData.etdLong))
                        'lblnPost = lclsInsured_expdis.insPostVI681Upd(.QueryString("Action"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddsClient"), mobjValues.StringToType(.Form.Item("cboDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cboDisexpri"), .Form.Item("chkUnit"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPermTemp"), mobjValues.StringToType(.Form.Item("tcdDate_Fr"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcnNotenum"), mobjValues.StringToType(.Form.Item("hddnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddoldnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkAgree"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboDisexprc_nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddTotalRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddDisexpri_old"), .Form.Item("hddCoverUse_old"), .Form.Item("hddUnit_old"), mobjValues.StringToType(.Form.Item("hddCause_old"), eFunctions.Values.eTypeData.etdLong), .Form.Item("hddCoverUse"))
                        lclsInsured_expdis = Nothing
                    End With
                End If

            '+ CA001: Tratamiento de pólizas
            Case "CA001"
                Session("PageRetCA050") = "CA001"
                lblnPost = insPostCA001()
            '+ CA001: Tratamiento de pólizas
            Case "CA001C"
                Session("PageRetCA050") = "CA001C"
                lblnPost = insPostCA001()
            '+ CA003: Vía de cobro
            Case "CA003"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA003(.QueryString("Action"), .Form.Item("tctClient"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valAccount"), mobjValues.StringToType(.Form.Item("cbeTyp_crecard"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optBank"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdDateExpir"), eFunctions.Values.eTypeData.etdDate), "", .Form.Item("tctBankAuth"), Session("nTransaction"), .Form.Item("valCredi_card"), mobjValues.StringToType(.Form.Item("cbeTyp_Account"), eFunctions.Values.eTypeData.etdDouble, True))
                End With
            '+ VA1410: Ilustración del valor póliza VUL
            Case "VI1410"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Per_deposit
                        lblnPost = mobjPolicySeq.InsPostVA595Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), "VI1410")

                        mstrQueryString = "&nCurrency=" & .Form.Item("hddnCurrency") & "&nPremiumbas=" & .Form.Item("hddnPremiumbas") & "&nPremimin=" & .Form.Item("hddnPremimin") & "&nVpprdeal=" & .Form.Item("hddnVPprdeal") & "&nPremfreq=" & .Form.Item("hddnPremfreq") & "&nPremdeal=" & .Form.Item("hddnPremdeal") & "&nAmountcontr=" & .Form.Item("hddnPremdep") & "&nIntwarr=" & .Form.Item("hddnIntwarr") & "&nRatepayf=" & .Form.Item("hddnRatepayf") & "&nInsurtime=" & .Form.Item("hddnInsurtime") & "&nOption=" & .Form.Item("hddnOption") & "&sOption=" & .Form.Item("hddsOption") & "&sPayfreq=" & .Form.Item("hddsPayfreq") & "&dBirthdate=" & .Form.Item("hddBirthdate") & "&dEffecdate_to=" & .Form.Item("hddEffecdate_to") & "&nVpi=" & .Form.Item("hddVp_initial")
                    Else
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Projectvul
                        lblnPost = mobjPolicySeq.InsPostVI1410(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), Session("SessionId"), Session("nTransaction"), .Form.Item("hddsProcessed"), .Form.Item("hddsPremdeal_Chan"), mobjValues.StringToType(.Form.Item("hddVp_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr3"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr4"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            Case "VI1410A"

                With Request
                    mstrQueryString = "&nMainAction=304&nYear_ini=" & .Form.Item("tcnYear_ini") & "&nPay=" & .Form.Item("tcnPay")

                    mobjPolicySeq = New ePolicy.Per_deposit_month
                    lblnPost = mobjPolicySeq.InsPostVI1410AUpd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAmountdep_aux"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))

                    If lblnPost And mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble) = 1 And mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                        If Request.Form.Item("sCodisplReload") > vbNullString Then
                            Response.Write("<SCRIPT>top.opener.top.opener.top.opener.top.fraFolder.SetMinDeposit('" & .Form.Item("tcnAmountdep_aux") & "');</" & "Script>")
                        Else
                            Response.Write("<SCRIPT>top.opener.top.opener.top.fraFolder.SetMinDeposit('" & .Form.Item("tcnAmountdep_aux") & "');</" & "Script>")
                        End If
                    End If
                End With
            '+ VA1410: Ilustración del valor póliza VUL
            Case "VI7006"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Per_deposit
                        lblnPost = mobjPolicySeq.InsPostVA595Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), "VI1410", mobjValues.StringToType(.Form.Item("tcnAmountdep_aux"), eFunctions.Values.eTypeData.etdDouble))

                        mstrQueryString = "&nCurrency=" & .Form.Item("hddnCurrency") & "&nPremiumbas=" & .Form.Item("tcnPremium1") & "&nPremimin=" & .Form.Item("hddnPremimin") & "&nVpprdeal=" & .Form.Item("hddnVPprdeal") & "&nPremfreq=" & .Form.Item("hddnPremfreq") & "&nPremdeal=" & .Form.Item("tcnPremdeal") & "&nAmountcontr=" & .Form.Item("hddnPremdep") & "&nIntwarr=" & .Form.Item("hddnIntwarr") & "&nRatepayf=" & .Form.Item("hddnRatepayf") & "&nInsurtime=" & .Form.Item("hddnInsurtime") & "&nOption=" & .Form.Item("hddnOption") & "&sOption=" & .Form.Item("hddsOption") & "&sPayfreq=" & .Form.Item("hddsPayfreq") & "&dBirthdate=" & .Form.Item("hddBirthdate") & "&dEffecdate_to=" & .Form.Item("hddEffecdate_to") & "&nVpi=" & .Form.Item("hddVp_initial")
                    Else
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Projectvul
                        lblnPost = mobjPolicySeq.InsPostVI7006(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), Session("SessionId"), Session("nTransaction"), .Form.Item("hddsProcessed"), .Form.Item("hddsPremdeal_Chan"), mobjValues.StringToType(.Form.Item("tcnPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ CA004: Datos para la facturación
            Case "CA004"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA004(.QueryString("nHolder"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("optFreq"), mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeQuota"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeIndexType"), .Form.Item("cbeIndexApl"), .Form.Item("chkNoNull"), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdIssuedat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdReqDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCopies"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndexRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDaysNull"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDeclarative"), .Form.Item("chkFracti"), IIf(String.IsNullOrEmpty(.Form.Item("chkRenewalAut")), "2", .Form.Item("chkRenewalAut")), .Form.Item("optDirTyp"), mobjValues.StringToType(.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSendAddr"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chksInsubank"), .Form.Item("chksNoPayRoll"), .Form.Item("chkExemption"), mobjValues.StringToType(.Form.Item("cbenSpecialbusiness"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksLeg"), mobjValues.StringToType(.Form.Item("tcnDays_quot"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBill_ind"), mobjValues.StringToType(.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAFPCommi"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hhDirTyp"), mobjValues.StringToType(.Form.Item("valCollector"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkFracReceip"), mobjValues.StringToType(.Form.Item("valgroup_Agree"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctcumul_code"), mobjValues.StringToType(.Form.Item("cbeRepInsured"), eFunctions.Values.eTypeData.etdDouble, True), "1", .Form.Item("cbeReceipt_ind"), mobjValues.StringToType(.Form.Item("tcnTerm_grace"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdTariffDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeDepreciationtable"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chksInd_Multiannual"), .Form.Item("chksIndqsame"), .Form.Item("chksInd_IFI"), mobjValues.StringToType(Session("nType_amend"), Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExtraDay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenFormPay"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbenPromissory_Note"), eFunctions.Values.eTypeData.etdInteger, True))
                    If lblnPost Then
                        Session("dStartdate") = mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate)
                        Session("dExpirdat") = mobjValues.StringToType(.Form.Item("tcdExpirDate"), eFunctions.Values.eTypeData.etdDate)
                        Session("nSpecialbusiness") = mobjValues.StringToType(.Form.Item("cbenSpecialbusiness"), eFunctions.Values.eTypeData.etdInteger)
                        Session("nPayFreq") = mobjValues.StringToType(.Form.Item("cbePayFreq"), eFunctions.Values.eTypeData.etdDouble)
                    End If
                End With
                'ehh - Ad. vt fase I ca073
                If CStr(Session("sPoliType")) = 2 Then
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), "CA073", "3")
                End If
            '+ Información general del colectivo
            Case "CA006"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA006(Session("sCertype"), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sColtimre"), .Form.Item("cbeColInvot"), .Form.Item("cbeColReint"), mobjValues.StringToType(.Form.Item("tcnQCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("txtTariff"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeTypClause"), .Form.Item("cbeTypDiscxp"), .Form.Item("cbeDocuTyp"), .Form.Item("cbeTypModule"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCNotice"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeColtPres"), .Form.Item("chkMassive"), .Form.Item("chkRepPrintCov"), mobjValues.StringToType(.Form.Item("cbenTypeExc"), eFunctions.Values.eTypeData.etdInteger))
                End With

            '+ VI021: documentos solicitados
            Case "VI021"
                With Request

                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If .QueryString.Item("Action") = "Add" Then

                            lcolRoles = New ePolicy.Roleses
                            If lcolRoles.Find_by_Policy(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Request.Form.Item("tctClient"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, 2, "1,13,16,25", True) Then
                                lclsRoles = lcolRoles.Item(1)
                                lintRole = lclsRoles.nRole

                            End If
                            lclsRoles = Nothing
                            lcolRoles = Nothing
                        Else
                            lintRole = mobjValues.StringToType(.Form.Item("hddnRole"), eFunctions.Values.eTypeData.etdDouble)
                        End If

                        lclsLife_docu = New ePolicy.Life_docu
                        If .QueryString.Item("Action") = "Add" Then
                            lblnDocExist = lclsLife_docu.Insdoc_In_Eval_Master(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tctDescript"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            lblnDocExist = False
                        End If


                        If .Form.Item("chkRequire") = "true" Then
                            lstrCheck = "1"
                        Else
                            lstrCheck = "2"
                        End If

                        If lblnDocExist Then
                            lblnPost = mobjPolicySeq.InsPostVI021Upd(.QueryString("Action"), Session("sKey"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("hddnCrThecni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdRecep_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeStat_docreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble), lintRole, .Form.Item("tctClient"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDatefree"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnEval"), eFunctions.Values.eTypeData.etdDouble, True), lclsLife_docu.dExpirdat, mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCumul"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(lclsLife_docu.nStatusdoc), eFunctions.Values.eTypeData.etdDouble, True), lclsLife_docu.dDocreq, lclsLife_docu.dDocrec, lclsLife_docu.dExpirdat, mobjValues.StringToType(CStr(lclsLife_docu.nNotenum_cli), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(lclsLife_docu.nEval_master), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(lclsLife_docu.nId), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble, True), lstrCheck)
                        Else
                            lblnPost = mobjPolicySeq.InsPostVI021Upd(.QueryString("Action"), Session("sKey"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("hddnCrThecni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdRecep_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeStat_docreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble), lintRole, .Form.Item("tctClient"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDatefree"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnEval"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDatevig"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCumul"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStatusdoc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDocreq"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDocrec"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirda"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnNotenum_cli"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEval_master"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble, True), lstrCheck)

                        End If
                        lclsLife_docu = Nothing
                        mstrQueryString = "&sKey=" & Session("sKey")
                    Else
                        lblnPost = mobjPolicySeq.InsPostVI021(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), .Form.Item("hddsKeyM"), mobjValues.StringToType(.Form.Item("hdnEval_Gen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenStatus_eval"), eFunctions.Values.eTypeData.etdDouble, True))

                    End If
                End With

            '+ CA002: Convenios de cobranza de una póliza
            Case "CA002"
                mobjPolicySeq = New ePolicy.Agreement_pol
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA002(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCod_Agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    lblnPost = True
                End If
                mobjPolicySeq = Nothing

            '+ CA008: Situaciones de riesgo.
            Case "CA008"
                mobjPolicySeq = New ePolicy.Situation
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA008(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSituation"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbePolicyHolder"), .Form.Item("tctDescript"),
                                                              mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                Else
                    lblnPost = True
                End If
                mobjPolicySeq = Nothing

            '+ CA009: Capitales Básicos Asegurados
            Case "CA009"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If mblnReload Then
                            mobjPolicySeq = New ePolicy.Sum_insur
                        End If
                        lblnPost = mobjPolicySeq.insPostCA009(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("nSumins_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSumins_real"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSum_insur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCoinsuran"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), .QueryString("Action"), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnSum_insur_old"), eFunctions.Values.eTypeData.etdDouble), Session("sPoliType"), Session("sBrancht"))

                        mstrQueryString = "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&sCurrency=" & Request.QueryString.Item("sCurrency")
                    Else
                        lblnPost = True
                    End If
                End With

            '+ CA010 : Bienes asegurables a la póliza.   
            Case "CA010"
                With Request
                    mobjPolicySeq = New ePolicy.Property_Renamed
                    If Not .QueryString.Item("WindowType") = vbNullString Then
                        lblnPost = mobjPolicySeq.insPostCA010(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFixamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("cbeFrandedi"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnVal_extra"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = mobjPolicySeq.insPostCA010All(Session("nBranch"), Session("nProduct"), Session("sCertype"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nType_amend"), Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), Values.eTypeData.etdDouble))
                        lclsPolicy_Win = New ePolicy.Policy_Win
                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA010", "2")
                    End If
                End With


            '+ CA060 : Desglose de una cobertura.   
            Case "CA060"
                With Request
                    If Not .QueryString.Item("WindowType") = vbNullString Then
                        If mblnReload Then
                            mobjPolicySeq = New ePolicy.Cover_Detail
                        End If

                        lblnPost = mobjPolicySeq.insPostCA060(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
            Case "CA060"
                With Request
                    If Not .QueryString.Item("WindowType") = vbNullString Then
                        If mblnReload Then
                            mobjPolicySeq = New ePolicy.Cover_Detail
                        End If

                        lblnPost = mobjPolicySeq.insPostCA060(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTabGoods"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
            '+ CA011 = Grupos de Colectivos
            Case "CA011"
                lclsPolicy_Win = New ePolicy.Policy_Win

                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    Session("lblnNotPopUp") = "False"
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA011(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeGroupStat"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nPriorGroupStat"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                    If lblnPost Then
                        Session("lblnNotPopUp") = "True"
                    End If

                Else
                    lblnPost = True
                    If CStr(Session("lblnNotPopUp")) = "True" Then
                        lclsPolicy_Win = New ePolicy.Policy_Win
                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA011", "2")
                    End If
                End If

            '+ CA012: Elementos de Protección            
            Case "CA012"
                lclsPolicy_Win = New ePolicy.Policy_Win
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA012(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnElement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble, True), CStr(Today), mobjValues.StringToType(.Form.Item("tcnDisrate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), Session("dNullDate"), Session("nUsercode"), .Form.Item("tctDescript"))
                        If lblnPost Then
                            '+ Se actualiza la imagen de Contenido en el Frame de la izquierda del Browser
                            Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA012", "2")
                        End If
                    End With
                Else
                    With Request
                        For lintCountCA012 = 1 To CInt(.Form.Item("hddnCount"))
                            'ORIGINAL
                            'Select Case .Form.GetValues("hddsAuxSelh").GetValue(lintCountCA012 - 1)
                            Select Case .Form.GetValues("hddsAuxSelh").GetValue(lintCountCA012 - 1)
                                Case "1"
                                    lstrAction = "Add"
                                Case "2"
                                    lstrAction = "Del"
                            End Select

                            mobjPolicySeq = New ePolicy.Protection
                            lblnPost = mobjPolicySeq.insPostCA012(lstrAction, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.GetValues("hddnElement").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hddnCurrency").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnDiscount").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), CStr(Today), mobjValues.StringToType(.Form.GetValues("hddnDisrate").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnMaxAmount").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnMinAmount").GetValue(lintCountCA012 - 1), eFunctions.Values.eTypeData.etdDouble, True), Session("dNullDate"), Session("nUsercode"), .Form.Item("tctDescript"))
                        Next
                    End With
                End If


            '+ CA013: Módulos de la Póliza Individual o Certificado
            Case "CA013", "CA013A"
                mobjPolicySeq = New ePolicy.Modules
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.InsPostCA013Upd(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkChange"), .Form.Item("hddsChange"), Session("nTransaction"), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), Session("sPoliType"), Session("sBrancht"), Session("SessionId"), .QueryString("Action"), .QueryString("sTyp_module"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddstyp_rat"), .Form.Item("chkInherit"))

                        mstrQueryString = "&nGroup=" & Request.QueryString.Item("nGroup") & "&nCurrency=" & Request.QueryString.Item("nCurrency")
                    Else
                        lblnPost = mobjPolicySeq.InsPostCA013(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUserCode"))
                    End If
                End With

            Case "CA014", "CA014A"
                '+ Se agrega el seteo del objeto para cuando el sistema arroaja advertencias
                mobjPolicySeq = New ePolicy.Cover
                With Request
                    If .QueryString.Item("ActionType") = "Check" Then
                        If .Form.Item("hddsExist").Length = 1 Then
                            lblnPost = mobjPolicySeq.InsPostCA014Upd(.QueryString("sCodispl"), .Form.Item("hddsKeyGrid"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeCurrencDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRateCove"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsFrandedi"), .Form.Item("hddsWait_type"), .Form.Item("hddsFrancApl"), mobjValues.StringToType(.Form.Item("hddnDisc_amoun"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnFraRate"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsChange"), mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDiscount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnFixAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnWaitQ"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnCapital_Wait"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnAgeminins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTypdurins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnDurinsur"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnTypdurpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnDurpay"), eFunctions.Values.eTypeData.etdDouble, True), "3", mobjValues.StringToType(.Form.Item("hddnRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRatecove_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemininsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequired"), .Form.Item("chkDefaulti"), mobjValues.StringToType(.Form.Item("hddnBranch_Rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRetarif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCauseupd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddfer"), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), mobjValues.StringToType(.Form.Item("hddnCapital_req"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFraRateClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFixamountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDiscountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_amounClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancdays"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("Action"))

                            If mblnReload Then
                                mstrScript = "top.opener.top.frames['fraFolder'].document.forms[0].Sel.checked=" & .QueryString.Item("sChecked") & "true;" & "top.opener.top.frames['fraFolder'].marrArray[0].Sel =" & .QueryString.Item("sChecked") & "true;"
                            End If
                        Else
                            lblnPost = mobjPolicySeq.InsPostCA014Upd(.QueryString("sCodispl"), .Form.GetValues("hddsKeyGrid").GetValue(CInt(.QueryString("nIndex")) - 1), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeCurrencDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCapital").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRateCove").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCover").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnModulec").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsFrandedi").GetValue(CInt(.QueryString("nIndex")) - 1), .Form.GetValues("hddsWait_type").GetValue(CInt(.QueryString("nIndex")) - 1), .Form.GetValues("hddsFrancApl").GetValue(CInt(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnDisc_amoun").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnFraRate").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), .Form.GetValues("hddsChange").GetValue(CInt(.QueryString("nIndex")) - 1), mobjValues.StringToType(.Form.GetValues("hddnCapital_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnDiscount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnFixAmount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnMaxAmount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnMinAmount").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnWaitQ").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnCapital_Wait").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnAgeminins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxper").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnTypdurins").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnDurinsur").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnTypdurpay").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnDurpay").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), "3", mobjValues.StringToType(.Form.GetValues("hddnRole").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.GetValues("hddnCapital_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRatecove_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnPremium_o").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemininsf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxinsf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnAgemaxperf").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequired"), .Form.Item("chkDefaulti"), mobjValues.StringToType(.Form.GetValues("hddnBranch_Rei").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnRetarif").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnCauseupd").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hdddfer").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), mobjValues.StringToType(.Form.GetValues("hddnCapital_req").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddFraRateClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddFixamountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddMinAmountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddMaxAmountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddDiscountClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddDisc_amounClaim").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddFrancDays").GetValue(CInt(.QueryString("nIndex")) - 1), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("Action"))

                            If mblnReload Then
                                mstrScript = "top.opener.top.frames['fraFolder'].document.forms[0].Sel[" & CStr(CShort(.QueryString.Item("nIndex")) - 1) & "].checked=" & .QueryString.Item("sChecked") & "true;" & "top.opener.top.frames['fraFolder'].marrArray[" & CStr(CShort(.QueryString.Item("nIndex")) - 1) & "].Sel =" & .QueryString.Item("sChecked") & "true;"
                            End If
                        End If
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicySeq.InsPostCA014Upd(.QueryString("sCodispl"), .Form.Item("hddsKeyGrid"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateCove"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeFrandedi"), .Form.Item("cbeWait_type"), .Form.Item("cbeFrancApl"), mobjValues.StringToType(.Form.Item("tcnDisc_amoun"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFraRate"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsChange"), mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFixAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnWaitQ"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital_wait"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgeminins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgemaxins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgemaxper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypdurins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDurinsur"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypdurpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDurpay"), eFunctions.Values.eTypeData.etdDouble, True), "3", mobjValues.StringToType(.Form.Item("hddnRole"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsClient"), mobjValues.StringToType(.Form.Item("hddnCapital_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnRatecove_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgemininsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequired"), .Form.Item("chkDefaulti"), mobjValues.StringToType(.Form.Item("valBranch_rei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRetarif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCauseupd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdFer"), eFunctions.Values.eTypeData.etdDate), Session("sPolitype"), mobjValues.StringToType(.Form.Item("hddnCapital_req"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFraRateClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFixamountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDiscountClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_amounClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancDays"), Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnPremimax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremimin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCacalmax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCacalmin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))

                            mstrQueryString = "&nRole=" & Request.Form.Item("hddnRole") & "&sKey=" & Request.Form.Item("hddsKeyGrid") & "&nGroup=" & Request.Form.Item("hddnGroup") & "&nCurrency=" & Request.Form.Item("hddnCurrency") & "&sClient=" & Request.Form.Item("hddsClient") & "&nIndexCover=" & Request.Form.Item("hddsIndexCover") & "&sDelTCover=0&sRecPopup=1"
                        Else
                            'UPGRADE_NOTE: A string expression is used as boolean value. It has a different behavior than the original code. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1021.htm
                            If Not mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                                lblnPost = mobjPolicySeq.InsPostCA014(.Form.Item("hddsKey"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), mobjValues.StringToDate(Session("dNulldate")), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString("sCodispl"), .QueryString("nIndexCover"), mobjValues.StringToType(.Form.Item("tcnLeg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))
                            Else
                                lblnPost = mobjPolicySeq.InsPostCA014Copy(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("nTransaction"))
                                If Not lblnPost Then
                                    lblnPost = True
                                End If
                            End If

                            '+ Si se efectúa la actualización puntual se recarga la página. 
                            If CBool(IIf(IsNothing(Request.Form.Item("hddbPuntual")), False, Request.Form.Item("hddbPuntual"))) Then
                                lclsErrors = New eFunctions.Errors
                                '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.10 
                                lclsErrors.sSessionID = Session.SessionID
                                lclsErrors.nUsercode = Session("nUsercode")
                                '~End Body Block VisualTimer Utility 
                                '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla 

                                Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881, , , , True))

                                lclsErrors = Nothing

                                Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")

                                Response.Write("try {")

                                Response.Write("window.close();top.frames['fraFolder'].document.location.href=top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")

                                Response.Write("}")
                                Response.Write("catch(error){")
                                Response.Write("window.close();opener.top.frames['fraFolder'].document.location.href=opener.top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")

                                Response.Write("}")

                                Response.Write("</" & "Script>")

                                lblnPost = False
                            End If
                        End If
                    End If
                End With
            '+ CA015: Franquicia/Deducible de la Póliza
            Case "CA015", "CA15-1"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA015(Request.QueryString.Item("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), CStr(Session("nTransaction")), Session("sTypeCompanyUser"), .Form.Item("optFranchiseType"), .Form.Item("cbeFranqApl"), mobjValues.StringToType(.Form.Item("cbeCurrencyFD"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscountPerc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscountAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchisePerc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFranchiseMax"), eFunctions.Values.eTypeData.etdDouble), Session("sPolitype"))
                End With

            '+ CA016: Recargos/descuentos/impuestos de una póliza individual o certificado
            Case "CA016", "CA016A"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        Session("WindowType") = "3"
                        lblnPost = mobjPolicySeq.InsPostCA016Upd(.Form.Item("hddnExist"), "1", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddnDisc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), Session("sPoliType"), .QueryString("sTyp_discxp"), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkAgree"), Session("nUsercode"))
                    Else
                        lblnPost = mobjPolicySeq.InsPostCA016(.QueryString("sCodispl"), .Form.Item("hddsSel"), .Form.Item("hddnExist"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddnCurrency"), Session("dNulldate"), .Form.Item("hddnDisc_code"), .Form.Item("hddnAmount"), .Form.Item("hddnPercent"), .Form.Item("hddnCause"), .Form.Item("hddsAgree"), Session("nUsercode"), Session("nTransaction"))
                        '+ Si se efectúa la actualización puntual se recarga la página.
                        If CBool(IIf(IsNothing(.Form.Item("hddbPuntual")), False, .Form.Item("hddbPuntual"))) Then
                            lclsErrors = New eFunctions.Errors
                            '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.11
                            lclsErrors.sSessionID = Session.SessionID
                            lclsErrors.nUsercode = Session("nUsercode")
                            '~End Body Block VisualTimer Utility
                            '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla
                            Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881, , , , True))
                            lclsErrors = Nothing
                            Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=top.frames['fraFolder'].document.location</" & "Script>")
                            lblnPost = False
                        End If
                    End If
                    If mobjPolicySeq.nChangeDisc_xprem = 1 Then
                        lclsRefresh = New ePolicy.ValPolicySeq
                        Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "Yes"))
                        lclsRefresh = Nothing


                    End If

                End With

                mstrQueryString = "&nPage=" & Request.Form.Item("hddPage")

            '+ CA017: Emisión de Recibos de una póliza 
            Case "CA017"
                lclsPolicy_Win = New ePolicy.Policy_Win
                lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA017", "2")

                If Session("nBranch") = ePolicy.Policy.Branch_Pol.cstr_VidaIndividualLargoPlazo And Session("nProduct") = ePolicy.Policy.Product_Pol.cstr_VidaDevolucionProtecta Then
                    lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA072", "2")
                End If
            '+ CA017A: Cuotas de un recibo 
            Case "CA017A"
                If CBool(Request.Form.Item("hddbValCa017a")) Then
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA017A", "2")
                Else
                    lblnPost = True
                End If

            '+ CA017A: Emisión de Recibos de una póliza  solicitud de endoso
            Case "CA017B"
                lclsPolicy_Win = New ePolicy.Policy_Win
                lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA017B", "2")
            '+ CA020: Distribución de Coaseguro
            Case "CA020"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostCA020(.QueryString("WindowType"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExpenses"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = mobjPolicySeq.insPostCA020(.QueryString("WindowType"), "Add", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nCompanyUser"), mobjValues.StringToType(.Form.Item("tcnOwnShare"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExpenses"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ CA021: Distribución de Reaseguro
            Case "CA021"

                If Not Session("bQuery") Then
                    lclsReinsuran = New ePolicy.Reinsuran
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        '+Grid contratante                
                        If Request.Form.Item("blnContract") = "True" Then
                            llngType = Request.Form.Item("tcnType")
                            llngCompany = Session("nCompanyuser")
                            ldtmAcce = eRemoteDB.Constants.dtmNull
                            ldblCommiss = "0"
                            ldblInterRate = "0"
                            ldblReserRate = "0"
                            mstrQueryString = "&nQueryModeF=4" & "&nCapital_cov=" & Request.QueryString.Item("nCapital_cov")
                            lintGrid = 2
                            '+Grid Facultativo
                        ElseIf Request.Form.Item("tctPopUpT") = "F" Then
                            llngType = Request.Form.Item("tcnType")
                            llngCompany = Request.Form.Item("cbeCompany")
                            ldtmAcce = Request.Form.Item("tcdAcceptdate")
                            ldblCommiss = Request.Form.Item("tcnComission")
                            ldblInterRate = Request.Form.Item("tcnInter_rate")
                            ldblReserRate = Request.Form.Item("tcnReser_rate")

                            mstrQueryString = "&nQueryModeF=4"
                            lintGrid = 3
                            '+Grid Cobertura
                        Else
                            mstrQueryString = "&nChange=" & Request.Form.Item("cbeChange") & "&nCapital_cov=" & Request.QueryString.Item("nCapital_cov")
                            lintGrid = 1
                        End If
                        '+ PRY-REASEGUROS VT - CAPITAL CORRECTO PARA TIPO DE DISTRIBUCION FACULTATIVA  - LAMC - INICIO
                        If CDbl(Request.Form.Item("cbeChange")) = 1 Or CDbl(Request.Form.Item("cbeChange")) = 2 Or CDbl(Request.Form.Item("cbeChange")) = 4 Or CDbl(Request.Form.Item("cbeChange")) = 3 Then
                            nCapital_rei = mobjValues.StringToType(Request.Form.Item("tcnReinCapital"), eFunctions.Values.eTypeData.etdDouble)
                        Else
                            nCapital_rei = mobjValues.StringToType(Request.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble)
                        End If
                        '+ PRY-REASEGUROS VT - CAPITAL CORRECTO PARA TIPO DE DISTRIBUCION FACULTATIVA  - LAMC - FIN
                        lblnPost = lclsReinsuran.insPostCA021upd(lintGrid, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("dNullDate"), mobjValues.StringToType(Request.Form.Item("cbeBranchrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valClient"), mobjValues.StringToType(llngType, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(llngCompany, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldtmAcce, eFunctions.Values.eTypeData.etdDate), nCapital_rei, mobjValues.StringToType(ldblCommiss, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sHeap_Code"), mobjValues.StringToType(ldblInterRate, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldblReserRate, eFunctions.Values.eTypeData.etdDouble), System.Math.Round(CDbl(mobjValues.StringToType(Request.Form.Item("tcnPercentage"), eFunctions.Values.eTypeData.etdDouble)), 6), Session("nUsercode"), Request.QueryString.Item("Action"), Request.Form.Item("tctBrancht"), CInt(Request.Form.Item("cbeChange")), CDbl(Request.Form.Item("tcnRetention")), mobjValues.StringToType(Request.Form.Item("tcnPremium_Agree"), Values.eTypeData.etdDouble))

                        '	                                                        mobjValues.StringToType(Request.Form(tcnInter_rate),eFunctions.Values.eTypeData.etdDouble),
                    Else
                        lblnPost = lclsReinsuran.insPostCA021(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"))
                    End If

                    lclsReinsuran = Nothing
                Else
                    lblnPost = True
                End If
            '+ CA021A: REASEGURO PÓLIZA MATRIZ	    
            Case "CA021A"
                'mstrQueryString = "&nQueryModeF=4" & 
                lclsReinsuran = New ePolicy.Reinsuran
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    'Coberturas
                    If Request.QueryString.Item("sIsCOB") = "1" Then
                        'Cuota Parte
                        If Request.Form.Item("hddnType") = "1" Or Request.Form.Item("hddnType") = "2" Then
                            lblnPost = lclsReinsuran.insPostCA021Aupd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.Form.Item("hddnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("hddnComission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnInter_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnReser_rate"), eFunctions.Values.eTypeData.etdDouble), System.Math.Round(CDbl(mobjValues.StringToType(Request.Form.Item("tcnQuota_sha"), eFunctions.Values.eTypeData.etdDouble)), 6), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("hddnCapital_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnShare_rei"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnContract"), eFunctions.Values.eTypeData.etdLong))
                        End If
                    Else
                        lblnPost = lclsReinsuran.insPostCA021Aupd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdAcceptDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnComission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInter_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnReser_rate"), eFunctions.Values.eTypeData.etdDouble), System.Math.Round(CDbl(mobjValues.StringToType(Request.Form.Item("tcnPercentage"), eFunctions.Values.eTypeData.etdDouble)), 6), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("tcnCapital_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnShare_rei"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"), 0, 0)

                    End If
                Else
                    lblnPost = lclsReinsuran.insPostCA021A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                End If
            '+ CA022: Cláusula/descriptivo/condición especial        
            Case "CA022"
                With Request
                    mobjPolicySeq = New ePolicy.Clause
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.InsPostCA022Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valClause"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valInsured"), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkAgree"), mobjValues.StringToType(.Form.Item("hddGroup_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddNoteNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIniNote"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("nTransaction"), Session("dNulldate"))
                    End If
                End With

            '+ CA022A: Cláusulas de la póliza matriz
            Case "CA022A"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA022A(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form.Item("hddnClause"), .Form.Item("hddnSelClause"), .Form.Item("hddNoteNum"), .Form.Item("hddNoteNum_Prod"), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkType_Clause"), Request.Form.Item("tctDoc_attach"))
                    If lblnPost Then
                        '+ Si se efectúa la actualización puntual se recarga la página.
                        If CBool(IIf(IsNothing(.Form.Item("hddbPuntual")), False, .Form.Item("hddbPuntual"))) Then
                            lclsErrors = New eFunctions.Errors
                            '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.17
                            lclsErrors.sSessionID = Session.SessionID
                            lclsErrors.nUsercode = Session("nUsercode")
                            '~End Body Block VisualTimer Utility
                            '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla
                            Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881, , , , True))
                            lclsErrors = Nothing
                            Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=top.frames['fraFolder'].document.location</" & "Script>")
                            lblnPost = False
                        End If
                    End If
                End With
            '+ CA072: Cuadro de rescate de poliza
            Case "CA072"
                lclsPolicy_Win = New ePolicy.Policy_Win
                lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA072", "2")
            '+ CA073: Estimación de contrato
            Case "CA073"
                Dim strReciept As String = ""
                Dim nStatus As Integer = 0
                strReciept = mobjPolicySeq.insPostCA073(CStr(Session("sPolitype")), Session("sCertype"), Session("nBranch"), Session("nProduct"),
                                                    Session("nPolicy"), Session("nCertif"), -1, -1, -1, mobjValues.StringToType(Request.Form.Item("tcnInPrimNetaFP"), eFunctions.Values.eTypeData.etdDouble),
                                                    mobjValues.StringToType(Request.Form.Item("tcnInDE"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInIGV"), eFunctions.Values.eTypeData.etdDouble), nStatus)

                If nStatus = 0 Then
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    lblnPost = lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA073", "2")
                    Session("strReciept") = strReciept
                Else
                    Response.Write("<SCRIPT>alert('Error en generar recibos.');</" & "Script>")
                End If

            '+ CA023: Beneficiarios identificados por código
            Case "CA023"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        nCover = .Form.Item("valCover")
                        If nCover = "" Then
                            nCover = 0
                        End If
                        nModulec = .Form.Item("valModulec")
                        If nModulec = "" Then
                            nModulec = 0
                        End If
                        If .Form.Item("chkIrrevoc") = "1" Then
                            lstrIrrevoc = "1"
                        Else
                            lstrIrrevoc = "2"
                        End If

                        If .Form.Item("chkConti") = "1" Then
                            lstrConting = "1"
                        Else
                            lstrConting = "2"
                        End If
                        If .Form.Item("chkDesign") = "1" Then
                            lstrDesign = "1"
                        Else
                            lstrDesign = "2"
                        End If


                        lblnPost = mobjPolicySeq.insPostCA023(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), mobjValues.StringToType(.Form.Item("tcddatedecla"), eFunctions.Values.eTypeData.etdDate), lstrIrrevoc, lstrConting, 0, lstrDesign)
                    End With
                End If

            '+ CA024: Intermediarios
            Case "CA024"
                With Request
                    If CBool(IIf(IsNothing(.QueryString.Item("bAll")), False, .QueryString.Item("bAll"))) Then
                        ldblPercent = mobjValues.StringToType(.Form.Item("tcnPercentCF"), eFunctions.Values.eTypeData.etdDouble, True)
                    Else
                        ldblPercent = mobjValues.StringToType(.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble, True)
                    End If
                    '+ bAll = false para actualizar comisiones por registro de la grilla 
                    lblnPost = mobjPolicySeq.insPostCA024(.QueryString("bAll"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form.Item("cbeType"), ldblPercent, .Form.Item("hddInd_Comm"), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddConColl"), .QueryString("Action"), Session("nTransaction"), mobjValues.StringToType(.Form.Item("cbeAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent_ce"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInstallcom"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CA025: Cliente de la póliza
            Case "CA025"
                mobjPolicySeq = New ePolicy.Roles
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble) = 13 Then
                            lstrClient = vbNullString
                            lstrClientOld = vbNullString
                            lintIntermedia = mobjValues.StringToType(.Form.Item("tctCode"), eFunctions.Values.eTypeData.etdDouble, True)
                            lintIntermediaOld = mobjValues.StringToType(.Form.Item("hddsOldCode"), eFunctions.Values.eTypeData.etdDouble, True)
                        Else
                            lstrClient = .Form.Item("tctCode")
                            lstrClientOld = .Form.Item("hddsOldCode")
                            lintIntermedia = eRemoteDB.Constants.intNull
                            lintIntermediaOld = eRemoteDB.Constants.intNull
                        End If

                        mobjPolicySeq.bCreateInsured = mblnCreateInsured
                        lblnPost = mobjPolicySeq.InsPostCA025Upd(.QueryString("Action"), Session("nTransaction"), mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), lstrClient, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), lintIntermedia, Session("sBrancht"), mobjValues.StringToType(.Form.Item("tcdBirthdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeSexclien"), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("cbeTyperisk"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkVIP"), mobjValues.StringToType(.Form.Item("tcnRating"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStatusrol"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctItem"), Session("sPolitype"), .Form.Item("hddsCompon"), lstrClientOld, lintIntermediaOld, Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeTypename"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(Session("dNulldate")), mobjValues.StringToType(.Form.Item("hddnCoverPos"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCode_sClient"), .Form.Item("hddsRequire"), mobjValues.StringToType(.Form.Item("tcdContinue"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valContrat_Pay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctPrintName"), .Form.Item("chksContinued"))
                    Else

                        lblnPost = mobjPolicySeq.InsPostCA025(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                    End If
                End With
            '+FR001: Instrumentos Financieros
            Case "FR001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = ePolicy.FinancialInstrument.Post(.QueryString.Item("sCodispl"),
                                                                    .QueryString.Item("Action"),
                                                                    .QueryString.Item("WindowType"),
                                                                    Session("sCertype"),
                                                                    Session("nBranch"),
                                                                    Session("nProduct"),
                                                                    Session("nPolicy"),
                                                                    Session("nCertif"),
                                                                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("NCONSECUTIVE"), eFunctions.Values.eTypeData.etdInteger),
                                                                    mobjValues.StringToType(.Form.Item("NBANK_CODE"), eFunctions.Values.eTypeData.etdInteger),
                                                                    mobjValues.StringToType(.Form.Item("NINSTRUMENT_TY"), eFunctions.Values.eTypeData.etdInteger),
                                                                    mobjValues.StringToType(.Form.Item("NCARD_TYPE"), eFunctions.Values.eTypeData.etdInteger),
                                                                    .Form.Item("SNUMBER"),
                                                                    mobjValues.StringToType(.Form.Item("DCARDEXPIR"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("DSTARTDATE"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("DTERM_DATE"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("NQUOTA"), eFunctions.Values.eTypeData.etdInteger),
                                                                    mobjValues.StringToType(.Form.Item("NAMOUNT"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdInteger),
                                                                    mobjValues.StringToType(.Form.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate),
                                                                    Session("nUsercode"))
                    Else
                    End If
                End With

            '+ CA027, CA027A: Emisión de recibo automático
            Case "CA027", "CA027A"
                If Request.QueryString.Item("sCodispl") = "CA027A" Then
                    mobjPolicySeq = New ePolicy.TDetail_pre
                    lblnPost = mobjPolicySeq.inspostCA027A(Request.Form.Item("chkDelReceipt"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Request.Form.Item("lblReceipt"))
                    If lblnPost Then
                        lclsPolicy_Win = New ePolicy.Policy_Win
                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA027A", "2")
                    End If
                Else
                    If CStr(Session("sCodisplOri")) = "CA034" Then
                        '+ Se ejecuta reporte de la transaccón CA034 cuando es llamada de dicha transacción
                        If Request.QueryString.Item("sExeReport") = "1" Then
                            mobjtRehabilitate = New ePolicy.TRehabilitate
                            Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dNullDate"), eFunctions.Values.eTypeData.etdDate), CInt(Request.QueryString.Item("nExeMode")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble))
                            Session("sKey") = mobjtRehabilitate.sKey

                            mobjtRehabilitate = Nothing
                            lobjDocuments = New eReports.Report
                            With lobjDocuments
                                If Request.QueryString.Item("sBrancht") = "1" Then
                                    .ReportFilename = "CAL034_V.rpt"
                                    .sCodispl = "CAL034"
                                    .setStorProcParam(1, Session("sKey"))
                                    .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(3, mobjValues.StringToType(Session("nProposal"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(4, Session("nUsercode"))
                                Else
                                    .ReportFilename = "CAL034.rpt"
                                    .sCodispl = "CAL034"
                                    .setStorProcParam(1, Session("sKey"))
                                    .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                                End If

                                Response.Write((.Command))
                            End With
                            lobjDocuments = Nothing
                        End If
                    Else
                        '+ Se ejecuta reporte de la transaccón CA033 cuando es llamada de dicha transacción
                        If CStr(Session("sCodisplOri")) = "CA033" Then

                            If mobjValues.StringToType(Session("optExecute"), eFunctions.Values.eTypeData.etdDouble) = 2 Then

                                mobjPolicyTra = New ePolicy.ValPolicyTra

                                lblnP_data = mobjPolicyTra.UpdatePartic_data(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                                mobjPolicyTra = Nothing

                            End If

                            If Request.QueryString.Item("sExeReport") = "1" Then
                                lobjDocuments = New eReports.Report
                                With lobjDocuments
                                    .ReportFilename = "CAL033.rpt"
                                    .sCodispl = "CAL033"
                                    '.setStorProcParam 1,  Request.QueryString("sCertype")
                                    .setStorProcParam(1, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(3, mobjValues.StringToType(Request.QueryString.Item("npolicy"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(4, mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(5, .setdate(Request.QueryString.Item("dNullDate")))
                                    .setStorProcParam(6, Request.QueryString.Item("sNullReceipt"))
                                    .setStorProcParam(7, Request.QueryString.Item("nExeMode"))
                                    .setStorProcParam(8, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(9, mobjValues.StringToType(Request.QueryString.Item("soptReceipt"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(10, Session("sKey"))
                                    .setStorProcParam(11, Request.QueryString.Item("nProponum"))
                                    .nGenPolicy = 1
                                    .sReport = "End" & Request.QueryString.Item("nProduct") & Request.QueryString.Item("npolicy")
                                    .MergeCertype = "2"
                                    .MergeBranch = Request.QueryString.Item("nBranch")
                                    .MergeProduct = Request.QueryString.Item("nProduct")
                                    .MergePolicy = Request.QueryString.Item("npolicy")
                                    .MergeCertif = Request.QueryString.Item("nCertif")
                                    lclsPolicy = New ePolicy.Policy
                                    lclsPolicy.Find(Request.QueryString.Item("sCertype"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nPolicy"), True)
                                    .nMovement = lclsPolicy.nMov_histor
                                    Response.Write((.Command))
                                End With
                                lobjDocuments = Nothing
                            End If
                        End If
                        lblnPost = True
                    End If
                End If

                mobjCertificat = New ePolicy.Certificat
                If mobjCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
                    llngPayfreq = mobjCertificat.nPayfreq
                End If
                mobjCertificat = Nothing

            '+ CA639: Condición de capitales
            Case "CA639"
                mobjPolicySeq = New ePolicy.Cond_cover
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA639(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTipcap"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("hddnID"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthI"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonthE"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    With Request
                        If Not mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                            lblnPost = True
                        Else
                            lblnPost = mobjPolicySeq.insPostCA639Copy(Session("sCertype"), Session("nBranch"), Session("nProduct"),
                                                                      Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                            If Not lblnPost Then
                                lblnPost = True
                            End If
                        End If

                        '+ Si se efectúa la actualización puntual se recarga la página. 
                        If CBool(IIf(IsNothing(Request.Form.Item("hddbPuntual")), False, Request.Form.Item("hddbPuntual"))) Then
                            lclsErrors = New eFunctions.Errors
                            '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.10 
                            lclsErrors.sSessionID = Session.SessionID
                            lclsErrors.nUsercode = Session("nUsercode")
                            '~End Body Block VisualTimer Utility 
                            '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla 

                            Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881, , , , True))

                            lclsErrors = Nothing

                            Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            Response.Write("try {")
                            Response.Write("window.close();top.frames['fraFolder'].document.location.href=top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")
                            Response.Write("}")
                            Response.Write("catch(error){")
                            Response.Write("window.close();opener.top.frames['fraFolder'].document.location.href=opener.top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")
                            Response.Write("}")
                            Response.Write("</" & "Script>")

                            lblnPost = False
                        End If
                    End With
                End If
                mobjPolicySeq = Nothing

            '**+ VI006:	Investments Funds.
            '+ VI006: Fondos de inversiones.

            Case "VI006"

                lclsFunds_Pol = New ePolicy.Funds_Pol
                lclsTab_ord_origin = New eBranches.Tab_Ord_Origin
                lcolTab_ord_origin = New eBranches.Tab_Ord_Origins

                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("chkActivFound"), eFunctions.Values.eTypeData.etdBoolean) Then
                            ' If CBool(.Form.Item("chkActivFound")) Then
                            sActivefound = "1"
                        Else
                            sActivefound = "2"
                        End If

                        If CStr(Session("sApv_VI006")) = "1" Then
                            Call lcolTab_ord_origin.Find(Session("nBranch"), Session("nProduct"))
                            For Each lclsTab_ord_origin In lcolTab_ord_origin
                                Call lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), sActivefound, "2", mobjValues.StringToType(CStr(lclsTab_ord_origin.nOrigin), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeProfile"), eFunctions.Values.eTypeData.etdDouble))

                            Next lclsTab_ord_origin
                        Else
                            Call lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), sActivefound, "2", mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeProfile"), eFunctions.Values.eTypeData.etdDouble))

                        End If

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")

                    Else
                        lblnPost = True

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "2")

                    End If

                    lclsFunds_Pol = Nothing
                    lclsPolicy_Win = Nothing
                    lcolTab_ord_origin = Nothing
                    lclsTab_ord_origin = Nothing

                End With

            '+ IN010: Datos particulares de incendio
            Case "IN010"
                With Request
                    lblnPost = mobjPolicySeq.InsPostIN010("Update", "IN010", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cboArticle"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboDetailArt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFloor_quan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSpCombType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSideCloseType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRooftype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBuildType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSeismicZone"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcnDep_prem"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeDecla_Freq"), .Form.Item("cbeDecla_Type"), Session("nUsercode"))
                    Session("IN010") = lblnPost
                End With

            '+ VI001: Interés Asegurable
            Case "VI001"
                With Request
                    lblnPost = mobjPolicySeq.InsPostVI001("Update", "VI001", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerNunMi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypDurins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsur_Time"), eFunctions.Values.eTypeData.etdDouble), Session("sPolitype"), mobjValues.StringToType(.Form.Item("cbovalgroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalsituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdexpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnrentamount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbocurrrent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcncount_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnperc_cap"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPay_Time"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate_pay"), eFunctions.Values.eTypeData.etdDate))
                End With

            '**+ VI7001 - Life Assurance.
            '+ VI7001 - Interés asegurable.
            '+ VI7001 - Interes Asegurable - Unit Linked 

            Case "VI7001"
                With Request
                    lblnPost = mobjPolicySeq.InsPostVI7001("Update", "VI7001", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeReinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsurTimeAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsurTimeAgeLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPduraind"), .Form.Item("tctIduraind"), "", mobjValues.StringToType(.Form.Item("tcnPay_Time"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(.Form.Item("tcnInsurTimeAge"), eFunctions.Values.eTypeData.etdDouble), Session("sPolitype"), mobjValues.StringToType(.Form.Item("cbovalgroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalsituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSaving_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_save_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_unit_pct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIndex_table"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremmin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntwarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctIduraind"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "VI7010"
                With Request
                    lblnPost = mobjPolicySeq.InsPostVI7010(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("tctFirstname"), .Form.Item("tctLastname"), .Form.Item("tctLastname2"), mobjValues.StringToType(.Form.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tctAge"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeSex"), mobjValues.StringToType(.Form.Item("cbeOccupat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSmoking"), .Form.Item("cbeTyperisk"), mobjValues.StringToType(.Form.Item("cbeCivilsta"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenCurrency"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "VI7011"
                With Request
                    lblnPost = mobjPolicySeq.InsPostCA014(.Form.Item("hddsKey"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), Session("dNulldate"), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hddnProdclas"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString("sCodispl"), .QueryString("nIndexCover"), mobjValues.StringToType(.Form.Item("tcnLeg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDataFound"), eFunctions.Values.eTypeData.etdDouble))

                End With

            '+ AU001: Información del vehículo

            Case "AU001"
                lclsPolicy_Win = New ePolicy.Policy_Win
                With Request
                    If .Form.Item("chksn_infrac") = "1" Then
                        lstrsn_infrac = "1"
                    Else
                        lstrsn_infrac = "2"
                    End If
                    If .Form.Item("chksrelapsing") = "1" Then
                        lstrsrelapsing = "1"
                    Else
                        lstrsrelapsing = "2"
                    End If
                    If .Form.Item("chksreturn") = "1" Then
                        lstrsreturn = "1"
                    Else
                        lstrsreturn = "2"
                    End If

                    lblnPost = mobjPolicySeq.insPostAU001("AU001", Session("sPolitype"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeLicense_ty"), .Form.Item("tctRegister"), .Form.Item("tctChassis"), .Form.Item("tctMotor"), .Form.Item("tctColor"), .Form.Item("valVehcode"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeProviCod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnVehPlace"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnVehPma"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeDeduc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLastClaim"), eFunctions.Values.eTypeData.etdDate), 1, mobjValues.StringToType(.Form.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDigit"), lstrsrelapsing, lstrsn_infrac, lstrsreturn, mobjValues.StringToType(.Form.Item("cbenlic_special"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCollectedPremium"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctEngine"), .Form.Item("chks_HybridVehicle"), .Form.Item("tctClient_Dealer"), .Form.Item("tctClient_Seller"), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nType_amend"), Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valAgenDealer"), Values.eTypeData.etdInteger, True))


                    If lblnPost And lstrsrelapsing = "1" Then
                        '+ Se actualiza la imagen de Contenido para que quede requerida la pagina ca024 - Intermediarios
                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), "CA024", "3")
                    ElseIf lblnPost Then
                        '+ Se actualiza la imagen de Contenido para que quede requerida la pagina ca014 - Coberturas
                        Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), "CA014", "3")
                    End If
                    lclsPolicy_Win = Nothing
                End With

            '+ CA041: Selección de Monedas
            Case "CA041"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA041(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddExist"), .Form.Item("hddChange"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddCurrency"))
                End With

            '+ Ventana de Fin de proceso        
            Case "GE101"
                lblnPost = insCancel()

            Case "CA047"
                With Request
                    lblnPost = mobjPolicySeq.insPostCA047(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form.Item("optTypeSolic"), .Form.Item("tcdStayDate"), Session("nTransaction"))
                End With

            '+ VI732: Cuadro de valores garantizados 
            Case "VI732"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lintCurrency = mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True)
                    Else
                        lintCurrency = mobjValues.StringToType(.Form.Item("cbeCurrency_A"), eFunctions.Values.eTypeData.etdDouble, True)
                    End If
                    lblnPost = mobjPolicySeq.inspostVI732(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optAut_guarval"), lintCurrency, mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPro_year"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnResc_val"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSald_val"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSaldvalkm"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnDefamount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDeferred"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSal_tax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPeriod_cov"), eFunctions.Values.eTypeData.etdDouble, True))
                End With


            '+ VI769: Declaración de beneficiarios
            Case "VI769"
                lclsDecla_benef = New ePolicy.Decla_benef
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = lclsDecla_benef.insPostVI769("VI769", .QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNumdecla"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkIrrevoc"), mobjValues.StringToType(.Form.Item("tcdDatedecla"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                End If
                lclsDecla_benef = Nothing

            '+ CA748: Observaciones de una propuesta
            Case "CA748"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.inspostCA748(.QueryString("Action"), .Form.Item("hddCertype"), mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddpolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeObservation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                        mstrQueryString = "&sCertype=" & .Form.Item("hddCertype") & "&nBranch=" & .Form.Item("hddBranch") & "&nProduct=" & .Form.Item("hddProduct") & "&nPolicy=" & .Form.Item("hddPolicy") & "&nCertif=" & .Form.Item("hddCertif") & "&dEffecdate=" & .Form.Item("hddEffecdate")
                    End If
                End With

            '+ VA589: Datos particulares de Vida activa
            Case "VA589"
                mobjPolicySeq = New ePolicy.Activelife
                With Request
                    lblnPost = mobjPolicySeq.insPostVA589(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapitaldeath"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIduraind"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInsurtime"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntproject"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWarminint"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenOption"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypeinvest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremiumbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnVPprdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hdddExpirdat"), eFunctions.Values.eTypeData.etdDate), Session("sPoliType"), Session("sBrancht"), mobjValues.StringToType(.Form.Item("hdddIssuedat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCapitaldeath"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPremMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPrsugest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnVPprsug"), eFunctions.Values.eTypeData.etdDouble))

                End With

            '+ VI701: Datos particulares vida colectivo desgravamen
            Case "VI701"
                mobjPolicySeq = New ePolicy.Life
                lblnPost = mobjPolicySeq.insPostVI701(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount_cre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount_act"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurren_cre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCalcapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTyppremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeSituation"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctCreditnum"), mobjValues.StringToType(Request.Form.Item("cbeCred_pro"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInit_cre"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_cre"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctAccnum"), mobjValues.StringToType(Request.Form.Item("tcnCapitalmax"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("tcnRateDesg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQ_Quot"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
            '+ CA658: Nómina de cotización (vida colectivo)
            Case "CA658"
                With Request
                    lblnPost = mobjPolicySeq.inspostCA658(Request.QueryString.Item("WindowType"), Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("OptAge"), mobjValues.StringToType(.Form.Item("tcdBirthDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnInitAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRentAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkVIP"), .Form.Item("optType"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ AM002: Tarifas de atención médica
            Case "AM002"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostAM002Upd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnTariff"), eFunctions.Values.eTypeData.etdDouble), Session("sDefaulti"), mobjValues.StringToType(.Form.Item("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAgeInit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAgeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.TypeToString(.Form.Item("cbeGroupComp"), eFunctions.Values.eTypeData.etdDouble), Session("dNullDate"), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("nTransaction"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnGroupDed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble))
                        mstrQueryString = "&sOnSeq=1" & "&nTariff=" & Request.Form.Item("tcnTariff") & "&nGroup=" & Request.Form.Item("tcnGroup") & "&nRole=" & Request.Form.Item("tcnRole") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("tcnCover") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sWait_type=" & .Form.Item("hddWait_type") & "&nWait_quan=" & .Form.Item("hddWait_quan")
                    Else
                        lblnPost = mobjPolicySeq.insPostAM002(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), Session("sDefaulti"), Session("nTransaction"), Session("dNullDate"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeWait_type"), mobjValues.StringToType(.Form.Item("tcnWait_quan"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            Case "AM003"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        lblnPost = mobjPolicySeq.insPostAM003Upd(.QueryString("Action"), Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnDed_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Percen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Quanti"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimit_exe"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLimitH"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIndem_Rate"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString("sAutRestit"), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), .QueryString("sIllness"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrestac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTyplim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPunish"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsCaren_Type"), mobjValues.StringToType(.Form.Item("tcnCaren_Dur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDed_Quanti_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIndem_Rate_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLimit_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTyplim_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCount_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLimit_Exe_2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPunish_2"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chksOtherLim"), Session("sPoliType"), CBool("True"))
                        'CBool(.QueryString("bCreHeader")))
                    Else
                        lblnPost = mobjPolicySeq.insPostAM003(Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("dNulldate"), Session("nUsercode"))
                    End If
                End With

            '+ AM006: Exclusión de enfermedades
            Case "AM006"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If CStr(Session("sPolitype")) = "2" And CStr(Session("nCertif")) = "0" Then
                            llngTariff = "0"
                        Else
                            If .QueryString.Item("nTariff") = "0" Then
                                llngTariff = 0
                            Else
                                llngTariff = .QueryString.Item("nTariff")
                            End If
                        End If
                        Dim lstrTypeExclu
                        lstrTypeExclu = .Form.Item("chkExclud")
                        If lstrTypeExclu = vbNullString Then
                            lstrTypeExclu = "2"
                        End If
                        lblnPost = mobjPolicySeq.InsPostAM006Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(llngTariff, eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeIllness"), .Form.Item("hddsClient"), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), lstrTypeExclu, mobjValues.StringToType(.QueryString.Item("nCount"), eFunctions.Values.eTypeData.etdDouble, True), Session("dNullDate"), mobjValues.StringToType(.Form.Item("cbeExc_Code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdInteger))
                        mstrQueryString = "&nTariff=" & .QueryString.Item("nTariff") & "&sTypeExclu=" & .Form.Item("hddTypeExclu") & "&sInsured=" & .Form.Item("hddsClient") & "&sOptType_exc=" & .Form.Item("hddOptType_exc")
                    Else
                        lblnPost = True
                    End If
                End With

            '+ VI811: Asegurados por coberturas (Pólizas innominadas)
            Case "VI811"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.inspostVI811(Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGroups"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQLifes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ VI666: Cotización
            Case "VI666"
                mobjPolicySeq = New ePolicy.Cover_quota
                With Request
                    lblnPost = mobjPolicySeq.inspostVI666(.QueryString("WindowType"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModule"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUtilMar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), Session("SessionId"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    mstrQueryString = "&nGroup=" & .QueryString.Item("nGroup")
                End With

            '+ VI662: Datos particulares vida colectivo (Educacional)
            Case "VI662"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        mobjPolicySeq_educ = New ePolicy.life_educ
                        lblnPost = mobjPolicySeq_educ.InsPostVI662(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optTyp"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPre"), .Form.Item("chkUniver"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), Session("sPoliType"), .Form.Item("optNomina"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnCapitalCost"), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"))
                        mobjPolicySeq_educ = Nothing
                    Else
                        If .QueryString.Item("sInBasUni") = "1" Then
                            lstrTyplevels = "1"
                            lintLevels = .Form.Item("tcnLevel_b")
                            lintCapital = .Form.Item("tcnCapital_b")
                            lintInsured = .Form.Item("tcnInsured_b")
                        Else
                            lstrTyplevels = "2"
                            lintLevels = .Form.Item("tcnLevel_u")
                            lintCapital = .Form.Item("tcnCapital_u")
                            lintInsured = .Form.Item("tcnInsured_u")
                        End If
                        mobjPolicySeq_lev = New ePolicy.life_levels
                        lblnPost = mobjPolicySeq_lev.InsPostVI662(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), lstrTyplevels, mobjValues.StringToType(lintLevels, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintCapital, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintInsured, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient_b"), Session("nUsercode"), Session("sPolitype"), Session("sBrancht"))

                        mstrQueryString = "&sInBasUni=" & Request.QueryString.Item("sInBasUni") & "&nGroup=" & mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble) & "&nSituation=" & mobjValues.StringToType(.QueryString.Item("nSituation"), eFunctions.Values.eTypeData.etdDouble) & "&nCapMax=" & mobjValues.StringToType(.QueryString.Item("nCapMax"), eFunctions.Values.eTypeData.etdDouble) & "&sOptTyp=" & .QueryString.Item("sOptTyp") & "&sOptNom=" & .QueryString.Item("sOptNom") & "&nPercent=" & .QueryString.Item("nPercent") & "&nCapCos=" & .QueryString.Item("nCapCos") & "&sChkPre=" & .QueryString.Item("sChkPre") & "&sChkUni=" & .QueryString.Item("sChkUni") & "&nCurrency=" & mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
                        mobjPolicySeq_lev = Nothing
                    End If
                End With

            '+ VA595: Ilustración del valor póliz
            Case "VA595"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Per_deposit
                        lblnPost = mobjPolicySeq.InsPostVA595Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), Session("nUsercode"), Session("nTransaction"))

                        mstrQueryString = "&nCurrency=" & .Form.Item("hddnCurrency") & "&nPremiumbas=" & .Form.Item("hddnPremAnu") & "&nPremimin=" & .Form.Item("hddnPremimin") & "&nVpprdeal=" & .Form.Item("hddnVPprdeal") & "&nPremfreq=" & .Form.Item("hddnPremfreq") & "&nPremdeal=" & .Form.Item("hddnPremdeal") & "&nPrsugest=" & .Form.Item("hddnPrsugest") & "&nVpprsug=" & .Form.Item("hddnVPprsug") & "&nAmountcontr=" & .Form.Item("hddnPremdep") & "&nIntproject=" & .Form.Item("hddnIntproject") & "&nWarminint=" & .Form.Item("hddnWarminint") & "&sInscalpre=" & .Form.Item("hddsIndCalPre") & "&nRatepayf=" & .Form.Item("hddnRatepayf") & "&nInsurtime" & .Form.Item("hddnInsurtime")
                    Else
                        '+Se setea en el supuesto caso que el sistema envíe una advertencia
                        mobjPolicySeq = New ePolicy.Projectlife
                        lblnPost = mobjPolicySeq.InsPostVA595(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("dNulldate"), Session("nUsercode"), Session("SessionId"), Session("nTransaction"), mobjValues.StringToType(.Form.Item("hddnPremAnu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnVPprdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPrsugest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnVPprsug"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsIndCalPre"), .Form.Item("hddsProcessed"), .Form.Item("hddsPremdeal"), .Form.Item("hddsPremdeal_Chan"), mobjValues.StringToType(.Form.Item("hddnInsurtime"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ RV778: Datos particulares de rentas vitalicias
            Case "RV778"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        mobjPolicySeq = New ePolicy.Annuities
                        lblnPost = mobjPolicySeq.insPostRV778(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnPremiumbas"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
                        mstrQueryString = "&nPremiumbas=" & .Form.Item("tcnPremiumbas")
                    Else
                        mobjPolicySeq = New ePolicy.Prem_annuities
                        lblnPost = mobjPolicySeq.insPostRV778Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("hddnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIndrecdep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_quot"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_disc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNom_valbon"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdIssuedatbon"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdatbon"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCount"), eFunctions.Values.eTypeData.etdDouble, True))
                        mstrQueryString = "&nPremiumbas=" & .QueryString.Item("nPremiumbas")
                    End If
                End With

            '+ OS001: Solicitud de ordenes de servicio
            Case "OS001", "OS001_K"
                With Request
                    mstrQueryString = "&nOrdClass=" & Request.Form.Item("hddnOrdClass") & "&nBranch=" & Request.Form.Item("hddnBranch") & "&nProduct=" & Request.Form.Item("hddnProduct") & "&nPolicy=" & Request.Form.Item("hddnPolicy") & "&nProponum=" & Request.Form.Item("hddnProponum") & "&nCertif=" & Request.Form.Item("hddnCertif") & "&nClaim=" & Request.Form.Item("hddnClaim") & "&nCase_num=" & Request.Form.Item("hddnCase_num") & "&sCodisplOri=" & Request.Form.Item("hddsCodisplOri") & "&sBrancht=" & Request.Form.Item("hddsBrancht") & "&nDeman_type=" & Request.Form.Item("hddnDeman_type")

                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostOS001Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnOrdClass"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeProvider"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFec_prog"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctTime_prog"), .Form.Item("tctPlace"), mobjValues.StringToType(.Form.Item("cbeWorksh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctName_cont"), .Form.Item("tctAdd_contact"), .Form.Item("tctPhone_cont"), mobjValues.StringToType(.Form.Item("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdMade_date"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctMade_time"))
                    Else
                        '+ Si se trata de la ventana llamada desde la secuencia (OS001)
                        If Request.QueryString.Item("sCodispl") = "OS001" Then
                            If mobjValues.StringToType(Request.Form.Item("hddnItems"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                                lstrContent = "2"
                            Else
                                lstrContent = "1"
                            End If
                            lclsPolicy_Win = New ePolicy.Policy_Win
                            Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "OS001", lstrContent)
                            lstrContent = Nothing
                        End If
                    End If
                End With

            '**+ CA829: Resumen de coberturas
            '+ CA829: Resumen de coberturas.

            Case "CA829"
                lblnPost = True

            '**+ CA830: Certificado de coberturas
            '+ CA830: Certificado de coberturas
            Case "CA830"
                lblnPost = mobjPolicySeq.insPostCA830(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCoverageCertificate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

            '**+ VI7003: Savings Plan.
            '+ VI7003: Plan de Ahorros.

            Case "VI7003"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Per_deposit
                        Call mobjPolicySeq.insPostVI7003Upd(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountdep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '**+ VI7005: Transference information.
            '+ VI7005: Información de transferencia. 

            Case "VI7005"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        Call mobjPolicySeq.insPostVI7005Upd(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nType_transf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_peso"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_UF"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_Profit"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ VI849: Criterios de selección de riesgo (Asegurado).
            Case "VI849"
                lblnPost = True
            '+ CA851: Vía de Pago
            Case "CA851"
                mobjPolicySeq = New ePolicy.ValPolicySeq
                With Request
                    lblnPost = True
                    lblnPost = mobjPolicySeq.insPostCA851(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAFPCommi"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optDirTyp"), mobjValues.StringToType(.Form.Item("tcnBillDay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hhDirTyp"), mobjValues.StringToType(.Form.Item("valCollector"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            '+ CA960: Límite por prestaciones.
            Case "CA960"
                mobjPolicySeq = New ePolicy.Franchise
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA960(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcnFixAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcsFrancApl"), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSeq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDed_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLevel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnModulec"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcnGroup"))

                    End With
                Else
                    lblnPost = True
                End If
                mobjPolicySeq = Nothing

            '+ CA100: prestaciones en convenio
            Case "CA100"
                mobjPolicySeq = New eProduct.Lend_Agree_Pres
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA100(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), Session("npolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcnPay_Concep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCod_Agree"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdLong))

                        mstrQueryString = "&nGroup=" & Request.Form.Item("valGroup")

                    End With
                Else
                    lblnPost = True
                End If
                mobjPolicySeq = Nothing

            '+ CA659: Secciones reporte automático para póliza
            Case "CA659"
                lblnPost = True
                lclsPolicy_Win = New ePolicy.Policy_Win
                Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA659", "2")
                lclsPolicy_Win = Nothing

            '+ VI8000: Valores garantizados
            Case "VI8000"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjPolicySeq = New ePolicy.Guar_Saving_Pol

                    lblnPost = mobjPolicySeq.insPostVI8000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("hddnGuarSavid"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cboGuarSav_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdStart_GuarSav"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_GuarSav_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnGuarSav_value"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeGuarSav_stat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRen_guarSav"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("hddnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPay"), Request.QueryString.Item("Action"))

                    mobjPolicySeq = Nothing
                Else
                    lblnPost = True
                End If

            '+ VI006A: Fondos de inversiones por póliza matríz.
            Case "VI006A"

                'UPGRADE_NOTE: The 'ePolicy.Funds_CO_P' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lclsFunds_CO_P = Server.CreateObject("ePolicy.Funds_CO_P")

                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If mobjValues.StringToType(.Form.Item("chkActivFound"), eFunctions.Values.eTypeData.etdBoolean) Then
                            'If CBool(.Form.Item("chkActivFound")) Then
                            sActivefound_P = "1"
                        Else
                            sActivefound_P = "2"
                        End If

                        Call lclsFunds_CO_P.insPostVI006A(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), sActivefound_P, "2", mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
                    Else
                        lblnPost = True

                        lclsPolicy_Win = New ePolicy.Policy_Win

                        Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "2")
                    End If

                    lclsFunds_CO_P = Nothing
                    lclsPolicy_Win = Nothing

                End With

            '+ VI8001: Prima base
            Case "VI8001"
                lblnPost = True

                lclsPolicy_Win = New ePolicy.Policy_Win

                Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI8001", "2")

            '+VI8002:Ahorro Previsional

            Case "VI8002"
                mobjPolicySeq = New ePolicy.Apv_origin
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostVI8002Upd(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremDeal_anu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremDeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nUserCode"))

                        mstrQueryString = "&nDepend=" & Request.Form.Item("hddnDepend") & "&nIndep=" & Request.Form.Item("hddnIndep") & "&dDate_work=" & Request.Form.Item("hdddtcdDate_origi") & "&nAct_date=" & Request.Form.Item("hdddtnAct_date") & "&sClient=" & Request.Form.Item("hddsClient") & "&nAFP=" & Request.Form.Item("hddnAFP") & "&hAFP=" & Request.Form.Item("hddhAFP") & "&nOption=" & Request.Form.Item("hddnOption") & "&nTaxRegime=" & Request.Form.Item("hddnTaxRegime") & "&nCapital=" & Request.Form.Item("hddnCapital") & "&nYearMonth=" & Request.Form.Item("hddnYearMonth") & "&sFolio=" & Request.Form.Item("hddsFolio")
                    Else
                        lintAfp = mobjValues.StringToType(.Form.Item("hddAfp"), eFunctions.Values.eTypeData.etdInteger, True)
                        If mobjValues.StringToType(.Form.Item("cbeAfp"), eFunctions.Values.eTypeData.etdInteger) > 0 Then
                            lintAfp = mobjValues.StringToType(.Form.Item("cbeAfp"), eFunctions.Values.eTypeData.etdInteger, True)
                        End If
                        lblnPost = mobjPolicySeq.insPostVI8002(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valTyp_ProfitWorker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddYearmonth"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient"), mobjValues.StringToType(.Form.Item("hddAct_date"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdDate_origi"), eFunctions.Values.eTypeData.etdDate), lintAfp, Session("nUserCode"), .Form.Item("tctFolio"))
                    End If
                End With

            '+ AP004: Clasificación de riesgos AP
            Case "AP004"
                mobjPolicySeq = New ePolicy.Class_ap
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.insPostAP004Upd(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeClass_ap"), eFunctions.Values.eTypeData.etdLong), Session("nUserCode"))
                    Else
                        lblnPost = mobjPolicySeq.insPostAP004(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUserCode"))
                    End If
                End With
            Case "CT001"
                mobjPolicySeq = New ePolicy.Credit
                With Request
                    lblnPost = mobjPolicySeq.insPostCT001(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnLimitRequest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimitCurrent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercentPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMateria"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeClassClient"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeAjustType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimitNoPayroll"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble))
                End With


            '+Declaración de las ventas mensuales de los seguros de crédito            
            Case "CT002"
                mobjPolicySeq = New ePolicy.CreditSales
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        lblnPost = mobjPolicySeq.insPostCT002(1, Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDocdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeDocType"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctNumDoc"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), mobjValues.StringToType(.Form.Item("tcdExpirdoc"), eFunctions.Values.eTypeData.etdDate))


                    Else
                        lblnPost = True
                    End If
                End With

            '+Actualizacion de la ventana de datos particulares de garantia

            Case "WT001"
                mobjPolicySeq = New ePolicy.Warranty
                With Request
                    lblnPost = mobjPolicySeq.insPostWT001(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctProjectName"), .Form.Item("tctIndentify"))
                End With

            Case "WT002"
                mobjPolicySeq = New ePolicy.WarrantyQuotes
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        lblnPost = mobjPolicySeq.insPostWT002(1, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnQuote"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), 0, 0, .Form.Item("tctComment"), Session("nUsercode"))

                    Else
                        lblnPost = True
                    End If
                End With

            '+RO001: Datos Particulares de Robo
            Case "RO001"
                mobjPolicySeq = New ePolicy.Theft
                With Request
                    lblnPost = True
                    lblnPost = mobjPolicySeq.InsPostRO001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnEmployees"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnVigilance"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeBusinessTy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCommerGrp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctDescBussi"), mobjValues.StringToType(.Form.Item("valConstCat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))

                End With

            '*+TR001: Particular information on transport
            '+TR001: Información particular de Transporte
            Case "TR001"
                mobjPolicySeq = New ePolicy.transport
                With Request
                    insPostSequence = mobjPolicySeq.InsPostTR001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnMaxLimTrip"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDep_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenDecla_freq"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnEstAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnOverLine"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeModalitySumins"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnDep_prem"), eFunctions.Values.eTypeData.etdDouble, True), Session("sPolitype"), mobjValues.StringToType(.Form.Item("tcnRate_Apply"), eFunctions.Values.eTypeData.etdDouble))
                    Session("nLimitCapital") = mobjValues.StringToType(.Form.Item("tcnMaxLimTrip"), eFunctions.Values.eTypeData.etdDouble, True)
                End With

            '*+TR002: Covered routes
            '+TR002: Rutas aseguradas
            Case "TR002"
                mobjPolicySeq = New ePolicy.tran_route
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insPostSequence = mobjPolicySeq.InsPostTR002(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnRoute"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeTypRoute"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeTranspType"), eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

            '*+ TR003: Shipped merchandise
            '+ TR003: Mercancías transportadas
            Case "TR003"
                mobjPolicySeq = New ePolicy.Tran_merch
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insPostSequence = mobjPolicySeq.InsPostTR003(CDbl(.QueryString.Item("nZone")) = 1,
                                                                     .QueryString("sCodispl"),
                                                                     mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger),
                                                                     .QueryString("Action"),
                                                                     mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger),
                                                                     Session("sCertype"),
                                                                     Session("nBranch"),
                                                                     Session("nProduct"),
                                                                     Session("nPolicy"),
                                                                     Session("nCertif"),
                                                                     Session("dEffecdate"),
                                                                     mobjValues.StringToType(.Form.Item("cbeClassMerch"), eFunctions.Values.eTypeData.etdInteger),
                                                                     mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger),
                                                                     .Form.Item("tctDescript"),
                                                                     mobjValues.StringToType(.Form.Item("tcnQuanTrans"), eFunctions.Values.eTypeData.etdInteger),
                                                                     mobjValues.StringToType(.Form.Item("cbeUnit"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                     mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble),
                                                                     .Form.Item("cbeFranDedi"),
                                                                     mobjValues.StringToType(.Form.Item("tcnFranDedRate"), eFunctions.Values.eTypeData.etdDouble),
                                                                     .Form.Item("tcnMinAmount"),
                                                                     .QueryString("nCurrency"))

                    End If
                End With

            '*+TR004: Transportation modes
            '+TR004: Medios de transporte
            Case "TR004"
                mobjPolicySeq = New ePolicy.tran_way
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = True
                        lblnPost = mobjPolicySeq.InsPostTR004(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnWay"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctName_licen"), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong))
                    End If
                End With
            '*+TR6000: Rate and Deductibles for transport of merchandise
            '+TR6000: Tasas y deducibles para mercancías de transporte
            Case "TR6000"
                mobjPolicySeq = New ePolicy.Tran_rate
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = True
                        lblnPost = mobjPolicySeq.InsPostTR6000(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeClassmerch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnLimit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmo_deduc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeType"))
                    Else
                        lblnPost = True
                    End If
                End With
            '*+TR009: Itinerario de transporte
            '+TR009: Transport Itineraries
            Case "TR009"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If CDbl(.QueryString.Item("nInd")) = 1 Then
                            If CStr(Session("sPolitype")) = "1" Then
                                mobjPolicySeq = New ePolicy.tran_route
                                lblnPost = mobjPolicySeq.InsPostTR002(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), eRemoteDB.Constants.intNull, .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnStage"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valRoute"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("valTransport"), eFunctions.Values.eTypeData.etdInteger))

                                mobjPolicySeq = New ePolicy.Tran_stage
                                lblnPost = mobjPolicySeq.InsPostTR009_Itin(.QueryString("Action"), Session("sPolitype"), Session("nUsercode"), .QueryString("nCurrency"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tcnStage"), Session("dEffecdate"), IIf(.Form.Item("tcdDestindat") = "", eRemoteDB.dtmNull, .Form.Item("tcdDestindat")), IIf(.Form.Item("tcdOrigindat") = "", eRemoteDB.dtmNull, .Form.Item("tcdOrigindat")), .Form.Item("tcnStage"), .Form.Item("tctOrigen"), .Form.Item("tctDestiny"), .Form.Item("tctName"), .Form.Item("tctPurchase_Order"), .Form.Item("tctApplicationNumber"))
                            Else
                                mobjPolicySeq = New ePolicy.Tran_stage
                                lblnPost = mobjPolicySeq.InsPostTR009_Itin(.QueryString("Action"), Session("sPolitype"), Session("nUsercode"), .QueryString("nCurrency"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tcnStage"), Session("dEffecdate"), IIf(.Form.Item("tcdDestindat") = "", eRemoteDB.dtmNull, .Form.Item("tcdDestindat")), IIf(.Form.Item("tcdOrigindat") = "", eRemoteDB.dtmNull, .Form.Item("tcdOrigindat")), .Form.Item("valTransport"), .Form.Item("tctOrigen"), .Form.Item("tctDestiny"), .Form.Item("tctName"), .Form.Item("tctPurchase_Order"), .Form.Item("tctApplicationNumber"))
                            End If
                        Else
                            If CStr(Session("sPolitype")) = "1" Then
                                mobjPolicySeq = New ePolicy.Tran_rate
                                lblnPost = mobjPolicySeq.InsPostTR6000(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valClass"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valPacking"), eFunctions.Values.eTypeData.etdLong), 0, 0, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "1")

                                mobjPolicySeq = New ePolicy.Tran_stagedet
                                lblnPost = mobjPolicySeq.InsPostTR009_Merch(.QueryString("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tcnMerchandise"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valClass"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valPacking"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFrandedi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuantran"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeUnit"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnMerchRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCostUnit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnImagenum"), eFunctions.Values.eTypeData.etdDouble))

                            Else
                                mobjPolicySeq = New ePolicy.Tran_stagedet
                                lblnPost = mobjPolicySeq.InsPostTR009_Merch(.QueryString("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("tcnMerchandise"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valClass"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valPacking"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFrandedi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuantran"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeUnit"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnMerchRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCostUnit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnImagenum"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                        End If
                        mstrQueryString = "&nCurrency=" & .QueryString.Item("nCurrency") & "&nStageDet=" & .Form.Item("tcnMerchandise") & "&nIndexItin=" & .QueryString.Item("nIndexItin") & "&sStage_Merch=" & .QueryString.Item("sStage_Merch") & "&sMerchandise=" & .QueryString.Item("sMerchandise")
                    Else
                        lblnPost = True
                    End If
                End With

            '+ SH001: Datos particulares de Maritimo Cascos
            Case "SH001"
                With Request
                    mobjPolicySeq = New ePolicy.Ship
                    lblnPost = mobjPolicySeq.insPostSH001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeShipUse"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctName"), .Form.Item("tctRegist"), mobjValues.StringToType(.Form.Item("valMaterial"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("cbeShipType"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctConstructor"), mobjValues.StringToType(.Form.Item("tcnConsYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnEquivYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdLastCareDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctLastCarePlace"), mobjValues.StringToType(.Form.Item("tcnDepth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLength"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWaters"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumMotors"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctModelMotors"), .Form.Item("tctSerialMotors"), mobjValues.StringToType(.Form.Item("tcnPower"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTRB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTRN"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapacity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeUnitMesureCode"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctSeaPort"), .Form.Item("tctDotation"), .Form.Item("tctActionZone"))
                End With

            '+HO001: Datos Particulares de Hogar
            Case "HO001"
                mobjPolicySeq = New ePolicy.HomeOwner
                With Request
                    lblnPost = True
                    lblnPost = mobjPolicySeq.InsPostHO001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeDwellingType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeOwnerShip"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnYear_built"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkCov_purc"), mobjValues.StringToType(.Form.Item("tcnPrice_purch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency_purch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdDate_purch"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkPolicy_other"), mobjValues.StringToType(.Form.Item("tcnCap_other"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency_other"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdExpir_other"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeExterConstr"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("tctOther_constr"), mobjValues.StringToType(.Form.Item("tcnStories"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeRoofType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRoofYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnHomeSuper"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnLandSuper"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnGarage"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnFirePlace"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnBedrooms"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnFullBath"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnHalfBath"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeAirType"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeAlt_heating"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkGas"), .Form.Item("chkSprinkSys"), .Form.Item("tctAlarm_comp"), mobjValues.StringToType(.Form.Item("tcnDist_Hydr"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkNon_smok"), mobjValues.StringToType(.Form.Item("tcnDist_fire"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctFireDepart"), mobjValues.StringToType(.Form.Item("cbeFloodZone"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeSeismicZone"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkFloodInd"), mobjValues.StringToType(.Form.Item("cbeSwimPool"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkFencePool"), mobjValues.StringToType(.Form.Item("tcnFenceHeight"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chkTrampoline"), .Form.Item("chkAnimalsInd"), .Form.Item("tctAnimalsDes"), .Form.Item("chkAttackedInd"), mobjValues.StringToType(.Form.Item("cbeFoundType"), eFunctions.Values.eTypeData.etdInteger))

                End With

            '+ RM001: Datos particulares de Rotura de Maquinaria
            Case "RM001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If Trim(.Form.Item("tcnFabYear")) = "" Then
                            liFabYear = 0
                        Else
                            liFabYear = .Form.Item("tcnFabYear")
                        End If
                        mobjPolicySeq = New ePolicy.Detail_Machine
                        lblnPost = mobjPolicySeq.insPostDetail_Machine(.QueryString("sCodispl"), .QueryString("nMainAction"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valMachineCode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(liFabYear, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnQuantityMachine"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"))
                    Else
                        mobjPolicySeq = New ePolicy.Machine
                        lblnPost = mobjPolicySeq.InsPostRM001(.QueryString("sCodispl"), .QueryString("nMainAction"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                    End If
                End With
                mobjPolicySeq = Nothing

            '+RC001: Información particular de Resp. Civil
            Case "RC001"
                mobjPolicySeq = New ePolicy.Civil
                With Request
                    lblnPost = True
                    lblnPost = mobjPolicySeq.InsPostRC001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeUnit_type"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnUnit_quan"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbeBusinessTy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCommerGrp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctDescBussi"), mobjValues.StringToType(.Form.Item("valConstCat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))

                End With

            Case "VI7500"
                lblnPost = True

                mobjsAapv = New eSaapv.Saapv
                lblnPost = mobjsAapv.Upd_policy(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), mobjValues.StringToType(Request.Form.Item("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble), CDate(Session("dEffecdate")), mobjValues.StringToType(Request.Form.Item("hddInstitution"), eFunctions.Values.eTypeData.etdLong))

                mobjsAapv = Nothing

            '+CC001: Crédito y Caución
            Case "CC001"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjPolicySeq = New ePolicy.Warranty
                        lblnPost = True
                        lblnPost = mobjPolicySeq.InsPostCC001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tctWarrnumber"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valTypewarranty"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctDocwarranty"), mobjValues.StringToType(.Form.Item("valCurrency_wrr"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapacity"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdLong), Session("nTransaction"), mobjValues.StringToType(.Form.Item("tcdMaturity"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctClieName"), mobjValues.StringToType(.Form.Item("valStatusbond"), eFunctions.Values.eTypeData.etdLong))
                        mstrQueryString = "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&valinsmodality=" & Request.QueryString.Item("nInsmodality") & "&tcnguar_type=" & Request.QueryString.Item("nGuar_type") & "&tctcontracnum=" & Request.QueryString.Item("sContracnum") & "&tcdcontracdat=" & Request.QueryString.Item("dContracdat") & "&tcntime_eject=" & Request.QueryString.Item("nTime_eject") & "&tcncredcau=" & Request.QueryString.Item("nCredcau") & "&cbeCurrency=" & Request.QueryString.Item("nCurrency") & "&tcnindemper=" & Request.QueryString.Item("nIndemper") & "&tcnmoraallow=" & Request.QueryString.Item("nMoraallow") & "&tcntransmon1=" & Request.QueryString.Item("nTransmon1") & "&tcntransmon2=" & Request.QueryString.Item("nTransmon2") & "&tcnindper1=" & Request.QueryString.Item("nIndper1") & "&tcnindper2=" & Request.QueryString.Item("nIndper2") & "&mblnReloadPage=True"
                    Else
                        mobjPolicySeq = New ePolicy.Credit
                        lblnPost = True
                        lblnPost = mobjPolicySeq.InsPostCC001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valinsmodality"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnguar_type"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctcontracnum"), mobjValues.StringToType(.Form.Item("tcdcontracdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valtime_unit"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdterm_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcntime_eject"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcncredcau"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valcurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnindemper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnmoraallow"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcntransmon1"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcntransmon2"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnindper1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnindper2"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksFollowUp"), .Form.Item("tctContractObject"), mobjValues.StringToType(.Form.Item("valStatusbond"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("chksInsurSector"))
                    End If
                End With

            Case "GI1408"
                Dim mobjInterface As Object
                Dim lcolfieldsheet As Object
                Dim lclsfieldsheet As Object
                mobjInterface = New eInterface.ValInterfaceSeq
                lcolfieldsheet = New eInterface.FieldSheets
                lclsfieldsheet = New eInterface.FieldSheet
                If lcolfieldsheet.Find2(mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("3", eFunctions.Values.eTypeData.etdDouble)) Then
                    For Each lclsfieldsheet In lcolfieldsheet
                        '+ LLamada por cada campo dinamico para almacenar datos para parametros
                        If lclsfieldsheet.nObjtype <> 8 Then
                            lblnPost = mobjInterface.InsPostGI1408(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nSheet"), Session("dEffecdate"), lclsfieldsheet.nfield, lclsfieldsheet.nDataType, Request.Form.Item(lclsfieldsheet.sColumnName), Session("nusercode"), Session("sCodispl"))
                        Else
                            lblnPost = mobjInterface.InsPostGI1408(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nSheet"), Session("dEffecdate"), lclsfieldsheet.nfield, lclsfieldsheet.nDataType, Request.Form.Item(lclsfieldsheet.sColumnName & "hdd"), Session("nusercode"), Session("sCodispl"))
                        End If
                    Next lclsfieldsheet
                End If
                mobjInterface = Nothing

            '+ CA635: Condiciones de prima
            Case "CA635"
                mobjPolicySeq = New ePolicy.Cond_cover_premium
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostCA635(.QueryString("Action"),
                                                              Session("sCertype"),
                                                              Session("nBranch"),
                                                              Session("nProduct"),
                                                              Session("nPolicy"),
                                                              mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                              Session("nCertif"),
                                                              mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                              mobjValues.StringToType(.Form.Item("cbeTipPrem"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("tcnCapital_min"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("tcnCapital_max"), eFunctions.Values.eTypeData.etdDouble),
                                                              mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble),
                                                              .Form.Item("cbeRoutine"),
                                                              mobjValues.StringToType(.Form.Item("valId_table"), eFunctions.Values.eTypeData.etdInteger),
                                                              mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                              Session("nUsercode"),
                                                              mobjValues.StringToType(.Form.Item("nId"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    With Request
                        If Not mobjValues.StringToType(.Form.Item("hddbCopiar"), eFunctions.Values.eTypeData.etdBoolean) Then
                            lblnPost = True
                        Else
                            lblnPost = mobjPolicySeq.insPostCA635Copy(Session("sCertype"),
                                                                      Session("nBranch"),
                                                                      Session("nProduct"),
                                                                      Session("nPolicy"),
                                                                      Session("nCertif"),
                                                                      mobjValues.StringToType(.Form.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                      Session("nUsercode"))
                            If Not lblnPost Then
                                lblnPost = True
                            End If
                        End If

                        '+ Si se efectúa la actualización puntual se recarga la página. 
                        If CBool(IIf(IsNothing(Request.Form.Item("hddbPuntual")), False, Request.Form.Item("hddbPuntual"))) Then
                            lclsErrors = New eFunctions.Errors
                            '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.10 
                            lclsErrors.sSessionID = Session.SessionID
                            lclsErrors.nUsercode = Session("nUsercode")
                            '~End Body Block VisualTimer Utility 
                            '+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla 

                            Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881, , , , True))

                            lclsErrors = Nothing

                            Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                            Response.Write("try {")
                            Response.Write("window.close();top.frames['fraFolder'].document.location.href=top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")
                            Response.Write("}")
                            Response.Write("catch(error){")
                            Response.Write("window.close();opener.top.frames['fraFolder'].document.location.href=opener.top.frames['fraFolder'].document.location.href.replace(/&sDelTCover=.*/,'') + '&sDelTCover=' ")
                            Response.Write("}")
                            Response.Write("</" & "Script>")

                            lblnPost = False
                        End If
                    End With
                End If
                mobjPolicySeq = Nothing
            Case "MU700"
                If Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipElect" Then
                    Dim mobjPolicySeq As New ePolicy.ValPolicySeq_MU700
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                 sGridName:=Request.QueryString("GridName"),
                                                                 sCerType:=Session("sCerType"),
                                                                 nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                 nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                 nType:=3,
                                                                 dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                 nUserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nConsec:=mobjValues.StringToType(.Form.Item("nConsec_EquipElect"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nElement_Type:=mobjValues.StringToType(.Form.Item("NTYPE_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 nSection:=mobjValues.StringToType(.Form.Item("NSECTION_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 sDescription:= .Form.Item("SDESCRIPTION_EquipElect"),
                                                                 nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 nRate:=mobjValues.StringToType(.Form.Item("NRATE_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True))





                    End With
                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "RotMaqui" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_RotMaqui"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nConsec:=mobjValues.StringToType(.Form.Item("nConsec_RotMaqui"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sTradeMark:= .Form.Item("STRADEMARK_RotMaqui"),
                                                                    sModel:= .Form.Item("SMODEL_RotMaqui"),
                                                                    nYear:=mobjValues.StringToType(.Form.Item("NYEAR_RotMaqui"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sOrigin:= .Form.Item("SORIGIN_RotMaqui"),
                                                                    sSerialNumber:= .Form.Item("SSERIALNUMBER_RotMaqui"),
                                                                    nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nRate:=mobjValues.StringToType(.Form.Item("NRATE_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True), nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With

                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipMaquiContr" Then

                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nConsec:=mobjValues.StringToType(.Form.Item("nConsec_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sTradeMark:= .Form.Item("STRADEMARK_EquipMaquiContr"),
                                                                    sModel:= .Form.Item("SMODEL_EquipMaquiContr"),
                                                                    nYear:=mobjValues.StringToType(.Form.Item("NYEAR_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sOrigin:= .Form.Item("SORIGIN_EquipMaquiContr"),
                                                                    sSerialNumber:= .Form.Item("SSERIALNUMBER_EquipMaquiContr"),
                                                                    nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nRate:=mobjValues.StringToType(.Form.Item("NRATE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True), nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With


                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "Fidelity" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sclient:= .Form.Item("sClient_Fidelity"),
                                                                    sDigit:= .Form.Item("SCLIENT_Fidelity_Digit"),
                                                                    sFirstName:= .Form.Item("sFirstName_Fidelity"),
                                                                    sMiddel_Name:= .Form.Item("sMiddleName_Fidelity"),
                                                                    sLastName:= .Form.Item("sLastName_Fidelity"),
                                                                    sLastName2:= .Form.Item("sLastName2_Fidelity"),
                                                                    nPosition:=mobjValues.StringToType(.Form.Item("NPOSITION_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    nSalary:=mobjValues.StringToType(.Form.Item("NSALARY_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nFactor:=mobjValues.StringToType(.Form.Item("NFACTOR_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nValue:=mobjValues.StringToType(.Form.Item("NVALUE_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True))

                    End With
                Else
                    mobjCertificat = New ePolicy.Certificat
                    If mobjCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then

                        With Request
                            lblnPost = mobjPolicySeq.insPostMU700(sCertype:=Session("sCerType"),
                                                                  nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                  nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                  dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                  nCapital:=mobjCertificat.nCapital,
                                                                  dExpirDat:=mobjCertificat.dExpirdat,
                                                                  dIssueDat:=mobjCertificat.dIssuedat,
                                                                  nNullCode:=0,
                                                                  dNullDate:=Date.MinValue,
                                                                  nPremium:=mobjCertificat.nPremium,
                                                                  dStartDate:=mobjCertificat.dStartdate,
                                                                  nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nTransactio:=mobjCertificat.nTransactio,
                                                                  nSituation:=mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nGroup:=mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  sClient:=mobjCertificat.sClient,
                                                                  nConstCat:=mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nCodKind:=mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nPayFreq:=mobjCertificat.nPayfreq,
                                                                  nSismicZone:=mobjValues.StringToType(.Form.Item("cbeSismicZone"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nFi_PolicyType:=mobjValues.StringToType(.Form.Item("tcnFi_PolicyType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nInsurType:=mobjValues.StringToType(.Form.Item("tcnInsurType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nNumberOfEmployees:=mobjValues.StringToType(.Form.Item("tcnNumberOfEmployees"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nInsured:=mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nTheftCapital:=mobjValues.StringToType(.Form.Item("tcnTheftCapital"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nSecurityMen:=mobjValues.StringToType(.Form.Item("tcnSecurityMen"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nArea:=mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  sInd_Fidelity:= .Form.Item("sInd_Fidelity"),
                                                                  sInd_Electronic:= .Form.Item("sInd_Electronic"),
                                                                  sInd_Machine:= .Form.Item("sInd_Machine"),
                                                                  sInd_Contractor:= .Form.Item("sInd_Contractor"),
                                                                  nMoney_Transit:=mobjValues.StringToType(.Form.Item("tcnMoney_Transit"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nMoney_Permanence:=mobjValues.StringToType(.Form.Item("tcnMoney_Permanence"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  narticle:=mobjValues.StringToType(.Form.Item("cboArticle"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  sriskdescription:= .Form.Item("sriskdescription")
                                                                  )


                            Session("NMONEY_TRANSIT") = Nothing
                            Session("NMONEY_PERMANENCE") = Nothing
                            Session("NCONSCAT") = Nothing
                            Session("NSISMICZONE") = Nothing
                            Session("NINSURED") = Nothing
                            Session("NTHEFTCAPITAL") = Nothing
                            Session("NSECURITYMEN") = Nothing
                            Session("NAREA") = Nothing
                            Session("POLICYTYPE") = Nothing
                            Session("NNUMBEROFEMPLOYEES") = Nothing
                            Session("NINSURTYPE") = Nothing

                        End With
                    End If
                    mobjCertificat = Nothing
                End If
                mobjPolicySeq = Nothing
            Case "AV001"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    lblnPost = mobjPolicyseqAviat_Marit.InsPostAV001_SH010("AV001", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnTakeoff_maxwei"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAirportbase"), .Form.Item("tctGeographical"), mobjValues.StringToType(.Form.Item("cbeUse"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSeatnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCrewnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPassengersnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNibranumber"), eFunctions.Values.eTypeData.etdDouble), , .Form.Item("tctSeries"), .Form.Item("tctOrigin"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            Case "SH010"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    lblnPost = mobjPolicyseqAviat_Marit.InsPostAV001_SH010("SH010", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), mobjValues.StringToType(.Form.Item("tctYear"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), Session("nUsercode"), , , , , , , , , .Form.Item("tctName"), .Form.Item("tctSeries"), .Form.Item("tctOrigin"), .Form.Item("tctNavigationcertificate"), mobjValues.StringToType(.Form.Item("tcnQualificationship"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPortdeparture"), .Form.Item("tctPortarrival"), .Form.Item("tctDimensions"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
                mobjPolicySeq = Nothing

            Case "CM001"
                mobjPolicySeq = New ePolicy.TRCM
                With Request
                    lblnPost = mobjPolicySeq.InsPostCM001(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form.Item("cbeTypeWork"), eFunctions.Values.eTypeData.etdInteger),
                                                          .Form.Item("tctWorkname"),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_work"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_work"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_m"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_m"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_em"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_em"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger),
                                                          Session("nUsercode"))
                End With
                mobjPolicySeq = Nothing

            Case "CA069"
                With Request
                    Dim lclsCertificat = New ePolicy.Certificat

                    lclsCertificat.Update_sRecType(Session("sCertype"),
                                                    Session("nBranch"),
                                                    Session("nProduct"),
                                                    Session("nPolicy"),
                                                    Session("nCertif"),
                                                    .Form("hddsRecType"),
                                                    Session("Deffecdate"),
                                                    Session("nUsercode"))
                End With
            Case "MU700"
                If Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipElect" Then
                    Dim mobjPolicySeq As New ePolicy.ValPolicySeq_MU700
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                 sGridName:=Request.QueryString("GridName"),
                                                                 sCerType:=Session("sCerType"),
                                                                 nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                 nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                 nType:=3,
                                                                 dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                 nUserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nConsec:=mobjValues.StringToType(.Form.Item("nConsec_EquipElect"), eFunctions.Values.eTypeData.etdInteger),
                                                                 nElement_Type:=mobjValues.StringToType(.Form.Item("NTYPE_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 nSection:=mobjValues.StringToType(.Form.Item("NSECTION_EquipElect"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                 sDescription:= .Form.Item("SDESCRIPTION_EquipElect"),
                                                                 nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 nRate:=mobjValues.StringToType(.Form.Item("NRATE_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                 nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_EquipElect"), eFunctions.Values.eTypeData.etdDouble, True))





                    End With
                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "RotMaqui" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_RotMaqui"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nConsec:=mobjValues.StringToType(.Form.Item("nConsec_RotMaqui"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sTradeMark:= .Form.Item("STRADEMARK_RotMaqui"),
                                                                    sModel:= .Form.Item("SMODEL_RotMaqui"),
                                                                    nYear:=mobjValues.StringToType(.Form.Item("NYEAR_RotMaqui"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sOrigin:= .Form.Item("SORIGIN_RotMaqui"),
                                                                    sSerialNumber:= .Form.Item("SSERIALNUMBER_RotMaqui"),
                                                                    nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nRate:=mobjValues.StringToType(.Form.Item("NRATE_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True), nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_RotMaqui"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With

                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "EquipMaquiContr" Then

                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nConsec:=mobjValues.StringToType(.Form.Item("nConsec_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sTradeMark:= .Form.Item("STRADEMARK_EquipMaquiContr"),
                                                                    sModel:= .Form.Item("SMODEL_EquipMaquiContr"),
                                                                    nYear:=mobjValues.StringToType(.Form.Item("NYEAR_EquipMaquiContr"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sOrigin:= .Form.Item("SORIGIN_EquipMaquiContr"),
                                                                    sSerialNumber:= .Form.Item("SSERIALNUMBER_EquipMaquiContr"),
                                                                    nCapital:=mobjValues.StringToType(.Form.Item("NCAPITAL_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nRate:=mobjValues.StringToType(.Form.Item("NRATE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True), nPremium:=mobjValues.StringToType(.Form.Item("NPREMIUM_EquipMaquiContr"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With


                ElseIf Request.QueryString("WindowType") = "PopUp" And Request.QueryString("gridName") = "Fidelity" Then
                    With Request
                        lblnPost = mobjPolicySeq.insPostMU700Upd(sAction:=Request.QueryString("Action"),
                                                                    sGridName:=Request.QueryString("GridName"),
                                                                    sCerType:=Session("sCerType"),
                                                                    nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                    nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                    nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                    nType:=mobjValues.StringToType(.Form.Item("NTYPE_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                    sclient:= .Form.Item("sClient_Fidelity"),
                                                                    sDigit:= .Form.Item("SCLIENT_Fidelity_Digit"),
                                                                    sFirstName:= .Form.Item("sFirstName_Fidelity"),
                                                                    sMiddel_Name:= .Form.Item("sMiddleName_Fidelity"),
                                                                    sLastName:= .Form.Item("sLastName_Fidelity"),
                                                                    sLastName2:= .Form.Item("sLastName2_Fidelity"),
                                                                    nPosition:=mobjValues.StringToType(.Form.Item("NPOSITION_Fidelity"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                    nSalary:=mobjValues.StringToType(.Form.Item("NSALARY_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nFactor:=mobjValues.StringToType(.Form.Item("NFACTOR_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                    nValue:=mobjValues.StringToType(.Form.Item("NVALUE_Fidelity"), eFunctions.Values.eTypeData.etdDouble, True))

                    End With
                Else
                    mobjCertificat = New ePolicy.Certificat
                    If mobjCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then

                        With Request
                            lblnPost = mobjPolicySeq.insPostMU700(sCertype:=Session("sCerType"),
                                                                  nProduct:=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nBranch:=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nPolicy:=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                                                  nCertif:=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                                                  dEffecdate:=mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                  nCapital:=mobjCertificat.nCapital,
                                                                  dExpirDat:=mobjCertificat.dExpirdat,
                                                                  dIssueDat:=mobjCertificat.dIssuedat,
                                                                  nNullCode:=0,
                                                                  dNullDate:=Date.MinValue,
                                                                  nPremium:=mobjCertificat.nPremium,
                                                                  dStartDate:=mobjCertificat.dStartdate,
                                                                  nuserCode:=mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                                                  nTransactio:=mobjCertificat.nTransactio,
                                                                  nSituation:=mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nGroup:=mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  sClient:=mobjCertificat.sClient,
                                                                  nConstCat:=mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nCodKind:=mobjValues.StringToType(.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nPayFreq:=mobjCertificat.nPayfreq,
                                                                  nSismicZone:=mobjValues.StringToType(.Form.Item("cbeSismicZone"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nFi_PolicyType:=mobjValues.StringToType(.Form.Item("tcnFi_PolicyType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nInsurType:=mobjValues.StringToType(.Form.Item("tcnInsurType"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nNumberOfEmployees:=mobjValues.StringToType(.Form.Item("tcnNumberOfEmployees"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nInsured:=mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nTheftCapital:=mobjValues.StringToType(.Form.Item("tcnTheftCapital"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nSecurityMen:=mobjValues.StringToType(.Form.Item("tcnSecurityMen"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  nArea:=mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  sInd_Fidelity:= .Form.Item("sInd_Fidelity"),
                                                                  sInd_Electronic:= .Form.Item("sInd_Electronic"),
                                                                  sInd_Machine:= .Form.Item("sInd_Machine"),
                                                                  sInd_Contractor:= .Form.Item("sInd_Contractor"),
                                                                  nMoney_Transit:=mobjValues.StringToType(.Form.Item("tcnMoney_Transit"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  nMoney_Permanence:=mobjValues.StringToType(.Form.Item("tcnMoney_Permanence"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                  narticle:=mobjValues.StringToType(.Form.Item("cboArticle"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                  sriskdescription:= .Form.Item("sriskdescription")
                                                                  )


                            Session("NMONEY_TRANSIT") = Nothing
                            Session("NMONEY_PERMANENCE") = Nothing
                            Session("NCONSCAT") = Nothing
                            Session("NSISMICZONE") = Nothing
                            Session("NINSURED") = Nothing
                            Session("NTHEFTCAPITAL") = Nothing
                            Session("NSECURITYMEN") = Nothing
                            Session("NAREA") = Nothing
                            Session("POLICYTYPE") = Nothing
                            Session("NNUMBEROFEMPLOYEES") = Nothing
                            Session("NINSURTYPE") = Nothing

                        End With
                    End If
                    mobjCertificat = Nothing
                End If
                mobjPolicySeq = Nothing
            Case "AV001"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    lblnPost = mobjPolicyseqAviat_Marit.InsPostAV001_SH010("AV001", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnTakeoff_maxwei"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAirportbase"), .Form.Item("tctGeographical"), mobjValues.StringToType(.Form.Item("cbeUse"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSeatnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCrewnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPassengersnumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNibranumber"), eFunctions.Values.eTypeData.etdDouble), , .Form.Item("tctSeries"), .Form.Item("tctOrigin"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
            Case "SH010"
                mobjPolicyseqAviat_Marit = New ePolicy.Aviat_marit
                With Request
                    lblnPost = mobjPolicyseqAviat_Marit.InsPostAV001_SH010("SH010", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeParticular"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBrand"), .Form.Item("tctModel"), mobjValues.StringToType(.Form.Item("tctYear"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctRegistrationnumber"), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctAddicionaltext"), Session("nUsercode"), , , , , , , , , .Form.Item("tctName"), .Form.Item("tctSeries"), .Form.Item("tctOrigin"), .Form.Item("tctNavigationcertificate"), mobjValues.StringToType(.Form.Item("tcnQualificationship"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPortdeparture"), .Form.Item("tctPortarrival"), .Form.Item("tctDimensions"))
                End With
                mobjPolicyseqAviat_Marit = Nothing
                mobjPolicySeq = Nothing

            '+ AP010: Accidentes Personales.
            'Case "AP010"
            '    mobjPolicySeq = New ePolicy.AccidentPerson
            '    If Request.QueryString.Item("WindowType") = "PopUp" Then
            '        With Request
            '            lblnPost = mobjPolicySeq.InsPostAP010Upd(.QueryString("Action"), Session("sCertype"), _
            '                                                        mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                        mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                        mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                        mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                        .Form.Item("cbeEmployeeCode"), .Form.Item("tctLastName"), .Form.Item("tctLastName2"), _
            '                                                        .Form.Item("tctFirstName"), .Form.Item("tctMiddleName"), mobjValues.StringToType(.Form.Item("DateBirthdate"), eFunctions.Values.eTypeData.etdDate), _
            '                                                         mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
            '                                                         mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                         mobjValues.StringToType(.QueryString.Item("nSituation"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                         mobjValues.StringToType(.QueryString.Item("nNumberInsured"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                         .Form.Item("tctBeneficiarnotes"), _
            '                                                         mobjValues.StringToType(.Form.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                         mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger), _
            '                                                         mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), _
            '                                                         mobjValues.StringToType(.Form.Item("DateNulldate"), eFunctions.Values.eTypeData.etdDate))



            '        End With
            '    Else
            '        lblnPost = True
            '    End If
            '    mobjPolicySeq = Nothing

            Case "CM001"
                mobjPolicySeq = New ePolicy.TRCM
                With Request
                    lblnPost = mobjPolicySeq.InsPostCM001(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("cbovalGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form.Item("cbovalSituation"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form.Item("cbeTypeWork"), eFunctions.Values.eTypeData.etdInteger),
                                                          .Form.Item("tctWorkname"),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_work"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_work"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_m"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_m"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dInitialdate_em"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(.Form.Item("dEnddate_em"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger),
                                                          Session("nUsercode"))
                End With
                mobjPolicySeq = Nothing

        End Select

        '+Se ejecutan las ventana automaticas
        '    mobjNetFrameWork.FinishProcess "PostSequence|" & Request.QueryString("sCodispl")
        If lblnPost And Request.QueryString.Item("WindowType") <> "PopUp" Then
            Call insGeneralAuto(Request.QueryString.Item("sCodispl"))
        End If
        lclsPolicy_Win = Nothing
        insPostSequence = lblnPost
    End Function

    '%insPostCA001: se realizan las actualizaciones de las tablas en la CA001
    '--------------------------------------------------------------------------------------------
    Function insPostCA001() As Boolean
        Dim clngDuplPolicy As Object
        Dim clngTransHolder As Object
        '--------------------------------------------------------------------------------------------
        Dim lblnConvertion As Boolean
        Dim lblnShowSequence As Boolean

        insPostCA001 = True
        '+ Se limpia la session de la transación original    
        Session("nTransaction2") = eRemoteDB.Constants.intNull
        '+ Se inicializa la variable de traspaso de asegurados
        Session("sTransHolder") = ""
        With Request
            '+ Se inicializan las variables de la sesión
            Session("sCertype") = .Form.Item("sCertype")
            Session("dEffecdate") = .Form.Item("tcdEffecdate")
            Session("nTransaction") = .Form.Item("cbeTransactio")
            Session("nTransaction3") = .Form.Item("cbeTransactio")
            Session("dLedgerDate") = .Form.Item("tcdLedgerDate")
            Session("sPolitype") = .Form.Item("optType")
            Session("sBussityp") = Request.Form.Item("optBussines")
            Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
            Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
            Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
            Session("nPolicyDest") = mobjValues.StringToType(.Form.Item("tcnPolicyDest"), eFunctions.Values.eTypeData.etdDouble)
            Session("nPolicy_old") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
            Session("nAgency") = mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble)
            Session("nContrat") = eRemoteDB.Constants.intNull
            Session("sCodisplOri") = ""
            Session("nType_amend") = mobjValues.StringToType(.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble, True)
            Session("nTypeAccount") = mobjValues.StringToType(.Form.Item("NTYPEACCOUNT"), eFunctions.Values.eTypeData.etdInteger, True)
            If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or _
                Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion) Or _
                Session("nTransaction") = clngDuplPolicy Then
                Session("nProponum") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                Session("nTransaction2") = Session("nTransaction")
            End If
            'Session("sApv") = lclsProduct.sApv
            '            Session("nTransaction") = clngQuotPropAmendentConvertion
            If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropRenewalQuery) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropAmendentQuery) Then
                Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble)
            End If
        End With

        If insPostCA001 Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropQuotConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotAmendConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropAmendConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotRenewalConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropRenewalConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion) Or _
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngReprint) Or _
            Session("nTransaction") = clngTransHolder Or _
            Session("nTransaction") = clngDuplPolicy Then
            lblnConvertion = (Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or Session("nTransaction") = eCollection.Premium.PolTransac.clngProposalConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPropQuotConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPropAmendConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotRenewalConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPropRenewalConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion Or Session("nTransaction") = clngTransHolder Or Session("nTransaction") = clngDuplPolicy)
            If mobjPolicySeq.insPostCA001(Session("sCertype"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicydest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), Session("sBussityp"), Session("sPolitype"), mobjValues.StringToType(Request.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdFer"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy_Digit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProp_reg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRenewalnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dLastChange"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sCodispl"), "", mobjValues.StringToType(Request.Form.Item("tcnFolio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddInstitution"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeOffice_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertificat_Associated"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("NTYPEACCOUNT"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tcnProcess_num"), mobjValues.StringToType(Request.Form.Item("tcnPolicy_Transfer"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif_transfer"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctCodBranch_transfer")) Then
                'If mobjPolicySeq.insPostCA001(Session("sCertype"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicydest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), Session("sBussityp"), Session("sPolitype"), mobjValues.StringToType(Request.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdFer"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy_Digit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProp_reg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRenewalnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dLastChange"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sCodispl"), "", mobjValues.StringToType(Request.Form.Item("tcnFolio"), eFunctions.Values.eTypeData.etdDouble)) Then

                Session("sBrancht") = mobjPolicySeq.sBrancht
                Session("nPolicy") = mobjPolicySeq.nPolicy
                Session("nCertif") = mobjPolicySeq.nCertif
                Session("dStartdate") = mobjPolicySeq.dStartdate
                Session("dExpirdat") = mobjPolicySeq.dExpirdat
                Session("nProdclas") = mobjPolicySeq.nProdclas

            End If

            If Session("nTransaction") <> CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) And Session("nTransaction") <> eCollection.Premium.PolTransac.clngProposalConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngPropQuotConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngQuotAmendConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngPropAmendConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngQuotRenewalConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngPropRenewalConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion And Session("nTransaction") <> eCollection.Premium.PolTransac.clngReprint And Session("nTransaction") <> clngDuplPolicy Then

                '+Si se trata de una modificación temporal se llama a la ventana de fecha de vencimiento
                If (Session("nTransaction") <> CInt(eCollection.Premium.PolTransac.clngTempPolicyAmendment) Or Session("nTransaction") <> CInt(eCollection.Premium.PolTransac.clngTempCertifAmendment)) Then
                    '+Si se trata de una re-emision se llama a la ventana de informacion de re-emision
                    If (Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyReissue) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifReissue)) And CStr(Session("sPolitype")) <> "2" And CStr(Session("sPolitype")) <> "3" Then
                    Else
                        If Session("nTransaction") = clngTransHolder Then
                            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifIssue)
                            Session("sTransHolder") = "1"
                        End If
                        '+ Se carga la secuencia             
                        lblnShowSequence = True
                        mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=InSequence&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
                    End If
                End If
            Else

                '+ Se muestra la página principal de la secuencia (en caso de Converciones a cotización o póliza)
                lblnShowSequence = False
                If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropQuotConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotAmendConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropAmendConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotRenewalConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropRenewalConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion) Or Session("nTransaction") = clngTransHolder Or Session("nTransaction") = clngDuplPolicy Then

                    '+ En el caso de conversiones, se cambia el valor a la variable de Sesion correspondiente al
                    '+ número de póliza con el nuevo Nº de Cotización o póliza
                    If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropQuotConvertion) Then
                        Session("sCertype") = "1"
                    ElseIf Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion) Then
                        Session("sCertype") = "6"
                    Else
                        Session("sCertype") = "2"
                    End If
                    If Session("nTransaction") = clngTransHolder Then
                        Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifIssue)
                        Session("sTransHolder") = "1"
                    End If
                    If Session("nTransaction") <> CInt(eCollection.Premium.PolTransac.clngPropAmendConvertion) And _
                        Session("nTransaction") <> CInt(eCollection.Premium.PolTransac.clngPropQuotConvertion) Then
                        Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngRecuperation)
                    End If
                    lblnShowSequence = True
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=InSequence&bMenu=1'"
                Else
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=WithOutSequence&bMenu=1'"
                End If
            End If

            If Session("nTransaction") = clngTransHolder Then
                Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion)
                Session("sTransHolder") = "1"
            End If

            If insPostCA001 And Not lblnConvertion Then

                '+ Se carga la secuencia
                lblnShowSequence = True
                mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=InSequence&bMenu=1'"
            Else
                If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion) Or CStr(Session("nTransaction")) = "43" Or Session("nTransaction") = clngDuplPolicy Then

                    '+ Se muestra la página principal de la secuencia
                    lblnShowSequence = False
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=WithOutSequence&bMenu=1'"
                End If
            End If
        End If

        If Session("nTransaction") = clngTransHolder Then
            Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngProposalConvertion)
            Session("sTransHolder") = "1"
        End If

        If lblnShowSequence And insPostCA001 And mstrLocationCA001 = vbNullString Then
            '+ Se muestra la secuencia       
            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=InSequence&bMenu=1'"
        End If

        '+ Se asigna la accion a tomar
        Select Case Session("nTransaction")
            Case eCollection.Premium.PolTransac.clngPolicyQuery, eCollection.Premium.PolTransac.clngCertifQuery, eCollection.Premium.PolTransac.clngQuotationQuery, eCollection.Premium.PolTransac.clngProposalQuery, eCollection.Premium.PolTransac.clngQuotAmendentQuery, eCollection.Premium.PolTransac.clngPropAmendentQuery, eCollection.Premium.PolTransac.clngQuotRenewalQuery, eCollection.Premium.PolTransac.clngPropRenewalQuery, "44"
                Session("bQuery") = True
            Case Else
                Session("bQuery") = False
        End Select

        '+ Si se trata de una modificacion temporal, se asigna valor a la fecha de anulación.
        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempCertifAmendment) Then
            Session("dNullDate") = mobjValues.StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate)
        End If
    End Function

    '% insCancel: Esta rutina es activada cuando el usuario cancela la transacción en donde
    '%              está trabajando.
    '--------------------------------------------------------------------------------------------
    Function insCancel() As Boolean
        Dim clngDuplPolicy As Object
        Dim lstrsCertype As String
        '--------------------------------------------------------------------------------------------
        Dim lclsValues As eFunctions.Values
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim lintString As Integer
        Dim lstrError As String = String.Empty
        Dim lclsPolicy_aux As ePolicy.Policy
        Dim llngProponum As Object
        Dim lclsPageRetCA050 As Object

        '- Variables para almacenar temporalmente el número de Referencia y Código del proceso    
        Dim llngReference As Integer
        Dim lintCodeProce As Integer

        lclsValues = New eFunctions.Values
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.55
        lclsValues.sSessionID = Session.SessionID
        lclsValues.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        lclsValues.sCodisplPage = "ValPolicySeq"
        lclsErrors = New eFunctions.Errors
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.55
        lclsErrors.sSessionID = Session.SessionID
        lclsErrors.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        insCancel = True
        lclsPageRetCA050 = Session("PageRetCA050")

        If lclsPageRetCA050 = "CA001C" Then
            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
        Else
            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
        End If

        '+ Se realiza el llamado al procedimiento que actualiza el campo UserAmend 
        '+ de Policy o Certificat, según sea el caso
        Call insUpdUserAmend()

        '+Se realiza el reverso de la modificación
        If CBool(Trim(CStr(CStr(Session("nTransaction")) <> vbNullString))) Then
            If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempCertifAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPropAmendConvertion) Then
                If Not lclsCertificat.insReverRenModPol(Session("sCertype"), lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 0, lclsValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), 0) Then
                    Response.Write(lclsErrors.ErrorMessage("CA001_K", 3616, , , , True))
                End If
            End If

            '+ Sólamente se efectuará este proceso siempre y cuando la ventana no sea la principal (CA001).
            If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyIssue) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifIssue) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngRecuperation) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifProposal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyProposal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuotation) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuotation) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuotAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuotAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyPropAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifPropAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuotRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuotRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyPropRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifPropRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion) Then
                If Request.Form.Item("optElim") = "Delete" Then
                    With lclsPolicy
                        .sCertype = Session("sCertype")
                        .nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                        .nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                        .nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
                        .nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)

                        '+ Se asigna el número de referencia
                        If Request.Form.Item("tcnReference") = vbNullString Then
                            llngReference = 0
                        Else
                            llngReference = lclsValues.StringToType(Request.Form.Item("tcnReference"), eFunctions.Values.eTypeData.etdDouble)
                        End If

                        '+ Se asigna el código del proceso
                        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyIssue) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngdeclarations) Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Then
                            lintCodeProce = 4
                        End If

                        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngQuotationConvertion) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuery) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuery) Or Session("nTransaction") = clngDuplPolicy Then
                            lintCodeProce = 6
                        End If
                        '+ Se busca la propuesta que dio origen a la propuesta si esta no es parte de los datos
                        '+ que existen para la transaccion
                        llngProponum = Session("nProponum")
                        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngRecuperation) Then
                            If lclsCertificat.Find("2", Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("nCertif")) Then
                                llngProponum = lclsCertificat.nProponum
                            End If
                        End If
                        '+ Se eliminan los datos de la póliza
                        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuotAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuotAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyPropAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifPropAmendent) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyQuotRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifQuotRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyPropRenewal) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifPropRenewal) Then
                            lclsPolicy_his = New ePolicy.Policy_his
                            Call lclsPolicy_his.DelRecordType_policy_his("2", Session("nBranch"), Session("nProduct"), Session("nPolicy_old"), Session("nCertif"), 9)
                            lclsPolicy_his = Nothing
                        End If
                        If .DelRecursivePolicy(lintCodeProce, llngReference) Then
                            '+ Reversa el estado de la propuesta

                            If IsNumeric(Session("nTransaction2")) AndAlso (eCollection.Premium.PolTransac.clngQuotationConvertion Or Session("nTransaction2") = eCollection.Premium.PolTransac.clngProposalConvertion Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngRecuperation)) Then
                                lstrsCertype = "1"
                                If Session("nTransaction2") = eCollection.Premium.PolTransac.clngQuotationConvertion Then
                                    lstrsCertype = "3"
                                End If
                                If lclsCertificat.Find(lstrsCertype, Session("nBranch"), Session("nProduct"), llngProponum, Session("nCertif")) Then
                                    lclsCertificat.nStatquota = 1
                                    lclsCertificat.nPol_quot = eRemoteDB.Constants.intNull
                                    Call lclsCertificat.Update()
                                End If
                                lclsPolicy_his = New ePolicy.Policy_his
                                If lclsPolicy_his.DelRecordType_policy_his(lstrsCertype, Session("nBranch"), Session("nProduct"), llngProponum, Session("nCertif"), 21) Then
                                    lclsPolicy_aux = New ePolicy.Policy
                                    If lclsPolicy_aux.Find(lstrsCertype, Session("nBranch"), Session("nProduct"), Session("nProponum")) Then
                                        lclsPolicy_aux.nMov_histor = lclsPolicy_aux.nMov_histor - 1
                                        lclsPolicy_aux.Add()
                                    End If
                                End If
                            End If
                            lstrError = lclsErrors.ErrorMessage("CA001_K", 3990, , , , True)
                            lintString = InStr(1, lstrError, "Err.")
                            If lintString > 0 Then
                                lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                            End If
                            Response.Write(lstrError)
                        Else
                            Response.Write(lclsErrors.ErrorMessage("CA001_K", 3991, , , , True))
                        End If
                    End With
                Else

                    '+ Mensaje informativo.
                    Select Case Session("nTransaction")
                        Case eCollection.Premium.PolTransac.clngPolicyIssue, eCollection.Premium.PolTransac.clngCertifIssue, eCollection.Premium.PolTransac.clngRecuperation
                            lstrError = lclsErrors.ErrorMessage("CA001_K", 3968, , , , True)
                            lintString = InStr(1, lstrError, "Men.")
                            If lintString > 0 Then
                                lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                            End If

                            Response.Write(lstrError)

                        Case eCollection.Premium.PolTransac.clngPolicyQuotation, eCollection.Premium.PolTransac.clngCertifQuotation, eCollection.Premium.PolTransac.clngPolicyQuotAmendent, eCollection.Premium.PolTransac.clngCertifQuotAmendent, eCollection.Premium.PolTransac.clngPolicyQuotRenewal, eCollection.Premium.PolTransac.clngCertifQuotRenewal

                            lstrError = lclsErrors.ErrorMessage("CA001_K", 3970, , , , True)
                            lintString = InStr(1, lstrError, "Men.")
                            If lintString > 0 Then
                                lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                            End If

                            Response.Write(lstrError)

                        Case eCollection.Premium.PolTransac.clngPolicyProposal, eCollection.Premium.PolTransac.clngCertifProposal, eCollection.Premium.PolTransac.clngPolicyPropAmendent, eCollection.Premium.PolTransac.clngCertifPropAmendent, eCollection.Premium.PolTransac.clngPolicyPropRenewal, eCollection.Premium.PolTransac.clngCertifPropRenewal

                            lstrError = lclsErrors.ErrorMessage("CA001_K", 3969, , , , True)
                            lintString = InStr(1, lstrError, "Men.")
                            If lintString > 0 Then
                                lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                            End If

                            Response.Write(lstrError)

                    End Select
                End If
            End If
        End If

        lclsErrors = Nothing
        lclsValues = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
    End Function

    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        Dim sPrinted As String
        '--------------------------------------------------------------------------------------------
        'if 1=2 then
        Dim lclsValues As eFunctions.Values
        Dim lclsPolicy As ePolicy.Certificat
        Dim lclsPolicy_amend As ePolicy.Policy
        Dim lclsPageRetCA050 As Object



        '-Objeto para transacciones batch	
        Dim lclsBatch_param As eSchedule.Batch_param

        lclsPageRetCA050 = Session("PageRetCA050")

        lclsValues = New eFunctions.Values
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.02
        lclsValues.sSessionID = Session.SessionID
        lclsValues.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        lclsValues.sCodisplPage = "ValPolicySeq"
        lclsPolicy = New ePolicy.Certificat

        '+ Si existe alguna carpeta que no halla sido carga con información.
        insFinish = True

        Select Case Session("nTransaction")
            Case "12", "13", "14", "15", "24", "25", "26", "27", "34"

                '+ Si se trata de Fin de Proceso (CA048). Modificación de Póliza individual o certificado.
                If Request.Form.Item("chkAfeccer") <> "1" Then

                    If lclsPolicy.insExecuteCA048(Request.QueryString.Item("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPendenstat"), mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkAfeccer"), mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkssstatus_pol")) Then
                        If Request.Form.Item("chkPrintNow") = "1" Then
                            insPrintDocuments()
                        End If

                        insFinish = True

                        If lclsPageRetCA050 = "CA001C" Then
                            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                        Else
                            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                        End If


                        '+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
                        Call insUpdUserAmend()
                    Else

                        insFinish = False
                    End If

                Else

                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 160
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sCertype"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("chkPendenstat"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("chkAfeccer"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True))

                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")

                    lclsBatch_param = Nothing

                    If Request.Form.Item("chkPrint") = "1" Then
                        insPrintDocuments()
                    End If

                    insFinish = True

                    If lclsPageRetCA050 = "CA001C" Then
                        mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                    Else
                        mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                    End If


                    '+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
                    Call insUpdUserAmend()

                End If

            Case "1", "2", "3", "4", "5", "6", "7", "18", "19", "30", "31", "43", "16", "23", "17", "45"
                '+ Si se trata de Fin de Emisión (CA050) 
                sPrinted = Request.Form.Item("chkDetailedEntryPrinted")
                If sPrinted <> "1" Then
                    sPrinted = "2"
                End If
                If lclsPolicy.insExecuteCA050(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), Session("nCertif"), CStr(Session("nTransaction")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), Request.Form.Item("gmtDocument"), Request.Form.Item("gmtDocumentTag"), IIf( IsNothing(Request.Form.Item("blnEnabledWaitCode")),0,Request.Form.Item("blnEnabledWaitCode")), mobjValues.StringToType(Request.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), IIf( IsNothing(Request.Form.Item("pblnDocQuotation")),0,Request.Form.Item("pblnDocQuotation")), Request.Form.Item("chkCertif"), mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), sPrinted) Then


                    '+ Se ejecuta el CAL001

                    If Request.Form.Item("chkPrintNow") = "1" Then
                        insPrintDocuments()
                    End If

                    If Request.Form.Item("chkPrintControlDig") = "1" Then
                        insPrintDocuments()
                    End If



                    insFinish = True

                    '+ Se muestra la página principal de la secuencia
                    If (CStr(Session("sPoliType")) = "2" Or CStr(Session("sPoliType")) = "3") And (CStr(Session("nTransaction")) = "1" Or CStr(Session("nTransaction")) = "2" Or CStr(Session("nTransaction")) = "18" Or CStr(Session("nTransaction")) = "19") Then
                        '+ Se invoca la secuencia de póliza con la transaccion de Emision de Certificado
                        mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&nTransaction=2&bMenu=1&nOrig_call=1'"
                    Else
                        If lclsPageRetCA050 = "CA001C" Then
                            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                        Else
                            mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1'" ' &nOrig_call=1'"
                        End If
                    End If

                    '+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
                    Call insUpdUserAmend()
                Else
                    insFinish = False
                End If

            '+Si se trata de consultas de cartera
            Case "8", "9", "10", "11", "44"
                '+ Se muestra la página principal de la secuencia
                If lclsPageRetCA050 = "CA001C" Then
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                Else
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
                End If
                insFinish = True

            '+Si se trata de las transacciones restantes
            Case "16", "17", "20", "22"
                '+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
                Call insUpdUserAmend()

            '+Declaraciones
            Case "21"
                '+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
                Call insUpdUserAmend()
            Case Else
                mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
        End Select

        Session("nFinish") = Request.QueryString.Item("nAction")
        '+ se agrego manejo de fecha de ultima modificación para los endosos    
        If insFinish Then
            lclsPolicy_amend = New ePolicy.Policy
            If lclsPolicy_amend.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                If Session("nTransaction") = 12 Or Session("nTransaction") = 14 Or Session("nTransaction") = 13 Or Session("nTransaction") = 15 Then
                    If Session("nCertif") = 0 Then
                        If Session("nTransaction") = 13 Or Session("nTransaction") = 15 Then
                            lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate)
                        Else
                            If Request.Form.Item("chkPendenstat") <> "1" Then
                                lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                            End If
                        End If
                    End If
                ElseIf Session("nTransaction") = 1 Or Session("nTransaction") = 6 Or Session("nTransaction") = 4 Or Session("nTransaction") = 24 Or Session("nTransaction") = 25 Or Session("nTransaction") = 28 Or Session("nTransaction") = 29 Or Session("nTransaction") = 26 Or Session("nTransaction") = 27 Or Session("nTransaction") = 30 Or Session("nTransaction") = 31 Or Session("nTransaction") = 18 Or Session("nTransaction") = 19 Then
                    If Request.Form.Item("chkPendenstat") <> "1" Then
                        lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                    End If
                ElseIf Session("nTransaction") = 3 And Session("nCertif") = 0 And Request.Form.Item("chkPendenstat") <> "1" Then
                    lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                End If
                lclsPolicy_amend.Add()
                'ehh - Ad. vt fase II rsis 2
                If CStr(Session("sPoliType")) = "1" Then
                    Dim lclsRecIng As New Reconocimiento_ingresos
                    lclsRecIng.genRecieptInd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nReceiptGenInd"))
                End If
            End If
            lclsPolicy_amend = Nothing
        End If
        '    end if
        'insFinish = False
    End Function


    '% insUpdUserAmend: se actualiza el campo nUser_amend de Policy o Certificat, según sea el caso
    '--------------------------------------------------------------------------------------------
    Sub insUpdUserAmend()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsValues As eFunctions.Values

        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsValues = New eFunctions.Values
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.08
        lclsValues.sSessionID = Session.SessionID
        lclsValues.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        lclsValues.sCodisplPage = "ValPolicySeq"

        If Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempPolicyAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngTempCertifAmendment) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngRecuperation) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngPolicyIssue) Or Session("nTransaction") = CInt(eCollection.Premium.PolTransac.clngCertifIssue) Then
            If CStr(Session("nCertif")) = vbNullString Or CStr(Session("nCertif")) = "0" Then
                '+ Se actualiza el campo en la tabla Policy        
                With lclsPolicy
                    .sCertype = Session("sCertype")
                    .nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                    .nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
                    .Update_UserAmend()
                End With
                '+ Se actualiza el campo en la tabla Certificat        
                With lclsCertificat
                    .sCertype = Session("sCertype")
                    .nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                    .nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
                    .nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
                    .Update_UserAmend()
                End With
            Else
                '+ Se actualiza el campo en la tabla Certificat        
                With lclsCertificat
                    .sCertype = Session("sCertype")
                    .nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                    .nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
                    .nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
                    .Update_UserAmend()
                End With
            End If
        End If

        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lclsValues = Nothing
    End Sub

    '+Esta función carga automáticamente con contenido las ventanas correspondientes dependiendo de la que se esté tratando.
    '------------------------------------------------------------------
    Private Sub insGeneralAuto(ByVal sCodispl As String)
        '------------------------------------------------------------------
        Dim lclsAutoCharge As ePolicy.AutoCharge

        '    mobjNetFrameWork.BeginProcess "AutoUpdGeneral-" & sCodispl
        lclsAutoCharge = New ePolicy.AutoCharge
        Call lclsAutoCharge.InsAutoUpdGeneral(sCodispl, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdLong), Session("sPoliType"), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToDate(Session("dNulldate")), Session("nTransaction"), Session("nUsercode"), Session("sBrancht"), Session("SessionId"), Session("sBussityp"), mobjValues.StringToType(Session("nType_amend"), eFunctions.Values.eTypeData.etdLong))
        lclsAutoCharge = Nothing
        '    mobjNetFrameWork.FinishProcess "AutoUpdGeneral-" & sCodispl
    End Sub


    '-----------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-----------------------------------------------------------------------------------
        Dim mobjDocuments As eReports.Report
        Dim lcolReport_prod As eProduct.report_prods
        Dim lclsReport_prod As eProduct.report_prod
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrQueryString As Object
        '    mobjNetFrameWork.BeginProcess "insPrintDocuments"

        mobjDocuments = New eReports.Report

        If Request.QueryString.Item("sCodispl") = "CA048" Or Request.QueryString.Item("sCodispl") = "CA050" Then
            'Si se está emitiendo una Cotización,
            'se llama el simulador de Cuadro de póliza


            'If CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "4" Then
            'lstrQueryString = "sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif")& "&nGraph=" & Request.Form("hddGraphics")
            'Response.Write ("<SCRIPT>ShowPopUp(""/VTimeNet/Common/PrintPol.aspx?" & lstrQueryString & """, ""PrintPolicy"",700,650,""yes"",""no"",100,20,'yes','yes');</" & "Script>")
            lcolReport_prod = New eProduct.report_prods

            If lcolReport_prod.FindReport_prod_By_Transac(Session("sCertype"),
                                                          Session("nBranch"),
                                                          Session("nProduct"),
                                                          Session("nPolicy"),
                                                          Session("nCertif"),
                                                          Session("nTransaction"),
                                                          eRemoteDB.Constants.intNull,
                                                          Session("dEffecdate"),
                                                          True) Then

                For Each lclsReport_prod In lcolReport_prod

                    If Request.Form.Item("chkPrintNow") = "1" Or Request.Form.Item("chkPrintControlDig") = "1" Then

                        With mobjDocuments
                            If Trim(lclsReport_prod.sCodCodispl) = "CAL668" And Request.Form.Item("chkPrintControlDig") = "1" Then
                                .sCodispl = Trim(lclsReport_prod.sCodCodispl)
                                .ReportFilename = lclsReport_prod.sReport
                                .setStorProcParam(1, Session("sCertype"))
                                .setStorProcParam(2, Session("nBranch"))
                                .setStorProcParam(3, Session("nProduct"))
                                .setStorProcParam(4, Session("nPolicy"))
                                .setStorProcParam(5, Session("nCertif"))
                                .setStorProcParam(6, Session("Contratante"))
                                .setStorProcParam(7, Session("Asegurado"))
                                .setStorProcParam(8, .setdate(Session("dEffecdate")))
                                .setStorProcParam(9, "")
                                .setStorProcParam(10, Session("nUsercode"))
                            Else
                                If Trim(lclsReport_prod.sCodCodispl) <> "CAL668" And Request.Form.Item("chkPrintNow") = "1" Then

                                    If lclsReport_prod.nRepType = 1 Then
                                        If Session("sCertype") = "2" Or Session("sCertype") = "6" Then
                                            .nFormat = 31
                                            If lclsReport_prod.nTratypep = 1 Then
                                                .ReportFilename = lclsReport_prod.sReport
                                                .nReport = 1
                                                .setStorProcParam(1, Session("sCertype"))
                                                .setStorProcParam(2, Session("nBranch"))
                                                .setStorProcParam(3, Session("nProduct"))
                                                .setStorProcParam(4, Session("nPolicy"))
                                                .setStorProcParam(5, 0)
                                                .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                                .nMovement = 1
                                                .Merge = False
                                                .nGenPolicy = 1
                                                .nForzaRep = 1
                                                .nTratypep = lclsReport_prod.nTratypep
                                            ElseIf lclsReport_prod.nTratypep = 2 Then
                                                .nReport = 1
                                                lclsPolicy = New ePolicy.Policy

                                                lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)
                                                .ReportFilename = "CAL001_A_V.RPT"
                                                .setStorProcParam(1, Session("sCertype"))
                                                .setStorProcParam(2, Session("nBranch"))
                                                .setStorProcParam(3, Session("nProduct"))
                                                .setStorProcParam(4, Session("nPolicy"))
                                                .setStorProcParam(5, Session("nCertif"))
                                                .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                                .setStorProcParam(7, "1")
                                                .setStorProcParam(8, "")
                                                .setStorProcParam(9, lclsPolicy.nMov_histor)
                                                .nReport = 2
                                                .Merge = False
                                                '.MergeCertype = "2"
                                                '.MergeBranch = Session("nBranch")
                                                '.MergeProduct = Session("nProduct")
                                                '.MergePolicy = Session("nPolicy")
                                                .nGenPolicy = 1
                                                .nMovement = lclsPolicy.nMov_histor
                                                .nForzaRep = 1
                                                .nTratypep = lclsReport_prod.nTratypep
                                                '.MergeCodispl = sCodispl


                                            End If
                                        Else
                                            .sCodispl = Trim(lclsReport_prod.sCodCodispl)
                                            .ReportFilename = lclsReport_prod.sReport
                                            .setStorProcParam(1, Session("sCertype"))
                                            .setStorProcParam(2, Session("nBranch"))
                                            .setStorProcParam(3, Session("nProduct"))
                                            .setStorProcParam(4, Session("nPolicy"))
                                            .setStorProcParam(5, Session("nCertif"))
                                            .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                            .nReport = 2
                                            .nFormat = 31
                                            .Merge = True
                                        End If
                                        .MergeCertype = Session("sCertype")
                                        .MergeBranch = Session("nBranch")
                                        .MergeProduct = Session("nProduct")
                                        .MergePolicy = Session("nPolicy")
                                        .MergeCertif = Session("nCertif")
                                        .sPolitype = Session("sPolitype")
                                    Else
                                        .sCodispl = Trim(lclsReport_prod.sCodCodispl)
                                        .ReportFilename = lclsReport_prod.sReport
                                        .setStorProcParam(1, Session("sCertype"))
                                        .setStorProcParam(2, Session("nBranch"))
                                        .setStorProcParam(3, Session("nProduct"))
                                        .setStorProcParam(4, Session("nPolicy"))
                                        .setStorProcParam(5, Session("nCertif"))
                                        .setStorProcParam(6, .setdate(Session("dEffecdate")))
                                    End If
                                End If
                            End If

                            If (Trim(lclsReport_prod.sCodCodispl) = "CAL668" And Request.Form.Item("chkPrintControlDig") = "1") Or
                               (Trim(lclsReport_prod.sCodCodispl) <> "CAL668" And Request.Form.Item("chkPrintNow") = "1") Then
                                Response.Write((.Command))
                                .Reset()
                                .bTimeOut = True
                            End If

                            'If CStr(Session("nTransaction")) = "4" And Request.Form.Item("chkPrintControlDig") = "1" Then
                            '    .sCodispl = "CAL01415"
                            '    .ReportFilename = "CAL01415.RPT"
                            '    .setStorProcParam(1, Session("sCertype"))
                            '    .setStorProcParam(2, Session("nBranch"))
                            '    .setStorProcParam(3, Session("nProduct"))
                            '    .setStorProcParam(4, Session("nPolicy"))
                            '    .setStorProcParam(5, Session("nCertif"))
                            '    .setStorProcParam(6, Session("Contratante"))
                            '    .setStorProcParam(7, Session("Asegurado"))
                            '    .setStorProcParam(8, Session("dEffecdate"))
                            '    .setStorProcParam(9, "")
                            '    .setStorProcParam(10, Session("nUsercode"))
                            '    Response.Write((.Command))
                            '    .Reset()
                            'End If
                            'If Request.Form.Item("chkPrintNow") = "1" And CStr(Session("sCertype")) = "3" Then

                            '    Select Case Trim(lclsReport_prod.sCodCodispl)
                            '        Case "CAL01504" 'Mas Salud
                            '            .sCodispl = "CAL01504"
                            '            .ReportFilename = "CAL01504.RPT"
                            '            .setStorProcParam(1, Session("nPolicy"))
                            '            .setStorProcParam(2, Session("nBranch"))
                            '            .setStorProcParam(3, Session("nProduct"))
                            '            Response.Write((.Command))

                            '        Case "CAL08000" 'Protector y Más Protector

                            '            .sCodispl = "CAL08000"
                            '            .ReportFilename = "CAL08000.rpt"
                            '            .setStorProcParam(1, Session("nBranch"))
                            '            .setStorProcParam(2, Session("nProduct"))
                            '            .setStorProcParam(3, Session("nPolicy"))
                            '            .setStorProcParam(4, Session("nUsercode"))
                            '            Response.Write((.Command))

                            '        Case "VIL8003" 'Previsor Plus
                            '            .sCodispl = "VIL8003"
                            '            .ReportFilename = "QuotationPrevisorPlus.RPT"
                            '            .setStorProcParam(1, Session("sCertype"))
                            '            .setStorProcParam(2, Session("nBranch"))
                            '            .setStorProcParam(3, Session("nProduct"))
                            '            .setStorProcParam(4, Session("nPolicy"))
                            '            .setStorProcParam(5, 0)
                            '            .setStorProcParam(6, 0)
                            '            Response.Write((.Command))


                            '        Case "CAL001" 'Planificadores
                            '            .sCodispl = "VIL8004"
                            '            .ReportFilename = "QuotationPlanificador.RPT"
                            '            .setStorProcParam(1, Session("sCertype"))
                            '            .setStorProcParam(2, Session("nBranch"))
                            '            .setStorProcParam(3, Session("nProduct"))
                            '            .setStorProcParam(4, Session("nPolicy"))
                            '            .setStorProcParam(5, 0)
                            '            .setStorProcParam(6, 0)
                            '            Response.Write((.Command))

                            '        Case "VI1410" 'Nuevo AVP
                            '            .sCodispl = "VIL8002"
                            '            .ReportFilename = "QuotationNewAPV.RPT"
                            '            .setStorProcParam(1, Session("sCertype"))
                            '            .setStorProcParam(2, Session("nBranch"))
                            '            .setStorProcParam(3, Session("nProduct"))
                            '            .setStorProcParam(4, Session("nPolicy"))
                            '            .setStorProcParam(5, 0)
                            '            .setStorProcParam(6, 0)
                            '            Response.Write((.Command))


                            '    End Select
                            'End If
                        End With
                    End If
                Next
            End If
            lcolReport_prod = Nothing
            lclsReport_prod = Nothing

            'Else
            '    With mobjDocuments
            '        .ReportFilename = "CAL001_B.rpt"
            '        .sCodispl = "CAL001"
            '        .setStorProcParam(1, Session("sCertype"))
            '        .setStorProcParam(2, Session("nBranch"))
            '        .setStorProcParam(3, Session("nProduct"))
            '        .setStorProcParam(4, Session("nPolicy"))
            '        .setStorProcParam(5, Session("nCertif"))
            '        .setStorProcParam(6, "")
            '        .setStorProcParam(7, "")
            '        .setStorProcParam(8, "")
            '        .setStorProcParam(9, "")
            '        .setStorProcParam(10, "")
            '        .setStorProcParam(11, "")
            '        .setStorProcParam(12, "1")
            '        .setStorProcParam(13, "1")
            '        Response.Write((.Command))
            '    End With
            'End If
        Else
            With mobjDocuments
                .ReportFilename = "CAL001_A.rpt"
                .sCodispl = "CAL001"
                .nFormat = 31
                .setStorProcParam(1, Session("sCertype"))
                .setStorProcParam(2, Session("nBranch"))
                .setStorProcParam(3, Session("nProduct"))
                .setStorProcParam(4, Session("nPolicy"))
                .setStorProcParam(5, Session("nCertif"))
                .setStorProcParam(6, Mid(Session("dEffecdate"), 7, 4) & Mid(Session("dEffecdate"), 4, 2) & Mid(Session("dEffecdate"), 1, 2))
                .setStorProcParam(7, "9999")
                .setStorProcParam(8, "")
                .setStorProcParam(9, Session("nTransaction"))
                .setStorProcParam(10, "S")
                .setStorProcParam(11, "TIMENOTHING")
                Response.Write((.Command))
            End With
        End If
        mobjDocuments = Nothing
        '    mobjNetFrameWork.FinishProcess "insPrintDocuments"
    End Sub

</script>
<%Response.Expires = -1441
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.20
'Dim mobjNetFrameWork 
'mobjNetFrameWork = Server.CreateObject("eNetFrameWork.Layout")
'    mobjNetFrameWork.sSessionID = Session.SessionID
'mobjNetFrameWork.nUsercode = Session("nUsercode")
'Call mobjNetFrameWork.BeginPage("ValPolicySeq")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mstrCommand = "sModule=Policy&sProject=PolicySeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
'+ se limpia variable de session
Session("nFinish") = ""

%> 
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





	


<SCRIPT>
    //+ Variable para el control de versiones 
    document.VssVersion = "$$Revision: 12 $|$$Date: 27-09-09 20:00 $|$$Author: Gazuaje $"

        var mintTpremium = "";
        //%NewLocation: se recalcula el URL de la página
        //------------------------------------------------------------------------------------------
        function NewLocation(Source, Codisp) {
            //------------------------------------------------------------------------------------------
            var lstrLocation = "";
            lstrLocation += Source.location;
            lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp
            Source.location = lstrLocation
        }
</SCRIPT>  
</HEAD>
<BODY>
<FORM ID="valPolicySeq" NAME="valPolicySeq">
    <%

        mobjPolicySeq = New ePolicy.ValPolicySeq

        '- Se define la variable para almacenar la nueva dirección de la CA001
        mstrLocationCA001 = vbNullString

        '+ Si no se han validado los campos de la página
        If Request.Form.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insvalSequence()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
            mblnReload = False
        Else
            mblnReload = True
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                If Request.QueryString.Item("ActionType") = "Check" Then
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & "&ActionType=" & Request.QueryString.Item("ActionType") & "&nIndex=" & Request.QueryString.Item("nIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """, ""PolicySeqError"",660,330);")
                Else
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""PolicySeqError"",660,330);")
                    If Request.QueryString.Item("sCodispl") <> "CA021" Then
                        .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                    End If
                End If
                .Write("</SCRIPT>")
            End With
        Else

            If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                If insPostSequence() Then

                    If Request.QueryString.Item("WindowType") <> "PopUp" Or
                      (Request.QueryString.Item("sCodispl") = "CA027A" And Request.QueryString.Item("sOnSeq") = "1") Then

                        '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                        '+ se mueve automaticamente a la siguiente página
                        If mstrLocationCA001 = vbNullString Then

                            '+ Validacion para cuando la CA012 llama a la sequencia desde el modulo "Ordenes profesionales".
                            If CStr(Session("CallSequence")) <> "Prof_ord" Then
                                lclsRefresh = New ePolicy.ValPolicySeq

                                Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl") & Request.QueryString.Item("nIndexCover"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "Yes"))
                                lclsRefresh = Nothing
                            Else
                                If Request.Form.Item("sCodisplReload") = vbNullString Then
                                    Response.Write("<script>top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</script>")
                                Else
                                    Response.Write("<script>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</script>")
                                End If
                            End If
                        Else
                            '+ Se carga nuevamente la ventana principal de la secuencia
                            If mblnReload Then
                                Response.Write("<script>window.close();opener.top.document.location=" & mstrLocationCA001 & ";</script>")
                            Else
                                Response.Write("<script>top.document.location=" & mstrLocationCA001 & ";</script>")
                            End If
                        End If
                        If Request.QueryString.Item("nZone") = "1" Then
                            'Response.Write("<script type='text/javascript'>self.history.go(-1)</script>")
                        End If
                    Else
                        If Request.QueryString.Item("sCodispl") <> "CA014" And Request.QueryString.Item("sCodispl") <> "CA014A" And Request.QueryString.Item("sCodispl") <> "VI021" And Request.QueryString.Item("sCodispl") <> "OS001_K" And Request.QueryString.Item("sCodispl") <> "CA027" And Request.QueryString.Item("sCodispl") <> "VI662" Then
                            If Request.QueryString.Item("sCodispl") = "CA025" Then
                                If mblnReload Then
                                    Response.Write("<script>top.opener.top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</script>")
                                Else
                                    Response.Write("<script>top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</script>")
                                End If
                            Else
                                lclsRefresh = New ePolicy.ValPolicySeq
                                Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "No"))
                                lclsRefresh = Nothing
                            End If
                        End If
                        Select Case Request.QueryString.Item("sCodispl")

                            '+ Si se trata de Fin de proceso, se recarga la ventana principal de la secuencia
                            Case "GE101"
                                Response.Write("<script>top.opener.top.document.location.href=" & mstrLocationCA001 & ";</script>")
                            '+ Emisión de recibo automático                                
                            Case "CA027"
                                Response.Write("<script>top.close();</script>")
                            Case "CA020"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOwnShare=" & Request.Form.Item("hddOwnShare") & "&nExpenses=" & Request.Form.Item("hddExpenses") & "'</script>")
                            Case "CA658"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & "Frame.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOptAge=" & Request.Form.Item("OptAge") & "'</script>")
                            Case "CA024"
                                Response.Write("<script>top.opener.document.location.href='CA024.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sInd_comm=" & Request.Form.Item("hddInd_Comm") & "&sConcoll=" & Request.Form.Item("hddConColl") & "&nCommityp=" & Session("hddsType") & "&nPercent=" & Request.Form.Item("hddtcnPercent") & "'</script>")
                            Case "CA061"
                                Response.Write("<script>top.opener.document.location.href='CA061.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sInd_comm=" & Request.Form.Item("hddInd_Comm") & "&sConcoll=" & Request.Form.Item("hddConColl") & "&nCommityp=" & Session("hddsType") & "&nPercent=" & Request.Form.Item("hddtcnPercent") & "&dIniDate=" & Request.QueryString.Item("dIniDate") & "&dEndDate=" & Request.QueryString.Item("dEndDate") & "'</script>")
                            Case "CA061"
                                Response.Write("<script>top.opener.document.location.href='CA061.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sInd_comm=" & Request.Form.Item("hddInd_Comm") & "&sConcoll=" & Request.Form.Item("hddConColl") & "&nCommityp=" & Session("hddsType") & "&nPercent=" & Request.Form.Item("hddtcnPercent") & "&dIniDate=" & Request.QueryString.Item("dIniDate") & "&dEndDate=" & Request.QueryString.Item("dEndDate") & "'</script>")
                            Case "CA021"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("valCover") & "&sClient=" & Request.Form.Item("valClient") & "&sPopupT=" & Request.Form.Item("tctPopUpT") & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("valCover") & "&sClient=" & Request.Form.Item("valClient") & "&sPopupT=" & Request.Form.Item("tctPopUpT") & mstrQueryString & "'</script>")
                                End If
                            Case "CA021A"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?" & Request.Params.Get("Query_String") & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nCover=" & Request.Form.Item("nCover") & mstrQueryString & "'</script>")
                                End If
                            Case "VI811"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nGroups=" & Request.Form.Item("valGroups") & "&nModulec=" & Request.Form.Item("valModulec") & "'</script>")

                            Case "VI681"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames('fraFolder').document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1&Reload=2&sClient=" & Request.Form.Item("hddsClient") & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1&Reload=2&sClient=" & Request.Form.Item("hddsClient") & "&nRole=" & Request.Form.Item("hddnRole") & "'</script>")
                                End If

                            '+ Cuadro de valores garantizados
                            Case "VI732"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sAut_guarval=" & Request.Form.Item("hddAut_guarval") & "&nCurrency=" & Request.Form.Item("cbeCurrency") & "'</script>")
                            Case "AM002"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nTariff=" & Request.Form.Item("tcnTariff") & "&nGroup=" & Request.Form.Item("tcnGroup") & "&nRole=" & Request.Form.Item("tcnRole") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("tcnCover") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</script>")
                                End If
                            Case "AM003"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nTariff=" & Request.QueryString.Item("nTariff") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole") & "&sClient=" & Request.QueryString.Item("sClient") & "&sIllness=" & Request.QueryString.Item("sIllness") & "&nGroup=" & Request.QueryString.Item("nGroup") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nLimitH=" & Request.QueryString.Item("nLimitH") & "&sAutoRestit=" & Request.QueryString.Item("sAutoRestit") & "'</script>")
                            Case "VI666"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sReloadPage=1" & mstrQueryString & "'</script>")
                            Case "CA025"
                                With Request
                                    Response.Write(mobjValues.GetUrl(eFunctions.Values.eUrlType.cstrGrid, mblnReload, .QueryString.Item("sCodisp"), .QueryString.Item("sCodispl"), "1", .Form.Item("chkContinue"), .QueryString.Item("Action"), .QueryString.Item("ReloadIndex"), .QueryString.Item("nMainAction"), .QueryString.Item("sWindowDescript"), .QueryString.Item("nWindowTy"), mstrQueryString))
                                End With
                            Case "FR001"
                                With Request
                                    Response.Write(mobjValues.GetUrl(eFunctions.Values.eUrlType.cstrGrid, mblnReload, .QueryString.Item("sCodisp"), .QueryString.Item("sCodispl"), "1", .Form.Item("chkContinue"), .QueryString.Item("Action"), .QueryString.Item("ReloadIndex"), .QueryString.Item("nMainAction"), .QueryString.Item("sWindowDescript"), .QueryString.Item("nWindowTy"), mstrQueryString))
                                End With
                            Case "CA014", "CA014A"
                                If Request.QueryString.Item("ActionType") = "Check" Then
                                    Response.Write("<script>")
                                    Response.Write("setPointer('');")
                                    If Request.QueryString.Item("sCodispl") = "CA014" Then
                                        If mblnReload Then
                                            mstrScript = mstrScript & "top.opener."
                                        End If
                                        If Request.QueryString.Item("sCodisplori") <> "VI7011" Then
                                            mstrScript = mstrScript & "top.frames['fraFolder'].InsCalTotalPremium();"
                                        End If
                                    End If
                                    If mblnReload Then
                                        mstrScript = mstrScript & "window.close();"
                                    End If
                                    Response.Write(mstrScript)
                                    If Request.QueryString.Item("sCodisplori") = "VI7011" Then
                                        mstrTotalPrima = mobjValues.StringToType(Request.QueryString.Item("TotalPrima"), eFunctions.Values.eTypeData.etdDouble)
                                        If Request.QueryString.Item("Action") = "Del" Then
                                            mstrTotalPrima = mstrTotalPrima - mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble)
                                        Else
                                            mstrTotalPrima = mstrTotalPrima + mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble)
                                        End If
                                        Response.Write("mintTpremium = " & mstrTotalPrima & ";")
                                        Response.Write("top.frames['fraFolder'].InsCalTotalPremium(mintTpremium);")
                                    End If
                                    Response.Write("</script>")
                                Else
                                    If mblnReload Then
                                        Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</script>")
                                    Else
                                        Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</script>")
                                    End If
                                End If
                            Case "CA016", "CA016A"
                                Response.Write("<script>top.opener.document.location.href='CA016.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</script>")
                            Case "VI7011"
                                Response.Write("<script>top.opener.document.location.href='VI7011.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</script>")
                            Case "OS001_K"
                                Response.Write("<script>top.opener.document.location.href='OS001.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</script>")
                            Case "CA748"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & mstrQueryString & "'</script>")
                            Case "CA013", "CA013A"
                                Response.Write("<script>top.opener.document.location.href='CA013.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</script>")
                            Case "VI7003"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='VI7003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=1" & "'</script>")
                                End If
                            Case "VI7005"
                                Response.Write("<script>top.opener.document.location.href='VI7005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=1" & "'</script>")
                            Case "RO001"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "'</script>")

                            Case "HO001"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "'</script>")

                            Case "TR002"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "'</script>")

                            Case "TR003"
                                Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</script>")
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "'</script>")
                            Case "TR004"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "'</script>")

                            Case "TR6000"
                                Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&tcnLimitCapital=" & Request.QueryString.Item("tcnLimitCapital") & "'</script>")
                            Case "CC001"
                                If Request.QueryString.Item("WindowType") = "PopUp" Then
                                    Response.Write("<script>")
                                    Response.Write("top.opener.top.frames['fraFolder'].document.fraGrid.document.location.href='" & Request.QueryString.Item("sCodispl") & "Frame" & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & mstrQueryString)
                                    Response.Write("'</" & "Script>")
                                Else
                                    Response.Write("<script>")
                                    Response.Write("top.frames['fraFolder'].document.fraGrid.document.location.href='" & Request.QueryString.Item("sCodispl") & "Frame" & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & mstrQueryString)
                                    Response.Write("'</" & "Script>")
                                End If
                            Case "CA100"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</script>")
                                End If
                            Case "VI1410A"
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</script>")
                                End If
                            Case "VI8002"
                                Response.Write("<script>top.opener.document.location.href='VI8002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sReloadPage=1" & mstrQueryString & "'</script>")

                            Case Else
                                If mblnReload Then
                                    Response.Write("<script>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</script>")
                                Else
                                    Response.Write("<script>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</script>")
                                End If
                        End Select
                    End If
                Else
                    If Not CBool(IIf(IsNothing(Request.Form.Item("hddbPuntual")), False, Request.Form.Item("hddbPuntual"))) Then
                        Response.Write("<script>alert('No se pudo realizar la actualización');</script>")
                    End If
                End If
            Else
                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    '+ Se recarga la página principal de la secuencia
                    If CStr(Session("CallSequence")) = "Prof_ord" Then
                        mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=OS590&sProject=Prof_ordseq&sModule=Prof_ord'"
                        Response.Write("<script>top.document.location=" & mstrLocationCA001 & ";</script>")
                    Else
                        If insFinish() Then
                            If Request.Form.Item("sCodisplReload") = "CA048" Then
                                Response.Write("<script>window.close();top.opener.top.document.location=" & mstrLocationCA001 & ";</script>")
                            Else
                                If Request.QueryString.Item("sCodispl") = "CA048" Then
                                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
                                    Response.Write("<script>top.opener.top.document.location=" & mstrLocationCA001 & ";</script>")
                                ElseIf Request.QueryString.Item("sCodispl") = "CA050" Then
                                    Response.Write("<script>top.opener.top.document.location=" & mstrLocationCA001 & ";</script>")
                                End If
                            End If
                        Else
                            Response.Write("<script>alert('No se pudo realizar la actualización final');</script>")
                        End If
                    End If
                End If
            End If
        End If
        mobjPolicySeq = Nothing
        mobjValues = Nothing
    %>
        </FORM>
    </BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.55
    'Call mobjNetFrameWork.FinishPage("ValPolicySeq")
    'mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
