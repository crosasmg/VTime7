Option Strict Off
Option Explicit On
Public Class Client_Trans
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_Trans.cls                         $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 5/11/03 18.11                                $%'
	'% $Revision:: 27                                       $%'
	'%-------------------------------------------------------%'
	
	'- Se declara varible para almacenar el código del cliente
	Private mclsClient As eClient.Client
	
	'% insValFolderBC005: Se realizan las validaciones de la zona puntual de la página
	Public Function insValFolderBC005(ByVal sCodispl As String, ByVal nOptAct As Integer, ByVal sNewCode As String, ByVal sDigit As String, ByVal sClientGrid As String, ByVal sClientHead As String) As String
		Dim lobjErrors As Object
		Dim lclsClient As Object
		lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		Dim lblnMessage As Boolean
		
		lblnMessage = True
		
		'+ Validación del campo: código de cliente.
		If sNewCode = String.Empty Or sDigit = String.Empty Then
			lobjErrors.ErrorMessage(sCodispl, 2001)
		Else
			sNewCode = lclsClient.ExpandCode(UCase(sNewCode))
			'+ Validación del campo código de cliente: debe estar registrado.
			If Not lclsClient.Find(sNewCode) Then
				lobjErrors.ErrorMessage(sCodispl, 1007)
				lblnMessage = False
			Else
				'+ Validación del campo código de cliente: no debe estar previamente indicado.
				If sNewCode = sClientGrid Then
					lobjErrors.ErrorMessage(sCodispl, 60438)
					lblnMessage = False
					'+ Validación del campo código de cliente: El cliente a sustituir no debe ser igual al cliente que reemplazara el codigo anterior
				ElseIf sNewCode = sClientHead Then 
					lobjErrors.ErrorMessage(sCodispl, 60437)
					lblnMessage = False
				End If
			End If
			'+ Si el cliente en la grilla tiene información en FinanceCO
			If nOptAct <> 1 Then
				If Find(sNewCode) Then
					lobjErrors.ErrorMessage(sCodispl, 56162)
				End If
			End If
			
			'+ Advertencia de confirmación de cambio y/o unificación de rut
			
			If lblnMessage Then
				If nOptAct = 1 Then '+ Cambio de rut
					lobjErrors.ErrorMessage(sCodispl, 55854)
				Else '+ Unificación de rut
					lobjErrors.ErrorMessage(sCodispl, 55937)
				End If
			End If
		End If
		insValFolderBC005 = lobjErrors.Confirm
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Finance_co" buscada por sClient
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lobjFinanceCO As eRemoteDB.Execute
		Dim lintExists As Integer
		On Error GoTo Find_Err
		lobjFinanceCO = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFinance_co_Client'
		
		With lobjFinanceCO
			.StoredProcedure = "reaFinance_co_Client"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjFinanceCO = Nothing
	End Function
	
	'% insValHeaderBC005: Se realizan las validaciones del encabezado de la página
	Public Function insValHeaderBC005(ByVal sCodispl As String, ByVal nOptAct As Integer, ByVal sClient As String, ByVal sDigit As String) As String
		Dim lobjErrors As Object
		Dim lclsClient As Object
		
		lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		On Error GoTo insValHeaderBC005_Err
		
		'+ Validación del campo: código de cliente.
		If sClient = String.Empty Or sDigit = String.Empty Then
			lobjErrors.ErrorMessage(sCodispl, 2001)
		Else
			sClient = lclsClient.ExpandCode(UCase(sClient))
			'+ Validación del campo código de cliente: debe estar registrado.
			If Not lclsClient.Find(sClient) Then
				If nOptAct = 2 Then
					lobjErrors.ErrorMessage(sCodispl, 1007)
				End If
			Else
				If nOptAct = 1 Then
					lobjErrors.ErrorMessage(sCodispl, 2020)
				End If
			End If
		End If
		
		insValHeaderBC005 = lobjErrors.Confirm
		
