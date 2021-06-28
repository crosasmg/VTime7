Option Strict Off
Option Explicit On
Public Class Group_columns
	'%-------------------------------------------------------%'
	'% $Workfile:: Group_columns.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:39p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name          Type                     Computed  Length Prec  Scale Nullable
	'-------------------- ------------------------ --------- ------ ----- ----- ------------
	Public nSheet As Integer 'smallint     no        2      5     0     no
	Public sField As String 'char         no        20                 no
	Public nUsercode As Integer 'smallint     no        2      5     0     no
	Public sColumnName As String 'char         no        30                 yes
	Public nOrder As Integer 'smallint     no        2      5     0     yes
	Public sValuesList As String 'char         no        30                 yes
	Public sTable As String 'char         no        20                 yes
	Public sRequire As String 'char         no        1                  yes
	Public sComment As String 'char         no        30                 yes
	Public nIdRec As Integer
	
	'-Se definen las variables auxiliares
	Public Enum typInfo
		typGeneral = 0
		typParticular = 1
	End Enum
	
	Public nBranch As Integer
	Public sTypeInfo As typInfo
	Public nId As Integer
	
	'% Add: Agrega los datos correspondientes para una hoja
	Public Function Add() As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		With lreaBulletins_det
			.StoredProcedure = "creGroup_columns"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValuesList", sValuesList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComment", sComment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins_det = Nothing
	End Function
	
	'%FindTabSystables: Busca la existencia de una Tabla
	Public Function FindTabSystables(ByVal sCondition As String) As String
		Dim lrectabSystables As eRemoteDB.Execute
		
		On Error GoTo FindTabSystables_Err
		
		lrectabSystables = New eRemoteDB.Execute
		
		With lrectabSystables
			.StoredProcedure = "tabSystablespkg.tabSystables"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sShowNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindTabSystables = .FieldToClass("sName", String.Empty)
			End If
		End With
		
FindTabSystables_Err:
        If Err.Number Then
            FindTabSystables = ""
            FindTabSystables = FindTabSystables & Err.Description
        End If
        On Error GoTo 0
		'UPGRADE_NOTE: Object lrectabSystables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectabSystables = Nothing
	End Function
	
	'%FindTabSyscolumns: Busca la existencia de una Columna en una Tabla
	Public Function FindTabSyscolumns(ByVal sTable As String, ByVal sCondition As String) As String
		Dim lrectabSyscolumns As eRemoteDB.Execute
		
		On Error GoTo FindTabSyscolumns_Err
		
		lrectabSyscolumns = New eRemoteDB.Execute
		
		With lrectabSyscolumns
			.StoredProcedure = "TABSYSCOLUMNSPKG.TABSYSCOLUMNS"
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sShowNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				FindTabSyscolumns = .FieldToClass("id", String.Empty)
			End If
		End With
		
FindTabSyscolumns_Err:
        If Err.Number Then
            FindTabSyscolumns = ""
            FindTabSyscolumns = FindTabSyscolumns & Err.Description
        End If
        On Error GoTo 0
		'UPGRADE_NOTE: Object lrectabSyscolumns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectabSyscolumns = Nothing
	End Function
	
	'%FindTabSysProcedure: Busca la existencia de una Tabla o un Procedure relacionada con una tabla,columna
	Public Function FindTabSysProcedure(ByVal sTable As String, ByVal sColumn As String, ByVal sCondition As String) As String
		Dim lrectabSysProcedure As eRemoteDB.Execute
		
		On Error GoTo FindTabSysProcedure_Err
		
		lrectabSysProcedure = New eRemoteDB.Execute
		
		With lrectabSysProcedure
			.StoredProcedure = "tabSystables_1pkg.tabSystables_1"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sShowNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumn", sColumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindTabSysProcedure = .FieldToClass("table_name", String.Empty)
			End If
		End With
		
