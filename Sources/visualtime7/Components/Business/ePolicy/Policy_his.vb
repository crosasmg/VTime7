Option Strict Off
Option Explicit On
Public Class Policy_his
    '%-------------------------------------------------------%'
    '% $Workfile:: Policy_his.cls                           $%'
    '% $Author:: Mvazquez                                   $%'
    '% $Date:: 29/03/06 18:16                               $%'
    '% $Revision:: 2                                        $%'
    '%-------------------------------------------------------%'

    '+ Tipos de movimiento de la historia de la poliza
    Public Enum ePolicyHisType
        ePolHisTypePolicyMod = 11 '+ Modifcacion de poliza
        ePolHisTypeCertificatMod = 12 '+ Modificacion de certificado
        ePolHisTypeReverMod = 25 '+ Modificacion reversada
        ePolHisTypePolicyNulling = 29 '+ Anulacion de poliza
        ePolHisTypeCertificatNulling = 30 '+ Anulacion de poliza
        ePolHisTypePartialSurrender = 46 '+ Rescate parcial
        ePolHisTypeTotalSurrender = 47 '+ Rescate total
        ePolHisTypeCapitalReducion = 48 '+ Reduccion de capital
        ePolHisTypeDurationReducion = 49 '+ Reduccion de vigencia
        ePolHisTypePolicyLoan = 50 '+ Anticipo sobre poliza
        ePolHisTypeSettled = 66 '+ Saldado Automatico
    End Enum

    '**+Properties according to the table in the system on 11/06/2000
    '**+ The key fields correspond to sCertype, nBranch, nProduct, nPolicy, nCertif, nMovement
    '+ Propiedades según la tabla en el sistema el 06/11/2000
    '+ Los campos llaves corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, nMovement
    '+  Column name                Type                 Computed Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
    '+  -------------------------  -------------------- -------- ------ ----  ----- -------- ------------------ ---------------------
    Public sCertype As String 'char     no       1                  no       no                 no
    Public nBranch As Integer 'smallint no       2      5     0     no       (n/a)              (n/a)
    Public nProduct As Integer 'smallint no       2      5     0     no       (n/a)              (n/a)
    Public nPolicy As Double 'int      no       4      10    0     no       (n/a)              (n/a)
    Public nCertif As Double 'int      no       4      10    0     no       (n/a)              (n/a)
    Public nMovement As Integer 'int      no       4      10    0     no       (n/a)              (n/a)
    Public dCompdate As Date 'datetime no       8                  yes      (n/a)              (n/a)
    Public nClaim As Double 'int      no       4      10    0     yes      (n/a)              (n/a)
    Public nCurrency As Integer 'smallint no       2      5     0     yes      (n/a)              (n/a)
    Public dEffecdate As Date 'datetime no       8                  yes      (n/a)              (n/a)
    Public sNull_move As String 'char     no       1                  yes      no                 yes
    Public dNulldate As Date 'datetime no       8                  yes      (n/a)              (n/a)
    Public nReceipt As Integer 'int      no       4      10    0     yes      (n/a)              (n/a)
    Public nTransactio As Integer 'int      no       4      10    0     yes      (n/a)              (n/a)
    Public nType As ePolicyHisType '  no       2      5     0     yes      (n/a)              (n/a)
    Public nUsercode As Integer 'smallint no       2      5     0     yes      (n/a)              (n/a)
    Public dLedgerDat As Date 'datetime no       8                  yes      (n/a)              (n/a)
    Public nOficial_p As Integer 'int      no       4      10    0     yes      (n/a)              (n/a)
    Public nType_amend As Integer 'Number   no       5
    Public nServ_order As Double 'Number   no      10
    Public dFer As Date 'date     no
    Public nProponum As Double 'number   no       2      10    0     yes
    Public nNotenum As Integer 'number   no       2      10    0     yes
    Public nType_Hist As Integer 'Number   no       5
    Public sIntermei As String 'Char     1
    Public nAgency As Integer 'Number   no       5

    Public nWaitCode As Integer
    Public nWait_code As Integer
    Public nNo_convers As Integer 'Number   no       5
    Public nStatquota As Integer 'Number   no       5

    '+ Define the auxiliary variables
    '+ Se definen la variables auxiliares
    Public dReahdate As Date
    Public nCertificat As Double
    Public nNullcode As Integer
    Public dRescuedate As Date
    Public sDesctran As String
    Public sDescurr As String
    Public sDescType_amend As String

    Public sCliename As String

    Public nPay_day As Integer

    Private mlngTransactio As Integer
    Private mintMovement As Integer

    Public sTextMessage As String
    Public blnWaitCode As Boolean
    Public nPrintNow As Short
    Public blnCarnetsNow As Boolean
    Public blnDocQuotation As Boolean
    Public blnCertif As Boolean
    Public blnVisibleCarnetsNow As Boolean
    Public nPayfreq As Integer

    Public sFile_report As String
    Public sDesProduct As String
    Public sDesBranch As String
    Public nCountRegist As Double
    Public sDescType_Hist As String
    Public sProcess_Num As String

    Public sPolitype As String
	
    '%insCountMov: Permite contar la cantidad de movimientos a una fecha de la poliza
    Public Function insCountMov(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nType_Hist As ePolicyHisType = eRemoteDB.Constants.intNull) As Integer
        Dim lrecreaPolicy_his_countmov As eRemoteDB.Execute
        Dim llngCount As Integer
        On Error GoTo reaPolicy_his_countmov_Err

        lrecreaPolicy_his_countmov = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaPolicy_his_countmov al 08-28-2002 12:27:26
        '+
        With lrecreaPolicy_his_countmov
            .StoredProcedure = "reaPolicy_his_countmov"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_hist", nType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", llngCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            insCountMov = .Parameters("nCount").Value

        End With

