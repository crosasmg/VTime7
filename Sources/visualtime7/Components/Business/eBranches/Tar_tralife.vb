Option Strict Off
Option Explicit On
Public Class Tar_tralife
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_tralife.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 20/10/03 13.35                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on October 16,2001.
	'*-Propiedades según la tabla en el sistema el 16/10/2001
	
	'Column_name               Type                        Nulleable
	'-----------------------   ------------------------    ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nModulec As Integer 'Number(5)       No
	Public nCover As Integer 'Number(5)       No
	Public sSmoking As String 'Char(1)         No
	Public nAge As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nInipercov As Integer 'Number(5)       No
	Public nInipaycov As Integer 'Number(5)       No
	Public nRatewomen As Double 'Number(9, 6)    Yes
	Public nPremwomen As Double 'Number(10, 2)   Yes
	Public nRatemen As Double 'Number(9, 6)    Yes
	Public nPremmen As Double 'Number(10, 2)   Yes
	Public dNulldate As Date 'Date            Yes
	Public dCompdate As Date 'Date            No
	Public nUsercode As Integer 'Number(5)       Yes
	Public nType_tar As Integer 'Number(5)       No
	Public nEndpercov As Integer 'Number(5)       Yes
	Public nEndpaycov As Integer 'Number(5)       Yes
	Public nTyperisk As Integer
	
	Private mvarTar_tralifes As Tar_tralifes
	
	'% Get Tar_tralifes: toma el objeto de la clase
	
	'% Set Tar_tralifes: setea el objeto de la clase
	Public Property Tar_tralifes() As Tar_tralifes
		Get
			If mvarTar_tralifes Is Nothing Then
				mvarTar_tralifes = New Tar_tralifes
			End If
			
			Tar_tralifes = mvarTar_tralifes
		End Get
		Set(ByVal Value As Tar_tralifes)
			mvarTar_tralifes = Value
		End Set
	End Property
	
	'% Class_Terminate: se controla el cierre de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTar_tralifes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTar_tralifes = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% insUpdTar_tralife: Se crean/actualizan/eliminan los datos de la tabla
	Private Function InsUpdTar_tralife(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTar_tralife As eRemoteDB.Execute
		
		On Error GoTo InsUpdTar_tralife_Err
		
		lrecInsUpdTar_tralife = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'InsUpdTar_tralife'
		'**+Information read on October 16,2001 11:58:10 a.m.
		'+Definición de parámetros para stored procedure 'InsUpdTar_tralife'
		'+Información leída el 16/10/2001 11:58:10 AM
		With lrecInsUpdTar_tralife
			.StoredProcedure = "InsUpdTar_tralife"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipercov", nInipercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipaycov", nInipaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tar", nType_tar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpercov", nEndpercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpaycov", nEndpaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTar_tralife = .Run(False)
		End With
		
InsUpdTar_tralife_Err: 
		If Err.Number Then
			InsUpdTar_tralife = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTar_tralife = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_tralife(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_tralife(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_tralife(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sSmoking As String, ByVal nAge As Integer, ByVal nInipercov As Integer, ByVal nInipaycov As Integer, ByVal dEffecdate As Date, ByVal nTyperisk As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaTar_tralife As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.nAge <> nAge Or Me.nInipercov <> nInipercov Or Me.nInipaycov <> nInipaycov Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lrecReaTar_tralife = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nModulec = nModulec
			Me.nCover = nCover
			Me.nAge = nAge
			Me.nInipercov = nInipercov
			Me.nInipaycov = nInipaycov
			Me.dEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'ReaTar_tralife_by_age'
			With lrecReaTar_tralife
				.StoredProcedure = "ReaTar_tralife_by_age"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInipercov", nInipercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInipaycov", nInipaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nModulec = nModulec
					Me.nCover = nCover
					Me.sSmoking = .FieldToClass("sSmoking")
					Me.nInipercov = .FieldToClass("nInipercov")
					Me.nInipaycov = .FieldToClass("nInipaycov")
					Me.nAge = .FieldToClass("nAge")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nRatewomen = .FieldToClass("nRatewomen")
					Me.nPremwomen = .FieldToClass("nPremwomen")
					Me.nRatemen = .FieldToClass("nRatemen")
					Me.nPremmen = .FieldToClass("nPremmen")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nType_tar = .FieldToClass("nType_tar")
					Me.nEndpercov = .FieldToClass("nEndpercov")
					Me.nEndpaycov = .FieldToClass("nEndpaycov")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_tralife = Nothing
		On Error GoTo 0
	End Function
	
	'% insValEffecdate: verifica que la fecha sea posterior a la última actualización a la tabla
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sSmoking As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaTar_tralife As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaTar_tralife = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'InsValEffecdate_Tar_tralife'
		With lrecReaTar_tralife
			.StoredProcedure = "InsValEffecdate_Tar_tralife"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_tralife = Nothing
		On Error GoTo 0
	End Function
	
	'% insvalMVI729_K: se realizan las validaciones de la ventana
	Public Function insvalMVI729_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sSmoking As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		sSmoking = IIf(sSmoking = String.Empty, "2", sSmoking)
		
		On Error GoTo InsValMVI729_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		
		With lclsErrors
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
			End If
			
			'+ Se valida el Campo Modulo
			If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
				If nModulec = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 12112)
				End If
			End If
			
			'+ Se valida el Campo Cobertura
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				'+ Debe ser posterior a la última modificación
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, nProduct, nModulec, nCover, sSmoking, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			insvalMVI729_K = .Confirm
		End With
		
InsValMVI729_K_Err: 
		If Err.Number Then
			insvalMVI729_K = "InsValMVI729_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insvalMVI729: se realizan las validaciones de la ventana
	Public Function insvalMVI729(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sSmoking As String, ByVal nAge As Integer, ByVal nInipercov As Integer, ByVal nInipaycov As Integer, ByVal nRatewomen As Double, ByVal nPremwomen As Double, ByVal nRatemen As Double, ByVal nPremmen As Double, ByVal nTyperisk As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI729_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Edad
			If nAge = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Edad:")
			Else
				If sAction = "Add" Then
					sSmoking = IIf(sSmoking = String.Empty, "2", sSmoking)
					If Find(nBranch, nProduct, nModulec, nCover, sSmoking, nAge, nInipercov, nInipaycov, dEffecdate, nTyperisk) Then
						.ErrorMessage(sCodispl, 55610)
					End If
				End If
			End If
			
			If nRatewomen = eRemoteDB.Constants.intNull And nPremwomen = eRemoteDB.Constants.intNull And nRatemen = eRemoteDB.Constants.intNull And nPremmen = eRemoteDB.Constants.intNull Then
				'+ Debe indicar información en tasa (Hombres/Mujeres) o monto fijo (Hombres/Mujeres)
				.ErrorMessage(sCodispl, 55877)
			End If
			
			insvalMVI729 = .Confirm
		End With
		
InsValMVI729_Err: 
		If Err.Number Then
			insvalMVI729 = "InsValMVI729: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostMVI729: se actualizan los campos de la página
	Public Function InsPostMVI729(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sSmoking As String, ByVal nAge As Integer, ByVal nInipercov As Integer, ByVal nInipaycov As Integer, ByVal dEffecdate As Date, ByVal nRatewomen As Double, ByVal nPremwomen As Double, ByVal nRatemen As Double, ByVal nPremmen As Double, ByVal nUsercode As Integer, ByVal nType_tar As Integer, ByVal nEndpercov As Integer, ByVal nEndpaycov As Integer, ByVal nTyperisk As Integer) As Boolean
		On Error GoTo InsPostMVI729_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nCover = nCover
			.sSmoking = IIf(sSmoking = String.Empty, 2, sSmoking)
			.nAge = nAge
			.nInipercov = IIf(nInipercov = eRemoteDB.Constants.intNull, 1, nInipercov)
			.nInipaycov = IIf(nInipaycov = eRemoteDB.Constants.intNull, 1, nInipaycov)
			.dEffecdate = dEffecdate
			.nRatewomen = nRatewomen
			.nPremwomen = nPremwomen
			.nRatemen = nRatemen
			.nPremmen = nPremmen
			.nUsercode = nUsercode
			.nType_tar = nType_tar
			.nEndpercov = nEndpercov
			.nEndpaycov = nEndpaycov
			.nTyperisk = nTyperisk
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI729 = Add
			Case "Update"
				InsPostMVI729 = Update
			Case "Del"
				InsPostMVI729 = Delete
		End Select
		
InsPostMVI729_Err: 
		If Err.Number Then
			InsPostMVI729 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		sSmoking = strNull
		nAge = eRemoteDB.Constants.intNull
		nInipercov = eRemoteDB.Constants.intNull
		nInipaycov = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRatewomen = eRemoteDB.Constants.intNull
		nPremwomen = eRemoteDB.Constants.intNull
		nRatemen = eRemoteDB.Constants.intNull
		nPremmen = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nType_tar = eRemoteDB.Constants.intNull
		nEndpercov = eRemoteDB.Constants.intNull
		nEndpaycov = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






