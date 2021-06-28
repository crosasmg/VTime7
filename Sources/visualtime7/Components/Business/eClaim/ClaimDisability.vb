Option Strict Off
Option Explicit On
Public Class ClaimDisability
	'%-------------------------------------------------------%'
	'% $Workfile:: ClaimDisability.cls                      $%'
	'% $Author:: Jperez                                     $%'
	'% $Date:: 3-01-12 15:14                                $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	Public nExist As Short
	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public sClient As String
	Public nModulec As Integer
	Public nCover As Integer
	Public nCovergen As Integer
	Public nDisability As Integer
	Public nRate As Double
	Public nUsercode As Integer
	
	'%InsUpdTar_Disability: Se encarga de actualizar la tabla
	Private Function insClaimDisability(ByVal nAction As Integer) As Boolean
		Dim lrecinsClaimDisability As eRemoteDB.Execute
		
		On Error GoTo insClaimDisability_Err
		
		lrecinsClaimDisability = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdTar_Disability'
		'+Información leída el 25/10/01
		With lrecinsClaimDisability
			.StoredProcedure = "insClaimDisability"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisability", nDisability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insClaimDisability = .Run(False) 

		End With
		
insClaimDisability_Err: 
		If Err.Number Then
			insClaimDisability = False
		End If
		lrecinsClaimDisability = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Registra un registro en la tabla
	Public Function Add() As Boolean
		Add = insClaimDisability(1)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = insClaimDisability(2)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecClaimDisability As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nClaim <> nClaim Or Me.nCase_num <> nCase_num Or Me.nDeman_type <> nDeman_type Then
			
			lrecClaimDisability = New eRemoteDB.Execute
			
			Me.nClaim = nClaim
			Me.nCase_num = nCase_num
			Me.nDeman_type = nDeman_type
			
			'+Definición de parámetros para stored procedure 'ReaTar_Disability'
			'+Información leída el 25/10/01
			With lrecClaimDisability
				.StoredProcedure = "reaClaimDisability"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nCovergen = .FieldToClass("nCovergen")
					Me.nDisability = .FieldToClass("nDisability")
					Me.nRate = .FieldToClass("nRate")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecClaimDisability = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostSI024D: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(SI024D)
	Public Function insPostSI024D(ByVal sAction As String, ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCovergen As Integer, ByVal nDisability As Integer, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostSI024D_Err
		
		With Me
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.nCovergen = nCovergen
			.nDisability = nDisability
			.nRate = nRate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostSI024D = Add
			Case "Del"
				insPostSI024D = Delete
		End Select
		
InsPostSI024D_Err: 
		If Err.Number Then
			insPostSI024D = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nClaim = eRemoteDB.Constants.intNull
		nCase_num = eRemoteDB.Constants.intNull
		nDeman_type = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nDisability = eRemoteDB.Constants.intNull
		nRate = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'insCalPercentDisability: Calcula el porcentaje total de indemnización por invalidez
	Public Function insCalPercentDisability(ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Double
		Dim lrecClaimDisability As eRemoteDB.Execute
		
		On Error GoTo insCalPercentDisability_Err
		
		lrecClaimDisability = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTar_Disability'
		'+Información leída el 25/10/01
		With lrecClaimDisability
			.StoredProcedure = "insCalPercentDisability"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insCalPercentDisability = .FieldToClass("nPercent")
				.RCloseRec()
			End If
		End With
		
insCalPercentDisability_Err: 
		If Err.Number Then
			insCalPercentDisability = 0
		End If
		lrecClaimDisability = Nothing
		On Error GoTo 0
	End Function
End Class






