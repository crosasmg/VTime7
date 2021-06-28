Option Strict Off
Option Explicit On
Imports eFunctions.Extensions
Public Class Claim
    '%-------------------------------------------------------%'
    '% $Workfile:: Claim.cls                                $%'
    '% $Author:: Jrengifo                                   $%'
    '% $Date:: 10-01-13 12:00                               $%'
    '% $Revision:: 4                                        $%'
    '%-------------------------------------------------------%'

    '-Se define el tipo enumerado para identificar los estados del siniestro
    Public Enum Estatclaim
        eNull = 1
        eNegotiate = 2
        eAdjust = 3
        ePayProcess = 4
        ePay = 5
        eImcomplete = 6
        eRefuse = 7
        eWaitApproval = 8
    End Enum

    Private Enum eCaseAction
        clngCaseAdd = 1
        clngCaseDel = 2
        clngCaseUpd = 3
        clngClientCaseAdd = 4
        clngClientCaseDel = 5
    End Enum

    '-Se define la lista enumerada que contendra el estado del siniestro (Table135)
    Public Enum eClaimStatus
        clngCancelled = 1 'Anulado
        clngInProcess = 2 'En tramitación
        clngInAdjust = 3 'En ajuste
        clngInPayProcess = 4 'En proceso de pago
        clngPayd = 5 'Pagado
        clngInformationPending = 6 'Pendiente de información
        clngRejected = 7 'Rechazado
        clngApprovalPending = 8 'Pendiente de aprobación
    End Enum

    '-Se define la lista enumerada que contendra el valor que es permitido para un campo (Table60)
    Public Enum eRequire
        cstrNotAccepted = 1 'No aceptada
        cstrRequired = 2 'Requerido
        cstrOptional = 3 'Opcional
    End Enum

    '- Se definen las propiedades principales de la clase correspondientes a la tabla Claim
    '- El campo llave corresponde a nClaim

    'Column_name                           Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
    '------------------------------------- ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
    Public nClaim As Double 'int                            4      10    0     no                                  (n/a)                               (n/a)
    Public sCertype As String 'char                           1                  no                                  yes                                 no
    Public nCausecod As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public nPolicy As Double 'int                            4      10    0     no                                  (n/a)                               (n/a)
    Public sClaimTyp As String 'char                           1                  no                                  yes                                 no
    Public nBranch As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nCertif As Double 'int                            4      10    0     no                                  (n/a)                               (n/a)
    Public sClient As String 'char                           14                 no                                  yes                                 no
    Public sCoinsuri As String 'char                           1                  yes                                 yes                                 yes
    Public dCompdate As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public dDecladat As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public sIns_claim As String 'char                           12                 yes                                 yes                                 yes
    Public sLeadcial As String 'char                           12                 yes                                 yes                                 yes
    Public nLoc_cos_re As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public nLoc_out_am As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public nLoc_pay_am As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public nLoc_rec_am As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public nLoc_Reserv As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public sMailnumb As String 'char                           6                  yes                                 yes                                 yes
    Public nMovement As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public nNotenum As Integer 'int                            4      10    0     yes                                 (n/a)                               (n/a)
    Public nNullcode As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public dOccurdat As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public nOffice As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nOfficeAgen As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nAgency As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nOffice_own As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public nOffictra As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public dPrescdat As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public sPrinted As String 'char                           1                  yes                                 yes                                 yes
    Public sReinsuri As String 'char                           1                  yes                                 yes                                 yes
    Public dShow_date As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public sShow_statu As String 'char                           1                  yes                                 yes                                 yes
    Public sStaclaim As Estatclaim 'char                           1                  yes                                 yes                                 yes
    Public nUnaccode As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public nUsercode As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public nProduct As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nWaitCl_Code As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
    Public sNumForm As String 'char                           12                 yes                                 yes                                 yes
    Public nTax_amo As Double 'decimal                        7      14    2     yes                                 (n/a)                               (n/a)
    Public nImageNum As Integer 'int                            4      10    0     yes                                 (n/a)                               (n/a)
    Public sCess_npr As String 'char                           1                  yes                                 yes                                 yes
    Public sBranchDesc As String 'char                           30                 yes                                 yes                                 yes
    Public sProductDesc As String 'char                           30                 yes                                 yes                                 yes
    Public sCauseDesc As String 'char                           30                 yes                                 yes                                 yes
    Public sStatusDesc As String 'char                           30                 yes                                 yes                                 yes
    Public nPremium As Double 'decimal                                                                                                                    no                                  9           10    2     yes                                 (n/a)                               (n/a)
    Public nCapital As Double 'decimal                                                                                                                    no                                  9           12    0     yes                                 (n/a)                               (n/a)
    Public sCliename As String 'char                                                                                                                       no                                  40                      yes                                 yes                                 yes
    Public sClient2 As String 'char                           14                 no                                  yes                                  no
    Public dLimit_pay As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
    Public nOffice_pay As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nOfficeAgen_pay As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nAgency_pay As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nRelaship As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
    Public nBordereaux_cl As Integer 'int                            4      10    0     yes                                 (n/a)                               (n/a)

    '- Se definen las variable auxiliares
    Public nExistValues As Integer
    Public nProv As Integer
    Public sProvType As String
    Public nCase_num As Integer
    Public nBene_type As Integer
    Public nDeman_type As Integer
    Public nOpt_claityp As Integer
    Public nQClaims As Integer
    Public nTransaction As Integer
    Public nCurrency_an As Integer
    Public nOpt_curr As Integer
    Public sEffecdate As String
    Public nTot_locam As Double
    Public nTotal As Double
    Public nCashNum As Double

    Public sClaimind As String
    Public sTypeProcess As String

    Public sStaClaimDes As String
    Public sDemanTypeDesc As String
    Public sBene_type As String
    Public sClaimTypDes As String
    Public sDesClaimCause As String
    Public sIsLife As String
    Public sRecover_Typ As String
    Public nClaimCost As String
    Public sDigit As String
    Public dExpirdate As Date
    Public ldtmStartdate As Date
    Public sClaims As String

    Private mclsClaim_cases As Claim_cases

    '- Variables usadas para ejecutar el pago de un siniestro
    Public nTransac As Integer
    Public nRole As Integer
    Public nPayForm As Integer
    Public nPay_type As Integer
    Public nServ_Order As Double
    Public sCoinsuNet As String
    Public nInvoice As Double
    Public dPay_date As Date
    Public dValdate As Date
    Public nPay_curr As Integer
    Public nLoc_exchange As Double
    Public nLoc_tot_pay As Double
    Public nRequest_nu As Integer
    Public sCheque As String
    Public nConsec As Integer
    Public nAmount As Double
    Public nConcept As Integer
    Public sClientOP As String
    Public dDat_propos As Date
    Public sDescript As String
    Public dIssue_dat As Date
    Public dLedger_dat As Date
    Public sRequest_ty As String
    Public nSta_cheque As Integer
    Public dStat_date As Date
    Public nUser_sol As Integer
    Public nAcc_bank As Integer
    Public sInter_pay As String
    Public nAcc_type As Integer
    Public sAcco_num As String
    Public nBank_code As Integer
    Public nBk_agency As Integer
    Public sN_Aba As String
    Public sHour As String
    Public sOfficeDesc As String
    Public nAcc_bankDest As Integer
    Public sInAcco_num As String
    Public nIn_nBank_Code As Integer
    Public nTypeTrans As Integer
    Public nAmountDest As Double
    Public nDoc_type As Integer
    Public dBilldate As Date
    Public nCompany As Integer
    Public nOrig_curr As Integer
    Public nOrig_amount As Double
    Public sCessiCoi As String
    Public sInd_rei As String
    Public nBordereaux As Integer

    Public nRecuper As Double
    Public nSalvata As Double

    Public nDeductible_Method As Integer

    '- Variable utilizada para grabar el valor en tabla Cheque
    Public nAmountCheq As Double
    Public nAfect_amount As Double
    Public nExcent_amount As Double

    '- Variable utilizada para grabar el valor en tabla Cheque correspondiente al pago de siniestro por deposito
	Public sAccountHolder As String  
	Public nBankExt As Integer 
	Public sBankAccount As String   
    Public nExternal_Concept As Integer 
	
    '- Variables usadas para el manejo de la consulta de operaciones de un sinestro
    Public sOper_TypeDesc As String
    Public nOper_type As Integer
    Public sCurrencyDesc As String
    Public dOperdate As Date
    Public nInc_amount As Double
    Public sPolitype As String

    '- Variable para saber si el arreglo contiene información
    Private mblnCharge As Boolean

    '- Variable para el tipo de lista
    Private mTypeList As eFunctions.Values.ecbeTypeList

    Public nReserve As Double
    Public nPay_amount As Double
    Public nCurrency As Integer
    Public nIdCatas As Integer
    Public nClaimParent As Long
    Public nTypesupport As Integer


    '- Tipo registro
    Private Structure udtClaimIns
        Dim nClaim As Double
        Dim sClient As String
        Dim dOccurdat As Date
        Dim sStaclaim As String
        Dim sStaClaimDes As String
        Dim sClaimTypDes As String
        Dim sDesClaimCause As String
        Dim sIsLife As String
    End Structure

    '- Tipo registro
    Private Structure udtClaimBordereaux
        Dim nClaim As Double
        Dim nBordereaux_cl As Integer
        Dim dDecladat As Date
        Dim sStaclaim As String
        Dim nCurrency As Integer
        Dim nReserve As Double
        Dim nLoc_Reserv As Double
    End Structure

    '- Tipo registro
    Private Structure udtClaimCli
        Dim nClaim As Double
        Dim sClient As String
        Dim dOccurdat As Date
        Dim sDesClaimCause As String
        Dim nBranch As Integer
        Dim nProduct As Integer
        Dim nPolicy As Double
        Dim nCertif As Double
    End Structure

    '- Arreglo que se emplea para cargar los siniestros de un cliente
    Private arrClaimIns() As udtClaimIns

    '- Arreglo que se emplea para cargar los siniestros asociados a una relación
    Private arrClaimBordereaux() As udtClaimBordereaux

    '- Arreglo que se emplea para cargar los siniestros de un cliente
    Private arrClaimCli() As udtClaimCli

    '- Variable pública utilizada como indicador de vigencia de la póliza.
    Public bPolicyVigency As Boolean
    Public dOccurdate_l As Date

    '%Add: Agrega un registro en la tabla Claim
    Public Function Add() As Boolean
        Dim lrecinsClaim As eRemoteDB.Execute

        On Error GoTo Add_err

        lrecinsClaim = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insClaim'
        'Información leída el 20/09/1999 08:26:29 AM

        With lrecinsClaim
            .StoredProcedure = "insClaim"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient2", sClient2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimtyp", sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoinsuri", sCoinsuri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDecladat", dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIns_claim", sIns_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadcial", sLeadcial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_cos_re", nLoc_cos_re, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_out_am", nLoc_out_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_pay_am", nLoc_pay_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_rec_am", nLoc_rec_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_reserv", nLoc_Reserv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMailnumb", sMailnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdat", dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_own", nOffice_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffictra", nOffictra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrescdat", dPrescdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrinted", sPrinted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReinsuri", sReinsuri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dShow_date", dShow_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShow_statu", sShow_statu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaClaim", sStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnaccode", nUnaccode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitCl_Code", nWaitCl_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nImageNum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCess_npr", sCess_npr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLimit_pay", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_pay", IIf(nOffice_pay = 0, eRemoteDB.Constants.intNull, nOffice_pay), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelaship", IIf(nRelaship = 0, eRemoteDB.Constants.intNull, nRelaship), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux_cl", IIf(nBordereaux_cl = 0, eRemoteDB.Constants.intNull, nBordereaux_cl), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        lrecinsClaim = Nothing
    End Function

    '%Find: Busca los datos del siniestro en la tabla Claim a partir del número de siniestro dado
    Public Function Find(ByVal llngnClaim As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaClaim_1 As eRemoteDB.Execute
        Dim lstrValue As String

        On Error GoTo Find_Err

        If llngnClaim <> nClaim Or lblnFind Then
            mclsClaim_cases = Nothing
            lrecreaClaim_1 = New eRemoteDB.Execute

            'Definición de parámetros para stored procedure 'insudb.reaClaim_1'
            'Información leída el 20/09/1999 08:02:03 AM

            With lrecreaClaim_1
                .StoredProcedure = "reaClaim_1"
                .Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nClaim = .FieldToClass("nClaim")
                    sCertype = .FieldToClass("sCertype")
                    nCausecod = .FieldToClass("nCausecod")
                    nPolicy = .FieldToClass("nPolicy")
                    sClaimTyp = .FieldToClass("sClaimtyp")
                    nBranch = .FieldToClass("nBranch")
                    nCertif = .FieldToClass("nCertif")
                    sClient = .FieldToClass("sClient")
                    sCoinsuri = .FieldToClass("sCoinsuri")
                    dDecladat = .FieldToClass("dDecladat")
                    sIns_claim = .FieldToClass("sIns_claim")
                    sLeadcial = .FieldToClass("sLeadcial")
                    nLoc_cos_re = .FieldToClass("nLoc_cos_re")
                    nLoc_out_am = .FieldToClass("nLoc_out_am")
                    nLoc_pay_am = .FieldToClass("nLoc_pay_am")
                    nLoc_rec_am = .FieldToClass("nLoc_rec_am")
                    nLoc_Reserv = .FieldToClass("nLoc_reserv")
                    sMailnumb = .FieldToClass("sMailnumb")
                    nMovement = .FieldToClass("nMovement")
                    nNotenum = .FieldToClass("nNotenum")
                    nNullcode = .FieldToClass("nNullcode")
                    dOccurdat = .FieldToClass("dOccurdat")
                    If .FieldToClass("dOccurdat") <> eRemoteDB.Constants.dtmNull Then
                        sHour = FormatDateTime(.FieldToClass("dOccurdat"), 4)
                    Else
                        sHour = "00:00"
                    End If
                    nOffice = .FieldToClass("nOffice")
                    nOfficeAgen = .FieldToClass("nOfficeAgen")
                    nAgency = .FieldToClass("nAgency")
                    nOffice_own = .FieldToClass("nOffice_own")
                    nOffictra = .FieldToClass("nOffictra")
                    dPrescdat = .FieldToClass("dPrescdat")
                    sPrinted = .FieldToClass("sPrinted")
                    sReinsuri = .FieldToClass("sReinsuri")
                    dShow_date = .FieldToClass("dShow_date")
                    sShow_statu = .FieldToClass("sShow_statu")
                    lstrValue = .FieldToClass("sStaclaim")
                    If lstrValue = String.Empty Then
                        lstrValue = CStr(0)
                    End If
                    sStaclaim = CShort(lstrValue)
                    nUnaccode = .FieldToClass("nUnaccode")
                    nProduct = .FieldToClass("nProduct")
                    nWaitCl_Code = .FieldToClass("nWaitCl_Code", 0)
                    sNumForm = .FieldToClass("sNumForm")
                    nImageNum = .FieldToClass("nImageNum")
                    sCess_npr = .FieldToClass("sCess_npr")
                    sClient2 = .FieldToClass("sClient2")
                    dLimit_pay = .FieldToClass("dLimit_pay")
                    nOffice_pay = .FieldToClass("nOffice_pay")
                    nOfficeAgen_pay = .FieldToClass("nOfficeAgen_pay")
                    nAgency_pay = .FieldToClass("nAgency_pay")
                    nRelaship = .FieldToClass("nRelaship")
                    nBordereaux_cl = .FieldToClass("nBordereaux_cl")
                    nIdCatas = .FieldToClass("nIdCatas")
                    nClaimParent = .FieldToClass("nClaimParent")

                    If .FieldToClass("sClient") <> String.Empty Then
                        sDigit = .FieldToClass("sDigit")
                        If sDigit = String.Empty Then
                            sDigit = CalcDigit(.FieldToClass("sClient"))
                        End If
                    End If
                    Find = True
                    .RCloseRec()
                Else
                    Find = False
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
        lrecreaClaim_1 = Nothing
    End Function

    '% FindClientClaim: Busca los datos del siniestro en la tabla Claim a partir del número de siniestro dado
    Public Function FindClientClaim(ByVal strClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaClaim As eRemoteDB.Execute

        '- Se declara la variable que determina el resultado de la funcion (True/False)

        Static lblnRead As Boolean

        On Error GoTo FindClientClaim_err

        lrecreaClaim = New eRemoteDB.Execute

        If sClient <> strClient Or lblnFind Then

            sClient = strClient

            '+ Definición de parámetros para stored procedure 'insudb.reaClaim'
            '+ Información leída el 03/11/2000 11:37:01 AM

            With lrecreaClaim
                .StoredProcedure = "reaClaim"
                .Parameters.Add("sClient", strClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    nClaim = .FieldToClass("nClaim")
                    .RCloseRec()
                    lblnRead = True
                Else
                    lblnRead = False
                End If
            End With
        End If

        FindClientClaim = lblnRead

FindClientClaim_err:
        If Err.Number Then
            FindClientClaim = False
        End If
        On Error GoTo 0
        lrecreaClaim = Nothing
    End Function

    '% ValClaimRequest: Busca los datos del siniestro en la tabla Claim
    Public Function ValClaimRequest(ByVal llngnClaim As Double, ByVal lstrsRequest As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim llngExists As Integer
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lrecreaClaim
        Dim lrecreaClaimRequest As eRemoteDB.Execute

        On Error GoTo ValClaimRequest_err

        lrecreaClaimRequest = New eRemoteDB.Execute
        If nClaim <> llngnClaim Or sNumForm <> lstrsRequest Or lblnFind Then

            sNumForm = lstrsRequest
            nClaim = llngnClaim

            'Definición de parámetros para stored procedure 'insudb.reaClaimRequest'
            'Información leída el 15/01/2001 10.42.24
            With lrecreaClaimRequest
                .StoredProcedure = "reaClaimRequest"
                .Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sNumForm", lstrsRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)
                ValClaimRequest = .Parameters("nExists").Value = 1
            End With
        Else
            ValClaimRequest = True
        End If

ValClaimRequest_err:
        If Err.Number Then
            ValClaimRequest = False
        End If
        On Error GoTo 0
        lrecreaClaimRequest = Nothing
    End Function

    '%Update: Actualiza todos los campos de un registro en la tabla Claim
    Public Function Update() As Boolean
        Dim lrecinsClaim As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecinsClaim = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insClaim'
        'Información leída el 20/09/1999 08:39:08 AM

        With lrecinsClaim
            .StoredProcedure = "insClaim"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient2", sClient2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimtyp", IIf(sClaimTyp = String.Empty, "3", sClaimTyp), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoinsuri", sCoinsuri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDecladat", dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIns_claim", sIns_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadcial", sLeadcial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_cos_re", nLoc_cos_re, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_out_am", nLoc_out_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_pay_am", nLoc_pay_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_rec_am", nLoc_rec_am, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_reserv", nLoc_Reserv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMailnumb", sMailnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdat", dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_own", nOffice_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffictra", nOffictra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrescdat", dPrescdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrinted", sPrinted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReinsuri", sReinsuri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dShow_date", dShow_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShow_statu", sShow_statu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaClaim", sStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnaccode", nUnaccode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitCl_Code", nWaitCl_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nImageNum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCess_npr", sCess_npr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLimit_pay", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_pay", nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelaship", IIf(nRelaship = 0, eRemoteDB.Constants.intNull, nRelaship), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux_cl", IIf(nBordereaux_cl = 0, eRemoteDB.Constants.intNull, nBordereaux_cl), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdCatas", nIdCatas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecinsClaim = Nothing
    End Function

    '%Delete: Elimina un registro en la tabla Claim, tomando como clave el número de siniestro
    Public Function Delete(ByVal llngnClaim As Double) As Boolean
        Dim lrecdelClaim As eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.delClaim'
        'Información leída el 20/09/1999 08:44:32 AM

        On Error GoTo Delete_Err

        lrecdelClaim = New eRemoteDB.Execute

        With lrecdelClaim
            .StoredProcedure = "delClaim"
            .Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Delete = True
            End If
        End With
Delete_Err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        lrecdelClaim = Nothing

    End Function

    '%FindControl: Busca los datos del siniestro en la tabla Claim a partir del número de siniestro dado
    Public Function FindControl(ByVal llngnClaim As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaClaim_1 As eRemoteDB.Execute
        Dim lstrValue As String

        On Error GoTo FindControl_err

        If llngnClaim <> nClaim Or lblnFind Then
            lrecreaClaim_1 = New eRemoteDB.Execute

            '+ Definición de parámetros para stored procedure 'insudb.reaClaim_1'
            '+ Información leída el 20/09/1999 08:02:03 AM

            With lrecreaClaim_1
                .StoredProcedure = "reaClaim_o"
                .Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nClaim = .FieldToClass("nClaim")
                    sCertype = .FieldToClass("sCertype")
                    nCausecod = .FieldToClass("nCausecod")
                    nPolicy = .FieldToClass("nPolicy")
                    sClaimTyp = .FieldToClass("sClaimtyp")
                    nBranch = .FieldToClass("nBranch")
                    nCertif = .FieldToClass("nCertif")
                    sClient = .FieldToClass("sClient")
                    sCoinsuri = .FieldToClass("sCoinsuri")
                    dDecladat = .FieldToClass("dDecladat")
                    sIns_claim = .FieldToClass("sIns_claim")
                    sLeadcial = .FieldToClass("sLeadcial")
                    nLoc_cos_re = .FieldToClass("nLoc_cos_re")
                    nLoc_out_am = .FieldToClass("nLoc_out_am")
                    nLoc_pay_am = .FieldToClass("nLoc_pay_am")
                    nLoc_rec_am = .FieldToClass("nLoc_rec_am")
                    nLoc_Reserv = .FieldToClass("nLoc_reserv")
                    sMailnumb = .FieldToClass("sMailnumb")
                    nMovement = .FieldToClass("nMovement")
                    nNotenum = .FieldToClass("nNotenum")
                    nNullcode = .FieldToClass("nNullcode")
                    dOccurdat = .FieldToClass("dOccurdat")
                    nOffice = .FieldToClass("nOffice")
                    nOffice_own = .FieldToClass("nOffice_own")
                    nOffictra = .FieldToClass("nOffictra")
                    dPrescdat = .FieldToClass("dPrescdat")
                    sPrinted = .FieldToClass("sPrinted")
                    sReinsuri = .FieldToClass("sReinsuri")
                    dShow_date = .FieldToClass("dShow_date")
                    sShow_statu = .FieldToClass("sShow_statu")
                    sStaclaim = .FieldToClass("sStaclaim", 0)
                    nUnaccode = .FieldToClass("nUnaccode")
                    nProduct = .FieldToClass("nProduct")
                    nWaitCl_Code = .FieldToClass("nWaitCl_Code")
                    sNumForm = .FieldToClass("sNumForm")
                    nImageNum = .FieldToClass("nImageNum")
                    sCess_npr = .FieldToClass("sCess_npr")
                    sBranchDesc = .FieldToClass("sBranchDesc")
                    sProductDesc = .FieldToClass("sProductDesc")
                    sCauseDesc = .FieldToClass("sCauseDesc")
                    sStatusDesc = .FieldToClass("sStatusDesc")
                    nPremium = .FieldToClass("nPremium")
                    nCapital = .FieldToClass("nCapital")
                    sCliename = .FieldToClass("sCliename")
                    sClient2 = .FieldToClass("sClient2")
                    nOffice_pay = .FieldToClass("nOffice_pay")
                    nOfficeAgen_pay = .FieldToClass("nOfficeAgen_pay")
                    nAgency_pay = .FieldToClass("nAgency_pay")
                    nIdCatas = .FieldToClass("nIdCatas")
                    FindControl = True
                    .RCloseRec()
                Else
                    FindControl = False
                End If
            End With
        Else
            FindControl = True
        End If

FindControl_err:
        If Err.Number Then
            FindControl = False
        End If
        On Error GoTo 0
        lrecreaClaim_1 = Nothing
    End Function

    '%Find_Provider: Comprueba la existencia de  un proveedor,  bien sea en ClaimBenef o en Claim_Attm
    Public Function Find_Provider(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal sProvType As String, ByVal nProv As Integer, ByVal nExistValues As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaClaimProvider As eRemoteDB.Execute

        On Error GoTo Find_Provider_Err

        lrecreaClaimProvider = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.sProvType <> sProvType Or Me.nProv <> nProv Or Me.nExistValues <> nExistValues Or lblnFind Then

            Me.sCertype = sCertype
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nPolicy = nPolicy
            Me.nCertif = nCertif
            Me.sProvType = sProvType
            Me.nProv = nProv
            Me.nExistValues = nExistValues

            'Definición de parámetros para stored procedure 'insudb.reaClaimProvider'
            'Información leída el 02/01/2001 08:45:39

            With lrecreaClaimProvider
                .StoredProcedure = "reaClaimProvider"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sProvType", sProvType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProv", nProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nExistValues", nExistValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run(False) Then
                    Me.nExistValues = .Parameters.Item("nExistValues").Value
                    Find_Provider = True
                Else
                    Find_Provider = False
                End If
            End With
        Else
            Find_Provider = True
        End If


Find_Provider_Err:
        If Err.Number Then
            Find_Provider = False
        End If
        On Error GoTo 0
        lrecreaClaimProvider = Nothing
    End Function

    '% Class_Initialize: se controla la creación de la clase
    Private Sub Class_Initialize_Renamed()
        Call InitClass()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% InitClass: se inicializan las variables públicas de la clase
    Public Sub InitClass()
        nClaim = eRemoteDB.Constants.intNull
        sCertype = CStr(eRemoteDB.Constants.strNull)
        nCausecod = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        sClaimTyp = CStr(eRemoteDB.Constants.strNull)
        nBranch = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        sClient = CStr(eRemoteDB.Constants.strNull)
        sCoinsuri = CStr(eRemoteDB.Constants.strNull)
        dDecladat = eRemoteDB.Constants.dtmNull
        sIns_claim = CStr(eRemoteDB.Constants.strNull)
        sLeadcial = CStr(eRemoteDB.Constants.strNull)
        nLoc_cos_re = eRemoteDB.Constants.intNull
        nLoc_out_am = eRemoteDB.Constants.intNull
        nLoc_pay_am = eRemoteDB.Constants.intNull
        nLoc_rec_am = eRemoteDB.Constants.intNull
        nLoc_Reserv = eRemoteDB.Constants.intNull
        sMailnumb = CStr(eRemoteDB.Constants.strNull)
        nMovement = eRemoteDB.Constants.intNull
        nNotenum = eRemoteDB.Constants.intNull
        nNullcode = eRemoteDB.Constants.intNull
        dOccurdat = eRemoteDB.Constants.dtmNull
        nOffice = eRemoteDB.Constants.intNull
        nOfficeAgen = eRemoteDB.Constants.intNull
        nAgency = eRemoteDB.Constants.intNull
        nOffice_own = eRemoteDB.Constants.intNull
        nOffictra = eRemoteDB.Constants.intNull
        dPrescdat = eRemoteDB.Constants.dtmNull
        sPrinted = CStr(eRemoteDB.Constants.strNull)
        sReinsuri = CStr(eRemoteDB.Constants.strNull)
        dShow_date = eRemoteDB.Constants.dtmNull
        sShow_statu = CStr(eRemoteDB.Constants.strNull)
        nUnaccode = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nWaitCl_Code = eRemoteDB.Constants.intNull
        sNumForm = CStr(eRemoteDB.Constants.strNull)
        nImageNum = eRemoteDB.Constants.intNull
        sCess_npr = CStr(eRemoteDB.Constants.strNull)
        sBranchDesc = CStr(eRemoteDB.Constants.strNull)
        sProductDesc = CStr(eRemoteDB.Constants.strNull)
        sCauseDesc = CStr(eRemoteDB.Constants.strNull)
        sStatusDesc = CStr(eRemoteDB.Constants.strNull)
        nPremium = eRemoteDB.Constants.intNull
        nCapital = eRemoteDB.Constants.intNull
        sCliename = CStr(eRemoteDB.Constants.strNull)
        nIdCatas = eRemoteDB.Constants.intNull
        bPolicyVigency = False
    End Sub

    '%ValQClaimsDay: Comprueba la existencia de otros siniestros declarados para la fecha
    Public Function ValQClaimsDay(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dCurDate As Date, ByVal nClaim As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaQClaimsDay As eRemoteDB.Execute
        Static lintOldBranch As Integer
        Static lintOldProduct As Integer
        Static llngOldPolicy As Integer
        Static llngOldCertif As Integer
        Static ldatOldCurDate As Date
        Static llngOldClaim As Integer

        On Error GoTo ValQClaimsDay_err

        lrecreaQClaimsDay = New eRemoteDB.Execute

        If lintOldBranch <> nBranch Or lintOldProduct <> nProduct Or llngOldPolicy <> nPolicy Or llngOldCertif <> nCertif Or llngOldClaim <> nClaim Or lblnFind Then

            lintOldBranch = nBranch
            lintOldProduct = nProduct
            llngOldPolicy = nPolicy
            llngOldCertif = nCertif
            llngOldClaim = nClaim

            'Definición de parámetros para stored procedure 'insudb.reaQClaimsDay'
            'Información leída el 16/01/2001 15.12.28

            With lrecreaQClaimsDay
                .StoredProcedure = "reaQClaimsDay"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dCurDate", dCurDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nQClaims = .FieldToClass("nQClaims", 0)
                    ValQClaimsDay = True
                    .RCloseRec()
                Else
                    ValQClaimsDay = False
                End If
            End With
        Else
            ValQClaimsDay = True
        End If


ValQClaimsDay_err:
        If Err.Number Then
            ValQClaimsDay = False
        End If
        On Error GoTo 0
        lrecreaQClaimsDay = Nothing
    End Function

    '% UpdateStatus: actualiza el estado del siniestro
    Public Function UpdateStatus(ByVal nClaim As Double, ByVal sStatus As String) As Boolean
        Dim lrecUpdStatusClaim As eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.UpdStatusClaim'
        'Información leída el 17/01/2001 2:10:50 PM

        On Error GoTo UpdateStatus_err

        lrecUpdStatusClaim = New eRemoteDB.Execute

        With lrecUpdStatusClaim
            .StoredProcedure = "UpdStatusClaim"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateStatus = .Run(False)
        End With

UpdateStatus_err:
        If Err.Number Then
            UpdateStatus = False
        End If
        On Error GoTo 0
        lrecUpdStatusClaim = Nothing
    End Function

    '% Update_SI007Total:
    Public Function Update_SI007Total() As Boolean
        Dim lrecinsUpdSI007Total As eRemoteDB.Execute

        On Error GoTo Update_SI007Total_err

        lrecinsUpdSI007Total = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insUpdSI007Total'
        'Información leída el 17/01/2001 3:37:59 PM

        With lrecinsUpdSI007Total
            .StoredProcedure = "insUpdSI007Total"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOpt_claityp", nOpt_claityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency_an", nCurrency_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOpt_curr", nOpt_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sEffecdate", sEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTot_locam", nTot_locam, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntotal", nTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_SI007Total = .Run(False)
        End With

Update_SI007Total_err:
        If Err.Number Then
            Update_SI007Total = False
        End If
        On Error GoTo 0
        lrecinsUpdSI007Total = Nothing
    End Function

    '% insValSI001: Esta función se encarga de validar los datos introducidos en la cabecera de la
    '%              forma.
    Public Function insValSI001(ByVal nTransactio As Integer, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertificat As Integer, ByVal sRequest_nu As String, ByVal dLedgerDate As Date, ByVal nReference As Integer, ByVal nCompany As Integer, ByVal nUsercode As Integer, ByVal dOccurdate As Date) As String
        Dim lstrErrorAll As String = ""
        Dim lintValue As Short
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalSI001 As eRemoteDB.Execute

        On Error GoTo insvalSI001_Err

        lrecinsvalSI001 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalSI001
            .StoredProcedure = "insSI001PKG.insvalSI001"
            .Parameters.Add("nTransaction", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertificat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequest_nu", sRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdate", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdate", dOccurdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolVigency", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdate_l", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
                dOccurdate_l = .Parameters("dOccurdate_l").Value
                bPolicyVigency = .Parameters("nPolVigency").Value = 1
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("SI001", , , , , , lstrErrorAll)
            End If
            insValSI001 = .Confirm
        End With

insvalSI001_Err:
        If Err.Number Then
            insValSI001 = "insvalSI001: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lrecinsvalSI001 = Nothing
    End Function

    '%insExpirDate: Calcula fecha de expiración de la póliza certificado
    Public Function insExpirDate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal dStartdate As Date) As Boolean
        Dim lrecCertificnn As eRemoteDB.Execute

        On Error GoTo insExpirDate_Err

        lrecCertificnn = New eRemoteDB.Execute

        With lrecCertificnn
            .StoredProcedure = "reaCertificnn"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.ldtmStartdate = .FieldToClass("dStartdate")
                Me.dExpirdate = .FieldToClass("dExpirdat")
            End If
            .RCloseRec()
        End With

insExpirDate_Err:
        If Err.Number Then
            insExpirDate = False
        End If

        On Error GoTo 0
        lrecCertificnn = Nothing
    End Function

    '% insPostSI001: Esta función se encarga de grabar/actualizar los datos introducidos en la
    '%               zona de del Header (datos de la clave) del siniestro (SI001)
    Public Function insPostSI001(ByVal nTransactio As Integer, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal sRequest_nu As String, ByVal dLedgerDate As Date, ByVal nReference As Integer, ByVal nUsercode As Integer, ByVal dOccurdate As Date, ByVal nIdCatas As Integer) As Boolean
        Dim lrecSI001 As eRemoteDB.Execute
        Dim lintValid As Short


        lrecSI001 = New eRemoteDB.Execute

        With lrecSI001
            .StoredProcedure = "insSI001PKG.insPostSI001"
            .Parameters.Add("nTransaction", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequest_nu", sRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdate", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdate", dOccurdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValid", lintValid, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdCatas", nIdCatas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insPostSI001 = .Parameters("nValid").Value = 1
            Me.nClaim = .Parameters("nClaim").Value
        End With

    End Function

    '% updClaim_his: Esta rutina se encarga de actualizar la tabla Claim_his
    Private Function updClaim_his(ByVal nClaim As Double, ByVal dEffecdate As Date, ByVal dLedgerDate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lclsClaimHis As Claim_his

        On Error GoTo updClaim_his_err

        lclsClaimHis = New Claim_his

        With lclsClaimHis
            .nClaim = nClaim
            .dOperdate = dEffecdate
            .dPosted = dLedgerDate
            .nUserCode = nUsercode
            updClaim_his = .Update_dates
        End With

updClaim_his_err:
        If Err.Number Then
            updClaim_his = False
        End If
        On Error GoTo 0
        lclsClaimHis = Nothing
    End Function

    '% insClaimNumber: Esta rutina se encarga de actualizar la tabla Claim_his
    Public Function calClaimNumber(ByVal nBranch As Integer, ByVal nCompany As Integer, ByVal nUsercode As Integer) As String
        Dim ldblClaim As Double
        Dim lclsErrors As eFunctions.Errors
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsLedCompan As Object
        Dim nYearAux As Short

        On Error GoTo calClaimNumber_err

        lclsErrors = New eFunctions.Errors
        lclsGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
        lclsLedCompan = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_compan")

        calClaimNumber = String.Empty

        '+ Se hace el llamado a la rutina que obtiene el año contable
        Call lclsLedCompan.Find(nCompany)

        nYearAux = IIf(Year(lclsLedCompan.dDate_end) = eRemoteDB.Constants.intNull, 0, Year(lclsLedCompan.dDate_end) - 2000)

        ldblClaim = lclsGeneral.Find_Numerator(7, nBranch, nUsercode, , nBranch, , , , , , , , nYearAux)
        If ldblClaim = -1 Then
            calClaimNumber = lclsErrors.ErrorMessage("SI001", 99041, , , , True)
        Else
            calClaimNumber = CStr(ldblClaim)
        End If

calClaimNumber_err:
        If Err.Number Then
            calClaimNumber = "calClaimNumber: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsGeneral = Nothing
        lclsLedCompan = Nothing

    End Function

    '% Update_CertifClaim:
    Public Function Update_CertifClaim() As Boolean
        Dim lrecUpdCertifClaim As eRemoteDB.Execute

        On Error GoTo Update_CertifClaim_Err

        lrecUpdCertifClaim = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updCertifClaim'
        '+ Información leída el 22/01/2001 10.08.21

        With lrecUpdCertifClaim
            .StoredProcedure = "updCertifClaim"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimind", sClaimind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_CertifClaim = .Run(False)
        End With

Update_CertifClaim_Err:
        If Err.Number Then
            Update_CertifClaim = False
        End If
        On Error GoTo 0
        lrecUpdCertifClaim = Nothing
    End Function

    Public ReadOnly Property Claim_cases() As Claim_cases
        Get
            If mclsClaim_cases Is Nothing Then
                mclsClaim_cases = New Claim_cases
            End If
            Call mclsClaim_cases.Find(nClaim, sTypeProcess)
            Claim_cases = mclsClaim_cases
        End Get
    End Property

    '% sReservstat:
    Public ReadOnly Property sReservstat() As String


        Get
            Dim lrecinsSreservstat As eRemoteDB.Execute
            Dim varAux As String = ""

            Try

                lrecinsSreservstat = New eRemoteDB.Execute

                '+ Definición de store procedure insSreservstat al 07-15-2003 13:20:54

                With lrecinsSreservstat

                    .StoredProcedure = "insSreservstat"
                    .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sReservstat", varAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Run(False)
                    varAux = .Parameters("sReservstat").Value
                End With
                Return lrecinsSreservstat

            Catch ex As Exception
                lrecinsSreservstat = Nothing
                Return lrecinsSreservstat
            End Try
        End Get
    End Property

    '% ValReserve : Valida que el usuario tiene nivel de autorización. Lee las coberturas
    '% del siniestro y el acumulado por tipo de moneda no debe exceder del límite permitido
    '% para el usuario.
    Public ReadOnly Property ValReserve() As Boolean
        Get
            Dim lrecreaCl_coverReserve As eRemoteDB.Execute

            On Error GoTo ValReserve_err

            lrecreaCl_coverReserve = New eRemoteDB.Execute

            'Definición de parámetros para stored procedure 'insudb.reaCl_coverReserve'
            'Información leída el 31/01/2001 15.25.13

            With lrecreaCl_coverReserve
                .StoredProcedure = "reaCl_coverReserve"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    If .FieldToClass("nAprobado") = "1" Then
                        ValReserve = True
                    Else
                        ValReserve = False
                    End If
                    .RCloseRec()
                Else
                    ValReserve = False
                End If
            End With

ValReserve_err:
            If Err.Number Then
                ValReserve = False
            End If
            On Error GoTo 0
            lrecreaCl_coverReserve = Nothing
        End Get
    End Property
    '% CountItemClaimIns: propiedad que indica el número de registros que se encuentra en
    '%                    el arreglo
    Public ReadOnly Property CountItemClaimIns() As Integer
        Get
            If mblnCharge Then
                CountItemClaimIns = UBound(arrClaimIns)
            Else
                CountItemClaimIns = -1
            End If
        End Get
    End Property

    '% DTypeList: tipo de lista
    Public ReadOnly Property DTypeList() As eFunctions.Values.ecbeTypeList
        Get
            DTypeList = mTypeList
        End Get
    End Property

    '% CountClaimBordereaux: propiedad que indica el número de registros que se encuentra en
    '%                    el arreglo
    Public ReadOnly Property CountClaimBordereaux() As Integer
        Get
            If mblnCharge Then
                CountClaimBordereaux = UBound(arrClaimBordereaux)
            Else
                CountClaimBordereaux = -1
            End If
        End Get
    End Property

    '% CountItemClaimCli: propiedad que indica el número de registros que se encuentra en
    '%                    el arreglo
    Public ReadOnly Property CountItemClaimCli() As Integer
        Get
            If mblnCharge Then
                CountItemClaimCli = UBound(arrClaimCli)
            Else
                CountItemClaimCli = -1
            End If
        End Get
    End Property

    '% bFullClaim:
    Public Function bFullClaim(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal sbrancht As String, ByVal sBussityp As String) As Boolean
        Dim lclsClaim_win As Claim_win
        Dim llngCount As Integer

        On Error GoTo bFullClaim_Err

        bFullClaim = True
        lclsClaim_win = New Claim_win

        With lclsClaim_win
            If .LoadTabsClaim(nTransaction, nClaim, sbrancht, sBussityp) Then
                For llngCount = 0 To .CountItem
                    Call .Item(llngCount)
                    ' Requerida y sin contenido
                    If .sRequire = "1" And .sContent = "1" Then
                        bFullClaim = False
                        Exit For
                    End If
                Next llngCount
            Else
                bFullClaim = False
            End If
        End With

bFullClaim_Err:
        If Err.Number Then
            bFullClaim = False
        End If
        On Error GoTo 0
        lclsClaim_win = Nothing

    End Function

    '%ValLimitsClaimDec: Función que valida el límite de declaración del siniestro de acuerdo a las coberturas definidas.
    Public Function ValLimitsClaimDec(ByVal sSche_code As String) As Boolean
        Dim lclsCl_cover As Cl_Cover
        Dim lclscl_covers As CL_Covers
        Dim lsecTime As eSecurity.Secur_sche
        Dim lblnExcess As Boolean

        On Error GoTo ValLimitsClaimDec_Err

        lsecTime = New eSecurity.Secur_sche
        lclsCl_cover = New Cl_Cover
        lclscl_covers = New CL_Covers

        ValLimitsClaimDec = True

        With lclscl_covers
            If lsecTime.Find(sSche_code, True) Then
                If .Find_ClaimReserve(nClaim) Then
                    For Each lclsCl_cover In lclscl_covers
                        If Not lsecTime.valLimits(eSecurity.Secur_sche.eTypeLimits.clngLimitsClaimDec, sSche_code, nBranch, (lclsCl_cover.nCurrency), (lclsCl_cover.nReserve), (lclsCl_cover.nProduct)) Then
                            lblnExcess = False
                            Exit For
                        End If
                    Next lclsCl_cover
                End If
            Else
                lblnExcess = False
            End If
        End With
        ValLimitsClaimDec = lblnExcess

ValLimitsClaimDec_Err:
        If Err.Number Then
            ValLimitsClaimDec = False
        End If
        On Error GoTo 0
        lclsCl_cover = Nothing
        lclscl_covers = Nothing
        lsecTime = Nothing

    End Function

	'%ValDocuments_Status: Función que valida que no haya documentos pendientes
	Public Function ValDocuments_Status(ByVal nClaim As Double) As Boolean
        Dim recreaDocuments As eRemoteDB.Execute
        Dim nExistPendDocuments As Integer
        Dim nSta_chequeaux As Integer

        On Error GoTo ReaDocuments_err

        recreaDocuments = New eRemoteDB.Execute

        With recreaDocuments
            .StoredProcedure = "ReaVal_Documents_Pend"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExistPendDocuments", nExistPendDocuments, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            nExistPendDocuments = .Parameters("nExistPendDocuments").Value

            If nExistPendDocuments = 1 Then
                ValDocuments_Status = False
            Else
                ValDocuments_Status = True
            End If
        End With
ReaDocuments_err:
        If Err.Number Then
            ValDocuments_Status = False
        End If
        On Error GoTo 0
    End Function


	
    ' Ejecuta toda la secuencia del post de la SI050
    Public Function insExecuteSI050(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal nWaitCode As Integer, ByVal blnWaitCode As Boolean, ByVal nReference As Integer, ByVal nUsercode As Integer, Optional ByVal nProdclas As Integer = 0, Optional ByVal nCause As Integer = 0, Optional ByVal nNotenum As Integer = 0) As Boolean
        Dim lrecinsExecutesi050 As eRemoteDB.Execute
        Dim lrecProf_ord As eClaim.Prof_ord
        Dim lrecProf_ords As eClaim.Prof_ords
        Dim lintCount As New Integer
        On Error GoTo insExecutesi050_Err

        lrecinsExecutesi050 = New eRemoteDB.Execute
        lrecProf_ord = New eClaim.Prof_ord
        lrecProf_ords = New eClaim.Prof_ords

        '+
        '+ Definición de store procedure insExecutesi050 al 07-15-2003 13:25:06
        '+
        With lrecinsExecutesi050
            .StoredProcedure = "insExecutesi050"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitcl_code", nWaitCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnablewaitcode", IIf(blnWaitCode = True, 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdClas", nProdclas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotemum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            insExecuteSI050 = .Parameters("nContent").Value = 1

            If lrecProf_ords.Find(nClaim) And nWaitCode = 0 Then
                For lintCount = 1 To lrecProf_ords.Count
                    lrecProf_ord = lrecProf_ords.Item(lintCount)
                    If (lrecProf_ord.nStatus_ord = "1" Or lrecProf_ord.nStatus_ord = "2") And lrecProf_ord.nOrdertype = 5 Then
                        lrecProf_ord.sendOrderToAudatex(lrecProf_ord.nServ_Order, lrecProf_ord.dFec_prog, nUsercode)
                    End If
                Next
            End If
        End With

insExecutesi050_Err:
        If Err.Number Then
            insExecuteSI050 = False
        End If
        lrecinsExecutesi050 = Nothing
        On Error GoTo 0

    End Function

    'insValSI006: esta función realiza las validaciones del frame
    Public Function insValSI006(ByVal cbonullclaim As Integer, ByVal sClaimTyp As String, ByVal nTransaction As Integer, Optional ByVal nCase_num As Integer = 0) As String
        Dim lerrTime As eFunctions.Errors

        On Error GoTo insValSI006_Err

        lerrTime = New eFunctions.Errors

        insValSI006 = String.Empty

        '+Si el proceso es diferente a "Desistimiento"
        If nTransaction <> 17 Then
            '+Validacion del CAMPO "Causa de Anulación"
            '+Este campo debe estar lleno
            If cbonullclaim = eRemoteDB.Constants.intNull Or cbonullclaim = 0 Then
                Call lerrTime.ErrorMessage("SI006", 4031)
            End If
        End If

        If nTransaction = 18 Then
            If nCase_num = eRemoteDB.Constants.intNull Or nCase_num = 0 Then
                Call lerrTime.ErrorMessage("SI006", 4310)
            End If
        End If

        '+Si el siniestro es "Pérdida total", se advierte que verifique las renovaciones que pudieron no haberse ejecutado
        '+a la póliza o certificado en tratamiento
        If sClaimTyp = "2" Then
            Call lerrTime.ErrorMessage("SI006", 4295)
        End If
        insValSI006 = lerrTime.Confirm

insValSI006_Err:
        If Err.Number Then
            insValSI006 = "insValSI006: " & Err.Description
        End If
        On Error GoTo 0
        lerrTime = Nothing

    End Function

    'insValSI004: Esta función realiza las validaciones de la ventana SI004
    Public Function insValSI004(ByVal sCodispl As String, ByVal sAction As String, ByVal nClaim As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nCaseNum As Double, ByVal sClientCode As String, ByVal nRole As Integer, ByVal dPrescdat As Date, ByVal dDecladat As Date, ByVal dLimit_pay As Date, ByVal dOccurrdat As Date, ByVal nCauseCode As Integer, ByVal nDeman_type As Integer, ByVal sbrancht As String, ByVal sLastName As String, ByVal sFirstName As String, ByVal bReclaimer As Boolean, ByVal nTransaction As Integer, ByVal bMassive As Boolean) As String
        Dim lstrErrorAll As String = ""
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalSI004 As eRemoteDB.Execute
        Dim sClientCodeColumnCaption As String
        Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("SI004", False)
        On Error GoTo insValSI004_err

        sClientCodeColumnCaption = resxValues.FindDictionaryValue("tctClientCodeColumnCaption")
        lrecinsvalSI004 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lrecinsvalSI004
            .StoredProcedure = "insSI004pkg.insvalSI004"
            .Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCasenum", nCaseNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientcode", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrescdat", dPrescdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDecladat", dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLimit_pay", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurrdat", dOccurrdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCausecode", nCauseCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReclaimer", IIf(bReclaimer, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMassive", IIf(bMassive, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientCodeColumnCaption", sClientCodeColumnCaption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCLAIMTYP", Me.sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", Me.nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then

                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)

            End If
            insValSI004 = .Confirm
        End With

insValSI004_err:
        If Err.Number Then
            insValSI004 = "insValSI004: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lrecinsvalSI004 = Nothing
    End Function

    '% insValAllRecla. Esta funcion se encarga de validar que todos los casos tengan al menos
    '% un reclamante asociado.
    Private Function insValAllRecla(ByVal lintCaseNumber As Integer, ByVal lintCasetype As Integer, ByVal lintClientCode As String, ByVal lblnReclaimer As Boolean) As Boolean
        Dim Claimcases As Object

        insValAllRecla = True

        Claimcases = Claim_cases
        If Claimcases.Count > 0 Then
            insValAllRecla = True
        Else
            insValAllRecla = False
        End If

        Claimcases = Nothing
    End Function

    '%insPostSI004: Registra en las tablas respectivas los siniestros y sus casos
    Public Function insPostSI004(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nId As Integer, ByVal sStaReserve As String, ByVal nNoteDama As Integer, ByVal sClaim_affe As String, ByVal sClient As String, ByVal sDigit As String, ByVal sDemandant As String, ByVal nBene_type As Integer, ByVal dOccurdat As Date, ByVal dPrescdat As Date, ByVal dLimit_pay As Date, ByVal nOffice_pay As Integer, ByVal nOfficeAgen_pay As Integer, ByVal nAgency_pay As Integer, ByVal nRelaship As Integer, ByVal nUsercode As Integer, ByVal nCausecod As Integer, ByVal sLastName As String, ByVal sLastName2 As String, ByVal sFirstName As String, Optional ByVal sAction As String = "", Optional ByVal sWindowType As String = "", Optional ByVal sClaimTyp As String = "", Optional ByVal blnMassiveClaim As Boolean = False, Optional ByVal nTransaction As Integer = 0, Optional ByVal nProdclas As Integer = 0, Optional ByVal nClaimParent As Integer = 0) As Boolean

        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo insPostSI004_Err

        lclsRemote = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lclsRemote
            .StoredProcedure = "insSI004pkg.inspostSI004"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCasenum", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStareserve", sStaReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotedama", nNoteDama, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaim_affe", sClaim_affe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientcode", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDemandant", sDemandant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBenetype", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdat", dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrescdat", dPrescdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLimit_pay", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice_pay", nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sWindowType", UCase(sWindowType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimtyp", sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMassiveClaim", IIf(blnMassiveClaim, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", nProdclas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaimParent", nClaimParent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostSI004 = .Run(False)
        End With

insPostSI004_Err:
        If Err.Number Then
            insPostSI004 = False
        End If
        On Error GoTo 0
        lclsRemote = Nothing
    End Function

    '% insUpdPolicy_his_ClaimOccurdat: se actualiza la fecha de efecto del movimiento en
    '%                                 Policy_his con la fecha de ocurrencia
    Private Sub insUpdPolicy_his_ClaimOccurdat(ByVal nUsercode As Integer)
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim lclsPolicy As ePolicy.Policy
        Dim ldtmOccurdat As String

        lclsPolicy_his = New ePolicy.Policy_his
        lclsPolicy = New ePolicy.Policy

        ldtmOccurdat = CStr(Me.dOccurdat)

        If lclsPolicy.Find(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy) Then
            With lclsPolicy_his
                .sCertype = Me.sCertype
                .nBranch = Me.nBranch
                .nProduct = Me.nProduct
                .nPolicy = Me.nPolicy
                .nCertif = lclsPolicy.nCertif
                .nMovement = lclsPolicy.nMov_histor ' + 1
                .dEffecdate = CDate(ldtmOccurdat)
                .nUsercode = nUsercode
                Call .Update_ClaimOccurdat()
            End With
        End If
        lclsPolicy_his = Nothing
        lclsPolicy = Nothing
    End Sub

    '%Pay: Se encarga de realizar los pagos de siniestros y la generacion respectiva de ordernes de
    '% pago o ingreso de dinero en caja.
    Public Function Pay() As Boolean
        Dim lrecinsClaim_pay As eRemoteDB.Execute

        On Error GoTo Pay_Err
        lrecinsClaim_pay = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insClaim_pay'
        'Información leída el 29/01/2001 6:26:35 PM
        With lrecinsClaim_pay
            .StoredProcedure = "insClaim_pay"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayForm", nPayForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoinsuNet", sCessiCoi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInvoice", nInvoice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPay_date", dPay_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_curr", nPay_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_exchange", nLoc_exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoc_tot_pay", nLoc_tot_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientOP", sClientOP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDat_propos", dDat_propos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssue_dat", dIssue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequest_ty", sRequest_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAcco_num", sAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNoteNum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sN_Aba", sN_Aba, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaclaim", sStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_nAcc_type", nAcc_bankDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_sAcco_num", sInAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_nBank_code", nIn_nBank_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("In_nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypetrans", nTypeTrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountDest", nAmountDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDoc_type", nDoc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBill_date", dBilldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_NCOMPANY", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_NORI_CURR", nOrig_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_NORI_AMOUNT", nOrig_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_rei", sInd_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValdate", dValdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPay_rent", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountCheq", nAmountCheq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfect_amount", nAfect_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExcent_amount", nExcent_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAccountHolder", sAccountHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAccount", sBankAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExternal_Concept", nExternal_Concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
            .Parameters.Add("nDeductible_Met", nDeductible_Method, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Pay = .Run(False)
            sStaclaim = .Parameters.Item("sStaclaim").Value
        End With

Pay_Err:
        If Err.Number Then
            Pay = False
        End If
        On Error GoTo 0
        lrecinsClaim_pay = Nothing
    End Function
    Public Function Update_SI006(ByVal sAction As String, ByVal nClaim As Double, ByVal nCausecod As Integer, ByVal sMailnumb As String, ByVal nUsercode As Integer, Optional ByVal nTransaction As Integer = 0, Optional ByVal nCase_num As Integer = 0, Optional ByVal nDeman_type As Integer = 0) As Boolean
        '---------------------------- ----------------------------------------------------------------
        Dim lrecinsUpdClaim_SI006 As eRemoteDB.Execute

        On Error GoTo Update_SI006_Err
        lrecinsUpdClaim_SI006 = New eRemoteDB.Execute
        'Definición de parámetros para stored procedure 'insudb.insUpdClaim_SI006'
        'Información leída el 30/01/2001 10.54.39

        With lrecinsUpdClaim_SI006
            .StoredProcedure = "insUpdClaim_SI006"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCauseCode", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMailnumb", sMailnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 10, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Me.nMovement = .Parameters("nMovement").Value
                Update_SI006 = True
            Else
                Update_SI006 = False
            End If

        End With

Update_SI006_Err:
        If Err.Number Then
            Update_SI006 = False
        End If
        On Error GoTo 0
        lrecinsUpdClaim_SI006 = Nothing
    End Function

    Public Function Update_SI010() As Boolean

        Dim lrecinsClaimSI010 As eRemoteDB.Execute
        Dim lclsbills = New eCollection.Bills
        Dim sKey_P As String = ""
        On Error GoTo Update_SI010_Err
        lrecinsClaimSI010 = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insClaimSI010'
        'Información leída el 31/01/2001 10.04.42
        With lrecinsClaimSI010
            .StoredProcedure = "insClaimSI010"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey_P", sKey_P, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update_SI010 = .Run(False)

            If Update_SI010 Then
                '+ se llama al WS de Invoice para Facturar la RASA
                ReaPremiumRasa(nClaim, nCase_num, nDeman_type, nMovement, nUsercode)
                If .Parameters.Item("sKey_P").Value <> String.Empty Then
                    'lclsbills.BillGenerateBulletins(.Parameters.Item("sKey_P").Value, 0, nUsercode)
                End If
            End If
        End With

Update_SI010_Err:
        If Err.Number Then
            Update_SI010 = False
        End If
        On Error GoTo 0
        lrecinsClaimSI010 = Nothing
    End Function

    '% Genera  la nota de credito de la rasa.
    Public Function ReaPremiumRasa(ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nMovement As Integer, ByVal nUsercode As Integer) As Boolean

        Dim lreaReaPremiumRasa As eRemoteDB.Execute
        Dim nCollector As Double
        Dim sClient As String
        Dim lreaBillGenerateService As eCollection.Bills
        lreaBillGenerateService = New eCollection.Bills

        nCollector = eRemoteDB.Constants.intNull
        sClient = String.Empty

        lreaReaPremiumRasa = New eRemoteDB.Execute

        On Error GoTo Find_Err

        'Definición de parámetros para stored procedure 'insudb.reaCollector'
        With lreaReaPremiumRasa
            .StoredProcedure = "ReaPremiumRasa"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    ' lreaBillGenerateService.BillGenerateService("1", .FieldToClass("nReceipt"), nUsercode, , .FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), , , "2")
                    .RNext()
                Loop
                .RCloseRec()
                ReaPremiumRasa = True
            Else
                ReaPremiumRasa = False
            End If
        End With

        'UPGRADE_NOTE: Object lreaCollectors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaReaPremiumRasa = Nothing

Find_Err:
        If Err.Number Then
            ReaPremiumRasa = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lreaCollectors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaBillGenerateService = Nothing
    End Function
    '% insPostSI006: Esta función se encarga de validar los datos introducidos en la zona de
    '%               cabecera de la SI006.
    Public Function insPostSI006(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal nUsercode As Integer, ByVal nCauseCode As Integer, ByVal sMailnumb As String, Optional ByVal nCase_num As Integer = 0, Optional ByVal nDeman_type As Integer = 0) As Boolean
        Dim ldtmEffecdate As Date
        Dim ldtmLedgerDate As Date
        Dim lintOffice As Integer
        Dim lblClaimUpd As Boolean
        Dim sAction As String = ""

        Dim lclsClaimWin As eClaim.Claim_win
        Dim lclsClaim As eClaim.Claim
        Dim lclsValues As eFunctions.Values
        Dim lclsClaim_case As Claim_case

        lclsClaim = New eClaim.Claim
        lclsClaim_case = New Claim_case
        lclsValues = New eFunctions.Values
        lclsClaimWin = New eClaim.Claim_win

        On Error GoTo insPostSI006_err

        insPostSI006 = True

        '+ Se asignan los valores necesarios para la actualización de los datos
        Me.nUsercode = nUsercode
        Me.nClaim = nClaim
        Me.nTransaction = nTransaction
        Me.nCausecod = nCauseCode
        Me.sMailnumb = sMailnumb

        If nTransaction = 15 Then
            sAction = CStr(1)
        ElseIf nTransaction = 7 Then
            sAction = CStr(2)
        ElseIf nTransaction = 17 Then
            sAction = CStr(3)
        End If
        Select Case nTransaction
            Case 15, 7, 17
                If lclsClaim.Update_SI006(sAction, nClaim, nCauseCode, sMailnumb, Me.nUsercode) Then
                    If lclsClaim_case.Update_Claim_case_sStaReserve_all(nClaim, IIf(nTransaction = 15, "7", "1"), Me.nUsercode) Then
                        insPostSI006 = True
                        Me.nMovement = lclsClaim.nMovement
                    End If
                End If
                Call lclsClaimWin.Add_Claim_win(nClaim, "SI006", "2", nUsercode)
            Case 18
                If lclsClaim.Update_SI006(sAction, nClaim, nCauseCode, sMailnumb, Me.nUsercode, nTransaction, nCase_num, nDeman_type) Then
                    If lclsClaim_case.UpdatesStareserve(nClaim, nDeman_type, nCase_num, "9", nUsercode) Then
                        insPostSI006 = True
                        Call lclsClaimWin.Add_Claim_win(nClaim, "SI006", "2", nUsercode)
                    End If
                End If
            Case Else
                insPostSI006 = False
        End Select

insPostSI006_err:
        If Err.Number Then
            insPostSI006 = False
        End If
        On Error GoTo 0
        lclsClaim = Nothing
        lclsClaim_case = Nothing
        lclsValues = Nothing
        lclsClaimWin = Nothing
    End Function

    '%insValSI051: Esta función se encarga de validar los datos introducidos en la zona de detalle para forma.
    Public Function insValSI051(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values

        On Error GoTo insValSI051_Err

        lclsErrors = New eFunctions.Errors

        '+Validacion del campo aprobar
        Me.nClaim = nClaim
        Me.nBranch = nBranch
        Me.nUsercode = nUsercode
        Me.nProduct = nProduct

        If Not ValReserve Then
            lclsValues = New eFunctions.Values
            lclsErrors.ErrorMessage(sCodispl, 12142, , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(262) & " " & nClaim & ":")
            lclsValues = Nothing
        End If

        insValSI051 = lclsErrors.Confirm

insValSI051_Err:
        If Err.Number Then
            insValSI051 = "insValSI051: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
    '%insValSI051_K: Esta función se encarga de validar los datos para filtrar consulta.
    Public Function insValSI051_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClaim As Double, ByVal nPolicy As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsLife_Claim As eClaim.Life_claim

        Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValSI051_K_Err

        lstrSep = "||"

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        '+ Debe venir siniestro o ramo-producto
        If nClaim = eRemoteDB.Constants.intNull And nBranch = eRemoteDB.Constants.intNull And nProduct = eRemoteDB.Constants.intNull Then
            lstrError = lstrError & lstrSep & "1022|0|0"
            lstrError = lstrError & lstrSep & "4006|0|0"
        Else
            'Esta validación se comenta porque la búsqueda también puede hacerse sólo por ramo 28/08/2015
            ''+ Si ingreso ramo,  entonces producto no debe ser null
            'If nBranch <> eRemoteDB.Constants.intNull And nProduct = eRemoteDB.Constants.intNull Then
            '    lstrError = lstrError & lstrSep & "1014|0|0"
            'Else
            If nClaim <> eRemoteDB.Constants.intNull Then
                '+ Valida que exista numero de siniestro
                If Not Find(nClaim) Then
                    lstrError = lstrError & lstrSep & "4005|0|0"
                Else
                    '+ Se realizan validaciones que requieren de varias lecturas de la BD.
                    lstrError = InsValSI051K_BD(sCodispl, nBranch, nProduct, nClaim, nPolicy)

                    If sStaclaim <> 8 Then
                        lstrError = lstrError & lstrSep & "4010|0|0"
                    End If

                End If

                ''+ Se realizan validaciones que requieren de varias lecturas de la BD
                'lstrError = InsValSI051K_BD(sCodispl, nBranch, nProduct, nClaim, nPolicy)

                ''If lstrError <> String.Empty Then
                ''    .ErrorMessage(sCodispl, , , , , , lstrError)
                ''End If

            End If

            If nPolicy <> eRemoteDB.Constants.intNull Then
                If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    lstrError = lstrError & lstrSep & "3001|0|0"
                End If
            End If
            'End If
        End If

        If lstrError <> String.Empty Then
            lstrError = Mid(lstrError, 3)
            With lclsErrors
                .ErrorMessage("SI051", , , , , , lstrError)
                insValSI051_K = .Confirm()
            End With
        End If

insValSI051_K_Err:
        If Err.Number Then
            insValSI051_K = "insValSI051_K: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lclsLife_Claim = Nothing
    End Function

    '%insPostSI051: Esta función se encarga de validar todos los datos introducidos en la forma SI051
    Public Function insPostSI051(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal sClaimTyp As String, ByVal nMovement As Integer, ByVal dOperdate As Date, ByVal sClient As String, ByVal nUsercode As Integer, ByVal sStatus As String) As Boolean
        Dim lrecinsPostSI051 As eRemoteDB.Execute

        On Error GoTo insPostSI051_Err

        lrecinsPostSI051 = New eRemoteDB.Execute

        With lrecinsPostSI051
            .StoredProcedure = "INSSI051PKG.INSPOSTSI051"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimtyp", sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostSI051 = (.Parameters("nStatus").Value = 1)
            Else
                insPostSI051 = False
            End If
        End With

insPostSI051_Err:
        If Err.Number Then
            insPostSI051 = False
        End If
        On Error GoTo 0
        lrecinsPostSI051 = Nothing
    End Function

    '% Deman_typeList: devuelve la lista de valores posibles para el campo Tipo de reclamo
    '%                 dependiendo del ramo-producto
    Public Function Deman_typeList(ByVal nBranch As Integer, ByVal nProduct As Integer) As String
        Dim lclsProdmaster As eProduct.Product

        On Error GoTo Deman_typeList_err

        lclsProdmaster = New eProduct.Product

        mTypeList = eFunctions.Values.ecbeTypeList.none

        If lclsProdmaster.FindProdMasterActive(nBranch, nProduct) Then
            '+ Sí no es automovíl, se excluye el tipo de caso Vehículo
            If lclsProdmaster.sBrancht <> eProduct.Product.pmBrancht.pmAuto Then
                Deman_typeList = "0,4"
                mTypeList = eFunctions.Values.ecbeTypeList.Exclution
            Else
                Deman_typeList = "1,3,4"
                mTypeList = eFunctions.Values.ecbeTypeList.Inclution
            End If

            '+Si se trata de un producto de vida, se permiten sólo reclamos de personas y de tipo salud
            If lclsProdmaster.sBrancht = eProduct.Product.pmBrancht.pmlife Then
                Call lclsProdmaster.FindProduct_li(nBranch, nProduct, Today)
                If lclsProdmaster.nProdClas = 13 Then
                    Deman_typeList = "1"
                    mTypeList = eFunctions.Values.ecbeTypeList.Inclution
                Else
                    Deman_typeList = "0,2,3,4"
                    mTypeList = eFunctions.Values.ecbeTypeList.Exclution
                End If

            End If
        End If

Deman_typeList_err:
        If Err.Number Then
            Deman_typeList = "Deman_typeList: " & Err.Description
        End If
        On Error GoTo 0
        lclsProdmaster = Nothing
    End Function

    '% FindClaimIns: busca los siniestros para un cliente en particular
    Public Function FindClaimIns(ByVal sClient As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
        Dim recreaClaimPol As eRemoteDB.Execute
        Dim lintIndex As Integer

        On Error GoTo FindClaimIns_err

        recreaClaimPol = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaTab_WinCla'
        '+ Información leída el 10/02/2000 15:35:36

        With recreaClaimPol
            .StoredProcedure = "reaClaimByIns"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintIndex = 0
                ReDim arrClaimIns(50)
                Do While Not .EOF
                    arrClaimIns(lintIndex).nClaim = .FieldToClass("nClaim")
                    arrClaimIns(lintIndex).sClient = .FieldToClass("sClient")
                    arrClaimIns(lintIndex).dOccurdat = .FieldToClass("dOccurdat")
                    arrClaimIns(lintIndex).sStaclaim = .FieldToClass("sStaClaim")
                    arrClaimIns(lintIndex).sStaClaimDes = .FieldToClass("sStaClaimDes")
                    arrClaimIns(lintIndex).sClaimTypDes = .FieldToClass("sClaimTypDes")
                    arrClaimIns(lintIndex).sDesClaimCause = .FieldToClass("sDesClaimCause")
                    arrClaimIns(lintIndex).sIsLife = .FieldToClass("sType")
                    .RNext()
                    lintIndex = lintIndex + 1
                Loop
                .RCloseRec()
                ReDim Preserve arrClaimIns(lintIndex - 1)
                mblnCharge = True
            Else
                mblnCharge = False
            End If
        End With

        FindClaimIns = mblnCharge

FindClaimIns_err:
        If Err.Number Then
            FindClaimIns = False
        End If
        On Error GoTo 0
        recreaClaimPol = Nothing
    End Function

    '% ItemClaimIns: Función que tomando en cuenta el valor del index carga en las variables
    '%               de la clase la información del arreglo
    Public Function ItemClaimIns(ByVal lintIndex As Integer) As Boolean
        If mblnCharge Then
            If lintIndex <= UBound(arrClaimIns) Then
                With arrClaimIns(lintIndex)
                    Me.nClaim = .nClaim
                    Me.sClient = .sClient
                    Me.dOccurdat = .dOccurdat
                    Me.sStaclaim = CShort(.sStaclaim)
                    Me.sStaClaimDes = .sStaClaimDes
                    Me.sClaimTypDes = .sClaimTypDes
                    Me.sDesClaimCause = .sDesClaimCause
                    Me.sIsLife = .sIsLife
                End With
                ItemClaimIns = True
            Else
                ItemClaimIns = False
            End If
        End If
    End Function
    '% FindClaimByBordereaux: busca los siniestros para un cliente en particular
    Public Function FindClaimByBordereaux(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_Agree As Integer, Optional ByVal nUsercode As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim recreaClaimBordereaux As eRemoteDB.Execute
        Dim lintIndex As Integer

        On Error GoTo FindClaimByBordereaux_err

        recreaClaimBordereaux = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaClaimByBordereaux'
        '+ Información leída el 10/02/2000 15:35:36

        With recreaClaimBordereaux
            .StoredProcedure = "reaClaimSI738"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintIndex = 0
                ReDim arrClaimBordereaux(50)
                Do While Not .EOF
                    arrClaimBordereaux(lintIndex).nClaim = .FieldToClass("nClaim")
                    arrClaimBordereaux(lintIndex).nBordereaux_cl = .FieldToClass("nBordereaux_cl")
                    arrClaimBordereaux(lintIndex).dDecladat = .FieldToClass("dDecladat")
                    arrClaimBordereaux(lintIndex).sStaclaim = .FieldToClass("sStaclaim")
                    arrClaimBordereaux(lintIndex).nReserve = .FieldToClass("nReserve")
                    arrClaimBordereaux(lintIndex).nCurrency = .FieldToClass("nCurrency")
                    arrClaimBordereaux(lintIndex).nLoc_Reserv = .FieldToClass("nLoc_Reserv")
                    .RNext()
                    lintIndex = lintIndex + 1
                Loop
                .RCloseRec()
                ReDim Preserve arrClaimBordereaux(lintIndex - 1)
                mblnCharge = True
            Else
                mblnCharge = False
            End If
        End With

        FindClaimByBordereaux = mblnCharge

FindClaimByBordereaux_err:
        If Err.Number Then
            FindClaimByBordereaux = False
        End If
        On Error GoTo 0

        recreaClaimBordereaux = Nothing
    End Function

    '% ItemClaimBordereaux: Función que tomando en cuenta el valor del index carga en las variables
    '%                      de la clase la información del arreglo
    Public Function ItemClaimBordereaux(ByVal lintIndex As Integer) As Boolean
        If mblnCharge Then
            If lintIndex <= UBound(arrClaimBordereaux) Then
                With arrClaimBordereaux(lintIndex)
                    Me.nClaim = .nClaim
                    Me.nBordereaux_cl = .nBordereaux_cl
                    Me.dDecladat = .dDecladat
                    Me.sStaclaim = CShort(.sStaclaim)
                    Me.nReserve = .nReserve
                    Me.nCurrency = .nCurrency
                    Me.nLoc_Reserv = .nLoc_Reserv

                End With
                ItemClaimBordereaux = True
            Else
                ItemClaimBordereaux = False
            End If
        End If
    End Function

    '%insValSIL002:Este listado muestra todos los siniestros con un costo (importe de reserva más pagos menos recuperación) superior o igual al importe dado por el usuario
    'Public Function insValSIL002(ByVal sCodispl As String, _
    ''                             ByVal dIniDate As Date, _
    ''                             ByVal dEndDate As Date, _
    ''                             ByVal nExcess As Double, _
    ''                             ByVal nCurrency As Long) As String

    Public Function insValSIL002(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSIL002_err

        lclsErrors = New eFunctions.Errors

        If dIniDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9071)
        End If
        '+ Si la fecha inicial es diferente de vacio continua las validaciones
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9072)
        End If
        '+ Si la fecha final es diferente de vacio continua las validaciones
        If dEndDate < dIniDate Then
            '+ Se verifica que que la fecha final sea mayor a la fecha inicial
            Call lclsErrors.ErrorMessage(sCodispl, 4159)
        End If
        '+ Se verifica que la fecha final no sea mayor a la fecha del día
        If dEndDate > Today Then
            Call lclsErrors.ErrorMessage(sCodispl, 4341)
        End If
        ''+ Se verifica que el importe en excesono este vacio
        '   If nExcess = 0 Or Fix(nExcess) = NumNull Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 4239)
        '   End If
        ''+ Se verifica que la moneda este llena
        '    If nCurrency = NumNull Or nCurrency = 0 Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 10107)
        '    End If

        insValSIL002 = lclsErrors.Confirm
        lclsErrors = Nothing
insValSIL002_err:
        If Err.Number Then
            insValSIL002 = insValSIL002 & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
    '%insValSIL007: valida los campos de la forma
    Public Function insValSIL007(ByVal sAction As String, Optional ByRef dInitDate As Date = eRemoteDB.Constants.dtmNull, Optional ByRef dEndDate As Date = eRemoteDB.Constants.dtmNull) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        If dInitDate = eRemoteDB.Constants.dtmNull Then
            lclsErrors.ErrorMessage("SIL007", 9071)
        End If

        If dEndDate = eRemoteDB.Constants.dtmNull Then
            lclsErrors.ErrorMessage("SIL007", 9072)
        End If

        If dInitDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull Then
            If dInitDate > dEndDate Then
                lclsErrors.ErrorMessage("SIL007", 4159)
            End If
        End If

        insValSIL007 = lclsErrors.Confirm

        lclsErrors = Nothing
    End Function

    '%insValSIL009_K: Esta función se encarga de validar los datos introducidos en la zona de
    '%detalle de la forma SIL009.
    Public Function insValSIL009_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nOrder As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSIL009_K_Err

        lclsErrors = New eFunctions.Errors

        '+Se realiza la validacion del campo Fecha de Inicio
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9071)
        End If

        '+Se valida la fecha final
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9072)
        End If

        '+Se valida que la fecha inicial no sea mayor que la fecha final
        If Not dInitDate = eRemoteDB.Constants.dtmNull And Not dEndDate = eRemoteDB.Constants.dtmNull Then
            If dInitDate > dEndDate Then
                Call lclsErrors.ErrorMessage(sCodispl, 4159)
            ElseIf dEndDate > Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 4341)
            End If
        End If

        '+Validacion del campo "Orden de la información"
        If nOrder = 0 Or nOrder = eRemoteDB.Constants.intNull Then

            Call lclsErrors.ErrorMessage(sCodispl, 4294)
        End If

        insValSIL009_K = lclsErrors.Confirm

        lclsErrors = Nothing

insValSIL009_K_Err:
        If Err.Number Then
            insValSIL009_K = insValSIL009_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    '%insValSIL762_K: Esta función se encarga de validar los datos introducidos en la zona de
    '%detalle de la forma insValSIL762_K.
    Public Function insValSIL762_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nClaim As Double, ByVal sCase_num As String, ByVal sOpt_option As String, ByVal dDate_ini As Date, ByVal dDate_end As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lcls_PayCla As eClaim.T_PayCla
        Dim lintCase_num As Integer

        On Error GoTo insValSIL762_K_Err

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        If sOpt_option = "2" Then
            If dDate_ini = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 3237)
            End If

            If dDate_end = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 3239)
            End If

            If dDate_ini <> eRemoteDB.Constants.dtmNull And dDate_end <> eRemoteDB.Constants.dtmNull Then
                If dDate_ini > dDate_end Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60207)
                End If
            End If
            '+Se realiza la validacion del campo Ramo
            If nBranch = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1022)
            End If

            '+Se realiza la validacion del campo Producto, si esta vacio
            If nProduct = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 11009)
            End If

        Else

            '+Se realiza la validacion del campo Ramo
            If nBranch = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1022)
            End If

            '+Se realiza la validacion del campo Producto, si esta vacio
            If nProduct = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 11009)
            End If

            '+Se realiza la validacion del campo Póliza, si esta vacio
            If nPolicy = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 3003)
            ElseIf Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy, True) Then
                Call lclsErrors.ErrorMessage(sCodispl, 8071)
            End If

            '+Se realiza la validacion del campo de siniestro, si esta vacio
            If nClaim = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 4006)
            ElseIf Find(nClaim, True) Then
                If Me.nPolicy <> nPolicy Then
                    Call lclsErrors.ErrorMessage(sCodispl, 4343)
                End If
            End If

            '+Se realiza la validacion del campo case, si esta vacio
            If sCase_num = String.Empty Then
                lintCase_num = eRemoteDB.Constants.intNull
            Else
                lcls_PayCla = New eClaim.T_PayCla
                lintCase_num = lcls_PayCla.getCaseInfo(sCase_num, 1)
            End If
        End If

        insValSIL762_K = lclsErrors.Confirm

insValSIL762_K_Err:
        If Err.Number Then
            insValSIL762_K = insValSIL762_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsPolicy = Nothing
        lcls_PayCla = Nothing
    End Function

    'insValOPL001_K: Valida los valores introducidos en el informe de cheque
    Public Function insValCRL003_K(ByVal sCodispl As String, ByVal sAction As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nCessType As Integer) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsValField As eFunctions.valField
        Dim lcolCheques As eCashBank.Cheques
        Dim lerrTime As New eFunctions.Errors
        Dim lintSta_cheque As Integer

        On Error GoTo insValCRL003_K_Err

        lclsErrors = New eFunctions.Errors

        lclsValField = New eFunctions.valField
        lclsValField.objErr = lerrTime

        ''**+ Validation of the field "Cession Type"
        ''+Validacion del campo "Tipo de cesion"
        '
        '    If nCessType = NumNull Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 6058)
        '    End If
        '
        '**+ Validation of the field "Date"
        '+ Validación del campo "Fecha"
        If eRemoteDB.Constants.dtmNull = dInitDate Then
            Call lclsErrors.ErrorMessage(sCodispl, 6128)
        End If
        If eRemoteDB.Constants.dtmNull = dEndDate Then
            Call lclsErrors.ErrorMessage(sCodispl, 6129)
        End If

        If lclsValField.ValDate(dInitDate) Then
            If lclsValField.ValDate(dEndDate) Then
                If dEndDate < dInitDate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6130)
                End If
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 7079)
            End If
        Else
            Call lclsErrors.ErrorMessage(sCodispl, 7079)
        End If

        insValCRL003_K = lclsErrors.Confirm

