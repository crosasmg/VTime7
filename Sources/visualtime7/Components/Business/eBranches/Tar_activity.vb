Option Strict Off
Option Explicit On
Public Class Tar_activity
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_activity.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 17/12/2001
	'+ Los campos llave corresponden a nBranch, nProduct, nSpeciality, nCover y dEffecdate
	
	'+ Name                   'Type                        Nullable
	'+ ---------------------- ---------------------------- --------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nSpeciality As Integer 'Number(10)      No
	Public nCover As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nPercent As Double 'Number(5, 2)    Yes
	Public nAmount As Double 'Number(10, 2)   Yes
	Public nTyperec As Integer 'Number(5)       No
	Public dNulldate As Date 'Date            Yes
	Public nUsercode As Integer 'Number(5)       No
	
	'% insvalMVI630_K: se realizan las validaciones asociadas a las tarifas por actividad
	Public Function insValMVI630_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModule As Integer, ByVal nCover As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lblnValid As Boolean
		Dim lvalField As eFunctions.valField
		Dim lclsBranches As eProduct.Branches
		
		On Error GoTo insValMVI630_K_err
		
		lobjErrors = New eFunctions.Errors
		lvalField = New eFunctions.valField
		
		lvalField.objErr = lobjErrors
		lblnValid = True
		With lobjErrors
			'+ El campo Ramo debe estar lleno
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI630", 1022)
				lblnValid = False
			Else
				'+ sBrancht del ramo debe ser Vida o combinado
				lclsBranches = New eProduct.Branches
				If Not lclsBranches.insVerifyBranch(nBranch, "('1','5')") Then
					Call .ErrorMessage("MVI630", 3987)
					lblnValid = False
				End If
				
				'+ El campo Producto debe estar lleno
				If nProduct = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("MVI630", 1014)
					lblnValid = False
				End If
				
				lvalField.ErrEmpty = 4003
				
				If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.ValAll) Then
					If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
						If InsValEffecdate(nBranch, nProduct, nCover) Then
							'+ Si la acción es Actualizar la fecha de efecto debe ser mayor o igual a la fecha de última
							'+ modificación
							If dEffecdate < Me.dEffecdate Then
								Call .ErrorMessage("MVI630", 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & Me.dEffecdate & ")")
								lblnValid = False
							End If
						End If
					End If
				Else
					lblnValid = False
				End If
				
				'+ El campo Módulo debe estar lleno si el producto es modular
				If nModule = eRemoteDB.Constants.intNull And lblnValid Then
					lclsProduct = New eProduct.Product
					If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
						Call .ErrorMessage("MVI630", 12112)
					End If
				End If
				
				'+ El campo Cobertura debe estar lleno
				If nCover = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("MVI630", 11163)
				End If
			End If
			insValMVI630_K = .Confirm
		End With
		
insValMVI630_K_err: 
		If Err.Number Then
			insValMVI630_K = "insvalMVI630_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		'UPGRADE_NOTE: Object lclsBranches may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBranches = Nothing
	End Function
	
	'% insvalMVI630Upd: se realizan las validaciones asociadas a las tarifas por actividad
	Public Function insValMVI630Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSpeciality As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nTyperec As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo insvalMVI630Upd_err
		
		lobjErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		With lobjErrors
			'+ El campo código actividad, debe estar lleno
			If nSpeciality = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI630", 8002)
			Else
				If lclsValues.IsValid("Table16", CStr(nSpeciality)) Then
					'+ No debe estar repetido en la tabla
					If sAction = "Add" Then
						If Find(nBranch, nProduct, nSpeciality, nCover, dEffecdate) Then
							Call .ErrorMessage("MVI630", 55891)
						End If
					End If
				Else
					'+ Debe existir en la tabla de actividades
					Call .ErrorMessage("MVI630", 8035)
				End If
			End If
			
			'+ El campo % Recargo debe estar lleno
			If nPercent = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI630", 10144)
			End If
			
			'+ El campo Tipo debe estar lleno
			If nTyperec = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI630", 55663)
			End If
			
			insValMVI630Upd = .Confirm
		End With
		
insvalMVI630Upd_err: 
		If Err.Number Then
			insValMVI630Upd = "insvalMVI630Upd : " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	'% Add: se agrega un registro a la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro de la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: se realiza la actualización de la tabla
	Private Function Update(ByVal nAction As Integer) As Boolean
		Dim lrecTar_activity As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecTar_activity = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insUpdTar_activity'
		'Información leída el 19/12/2001
		With lrecTar_activity
			.StoredProcedure = "insUpdTar_activity"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperec", nTyperec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_activity = Nothing
	End Function
	
	'% Find: busca los datos de una tarifa en particular
	Private Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSpeciality As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTar_activity As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If bFind Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nSpeciality <> Me.nSpeciality Or nCover <> Me.nCover Or dEffecdate <> Me.dEffecdate Then
			
			lrecTar_activity = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'reatar_actlife'
			'+Información leída el 18/12/2001
			With lrecTar_activity
				.StoredProcedure = "reaTar_activity"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nSpeciality = nSpeciality
					Me.nCover = nCover
					Me.dEffecdate = dEffecdate
					nPercent = .FieldToClass("nPercent")
					nAmount = .FieldToClass("nAmount")
					nTyperec = .FieldToClass("nTyperec")
					dNulldate = .FieldToClass("dNulldate")
					Find = True
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_activity = Nothing
	End Function
	
	'% insValEffecdate: verifica la última de fecha de modificación de las tarifas
	Private Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer) As Boolean
		Dim lrecTar_activity As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		
		lrecTar_activity = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaLeg_maxEffecdate '
		'+Información leída el 10/10/2001
		With lrecTar_activity
			.StoredProcedure = "reaTar_activity_maxeffecdate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsValEffecdate = True
				Me.dEffecdate = .FieldToClass("dMax_Effecdate")
				.RCloseRec()
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_activity = Nothing
	End Function
	
	'% insPostMVI630: se realizan las actualizaciones sobre la tabla
	Public Function insPostMVI630(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSpeciality As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal nPercent As Double = 0, Optional ByVal nTyperec As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		On Error GoTo insPostMVI630_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nSpeciality = nSpeciality
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nPercent = nPercent
			.nTyperec = nTyperec
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostMVI630 = Add
			Case "Update"
				insPostMVI630 = Update(2)
			Case "Del"
				insPostMVI630 = Delete
		End Select
		
insPostMVI630_err: 
		If Err.Number Then
			insPostMVI630 = False
		End If
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nSpeciality = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nPercent = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nTyperec = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






