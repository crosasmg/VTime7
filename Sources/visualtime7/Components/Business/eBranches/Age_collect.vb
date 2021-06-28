Option Strict Off
Option Explicit On
Public Class Age_collect
	'%-------------------------------------------------------%'
	'% $Workfile:: Age_collect.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 07/01/2002
	'+ Los campos llave corresponden a nBranch, nProduct, dEffecdate, nInitAge
	'+ Name                    Type                                Nullable
	'+ ----------------------- ----------------------------------  --------
	Public nBranch As Integer 'NUMBER(5)      NO
	Public nProduct As Integer 'NUMBER(5)      NO
	Public dEffecdate As Date 'DATE           NO
	Public nInitAge As Double 'NUMBER(5)      NO
	Public nEndAge As Double 'Number(5)      YES
	Public nAct_perc As Double 'Number(5, 2)   YES
	Public dNulldate As Date 'Date           YES
	Public nUsercode As Integer 'NUMBER(5)      NO
	
	'% insValMVI693_K: se realizan las validaciones correspondientes al encabezado de la transacción
	Public Function insValMVI693_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lobjValid As eFunctions.valField
		Dim lcolAge_collect As Age_collects
		Dim lblnValid As Boolean
		Dim ldtmDate As Date
		
		On Error GoTo insValMVI693_K_err
		
		lobjErrors = New eFunctions.Errors
		lobjValid = New eFunctions.valField
		lobjValues = New eFunctions.Values
		lcolAge_collect = New Age_collects
		
		lobjValid.objErr = lobjErrors
		
		lblnValid = True
		
		With lobjErrors
			'+ Validación campo: Ramo.
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 1022)
			End If
			
			'+ Validación del campo: Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI693", 11009)
				lblnValid = False
			End If
			
			'+ Validación del campo: Fecha
			lobjValid.ErrEmpty = 3404
			If lobjValid.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					ldtmDate = getMaxEffecdate(nBranch, nProduct)
					If ldtmDate <> dtmNull Then
						If dEffecdate < ldtmDate Then
							Call .ErrorMessage("MVI693", 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmDate & ")")
						End If
					End If
				End If
			End If
			insValMVI693_K = .Confirm
		End With
		
