Option Strict Off
Option Explicit On
Public Class UsersWeb
	
	Public sCLient As String
	Public sUser As String
	Public sPassword As String
	Public sStatregt As String
	Public nRol As Short
	Public nUsercode As Short
	'% Find: Función que realiza la busqueda en la tabla client dado un codigo de cliente....
	Public Function Find(ByRef sCLient As String) As Boolean
		Dim lrecUsersWeb As eRemoteDB.Execute
		Dim lclsSecurity As Object
		
		On Error GoTo Find_err
		
		lrecUsersWeb = New eRemoteDB.Execute
		With lrecUsersWeb
			.StoredProcedure = "reaUserWeb"
			.Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				lclsSecurity = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.CryptSupport")
				Me.sCLient = sCLient
				sUser = .FieldToClass("sUser")
				sPassword = .FieldToClass("sPassword")
				sPassword = lclsSecurity.DecryptString(sPassword)
				sStatregt = .FieldToClass("sStatregt")
				nRol = .FieldToClass("nRol")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUsersWeb = Nothing
		'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurity = Nothing
		
		
	End Function
	'% Upd: Actualiza la informacion de usuarios WEB
	Public Function Upd(ByVal sCLient As String) As Boolean
		Dim lreccreUsersWeb As eRemoteDB.Execute
		
		lreccreUsersWeb = New eRemoteDB.Execute
		
		With lreccreUsersWeb
			.StoredProcedure = "insUsersWeb"
			.Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUser", sUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPassword", sPassword, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRol", nRol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Upd = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreUsersWeb = Nothing
		If Upd Then
			sCLient = sCLient
		End If
	End Function
	
	Public Function valBC9001(ByRef sCLient As String, ByRef sUser As String, ByRef sPassword As String, ByRef nRol As Short, ByRef sStatregt As String, ByRef sCodispl As String) As String
		
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lobjErrors As Object
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        Dim lblnMessage As Boolean


        Dim lreccreUsersWeb As eRemoteDB.Execute

        On Error GoTo valBC9001_Err

        lreccreUsersWeb = New eRemoteDB.Execute

        With lreccreUsersWeb
            .StoredProcedure = "insBC9001PKG.valBC9001"
            .Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUser", sUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPassword", sPassword, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRol", nRol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            valBC9001 = .Confirm
        End With

valBC9001_Err:
        If Err.Number Then
            valBC9001 = "valBC9001: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing

    End Function

    Public Function PostBC9001(ByRef sCLient As String, ByRef sUser As String, ByRef sPassword As String, ByRef nRol As Short, ByRef sStatregt As String, ByRef sCodispl As String, ByRef nUsercode As Short) As Boolean

        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lobjErrors As Object
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        Dim lblnMessage As Boolean
        Dim lclsClientWin As New ClientWin
        Dim lclsSecurity As Object

        Dim lreccreUsersWeb As eRemoteDB.Execute

        On Error GoTo PostBC9001_Err
        lclsSecurity = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.CryptSupport")
        lreccreUsersWeb = New eRemoteDB.Execute
        sPassword = lclsSecurity.EncryptString(sPassword)
        With lreccreUsersWeb
            .StoredProcedure = "insBC9001PKG.PostBC9001"
            .Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUser", sUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPassword", sPassword, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRol", nRol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            PostBC9001 = .Run(False)
        End With
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing
        'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecurity = Nothing
        lclsErrors = New eFunctions.Errors
        If PostBC9001 Then
            lclsClientWin = New ClientWin
            Call lclsClientWin.insUpdClient_win(sCLient, "BC9001", "2", , , nUsercode)
            'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsClientWin = Nothing
        End If


PostBC9001_Err:
        If Err.Number Then
            PostBC9001 = CBool("PostBC9001: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing

    End Function
    Public Function PostBC9001_upd(ByRef nAction As Short, ByRef sCLient As String, ByRef nBranch As Short, ByRef nProduct As Short, ByRef nUsercode As Short) As Boolean

        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lobjErrors As Object
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        Dim lblnMessage As Boolean


        Dim lreccreUsersWeb As eRemoteDB.Execute

        On Error GoTo PostBC9001_upd_Err

        lreccreUsersWeb = New eRemoteDB.Execute

        With lreccreUsersWeb
            .StoredProcedure = "insBC9001PKG.PostBC9001_upd"
            .Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            PostBC9001_upd = .Run(False)
        End With
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing
        lclsErrors = New eFunctions.Errors


PostBC9001_upd_Err:
        If Err.Number Then
            PostBC9001_upd = CBool("PostBC9001_upd: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing

    End Function

    Public Function valBC9001_upd(ByRef sCLient As String, ByRef nBranch As Short, ByRef nProduct As Short, ByRef sCodispl As String) As String

        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lobjErrors As Object
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        Dim lblnMessage As Boolean


        Dim lreccreUsersWeb As eRemoteDB.Execute

        On Error GoTo valBC9001_Err

        lreccreUsersWeb = New eRemoteDB.Execute

        With lreccreUsersWeb
            .StoredProcedure = "insBC9001PKG.valBC9001_upd"
            .Parameters.Add("sClient", sCLient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            valBC9001_upd = .Confirm
        End With

valBC9001_Err:
        If Err.Number Then
            valBC9001_upd = "valBC9001_upd: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lreccreUsersWeb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreUsersWeb = Nothing

    End Function
End Class






