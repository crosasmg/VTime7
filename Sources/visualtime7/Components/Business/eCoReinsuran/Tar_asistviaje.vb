Option Strict Off
Option Explicit On
Public Class Tar_asistviaje
	
	'+
	'+ Estructura de tabla contr_rate_III al 04-08-2002 16:26:00
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public nCapital As Double ' NUMBER     22   6    18    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nDay_ini As Integer ' NUMBER     22   0     5    N
	Public nDay_end As Integer ' NUMBER     22   0     5    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nTar_min As Double ' NUMBER     22   6    18    S
	Public nTar_adic As Double ' NUMBER     22   6    18    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public Function InsPostCR768(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nCapital As Integer, ByVal nDiaInicial As Integer, ByVal nDiaFinal As Integer, ByVal nTarMinima As Integer, ByVal nTarAdicional As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsErrors As eFunctions.Errors
		Dim lrec_tarasistviaje As eRemoteDB.Execute
		
		On Error GoTo insValCR768_Err
		
		lrec_tarasistviaje = New eRemoteDB.Execute
		
		With lrec_tarasistviaje
			.StoredProcedure = "insupdcontr_tarasistviaje"
			.Parameters.Add("sAccion", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_ini", nDiaInicial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_end", nDiaFinal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTar_min", nTarMinima, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTar_adic", nTarAdicional, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsPostCR768 = True
			Else
				InsPostCR768 = False
			End If
		End With
		
insValCR768_Err: 
		If Err.Number Then
			InsPostCR768 = False
		End If
		'UPGRADE_NOTE: Object lrec_tarasistviaje may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_tarasistviaje = Nothing
		On Error GoTo 0
	End Function
	
	
	Public Function InsValCR768(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nCapital As Integer, ByVal nDiaInicial As Integer, ByVal nDiaFinal As Integer, ByVal nTarMinima As Integer, ByVal nTarAdicional As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcls_tarasistviajes As eCoReinsuran.tar_asistviajes
		
		lcls_tarasistviajes = New eCoReinsuran.tar_asistviajes
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR768_Err
		
		'+ Se valida que el registro no exista en la tabla VIAJES
		If sAction = "Add" Then
			If lcls_tarasistviajes.FindCR768(nBranch, nProduct, nNumber, nBranchRei, nCovergen, nCapital, dEffecdate, nDiaInicial) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1)
			End If
		End If
		
		If nDiaInicial = eRemoteDB.Constants.intNull Or nDiaInicial = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300024)
		End If
		
		If nDiaFinal = eRemoteDB.Constants.intNull Or nDiaFinal = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300025)
		End If
		
		If nDiaInicial > nDiaFinal Then
			Call lclsErrors.ErrorMessage(sCodispl, 300027)
		End If
		
		If nTarMinima = eRemoteDB.Constants.intNull Or nTarMinima = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300012)
		End If
		
		
		InsValCR768 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR768_Err: 
		If Err.Number Then
			InsValCR768 = "InsValCR768: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	Public Function InsValCR768_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nCapital As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR768_K_Err
		
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3357)
		End If
		
		If nBranchRei = eRemoteDB.Constants.intNull Or nBranchRei = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60314)
		End If
		
		If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60315)
		End If
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9068)
		End If
		
		If nCapital = eRemoteDB.Constants.intNull Or nCapital = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300026)
		End If
		
		InsValCR768_K = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR768_K_Err: 
		If Err.Number Then
			InsValCR768_K = "InsValCR768_K: " & Err.Description
		End If
		On Error GoTo 0
	End Function
End Class






