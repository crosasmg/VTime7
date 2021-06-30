Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Web
Imports System.IO

Public Class Policy
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Policy.cls                               $%'
    '% $Author:: Ljimenez                                   $%'
    '% $Date:: 15-10-09 20:55                               $%'
    '% $Revision:: 9                                        $%'
    '%-------------------------------------------------------%'

    '- Se define la lista enumerada eType, para diferenciar el tipo de compañía

    Enum CompanyType
        cstrInsurance = 1
        cstrReinsurance = 2
        cstrBrokerOrBrokerageFirm = 3
        cstrInsuranceReinsurance = 4
    End Enum

    '-Se definen las constantes globales para el manejo del estado de la póliza

    Enum TypeStatus_Pol
        cstrValid = 1 'Valido
        cstrInvalid = 2 'Invalido
        cstrIncomplete = 3 'En captura incompleta
        cstrPrintPendent = 4 'Pendiente por impresión
        cstrPrinted = 5 'Impreso
        cstrAnnuled = 6 'Anulada
        cstrSaldProrr = 7 'Saldado prorrogado
        cstrRansom = 8 'Rescatada
    End Enum

    '+ Constantes
    '+ Ejecucion proceso Calculo de Reserva
    Const clngGenCalcReserva As Short = 86

    '+ Column_name            Type                         Computed     Length      Prec  Scale Nullable      TrimTrailingBlanks    FixedLenNullInSource
    '+ --------------------------------------------------------------------------------------------------------------------------------------------------
    Public sCertype As String 'char          no           1                       no            yes                   no
    Public nBranch As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
    Public nProduct As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
    Public nPolicy As Double 'int           no           4           10    0     no            (n/a)                 (n/a)
    Public SCLIENT As String 'char          no           14                      yes           yes                   yes
    Public sAccounti As String 'char          no           1                       yes           yes                   yes
    Public sBussityp As String 'char          no           1                       yes           yes                   yes
    Public sCoinsuri As String 'char          no           1                       yes           yes                   yes
    Public sColinvot As String 'char          no           1                       yes           yes                   yes
    Public sColReint As String 'char          no           1                       yes           yes                   yes
    Public sColtimre As String 'char          no           1                       yes           yes                   yes
    Public sCommityp As String 'char          no           1                       yes           yes                   yes
    Public sDeclari As String 'char          no           1                       yes           yes                   yes
    Public sDirdebit As String 'char          no           1                       yes           yes                   yes
    Public sIndextyp As String 'char          no           1                       yes           yes                   yes
    Public sLeadinvo As String 'char          no           12                      yes           yes                   yes
    Public sLeadnoti As String 'char          no           12                      yes           yes                   yes
    Public sLeadpoli As String 'char          no           12                      yes           yes                   yes
    Public sPolitype As String 'char          no           1                       yes           yes                   yes
    Public sPropo_cert As String 'char          no           1                       yes           yes                   yes
    Public sRenewal As String 'char          no           1                       yes           yes                   yes
    Public sRevalapl As String 'char          no           1                       yes           yes                   yes
    Public sStatus_pol As String 'char          no           1                       yes           yes                   yes
    Public sSubstiti As String 'char          no           1                       yes           yes                   yes
    Public sTyp_Clause As String 'char          no           1                       yes           yes                   yes
    Public sTyp_Discxp As String 'char          no           1                       yes           yes                   yes
    Public sDocuTyp As String 'char          no           1                       yes           yes                   yes
    Public sTyp_module As String 'char          no           1                       yes           yes                   yes
    Public sNoNull As String 'char          no           1                       yes           yes                   yes
    Public sConColl As String 'char          no           1                       yes           yes                   yes
    Public sNumForm As String 'char          no           12                      yes           yes                   yes
    Public dChangdat As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public DCOMPDATE As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dDat_no_con As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dDate_Origi As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dStartdate As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public DEXPIRDAT As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public DISSUEDAT As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dMaximum_da As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dNulldate As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dPropodat As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public dNextReceip As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
    Public nAmoucomm As Double 'decimal       no           5           8     2     yes           (n/a)                 (n/a)
    Public NCAPITAL As Double 'decimal       no           9           12    0     yes           (n/a)                 (n/a)
    Public nColcladi As Double 'decimal       no           5           4     2     yes           (n/a)                 (n/a)
    Public nCommissi As Double 'decimal       no           5           4     2     yes           (n/a)                 (n/a)
    Public nIndexfac As Double 'decimal       no           5           5     2     yes           (n/a)                 (n/a)
    Public nLeadcomi As Double 'decimal       no           5           4     2     yes           (n/a)                 (n/a)
    Public nLeadexpe As Double 'decimal       no           5           5     2     yes           (n/a)                 (n/a)
    Public nLeadshare As Double 'decimal       no           5           4     2     yes           (n/a)                 (n/a)
    Public nParticip As Double 'decimal       no           5           5     2     yes           (n/a)                 (n/a)
    Public NPREMIUM As Double 'decimal       no           9           10    2     yes           (n/a)                 (n/a)
    Public nShare As Double 'decimal       no           5           4     2     yes           (n/a)                 (n/a)
    Public nPayfreq As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nIntermed As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nLast_certi As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nNote_adend As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nNote_benef As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nNote_comme As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nNote_condi As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nNote_cover As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nProponum As Double 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nQ_Certif As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public NTRANSACTIO As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nMov_histor As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nOficial_p As Integer 'int           no           4           10    0     yes           (n/a)                 (n/a)
    Public nCopies As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nLeadcomp As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nNo_convers As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nNotice As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nNullcode As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nOffice As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
    Public nDummy As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
    Public nOffice_own As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nTariff As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nUser_amend As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nUsercode As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nQuota As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public sType_prop As String 'char          no           1                       yes           yes                   yes
    Public sProrShort As String 'char          no           1                       yes           yes                   yes
    Public nDaysFQ As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nDaysSQ As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nCompany As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public nOfficeIns As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
    Public sProrata As String
    Public sOriginal As String 'char          no           20                      yes           yes                   yes
    Public nCod_Agree As Integer
    Public sLeg As String 'CHAR           1              Yes
    Public sInsubank As String 'char                       1                       yes
    Public nLegAmount As Double 'NUMBER        22    12      0 Yes
    Public nLegAmount_old As Double 'NUMBER        22    12      0 Yes
    Public sTypenom As String 'CHAR           1
    Public sNopayroll As String 'CHAR           1
    Public sColtpres As String 'Char
    Public sInd_Comm As String 'Char           1
    Public sCurrAcc As String 'char          no           1                       yes           yes                   yes
    Public nRepInsured As Integer
    Public nClaim_notice As Integer
    Public nIdproces As Double
    Public nTerm_grace As Integer
    Public nSpecialbusiness As Integer
    Public nTypeAccount As Integer

    Public nAgency As Integer 'number        no           5
    Public nOfficeAgen As Integer 'number        no           5
    Public nBordereaux As Integer
    Public sClient_dest As String
    Public sStatus As String
    Public nCredit As Double
    Public sCurrency As String
    Public nProcess As Short
    Public sMassive As String
    Public sRepPrintCov As String
    Public sReceipt_ind As String

    '-Propiedades auxiliares
    Public sDigit As String

    '-Se define la variable que contiene la fecha de fin de una modificacion temporal
    Public pdtmNulldate As Date

    '-Se define la variable que contiene la fecha de la última modificación
    Public pdtmLastChange As Date

    '- Variables auxiliares

    '- Variable que guarda el número de póliza de la cual se heredan las condiciones de asegurabilidad
    '- de las coberturas
    Public nPolhered As Integer

    '-Variables para la ejecucion del stored procedures upd_capital_premium
    Public dEffecdate As Date
    Public nCertif As Double
    Public sParticular As String
    Public sBrancht As String
    Public sDesBrokOffice As String
    Public sDesBrokCpany As String
    Public npProctype As Integer
    Public nNewPolicy As Integer
    Public ncount As Integer

    '- Código del ramo y nombre de la tabla particular del mismo
    Public nPartBranch As Integer
    Public sTabname As String

    Public nProvince As Integer
    Public dRescuedate As Date

    '- CA031 :Renovación : Indica si el campo certificado estará o no activo
    Public nCertifLock As Boolean

    '- CA031 :Renocavión : Nombre del intermediario
    Public sIntermediaName As String
    Public nIndexFactMn As Double
    Public sSimul As String

    '- Variables utilizadas en la CAC002
    Public sClient_Inter As String
    Public nWait_code As Integer
    Public sWait_des As String
    Public sShort_des As String
    Public NCURRENCY As Integer
    Public nExchange As Double
    Public sCliename As String
    Public sCliename_Inter As String
    Public sDesOfficeIns As String

    '- Variables utilizadas en la CAC003
    Public sDesBranch As String
    Public sDesProduct As String
    Public sDesOffice As String
    Public nRole As Integer
    Public nMaxCurr As Integer
    Public nCountCur As Integer

    '- Propiedad para obtener codigo de reporte generado
    Public sKey As String

    '- Tipo de movimiento en tabla historica
    Public nTypeMoveHis As Policy_his.ePolicyHisType

    '- Variable de la fecha de copia obtenida como la ulima fecha de modificaciones a la póliza
    Public dDateCopy As Date

    '- Variables privadas
    Private mclsCertificat As Certificat

    '- Variables usadas para carga inicial de datos de transaccion CA642
    Private mstrClient As String
    Private mdtmStartdate As Date
    Private mdtmExpirdat As Date
    Private mdtmChangdat As Date
    Private mdtmNextreceip As Date
    Private blnStatusprepp As Boolean 'Poliza pendiente de pago
    Private blnStatusprepc As Boolean 'Poliza pendiente de pago que se envió al cobro
    Private intPayfreq As Integer
    Private mdtmDateRecPay As Date 'Indica la fecha hasta el último recibo pagado
    Private mdtmDayForce As Date
    Private mdtmNewNextreceip As Date 'Indica la nueva fecha de próxima facturación

    '- Variables usadas en la FindVic001
    Public dEffecdateV As Date
    Public dDateResul As Date
    Public nAmount_pen As Double
    Public nAvailMax As Double
    Public nQuanti_pen As Integer
    Public dExp_dat_pre As Date
    Public dExpire As Date
    Public nLoans As Double
    Public nSalvage As Double
    Public nCap_reduc As Double
    Public nYears As Integer
    Public nMonths As Integer
    Public nCap_initial As Double
    Public nAmount As Double

    Public nOrigin As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)

    '- Número de Convenio
    Public nAgreement As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)

    'PDF Name
    Public sPDFName As String
    Public sPDFFullPath As String

    '- Variable para controlar los errores de la CA031
    Public nNumError As Integer

    '- Objeto interno para el manejo de la colección de certificados
    Private mColCertificat As Collection

    '%updPolHisNulldate : Procedimiento que realiza las actualizaciones respectivas del histórico de la póliza, actualizando dnulldate,
    Public Function updPolHisNulldate() As Boolean
        Dim lrecupdPolicy_his_nulldate As eRemoteDB.Execute

        On Error GoTo updPolHisNulldate_Err
        lrecupdPolicy_his_nulldate = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.updPolicy_his_nulldate'
        '+Información leída el 30/11/1999 03:37:15 PM

        With lrecupdPolicy_his_nulldate
            .StoredProcedure = "updPolicy_his_nulldate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", DEXPIRDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMov_histor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            updPolHisNulldate = .Run(False)
        End With

updPolHisNulldate_Err:
        If Err.Number Then
            updPolHisNulldate = False
        End If
        'UPGRADE_NOTE: Object lrecupdPolicy_his_nulldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_his_nulldate = Nothing
    End Function

    '%insUpdNullPolicy: Esta función se encarga de anular la póliza en caso de que sea un rescate total.
    Public Function insUpdNullPolicy() As Boolean
        Dim lrecupdPolicy_null As eRemoteDB.Execute

        On Error GoTo insUpdNullPolicy_Err

        lrecupdPolicy_null = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.updPolicy_null'
        '+Información leída el 21/01/2000 10:40:40

        insUpdNullPolicy = True

        With lrecupdPolicy_null
            .StoredProcedure = "updPolicy_null"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nTypeMoveHis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusPol", sStatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdNullPolicy = .Run(False)

        End With
        'UPGRADE_NOTE: Object lrecupdPolicy_null may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_null = Nothing

insUpdNullPolicy_Err:
        If Err.Number Then
            insUpdNullPolicy = False
        End If
        On Error GoTo 0
    End Function

    '% ClearFields: se inicializa el valor de las variables de la clase
    Private Sub ClearFields()
        sCertype = String.Empty
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        SCLIENT = String.Empty
        sAccounti = String.Empty
        nAmoucomm = eRemoteDB.Constants.intNull
        sBussityp = String.Empty
        NCAPITAL = eRemoteDB.Constants.intNull
        dChangdat = eRemoteDB.Constants.dtmNull
        sCoinsuri = String.Empty
        nColcladi = eRemoteDB.Constants.intNull
        sColinvot = String.Empty
        sColReint = String.Empty
        sColtimre = String.Empty
        nCommissi = eRemoteDB.Constants.intNull
        sCommityp = String.Empty
        nCopies = eRemoteDB.Constants.intNull
        dDat_no_con = eRemoteDB.Constants.dtmNull
        dDate_Origi = eRemoteDB.Constants.dtmNull
        sDeclari = String.Empty
        sDirdebit = String.Empty
        dStartdate = eRemoteDB.Constants.dtmNull
        DEXPIRDAT = eRemoteDB.Constants.dtmNull
        nIndexfac = eRemoteDB.Constants.intNull
        sIndextyp = String.Empty
        nIntermed = eRemoteDB.Constants.intNull
        DISSUEDAT = eRemoteDB.Constants.dtmNull
        nLast_certi = eRemoteDB.Constants.intNull
        nLeadcomi = eRemoteDB.Constants.intNull
        nLeadcomp = eRemoteDB.Constants.intNull
        nLeadexpe = eRemoteDB.Constants.intNull
        sLeadinvo = String.Empty
        sLeadnoti = String.Empty
        sLeadpoli = String.Empty
        nLeadshare = eRemoteDB.Constants.intNull
        dMaximum_da = eRemoteDB.Constants.dtmNull
        nNo_convers = eRemoteDB.Constants.intNull
        nNote_adend = eRemoteDB.Constants.intNull
        nNote_benef = eRemoteDB.Constants.intNull
        nNote_comme = eRemoteDB.Constants.intNull
        nNote_condi = eRemoteDB.Constants.intNull
        nNote_cover = eRemoteDB.Constants.intNull
        nNotice = eRemoteDB.Constants.intNull
        nNullcode = eRemoteDB.Constants.intNull
        nOffice = eRemoteDB.Constants.intNull
        nOffice_own = eRemoteDB.Constants.intNull
        nParticip = eRemoteDB.Constants.intNull
        nPayfreq = eRemoteDB.Constants.intNull
        sPolitype = String.Empty
        NPREMIUM = eRemoteDB.Constants.intNull
        sPropo_cert = String.Empty
        nProponum = eRemoteDB.Constants.intNull
        dPropodat = eRemoteDB.Constants.dtmNull
        sProrata = String.Empty
        nQ_Certif = eRemoteDB.Constants.intNull
        sRenewal = String.Empty
        sRevalapl = String.Empty
        nShare = eRemoteDB.Constants.intNull
        sProrShort = String.Empty
        nMov_histor = eRemoteDB.Constants.intNull
        sStatus_pol = String.Empty
        sSubstiti = String.Empty
        nTariff = eRemoteDB.Constants.intNull
        NTRANSACTIO = eRemoteDB.Constants.intNull
        sTyp_Clause = String.Empty
        sTyp_Discxp = String.Empty
        sDocuTyp = String.Empty
        sTyp_module = String.Empty
        nUser_amend = eRemoteDB.Constants.intNull
        nUsercode = eRemoteDB.Constants.intNull
        dNextReceip = eRemoteDB.Constants.dtmNull
        nQuota = eRemoteDB.Constants.intNull
        sNoNull = String.Empty
        sConColl = String.Empty
        sType_prop = String.Empty
        nOficial_p = eRemoteDB.Constants.intNull
        nDummy = eRemoteDB.Constants.intNull
        sNumForm = String.Empty
        nDaysFQ = eRemoteDB.Constants.intNull
        nDaysSQ = eRemoteDB.Constants.intNull
        nCompany = eRemoteDB.Constants.intNull
        sOriginal = String.Empty
        nOfficeIns = eRemoteDB.Constants.intNull
        nCod_Agree = eRemoteDB.Constants.intNull
        nAgency = eRemoteDB.Constants.intNull
        nOfficeAgen = eRemoteDB.Constants.intNull
        sInsubank = String.Empty
        nLegAmount = eRemoteDB.Constants.intNull
        nPolhered = eRemoteDB.Constants.intNull
        sColtpres = String.Empty
        sInd_Comm = String.Empty
        nRepInsured = eRemoteDB.Constants.intNull
        nClaim_notice = eRemoteDB.Constants.intNull
        sRepPrintCov = CStr(eRemoteDB.Constants.intNull)
    End Sub

    '% Add: se actualza la tabla con los datos de las variables públicas de la clase
    Public Function Add() As Boolean
        Dim lrecinsPolicy As eRemoteDB.Execute

        On Error GoTo Add_err

        lrecinsPolicy = New eRemoteDB.Execute

        With lrecinsPolicy
            .StoredProcedure = "insPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAccounti", sAccounti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmoucomm", nAmoucomm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", NCAPITAL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dChangdat", dChangdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoinsuri", sCoinsuri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nColcladi", nColcladi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColreint", sColReint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommissi", nCommissi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCommityp", sCommityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCopies", nCopies, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDat_no_con", dDat_no_con, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_origi", dDate_Origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDeclari", sDeclari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirdebit", sDirdebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dstartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", DEXPIRDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndexfac", nIndexfac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndextyp", sIndextyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedat", DISSUEDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLast_certi", nLast_certi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLeadcomi", nLeadcomi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLeadcomp", nLeadcomp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLeadexpe", nLeadexpe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadinvo", sLeadinvo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadnoti", sLeadnoti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadpoli", sLeadpoli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLeadshare", nLeadshare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dMaximum_da", dMaximum_da, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_adend", nNote_adend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_benef", nNote_benef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_comme", nNote_comme, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_condi", nNote_condi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_cover", nNote_cover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotice", nNotice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_own", nOffice_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", NPREMIUM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPropo_cert", sPropo_cert, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPropodat", dPropodat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQ_certif", nQ_Certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewal", sRenewal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRevalapl", sRevalapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nShare", nShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProrShort", sProrShort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMov_histor", nMov_histor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus_pol", sStatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSubstiti", sSubstiti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", NTRANSACTIO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_clause", sTyp_Clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_discxp", sTyp_Discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUser_amend", nUser_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNoNull", sNoNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sConColl", sConColl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_prop", sType_prop, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOficial_p", nOficial_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaysFQ", nDaysFQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaysSQ", nDaysSQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCompany", IIf(nCompany = 0, System.DBNull.Value, nCompany), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nOfficeIns", IIf(nOfficeIns = 0, System.DBNull.Value, nOfficeIns), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInsubank", sInsubank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeg", sLeg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypenom", sTypenom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNopayroll", sNopayroll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtpres", sColtpres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_Comm", sInd_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCurrAcc", sCurrAcc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRepInsured", nRepInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim_Notice", nClaim_notice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRepPrintCov", sRepPrintCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceipt_Ind", sReceipt_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocuTyp", sDocuTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTerm_grace", nTerm_grace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeAccount", IIf(nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0, eRemoteDB.Constants.intNull, nTypeAccount), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSpecialbusiness", nSpecialbusiness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPolicy = Nothing
    End Function

    '%InsPolicy_CA004. Esta funcion se encarga de actualizar los campos del registro de la
    '%tabla Policy, para la poliza en tratamiento
    Public Function InsPolicy_CA004(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTransaction As Integer, ByVal sTitularC As String, ByVal sFreq As String, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal sIndexType As String, ByVal sIndexApl As String, ByVal sNoNull As String, ByVal dStartdate As Date, ByVal dExpirDate As Date, ByVal DISSUEDAT As Date, ByVal dReqDate As Date, ByVal nCopies As Integer, ByVal nIndexRate As Double, ByVal nDaysNull As Integer, ByVal sDeclarative As String, ByVal sFracti As String, ByVal sRenewalAut As String, ByVal nCodAgree As Integer, ByVal sInsubank As String, ByVal sNopayroll As String, ByVal nDays_quot As Integer, ByVal dEffecdate As Date, ByVal sReceipt_ind As String, Optional ByVal sLeg As String = "") As Boolean
        Dim lintCount As Integer
        Dim lintDaysVig As Integer
        Dim lintDaysForQ As Integer
        Dim lintDaysFirstQ As Integer

        Dim lclsPolicy As ePolicy.Policy
        Dim lclsProduct As eProduct.Product

        On Error GoTo InsPolicy_CA004_Err

        lclsPolicy = New Policy

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then

            With lclsPolicy

                '+Si la forma de pago es por cuota, se realizan los cálculos correspondientes a la cantidad de días de la
                '+primera cuota y cuotas subsiguientes.

                If Not nPayfreq = 0 Then
                    If sFreq = "3" And nPayfreq = 8 Then

                        '+Si ambas son fechas válidas
                        If IsDate(dStartdate) And Not (dStartdate = System.DateTime.FromOADate(eRemoteDB.Constants.intNull)) And IsDate(dExpirDate) And Not (dExpirDate = System.DateTime.FromOADate(eRemoteDB.Constants.intNull)) Then

                            '+Cálculo de "Días de la vigencia"
                            If DateDiff(Microsoft.VisualBasic.DateInterval.Year, dStartdate, dExpirDate) <> 0 Then
                                'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
                                lintDaysVig = (DateDiff(Microsoft.VisualBasic.DateInterval.Day, dStartdate, dExpirDate) / DateDiff(Microsoft.VisualBasic.DateInterval.Year, dStartdate, dExpirDate))
                            Else
                                'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
                                lintDaysVig = DateDiff(Microsoft.VisualBasic.DateInterval.Day, dStartdate, dExpirDate)
                            End If
                        Else
                            lintDaysVig = 0
                        End If
                        '+Cálculo de "Días de cada cuota"
                        lintDaysForQ = Int(lintDaysVig / nQuota)
                        '+Cálculo de "Días de la primera cuota"
                        lintDaysFirstQ = lintDaysVig - (lintDaysForQ * nQuota) + lintDaysForQ
                    Else
                        lintDaysVig = 0
                        lintDaysForQ = 0
                        lintDaysFirstQ = 0
                    End If
                Else
                    lintDaysVig = 0
                    lintDaysForQ = 0
                    lintDaysFirstQ = 0
                End If

                '+<Titular de recibo>
                If Trim(sTitularC) <> String.Empty Then
                    .SCLIENT = sTitularC
                End If

                '+<Número de copias>
                .nCopies = nCopies
                '+<Indicador de Póliza declarativa>
                .sDeclari = IIf(sDeclarative = CStr(System.Windows.Forms.CheckState.Checked), "1", "2")
                '+<Fecha de Vencimiento>
                If IsDate(dExpirDate) Then
                    .DEXPIRDAT = dExpirDate
                Else
                    .DEXPIRDAT = eRemoteDB.Constants.dtmNull
                End If
                '+<Tipo de revalorización>
                If Val(sIndexType) = 0 Then
                    .sIndextyp = String.Empty
                Else
                    .sIndextyp = sIndexType
                End If
                '+<Porcentaje de revalorización>
                .nIndexfac = nIndexRate
                '+<Fecha de Emisión>
                .DISSUEDAT = DISSUEDAT

                '+Se determina la fecha de próxima emisión de recibo de la póliza
                If sFreq = "3" Then
                    If Not nPayfreq = 0 Then
                        Select Case nPayfreq
                            '+ Si la frecuencia de pago es anual
                            Case 1
                                If nCertif = 0 And IsDate(dExpirDate) And Not Trim(CStr(dExpirDate)) = "" Then
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dStartdate)
                                Else
                                    .dNextReceip = eRemoteDB.Constants.dtmNull
                                End If
                                '+ Si la frecuencia de pago es semestral
                            Case 2
                                If nCertif = 0 Then
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, dStartdate)
                                End If
                                '+ Si la frecuencia de pago es trimestral
                            Case 3
                                If nCertif = 0 Then
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, dStartdate)
                                End If
                                '+ Si la frecuencia de pago es bimestral
                            Case 4
                                If nCertif = 0 Then
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, dStartdate)
                                End If
                                '+ Si la frecuencia de pago es mensual
                            Case 5
                                If nCertif = 0 Then
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dStartdate)
                                End If
                                '+ Si la frecuencia de pago es única
                            Case 6
                                .dNextReceip = eRemoteDB.Constants.dtmNull
                                '+ Si la frecuencia de pago es cuotas
                            Case 8
                                If nCertif = 0 Then
                                    '+ La fecha de próxima emisión del recibo es igual a la fecha de expiración de la póliza
                                    .dNextReceip = dExpirDate
                                End If

                            Case Else
                                If nCertif = 0 Then
                                    .dNextReceip = eRemoteDB.Constants.dtmNull
                                End If
                        End Select

                        '+ Si el producto en tratamiento es de vida restar 1 día a fecha proxima facturación del recibo
                        If .dNextReceip <> eRemoteDB.Constants.dtmNull Then
                            lclsProduct = New eProduct.Product
                            Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
                            If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Then
                                Select Case nTransaction
                                    Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngReprint, Constantes.PolTransac.clngdeclarations, Constantes.PolTransac.clngCoverNote
                                        .dNextReceip = System.DateTime.FromOADate(.dNextReceip.ToOADate - 1)
                                End Select
                            End If
                        End If
                    Else
                        .dNextReceip = eRemoteDB.Constants.dtmNull
                    End If
                Else
                    If nCertif = 0 Then
                        .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dStartdate)
                    End If
                End If

                '+<Días de aviso para anulación>
                .nNotice = IIf(nDaysNull = 0, 0, nDaysNull)
                '+<Frecuencia de pago>
                If Not nPayfreq = 0 Then
                    If sFreq = "3" Then
                        .nPayfreq = nPayfreq
                    Else
                        .nPayfreq = eRemoteDB.Constants.intNull
                    End If
                Else
                    .nPayfreq = eRemoteDB.Constants.intNull
                End If

                '+<Emisión Recepción>
                If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngCertifReissue Or nTransaction = Constantes.PolTransac.clngPolicyReissue Then
                    .dPropodat = dReqDate
                End If

                If sFracti = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .sProrShort = "9"
                Else
                    If sFreq = "1" Then
                        .sProrShort = CStr(1)
                    ElseIf sFreq = "2" Then
                        .sProrShort = CStr(2)
                    ElseIf sFreq = "3" Then
                        .sProrShort = CStr(3)
                    End If
                End If
                '+<Renovación automática>
                .sRenewal = IIf(sRenewalAut = CStr(System.Windows.Forms.CheckState.Checked), "1", "2")
                '+<Revalorización aplicación>
                If (sIndexApl = "0") Then
                    .sRevalapl = String.Empty
                Else
                    .sRevalapl = sIndexApl
                End If
                '+<Número de cuotas>
                .nQuota = CInt(0 & IIf(nQuota = eRemoteDB.Constants.intNull, 0, nQuota))
                '+<Exonerada>
                .sNoNull = IIf(sNoNull = CStr(System.Windows.Forms.CheckState.Checked), "1", "2")
                '+<Días de la primera cuota>
                .nDaysFQ = lintDaysFirstQ
                '+<Cuotas subsiguientes>
                .nDaysSQ = lintDaysForQ

                '+ Si se trata de una re-emisión o de re-impresión se cambia el estado de la póliza
                If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                    .sStatus_pol = "3"
                Else
                    If nTransaction = Constantes.PolTransac.clngReprint Then
                        .sStatus_pol = "4"
                    End If
                End If

                '+Convenio de pago
                .nCod_Agree = nCodAgree
                .nDummy = 0
                '+Calculo del LEG
                .sLeg = IIf(sLeg = "1", "1", "2")
                '+BancaSeguros
                .sInsubank = IIf(sInsubank = Trim(Str(System.Windows.Forms.CheckState.Checked)), "1", "2")
                '+ Póliza innominada
                .sNopayroll = IIf(sNopayroll = String.Empty, "2", sNopayroll)

                If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropRenewal Or nTransaction = Constantes.PolTransac.clngCertifQuotRenewal Then
                    '+ Días de validez de la propuesta o cotización
                    '               .nDays_quot = nDays_quot
                    .dMaximum_da = DateAdd(Microsoft.VisualBasic.DateInterval.Day, IIf(nDays_quot = eRemoteDB.Constants.intNull, 0, nDays_quot), dEffecdate)
                End If






                InsPolicy_CA004 = .Add
            End With
        End If

