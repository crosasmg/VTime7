Option Strict Off
Option Explicit On
Public Class Saapv_Transfer
	
	'+ Column_Name
	'------------------------------
	Public nCod_saapv As Double
	Public nFunds_origin As Integer
	Public nTax_regime As Integer
	Public sAfp_type As String
	Public nType_transfer As Integer
	Public nSaving_Loc As Double
	Public nSaving_UF As Double
	Public nSaving_PCT As Double
	Public nUsercode As Integer
	Public dCompdate As Date
	Public nInstitution As Integer
	
	Public Function insValVI7501_F(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal nFunds_origin As Integer, ByVal nTax_regime As Integer, ByVal sAfp_type As String, ByVal nType_transfer As Integer, ByVal nSaving_Loc As Double, ByVal nSaving_UF As Double, ByVal nSaving_PCT As Double, ByVal nInstitution As Integer) As String
		Dim lrecinsValVI7501_F As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_F_Err
		lrecinsValVI7501_F = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsValVI7501_F
			.StoredProcedure = "insVI7501_F_pkg.insvalVI7501_F"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds_origin", nFunds_origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_regime", nTax_regime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAfp_type", sAfp_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_transfer", nType_transfer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_Loc", nSaving_Loc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_UF", nSaving_UF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_PCT", nSaving_PCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_F",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_F = lclsErrors.Confirm
		
insValVI7501_F_Err: 
		If Err.Number Then
			insValVI7501_F = "insValVI7501_F: " & Err.Description
		End If
		
		lrecinsValVI7501_F = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	Public Function insPostVI7501_F(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal nFunds_origin As Integer, ByVal nTax_regime As Integer, ByVal sAfp_type As String, ByVal nType_transfer As Integer, ByVal nSaving_Loc As Double, ByVal nSaving_UF As Double, ByVal nSaving_PCT As Double, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_F As eRemoteDB.Execute
		
		On Error GoTo insPostVI7501_F_Err
		lrecinsPostVI7501_F = New eRemoteDB.Execute
		
		With lrecinsPostVI7501_F
			.StoredProcedure = "insVI7501_F_pkg.inspostVI7501_F"
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds_origin", nFunds_origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_regime", nTax_regime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAfp_type", sAfp_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_transfer", nType_transfer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_Loc", nSaving_Loc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_UF", nSaving_UF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_PCT", nSaving_PCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostVI7501_F = .Run(False)
		End With
		
insPostVI7501_F_Err: 
		If Err.Number Then
			insPostVI7501_F = False
		End If
		
		lrecinsPostVI7501_F = Nothing
		
		On Error GoTo 0
	End Function
End Class






