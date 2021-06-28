Option Strict Off
Option Explicit On
Public Class Tar_auto
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_auto.cls                             $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 29/09/03 12.36                               $%'
	'% $Revision:: 35                                       $%'
	'%-------------------------------------------------------%'
	'+ Definición de la tabla Tar_auto tomada el 05/03/2002 19:16
	
	'+ Column_Name                             Type      Length  Prec  Scale  Nullable
	' ----------------- ---------------      --------  --------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0     No
	Public nProduct As Integer ' NUMBER        22     5      0     No
	Public nCurrency As Integer ' NUMBER        22     5      0     No
	Public nModulec As Integer ' NUMBER        22     5      0     No
	Public nCover As Integer ' NUMBER        22     5      0     No
	Public dEffecdate As Date ' DATE           7                  No
	Public nId As Double ' NUMBER        10     5      0     No
	Public dNulldate As Date ' DATE           7                  Yes
	Public sVehcode As String ' VARCHAR2       6                  Yes
	Public nRate As Double ' NUMBER        22     5      2     Yes
	Public nPrem_fix As Double ' NUMBER        22    10      2     Yes
	Public nUsercode As Integer ' NUMBER        22     5      0     No
	Public dCompdate As Date ' DATE           7                  No
	Public lintExist As Integer
	Public sDesc_modulec As String
	Public sDesc_cover As String
	
	'% Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_auto(1)
	End Function
	
	'% Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_auto(2)
	End Function
	
	'% Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_auto(3)
	End Function
	
	'% InsValTar_auto: Lee los datos de la tabla
	Public Function InsValTar_auto(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sVehcode As String, ByVal nExist As Integer) As Boolean
		Dim lrecreaTar_auto_v As eRemoteDB.Execute
		
		On Error GoTo reaTar_auto_v_Err
		
		lrecreaTar_auto_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaTar_auto_val 03-01-2002 17:14:26
		With lrecreaTar_auto_v
			.StoredProcedure = "reaTar_auto_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValTar_auto = True
				lintExist = .Parameters("nExist").Value
			End If
		End With
		
reaTar_auto_v_Err: 
		If Err.Number Then
			InsValTar_auto = False
		End If
		'UPGRADE_NOTE: Object lrecreaTar_auto_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_auto_v = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insValMAU571_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Tarifa de automóvil
	Public Function insValMAU571_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal sVehcode As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lobjProduct As eProduct.Product
		Dim lblnError As Boolean
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lobjProduct = New eProduct.Product
		
		On Error GoTo insValMAU571_k_Err
		lblnError = False
		
		'+ Validación del ramo
		With lobjErrors
			If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Validación del producto
			If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 11009)
			End If
			
			'+ Validación de que el ramo-producto sea del ramo "Auto"
			If nBranch > 0 And nProduct > 0 Then
				If lobjProduct.FindProdMaster(nBranch, nProduct) Then
					If CStr(lobjProduct.sBrancht) <> "3" Then
						lblnError = True
						Call .ErrorMessage(sCodispl, 55980)
					End If
				End If
			End If
			'+ Validación de fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 11198)
			End If
			
			'+ Validacion de fecha de actualización
			If Not lblnError Then
				If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					If Find_Date_Greater(nBranch, nProduct, dEffecdate, sVehcode) Then
						Call .ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			'+ Validación de la moneda
			If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10107)
			End If
		End With
		
		insValMAU571_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProduct = Nothing
		
insValMAU571_k_Err: 
		If Err.Number Then
			insValMAU571_k = "insValMAU571_k: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% Find_Date_Greater Valida la fecha de efecto de la transacción
	Public Function Find_Date_Greater(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sVehcode As String) As Boolean
		Dim lrecreaTar_auto As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaTar_auto_v_Err
		
		lrecreaTar_auto = New eRemoteDB.Execute
		
		'+ Definición de store procedure ReaTar_auto_date al 08-03-2002 10:31:00
		With lrecreaTar_auto
			.StoredProcedure = "ReaTar_auto_date"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Find_Date_Greater = .Parameters("nExist").Value = 1
			Else
				Find_Date_Greater = False
			End If
		End With
		
