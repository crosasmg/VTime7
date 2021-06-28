Option Strict Off
Option Explicit On
Public Class Client_SF

    '-Campos de la tabla

    Public sClient As String
    Public sDigit As String
    Public nPosition As Integer
    Public dStartcondition As Date
    Public dEndcondition As Date
    Public dEffecdate As Date
    Public dNulldate As Date
    Public nUsercode As Integer
    Public dCompdate As Date
    Public sPEP As String
    Public nPlacebirth As Integer
    Public nSecond_nationality As Integer
    Public nResident_former As Integer
    Public sUsAdress As String
    Public sUsLegal_person As String
    Public sUsphone As String
    Public sUsAccount As String
    Public sSSN As String
    Public sUsItinnum As String
    Public sUsIrsind As String
    Public sPlacebirth As String


    '% insValBC007P: Realiza las validaciones de la transaccion
    Public Function insValBC007P(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal sClient As String = "", Optional ByVal nCodRating As Integer = 0, Optional ByVal nCurrency As Short = 0, Optional ByVal nLimitCredit As Double = 0) As String


        Dim lerrTime As eFunctions.Errors
        On Error GoTo insValBC007P_Err
        lerrTime = New eFunctions.Errors
        With lerrTime

            '+Validaciones del campo Clasificación Rating

            If nCodRating <= 0 Then
                .ErrorMessage(sCodispl, 9000014)
            End If
            '+Si se indico limite de credito, es necesario indicar la moneda asociada
            If nLimitCredit > 0 Then
                If nCurrency <= 0 Then
                    .ErrorMessage(sCodispl, 750024)
                End If
            Else
                If nCurrency > 0 Then
                    .ErrorMessage(sCodispl, 11417)
                End If
            End If

        End With

        insValBC007P = lerrTime.Confirm

insValBC007P_Err:
        If Err.Number Then
            insValBC007P = insValBC007P & Err.Description
        End If
        'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lerrTime = Nothing
        On Error GoTo 0



    End Function

    '% Find: Esta función es la encarga de buscar si existe informacion en las tablas.
    '%                CLIENT_SF
    Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date) As Object

        Dim lrecreaClient_PEP As eRemoteDB.Execute
        Dim lclsreaClient_PEP As Client

        On Error GoTo Find_err
        lrecreaClient_PEP = New eRemoteDB.Execute

        With lrecreaClient_PEP
            .StoredProcedure = "reaClient_SF"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Find = True
                Me.sClient = sClient
                'sClient = .FieldToClass("sClient")
                sDigit = .FieldToClass("sDigit", eRemoteDB.Constants.intNull)
                nPosition = .FieldToClass("nPosition", eRemoteDB.Constants.intNull)
                dStartcondition = .FieldToClass("dStartcondition")
                dEndcondition = .FieldToClass("dEndcondition")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                'dEffecdate = .FieldToClass("dEffecdate")
                dNulldate = .FieldToClass("dNulldate")
                nUsercode = .FieldToClass("nUsercode", eRemoteDB.Constants.intNull)
                dCompdate = .FieldToClass("dCompdate")
                nPlacebirth = .FieldToClass("nPlacebirth")
                nSecond_nationality = .FieldToClass("nSecond_nationality")
                nResident_former = .FieldToClass("nResident_former")
                sUsAdress = .FieldToClass("susaddres")
                sUsLegal_person = .FieldToClass("sUsLegal_person")
                sUsphone = .FieldToClass("sUsphone")
                sUsAccount = .FieldToClass("susaccount")
                sSSN = .FieldToClass("sSsn")
                sUsItinnum = .FieldToClass("sus_itinnum")
                sUsIrsind = .FieldToClass("sus_irsind")
                sPlacebirth = .FieldToClass("sPlacebirth")


            Else
                Find = False
            End If
        End With

