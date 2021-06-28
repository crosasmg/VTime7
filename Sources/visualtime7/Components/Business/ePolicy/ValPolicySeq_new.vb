Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Web

Public Class ValPolicySeq

    '%-------------------------------------------------------%'
    '% $Workfile:: ValPolicySeq.cls                         $%'
    '% $Author:: Ljimenez                                   $%'
    '% $Date:: 2-09-09 19:41                                $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    '- Se define la variable que contiene temporalmente el Nº de póliza en la CA001_K
    Public mlngPolicy As Double

    '- Descripción de la tabla de datos particulares
    Public mstrTabName As String

    '   Se define la variable que contiene la transacción que se ejecuta en cierto momento.
    Public Enum eFinanceTransac
        eftAddContrat = 1
        eftQuerycontrat = 2
        eftUpDateContrat = 3
        eftRecoveryContrat = 4
    End Enum

    Public sColtimre As String

    Public lstrCodispl As String

    Public mblnAmendment As Boolean
    Public mblnAcceptCont As Boolean
    Public lblnSituation As Boolean
    Public lblnGroups As Boolean
    Public mintMinDurIns As Short

    '+ Variables utilizadas para la ejecución de procesos de la forma CA041
    Public pstrType_prop As String '-(pstrType_prop): contiene si la póliza es normal o si es cobertura provisional.
    Public pdtmMaximum_da As Date '-(pdtmMaximum_da): contiene la fecha máxima de permanencia.

    Public dtmEffecdate As Date

    '- Se define la variable para almacenar la cadena que se quiera luego mostrar desde las páginas
    Public sString As String

    '- Se define la variable para almacenar si la transacción esta con o sin contenido
    Public sContent As String

    '-Variables para la validación de pago de primera prima
    Private mdblFirstPremium As Double
    Private mdblnPremprop As Double

    '-Variable que contiene el ramo técnico del producto
    Public sBrancht As String
    Public nProdClas As Double

    '-Variable que contiene ña fecha de inicio de la póliza
    Public nPolicy As Double
    Public nCertif As Double
    Public dStartdate As Date
    Public dExpirdat As Date
    Public nErrors As Integer
    Public nCapital As Double
    Public sErrors As String
    Public nTransactio As Integer
    '% insPostCA004: Se realiza la actualización de los datos
    Public Function insPostCA004(ByVal sHolder As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal nTransaction As Integer, ByVal sTitularC As String, ByVal sFreq As String, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal sIndexType As String, ByVal sIndexApl As String, ByVal sNoNull As String, ByVal dStartdate As Date, ByVal dExpirDate As Date, ByVal dIssuedat As Date, ByVal dReqDate As Date, ByVal nCopies As Integer, ByVal nIndexRate As Double, ByVal nDaysNull As Integer, ByVal sDeclarative As String, ByVal sFracti As String, ByVal sRenewalAut As String, ByVal sDirTyp As String, ByVal nWaypay As Integer, ByVal nBill_day As Integer, ByVal nCod_Agree As Integer, ByVal nUsercode As Integer, ByVal nSendAddr As Integer, ByVal sInsubank As String, ByVal sNopayroll As String, ByVal sExemption As String, Optional ByVal sLeg As String = "", Optional ByVal nDays_quot As Integer = 0, Optional ByVal sBill_Ind As String = "", Optional ByVal nDuration As Integer = 0, Optional ByVal nOrigin As Integer = 0, Optional ByVal nAFP_Commiss As Double = 0, Optional ByVal nAFP_Comm_Curr As Integer = 0, Optional ByVal sDirTyp_old As String = "", Optional ByVal nCollector As Integer = 0, Optional ByVal sFracReceip As String = "", Optional ByVal nGroup_agree As Integer = 0, Optional ByVal sCumul_code As String = "", Optional ByVal nRepInsured As Integer = 0, Optional ByVal sRetarif As String = "", Optional ByVal sReceipt_ind As String = "", Optional ByVal nTerm_grace As Integer = 0, Optional ByVal dTariffdate As Date = #12:00:00 AM#) As Boolean
        Dim lrecInsPostCA004 As eRemoteDB.Execute

        On Error GoTo InsPostCA004_Err

        lrecInsPostCA004 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'InsPostCA004'
        '+Información leída el 23/04/2003
        With lrecInsPostCA004
            .StoredProcedure = "insCA004PKG.inspostCA004"
            .Parameters.Add("sHolder", sHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTitularc", sTitularC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFreq", sFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndextype", sIndexType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndexapl", sIndexApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNonull", sNoNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdate", dExpirDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dReqdate", dReqDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCopies", nCopies, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndexrate", nIndexRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaysnull", nDaysNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDeclarative", sDeclarative, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFracti", sFracti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewalaut", sRenewalAut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp", sDirTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaypay", nWaypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSendaddr", nSendAddr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInsubank", sInsubank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNopayroll", sNopayroll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExemption", sExemption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeg", sLeg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDays_quot", nDays_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBill_ind", sBill_Ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_commiss", nAFP_Commiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_comm_curr", nAFP_Comm_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp_old", sDirTyp_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFracReceip", sFracReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_agree", nGroup_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCumul_code", sCumul_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRepInsured", nRepInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRetarif", IIf(sRetarif = "1", sRetarif, "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceipt_ind", sReceipt_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTerm_grace", nTerm_grace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dTariffdate", dTariffdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA004 = .Run(False)
        End With

InsPostCA004_Err:
        If Err.Number Then
            insPostCA004 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA004 = Nothing
        On Error GoTo 0
    End Function

    '% insPostCA003: Se realiza la actualización de los datos
    Public Function insPostCA003(ByVal Action As String, ByVal sClient As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nBankext As Integer, ByVal sAccount As String, ByVal nTyp_crecard As Integer, ByVal optBank As String, ByVal nUsercode As Integer, ByVal dCardExpir As Date, ByVal chkDeletDom As String, ByVal sBankauth As String, ByVal nTransaction As Integer, ByVal sCredi_card As String, ByVal nTyp_acc As Integer) As Boolean
        ''-------------------------------------------------------------------------------------------
        Dim lclsClient As eClient.Client
        Dim lclsDirDebit As ePolicy.DirDebit
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicyWin As ePolicy.Policy_Win

        On Error GoTo insPostCA003_Err

        lclsCertificat = New ePolicy.Certificat

        If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
            If lclsCertificat.sDirind = "1" Then
                insPostCA003 = True
            ElseIf lclsCertificat.sDirind = "2" Then

                lclsDirDebit = New ePolicy.DirDebit
                lclsClient = New eClient.Client
                lclsPolicy = New ePolicy.Policy

                sClient = lclsClient.ExpandCode(UCase(sClient))

                '+ Si existen mas de una poliza asignada al mandato de reutiliza
                If lclsDirDebit.valExistsBankAutPol(sCertype, nBranch, nProduct, nPolicy, nCertif, sBankauth, sClient, dEffecdate) Then
                    lclsDirDebit.sReuse = "1"
                Else
                    lclsDirDebit.sReuse = "2"
                End If

                With lclsDirDebit
                    .nProcess = 0
                    .sTransaction = CStr(nTransaction)
                    .sCertype = sCertype
                    .nBranch = nBranch
                    .nProduct = nProduct
                    .nPolicy = nPolicy
                    .nCertif = nCertif
                    .dEffecdate = dEffecdate
                    .sTyp_dirdeb = optBank
                    .sAccount = sAccount
                    .sCredi_card = sCredi_card
                    .nBankext = nBankext
                    .sClient = sClient
                    .nTyp_crecard = nTyp_crecard
                    .nUsercode = nUsercode
                    .dCardExpir = dCardExpir
                    .sBankauth = sBankauth
                    .nTyp_acc = nTyp_acc
                    insPostCA003 = .Add
                End With
                With lclsPolicy
                    If .Find(sCertype, nBranch, nProduct, nPolicy, True) Then
                        .sDirdebit = optBank

                        '+ Si se trata de una re-emisión o de re-impresión se cambia el estado de la póliza
                        If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                            .sStatus_pol = "3"
                        Else
                            If nTransaction = Constantes.PolTransac.clngReprint Then
                                .sStatus_pol = "4"
                            End If
                            .nDummy = 0
                            insPostCA003 = .Add
                        End If
                    Else
                        insPostCA003 = False
                    End If
                End With
            End If
        Else
            insPostCA003 = False
        End If

        If insPostCA003 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            With lclsPolicyWin
                insPostCA003 = .Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA003", "2")
            End With
        End If

insPostCA003_Err:
        If Err.Number Then
            insPostCA003 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsDirDebit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDirDebit = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '% insValCA003: Se realizan las validaciones de los campos de la forma CA003
    Public Function insValCA003(ByVal sCodispl As String, ByVal sBank As String, ByVal sClient As String, ByVal nBankext As Integer, ByVal sAccount As String, ByVal nTyp_Account As Integer, ByVal nTyp_crecard As Integer, ByVal dDateExpir As Date, ByVal sBankauth As String, ByVal sCred_card As String, ByVal nWay_pay As Integer, ByVal sDirind As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClient As eClient.Client
        Dim lclsDirDebit As ePolicy.DirDebit
        Dim lclsProduct As eProduct.Product
        Dim bPAC_TB As Boolean

        On Error GoTo insValCA003_Err

        lclsErrors = New eFunctions.Errors
        lclsClient = New eClient.Client

        bPAC_TB = (nWay_pay = Constantes.eWayPay.clngPayByPAC Or nWay_pay = Constantes.eWayPay.clngPayByTransBank)

        With lclsErrors
            '+ Si la vía de pago no es PAC/Transbank o es PAC/Transbank pero la vía de cobro es
            '+ por cliente, no se validan los campos de la ventana
            If Not bPAC_TB Or (bPAC_TB And sDirind = "1") Then
                Call .ErrorMessage(sCodispl, 56167)
            Else
                '+ Validacion del campo Cliente
                sClient = lclsClient.ExpandCode(UCase(sClient))
                If sClient = String.Empty Then
                    Call .ErrorMessage(sCodispl, 3342)
                ElseIf Not lclsClient.Find(sClient) Then
                    Call .ErrorMessage(sCodispl, 3343)
                End If

                If sBank = "1" Then
                    '+ Validando campo Banco
                    If nBankext = eRemoteDB.Constants.intNull Then
                        Call .ErrorMessage(sCodispl, 10828)
                    End If

                    '+ Validando campos para Nùmero de Cuenta
                    '+ Validando campo Numero de Cuenta
                    If sAccount = String.Empty Then
                        Call .ErrorMessage(sCodispl, 3058)
                    End If

                    If nTyp_Account = eRemoteDB.Constants.intNull Then
                        Call .ErrorMessage(sCodispl, 7030)
                    End If
                Else
                    '+ Validando campo Banco
                    If nBankext = eRemoteDB.Constants.intNull Then
                        Call .ErrorMessage(sCodispl, 10828)
                    End If

                    '+ Validando campos para Nùmero de tarjeta
                    '+ Validando Tipo de Tarjeta de Credito
                    If nTyp_crecard = eRemoteDB.Constants.intNull Then
                        Call .ErrorMessage(sCodispl, 3864)
                    End If

                    '+ Validando campo Nùmero de tarjeta
                    If sCred_card = String.Empty Then
                        Call .ErrorMessage(sCodispl, 3865)
                    End If

                    '+ Validando campo fecha de vencimiento
                    If IsDate(dDateExpir) And dDateExpir <> eRemoteDB.Constants.dtmNull Then

                        '+ La fecha de vencimiento debe ser mayor a la fecha del día
                        If dDateExpir <= Today Then
                            Call .ErrorMessage(sCodispl, 3937)
                        End If
                    Else

                        '+ La fecha de vencimiento debe estar llena
                        Call .ErrorMessage(sCodispl, 3876)
                    End If
                End If

                '+ Validando campo Numero de Mandato
                If sBankauth = String.Empty Then
                    Call .ErrorMessage(sCodispl, 55007)
                Else
                    lclsProduct = New eProduct.Product
                    Call lclsProduct.FindProdMaster(nBranch, nProduct)
                    If CStr(lclsProduct.sBrancht) <> "1" Then
                        lclsDirDebit = New ePolicy.DirDebit
                        If lclsDirDebit.Find_Valsbankaut(sCertype, nBranch, nProduct, nPolicy, nCertif, sBankauth) Then
                            Call .ErrorMessage(sCodispl, 750115)
                        End If
                        'UPGRADE_NOTE: Object lclsDirDebit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsDirDebit = Nothing
                    End If
                    'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsProduct = Nothing
                End If
            End If

            insValCA003 = .Confirm
        End With

insValCA003_Err:
        If Err.Number Then
            insValCA003 = "insValCA003" & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
    End Function

    '% insValCA004: se realizan las validaciones de los campos de la forma CA004
    Public Function insValCA004(ByVal sCodispl As String, ByVal dIssuedat As Date, ByVal dPropodat As Date, ByVal sClient As String, ByVal dExpirdat As Date, ByVal dStartdate As Date, ByVal sFreq As String, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal nIndexApl As Integer, ByVal nIndexType As Integer, ByVal nIndexRate As Double, ByVal nTransaction As Integer, ByVal nCertif As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sPoltype As String, ByVal dEffecdate As Date, ByVal nMainAction As Integer, ByVal nHolder As Integer, ByVal nCod_Agree As Integer, ByVal nWaypay As Integer, ByVal sDirTyp As String, ByVal nBill_day As Integer, ByVal nSendAddr As Integer, ByVal nOrigin As Integer, ByVal nAFP_Commiss As Double, ByVal nAFP_Comm_Curr As Integer) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA004 As eRemoteDB.Execute

        On Error GoTo insvalCA004_Err

        lrecinsvalCA004 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalCA004
            .StoredProcedure = "insCA004PKG.insvalCA004"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPropodat", dPropodat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFreq", sFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndexapl", nIndexApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndextype", nIndexType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndexrate", nIndexRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPoltype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHolder", nHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWaypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp", sDirTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSendaddr", nSendAddr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_commiss", nAFP_Commiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_comm_curr", nAFP_Comm_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValCA004 = .Confirm
        End With

insvalCA004_Err:
        If Err.Number Then
            insValCA004 = "insvalCA004: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA004 = Nothing
    End Function

    '% inspostCA006: ejecuta las acciones de actualización de la transacción
    Public Function inspostCA006(ByVal sCertype As String, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sColtimre As String, ByVal sColinvot As String, ByVal sColReint As String, ByVal nQ_Certif As Integer, ByVal nTariff As Integer, ByVal sTyp_Clause As String, ByVal sTyp_Discxp As String, ByVal sDocuTyp As String, ByVal sTyp_module As String, ByVal nTransactio As Integer, ByVal nClaim_notice As Integer, ByVal sColtpres As String, Optional ByVal sMassive As String = "", Optional ByVal sRepPrintCov As String = "") As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo inspostCA006_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insCA006PKG.inspostCA006"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColReint", sColReint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQ_certif", nQ_Certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Clause", sTyp_Clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Discxp", sTyp_Discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocuTyp", sDocuTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim_Notice", nClaim_notice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtpres", sColtpres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRepPrintCov", sRepPrintCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValid", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                inspostCA006 = .Parameters("nValid").Value = 1
            End If
        End With

inspostCA006_Err:
        If Err.Number Then
            inspostCA006 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '% insValCA011: Realiza la validación de los campos a actualizar en la ventana CA011
    Public Function insValCA011(ByVal lstrCodispl As String, ByVal sAction As String, ByVal nGroup As Integer, ByVal sDescription As String, ByVal nParticip As Double, ByVal sStatregt As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date) As String
        Dim lobjErrors As New eFunctions.Errors
        Dim lobjValues As New eFunctions.Values
        Dim lclsClaus As ePolicy.Claus_co_gp = New ePolicy.Claus_co_gp
        Dim lclsGroupss As ePolicy.Groupss = New ePolicy.Groupss
        Dim lintGroup As Integer

        On Error GoTo insValCA011_Err

        insValCA011 = String.Empty

        ' + Si el código de GRUPO está vacío o es NULO y existe infomación en los demás campos - error # 1084
        If (nGroup = eRemoteDB.Constants.intNull And ((sStatregt <> String.Empty And sStatregt <> "0") Or sDescription <> String.Empty Or nParticip <> eRemoteDB.Constants.intNull)) Then
            Call lobjErrors.ErrorMessage(lstrCodispl, 1084)
        End If

        ' + Si el código de GRUPO está vacío o es NULO se registra el error # 10152
        If nGroup = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(lstrCodispl, 10152)
            ' + Si el código de GRUPO es igual "0" se registra el error # 55876
        ElseIf nGroup = 0 Then
            Call lobjErrors.ErrorMessage(lstrCodispl, 55876)
        End If

        ' + Si se está eliminando no pueden existir registros asociados al certificado - Error # 3135
        If sAction = "Delete" Then
            If lclsClaus.FindGroupLinks(sCertype, nBranch, nPolicy, nProduct, nGroup) Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 3135)
            End If
        End If
        ' + Si se está registrando (añadiendo) un grupo, éste NO DEBE ESTAR REGISTRADO EN EL
        ' + ARCHIVO DE PÓLIZAS COLECTIVAS - ERROR 3133
        If sAction = "Add" Then
            If nGroup <> eRemoteDB.Constants.intNull Then
                If lclsGroupss.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nGroup) Then
                    Call lobjErrors.ErrorMessage(lstrCodispl, 3133)
                End If
            End If
        End If

        ' + Se muestra el siguiente error en caso que el GRUPO esté lleno y la DESCRIPCIÓN esté vacía
        ' + ARCHIVO DE PÓLIZAS COLECTIVAS - ERROR 3134
        If nGroup <> eRemoteDB.Constants.intNull Then
            If Trim(sDescription) = String.Empty Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 3134)
            End If
        End If
        If nGroup <> eRemoteDB.Constants.intNull Then
            If sStatregt = "0" Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 1016)
            Else
                If (sStatregt <> "0") And (sStatregt <> "") Then
                    sStatregt = CStr(CInt(sStatregt))
                End If
            End If
        End If

        insValCA011 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsClaus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClaus = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsGroupss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroupss = Nothing

insValCA011_Err:
        If Err.Number Then
            insValCA011 = "insValCA011: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insPostCA011: Se realiza la actualización de los datos
    Public Function insPostCA011(ByVal Action As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal sDescript As String, ByVal nParticip As Double, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nPriorGroup As Integer) As Boolean
        ''-------------------------------------------------------------------------------------------
        Dim lclsGroups As ePolicy.Groups = New ePolicy.Groups
        Dim lclsGroupsVal As ePolicy.Groups
        Dim lclsPolicy_Win As ePolicy.Policy_Win = New ePolicy.Policy_Win
        Dim lblnInd As Boolean

        lblnInd = False

        On Error GoTo insPostCA011_Err

        With lclsGroups
            .sCertype = sCertype
            .nBranch = nBranch
            .nPolicy = nPolicy
            .nProduct = nProduct
            .nGroup = nGroup
            .sDescript = sDescript
            .nParticip = nParticip
            .sStatregt = sStatregt
            .nUsercode = nUsercode
            .deffecdate = dEffecdate


            Select Case Action
                Case "Add"
                    '+Se verifica si este es el primer grupo que se crea a la poliza
                    lclsGroupsVal = New ePolicy.Groups
                    If Not lclsGroupsVal.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                        lblnInd = True
                    End If
                    'UPGRADE_NOTE: Object lclsGroupsVal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsGroupsVal = Nothing

                    insPostCA011 = .Add

                    If insPostCA011 Then
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA011", "2")

                        '+Si fue el primer grupo creado, se dejan los Datos de colectivo requerido de actualizar
                        If lblnInd Then
                            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI662", "1")
                        End If
                    End If

                Case "Update"
                    '+ Si el grupo que se está modificando anteriormente se encontraba en estado ACTIVO (1)
                    '+ no puede ser cambiado a EN PROCESO DE INSTALACIÓN (2) - ACM - 24/02/2001
                    If nGroup = CDbl("1") And sStatregt = "2" Then
                        insPostCA011 = True
                    Else
                        insPostCA011 = .Update
                    End If
                Case "Delete"
                    insPostCA011 = .Delete
                    If insPostCA011 Then
                        If Not lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA011", "1")
                        End If
                    End If
            End Select
        End With

insPostCA011_Err:
        If Err.Number Then
            insPostCA011 = False
        End If
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        'UPGRADE_NOTE: Object lclsGroupsVal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroupsVal = Nothing
        On Error GoTo 0
    End Function

    '%insValCA048: Valida la transaccion CA048
    Public Function insValCA048(ByVal sCodispl As String, ByVal sReverMod As String, ByVal nWait_code As Integer, ByVal nTransaction As Integer) As String
        Dim lclsError As eFunctions.Errors

        On Error GoTo insValCA048_Err
        lclsError = New eFunctions.Errors

        '+Si hay motivos para revertir la póliza, pero no se revierte
        '+se advierte que modificación no será exitosa
        If nWait_code <> eRemoteDB.Constants.intNull Then
            If sReverMod <> "1" And Trim(Str(nTransaction)) <> CStr(Constantes.PolTransac.clngPolicyPropAmendent) And Trim(Str(nTransaction)) <> CStr(Constantes.PolTransac.clngCertifPropAmendent) Then
                Call lclsError.ErrorMessage(sCodispl, 3912)
            End If
        End If

        insValCA048 = lclsError.Confirm

