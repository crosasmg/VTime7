Option Strict Off
Option Explicit On
Public Class Agent_rate
	'%-------------------------------------------------------%'
	'% $Workfile:: Agent_rate.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/09/03 12:57                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla agent_rate al 04-03-2002 17:18:08
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nInit_rate As Double ' NUMBER     22   2     5    N
	Public nEnd_rate As Double ' NUMBER     22   2     5    N
	Public nFactor As Double ' NUMBER     22   2     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdAgent_rate: Se encarga de actualizar la tabla Agent_rate
	Private Function InsUpdAgent_rate(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdagent_rate As eRemoteDB.Execute
		
		On Error GoTo insUpdagent_rate_Err
		
		lrecinsUpdagent_rate = New eRemoteDB.Execute
		'+ Definición de store procedure insUpdagent_rate al 04-25-2002 18:06:59
		With lrecinsUpdagent_rate
			.StoredProcedure = "insUpdagent_rate"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_rate", nInit_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_rate", nEnd_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdAgent_rate = .Run(False)
		End With
		
insUpdagent_rate_Err: 
		If Err.Number Then
			InsUpdAgent_rate = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdagent_rate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdagent_rate = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAgent_rate(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdAgent_rate(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAgent_rate(3)
	End Function
	
	'%InsValMAG801_K: Validaciones de la transacción(Header)
	Public Function InsValMAG801_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nInit_rate As Double, ByVal nEnd_rate As Double, ByVal nFactor As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAG801_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Rango Inicial: Debe estar lleno
			
			If (nInit_rate = 0 Or nInit_rate = eRemoteDB.Constants.intNull) Then
				.ErrorMessage(sCodispl, 10182)
			End If
			
			'+ Rango Final: Debe estar lleno
			
			If (nEnd_rate = 0 Or nEnd_rate = eRemoteDB.Constants.intNull) Then
				.ErrorMessage(sCodispl, 10183)
			End If
			
			'+ Rango Final: Debe ser mayor al rango inicial
			
			If nEnd_rate < nInit_rate Then
				.ErrorMessage(sCodispl, 10184)
			End If
			
			'+ Factor: Debe estar lleno
			
			If (nFactor = 0 Or nFactor = eRemoteDB.Constants.intNull) Then
				.ErrorMessage(sCodispl, 1095)
			End If
			
			'+ Registro no debe estar repetido
			If sAction = "Add" Then
				If IsExist(nInit_rate, nEnd_rate) Then
					.ErrorMessage(sCodispl, 60214)
				End If
			End If
			
			InsValMAG801_K = .Confirm
		End With
		
InsValMAG801_K_Err: 
		If Err.Number Then
			InsValMAG801_K = "InsValMAG801_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAG801_k: Ejecuta el post de la transacción
	'%               Tabla de factor de cumplimiento por dotación de agentes(MAG801)
	Public Function InsPostMAG801_k(ByVal sAction As String, ByVal nInit_rate As Double, ByVal nEnd_rate As Double, ByVal nFactor As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAG801_k_Err
		
		With Me
			.nInit_rate = nInit_rate
			.nEnd_rate = nEnd_rate
			.nFactor = nFactor
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMAG801_k = Add
			Case "Update"
				InsPostMAG801_k = Update
			Case "Del"
				InsPostMAG801_k = Delete
		End Select
		
InsPostMAG801_k_Err: 
		If Err.Number Then
			InsPostMAG801_k = False
		End If
		On Error GoTo 0
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.Agent_Rate'
	Public Function IsExist(ByVal nInit_rate As Double, ByVal nEnd_rate As Double) As Boolean
		Dim lclsAgent_rate As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		On Error GoTo IsExist_Err
		
		lclsAgent_rate = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valgen_bonsupExist'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsAgent_rate
			.StoredProcedure = "Val_Agent_rate_Exist"
			.Parameters.Add("nInit_rate", nInit_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_rate", nEnd_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nCount") = 1 Then
					IsExist = True
				End If
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lclsAgent_rate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent_rate = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nInit_rate = eRemoteDB.Constants.intNull
		nEnd_rate = eRemoteDB.Constants.intNull
		nFactor = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






