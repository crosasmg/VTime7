Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("calend_NET.calend")> Public Class calend
	'+
	'+ Estructura de tabla insudb.Calend_Interface
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSheet As Integer 'NUMBER(5)                     NOT NULL,
	Public nId As Integer 'NUMBER(5)                     NOT NULL,
	Public nDay As Integer 'NUMBER(5),
	Public dDateProc As Date 'DATE,
	Public sHour As String 'VARCHAR2(5 BYTE)                   NOT NULL,
	Public nUsercode As Integer 'NUMBER(5)                          NOT NULL,
	
	
	'%InsUpdTableSheet: Se encarga de actualizar la tabla Calend_Interface
	Private Function InsUpdCalend_Interface(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdCalend_Interface As eRemoteDB.Execute
		
		On Error GoTo insUpdCalend_Interface_Err
		
		lrecinsUpdCalend_Interface = New eRemoteDB.Execute
		With lrecinsUpdCalend_Interface
			.StoredProcedure = "insUpdCalend_Interface"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay", nDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateProc", dDateProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour", sHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdCalend_Interface = .Run(False)
		End With
		
insUpdCalend_Interface_Err: 
		If Err.Number Then
			InsUpdCalend_Interface = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdCalend_Interface may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdCalend_Interface = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdCalend_Interface(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdCalend_Interface(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdCalend_Interface(3)
	End Function
	
	'%InsPostMGI1408: Ejecuta el post de la transacción
	'%               Calendario de Interfaces
	Public Function InsPostMGI1408(ByVal sAction As String, ByVal nSheet As Integer, ByVal nId As Integer, ByVal dDateProc As Date, ByVal nDay As Integer, ByVal sHour As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMGI1408_Err
		
		With Me
			.nSheet = nSheet
			.nId = nId
			.nDay = nDay
			.dDateProc = dDateProc
			.sHour = sHour
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMGI1408 = Add
			Case "Update"
				InsPostMGI1408 = Update
			Case "Del"
				InsPostMGI1408 = Delete
		End Select
		
InsPostMGI1408_Err: 
		If Err.Number Then
			InsPostMGI1408 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nSheet = numNull
		nId = numNull
		nDay = numNull
		dDateProc = dtmNull
		sHour = strNull
		nUsercode = numNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% insValMGI1408: Valida los datos introducidos
	'-------------------------------------------------------------
	Public Function InsValMGI1408(ByVal sCodispl As String, ByVal nPeriod As Integer, ByVal nDay As Integer, ByVal dDateProc As Date, ByVal sHour As String) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGI1408_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Si Periodicidad = 2 ("Mensual") "dia" debe estar lleno
		'+ Si Periodicidad = 4 ("Eventual") "Fecha" debe estar lleno
		If nPeriod = 2 Then
			'+ Validación del campo "Día de ejecución del proceso"
			If nDay = numNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Día de ejecución del proceso")
			End If
		ElseIf nPeriod = 4 Then 
			'+ Validación del campo "Fecha exacta de ejecución del proceso"
			If dDateProc = dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Fecha exacta de ejecución del proceso")
			End If
		End If
		
		'+ Validación del campo "Hora de ejecución del proceso"
		If sHour = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Hora de ejecución del proceso")
		End If
		
		InsValMGI1408 = lclsErrors.Confirm
		
insValMGI1408_Err: 
		If Err.Number Then
			InsValMGI1408 = lclsErrors.Confirm & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsCalIdCalend: Se encarga de Rescatar correlativo desde la tabla Calend_interface
	Public Function InsCalIdCalend(ByVal nSheet As Integer) As Integer
		Dim lrecInsCalIdCalend As eRemoteDB.Execute
		
		On Error GoTo InsCalIdCalend_Err
		
		lrecInsCalIdCalend = New eRemoteDB.Execute
		
		With lrecInsCalIdCalend
			.StoredProcedure = "InsCalIdCalend"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nId", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			InsCalIdCalend = .Parameters("nId").Value
			
		End With
		
InsCalIdCalend_Err: 
		If Err.Number Then
			InsCalIdCalend = 0
		End If
		'UPGRADE_NOTE: Object lrecInsCalIdCalend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalIdCalend = Nothing
		On Error GoTo 0
	End Function
End Class