insValCRL003_K_Err:
        If Err.Number Then
            insValCRL003_K = insValCRL003_K & Err.Description
        End If
        On Error GoTo 0
        lcolCheques = Nothing
        lclsValField = Nothing
        lclsErrors = Nothing
        lerrTime = Nothing
    End Function

    '%reacountclaim: Esta rutina permite saver si existen siniestros declarados
    Function reacountclaim(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer) As Integer

        Dim lrecreacountclaim As eRemoteDB.Execute
        Dim nCount As Integer

        On Error GoTo reacountclaim_Err

        reacountclaim = 0

        lrecreacountclaim = New eRemoteDB.Execute
        With lrecreacountclaim
            .StoredProcedure = "reacountclaim"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 10, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                reacountclaim = .Parameters.Item("nCount").Value
            End If
        End With
reacountclaim_Err:
        If Err.Number Then reacountclaim = 0
        On Error GoTo 0
        lrecreacountclaim = Nothing
    End Function
    '% FindNumberOfClaims: Retorna la cantidad de siniestros declarados para
    '%                     un número de póliza en particular
    Public Function FindNumberOfClaims(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Double, ByVal nPolicy As Integer, ByVal nCertif As Double) As Integer
        Dim lintCountClaims As Integer
        Dim recreaNumber_Of_Claims As eRemoteDB.Execute

        On Error GoTo FindNumberOfClaims_err

        lintCountClaims = 0

        recreaNumber_Of_Claims = New eRemoteDB.Execute

        With recreaNumber_Of_Claims
            .StoredProcedure = "rea_Number_Of_Claims"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNumberOfClaims", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaims", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lintCountClaims = .Parameters("nNumberOfClaims").Value
                sClaims = .Parameters("sClaims").Value
            Else
                lintCountClaims = eRemoteDB.Constants.intNull
                sClaims = ""
            End If
        End With
        FindNumberOfClaims = lintCountClaims

