Option Strict Off
Option Explicit On
Public Class Adjacence
	'%-------------------------------------------------------%'
	'% $Workfile:: Adjacence.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.adjacence al 04-25-2002 17:52:20
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0    10    N
	Public nCardinal As Integer ' NUMBER     22   0     5    N
	Public sDescript As String ' CHAR       30   0     0    S
	Public sMat_divid As String ' CHAR       30   0     0    S
	Public nDistant As Double ' NUMBER     22   2     7    S
	
	'%InsUpdAdjacence: Se encarga de actualizar la tabla Adjacence
	Private Function InsUpdAdjacence(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdadjacence As eRemoteDB.Execute
		
		On Error GoTo insUpdadjacence_Err
		
		lrecinsUpdadjacence = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdadjacence al 04-25-2002 17:55:43
		'+
		With lrecinsUpdadjacence
			.StoredProcedure = "insUpdadjacence"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCardinal", nCardinal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMat_divid", sMat_divid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDistant", nDistant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdAdjacence = .Run(False)
		End With
		
insUpdadjacence_Err: 
		If Err.Number Then
			InsUpdAdjacence = False
		End If
		On Error GoTo 0
		lrecinsUpdadjacence = Nothing
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAdjacence(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdAdjacence(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAdjacence(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaadjacence As eRemoteDB.Execute
		Dim lclsreaAdjacence As Adjacence
		
		On Error GoTo reaAdjacence_Err
		
		lrecreaadjacence = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaAdjacence al 04-25-2002 17:54:39
		'+
		With lrecreaadjacence
			.StoredProcedure = "reaAdjacence"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nServ_Order = nServ_Order
				nCardinal = .FieldToClass("nCardinal")
				sDescript = .FieldToClass("sDescript")
				sMat_divid = .FieldToClass("sMat_divid")
				nDistant = .FieldToClass("nDistant")
			Else
				Find = False
			End If
		End With
		
reaAdjacence_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecreaadjacence = Nothing
		
	End Function
	
	'%InsPostOS592_5: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(OS592_5)
	Public Function InsPostOS592_5(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nCardinal As Integer, ByVal sDescript As String, ByVal sMat_divid As String, ByVal nDistant As Double) As Boolean
		
		On Error GoTo InsPostOS592_5_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nCardinal = nCardinal
			.sDescript = sDescript
			.sMat_divid = sMat_divid
			.nDistant = nDistant
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS592_5 = Add
			Case "Update"
				InsPostOS592_5 = Update
			Case "Del"
				InsPostOS592_5 = Delete
		End Select
		
InsPostOS592_5_Err: 
		If Err.Number Then
			InsPostOS592_5 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = nServ_Order
		nCardinal = nCardinal
		sDescript = sDescript
		sMat_divid = sMat_divid
		nDistant = nDistant
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






