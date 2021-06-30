Option Strict Off
Option Explicit On
Public Class Certificat
	'%-------------------------------------------------------%'
	'% $Workfile:: Certificat.cls                           $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 14-08-09 11:20                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'-Se definen las constantes para el manejo del tipo de registro (Póliza)
	Public Enum Stat_quot
		esqPending = 1 '+ Pendiente
		esqApprove = 2 '+ Aprobado
		esqRejected = 3 '+ Rechazado
		esqAnnul = 4 '+ Anulado
		esqModernize = 5 '+ Actualizar
		esqRegulate = 6 '+ Regularizar
	End Enum
	
	'- Estructura de tabla insudb.certificat al 06-10-2002 12:01:51
	'- Property                     Type         DBType   Size Scale  Prec  Null
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public sClient As String ' CHAR       14   0     0    S
	Public nCapital As Double ' NUMBER     22   0     12   S
	Public sCumul_code As String ' CHAR       14   0     0    S
	Public dDat_no_con As Date ' DATE       7    0     0    S
	Public dDate_Origi As Date ' DATE       7    0     0    S
	Public dChangdat As Date ' DATE       7    0     0    S
	Public dExpirdat As Date ' DATE       7    0     0    S
	Public nGroup As Integer ' NUMBER     22   0     5    S
	Public dIssuedat As Date ' DATE       7    0     0    S
	Public dMaximum_da As Date ' DATE       7    0     0    S
	Public nNo_convers As Integer ' NUMBER     22   0     5    N
	Public nNote_benef As Integer ' NUMBER     22   0     10   S
	Public nNote_drisk As Integer ' NUMBER     22   0     10   S
	Public nNullcode As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nPayfreq As Integer ' NUMBER     22   0     5    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public dPropodat As Date ' DATE       7    0     0    S
	Public sRenewal As String ' CHAR       1    0     0    S
	Public nSituation As Integer ' NUMBER     22   0     5    S
	Public dStartdate As Date ' DATE       7    0     0    S
	Public sStatusva As String ' CHAR       1    0     0    S
	Public nUser_amend As Integer ' NUMBER     22   0     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nSus_branch As Integer ' NUMBER     22   0     5    S
	Public nWait_code As Integer ' NUMBER     22   0     5    N
	Public nSus_product As Integer ' NUMBER     22   0     5    S
	Public nSus_policy As Integer ' NUMBER     22   0     10   S
	Public nSus_certif As Integer ' NUMBER     22   0     10   S
	Public dNextReceip As Date ' DATE       7    0     0    S
	Public nProponum As Double ' NUMBER     22   0     10   S
	Public nQuota As Integer ' NUMBER     22   0     5    S
	Public nDaysFQ As Integer ' NUMBER     22   0     5    S
	Public sProrShort As String ' CHAR       1    0     0    S
	Public nDaysSQ As Integer ' NUMBER     22   0     5    S
	Public sNumForm As String ' CHAR       12   0     0    S
	Public sReinsura As String ' CHAR       1    0     0    S
	Public sClaimind As String ' CHAR       1    0     0    S
	Public sException As String ' CHAR       1    0     0    S
	Public nExcCause As Integer ' NUMBER     22   0     5    S
	Public sProperUse As String ' CHAR       1    0     0    S
	Public sExemption As String ' CHAR       1    0     0    S
	Public nImageNum As Integer ' NUMBER     22   0     10   S
	Public sDirind As String ' CHAR       1    0     0    S
	Public nWay_pay As Integer ' NUMBER     22   0     5    S
	Public nBill_day As Integer ' NUMBER     22   0     5    S
	Public nSendAddr As Integer ' NUMBER     22   0     5    S
	Public sAut_guarval As String ' CHAR       1    0     0    S
	Public nSellChannel As Integer ' NUMBER     22   0     5    S
	Public sAnulletter As String ' CHAR       1    0     0    S
	Public dFer As Date ' DATE       7    0     0    S
	Public nDays_quot As Integer ' NUMBER     22   0     5    S
	Public nPol_quot As Double ' NUMBER     22   0     10   S
	Public dDate_accept As Date ' DATE       7    0     0    S
	Public nStatquota As Integer ' NUMBER     22   0     5    S
	Public dCollsus_ini As Date ' DATE       7    0     0    S
	Public dCollsus_end As Date ' DATE       7    0     0    S
	Public sRefundprem As String ' CHAR       1    0     0    S
	Public nSus_reason As Integer ' NUMBER     22   0     5    S
	Public sSus_origi As String ' CHAR       1    0     0    S
	Public sPendinfo As String ' CHAR       1    0     0    S
	Public sBill_Ind As String ' CHAR       1    0     0    S
	Public sProrata As String ' CHAR       1    0     0    S
	Public nPay_day As Integer ' NUMBER     22   0     3    S
	Public nAgency As Integer ' NUMBER     22   0     5    S
	Public nDuration As Integer
	Public nCollector As Integer
	Public sFracReceip As String
	Public nGroup_agree As Integer
    Public sReceipt_ind As String ' CHAR       1    0     0    S
    Public nTerm_grace As Integer
    Public dTariffdate As Date
    Public nTypeExc As Integer
    Public nDepreciationTable As Integer
    Public sIntermediaName As String
    Public nOffice_Associated As Integer
    Public nOfficeAgen_Associated As Integer
    Public nAgency_Associated As Integer
    Public nBranch_Associated As Integer
    Public nProduct_Associated As Integer
    Public nPolicy_Associated As Integer
    Public nCertif_Associated As Integer
    Public sInd_Multiannual As String
    Public sInd_IFI As String
    Public sIndqsamevalue As String
    Public nSpecialbusiness As Integer
    Public nExtraDay As Integer
    Public nFormPay As Integer
    Public sCodbranch_Transfer As String
    Public nCertif_transfer As Integer
    Public nPromissory_Note As Integer
    Public nRecrelatedcoll As Double
    Public nProceedingNum As Double
    Public nContrat As Double
    Public nDraft As Integer

    ' - Indica los números de movimientos pendientes a facturar generados
    Public sOut_moveme As String

    ' - Variable para determinar si la transacción CA028 se esta ejecutando desde la sequencia o desde el menú
    Public sOnSeq As String
    ' - Número de movimiento en la historia de la póliza

    Public nMovement As Double
    ' - Indica si se va a generar un recibo de devolución igual al de cobro

    Public sDevReceipt As String

    '- Arreglo para la carga de cláusulas por grupo
    Public marrCert_Address() As udtCert_Address

    '-Código de error puntual encontrado en carga inicial de forma
    Private mlngErrorClause As Integer

    '-Indicador que se encontró información en Claus_co_g
    Private mblnDataFound As Boolean

    '- Indica si el arreglo de certificados_direcciones se cargo o no
    Private mblnCharge As Boolean

    '-Tipo de datos con información de array de certificat
    Public Structure udtCert_Address
        Dim sRectype As String
        Dim sDescadd As String
        Dim sRectypeAux As String

    End Structure

    '- Número de la propuesta
    Public nCode As Double

    '- Número de certificados de la póliza
    Public nCertifNum As Integer

    '- Propiedad utilizada para el proceso "updDatpart"
    Public dEffecdate As Date

    '- Propiedades para el método VerifyDeclaFreq
    Public nFlag As Integer
    Public dDeclaDatN As Date
    Public nCertifExist As Integer

    '- Descripción de la tabla de Datos Particulares
    Public sTabname As String

    '- Código de la Provincia
    Public nProvince As Integer

    '- Definicion de Propiedades para los datos particulares de VIDA
    Public nCapital_ca As Double ' decimal     no        9     12    0     yes      (n/a)               (n/a)
    Public nPremium_ca As Double ' decimal     no        9     10    2     yes      (n/a)               (n/a)

    '- Definición Digito verificador de la póliza, Propuesta regularizada y
    '- Número de renovaciones
    Public nRenewalnum As Integer ' NUMBER     22   0     5    S
    Public nDigit As Integer ' NUMBER     22   0     1    S
    Public nProp_reg As Integer ' NUMBER     22   0     10   S

    Public nAnulReceipt As Integer
    Public nNullOutMov As Integer
    Public nAnulPropQuot As Integer
    Public sPolitype As String

    '- Definición de propiedades para emisión de recibo manual
    Public sEffecDate As String
    Public sBrancht As String
    Public nCurrency As Integer
    Public nTransac As Integer
    Public sKey As String
    Public sExpirRec As String
    Public nReceipt As Double
    Public nType As Integer
    Public sOrigReceipt As String
    Public nProctype As Integer
    Public dIssuedate As Date
    Public nTratypei As Integer
    Public nModulec As Integer
    Public nGroup_insu As Integer
    Public sNomin_quote As String
    '- Indica si la ejecucuón es de forma preliminar = 1 o definitiva = 2
    Public sTypExecute As String

    '- Definición de propiedades para el cambio de fecha de renovación(Update_RenDate)
    Public sColtimre As String

    '- Propiedades utilizadas en el la busqueda de información de una
    '- póliza/certificado no suspendida(o) (Find_CA038)
    Public nDeclaredClaims As Integer
    Public nSuspCount As Integer
    Public nTransactio As Integer
    Public sColinvot As String
    Public sDirdebit As String
    Public nOffice As Integer
    Public nIntermed As Double
    Public nCoverageCertificate As Double
    Public nStatusCoverageCertificate As Integer
    Public sStatusCoverageCertificate As String
    Public nParticip As Double

    '- Código de cleinte y Nombre del Intermediario
    Public sAgent_cli As String
    Public sAgent_name As String
    Public sAgent_Phones As String

    '- Propiedades para la emisión de recibo manual a través de la CA048
    Public sstatus_pol As String

    Public dRescuedate As Date

    '- Propiedades para la emisión de recibo manual a través de la CA048
    Public sMessage As String

    '- Propiedades para el estado del combo
    Public bWait_code As Boolean

    '- Propiedades para el estado del check "Reversar"
    Public bPendenstat As Boolean
    Public nPendenStat As Integer

    '- Propiedades para el estado del check "Impresión inmediata"
    Public bPrinterStat As Boolean
    Public nPrinterStat As Integer

    '- Propiedades para establecer el estado del recibo
    Public bNotReceipt As Boolean
    Public bAutReceipt As Boolean
    Public bManualReceipt As Boolean
    '- numero de propuesta especial
    Public specProponum As Integer

    '- Variables a utilizar en la CA004.  El valor es asignado en el "pre" de la CA004
    Public sNopayroll As String
    Public nCod_Agree As Integer
    Public sLeg As String
    Public sInsubank As String
    Public sReinst As String
    Public nCopies As Integer
    Public nNotice As Integer
    Public nIndexfac As Integer
    Public sNoNull As String
    Public sDeclari As String
    Public sRevalapl As String
    Public sIndextyp As String

    '- Nombre del cliente.
    Public sCliename As String
    Public sFirstname As String
    Public sLastname As String
    Public sLastname2 As String
    Public dBirthDat As Date
    Public nAge As Short
    Public sSexclie As String
    Public nSpeciality As Integer
    Public sSmoking As String
    Public nTyperisk As Integer
    Public nCivilsta As Integer
    Public nOption As Integer

    Public nHolder As Integer

    Public mdtmnMovnumbe As Integer

    Public nTransaction As Integer

    Public mdtmNextreceip As Date

    Public nAFP_Commiss As Double
    Public nAFP_Comm_Curr As Integer
    Public nOrigin As Integer
    Public nAFP As Integer
    Public sPayer As String
    Public nRepInsured As Integer
    Public sRetarif As String
    Public dLast_tarif As Date
    Public nFolio As Double
    Public sDesBranch As String
    Public sDesProduct As String
    Public sDigit As String

    Public sOrigin As String
    Public dStartCurrentPeriod As Date
    Public dEndCurrentPeriod As Date
    Public dStartNextPeriod As Date
    Public nPolicy_transfer As Double

    Public sRectype As String
    Public sAddress As String
    Public sRectypeaux As String

    '- Tipo enumerado con los objetos que pertenencen a la forma CA004
    Public Enum eTypeControlsCA004
        tcdIssuedat = 0
        tcdReqDate = 1
        tcdStartDate = 2
        tcdExpirDate = 3
        optFreq1 = 4
        optFreq2 = 5
        optFreq3 = 6
        chkFracti = 7
        cbePayFreq = 8
        cbeQuota = 9
        chkDeclarative = 10
        chkRenewalAut = 11
        tcnCopies = 12
        tcnDaysNull = 13
        chkNoNull = 14
        chkExemption = 15
        tcnIndexRate = 16
        cbeIndexType = 17
        cbeIndexApl = 18
        tctClient = 19
        optDirTyp = 20
        cbeWayPay = 21
        tcnBillDay = 22
        valAgreement = 23
        blnUpdVI001 = 24
        cbeSendAddr = 25
        tcsTyp_dom = 26
        chksLeg = 27
        tcnRehabperiod = 28
        chksReinst = 29
        chksFirst_pay = 30
        chksDatecoll = 31
        tcnDays_quot = 32
        chksInsubank = 33
        chksNopayroll = 34
        tcnDuration = 35
        chkBill_Ind = 36
        valOrigin = 37
        tcnAFPCommi = 38
        cbeCurrency = 39
        valCollector = 40
        valgroup_Agree = 41
        cbeRepInsured = 42
        tcnTerm_grace = 43
        tctCumul_code = 44
        cbeReceipt_ind = 45
    End Enum

    '- Forma de Pago de rescate
    Public Enum eSurrPayWay
        eSurrPayOrder = 1 '+ Orden de pago
        eSurrPayBankAccLoad = 2 '+ Cargo a cuenta cte bancaria
        eSurrPayPolicyAccLoad = 3 '+ Cargo a cuenta cte de poliza
        eSurrPayClientAccLoad = 4 '+ Cargo a cuenta cte de cliente
    End Enum

    Public mclsValPolicyTra As ValPolicyTra
    '+Variables privadas

    '- Variable para establecer el estado de la póliza
    Private mstrStatus_pol As String

    '- Variable para establecer el estado del certificado
    Private mstrStatusva As String

    '- Propiedad para validar pago de primera prima (Cotizaciónes y Solicitudes)
    Private lstrMessage As String

    '- Variables que definen el estado de los objetos de la transaciión CA004
    Private bEnabledtcdIssuedat As Boolean
    Private bEnabledtcdReqDate As Boolean
    Private bEnabledtcnDuration As Boolean
    Private bEnabledtcdExpirDate As Boolean
    Private bEnabledoptFreq1 As Boolean
    Private bEnabledoptFreq2 As Boolean
    Private bEnabledoptFreq3 As Boolean
    Private bEnabledcbePayFreq As Boolean
    Private bEnabledcbeQuota As Boolean
    Private bDisabledchksLeg As Boolean
    Private bEnabledchkDeclarative As Boolean
    Private bEnabledchkRenewalAut As Boolean
    Private bEnabledtcnCopies As Boolean
    Private bEnabledtcnDaysNull As Boolean
    Private bEnabledchkNoNull As Boolean
    Private bEnabledchksInsubank As Boolean
    Private bEnabledtcnIndexRate As Boolean
    Private bEnabledcbeIndexType As Boolean
    Private bEnabledcbeIndexApl As Boolean
    Private bEnabledoptDirTyp As Boolean
    Private bEnabledcbeWayPay As Boolean
    Private bEnabledvalAgreement As Boolean
    Private bEnabledvalOrigin As Boolean
    Private bEnabledtcnAFPCommi As Boolean
    Private bEnabledcbeCurrency As Boolean
    Private bEnabledtctClient As Boolean
    Private bEnabledchksNopayroll As Boolean
    Private bEnabledvalgroup_Agree As Boolean
    Private bEnabledcbeRepInsured As Boolean
    Private bEnabledtcnTermgrace As Boolean
    Private bEnabledtctCumulcode As Boolean
    Private bEnabledchkBillInd As Boolean
    Private bEnabledcbeReceiptind As Boolean
    Private bEnabledtcnBillDay As Boolean
    Private bEnabledtcnDaysquot As Boolean



    '- Enumerado de frecuencia.
    Enum eDeclaFreq
        clngDeclaMonthly = 1
        clngDeclaTwoMonth = 2
        clngDeclaTrheeMonth = 3
        clngDeclaSixMonth = 4
        clngDeclaYear = 5
        clngNonDecla = 6
    End Enum

    '- Enumerado de vía de pago
    Private Enum eWayPay
        clngPayByPAC = 1
        clngPayByTransBank = 2
        clngPayByBrief = 3
        clngPayByBulletin = 4
        clngPayByCoupon = 5
        clngPayByAFP_INP = 7
    End Enum

    '- Enumerado de origen
    Private Enum eOrigin
        clngDepositAPV = 2
        clngDepositVolun = 1
        clngDepoAgreement = 3
    End Enum



    '% existsSpecialProposal: Esta rutina permite saber si existen propuestas especiales para una poliza/certifcado
    Public Function existsSpecialProposal(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Stat_quot) As Boolean
        Dim lrecreaSpecialproposalcount As eRemoteDB.Execute

        On Error GoTo existsSpecialProposal_Err

        '+ Definición de store procedure reaSpecialproposalcount al 03-06-2002 19:14:20
        lrecreaSpecialproposalcount = New eRemoteDB.Execute
        With lrecreaSpecialproposalcount
            .StoredProcedure = "reaSpecialproposalcount"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                existsSpecialProposal = .Parameters("nCount").Value > 0
                specProponum = .Parameters("nProponum").Value
            End If
        End With

existsSpecialProposal_Err:
        If Err.Number Then
            existsSpecialProposal = False
        End If
        'UPGRADE_NOTE: Object lrecreaSpecialproposalcount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaSpecialproposalcount = Nothing
        On Error GoTo 0
    End Function

    '%insCalcPeriodDates: Calcula las fechas de los periodos de cargos
    '% Nota: se debe tener cuidad al usar funcion ya que actualiza valor
    '%       de campos de la clase que pueden estar previamente cargados.
    '%      Por ejemplo, si primero se llama a .Find, y luego a esta
    '%      se va a reemplazar el valor del campo dNextReceip
    Public Function insCalcPeriodDates(ByVal dEffecdate As Date, Optional ByVal nPayfreq As Integer = 0, Optional ByVal sFracReceip As String = "", Optional ByVal sBrancht As String = "", Optional ByVal dExpirdat As Date = #12:00:00 AM#) As Boolean
        Dim lreccalC_period_dates As eRemoteDB.Execute
        On Error GoTo calC_period_dates_Err


        lreccalC_period_dates = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure calC_period_dates al 10-27-2004 18:14:57
        '+
        With lreccalC_period_dates
            .StoredProcedure = "calc_period_dates"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartcurrentperiod", dStartCurrentPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndcurrentperiod", dEndCurrentPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartnextperiod", dStartNextPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextreceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFracreceip", sFracReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insCalcPeriodDates = .Run(False)
            If insCalcPeriodDates Then
                dStartCurrentPeriod = .Parameters("dStartCurrentPeriod").Value
                dEndCurrentPeriod = .Parameters("dEndCurrentPeriod").Value
                dStartNextPeriod = .Parameters("dStartNextPeriod").Value
                dNextReceip = .Parameters("dNextReceip").Value
            End If
        End With

calC_period_dates_Err:
        If Err.Number Then
            insCalcPeriodDates = False
        End If
        'UPGRADE_NOTE: Object lreccalC_period_dates may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccalC_period_dates = Nothing
        On Error GoTo 0
    End Function

    '% InsCalQSurr: Retorna la cantidad de rescates por mes o año
    Public Function InsCalQSurr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal sType As Object = "M") As Integer
        Dim lrecInsCalQSurr As eRemoteDB.Execute

        On Error GoTo InsCalQSurr_Err

        lrecInsCalQSurr = New eRemoteDB.Execute
        InsCalQSurr = -1

        '+ Definición de store procedure InsCalQSurr al 03-15-2002 15:57:13
        With lrecInsCalQSurr
            .StoredProcedure = "InsCalQSurr"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQsurr", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsCalQSurr = .Parameters("nQsurr").Value
            End If
        End With

InsCalQSurr_Err:
        If Err.Number Then
            InsCalQSurr = -1
        End If
        'UPGRADE_NOTE: Object lrecInsCalQSurr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalQSurr = Nothing
        On Error GoTo 0
    End Function

    '% insValCAC001_k: Realiza la validación de los campos
    Public Function insValCAC001_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrrent As Integer, ByVal sExecute As String) As String
        Dim lobjErrors As Object
        Dim lclsPolicy As ePolicy.Policy

        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lclsPolicy = New ePolicy.Policy

        On Error GoTo insValCAC001_k_Err

        '+ Validación del ramo
        If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+ Validación del producto
        If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+ Validación de la póliza
        If nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3916)
        ElseIf (nPolicy <> CDbl("0") Or nPolicy <> eRemoteDB.Constants.intNull) And (nBranch <> CDbl("0") Or nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> CDbl("0") Or nProduct <> eRemoteDB.Constants.intNull) Then
            With lclsPolicy

                '+ Si la póliza no existe
                If Not .Find(sCertype, CInt(nBranch), CInt(nProduct), nPolicy) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3917)
                Else

                    '+ Si está anulada
                    If sExecute = "1" Then
                        If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull And .sStatus_pol = "6" And .dNulldate <> eRemoteDB.Constants.dtmNull Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3098)
                        End If

                        '+ Si no es válida
                        If .sStatus_pol <> "1" And .sStatus_pol <> "3" And .sStatus_pol <> "4" And .sStatus_pol <> "5" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3882)
                        End If
                    End If

                    '+ Debe ser especial o colectiva
                    If .sPolitype = "1" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3109)
                    End If
                End If
            End With
        End If

        If nCurrrent = 0 Or nCurrrent = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 12110)
        End If

        insValCAC001_k = lobjErrors.Confirm