insValCA048_Err:
        If Err.Number Then
            insValCA048 = "insValCA048:" & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsError = Nothing
    End Function


    'insValCA050: Valida la transaccion CA050
    Public Function insValCA050(ByVal sCodispl As String, ByVal sDetailedEntryPrinted As String, ByVal nWait_code As Integer, ByVal nTransaction As Integer) As String
        Dim lclsError As eFunctions.Errors

        On Error GoTo insValCA050_Err
        lclsError = New eFunctions.Errors

        '+Si hay motivos para revertir la póliza, pero no se revierte
        '+se advierte que modificación no será exitosa
        If sDetailedEntryPrinted <> "1" And (nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPropQuotConvertion) And nWait_code = 4 Then
            Call lclsError.ErrorMessage(sCodispl, 197805)
        End If

        insValCA050 = lclsError.Confirm

insValCA050_Err:
        If Err.Number Then
            insValCA050 = "insValCA050:" & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsError = Nothing
    End Function

    '% valFieldDate: Esta función se encarga de validar los campo tipo fecha
    Public Function valFieldDate(ByVal sCodispl As String, ByVal dFieldDate As Date, ByRef lobjErrors As eFunctions.Errors, Optional ByVal nRequiredDate As Integer = 1012, Optional ByVal bShowErr As Boolean = True, Optional ByVal bLocate As Boolean = True, Optional ByVal sMessage As String = "") As Boolean
        valFieldDate = True
        If dFieldDate = eRemoteDB.Constants.dtmNull Then
            If bShowErr Then
                Call lobjErrors.ErrorMessage(sCodispl, nRequiredDate, , eFunctions.Errors.TextAlign.LeftAling, sMessage)
            End If
            valFieldDate = False
        ElseIf Not insvalDate(dFieldDate) Then
            If bShowErr Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 1001, , eFunctions.Errors.TextAlign.LeftAling, sMessage)
            End If
            valFieldDate = False
        Else
            valFieldDate = True
        End If
    End Function

    '% insvalDate: realiza la conversion de la fecha
    Public Function insvalDate(ByVal dFieldDate As Date) As Boolean
        Dim lTempDate As Date

        insvalDate = True

        On Error GoTo valDate_Err

        lTempDate = CDate(dFieldDate)

valDate_Err:
        If Err.Number Then
            insvalDate = False
        End If
        On Error GoTo 0
    End Function

    '% DateType_Amend: Crea fecha a partir del tipo de endoso
    Public Function DateType_Amend(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintPolicy As Integer, ByVal lintCertif As Integer, ByVal lintTypeIssue As Integer, ByVal lintServ_order As Double) As Boolean
        Dim lclsCertificat As Certificat
        Dim lclsProf_ord As Object

        DateType_Amend = True

        On Error GoTo DateType_Amend_Err

        If lintTypeIssue = 1 Then

            Me.dtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, DateSerial(Year(Today), Month(Today), 1))

        ElseIf lintTypeIssue = 2 Then
            lclsCertificat = New Certificat
            If lclsCertificat.Find(lstrCertype, lintBranch, lintProduct, lintPolicy, lintCertif) Then
                Me.dtmEffecdate = lclsCertificat.dNextReceip
            End If
            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing
        ElseIf lintTypeIssue = 3 Then
            lclsProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")
            If lclsProf_ord.Find_nServ(lintServ_order) Then
                Me.dtmEffecdate = lclsProf_ord.dMade_date
            End If
            'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsProf_ord = Nothing
        ElseIf lintTypeIssue = 4 Then

            Me.dtmEffecdate = DateSerial(Year(Today), Month(Today), VB.Day(Today))

        End If

DateType_Amend_Err:
        If Err.Number Then
            DateType_Amend = False
        End If
        On Error GoTo 0
    End Function

    '% insvalCA006: esta función realiza las validaciones de la Información General del Colectivo
    Public Function insvalCA006(ByVal sCertype As String, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCodispl As String, ByVal sColtimre As String, ByVal sColinvot As String, ByVal sColReint As String, ByVal sTyp_module As String, ByVal sTyp_Clause As String, ByVal sTyp_Discxp As String, ByVal sDocuTyp As String, ByVal nQ_Certif As Integer, ByVal dEffecdate As Date) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA006 As eRemoteDB.Execute

        On Error GoTo insvalCA006_Err

        lrecinsvalCA006 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalCA006
            .StoredProcedure = "insCA006PKG.insvalCA006"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColReint", sColReint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Clause", sTyp_Clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Discxp", sTyp_Discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocuTyp", sDocuTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQ_certif", nQ_Certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insvalCA006 = .Confirm
        End With

insvalCA006_Err:
        If Err.Number Then
            insvalCA006 = "insvalCA006: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA006 = Nothing
    End Function

    '%InsValAU001: Realiza la validación de los campos a actualizar
    'en la ventana de datos particulares del automovil (AU001)
    Public Function insValAU001(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nValGroup As Integer = 0, Optional ByVal nValSituation As Integer = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal sRegister As String = "", Optional ByVal sDigit As String = "", Optional ByVal sVehcode As String = "", Optional ByVal nVehType As Integer = 0, Optional ByVal nYear As Integer = 0, Optional ByVal sLicence As String = "", Optional ByVal sMotor As String = "", Optional ByVal sChassis As String = "", Optional ByVal nCapital As Double = 0, Optional ByVal nCollectedPrem As Double=0) As String
        Dim lclsErrors As eFunctions.Errors
        On Error GoTo InsValAU001_Err
        lclsErrors = New eFunctions.Errors
        Dim lstrErrors As String

        '+Validaciones que se realizan el la BD
        lstrErrors = insValAU001DB(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nValGroup, nValSituation, nTransaction, sRegister, sDigit, sVehcode, nVehType, nYear, sLicence, sMotor, sChassis, nCapital, nCollectedPrem)

        Call lclsErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)

        insValAU001 = lclsErrors.Confirm


InsValAU001_Err:
        If Err.Number Then
            insValAU001 = "InsValAU001: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
    '%InsValAU001DB: Llamado del procedure de la validación de los campos a actualizar en la
    '                ventana de datos particulares del automovil (AU001)
    Public Function insValAU001DB(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nValGroup As Integer, ByVal nValSituation As Integer, ByVal nTransaction As Integer, ByVal sRegister As String, ByVal sDigit As String, ByVal sVehcode As String, ByVal nVehType As Integer, ByVal nYear As Integer, ByVal sLicence As String, ByVal sMotor As String, ByVal sChassis As String, ByVal nCapital As Double, Optional ByVal nCollectedPrem As Double=0) As String
        Dim lrecInsValAU001 As eRemoteDB.Execute

        On Error GoTo InsValAU001DB_Err

        lrecInsValAU001 = New eRemoteDB.Execute

        With lrecInsValAU001
            .StoredProcedure = "InsValAU001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValGroup", nValGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValSituation", nValSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRegister", sRegister, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLicence", sLicence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCOLLECTEDPREMIUM", nCollectedPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insValAU001DB = .Parameters("Arrayerrors").Value
            End If
        End With

InsValAU001DB_Err:
        If Err.Number Then
            insValAU001DB = "InsValAU001DB: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValAU001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValAU001 = Nothing
        On Error GoTo 0
    End Function

    '%InsPostAU001: Se realiza la actualización de los datos en la ventana de datos particulares del automovil (AU001)
    Public Function insPostAU001(ByVal sCodispl As String, ByVal sPoltype As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal sLicense_ty As String = "", Optional ByVal sRegist As String = "", Optional ByVal sChassis As String = "", Optional ByVal sMotor As String = "", Optional ByVal sColor As String = "", Optional ByVal sVehcode As String = "", Optional ByVal nYear As Integer = 0, Optional ByVal nValGroup As Integer = 0, Optional ByVal nValSituation As Integer = 0, Optional ByVal nAutoZone As Double = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nTransactio As Integer = 0, Optional ByVal nVehplace As Integer = 0, Optional ByVal nVehpma As Integer = 0, Optional ByVal nDeduc As Double = 0, Optional ByVal dLastClaim As Date = #12:00:00 AM#, Optional ByVal nVestatus As Integer = 0, Optional ByVal nVehType As Integer = 0, Optional ByVal dStartdate As Date = #12:00:00 AM#, Optional ByVal nVeh_valor As Double = 0, Optional ByVal sDigit As String = "", Optional ByVal sRelapsing As String = "", Optional ByVal sN_infrac As String = "", Optional ByVal sReturn As String = "", Optional ByVal nLic_special As Integer = 0, Optional ByVal nCollectedPrem As Double=0) As Boolean
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsAuto_db As ePolicy.Auto_db
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicyWin As ePolicy.Policy_Win
        'Dim sLicense_ty_old   As String
        'Dim sRegist_old       As String

        lclsAuto = New ePolicy.Automobile
        lclsAuto_db = New ePolicy.Auto_db
        lclsPolicy = New ePolicy.Policy

        On Error GoTo insPostAU001_Err

        insPostAU001 = True

        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)

        Call lclsAuto.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

        '    sLicense_ty_old = lclsAuto.sLicense_ty
        '    sRegist_old = lclsAuto.sRegist
        '    If sLicense_ty_old <> String.Empty And _
        ''       sRegist_old <> String.Empty Then
        '           Call lclsAuto_db.insPostAU557(sRegist_old, sRegist, sLicense_ty_old, sLicense_ty, sDigit, nUsercode)
        '    End If

        With lclsAuto_db
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .sLicense_ty = sLicense_ty
            .sRegist = sRegist
            .sChassis = sChassis
            .sMotor = sMotor
            .sClient = lclsPolicy.SCLIENT
            .sColor = sColor
            .sVeh_own = lclsPolicy.SCLIENT
            .sVehcode = sVehcode
            .nVestatus = nVestatus
            '        .nNoteNum = nNoteNum
            .nUsercode = nUsercode
            .nYear = nYear
            .nVehType = nVehType
            '        .nAnualKm = nAnualKm
            '        .nActualKm = nActualKm
            '        .nKeepVeh = nKeepVeh
            '        .nRoadType = nRoadType
            '        .nIndLaw = nIndLaw
            '        .nFuelType = nFuelType
            '        .nIndAlarm = nIndAlarm
            .sDigit = sDigit
            .nLic_special = nLic_special

            If Not .Exist_db1(sLicense_ty, sRegist) Then
                Call .Add()
            Else
                Call .Update()
            End If
        End With

        With lclsAuto

            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nAutoZone = nAutoZone
            .sClient = lclsPolicy.SCLIENT
            .nVehType = nVehType
            .sChassis = sChassis
            .sColor = sColor
            .sLicense_ty = sLicense_ty
            .sMotor = sMotor
            .sRegist = sRegist
            .sVehcode = sVehcode
            .dExpirdat = lclsAuto.dExpirdat
            .dIssuedat = lclsAuto.dIssuedat
            .dNulldate = lclsAuto.dNulldate
            .dStartdate = dStartdate
            .nCapital = nCapital
            .nPremium = lclsAuto.nPremium
            .nVeh_valor = nVeh_valor
            .nVal_extra = lclsAuto.nVal_extra
            .nTransactio = nTransactio
            .nNullcode = IIf(lclsAuto.nNullcode <= 0, eRemoteDB.Constants.intNull, lclsAuto.nNullcode)
            .nUsercode = nUsercode
            .nVehplace = nVehplace
            .nVehpma = nVehpma
            .nYear = nYear
            .nInd0km = lclsAuto.nInd0km
            .sReference = lclsAuto.sReference
            .nValueType = lclsAuto.nValueType
            .nDiscClaim = lclsAuto.nDiscClaim
            .nDeduc = nDeduc
            .nUse = lclsAuto.nUse
            .nPercTabVal = lclsAuto.nPercTabVal
            .nGroup = nValGroup
            .dLastClaim = dLastClaim
            .nSituation = nValSituation
            .sDigit = sDigit
            .sRelapsing = sRelapsing
            .sN_infrac = sN_infrac
            .sPromotion = lclsAuto.sPromotion
            .sReturn = sReturn
            .nLic_special = nLic_special
			.nCollectedPrem = nCollectedPrem

            If Not lclsAuto.Update Then
                insPostAU001 = False
            Else
                lclsPolicyWin = New ePolicy.Policy_Win
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AU001", "2")
            End If
        End With

insPostAU001_Err:
        If Err.Number Then
            insPostAU001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAuto = Nothing
        'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAuto_db = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '%insPostCA001: Esta función se encarga de actualizar los datos introducidos en la zona de
    '%              cabecera.
    Public Function insPostCA001(ByVal sCertype As String, ByVal nTransaction As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicydest As Double, ByVal nCertif As Double, ByVal sBussityp As String, ByVal sPoliTyp As String, ByVal dLedgerDate As Date, ByVal nUsercode As Integer, ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByVal nSellChannel As Integer, ByVal nType_amend As Integer, ByVal nServ_order As Double, ByVal dFer As Date, ByVal nQuotProp As Integer, ByVal nDigit As Integer, ByVal nProp_reg As Integer, ByVal nRenewalnum As Integer, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal dLastChange As Date = #12:00:00 AM#, Optional ByVal sCodispl As String = "", Optional ByVal sClient As String = "", Optional ByVal nFolio As Double = 0) As Boolean

        '+ Se realiza el llamado a la función que realiza las actualizaciones de las tablas
        insPostCA001 = InsTransaction(CInt(nTransaction), sCertype, nBranch, nProduct, nPolicy, nPolicydest, nCertif, dEffecdate, nOffice, sBussityp, sPoliTyp, dLedgerDate, dNulldate, dLastChange, nAgency, nOfficeAgen, nSellChannel, nType_amend, nServ_order, dFer, nQuotProp, nUsercode, nDigit, nProp_reg, nRenewalnum, sCodispl, sClient, nFolio)
    End Function

    '%InsTransaction: Esta rutina se encarga de actualizar las tablas necesarias
    '%según el tipo de transacción que el usuario haya seleccionado.
    Public Function InsTransaction(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicydest As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal sBussityp As String, ByVal sPolitype As String, ByVal dLedgerDate As Date, ByVal dNulldate As Date, ByVal dLastChange As Date, ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByVal nSellChannel As Integer, ByVal nType_amend As Integer, ByVal nServ_order As Double, ByVal dFer As Date, ByVal nQuotProp As Integer, ByVal nUsercode As Integer, ByVal nDigit As Integer, ByVal nProp_reg As Integer, ByVal nRenewalnum As Integer, ByVal sCodispl As String, Optional ByVal sClient As String = "", Optional ByVal nFolio As Double = 0) As Boolean
        Dim lrecInsTransaction As eRemoteDB.Execute
        '+Definición de parámetros para stored procedure 'InsTransaction'
        '+Información leída el 07/04/2003
        On Error GoTo InsTransaction_Err
        lrecInsTransaction = New eRemoteDB.Execute
        With lrecInsTransaction
            .StoredProcedure = "InsCA001PKG.InsTransaction"
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicydest", nPolicydest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLastchange", dLastChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSellchannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFer", dFer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPol_quot", nQuotProp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProp_reg", nProp_reg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRenewalnum", nRenewalnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolio", nFolio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nTransactio = .FieldToClass("nTransactio")
                Me.sBrancht = .FieldToClass("sBrancht")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.dStartdate = .FieldToClass("dStartdate")
                Me.dExpirdat = .FieldToClass("dExpirdat")
                Me.sString = .FieldToClass("sInvalid")
                Me.nProdClas = .FieldToClass("nProdclas")
                InsTransaction = True
                .RCloseRec()
            End If
        End With
InsTransaction_Err:
        If Err.Number Then
            InsTransaction = False
        End If
        'UPGRADE_NOTE: Object lrecInsTransaction may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsTransaction = Nothing
        On Error GoTo 0
    End Function

    '% insPolicy_Ca001: actualiza la tabla Policy
    Private Function insPolicy_CA001(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal sBussityp As String, ByVal sPolitype As String, ByVal dNulldate As Date, ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByRef lclsPolicy As Policy) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lblnPolicy_Find As Boolean

        On Error GoTo insPolicy_Ca001_Err

        '+Esta variable no se debe setear con Nothing. Milko
        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                lblnPolicy_Find = True
            Else
                lblnPolicy_Find = False
            End If
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nUsercode = nUsercode

            '+Si se trata de la emision de una cotización o solicitud se guarda el número
            '+en otro campo
            If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Then
                .sStatus_pol = "3"
                .sPropo_cert = sCertype
                .sType_prop = "1"
            End If
            If nTransaction = Constantes.PolTransac.clngQuotationConvertion Or nTransaction = Constantes.PolTransac.clngProposalConvertion Then
                .nProponum = nPolicy
            End If

            '+Se asigna el valor del usuario de la modificación
            If nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Then
                If nCertif = 0 Then
                    .nUser_amend = nUsercode
                Else
                    .nUser_amend = eRemoteDB.Constants.intNull
                End If
            Else
                .nUser_amend = eRemoteDB.Constants.intNull
            End If

            '+Asignación del parámetro estado de la póliza como en captura imcompleta sólo si se está emitiendo
            .sStatus_pol = IIf(nTransaction <> Constantes.PolTransac.clngPolicyIssue, .sStatus_pol, "3")

            '+ Se asigna la fecha de efecto solo cuando corresponda a una emisión
            If nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifQuotRenewal Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal Or nTransaction = Constantes.PolTransac.clngCertifPropRenewal Then
                .dDate_Origi = dEffecdate
                .sDirdebit = String.Empty
                .dStartdate = dEffecdate
                .sInd_Comm = "1"
                .sConColl = "2"
                '.sCommityp = "1"
            End If

            '+ Si la transación es Emisión, Cotización o Propuesta de póliza o certificado
            If nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Then

                '+ Se recupera la informacion de product si
                lclsProduct = New eProduct.Product
                If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                    If (nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPolicyQuotation) Then
                        If Not lblnPolicy_Find Then
                            .sIndextyp = lclsProduct.sRevaltyp
                            .sRenewal = lclsProduct.sRenewal
                            .sRevalapl = lclsProduct.sRevalapl
                            .sTyp_Clause = lclsProduct.sTyp_clause
                            .sTyp_Discxp = lclsProduct.sTyp_discxp
                            .sTyp_module = lclsProduct.sTyp_module
                            .nIndexfac = lclsProduct.nRevalrat
                            .nPayfreq = lclsProduct.nPayFreq
                            .nCopies = lclsProduct.nCopies
                            .nNotice = lclsProduct.nCancnoti
                        End If
                    Else
                        .sIndextyp = lclsProduct.sRevaltyp
                        .sRenewal = lclsProduct.sRenewal
                        .sRevalapl = lclsProduct.sRevalapl
                        .sTyp_Clause = lclsProduct.sTyp_clause
                        .sTyp_Discxp = lclsProduct.sTyp_discxp
                        .sTyp_module = lclsProduct.sTyp_module
                        .nIndexfac = lclsProduct.nRevalrat
                        .nPayfreq = lclsProduct.nPayFreq
                        .nCopies = lclsProduct.nCopies
                        .nNotice = lclsProduct.nCancnoti
                    End If
                    '+Se Recupera el ramo tecnico de la transación
                    'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsProduct = Nothing
                End If
            End If

            '+Asignación del parámetro Sucursal
            .nOffice = nOffice

            '+Asignación del parámetro Propietaria
            .nOffice_own = nOffice

            '+Asignación del parámetro tipo de póliza
            .sBussityp = sBussityp

            '+Asignación del parámetro tipo de póliza
            .sPolitype = sPolitype

            '+Asignación del parámetro de número de la Agencia
            .nAgency = nAgency

            '+Asignación del parámetro de número de la Oficina
            .nOfficeAgen = nOfficeAgen

            '+Se inicializa el campo 'sAccounti' que indica si la poliza tiene asociada una cuenta corriente
            .sAccounti = "2"

            '+Se inicializa el campo 'sCoinsuri' que indica la existencia de coaseguro cedido
            .sCoinsuri = "2"

            '+Se inicializa el Código de Anulación con ceros
            .nNullcode = eRemoteDB.Constants.intNull

            '+Se inicializa el campo 'sSubstiti' que indica si la poliza sustituye a otra
            .sSubstiti = CStr(2)

            '+Se inicializa el tipo de recibo
            If .sPolitype = "1" Then
                .sColinvot = "2"
            End If

            .nLast_certi = nCertif

            '+ Si se trata de una re-emisión o de re-impresión se cambia el estado de la póliza

            If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                If nTransaction = Constantes.PolTransac.clngPolicyReissue Then
                    .sStatus_pol = "3"
                End If
                .dDate_Origi = dEffecdate
                .dStartdate = dEffecdate
                .DISSUEDAT = Today
            Else
                If nTransaction = Constantes.PolTransac.clngReprint Then
                    .sStatus_pol = "4"
                End If
            End If
            .nDummy = 0

            lclsProduct = New eProduct.Product

            If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate, True) Then
                If lclsProduct.nProdClas <> 4 Then
                    .sCurrAcc = "1"
                Else
                    .sCurrAcc = "4"
                End If

                'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsProduct = Nothing
            End If

            insPolicy_CA001 = .Add

        End With

