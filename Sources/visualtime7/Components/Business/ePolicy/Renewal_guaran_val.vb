Option Strict Off
Option Explicit On
Public Class Renewal_guaran_val
	
	'+
	'+ Estructura de tabla Renewal_guaran_val al 09-26-2008 18:30:09
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nGuarsav_year As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public dIniperiod As Date ' DATE       7    0     0    N
	Public dEndperiod As Date ' DATE       7    0     0    N
	Public nCurrentamount As Double ' NUMBER     22   6     18   N
	Public nNewamount As Double ' NUMBER     22   6     18   N
	Public nCurrentprem As Double ' NUMBER     22   6     18   N
	Public nNewprem As Double ' NUMBER     22   6     18   N
	Public sTypepaid As String ' CHAR       1    0     0    N
	Public sProcess As String ' CHAR       1    0     0    S
	Public sTypepaidDes As String
	
	'%InsUpdRenewal_guaran_val: Se encarga de actualizar la tabla Renewal_guaran_val
	Private Function InsUpdRenewal_guaran_val(ByVal nAction As Short) As Boolean
		Dim lrecinsUpdRenewal_guaran_val As eRemoteDB.Execute
		On Error GoTo insUpdRenewal_guaran_val_Err
		lrecinsUpdRenewal_guaran_val = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdRenewal_guaran_val al 09-26-2008 18:31:56
		'+
		With lrecinsUpdRenewal_guaran_val
			.StoredProcedure = "InsRenewal_guaran_valpkg.InsUpdRenewal_guaran_val"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIniperiod", dIniperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndperiod", dEndperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrentamount", nCurrentamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewamount", nNewamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrentprem", nCurrentprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewprem", nNewprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypepaid", sTypepaid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdRenewal_guaran_val = .Run(False)
		End With
		