insValCAC001_k_Err:
        If Err.Number Then
            insValCAC001_k = insValCAC001_k & Err.Description
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% insPreCA004. Esta rutina se encarga de realizar las operaciones que corresponden cuando
    '% se entra en el frame de "Datos para la facturación"
    Public Sub insPreCA004(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sSche_code_user As String)
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo inspreCA004_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insCA004PKG.inspreCA004"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSche_code_user", sSche_code_user, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nSendAddr = .FieldToClass("nSendAddr")
                nPayfreq = .FieldToClass("nPayfreq")
                nQuota = .FieldToClass("nQuota")
                sExemption = .FieldToClass("sExemption")
                sBill_Ind = .FieldToClass("sBill_Ind")
                sRenewal = .FieldToClass("sRenewal")
                sProrShort = .FieldToClass("sProrShort")
                dExpirdat = .FieldToClass("dExpirdat")
                dIssuedat = .FieldToClass("dIssuedat")
                dPropodat = .FieldToClass("dPropodat")
                nDuration = .FieldToClass("nDuration")
                dStartdate = .FieldToClass("dStartdate")
                sClient = .FieldToClass("sClient")
                sDirind = .FieldToClass("sDirind")
                nWay_pay = .FieldToClass("nWay_pay")
                nOrigin = .FieldToClass("nOrigin")
                nAFP_Commiss = .FieldToClass("nAFP_Commiss")
                nAFP_Comm_Curr = .FieldToClass("nAFP_Comm_Curr")
                nBill_day = .FieldToClass("nBill_day")
                nDays_quot = .FieldToClass("nDays_quot")
                nCollector = .FieldToClass("nCollector")
                sNopayroll = .FieldToClass("sNopayroll")
                nCod_Agree = .FieldToClass("nCod_Agree")
                sColtimre = .FieldToClass("sColtimre")
                nSpecialbusiness = .FieldToClass("nSpecialbusiness")
                sLeg = .FieldToClass("sLeg")
                sInsubank = .FieldToClass("sInsubank")
                sReinst = .FieldToClass("sReinst")
                nCopies = .FieldToClass("nCopies")
                nNotice = .FieldToClass("nNotice")
                nIndexfac = .FieldToClass("nIndexfac")
                sNoNull = .FieldToClass("sNoNull")
                sDeclari = .FieldToClass("sDeclari")
                sRevalapl = .FieldToClass("sRevalapl")
                sIndextyp = .FieldToClass("sIndextyp")
                sFracReceip = .FieldToClass("sFracReceip")
                nGroup_agree = .FieldToClass("nGroup_agree")
                sCumul_code = .FieldToClass("sCumul_code")
                dDate_Origi = .FieldToClass("dDate_origi")
                nRepInsured = .FieldToClass("nRepInsured")
                sRetarif = .FieldToClass("sRetarif")
                sReceipt_ind = .FieldToClass("sReceipt_ind")
                nTerm_grace = .FieldToClass("nTerm_grace")
                bEnabledtcdIssuedat = IIf(.FieldToClass("sEnabledtcdIssuedat") = "1", True, False)
                bEnabledtcdReqDate = IIf(.FieldToClass("sEnabledtcdReqDate") = "1", True, False)
                bEnabledtcnDuration = IIf(.FieldToClass("sEnabledtcnDuration") = "1", True, False)
                bEnabledtcdExpirDate = IIf(.FieldToClass("sEnabledtcdExpirDate") = "1", True, False)
                bEnabledoptFreq1 = IIf(.FieldToClass("sEnabledoptFreq1") = "1", True, False)
                bEnabledoptFreq2 = IIf(.FieldToClass("sEnabledoptFreq2") = "1", True, False)
                bEnabledoptFreq3 = IIf(.FieldToClass("sEnabledoptFreq3") = "1", True, False)
                bEnabledcbePayFreq = IIf(.FieldToClass("sEnabledcbePayFreq") = "1", True, False)
                bEnabledcbeQuota = IIf(.FieldToClass("sEnabledcbeQuota") = "1", True, False)
                bDisabledchksLeg = IIf(.FieldToClass("sDisabledchksLeg") = "1", True, False)
                bEnabledchkDeclarative = IIf(.FieldToClass("sEnabledchkDeclarative") = "1", True, False)
                bEnabledchkRenewalAut = IIf(.FieldToClass("sEnabledchkRenewalAut") = "1", True, False)
                bEnabledtcnCopies = IIf(.FieldToClass("sEnabledtcnCopies") = "1", True, False)
                bEnabledtcnDaysNull = IIf(.FieldToClass("sEnabledtcnDaysNull") = "1", True, False)
                bEnabledchkNoNull = IIf(.FieldToClass("sEnabledchkNoNull") = "1", True, False)
                bEnabledchksInsubank = IIf(.FieldToClass("sEnabledchksInsubank") = "1", True, False)
                bEnabledtcnIndexRate = IIf(.FieldToClass("sEnabledtcnIndexRate") = "1", True, False)
                bEnabledcbeIndexType = IIf(.FieldToClass("sEnabledcbeIndexType") = "1", True, False)
                bEnabledcbeIndexApl = IIf(.FieldToClass("sEnabledcbeIndexApl") = "1", True, False)
                bEnabledoptDirTyp = IIf(.FieldToClass("sEnabledoptDirTyp") = "1", True, False)
                bEnabledcbeWayPay = IIf(.FieldToClass("sEnabledcbeWayPay") = "1", True, False)
                bEnabledvalAgreement = IIf(.FieldToClass("sEnabledvalAgreement") = "1", True, False)
                bEnabledvalOrigin = IIf(.FieldToClass("sEnabledvalOrigin") = "1", True, False)
                bEnabledtcnAFPCommi = IIf(.FieldToClass("sEnabledtcnAFPCommi") = "1", True, False)
                bEnabledcbeCurrency = IIf(.FieldToClass("sEnabledcbeCurrency") = "1", True, False)
                bEnabledtctClient = IIf(.FieldToClass("sEnabledtctClient") = "1", True, False)
                bEnabledchksNopayroll = IIf(.FieldToClass("sEnabledchksNopayroll") = "1", True, False)
                bEnabledvalgroup_Agree = IIf(.FieldToClass("sEnabledvalgroup_Agree") = "1", True, False)
                bEnabledcbeRepInsured = IIf(.FieldToClass("sEnabledcbeRepInsured") = "1", True, False)
                bEnabledtcnTermgrace = IIf(.FieldToClass("sEnabledtcnTermgrace") = "1", True, False)
                bEnabledtctCumulcode = IIf(.FieldToClass("sEnabledtctcumulcode") = "1", True, False)
                bEnabledchkBillInd = IIf(.FieldToClass("sEnabledchkBillInd") = "1", True, False)
                bEnabledcbeReceiptind = IIf(.FieldToClass("sEnabledcbeReceiptind") = "1", True, False)
                bEnabledtcnBillDay = IIf(.FieldToClass("sEnabledtcnBillDay") = "1", True, False)
                bEnabledtcnDaysquot = IIf(.FieldToClass("sEnabledtcnDaysquot") = "1", True, False)
                sOrigin = .FieldToClass("sOrigin")
                dTariffdate = .FieldToClass("dTariffDate")
                nDepreciationTable = .FieldToClass("nDepreciationTable")
                sInd_Multiannual = .FieldToClass("sInd_Multiannual")
                sIndqsamevalue = .FieldToClass("sIndqsamevalue")
                sInd_IFI = .FieldToClass("sInd_IFI")
                nExtraDay = .FieldToClass("nExtraDay")
                nFormPay = .FieldToClass("nFormPay")
                nPromissory_Note = .FieldToClass("nPromissory_Note")
            End If
        End With

inspreCA004_Err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Sub

    '% insPreVI7010. Esta rutina se encarga de realizar las operaciones que corresponden cuando
    '% se entra en el frame de "Información general VUL"
    Public Sub insPreVI7010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sNomin_quote As String)
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo inspreVI7010_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insVI7010PKG.inspreVI7010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomatic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNomin_quote", sNomin_quote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sClient = .FieldToClass("sClient")
                sFirstname = .FieldToClass("sFirstname")
                sLastname = .FieldToClass("sLastname")
                sLastname2 = .FieldToClass("sLastname2")
                dBirthDat = .FieldToClass("dBirthdat")
                nAge = .FieldToClass("nAge")
                sSexclie = .FieldToClass("sSexclie")
                nSpeciality = .FieldToClass("nSpeciality")
                sSmoking = .FieldToClass("sSmoking")
                nTyperisk = .FieldToClass("nTyperisk")
                nCivilsta = .FieldToClass("nCivilsta")
                nOption = .FieldToClass("nOption")
                nCapital = .FieldToClass("nCapital")
                nCurrency = .FieldToClass("nCurrency")
                sNomin_quote = .FieldToClass("sNomin_quote")
            End If
        End With