insPolicy_Ca001_Err:
        If Err.Number Then
            insPolicy_CA001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '%insCertificat_ca001: Esta rutina se encarga de realizar la actualización en la
    '%                     tabla 'certificat'
    Private Function InsCertificat_CA001(ByVal nTransaction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal dEffecdate As String, ByVal nSellChannel As Integer, ByVal dFer As Date, ByVal nPol_quot As Double, ByVal nDigit As Integer, ByVal nProp_reg As Integer, ByVal nRenewalnum As Integer) As Boolean
        Dim lclsCertificat As Certificat

        '- Variable de tipo booleana que especifica si es la primera vez que se ejecuta esta función
        Dim lblnFirstTime As Boolean

        On Error GoTo InsCertificat_Ca001_Err

        lclsCertificat = New Certificat
        With lclsCertificat
            lblnFirstTime = Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nUsercode = nUsercode
            .nDigit = nDigit
            .nProp_reg = nProp_reg
            .nRenewalnum = nRenewalnum
            '+ Si se trata de la emision de una cotización o solicitud se guarda el número
            '+ en otro campo
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyProposal) Or nTransaction = CStr(Constantes.PolTransac.clngCertifProposal) Then
                If nTransaction = CStr(Constantes.PolTransac.clngQuotationConvertion) Or nTransaction = CStr(Constantes.PolTransac.clngProposalConvertion) Then
                    .nProponum = nPolicy
                End If
                .nStatquota = 1
            End If

            '+Asignación del parámetro fecha de efecto
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngCertifAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngTempPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngTempCertifAmendment) Then
                .dChangdat = CDate(dEffecdate)
            End If

            '+Se asigna el valor del usuario de la modificación
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngCertifAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngTempPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngTempCertifAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngRecuperation) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyIssue) Or nTransaction = CStr(Constantes.PolTransac.clngCertifIssue) Or nTransaction = CStr(Constantes.PolTransac.clngCertifProposal) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyProposal) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotation) Then
                .nUser_amend = nUsercode
            Else
                .nUser_amend = eRemoteDB.Constants.intNull
            End If

            '+Asignación del parámetro fecha de efecto de la póliza
            '+Se asigna la fecha de efecto solo cuando corresponda a una emisión
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyIssue) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyProposal) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngCertifIssue) Or nTransaction = CStr(Constantes.PolTransac.clngCertifProposal) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotation) Then
                .dStartdate = CDate(dEffecdate)
                .dChangdat = CDate(dEffecdate)
                .dDate_Origi = CDate(dEffecdate)
            End If

            '+ Si corresponde a un re-emisión
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyReissue) Or nTransaction = CStr(Constantes.PolTransac.clngCertifReissue) Then
                .dDate_Origi = CDate(dEffecdate)
                .dChangdat = CDate(dEffecdate)
                .dIssuedat = Today
                .dStartdate = CDate(dEffecdate)
            End If

            '+Asignación del parámetro estado del certificado
            .sStatusva = "3"

            '+Asignación del parámetro Indicador de reaseguro
            .sReinsura = "2"

            '+Se inicializa el Código de Anulación con ceros
            .nNullcode = eRemoteDB.Constants.intNull

            '+Se inicializa el Código del canal de venta
            .nSellChannel = nSellChannel

            '+Se inicializa el Código del canal de venta
            .dFer = dFer
            '+ Se asigna el valor a la póliza asociada a la cotización en tratamiento
            .nPol_quot = nPol_quot
            '+ Si se trata de una Cotización/propuesta de poliza/certificado
            '+ propuesta de modificación/renovacion de poliza/certificado,
            '+ cotización de modificacion/renocación de poliza certificado
            '+ la causa de estado pendiente "nwait_code" debe ser 1
            If nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotation) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyProposal) Or nTransaction = CStr(Constantes.PolTransac.clngCertifProposal) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotAmendent) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotAmendent) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyPropAmendent) Or nTransaction = CStr(Constantes.PolTransac.clngCertifPropAmendent) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyQuotRenewal) Or nTransaction = CStr(Constantes.PolTransac.clngCertifQuotRenewal) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyPropRenewal) Or nTransaction = CStr(Constantes.PolTransac.clngCertifPropRenewal) Then
                .nWait_code = 1
            End If
            If lblnFirstTime Then
                InsCertificat_CA001 = .Add
            Else
                InsCertificat_CA001 = .Update
            End If
        End With

InsCertificat_Ca001_Err:
        If Err.Number Then
            InsCertificat_CA001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '%insParticularData: Se actualiza el archivo de datos particulares con los valores globales
    '%de la póliza.
    Public Function insParticularData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As String, ByVal dLastChange As Date, ByVal dNulldate As Date) As Boolean
        Dim lblnExist As Boolean
        Dim lclsCertificat As Certificat
        Dim lclsPolicy As Policy

        On Error GoTo insParticularData_Err
        lclsCertificat = New Certificat
        insParticularData = True
        If lclsCertificat.Find_ParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            lblnExist = True
            mstrTabName = lclsCertificat.sTabname
        Else
            lblnExist = False
        End If

        If Not lblnExist Then
            lclsPolicy = New Policy
            If lclsPolicy.Find_TabNameB(nBranch) Then
                mstrTabName = lclsPolicy.sTabname
            End If
            'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsPolicy = Nothing
        End If

        If Not lblnExist And mstrTabName <> String.Empty Then
            If Not lclsCertificat.Add_ParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, mstrTabName) Then
                insParticularData = False
            End If
        Else
            If nTransaction = CStr(Constantes.PolTransac.clngCertifReissue) Or nTransaction = CStr(Constantes.PolTransac.clngPolicyReissue) Then
                If Not lclsCertificat.AddUpdParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, nUsercode, mstrTabName, 18) Then
                    insParticularData = False
                End If
            Else
                If Format(dLastChange, "yyyyMMdd") < Format(dEffecdate, "yyyyMMdd") Then
                    If nTransaction = CStr(Constantes.PolTransac.clngPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngCertifAmendment) Then
                        If Not lclsCertificat.AddUpdParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsCertificat.dEffecdate, dEffecdate, eRemoteDB.Constants.dtmNull, nUsercode, mstrTabName, 12) Then
                            insParticularData = False
                            Exit Function
                        End If
                    Else
                        If nTransaction = CStr(Constantes.PolTransac.clngTempPolicyAmendment) Or nTransaction = CStr(Constantes.PolTransac.clngTempCertifAmendment) Then
                            If Format(dLastChange, "yyyyMMdd") < Format(dEffecdate, "yyyyMMdd") Then
                                If Not lclsCertificat.AddUpdParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsCertificat.dEffecdate, dEffecdate, dNulldate, nUsercode, mstrTabName, 13) Then
                                    insParticularData = False
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

insParticularData_Err:
        If Err.Number Then
            insParticularData = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% insConvertion2: Convierte las solicitudes a cotizaciones de póliza
    Private Function insConvertion2(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sPolitype As String) As Boolean
        Dim nPolicyNum As Double
        Dim lclsNumerator As eGeneral.GeneralFunction
        Dim lclsOptSystem As eGeneral.Opt_system
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsConvert As ePolicy.Policy

        On Error GoTo insConvertion2_Err

        lclsNumerator = New eGeneral.GeneralFunction
        lclsOptSystem = New eGeneral.Opt_system
        lclsConvert = New ePolicy.Policy

        Call lclsOptSystem.find()
        With lclsConvert
            .sCertype = "3"
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode
            .sPolitype = sPolitype
            Select Case lclsOptSystem.sPolicyNum
                '-Numeración general
                Case "1"
                    nPolicyNum = lclsNumerator.Find_Numerator(1, 0, nUsercode, "1", nBranch, nProduct)

                    '-Numeración por ramo
                Case "2"
                    nPolicyNum = lclsNumerator.Find_Numerator(1, nBranch, nUsercode, "1", nBranch, nProduct)

                    '-Si no coincide el numerador se numera general
                Case Else
                    nPolicyNum = lclsNumerator.Find_Numerator(1, 0, nUsercode, "1", nBranch, nProduct)
            End Select
            .nNewPolicy = nPolicyNum
            insConvertion2 = .ConvertToCotizac
        End With

        If insConvertion2 Then
            lclsPolicy = New ePolicy.Policy
            If lclsPolicy.Find("1", nBranch, nProduct, nPolicyNum) Then
                lclsPolicy.pdtmLastChange = lclsPolicy.dChangdat
                mlngPolicy = nPolicyNum
            Else
                insConvertion2 = False
            End If
        End If

insConvertion2_Err:
        If Err.Number Then
            insConvertion2 = False
        End If
        'UPGRADE_NOTE: Object lclsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsNumerator = Nothing
        'UPGRADE_NOTE: Object lclsOptSystem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsOptSystem = Nothing
        'UPGRADE_NOTE: Object lclsConvert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsConvert = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        On Error GoTo 0
    End Function

    '% insreaParticularTable: lee el nombre de la tabla particular de la póliza
    Public Function insreaParticularTable(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Date) As Boolean
        Dim lclsCertificat As Certificat
        Dim lclsPolicy As Policy

        On Error GoTo insreaParticularTable_Err
        lclsCertificat = New Certificat
        If lclsCertificat.Find_ParticularData(lstrCertype, lintBranch, lintProduct, llngPolicy, llngCertif, ldtmEffecdate) Then
            mstrTabName = lclsCertificat.sTabname
            insreaParticularTable = True
        End If

        If mstrTabName = String.Empty Then
            lclsPolicy = New Policy
            If lclsPolicy.Find_TabNameB(lintBranch) Then
                mstrTabName = lclsPolicy.sTabname
                insreaParticularTable = True
            End If
        End If

insreaParticularTable_Err:
        If Err.Number Then
            insreaParticularTable = False
        End If
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        On Error GoTo 0
    End Function

    '% insValCA026: se realizan las validaciones de los campos de la forma CA003
    Public Function insValCA026(ByVal nTransaction As Integer, ByVal lstrCodispl As String, ByVal dEffecdate As Date, ByVal dExpDate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal lobjErrorsCA001 As Object) As String
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lobjValues As eFunctions.Values
        Dim lclsValid As eFunctions.valField
        Dim lobjErrors As Object

        lclsCertificat = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy
        lobjValues = New eFunctions.Values
        lclsValid = New eFunctions.valField
        lobjErrors = lobjErrorsCA001

        On Error GoTo insValCA026_Err

        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
        Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

        If dExpDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(lstrCodispl, 1012, , , HttpContext.GetGlobalResourceObject("BackOfficeResource", "DateTitle") & ": ")
        Else
            If lclsValid.ValDate(dExpDate, , eFunctions.valField.eTypeValField.onlyvalid) Then
                If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                    If dExpDate <= dEffecdate Then
                        Call lobjErrors.ErrorMessage(lstrCodispl, 3059)
                    End If
                End If

                If nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Then
                    If dExpDate > lclsPolicy.DEXPIRDAT Then
                        Call lobjErrors.ErrorMessage(lstrCodispl, 3263)
                    End If
                End If

                If nTransaction = Constantes.PolTransac.clngTempCertifAmendment Then
                    If dExpDate > lclsCertificat.dExpirdat Then
                        Call lobjErrors.ErrorMessage(lstrCodispl, 3265)
                    End If
                End If
            End If
        End If

        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsValid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValid = Nothing
        'UPGRADE_NOTE: Object lobjErrorsCA001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrorsCA001 = Nothing

insValCA026_Err:
        If Err.Number Then
            insValCA026 = insValCA026 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insValCA017A: Validación Cuotas del Recibo
    Public Function insValCA017A(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nWay_pay As Integer, ByVal nPremium As Double, ByVal nQuota As Integer, ByVal nQuoPend As Integer, ByVal nValQuot As Double, ByVal nInitial As Double, ByVal nPayfreq As Integer, ByVal nTransaction As Integer, ByVal nContrat As Integer, ByVal nInterest As Double, ByVal nUsercode As Integer, ByVal nPendAmount As Double) As String
        Dim lrecInsValCA017a As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrErrors As String

        On Error GoTo InsValCA017aDB_Err

        lrecInsValCA017a = New eRemoteDB.Execute
        lobjErrors = New eFunctions.Errors

        With lrecInsValCA017a
            .StoredProcedure = "InsValCA017a"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuoPend", nQuoPend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValQuot", nValQuot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPendAmount", nPendAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrors = .Parameters("Arrayerrors").Value
            End If

            Call lobjErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)

            insValCA017A = lobjErrors.Confirm

        End With

InsValCA017aDB_Err:
        If Err.Number Then
            insValCA017A = "InsValCA017: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA017a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA017a = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        On Error GoTo 0
    End Function

    'insValCA041: Esta función realiza las validaciones de la Información General del Colectivo
    Public Function insValCA041(ByVal sCodispl As String, ByVal sSel As String, ByVal ncount As Integer, ByVal sExchange As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sCurrency As String, ByVal nCertif As Double, ByVal dEffecdate As Date) As String

        Dim lclsProduct As eProduct.Product
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsPolicy As ePolicy.Policy
        Dim lblnError As Boolean

        lclsProduct = New eProduct.Product
        lobjErrors = New eFunctions.Errors
        lclsPolicyWin = New ePolicy.Policy_Win
        lclsPolicy = New ePolicy.Policy

        Dim lintCurrency As Integer
        Dim lintSel As Integer
        Dim bLast As Boolean

        On Error GoTo insValCA041_Err

        bLast = False
        '+ Se realizan las validaciones sobre el grid
        Do While Len(sSel) > 0 And Not bLast
            '+ Se descomponen los datos, cada uno viene en una cadena de valores separados por coma.
            If InStr(1, sSel, ",") > 0 Then
                lintSel = CInt(Mid(sSel, 1, InStr(1, sSel, ",") - 1))
                lintCurrency = CInt(Mid(sCurrency, 1, InStr(1, sCurrency, ",") - 1))

                sSel = Trim(Mid(sSel, InStr(1, sSel, ",") + 1))
                sCurrency = Trim(Mid(sCurrency, InStr(1, sCurrency, ",") + 1))
                sExchange = Trim(Mid(sExchange, InStr(1, sExchange, ",") + 1))
            Else
                '+ Se trata al último elemento de la cadena o la cadena traía sólo un elemento
                lintSel = CInt(sSel)
                lintCurrency = CInt(sCurrency)
                bLast = True
            End If

            '+ Se valida que no exista cobertura ni recibo asociado a la póliza/certif con la moneda si
            '+ esta se eliminó
            If lintSel = eRemoteDB.Constants.intNull Or lintSel = 2 Then
                lblnError = False

                If insReaCover_curr(sCertype, nBranch, nProduct, nPolicy, lintCurrency) Then
                    lblnError = True
                ElseIf insReaPremium_curr(lintCurrency, sCertype, nBranch, nProduct, nPolicy) Then
                    lblnError = True
                End If

                If lblnError = True Then
                    Call lobjErrors.ErrorMessage(lstrCodispl, 3325)
                End If
            End If
        Loop

        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
        '+ Se valida que el número de monedas seleccionadas no exceda al máximo permitido
        If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
            If ncount > lclsProduct.nQmaxcurr And (lclsPolicy.sPolitype = "1" Or nCertif <> 0) Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 11208)
            ElseIf ncount = 0 Then
                '+ Debe haber al menos una moneda seleccionada
                Call lobjErrors.ErrorMessage(lstrCodispl, 12125)
            End If
        End If

        insValCA041 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValCA041_Err:

        If Err.Number Then
            insValCA041 = insValCA041 & Err.Description
        End If

        On Error GoTo 0
    End Function

    '%insReaCover_curr: Lee la moneda de las coberturas de la poliza
    Public Function insReaCover_curr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCover As ePolicy.Cover

        lclsPolicy = New ePolicy.Policy
        lclsCover = New ePolicy.Cover

        insReaCover_curr = True

        On Error GoTo insReaCover_curr_Err

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            If Not lclsCover.LoadCurr(lclsPolicy.sCertype, lclsPolicy.nBranch, lclsPolicy.nProduct, lclsPolicy.nPolicy, lclsPolicy.nCertif, lclsPolicy.dStartdate, nCurrency, lclsPolicy.sTyp_module, lclsPolicy.sPolitype) Then
                insReaCover_curr = False
            End If
        End If

insReaCover_curr_Err:
        If Err.Number Then
            insReaCover_curr = False
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover = Nothing

    End Function

    '%insReaPremium_curr: Permite leer las monedas utilizadas por los recibos
    Public Function insReaPremium_curr(ByVal nCurrency As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPremium As eCollection.Premium

        lclsPolicy = New ePolicy.Policy
        lclsPremium = New eCollection.Premium

        On Error GoTo insReaPremium_curr_Err

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then

            insReaPremium_curr = True

            If Not lclsPremium.LoadCurr(sCertype, nBranch, nProduct, nPolicy, nCurrency) Then
                insReaPremium_curr = False
            End If
        End If

insReaPremium_curr_Err:
        If Err.Number Then
            insReaPremium_curr = False
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing

    End Function

    '% InsVAlIN010: Realiza la validación de los campos a actualizar en la ventana IN010
    Public Function InsValIN010(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nArticle As Integer = 0, Optional ByVal nDetailArt As Integer = 0, Optional ByVal nConstCat As Integer = 0, Optional ByVal nFloor_quan As Integer = 0, Optional ByVal nIndPeriod As Integer = 0, Optional ByVal nDep_prem As Double = 0, Optional ByVal nDecla_freq As Integer = 0, Optional ByVal nDecla_type As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValNum As eFunctions.valField
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo InsValIN010_Err
        lobjErrors = New eFunctions.Errors
        lobjValNum = New eFunctions.valField
        lclsPolicy = New ePolicy.Policy

        '+Se verifica que el campo Actividad tenga un valor y  que sea válido.

        If nArticle = 0 Or nArticle = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 2005)
        End If

        '+Se verifica el Detalle de Actividad
        If nDetailArt = 0 Or nDetailArt = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3873)
        End If

        '+Se verifica la Categoría de la Construcción
        If nConstCat = eRemoteDB.Constants.intNull Or nConstCat = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3505)
        End If

        '+Se verifica que el campo Número de Plantas tenga un valor y sea numérico.
        If nFloor_quan = eRemoteDB.Constants.intNull Or nFloor_quan = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3227)
        End If

        lobjValNum.objErr = lobjErrors

        If nFloor_quan <> eRemoteDB.Constants.intNull And nFloor_quan <> 0 Then
            nFloor_quan = lobjValNum.ValNumber(nFloor_quan)
            nFloor_quan = lobjValNum.Value
        End If

        '+Se verifica que el campo Período de Indemnización sea numérico.
        If nIndPeriod <> eRemoteDB.Constants.intNull And nIndPeriod <> 0 Then
            nIndPeriod = lobjValNum.ValNumber(nIndPeriod)
            nIndPeriod = lobjValNum.Value
        End If

        '+Se verifica que el campo % Prima de Depósito tenga un valor y sea numérico.
        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)

        If lclsPolicy.sDeclari = "1" Then
            If nDep_prem = 0 Or nDep_prem = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 38050)
            End If

            '+Se verifica el campo frecuencia de declaración
            If nDecla_freq = eRemoteDB.Constants.intNull Or nDecla_freq = 0 Then
                Call lobjErrors.ErrorMessage(sCodispl, 3217)
            End If

            '+Se verifica el campo tipo de declaración
            If nDecla_type = eRemoteDB.Constants.intNull Or nDecla_type = 0 Then
                Call lobjErrors.ErrorMessage(sCodispl, 3241)
            End If
        End If

        InsValIN010 = lobjErrors.Confirm

