Option Strict Off
Option Explicit On
Public Class Actions
	'%-------------------------------------------------------%'
	'% $Workfile:: Actions.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Public nAction As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public sDescript As String 'char       no       1                    yes                                 yes                                 yes
	Public sHel_actio As String 'char       no       1                    yes                                 yes                                 yes
	Public sStatregt As String 'char       no       1                    yes                                 yes                                 yes
	Public nUsercode As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public sPathImage As String 'char       no       1                    yes                                 yes                                 yes
	Public sExist As String 'char       no       1                    yes                                 yes                                 yes
	
	'**% Update: updates records in the actions table.
	'%Update: Esta rutina se encarga de actualizar los registros de la tabla actions.
	Public Function Update() As Boolean
		
		'**- Variable definition for the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		
		On Error GoTo Update_err
		
		Dim lrecupdActions As eRemoteDB.Execute
		lrecupdActions = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updActions'
		'Información leída el 04/02/2002 08:58:31 a.m.
		
		With lrecupdActions
			.StoredProcedure = "insUpdActions"
			.Parameters.Add("ActionValue", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHel_actio", sHel_actio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 70, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPathImage", sPathImage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdActions = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: delete records in the actions table.
	'%Delete: Esta rutina se encarga de borrar el registros de la tabla actions.
	Public Function Delete() As Boolean
		
		'**- Variable definition of the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		On Error GoTo Delete_err
		
		Dim lrecupdActions As eRemoteDB.Execute
		lrecupdActions = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updActions'
		'Información leída el 04/02/2002 08:58:31 a.m.
		
		With lrecupdActions
			.StoredProcedure = "insUpdActions"
			.Parameters.Add("ActionValue", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHel_actio", sHel_actio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 70, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPathImage", sPathImage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecupdActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdActions = Nothing
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Add: add records to the actions table.
	'%Add: Esta rutina se encarga de Añadir registro en la tabla actions.
	Public Function Add() As Boolean
		
		'**- Variable definition for the use of the SP and the parameters sent to the same.
		'-Se define la variable para el uso del SP y de los parámetros enviados al mismo
		On Error GoTo Add_err
		
		Dim lrecupdActions As eRemoteDB.Execute
		lrecupdActions = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updActions'
		'Información leída el 04/02/2002 08:58:31 a.m.
		
		With lrecupdActions
			.StoredProcedure = "insUpdActions"
			.Parameters.Add("ActionValue", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHel_actio", sHel_actio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 70, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPathImage", sPathImage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecupdActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdActions = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Find: validates the control in the actions table if the actions code already exists.
	'%Find: valida contra la tabla actions si ya el código del actions existe
	Public Function Find(ByVal nActions As Integer) As Boolean
		
		'**- Variable definition for the treatment with the SP and with the parameteres
		'-Se define la variable para el tratamiento con el SP y con los parámetros
		
		Dim ltempReaActions As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		ltempReaActions = New eRemoteDB.Execute
		With ltempReaActions
			.StoredProcedure = "reaActions"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nAction = .FieldToClass("nAction")
				sDescript = .FieldToClass("sDescript")
				sHel_actio = .FieldToClass("sHel_actio")
				sStatregt = .FieldToClass("sStatregt")
				nUsercode = .FieldToClass("nUsercode")
				sPathImage = .FieldToClass("sPathImage")
				sExist = .FieldToClass("sExist")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempReaActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempReaActions = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS006_K: Validates the actions.
	'% insValMS006_K: Valida las actiones
	Public Function insValMS006_K(ByVal sCodispl As String, ByVal ActionType As String, ByVal nAction As Integer, ByVal sDescript As String, ByVal sHel_actio As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sPathImage As String) As String
		Dim lclsactions As eGeneral.Actions
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValMS006_K_err
		lclsactions = New eGeneral.Actions
		lclsErrors = New eFunctions.Errors
		
		If nAction = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3794)
		Else
			'+Validaciones de código
			If Trim(ActionType) = "Add" Then
				If lclsactions.Find(nAction) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			Else
				If Not lclsactions.Find(nAction) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10061)
				End If
			End If
		End If
		
		'+Validaciones de descripcion
		If nAction <> eRemoteDB.Constants.intNull Then
			If sDescript = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 10062)
			End If
		End If
		If sStatregt = "" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012)
		End If
		insValMS006_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsactions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsactions = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS006_K_err: 
		If Err.Number Then
			insValMS006_K = insValMS006_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS001: Updates the Error Message Window.
	'% insPostMS001: Actualiza la Ventana de Mensajes de Error
	Public Function insPostMS006_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nAction As Integer, ByVal sDescript As String, ByVal sHel_actio As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sPathImage As String) As Boolean
		
		On Error GoTo insPostMS006_K_err
		
		With Me
			.nAction = nAction
			.sDescript = sDescript
			.sHel_actio = sHel_actio
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.sPathImage = sPathImage
		End With
		
		sAction = Trim(sAction)
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS006_K = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS006_K = Update
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS006_K = Delete
				
		End Select
		
insPostMS006_K_err: 
		If Err.Number Then
			insPostMS006_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class