insValMVI693_K_err: 
		If Err.Number Then
			insValMVI693_K = "insValMVI693_K:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValid = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lcolAge_collect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAge_collect = Nothing
	End Function
	
	'% insValMVI693: se realizan las validaciones correspondientes al detalle de la transacción
	Public Function insValMVI693(ByVal WindowType As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nInitAge As Integer, ByVal nEndAge As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValMVI693_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			If WindowType = "PopUp" Then
				'+ Edad inicial
				If nInitAge = eRemoteDB.Constants.intNull Then
					'+ Debe estar lleno
					Call .ErrorMessage("MVI693", 3545)
				Else
					'+ Debe ser positiva
					If nInitAge < 0 Then
						Call .ErrorMessage("MVI693", 11402)
					End If
				End If
				
				'+ Edad final
				If nEndAge = eRemoteDB.Constants.intNull Then
					'+ Debe estar lleno
					Call .ErrorMessage("MVI693", 3547)
				Else
					'+ Debe ser mayor a la edad inicial
					If nEndAge <= nInitAge Then
						Call .ErrorMessage("MVI693", 3546)
					End If
				End If
				
				'+ Se verifica que la edad inicial y final no se encuentren en otro rango dentro de la tabla
				If nInitAge <> eRemoteDB.Constants.intNull And nEndAge <> eRemoteDB.Constants.intNull Then
					Call insValidRange(sAction, nBranch, nProduct, dEffecdate, nInitAge, nEndAge, lobjErrors)
				End If
			End If
			
			'+ % Ancho del rango
			If nPercent = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI693", 55688)
			End If
			
			insValMVI693 = .Confirm
		End With
		
insValMVI693_err: 
		If Err.Number Then
			insValMVI693 = "insValMVI693:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostMVI693: se realizan las actualizaciones sobre la tabla
	Public Function insPostMVI693(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nInitAge As Integer, Optional ByVal nPercent As Double = 0, Optional ByVal nEndAge As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		On Error GoTo insPostMVI693_err
		
		With Me
			.dEffecdate = dEffecdate
			.nAct_perc = nPercent
			.nBranch = nBranch
			.nEndAge = nEndAge
			.nInitAge = nInitAge
			.nProduct = nProduct
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostMVI693 = Add
			Case "Update"
				insPostMVI693 = Update(2)
			Case "Del"
				insPostMVI693 = Delete
		End Select
		
insPostMVI693_err: 
		If Err.Number Then
			insPostMVI693 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se inserta un registro en la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro en la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: actualiza la informacón de la tabla
	Private Function Update(Optional ByVal nAction As Integer = 0) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo insValMaxEffecdate_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.valMaxDate_age_collect'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		With lclsExecute
			.StoredProcedure = "insupdAge_collect"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitAge", nInitAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndAge", nEndAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAct_perc", nAct_perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
insValMaxEffecdate_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% getMaxEffecdate: devuelve la fecha de última modificación de la tabla
	Private Function getMaxEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer) As Date
		Dim lclsExecute As eRemoteDB.Execute
		Dim ldtmEffecdate As Date
		
		On Error GoTo getMaxEffecdate_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		getMaxEffecdate = dtmNull
		'+Definición de parámetros para stored procedure 'insudb.valMaxDate_age_collect'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		With lclsExecute
			.StoredProcedure = "reaAge_collect_maxdate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				getMaxEffecdate = .Parameters("dEffecdate").Value
			End If
		End With
		
getMaxEffecdate_Err: 
		If Err.Number Then
			getMaxEffecdate = dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% insValidRange: se verifica que la edad inicial y/o final no se encuentre dentro de la tabla
	Private Sub insValidRange(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nInitAge As Integer, ByVal nEndAge As Integer, ByRef lobjErrors As eFunctions.Errors)
		Dim lcolAge_collect As Age_collects
		Dim lclsAge_collect As Age_collect
		
		On Error GoTo insValidRange_err
		
		lcolAge_collect = New Age_collects
		
		With lobjErrors
			If lcolAge_collect.Find(nBranch, nProduct, dEffecdate) Then
				For	Each lclsAge_collect In lcolAge_collect
					If sAction = "Add" Then
						If nInitAge >= lclsAge_collect.nInitAge And nInitAge <= lclsAge_collect.nEndAge Then
							Call .ErrorMessage("MVI693", 10185,  ,  , "(" & lclsAge_collect.nInitAge & " - " & lclsAge_collect.nEndAge & ")")
						End If
					End If
				Next lclsAge_collect
			End If
		End With
		
insValidRange_err: 
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsAge_collect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAge_collect = Nothing
		'UPGRADE_NOTE: Object lcolAge_collect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAge_collect = Nothing
	End Sub
	
	'% Update_Act_perc: actualiza el porcentaje de toda los registros
	Public Function Update_Act_perc(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAct_perc As Double) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo Update_Act_perc_err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.valMaxDate_age_collect'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		With lclsExecute
			.StoredProcedure = "updAge_collect_act_perc"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAct_perc", nAct_perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update_Act_perc = True
			End If
		End With
		
Update_Act_perc_err: 
		If Err.Number Then
			Update_Act_perc = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nInitAge As Integer) As Boolean
		Dim lrecreaAge_collect As eRemoteDB.Execute
		
		On Error GoTo reaAge_collect_Err
		
		lrecreaAge_collect = New eRemoteDB.Execute
		
		With lrecreaAge_collect
			.StoredProcedure = "reaAge_collect"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitage", nInitAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nInitAge = .FieldToClass("nInitage")
				Me.nEndAge = .FieldToClass("nEndage")
				Me.nAct_perc = .FieldToClass("nAct_perc")
				Me.dNulldate = .FieldToClass("dNulldate")
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaAge_collect_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaAge_collect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAge_collect = Nothing
		On Error GoTo 0
		
	End Function
	
	'% ClearFields: se inicializan las variables públicas de la clase
	Private Sub ClearFields()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nInitAge = eRemoteDB.Constants.intNull
		nEndAge = eRemoteDB.Constants.intNull
		nAct_perc = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