InsPolicy_CA004_Err:
        If Err.Number Then
            InsPolicy_CA004 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '%FindPolicyOfficeName: Esta rutina se encarga de validar la existencia de la póliza en la tabla
    '%'policy'
    Public Function FindPolicyOfficeName(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sCompanyType As String) As Boolean
        Dim lrecPolicy As eRemoteDB.Execute

        lrecPolicy = New eRemoteDB.Execute

        On Error GoTo FindPolicyOfficeName_Err

        With lrecPolicy
            .StoredProcedure = "reaPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompanyType", sCompanyType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sCertype = .FieldToClass("sCertype")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nPolicy = .FieldToClass("nPolicy")
                SCLIENT = .FieldToClass("sClient")
                sAccounti = .FieldToClass("sAccounti")
                sBussityp = .FieldToClass("sBussityp")
                sCoinsuri = .FieldToClass("sCoinsuri")
                sColinvot = .FieldToClass("sColinvot")
                sColReint = .FieldToClass("sColreint")
                sColtimre = .FieldToClass("sColtimre")
                sCommityp = .FieldToClass("sCommityp")
                sDeclari = .FieldToClass("sDeclari")
                sDirdebit = .FieldToClass("sDirdebit")
                sIndextyp = .FieldToClass("sIndextyp")
                sLeadinvo = .FieldToClass("sLeadinvo")
                sLeadnoti = .FieldToClass("sLeadnoti")
                sLeadpoli = .FieldToClass("sLeadpoli")
                sPolitype = .FieldToClass("sPolitype")
                sPropo_cert = .FieldToClass("sPropo_cert")
                sRenewal = .FieldToClass("sRenewal")
                sRevalapl = .FieldToClass("sRevalapl")
                sStatus_pol = .FieldToClass("sStatus_pol")
                sSubstiti = .FieldToClass("sSubstiti")
                sTyp_Clause = .FieldToClass("sTyp_clause")
                sTyp_Discxp = .FieldToClass("sTyp_discxp")
                sDocuTyp = .FieldToClass("sDocutyp")
                sTyp_module = .FieldToClass("sTyp_module")
                sNoNull = .FieldToClass("sNoNull")
                sConColl = .FieldToClass("sConColl")
                sNumForm = .FieldToClass("sNumForm")
                dChangdat = .FieldToClass("dChangdat")
                dDat_no_con = .FieldToClass("dDat_no_con")
                dDate_Origi = .FieldToClass("dDate_origi")
                dStartdate = .FieldToClass("dStartdate")
                DEXPIRDAT = .FieldToClass("dExpirdat")
                DISSUEDAT = .FieldToClass("dIssuedat")
                dMaximum_da = .FieldToClass("dMaximum_da")
                dNulldate = .FieldToClass("dNulldate")
                dPropodat = .FieldToClass("dPropodat")
                dNextReceip = .FieldToClass("dNextReceip")
                nAmoucomm = .FieldToClass("nAmoucomm")
                NCAPITAL = .FieldToClass("nCapital")
                nColcladi = .FieldToClass("nColcladi")
                nCommissi = .FieldToClass("nCommissi", 0)
                nIndexfac = .FieldToClass("nIndexfac")
                nLeadcomi = .FieldToClass("nLeadcomi")
                nLeadexpe = .FieldToClass("nLeadexpe")
                nLeadshare = .FieldToClass("nLeadshare")
                nParticip = .FieldToClass("nParticip")
                NPREMIUM = .FieldToClass("nPremium")
                nShare = .FieldToClass("nShare")
                nPayfreq = .FieldToClass("nPayfreq")
                nIntermed = .FieldToClass("nIntermed")
                nLast_certi = .FieldToClass("nLast_certi")
                nNote_adend = .FieldToClass("nNote_adend")
                nNote_benef = .FieldToClass("nNote_benef")
                nNote_comme = .FieldToClass("nNote_comme")
                nNote_condi = .FieldToClass("nNote_condi")
                nNote_cover = .FieldToClass("nNote_cover")
                nProponum = .FieldToClass("nPropoNum")
                nQ_Certif = .FieldToClass("nQ_certif")
                NTRANSACTIO = .FieldToClass("nTransactio")
                nMov_histor = .FieldToClass("nMov_histor")
                nOficial_p = .FieldToClass("nOficial_p")
                nCopies = .FieldToClass("nCopies")
                nLeadcomp = .FieldToClass("nLeadcomp")
                nNo_convers = .FieldToClass("nNo_convers")
                nNotice = .FieldToClass("nNotice")
                nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                nOffice = .FieldToClass("nOffice")
                nOffice_own = .FieldToClass("nOffice_own")
                nTariff = .FieldToClass("nTariff")
                nUser_amend = .FieldToClass("nUser_amend")
                nQuota = .FieldToClass("nQuota")
                sType_prop = .FieldToClass("sType_prop")
                sProrShort = .FieldToClass("sProrShort")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                nCompany = .FieldToClass("nCompany")
                nOfficeIns = .FieldToClass("nOfficeIns")
                sOriginal = .FieldToClass("sOriginal")
                nRepInsured = .FieldToClass("nRepInsured")
                If sCompanyType = CStr(CompanyType.cstrBrokerOrBrokerageFirm) Then
                    sDesBrokCpany = .FieldToClass("sNameCompany")
                    sDesBrokOffice = .FieldToClass("sNameOffice")
                End If
                FindPolicyOfficeName = True
            End If
        End With

FindPolicyOfficeName_Err:
        If Err.Number Then
            FindPolicyOfficeName = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function

    '% Find: se realiza la búsqueda de los datos de la póliza
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecPolicy As eRemoteDB.Execute

        On Error GoTo Find_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or lblnFind Then

            lrecPolicy = New eRemoteDB.Execute
            '+ Definición de parámetros para stored procedure 'insudb.reaPolicy_branch'
            '+ Información leída el 29/06/1999 10:52:14 AM
            With lrecPolicy
                .StoredProcedure = "reaPolicy_Branch"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sCertype = .FieldToClass("sCertype")
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.nPolicy = nPolicy
                    SCLIENT = .FieldToClass("sClient")
                    sAccounti = .FieldToClass("sAccounti")
                    sBussityp = .FieldToClass("sBussityp")
                    sCoinsuri = .FieldToClass("sCoinsuri")
                    sColinvot = .FieldToClass("sColinvot")
                    sColReint = .FieldToClass("sColreint")
                    sColtimre = .FieldToClass("sColtimre")
                    sCommityp = .FieldToClass("sCommityp")
                    sDeclari = .FieldToClass("sDeclari")
                    sDirdebit = .FieldToClass("sDirdebit")
                    sIndextyp = .FieldToClass("sIndextyp")
                    sLeadinvo = .FieldToClass("sLeadinvo")
                    sLeadnoti = .FieldToClass("sLeadnoti")
                    sLeadpoli = .FieldToClass("sLeadpoli")
                    sPolitype = .FieldToClass("sPolitype")
                    sPropo_cert = .FieldToClass("sPropo_cert")
                    sRenewal = .FieldToClass("sRenewal")
                    sRevalapl = .FieldToClass("sRevalapl")
                    sStatus_pol = .FieldToClass("sStatus_pol")
                    sSubstiti = .FieldToClass("sSubstiti")
                    sTyp_Clause = .FieldToClass("sTyp_clause")
                    sTyp_Discxp = .FieldToClass("sTyp_discxp")
                    sDocuTyp = .FieldToClass("sDocutyp")
                    sTyp_module = .FieldToClass("sTyp_module")
                    sNoNull = .FieldToClass("sNoNull")
                    sConColl = .FieldToClass("sConColl")
                    sNumForm = .FieldToClass("sNumForm")
                    dChangdat = .FieldToClass("dChangdat")
                    dDat_no_con = .FieldToClass("dDat_no_con")
                    dDate_Origi = .FieldToClass("dDate_origi")
                    dStartdate = .FieldToClass("dStartdate")
                    DEXPIRDAT = .FieldToClass("dExpirdat")
                    DISSUEDAT = .FieldToClass("dIssuedat")
                    dMaximum_da = .FieldToClass("dMaximum_da")
                    dNulldate = .FieldToClass("dNulldate")
                    dPropodat = .FieldToClass("dPropodat")
                    dNextReceip = .FieldToClass("dNextReceip")
                    nAmoucomm = .FieldToClass("nAmoucomm")
                    NCAPITAL = .FieldToClass("nCapital")
                    nColcladi = .FieldToClass("nColcladi")
                    nCommissi = .FieldToClass("nCommissi", 0)
                    nIndexfac = .FieldToClass("nIndexfac")
                    nLeadcomi = .FieldToClass("nLeadcomi")
                    nLeadexpe = .FieldToClass("nLeadexpe")
                    nLeadshare = .FieldToClass("nLeadshare")
                    nParticip = .FieldToClass("nParticip")
                    NPREMIUM = .FieldToClass("nPremium")
                    nShare = .FieldToClass("nShare")
                    nPayfreq = .FieldToClass("nPayfreq")
                    nIntermed = .FieldToClass("nIntermed")
                    nLast_certi = .FieldToClass("nLast_certi")
                    nNote_adend = .FieldToClass("nNote_adend")
                    nNote_benef = .FieldToClass("nNote_benef")
                    nNote_comme = .FieldToClass("nNote_comme")
                    nNote_condi = .FieldToClass("nNote_condi")
                    nNote_cover = .FieldToClass("nNote_cover")
                    nProponum = .FieldToClass("nPropoNum")
                    nQ_Certif = .FieldToClass("nQ_certif")
                    NTRANSACTIO = .FieldToClass("nTransactio")
                    nMov_histor = .FieldToClass("nMov_histor")
                    nOficial_p = .FieldToClass("nOficial_p")
                    nCopies = .FieldToClass("nCopies")
                    nLeadcomp = .FieldToClass("nLeadcomp")
                    nNo_convers = .FieldToClass("nNo_convers")
                    nNotice = .FieldToClass("nNotice")
                    nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                    nOffice = .FieldToClass("nOffice")
                    nOffice_own = .FieldToClass("nOffice_own")
                    nTariff = .FieldToClass("nTariff")
                    nUser_amend = .FieldToClass("nUser_amend")
                    nQuota = .FieldToClass("nQuota")
                    sType_prop = .FieldToClass("sType_prop")
                    sProrShort = .FieldToClass("sProrShort")
                    nDaysFQ = .FieldToClass("nDaysFQ")
                    nDaysSQ = .FieldToClass("nDaysSQ")
                    nCompany = .FieldToClass("nCompany")
                    nOfficeIns = .FieldToClass("nOfficeIns")
                    sOriginal = .FieldToClass("sOriginal")
                    nCod_Agree = .FieldToClass("nCod_agree")
                    sInsubank = .FieldToClass("sInsubank")
                    sLeg = .FieldToClass("sLeg")
                    nSpecialbusiness = .FieldToClass("nSpecialbusiness")
                    nAgency = .FieldToClass("nAgency")
                    nOfficeAgen = .FieldToClass("nOfficeAgen")
                    sInsubank = .FieldToClass("sinsuBank")
                    nLegAmount = .FieldToClass("nLegAmount")
                    sTypenom = .FieldToClass("sTypenom")
                    sNopayroll = .FieldToClass("sNopayroll")
                    sColtpres = .FieldToClass("sColtpres")
                    sInd_Comm = .FieldToClass("sInd_Comm")
                    nUsercode = .FieldToClass("nUsercode")
                    sCurrAcc = .FieldToClass("sCurrAcc")
                    nRepInsured = .FieldToClass("nRepInsured")
                    nClaim_notice = .FieldToClass("nClaim_notice")
                    sMassive = .FieldToClass("sMassive")
                    sRepPrintCov = .FieldToClass("sRepPrintCov")
                    sReceipt_ind = .FieldToClass("sReceipt_Ind")
                    nTerm_grace = .FieldToClass("nTerm_grace")
                    nAgreement = .FieldToClass("nAgreement")

                    If sInd_Comm = String.Empty Then
                        sInd_Comm = "1"
                    End If

                    Find = True
                End If
            End With
        Else
            Find = True
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function

    '%GetNewPolicyCode: Obtiene el nuevo número de póliza ya validado de que no exista
    Public Function GetNewPolicyCode(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer) As Integer
        Dim lrecreaPolicyCodeNew As eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaPolicyCodeNew'
        'Información leída el 10/12/1999 09:57:42 AM
        lrecreaPolicyCodeNew = New eRemoteDB.Execute
        With lrecreaPolicyCodeNew
            .StoredProcedure = "reaPolicyCodeNew"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                GetNewPolicyCode = .Parameters("nPolicy").Value
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaPolicyCodeNew may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicyCodeNew = Nothing
    End Function

    '%Update_Capital_Premium: Actualiza el capital y prima de la póliza,
    '%                        y en la tabla de datos particulares
    Public Function Update_Capital_Premium() As Boolean
        Dim lrecinsUpdCapitalPremium As eRemoteDB.Execute

        On Error GoTo Update_Capital_Premium_Err
        lrecinsUpdCapitalPremium = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insUpdCapitalPremium'
        'Información leída el 30/11/1999 15:49:51
        With lrecinsUpdCapitalPremium
            .StoredProcedure = "insUpdCapitalPremium"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", NCAPITAL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", NPREMIUM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegamount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_Capital_Premium = .Run(False)
        End With

Update_Capital_Premium_Err:
        If Err.Number Then
            Update_Capital_Premium = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdCapitalPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdCapitalPremium = Nothing
        On Error GoTo 0
    End Function

    '% ValExistPolicyRec: Valida la existencia del registro de póliza
    Public Function ValExistPolicyRec(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sCompanyType As String, Optional ByVal sCertype As String = "2") As Boolean
        Dim lrecreaPolicy As eRemoteDB.Execute

        On Error GoTo ValExistPolicyRec_Err
        lrecreaPolicy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaPolicy'
        '+ Información leída el 31/10/2000 02:20:02 PM
        With lrecreaPolicy
            .StoredProcedure = "reaPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeCompany", sCompanyType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            ValExistPolicyRec = .Run
            If ValExistPolicyRec Then
                dChangdat = .FieldToClass("dChangdat")
                dDat_no_con = .FieldToClass("dDat_no_con")
                dDate_Origi = .FieldToClass("dDate_origi")
                DEXPIRDAT = .FieldToClass("dExpirdat")
                DISSUEDAT = .FieldToClass("dIssuedat")
                dMaximum_da = .FieldToClass("dMaximum_da")
                dNextReceip = .FieldToClass("dNextReceip")
                dNulldate = .FieldToClass("dNulldate")
                dPropodat = .FieldToClass("dPropodat")
                dStartdate = .FieldToClass("dStartdate")
                nAmoucomm = .FieldToClass("nAmoucomm")
                nBranch = .FieldToClass("nBranch")
                NCAPITAL = .FieldToClass("nCapital")
                nColcladi = .FieldToClass("nColcladi")
                nCommissi = .FieldToClass("nCommissi", 0)
                nCompany = .FieldToClass("nCompany")
                nCopies = .FieldToClass("nCopies")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                nIndexfac = .FieldToClass("nIndexfac")
                nIntermed = .FieldToClass("nIntermed")
                nLast_certi = .FieldToClass("nLast_certi")
                nLeadcomi = .FieldToClass("nLeadcomi")
                nLeadcomp = .FieldToClass("nLeadcomp")
                nLeadexpe = .FieldToClass("nLeadexpe")
                nLeadshare = .FieldToClass("nLeadshare")
                nMov_histor = .FieldToClass("nMov_histor")
                nNo_convers = .FieldToClass("nNo_convers")
                nNote_adend = .FieldToClass("nNote_adend")
                nNote_benef = .FieldToClass("nNote_benef")
                nNote_comme = .FieldToClass("nNote_comme")
                nNote_condi = .FieldToClass("nNote_condi")
                nNote_cover = .FieldToClass("nNote_cover")
                nNotice = .FieldToClass("nNotice")
                nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                nOffice = .FieldToClass("nOffice")
                nOffice_own = .FieldToClass("nOffice_own")
                nOfficeIns = .FieldToClass("nOfficeIns")
                nOficial_p = .FieldToClass("nOficial_p")
                nParticip = .FieldToClass("nParticip")
                nPayfreq = .FieldToClass("nPayfreq")
                nPolicy = .FieldToClass("nPolicy")
                NPREMIUM = .FieldToClass("nPremium")
                nProduct = .FieldToClass("nProduct")
                nProponum = .FieldToClass("nPropoNum")
                nQ_Certif = .FieldToClass("nQ_certif")
                nQuota = .FieldToClass("nQuota")
                nShare = .FieldToClass("nShare")
                nTariff = .FieldToClass("nTariff")
                NTRANSACTIO = .FieldToClass("nTransactio")
                sAccounti = .FieldToClass("sAccounti")
                sBussityp = .FieldToClass("sBussityp")
                sCertype = .FieldToClass("sCertype")
                SCLIENT = .FieldToClass("sClient")
                sCoinsuri = .FieldToClass("sCoinsuri")
                sColinvot = .FieldToClass("sColinvot")
                sColReint = .FieldToClass("sColreint")
                sColtimre = .FieldToClass("sColtimre")
                sCommityp = .FieldToClass("sCommityp")
                sConColl = .FieldToClass("sConColl")
                sDeclari = .FieldToClass("sDeclari")
                sDirdebit = .FieldToClass("sDirdebit")
                sIndextyp = .FieldToClass("sIndextyp")
                sLeadinvo = .FieldToClass("sLeadinvo")
                sLeadnoti = .FieldToClass("sLeadnoti")
                sLeadpoli = .FieldToClass("sLeadpoli")
                sNoNull = .FieldToClass("sNoNull")
                sNumForm = .FieldToClass("sNumForm")
                sOriginal = .FieldToClass("sOriginal")
                sPolitype = .FieldToClass("sPolitype")
                sPropo_cert = .FieldToClass("sPropo_cert")
                sProrShort = .FieldToClass("sProrShort")
                sRenewal = .FieldToClass("sRenewal")
                sRevalapl = .FieldToClass("sRevalapl")
                sStatus_pol = .FieldToClass("sStatus_pol")
                sSubstiti = .FieldToClass("sSubstiti")
                sTyp_Clause = .FieldToClass("sTyp_clause")
                sTyp_Discxp = .FieldToClass("sTyp_discxp")
                sDocuTyp = .FieldToClass("sDocuTyp")
                sTyp_module = .FieldToClass("sTyp_module")
                sType_prop = .FieldToClass("sType_prop")
                nRepInsured = .FieldToClass("nRepInsured")
                .RCloseRec()
            End If
        End With

ValExistPolicyRec_Err:
        If Err.Number Then
            ValExistPolicyRec = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy = Nothing
        On Error GoTo 0
    End Function

    '% Find_PolicyPropo: verifica si la propuesta se encuentra registrada para otra póliza
    Public Function Find_PolicyPropo(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sNumForm As String) As Boolean
        Dim lrecreaPolicy_propo As eRemoteDB.Execute

        On Error GoTo Find_PolicyPropo_Err
        lrecreaPolicy_propo = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaPolicy_propo'
        '+ Información leída el 06/11/2000 02:27:52 p.m.
        With lrecreaPolicy_propo
            .StoredProcedure = "reaPolicy_propo"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_PolicyPropo = .Run
        End With

Find_PolicyPropo_Err:
        If Err.Number Then
            Find_PolicyPropo = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicy_propo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_propo = Nothing
    End Function

    '% Find_Proposal_Pol: verifica si la propuesta se encuentra registrada
    Public Function Find_Proposal_Pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
        Dim lrecreaProposal_Pol As eRemoteDB.Execute

        On Error GoTo Find_Proposal_Pol_Err
        lrecreaProposal_Pol = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaProposal_Pol'
        With lrecreaProposal_Pol
            .StoredProcedure = "reaProposal_Pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Proposal_Pol = .Run
        End With
        'UPGRADE_NOTE: Object lrecreaProposal_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProposal_Pol = Nothing

Find_Proposal_Pol_Err:
        If Err.Number Then
            Find_Proposal_Pol = False
        End If
        'UPGRADE_NOTE: Object lrecreaProposal_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProposal_Pol = Nothing
        On Error GoTo 0
    End Function

    '% Find_PolicyPropo: verifica si la propuesta se encuentra registrada para otra póliza
    Public Function Find_OriginalPolicy(ByVal nCompany As Integer, ByVal sOriginal As String) As Boolean
        Dim lrecreaOriginalPolicy As eRemoteDB.Execute

        On Error GoTo Find_OriginalPolicy_Err
        lrecreaOriginalPolicy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOriginalPolicy'
        '+ Información leída el 06/11/2000 02:31:25 p.m.
        With lrecreaOriginalPolicy
            .StoredProcedure = "reaOriginalPolicy"
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_OriginalPolicy = True
                ncount = .FieldToClass("nCount")
                .RCloseRec()
            End If
        End With

Find_OriginalPolicy_Err:
        If Err.Number Then
            Find_OriginalPolicy = False
        End If
        'UPGRADE_NOTE: Object lrecreaOriginalPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOriginalPolicy = Nothing
        On Error GoTo 0
    End Function

    '% Find_DatPolicy: Verifica que los datos del ramo y producto, concuerden con los definidos para la póliza
    Public Function Find_DatPolicy(ByVal sCertype As String, ByVal nPolicy As Integer) As Boolean
        Dim lreadatapolicy As eRemoteDB.Execute

        On Error GoTo Find_DatPolicy_Err
        lreadatapolicy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.Readatapolicy'
        '+ Información leída el 30/03/2007 4:35:40 p.m.
        With lreadatapolicy
            .StoredProcedure = "Readatapolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_DatPolicy = True
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                .RCloseRec()
            End If
        End With

Find_DatPolicy_Err:
        If Err.Number Then
            Find_DatPolicy = False
        End If
        'UPGRADE_NOTE: Object lreadatapolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreadatapolicy = Nothing
        On Error GoTo 0
    End Function

    '% Update_UserAmend: Actualiza el usuario que está modificando la póliza
    Public Function Update_UserAmend() As Boolean
        Dim lrecupdPolicyUserAmend As eRemoteDB.Execute

        lrecupdPolicyUserAmend = New eRemoteDB.Execute

        On Error GoTo Update_UserAmend_Err

        '+ Definición de parámetros para stored procedure 'insudb.updPolicyUserAmend'
        '+ Información leída el 06/11/2000 02:37:39 p.m.

        With lrecupdPolicyUserAmend
            .StoredProcedure = "updPolicyUserAmend"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_UserAmend = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdPolicyUserAmend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicyUserAmend = Nothing