reaPolicy_his_countmov_Err:
        If Err.Number Then
            insCountMov = 0
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_countmov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_countmov = Nothing
        On Error GoTo 0
    End Function

    '% FindLastMovementByType: Busca el ultimo movimiento de un cierto tipo
    Public Function FindLastMovementByType(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_Hist As Integer) As Boolean
        Dim lrecreaPolicy_his_type_last As eRemoteDB.Execute

        On Error GoTo reaPolicy_his_type_last_Err
        lrecreaPolicy_his_type_last = New eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_type_last al 12-06-2001 10:50:41
        With lrecreaPolicy_his_type_last
            .StoredProcedure = "reaPolicy_his_type_last"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_hist", nType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindLastMovementByType = True
                Me.sCertype = sCertype
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nPolicy = nPolicy
                Me.nCertif = nCertif
                Me.nMovement = .FieldToClass("nMovement")
                Me.nClaim = .FieldToClass("nClaim")
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.sNull_move = .FieldToClass("sNull_move")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.nReceipt = .FieldToClass("nReceipt")
                Me.nTransactio = .FieldToClass("nTransactio")
                Me.nType = .FieldToClass("nType")
                Me.dLedgerDat = .FieldToClass("dLedgerdat")
                Me.nOficial_p = .FieldToClass("nOficial_p")
                Me.nType_amend = .FieldToClass("nType_amend")
                Me.nServ_order = .FieldToClass("nServ_order")
                Me.nProponum = .FieldToClass("nProponum")
                .RCloseRec()
            End If
        End With

reaPolicy_his_type_last_Err:
        If Err.Number Then
            FindLastMovementByType = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicy_his_type_last may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_type_last = Nothing
    End Function
    '% FindLastMovementByTypes: Busca el ultimo movimiento de un cierto tipo
    Public Function FindLastMovementByTypes(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sType_Hist As String) As Boolean
        Dim lrecreaPolicy_his_type_last As eRemoteDB.Execute

        On Error GoTo FindLastMovementByTypes_Err
        lrecreaPolicy_his_type_last = New eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_type_last al 12-06-2001 10:50:41
        With lrecreaPolicy_his_type_last
            .StoredProcedure = "reaPolicy_his_types_lasts"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_hist", sType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindLastMovementByTypes = True
                Me.sCertype = sCertype
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nPolicy = nPolicy
                Me.nCertif = nCertif
                Me.nMovement = .FieldToClass("nMovement")
                Me.nClaim = .FieldToClass("nClaim")
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.sNull_move = .FieldToClass("sNull_move")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.nReceipt = .FieldToClass("nReceipt")
                Me.nTransactio = .FieldToClass("nTransactio")
                Me.nType = .FieldToClass("nType")
                Me.dLedgerDat = .FieldToClass("dLedgerdat")
                Me.nOficial_p = .FieldToClass("nOficial_p")
                Me.nType_amend = .FieldToClass("nType_amend")
                Me.nServ_order = .FieldToClass("nServ_order")
                Me.nProponum = .FieldToClass("nProponum")
                .RCloseRec()
            End If
        End With

FindLastMovementByTypes_Err:
        If Err.Number Then
            FindLastMovementByTypes = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicy_his_type_last may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_type_last = Nothing
    End Function

    '% FindPropType_Hist: Busca el ultimo movimiento
    Public Function FindPropType_Hist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_Hist As Integer) As Boolean
        Dim lrecreaFindPropType_Hist As eRemoteDB.Execute

        On Error GoTo FindPropType_Hist_Err
        lrecreaFindPropType_Hist = New eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_type_last al 12-06-2001 10:50:41
        With lrecreaFindPropType_Hist
            .StoredProcedure = "REAPROPTYPE_HIST"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_hist", nType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Me.nProponum = .Parameters("nProponum").Value
                FindPropType_Hist = True
            End If

        End With

