Option Strict Off
Option Explicit On
Public Class Contrnp_Risks

    Public nNumber As Integer
    Public nBranch As Integer
    Public dEffecdate As Date
    Public nType As Integer
    Public sClient As String
    Public nSumInsured As Double
    Public dNulldate As Date
    Public dCompdate As Date
    Public nUsercode As Integer
    Public sSpcApply As String

    '%Add: Crea un registro en la tabla
    Public Function Add() As Boolean
        Add = InsUpdContrnp_Risks(1)
    End Function

    '%Update: Actualiza los datos de la tabla
    Public Function Update() As Boolean
        Update = InsUpdContrnp_Risks(2)
    End Function

    '%Delete: Borra los datos de la tabla
    Public Function Delete() As Boolean
        Delete = InsUpdContrnp_Risks(3)
    End Function

    Private Function IsExist(ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nType As Integer, ByVal sClient As String) As Boolean
        Dim lclsContrnp_Risks As eRemoteDB.Execute
        Dim lintExist As Short

        lclsContrnp_Risks = New eRemoteDB.Execute
        lintExist = 0

        With lclsContrnp_Risks
            .StoredProcedure = "reaContrnp_Risks_v"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsContrnp_Risks = Nothing

        Exit Function
    End Function

    '+ Update : Actualiza un registro de la tabla
    Function InsUpdContrnp_Risks(ByVal nAction As Integer) As Integer
        Dim lrecinsUpdContrnp_Risks As eRemoteDB.Execute
        On Error GoTo insUpdContrnp_Risks_Err

        lrecinsUpdContrnp_Risks = New eRemoteDB.Execute

        With lrecinsUpdContrnp_Risks
            .StoredProcedure = "insUpdContrnp_Risks"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSumInsured", nSumInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSpcApply", sSpcApply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsUpdContrnp_Risks = .Run(False)
        End With

insUpdContrnp_Risks_Err:
        If Err.Number Then
            InsUpdContrnp_Risks = False
        End If
        lrecinsUpdContrnp_Risks = Nothing
        On Error GoTo 0
    End Function
    '+InsposCR309 : Función que realiza los cambios en la base de datos especificados en CR309
    Function insPostCR309(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nType As Integer, ByVal sClient As String, ByVal nSumInsured As Double, ByVal nUsercode As Integer, ByVal sSpcApply As String) As Object
        Dim lintAction As Integer

        On Error GoTo InspostCR309_Err

        With Me
            .nNumber = nNumber
            .nBranch = nBranch
            .dEffecdate = dEffecdate
            .nType = nType
            .sClient = sClient
            .nSumInsured = nSumInsured
            .nUsercode = nUsercode
            .sSpcApply = sSpcApply

            If sAction = "Del" Then
                lintAction = 3
            Else
                If sAction = "Update" Then
                    lintAction = 2
                Else
                    If sAction = "Add" Then
                        lintAction = 1
                    End If
                End If
            End If

            Select Case lintAction
                Case 1
                    '+ Se crea el registro
                    insPostCR309 = .Add

                    '+ Se modifica el registro
                Case 2
                    insPostCR309 = .Update

                    '+ Se elimina el registro
                Case 3
                    insPostCR309 = .Delete

            End Select
        End With

InspostCR309_Err:
        If Err.Number Then
            insPostCR309 = False
        End If
        On Error GoTo 0
    End Function

    '+ Esta función realiza las validaciones de la forma CR309
    Function insvalCR309(ByVal sAction As String, ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nType As Integer, ByVal sClient As String, ByVal nSumInsured As Double) As String
        Dim lclsErrors As eFunctions.Errors

        Dim lcolContrnp_Riskss As Contrnp_Riskss
        Dim lclsContrnp_Risks As Contrnp_Risks
        Dim lintCount As Object

        lclsErrors = New eFunctions.Errors
        lcolContrnp_Riskss = New Contrnp_Riskss
        lclsContrnp_Risks = New Contrnp_Risks

        On Error GoTo insValCR309_Err

        lintCount = 0

        '+Validación del campo asegurado
        If sClient = eRemoteDB.Constants.strNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 2792)
        Else
            '+Validar que no se dupliquen registros
            If sAction = "Add" Then
                If IsExist(nNumber, nBranch, dEffecdate, nType, sClient) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 20029)
                End If
            End If
        End If
        '+Validación del campo suma asegurada
        If nSumInsured <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60169)
        End If


        insvalCR309 = lclsErrors.Confirm
        lclsErrors = Nothing

insValCR309_Err:
        If Err.Number Then
            insvalCR309 = insvalCR309 & Err.Description
        End If
        On Error GoTo 0
    End Function
End Class