FindNumberOfClaims_err:
        If Err.Number Then
            FindNumberOfClaims = eRemoteDB.Constants.intNull
            sClaims = ""
        End If
        On Error GoTo 0
        recreaNumber_Of_Claims = Nothing

    End Function

    '% FindClaim_per_Policy: Devuelve verdadero si consigue siniestros para una
    '%                       determinada póliza a una determinada fecha - ACM - 03/06/2002
    Public Function FindClaim_per_Policy(ByVal nPolicy As Integer, ByVal nClaim As Double, ByVal dDecladate As Date) As Boolean
        Dim recrea_ClaimPerPolicy As eRemoteDB.Execute
        Dim lintExist As Integer

        On Error GoTo FindClaim_per_Policy_err

        lintExist = 0

        recrea_ClaimPerPolicy = New eRemoteDB.Execute

        With recrea_ClaimPerPolicy
            .StoredProcedure = "rea_ClaimPerPolicy"
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDecladate", dDecladate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters("nExist").Value > 1 Then
                    FindClaim_per_Policy = True
                Else
                    FindClaim_per_Policy = False
                End If
            Else
                FindClaim_per_Policy = False
            End If
        End With

FindClaim_per_Policy_err:
        If Err.Number Then
            FindClaim_per_Policy = False
        End If
        On Error GoTo 0
        recrea_ClaimPerPolicy = Nothing
    End Function


    'insValSIC001_K: Valida los valores introducidos en la consulta de siniestros de un cliente
    Public Function insValSIC001_K(ByVal sCodispl As String, ByVal sClient As String, ByVal dOccurdate As Date) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsClient As eClient.Client

        On Error GoTo insValSIC001_Err

        lclsErrors = New eFunctions.Errors
        lclsClient = New eClient.Client

        '**+ Validation of the field "sClient"
        '+Validacion del campo "sClient"

        If sClient = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 2001)
        Else
            If Not lclsClient.Find(sClient, True) Then
                Call lclsErrors.ErrorMessage(sCodispl, 2044)
            End If
        End If

        '**+ Validation of the field "Date"
        '+ Validación del campo "Fecha"
        If Not dOccurdate = eRemoteDB.Constants.dtmNull Then
            If dOccurdate > Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 1002)
            End If
        End If

        insValSIC001_K = lclsErrors.Confirm
