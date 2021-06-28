Option Strict Off
Option Explicit On
Public Class Capital_age
	'%-------------------------------------------------------%'
	'% $Workfile:: Capital_age.cls                          $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 29/08/08 12:35p                              $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla Capital_age
	'-        Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nRole As Integer ' NUMBER     22   0     5    N
	Public nAge_init As Integer ' NUMBER     22   0     5    N
	Public nAge_end As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nCapmini As Double ' NUMBER     22   0     12   N
	Public nCapmaxim As Double ' NUMBER     22   0     12   N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'- Se definen las propiedades utilizadas en la ventana
	'- DP8003 - Capital por edad actuarial
	
	
	'- Se define las constantes que contienen los máximos y minimos valores para las
	'- edades y capitales.
	
	Const MaxE As Integer = 130
	Const MinE As Integer = 0
	
	'%Add: Permite registrar la información de los Capitales por edad actuarial
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_end As Integer, ByVal nAge_init As Integer, ByVal nCapmaxim As Double, ByVal nCapmini As Double, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecCreCapital_age As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecCreCapital_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creCapital_age'
		
		With lrecCreCapital_age
			.StoredProcedure = "creCapital_age"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmini", nCapmini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmaxim", nCapmaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreCapital_age = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCreCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreCapital_age = Nothing
	End Function
	
	'%Update: Permite actualizar la información de los Capitales por edad actuarial.
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_end As Integer, ByVal nAge_init As Integer, ByVal nCapmaxim As Double, ByVal nCapmini As Double, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecUpdCapital_age As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdCapital_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updCapital_age'
		
		With lrecUpdCapital_age
			.StoredProcedure = "updCapital_age"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmini", nCapmini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmaxim", nCapmaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdCapital_age = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdCapital_age = Nothing
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_end As Integer, ByVal nAge_init As Integer, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecDeCapital_age As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecDeCapital_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delMortality'
		
		With lrecDeCapital_age
			.StoredProcedure = "delCapital_age"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDeCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDeCapital_age = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDeCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDeCapital_age = Nothing
	End Function
	
	'% insValDP8003: Realiza la validación de los campos puntuales de la página DP8003 - Capitales por edad actuarial.
	Public Function insValDP8003(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_end As Integer, ByVal nAge_init As Integer, ByVal nCapmaxim As Double, ByVal nCapmini As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sAction As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lObjValField As eFunctions.valField
		
		lobjErrors = New eFunctions.Errors
		lObjValField = New eFunctions.valField
		
		insValDP8003 = String.Empty
		
		On Error GoTo insValDP8003_Err
		
		'+ Se realizan las validaciones del campo "Edad inicial".
		If sAction = "Add" Then
			If nAge_init = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 11109)
			Else
				If nAge_init < MinE Or nAge_init > MaxE Then
					Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad inicial: 0-130 años)")
				Else
					If nAge_end <> eRemoteDB.Constants.intNull Then
						If insValRange_age(nAge_init, nBranch, nProduct, dEffecdate, nModulec, nCover, nRole) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial:")
						End If
					End If
				End If
			End If
			
			'+ Se realizan las validaciones del campo "Edad Final".
			
			If nAge_end = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 11110)
			Else
				If nAge_end < MinE Or nAge_end > MaxE Then
					Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad final: 0-130 años)")
				Else
					If nAge_init <> eRemoteDB.Constants.intNull Then
						If (nAge_end < nAge_init) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11036)
						Else
							If insValRange_age(nAge_end, nBranch, nProduct, dEffecdate, nModulec, nCover, nRole) Then
								Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad final:")
							End If
						End If
					End If
				End If
			End If
		End If
		'+ Se realizan las validaciones del campo "Capital inicial".
		
		If nCapmini = 0 Or nCapmini = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 60157)
		End If
		
		'+ Se realizan las validaciones del campo "Capital final".
		
		If nCapmaxim = 0 Or nCapmaxim = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 6116)
		Else
			If nCapmini <> eRemoteDB.Constants.intNull And nCapmini <> 0 Then
				If nCapmaxim < nCapmini Then
					Call lobjErrors.ErrorMessage(sCodispl, 11113)
				End If
			End If
		End If
		
		insValDP8003 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValDP8003_Err: 
		If Err.Number Then
			insValDP8003 = insValDP8003 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValRange_age: Esta rutina es la encargada de evitar las intercepciones entre los rangos.
	Private Function insValRange_age(ByVal nAge As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecCapital_age As eRemoteDB.Execute
		
		On Error GoTo insValRange_age_Err
		lrecCapital_age = New eRemoteDB.Execute
		With lrecCapital_age
			.StoredProcedure = "insValRange_age"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If (.Run) Then
				.RCloseRec()
				insValRange_age = True
			End If
		End With
		
insValRange_age_Err: 
		If Err.Number Then
			insValRange_age = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCapital_age = Nothing
	End Function
	
	'% insPostDP8003: Esta función se encarga de almacenar los datos en las tablas, en este caso Capital_age
	'% ventana DP8003 - Capitales por edad actuarial
	
	Public Function insPostDP8003(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_end As Integer, ByVal nAge_init As Integer, ByVal nCapmaxim As Double, ByVal nCapmini As Double, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		
		On Error GoTo insPostDP8003_err
		
		'insPostDP8003 = True
		
		'nBranch = nBranch
		'nProduct = nProduct
		'dEffecdate = dEffecdate
		'nAge_end = nAge_end
		'nAge_init = nAge_init
		'nCapmaxim = nCapmaxim
		'nCapmini = nCapmini
		'nUsercode = nUsercode
		'nModulec = nModulec
		'nCover = nCover
		'nRole = nRole
		
		Select Case sAction
			
			'+ Si la opción seleccionada es Registrar.
			
			Case "Add"
				insPostDP8003 = Add(nBranch, nProduct, dEffecdate, nAge_end, nAge_init, nCapmaxim, nCapmini, nUsercode, nModulec, nCover, nRole)
				
				'+ Si la opción seleccionada es Modificar.
				
			Case "Update"
				insPostDP8003 = Update(nBranch, nProduct, dEffecdate, nAge_end, nAge_init, nCapmaxim, nCapmini, nUsercode, nModulec, nCover, nRole)
				
				'+ Si la opción seleccionada es Eliminar.
			Case "Del"
				insPostDP8003 = Delete(nBranch, nProduct, dEffecdate, nAge_end, nAge_init, nUsercode, nModulec, nCover, nRole)
		End Select
		
insPostDP8003_err: 
		If Err.Number Then
			insPostDP8003 = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValCapital: Esta rutina es la encargada de validar la suma asegurada o capital inicial.
	Public Function insValCapital(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal nTransaction As Integer) As Boolean
		Dim lrecCapital_age As eRemoteDB.Execute
		
		On Error GoTo insValCapital_Err
		
		lrecCapital_age = New eRemoteDB.Execute
		
		With lrecCapital_age
			.StoredProcedure = "insValVi7001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmini", nCapmini, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmaxim", nCapmaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValCapital = True
				Me.nCapmini = .Parameters("nCapmini").Value
				Me.nCapmaxim = .Parameters("nCapmaxim").Value
			End If
		End With
		
insValCapital_Err: 
		If Err.Number Then
			insValCapital = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCapital_age = Nothing
	End Function
End Class