Update_UserAmend_Err:
        If Err.Number Then
            Update_UserAmend = False
        End If
        On Error GoTo 0
    End Function

    '% valDeclaFreq:
    Public Function valDeclaFreq() As Boolean
        Dim lrecinsVerifyDeclaFreq As eRemoteDB.Execute

        On Error GoTo valDeclaFreq_Err
        lrecinsVerifyDeclaFreq = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insVerifyDeclaFreq'
        '+ Información leída el 06/11/2000 03:33:14 p.m.
        With lrecinsVerifyDeclaFreq
            .StoredProcedure = "insVerifyDeclaFreq"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", DEXPIRDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            valDeclaFreq = .Run
        End With

valDeclaFreq_Err:
        If Err.Number Then
            valDeclaFreq = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsVerifyDeclaFreq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVerifyDeclaFreq = Nothing
    End Function

    '% ConvertToPolicy: realiza la conversión de solicitud a póliza.
    '%                  sError se recibe por referencia, porque es utilizado luego de ejecutar el
    '%                  proceso de conversión
    Public Function ConvertToPolicy(Optional ByVal sError As String = "") As Boolean
        Dim lrecinsConvertion As eRemoteDB.Execute
        Dim lclsError As eFunctions.Errors

        On Error GoTo ConvertToPolicy_Err
        lrecinsConvertion = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insConvertion'
        '+ Información leída el 06/11/2000 05:49:11 p.m.

        With lrecinsConvertion
            .StoredProcedure = "insQuotPropConvertionPKG.insQuotPropConvertion"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInvalid", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype_des", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sCommit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sColtimre", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ConvertToPolicy = .Run(False)
            If ConvertToPolicy Then
                '+ Si no se pudo convertir la cotización.
                If .Parameters.Item("sInvalid").Value <> String.Empty Then
                    If .Parameters.Item("sInvalid").Value <> String.Empty Then
                        lclsError = New eFunctions.Errors
                        sError = lclsError.ErrorMessage("CA001_K", 55507, , eFunctions.Errors.TextAlign.RigthAling, " Certificado(s): " & .Parameters.Item("sInvalid").Value, True)
                    End If
                End If
            End If
        End With

ConvertToPolicy_Err:
        If Err.Number Then
            ConvertToPolicy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsConvertion may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsConvertion = Nothing
        'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsError = Nothing
    End Function

    '% Convert_Sol: realiza la conversión de cotización a propuesta
    Public Function ConvertToCotizac() As Boolean
        Dim lrecinsConvertion2 As eRemoteDB.Execute

        lrecinsConvertion2 = New eRemoteDB.Execute

        On Error GoTo ConvertToCotizac_Err

        '+ Definición de parámetros para stored procedure 'insudb.insConvertion2'
        '+ Información leída el 06/11/2000 05:50:15 p.m.

        With lrecinsConvertion2
            .StoredProcedure = "insQuotPropConvertionPKG.insQuotPropConvertion"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nNewPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInvalid", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype_des", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sCommit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sColtimre", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ConvertToCotizac = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsConvertion2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsConvertion2 = Nothing

ConvertToCotizac_Err:
        If Err.Number Then
            ConvertToCotizac = False
        End If
        On Error GoTo 0
    End Function

    '% Find_TabNameB: se buscan el nombre de la tabla particular del ramo
    Public Function Find_TabNameB(ByVal nBranch As Integer) As Boolean
        Dim lrecreaTab_name_b As eRemoteDB.Execute

        On Error GoTo Find_TabNameB_Err
        lrecreaTab_name_b = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaTab_name_b'
        '+ Información leída el 14/11/2000 11:16:30 a.m.
        With lrecreaTab_name_b
            .StoredProcedure = "reaTab_name_b"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_TabNameB = True
                nPartBranch = .FieldToClass("nBranch")
                sTabname = .FieldToClass("sTabname")
                .RCloseRec()
            End If
        End With

Find_TabNameB_Err:
        If Err.Number Then
            Find_TabNameB = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaTab_name_b may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_name_b = Nothing
    End Function

    '% InsPolicy_CA006:
    Public Function InsPolicy_CA006(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nUser As Integer, ByVal sColinvot As String, ByVal sColReint As String, ByVal sColtimre As String, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal sTyp_Clause As String, ByVal sTyp_Discxp As String, ByVal sDocuTyp As String, ByVal sTyp_module As String, ByVal nTransaction As Integer, ByVal sColtpres As String) As Boolean
        Dim lclsPolicy As Policy

        On Error GoTo InsPolicy_CA006_Err

        lclsPolicy = New Policy

        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)

        If nTariff = eRemoteDB.Constants.intNull Then
            nTariff = 0
        End If

        With lclsPolicy
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nUsercode = nUser
            .sColinvot = sColinvot
            .sColReint = sColReint
            .sColtimre = sColtimre
            .nQ_Certif = nCertif
            .nTariff = nTariff
            .sTyp_Clause = sTyp_Clause
            .sTyp_Discxp = sTyp_Discxp
            .sDocuTyp = sDocuTyp
            .sTyp_module = sTyp_module
            .sColtpres = sColtpres

            If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                .sStatus_pol = "3"
            Else
                If nTransaction = Constantes.PolTransac.clngReprint Then
                    .sStatus_pol = "4"
                End If
            End If
            .nDummy = 0
            InsPolicy_CA006 = .Add
        End With

InsPolicy_CA006_Err:
        If Err.Number Then
            InsPolicy_CA006 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% Delete_Policy: Rutina que borra la póliza de todas las tablas involucradas
    Public Function Delete_Policy(ByVal nCodeProce As Integer, ByVal nReference As Integer) As Boolean
        Dim lrecdelPolicy As eRemoteDB.Execute

        On Error GoTo Delete_Policy_Err
        lrecdelPolicy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.delPolicy'
        '+ Información leída el 18/07/2000 11.32.36
        With lrecdelPolicy
            .StoredProcedure = "delPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_activ", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_proce", nCodeProce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete_Policy = .Run(False)
        End With

Delete_Policy_Err:
        If Err.Number Then
            Delete_Policy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdelPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelPolicy = Nothing
    End Function

    '% Find_DateMax_Type_Doc: Retorna un sólo registro, proporcionando los campos fecha máxima y tipo de documento (normal y cobertura provisoria).*/
    Public Function Find_DateMax_Type_Doc(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaPolicy_o As eRemoteDB.Execute

        On Error GoTo Find_DateMax_Type_Doc_Err
        lrecreaPolicy_o = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or lblnFind Then

            '+Definición de parámetros para stored procedure 'insudb.reaPolicy_o'
            '+Información leída el 20/12/2000 11:07:44
            With lrecreaPolicy_o
                .StoredProcedure = "reaPolicy_o"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sCertype = sCertype
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nPolicy = nPolicy
                    dMaximum_da = .FieldToClass("dMaximum_da")
                    sType_prop = .FieldToClass("sType_prop")
                    Find_DateMax_Type_Doc = True
                    .RCloseRec()
                End If
            End With
        Else
            Find_DateMax_Type_Doc = True
        End If

Find_DateMax_Type_Doc_Err:
        If Err.Number Then
            Find_DateMax_Type_Doc = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicy_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_o = Nothing
    End Function

    '% FindDateLastEdit: Busca la ultima fecha de modificación de la póliza.
    '%
    Public Function FindDateLastEdit(ByVal sCertype As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo FindDateLastEdit_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "FindDateLastEdit"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DdateLastEdit", Nothing, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                dDateCopy = .Parameters("DdateLastEdit").Value
                FindDateLastEdit = True
            End If
        End With

FindDateLastEdit_Err:
        If Err.Number Then
            FindDateLastEdit = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '%InsPolicy_Ca047
    Public Function InsPolicy_Ca047(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal dMaximum_da As Date, ByVal sType_prop As String, ByVal nTransaction As Integer) As Boolean

        On Error GoTo InsPolicy_Ca047_Err

        With Me
            If .Find(sCertype, nBranch, nProduct, nPolicy, True) Then
                .dMaximum_da = dMaximum_da
                .sType_prop = sType_prop

                '+ Si se trata de una re-emisión o de re-impresión se cambia el estado de la póliza
                If nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then
                    .sStatus_pol = "3"
                ElseIf nTransaction = Constantes.PolTransac.clngReprint Then
                    .sStatus_pol = "4"
                End If
                .nDummy = 0
                InsPolicy_Ca047 = .Add
            End If
        End With

InsPolicy_Ca047_Err:
        If Err.Number Then
            InsPolicy_Ca047 = False
        End If
        On Error GoTo 0
    End Function

    '% InsLoadCA047:
    Public Function InsLoadCA047(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        InsLoadCA047 = True
        If Find_DateMax_Type_Doc(sCertype, nBranch, nProduct, nPolicy) Then
            InsLoadCA047 = dMaximum_da <> eRemoteDB.Constants.dtmNull And sType_prop <> String.Empty
        End If
    End Function

    '% InsValDatePol: Valida la fecha de emisión de un recibo
    Public Function InsValDatePol(ByVal sCodispl As String, ByRef lclsErrors As eFunctions.Errors, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dGeneralDate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "", Optional ByVal nSituation As Integer = 0) As String
        Dim lclsGeneral As eGeneral.Ctrol_date
        Dim lobjObject As Object
        Dim lblnError As Boolean

        On Error GoTo InsValDatePol_Err
        InsValDatePol = String.Empty

        '+Se valida que la fecha no esté vacia
        With lclsErrors
            If dGeneralDate = eRemoteDB.Constants.dtmNull Then
                lblnError = True
                .ErrorMessage(sCodispl, 1012)
            End If

            '+Se valida que sea una fecha lógica
            If Not lblnError Then
                If Not IsDate(dGeneralDate) Then
                    lblnError = True
                    .ErrorMessage(sCodispl, 1001)
                End If
            End If

            '+Si no existe errores se pueden realizar las demás validaciones
            If Not lblnError Then
                If nCertif = 0 Then
                    lobjObject = New ePolicy.Policy
                    Call lobjObject.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType)
                Else
                    sCompanyType = CStr(nCertif)
                    lobjObject = New ePolicy.Certificat
                    Call lobjObject.FindCertificatToNull(sCertype, nBranch, nProduct, nPolicy, nCertif, nSituation)
                End If

                '+Se valida que la póliza esté entre el periodo de vigencia
                If Not (dGeneralDate >= lobjObject.dStartdate And dGeneralDate <= lobjObject.DEXPIRDAT) Then
                    lblnError = True
                    .ErrorMessage(sCodispl, 3086)
                End If

                '+Se valida que la fecha  debe ser posterior al periodo contable en vigor
                If Not lblnError Then
                    lobjObject = New eLedge.Ledger
                    If lobjObject.Find Then
                        If Not dGeneralDate >= lobjObject.dStart_date Then
                            lblnError = True
                            .ErrorMessage(sCodispl, 1006)
                        End If
                    End If
                End If

                '+Se valida que la fecha debe ser posterior al último proceso de asientos automáticos
                If Not lblnError Then
                    lclsGeneral = New eGeneral.Ctrol_date
                    If lclsGeneral.Find(1) Then
                        If Not dGeneralDate > lclsGeneral.dEffecdate Then
                            .ErrorMessage(sCodispl, 1008)
                        End If
                    End If
                End If
            End If

        End With

InsValDatePol_Err:
        If Err.Number Then
            InsValDatePol = "InsValDatePol: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGeneral = Nothing
        'UPGRADE_NOTE: Object lobjObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjObject = Nothing
    End Function

    '% InsLocalValPolDate:
    Public Function InsLocalValPolDate(ByVal sCodispl As String, ByRef lclsErrors As eFunctions.Errors, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dStartdate As Date = #12:00:00 AM#, Optional ByVal DEXPIRDAT As Date = #12:00:00 AM#, Optional ByVal dNewExpirdat As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "", Optional ByVal nSituation As Integer = 0) As String
        Dim lclsLedger As eLedge.Ledger
        Dim lclsctrol_date As eGeneral.Ctrol_date
        Dim lblnError As Boolean

        On Error GoTo InsLocalValPolDate_Err
        lclsLedger = New eLedge.Ledger
        lclsctrol_date = New eGeneral.Ctrol_date

        With lclsErrors
            '+Se valida que la fecha no esté vacia
            If DEXPIRDAT = eRemoteDB.Constants.dtmNull Then
                lblnError = True
                .ErrorMessage(sCodispl, 1012)
            End If

            '+Se valida que la póliza esté entre el período de vigencia
            If Not (DEXPIRDAT >= dStartdate And DEXPIRDAT <= dNewExpirdat) Then
                .ErrorMessage(sCodispl, 3086)
                lblnError = True
            End If

            '+Se valida que la fecha debe ser posterior al período contable en vigor
            If Not lblnError Then
                If lclsLedger.Find Then
                    If Not DEXPIRDAT >= lclsLedger.dStart_date Then
                        .ErrorMessage(sCodispl, 1006)
                        lblnError = True
                    End If
                End If
            End If

            '+Se valida que la fecha debe ser posterior al último proceso de asientos automáticos
            If Not lblnError Then
                If lclsctrol_date.Find(1) Then
                    If Not DEXPIRDAT > lclsctrol_date.dEffecdate Then
                        .ErrorMessage(sCodispl, 1008)
                    End If
                End If
            End If

        End With

InsLocalValPolDate_Err:
        If Err.Number Then
            InsLocalValPolDate = CStr(False)
        End If
        'UPGRADE_NOTE: Object lclsLedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLedger = Nothing
        'UPGRADE_NOTE: Object lclsctrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsctrol_date = Nothing
        On Error GoTo 0
    End Function

    '% InsValprovince: Valida que la póliza tenga provincia asociada
    Public Function InsValprovince() As Boolean
        Dim lrecinsCalProvince As eRemoteDB.Execute
        Dim lintProvince As Integer

        lrecinsCalProvince = New eRemoteDB.Execute
        On Error GoTo InsValprovince_Err

        '+ Definición de parámetros para stored procedure 'insudb.insCalProvince'
        '+ Información leída el 18/01/2001 05:01:20 p.m.

        With lrecinsCalProvince
            .StoredProcedure = "insCalProvince"
            .Parameters.Add("psCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("psClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnRunTyp", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnAddres", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                InsValprovince = .FieldToClass("nProvince") <> eRemoteDB.Constants.intNull
                nProvince = .FieldToClass("nProvince")
                .RCloseRec()
            End If
        End With

InsValprovince_Err:
        If Err.Number Then
            InsValprovince = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCalProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalProvince = Nothing
    End Function

    '% UpdateLastTransac: Permite actualizar el numero de transacciones ejecutadas a la póliza
    Public Function UpdateLastTransac(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsupdLastTransacPol As eRemoteDB.Execute

        On Error GoTo UpdateLastTransac_Err
        lrecinsupdLastTransacPol = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insupdLastTransacPol'
        '+ Información leída el 19/01/2001 09:39:01 a.m.
        With lrecinsupdLastTransacPol
            .StoredProcedure = "insupdLastTransacPol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                NTRANSACTIO = .Parameters("nTransactio").Value
                UpdateLastTransac = True
            End If
        End With

UpdateLastTransac_Err:
        If Err.Number Then
            UpdateLastTransac = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsupdLastTransacPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsupdLastTransacPol = Nothing
    End Function

    '%Certificat:
    Public ReadOnly Property Certificat(ByVal nCertif As Double) As Certificat
        Get

            nCertif = IIf(nCertif <> eRemoteDB.Constants.intNull, nCertif, 0)

            If mclsCertificat Is Nothing Then
                mclsCertificat = New Certificat
            End If

            Call mclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
            Certificat = mclsCertificat
        End Get
    End Property

    '*** Count: counts the number of elements in the collection
    '* Count: cuenta el número de elementos dentro de la colección
    Public ReadOnly Property CountCertificat() As Integer
        Get
            CountCertificat = mColCertificat.Count()
        End Get
    End Property

    '***NewEnum: Enumerates the collection for use in a For Each...Next loop
    '*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    '
    'NewEnum = mColCertificat._NewEnum
    '
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mColCertificat.GetEnumerator
    End Function

    '% Class_Initialize: se inicializan las variables de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Call ClearFields()

        mColCertificat = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%InsUpdPolicyCapital: Esta función se encarga de actualizar el capital en la tabla Policy
    Public Function InsUpdPolicyCapital(ByVal ldblCapital As Double) As Boolean
        Dim lrecupdPolicy_capital As eRemoteDB.Execute

        On Error GoTo InsUpdPolicyCapital_Err
        lrecupdPolicy_capital = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.updPolicy_capital'
        '+Información leída el 21/01/2000 10:37:11
        With lrecupdPolicy_capital
            .StoredProcedure = "updPolicy_capital"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", ldblCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsUpdPolicyCapital = .Run(False)
        End With

InsUpdPolicyCapital_Err:
        If Err.Number Then
            InsUpdPolicyCapital = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdPolicy_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_capital = Nothing
    End Function

    '% UpdateClientPolicy: Actualiza el cliente de una póliza
    Public Function UpdateClientPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal SCLIENT As String, ByVal nUsercode As Integer) As Boolean

        '- Se define la variable lrecupdPolicy_Client
        Dim lrecupdPolicy_Client As eRemoteDB.Execute

        On Error GoTo UpdateClientPolicy_Err
        lrecupdPolicy_Client = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.updPolicy_Client'
        '+Información leída el 27/03/2001 13:20:05
        With lrecupdPolicy_Client
            .StoredProcedure = "updPolicy_Client"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateClientPolicy = .Run(False)
        End With

UpdateClientPolicy_Err:
        If Err.Number Then
            UpdateClientPolicy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdPolicy_Client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_Client = Nothing
    End Function

    '%InsValCAC005_K: Esta función realiza las validaciones del Header de la Consulta de Ubicación del riesgo.
    Public Function InsValCAC005_K(ByVal sCodispl As String, ByVal nProvince As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        '+ Validación del campo región.
        On Error GoTo InsValCAC005_K_Err
        If nProvince = eRemoteDB.Constants.intNull Then
            lclsErrors = New eFunctions.Errors
            lclsErrors.ErrorMessage(sCodispl, 99153)
            InsValCAC005_K = lclsErrors.Confirm
        End If

InsValCAC005_K_Err:
        If Err.Number Then
            InsValCAC005_K = "InsValCAC005_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '% insPreCAC003: Crea un único objeto para la consulta de recibos/pólizas pendientes de imprimir
    Public Function insPreCAC003(ByVal nOffice As Integer, ByVal nBranch As Integer, ByVal nOption As Integer) As Object

        '+ La consulta es por póliza
        If nOption = 1 Then
            insPreCAC003 = New ePolicy.Policys
            '+ La consulta es por recibo
        Else
            insPreCAC003 = New eCollection.Premiums
        End If

        Call insPreCAC003.FindCAC003(nOffice, nBranch, True)
    End Function

    '% Count_PolicyCod_agree: Verifica la existencia de un determinado convenio de pago en la tabla policy.
    Public Function Count_PolicyCod_agree(Optional ByVal Cod_agree As Integer = 0, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lreaPolicyCod_agree As eRemoteDB.Execute

        On Error GoTo Count_PolicyCod_agree_Err
        lreaPolicyCod_agree = New eRemoteDB.Execute

        If Cod_agree = nCod_Agree And Not lblnFind Then
            Count_PolicyCod_agree = True
        Else
            With lreaPolicyCod_agree
                .StoredProcedure = "valPolicyCod_agree"
                .Parameters.Add("nCod_agree", Cod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    If .FieldToClass("lCount") = 1 Then
                        Count_PolicyCod_agree = True
                    End If
                    .RCloseRec()
                Else
                    Count_PolicyCod_agree = False
                End If
            End With
        End If

Count_PolicyCod_agree_Err:
        If Err.Number Then
            Count_PolicyCod_agree = False
        End If
        'UPGRADE_NOTE: Object lreaPolicyCod_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaPolicyCod_agree = Nothing
        On Error GoTo 0
    End Function

    '% TransactionCA001: Devuelve la descripción a mostrar en el campo Póliza
    Public Function TransactionCA001(ByVal nTransaction As Integer, Optional ByVal bHTMLTag As Boolean = False) As String
        On Error GoTo TransactionCA001_err
        Dim varAux As String
        TransactionCA001 = ""
        If nTransaction = eRemoteDB.Constants.intNull Or NTRANSACTIO = 0 Then
            TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "Policy")
        Else
            Select Case nTransaction
                Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngPolicyAmendment, Constantes.PolTransac.clngTempPolicyAmendment, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngReprint, Constantes.PolTransac.clngdeclarations, Constantes.PolTransac.clngCoverNote, Constantes.PolTransac.clngDuplPolicy

                    '+ Póliza
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "Policy")

                Case Constantes.PolTransac.clngInspections, Constantes.PolTransac.clngQuotAmendConvertion, Constantes.PolTransac.clngQuotRenewalConvertion, Constantes.PolTransac.clngPropAmendConvertion, Constantes.PolTransac.clngPropRenewalConvertion
                    '+ Póliza
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "Policy")

                Case Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngQuotationConvertion
                    '+ Cotización
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "Quotation")

                Case Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngCertifProposal, Constantes.PolTransac.clngProposalQuery, Constantes.PolTransac.clngProposalConvertion, Constantes.PolTransac.clngPropQuotConvertion, Constantes.PolTransac.clngModPropRehabQuery
                    '+ Propuesta
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "Proposal")

                Case Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngPolicyQuotAmendent
                    '+ Cotización de modificación
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "QuotationAmendment")

                Case Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent, Constantes.PolTransac.clngQuotPropAmendentConvertion, Constantes.PolTransac.clngPropAmendentQuery
                    '+ Solicitud de endoso
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "ProposalAmendment")

                Case Constantes.PolTransac.clngPolicyPropRenewal, Constantes.PolTransac.clngCertifPropRenewal, Constantes.PolTransac.clngQuotPropRenewalConvertion, Constantes.PolTransac.clngPropRenewalQuery
                    '+ Solicitud de Renovación
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "RenewalProposal")

                Case Constantes.PolTransac.clngPolicyQuotRenewal, Constantes.PolTransac.clngCertifQuotRenewal, Constantes.PolTransac.clngQuotRenewalQuery
                    '+ Cotización de Renovación
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "RenewalQuotation")
                    '+ Propuesta de rehabilitacion
                Case Constantes.PolTransac.clngProprehabilitate
                    TransactionCA001 = HttpContext.GetGlobalResourceObject("BackOfficeResource", "RehabilitationProposal")
            End Select
        End If

        If bHTMLTag Then
            TransactionCA001 = "<LABEL>" & TransactionCA001 & "</LABEL>"
        End If

TransactionCA001_err:
        If Err.Number Then
            TransactionCA001 = String.Empty
        End If
        On Error GoTo 0
    End Function

    '% insPostCAL005: graba los parámetros para la ejecución de la
    'renovación masiva
    Private Function insPostCAL005(ByVal sCodispl As String, ByVal dRendateFrom As Date, ByVal dRenDateTo As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nIntermedia As Integer, ByVal nUsercode As Integer, ByVal sTypeCompany As Integer, ByVal nTypeinfo As Integer, ByVal nRenewal As Integer, ByVal nAgency As Integer, ByVal sReceipt_ind As String) As Boolean
        Dim lrecinsRenewal As eRemoteDB.Execute

        On Error GoTo insPostCAL005_Err

        lrecinsRenewal = New eRemoteDB.Execute

        '+ Si se trata de una renovación definitiva nRenewal = 2 si no nRenewal = 1
        '+Definición de parámetros para stored procedure 'insudb.insRenewal'
        '+Información leída el 05/02/2001 04:49:28 PM

        With lrecinsRenewal
            .StoredProcedure = "insRenewal"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewdat_i", dRendateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewdat_e", dRenDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermedia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReval_year", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReval_mont", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", IIf(nRenewal = 2, 99, 98), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMasive", nTypeinfo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceipt_ind", sReceipt_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.sKey = .Parameters("sKey").Value
                insPostCAL005 = True
            End If
        End With
