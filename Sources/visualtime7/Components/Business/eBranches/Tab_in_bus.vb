Option Strict Off
Option Explicit On
Public Class Tab_in_bus
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_in_bus.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	'- Definir propiedades principales clase correspondiente tabla tab_in_bus (13/11/2001)
	'Column_name                        Type              Computed      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nArticle As Integer 'smallint    no  2   5       0       no  (n/a)   (n/a)   NULL
	Public nDetailArt As Integer 'smallint    no  2   5       0       no  (n/a)   (n/a)   NULL
	Public sDescript As String 'char        no  30                  yes no  yes SQL_Latin1_General_CP1_CI_AS
	Public nNoteNum As Integer 'int         no  4   10      0       yes (n/a)   (n/a)   NULL
	Public sShort_des As String 'char        no  12                  yes no  yes SQL_Latin1_General_CP1_CI_AS
	Public sStatregt As String 'char        no  1                   yes no  yes SQL_Latin1_General_CP1_CI_AS
	Public nUsercode As Integer 'smallint    no  2   5       0       yes (n/a)   (n/a)   NULL
	Public nActivityType As Integer 'smallint    no  2   5       0       yes (n/a)   (n/a)   NULL
	Public nFamily As Integer 'smallint    no  2   5       0       yes (n/a)   (n/a)   NULL
	'%Find: Lectura Detalle de Actividad Tabla Tab_in_bus
	Public Function Find(ByVal nArticle As Integer) As Boolean
		Dim lrecreaTab_In_Bus As eRemoteDB.Execute
		
		On Error GoTo Find_Tab_in_bus_Err
		
		lrecreaTab_In_Bus = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_In_Bus'
		'+ Información leída el 13/11/2001 02:52:12 p.m.
		With lrecreaTab_In_Bus
			.StoredProcedure = "reaTab_In_Bus"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				.RCloseRec()
			End If
		End With
Find_Tab_in_bus_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_In_Bus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_In_Bus = Nothing
	End Function
	'%Update: Actualiza Detalle de Actividad Tabla Tab_in_bus
	Public Function Update() As Boolean
		Dim lrecupdTab_In_Bus As eRemoteDB.Execute
		
		On Error GoTo Update_Tab_in_bus_Err
		
		lrecupdTab_In_Bus = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updTab_In_Bus'
		'Información leída el 13/11/2001 02:55:13 p.m.
		With lrecupdTab_In_Bus
			.StoredProcedure = "updTab_In_Bus"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_Des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActivityType", nActivityType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFamily", nFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
Update_Tab_in_bus_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTab_In_Bus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_In_Bus = Nothing
	End Function
	'%Add: Ingresar Detalle de Actividad Tabla Tab_in_bus
	Public Function Add() As Boolean
		Dim lreccreTab_In_Bus As eRemoteDB.Execute
		
		On Error GoTo Add_Tab_in_bus_Err
		
		lreccreTab_In_Bus = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creTab_In_Bus'
		'+ Información leída el 13/11/2001 02:56:55 p.m.
		With lreccreTab_In_Bus
			.StoredProcedure = "creTab_In_Bus"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_Des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActivityType", nActivityType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFamily", nFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
Add_Tab_in_bus_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTab_In_Bus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_In_Bus = Nothing
	End Function
	'%Delete: Borrar un registro Detalle de Actividad Tabla Tab_in_bus
	Public Function Delete() As Boolean
		Dim lrecdelTab_In_Bus As eRemoteDB.Execute
		
		On Error GoTo Delete_Tab_in_bus_Err
		
		lrecdelTab_In_Bus = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delTab_In_Bus'
		'+ Información leída el 13/11/2001 02:59:35 p.m.
		With lrecdelTab_In_Bus
			.StoredProcedure = "delTab_In_Bus"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
Delete_Tab_in_bus_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelTab_In_Bus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_In_Bus = Nothing
	End Function
	'% insValHeaderMIN001: validate the header of the page.
	'% insValHeaderMIN001: Validar encabezado de la página
	Public Function insValHeaderMIN001(ByVal lstrCodispl As String, ByVal nArticle As Integer, ByVal nAction As Integer) As String
		On Error GoTo insValHeaderMIN001_Err
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		If nArticle = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(lstrCodispl, 700001,  ,  , " 'Actividad'")
		Else
			If nAction = 401 Or nAction = 302 Then
				If Not Find(nArticle) Then
					lclsErrors.ErrorMessage(lstrCodispl, 715003)
				End If
			End If
		End If
		insValHeaderMIN001 = lclsErrors.Confirm
		
