Option Strict Off
Option Explicit On
Public Class Tar_sef
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_sef.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla TAR_SEF tomada el 14/11/2001 08:47
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nRole As Integer ' NUMBER        22     5      0 No
	Public nCover As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nAge_init As Integer ' NUMBER        22     5      0 No
	Public nCapital_init As Double ' NUMBER        22    18      6 No
	Public nAge_end As Integer ' NUMBER        22     5      0 Yes
	Public nCapital_end As Double ' NUMBER        22    18      6 Yes
	Public nRate As Double ' NUMBER        22     9      6 Yes
	Public nType_tar As Integer ' NUMBER        22     5      0 Yes
	Public nTax As Double ' NUMBER        22     9      6 Yes
	Public nUsercode As Integer ' NUMBER        5
	
	'- Variables auxiliares
	Private mblnAge As Boolean
	Private mblnCapital As Boolean
	
	'% Delete: Elimina un registro de la tabla
	Public Function Delete() As Boolean
		Dim lrecdelTar_sef As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelTar_sef = New eRemoteDB.Execute
		
		With lrecdelTar_sef
			.StoredProcedure = "delTar_sef"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelTar_sef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_sef = Nothing
	End Function
	
	'% Find: Busca un registro dentro de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nAge_init As Integer, ByVal nCapital_init As Double) As Boolean
		Dim lrecreaTar_sef As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaTar_sef = New eRemoteDB.Execute
		
		With lrecreaTar_sef
			.StoredProcedure = "reaTar_sef"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				dEffecdate = .FieldToClass("dEffecdate")
				nRole = .FieldToClass("nRole")
				nCover = .FieldToClass("nCover")
				nModulec = .FieldToClass("nModulec")
				nAge_init = .FieldToClass("nAge_init")
				nCapital_init = .FieldToClass("nCapital_init")
				nAge_end = .FieldToClass("nAge_end")
				nCapital_end = .FieldToClass("nCapital_init")
				nRate = .FieldToClass("nRate")
				nType_tar = .FieldToClass("nType_tar")
				nTax = .FieldToClass("nTax")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTar_sef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_sef = Nothing
	End Function
	
	'% Update: Crea/Actualiza un registro dentro de la tabla
	Public Function Update() As Boolean
		Dim lrecupdTar_sef As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdTar_sef = New eRemoteDB.Execute
		
		With lrecupdTar_sef
			.StoredProcedure = "insupdTar_sef"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 22, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tar", nType_tar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTar_sef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_sef = Nothing
	End Function
	
	'% insvalMVI772_K: valida los campos del encabezado de la transacción
	Public Function insvalMVI772_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nCover As Integer, ByVal nModulec As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lblnValid As Boolean
		
		On Error GoTo InsValMVI772_Err
		
		lobjErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		
		lblnValid = True
		
		With lobjErrors
			'+ El ramo debe estar lleno
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
				lblnValid = False
			End If
			
			'+ El producto debe estar lleno
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1014)
				lblnValid = False
			End If
			
			If lblnValid Then
				If lclsProduct.FindProdMaster(nBranch, nProduct) Then
					If lclsProduct.sBrancht <> eProduct.Product.pmBrancht.pmlife And lclsProduct.sBrancht <> eProduct.Product.pmBrancht.pmMixed Then
						'+ El producto debe ser de vida o combinado
						Call .ErrorMessage(sCodispl, 3403)
					End If
				End If
			End If
			
			'+ La fecha de efecto debe estar llena
			If dEffecdate = dtmNull Then
				Call .ErrorMessage(sCodispl, 3404)
			End If
			
			'+ Si el producto es modular, el código del módulo debe estar lleno
			If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
				If nModulec = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 12165)
				End If
			End If
			
			'+ El código de la cobertura debe estar lleno
			If nCover = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11163)
			End If
			
			'+ El tipo de asegurado debe estar lleno
			If nRole = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10241)
			End If
			
			insvalMVI772_K = .Confirm
		End With
		
