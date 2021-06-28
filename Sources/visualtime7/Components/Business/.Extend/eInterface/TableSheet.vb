Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("tablesheet_NET.tablesheet")> Public Class tablesheet
	'+
	'+ Estructura de tabla insudb.tablesheet al 06-22-2004
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSheet As Integer 'NUMBER(5)                     NOT NULL,
	Public sTable As String 'VARCHAR2(40)                  NOT NULL,
	Public sAlias As String 'VARCHAR2(5)                   NOT NULL,
	Public nOrder As Integer 'NUMBER(5)                     NOT NULL,
	Public nUsercode As Integer 'NUMERIC(22)                   NOT NULL
	
	'%InsUpdTableSheet: Se encarga de actualizar la tabla TableSheet
	Private Function InsUpdTableSheet(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTableSheet As eRemoteDB.Execute
		
		On Error GoTo insUpdTableSheet_Err
		
		lrecinsUpdTableSheet = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdTableSheet al 04-25-2002 16:04:41
		'+
		With lrecinsUpdTableSheet
			.StoredProcedure = "insUpdTableSheet"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAlias", sAlias, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTableSheet = .Run(False)
		End With
		
insUpdTableSheet_Err: 
		If Err.Number Then
			InsUpdTableSheet = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdTableSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTableSheet = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTableSheet(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTableSheet(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTableSheet(3)
	End Function
	
	'%InsPostMGI1406: Ejecuta el post de la transacción
	'%               Tabla de Plantillas de Interfaces
	Public Function InsPostMGI1406(ByVal sAction As String, ByVal nSheet As Integer, ByVal sTable As String, ByVal sAlias As String, ByVal nOrder As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMGI1406_Err
		
		With Me
			.nSheet = nSheet
			.sTable = sTable
			.sAlias = sAlias
			.nOrder = nOrder
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMGI1406 = Add
			Case "Update"
				InsPostMGI1406 = Update
			Case "Del"
				InsPostMGI1406 = Delete
		End Select
		
InsPostMGI1406_Err: 
		If Err.Number Then
			InsPostMGI1406 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nSheet = numNull
		sTable = strNull
		sAlias = strNull
		nOrder = numNull
		nUsercode = numNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% insValMGI1406: Valida los datos introducidos
	'-------------------------------------------------------------
	Public Function InsValMGI1406(ByVal sCodispl As String, ByVal sTable As String, ByVal sAlias As String, ByVal nOrder As Integer) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGi1406_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del campo "Nombre de la tabla de base de datos"
		If sTable = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre de la tabla de base de datos")
		End If
		
		'+ Validación del campo "Alias del nombre de tabla de base de datos"
		If sAlias = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Alias del nombre de tabla de base de datos")
		End If
		
		'+ Validación del campo "Ubicación de la tabla en la instrucción FROM"
		If nOrder = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Ubicación de la tabla en la instrucción FROM")
		End If
		
		InsValMGI1406 = lclsErrors.Confirm
		
insValMGi1406_Err: 
		If Err.Number Then
			InsValMGI1406 = lclsErrors.Confirm & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
End Class






