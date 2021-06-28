Option Strict Off
Option Explicit On
Public Class Fire_risk
	'%-------------------------------------------------------%'
	'% $Workfile:: Fire_risk.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla en el sistema al 14/10/2002
	
	'+ Property                Type
	'+------------------------ ----------------------
	Public nServ_Order As Double ' NUMBER
	Public nElecProt As Integer ' NUMBER
	Public nElecStat As Integer ' NUMBER
	Public nUsercode As Integer ' NUMBER
	
	'% insUpdFire_risk: Se encarga de actualizar la tabla Fire_risk
	Private Function insUpdFire_risk() As Boolean
		Dim lrecinsUpdfire_risk As eRemoteDB.Execute
		
		On Error GoTo insUpdfire_risk_Err
		
		lrecinsUpdfire_risk = New eRemoteDB.Execute
		
		With lrecinsUpdfire_risk
			.StoredProcedure = "insUpdfire_risk"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElecprot", nElecProt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElecstat", nElecStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdFire_risk = .Run(False)
		End With
		
insUpdfire_risk_Err: 
		If Err.Number Then
			insUpdFire_risk = False
		End If
		lrecinsUpdfire_risk = Nothing
		On Error GoTo 0
	End Function
	
	'% Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = insUpdFire_risk()
	End Function
	
	'% insPostOS592_2: se actualizan los datos en la tabla
	Public Function InsPostOS592_2(ByVal nServ_Order As Double, ByVal nElecProt As Integer, ByVal nElecStat As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostOS592_2_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nElecProt = nElecProt
			.nElecStat = nElecStat
			.nUsercode = nUsercode
		End With
		
		InsPostOS592_2 = Update
		
InsPostOS592_2_Err: 
		If Err.Number Then
			InsPostOS592_2 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: Lee los datos de la tabla asociados a la orden de servicio
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaFire_risk As eRemoteDB.Execute
		Dim lclsFire_Risk As Fire_risk
		
		On Error GoTo reaFire_risk_Err
		
		lrecreaFire_risk = New eRemoteDB.Execute
		
		With lrecreaFire_risk
			.StoredProcedure = "reaFire_risk"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Me.nServ_Order = .FieldToClass("nServ_order")
				nElecProt = .FieldToClass("nElecprot")
				nElecStat = .FieldToClass("nElecstat")
				Find = True
			End If
		End With
		
reaFire_risk_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaFire_risk = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nElecProt = eRemoteDB.Constants.intNull
		nElecStat = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






