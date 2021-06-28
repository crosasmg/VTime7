Option Strict Off
Option Explicit On
Public Class Depend_Sheet
	
	'-Propiedades según la tabla en el sistema el 08/07/2002
	
	'Column_name               Type
	'------------------------  -----------
	Public nSheet_Father As String
	Public nSheet_Child As Integer
	Public sSheet_Child As String
	
	'% InsValMGI1410: Realiza la validación puntual de los campos a actualizar en la ventana MGI1410
	Public Function InsValMGI1410Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nSheet_Father As String, ByVal nSheet_Child As Integer) As String
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecMGI1410 As eRemoteDB.Execute

        On Error GoTo InsValMGI1410Upd_Err

        lrecMGI1410 = New eRemoteDB.Execute
        With lrecMGI1410
            .StoredProcedure = "INSMGI1410PKG.INSVALMGI1410UPD"
            .Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet_Father", nSheet_Father, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet_Child", nSheet_Child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        'UPGRADE_NOTE: Object lrecMGI1410 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecMGI1410 = Nothing

        If Len(lstrErrorAll) > 0 Then
            lclsErrors = New eFunctions.Errors
            With lclsErrors
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
                InsValMGI1410Upd = .Confirm
            End With
        End If

InsValMGI1410Upd_Err:
        If Err.Number Then
            InsValMGI1410Upd = "InsValMGI1410Upd: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecMGI1410 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecMGI1410 = Nothing
    End Function

    '% InsValMGI1410: Realiza la validación puntual de los campos a actualizar en la ventana MGI1410
    Public Function InsValMGI1410(ByVal sCodispl As String, ByVal nSheet_Father As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecMGI1410 As eRemoteDB.Execute

        On Error GoTo InsValMGI1410_Err

        lrecMGI1410 = New eRemoteDB.Execute
        With lrecMGI1410
            .StoredProcedure = "INSMGI1410PKG.INSVALMGI1410"
            .Parameters.Add("nSheet_Father", nSheet_Father, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        'UPGRADE_NOTE: Object lrecMGI1410 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecMGI1410 = Nothing

        If Len(lstrErrorAll) > 0 Then
            lclsErrors = New eFunctions.Errors
            With lclsErrors
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
                InsValMGI1410 = .Confirm
            End With
        End If

InsValMGI1410_Err:
        If Err.Number Then
            InsValMGI1410 = "InsValMGI1410: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecMGI1410 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecMGI1410 = Nothing
    End Function
	
	'%InsPostMGI1410Upd: Se realiza la actualización de los datos en la ventana MGI1410
	Public Function InsPostMGI1410Upd(ByVal sAction As String, ByVal nSheet_Father As String, ByVal nSheet_Child As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecMGI1410 As eRemoteDB.Execute
		
		On Error GoTo InsPostMGI1410Upd_Err
		
		lrecMGI1410 = New eRemoteDB.Execute
		With lrecMGI1410
			.StoredProcedure = "INSMGI1410PKG.INSPOSTMGI1410UPD"
			.Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet_Father", nSheet_Father, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet_Child", nSheet_Child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostMGI1410Upd = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecMGI1410 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMGI1410 = Nothing
		
InsPostMGI1410Upd_Err: 
		If Err.Number Then
			InsPostMGI1410Upd = False
		End If
		On Error GoTo 0
	End Function
End Class






