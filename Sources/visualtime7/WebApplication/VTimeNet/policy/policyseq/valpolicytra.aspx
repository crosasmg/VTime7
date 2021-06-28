<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Se define la contante para el manejo de errores en caso de advertencias 

    Dim mstrCommand As String

    '- Variable auxiliar para pase de valores del encabezado al folder

    Dim mstrQueryString As String
    Dim mstrCodispl As String
    Dim mstrMessage As String

    '- Variable usada en para el reporte de rescate(VIL009)
    Dim nProposal As String

    Dim sKey As String
    Dim sKeyVI008 As String
    Dim sBranchtCA034 As String


    '- Variable usada en para el rescate
    Dim sActivefound As String

    '- Variable auxiliar para almacenar el tipo de póliza
    Dim mstrpoli_type As Object
    Dim llngProposal As Object

    '-Tipo de reporte proceso CAL005    
    Dim lintTypeRepCAL005 As Byte

    Dim mobjValues As eFunctions.Values

    Dim nProjRent As Double

    Dim mstrErrors As String
    Dim mobjPolicyTra As Object
    Dim MobjPolicy As ePolicy.Policy
    Dim lclsFunds_Pol As ePolicy.Funds_Pol


    '% insValPolicyTra: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Private Function insValPolicyTra() As String
        'dim dblNull As Object
        'dim eRemoteDB.Constants.intNull As Object
        '--------------------------------------------------------------------------------------------
        Dim lintCountCA051 As Object

        Select Case Request.QueryString.Item("sCodispl")

        '+ CA032: Reverso de modificación/renovación de una póliza                
            Case "CA032"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA032_k("CA032", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA032("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdTransDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTransac"), eFunctions.Values.eTypeData.etdLong))
                    End If
                End With

            '+ CA033: Anulación de una póliza
            Case "CA033"
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA033_k("CA033", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        mobjPolicyTra = New ePolicy.ValPolicyTra
                        insValPolicyTra = mobjPolicyTra.insValCA033("CA033", "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optDev"), .Form.Item("optReceipt"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+CA034: Rehabilitación de una póliza
            Case "CA034"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&sCertype=" & .Form.Item("tctCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nAgency=" & .Form.Item("cbeAgency") & "&nExeMode=" & .Form.Item("optExecute") & "&sCodisplOri=" & .Form.Item("hddCodisplOri") & "&nServ_Order=" & .Form.Item("tcnServ_Order")

                        insValPolicyTra = mobjPolicyTra.insValCA034_K(Session("sCodispl"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("nPolicy"), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nExeMode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate, True), .Form.Item("chkRescRequest"), mobjValues.StringToType(.Form.Item("ValNullLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodisplOri"))
                    End If
                End With

            '+ CA035: Suspensión de Garantias a una Póliza
            Case "CA035"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        If .Form.Item("tcnCertif") = vbNullString Then
                            Session("nCertif") = 0
                        Else
                            Session("nCertif") = .Form.Item("tcnCertif")
                        End If
                        Session("dEffecdate") = .Form.Item("tcdeffecdate")

                        insValPolicyTra = mobjPolicyTra.insValCA035_k("CA035", "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("nMainAction"))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA035("CA035", .QueryString("nMainAction"), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctMailnum"), mobjValues.StringToType(.Form.Item("cbeCode_sus"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdPolExpirdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    End If
                End With

            '+ CA051: Hojas para la carga de pólizas/certificados
            Case "CA051"
                With Request
                    mobjPolicyTra = New eBatch.ValBatch
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA051_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctFile"), .Form.Item("tctDescript"), .Form.Item("chkList"), mobjValues.StringToType(.Form.Item("tcnWorksheet"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("nMainAction") <> "401" Then
                            If Request.QueryString.Item("WindowType") <> "PopUp" Then
                                insValPolicyTra = mobjPolicyTra.insValCA051(.Form.Item("chkAuxSel").Length, .Form.Item("chkSelected").Length, mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble))

                            Else
                                insValPolicyTra = mobjPolicyTra.insValCA051Upd(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdRec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctColumnName"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRequire"), .Form.Item("chkSelected"), Session("nUserCode"), mobjValues.StringToType(.Form.Item("tcnSheet"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsField"), .Form.Item("hddsCritery"))
                            End If
                        End If
                    End If
                End With

            '+CA888: Reinstalación de una póliza (Modificación incompleta)            
            Case "CA888"
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA888_k("CA888", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valUsers"), eFunctions.Values.eTypeData.etdDouble, True), Session("sTypeCompanyUser"))
                    mstrpoli_type = mobjPolicyTra.sPolitype
                End With

            '+ CA028, CA028A: Recibo manual
            Case "CA028", "CA028A"
                mobjPolicyTra = New ePolicy.TDetail_pre
                With Request
                    mstrQueryString = "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&dEffecdate=" & Request.Form.Item("hddStartDateR") & "&dExpirDate=" & Request.Form.Item("hddExpirDate") & "&nReceipt=" & Request.Form.Item("hddReceipt") & "&dIssuedat=" & Request.Form.Item("hddIssueDate") & "&nCurrency=" & Request.Form.Item("hddCurrency") & "&nTratypei=" & Request.Form.Item("hddSource") & "&sOrigReceipt=" & Request.Form.Item("hddOrigReceipt") & "&sCodisplOrig=" & .QueryString.Item("sCodisplOrig") & "&sCertype=" & .QueryString.Item("sCertype") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dNullDate=" & .QueryString.Item("dNullDate") & "&sNullReceipt=" & .QueryString.Item("sNullReceipt") & "&sTypeReceipt=" & .QueryString.Item("sTypeReceipt") & "&nExeMode=" & .QueryString.Item("nExeMode") & "&sExeReport=" & .QueryString.Item("sExeReport") & "&nAgency=" & .QueryString.Item("nAgency") & "&sOnSeq=" & .QueryString.Item("sOnSeq") & "&sNewData=" & .QueryString.Item("sNewData") & "&sKey=" & .QueryString.Item("sKey") & "&sAdjust=" & .QueryString.Item("sAdjust") & "&nAdjReceipt=" & .QueryString.Item("nAdjReceipt") & "&nAdjAmount=" & .QueryString.Item("nAdjAmount")

                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA028_K("CA028", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValCA028(.QueryString("WindowType"), .QueryString("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  , mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate),  ,  , mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeType"), .Form.Item("hddCacalili"), .Form.Item("hddCommissi_i"), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("hddPrem_det_proc"), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValPolicyTra = mobjPolicyTra.insValCA028(.QueryString("WindowType"), .QueryString("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(.Form.Item("Sel").Length + 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLedReceipt"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .Form.Item("optType"), "", "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("tcnAdjReceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    End If
                End With

            '+CA038: Cambio de fecha de renovación
            Case "CA038"
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA038_k("CA038", "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdExpirdate")), mobjValues.StringToDate(.Form.Item("tcdNextReceip")))
                End With

            '+CA037: Cambio de fecha de Efecto
            Case "CA037"
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA037_k("CA037", "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("mdtmExpirdat")), mobjValues.StringToDate(.Form.Item("tcdNextReceip")), mobjValues.StringToDate(.Form.Item("tcdEffecDate")))
                End With

            '+VI009: Rescate de pólizas
            Case "VI009"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValVI009_K(.QueryString("sCodispl"), 301, "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optSurrType"), mobjValues.StringToType(.Form.Item("cbeSurrPayWay"), eFunctions.Values.eTypeData.etdDouble, True), Session("sTypeCompanyUser"), .Form.Item("optProcessType"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.InsValVI009("VI009", .QueryString("nMainAction"), "2", mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnSurrAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsSurrType"), mobjValues.StringToType(.Form.Item("hddsSurrPayWay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnVP"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+VI011: Registro de anticipos
            Case "VI011"
                mobjPolicyTra = New ePolicy.Loans
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValVI011_k("VI011", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("valCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValVI011("VI011", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInter_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExecute"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+VI012: Registro de abono de anticipo
            Case "VI012"
                mobjPolicyTra = New ePolicy.Improve_lo
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nProduct=" & .Form.Item("valProduct") & "&nCertif=" & .Form.Item("tcnCertif") & "&nMainAction=" & .QueryString.Item("nMainAction")

                        insValPolicyTra = mobjPolicyTra.insValVI012_k("VI012", .QueryString("nMainAction"), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then

                            Session("dPay_Date") = .Form.Item("tcdPay_Date")
                            Session("nAport") = .Form.Item("tcnAport")
                            Session("nSald_fin") = .Form.Item("tcnSald_fin")

                            insValPolicyTra = mobjPolicyTra.insValVI012("VI012", "PopUp", .QueryString("nMainAction"), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("tcnAport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdPay_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdLoan_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSald_ini"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSald_fin"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            insValPolicyTra = mobjPolicyTra.insValVI012("VI012", "Normal", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(Session("nAport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dPay_Date"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dblNull, eRemoteDB.Constants.dblNull)
                        End If
                    End If

                End With

            '+VI7000: Rescate de pólizas
            Case "VI7000"
                Session("dEffecDate") = Today
                With Request
                    If .QueryString.Item("nZone") = "1" Then

                        insValPolicyTra = mobjPolicyTra.InsValVI7000_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        insValPolicyTra = mobjPolicyTra.InsValVI7000(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeSurrReas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAvailBal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '**+ VI010: Switches
            '+ VI010: Cambios de fondos de inversión

            Case "VI010"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("nCurrency") = .Form.Item("cbeCurrency")
                        Session("nOrigin") = .Form.Item("cbeOrigin")

                        insValPolicyTra = mobjPolicyTra.insValVI010_k("VI010", "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then

                            If CBool(.Form.Item("chkActivFound")) Then
                                sActivefound = "1"
                            Else
                                sActivefound = "2"
                            End If
                            insValPolicyTra = mobjPolicyTra.insValVI010("VI010", "PopUp", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTotal_Amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUpdate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOrigin"), eFunctions.Values.eTypeData.etdDouble), sActivefound, mobjValues.StringToType(.Form.Item("tcnAvailable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValPolicyTra = vbNullString
                        End If
                    End If
                End With

            '**+ VI7002: Redirections
            '+ VI7002: Redirecciones

            Case "VI7002"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("sCertype") = "2"
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")

                        insValPolicyTra = mobjPolicyTra.insValVI7002_k("VI7002", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            If CBool(.Form.Item("chkActivFound")) Then
                                sActivefound = "1"
                            Else
                                sActivefound = "2"
                            End If
                            insValPolicyTra = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), .Form.Item("Sel"), "Popup", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 12, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivefound)
                        Else
                            insValPolicyTra = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), "1",  ,  ,  ,  , Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 12, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString)
                        End If
                    End If
                End With

            '+VIL733: Aniversario de coberturas (Productos de Vida)
            Case "VIL733"
                With Request
                    insValPolicyTra = mobjPolicyTra.insValVIL733_k("VIL733", Request.Form.Item("sOptExecute"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ Renovación de pólizas (Header)		
            Case "CA031_K"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    Session("nRenewal") = Request.Form.Item("optRenewal")
                    Session("nInfo") = Request.Form.Item("optInfo")
                    insValPolicyTra = MobjPolicy.insValCA031_k
                Else
                    insValPolicyTra = MobjPolicy.insValCA031(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, False), Session("sTypeCompanyUser"), Session("nInfo"), mobjValues.StringToDate(Request.Form.Item("tcdRendateFrom")), mobjValues.StringToDate(Request.Form.Item("tcdRenDateto")))
                End If
            '+CA099: Tratamiento de cotizaciones y solicitudes
            Case "CA099"
                mobjPolicyTra = New ePolicy.TConvertions
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA099_K(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valProduct_sBrancht"), mobjValues.StringToType(.Form.Item("cbeOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypeDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), Session("sSche_code"), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CA099A: Tratamiento de cotizaciones y solicitudes
            Case "CA099A"
                mobjPolicyTra = New ePolicy.TConvertions
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValPolicyTra = mobjPolicyTra.insValCA099(.QueryString("nOperat"), mobjValues.StringToType(.Form.Item("valNoConvers"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStat"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+CA767 Tratamiento de propuestas especiales
            Case "CA767"
                insValPolicyTra = vbNullString

            '+VI008: Reducción de capital o vigencia
            Case "VI008"
                mobjPolicyTra = New ePolicy.Certificat
                With Request
                    insValPolicyTra = mobjPolicyTra.insValVI008("VI008", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optReduction"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+VI806: Capitalización de fondos
            Case "VI806"
                mobjPolicyTra = New ePolicy.TMovprev_Capital
                With Request
                    If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValVI806("VI806", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+CA642: Cambio de frecuencia de pago
            Case "CA642"
                With Request
                    mobjPolicyTra = New ePolicy.Policy
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA642_k("CA642", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA642("CA642", mobjValues.StringToType(.Form.Item("tcdChangdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valNpayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNewpayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNewChangdat"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ VA650: Movimientos al valor póliza
            Case "VA650"
                With Request
                    mobjPolicyTra = New ePolicy.Account_Pol
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValVA650_K("VA650", "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optMovType"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        If .QueryString.Item("nTypemove") = "1" Then
                            insValPolicyTra = mobjPolicyTra.InsValVA650("VA650", mobjValues.StringToType(.Form.Item("hddnAmount"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    End If
                End With

            '+ VA669: Solicitud de Ilustracion de Poliza
            Case "VA669"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = ""
                        mobjPolicyTra = New ePolicy.Activelife
                        insValPolicyTra = mobjPolicyTra.insValVA669_K("VA669", mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        insValPolicyTra = ""
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            mobjPolicyTra = New ePolicy.Activelife
                            insValPolicyTra = mobjPolicyTra.insValVA669(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddIllustType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProjRent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAddPrem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTargetVP"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            '+ La unica ventana popup que actualiza es la de Plan de pagos                    
                            mobjPolicyTra = New ePolicy.Per_deposit
                            insValPolicyTra = mobjPolicyTra.InsValVA669Upd(.QueryString("sCodispl"), .QueryString("sAction"), "2", mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearPrem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddPolYears"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    End If

                End With

            Case "VI7700"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    insValPolicyTra = mobjPolicyTra.insValVI770_K("VI7700", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    insValPolicyTra = vbNullString
                End If

            '+ CA028_1: Desglose de prima del recibo
            Case "CA028_1"
                mobjPolicyTra = New ePolicy.TDetail_pre
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA028_1(.QueryString("sCodispl"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("dIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddType_detai"), mobjValues.StringToType(.Form.Item("hddDisexprc"), eFunctions.Values.eTypeData.etdLong, True))
                End With
            '+ CA028_1: Autorización de propuestas sin pago de primera prima
            Case "CA789"
                mobjPolicyTra = New ePolicy.ValPolicyTra
                insValPolicyTra = mobjPolicyTra.insValCA789_k(mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
            Case Else
                insValPolicyTra = "insValPolicyTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostPolicyTra: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Private Function insPostPolicyTra() As Boolean
        Dim lstrMessageCa028 As String
        Dim llngPayOrderTyp As Byte
        Dim chkSurrTot As String
        Dim lintCountCA051 As Integer
        Dim lblnP_data As Object
        Dim sCodisplOri As String
        Dim lstrMessageProposal As String
        Dim soptDev As String
        Dim llngnPayOrderTyp As Byte
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrMessage As String
        '-Objeto para transacciones batch	
        Dim lclsBatch_param As eSchedule.Batch_Param

        lblnPost = False

        Dim lclsErrors As eFunctions.Errors
        Dim mobjtRehabilitate As ePolicy.TRehabilitate
        Dim lclsGeneralObj As eGeneral.GeneralFunction
        Dim lclsGeneralCa028 As eGeneral.GeneralFunction
        Dim lobjWorksheet As eBatch.Worksheet
        Dim lobjSheet As eBatch.Colsheet
        Select Case Request.QueryString.Item("sCodispl")

        '+ CA032: Reverso de modificación/renovación de una póliza
            Case "CA032"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif")
                        lblnPost = True
                    Else
                        mobjPolicyTra = New ePolicy.ValPolicyTra
                        lblnPost = mobjPolicyTra.insPostCA032("CA032", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNullOutMov"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctReverCertif"), mobjValues.StringToType(.Form.Item("chkNullPropQuot"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+CA033: Anulación de una póliza                
            Case "CA033"
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nProponum=" & Request.QueryString.Item("nProponum")

                        Session("sCertype") = "2"
                        Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                        If mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            Session("nCertif") = 0
                        Else
                            Session("nCertif") = mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                        End If
                        Session("nOffice") = mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nOfficeAgen") = mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nAgency") = mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble)
                        Session("optExecute") = .Form.Item("optExecute")
                        If IsNothing(.QueryString("sCodisplOri")) Then
                            Session("sCodisplOri") = "CA033"
                        Else
                            Session("sCodisplOri") = .QueryString.Item("sCodisplOri")
                        End If
                        lblnPost = True
                    Else
                        lblnPost = mobjPolicyTra.insPostCA033(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), "CA033", Session("sCodisplOri"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTransacion"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("optReceipt"), eFunctions.Values.eTypeData.etdDouble), Session("OptExecute"), .Form.Item("ChkNullRequest"), .Form.Item("chkNullReceipt"), Session("nOperat"), mobjValues.StringToType(.QueryString.Item("nNoteNum"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sDescript"), Session("nAgency"), .Form.Item("optDev"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost = True Then
                            '+ Si el usuario pidió generar propuesta, se recupera el número de propuesta creado
                            '+ para la póliza/certificado en tratamiento, y se muestra el mensaje al usuario
                            If CDbl(.Form.Item("ChkNullRequest")) = 1 And CStr(Session("OptExecute")) = "1" Then
                                lclsCertificat = New ePolicy.Certificat
                                llngProposal = lclsCertificat.insProposal_of_Pol("8", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                                lclsCertificat = Nothing
                                lclsErrors = New eFunctions.Errors
                                '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.32
                                lclsErrors.sSessionID = Session.SessionID
                                lclsErrors.nUsercode = Session("nUsercode")
                                '~End Body Block VisualTimer Utility
                                Response.Write(lclsErrors.ErrorMessage("CA033", 55940,  ,  , llngProposal, True))
                                lclsErrors = Nothing
                            Else
                                llngProposal = Request.QueryString.Item("nProponum")
                            End If


                            Select Case .Form.Item("optReceipt")
                                Case "3"
                                    Session("sCodisplRep") = "CA033"
                                    Session("dEffecdate") = ""

                                    Response.Write("" & vbCrLf)
                                    Response.Write("                                <SCRIPT>" & vbCrLf)
                                    Response.Write("									var lstrQueryString" & vbCrLf)
                                    Response.Write("									lstrQueryString = '&sCertype=")


                                    Response.Write(Session("sCertype"))


                                    Response.Write("' " & vbCrLf)
                                    Response.Write("									                + '&nBranch=")


                                    Response.Write(Session("nBranch"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nProduct=")


                                    Response.Write(Session("nProduct"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nPolicy=")


                                    Response.Write(Session("nPolicy"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nCertif=")


                                    Response.Write(Session("nCertif"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nAgency=")


                                    Response.Write(Session("nAgency"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&dNullDate=")


                                    Response.Write(Request.Form.Item("tcdNullDate"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&sNullReceipt=")


                                    Response.Write(Request.Form.Item("chkNullReceipt"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&soptReceipt=")


                                    Response.Write(Request.Form.Item("optReceipt"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&sExeReport=")


                                    Response.Write(Request.Form.Item("chkNullReport"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nExeMode=")


                                    Response.Write(Session("OptExecute"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&sCodispl=CA028'" & vbCrLf)
                                    Response.Write("									                + '&nProponum=")


                                    Response.Write(llngProposal)


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("                                    ShowPopUp('/VTimeNet/Policy/PolicyTra/CA028.aspx?sCodisplRep=' + 'CA033' + '&WinType=' + 'PopUp' + '&nAction=' + '392' + '&sCodisplOrig=CA033_CA028' + lstrQueryString,'Receipts','700','500','yes','No',40,40);" & vbCrLf)
                                    Response.Write("                                </" & "SCRIPT>" & vbCrLf)
                                    Response.Write("                                ")



                                Case "2"

                                    Session("sCertype") = "2"
                                    If CStr(Session("nCertif")) = "0" Then
                                        Session("nTransaction") = "29"
                                    ElseIf CStr(Session("nCertif")) > "0" Then
                                        Session("nTransaction") = "30"
                                    End If
                                    Session("dLedgerDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                                    Session("dEffecdate") = .Form.Item("tcdNullDate")
                                    Session("sCodisplOri") = "CA033"

                                    If Request.Form.Item("optDev") = "2" Then
                                        soptDev = "1"
                                    ElseIf Request.Form.Item("optDev") = "3" Then
                                        soptDev = "2"
                                    ElseIf Request.Form.Item("optDev") = "4" Then
                                        soptDev = "3"
                                    End If

                                    Response.Write("" & vbCrLf)
                                    Response.Write("" & vbCrLf)
                                    Response.Write("                                <SCRIPT>" & vbCrLf)
                                    Response.Write("									var lstrQueryString" & vbCrLf)
                                    Response.Write("									lstrQueryString = '&sCertype=")


                                    Response.Write(Session("sCertype"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nBranch=")


                                    Response.Write(Session("nBranch"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nProduct=")


                                    Response.Write(Session("nProduct"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nPolicy=")


                                    Response.Write(Session("nPolicy"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nCertif=")


                                    Response.Write(Session("nCertif"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nAgency=")


                                    Response.Write(Session("nAgency"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&dNullDate=")


                                    Response.Write(Request.Form.Item("tcdNullDate"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&sNullReceipt=")


                                    Response.Write(Request.Form.Item("chkNullReceipt"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&soptReceipt=")


                                    Response.Write(Request.Form.Item("optReceipt"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&sExeReport=")


                                    Response.Write(Request.Form.Item("chkNullReport"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nPercent=")


                                    Response.Write(Request.Form.Item("tcnPercent"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nProponum=")


                                    Response.Write(llngProposal)


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("									                + '&nNullCode=")


                                    Response.Write(Request.Form.Item("valNullCode"))


                                    Response.Write("'" & vbCrLf)
                                    Response.Write("                                    ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?sCodispl=CA027&soptDev=")


                                    Response.Write(soptDev)


                                    Response.Write("&nExeMode=")


                                    Response.Write(Session("OptExecute"))


                                    Response.Write("' + lstrQueryString,'Receipts','700','450','yes','No',40,40);" & vbCrLf)
                                    Response.Write("                                </" & "SCRIPT>" & vbCrLf)
                                    Response.Write("                                ")


                            End Select

                            If .Form.Item("optReceipt") <> "2" Then
                                lblnP_data = mobjPolicyTra.UpdatePartic_data(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            End If

                            If CDbl(.Form.Item("chkNullReport")) = 1 And .Form.Item("optReceipt") <> "3" And .Form.Item("optReceipt") <> "2" Then
                                insPrintPolicyRep(("CAL033"))
                            End If
                        End If
                    End If
                End With

            '+CA034: Rehabilitación de una póliza
            Case "CA034"
                With Request
                    If CStr(Session("sCodispl")) = "CA767" Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            mstrQueryString = mstrQueryString & "&nProponum=" & Request.QueryString.Item("nProponum")
                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("nPolicy") = .Form.Item("tcnPolicy")
                            Session("nCertif") = .Form.Item("tcnCertif")
                            Session("optExecute") = .Form.Item("optExecute")
                            Session("nAgency") = .Form.Item("cbeAgency")
                            Session("sCertype") = "2"
                            Session("nProponum") = Request.QueryString.Item("nProponum")
                            If CDbl(.Form.Item("tcnCertif")) = 0 Then
                                Session("nTransaction") = 31
                            Else
                                Session("nTransaction") = 32
                            End If
                            lblnPost = True
                        Else
                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.insPostCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nExeMode"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form.Item("chkNullDevRec"), .Form.Item("chkNullReceipt"), .QueryString("nExeMode"), .Form.Item("chkRescRequest"), Session("nOperat"), mobjValues.StringToType(.Form.Item("nDay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullLetter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))

                            sBranchtCA034 = mobjPolicyTra.sBrancht

                            If mobjPolicyTra.sBrancht = "1" And mobjPolicyTra.nProdclas = 7 And lblnPost Then
                                Session("sCodisplOri") = "CA034"
                                Session("dEffecdate") = Request.Form.Item("tcdNullDate")

                                Response.Write("" & vbCrLf)
                                Response.Write("								<SCRIPT>" & vbCrLf)
                                Response.Write("									var lstrQueryString" & vbCrLf)
                                Response.Write("									lstrQueryString = '&nAgency=")


                                Response.Write(Session("nAgency"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sCodispl=CA027'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sExeReport=")


                                Response.Write(Request.Form.Item("chkRescReport"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nBranch=")


                                Response.Write(Session("nBranch"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nProduct=")


                                Response.Write(Session("nProduct"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nPolicy=")


                                Response.Write(Session("nPolicy"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nCertif=")


                                Response.Write(Session("nCertif"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&dNullDate=")


                                Response.Write(Request.Form.Item("tcdNullDate"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&chkNullReceipt=")


                                Response.Write(Request.Form.Item("chkNullReceipt"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sBrancht=")


                                Response.Write(sBranchtCA034)


                                Response.Write("'" & vbCrLf)
                                Response.Write("									ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?soptDev=2&nExeMode=")


                                Response.Write(Request.QueryString.Item("nExeMode"))


                                Response.Write("' + lstrQueryString,'Receipts','700','450','yes','No',200,200);" & vbCrLf)
                                Response.Write("								</" & "SCRIPT>" & vbCrLf)
                                Response.Write("						    ")


                            Else
                                If mobjPolicyTra.sBrancht = "1" And CStr(Session("sPolitype")) = "1" And lblnPost Then

                                    mobjtRehabilitate = New ePolicy.TRehabilitate
                                    Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nExeMode")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), 1)
                                    Session("sKey") = mobjtRehabilitate.sKey
                                    mobjtRehabilitate = Nothing
                                    '+ Llamada al procedimiento que invoca al reporte
                                    If .Form.Item("chkRescReport") = "1" Then
                                        Call insPrintPolicyRep("CAL034")
                                    End If

                                Else
                                    If .Form.Item("chkRescReport") = "1" And lblnPost Then
                                        mobjtRehabilitate = New ePolicy.TRehabilitate
                                        Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nExeMode")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble))
                                        Session("sKey") = mobjtRehabilitate.sKey
                                        mobjtRehabilitate = Nothing
                                        '+ Llamada al procedimiento que invoca al reporte
                                        Call insPrintPolicyRep("CAL034")
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("nPolicy") = .Form.Item("tcnPolicy")
                            Session("nCertif") = .Form.Item("tcnCertif")
                            Session("optExecute") = .Form.Item("optExecute")
                            Session("nAgency") = .Form.Item("cbeAgency")
                            Session("sCertype") = "2"
                            If CDbl(.Form.Item("tcnCertif")) = 0 Then
                                Session("nTransaction") = 31
                            Else
                                Session("nTransaction") = 32
                            End If
                            If IsNothing(.QueryString("sCodisplOri")) Then
                                Session("sCodisplOri") = "CA033"
                            Else
                                Session("sCodisplOri") = .QueryString.Item("sCodisplOri")
                            End If
                            lblnPost = True
                        Else
                            lblnPost = True
                            Session("dEffecdate") = mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate)

                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.insPostCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nExeMode"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkNullDevRec"), .Form.Item("chkNullReceipt"), .QueryString("nExeMode"), .Form.Item("chkRescRequest"), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nDay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullLetter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                            If lblnPost Then
                                Session("nProposal") = mobjPolicyTra.nProposal
                                lclsGeneralObj = New eGeneral.GeneralFunction
                                If .Form.Item("chkRescRequest") = "1" And Request.QueryString.Item("nExeMode") = "1" Then
                                    lstrMessageProposal = lclsGeneralObj.insLoadMessage(55940) & " " & mobjPolicyTra.nProposal
                                    Response.Write("<SCRIPT>alert(""Men. 55940: " & lstrMessageProposal & """);</" & "Script>")
                                End If
                                If mobjPolicyTra.sBrancht <> "1" Or CStr(Session("spolitype")) <> "1" Then
                                    Response.Write("<SCRIPT>alert(""Ultima facturación " & mobjPolicyTra.dNextReceip & ", debe generar cargos hasta la fecha Actual " & """);</" & "Script>")
                                End If
                                lclsGeneralObj = Nothing
                            End If

                            sBranchtCA034 = mobjPolicyTra.sBrancht

                            If mobjPolicyTra.sBrancht = "1" And mobjPolicyTra.nProdclas = 7 And lblnPost Then
                                Session("sCodisplOri") = "CA034"

                                Response.Write("" & vbCrLf)
                                Response.Write("								<SCRIPT>" & vbCrLf)
                                Response.Write("									var lstrQueryString" & vbCrLf)
                                Response.Write("									lstrQueryString = '&nAgency=")


                                Response.Write(Session("nAgency"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sCodispl=CA027'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sExeReport=")


                                Response.Write(Request.Form.Item("chkRescReport"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nBranch=")


                                Response.Write(Session("nBranch"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nProduct=")


                                Response.Write(Session("nProduct"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nPolicy=")


                                Response.Write(Session("nPolicy"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&nCertif=")


                                Response.Write(Session("nCertif"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&dNullDate=")


                                Response.Write(Request.Form.Item("tcdNullDate"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&chkNullReceipt=")


                                Response.Write(Request.Form.Item("chkNullReceipt"))


                                Response.Write("'" & vbCrLf)
                                Response.Write("									lstrQueryString = lstrQueryString + '&sBrancht=")


                                Response.Write(sBranchtCA034)


                                Response.Write("'									" & vbCrLf)
                                Response.Write("									ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?soptDev=2&nExeMode=")


                                Response.Write(Request.QueryString.Item("nExeMode"))


                                Response.Write("' + lstrQueryString,'Receipts','700','450','yes','No',200,200);" & vbCrLf)
                                Response.Write("								</" & "SCRIPT>" & vbCrLf)
                                Response.Write("						    ")


                            Else
                                If mobjPolicyTra.sBrancht = "1" And CStr(Session("sPolitype")) = "1" And lblnPost Then
                                    mobjtRehabilitate = New ePolicy.TRehabilitate
                                    Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nExeMode")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), 1)
                                    Session("sKey") = mobjtRehabilitate.sKey
                                    mobjtRehabilitate = Nothing
                                    '+ Llamada al procedimiento que invoca al reporte
                                    If .Form.Item("chkRescReport") = "1" Then
                                        Call insPrintPolicyRep("CAL034")
                                    End If

                                Else
                                    If .Form.Item("chkRescReport") = "1" And lblnPost Then
                                        mobjtRehabilitate = New ePolicy.TRehabilitate
                                        Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nExeMode")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble))
                                        Session("sKey") = mobjtRehabilitate.sKey
                                        mobjtRehabilitate = Nothing
                                        '+ Llamada al procedimiento que invoca al reporte
                                        Call insPrintPolicyRep("CAL034")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End With

            '+CA035: Suspensión de Garantias a una Póliza
            Case "CA035"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        If .Form.Item("tcnCertif") = vbNullString Then
                            Session("nCertif") = 0
                        Else
                            Session("nCertif") = .Form.Item("tcnCertif")
                        End If
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        lblnPost = True
                    Else
                        mobjPolicyTra = New ePolicy.ValPolicyTra
                        lblnPost = mobjPolicyTra.insPostCA035("CA035", .QueryString("nMainAction"), 2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctMailnum"), mobjValues.StringToType(.Form.Item("cbeCode_sus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdPolExpirdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost = True Then
                            Select Case .Form.Item("optReceipt")
                                Case "1"
                                Case "2"

                                    Response.Write("" & vbCrLf)
                                    Response.Write("                                <SCRIPT>" & vbCrLf)
                                    Response.Write("                                    ShowPopUp('/VTimeNet/Policy/Policytra/CA028.aspx?sCodispl=CA028&sCodisplRep=CA048','CA028PopUp','700','500','yes','no',20,20);" & vbCrLf)
                                    Response.Write("                                </" & "SCRIPT>" & vbCrLf)
                                    Response.Write("                                ")


                                Case "3"

                                    Response.Write("" & vbCrLf)
                                    Response.Write("                                <SCRIPT>" & vbCrLf)
                                    Response.Write("                                    ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?sCodispl=CA027','Receipts','700','450','yes','No',200,200);" & vbCrLf)
                                    Response.Write("                                </" & "SCRIPT>" & vbCrLf)
                                    Response.Write("                                ")


                            End Select
                        End If
                    End If
                End With

            '+CA888: Reinstalación de una póliza (Modificación incompleta)
            Case "CA888"
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    lblnPost = mobjPolicyTra.insPostCA888_k("Update", "CA888", mstrpoli_type, .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, mobjValues.StringToType(.Form.Item("valUsers"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CA028, CA028A: Emisión de recibo manual
            Case "CA028", "CA028A"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        Session("sCertype") = "2"
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("dEffecdate") = vbNullString
                    Else
                        mobjPolicyTra = New ePolicy.TDetail_pre
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.insPostCA028Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                        Else
                            '+Se es un recibo de cobro o la forma de pago es:
                            '+3 Cargo cuenta corriente poliza 
                            '+4 Cargo cuenta corriente cliente
                            mstrCodispl = "CA028"
                            If .Form.Item("cbePayWay") > "2" Or .Form.Item("optType") = "1" Then
                                lblnPost = mobjPolicyTra.insPostCA028(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient_policy"), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctOrigReceipt"), Session("nUsercode"), Session("OptExecute"), Request.Form.Item("chkDelReceipt"), Request.Form.Item("hddKey"), .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("tcnAdjReceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayWay"), eFunctions.Values.eTypeData.etdLong, True), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("hddClient_policy"))
                                If lblnPost And Request.Form.Item("chkDelReceipt") <> "1" And mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, 0) = eRemoteDB.Constants.intNull Then
                                    '+ Se envia alerta con número de recibo generado 
                                    lclsGeneralCa028 = New eGeneral.GeneralFunction
                                    lstrMessageCa028 = lclsGeneralCa028.insLoadMessage(5064) & " con Nro.: " & mobjPolicyTra.nReceipt
                                    Response.Write("<SCRIPT>alert(""Men. 5064: " & lstrMessageCa028 & """);</" & "Script>")
                                    lclsGeneralCa028 = Nothing
                                    '+ Se genera el reporte 
                                    Session("sKey") = mobjPolicyTra.sKey
                                    If Request.QueryString.Item("sExeReport") = "1" Then
                                        insPrintPolicyRep(("CAL033"))
                                    End If
                                End If

                                '+Se llama a la OP06-2 si la forma de pago si es:
                                '+1 Orden de pago                
                                '+2 Cargo a cuenta cte bancaria
                            Else
                                lblnPost = True

                                Session("OP006_sCodispl") = "CA028"
                                mstrCodispl = "OP06-2"
                                mstrQueryString = "&sCodisplOri=CA028" & "&nConcept=24" & "&dEffecdate=" & .Form.Item("tcdStartDateR") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("hddAmountTot") & "&nCurrencypay=1" & "&nAmountPay=" & .Form.Item("hddAmountTotPay") & "&nPayOrderTyp=2" & "&sCertype=2" & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&sClient=" & .Form.Item("hddClient_policy") & "&sBenef=" & .Form.Item("hddClient_policy") & "&nBranchPay=" & .Form.Item("cbeBranchPay") & "&nProductPay=" & .Form.Item("valProductPay") & "&nPolicyPay=" & .Form.Item("tcnPolicyPay") & "&nCertifPay=" & .Form.Item("tcnCertifPay") & "&nBalance=" & "" & "&nOperat=" & "" & "&sAnulReceipt=" & "" & "&sReport=" & "" & "&nOffice=" & "" & "&nOfficeAgen=" & "" & "&nAgency=" & "" & "&nReceipt=" & .Form.Item("tcnReceipt") & "&dExpirDat=" & .Form.Item("tcdExpirDateR") & "&nSource=" & .Form.Item("cbeSource") & "&nTypeReceipt=" & .Form.Item("optType") & "&sOrigReceipt=" & .Form.Item("tctOrigReceipt") & "&sKey=" & .Form.Item("hddKey") & "&sAdjust=" & .Form.Item("chkAdjust") & "&nAdjReceipt=" & .Form.Item("tcnAdjReceipt") & "&nAdjAmount=" & .Form.Item("tcnAdjAmount") & "&nTypePay=" & .Form.Item("cbePayWay")

                            End If
                        End If
                    End If
                End With

            '+CA038: Cambio de fecha de renovación
            Case "CA038"
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    lblnPost = mobjPolicyTra.insPostCA038_k(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdExpirdate")), mobjValues.StringToDate(.Form.Item("tcdNextReceip")), Session("sColtimre"), mobjValues.StringToDate(.Form.Item("tcdFromDate")), Session("sPolitype"), .Form.Item("sOptReceiptType"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    If lblnPost = True Then
                        Select Case .Form.Item("OptReceiptType")
                            Case "2"

                                Response.Write("        " & vbCrLf)
                                Response.Write("                            <SCRIPT>    " & vbCrLf)
                                Response.Write("                                ShowPopUp('/VTimeNet/Policy/Policytra/CA028.aspx?sCodispl=CA028&sCodisplRep=CA048','CA028PopUp','700','500','yes','no',20,20);" & vbCrLf)
                                Response.Write("                            </" & "SCRIPT>     " & vbCrLf)
                                Response.Write("                            ")


                            Case "3"

                                Response.Write("        " & vbCrLf)
                                Response.Write("                            <SCRIPT>    " & vbCrLf)
                                Response.Write("                                ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?sCodispl=CA027','Receipts','700','450','yes','No',200,200);" & vbCrLf)
                                Response.Write("                            </" & "SCRIPT>     " & vbCrLf)
                                Response.Write("                            ")


                        End Select
                    End If
                End With

            '+CA037: Cambio de fecha de Efecto
            Case "CA037"
                With Request
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    lblnPost = mobjPolicyTra.insPostCA037_k("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate))


                    If lblnPost Then
                        Select Case .Form.Item("optReceiptType")
                            Case "2"

                                Response.Write("" & vbCrLf)
                                Response.Write("                            <SCRIPT>    " & vbCrLf)
                                Response.Write("                                ShowPopUp('/VTimeNet/Policy/Policytra/CA028.aspx?sCodispl=CA028&sCodisplRep=CA048','CA028PopUp','700','500','yes','no',20,20);" & vbCrLf)
                                Response.Write("                            </" & "SCRIPT>     " & vbCrLf)
                                Response.Write("                            ")

                            Case "3"
                                Response.Write("" & vbCrLf)
                                Response.Write("                            <SCRIPT>" & vbCrLf)
                                Response.Write("                                ShowPopUp('/VTimeNet/Policy/PolicySeq/CA027.aspx?sCodispl=CA027','Receipts','700','450','yes','No',200,200);" & vbCrLf)
                                Response.Write("                            </" & "SCRIPT>     " & vbCrLf)
                                Response.Write("                            ")


                        End Select
                    End If
                End With

            '+VI009: Rescate de póliza
            Case "VI009"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        mstrQueryString = "&sSurrType=" & .Form.Item("optSurrType") & "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nAgency=" & .Form.Item("cbeAgency") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sProcessType=" & .Form.Item("optProcessType") & "&sSurrPayWay=" & .Form.Item("cbeSurrPayWay") & "&sCodisplOri=" & .Form.Item("hddsCodisplOri") & "&nOperat=" & .Form.Item("hddnOperat") & "&nPropoNum=" & .Form.Item("tcnProponum") & "&nOffice=" & .Form.Item("cbeOffice") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&sAnulReceipt=" & .Form.Item("chkNullPrem")
                    Else
                        '+Se ejecuta el rescate si la forma de pago es:
                        '+3 Cargo cuenta corriente poliza 
                        '+4 Cargo cuenta corriente cliente
                        '+O la opción de ejecución de proceso es Preliminar
                        mstrCodispl = "VI009"
                        If .Form.Item("hddsSurrPayWay") > "2" Or .Form.Item("hddsProcessType") = "1" Then
                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.InsPostVI009("2", mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddsSurrType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddsProcessType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequest"), mobjValues.StringToType(.Form.Item("hddsSurrPayWay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBalance"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnOperat"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsAnulReceipt"))

                            If mobjPolicyTra.nProposal > 0 Then
                                nProposal = mobjPolicyTra.nProposal
                                mobjPolicyTra = Nothing
                                mobjPolicyTra = New eGeneral.GeneralFunction
                                Response.Write("<SCRIPT>alert(""Men. 55940: " & mobjPolicyTra.insLoadMessage(55940) & " " & nProposal & """);</" & "Script>")
                            End If

                            If .Form.Item("chkReport") = "1" And lblnPost Then
                                Call insPrintPolicyRep("VIL009")
                            End If
                            '+Se llama a la OP06-2 si la forma de pago si es:
                            '+1 Orden de pago                
                            '+2 Cargo a cuenta cte bancaria
                        Else
                            lblnPost = True
                            '+Se llama a la OP06-2 si la opción de ejecución es definitiva
                            If .Form.Item("hddsProcessType") = "2" Then
                                llngnPayOrderTyp = 2
                                If .Form.Item("hddsSurrPayWay") = "2" Then
                                    llngnPayOrderTyp = 4
                                End If
                                Session("OP006_sCodispl") = "VI009"
                                mstrCodispl = "OP06-2"
                                mstrQueryString = "&sCodisplOri=VI009" & "&sBenef=" & .Form.Item("tctClient") & "&nConcept=11" & "&dEffecdate=" & .Form.Item("hdddEffecdate") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("tcnRescDef") & "&nAmountPay=" & .Form.Item("tcnSurrCurr") & "&nPayOrderTyp=" & llngnPayOrderTyp & "&nBranch=" & .Form.Item("hddnBranch") & "&nProduct=" & .Form.Item("hddnProduct") & "&nPolicy=" & .Form.Item("hddnPolicy") & "&nCertif=" & .Form.Item("hddnCertif") & "&dRescdate=" & .Form.Item("hdddEffecdate") & "&sSurrType=" & .Form.Item("hddsSurrType") & "&sProcessType=" & .Form.Item("hddsProcessType") & "&sRequest=" & .Form.Item("chkRequest") & "&sSurrPayWay=" & .Form.Item("hddsSurrPayWay") & "&nSurrAmount=" & .Form.Item("tcnSurrAmount") & "&nCurrency=" & .Form.Item("hddnCurrency") & "&nCurrencypay=1" & "&sClient=" & .Form.Item("tctClient") & "&nBranchPay=" & .Form.Item("cbeBranch") & "&nProductPay=" & .Form.Item("valProduct") & "&nPolicyPay=" & .Form.Item("tcnPolicy") & "&nCertifPay=" & .Form.Item("tcnCertif") & "&nProponum=" & .Form.Item("hddnProponum") & "&nBalance=" & .Form.Item("hddnBalance") & "&nOperat=" & .Form.Item("hddnOperat") & "&sAnulReceipt=" & .Form.Item("hddsAnulReceipt") & "&sReport=" & .Form.Item("chkReport") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&tcnCapital=" & .Form.Item("tcnCapital")
                            End If
                        End If
                    End If
                End With

            '+VI7000: Rescate de póliza
            Case "VI7000"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        lblnPost = True

                        mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&sProcess=" & .Form.Item("optProcessType") & "&nOffice=" & .Form.Item("cbeOffice") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&nAgency=" & .Form.Item("cbeAgency") & "&sClientBenef=" & .Form.Item("hddClientBenef") & "&nProponum=" & .Form.Item("tcnProponum")

                        Session("sCertype") = "2"
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("nCurrency") = .Form.Item("cbeCurrency")
                        Session("optProcessType") = .Form.Item("optProcessType")
                    Else
                        If .Form.Item("chkSurrTot") = "1" Then
                            chkSurrTot = "1"
                        Else
                            chkSurrTot = "0"
                        End If

                        '+ Si se trata de un rescate preliminar, se hace el llamado a la función que creará la propuesta y reportará el número generado al User
                        '+ Con el parámetro sProcessType = "1" se le indica que no actualice las tablas concernientes a fondos pues es un rescate preliminar

                        If CStr(Session("optProcessType")) = "1" Then
                            mobjPolicyTra = New ePolicy.ValPolicyTra

                            '+ Modificación [APV2] - ACM - 17/09/2003

                            lblnPost = mobjPolicyTra.InsPostVI7000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjValues.StringToType(.Form.Item("tcnSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("cbeEntFinDes"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "1", mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True))

                            If mobjPolicyTra.nProposal > 0 Then
                                llngProposal = mobjPolicyTra.nProposal
                                mobjPolicyTra = Nothing
                                mobjPolicyTra = New eGeneral.GeneralFunction
                                Response.Write("<SCRIPT>alert(""Men. 55940: " & mobjPolicyTra.insLoadMessage(55940) & " " & llngProposal & """);</" & "Script>")
                            End If
                        Else
                            lblnPost = True
                            '+ Sólo si se solicita la solicitud de orden de pago se irá a dicha transacción
                            If CDbl(.Form.Item("cbePmtOrd")) = 1 Then


                                If IsNothing(.Form.Item("dtcClient")) Then
                                    Session("OP006_sBenef") = .Form.Item("hddClientBenef")
                                Else
                                    Session("OP006_sBenef") = .Form.Item("dtcClient")
                                End If

                                llngPayOrderTyp = 2

                                Session("OP006_sCodispl") = "VI7000"
                                Session("OP006_nPayOrderTyp") = "2"

                                mstrCodispl = "OP06-2"

                                mstrQueryString = "&sCodisplOri=VI7000" & "&sBenef=" & Session("OP006_sBenef") & "&nConcept=25" & "&dEffecdate=" & Session("dEffecdate") & "&nAmount=" & .Form.Item("tcnTotal") & "&nAmountPay=" & .Form.Item("tcnTotal") & "&nPayOrderTyp=" & llngPayOrderTyp & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dRescdate=" & Session("dEffecdate") & "&sSurrType=" & .Form.Item("chkSurrTot") & "&nSurrAmt=" & .Form.Item("tcnSurrAmt") & "&nCurrency=" & Session("nCurrency") & "&sClient=" & Session("OP006_sBenef") & "&nBranchPay=" & Session("nBranch") & "&nProductPay=" & Session("nProduct") & "&nPolicyPay=" & Session("nPolicy") & "&nCertifPay=" & Session("nCertif") & "&sCertype=" & Session("sCertype") & "&sSurrTot=" & chkSurrTot & "&nCoverCost=" & .Form.Item("hddnCoverCost") & "&nPmtOrd=" & .Form.Item("cbePmtOrd") & "&nSurrReas=" & .Form.Item("cbeSurrReas") & "&nRetention=" & .Form.Item("tcnRetention") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&sProcess=" & .Form.Item("hddProcess") & "&nProponum=" & .Form.Item("hddProponum") & "&nEntity=" & .Form.Item("cbeEntFinDes") & "&sClientEnt=" & .Form.Item("dtcClient") & "&nOrigin_apv=" & .Form.Item("valOrigin")
                            End If
                        End If
                    End If
                End With

            '+VI011: Registro de Anticipos
            Case "VI011"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        mstrQueryString = "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sCertype=" & .Form.Item("tctCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nValCode=" & .Form.Item("valCode") & "&nOffice=" & .Form.Item("cbeOffice") & "&nAgency=" & .Form.Item("cbeAgency") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&nCurrency=" & .Form.Item("tcnCurrency") & "&sCodisplOri=" & .Form.Item("tctCodisplOri") & "&nExecute=" & .Form.Item("optExecute") & "&nAmount=" & .QueryString.Item("nAmount")
                    Else
                        If .QueryString.Item("nMainAction") <> "401" Then
                            mobjPolicyTra = New ePolicy.Loans
                            '+ Si la ejecución es preliminar
                            If CDbl(.Form.Item("optExecute")) = 1 Then
                                '+ Si se desea crear la solicitud del préstamo/anticipo
                                lblnPost = mobjPolicyTra.insPostVI011(.Form.Item("tctCodisplOri"), mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExecute"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInter_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOperat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmoTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequest"), eRemoteDB.Constants.intNull, Session("SessionID"), eRemoteDB.Constants.intNull, .Form.Item("tcnSurrVal"), .Form.Item("tcnMaxAmount"), .Form.Item("tcnLoans"))

                                If lblnPost Then
                                    '+ Se indica el nro. de propuesta generado 
                                    If mobjPolicyTra.nCode > 0 Then
                                        lclsGeneral = New eGeneral.GeneralFunction
                                        lstrMessage = lclsGeneral.insLoadMessage(55940) & " " & mobjPolicyTra.nCode
                                        Response.Write("<SCRIPT>alert(""Men. 55940: " & lstrMessage & """);</" & "Script>")
                                        lclsGeneral = Nothing
                                    End If
                                End If
                                '+ Se muestra el reporte con los datos preliminares del préstamo
                                insPrintPolicyRep(("VI011"))
                            Else
                                lblnPost = True
                                mstrCodispl = "OP06-2"
                                Session("OP006_sCodispl") = "VI011"
                                Session("OP006_dReqDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)

                                mstrQueryString = "&sCodisplOri=" & .Form.Item("tctCodisplOri") & "&sBenef=" & .Form.Item("tctClient") & "&nConcept=10" & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nOffice=" & .Form.Item("cbeOffice") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&nOfficepay=" & .Form.Item("cbeOffice") & "&nAmount=" & .Form.Item("hddFinalOri") & "&nAmountPay=" & .Form.Item("hddFinal") & "&nPayOrderTyp=" & .Form.Item("cbePayOrder") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dRescdate=" & .Form.Item("tcdEffecdate") & "&sProcessType=" & .Form.Item("optExecute") & "&sRequest=" & .Form.Item("chkRequest") & "&nCurrency=" & .Form.Item("tcnCurrency") & "&nCurrencypay=1" & "&sClient=" & .Form.Item("tctClient") & "&nBranchPay=" & .Form.Item("cbeBranch") & "&nProductPay=" & .Form.Item("valProduct") & "&nPolicyPay=" & .Form.Item("tcnPolicy") & "&nCertifPay=" & .Form.Item("tcnCertif") & "&nAgency=" & .Form.Item("cbeAgency") & "&nAmotax=" & .Form.Item("tcnAmotax") & "&nInterest=" & .Form.Item("tcnInter_year") & "&nSurrVal=" & .Form.Item("tcnSurrVal") & "&nMaxAmount=" & .Form.Item("tcnMaxAmount") & "&nLoans=" & .Form.Item("tcnLoans")
                            End If
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '+VI012: Registro de abono de anticipos
            Case "VI012"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("nAport") = 0
                        Session("dPay_Date") = vbNullString

                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then

                            lblnPost = mobjPolicyTra.insPostVI012("VI012", .QueryString("nMainAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLoan_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '**+ VI010: Switches
            '+ VI010: Cambios de fondos de inversión

            Case "VI010"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                        lblnPost = True
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            mobjPolicyTra = New ePolicy.ValPolicyTra

                            lblnPost = mobjPolicyTra.insPostVI010("VI010", .QueryString("nMainAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTotal_Amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBuy_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSwi_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDeb_acc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOrigin"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            Case "VI7002"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If CBool(.Form.Item("chkActivFound")) Then
                            sActivefound = "1"
                        Else
                            sActivefound = "2"
                        End If
                        lblnPost = lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), "Add", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.dtmNull, 12, sActivefound, "1")
                    Else
                        lblnPost = True
                    End If
                End With

            '+ Renovación de pólizas (Header)		
            Case "CA031_K", "CA031"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    lblnPost = MobjPolicy.insPostCA031_k
                Else

                    '+Tipo de proceso/reporte
                    lintTypeRepCAL005 = 0
                    If Session("nRenewal") = 1 Then
                        If Session("nInfo") = 1 Then
                            lintTypeRepCAL005 = 1 '+Renovacion preliminar masiva
                        Else
                            lintTypeRepCAL005 = 2 '+Renovacion preliminar puntual
                        End If
                    Else
                        If Session("nInfo") = 1 Then
                            lintTypeRepCAL005 = 3 '+Renovacion definitiva masiva
                        Else
                            lintTypeRepCAL005 = 4 '+Renovacion definitiva puntual
                        End If
                    End If

                    If CStr(Session("BatchEnabled")) <> "1" Then
                        lblnPost = MobjPolicy.insPostCA031(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdRendateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdRenDateto"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.strNull)
                        sKey = MobjPolicy.sKey
                        '+Preliminar y Renovacion puntual				    
                        If Session("nInfo") = 2 And lblnPost Then
                            Call insPrintPolicyRep("CA031")
                        Else
                            '+Renovacion masiva
                            If Session("nInfo") = 1 And lblnPost Then
                                Call insPrintPolicyRep("CAL005")
                            End If
                        End If

                    Else

                        lclsBatch_param = New eSchedule.Batch_Param

                        '+La siguiente condicion se incluyó para replicar lo presente en el método insPostCA031 (!!!)
                        '+Si es masiva
                        If CStr(Session("nInfo")) = "1" Then
                            With lclsBatch_param
                                .nBatch = 110
                                .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "") '+Poliza
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "") '+Certificado
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdRendateFrom"), eFunctions.Values.eTypeData.etdDate))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdRenDateto"), eFunctions.Values.eTypeData.etdDate))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "0")
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "0")
                                '+Renovacion definitiva
                                If mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 99)
                                    '+Renovacion preliminar
                                Else
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 98)
                                End If
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdRendateFrom"))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdRenDateto"))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "1") '+Masivo
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, lintTypeRepCAL005)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                                .Save()
                            End With
                            '+Si es puntual                    
                        Else
                            MobjPolicy.Find("2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            With lclsBatch_param
                                .nBatch = 110
                                .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                If MobjPolicy.sSimul = "1" Then
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "") '+Certificado
                                Else
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                                End If
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, MobjPolicy.dNextReceip)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "")
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "")
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "")
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, MobjPolicy.nIndexfac)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, MobjPolicy.nIndexFactMn)
                                '+Renovacion definitiva
                                If mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 99)
                                    '+Renovacion preliminar
                                Else
                                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 98)
                                End If
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdRendateFrom"))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdRenDateto"))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "2") '+Puntual
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, lintTypeRepCAL005)
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("hddIntermed"), eFunctions.Values.eTypeData.etdDouble))
                                .Save()
                            End With

                        End If

                        Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")

                        lclsBatch_param = Nothing

                        lblnPost = True
                    End If

                End If

            '+ CA051 Hojas de Excel para la carga de pólizas / certificado
            Case "CA051"

                mobjPolicyTra = New eBatch.ValBatch

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("sFile") = .Form.Item("tctFile")

                        lobjWorksheet = New eBatch.Worksheet
                        Session("nId") = lobjWorksheet.Generate(mobjValues.StringToType(.Form.Item("tcnWorksheet"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
                        lobjWorksheet = Nothing
                        If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then

                            lblnPost = mobjPolicyTra.insPostCA051_K(.QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, False), .Form.Item("tctDescript"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkList"), Session("nId"))
                        Else
                            lblnPost = True
                        End If
                    Else

                        If Request.QueryString.Item("WindowType") = "PopUp" Then

                            lblnPost = mobjPolicyTra.insPostCA051(.Form.Item("chkAuxSel"), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdRec"), eFunctions.Values.eTypeData.etdDouble), .QueryString("WindowType"), .Form.Item("tctColumnName"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequire"), Session("nUserCode"), .Form.Item("chkSelected"))


                        Else
                            If CDbl(.QueryString.Item("nMainAction")) <> 401 Then
                                For lintCountCA051 = 1 To CInt(.Form.Item("hddnCount"))
                                    lblnPost = mobjPolicyTra.insPostCA051(.Form.GetValues("hddsAuxSelh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(.Form.GetValues("hddnId").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnIdRec").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, False), .QueryString("WindowType"), .Form.GetValues("hddsColumnNameh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(.Form.GetValues("hddnOrderh").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, True), .Form.GetValues("hddsRequireh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.GetValues("hddsSelectedh").GetValue(lintCountCA051 - 1))

                                Next
                            Else
                                lblnPost = True
                            End If

                            If lblnPost And CStr(Session("sFile")) <> vbNullString Then

                                lobjSheet = New eBatch.Colsheet
                                'If lobjSheet.insQueryExportExcel(mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble, True), Session("sFile")) Then

                                'End If

                                lobjSheet.insQueryExportExcel(mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble, True), Session("sFile"))

                                lobjSheet = Nothing
                            End If
                        End If

                    End If

                End With

            '+CA099: Tratamiento de cotizaciones y solicitudes
            Case "CA099"
                mobjPolicyTra = New ePolicy.TConvertions
                With Request
                    If mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                        mstrQueryString = "&sCertype=" & mobjPolicyTra.CertypeByOrigin(mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypeDoc"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If mobjValues.StringToType(.Form.Item("optTypeDoc"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                            mstrQueryString = "&sCertype=3"
                        Else
                            mstrQueryString = "&sCertype=1"
                        End If
                    End If
                    mstrQueryString = mstrQueryString & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nCertif=" & .Form.Item("tcnCertif") & "&nOrigin=" & .Form.Item("valOrigin") & "&nProponum=" & .Form.Item("tcnProponum") & "&sClient=" & .Form.Item("dtcClient") & "&nStatus=" & .Form.Item("cbeStat") & "&nIntermed=" & .Form.Item("valIntermed") & "&nAgency=" & .Form.Item("valAgency") & "&sTypeDoc=" & .Form.Item("optTypeDoc") & "&sExpired=" & .Form.Item("chkDueDate") & "&dStartdate=" & .Form.Item("tcdEffecdate") & "&sBrancht=" & .Form.Item("valProduct_sBrancht") & "&nWaitCode=" & .Form.Item("cboWaitCode")

                    If IsNothing(.Form.Item("tcdEffecdate")) Then
                        Session("dEffecdate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                        mstrQueryString = mstrQueryString & "&datecont=1&dEffecdate=" & Session("dEffecdate")
                    Else
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        mstrQueryString = mstrQueryString & "&datecont=2&dEffecdate=" & Session("dEffecdate")
                    End If
                    If IsNothing(.Form.Item("cbeOperat")) Then
                        mstrQueryString = mstrQueryString & "&nOperat=0"
                        Session("nOperat") = "0"
                    Else
                        mstrQueryString = mstrQueryString & "&nOperat=" & .Form.Item("cbeOperat")
                        Session("nOperat") = .Form.Item("cbeOperat")
                    End If
                    If mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
                        Session("nCertif") = 0
                    Else
                        Session("nCertif") = .Form.Item("tcnCertif")
                    End If
                    Session("nOrigin") = .Form.Item("valOrigin")
                    Session("nBranch") = .Form.Item("cbeBranch")
                    Session("nProduct") = .Form.Item("valProduct")
                    If mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        Session("nPolicy") = ""
                    Else
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                    End If

                    If mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        Session("nProponum") = ""
                    Else
                        Session("nProponum") = .Form.Item("tcnProponum")
                    End If
                    Session("Action_CA099") = .QueryString.Item("nMainAction")
                End With
                lblnPost = True

            '+CA099A: Tratamiento de cotizaciones/solicitudes
            Case "CA099A"
                mobjPolicyTra = New ePolicy.TConvertions
                lblnPost = True
                With Request

                    mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nCertif=" & .QueryString.Item("nCertif") & "&nOrigin=" & .QueryString.Item("nOrigin") & "&nProponum=" & .QueryString.Item("nProponum") & "&sClient=" & .QueryString.Item("sClient") & "&nStatus=" & .QueryString.Item("nStat") & "&nIntermed=" & .QueryString.Item("nIntermed") & "&nAgency=" & .QueryString.Item("nAgency") & "&sTypeDoc=" & .QueryString.Item("sTypeDoc") & "&sExpired=" & .QueryString.Item("sExpired") & "&dStartdate=" & .QueryString.Item("dStartdate") & "&sBrancht=" & .QueryString.Item("sBrancht") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nOperat=" & .QueryString.Item("nOperat")
                    '+ Accion actualizar un registro de la tabla
                    If (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or CDbl(.QueryString.Item("nMainAction")) = 401) And .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicyTra.insPostCA099(.QueryString("WindowType"), "Update", mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDoc_pend"), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeStat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStatdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valNoConvers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdMaximun_da"), eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFirstPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrPrem"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPrem_cheq"), "2", mobjValues.StringToType(.Form.Item("tcnCollect"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDevolut"), .Form.Item("hddCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient"), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        '+ Accion actualizar transaccion  (no popup), a menos que sea consultar
                        If (.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Or .QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataAccept)) And .QueryString.Item("nOperat") <> "1" Then

                            lblnPost = mobjPolicyTra.insPostCA099("", "Update", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDoc_pend"), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), .QueryString("dEffecdate"), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", .Form.Item("hddScertype_aux"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble), Session("skey"))

                            '+ Se retorna cadena con llave de proceso
                            mstrMessage = mobjPolicyTra.sKey

                        End If
                    End If
                End With


            '+CA767: Tratamiento de propuestas especiales
            Case "CA767"
                mobjPolicyTra = New ePolicy.Request
                With Request

                    If CStr(Session("nOperat")) = "2" Or CStr(Session("nOperat")) = "5" Then
                        Select Case Session("nOrigin")
                            Case "4"
                                mstrCodispl = "CA033"
                            Case "5"
                                mstrCodispl = "CA034"
                            Case "6"
                                mstrCodispl = "VI008"
                            Case "7"
                                mstrCodispl = "VI008"
                            Case "8"
                                If CDbl(.Form.Item("hddProdClass")) = 3 Or CDbl(.Form.Item("hddProdClass")) = 4 Then
                                    mstrCodispl = "VI7000"
                                Else
                                    mstrCodispl = "VI009"
                                End If

                            Case "9"
                                mstrCodispl = "VI011"
                        End Select
                        lblnPost = True
                        mstrQueryString = "&sCodisplOri=CA767" & "&sDescript=" & Request.Form.Item("tctDescript") & "&nNotenum=" & Request.Form.Item("tcnNotenum") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nPropoNum=" & Session("nPropoNum") & "&dEffecdate=" & Session("dEffecdate") & "&nTypePay=" & .Form.Item("cbeTypepay") & "&sTyp_surr=" & .Form.Item("optTyp_surr") & "&nAgency=" & .Form.Item("hddnAgency") & "&nOperat=" & Session("nOperat") & "&nAmount=" & .Form.Item("tcnAmount") & "&nSurrReas" & .Form.Item("cbeSurrReas") & "&nOrigin=" & .Form.Item("valOrigin")
                    Else
                        mstrCodispl = "CA099"
                        mstrQueryString = "&nProponum=" & Session("nPolicy")
                        lblnPost = mobjPolicyTra.insPostCA767("CA767", mobjValues.StringToType(Session("Action_CA099"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optTyp_surr"), .Form.Item("cboPayorder"), .Form.Item("chkNull_Rec"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboNullcode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optTyp_rec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkReh_lrec"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboStatquota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboNo_convers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+VIL733: Aniversario de coberturas (Productos de Vida)
            Case "VIL733"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    With Request
                        lblnPost = mobjPolicyTra.insPostVIL733_k("VIL733", Request.Form.Item("sOptExecute"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                    If lblnPost Then
                        Call insPrintPolicyRep("VIL733")
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 119
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("sOptExecute"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdEffecdate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("sOptExecute"))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    lblnPost = True
                End If

            '+CAL006: Reservas de Primas
            Case "CAL006"

                With Request
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    If Request.Form.Item("cbeInsurArea") = "2" Then
                        lblnPost = mobjPolicyTra.insPostCAL006_k("CAL006", Request.Form.Item("cbeInsurArea"), Request.Form.Item("sOptDetail"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

                If lblnPost Then
                    If Request.Form.Item("cbeInsurArea") = "2" Then
                        Call insPrintPolicyRep("CAL006")
                    End If
                End If

            '+VI008: Reducción de capital o vigencia
            Case "VI008"
                With Request
                    mobjPolicyTra = New ePolicy.Certificat

                    sKeyVI008 = "TMP" & Session("SessionID") & Session("nUsercode")

                    If .Form.Item("hddCodisplOri") = "CA767" Then
                        sCodisplOri = .Form.Item("hddCodisplOri")
                    Else
                        sCodisplOri = "VI008"
                    End If

                    lblnPost = mobjPolicyTra.insPostVI008(sCodisplOri, mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExeMode"), .Form.Item("optReduction"), .Form.Item("chkNulling"), .Form.Item("chkGenProposal"), mobjValues.StringToType(.Form.Item("hddOperat"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "", mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), sKeyVI008)

                    If lblnPost Then
                        '+ Se indica el nro. de propuesta generado 
                        If mobjPolicyTra.nCode > 0 Then
                            lclsGeneral = New eGeneral.GeneralFunction
                            lstrMessage = lclsGeneral.insLoadMessage(55940) & " " & mobjPolicyTra.nCode
                            Response.Write("<SCRIPT>alert(""Men. 55940: " & lstrMessage & """);</" & "Script>")
                            lclsGeneral = Nothing
                        End If
                    End If

                    If .Form.Item("chkGenReport") = "1" Then
                        insPrintPolicyRep(("VIL008"))
                    End If
                End With

            '+VI806: Capitalización de Fondos (Previsión y Retiro)
            Case "VI806"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    With Request
                        If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                            mobjPolicyTra = New ePolicy.TMovprev_Capital
                            lblnPost = mobjPolicyTra.InsPostVI806("VI806", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("SessionID"), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))

                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            Session("nCertif") = .Form.Item("tcnCertif")
                            Session("nPolicy") = .Form.Item("tcnPolicy")
                        Else
                            mobjPolicyTra = New ePolicy.TMovprev_Capital
                            lblnPost = mobjPolicyTra.Copy_TMovprev_capital(Session("SessionID"))
                            Session("nBranch") = ""
                            Session("nProduct") = ""
                            Session("dEffecdate") = ""
                            Session("nCertif") = ""
                            Session("nPolicy") = ""
                        End If
                    End With
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 115
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        Session("sKey") = .sKey
                        If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                            mobjPolicyTra = New ePolicy.TMovprev_Capital
                            lblnPost = mobjPolicyTra.InsPostVI806("VI806", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            Session("nBranch") = Request.Form.Item("cbeBranch")
                            Session("nProduct") = Request.Form.Item("valProduct")
                            Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                            Session("nCertif") = Request.Form.Item("tcnCertif")
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                        Else
                            If Request.Form.Item("hddFindData") = "1" Then
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sKey"))
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                                .Save()
                                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & Session("sKey") & "');</" & "Script>")
                            End If
                            lblnPost = True
                        End If
                    End With
                    lclsBatch_param = Nothing
                End If
            '+CA642: Cambio de frecuencia de pago            
            Case "CA642"
                With Request
                    mobjPolicyTra = New ePolicy.Policy
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nTransaction") = 61
                        lblnPost = True
                    Else
                        lblnPost = mobjPolicyTra.InsPostCA642("CA642", Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("tcdNewChangdat"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnNewpayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNewNextreceip"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ VA650: Movimientos al valor póliza
            Case "VA650"
                With Request
                    mobjPolicyTra = New ePolicy.Account_Pol
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nTypemove=" & .Form.Item("optMovType") & "&sReload=" & "No"
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.InsPostVA650Upd(.QueryString("sKey"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True))
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nTypemove=" & .QueryString.Item("nTypemove") & "&sKey=" & .QueryString.Item("sKey") & "&sReload=" & "Yes"

                        Else
                            lblnPost = mobjPolicyTra.InsPostVA650("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nTypemove"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctKey"), Session("nUsercode"))

                        End If
                    End If
                End With

            '+ VA669: Solicitud de Ilustracion de Poliza
            Case "VA669"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nIllustType=" & .Form.Item("cbeIllustType")
                    Else
                        lblnPost = True
                        mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("hddBranch") & "&nProduct=" & .Form.Item("hddProduct") & "&nPolicy=" & .Form.Item("hddPolicy") & "&nCertif=" & .Form.Item("hddCertif") & "&dEffecdate=" & .Form.Item("hddEffecdate") & "&nIllustType=" & .Form.Item("hddIllustType")
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            mobjPolicyTra = New ePolicy.Per_deposit
                            lblnPost = mobjPolicyTra.InsPostVA595Upd(.QueryString("Action"), "2", mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnEndYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYearPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble))
                        Else
                            If .Form.Item("chkIllustPrint") = "1" Then
                                If CStr(Session("sKey")) = "" Then
                                    Call insCreIllustration("2", Request.Form.Item("hddBranch"), Request.Form.Item("hddProduct"), Request.Form.Item("hddPolicy"), Request.Form.Item("hddCertif"), Request.Form.Item("hddEffecdate"), Request.Form.Item("hddIllusttype"), Request.Form.Item("tcnProjRent"), Request.Form.Item("tcnAddprem"), Request.Form.Item("tcnSurrMonth"), Request.Form.Item("tcnSurrYear"), Request.Form.Item("tcnSurrAmount"))
                                End If
                                Call insPrintPolicyRep("VAL669")
                            End If
                        End If
                    End If
                End With

            Case "VI7700"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    lblnPost = True
                    Session("sCertype") = "2"
                    Session("nBranch") = Request.Form.Item("cbeBranch")
                    Session("nProduct") = Request.Form.Item("valProduct")
                    Session("nPolicy") = Request.Form.Item("tcnPolicy")
                    Session("nCertif") = Request.Form.Item("tcnCertif")
                Else
                    lblnPost = True
                End If

            '+ CA028_1: Desglose de prima del recibo
            Case "CA028_1"
                mobjPolicyTra = New ePolicy.TDetail_pre
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mstrQueryString = "&nCodeItem=" & mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble)
                        lblnPost = mobjPolicyTra.insPostCA028Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), 2, 2, "1")
                    End If
                End With
            '+ CA028_1: Autorización de propuestas sin pago de primera prima
            Case "CA789"
                mobjPolicyTra = New ePolicy.Certificat
                Call mobjPolicyTra.Find("1", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), 0)
                mobjPolicyTra.nWait_code = 4
                lblnPost = mobjPolicyTra.Add()
        End Select
        insPostPolicyTra = lblnPost
    End Function

    '% insPrintCollectionRep: Se encarga de generar el reporte correspondiente.
    '--------------------------------------------------------------------------------------------
    Private Sub insPrintPolicyRep(ByRef Codispl As Object)
        Dim ProcessType As Byte
        '--------------------------------------------------------------------------------------------
        Dim mobjDocuments As eReports.Report
        Dim lobjPolicy_His As ePolicy.Policy_his

        mobjDocuments = New eReports.Report

        Select Case Codispl
        '+ VIL733: Aniversario de coberturas (Productos de Vida)
            Case "VIL733"
                With mobjDocuments
                    .ReportFilename = "VIL733.rpt"
                    .sCodispl = "VIL733"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdEffecdate")))
                    .setStorProcParam(2, Request.Form.Item("sOptExecute"))
                End With
                Response.Write((mobjDocuments.Command))

            '+ CA031, CAL005: Reportes de renovación puntual y masiva
            Case "CA031", "CAL005"
                With mobjDocuments
                    .sCodispl = "CAL005"
                    .ReportFilename = "CAL005.rpt"
                    .setParamField(1, "dStartDate", Request.Form.Item("tcdRendateFrom"))
                    .setParamField(2, "dEndDate", Request.Form.Item("tcdRenDateto"))
                    If Codispl = "CA031" Then
                        .setParamField(3, "nProctype", 2) '+Puntual
                    Else
                        .setParamField(3, "nProctype", 1) '+Masivo
                    End If
                    .setStorProcParam(1, sKey)
                    .setStorProcParam(2, lintTypeRepCAL005)
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                End With
                Response.Write((mobjDocuments.Command))

            '+ CAL006: Reservas de Primas (Vida)
            Case "CAL006"
                With mobjDocuments
                    .ReportFilename = "CAL006.rpt"
                    .sCodispl = "CAL006"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                End With
                Response.Write((mobjDocuments.Command))

            '+ VIL008: Saldado / Prorrogado de Poliza
            Case "VIL008"
                If Request.QueryString.Item("sCodispl") = "VI008" Then
                    Session("sCertype") = "2"
                End If
                With mobjDocuments
                    .ReportFilename = "VIL008.rpt"
                    .sCodispl = "VIL008"
                    .setStorProcParam(1, Session("sCertype"))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, .setdate(Request.Form.Item("tcdEffecdate")))
                    .setStorProcParam(7, sKeyVI008)
                End With
                Response.Write((mobjDocuments.Command))


            '+ VI011: Préstamo de póliza/certificado
            Case "VI011"
                With mobjDocuments
                    .ReportFilename = "VIL011.rpt"
                    .sCodispl = "VIL011"
                    .setStorProcParam(1, "TMP" & Session("SessionID") & Session("nUsercode"))
                End With
                Response.Write((mobjDocuments.Command))

            '+ CAL033: Impresión de anulación preliminar o definitiva
            '+		   Se ejecuta solo cuando la opción del recibo es: "Sin recibo => 1"
            '+		   o con "Recibo manual => 3", cuando es con "Recibo automático =>2" 
            '+		   el reporte es llamado desde el post de la CA027
            Case "CAL033"
                If Request.QueryString.Item("sCodisplOrig") = "CA033_CA028" Then
                    With mobjDocuments
                        .ReportFilename = "CAL033.rpt"
                        .sCodispl = "CAL033"
                        '.setStorProcParam 1,  Request.QueryString("sCertype")
                        .setStorProcParam(1, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(4, mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(5, .setdate(Request.QueryString.Item("dNullDate")))
                        .setStorProcParam(6, Request.QueryString.Item("sNullReceipt"))
                        .setStorProcParam(7, Request.QueryString.Item("nExeMode"))
                        .setStorProcParam(8, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(9, mobjValues.StringToType(Request.QueryString.Item("soptReceipt"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(10, Session("sKey"))
                        .setStorProcParam(11, llngProposal)

                        Response.Write((.Command))
                    End With
                Else
                    With mobjDocuments
                        .ReportFilename = "CAL033.rpt"
                        .sCodispl = "CAL033"
                        '.setStorProcParam 1,  Session("sCertype")
                        .setStorProcParam(1, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(4, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(5, .setdate(Request.Form.Item("tcdNullDate")))
                        .setStorProcParam(6, Request.Form.Item("chkNullReceipt"))
                        .setStorProcParam(7, Session("optExecute"))
                        .setStorProcParam(8, mobjValues.StringToType(Session("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(9, mobjValues.StringToType(Request.Form.Item("optReceipt"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(10, "")
                        .setStorProcParam(11, llngProposal)

                        Response.Write((.Command))
                    End With
                End If

            '+ CAL034: Rehabilitación de Póliza/Certificado
            Case "CAL034"
                With mobjDocuments
                    If sBranchtCA034 = "1" Then
                        .ReportFilename = "CAL034_V.rpt"
                    Else
                        .ReportFilename = "CAL034.rpt"
                    End If

                    .sCodispl = "CAL034"
                    .setStorProcParam(1, Session("sKey"))
                    .setStorProcParam(2, mobjValues.StringToType(Session("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                    If Session("nProposal") <> 0 And Session("nProposal") <> eRemoteDB.Constants.intNull Then
                        .setStorProcParam(3, mobjValues.StringToType(Session("nProposal"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        .setStorProcParam(3, mobjValues.StringToType(Session("nProponum"), eFunctions.Values.eTypeData.etdDouble))
                    End If

                    If sBranchtCA034 = "1" Then
                        .setStorProcParam(4, Session("nUsercode"))
                    End If
                End With
                Response.Write((mobjDocuments.Command))

            '+ val669: Solicitud de ilustracion
            Case "VAL669"
                With mobjDocuments
                    .ReportFilename = "VAL669.rpt"
                    .sCodispl = "VA669"
                    .setStorProcParam(1, Session("sKey"))
                    '                .bTimeOut = True
                    '                .nTimeOut = 20000
                End With
                Response.Write((mobjDocuments.Command))
                Session("sKey") = ""

                lobjPolicy_His = New ePolicy.Policy_his
                With lobjPolicy_His
                    .sCertype = "2"
                    .nBranch = mobjValues.StringToType(Request.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .nProduct = mobjValues.StringToType(Request.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble)
                    .nPolicy = mobjValues.StringToType(Request.Form.Item("hddPolicy"), eFunctions.Values.eTypeData.etdDouble)
                    .nCertif = mobjValues.StringToType(Request.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble)
                    .nType = 71
                    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .nMovement = 0
                    .insCrePolicy_his()
                End With
                lobjPolicy_His = Nothing

            '+ VIL009: Impresión de rescate de póliza/certificado
            Case "VIL009"
                With mobjDocuments
                    .ReportFilename = "VIL009.rpt"
                    .sCodispl = "VIL009"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Request.Form.Item("hdddEffecdate")))
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("chkNullPrem"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(8, mobjValues.StringToType(Request.Form.Item("hddsSurrPayWay"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(9, mobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(10, mobjValues.StringToType(Request.Form.Item("hddsSurrType"), eFunctions.Values.eTypeData.etdDouble))
                    If mobjValues.StringToType(Request.Form.Item("hddsProcessType"), eFunctions.Values.eTypeData.etdDouble) = "" Then
                        ProcessType = 0
                    Else
                        ProcessType = mobjValues.StringToType(Request.Form.Item("hddsProcessType"), eFunctions.Values.eTypeData.etdDouble)
                    End If

                    .setStorProcParam(11, ProcessType)
                    .setStorProcParam(12, nProposal)

                    Response.Write((.Command))
                End With

        End Select
        mobjDocuments = Nothing
    End Sub

    '% insCreIllustration: Genera datos para reporte de ilustracion
    '---------------------------------------------------------------------------
    Private Sub insCreIllustration(ByRef sCertype As String, ByVal nBranch As String, ByVal nProduct As String, ByVal nPolicy As String, ByVal nCertif As String, ByVal dEffecdate As String, ByVal nIllustType As String, ByVal nProjRent As String, ByVal nAddPremium As String, ByVal nSurrMonth As String, ByVal nSurrYear As String, ByVal nSurrAmount As String)
        '---------------------------------------------------------------------------
        Dim lcolTmp_val669 As ePolicy.Tmp_val669s

        lcolTmp_val669 = New ePolicy.Tmp_val669s

        If lcolTmp_val669.InsCalValuePolIlustration(sCertype, mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nIllustType, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nProjRent, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nAddPremium, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nSurrMonth, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nSurrYear, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nSurrAmount, eFunctions.Values.eTypeData.etdDouble, True)) Then

            Session("sKey") = lcolTmp_val669.sKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionId"), eFunctions.Values.eTypeData.etdDouble))
        End If
        lcolTmp_val669 = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valpolicytra")
mobjValues = New eFunctions.Values

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valpolicytra"

mstrCommand = "sModule=Policy&sProject=PolicyTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




</HEAD>
<BODY>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 86 $|$$Date: 12/11/04 11:45 $|$$Author: Nvaplat7 $"

<%If Request.QueryString.Item("sCodispl") = "VA669" Then%>
//%InsShowIlustration: Muestra la ventana con la Ilustración de la poliza
//------------------------------------------------------------------------------------------------
function InsShowIlustration(){
//------------------------------------------------------------------------------------------------
    var lstrQueryString = new String;
    var lstrError;
    <%	nProjRent = mobjValues.StringToType(Request.Form.Item("tcnProjRent"), eFunctions.Values.eTypeData.etdDouble) / 100
	nProjRent = mobjValues.TypeToString(nProjRent, eFunctions.Values.eTypeData.etdDouble)
	%>
    lstrQueryString = '&sCertype=<%=Request.Form.Item("hddCertype")%>' + 
                      '&nBranch=<%=Request.Form.Item("hddBranch")%>' +
                      '&nProduct=<%=Request.Form.Item("hddProduct")%>' + 
                      '&nPolicy=<%=Request.Form.Item("hddPolicy")%>' +
                      '&nCertif=<%=Request.Form.Item("hddCertif")%>' + 
                      '&dEffecdate=<%=Request.Form.Item("hddEffecdate")%>' +
                      '&nIllusttype=<%=Request.Form.Item("hddIllusttype")%>' + 
                      '&nProjRent=<%=nProjRent%>';
    try {
        lstrQueryString += '&nAddpremium=<%=Request.Form.Item("tcnAddPrem")%>';
    } catch(lstrError){}
        
    try {            
        lstrQueryString += '&nSurrYear=<%=Request.Form.Item("tcnSurrYear")%>' + 
                           '&nSurrMonth=<%=Request.Form.Item("tcnSurrMonth")%>' + 
                           '&nSurrAmount=<%=Request.Form.Item("tcnSurrAmount")%>';
    }catch(lstrError){}
        
    ShowPopUp("../../Common/ShowIlustration.aspx?sCodispl=VA669" + lstrQueryString,"ValuePolIllustration",750,500,'yes','yes',10,10);
}
<%End If%>
//%CancelErrors: Se ejecuta cuando se cancela la ventana de errores
//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)
}
</SCRIPT>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
<%
lclsFunds_Pol = New ePolicy.Funds_Pol
MobjPolicy = New ePolicy.Policy
mobjPolicyTra = New ePolicy.ValPolicyTra

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValPolicyTra
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If
If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""PolicyTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		
		If Request.QueryString.Item("sCodispl") = "CA028" And Request.QueryString.Item("sPopUp") = "1" Then
			.Write("CancelErrors();")
		Else
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		End If
		.Write("</SCRIPT>")
	End With
Else
	If insPostPolicyTra Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Select Case Request.QueryString.Item("sCodispl")
					Case "VI009"
						Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
						'+ VI011: Préstamos/Anticipos
					Case "VI011"
						'+ Si la ejecución es preliminar
						If CDbl(Request.Form.Item("optExecute")) = 1 Then
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
							End If
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();top.opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
							End If
						End If
					Case "CA031_K"
						'+ Si la ejecución es preliminar
						If Session("nRenewal") = 1 Then
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
							End If
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();top.opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
							End If
						End If
					Case "VI7000"
						If Request.QueryString.Item("nZone") = "1" Then
							Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
						Else
							If CDbl(Request.Form.Item("cbePmtOrd")) = 2 Or CStr(Session("optProcessType")) = "1" Then
								If Request.Form.Item("sCodisplReload") = vbNullString Then
									Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
								Else
									Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
								End If
							Else
								If CStr(Session("optProcessType")) <> "1" Then
									Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
								End If
							End If
						End If
						
					Case "CA767"
						Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
					Case "VI008"
						If CDbl(Request.Form.Item("optExeMode")) = 1 Then
							Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=VI008';</SCRIPT>")
						End If
					Case "CA099A"
						'+Si se realizao algun proceso se muestran resultados
						If mstrMessage <> vbNullString Then
							Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
							Response.Write("ShowPopUp('/VTimeNet/Common/ShowResults.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sKey=" & mstrMessage & "', 'PolicyTraRes',660,330);")
							Response.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
							Response.Write("</SCRIPT>")
							'+Sino retorna a la página inicial
						Else
							Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099';</SCRIPT>")
						End If
					Case "CA034"
						If Request.QueryString.Item("sCodisplOri") = "CA767" Then
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099';</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099';</SCRIPT>")
							End If
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
							End If
						End If
						
					Case "VA669"
						If Request.Form.Item("chkShowIllustration") = "1" Then
							Response.Write(("<SCRIPT>setTimeout('InsShowIlustration();top.document.location.reload();',10000);</SCRIPT>"))
						Else
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						End If
					Case "CA099"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicyTra&sCodispl=CA099&sConfig=InSequence&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicyTra&sCodispl=CA099&sConfig=InSequence&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
						End If
					Case "CA033"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
						End If
					Case "CA028"
						Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
					Case Else
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				End Select
				
			ElseIf Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataCancel) Then 
				Select Case Request.QueryString.Item("sCodispl")
					Case "CA034"
						If Request.QueryString.Item("sCodisplOri") = "CA767" Then
							Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099';</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						End If
				End Select
				
			Else
				Select Case Request.QueryString.Item("sCodispl")
					Case "CA037", "CA038"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>insReloadTop(false)</SCRIPT>")
						Else
							Response.Write("<SCRIPT>opener.top.document.location.reload();window.close();</SCRIPT>")
						End If
						
					Case "VI008"
						If CDbl(Request.Form.Item("optExeMode")) = 1 Then
							Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=VI008';</SCRIPT>")
						End If
						
					Case "CA028"
						'+ Si la ventana donde se encuentra la grilla se muestra como PopUp
						If Request.QueryString.Item("sPopUp") = "1" Then
							Response.Write("<SCRIPT>window.close();</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=CA028&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=CA028&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
							End If
						End If
					Case "CA028A"
						'+ Si la ventana de recibo manual se encuentra en la secuencia, se recarga la misma
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
						End If
					Case "CA099"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicyTra&sCodispl=CA099&sConfig=InSequence&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicyTra&sCodispl=CA099&sConfig=InSequence&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
						End If
					Case Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = "CA034" Then
								If Request.QueryString.Item("nZone") = "1" Then
									mstrQueryString = "&sCertype=" & Request.Form.Item("tctCertype") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPolicy=" & Request.Form.Item("tcnPolicy") & "&nCertif=" & Request.Form.Item("tcnCertif") & "&nAgency=" & Request.Form.Item("cbeAgency") & "&nExeMode=" & Request.Form.Item("optExecute") & "&sCodisplOri=" & Request.Form.Item("hddCodisplOri") & "&nServ_Order=" & Request.Form.Item("tcnServ_Order")
								End If
							End If
							
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
						End If
				End Select
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "CA028", "CA028A"
					Response.Write("<SCRIPT>top.opener.document.location.href='CA028.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
				Case "VI010"
					Response.Write("<SCRIPT>top.opener.document.location.href='VI010.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	Else
		Response.Write("<SCRIPT>alert('Proceso del Post arrojó falso');</SCRIPT>")
	End If
End If

lclsFunds_Pol = Nothing
mobjValues = Nothing
mobjPolicyTra = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
Call mobjNetFrameWork.FinishPage("valpolicytra")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