insValSIC001_Err:
        If Err.Number Then
            insValSIC001_K = insValSIC001_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsClient = Nothing
    End Function

    'insValSIC002_K: Valida los valores introducidos en la consulta de siniestros de un cliente
    Public Function insValSIC002_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dOccurdate As Date) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat

        On Error GoTo insValSIC002_Err

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        '**+ Validation of the field "nBranch"
        '+Validacion del campo "nBranch"
        If nBranch = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
        End If

        '**+ Validation of the field "nProduct"
        '+Validacion del campo "nProduct"
        If nProduct = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1014)
        End If

        '**+ Validation of the field "nPolicy"
        '+Validacion del campo "nPolicy"
        If nPolicy = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3003)
        Else
            If Not nBranch = eRemoteDB.Constants.intNull Or Not nProduct = eRemoteDB.Constants.intNull Then
                If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy, True) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 3001)
                    '**+ Validation of the field "nCertif"
                    '+Validacion del campo "nCertif"
                Else
                    If lclsPolicy.sPolitype > "1" Then
                        If nCertif = eRemoteDB.Constants.intNull Then
                            Call lclsErrors.ErrorMessage(sCodispl, 3006)
                        Else
                            If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                                Call lclsErrors.ErrorMessage(sCodispl, 3010)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '**+ Validation of the field "Date"
        '+ Validación del campo "Fecha"
        If Not dOccurdate = eRemoteDB.Constants.dtmNull Then
            If dOccurdate > Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 1002)
            End If
        End If

        insValSIC002_K = lclsErrors.Confirm