inspreVI7010_Err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Sub

    '% insPreCA830. Esta rutina se encarga de realizar las operaciones que corresponden cuando
    '% se entra en el frame de "certificado de cobertura"
    Public Sub insPreCA830(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo insPreCA830_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insCA830PKG.inspreCA830"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nIntermed = .FieldToClass("nIntermed")
                nCoverageCertificate = .FieldToClass("nCoverageCertificate")
                nStatusCoverageCertificate = .FieldToClass("nStatusCoverageCertificate")
                sStatusCoverageCertificate = IIf(nStatusCoverageCertificate = 1, "Emitido", "Pendiente")
            End If
        End With

insPreCA830_Err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Sub
    Public Function Load_insPreCA069(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        '- Se define la variable lrecreaTab_CA069
        Dim lrecreaTab_CA069 As eRemoteDB.Execute
        Dim lintIndex As Integer
        Dim llngTop As Integer

        On Error GoTo Load_insPreCA069_Err

        lrecreaTab_CA069 = New eRemoteDB.Execute
        '% insPreCA069. Esta rutina se encarga de realizar las operaciones que corresponden cuando
        '% se entra en el frame de "Direcciones de Poliza"
        '   Public Sub insPreCA069(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
        'Dim lclsRemote As eRemoteDB.Execute

        With lrecreaTab_CA069
            .StoredProcedure = "INSPRECA069"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then

                mblnCharge = True

                Load_insPreCA069 = Not .EOF
                mblnCharge = True
                lintIndex = -1
                Do While Not .EOF
                    lintIndex = lintIndex + 1
                    If lintIndex >= llngTop Then
                        llngTop = llngTop + 50
                        ReDim Preserve marrCert_Address(llngTop)
                    End If
                    marrCert_Address(lintIndex).sRectype = .FieldToClass("sRectype")
                    marrCert_Address(lintIndex).sDescadd = .FieldToClass("SDESCADD")
                    marrCert_Address(lintIndex).sRectypeAux = .FieldToClass("SRECTYPEAUX")

                    .RNext()
                Loop

                .RCloseRec()
                ReDim Preserve marrCert_Address(lintIndex)
            Else
                Load_insPreCA069 = False
                mblnCharge = False
            End If
        End With

        'sRectype = .FieldToClass("SRECTYPEAUX")
        'sAddress = .FieldToClass("SDESCADDAUX")

Load_insPreCA069_Err:
        If Err.Number Then
            Load_insPreCA069 = False
            mblnCharge = False
        End If
        'UPGRADE_NOTE: Object lrecreaTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_CA069 = Nothing

        On Error GoTo 0
    End Function
    '% insPreCA069 : Carga los datos iniciales de la transacción
    Public Function insPreCA069(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Object

        Dim lclsClass As Object

        lclsClass = New ePolicy.Certificat
        Call lclsClass.LoadClauseByProductG(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

        insPreCA069 = lclsClass

insPreCA022A_Err:
        If Err.Number Then
            'UPGRADE_NOTE: Object insPreCA022A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            insPreCA069 = Nothing
        End If

        lclsClass = Nothing
        On Error GoTo 0
    End Function

    '% DefaultValueCA642: Retorna valores por defecto de transaccion CA642
    '%                    cargados en insPreCA642
    Public Function DefaultValueCA069(ByVal sRectype As Integer) As Object
        Dim caseAux As Object = New Object

        Select Case sRectype
            Case 1
                caseAux = sAddress
            Case 2
                caseAux = sAddress
            Case 3
                caseAux = sAddress
                'Case "tcdStartdate"
                '    DefaultValueCA069 = mdtmStartdate
                'Case "tcdExpirdat"
                '    DefaultValueCA069 = mdtmExpirdat
                'Case "tcdChangdat"
                '    DefaultValueCA069 = mdtmChangdat
                'Case "tcdNextreceip"
                '    DefaultValueCA069 = mdtmNextreceip
                'Case "chkStatusprepp"
                '    If blnStatusprepp Then
                '        DefaultValueCA642 = "1"
                '    Else
                '        DefaultValueCA642 = "2"
                '    End If
                'Case "chkStatusprepc"
                '    If blnStatusprepc Then
                '        DefaultValueCA642 = "1"
                '    Else
                '        DefaultValueCA642 = "2"
                '    End If
                'Case "valNpayfreq"
                '    DefaultValueCA642 = intPayfreq
                'Case "tcdNewChangdat"
                '    DefaultValueCA642 = mdtmDateRecPay
                'Case "tcdDateToForce"
                '    DefaultValueCA642 = mdtmDayForce
                'Case "DateNextreceip"
                '    DefaultValueCA642 = mdtmNewNextreceip
        End Select
        Return caseAux
    End Function
    '% insGetClient: Obtiene el nombre del titular del recibo de pago.
    Private Sub insGetClient(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
        Dim lclsRoles As ePolicy.Roles
        lclsRoles = New ePolicy.Roles

        Me.sClient = String.Empty
        Me.sCliename = String.Empty

        With lclsRoles
            Call .InsGetClientHolder(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

            Me.sClient = .SCLIENT
            Me.sCliename = .sCliename
            Me.nHolder = .nHolder
        End With

        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
    End Sub

    '%insReaCertificnnsDeclafreq: Se encarga de leer la frecuencia de
    '%declaracion de la tabla de datos particulares
    Private Function insReaCertificnnsDeclaFreq(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal valEnv As Boolean) As Boolean
        Dim lrecCertificnn As eRemoteDB.Execute

        On Error GoTo insReaCertificnnsDeclaFreq_err

        lrecCertificnn = New eRemoteDB.Execute

        insReaCertificnnsDeclaFreq = CBool(String.Empty)

        With lrecCertificnn
            .StoredProcedure = "reaCertificnn"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                insReaCertificnnsDeclaFreq = CBool(String.Empty)
            ElseIf Not .EOF Then
                If Not valEnv Then
                    On Error Resume Next
                End If
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If Not IsDBNull(.FieldToClass("sDecla_freq")) Then
                    insReaCertificnnsDeclaFreq = CBool(CStr(.FieldToClass("sDecla_freq")))
                End If
                .RCloseRec()
            End If
        End With

insReaCertificnnsDeclaFreq_err:
        If Err.Number Then
            insReaCertificnnsDeclaFreq = CBool(String.Empty)
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecCertificnn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCertificnn = Nothing
    End Function

    '% AllowRever: Indica si es posible o no el reverso, si se está realizando un reverso de una
    '% modificación al mismo día.
    Public Function AllowRever(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim llngCount As Integer

        lclsPolicy_his = New ePolicy.Policy_his

        '+Solo se permite reversar modificación cuando no se han hecho modificaciones el mismo día
        '+Como al inicio de la secuencia de modificacion se crea automaticamente un movimiento,
        '+si hay más de una modificacion se indica que se han hecho modificaciones previamente a la en curso
        llngCount = lclsPolicy_his.insCountMov(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, Policy_his.ePolicyHisType.ePolHisTypePolicyMod)
        AllowRever = llngCount <= 1

        '+Si existiera más de una modificacion, se valida que estas no hayan sido anuladas
        If Not AllowRever Then
            AllowRever = (llngCount - 1) = lclsPolicy_his.insCountMov(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, Policy_his.ePolicyHisType.ePolHisTypeReverMod)
        End If

        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing

    End Function

    '% insAcceptCA050: Ejecuta los procesos de validación y actualización de datos de la póliza
    '% en proceso cuando se desea terminar el uso de la misma.
    Private Function insAcceptCA050(ByVal lintWaitCode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal blnPrintCarnets As Boolean, ByVal plngOficial_p As Integer) As Boolean
        On Error GoTo insAcceptCA050_Err

        Dim lblnContinue As Boolean
        Dim pintPeriod As Integer
        Dim ldtmNextReceip As Date
        Dim ldtmLedgerDate As Date
        Dim mdtmEffecdate As Date

        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As Policy
        Dim lclsPolicy_his As Policy_his
        Dim lclsCertificat As Certificat
        Dim lcsPremium As eCollection.Premium
        Dim lclsPremium As eCollection.Premium
        Dim lclsFinanceCO As Object
        Dim lclsFinancePre As Object

        lclsProduct = New eProduct.Product
        lclsPolicy = New Policy
        lclsCertificat = New Certificat
        lclsPolicy_his = New Policy_his
        lclsFinanceCO = eRemoteDB.NetHelper.CreateClassInstance("eFinance.financeCO")
        lclsFinancePre = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinancePre")
        lclsPremium = New eCollection.Premium

        '+ Se obtienen los datos correspondientes con el producto.
        Call lclsProduct.Find(nBranch, nProduct, dEffecdate)

        '+ Obtiene los datos de la póliza en tratamiento.
        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)

        Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)

        Call lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif)
        ldtmLedgerDate = lclsPolicy_his.dLedgerDat

        insAcceptCA050 = True
        lblnContinue = True

        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lclsUl_Curr_acc_pol As ePolicy.UL_Curr_Acc_Pol
        If lblnContinue Then

            '+ Tipo de registro: Póliza.
            If sCertype = "2" Then
                mstrStatus_pol = "4"
                pintPeriod = 1
                mstrStatusva = "4"

                '+ Si la póliza se encuentra en estado completo.
                If lintWaitCode = eRemoteDB.Constants.intNull Then
                    plngOficial_p = lclsPolicy.nOficial_p
                    Call insUpdPolicy_his(plngOficial_p, nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate)

                    Call insUpdPolicyCA050(plngOficial_p, sCertype, nBranch, nProduct, nPolicy, mstrStatus_pol)

                    If CStr(lclsProduct.sBrancht) = "1" Then
                        Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate, True)

                        If lclsProduct.nProdClas = 4 Then

                            lclsCurren_pol = New ePolicy.Curren_pol
                            lclsUl_Curr_acc_pol = New ePolicy.UL_Curr_Acc_Pol

                            If lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                            End If

                            With lclsUl_Curr_acc_pol
                                .sCertype = sCertype
                                .nBranch = nBranch
                                .nProduct = nProduct
                                .nPolicy = nPolicy
                                .nCertif = nCertif
                                .nCurrency = lclsCurren_pol.nCurrency
                                .nBalance = 0
                                .nCredit = 0
                                .nDebit = 0
                                .nUsercode = nUsercode
                                .nLed_Compan = eRemoteDB.Constants.intNull
                                .sAccount = String.Empty
                                .sAux_Accoun = String.Empty

                                .Add()
                            End With

                            'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsCurren_pol = Nothing
                            'UPGRADE_NOTE: Object lclsUl_Curr_acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsUl_Curr_acc_pol = Nothing
                        End If
                    End If

                    If Not insUpdPremiumStatusva("4", sCertype, nBranch, nProduct, nPolicy, nUsercode) Then
                        lblnContinue = False
                    Else
                        '+ Si la frecuencia de pago es por cuotas
                        If lclsCertificat.nPayfreq = 8 Then
                            lclsPremium.nBranch = nBranch
                            lclsPremium.nProduct = nProduct
                            lclsPremium.nPolicy = nPolicy
                            lclsPremium.sCertype = sCertype
                            If lclsPremium.Find_Receipt() Then
                                If lclsFinancePre.Find_Receipt(lclsPremium.nReceipt) Then
                                    If lclsFinanceCO.Find_Contrat(lclsFinancePre.nContrat) Then
                                        If lclsFinanceCO.nStat_contr = 5 Then
                                            lclsFinanceCO.nStat_contr = 1
                                            lclsFinanceCO.nUsercode = nUsercode
                                            lclsFinanceCO.Update()
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If CStr(lclsProduct.sBrancht) = "1" Then
                            Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate, True)
                            If lclsProduct.nProdClas = 7 And (nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngRecuperation) And lclsCertificat.nPayfreq = 5 Then

                                lcsPremium = New eCollection.Premium

                                If lcsPremium.Count_premium_ca050(sCertype, nBranch, nProduct, nPolicy, nCertif) < 2 Then

                                    mdtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dEffecdate)

                                    ldtmNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, lclsCertificat.dNextReceip)
                                    mdtmNextreceip = ldtmNextReceip

                                    If lcsPremium.CalReceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, mdtmEffecdate, lclsCertificat.sProrShort, lclsCertificat.dStartdate, lclsCertificat.dExpirdat, lclsPolicy.DEXPIRDAT, lclsPolicy.sColtimre, lclsCertificat.nPayfreq, ldtmNextReceip, lclsPolicy.sPolitype, lclsPolicy.sColinvot, lclsPolicy.sDeclari, 99, ldtmNextReceip, 1, lclsCertificat.nDaysFQ, lclsCertificat.nDaysSQ, nUsercode, CStr(lclsProduct.sBrancht), lclsPolicy.sDirdebit, lclsPolicy.nOffice, lclsPolicy.nIntermed, lclsPolicy.SCLIENT, lclsPolicy.NTRANSACTIO, lclsCertificat.nGroup, "1", 2, 0, 0, eRemoteDB.Constants.intNull, " ", lclsCertificat.dDate_Origi, lclsPolicy.nParticip, ldtmLedgerDate, lclsCertificat.nWay_pay) Then

                                        If Not insUpdPremiumStatusva("4", sCertype, nBranch, nProduct, nPolicy, nUsercode) Then
                                            lblnContinue = False
                                        End If
                                    End If
                                    '+ Se actualiza la fecha de próxima facturación en la tabla Policy y Certificat
                                    Call insUpdNextreceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, ldtmNextReceip, nUsercode)
                                End If
                                'UPGRADE_NOTE: Object lcsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lcsPremium = Nothing
                            End If
                        End If
                        '+ Se actualiza el estado de los movimientos a "Pendiente por facturar" (1).
                        If Not insUpdOut_moveme_Status("1", sCertype, nBranch, nProduct, nPolicy, nCertif, nUsercode) Then
                            lblnContinue = False
                        Else
                            If Not insUpdPremiumCA050(plngOficial_p, lclsPolicy.sPolitype, nCertif, sCertype, nBranch, nProduct, nPolicy) Then
                                lblnContinue = False
                            Else

                                '+ Si la póliza pertenece al ramo Crédito y caución.
                                If insValCredit(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then

                                    '+ Si la póliza es individual.
                                    If lclsPolicy.sPolitype = "1" Then

                                        '+ Actualización del campo heap (cúmulo) de la tabla cliente cuyo código de rol es 9 -> "Afianzado".
                                        ': Ojo: este proceso quedará pendiente hasta que se desarrollo la transacción de crédito y caución.
                                        If Not insUpdClient_heap(sCertype, nBranch, nPolicy, nCertif, dEffecdate) Then
                                            lblnContinue = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If CStr(lclsProduct.sBrancht) = "7" Then
            Call insUpdInsured_he(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode)
        End If

insAcceptCA050_Err:
        If Err.Number Then
            insAcceptCA050 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing
        'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFinanceCO = Nothing
        'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFinancePre = Nothing
        On Error GoTo 0
    End Function

    '% inscreDoc_Quotation: Inserta de un registro en la tabla de documentos de cotización
    Public Function inscreDoc_Quotation(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQuotation As Integer, ByVal sDocument As String, ByVal nUsercode As Integer) As Boolean
        On Error GoTo inscreDoc_Quotation_Err

        Dim lreccreDoc_Quotation As eRemoteDB.Execute

        lreccreDoc_Quotation = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.creDoc_Quotation'
        'Información leída el 18/01/2001 08:17:43 a.m.
        With lreccreDoc_Quotation
            .StoredProcedure = "creDoc_Quotation"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

        'UPGRADE_NOTE: Object lreccreDoc_Quotation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreDoc_Quotation = Nothing

inscreDoc_Quotation_Err:
        If Err.Number Then
            inscreDoc_Quotation = False
        End If
        On Error GoTo 0
    End Function

    '% insExecuteCA048 : Único método público que "dispara" toda la secuencia de la CA048
    Public Function insExecuteCA048(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sPendenStat As String, ByVal nWaitCode As Integer, ByVal nNotenum As Integer, ByVal sAfecCer As String, Optional ByVal nCapital As Double = 0, Optional ByVal sPenstatus_pol As String = "") As Boolean
        Dim lrecinsExecuteca048 As eRemoteDB.Execute
        On Error GoTo insExecuteca048_Err

        lrecinsExecuteca048 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insExecuteca048 al 07-23-2004 12:18:26
        '+
        With lrecinsExecuteca048
            .StoredProcedure = "insExecuteCA048"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPendenstat", sPendenStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitcode", nWaitCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAfeccer", sAfecCer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPenstatus_pol", sPenstatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insExecuteCA048 = .Run(False)
        End With

insExecuteca048_Err:
        If Err.Number Then
            insExecuteCA048 = False
        End If
        'UPGRADE_NOTE: Object lrecinsExecuteca048 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsExecuteca048 = Nothing
        On Error GoTo 0

    End Function

    '% insmodCertif : Procedimiento que realiza la modificación masiva de certificados
    Public Sub insModCertif(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal nCapital As Double)
        Dim lrecupdinsmodCertif As eRemoteDB.Execute

        lrecupdinsmodCertif = New eRemoteDB.Execute

        On Error GoTo insModcertif_Err

        '+
        '+ Definición de store procedure insModcertif al 08-28-2002 20:15:51
        '+
        With lrecupdinsmodCertif
            .StoredProcedure = "insModCertif"
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCal_Receipt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewpol", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

insModcertif_Err:
        'UPGRADE_NOTE: Object lrecupdinsmodCertif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdinsmodCertif = Nothing
        On Error GoTo 0
    End Sub

    '% insExecuteCA050: Método público que "dispara" toda la secuencia de la CA050
    Public Function insExecuteCA050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTransaction As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTypeCompany As String, ByVal sDocument As String, ByVal sDocumentTag As String, ByVal blnEnabledWaitCode As Boolean, ByVal nWaitCode As Integer, ByVal blnDocQuotation As Boolean, ByVal sAfecCer As String, Optional ByVal nCapital As Double = 0, Optional ByVal sDetailedEntryPrinted As String = "") As Boolean

        On Error GoTo insExecuteCA050_Err
        Dim lrecinsExecuteCA050 As eRemoteDB.Execute
        lrecinsExecuteCA050 = New eRemoteDB.Execute

        insExecuteCA050 = True

        With lrecinsExecuteCA050
            .StoredProcedure = "insExecuteCA050"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCompany", nTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocumentTag", sDocumentTag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnabledWaitCode", IIf(blnEnabledWaitCode = True, 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitCode", nWaitCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocQuotation", IIf(blnDocQuotation = True, 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAfecCer", IIf(sAfecCer = "1", "1", "0"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMasive", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDetailedEntryPrinted", sDetailedEntryPrinted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insExecuteCA050 = .Run(False)
        End With

insExecuteCA050_Err:
        If Err.Number Then
            insExecuteCA050 = False
        End If
        On Error GoTo 0
    End Function

    '% insValPolicyLimits: Función que realiza la validación de los límites de una póliza.
    Public Function insValPolicyLimits(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecValLimits As eRemoteDB.Execute

        On Error GoTo insValPolicyLimits_Err

        lrecValLimits = New eRemoteDB.Execute

        insValPolicyLimits = True

        With lrecValLimits
            .StoredProcedure = "reaLimitsPolicy"
            '+ Ojo se debe asignar el valor del código de seguridad del sistema según el usuario. Falta por desarrollarlo.
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sSche_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                If .ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK Then
                    If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
                        insValPolicyLimits = False
                    End If
                Else
                    If .FieldToClass("lblnOk") = 1 Then
                        insValPolicyLimits = False
                    End If
                End If
                .RCloseRec()
            End If
        End With

insValPolicyLimits_Err:
        If Err.Number Then
            insValPolicyLimits = False
        End If
        'UPGRADE_NOTE: Object lrecValLimits may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecValLimits = Nothing
        On Error GoTo 0
    End Function

    '% insCertificat_CA050: Se encarga de realizar la actualización en la tabla 'certificat'
    Public Function insCertificat_CA050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal pstrStatusva As String, ByVal pintWait_code As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy_his As ePolicy.Policy_his

        lclsCertificat = New ePolicy.Certificat
        lclsPolicy_his = New ePolicy.Policy_his

        If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then

            If mdtmNextreceip <> eRemoteDB.Constants.dtmNull Then
                lclsCertificat.dNextReceip = mdtmNextreceip
            End If
            With lclsCertificat
                .sStatusva = pstrStatusva
                .nUser_amend = 0
                .nUsercode = nUsercode
                .nWait_code = pintWait_code
                insCertificat_CA050 = .Update
            End With
        End If

        '+ Se actualiza nWait_code en policy_his
        Call lclsPolicy_his.updPolHisnWait_code(sCertype, nBranch, nProduct, nPolicy, nCertif, nUsercode, pintWait_code, Me.mdtmnMovnumbe)

        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing

    End Function

    '% insPolicy_CA050: Valores para la ventana de fin de emisión
    Public Function insPolicy_CA050(ByRef lobjPolicy As Policy, ByVal nOficial_p As Integer, ByVal nPeriod As Integer, ByVal nUsercode As Integer) As Boolean
        '+ Se asignan los valores de la ventana de Fin de emisión.
        With lobjPolicy
            .sStatus_pol = mstrStatus_pol
            .nUser_amend = 0
            .nUsercode = nUsercode
            .nOficial_p = nOficial_p
            .nDummy = nPeriod
            insPolicy_CA050 = .Add
        End With

    End Function

    '% insUpdDoc_Quotation: Realiza la actualización en la tabla de documentos de cotización
    Public Function insUpdDoc_Quotation(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQuotation As Integer, ByVal sDocument As String, ByVal nUsercode As Integer) As Boolean
        On Error GoTo insUpdDoc_Quotation_Err

        Dim lrecupdDoc_Quotation As eRemoteDB.Execute

        lrecupdDoc_Quotation = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updDoc_Quotation'
        '+ Información leída el 18/01/2001 08:23:12 a.m.
        With lrecupdDoc_Quotation
            .StoredProcedure = "updDoc_Quotation"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdDoc_Quotation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdDoc_Quotation = Nothing

insUpdDoc_Quotation_Err:
        If Err.Number Then
            insUpdDoc_Quotation = False
        End If
        On Error GoTo 0
    End Function

    '% insUpdPolicy_his: Se actualiza la moneda en la historia de la póliza
    Public Function insUpdPolicy_his(ByVal plngOficial_p As Integer, ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        On Error GoTo insUpdPolicy_his_Err

        Dim lstrCurrency As String
        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lrecupdPolicy_hisCA050 As eRemoteDB.Execute

        lclsCurren_pol = New ePolicy.Curren_pol

        With lclsCurren_pol
            If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                If .CountCurrenPol = 0 Then
                    .Val_Curren_pol(0)
                    lstrCurrency = CStr(.nCurrency)
                Else
                    lstrCurrency = String.Empty
                End If
            Else
                lstrCurrency = "1"
            End If
        End With

        lrecupdPolicy_hisCA050 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updPolicy_hisCA050'
        '+ Información leída el 30/11/1999 13:19:07
        With lrecupdPolicy_hisCA050
            .StoredProcedure = "updPolicy_hisCA050"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Trim(lstrCurrency) = String.Empty Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nCurrency", lstrCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("nOfficial_p", plngOficial_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing
        'UPGRADE_NOTE: Object lrecupdPolicy_hisCA050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_hisCA050 = Nothing

insUpdPolicy_his_Err:
        If Err.Number Then
            insUpdPolicy_his = False
        End If
        On Error GoTo 0
    End Function

    '% insUpdPolicy_his: Se actualiza la moneda en la historia de la póliza
    Public Function insUpdPolicyCA050(ByVal plngOficial_p As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sstatus_pol As String) As Boolean
        On Error GoTo insUpdPolicyCA050_Err

        Dim lrecupdPolicyCA050 As eRemoteDB.Execute

        lrecupdPolicyCA050 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updPolicyCA050'
        '+ Información leída el 30/11/1999 13:17:06
        With lrecupdPolicyCA050
            .StoredProcedure = "updPolicyCA050"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOficial_p", plngOficial_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus_pol", sstatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdPolicyCA050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicyCA050 = Nothing

insUpdPolicyCA050_Err:
        If Err.Number Then
            insUpdPolicyCA050 = False
        End If
        On Error GoTo 0
    End Function

    '% insUpdPremiumStatusva: Función que realiza las actualizaciones respectivas del histórico de la póliza (policy_his).
    Private Function insUpdPremiumStatusva(ByVal lstrStatusva As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Integer) As Boolean
        On Error GoTo insUpdPremiumStatusva_Err

        Dim lrecupdPremium_sStatusva As eRemoteDB.Execute
        Dim llngAux As Integer

        lrecupdPremium_sStatusva = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updPremium_sStatusva'
        '+ Información leída el 30/11/1999 13:22:01
        With lrecupdPremium_sStatusva
            .StoredProcedure = "updPremium_sStatusva"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusva", lstrStatusva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", llngAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters.Item("ncount").Value > 0 Then
                    insUpdPremiumStatusva = False
                Else
                    insUpdPremiumStatusva = True
                End If
                insUpdPremiumStatusva = True
            Else
                insUpdPremiumStatusva = True
            End If
        End With

        'UPGRADE_NOTE: Object lrecupdPremium_sStatusva may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPremium_sStatusva = Nothing

insUpdPremiumStatusva_Err:
        If Err.Number Then
            insUpdPremiumStatusva = False
        End If
        On Error GoTo 0
    End Function

    '% insUpdOut_moveme_Status: Función que realiza las actualizaciones al estado del movimiento.
    Public Function insUpdOut_moveme_Status(ByVal lstrStatus_mov As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer) As Boolean
        On Error GoTo insUpdOut_moveme_Status_Err

        Dim lrecOut_moveme As eRemoteDB.Execute

        lrecOut_moveme = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updOut_moveme_sStatus_mov'
        '+ Información leída el 02/12/1999 11:16:05 AM
        With lrecOut_moveme
            .StoredProcedure = "updOut_moveme_sStatus_mov"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus_mov", lstrStatus_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
            End If
            insUpdOut_moveme_Status = .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngOK
        End With
        'UPGRADE_NOTE: Object lrecOut_moveme may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecOut_moveme = Nothing

insUpdOut_moveme_Status_Err:
        If Err.Number Then
            insUpdOut_moveme_Status = False
        End If
        On Error GoTo 0
    End Function

    '% insUpdPremiumCA050: Se actualizan los movimientos de recibos de la póliza en tratamiento.
    Private Function insUpdPremiumCA050(ByVal llngOficial_p As Integer, ByVal sPoltype As String, ByVal nCertif As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        On Error GoTo insUpdPremiumCA050_Err

        Dim lrecupdPremiumPeriod As eRemoteDB.Execute
        Dim lrecPremium As eRemoteDB.Execute
        Dim lclsPolicy As ePolicy.Policy
        Dim lblnOk As Boolean

        lclsPolicy = New ePolicy.Policy
        lrecPremium = New eRemoteDB.Execute

        insUpdPremiumCA050 = True
        lblnOk = False

        '+ Póliza individual.
        If (sPoltype = "1" And nCertif = 0) Then

            '+ Definición de parámetros para stored procedure 'insudb.reaPremium_a'
            '+ Información leída el 01/12/1999 11:22:41 AM
            With lrecPremium
                .StoredProcedure = "reaPremium_a"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                End If
                lblnOk = True
            End With

        End If

        '+ Obtiene los datos de la póliza
        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
        End If

        '+ Certificado de una póliza. Si los recibos estan por certificado.
        If lclsPolicy.sColinvot = "2" Then
            If (sPoltype <> "1" And nCertif <> 0) Then

                '+ Definición de parámetros para stored procedure 'insudb.reaPremium_Premium_ce_a'
                '+ Información leída el 01/12/1999 11:40:55 AM
                With lrecPremium
                    .StoredProcedure = "reaPremium_Premium_ce_a"
                    .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run Then
                    End If
                    lblnOk = True
                End With
            End If
        End If

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing

        '+ Si se ejecutó la lectura del RecordSet.
        If lblnOk Then

            With lrecPremium
                If .ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK Then
                    If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
                        insUpdPremiumCA050 = False
                    End If
                Else
                    Do While Not .EOF

                        lrecupdPremiumPeriod = New eRemoteDB.Execute

                        '+ Definición de parámetros para stored procedure 'insudb.updPremiumPeriod'
                        '+ Información leída el 01/12/1999 11:10:58 AM
                        With lrecupdPremiumPeriod
                            .StoredProcedure = "updPremiumPeriod"
                            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nReceipt", lrecPremium.FieldToClass("nReceipt"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nPeriod", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Run(False)
                        End With

                        '+ Se obliga a salir del ciclo ya que cuando es por certificado pueden haber más de un registro (se trata el primero); ya
                        '+ que lo que interesa es el número del recibo.
                        Exit Do
                    Loop
                    .RCloseRec()

                End If
            End With
        End If

        'UPGRADE_NOTE: Object lrecupdPremiumPeriod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPremiumPeriod = Nothing
        'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPremium = Nothing

insUpdPremiumCA050_Err:
        If Err.Number Then
            insUpdPremiumCA050 = False
        End If
        On Error GoTo 0
    End Function

    '% insValCredit: Función que verifica si una póliza pertenece al ramo de Crédito y caución.
    Private Function insValCredit(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        On Error GoTo insValCredit_Err

        Dim lrecCredit As eRemoteDB.Execute

        lrecCredit = New eRemoteDB.Execute

        insValCredit = True

        '+ Definición de parámetros para stored procedure 'insudb.reaCredit_v'
        '+ Información leída el 01/12/1999 10:30:37 AM
        With lrecCredit
            .StoredProcedure = "reaCredit_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
            End If
            If .ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK Then
                If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
                    insValCredit = False
                End If
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(.FieldToClass("nCount")) Or .FieldToClass("nCount") = 0 Then
                    insValCredit = False
                End If
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecCredit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCredit = Nothing

insValCredit_Err:
        If Err.Number Then
            insValCredit = False
        End If
        On Error GoTo 0
    End Function

    '% insValRoles_nRole: Función que verifica la existencia de un determinado role de un cliente de la póliza.
    Private Function insValRoles_nRole(ByVal lintRole As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecRole_nRole As eRemoteDB.Execute

        On Error GoTo insValRoles_nRole_Err
        insValRoles_nRole = True

        lrecRole_nRole = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaRoles_v_nRole'
        'Información leída el 03/12/1999 09:22:34 AM

        With lrecRole_nRole
            .StoredProcedure = "reaRoles_v_nRole"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", lintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then

            End If


            'Set lrecRole_nRole = insExecuteQuery("insudb.reaRoles_v_nRole", clngQuery, lvntParameters, True, False)
            If .ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK Then
                If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
                    insValRoles_nRole = False
                End If
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(.FieldToClass("nCount")) Then ' Or lrecCredit.RecOfRecordset.FieldToClass("nCount") = 0 Then
                    insValRoles_nRole = False
                End If
                .RCloseRec()
            End If
        End With

insValRoles_nRole_Err:
        If Err.Number Then
            insValRoles_nRole = False
        End If
        'UPGRADE_NOTE: Object lrecRole_nRole may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecRole_nRole = Nothing
        On Error GoTo 0
    End Function

    '% insUpdClient_heap: Función que actualiza el campo de cúmulo del afianzado de la póliza.
    Private Function insUpdClient_heap(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        '+ Se verifica si la póliza tiene cliente afianzado.
        Call insValRoles_nRole(CInt("9"), sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
    End Function

    '% insUpdInsured_he: Se actualiza el Nº de carnet de los asegurados de la póliza (Atención médica)
    Private Sub insUpdInsured_he(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer)
        Dim lclsInsured_he As eBranches.Insured_he
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lintPos As Integer

        On Error GoTo insUpdInsured_he

        lclsInsured_he = New eBranches.Insured_he
        lclsGeneral = New eGeneral.GeneralFunction

        lintPos = 0

        With lclsInsured_he
            If .Load(sCertype, nBranch, nProduct, nPolicy, nCertif, 0, dEffecdate) Then
                Do While .Item(lintPos)
                    If .sCarnet = String.Empty Then
                        .sIndicator = "1"
                        .sCarnet = CStr(lclsGeneral.Find_Numerator(15, 0, nUsercode))
                        .Update()
                    End If
                    lintPos = lintPos + 1
                Loop
            End If
        End With

insUpdInsured_he:
        'UPGRADE_NOTE: Object lclsInsured_he may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsInsured_he = Nothing
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGeneral = Nothing
    End Sub

    '% ParticularTable: busca el nombre de la tabla de datos particulares en Certificat,
    '%                  si no la encuentra, la busca en Policy
    Public ReadOnly Property ParticularTable() As String
        Get
            Dim lclsPolicy As Policy
            Dim varAux As String = ""
            lclsPolicy = New Policy

            If Find_ParticularData(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                varAux = sTabname
            Else
                If lclsPolicy.Find_TabNameB(nBranch) Then
                    varAux = lclsPolicy.sTabname
                End If
            End If
            Return varAux
        End Get
    End Property

    '% StateVarCa004: Obtiene el estado del campo indicado para la transacción CA004
    Public ReadOnly Property StateVarCa004(ByVal nControlName As eTypeControlsCA004) As Boolean
        Get
            Select Case nControlName
                '+ Indice de la aplicación
                Case eTypeControlsCA004.cbeIndexApl
                    StateVarCa004 = bEnabledcbeIndexApl

                    '+ Indice del tipo de revalorización
                Case eTypeControlsCA004.cbeIndexType
                    StateVarCa004 = bEnabledcbeIndexType

                    '+ Indice del tipo de frecuencia de pago.
                Case eTypeControlsCA004.cbePayFreq
                    StateVarCa004 = bEnabledcbePayFreq

                    '+ Indice del tipo de cuotas.
                Case eTypeControlsCA004.cbeQuota
                    StateVarCa004 = bEnabledcbeQuota

                    '+ Revalorización declarativa.
                Case eTypeControlsCA004.chkDeclarative
                    StateVarCa004 = bEnabledchkDeclarative

                    '+ Exonerada
                Case eTypeControlsCA004.chkNoNull
                    StateVarCa004 = bEnabledchkNoNull

                    '+ Renovación automática
                Case eTypeControlsCA004.chkRenewalAut
                    StateVarCa004 = bEnabledchkRenewalAut

                    '+ Option button corto plazo
                Case eTypeControlsCA004.optFreq1
                    StateVarCa004 = bEnabledoptFreq1

                    '+ Option button prorrata
                Case eTypeControlsCA004.optFreq2
                    StateVarCa004 = bEnabledoptFreq2

                    '+ Option button prorrata
                Case eTypeControlsCA004.optFreq3
                    StateVarCa004 = bEnabledoptFreq3

                    '+ Fecha de vencimiento
                Case eTypeControlsCA004.tcdExpirDate
                    StateVarCa004 = bEnabledtcdExpirDate

                    '+ Fecha de emisión
                Case eTypeControlsCA004.tcdIssuedat
                    StateVarCa004 = bEnabledtcdIssuedat

                    '+ Fecha de recepción
                Case eTypeControlsCA004.tcdReqDate
                    StateVarCa004 = bEnabledtcdReqDate

                    '+ Meses de duración
                Case eTypeControlsCA004.tcnDuration
                    StateVarCa004 = bEnabledtcnDuration

                    '+ Copias
                Case eTypeControlsCA004.tcnCopies
                    StateVarCa004 = bEnabledtcnCopies

                    '+ Días de aviso
                Case eTypeControlsCA004.tcnDaysNull
                    StateVarCa004 = bEnabledtcnDaysNull

                    '+ Porcentaje
                Case eTypeControlsCA004.tcnIndexRate
                    StateVarCa004 = bEnabledtcnIndexRate

                    '+ Titular del recibo
                Case eTypeControlsCA004.tctClient
                    StateVarCa004 = bEnabledtctClient

                    '+Domiciliación
                Case eTypeControlsCA004.optDirTyp
                    StateVarCa004 = bEnabledoptDirTyp

                    '+Vía de pago
                Case eTypeControlsCA004.cbeWayPay
                    StateVarCa004 = bEnabledcbeWayPay

                    '+Origen del pago
                Case eTypeControlsCA004.valOrigin
                    StateVarCa004 = bEnabledvalOrigin

                    '+Comisión AFP
                Case eTypeControlsCA004.tcnAFPCommi
                    StateVarCa004 = bEnabledtcnAFPCommi

                    '+Moneda de la comisión AFP
                Case eTypeControlsCA004.cbeCurrency
                    StateVarCa004 = bEnabledcbeCurrency

                    '+Convenio de pago
                Case eTypeControlsCA004.valAgreement
                    StateVarCa004 = bEnabledvalAgreement

                    '+ Cálculo del LEG
                Case eTypeControlsCA004.chksLeg
                    StateVarCa004 = bDisabledchksLeg

                    '+ BancaSeguros
                Case eTypeControlsCA004.chksInsubank
                    StateVarCa004 = bEnabledchksInsubank

                    '+ Póliza innominada
                Case eTypeControlsCA004.chksNopayroll
                    StateVarCa004 = bEnabledchksNopayroll

                    '+ Póliza innominada
                Case eTypeControlsCA004.valgroup_Agree
                    StateVarCa004 = bEnabledvalgroup_Agree

                    '+ Criterio de duplicidad
                Case eTypeControlsCA004.cbeRepInsured
                    StateVarCa004 = bEnabledcbeRepInsured
                    'Plazo de gracia
                Case eTypeControlsCA004.tcnTerm_grace
                    StateVarCa004 = bEnabledtcnTermgrace

                    'Código del cúmulo
                Case eTypeControlsCA004.tctCumul_code
                    StateVarCa004 = bEnabledtctCumulcode

                    'Indicador de factura
                Case eTypeControlsCA004.chkBill_Ind
                    StateVarCa004 = bEnabledchkBillInd

                    'Indicador de generación de recibos
                Case eTypeControlsCA004.cbeReceipt_ind
                    StateVarCa004 = bEnabledcbeReceiptind

                    'Día de pago
                Case eTypeControlsCA004.tcnBillDay
                    StateVarCa004 = bEnabledtcnBillDay

                    'Días de validez
                Case eTypeControlsCA004.tcnDays_quot
                    StateVarCa004 = bEnabledtcnDaysquot


            End Select
        End Get
    End Property

    '% insCertificat_CA004: Esta rutina se encarga de realizar la actualización en la
    '% tabla 'certificat'
    Public Function insCertificat_CA004(ByVal sClient As String, ByVal nCertif As Integer, ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal dStartdate As Date, ByVal dExpirDate As Date, ByVal dIssuedat As Date, ByVal dReqDate As Date, ByVal sRenewalAut As String, ByVal sFracti As String, ByVal sFreq As String, ByVal sDirTyp As String, ByVal nWayPay As Integer, ByVal nBill_day As Integer, ByVal nSendAddr As Integer, ByVal nDays_quot As Integer, ByVal dEffecdate As Date, ByVal sBill_Ind As String, ByVal nDuration As Integer, ByVal sExemption As String, Optional ByVal nOrigin As Integer = 0, Optional ByVal nAFP_Commiss As Double = 0, Optional ByVal nAFP_Comm_Curr As Integer = 0, Optional ByVal nGroup As Integer = 0, Optional ByVal nSituation As Integer = 0) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsProduct As eProduct.Product
        Dim lclsGeneral As Object
        'Dim lblnFirstTime As Boolean

        '+Se definen las variables lintdaysvig,lintdaysforq y lintDaysFirstQ usadas para
        '+los cálculos de (Días de primera cuota y cuotas subsiguientes) de datos de
        '+facturación <CA004>

        Dim lintDaysVig As Integer
        Dim lintDaysForQ As Integer
        Dim lintDaysFirstQ As Integer
        'Dim lintCount As Integer

        lclsPolicy = New ePolicy.Policy
        lclsGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")

        '+ Se obtienen los datos que corresponden con la póliza.
        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then

            '+ Se obtienen los datos que corresponden con el certificado
            If Find(lclsPolicy.sCertype, lclsPolicy.nBranch, lclsPolicy.nProduct, lclsPolicy.nPolicy, nCertif) Then

                '+Si la forma de pago es por cuota, se realizan los cálculos correspondientes a la cantidad de días de la
                '+primera cuota y cuotas subsiguientes.
                If Not nPayfreq = 0 Then
                    If sFreq = "3" And nPayfreq = 8 Then
                        '+Si ambas son fechas válidas
                        If dStartdate <> eRemoteDB.Constants.dtmNull And dExpirDate <> eRemoteDB.Constants.dtmNull Then
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
                        lintDaysFirstQ = Int(lintDaysVig - (lintDaysForQ * nQuota)) + lintDaysForQ
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


                With Me
                    '+<Titular del recibo>
                    .sClient = sClient
                    .dExpirdat = dExpirDate

                    '+<Fecha de emisión>
                    .dIssuedat = dIssuedat
                    '+<Fecha de recepción>
                    If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngCertifReissue Or nTransaction = Constantes.PolTransac.clngPolicyReissue Then
                        .dPropodat = lclsGeneral.LetValue(dReqDate, eGeneral.GeneralFunction.eTypeData.TypDate)
                    End If
                    '+<Indic. Renovac. automática>
                    .sRenewal = IIf(sRenewalAut = "1", "1", "2")
                    '+<Tipo de facturación>
                    If sFracti = CStr(System.Windows.Forms.CheckState.Checked) Then
                        .sProrShort = "9" 'Fraccionada (Vida no tradicional)
                    Else
                        If sFreq = "1" Then
                            .sProrShort = CStr(1)
                        ElseIf sFreq = "2" Then
                            .sProrShort = CStr(2)
                        ElseIf sFreq = "3" Then
                            .sProrShort = CStr(3)
                        End If
                    End If
                    '+<Tipo de frecuencia de pago>

                    If nPayfreq <> 0 Then
                        .nPayfreq = nPayfreq
                    End If

                    '+Se determina la fecha de próxima emisión de recibo de la póliza
                    If sFreq = "3" Then
                        If lclsPolicy.sColinvot = "1" Then
                            .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dStartdate)
                        Else
                            .dNextReceip = eRemoteDB.Constants.dtmNull
                        End If
                        If Not nPayfreq = 0 Then
                            Select Case nPayfreq
                                '+<Anual>
                                Case 1
                                    .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dStartdate)
                                    '+<Semestral>
                                Case 2
                                    .dNextReceip = lclsGeneral.LetValue(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, dStartdate), eGeneral.GeneralFunction.eTypeData.TypDate)
                                    '+<Trimestral>
                                Case 3
                                    .dNextReceip = lclsGeneral.LetValue(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, dStartdate), eGeneral.GeneralFunction.eTypeData.TypDate)
                                    '+<Bimestral>
                                Case 4
                                    .dNextReceip = lclsGeneral.LetValue(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, dStartdate), eGeneral.GeneralFunction.eTypeData.TypDate)
                                    '+<Mensual>
                                Case 5
                                    .dNextReceip = lclsGeneral.LetValue(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dStartdate), eGeneral.GeneralFunction.eTypeData.TypDate)
                                    '+<Unica>
                                Case 6
                                    '+<Cuotas>
                                Case 8
                                    '+ La fecha de próxima emisión del recibo es igual a la fecha de expiración de la póliza
                                    .dNextReceip = dExpirDate
                                Case Else
                                    .dNextReceip = lclsGeneral.LetValue(lclsPolicy.dNextReceip, eGeneral.GeneralFunction.eTypeData.TypDate)
                            End Select
                        End If
                    Else
                        If nCertif <> 0 And lclsPolicy.sColtimre = "1" Then
                            .dNextReceip = lclsGeneral.LetValue(lclsPolicy.dNextReceip, eGeneral.GeneralFunction.eTypeData.TypDate)
                        Else
                            .dNextReceip = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dStartdate)
                        End If
                    End If

                    '+ Si el producto en tratamiento es de vida restar 1 día a fecha proxima facturación del recibo
                    If .dNextReceip <> eRemoteDB.Constants.dtmNull Then
                        lclsProduct = New eProduct.Product
                        Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
                        If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Then
                            Select Case nTransaction
                                Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngCertifProposal, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngReprint, Constantes.PolTransac.clngdeclarations, Constantes.PolTransac.clngCoverNote
                                    .dNextReceip = System.DateTime.FromOADate(.dNextReceip.ToOADate - 1)
                            End Select
                        End If
                    End If

                    If .dNextReceip <> eRemoteDB.Constants.dtmNull Then
                        If lclsPolicy.sPolitype <> "1" And lclsPolicy.sColtimre = "1" And nCertif <> 0 Then
                            If lclsPolicy.DEXPIRDAT <> eRemoteDB.Constants.dtmNull Then
                                If .dNextReceip > lclsPolicy.DEXPIRDAT Then
                                    .dNextReceip = lclsPolicy.dNextReceip
                                End If
                            End If

                            If lclsPolicy.dNextReceip <> eRemoteDB.Constants.dtmNull Then
                                If Me.sFracReceip = "1" Then
                                    If .dNextReceip > lclsPolicy.dNextReceip Then
                                        .dNextReceip = lclsPolicy.dNextReceip
                                    End If
                                End If
                            End If

                        End If
                    End If
                    '+<Cuotas>
                    .nQuota = CInt(0 & IIf(nQuota = eRemoteDB.Constants.intNull, 0, nQuota))
                    '+<Días de la primera cuota>
                    .nDaysFQ = lintDaysFirstQ
                    '+<Cuotas subsiguientes>
                    .nDaysSQ = lintDaysForQ
                    '+Domiciliación
                    .sDirind = sDirTyp
                    '+Vía de pago
                    .nWay_pay = nWayPay
                    '+Día de pago
                    .nBill_day = nBill_day
                    '+Direcion de envio
                    .nSendAddr = nSendAddr
                    '+Indicador de factura
                    .sBill_Ind = IIf(sBill_Ind = "1", "1", "2")
                    '+Duración en meses
                    .nDuration = nDuration
                    '+Exención de pago de primera prima
                    .sExemption = IIf(sExemption = "1", "1", "2")

                    '+ Origen, comisión AFP y moneda en la cual la comisión AFP esta expresada
                    .nOrigin = nOrigin
                    .nAFP_Commiss = nAFP_Commiss
                    .nAFP_Comm_Curr = nAFP_Comm_Curr

                    .nGroup = nGroup
                    .nSituation = nSituation

                    If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropRenewal Or nTransaction = Constantes.PolTransac.clngCertifQuotRenewal Then
                        '+ Días de validez
                        .nDays_quot = nDays_quot
                        .dMaximum_da = DateAdd(Microsoft.VisualBasic.DateInterval.Day, IIf(nDays_quot = eRemoteDB.Constants.intNull, 0, nDays_quot), dEffecdate)
                    End If

                    If nTransaction = Constantes.PolTransac.clngCertifIssue Or
                        nTransaction = Constantes.PolTransac.clngRecuperation Or
                        nTransaction = Constantes.PolTransac.clngCertifQuotation Or
                        nTransaction = Constantes.PolTransac.clngCertifProposal Then

                        '+ cuando la generacion de recibos es por poliza
                        If lclsPolicy.sPolitype <> "1" And lclsPolicy.sColtimre = "1" And lclsPolicy.sColinvot <> "2" Then
                            .dNextReceip = lclsPolicy.dNextReceip
                        End If
                    End If

                    insCertificat_CA004 = .Update
                End With
            End If
        End If

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGeneral = Nothing
    End Function

    '% Add: Se genera el registro en la tabla Certificat para la póliza/certificado
    Public Function Add() As Boolean
        Add = Update()
    End Function

    '% ClearFields: función que limpia las variables de la clase y les asigna el valor de una constante
    '%              que semeja el valor null
    Public Function ClearFields() As Boolean
        ClearFields = True
        sCertype = String.Empty
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        sClient = String.Empty
        nCapital = eRemoteDB.Constants.intNull
        sCumul_code = String.Empty
        dDat_no_con = eRemoteDB.Constants.dtmNull
        dDate_Origi = eRemoteDB.Constants.dtmNull
        dChangdat = eRemoteDB.Constants.dtmNull
        dExpirdat = eRemoteDB.Constants.dtmNull
        nGroup = eRemoteDB.Constants.intNull
        dIssuedat = eRemoteDB.Constants.dtmNull
        dMaximum_da = eRemoteDB.Constants.dtmNull
        nNo_convers = eRemoteDB.Constants.intNull
        nNote_benef = eRemoteDB.Constants.intNull
        nNote_drisk = eRemoteDB.Constants.intNull
        nNullcode = eRemoteDB.Constants.intNull
        dNulldate = eRemoteDB.Constants.dtmNull
        nPayfreq = eRemoteDB.Constants.intNull
        nPremium = eRemoteDB.Constants.intNull
        dPropodat = eRemoteDB.Constants.dtmNull
        sRenewal = String.Empty
        nSituation = eRemoteDB.Constants.intNull
        dStartdate = eRemoteDB.Constants.dtmNull
        sStatusva = String.Empty
        nUser_amend = eRemoteDB.Constants.intNull
        nUsercode = eRemoteDB.Constants.intNull
        nSus_branch = eRemoteDB.Constants.intNull
        nWait_code = eRemoteDB.Constants.intNull
        nSus_product = eRemoteDB.Constants.intNull
        nSus_policy = eRemoteDB.Constants.intNull
        nSus_certif = eRemoteDB.Constants.intNull
        dNextReceip = eRemoteDB.Constants.dtmNull
        nProponum = eRemoteDB.Constants.intNull
        nQuota = eRemoteDB.Constants.intNull
        nDaysFQ = eRemoteDB.Constants.intNull
        sProrShort = String.Empty
        nDaysSQ = eRemoteDB.Constants.intNull
        sNumForm = String.Empty
        sReinsura = String.Empty
        sClaimind = String.Empty
        sException = String.Empty
        nExcCause = eRemoteDB.Constants.intNull
        sProperUse = String.Empty
        sExemption = String.Empty
        nImageNum = eRemoteDB.Constants.intNull
        sDirind = String.Empty
        nWay_pay = eRemoteDB.Constants.intNull
        nBill_day = eRemoteDB.Constants.intNull
        nSendAddr = eRemoteDB.Constants.intNull
        sAut_guarval = String.Empty
        nSellChannel = eRemoteDB.Constants.intNull
        sAnulletter = String.Empty
        dFer = eRemoteDB.Constants.dtmNull
        nDays_quot = eRemoteDB.Constants.intNull
        nPol_quot = eRemoteDB.Constants.intNull
        dDate_accept = eRemoteDB.Constants.dtmNull
        nStatquota = eRemoteDB.Constants.intNull
        dCollsus_ini = eRemoteDB.Constants.dtmNull
        dCollsus_end = eRemoteDB.Constants.dtmNull
        sRefundprem = String.Empty
        nSus_reason = eRemoteDB.Constants.intNull
        sSus_origi = String.Empty
        sPendinfo = String.Empty
        sBill_Ind = String.Empty
        sReceipt_ind = String.Empty
        nRenewalnum = eRemoteDB.Constants.intNull
        nDigit = eRemoteDB.Constants.intNull
        nProp_reg = eRemoteDB.Constants.intNull
        nOrigin = eRemoteDB.Constants.intNull
        nAFP = eRemoteDB.Constants.intNull
        nAFP_Comm_Curr = eRemoteDB.Constants.intNull
        nAFP_Commiss = eRemoteDB.Constants.intNull
        sPayer = String.Empty
        sNomin_quote = String.Empty
        nTerm_grace = eRemoteDB.Constants.intNull
        dTariffdate = eRemoteDB.Constants.dtmNull
    End Function

    '%Find: Carga la información del certificado de una póliza en las variable públicas de
    '%      la clase
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaCertificat_branch As eRemoteDB.Execute

        On Error GoTo Find_Err
        lrecreaCertificat_branch = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bFind Then

            '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
            With lrecreaCertificat_branch
                .StoredProcedure = "reaCertificat_branch"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nCapital = .FieldToClass("nCapital")
                    sCumul_code = .FieldToClass("sCumul_code")
                    dDat_no_con = .FieldToClass("dDat_no_con")
                    dDate_Origi = .FieldToClass("dDate_origi")
                    dChangdat = .FieldToClass("dChangdat")
                    dExpirdat = .FieldToClass("dExpirdat")
                    nGroup = .FieldToClass("nGroup")
                    dIssuedat = .FieldToClass("dIssuedat")
                    dMaximum_da = .FieldToClass("dMaximum_da")
                    nNo_convers = .FieldToClass("nNo_convers")
                    nNote_benef = .FieldToClass("nNote_benef")
                    nNote_drisk = .FieldToClass("nNote_drisk")
                    nNullcode = .FieldToClass("nNullcode")
                    dNulldate = .FieldToClass("dNulldate")
                    nPayfreq = .FieldToClass("nPayfreq")
                    nPremium = .FieldToClass("nPremium")
                    dPropodat = .FieldToClass("dPropodat")
                    sRenewal = .FieldToClass("sRenewal")
                    nSituation = .FieldToClass("nSituation")
                    dStartdate = .FieldToClass("dStartdate")
                    sStatusva = .FieldToClass("sStatusva")
                    nWait_code = .FieldToClass("nWait_code")
                    nSus_branch = .FieldToClass("nSus_branch")
                    nSus_product = .FieldToClass("nSus_product")
                    nSus_policy = .FieldToClass("nSus_policy")
                    nSus_certif = .FieldToClass("nSus_certif")
                    sClient = .FieldToClass("sClient")
                    nUser_amend = .FieldToClass("nUser_Amend")
                    nProponum = .FieldToClass("nPropoNum")
                    dNextReceip = .FieldToClass("dNextReceip")
                    nQuota = .FieldToClass("nQuota")
                    nDaysFQ = .FieldToClass("nDaysFQ")
                    nDaysSQ = .FieldToClass("nDaysSQ")
                    sNumForm = .FieldToClass("sNumForm")
                    sProrShort = .FieldToClass("sProrShort")
                    sReinsura = .FieldToClass("sReinsura")
                    sClaimind = .FieldToClass("sClaimind")
                    sException = .FieldToClass("sException")
                    nExcCause = .FieldToClass("nExcCause")
                    sProperUse = .FieldToClass("sProperUse")
                    nImageNum = .FieldToClass("nImageNum")
                    sExemption = .FieldToClass("sExemption")
                    sDirind = .FieldToClass("sDirInd")
                    nWay_pay = .FieldToClass("nWay_pay")
                    nBill_day = .FieldToClass("nBill_day")
                    nSendAddr = .FieldToClass("nSendAddr")
                    sAut_guarval = .FieldToClass("sAut_guarval")
                    nSellChannel = .FieldToClass("nSellChannel")
                    dFer = .FieldToClass("dFer")
                    nDays_quot = .FieldToClass("nDays_quot")
                    nStatquota = .FieldToClass("nStatQuota")
                    dCollsus_ini = .FieldToClass("dCollSus_ini")
                    dCollsus_end = .FieldToClass("dCollSus_end")
                    sRefundprem = .FieldToClass("sRefundPrem")
                    nSus_reason = .FieldToClass("nSus_reason")
                    sSus_origi = .FieldToClass("sSus_origi")
                    sAnulletter = .FieldToClass("sAnulletter")
                    sPendinfo = .FieldToClass("sPendInfo")
                    sBill_Ind = .FieldToClass("sBill_Ind")
                    nProp_reg = .FieldToClass("nProp_reg")
                    nDigit = .FieldToClass("nDigit")
                    nRenewalnum = .FieldToClass("nRenewalnum")
                    nDigit = .FieldToClass("nDigit")
                    nProp_reg = .FieldToClass("nProp_reg")
                    nDuration = .FieldToClass("nDuration")
                    nPol_quot = .FieldToClass("nPol_quot")
                    nAFP_Commiss = .FieldToClass("nAFP_Commiss")
                    nAFP_Comm_Curr = .FieldToClass("nAFP_Comm_Curr")
                    nOrigin = .FieldToClass("nOrigin")
                    nCollector = .FieldToClass("nCollector")
                    sFracReceip = .FieldToClass("sFracReceip")
                    nGroup_agree = .FieldToClass("nGroup_Agree")
                    nUsercode = .FieldToClass("nUsercode")
                    sRetarif = .FieldToClass("sRetarif")
                    dLast_tarif = .FieldToClass("dLast_tarif")
                    nFolio = .FieldToClass("nFolio")
                    nTypeExc = .FieldToClass("nTypeExc")
                    nCod_Agree = .FieldToClass("nCod_Agree")

                    nOffice_Associated = .FieldToClass("nOffice_Associated")
                    nOfficeAgen_Associated = .FieldToClass("nOfficeAgen_Associated")
                    nAgency_Associated = .FieldToClass("nAgency_Associated")
                    nBranch_Associated = .FieldToClass("nBranch_Associated")
                    nProduct_Associated = .FieldToClass("nProduct_Associated")
                    nPolicy_Associated = .FieldToClass("nPolicy_Associated")
                    nCertif_Associated = .FieldToClass("nCertif_Associated")
                    nDepreciationTable = .FieldToClass("nDepreciationTable")
                    sInd_Multiannual = .FieldToClass("sInd_Multiannual")
                    sIndqsamevalue = .FieldToClass("sIndqsamevalue")
                    sInd_IFI = .FieldToClass("sInd_IFI")
                    nExtraDay = .FieldToClass("nExtraDay")
                    nPolicy_transfer = .FieldToClass("nPolicy_transfer")
                    nCertif_transfer = .FieldToClass("nCertif_transfer")
                    sCodbranch_Transfer = .FieldToClass("sCodbranch_Transfer")
                    Me.sCertype = sCertype
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nPolicy = nPolicy
                    Me.nCertif = nCertif
                    .RCloseRec()
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
        'UPGRADE_NOTE: Object lrecreaCertificat_branch may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_branch = Nothing
    End Function

    '% Update: función que actualiza la tabla Certificat mediante el stored procedure insCertificat
    '%        el cual es capaz de definir si  es una inserción o una actualización.....
    Public Function Update() As Boolean
        Dim lrecinsCertificat As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecinsCertificat = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insCertificat'
        '+ Información leída el 06/12/1999 04:53:36 PM
        With lrecinsCertificat
            .StoredProcedure = "insCertificat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCumul_code", sCumul_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDat_no_con", dDat_no_con, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_origi", dDate_Origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dchangdat", dChangdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dMaximum_da", dMaximum_da, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_benef", nNote_benef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_drisk", nNote_drisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPropodat", dPropodat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRenewal", sRenewal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProrShort", sProrShort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusva", sStatusva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUser_amend", nUser_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSus_branch", nSus_branch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSus_policy", nSus_policy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSus_certif", nSus_certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaysFQ", nDaysFQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaysSQ", nDaysSQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReinsura", sReinsura, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExemption", sExemption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirind", sDirind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSendAddr", nSendAddr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAut_guarval", sAut_guarval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFer", dFer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDays_quot", nDays_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatQuota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPol_Quot", nPol_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBill_Ind", sBill_Ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nImageNum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRenewalnum", nRenewalnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProp_reg", nProp_reg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_day", nPay_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAFP_Commiss", nAFP_Commiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAFP_Comm_Curr", nAFP_Comm_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFracReceip", sFracReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_Agree", nGroup_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRetarif", sRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLast_tarif", dLast_tarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCertificat = Nothing
    End Function

    '% updDatPart : Procedimiento que realiza las actualizaciones respectivas de la tabla de datos particulares respectiva
    Public Function UpdateDatPart() As Boolean
        Dim lrecinsDatPart As eRemoteDB.Execute

        On Error GoTo UpdateDatPart_Err

        lrecinsDatPart = New eRemoteDB.Execute

        With lrecinsDatPart
            .StoredProcedure = "insDatPart"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateDatPart = .Run(False)
        End With

UpdateDatPart_Err:
        If Err.Number Then
            UpdateDatPart = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsDatPart may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsDatPart = Nothing
    End Function


    '% Find_CertifCount: verifica cantidad máxima de emisión de certificados según lo definido
    '                    en la póliza matriz
    Public Function Find_CertifCount(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer) As Boolean
        Dim lrecreaCertifCount As eRemoteDB.Execute
        Dim lintCount As Short

        lrecreaCertifCount = New eRemoteDB.Execute

        On Error GoTo Find_CertifCount_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaCertifCount'
        '+ Información leída el 04/11/2000 02:19:00 p.m.

        With lrecreaCertifCount
            .StoredProcedure = "reaCertifCount"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", lintCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Find_CertifCount = True
                nCertifNum = .Parameters("nCount").Value
                .RCloseRec()
            Else
                Find_CertifCount = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaCertifCount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertifCount = Nothing

Find_CertifCount_Err:
        If Err.Number Then
            Find_CertifCount = False
        End If
        On Error GoTo 0
    End Function

    '% Find_MaxCertif: busca el número de certificado más alto de la póliza
    Public Function Find_MaxCertif(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal llngPolicy As Integer) As Boolean
        Dim lrecreaCertificat_certif As eRemoteDB.Execute

        lrecreaCertificat_certif = New eRemoteDB.Execute

        On Error GoTo Find_MaxCertif_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaCertificat_certif'
        '+ Información leída el 06/11/2000 08:56:32 a.m.

        With lrecreaCertificat_certif
            .StoredProcedure = "reaCertificat_certif"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_MaxCertif = True
                nCertifNum = .FieldToClass("Last_cert")
                .RCloseRec()
            Else
                Find_MaxCertif = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaCertificat_certif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_certif = Nothing

Find_MaxCertif_Err:
        If Err.Number Then
            Find_MaxCertif = False
        End If
        On Error GoTo 0
    End Function

    '% Update_UserAmend: Actualiza el usuario que está modificando el certificado
    Public Function Update_UserAmend() As Boolean
        Dim lrecupdCertificatUserAmend As eRemoteDB.Execute

        lrecupdCertificatUserAmend = New eRemoteDB.Execute

        On Error GoTo Update_UserAmend_Err

        '+ Definición de parámetros para stored procedure 'insudb.updCertificatUserAmend'
        '+ Información leída el 06/11/2000 09:16:51 a.m.

        With lrecupdCertificatUserAmend
            .StoredProcedure = "updCertificatUserAmend"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_UserAmend = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdCertificatUserAmend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCertificatUserAmend = Nothing

Update_UserAmend_Err:
        If Err.Number Then
            Update_UserAmend = False
        End If
        On Error GoTo 0
    End Function

    '% VerifyDeclaFreq: verifica los datos para la declaración
    Public Function VerifyDeclaFreq(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dStartdate As Date, ByVal dExpirdat As Date) As Boolean
        Dim lrecinsVerifyDeclaFreq As eRemoteDB.Execute

        On Error GoTo VerifyDeclaFreq_Err

        lrecinsVerifyDeclaFreq = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insVerifyDeclaFreq'
        '+ Información leída el 13/11/2000 06:01:22 p.m.

        With lrecinsVerifyDeclaFreq
            .StoredProcedure = "ReaVerifyDeclaFreq"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                VerifyDeclaFreq = True
                nFlag = .FieldToClass("nFlag")
                dDeclaDatN = .FieldToClass("dDeclaDatN")
                nCertifExist = .FieldToClass("nCertifExist")
                .RCloseRec()
            Else
                VerifyDeclaFreq = False
            End If
        End With

VerifyDeclaFreq_Err:
        If Err.Number Then
            VerifyDeclaFreq = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecinsVerifyDeclaFreq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsVerifyDeclaFreq = Nothing

    End Function

    '% Find_LifeReduction: verifica los datos para la reducción de capital
    Public Function Find_LifeReduction(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Date, ByVal lstrTabName As String) As String
        Dim lrecreaLife_reduction As eRemoteDB.Execute

        lrecreaLife_reduction = New eRemoteDB.Execute

        On Error GoTo Find_LifeReduction_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaLife_reduction'
        '+ Información leída el 14/11/2000 11:50:26 a.m.

        With lrecreaLife_reduction
            .StoredProcedure = "reaLife_reductionpkg.reaLife_reduction"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTabname", lstrTabName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_LifeReduction = .FieldToClass("sSald_prog", String.Empty)
                .RCloseRec()
            Else
                Find_LifeReduction = String.Empty
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaLife_reduction may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaLife_reduction = Nothing

Find_LifeReduction_Err:
        If Err.Number Then
            Find_LifeReduction = "(Find_LifeReduction): " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% FindParticularData: Busca la informacion de un archivo de datos particulares
    Public Function Find_ParticularData(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Date) As Boolean
        Dim lrecreaParticularData As eRemoteDB.Execute

        lrecreaParticularData = New eRemoteDB.Execute

        On Error GoTo Find_ParticularData_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaParticularData'
        '+ Información leída el 17/11/2000 09:27:37 a.m.

        With lrecreaParticularData
            .StoredProcedure = "reaParticularData"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sTabname = .FieldToClass("sTabname")
                dNulldate = .FieldToClass("dNulldate")
                dEffecdate = .FieldToClass("dEffecdate")
                .RCloseRec()
                Find_ParticularData = True
            Else
                Find_ParticularData = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaParticularData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaParticularData = Nothing

Find_ParticularData_Err:
        If Err.Number Then
            Find_ParticularData = False
        End If
        On Error GoTo 0
    End Function

    '% FindParticularData: Busca la informacion de un archivo de datos particulares
    Public Function FindParticularDataLI(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Date) As Boolean
        Dim lrecreaParticularData As eRemoteDB.Execute

        lrecreaParticularData = New eRemoteDB.Execute

        On Error GoTo FindParticularDataLI_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaParticularData'
        '+ Información leída el 17/11/2000 09:27:37 a.m.

        With lrecreaParticularData
            .StoredProcedure = "reaCertificNN"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nCapital_ca = .FieldToClass("nCapital_ca")
                nPremium_ca = .FieldToClass("nPremium_ca")
                .RCloseRec()
                FindParticularDataLI = True
            Else
                FindParticularDataLI = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaParticularData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaParticularData = Nothing

FindParticularDataLI_Err:
        If Err.Number Then
            FindParticularDataLI = False
        End If
        On Error GoTo 0
    End Function

    '% Add_ParticularData: se genera un registro en la tabla de datos particulares que
    '%                     corresponda según el ramo
    Public Function Add_ParticularData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sTabname As String) As Boolean
        Dim lreccreParticularData As eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creParticularData'
        '+ Información leída el 17/11/2000 11:18:49 a.m.
        On Error GoTo Add_ParticularData_Err
        lreccreParticularData = New eRemoteDB.Execute
        With lreccreParticularData
            .StoredProcedure = "creParticularData"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add_ParticularData = .Run(False)
        End With

Add_ParticularData_Err:
        If Err.Number Then
            Add_ParticularData = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreccreParticularData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreParticularData = Nothing
    End Function

    '% AddUpdParticularData:
    Public Function AddUpdParticularData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dStartdate As Date, ByVal dNulldate As Date, ByVal dTempUpdate As Date, ByVal nUsercode As Integer, ByVal sTabname As String, ByVal nProctype As Integer) As Boolean
        Dim lrecinsParticularData As eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insParticularData'
        '+ Información leída el 17/11/2000 11:46:27 a.m.
        On Error GoTo AddUpdParticularData_Err
        lrecinsParticularData = New eRemoteDB.Execute
        With lrecinsParticularData
            .StoredProcedure = "insParticularData"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dNulldate", IIf(dNulldate = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dNulldate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dTempUpdate", IIf(dTempUpdate = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dTempUpdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            AddUpdParticularData = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsParticularData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsParticularData = Nothing

AddUpdParticularData_Err:
        If Err.Number Then
            AddUpdParticularData = False
        End If
        On Error GoTo 0
    End Function

    '% CalProvinceCode: Permite calcular el codigo de la provincia que se va a asociar
    '% a los intermediarios
    Public Function CalProvinceCode(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal lstrClient As String, ByVal lintRunTyp As Integer, ByVal ldtmEffecdate As Date, ByVal lintProvince As Integer, ByVal lstrAddres As String) As Object
        Dim lrecinsCalProvince As eRemoteDB.Execute

        On Error GoTo CalProvinceCode_Err

        lrecinsCalProvince = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.insCalProvince'
        '+Información leída el 21/11/2000 09:48:46

        With lrecinsCalProvince
            .StoredProcedure = "insCalProvince"
            .Parameters.Add("psCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("psClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnRunTyp", lintRunTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnProvince", lintProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("pnAddres", lstrAddres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                nProvince = .Parameters.Item("pnProvince").Value
                CalProvinceCode = True
            Else
                CalProvinceCode = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecinsCalProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalProvince = Nothing

CalProvinceCode_Err:
        If Err.Number Then
            CalProvinceCode = False
        End If
        On Error GoTo 0
    End Function

    '% insCertificat_Ca047: Esta rutina se encarga de realizar la actualización en la
    '% tabla 'certificat'
    Public Function insCertificat_Ca047(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal pdtmMaximum_da As Date) As Boolean
        Dim lclsCertificat As eRemoteDB.Execute

        lclsCertificat = New eRemoteDB.Execute

        On Error GoTo insCertificat_Ca047_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Then

            Me.sCertype = sCertype
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nPolicy = nPolicy

            With lclsCertificat
                If Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True) Then
                    Me.dMaximum_da = pdtmMaximum_da
                    insCertificat_Ca047 = Me.Update
                End If
            End With
        Else
            insCertificat_Ca047 = True
        End If

        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing

insCertificat_Ca047_Err:
        If Err.Number Then
            insCertificat_Ca047 = False
        End If
        On Error GoTo 0
    End Function

    '% FindCertificatToNull: Devuelve información de un registro de la tabla Certificat para ser anulado
    Public Function FindCertificatToNull(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSituation As Integer) As Boolean
        '- Se define la variable lrecreaCertificatCA033
        Dim lrecreaCertificatCA033 As eRemoteDB.Execute
        lrecreaCertificatCA033 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaCertificatCA033'
        '+ Información leída el 03/01/2001 15:56:19

        On Error GoTo FindCertificatToNull_Err

        With lrecreaCertificatCA033
            .StoredProcedure = "reaCertificatCA033"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                sCertype = .FieldToClass("sCertype")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nPolicy = .FieldToClass("nPolicy")
                nCertif = .FieldToClass("nCertif")
                nCapital = .FieldToClass("nCapital")
                sCumul_code = .FieldToClass("sCumul_code")
                dDat_no_con = .FieldToClass("dDat_no_con")
                dDate_Origi = .FieldToClass("dDate_origi")
                dChangdat = .FieldToClass("dChangdat")
                dExpirdat = .FieldToClass("dExpirdat")
                nGroup = .FieldToClass("nGroup")
                dIssuedat = .FieldToClass("dIssuedat")
                dMaximum_da = .FieldToClass("dMaximum_da")
                nNo_convers = .FieldToClass("nNo_convers")
                nNote_benef = .FieldToClass("nNote_benef")
                nNote_drisk = .FieldToClass("nNote_drisk")
                nNullcode = .FieldToClass("nNullcode")
                dNulldate = .FieldToClass("dNulldate")
                nPayfreq = .FieldToClass("nPayfreq")
                nPremium = .FieldToClass("nPremium")
                dPropodat = .FieldToClass("dPropodat")
                sRenewal = .FieldToClass("sRenewal")
                nSituation = .FieldToClass("nSituation")
                dStartdate = .FieldToClass("dStartdate")
                sStatusva = .FieldToClass("sStatusva")
                nUser_amend = .FieldToClass("nUser_amend")
                nUsercode = .FieldToClass("nUsercode")
                nWait_code = .FieldToClass("nWait_code")
                nSus_branch = .FieldToClass("nSus_branch")
                nSus_product = .FieldToClass("nSus_product")
                nSus_policy = .FieldToClass("nSus_policy")
                nSus_certif = .FieldToClass("nSus_certif")
                sClient = .FieldToClass("sClient")
                nProponum = .FieldToClass("nPropoNum")
                dNextReceip = .FieldToClass("dNextReceip")
                nQuota = .FieldToClass("nQuota")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                sNumForm = .FieldToClass("sNumForm")
                sProrShort = .FieldToClass("sProrShort")
                sReinsura = .FieldToClass("sReinsura")
                sClaimind = .FieldToClass("sClaimind")
                sException = .FieldToClass("sException")
                nExcCause = .FieldToClass("nExcCause")
                sProperUse = .FieldToClass("sProperUse")
                nImageNum = .FieldToClass("nImageNum")
                sExemption = .FieldToClass("sExemption")

                .RCloseRec()
                FindCertificatToNull = True
            Else
                FindCertificatToNull = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaCertificatCA033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificatCA033 = Nothing

FindCertificatToNull_Err:
        If Err.Number Then
            FindCertificatToNull = False
        End If
        On Error GoTo 0
    End Function

    '% updPolicyCA888: Función que retorna VERDADERO al actualizar un registro en la tabla 'Certificat'
    Public Function updPolicyCA888() As Boolean
        Dim lrecupdPolicyCA888 As eRemoteDB.Execute

        On Error GoTo updPolicyCA888_Err

        lrecupdPolicyCA888 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updPolicyCA888'
        '+ Información leída el 15/01/2001 8:46:03 a.m.

        With lrecupdPolicyCA888
            .StoredProcedure = "updPolicyCA888"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            updPolicyCA888 = .Run(False)
        End With

updPolicyCA888_Err:
        If Err.Number Then
            updPolicyCA888 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdPolicyCA888 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicyCA888 = Nothing
    End Function

    '% ReverseRenModPol: Funcion que reversa una modificación incompleta
    Public Function ReverseRenModPol(Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nAnulReceipt As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nNullOutMov As Integer = 0, Optional ByVal nAnulPropQuot As Integer = 0, Optional ByVal sOrigen As String = "3", Optional ByVal nCancel As Integer = 0) As Boolean
        Dim lrecinsReverRenModPol As eRemoteDB.Execute

        On Error GoTo ReverseRenModPol_Err
        lrecinsReverRenModPol = New eRemoteDB.Execute
        '+ Definición de parámetros para stored procedure 'insudb.insReverRenModPol'
        '+ Información leída el 15/01/2001 8:55:04 a.m.
        With lrecinsReverRenModPol
            .StoredProcedure = "insReverRenModPol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAnulReceipt", nAnulReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullOutMov", nNullOutMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAnulPropQuot", nAnulPropQuot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigen", sOrigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCancel", nCancel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ReverseRenModPol = .Run(False)
        End With

ReverseRenModPol_Err:
        If Err.Number Then
            ReverseRenModPol = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsReverRenModPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReverRenModPol = Nothing
    End Function

    '% insReverRenModPol: Funcion que reversa una modificación incompleta
    Public Function insReverRenModPol(Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nAnulReceipt As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nNullOutMov As Integer = 0, Optional ByVal nAnulPropQuot As Integer = 0, Optional ByVal nCancel As Integer = 0) As Boolean

        '+Si no vienen datos por parametros se cargan los del objeto
        On Error GoTo insReverRenModPol_Err
        If sCertype = String.Empty Then
            sCertype = Me.sCertype
            nBranch = Me.nBranch
            nProduct = Me.nProduct
            nPolicy = Me.nPolicy
            nCertif = Me.nCertif
            nAnulReceipt = Me.nAnulReceipt
            nUsercode = Me.nUsercode
            nNullOutMov = Me.nNullOutMov
            nAnulPropQuot = Me.nAnulPropQuot
        End If

        '+Como sólo se reversa la modificacion, se deja sOrigin = "3"
        insReverRenModPol = ReverseRenModPol(sCertype, nBranch, nProduct, nPolicy, nCertif, nAnulReceipt, nUsercode, nNullOutMov, nAnulPropQuot, "3", nCancel)
insReverRenModPol_Err:
        If Err.Number Then
            insReverRenModPol = False
        End If
        On Error GoTo 0
    End Function

    '% valExistSalvage: Esta rutina permite verificar si la póliza/certificado tiene valor de rescate.
    Public Function valExistSalvage(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dblSalvage As Double, ByVal strIndicator As String) As Double

        '- Se define la variable lrecinsCalculateSalvage
        Dim lrecinsCalculateSalvage As eRemoteDB.Execute
        lrecinsCalculateSalvage = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insCalculateSalvage'
        '+ Información leída el 15/01/2001 10:54:07

        On Error GoTo valExistSalvage_Err

        With lrecinsCalculateSalvage
            .StoredProcedure = "insCalculateSalvage"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSalvage", dblSalvage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndicator", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                valExistSalvage = .Parameters.Item("nSalvage").Value
                .RCloseRec()
            Else
                valExistSalvage = 0
            End If
        End With
        'UPGRADE_NOTE: Object lrecinsCalculateSalvage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalculateSalvage = Nothing

valExistSalvage_Err:
        If Err.Number Then
            valExistSalvage = False
        End If
        On Error GoTo 0
    End Function

    '% insGetSurrenAmount: Obtiene el valor de rescate de la poliza dependiendo del tipo
    '% de producto. Si la moneda es distinta de local, el monto se convierte a local
    '% Se pasa tipo de producto y fecha de vigencia para no tener que recuperarlo si
    '% transacción ya lo hizo previamente
    Public Function insGetSurrenAmount(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, Optional ByVal nCurrency As Integer = 1, Optional ByVal nProdClas As Integer = 0, Optional ByVal dStartdate As Date = #12:00:00 AM#, Optional ByVal sRousurre As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal sSurrType As String = "", Optional ByVal sProcessType As String = "") As Double

        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As Policy
        Dim lclsGuarant_val As Guarant_val
        Dim lclsExchange As eGeneral.Exchange
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim ldblSurrenAmount As Double
        Dim lintyear As Integer
        Dim lintMonth As Integer
        Dim lvarExchange As Object = New Object

        On Error GoTo insGetSurrenAmountErr

        '+ Se busca el producto para ver su clase
        If nProdClas <= 0 Then
            lclsProduct = New eProduct.Product
            Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
            nProdClas = lclsProduct.nProdClas
            sRousurre = lclsProduct.sRousurre
        End If

        If nProdClas = 7 Then ' VidActiva

            mclsValPolicyTra = New ValPolicyTra

            If sSurrType <> "" And sProcessType <> "" Then
                Call mclsValPolicyTra.InsPreVI009(sSurrType, sProcessType, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, eRemoteDB.Constants.intNull, 4, eRemoteDB.Constants.intNull, sCodispl)
            Else
                Call mclsValPolicyTra.InsPreVI009("2", "1", sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, eRemoteDB.Constants.intNull, 4, eRemoteDB.Constants.intNull, sCodispl)
            End If

            ldblSurrenAmount = mclsValPolicyTra.DefaultValueVI009("tcnRescDef")

        Else
            If nProdClas = 3 Or nProdClas = 4 Then
                mclsValPolicyTra = New ValPolicyTra

                Call mclsValPolicyTra.InsPreVI7000(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency, eRemoteDB.Constants.intNull, 1, "2", sCodispl, eRemoteDB.Constants.intNull)

                ldblSurrenAmount = mclsValPolicyTra.DefaultValueVI7000("tcnSurrVal")

            Else
                '+ Año-meses de vigencia
                '+ Si no viene fecha se inicio de poliza, se calcula
                If dStartdate = eRemoteDB.Constants.dtmNull Then
                    lclsPolicy = New Policy
                    With lclsPolicy
                        If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                            dStartdate = .dStartdate
                        End If
                    End With
                End If

                lclsGeneral = New eGeneral.GeneralFunction
                lclsGeneral.getYearMonthDiff(dStartdate, dEffecdate, lintyear, lintMonth)
                lclsGuarant_val = New Guarant_val
                With lclsGuarant_val
                    If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.intNull, lintyear, lintMonth) Then
                        ldblSurrenAmount = .nResc_val
                    End If
                End With

                If ldblSurrenAmount = eRemoteDB.Constants.intNull Or ldblSurrenAmount = 0 Then
                    mclsValPolicyTra = New ValPolicyTra

                    Call mclsValPolicyTra.InsPreVI009(sSurrType, sProcessType, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, sCodispl)

                    ldblSurrenAmount = mclsValPolicyTra.DefaultValueVI009("tcnSurrVal")

                End If
            End If
        End If

        '+ Si ya está en moneda local no se hace conversion
        If nCurrency <> 1 And nCurrency <> eRemoteDB.Constants.intNull Then
            lclsExchange = New eGeneral.Exchange
            Call lclsExchange.Convert(lvarExchange, ldblSurrenAmount, nCurrency, 1, dEffecdate, 0)
            ldblSurrenAmount = ldblSurrenAmount * lclsExchange.pdblExchange
        End If

        insGetSurrenAmount = ldblSurrenAmount

insGetSurrenAmountErr:
        If Err.Number Then
            insGetSurrenAmount = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsGuarant_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGuarant_val = Nothing
        'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsExchange = Nothing
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGeneral = Nothing
    End Function

    '% UpdateClientCertificat: Actualiza el cliente  de un certificado
    Public Function UpdateClientCertificat(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nUsercode As Integer) As Boolean
        '- Se define la variable lrecupdCertificat_Client
        Dim lrecupdCertificat_Client As eRemoteDB.Execute

        On Error GoTo UCCertErr

        lrecupdCertificat_Client = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updCertificat_Client'
        '+ Información leída el 03/11/2000 03:34:38 PM

        With lrecupdCertificat_Client
            .StoredProcedure = "updCertificat_Client"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateClientCertificat = .Run(False)
        End With

UCCertErr:
        If Err.Number Then
            UpdateClientCertificat = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdCertificat_Client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCertificat_Client = Nothing
    End Function

    '% UpdateClientParticular: Actualiza el código de cliente en la tabla de datos particulare sque se esté manejando en el momento
    Public Function UpdateClientParticular(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nUsercode As Integer) As Boolean

        Dim lrecupdClient As eRemoteDB.Execute

        On Error GoTo UpdateClientParticular_Err

        lrecupdClient = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.updClient'
        'Información leída el 14/11/2000 08:47:28

        With lrecupdClient
            .StoredProcedure = "updClient"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateClientParticular = .Run(False)

        End With

UpdateClientParticular_Err:
        If Err.Number Then
            UpdateClientParticular = False
        End If
        'UPGRADE_NOTE: Object lrecupdClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdClient = Nothing
    End Function

    '% Update_RenDate: Permite realizar el cambio de la fecha de renovación de una póliza/certificado
    Public Function Update_RenDate() As Boolean
        Dim lrecinsRenDateChange As eRemoteDB.Execute

        On Error GoTo Update_RenDate_Err

        lrecinsRenDateChange = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insRenDateChange'
        'Información leída el 19/01/2001 08:42:12

        With lrecinsRenDateChange
            .StoredProcedure = "insRenDateChange"
            .Parameters.Add("sPoliType", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_RenDate = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecinsRenDateChange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRenDateChange = Nothing

Update_RenDate_Err:
        If Err.Number Then
            Update_RenDate = False
        End If
        On Error GoTo 0

    End Function

    '% insCertificat_Ca047: Esta rutina se encarga de realizar la actualización en la
    '% tabla 'certificat'
    Public Function insReaCA037(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsReaCA037 As eRemoteDB.Execute

        lrecinsReaCA037 = New eRemoteDB.Execute

        On Error GoTo insReaCA037_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Then

            Me.sCertype = sCertype
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nPolicy = nPolicy

            '+Definición de parámetros para stored procedure 'insudb.insReaCA037'
            '+Información leída el 22/01/2001 8:31:39
            With lrecinsReaCA037
                .StoredProcedure = "insReaCA037"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    dNulldate = .FieldToClass("dNulldate")
                    dDate_Origi = .FieldToClass("dDate_origi")
                    dStartdate = .FieldToClass("dStartdate")
                    dExpirdat = .FieldToClass("dExpirdat")
                    sColtimre = .FieldToClass("sColtimre")
                    dNextReceip = .FieldToClass("dNextReceip")
                    sBrancht = .FieldToClass("sBrancht")
                    nSuspCount = .FieldToClass("nSuspCount")
                    nDeclaredClaims = .FieldToClass("nDeclaredClaims")
                    If nCertif > 0 Then
                        sStatusva = .FieldToClass("sStatusva")
                        nNullcode = .FieldToClass("nNullcode")
                    End If
                    insReaCA037 = True
                    .RCloseRec()
                Else
                    insReaCA037 = False
                End If
            End With

            'UPGRADE_NOTE: Object lrecinsReaCA037 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecinsReaCA037 = Nothing

        End If

insReaCA037_Err:
        If Err.Number Then
            insReaCA037 = False
        End If
        On Error GoTo 0
    End Function

    '% Find_CA038: Función que carga la información de una póliza
    '%            validando que la misma no se encuentre suspendida
    Public Function Find_CA038(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsReaCA038 As eRemoteDB.Execute

        lrecinsReaCA038 = New eRemoteDB.Execute

        On Error GoTo Find_CA038_Err

        'Definición de parámetros para stored procedure 'insudb.insReaCA038'
        'Información leída el 17/01/2001 15:32:23

        With lrecinsReaCA038
            .StoredProcedure = "insReaCA038"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                dNulldate = .FieldToClass("dNulldate")
                dStartdate = .FieldToClass("dStartdate")
                dExpirdat = .FieldToClass("dExpirdat")
                sColtimre = .FieldToClass("sColtimre")
                nProduct = .FieldToClass("nProduct")
                nSuspCount = .FieldToClass("nSuspCount")
                nTransactio = .FieldToClass("nTransactio")
                sPolitype = .FieldToClass("sPolitype")
                sClient = .FieldToClass("sClient")
                dNextReceip = .FieldToClass("dNextReceip")
                sColinvot = .FieldToClass("sColinvot")
                sProrShort = .FieldToClass("sProrShort")
                nPayfreq = .FieldToClass("nPayfreq")
                nGroup = .FieldToClass("nGroup")
                sDirdebit = .FieldToClass("sDirdebit")
                nOffice = .FieldToClass("nOffice")
                nIntermed = .FieldToClass("nIntermed")
                nDaysFQ = .FieldToClass("nDaysFQ")
                nDaysSQ = .FieldToClass("nDaysSQ")
                nQuota = .FieldToClass("nQuota")
                nParticip = .FieldToClass("nParticip")
                If nCertif <> 0 Then
                    nDeclaredClaims = .FieldToClass("nDeclaredClaims")
                End If
                Find_CA038 = True
                .RCloseRec()
            Else
                Find_CA038 = False
            End If
        End With

Find_CA038_Err:
        If Err.Number Then
            Find_CA038 = False
        End If
        'UPGRADE_NOTE: Object lrecinsReaCA038 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaCA038 = Nothing
        On Error GoTo 0
    End Function

    '% insCertificat_Ca037: Esta rutina se encarga de realizar la actualización en la
    '% tabla 'certificat'
    Public Function reaCertificat_branch_Ca037(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean

        Dim lclsCertificat As eRemoteDB.Execute

        lclsCertificat = New eRemoteDB.Execute

        On Error GoTo reaCertificat_branch_Ca037_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Then

            Me.sCertype = sCertype
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nPolicy = nPolicy

            With lclsCertificat
                reaCertificat_branch_Ca037 = Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)
            End With

        Else
            reaCertificat_branch_Ca037 = True
        End If


        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing


reaCertificat_branch_Ca037_Err:
        If Err.Number Then
            reaCertificat_branch_Ca037 = False
        End If
        On Error GoTo 0
    End Function

    '% insEffecDateChange: Cambia la fecha de efecto de poliza, certificado y datos particulares
    Public Function insEffecDateChange(ByVal sPolitype As String, ByVal sColtimre As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal dNextReceip As Date, ByVal nUsercode As Integer, Optional ByVal sReceiptType As String = "") As Boolean
        Dim lrecinsEffecdatechange As eRemoteDB.Execute
        On Error GoTo insEffecdatechange_Err

        lrecinsEffecdatechange = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insEffecdatechange al 08-06-2002 15:34:31
        '+
        With lrecinsEffecdatechange
            .StoredProcedure = "insEffecdatechange"
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextreceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceiptType", sReceiptType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insEffecDateChange = .Run(False)
        End With

insEffecdatechange_Err:
        If Err.Number Then
            insEffecDateChange = False
        End If
        'UPGRADE_NOTE: Object lrecinsEffecdatechange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsEffecdatechange = Nothing
        On Error GoTo 0
    End Function

    '% insUpdNullCertificat: Esta función se encarga de anular el certificado en caso de que sea una póliza colectiva.
    Public Function insUpdNullCertificat() As Boolean
        Dim lrecupdCertificat_null As eRemoteDB.Execute

        lrecupdCertificat_null = New eRemoteDB.Execute

        On Error GoTo insUpdNullCertificat_Err

        '+Definición de parámetros para stored procedure 'insudb.updCertificat_null'
        '+Información leída el 21/01/2000 10:54:19

        insUpdNullCertificat = True

        With lrecupdCertificat_null
            .StoredProcedure = "updCertificat_null"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusva", sStatusva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHistor", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdNullCertificat = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdCertificat_null may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCertificat_null = Nothing

insUpdNullCertificat_Err:
        If Err.Number Then
            insUpdNullCertificat = False
        End If
        On Error GoTo 0
    End Function

    '%Class_Initialize: Inicializa las variables del objeto
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        ClearFields()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% insCalQuotesDate:Calcula la fecha d la cuota
    Private Function insCalQuotesDate(ByVal lclsCertificat As ePolicy.Certificat, ByVal dEffecdate As Date) As Date
        Dim dNextReceip As Date
        Dim dStartdate As Date
        Dim nNumQuotes As Integer

        dNextReceip = IIf(Not lclsCertificat.dNextReceip = eRemoteDB.Constants.dtmNull, lclsCertificat.dNextReceip, dEffecdate)
        dStartdate = lclsCertificat.dStartdate

        If lclsCertificat.nPayfreq = 7 And Not (lclsCertificat.dNextReceip = eRemoteDB.Constants.dtmNull) Then
            nNumQuotes = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dStartdate, dNextReceip)
            If nNumQuotes < lclsCertificat.nQuota Then
                insCalQuotesDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, lclsCertificat.nDaysFQ, dStartdate)
                insCalQuotesDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, (nNumQuotes - 1) * lclsCertificat.nDaysSQ, insCalQuotesDate)
            Else
                insCalQuotesDate = dNextReceip
            End If
        Else
            insCalQuotesDate = dNextReceip
        End If

    End Function

    '% ValfyPrem_first: verifica indicador de primera primea obligatoria
    Public Function ValPrem_first(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal nProponum As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsWay_pay_prod As eProduct.Way_pay_prod
        Dim lclsMove_Acc As eCashBank.Move_Acc

        '+ Buscar indicador de primera prima obligatoria para Ramo/Producto/Via de Pago
        On Error GoTo ValPrem_first_Err
        lclsWay_pay_prod = New eProduct.Way_pay_prod
        If lclsWay_pay_prod.Find(nBranch, nProduct, nWay_pay, dEffecdate) Then
            If lclsWay_pay_prod.sPrem_first = "1" Then
                lclsMove_Acc = New eCashBank.Move_Acc
                If lclsMove_Acc.FindMove_Prem_first(sClient, nBranch, nProduct, nProponum) Then
                    ValPrem_first = True
                Else
                    ValPrem_first = False
                End If
            End If
        End If

ValPrem_first_Err:
        If Err.Number Then
            ValPrem_first = False
        End If
        'UPGRADE_NOTE: Object lclsWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsWay_pay_prod = Nothing
        'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsMove_Acc = Nothing
        On Error GoTo 0
    End Function

    '% insValCA050: Validación de pago de primera prima en fin de emisión
    Public Function insValCA050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsMove_Acc As eCashBank.Move_Acc
        Dim lrecinsValway_pay_prod As eRemoteDB.Execute
        Dim sPrem_first As String = ""
        Dim nProponum As Double

        lclsMove_Acc = New eCashBank.Move_Acc
        lrecinsValway_pay_prod = New eRemoteDB.Execute

        On Error GoTo insValway_pay_prod_Err

        '+ Definición de store procedure insValway_pay_prod al 01-23-2002 15:46:38
        With lrecinsValway_pay_prod
            .StoredProcedure = "insValway_pay_prod"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrem_first", sPrem_first, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then

                '+ Si se exige el pago adelantado de la primera prima
                If Trim(.Parameters("sPrem_first").Value) = "1" Then

                    '+ Si para la póliza/propuesta no existen movimientos de Cuentas Corrientes
                    If .Parameters("nProponum").Value <> eRemoteDB.Constants.intNull Then
                        If Not lclsMove_Acc.Find_nProponum(.Parameters("nProponum").Value) Then
                            insValCA050 = True
                        Else
                            insValCA050 = False
                        End If
                    Else
                        insValCA050 = False
                    End If
                Else
                    insValCA050 = False
                End If
            Else
                insValCA050 = False
            End If
        End With

insValway_pay_prod_Err:
        If Err.Number Then
            insValCA050 = False
        End If

        'UPGRADE_NOTE: Object lrecinsValway_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValway_pay_prod = Nothing
        'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsMove_Acc = Nothing
        On Error GoTo 0

    End Function

    '% Find_CertificatVI008: Esta rutina permite verificar si a la póliza/certificado ya
    '% se le ha hecho una reducción previa.
    Private Function Find_CertificatVI008(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaCertificNNVI008 As eRemoteDB.Execute

        On Error GoTo Find_CertificatVI008_Err

        lrecreaCertificNNVI008 = New eRemoteDB.Execute

        '** Parameters definition to stored procedure 'insudb.reaCertificNNVI008'
        'Definición de parámetros para stored procedure 'insudb.reaCertificNNVI008'
        '** Data read on 20/03/2000 09:38:45
        'Información leída el 20/03/2000 09:38:45

        With lrecreaCertificNNVI008
            .StoredProcedure = "reaCertificNNVI008"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                If .FieldToClass("Saldado") = "Y" Then
                    Find_CertificatVI008 = True
                Else
                    Find_CertificatVI008 = False
                End If
                .RCloseRec()
            End If
        End With

Find_CertificatVI008_Err:
        If Err.Number Then Find_CertificatVI008 = False
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertificNNVI008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificNNVI008 = Nothing
    End Function

    '%insUpdVI008: Proceso final
    Private Function insUpdVI008(ByVal sIndExecute As String, ByVal sIndReduction As String, ByVal sIndNulling As String, ByVal sKey As String) As Boolean
        '-Objetos para validacion
        Dim lrecinsUpdVI008 As eRemoteDB.Execute
        Dim lclsProduct As eProduct.Product
        Dim lclsGuarant_val As ePolicy.Guarant_val
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsRoles As ePolicy.Roles
        Dim lclsPolCert As Object
        '-Variables asociadas propiedades de objetos
        Dim lintProdClas As Integer
        Dim lintInsuAge As Integer
        Dim lintyear As Integer
        Dim lintMonth As Integer
        Dim ldblPremium As Double
        Dim ldblCapital As Double
        Dim lintDeferred As Integer
        Dim ldtmExpirdat As Date
        Dim ldtmStartdate As Date
        Dim ldblRescVal As Double

        On Error GoTo insUpdVI008_Err

        insUpdVI008 = False
        lintDeferred = eRemoteDB.Constants.intNull

        '+ Se realizan los calculos para obtener
        '+ los valores a registrar

        '+ Se recupera fecha de inicio de vigencia de la poliza/certificado
        If nCertif = 0 Then
            lclsPolCert = New Policy
            Call lclsPolCert.Find(sCertype, nBranch, nProduct, nPolicy)
        Else
            lclsPolCert = New Certificat
            Call lclsPolCert.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
        End If
        ldtmStartdate = lclsPolCert.dStartdate
        'UPGRADE_NOTE: Object lclsPolCert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolCert = Nothing

        '+ Se obtiene la clase de producto
        lclsProduct = New eProduct.Product
        If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
            lintProdClas = lclsProduct.nProdClas
        End If
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing

        '+ Producto de clase 'Vida Tradicional'
        If lintProdClas = 1 Then

            '+ Se busca edad actuarial del segurado principal de la poliza
            lclsRoles = New Roles
            With lclsRoles
                If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, Roles.eRoles.eRolInsured, String.Empty, dEffecdate) Then
                    Call .CalInsuAge(nBranch, nProduct, dEffecdate, .dBirthdate, .sSexclien, .sSmoking)
                    lintInsuAge = .nAge(True)
                End If
            End With
            'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsRoles = Nothing

            '+ Se calcula la cantidad de años/meses de la poliza
            lclsGeneral = New eGeneral.GeneralFunction
            Call lclsGeneral.getYearMonthDiff(ldtmStartdate, dEffecdate, lintyear, lintMonth)
            'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsGeneral = Nothing

            '+ Se obtiene valores garantizados asociados a poliza
            lclsGuarant_val = New Guarant_val
            With lclsGuarant_val
                Call .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, System.DateTime.FromOADate(lintInsuAge), lintyear, lintMonth, dEffecdate.ToOADate)

                '+ Se obtiene valores generados por reduccion
                '+ Saldado
                If sIndReduction = "1" Then
                    ldblCapital = .nSaldvalkm
                    ldblPremium = .nSald_val
                    ldblRescVal = .nResc_val
                    '+ Prorrogado
                Else
                    lintDeferred = .nDeferred
                    ldblCapital = .nDefamount
                    ldblPremium = .nResc_val
                    '+ En prorrogado, valor de rescate es el mismo de prima
                    ldblRescVal = .nResc_val
                    ldtmExpirdat = DateAdd(Microsoft.VisualBasic.DateInterval.DayOfYear, .nPro_year, ldtmStartdate)
                    ldtmExpirdat = DateAdd(Microsoft.VisualBasic.DateInterval.Month, .nPeriod_cov, ldtmExpirdat)
                End If
            End With
            'UPGRADE_NOTE: Object lclsGuarant_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsGuarant_val = Nothing

            '+ Vida <> de Tradicional
        Else

            ldblPremium = 0
            ldblRescVal = insGetSurrenAmount(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "VI009",  ,  , ldtmStartdate)

            '+ Saldado
            If sIndReduction = "1" Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                ldblCapital = insCapitalreduc(sCertype, nBranch, nProduct, nPolicy, IIf(IsDBNull(nCertif), 0, nCertif), dEffecdate)
                '+ Prorrogado
            Else
                ldtmExpirdat = insVigenciareduc(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
            End If

        End If

        '+ Se ejecuta el proceso de reduccion de vigencia o capital
        lrecinsUpdVI008 = New eRemoteDB.Execute

        '+ Parameters definition to stored procedure 'insudb.insUpdVI008'
        '+ Definición de parámetros para stored procedure 'insudb.insUpdVI008'
        '+ Data read on 20/03/2000 11:34:46
        '+ Información leída el 20/03/2000 11:34:46

        With lrecinsUpdVI008
            .StoredProcedure = "insUpdVI008"

            '+ Preliminar
            If sIndExecute = "1" Then
                .Parameters.Add("nExetype", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '+ Definitiva
            Else
                .Parameters.Add("nExetype", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            '+ Saldado
            If sIndReduction = "1" Then
                .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '+ Prorrogado
            Else
                .Parameters.Add("nType", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nCapital", ldblCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", ldblPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", ldtmExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '+ Anular recibos
            If sIndNulling = "1" Then
                .Parameters.Add("nIndNulling", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nIndNulling", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", lintProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeferred", lintDeferred, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRescval", ldblRescVal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insUpdVI008 = True
            End If
        End With

insUpdVI008_Err:
        If Err.Number Then
            insUpdVI008 = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdVI008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdVI008 = Nothing
        On Error GoTo 0
    End Function

    '% insValVI008: Esta función se encarga de validar los datos introducidos en la
    '% forma.
    Public Function insValVI008(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sIndReduction As String, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal nProponum As Double = 0, Optional ByVal sCodisplori As String = "") As String
        '-Objetos para validaciones
        Dim lclsErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values
        Dim lclsProduct As eProduct.Product
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsLoan As ePolicy.Loans
        Dim lobjClaim As Object
        Dim lobjExistProp As ValPolicySeq
        Dim lclsPremium As eCollection.Premium

        '-Variables de propiedades
        Dim lintyear As Integer
        Dim lintMonth As Integer
        Dim lblnError As Boolean
        Dim lblnProdMaster As Boolean
        Dim lblnProduct_li As Boolean
        Dim lblnPolicy As Boolean
        Dim lblnCertificat As Boolean
        Dim lstrDescript As String = String.Empty

        On Error GoTo InsValVI008_Err

        lclsErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values
        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
        lobjExistProp = New ValPolicySeq

        If nProponum = eRemoteDB.Constants.intNull Then nProponum = 0

        '+ It's validated the Branch field
        '+ Se valida el campo ramo
        If nBranch = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+ It's validated the Product field
        '+ Se valida el campo Producto
        If nProduct = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 1014)
        Else
            lobjValues.Parameters.Add("nBranch", CShort(nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not lobjValues.IsValid("tabProdmaster1", CStr(CShort(nProduct)), True) Then
                lclsErrors.ErrorMessage(sCodispl, 1011)
            Else
                lblnProdMaster = lclsProduct.insValProdMaster(nBranch, nProduct)

                If lblnProdMaster Then
                    If CStr(lclsProduct.sBrancht) <> "1" And CStr(lclsProduct.sBrancht) <> "2" And CStr(lclsProduct.sBrancht) <> "5" Then
                        lclsErrors.ErrorMessage(sCodispl, 3987)
                    Else
                        lblnProduct_li = lclsProduct.FindProduct_li(nBranch, nProduct, Today)

                        If lblnProduct_li Then
                            If (Trim(lclsProduct.sRoureddc) = String.Empty And sIndReduction = "2") Or (Trim(lclsProduct.sRoureduc) = String.Empty And sIndReduction = "1") Then
                                lclsErrors.ErrorMessage(sCodispl, 3406)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '+ It's validated the Policy field
        '+Se valida el campo póliza

        If nPolicy = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 3003)
        Else
            lblnPolicy = lclsPolicy.Find(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, True)

            If Not lblnPolicy Then
                lclsErrors.ErrorMessage(sCodispl, 3001)
            Else
                If lclsPolicy.sStatus_pol = Trim(Str(Policy.TypeStatus_Pol.cstrIncomplete)) Or lclsPolicy.sStatus_pol = Trim(Str(Policy.TypeStatus_Pol.cstrInvalid)) Then
                    lclsErrors.ErrorMessage(sCodispl, 3720)
                Else
                    If Find_CertificatVI008(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, 0) Then
                        lclsErrors.ErrorMessage(sCodispl, 3961)
                    End If
                End If

                '+ El estado de la póliza no debe ser rescatada sStatus_pol = "9"
                If lclsPolicy.sStatus_pol = "9" Then
                    lclsErrors.ErrorMessage(sCodispl, 60486)
                End If

                '+ Debe tener valores de rescate
                If insGetSurrenAmount(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, nCertif, dEffecdate, "VI009",  , IIf(lblnProdMaster, lclsProduct.nProdClas, 0), lclsPolicy.dStartdate, IIf(lblnProdMaster, lclsProduct.sRousurre, String.Empty)) = 0 Then
                    lclsErrors.ErrorMessage(sCodispl, 3408)
                End If

                '+ Para Prorrogar, Poliza no debe estar anulada
                If (sIndReduction = "2") Then
                    If (lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull) Then
                        lclsErrors.ErrorMessage(sCodispl, 3098)
                    End If
                Else
                    '+ Para Saldar, Poliza solo puede estar anulada si motivo es 'Falta de pago'
                    If (lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull) And (lclsPolicy.nNullcode <> 70) Then
                        lclsErrors.ErrorMessage(sCodispl, 55789)
                    End If

                    '+ No deben existir prestamos vigentes
                    lclsLoan = New Loans
                    lclsLoan.dLoan_date = dEffecdate
                    If Not lclsLoan.insValLastLoan(nBranch, nProduct, nPolicy, nCertif) Then
                        lclsErrors.ErrorMessage(sCodispl, 55787)
                    End If

                    '+ Debe estar anulada el tiempo indicado por el producto
                    If lblnProduct_li Then
                        If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                            If DateAdd(Microsoft.VisualBasic.DateInterval.Month, IIf(lclsProduct.nMonth_surr = eRemoteDB.Constants.intNull, 0, lclsProduct.nMonth_surr), lclsPolicy.dNulldate) > dEffecdate Then
                                lclsErrors.ErrorMessage(sCodispl, 55788)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '+ It's validated the Certificat field
        '+Se valida el campo certificado

        If nCertif = eRemoteDB.Constants.intNull And Not lclsPolicy.sPolitype = "1" Then
            lclsErrors.ErrorMessage(sCodispl, 3006)
        Else
            If nCertif <> eRemoteDB.Constants.intNull And Not lclsPolicy.sPolitype = "1" Then

                lblnCertificat = lclsCertificat.Find(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, nCertif, True)

                If Not lblnCertificat Then
                    lclsErrors.ErrorMessage(sCodispl, 3010)
                Else
                    If lclsCertificat.sStatusva = Trim(Str(Policy.TypeStatus_Pol.cstrIncomplete)) Or lclsCertificat.sStatusva = Trim(Str(Policy.TypeStatus_Pol.cstrInvalid)) Then
                        lclsErrors.ErrorMessage(sCodispl, 750044)
                    Else
                        If lclsCertificat.dNulldate = eRemoteDB.Constants.dtmNull Then
                            lclsErrors.ErrorMessage(sCodispl, 3099)
                        Else
                            If Find_CertificatVI008(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, nCertif) Then
                                lclsErrors.ErrorMessage(sCodispl, 3961)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '+ It's validated the Date field
        '+ Validación de la Fecha de reducción de capital o vigencia.

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            lclsErrors.ErrorMessage(sCodispl, 7079)
        Else

            '+ Valida fechas de Poliza
            If nCertif = eRemoteDB.Constants.intNull Then
                If lblnPolicy Then

                    '+ It's validated that the recue date be greater than the Effective date of the policy
                    '+ Se válida que la fecha de rescate sea posterior a la fecha de efecto de la Póliza
                    If dEffecdate <= lclsPolicy.dDate_Origi Then
                        lclsErrors.ErrorMessage(sCodispl, 3405)
                    Else

                        '+ It's validated that the years difference among the effective date of the policy and the current date be
                        '+ greater or iqual of two years
                        '+ Se válida que la diferencia de años entre el efecto de la póliza y la realización de la transacción debe ser superior
                        '+ o igual a dos años
                        lclsGeneral = New eGeneral.GeneralFunction
                        Call lclsGeneral.getYearMonthDiff(lclsPolicy.dDate_Origi, dEffecdate, lintyear, lintMonth)
                        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsGeneral = Nothing

                        If lintyear < 2 Then
                            lclsErrors.ErrorMessage(sCodispl, 3407)
                        End If
                    End If
                End If

                '+ Valida fechas de Certificado
            Else

                '+ It's validated that the recue date be greater than the Effective date of the certificat
                '+ Se válida que la fecha de rescate sea posterior a la fecha de efecto del Certificado
                If Not lblnCertificat Then
                    If dEffecdate <= lclsCertificat.dDate_Origi Then
                        lclsErrors.ErrorMessage(sCodispl, 3405)
                    Else

                        '+ It's validated that the years difference among the effective date of the policy and the current date be
                        '+ greater or iqual of two years
                        '+ Se válida que la diferencia de años entre el efecto de la póliza y la realización de la transacción debe ser superior
                        '+ o igual a dos años
                        lclsGeneral = New eGeneral.GeneralFunction
                        Call lclsGeneral.getYearMonthDiff(lclsCertificat.dDate_Origi, dEffecdate, lintyear, lintMonth)
                        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsGeneral = Nothing
                        If lintyear < 2 Then
                            lclsErrors.ErrorMessage(sCodispl, 3407)
                        End If
                    End If
                End If
            End If
        End If

        '+ se valida que la poliza/certificado no tenga propuestas especiales/de endoso pendientes
        If sCodisplori <> "CA767" Then
            If existsModProposal(nBranch, nProduct, nPolicy, nCertif, True) Then
                lclsErrors.ErrorMessage(sCodispl, 60303,  , eFunctions.Errors.TextAlign.RigthAling, "(" & sMessage & ")")
            End If
        End If

        '+ se valida que la poliza no posea otras propuestas pendientes
        If lobjExistProp.ReaPolicy_QuotProp("2", nBranch, nProduct, nPolicy, nCertif, nProponum, Stat_quot.esqPending, lstrDescript) Then
            lclsErrors.ErrorMessage(sCodispl, 55779)
        End If

        '+ se valida si poliza/certificado posee una declaración de siniestro

        If lobjClaim.reacountclaim(sCertype, nBranch, nProduct, nPolicy, nCertif) <> 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55778)
        End If

        '+ Se valida que la póliza no tenga recibos pendientes de pago.

        lclsPremium = New eCollection.Premium

        If lblnPolicy Then
            If lclsPremium.Find_ByPolicy(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), lclsPolicy.sColinvot, dEffecdate) Then
                lclsErrors.ErrorMessage(sCodispl, 3663,  , eFunctions.Errors.TextAlign.LeftAling, "No se puede saldar. ")
            End If
        End If
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing

        '+ A message is sent to the user asking if the process will continue
        '+ Se envia advertencia al usuario para que determine si desea proseguir el proceso
        If lclsErrors.Confirm = String.Empty Then
            lclsErrors.ErrorMessage(sCodispl, 3960)
        End If

        If nOffice = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("VI008", 9120)
        End If
        If nOfficeAgen = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("VI008", 55519)
        End If
        If nAgency = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("VI008", 1080)
        End If

        insValVI008 = lclsErrors.Confirm

InsValVI008_Err:
        If Err.Number Then
            insValVI008 = insValVI008 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsLoan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLoan = Nothing
    End Function

    '% insPostVI008:Se realiza la actualización de los datos en la ventana VI008
    Public Function insPostVI008(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sExeType As String, ByVal sIndReduction As String, ByVal sIndNulling As String, ByVal sIndProposal As String, Optional ByVal nOperat As Integer = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal nAgency As Integer = 0, Optional ByVal sKey As String = "") As Boolean
        '-Objetos de proceso
        Dim lclsNotes As eGeneralForm.Notes
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsValPolicyTra As ePolicy.ValPolicyTra
        Dim lclsRequest As ePolicy.Request
        Dim lclsCertificat As ePolicy.Certificat
        '-Numero de propuesta generado
        Dim llngProposalNum As Double

        On Error GoTo InsPostVI008_Err

        lclsNotes = New eGeneralForm.Notes
        lclsPolicy_his = New Policy_his
        lclsPolicy = New Policy
        lclsCertificat = New Certificat
        lclsRequest = New Request

        insPostVI008 = False

        '+ Ejecución preliminar
        If sExeType = "1" Then
            '+ Llamado directamente (no a través de otra transacción)
            If sCodispl = "VI008" Then
                '+ Se genera propuesta si se indico en la transaccion
                If sIndProposal = "1" Then
                    '+ Se crea propuesta
                    lclsValPolicyTra = New ValPolicyTra
                    If lclsValPolicyTra.AddProposal(sCertype, nBranch, nProduct, nPolicy, nCertif, llngProposalNum, dEffecdate, nUsercode, nAgency) Then

                        Me.nCode = llngProposalNum
                        '+ Se crea solicitud
                        With lclsRequest
                            .sCertype = "8"
                            .nBranch = nBranch
                            .nProduct = nProduct
                            .nPolicy = llngProposalNum
                            .nCertif = nCertif
                            .dEffecdate = dEffecdate
                            '+ Saldado
                            If sIndReduction = "1" Then
                                .nOrigin = Request.eRequestOrigin.reqOrigSettled
                                '+ Prorrogado
                            Else
                                .nOrigin = Request.eRequestOrigin.reqOrigExtended
                            End If
                            .sDescript = sDescript
                            .nNotenum = nNotenum
                            .nUsercode = nUsercode
                            .sNull_rec = sIndNulling
                            insPostVI008 = .Add
                        End With
                    End If
                End If

            ElseIf sCodispl = "CA767" Then

                '+ Se obtiene numero de solicitud creada
                If lclsPolicy_his.FindLastMovementByType(sCertype, nBranch, nProduct, nPolicy, nCertif, IIf(nCertif > 0, 10, 9)) Then
                    If nAction = eFunctions.Menues.TypeActions.clngAcceptdataCancel Then
                        '+ se elimina nota si cancela la operación
                        Call lclsNotes.DeleteNote(nNotenum)
                        With lclsRequest
                            If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dEffecdate) Then

                            End If
                            .nNotenum = 0
                            .sDescript = ""

                            insPostVI008 = .Update
                        End With
                        '+ Actualizar
                    ElseIf nOperat = 5 Then
                        '+ Se actualiza la solicitud
                        With lclsRequest
                            If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dEffecdate) Then
                                .nNotenum = nNotenum
                                .sDescript = sDescript
                                .sNull_rec = sIndNulling
                                '+ Saldado
                                If sIndReduction = "1" Then
                                    .nOrigin = Request.eRequestOrigin.reqOrigSettled
                                    '+ Prorrogado
                                Else
                                    .nOrigin = Request.eRequestOrigin.reqOrigExtended
                                End If
                                insPostVI008 = .Update
                            End If

                        End With
                        '+ Tipo de Accion
                    End If
                    '+ Find policy_his
                End If
                '+ sCodispl
            End If
            '+ Ejecucion definitiva
        Else
            '+ Se busca certificado para obtener datos y actualizarlo
            Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
            '+ Llamado a través de transaccion CA767
            If sCodispl = "CA767" Then
                If nAction = eFunctions.Menues.TypeActions.clngAcceptdataCancel Then
                    '+ Se elimina nota si cancela la operación
                    lclsNotes = New eGeneralForm.Notes
                    Call lclsNotes.DeleteNote(nNotenum)

                    With lclsRequest
                        If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dEffecdate) Then

                        End If
                        .nNotenum = 0
                        .sDescript = ""

                        insPostVI008 = .Update
                    End With

                ElseIf nOperat = 2 Then  ' aprobar
                    '+ Se obtiene numero de solicitud creada
                    If lclsPolicy_his.FindLastMovementByType(sCertype, nBranch, nProduct, nPolicy, nCertif, IIf(nCertif > 0, 10, 9)) Then
                        '+ Se actualiza la solicitud
                        With lclsRequest
                            If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dEffecdate) Then
                                .nNotenum = nNotenum
                                .sDescript = sDescript
                                .sNull_rec = sIndNulling
                                .nUsercode = nUsercode
                                '+ Saldado
                                If sIndReduction = "1" Then
                                    .nOrigin = Request.eRequestOrigin.reqOrigSettled
                                    '+ Prorrogado
                                Else
                                    .nOrigin = Request.eRequestOrigin.reqOrigExtended
                                End If
                                insPostVI008 = .Update
                            End If
                        End With
                    End If
                    '+ Se crea registro en historia de poliza
                    With lclsPolicy_his
                        .sCertype = sCertype
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nPolicy = nPolicy
                        .nCertif = nCertif
                        .nUsercode = nUsercode
                        .nAgency = nAgency
                        .nMovement = 0
                        If sIndReduction = "1" Then '+ Saldado
                            .nType = Policy_his.ePolicyHisType.ePolHisTypeCapitalReducion
                        Else '+ Prorrogado
                            .nType = Policy_his.ePolicyHisType.ePolHisTypeDurationReducion
                        End If
                        insPostVI008 = .insCrePolicy_his
                    End With

                    '+ Se aprueba propuesta
                    With lclsCertificat
                        '+ Se actualiza los datos de la poliza/certificado.
                        If .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                            .sStatusva = CStr(7) ' Saldado/Prorrogado
                            .dChangdat = dEffecdate
                            insPostVI008 = .Update
                        End If
                        '+ Se actualiza los datos de la propuesta.
                        If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif) Then
                            .nStatquota = Stat_quot.esqApprove
                            .dChangdat = dEffecdate
                            insPostVI008 = .Update
                        End If
                    End With

                    With lclsPolicy
                        If .Find("2", nBranch, nProduct, nPolicy) Then
                            .sStatus_pol = CStr(7)
                            insPostVI008 = .Add
                        End If
                    End With
                End If
            End If
        End If
        '+ Se realiza o simula (segun modo de ejecucion) la reducción de vigencia o capital
        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode
            .nAgency = nAgency
            .nProponum = IIf(llngProposalNum = eRemoteDB.Constants.intNull, 0, llngProposalNum)

            insPostVI008 = insUpdVI008(sExeType, sIndReduction, sIndNulling, sKey)
        End With

        '+ Para Saldado definitivo, si poliza esta anulada se rehabilita
        '+ y crea movimiento
        If insPostVI008 And sExeType = "2" And sIndReduction = "1" Then

            If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                If lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
                    With lclsPolicy_his
                        .sCertype = sCertype
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nPolicy = nPolicy
                        .nNullcode = eRemoteDB.Constants.intNull
                        .dNulldate = eRemoteDB.Constants.dtmNull
                        .nCertificat = 0
                        '+ Saldado automatico
                        .nType = Policy_his.ePolicyHisType.ePolHisTypeSettled
                        .dReahdate = eRemoteDB.Constants.dtmNull
                        .nUsercode = nUsercode
                        .nAgency = nAgency
                        insPostVI008 = .Update_PolCerti
                    End With
                End If
            End If
        End If

