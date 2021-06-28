Option Strict Off
Option Explicit On
Public Class Tab_state_saapv
	
	
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	
	Public nType_saapv As Integer ' NUMBER        22     5      0         No
	Public nType_state_origi As Integer ' NUMBER        22     5      0         No
	Public nType_state_end As Integer ' NUMBER        22     5      0         No
	Public nUsercode As Integer ' NUMBER        22     5      0         No
	Public dCompdate As Date ' DATE           0     5      0         No
	Public Function insValMVI7500_K(ByVal sAction As String, ByVal nType_saapv As Integer, ByVal nType_state_origi As Integer, ByVal nType_state_end As Integer) As String
		Dim lrecinsValMVI7500_K As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValMVI7500_K_Err
		lrecinsValMVI7500_K = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValMVI7500_K
			.StoredProcedure = "insMVI7500pkg.insvalMVI7500_K"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_state_origi", nType_state_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_state_end", nType_state_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("MVI7500",  ,  ,  ,  ,  , lstrErrors)
		
		insValMVI7500_K = lclsErrors.Confirm
		
insValMVI7500_K_Err: 
		If Err.Number Then
			insValMVI7500_K = "insValMVI7500_K: " & Err.Description
		End If
		
		lrecinsValMVI7500_K = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	Public Function insPostMVI7500(ByVal sAction As String, ByVal nType_saapv As Integer, ByVal nType_state_origi As Integer, ByVal nType_state_end As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostMVI7500 As eRemoteDB.Execute
		
		On Error GoTo insPostMVI7500_Err
		lrecinsPostMVI7500 = New eRemoteDB.Execute
		
		With lrecinsPostMVI7500
			.StoredProcedure = "insMVI7500pkg.inspostMVI7500"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_state_origi", nType_state_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_state_end", nType_state_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMVI7500 = .Run(False)
		End With
		
insPostMVI7500_Err: 
		If Err.Number Then
			insPostMVI7500 = False
		End If
		
		lrecinsPostMVI7500 = Nothing
		
		On Error GoTo 0
	End Function
End Class