insPostCAL005_Err:
        If Err.Number Then
            insPostCAL005 = False
        End If
        'UPGRADE_NOTE: Object lrecinsRenewal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRenewal = Nothing
        On Error GoTo 0
    End Function

    '%insValCAL005:  Valida los valores incluídos en el frame de Renovación masiva
    Public Function insValCAL005(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dStartdate As Date, ByVal dEndDate As Date, ByRef lclsErrors As eFunctions.Errors, ByVal sPolitype As String) As String

        On Error GoTo insValCAL005_err

        Dim lclsReval_fact As eGeneral.Reval_fact

        Dim lclsField As eFunctions.valField = New eFunctions.valField
        '+ Indica si la fecha de inicio es válida
        Dim blnStartDate As Boolean
        '+ Indica si la fecha final es válida
        Dim blnEndDate As Boolean
        '+ Almacena el valor del año para la busqueda de los factores de revalorizacion
        Dim lintyear As Short
        '+ Almacena el valor del mes para la busqueda de los factores de revalorizacion
        Dim lintMonth As Short
        '+ Almacena el período desde para la busqueda de los factores de revalorizacion
        Dim ldStartdate As Date
        '+ Almacena el período hasta para la busqueda de los factores de revalorizacion
        Dim ldEndDate As Date
        '+ Almacena el valor del mes para la busqueda de los 12 meses anteriores
        Dim lintMonth_for As Short

        Dim sIndivind As String

        Dim sGroupind As String

        Dim lclsProduct As eProduct.Product

        lclsProduct = New eProduct.Product

        If nProduct > 0 Then
            If lclsProduct.Find(nBranch, nProduct, Today, True) Then
                sIndivind = lclsProduct.sIndivind
                sGroupind = lclsProduct.sGroupind

                '+Negocios individuales, producto no los permite
                If sPolitype = "1" And sIndivind = "2" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 11200)
                End If
                '+Negocios Colectivos/Masivos, producto no los permite
                If sPolitype = "2" And sGroupind = "2" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 11200)
                End If

            End If
        End If
        '+Se valida la fecha desde
        With lclsField
            .ErrEmpty = 3237
            .ErrInvalid = 1001
            .objErr = lclsErrors
            blnStartDate = .ValDate(dStartdate, , eFunctions.valField.eTypeValField.ValAll)

            '+Se valida la fecha hasta
            .ErrEmpty = 3239
            .ErrInvalid = 1001
            blnEndDate = .ValDate(dEndDate, , eFunctions.valField.eTypeValField.ValAll)

        End With

        '+ Se verifica que la fecha desde sea menor igual a la fecha hasta
        If blnStartDate Then
            If blnEndDate Then
                If dStartdate > dEndDate Then Call lclsErrors.ErrorMessage(sCodispl, 3108)
            End If
        End If

        '+ Valida que este registrado el factor de revalorizacion anual para el año inmediatamente anterior al período desde
        lintyear = Year(dStartdate) - 1
        lintMonth = Month(dStartdate)
        '+ Se comenta este manejo de revalorización

        '    If Not lclsReval_fact.IsExist(5, lintYear, lintMonth) Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 56100)
        '   End If
        lintyear = eRemoteDB.Constants.intNull
        lintMonth = eRemoteDB.Constants.intNull
        dChangdat = eRemoteDB.Constants.dtmNull

        '+ Valida que este registrado el factor de revalorizacion anual para el año inmediatamente anterior al período hasta
        lintyear = Year(dEndDate) - 1
        lintMonth = Month(dEndDate)

        '+ Se comenta este manejo de revalorización

        '    If Not lclsReval_fact.IsExist(5, lintYear, lintMonth) Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 3906)
        '    End If
        lintyear = eRemoteDB.Constants.intNull
        lintMonth = eRemoteDB.Constants.intNull

        '+ Valida que este registrado el factor de revalorizacion mensual para los ultimos 12 meses anteriores al período desde

        '+ Se comenta este manejo de revalorización

        '    For lintMonth_for = 0 To 11
        '        ldStartdate = DateAdd("m", lintMonth_for, dStartdate - 365)
        '        lintYear = Year(ldStartdate)
        '        lintMonth = Month(ldStartdate)
        '        If Not lclsReval_fact.IsExist(5, lintYear, lintMonth) Then
        '            Call lclsErrors.ErrorMessage(sCodispl, 56101)
        '            Exit For
        '        End If
        '    Next lintMonth_for
        lintyear = eRemoteDB.Constants.intNull
        lintMonth = eRemoteDB.Constants.intNull

        '+ Valida que este registrado el factor de revalorizacion mensual para los ultimos 12 meses anteriores al período hasta

        '+ Se comenta este manejo de revalorización
        '    For lintMonth_for = 0 To 11
        '        ldEndDate = DateAdd("m", lintMonth_for, dEndDate - 365)
        '        lintYear = Year(ldEndDate)
        '        lintMonth = Month(ldEndDate)
        '        If Not lclsReval_fact.IsExist(5, lintYear, lintMonth) Then
        '            Call lclsErrors.ErrorMessage(sCodispl, 3907)
        '            Exit For
        '        End If
        '    Next lintMonth_for

insValCAL005_err:
        If Err.Number Then
            insValCAL005 = "insValCAL005" & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsField = Nothing
        'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsReval_fact = Nothing
        On Error GoTo 0
    End Function

    '% insShowClient: Muestra el cliente de la póliza
    Public Sub insShowClient(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)

        On Error GoTo insShowClient_err

        Dim lrecreaRoles_a_name As eRemoteDB.Execute
        lrecreaRoles_a_name = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaRoles_a_name'
        'Información leída el 25/02/2000 10:22:44

        With lrecreaRoles_a_name
            .StoredProcedure = "reaRoles_a_name"
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", IIf(nCertif = 0, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.sCliename = .FieldToClass("sCliename")
                .RCloseRec()
            End If
        End With

insShowClient_err:
        If Err.Number Then
            Resume Next
        End If
        'UPGRADE_NOTE: Object lrecreaRoles_a_name may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles_a_name = Nothing
    End Sub

    '% insName: Busca el nombre dado el código del cliente
    Public Function insName(ByVal SCLIENT As String, ByVal blnInter As Boolean) As String

        On Error GoTo insName_err

        Dim lrecreaClient As eRemoteDB.Execute
        lrecreaClient = New eRemoteDB.Execute

        With lrecreaClient
            If blnInter Then
                .StoredProcedure = "reaClient"
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .StoredProcedure = "reaIntermedia"
                .Parameters.Add("nIntermed", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            If .Run Then
                insName = .FieldToClass("sCliename")
                .RCloseRec()
            End If
        End With

insName_err:
        If Err.Number Then
            insName = String.Empty
        End If
        'UPGRADE_NOTE: Object lrecreaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaClient = Nothing
        On Error GoTo 0
    End Function

    '%insValPolicy: realiza las validaciones sobre el número de póliza ingresado
    Public Function insValPolicy(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sTypeCompany As String) As Integer

        On Error GoTo insValPolicy_Err

        Dim lrecreaPolicy As eRemoteDB.Execute
        lrecreaPolicy = New eRemoteDB.Execute

        If nPolicy <= 0 Then
            insValPolicy = -2
        ElseIf nBranch > 0 And nProduct > 0 And nPolicy > 0 Then

            '+ Definición de parámetros para stored procedure 'insudb.reaPolicy'
            '+ Información leída el 27/07/2001 03:23:01 p.m.

            With lrecreaPolicy
                .StoredProcedure = "reaPolicy"
                .Parameters.Add("sCertype", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sTypeCompany", sTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    If .FieldToClass("nNullCode") > 0 Then
                        insValPolicy = 1
                    ElseIf .FieldToClass("sStatus_pol") <> "1" And .FieldToClass("sStatus_pol") <> "4" And .FieldToClass("sStatus_pol") <> "5" Then
                        insValPolicy = 2
                    Else

                        '+ Se valida que estén registrados los factores de revalorización necesarios
                        '+ Para la renovacion
                        sIndextyp = .FieldToClass("sIndextyp")
                        sPolitype = .FieldToClass("spolitype")
                        dNextReceip = .FieldToClass("dNextreceip")
                        sColtimre = .FieldToClass("sColtimre")
                        sRenewal = .FieldToClass("sRenewal")
                        sReceipt_ind = .FieldToClass("sReceipt_Ind")

                        If sPolitype <> "1" Then
                            If Trim(sColtimre) = "1" Then
                                sSimul = "1"
                            Else
                                sSimul = "0"
                            End If
                        End If

                        dStartdate = CDate(Format(.FieldToClass("dStartDate"), "yyyy/MM/dd"))
                        DEXPIRDAT = CDate(Format(.FieldToClass("dExpirdat"), "yyyy/MM/dd"))


                        If sPolitype <> "1" Then
                            If sSimul = "1" Then nCertif = 0
                            nCertifLock = Not Trim(sSimul) <> "1"
                        Else
                            nCertif = 0
                        End If
                        Call insShowClient(nBranch, nProduct, nPolicy, Me.nCertif, Today)

                        If .FieldToClass("nintermed") <> eRemoteDB.Constants.intNull Then
                            sIntermediaName = insName(.FieldToClass("nintermed"), False)
                            nIntermed = .FieldToClass("nintermed")
                        Else
                            sIntermediaName = String.Empty
                            nIntermed = 0
                        End If
                        .RCloseRec()
                    End If
                Else
                    insValPolicy = -1
                End If
            End With
        End If

insValPolicy_Err:
        If Err.Number Then
            insValPolicy = -1
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy = Nothing
    End Function

    '% insvalcertif: valida que el certificado exista en el sistema
    Private Function insValCertif(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sTypeCompany As String, Optional ByVal DEXPIRDAT As Date = eRemoteDB.Constants.dtmNull, Optional ByVal lblnIn As Boolean = False) As Integer
        Dim lstrNextReceipt As String
        Dim lrecreaCertificat As eRemoteDB.Execute

        On Error GoTo insValCertif_Err

        lrecreaCertificat = New eRemoteDB.Execute

        If nCertif < 0 Then
            insValCertif = -1
        ElseIf nCertif = 0 And sSimul <> "1" And sPolitype <> "1" Then
            insValCertif = -1
        ElseIf nCertif >= 0 And nBranch > 0 And nProduct > 0 And nPolicy <> 0 Then

            '+Definición de parámetros para stored procedure 'insudb.reaCertificat'
            '+Información leída el 05/02/2001 04:45:40 PM
            With lrecreaCertificat
                .StoredProcedure = "reaCertificat"
                .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    If Trim(.FieldToClass("sStatusva")) = "2" Or Trim(.FieldToClass("sStatusva")) = "3" Then
                        insValCertif = 2
                    ElseIf .FieldToClass("sClaimind") = "1" Then
                        insValCertif = 3
                    Else
                        Call insShowClient(nBranch, nProduct, nPolicy, nCertif, dEffecdate)
                        lstrNextReceipt = IIf(.FieldToClass("dNextReceip") = eRemoteDB.Constants.dtmNull, "", .FieldToClass("dNextReceip"))
                        If CDate(lstrNextReceipt) = CDate(DEXPIRDAT) Then
                            If CDbl(sRenewal) = 2 Then
                                insValCertif = 4
                            End If
                            If Not insvalRevalfact(nBranch, nProduct, nPolicy, sTypeCompany, CDate(lstrNextReceipt), .FieldToClass("dStartDate").ToString("MMM dd yyyy"), .FieldToClass("dExpirdat").ToString("MMM dd yyyy")) Then
                                Me.dNextReceip = CDate(Format(.FieldToClass("dNextreceip"), "yyyy/MM/dd"))
                                Me.dStartdate = CDate(Format(.FieldToClass("dStartDate"), "yyyy/MM/dd"))
                                Me.DEXPIRDAT = CDate(Format(.FieldToClass("dExpirdat"), "yyyy/MM/dd"))
                            End If
                        End If
                    End If
                    .RCloseRec()
                Else
                    insValCertif = 1
                End If
            End With
        End If

insValCertif_Err:
        If Err.Number Then
            insValCertif = 1
        End If
        'UPGRADE_NOTE: Object lrecreaCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat = Nothing
    End Function

    '% insvalRevalfact: Valida que existan los factores de revalorización necesarios
    '% para la renovación
    Private Function insvalRevalfact(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sTypeCompany As String, ByVal dNextReceipt As Date, ByVal sStartDate As String, ByVal sExpirdate As String) As Boolean
        Dim lrecinsvalRevalfact As eRemoteDB.Execute
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insvalRevalfact

        lrecinsvalRevalfact = New eRemoteDB.Execute
        lclsPolicy = New ePolicy.Policy

        Call lclsPolicy.FindPolicyOfficeName(CStr(2), nBranch, nProduct, nPolicy, sTypeCompany)

        insvalRevalfact = True

        If lclsPolicy.sIndextyp <> "1" And lclsPolicy.sIndextyp <> "2" Then Exit Function

        'Definición de parámetros para stored procedure 'insudb.insvalRevalfact'
        'Información leída el 21/02/2000 10:23:36

        With lrecinsvalRevalfact
            .StoredProcedure = "insvalRevalfact"
            .Parameters.Add("dRenDate", dNextReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndextyp", lclsPolicy.sIndextyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndexFactMn", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndexFactYe", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters("nresp").Value <> 0 Then
                    insvalRevalfact = False
                Else
                    Me.nIndexfac = .Parameters("nIndexFactYe").Value
                    Me.nIndexFactMn = .Parameters("nIndexFactMn").Value
                End If
            Else
                insvalRevalfact = False
            End If
        End With

insvalRevalfact:
        If Err.Number Then
            insvalRevalfact = False
        End If
        'UPGRADE_NOTE: Object lrecinsvalRevalfact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalRevalfact = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% insPostCA031: Realiza la renovación para una póliza (Renovación masiva o puntual)
    Public Function insPostCA031(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal sTypeCompany As String, ByVal nTypeinfo As Integer, ByVal nRenewal As Integer, ByVal dRendateFrom As Date, ByVal dRenDateTo As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nIntermedia As Integer, ByVal nAgency As Integer, ByVal sReceipt_ind As String) As Boolean

        '+ Ejecuta las actualzaciones según el tipo de información 1:Masiva, 2:Puntual
        If nTypeinfo = 1 Then
            insPostCA031 = insPostCAL005(sCodispl, dRendateFrom, dRenDateTo, nBranch, nProduct, nOffice, nOfficeAgen, nIntermedia, nUsercode, 0, nTypeinfo, nRenewal, nAgency, sReceipt_ind)
        Else
            insPostCA031 = insPostCA031P(sCodispl, nBranch, nProduct, nPolicy, nCertif, nUsercode, sTypeCompany, nTypeinfo, nRenewal, sReceipt_ind)
        End If
    End Function

    '%insValCA031_k: Realiza la validación de los campos a actualizar en la ventana CA031_k (Header)
    Public Function insValCA031_k() As String
        On Error GoTo insValCA031_k_Err

        insValCA031_k = String.Empty

insValCA031_k_Err:
        If Err.Number Then
            insValCA031_k = insValCA031_k & Err.Description
        End If
        On Error GoTo 0
    End Function



    '%insValCA031_k: Realiza la validación de los campos a actualizar en la ventana CA031 (Folder)
    Public Function insValCA031(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sTypeCompany As String, ByVal nTypeinfo As Integer, Optional ByVal dStartdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal dEndDate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal sPolitype As String = "") As String
        On Error GoTo insValCA031_Err

        Dim lclsErrors As eFunctions.Errors
        lclsErrors = New eFunctions.Errors

        '+ Renovación masiva
        If nTypeinfo = 1 Then
            insValCA031 = insValCAL005(sCodispl, nBranch, nProduct, dStartdate, dEndDate, lclsErrors, sPolitype)
        Else
            '+ Renovación puntual
            insValCA031 = insValCA031P(sCodispl, nBranch, nProduct, nPolicy, nCertif, sTypeCompany, lclsErrors)
        End If

        insValCA031 = lclsErrors.Confirm

insValCA031_Err:
        If Err.Number Then
            insValCA031 = insValCA031 & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function


    '% insPostCA031P: Realiza la renovación para una póliza (Renovación puntual)
    Private Function insPostCA031P(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal sTypeCompany As String, ByVal nTypeinfo As Integer, ByVal nRenewal As Integer, ByVal sReceipt_ind As String) As Object
        Dim lrecinsRenewal As eRemoteDB.Execute

        On Error GoTo insPostCA031P_Err

        lrecinsRenewal = New eRemoteDB.Execute

        Me.Find("2", nBranch, nProduct, nPolicy)

        insPostCA031P = False
        '+ Si se trata de una renovación definitiva : nRenewal = 2 si no nRenewal = 1

        '+Definición de parámetros para stored procedure 'insudb.insRenewal'
        '+Información leída el 05/02/2001 04:49:28 PM

        With lrecinsRenewal
            .StoredProcedure = "insRenewal"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCertif", IIf(sSimul = "1", System.DBNull.Value, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRenewdat_i", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRenewdat_e", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeagen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReval_year", nIndexfac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReval_mont", nIndexFactMn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", IIf(nRenewal = 2, 99, 98), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMasive", nTypeinfo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceipt_Ind", sReceipt_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostCA031P = True
                Me.sKey = .Parameters("sKey").Value
            End If
        End With

insPostCA031P_Err:
        If Err.Number Then
            insPostCA031P = False
        End If
        'UPGRADE_NOTE: Object lrecinsRenewal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRenewal = Nothing
    End Function

    '%insValCA031P: Realiza la validación de los campos a actualizar en la ventana CA031.
    '%              Información puntual.
    Private Function insValCA031P(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sTypeCompany As String, ByRef lclsErrors As eFunctions.Errors) As String
        Dim lclsReval_fact As eGeneral.Reval_fact
        Dim lintyear As Short
        Dim lintMonth As Short
        Dim lintMonth_for As Short
        Dim ldRenewal As Date
        Dim lclsCertificat As Certificat
        Dim strResult As String = ""

        Try

            lclsReval_fact = New eGeneral.Reval_fact
            lclsCertificat = New Certificat

            '+ Asignación del numero de error a la clase
            nNumError = insValPolicy(nBranch, nProduct, nPolicy, sTypeCompany)

            '+ Validacion de la póliza
            Select Case nNumError
                Case -2
                    '+ Debe incluir el número de la póliza
                    Call lclsErrors.ErrorMessage(sCodispl, 3003)
                Case -1
                    '+ Número de póliza no está registrado en el sistema
                    Call lclsErrors.ErrorMessage(sCodispl, 3001)
                Case 1
                    '+ La póliza se encuentra anulada
                    Call lclsErrors.ErrorMessage(sCodispl, 3098)
                Case 2
                    '+ La póliza no tiene estado válido.
                    Call lclsErrors.ErrorMessage(sCodispl, 3882)

            End Select

            '+ Validación de certificado solo cuando se haya indicado la póliza
            If nPolicy > 0 Then
                Select Case insValCertif(nBranch, nProduct, nPolicy, nCertif, sTypeCompany, , True)
                    Case -1
                        ' Debe incluir el número del certificado
                        If sColtimre <> "1" And CDbl(sPolitype) <> 1 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 3006)
                        End If
                    Case 1
                        '+ Certificado no existe en archivo de certificados
                        Call lclsErrors.ErrorMessage(sCodispl, 13908)
                    Case 2
                        '+ El certificado no está válido.
                        Call lclsErrors.ErrorMessage(sCodispl, 3883)
                    Case 3
                        '+ La póliza/certificado se encuentra en "proceso de siniestro total"
                        Call lclsErrors.ErrorMessage(sCodispl, 3947)
                    '+ La póliza/certificado no renueva y se encuentra en la fecha de la anualidad.
                    Case 4
                        Call lclsErrors.ErrorMessage(sCodispl, 60753)
                End Select
            End If

            '+ Validar que el ramo este lleno
            If nBranch <= 0 Then Call lclsErrors.ErrorMessage(sCodispl, 1022)

            '+ Validar que el producto este lleno
            If nProduct <= 0 Then Call lclsErrors.ErrorMessage(sCodispl, 1014)

            '+ Valida que este registrado el factor de revalorizacion anual
            If DEXPIRDAT = dNextReceip Then
                If sRevalapl <> "3" Then
                    If sIndextyp = "1" Then
                        lintyear = Year(dNextReceip) - 1
                        lintMonth = Month(dNextReceip)
                        If Not lclsReval_fact.IsExist(5, lintyear, lintMonth) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 3906)
                        End If
                    End If
                End If
            End If
            lintyear = eRemoteDB.Constants.intNull
            lintMonth = eRemoteDB.Constants.intNull

            If lclsCertificat.Find_Renewal("2", nBranch, nProduct, nPolicy, nCertif) Then
                Call lclsErrors.ErrorMessage(sCodispl, 56193)
            End If
            '+ Valida que este registrado el factor de revalorizacion mensual para los ultimos 12 meses anteriores
            If DEXPIRDAT = dNextReceip Then
                If sRevalapl <> "3" Then
                    If sIndextyp = "2" Then
                        For lintMonth_for = 0 To 11
                            ldRenewal = DateAdd(Microsoft.VisualBasic.DateInterval.Month, lintMonth_for, System.DateTime.FromOADate(dNextReceip.ToOADate - 365))
                            lintyear = Year(ldRenewal)
                            lintMonth = Month(ldRenewal)
                            If Not lclsReval_fact.IsExist(5, lintyear, lintMonth) Then
                                Call lclsErrors.ErrorMessage(sCodispl, 3907)
                                Exit For
                            End If
                        Next lintMonth_for
                    End If
                End If
            End If

            '+ Si la póliza tiene generación de recibo anticipada y es una póliza colectiva. Se revisa no se esté generando el primer recibo para obligar a generarlo por la transacción CA036 -  Generación de recibo
            If Me.sReceipt_ind = "2" And sPolitype = "2" Then
                If InsVal_Pend_Fact(CInt("2"), nBranch, nProduct, nPolicy, dStartdate) = 1 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 26204)
                End If
            End If

            If DEXPIRDAT = dNextReceip Then
                'la poliza no debe tener una propuesta pendiente para renovar.

                If reaPropfrompolicy("7", nBranch, nProduct, nPolicy, nCertif, 1, DEXPIRDAT) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55649)
                    'si la poliza no tiene una propuesta aprobada no puede renovar.
                ElseIf Not REAPROPRENEWPOL("7", nBranch, nProduct, nPolicy, nCertif, 2, DEXPIRDAT) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 9000036)
                End If
            End If

            'If sReceipt_ind = "1" And Today < dNextReceip Then
            'Call lclsErrors.ErrorMessage(sCodispl, 100139)
            'End If
            Return strResult
        Catch ex As Exception
            Return strResult = strResult & Err.Description
        Finally
            'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsReval_fact = Nothing
            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing
        End Try
    End Function

    '%insPostCA031_k: Se realiza la actualización de los datos en la ventana CA028_k (Header)
    Public Function insPostCA031_k() As Boolean
        insPostCA031_k = True
    End Function

    '**% insValCAL001: This function makes the validations of the CAL001 - "Printing policy documents" transaction.
    '%insValCAL001: Esta función realiza las validaciones de la transacciòn CAL001 - "Cuadro de pólizas".
    '% Modificacion : Felipe Lagos B. 09/01/2001
    Public Function insValCAL001(ByVal sCodispl As String, ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRe_im As Integer, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal dmoddate As Date = #12:00:00 AM#, Optional ByVal nOption As Integer = 0, Optional ByVal nTypeProponum As Integer = 0, Optional ByVal nProponum As Double = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat
        Dim lstrsCertype As String

        On Error GoTo insValCAL001_Err

        lobjErrors = New eFunctions.Errors
        insValCAL001 = String.Empty

        '+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
        '+ Póliza y certificado.

        If nType = 1 Then
            '+ la poliza debe estar llena
            If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 3003)
            End If
            '+ si el producto y la poliza tienen valor el ramo debe estar lleno
            If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 11135)
            End If
            '+ si la poliza tienen valor el producto debe estar lleno
            If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 1014)
            End If
            '+ si la poliza tienen valor debe existir en el sistema
            If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
                If Not ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                Else
                    If nRe_im <> 1 Then
                        If CDbl(Me.sStatus_pol) <> 4 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55551)
                        End If
                    End If
                End If
            End If
            '+ si el certificado tienen valor debe existir en el sistema
            If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
                lclsCertificat = New ePolicy.Certificat
                If Not lclsCertificat.Find("2", CInt(nBranch), CInt(nProduct), nPolicy, nCertif) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 8215)
                Else
                    If nRe_im <> 1 Then
                        If CDbl(lclsCertificat.sStatusva) <> 4 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55552)
                        End If
                    End If
                End If
            End If
            If (nOption = 1 Or nOption = 2) And nPolicy <= 0 Then
                Call lobjErrors.ErrorMessage(sCodispl, 3003)
            End If

            If nOption = 2 And dmoddate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 55534)
            End If

            If nOption < 1 Then
                Call lobjErrors.ErrorMessage(sCodispl, 60213)
            End If
            If nProponum <= 0 And (nOption = 3 Or nOption = 4) Then
                Call lobjErrors.ErrorMessage(sCodispl, 55943)
            End If

            If nProponum > 0 And (nOption = 3 Or nOption = 4) And nTypeProponum = 2 Then
                If nTypeProponum = 3 Then
                    lstrsCertype = CStr(4)
                Else
                    lstrsCertype = CStr(6)
                End If
                If Not Find(lstrsCertype, nBranch, nProduct, nProponum, True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 55651)
                End If
            End If

        Else

            '+ Si el tipo de ejecucion es "Masivo" se realizan las validaciones de los campos
            '+ Sucursal y Agencia.

            '+ si es masivo y ninguno de los campos tiene informacion
            If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency = eRemoteDB.Constants.intNull Or nAgency = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 55550)
            End If
            '+ si la agencia tiene informacion debe estar llena la sucursal
            If (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency <> eRemoteDB.Constants.intNull And nAgency <> 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 55520)
            End If
            '+ si el producto tiene informacion debe estar lleno el ramo
            If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 11135)
            End If
        End If


        insValCAL001 = lobjErrors.Confirm

