Option Strict Off
Option Explicit On
Public Class Account_Pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Account_Pol.cls                          $%'
	'% $Author:: Nvaplat2                                   $%'
	'% $Date:: 30/04/04 6:02p                               $%'
	'% $Revision:: 29                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla insudb.account_pol al 11-06-2001 12:07:30
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nValuePol As Double ' NUMBER     22   2     10   S
	Public nAmosurren As Double ' NUMBER     22   2     10   S
	Public nFixcharge As Double ' NUMBER     22   2     10   S
	Public nCovercost As Double ' NUMBER     22   2     10   S
	Public nProfit As Double ' NUMBER     22   2     10   S
	Public nNetpays As Double ' NUMBER     22   2     10   S
	Public nPays As Double ' NUMBER     22   2     10   S
	Public dLastdate As Date ' DATE       7    0     0    S
	Public dVp_neg As Date ' DATE       7    0     0    S
	Public dLastpay As Date ' DATE       7    0     0    S
	Public nNextmonth As Integer ' NUMBER     22   0     5    S
	Public nNextyear As Integer ' NUMBER     22   0     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'-Variables auxiliares
	'-Variables para guardar el cliente contratante de la póliza
	Public sClient As String
	Public sCliename As String
	
	'-Objeto para obtener los movimiento de la cuenta corriente
	Public mcolMove_Accpols As Move_Accpols
	
	Private mstrKey As String
	
	'% Find: Busca un registro de cuenta cte de poliza segun su llave
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaAccount_pol As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaAccount_pol = New eRemoteDB.Execute
		
		With lrecreaAccount_pol
			.StoredProcedure = "reaAccount_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nValuePol = .FieldToClass("nValuepol")
				Me.nAmosurren = .FieldToClass("nAmosurren")
				Me.nFixcharge = .FieldToClass("nFixcharge")
				Me.nCovercost = .FieldToClass("nCovercost")
				Me.nProfit = .FieldToClass("nProfit")
				Me.nNetpays = .FieldToClass("nNetpays")
				Me.nPays = .FieldToClass("nPays")
				Me.dLastdate = .FieldToClass("dLastdate")
				Me.dVp_neg = .FieldToClass("dVp_neg")
				Me.dLastpay = .FieldToClass("dLastpay")
				Me.nNextmonth = .FieldToClass("nNextmonth")
				Me.nNextyear = .FieldToClass("nNextyear")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaAccount_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAccount_pol = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVA650_K: Validaciones de la transacción VA650
	Public Function InsValVA650_K(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nMovType As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCertificat As Certificat
		Dim lblnError As Boolean
		
		On Error GoTo InsValVA650_K_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+ Se valida el campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
				lblnError = True
			End If
			
			'+ Se valida el campo producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
				lblnError = True
			End If
			
			'+ Se valida el campo póliza
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3003)
				lblnError = True
			End If
			
			'+ Se valida la fecha de efecto
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5055)
				lblnError = True
			End If
			
			'+ Se valida la información de la póliza/certificado
			If Not lblnError Then
				If nCertif = eRemoteDB.Constants.intNull Then
					nCertif = 0
				End If
				lclsCertificat = New Certificat
				'+ Si la póliza/certificado no esta registrada
				If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					.ErrorMessage(sCodispl, 1978)
				Else
					'+ Si la póliza/certificado no posee un estado válido
					If lclsCertificat.sStatusva = "2" Or lclsCertificat.sStatusva = "3" Then
						.ErrorMessage(sCodispl, 3882)
						
						'+ Si la póliza/certificado está anulada
					ElseIf lclsCertificat.sStatusva = "6" Then 
						.ErrorMessage(sCodispl, 3098)
						
					Else
						'+ Se valida la fecha de efecto
						If Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
							If nMovType = 1 Then
								If dEffecdate <= Me.dLastdate Then
									.ErrorMessage(sCodispl, 60292)
								End If
							Else
								If dEffecdate > Me.dLastdate Then
									.ErrorMessage(sCodispl, 60563)
								End If
							End If
						End If
					End If
				End If
			End If
			InsValVA650_K = .Confirm
		End With
		