FindPropType_Hist_Err:
        If Err.Number Then
            FindPropType_Hist = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaFindPropType_Hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFindPropType_Hist = Nothing
    End Function

    '**%insCrePolicy_his: This function is in charge of adding one record in the History table
    '**%                   of a policy (policy_his)
    '%insCrePolicy_his: Esta función se encarga de agregar un registro en la tabla de
    '%                 Historia de una póliza (policy_his)
    Public Function insCrePolicy_his() As Boolean
        Dim lrecinsCrepolicy_his As eRemoteDB.Execute

        On Error GoTo insCrepolicy_his_Err
        lrecinsCrepolicy_his = New eRemoteDB.Execute

        '+ Definición de store procedure insCrepolicy_his al 03-15-2002 13:38:31
        With lrecinsCrepolicy_his
            .StoredProcedure = "insCrepolicy_his"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("iN_nmovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("iN_snull_move", sNull_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficial_p", nOficial_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", dLedgerDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insCrePolicy_his = .Run(False)
        End With

insCrepolicy_his_Err:
        If Err.Number Then
            insCrePolicy_his = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCrepolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCrepolicy_his = Nothing
    End Function
    '%updPolHisnwait_code : Procedimiento que realiza las actualizaciones respectivas del histórico de la póliza, actualizando nWait_code
    Public Function updPolHisnWait_code(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal nWait_code As Integer, ByVal mdtmnMovnumbe As Integer) As Boolean
        Dim lrecupdPolHisnWait_code As eRemoteDB.Execute

        On Error GoTo updPolHisnWait_code_Err

        lrecupdPolHisnWait_code = New eRemoteDB.Execute

        With lrecupdPolHisnWait_code
            .StoredProcedure = "updPolHisnwait_code"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nMovement", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", mdtmnMovnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            updPolHisnWait_code = .Run(False)
        End With

updPolHisnWait_code_Err:
        If Err.Number Then
            updPolHisnWait_code = False
        End If
        'UPGRADE_NOTE: Object lrecupdPolHisnWait_code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolHisnWait_code = Nothing
        On Error GoTo 0
    End Function

    '%updPolHisnotenum : Procedimiento que realiza las actualizaciones respectivas del histórico de la póliza, actualizando nNotenum,
    Public Sub updPolHisnNotenum(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal nNotenum As Integer, Optional ByVal nWait_code As Integer = 0, Optional ByVal nStatquota As Integer = 0)
        Dim lrecupdPolicy_his_nNotenum As eRemoteDB.Execute

        On Error GoTo updPolHisnNotenum_Err

        lrecupdPolicy_his_nNotenum = New eRemoteDB.Execute

        With lrecupdPolicy_his_nNotenum
            .StoredProcedure = "updPolicy_His_nNotenum"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

updPolHisnNotenum_Err:
        'UPGRADE_NOTE: Object lrecupdPolicy_his_nNotenum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_his_nNotenum = Nothing
        On Error GoTo 0
    End Sub

    '**%updPolHisNulldate : Procedure that makes the respective update of the policy history, update dnulldate
    '%updPolHisNulldate : Procedimiento que realiza las actualizaciones respectivas del histórico de la póliza, actualizando dnulldate,
    Public Sub updPolHisNulldate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer)
        Dim lrecupdPolicy_his_nulldate As eRemoteDB.Execute
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo updPolHisNulldate_Err

        lclsPolicy = New ePolicy.Policy
        lrecupdPolicy_his_nulldate = New eRemoteDB.Execute

        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)

        '**Parameters definition for the stored procedure 'insudb.updPolicy_his_nulldate'
        '** Data read on 11/30/1999 03:37:15 PM
        'Definición de parámetros para stored procedure 'insudb.updPolicy_his_nulldate'
        'Información leída el 30/11/1999 03:37:15 PM

        With lrecupdPolicy_his_nulldate
            .StoredProcedure = "updPolicy_his_nulldate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", lclsPolicy.dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", lclsPolicy.nMov_histor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