InsValIN010_Err:
        If Err.Number Then
            InsValIN010 = InsValIN010 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValNum = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '% insPostIN010: Se realiza la actualización de los datos en la ventana IN010
    Public Function InsPostIN010(ByVal sAction As String, ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nArticle As Integer = 0, Optional ByVal nDetailArt As Integer = 0, Optional ByVal nConstCat As Integer = 0, Optional ByVal nFloor_quan As Integer = 0, Optional ByVal nSpCombType As Integer = 0, Optional ByVal nSideCloseType As Integer = 0, Optional ByVal nIndPeriod As Integer = 0, Optional ByVal nRoofType As Integer = 0, Optional ByVal nBuildType As Integer = 0, Optional ByVal nSeismicZone As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal nTransactio As Integer = 0, Optional ByVal nFamily As Integer = 0, Optional ByVal nActivityType As Integer = 0, Optional ByVal nDep_prem As Double = 0, Optional ByVal sDecla_freq As String = "", Optional ByVal sDecla_type As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lclsFire As ePolicy.Fire
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lobjValues As eFunctions.Values

        On Error GoTo InsPostIN010_Err

        lclsFire = New ePolicy.Fire
        lclsPolicy = New ePolicy.Policy
        lclsPolicyWin = New ePolicy.Policy_Win
        lobjValues = New eFunctions.Values
        InsPostIN010 = True

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            sClient = lclsPolicy.SCLIENT
            nTransactio = lclsPolicy.NTRANSACTIO
        Else
            sClient = String.Empty
        End If

        If lclsFire.Find_DetArt(nArticle, nDetailArt, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            nActivityType = lclsFire.nActivityType
            If lclsFire.nActivityType = eRemoteDB.Constants.intNull Then
                nFamily = lclsFire.nFamily
            Else
                nFamily = eRemoteDB.Constants.intNull
            End If
        End If

        With lclsFire
            .sCertype = sCertype
            .sCodispl = sCodispl
            .nBranch = lobjValues.StringToType(CStr(nBranch), eFunctions.Values.eTypeData.etdInteger)
            .nProduct = lobjValues.StringToType(CStr(nProduct), eFunctions.Values.eTypeData.etdInteger)
            .nPolicy = lobjValues.StringToType(CStr(nPolicy), eFunctions.Values.eTypeData.etdDouble)
            .nCertif = lobjValues.StringToType(CStr(nCertif), eFunctions.Values.eTypeData.etdDouble)
            .dEffecdate = lobjValues.StringToType(CStr(dEffecdate), eFunctions.Values.eTypeData.etdDate)
            .nArticle = lobjValues.StringToType(CStr(nArticle), eFunctions.Values.eTypeData.etdLong)
            .nDetailArt = lobjValues.StringToType(CStr(nDetailArt), eFunctions.Values.eTypeData.etdLong)
            .nConstCat = lobjValues.StringToType(CStr(nConstCat), eFunctions.Values.eTypeData.etdLong)
            .nConstCat = IIf(.nConstCat = 0, eRemoteDB.Constants.intNull, .nConstCat)
            .nFloor_quan = lobjValues.StringToType(CStr(nFloor_quan), eFunctions.Values.eTypeData.etdLong)
            .nSpCombType = lobjValues.StringToType(CStr(nSpCombType), eFunctions.Values.eTypeData.etdLong)
            .nSpCombType = IIf(.nSpCombType = 0, eRemoteDB.Constants.intNull, .nSpCombType)
            .nSideCloseType = lobjValues.StringToType(CStr(nSideCloseType), eFunctions.Values.eTypeData.etdLong)
            .nSideCloseType = IIf(.nSideCloseType = 0, eRemoteDB.Constants.intNull, .nSideCloseType)
            .nIndPeriod = lobjValues.StringToType(CStr(nIndPeriod), eFunctions.Values.eTypeData.etdLong)
            .nRoofType = lobjValues.StringToType(CStr(nRoofType), eFunctions.Values.eTypeData.etdLong)
            .nRoofType = IIf(.nRoofType = 0, eRemoteDB.Constants.intNull, .nRoofType)
            .nBuildType = lobjValues.StringToType(CStr(nBuildType), eFunctions.Values.eTypeData.etdLong)
            .nBuildType = IIf(.nBuildType = 0, eRemoteDB.Constants.intNull, .nBuildType)
            .nSeismicZone = lobjValues.StringToType(CStr(nSeismicZone), eFunctions.Values.eTypeData.etdLong)
            .nSeismicZone = IIf(.nSeismicZone = 0, eRemoteDB.Constants.intNull, .nSeismicZone)
            .nDep_prem = IIf(lobjValues.StringToType(CStr(nDep_prem), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull, 0, lobjValues.StringToType(CStr(nDep_prem), eFunctions.Values.eTypeData.etdDouble))
            .sClient = sClient
            .nTransactio = lobjValues.StringToType(CStr(nTransactio), eFunctions.Values.eTypeData.etdInteger)
            .nFamily = lobjValues.StringToType(CStr(nFamily), eFunctions.Values.eTypeData.etdInteger)
            .nFamily = IIf(.nFamily = 0, eRemoteDB.Constants.intNull, .nFamily)
            .nActivityType = lobjValues.StringToType(CStr(nActivityType), eFunctions.Values.eTypeData.etdInteger)
            .sDecla_freq = sDecla_freq
            .sDecla_freq = IIf(.sDecla_freq = "0", String.Empty, .sDecla_freq)
            .sDecla_type = sDecla_type
            .sDecla_type = IIf(.sDecla_type = "0", String.Empty, .sDecla_type)

            Select Case sAction
                Case "Update"
                    InsPostIN010 = .Update
            End Select
        End With

        '+ Se actualiza la Tabla de Datos Particulares de Incendio

        If lobjValues.IsValid("Tab_in_bus", "", True) Then
            lclsFire.nDetailArt = nDetailArt
        End If

        ''+Se actualiza en Policy_Win la ventana con contenido

        If InsPostIN010 Then
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "IN010", "2")
        Else
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "IN010", "1")
        End If

InsPostIN010_Err:
        If Err.Number Then
            InsPostIN010 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsFire may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFire = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing

    End Function

    '% insValCA013A: Realiza la validación de los campos a actualizar en la ventana CA013A
    Public Function insValCA013A(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal sTyp_module As String) As String
        Dim lrecInsValCA013A As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        '+Definición de parámetros para stored procedure 'InsCA013pkg.InsValCA013Upd'
        '+Información leída el 24/04/2003
        On Error GoTo InsValCA013A_Err
        lrecInsValCA013A = New eRemoteDB.Execute
        With lrecInsValCA013A
            .StoredProcedure = "InsCA013pkg.insvalca013a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage("CA013A", , , , , , lstrError)
                    insValCA013A = lobjErrors.Confirm
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing

            End If

        End With
InsValCA013A_Err:
        If Err.Number Then
            insValCA013A = "InsValCA013A: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA013A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA013A = Nothing
        On Error GoTo 0

    End Function

    '% insPostCA013A: Se recorre el grid para actualizar los registros correspondientes
    Public Function insPostCA013A(ByVal nTransaction As String, ByVal sExists As String, ByVal sSelected As String, ByVal sModulec As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal sTyp_module As String, ByVal nUsercode As Integer, ByVal npremirat As Double, ByVal styp_rat As String, Optional ByVal bPuntual As Boolean = False) As Boolean
        Dim lrecExecute As eRemoteDB.Execute
        Dim lclsModul_co_gp As ePolicy.Modul_co_gp
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lstrContent As String
        Dim nPuntual As Short

        If bPuntual Then
            nPuntual = 1
        Else
            nPuntual = 0
        End If

        '+ Definición de store procedure insPostca013a al 07-27-2002 16:29:40
        On Error GoTo insPostCA013A_Err
        lrecExecute = New eRemoteDB.Execute
        With lrecExecute
            .StoredProcedure = "insPostca013A"
            .Parameters.Add("sSel", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExist", sExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sModulec", sModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPuntual", nPuntual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npremirat", npremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("styp_rat", styp_rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA013A = .Run(False)
        End With

insPostCA013A_Err:
        If Err.Number Then
            insPostCA013A = False
        End If
        On Error GoTo 0
        '+ Se liberan de memoria las instancias creadas
        'UPGRADE_NOTE: Object lclsModul_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsModul_co_gp = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lrecExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecExecute = Nothing
    End Function

    '% insPostCA041: Realiza la acutalización de la tabla "Sum_insur"
    Public Function insPostCA041(ByVal nTransaction As Integer, ByVal sSel As String, ByVal sExist As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sCurrency As String) As Boolean
        Dim lintIndex As Integer
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsCurren_pol As ePolicy.Curren_pol

        Dim lintCurrency As Integer
        Dim lintSel As Integer
        Dim lintExist As Integer
        Dim bLast As Boolean


        On Error GoTo insPostCA041_Err

        lclsCurren_pol = New ePolicy.Curren_pol
        lclsPolicyWin = New ePolicy.Policy_Win
        lclsPolicy = New ePolicy.Policy

        bLast = False

        '+ Se realizan las validaciones sobre el grid
        Do While Len(sSel) > 0 And Not bLast

            '+ Se descomponen los datos, cada uno viene en una cadena de valores separados por coma.
            If InStr(1, sSel, ",") > 0 Then
                lintSel = CInt(Mid(sSel, 1, InStr(1, sSel, ",") - 1))
                lintCurrency = CInt(Mid(sCurrency, 1, InStr(1, sCurrency, ",") - 1))
                lintExist = CInt(Mid(sExist, 1, InStr(1, sExist, ",") - 1))

                sSel = Trim(Mid(sSel, InStr(1, sSel, ",") + 1))
                sCurrency = Trim(Mid(sCurrency, InStr(1, sCurrency, ",") + 1))
                sExist = Trim(Mid(sExist, InStr(1, sExist, ",") + 1))

            Else
                '+ Se trata al último elemento de la cadena o la cadena traía sólo un elemento
                lintSel = CInt(sSel)
                lintCurrency = CInt(sCurrency)
                lintExist = CInt(sExist)
                bLast = True

            End If
            '+ Si el proceso es emisión, recuperación, solicitud o cotización
            If (nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue) Then
                '+ Si esta seleccionada
                If lintSel = CDbl("1") Then
                    '+ Si existe
                    If lintExist = CDbl("1") Then
                        Call lclsCurren_pol.updCurren_pol(sCertype, nBranch, nProduct, nPolicy, lintCurrency, nCertif, dEffecdate, nUsercode)
                    Else
                        Call lclsCurren_pol.CreCurren_pol(sCertype, nBranch, nProduct, nPolicy, lintCurrency, nCertif, dEffecdate, nUsercode)
                    End If
                Else
                    '+ Si no está seleccionada y existe
                    If lintExist = CDbl("1") Then
                        Call lclsCurren_pol.DelCurren_pol(sCertype, nBranch, nProduct, nPolicy, lintCurrency, nCertif, dEffecdate)
                    End If
                End If
                '+ Si el proceso es modificación
            ElseIf (nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment) Then
                '+ Si esta seleccionada
                If lintSel = CDbl("1") Then
                    If lintExist <> CDbl("1") Then
                        Call lclsCurren_pol.CreCurren_pol(sCertype, nBranch, nProduct, nPolicy, lintCurrency, nCertif, dEffecdate, nUsercode)
                    End If
                Else
                    If lintExist = CDbl("1") Then
                        Call lclsCurren_pol.insDelCurren_pol(sCertype, nBranch, nProduct, nPolicy, lintCurrency, nCertif, dEffecdate)
                    End If
                End If
            End If
        Loop

        Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA041", "2")

        insPostCA041 = True
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing

insPostCA041_Err:
        If Err.Number Then
            insPostCA041 = False
        End If
        On Error GoTo 0

    End Function

    '%insConvertion: Esta rutina se encarga de invocar al store-procedured que convierte las
    '%cotizaciones o solicitudes a póliza
    Private Function insConvertion(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal nUsercode As Integer, ByVal nQuotProp As Integer, ByVal nTransaction As Integer) As Boolean
        Dim llngPolicyNum As Double
        Dim lclsNumerator As eGeneral.GeneralFunction
        Dim lclsOptSystem As eGeneral.Opt_system
        Dim lclsConvert As ePolicy.Policy

        On Error GoTo insConvertion_Err
        lclsConvert = New ePolicy.Policy
        With lclsConvert
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nProponum = nQuotProp
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .npProctype = 0
            .sPolitype = sPolitype
            .nUsercode = nUsercode
            llngPolicyNum = nPolicy
            If nTransaction = Constantes.PolTransac.clngQuotationConvertion Or nTransaction = Constantes.PolTransac.clngProposalConvertion Then

                lclsOptSystem = New eGeneral.Opt_system
                If lclsOptSystem.find Then
                    lclsNumerator = New eGeneral.GeneralFunction
                    Select Case lclsOptSystem.sPolicyNum
                        '-Númeración general
                        Case "1"
                            llngPolicyNum = lclsNumerator.Find_Numerator(2, 0, nUsercode, sCertype, nBranch, nProduct)

                            '-Númeración por ramo
                        Case "2"
                            llngPolicyNum = lclsNumerator.Find_Numerator(2, nBranch, nUsercode, sCertype, nBranch, nProduct)

                            '-Si ni coincide el numerador se numera general
                        Case Else
                            llngPolicyNum = lclsNumerator.Find_Numerator(2, 0, nUsercode, sCertype, nBranch, nProduct)
                    End Select
                End If
            End If
            .nPolicy = llngPolicyNum
            insConvertion = .ConvertToPolicy(sString)
        End With

        mlngPolicy = llngPolicyNum

insConvertion_Err:
        If Err.Number Then
            insConvertion = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsNumerator = Nothing
        'UPGRADE_NOTE: Object lclsOptSystem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsOptSystem = Nothing
        'UPGRADE_NOTE: Object lclsConvert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsConvert = Nothing
    End Function

    '%insValCA047: Esta función realiza las validaciones de Información del préstamo/hipoteca.
    Public Function insValCA047(ByVal sCodispl As String, ByVal tcdStayDate As Date) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValCA047_Err

        lobjErrors = New eFunctions.Errors

        If tcdStayDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(lstrCodispl, 3323)
        Else
            If tcdStayDate < Today Then
                Call lobjErrors.ErrorMessage(lstrCodispl, 3324)
            End If
        End If

        insValCA047 = lobjErrors.Confirm

insValCA047_Err:
        If Err.Number Then
            insValCA047 = "insValCA047: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%insPostCA047: se actualizan los datos correspondiente a la Fecha máxima de permanencia
    Public Function insPostCA047(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal optTypeSolic As String, ByVal tcdStayDate As Date, ByVal nTransaction As Integer) As Boolean
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat

        On Error GoTo insPostCA047_Err

        lclsPolicy = New ePolicy.Policy
        lclsPolicyWin = New ePolicy.Policy_Win
        lclsCertificat = New ePolicy.Certificat

        If optTypeSolic = "1" Then
            pstrType_prop = "1"
        Else
            pstrType_prop = "2"
        End If

        pdtmMaximum_da = tcdStayDate

        insPostCA047 = lclsPolicy.InsPolicy_Ca047(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, pdtmMaximum_da, pstrType_prop, nTransaction)
        If insPostCA047 Then
            insPostCA047 = lclsCertificat.insCertificat_Ca047(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, pdtmMaximum_da)
        End If

        If lclsPolicy.InsLoadCA047(sCertype, nBranch, nProduct, nPolicy) Then
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA047", "2", True)
        Else
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA047", "1", True)
        End If

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing

insPostCA047_Err:
        If Err.Number Then
            insPostCA047 = False
        End If
        On Error GoTo 0
    End Function

    '% InsValVI001: Realiza la validación de los campos a actualizar en la ventana VI001
    Public Function InsValVI001(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sValGroup As String = "", Optional ByVal sValSituation As String = "", Optional ByVal nTypDurins As Integer = 0, Optional ByVal nInsur_time As Integer = 0, Optional ByVal nPernunmi As Double = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nCapital_ca As Double = 0, Optional ByVal dExpirdat As Date = #12:00:00 AM#, Optional ByVal nTypDurpay As Integer = 0, Optional ByVal nPay_time As Integer = 0, Optional ByVal dDate_pay As Date = #12:00:00 AM#, Optional ByVal nRentamount As Integer = 0, Optional ByVal nCurrrent As Integer = 0, Optional ByVal nTransaction As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsGroups As ePolicy.Groups
        Dim lclsSituation As ePolicy.Situation
        Dim lclsProduct As eProduct.Product
        Dim lclsCertificat As Certificat

        On Error GoTo InsValVI001_Err
        lobjErrors = New eFunctions.Errors
        lclsGroups = New ePolicy.Groups
        lclsSituation = New ePolicy.Situation
        lclsProduct = New eProduct.Product
        lclsCertificat = New Certificat

        Dim lclsCapital_age As eProduct.Capital_age
        Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
        Dim nModulec As Integer
        lclsCapital_age = New eProduct.Capital_age
        lclsTab_Activelife = New eProduct.Tab_ActiveLife
        nModulec = 0

        With lobjErrors
            '+Validación del campo grupo colectivo
            If nCertif <> 0 Then
                If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                    lblnGroups = True
                End If
                If (sValGroup = String.Empty Or sValGroup = "0") And lblnGroups Then
                    .ErrorMessage(sCodispl, 3946)
                End If

                '+Validación del campo Situación del riesgo.
                If lclsSituation.insReaSituation(sCertype, nBranch, nProduct, nPolicy) Then
                    lblnSituation = True
                End If
                If (sValSituation = String.Empty Or sValSituation = "0") And lblnSituation Then
                    .ErrorMessage(sCodispl, 13983)
                End If
            End If

            '+Se verifica que el indicador de duración este con información
            If nTypDurins = eRemoteDB.Constants.intNull Or nTypDurins = 0 Then
                .ErrorMessage(sCodispl, 60167)
            Else
                '+Se verifica el indicador de duración del seguro para saber que campos deben estar llenos del
                '+del frame de duración/Seguro
                If nTypDurins <> CDbl("4") And nTypDurins <> CDbl("5") And nTypDurins <> CDbl("6") Then
                    Select Case nTypDurins
                        Case CDec("1"), CDec("2"), CDec("7"), CDec("8"), CDec("9")
                            If nInsur_time = eRemoteDB.Constants.intNull Then
                                .ErrorMessage(sCodispl, 56012)
                            End If
                        Case CDec("3")
                            If dExpirdat = eRemoteDB.Constants.dtmNull Then
                                .ErrorMessage(sCodispl, 55774)
                            End If
                    End Select
                End If

                '+Se verifica el indicador de pagos del seguro para saber que campos deben estar llenos del
                '+del frame de duración/Pagos
                If nTypDurpay = eRemoteDB.Constants.intNull Or nTypDurpay = 0 Then
                    .ErrorMessage(sCodispl, 60180)
                Else
                    '+Se verifica el indicador de duración del pago para saber que campos deben estar llenos del
                    '+del frame de duración/Pagos
                    If nTypDurpay <> CDbl("4") And nTypDurpay <> CDbl("5") And nTypDurpay <> CDbl("6") Then
                        '+ Validaciones sobre la duración de los pagos del seguro
                        Select Case nTypDurpay
                            Case CDec("1"), CDec("2"), CDec("7"), CDec("8"), CDec("9")
                                If nPay_time = eRemoteDB.Constants.intNull Then
                                    .ErrorMessage(sCodispl, 56013)
                                End If
                            Case CDec("3")
                                If dDate_pay = eRemoteDB.Constants.dtmNull Then
                                    .ErrorMessage(sCodispl, 55774)
                                End If
                        End Select
                    End If
                End If
            End If

            '+ La fecha hasta del seguro y de los pagos debe estar dentro de las fecha vigentes para la póliza
            If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                If dExpirdat <> eRemoteDB.Constants.dtmNull Then
                    If lclsCertificat.dStartdate > dExpirdat Then
                        .ErrorMessage(sCodispl, 55774)
                    End If
                End If
                If dDate_pay <> eRemoteDB.Constants.dtmNull Then
                    If dDate_pay < lclsCertificat.dStartdate Or dDate_pay > lclsCertificat.dExpirdat Then
                        .ErrorMessage(sCodispl, 11424)
                    End If
                End If
                '+ Se valida que la duración de la póliza se mayor o idual a la duración mínima
                '+ establecida en el diseñador de productos
                If insValDuration(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lclsCertificat.dDate_Origi, nTypDurins, nInsur_time) Then
                    If mintMinDurIns > 0 Then
                        Select Case nTypDurins
                            Case 1, 2 '+ Anual, Edad alcanzada
                                .ErrorMessage(sCodispl, 80180, , , " por el producto (" & mintMinDurIns & " Años).")
                            Case 8 '+ Meses
                                .ErrorMessage(sCodispl, 80180, , , " por el producto (" & mintMinDurIns & " Meses).")
                            Case 9 '+ Días
                                .ErrorMessage(sCodispl, 80180, , , " por el producto (" & mintMinDurIns & " Días).")
                        End Select
                    Else
                        If nTypDurins = 1 Then
                            .ErrorMessage(sCodispl, 80181, , , " (" & nInsur_time & " Años).")
                        End If
                    End If
                End If
            End If
            If nTypDurpay = 6 Then
                If dDate_pay = eRemoteDB.Constants.dtmNull And nCertif = 0 Then
                    .ErrorMessage(sCodispl, 11423)
                End If
            End If
            '+ Se validad que se ingrese la moneda de renta si se ingreso el monto de renta
            If nRentamount <> 0 And nRentamount <> eRemoteDB.Constants.intNull Then
                If nCurrrent = 0 Or nCurrrent = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 10107)
                End If
            End If

            '+ Se validad que los capitales cumplan condicon se copia de la vi7001
            Call lclsTab_Activelife.Find(nBranch, nProduct, nModulec, eRemoteDB.Constants.intNull, dEffecdate)

            If lclsCapital_age.insValCapital(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCapital, nTransaction) Then
                If (lclsCapital_age.nCapmini > 0 And nCapital < lclsCapital_age.nCapmini) Then
                    Call .ErrorMessage(sCodispl, 800032, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmini & "-")
                ElseIf (lclsCapital_age.nCapmaxim > 0 And nCapital > lclsCapital_age.nCapmaxim) Then
                    Call .ErrorMessage(sCodispl, 800033, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmaxim & "-")
                End If
            End If

            If nCapital < lclsTab_Activelife.nCapmin Then
                Call .ErrorMessage(sCodispl, 60170)
            End If

            InsValVI001 = .Confirm
        End With

InsValVI001_Err:
        If Err.Number Then
            InsValVI001 = "InsValVI001: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSituation = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        On Error GoTo 0
    End Function

    '%InsPostVI001: Se realiza la actualización de los datos en la ventana VI001
    Public Function insPostVI001(ByVal sAction As String, ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nPernunmi As Double = 0, Optional ByVal nTypDurins As Integer = 0, Optional ByVal nInsur_time As Integer = 0, Optional ByVal sPoltype As String = "", Optional ByVal nValGroup As Integer = 0, Optional ByVal nValSituation As Integer = 0, Optional ByVal dExpirdat As Date = #12:00:00 AM#, Optional ByVal nRentamount As Double = 0, Optional ByVal nCurrrent As Integer = 0, Optional ByVal nCount_insu As Integer = 0, Optional ByVal nPerc_cap As Double = 0, Optional ByVal nTypDurpay As Integer = 0, Optional ByVal nPay_time As Integer = 0, Optional ByVal dDate_pay As Date = #12:00:00 AM#) As Boolean
        Dim lrecinsPostVI001 As eRemoteDB.Execute
        On Error GoTo insPostVI001_Err
        lrecinsPostVI001 = New eRemoteDB.Execute
        '+
        '+ Definición de store procedure reaVeh_allow_v al 05-07-2002 09:46:35
        '+
        With lrecinsPostVI001
            .StoredProcedure = "insPostVI001"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPernunmi", nPernunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypDurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Time", nInsur_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPoltype", sPoltype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValGroup", nValGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValSituation", nValSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRentamount", nRentamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrrent", nCurrrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount_insu", nCount_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPerc_cap", nPerc_cap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypDurpay", nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Time", nPay_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_pay", dDate_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVI001 = .Parameters("sContent").Value > 0
            Else
                insPostVI001 = False
            End If
        End With

insPostVI001_Err:
        If Err.Number Then
            insPostVI001 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostVI001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostVI001 = Nothing
        On Error GoTo 0
    End Function

    '% insValCA016A: Realiza la validación del frame de recargos y descuentos
    '------------------------------------------------------------
    Public Function insValCA016A(ByVal lstrCodispl As String, ByVal nPercent As String, ByVal sChanallo As String, ByVal nRate As String, ByVal nDisexaddper As String, ByVal nDisexsubper As String, ByVal nAmount As String, ByVal nDisexpra As String, ByVal sRoutine As String, ByVal sRequire As String, ByVal Defoult As String, ByVal nGroup As String, ByVal pblnQuery As String, ByVal sEdperapl As String) As String
        '------------------------------------------------------------
        Dim lstrResult As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValCA016A_Err
        lobjErrors = New eFunctions.Errors
        insValCA016A = CStr(True)
        If CStr(Defoult) = "1" Then '4
            If sEdperapl <> "1" Then '3
                If Trim(nPercent) = "" Then '2
                Else '2
                    If sEdperapl = "0" Then
                        lstrResult = nPercent
                    End If
                    If lstrResult <> "-1" Then '1
                        If CByte(sChanallo) <> 0 And Len(Trim(nRate)) > 0 Then '24
                            If CSng(lstrResult) > CSng(nRate) Then '19
                                If CByte(sChanallo) And 1 Then '23
                                    If Trim(nDisexaddper) <> "" Then '22
                                        If CSng(lstrResult) > (CDbl(nRate) * (1 + CDbl(nDisexaddper) / 100)) Then '21
                                            Call lobjErrors.ErrorMessage(lstrCodispl, 3833)
                                            insValCA016A = CStr(False)
                                        End If '21
                                    End If '22
                                Else '23
                                    Call lobjErrors.ErrorMessage(lstrCodispl, 3307)
                                    insValCA016A = CStr(False)
                                End If '23
                            Else '19
                                If CSng(lstrResult) < CSng(nRate) Then '20
                                    If CByte(sChanallo) And 2 Then '18
                                        If Trim(nDisexsubper) <> "" Then '17
                                            If CSng(lstrResult) < (CDbl(nRate) * (1 - CDbl(nDisexsubper) / 100)) Then '16
                                                Call lobjErrors.ErrorMessage(lstrCodispl, 3834)
                                                insValCA016A = CStr(False)
                                            End If '16
                                        End If '17
                                    Else '18
                                        Call lobjErrors.ErrorMessage(lstrCodispl, 3306)
                                        insValCA016A = CStr(False)
                                    End If '18
                                End If '20
                            End If '19
                        End If '24
                    Else '1
                        insValCA016A = CStr(False)
                    End If '1
                End If '2
            End If '3
        Else '4
            If Trim(nAmount) = String.Empty Or Trim(nAmount) = "0" Then '8
            Else '8
                lstrResult = nAmount
                If lstrResult <> "-1" Then '7
                    If CByte(sChanallo) <> 0 And Len(Trim(nDisexpra)) Then '6
                        If CSng(lstrResult) > CSng(nDisexpra) Then '5
                            If CByte(sChanallo) And 1 Then '15
                                If Trim(nDisexaddper) <> "" Then '14
                                    If CSng(lstrResult) > (CDbl(nDisexpra) * (1 + CDbl(nDisexaddper) / 100)) Then '13
                                        Call lobjErrors.ErrorMessage(lstrCodispl, 3729)
                                        insValCA016A = CStr(False)
                                    End If '13
                                End If '14
                            Else '15
                                Call lobjErrors.ErrorMessage(lstrCodispl, 3314)
                                insValCA016A = CStr(False)
                            End If '15
                        Else '5
                            If CSng(lstrResult) < CSng(nDisexpra) Then '9
                                If CByte(sChanallo) And 2 Then '10
                                    If Trim(nDisexsubper) <> "" Then '11
                                        If CSng(lstrResult) < (CDbl(nDisexpra) * (1 - CDbl(nDisexsubper) / 100)) Then '12
                                            Call lobjErrors.ErrorMessage(lstrCodispl, 3730)
                                            insValCA016A = CStr(False)
                                        End If '12
                                    End If '11
                                Else '10
                                    Call lobjErrors.ErrorMessage(lstrCodispl, 3313)
                                    insValCA016A = CStr(False)
                                End If '10
                            End If '9
                        End If '5
                    End If '6
                Else '7
                    insValCA016A = CStr(False)
                End If '7
            End If '8
        End If
        insValCA016A = lobjErrors.Confirm

insValCA016A_Err:
        If Err.Number Then
            insValCA016A = "insValCA016A: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        On Error GoTo 0
    End Function

    '% insreaPolicy_quotprop: retorna las cotizaciones pendientes de una póliza
    Private Function insreaPolicy_quotprop(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nQuotProp As Integer, ByVal nStatquota As Certificat.Stat_quot, ByVal objError As eFunctions.Errors) As Boolean
        Dim lstrQuotProp As String
        Dim lintCount As Integer
        Dim llngPolicy As Integer
        Dim lblnCertype As Boolean

        Dim lrecReaPolicy_QuotProp As eRemoteDB.Execute

        On Error GoTo ReaPolicy_QuotProp_Err

        insreaPolicy_quotprop = True

        lrecReaPolicy_QuotProp = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'reapolicy_quotprop'
        '+Información leída el 20/11/2001

        With lrecReaPolicy_QuotProp
            .StoredProcedure = "reapolicy_quotprop"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPropquot", nQuotProp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintCount = 1
                Do While Not .EOF
                    llngPolicy = .FieldToClass("nPolicy")
                    If nQuotProp <> llngPolicy Then
                        If .FieldToClass("nStatquota") = 1 Then
                            If lintCount = 1 Then
                                lstrQuotProp = CStr(llngPolicy)
                            Else
                                lstrQuotProp = lstrQuotProp & ", " & llngPolicy
                            End If
                            lintCount = lintCount + 1
                        End If
                    End If
                    .RNext()
                Loop
                If lintCount > 1 Then
                    Call objError.ErrorMessage("CA001_K", 55649, , eFunctions.Errors.TextAlign.RigthAling, "(" & lstrQuotProp & ")")
                    insreaPolicy_quotprop = False
                End If
            End If
        End With

ReaPolicy_QuotProp_Err:
        If Err.Number Then
            insreaPolicy_quotprop = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaPolicy_QuotProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaPolicy_QuotProp = Nothing
    End Function

    '% AddQuotProp: se genera la cotización/propuesta de modificación/renovación
    Private Function AddQuotProp(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProponum As Double, ByVal dEffecdate As Date, ByVal dLedgetdat As Date, ByVal nType_amend As Integer, ByVal nServ_order As Double, ByVal dFer As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecQuotProp As eRemoteDB.Execute

        On Error GoTo AddQuotProp_err

        lrecQuotProp = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'creQuotPropPolicy'
        '+Información leída el 11/12/2001

        With lrecQuotProp
            .StoredProcedure = "creQuotPropPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgetdat", dLedgetdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFer", dFer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            AddQuotProp = .Run(False)
        End With

AddQuotProp_err:
        If Err.Number Then
            AddQuotProp = False
        End If
        'UPGRADE_NOTE: Object lrecQuotProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecQuotProp = Nothing
    End Function

    '% insValAM002Upd: Valida la información almacenada en la ventana AM002
    Public Function insValAM002Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal nAgeIni As Integer, ByVal nAgeEnd As Integer, ByVal nGroup_comp As Integer, ByVal nPremium As Double, ByVal nGroupDed As Double, ByVal nModulec As Integer, ByVal nCover As Integer) As String
        Dim lobjError As eFunctions.Errors
        Dim lclsTar_am_pol As eBranches.Tar_am_pol
        Dim lblnError As Boolean

        On Error GoTo insValAM002Upd_Err

        lobjError = New eFunctions.Errors

        With lobjError

            '+ Validación del campo: Tarifa.
            If nTariff <= 0 Then
                .ErrorMessage("AM002", 10117)
                lblnError = True
            End If

            If Not lblnError Then
                '+ Validación del campo: Edad inicial.
                If nAgeIni < 0 Then
                    .ErrorMessage("AM002", 10247)
                    lblnError = True
                End If

                '+ Validación del campo: Edad final.
                If nAgeEnd <= 0 Then
                    .ErrorMessage("AM002", 10248)
                    lblnError = True
                End If

                '+ Validación de que la edad final sea mayor que la edad inicial.
                If nAgeIni > 0 And nAgeEnd > 0 Then
                    If nAgeIni > nAgeEnd Then
                        .ErrorMessage("AM002", 10184)
                        lblnError = True
                    End If
                End If

                '+ Validación del campo: Composición de grupo.
                If nGroup_comp <= 0 Then
                    .ErrorMessage("AM002", 3549)
                    lblnError = True
                End If

                If Not lblnError Then
                    If sAction = "Add" Then
                        lclsTar_am_pol = New eBranches.Tar_am_pol
                        '+ Validación del rango de edades.
                        If lclsTar_am_pol.valRangeAge(sCertype, nBranch, nProduct, nPolicy, nTariff, nGroup_comp, nAgeIni, nAgeEnd, nRole, nGroup, dEffecdate, nModulec, nCover) Then
                            .ErrorMessage("AM002", 10185)
                        End If
                    End If
                End If

                '+ Validación del campo: Prima.
                If nPremium = eRemoteDB.Constants.intNull Then
                    .ErrorMessage("AM002", 60345)
                End If
            End If
            insValAM002Upd = .Confirm
        End With

insValAM002Upd_Err:
        If Err.Number Then
            insValAM002Upd = insValAM002Upd & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjError = Nothing
        'UPGRADE_NOTE: Object lclsTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_pol = Nothing
    End Function

    '% insValAM002: Valida la información almacenada en la ventana AM002
    Public Function insValAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sDefaulti As String, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer) As String
        Dim lobjError As eFunctions.Errors
        Dim lclsTar_Am_Bas As eBranches.Tar_am_bas
        Dim lclsTar_am_pol As eBranches.Tar_am_pol
        Dim lblnError As Boolean
        Dim lintCount As Integer

        On Error GoTo insValAM002_Err
        lobjError = New eFunctions.Errors
        With lobjError
            '+ Validación del campo: Tarifa.
            If nTariff <= 0 Then
                .ErrorMessage("AM002", 10117)
                lblnError = True
            End If

            If Not lblnError Then
                '+ Validación del campo: Por defecto (solo para cuando se trate de póliza matriz).
                If sPolitype <> "1" And nCertif = 0 Then
                    lclsTar_am_pol = New eBranches.Tar_am_pol
                    '+ Si la tarifa está seleccionado por defecto.
                    If sDefaulti = "1" Then
                        '+ Debe indicar información del detalle de la tarifa.
                        If Not lclsTar_am_pol.valExistsTar_am_pol(sCertype, nBranch, nProduct, nPolicy, nTariff, nRole, nGroup, dEffecdate) Then
                            .ErrorMessage("AM002", 55866)
                            lblnError = True
                        End If
                    End If
                    If Not lblnError Then
                        lclsTar_Am_Bas = New eBranches.Tar_am_bas
                        lintCount = lclsTar_Am_Bas.getCountTar_am_bas(sCertype, nBranch, nProduct, nPolicy, dEffecdate, "1")
                        If lintCount = 0 Then
                            .ErrorMessage("AM002", 11420)
                            lblnError = True
                        End If
                    End If
                End If
            End If
            insValAM002 = .Confirm
        End With

insValAM002_Err:
        If Err.Number Then
            insValAM002 = insValAM002 & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjError = Nothing
        'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_Bas = Nothing
        'UPGRADE_NOTE: Object lclsTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_pol = Nothing
    End Function

    '%insPostAM002: Actualiza masiva de la información de la ventana AM002
    Public Function insPostAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal sDefaulti As String, ByVal nTransaction As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lclsTar_Am_Bas As eBranches.Tar_am_bas
        Dim lclsTar_am_pol As eBranches.Tar_am_pol
        Dim lclsLife As ePolicy.Life
        Dim lintTariff As Integer
        Dim lintRole As Integer
        Dim lintGroup As Integer
        Dim lclsPolicyWin As ePolicy.Policy_Win

        On Error GoTo insPostAM002_Err

        lclsTar_Am_Bas = New eBranches.Tar_am_bas
        lclsTar_am_pol = New eBranches.Tar_am_pol
        lclsLife = New ePolicy.Life
        lclsPolicyWin = New ePolicy.Policy_Win

        lintTariff = nTariff
        lintRole = nRole
        lintGroup = nGroup

        '+ Si la póliza es matriz o individual
        If nCertif <= 0 Then
            If lclsTar_Am_Bas.insCreUpdTar_am_bas(nTransaction, sCertype, nBranch, nProduct, nPolicy, dEffecdate, dNulldate, nTariff, nRole, nGroup, sDefaulti, nUsercode, nModulec, nCover) Then
            End If
            '+ Se obtiene la tarifa por defecto
            lclsTar_Am_Bas.nTariff = lintTariff
            lclsTar_Am_Bas.nRole = lintRole
            lclsTar_Am_Bas.nGroup = lintGroup

            If lclsTar_Am_Bas.Find_Defaulti(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "1", True) Then
                lintTariff = lclsTar_Am_Bas.nTariff
                lintRole = lclsTar_Am_Bas.nRole
                lintGroup = lclsTar_Am_Bas.nGroup
            End If
        End If

        '+ Se obtiene el primer elemento encontrado en el detalle de la tarifa en tratamiento.
        If lclsTar_am_pol.Find_First(sCertype, nBranch, nProduct, nPolicy, dEffecdate, lintTariff, lintGroup, lintRole, nModulec, nCover, True) Then
            If lclsLife.insCreUpdLife(nTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, lintTariff, lintGroup, lclsTar_am_pol.nGroup_comp, nUsercode) Then
                insPostAM002 = True
            End If
        Else
            insPostAM002 = True
        End If

        If insPostAM002 Then
            If lclsTar_am_pol.ValExist_Tar_am_pol(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM002", "2")
                Me.sContent = "2"
            Else
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM002", "1")
                Me.sContent = "1"
            End If
        End If

insPostAM002_Err:
        If Err.Number Then
            insPostAM002 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_Bas = Nothing
        'UPGRADE_NOTE: Object lclsTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_pol = Nothing
        'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '%insPostAM002: Actualización de la ventana puntual de la transacción AM002.
    Public Function insPostAM002Upd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal sDefaulti As String, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nAge_init As Integer, ByVal nAge_end As Integer, ByVal nGroup_comp As Integer, ByVal dNulldate As Date, ByVal nPremium As Double, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sAction As String, ByVal nGroupDed As Double, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lclsTar_Am_Bas As eBranches.Tar_am_bas
        Dim lclsTar_am_pol As eBranches.Tar_am_pol
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lintIndex As Integer

        On Error GoTo insPostAM002Upd_Err
        lclsTar_Am_Bas = New eBranches.Tar_am_bas
        lclsTar_am_pol = New eBranches.Tar_am_pol

        '+ Se verifica que exista información en tar_am_bas (maestro de las tarifas)
        If Not lclsTar_Am_Bas.valTar_am_bas(sCertype, nBranch, nProduct, nPolicy, nTariff, nRole, nGroup, dEffecdate, nModulec, nCover) Then
            If lclsTar_Am_Bas.insCreUpdTar_am_bas(nTransaction, sCertype, nBranch, nProduct, nPolicy, dEffecdate, dNulldate, nTariff, nRole, nGroup, sDefaulti, nUsercode, nModulec, nCover) Then
                insPostAM002Upd = True
            End If
        Else
            insPostAM002Upd = True
        End If

        '+ Si no existe ningun error
        If insPostAM002Upd Then
            '+ Se actualiza la información de tar_am_pol (detalle de las tarifas).
            insPostAM002Upd = lclsTar_am_pol.insCreUpdTar_am_pol(sAction, nTransaction, sCertype, nBranch, nProduct, nPolicy, dEffecdate, dNulldate, nTariff, nRole, nGroup, nAge_init, nAge_end, nGroup_comp, nPremium, nUsercode, nGroupDed, nModulec, nCover)
        End If

        If insPostAM002Upd Then
            lclsPolicyWin = New ePolicy.Policy_Win
            If lclsTar_am_pol.ValExist_Tar_am_pol(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM002", "2")
                Me.sContent = "2"
            Else
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM002", "1")
                Me.sContent = "1"
            End If
        End If

insPostAM002Upd_Err:
        If Err.Number Then
            insPostAM002Upd = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_pol = Nothing
        'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_Bas = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '%insValAM003Upd: valida la información almacenada en la ventana AM003
    Public Function insValAM003Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sIllness As String, ByVal nPay_Concep As Integer, ByVal nPrestac As Integer, ByVal nDed_Type As Integer, ByVal nDed_Percen As Double, ByVal nDed_Amount As Double, ByVal nDed_Quanti As Integer, ByVal nLimit As Double, ByVal nLimit_exe As Double, ByVal nLimitH As Double, ByVal nIndem_rate As Double, ByVal nTyplim As Integer, ByVal ncount As Integer, ByVal sCaren_type As String, ByVal nCaren_Dur As Integer, ByVal NDED_QUANTI_2 As Integer, ByVal NINDEM_RATE_2 As Double, ByVal NLIMIT_2 As Double, ByVal NTYPLIM_2 As Integer, ByVal NCOUNT_2 As Integer, ByVal NLIMIT_EXE_2 As Double, ByVal NPUNISH_2 As Double, ByVal SOTHERLIM As String, ByVal dEffecdate As Date) As String
        Dim lclsTab_am_bil As eBranches.Tab_Am_Bil
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValAM003Upd_Err

        lobjErrors = New eFunctions.Errors

        With lobjErrors
            '+ Valida que la tarifa se haya incluído y no se haya borrado su contenido del campo
            If nTariff <= 0 Then
                .ErrorMessage("AM003", 3550)
            End If

            '+ Valida que la cobertura se haya incluído y no se haya borrado su contenido
            If nCover <= 0 Then
                .ErrorMessage("AM003", 3552)
            End If

            '+ Valida que el concepto se haya incluído.
            If nPay_Concep <= 0 Then
                .ErrorMessage("AM003", 100136)
            End If

            '+ Valida que la prestación se haya incluído.
            If nPrestac <= 0 Then
                .ErrorMessage("AM003", 100137)
            End If

            '+ Se valida que la combinación Concepto-prestación sean únicos.
            If nPay_Concep > 0 And nPrestac >= 0 Then
                If sAction = "Add" Then
                    lclsTab_am_bil = New eBranches.Tab_Am_Bil

                    If lclsTab_am_bil.valExistsTab_am_bil(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, nCover, nTariff, nRole, sClient, sIllness, nPay_Concep, nPrestac, dEffecdate) Then
                        .ErrorMessage("AM003", 55700)
                    End If
                End If
            End If

            '+Se valida el campo "Duración de la carencia"
            If CDbl(sCaren_type) > 1 Then
                If nCaren_Dur = eRemoteDB.Constants.intNull Or nCaren_Dur = 0 Then
                    .ErrorMessage("AM003", 100116)
                End If
            Else
                If CDbl(sCaren_type) = 1 Then
                    If nCaren_Dur <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage("AM003", 100117)
                    End If
                Else
                    .ErrorMessage("AM003", 3048)
                End If
            End If

            '+Se valida que el campo "%Deducible" sea mayor que cero.
            If nDed_Type <= 0 Then
                .ErrorMessage("AM003", 3553)
            Else
                '+ Si es diferente a no tiene (valor 1 del campo tipo)
                If nDed_Type <> 1 Then
                    '+ Se debe agregar información de los campos del deducible
                    If nDed_Percen <= 0 And nDed_Amount <= 0 And nDed_Quanti <= 0 Then
                        .ErrorMessage("AM003", 38038)
                    Else
                        If nDed_Percen > 0 And nDed_Amount > 0 And nDed_Quanti > 0 Then
                            .ErrorMessage("AM003", 3556)
                        Else
                            If nDed_Percen > 0 And nDed_Amount > 0 Then
                                .ErrorMessage("AM003", 3556)
                            Else
                                If nDed_Percen > 0 And nDed_Quanti > 0 Then
                                    .ErrorMessage("AM003", 3556)
                                Else
                                    If nDed_Amount > 0 And nDed_Quanti > 0 Then
                                        .ErrorMessage("AM003", 3556)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    '+ Si el tipo de deducible es no tiene y alguno de los campos relacionados al deducible tienen valor
                    If nDed_Percen > 0 Or nDed_Amount > 0 Or nDed_Quanti > 0 Then
                        .ErrorMessage("AM003", 3555)
                    End If
                End If
            End If

            '+Se valida que el campo "%Deducible" mayor que cero.
            If nDed_Percen > 0 Then
                If nDed_Percen > 100 Then
                    .ErrorMessage("AM003", 9992, , eFunctions.Errors.TextAlign.LeftAling, "% Deduc:")
                End If
            End If

            '+Se valida que el campo "Monto" mayor que cero.
            If nDed_Amount <> 0 And nDed_Amount <> eRemoteDB.Constants.intNull Then
                If nDed_Amount < 0 Then
                    .ErrorMessage("AM003", 3749, , eFunctions.Errors.TextAlign.LeftAling, "Monto: ")
                End If
            ElseIf nDed_Type <> 0 And nDed_Type <> eRemoteDB.Constants.intNull Then
                If nDed_Amount = 0 And nDed_Type <> 1 And (nDed_Percen = 0 Or nDed_Percen = eRemoteDB.Constants.intNull) Then
                    .ErrorMessage("AM003", 3749, , eFunctions.Errors.TextAlign.LeftAling, "Monto: ")
                End If
            End If

            '+Se valida que el campo "Días" sea mayor que cero.
            '        If nDed_Quanti <= 0 Then
            '+ Si el concepto coresponde a Habitación-cuartos
            '            If nPay_Concep = 12 Then
            '                .ErrorMessage "AM003", 3749, , LeftAling, "Días: "
            '            End If
            '        End If

            '+ Se efectúan las validaciones del campo "%Indemnizar". Se valida que el campo no esté vacio
            If nIndem_rate <= 0 Then
                .ErrorMessage("AM003", 3557)
            Else
                '+ Se valida que el campo este comprendido entre 0 y 100
                If nIndem_rate <= 0 Or nIndem_rate > 100 Then
                    .ErrorMessage("AM003", 3558)
                End If
            End If

            '+Se valida que el campo "Límite" sea mayor que cero.
            If nLimit > 0 Then
                '+ Si el Monto es mayor al límite cobertura
                If nDed_Amount > nLimit Then
                    .ErrorMessage("AM003", 38036)
                Else
                    If nLimit > nLimitH And nLimitH > 0 Then
                        .ErrorMessage("AM003", 3607)
                    End If
                End If
            Else
                If nDed_Amount > nLimitH And nLimitH > 0 Then
                    .ErrorMessage("AM003", 38036)
                End If
            End If

            '+ Se valida que si el campo nTyplim es "Cantidad de veces" el campo nCount debe estar lleno
            If nTyplim = 7 Then
                If ncount <= 0 Then
                    Call .ErrorMessage("AM003", 55701)
                End If
            End If

            If SOTHERLIM = "1" Then
                If (NDED_QUANTI_2 = 0 Or NDED_QUANTI_2 = eRemoteDB.Constants.intNull) And (NINDEM_RATE_2 = 0 Or NINDEM_RATE_2 = eRemoteDB.Constants.intNull) And (NLIMIT_2 = 0 Or NLIMIT_2 = eRemoteDB.Constants.intNull) And (NTYPLIM_2 = 0 Or NTYPLIM_2 = eRemoteDB.Constants.intNull) And (NCOUNT_2 = 0 Or NCOUNT_2 = eRemoteDB.Constants.intNull) And (NLIMIT_EXE_2 = 0 Or NLIMIT_EXE_2 = eRemoteDB.Constants.intNull) And (NPUNISH_2 = 0 Or NPUNISH_2 = eRemoteDB.Constants.intNull) Then
                    .ErrorMessage("AM003", 100118)
                Else
                    '+Se valida que el campo "%Deducible" sea mayor que cero.
                    If nDed_Type > 0 Then
                        '+ Si es diferente a no tiene (valor 1 del campo tipo)
                        If nDed_Type <> 1 Then
                            '+ Se debe agregar información de los campos del deducible
                            If nDed_Percen <= 0 And nDed_Amount <= 0 And NDED_QUANTI_2 <= 0 Then
                                .ErrorMessage("AM003", 38038, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                            Else
                                If nDed_Percen > 0 And nDed_Amount > 0 And NDED_QUANTI_2 > 0 Then
                                    .ErrorMessage("AM003", 3556, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                                Else
                                    If nDed_Percen <= 0 Or nDed_Amount <= 0 Then
                                        If nDed_Percen > 0 And NDED_QUANTI_2 > 0 Then
                                            .ErrorMessage("AM003", 3556, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                                        Else
                                            If nDed_Amount > 0 And NDED_QUANTI_2 > 0 Then
                                                .ErrorMessage("AM003", 3556, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            '+ Si el tipo de deducible es no tiene y alguno de los campos relacionados al deducible tienen valor
                            If nDed_Percen > 0 Or nDed_Amount > 0 Or NDED_QUANTI_2 > 0 Then
                                .ErrorMessage("AM003", 3555, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                            End If
                        End If
                    End If

                    '+Se valida que el campo "Días" sea mayor que cero.
                    '            If NDED_QUANTI_2 <= 0 Then
                    '+ Si el concepto coresponde a Habitación-cuartos
                    '              If nPay_Concep = 12 Then
                    '                .ErrorMessage "AM003", 3749, , LeftAling, "Límite parte 2 - Días: "
                    '              End If
                    '            End If

                    '+ Se efectúan las validaciones del campo "%Indemnizar". Se valida que el campo no esté vacio
                    If NINDEM_RATE_2 <= 0 Then
                        .ErrorMessage("AM003", 3557, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                    Else
                        '+ Se valida que el campo este comprendido entre 0 y 100
                        If NINDEM_RATE_2 <= 0 Or NINDEM_RATE_2 > 100 Then
                            .ErrorMessage("AM003", 3558, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                        End If
                    End If

                    '+Se valida que el campo "Límite" sea mayor que cero.
                    If NLIMIT_2 > 0 Then
                        '+ Si el Monto es mayor al límite cobertura
                        If nDed_Amount > NLIMIT_2 Then
                            .ErrorMessage("AM003", 38036, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                        End If
                    End If

                    '+ Se valida que si el campo nTyplim es "Cantidad de veces" el campo nCount debe estar lleno
                    If NTYPLIM_2 = 7 Then
                        If NCOUNT_2 <= 0 Then
                            .ErrorMessage("AM003", 55701, , eFunctions.Errors.TextAlign.LeftAling, "Límite parte 2: ")
                        End If
                    End If
                End If
            End If
            insValAM003Upd = .Confirm
        End With

insValAM003Upd_Err:
        If Err.Number Then
            insValAM003Upd = insValAM003Upd & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bil = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%insValAM003: valida la información almacenada en la ventana AM003
    Public Function insValAM003(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nCover As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsTab_am_bil As eBranches.Tab_Am_Bil
        Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
        Dim lblnError As Boolean
        Dim lstrString As String = String.Empty

        On Error GoTo insValAM003_Err

        lobjErrors = New eFunctions.Errors
        lclsTab_am_bab = New eBranches.Tab_Am_Bab

        With lobjErrors

            '+ Se verifica si existe información
            If Not lclsTab_am_bab.valExistsTab_am_babAll(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                .ErrorMessage("AM003", 55901)
                lblnError = True
            End If

            '+ Valida que la tarifa se haya incluído y no se haya borrado su contenido del campo
            If nTariff <= 0 Then
                .ErrorMessage("AM003", 3550)
                lblnError = True
            End If

            '+ Valida que la cobertura se haya incluído y no se haya borrado su contenido
            If nCover <= 0 Then
                .ErrorMessage("AM003", 3552)
                lblnError = True
            End If

            If Not lblnError Then
                '+Se valida que la suma de los límites de los conceptos no sean superior al límite de la cobertura.
                lclsTab_am_bil = New eBranches.Tab_Am_Bil
                If lclsTab_am_bil.valTab_am_bilSumLim(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    lstrString = " (Ver -> " & "T:" & lclsTab_am_bil.nTariff & "-" & "G:" & lclsTab_am_bil.nGroup & "-" & "M:" & lclsTab_am_bil.nModulec & "-" & "C:" & lclsTab_am_bil.nCover & "-" & "TA:" & lclsTab_am_bil.nRole & "-" & "A:" & lclsTab_am_bil.sClient & "-" & "E:" & lclsTab_am_bil.sIllness & ")"
                    .ErrorMessage("AM003", 3612, , eFunctions.Errors.TextAlign.RigthAling, lstrString)
                End If
            End If

            insValAM003 = .Confirm
        End With

insValAM003_Err:
        If Err.Number Then
            insValAM003 = insValAM003 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bil = Nothing
        'UPGRADE_NOTE: Object lclsTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bab = Nothing
    End Function

    '%insPostAM003Upd: realiza las actualizaciones par ala ventana AM003 en las respectivas tablas
    Public Function insPostAM003Upd(ByVal sAction As String, ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal nCover As Integer, ByVal nPay_Concep As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nDed_Type As Integer, ByVal nDed_Percen As Double, ByVal nDed_Amount As Double, ByVal nDed_Quanti As Integer, ByVal nLimit As Double, ByVal nLimit_exe As Double, ByVal nLimitH As Double, ByVal nIndem_rate As Double, ByVal nUsercode As Integer, ByVal sAutRestit As String, ByVal nModulec As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sIllness As String, ByVal nGroup As Integer, ByVal ncount As Integer, ByVal nPrestac As Integer, ByVal nTyplim As Integer, ByVal nPunish As Integer, ByVal sCaren_type As String, ByVal nCaren_Dur As Integer, ByVal NDED_QUANTI_2 As Integer, ByVal NINDEM_RATE_2 As Double, ByVal NLIMIT_2 As Double, ByVal NTYPLIM_2 As Integer, ByVal NCOUNT_2 As Integer, ByVal NLIMIT_EXE_2 As Double, ByVal NPUNISH_2 As Double, ByVal SOTHERLIM As String, ByVal sPoltype As String, ByVal bCreHeader As Boolean) As Boolean
        Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
        Dim lclsTab_am_bil As eBranches.Tab_Am_Bil
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insPostAM003Upd_Err

        lclsTab_am_bab = New eBranches.Tab_Am_Bab
        lclsTab_am_bil = New eBranches.Tab_Am_Bil
        'Set lclsPolicy = New ePolicy.Policy

        'If sPoltype <> "1" And _
        ''    nCertif = 0 Then
        '   With lclsPolicy
        '        If .Find(sCertype, nBranch, nProduct, nPolicy) Then
        '            sClient = .sClient
        '        End If
        '   End With
        'End If

        If nModulec = eRemoteDB.Constants.intNull Then
            nModulec = 0
        End If

        If nGroup = eRemoteDB.Constants.intNull Then
            nGroup = 0
        End If

        insPostAM003Upd = True

        '+ En caso de que no se haya creado el maestro
        If bCreHeader Then
            insPostAM003Upd = lclsTab_am_bab.insCreUpdTab_am_bab(sCertype, nBranch, nProduct, nPolicy, nCertif, nCover, nTariff, sAutRestit, nLimitH, dEffecdate, dNulldate, nTransaction, nUsercode, nModulec, nRole, sClient, sIllness, nGroup)
        End If

        If insPostAM003Upd Then
            With lclsTab_am_bil
                Select Case sAction
                    Case "Add"
                        insPostAM003Upd = .insCreTab_am_bil(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, nCover, nPay_Concep, nPrestac, nTariff, nRole, sClient, sIllness, dEffecdate, dNulldate, nDed_Type, nDed_Percen, nDed_Amount, nDed_Quanti, nIndem_rate, nLimit, nLimit_exe, ncount, nTyplim, nPunish, sCaren_type, nCaren_Dur, NDED_QUANTI_2, NINDEM_RATE_2, NLIMIT_2, NTYPLIM_2, NCOUNT_2, NLIMIT_EXE_2, NPUNISH_2, SOTHERLIM, nTransaction, nUsercode)
                    Case "Update"
                        insPostAM003Upd = .insUpdTab_am_bil(sCertype, nBranch, nProduct, nPolicy, nCertif, nCover, nPay_Concep, nTariff, dEffecdate, dNulldate, nDed_Type, nDed_Percen, nDed_Amount, nDed_Quanti, nIndem_rate, nLimit, nLimit_exe, nUsercode, nTransaction, nModulec, nRole, sClient, sIllness, nGroup, ncount, nPrestac, nTyplim, nPunish, sCaren_type, nCaren_Dur, NDED_QUANTI_2, NINDEM_RATE_2, NLIMIT_2, NTYPLIM_2, NCOUNT_2, NLIMIT_EXE_2, NPUNISH_2, SOTHERLIM)
                    Case "Delete"
                        insPostAM003Upd = .insDelTab_am_bil(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, nCover, nPay_Concep, nPrestac, nTariff, nRole, sClient, sIllness, dEffecdate, dNulldate, nTransaction, nUsercode)
                End Select
            End With
        End If

insPostAM003Upd_Err:
        If Err.Number Then
            insPostAM003Upd = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bab = Nothing
        'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bil = Nothing
        ' Set lclsPolicy = Nothing
    End Function

    '%insPostAM003: realiza las actualizaciones para la ventana AM003 en las respectivas tablas
    Public Function insPostAM003(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lstrContent As Object

        On Error GoTo insPostAM003_Err

        lclsTab_am_bab = New eBranches.Tab_Am_Bab
        lclsPolicy_Win = New ePolicy.Policy_Win

        '+ Se elimina la información del maestro cuando no existe información en el detalle.
        insPostAM003 = lclsTab_am_bab.insDelTab_am_babAll(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nTransaction, nUsercode)

        '+ Se verifica si existe información en el sistema
        If lclsTab_am_bab.valExistsTab_am_babAll(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            lstrContent = "2"
        Else
            lstrContent = "1"
        End If

        '+ Se actualiza la ventana con contenido o sin contenido.
        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM003", lstrContent)

insPostAM003_Err:
        If Err.Number Then
            insPostAM003 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_am_bab = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing

    End Function

    '%ValVeh_Allow: Valida que un automovil pueda ser asegurado para un ramo - producto especifico
    Public Function ValVeh_Allow(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sVehcode As String) As Boolean
        Dim lrecreaVeh_allow_v As eRemoteDB.Execute

        On Error GoTo reaVeh_allow_v_Err

        lrecreaVeh_allow_v = New eRemoteDB.Execute

        '+ Definición de store procedure reaVeh_allow_v al 05-07-2002 09:46:35
        With lrecreaVeh_allow_v
            .StoredProcedure = "reaVeh_allow_v"
            .Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                ValVeh_Allow = .Parameters("nExist").Value > 0
            Else
                ValVeh_Allow = False
            End If
        End With

reaVeh_allow_v_Err:
        If Err.Number Then
            ValVeh_Allow = False
        End If
        'UPGRADE_NOTE: Object lrecreaVeh_allow_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaVeh_allow_v = Nothing
        On Error GoTo 0
    End Function

    '%InsValRelapsing: Revisa si el automovil está asugurado en alguna otra poliza sin importar el
    '%                 estado de dicha poliza
    Public Function InsValRelapsing(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sVehcode As String, ByVal nYear As Integer, ByVal sRegist As String) As Boolean
        Dim lrecInsValRelapsing As eRemoteDB.Execute

        On Error GoTo InsValRelapsing_Err
        lrecInsValRelapsing = New eRemoteDB.Execute
        '+ Definición de store procedure InsValRelapsing al 28-01-2003
        With lrecInsValRelapsing
            .StoredProcedure = "InsValRelapsing"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelapsing", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsValRelapsing = .Parameters("nRelapsing").Value = 1
        End With

InsValRelapsing_Err:
        If Err.Number Then
            InsValRelapsing = False
        End If
        'UPGRADE_NOTE: Object lrecInsValRelapsing may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValRelapsing = Nothing
        On Error GoTo 0
    End Function

    '% insValCA017: Se realizan las validaciones de los campos de la forma CA017
    Public Function insValCA017(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nReceipt As Integer, ByVal sList As String, ByVal nPremium As Double) As String
        Dim lrecInsValCA017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        '+Definición de parámetros para stored procedure 'InsCA017pkg.InsValCA017Upd'
        '+Información leída el 24/04/2003
        On Error GoTo InsValCA017_Err
        lrecInsValCA017 = New eRemoteDB.Execute
        With lrecInsValCA017
            .StoredProcedure = "insvalCA017"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sList", sList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValCA017 = lobjErrors.Confirm
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing

            End If

        End With
InsValCA017_Err:
        If Err.Number Then
            insValCA017 = "InsValCA017: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA017 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA017 = Nothing
        On Error GoTo 0

    End Function

    '%RefreshSequence: Refresca secuencia de poliza para paginas
    Public Function RefreshSequence(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal sPolitype As String, ByVal sGoToNext As String) As String
        Dim lclsPolicyWin As Policy_Win
        Dim lclsValues As eFunctions.Values
        Dim lstrRefresh As String
        Dim lstrV_conpolic As String
        Dim lstrV_winpolic As String
        Dim lintTop As Integer
        Dim lintIndex As Integer
        Dim lintCount As Integer
        Dim lstrCodispl As String
        Dim lstrContent As String
        Dim lblnLife As Boolean

        On Error GoTo RefreshSequence_Err
        lclsPolicyWin = New Policy_Win
        If lclsPolicyWin.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then

            lclsValues = New eFunctions.Values
            lstrV_conpolic = lclsPolicyWin.sV_conpolic
            lstrV_winpolic = lclsPolicyWin.sV_winpolic
            If (sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPolitype = "1" Or (sPolitype = "2" And nCertif > 0)) Then
                lblnLife = True
            End If

            lintTop = Len(Trim(lstrV_conpolic)) - 1
            lintIndex = 1
            For lintCount = 0 To lintTop
                lstrCodispl = Trim(Mid(lstrV_winpolic, lintCount * 8 + 1, 8))
                lstrContent = Mid(lstrV_conpolic, lintCount + 1, 1)
                If Not lblnLife Or (lblnLife And lstrCodispl <> "CA014") Then
                    lstrRefresh = lstrRefresh & lclsValues.UpdContent(lstrCodispl, lstrContent) & vbCrLf
                End If
            Next lintCount
            If Trim(LCase(sGoToNext)) = "yes" Then
                lstrRefresh = lstrRefresh & "<SCRIPT>" & vbCrLf & "if (typeof(top.fraSequence)!='undefined'){" & vbCrLf & "    if (typeof(top.fraSequence.NextWindows)!='undefined')" & vbCrLf & "        top.fraSequence.NextWindows('" & sCodispl & "');" & vbCrLf & "}" & vbCrLf & "else{" & vbCrLf & "    if (typeof(top.opener.top.fraSequence)!='undefined'){" & vbCrLf & "        if (typeof(top.opener.top.fraSequence.NextWindows)!='undefined')" & vbCrLf & "            top.opener.top.fraSequence.NextWindows('" & sCodispl & "');" & vbCrLf & "        window.close();" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf & "</SCRIPT>" & vbCrLf
            End If
            RefreshSequence = lstrRefresh
        End If

RefreshSequence_Err:
        If Err.Number Then
            RefreshSequence = ""
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '**%InsValVI7001: This function makes the validation of the fields to update in window VI7001.
    '%InsValVI7001: Esta función realiza la validación de los campos a actualizar en la ventana VI7001.
    '+ VI7001 - Interes Asegurable - Unit Linked
    Public Function InsValVI7001(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sValGroup As String, ByVal sValSituation As String, ByVal sIndSecure As String, ByVal sIndPay As String, ByVal nInsurTimeAge As Integer, ByVal nInsurTimeAgeLimit As Integer, ByVal nInsurPayTimeAge As Integer, ByVal nInsurPayTimeAgeLimit As Integer, ByVal nAge As Integer, ByVal nAge_reinsu As Integer, ByVal nAge_limit As Integer, ByVal nCapital As Double, ByVal nCapitalCalc As Double, Optional ByVal nSaving_pct As Integer = 0, Optional ByVal nDisc_save_pct As Integer = 0, Optional ByVal nDisc_unit_pct As Integer = 0, Optional ByVal nIndex_table As Integer = 0, Optional ByVal nWarrn_table As Integer = 0, Optional ByVal nPremDeal As Double = 0, Optional ByVal nIntwarr As Double = 0, Optional ByVal nOption As Integer = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal nTypDurpay As Integer = 0, Optional ByVal nPay_time As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsGroups As ePolicy.Groups
        Dim lclsSituation As ePolicy.Situation
        Dim lclsProduct As eProduct.Product
        Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
        Dim lclsCapital_age As eProduct.Capital_age
        Dim nModulec As Integer

        lclsTab_Activelife = New eProduct.Tab_ActiveLife
        lclsCapital_age = New eProduct.Capital_age

        nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)

        '+ Se obtiene el plan asociado a la póliza/certificado.
        Call lclsTab_Activelife.Find(nBranch, nProduct, nModulec, eRemoteDB.Constants.intNull, dEffecdate)

        '- VI7001 - Interes Asegurable - Unit Linked

        Dim lngDisc_save_pct As Integer
        Dim lngDisc_unit_pct As Integer

        On Error GoTo ErrorHandler
        lobjErrors = New eFunctions.Errors
        lclsGroups = New ePolicy.Groups
        lclsSituation = New ePolicy.Situation
        lclsProduct = New eProduct.Product

        Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)

        '**+ Validations of the field "Divisions".
        '+ Validación del campo "Grupo colectivo".

        If nCertif <> 0 Then
            If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                lblnGroups = True
            End If
            If (sValGroup = String.Empty Or sValGroup = "0") And (lblnGroups) Then
                lobjErrors.ErrorMessage(sCodispl, 3946)
            End If

            '**+ Validations of the field "Risk Situation".
            '+ Validación del campo "Situación del riesgo".

            If lclsSituation.insReaSituation(sCertype, nBranch, nProduct, nPolicy) Then
                lblnSituation = True
            End If
            If (sValSituation = String.Empty Or sValSituation = "0") And (lblnSituation) Then
                lobjErrors.ErrorMessage(sCodispl, 13983)
            End If
        End If

        '+ Suma asegurada: Debe ser mayor a cero
        If nCapital <= 0 Then
            lobjErrors.ErrorMessage(sCodispl, 60169)
        Else
            If lclsCapital_age.insValCapital(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCapital, nTransaction) Then

                If (lclsCapital_age.nCapmini > 0 And nCapital < lclsCapital_age.nCapmini) Then

                    lobjErrors.ErrorMessage(sCodispl, 800032, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmini & "-")

                ElseIf (lclsCapital_age.nCapmaxim > 0 And nCapital > lclsCapital_age.nCapmaxim) Then

                    lobjErrors.ErrorMessage(sCodispl, 800033, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmaxim & "-")

                End If
            End If

            If nCapital < lclsTab_Activelife.nCapmin Then
                lobjErrors.ErrorMessage(sCodispl, 60170)
            End If
        End If

        '+ Prima según frecuencia de pago: Si este campo está lleno, debe ser mayor o igual a la
        '+ prima mínima definida para el producto
        If nPremDeal <> eRemoteDB.Constants.intNull Then
            If nPremDeal < lclsProduct.nPremmin Then
                lobjErrors.ErrorMessage(sCodispl, 60172)
            End If
        End If

        '+Se valida que según el indicador que esté encendido en el archivo para la duración del seguro o para la duración de los pagos del
        '+estén, se deban llenar ciertos campos del frame de duración/pagos o duración/seguro (Siempre uno de los indicadores estará encendido)

        '+Se verifica el indicador de duración de los pagos del seguro para saber que campos deben estar llenos del
        '+del frame de duración/Seguro

        If sIndSecure <> "-1" And sIndSecure <> "1" Then

            '+Si devuelve que el indicador de seguro expresa número de años entonces el campo años deberá estar lleno

            Select Case sIndSecure
                Case "2"
                    If nInsurTimeAge = eRemoteDB.Constants.intNull Then
                        lobjErrors.ErrorMessage(sCodispl, 3381)
                    End If

                Case "3"
                    If nInsurTimeAgeLimit = eRemoteDB.Constants.intNull Then
                        lobjErrors.ErrorMessage(sCodispl, 3382)
                    Else
                        If nAge <> eRemoteDB.Constants.intNull Then
                            If nInsurTimeAgeLimit < nAge Then
                                lobjErrors.ErrorMessage(sCodispl, 3746)
                            End If
                        End If
                    End If
            End Select
        End If

        Select Case sIndPay
            Case "2"
                If nInsurPayTimeAge = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 3383)
                End If
            Case "3"
                If nInsurPayTimeAgeLimit = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 3384)
                Else

                    '+Si está lleno debe ser mayor a la edad-real

                    If nAge <> eRemoteDB.Constants.intNull Then
                        If nInsurPayTimeAgeLimit <> eRemoteDB.Constants.intNull Then
                            If nInsurPayTimeAgeLimit < nAge Then
                                lobjErrors.ErrorMessage(sCodispl, 3755)
                            End If
                        End If
                    End If
                End If
        End Select

        '+Se valida que en el frame de Edad, el campo edad real esté lleno

        If nAge = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 3953)
        Else

            '+ Si es vida, se verifica que la edad del asegurado esté entre el rango de edades
            '+ de contratación definidas para el producto
            If (nTransaction = 1 Or nTransaction = 2 Or nTransaction = 3 Or nTransaction = 4 Or nTransaction = 5 Or nTransaction = 6 Or nTransaction = 7 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 23 Or nTransaction = 17) Then

                If lclsProduct.nSuagemax <> 0 And lclsProduct.nSuagemax <> eRemoteDB.Constants.intNull Then
                    If nAge > lclsProduct.nSuagemax Then
                        lobjErrors.ErrorMessage(sCodispl, 38052)
                    End If
                End If

                If lclsProduct.nSuagemin <> 0 And lclsProduct.nSuagemin <> eRemoteDB.Constants.intNull Then
                    If nAge < lclsProduct.nSuagemin Then
                        lobjErrors.ErrorMessage(sCodispl, 38052)
                    End If
                End If
            End If
        End If

        '+Se valida que en el frame de Edad, el campo edad actuarial esté lleno

        If nAge_reinsu = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 3954)
        Else

            '+De estar lleno, debe ser menor que la edad alcanzada del frame de duración/seguro

            If nInsurTimeAgeLimit <> eRemoteDB.Constants.intNull Then
                If nAge_reinsu > nInsurTimeAgeLimit Then
                    lobjErrors.ErrorMessage(sCodispl, 3747)
                End If
            End If
        End If

        '+Se valida que en el frame de Edad, si el campo edad máximo está lleno sea menor a la edad alcanzada del frame
        '+duración/seguro-edad alcanzada

        If nAge_limit <> eRemoteDB.Constants.intNull Then
            If nInsurTimeAgeLimit < nAge_limit And nInsurTimeAgeLimit <> eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 3748)
            End If
        End If

        '+De estar lleno el campo del frame de edad, edad máxima, éste debe ser mayor a la edad real

        If nAge_limit < nAge Then
            lobjErrors.ErrorMessage(sCodispl, 3601)
        End If

        '+Se valida la duración/Pagos
        If nTypDurpay = eRemoteDB.Constants.intNull Or nTypDurpay = 0 Then
            lobjErrors.ErrorMessage(sCodispl, 60180)
        Else
            '+Se verifica el indicador de duración del pago para saber que campos deben estar llenos del
            '+del frame de duración/Pagos
            If nTypDurpay <> CDbl("4") And nTypDurpay <> CDbl("5") And nTypDurpay <> CDbl("6") Then
                '+ Validaciones sobre la duración de los pagos del seguro
                Select Case nTypDurpay
                    Case CDec("1"), CDec("2"), CDec("7"), CDec("8"), CDec("9")
                        If nPay_time = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 56013)
                        End If

                        'Case "3"
                        '    If dDate_pay = dtmNull Then
                        '        lobjErrors.ErrorMessage sCodispl, 55774
                        '    End If
                End Select
            End If
        End If

        '+Se validan los campos prima total y nCapital del frame Cálculo de prima y cálculo de nCapital

        If (nCapital <> eRemoteDB.Constants.intNull) And (nCapitalCalc <> eRemoteDB.Constants.intNull) Then
            lobjErrors.ErrorMessage(sCodispl, 3385)
        End If

        '+ VI7001 - Interes Asegurable - Unit Linked

        If nSaving_pct > 100 Then
            lobjErrors.ErrorMessage(sCodispl, 70152)
        End If

        If nSaving_pct > 0 Then
            If nIndex_table = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 70153)
            End If
            If nIndex_table <= 3 Then
                If nWarrn_table = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 70154)
                End If
            End If
        End If
        lngDisc_save_pct = nDisc_save_pct
        lngDisc_unit_pct = nDisc_unit_pct

        If lngDisc_save_pct = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70155)
        End If

        If lngDisc_unit_pct = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70156)
        End If

        If lngDisc_save_pct + lngDisc_unit_pct <> 100 Then
            lobjErrors.ErrorMessage(sCodispl, 70157)
        End If

        Me.nErrors = eRemoteDB.Constants.intNull
        Me.sErrors = String.Empty
        Call insValFall_Vul(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTransaction, sCodispl, nOption, nCapital)
        If Me.nErrors <> eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, Me.nErrors, , eFunctions.Errors.TextAlign.RigthAling, Me.sErrors)
        End If

        If nOption = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 56006)
        End If

        InsValVI7001 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSituation = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCapital_age = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSituation = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsTab_Activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_Activelife = Nothing
        'UPGRADE_NOTE: Object lclsCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCapital_age = Nothing
        InsValVI7001 = String.Empty
    End Function

    '%Objetivo: Se realiza la actualización de los datos en la ventana VI7001
    Public Function InsPostVI7001(ByVal sAction As String, ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0, Optional ByVal nAge As Integer = 0, Optional ByVal nAge_limit As Integer = 0, Optional ByVal nAge_reinsu As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nCapitalCalc As Double = 0, Optional ByVal nInsurTimeAge As Integer = 0, Optional ByVal nInsurTimeAgeLimit As Integer = 0, Optional ByVal nInsurPayTimeAge As Integer = 0, Optional ByVal nInsurPayTimeAgeLimit As Integer = 0, Optional ByVal sPduraind As String = "", Optional ByVal sIduraind As String = "", Optional ByVal sIndSecure As String = "", Optional ByVal nPay_time As Integer = 0, Optional ByVal sIndPay As String = "", Optional ByVal nInsur_time As Integer = 0, Optional ByVal sPoltype As String = "", Optional ByVal nValGroup As Integer = 0, Optional ByVal nValSituation As Integer = 0, Optional ByVal nPremium_ca As Double = 0, Optional ByVal nSaving_pct As Integer = 0, Optional ByVal nDisc_save_pct As Integer = 0, Optional ByVal nDisc_unit_pct As Integer = 0, Optional ByVal nIndex_table As Integer = 0, Optional ByVal nWarrn_table As Integer = 0, Optional ByVal nOption As Integer = 0, Optional ByVal nPremiumbas As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nPremDeal As Double = 0, Optional ByVal nPremDeal_anu As Double = 0, Optional ByVal nPremMin As Double = 0, Optional ByVal nIntwarr As Double = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal nTypDurins As Integer = 0, Optional ByVal nTypDurpay As Integer = 0) As Boolean
        Dim lclsLife As ePolicy.Life
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsRoles As ePolicy.Roles
        Dim lclsPolicyHis As ePolicy.Policy_his

        Dim mintAge As Object

        On Error GoTo InsPostVI7001_Err
        lclsLife = New ePolicy.Life
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsRoles = New ePolicy.Roles

        '+ se recupera la fecha de inicio de la poliza
        Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

        With lclsLife
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nCapital = nCapital
            .nCapital_ca = nCapital
            .nUsercode = nUsercode
            .nGroup = nValGroup
            .nSituation = nValSituation
            .nAge = nAge
            .nAge_limit = nAge_limit
            .nAge_reinsu = nAge_reinsu

            .nSaving_pct = nSaving_pct
            .nDisc_save_pct = nDisc_save_pct
            .nDisc_unit_pct = nDisc_unit_pct
            .nIndex_table = nIndex_table
            .nWarrn_table = nWarrn_table

            .nOption = nOption
            .nPremiumbas = nPremiumbas
            .nModulec = nModulec
            .nPremDeal = nPremDeal
            .nPremDeal_anu = nPremDeal_anu
            .nPremMin = nPremMin
            .nIntwarr = nIntwarr
            .nTransactio = nTransaction
            .nTypDurins = nTypDurins
            .nTypDurpay = nTypDurpay
            .nPay_time = nPay_time

            If nInsurTimeAge > 0 Then
                .nInsur_time = nInsurTimeAge
                .sIduraind = "2"
                .dExpirdat = DateAdd(Microsoft.VisualBasic.DateInterval.Year, nInsurTimeAge, lclsCertificat.dStartdate)
                .dExpirdat = System.DateTime.FromOADate(.dExpirdat.ToOADate - 1)
            ElseIf nInsurTimeAgeLimit > 0 Then
                '+ se recuperon los datos del asegurado principal
                lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, Roles.eRoles.eRolInsured, String.Empty, dEffecdate)
                '+ se calcula la edad del asegurado principal
                lclsRoles.CalInsuAge(nBranch, nProduct, dEffecdate, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking)
                '+ se recupera la edad del asegurado principal
                mintAge = lclsRoles.nAge(False)
                .nInsur_time = nInsurTimeAgeLimit
                If nInsurTimeAgeLimit <> 99 Then
                    .sIduraind = "3"
                    .dExpirdat = DateAdd(Microsoft.VisualBasic.DateInterval.Year, nInsurTimeAgeLimit - mintAge, lclsCertificat.dStartdate)
                    .dExpirdat = System.DateTime.FromOADate(.dExpirdat.ToOADate - 1)
                Else
                    .sIduraind = "1"
                    .nInsur_time = eRemoteDB.Constants.intNull
                    .dExpirdat = eRemoteDB.Constants.dtmNull
                End If
                '+ se calcula la fecha de termino de la vigencia
            Else
                .sIduraind = "4"
                .dExpirdat = lclsCertificat.dExpirdat
            End If

            '+ Modificación de fecha de vencimiento para
            '+ duración de seguro Libre
            If .sIduraind = "4" Then
                lclsCertificat.dEffecdate = dEffecdate
                lclsCertificat.nUsercode = nUsercode
                lclsCertificat.nCapital = nCapital
                lclsCertificat.sPolitype = sPoltype
                lclsCertificat.nGroup = nValGroup
                lclsCertificat.nSituation = nValSituation
                lclsCertificat.dExpirdat = lclsLife.dExpirdat
                lclsCertificat.Update()

                'Actualización dexpirdat a policy
                lclsPolicy.sCertype = sCertype
                lclsPolicy.nBranch = nBranch
                lclsPolicy.nProduct = nProduct
                lclsPolicy.nPolicy = nPolicy
                lclsPolicy.nUsercode = nUsercode
                lclsPolicy.DEXPIRDAT = lclsLife.dExpirdat
                lclsPolicy.Update_dexpirdat()
            Else
                lclsCertificat.dEffecdate = dEffecdate
                lclsCertificat.nUsercode = nUsercode
                lclsCertificat.nCapital = nCapital
                lclsCertificat.sPolitype = sPoltype
                lclsCertificat.nGroup = nValGroup
                lclsCertificat.nSituation = nValSituation
                lclsCertificat.dExpirdat = lclsLife.dExpirdat
                lclsCertificat.Update()
                lclsPolicy.sCertype = sCertype
                lclsPolicy.nBranch = nBranch
                lclsPolicy.nProduct = nProduct
                lclsPolicy.nPolicy = nPolicy
                lclsPolicy.nUsercode = nUsercode
                lclsPolicy.DEXPIRDAT = lclsLife.dExpirdat
                lclsPolicy.Update_dexpirdat()
            End If

            '+Se actualiza el grupo y la situación a certificat
            If sPoltype <> "1" And nCertif <> 0 Then
                Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
                lclsCertificat.dEffecdate = dEffecdate
                lclsCertificat.nUsercode = nUsercode
                lclsCertificat.nCapital = nCapital
                lclsCertificat.sPolitype = sPoltype
                lclsCertificat.nGroup = nValGroup
                lclsCertificat.nSituation = nValSituation
                lclsCertificat.dExpirdat = lclsLife.dExpirdat
                lclsCertificat.Update()
            End If
            Select Case sAction
                Case "Update"
                    InsPostVI7001 = .UpdateVI7001
                    lclsPolicyHis = New ePolicy.Policy_his
                    Call lclsPolicyHis.updPolHisNulldate(sCertype, nBranch, nProduct, nPolicy, nCertif, nUsercode)
            End Select

        End With

        '+Se actualiza en Policy_Win la ventana con contenido
        lclsPolicy_Win = New ePolicy.Policy_Win
        If InsPostVI7001 Then
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7001", "2")
        Else
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7001", "1")
        End If

InsPostVI7001_Err:
        If Err.Number Then
            InsPostVI7001 = False
        End If
        'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsPolicyHis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyHis = Nothing
        On Error GoTo 0
    End Function

    '% GenPolTransac: Obtiene la transacción general que se está ejecutando
    Private ReadOnly Property GenPolTransac(ByVal nTransaction As Constantes.PolTransac) As Constantes.eGenPolTransac
        Get
            Dim llngTransac As Constantes.eGenPolTransac

            Select Case nTransaction
                Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngCertifProposal

                    llngTransac = Constantes.eGenPolTransac.clngIssue

                Case Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngProposalQuery, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngPropAmendentQuery, Constantes.PolTransac.clngQuotRenewalQuery, Constantes.PolTransac.clngPropRenewalQuery

                    llngTransac = Constantes.eGenPolTransac.clngQuery

                Case Constantes.PolTransac.clngPolicyAmendment, Constantes.PolTransac.clngTempPolicyAmendment, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment

                    llngTransac = Constantes.eGenPolTransac.clngAmend

                Case Constantes.PolTransac.clngQuotationConvertion, Constantes.PolTransac.clngProposalConvertion, Constantes.PolTransac.clngPropQuotConvertion, Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngPropRenewalConvertion, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngQuotPropRenewalConvertion

                    llngTransac = Constantes.eGenPolTransac.clngConvert

                Case Constantes.PolTransac.clngPolicyQuotAmendent, Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngPolicyQuotRenewal, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngPolicyPropRenewal, Constantes.PolTransac.clngCertifPropRenewal

                    llngTransac = Constantes.eGenPolTransac.clngAmendPropQuot

                Case Else

                    llngTransac = nTransaction
            End Select
            GenPolTransac = llngTransac
        End Get
    End Property

    '%PolicyIsNeed: Indica si el campo póliza es obligatorio según la transacción
    Private ReadOnly Property PolicyIsNeed(ByVal nTransaction As Constantes.PolTransac) As Boolean
        Get
            Select Case nTransaction
                Case Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngCertifProposal, Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngProposalQuery, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngPropAmendentQuery, Constantes.PolTransac.clngQuotRenewalQuery, Constantes.PolTransac.clngPropRenewalQuery, Constantes.PolTransac.clngPolicyAmendment, Constantes.PolTransac.clngTempPolicyAmendment, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment, Constantes.PolTransac.clngQuotationConvertion, Constantes.PolTransac.clngProposalConvertion, Constantes.PolTransac.clngPropQuotConvertion, Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngPropRenewalConvertion, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngQuotPropRenewalConvertion, Constantes.PolTransac.clngPolicyQuotAmendent, Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngPolicyQuotRenewal, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngPolicyPropRenewal, Constantes.PolTransac.clngCertifPropRenewal

                    PolicyIsNeed = True

                Case Else
                    PolicyIsNeed = False
            End Select
        End Get
    End Property

    '%CertifIsNeed: Indica si el campo póliza es obligatorio según la transacción
    Private ReadOnly Property CertifIsNeed(ByVal nTransaction As Constantes.PolTransac) As Boolean
        Get
            Select Case nTransaction
                Case Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment, Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngCertifPropRenewal
                    CertifIsNeed = True
            End Select
        End Get
    End Property

    '%PropQuotIsNeed: Indica si el campo póliza es obligatorio según la transacción
    Private ReadOnly Property PropQuotIsNeed(ByVal nTransaction As Constantes.PolTransac) As Boolean
        Get
            Select Case nTransaction
                Case Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngQuotPropRenewalConvertion, Constantes.PolTransac.clngQuotRenewalQuery, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngPropAmendentQuery, Constantes.PolTransac.clngPropRenewalConvertion, Constantes.PolTransac.clngPropRenewalQuery

                    PropQuotIsNeed = True

                Case Else
                    PropQuotIsNeed = False
            End Select
        End Get
    End Property

    '%GetPolCertype: Obtiene el valor SCERTYPE según la transacción
    Private ReadOnly Property GetPolCertype(ByVal nTransaction As Constantes.PolTransac) As Constantes.ePolCertype
        Get
            Dim lstrCertype As Constantes.ePolCertype
            Select Case nTransaction
                '+1-Propuesta
                Case Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngCertifProposal, Constantes.PolTransac.clngProposalQuery, Constantes.PolTransac.clngProposalConvertion

                    lstrCertype = Constantes.ePolCertype.cstrProposal

                    '+2-Póliza
                Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngPolicyAmendment, Constantes.PolTransac.clngTempPolicyAmendment, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngReprint, Constantes.PolTransac.clngdeclarations, Constantes.PolTransac.clngCoverNote, Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngPolicyQuotRenewal, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngQuotPropRenewalConvertion, Constantes.PolTransac.clngQuotRenewalQuery, Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngPropAmendentQuery, Constantes.PolTransac.clngPolicyPropRenewal, Constantes.PolTransac.clngCertifPropRenewal, Constantes.PolTransac.clngPropRenewalConvertion, Constantes.PolTransac.clngPropRenewalQuery

                    lstrCertype = Constantes.ePolCertype.cstrPolicy

                    '+3-Cotización
                Case Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngQuotationConvertion, Constantes.PolTransac.clngPropQuotConvertion

                    lstrCertype = Constantes.ePolCertype.cstrQuotation

                Case Else
                    lstrCertype = Constantes.ePolCertype.cstrPolicy
            End Select
            GetPolCertype = lstrCertype
        End Get
    End Property

    '%GetPropQuotCertype: Obtiene el valor SCERTYPE según la transacción
    Private ReadOnly Property GetPropQuotCertype(ByVal nTransaction As Constantes.PolTransac) As Constantes.ePolCertype
        Get
            Dim lstrCertype As Constantes.ePolCertype
            Select Case nTransaction
                '+4-Cotización de modificación
                Case Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngQuotAmendentQuery

                    lstrCertype = Constantes.ePolCertype.cstrAmendQuot

                    '+5-Cotización de renovación
                Case Constantes.PolTransac.clngPolicyQuotRenewal, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngQuotPropRenewalConvertion, Constantes.PolTransac.clngQuotRenewalQuery

                    lstrCertype = Constantes.ePolCertype.cstrRenewalQuot

                    '+6-Propuesta de modificación
                Case Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngPropAmendentQuery

                    lstrCertype = Constantes.ePolCertype.cstrAmendProposal

                    '+7-Propuesta de renovación
                Case Constantes.PolTransac.clngPolicyPropRenewal, Constantes.PolTransac.clngCertifPropRenewal, Constantes.PolTransac.clngPropRenewalConvertion, Constantes.PolTransac.clngPropRenewalQuery

                    lstrCertype = Constantes.ePolCertype.cstrRenewalProposal

                Case Else
                    lstrCertype = Constantes.ePolCertype.cstrPolicy
            End Select
            GetPropQuotCertype = lstrCertype
        End Get
    End Property

    '% InsValStatusPol: Valida el estado actual de la póliza
    Private Function InsValStatusPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTransaction As Constantes.PolTransac, ByVal nGenTransac As Constantes.eGenPolTransac, ByVal sStatusva As String) As Integer
        Dim llngErrornum As Integer
        Dim lclsPolicy_his As ePolicy.Policy_his

        '+Si la póliza está saldada o prorrogada entonces solamente puede ser consultada
        If sStatusva = "7" And nGenTransac <> Constantes.eGenPolTransac.clngQuery Then

            llngErrornum = 3971

            '+Si la transacción es recuperación se valida que la póliza/certificado este incompleta
        ElseIf nTransaction = Constantes.PolTransac.clngRecuperation Then
            If sStatusva <> "3" Then
                If nCertif = 0 Then
                    llngErrornum = 3004
                Else
                    llngErrornum = 3007
                End If
            End If

            '+Si la transacción es modificación o re-emisión se valida que la póliza/certificado
            '+este válida
        ElseIf nGenTransac = Constantes.eGenPolTransac.clngAmend Or nGenTransac = Constantes.eGenPolTransac.clngAmendPropQuot Or nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then

            If sStatusva <> "1" And sStatusva <> "4" And sStatusva <> "5" Then

                '+Se válida si la póliza/certificado está anulado
                If sStatusva = "6" Then
                    llngErrornum = 3098
                Else
                    If nCertif = 0 Then
                        llngErrornum = 3720
                    Else
                        llngErrornum = 3723
                    End If
                End If
            Else
                '+Si la transacción es re-emisión se valida que la póliza no tenga movimientos posteriores
                '+a la emisión
                If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                    lclsPolicy_his = New ePolicy.Policy_his
                    If lclsPolicy_his.Find_Movement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then

                        If nCertif = 0 Then
                            llngErrornum = 3267
                        Else
                            llngErrornum = 3266
                        End If
                    End If
                    'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPolicy_his = Nothing
                End If
            End If

            '+Si es reimpresión se valida que la póliza este impresa
        ElseIf nTransaction = Constantes.PolTransac.clngReprint Then
            If sStatusva <> "1" And sStatusva <> "5" Then
                llngErrornum = 3988
            End If
        End If

        InsValStatusPol = llngErrornum
    End Function

    '% InsValCA001: Esta rutina realiza las validaciones de los campos de la página.
    '               CA001_K.aspx - Tratamiento de pólizas
    Public Function InsValCA001(ByVal sCodispl As String, ByVal nTransaction As Integer, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicydest As Double, ByVal nCertif As Double, ByVal sPolitype As String, ByVal dLedgerDate As Date, ByVal nUsercode As String, ByVal dExpDate As Date, ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByVal nSellChannel As Integer, ByVal nType_amend As Integer, ByVal nServ_order As Double, ByVal nPropQuot As Double, ByVal nDigit As Integer, ByVal nProp_reg As Integer, Optional ByVal nFolio As Double = 0) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA001 As eRemoteDB.Execute

        On Error GoTo insvalCA001_Err

        lrecinsvalCA001 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalCA001
            .StoredProcedure = "insCA001PKG.insvalCA001"
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicyDest", nPolicydest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPoliType", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdate", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpdate", dExpDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPropQuot", nPropQuot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProp_reg", nProp_reg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolio", nFolio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            InsValCA001 = .Confirm
        End With

insvalCA001_Err:
        If Err.Number Then
            InsValCA001 = "insvalCA001: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA001 = Nothing
    End Function

    '%ReaPropQuotPremium: obtiene el monto de prima asociada a la propuesta y
    '%                    el monto de prima a cobrar
    Private Function ReaPropQuotPremium(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecReaPropQuotPremium As eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'ReaPropQuotPremium'
        '+Información leída el 21/03/2003
        On Error GoTo ReaPropQuotPremium_Err
        lrecReaPropQuotPremium = New eRemoteDB.Execute
        With lrecReaPropQuotPremium
            .StoredProcedure = "ReaPropQuotPremium"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                ReaPropQuotPremium = True
                mdblFirstPremium = .FieldToClass("nFirstPremium")
                mdblnPremprop = .FieldToClass("nPremprop")
                .RCloseRec()
            End If
        End With

ReaPropQuotPremium_Err:
        If Err.Number Then
            ReaPropQuotPremium = False
        End If
        'UPGRADE_NOTE: Object lrecReaPropQuotPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaPropQuotPremium = Nothing
        On Error GoTo 0
    End Function

    '%ReaPolicy_QuotProp: retorna las cotizaciones pendientes de una póliza
    Public Function ReaPolicy_QuotProp(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nPropQuot As Integer, ByVal nStatquota As Certificat.Stat_quot, ByVal sQuotProp As String) As Boolean
        Dim lintCount As Integer
        Dim lblnSpecprop As Boolean
        Dim lrecReaPolicy_QuotProp As eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'ReaPolicy_QuotProp'
        '+Información leída el 20/11/2001
        On Error GoTo ReaPolicy_QuotProp_Err
        lrecReaPolicy_QuotProp = New eRemoteDB.Execute
        With lrecReaPolicy_QuotProp
            .StoredProcedure = "ReaPolicy_QuotProp"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPropQuot", nPropQuot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintCount = 1
                Do While Not .EOF
                    If lintCount = 1 Then
                        sQuotProp = .FieldToClass("nPolicy")
                    Else
                        sQuotProp = sQuotProp & ", " & .FieldToClass("nPolicy")
                    End If
                    lintCount = lintCount + 1
                    .RNext()
                Loop
                If lintCount > 1 Then
                    ReaPolicy_QuotProp = True
                End If
            End If
        End With

ReaPolicy_QuotProp_Err:
        If Err.Number Then
            ReaPolicy_QuotProp = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaPolicy_QuotProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaPolicy_QuotProp = Nothing
    End Function

    '% insPostCA851: se realizan la actualización de los campos de la forma CA851
    Public Function insPostCA851(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nAFP_Commiss As Double, ByVal dEffecdate As Date, ByVal nWaypay As Integer, ByVal sDirTyp As String, ByVal nBill_day As Integer, ByVal nOrigin As Integer, ByVal nAFP_Comm_Curr As Integer, ByVal nUsercode As Integer, Optional ByVal sDirTyp_old As String = "", Optional ByVal nCollector As Integer = 0) As Boolean
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsPostCA851 As eRemoteDB.Execute

        On Error GoTo insPostCA851_Err

        lrecinsPostCA851 = New eRemoteDB.Execute

        '+ Se invoca el SP para Postidar los campos de la transacción
        With lrecinsPostCA851
            .StoredProcedure = "insCA851PKG.insPostCA851"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWaypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp", sDirTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_commiss", nAFP_Commiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_comm_curr", nAFP_Comm_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp_old", sDirTyp_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA851 = .Run(False)
        End With

insPostCA851_Err:
        If Err.Number Then
            insPostCA851 = CBool("insPostCA851: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsPostCA851 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCA851 = Nothing
    End Function

    '% insValCA851: se realizan las validaciones de los campos de la forma CA851
    Public Function insValCA851(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nAFP_Commiss As Double, ByVal dEffecdate As Date, ByVal nWaypay As Integer, ByVal sDirTyp As String, ByVal nBill_day As Integer, ByVal nOrigin As Integer, ByVal nAFP_Comm_Curr As Integer) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA851 As eRemoteDB.Execute

        On Error GoTo insvalCA851_Err

        lrecinsvalCA851 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lrecinsvalCA851
            .StoredProcedure = "insCA851PKG.insvalCA851"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWaypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirtyp", sDirTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_commiss", nAFP_Commiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_comm_curr", nAFP_Comm_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("CA851", , , , , , lstrErrorAll)
            End If
            insValCA851 = .Confirm
        End With

insvalCA851_Err:
        If Err.Number Then
            insValCA851 = "insvalCA851: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA851 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA851 = Nothing
    End Function

    '% insValFall_Vul: Válida y calcula suma asegurada para fallecimiento
    Public Function insValFall_Vul(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sCodispl As String, ByVal nOption_new As Integer, ByVal nCapital As Double) As Boolean
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsValFall_Vul As eRemoteDB.Execute

        On Error GoTo insValFall_Vul_Err

        lrecinsValFall_Vul = New eRemoteDB.Execute

        '+ Se invoca el SP para Postidar los campos de la transacción
        With lrecinsValFall_Vul
            .StoredProcedure = "INSCALFALL_VUL"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption_new", nOption_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nErrors", nErrors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sErrors", sErrors, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insValFall_Vul = .Run(False)
            Me.nErrors = .Parameters("nErrors").Value
            Me.nCapital = .Parameters("nCapital").Value
            Me.sErrors = .Parameters("sErrors").Value
        End With

insValFall_Vul_Err:
        If Err.Number Then
            insValFall_Vul = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsValFall_Vul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValFall_Vul = Nothing
    End Function

    '% insPostVI7010. Esta rutina se encarga de realizar actualización de la ventana
    '% "Información general VUL" VI7010
    Public Function insPostVI7010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sClient As String, ByVal sFirstname As String, ByVal sLastname As String, ByVal sLastname2 As String, ByVal dBirthDat As Date, ByVal nAge As Integer, ByVal sSexclie As String, ByVal nSpeciality As Integer, ByVal sSmoking As String, ByVal nTyperisk As Integer, ByVal nCivilsta As Integer, ByVal nOption As Integer, ByVal nCapital As Integer, ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal sNomin_quote As String) As Boolean
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo insPostVI7010_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insVI7010PKG.inspostVI7010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstname", sFirstname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname", sLastname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname2", sLastname2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthDat", dBirthDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclie", sSexclie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCivilsta", nCivilsta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNomin_quote", sNomin_quote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostVI7010 = .Run(False)

        End With

insPostVI7010_Err:
        If Err.Number Then
            insPostVI7010 = CBool("insPostVI7010: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing

    End Function

    '% insValCA851: se realizan las validaciones de los campos de la forma VI7010
    Public Function insValVI7010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sClient As String, ByVal sFirstname As String, ByVal sLastname As String, ByVal sLastname2 As String, ByVal dBirthDat As Date, ByVal nAge As Integer, ByVal sSexclie As String, ByVal nSpeciality As Integer, ByVal sSmoking As String, ByVal nTyperisk As Integer, ByVal nCivilsta As Integer, ByVal nOption As Integer, ByVal nCapital As Integer, ByVal nCurrency As Integer, ByVal sNomin_quote As String) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalVI7010 As eRemoteDB.Execute

        On Error GoTo insvalVI7010_Err

        lrecinsvalVI7010 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lrecinsvalVI7010
            .StoredProcedure = "insVI7010PKG.insvalVI7010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstname", sFirstname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname", sLastname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname2", sLastname2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthDat", dBirthDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclie", sSexclie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCivilsta", nCivilsta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNomin_quote", sNomin_quote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("VI7010", , , , , , lstrErrorAll)
            End If
            insValVI7010 = .Confirm
        End With

insvalVI7010_Err:
        If Err.Number Then
            insValVI7010 = "insvalVI7010: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalVI7010 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalVI7010 = Nothing
    End Function

    '% insPostVI7011. Esta rutina se encarga de realizar actualización de la ventana
    '% "Coberturas" VI7011
    Public Function insPostVI7011(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo insPostVI7011_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insVI7011PKG.inspostVI7011"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostVI7011 = .Run(False)

        End With

insPostVI7011_Err:
        If Err.Number Then
            insPostVI7011 = CBool("insPostVI7011: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing

    End Function

    '% insValCA851: se realizan las validaciones de los campos de la forma VI7010
    Public Function insValVI7011(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sKey As String) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalVI7011 As eRemoteDB.Execute

        On Error GoTo insvalVI7011_Err

        lrecinsvalVI7011 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lrecinsvalVI7011
            .StoredProcedure = "insVI7011PKG.insvalVI7011"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("VI7011", , , , , , lstrErrorAll)
            End If
            insValVI7011 = .Confirm
        End With

insvalVI7011_Err:
        If Err.Number Then
            insValVI7011 = "insvalVI7011: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalVI7011 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalVI7011 = Nothing
    End Function

    '% InsValVI001: Realiza la validación de los campos a actualizar en la ventana VI001
    Public Function insValCAL901(ByVal sCodispl As String, ByVal dEffecdate_I As Date, ByVal dEffecdate_E As Date) As String
        Dim lobjErrors As eFunctions.Errors


        On Error GoTo insValCAL901_Err
        lobjErrors = New eFunctions.Errors

        With lobjErrors
            '+

            If dEffecdate_I = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 6128)
            End If
            If dEffecdate_E = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 6129)
            End If
            If dEffecdate_I <> eRemoteDB.Constants.dtmNull And dEffecdate_E <> eRemoteDB.Constants.dtmNull And dEffecdate_E < dEffecdate_I Then
                .ErrorMessage(sCodispl, 6130)
            End If
            insValCAL901 = .Confirm
        End With

insValCAL901_Err:
        If Err.Number Then
            insValCAL901 = "InsValCAL901: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        On Error GoTo 0
    End Function



    '% DateType_Amend: Se valida que la duración real de la póliza sea mayor o igual a la duración
    '% mínima establecida en el diseñador de productos
    Public Function insValDuration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dDate_Origi As Date, ByVal nTypDurins As Integer, ByVal nInsur_time As Integer) As Boolean
        Dim dDate_end As Date
        Dim lclsDurinsu_prod As eProduct.Durinsu_prod
        Dim lclsProduct_li As eProduct.Product
        Dim lclsRoleses As ePolicy.Roleses
        Dim lclsRoles As ePolicy.Roles
        Dim nDurAge As Short

        insValDuration = False
        If nInsur_time < 0 Then nInsur_time = 0

        If nTypDurins <> 4 And nTypDurins <> 5 And nTypDurins <> 6 Then

            '+ Se determina la fecha de fin de la póliza según el tipo de duración

            Select Case nTypDurins
                Case 2 '+ Anual
                    dDate_end = DateAdd(Microsoft.VisualBasic.DateInterval.Year, nInsur_time, dDate_Origi)
                Case 8 '+ Meses
                    dDate_end = DateAdd(Microsoft.VisualBasic.DateInterval.Month, nInsur_time, dDate_Origi)
                Case 9 '+ Días
                    dDate_end = DateAdd(Microsoft.VisualBasic.DateInterval.Day, nInsur_time, dDate_Origi)
            End Select

            lclsRoleses = New ePolicy.Roleses
            '% ydavila 14/01/2009
            '% Es necesario incluir la condiciones para este manejo cuando el tipo de duración es variable

            '+ Se recuperan los datos del asegurado de la póliza
            If lclsRoleses.Find_by_Policy(sCertype, nBranch, nProduct, nPolicy, nCertif, String.Empty, dEffecdate, 2) Then
                lclsRoles = lclsRoleses.Item(1)
                lclsProduct_li = New eProduct.Product

                '+ Si el tipo de duración es Edad Alcanzada, se calcula la edad actuarial del asegurado a la fecha original de la póliza
                If nTypDurins = 1 Then '+ Edad alcanzada
                    '+ Se determina la edad acctuarial del asegurado
                    Call lclsRoles.CalInsuAge(nBranch, nProduct, dDate_Origi, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking)
                    lclsDurinsu_prod = New eProduct.Durinsu_prod

                    If lclsRoles.mintInsuAge < nInsur_time Then

                        nDurAge = nInsur_time - lclsRoles.mintInsuAge

                        If lclsDurinsu_prod.Find(nBranch, nProduct, nInsur_time, dEffecdate, nTypDurins) Then
                            If lclsDurinsu_prod.nMinDurIns > 0 Then
                                If nDurAge < lclsDurinsu_prod.nMinDurIns Then
                                    insValDuration = True
                                    mintMinDurIns = lclsDurinsu_prod.nMinDurIns
                                End If
                            End If
                        End If
                    Else
                        '+ Si la edad del asegurado es mayor que la establecida para el asegurado no se puede emitir la póliza
                        If lclsDurinsu_prod.Find(nBranch, nProduct, nInsur_time, dEffecdate, nTypDurins) Then
                            If lclsDurinsu_prod.nMinDurIns > 0 Then
                                If nDurAge < lclsDurinsu_prod.nMinDurIns Then
                                    insValDuration = True
                                    mintMinDurIns = lclsDurinsu_prod.nMinDurIns
                                End If
                            Else
                                insValDuration = True
                                mintMinDurIns = 0
                            End If
                        End If
                    End If

                    'UPGRADE_NOTE: Object lclsDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsDurinsu_prod = Nothing

                    '+ En caso contrario se calcula edad actuarial del asegura al final de la póliza
                Else
                    '+ Se determina la edad acctuarial del aegurado
                    Call lclsRoles.CalInsuAge(nBranch, nProduct, dDate_end, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking)

                    '+ Se busca la edad máxima de permanencia en los datos del producto
                    If lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate) Then

                        If lclsProduct_li.nReagemax <> eRemoteDB.Constants.intNull Then
                            If lclsRoles.mintInsuAge > lclsProduct_li.nReagemax Then
                                lclsDurinsu_prod = New eProduct.Durinsu_prod

                                Call lclsRoles.CalInsuAge(nBranch, nProduct, dDate_Origi, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking)

                                nDurAge = lclsProduct_li.nReagemax - lclsRoles.mintInsuAge

                                If lclsDurinsu_prod.Find(nBranch, nProduct, nInsur_time, dEffecdate, nTypDurins) Then
                                    If lclsDurinsu_prod.nMinDurIns > 0 Then
                                        If nDurAge < lclsDurinsu_prod.nMinDurIns Then
                                            insValDuration = True
                                            mintMinDurIns = lclsDurinsu_prod.nMinDurIns
                                        End If
                                    End If
                                End If

                                'UPGRADE_NOTE: Object lclsDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lclsDurinsu_prod = Nothing
                            End If
                        End If
                    End If
                End If



                'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsRoles = Nothing
                'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsProduct_li = Nothing
            End If
        End If

DateType_Amend_Err:
        If Err.Number Then
            insValDuration = CBool("insValDuration: " & Err.Description)
        End If

        'UPGRADE_NOTE: Object lclsDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDurinsu_prod = Nothing
        'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct_li = Nothing
        'UPGRADE_NOTE: Object lclsRoleses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoleses = Nothing

        On Error GoTo 0
    End Function
End Class