FindTabSysProcedure_Err:
        If Err.Number Then
            FindTabSysProcedure = ""
            FindTabSysProcedure = FindTabSysProcedure & Err.Description
        End If
        On Error GoTo 0
		'UPGRADE_NOTE: Object lrectabSysProcedure may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectabSysProcedure = Nothing
	End Function
	
	'%Find: Obtiene las columnas de una plantilla para la carga de pólizas/certificados.
	Public Function Find(ByVal nSheet As Integer, Optional ByVal sField As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "ReaGroup_columns_v"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					nSheet = .FieldToClass("nSheet", 0)
					nId = .FieldToClass("nId", 0)
					nBranch = .FieldToClass("nBranch", 0)
					sTypeInfo = .FieldToClass("sTypeInfo", "1")
					nOrder = .FieldToClass("nOrder", 0)
					Find = True
				End If
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%FindGroupColSheet: Obtiene las columnas por hoja
	Public Function FindGroupColSheet(ByVal nSheet As Integer, ByVal sColumnName As String) As Boolean
		Dim lrecreaColSheet_v As eRemoteDB.Execute
		
		On Error GoTo FindGroupColSheet_Err
		
		lrecreaColSheet_v = New eRemoteDB.Execute
		
		With lrecreaColSheet_v
			.StoredProcedure = "reaColSheet_v"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				FindGroupColSheet = True
			End If
		End With
		
FindGroupColSheet_Err: 
		If Err.Number Then
			FindGroupColSheet = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaColSheet_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColSheet_v = Nothing
	End Function
	
	'%FindGroup_columns_order: Obtiene las columnas según un orden
	Public Function FindGroup_columns_order(ByVal nSheet As Integer, ByVal sField As String, Optional ByVal sColumnName As String = "", Optional ByVal nOrder As Integer = 0, Optional ByVal nAction As Integer = 0) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo FindGroup_columns_order_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "ReaGroup_columns_order"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					FindGroup_columns_order = True
				End If
			End If
		End With
		
FindGroup_columns_order_Err: 
		If Err.Number Then
			FindGroup_columns_order = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'% Update: Agrega los datos correspondientes para una hoja
	Public Function Update() As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		With lreaBulletins_det
			.StoredProcedure = "updGroup_columns"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValuesList", sValuesList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComment", sComment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdrec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins_det = Nothing
	End Function
	
	'% Delete: Elimina los datos correspondientes a la hoja
	Public Function Delete() As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		With lreaBulletins_det
			.StoredProcedure = "delGroup_columns"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdrec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins_det = Nothing
	End Function
	
	'%insValMCA006: Esta función se encarga de validar los datos introducidos en la zona de detalle para forma.
	Public Function insValMCA006(ByVal sAction As String, ByVal nSheet As Integer, ByVal nBranch As Integer, ByVal sTable As String, Optional ByVal sField As String = "", Optional ByVal sColumnName As String = "", Optional ByVal sComment As String = "", Optional ByVal nOrder As Integer = 0, Optional ByVal sRequire As String = "", Optional ByVal sValuesList As String = "") As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalDate As eFunctions.valField
		Dim lintAction As Integer
		
		On Error GoTo insValMCA006_Err
		
		lerrTime = New eFunctions.Errors
		
		'+ Validación de campo asociado a BD
		With lerrTime
			If Trim(sTable) = String.Empty Then
				Call .ErrorMessage("MCA006", 10905)
			Else
				If FindTabSystables(sTable) = String.Empty Then
					Call .ErrorMessage("MCA006", 55815)
				Else
					'+ Validación de campo asociado a BD
					If Trim(sField) = String.Empty Then
						Call .ErrorMessage("MCA006", 10898)
					Else
						If FindTabSyscolumns(sTable, sField) = String.Empty Then
							Call .ErrorMessage("MCA006", 55816)
						Else
							If (sValuesList <> String.Empty And FindTabSysProcedure(sTable, sField, sValuesList) = String.Empty) Then
								Call .ErrorMessage("MCA006", 55816)
							End If
						End If
					End If
				End If
			End If
			
			'+ Validación de nombre de la columna
			If Trim(sColumnName) = String.Empty Then
				Call .ErrorMessage("MCA006", 10897)
			Else
				If sAction = "Update" Then
					lintAction = 2
				Else
					lintAction = 1
				End If
				If FindGroup_columns_order(nSheet, sField, sColumnName, 0, lintAction) Then
					Call .ErrorMessage("MCA006", 10931)
				End If
			End If
			
			'+ Validación del orden
			If nOrder = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MCA006", 10900)
			Else
				'+ Se valida que el orden no este duplicado
				If sAction = "Update" Then
					lintAction = 2
				Else
					lintAction = 1
				End If
				If FindGroup_columns_order(nSheet, sField, String.Empty, nOrder, lintAction) Then
					Call .ErrorMessage("MCA006", 10902)
				End If
			End If
			
			'+ No permitir modificar si está seleccionada en algún archivo de Excel.
			If sAction = "Update" Then
				If FindGroupColSheet(nSheet, sColumnName) Then
					Call .ErrorMessage("MCA006", 10904)
				End If
			End If
			
			insValMCA006 = .Confirm
		End With
		
insValMCA006_Err: 
		If Err.Number Then
			insValMCA006 = insValMCA006 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insValMCA006_K: Esta función se encarga de validar los datos introducidos en el header de la forma.
	Public Function insValMCA006_K(ByVal nAction As Integer, ByVal nSheet As Integer, ByVal nBranch As Integer, ByVal sTypeInfo As typInfo) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsBranches As eProduct.Branches
		
		On Error GoTo insValMCA006_Err
		
		lerrTime = New eFunctions.Errors
		lclsBranches = New eProduct.Branches
		
		With lerrTime
			'+ Validación de la hoja de Excel
			If nSheet = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MCA006", 10906)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If valExistsSheet(nSheet, "") Then
						Call .ErrorMessage("MCA006", 10929)
					End If
				Else
					If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
						If Not valExistsSheet(nSheet, "") Then
							Call .ErrorMessage("MCA006", 10930)
						End If
					End If
				End If
			End If
			
			'+ Validación del ramo
			If sTypeInfo = typInfo.typParticular Then
				If nBranch = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("MCA006", 9064)
				Else
					If Not lclsBranches.valExistsTab_name_b(nBranch, "") Then
						Call .ErrorMessage("MCA006", 3341)
					End If
				End If
			End If
			
			insValMCA006_K = .Confirm
		End With
		
insValMCA006_Err: 
		If Err.Number Then
			insValMCA006_K = insValMCA006_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsBranches may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBranches = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'%insPostMCA006: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostMCA006(Optional ByVal sAction As String = "", Optional ByVal nSheet As Integer = eRemoteDB.Constants.intNull, Optional ByVal nBranch As Integer = eRemoteDB.Constants.intNull, Optional ByVal sTable As String = "", Optional ByVal sField As String = "", Optional ByVal sColumnName As String = "", Optional ByVal sComment As String = "", Optional ByVal nOrder As Integer = eRemoteDB.Constants.intNull, Optional ByVal sRequire As String = "", Optional ByVal sValuesList As String = "", Optional ByVal nUsercode As Integer = eRemoteDB.Constants.intNull, Optional ByVal nIdRec As Integer = eRemoteDB.Constants.intNull) As Boolean
		On Error GoTo insPostMCA006_Err
		
		With Me
			If nSheet <> eRemoteDB.Constants.intNull Then
				.nSheet = nSheet
			End If
			
			If nBranch <> eRemoteDB.Constants.intNull Then
				.nBranch = nBranch
			End If
			
			If sTable <> String.Empty Then
				.sTable = sTable
			End If
			
			If sField <> String.Empty Then
				.sField = sField
			End If
			
			If sColumnName <> String.Empty Then
				.sColumnName = sColumnName
			End If
			
			If sComment <> String.Empty Then
				.sComment = sComment
			End If
			
			If nOrder <> eRemoteDB.Constants.intNull Then
				.nOrder = nOrder
			End If
			
			If sRequire <> String.Empty Then
				.sRequire = sRequire
			End If
			
			If sValuesList <> String.Empty Then
				.sValuesList = sValuesList
			End If
			
			If nUsercode <> eRemoteDB.Constants.intNull Then
				.nUsercode = nUsercode
			End If
			If nIdRec <> eRemoteDB.Constants.intNull Then
				.nIdRec = nIdRec
			End If
		End With
		
		'+ Se efectúa el proceso según la acción.
		Select Case sAction
			Case "Add"
				insPostMCA006 = Add
			Case "Update"
				insPostMCA006 = Update
			Case "Del"
				insPostMCA006 = Delete
		End Select
		
insPostMCA006_Err: 
		If Err.Number Then
			insPostMCA006 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%valExistsSheet: Valida si existe una hoja con información.
	Public Function valExistsSheet(ByVal nSheet As Integer, ByVal sField As String) As Boolean
		Dim lrecGroup_columns As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsSheet_Err
		
		lrecGroup_columns = New eRemoteDB.Execute
		
		With lrecGroup_columns
			.StoredProcedure = "valExistsSheet"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valExistsSheet = True
				End If
			End If
		End With
		
valExistsSheet_Err: 
		If Err.Number Then
			valExistsSheet = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecGroup_columns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGroup_columns = Nothing
	End Function
	
	'%getGroupSheet_sTable: Valida si existe una hoja con información.
	Public Function getGroupSheet_sTable(ByVal nSheet As Integer) As String
		Dim lrecGroup_columns As eRemoteDB.Execute
        Dim lstrTable As String = ""

        On Error GoTo getGroupSheet_sTable_Err
		
		lrecGroup_columns = New eRemoteDB.Execute
		
		getGroupSheet_sTable = String.Empty
		
		With lrecGroup_columns
			.StoredProcedure = "reaGroup_columns_Group"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", lstrTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				getGroupSheet_sTable = Trim(.Parameters.Item("sTable").Value)
			End If
		End With
		
getGroupSheet_sTable_Err: 
		If Err.Number Then
			getGroupSheet_sTable = String.Empty
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecGroup_columns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGroup_columns = Nothing
	End Function
End Class