InsValVA650_K_Err: 
		If Err.Number Then
			InsValVA650_K = "InsValVA650_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVA650: Validaciones de la transacción
	Public Function InsValVA650(ByVal sCodispl As String, ByVal nAmount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValVA650_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nAmount <= 0 Then
				.ErrorMessage(sCodispl, 60293)
			End If
			InsValVA650 = .Confirm
		End With
		
InsValVA650_Err: 
		If Err.Number Then
			InsValVA650 = "InsValVA650: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPreVA650: Realiza el llamado a las rutinas para recalcular el valor póliza o
	'%             para ingresar la prima de inyección
	Public Function InsPreVA650(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypemove As Integer, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsRoles As ePolicy.Roles
		
		On Error GoTo InsPreVA650_Err
		InsPreVA650 = True
		Call Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		lclsRoles = New ePolicy.Roles
		If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, Roles.eRoles.eRolContratanting, String.Empty, dEffecdate) Then
			sClient = lclsRoles.sClient
			sCliename = lclsRoles.sCliename
		End If
		
		If mcolMove_Accpols Is Nothing Then
			mcolMove_Accpols = New Move_Accpols
		End If
		
		Call mcolMove_Accpols.InsCalMoveVP(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTypemove, nUsercode, sKey)
InsPreVA650_Err: 
		If Err.Number Then
			InsPreVA650 = False
		End If
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostVA650Upd: Actualizaciones según especificaciones funcionales VA650
	Public Function InsPostVA650Upd(ByVal sKey As String, ByVal nAmount As Double, ByVal nCredit As Double) As Boolean
		Dim lclsMove_Accpol As Move_Accpol
		lclsMove_Accpol = New Move_Accpol
		With lclsMove_Accpol
			.sKey = sKey
			.nAmount = nAmount
			.nCredit = nCredit
			InsPostVA650Upd = .UpdateTmp
		End With
		'UPGRADE_NOTE: Object lclsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Accpol = Nothing
	End Function
	
	'%InsPostVA650: Realiza el llamado al procesoq que genera los movimiento de cuentas
	Public Function InsPostVA650(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypemove As Integer, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsMove_Accpol As Move_Accpol
		lclsMove_Accpol = New Move_Accpol
		InsPostVA650 = lclsMove_Accpol.InsUpdMoveVP(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTypemove, sKey, nUsercode)
		'UPGRADE_NOTE: Object lclsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Accpol = Nothing
	End Function
	
	'%InsCalVP: Llama al procedimiento que cálcula el VP
	Private Function InsCalVP(ByVal sOption As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nUsercode As Integer, ByVal sKey As String, ByVal sType As String) As Boolean
		Dim lrecInsCalVP As eRemoteDB.Execute
		
		On Error GoTo InsCalVP_Err
		lrecInsCalVP = New eRemoteDB.Execute
		
		With lrecInsCalVP
			.StoredProcedure = "InsCalVAL601"
			.Parameters.Add("sOption", sOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsCalVP = .Run(False)
		End With
		
InsCalVP_Err: 
		If Err.Number Then
			InsCalVP = False
		End If
		'UPGRADE_NOTE: Object lrecInsCalVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalVP = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValVAL601: Validaciones de la transacción
	Public Function InsValVAL601(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sOption As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal dVp_neg As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lclsCertificat As Certificat
		Dim lclsPolicy As Policy
		Dim lblnError As Boolean
		Dim llngMonths As Integer
		Dim ldtmEffecdate As Date
		
		On Error GoTo InsValVAL601_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			'+Si la opción de ejecución es masiva se válida los campos masivos
			If sOption = "1" Then
				'+Se valida el año y mes de ejecución
				If nYear = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60288)
				End If
				If nMonth = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60289)
				Else
					If nMonth < 1 Or nMonth > 12 Then
						.ErrorMessage(sCodispl, 60290)
					End If
				End If
			Else
				'+Si la opción de ejecución es puntual se válida los campos puntuales
				'+Se valida los campos claves de la póliza
				If nBranch = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 1022)
					lblnError = True
				End If
				If nProduct = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 1014)
					lblnError = True
				End If
				If nPolicy = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3003)
					lblnError = True
				End If
				If nCertif = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3006)
					lblnError = True
				End If
				If nPolicy <> eRemoteDB.Constants.intNull Then
					lclsPolicy = New Policy
					If Not lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
						.ErrorMessage(sCodispl, 3001)
						lblnError = True
					End If
				End If
				If nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then
					lclsCertificat = New Certificat
					If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						.ErrorMessage(sCodispl, 3010)
						lblnError = True
					End If
				End If
				
				'+Se valida que la cantidad de meses que el VP es negativo no se mayor a los meses
				'+permitidos según el producto
				If Not lblnError And dVp_neg <> eRemoteDB.Constants.dtmNull Then
					lclsProduct = New eProduct.Product
					ldtmEffecdate = DateSerial(nYear, nMonth, CInt("01"))
					If lclsProduct.FindProduct_li(nBranch, nProduct, ldtmEffecdate) Then
						llngMonths = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dVp_neg, ldtmEffecdate)
						If llngMonths > lclsProduct.nQmonToVPN Then
							.ErrorMessage(sCodispl, 60291)
						End If
					End If
				End If
			End If
			InsValVAL601 = .Confirm
		End With
		
InsValVAL601_Err: 
		If Err.Number Then
			InsValVAL601 = "InsValVAL601: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostVAL601: Ejecuta las actualizaciones según especificaciones funcionales
	Public Function InsPostVAL601(ByVal sOption As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nUsercode As Integer, ByVal nSessionId As String, ByVal sType As String) As Boolean
		
		mstrKey = "T" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode.ToString("00000")
		InsPostVAL601 = InsCalVP(sOption, nBranch, nProduct, nPolicy, nCertif, nMonth, nYear, nUsercode, mstrKey, sType)
	End Function
	
	'%sKey. Esta propiedad se encarga de devolver la llave de lectura del registro de la tabla
	'%      temporal
	'%      Se dejan parametros aunque no se ocupen para no perder compatibilidad.
	'%      A futuro cuando se procese como transaccion batch se eliminará
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String) As String
		Get
			
			sKey = mstrKey
			
		End Get
	End Property
	
	
	'%Class_Initialize: Se ejecuta cuando se instancia un objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nValuePol = eRemoteDB.Constants.intNull
		nAmosurren = eRemoteDB.Constants.intNull
		nFixcharge = eRemoteDB.Constants.intNull
		nCovercost = eRemoteDB.Constants.intNull
		nProfit = eRemoteDB.Constants.intNull
		nNetpays = eRemoteDB.Constants.intNull
		nPays = eRemoteDB.Constants.intNull
		dLastdate = eRemoteDB.Constants.dtmNull
		dVp_neg = eRemoteDB.Constants.dtmNull
		dLastpay = eRemoteDB.Constants.dtmNull
		nNextmonth = eRemoteDB.Constants.intNull
		nNextyear = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sClient = String.Empty
		sCliename = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Se ejecuta cuando se destruye un objeto de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolMove_Accpols may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolMove_Accpols = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