updPolHisNulldate_Err:
        'UPGRADE_NOTE: Object lrecupdPolicy_his_nulldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_his_nulldate = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Sub


    '**% Find_Movement: Verifies the previous cash flows of a policy
    '%Find_Movement: verifica los movimiento anteriores de una póliza
    Public Function Find_Movement(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaPolicy_his_av As eRemoteDB.Execute

        '**+ Parameters definition for the stored porcedure 'insudb.reaPolicy_his_av'
        '**+ Data read on 11/06/2000 06:06:37 p.m.
        '+ Definición de parámetros para stored procedure 'insudb.reaPolicy_his_av'
        '+ Información leída el 06/11/2000 06:06:37 p.m.
        On Error GoTo Find_Movement_Err
        lrecreaPolicy_his_av = New eRemoteDB.Execute
        With lrecreaPolicy_his_av
            .StoredProcedure = "reaPolicy_his_av"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Find_Movement = .Parameters("nExist").Value > 0
        End With

Find_Movement_Err:
        If Err.Number Then
            Find_Movement = True
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPolicy_his_av may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_av = Nothing
    End Function

    '%Find_Policy_his_nNotenum: verifica la existencia de numero de nota para movimiento histórico de una póliza
    Public Function Find_Policy_his_nNotenum(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaPolicy_his_nNotenum As eRemoteDB.Execute

        lrecreaPolicy_his_nNotenum = New eRemoteDB.Execute

        On Error GoTo Find_Policy_his_nNotenum_Err

        '+ Definición de parámetros para stored procedure 'reaPolicy_his_nnotenum'
        '+ Información leída el 12/08/2002 11:00:00 p.m.

        With lrecreaPolicy_his_nNotenum
            .StoredProcedure = "reaPolicy_his_nnotenum"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find_Policy_his_nNotenum = True
                Me.nNotenum = .FieldToClass("nNotenum")
                .RCloseRec()
            Else
                Find_Policy_his_nNotenum = False
            End If
        End With

Find_Policy_his_nNotenum_Err:
        If Err.Number Then
            Find_Policy_his_nNotenum = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_nNotenum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_nNotenum = Nothing
        On Error GoTo 0
    End Function

    '**%Update: This method updates the records of the collection in the table "Policy_his"
    '%Update: Permite actualizar los registros de la colección en la tabla "Policy_his"
    '
    Public Function Update_Policyhis(ByVal nType As Integer, Optional ByRef lclsPolicy As Policy = Nothing) As Boolean
        Dim llngCount As Integer
        Dim llngCurrency As Integer
        Dim lclsCurren_pol As Curren_pol
        Dim lcolCur_allow As eProduct.Cur_Allows

        On Error GoTo insPolicy_his_Err
        '+Esta variable no se puede setear a Nothing. Milko
        If lclsPolicy Is Nothing Then
            lclsPolicy = New ePolicy.Policy
        End If

        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            lclsCurren_pol = New ePolicy.Curren_pol
            With lclsCurren_pol
                If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                    If .CountCurrenPol = 0 Then
                        .Val_Curren_pol(0)
                        llngCurrency = .nCurrency
                    Else
                        llngCurrency = eRemoteDB.Constants.intNull
                    End If
                Else
                    lcolCur_allow = New eProduct.Cur_Allows
                    If lcolCur_allow.Find_CA001(nBranch, nProduct) Then
                        If lcolCur_allow.Count = 1 Then
                            llngCurrency = lcolCur_allow(1).nCurrency
                        Else
                            llngCurrency = 1
                        End If
                    Else
                        llngCurrency = 1
                    End If
                    'UPGRADE_NOTE: Object lcolCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lcolCur_allow = Nothing
                End If
            End With

            nCurrency = llngCurrency
            If nType = 54 Or nType = 55 Then
                dNulldate = dNulldate
            Else
                If nType = 11 Then
                    dNulldate = lclsPolicy.dExpirdat
                Else
                    dNulldate = eRemoteDB.Constants.dtmNull
                End If
            End If
            nTransactio = IIf(lclsPolicy.nTransactio = eRemoteDB.Constants.intNull, 0, lclsPolicy.nTransactio)
            Me.nType = nType
            nMovement = IIf(lclsPolicy.nMov_histor = eRemoteDB.Constants.intNull, 0, lclsPolicy.nMov_histor)

            If Update Then
                Update_Policyhis = True
                lclsPolicy.nMov_histor = mintMovement
                lclsPolicy.nTransactio = mlngTransactio
            End If
        End If

insPolicy_his_Err:
        If Err.Number Then
            Update_Policyhis = False
        End If
        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing
        On Error GoTo 0
    End Function

    '**+ FindLastMovement: makes the reading of the last movement created in Policy_his
    '+ FindLastMovement: Realiza la lectura del último movimiento creado en Policy_his
    Public Function FindLastMovement(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecreaPolicy_his_1 As eRemoteDB.Execute

        On Error GoTo FindLastMovement_Err

        lrecreaPolicy_his_1 = New eRemoteDB.Execute

        '+**Parameters definition for the stored porcedure 'insudb.reaPolicy_his_1'
        '+** Data read on 11/27/2000 03:04:29 PM
        '+Definición de parámetros para stored procedure 'insudb.reaPolicy_his_1'
        '+Información leída el 27/11/2000 03:04:29 PM

        With lrecreaPolicy_his_1
            .StoredProcedure = "reaPolicy_his_1"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                FindLastMovement = True

                Me.sCertype = sCertype
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nPolicy = nPolicy
                Me.nCertif = nCertif
                nMovement = .FieldToClass("nMovement")
                nClaim = .FieldToClass("nClaim")
                nCurrency = .FieldToClass("nCurrency")
                dEffecdate = .FieldToClass("dEffecdate")
                sNull_move = .FieldToClass("sNull_Move")
                dNulldate = .FieldToClass("dNulldate")
                nReceipt = .FieldToClass("nReceipt")
                nTransactio = .FieldToClass("nTransactio")
                nType = .FieldToClass("nType")
                dLedgerDat = .FieldToClass("dLedgerDat")
                nOficial_p = .FieldToClass("nOficial_p")
                nType_amend = .FieldToClass("nType_amend")
                nServ_order = .FieldToClass("nServ_order")
                nProponum = .FieldToClass("nProponum")
                nType_Hist = .FieldToClass("nType_Hist")

                .RCloseRec()
            Else
                FindLastMovement = False
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaPolicy_his_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_1 = Nothing

FindLastMovement_Err:
        If Err.Number Then
            FindLastMovement = False
        End If
        On Error GoTo 0

    End Function
    '**%Update_PolCerti: Allows to make the update about the cash flows
    '**% history of a policy
    '% Update_PolCerti: Permite realizar actualizaciones sobre la historia
    '%  de los movimientos de una poliza
    Public Function Update_PolCerti() As Boolean
        Dim lrecupdPolCerti As eRemoteDB.Execute
        On Error GoTo Update_PolCerti_Err
        lrecupdPolCerti = New eRemoteDB.Execute

        '**+ Parameters definition for the stored procedure 'insudb.updPolCerti'
        '**+ Data read on 01/04/2001 14:01:49
        '+Definición de parámetros para stored procedure 'insudb.updPolCerti'
        '+Información leída el 04/01/2001 14:01:49

        With lrecupdPolCerti
            .StoredProcedure = "updPolCerti"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Certificat", nCertificat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("TypeMove", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dReahdate", dReahdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_day", nPay_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_PolCerti = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdPolCerti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolCerti = Nothing

Update_PolCerti_Err:
        If Err.Number Then
            Update_PolCerti = False
        End If
        On Error GoTo 0
    End Function

    '**% Update: updates the table
    '% Update: actualiza la tabla
    Public Function Update() As Boolean
        Dim lrecinsPolicy_his As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecinsPolicy_his = New eRemoteDB.Execute

        Update = False

        '**+ Parameters definition for the stored procedure 'insudb.insPolicy_his'
        '**+ Data read on 01/20/2001 04:16:55 p.m.
        '+ Definición de parámetros para stored procedure 'insudb.insPolicy_his'
        '+ Información leída el 20/01/2001 04:16:55 p.m.

        With lrecinsPolicy_his
            .StoredProcedure = "insPolicy_his"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNull_move", sNull_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerDat", dLedgerDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFer", dFer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                mlngTransactio = IIf(IsDbNull(.Parameters.Item("nTransactio").Value), eRemoteDB.Constants.intNull, .Parameters.Item("nTransactio").Value)
                mintMovement = .Parameters.Item("nMovement").Value
                Update = True
            End If
        End With
        'UPGRADE_NOTE: Object lrecinsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPolicy_his = Nothing

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
    End Function

    '**%insReaLastMovPolicy_his: This function makes the search of the msximum date of the last policy movement
    '%insReaLastMovPolicy_his:Esta funcion realiza la búsqueda de la máxima fecha del último movimiento de la póliza.
    Public Function insReaLastMovPolicy_his(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer) As Date

        Dim lrecreaPolicy_hisLastMov As eRemoteDB.Execute

        lrecreaPolicy_hisLastMov = New eRemoteDB.Execute

        '**+ Parameters definition for the stored procedure 'insudb.reaPolicy_hisLastMov'
        '**+ Data read on 02/21/2001 02:00:34 p.m.
        '+ Definición de parámetros para stored procedure 'insudb.reaPolicy_hisLastMov'
        '+ Información leída el 21/02/2001 02:00:34 p.m.

        With lrecreaPolicy_hisLastMov
            .StoredProcedure = "reaPolicy_hisLastMov"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                If Not .FieldToClass("ldtmEffecdate") = eRemoteDB.Constants.dtmNull Then
                    insReaLastMovPolicy_his = CDate(.FieldToClass("ldtmEffecdate"))
                End If
            End If

        End With
        'UPGRADE_NOTE: Object lrecreaPolicy_hisLastMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_hisLastMov = Nothing

    End Function

    '**% Update_ClaimOccurdat: Allows to make the cash flows update
    '**+ generated in the history of the policy/certificate from the claim
    '**% declaration
    '% Update_ClaimOccurdat: Permite realizar la actualización del movimiento
    '%  generado en la historia de la poliza/certificado a partir de la declaración
    '%  de un siniestro
    Public Function Update_ClaimOccurdat() As Boolean

        Dim lrecupdPolicy_his_ClaimOccurdat As eRemoteDB.Execute

        On Error GoTo Update_ClaimOccurdat_Err

        lrecupdPolicy_his_ClaimOccurdat = New eRemoteDB.Execute

        '**+ Parameters definition for the stored procedure 'insudb.updPolicy_his_ClaimOccurdat'
        '**+ Data read on 11/23/2000 03:37:29 p.m.
        '+Definición de parámetros para stored procedure 'insudb.updPolicy_his_ClaimOccurdat'
        '+Información leída el 23/11/2000 03:37:29 p.m.

        With lrecupdPolicy_his_ClaimOccurdat
            .StoredProcedure = "updPolicy_his_ClaimOccurdat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdat", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update_ClaimOccurdat = .Run(False)

        End With
        'UPGRADE_NOTE: Object lrecupdPolicy_his_ClaimOccurdat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_his_ClaimOccurdat = Nothing

Update_ClaimOccurdat_Err:
        If Err.Number Then
            Update_ClaimOccurdat = False
        End If
    End Function

    '%InsValCAC011: Validaciones de la transacción CAC011, según especificaciones funcionales
    Public Function InsValCAC011(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCertificat As Certificat

        On Error GoTo InsValCAC011_Err
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            '+Validaciones del campo tipo de información
            If sCertype = "0" Then
                .ErrorMessage(sCodispl, 60213)
            End If

            '+Validaciones del campo ramo
            If nBranch = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1022)
            End If

            '+Validaciones del campo producto
            If nProduct = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1014)
            End If

            '+Validaciones del campo póliza
            If nPolicy = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 3003)
            Else
                lclsCertificat = New Certificat
                nCertif = IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif)
                If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                    .ErrorMessage(sCodispl, 1978)
                End If
            End If

            InsValCAC011 = .Confirm
        End With

