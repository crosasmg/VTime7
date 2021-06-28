Public Class FinancialInstrument

#Region "Properties"

    Public Property DEFFECDATE As Date
    Public Property NCONSECUTIVE As Integer
    Public Property NBANK_CODE As Integer
    Public Property NINSTRUMENT_TY As Integer
    Public Property NCARD_TYPE As Integer
    Public Property SNUMBER As String
    Public Property DCARDEXPIR As Date
    Public Property DSTARTDATE As Date
    Public Property DTERM_DATE As Date
    Public Property NQUOTA As Integer
    Public Property NAMOUNT As Decimal
    Public Property NCURRENCY As Integer

#End Region

#Region "Page Process"

    Public Shared Function Retrieve(sCertype As String, nBranch As Integer, nProduct As Integer, nPolicy As Double, nCertif As Double, dEffecdate As Date) As List(Of FinancialInstrument)
        Dim result As New List(Of FinancialInstrument)
        Dim records As New eRemoteDB.Execute
        With records
            .StoredProcedure = "FINANCIAL_INSTRUMENTSPKG.RETRIEVE"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        If records.Run Then
            Do While Not records.EOF
                result.Add(New FinancialInstrument With {.DEFFECDATE = records.FieldToClass("DEFFECDATE"),
                                                         .NCONSECUTIVE = records.FieldToClass("NCONSECUTIVE"),
                                                         .NBANK_CODE = records.FieldToClass("NBANK_CODE"),
                                                         .NINSTRUMENT_TY = records.FieldToClass("NINSTRUMENT_TY"),
                                                         .NCARD_TYPE = records.FieldToClass("NCARD_TYPE"),
                                                         .SNUMBER = records.FieldToClass("SNUMBER"),
                                                         .DCARDEXPIR = records.FieldToClass("DCARDEXPIR"),
                                                         .DSTARTDATE = records.FieldToClass("DSTARTDATE"),
                                                         .DTERM_DATE = records.FieldToClass("DTERM_DATE"),
                                                         .NQUOTA = records.FieldToClass("NQUOTA"),
                                                         .NAMOUNT = records.FieldToClass("NAMOUNT"),
                                                         .NCURRENCY = records.FieldToClass("NCURRENCY")})
                records.RNext()
            Loop
            records.RCloseRec()
        End If
        Return result
    End Function

    Public Shared Function Delete(sCertype As String, nBranch As Integer, nProduct As Integer, nPolicy As Double, nCertif As Double, dEffecdate As Date, nConsecutive As Integer, dCurrentEffecdate As Date, nUsercode As Integer) As Boolean
        Dim result As Boolean
        Dim command As New eRemoteDB.Execute
        With command
            .StoredProcedure = "FINANCIAL_INSTRUMENTSPKG.DELETERECORD"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCurrent", dCurrentEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            result = .Run(False)
        End With
        Return result
    End Function

    Public Shared Function Validate(sCodispl As String,
                            sAction As String,
                            sCertype As String,
                            nBranch As Integer,
                            nProduct As Integer,
                            nPolicy As Double,
                            nCertif As Double,
                            dEffecdate As Date,
                            nConsecutive As Integer,
                            NBANK_CODE As Integer,
                            NINSTRUMENT_TY As Integer,
                            NCARD_TYPE As Integer,
                            SNUMBER As String,
                            DCARDEXPIR As Date,
                            DSTARTDATE As Date,
                            DTERM_DATE As Date,
                            NQUOTA As Integer,
                            NAMOUNT As Decimal,
                            NCURRENCY As Integer,
                            dCurrentEffecdate As Date) As String
        Dim lclsErrors As New eFunctions.Errors

        '+Banco : Debe estar lleno.
        If NBANK_CODE = 0 OrElse
           NBANK_CODE = eRemoteDB.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 10828)
        End If

        '+Tipo de instrumento: Debe estar lleno.
        If NINSTRUMENT_TY = 0 OrElse
           NINSTRUMENT_TY = eRemoteDB.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 9000040)
        End If

        '+Tipo de tarjeta: Si el “Tipo de instrumento” corresponde a “Tarjeta de crédito” debe estar lleno
        If NINSTRUMENT_TY = 6 AndAlso
           (NCARD_TYPE = 0 OrElse
            NCARD_TYPE = eRemoteDB.intNull) Then
            lclsErrors.ErrorMessage(sCodispl, 3864)
        End If

        '+Número: Si el “Tipo de instrumento” corresponde a “Tarjeta de crédito” debe estar lleno
        If NINSTRUMENT_TY = 6 AndAlso
           SNUMBER = eRemoteDB.strNull Then
            lclsErrors.ErrorMessage(sCodispl, 3865)
        End If

        '+Número: Si se está registrando un Tipo de instrumento, la relación “Banco / Tipo de instrumento / Número” no debe existir en la base de datos
        If sAction = "Add" AndAlso
           (NBANK_CODE <> 0 AndAlso
            NBANK_CODE <> eRemoteDB.intNull) AndAlso
           (NINSTRUMENT_TY <> 0 AndAlso
            NINSTRUMENT_TY <> eRemoteDB.intNull) AndAlso
           SNUMBER <> eRemoteDB.strNull AndAlso
           Exist_BankInstrumentNumber(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, NBANK_CODE, NINSTRUMENT_TY, SNUMBER) Then
            lclsErrors.ErrorMessage(sCodispl, 9000043)
        End If

        '+Fecha de vencimiento: Si el “Tipo de instrumento” corresponde a “Tarjeta de crédito” debe estar lleno
        If NINSTRUMENT_TY = 6 AndAlso
           DCARDEXPIR = eRemoteDB.dtmNull Then
            lclsErrors.ErrorMessage(sCodispl, 3876)
        End If

        '+Fecha de vencimiento: En el caso de tener valor ésta debe ser mayor a la fecha del día
        If DCARDEXPIR <> eRemoteDB.dtmNull AndAlso
            DCARDEXPIR < Today Then
            lclsErrors.ErrorMessage(sCodispl, 3937)
        End If


        '+Inicio de vigencia: Si el “Tipo de instrumento” corresponde a “Crédito hipotecario” o “Crédito de consumo” debe estar lleno
        If (NINSTRUMENT_TY = 5 OrElse
            NINSTRUMENT_TY = 4) AndAlso
           DSTARTDATE = eRemoteDB.dtmNull Then
            lclsErrors.ErrorMessage(sCodispl, 60300)
        End If

        '+Fin de vigencia: Si el “Tipo de instrumento” corresponde a “Crédito hipotecario” o “Crédito de consumo” debe estar lleno
        If (NINSTRUMENT_TY = 5 OrElse
            NINSTRUMENT_TY = 4) AndAlso
           DTERM_DATE = eRemoteDB.dtmNull Then

            'TODO:  No esta el error en el funcional

            lclsErrors.ErrorMessage(sCodispl, 60300)
        End If

        '+Fin de vigencia: Debe ser mayor a la fecha de Inicio de vigencia
        If DSTARTDATE <> eRemoteDB.dtmNull AndAlso
           DTERM_DATE <> eRemoteDB.dtmNull AndAlso
           DTERM_DATE < DSTARTDATE Then
            lclsErrors.ErrorMessage(sCodispl, 9000041)
        End If

        '+Cantidad de cuotas: Si el “Tipo de instrumento” corresponde a “Crédito hipotecario” o “Crédito de consumo” debe estar lleno
        If (NINSTRUMENT_TY = 5 OrElse
            NINSTRUMENT_TY = 4) AndAlso
           (NQUOTA = 0 OrElse
            NQUOTA = eRemoteDB.intNull) Then
            lclsErrors.ErrorMessage(sCodispl, 21011)
        End If

        '+Monto del crédito: Si el “Tipo de instrumento” corresponde a “Crédito hipotecario” o “Crédito de consumo” debe estar lleno
        If (NINSTRUMENT_TY = 5 OrElse
            NINSTRUMENT_TY = 4) AndAlso
           (NAMOUNT = 0 OrElse
            NAMOUNT = eRemoteDB.intNull) Then
            lclsErrors.ErrorMessage(sCodispl, 9000042)
        End If

        '+Moneda: Si el “Tipo de instrumento” corresponde a “Crédito hipotecario” o “Crédito de consumo” debe estar lleno
        If (NINSTRUMENT_TY = 5 OrElse
            NINSTRUMENT_TY = 4) AndAlso
           (NCURRENCY = 0 OrElse
            NCURRENCY = eRemoteDB.intNull) Then
            lclsErrors.ErrorMessage(sCodispl, 750024)
        End If

        Return lclsErrors.Confirm
    End Function

    Public Shared Function Post(sCodispl As String,
                                sAction As String,
                                sWindowType As String,
                                sCertype As String,
                                nBranch As Integer,
                                nProduct As Integer,
                                nPolicy As Double,
                                nCertif As Double,
                                dEffecdate As Date,
                                nConsecutive As Integer,
                                NBANK_CODE As Integer,
                                NINSTRUMENT_TY As Integer,
                                NCARD_TYPE As Integer,
                                SNUMBER As String,
                                DCARDEXPIR As Date,
                                DSTARTDATE As Date,
                                DTERM_DATE As Date,
                                NQUOTA As Integer,
                                NAMOUNT As Decimal,
                                NCURRENCY As Integer,
                                dCurrentEffecdate As Date,
                                nUsercode As Integer) As Boolean
        Dim result As Boolean = False

        If NCARD_TYPE = 0 Then
            NCARD_TYPE = eRemoteDB.intNull
        End If
        If NCURRENCY = 0 Then
            NCURRENCY = eRemoteDB.intNull
        End If


        If sWindowType = "PopUp" Then
            Select Case sAction
                Case "Add"
                    result = Create(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate,
                                    NBANK_CODE,
                                    NINSTRUMENT_TY,
                                    NCARD_TYPE,
                                    SNUMBER,
                                    DCARDEXPIR,
                                    DSTARTDATE,
                                    DTERM_DATE,
                                    NQUOTA,
                                    NAMOUNT,
                                    NCURRENCY,
                                    nUsercode)
                Case "Update"
                    result = Update(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate,
                                    NBANK_CODE,
                                    NINSTRUMENT_TY,
                                    NCARD_TYPE,
                                    SNUMBER,
                                    DCARDEXPIR,
                                    DSTARTDATE,
                                    DTERM_DATE,
                                    NQUOTA,
                                    NAMOUNT,
                                    NCURRENCY,
                                    nConsecutive, dCurrentEffecdate, nUsercode)
            End Select
            If result Then
                With New ePolicy.Policy_Win
                    .Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "2")
                End With
            End If
        End If

        Return result
    End Function

