Option Strict Off
Option Explicit On
Public Class Level_param
	'%-------------------------------------------------------%'
	'% $Workfile:: Level_param.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 07/01/2002
	'+ Los campos llave corresponden a nBranch, nProduct, nLevel, dEffecdate
	'+ Name                    Type                                Nullable
	'+ ----------------------- ----------------------------------  --------
	Public nBranch As Integer 'NUMBER(5)      NO
	Public nProduct As Integer 'NUMBER(5)      NO
	Public nLevel As Integer 'NUMBER(5)      NO
	Public dEffecdate As Date 'DATE           NO
	Public nAge As Integer 'Number(5)
	Public nTax As Double 'Number(5, 2)
	Public nAge_Father As Integer 'Number(5)
	Public nUsercode As Integer 'NUMBER(5)      NO
	
	'% insvalMVI670_K: se realizan las validaciones correspondientes al encabezado de la transacción
	Public Function insValMVI670_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProdmaster As eProduct.Product
		Dim ldtmDate As Date
		
		On Error GoTo insValMVI670_K_err
		
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			'+ Validación campo: Ramo.
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 1022)
			End If
			
			'+ Validación campo: Producto.
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 1014)
			Else
				lclsProdmaster = New eProduct.Product
				If lclsProdmaster.FindProdMasterActive(nBranch, nProduct) Then
					If lclsProdmaster.sBrancht <> eProduct.Product.pmBrancht.pmlife And lclsProdmaster.sBrancht <> eProduct.Product.pmBrancht.pmMixed Then
						Call .ErrorMessage("MVI670", 3987)
					End If
				End If
			End If
			
			'+ Validación campo: Fecha de efecto.
			If dEffecdate = dtmNull Then
				Call .ErrorMessage("MVI670", 3404)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					ldtmDate = getMaxEffecdate(nBranch, nProduct, eRemoteDB.Constants.intNull)
					If ldtmDate <> dtmNull Then
						'+ Si la acción es Actualizar la fecha de efecto debe ser mayor o igual a la fecha de última
						'+ modificación
						If dEffecdate < ldtmDate Then
							Call .ErrorMessage("MVI630", 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmDate & ")")
						End If
					End If
				End If
			End If
			
			insValMVI670_K = .Confirm
		End With
		
insValMVI670_K_err: 
		If Err.Number Then
			insValMVI670_K = "insvalMVI670_K:" & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProdmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProdmaster = Nothing
		On Error GoTo 0
	End Function
	
	'% insvalMVI670: se realizan las validaciones correspondientes al encabezado de la transacción
	Public Function insValMVI670(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLevel As Integer, ByVal nAge As Integer, ByVal nAge_Father As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValMVI670_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Validación del campo: Código del curso.
			If nLevel = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 55684)
			Else
				'+ No debe estar repetido en la tabla
				If sAction = "Add" Then
					If valExistsLevel_param(nBranch, nProduct, nLevel, dEffecdate) Then
						Call .ErrorMessage("MVI630", 10284)
					End If
				End If
			End If
			
			'+ Validación del campo: Edad hijo.
			If nAge = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 55656)
			End If
			
			'+ Validación del campo: Edad del padre.
			If nAge_Father = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVI670", 6026)
			End If
			
			'+ Validación sobre que la edad del hijo debe ser menor que la del padre
			If nAge > 0 And nAge_Father > 0 Then
				If nAge > nAge_Father Then
					Call .ErrorMessage("MVI670", 55981)
				End If
			End If
			
			insValMVI670 = .Confirm
		End With
		
insValMVI670_err: 
		If Err.Number Then
			insValMVI670 = "insValMVI670:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostMVI670: se realizan las actualizaciones sobre la tabla
	Public Function insPostMVI670(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLevel As Integer, ByVal dEffecdate As Date, Optional ByVal nAge As Integer = 0, Optional ByVal nTax As Double = 0, Optional ByVal nAge_Father As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		On Error GoTo insPostMVI670_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nLevel = nLevel
			.dEffecdate = dEffecdate
			.nAge = nAge
			.nTax = nTax
			.nAge_Father = nAge_Father
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostMVI670 = Add
			Case "Update"
				insPostMVI670 = Update(2)
			Case "Del"
				insPostMVI670 = Delete
		End Select
		
insPostMVI670_err: 
		If Err.Number Then
			insPostMVI670 = False
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
		
		On Error GoTo Update_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.valMaxDate_age_collect'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		With lclsExecute
			.StoredProcedure = "insupdLevel_param"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_Father", nAge_Father, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% ClearFields: se inicializan las variables públicas de la clase
	Private Sub ClearFields()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nLevel = eRemoteDB.Constants.intNull
		nAge = eRemoteDB.Constants.intNull
		nTax = eRemoteDB.Constants.intNull
		nAge_Father = eRemoteDB.Constants.intNull
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
	
	'Funcion que valida la effecdate
	Public Function valExistsLevel_param(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLevel As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecLevel_param As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsLevel_param_Err
		
		lrecLevel_param = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valExistsLevel_param
		'+
		With lrecLevel_param
			.StoredProcedure = "valExistsLevel_param"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			If .Parameters("nExists").Value = 1 Then
				valExistsLevel_param = True
			End If
		End With
		
valExistsLevel_param_Err: 
		If Err.Number Then
			valExistsLevel_param = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecLevel_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLevel_param = Nothing
	End Function
	
	'%getMaxEffecdate. Este metodo se encarga de obtener la máxima fecha de efecto de la tabla.
	Public Function getMaxEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLevel As Integer) As Date
		Dim lrecLevel_param As eRemoteDB.Execute
		Dim ldtmPaydate As Date
		
		On Error GoTo getMaxEffecdate_Err
		
		lrecLevel_param = New eRemoteDB.Execute
		
		getMaxEffecdate = dtmNull
		
		With lrecLevel_param
			.StoredProcedure = "reaMaxDateLevel_param"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmPaydate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				getMaxEffecdate = .Parameters("dEffecdate").Value
			End If
		End With
		
getMaxEffecdate_Err: 
		If Err.Number Then
			getMaxEffecdate = dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecLevel_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLevel_param = Nothing
	End Function
End Class






