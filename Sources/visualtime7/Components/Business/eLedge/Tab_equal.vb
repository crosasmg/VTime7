Option Strict Off
Option Explicit On
Public Class Tab_equal
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_equal.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'*+Properties according to the table 'Tab_equal' in the system 07/10/2002 03:59:48 p.m.
	'+ Propiedades según la tabla 'Tab_equal' en el sistema 07/10/2002 03:59:48 p.m.
	
	'-   Nombre de la columna          Tipo
	'------------------------------------------------
	Public nLed_compan As Integer
	Public nTypecode As Integer
	Public sCodeVisual As String
	Public sCodeAsi As String
	Public sDescript As String
	
	'*%Add: Add a record to the table "Tab_equal"
	'% Add: Agrega un registro a la tabla "Tab_equal"
	Public Function Add(ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String, ByVal sCodeAsi As String, ByVal sDescript As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsTab_equal As eRemoteDB.Execute
		
		On Error GoTo AddMCP774_Err
		
		lclsTab_equal = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTab_equal'. Generated on 07/10/2002 03:59:48 p.m.
		
		With lclsTab_equal
			.StoredProcedure = "creTab_equal"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecode", nTypecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeVisual", sCodeVisual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeAsi", sCodeAsi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
AddMCP774_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
		On Error GoTo 0
	End Function
	
	'*%Update: updates a registry to the table "Tab_equal" using the key for this table.
	'% Update: Actualiza un registro a la tabla "Tab_equal" usando la clave para dicha tabla.
	Public Function Update(ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String, ByVal sCodeAsi As String, ByVal sDescript As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsTab_equal As eRemoteDB.Execute
		
		On Error GoTo UpdateMCP774_Err
		
		lclsTab_equal = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTab_equal'. Generated on 07/10/2002 03:59:48 p.m.
		With lclsTab_equal
			.StoredProcedure = "updTab_equal"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecode", nTypecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeVisual", sCodeVisual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeAsi", sCodeAsi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
UpdateMCP774_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
		On Error GoTo 0
	End Function
	
	'*%Delete: Delete a registry the table "Tab_equal" using the key for this table.
	'% Delete: Elimina un registro a la tabla "Tab_equal" usando la clave para dicha tabla.
	Public Function Delete(ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String) As Boolean
		Dim lclsTab_equal As eRemoteDB.Execute
		
		On Error GoTo DeleteMCP774_Err
		
		lclsTab_equal = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTab_equal'. Generated on 07/10/2002 03:59:48 p.m.
		With lclsTab_equal
			.StoredProcedure = "delTab_equal"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecode", nTypecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeVisual", sCodeVisual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
DeleteMCP774_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
		On Error GoTo 0
	End Function
	
	'*%IsExist: It verifies the existence of a registry in table "Tab_equal" using the key of this table.
	'% IsExist: Verifica la existencia de un registro en la tabla "Tab_equal" usando la clave de dicha tabla.
	Public Function IsExist(ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String, ByVal sCodeAsi As String) As Boolean
		Dim lclsTab_equal As eRemoteDB.Execute
		
		On Error GoTo IsExistMCP774_Err
		
		lclsTab_equal = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valTab_equalExist'. Generated on 07/10/2002 03:59:48 p.m.
		With lclsTab_equal
			.StoredProcedure = "reaTab_equal_v"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecode", nTypecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeVisual", sCodeVisual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodeAsi", sCodeAsi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				IsExist = True
				Me.sCodeAsi = .FieldToClass("sCodeAsi")
				Me.sCodeVisual = .FieldToClass("sCodeVisual")
			Else
				IsExist = False
			End If
		End With
		
IsExistMCP774_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
		On Error GoTo 0
	End Function
	
	'*%InsValMCP774_k: Validation of the data for the page of the headed one.
	'% InsValMCP774_k: Validación de los datos para la página del encabezado.
	Public Function InsValMCP774_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nLed_compan As Integer, ByVal nTypecode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCP774_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Compañía contable: Debe estar lleno
		
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		
		'+ Tipo de Código: Debe estar lleno
		
		If nTypecode = 0 Or nTypecode = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60371)
		End If
		
		
		
		InsValMCP774_k = lclsErrors.Confirm
		
InsValMCP774_k_Err: 
		If Err.Number Then
			InsValMCP774_k = InsValMCP774_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'*%InsValMCP774: Validation of the data for the page details.
	'% InsValMCP774: Validación de los datos para la página detalle.
	Public Function InsValMCP774(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String, ByVal sCodeAsi As String, ByVal sDescript As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCP774_Err
		
		lclsErrors = New eFunctions.Errors
		
		
		'+ Código VisualTIME: Debe estar lleno
		
		If sCodeVisual = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60372)
		End If
		
		'+ Código FIN700: Debe estar lleno
		
		If sCodeAsi = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60373)
		End If
		
		'+ No debe existir la misma combinación para la compañía y tipo de código
		
		If sAction = "Add" Or sAction = "Update" Then
			If sCodeVisual <> String.Empty And sCodeAsi = String.Empty Then
                If IsExist(nLed_compan, nTypecode, sCodeVisual, vbNullString) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10259, , , ": [Código VisualTIME]")
                End If
			End If
		End If
		
		If sAction = "Add" Or sAction = "Update" Then
			
			If sCodeAsi <> String.Empty And sCodeVisual = String.Empty Then
                If IsExist(nLed_compan, nTypecode, vbNullString, sCodeAsi) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10259, , , ": [Código FIN700]")
                End If
			End If
		End If
		
		If sAction = "Add" Or sAction = "Update" Then
			
			If sCodeAsi <> String.Empty And sCodeVisual <> String.Empty Then
                If IsExist(nLed_compan, nTypecode, sCodeVisual, vbNullString) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10259, , , ": [Código VisualTIME]")
                End If
				
				If IsExist(nLed_compan, nTypecode, " ", sCodeAsi) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10259,  ,  , ": [Código FIN700]")
				End If
				
				'If IsExist(nLed_compan, nTypecode, sCodeVisual, sCodeAsi) Then
				'    Call lclsErrors.ErrorMessage(sCodispl, 10259, , , ": [Código VisualTIME - Código FIN700]")
				'End If
			End If
		End If
		'+ Descripción : Debe estar llena
		
		If sDescript = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60374)
		End If
		
		InsValMCP774 = lclsErrors.Confirm
		
InsValMCP774_Err: 
		If Err.Number Then
			InsValMCP774 = InsValMCP774 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'*%InsPostMCP774: Pass of the information introduced towards the layers of rules of business and access of data.
	'% InsPostMCP774: Pase de la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostMCP774(ByVal bHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nLed_compan As Integer, ByVal nTypecode As Integer, ByVal sCodeVisual As String, ByVal sCodeAsi As String, ByVal sDescript As String) As Boolean
		
		On Error GoTo InsPostMCP774_Err
		
		If bHeader Then
			InsPostMCP774 = True
		Else
			If sAction = "Add" Then
				InsPostMCP774 = Add(nLed_compan, nTypecode, sCodeVisual, sCodeAsi, sDescript, nUsercode)
			ElseIf sAction = "Update" Then 
				InsPostMCP774 = Update(nLed_compan, nTypecode, sCodeVisual, sCodeAsi, sDescript, nUsercode)
			ElseIf sAction = "Del" Then 
				InsPostMCP774 = Delete(nLed_compan, nTypecode, sCodeVisual)
			End If
		End If
		
InsPostMCP774_Err: 
		If Err.Number Then
			InsPostMCP774 = False
		End If
		
		On Error GoTo 0
		
	End Function
End Class