insValHeaderMIN001_Err: 
		If Err.Number Then insValHeaderMIN001 = insValHeaderMIN001 & Err.Description
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	'% insPostMIN001: Realiza la acción especificada de la transacción MIN001
	Public Function insPostMIN001(ByVal sCodispl As String, ByVal sAction As String, ByVal nArticle As Integer, ByVal nDetailArt As Integer, ByVal sDescript As String, ByVal nNoteNum As Integer, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nActivityType As Integer, ByVal nFamily As Integer) As Boolean
		On Error GoTo insPostMIN001_err
		
		sAction = Trim(sAction)
		
		With Me
			.nArticle = nArticle
			.nDetailArt = nDetailArt
			.sDescript = sDescript
			.nNoteNum = nNoteNum
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.nActivityType = nActivityType
			.nFamily = nFamily
		End With
		
		Select Case sAction
			'+ If the selected option is Register
			'+ Si la opción seleccionada es Registrar
			Case "Add"
				insPostMIN001 = Add
				
				'+ If the selected option is Modify
				'+ Si la opción seleccionada es Modificar
			Case "Update"
				insPostMIN001 = Update
				
				'+ If the selected option is Delete
				'+ Si la opción seleccionada es Eliminar
			Case "Del"
				insPostMIN001 = Delete
		End Select
		
insPostMIN001_err: 
		If Err.Number Then
			insPostMIN001 = False
		End If
		On Error GoTo 0
	End Function
	'% insValPopUpMIN001: Realiza validaciones -puntuales y/o masivas- sobre campos de PopUp.
	'% insValPopUpMIN001: Realiza validaciones -puntuales y/o masivas- sobre campos de PopUp.
	Public Function insValPopUpMIN001(ByVal lstrCodispl As String, ByVal sAction As String, ByVal nArticle As Integer, ByVal nDetailArt As Integer, ByVal sDescript As String, ByVal nNoteNum As Integer, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nActivityType As Integer, ByVal nFamily As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValPopUpMIN001_Err
		
		
		'+ Validación del campo Código del detalle (nDetailArt)
		'+ Debe estar lleno
		If nDetailArt <= 0 Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 700001,  ,  , " 'Código del detalle'")
			'+ Si
		ElseIf sAction = "Add" Then 
			If valExistsTab_In_Bus(nArticle, nDetailArt) Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 38011,  ,  , " 'Código del detalle'")
			End If
		End If
		
		'+ Validación del campo Descripción (sDescript)
		'+ Debe estar lleno
		If sDescript = String.Empty Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 700001,  ,  , " 'Descripción'")
		End If
		
		'+ Validación del campo Descripción abreviada (sShort_des)
		'+ Debe estar lleno
		If sShort_des = String.Empty Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 700001,  ,  , " 'Descripción Abreviada'")
		End If
		
		'+ Validación del campo Tipo Actividad (nActivityType)
		'+ Debe estar lleno
		If nActivityType = 0 Then
			Call lobjErrors.ErrorMessage(CStr(nActivityType), 700001,  ,  , " 'Tipo de Actividad'")
		End If
		
		If nActivityType = 1 Then
			If Not (nFamily = eRemoteDB.Constants.intNull Or nFamily = 0) Then
				Call lobjErrors.ErrorMessage(CStr(nFamily), 3993)
			End If
		End If
		
		If nActivityType = 2 Then
			If (nFamily = eRemoteDB.Constants.intNull Or nFamily = 0) Then
				Call lobjErrors.ErrorMessage(CStr(nFamily), 700001,  ,  , " 'Familia'")
			End If
		End If
		
		insValPopUpMIN001 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValPopUpMIN001_Err: 
		If Err.Number Then insValPopUpMIN001 = insValPopUpMIN001 & Err.Description
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%valExistsTab_In_Bus: Verifica si existe información para el dato pasado como parámetro.
	Public Function valExistsTab_In_Bus(ByVal nArticle As Integer, ByVal nDetailArt As Integer) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsTab_In_Bus_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "VALEXISTSTAB_IN_BUS"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExistsTab_In_Bus = True
			End If
		End With
		
valExistsTab_In_Bus_Err: 
		If Err.Number Then
			valExistsTab_In_Bus = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






