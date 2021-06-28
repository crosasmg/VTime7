<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eBatch" %>

<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    Dim mobjValues As eFunctions.Values

    Dim sCodispl As Object

    Private mstrErrors As Object

    '+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
    '+ de la página que la invoca.

    Dim mstrCommand As String
    Dim mobjClaimRep As Object
    Dim mblnTimeOut As Boolean

    ' +Declaración de las variables que reciben los valores de los campos que se deben validar.
    Dim sClient As String
    Dim dEndDate As Date
    Dim nPolicy As Double
    Dim dIniDate As Date
    Dim nUserCode As Integer
    Dim nBranch As Integer
    Dim nClaim As Double
    Dim nProduct As Integer

    Dim mclsGeneral As Object
    Dim mstrKey As String
    Dim mstrFileName As String
    Dim mstrPath As String


    '% insValClaim: Se realizan las validaciones masivas de la forma
    '-------------------------------------------------------------------------------------------
    Function insValClaim() As Object
        '-------------------------------------------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        insValClaim = vbNullString

        Select Case sCodispl
        '+ SIL001: Informe de Siniestros
            Case "SIL001"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL001_k(.QueryString("sCodispl"), mobjValues.StringToDate(.Form("tcdIniDate")), mobjValues.StringToDate(.Form("tcdEndDate")), mobjValues.StringToType(.Form("valModOpc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+SIL002: Siniestros en exceso de un importe dado
            Case "SIL002"
                mobjClaimRep = New eClaim.Claim
                With Request
                    insValClaim = mobjClaimRep.insValSIL002("SIL002", mobjValues.StringToType(.Form("tcdIniDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdEndate"), eFunctions.Values.eTypeData.etdDate))
                End With

            '+ SIL003: Órdenes de reparación y peritaje 
            Case "SIL003"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL003_k(.QueryString("sCodispl"), mobjValues.StringToDate(.Form("tcdIniDate")), mobjValues.StringToDate(.Form("tcdEndDate")), mobjValues.StringToType(.Form("valProfessional"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ SIL004: Relación de órdenes de un profesional 
            Case "SIL004"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL004_k(.QueryString("sCodispl"), mobjValues.StringToDate(.Form("tcdIniDate")), mobjValues.StringToDate(.Form("tcdEndDate")))
                End With

            '+ SIL005: Recibo de finiquitos
            Case "SIL005"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL005_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnFinishNum"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ SIL006: Carátula de un siniestro 
            Case "SIL006"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL006_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+SIL007: Reserva de siniestros
            Case "SIL007"
                mobjClaimRep = New eClaim.Claim
                With Request
                    insValClaim = mobjClaimRep.insValSIL007(.QueryString("Action"), mobjValues.StringToDate(.Form("tcdInitdate")), mobjValues.StringToDate(.Form("tcdEnddate")))
                End With

            '+SIL009: Planilla de siniestro
            Case "SIL009"
                mobjClaimRep = New eClaim.Claim
                With Request
                    insValClaim = mobjClaimRep.insValSIL009_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcdInidate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOrder"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ SIL010: Informe de Siniestralidad
            Case "SIL010"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL010_k(.QueryString("sCodispl"), mobjValues.StringToDate(.Form("tcdIniDate")), mobjValues.StringToDate(.Form("tcdEndDate")), mobjValues.StringToType(.Form("valModOpc"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+SIL762: Denuncio de siniestro
            Case "SIL762"
                mobjClaimRep = New eClaim.Claim
                With Request
                    insValClaim = mobjClaimRep.insValSIL762_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeCase"), .Form("optOption"), mobjValues.StringToType(.Form("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdDate_end"), eFunctions.Values.eTypeData.etdDate))
                End With

            '+SIL780: Listado de reserva de siniestro
            Case "SIL780"
                mobjClaimRep = New eClaim.ValClaimRep
                With Request
                    insValClaim = mobjClaimRep.insValSIL780_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "SIL961"
                mobjClaimRep = New eClaim.Claim
                With Request
                    If Request.QueryString("nZone") = 1 Then
                        insValClaim = mobjClaimRep.insValSIL961(.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        insValClaim = ""
                    End If
                End With

                '+SIL910: Nómina de siniestros                
            Case "SIL910"
                insValClaim = vbNullString

            Case "SIL704"
                insValClaim = vbNullString

            '++++++++++++ VT00014 GAP 13 Libro de siniestros pagados			
            Case "SIL705"
                mobjClaimRep = New eClaim.ValClaimRep
                insValClaim = mobjClaimRep.insValSIL705(sCodispl, dIniDate, dEndDate)
                'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimRep = Nothing

            '++++++++++++ VT00017 GAP 11 Reportes de siniestros por estado			
            Case "SIL00970"
                mobjClaimRep = New eClaim.ValClaimRep
                insValClaim = mobjClaimRep.insValSIL00970(sCodispl, nBranch, nProduct, nPolicy, dIniDate, dEndDate)
                'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimRep = Nothing

            '++++++++++++ VT00057 GAP 12 Reportes de documentos solicitados a un siniestro			
            Case "SIL00971"
                mobjClaimRep = New eClaim.ValClaimRep
                insValClaim = mobjClaimRep.insValSIL00971(sCodispl, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjClaimRep = Nothing

            Case "SIL974"
                mobjClaimRep = New eClaim.ValClaimRep
                insValClaim = mobjClaimRep.insValSIL974(sCodispl, mobjValues.StringToType(Request.Form("tcnCheque"), eFunctions.Values.eTypeData.etdDouble))
                mobjClaimRep = Nothing

            Case "SIL1065"
                insValClaim = True

            '+ Finiquito de muerte  	        
            Case "SIL1001"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.InsValSIL1001_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble))

                    'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjClaimRep = Nothing
                End With

            '+ Carta de citación APV
            Case "SIL978"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.InsValSIL978_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble))

                    'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjClaimRep = Nothing
                End With

            '+SIL1002: Informe de siniestros según estado
            Case "SIL1002"
                With Request
                    mobjClaimRep = New eClaim.ValClaimRep
                    insValClaim = mobjClaimRep.insValSIL1002(.QueryString("sCodispl"), .Form("optDate"), .Form("cbeStaClaim"), mobjValues.StringToDate(.Form("tcdInitDate")), mobjValues.StringToDate(.Form("tcdEndDate")))
                    'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjClaimRep = Nothing
                End With
                '+SIL7482: Valorización II - SOAP
            Case "SIL7482"
                mobjClaimRep = New eClaim.ValClaimRep
                insValClaim = mobjClaimRep.insValSIL7482_k(Request.QueryString("sCodispl"),
                                                         mobjValues.StringToType(Request.Form("tcnClaim"), Values.eTypeData.etdLong),
                                                         mobjValues.StringToDate(Request.Form("tcdValDate")))
            Case "SIL7483", "SIL7484"
                insValClaim = String.Empty
            Case Else
                insValClaim = "insValClaim: Código lógico no encontrado (" & sCodispl & ")"
        End Select

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsErrors = Nothing
    End Function

    '% insPostClaim: Se realizan las actualizaciones de las ventanas
    '-------------------------------------------------------------------------------------------
    Function insPostClaim() As Boolean
        '-------------------------------------------------------------------------------------------
        Dim nOffice As Byte
        Dim nDetOffice As Byte
        Dim nBranch As Byte
        Dim nDetBranch As Byte
        Dim nProduct As Byte
        Dim nDetProduct As Byte
        Dim nTypeMov As Byte
        Dim nDetMov As Byte
        Dim nCause As Byte
        Dim nDetCause As Byte
        Dim nDraft As Byte
        Dim nDetDraft As Byte
        Dim lclsBatch_param As eSchedule.Batch_param

        Dim lblnPrintReport As Boolean
        lblnPrintReport = True

        Dim mclsLedgerAutDetail3 As eLedge.LedgerAutDetail
        Dim mclsSil1065 As eClaim.ValClaimRep

        Select Case sCodispl

        '+ SIL001: Informe de Siniestros 
            Case "SIL001"
                insPostClaim = True

            '+SIL002: Siniestros en exceso de un importe dado
            Case "SIL002"
                insPostClaim = True

            '+ SIL003: Órdenes de reparación y peritaje 
            Case "SIL003"
                insPostClaim = True

            '+ SIL004: Relación de órdenes de un profesional 
            Case "SIL004"
                insPostClaim = True

            '+ SIL005: Recibos de finiquitos		
            Case "SIL005"
                insPostClaim = True

            '+ SIL006: Carátula de un siniestro 
            Case "SIL006"
                With Request
                    insPostClaim = True
                End With

            '+SIL007: Reserva de siniestros
            Case "SIL007"
                insPostClaim = True

            '+SIL009: Planilla de siniestro
            Case "SIL009"
                With mobjValues
                    If .StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nOffice = 0
                    Else
                        nOffice = .StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkOfficeDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetOffice = 1
                    Else
                        nDetOffice = 0
                    End If

                    If .StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nBranch = 0
                    Else
                        nBranch = .StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkBranchDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetBranch = 1
                    Else
                        nDetBranch = 0
                    End If

                    If .StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nProduct = 0
                    Else
                        nProduct = .StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkProductDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetProduct = 1
                    Else
                        nDetProduct = 0
                    End If

                    If .StringToType(Request.Form("cbeMov_type"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nTypeMov = 0
                    Else
                        nTypeMov = .StringToType(Request.Form("cbeMov_type"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkTypeMov"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetMov = 1
                    Else
                        nDetMov = 0
                    End If

                    If .StringToType(Request.Form("valCause"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nCause = 0
                    Else
                        nCause = .StringToType(Request.Form("valCause"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkCause"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetCause = 1
                    Else
                        nDetCause = 0
                    End If

                    If .StringToType(Request.Form("cbeB_draft"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                        nDraft = 0
                    Else
                        nDraft = .StringToType(Request.Form("cbeB_draft"), eFunctions.Values.eTypeData.etdLong)
                    End If

                    If .StringToType(Request.Form("chkBDraft"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                        nDetDraft = 1
                    Else
                        nDetDraft = 0
                    End If
                End With
                mobjClaimRep = New eClaim.ValClaimRep
                insPostClaim = mobjClaimRep.insPostSIL009_k(mobjValues.StringToType(Request.Form("tcdInidate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("optTypeRep"), eFunctions.Values.eTypeData.etdLong), nOffice, nDetOffice, nBranch, nDetBranch, nProduct, nDetProduct, nTypeMov, nDetMov, nCause, nDetCause, nDraft, nDetDraft, mobjValues.StringToType(Request.Form("tcnIndic"), eFunctions.Values.eTypeData.etdInteger))

                Session("P_SKEY") = mobjClaimRep.skey
            '+ SIL001: Informe de Siniestralidad
            Case "SIL010"
                insPostClaim = True
            '+ SIL001: Informe de Siniestralidad
            Case "SIL00970"
                insPostClaim = True
            '+ SIL001: Informe de documentos
            Case "SIL00971"
                insPostClaim = True

            '+SIL762: Denuncio de siniestro
            Case "SIL762"
                insPostClaim = True
            '+SIL780: Listado de reserva de siniestro
            Case "SIL780"
                insPostClaim = mobjClaimRep.insPostSIL780_K(mobjValues.StringToType(Request.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
            '+SIL691: impresion de rechazo
            Case "SIL961"
                insPostClaim = True
                If Request.QueryString("nZone") = 1 Then
                    lblnPrintReport = False
                    Session("nClaim") = mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nCaseNum") = mobjValues.StringToType(Request.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nDeman_Type") = mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nNotenum") = mobjValues.StringToType(Request.Form("nNotenum"), eFunctions.Values.eTypeData.etdDouble)
                Else
                    lblnPrintReport = True
                End If

            Case "SIL910"
                insPostClaim = True

            Case "SIL704"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mclsLedgerAutDetail3 = New eLedge.LedgerAutDetail
                    insPostClaim = mclsLedgerAutDetail3.InsCreTmp_Sil704(mobjValues.StringToType(CStr(Session("nCompanyUser")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nInsur_Area")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                    Session("P_SKEY") = mclsLedgerAutDetail3.P_SKEY
                    'UPGRADE_NOTE: Object mclsLedgerAutDetail3 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mclsLedgerAutDetail3 = Nothing
                Else
                    insPostClaim = True
                    lblnPrintReport = False

                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 121
                        .nUserCode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)
                        .Add(1, .skey)
                        .Add(1, mobjValues.StringToType(CStr(Session("nCompanyUser")), eFunctions.Values.eTypeData.etdDouble))
                        .Add(1, mobjValues.StringToType(CStr(Session("nInsur_Area")), eFunctions.Values.eTypeData.etdDouble))
                        .Add(1, mobjValues.StringToType(Request.Form("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                        .Add(1, mobjValues.StringToType(Request.Form("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                        .Add(1, mobjValues.StringToType(Request.Form("optOption"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(1, .nUserCode)
                        .Add(2, .sKey)
                        .Add(2, mobjValues.StringToType(Request.Form("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                        .Add(2, mobjValues.StringToType(Request.Form("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                        .Add(2, mobjValues.StringToType(Request.Form("optOption"), eFunctions.Values.eTypeData.etdInteger))
                        
                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.skey & "');</" & "Script>")
                    'UPGRADE_NOTE: Object lclsBatch_param may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsBatch_param = Nothing
                End If

            '+ SIL705: Libro de Siniestros Pagados		
            Case "SIL705"
                insPostClaim = True
                lblnPrintReport = False

                If CStr(Session("BatchEnabled")) = "1" Then
                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 122
                        .nUserCode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)
                        .Add(1, .skey)
                        .Add(1, mobjValues.StringToType(CStr(Session("nCompanyUser")), eFunctions.Values.eTypeData.etdDouble))
                        .Add(1, mobjValues.StringToType(Request.Form("tcdIniDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(1, mobjValues.StringToType(Request.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(1, mobjValues.StringToType(Request.Form("optOption"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(1, .nUsercode)
                        .Add(2, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.skey & "');</" & "Script>")
                    'UPGRADE_NOTE: Object lclsBatch_param may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsBatch_param = Nothing
                End If
            Case "SIL1065"
                mclsSil1065 = New eClaim.ValClaimRep
                insPostClaim = mclsSil1065.insPostSIL1065(mobjValues.StringToType(Request.Form("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))

                Session("P_SKEY") = mclsSil1065.P_SKEY
                'UPGRADE_NOTE: Object mclsSil1065 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mclsSil1065 = Nothing

            '+ SIL1001: Finiquitos de muerte
            Case "SIL1001"
                insPostClaim = True

            '+SIL974: Reimpresión de Orden de Pago
            Case "SIL974"
                mclsSil1065 = New eClaim.ValClaimRep
                insPostClaim = mclsSil1065.insPostSIL974(mobjValues.StringToType(Request.Form("tcnCheque"), eFunctions.Values.eTypeData.etdDouble))
                Session("P_SKEY") = mclsSil1065.sKey
                mclsSil1065 = Nothing

            '+ Carta de citación APV
            Case "SIL978"
                insPostClaim = True

            '+ VIL8007: Reporte de cartolas mensuales
            Case "SIL1002"
                insPostClaim = True
                lblnPrintReport = True
            Case "SIL7482"
                insPostClaim = True
                lblnPrintReport = True
            Case "SIL7483", "SIL7484"
                insPostClaim = True
                lblnPrintReport = False
        End Select


        If insPostClaim Then
            If lblnPrintReport Then insPrintDocuments()
        Else
            If (Request.QueryString("nZone") <> 1 And sCodispl = "SIL961") Then insPrintDocuments()
        End If

    End Function

    '%insPrintDocuments : Realiza la ejecución del reporte
    '-------------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-------------------------------------------------------------------------------------------
        Dim nBranch As Integer
        Dim nDetBranch As Byte
        Dim nOffice As Byte
        Dim nDetOffice As Byte
        Dim nProduct As Integer
        Dim nDetProduct As Byte
        Dim nTypeMov As Byte
        Dim nDetMov As Byte
        Dim nCause As Byte
        Dim nDetCause As Byte
        Dim nDraft As Byte
        Dim nDetDraft As Byte
        Dim nClaim As Object
        Dim nPolicy As Object
        Dim nCertif As Object
        Dim nCase As Object
        Dim nDeman_Type As Object
        Dim nYear As Object
        Dim nPrint As Boolean

        Dim mobjDocuments As eReports.Report
        mobjDocuments = New eReports.Report

        Dim lclsProdmaster As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        With mobjDocuments
            Select Case sCodispl
                Case "SIL001"
                    .sCodispl = "SIL001"
                    .ReportFilename = "SIL001.rpt"
                    .setStorProcParam(1, mobjDocuments.setdate(Request.Form("tcdInidate")))
                    .setStorProcParam(2, mobjDocuments.setdate(Request.Form("tcdEnddate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("valModOpc"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    If mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(6, 0)
                    Else
                        .setStorProcParam(6, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("valCurrency"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(7, 0)
                    Else
                        .setStorProcParam(7, mobjValues.StringToType(Request.Form("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    Response.Write(.Command)

                Case "SIL002"
                    .sCodispl = "SIL002"

                    If Request.Form("optTypeRep") = "1" Then
                        .ReportFilename = "SIL002a.RPT"
                    Else
                        .ReportFilename = "SIL002p.RPT"
                    End If

                    .setStorProcParam(1, .setdate(Request.Form("tcdIniDate")))
                    .setStorProcParam(2, .setdate(Request.Form("tcdEndate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                Case "SIL003"
                    mobjDocuments.sCodispl = "SIL003"
                    If Request.Form("cbeOrderType") = 1 Then
                        mobjDocuments.ReportFilename = "SIL003OC.rpt"
                    Else
                        mobjDocuments.ReportFilename = "SIL003ALL.rpt"
                    End If
                    .setStorProcParam(1, mobjDocuments.setdate(Request.Form("tcdInidate")))
                    .setStorProcParam(2, mobjDocuments.setdate(Request.Form("tcdEnddate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    If mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(5, 0)
                    Else
                        .setStorProcParam(5, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("valProfessional"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(6, 0)
                    Else
                        .setStorProcParam(6, mobjValues.StringToType(Request.Form("valProfessional"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                Case "SIL004"
                    .sCodispl = "SIL004"
                    .ReportFilename = "SIL004.rpt"
                    .setStorProcParam(1, mobjDocuments.setdate(Request.Form("tcdInidate")))
                    .setStorProcParam(2, mobjDocuments.setdate(Request.Form("tcdEnddate")))
                    If mobjValues.StringToType(Request.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(3, 0)
                    Else
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("cbeDesc_Status"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                Case "SIL005"
                    .sCodispl = "SIL005"
                    .ReportFilename = "SIL005.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    If mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(3, 0)
                    Else
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(4, 0)
                    Else
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(5, 0)
                    Else
                        .setStorProcParam(5, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(6, 0)
                    Else
                        .setStorProcParam(6, mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnCasenum"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(7, 0)
                    Else
                        .setStorProcParam(7, mobjValues.StringToType(Request.Form("tcnCasenum"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(8, 0)
                    Else
                        .setStorProcParam(8, mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    If mobjValues.StringToType(Request.Form("tcnFinishNum"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(9, 0)
                    Else
                        .setStorProcParam(9, mobjValues.StringToType(Request.Form("tcnFinishNum"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    .setStorProcParam(10, Session("nUsercode"))
                    Response.Write(.Command)

                Case "SIL006"
                    .sCodispl = "SIL006"
                    .ReportFilename = "SIL006.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                Case "SIL007"
                    .sCodispl = "SIL007"

                    If Request.Form("optModal") = 1 Then
                        .ReportFilename = "SIL007ALL.rpt"
                    Else
                        .ReportFilename = "SIL007CC.rpt"
                    End If

                    .setStorProcParam(1, Request.Form("optModal"))
                    .setStorProcParam(2, .setdate(Request.Form("tcdInitdate")))
                    .setStorProcParam(3, .setdate(Request.Form("tcdEnddate")))
                    .setStorProcParam(4, Request.Form("cbeOffice"))
                    .setStorProcParam(5, Request.Form("cbeBranch"))
                    .setStorProcParam(6, Request.Form("valProduct"))
                    Response.Write(.Command)

                Case "SIL009"
                    .sCodispl = "SIL009"
                    With mobjValues
                        If .StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nOffice = 0
                        Else
                            nOffice = .StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkOfficeDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetOffice = 1
                        Else
                            nDetOffice = 0
                        End If

                        If .StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nBranch = 0
                        Else
                            nBranch = .StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkBranchDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetBranch = 1
                        Else
                            nDetBranch = 0
                        End If

                        If .StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nProduct = 0
                        Else
                            nProduct = .StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkProductDet"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetProduct = 1
                        Else
                            nDetProduct = 0
                        End If

                        If .StringToType(Request.Form("cbeMov_type"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nTypeMov = 0
                        Else
                            nTypeMov = .StringToType(Request.Form("cbeMov_type"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkTypeMov"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetMov = 1
                        Else
                            nDetMov = 0
                        End If

                        If .StringToType(Request.Form("valCause"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nCause = 0
                        Else
                            nCause = .StringToType(Request.Form("valCause"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkCause"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetCause = 1
                        Else
                            nDetCause = 0
                        End If

                        If .StringToType(Request.Form("cbeB_draft"), eFunctions.Values.eTypeData.etdLong, True) < 0 Then
                            nDraft = 0
                        Else
                            nDraft = .StringToType(Request.Form("cbeB_draft"), eFunctions.Values.eTypeData.etdLong)
                        End If

                        If .StringToType(Request.Form("chkBDraft"), eFunctions.Values.eTypeData.etdLong) = 1 Then
                            nDetDraft = 1
                        Else
                            nDetDraft = 0
                        End If
                    End With


                    Select Case Request.Form("optTypeRep")
                    '+ Detalle.
                        Case 1
                            .ReportFilename = "SIL009D.rpt"
                        '+ Resumen.
                        Case 2
                            .ReportFilename = "SIL009R.rpt"
                        '+Ambos
                        Case 3
                            .ReportFilename = "SIL009D.rpt"
                    End Select
                    .setStorProcParam(1, Session("P_SKEY"))
                    .setStorProcParam(2, nDetOffice)
                    .setStorProcParam(3, nDetBranch)
                    .setStorProcParam(4, nDetProduct)
                    .setStorProcParam(5, nDetMov)
                    .setStorProcParam(6, nDetCause)
                    .setStorProcParam(7, nDetDraft)
                    .setStorProcParam(8, mobjValues.StringToType(Request.Form("cbeOrder"), eFunctions.Values.eTypeData.etdInteger))
                    Response.Write(.Command)

                    If Request.Form("optTypeRep") = "3" Then
                        .ReportFilename = "SIL009R.rpt"
                        Response.Write(.Command)
                    End If

                '+ SIL010: Informe de Siniestralidad
                Case "SIL010"
                    .sCodispl = "SIL010"
                    Select Case Request.Form("valModOpc")
                        Case 1
                            .ReportFilename = "SIL010_1.rpt"
                        Case 3
                            .ReportFilename = "SIL010_3.rpt"
                        Case 4
                            .ReportFilename = "SIL010_4.rpt"
                    End Select
                    .setStorProcParam(1, mobjDocuments.setdate(Request.Form("tcdInidate")))
                    .setStorProcParam(2, mobjDocuments.setdate(Request.Form("tcdEnddate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("valModOpc"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                '+SIL762: Denuncio de siniestro
                Case "SIL762"
                    Dim lclsClaim_Case As eClaim.Claim_case

                    lclsProdmaster = New eProduct.Product
                    lclsClaim_Case = New eClaim.Claim_case

                    If lclsClaim_Case.GetClaim_CaseInfo(Request.Form("cbeCase")) Then
                        nCase = lclsClaim_Case.nCase_num
                        nDeman_Type = lclsClaim_Case.nDeman_type
                    Else
                        nCase = 0
                        nDeman_Type = 0
                    End If
                    'nCase = mobjValues.StringToType(Request.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble)
                    'nDeman_Type = mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble)

                    nBranch = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                    nClaim = mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble)
                    nProduct = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                    nPolicy = mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                    nCertif = mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                    If nCase = 0  Then
                        nCase = mobjValues.StringToType(Request.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble)
                        nDeman_Type = mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble)
                    End If

                    If lclsProdmaster.FindProdMaster(nBranch, nProduct) Then
                        Select Case lclsProdmaster.sBrancht
                        '+Denuncio Vida/Oncologico.
                            Case 1
                                .sCodispl = "SIL762"
                                If mobjValues.StringToType(Request.Form("chkClaim"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                                    .ReportFilename = "SIL762_O.rpt"
                                Else
                                    .ReportFilename = "SIL762_V.rpt"
                                End If
                                nPrint = True
                            '+Denuncio FullHouse.
                            Case 4
                                lclsPolicy = New ePolicy.Policy
                                lclsPolicy.Find_TabNameB(nBranch)
                                If lclsPolicy.sTabname = "FIRE" Then
                                    .sCodispl = "SIL762"
                                    .ReportFilename = "SIL762_FH.rpt"
                                    nPrint = True
                                Else
                                    nPrint = False
                                End If
                                'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                                lclsPolicy = Nothing
                            '+Denuncio FullCar.
                            Case 3
                                .sCodispl = "SIL762"
                                .ReportFilename = "SIL762_FC.rpt"
                                nPrint = True
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
                            .setStorProcParam(4, nPolicy) 'mobjValues.StringToType(Request.Form("tcnPolicy"),eFunctions.Values.eTypeData.etdDouble,true)

                            'IF nCertif = intNull Then 
                            '	.setStorProcParam 5, 0 
                            'Else
                            .setStorProcParam(5, nCertif)
                            'End if 
                            .setStorProcParam(6, nCase)
                            .setStorProcParam(7, nDeman_Type)
                            .setStorProcParam(8, Request.Form("optOption"))
                            .setStorProcParam(9, .setdate(Request.Form("tcdDate_ini")))
                            .setStorProcParam(10, .setdate(Request.Form("tcdDate_end")))


                            Response.Write(.Command)
                        End If
                    End If

                '+SIL780: Listado de reserva de siniestro
                Case "SIL780"
                    .sCodispl = "SIL780"
                    .ReportFilename = "SIL780.rpt"
                    .setStorProcParam(1, mobjClaimRep.skey)
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                '+SIL961: Listado de reserva de siniestro
                Case "SIL961"
                    .sCodispl = "SIL961"
                    .ReportFilename = "SIL961.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(CStr(Session("nNotenum")), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                    '+SIL910: Nómina de siniestro
                Case "SIL910"
                    .sCodispl = "SIL910"
                    If mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
                        .ReportFilename = "SIL910_2.rpt"
                    Else
                        .ReportFilename = "SIL910_1.rpt"
                    End If
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, Request.Form("dtcClient"))
                    Response.Write(.Command)

                Case "SIL704"
                    .sCodispl = "SIL704"
                    .ReportFilename = "SIL704.rpt"
                    .setStorProcParam(1, Session("P_SKEY"))
                    Response.Write(.Command)

                    '+SIL705: Libro de Siniestro Pagados			
                Case "SIL705"
                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".RPT"
                    .setStorProcParam(1, 1)
                    .setStorProcParam(2, Request.Form("tcdIniDate"))
                    .setStorProcParam(3, Request.Form("tcdEndDate"))
                    Response.Write(.Command)
                Case "SIL00970"

                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".RPT"
                    .setStorProcParam(1, Request.Form("tcnPolicy"))
                    .setStorProcParam(2, Request.Form("tcdIniDate"))
                    .setStorProcParam(3, Request.Form("tcdEndDate"))
                    .setStorProcParam(4, Request.Form("cbeBranch"))
                    .setStorProcParam(5, Request.Form("valProduct"))
                    Response.Write(.Command)

                Case "SIL00971"
                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".RPT"
                    .setStorProcParam(1, Request.Form("cbeBranch"))
                    .setStorProcParam(2, Request.Form("valProduct"))
                    .setStorProcParam(3, Request.Form("tcnPolicy"))
                    .setStorProcParam(4, Request.Form("tcnClaim"))
                    .setStorProcParam(5, Session("nUsercode"))
                    .setStorProcParam(6, Request.Form("tctAtention"))
                    .setStorProcParam(7, Request.Form("txtComent"))
                    Response.Write(.Command)

                Case "SIL1065"
                    .sCodispl = sCodispl
                    .ReportFilename = "SIL1065.RPT"
                    .setStorProcParam(1, Request.Form("tcdIniDate"))
                    .setStorProcParam(2, Request.Form("tcdEndDate"))
                    .setStorProcParam(3, Session("P_SKEY"))
                    Response.Write(.Command)

                    '+SIL1001: Finiquito de muerte
                Case "SIL1001"
                    .sCodispl = "SIL1001"
                    .ReportFilename = "SIL1001.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, Request.Form("valClient"))
                    Response.Write(.Command)

                    '+ Carta de citación APV
                Case "SIL978"
                    .sCodispl = "SIL978"
                    .ReportFilename = "SIL978.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("tcnCaseNum"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("tcnDeman_Type"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write(.Command)

                    '+ SIL1002: Informe de siniestros según estado
                Case "SIL1002"
                    mclsGeneral = New eGeneral.GeneralFunction
                    mstrKey = mclsGeneral.getsKey(Session("nUsercode"))
                    mstrFileName = "window.open('/VTimeNet/tfiles/" & mstrKey & ".xls','Listado');"
                    'UPGRADE_NOTE: Object mclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mclsGeneral = Nothing
                    mclsGeneral = New eBatch.MasiveCharge
                    mstrPath = mclsGeneral.GetLoadFile(True)
                    'UPGRADE_NOTE: Object mclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mclsGeneral = Nothing
                    mobjClaimRep = New eClaim.ValClaimRep

                    If mobjClaimRep.insPostSIL1002(Request.Form("optDate"), Request.Form("cbeStaClaim"), mobjValues.StringToDate(Request.Form("tcdInitDate")), mobjValues.StringToDate(Request.Form("tcdEndDate")), mstrKey, mstrPath) Then
                        Response.Write("<SCRIPT>" & mstrFileName & " </" & "Script>")
                    Else
                        Response.Write("<SCRIPT>alert('No existen datos para los parámetros ingresados');</" & "Script>")
                    End If

                    'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjClaimRep = Nothing

                    '+ SIL974: Reimpresión de Orden de Pago
                Case "SIL974"
                    .sCodispl = "OPL714"
                    .ReportFilename = "OPL714.rpt"
                    .setStorProcParam(1, Session("P_SKEY"))
                    .setStorProcParam(2, "01/01/2000")
                    .setStorProcParam(3, "01/01/2000")
                    .setStorProcParam(4, "")
                    Response.Write(.Command)
                    '+ SIL974: Reimpresión de Orden de Pago
                Case "SIL7482"
                    nClaim = mobjValues.TypeToString(Request.Form("tcnClaim"), Values.eTypeData.etdLong)
                    Dim lobjCases As New eClaim.Claim_cases
                    lobjCases.OnlyDemandant = True
                    If lobjCases.Find(nClaim) Then
                        Dim lobjCase As eClaim.Claim_case
                        For Each lobjCase In lobjCases
                            If lobjCase.sStacase <> "13" And lobjCase.nCase_num > 0 Then
                                .Reset()
                                .sCodispl = "SIL7482"
                                .ReportFilename = "SinVSoap.rpt"
                                .setStorProcParam(1, nClaim)
                                .setStorProcParam(2, lobjCase.nCase_num)
                                .setStorProcParam(3, lobjCase.nDeman_type)
                                .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnRequestNu"), eFunctions.Values.eTypeData.etdLong))
                                .setStorProcParam(5, "")
                                .setStorProcParam(6, .setdate(Request.Form("tcdValDate")))
                                .setStorProcParam(7, Request.Form("optHistoryBy"))
                                Response.Write(.Command)
                            End If
                        Next lobjCase
                    End If
            End Select
        End With

        'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjDocuments = Nothing
        'UPGRADE_NOTE: Object lclsProdmaster may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProdmaster = Nothing
    End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUserCode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valclaimrep")
sCodispl = UCase(Request.QueryString("sCodispl"))

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUserCode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valclaimrep"

mstrCommand = "&sModule=Claim&sProject=ClaimRep&sCodisplReload=" & sCodispl

nBranch = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger)
nProduct = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger)
nPolicy = mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True)
nClaim = mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble, True)
dIniDate = mobjValues.StringToType(Request.Form("tcdIniDate"), eFunctions.Values.eTypeData.etdDate)
dEndDate = mobjValues.StringToType(Request.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
sClient = Request.Form("dtcClient")
nUserCode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong)

If nBranch <= 0 Then nBranch = 0
If nProduct <= 0 Then nProduct = 0
If nPolicy <= 0 Then nPolicy = 0
If nClaim <= 0 Then nClaim = 0

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>

	
<SCRIPT>
// Función que retorna a la pagina anterior
//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)}

// Función que define la ubicación de la Pagina
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
</HEAD>
<BODY>

	<%
        If Not Session("bQuery") Or Request.QueryString("nZone") = "1" Then

            '+ Si no se han validado los campos de la página

            If Request.QueryString("sCodisplReload") = vbNullString Then
                mstrErrors = insValClaim
                Session("sErrorTable") = mstrErrors
                Session("sForm") = Request.Form.ToString
            Else
                Session("sErrorTable") = vbNullString
                Session("sForm") = vbNullString
            End If
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """, ""ClaimRepErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If insPostClaim Then
                If sCodispl = "SIL961" Then
                    If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    Else
                        If Request.QueryString("nZone") = 1 Then
                            Response.Write("<SCRIPT>top.fraFolder.document.location='" & sCodispl & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "'</SCRIPT>")
                        End If
                    End If
                ElseIf sCodispl = "SIL7483" Then
                    If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    Else
                        If Request.QueryString("nZone") = 1 Then
                            Response.Write("<SCRIPT>top.fraFolder.document.location='" & sCodispl & ".aspx?sCodispl=" & sCodispl & "&sClient=" & Request.Form("tctClient") & "&sDigit=" & Request.Form("tctClient_Digit") & "'</SCRIPT>")
                        End If
                    End If
                ElseIf sCodispl = "SIL7484" Then
                    If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    Else
                        If Request.QueryString("nZone") = 1 Then
                            Response.Write("<SCRIPT>top.fraFolder.document.location='" & sCodispl & ".aspx?sCodispl=" & sCodispl & "&nClaim=" & Request.Form("tcnClaim") & "'</SCRIPT>")
                        End If
                    End If
                Else
                    'Response.Write("<SCRIPT>setTimeout('insReloadTop(true,false);',5000);</SCRIPT>")           
                    If Request.Form("sCodisplReload") = vbNullString Then
                        If mblnTimeOut Then
                            Response.Write("<SCRIPT>setTimeout('top.document.location.reload();',5000);</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                    End If
                End If
            End If
        End If

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjClaimRep may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjClaimRep = Nothing
%>
	</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("valclaimrep")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