insValCAL001_Err:
        If Err.Number Then
            insValCAL001 = "insValCAL001: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '**% insValCAL010: This function makes the validations of the CAL010 - "Printing policy documents" transaction.
    '%insValCAL010: Esta función realiza las validaciones de la transacciòn CAL010 - "Generador de Reportes".
    '% Modificacion : Patricia Moreno. 12/03/2007
    Public Function insValCAL010(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                 ByVal nTypeReport As Integer, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0,
                                 Optional ByVal nTypeletter As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nLetter As Double = 0,
                                 Optional ByVal nPolicy_Init As Double = 0, Optional ByVal nPolicy_End As Double = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsCertificata As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrsCertype As String
        Dim lclsPolicy_his As Policy_his

        On Error GoTo insValCAL010_Err

        lobjErrors = New eFunctions.Errors
        insValCAL010 = String.Empty

        '+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
        '+ Póliza y certificado.

        '+ El ramo debe estar lleno
        If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ El producto debe estar lleno
        If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
            'Call lobjErrors.ErrorMessage(sCodispl, 3003)
        End If

        '+ El tipo de reporte debe estar lleno
        If (nTypeReport = eRemoteDB.Constants.intNull Or nTypeReport = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 100102)
        End If

        '+ si el producto y el ramo tienen valor el tipo de reporte debe estar lleno
        If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nBranch = eRemoteDB.Constants.intNull And nBranch = 0) And (nTypeReport = eRemoteDB.Constants.intNull Or nTypeReport = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 100102)
        End If

        '+ si el producto tienen valor el ramo debe estar lleno
        If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ si la poliza tiene valor el ramo debe estar lleno
        If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ si la poliza tiene valor el tipo de reporte debe estar lleno
        If (nTypeReport = eRemoteDB.Constants.intNull Or nTypeReport = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 100102)
        End If
        '+Si el reporte es cuadro de poliza, se debe indicar el numero de póliza


        If nTypeReport <> 1 And nPolicy <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        End If
        '+ si la poliza tiene valor el producto debe estar lleno
        If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+ si la poliza tiene valor debe existir en el sistema
        If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            If Not ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
                Call lobjErrors.ErrorMessage(sCodispl, 3001)
            End If
        End If

        '+ si el certificado tiene valor poliza debe tener valor
        If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        End If

        '+ si el certificado tienen valor debe existir en el sistema
        If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
            lclsCertificat = New ePolicy.Certificat
            If Not lclsCertificat.Find("2", CInt(nBranch), CInt(nProduct), nPolicy, nCertif) Then
                Call lobjErrors.ErrorMessage(sCodispl, 8215)
            Else
                If (lclsCertificat.sStatusva = "2" Or lclsCertificat.sStatusva = "3") Then
                    Dim lclsTabGen As eGeneralForm.TabGen
                    lclsTabGen = New eGeneralForm.TabGen

                    If lclsTabGen.Find("TAB_WAITPO", lclsCertificat.nWait_code) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 750044, , eFunctions.Errors.TextAlign.RigthAling, IIf(lclsCertificat.nWait_code <> 0, ". Causal: " & lclsTabGen.sDescript, ""))
                    End If

                    lclsTabGen = Nothing
                End If
            End If
        End If

        '+ si el tipo de reporte es cuadro póliza
        If (nTypeReport = 1) Then
            If nPolicy_Init <> eRemoteDB.Constants.intNull Then
                If nPolicy_End = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3003, , eFunctions.Errors.TextAlign.RigthAling, " hasta")
                Else
                    If nPolicy_Init > nPolicy_End Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3621)
                    End If
                End If
            End If

            If nPolicy_End <> eRemoteDB.Constants.intNull Then
                If nPolicy_Init = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3003, , eFunctions.Errors.TextAlign.RigthAling, " desde")
                Else
                    If nPolicy_Init > nPolicy_End Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3621)
                    End If
                End If
            End If

            '+ si el tipo de reporte es carta
        ElseIf (nTypeReport = 2) Then
            If (nTypeletter = eRemoteDB.Constants.intNull Or nTypeletter = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 100103)
            End If

            If (dEffecdate = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Or dEffecdate = System.DateTime.FromOADate(0)) Then
                Call lobjErrors.ErrorMessage(sCodispl, 2056)
            End If

            If (nLetter = eRemoteDB.Constants.intNull Or nLetter = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 100104)
            End If
            If nTypeletter = 3 Then
                lclsCertificata = New ePolicy.Certificat
                If lclsCertificata.Find("2", CInt(nBranch), CInt(nProduct), nPolicy, nCertif) Then
                    If CDbl(lclsCertificata.sStatusva) = 2 Or CDbl(lclsCertificata.sStatusva) = 3 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 750044)
                    End If
                End If
            End If
            '+ si tipo de reporte tiene valor certificado de coberturas(3) y poliza tiene valor
            '+ sreprpintcov debe ser = 1.
        ElseIf (nTypeReport = 3) Then
            If (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
                lclsPolicy = New ePolicy.Policy
                If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    If Trim(lclsPolicy.sRepPrintCov) = "" Or lclsPolicy.sRepPrintCov = "2" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 100128)
                    End If
                End If
            End If
            '+Si se refiere al cuadro de endoso

        ElseIf (nTypeReport = 4) Then
            If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
                lclsPolicy_his = New Policy_his
                If lclsPolicy_his.FindLastMovementByTypes(sCertype, nBranch, nProduct, nPolicy, nCertif, "11, 12, 54, 55, 65") Then
                    dEffecdate = lclsPolicy_his.dEffecdate
                    Me.nMov_histor = lclsPolicy_his.nMovement
                    Me.NTRANSACTIO = lclsPolicy_his.nTransactio
                Else
                    Call lobjErrors.ErrorMessage(sCodispl, 56182, , eFunctions.Errors.TextAlign.LeftAling, "Poliza/Certificado. ")
                End If

            End If

        End If

        insValCAL010 = lobjErrors.Confirm

insValCAL010_Err:
        If Err.Number Then
            insValCAL010 = "insValCAL010: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsCertificata may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificata = Nothing
    End Function

    '**% insValCAL014: This function makes the validations of the CAL014 - "" transaction.
    '%insValCAL014: Esta función realiza las validaciones de la transacciòn CAL014 - "Nomina de asegurados con DPS".
    '% Modificacion : Patricia Moreno. 17/05/2007
    Public Function insValCAL014(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrsCertype As String

        On Error GoTo insValCAL014_Err

        lobjErrors = New eFunctions.Errors
        insValCAL014 = String.Empty

        '+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
        '+ Póliza y certificado.

        '+ El ramo debe estar lleno
        If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ El producto debe estar lleno
        If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
            'Call lobjErrors.ErrorMessage(sCodispl, 3003)
        End If

        '+ si el producto tienen valor el ramo debe estar lleno
        If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ si la poliza tiene valor el ramo debe estar lleno
        If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ si la poliza tiene valor el producto debe estar lleno
        If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+ si la poliza tiene valor debe existir en el sistema
        If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            If Not ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
                Call lobjErrors.ErrorMessage(sCodispl, 3001)
            End If
        End If

        insValCAL014 = lobjErrors.Confirm

insValCAL014_Err:
        If Err.Number Then
            insValCAL014 = "insValCAL014: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '**% insValCAL010: This function makes the validations of the CAL011 - "Printing policy documents" transaction.
    '%insValCAL010: Esta función realiza las validaciones de la transacciòn CAL011 - "Estadística Asegurado Vida".
    '% Modificacion : Patricia Moreno. 17/05/2007
    Public Function insValCAL011(ByVal sCodispl As String, ByVal dInitial As Date, ByVal dFinish As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCAL011_Err

        lclsErrors = New eFunctions.Errors

        insValCAL011 = String.Empty

        '+valida el ingreso de fecha
        If dInitial = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) And dFinish = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1094)
        Else
            '+Valida que fecha de inicio no sea nula
            If dInitial = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 2794)
            End If

            '+Valida que fecha termino no sea nula
            If dFinish = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1097)
            End If

            If dFinish < dInitial And dFinish <> System.DateTime.FromOADate(eRemoteDB.Constants.intNull) And dInitial <> System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 736026)
            End If
        End If

        insValCAL011 = lclsErrors.Confirm

insValCAL011_Err:
        If Err.Number Then
            insValCAL011 = "insValCAL011: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '%InsValCA642_k: Realiza la validación de los campos a actualizar en la ventana CA642_k (Header)
    Public Function insValCA642_k(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        On Error GoTo insValCA642_k_Err
        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        '+Validación del ramo
        If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+Validación del producto
        If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+Validación de la póliza

        If nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        ElseIf (nPolicy <> CDbl("0") Or nPolicy <> eRemoteDB.Constants.intNull) And (nBranch <> CDbl("0") Or nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> CDbl("0") Or nProduct <> eRemoteDB.Constants.intNull) Then
            With lclsPolicy
                '+ Si la póliza no existe
                If Not .Find("2", nBranch, nProduct, nPolicy) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                Else
                    '+ Si está anulada
                    If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull And CDbl(.sStatus_pol) = 6 And .dNulldate <> eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3098)
                    End If
                    '+ Si no es válida
                    If .sStatus_pol <> "1" And .sStatus_pol <> "4" And .sStatus_pol <> "5" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3882)
                    End If

                    '+ Si la frecuencia de pago es cuotas
                    If .nPayfreq = 8 Or .nPayfreq = 6 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 5532)
                    End If
                    '+ Si tiene propuestas de modificación con estado pendiente
                    If .Find("6", nBranch, nProduct, nPolicy) Then
                        If .sStatus_pol = "4" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55531)
                        End If
                    End If
                    '+ Si tiene propuestas de renovación con estado pendiente
                    If .Find("7", nBranch, nProduct, nPolicy) Then
                        If .sStatus_pol = "4" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55531)
                        End If
                    End If
                End If
            End With
        End If
        insValCA642_k = lobjErrors.Confirm

insValCA642_k_Err:
        If Err.Number Then
            insValCA642_k = insValCA642_k & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        On Error GoTo 0
    End Function

    '**% insValCAL010: This function makes the validations of the CAL011 - "Printing policy documents" transaction.
    '%insValCAL010: Esta función realiza las validaciones de la transacciòn CAL011 - "Estadística Asegurado Vida".
    '% Modificacion : Patricia Moreno. 17/05/2007
    Public Function insValCAL970(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dFinish As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        On Error GoTo insValCAL970_Err
        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        '+Validación del ramo
        If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+Validación del producto
        If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If


        '+ Se valida la fecha no sea nula
        If dFinish = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1094)
        End If

        '+Validación de la póliza

        If nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        End If
        insValCAL970 = lobjErrors.Confirm

insValCAL970_Err:
        If Err.Number Then
            insValCAL970 = insValCAL970 & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        On Error GoTo 0

    End Function



    '%insPreCA642: Recupera los datos de la ventana CA642
    Public Function insPreCA642(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCollection As eCollection.Premium
        Dim lintDay As Integer
        Dim lintNewDay As Integer
        Dim lintMonth As Integer
        Dim lintyear As Integer
        Dim ldtmNewMonth As Date

        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCollection = New eCollection.Premium

        On Error GoTo insPreCA642_Err

        With lclsPolicy
            If .Find("2", CInt(nBranch), CInt(nProduct), nPolicy) Then
                mstrClient = .SCLIENT
                mdtmStartdate = .dStartdate
                mdtmExpirdat = .DEXPIRDAT
                mdtmChangdat = .dChangdat
                mdtmNextreceip = .dNextReceip
                blnStatusprepp = lclsCollection.InsValPendendReceipt(nBranch, nProduct, nPolicy, .dEffecdate, 1)
                blnStatusprepc = lclsCollection.InsValPendendReceipt(nBranch, nProduct, nPolicy, .dEffecdate, 4)
                intPayfreq = .nPayfreq
            End If
        End With

        With lclsCollection
            If .FindLastPayDate("2", CInt(nBranch), CInt(nProduct), nPolicy) Then
                If .dExpirDat = eRemoteDB.Constants.dtmNull Then
                    If .FindFirtPendDate("2", CInt(nBranch), CInt(nProduct), nPolicy) Then
                        mdtmDateRecPay = .dExpirDat
                    End If
                Else
                    mdtmDateRecPay = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, .dExpirDat)
                End If
                If mdtmDateRecPay <> eRemoteDB.Constants.dtmNull Then
                    lintDay = VB.Day(mdtmDateRecPay)
                    lintMonth = Month(mdtmDateRecPay)
                    lintyear = Year(mdtmDateRecPay)
                    lintNewDay = VB.Day(mdtmStartdate)
                    If lintMonth = 1 Or lintMonth = 3 Or lintMonth = 5 Or lintMonth = 7 Or lintMonth = 8 Or lintMonth = 10 Or lintMonth = 12 Then
                        mdtmDayForce = DateSerial(lintyear, lintMonth, lintNewDay)
                    End If
                    If lintMonth = 4 Or lintMonth = 6 Or lintMonth = 9 Or lintMonth = 11 Then
                        If lintNewDay <= 30 Then
                            mdtmDayForce = DateSerial(lintyear, lintMonth, lintNewDay)
                        Else
                            mdtmDayForce = DateSerial(lintyear, lintMonth, lintDay)
                        End If
                    End If
                    If lintMonth = 2 Then
                        If (lintyear / 4) > 0 Then
                            If lintNewDay <= 28 Then
                                mdtmDayForce = DateSerial(lintyear, lintMonth, lintNewDay)
                            Else
                                mdtmDayForce = DateSerial(lintyear, lintMonth, lintDay)
                            End If
                        Else
                            If lintNewDay <= 29 Then
                                mdtmDayForce = DateSerial(lintyear, lintMonth, lintNewDay)
                            Else
                                mdtmDayForce = DateSerial(lintyear, lintMonth, lintDay)
                            End If
                        End If
                    End If
                Else
                    mdtmDateRecPay = mdtmStartdate
                End If
            Else
                mdtmDateRecPay = mdtmStartdate
            End If
        End With

        insPreCA642 = lobjErrors.Confirm

insPreCA642_Err:
        If Err.Number Then
            insPreCA642 = insPreCA642 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCollection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCollection = Nothing
    End Function


    '% DefaultValueCA642: Retorna valores por defecto de transaccion CA642
    '%                    cargados en insPreCA642
    Public Function DefaultValueCA642(ByVal strKey As String) As Object
        Dim caseAux As Object = New Object

        Select Case strKey
            Case "tctsClient"
                caseAux = mstrClient
            Case "tcdStartdate"
                caseAux = mdtmStartdate
            Case "tcdExpirdat"
                caseAux = mdtmExpirdat
            Case "tcdChangdat"
                caseAux = mdtmChangdat
            Case "tcdNextreceip"
                caseAux = mdtmNextreceip
            Case "chkStatusprepp"
                If blnStatusprepp Then
                    caseAux = "1"
                Else
                    caseAux = "2"
                End If
            Case "chkStatusprepc"
                If blnStatusprepc Then
                    caseAux = "1"
                Else
                    caseAux = "2"
                End If
            Case "valNpayfreq"
                caseAux = intPayfreq
            Case "tcdNewChangdat"
                caseAux = mdtmDateRecPay
            Case "tcdDateToForce"
                caseAux = mdtmDayForce
            Case "DateNextreceip"
                caseAux = mdtmNewNextreceip
        End Select
        Return caseAux
    End Function

    '%InsValCA642: Realiza la validación de los campos a actualizar en la ventana CA642
    Public Function InsValCA642(ByVal sCodispl As String, ByVal dChangdat As Date, ByVal nPayfreq As Integer, ByVal dStartdate As Date, ByVal DEXPIRDAT As Date, ByVal nNewPayfreq As Integer, ByVal dNewChangdat As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lintNewMonth As Integer
        Dim lintNewYear As Integer
        Dim lintNewDay As Integer
        Dim lintDay As Integer
        Dim lintLastDay As Integer
        Dim lblnDateOk As Boolean

        On Error GoTo InsValCA642_Err

        lobjErrors = New eFunctions.Errors

        '+ Si la frecuencia de pago nueva está nula
        If nNewPayfreq = 0 Or nNewPayfreq = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3216)
        End If

        '+ Si la frecuencia de pago antigua y la actual son iguales
        If nNewPayfreq = nPayfreq Then
            Call lobjErrors.ErrorMessage(sCodispl, 55533)
        End If

        '+ Si la fecha de endoso es nula
        If dNewChangdat = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55534)
        Else
            '+ Si la fecha de endoso es menor que la fecha de ultima modificación
            If dNewChangdat < dChangdat Then
                Call lobjErrors.ErrorMessage(sCodispl, 10869)
            End If

            '+ Si dia de endoso es distinto al de inicio de vigencia de la póliza
            lblnDateOk = True
            If VB.Day(dNewChangdat) <> VB.Day(dStartdate) Then
                lintDay = VB.Day(dStartdate)
                lintNewMonth = Month(dNewChangdat)
                lintNewDay = VB.Day(dNewChangdat)
                lintNewYear = Year(dNewChangdat)
                '+Se calcula el último dia del mes de la fecha de endoso
                lintLastDay = VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, DateSerial(lintNewYear, lintNewMonth, 1))))
                If lintDay > lintNewDay Then
                    If lintNewDay = lintLastDay Then
                        lblnDateOk = True
                    Else
                        lblnDateOk = False
                    End If
                Else
                    lblnDateOk = False
                End If
            Else
                lblnDateOk = True
            End If

            If Not (lblnDateOk) Then
                If nPayfreq > nNewPayfreq Then
                    Call lobjErrors.ErrorMessage(sCodispl, 55536)
                End If
            End If

            '+ La fecha de endoso debe estar comprendida dentro de la vigencia de la póliza
            If dNewChangdat < dStartdate Or (DEXPIRDAT <> eRemoteDB.Constants.dtmNull And dNewChangdat > DEXPIRDAT) Then
                Call lobjErrors.ErrorMessage(sCodispl, 55541)
            End If
        End If

        InsValCA642 = lobjErrors.Confirm

InsValCA642_Err:
        If Err.Number Then
            InsValCA642 = InsValCA642 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%ValDate_Nextreceip: Busca la nueva fecha de próxima facturación de la transacción CA642
    Public Function ValDate_Nextreceip(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPayfreq As Integer, ByVal dChandat As Date, ByVal DEXPIRDAT As Date) As String
        Dim lclsCertificat As ePolicy.Certificat
        Dim lintQuantMonth As Object
        Dim bFracReceip As Boolean

        On Error GoTo InsValCA642_Err

        lclsCertificat = New ePolicy.Certificat

        If lclsCertificat.Find("2", nBranch, nProduct, nPolicy, 0) Then
            If lclsCertificat.sFracReceip = "1" Then
                bFracReceip = True
            Else
                bFracReceip = False
            End If
            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing
        End If

        Select Case nPayfreq
            '+ Anual
            Case 1
                mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, dChandat)
                If DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                    If mdtmNewNextreceip > DEXPIRDAT Then
                        mdtmNewNextreceip = DEXPIRDAT
                    End If
                End If
                '+ Semestral
            Case 2
                mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, dChandat)
                If DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                    If mdtmNewNextreceip > DEXPIRDAT Then
                        mdtmNewNextreceip = DEXPIRDAT
                    End If
                End If
                '+ Trimestral
            Case 3
                mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, dChandat)
                If DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                    If mdtmNewNextreceip > DEXPIRDAT Then
                        mdtmNewNextreceip = DEXPIRDAT
                    End If
                End If
                '+ Bimestral
            Case 4
                mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, dChandat)
                If DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                    If mdtmNewNextreceip > DEXPIRDAT Then
                        mdtmNewNextreceip = DEXPIRDAT
                    End If
                End If
                '+ Mensual
            Case 5
                mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dChandat)
                If DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                    If mdtmNewNextreceip > DEXPIRDAT Then
                        mdtmNewNextreceip = DEXPIRDAT
                    End If
                End If
        End Select

        If Not bFracReceip Then
            mdtmNewNextreceip = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, mdtmNewNextreceip)
        End If

InsValCA642_Err:
        If Err.Number Then
            ValDate_Nextreceip = ""
            ValDate_Nextreceip = ValDate_Nextreceip & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        On Error GoTo 0
    End Function

    '% insPostCA642: Realiza la actualización de los recibos para un cambio de frecuencia de pago
    Public Function insPostCA642(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dChangdat As Date, ByVal nUsercode As Integer, ByVal nPayfreq As Integer, ByVal dNextReceip As Date) As Boolean
        Dim lrecinsChange_payfreq As eRemoteDB.Execute
        On Error GoTo insChange_payfreq_Err
        lrecinsChange_payfreq = New eRemoteDB.Execute
        '+
        '+ Definición de store procedure insChange_payfreq al 01-08-2002 18:50:44
        '+
        With lrecinsChange_payfreq
            .StoredProcedure = "insChange_payfreq"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dChangdat", dChangdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextreceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nError", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCA642 = .Run(False)
            If insPostCA642 Then
                nNumError = .Parameters("nError").Value
            End If

        End With

