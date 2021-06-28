Public Class Soap_Sell_Period
    Public nVehType As Integer
    Public dStartPeriod As Date
    Public dExpirePeriod As Date
    Public dStartDatepol As Date
    Public dExpireDatepol As Date
    Public sStatus As String
    Public sError As String
    Public dNullDate As Date
    Public nUserCode As Integer
    Public nYear As Integer
    '% Find: 
    Public Function Find(ByVal nVehType As Integer, ByVal dStartPeriod As Date, ByVal dExpiredPeriod As Date, ByVal dStartDatePol As Date, ByVal dExpiredDatePol As Date, ByVal nYear As Integer) As Boolean
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lreSoap_Sell_Period
        Dim lreSoap_Sell_Period As eRemoteDB.Execute

        lreSoap_Sell_Period = New eRemoteDB.Execute

        With lreSoap_Sell_Period
            .StoredProcedure = "FINDSOAP_SELL_PERIOD"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpiredPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpiredDatePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find = lblnRead
        lreSoap_Sell_Period = Nothing
    End Function
    '% Find: 
    Public Function Find_Date(ByVal nVehType As Integer, ByVal nChannel As Integer, ByVal dDateSell As Date, ByVal nYear As Integer) As Boolean
        ', ByVal dStartPeriod As Date, ByVal dExpiredPeriod As Date, ByVal dStartDatePol As Date, ByVal dExpiredDatePol As Date
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean
        '- Se define la variable lreSoap_Sell_Period
        Dim lreSoap_Sell_Date As eRemoteDB.Execute
        lreSoap_Sell_Date = New eRemoteDB.Execute

        With lreSoap_Sell_Date
            .StoredProcedure = "REASOAP_SELL_PERIOD"
            .Parameters.Add("NTYPEVEH", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCHANNEL", nChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DSTARTPERIOD", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DEXPIREPERIOD", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DSTARTDATEPOL", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DEXPIRDATEPOL", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SARRAYERRORS", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DSTARDATECER", dDateSell, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NYEAR", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.dStartPeriod = .Parameters("DSTARTPERIOD").Value
                Me.dExpirePeriod = .Parameters("DEXPIREPERIOD").Value
                Me.dStartDatepol = .Parameters("DSTARTDATEPOL").Value
                Me.dExpireDatepol = .Parameters("DEXPIRDATEPOL").Value
                Me.sError = .Parameters("SARRAYERRORS").Value
                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find_Date = lblnRead
        lreSoap_Sell_Date = Nothing
    End Function

    '% Find2: 
    Public Function Find2(ByVal nVehType As Integer) As Boolean
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lreSoap_Sell_Period
        Dim lreSoap_Sell_Period_Active As eRemoteDB.Execute

        lreSoap_Sell_Period_Active = New eRemoteDB.Execute

        With lreSoap_Sell_Period_Active
            .StoredProcedure = "FINDSOAP_SELL_PERIOD_ACTIVE"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find2 = lblnRead
        lreSoap_Sell_Period_Active = Nothing
    End Function
    '% Find3: 
    Public Function Find3(ByVal nVehType As Integer, ByVal dStartPeriod As Date, ByVal dExpiredPeriod As Date, ByVal dStartDatePol As Date, ByVal dExpiredDatePol As Date) As Boolean
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lreSoap_Sell_Period
        Dim lreSoap_Sell_Period_Null As eRemoteDB.Execute

        lreSoap_Sell_Period_Null = New eRemoteDB.Execute

        With lreSoap_Sell_Period_Null
            .StoredProcedure = "FINDSOAP_SELL_PERIOD_NULL"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpiredPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpiredDatePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find3 = lblnRead
        lreSoap_Sell_Period_Null = Nothing
    End Function
    '% Add: Agrega un registro a la tabla de Periodos de Venta por Tipo de vehículo (Soap_Sell_Period)
    Public Function Add() As Boolean
        Dim lreccreSoap_Sell_Period As eRemoteDB.Execute
        lreccreSoap_Sell_Period = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creFolios_agent'
        '+ Información leída el 06/07/2001 05:37:41 p.m.
        With lreccreSoap_Sell_Period
            .StoredProcedure = "CRESOAP_SELL_PERIOD"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpirePeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpireDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("dNullDate", dNullDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

    End Function

    '% Update : Actualiza un registro en la tabla Periodos de Venta por Tipo de vehículo (Soap_Sell_Period)
    Public Function Update() As Boolean
        Dim lrecupdSoap_Sell_Period As eRemoteDB.Execute

        On Error GoTo Update_err

        lrecupdSoap_Sell_Period = New eRemoteDB.Execute

        With lrecupdSoap_Sell_Period
            .StoredProcedure = "UPDSOAP_SELL_PERIOD"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpirePeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpireDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("dNullDate", dNullDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecupdSoap_Sell_Period = Nothing
    End Function

    '% Delete: Elimina un registro de la tabla Periodos de Venta por Tipo de vehículo (Soap_Sell_Period)
    Public Function Delete() As Boolean
        Dim lrecdelSoap_Sell_Period As eRemoteDB.Execute

        On Error GoTo Delete_err

        lrecdelSoap_Sell_Period = New eRemoteDB.Execute

        With lrecdelSoap_Sell_Period
            .StoredProcedure = "delSoap_Sell_Period"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpirePeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpireDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNullDate", dNullDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        lrecdelSoap_Sell_Period = Nothing
    End Function

    '% insValCA985: Valida los datos introducidos en la página
    '---------------------------------------------------------
    Public Function insValCA986_K(ByVal sCodispl As String, ByVal nZone As Integer, ByVal sWindowType As String, ByVal sAction As String, _
                                ByVal nVehType As Integer) As String
        '---------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCA986_K_Err

        lclsErrors = New eFunctions.Errors

        ' Validaciones del encabezado
        If nZone = 1 Then
            ' Incluya el tipo de vehículo
            If nVehType <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 13988)
            End If

        End If

        insValCA986_K = lclsErrors.Confirm

insValCA986_K_Err:
        If Err.Number Then
            insValCA986_K = insValCA986_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
    Public Function insValCA986(ByVal sCodispl As String, ByVal nZone As Integer, ByVal sWindowType As String, ByVal sAction As String, _
                                ByVal nVehType As Integer, ByVal dStartPeriod As Date, ByVal dExpiredPeriod As Date, ByVal dStartDatePol As Date, _
                                ByVal dExpiredDatePol As Date, ByVal nYear As Integer) As String
        '---------------------------------------------------------
        Dim days As Integer
        Dim lclsErrors As eFunctions.Errors
        On Error GoTo insValCA986_Err
        lclsErrors = New eFunctions.Errors

        ' Validaciones de la parte masiva
        If sWindowType = "PopUp" Then
            If nYear <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60338)
            End If

            'Verifica que el tipo de vehículo seleccionado no tenga un periodo activo     **********
            If sAction <> "Update" Then
                If Find2(nVehType) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90000056)
                End If
            End If

            'Verifica que el tipo de vehículo seleccionado no presente el estatus anulado     **********
            If sAction = "Update" Then
                If Find3(nVehType, dStartPeriod, dExpiredPeriod, dStartDatePol, dExpiredDatePol) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90000056)
                End If
            End If

            'Incluya la fecha de inicio del periodo de venta     **********
            If dStartPeriod = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 9068, , eFunctions.Errors.TextAlign.RigthAling, "Inicio del Periodo de Venta")
            End If

            'Incluya la fecha de fin del periodo de venta     **********
            If dExpiredPeriod = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 9068, , eFunctions.Errors.TextAlign.RigthAling, "Fin del Periodo de Venta")
            End If

            'Verifica que la fecha desde y hasta del periodo de venta no son iguales     **********
            If dStartPeriod = dExpiredPeriod Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000054)
            End If

            'Incluya la fecha de Inicio de vigencia de las pólizas     **********
            If dStartDatePol = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 9068, , eFunctions.Errors.TextAlign.RigthAling, "Inicio de Vigencia de las Polizas")
            End If

            'Incluya la fecha de Fin de Vigencia de las pólizas     **********
            If dExpiredDatePol = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 9068, , eFunctions.Errors.TextAlign.RigthAling, "Fin de Vigencia de las Polizas")
            End If

            'Verifica que la fecha desde y hasta del periodo de vigencia no son iguales     **********
            If dStartDatePol = dExpiredDatePol Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000055)
            End If

            'Verifica que no exista un registro con los mismos rangos de fecha     **********
            If sAction <> "Update" Then
                If Find(nVehType, dStartPeriod, dExpiredPeriod, dStartDatePol, dExpiredDatePol, nYear) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10185)
                End If
            End If
            'Verifica que la fecha desde del periodo de venta sea menor a la fecha hasta del período de venta     **********
            If dStartPeriod >= dExpiredPeriod Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000048)
            End If

            'Verifica que la fecha hasta del periodo de vigencia sea mayor a la fecha desde del período de vigencia     **********
            If dStartDatePol >= dExpiredDatePol Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000073)
            End If

            'Verifica que el período comprendido entre la fecha desde y hasta del periodo de venta sea igual o menor a un año     **********
            days = (DateDiff(DateInterval.Day, dStartPeriod, dExpiredPeriod))
            If days > 365 Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000050)
            End If

            'Verifica que el período comprendido entre la fecha desde y hasta del periodo de vigencia sea igual o menor a un año     **********
            days = (DateDiff(DateInterval.Day, dStartDatePol, dExpiredDatePol))
            If days > 365 Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000051)
            End If

            'Verifica que la fecha hasta del periodo de venta sea <= a la fecha hasta del período de vigencia     **********
            If dExpiredPeriod > dExpiredDatePol Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000052)
            End If

            'Verifica que la fecha inicio del periodo de vigencia está en el período de venta     **********
            If ((dStartDatePol < dStartPeriod) Or (dStartDatePol > dExpiredPeriod)) Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000053)
            End If

        End If

        insValCA986 = lclsErrors.Confirm

