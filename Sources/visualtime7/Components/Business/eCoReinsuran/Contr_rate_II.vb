Option Strict Off
Option Explicit On
Public Class Contr_rate_II
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_rate_II.cls                        $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/10/03 6:15p                               $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public sSmoking As String ' CHAR       1    0     0    N
	Public sPeriodpol As String ' CHAR       1    0     0    N
	Public nTyperisk As Integer ' NUMBER     22   2     10   N
	Public nCap_ini As Double ' NUMBER     22   0     12   N
	Public nAge_reinsu As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nCap_end As Double ' NUMBER     22   0     12   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRatewomen As Double ' NUMBER     22   6     8    S
	Public nPremwomen As Double ' NUMBER     22   2     10   S
	Public nRatemen As Double ' NUMBER     22   6     8    S
	Public nPremmen As Double ' NUMBER     22   2     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%getCountRange: Busca los registros que tengan un capital inicial o final
	'                dentro de un rango que ya existe
	Public Function getCountRange(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nCap_end As Double, ByVal dEffecdate As Date) As Integer
		
		Dim lrecCoutRange_contr_rate_ii As eRemoteDB.Execute
		
		On Error GoTo CountRange_err
		
		lrecCoutRange_contr_rate_ii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure couNtrange_contr_rate_ii al 04-03-2002 16:43:30
		'+
		With lrecCoutRange_contr_rate_ii
			.StoredProcedure = "countrange_contr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				getCountRange = .FieldToClass("nCount")
			End If
			
		End With
		
CountRange_err: 
		If Err.Number Then
			getCountRange = eRemoteDB.Constants.intNull
		End If
		'UPGRADE_NOTE: Object lrecCoutRange_contr_rate_ii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCoutRange_contr_rate_ii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreContr_rate_ii As eRemoteDB.Execute
		
		On Error GoTo creContr_rate_ii_Err
		
		lreccreContr_rate_ii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure creContr_rate_ii al 04-04-2002 17:36:53
		'+
		With lreccreContr_rate_ii
			.StoredProcedure = "creContr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
creContr_rate_ii_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreContr_rate_ii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreContr_rate_ii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Dim lrecupdContr_rate_ii As eRemoteDB.Execute
		
		On Error GoTo Upd_Err
		
		lrecupdContr_rate_ii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure updContr_rate_ii al 04-04-2002 15:47:44
		'+
		With lrecupdContr_rate_ii
			.StoredProcedure = "updContr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Upd_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdContr_rate_ii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContr_rate_ii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Dim lrecdelContr_rate_ii As eRemoteDB.Execute
		
		On Error GoTo Del_Err
		
		lrecdelContr_rate_ii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure delContr_rate_ii al 04-04-2002 15:51:12
		'+
		With lrecdelContr_rate_ii
			.StoredProcedure = "delContr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Del_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelContr_rate_ii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelContr_rate_ii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValCR765_K: Validaciones de la transacción(Header)
	Public Function insValCR765_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nCap_end As Double, ByVal dEffecdate As Date, ByVal nDuplicate As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lintCount As Integer
		Dim lobjContr_rate_iis As Contr_rate_IIs
		
		lclsErrors = New eFunctions.Errors
		lclsContrproc = New eCoReinsuran.Contrproc
		lobjContr_rate_iis = New Contr_rate_IIs
		
		On Error GoTo insValCR765_k_Err
		
		If sSmoking = String.Empty Or sSmoking = "2" Then
			sSmoking = "2"
		Else
			sSmoking = "1"
		End If
		
		'+ Valida que el registro a duplicar no exista en Contr_rate_i
		If nDuplicate = 1 And lobjContr_rate_iis.Find(nNumber, nBranch_rei, nType, nCovergen, sSmoking, sPeriodpol, nTyperisk, nCap_ini, dEffecdate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55858)
		End If
		
		'+ Se valida el ramo del reaseguro
		If nBranch_rei = eRemoteDB.Constants.intNull Or nBranch_rei = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60314)
		End If
		
		'+Se valida que el código del contrato
		If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3357)
		End If
		
		'+Se valida que el tipo de contrato
		If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6018)
		End If
		
		'+Se valida la cobertura genérica
		If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60315)
		End If
		
		'+Validacion de la Fecha de Inicio de Vigencia del contrato
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60300)
		End If
		
		'+ Validación del rango de capital inicial
		If nCap_ini = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11111)
		End If
		
		If nAction = 301 Then
			lintCount = 0
			lintCount = getCountRange(nNumber, nBranch_rei, nType, nCovergen, sSmoking, sPeriodpol, nTyperisk, nCap_ini, nCap_end, dEffecdate)
			If lintCount > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11138)
			End If
		End If
		
		'+ Validación del rango de capital final
		If nCap_end = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11112)
		Else
			'+ Validacion del rango final no puede ser menor al inicial
			If nCap_end < nCap_ini Then
				Call lclsErrors.ErrorMessage(sCodispl, 10148)
			End If
		End If
		
		'+ Valida que el contrato este registrado en la tabla de contratos
		'+ proporcionales "contrmaster"
		If nAction = 301 Or nAction = 306 Then
			If Not lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 21002)
			End If
		End If
		
		insValCR765_k = lclsErrors.Confirm
		