insChange_payfreq_Err:
        If Err.Number Then
            insPostCA642 = False
        End If
        'UPGRADE_NOTE: Object lrecinsChange_payfreq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsChange_payfreq = Nothing
        On Error GoTo 0
    End Function

    '% FindName: Busca el nombre del cliente dado su código
    Public Function FindName(ByVal SCLIENT As String, ByVal blnInter As Boolean) As String
        Dim lrecreaClient As eRemoteDB.Execute
        On Error GoTo FindName_err

        lrecreaClient = New eRemoteDB.Execute

        With lrecreaClient
            If blnInter Then
                .StoredProcedure = "reaClient"
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .StoredProcedure = "reaIntermedia"
                .Parameters.Add("nIntermed", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            If .Run Then
                FindName = .FieldToClass("sCliename")
                .RCloseRec()
            End If
        End With

FindName_err:
        If Err.Number Then
            FindName = String.Empty
        End If
        'UPGRADE_NOTE: Object lrecreaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaClient = Nothing
        On Error GoTo 0
    End Function

    '%Update_dexpirdat: Se actualiza la fecha de expiracion
    Public Function Update_dexpirdat() As Boolean
        Dim lrecUpddexpirdat As eRemoteDB.Execute

        On Error GoTo Update_dexpirdat_Err

        lrecUpddexpirdat = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.Upddexpirdat
        With lrecUpddexpirdat
            .StoredProcedure = "upddExpirdat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dexpirdat", DEXPIRDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_dexpirdat = .Run(False)
        End With

Update_dexpirdat_Err:
        If Err.Number Then
            Update_dexpirdat = False
        End If
        'UPGRADE_NOTE: Object lrecUpddexpirdat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpddexpirdat = Nothing
    End Function

    '% insValCAL006: se realizan las validaciones de los campos de la transacción
    Public Function insValCAL006(ByVal sCodispl As String, ByVal sOptInsur As String, ByVal dEffecdate As Date, ByVal sBrancht As String) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCtrolDate As eGeneral.Ctrol_date
        Dim sDateProc As String

        On Error GoTo insValCAL006_Err

        lobjErrors = New eFunctions.Errors

        '+Validación del Campo dEffecdate
        With lobjErrors
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                Call .ErrorMessage(sCodispl, 1103)
            Else
                lclsCtrolDate = New eGeneral.Ctrol_date
                Call lclsCtrolDate.Find(clngGenCalcReserva)
                If dEffecdate < lclsCtrolDate.dEffecdate Then
                    Call .ErrorMessage(sCodispl, 100142, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                Else
                    If (Month(dEffecdate) = Month(lclsCtrolDate.dEffecdate)) And (Year(dEffecdate) = Year(lclsCtrolDate.dEffecdate)) Then
                        sDateProc = "Mes: " & Month(dEffecdate) & " Año: " & Year(dEffecdate)
                        Call .ErrorMessage(sCodispl, 100143, , , sDateProc)
                    End If
                End If
                'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCtrolDate = Nothing
            End If

            insValCAL006 = .Confirm
        End With

insValCAL006_Err:
        If Err.Number Then
            insValCAL006 = "insValCAL006: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '% insValCAL825: se realizan las validaciones de los campos de la transacción
    Public Function insValCAL825(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicy_his As ePolicy.Policy_his
        On Error GoTo insValCAL825_Err
        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsPolicy_his = New ePolicy.Policy_his

        With lobjErrors
            '+ si el producto tiene informacion debe estar lleno el ramo
            If nBranch = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 11135)
            End If

            '+ El producto debe estar lleno
            If nProduct = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1014)
            End If

            '+ La póliza debe corresponder con un registro válido
            If Not lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                Call .ErrorMessage(sCodispl, 3001)
            End If

            '+ La póliza debe ser de tipo colectiva
            If lclsPolicy.sPolitype <> "2" Then
                Call .ErrorMessage(sCodispl, 38016)
            End If

            '+ La póliza debe aplicar el calculo del leg
            If lclsPolicy.sLeg = "2" Then
                Call .ErrorMessage(sCodispl, 56104)
            End If

            '+ La póliza no debe tener movimientos de endosos posteriores a la emision
            If lclsPolicy_his.Find_Movement(sCertype, nBranch, nProduct, nPolicy, -1) Then
                Call .ErrorMessage(sCodispl, 3267)
            End If

            insValCAL825 = .Confirm
        End With

insValCAL825_Err:
        If Err.Number Then
            insValCAL825 = "insValCAL825: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
    '% insPostCAL006: Calcula la reversa de primas para ramos generales y de vida
    Public Function insPostCAL006(ByVal sOptInsur As String, ByVal sOptDetail As String, ByVal dEffecdate As Date, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nOptAct As Integer = 0, Optional ByVal nType_reserve As Integer = 0) As Boolean
        Dim lrecinscalreserves As eRemoteDB.Execute
        Dim nExcp_Return As Integer

        On Error GoTo insPostCAL006_err

        lrecinscalreserves = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.inscallifereserves'
        '+Información leída el 01/10/01 01:45:47 p.m.

        If sOptInsur = "2" Then
            With lrecinscalreserves
                .StoredProcedure = "inscallifereserves"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nExcp_Return", nExcp_Return, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nOptAct", nOptAct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType_reserve", nType_reserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey_Aux", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                insPostCAL006 = .Run(False)

                If insPostCAL006 Then
                    Me.sKey = .Parameters("sKey_Aux").Value
                End If

            End With
        Else
            With lrecinscalreserves
                .StoredProcedure = "inscalgenreserves"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                insPostCAL006 = .Run(False)
            End With
        End If

insPostCAL006_err:
        If Err.Number Then
            insPostCAL006 = False
        End If
        'UPGRADE_NOTE: Object lrecinscalreserves may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinscalreserves = Nothing
        On Error GoTo 0
    End Function

    '% insPostCAL825: Actualiza los certificados de la poliza con el leg calculado
    '%---------------------------------------------------------------------------
    Public Function insPostCAL825(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nLegAmount As Double, ByVal nUsercode As Integer, ByVal nSessionId As String, ByVal sCompany As String) As Boolean
        Dim lrecinsPostCAL825 As eRemoteDB.Execute
        On Error GoTo lrecinsPostCAL825_err
        lrecinsPostCAL825 = New eRemoteDB.Execute
        '+Definición de parámetros para stored procedure 'insudb.inscal_leg_masivo'

        'falta programar

        With lrecinsPostCAL825
            .StoredProcedure = "inscal_leg_masivo"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegAmount", nLegAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSessionId", nSessionId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompany", sCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCAL825 = .Run(False)
        End With

lrecinsPostCAL825_err:
        If Err.Number Then
            insPostCAL825 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostCAL825 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCAL825 = Nothing
        On Error GoTo 0
    End Function

    '% insPreCAL825: Calcula el leg de la poliza requerida
    '%---------------------------------------------------------------------------
    Public Function insPreCAL825(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        Dim lblnCalcLEG As Boolean
        Dim lstrClientGE As String
        Dim NCURRENCY As Integer
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsRoles As ePolicy.Roles
        Dim mclsCurren_pol As ePolicy.Curren_pol
        On Error GoTo insPreCAL825_err
        lclsPolicy = New ePolicy.Policy
        lclsRoles = New ePolicy.Roles
        mclsCurren_pol = New ePolicy.Curren_pol

        insPreCAL825 = True
        lblnCalcLEG = True
        lstrClientGE = String.Empty

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            Me.nLegAmount_old = lclsPolicy.nLegAmount
            Me.dStartdate = lclsPolicy.dStartdate
            '+ Se obtiene las monedas asociadas a la póliza
            With mclsCurren_pol
                If .Find(nPolicy, nBranch, nProduct, sCertype, 0, lclsPolicy.dStartdate) Then
                    If .IsLocal Then
                        lclsPolicy.NCURRENCY = 1
                    Else
                        Call .Val_Curren_pol(0)
                    End If
                End If
            End With
            If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, 0, Roles.eRoles.eRolEnterpriseGroup, String.Empty, lclsPolicy.dStartdate) Then
                lstrClientGE = lclsRoles.SCLIENT
                If lclsPolicy.InsCalLegAmount(sCertype, nBranch, nProduct, nPolicy, "2", lstrClientGE, lclsPolicy.dStartdate, mclsCurren_pol.nCurrency) Then
                    Me.nLegAmount = lclsPolicy.nLegAmount
                    lblnCalcLEG = False
                End If
            End If
            If lblnCalcLEG Then
                If lclsPolicy.InsCalLegAmount(sCertype, nBranch, nProduct, nPolicy, "2", lstrClientGE, lclsPolicy.dStartdate, mclsCurren_pol.nCurrency) Then
                    Me.nLegAmount = lclsPolicy.nLegAmount
                End If
            End If
        End If

insPreCAL825_err:
        If Err.Number Then
            insPreCAL825 = False
        End If
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object mclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCurren_pol = Nothing
        On Error GoTo 0
    End Function

    '%InsCalLegAmount: Calcula el monto del LEG para la póliza matriz en tratamiento
    Public Function InsCalLegAmount(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sTypenom As String, ByVal sRut_ge As String, ByVal dEffecdate As Date, ByVal NCURRENCY As Integer, Optional ByVal sKey As String = "", Optional ByVal NCAPITAL As Double = eRemoteDB.Constants.intNull) As Boolean
        Dim lrecCalleg As eRemoteDB.Execute

        On Error GoTo InsCalLegAmount_Err

        lrecCalleg = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'inscalleg_amount'
        '+Información leída el 7/2/02
        InsCalLegAmount = False
        With lrecCalleg
            .StoredProcedure = "InsCal_leg"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypenom", sTypenom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRut_ge", sRut_ge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", NCURRENCY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", NCAPITAL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegamount", nLegAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                nLegAmount = .Parameters.Item("nLegAmount").Value
                InsCalLegAmount = True
            End If
        End With

InsCalLegAmount_Err:
        If Err.Number Then
            InsCalLegAmount = False
        End If
        'UPGRADE_NOTE: Object lrecCalleg may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCalleg = Nothing
        On Error GoTo 0
    End Function

    '%Find_PolicyGE: Obtiene las pólizas de un grupo empresarial
    Public Function Find_PolicyGE(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal SCLIENT As String, ByVal dEffecdate As Date, ByVal sTyp_module As String) As Boolean
        Dim lrecreaPolicy As eRemoteDB.Execute

        On Error GoTo Find_PolicyGE_Err

        lrecreaPolicy = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'reapolicy_ge'
        '+Información leída el 8/2/02
        With lrecreaPolicy
            .StoredProcedure = "ReaPolicy_GE"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_PolicyGE = True
                nPolhered = .FieldToClass("nPolicy")
                nLegAmount = .FieldToClass("nLegAmount")
                .RCloseRec()
            End If
        End With

Find_PolicyGE_Err:
        If Err.Number Then
            Find_PolicyGE = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy = Nothing
        On Error GoTo 0
    End Function

    '% Find_Certificat_pol: Retorna los certificados de una poliza
    Public Function Find_Certificat_pol(ByVal lstrCertype As String, ByVal lstrState As String, ByVal lstrClient As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal ddatStartdate As Date, ByVal llngCurrrent As Integer, ByVal lstrCreditnum As String, ByVal lstrAccnum As String, ByVal nRow As Integer) As Boolean
        Dim lrecreaCertificat_pol As eRemoteDB.Execute
        Dim lclsCertificat As Certificat

        lrecreaCertificat_pol = New eRemoteDB.Execute

        On Error GoTo Find_Certificat_pol_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaCertifCount'
        '+ Información leída el 04/11/2000 02:19:00 p.m.
        With lrecreaCertificat_pol
            .StoredProcedure = "reaCertificat_pol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sState", lstrState, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", ddatStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrrent", llngCurrrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCreditnum", lstrCreditnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSaccnum", lstrAccnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ncount = 1

            If .Run Then
                Find_Certificat_pol = True

                Do While Not .EOF And ncount < nRow
                    ncount = ncount + 1
                    .RNext()
                Loop

                Do While Not .EOF And ncount < nRow + 50
                    If ncount = 1 Then
                        Me.sKey = .FieldToClass("sKey")
                    End If
                    ncount = ncount + 1
                    lclsCertificat = New Certificat
                    lclsCertificat.sCertype = lstrCertype
                    lclsCertificat.nBranch = lintBranch
                    lclsCertificat.nProduct = lintProduct
                    lclsCertificat.nPolicy = llngPolicy
                    lclsCertificat.nCertif = .FieldToClass("nCertif")
                    lclsCertificat.sClient = .FieldToClass("sClient")
                    lclsCertificat.dStartdate = .FieldToClass("dStartdate")
                    lclsCertificat.dExpirdat = .FieldToClass("dExpirdat")
                    lclsCertificat.nCapital = .FieldToClass("nCapital")
                    lclsCertificat.nPremium = .FieldToClass("nPremium")
                    lclsCertificat.sCliename = .FieldToClass("sCliename")
                    lclsCertificat.sStatusva = .FieldToClass("sStatusva")
                    lclsCertificat.nNullcode = .FieldToClass("nNullcode")
                    lclsCertificat.nWait_code = .FieldToClass("nWait_code")
                    Call AddCertificat(lclsCertificat)
                    'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsCertificat = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_Certificat_pol = False
            End If

        End With
        'UPGRADE_NOTE: Object lrecreaCertificat_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_pol = Nothing

Find_Certificat_pol_Err:
        If Err.Number Then
            Find_Certificat_pol = False
        End If
    End Function

    '% Add: se agrega un elemento a la colección
    Public Function AddCertificat(ByVal lclsCertificat As Certificat) As Certificat
        With lclsCertificat
            mColCertificat.Add(lclsCertificat, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif)
        End With

        AddCertificat = lclsCertificat
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '%Class_Terminate: Destrucción de objeto
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mColCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mColCertificat = Nothing

    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '% InsValCAL671_K: Valida filtros para reporte de Propuestas/Cotizaciones
    Public Function InsValCAL671_K(ByVal sCodispl As String, ByVal sCertype As String, ByVal nInsurArea As Integer, ByVal dDateFrom As Date, ByVal ddateto As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValCAL671_K_err
        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If nInsurArea = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 60215)
            End If

            If sCertype = String.Empty Then
                .ErrorMessage(sCodispl, 60216)
            End If

            If dDateFrom = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 60217)
            End If
            If ddateto = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 60218)
            End If

            If dDateFrom <> eRemoteDB.Constants.dtmNull And ddateto <> eRemoteDB.Constants.dtmNull Then
                If dDateFrom > ddateto Then
                    .ErrorMessage(sCodispl, 60207)
                End If
            End If
            InsValCAL671_K = lclsErrors.Confirm
        End With

InsValCAL671_K_err:
        If Err.Number Then
            InsValCAL671_K = "InsValCAL671_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '% insPostCAL671_k: Genera reporte de Propuestas/Cotizaciones
    Public Function insPostCAL671_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nProcessType As Integer, ByVal sCertype As String, ByVal nInsurArea As Integer, ByVal dDateFrom As Date, ByVal ddateto As Date, ByVal nUsercode As Integer, Optional ByVal nAgency As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nStatus As Integer = 0, Optional ByVal nWaitCode As Integer = 0, Optional ByVal nIntermed As Integer = 0) As Boolean
        Dim lrecreaDatquotaupd As eRemoteDB.Execute
        Dim lclsCheque As eCashBank.Cheque
        Dim lclsMove_Acc As eCashBank.Move_Acc
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim ldblAmount As Double
        Dim llngRequestNu As Integer


        On Error GoTo insPostCAL671_kErr
        '+ Crea los datos del reporte

        If creDatQuota(nProcessType, sCertype, nInsurArea, dDateFrom, ddateto, nAgency, nBranch, nProduct, nStatus, nWaitCode, nIntermed, nUsercode) Then
        End If

        insPostCAL671_k = True

insPostCAL671_kErr:
        If Err.Number Then
            insPostCAL671_k = False
        End If
        'UPGRADE_NOTE: Object lrecreaDatquotaupd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDatquotaupd = Nothing
    End Function

    '% creDatQuota: Crea datos iniciales de reporte de Propuestas / Cotizaciones
    Private Function creDatQuota(ByVal nProcessType As Integer, ByVal sCertype As String, ByVal nInsurArea As Integer, ByVal dDateFrom As Date, ByVal ddateto As Date, Optional ByVal nAgency As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nStatus As Integer = 0, Optional ByVal nWaitCode As Integer = 0, Optional ByVal nIntermed As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lreccreDatquota As eRemoteDB.Execute
        On Error GoTo creDatquota_Err

        lreccreDatquota = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creDatquota al 03-02-2002 19:29:38
        '+
        With lreccreDatquota
            .StoredProcedure = "QueDatQuotaPkg.creDatquota"
            .Parameters.Add("nProcesstype", nProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsurarea", nInsurArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDatefrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateto", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitcode", nWaitCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            creDatQuota = .Run(False)

            If creDatQuota Then
                Me.sKey = .Parameters("sKey").Value
            End If
        End With

creDatquota_Err:
        If Err.Number Then
            creDatQuota = False
        End If
        'UPGRADE_NOTE: Object lreccreDatquota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreDatquota = Nothing
        On Error GoTo 0
    End Function

    '%InsValVIL701_k: Realiza la validación de los campos a actualizar en la ventana VIL701_k (Header)
    Public Function insValVIL701_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dDateIni As Date, ByVal dEffecdate As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat

        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        On Error GoTo insValVIL701_k_Err

        '+ Validación del ramo
        If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        ' Validación de la Fecha Desde
        If dDateIni = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 60217)
        End If

        ' Validación de la fecha Hasta
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 605218)
        Else
            If dEffecdate < dDateIni Then
                Call lobjErrors.ErrorMessage(sCodispl, 11425)
            End If
        End If

        '+ Validación de la póliza
        If (sCertype <> "0" And sCertype <> CStr(eRemoteDB.Constants.intNull)) And (nPolicy <> CDbl("0") And nPolicy <> eRemoteDB.Constants.intNull) And (nBranch <> CDbl("0") And nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> CDbl("0") And nProduct <> eRemoteDB.Constants.intNull) Then

            With lclsPolicy
                '+ Si la póliza no existe
                If Not .Find(sCertype, nBranch, nProduct, nPolicy) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3917)
                Else
                    '+ Si la fecha de expiración es nula se le suman 99 años
                    If (.DEXPIRDAT = eRemoteDB.Constants.dtmNull) Then
                        .DEXPIRDAT = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 99, Today)
                    End If
                    '+ Si no está vigente o no es válida
                    If (.dStartdate > dEffecdate Or .DEXPIRDAT < dEffecdate) Or (.sStatus_pol <> "1" And .sStatus_pol <> "4" And .sStatus_pol <> "5") Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3882)
                    Else
                        If nOffice > 0 Then
                            If .nOffice <> nOffice Then
                                Call lobjErrors.ErrorMessage(sCodispl, 8071, , , "En la Sucursal indicada")
                            End If
                        End If
                    End If
                End If
            End With
        End If

        '+ Validación del certificado
        If (sCertype <> "0" And sCertype <> CStr(eRemoteDB.Constants.intNull)) And (nPolicy <> CDbl("0") And nPolicy <> eRemoteDB.Constants.intNull) And (nBranch <> CDbl("0") And nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> CDbl("0") And nProduct <> eRemoteDB.Constants.intNull) And (nCertif <> CDbl("0") And nCertif <> eRemoteDB.Constants.intNull) Then

            With lclsCertificat
                '+ Si el certificado no existe
                If Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3010)
                Else
                    '+ Si la fecha de expiración es nula se le suman 99 años
                    If (.dExpirdat = eRemoteDB.Constants.dtmNull) Then
                        .dExpirdat = DateAdd("A", 99, .dExpirdat)
                    End If
                    '+ Si no está vigente o no es válido
                    If (.dStartdate > dEffecdate Or .dExpirdat < dEffecdate) Or (.sStatusva <> "1" And .sStatusva <> "4" And .sStatusva <> "5") Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3099)
                    End If
                End If
            End With
        End If

        insValVIL701_k = lobjErrors.Confirm

insValVIL701_k_Err:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        If Err.Number Then
            insValVIL701_k = insValVIL701_k & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insvalVIL702: se realizan las validaciones de la forma VIL702_K
    Public Function insvalVIL702(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCertificat As Certificat
        Dim lblnError As Boolean

        On Error GoTo insValVIL702_err

        lclsErrors = New eFunctions.Errors
        lclsCertificat = New Certificat

        lblnError = False

        If nBranch = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("VIL702", 1022)
            lblnError = True
        End If

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage("VIL702", 4003)
            lblnError = True
        End If

        If Not lblnError Then
            If nPolicy <> eRemoteDB.Constants.intNull Then
                If Find(sCertype, nBranch, nProduct, nPolicy) Then
                    If sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Or sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or CDbl(sStatus_pol) = 6 Or dEffecdate < DISSUEDAT Or (dEffecdate > DEXPIRDAT And DEXPIRDAT <> eRemoteDB.Constants.dtmNull) Then
                        Call lclsErrors.ErrorMessage("VIL702", 60261)
                    End If

                    If nCertif <> eRemoteDB.Constants.intNull Then
                        With lclsCertificat
                            If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                                If .sStatusva = CStr(TypeStatus_Pol.cstrInvalid) Or .sStatusva = CStr(TypeStatus_Pol.cstrIncomplete) Or CDbl(.sStatusva) = 6 Or dEffecdate < .dIssuedat Or (dEffecdate > .dExpirdat And .dExpirdat <> eRemoteDB.Constants.dtmNull) Then
                                    Call lclsErrors.ErrorMessage("VIL702", 55890)
                                End If
                            Else
                                Call lclsErrors.ErrorMessage("VIL702", 3010)
                            End If
                        End With
                    End If
                Else
                    Call lclsErrors.ErrorMessage("VIL702", 3917)
                End If
            End If
        End If

        insvalVIL702 = lclsErrors.Confirm

insValVIL702_err:
        If Err.Number Then
            insvalVIL702 = "insValVIL702: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '%FindVIC001: Esta función se encarga de buscar todos los valores para la ventana
    '%de Verificación de datos.
    Public Function FindVIC001(ByVal nTypeProce As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean

        Dim lrecinsReaVIC008 As eRemoteDB.Execute

        On Error GoTo FindVIC001_Err

        lrecinsReaVIC008 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure
        '+ Información leída el 16/10/2001 09:10:09 a.m.

        With lrecinsReaVIC008
            .StoredProcedure = "insReaVIC008"
            .Parameters.Add("nTypeProce", nTypeProce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindVIC001 = True
                DISSUEDAT = .FieldToClass("dIssuedat")
                dEffecdateV = .FieldToClass("dEffecdate")
                DEXPIRDAT = .FieldToClass("dExpirdat")
                dExp_dat_pre = .FieldToClass("dExp_dat_pre")
                nQuanti_pen = .FieldToClass("nQuanti_pen")
                nAmount_pen = .FieldToClass("nAmount_pen")
                nLoans = .FieldToClass("nLoans")
                nSalvage = .FieldToClass("nSalvage")
                nAvailMax = .FieldToClass("nAvailMax")
                dExpire = .FieldToClass("dExpire")
                nCap_reduc = .FieldToClass("nCap_reduc")
                nYears = .FieldToClass("nYears")
                nMonths = .FieldToClass("nMonths")
                nCap_initial = .FieldToClass("nCap_Initial")
                nAmount = .FieldToClass("nAmount")
                .RCloseRec()
            Else
                FindVIC001 = False
            End If
        End With

FindVIC001_Err:
        If Err.Number Then
            FindVIC001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsReaVIC008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaVIC008 = Nothing
    End Function

    '%UpdatePolicy_sTypeNom: Se actualiza el tipo de nómina de la tabla policy
    Public Function UpdatePolicy_sTypeNom() As Boolean
        Dim lrecPolicy As eRemoteDB.Execute

        On Error GoTo UpdatePolicy_sTypeNom_Err

        lrecPolicy = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.Upddexpirdat
        With lrecPolicy
            .StoredProcedure = "updPolicy_stypenom"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypenom", sTypenom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdatePolicy_sTypeNom = .Run(False)
        End With

UpdatePolicy_sTypeNom_Err:
        If Err.Number Then
            UpdatePolicy_sTypeNom = False
        End If
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function

    '%DelRecursivePolicy: Elimina una poliza o certificado y todas las tablas que tienen
    '%                    referencia a esta
    Public Function DelRecursivePolicy(ByVal nCodeProce As Integer, ByVal nReference As Integer) As Boolean
        Dim lrecdelPolicy2 As eRemoteDB.Execute
        On Error GoTo delPolicy2_Err

        lrecdelPolicy2 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure delPolicy2 al 01-08-2003 12:54:51
        '+
        With lrecdelPolicy2
            .StoredProcedure = "delpolicypkg.delPolicy2"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_activ", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_proce", nCodeProce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            DelRecursivePolicy = .Run(False)
        End With

delPolicy2_Err:
        If Err.Number Then
            DelRecursivePolicy = False
        End If
        'UPGRADE_NOTE: Object lrecdelPolicy2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelPolicy2 = Nothing
        On Error GoTo 0
    End Function

    '% Find_Proponum: verifica si la propuesta se encuentra registrada para otra póliza
    Public Function Find_Proponum(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        Dim lrecPolicy As eRemoteDB.Execute

        On Error GoTo Find_Proponum_Err

        lrecPolicy = New eRemoteDB.Execute

        With lrecPolicy
            .StoredProcedure = "reaPolicy_Proponum"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find_Proponum = True
                Me.sCertype = .FieldToClass("sCertype")
                Me.sPolitype = .FieldToClass("sPolitype")
            Else
                Me.sCertype = String.Empty
                Me.sPolitype = String.Empty
            End If
        End With

Find_Proponum_Err:
        If Err.Number Then
            Find_Proponum = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function

    '% insValVIL7004: Se realizan las validaciones de la transacción VIL7004 - Cálculo de Saldos diarios.
    Public Function insValVIL7004(ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjProd As eProduct.Product

        On Error GoTo insValVIL7004_Err

        lobjErrors = New eFunctions.Errors
        lobjProd = New eProduct.Product

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7004", 70016)
        End If

        '**+ Validate that the field Product.
        '+ Se valida que el campo Producto.
        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7004", 70137)
            Else
                Call lobjProd.insValProdMaster(nBranch, nProduct)
                If lobjProd.blnError Then
                    If CStr(lobjProd.sBrancht) <> "1" Then
                        Call lobjErrors.ErrorMessage("VIL7004", 70132)
                    End If
                End If

                '**+ Validate that the product corresponds to unit linked
                '+ Se valida que el producto corresponda a unit linked
                If lobjProd.FindProduct_li(nBranch, nProduct, Today) Then
                    If lobjProd.nProdClas <> 4 Then
                        Call lobjErrors.ErrorMessage("VIL7004", 70140)
                    End If
                Else
                    Call lobjErrors.ErrorMessage("VIL7004", 70140)
                End If
            End If
        End If

        '**+ Validate that the field Policy.
        '+ Se valida que el campo Poliza.
        If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
            If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7004", 70138)
            End If
        End If

        '**+ Validate that the field certificate.
        '+ Se valida que el campo Certificado.

        If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
            If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7004", 70139)
            End If
        End If

        insValVIL7004 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValVIL7004_Err:
        If Err.Number Then
            insValVIL7004 = "insValVIL7004: " & Err.Description
        End If

        On Error GoTo 0

    End Function

    '% insPostVIL7004: Esta función realiza el llamado al procedimiento que calcula los saldos diarios.
    Public Function insPostVIL7004(ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer) As Boolean
        Dim lrecInsVIL7004 As eRemoteDB.Execute

        On Error GoTo insPostVIL7004_Err

        lrecInsVIL7004 = New eRemoteDB.Execute

        With lrecInsVIL7004
            .StoredProcedure = "insVIL7004"
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", IIf(nBranch = eRemoteDB.Constants.intNull, 0, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", IIf(nPolicy = eRemoteDB.Constants.intNull, 0, nPolicy), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostVIL7004 = .Run(False)
        End With

insPostVIL7004_Err:
        If Err.Number Then
            insPostVIL7004 = False
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecInsVIL7004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsVIL7004 = Nothing
    End Function

    '% insVIL702: Se procesa la informacion para generar el listado de excluídos por asegurabilidad.
    Public Function insVIL702(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As String
        Dim lrecinsVIL702 As eRemoteDB.Execute
        Dim lstrKey As String

        lrecinsVIL702 = New eRemoteDB.Execute

        With lrecinsVIL702
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "INSEXCLUDINSURED"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insVIL702 = lstrKey
            Else
                insVIL702 = ""
            End If
        End With

        'UPGRADE_NOTE: Object lrecinsVIL702 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVIL702 = Nothing
    End Function

    '% insVIL7012: Esta función permite realizar el llamado al procedimiento que permite imprimir el promedio de rentabilidad mensual por fondo.
    Public Function insVIL7012(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nUsercode As Integer) As String
        '---------------------------------------------------- ----------------------------------------
        Dim lrecinsVIL7012 As eRemoteDB.Execute
        Dim lstrKey As String

        lrecinsVIL7012 = New eRemoteDB.Execute

        With lrecinsVIL7012
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "INSVIL7012"

            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insVIL7012 = lstrKey
            End If
        End With
        Return lrecinsVIL7012
        'UPGRADE_NOTE: Object lrecinsVIL7012 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVIL7012 = Nothing
    End Function

    '%insValVIL7012: Realiza la validación de los campos a actualizar en la ventana insValVIL7003(Header)
    Public Function insValVIL7012(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nYear As Integer, ByVal nMonth As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjProd As eProduct.Product

        On Error GoTo insValVIL7012_Err

        lobjErrors = New eFunctions.Errors
        lobjProd = New eProduct.Product

        '+ Validación del ramo

        If nBranch <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7012", 70108)
        End If

        '+ Validación del producto

        If nProduct <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7012", 70109)
        ElseIf lobjProd.FindProduct_li(nBranch, nProduct, dEffecdate) Then
            If lobjProd.nProdClas <> 4 Then
                Call lobjErrors.ErrorMessage("VIL7012", 70123)
            End If
        End If

        '+ Validación del año

        If nYear <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7012", 70131)
        End If

        '+ Validación del mes

        If nMonth <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7012", 70124)
        End If

        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing

        insValVIL7012 = lobjErrors.Confirm

insValVIL7012_Err:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

        If Err.Number Then
            insValVIL7012 = insValVIL7012 & Err.Description
        End If

        On Error GoTo 0
    End Function

    '%insValVIL7003: Realiza la validación de los campos a actualizar en la ventana insValVIL7003(Header)
    Public Function insValVIL7003(ByVal dFromDate As Date, ByVal dToDate As Date) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValVIL7003_Err

        '+ Validación de las fechas
        If dFromDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7003", 70059)
        End If

        If dToDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7003", 70060)
        End If

        If dFromDate > dToDate And dToDate <> eRemoteDB.Constants.dtmNull And dFromDate <> eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7003", 70061)
        End If

        insValVIL7003 = lobjErrors.Confirm

insValVIL7003_Err:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        If Err.Number Then
            insValVIL7003 = insValVIL7003 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '%insValCAL901: Realiza la validación de los campos a actualizar en la ventana insValCAL901(Header)
    Public Function insValCAL901(ByVal dFromDate As Date, ByVal dToDate As Date) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValCAL901_Err

        '+ Validación de las fechas
        If dFromDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL901", 70059)
        End If

        If dToDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL901", 70060)
        End If

        If dFromDate > dToDate And dToDate <> eRemoteDB.Constants.dtmNull And dFromDate <> eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL901", 70061)
        End If

        insValCAL901 = lobjErrors.Confirm

insValCAL901_Err:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        If Err.Number Then
            insValCAL901 = insValCAL901 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '%insValVIL7010: Realiza la validación de los campos a actualizar en la ventana insValVIL7010
    Public Function insValVIL7010(ByVal optType As Integer, ByVal dDateIni As Date, ByVal dDateFin As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjProd As eProduct.Product
        Dim lobjDate As eGeneral.Ctrol_date

        lobjErrors = New eFunctions.Errors
        lobjProd = New eProduct.Product
        lobjDate = New eGeneral.Ctrol_date

        On Error GoTo insValVIL7010_Err

        '+ Validación de las fechas
        If dDateIni = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7010", 70059)
        End If

        If dDateFin = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7010", 70060)
        End If

        If dDateIni > dDateFin And dDateIni <> eRemoteDB.Constants.dtmNull And dDateFin <> eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("VIL7010", 70061)
        End If

        '+ Valida que la fecha desde sea mayor a la ultima fecha de proceso, en caso de ser proceso preliminar
        If optType = 1 Then
            If lobjDate.Find(80) Then
                If dDateIni <= lobjDate.dEffecdate Then
                    Call lobjErrors.ErrorMessage("VIL7010", 70062)
                End If
            End If
        End If

        '**+ Validate that the product corresponds to unit linked
        '+ Se valida que el producto corresponda a unit linked
        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch <> 0 Or nBranch <> eRemoteDB.Constants.intNull) Then
                If lobjProd.FindProduct_li(nBranch, nProduct, Today) Then
                    If lobjProd.nProdClas <> 4 Then
                        Call lobjErrors.ErrorMessage("VIL7010", 70140)
                    End If
                Else
                    Call lobjErrors.ErrorMessage("VIL7010", 70140)
                End If
            End If
        End If


        insValVIL7010 = lobjErrors.Confirm
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing
        'UPGRADE_NOTE: Object lobjDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjDate = Nothing

insValVIL7010_Err:
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing
        'UPGRADE_NOTE: Object lobjDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjDate = Nothing
        If Err.Number Then
            insValVIL7010 = insValVIL7010 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '**% insVIL7002: This function call to the procedure that calculates the distribution of investment.
    '% insVIL7002: Esta función permite realizar el llamado al procedimiento que calcula la distribución de inversiones.
    Public Function insVIL7002(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sEffecDate As String, ByVal nUsercode As Integer) As String
        Dim lrecinsVIL7002 As eRemoteDB.Execute
        Dim lstrKey As String

        lrecinsVIL7002 = New eRemoteDB.Execute

        With lrecinsVIL7002
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "UPDATE_CHRG_VIL7002"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", CDate(sEffecDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insVIL7002 = lstrKey
            End If
        End With
        Return lrecinsVIL7002
        'UPGRADE_NOTE: Object lrecinsVIL7002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVIL7002 = Nothing
    End Function


    '%insValVIL7008: Realiza la validación de los campos a actualizar en la ventana insValVIL7008(Header)
    Public Function insValVIL7008(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCompany As Integer, ByVal dEffecdatefrom As Date, ByVal dEffecdateto As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjProd As eProduct.Product

        On Error GoTo insValVIL7008_Err

        lobjErrors = New eFunctions.Errors
        lobjProd = New eProduct.Product

        If nCompany = 0 Or nCompany = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("VIL7008", 70141)
        End If


        '**+ Validate that the field Product.
        '+ Se valida que el campo Producto.
        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7008", 70137)
            Else
                Call lobjProd.insValProdMaster(nBranch, nProduct)
                If lobjProd.blnError Then
                    If CStr(lobjProd.sBrancht) <> "1" Then
                        Call lobjErrors.ErrorMessage("VIL7002", 70132)
                    End If
                End If

                '**+ Validate that the product corresponds to unit linked
                '+ Se valida que el producto corresponda a unit linked
                If lobjProd.FindProduct_li(nBranch, nProduct, Today) Then
                    If lobjProd.nProdClas <> 4 Then
                        Call lobjErrors.ErrorMessage("VIL7008", 70140)
                    End If
                Else
                    Call lobjErrors.ErrorMessage("VIL7008", 70140)
                End If
            End If
        End If

        '**+ Validate that the field Policy.
        '+ Se valida que el campo Poliza.
        If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
            If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7008", 70138)
            End If
        End If

        '**+ Validate that the field certificate.
        '+ Se valida que el campo Certificado.

        If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
            If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage("VIL7008", 70139)
            End If
        End If

        If (dEffecdatefrom = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Or dEffecdatefrom = System.DateTime.FromOADate(0)) Then
            Call lobjErrors.ErrorMessage("VIL7008", 60217)
        End If

        If (dEffecdateto = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Or dEffecdateto = System.DateTime.FromOADate(0)) Then
            Call lobjErrors.ErrorMessage("VIL7008", 60218)
        End If

        If dEffecdatefrom > dEffecdateto Then
            Call lobjErrors.ErrorMessage("VIL7008", 60207)
        End If

        insValVIL7008 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValVIL7008_Err:
        If Err.Number Then
            insValVIL7008 = insValVIL7008 & Err.Description
        End If

        On Error GoTo 0
    End Function

    '%insValVIL7002: Realiza la validación de los campos a actualizar en la ventana insValVIL7002(Header)
    Public Function insValVIL7002(ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjProd As eProduct.Product

        Dim lobjValues As Object
        Dim ldtmLastDate As Date

        '-VIL7002 - Proceso Unificado de Inversión, Intereses y Costos.
        Dim lclsTab_Interest As eBranches.Tab_Interest

        On Error GoTo insValVIL7002_Err

        lobjErrors = New eFunctions.Errors
        lobjProd = New eProduct.Product
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        '- VIL7002 - Proceso Unificado de Inversión, Intereses y Costos.
        lclsTab_Interest = New eBranches.Tab_Interest

        '+ Validación de las fechas

        If dEffecdate = eRemoteDB.Constants.dtmNull Or Trim(CStr(dEffecdate)) = String.Empty Then
            Call lobjErrors.ErrorMessage("VIL7002", 70122)
        ElseIf dEffecdate > Today Then
            Call lobjErrors.ErrorMessage("VIL7002", 70134)
        End If

        '+ Validación del ramo

        If nBranch <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7002", 70108)
        End If

        '**+ Validate that the field Product.
        '+ Se valida que el campo Producto.

        If nProduct <= 0 Then
            Call lobjErrors.ErrorMessage("VIL7002", 70109)
        Else
            lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                Call lobjErrors.ErrorMessage("VIL7002", 9066)
            Else

                '**+ Validate that the product corresponds to life or combined
                '+ Se valida que el producto corresponda a vida o combinado

                With lobjProd
                    Call .insValProdMaster(nBranch, nProduct)

                    If .blnError Then
                        If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                            Call lobjErrors.ErrorMessage("VIL7002", 3403)
                        Else

                            '**+ Read the Funds associated to the Line of business_Product to the given date
                            '+ Leer de Funds los fondos asociados al Ramo-Producto a la fecha dada

                            If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                                If lobjProd.FindProduct_li(nBranch, nProduct, dEffecdate) Then
                                    If lobjProd.nProdClas <> 4 Then
                                        Call lobjErrors.ErrorMessage("VIL7002", 70123)
                                    End If
                                Else
                                    Call lobjErrors.ErrorMessage("VIL7002", 70123)
                                End If
                            End If
                        End If
                    End If
                End With
                If InsValFund_Request(nBranch, nProduct) Then
                    Call lobjErrors.ErrorMessage("VIL7002", 750133)
                End If
            End If
        End If

        '+ Debe haber precio de unidad para todos los fondos de la empresa para la fecha indicada
        If Not Get_Price_funds(dEffecdate) Then
            Call lobjErrors.ErrorMessage("VIL7002", 70162)
        End If

        '+ Solamente se validara que exista precio de unidad para todos los fondos si el producto
        '+ en cuestión tiene unidades ó es de ahorros y permite cambios durante la emisión

        If (lobjProd.nSaving_pct <> 100 And lobjProd.nSaving_pct > 0) Or (lobjProd.sS_allwchng = "1") Then

            '+ Deben estar registradas las tasas de interes para el indice de inversión a
            '+ la fecha indicada si y solo si se trata del fin de mes
            If Not lclsTab_Interest.insVal_Cap_Index(nBranch, nProduct, lobjProd.nIndex_table, dEffecdate) Then
                Call lobjErrors.ErrorMessage("VIL7002", 70163)
            End If

        End If

        '+ Validación #70164 - La fecha no puede ser mayor a la fecha del último proceso más 1 día
        '+ ACM - 04/09/2003
        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
            ldtmLastDate = Me.GetLast_date_APV("VIL7002", nBranch, nProduct)

            If dEffecdate > ldtmLastDate Then
                Call lobjErrors.ErrorMessage("VIL7002", 70170)
            End If
        End If

        insValVIL7002 = lobjErrors.Confirm

insValVIL7002_Err:
        If Err.Number Then
            insValVIL7002 = insValVIL7002 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProd = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsTab_Interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_Interest = Nothing
    End Function



    '%insValVIL7000: Realiza la validación de los campos de la ventana VIL7000 - Cartola detallada de movimientos (APV).
    Public Function insValVIL7000(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado

                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dEndDate <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dEndDate) Then
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If
                                    Else
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                Else
                                    Call lobjErrors.ErrorMessage(sCodispl, 3239)
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        Else
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        If Not lblnError Then
            If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
                If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70138)

                    '**+ Validate that it is valid policy.
                    '+ Se valida que sea una póliza válida.

                Else
                    With lclsPolicy
                        If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3001)

                            lblnError = True
                        Else
                            If .sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3720)

                                lblnError = True
                            Else

                                '**+ Verify that the policy is not anulled
                                '+ Verificar que la póliza no esté anulada

                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            Else
                Call lobjErrors.ErrorMessage(sCodispl, 21033)
            End If
        End If

        '**+ Validate the field Certificate.
        '+Se valida el campo Certificado.

        If Not lblnError Then
            If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
                If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70139)
                Else
                    With lclsCertificat
                        If Not .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3010)

                            lblnError = True
                        Else

                            '**+ Validate that the certificate is valid
                            '+ Se válida que el certificado sea válido

                            If .sStatusva = "3" Or .sStatusva = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 750044)

                                lblnError = True
                            Else
                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3099)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            Else
                If Find("2", nBranch, nProduct, nPolicy) Then
                    If sPolitype <> "1" Then
                        If nCertif = eRemoteDB.Constants.intNull Or nCertif = 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3200)
                        End If
                    End If
                End If
            End If
        End If

        '**+ The field date from must be full
        '+ El campo fecha desde debe estar lleno.

        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3237)
        End If

        '**+ The field date to must be full.
        '+ El campo fecha hasta debe estar lleno.

        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3239)
        Else
            If dEndDate < dInitDate Then
                Call lobjErrors.ErrorMessage(sCodispl, 11425)
            End If
        End If

        insValVIL7000 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '% insPostVIL7000: Esta función permite realizar el llamado al procedimiento que permite imprimir
    '% la cartola detallada de los movimientos (APV).
    Public Function insPostVIL7000(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nUsercode As Integer) As String
        '---------------------------------------------------- ----------------------------------------
        Dim lrecInsVIL7000 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo insPostVIL7000_Err

        lrecInsVIL7000 = New eRemoteDB.Execute

        With lrecInsVIL7000
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "InsVIL7000"

            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insPostVIL7000 = lstrKey
            End If
        End With