insValCA986_Err:
        If Err.Number Then
            insValCA986 = insValCA986 & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
    '% insPostCA985: Actualiza los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostCA986Upd(ByVal sAction As String, ByVal nVehType As Integer, ByVal dStartPeriod As Date, ByVal dExpirePeriod As Date, ByVal dStartDatepol As Date, _
                                    ByVal dExpireDatepol As Date, ByVal sStatus As String, ByVal nUserCode As Integer, ByVal nYear As Integer) As Boolean
        Dim AuxStatus As String

        If sStatus = "1" Then
            AuxStatus = "2"
        Else
            AuxStatus = "1"
        End If

        With Me
            .nVehType = nVehType
            .dStartPeriod = dStartPeriod
            .dExpirePeriod = dExpirePeriod
            .dStartDatepol = dStartDatepol
            .dExpireDatepol = dExpireDatepol
            .sStatus = AuxStatus
            .nUserCode = nUserCode
            .nYear = nYear

            Select Case sAction.Trim
                Case "Add"
                    insPostCA986Upd = Add()
                Case "Del"
                    insPostCA986Upd = Delete()
                Case "Update"
                    insPostCA986Upd = Update()
            End Select
        End With

    End Function

    '% insPostCA985: Actualiza los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostCA986(ByVal nVehType As Integer, ByVal dStartPeriod As Date, ByVal dExpirePeriod As Date, ByVal dStartDatepol As Date, _
                                 ByVal dExpireDatepol As Date, ByVal sStatus As String, ByVal dStadNullDate As Date, ByVal nUserCode As Integer) As Boolean

        Dim lrecinsPostCA986 As eRemoteDB.Execute

        On Error GoTo insPostCA986_err

        lrecinsPostCA986 = New eRemoteDB.Execute

        Dim AuxStatus As String

        If sStatus = "1" Then
            AuxStatus = "2"
        Else
            AuxStatus = "1"
        End If

        With lrecinsPostCA986
            .StoredProcedure = "insPostCA985pkg.insPostCA986"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartPeriod", dStartPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirePeriod", dExpirePeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDatepol", dStartDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpireDatepol", dExpireDatepol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStadNullDate", dStadNullDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA986 = .Run(False)
        End With

insPostCA986_err:
        If Err.Number Then
            insPostCA986 = False
        End If
        On Error GoTo 0
        lrecinsPostCA986 = Nothing

    End Function

End Class