Option Strict Off
Option Explicit On
Public Class Surr_retention
	'%-------------------------------------------------------%'
	'% $Workfile:: Surr_retention.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name                    Type           Nullable
	'+ ------------------------------ -------------- --------
	Public nBranch As Integer 'NUMBER (5)    NOT NULL
	Public nProduct As Integer 'NUMBER (5)    NOT NULL
	Public dEffecdate As Date 'DATE          NOT NULL
	Public dNulldate As Date 'DATE          NULL
	Public nSurr_reason As Integer 'NUMBER (5)    NOT NULL
	Public nSurr_ret As Double 'NUMBER (4,2)  NOT NULL
	Public dCompdate As Date 'DATE          NOT NULL
    Public nUsercode As Integer 'NUMBER (5)    NOT NULL
    Public nTyp_profitworker As Integer
    Public nAmountfree As Integer
    Public nCurrency As Integer
    Public nOrigin As Integer

    '%insValDP7000: Se encarga de validar la ventana de porcentanjes de retención de rescates
    Public Function insValDP7000(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                 ByVal dEffecdate As Date, ByVal nSurr_reason As Integer, ByVal nSurr_ret As Double, ByVal nTyp_profitworker As Integer, _
                                 ByVal nAmountfree As Integer, ByVal nCurrency As String, ByVal nOrigin As Integer) As String

        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP7000_err

        lclsErrors = New eFunctions.Errors

        insValDP7000 = CStr(True)

        If nSurr_reason <= 0 Then
            Call lclsErrors.ErrorMessage("DP7000", 70054)
        Else
            If sAction = "Add" Then
                If ValDupSurr_retention(nBranch, nProduct, dEffecdate, nSurr_reason, nTyp_profitworker, nAmountfree, nCurrency, nOrigin) Then
                    Call lclsErrors.ErrorMessage("DP7000", 70126)
                End If
            End If
        End If

        If nSurr_ret < 0 Then
            Call lclsErrors.ErrorMessage("DP7000", 70097)
        End If

        insValDP7000 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP7000_err:
        If Err.Number Then
            insValDP7000 = insValDP7000 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '%insPostDP7000: Esta función se encarga de actualizar los datos de la tabla Surr_retention
    Public Function insPostDP7000(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSurr_reason As Integer, ByVal nSurr_ret As Double, ByVal nTyp_profitworker As Integer, ByVal nAmountfree As Integer, ByVal nCurrency As String, ByVal nOrigin As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsSurr_retention As Surr_retention

        lclsSurr_retention = New Surr_retention

        On Error GoTo insPostDP7000_err

        insPostDP7000 = True

        If sAction <> String.Empty Then
            If sAction = "Add" Or sAction = "Update" Then
                insPostDP7000 = lclsSurr_retention.Update(nBranch, nProduct, dEffecdate, nSurr_reason, nSurr_ret, nTyp_profitworker, nAmountfree, nCurrency, nOrigin, nUsercode)
            Else
                insPostDP7000 = lclsSurr_retention.Delete(nBranch, nProduct, dEffecdate, nSurr_reason, nSurr_ret, nUsercode, nTyp_profitworker, nAmountfree, nCurrency, nOrigin)
            End If
        End If

        lclsSurr_retention = Nothing

insPostDP7000_err:
        If Err.Number Then
            insPostDP7000 = False
        End If

        On Error GoTo 0
    End Function

    '% Delete: Elimina registros de la tabla Surr_retention
    Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSurr_reason As Integer, ByVal nSurr_ret As Double, ByVal nUsercode As Integer, ByVal nTyp_profitworker As Integer, ByVal nAmountfree As Integer, ByVal nCurrency As Integer, ByVal nOrigin As Integer) As Boolean
        Dim lrecDelete As eRemoteDB.Execute

        On Error GoTo Delete_Err

        lrecDelete = New eRemoteDB.Execute

        Delete = False

        With lrecDelete
            .StoredProcedure = "delSurr_retention"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_ret", nSurr_ret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountfree", nAmountfree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_profitworker", nTyp_profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Delete = True
            End If
        End With

Delete_Err:
        If Err.Number Then
            Delete = False
        End If

        'UPGRADE_NOTE: Object lrecDelete may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDelete = Nothing

        On Error GoTo 0
    End Function

    '% Update: Actualiza los registros de la tabla Surr_retention
    Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSurr_reason As Integer, _
                           ByVal nSurr_ret As Double, ByVal nTyp_profitworker As Integer, ByVal nAmountfree As Integer, ByVal nCurrency As Integer, _
                           ByVal nOrigin As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecUpdate As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecUpdate = New eRemoteDB.Execute

        Update = False

        With lrecUpdate
            .StoredProcedure = "updSurr_retention"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_ret", nSurr_ret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountfree", nAmountfree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", IIf(nCurrency = 0, eRemoteDB.Constants.intNull, nCurrency), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_profitworker", nTyp_profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Update = True
            End If
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If

        'UPGRADE_NOTE: Object lrecUpdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdate = Nothing

        On Error GoTo 0
    End Function

    '% ValSurr_retention: Valida que no existan datos duplicados en Surr_retention
    Public Function ValDupSurr_retention(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSurr_reason As Integer, ByVal nTyp_profitworker As Integer, ByVal nAmountfree As Integer, ByVal nCurrency As String, ByVal nOrigin As Integer) As Boolean
        Dim lrecDup As eRemoteDB.Execute

        On Error GoTo ValDupSurr_retention_Err

        lrecDup = New eRemoteDB.Execute
        With lrecDup
            .SQL = " SELECT * FROM SURR_RETENTION " & " WHERE NBRANCH  = " & nBranch & "  AND NPRODUCT = " & nProduct & "  AND NSURR_REASON = " & nSurr_reason & " AND nTyp_profitworker = " & nTyp_profitworker & " AND nAmountfree = " & nAmountfree & " AND nCurrency = " & nCurrency & " AND nOrigin = " & nOrigin

            If .Run Then
                ValDupSurr_retention = True
                .RCloseRec()
            End If
        End With

ValDupSurr_retention_Err:
        If Err.Number Then
            ValDupSurr_retention = False
        End If
        'UPGRADE_NOTE: Object lrecDup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDup = Nothing
        On Error GoTo 0
    End Function
End Class