#End Region

#Region "Helpers"

    Private Shared Function Create(sCertype As String, nBranch As Integer, nProduct As Integer, nPolicy As Double, nCertif As Double, dEffecdate As Date,
                                    NBANK_CODE As Integer,
                                    NINSTRUMENT_TY As Integer,
                                    NCARD_TYPE As Integer,
                                    SNUMBER As String,
                                    DCARDEXPIR As Date,
                                    DSTARTDATE As Date,
                                    DTERM_DATE As Date,
                                    NQUOTA As Integer,
                                    NAMOUNT As Decimal,
                                    NCURRENCY As Integer,
                                  nUsercode As Integer) As Boolean
        Dim result As Boolean
        Dim command As New eRemoteDB.Execute
        With command
            .StoredProcedure = "FINANCIAL_INSTRUMENTSPKG.CREATERECORD"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBANK_CODE", NBANK_CODE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NINSTRUMENT_TY", NINSTRUMENT_TY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCARD_TYPE", NCARD_TYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SNUMBER", SNUMBER, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DCARDEXPIR", DCARDEXPIR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DSTARTDATE", DSTARTDATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DTERM_DATE", DTERM_DATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQUOTA", NQUOTA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMOUNT", NAMOUNT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCURRENCY", NCURRENCY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            result = .Run(False)
        End With
        Return result
    End Function

    Private Shared Function Update(sCertype As String, nBranch As Integer, nProduct As Integer, nPolicy As Double, nCertif As Double, dEffecdate As Date,
                            NBANK_CODE As Integer,
                            NINSTRUMENT_TY As Integer,
                            NCARD_TYPE As Integer,
                            SNUMBER As String,
                            DCARDEXPIR As Date,
                            DSTARTDATE As Date,
                            DTERM_DATE As Date,
                            NQUOTA As Integer,
                            NAMOUNT As Decimal,
                            NCURRENCY As Integer,
                           nConsecutive As Integer, dCurrentEffecdate As Date, nUsercode As Integer) As Boolean
        Dim result As Boolean
        Dim command As New eRemoteDB.Execute
        With command
            .StoredProcedure = "FINANCIAL_INSTRUMENTSPKG.UPDATERECORD"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBANK_CODE", NBANK_CODE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NINSTRUMENT_TY", NINSTRUMENT_TY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCARD_TYPE", NCARD_TYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SNUMBER", SNUMBER, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DCARDEXPIR", DCARDEXPIR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DSTARTDATE", DSTARTDATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DTERM_DATE", DTERM_DATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQUOTA", NQUOTA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMOUNT", NAMOUNT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCURRENCY", NCURRENCY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCurrent", dCurrentEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            result = .Run(False)
        End With
        Return result
    End Function

    Private Shared Function Exist_BankInstrumentNumber(sCertype As String, nBranch As Integer, nProduct As Integer, nPolicy As Double, nCertif As Double, dEffecdate As Date,
                                                        NBANK_CODE As Integer,
                                                        NINSTRUMENT_TY As Integer,
                                                        SNUMBER As String) As Boolean
        Dim result As Boolean
        Dim count As Integer = 0
        Dim command As New eRemoteDB.Execute
        With command
            .StoredProcedure = "FINANCIAL_INSTRUMENTSPKG.EXIST_BANKINSTRUMENTNUMBER"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBANK_CODE", NBANK_CODE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NINSTRUMENT_TY", NINSTRUMENT_TY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SNUMBER", SNUMBER, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCOUNT", count, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            result = .Run(False)
            If result Then
                count = .Parameters("NCOUNT").Value
            End If
        End With
        Return (count > 0)
    End Function

#End Region

End Class

