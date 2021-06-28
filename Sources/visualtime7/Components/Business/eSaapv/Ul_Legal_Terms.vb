Option Strict Off
Option Explicit On
Public Class Ul_Legal_Terms
	
	
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	
	Public nType_saapv As Integer ' NUMBER        22     5      0         No
	Public dEffecdate As Date ' DATE           0     5      0         No
	Public nValuesmo As Integer ' NUMBER        22     5      0         No
	Public nValuesty As Integer ' NUMBER        22     5      0         No
	Public nUsercode As Integer ' NUMBER        22     5      0         No
	Public dCompdate As Date ' DATE           0     5      0         No
	Public dNulldate As Date ' DATE           0     5      0         No
	Public nDayadd As Integer ' NUMBER        22     5      0         No
	Public Function insValMVI7300(ByVal sAction As String, ByVal nType_saapv As Integer, ByVal nValuesmo As Integer, ByVal nValuesty As Integer, ByVal nDayadd As Integer, ByVal dEffecdate As Date) As String
		Dim lrecinsValMVI7300 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValMVI7300_Err
		lrecinsValMVI7300 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValMVI7300
			.StoredProcedure = "insMVI7300pkg.insvalMVI7300"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesmo", nValuesmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesty", nValuesty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayadd", nDayadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("MVI7300",  ,  ,  ,  ,  , lstrErrors)
		
		insValMVI7300 = lclsErrors.Confirm
		
insValMVI7300_Err: 
		If Err.Number Then
			insValMVI7300 = "insValMVI7300: " & Err.Description
		End If
		
		lrecinsValMVI7300 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	Public Function insValMVI7300_K(ByVal dEffecdate As Date) As String
		Dim lrecinsValMVI7300_K As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValMVI7300_K_Err
		lrecinsValMVI7300_K = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValMVI7300_K
			.StoredProcedure = "insMVI7300pkg.insvalMVI7300_K"
			
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("MVI7300",  ,  ,  ,  ,  , lstrErrors)
		
		insValMVI7300_K = lclsErrors.Confirm
		
insValMVI7300_K_Err: 
		If Err.Number Then
			insValMVI7300_K = "insValMVI7300_K: " & Err.Description
		End If
		
		lrecinsValMVI7300_K = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	Public Function insPostMVI7300(ByVal sAction As String, ByVal dEffecdate As Date, ByVal nType_saapv As Integer, ByVal nValuesmo As Integer, ByVal nValuesty As Integer, ByVal nDayadd As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostMVI7300 As eRemoteDB.Execute
		
		On Error GoTo insPostMVI7300_Err
		lrecinsPostMVI7300 = New eRemoteDB.Execute
		
		With lrecinsPostMVI7300
			.StoredProcedure = "insMVI7300pkg.inspostMVI7300"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesmo", nValuesmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesty", nValuesty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayadd", nDayadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMVI7300 = .Run(False)
		End With
		
insPostMVI7300_Err: 
		If Err.Number Then
			insPostMVI7300 = False
		End If
		
		lrecinsPostMVI7300 = Nothing
		
		On Error GoTo 0
	End Function
End Class






