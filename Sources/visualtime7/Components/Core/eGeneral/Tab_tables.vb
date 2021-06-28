Option Strict Off
Option Explicit On
Public Class Tab_tables
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_tables.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Public sTab_code As String
	Public nCount_item As Integer
	Public sCode_item As String
	Public sDesc_item As String
	Public nCount_tabl As Integer
	Public sDescript As String
	Public sDs_select As String
	Public sQ_value As String
	Public nUsercode As Integer
	Public sShowNum As String
	Public sInitQuery As String
	Public sIndSp As String
	Public sKey As String
	
	'**% Update: updates records in the tab_tables table.
	'%Update: Esta rutina se encarga de actualizar los registros de la tabla tab_tables.
	Public Function Update() As Boolean
		'**- Variable definition for the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		On Error GoTo Update_err
		
		Dim lrecinsUpdTab_tables As eRemoteDB.Execute
		lrecinsUpdTab_tables = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.insUpdTab_tables'
		'Información leída el 14/02/2002 11:46:32 a.m.
		With lrecinsUpdTab_tables
			.StoredProcedure = "insUpdTab_tables"
			.Parameters.Add("ActionValue", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTab_code", Trim(sTab_code), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_item", nCount_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_item", sCode_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDesc_item", sDesc_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_tabl", nCount_tabl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDs_select", sDs_select, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQ_value", sQ_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitQuery", sInitQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndSp", sIndSp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecinsUpdTab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTab_tables = Nothing
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Delete: delete records in the tab_tables table.
	'%Delete: Esta rutina se encarga de borrar el registros de la tabla tab_tables.
	Public Function Delete() As Boolean
		'**- Variable definition of the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		On Error GoTo Delete_err
		
		Dim lrecinsUpdTab_tables As eRemoteDB.Execute
		lrecinsUpdTab_tables = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.insUpdtab_tables'
		'Información leída el 13/02/2002 11:41:48 a.m.
		With lrecinsUpdTab_tables
			.StoredProcedure = "insUpdtab_tables"
			.Parameters.Add("ActionValue", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTab_code", sTab_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_item", nCount_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_item", sCode_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDesc_item", sDesc_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_tabl", nCount_tabl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDs_select", sDs_select, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQ_value", sQ_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitQuery", sInitQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndSp", sIndSp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecinsUpdTab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTab_tables = Nothing
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Add: add records to the tab_tables table.
	'%Add: Esta rutina se encarga de Añadir registro en la tabla tab_tables.
	Public Function Add() As Boolean
		'**- Variable definition for the use of the SP and the parameters sent to the same.
		'-Se define la variable para el uso del SP y de los parámetros enviados al mismo
		On Error GoTo Add_err
		
		Dim lrecinsUpdTab_tables As eRemoteDB.Execute
		lrecinsUpdTab_tables = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.insUpdtab_tables'
		'Información leída el 13/02/2002 11:41:48 a.m.
		With lrecinsUpdTab_tables
			.StoredProcedure = "insUpdTab_tables"
			.Parameters.Add("ActionValue", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTab_code", sTab_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_item", nCount_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_item", sCode_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDesc_item", sDesc_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_tabl", nCount_tabl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDs_select", sDs_select, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQ_value", sQ_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitQuery", sInitQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndSp", sIndSp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecinsUpdTab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTab_tables = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Find: validates the control in the tab_tables table if the tab_tables code already exists.
	'%Find: valida contra la tabla tab_tables si ya el código del tab_tables existe
	Public Function Find(ByVal psTab_code As String) As Boolean
		'**- Variable definition for the treatment with the SP and with the parameteres
		'-Se define la variable para el tratamiento con el SP y con los parámetros
		Dim lrecreatab_tables As eRemoteDB.Execute
		On Error GoTo Find_err
		lrecreatab_tables = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.reatab_tables'
		'Información leída el 13/02/2002 11:44:17 a.m.
		With lrecreatab_tables
			.StoredProcedure = "reatab_tables"
			.Parameters.Add("stab_code", Trim(psTab_code), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sTab_code = .FieldToClass("sTab_code")
				nCount_item = .FieldToClass("nCount_item")
				sCode_item = .FieldToClass("sCode_item")
				sDesc_item = .FieldToClass("sDesc_item")
				nCount_tabl = .FieldToClass("nCount_tabl")
				sDescript = .FieldToClass("sDescript")
				sDs_select = .FieldToClass("sDs_select")
				sQ_value = .FieldToClass("sQ_value")
				nUsercode = .FieldToClass("nUsercode")
				sShowNum = .FieldToClass("sShowNum")
				sInitQuery = .FieldToClass("sInitQuery")
				sIndSp = .FieldToClass("sIndSp")
				sKey = .FieldToClass("sKey")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreatab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_tables = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS013_K: Validates the tab_tables.
	'% insValMS013_K: Valida las tab_tables
	Public Function insValMS013_K(ByVal ActionType As String, ByVal sCodispl As String, ByVal sTab_code As String, ByVal nCount_item As Integer, ByVal sCode_item As String, ByVal sDesc_item As String, ByVal nCount_tabl As Integer, ByVal sDescript As String, ByVal sDs_select As String, ByVal sQ_value As String, ByVal nUsercode As Integer, ByVal sShowNum As String, ByVal sInitQuery As String, ByVal sIndSp As String, ByVal sKey As String) As String
		Dim i As Integer
		Dim lclstab_tables As eGeneral.Tab_tabless
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS013_K_err
		
		lclstab_tables = New eGeneral.Tab_tabless
		lclsErrors = New eFunctions.Errors
		'+Validaciones
		If Trim(sTab_code) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10319)
		End If
		If Trim(ActionType) = "Add" Then
			'no debe estar registrado en el archivo de condiciones de búsqueda
			If Find(sTab_code) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10225)
			End If
			If Trim(sDescript) = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10219)
			End If
			If Trim(sDs_select) = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10220)
			End If
		End If
		insValMS013_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclstab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_tables = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS013_K_err: 
		If Err.Number Then
			insValMS013_K = insValMS013_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS013: Updates the Error Message Window.
	'% insPostMS013: Actualiza la Ventana de Mensajes de Error
	Public Function insPostMS013_K(ByVal sAction As String, ByVal sTab_code As String, ByVal nCount_item As Integer, ByVal sCode_item As String, ByVal sDesc_item As String, ByVal nCount_tabl As Integer, ByVal sDescript As String, ByVal sDs_select As String, ByVal sQ_value As String, ByVal nUsercode As Integer, ByVal sShowNum As String, ByVal sInitQuery As String, ByVal sIndSp As String, ByVal sKey As String) As Boolean
		
		On Error GoTo insPostMS013_K_err
		
		With Me
			.sTab_code = sTab_code
			.nCount_item = nCount_item
			.sCode_item = sCode_item
			.sDesc_item = sDesc_item
			.nCount_tabl = nCount_tabl
			.sDescript = sDescript
			.sDs_select = sDs_select
			.sQ_value = sQ_value
			.nUsercode = nUsercode
			If sShowNum = String.Empty Then
				.sShowNum = CStr(2)
			Else
				.sShowNum = CStr(1)
			End If
			.sInitQuery = sInitQuery
			If sIndSp = String.Empty Then
				.sIndSp = CStr(2)
			Else
				.sIndSp = CStr(1)
			End If
			.sKey = sKey
		End With
		
		sAction = Trim(sAction)
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS013_K = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS013_K = Update
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS013_K = Delete
				
		End Select
		
insPostMS013_K_err: 
		If Err.Number Then
			insPostMS013_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class






