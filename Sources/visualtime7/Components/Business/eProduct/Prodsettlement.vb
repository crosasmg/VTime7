Option Strict Off
Option Explicit On
Public Class Prodsettlement

    '**%insValDP7002: Validates the page "DP7002" as described in the functional specifications
    '%InsValDP7002: Este metodo se encarga de realizar las validaciones descritas en el funcional
    '%de la ventana "DP7002"
    Public Function insValDP7002(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nOrder As Integer, ByVal sWin_type As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsValDP7002 As eRemoteDB.Execute
        lrecinsValDP7002 = New eRemoteDB.Execute
        Dim lstrError As String = String.Empty

        On Error GoTo insValDP7002_Err

        lclsErrors = New eFunctions.Errors

        If sWin_type = "PopUp" Then
            '+El número de orden debe estar lleno
            If (nOrder = eRemoteDB.Constants.intNull Or nOrder = 0) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1925)
                insValDP7002 = lclsErrors.Confirm
            End If
        End If

        '+
        '+ Definición de store procedure
        '+
        With lrecinsValDP7002
            .StoredProcedure = "insvalDP7002"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("sArrayerrors").Value

            If lstrError <> String.Empty Then
                lclsErrors = New eFunctions.Errors
                With lclsErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValDP7002 = .Confirm()
                End With
            End If
        End With

insValDP7002_Err:
        If Err.Number Then
            insValDP7002 = ""
            insValDP7002 = insValDP7002 & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        lrecinsValDP7002 = Nothing
        On Error GoTo 0
    End Function

    Public Function insPostDP7002(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nId_Settle As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsProdwin As eProduct.Prod_win = New eProduct.Prod_win
        Dim lrecinsPostDP7002 As eRemoteDB.Execute

        On Error GoTo insPostDP7002Err
        lrecinsPostDP7002 = New eRemoteDB.Execute
        insPostDP7002 = False

        '+ Definición de parámetros para stored procedure 'insudb.inspostDP7002'
        With lrecinsPostDP7002
            .StoredProcedure = "insPostDP7002"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostDP7002 = .Run(False)
        End With


insPostDP7002Err:
        If Err.Number Then
            insPostDP7002 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecAnnulment may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostDP7002 = Nothing
    End Function

    Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nId_Settle As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecdelProdsettlement As eRemoteDB.Execute

        On Error GoTo DeleteErr
        lrecdelProdsettlement = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.inspostDP7002'
        With lrecdelProdsettlement
            .StoredProcedure = "delProdsettlement"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

DeleteErr:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecAnnulment may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelProdsettlement = Nothing
    End Function

    Public Function insReaProdsettlement(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaProdsettlement As eRemoteDB.Execute

        On Error GoTo DeleteErr
        lrecreaProdsettlement = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.inspostDP7002'
        With lrecreaProdsettlement
            .StoredProcedure = "reaProdsettlement"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insReaProdsettlement = True
            Else
                insReaProdsettlement = False
            End If
        End With

DeleteErr:
        If Err.Number Then
            insReaProdsettlement = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecAnnulment may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProdsettlement = Nothing
    End Function

End Class
