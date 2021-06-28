Option Strict Off
Option Explicit On
Public Class tab_am_ill
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_am_ill.cls                           $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 28/10/03 12:33p                              $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.tab_am_ill al 06-23-2002 15:59:57
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sIllness As String ' CHAR       8    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public sDescript As String ' CHAR       30   0     0    S
	Public sIll_OMS As String ' CHAR       6    0     0    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'- Variable auxiliar
	Private sAuxOMS As String
	
	'Find: Función que realiza la busqueda en la tabla 'tab_am_ill'
	Public Function Find(Optional ByVal sIllness As String = "") As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "reatab_am_ill"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				sIllness = .FieldToClass("sIllness")
				sDescript = .FieldToClass("sDescript")
				sIll_OMS = .FieldToClass("sIll_OMS")
				sStatregt = .FieldToClass("sStatregt")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'%Add: Crea un registros en la tabla tab_am_ill.
	Public Function Add() As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "creTab_am_ill"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIll_OMS", sIll_OMS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'%Update: Actualiza la información de la tabla de enfermedades.
	Public Function Update() As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "updTab_am_ill"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIll_OMS", sIll_OMS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'%Delete: Elimina un registro dado de la tabla de enfermedades
	Public Function Delete() As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "deltab_am_ill"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'IsExistLevelInf: Verifica la existencia de un nivel inferior
	Public Function IsExistLevelInf(ByVal sIllness As String) As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		Dim lintExists As String
		
		On Error GoTo IsExistLevelInf_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "reaLevel_Illness"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExistLevelInf = (.Parameters("nExists").Value = 1)
		End With
		
IsExistLevelInf_Err: 
		If Err.Number Then
			IsExistLevelInf = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'% Find_CodeOMS: Lee la enfermedad asociada a un Codigo de enfermedad según la Organización
	'%               Mundial de la Salud.
	Public Function Find_CodeOMS(ByVal sIll_OMS As String) As Boolean
		Dim lrecreaTab_am_ill_OMS As eRemoteDB.Execute
		
		On Error GoTo Find_CodeOMS_Err
		
		lrecreaTab_am_ill_OMS = New eRemoteDB.Execute
		
		With lrecreaTab_am_ill_OMS
			.StoredProcedure = "reaTab_am_ill_OMS"
			.Parameters.Add("sIll_OMS", sIll_OMS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CodeOMS = True
				.RCloseRec()
			Else
				Find_CodeOMS = False
			End If
		End With
		
Find_CodeOMS_Err: 
		If Err.Number Then
			Find_CodeOMS = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_am_ill_OMS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_ill_OMS = Nothing
	End Function
	
	'%insValMAM003_k: Función que realiza la validacion de los datos introducidor por la ventana
	Public Function insValMAM003_k(ByVal sCodispl As String, ByVal sAction As String, ByVal sIllness As String, ByVal sDescript As String, ByVal sIll_OMS As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAM003_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida que el campo "Código" no esté vacio
			If sIllness = String.Empty Or sIllness = "0" Then
				Call .ErrorMessage(sCodispl, 4230)
			Else
				'+ Debe ser un código válido
				If Len(sIllness) Mod 2 <> 0 Then
					Call .ErrorMessage(sCodispl, 700007,  , eFunctions.Errors.TextAlign.LeftAling, "Enfermedad: ")
				Else
					If sAction = "Del" Then
						'+ Si existen niveles inferiores, no es posible eliminar el código en tratamiento
						If IsExistLevelInf(sIllness) Then
							Call .ErrorMessage(sCodispl, 10307)
						End If
					Else
						'+ Si la acción es registrar
						If sAction = "Add" Then
							If valExistsTab_am_ill(sIllness) Then
								Call .ErrorMessage(sCodispl, 10199)
							Else
								'+ Los niveles deben estar llenos de forma correlativa
								If Len(sIllness) > 2 Then
									'+ Si la enfermedad no corresponde al primer nivel, la enfermedad del nivel anterior debe estar
									'+ registrada en el sistema
									If Not valExistsTab_am_ill(Mid(sIllness, 1, Len(sIllness) - 2)) Then
										Call .ErrorMessage(sCodispl, 10196)
									End If
								End If
							End If
						End If
					End If
				End If
			End If
			
			'+ Se valida que el campo "Descripción" no esté vacio
			If sDescript = String.Empty Or sDescript = "0" Then
				Call .ErrorMessage(sCodispl, 10010)
			End If
			
			'+ Si está lleno, no debe estar asociado a ninguna otra enfermedad de la tabla
			If sIll_OMS <> String.Empty Then
				If sAction = "Update" Then
					If Me.sIll_OMS <> sAuxOMS Then
						If Find_CodeOMS(sIll_OMS) Then
							Call .ErrorMessage(sCodispl, 10197)
						End If
					End If
				Else
					If sAction = "Add" Then
						If Find_CodeOMS(sIll_OMS) Then
							Call .ErrorMessage(sCodispl, 10197)
						End If
					End If
				End If
			End If
			
			'+ Se valida que el campo "Estado" no esté vacio
			If sStatregt = String.Empty Or sStatregt = "0" Then
				Call .ErrorMessage(sCodispl, 1922)
			End If
			
			insValMAM003_k = .Confirm
		End With
		
insValMAM003_k_Err: 
		If Err.Number Then
			insValMAM003_k = insValMAM003_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostMAM003: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostMAM003(ByVal sCodispl As String, ByVal sAction As String, ByVal sIllness As String, ByVal sDescript As String, ByVal sIll_OMS As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMAM003_Err
		
		With Me
			.sIllness = sIllness
			.sDescript = sDescript
			.sIll_OMS = sIll_OMS
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'+Si la opción seleccionada es Registrar
				Case "Add"
					insPostMAM003 = .Add
					
					'+Si la opción seleccionada es Modificar
				Case "Update"
					insPostMAM003 = .Update
					
					'+Si la opción seleccionada es Borrar
				Case "Del"
					insPostMAM003 = .Delete
					
			End Select
			
		End With
		
insPostMAM003_Err: 
		If Err.Number Then
			insPostMAM003 = False
		End If
		On Error GoTo 0
	End Function
	
	'%valExistsTab_am_ill: Verifica si existe información para el dato pasado como parámetro.
	Public Function valExistsTab_am_ill(ByVal sIllness As String) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsTab_am_ill_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "valExistsTab_am_ill"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExistsTab_am_ill = True
			End If
		End With
		
valExistsTab_am_ill_Err: 
		If Err.Number Then
			valExistsTab_am_ill = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