InsValCAC011_Err:
        If Err.Number Then
            InsValCAC011 = "InsValCAC011: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '% Reference: Retorna el tipo de endoso de la tabla policy_his
    Public Function reaPolicy_his_typeamend(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProponum As Double) As Boolean

        Dim lrecreaPolicy_his_typeamend As eRemoteDB.Execute

        On Error GoTo reaPolicy_his_typeamend_Err

        lrecreaPolicy_his_typeamend = New eRemoteDB.Execute

        With lrecreaPolicy_his_typeamend
            .StoredProcedure = "reaPolicy_his_typeamend"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nType_amend", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                reaPolicy_his_typeamend = True
                Me.nType_amend = .Parameters("nType_amend").Value
            Else
                reaPolicy_his_typeamend = False
            End If
        End With

reaPolicy_his_typeamend_Err:
        If Err.Number Then
            reaPolicy_his_typeamend = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_typeamend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_typeamend = Nothing
        On Error GoTo 0
    End Function
    '% Reference: Retorna el tipo de endoso de la tabla policy_his
    Public Function FindPolicy_his_nproponum(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProponum As Double) As Boolean
        Dim lrecreaPolicy_his_nproponum As eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_nproponum al 10-30-2002 18:44:03
        On Error GoTo reaPolicy_his_nproponum_Err
        lrecreaPolicy_his_nproponum = New eRemoteDB.Execute
        With lrecreaPolicy_his_nproponum
            .StoredProcedure = "reaPolicy_his_nproponum"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                FindPolicy_his_nproponum = .Parameters("nCount").Value <> 0
            End If
        End With

