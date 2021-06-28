Option Strict Off
Option Explicit On
Option Compare Text
Public Class Tar_ActLife
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_ActLife.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla Tar_ActLife al 11-07-2001 11:49:12
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public sTypetab As String ' CHAR       1    0     0    N
	Public sSmoking As String ' CHAR       1    0     0    N
	Public nAge As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nRatewomen As Double ' NUMBER     22   6     9    S
	Public nPremwomen As Double ' NUMBER     22   2     10   S
	Public nRatemen As Double ' NUMBER     22   6     9    S
	Public nPremmen As Double ' NUMBER     22   2     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	'%InsUpdTar_ActLife: Se encarga de actualizar la tabla Tar_ActLife
	Private Function InsUpdTar_ActLife(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdtar_actlife As eRemoteDB.Execute
		
		On Error GoTo insUpdtar_actlife_Err
		
		lrecinsUpdtar_actlife = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdtar_actlife al 11-07-2001 11:56:23
		'+
		With lrecinsUpdtar_actlife
			.StoredProcedure = "insUpdtar_actlife"
			With .Parameters
				.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sTypetab", sTypetab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			InsUpdTar_ActLife = .Run(False)
			
		End With
		
insUpdtar_actlife_Err: 
		If Err.Number Then
			InsUpdTar_ActLife = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdtar_actlife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdtar_actlife = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_ActLife(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_ActLife(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_ActLife(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal nAge As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTar_actlife As eRemoteDB.Execute
		
		On Error GoTo reaTar_actlife_Err
		
		Find = False
		
		lrecreaTar_actlife = New eRemoteDB.Execute
		
		sSmoking = IIf(sSmoking = "1", "1", "2")
		'+
		'+ Definición de store procedure reaTar_actlife al 11-07-2001 11:51:36
		'+
		With lrecreaTar_actlife
			.StoredProcedure = "reaTar_actlife"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sTypetab", sTypetab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(True) Then
				
				Find = True
				
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nModulec = nModulec
				Me.nCover = nCover
				Me.nAge = nAge
				Me.sSmoking = IIf(sSmoking = "1", "1", "2")
				Me.sTypetab = sTypetab
				Me.dEffecdate = dEffecdate
				Me.nRatewomen = .FieldToClass("nRatewomen")
				Me.nPremwomen = .FieldToClass("nPremwomen")
				Me.nRatemen = .FieldToClass("nRatemen")
				Me.nPremmen = .FieldToClass("nPremmen")
				Me.nUsercode = .FieldToClass("nUsercode")
				.RCloseRec()
			End If
		End With
reaTar_actlife_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTar_actlife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_actlife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreaTar_ActLife_age As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdateErr
		
		
		lrecreaTar_ActLife_age = New eRemoteDB.Execute
		
		sSmoking = IIf(sSmoking = "1", "1", "2")
		
		'+
		'+ Definición de store procedure reaTar_actlife_age al 11-07-2001 11:59:27
		'+
		With lrecreaTar_ActLife_age
			.StoredProcedure = "insValEffecdate_Tar_ActLife"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sTypetab", sTypetab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			InsValEffecdate = Not .Run(True)
			.RCloseRec()
		End With
		
InsValEffecdateErr: 
		If Err.Number Then
			InsValEffecdate = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecreaTar_ActLife_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_ActLife_age = Nothing
	End Function
	
	'**%Funcion insValCover: This function validates a cover existence
	'%Funcion insValCover. Esta funcion valida la existencia de una cobertura
	'% asociada al producto
	'%---------------------------------------------------
	Private Function insValCover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		'%---------------------------------------------------
		On Error GoTo insValCoverErr
		
		Dim lclsLife_Cover As Object 'eProduct.Life_cover
		
		lclsLife_Cover = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Life_cover")
		
		insValCover = lclsLife_Cover.Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		
insValCoverErr: 
		If Err.Number Then
			insValCover = False
		End If
		'UPGRADE_NOTE: Object lclsLife_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'**%Funcion insValModule: This function validates a module existence
	'**% for as given product
	'**%the data for a ActiveLife commiss related with a plan a intermediary
	'%Funcion insValModule. Esta funcion valida la existencia de un
	'% modulo asociado a un producto
	'%---------------------------------------------------'
	Private Function insValModule(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		'%---------------------------------------------------
		Dim lclsTab_modul As eProduct.Tab_modul
		
		On Error GoTo insValModule_Err
		
		lclsTab_modul = New eProduct.Tab_modul
		
		insValModule = lclsTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate)
		
insValModule_Err: 
		If Err.Number Then
			insValModule = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modul = Nothing
	End Function
	
	'%InsValMVA606_K: Validaciones de la transacción(Header)
	Public Function InsValMVA606_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA606_K_Err
		lclsErrors = New eFunctions.Errors
		
		sSmoking = IIf(sSmoking = "1", "1", "2")
		
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
			End If
			
			If nModulec = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1901)
			ElseIf nModulec <> 0 Then 
				If Not insValModule(nBranch, nProduct, nModulec, dEffecdate) Then
					.ErrorMessage(sCodispl, 55566)
				End If
			End If
			
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			Else
				If Not insValCover(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
					.ErrorMessage(sCodispl, 11165)
				End If
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, nProduct, nModulec, nCover, sTypetab, sSmoking, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			InsValMVA606_K = .Confirm
		End With
		
InsValMVA606_K_Err: 
		If Err.Number Then
			InsValMVA606_K = "InsValMVA606_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA606: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MVA606)
	Public Function InsValMVA606(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal nAge As Integer, ByVal dEffecdate As Date, Optional ByVal nRatewomen As Double = 0, Optional ByVal nPremwomen As Double = 0, Optional ByVal nRatemen As Double = 0, Optional ByVal nPremmen As Double = 0, Optional ByVal nUsercode As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTar_ActLife As Tar_ActLife
		
		On Error GoTo InsValMVA606_Err
		lclsErrors = New eFunctions.Errors
		
		sSmoking = IIf(sSmoking = "1", "1", "2")
		
		With lclsErrors
			'+Validar que no se dupliquen registros
			If (nRatewomen = eRemoteDB.Constants.intNull And nPremwomen = eRemoteDB.Constants.intNull And nRatemen = eRemoteDB.Constants.intNull And nPremmen = eRemoteDB.Constants.intNull) Then
				
				.ErrorMessage(sCodispl, 60208)
			End If
			
			If nAge <> eRemoteDB.Constants.intNull Then
				If sAction = "Add" Then
					lclsTar_ActLife = New Tar_ActLife
					If lclsTar_ActLife.Find(nBranch, nProduct, nModulec, nCover, sTypetab, sSmoking, nAge, dEffecdate) Then
						.ErrorMessage(sCodispl, 60209)
					End If
					'UPGRADE_NOTE: Object lclsTar_ActLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTar_ActLife = Nothing
				End If
			End If
			
			InsValMVA606 = .Confirm
			
		End With
		
InsValMVA606_Err: 
		If Err.Number Then
			InsValMVA606 = "InsValMVA606: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA606: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MVA606)
	Public Function InsPostMVA606(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal nAge As Integer, ByVal dEffecdate As Date, Optional ByVal nRatewomen As Double = 0, Optional ByVal nPremwomen As Double = 0, Optional ByVal nRatemen As Double = 0, Optional ByVal nPremmen As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo InsPostMVA606_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.sTypetab = sTypetab
			.sSmoking = IIf(sSmoking = "1", "1", "2")
			.nAge = nAge
			.dEffecdate = dEffecdate
			.nRatewomen = nRatewomen
			.nPremwomen = nPremwomen
			.nRatemen = nRatemen
			.nPremmen = nPremmen
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVA606 = Add
			Case "Update"
				InsPostMVA606 = Update
			Case "Del"
				InsPostMVA606 = Delete
		End Select
		
InsPostMVA606_Err: 
		If Err.Number Then
			InsPostMVA606 = False
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
		sTypetab = "1"
		sSmoking = "2"
		nAge = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRatewomen = eRemoteDB.Constants.intNull
		nPremwomen = eRemoteDB.Constants.intNull
		nRatemen = eRemoteDB.Constants.intNull
		nPremmen = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






