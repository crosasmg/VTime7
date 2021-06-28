Option Strict Off
Option Explicit On
Public Class Au_bon_mod
	'%-------------------------------------------------------%'
	'% $Workfile:: Au_bon_mod.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	'+ Definición de la tabla AU_BON_MOD tomada el 23/03/2002 17:02
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nCurrency As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nInimonth As Integer ' NUMBER        22     5      0 No
	Public nId As Integer ' NUMBER        22    10      0 No
	Public nEndmonth As Integer ' NUMBER        22     5      0 No
	Public dNulldate As Date ' DATE           7              Yes
	Public nAmount_claim As Double ' NUMBER        22    10      2 Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public nExist As Integer
	Public lintExist As Integer
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAu_bon_mod(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdAu_bon_mod(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAu_bon_mod(3)
	End Function
	
	'%InsValAu_bon_mod: Lee los datos de la tabla
	Public Function InsValAu_bon_mod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nInimonth As Integer, ByVal nEndmonth As Integer, ByVal nExist As Double) As Boolean
		Dim lrecreaAu_bon_mod_v As eRemoteDB.Execute
		' Dim nExist As long
		
		On Error GoTo reaAu_bon_mod_v_Err
		
		lrecreaAu_bon_mod_v = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaAu_bon_mod_val 23-03-2002 17:14:26
		'+
		With lrecreaAu_bon_mod_v
			.StoredProcedure = "reaAu_bon_mod_v"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nInimonth", nInimonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nEndmonth", nEndmonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			If .Run(False) Then
				InsValAu_bon_mod = True
				lintExist = .Parameters("nExist").Value
			End If
		End With
		
reaAu_bon_mod_v_Err: 
		If Err.Number Then
			InsValAu_bon_mod = False
		End If
		'UPGRADE_NOTE: Object lrecreaAu_bon_mod_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAu_bon_mod_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMAU587_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Descuento por siniestralidad según módulo
	Public Function insValMAU587_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer) As String
		'- Se definen los objetos para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim lobjProduct As eProduct.Product
		Dim lblnError As Boolean
		
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		On Error GoTo insValMAU587_k_Err
		lblnError = False
		
		'+ Validación del ramo
		With lobjErrors
			If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 1022)
			End If
		End With
		
		'+ Validación del producto
		With lobjErrors
			If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 11009)
			End If
		End With
		
		'+ Validación del producto
		If Not lblnError Then
			lobjProduct = New eProduct.Product
			With lobjProduct
				If Not .FindProdMaster(nBranch, nProduct) Then
					lblnError = True
					Call lobjErrors.ErrorMessage(sCodispl, 9066)
				End If
			End With
		End If
		
		'+ Validación de fecha
		With lobjErrors
			If dEffecdate = dtmNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 11198)
			End If
		End With
		
		'+ Validacion de fecha de actualización
		If Not lblnError Then
			If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If Find_Date_Greater(nBranch, nProduct, dEffecdate, nCurrency) Then
					Call lobjErrors.ErrorMessage(sCodispl, 55611)
				End If
			End If
		End If
		
		'+ Validación de la moneda
		With lobjErrors
			If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10107)
			End If
		End With
		insValMAU587_k = lobjErrors.Confirm
		
insValMAU587_k_Err: 
		If Err.Number Then
			insValMAU587_k = "insValMAU587_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProduct = Nothing
	End Function
	
	'%Find_Date_Greater Valida la fecha de efecto de la transacción
	Public Function Find_Date_Greater(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer) As Boolean
		Dim lrecreaAu_bon_mod As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaAu_bon_mod_v_Err
		lrecreaAu_bon_mod = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure ReaAu_bon_mod_date al 23-03-2002 10:31:00
		'+
		With lrecreaAu_bon_mod
			.StoredProcedure = "ReaAu_bon_mod_date"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Find_Date_Greater = .Parameters("nExist").Value = 1
			Else
				Find_Date_Greater = False
			End If
		End With
		
