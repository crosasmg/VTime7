Option Strict Off
Option Explicit On
Public Class Reconocimiento_ingresos
    Public nReceipt As String
    Public nCurrency As Integer

    Public mclsCurren_pol As Curren_pol
    Public nCountCurrency As Integer
    Public nInPrimNetaFP As Double = 0
    Public nInDE As Integer = 0
    Public nInIGV As Integer = 0

    Public bError As Boolean

    Public nError As Integer
    Public nPrimNetaFP As Double = 0

    '%insValCa014: Este metodo se encarga realizar las validaciones masivas correspondientes a la
    '%ventana de coberturas (CA014).
    Public Function insPostCA073(ByVal SPOLITYPE As String, ByVal SCERTYPE As String, ByVal NBRANCH As Integer, ByVal NPRODUCT As Integer, ByVal NPOLICY As Long,
                                ByVal NCERTIF As Integer, ByVal NTYPE As Integer, ByVal NTRATYPEI As Integer, ByVal NPAYNUMBE As Integer, ByVal NPREMIUMN As Double, ByVal NPORCDE As Double, ByVal NPORCIGV As Double, ByRef nStatus As Integer) As String
        Dim lrecinsbtc00015 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insbtc00015_Err

        nStatus = 0
        lrecinsbtc00015 = New eRemoteDB.Execute

        With lrecinsbtc00015
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.SP_GEN_RECEIPTS" '"insbtc00015"
            .Parameters.Add("SPOLITYPE", SPOLITYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCERTYPE", SCERTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", NBRANCH, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", NPRODUCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", NPOLICY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCERTIF", NCERTIF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTYPE", IIf(NTYPE = -1, Nothing, NTYPE), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTRATYPEI", IIf(NTRATYPEI = -1, Nothing, NTRATYPEI), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPAYNUMBE", IIf(NPAYNUMBE = -1, Nothing, NPAYNUMBE), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPREMIUMN", NPREMIUMN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPORCDE", NPORCDE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPORCIGV", NPORCIGV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF
                    If Not nReceipt = "" Then
                        nReceipt = nReceipt + ","
                    End If
                    nReceipt = nReceipt + .FieldToClass("NRECEIPT").ToString
                    .RNext()
                Loop
                .RCloseRec()
                insPostCA073 = nReceipt
            End If
        End With

insbtc00015_Err:
        If Err.Number Then
            Dim strError As String
            strError = Err.Description
            nStatus = 1
            insPostCA073 = strError
        End If

        'UPGRADE_NOTE: Object lrecinsValca014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsbtc00015 = Nothing
        On Error GoTo 0

    End Function

    Public Function insPreCA073(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        insPreCA073 = True

        Call insPreCA073(sCertype, nBranch, nProduct, nPolicy, nCertif)
        '+ Se obtiene las monedas asociadas a la póliza
        mclsCurren_pol = New Curren_pol
        With mclsCurren_pol
            If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                If nCurrency > 0 Then
                    Me.nCurrency = nCurrency
                    .nCurrency = nCurrency
                Else
                    Me.nCurrency = 0
                    If .IsLocal Then
                        Me.nCurrency = 1
                    Else
                        Call .Val_Curren_pol(0)
                    End If
                    Me.nCurrency = .nCurrency
                End If
                Me.nCountCurrency = .CountCurrenPol + 1
            Else
                insPreCA073 = False
                '+ 3738: La póliza no tiene monedas asignadas
                Me.nError = 3738
                Me.bError = True
            End If
        End With

    End Function

    Public Function InsValCA073(ByVal sPrimEst As Double)
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        lobjErrors = New eFunctions.Errors
        InsValCA073 = ""
        If sPrimEst <= 0 Then
            With lobjErrors
                .ErrorMessage("CA073", 6600, , , , , "")
                InsValCA073 = .Confirm()
            End With
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    Public Sub insPreCA073(ByVal SCERTYPE As String, ByVal NBRANCH As Integer, ByVal NPRODUCT As Integer, ByVal NPOLICY As Long,
                            ByVal NCERTIF As Integer)
        Dim lrecinsPreCA073 As eRemoteDB.Execute
        Dim lstrError As String = String.Empty

        lrecinsPreCA073 = New eRemoteDB.Execute

        With lrecinsPreCA073
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.SP_REA_ESTIMADO" '"insPreCA073"
            .Parameters.Add("SCERTYPE", SCERTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", NBRANCH, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", NPRODUCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", NPOLICY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCERTIF", NCERTIF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF
                    Me.nInPrimNetaFP = Convert.ToDouble(.FieldToClass("NPREMIUMN").ToString)
                    Me.nInDE = Convert.ToDouble(.FieldToClass("NPORCDE").ToString)
                    Me.nInIGV = Convert.ToDouble(.FieldToClass("NPORCIGV").ToString)
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
        lrecinsPreCA073 = Nothing
    End Sub

    Public Function reaBillGen(ByVal NBRANCH As Integer, ByVal NPRODUCT As Integer, ByVal NPOLICY As Long) As String
        Dim lrecreaBillGen As eRemoteDB.Execute
        Dim lstrError As String = String.Empty

        lrecreaBillGen = New eRemoteDB.Execute

        With lrecreaBillGen
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.SP_REA_BILL" '"insPreCA073"
            '.Parameters.Add("SCERTYPE", SCERTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", NBRANCH, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", NPRODUCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", NPOLICY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SBILL", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters("SBILL").Value <> String.Empty Then
                    reaBillGen = .Parameters("SBILL").Value
                Else
                    reaBillGen = String.Empty
                End If
            Else
                reaBillGen = String.Empty
            End If
        End With
        lrecreaBillGen = Nothing
    End Function

    Public Function genRecieptInd(ByVal SCERTYPE As String, ByVal NBRANCH As Integer, ByVal NPRODUCT As Integer, ByVal NPOLICY As Long,
                                    ByVal NCERTIF As Integer, ByVal NRECEIPT As Long) As String
        Dim lrecreaBillGen As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo genRecieptInd_Err

        lrecreaBillGen = New eRemoteDB.Execute

        With lrecreaBillGen
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.SP_GEN_BILL_IND"
            .Parameters.Add("SCERTYPE", SCERTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", NBRANCH, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", NPRODUCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", NPOLICY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCERTIF", NCERTIF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NRECEIPT", NRECEIPT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                genRecieptInd = ""
            Else
                genRecieptInd = "Error en generación de recibos."
            End If
        End With
genRecieptInd_Err:
        If Err.Number Then
            Dim strError As String
            strError = Err.Description
            genRecieptInd = strError
        End If

        'UPGRADE_NOTE: Object lrecinsValca014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaBillGen = Nothing
        On Error GoTo 0
    End Function
End Class
