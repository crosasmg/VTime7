Option Strict Off
Option Explicit On
Public Class Contac_cli
	
	'%-------------------------------------------------------%'
	'% $Workfile:: Contac_cli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 11,2000
	'+ Propiedades según la tabla en el sistema 11/01/2000
	'**+ The key fields corresponds to a sClient, dFinanDate and nConcept.
	'+ Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'+ Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public sClient As String 'char      no        14                 no       yes                no
	Public sClientr As String 'char      no        14                 no       yes                no
	Public dEffecdate As Date 'datetime  no        8                  no       (n/a)              (n/a)
	Public nOrder As Integer 'int       no        4      10    0     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime  no        8                  no       (n/a)              (n/a)
	Public nUsercode As Integer ' NUMBER   22   0     5    N
	Public nPosition As Integer 'int       no        4      10    0     yes      (n/a)              (n/a)
	
	'**+ Additional properties
	'+ Propiedades auxiliares
	Public sCliename As String 'Descripción del cliente
	Public sNewClientr As String 'Variable temporal que guarda el nuevo cod. de cliente
	
	'**+ Define the variable that contains the status of each instance of the class,
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	'**% Find: search for a specific client, year and concept.
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal sClient As String, ByVal sClientr As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaContac_cli As eRemoteDB.Execute
		
		lrecreaContac_cli = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If sClient = Me.sClient And sClientr = Me.sClientr And dEffecdate = Me.dEffecdate And Not bFind Then
			Find = True
		Else
			With lrecreaContac_cli
				.StoredProcedure = "reaContac_cli_bc"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sClient = .FieldToClass("sClient")
					sClientr = .FieldToClass("sClientr")
					dEffecdate = .FieldToClass("dEffecdate")
					nOrder = .FieldToClass("nOrder")
					dNulldate = .FieldToClass("dNulldate")
					nUsercode = .FieldToClass("nUsercode")
					nPosition = .FieldToClass("nPosition")
					sCliename = .FieldToClass("sCliename")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaContac_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaContac_cli = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add: Add client contacts information.
	'% Add: Agrega los datos correspondientes para los contactos el cliente
	Public Function Add() As Boolean
		Dim lrecinsContac_cli As eRemoteDB.Execute
		
		lrecinsContac_cli = New eRemoteDB.Execute
		On Error GoTo Add_Err
		
		With lrecinsContac_cli
			.StoredProcedure = "insContac_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNewClientr", sNewClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsContac_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsContac_cli = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: Updates data for a specific client, year and concept
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Dim lrecupdContac_cli As eRemoteDB.Execute
		
		lrecupdContac_cli = New eRemoteDB.Execute
		On Error GoTo Update_Err
		
		With lrecupdContac_cli
			.StoredProcedure = "updContac_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sNewClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdContac_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContac_cli = Nothing
		
		If Update Then
			Update = Add
		End If
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: Deletes data for a specific client, year and concept.
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Dim lrecupdContac_cli As eRemoteDB.Execute
		
		lrecupdContac_cli = New eRemoteDB.Execute
		On Error GoTo Delete_Err
		
		With lrecupdContac_cli
			.StoredProcedure = "updContac_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sNewClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdContac_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContac_cli = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'*** Class_Initialize: the objective of this routine is control the opening of the class
	'* Class_Initialize: el objetivo de esta rutina es la de controlar la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.sClient = String.Empty
		Me.sClientr = String.Empty
		Me.dEffecdate = eRemoteDB.Constants.dtmNull
		Me.nOrder = eRemoteDB.Constants.intNull
		Me.dNulldate = eRemoteDB.Constants.dtmNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nPosition = eRemoteDB.Constants.intNull
		Me.sCliename = String.Empty
		Me.sNewClientr = String.Empty
		Me.nStatusInstance = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Find: search for a specific client, year and concepet.
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function FindOrder(ByVal sClient As String, ByVal nOrder As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaOrderContac_Cli As eRemoteDB.Execute
		
		lrecreaOrderContac_Cli = New eRemoteDB.Execute
		On Error GoTo FindOrder_Err
		
		If sClient = Me.sClient And CDbl(nOrder) = Me.nOrder And Not bFind Then
			FindOrder = True
		Else
			With lrecreaOrderContac_Cli
				.StoredProcedure = "reaOrderContac_Cli"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sClient = .FieldToClass("sClient")
					sClientr = .FieldToClass("sClientr")
					dEffecdate = .FieldToClass("dEffecdate")
					nOrder = .FieldToClass("nOrder")
					dNulldate = .FieldToClass("dNulldate")
					nUsercode = .FieldToClass("nUsercode")
					nPosition = .FieldToClass("nPosition")
					sCliename = .FieldToClass("sCliename")
					FindOrder = True
				Else
					FindOrder = False
				End If
			End With
		End If
		
FindOrder_Err: 
		If Err.Number Then FindOrder = False
		'UPGRADE_NOTE: Object lrecreaOrderContac_Cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOrderContac_Cli = Nothing
		On Error GoTo 0
	End Function
End Class






