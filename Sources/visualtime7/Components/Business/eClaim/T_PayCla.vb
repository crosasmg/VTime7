Option Strict Off
Option Explicit On
Public Class T_PayCla
	'%-------------------------------------------------------%'
	'% $Workfile:: T_PayCla.cls                             $%'
	'% $Author:: Cidler                                     $%'
	'% $Date:: 10-05-12 14:31                               $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla T_PAYCLA al 10-16-2003 20:45:35
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nClaim As Double ' NUMBER     22   0     10   N
	Public nCase_num As Integer ' NUMBER     22   0     5    N
	Public nDeman_type As Integer ' NUMBER     22   0     5    N
	Public nCover_curr As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nPay_concep As Double ' NUMBER     22   0     5    N
	Public nPay_amount As Double ' NUMBER     22   6     18   S
	Public nCov_exchange As Double ' NUMBER     22   6     11   S
	Public nTax As Double ' NUMBER     22   2     4    S
	Public nTot_amount As Double ' NUMBER     22   6     18   S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nGroup_insu As Integer ' NUMBER     22   0     5    S
	Public sIndAuto As String ' CHAR       1    0     0    S
	Public nCurrency_pay As Integer ' NUMBER     22   0     5    S
	Public nPaycov_amount As Double ' NUMBER     22   6     18   S
	Public nTotcov_amount As Double ' NUMBER     22   6     18   S
	Public nParticip As Double ' NUMBER     22   6     18   S

    Public WithInformation As String
    Public nBordereaux As Integer
    Public nFra_amount As Double
    Public nDepreciatebase As Double
    Public nDepreciateamount As Double
    Public nDepreciaterate As Double
    Public nRasa As Double
    Public nRasaAnnual As Double
    Public sRasa_routine As String
    Public nTypesupport As Integer

    Public nDDR_Amount As Double
    Public sInd_ApplyDDR As String
    Public nPay_Amount_No_DDR As Double
    Public nId_Settle As Integer


    '**-Define the enumerate type for the Current account types accourding to the 400 table
    '- Se define el tipo enumenrado para los Tipos de cuentas corrientes segun la tabla 400

    Public Enum eTypeAccount
        etaIntermediaryEfective = 1
        etaCoInsurender = 2
        etaCoFaculInsurender = 3
        etaProfessionals = 4
        etaClients = 5
        etaClinics = 6
        etaWorks = 7
        etaCoObligFaculInsurender = 8
        etaPolicy = 9
        etaIntermediaryPaper = 10
        etaAssociates = 11
        etaProvider = 12
        etaAsseCompany = 13
    End Enum

    '**-Define the variable mstrPayTypeValues, to contein the list of the available pays form
    '- Se define la variable mstrPayTypeValues, para contener la lista de formas de pagos disponibles

    Private mstrPayTypeValues As String
    Private mlngTypeList As Integer
    Private mclsClaim As Claim

    '**- Define the enumerate list that will contein the claim status (Table135)
    '-Se define la lista enumerada que contendra el estado del siniestro (Table135)

    Private Enum eClaimStatus '**Cancelled
        clngCancelled = 1 'Anulado
        clngInProcess = 2 '**In process
        'En tramitación
        clngInAdjust = 3 '** In adjustement
        'En ajuste
        clngInPayProcess = 4 '**In pay process
        'En proceso de pago
        clngPayd = 5 '**Paid
        'Pagado
        clngInformationPending = 6 '**Pending information
        'Pendiente de información
        clngRejected = 7 '**Rejected
        'Rechazado
        clngApprovalPending = 8 '**Approval pending
        'Pendiente de aprobación
    End Enum
    '+Se define la variable sBenef y dNext_Pay para el código del beneficiario y la fecha del próximo pago de la renta en la SI773
    Public sBenef As String
    Public dNext_Pay As Date
    Public nId As Integer
    Public nCashNum As Integer
    Public nOutReserv As Double
    Public sClient_Rep As String
    Public nOffice_pay As Integer
    Public nAgency_pay As Integer
    Public nOfficeAgen_pay As Integer
    Public dOccurdate_l As Date
    '

    '%Delete:
    Public Function Delete(ByVal Claim As Double) As Boolean
        Dim lrecdelT_PayCla As eRemoteDB.Execute

        On Error GoTo Delete_Err

        lrecdelT_PayCla = New eRemoteDB.Execute

        '**+Parameters definition for the stored procedure 'insudb.delT_PayCla'
        '**+Data read on 01/29/2001 4:54:54 PM
        '+ Definición de parámetros para stored procedure 'insudb.delT_PayCla'
        '+ Información leída el 29/01/2001 4:54:54 PM
        With lrecdelT_PayCla
            .StoredProcedure = "delT_PayCla"
            .Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCase_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nDeman_type", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_Err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        lrecdelT_PayCla = Nothing
    End Function

    '% Add: se crean los registros en t_PayCla
    Public Function Add() As Boolean
        Dim lrecinsCreT_PayCla As eRemoteDB.Execute

        On Error GoTo Add_err

        lrecinsCreT_PayCla = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insCreT_PayCla'
        With lrecinsCreT_PayCla
            .StoredProcedure = "insCreT_PayCla"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover_curr", nCover_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_amount", nPay_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCov_exchange", nCov_exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTot_amount", nTot_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndAuto", sIndAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency_pay", nCurrency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaycov_amount", nPaycov_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTotcov_amount", nTotcov_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)            
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciateamount", nDepreciateamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciaterate", nDepreciaterate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciatebase", nDepreciatebase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRASA_routine", sRasa_routine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRasaAnnual", nRasaAnnual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRasa", nRasa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nDDR_Amount", nDDR_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_ApplyDDR", sInd_ApplyDDR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Amount_No_DDR", nPay_Amount_No_DDR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        lrecinsCreT_PayCla = Nothing
    End Function

    '%Find:
    Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover_curr As Integer, ByVal nCover As Integer, ByVal nPay_concept As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecReaT_paycla As eRemoteDB.Execute

        On Error GoTo Find_Err
        Find = True
        If nClaim <> nClaim Or nCase_num <> nCase_num Or nDeman_type <> nDeman_type Or nCover_curr <> nCover_curr Or nCover <> nCover Or nPay_concep <> nPay_concept Or lblnFind Then
            lrecReaT_paycla = New eRemoteDB.Execute

            '+Definición de parámetros para stored procedure 'insudb.ReaT_paycla'
            With lrecReaT_paycla
                .StoredProcedure = "ReaT_paycla"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover_curr", nCover_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPay_concep", nPay_concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nClaim = .FieldToClass("nClaim")
                    nCase_num = .FieldToClass("nCase_num")
                    nDeman_type = .FieldToClass("nDeman_type")
                    nCover_curr = .FieldToClass("nCover_curr")
                    nModulec = .FieldToClass("nModulec")
                    nCover = .FieldToClass("nCover")
                    nPay_concep = .FieldToClass("nPay_concep")
                    nPay_amount = .FieldToClass("nPay_amount")
                    nCov_exchange = .FieldToClass("nCov_exchange")
                    nTax = .FieldToClass("nTax")
                    nTot_amount = .FieldToClass("nTot_amount")
                    nGroup_insu = .FieldToClass("nGroup_insu")
                    sIndAuto = .FieldToClass("sIndAuto")
                    nCurrency_pay = .FieldToClass("nCurrency_pay")
                    nPaycov_amount = .FieldToClass("nPaycov_amount")
                    nTotcov_amount = .FieldToClass("nTotcov_amount")
                    nParticip = .FieldToClass("nParticip")
                    nRasa = .FieldToClass("nRasa")
                    nRasaAnnual = .FieldToClass("nRasaAnnual")
                    nDepreciateamount = .FieldToClass("nDepreciateamount")
                    nDepreciaterate = .FieldToClass("nDepreciaterate")
                    nDepreciatebase = .FieldToClass("nDepreciatebase")
                    nFra_amount = .FieldToClass("nFra_amount")
                    sRasa_routine = .FieldToClass("sRasa_routine")
                    .RCloseRec()
                Else
                    Find = False
                End If
            End With
            lrecReaT_paycla = Nothing
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
    End Function

    '%insChangeValdate_Currency:Recalcula los importes a pagar según fecha de valorización y moneda
    Public Function insChangeValdate_Currency(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover_curr As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nPay_concep As Integer, ByVal nAmountPay As Double, ByVal nTax As Double, ByVal nUsercode As Integer, ByVal nGroup_insu As Integer, ByVal sIndAuto As String, ByVal nCurrency As Integer, ByVal dValdate As Date) As Boolean
        Dim lclsExchange As eGeneral.Exchange
        Dim ldblPay_amount As Double
        Dim ldblTot_amount As Double
        Dim ldblExchange As Double

        lclsExchange = New eGeneral.Exchange

        On Error GoTo insChangeValdate_Currency_Err

        With lclsExchange
            If .Find(nCover_curr, dValdate) Then
                ldblExchange = .nExchange
            End If

            Me.nCurrency_pay = nCover_curr
            Me.nPaycov_amount = nAmountPay

            Call .Convert(eRemoteDB.Constants.intNull, nAmountPay, nCover_curr, nCurrency, dValdate, ldblPay_amount)
            ldblPay_amount = lclsExchange.pdblResult

            nAmountPay = nAmountPay + nAmountPay * nTax / 100

            Me.nTotcov_amount = nAmountPay

            Call .Convert(eRemoteDB.Constants.intNull, nAmountPay, nCover_curr, nCurrency, dValdate, ldblTot_amount)
            ldblTot_amount = lclsExchange.pdblResult
        End With

        With Me
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nDeman_type = nDeman_type
            .nCover_curr = nCover_curr
            .nModulec = nModulec
            .nCover = nCover
            .nPay_concep = nPay_concep
            .nPay_amount = ldblPay_amount
            .nCov_exchange = ldblExchange
            .nTax = nTax
            .nTot_amount = ldblTot_amount
            .nUsercode = nUsercode
            .nGroup_insu = nGroup_insu
            .sIndAuto = sIndAuto
            .nCurrency_pay = nCurrency_pay
            .nPaycov_amount = nPaycov_amount
            .nTotcov_amount = nTotcov_amount
        End With

        insChangeValdate_Currency = Add