insPostVIL7000_Err:
        If Err.Number Then
            insPostVIL7000 = String.Empty
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecInsVIL7000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsVIL7000 = Nothing
    End Function

    '%insValVIL7006: Realiza la validación de los campos de la ventana VIL7006 - Reserva del valor del fondo.
    Public Function insValVIL7006(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado

                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dEndDate <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dEndDate) Then
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If
                                    Else
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        If Not lblnError Then
            If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
                If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70138)

                    '**+ Validate that it is valid policy.
                    '+ Se valida que sea una póliza válida.

                Else
                    With lclsPolicy
                        If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3001)

                            lblnError = True
                        Else
                            If .sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3720)

                                lblnError = True
                            Else

                                '**+ Verify that the policy is not anulled
                                '+ Verificar que la póliza no esté anulada

                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ Validate the field Certificate.
        '+Se valida el campo Certificado.

        If Not lblnError Then
            If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
                If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70139)
                Else
                    With lclsCertificat
                        If Not .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3010)

                            lblnError = True
                        Else

                            '**+ Validate that the certificate is valid
                            '+ Se válida que el certificado sea válido

                            If .sStatusva = "3" Or .sStatusva = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 750044)

                                lblnError = True
                            Else
                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3099)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ The field date from must be full
        '+ El campo fecha desde debe estar lleno.

        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3237)
        End If

        '**+ The field date to must be full.
        '+ El campo fecha hasta debe estar lleno.

        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3239)
        Else
            If dEndDate < dInitDate Then
                Call lobjErrors.ErrorMessage(sCodispl, 11425)
            End If
        End If

        insValVIL7006 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '% insPostVIL7006: Esta función permite realizar el llamado al procedimiento que permite imprimir
    '% la Reserva del valor del fondo.
    Public Function insPostVIL7006(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nUsercode As Integer) As String
        '---------------------------------------------------- ----------------------------------------
        Dim lrecInsVIL7006 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo insPostVIL7006_Err

        lrecInsVIL7006 = New eRemoteDB.Execute

        With lrecInsVIL7006
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "InsVIL7006"

            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insPostVIL7006 = lstrKey
            End If
        End With

insPostVIL7006_Err:
        If Err.Number Then
            insPostVIL7006 = String.Empty
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecInsVIL7006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsVIL7006 = Nothing
    End Function

    '%insValVIL7001: Realiza la validación de los campos de la ventana VIL7001 - Cartola tributaria unificada anual resumida (APV).
    Public Function insValVIL7001(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEndDate As Date) As String
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado

                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dEndDate <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dEndDate) Then
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If
                                    Else
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        If Not lblnError Then
            If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
                If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70138)

                    '**+ Validate that it is valid policy.
                    '+ Se valida que sea una póliza válida.

                Else
                    With lclsPolicy
                        If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3001)

                            lblnError = True
                        Else
                            If .sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3720)

                                lblnError = True
                            Else

                                '**+ Verify that the policy is not anulled
                                '+ Verificar que la póliza no esté anulada

                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ Validate the field Certificate.
        '+Se valida el campo Certificado.

        If Not lblnError Then
            If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
                If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70139)
                Else
                    With lclsCertificat
                        If Not .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3010)

                            lblnError = True
                        Else

                            '**+ Validate that the certificate is valid
                            '+ Se válida que el certificado sea válido

                            If .sStatusva = "3" Or .sStatusva = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 750044)

                                lblnError = True
                            Else
                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3099)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ The field date to must be full.
        '+ El campo fecha hasta debe estar lleno.

        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3239)
        End If

        insValVIL7001 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '% insPostVIL7001: Esta función permite realizar el llamado al procedimiento que permite imprimir
    '% la cartola tributaria unificada anual resumida (APV).
    Public Function insPostVIL7001(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEndDate As Date, ByVal nUsercode As Integer) As String
        '---------------------------------------------------- ----------------------------------------
        Dim lrecInsVIL7001 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo insPostVIL7001_Err

        lrecInsVIL7001 = New eRemoteDB.Execute

        With lrecInsVIL7001
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "InsVIL7001"
            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insPostVIL7001 = lstrKey
            End If
        End With

insPostVIL7001_Err:
        If Err.Number Then
            insPostVIL7001 = String.Empty
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecInsVIL7001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsVIL7001 = Nothing
    End Function

    '% insreaProponum: lee los datos de una propuesta/cotización/póliza, basado solo en el número
    '%                 de la misma
    Public Function FindPolicybyPolicy(ByVal sCertype As String, ByVal nPolicy As Double, Optional ByVal nBranch As Double = 0, Optional ByVal nProduct As Double = 0) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo FindPolicybyPolicy_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "reaPolicy_npolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                SCLIENT = .FieldToClass("sClient")
                sAccounti = .FieldToClass("sAccounti")
                sBussityp = .FieldToClass("sBussityp")
                sCoinsuri = .FieldToClass("sCoinsuri")
                sColinvot = .FieldToClass("sColinvot")
                sColReint = .FieldToClass("sColreint")
                sColtimre = .FieldToClass("sColtimre")
                sCommityp = .FieldToClass("sCommityp")
                sDeclari = .FieldToClass("sDeclari")
                sDirdebit = .FieldToClass("sDirdebit")
                sIndextyp = .FieldToClass("sIndextyp")
                sLeadinvo = .FieldToClass("sLeadinvo")
                sLeadnoti = .FieldToClass("sLeadnoti")
                sLeadpoli = .FieldToClass("sLeadpoli")
                sPolitype = .FieldToClass("sPolitype")
                sPropo_cert = .FieldToClass("sPropo_cert")
                sRenewal = .FieldToClass("sRenewal")
                sRevalapl = .FieldToClass("sRevalapl")
                sStatus_pol = .FieldToClass("sStatus_pol")
                sSubstiti = .FieldToClass("sSubstiti")
                sTyp_Clause = .FieldToClass("sTyp_clause")
                sTyp_Discxp = .FieldToClass("sTyp_discxp")
                sDocuTyp = .FieldToClass("sDocutyp")
                sTyp_module = .FieldToClass("sTyp_module")
                sNoNull = .FieldToClass("sNoNull")
                sConColl = .FieldToClass("sConColl")
                sNumForm = .FieldToClass("sNumForm")
                dChangdat = .FieldToClass("dChangdat")
                dDat_no_con = .FieldToClass("dDat_no_con")
                dDate_Origi = .FieldToClass("dDate_origi")
                dStartdate = .FieldToClass("dStartdate")
                DEXPIRDAT = .FieldToClass("dExpirdat")
                DISSUEDAT = .FieldToClass("dIssuedat")
                dMaximum_da = .FieldToClass("dMaximum_da")
                dNulldate = .FieldToClass("dNulldate")
                dPropodat = .FieldToClass("dPropodat")
                dNextReceip = .FieldToClass("dNextReceip")
                nAmoucomm = .FieldToClass("nAmoucomm")
                NCAPITAL = .FieldToClass("nCapital")
                nColcladi = .FieldToClass("nColcladi")
                nCommissi = .FieldToClass("nCommissi", 0)
                nIndexfac = .FieldToClass("nIndexfac")
                nLeadcomi = .FieldToClass("nLeadcomi")
                nLeadexpe = .FieldToClass("nLeadexpe")
                nLeadshare = .FieldToClass("nLeadshare")
                nParticip = .FieldToClass("nParticip")
                NPREMIUM = .FieldToClass("nPremium")
                nShare = .FieldToClass("nShare")
                nPayfreq = .FieldToClass("nPayfreq")
                nIntermed = .FieldToClass("nIntermed")
                nLast_certi = .FieldToClass("nLast_certi")
                nNote_adend = .FieldToClass("nNote_adend")
                nNote_benef = .FieldToClass("nNote_benef")
                nNote_comme = .FieldToClass("nNote_comme")
                nNote_condi = .FieldToClass("nNote_condi")
                nNote_cover = .FieldToClass("nNote_cover")
                nProponum = .FieldToClass("nPropoNum")
                nQ_Certif = .FieldToClass("nQ_certif")
                NTRANSACTIO = .FieldToClass("nTransactio")
                nMov_histor = .FieldToClass("nMov_histor")
                nOficial_p = .FieldToClass("nOficial_p")
                nCopies = .FieldToClass("nCopies")
                nLeadcomp = .FieldToClass("nLeadcomp")
                nNo_convers = .FieldToClass("nNo_convers")
                nNotice = .FieldToClass("nNotice")
                nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                nOffice = .FieldToClass("nOffice")
                nOffice_own = .FieldToClass("nOffice_own")
                nTariff = .FieldToClass("nTariff")
                nUser_amend = .FieldToClass("nUser_amend")
                nQuota = .FieldToClass("nQuota")
                sType_prop = .FieldToClass("sType_prop")
                sProrShort = .FieldToClass("sProrShort")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                nCompany = .FieldToClass("nCompany")
                nOfficeIns = .FieldToClass("nOfficeIns")
                sOriginal = .FieldToClass("sOriginal")
                nCod_Agree = .FieldToClass("nCod_agree")
                sInsubank = .FieldToClass("sInsubank")
                sLeg = .FieldToClass("sLeg")
                nAgency = .FieldToClass("nAgency")
                nOfficeAgen = .FieldToClass("nOfficeAgen")
                sInsubank = .FieldToClass("sinsuBank")
                nLegAmount = .FieldToClass("nLegAmount")
                sTypenom = .FieldToClass("sTypenom")
                sNopayroll = .FieldToClass("sNopayroll")
                sColtpres = .FieldToClass("sColtpres")
                sInd_Comm = .FieldToClass("sInd_Comm")
                nUsercode = .FieldToClass("nUsercode")
                sCurrAcc = .FieldToClass("sCurrAcc")
                sRepPrintCov = .FieldToClass("sRepPrintCov")
                nTypeAccount = .FieldToClass("ntypeAccount")
                .RCloseRec()
                FindPolicybyPolicy = True
            Else
                FindPolicybyPolicy = False
            End If
        End With