reaAu_bon_mod_v_Err: 
		If Err.Number Then
			Find_Date_Greater = False
		End If
		'UPGRADE_NOTE: Object lrecreaAu_bon_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAu_bon_mod = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMAU587: Esta función se encarga de validar los datos del Form
	'%Tarifa de automóvil
	Public Function insValMAU587(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nInimonth As Integer, ByVal nEndmonth As Integer, ByVal nAmount_claim As Double) As String
		
		'- Se define el objeto para el manejo de las clases
		
		Dim lobjErrors As eFunctions.Errors
		Dim lobjTab_Moduls As eProduct.Tab_moduls
		Dim lblnError As Boolean
		
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		lobjTab_Moduls = New eProduct.Tab_moduls
		
		On Error GoTo insValMAU587_Err
		lblnError = False
		
		'+ Validación del módulo
		With lobjErrors
			If lobjTab_Moduls.Find(nBranch, nProduct, dEffecdate) Then
				If nModulec = eRemoteDB.Constants.intNull Or nModulec = 0 Then
					lblnError = True
					Call .ErrorMessage(sCodispl, 12112)
				End If
			End If
		End With
		
		If nModulec = eRemoteDB.Constants.intNull Then
			nModulec = 0
		End If
		
		'+ Validación de tiempo final
		With lobjErrors
			If nEndmonth <> 0 And nEndmonth <> eRemoteDB.Constants.intNull Then
				If nEndmonth < nInimonth Then
					lblnError = True
					Call .ErrorMessage(sCodispl, 55671)
				End If
			End If
		End With
		
		'+ Validación de Tiempo o Costo siniestro
		With lobjErrors
			'If nModulec <> 0 And nModulec <> eRemoteDB.Constants.intNull Then
				
				If nInimonth <= 0 Then
					Call .ErrorMessage(sCodispl, 55953)
				End If
				
				If nEndmonth <= 0 Then
					Call .ErrorMessage(sCodispl, 55954)
				End If
				
				If nAmount_claim <= 0 Then
					Call .ErrorMessage(sCodispl, 55955)
				End If
				
			'End If
		End With
		
		'+ Validación de duplicidad Ramo/Producto/Fecha Efecto/Moneda/Modulo/Tiempo Inicial/Tiempo Final
		'+ y,  el rango está comprendido dentro de otro ya registrado para el mismo módulo
		With lobjErrors
			If sAction = "Add" Then
				If Not lblnError Then
					nExist = 0
					Call InsValAu_bon_mod(nBranch, nProduct, dEffecdate, nCurrency, nModulec, nInimonth, nEndmonth, lintExist)
					If lintExist = 1 Then
						Call .ErrorMessage(sCodispl, 11137)
					Else
						If lintExist = 2 Then
							Call .ErrorMessage(sCodispl, 10185)
						End If
					End If
				End If
			End If
		End With
		
		insValMAU587 = lobjErrors.Confirm
		
		
insValMAU587_Err: 
		If Err.Number Then
			insValMAU587 = "insValMAU587: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjTab_Moduls may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTab_Moduls = Nothing
	End Function
	
	'%InsPostMAU587Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MAU587)
	Public Function InsPostMAU587Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nId As Integer, ByVal nInimonth As Integer, ByVal nEndmonth As Integer, ByVal nAmount_claim As Double, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InsPostMAU587Upd_Err
		
		If nModulec = eRemoteDB.Constants.intNull Then
			nModulec = 0
		End If
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nCurrency = nCurrency
			.nModulec = nModulec
			.nId = nId
			.nInimonth = nInimonth
			.nEndmonth = nEndmonth
			.nAmount_claim = nAmount_claim
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
					InsPostMAU587Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMAU587Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMAU587Upd = .Delete
					
			End Select
		End With
		
InsPostMAU587Upd_Err: 
		If Err.Number Then
			InsPostMAU587Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdAu_bon_mod: Realiza la actualización de la tabla
	Private Function InsUpdAu_bon_mod(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdAu_bon_mod As eRemoteDB.Execute
		
		On Error GoTo InsUpdAu_bon_mod_Err
		
		lrecInsUpdAu_bon_mod = New eRemoteDB.Execute
		
		
		'+ Definición de parámetros para stored procedure 'InsUpdAu_bon_mod'
		'+ Información leída el 23/03/2002
		With lrecInsUpdAu_bon_mod
			.StoredProcedure = "InsUpdAu_bon_mod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInimonth", nInimonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndmonth", nEndmonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_claim", nAmount_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdAu_bon_mod = .Run(False)
		End With
		
InsUpdAu_bon_mod_Err: 
		If Err.Number Then
			InsUpdAu_bon_mod = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdAu_bon_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdAu_bon_mod = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nCurrency = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nInimonth = eRemoteDB.Constants.intNull
		nEndmonth = eRemoteDB.Constants.intNull
		nAmount_claim = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