insChangeValdate_Currency_Err:
        If Err.Number Then
            insChangeValdate_Currency = False
        End If
        lclsExchange = Nothing
    End Function

    '**%insValSI008: makes the validation for the Claim Payment window
    '%insValSI008: se realizan las validaciones para la ventana de Pago de siniestro
    Public Function insValSI008(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date, ByVal dValdate As Date, ByVal dPayDate As Date, ByVal nUsercode As Integer, Optional ByVal nRole As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal nCurrency As Integer = 0, Optional ByVal nPay_form As Integer = 0, Optional ByVal nPay_type As Integer = 0, Optional ByVal nServ_Order As Double = 0, Optional ByVal nInvoice As Double = 0, Optional ByVal nExchange As Double = 0, Optional ByVal nDoc_type As Integer = 0, Optional ByVal dBilldate As Date = #12:00:00 AM#, Optional ByVal nTotalAmount As Double = 0, Optional ByVal nPremium As Double = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nDeductible_Met As Integer = 0) As String
        Dim lrecInsValSi008 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String
        Dim lclsValClient As eClient.ValClient
        Dim nIndStatus As Integer
        Dim nIndautorizacion As Integer
        Dim sListas As String

        On Error GoTo InsValSi008_Err

        lclsValClient = New eClient.ValClient

        lrecInsValSi008 = New eRemoteDB.Execute



        With lrecInsValSi008
            .StoredProcedure = "InsSi008pkg.InsValSi008"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValdate", dValdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_form", nPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInvoice", nInvoice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDoc_type", nDoc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBilldate", dBilldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTotalamount", nTotalAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeductible_Met", nDeductible_Met, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sListas", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndStatus", nIndStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value


            lobjErrors = New eFunctions.Errors

            '+ Las validaciones de seguridad no estan en el procedure por todos los cambios que se
            '+ deben realizar en seguridad
            If lstrError <> String.Empty Then
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValSI008 = lobjErrors.Confirm
                End With
                lobjErrors = Nothing
            End If



        End With
InsValSi008_Err:
        If Err.Number Then
            insValSI008 = "InsValSi008: " & Err.Description
        End If
        On Error GoTo 0
        lrecInsValSi008 = Nothing
    End Function

    '**% insValSI008_K: make the validation of the header Claim Payment
    '% insValSI008_K: se realizan las validaciones del encabezado de Pago de Siniestro
    Public Function insValSI008_K(ByVal sCodispl As String, ByVal nClaim As Double, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCase_num As String = "", Optional ByVal nPay_type As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0) As String
		Dim lrecInsValSi008_K As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
		Dim lstrError As String
		
		'+Definición de parámetros para stored procedure 'InsSi008pkg.InsValSi008_KUpd'
		'+Información leída el 24/04/2003
		On Error GoTo InsValSi008_K_Err
		lrecInsValSi008_K = New eRemoteDB.Execute
		With lrecInsValSi008_K
			.StoredProcedure = "InsSi008pkg.InsValSi008_K"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCase_num", sCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdate_l", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			Me.dOccurdate_l = .Parameters("dOccurdate_l").Value
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
					insValSI008_K = lobjErrors.Confirm
				End With
				lobjErrors = Nothing
			End If
			
		End With
InsValSi008_K_Err: 
		If Err.Number Then
			insValSI008_K = "InsValSi008_K: " & Err.Description
		End If
		On Error GoTo 0
		lrecInsValSi008_K = Nothing
	End Function
	
	'%insValSI008Upd: se realizan las validaciones de la ventana emergente para Pago de siniestro
    Public Function insValSI008Upd(ByVal sCodispl As String, ByVal sSchema As String, ByVal nAction As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nCover_curr As Integer, ByVal nPay_concep As Integer, ByVal nAmount_CurCov As Double, ByVal nAmount As Double, ByVal nTax As Double, ByVal nTotAmount As Double, ByVal nAmountPend As Double, ByVal nExchange As Double, ByVal nDest_role As Integer, ByVal nPayType As Integer, ByVal dEffecdate As Date, ByVal dValdate As Date, ByVal dPayDate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nId_Settle As Integer = 0, Optional ByVal nFra_amount As Integer = 0, Optional ByVal nDDR As Double = 0) As String
        Dim lrecInsValSi008upd As eRemoteDB.Execute
        Dim lsecTime As eSecurity.Secur_sche
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo InsValSi008upd_Err

        lsecTime = New eSecurity.Secur_sche
        lrecInsValSi008upd = New eRemoteDB.Execute

        With lrecInsValSi008upd
            .StoredProcedure = "InsSi008pkg.InsValSi008upd"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSchema", sSchema, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover_curr", nCover_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_CurCov", nAmount_CurCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTotamount", nTotAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountpend", nAmountPend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDest_role", nDest_role, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaytype", nPayType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValdate", dValdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSecurity", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ldblTotalPay", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDDR", nDDR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value
            '+ Las validaciones de seguridad no estan en el procedure por todos los cambios que se
            '+ deben realizar en seguridad

            If .Parameters("nSecurity").Value = 1 Then
                If lsecTime.Reload(eSecurity.Secur_sche.eTypeTable.Limits, sSchema) Then
                    If Not lsecTime.valLimits(eSecurity.Secur_sche.eTypeLimits.clngLimitsClaimPay, sSchema, (.Parameters("nBranch").Value), nCurrency, CDec(nAmount), (.Parameters("nProduct").Value)) Then
                        If lstrError <> String.Empty Then
                            lstrError = lstrError & "||4107"
                        Else
                            lstrError = lstrError & "4107"
                        End If
                    End If
                End If
            End If
            If lsecTime.Reload(eSecurity.Secur_sche.eTypeTable.Limits, sSchema) Then
                If Not lsecTime.valLimits(eSecurity.Secur_sche.eTypeLimits.clngLimitsClaimPay, sSchema, (.Parameters("nBranch").Value), nCover_curr, CDec(.Parameters("ldblTotalPay").Value), (.Parameters("nProduct").Value)) Then
                    If lstrError <> String.Empty Then
                        lstrError = lstrError & "||4107"
                    Else
                        lstrError = lstrError & "4107"
                    End If
                End If
            End If
            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValSI008Upd = .Confirm()
                End With
                lobjErrors = Nothing
            End If

        End With
InsValSi008upd_Err:
        If Err.Number Then
            insValSI008Upd = "InsValSi008upd: " & Err.Description
        End If
        On Error GoTo 0
        lrecInsValSi008upd = Nothing
        lsecTime = Nothing
    End Function

    '%InsPostSI008Upd: se actualiza la vantana pop up
    Public Function insPostSI008Upd(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover_curr As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nPay_concep As Integer, ByVal nPay_amount As Double, ByVal nCov_exchange As Double, ByVal nTax As Double, ByVal nTot_amount As Double, ByVal nUsercode As Integer, ByVal nGroup_insu As Integer, ByVal sIndAuto As String, ByVal nCurrency_pay As Integer, ByVal nPaycov_amount As Object, ByVal nTotcov_amount As Object, Optional ByVal nParticip As Double = 0, Optional ByVal nDepreciateamount As Double = 0, Optional ByVal nDepreciaterate As Double = 0, Optional ByVal nDepreciatebase As Double = 0, Optional ByVal nFra_amount As Double = 0, Optional ByVal nRasa As Double = 0, Optional ByVal nRasaAnnual As Double = 0, Optional ByVal sRasa_routine As String = "", Optional nDDR_Amount As Double = 0, Optional sInd_ApplyDDR As String = "2", Optional nPay_Amount_No_DDR As Double = 0, Optional nId_Settle As Integer = 0) As Boolean
        On Error GoTo InsPostSI008Upd_Err

        Me.nClaim = nClaim
        Me.nCase_num = nCase_num
        Me.nDeman_type = nDeman_type
        Me.nCover_curr = nCover_curr
        Me.nCover = nCover
        Me.nModulec = nModulec
        Me.nPay_concep = nPay_concep
        Me.nPay_amount = nPay_amount
        Me.nCov_exchange = nCov_exchange
        Me.nTax = nTax
        Me.nTot_amount = nTot_amount
        Me.nUsercode = nUsercode
        Me.nGroup_insu = nGroup_insu
        Me.sIndAuto = sIndAuto
        Me.nCurrency_pay = nCurrency_pay
        Me.nPaycov_amount = nPaycov_amount
        Me.nTotcov_amount = nTotcov_amount
        Me.nTotcov_amount = (nPaycov_amount + (nPaycov_amount * (nTax / 100)))
        Me.nParticip = nParticip
        Me.nRasa = nRasa
        Me.nRasaAnnual = nRasaAnnual
        Me.nDepreciateamount = nDepreciateamount
        Me.nDepreciaterate = nDepreciaterate
        Me.nDepreciatebase = nDepreciatebase
        Me.nFra_amount = nFra_amount
        Me.sRasa_routine = sRasa_routine
        Me.nDDR_Amount = nDDR_Amount
        Me.sInd_ApplyDDR = sInd_ApplyDDR
        Me.nPay_Amount_No_DDR = nPay_Amount_No_DDR
        Me.nId_Settle = nId_Settle
        insPostSI008Upd = Add()

InsPostSI008Upd_Err:
        If Err.Number Then
            insPostSI008Upd = False
        End If
        On Error GoTo 0
    End Function

    '% InsPostSI008: se actualizan los datos del siniestro asociado al pago
    Public Function InsPostSI008(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal nPayForm As Integer, ByVal nPay_type As Integer, ByVal nServ_Order As Double, ByVal sCoinsuNet As String, ByVal nInvoice As Integer, ByVal dPay_date As Date, ByVal nPay_curr As Integer, ByVal nLoc_exchange As Double, ByVal nLoc_tot_pay As Double, ByVal nUsercode As Integer, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, Optional ByVal nAmountPay As Double = 0, Optional ByVal nRequest_nu As Integer = 0, Optional ByVal sCheque As String = "", Optional ByVal nConsec As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nConcept As Integer = 0, Optional ByVal sClientOP As String = "", Optional ByVal dDat_propos As Date = #12:00:00 AM#, Optional ByVal sDescript As String = "", Optional ByVal dIssue_dat As Date = #12:00:00 AM#, Optional ByVal dLedger_dat As Date = #12:00:00 AM#, Optional ByVal sRequest_ty As String = "", Optional ByVal nSta_cheque As Integer = 0, Optional ByVal dStat_date As Date = #12:00:00 AM#, Optional ByVal nUser_sol As Integer = 0, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal sInter_pay As String = "", Optional ByVal nAcc_type As Integer = 0, Optional ByVal sAcco_num As String = "", Optional ByVal nBank_code As Integer = 0, Optional ByVal nBk_agency As Integer = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sN_Aba As String = "", Optional ByVal nDoc_type As Integer = 0, Optional ByRef dBilldate As Date = #12:00:00 AM#, Optional ByVal sInAcco_num As String = "", Optional ByVal nIn_nBank_Code As Integer = 0, Optional ByVal nTypeTrans As Integer = 0, Optional ByVal nAmountDest As Double = 0, Optional ByVal nCompany As Integer = 0, Optional ByVal nOrig_curr As Integer = 0, Optional ByVal nOrig_amount As Double = 0, Optional ByVal nAfect_amount As Double = 0, Optional ByVal nExcent_amount As Double = 0, Optional ByVal sCessirei As String = "", Optional ByVal nBordereaux As Integer = 0, Optional ByVal dValdate As Date = #12:00:00 AM#, Optional ByVal sAccountHolder As String = vbNullString,
Optional ByVal nBankExt As Integer = 0, Optional ByVal sBankAccount As String = vbNullString, Optional ByVal nExternal_Concept As Integer = 0, Optional ByVal nDeductible_Method As Integer = eRemoteDB.Constants.intNull, Optional ByVal nTypesupport As Integer = 1) As Boolean
        Dim lclsClaim As eClaim.Claim
        Dim lcolT_PayClas As eClaim.T_PayClas
        Dim lclsT_PayClas As eClaim.T_PayCla
        Dim lclsExchange As eGeneral.Exchange
        Dim lclsOpt_sinies As Opt_sinies
        Dim lreaBillGenerateService As eCollection.Bills
        lreaBillGenerateService = New eCollection.Bills

        lclsExchange = New eGeneral.Exchange
        lclsOpt_sinies = New Opt_sinies

        On Error GoTo InsPostSI008_Err
        lcolT_PayClas = New eClaim.T_PayClas

        Call lclsOpt_sinies.Find()

        With lcolT_PayClas
            If .FindSI008(nClaim, nCase_num, nDeman_type, nPay_type, nLoc_exchange, dPay_date) Then
                For Each lclsT_PayClas In lcolT_PayClas
                    If Not Me.Find(nClaim, nCase_num, nDeman_type, lclsT_PayClas.nCover_curr, lclsT_PayClas.nCover, lclsT_PayClas.nPay_concep) Then
                        Me.nClaim = nClaim
                        Me.nCase_num = nCase_num
                        Me.nDeman_type = nDeman_type
                        Me.nCover_curr = lclsT_PayClas.nCover_curr
                        Me.nModulec = lclsT_PayClas.nModulec
                        Me.nCover = lclsT_PayClas.nCover
                        Me.nPay_concep = lclsT_PayClas.nPay_concep
                        Me.nPay_amount = lclsT_PayClas.nPay_amount
                        Me.nCov_exchange = lclsT_PayClas.nCov_exchange
                        Me.nTax = lclsT_PayClas.nTax
                        Me.nTot_amount = lclsT_PayClas.nTot_amount
                        Me.nUsercode = nUsercode
                        Me.nGroup_insu = lclsT_PayClas.nGroup_insu
                        Me.sIndAuto = lclsT_PayClas.sIndAuto
                        Me.nCurrency_pay = nPay_curr
                        Me.nParticip = nParticip
                        Me.nRasa = lclsT_PayClas.nRasa
                        InsPostSI008 = Add()
                    End If
                Next lclsT_PayClas
            End If
        End With

        lclsClaim = New Claim
        With lclsClaim
            If .Find(nClaim) Then
                .nClaim = nClaim
                .nCase_num = nCase_num
                .nDeman_type = nDeman_type
                .nTransac = .nMovement + 1
                .nRole = nRole
                .sClient = sClient
                .nPayForm = nPayForm
                .nPay_type = nPay_type
                .nServ_Order = nServ_Order
                .sCoinsuNet = sCoinsuNet
                .nInvoice = nInvoice
                .dPay_date = dPay_date
                .dValdate = dValdate
                .nPay_curr = nPay_curr
                If nPay_curr <> lclsOpt_sinies.nCurrency Then
                    Call lclsExchange.Convert(nLoc_exchange, nLoc_tot_pay, nPay_curr, lclsOpt_sinies.nCurrency, dPay_date, .nLoc_tot_pay)
                    .nLoc_tot_pay = lclsExchange.pdblResult
                    .nLoc_exchange = lclsExchange.pdblExchange
                Else
                    .nLoc_tot_pay = nLoc_tot_pay
                    .nLoc_exchange = 1
                End If
                .nRequest_nu = nRequest_nu
                .sCheque = sCheque
                .nConsec = nConsec
                .nAmount = nAmount
                .nConcept = nConcept
                .sClientOP = sClientOP
                .dDat_propos = dDat_propos
                .sDescript = sDescript
                .dIssue_dat = dIssue_dat
                .dLedger_dat = dLedger_dat
                .sRequest_ty = sRequest_ty
                .nSta_cheque = nSta_cheque
                .dStat_date = dStat_date
                .nUser_sol = nUser_sol
                .nAcc_bank = nAcc_bank
                .sInter_pay = sInter_pay
                .nAcc_type = nAcc_type
                .sAcco_num = sAcco_num
                .nBank_code = nBank_code
                .nBk_agency = nBk_agency
                .nOffice = nOffice
                .nOfficeAgen = nOfficeAgen
                .nAgency = nAgency
                .nNotenum = nNotenum
                .sN_Aba = sN_Aba
                .nUsercode = nUsercode
                .nDoc_type = nDoc_type
                .dBilldate = dBilldate
                .sInAcco_num = sInAcco_num
                .nIn_nBank_Code = nIn_nBank_Code
                .nBk_agency = nBk_agency
                .nTypeTrans = nTypeTrans
                .nAmountDest = nAmountDest
                .nDoc_type = nDoc_type
                .dBilldate = dBilldate
                .nCompany = nCompany
                .nOrig_curr = nOrig_curr
                .nOrig_amount = nOrig_amount
                .sCessiCoi = sCoinsuNet
                .sInd_rei = sCessirei
                .nBordereaux = IIf(nBordereaux = 0, eRemoteDB.Constants.intNull, nBordereaux)
                .nCashNum = nCashNum
                .nAmountCheq = 0
                .nAfect_amount = nAfect_amount
                .nExcent_amount = nExcent_amount
                .sAccountHolder = sAccountHolder
                .nBankExt = nBankExt
                .sBankAccount = sBankAccount
                .nExternal_Concept = nExternal_Concept
                .nDeductible_Method = nDeductible_Method
                .nTypesupport = nTypesupport
                InsPostSI008 = .Pay
            End If
        End With

        If InsPostSI008 Then
            '+ se llama al WS de Invoice para Facturar la RASA
            If nPay_type = 2 Then
                ReaPremiumRasa(nClaim, nUsercode)
            End If
        End If

InsPostSI008_Err:
        If Err.Number Then
            InsPostSI008 = False
        End If

        On Error GoTo 0
        lclsExchange = Nothing
        lclsOpt_sinies = Nothing
        lcolT_PayClas = Nothing
        lclsClaim = Nothing

    End Function

    '% Find: Permite cargar en la colección los datos de la tabla Collect_comm
    Public Function ReaPremiumRasa(ByVal nClaim As String, ByVal nUsercode As Integer) As Boolean

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
                    'lreaBillGenerateService.BillGenerateService("1", .FieldToClass("nReceipt"), nUsercode, , .FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), , , "1")
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
    '**% insValSI738_K: make the validation of the header Massive Claim Payment
    '% insValSI738_K: se realizan las validaciones del encabezado de Pagos Masivos de Siniestros
	Public Function insValSI738_K(ByVal sCodispl As String, ByVal dPayDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_Agree As Integer, ByVal sClientContr As String) As Object
		
		Dim lclsErrors As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclsClaim As eClaim.Claim
		Dim lclsClaim_Master As eClaim.Claim_Master
		Dim lintIndex As Integer
		Dim lblnPayProcess As Boolean
		Dim lblnPaydate As Boolean
		
		On Error GoTo insValSI738_K_Err
		
		lclsErrors = New eFunctions.Errors
		lvalTime = New eFunctions.valField
		lclsClaim = New eClaim.Claim
		lclsClaim_Master = New eClaim.Claim_Master
		lvalTime.objErr = lclsErrors
		
		lblnPayProcess = False
		lblnPaydate = False
		
		'+Validación de la fecha de pago.
		With lvalTime
			.objErr = lclsErrors
			.Codispl = sCodispl
			.ErrEmpty = 4003
			.ErrInvalid = 7114
			Call .ValDate(dPayDate,  , eFunctions.valField.eTypeValField.ValAll)
		End With

        '+Validación del número de Poliza
		If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
		    Call lclsErrors.ErrorMessage(sCodispl, 55752)
		End If
		'+No se necesita esta validacion *
		'+Validación del número de relación.
		'If nBordereaux_cl = NumNull Or _
		''    nBordereaux_cl = 0 Then
		'    Call lclsErrors.ErrorMessage(sCodispl, 55752)
		'Else **
		'If Not lclsClaim_Master.Find(nBordereaux_cl) Then
		'    Call lclsErrors.ErrorMessage(sCodispl, 55753)
		'Else *
		
		If sClientContr = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 100154)
		End If
		'+Debe ser posterior a la fecha de denuncio registrada para la relación en tratamiento.
		If lclsClaim.FindClaimByBordereaux(nBranch, nProduct, nPolicy, nCertif, nCod_Agree) Then
			For lintIndex = 0 To lclsClaim.CountClaimBordereaux
				If lclsClaim.ItemClaimBordereaux(lintIndex) Then
					If Not lblnPaydate Then
						If dPayDate <= lclsClaim.dDecladat Then
							Call lclsErrors.ErrorMessage(sCodispl, 55751)
							lblnPaydate = True
						End If
					End If
					If CStr(lclsClaim.sStaclaim) = "2" Then
						lblnPayProcess = True
						Exit For
					End If
				End If
			Next 
		End If
		'+Al menos uno de los siniestros de la relación, debe tener estado "En proceso de liquidación".
		If Not lblnPayProcess Then
			Call lclsErrors.ErrorMessage(sCodispl, 55754)
		End If
		'End If *
		'End If **
		
		insValSI738_K = lclsErrors.Confirm
		