FindPolicybyPolicy_Err:
        If Err.Number Then
            FindPolicybyPolicy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '%FindPolicyClient: Rescata datos de la poliza y el cliente para ser mostrados desde la secuencia de cobranza
    Public Function FindPolicyClient(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecPolicy As eRemoteDB.Execute
        On Error GoTo FindPolicyClient_Err

        lrecPolicy = New eRemoteDB.Execute

        With lrecPolicy
            .StoredProcedure = "ReaPolicy_Client"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif < 0, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.sCliename = .FieldToClass("sCliename")
                Me.SCLIENT = .FieldToClass("sClient")
                Me.sDigit = .FieldToClass("sDigit")
                Me.NCURRENCY = .FieldToClass("nCurrency")
                Me.nExchange = .FieldToClass("nExchange")
                Me.nOrigin = .FieldToClass("nOrigin")

                FindPolicyClient = True
            Else
                FindPolicyClient = False
            End If
        End With

FindPolicyClient_Err:
        If Err.Number Then
            FindPolicyClient = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function



    '% Find: se realiza la búsqueda de la propuesta por Asegurado
    Public Function FindClient_Prop(ByVal SCLIENT As String, ByVal nPolicy As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecPropuesta As eRemoteDB.Execute
        On Error GoTo Find_Err

        If Me.sCertype <> sCertype Or Me.SCLIENT <> SCLIENT Or Me.nPolicy <> nPolicy Or lblnFind Then

            lrecPropuesta = New eRemoteDB.Execute
            '+ Definición de parámetros para stored procedure 'insudb.reaclient_prop'

            With lrecPropuesta
                .StoredProcedure = "reaclient_prop"
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    sCertype = sCertype
                    Me.SCLIENT = SCLIENT
                    Me.nPolicy = nPolicy
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")
                    'nCover = .FieldToClass("nCover")

                    FindClient_Prop = True
                End If
            End With
        Else
            FindClient_Prop = True
        End If

Find_Err:
        If Err.Number Then
            FindClient_Prop = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPropuesta may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPropuesta = Nothing
    End Function


    '% reaPolProp_npolicy: lee los datos de una propuesta o poliza (scertype = 1,2,6,7,8), basado solo en el número
    '%                 de la misma.
    Public Function FindPolPropbyPolicy(ByVal nPolicy As Double) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo FindPolPropbyPolicy_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "reaPolProp_npolicy"
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                sCertype = .FieldToClass("sCertype")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")

                .RCloseRec()
                FindPolPropbyPolicy = True
            Else
                FindPolPropbyPolicy = False
            End If
        End With

FindPolPropbyPolicy_Err:
        If Err.Number Then
            FindPolPropbyPolicy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function


    '% Find: se realiza la búsqueda de la propuesta por Asegurado
    Public Function FindContrat_Prop(ByVal SCLIENT As String, ByVal nPolicy As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecPropuesta As eRemoteDB.Execute
        On Error GoTo Find_Err

        If Me.sCertype <> sCertype Or Me.SCLIENT <> SCLIENT Or Me.nPolicy <> nPolicy Or lblnFind Then

            lrecPropuesta = New eRemoteDB.Execute
            '+ Definición de parámetros para stored procedure 'insudb.reaclient_prop'

            With lrecPropuesta
                .StoredProcedure = "reacontrat_prop"
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    sCertype = sCertype
                    Me.SCLIENT = SCLIENT
                    Me.nPolicy = nPolicy
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")

                    FindContrat_Prop = True
                End If
            End With
        Else
            FindContrat_Prop = True
        End If

Find_Err:
        If Err.Number Then
            FindContrat_Prop = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPropuesta may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPropuesta = Nothing
    End Function

    '% GetLast_date_APV: Retorna la última fecha registrada + 1 día para
    '%                   un ramo-producto específico
    Public Function GetLast_date_APV(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As Date

        Dim lrecrealast_date_ctroldate_apv As eRemoteDB.Execute

        On Error GoTo GetLast_date_APV_err

        lrecrealast_date_ctroldate_apv = New eRemoteDB.Execute
        '+ Definición de parámetros para stored procedure 'realast_date_ctroldate_apv'
        '+ Información leída el: 04/09/2003

        With lrecrealast_date_ctroldate_apv
            .StoredProcedure = "realast_date_ctroldate_apv"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLastprocess_date", Nothing, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdproces", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                GetLast_date_APV = .Parameters("dLastprocess_date").Value
                nIdproces = .Parameters("nIdproces").Value
            Else
                GetLast_date_APV = CDate(Nothing)
                nIdproces = eRemoteDB.Constants.intNull
            End If
        End With
        'UPGRADE_NOTE: Object lrecrealast_date_ctroldate_apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecrealast_date_ctroldate_apv = Nothing

GetLast_date_APV_err:

        If Err.Number Then
            GetLast_date_APV = CDate(Nothing)
            'UPGRADE_NOTE: Object lrecrealast_date_ctroldate_apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecrealast_date_ctroldate_apv = Nothing
        End If
        On Error GoTo 0

    End Function


    '% Get_unit_price: Comprueba la existencia del el precio de la unidad
    '%                 para una fecha especifica
    Private Function Get_Price_funds(ByVal dEffecdate As Date) As Boolean
        Dim lrecinsval_price_funds As eRemoteDB.Execute

        On Error GoTo insval_price_funds_Err

        lrecinsval_price_funds = New eRemoteDB.Execute

        '**+ Definition of parameters for stored procedure 'insval_price_funds'
        '**+ The Information was read on  04/09/2003

        '+ Definición de parámetros para stored procedure 'insval_price_funds'
        '+ Información leída el: 04/09/2003

        With lrecinsval_price_funds
            .StoredProcedure = "insval_price_funds"

            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist_value", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Get_Price_funds = IIf(.Parameters.Item("nExist_value").Value = 1, True, False)
            End If
        End With

insval_price_funds_Err:
        If Err.Number Then
            Get_Price_funds = False
        End If

        'UPGRADE_NOTE: Object lrecinsval_price_funds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsval_price_funds = Nothing

        On Error GoTo 0
    End Function




    '% insvalcertif: valida que el certificado exista en el sistema
    Public Function insValPolCertif(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaCertificat As eRemoteDB.Execute

        On Error GoTo insValCertif_Err

        lrecreaCertificat = New eRemoteDB.Execute


        '+Definición de parámetros para stored procedure 'insudb.reaCertificat'
        '+Información leída el 05/02/2001 04:45:40 PM
        With lrecreaCertificat
            .StoredProcedure = "reaCertificat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                insValPolCertif = True
            Else
                insValPolCertif = False
            End If
        End With

insValCertif_Err:
        If Err.Number Then
            insValPolCertif = False
        End If
        'UPGRADE_NOTE: Object lrecreaCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat = Nothing
    End Function
    '% InsVal_Pend_Fact: VERIFICA SI LA POLIZA TIENE MOVIMIENTOS PENDIENTES POR
    '%                   FACTURAR Y SI ESTÁ RECIEN EMITIDA (NO TIENE RECIBOS GENERADOS)  */
    Public Function InsVal_Pend_Fact(ByVal sCertype As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dStartdate As Date) As Short
        Dim lrecInsVal_Pend_Fact As eRemoteDB.Execute

        On Error GoTo InsVal_Pend_Fact_Err

        lrecInsVal_Pend_Fact = New eRemoteDB.Execute

        With lrecInsVal_Pend_Fact
            .StoredProcedure = "InsVal_Pend_Fact"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsVal_Pend_Fact = .Parameters("nExist").Value
            Else
                InsVal_Pend_Fact = 0
            End If
        End With

InsVal_Pend_Fact_Err:
        If Err.Number Then
            InsVal_Pend_Fact = 0
        End If
        'UPGRADE_NOTE: Object lrecInsVal_Pend_Fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsVal_Pend_Fact = Nothing
    End Function

    '% InsVal_Pend_Fact: VERIFICA SI LA POLIZA TIENE MOVIMIENTOS PENDIENTES POR
    '%                   FACTURAR Y SI ESTÁ RECIEN EMITIDA (NO TIENE RECIBOS GENERADOS)  */
    Public Function reaPropfrompolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Integer, ByVal dDateRenew As Date) As Boolean
        Dim lrecreaPropfrompolicy As eRemoteDB.Execute
        Dim lclsCertificat As Certificat

        On Error GoTo reaPropfrompolicy_Err

        lrecreaPropfrompolicy = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaPropfrompolicy al 09-29-2004 09:55:37
        '+
        With lrecreaPropfrompolicy
            .StoredProcedure = "reaPropfrompolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateRenew", dDateRenew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                reaPropfrompolicy = True
            Else
                reaPropfrompolicy = False
            End If
        End With

reaPropfrompolicy_Err:
        If Err.Number Then
            reaPropfrompolicy = False
        End If
        'UPGRADE_NOTE: Object lrecreaPropfrompolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPropfrompolicy = Nothing
        On Error GoTo 0

    End Function

    '% InsValFund_Request: valida que los movimientos de ordenes de compra y venta, generados
    '%                     por última ejecución del proceso de inversiones esten invertidos
    Public Function InsValFund_Request(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecInsValFund_Request As eRemoteDB.Execute

        On Error GoTo InsValFund_Request_Err

        lrecInsValFund_Request = New eRemoteDB.Execute


        '+Definición de parámetros para stored procedure 'insudb.reaCertificat'
        '+Información leída el 05/02/2001 04:45:40 PM
        With lrecInsValFund_Request
            .StoredProcedure = "InsValFund_Request"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters.Item("nExist").Value > 0 Then
                    InsValFund_Request = True
                Else
                    InsValFund_Request = False
                End If
            End If
        End With

InsValFund_Request_Err:
        If Err.Number Then
            InsValFund_Request = False
        End If
        'UPGRADE_NOTE: Object lrecInsValFund_Request may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValFund_Request = Nothing
    End Function

    '%insValVIL1412: Realiza la validación de los campos de la ventana VIL1412 - Reporte de post-cargos pendientes
    Public Function insValVIL1412(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dFecha As Date) As String
        Dim lclsProduct As eProduct.Product
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado

                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dFecha <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dFecha) Then
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If
                                    Else
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        Else
            Call lobjErrors.ErrorMessage(sCodispl, 11009)
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        '+ El campo fecha hasta debe estar lleno si el indicador Invertido es seleccionado

        If dFecha = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 7114)
        End If

        insValVIL1412 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '%insValVIL1411: Realiza la validación de los campos de la ventana VIL1411 - Reporte de ordenes de compra/venta a inversiones.
    Public Function insValVIL1411(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dFecha As Date, ByVal sProcess As String) As String
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado

                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dFecha <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dFecha) Then
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If
                                    Else
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        If Not lblnError Then
            If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
                If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70138)

                    '**+ Validate that it is valid policy.
                    '+ Se valida que sea una póliza válida.

                Else
                    With lclsPolicy
                        If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3001)

                            lblnError = True
                        Else
                            If .sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3720)

                                lblnError = True
                            Else

                                '**+ Verify that the policy is not anulled
                                '+ Verificar que la póliza no esté anulada

                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End If

        '**+ The field date to must be full.
        '+ El campo fecha hasta debe estar lleno si el indicador Invertido es seleccionado

        If sProcess = "1" Then
            If dFecha = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114)
            End If
        End If

        insValVIL1411 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '%insValCA900: Realiza la validación de los campos de la ventana
    Public Function insValCA900(ByVal nBordereaux As Integer, ByVal sClient_orig As String, ByVal sClient_dest As String, ByVal nProcess As Integer, ByVal nCredit As Double) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA900 As eRemoteDB.Execute

        On Error GoTo insValCA900_Err

        lrecinsvalCA900 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalCA900
            .StoredProcedure = "insCA900PKG.insvalCA900"
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_orig", sClient_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_dest", sClient_dest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("CA900", , , , , , lstrErrorAll)
            End If
            insValCA900 = .Confirm
        End With

insValCA900_Err:
        If Err.Number Then
            insValCA900 = "insValCA900: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA900 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA900 = Nothing
    End Function

    '%insPostCA900_k: Se realiza la actualización de los datos en la ventana CA028_k (Header)
    Public Function insPostCA900(ByVal nBordereaux As Integer, ByVal sClient_orig As String, ByVal sClient_dest As String, ByVal nUsercode As Integer) As Boolean
        insPostCA900 = True

        Dim lrecinspostCA900 As eRemoteDB.Execute

        On Error GoTo insPostCA900_Err

        lrecinspostCA900 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinspostCA900
            .StoredProcedure = "insCA900PKG.insPostCA900"
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_orig", sClient_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_dest", sClient_dest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA900 = .Run(False)
        End With


insPostCA900_Err:
        If Err.Number Then
            insPostCA900 = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecinspostCA900 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinspostCA900 = Nothing
    End Function

    '%insPreCA900: Se realiza la actualización de los datos en la ventana CA028_k (Header)
    Public Function insPreCA900(ByVal nBordereaux As Integer, ByVal sClient_orig As String) As Boolean
        Dim lrecInsPreCA900 As eRemoteDB.Execute

        insPreCA900 = False

        On Error GoTo InsPreCA900_Err
        lrecInsPreCA900 = New eRemoteDB.Execute
        '+ Definición de store procedure InsPreVI009 al 02-21-2003 12:39:29
        With lrecInsPreCA900
            .StoredProcedure = "INSCA900PKG.INSPRECA900"
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_orig", sClient_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                insPreCA900 = True
                sStatus = .FieldToClass("sStatus")
                nCredit = .FieldToClass("nCredit")
                sCurrency = .FieldToClass("sCurrency")
                nProcess = .FieldToClass("nProcess")
                .RCloseRec()
            End If
        End With

InsPreCA900_Err:
        If Err.Number Then
            insPreCA900 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPreCA900 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPreCA900 = Nothing
        On Error GoTo 0

    End Function

    Public Function insPostCAL001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsPostCAL001 As eRemoteDB.Execute

        On Error GoTo insPostCAL001_Err

        lrecinsPostCAL001 = New eRemoteDB.Execute


        '+Definición de parámetros para stored procedure 'insudb.reaCertificat'
        '+Información leída el 05/02/2001 04:45:40 PM
        With lrecinsPostCAL001
            .StoredProcedure = "Rearoles_cal001cs"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostCAL001 = True
                sKey = .Parameters("sKey").Value
            Else
                insPostCAL001 = False
            End If
        End With

insPostCAL001_Err:
        If Err.Number Then
            insPostCAL001 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostCAL001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCAL001 = Nothing
    End Function

    Public Function insPostCAL010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Double, ByVal nTypeRep As Integer, ByVal nTypeletter As Integer, ByVal nPolicy_Init As Double, ByVal nPolicy_End As Double, ByVal sNote As String, ByVal sAtention As String) As Boolean
        Dim lrecinsPostCAL010 As eRemoteDB.Execute

        On Error GoTo insPostCAL010_Err

        lrecinsPostCAL010 = New eRemoteDB.Execute

        With lrecinsPostCAL010
            .StoredProcedure = "Reacertificat_cal010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeRep", nTypeRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Letter", nTypeletter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy_Init", nPolicy_Init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy_End", nPolicy_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNote", sNote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAtention", sAtention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostCAL010 = True
                sKey = .Parameters("sKey").Value
            Else
                insPostCAL010 = False
            End If
        End With

insPostCAL010_Err:
        If Err.Number Then
            insPostCAL010 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostCAL010 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCAL010 = Nothing
    End Function

    Public Function insValCAL1415(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dmoddate As Date = #12:00:00 AM#) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat
        Dim lstrsCertype As String

        On Error GoTo insValCAL1415_Err

        lobjErrors = New eFunctions.Errors
        insValCAL1415 = String.Empty

        '+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
        '+ Póliza y certificado.

        '+ la poliza debe estar llena
        If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 55652)
        End If

        '+ si el producto y la poliza tienen valor el ramo debe estar lleno
        If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        '+ si la poliza tienen valor el producto debe estar lleno
        If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+ La propuesta debe existir
        If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
            lclsCertificat = New ePolicy.Certificat
            If Not lclsCertificat.Find(sCertype, CInt(nBranch), CInt(nProduct), nPolicy, 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 55651)
            End If

            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing

            '+ si el certificado tienen valor debe existir en el sistema
            If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
                lclsCertificat = New ePolicy.Certificat
                If Not lclsCertificat.Find(sCertype, CInt(nBranch), CInt(nProduct), nPolicy, nCertif) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 8215)
                End If
                'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCertificat = Nothing
            End If

        End If

        insValCAL1415 = lobjErrors.Confirm

insValCAL1415_Err:
        If Err.Number Then
            insValCAL1415 = "insValCAL1415: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function



    '**% insVIL8020: This function call to the procedure that calculates the distribution of investment.
    '% insVIL8020: Esta función permite realizar el llamado al procedimiento que calcula la distribución de inversiones.
    Public Function insVIL8020(ByVal sEffecDate As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsVIL8020 As eRemoteDB.Execute
        lrecinsVIL8020 = New eRemoteDB.Execute

        With lrecinsVIL8020

            .StoredProcedure = "insVIL8020"
            .Parameters.Add("dEffecDate", CDate(sEffecDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("noption", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insVIL8020 = .Run(False)

        End With

        'UPGRADE_NOTE: Object lrecinsVIL8020 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVIL8020 = Nothing
    End Function

    '% Find: se realiza la búsqueda de los datos de la póliza
    Public Function FindPolicyOptSystem(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        Dim lrecPolicy As eRemoteDB.Execute

        On Error GoTo Find_Err


        lrecPolicy = New eRemoteDB.Execute
        '+ Definición de parámetros para stored procedure 'insudb.reaPolicy_branch'
        '+ Información leída el 29/06/1999 10:52:14 AM
        With lrecPolicy
            .StoredProcedure = "reaPolicy_Branch"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                SCLIENT = .FieldToClass("sClient")
                sAccounti = .FieldToClass("sAccounti")
                sBussityp = .FieldToClass("sBussityp")
                sCoinsuri = .FieldToClass("sCoinsuri")
                sColinvot = .FieldToClass("sColinvot")
                sColReint = .FieldToClass("sColreint")
                sColtimre = .FieldToClass("sColtimre")
                sCommityp = .FieldToClass("sCommityp")
                sDeclari = .FieldToClass("sDeclari")
                sDirdebit = .FieldToClass("sDirdebit")
                sIndextyp = .FieldToClass("sIndextyp")
                sLeadinvo = .FieldToClass("sLeadinvo")
                sLeadnoti = .FieldToClass("sLeadnoti")
                sLeadpoli = .FieldToClass("sLeadpoli")
                sPolitype = .FieldToClass("sPolitype")
                sPropo_cert = .FieldToClass("sPropo_cert")
                sRenewal = .FieldToClass("sRenewal")
                sRevalapl = .FieldToClass("sRevalapl")
                sStatus_pol = .FieldToClass("sStatus_pol")
                sSubstiti = .FieldToClass("sSubstiti")
                sTyp_Clause = .FieldToClass("sTyp_clause")
                sTyp_Discxp = .FieldToClass("sTyp_discxp")
                sDocuTyp = .FieldToClass("sDocutyp")
                sTyp_module = .FieldToClass("sTyp_module")
                sNoNull = .FieldToClass("sNoNull")
                sConColl = .FieldToClass("sConColl")
                sNumForm = .FieldToClass("sNumForm")
                dChangdat = .FieldToClass("dChangdat")
                dDat_no_con = .FieldToClass("dDat_no_con")
                dDate_Origi = .FieldToClass("dDate_origi")
                dStartdate = .FieldToClass("dStartdate")
                DEXPIRDAT = .FieldToClass("dExpirdat")
                DISSUEDAT = .FieldToClass("dIssuedat")
                dMaximum_da = .FieldToClass("dMaximum_da")
                dNulldate = .FieldToClass("dNulldate")
                dPropodat = .FieldToClass("dPropodat")
                dNextReceip = .FieldToClass("dNextReceip")
                nAmoucomm = .FieldToClass("nAmoucomm")
                NCAPITAL = .FieldToClass("nCapital")
                nColcladi = .FieldToClass("nColcladi")
                nCommissi = .FieldToClass("nCommissi", 0)
                nIndexfac = .FieldToClass("nIndexfac")
                nLeadcomi = .FieldToClass("nLeadcomi")
                nLeadexpe = .FieldToClass("nLeadexpe")
                nLeadshare = .FieldToClass("nLeadshare")
                nParticip = .FieldToClass("nParticip")
                NPREMIUM = .FieldToClass("nPremium")
                nShare = .FieldToClass("nShare")
                nPayfreq = .FieldToClass("nPayfreq")
                nIntermed = .FieldToClass("nIntermed")
                nLast_certi = .FieldToClass("nLast_certi")
                nNote_adend = .FieldToClass("nNote_adend")
                nNote_benef = .FieldToClass("nNote_benef")
                nNote_comme = .FieldToClass("nNote_comme")
                nNote_condi = .FieldToClass("nNote_condi")
                nNote_cover = .FieldToClass("nNote_cover")
                nProponum = .FieldToClass("nPropoNum")
                nQ_Certif = .FieldToClass("nQ_certif")
                NTRANSACTIO = .FieldToClass("nTransactio")
                nMov_histor = .FieldToClass("nMov_histor")
                nOficial_p = .FieldToClass("nOficial_p")
                nCopies = .FieldToClass("nCopies")
                nLeadcomp = .FieldToClass("nLeadcomp")
                nNo_convers = .FieldToClass("nNo_convers")
                nNotice = .FieldToClass("nNotice")
                nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                nOffice = .FieldToClass("nOffice")
                nOffice_own = .FieldToClass("nOffice_own")
                nTariff = .FieldToClass("nTariff")
                nUser_amend = .FieldToClass("nUser_amend")
                nQuota = .FieldToClass("nQuota")
                sType_prop = .FieldToClass("sType_prop")
                sProrShort = .FieldToClass("sProrShort")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                nCompany = .FieldToClass("nCompany")
                nOfficeIns = .FieldToClass("nOfficeIns")
                sOriginal = .FieldToClass("sOriginal")
                nCod_Agree = .FieldToClass("nCod_agree")
                sInsubank = .FieldToClass("sInsubank")
                sLeg = .FieldToClass("sLeg")
                nAgency = .FieldToClass("nAgency")
                nOfficeAgen = .FieldToClass("nOfficeAgen")
                sInsubank = .FieldToClass("sinsuBank")
                nLegAmount = .FieldToClass("nLegAmount")
                sTypenom = .FieldToClass("sTypenom")
                sNopayroll = .FieldToClass("sNopayroll")
                sColtpres = .FieldToClass("sColtpres")
                sInd_Comm = .FieldToClass("sInd_Comm")
                nUsercode = .FieldToClass("nUsercode")
                sCurrAcc = .FieldToClass("sCurrAcc")
                nRepInsured = .FieldToClass("nRepInsured")
                nClaim_notice = .FieldToClass("nClaim_notice")
                sMassive = .FieldToClass("sMassive")
                sRepPrintCov = .FieldToClass("sRepPrintCov")
                sReceipt_ind = .FieldToClass("sReceipt_Ind")
                nTerm_grace = .FieldToClass("nTerm_grace")

                If sInd_Comm = String.Empty Then
                    sInd_Comm = "1"
                End If

                FindPolicyOptSystem = True
            End If
        End With

Find_Err:
        If Err.Number Then
            FindPolicyOptSystem = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPolicy = Nothing
    End Function
    Public Function insDisabledInsurRecord(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsDisabledInsurRecord As eRemoteDB.Execute

        On Error GoTo insDisabledInsurRecord_Err

        lrecinsDisabledInsurRecord = New eRemoteDB.Execute

        With lrecinsDisabledInsurRecord
            .StoredProcedure = "InsDisabledInsurRecord"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nResult").Value = 1 Then
                    insDisabledInsurRecord = True
                Else
                    insDisabledInsurRecord = False
                End If
            Else
                insDisabledInsurRecord = False
            End If

        End With

insDisabledInsurRecord_Err:

        If Err.Number Then
            insDisabledInsurRecord = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecinsDisabledInsurRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsDisabledInsurRecord = Nothing
    End Function
    '% InsProcessPDF: Hace el llamado al formato de reporte para generar los archivos pdfs del cuadro póliza
    '--------------------------------------------------------------------------------------------
    Public Function InsProcessPDF(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                  ByVal nPolicyIni As Double, ByVal nPolicyEnd As Double, ByVal sNameReport As String, _
                                  ByVal sPrefix As String, ByVal strLogin As String, ByVal strPassword As String, _
                                  Optional ByVal dIniDate As Date = eRemoteDB.dtmNull, Optional ByRef dEndDate As Date = eRemoteDB.dtmNull) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecInsProcessPDF As eRemoteDB.Execute
        Dim lobjExp As Object
        Dim lobjMergePDF As Object
        Dim lclsGetsettings As New eRemoteDB.VisualTimeConfig

        Dim lblnExportReport
        Dim report1 As String = ""
        Dim report2 As String
        Dim nameRandom As String
        Dim sExportName As String
        Dim sStartDate As String
        Dim sExportPath As String
        Dim sExportPathName As String

        On Error GoTo InsProcessPDF_Err

        lrecInsProcessPDF = New eRemoteDB.Execute

        With lrecInsProcessPDF
            .StoredProcedure = "REAPOLICIESRANGE"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicyIni", nPolicyIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicyEnd", nPolicyEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIniDate", dIniDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                ncount = 0
                lobjMergePDF = eRemoteDB.NetHelper.CreateClassInstance("eCrystalExport.MergePDF")
                lclsGetsettings = New eRemoteDB.VisualTimeConfig
                sExportPath = lclsGetsettings.LoadSetting("ExportDirectoryReport", "\\Reports\\", "Paths")
                sExportName = sPrefix & nBranch & "_" & nProduct & "_" & nPolicyIni & "_" & nPolicyEnd & ".pdf"
                sExportPathName = sExportPath & "\\" & sPrefix & nBranch & "_" & nProduct & "_" & nPolicyIni & "_" & nPolicyEnd & ".pdf"
                If File.Exists(sExportPathName) Then File.Delete(sExportPathName)
                Do While Not .EOF

                    lobjExp = eRemoteDB.NetHelper.CreateClassInstance("eCrystalExport.Export")
                    lobjExp.DBParameters.Add("2")
                    lobjExp.DBParameters.Add(nBranch)
                    lobjExp.DBParameters.Add(nProduct)
                    lobjExp.DBParameters.Add(.FieldToClass("nPolicy"))
                    lobjExp.DBParameters.Add(.FieldToClass("nCertif"))
                    sStartDate = .FieldToClass("dIssueDat")
                    lobjExp.DBParameters.Add(CDate(sStartDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture))
                    If lobjExp.RealExport(sNameReport, sExportPathName, "PDF", strLogin, strPassword) Then
                        report2 = lobjExp.sExportedFilePath
                        If ncount > 0 Then
                            Call lobjMergePDF.Merge2PDFs(report1, report2, sExportPathName)
                            Call File.Delete(report1)
                            Call File.Delete(report2)
                            Call File.Copy(sExportPathName, report1)
                            Call File.Delete(sExportPathName)
                        Else
                            report1 = report2
                        End If
                    End If
                    ncount = ncount + 1
                    .RNext()
                    lobjExp = Nothing
                Loop
                If ncount > 0 Then
                    Call File.Copy(report1, sExportPathName)
                    Call File.Delete(report1)
                    sPDFName = sExportName
                    sPDFFullPath = sExportPathName
                    InsProcessPDF = True
                End If
                lclsGetsettings = Nothing
            End If
        End With

InsProcessPDF_Err:
        If Err.Number Then
            InsProcessPDF = False
        End If
        On Error GoTo 0
        lrecInsProcessPDF = Nothing
    End Function



    '% InsVal_Pend_Fact: VERIFICA SI LA POLIZA TIENE MOVIMIENTOS PENDIENTES POR
    '%                   FACTURAR Y SI ESTÁ RECIEN EMITIDA (NO TIENE RECIBOS GENERADOS)  */
    Public Function REAPROPRENEWPOL(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                    ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Integer, _
                                    ByVal dDateRenew As Date) As Boolean
        Dim lrecreaPropfrompolicy As eRemoteDB.Execute
        Dim lclsCertificat As Certificat

        On Error GoTo reaPropfrompolicy_Err

        lrecreaPropfrompolicy = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaPropfrompolicy al 09-29-2004 09:55:37
        '+
        With lrecreaPropfrompolicy
            .StoredProcedure = "REAPROPRENEWPOL"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateRenew", dDateRenew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                REAPROPRENEWPOL = True
            Else
                REAPROPRENEWPOL = False
            End If
        End With

reaPropfrompolicy_Err:
        If Err.Number Then
            REAPROPRENEWPOL = False
        End If
        'UPGRADE_NOTE: Object lrecreaPropfrompolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPropfrompolicy = Nothing
        On Error GoTo 0

    End Function

    Public Function EquivalentFieldToClass(ByVal sField As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nRole As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nUsercode As Integer = 0) As String
        Dim lrecTime As eRemoteDB.Execute

        Dim lstrOutValue As String = String.Empty

        On Error GoTo EquivalentFieldToClass_Err

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "VTINTEGRATION_UTILITIESPKG.GetEquivalentValueToClass"
            .Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInValue", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInValue", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOutValue", lstrOutValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 150, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            EquivalentFieldToClass = .Parameters("sOutValue").Value.ToString.Trim 
  
        End With

EquivalentFieldToClass_Err:
        If Err.Number Then
            EquivalentFieldToClass = String.Empty  
        End If
        On Error GoTo 0
    End Function

    '%Update_dexpirdat: Se actualiza la fecha de proxima facturación
    '--------------------------------------------------------------------------------
    Public Function Update_dNextReceip(ByVal sCertype As String,
                                       ByVal nBranch As Long,
                                       ByVal nProduct As Long,
                                       ByVal nPolicy As Double,
                                       ByVal nCertif As Double,
                                       ByVal dNextReceip As Date,
                                       ByVal nUsercode As Long) As Boolean
        '--------------------------------------------------------------------------------
        Dim lrecUpddNextReceip As eRemoteDB.Execute

        On Error GoTo Update_dNextReceip_err

        lrecUpddNextReceip = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.Upddexpirdat
        With lrecUpddNextReceip
            .StoredProcedure = "upddNextReceip"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_dNextReceip = .Run(False)
        End With

Update_dNextReceip_err:
        lrecUpddNextReceip = Nothing
    End Function



End Class