InsValMVI772_Err: 
		If Err.Number Then
			insvalMVI772_K = "insvalMVI772: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insvalMVI772Upd: Valida los campos de la zona masiva
	Public Function insvalMVI772Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer, ByVal nCapital_init As Double, ByVal nCapital_end As Double) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lblnValid As Boolean
		
		On Error GoTo InsValMVI772_Err
		
		lobjErrors = New eFunctions.Errors
		
		lblnValid = True
		
		With lobjErrors
			If nAge_init = eRemoteDB.Constants.intNull Then
				'+ La edad inicial debe estar llena
				Call .ErrorMessage(sCodispl, 11109)
				lblnValid = False
			End If
			
			If nAge_end = eRemoteDB.Constants.intNull Then
				'+ La edad final debe estar llena
				Call .ErrorMessage(sCodispl, 11110)
			Else
				'+ La edad final debe ser mayor a la edad inicial
				If lblnValid Then
					If nAge_end < nAge_init Then
						Call .ErrorMessage(sCodispl, 11036)
					End If
				End If
			End If
			
			lblnValid = True
			
			If nCapital_init = eRemoteDB.Constants.intNull Then
				'+ El capital inicial debe estar lleno
				Call .ErrorMessage(sCodispl, 11111)
				lblnValid = False
			End If
			
			If nCapital_end = eRemoteDB.Constants.intNull Then
				'+ El capital final debe estar lleno
				Call lobjErrors.ErrorMessage(sCodispl, 11112)
			Else
				'+ El capital final debe ser mayor al capital inicial
				If lblnValid Then
					If nCapital_end < nCapital_init Then
						Call .ErrorMessage(sCodispl, 10148)
					End If
				End If
			End If
			
			'+ Se verifica que la edad y el capital no se encuentre en otro rango dentro de la tabla
			If sAction = "Add" Then
				If valTar_sef_range(nBranch, nProduct, dEffecdate, nModulec, nCover, nRole, nAge_init, nAge_end, nCapital_init, nCapital_end) Then
					If mblnAge Then
						Call .ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad:")
					End If
					If mblnCapital Then
						Call .ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Capital:")
					End If
				End If
			End If
			
			insvalMVI772Upd = lobjErrors.Confirm
		End With
		
InsValMVI772_Err: 
		If Err.Number Then
			insvalMVI772Upd = "InsValMVI772: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insPostMVI772: Esta función se encarga de crear/actualizar/eliminar los registros
	'%               correspondientes en la tabla
	Public Function insPostMVI772(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nAge_init As Integer, ByVal nCapital_init As Double, ByVal nAge_end As Integer, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nType_tar As Integer, ByVal nTax As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMVI772_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nRole = nRole
			.nCover = nCover
			.nModulec = nModulec
			.nAge_init = nAge_init
			.nCapital_init = nCapital_init
			.nAge_end = nAge_end
			.nCapital_end = nCapital_end
			.nRate = nRate
			.nType_tar = nType_tar
			.nTax = nTax
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add", "Update"
				insPostMVI772 = Update
				
			Case "Del"
				insPostMVI772 = Delete
		End Select
		
insPostMVI772_err: 
		If Err.Number Then
			insPostMVI772 = False
		End If
		On Error GoTo 0
	End Function
	
	'% valTar_sef_range: verifica la existencia de la edad y del capital dentro de otro rango.
	Private Function valTar_sef_range(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer, ByVal nCapital_init As Double, ByVal nCapital_end As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo valTar_sef_range_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valTar_sef_range"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists_age", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists_capital", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nExists_age").Value = 1 Then
					mblnAge = True
				End If
				If .Parameters("nExists_capital").Value = 1 Then
					mblnCapital = True
				End If
				valTar_sef_range = True
			End If
		End With
		
valTar_sef_range_err: 
		If Err.Number Then
			valTar_sef_range = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






