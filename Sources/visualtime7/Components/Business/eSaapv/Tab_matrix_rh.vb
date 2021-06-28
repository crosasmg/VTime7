Option Strict Off
Option Explicit On
Public Class Tab_matrix_rh
	
	
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	
	Public nType_move As Integer ' NUMBER        22     5      0         No
	Public nOrigin As Integer ' NUMBER        22     5      0         No
	Public nTyp_ProfitWorker As Integer ' NUMBER        22     5      0         No
	Public nTransac As Integer ' NUMBER        22     5      0         No
	'Public nUsercode         As Long     ' NUMBER        22     5      0         No
	'Public dCompdate         As Date     ' DATE           0     5      0         No
	
	Public Function insValMVI1488_K(ByVal sAction As String, ByVal nType_move As Integer, ByVal nOrigin As Integer, ByVal nTyp_ProfitWorker As Integer, ByVal nTransac As Integer) As String
		Dim lrecinsValMVI1488_K As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValMVI1488_K_Err
		lrecinsValMVI1488_K = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValMVI1488_K
			.StoredProcedure = "insMVI1488pkg.insvalMVI1488_K"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_ProfitWorker", nTyp_ProfitWorker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("MVI1488",  ,  ,  ,  ,  , lstrErrors)
		
		insValMVI1488_K = lclsErrors.Confirm
		
insValMVI1488_K_Err: 
		If Err.Number Then
			insValMVI1488_K = "insValMVI1488_K: " & Err.Description
		End If
		
		lrecinsValMVI1488_K = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	Public Function insPostMVI1488(ByVal sAction As String, ByVal nType_move As Integer, ByVal nOrigin As Integer, ByVal nTyp_ProfitWorker As Integer, ByVal nTransac As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostMVI1488 As eRemoteDB.Execute
		
		On Error GoTo insPostMVI1488_Err
		lrecinsPostMVI1488 = New eRemoteDB.Execute
		
		With lrecinsPostMVI1488
			.StoredProcedure = "insMVI1488pkg.inspostMVI1488"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_ProfitWorker", nTyp_ProfitWorker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMVI1488 = .Run(False)
		End With
		
insPostMVI1488_Err: 
		If Err.Number Then
			insPostMVI1488 = False
		End If
		
		lrecinsPostMVI1488 = Nothing
		
		On Error GoTo 0
	End Function
End Class