insValSIC002_Err:
        If Err.Number Then
            insValSIC002_K = insValSIC002_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
    End Function

    '% CalcDigit: Retorna el valor del dígito verificador dado un clinte - ACM - 29/07/2002
    Public Function CalcDigit(ByVal sClientCode As String) As String
        Dim lrecinsCalDigit As New eRemoteDB.Execute
        Dim sDigitParamValue As String

        sDigitParamValue = "0"
        With lrecinsCalDigit
            .StoredProcedure = "insCalDigit"
            .Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigitParamValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                CalcDigit = .Parameters("sDigit").Value
            Else
                CalcDigit = "0"
            End If
        End With

        lrecinsCalDigit = Nothing
    End Function

    '%insValSIC005_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
    '%forma.
    Public Function insValSIC005_K(ByVal sCodispl As String, ByVal dInitDate As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSIC005_Err

        lclsErrors = New eFunctions.Errors

        '**+ Validation of the field "Date"
        '+ Validación del campo "Fecha"
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4095)
        End If

        insValSIC005_K = lclsErrors.Confirm

insValSIC005_Err:
        If Err.Number Then
            insValSIC005_K = insValSIC005_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
    'insValSI003: esta función realiza las validaciones de la transacción SI003
    Public Function insValSI003(ByVal sCodispl As String, ByVal sLeadcial As String) As String
        Dim lerrTime As eFunctions.Errors

        On Error GoTo insValSI003_Err

        lerrTime = New eFunctions.Errors

        insValSI003 = String.Empty

        '+Si el número del siniestro en la compañia lider es nulo
        If sLeadcial = String.Empty Then
            Call lerrTime.ErrorMessage(sCodispl, 4014)
        End If

        insValSI003 = lerrTime.Confirm