insValCR765_k_Err: 
		If Err.Number Then
			insValCR765_k = insValCR765_k & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrproc = Nothing
		'UPGRADE_NOTE: Object lobjContr_rate_iis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjContr_rate_iis = Nothing
		
		On Error GoTo 0
	End Function
	'%InsPostCR765: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CR765)
	Public Function InsPostCR765(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nAge_reinsu As Integer, ByVal dEffecdate As Date, ByVal nCap_end As Double, ByVal nRatewomen As Double, ByVal nPremwomen As Double, ByVal nRatemen As Double, ByVal nPremmen As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostCR765_Err
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nCovergen = nCovergen
			
			If sSmoking = String.Empty Or sSmoking = "2" Then
				.sSmoking = "2"
			Else
				.sSmoking = "1"
			End If
			
			.sPeriodpol = sPeriodpol
			.nTyperisk = nTyperisk
			.nCap_ini = nCap_ini
			.nAge_reinsu = nAge_reinsu
			.dEffecdate = dEffecdate
			.nCap_end = nCap_end
			.nRatewomen = nRatewomen
			.nPremwomen = nPremwomen
			.nRatemen = nRatemen
			.nPremmen = nPremmen
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostCR765 = Add
			Case "Update"
				InsPostCR765 = Update
			Case "Del"
				InsPostCR765 = Delete
		End Select
		
InsPostCR765_Err: 
		If Err.Number Then
			InsPostCR765 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nNumber = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nType = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		sSmoking = String.Empty
		sPeriodpol = String.Empty
		nTyperisk = eRemoteDB.Constants.intNull
		nCap_ini = eRemoteDB.Constants.intNull
		nAge_reinsu = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nCap_end = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nRatewomen = eRemoteDB.Constants.intNull
		nPremwomen = eRemoteDB.Constants.intNull
		nRatemen = eRemoteDB.Constants.intNull
		nPremmen = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Public Function InsValCR765(ByVal sCodispl As String, ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nAge_reinsu As Integer, ByVal dEffecdate As Date, ByVal nRatewomen As Double, ByVal nPremwomen As Double, ByVal nRatemen As Double, ByVal nPremmen As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContr_rate_IIs As eCoReinsuran.Contr_rate_IIs
		
		lclsErrors = New eFunctions.Errors
		lclsContr_rate_IIs = New eCoReinsuran.Contr_rate_IIs
		
		On Error GoTo insValCR765_Err
		
		'+ Se valida que el registro no exista en la tabla CONTR_RATE_II
		If sAction = "Add" Then
			If lclsContr_rate_IIs.FindCR765(nNumber, nBranch_rei, nType, nCovergen, sSmoking, sPeriodpol, nTyperisk, nCap_ini, nAge_reinsu, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55845)
			End If
		End If
		
		'+ Se valida la edad actuarial
		If nAge_reinsu = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3954)
		End If
		
		'+ Se valida que exista al menos un valor en el campo hombres
		If nRatemen = eRemoteDB.Constants.intNull And nPremmen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60316)
		End If
		
		'+ Se valida que exista al menos un valor en el campo mujeres
		If nRatewomen = eRemoteDB.Constants.intNull And nPremwomen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60317)
		End If
		
		InsValCR765 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContr_rate_IIs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContr_rate_IIs = Nothing
		
insValCR765_Err: 
		If Err.Number Then
			InsValCR765 = InsValCR765 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% InsDupContr_rate_II: Invoca al procedimiento que duplica, la información
	'%                      de la tabla para un nueva llave
	Public Function InsDupContr_rate_II(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nCap_end As Double, ByVal dEffecdate As Date, ByVal nNumber_new As Integer, ByVal nBranch_rei_new As Integer, ByVal nType_new As Integer, ByVal nCovergen_new As Integer, ByVal sSmoking_new As String, ByVal sPeriodpol_new As String, ByVal nTyperisk_new As Integer, ByVal nCap_ini_new As Double, ByVal nCap_end_new As Double, ByVal dEffecdate_new As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsDupcontr_rate_ii As eRemoteDB.Execute
		
		On Error GoTo insDupcontr_rate_ii_Err
		
		lrecinsDupcontr_rate_ii = New eRemoteDB.Execute
		
		'+ Definición de store procedure insDupcontr_rate_i al 04-24-2002 15:52:02
		
		With lrecinsDupcontr_rate_ii
			.StoredProcedure = "insDupcontr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber_new", nNumber_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei_new", nBranch_rei_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_new", nType_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen_new", nCovergen_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking_new", sSmoking_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol_new", sPeriodpol_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk_new", nTyperisk_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini_new", nCap_ini_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end_new", nCap_end_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_new", dEffecdate_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsDupContr_rate_II = True
			Else
				InsDupContr_rate_II = False
			End If
		End With
		
insDupcontr_rate_ii_Err: 
		If Err.Number Then
			InsDupContr_rate_II = False
		End If
		'UPGRADE_NOTE: Object lrecinsDupcontr_rate_ii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDupcontr_rate_ii = Nothing
		On Error GoTo 0
	End Function
End Class






