Option Strict Off
Option Explicit On
Public Class Limits_age
	'%-------------------------------------------------------%'
	'% $Workfile:: Limits_age.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla Limits_age al 07-17-2002 16:32:45
	'-  Property                       Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nRelation As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nAge As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	Public dEffecdate_reg As Date
	Public dEffecdate_Temp As Date
	
	'% Add : Permite añadir una edad límite para un producto
	Public Function Add() As Boolean
		Dim lintCount As Integer
		Dim lreccreLimits_age As eRemoteDB.Execute
		
		On Error GoTo creLimits_age_Err
		
		lreccreLimits_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creLimits_age'
		'+ Información leída el 31/01/2000 13:47:17
		
		With lreccreLimits_age
			.StoredProcedure = "creLimits_age"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		
creLimits_age_Err: 
		If Err.Number Then
			Add = False
		End If
		
		'UPGRADE_NOTE: Object lreccreLimits_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLimits_age = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Update : Permite actualizar una edad límite para un asegurado.
	Public Function Update() As Boolean
		Dim lintPos As Integer
		Dim lrecupdLimits_age As eRemoteDB.Execute
		
		On Error GoTo updLimits_age_Err
		
		lrecupdLimits_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updLimits_age'
		'+ Información leída el 31/01/2000 13:50:09
		
		With lrecupdLimits_age
			.StoredProcedure = "updLimits_age"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
updLimits_age_Err: 
		If Err.Number Then
			Update = False
		End If
		
		'UPGRADE_NOTE: Object lrecupdLimits_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLimits_age = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Delete : Permite eliminar una edad límite para un asegurado
	Public Function Delete() As Boolean
		Dim lintPos As Integer
		Dim lrecdelLimits_age As eRemoteDB.Execute
		
		On Error GoTo delLimits_age_Err
		
		lrecdelLimits_age = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delLimits_age'
		'+ Información leída el 31/01/2000 13:54:51
		
		With lrecdelLimits_age
			.StoredProcedure = "delLimits_age"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		
delLimits_age_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		'UPGRADE_NOTE: Object lrecdelLimits_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLimits_age = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValDP060: Función que permite efectuar las validaciones.
	Public Function insValDP060(ByVal sCodispl As String, ByVal sAction As String, ByVal nRelation As Integer, ByVal nAge As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		
		'- Se define la variable insValDP060_Err para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValDP060_Err
		
		'+Si se trata de una validación masiva es necesario mover el punto del grid a la primera posición.
		
		'+Validación del campo "Tarifa".
		
		If nRelation <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3085)
		End If
		
		If nRelation > 0 And sAction = "Add" Then
			If FindRelation(nRelation, nBranch, nProduct, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 11421)
			End If
		End If
		
		If nAge <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3544)
		End If
		
		insValDP060 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		
insValDP060_Err: 
		If Err.Number Then
			insValDP060 = insValDP060 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%FindRelation: Permite encontrar un elemento en la tabla de acuerdo al código de la relación
	Public Function FindRelation(ByVal nRelation As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLimits_age_exist As eRemoteDB.Execute
		
		On Error GoTo reaLimits_age_exist_Err
		
		lrecreaLimits_age_exist = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaLimits_age_exist al 07-17-2002 16:42:59
		'+
		With lrecreaLimits_age_exist
			.StoredProcedure = "reaLimits_age_exist"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dEffecdate_reg = .FieldToClass("dEffecdate")
				FindRelation = True
			Else
				dEffecdate_reg = dtmNull
			End If
		End With
		
reaLimits_age_exist_Err: 
		If Err.Number Then
			FindRelation = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaLimits_age_exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLimits_age_exist = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsPostDP060: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Limtit_age
	Public Function insPostDP060(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRelation As Integer, ByVal nAge As Integer, ByVal dEffecdate As Date, ByVal dEffecdate_reg As Date, ByVal nUsercode As Integer) As Boolean
		Dim lblRelation As Boolean
		Dim lclsLimits_age As eBranches.Limits_ages
		Dim lclsProduct_Win As eProduct.Prod_win
		
		On Error GoTo insPostDP060_err
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.dEffecdate_reg = dEffecdate_reg
			.nAge = nAge
			.nRelation = nRelation
			.nUsercode = nUsercode
		End With
		
		insPostDP060 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				If dEffecdate_reg <> dtmNull Then
					If dEffecdate_reg <> dEffecdate Then
						dEffecdate_Temp = dEffecdate
						Me.dNulldate = dEffecdate
						Me.dEffecdate = dEffecdate_reg
						insPostDP060 = Update()
						Me.dNulldate = dtmNull
						Me.dEffecdate = dEffecdate_Temp
						insPostDP060 = Add()
					End If
				Else
					Me.dNulldate = dtmNull
					insPostDP060 = Add()
				End If
				
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP060 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
					insPostDP060 = Add()
				Else
					insPostDP060 = Update()
				End If
				
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP060 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
					
				Else
					insPostDP060 = Delete()
				End If
				
		End Select
		
		If insPostDP060 Then
			lclsLimits_age = New eBranches.Limits_ages
			lclsProduct_Win = New eProduct.Prod_win
			If lclsLimits_age.Find(nBranch, nProduct, dEffecdate) Then
				Call lclsProduct_Win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP060", "2", nUsercode)
			Else
				Call lclsProduct_Win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP060", "1", nUsercode)
			End If
		End If
		
insPostDP060_err: 
		If Err.Number Then
			insPostDP060 = False
		End If
		'UPGRADE_NOTE: Object lclsLimits_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLimits_age = Nothing
		'UPGRADE_NOTE: Object lclsProduct_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_Win = Nothing
		On Error GoTo 0
	End Function
End Class






