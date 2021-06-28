Option Strict Off
Option Explicit On
Public Class Add_risk
	'%-------------------------------------------------------%'
	'% $Workfile:: Add_risk.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.add_risk al 04-25-2002 15:56:33
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0    10    N
	Public nCon_earthquake As Integer ' NUMBER     22   0     5    N
	Public nDamage As Integer ' NUMBER     22   0     5    N
	Public nContainrisk As Integer ' NUMBER     22   0     5    N
	Public sRiverbed As String ' CHAR       1    0     0    N
	Public nDist_river As Double ' NUMBER     22   2     7    S
	Public sInflu_risk As String ' CHAR       1    0     0    N
	Public nInundat As Integer ' NUMBER     22   0     5    N
	Public sStratobj As String ' CHAR       1    0     0    N
	Public sTerrefy As String ' CHAR       1    0     0    N
	Public nWaterpipe As Integer ' NUMBER     22   0     5    S
	Public nDam_waterpipe As Integer ' NUMBER     22   0     5    S
	Public nSewerpipe As Integer ' NUMBER     22   0     5    S
	Public nDam_sewerpipe As Integer ' NUMBER     22   0     5    S
	Public nStatroof As Integer ' NUMBER     22   0     5    N
	Public nDamroof As Integer ' NUMBER     22   0     5    N
	Public sStorm As String ' CHAR       1    0     0    N
	Public sSnow As String ' CHAR       1    0     0    N
	Public sShockauto As String ' CHAR       1    0     0    N
	Public sFallplane As String ' CHAR       1    0     0    N
	Public sWind As String ' CHAR       1    0     0    N
	Public sAirport As String ' CHAR       1    0     0    N
	Public nDistair As Double ' NUMBER     22   2     7    S
	Public sSea As String ' CHAR       1    0     0    N
	Public nDistsea As Double ' NUMBER     22   2     7    S
	
	'%InsUpdAdd_risk: Se encarga de actualizar la tabla Add_risk
	Private Function InsUpdAdd_risk(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdadd_risk As eRemoteDB.Execute
		Dim lclsinsUpdadd_risk As Add_risk
		
		On Error GoTo insUpdadd_risk_Err
		
		lrecinsUpdadd_risk = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdadd_risk al 04-25-2002 16:04:41
		'+
		With lrecinsUpdadd_risk
			.StoredProcedure = "insUpdadd_risk"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCon_earthquake", nCon_earthquake, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage", nDamage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContainrisk", nContainrisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRiverbed", sRiverbed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDist_river", nDist_river, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInflu_risk", sInflu_risk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInundat", nInundat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStratobj", sStratobj, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTerrefy", sTerrefy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWaterpipe", nWaterpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDam_waterpipe", nDam_waterpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSewerpipe", nSewerpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDam_sewerpipe", nDam_sewerpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatroof", nStatroof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamroof", nDamroof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStorm", sStorm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSnow", sSnow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShockauto", sShockauto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFallplane", sFallplane, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWind", sWind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAirport", sAirport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDistair", nDistair, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSea", sSea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDistsea", nDistsea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdAdd_risk = .Run(False)
		End With
		
insUpdadd_risk_Err: 
		If Err.Number Then
			InsUpdAdd_risk = False
		End If
		lrecinsUpdadd_risk = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAdd_risk(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdAdd_risk(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAdd_risk(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaAdd_risk As eRemoteDB.Execute
		Dim lclsAdd_risk As Add_risk
		
		On Error GoTo reaAdd_risk_Err
		
		lrecreaAdd_risk = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaAdd_risk al 04-25-2002 16:02:43
		'+
		With lrecreaAdd_risk
			.StoredProcedure = "reaAdd_risk"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nServ_Order = nServ_Order
				nCon_earthquake = .FieldToClass("nCon_earthquake")
				nDamage = .FieldToClass("nDamage")
				nContainrisk = .FieldToClass("nContainrisk")
				sRiverbed = .FieldToClass("sRiverbed")
				nDist_river = .FieldToClass("nDist_river")
				sInflu_risk = .FieldToClass("sInflu_risk")
				nInundat = .FieldToClass("nInundat")
				sStratobj = .FieldToClass("sStratobj")
				sTerrefy = .FieldToClass("sTerrefy")
				nWaterpipe = .FieldToClass("nWaterpipe")
				nDam_waterpipe = .FieldToClass("nDam_waterpipe")
				nSewerpipe = .FieldToClass("nSewerpipe")
				nDam_sewerpipe = .FieldToClass("nDam_sewerpipe")
				nStatroof = .FieldToClass("nStatroof")
				nDamroof = .FieldToClass("nDamroof")
				sStorm = .FieldToClass("sStorm")
				sSnow = .FieldToClass("sSnow")
				sShockauto = .FieldToClass("sShockauto")
				sFallplane = .FieldToClass("sFallplane")
				sWind = .FieldToClass("sWind")
				sAirport = .FieldToClass("sAirport")
				nDistair = .FieldToClass("nDistair")
				sSea = .FieldToClass("sSea")
				nDistsea = .FieldToClass("nDistsea")
			Else
				Find = False
			End If
		End With
		
reaAdd_risk_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaAdd_risk = Nothing
		On Error GoTo 0
		
	End Function
	'%InsPostOS592_4: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(OS592_4)
	Public Function InsPostOS592_4(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nCon_earthquake As Integer, ByVal nDamage As Integer, ByVal nContainrisk As Integer, ByVal sRiverbed As String, ByVal nDist_river As Double, ByVal sInflu_risk As String, ByVal nInundat As Integer, ByVal sStratobj As String, ByVal sTerrefy As String, ByVal nWaterpipe As Integer, ByVal nDam_waterpipe As Integer, ByVal nSewerpipe As Integer, ByVal nDam_sewerpipe As Integer, ByVal nStatroof As Integer, ByVal nDamroof As Integer, ByVal sStorm As String, ByVal sSnow As String, ByVal sShockauto As String, ByVal sFallplane As String, ByVal sWind As String, ByVal sAirport As String, ByVal nDistair As Double, ByVal sSea As String, ByVal nDistsea As Double) As Boolean
		
		On Error GoTo InsPostOS592_4_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nCon_earthquake = nCon_earthquake
			.nDamage = nDamage
			.nContainrisk = nContainrisk
			.sRiverbed = IIf(sRiverbed = "1", sRiverbed, "2")
			.nDist_river = nDist_river
			.sInflu_risk = IIf(sInflu_risk = "1", sInflu_risk, "2")
			.nInundat = nInundat
			.sStratobj = IIf(sStratobj = "1", sStratobj, "2")
			.sTerrefy = IIf(sTerrefy = "1", sTerrefy, "2")
			.nWaterpipe = nWaterpipe
			.nDam_waterpipe = nDam_waterpipe
			.nSewerpipe = nSewerpipe
			.nDam_sewerpipe = nDam_sewerpipe
			.nStatroof = nStatroof
			.nDamroof = nDamroof
			.sStorm = IIf(sStorm = "1", sStorm, "2")
			.sSnow = IIf(sSnow = "1", sSnow, "2")
			.sShockauto = IIf(sShockauto = "1", sShockauto, "2")
			.sFallplane = IIf(sFallplane = "1", sFallplane, "2")
			.sWind = IIf(sWind = "1", sWind, "2")
			.sAirport = IIf(sAirport = "1", sAirport, "2")
			.nDistair = nDistair
			.sSea = IIf(sSea = "1", sSea, "2")
			.nDistsea = nDistsea
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS592_4 = Add
			Case "Update"
				InsPostOS592_4 = Update
			Case "Del"
				InsPostOS592_4 = Delete
		End Select
		
InsPostOS592_4_Err: 
		If Err.Number Then
			InsPostOS592_4 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nCon_earthquake = eRemoteDB.Constants.intNull
		nDamage = eRemoteDB.Constants.intNull
		nContainrisk = eRemoteDB.Constants.intNull
		sRiverbed = CStr(eRemoteDB.Constants.intNull)
		nDist_river = eRemoteDB.Constants.intNull
		sInflu_risk = CStr(eRemoteDB.Constants.intNull)
		nInundat = eRemoteDB.Constants.intNull
		sStratobj = CStr(eRemoteDB.Constants.intNull)
		sTerrefy = CStr(eRemoteDB.Constants.strNull)
		nWaterpipe = eRemoteDB.Constants.intNull
		nDam_waterpipe = eRemoteDB.Constants.intNull
		nSewerpipe = eRemoteDB.Constants.intNull
		nDam_sewerpipe = eRemoteDB.Constants.intNull
		nStatroof = eRemoteDB.Constants.intNull
		nDamroof = eRemoteDB.Constants.intNull
		sStorm = CStr(eRemoteDB.Constants.strNull)
		sSnow = CStr(eRemoteDB.Constants.strNull)
		sShockauto = CStr(eRemoteDB.Constants.strNull)
		sFallplane = CStr(eRemoteDB.Constants.strNull)
		sWind = CStr(eRemoteDB.Constants.strNull)
		sAirport = CStr(eRemoteDB.Constants.strNull)
		nDistair = eRemoteDB.Constants.intNull
		sSea = CStr(eRemoteDB.Constants.strNull)
		nDistsea = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