insValSI003_Err:
        If Err.Number Then
            insValSI003 = "insValSI003: " & Err.Description
        End If
        On Error GoTo 0
        lerrTime = Nothing
    End Function
    '%insPostSI003: Esta función se encarga de llamar a las actualizaciones de la SI003
    Public Function insPostSI003(ByVal nClaim As Double, ByVal sLeadcial As String, ByVal nUsercode As Integer) As Boolean
        Dim lclsClaim_win As eClaim.Claim_win

        On Error GoTo insPostSI003_Err

        lclsClaim_win = New eClaim.Claim_win

        If UpdateLeadCial(nClaim, sLeadcial, nUsercode) Then
            insPostSI003 = lclsClaim_win.Add_Claim_win(nClaim, "SI003", "2", nUsercode)
        Else
            insPostSI003 = False
        End If

insPostSI003_Err:
        If Err.Number Then
            insPostSI003 = False
        End If
        On Error GoTo 0
        lclsClaim_win = Nothing
    End Function

    'Realiza las actualizaciones correspondientes a la transacción SI003
    Public Function UpdateLeadCial(ByVal nClaim As Double, ByVal sLeadcial As String, ByVal nUsercode As Integer) As Boolean

        Dim lrecinsUpdLeadCial As eRemoteDB.Execute

        On Error GoTo UpdateLeadCial_Err
        lrecinsUpdLeadCial = New eRemoteDB.Execute

        With lrecinsUpdLeadCial
            .StoredProcedure = "insUpdLeadCial"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLeadCial", sLeadcial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateLeadCial = .Run(False)
        End With

