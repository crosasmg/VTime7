<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eApvc" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eCrystalExport" %>

<SCRIPT language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
    Dim mobjNetFrameWork As Object
    '~End Header Block VisualTimer Utility
    Dim nReceiptCollected As Object
    Dim dNextReceiptByChangdat As Object


    '- Se define la contante para el manejo de errores en caso de advertencias 

    Dim mstrCommand As String

    '- Variable auxiliar para pase de valores del encabezado al folder

    Dim mstrQueryString As String
    Dim mstrCodispl As String
    Dim mstrMessage As String

    '- Variable usada en para el reporte de rescate(VIL009)
    Dim nProposal As String

    Dim sKey As Object
    Dim sKeyVI008 As String
    Dim sBranchtCA034 As String

    Dim ncreditmanualAux As Double
    Dim ndebitmanualAux As Double

    '- Variable usada en para el rescate
    Dim sActivefound As Object

    '- Variable auxiliar para almacenar el tipo de póliza
    Dim mstrpoli_type As Object
    Dim llngProposal As String

    '-Tipo de reporte proceso CAL005    
    Dim lintTypeRepCAL005 As Byte

    Dim mobjValues As eFunctions.Values

    '- Se define las variables para la creción del detalle del recibo manual (CA028)
    Dim mclsTDetail_pre As Object

    Dim nProjRent As Double

    Dim mstrErrors As Object
    Dim mobjPolicyTra As Object
    Dim mobjPolicy_Tra As Object
    Dim MobjPolicy As Object
    Dim lclsFunds_Pol As Object
    Dim mobjBatch As Object


    '% insValPolicyTra: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Private Function insValPolicyTra() As String
        'dim dblNull As Object
        'dim eRemoteDB.Constants.intNull As Object
        'dim dtmNull As Object
        ''Dim eRemoteDB.Constants.intNull As Object
        '--------------------------------------------------------------------------------------------
        Dim lintCountCA051 As Object

        Select Case Request.QueryString.Item("sCodispl")

            '+ CA032: Reverso de modificación/renovación de una póliza                
            Case "CA032"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA032_k("CA032", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA032("2", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdTransDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("optTransac"), eFunctions.Values.eTypeData.etdLong))
                    End If
                End With

            '+ CA033: Anulación de una póliza
            Case "CA033"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA033_k("CA033", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        mobjPolicyTra = New ePolicy.ValPolicyTra

                        insValPolicyTra = mobjPolicyTra.insValCA033("CA033", "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optDev"), .Form.Item("optReceipt"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+CA034: Rehabilitación/Reactivación de una póliza
            Case "CA034"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&sCertype=" & .Form.Item("tctCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nAgency=" & .Form.Item("cbeAgency") & "&nExeMode=" & .Form.Item("optExecute") & "&nProcess=" & .Form.Item("optProcess") & "&sCodisplOri=" & .Form.Item("hddCodisplOri") & "&nServ_Order=" & .Form.Item("tcnServ_Order")

                        insValPolicyTra = mobjPolicyTra.insValCA034_K(Session("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Session("nPolicy"), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nExeMode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .Form.Item("chkRescRequest"), mobjValues.StringToType(.Form.Item("ValNullLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodisplOri"))
                    End If
                End With

            '+CA034A: Rehabilitación de una póliza
            Case "CA034A"
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA034A_K(Session("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.Form.Item("tcdNullDate")))
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
                Dim AuxSel As Integer = Integer.MinValue
                Dim Selected As Integer = Integer.MinValue

                With Request
                    mobjPolicyTra = New eBatch.ValBatch
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA051_K(mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctFile"), .Form.Item("tctDescript"), .Form.Item("chkList"), mobjValues.StringToType(.Form.Item("tcnWorksheet"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("nMainAction") <> "401" Then
                            If Request.QueryString.Item("WindowType") <> "PopUp" Then

                                If Not IsNothing(.Form.Item("chkAuxSel")) Then
                                    AuxSel = .Form.Item("chkAuxSel").Split(",").Length
                                End If

                                If Not IsNothing(.Form.Item("chkSelected")) Then
                                    Selected = .Form.Item("chkSelected").Split(",").Length
                                End If

                                insValPolicyTra = mobjPolicyTra.insValCA051(AuxSel, Selected, mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble))

                            Else
                                insValPolicyTra = mobjPolicyTra.insValCA051Upd(mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdRec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctColumnName"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRequire"), .Form.Item("chkSelected"), Session("nUserCode"), mobjValues.StringToType(.Form.Item("tcnSheet"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsField"), .Form.Item("hddsCritery"))
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
                Dim SelLength As Integer = 0
                mobjPolicyTra = New ePolicy.TDetail_pre

                With Request
                    mstrQueryString = "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&dEffecdate=" & Request.Form.Item("hddStartDateR") & "&dExpirDate=" & Request.Form.Item("hddExpirDate") & "&nReceipt=" & Request.Form.Item("hddReceipt") & "&dIssuedat=" & Request.Form.Item("hddIssueDate") & "&nCurrency=" & Request.Form.Item("hddCurrency") & "&nTratypei=" & Request.Form.Item("hddSource") & "&sOrigReceipt=" & Request.Form.Item("hddOrigReceipt") & "&sCodisplOrig=" & .QueryString.Item("sCodisplOrig") & "&sCertype=" & .QueryString.Item("sCertype") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dNullDate=" & .QueryString.Item("dNullDate") & "&sNullReceipt=" & .QueryString.Item("sNullReceipt") & "&sTypeReceipt=" & .QueryString.Item("sTypeReceipt") & "&nExeMode=" & .QueryString.Item("nExeMode") & "&sExeReport=" & .QueryString.Item("sExeReport") & "&nAgency=" & .QueryString.Item("nAgency") & "&sOnSeq=" & .QueryString.Item("sOnSeq") & "&sNewData=" & .QueryString.Item("sNewData") & "&sKey=" & .QueryString.Item("sKey")

                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA028_K("CA028", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValCA028(.QueryString("WindowType"), .QueryString("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), , , , , mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), , , mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeType"), .Form.Item("hddCacalili"), .Form.Item("hddCommissi_i"), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("hddPrem_det_proc"), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            If Not String.IsNullOrEmpty(.Form.Item("Sel")) Then
                                SelLength = .Form.Item("Sel").Split(",").Length
                            End If

                            insValPolicyTra = mobjPolicyTra.insValCA028(.QueryString("WindowType"), .QueryString("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                       SelLength,
                                                                        mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbenreceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLedReceipt"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .Form.Item("optType"), "", "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("cbenreceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True))
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
                    insValPolicyTra = mobjPolicyTra.insValCA037_k("CA037", "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdExpirdateNew"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdExpirdateNew"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), Session("sSche_code"))
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
                'UPGRADE_NOTE: The 'ePolicy.Loans' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Loans
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValVI011_k("VI011", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("valCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.insValVI011("VI011", .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInter_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExecute"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+VI012: Registro de abono de anticipo
            Case "VI012"
                'UPGRADE_NOTE: The 'ePolicy.Improve_lo' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                            insValPolicyTra = mobjPolicyTra.insValVI012("VI012", "Normal", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(Session("nAport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dPay_Date"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dblNull, eRemoteDB.Constants.dblNull)
                        End If
                    End If

                End With

            '+VI7000: Rescate de pólizas
            Case "VI7000"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        Session("dEffecDate") = mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        insValPolicyTra = mobjPolicyTra.InsValVI7000_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optSurrType"), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), Session("sSche_code"), Request.QueryString.Item("sCodisplOri"), .Form.Item("optProcessType"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkInsur"))
                    Else
                        '+ Modificación
                        Session("dEffecDate") = mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.InsValVI7000("2", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodispl"), mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), .Form.Item("hddsApv"), Session("optProcessType"), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSurrTot"), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), Session("sSche_code"), .Form.Item("hddIsCancelling"))
                        Else

                            'UPGRADE_NOTE: The 'ePolicy.Surr_Origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Surr_origins
                            insValPolicyTra = mobjPolicyTra.InsValVI7000_Upd(.QueryString("sCodispl"), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAvailBal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmt"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), mobjValues.StringToType(.Form.Item("tcnRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWDCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntLoans"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            '+VI7004: Rescate de pólizas APV
            Case "VI7004"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        Session("dEffecDate") = mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        insValPolicyTra = mobjPolicyTra.InsValVI7000_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optSurrType"), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True), Session("sSche_code"), Request.QueryString.Item("sCodisplOri"), .Form.Item("optProcessType"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkInsur"))
                    Else
                        '+ Modificación
                        Session("dEffecDate") = mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.InsValVI7000("2", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodispl"), mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), .Form.Item("hddsApv"), Session("optProcessType"), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSurrTot"), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), Session("sSche_code"), .Form.Item("hddIsCancelling"), mobjValues.StringToType(.Form.Item("tcnSaapv"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            'UPGRADE_NOTE: The 'ePolicy.Surr_Origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Surr_origins

                            insValPolicyTra = mobjPolicyTra.InsValVI7000_Upd(.QueryString("sCodispl"), "2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnVP"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), mobjValues.StringToType(.Form.Item("tcnRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWDCost"), eFunctions.Values.eTypeData.etdDouble), 0, 0)
                        End If
                    End If
                End With
            '**+ VI010: Switches
            '+ VI010: Cambios de fondos de inversión
            Case "VI010"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.Funds_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.Funds_Pol
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValVI010_k(.QueryString("sCodispl"), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optProcessType"), .Form.Item("hddsCodisplOri"))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValVI010(.QueryString("sCodispl"), .QueryString("WindowType"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkActivFound"), mobjValues.StringToType(.Form.Item("tcnAvailable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange_aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValPolicyTra = mobjPolicyTra.insValVI010A("8", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodispl"))
                        End If
                    End If
                End With
            '**+ VI016: Switches - APV
            '+ VI016: Cambios de fondos de inversión - APV
            Case "VI016"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.Funds_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.Funds_Pol
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValVI016_k(.QueryString("sCodispl"), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optProcessType"), .Form.Item("hddsCodisplOri"))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValVI016(.QueryString("sCodispl"), .QueryString("WindowType"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkActivFound"), mobjValues.StringToType(.Form.Item("tcnAvailable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_Profitworker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange_aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValPolicyTra = mobjPolicyTra.insValVI016A("8", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sCodispl"))
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
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValVI7002(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
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
                    insValPolicyTra = MobjPolicy.insValCA031(Request.QueryString.Item("sCodispl"),
                                                             mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                             mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, False),
                                                             Session("sTypeCompanyUser"),
                                                             Session("nInfo"),
                                                             mobjValues.StringToDate(Request.Form.Item("tcdRendateFrom")), mobjValues.StringToDate(Request.Form.Item("tcdRenDateto")),
                                                             Request.Form.Item("optType"))
                End If
            '+CA099: Tratamiento de cotizaciones y solicitudes
            Case "CA099"
                'UPGRADE_NOTE: The 'ePolicy.TConvertions' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TConvertions
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA099_K(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valProduct_sBrancht"), mobjValues.StringToType(.Form.Item("cbeOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optTypeDoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), Session("sSche_code"), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CA099A: Tratamiento de cotizaciones y solicitudes
            Case "CA099A"
                'UPGRADE_NOTE: The 'ePolicy.TConvertions' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TConvertions
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValPolicyTra = mobjPolicyTra.insValCA099(.QueryString("nOperat"), mobjValues.StringToType(.Form.Item("valNoConvers"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStat"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStatdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdMaximum_da"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+CA767 Tratamiento de propuestas especiales
            Case "CA767"
                insValPolicyTra = vbNullString

            '+VI008: Reducción de capital o vigencia
            Case "VI008"
                'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Certificat
                With Request
                    insValPolicyTra = mobjPolicyTra.insValVI008("VI008", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optReduction"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddCodisplOri"))
                End With

            '+VI806: Capitalización de fondos
            Case "VI806"
                'UPGRADE_NOTE: The 'ePolicy.TMovprev_Capital' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TMovprev_Capital
                With Request
                    If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValVI806("VI806", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+CA642: Cambio de frecuencia de pago
            Case "CA642"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                    'UPGRADE_NOTE: The 'ePolicy.Account_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                        'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.Activelife
                        insValPolicyTra = mobjPolicyTra.insValVA669_K("VA669", mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        insValPolicyTra = ""
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Activelife
                            insValPolicyTra = mobjPolicyTra.insValVA669(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddIllustType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProjRent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAddPrem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnSurrAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTargetVP"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            '+ La unica ventana popup que actualiza es la de Plan de pagos                    
                            'UPGRADE_NOTE: The 'ePolicy.Per_deposit' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Per_deposit
                            insValPolicyTra = mobjPolicyTra.InsValVA669Upd(.QueryString("sCodispl"), .QueryString("sAction"), "2", mobjValues.StringToType(.Form.Item("hddBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYearPrem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddPolYears"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    End If

                End With

            Case "VI7700"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    insValPolicyTra = mobjPolicyTra.insValVI770_K("VI7700", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    insValPolicyTra = vbNullString
                End If

            '+ CA028_1: Desglose de prima del recibo
            Case "CA028_1"
                'UPGRADE_NOTE: The 'ePolicy.Tdetail_pre' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TDetail_pre
                With Request
                    insValPolicyTra = mobjPolicyTra.insValCA028_1(.QueryString("sCodispl"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString("dIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddType_detai"), mobjValues.StringToType(.Form.Item("hddDisexprc"), eFunctions.Values.eTypeData.etdLong, True))
                End With

            '+CA0789: Autorización de propuestas sin pago de primera prima
            Case "CA789"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                insValPolicyTra = mobjPolicyTra.insValCA789_k("1", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), 0)
            '+CA900: Traspaso de primera prima
            Case "CA900"
                'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Policy

                insValPolicyTra = mobjPolicyTra.insValCA900(mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdLong, False), Request.Form.Item("tctClient"), Request.Form.Item("tctClient_des"), mobjValues.StringToType(Request.Form.Item("tcnProcess"), eFunctions.Values.eTypeData.etdLong, False), mobjValues.StringToType(Request.Form.Item("tcnCredit"), eFunctions.Values.eTypeData.etdDouble, False))
            '+ CAL963: Ajuste por endoso retroactivo
            Case "CAL963"
                insValPolicyTra = True
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insValPolicyTra = mobjPolicyTra.insValCal963_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    mstrQueryString = "&cbeBranch=" & Request.Form.Item("cbeBranch") & "&valProduct=" & Request.Form.Item("valProduct") & "&tcnPolicy=" & Request.Form.Item("tcnPolicy")
                End If

            '+ MVI8017: Registro de renovaciones de un ahorro garantizado
            Case "MVI8017"
                'UPGRADE_NOTE: The 'ePolicy.Renewal_guaran_val' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Renewal_guaran_val
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValMVI8017_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdLong), .Form.Item("hddsCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        mstrQueryString = "&sCertype=" & .Form.Item("hddsCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.InsValMVI8017(.QueryString("sCodispl"), .QueryString("Action"), .QueryString("sCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGuarsav_year"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdIniperiod"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndperiod"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCurrentamount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNewamount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNewprem"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkFunds"), .Form.Item("chkReceipt"))
                        End If
                    End If
                End With

            '+ CAL815: Rehabilitación/Reactivación masiva de pólizas
            Case "CAL815"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request
                    insValPolicyTra = mobjPolicyTra.InsValCAL815(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate))

                End With

            '+ CAL978: Reactivación de pólizas
            Case "CAL978"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request

                    insValPolicyTra = mobjPolicyTra.InsValCAL978(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))

                End With

            Case "CA088"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValCA088_K("CA088", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValPolicyTra = mobjPolicyTra.InsValCA088("CA088", mobjValues.StringToType(.Form.Item("tcdRecepInt"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInt_Comp"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInsu"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInsu_Comp"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dDate_Origi"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

            '+ MVI8015: DESCUENTOS PORCENTUALES POR VALOR PÓLIZA
            Case "MVI8015"
                'UPGRADE_NOTE: The 'eProduct.Perc_DiscVP' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New eProduct.Perc_DiscVP
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.InsValMVI8015_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.InsValMVI8015(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnvp_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnvp_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcndisc_perc_vp"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With

            '+VI818: Ajuste de movimientos en cuenta preliminar
            Case "VI818"
                'UPGRADE_NOTE: The 'eBatch.tmp_undo_move_acc' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New eBatch.Tmp_undo_Move_Acc
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValVI818_k(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optExecute"))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValVI818upd(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddidconsec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctCredit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctDebit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdoperdatemanual"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctType_move"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tctProfitworker"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValPolicyTra = mobjPolicyTra.insValVI818(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        End If
                    End If
                End With

            '+VI820: Ajuste de movimientos en cuenta Definitivo
            Case "VI820"
                insValPolicyTra = ""

            '+CA980: Folios asignados a la compañía
            Case "CA980"
                With Request
                    mobjPolicyTra = New ePolicy.Folios_comp
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValPolicyTra = mobjPolicyTra.insValCA980(.QueryString("sCodispl"),
                                                                    mobjValues.StringToType(.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnStart"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnEnd"), eFunctions.Values.eTypeData.etdDouble),
                                                                    .Form("cbeStatregt"),
                                                                    .QueryString("Action"))
                    End If
                End With

            '+CA985: Asignación de folios por intermediario
            Case "CA985"
                With Request
                    mobjPolicyTra = New ePolicy.Folios_Agent
                    If Request.QueryString.Item("WindowType") <> "PopUp" Then
                        mobjPolicyTra.nBranch = mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdLong)
                        mobjPolicyTra.nProduct = mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdLong)
                        mobjPolicyTra.nintermed = mobjValues.StringToType(.Form("valIntermed"), eFunctions.Values.eTypeData.etdLong)
                        mobjPolicyTra.dassign_date = mobjValues.StringToType(.Form("tcdAssign_date"), eFunctions.Values.eTypeData.etdDate)
                    Else
                        mobjPolicyTra.nBranch = Session("nBranch_CA985")
                        mobjPolicyTra.nProduct = Session("nProduct_CA985")
                        mobjPolicyTra.nintermed = Session("nIntermed_CA985")
                        mobjPolicyTra.dassign_date = Session("dAssign_date_CA985")
                    End If
                    insValPolicyTra = mobjPolicyTra.insValCA985(.QueryString("sCodispl"),
                                                                mobjValues.StringToType(.QueryString("nZone"), eFunctions.Values.eTypeData.etdDouble),
                                                                .QueryString("WindowType"),
                                                                .QueryString("Action"),
                                                                mobjPolicyTra.nBranch,
                                                                mobjPolicyTra.nProduct,
                                                                mobjPolicyTra.nintermed,
                                                                mobjPolicyTra.dassign_date,
                                                                mobjValues.StringToType(.Form("tcnStart"), eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(.Form("tcnEnd"), eFunctions.Values.eTypeData.etdDouble),
                                                                .Form("cbePolitype"),
                                                                mobjValues.StringToType(.Form("tcnStartPolNumber"), eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(.Form("tcnEndPolNumber"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "CA986"
                mobjPolicyTra = New ePolicy.Soap_Sell_Period
                With Request

                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA986_K(.QueryString("sCodispl"),
                                                                mobjValues.StringToType(.QueryString("nZone"), eFunctions.Values.eTypeData.etdDouble),
                                                                .QueryString("WindowType"),
                                                                .QueryString("Action"),
                                                                mobjValues.StringToType(.Form("cbeTypeVeh"), eFunctions.Values.eTypeData.etdLong))
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValCA986(.QueryString("sCodispl"),
                                                                        mobjValues.StringToType(.QueryString("nZone"), eFunctions.Values.eTypeData.etdDouble),
                                                                        .QueryString("WindowType"),
                                                                        .QueryString("Action"),
                                                                        mobjValues.StringToType(Session("nVehType_CA986"), eFunctions.Values.eTypeData.etdDouble),
                                                                        mobjValues.StringToType(.Form.Item("tcdStartPeriod"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcdExpiredPeriod"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcdStartDatePol"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcdExpiredDatePol"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger))
                        End If
                    End If
                End With

            '+SO001: Digitación de Pólizas SOAP
            Case "SO001"
                Dim mobjPolicyTra = New ePolicy.Soap_entry


                With Request


                    insValPolicyTra = mobjPolicyTra.insValSO001(.QueryString("sCodispl"),
                                                                 .Form("tctRegist"),
                                                                 mobjValues.StringToType(.Form("tctType"), eFunctions.Values.eTypeData.etdInteger),
                                                                 mobjValues.StringToType(.Form("ValVehMark"), eFunctions.Values.eTypeData.etdDouble),
                                                                 .Form("ValVehModel"),
                                                                 IIf(.Form("ValVehMark") = eRemoteDB.strNull, "", IIf(.Form("ValVehMark") = "9999", .Form("tctMark"), "")),
                                                                 IIf(.Form("ValVehMark") = eRemoteDB.strNull, "", IIf(.Form("ValVehMark") = "9999", .Form("tctModel"), "")),
                                                                 mobjValues.StringToType(.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble),
                                                                 .Form("tctMotor"),
                                                                 .Form("tctChassis"),
                                                                 .Form("tctColor"),
                                                                 IIf(.Form("valCausal") = eRemoteDB.strNull, 0, .Form("valCausal")),
                                                                 .Form("dtcClient"),
                                                                 .Form("dtcClient_Digit"),
                                                                 .Form("tctNames"),
                                                                 .Form("tctFatherLastName"),
                                                                 .Form("tctMotherLastName"),
                                                                 mobjValues.StringToDate(.Form("dtcBirthdayDate")),
                                                                 mobjValues.StringToType(.Form("cbeProvince"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form("valLocal"), eFunctions.Values.eTypeData.etdDouble),
                                                                 mobjValues.StringToType(.Form("valMunicipality"), eFunctions.Values.eTypeData.etdDouble),
                                                                  .Form("tctPhone"),
                                                                 mobjValues.StringToType(.Form("tcnFolio"), eFunctions.Values.eTypeData.etdDouble),
                                                                .Form("hddStatusva"),
                                                                mobjValues.StringToDate(.Form("tcdStartDate")),
                                                                mobjValues.StringToDate(.Form("tcdExpirDate")),
                                                                mobjValues.StringToDate(.Form("dStartDateOri")),
                                                                mobjValues.StringToDate(.Form("dStartDatePol")),
                                                                mobjValues.StringToDate(.Form("dExpirDatePol")),
                                                                mobjValues.StringToType(.Form("cbeModule"), eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(.Form("tcnCollectedPremium"), eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(.Form("valAgreement"), eFunctions.Values.eTypeData.etdDouble),
                                                                .Form("tctDigit"), .Form("cbeLicense_ty"),
                                                                .Form("tctDigitalLink"))
                End With
            Case "SO002"
                With Request
                    mobjPolicyTra = New ePolicy.Folios_Agent
                    If .QueryString.Item("nZone") = "1" Then
                        insValPolicyTra = mobjPolicyTra.insValSO002_K(.QueryString("sCodispl"),
                                                                    mobjValues.StringToType(.QueryString("nZone"), eFunctions.Values.eTypeData.etdLong),
                                                                    .QueryString("WindowType"),
                                                                    .QueryString("Action"),
                                                                    mobjValues.StringToType(.Form("valIntermedSource"), eFunctions.Values.eTypeData.etdLong),
                                                                    mobjValues.StringToType(.Form("tcnFolioI"), eFunctions.Values.eTypeData.etdLong),
                                                                    mobjValues.StringToType(.Form("tcnFolioE"), eFunctions.Values.eTypeData.etdLong),
                                                                    mobjValues.StringToType(.Form("valIntermedDest"), eFunctions.Values.eTypeData.etdLong),
                                                                    Session("nUsercode"))
                    End If
                End With

            Case "VI7502"
                With Request
                    mobjPolicyTra = New eSaapv.Saapv_pol

                    If .QueryString("nZone") = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValvi7502(.Form("cbeCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, .Form("tctClient"), mobjValues.StringToType(.Form("tcnCod_saapv"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString("WindowType") <> "PopUp" Then
                            insValPolicyTra = ""
                        Else
                            insValPolicyTra = mobjPolicyTra.insValvi7502upd(mobjValues.StringToType(.Form("tcnCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbestatus_saapv"), eFunctions.Values.eTypeData.etdDouble), .Form("chkAutodif"), mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdLong))
                        End If
                    End If
                    'UPGRADE_NOTE: Object mobjPolicyTra may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjPolicyTra = Nothing
                End With
            '+ VI017: Traspaso por porcentajes
            Case "VI017", "VI017-2"
                With Request

                    mobjPolicyTra = New ePolicy.Funds_Pol
                    mobjBatch = New eBatch.tmp_switch

                    If .QueryString("nZone") = 1 Then
                        insValPolicyTra = mobjBatch.insValvi017_k(mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    Else
                        If .QueryString("sCodispl") = "VI017" Then
                            insValPolicyTra = mobjBatch.insValvi017(Session("sKey"))
                        Else
                            insValPolicyTra = mobjBatch.insValVI017_2(.QueryString("sCodispl"),
                                                                      mobjValues.StringToType(.Form("hddValTotal3"), eFunctions.Values.eTypeData.etdDouble),
                                                                                     mobjValues.StringToType(.Form("hddValTotal4"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With

            '+ CA080, CA080A: Recibo manual
            Case "CA080", "CA080A"
                Dim SelLength As Integer = 0
                mobjPolicyTra = New ePolicy.TDetail_pre

                With Request
                    'mstrQueryString = "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&dEffecdate=" & Request.Form.Item("hddStartDateR") & "&dExpirDate=" & Request.Form.Item("hddExpirDate") & "&nReceipt=" & Request.Form.Item("hddReceipt") & "&dIssuedat=" & Request.Form.Item("hddIssueDate") & "&nCurrency=" & Request.Form.Item("hddCurrency") & "&nTratypei=" & Request.Form.Item("hddSource") & "&sOrigReceipt=" & Request.Form.Item("hddOrigReceipt") & "&sCodisplOrig=" & .QueryString.Item("sCodisplOrig") & "&sCertype=" & .QueryString.Item("sCertype") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dNullDate=" & .QueryString.Item("dNullDate") & "&sNullReceipt=" & .QueryString.Item("sNullReceipt") & "&sTypeReceipt=" & .QueryString.Item("sTypeReceipt") & "&nExeMode=" & .QueryString.Item("nExeMode") & "&sExeReport=" & .QueryString.Item("sExeReport") & "&nAgency=" & .QueryString.Item("nAgency") & "&sOnSeq=" & .QueryString.Item("sOnSeq") & "&sNewData=" & .QueryString.Item("sNewData")
                    mstrQueryString = "&sCodisplOrig=" & .QueryString.Item("sCodisplOrig") &
                              "&sCertype=" & .QueryString.Item("sCertype") &
                              "&nBranch=" & .QueryString.Item("nBranch") &
                              "&nProduct=" & .QueryString.Item("nProduct") &
                              "&nPolicy=" & .QueryString.Item("nPolicy") &
                              "&nCertif=" & .QueryString.Item("nCertif") &
                              "&dNullDate=" & .QueryString.Item("dNullDate") &
                              "&sNullReceipt=" & .QueryString.Item("sNullReceipt") &
                              "&soptReceipt=" & .QueryString.Item("soptReceipt") &
                              "&nExeMode=" & .QueryString.Item("nExeMode") &
                              "&sExeReport=" & .QueryString.Item("sExeReport") &
                              "&nAgency=" & .QueryString.Item("nAgency") &
                              "&nReceiptGrid=" & .QueryString.Item("nReceiptGrid") &
                              "&sOnSeq=" & .QueryString.Item("sOnSeq") &
                              "&nPremium_Collect=" & .QueryString.Item("nPremium_Collect") &
                              "&sClient=" & .QueryString.Item("sClient") &
                              "&hddClient=" & .QueryString.Item("hddClient") &
                              "&nTypeReceipt=" & .QueryString.Item("nTypeReceipt")


                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValPolicyTra = mobjPolicyTra.insValCA080_K("CA080", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValPolicyTra = mobjPolicyTra.insValCA080(.QueryString.Item("WindowType"),
                                                                        .QueryString.Item("sCodispl"),
                                                                        mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                                        ,
                                                                        mobjValues.StringToType(.Form.Item("hddStartDateR"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("hddExpirDate"), eFunctions.Values.eTypeData.etdDate),
                                                                                        ,
                                                                        mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate),
                                                                                        ,
                                                                                        ,
                                                                        mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble),
                                                                        mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        .Form.Item("cbeType"),
                                                                        .Form.Item("hddCacalili"),
                                                                        .Form.Item("hddCommissi_i"),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger, True),
                                                                        .Form.Item("hddPrem_det_proc"),
                                                                        mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble),
                                                                        mobjValues.StringToType(.Form.Item("hddReceiptCollect"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremium_Collec"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremiumFact_All"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremium_All"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        .Form.Item("hddType"),,,,, Session("nUsercode"),
                                                                        Session("SessionID"))
                        Else
                            'If Not String.IsNullOrEmpty(.Form.Item("Sel")) Then
                            '    SelLength = .Form.Item("Sel").Split(",").Length
                            'End If

                            insValPolicyTra = mobjPolicyTra.insValCA080(.QueryString.Item("WindowType"),
                                                                        .QueryString.Item("sCodispl"),
                                                                        mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                        mobjValues.StringToType(.Form.Item("Sel").Count, eFunctions.Values.eTypeData.etdDouble),
                                                                        mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate),
                                                                        mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        .Form.Item("tctOrigReceipt"),,,,,,,,,,,,,
                                                                        mobjValues.StringToType(.Form.Item("tcnReceipt_Collec"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremium_Collec"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremiumFact_All"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnPremium_All"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        .Form.Item("optType"), .Form.Item("chkDelReceipt"), .Form.Item("tctClient"),
                                                                        Session("sCodisplOri"),
                                                                        mobjValues.StringToType(.Form.Item("hddPorcCommision"), eFunctions.Values.eTypeData.etdDouble),
                                                                        Session("nUsercode"),
                                                                        Session("SessionID"),
                                                                        Request.Form.Item("chkDevReceipt"),
                                                                        mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("tcnCoupon"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                        mobjValues.StringToType(.Form.Item("hddCouponAmount"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    End If
                End With


            Case Else
                insValPolicyTra = "insValPolicyTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostPolicyTra: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Private Function insPostPolicyTra() As Boolean
        ''Dim eRemoteDB.Constants.intNull As Object
        Dim clngActionUpdate As String
        Dim clngActionQuery As String
        'dim dtmNull As Object
        Dim clngAcceptdataAccept As String
        Dim lstrMessageCa028 As String
        Dim soptDev As String
        Dim lintCountCA051 As Integer
        Dim lblnP_data As Object
        Dim sApv As String
        Dim lobjWorksheet As Object
        Dim mobjtRehabilitate As Object
        Dim lclsGeneralObj As Object
        Dim sReceipt_ind As String
        Dim lclsProduct_li As Object
        Dim lstrMessageProposal As String
        Dim lobjSheet As Object
        Dim sCodisplOri As String
        Dim mclsProduct_li As Object
        Dim llngnPayOrderTyp As Byte
        Dim lclsErrors As Object
        Dim lclsGeneralCa028 As Object
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lclsCertificat As Object
        Dim lclsGeneral As Object
        Dim lstrMessage As String
        Dim lstrClient As Object

        Dim ldtmNexRecip As Object
        Dim lblnReceipt As Object

        Dim lsClient As String
        Dim lnCurrency As Byte
        Dim lstrCurrency As String
        Dim llngPayOrderTyp As Byte
        Dim chkSurrTot As String
        Dim llngConcept As Byte
        Dim llngTypesupport As Byte
        Dim ldblAfect As Double
        Dim ldblExcent As Double

        '-Objeto para transacciones batch	
        Dim lclsBatch_param As Object

        lblnPost = False

        Select Case Request.QueryString.Item("sCodispl")

            '+ CA032: Reverso de modificación/renovación de una póliza
            Case "CA032"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif")
                        lblnPost = True
                    Else
                        'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.ValPolicyTra
                        lblnPost = mobjPolicyTra.insPostCA032("CA032", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNullOutMov"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctReverCertif"), mobjValues.StringToType(.Form.Item("chkNullPropQuot"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+CA033: Anulación de una póliza                
            Case "CA033"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                        lblnPost = mobjPolicyTra.insPostCA033(mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), "CA033", Session("sCodisplOri"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToDate(.Form.Item("tcdNullDate")), mobjValues.StringToDate(.Form.Item("tcdNullDate")), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nTransacion"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("optReceipt"), eFunctions.Values.eTypeData.etdDouble), Session("OptExecute"), .Form.Item("ChkNullRequest"), .Form.Item("chkNullReceipt"), Session("nOperat"), mobjValues.StringToType(.QueryString("nNoteNum"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sDescript"), Session("nAgency"), .Form.Item("optDev"), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost = True Then
                            '+ Si el usuario pidió generar propuesta, se recupera el número de propuesta creado
                            '+ para la póliza/certificado en tratamiento, y se muestra el mensaje al usuario
                            If CDbl(.Form.Item("ChkNullRequest")) = 1 And CStr(Session("OptExecute")) = "1" Then
                                'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                lclsCertificat = New ePolicy.Certificat
                                llngProposal = lclsCertificat.insProposal_of_Pol("8", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                                lclsCertificat = Nothing
                                'UPGRADE_NOTE: The 'eFunctions.Errors' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                                    ElseIf Request.Form.Item("optDev") = "9" Then
                                        soptDev = "9"

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
                                lblnP_data = mobjPolicyTra.UpdatePartic_data(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
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
                            'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.insPostCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nExeMode"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), Session("nUsercode"), .Form.Item("chkNullDevRec"), .Form.Item("chkNullReceipt"), .QueryString("nExeMode"), .Form.Item("chkRescRequest"), Session("nOperat"), mobjValues.StringToType(.Form.Item("nDay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullLetter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProcess"), eFunctions.Values.eTypeData.etdInteger))

                            sBranchtCA034 = mobjPolicyTra.sBrancht
                            'mobjPolicyTra.nProdClas = 7 And 						

                            If mobjPolicyTra.sBrancht = "1" And lblnPost Then

                                'UPGRADE_NOTE: The 'ePolicy.tRehabilitate' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                mobjtRehabilitate = New ePolicy.TRehabilitate
                                Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .QueryString("nExeMode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), 1)
                                Session("sKey") = mobjtRehabilitate.sKey
                                mobjtRehabilitate = Nothing
                                '+ Llamada al procedimiento que invoca al reporte
                                If .Form.Item("chkRescReport") = "1" Then
                                    Call insPrintPolicyRep("CAL034")
                                End If

                            Else
                                If .Form.Item("chkRescReport") = "1" And lblnPost Then
                                    'UPGRADE_NOTE: The 'ePolicy.tRehabilitate' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjtRehabilitate = New ePolicy.TRehabilitate
                                    Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .QueryString("nExeMode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble))
                                    Session("sKey") = mobjtRehabilitate.sKey
                                    mobjtRehabilitate = Nothing
                                    '+ Llamada al procedimiento que invoca al reporte
                                    Call insPrintPolicyRep("CAL034")
                                End If
                            End If
                        End If
                    End If

                    If CStr(Session("sCodispl")) <> "CA767" Then

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
                        End If

                        If CDbl(.QueryString.Item("nZone")) <> 1 Then
                            lblnPost = True
                            Session("dEffecdate") = mobjValues.StringToDate(.Form.Item("tcdNullDate"))

                            'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.insPostCA034(Session("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nExeMode"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkNullDevRec"), .Form.Item("chkNullReceipt"), .QueryString("nExeMode"), .Form.Item("chkRescRequest"), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nDay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valNullLetter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nAgency"), eFunctions.Values.eTypeData.etdDouble))
                            If lblnPost Then
                                Session("nProposal") = mobjPolicyTra.nProposal
                                'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                            'mobjPolicyTra.nProdclas = 7  And 						If mobjPolicyTra.sBrancht = "1" And 						   lblnPost						Then
                            Session("sCodisplOri") = "CA034"
                            'UPGRADE_NOTE: The 'ePolicy.tRehabilitate' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjtRehabilitate = New ePolicy.TRehabilitate
                            Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .QueryString("nExeMode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), 1)
                            Session("sKey") = mobjtRehabilitate.sKey
                            mobjtRehabilitate = Nothing
                            '+ Llamada al procedimiento que invoca al reporte
                            If .Form.Item("chkRescReport") = "1" Then
                                Call insPrintPolicyRep("CAL034")
                            End If



                        Else
                            If mobjPolicyTra.sBrancht = "1" And CStr(Session("sPolitype")) = "1" And lblnPost Then
                                'UPGRADE_NOTE: The 'ePolicy.tRehabilitate' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                mobjtRehabilitate = New ePolicy.TRehabilitate
                                Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .QueryString("nExeMode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble), 1)
                                Session("sKey") = mobjtRehabilitate.sKey
                                mobjtRehabilitate = Nothing
                                '+ Llamada al procedimiento que invoca al reporte
                                If .Form.Item("chkRescReport") = "1" Then
                                    Call insPrintPolicyRep("CAL034")
                                End If

                            Else
                                If .Form.Item("chkRescReport") = "1" And lblnPost Then
                                    'UPGRADE_NOTE: The 'ePolicy.tRehabilitate' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjtRehabilitate = New ePolicy.TRehabilitate
                                    Call mobjtRehabilitate.Inscalrehabilitate(mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdNullDate")), .QueryString("nExeMode"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkNullReceipt"), eFunctions.Values.eTypeData.etdDouble))
                                    Session("sKey") = mobjtRehabilitate.sKey
                                    mobjtRehabilitate = Nothing
                                    '+ Llamada al procedimiento que invoca al reporte
                                    Call insPrintPolicyRep("CAL034")
                                End If
                            End If
                        End If
                    End If
                    'End If
                End With

            '+CA034A: Rehabilitación de una póliza (Versión ING)
            Case "CA034A"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    lblnPost = mobjPolicyTra.insPostCA034A(.Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))

                    If mobjPolicyTra.nProposal > 0 Then
                        nProposal = mobjPolicyTra.nProposal
                        mobjPolicyTra = Nothing
                        'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New eGeneral.GeneralFunction
                        Response.Write("<SCRIPT>alert(""Men. 55940: " & mobjPolicyTra.insLoadMessage(55940) & " " & nProposal & """);</" & "Script>")
                    End If

                    If lblnPost Then
                        Call insPrintPolicyRep("CAL034A")
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
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                        'UPGRADE_NOTE: The 'ePolicy.TDetail_pre' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.TDetail_pre
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            'mstrQueryString = "&sCodisplOri=CA028" & "&nConcept=24" & "&dEffecdate=" & .Form.Item("tcdStartDateR") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("hddAmountTot") & "&nCurrencypay=1" & "&nAmountPay=" & .Form.Item("hddAmountTotPay") & "&nPayOrderTyp=2" & "&sCertype=2" & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&sClient=" & .Form.Item("hddClient_policy") & "&sBenef=" & .Form.Item("hddClient_policy") & "&nBranchPay=" & .Form.Item("cbeBranchPay") & "&nProductPay=" & .Form.Item("valProductPay") & "&nPolicyPay=" & .Form.Item("tcnPolicyPay") & "&nCertifPay=" & .Form.Item("tcnCertifPay") & "&nBalance=" & "" & "&nOperat=" & "" & "&sAnulReceipt=" & "" & "&sReport=" & "" & "&nOffice=" & "" & "&nOfficeAgen=" & "" & "&nAgency=" & "" & "&nReceipt=" & .Form.Item("tcnReceipt") & "&dExpirDat=" & .Form.Item("tcdExpirDateR") & "&nSource=" & .Form.Item("cbeSource") & "&nTypeReceipt=" & .Form.Item("optType") & "&sOrigReceipt=" & .Form.Item("tctOrigReceipt") & "&sKey=" & .Form.Item("hddKey") & "&sAdjust=" & .Form.Item("chkAdjust") & "&nAdjReceipt=" & .Form.Item("tcnAdjReceipt") & "&nAdjAmount=" & .Form.Item("tcnAdjAmount") & "&nTypePay=" & .Form.Item("cbePayWay") & "&nOrigin_apv=" & .Form.Item("valOrigin")
                            'lblnPost = mobjPolicyTra.insPostCA028Upd(.QueryString("sCodispl"), .QueryString("sKey"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                            lblnPost = mobjPolicyTra.insPostCA028Upd(.QueryString("sCodispl"), .QueryString("sKey"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                        Else
                            '+Se es un recibo de cobro o la forma de pago es:
                            '+3 Cargo cuenta corriente poliza 
                            '+4 Cargo cuenta corriente cliente
                            mstrCodispl = "CA028"
                            If .Form.Item("cbePayWay") > "2" Or .Form.Item("optType") = "1" Then
                                '+Si el Producto es Unit Linked, se crea el registro de TDetail_pre                           
                                If .Form.Item("hddProdClas") = "4" Then
                                    'UPGRADE_NOTE: The 'ePolicy.TDetail_pre' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mclsTDetail_pre = New ePolicy.TDetail_pre

                                    Call mclsTDetail_pre.inspreCA028Grid(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdStartDate_policy"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), "1", Request.Form.Item("hddKey"), "2", .Form.Item("tctOrigReceipt"), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble))
                                End If

                                'lblnPost = mobjPolicyTra.insPostCA028(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient_policy"), mobjValues.StringToType(.Form.Item("cbenreceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctOrigReceipt"), Session("nUsercode"), Session("OptExecute"), Request.Form.Item("chkDelReceipt"), Request.Form.Item("hddKey"), .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("cbenreceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayWay"), eFunctions.Values.eTypeData.etdLong, True), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("hddClient_policy"))
                                lblnPost = mobjPolicyTra.insPostCA028(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient_policy"), mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctOrigReceipt"), Session("nUsercode"), Session("OptExecute"), Request.Form.Item("chkDelReceipt"), Request.Form.Item("hddKey"), .Form.Item("chkAdjust"), mobjValues.StringToType(.Form.Item("cbenreceipt"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnAdjAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbePayWay"), eFunctions.Values.eTypeData.etdLong, True), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .Form.Item("hddClient_policy"))
                                If lblnPost And Request.Form.Item("chkDelReceipt") <> "1" And mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, 0) = eRemoteDB.Constants.intNull Then
                                    '+ Se envia alerta con número de recibo generado 
                                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                                mstrQueryString = "&sCodisplOri=CA028" & "&nConcept=24" & "&dEffecdate=" & .Form.Item("tcdStartDateR") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("hddAmountTot") & "&nCurrencypay=1" & "&nAmountPay=" & .Form.Item("hddAmountTotPay") & "&nPayOrderTyp=2" & "&sCertype=2" & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&sClient=" & .Form.Item("hddClient_policy") & "&sBenef=" & .Form.Item("hddClient_policy") & "&nBranchPay=" & .Form.Item("cbeBranchPay") & "&nProductPay=" & .Form.Item("valProductPay") & "&nPolicyPay=" & .Form.Item("tcnPolicyPay") & "&nCertifPay=" & .Form.Item("tcnCertifPay") & "&nBalance=" & "" & "&nOperat=" & "" & "&sAnulReceipt=" & "" & "&sReport=" & "" & "&nOffice=" & "" & "&nOfficeAgen=" & "" & "&nAgency=" & "" & "&nReceipt=" & .Form.Item("tcnReceipt") & "&dExpirDat=" & .Form.Item("tcdExpirDateR") & "&nSource=" & .Form.Item("cbeSource") & "&nTypeReceipt=" & .Form.Item("optType") & "&sOrigReceipt=" & .Form.Item("tctOrigReceipt") & "&sKey=" & .Form.Item("hddKey") & "&sAdjust=" & .Form.Item("chkAdjust") & "&nAdjReceipt=" & .Form.Item("cbenreceipt") & "&nAdjAmount=" & .Form.Item("tcnAdjAmount") & "&nTypePay=" & .Form.Item("cbePayWay") & "&nOrigin_apv=" & .Form.Item("valOrigin")

                            End If
                        End If
                    End If
                End With

            '+CA038: Cambio de fecha de renovación
            Case "CA038"
                'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.ValPolicyTra

                    Session("sCertype") = "2"
                    Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nCertif") = mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                    Session("dEffecdate") = mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate)

                    lblnPost = mobjPolicyTra.insPostCA037_k("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToDate(.Form.Item("tcdExpirdateNew")), "1", mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdExpirdateNew"), eFunctions.Values.eTypeData.etdDate))

                    Session("dNextReceip") = eRemoteDB.Constants.dtmNull
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
                        If .Form.Item("hddsSurrPayWay") > "2" And .Form.Item("hddsSurrPayWay") <> "5" Or .Form.Item("hddsProcessType") = "1" Then
                            'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.ValPolicyTra
                            lblnPost = mobjPolicyTra.InsPostVI009("2", mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddsSurrType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddsProcessType"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequest"), mobjValues.StringToType(.Form.Item("hddsSurrPayWay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnBalance"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnOperat"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("hddsAnulReceipt"), mobjValues.StringToType(.Form.Item("hddTaxSurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddSurrValue_Tax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdPaymentDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrCostPar"), eFunctions.Values.eTypeData.etdDouble))

                            If mobjPolicyTra.nProposal > 0 Then
                                nProposal = mobjPolicyTra.nProposal
                                mobjPolicyTra = Nothing
                                'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                            Session("sSurrPayWay") = .Form.Item("hddsSurrPayWay")
                            lblnPost = True
                            '+Se llama a la OP06-2 si la opción de ejecución es definitiva
                            If .Form.Item("hddsProcessType") = "2" Then
                                llngnPayOrderTyp = 2
                                If .Form.Item("hddsSurrPayWay") = "2" Then
                                    llngnPayOrderTyp = 4
                                End If

                                If .Form.Item("hddsSurrPayWay") = "5" Then
                                    llngConcept = 28
                                Else
                                    llngConcept = 11
                                End If
                                Session("OP006_sCodispl") = "VI009"
                                Session("OP006_dReqDate") = mobjValues.StringToType(.Form.Item("tcdPaymentDate"), eFunctions.Values.eTypeData.etdDate)
                                mstrCodispl = "OP06-2"
                                mstrQueryString = "&sCodisplOri=VI009" & "&sBenef=" & .Form.Item("tctClient") & "&nConcept=" & llngConcept & "&dEffecdate=" & .Form.Item("hdddEffecdate") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("tcnRescDef") & "&nAmountPay=" & .Form.Item("tcnSurrCurr") & "&nPayOrderTyp=" & llngnPayOrderTyp & "&nBranch=" & .Form.Item("hddnBranch") & "&nProduct=" & .Form.Item("hddnProduct") & "&nPolicy=" & .Form.Item("hddnPolicy") & "&nCertif=" & .Form.Item("hddnCertif") & "&dRescdate=" & .Form.Item("hdddEffecdate") & "&sSurrType=" & .Form.Item("hddsSurrType") & "&sProcessType=" & .Form.Item("hddsProcessType") & "&sRequest=" & .Form.Item("chkRequest") & "&sSurrPayWay=" & .Form.Item("hddsSurrPayWay") & "&nSurrAmount=" & .Form.Item("tcnSurrAmount") & "&nCurrency=" & .Form.Item("hddnCurrency") & "&nCurrencypay=1" & "&sClient=" & .Form.Item("tctClient") & "&nBranchPay=" & .Form.Item("cbeBranch") & "&nProductPay=" & .Form.Item("valProduct") & "&nPolicyPay=" & .Form.Item("tcnPolicy") & "&nCertifPay=" & .Form.Item("tcnCertif") & "&nProponum=" & .Form.Item("hddnProponum") & "&nBalance=" & .Form.Item("hddnBalance") & "&nOperat=" & .Form.Item("hddnOperat") & "&sAnulReceipt=" & .Form.Item("hddsAnulReceipt") & "&sReport=" & .Form.Item("chkReport") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&tcnCapital=" & .Form.Item("tcnCapital") & "&hddTaxSurr=" & .Form.Item("hddTaxSurr") & "&hddSurrValue_Tax=" & .Form.Item("hddSurrValue_Tax") & "&tcdPaymentDate=" & .Form.Item("tcdPaymentDate") & "&tcnPremium=" & .Form.Item("tcnPremium") & "&tcnSurrVal=" & .Form.Item("tcnSurrVal") & "&tcnLoans=" & .Form.Item("tcnLoans") & "&tcnInterest=" & .Form.Item("tcnInterest") & "&tcnSurrCostPar=" & .Form.Item("tcnSurrCostPar")
                            End If
                        End If
                    End If
                End With

            '+VI7000: Rescate de póliza
            Case "VI7000"

                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        lblnPost = True

                        'UPGRADE_NOTE: The 'ePolicy.Roles' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.Roles

                        If mobjPolicyTra.Find("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), 1, "", mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                            lsClient = mobjPolicyTra.sClient
                        End If
                        If IsNothing(.QueryString("sCodisplOri")) Then
                            Session("sCodisplOri") = "VI7000"
                        Else
                            Session("sCodisplOri") = .QueryString.Item("sCodisplOri")
                        End If

                        mobjPolicyTra = Nothing
                        'UPGRADE_NOTE: The 'ePolicy.Curren_pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.Curren_pol
                        lstrCurrency = mobjPolicyTra.findCurrency("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))

                        If lstrCurrency = "*" Then
                            lnCurrency = 1
                        Else
                            lnCurrency = mobjPolicyTra.nCurrency
                        End If

                        mobjPolicyTra = Nothing

                        mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nCurrency=" & lnCurrency & "&sProcess=" & .Form.Item("optProcessType") & "&nOffice=" & .Form.Item("cbeOffice") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&nAgency=" & .Form.Item("cbeAgency") & "&sClientBenef=" & lsClient & "&nProponum=" & .Form.Item("tcnProponum") & "&sSurrType=" & .Form.Item("optSurrType") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sCodisplOri=" & Request.QueryString.Item("sCodisplOri") & "&nOperat=" & Request.QueryString.Item("nOperat") & "&nSurrReas=" & .Form.Item("cbeSurrReas") & "&nDelete=1" & "&sInd_Insur=" & .Form.Item("chkInsur")
                        Session("sCertype") = "2"
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("nCurrency") = lnCurrency
                        Session("optProcessType") = .Form.Item("optProcessType")
                        Session("sSurrType") = .Form.Item("optSurrType")
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        Session("nSurrReas") = .Form.Item("cbeSurrReas")
                        Session("sApv") = .Form.Item("hsApv")
                        Session("sInd_Insur") = .Form.Item("chkInsur")

                    Else
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            If CStr(Session("sSurrType")) = "1" Then
                                chkSurrTot = "1"
                            Else
                                chkSurrTot = "0"
                            End If
                            '+ Si se trata de un rescate preliminar, se hace el llamado a la función que creará la propuesta y reportará el número generado al User
                            '+ Con el parámetro sProcessType = "1" se le indica que no actualice las tablas concernientes a fondos pues es un rescate preliminar
                            If CStr(Session("optProcessType")) = "1" Then
                                'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                mobjPolicyTra = New ePolicy.ValPolicyTra
                                '+ Modificación [APV2] - ACM - 17/09/2003
                                lblnPost = mobjPolicyTra.InsPostVI7000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotSurrCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "1", mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), eRemoteDB.Constants.intNull, Session("sInd_Insur"), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddIsCancelling"))

                                If mobjPolicyTra.nProposal > 0 Then
                                    llngProposal = mobjPolicyTra.nProposal
                                    mobjPolicyTra = Nothing
                                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New eGeneral.GeneralFunction
                                    Response.Write("<SCRIPT>alert(""Men. 55940: " & mobjPolicyTra.insLoadMessage(55940) & " " & llngProposal & """);</" & "Script>")
                                End If
                            Else
                                lblnPost = True

                                '+ Sólo si se solicita la solicitud de orden de pago se irá a dicha transacción
                                Session("cbePmtOrd") = .Form.Item("cbePmtOrd")
                                'UPGRADE_NOTE: A string expression is used as boolean value. It has a different behavior than the original code. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1021.htm
                                If CDbl(.Form.Item("cbePmtOrd")) = 1 Then

                                    'UPGRADE_NOTE: The 'ePolicy.Surr_Origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New ePolicy.Surr_origins
                                    '+ Si la lectura del total de rescate es satisfactoria, los valores on extraidos desde la BD
                                    If mobjPolicyTra.Find_tot(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate)) Then

                                        ldblAfect = mobjValues.StringToType(.Form.Item("hddProfit"), eFunctions.Values.eTypeData.etdDouble)
                                        If ldblAfect < 0 Then
                                            ldblAfect = 0
                                        End If
                                        ldblExcent = mobjPolicyTra.nRequestedAmount - ldblAfect + mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble)

                                    Else
                                        ldblAfect = mobjValues.StringToType(.Form.Item("hddProfit"), eFunctions.Values.eTypeData.etdDouble)
                                        If ldblAfect < 0 Then
                                            ldblAfect = 0
                                        End If
                                        ldblExcent = mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble) - ldblAfect + mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble)

                                    End If
                                    If mobjPolicyTra.nRequestedAmount = 0 Then

                                        Session("optProcessType") = "1"
                                        'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                        mobjPolicy_Tra = New ePolicy.ValPolicyTra
                                        lblnPost = mobjPolicy_Tra.InsPostVI7000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjPolicyTra.nAmount, mobjValues.StringToType(.Form.Item("tcnCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), eRemoteDB.Constants.intNull, Session("sInd_Insur"), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddIsCancelling"))
                                        mobjPolicyTra = Nothing
                                    Else

                                        If String.IsNullOrEmpty(.Form.Item("dtcClient")) Then
                                            Session("OP006_sBenef") = .Form.Item("hddClientBenef")
                                        Else
                                            Session("OP006_sBenef") = .Form.Item("dtcClient")
                                        End If

                                        '+ Se asigna el concepto de la Orden de Pago dependiendo del Tipo de Rescate
                                        If CStr(Session("nSurrReas")) = "1" Then 'Retiro de fondos
                                            llngConcept = 25 '+Rescate Poliza UL
                                            llngTypesupport = 4 'No tiene

                                        Else
                                            If CStr(Session("nSurrReas")) = "2" Then 'Traspaso de prima
                                                llngConcept = 26
                                            Else
                                                'Devolución de prima
                                                llngConcept = 27
                                            End If
                                            llngTypesupport = 4 'No tiene
                                        End If

                                        If CDbl(.Form.Item("cbePmtOrd")) = 5 Then
                                            llngConcept = 28 ' Vale Vista
                                            llngTypesupport = 4 'No tiene
                                        End If
                                        If CStr(Session("sSurrType")) = "1" Then
                                            llngConcept = 50 ' Rescate total UL
                                        Else
                                            llngConcept = 51 ' Rescate Parcial ul
                                        End If
                                        llngPayOrderTyp = 2
                                        Session("OP006_sCodispl") = "VI7000"
                                        Session("OP006_nPayOrderTyp") = "2"
                                        Session("OP006_dReqDate") = mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate)
                                        mstrCodispl = "OP06-2"
                                        mstrQueryString = "&sCodisplOri=VI7000" & "&sBenef=" & Session("OP006_sBenef") & "&nConcept=" & llngConcept & "&dEffecdate=" & Session("dEffecdate") & "&nAmount=" & mobjPolicyTra.nAmount & "&nAmountPay=" & mobjPolicyTra.nRequestedAmount & "&nPayOrderTyp=" & llngPayOrderTyp & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dRescdate=" & Session("dEffecdate") & "&sSurrType=" & Session("sSurrType") & "&nSurrAmt=" & mobjPolicyTra.nAmount & "&nCurrency=" & Session("nCurrency") & "&sClient=" & Session("OP006_sBenef") & "&nBranchPay=" & Session("nBranch") & "&nProductPay=" & Session("nProduct") & "&nPolicyPay=" & Session("nPolicy") & "&nCertifPay=" & Session("nCertif") & "&sCertype=" & Session("sCertype") & "&sSurrTot=" & chkSurrTot & "&nCoverCost=" & .Form.Item("hddnCoverCost") & "&nPmtOrd=" & .Form.Item("cbePmtOrd") & "&nSurrReas=" & Session("nSurrReas") & "&nRetention=" & .Form.Item("hddnTotRetention") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&sProcess=" & .Form.Item("hddProcess") & "&nProponum=" & .Form.Item("hddProponum") & "&sClientEnt=" & .Form.Item("dtcClient") & "&nOrigin_apv=" & .Form.Item("valOrigin") & "&nTypesupport=" & llngTypesupport & "&nExcent=" & ldblExcent & "&nAfect=" & ldblAfect & "&nTax_amount=" & .Form.Item("hddnTotRetention") & "&nAmounttotal=" & mobjPolicyTra.nRequestedAmount & "&sInd_Insur=" & Session("sInd_Insur")

                                    End If
                                Else

                                    'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New ePolicy.ValPolicyTra
                                    lblnPost = mobjPolicyTra.InsPostVI7000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), Session("optProcessType"), mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), eRemoteDB.Constants.intNull, Session("sInd_Insur"), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddIsCancelling"))

                                    mobjPolicyTra = Nothing

                                End If
                            End If

                            If CStr(Session("optProcessType")) = "1" Or (CStr(Session("optProcessType")) <> "1" And CDbl(.Form.Item("cbePmtOrd")) <> 1) Then
                                'UPGRADE_NOTE: The 'eProduct.Product' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                mclsProduct_li = New eProduct.Product
                                Call mclsProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), True)
                                If mclsProduct_li.NSAVING_PCT = 0 Then
                                    Call insPrintPolicyRep("VI7000")
                                Else
                                    Call insPrintPolicyRep("VIL009_1")
                                End If
                                mclsProduct_li = Nothing
                            End If
                        Else


                            mstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & Session("nCurrency") & "&sProcess=" & Session("optProcessType") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&sClientBenef=" & .Form.Item("hddClientBenef") & "&nProponum=" & .Form.Item("hddProponum") & "&nSurrReas=" & Session("nSurrReas") & "&sSurrType=" & Session("sSurrType") & "&sClientDest=" & .Form.Item("hddClientDest") & "&sInd_Insur=" & Session("sInd_Insur")

                            'UPGRADE_NOTE: The 'ePolicy.Surr_origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Surr_origins

                            lblnPost = mobjPolicyTra.InsPostVI7000_Upd("Add", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAvailBal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), mobjValues.StringToType(.Form.Item("tcnRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWDCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdPaymentDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCost_cov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble))

                            Session("dEffecdate") = .Form.Item("hdddEffecdate")
                            mobjPolicyTra = Nothing
                        End If
                    End If
                End With

            '+VI7004: Rescate de póliza APV
            Case "VI7004"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        lblnPost = True

                        'UPGRADE_NOTE: The 'ePolicy.Roles' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.Roles

                        If mobjPolicyTra.Find("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), 1, "", mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                            lsClient = mobjPolicyTra.sClient
                        End If

                        mobjPolicyTra = Nothing
                        'UPGRADE_NOTE: The 'ePolicy.Curren_pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.Curren_pol
                        lstrCurrency = mobjPolicyTra.findCurrency("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))

                        If lstrCurrency = "*" Then
                            lnCurrency = 1
                        Else
                            lnCurrency = mobjPolicyTra.nCurrency
                        End If

                        mobjPolicyTra = Nothing

                        mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nCurrency=" & lnCurrency & "&sProcess=" & .Form.Item("optProcessType") & "&nOffice=" & .Form.Item("cbeOffice") & "&nOfficeAgen=" & .Form.Item("cbeOfficeAgen") & "&nAgency=" & .Form.Item("cbeAgency") & "&sClientBenef=" & lsClient & "&nProponum=" & .Form.Item("tcnProponum") & "&sSurrType=" & .Form.Item("optSurrType") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sCodisplOri=" & Request.QueryString.Item("sCodisplOri") & "&nOperat=" & Request.QueryString.Item("nOperat") & "&nSurrReas=" & .Form.Item("cbeSurrReas") & "&nDelete=1" & "&sInd_Insur=" & .Form.Item("chkInsur")
                        Session("sCertype") = "2"
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("nCurrency") = lnCurrency
                        Session("optProcessType") = .Form.Item("optProcessType")
                        Session("sSurrType") = .Form.Item("optSurrType")
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        Session("nSurrReas") = .Form.Item("cbeSurrReas")
                        Session("sApv") = .Form.Item("hsApv")
                        Session("sInd_Insur") = .Form.Item("chkInsur")
                    Else
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            If CStr(Session("sSurrType")) = "1" Then
                                chkSurrTot = "1"
                            Else
                                chkSurrTot = "0"
                            End If

                            '+ Si se trata de un rescate preliminar, se hace el llamado a la función que creará la propuesta y reportará el número generado al User
                            '+ Con el parámetro sProcessType = "1" se le indica que no actualice las tablas concernientes a fondos pues es un rescate preliminar
                            If CStr(Session("optProcessType")) = "1" Then
                                'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                mobjPolicyTra = New ePolicy.ValPolicyTra

                                '+ Modificación [APV2] - ACM - 17/09/2003


                                lblnPost = mobjPolicyTra.InsPostVI7004(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotSurrCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "1", mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), eRemoteDB.Constants.intNull, Session("sInd_Insur"), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddIsCancelling"), mobjValues.StringToType(.Form.Item("cbeTyp_Profitworker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSaapv"), eFunctions.Values.eTypeData.etdDouble))

                                If mobjPolicyTra.nProposal > 0 Then
                                    llngProposal = mobjPolicyTra.nProposal
                                    mobjPolicyTra = Nothing
                                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New eGeneral.GeneralFunction
                                    Response.Write("<SCRIPT>alert(""Men. 55940: " & mobjPolicyTra.insLoadMessage(55940) & " " & llngProposal & """);</" & "Script>")
                                End If
                            Else
                                lblnPost = True

                                '+ Sólo si se solicita la solicitud de orden de pago se irá a dicha transacción
                                Session("cbePmtOrd") = .Form.Item("cbePmtOrd")
                                'UPGRADE_NOTE: A string expression is used as boolean value. It has a different behavior than the original code. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1021.htm
                                If mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdLong) = 1 Or mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdLong) = 5 And Not mobjValues.StringToType(.Form.Item("hddIsCancelling"), eFunctions.Values.eTypeData.etdBoolean) Then
                                    'UPGRADE_NOTE: The 'ePolicy.Surr_Origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New ePolicy.Surr_origins

                                    If String.IsNullOrEmpty(.Form.Item("dtcClient")) Then
                                        Session("OP006_sBenef") = .Form.Item("hddClientBenef")
                                    Else
                                        Session("OP006_sBenef") = .Form.Item("dtcClient")
                                    End If

                                    '+ Se asigna el concepto de la Orden de Pago dependiendo del Tipo de Rescate
                                    If CStr(Session("nSurrReas")) = "1" Then 'Retiro de fondos
                                        llngConcept = 25 '+Rescate Poliza UL
                                        llngTypesupport = 4 'No tiene

                                    Else
                                        If CStr(Session("nSurrReas")) = "2" Then 'Traspaso de prima
                                            llngConcept = 26
                                        Else
                                            'Devolución de prima
                                            llngConcept = 27
                                        End If
                                        llngTypesupport = 4 'No tiene
                                    End If

                                    If CDbl(.Form.Item("cbePmtOrd")) = 5 Then
                                        llngConcept = 28 ' Vale Vista
                                        llngTypesupport = 4 'No tiene
                                    End If
                                    If CStr(Session("nSurrReas")) = "2" Then 'Traspaso de prima
                                        If CStr(Session("sSurrType")) = "1" Then
                                            llngConcept = 54 ' Traspaso total apv
                                        Else
                                            llngConcept = 55 ' Traspaso Parcial apv
                                        End If
                                    Else
                                        If CStr(Session("sSurrType")) = "1" Then
                                            llngConcept = 52 ' Rescate total Apv
                                        Else
                                            llngConcept = 53 ' Rescate Parcial uApv
                                        End If
                                    End If


                                    llngPayOrderTyp = 2

                                    Session("OP006_sCodispl") = "VI7004"
                                    Session("OP006_nPayOrderTyp") = "2"
                                    Session("OP006_dReqDate") = mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate)
                                    mstrCodispl = "OP06-2"

                                    '+ Si la lectura del total de rescate es satisfactoria, los valores on extraidos desde la BD
                                    If mobjPolicyTra.Find_tot(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate)) Then

                                        ldblAfect = mobjValues.StringToType(.Form.Item("hddProfit"), eFunctions.Values.eTypeData.etdDouble)
                                        If ldblAfect < 0 Then
                                            ldblAfect = 0
                                        End If
                                        ldblExcent = mobjPolicyTra.nRequestedAmount - ldblAfect + mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble)

                                    Else
                                        ldblAfect = mobjValues.StringToType(.Form.Item("hddProfit"), eFunctions.Values.eTypeData.etdDouble)
                                        If ldblAfect < 0 Then
                                            ldblAfect = 0
                                        End If
                                        ldblExcent = mobjValues.StringToType(.Form.Item("tcnTotal"), eFunctions.Values.eTypeData.etdDouble) - ldblAfect + mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble)

                                    End If

                                    ldblAfect = mobjValues.StringToType(.Form.Item("hddAfec"), eFunctions.Values.eTypeData.etdDouble)
                                    ldblExcent = mobjValues.StringToType(.Form.Item("hddExe"), eFunctions.Values.eTypeData.etdDouble)

                                    mobjValues.StringToType(.Form.Item("tcnTotalSurrNeto"), eFunctions.Values.eTypeData.etdDouble)

                                    mobjPolicyTra.nAmount = mobjValues.StringToType(.Form.Item("tcnTotalSurrNeto"), eFunctions.Values.eTypeData.etdDouble)
                                    mobjPolicyTra.nRequestedAmount = mobjValues.StringToType(.Form.Item("tcnTotalSurrNeto_local"), eFunctions.Values.eTypeData.etdDouble)
                                    'mobjPolicyTra.nRequestedAmount = ldblExcent

                                    mstrQueryString = "&sCodisplOri=VI7004" & "&sBenef=" & Session("OP006_sBenef") & "&nConcept=" & llngConcept & "&dEffecdate=" & Session("dEffecdate") & "&nAmount=" & mobjPolicyTra.nAmount & "&nAmountPay=" & mobjPolicyTra.nRequestedAmount & "&nPayOrderTyp=" & llngPayOrderTyp & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dRescdate=" & Session("dEffecdate") & "&sSurrType=" & Session("sSurrType") & "&nSurrAmt=" & mobjPolicyTra.nAmount & "&nCurrency=" & Session("nCurrency") & "&sClient=" & Session("OP006_sBenef") & "&nBranchPay=" & Session("nBranch") & "&nProductPay=" & Session("nProduct") & "&nPolicyPay=" & Session("nPolicy") & "&nCertifPay=" & Session("nCertif") & "&sCertype=" & Session("sCertype") & "&sSurrTot=" & chkSurrTot & "&nCoverCost=" & .Form.Item("hddnCoverCost") & "&nPmtOrd=" & .Form.Item("cbePmtOrd") & "&nSurrReas=" & Session("nSurrReas") & "&nRetention=" & .Form.Item("hddnTotRetention") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&sProcess=" & .Form.Item("hddProcess") & "&nProponum=" & .Form.Item("hddProponum") & "&sClientEnt=" & .Form.Item("dtcClient") & "&nOrigin_apv=" & .Form.Item("valOrigin") & "&nTypesupport=" & llngTypesupport & "&nExcent=" & ldblExcent & "&nAfect=" & ldblAfect & "&nTax_amount=" & .Form.Item("hddnTotRetention") & "&nAmounttotal=" & mobjPolicyTra.nRequestedAmount & "&sInd_Insur=" & Session("sInd_Insur")

                                Else
                                    'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                    mobjPolicyTra = New ePolicy.ValPolicyTra
                                    lblnPost = mobjPolicyTra.InsPostVI7004(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), chkSurrTot, mobjValues.StringToType(.Form.Item("hddnSurrAmou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCoverCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnTotRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePmtOrd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), "1", mobjValues.StringToType(.Form.Item("hddProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("dtcRetirement"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddClientCode"), eRemoteDB.Constants.intNull, Session("sInd_Insur"), mobjValues.StringToType(.Form.Item("hdddPaymentDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddIsCancelling"), mobjValues.StringToType(.Form.Item("cbeTyp_Profitworker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSaapv"), eFunctions.Values.eTypeData.etdDouble))
                                    mobjPolicyTra = Nothing
                                End If
                            End If

                            If CStr(Session("optProcessType")) = "1" Or (CStr(Session("optProcessType")) <> "1" And CDbl(.Form.Item("cbePmtOrd")) <> 1) Then
                                Call insPrintPolicyRep("VI7004")
                            End If
                        Else

                            mstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & Session("nCurrency") & "&sProcess=" & Session("optProcessType") & "&nOffice=" & .Form.Item("hddOffice") & "&nOfficeAgen=" & .Form.Item("hddOfficeAgen") & "&nAgency=" & .Form.Item("hddAgency") & "&sClientBenef=" & .Form.Item("hddClientBenef") & "&nProponum=" & .Form.Item("hddProponum") & "&nSurrReas=" & Session("nSurrReas") & "&sSurrType=" & Session("sSurrType") & "&sClientDest=" & .Form.Item("hddClientDest") & "&sInd_Insur=" & Session("sInd_Insur") & "&nRequestedAmount=" & .Form.Item("tcnRequestedSurrAmt")

                            'UPGRADE_NOTE: The 'ePolicy.Surr_origins' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Surr_origins

                            lblnPost = mobjPolicyTra.InsPostVI7004_Upd("Add", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAvailBal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSurrCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSurrReas"), eFunctions.Values.eTypeData.etdDouble), Session("sSurrType"), mobjValues.StringToType(.Form.Item("tcnRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnWDCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_Profitworker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdPaymentDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCost_cov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntLoans"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTypeResc"), eFunctions.Values.eTypeData.etdDouble))
                            Session("dEffecdate") = .Form.Item("hdddEffecdate")
                            mobjPolicyTra = Nothing
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
                            'UPGRADE_NOTE: The 'ePolicy.Loans' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Loans
                            '+ Si la ejecución es preliminar
                            If CDbl(.Form.Item("optExecute")) = 1 Then
                                '+ Si se desea crear la solicitud del préstamo/anticipo
                                lblnPost = mobjPolicyTra.insPostVI011(.Form.Item("tctCodisplOri"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExecute"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInter_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOperat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("cbePayOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmoTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequest"), eRemoteDB.Constants.intNull, Session("SessionID"), eRemoteDB.Constants.intNull, .Form.Item("tcnSurrVal"), .Form.Item("tcnMaxAmount"), .Form.Item("tcnLoans"))

                                If lblnPost Then
                                    '+ Se indica el nro. de propuesta generado 
                                    If mobjPolicyTra.nCode > 0 Then
                                        'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                    lblnPost = True
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&nOrigin=" & .Form.Item("cbeOrigin") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sProcessType=" & .Form.Item("optProcessType")

                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")

                        If .Form.Item("tcnCertif") = vbNullString Then
                            Session("nCertif") = 0
                        Else
                            Session("nCertif") = .Form.Item("tcnCertif")
                        End If
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&nCurrency=" & .QueryString.Item("nCurrency") & "&nOrigin=" & .QueryString.Item("nOrigin") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&sProcessType=" & .QueryString.Item("sProcessType")

                            'UPGRADE_NOTE: The 'ePolicy.Funds_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Funds_Pol
                            lblnPost = mobjPolicyTra.insPostVI010(.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBuy_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSwi_cost_tot"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sProcessType"))
                        Else
                            If .Form.Item("hddsProcessType") = "1" Then
                                lblnPost = mobjPolicyTra.insPostVI010_A(.Form.Item("hddsSel"), .Form.Item("hddnBranch"), .Form.Item("hddnProduct"), .Form.Item("hddnPolicy"), .Form.Item("hddnCertif"), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                            Call insPrintPolicyRep("VIL010")
                        End If

                    End If



                End With

            '**+VI016: Switches - APV
            '+ VI016: Cambios de fondos de inversión - APV
            Case "VI016"
                With Request
                    lblnPost = True
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&nOrigin=" & .Form.Item("cbeOrigin") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sProcessType=" & .Form.Item("optProcessType")

                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")

                        If .Form.Item("tcnCertif") = vbNullString Then
                            Session("nCertif") = 0
                        Else
                            Session("nCertif") = .Form.Item("tcnCertif")
                        End If
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&nCurrency=" & .QueryString.Item("nCurrency") & "&nOrigin=" & .QueryString.Item("nOrigin") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&sProcessType=" & .QueryString.Item("sProcessType")

                            'UPGRADE_NOTE: The 'ePolicy.Funds_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.Funds_Pol
                            lblnPost = mobjPolicyTra.insPostVI016(.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCodFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSignal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnitsChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBuy_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnSwi_cost_tot"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnValueChange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sProcessType"), mobjValues.StringToType(.Form.Item("cbeTyp_Profitworker"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            If .Form.Item("hddsProcessType") = "1" Then
                                lblnPost = mobjPolicyTra.insPostVI010_A(.Form.Item("hddsSel"), .Form.Item("hddnBranch"), .Form.Item("hddnProduct"), .Form.Item("hddnPolicy"), .Form.Item("hddnCertif"), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                            Call insPrintPolicyRep("VIL010")
                        End If

                    End If



                End With

            Case "VI7002"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = mobjPolicyTra.insPostvi7002_k(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.insPostvi7002upd(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), "1", mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate), "Add")
                        Else
                            lblnPost = mobjPolicyTra.insPostvi7002(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            'Session("nBranch") = .Form.Item("cbeBranch")
                            'Session("nProduct") = .Form.Item("valProduct")
                            'Session("nPolicy") = .Form.Item("tcnPolicy")
                            'Session("nCertif") = .Form.Item("tcnCertif")
                            'Session("sCertype") = "2"
                            'Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            If lblnPost Then
                                Call insPrintPolicyRep("VI7002")
                            End If
                        End If
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

                    If Request.Form.Item("chkGenCobAnt") = "1" Then
                        sReceipt_ind = "2"
                    Else
                        sReceipt_ind = ""
                    End If
                    If CStr(Session("BatchEnabled")) <> "1" Or Request.Form.Item("chkEjecInt") = "1" Then
                        lblnPost = MobjPolicy.insPostCA031(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdRendateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdRenDateto"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), sReceipt_ind)
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

                        'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        lclsBatch_param = New eSchedule.Batch_Param

                        '+La siguiente condicion se incluyó para replicar lo presente en el método insPostCA031 (!!!)
                        '+Si es masiva
                        If CStr(Session("nInfo")) = "1" Then
                            With lclsBatch_param
                                .nBatch = 110
                                .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "") '+Poliza
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "") '+Certificado
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToDate(Request.Form.Item("tcdRendateFrom")))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToDate(Request.Form.Item("tcdRenDateto")))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "0")
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "0")
                                '+Renovacion definitiva
                                If mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, 99)
                                    '+Renovacion preliminar
                                Else
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, 98)
                                End If
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, sReceipt_ind)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("tcdRendateFrom"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("tcdRenDateto"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, "1") '+Masivo
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, lintTypeRepCAL005)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                                .Save()
                            End With
                            '+Si es puntual                    
                        Else
                            MobjPolicy.Find("2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            With lclsBatch_param
                                .nBatch = 110
                                .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                If MobjPolicy.sSimul = "1" Then
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "") '+Certificado
                                Else
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                                End If
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, MobjPolicy.dNextReceip)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "")
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "")
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, "")
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, MobjPolicy.nIndexfac)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, MobjPolicy.nIndexFactMn)
                                '+Renovacion definitiva
                                If mobjValues.StringToType(Session("nRenewal"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, 99)
                                    '+Renovacion preliminar
                                Else
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, 98)
                                End If
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nInfo"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, sReceipt_ind)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("tcdRendateFrom"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("tcdRenDateto"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, "2") '+Puntual
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, lintTypeRepCAL005)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("hddIntermed"), eFunctions.Values.eTypeData.etdDouble))
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
                        If CDbl(.QueryString.Item("nMainAction")) <> 401 Then
                            lblnPost = mobjPolicyTra.insPostCA051_K(.QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, False), .Form.Item("tctDescript"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkList"), Session("nId"))
                        Else
                            lblnPost = True
                        End If
                    Else

                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.insPostCA051(.Form.Item("chkAuxSel"), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnIdRec"), eFunctions.Values.eTypeData.etdDouble), .QueryString("WindowType"), .Form.Item("tctColumnName"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRequire"), Session("nUserCode"), .Form.Item("chkSelected"), .Form.Item("tctDefaultValue"))
                        Else
                            If CDbl(.QueryString.Item("nMainAction")) <> 401 Then
                                For lintCountCA051 = 1 To CInt(.Form.Item("hddnCount"))
                                    lblnPost = mobjPolicyTra.insPostCA051(.Form.GetValues("hddsAuxSelh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(.Form.GetValues("hddnId").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnIdRec").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, False), .QueryString("WindowType"), .Form.GetValues("hddsColumnNameh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(.Form.GetValues("hddnOrderh").GetValue(lintCountCA051 - 1), eFunctions.Values.eTypeData.etdDouble, True), .Form.GetValues("hddsRequireh").GetValue(lintCountCA051 - 1), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.GetValues("hddsSelectedh").GetValue(lintCountCA051 - 1), .Form.GetValues("hddsDefaultValueh").GetValue(lintCountCA051 - 1))
                                Next
                            Else
                                lblnPost = True
                            End If

                            If lblnPost And CStr(Session("sFile")) <> vbNullString Then

                                lobjSheet = New eBatch.Colsheet
                                If lobjSheet.insQueryExportExcel(mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble, True), Session("sFile")) Then

                                End If
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
                    mstrQueryString = mstrQueryString & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nCertif=" & .Form.Item("tcnCertif") & "&nOrigin=" & .Form.Item("valOrigin") & "&nProponum=" & .Form.Item("tcnProponum") & "&sClient=" & .Form.Item("dtcClient") & "&nStatus=" & .Form.Item("cbeStat") & "&nIntermed=" & .Form.Item("valIntermed") & "&nAgency=" & .Form.Item("valAgency") & "&sTypeDoc=" & .Form.Item("optTypeDoc") & "&sExpired=" & .Form.Item("chkDueDate") & "&sApplyCostFP=" & .Form.Item("chkApplyCostFP") & "&dStartdate=" & .Form.Item("tcdEffecdate") & "&sBrancht=" & .Form.Item("valProduct_sBrancht") & "&nWaitCode=" & .Form.Item("cboWaitCode") & "&dLastdate=" & .Form.Item("tcdLastdate") & "&sCodispl_orig=" & .QueryString.Item("sCodispl_orig")

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
                'UPGRADE_NOTE: The 'ePolicy.TConvertions' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TConvertions
                lblnPost = True
                With Request

                    mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nCertif=" & .QueryString.Item("nCertif") & "&nOrigin=" & .QueryString.Item("nOrigin") & "&nProponum=" & .QueryString.Item("nProponum") & "&sClient=" & .QueryString.Item("sClient") & "&nStatus=" & .QueryString.Item("nStat") & "&nIntermed=" & .QueryString.Item("nIntermed") & "&nAgency=" & .QueryString.Item("nAgency") & "&sTypeDoc=" & .QueryString.Item("sTypeDoc") & "&sExpired=" & .QueryString.Item("sExpired") & "&sApplyCostFP=" & .QueryString.Item("sApplyCostFP") & "&dStartdate=" & .QueryString.Item("dStartdate") & "&sBrancht=" & .QueryString.Item("sBrancht") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nOperat=" & .QueryString.Item("nOperat") & "&sCodispl_orig=" & .QueryString.Item("sCodispl_orig")
                    '+ Accion actualizar un registro de la tabla
                    If (.QueryString.Item("nMainAction") = eFunctions.Menues.TypeActions.clngActionUpdate Or CDbl(.QueryString.Item("nMainAction")) = 401) And .QueryString.Item("WindowType") = "PopUp" Then
                        If Request.QueryString.Item("sClickCheck") = "1" Then
                            lblnPost = mobjPolicyTra.insPostCA099("PopUp", "Update", mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPen_doc"), mobjValues.StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dStat_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dLimit_date"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sObserv"), mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nFirst_prem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPrem_curr"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPrem_che"), Request.QueryString.Item("sPay_order"), mobjValues.StringToType(Request.QueryString.Item("nExpenses"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDevolut"), Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWait_Code"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = mobjPolicyTra.insPostCA099(.QueryString("WindowType"), "Update", mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDoc_pend"), mobjValues.StringToType(.Form.Item("tcdStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeStat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdStatdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valNoConvers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdMaximun_da"), eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFirstPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCurrPrem"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPrem_cheq"), "2", mobjValues.StringToType(.Form.Item("tcnCollect"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDevolut"), .Form.Item("hddCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddClient"), mobjValues.StringToType(.Form.Item("hddCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOperat"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(.Form.Item("tcnGastMed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGastProv"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksPenstatus_pol"))
                        End If
                    Else
                        '+ Accion actualizar transaccion  (no popup), a menos que sea consultar
                        If (.QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Or .QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdataAccept) And .QueryString.Item("nOperat") <> "1" Then

                            lblnPost = mobjPolicyTra.insPostCA099("", "Update", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkDoc_pend"), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", .Form.Item("hddScertype_aux"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOperat"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble), Session("skey"))

                            If Session("nOperat") = 2 Then
                                If lblnPost Then

                                    Dim mcolTmpReportMasives As New eCrystalExport.TmpReportMasives
                                    Dim mclsTmpReportMasive As New eCrystalExport.TmpReportMasive
                                    Dim oHelper As New eCrystalExport.Export
                                    Dim mobjDocuments_aux As New eReports.Report
                                    If mcolTmpReportMasives.Find(DirectCast(mobjPolicyTra, TConvertions).sKey) Then
                                        For Each mclsTmpReportMasive In mcolTmpReportMasives
                                            oHelper.sCertype = mclsTmpReportMasive.sCertype
                                            oHelper.nBranch = mclsTmpReportMasive.nBranch
                                            oHelper.nProduct = mclsTmpReportMasive.nProduct
                                            oHelper.nPolicy = mclsTmpReportMasive.nPolicy
                                            oHelper.nCertif = mclsTmpReportMasive.nCertif
                                            oHelper.nMovement = mclsTmpReportMasive.nFolionum
                                            oHelper.nForzaRep = 1
                                            oHelper.nTratypep = 2
                                            oHelper.ReportParameters.Add("2")
                                            oHelper.ReportParameters.Add(mclsTmpReportMasive.nBranch)
                                            oHelper.ReportParameters.Add(mclsTmpReportMasive.nProduct)
                                            oHelper.ReportParameters.Add(mclsTmpReportMasive.nPolicy)
                                            oHelper.ReportParameters.Add(mclsTmpReportMasive.nCertif)
                                            oHelper.ReportParameters.Add(mobjDocuments_aux.setdate(mclsTmpReportMasive.dStartdate))
                                            oHelper.ReportParameters.Add(mclsTmpReportMasive.nFolionum)
                                            oHelper.GenPoliza(32, 2, Session("sInitialsCon"), Session("sAccesswoCon"), , Server.MapPath("/VTIMENET"))

                                        Next

                                    End If



                                    Call insPrintPolicyRep("CA099")
                                End If
                            End If

                            '+ Se retorna cadena con llave de proceso
                            mstrMessage = mobjPolicyTra.sKey

                        End If
                    End If
                End With


            '+CA767: Tratamiento de propuestas especiales
            Case "CA767"
                'UPGRADE_NOTE: The 'ePolicy.Request' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Request
                With Request

                    If CStr(Session("nOperat")) = "2" Or CStr(Session("nOperat")) = "5" Then
                        '+-------------------------------------------------------------------------------------------------------------------------------------------------                   
                        '+Si se trata de la ventana de Rescates y el producto es VNT, o se trata de Switch, se identifica si el prod es APV para invocar distintas ventanas                   
                        '+-------------------------------------------------------------------------------------------------------------------------------------------------
                        If (CStr(Session("nOrigin")) = "8" And (CDbl(.Form.Item("hddProdClass")) = 3 Or CDbl(.Form.Item("hddProdClass")) = 4)) Or CStr(Session("nOrigin")) = "10" Then
                            'UPGRADE_NOTE: The 'eProduct.Product' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            lclsProduct_li = New eProduct.Product
                            With lclsProduct_li
                                If .FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                                    sApv = lclsProduct_li.sApv
                                Else
                                    sApv = "2"
                                End If
                            End With
                            lclsProduct_li = Nothing
                        End If

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
                                    If sApv = "1" Then
                                        mstrCodispl = "VI7004"
                                    Else
                                        mstrCodispl = "VI7000"
                                    End If
                                Else
                                    mstrCodispl = "VI009"
                                End If

                            Case "9"
                                mstrCodispl = "VI011"
                            Case "10"
                                If sApv = "1" Then
                                    mstrCodispl = "VI016"
                                Else
                                    mstrCodispl = "VI010"
                                End If

                        End Select
                        '+ Actualiza número de nota
                        If mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                            If mobjPolicyTra.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                                mobjPolicyTra.nUsercode = Session("nUsercode")
                                mobjPolicyTra.nNotenum = mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble)
                                Call mobjPolicyTra.Update()
                            End If
                        End If
                        lblnPost = True
                        mstrQueryString = "&sCodisplOri=CA767" & "&sDescript=" & Request.Form.Item("tctDescript") & "&nNotenum=" & Request.Form.Item("tcnNotenum") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nPropoNum=" & Session("nPropoNum") & "&dEffecdate=" & Session("dEffecdate") & "&nTypePay=" & .Form.Item("cbeTypepay") & "&sTyp_surr=" & .Form.Item("optTyp_surr") & "&nAgency=" & .Form.Item("hddnAgency") & "&nOperat=" & Session("nOperat") & "&nAmount=" & .Form.Item("tcnAmount") & "&nSurrReas=" & .Form.Item("cbeSurrReas") & "&nOrigin=" & .Form.Item("valOrigin") & "&sInd_Insur=" & .Form.Item("hddInd_Insur")
                    Else
                        mstrCodispl = "CA099"
                        mstrQueryString = "&nProponum=" & Session("nPolicy")
                        lblnPost = mobjPolicyTra.insPostCA767("CA767", mobjValues.StringToType(Session("Action_CA099"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optTyp_surr"), .Form.Item("cboPayorder"), .Form.Item("chkNull_Rec"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboNullcode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optTyp_rec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkReh_lrec"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboStatquota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboNo_convers"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+VIL733: Aniversario de coberturas (Productos de Vida)
            Case "VIL733"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.ValPolicyTra
                    With Request
                        lblnPost = mobjPolicyTra.insPostVIL733_k("VIL733", Request.Form.Item("sOptExecute"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                    If lblnPost Then
                        Call insPrintPolicyRep("VIL733")
                    End If
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 119
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Request.Form.Item("sOptExecute"))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("tcdEffecdate"))
                        .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Request.Form.Item("sOptExecute"))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    lblnPost = True
                End If

            '+CAL006: Reservas de Primas
            Case "CAL006"

                With Request
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                    'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.Certificat

                    sKeyVI008 = "TMP" & Session("SessionID") & Session("nUsercode")

                    If .Form.Item("hddCodisplOri") = "CA767" Then
                        sCodisplOri = .Form.Item("hddCodisplOri")
                    Else
                        sCodisplOri = "VI008"
                    End If

                    lblnPost = mobjPolicyTra.insPostVI008(sCodisplOri, mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExeMode"), .Form.Item("optReduction"), .Form.Item("chkNulling"), .Form.Item("chkGenProposal"), mobjValues.StringToType(.Form.Item("hddOperat"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "", mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), sKeyVI008)

                    If lblnPost Then
                        '+ Se indica el nro. de propuesta generado 
                        If mobjPolicyTra.nCode > 0 Then
                            'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                            'UPGRADE_NOTE: The 'ePolicy.TMovprev_Capital' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.TMovprev_Capital
                            lblnPost = mobjPolicyTra.InsPostVI806("VI806", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("SessionID"), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))

                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            Session("nCertif") = .Form.Item("tcnCertif")
                            Session("nPolicy") = .Form.Item("tcnPolicy")
                        Else
                            'UPGRADE_NOTE: The 'ePolicy.TMovprev_Capital' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 115
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        Session("sKey") = .sKey
                        If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                            'UPGRADE_NOTE: The 'ePolicy.TMovprev_Capital' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            mobjPolicyTra = New ePolicy.TMovprev_Capital
                            lblnPost = mobjPolicyTra.InsPostVI806("VI806", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            Session("nBranch") = Request.Form.Item("cbeBranch")
                            Session("nProduct") = Request.Form.Item("valProduct")
                            Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                            Session("nCertif") = Request.Form.Item("tcnCertif")
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                        Else
                            If Request.Form.Item("hddFindData") = "1" Then
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sKey"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .nUsercode)
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
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.Policy
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nTransaction") = 61
                        lblnPost = True
                    Else
                        lblnPost = mobjPolicyTra.InsPostCA642("CA642", Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(.Form.Item("tcdNewChangdat"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnNewpayfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNewNextreceip"), eFunctions.Values.eTypeData.etdDate))

                        'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        lclsGeneral = New eGeneral.GeneralFunction
                        If mobjPolicyTra.nNumError <> 0 Then
                            lstrMessage = lclsGeneral.insLoadMessage(mobjPolicyTra.nNumError) & " " & "para la frecuencia de pago indicada, no puede realizar el cambio."
                            Response.Write("<SCRIPT>alert(""Men. " & CStr(mobjPolicyTra.nNumError) & ": " & lstrMessage & """);</" & "Script>")
                        End If
                        lclsGeneral = Nothing
                    End If
                End With

            '+ VA650: Movimientos al valor póliza
            Case "VA650"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.Account_Pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mobjPolicyTra = New ePolicy.Account_Pol
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nTypemove=" & .Form.Item("optMovType") & "&sReload=" & "No"
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.InsPostVA650Upd(.QueryString("sKey"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True))
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nTypemove=" & .QueryString.Item("nTypemove") & "&sKey=" & .QueryString.Item("sKey") & "&sReload=" & "Yes"

                        Else
                            lblnPost = mobjPolicyTra.InsPostVA650("2", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nTypemove"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctKey"), Session("nUsercode"))

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
                            'UPGRADE_NOTE: The 'ePolicy.Per_deposit' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
                'UPGRADE_NOTE: The 'ePolicy.TDetail_pre' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.TDetail_pre
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mstrQueryString = "&nCodeItem=" & mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble)
                        lblnPost = mobjPolicyTra.insPostCA028Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), 2, 2, "1")
                    End If
                End With
            '+ CA0789: Autorización de propuestas sin pago de primera prima
            Case "CA789"
                'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                lblnPost = mobjPolicyTra.inspostCA789("1", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, Session("nUsercode"))
            '+ CA900: Autorización de propuestas sin pago de primera prima
            Case "CA900"
                'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Policy

                lblnPost = mobjPolicyTra.insPostCA900(mobjValues.StringToType(Request.Form.Item("tcnBordereaux"), eFunctions.Values.eTypeData.etdLong, False), Request.Form.Item("tctClient"), Request.Form.Item("tctClient_des"), Session("nUsercode"))

            '+ CAL963: Ajuste por endoso retroactivo
            Case "CAL963"
                lblnPost = True

            '+ MVI8017: Registro de renovaciones de un ahorro garantizado
            Case "MVI8017"
                'UPGRADE_NOTE: The 'ePolicy.Renewal_guaran_val' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.Renewal_guaran_val
                lblnPost = True
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&sCertype=" & .Form.Item("hddsCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.InsPostMVI8017(.QueryString("Action"), .QueryString("sCertype"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valGuarsav_year"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdIniperiod"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndperiod"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCurrentamount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNewamount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCurrentprem"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNewprem"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkFunds"), .Form.Item("chkReceipt"))
                            mstrQueryString = "&sCertype=" & .QueryString.Item("sCertype") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
                        End If
                    End If
                End With

            '+ CAL815: Rehabilitación/Reactivación masiva de pólizas
            Case "CAL815"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request



                    lblnPost = mobjPolicyTra.insPostCAL815(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNullDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), .Form.Item("chkNullDevRec"), .Form.Item("chkNullReceipt"), mobjValues.StringToType(.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nDay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeagen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdInteger))

                    Session("sKey") = mobjPolicyTra.sKey

                    '+ Llamada al procedimiento que invoca al reporte
                    If .Form.Item("chkRescReport") = "1" Then
                        Call insPrintPolicyRep("CAL815")
                    End If

                End With

            '+ CAL978: Reactivación de pólizas
            Case "CAL978"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New ePolicy.ValPolicyTra
                With Request

                    'rESPONSE.Write "*" mobjValues.StringToType(.Form("optExecute"), eFunctions.Values.eTypeData.etdDouble,True)
                    'RESPONSE.END

                    lblnPost = mobjPolicyTra.insPostCAL978(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeagen"), eFunctions.Values.eTypeData.etdDouble))

                    Session("sKey") = Trim(mobjPolicyTra.sKey)

                    '+ Llamada al procedimiento que invoca al reporte
                    Call insPrintPolicyRep("CAL978")
                End With

            Case "CA088"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    lblnPost = True
                    Session("sCertype") = "2"
                    Session("nBranch") = Request.Form.Item("cbeBranch")
                    Session("nProduct") = Request.Form.Item("valProduct")
                    Session("nPolicy") = Request.Form.Item("tcnPolicy")
                    Session("nCertif") = Request.Form.Item("tcnCertif")
                    Session("dDate_Origi") = Request.Form.Item("tcdDate_origin")
                Else
                    With Request
                        lblnPost = mobjPolicyTra.insPostCA088(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdRecepInt"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInt_Comp"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInsu"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdRecepInsu_Comp"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                    End With
                End If

            '+ MVI8015: DESCUENTOS PORCENTUALES POR VALOR PÓLIZA
            Case "MVI8015"
                'UPGRADE_NOTE: The 'eProduct.Perc_DiscVP' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New eProduct.Perc_DiscVP
                lblnPost = True
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.InsPostMVI8015(.QueryString("Action"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnvp_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnvp_end"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcndisc_perc_vp"), eFunctions.Values.eTypeData.etdDouble))

                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
                        End If
                    End If
                End With
            '+VI818: Reverso de movimientos en cuenta
            Case "VI818"
                'UPGRADE_NOTE: The 'eBatch.tmp_undo_move_acc' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New eBatch.Tmp_undo_Move_Acc
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("nPolicy") = .Form.Item("tcnPolicy")
                        Session("nCertif") = .Form.Item("tcnCertif")
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        Session("nOperat") = .Form.Item("optExecute")
                        Session("nType_move") = .Form.Item("hddnType_move")
                        lblnPost = mobjPolicyTra.inspostVI818_K(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            If mobjValues.StringToType(.Form("tctType_move"), eFunctions.Values.eTypeData.etdDouble) = 802 Then
                                ncreditmanualAux = mobjValues.StringToType(.Form("tctDebit"), eFunctions.Values.eTypeData.etdDouble)
                                ndebitmanualAux = mobjValues.StringToType(.Form("tctCredit"), eFunctions.Values.eTypeData.etdDouble)
                            Else
                                ncreditmanualAux = mobjValues.StringToType(.Form("tctCredit"), eFunctions.Values.eTypeData.etdDouble)
                                ndebitmanualAux = mobjValues.StringToType(.Form("tctDebit"), eFunctions.Values.eTypeData.etdDouble)
                            End If
                            lblnPost = mobjPolicyTra.inspostVI818upd(.QueryString("Action"),
                                                                     mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate),
                                                                     mobjValues.StringToType(.Form.Item("hddidconsec"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                     mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(ncreditmanualAux, eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(ndebitmanualAux, eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form.Item("tcdoperdatemanual"), eFunctions.Values.eTypeData.etdDate),
                                                                     mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form.Item("tctType_move"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form("tctProfitworker"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form("cbeoperdatetype"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form("cbeoperdatemanualtype"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form("tcdoperdate_new"), eFunctions.Values.eTypeData.etdDate))
                        Else
                            lblnPost = mobjPolicyTra.inspostVI818(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            '+VI820: Ajuste de movimientos en cuenta Definitivo
            Case "VI820"
                'UPGRADE_NOTE: The 'eBatch.tmp_undo_move_acc' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjPolicyTra = New eBatch.Tmp_undo_Move_Acc
                With Request
                    lblnPost = mobjPolicyTra.insPostVI820(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CA980: Folios asignados a la compañía
            Case "CA980"
                With Request
                    mobjPolicyTra = New ePolicy.Folios_comp
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicyTra.insPostCA980(.QueryString("Action"),
                                                                    mobjValues.StringToType(.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnStart"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnEnd"), eFunctions.Values.eTypeData.etdDouble),
                                                                    .Form("cbeStatregt"),
                                                                    mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With

            '+CA985: Asignación de folios por intermediario
            Case "CA985"
                mobjPolicyTra = New ePolicy.Folios_Agent
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then

                        Session("nBranch_CA985") = .Form.Item("cbeBranch")
                        Session("nProduct_CA985") = .Form.Item("valProduct")
                        Session("nIntermed_CA985") = .Form.Item("valIntermed")
                        Session("dAssign_date_CA985") = .Form.Item("tcdAssign_date")

                        lblnPost = True

                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.insPostCA985Upd(.QueryString("Action"),
                                                                    mobjValues.StringToType(Session("nBranch_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Session("nProduct_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Session("nIntermed_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Session("dAssign_date_CA985"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form("tcnStart"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnEnd"), eFunctions.Values.eTypeData.etdDouble),
                                                                    .Form("cbePolitype"),
                                                                    .Form("cbeStatregt"),
                                                                    mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnStartPolNumber"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form("tcnEndPolNumber"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = mobjPolicyTra.insPostCA985(mobjValues.StringToType(Session("nBranch_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                                    mobjValues.StringToType(Session("nProduct_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                                    mobjValues.StringToType(Session("nIntermed_CA985"), eFunctions.Values.eTypeData.etdDouble),
                                                                                      mobjValues.StringToType(Session("dAssign_date_CA985"), eFunctions.Values.eTypeData.etdDate),
                                                                                      mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With

            Case "CA986"
                mobjPolicyTra = New ePolicy.Soap_Sell_Period
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nVehType_CA986") = .Form.Item("cbeTypeVeh")
                        lblnPost = True
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjPolicyTra.insPostCA986Upd(.QueryString("Action"),
                                                                    mobjValues.StringToType(Session("nVehType_CA986"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("tcdStartPeriod"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("tcdExpiredPeriod"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("tcdStartDatePol"), eFunctions.Values.eTypeData.etdDate),
                                                                    mobjValues.StringToType(.Form.Item("tcdExpiredDatePol"), eFunctions.Values.eTypeData.etdDate),
                                                                    .Form.Item("chkStatus"),
                                                                    mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                                     mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '+SO001: Digitación de Pólizas SOAP
            Case "SO001"
                With Request
                    Dim mobjPolicyTra = New ePolicy.Soap_entry

                    lblnPost = mobjPolicyTra.insPostSO001(.Form("tctRegist"),
                                     mobjValues.StringToType(.Form("ValVehMark"), eFunctions.Values.eTypeData.etdDouble),
                                     .Form("ValVehModel"),
                                     IIf(.Form("ValVehMark") = "9999", .Form("tctMark"), ""),
                                     IIf(.Form("ValVehMark") = "9999", .Form("tctModel"), ""),
                                     mobjValues.StringToType(.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble),
                                    .Form("tctMotor"),
                                    .Form("tctChassis"),
                                    .Form("tctColor"),
                                     IIf(.Form("valCausal") = eRemoteDB.strNull, 0, .Form("valCausal")),
                                    .Form("dtcClient"),
                                    .Form("dtcClient_Digit"),
                                    .Form("tctNames"),
                                    .Form("tctFatherLastName"),
                                    .Form("tctMotherLastName"),
                                    mobjValues.StringToType(.Form("dtcBirthdayDate"), eFunctions.Values.eTypeData.etdDate),
                                    .Form("tctAddress"),
                                    mobjValues.StringToType(.Form("cbeProvince"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("valLocal"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("valMunicipality"), eFunctions.Values.eTypeData.etdDouble),
                                    .Form("tctPhone"),
                                    mobjValues.StringToType(.Form("tcnFolio"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToType(.Form("valIntermed"), eFunctions.Values.eTypeData.etdInteger),
                                    mobjValues.StringToType(.Form("tcdStartDate"), eFunctions.Values.eTypeData.etdDate),
                                    mobjValues.StringToType(.Form("tcdExpirDate"), eFunctions.Values.eTypeData.etdDate),
                                    mobjValues.StringToType(.Form("cbeModule"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("tcnCollectedPremium"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger),
                                    .Form("tctMistakenDigit"),
                                    .Form("tctDigitalLink"),
                                    mobjValues.StringToType(.Form("valAgreement"), eFunctions.Values.eTypeData.etdDouble),
                                    mobjValues.StringToType(.Form("tctType"), eFunctions.Values.eTypeData.etdInteger),
                                    .Form("tctDigit"), .Form("cbeLicense_ty"), .Form.Item("chkAcchsend_ind"))


                    If lblnPost Then
                        If String.IsNullOrEmpty(mobjPolicyTra.Validations) Then
                            If Request.Form("valCausal") = "13" Then
                                Response.Write("<SCRIPT>alert(""Men. 7815: Póliza anulada"");</" & "Script>")
                            Else
                                'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                Response.Write("<SCRIPT>alert(""Men. 7815: " & New eGeneral.GeneralFunction().insLoadMessage(7815) & """);</" & "Script>")
                            End If
                        Else
                            Response.Write("<SCRIPT>alert(""Men. " & mobjPolicyTra.Validations & ": " & New eGeneral.GeneralFunction().insLoadMessage(mobjPolicyTra.Validations) & """);</" & "Script>")
                            lblnPost = False
                        End If
                    End If
                End With

            Case "SO002"
                mobjPolicyTra = New ePolicy.Folios_Agent
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        Session("nIntermedSource") = .Form.Item("valIntermedSource")
                        Session("nFolioI") = .Form.Item("tcnFolioI")
                        Session("nFolioE") = .Form.Item("tcnFolioE")
                        Session("nIntermedDest") = .Form.Item("valIntermedDest")
                        lblnPost = True
                    Else

                        lblnPost = mobjPolicyTra.PostSO002(Session("nIntermedSource"),
                             Session("nFolioI"),
                             Session("nFolioE"),
                             Session("nIntermedDest"),
                             Session("nUsercode"))
                    End If
                End With
            '+vi7502: Control SAAPV
            Case "VI7502"
                With Request

                    lblnPost = True
                    If .QueryString("nZone") = 1 Then

                        mstrQueryString = "&sCertype=" & .Form("cbeCertype") & "&nBranch=" & .Form("cbeBranch") & "&nProduct=" & .Form("valProduct") & "&nPolicy=" & .Form("tcnPolicy") & "&sClient=" & .Form("tctClient") & "&nCod_saapv=" & .Form("tcnCod_saapv") & "&nInstitution=" & .Form("valInstitution")

                    Else
                        If .QueryString("WindowType") <> "PopUp" Then
                            lblnPost = True
                        Else

                            mobjPolicyTra = New eSaapv.Saapv_pol
                            lblnPost = mobjPolicyTra.INSPOSTVI7502(.Form("hdsCertype"), mobjValues.StringToType(.Form("hdnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hdnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hdnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hdnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbestatus_saapv"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .Form("chkAutodif"), mobjValues.StringToType(.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdLong))

                            mstrQueryString = "&sCertype=" & .Form("hdsCertype_cond") & "&nBranch=" & .Form("hdnBranch_cond") & "&nProduct=" & .Form("hdnProduct_cond") & "&nPolicy=" & .Form("hdnPolicy_cond") & "&sClient=" & .Form("hdClient_cond") & "&nCod_saapv=" & .Form("hdCod_saapv_cond") & "&nInstitution=" & .Form("hdInstitution_cond")


                            'UPGRADE_NOTE: Object mobjPolicyTra may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                            mobjPolicyTra = Nothing
                        End If
                    End If
                End With
            Case "VI017", "VI017-2"
                With Request
                    lblnPost = True
                    mobjBatch = New eBatch.tmp_switch
                    If .QueryString("nZone") = 1 Then
                        lblnPost = mobjBatch.insPrevi017(mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                          mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble),
                                                          mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                          .Form("chkTransferAll"),
                                                          .QueryString("sCodispl"),
                                                          .Form("chkByAccount"))

                        Session("nBranch") = .Form("cbeBranch")
                        Session("nProduct") = .Form("valProduct")
                        Session("nPolicy") = .Form("tcnPolicy")
                        Session("nCertif") = .Form("tcnCertif")
                        Session("sCertype") = "2"
                        Session("dEffecdate") = .Form("tcdEffecdate")

                        mstrQueryString = "&sChkByAccount=" & .Form("chkByAccount") & "&sChkTransferAll=" & .Form("chkTransferAll")

                        If lblnPost Then
                            Session("sKey") = mobjBatch.sKey
                        Else
                            Session("sKey") = ""
                        End If

                    Else
                        If .Form("chkProponum") = "1" Then
                            Dim sChkAll As String
                            sChkAll = vbNullString
                            If .Form("hddChkByAccount") = vbNullString Then
                                sChkAll = "1"
                            End If
                            lblnPost = mobjBatch.insPostvi017(Session("sKey"), sChkAll)
                            Session("dBeginDate") = Session("dEffecdate")
                            Session("dEndDate") = Session("dEffecdate")
                            Call insPrintPolicyRep("VI017")
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '+CA080, CA080A: Emisión de recibo manual
            Case "CA080", "CA080A"
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
                        'UPGRADE_NOTE: The 'ePolicy.TDetail_pre' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mobjPolicyTra = New ePolicy.TDetail_pre
                        mstrCodispl = "CA080"

                        If .QueryString.Item("WindowType") = "PopUp" Then
                            'mstrQueryString = "&sCodisplOri=CA080" & "&nConcept=24" & "&dEffecdate=" & .Form.Item("tcdStartDateR") & "&nOfficepay=" & .Form.Item("hddnOffice") & "&nAmount=" & .Form.Item("hddAmountTot") & "&nCurrencypay=1" & "&nAmountPay=" & .Form.Item("hddAmountTotPay") & "&nPayOrderTyp=2" & "&sCertype=2" & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nCurrency=" & .Form.Item("cbeCurrency") & "&sClient=" & .Form.Item("hddClient_policy") & "&sBenef=" & .Form.Item("hddClient_policy") & "&nBranchPay=" & .Form.Item("cbeBranchPay") & "&nProductPay=" & .Form.Item("valProductPay") & "&nPolicyPay=" & .Form.Item("tcnPolicyPay") & "&nCertifPay=" & .Form.Item("tcnCertifPay") & "&nBalance=" & "" & "&nOperat=" & "" & "&sAnulReceipt=" & "" & "&sReport=" & "" & "&nOffice=" & "" & "&nOfficeAgen=" & "" & "&nAgency=" & "" & "&nReceipt=" & .Form.Item("tcnReceipt") & "&dExpirDat=" & .Form.Item("tcdExpirDateR") & "&nSource=" & .Form.Item("cbeSource") & "&nTypeReceipt=" & .Form.Item("optType") & "&sOrigReceipt=" & .Form.Item("tctOrigReceipt") & "&sKey=" & .Form.Item("hddKey") & "&sAdjust=" & .Form.Item("chkAdjust") & "&nAdjReceipt=" & .Form.Item("tcnAdjReceipt") & "&nAdjAmount=" & .Form.Item("tcnAdjAmount") & "&nTypePay=" & .Form.Item("cbePayWay") & "&nOrigin_apv=" & .Form.Item("valOrigin")
                            'lblnPost = mobjPolicyTra.insPostCA080Upd(.QueryString("sCodispl"), .QueryString("sKey"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("hddIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                            'INICIO DMendoza 14/07/2021
                            'lblnPost = mobjPolicyTra.insPostCA080Upd(.QueryString("sCodispl"), .QueryString("sKey"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                            lblnPost = mobjPolicyTra.insPostCA080Upd(.QueryString("sCodispl"), Session("sKey"), .QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCodeItem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommi_rate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommission"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremiumE"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddAddsuini"), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId_Bill"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), .Form.Item("hddAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.Form.Item("cbePrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("hddPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("hddPrem_det_proc"))
                            If lblnPost Then
                                mstrQueryString = mstrQueryString + "&sNewData=2"
                            End If

                            'FIN DMendoza 14/07/2021
                        Else
                            If Request.QueryString.Item("sCodispl") = "CA080A" Then
                                lstrClient = .Form.Item("hddClient_policy")
                            Else
                                lstrClient = .Form.Item("tctClient")
                            End If

                            If .Form.Item("hddOnSeq") = "1" Or .QueryString.Item("sCodisplOrig") = vbNullString Then
                                Session("OptExecute") = "2"
                            End If

                            lblnPost = mobjPolicyTra.insPostCA080(Session("sCertype"),
                                                                  Session("nBranch"),
                                                                  Session("nProduct"),
                                                                  Session("nPolicy"),
                                                                  Session("nCertif"),
                                                                  mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate),
                                                                  Session("sPoliType"),
                                                                  mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble),
                                                                  lstrClient,
                                                                  mobjValues.StringToType(.Form.Item("tcdExpirDateR"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("tcnReceipt_Collec"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("tcdIssueDate"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(.Form.Item("tcdStartDateR"), eFunctions.Values.eTypeData.etdDate),
                                                                  mobjValues.StringToType(.Form.Item("hddProvince"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble),
                                                                  .Form.Item("tctOrigReceipt"),
                                                                  Session("SessionID"),
                                                                  Session("nUsercode"),
                                                                  Session("OptExecute"),
                                                                  .Form.Item("chkDelReceipt"),
                                                                  "2",
                                                                  .Form.Item("hddOnSeq"),
                                                                  Request.Form.Item("chkDevReceipt"),
                                                                  mobjValues.StringToType(.Form.Item("tcnProceedingNum"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("tcnContrat"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(.Form.Item("tcnCoupon"), eFunctions.Values.eTypeData.etdDouble),
                                                                    Session("sKey"))

                            If lblnPost And Request.Form.Item("chkDelReceipt") <> "1" And
           mobjValues.StringToType(.Form.Item("tcnReceipt"), eFunctions.Values.eTypeData.etdDouble, 0) = eRemoteDB.Constants.intNull Then

                                '+ Se envia alerta con número de recibo generado solo si la ejecución es definitiva 
                                If Session("OptExecute") <> "1" Then
                                    Dim lclsGeneralCa080 As eGeneral.GeneralFunction
                                    Dim lstrMessageCa080 As String

                                    Response.Write("<SCRIPT>opener.top.frames['fraHeader'].document.A392.disabled=true;</" & "SCRIPT>")

                                    Session("sKey") = Nothing
                                    lclsGeneralCa080 = New eGeneral.GeneralFunction
                                    If mobjPolicyTra.nReceipt <> eRemoteDB.Constants.intNull Then
                                        lstrMessageCa080 = lclsGeneralCa080.insLoadMessage(5064) & " con Nro.: " & mobjPolicyTra.nReceipt
                                        Response.Write("<Script>alert(""Men. 5064: " & lstrMessageCa080 & """);</" & "Script>")
                                        Session("nReceiptAux") = mobjPolicyTra.nReceipt
                                    Else
                                        lstrMessageCa080 = lclsGeneralCa080.insLoadMessage(94599) & " con Nro.: " & mobjPolicyTra.sOut_moveme
                                        Response.Write("<Script>alert(""Men. 94599: " & lstrMessageCa080 & """);</" & "Script>")
                                        Session("nReceiptAux") = mobjPolicyTra.sOut_moveme
                                    End If
                                    lclsGeneralCa080 = Nothing
                                End If

                                '+ Se genera el reporte 
                                Session("sKey") = mobjPolicyTra.sKey
                                If Request.QueryString.Item("sExeReport") = "1" Then
                                    insPrintPolicyRep("CAL033")
                                End If
                            End If

                            If Request.QueryString.Item("sCodispl") = "CA080A" Then
                                Dim lclspolicy_Win
                                lclspolicy_Win = New ePolicy.Policy_Win

                                If Request.Form.Item("chkDelReceipt") <> "1" Then
                                    Call lclspolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"),
                                                                      Session("nProduct"), Session("nPolicy"),
                                                                      Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                      Session("nUsercode"), "CA080A", "2")
                                Else
                                    Call lclspolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"),
                                                                  Session("nProduct"), Session("nPolicy"),
                                                                  Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                                  Session("nUsercode"), "CA080A", "1")
                                End If
                                lclspolicy_Win = Nothing
                            End If

                            'Call insFinishTransactionReceipt(Session("nReceiptAux"), "CA080", mobjValues.StringToType(.Form("optType"), eFunctions.Values.eTypeData.etdDouble))

                        End If
                    End If
                End With

        End Select
        insPostPolicyTra = lblnPost
    End Function

    '% insPrintCollectionRep: Se encarga de generar el reporte correspondiente.
    '--------------------------------------------------------------------------------------------
    Private Sub insPrintPolicyRep(ByRef Codispl As Object)
        ''Dim eRemoteDB.Constants.intNull As Object
        Dim ProcessType As Object
        '--------------------------------------------------------------------------------------------
        Dim mobjDocuments As Object
        Dim lobjPolicy_His As Object

        'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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

                    .setStorProcParam(1, sKey)
                    .setStorProcParam(2, lintTypeRepCAL005)
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))


                    If Codispl = "CA031" Then
                        .setStorProcParam(4, 2) '+Puntual
                    Else
                        .setStorProcParam(4, 1) '+Masivo
                    End If

                    '.setParamField 2,"dStartDate", Request.Form("tcdRendateFrom")
                    '.setParamField 3,"dEndDate", Request.Form("tcdRenDateto")

                    '.setParamField 3,"SKEY", sKey
                    '.setParamField 2,"NTITLETYPE", lintTypeRepCAL005
                    '.setParamField 1,"VALINTERMEDIA", mobjValues.StringToType(Request.Form("valIntermedia"), eFunctions.Values.eTypeData.etdDouble)


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
                    lobjPolicy_His = New ePolicy.Policy
                    lobjPolicy_His.Find(Request.Form.Item("tctCertype"), Request.Form.Item("cbeBranch"), Request.Form.Item("valProduct"), Request.Form.Item("tcnPolicy"), True)

                    .Merge = False
                    .nGenPolicy = 1
                    .nMovement = lobjPolicy_His.nMov_histor
                    .nForzaRep = 1
                    .nTratypep = 2
                    .nCopyPolicy = 1
                    .MergeCertype = Request.Form.Item("tctCertype")
                    .MergeBranch = Request.Form.Item("cbeBranch")
                    .MergeProduct = Request.Form.Item("valProduct")
                    .MergePolicy = Request.Form.Item("tcnPolicy")
                    .MergeCertif = Request.Form.Item("tcnCertif")
                    lobjPolicy_His = Nothing
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

                        lobjPolicy_His = New ePolicy.Policy
                        lobjPolicy_His.Find("2", Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nPolicy"), True)

                        .Merge = False
                        .nGenPolicy = 1
                        .nMovement = lobjPolicy_His.nMov_histor
                        .nForzaRep = 1
                        .nTratypep = 2
                        .nCopyPolicy = 1
                        .MergeCertype = "2"
                        .MergeBranch = Request.QueryString.Item("nBranch")
                        .MergeProduct = Request.QueryString.Item("nProduct")
                        .MergePolicy = Request.QueryString.Item("nPolicy")
                        .MergeCertif = Request.QueryString.Item("nCertif")
                        lobjPolicy_His = Nothing

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
                        lobjPolicy_His = New ePolicy.Policy
                        lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

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
                        lobjPolicy_His = Nothing
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
                lobjPolicy_His = New ePolicy.Policy
                lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

                mobjDocuments.Merge = False
                mobjDocuments.nGenPolicy = 1
                mobjDocuments.nMovement = lobjPolicy_His.nMov_histor
                mobjDocuments.nForzaRep = 1
                mobjDocuments.nTratypep = 2
                mobjDocuments.nCopyPolicy = 1
                mobjDocuments.MergeCertype = "2"
                mobjDocuments.MergeBranch = Session("nBranch")
                mobjDocuments.MergeProduct = Session("nProduct")
                mobjDocuments.MergePolicy = Session("nPolicy")
                mobjDocuments.MergeCertif = Session("nCertif")
                lobjPolicy_His = Nothing

                Response.Write((mobjDocuments.Command))

            '+ CAL034A: Propuesta de Rehabilitación de Póliza/Certificado
            Case "CAL034A"
                With mobjDocuments
                    .ReportFilename = "CA034A.rpt"
                    .sCodispl = "CAL034"
                    .setStorProcParam(1, "8")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, nProposal)
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Request.Form.Item("tcdNullDate")))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))


                    Response.Write((.Command))
                End With

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

                'UPGRADE_NOTE: The 'ePolicy.Policy_his' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
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
            '+ JESV: Nuevo formato de reporte cambiado el 21/04/2009
            Case "VIL009"
                With mobjDocuments
                    .ReportFilename = "VI009.rpt"
                    .sCodispl = "VI009"
                    '+ Se verifica si se ha generado propuesta, en cuyo caso se pasa el sCertype correspondiente
                    '+ a propuestas especiales
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Request.Form.Item("hdddEffecdate")))
                    .setStorProcParam(7, .sCodispl)
                    .setStorProcParam(8, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(9, Request.Form.Item("hddsProcessType"))
                    Response.Write((.Command))
                    .Reset()
                    .ReportFilename = "VI7000_PolicyValue.rpt"
                    .sCodispl = "VI009"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("hddnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("hddnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Request.Form.Item("hdddEffecdate")))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))
                End With

            '+ VIL010: Reporte de solicitud de switch
            Case "VIL010"

                With mobjDocuments
                    .ReportFilename = "VIL010.rpt"
                    .sCodispl = "VI010"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, 0) 'mobjValues.StringToType(Session("nCertif"),eFunctions.Values.eTypeData.etdDouble,True)
                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nPolicy_prop"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))
                End With


            Case "VI7000"
                With mobjDocuments
                    lobjPolicy_His = New ePolicy.Policy
                    lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

                    .ReportFilename = "VI7000_1.rpt"
                    .sCodispl = "VI7000"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(8, Request.Form.Item("hddsProcessType"))
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
                    '+Este reporte sale si se trata de un rescate definitivo
                    If Session("optProcessType") = "2" Then
                        .ReportFilename = "VI7000_PolicyValue.rpt"
                        .sCodispl = "VI7000"
                        .setStorProcParam(1, "2")
                        .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(6, .setdate(Session("dEffecdate")))
                        .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setParamField(1, "nproponum", llngProposal)
                        .setParamField(2, "sprocess", Session("optProcessType"))
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
                    End If
                End With
            Case "VI7002"
                With mobjDocuments
                    lobjPolicy_His = New ePolicy.Policy
                    lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)
                    .ReportFilename = "VIL7002_N.rpt"
                    .sCodispl = "VI7002"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
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
            '+ VI7004: Impresión de rescate de póliza/certificado Vida no tradicional
            '+ JESV: Formato nuevo cambiado el 22/04/2009
            Case "VI7004"
                With mobjDocuments
                    lobjPolicy_His = New ePolicy.Policy
                    lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

                    .ReportFilename = "VI7004_1.rpt"
                    .sCodispl = "VI7004"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(8, Request.Form.Item("hddsProcessType"))
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
                    '+Este reporte sale si se trata de un rescate definitivo
                    If Session("optProcessType") = "2" Then
                        .ReportFilename = "VI7000_PolicyValue.rpt"
                        .sCodispl = "VI7004"
                        .setStorProcParam(1, "2")
                        .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(5, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setStorProcParam(6, .setdate(Session("dEffecdate")))
                        .setStorProcParam(7, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                        .setParamField(1, "nproponum", llngProposal)
                        .setParamField(2, "sprocess", Session("optProcessType"))
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
                    End If
                End With

            '+ CAL815: Rehabilitación/Reactivación masiva de pólizas
            Case "CAL815"
                With mobjDocuments
                    .ReportFilename = "CAL815.rpt"
                    .sCodispl = "CAL815"
                    .setStorProcParam(1, Session("sKey"))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeOfficeagen"), eFunctions.Values.eTypeData.etdDouble))

                End With
                Response.Write((mobjDocuments.Command))

            '+ CAL815: Rehabilitación/Reactivación masiva de pólizas
            Case "CAL978"
                With mobjDocuments
                    .ReportFilename = "CAL978.rpt"
                    .sCodispl = "CAL978"
                    .setStorProcParam(1, Session("sKey"))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("cbeOfficeagen"), eFunctions.Values.eTypeData.etdDouble))

                End With
                Response.Write((mobjDocuments.Command))
            Case "CA099"

                With mobjDocuments
                    .bTimeOut = True
                    .nTimeOut = 4000
                    .ReportFilename = "CAL001_F1.rpt"
                    .sCodispl = "CAL001"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, Session("nBranch"))
                    .setStorProcParam(3, Session("nProduct"))
                    .setStorProcParam(4, Session("nProponum"))
                    .setStorProcParam(5, Session("nCertif"))
                    .setStorProcParam(6, Request.Form.Item("hddStartDate"))
                    'Response.Write((.Command))
                    'Este reporte no se encuentra, no existe

                End With

            Case "VIL009_1"
                With mobjDocuments
                    .ReportFilename = "VI009.rpt"
                    .sCodispl = "VI009"
                    '+ Se verifica si se ha generado propuesta, en cuyo caso se pasa el sCertype correspondiente
                    '+ a propuestas especiales
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, 0) 'mobjValues.StringToType(Session("nCertif"),eFunctions.Values.eTypeData.etdDouble,True)
                    .setStorProcParam(6, .setdate(Session("dEffecdate")))
                    .setStorProcParam(7, "VI7000") '.sCodispl
                    .setStorProcParam(8, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(9, Request.Form.Item("hddsProcessType"))
                    lobjPolicy_His = New ePolicy.Policy
                    lobjPolicy_His.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

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
            Case "VI017"
                With mobjDocuments
                    .ReportFilename = "VIL017.rpt"
                    .sCodispl = "VIL017"
                    .setStorProcParam(1, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong, True))
                    .setStorProcParam(2, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong, True))
                    .setStorProcParam(3, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nStatquota"), eFunctions.Values.eTypeData.etdLong, True))
                    .setStorProcParam(5, .setdate(Session("dBeginDate")))
                    .setStorProcParam(6, .setdate(Session("dEndDate")))
                    If Request.QueryString("sCodispl") = "VI017" Or
                       Request.QueryString("sCodispl") = "VI017-2" Then
                        .setStorProcParam(7, "1")
                        .setStorProcParam(8, Session("sKey"))
                    Else
                        .setStorProcParam(7, "2")
                        .setStorProcParam(8, "")
                    End If
                    Response.Write(.Command)
                End With

        End Select
        mobjDocuments = Nothing
    End Sub

    '% insCreIllustration: Genera datos para reporte de ilustracion
    '---------------------------------------------------------------------------
    Private Sub insCreIllustration(ByRef sCertype As String, ByVal nBranch As String, ByVal nProduct As String, ByVal nPolicy As String, ByVal nCertif As String, ByVal dEffecdate As String, ByVal nIllustType As String, ByVal nProjRent As String, ByVal nAddPremium As String, ByVal nSurrMonth As String, ByVal nSurrYear As String, ByVal nSurrAmount As String)
        '---------------------------------------------------------------------------
        Dim lcolTmp_val669 As Object

        'UPGRADE_NOTE: The 'ePolicy.Tmp_val669s' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lcolTmp_val669 = New ePolicy.Tmp_val669s

        If lcolTmp_val669.InsCalValuePolIlustration(sCertype, mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nIllustType, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nProjRent, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nAddPremium, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nSurrMonth, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nSurrYear, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(nSurrAmount, eFunctions.Values.eTypeData.etdDouble, True)) Then

            Session("sKey") = lcolTmp_val669.sKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionId"), eFunctions.Values.eTypeData.etdDouble))
        End If
        lcolTmp_val669 = Nothing
    End Sub
    '% insUpdNextReceipt: actualiza la fecha de próxima facturación con la fecha de fin de vigencia
    '%                    luego de haber generado el recibo de devolución correspondiente a la reducción
    '%                    de vigencia
    '----------------------------------------------------------------------------------------------
    Private Sub insUpdNextReceipt(ByRef sCertype As Object, ByVal nBranch As Object, ByVal nProduct As Object, ByVal nPolicy As Object, ByVal nCertif As Object, ByVal dExpirdate As Object, ByVal nIndicator As Object)
        '---------------------------------------------------------------------------
        Dim lclsPolicy As Object

        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lclsPolicy = New ePolicy.Policy

        With lclsPolicy
            Call .insUpdNextReceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, dExpirdate, nIndicator, Session("nUsercode"))
        End With

        lclsPolicy = Nothing
    End Sub

</script>
<%Response.Expires = -1
                                            'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                            mobjNetFrameWork = new eNetFrameWork.Layout
                                            mobjNetFrameWork.sSessionID = Session.SessionID
                                            mobjNetFrameWork.nUsercode = Session("nUsercode")
                                            Call mobjNetFrameWork.BeginPage("valpolicytra")
                                            'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                                            mobjValues = new eFunctions.Values

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
    document.VssVersion="$$Revision: 46 $|$$Date: 22/10/09 7:02p $|$$Author: Gazuaje $"

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
<%If Request.QueryString.Item("sCodispl") = "SO001" Then%>
//%InsShowSequence: Muestra la secuencia de la póliza en Recuperación
//------------------------------------------------------------------------------------------------
function InsShowSequence(){
//------------------------------------------------------------------------------------------------
    var lstrQueryString = new String;
    lstrQueryString = "&sCertype=2" + "&nBranch=" + <%=Request.Form.Item("cbeBranch") %> +  "&nProduct=" + <%=Request.Form.Item("valProduct") %> + "&nPolicy=" + <%=Request.Form.Item("tcnPolicy") %> + "&nCertif=" + <%=Request.Form.Item("tcnCertif") %> + "&nTransaction=3"+ "&LoadWithAction=301";
    //ShowPopUp('../../Common/GoTo.aspx?sPopUp=1&sOriginalForm=SO001&sCodispl=CA001_K&sCodisplOrig=SO001'+ lstrQueryString , 'CA001_K', window.screen.availWidth, window.screen.availHeight, 'no','no',0,0)
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
    'UPGRADE_NOTE: The 'ePolicy.Funds_pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    lclsFunds_Pol = new ePolicy.Funds_pol
    'UPGRADE_NOTE: The 'ePolicy.policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    MobjPolicy = new ePolicy.policy

    If Request.QueryString.Item("sCodispl") = "VI7002" Then
        'UPGRADE_NOTE: The 'ePolicy.tmp_Funds_pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        mobjPolicyTra = new ePolicy.tmp_Funds_pol
    Else
        'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        mobjPolicyTra = new ePolicy.ValPolicyTra
    End If

    '+ Si no se han validado los campos de la página
    If Request.Form.Item("sCodisplReload") = vbNullString Then
        mstrErrors = insValPolicyTra()
        Session("sErrorTable") = mstrErrors

        Session("sForm") = Request.Form.ToString
    Else
        Session("sErrorTable") = vbNullString
        Session("sForm") = vbNullString
    End If
    If mstrErrors > vbNullString Then
        With Response
            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""PolicyTraError"",660,330);")
            If Request.QueryString.Item("sCodispl") = "VI818" Then
                .Write("CancelErrors();")
            End If
            If (Request.QueryString.Item("sCodispl") = "CA028" Or Request.QueryString.Item("sCodispl") = "CA080") And Request.QueryString.Item("sPopUp") = "1" Then
                .Write("CancelErrors();")
            Else
                .Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
            End If
            .Write("</SCRIPT>")
        End With
    Else
        If insPostPolicyTra Then
            If Request.QueryString.Item("WindowType") <> "PopUp" Then
                If Request.QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
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

                                    If CStr(Session("sCodisplOri")) = "CA767" Then
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
                                Else
                                    If CStr(Session("optProcessType")) <> "1" Then
                                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                                            Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
                                        Else
                                            Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
                                        End If
                                    End If
                                End If
                            End If
                        Case "VI7004"
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
                                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                                            Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
                                        Else
                                            Response.Write("<SCRIPT>window.close();opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
                                        End If
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
                                Response.Write("ShowPopUp('/VTimeNet/Common/ShowResults.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sKey=" & mstrMessage & "&sCodispl_orig=" & Request.QueryString.Item("sCodispl_orig") & "', 'PolicyTraRes',660,330);")
                                Response.Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
                                Response.Write("</SCRIPT>")
                                '+Sino retorna a la página inicial
                            Else
                                If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
                                    Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099C';</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CA099';</SCRIPT>")
                                End If
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
                        Case "VI017", "VI017-2"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
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
                        Case "CA080"
                            Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & mstrCodispl & mstrQueryString & "';</SCRIPT>")
                        Case "CA051"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                If Session("sFile") <> String.Empty Then
                                    Response.Write("<SCRIPT>")
                                    Response.Write("myWindow= window.open('/VTimeNet/Common/fileplaceholder.aspx?dt=cm&file=" & Session("sFile") & "','download','menubar=1,resizable=1,width=350,height=300');")
                                    Response.Write("</SCRIPT>")
                                End If
                                Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            Else
                                If Session("sFile") <> String.Empty Then
                                    Response.Write("<SCRIPT>")
                                    Response.Write("myWindow= window.open('/VTimeNet/Common/fileplaceholder.aspx?dt=cm&file=" & Session("sFile") & "','download','menubar=1,resizable=1,width=350,height=300');")
                                    Response.Write("</SCRIPT>")
                                End If
                                Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                            End If
                        Case "VI818"
                            Response.Write("<SCRIPT>top.fraFolder.document.location.href='VI818.aspx?sCodispl=VI818';ShowPopUp('/VTimeNet/Policy/Policytra/Vi820.aspx?sSource=1', 'VI820',900,600,'yes','yes');</SCRIPT>")
                        Case "CA037"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                            End If
                        Case "SO001"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                            End If
                        Case "CA032"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();top.opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
                            End If
                        Case Else
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    End Select

                ElseIf Request.QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdataCancel Then
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
                        Case "CA034A"
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
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

                            '                        Case "CAL963"
                            '							If Request.QueryString("nZone") = 1 Then
                            '								Response.Write "<NOTSCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=CAL963&cbeBranch="& Request.Form("cbeBranch") & "&valProduct="& Request.Form("valProduct") & "&tcnPolicy="& Request.Form("tcnPolicy") &"';</SCRIPT>"
                            '							End If

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
                        Case "CA080"
                            '+ Si la ventana donde se encuentra la grilla se muestra como PopUp
                            If Request.QueryString.Item("sPopUp") = "1" Then
                                Response.Write("<SCRIPT>window.close();</SCRIPT>")
                            Else
                                If Request.Form.Item("sCodisplReload") = vbNullString Then
                                    Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=CA080&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=CA080&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
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
                        Case "VI820"
                            Response.Write("<SCRIPT>window.close();top.document.location.reload();</SCRIPT>")
                        Case "VI017-2"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""VI017-2.asp?sCodispl=VI017-2&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""VI017-2.asp?sCodispl=VI017-2&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                            End If

                        Case Else
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                If Request.QueryString.Item("sCodispl") = "VI7002" Then
                                    Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                ElseIf Request.QueryString.Item("sCodispl") = "CA051" Then
                                    Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                End If
                            Else
                                If Request.Form.Item("sCodisplReload") = "CA034" Then
                                    If Request.QueryString.Item("nZone") = "1" Then
                                        mstrQueryString = "&sCertype=" & Request.Form.Item("tctCertype") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPolicy=" & Request.Form.Item("tcnPolicy") & "&nCertif=" & Request.Form.Item("tcnCertif") & "&nAgency=" & Request.Form.Item("cbeAgency") & "&nExeMode=" & Request.Form.Item("optExecute") & "&nProcess=" & Request.Form.Item("optProcess") & "&sCodisplOri=" & Request.Form.Item("hddCodisplOri") & "&nServ_Order=" & Request.Form.Item("tcnServ_Order")
                                    End If
                                End If

                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                            End If
                    End Select
                End If
            Else
                '+ Se recarga la página que invocó la PopUp
                If IsNothing(Request.Form.Item("sCodisplReload")) Then
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "CA028", "CA028A"
                            Response.Write("<SCRIPT>top.opener.document.location.href='CA028.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                        Case "CA080"
                            Response.Write("<SCRIPT>top.opener.document.location.href='CA080.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                        Case "VI010"
                            Response.Write("<SCRIPT>top.opener.document.location.href='VI010.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
                        Case "VI016"
                            Response.Write("<SCRIPT>top.opener.document.location.href='VI016.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
                        Case "CA980"
                            Response.Write("<SCRIPT>top.opener.document.location.href='CA980_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        Case "VI7502"
                            Response.Write("<SCRIPT>top.opener.document.location.href='VI7502.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("ReloadIndex") & mstrQueryString & "'</SCRIPT>")

                        Case Else
                            If Request.QueryString.Item("EditWithoutPopPup") = "True" Then
                                Response.Write("<SCRIPT>top.fraFolder.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
                            End If
                    End Select
                Else
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "CA099A"
                            If Request.QueryString.Item("sClickCheck") = "1" Then
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.opener.top.fraFolder.document.location.reload();setTimeout('opener.top.close();',500);</SCRIPT>")
                            End If
                        Case "VI016"
                            If Request.QueryString.Item("sClickCheck") = "1" Then
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.opener.top.fraFolder.document.location.reload();setTimeout('opener.top.close();',500);</SCRIPT>")
                            End If
                        Case "VI7000"
                            If Request.QueryString.Item("sClickCheck") = "1" Then
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.opener.top.fraFolder.document.location.reload();setTimeout('opener.top.close();',500);</SCRIPT>")
                            End If

                        Case "VI7004"
                            If Request.QueryString.Item("sClickCheck") = "1" Then
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.opener.top.fraFolder.document.location.reload();setTimeout('opener.top.close();',500);</SCRIPT>")
                            End If
                        Case "CA980"
                            Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='CA980_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        Case "CA986"
                            Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='CA986.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                        Case Else
                            Response.Write("<SCRIPT>window.close();top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicyTra&sCodispl=CA099A&sConfig=InSequence&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
                    End Select
                End If
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