insUpdRenewal_guaran_val_Err: 
		If Err.Number Then
			InsUpdRenewal_guaran_val = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdRenewal_guaran_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdRenewal_guaran_val = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdRenewal_guaran_val(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdRenewal_guaran_val(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdRenewal_guaran_val(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGuarsav_year As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecRenewal_guaran_valo As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecRenewal_guaran_valo = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure Renewal_guaran_valo al 09-26-2008 18:48:08
		'+
		With lrecRenewal_guaran_valo
			.StoredProcedure = "InsRenewal_guaran_valpkg.ReaRenewal_guaran_val_o"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Find = True
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nGuarsav_year = .FieldToClass("nGuarsav_year")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.dIniperiod = .FieldToClass("dIniperiod")
				Me.dEndperiod = .FieldToClass("dEndperiod")
				Me.nCurrentamount = .FieldToClass("nCurrentamount")
				Me.nNewamount = .FieldToClass("nNewamount")
				Me.nCurrentprem = .FieldToClass("nCurrentprem")
				Me.nNewprem = .FieldToClass("nNewprem")
				Me.sTypepaid = .FieldToClass("sTypepaid")
				Me.sTypepaidDes = .FieldToClass("sTypepaidDes")
				Me.sProcess = .FieldToClass("sProcess")
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecRenewal_guaran_valo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRenewal_guaran_valo = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalDeffecdate As eRemoteDB.Execute
		Dim ldMaxEffecdate As Date
		On Error GoTo InsValEffecdate_Err
		
		lrecvalDeffecdate = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valDeffecdate al 09-26-2008 19:52:32
		'+
		With lrecvalDeffecdate
			.StoredProcedure = "InsRenewal_guaran_valpkg.valdEffecdate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldMaxEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				ldMaxEffecdate = .FieldToClass("dEffecdate")
				InsValEffecdate = ldMaxEffecdate = eRemoteDB.Constants.dtmNull Or ldMaxEffecdate <= dEffecdate
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecvalDeffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDeffecdate = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValMVI8017_K: Validaciones de la transacción(Header)
	Public Function InsValMVI8017_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lintValPolicy As Integer
		Dim lblnValDate As Boolean
		
		On Error GoTo InsValMVI8017_K_Err
		lclsErrors = New eFunctions.Errors
		lblnValDate = True
		
		With lclsErrors
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
				lblnValDate = False
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
				lblnValDate = False
			End If
			
			'+ Se valida el Campo Póliza
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3003)
				lblnValDate = False
			Else
				lclsPolicy = New ePolicy.Policy
				lintValPolicy = lclsPolicy.insValPolicy(nBranch, nProduct, nPolicy, "1")
				If lintValPolicy = -1 Then
					.ErrorMessage(sCodispl, 3001)
					lblnValDate = False
				ElseIf lintValPolicy > 0 Then 
					.ErrorMessage(sCodispl, 60261)
					lblnValDate = False
				End If
			End If
			
			'+ Se valida el Campo Certificado
			If nCertif = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3006)
				lblnValDate = False
			Else
				If lintValPolicy = 0 Then
					lclsCertificat = New ePolicy.Certificat
					If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						'+ Si el certificado no está registrado.
						.ErrorMessage(sCodispl, 3010)
						lblnValDate = False
					Else
						'+ Se válida que el certificado sea válido
						If dEffecdate <> eRemoteDB.Constants.dtmNull Then
							If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Or lclsCertificat.dNulldate <> eRemoteDB.Constants.dtmNull Or (lclsCertificat.dStartdate > dEffecdate Or (lclsCertificat.dExpirdat <> eRemoteDB.Constants.dtmNull And lclsCertificat.dExpirdat < dEffecdate)) Then
								
								.ErrorMessage(sCodispl, 55890)
								lblnValDate = False
							End If
						End If
					End If
				End If
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 2056)
			Else
				If lblnValDate And nAction = eFunctions.Menues.TypeActions.clngActionInput Then
					If Not InsValEffecdate(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
						.ErrorMessage(sCodispl, 1943)
					End If
				End If
			End If
			
			InsValMVI8017_K = .Confirm
		End With
		
InsValMVI8017_K_Err: 
		If Err.Number Then
			InsValMVI8017_K = "InsValMVI8017_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVI8017: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MVI8017)
	Public Function InsValMVI8017(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGuarsav_year As Integer, ByVal dEffecdate As Date, ByVal dIniperiod As Date, ByVal dEndperiod As Date, ByVal nCurrentamount As Double, ByVal nNewamount As Double, ByVal nNewprem As Double, ByVal chkFunds As String, ByVal chkReceipt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI8017_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Validar que no se dupliquen registros
			If sAction = "Add" Then
				If nGuarsav_year = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 55200)
				Else
					If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGuarsav_year, dEffecdate) Then
						.ErrorMessage(sCodispl, 10284)
					End If
				End If
			End If
			
			'+La fecha inicial debe estar llena
			If dIniperiod = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 80070)
			End If
			
			'+La fecha final debe estar llena
			If dEndperiod = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 80071)
			Else
				'+La fecha final debe ser mayor o igual a la fecha inicial
				If dIniperiod <> eRemoteDB.Constants.dtmNull And dEndperiod < dIniperiod Then
					.ErrorMessage(sCodispl, 80072)
				End If
			End If
			
			'+El nuevo valor debe estar lleno
			If nNewamount = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80073)
			End If
			
			'+Si la diferencia entre los valores es negativo, se debe indicar una forma de pago
			If nCurrentamount - nNewprem < 0 Then
				If chkFunds & chkReceipt = String.Empty Then
					.ErrorMessage(sCodispl, 80074)
				End If
			End If
			
			InsValMVI8017 = .Confirm
		End With
		
InsValMVI8017_Err: 
		If Err.Number Then
			InsValMVI8017 = "InsValMVI8017: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVI8017: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MVI8017)
	Public Function InsPostMVI8017(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGuarsav_year As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal dIniperiod As Date = #12:00:00 AM#, Optional ByVal dEndperiod As Date = #12:00:00 AM#, Optional ByVal nCurrentamount As Double = 0, Optional ByVal nNewamount As Double = 0, Optional ByVal nCurrentprem As Double = 0, Optional ByVal nNewprem As Double = 0, Optional ByVal sFunds As String = "", Optional ByVal sReceipt As String = "") As Boolean
		
		On Error GoTo InsPostMVI8017_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nGuarsav_year = nGuarsav_year
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			.dIniperiod = dIniperiod
			.dEndperiod = dEndperiod
			.nCurrentamount = nCurrentamount
			.nNewamount = nNewamount
			.nCurrentprem = nCurrentprem
			.nNewprem = nNewprem
			.sTypepaid = sReceipt & sFunds
			.sProcess = "2"
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI8017 = Add
			Case "Update"
				InsPostMVI8017 = Update
			Case "Del"
				InsPostMVI8017 = Delete
		End Select
		
InsPostMVI8017_Err: 
		If Err.Number Then
			InsPostMVI8017 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nGuarsav_year = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		dIniperiod = eRemoteDB.Constants.dtmNull
		dEndperiod = eRemoteDB.Constants.dtmNull
		nCurrentamount = eRemoteDB.Constants.intNull
		nNewamount = eRemoteDB.Constants.intNull
		nCurrentprem = eRemoteDB.Constants.intNull
		nNewprem = eRemoteDB.Constants.intNull
		sTypepaid = String.Empty
		sProcess = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






