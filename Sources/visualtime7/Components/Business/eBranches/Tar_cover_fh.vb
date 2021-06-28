Option Strict Off
Option Explicit On
Public Class Tar_cover_fh
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_cover_fh.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla tar_cover_fh al 10-04-2002 17:09:36
	'+  Property                       Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nCap_initial As Double ' NUMBER     22   6     18   N
	Public nProvince As Integer ' NUMBER     22   0     5    N
	Public nCap_end As Double ' NUMBER     22   6     18   S
	Public nMunicipality As Integer ' NUMBER     22   0     5    N
	Public nConstcat As Integer ' NUMBER     22   0     5    N
	Public nRate As Double ' NUMBER     22   2     5    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdTar_cover_fh: Se encarga de actualizar la tabla Tar_cover_fh
	Private Function InsUpdTar_cover_fh(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTarCoverFh As eRemoteDB.Execute
		On Error GoTo lrecinsUpdTarCoverFh_Err
		
		lrecinsUpdTarCoverFh = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdTarcoverfh al 03-01-2002 17:17:23
		'+
		With lrecinsUpdTarCoverFh
			.StoredProcedure = "insUpdtar_cover_fh"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_initial", nCap_initial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstcat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTar_cover_fh = .Run(False)
		End With
		
lrecinsUpdTarCoverFh_Err: 
		If Err.Number Then
			InsUpdTar_cover_fh = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsUpdTarCoverFh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTarCoverFh = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_cover_fh(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_cover_fh(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_cover_fh(3)
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim nExist As Integer
		Dim lrecinsValeffecdate_tar_cover_fh As eRemoteDB.Execute
		On Error GoTo lrecinsValeffecdate_tar_cover_fh_Err
		
		lrecinsValeffecdate_tar_cover_fh = New eRemoteDB.Execute
		
		nExist = 0
		'+
		'+ Definición de store procedure insValeffecdate_tar_cover_fh
		'+
		With lrecinsValeffecdate_tar_cover_fh
			.StoredProcedure = "insValEffecdate_tar_cover_fh"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Es valido si no retorna registros mayores a la fecha
			'+
			If .Run(False) Then
				If .Parameters("nExist").Value = 0 Then
					InsValEffecdate = False
				Else
					InsValEffecdate = True
				End If
			End If
			
		End With
		
lrecinsValeffecdate_tar_cover_fh_Err: 
		If Err.Number Then
			InsValEffecdate = True
		End If
		
		'UPGRADE_NOTE: Object lrecinsValeffecdate_tar_cover_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValeffecdate_tar_cover_fh = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMIN652_K: Validaciones de la transacción(Header)
	Public Function InsValMIN652_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lTar_cover_fh As eBranches.Tar_cover_fh
		Dim lobjValues As eFunctions.Values
		
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lTar_cover_fh = New eBranches.Tar_cover_fh
		lobjValues = New eFunctions.Values
		
		On Error GoTo insVal_MIN652_K_Err
		
		'+Validación del campo nBranch.
		If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
			Call lerrTime.ErrorMessage(sCodispl, 1022,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+Validación del campo nProduct.
		If nProduct = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 11009,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+Validación del campo nCurrency.
		If (nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0) Then
			Call lerrTime.ErrorMessage(sCodispl, 10107,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+Validación del campo nCover.
		If nModulec <= 0 Then
			If (nCover = eRemoteDB.Constants.intNull And nCover <> 0) Then
				Call lerrTime.ErrorMessage(sCodispl, 3245,  , eFunctions.Errors.TextAlign.LeftAling)
			End If
		End If
		
		'+Validación del campo nModulec.
		If (nModulec = eRemoteDB.Constants.intNull) Then
			Call lerrTime.ErrorMessage(sCodispl, 12112,  , eFunctions.Errors.TextAlign.RigthAling)
		End If
		
		'+Validación del campo dEffectdate.
		If dEffecdate <> dtmNull Then
			If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nAction = 302 Then
					If InsValEffecdate(nBranch, nProduct, nModulec, nCover, nCurrency, dEffecdate) Then
						Call lerrTime.ErrorMessage(sCodispl, 55611,  , eFunctions.Errors.TextAlign.RigthAling)
					End If
				End If
			End If
		Else
			Call lerrTime.ErrorMessage(sCodispl, 2056,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		InsValMIN652_K = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		'UPGRADE_NOTE: Object lTar_cover_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lTar_cover_fh = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insVal_MIN652_K_Err: 
		If Err.Number Then
			InsValMIN652_K = InsValMIN652_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%InsValMIN652: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MIN652)
	Public Function InsValMIN652(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nConstcat As Integer, ByVal nProvince As Integer, ByVal nMunicipality As Integer, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nRate As Double, ByVal nPremium As Double) As String
		
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim nerrornum As Integer
		Dim lnAction As Integer
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		
		On Error GoTo insValMIN652_Err
		
		'+ Validacion de los campos nRate y nPremium
		If (nRate = eRemoteDB.Constants.intNull And nPremium = eRemoteDB.Constants.intNull Or nRate = 0 And nPremium = 0) Then
			Call lerrTime.ErrorMessage(sCodispl, 60208,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+ Validación del campo nCap_end
		If nCap_end > 0 And nCap_initial > 0 Then
			If nCap_end <= nCap_initial Then
				Call lerrTime.ErrorMessage(sCodispl, 10148,  , eFunctions.Errors.TextAlign.LeftAling)
			End If
		End If
		
		'+ Validacion el rango nCap_initial y nCap_end
		If sAction = "Update" Or sAction = "Upd" Then
			lnAction = 302
		Else
			If sAction = "Add" Then
				lnAction = 301
			End If
		End If
		nerrornum = InsValCapIinitialEnd(lnAction, nBranch, nProduct, nModulec, nCover, nCurrency, dEffecdate, nCap_initial, nCap_end, nConstcat, nProvince, nMunicipality)
		If nerrornum <> 0 Then
			Call lerrTime.ErrorMessage(sCodispl, nerrornum,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		InsValMIN652 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		
insValMIN652_Err: 
		If Err.Number Then
			InsValMIN652 = InsValMIN652 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%InsPostMIN652: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MIN652)
	Public Function InsPostMIN652(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nConstcat As Integer, ByVal nProvince As Integer, ByVal nMunicipality As Integer, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nRate As Double, ByVal nPremium As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMIN652_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nConstcat = nConstcat
			.nProvince = nProvince
			.nMunicipality = nMunicipality
			.nCap_initial = IIf(nCap_initial = eRemoteDB.Constants.intNull, 0, nCap_initial)
			.nCap_end = IIf(nCap_end = eRemoteDB.Constants.intNull, 0, nCap_end)
			.nRate = IIf(nRate = eRemoteDB.Constants.intNull, 0, nRate)
			.nPremium = IIf(nPremium = eRemoteDB.Constants.intNull, 0, nPremium)
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMIN652 = Add
			Case "Update"
				InsPostMIN652 = Update
			Case "Del"
				InsPostMIN652 = Delete
		End Select
		
InsPostMIN652_Err: 
		If Err.Number Then
			InsPostMIN652 = False
		End If
		On Error GoTo 0
	End Function
	
	'Funcion que valida em modulo este asociado a la cobertura
	Public Function InsValGenCover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim nModulecout As Integer
		Dim lrecinsValGen_Cover As eRemoteDB.Execute
		On Error GoTo lrecinsValGen_Cover_Err
		
		lrecinsValGen_Cover = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insValGen_Cover
		'+
		With lrecinsValGen_Cover
			.StoredProcedure = "insValGen_Cover_TarCoverFh"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulecout", nModulecout, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Es valido si no retorna filas
			'+
			.Run(False)
			If .Parameters("nModulecout").Value = dtmNull Then
				InsValGenCover = True
			Else
				InsValGenCover = False
			End If
			
		End With
		
lrecinsValGen_Cover_Err: 
		If Err.Number Then
			InsValGenCover = True
		End If
		'UPGRADE_NOTE: Object lrecinsValGen_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValGen_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'Funcion que valida el rango nCap_initial y nCap_end
	Public Function InsValCapIinitialEnd(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nConstcat As Integer, ByVal nProvince As Integer, ByVal nMunicipality As Integer) As Integer
		Dim nerrornum As Integer
		Dim InsValCapIinitialEnd_tar_cover_fh As eRemoteDB.Execute
		On Error GoTo InsValCapIinitialEnd_tar_cover_fh_Err
		
		InsValCapIinitialEnd_tar_cover_fh = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insValeffecdate_tar_cover_fh
		'+
		With InsValCapIinitialEnd_tar_cover_fh
			.StoredProcedure = "InsValCapInitialEnd_TarCoverFh"
			With .Parameters
				.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCap_initial", nCap_initial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nConstcat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nErrornum", nerrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Retorna el código de error a desplegar o cero no tiene errores
			'+
			.Run(False)
			InsValCapIinitialEnd = .Parameters("nErrornum").Value
		End With
		
InsValCapIinitialEnd_tar_cover_fh_Err: 
		If Err.Number Then
			InsValCapIinitialEnd = 0
		End If
		'UPGRADE_NOTE: Object InsValCapIinitialEnd_tar_cover_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		InsValCapIinitialEnd_tar_cover_fh = Nothing
		On Error GoTo 0
	End Function
End Class