insValSI738_K_Err: 
		If Err.Number Then
			insValSI738_K = insValSI738_K & Err.Description
		End If
		lclsErrors = Nothing
		lvalTime = Nothing
		lclsClaim = Nothing
		lclsClaim_Master = Nothing
		
		On Error GoTo 0
	End Function
	
	'**% insValSI738: make the validation of the folder Massive Claim Payment
	'%   insValSI738: se realizan las validaciones del frame de Pagos Masivos de Siniestros
	Public Function insValSI738(ByVal sCodispl As String, ByVal nWayPay As Integer, ByVal nPayType As Integer, ByVal nCurrency As Integer, ByVal dValdate As Date, ByVal nClaim As Double, ByVal sClient As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCurr_acc As eCashBank.Curr_acc
		Dim lclsClaim As eClaim.Claim
		Dim lclsRoles As ePolicy.Roles
		Dim lcolRoleses As ePolicy.Roleses
		Dim lintTyp_acco As Integer
		
		On Error GoTo insValSI738_Err
		
		lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		lcolRoleses = New ePolicy.Roleses
		
		'+Validación de la Forma de pago.
		
		Call lclsClaim.Find(nClaim, True)
		
		If nWayPay = eRemoteDB.Constants.intNull Or nWayPay = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 4043)
		Else
			If nWayPay = 3 Then 'A cuenta
				If lcolRoleses.Find_by_Policy(lclsClaim.sCertype, lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.nPolicy, lclsClaim.nCertif, sClient, Today, eRemoteDB.Constants.intNull) Then
					
					For	Each lclsRoles In lcolRoleses
						If lclsRoles.nRole <> eRemoteDB.Constants.intNull And lclsRoles.nRole <> 0 And nCurrency <> eRemoteDB.Constants.intNull And nCurrency <> 0 And sClient <> String.Empty Then
							Select Case lclsRoles.nRole
								Case Claim_case.eClaimRole.clngClaimRContract To Claim_case.eClaimRole.clngClaimRContGuar, Claim_case.eClaimRole.clngClaimRInsuredAffected, 50, 51, 52, 53, 54, 55
									lintTyp_acco = eTypeAccount.etaClients
									
								Case Claim_case.eClaimRole.clngClaimRPrivHosp
									lintTyp_acco = eTypeAccount.etaClinics
									
								Case Claim_case.eClaimRole.clngClaimRWorkShop
									lintTyp_acco = eTypeAccount.etaWorks
									
								Case Claim_case.eClaimRole.clngClaimRProfessional
									lintTyp_acco = eTypeAccount.etaProfessionals
									'+Pago diferente a "Honorarios"
									If nPayType <> eRemoteDB.Constants.intNull And nPayType <> 0 Then
										If (nPayType <> 3) Then
											Call lclsErrors.ErrorMessage(sCodispl, 4081)
										End If
									End If
									
								Case Claim_case.eClaimRole.clngClaimRAgent
									lintTyp_acco = eTypeAccount.etaIntermediaryEfective
							End Select
							lclsCurr_acc = New eCashBank.Curr_acc
							If Not lclsCurr_acc.FindClientCurr_acc(lintTyp_acco, "0", sClient, nCurrency) Then
								Call lclsErrors.ErrorMessage(sCodispl, 4054)
							End If
							lclsCurr_acc = Nothing
						End If
					Next lclsRoles
				End If
			End If
		End If
		
		
		'+Validacion del tipo de pago
		With lclsClaim
			If nPayType = eRemoteDB.Constants.intNull Or nPayType = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 4045)
			Else
				'+Estado del siniestro "pago total"
				If .sStaclaim = eClaimStatus.clngPayd Then
					'+Pago diferente a "Gastos de recuperación" y "Pago ex-gratia"
					If (nPayType <> 5 And nPayType <> 4) Then
						Call lclsErrors.ErrorMessage(sCodispl, 4052)
					End If
					
					'+Estado del siniestro "en proceso de autorización"
				ElseIf .sStaclaim = eClaimStatus.clngInAdjust Then 
					'+Se Valida la existencia de algún pago total
					If InsValClaim_his(nClaim, 0) Then
						'+Pago diferente a "Gastos de recuperación" y "Pago ex-gratia"
						If (nPayType <> 5 And nPayType <> 4) Then
							Call lclsErrors.ErrorMessage(sCodispl, 4052)
						End If
					End If
					
					'+Estado del siniestro igual a "rechazado"
				ElseIf .sStaclaim = eClaimStatus.clngRejected Then 
					'+Pago diferente a "Pago de honorarios"
					If nPayType <> 3 Then
						Call lclsErrors.ErrorMessage(sCodispl, 4050)
					End If
				End If
			End If
		End With
		
		'+Validación de la Moneda.
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1351)
		End If
		
		'+Validación de la Fecha de valorización.
		
		If dValdate = System.Date.FromOADate(eRemoteDB.Constants.intNull) Or dValdate = System.Date.FromOADate(0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55527)
		End If
		
		insValSI738 = lclsErrors.Confirm
		