reaTar_auto_v_Err: 
		If Err.Number Then
			Find_Date_Greater = False
		End If
		'UPGRADE_NOTE: Object lrecreaTar_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_auto = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insValMAU571: Esta función se encarga de validar los datos del Form
	'% Tarifa de automóvil
	Public Function insValMAU571(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sVehcode As String, ByVal nRate As Double, ByVal nPrem_fix As Double) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lobjGen_cover As eProduct.Gen_cover
		Dim lobjTab_Moduls As eProduct.Tab_moduls
		Dim lblnError As Boolean
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lobjGen_cover = New eProduct.Gen_cover
		lobjTab_Moduls = New eProduct.Tab_moduls
		
		On Error GoTo insValMAU571_Err
		lblnError = False
		
		'+ Validación del módulo
		With lobjErrors
			If lobjTab_Moduls.Find(nBranch, nProduct, dEffecdate) Then
				If nModulec = eRemoteDB.Constants.intNull Or nModulec = 0 Then
					lblnError = True
					Call .ErrorMessage(sCodispl, 12112)
				End If
			End If
			
			If nModulec = eRemoteDB.Constants.intNull Then
				nModulec = 0
			End If
			
			If nCover = eRemoteDB.Constants.intNull Then
				nCover = 0
			End If
			
			'+ Validación de la tasa y prima
			If nRate = eRemoteDB.Constants.intNull And nPrem_fix = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 60208)
			End If
			
			'+ Validación de duplicidad Ramo/Producto/Fecha Efecto/modulo/cobertura/código de vehículo
			If sAction = "Add" Then
				If Not lblnError Then
					lintExist = 0
					Call InsValTar_auto(nBranch, nProduct, nCurrency, nModulec, nCover, dEffecdate, sVehcode, lintExist)
					If lintExist = 1 Then
						Call .ErrorMessage(sCodispl, 8307)
					End If
				End If
			End If
		End With
		
		
		insValMAU571 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lobjGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGen_cover = Nothing
		'UPGRADE_NOTE: Object lobjTab_Moduls may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTab_Moduls = Nothing
		
insValMAU571_Err: 
		If Err.Number Then
			insValMAU571 = "insValMAU571: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% InsPostMAU571Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                   de la transacción (MAU571)
	Public Function InsPostMAU571Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal sOptTyp_var As String, ByVal nRateAddSub As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nId As Double, ByVal sVehcode As String, ByVal nRate As Double, ByVal nPrem_fix As Double, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		Dim lobjValues As eFunctions.Values
		
		On Error GoTo InsPostMAU571Upd_Err
		lobjValues = New eFunctions.Values
		
		If nCover = eRemoteDB.Constants.intNull Then
			nCover = 0
		End If
		
		If nRateAddSub <> eRemoteDB.Constants.intNull And nRateAddSub <> 0 Then
			If sOptTyp_var = "1" Then
				nRate = nRate + (nRate * nRateAddSub / 100)
				nPrem_fix = nPrem_fix + (nPrem_fix * nRateAddSub / 100)
			Else
				nRate = nRate - (nRate * nRateAddSub / 100)
				nPrem_fix = nPrem_fix - (nPrem_fix * nRateAddSub / 100)
			End If
		End If
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nCurrency = nCurrency
            .nModulec = IIf(nModulec = -32768, 0, nModulec)
            .nCover = nCover
			.dEffecdate = dEffecdate
			.nId = nId
			.sVehcode = sVehcode
			.nRate = nRate
			.nPrem_fix = nPrem_fix
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = 3
			Else
				If sAction = "Update" Then
					lintAction = 2
				Else
					If sAction = "Add" Then
						lintAction = 1
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostMAU571Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMAU571Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMAU571Upd = .Delete
					
			End Select
		End With
		
InsPostMAU571Upd_Err: 
		If Err.Number Then
			InsPostMAU571Upd = False
		End If
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		On Error GoTo 0
	End Function
	
	'% InsUpdTar_auto: Realiza la actualización de la tabla
	Private Function InsUpdTar_auto(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTar_auto As eRemoteDB.Execute
		
		On Error GoTo InsUpdTar_auto_Err
		
		lrecInsUpdTar_auto = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdTar_auto'
		'+ Información leída el 23/01/02
		With lrecInsUpdTar_auto
			.StoredProcedure = "InsUpdTar_auto"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehCode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_fix", nPrem_fix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTar_auto = .Run(False)
		End With
		
InsUpdTar_auto_Err: 
		If Err.Number Then
			InsUpdTar_auto = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTar_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTar_auto = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insPostDuplicateMAU571: Realiza los procesos de duplicación de datos
	'% de la tabla Tar_auto de la transacción MAU571
	Public Function insPostDuplicateMAU571(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal sVehcode As String, ByVal nUsercode As Integer, ByVal nBranch_ori As Integer, ByVal nProduct_ori As Integer, ByVal nCurrency_ori As Integer, ByVal dEffecdate_ori As Date, ByVal sVehcode_ori As String, ByVal nId_ori As Double) As Boolean
		Dim lrecinsduptar_auto As eRemoteDB.Execute
		
		On Error GoTo insduptar_auto_Err
		
		lrecinsduptar_auto = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insduptar_auto'
		'+Información leída el 07/10/2002
		With lrecinsduptar_auto
			.StoredProcedure = "insduptar_auto"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_ori", nBranch_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ori", nProduct_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency_ori", nCurrency_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_ori", dEffecdate_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode_ori", sVehcode_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_ori", nId_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostDuplicateMAU571 = .Run(False)
		End With
		
insduptar_auto_Err: 
		If Err.Number Then
			insPostDuplicateMAU571 = False
		End If
		'UPGRADE_NOTE: Object lrecinsduptar_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsduptar_auto = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nId = eRemoteDB.Constants.intNull
		sVehcode = String.Empty
		nRate = eRemoteDB.Constants.intNull
		nPrem_fix = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






