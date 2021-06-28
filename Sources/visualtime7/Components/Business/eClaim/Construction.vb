Option Strict Off
Option Explicit On
Public Class Construction
	'%-------------------------------------------------------%'
	'% $Workfile:: Construction.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+  Property                    Type             DBType   Size Scale Prec  Null
	'+-----------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0     10   N
	Public nArea As Double ' NUMBER     22   2     7    N
	Public nOldness As Integer ' NUMBER     22   0     2    S
	Public nSta_local As Integer ' NUMBER     22   0     1    N
	Public nStructure_wall As Integer ' NUMBER     22   0     5    S
	Public nStruct_wallint As Integer ' NUMBER     22   0     5    S
	Public nRooftype As Integer ' NUMBER     22   0     5    S
	Public nStructure_type As Integer ' NUMBER     22   0     5    S
	Public nStruct_mezz As Integer ' NUMBER     22   0     5    S
	Public nSideclosetype As Integer ' NUMBER     22   0     5    S
	Public sSubway As String ' CHAR       1    0     0    N
	Public nFloor As Integer ' NUMBER     22   0     3    S
	Public nTotalfloor As Integer ' NUMBER     22   0     3    S
	'%InsUpdConstruction: Se encarga de actualizar la tabla Construction
	Private Function InsUpdConstruction(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdconstruction As eRemoteDB.Execute
		
		On Error GoTo insUpdconstruction_Err
		
		lrecinsUpdconstruction = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdconstruction al 04-23-2002 11:31:14
		With lrecinsUpdconstruction
			.StoredProcedure = "insUpdconstruction"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOldness", nOldness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_local", nSta_local, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStructure_wall", nStructure_wall, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStruct_wallint", nStruct_wallint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRooftype", nRooftype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStructure_type", nStructure_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStruct_mezz", nStruct_mezz, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSideclosetype", nSideclosetype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSubway", sSubway, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFloor", nFloor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalfloor", nTotalfloor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdConstruction = .Run(False)
		End With
		
insUpdconstruction_Err: 
		If Err.Number Then
			InsUpdConstruction = False
		End If
		lrecinsUpdconstruction = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdConstruction(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdConstruction(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdConstruction(3)
	End Function
	
	'%InsPostOS592_1: Ejecuta el post de la transacción
	'%                Tabla Construction(OS592_1)
	Public Function InsPostOS592_1(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nArea As Double, ByVal nOldness As Integer, ByVal nSta_local As Integer, ByVal nStructure_wall As Integer, ByVal nStruct_wallint As Integer, ByVal nRooftype As Integer, ByVal nStructure_type As Integer, ByVal nStruct_mezz As Integer, ByVal nSideclosetype As Integer, ByVal sSubway As String, ByVal nFloor As Integer, ByVal nTotalfloor As Integer) As Boolean
		
		On Error GoTo InsPostOS592_1_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nArea = nArea
			.nOldness = nOldness
			.nSta_local = nSta_local
			.nStructure_wall = nStructure_wall
			.nStruct_wallint = nStruct_wallint
			.nRooftype = nRooftype
			.nStructure_type = nStructure_type
			.nStruct_mezz = nStruct_mezz
			.nSideclosetype = nSideclosetype
			.sSubway = sSubway
			.nFloor = nFloor
			.nTotalfloor = nTotalfloor
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS592_1 = Add
			Case "Update"
				InsPostOS592_1 = Update
			Case "Del"
				InsPostOS592_1 = Delete
		End Select
		
InsPostOS592_1_Err: 
		If Err.Number Then
			InsPostOS592_1 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaConstruction As eRemoteDB.Execute
		Dim lclsConstruction As Construction
		
		On Error GoTo reaConstruction_Err
		
		lrecreaConstruction = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaConstruction al 04-23-2002 11:33:22
		With lrecreaConstruction
			.StoredProcedure = "reaConstruction"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nServ_Order = nServ_Order
				nArea = .FieldToClass("nArea")
				nOldness = .FieldToClass("nOldness")
				nSta_local = .FieldToClass("nSta_local")
				nStructure_wall = .FieldToClass("nStructure_wall")
				nStruct_wallint = .FieldToClass("nStruct_wallint")
				nRooftype = .FieldToClass("nRooftype")
				nStructure_type = .FieldToClass("nStructure_type")
				nStruct_mezz = .FieldToClass("nStruct_mezz")
				nSideclosetype = .FieldToClass("nSideclosetype")
				sSubway = .FieldToClass("sSubway")
				nFloor = .FieldToClass("nFloor")
				nTotalfloor = .FieldToClass("nTotalfloor")
			Else
				Find = False
			End If
		End With
		
reaConstruction_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaConstruction = Nothing
		On Error GoTo 0
		
	End Function
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nArea = eRemoteDB.Constants.intNull
		nOldness = eRemoteDB.Constants.intNull
		nSta_local = eRemoteDB.Constants.intNull
		nStructure_wall = eRemoteDB.Constants.intNull
		nStruct_wallint = eRemoteDB.Constants.intNull
		nRooftype = eRemoteDB.Constants.intNull
		nStructure_type = eRemoteDB.Constants.intNull
		nStruct_mezz = eRemoteDB.Constants.intNull
		nSideclosetype = eRemoteDB.Constants.intNull
		sSubway = "S"
		nFloor = eRemoteDB.Constants.intNull
		nTotalfloor = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