insValSI738_Err: 
		If Err.Number Then
			insValSI738 = insValSI738 & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
		lcolRoleses = Nothing
	End Function
	
	Public ReadOnly Property sPayTypeList(Optional ByVal nClaim As Double = 0) As String
		Get
			If nClaim = 0 Then
				nClaim = Me.nClaim
			End If
			Call insLoadPayTypeValues()
			sPayTypeList = mstrPayTypeValues
		End Get
	End Property
	
	Public ReadOnly Property nPayTypeListType(Optional ByVal nClaim As Double = 0) As Integer
		Get
			If nClaim = 0 Then
				nClaim = Me.nClaim
			End If
			Call insLoadPayTypeValues()
			nPayTypeListType = mlngTypeList
		End Get
	End Property
	
	
	'%Get:
	Public ReadOnly Property Total_amount(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer) As Double
		Get
			Dim lrecReaTotal_amount_t_paycla As eRemoteDB.Execute
			
			On Error GoTo Total_amount_err
			
			Total_amount = 0
			lrecReaTotal_amount_t_paycla = New eRemoteDB.Execute
			
			With lrecReaTotal_amount_t_paycla
				.StoredProcedure = "ReaTotal_amount_t_paycla"
				.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Total_amount = IIf(.FieldToClass("nTotal") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nTotal"))
					.RCloseRec()
				End If
			End With
			
Total_amount_err: 
			If Err.Number Then
				Total_amount = 0
			End If
			On Error GoTo 0
			lrecReaTotal_amount_t_paycla = Nothing
		End Get
	End Property
	
	Private Sub insLoadPayTypeValues()
		Dim lblnFind As Boolean
		If mstrPayTypeValues = String.Empty Then
			
			If Not mclsClaim Is Nothing Then
				If mclsClaim.nClaim = Me.nClaim Then
					lblnFind = True
				Else
					lblnFind = mclsClaim.Find(Me.nClaim)
				End If
			Else
				mclsClaim = New Claim
				lblnFind = mclsClaim.Find(Me.nClaim)
				mclsClaim = Nothing
			End If
			
			If lblnFind Then
				If mclsClaim.sStaclaim = eClaimStatus.clngPayd Then
					mlngTypeList = eFunctions.Values.ecbeTypeList.Exclution
					mstrPayTypeValues = "1,2,3"
				Else
					If mclsClaim.sStaclaim = eClaimStatus.clngRejected Then
						mlngTypeList = eFunctions.Values.ecbeTypeList.Exclution
						mstrPayTypeValues = "1,2,4,5"
					Else
						'                    If Trim$(lstrBase) <> "SI021" Then
						mlngTypeList = eFunctions.Values.ecbeTypeList.none
						mstrPayTypeValues = String.Empty
						'                    End If
					End If
				End If
			End If
		End If
	End Sub
	
	'**% getCaseInfo: breaks down the received string as a parameter, returning the case number
	'**             or of the claimant.
	'**             Format: Case/Deman_type/Cliename
	'% getCaseInfo: descompone el string recibido como parámetro, devolviendo el número del caso
	'%              o del demandante.
	'%              Formato: Case/Deman_type/Cliename
	Public Function getCaseInfo(ByVal sValue As String, ByVal nItem As Integer) As Integer
		If sValue = String.Empty Then
			getCaseInfo = 0
		Else
			If nItem = 2 Then
				sValue = Mid(sValue, InStr(1, sValue, "/") + 1)
			End If
			getCaseInfo = CShort(Mid(sValue, 1, InStr(1, sValue, "/") - 1))
		End If
	End Function
	
	'**%DeleteByCase: delete the info associates to the case
	'% DeleteByCase: se elimina la información asociada al caso
	Public Function DeleteByCase(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecdelT_PayClaByCase As eRemoteDB.Execute
		
		On Error GoTo DeleteByCase_err
		
		lrecdelT_PayClaByCase = New eRemoteDB.Execute
		
		'**+Parameter definition for the stored procedure 'insudb.delT_PayClaByCase'
		'**+Data read on 03/09/2001 09:40:39
		'+ Definición de parámetros para stored procedure 'insudb.delT_PayClaByCase'
		'+ Información leída el 09/03/2001 09:40:39 a.m.
		
		With lrecdelT_PayClaByCase
			.StoredProcedure = "delT_PayCla"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteByCase = .Run(False)
		End With
		
DeleteByCase_err: 
		If Err.Number Then
			DeleteByCase = False
		End If
		On Error GoTo 0
		lrecdelT_PayClaByCase = Nothing
    End Function

    '**%DeleteByCase: delete the info associates to the case
    '% DeleteByCase: se elimina la información asociada al caso
    Public Function insCalServ_order(ByVal nServ_order As Double, ByVal nPay_type As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsCalServ_order As eRemoteDB.Execute

        On Error GoTo insCalServ_order_err

        lrecinsCalServ_order = New eRemoteDB.Execute

        '**+Parameter definition for the stored procedure 'insudb.delT_PayClaByCase'
        '**+Data read on 03/09/2001 09:40:39
        '+ Definición de parámetros para stored procedure 'insudb.delT_PayClaByCase'
        '+ Información leída el 09/03/2001 09:40:39 a.m.

        With lrecinsCalServ_order
            .StoredProcedure = "insCalServ_order"
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insCalServ_order = .Run(False)
        End With

insCalServ_order_err:
        If Err.Number Then
            insCalServ_order = False
        End If
        On Error GoTo 0
        lrecinsCalServ_order = Nothing
    End Function
	
	'%insValSIL099: This function validates the data introduced in the detail zone of the form SIL099
	'%insValSIL099: Esta función se encarga de validar los datos introducidos en forma SIL099
	Public Function insValSIL099(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer) As String
		Dim lclsValField As eFunctions.valField
		Dim lclsErrors As eFunctions.Errors
		
		lclsValField = New eFunctions.valField
		lclsErrors = New eFunctions.Errors
		lclsValField.objErr = lclsErrors
		
		'+Se realizan las validaciones correspondientes a la fecha inicial.
		
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 9071)
		Else
			If Not lclsValField.ValDate(dIniDate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				lclsErrors.ErrorMessage(sCodispl, 2082)
			End If
		End If
		
		'+Se realizan las validaciones correspondientes a ramo.
		
		If nBranch <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'+Se realizan las validaciones correspondientes a producto.
		
		If nProduct <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		
		'+Se realizan las validaciones correspondientes a sucursal.
		
		If nOffice <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 1079)
		End If
		
		insValSIL099 = lclsErrors.Confirm
		
insValSIL099_Err: 
		If Err.Number Then
			insValSIL099 = insValSIL099 & Err.Description
		End If
		On Error GoTo 0
		lclsValField = Nothing
		lclsErrors = Nothing
	End Function
	
	'**%insPostSIL099: This routine is incharge to create the records in the payment orders table
	'%insPostSIL099: Esta rutina se encarga de crear los registros en la tabla de ordenes de pago
	Public Function insPostSIL099(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		insPostSIL099 = PaymentDisability(nBranch, nProduct, nOffice, dEffecdate, nUsercode)
		
insPostSIL099_Err: 
		If Err.Number Then
			insPostSIL099 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%PaymentDisability: Gen. automática de pagos por invalidez
	'% PaymentDisability: Gen. automática de pagos por invalidez
	Private Function PaymentDisability(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPaymentDisability As eRemoteDB.Execute
		
		lrecinsPaymentDisability = New eRemoteDB.Execute
		
		'**+Parameter definition for the stored procedure 'insudb.insPaymentDisability'
		'**+Data read on 10/10/2001 06:44:51
		'Definición de parámetros para stored procedure 'insudb.insPaymentDisability'
		'Información leída el 10/10/2001 06:44:51 p.m.
		
		With lrecinsPaymentDisability
			.StoredProcedure = "insPaymentDisability"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			PaymentDisability = .Run(False)
		End With
		lrecinsPaymentDisability = Nothing
	End Function
	
	Private Sub Class_Initialize_Renamed()
		mstrPayTypeValues = String.Empty
		mlngTypeList = 0
		mclsClaim = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	Private Sub Class_Terminate_Renamed()
		mclsClaim = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%InsValClaim_his: Verifica la existencia de pago total
	Public Function InsValClaim_his(ByVal Claim As Double, ByVal Case_num As Integer) As Boolean
		Dim lrecReaT_paycla As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsValClaim_his_Err
		
		InsValClaim_his = True
		
		lrecReaT_paycla = New eRemoteDB.Execute
		
		With lrecReaT_paycla
			.StoredProcedure = "ValClaim_His"
			.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					InsValClaim_his = True
				Else
					InsValClaim_his = False
				End If
			Else
				InsValClaim_his = False
			End If
		End With
		
InsValClaim_his_Err: 
		If Err.Number Then
			InsValClaim_his = False
		End If
		On Error GoTo 0
		lrecReaT_paycla = Nothing
	End Function
	
	'%InsValFinanc_Dra_Claim: Verifica que se hayan cancelado giros con el siniestro en tratamiento
	Public Function InsValFinanc_Dra_Claim(ByVal Claim As Double) As Boolean
		Dim lrecReaT_paycla As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsValFinanc_Dra_Claim_Err
		
		InsValFinanc_Dra_Claim = True
		
		lrecReaT_paycla = New eRemoteDB.Execute
		
		With lrecReaT_paycla
			.StoredProcedure = "ValFinanc_Dra_Claim"
			.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					InsValFinanc_Dra_Claim = True
				Else
					InsValFinanc_Dra_Claim = False
				End If
			Else
				InsValFinanc_Dra_Claim = False
			End If
		End With
		
InsValFinanc_Dra_Claim_Err: 
		If Err.Number Then
			InsValFinanc_Dra_Claim = False
		End If
		On Error GoTo 0
		lrecReaT_paycla = Nothing
	End Function
	
	'%LoadTabs: arma la secuencia en código HTML
	Public Function LoadTabs(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDemanType As Integer, ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nUsercode As Integer, ByVal sOpener As String, ByVal nPay_type As Integer, ByVal sSI008Required As String, ByVal dEffecdate As Date) As String
        '-Codigos de las txs de la secuencia
        '-Cantidad de tx de la secuencia.
        '-Debe modificarse si se modifica la cantidad de datos en CN_Codispl
        Dim lrecWindows As eGeneral.WinMessag
        Dim lclsSecurSche As eSecurity.Secur_sche
		Dim lclsSequence As eFunctions.Sequence
		Dim lstrCodisp As String
		Dim lstrCodispl As String
		Dim lstrShort_desc As String
		Dim lstrDescript As String
		Dim nModules As Short
		Dim nWindowTy As Short
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		Dim lstrAllTx() As String
		Dim lstrOneTx() As String
		Dim lintIndex As Short
		Dim lstrHTMLCode As String
        '-Codigos de las txs de la secuencia
        Dim CN_Codispl As String
        '-Cantidad de tx de la secuencia.
        '-Debe modificarse si se modifica la cantidad de datos en CN_Codispl
        Dim CN_CodisplCount As Short
        Dim oClaim As New eClaim.Claim
        Dim oProduct As New eProduct.Product
		
		On Error GoTo LoadTabs_Err
		
		lclsSecurSche = New eSecurity.Secur_sche
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eGeneral.WinMessag
		lstrHTMLCode = String.Empty
		
        oClaim.Find(nClaim)
        oProduct.Find(oClaim.nBranch, oClaim.nProduct, dEffecdate)

        '+ Se elimna la pantalla de reaseguro para SOAP
        If oProduct.sBrancht = "6" Then
            CN_Codispl = "'SI008','SI754','SI762'"
            '-Cantidad de tx de la secuencia.
            '-Debe modificarse si se modifica la cantidad de datos en CN_Codispl
            CN_CodisplCount = 3

        Else
            CN_Codispl = "'SI008','SI754','SI749','SI762'"
            '-Cantidad de tx de la secuencia.
            '-Debe modificarse si se modifica la cantidad de datos en CN_Codispl
            CN_CodisplCount = 4

        End If

		Call ValRequired(nClaim, nCase_num, nDemanType, dEffecdate)
		
		lstrHTMLCode = lclsSequence.makeTable
		
		'+Se recuperan los datos de las ventanas de la secuencia
		lstrDescript = lrecWindows.Find_Windowsdesc(CN_Codispl)
		If lstrDescript <> String.Empty Then
			lstrAllTx = Microsoft.VisualBasic.Split(lstrDescript, "||")
			
			For lintIndex = 0 To CN_CodisplCount - 1
				lstrOneTx = Microsoft.VisualBasic.Split(lstrAllTx(lintIndex), "|")
				
				lstrCodisp = lstrOneTx(0)
				lstrCodispl = Trim(lstrOneTx(1))
				lstrShort_desc = lstrOneTx(2)
				nModules = IIf(Trim(lstrOneTx(3)) = String.Empty, eRemoteDB.Constants.intNull, CShort(lstrOneTx(3)))
				lstrDescript = lstrOneTx(4)
				nWindowTy = IIf(Trim(lstrOneTx(5)) = String.Empty, eRemoteDB.Constants.intNull, CShort(lstrOneTx(5)))
				
				'+ Se asignan los valores a las variables de requerido
				If lstrCodispl = "SI008" Then
					lblnRequired = True
				Else
					lblnRequired = False
				End If
				
				'+ Se asignan los valores a las variables de contenido
				If lstrCodispl = "SI008" Then
					If sSI008Required = "1" Then
						lblnContent = False
					Else
						lblnContent = True
					End If
				Else
					If InStr(1, WithInformation, lstrCodispl) <> 0 Then
						lblnContent = True
					Else
						lblnContent = False
					End If
				End If
				
				'+Si se acepto la ventana SI0762 se coloca como requerida la SI008
				If sOpener = "SI762" And lstrCodispl = "SI008" Then
					lblnContent = False
				End If

                '+ Se busca la imagen a colocar en los links
                With lclsSecurSche
                    If Not .valTransAccess(sUserSchema, lstrCodispl, "1") Then
                        If lblnContent Then
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
                        Else
                            If lblnRequired Then
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
                            Else
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
                            End If
                        End If
                    Else
                        If Not lblnContent Then
                            If lblnRequired Then
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                            Else
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                            End If
                        Else
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                        End If
                    End If
                End With

                lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage, , , , , , , lstrDescript, nModules, nWindowTy)
            Next
        End If

        lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()

        LoadTabs = lstrHTMLCode

LoadTabs_Err:
        On Error GoTo 0
        lclsSequence = Nothing
        lclsSecurSche = Nothing
        lrecWindows = Nothing
	End Function
	Public Function ValRequired(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValRequired_ClaimPay As eRemoteDB.Execute
		lrecinsValRequired_ClaimPay = New eRemoteDB.Execute
		
		On Error GoTo ValRequired_Err
		'+Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'+Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_ClaimPay
			.StoredProcedure = "insValRequired_ClaimPay"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWindowsCheck", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Me.WithInformation = .Parameters("sWindowsCheck").Value
				ValRequired = True
			Else
				ValRequired = False
			End If
		End With
		
ValRequired_Err: 
		If Err.Number Then
			ValRequired = False
		End If
		On Error GoTo 0
		lrecinsValRequired_ClaimPay = Nothing
	End Function
	
	
	'% insValSI773_K: se realizan las validaciones del encabezado de Pago de Rentas
	Public Function insValSI773_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClaim As Double, ByVal dStartdate As Date, ByVal dEndDate As Date, ByVal nProcess As Integer, ByVal nWayPay As Integer) As String
		
		On Error GoTo insValSI773_K_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		Dim lstrSep As String
        Dim lstrError As String = ""

        lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		lstrSep = "||"
		
		'+Ramo debe estar lleno
		If nBranch = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "1022"
		End If
		
		'+Producto debe estar lleno
		If nProduct = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "1014"
		End If
		
		'+Si el siniestro está lleno debe estar registrado en el sistema
		If nClaim <> eRemoteDB.Constants.intNull Then
			If Not lclsClaim.Find(nClaim) Then
				lstrError = lstrError & lstrSep & "4005"
			Else
				If lclsClaim.sStaclaim = Claim.Estatclaim.eWaitApproval Then
					lstrError = lstrError & lstrSep & "1022|0|1|. Debe ser En proceso de liquidación"
				End If
			End If
		End If
		
		'+Fecha de inicio debe estar llena
		If dStartdate = eRemoteDB.Constants.dtmNull Then
			lstrError = lstrError & lstrSep & "4160"
		End If
		
		'+Fecha hasta debe estar llena y debe ser mayor a fecha inicio
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			lstrError = lstrError & lstrSep & "3239"
		Else
			If dStartdate > dEndDate Then
				lstrError = lstrError & lstrSep & "4158"
			End If
		End If
		
		'+Si el tipo de proceso es masivo debe incluir la forma de pago
		If nProcess = 2 Then
			If nWayPay = eRemoteDB.Constants.intNull Then
				lstrError = lstrError & lstrSep & "5127"
			End If
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI773",  ,  ,  ,  ,  , lstrError)
				insValSI773_K = .Confirm()
			End With
		End If
		
insValSI773_K_Err:
        If Err.Number Then
            insValSI773_K = ""
            insValSI773_K = insValSI773_K & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
	End Function
	
	'% insValSI773Upd: se realizan las validaciones de la PopUp de Pago de Rentas
	Public Function insValSI773Upd(ByVal sCodispl As String, ByVal nPayForm As Integer, ByVal nAmount As Double, ByVal nAmountPay As Double) As String
		
		On Error GoTo insValSI773Upd_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		Dim lstrSep As String
        Dim lstrError As String = ""

        lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		lstrSep = "||"
		
		'+La forma de pago debe estar llena
		If nPayForm = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "3015"
		End If
		
		'+Monto a pagar debe estar lleno
		If nAmountPay = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "60141"
		Else
			'+Debe ser igual o menor al monto de renta
			If nAmountPay > nAmount Then
				lstrError = lstrError & lstrSep & "55733"
			End If
		End If
		
		If lstrError = String.Empty Then
			lstrError = lstrError & lstrSep & "3616|0|0| Al Aceptar se actualizara el pago. "
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI773",  ,  ,  ,  ,  , lstrError)
				insValSI773Upd = .Confirm()
			End With
		End If
		
insValSI773Upd_Err:
        If Err.Number Then
            insValSI773Upd = ""
            insValSI773Upd = insValSI773Upd & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
	End Function
	
	'% insPostSI773: se ejecutan las actualizaciones correspondientes al pago de rentas
	Public Function insPostSI773(ByVal sClaims As String, ByVal sCase_Nums As String, ByVal sDeman_Types As String, ByVal sClients As String, ByVal sIds As String, ByVal nRequest_nu As Integer, ByVal sCheque As String, ByVal nAmount As Double, ByVal nConcept As Integer, ByVal sInter_pay As String, ByVal dDat_propos As Date, ByVal sDescript As String, ByVal dIssue_dat As Date, ByVal dLedger_dat As Date, ByVal sRequest_ty As String, ByVal nUser_sol As Integer, ByVal nUsercode As Integer, ByVal nTypeSupport As Integer, ByVal nDocSupport As Double, ByVal nTax_Percent As Double, ByVal nCurrencyOri As Integer, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nOfficePay As Integer, ByVal nCompany As Integer, ByVal nTaxCode As Integer, ByVal nAfect As Double, ByVal nExent As Double, ByVal nTax_Amount As Double, ByVal nOffice As Integer, ByVal sKey As String, ByVal nAcc_bank As Integer, ByVal nAcc_type As Integer, ByVal sAcco_num As String, ByVal nBank_code As Double, ByVal nBk_agency As Double, ByVal sN_Aba As String, ByVal sOfficeAgen As String, ByVal sAgency As String, Optional ByVal nExternal_Concept As Integer = 0) As Boolean
		
		On Error GoTo InsPostSI773_Err
		
		insPostSI773 = insPay_SI773(sClaims, sCase_Nums, sDeman_Types, sClients, sIds, nRequest_nu, sCheque, nAmount, nConcept, sInter_pay, dDat_propos, sDescript, dIssue_dat, dLedger_dat, sRequest_ty, nUser_sol, nUsercode, nTypeSupport, nDocSupport, nTax_Percent, nCurrencyOri, nCurrencyPay, nAmountPay, nOfficePay, nCompany, nTaxCode, nAfect, nExent, nTax_Amount, nOffice, sKey, nAcc_bank, nAcc_type, sAcco_num, nBank_code, nBk_agency, sN_Aba, sOfficeAgen, sAgency, nExternal_Concept)
InsPostSI773_Err: 
		If Err.Number Then
			insPostSI773 = False
		End If
		On Error GoTo 0
	End Function
	'**% insPay_SI773: Realiza los pagos correspondientes a las rentas
	Public Function insPay_SI773(ByVal sClaims As String, ByVal sCase_Nums As String, ByVal sDeman_Types As String, ByVal sClients As String, ByVal sIds As String, ByVal nRequest_nu As Integer, ByVal sCheque As String, ByVal nAmount As Double, ByVal nConcept As Integer, ByVal sInter_pay As String, ByVal dDat_propos As Date, ByVal sDescript As String, ByVal dIssue_dat As Date, ByVal dLedger_dat As Date, ByVal sRequest_ty As String, ByVal nUser_sol As Integer, ByVal nUsercode As Integer, ByVal nTypeSupport As Integer, ByVal nDocSupport As Double, ByVal nTax_Percent As Double, ByVal nCurrencyOri As Integer, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nOfficePay As Integer, ByVal nCompany As Integer, ByVal nTaxCode As Integer, ByVal nAfect As Double, ByVal nExent As Double, ByVal nTax_Amount As Double, ByVal nOffice As Integer, ByVal sKey As String, ByVal nAcc_bank As Integer, ByVal nAcc_type As Integer, ByVal sAcco_num As String, ByVal nBank_code As Double, ByVal nBk_agency As Double, ByVal sN_Aba As String, ByVal sOfficeAgen As String, ByVal sAgency As String, Optional ByVal nExternal_Concept As Integer = 0) As Boolean
		
		'**- Variable definition lrecinsPay_SI773
		'- Se define la variable lrecinsPay_SI773
		
		Dim lrecinsPay_SI773 As eRemoteDB.Execute
		lrecinsPay_SI773 = New eRemoteDB.Execute
		
		On Error GoTo insPay_SI773_Err
		
		'**+Parameter definition for stored procedure 'insudb.insPay_SI773'
		'+ Definición de parámetros para stored procedure 'insudb.insPay_SI773'
		
		With lrecinsPay_SI773
			.StoredProcedure = "insPay_SI773"
			.Parameters.Add("sClaims", sClaims, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCase_Nums", sCase_Nums, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeman_Types", sDeman_Types, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClients", sClients, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIds", sIds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", dDat_propos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", dIssue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest_ty", sRequest_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocSupport", nDocSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_percent", nTax_Percent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyOri", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyPay", nCurrencyPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountPay", nAmountPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficepay", nOfficePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxCode", nTaxCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfect", nAfect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExent", nExent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_amount", nTax_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", Left(sKey, 20), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcco_Num", sAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_Agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_Aba", sN_Aba, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOfficeAgen", sOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgency", sAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExternal_Concept", nExternal_Concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPay_SI773 = .Run(False)
		End With
insPay_SI773_Err: 
		If Err.Number Then
			insPay_SI773 = False
		End If
		On Error GoTo 0
		lrecinsPay_SI773 = Nothing
	End Function
	'%Esta funcion realiza el llamado al proceso de pago de siniestro y conciliacion de recibos.
	Public Function insFinish(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal nPayForm As Integer, ByVal nPay_type As Integer, ByVal nServ_Order As Double, ByVal sCoinsuNet As String, ByVal nInvoice As Integer, ByVal dPay_date As Date, ByVal nPay_curr As Integer, ByVal nLoc_exchange As Double, ByVal nLoc_tot_pay As Double, ByVal nUsercode As Integer, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, Optional ByVal nAmountPay As Double = 0, Optional ByVal nRequest_nu As Integer = 0, Optional ByVal sCheque As String = "", Optional ByVal nConsec As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nConcept As Integer = 0, Optional ByVal sClientOP As String = "", Optional ByVal dDat_propos As Date = #12:00:00 AM#, Optional ByVal sDescript As String = "", Optional ByVal dIssue_dat As Date = #12:00:00 AM#, Optional ByVal dLedger_dat As Date = #12:00:00 AM#, Optional ByVal sRequest_ty As String = "", Optional ByVal nSta_cheque As Integer = 0, Optional ByVal dStat_date As Date = #12:00:00 AM#, Optional ByVal nUser_sol As Integer = 0, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal sInter_pay As String = "", Optional ByVal nAcc_type As Integer = 0, Optional ByVal sAcco_num As String = "", Optional ByVal nBank_code As Integer = 0, Optional ByVal nBk_agency As Integer = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sN_Aba As String = "", Optional ByVal nDoc_type As Integer = 0, Optional ByRef dBilldate As Date = #12:00:00 AM#, Optional ByVal sInAcco_num As String = "", Optional ByVal nIn_nBank_Code As Integer = 0, Optional ByVal nTypeTrans As Integer = 0, Optional ByVal nAmountDest As Double = 0, Optional ByVal nCompany As Integer = 0, Optional ByVal nOrig_curr As Integer = 0, Optional ByVal nOrig_amount As Double = 0, Optional ByVal nAfect_amount As Object = Nothing, Optional ByVal nExcent_amount As Object = Nothing, Optional ByVal sCessi_rei As String = "", Optional ByVal nBorederaux As Integer = 0, Optional ByVal dValdate As Date = #12:00:00 AM#) As Boolean
		insFinish = True
		
		If insFinish Then
			
			insFinish = InsPostSI008(nClaim, nCase_num, nDeman_type, nRole, sClient, nPayForm, nPay_type, nServ_Order, sCoinsuNet, nInvoice, dPay_date, nPay_curr, nLoc_exchange, nLoc_tot_pay, nUsercode, nOffice, nOfficeAgen, nAgency, nAmountPay, nRequest_nu, sCheque, nConsec, nAmount, nConcept, sClientOP, dDat_propos, sDescript, dIssue_dat, dLedger_dat, sRequest_ty, nSta_cheque, dStat_date, nUser_sol, nAcc_bank, sInter_pay, nAcc_type, sAcco_num, nBank_code, nBk_agency, nNotenum, sN_Aba, nDoc_type, dBilldate, sInAcco_num, nIn_nBank_Code, nTypeTrans, nAmountDest, nCompany, nOrig_curr, nOrig_amount, nAfect_amount, nExcent_amount, sCessi_rei, nBordereaux, dValdate)
			
		End If
	End Function
	'**% deleteTempSI773: Elimina  los registros del archivo temporal para el listado
	Public Function deleteTempSI773(ByVal sKey As String) As Boolean
		
		'**- Variable definition lrecdeleteTempSI773
		'- Se define la variable lrecdeleteTempSI773
		
		Dim lrecdeleteTempSI773 As eRemoteDB.Execute
		lrecdeleteTempSI773 = New eRemoteDB.Execute
		
		On Error GoTo deleteTempSI773_Err
		
		'**+Parameter definition for stored procedure 'insudb.deleteTempSI773'
		'+ Definición de parámetros para stored procedure 'insudb.deleteTempSI773'
		
		With lrecdeleteTempSI773
			.StoredProcedure = "deleteTempSI773"
			.Parameters.Add("sKey", Left(sKey, 20), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			deleteTempSI773 = .Run(False)
		End With
deleteTempSI773_Err: 
		If Err.Number Then
			deleteTempSI773 = False
		End If
		On Error GoTo 0
		lrecdeleteTempSI773 = Nothing
	End Function
	
	'**% insPay_SI773: Realiza los pagos correspondientes a las rentas
    Public Function insPostSI738(ByVal nClaim As Double, ByVal nPay_type As Integer, ByVal nPay_form As Integer, ByVal nCurrencyOri As Integer, ByVal nCurrencyPay As Integer, ByVal sClient As String, ByVal dPay_date As Date, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal sKey As String) As Boolean
        Dim lrecinsPay_SI738 As eRemoteDB.Execute
        lrecinsPay_SI738 = New eRemoteDB.Execute

        On Error GoTo insPostSI738_Err

        With lrecinsPay_SI738
            .StoredProcedure = "insPostSI738"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Form", nPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencyOri", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencyPay", nCurrencyPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPay_date", dPay_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI738 = .Run(False)
        End With

insPostSI738_Err:
        If Err.Number Then
            insPostSI738 = False
        End If
        On Error GoTo 0
        lrecinsPay_SI738 = Nothing
    End Function

    '
    '%insPostCloseClaim: Realiza el cierre del siniestro
    Public Function insPostCloseClaim(ByVal nClaim As Double, ByVal nIdCasualty As Integer, ByVal nPolicy As Double, ByVal nCover As Integer) As Boolean
        'npolicy
        Dim lupdCurr_acc As eRemoteDB.Execute
        On Error GoTo insPostCloseClaim_Err
        Dim SqlCode As Integer
        Dim SqlError As String
        SqlError = ""
        lupdCurr_acc = New eRemoteDB.Execute
        With lupdCurr_acc
            .StoredProcedure = "SOAPMED.SPCLOSECLAIM"
            '.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_nIdClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nIdCasualty", nIdCasualty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_nIdCasualty", nIdCasualty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_nIdCober", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("R_SQLCode", SqlCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 50, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sqlError", SqlError, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCloseClaim = .Run(False)

            If insPostCloseClaim Then
                SqlCode = .Parameters.Item("R_SQLCode").Value
                SqlError = .Parameters.Item("sqlError").Value
            End If
        End With
        'UPGRADE_NOTE: Object lupdCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lupdCurr_acc = Nothing


insPostCloseClaim_Err:
        If Err.Number Then
            insPostCloseClaim = False
        End If
        On Error GoTo 0
    End Function

    Public Function InsApplyDDR(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDemanType As Integer, sApply_DDR As String) As Boolean
        On Error GoTo InsApplyDDR_err

        Dim lrecInsApplyDDR As New eRemoteDB.Execute

        With lrecInsApplyDDR
            .StoredProcedure = "INSAPPLY_DDR"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDemanType", nDemanType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApply_DDR", sApply_DDR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NEXIST", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsApplyDDR = (.Parameters("NEXIST").Value = 1)
            End If

        End With

InsApplyDDR_err:

        If Err.Number Then
            InsApplyDDR = False
        End If
        On Error GoTo 0

    End Function

End Class