insValHeaderBC005_Err: 
		If Err.Number Then insValHeaderBC005 = insValHeaderBC005 & Err.Description
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostFolderBC005: Efectúa la actualización del cambio de código de cliente en el resto de las tablas donde se encuentra dicho campo.
	Public Function insPostFolderBC005(ByVal sNewCode As String, ByVal sDigit As String, ByVal sClient As String, ByVal nOptAct As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsTab_modcli As Object
		Dim lclsClient As Client
		
		lclsClient = New Client
		lclsTab_modcli = eRemoteDB.NetHelper.CreateClassInstance("eClient.Tab_modCli")
		
		On Error GoTo insPostFolderBC005_Err
		
		insPostFolderBC005 = True
		
		'+Si la accion es cambio de codigo de cliente se debe crear el codigo nuevo.
		
		If nOptAct = 1 Then
			If Not lclsClient.Find(sNewCode) Then
				If lclsClient.Find(sClient) Then
					lclsClient.sClient = sNewCode
					lclsClient.sDigit = sDigit
					If lclsClient.AddClient Then
						'+ Se comenta, se espera un cambio desde Caracas
						insPostFolderBC005 = insCreClient(sNewCode, sClient, nUsercode)
						insPostFolderBC005 = True
					End If
				End If
			End If
		End If
		
		'+ Se efectua la búsqueda de la información a procesar
		If insPostFolderBC005 Then
			If lclsTab_modcli.insTabMod_cli(sNewCode, sClient, nUsercode) Then
				insPostFolderBC005 = insUpdAddressKey(sClient, sNewCode, nUsercode)
			Else
				insPostFolderBC005 = False
			End If
		End If
		'+ Eliminación del cliente
		If insPostFolderBC005 Then
			With lclsClient
				.sClient = sClient
				insPostFolderBC005 = .Delete
			End With
		End If
		
insPostFolderBC005_Err: 
		If Err.Number Then
			insPostFolderBC005 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_modcli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modcli = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
	End Function
	
	'% insUpdAddressKey: Actualiza la clave de las tablas Address y Phones con el nuevo código de cliente.
	Private Function insUpdAddressKey(ByVal sClient As String, ByVal sNewCode As String, Optional ByVal nUsercode As Integer = 0) As Boolean
		Dim lclsPhone As Object
		
		'- Utilizada como contador.
		Dim lintCount As Integer
		
		'- Contiene el valor del titular de la dirección en nuestro caso 2 "Cliente".
		Dim lintRecowner As Integer
		
		'- Contiene el valor de la clave del registro con nuevo valor.
		Dim lstrKeyAddressNew As String
		
		'- Contiene el valor de la clave del registro con el valor viejo.
		Dim lstrKeyAddressOld As String
		Dim lclsAddress As Object
		
		lclsAddress = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Address")
		lclsPhone = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Phone")
		
		On Error GoTo insUpdAddressKey_Err
		
		insUpdAddressKey = True
		lintRecowner = 2
		
		For lintCount = 1 To 2
			lstrKeyAddressOld = CStr(lintCount) & sClient
			
			If lclsAddress.Find(lstrKeyAddressOld, lintRecowner, Today) Then
				lstrKeyAddressNew = CStr(lintCount) & sNewCode
				
				lclsAddress.nRecowner = lintRecowner
				lclsAddress.sKeyAddress = lstrKeyAddressNew
				lclsAddress.nUsercode = nUsercode
				
				insUpdAddressKey = lclsAddress.Add
				
				Call lclsPhone.UpdPhonesKey(lintRecowner, lstrKeyAddressNew, lstrKeyAddressOld, nUsercode)
				
				lclsAddress.nRecowner = lintRecowner
				lclsAddress.sKeyAddress = lstrKeyAddressOld
				
				insUpdAddressKey = lclsAddress.Delete
			End If
		Next lintCount
		
insUpdAddressKey_Err: 
		If Err.Number Then
			insUpdAddressKey = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPhone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPhone = Nothing
		'UPGRADE_NOTE: Object lclsAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAddress = Nothing
	End Function
	
	'% insCreClient: Actualiza la clave de las tablas Address y Phones con el nuevo código de cliente.
	Public Function insCreClient(ByRef sNewCode As String, ByRef sOldClient As String, ByRef nUsercode As Integer) As Boolean
		Dim lrecCreClient_code As eRemoteDB.Execute
		
		insCreClient = True
		If sNewCode <> sOldClient Then
			lrecCreClient_code = New eRemoteDB.Execute
			With lrecCreClient_code
				.StoredProcedure = "insCreClient"
				.Parameters.Add("sOldClient", Trim(sOldClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sNewClient", Trim(sNewCode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				insCreClient = .Run(False)
			End With
			'UPGRADE_NOTE: Object lrecCreClient_code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecCreClient_code = Nothing
		End If
	End Function
End Class






