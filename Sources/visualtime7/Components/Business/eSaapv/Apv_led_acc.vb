Option Strict Off
Option Explicit On
Public Class Apv_led_acc
	
	
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	
	Public nType_move As Integer ' NUMBER        22     5      0         No
	Public nTyp_profitworker As Integer ' NUMBER        22     5      0         No
	Public sLedacc As String ' NUMBER        22     5      0         No
	Public sDescled As String
	Public nUsercode As Integer ' NUMBER        22     5      0         No
	Public dCompdate As Date ' DATE           0     5      0         No
	Public Function insValMCA1485(ByVal nType_move As Integer, ByVal nTyp_profitworker As Integer, ByVal sLedacc As String, ByVal sDescled As String) As String
        Dim lrecinsValMCA1485 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = New Object

        On Error GoTo insValMCA1485_Err
		lrecinsValMCA1485 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValMCA1485
			.StoredProcedure = "INSMCA1485PKG.INSVALMCA1485"
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profitworker", nTyp_profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLedacc", sLedacc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescled", sDescled, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("MCA1485",  ,  ,  ,  ,  , lstrErrors)
		
		insValMCA1485 = lclsErrors.Confirm
		
insValMCA1485_Err: 
		If Err.Number Then
			insValMCA1485 = "insValMCA1485: " & Err.Description
		End If
		
		lrecinsValMCA1485 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	Public Function insPostMCA1485(ByVal nAction As String, ByVal nType_move As Integer, ByVal nTyp_profitworker As Integer, ByVal sLedacc As String, ByVal sDescled As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostMCA1485 As eRemoteDB.Execute
		
		On Error GoTo insPostMCA1485_Err
		lrecinsPostMCA1485 = New eRemoteDB.Execute
		
		With lrecinsPostMCA1485
			.StoredProcedure = "insMCA1485pkg.inspostMCA1485"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profitworker", nTyp_profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLedacc", sLedacc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescled", sDescled, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMCA1485 = .Run(False)
		End With
		
insPostMCA1485_Err: 
		If Err.Number Then
			insPostMCA1485 = False
		End If
		
		lrecinsPostMCA1485 = Nothing
		
		On Error GoTo 0
	End Function
End Class