InsPostVI008_Err:
        If Err.Number Then
            insPostVI008 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsNotes = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsValPolicyTra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValPolicyTra = Nothing
        'UPGRADE_NOTE: Object lclsRequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRequest = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '% insCapitalreduc: Esta rutina permite calcular el capital reducido
    Public Function insCapitalreduc(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Double
        Dim lrecinsCapitalreduc As eRemoteDB.Execute

        On Error GoTo insCapitalreduc_Err
        lrecinsCapitalreduc = New eRemoteDB.Execute
        '+ Parameters definition to stored procedure 'insudb.insCapitalreduc'
        '+ Definición de parámetros para stored procedure 'insudb.insCapitalreduc'
        '+ Data read on 09/01/1999 16:01:58
        '+ Información leída el 09/01/1999 16:01:58
        With lrecinsCapitalreduc
            .StoredProcedure = "insCalCapitalreduc"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insCapitalreduc = .Parameters("nCapital").Value
            End If
        End With

insCapitalreduc_Err:
        If Err.Number Then
            insCapitalreduc = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCapitalreduc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCapitalreduc = Nothing
    End Function

    '% insProposal_of_Pol: Esta rutina permite recuperar el número de propuesta de una Póliza/Certificado
    Public Function insProposal_of_Pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPol_quot As Double, ByVal nCertif As Integer) As Integer
        Dim lrecinsProposal_of_Pol As eRemoteDB.Execute

        On Error GoTo insProposal_of_Pol_Err
        lrecinsProposal_of_Pol = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insCapitalreduc'
        '+ Información leída el 01/11/2002
        With lrecinsProposal_of_Pol
            .StoredProcedure = "ReaProposal_of_Pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPol_Quot", nPol_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insProposal_of_Pol = .Parameters("nPolicy").Value
            End If
        End With