UpdateLeadCial_Err:
        If Err.Number Then
            UpdateLeadCial = False
        End If
        On Error GoTo 0
        lrecinsUpdLeadCial = Nothing
    End Function

    '% inspreSI004: se buscan los datos utilizar en la transacción
    Public Sub inspreSI004(ByVal sReloadAction As String, ByVal bQuery As Boolean, ByVal nClaim As Double, ByVal dDecladat As Date, ByVal dPrescdat As Date, ByVal dOccurdat As Date, ByVal sHour As String, ByVal nCausecod As Integer, ByVal sClaimTyp As String, ByVal nOffice_pay As Integer, ByVal nOfficeAgen_pay As Integer, ByVal nAgency_pay As Integer, ByVal dLimit_pay As Date, nClaimParent As Integer)

        Dim lclsProduct As eProduct.Product = New eProduct.Product
        Dim lblnFind As Boolean

        On Error GoTo inspreSI004_Err

        '+ Si no se está recargando la página

        If sReloadAction = String.Empty Then
            If Find(nClaim) Then
            End If
        Else
            Me.nClaim = nClaim
            Me.dPrescdat = dPrescdat
            Me.sHour = sHour
            Me.nCausecod = nCausecod
            Me.sClaimTyp = sClaimTyp
            Me.nOffice_pay = nOffice_pay
            Me.nOfficeAgen_pay = nOfficeAgen_pay
            Me.nAgency_pay = nAgency_pay
            Me.dLimit_pay = dLimit_pay
            Me.dDecladat = dDecladat
            Me.dOccurdat = dOccurdat
            Me.nClaimParent = nClaimParent
        End If

        If Not bQuery Then
            If Me.dPrescdat = eRemoteDB.Constants.dtmNull And dPrescdat = eRemoteDB.Constants.dtmNull Then
                lclsProduct = New eProduct.Product
                lblnFind = True
                If lclsProduct.Find(Me.nBranch, Me.nProduct, Me.dDecladat) Then
                    If lclsProduct.nClaim_pres = eRemoteDB.Constants.intNull Then
                        lclsProduct.nClaim_pres = 0
                    End If
                Else
                    lclsProduct.nClaim_pres = 0
                End If

                '+ Si en el Diseñador de productos no se indica plazo para la entrega de documentos, el campo
                '+ "Fecha de entrega de documentos", puede quedar vacio pudiendo ser modificado por el usuario.
                If Me.dOccurdat <> eRemoteDB.Constants.dtmNull And lclsProduct.nClaim_pres <> 0 Then
                    Me.dPrescdat = System.DateTime.FromOADate(Me.dOccurdat.ToOADate + lclsProduct.nClaim_pres)
                End If
            End If

            If Me.dLimit_pay = eRemoteDB.Constants.dtmNull And dLimit_pay = eRemoteDB.Constants.dtmNull Then
                If Not lblnFind Then
                    lclsProduct = New eProduct.Product
                    If Not lclsProduct.Find(Me.nBranch, Me.nProduct, Me.dDecladat) Then
                        lclsProduct.nClaim_Pay = 0
                    End If
                End If

                If lclsProduct.nClaim_Pay = eRemoteDB.Constants.intNull Then
                    lclsProduct.nClaim_Pay = 0
                End If

                If Me.dDecladat <> eRemoteDB.Constants.dtmNull Then
                    Me.dLimit_pay = System.DateTime.FromOADate(Me.dOccurdat.ToOADate + lclsProduct.nClaim_Pay)
                Else
                    Me.dLimit_pay = System.DateTime.FromOADate(Today.ToOADate + lclsProduct.nClaim_Pay)
                End If
            End If
        End If

