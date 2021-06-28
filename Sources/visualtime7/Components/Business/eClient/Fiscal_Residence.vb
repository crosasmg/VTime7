Option Strict Off
Option Explicit On
Public Class Fiscal_Residence

    '-Campos de la tabla 

    Public sClient As String
    Public nCountry As Integer
    Public dEffecdate As Date
    Public sUs_Itinnum As String
    Public nMotive_Itin As Integer
    Public sJurisdiction As String
    Public dNulldate As Date
    Public nUsercode As Integer
    Public dCompdate As Date


    '% insValBC007P: Realiza las validaciones de la transaccion
    Public Function insValBC007P(ByVal sClient As String, ByVal nCountry As Integer, ByVal sUs_Itinnum As String, ByVal nMotive_itin As Integer, ByVal sJurisdiction As String) As String
        'Public Function insValBC007P(ByVal sClient As String) As String


        Dim lerrTime As eFunctions.Errors
        On Error GoTo insValBC007P_Err
        lerrTime = New eFunctions.Errors

        '+Valida que el campo nCountry no sea nulo
        If nCountry = eRemoteDB.Constants.intNull Or nCountry = 0 Then
            Call lerrTime.ErrorMessage("BC007P", 6004)
        End If

        '+Valida que si no se indica TIN se debe indicar el motivo
        sUs_Itinnum.Trim()
        If (sUs_Itinnum = "" Or sUs_Itinnum = eRemoteDB.Constants.strNull) And (nMotive_itin = eRemoteDB.Constants.intNull Or nMotive_itin = 0) Then
            Call lerrTime.ErrorMessage("BC007P", 90000519)
        End If

        '+Valida que se ingrese jurridicion 
        sJurisdiction.Trim()
        If (sJurisdiction = "" Or sJurisdiction = eRemoteDB.Constants.strNull) Then
            Call lerrTime.ErrorMessage("BC007P", 760002)
        End If

        '+ valida que no se ingrese motivo, si es que se esta ingresando TIN
        If (sUs_Itinnum <> eRemoteDB.Constants.strNull And nMotive_itin <> 0) Then
            Call lerrTime.ErrorMessage("BC007P", 90000520)
        End If

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
    '%                FISCAL_RESIDENCE
    Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date) As Object

        Dim lrecreaFiscal_Residence As eRemoteDB.Execute
        lrecreaFiscal_Residence = New eRemoteDB.Execute


        On Error GoTo Find_err

        With lrecreaFiscal_Residence
            .StoredProcedure = "Reafiscal_Residence"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find = .Run
            If Find Then
                'Find = True
                Me.sClient = sClient
                'sClient = .FieldToClass("sClient")
                nCountry = .FieldToClass("nPosition")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                'dEffecdate = .FieldToClass("dEffecdate")
                sUs_Itinnum = .FieldToClass("sus_itinnum", eRemoteDB.Constants.intNull)
                nMotive_Itin = .FieldToClass("nMotive_Itin", eRemoteDB.Constants.intNull)
                sJurisdiction = .FieldToClass("sJurisdiction", eRemoteDB.Constants.intNull)
                dNulldate = .FieldToClass("dNulldate")
                nUsercode = .FieldToClass("nUsercode", eRemoteDB.Constants.intNull)
                dCompdate = .FieldToClass("dCompdate")
            End If
        End With

Find_err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaClient_PEP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFiscal_Residence = Nothing
        On Error GoTo 0
    End Function

    '% UpdClient_PEP: Esta funcion se encarga de realizar las actualizaciones de la tabla
    '%                      Client_SF , correspodiente a las caracteristicas especiales para el caso PEP.
    Function UpdFiscal_Residence(ByVal nAction As Integer) As Boolean
        Dim lobjTime As eRemoteDB.Execute

        On Error GoTo UpdUpdFiscal_Residence_Err
        lobjTime = New eRemoteDB.Execute

        With lobjTime
            .StoredProcedure = "insUpdFiscal_Residence"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUs_Itinnum", sUs_Itinnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMotive_Itin", nMotive_Itin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sJurisdiction", sJurisdiction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdFiscal_Residence = .Run(False)
        End With


UpdUpdFiscal_Residence_Err:
        If Err.Number Then
            UpdFiscal_Residence = False
        End If
        'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjTime = Nothing
        On Error GoTo 0
    End Function
    '% InsPostBC007P: Realiza las actualizaciones de la transaccion
    Public Function InsPostBC007P(ByVal nAction As Integer, ByVal sClient As String, ByVal nCountry As Integer, ByVal dEffecdate As Date, ByVal sUs_Itinnum As String, ByVal nMotive_itin As Integer, ByVal sJurisdiction As String, ByVal nUsercode As Integer) As Boolean

        'Dim lclsClientWin As eClient.ClientWin
        'Dim lclsClient As eClient.Client
        Dim lclsFiscal_Residence As eClient.Fiscal_Residence
        'lclsClient = New Client
        lclsFiscal_Residence = New Fiscal_Residence

        Me.sClient = sClient
        Me.nCountry = nCountry
        Me.dEffecdate = dEffecdate
        Me.nUsercode = nUsercode
        Me.sUs_Itinnum = sUs_Itinnum
        Me.nMotive_Itin = nMotive_itin
        Me.sJurisdiction = sJurisdiction
        Me.nUsercode = nUsercode

        InsPostBC007P = Me.UpdFiscal_Residence(nAction)


    End Function

    '% DelFiscal_Residence: Actualiza el usuario que está modificando la póliza
    Public Function DelFiscal_Residence(ByVal sClient As String, ByVal nCountry As Integer, ByRef dEffecdate As Date, nUsercode As Integer) As Boolean
        Dim lrecDelFiscal_Residence As eRemoteDB.Execute

        lrecDelFiscal_Residence = New eRemoteDB.Execute

        On Error GoTo Update_UserAmend_Err

        '+ Definición de parámetros para stored procedure 'insudb.DelFiscal_Residence'
        '+ Información leída el 06/11/2000 02:37:39 p.m.

        With lrecDelFiscal_Residence
            .StoredProcedure = "DelFiscal_Residence"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            DelFiscal_Residence = .Run(False)
        End With

        DelFiscal_Residence = Nothing

Update_UserAmend_Err:
        If Err.Number Then
            DelFiscal_Residence = False
        End If
        On Error GoTo 0
    End Function

End Class

