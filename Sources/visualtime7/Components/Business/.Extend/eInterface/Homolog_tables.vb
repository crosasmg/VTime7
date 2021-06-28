Option Strict Off
Option Explicit On
Public Class Homolog_tables
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclshomolog_table As Homolog_table) As Homolog_table
		mCol.Add(lclshomolog_table)
		
		'+ Devolver el objeto creado
		Add = lclshomolog_table
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Homolog_table
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a el ramo, producto, modulo, cobertura y fecha
	Public Function Find(ByVal nSystem As Integer, ByVal nTable As Integer) As Boolean
		Dim lrecreahomolog_table As eRemoteDB.Execute
		Dim lclshomolog_table As Homolog_table
		
		On Error GoTo reahomolog_table_Err
		
		lrecreahomolog_table = New eRemoteDB.Execute
		
		With lrecreahomolog_table
			.StoredProcedure = "reahomolog_table"
			.Parameters.Add("nSystem", nSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclshomolog_table = New Homolog_table
					
					lclshomolog_table.nSystem = nSystem
					lclshomolog_table.nTable = nTable
					lclshomolog_table.nId = .FieldToClass("nId")
					lclshomolog_table.sColumnName_Vt = .FieldToClass("sColumnname_Vt")
					lclshomolog_table.sCodValue_Vt = .FieldToClass("sCodvalue_Vt")
					lclshomolog_table.sValue_Vt = .FieldToClass("sValue_Vt")
					lclshomolog_table.sTableName = .FieldToClass("sTablename")
					lclshomolog_table.sColumnName = .FieldToClass("sColumnname")
					lclshomolog_table.sCodValue = .FieldToClass("sCodvalue")
					lclshomolog_table.sPredom = .FieldToClass("sPredom")
					
					Call Add(lclshomolog_table)
					'UPGRADE_NOTE: Object lclshomolog_table may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclshomolog_table = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
reahomolog_table_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreahomolog_table may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreahomolog_table = Nothing
	End Function
End Class






