Option Strict Off
Option Explicit On
Public Class Tar_fire_fh
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_fire_fh.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 29                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla tar_fire_fh al 04-04-2002 11:25:14
	'-  Property                    Type           DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nConstcat As Integer ' NUMBER     22   0     5    N
	Public nCap_initial As Double ' NUMBER     22   6     18   N
	Public nCap_end As Double ' NUMBER     22   6     18   S
	Public nRate As Double ' NUMBER     22   2     5    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTArFireFh(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTArFireFh(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTArFireFh(3)
	End Function
	
	'Función que valida el ramo, producto, moneda y fecha
	Public Function insvalMIN651_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lTar_fire_fh As eBranches.Tar_fire_fh
		Dim lobjValues As eFunctions.Values
		
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lTar_fire_fh = New eBranches.Tar_fire_fh
		lobjValues = New eFunctions.Values
		
		On Error GoTo insVal_MIN651_K_Err
		
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
		If nModulec = eRemoteDB.Constants.intNull Or nModulec = 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 12112,  , eFunctions.Errors.TextAlign.RigthAling)
		End If
		
		'+Validación del campo dEffectdate.
		If dEffecdate <> dtmNull Then
			If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nAction = 302 Or nAction = 301 Then
					If Not InsValEffecdate(nBranch, nProduct, nModulec, nCover, nCurrency, dEffecdate) Then
						Call lerrTime.ErrorMessage(sCodispl, 55611,  , eFunctions.Errors.TextAlign.RigthAling)
					End If
				End If
			End If
		Else
			Call lerrTime.ErrorMessage(sCodispl, 2056,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		insvalMIN651_K = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		'UPGRADE_NOTE: Object lTar_fire_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lTar_fire_fh = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insVal_MIN651_K_Err: 
		If Err.Number Then
			insvalMIN651_K = insvalMIN651_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'Funcion que valida em modulo este asociado a la cobertura
	Public Function InsValGenCover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lrecinsValGen_Cover As eRemoteDB.Execute
		On Error GoTo lrecinsValGen_Cover_Err
		
		lrecinsValGen_Cover = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insValGen_Cover
		'+
		With lrecinsValGen_Cover
			.StoredProcedure = "insValGen_Cover"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Es valido si no retorna filas
			'+
			InsValGenCover = Not .Run(True)
			.RCloseRec()
			
		End With
		
lrecinsValGen_Cover_Err: 
		If Err.Number Then
			InsValGenCover = False
		End If
		'UPGRADE_NOTE: Object lrecinsValGen_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValGen_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'InsValEffecdate: Funcion que valida la effecdate
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValeffecdate_tar_fire_fh As eRemoteDB.Execute
		On Error GoTo insValeffecdate_tar_fire_fh_Err
		
		lrecinsValeffecdate_tar_fire_fh = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insValeffecdate_tar_fire_fh
		'+
		With lrecinsValeffecdate_tar_fire_fh
			.StoredProcedure = "insValEffecdate_tar_fire_fh"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Es valido si no retorna registros mayores a la fecha
			'+
			InsValEffecdate = Not .Run(True)
			.RCloseRec()
			
		End With
		
insValeffecdate_tar_fire_fh_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecinsValeffecdate_tar_fire_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValeffecdate_tar_fire_fh = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMIN651: Ejecuta el post de la transacción Tabla Tar_fire_fh
	Public Function InsPostMIN651(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nConstcat As Integer, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nRate As Double, ByVal nPremium As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMIN651_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nConstcat = nConstcat
			.nCap_initial = IIf(nCap_initial = eRemoteDB.Constants.intNull, 0, nCap_initial)
			.nCap_end = IIf(nCap_end = eRemoteDB.Constants.intNull, 0, nCap_end)
			.nRate = IIf(nRate = eRemoteDB.Constants.intNull, 0, nRate)
			.nPremium = IIf(nPremium = eRemoteDB.Constants.intNull, 0, nPremium)
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMIN651 = Add
			Case "Update"
				InsPostMIN651 = Update
			Case "Del"
				InsPostMIN651 = Delete
		End Select
		
InsPostMIN651_Err: 
		If Err.Number Then
			InsPostMIN651 = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdSeries: Se encarga de actualizar la tabla Tar_fire_fh
	Private Function InsUpdTArFireFh(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTarFireFh As eRemoteDB.Execute
		On Error GoTo insUpdTarFireFh_Err
		
		lrecinsUpdTarFireFh = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdTarfirefh al 03-01-2002 17:17:23
		'+
		With lrecinsUpdTarFireFh
			.StoredProcedure = "insUpdtar_fire_fh"
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
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTArFireFh = .Run(False)
		End With
		
insUpdTarFireFh_Err: 
		If Err.Number Then
			InsUpdTArFireFh = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdTarFireFh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTarFireFh = Nothing
		On Error GoTo 0
	End Function
	
	'Función que valida los campos a actualizar en la Tabla Tar_fire_FH
	Public Function insValMIN651Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nConstcat As Integer, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nRate As Double, ByVal nPremium As Double) As String
		
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim nerrornum As Integer
		Dim lnAction As Integer
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		
		On Error GoTo insValMIN651Upd_Err
		
		
		'+ Validacion de los campos nRate y nPremium
		If (nRate = eRemoteDB.Constants.intNull And nPremium = eRemoteDB.Constants.intNull) Then
			Call lerrTime.ErrorMessage(sCodispl, 60208,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+ Validación del campo nCap_end
		If nCap_end > 0 And nCap_initial > 0 Then
			If nCap_end <= nCap_initial Then
				Call lerrTime.ErrorMessage(sCodispl, 10148,  , eFunctions.Errors.TextAlign.LeftAling)
			End If
		End If
		
		'+ Validación del campo nConstcat
		If (nConstcat = eRemoteDB.Constants.intNull Or nConstcat = 0) Then
			Call lerrTime.ErrorMessage(sCodispl, 55672,  , eFunctions.Errors.TextAlign.LeftAling)
		End If
		
		'+ Validacion el rango nCap_initial y nCap_end
		If sAction = "Update" Or sAction = "Upd" Then
			lnAction = 302
		Else
			If sAction = "Add" Then
				lnAction = 301
			End If
		End If
		nerrornum = InsValCapIinitialEnd(lnAction, nBranch, nProduct, nModulec, nCover, nCurrency, dEffecdate, nCap_initial, nCap_end, nConstcat)
		If nerrornum <> 0 Then
			Call lerrTime.ErrorMessage(sCodispl, nerrornum,  , eFunctions.Errors.TextAlign.LeftAling, "Monto asegurado:")
		End If
		
		insValMIN651Upd = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		
insValMIN651Upd_Err: 
		If Err.Number Then
			insValMIN651Upd = insValMIN651Upd & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'Funcion que valida el rango nCap_initial y nCap_end
	Public Function InsValCapIinitialEnd(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nCap_initial As Double, ByVal nCap_end As Double, ByVal nConstcat As Integer) As Integer
		Dim nerrornum As Integer
		Dim InsValCapIinitialEnd_tar_fire_fh As eRemoteDB.Execute
		On Error GoTo InsValCapIinitialEnd_tar_fire_fh_Err
		
		InsValCapIinitialEnd_tar_fire_fh = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insValeffecdate_tar_fire_fh
		'+
		With InsValCapIinitialEnd_tar_fire_fh
			.StoredProcedure = "InsValCap_Initial_End"
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
				.Add("nErrornum", nerrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+
			'+ Retorna el código de error a desplegar o cero no tiene errores
			'+
			.Run(False)
			InsValCapIinitialEnd = .Parameters("nErrornum").Value
		End With
		
InsValCapIinitialEnd_tar_fire_fh_Err: 
		If Err.Number Then
			InsValCapIinitialEnd = 0
		End If
		'UPGRADE_NOTE: Object InsValCapIinitialEnd_tar_fire_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		InsValCapIinitialEnd_tar_fire_fh = Nothing
		On Error GoTo 0
	End Function
	
	'Find: Funcion que busca el un registro por la PK de la tabla TAR_FIRE_FH
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nConstcat As Integer, ByVal nCap_initial As Double, ByVal nRate As Double, ByVal nPremium As Double) As String
		Dim lrecreaTar_fire_fh_all As eRemoteDB.Execute
		
		On Error GoTo reaTar_fire_fh_all_Err
		
		lrecreaTar_fire_fh_all = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaTar_fire_fh_all al 10-04-2002 10:20:49
		'+
		With lrecreaTar_fire_fh_all
			.StoredProcedure = "reaTar_fire_fh_all"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstcat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_initial", nCap_initial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = CStr(True)
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nModulec = .FieldToClass("nModulec")
				Me.nCover = .FieldToClass("nCover")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nConstcat = .FieldToClass("nConstcat")
				Me.nCap_initial = .FieldToClass("nCap_initial")
				Me.nCap_end = .FieldToClass("nCap_end")
				Me.nRate = .FieldToClass("nRate")
				Me.nPremium = .FieldToClass("nPremium")
				Me.nUsercode = .FieldToClass("nUsercode")
			Else
				Find = CStr(False)
			End If
		End With
		
reaTar_fire_fh_all_Err: 
		If Err.Number Then
			Find = CStr(False)
		End If
		
		'UPGRADE_NOTE: Object lrecreaTar_fire_fh_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_fire_fh_all = Nothing
		On Error GoTo 0
		
	End Function
End Class