Find_err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaClient_PEP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaClient_PEP = Nothing
        On Error GoTo 0
    End Function

    '% UpdClient_PEP: Esta funcion se encarga de realizar las actualizaciones de la tabla
    '%                      Client_SF , correspodiente a las caracteristicas especiales para el caso PEP.
    Function UpdClient_SF(ByVal nAction As Integer) As Boolean
        Dim lobjTime As eRemoteDB.Execute

        On Error GoTo UpdClient_PEP_Err
        lobjTime = New eRemoteDB.Execute

        With lobjTime
            .StoredProcedure = "insUpdClient_SF"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If nAction = 2 Then
                .Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dStartcondition", dStartcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEndcondition", dEndcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dNulldate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ElseIf nAction = 3 Then
                .Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dStartcondition", dStartcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEndcondition", dEndcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dNulldate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dStartcondition", dStartcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEndcondition", dEndcondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCompdate", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nPlacebirth", nPlacebirth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSecond_nationality", nSecond_nationality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResident_former", nResident_former, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If nAction = 3 Then
                .Parameters.Add("sUsAdress", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsLegal_person", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsphone", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsAccount", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sSSN", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsItinnum", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsIrsind", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("sUsAdress", sUsAdress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsLegal_person", sUsLegal_person, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsphone", sUsphone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsAccount", sUsAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sSSN", sSSN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsItinnum", sUsItinnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sUsIrsind", sUsIrsind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            .Parameters.Add("sPlacebirth", sPlacebirth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdClient_SF = .Run(False)
        End With


UpdClient_PEP_Err:
        If Err.Number Then
            UpdClient_SF = False
        End If
        'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjTime = Nothing
        On Error GoTo 0
    End Function
    '% InsPostBC007P: Realiza las actualizaciones de la transaccion
    Public Function InsPostBC007P(ByVal nAction As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nTypeOfPoliticalOffice As Integer, ByVal dGrantDate As Date, ByVal dEndDate As Date, ByVal sDigit As String, ByVal nUsercode As Integer, ByVal nPlacebirth As Integer, ByVal nSecond_nationality As Integer, ByVal nResident_former As Integer, Optional ByVal sUsaddres As String = "", Optional ByVal sSSN As String = "", Optional ByVal sUsLegal_person As String = "", Optional ByVal sUsItinnum As String = "", Optional ByVal sUsphone As String = "", Optional ByVal sUsIrsind As String = "", Optional ByVal sUsAccount As String = "", Optional ByVal txtPlaceOfBirth As String = "") As Boolean

        Dim lclsClientWin As eClient.ClientWin
        Dim lclsClient As eClient.Client
        lclsClient = New Client

        Me.sClient = sClient
        Me.sDigit = sDigit
        Me.nPosition = nTypeOfPoliticalOffice
        Me.dStartcondition = dGrantDate
        Me.dEndcondition = dEndDate
        Me.dEffecdate = dEffecdate
        Me.dNulldate = dNulldate
        Me.nUsercode = nUsercode
        Me.dCompdate = dCompdate
        Me.nPlacebirth = nPlacebirth
        Me.nSecond_nationality = nSecond_nationality
        Me.nResident_former = nResident_former
        Me.sUsAdress = sUsaddres
        Me.sUsLegal_person = sUsLegal_person
        Me.sUsphone = sUsphone
        Me.sUsAccount = sUsAccount
        Me.sSSN = sSSN
        Me.sUsItinnum = sUsItinnum
        Me.sUsIrsind = sUsIrsind
        Me.sPlacebirth = txtPlaceOfBirth

        Dim total_dias As Integer

        Call lclsClient.Find(sClient)
        ' Si es PEP se realizan las siguientes validaciones.
        ' Se valida que el campo cargo sea distinto de vacio o ninguno.
        ' Se valida que tenga fecha de inicio
        ' Si tienen fecha de fin se valida lo siguiente:

        ' Si la fecha de fin es mayor que la fecha del registro, cliente es PEP
        ' Si la fecha de fin es menor que la fecha del dia se valida lo siguiente
        ' Tiempo transcurrido entre fecha Inicio menos fecha fin mayor o igual  a 1 año NO es PEP y se debe actualizar SPEP de la bc001N 0 J
        ' Si es menor a 1 año es PEP sigue igual.
        If lclsClient.sPEP = "1" Then
            If nTypeOfPoliticalOffice <> 98 And dGrantDate <> eRemoteDB.Constants.dtmNull And dGrantDate <> eRemoteDB.Constants.dtmNull Then

                If dEndDate <> eRemoteDB.Constants.dtmNull Then

                    If dEndDate < Date.Now Then
                        total_dias = DateDiff(DateInterval.Day, dEndDate, Date.Now)
                        If total_dias > 365 Then
                            Update_ClientPEP()
                            'si deja de ser PEP se actualiza con fecha de anulacion                          
                            UpdClient_SF(2)
                        End If

                    End If
                End If
            End If
        Else
            If nTypeOfPoliticalOffice = eRemoteDB.Constants.intNull Then
                Me.nPosition = 98
            End If
        End If


        InsPostBC007P = Me.UpdClient_SF(nAction)

        If InsPostBC007P Then
            lclsClientWin = New eClient.ClientWin
            Call lclsClientWin.insUpdClient_win(sClient, "BC007P", "2", , , nUsercode)
            'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsClientWin = Nothing
        End If

    End Function

    '% Update_UserAmend: Actualiza el usuario que está modificando la póliza
    Public Function Update_ClientPEP() As Boolean
        Dim lrecUpdate_ClientPEP As eRemoteDB.Execute

        lrecUpdate_ClientPEP = New eRemoteDB.Execute

        On Error GoTo Update_UserAmend_Err

        '+ Definición de parámetros para stored procedure 'insudb.Update_ClientPEP'
        '+ Información leída el 06/11/2000 02:37:39 p.m.

        With lrecUpdate_ClientPEP
            .StoredProcedure = "UPDCLIENTPEPBC007P"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPEP", sPEP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_ClientPEP = .Run(False)
        End With

        lrecUpdate_ClientPEP = Nothing

Update_UserAmend_Err:
        If Err.Number Then
            Update_ClientPEP = False
        End If
        On Error GoTo 0
    End Function

End Class








