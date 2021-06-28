Option Strict Off
Option Explicit On
Public Class Bud_agen
	'%-------------------------------------------------------%'
	'% $Workfile:: Bud_agen.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla bud_agen al 03-04-2002 10:40:24
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nAgent_quan As Integer ' NUMBER     22   0     5    N
	Public nAgency As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdBud_agen: Se encarga de actualizar la tabla Bud_agen
	Private Function InsUpdBud_agen(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdbud_agen As eRemoteDB.Execute
		On Error GoTo insUpdbud_agen_Err
		
		lrecinsUpdbud_agen = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdbud_agen al 03-04-2002 11:34:19
		'+
		With lrecinsUpdbud_agen
			.StoredProcedure = "insUpdbud_agen"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgent_quan", nAgent_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdBud_agen = .Run(False)
		End With
		
insUpdbud_agen_Err: 
		If Err.Number Then
			InsUpdBud_agen = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdbud_agen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdbud_agen = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdBud_agen(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdBud_agen(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdBud_agen(3)
	End Function
	
	'%InsValMAG800_K: Validaciones de la transacción(Header)
	Public Function InsValMAG800_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nAgent_quan As Integer, ByVal nAgency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAG800_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Cantidad de agentes: Debe estar lleno.
			
			If nAgent_quan = 0 Or nAgent_quan = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60388)
			End If
			
			
			'+ Agencia: Debe estar lleno
			
			If nAgency = 0 Or nAgency = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55518)
			End If
			
			
			' Registro no debe estar repetido
			
			If sAction = "Add" Then
				If insValBud_AgentExist(nAgency) Then
					.ErrorMessage(sCodispl, 10284)
				End If
			End If
			
			InsValMAG800_K = .Confirm
		End With
		
InsValMAG800_K_Err: 
		If Err.Number Then
			InsValMAG800_K = "InsValMAG800_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAG800_k: Ejecuta el post de la transacción
	'%               Tabla de presupuesto de dotación por agencia(MAG800)
	Public Function InsPostMAG800_k(ByVal sAction As String, ByVal nAgent_quan As Integer, ByVal nAgency As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAG800_k_Err
		
		With Me
			.nAgent_quan = nAgent_quan
			.nAgency = nAgency
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMAG800_k = Add
			Case "Update"
				InsPostMAG800_k = Update
			Case "Del"
				InsPostMAG800_k = Delete
		End Select
		
InsPostMAG800_k_Err: 
		If Err.Number Then
			InsPostMAG800_k = False
		End If
		On Error GoTo 0
	End Function
	
	'insValBud_AgentExist: Función que realiza la busqueda en la tabla 'insudb.Bud_agen'
	Public Function insValBud_AgentExist(ByVal nAgency As Integer) As Boolean
		
		Dim lrecVal_Bud_agen_exist As eRemoteDB.Execute
		
		insValBud_AgentExist = False
		
		On Error GoTo insValBud_AgentExist_Err
		
		lrecVal_Bud_agen_exist = New eRemoteDB.Execute
		
		With lrecVal_Bud_agen_exist
			.StoredProcedure = "Val_Bud_agen_exist"
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nCount") > 0 Then
					insValBud_AgentExist = True
				End If
			End If
		End With
		
insValBud_AgentExist_Err: 
		If Err.Number Then
			insValBud_AgentExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecVal_Bud_agen_exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecVal_Bud_agen_exist = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAgent_quan = eRemoteDB.Constants.intNull
		nAgency = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