inspreSI004_Err:
        If Err.Number Then
            On Error GoTo 0
        End If
        lclsProduct = Nothing
    End Sub

    Public Function FindClaim(ByVal nClaim As Double) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo FindClaim_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "reaclaim_find"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                Me.nClaim = .FieldToClass("nClaim")

                .RCloseRec()
                FindClaim = True
            Else
                FindClaim = False
            End If
        End With

FindClaim_Err:
        If Err.Number Then
            FindClaim = False
        End If
        On Error GoTo 0
        lclsRemote = Nothing
    End Function

    '%Find_1: Busca los datos del siniestro en la tabla Claim a partir del número de siniestro dado
    Public Function Find_1(ByVal llngnClaim As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaClaim_1 As eRemoteDB.Execute
        Dim lstrValue As String

        On Error GoTo Find_Err

        If llngnClaim <> nClaim Or lblnFind Then
            mclsClaim_cases = Nothing
            lrecreaClaim_1 = New eRemoteDB.Execute

            'Definición de parámetros para stored procedure 'insudb.reaClaim_1'
            'Información leída el 20/09/1999 08:02:03 AM

            With lrecreaClaim_1
                .StoredProcedure = "reaClaim_1"
                .Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nClaim = .FieldToClass("nClaim")
                    sCertype = .FieldToClass("sCertype")
                    nPolicy = .FieldToClass("nPolicy")
                    nBranch = .FieldToClass("nBranch")
                    nCertif = .FieldToClass("nCertif")
                    nOffice = .FieldToClass("nOffice")
                    nOfficeAgen = .FieldToClass("nOfficeAgen")
                    nAgency = .FieldToClass("nAgency")
                    Find_1 = True
                    .RCloseRec()
                Else
                    Find_1 = False
                End If
            End With
        Else
            Find_1 = True
        End If

Find_Err:
        If Err.Number Then
            Find_1 = False
        End If
        On Error GoTo 0
        lrecreaClaim_1 = Nothing
    End Function

    '% insvalSIL961:
    Public Function insValSIL961(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClaim As Double, ByVal nPolicy As Integer) As String
        Dim lclsValues As eFunctions.Values
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsErrors As eFunctions.Errors

        Dim lclsCl_cover As Cl_Cover
        Dim lclscl_covers As CL_Covers
        Dim lblStareserv As Boolean

        lclsCl_cover = New Cl_Cover
        lclscl_covers = New CL_Covers

        Dim lstrError As String

        On Error GoTo insValSIL961_Err

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        '+ Debe venir siniestro o ramo-producto
        If nClaim = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4006)
        Else
            '+ Si ingreso ramo,  entonces producto no debe ser null
            If nBranch <> eRemoteDB.Constants.intNull And nProduct = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1014)
            Else
                If nClaim <> eRemoteDB.Constants.intNull Then
                    '+ Valida que exista numero de siniestro
                    If Not Find(nClaim) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 4005)
                    Else
                        If sStaclaim <> 7 Then
                            With lclscl_covers
                                If .Find_ClaimReserve(nClaim) Then
                                    For Each lclsCl_cover In lclscl_covers
                                        If lclsCl_cover.sReservstat = "5" Then
                                            lblStareserv = False
                                            Exit For
                                        End If
                                    Next lclsCl_cover
                                End If
                            End With

                            If lblStareserv Then
                                Call lclsErrors.ErrorMessage(sCodispl, 100150)
                            End If

                        End If
                    End If
                End If

            End If
        End If

        insValSIL961 = lclsErrors.Confirm

insValSIL961_Err:
        If Err.Number Then
            insValSIL961 = "insValSIL961: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
    End Function

    '% Reastatus_cheques: busca el estado de las ordenes de pagos
    Public Function Reastatus_Cheques(ByVal nRequest_Nu As Double) As Boolean
        Dim recreaCheques As eRemoteDB.Execute
        Dim status As Integer
        Dim nSta_chequeaux As Integer

        On Error GoTo Reastatus_Chques_err

        recreaCheques = New eRemoteDB.Execute

        With recreaCheques
            .StoredProcedure = "reastatus_cheques"
            .Parameters.Add("nRequest_nu", nRequest_Nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSta_chequeaux", nSta_chequeaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            status = .Parameters("nSta_chequeaux").Value

            '% Control de ordenes de pagos aprobadas
            If status = 2 Or status = 3 Or status = 4 Or status = 6 Or status = 8 Or status = 9 Then
                Reastatus_Cheques = True
            Else
                Reastatus_Cheques = False
            End If
        End With
Reastatus_Chques_err:
        If Err.Number Then
            Reastatus_Cheques = False
        End If
        On Error GoTo 0
        recreaCheques = Nothing
    End Function

    '% FindClaimCli: busca los siniestros para un cliente en particular
    Public Function FindClaimCli(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nType As Integer) As Boolean
        Dim recreaClaimCli As eRemoteDB.Execute
        Dim lintIndex As Integer

        On Error GoTo FindClaimCli_err

        recreaClaimCli = New eRemoteDB.Execute

        With recreaClaimCli
            .StoredProcedure = "reaClaimCli"

            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                lintIndex = 0
                ReDim arrClaimCli(50)
                Do While Not .EOF

                    arrClaimCli(lintIndex).nClaim = .FieldToClass("nClaim")
                    arrClaimCli(lintIndex).dOccurdat = .FieldToClass("dOccurdat")
                    arrClaimCli(lintIndex).sDesClaimCause = .FieldToClass("sDesClaimCause")
                    arrClaimCli(lintIndex).nBranch = .FieldToClass("nBranch")
                    arrClaimCli(lintIndex).nProduct = .FieldToClass("nProduct")
                    arrClaimCli(lintIndex).nPolicy = .FieldToClass("nPolicy")
                    arrClaimCli(lintIndex).nCertif = .FieldToClass("nCertif")

                    .RNext()
                    lintIndex = lintIndex + 1
                Loop
                .RCloseRec()
                ReDim Preserve arrClaimCli(lintIndex - 1)
                mblnCharge = True
            Else
                mblnCharge = False
            End If
        End With

        FindClaimCli = mblnCharge

FindClaimCli_err:
        If Err.Number Then
            FindClaimCli = False
        End If
        On Error GoTo 0
        recreaClaimCli = Nothing
    End Function

    '% ItemClaimCli: Función que tomando en cuenta el valor del index carga en las variables
    '%               de la clase la información del arreglo
    Public Function ItemClaimCli(ByVal lintIndex As Integer) As Boolean
        If mblnCharge Then
            If lintIndex <= UBound(arrClaimCli) Then
                With arrClaimCli(lintIndex)
                    Me.nClaim = .nClaim
                    Me.sClient = .sClient
                    Me.dOccurdat = .dOccurdat
                    Me.sDesClaimCause = .sDesClaimCause
                    Me.nBranch = .nBranch
                    Me.nProduct = .nProduct
                    Me.nPolicy = .nPolicy
                    Me.nCertif = .nCertif
                End With
                ItemClaimCli = True
            Else
                ItemClaimCli = False
            End If
        End If
    End Function

    '% insvalvi8002bd: Se valida informacion de la BD
    Public Function InsValSI051K_BD(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClaim As Double, ByVal nPolicy As Integer) As String
        Dim lrecreaClaim As eRemoteDB.Execute
        Dim lstrDes As String = ""

        On Error GoTo InsValSI051K_BD_Err

        lrecreaClaim = New eRemoteDB.Execute

        With lrecreaClaim
            .StoredProcedure = "InsValSI051K_BD"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ArrayErrors", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsValSI051K_BD = .Parameters("ArrayErrors").Value
        End With

InsValSI051K_BD_Err:
        If Err.Number Then
            InsValSI051K_BD = String.Empty
        End If
        lrecreaClaim = Nothing
        On Error GoTo 0
    End Function

    '% ValClaimRequest: valida que El titular del recibo ha tenido cheques devueltos o tarjetas de crédito incobrables
    Public Function ValProtestCheck_his(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nUsercode As Integer) As Boolean
        Dim llngExists As Integer
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lrecreaClaim
        Dim lrecreaClaimRequest As eRemoteDB.Execute

        On Error GoTo ValClaimRequest_err

        lrecreaClaimRequest = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaClaimRequest'
        'Información leída el 15/01/2001 10.42.24
        With lrecreaClaimRequest
            .StoredProcedure = "REAVALPROTESTCHECK_HIS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            ValProtestCheck_his = .Parameters("nExists").Value = 1
        End With


ValClaimRequest_err:
        If Err.Number Then
            ValProtestCheck_his = False
        End If
        On Error GoTo 0
        lrecreaClaimRequest = Nothing
    End Function
End Class