reaPolicy_his_nproponum_Err:
        If Err.Number Then
            FindPolicy_his_nproponum = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_nproponum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_nproponum = Nothing
        On Error GoTo 0
    End Function


    '% Reference: Retorna el número de póliza, póliza-certificado, recibo o siniestro según el tipo
    '%            de movimiento de historia
    Public ReadOnly Property Reference(ByVal nType_Hist As Integer) As String
        Get

            If nType_Hist = 17 Or nType_Hist = 18 Or nType_Hist = 56 Or nType_Hist = 57 Or nType_Hist = 58 Then
                Reference = CStr(Me.nReceipt)
            Else
                If nType_Hist = 42 Then
                    Reference = CStr(Me.nClaim)
                Else
                    Reference = Me.nPolicy & IIf(Me.nCertif = 0, "", "/" & Me.nCertif)
                End If
            End If

        End Get
    End Property

    '% insPreca050: Verifica la transaccion CA050
    Public Function insPreca050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCompany As String, ByVal nTransaction As Short, ByVal nUsercode As Integer, ByVal sJustQuote As String) As Boolean
        Dim lrecinsPreca050 As New eRemoteDB.Execute
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig
        Dim ldblProponum As Double
        Dim lstrMainInsured As String
        Dim sResult As String
        Dim nPolicy_Aux As Double

        On Error GoTo insPreca050_Err

        With lrecinsPreca050
            .StoredProcedure = "insPreca050"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompany", sCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTextMessage", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWaitCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnWaitCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrintNow", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnCarnetsNow", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnDocQuotation", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nblnVisibleCarnetsNow", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMainInsured", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sJustQuote", sJustQuote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Me.sTextMessage = .Parameters.Item("sTextMessage").Value
                Me.nWaitCode = .Parameters.Item("nWaitCode").Value
                Me.blnWaitCode = .Parameters.Item("nblnWaitCode").Value = 0
                Me.nPrintNow = .Parameters.Item("nPrintNow").Value
                Me.blnCarnetsNow = .Parameters.Item("nblnCarnetsNow").Value = 1
                Me.blnDocQuotation = .Parameters.Item("nblnDocQuotation").Value = 1
                Me.blnCertif = .Parameters.Item("nblnCertif").Value = 1
                Me.blnVisibleCarnetsNow = .Parameters.Item("nblnVisibleCarnetsNow").Value = 1
                .RCloseRec()

                '+Se busca si se debe realizar el manejo PEP
                If lclsConfig.LoadSetting("Active", "No", "HandlingExtensions") = "Yes" AndAlso sCertype = "2" AndAlso Me.nWaitCode <= 0 Then

                    lstrMainInsured = .Parameters.Item("sMainInsured").Value
                    ldblProponum = .Parameters.Item("nProponum").Value

                    If ldblProponum > 0 Then
                        nPolicy_Aux = ldblProponum
                    Else
                        nPolicy_Aux = nPolicy
                    End If

                    sResult = SearchStatusNotification(nBranch, nProduct, nPolicy_Aux, lstrMainInsured)


                    'Correcto, así debe ser
                    'Si no retorna estado = continua flujo
                    'Si retorna estado 
                    'Estado “APROBADO” = continua flujo
                    'Estado “INGRESO”, “PREAPROB”, “RECHAZADO” = No continua flujo
                    If Not (sResult.Trim().ToUpper() = "APROBADO" Or String.IsNullOrEmpty(sResult.Trim())) Then
                        Me.sTextMessage = "Verificación PEP: " & sResult.Trim
                        Me.nWaitCode = 50
                    End If
                End If

            End If
        End With