insProposal_of_Pol_Err:
        If Err.Number Then
            insProposal_of_Pol = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsProposal_of_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsProposal_of_Pol = Nothing
    End Function


    '% insVigenciareduc: Esta rutina permite calcular la vigencia reducida
    Private Function insVigenciareduc(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Date
        Dim ldtmdVigency As Date
        Dim lrecinsCalVigenciaReduc As eRemoteDB.Execute

        On Error GoTo insVigenciareduc_Err

        insVigenciareduc = eRemoteDB.Constants.dtmNull

        lrecinsCalVigenciaReduc = New eRemoteDB.Execute

        '+ Parameters definition to stored procedure 'insudb.insCalVigenciaReduc'
        '+ Definición de parámetros para stored procedure 'insudb.insCalVigenciaReduc'
        '+ Data read on 20/03/2000 09:41:15
        '+ Información leída el 20/03/2000 09:41:15
        ldtmdVigency = Today
        With lrecinsCalVigenciaReduc
            .StoredProcedure = "insCalVigenciaReduc"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dVigencia", ldtmdVigency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insVigenciareduc = .Parameters.Item("dVigencia").Value
            End If
        End With

insVigenciareduc_Err:
        If Err.Number Then
            insVigenciareduc = eRemoteDB.Constants.dtmNull
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCalVigenciaReduc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalVigenciaReduc = Nothing
    End Function

    '% reacertif_proposal: Esta rutina permite saber si existen propuestas/cotizaciones
    '%                    de endoso
    Public Function existsModProposal(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal bPending As Boolean = True) As Boolean
        Dim lrecreaModproposalcount As eRemoteDB.Execute

        On Error GoTo existsModProposal_Err
        '+ Definición de store procedure reaModproposalcount al 03-06-2002 18:55:32
        lrecreaModproposalcount = New eRemoteDB.Execute
        With lrecreaModproposalcount
            .StoredProcedure = "reaModproposalcount"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPending", IIf(bPending, 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMessaged", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                nPol_quot = .Parameters("nCount").Value
                sMessage = .Parameters("sMessaged").Value
                If nPol_quot > 0 Then
                    existsModProposal = True
                End If
            End If
        End With

existsModProposal_Err:
        If Err.Number Then
            existsModProposal = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaModproposalcount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaModproposalcount = Nothing
    End Function


    '% reacertif_proposal: Esta rutina permite saber si existen propuestas/cotizaciones
    '%                    de endoso
    Public Function Proposal_val(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Integer, ByVal nPropQuot As Double) As String
        Dim lrecreaModproposalcount As eRemoteDB.Execute

        On Error GoTo existsModProposal_Err
        '+ Definición de store procedure reaModproposalcount al 03-06-2002 18:55:32
        lrecreaModproposalcount = New eRemoteDB.Execute
        With lrecreaModproposalcount
            .StoredProcedure = "REAPROPOSAL_VAL"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPropQuot", nPropQuot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sQuotprop", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Proposal_val = .Parameters("sQuotprop").Value
            End If
        End With

existsModProposal_Err:
        If Err.Number Then
            Proposal_val = ""
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaModproposalcount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaModproposalcount = Nothing
    End Function




    '% ValSpecialProposal: Esta rutina permite saber si existen propuestas especiales
    '%                    para una poliza/certifcado, además devuelve el estado y el origen de la propuesta
    Public Function ValSpecialProposal(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Integer, ByVal nOrigin As Integer, ByVal sDescript As String) As Boolean
        Dim lrecreaSpecialproposalval As eRemoteDB.Execute

        On Error GoTo reaSpecialproposalval_Err

        lrecreaSpecialproposalval = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaspecialproposalval'
        '+ Información leída el 11/04/2002
        With lrecreaSpecialproposalval
            .StoredProcedure = "reaspecialproposalval"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                ValSpecialProposal = True
                nStatquota = .Parameters.Item("nStatquota").Value
                nOrigin = .Parameters.Item("nOrigin").Value
                sDescript = .Parameters.Item("sDescript").Value
            End If
        End With

reaSpecialproposalval_Err:
        If Err.Number Then
            ValSpecialProposal = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaSpecialproposalval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaSpecialproposalval = Nothing
    End Function

    '% getParticularDataGroup: Obtiene el número de grupo asociado a los datos particulares.
    Public Function getParticularDataGroup(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Double, ByVal ldtmEffecdate As Date) As Integer
        Dim lrecParticularData As eRemoteDB.Execute

        lrecParticularData = New eRemoteDB.Execute

        On Error GoTo getParticularDataGroup_Err

        getParticularDataGroup = -1

        With lrecParticularData
            .StoredProcedure = "reaCertificNN_nGroup"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                getParticularDataGroup = .FieldToClass("nGroup")
                .RCloseRec()
            End If
        End With

getParticularDataGroup_Err:
        If Err.Number Then
            getParticularDataGroup = -1
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecParticularData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecParticularData = Nothing
    End Function

    '% insUpdPolicy_his: Se actualiza la fecha de próxima facturación en la tabla Policy y Certificat
    Public Function insUpdNextreceipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dNextReceip As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecupdNextreceipt As eRemoteDB.Execute
        On Error GoTo updNextreceipt_Err

        lrecupdNextreceipt = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure updNextreceipt al 09-20-2002 19:02:13
        '+
        With lrecupdNextreceipt
            .StoredProcedure = "updNextreceipt"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextreceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdNextreceipt = .Run(False)
        End With

updNextreceipt_Err:
        If Err.Number Then
            insUpdNextreceipt = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdNextreceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdNextreceipt = Nothing
    End Function

    '% getPolicyByProponum: Esta rutina devuelve el número de póliza de una propuesta pasada como parámetro
    Public Function getPolicyByProponum(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As String
        '- Se define la variable lrecCertificat
        Dim lRecCertificat As eRemoteDB.Execute
        Dim lstrPolicy As String = ""

        On Error GoTo getPolicyByProponum_Err

        lRecCertificat = New eRemoteDB.Execute

        With lRecCertificat
            .StoredProcedure = "getPolicyByProponum"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicy", lstrPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            getPolicyByProponum = .Parameters("sPolicy").Value
        End With
        'UPGRADE_NOTE: Object lRecCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lRecCertificat = Nothing

getPolicyByProponum_Err:
        If Err.Number Then
            getPolicyByProponum = String.Empty
        End If
        On Error GoTo 0
    End Function

    '% insValCertifByPropoNum: Valida la existencia de una propuesta que no haya sido convertida
    Public Function insValCertifByPropoNum(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
        Dim lrecinsValCertifByPropoNum As eRemoteDB.Execute

        On Error GoTo insValCertifByPropoNum_Err

        lrecinsValCertifByPropoNum = New eRemoteDB.Execute

        With lrecinsValCertifByPropoNum
            .StoredProcedure = "insValCertifByPropoNum"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValid", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insValCertifByPropoNum = .Parameters("nValid").Value = 1
            End If
        End With

insValCertifByPropoNum_Err:
        If Err.Number Then
            insValCertifByPropoNum = False
        End If
        'UPGRADE_NOTE: Object lrecinsValCertifByPropoNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValCertifByPropoNum = Nothing
        On Error GoTo 0
    End Function

    '% insLoadCA048 : Único método público que "dispara" toda la secuencia de la CA048
    Public Sub insLoadCA048(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sTypeCompany As String)

        Dim lrecinsPreca048 As eRemoteDB.Execute

        On Error GoTo insLoadCA048_Err
        lrecinsPreca048 = New eRemoteDB.Execute

        With lrecinsPreca048
            .StoredProcedure = "insPreca048"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompany", sTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTextMessage", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnWaitCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPendenstat", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotreceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAutreceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nManualreceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrinterstat", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBlnpendenstat", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.sMessage = .Parameters.Item("sTextMessage").Value
                Me.nTransactio = .Parameters.Item("nTransactio").Value
                Me.nPendenStat = .Parameters.Item("nPendenstat").Value
                Me.bPendenstat = .Parameters.Item("nBlnpendenstat").Value = 1
                Me.nWait_code = .Parameters.Item("nWaitCode").Value
                Me.bWait_code = .Parameters.Item("nblnWaitCode").Value = 1
                Me.bPrinterStat = .Parameters.Item("nPrinterstat").Value = 1
                Me.bNotReceipt = .Parameters.Item("nNotreceipt").Value = 1
                Me.bAutReceipt = .Parameters.Item("nAutreceipt").Value = 1
                Me.bManualReceipt = .Parameters.Item("nPrinterstat").Value = 1
                .RCloseRec()
            End If
        End With

insLoadCA048_Err:
        If Err.Number Then
            Me.sMessage = "insLoadCa048: Error al cargar datos - " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsPreca048 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPreca048 = Nothing
    End Sub

    Public Function Find_policy(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaCertificat_policy As eRemoteDB.Execute

        On Error GoTo findPolicy_Err
        lrecreaCertificat_policy = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bFind Then

            '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
            With lrecreaCertificat_policy
                .StoredProcedure = "reacertificat_policy"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    .RCloseRec()
                    Find_policy = True
                End If
            End With
        Else
            Find_policy = True
        End If

findPolicy_Err:
        If Err.Number Then
            Find_policy = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertificat_policy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_policy = Nothing
    End Function
    '% insUpdNote_Benef: Realiza la actualización en la tabla policy el campo nNote_Benef
    Public Function insUpdNote_Benef(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal nNote_benef As Integer) As Boolean
        On Error GoTo insUpdNote_Benef_Err

        Dim lrecinsUpdNote_Benef As eRemoteDB.Execute

        lrecinsUpdNote_Benef = New eRemoteDB.Execute

        With lrecinsUpdNote_Benef
            .StoredProcedure = "insUpdNote_Benef"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNote_Benef", nNote_benef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

insUpdNote_Benef_Err:
        If Err.Number Then
            insUpdNote_Benef = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdNote_Benef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdNote_Benef = Nothing
        On Error GoTo 0
    End Function

    '%Find_CRL515: Verifica ala existencia de la Poliza/certificado/asegurado
    Public Function Find_CRL515(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreapolcer_client As eRemoteDB.Execute

        On Error GoTo Find_Err
        lrecreapolcer_client = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bFind Then

            '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
            With lrecreapolcer_client
                .StoredProcedure = "reapolcer_client"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sCertype = sCertype
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nPolicy = nPolicy
                    Me.nCertif = nCertif
                    .RCloseRec()
                    Find_CRL515 = True
                End If
            End With
        Else
            Find_CRL515 = True
        End If

Find_Err:
        If Err.Number Then
            Find_CRL515 = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreapolcer_client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreapolcer_client = Nothing
    End Function

    '%Find_Renewal: Verifica si la poliza esta en un periodo que puede ser renovada
    Public Function Find_Renewal(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lRecCertificat As eRemoteDB.Execute
        Dim nExists As Short
        On Error GoTo Find_Err
        lRecCertificat = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
        With lRecCertificat
            .StoredProcedure = "insvalrenewal_certificat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Find_Renewal = (.Parameters("nExists").Value = 1)
        End With

Find_Err:
        If Err.Number Then
            Find_Renewal = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lRecCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lRecCertificat = Nothing
    End Function
    '%Find_PolicyFI001: Carga la información del certificado de una póliza para la transacción de
    '%      Financiamiento
    Public Function Find_PolicyFI001(ByVal sCertype As String, ByVal nPolicy As Double) As Boolean
        Dim lrecreaPolicyFI001 As eRemoteDB.Execute

        On Error GoTo Find_PolicyFI001_Err
        lrecreaPolicyFI001 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure reaPolicyFI001
        With lrecreaPolicyFI001
            .StoredProcedure = "reaPolicyFI001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nBranch = .FieldToClass("nBranch")
                sDesBranch = .FieldToClass("sDesBranch")
                nProduct = .FieldToClass("nProduct")
                sDesProduct = .FieldToClass("sDesProduct")
                sDigit = .FieldToClass("sDigit")
                sCliename = .FieldToClass("sCliename")
                dStartdate = .FieldToClass("dStartdate")
                dExpirdat = .FieldToClass("dExpirdat")
                sClient = .FieldToClass("sClient")
                nOffice = .FieldToClass("nOffice")
                nWay_pay = .FieldToClass("nWay_pay")
                nBill_day = .FieldToClass("nBill_day")
                nCurrency = .FieldToClass("nCurrency")
                .RCloseRec()
                Find_PolicyFI001 = True
            End If
        End With

Find_PolicyFI001_Err:
        If Err.Number Then
            Find_PolicyFI001 = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicyFI001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicyFI001 = Nothing
    End Function

    Public Function FindPolicyVI7010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String) As Boolean
        Dim lrecreaCertificat_policy As eRemoteDB.Execute

        On Error GoTo findPolicy_Err
        lrecreaCertificat_policy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
        With lrecreaCertificat_policy
            .StoredProcedure = "InsVi7010pkg.FindPolicy"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                .RCloseRec()
                FindPolicyVI7010 = True
            Else
                FindPolicyVI7010 = False
            End If
        End With

findPolicy_Err:
        If Err.Number Then
            FindPolicyVI7010 = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertificat_policy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_policy = Nothing
    End Function

    '% insReaPrintVUL: Función que retorna VERDADERO al realizar la lectura de registros para simular
    '% la impresión de póliza de productos VUL (SCAL001)
    Public Function insReaPrintVUL(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaCertif As eRemoteDB.Execute

        On Error GoTo insReaPrintVUL_Err

        lrecreaCertif = New eRemoteDB.Execute

        With lrecreaCertif
            .StoredProcedure = "INSREAPRINTVUL"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insReaPrintVUL = .Run

            If insReaPrintVUL Then
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.dStartdate = .FieldToClass("dStartdate")
                Me.sClient = .FieldToClass("sClient")
                Me.sFirstname = .FieldToClass("sFirstname")
                Me.sLastname = .FieldToClass("sLastname")
                Me.sLastname2 = .FieldToClass("sLastname2")
                Me.dBirthDat = .FieldToClass("dBirthdate")
                Me.nAge = .FieldToClass("nAge")
                Me.sSexclie = .FieldToClass("sSexclien")
                Me.nCivilsta = .FieldToClass("nCivilsta")
                Me.sSmoking = .FieldToClass("sSmoking")
                Me.nOption = .FieldToClass("nOption")
                Me.nPayfreq = .FieldToClass("nPayfreq")
                Me.sAgent_cli = .FieldToClass("sAgent_cli")
                Me.sAgent_name = .FieldToClass("sAgent_name")
                Me.sAgent_Phones = .FieldToClass("sAgent_Phones")
                Me.nTyperisk = .FieldToClass("nTypeRisk")

                .RCloseRec()
            End If
        End With

insReaPrintVUL_Err:
        If Err.Number Then
            insReaPrintVUL = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertif = Nothing
    End Function


    '% Update_sRecType: Actualiza el usuario que está modificando el certificado
    Public Function Update_sRecType(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sRectype As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecupdCertificatsRecType As eRemoteDB.Execute

        lrecupdCertificatsRecType = New eRemoteDB.Execute

        On Error GoTo Update_sRecType_Err

        '+ Definición de parámetros para stored procedure 'insudb.updCertificatUserAmend'
        '+ Información leída el 06/11/2000 09:16:51 a.m.

        With lrecupdCertificatsRecType
            .StoredProcedure = "updCertificatsRecType"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRectype", sRectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_sRecType = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdCertificatUserAmend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCertificatsRecType = Nothing

Update_sRecType_Err:
        If Err.Number Then
            Update_sRecType = False
        End If
        On Error GoTo 0
    End Function

    '%insValPolicy: realiza las validaciones sobre el número de póliza ingresado
    Public Function insValCertificat(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer

        On Error GoTo insValCertificat_Err
        Dim lclsPolicy As ePolicy.Policy
        Dim lrecreaPolicy As eRemoteDB.Execute
        lrecreaPolicy = New eRemoteDB.Execute
        lclsPolicy = New ePolicy.Policy
        If nPolicy <= 0 Then
            insValCertificat = -2
        ElseIf nBranch > 0 And nProduct > 0 And nPolicy > 0 Then

            '+ Definición de parámetros para stored procedure 'insudb.reaPolicy'
            '+ Información leída el 27/07/2001 03:23:01 p.m.

            With lrecreaPolicy
                .StoredProcedure = "reaCertificat_branch2"
                .Parameters.Add("sCertype", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    If .FieldToClass("nNullCode") > 0 Then
                        insValCertificat = 1
                    ElseIf .FieldToClass("sStatusva") <> "1" And .FieldToClass("sStatusva") <> "4" And .FieldToClass("sStatusva") <> "5" Then
                        insValCertificat = 2
                    Else

                        '+ Se valida que estén registrados los factores de revalorización necesarios
                        '+ Para la renovacion
                        sIndextyp = .FieldToClass("sIndextyp")
                        sPolitype = .FieldToClass("spolitype")
                        dNextReceip = .FieldToClass("dNextreceip")
                        sColtimre = .FieldToClass("sColtimre")
                        sRenewal = .FieldToClass("sRenewal")
                        sReceipt_ind = .FieldToClass("sReceipt_Ind")

                        Me.nCertif = .FieldToClass("nCertif")
                        dStartdate = CDate(Format(.FieldToClass("dStartDate"), "yyyy/MM/dd"))
                        dExpirdat = CDate(Format(.FieldToClass("dExpirdat"), "yyyy/MM/dd"))


                        Call lclsPolicy.insShowClient(nBranch, nProduct, nPolicy, Me.nCertif, Today)
                        Me.sCliename = lclsPolicy.sCliename
                        If .FieldToClass("nintermed") <> eRemoteDB.Constants.intNull Then
                            sIntermediaName = lclsPolicy.insName(.FieldToClass("nintermed"), False)
                            nIntermed = .FieldToClass("nintermed")
                        Else
                            sIntermediaName = String.Empty
                            nIntermed = 0
                        End If
                        .RCloseRec()
                    End If
                Else
                    insValCertificat = -1
                End If
            End With
        End If

insValCertificat_Err:
        If Err.Number Then
            insValCertificat = -1
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy = Nothing
    End Function

    '% CreManReceiptN: Función que se utiliza para la emisión del recibo manual
    '--------------------------------------------------------------------------------
    Public Function CreManReceiptN() As Boolean
        '--------------------------------------------------------------------------------
        Dim lrecinsManreceipt As eRemoteDB.Execute

        On Error GoTo CreManReceiptN_err

        sOut_moveme = " "

        lrecinsManreceipt = New eRemoteDB.Execute

        With lrecinsManreceipt
            .StoredProcedure = "insManreceiptN"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayFreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigReceipt", sOrigReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypExecute", sTypExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssueDat", dIssuedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecRelatedColl", nRecrelatedcoll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOnSeq", sOnSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDevReceipt", sDevReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOut_moveme", sOut_moveme, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            CreManReceiptN = .Run(False)

            sOut_moveme = .Parameters("sOut_moveme").Value
        End With

CreManReceiptN_err:
        lrecinsManreceipt = Nothing
    End Function

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mclsValPolicyTra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsValPolicyTra = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