insPreca050_Err:
        If Err.Number Then
            insPreca050 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPreca050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPreca050 = Nothing
    End Function


    '% Class_Initialize: se controla el acceso a la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Call ClearFields()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% ClearFields: se inicializa el valor de las variables de la clase
    Private Sub ClearFields()
        sCertype = String.Empty
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        nMovement = eRemoteDB.Constants.intNull
        dCompdate = eRemoteDB.Constants.dtmNull
        nClaim = eRemoteDB.Constants.intNull
        nCurrency = eRemoteDB.Constants.intNull
        dEffecdate = eRemoteDB.Constants.dtmNull
        sNull_move = String.Empty
        dNulldate = eRemoteDB.Constants.dtmNull
        nReceipt = eRemoteDB.Constants.intNull
        nTransactio = eRemoteDB.Constants.intNull
        nType = eRemoteDB.Constants.intNull
        nUsercode = eRemoteDB.Constants.intNull
        dLedgerDat = eRemoteDB.Constants.dtmNull
        nOficial_p = eRemoteDB.Constants.intNull
        nType_amend = eRemoteDB.Constants.intNull
        nServ_order = eRemoteDB.Constants.intNull
        dFer = eRemoteDB.Constants.dtmNull
        nProponum = eRemoteDB.Constants.intNull
        dReahdate = eRemoteDB.Constants.dtmNull
        nCertificat = eRemoteDB.Constants.intNull
        nNullcode = eRemoteDB.Constants.intNull
        dRescuedate = eRemoteDB.Constants.dtmNull
        mlngTransactio = eRemoteDB.Constants.intNull
        mintMovement = eRemoteDB.Constants.intNull
        sTextMessage = String.Empty
        nWaitCode = eRemoteDB.Constants.intNull
        blnWaitCode = False
        nPrintNow = eRemoteDB.Constants.intNull
        blnCarnetsNow = False
        blnDocQuotation = False
        blnCertif = False
        blnVisibleCarnetsNow = False

    End Sub

    '% insPreca050: Verifica la transaccion CA050
    Public Function DelRecordType_policy_his(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_Hist As Integer) As Boolean
        Dim lrecdelRecordtype_policy_his As eRemoteDB.Execute
        On Error GoTo delRecordtype_policy_his_Err

        lrecdelRecordtype_policy_his = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure delRecordtype_policy_his al 02-03-2004 12:04:46
        '+
        With lrecdelRecordtype_policy_his
            .StoredProcedure = "delRecordtype_policy_his"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_hist", nType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            DelRecordType_policy_his = .Run(False)
        End With

delRecordtype_policy_his_Err:
        If Err.Number Then
            DelRecordType_policy_his = False
        End If
        'UPGRADE_NOTE: Object lrecdelRecordtype_policy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelRecordtype_policy_his = Nothing
        On Error GoTo 0

    End Function

    '% insPreca050: Verifica la transaccion CA050
    Public Function reapolicy_hisdate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_Hist As Integer) As Date
        Dim lrecreapolicy_hisdate As eRemoteDB.Execute

        On Error GoTo reapolicy_hisdate_Err

        lrecreapolicy_hisdate = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reapolicy_hisdate al 02-03-2004 12:04:46
        '+
        With lrecreapolicy_hisdate
            .StoredProcedure = "insreapolicy_hisdate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_hist", nType_Hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                reapolicy_hisdate = .Parameters("dEffecdate").Value
            Else
                reapolicy_hisdate = eRemoteDB.Constants.dtmNull
            End If
        End With

reapolicy_hisdate_Err:
        If Err.Number Then
            reapolicy_hisdate = eRemoteDB.Constants.dtmNull
        End If
        'UPGRADE_NOTE: Object lrecreapolicy_hisdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreapolicy_hisdate = Nothing
        On Error GoTo 0

    End Function

    '+ FindLast_order: Realiza la lectura del último movimiento creado en Policy_his para una orden de servicio
    Public Function FindLast_order(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProponum As Double) As Boolean
        Dim lrecreaPolicy_his_2 As eRemoteDB.Execute

        On Error GoTo FindLast_order_Err

        lrecreaPolicy_his_2 = New eRemoteDB.Execute

        '+**Parameters definition for the stored porcedure 'insudb.reaPolicy_his_1'
        '+** Data read on 11/27/2000 03:04:29 PM
        '+Definición de parámetros para stored procedure 'insudb.reaPolicy_his_1'
        '+Información leída el 27/11/2000 03:04:29 PM

        With lrecreaPolicy_his_2
            .StoredProcedure = "reaPolicy_his_2"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                FindLast_order = True
                '**+ The values in the store procedure to the public
                '**+variables of the class are assigned - ACM - 11/27/2000
                '+ Se asignan los valores obtenidos en el Stored Procedure a las variables
                '+ públicas de la clase - ACM - 27/11/2000
                Do While Not .EOF
                    Me.sCertype = sCertype
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nPolicy = nPolicy
                    Me.nCertif = nCertif
                    nMovement = .FieldToClass("nMovement")
                    nClaim = .FieldToClass("nClaim")
                    nCurrency = .FieldToClass("nCurrency")
                    dEffecdate = .FieldToClass("dEffecdate")
                    sNull_move = .FieldToClass("sNull_Move")
                    dNulldate = .FieldToClass("dNulldate")
                    nReceipt = .FieldToClass("nReceipt")
                    nTransactio = .FieldToClass("nTransactio")
                    nType = .FieldToClass("nType")
                    dLedgerDat = .FieldToClass("dLedgerDat")
                    nOficial_p = .FieldToClass("nOficial_p")
                    nType_amend = .FieldToClass("nType_amend")
                    nServ_order = .FieldToClass("nServ_order")
                    nProponum = .FieldToClass("nProponum")
                    nType_Hist = .FieldToClass("nType_Hist")
                    .RNext()
                Loop
                .RCloseRec()
            Else
                FindLast_order = False
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaPolicy_his_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_his_2 = Nothing

FindLast_order_Err:
        If Err.Number Then
            FindLast_order = False
        End If
        On Error GoTo 0

    End Function

    Public Function SearchStatusNotification(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal sClient As String) As String
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig
        Dim lclsPolicy As New ePolicy.Policy
        Dim sMessage As String = String.Empty
        Dim bTrace As Boolean = lclsConfig.LoadSetting("Trace", "Yes", "VerificationPEP") = "Yes"
        Dim status As String
        Dim sEquivalentProduct As String = lclsPolicy.EquivalentFieldToClass("nProduct", nBranch, nProduct)

        Try
            sMessage += "Comenzando Bloque PEP. "
            Dim asb As System.Reflection.Assembly
            sMessage += "Cargando Assembly. "
            asb = System.Reflection.Assembly.LoadFrom(lclsConfig.LoadSetting("DllFullPath", "", "VerificationPEP"))
            sMessage += "ok. Instanciando clase "
            Dim cls As Object = asb.CreateInstance("CorpvidaIntegration.PEPClient")
            sMessage += "ok. "

            If Not cls Is Nothing Then
                sMessage += "objeto instanciado. "
            Else
                sMessage += "Objeto es nothing. "
            End If
            sMessage += "Asignando EndPoint. "
            cls.RemoteAddress = lclsConfig.LoadSetting("WSEndPoint", "", "VerificationPEP")
            sMessage += "ok. Invocando consulta"

            sMessage += "Producto asignado: " & sEquivalentProduct & " Asignando negocio " & CStr(nPolicy)

            sMessage += "Poliza asignada: " & CStr(nPolicy) & " Invocando Buscaestadonot... " & sClient


            status = cls.CheckPEPStatus(sClient, sEquivalentProduct, CStr(nPolicy))

            sMessage += "Luego de invocar, sin excepcion. Status:" & status

            If bTrace Then
                Throw New Exception("Invocacion PEP sin problemas: " & "-" & "status:" & status & ". " & sMessage)
            Else
                SearchStatusNotification = status
            End If

        Catch ex As Exception

            If lclsConfig.LoadSetting("IgnoreError", "Yes", "HandlingExtensions") = "No" Then
                If bTrace Then
                    SearchStatusNotification = "Error en Dll. " & ex.Message & ".Origen:" & ex.Source & ".Traza:" & sMessage
                Else
                    SearchStatusNotification = "Error en Dll." & ex.Message & ".Origen:" & ex.Source
                End If
            Else
                SearchStatusNotification = String.Empty
            End If
        End Try

    End Function
    Public Function FindPolicy_His_nNovement(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_Hist As Integer) As Boolean
        Dim lrecreaFindPolicy_His_nNovement As eRemoteDB.Execute

        On Error GoTo FindPolicy_His_nNovement_Err
        lrecreaFindPolicy_His_nNovement = New eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_type_last al 12-06-2001 10:50:41
        With lrecreaFindPolicy_His_nNovement
            .StoredProcedure = "REAPROPTYPE_HIST"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Me.sFile_report = .Parameters("sReport").Value
                FindPolicy_His_nNovement = True
            End If

        End With

FindPolicy_His_nNovement_Err:
        If Err.Number Then
            FindPolicy_His_nNovement = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaFindPropType_Hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFindPolicy_His_nNovement = Nothing
    End Function


    '*%insCreaPolicy_his_v2: This function is in charge of adding one record in the History table
    '*%                   of a policy (policy_his)
    '%insCreaPolicy_his_v2: Esta función se encarga de agregar un registro en la tabla de
    '%                 Historia de una póliza (policy_his)
    Public Function insCreaPolicy_his_v2(ByVal sCertype_aux As String, ByVal nBranch_aux As Integer, ByVal nProduct_aux As Integer, ByVal nPolicy_aux As Double, ByVal dEffecdate_aux As Date, ByVal nTransactio_aux As Integer, ByVal nUserCode_aux As Integer, ByVal nCertif_aux As Integer, ByVal nTypeReport_aux As Integer) As Boolean
        Dim lrecinsCreapolicy_his As eRemoteDB.Execute
        Dim nMovement_aux As Integer 'Variable para recepcionar movimiento

        On Error GoTo insCreapolicy_his_v2_Err
        lrecinsCreapolicy_his = New eRemoteDB.Execute

        '+ Definición de store procedure insCrepolicy_his_V2 al 03-15-2002 13:38:31
        With lrecinsCreapolicy_his
            .StoredProcedure = "insCreapolicy_his_v2"
            .Parameters.Add("sCertype", sCertype_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Report", nTypeReport_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement_Aux", nMovement_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nMovement_aux = .Parameters("nMovement_Aux").Value
                nMovement = nMovement_aux
                insCreaPolicy_his_v2 = True
            End If
        End With

insCreapolicy_his_v2_Err:
        If Err.Number Then
            insCreaPolicy_his_v2 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCrepolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCreapolicy_his = Nothing
    End Function

End Class
