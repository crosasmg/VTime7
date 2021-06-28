Option Strict Off
Option Explicit On
Public Class Errors
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsT_Err_Interface As ErrorTyp) As ErrorTyp
		mCol.Add(lclsT_Err_Interface)
		
		'+ Devolver el objeto creado
		Add = lclsT_Err_Interface
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ErrorTyp
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
	
	'%Find : Función se encarga de de buscar la colección de datos en t_err_interface
	Public Function Find(ByVal sKey As String) As Boolean
		Dim lrecReaT_Err_Interface As eRemoteDB.Execute
		Dim lclsT_Err_Interface As ErrorTyp
		
		On Error GoTo reaT_Err_Interface_Err
		
		lrecReaT_Err_Interface = New eRemoteDB.Execute
		
		With lrecReaT_Err_Interface
			.StoredProcedure = "reat_err_interface"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsT_Err_Interface = New ErrorTyp
					
					lclsT_Err_Interface.nRow = .FieldToClass("nRow")
					lclsT_Err_Interface.nSeq = .FieldToClass("nSeq")
					lclsT_Err_Interface.nError = .FieldToClass("nError")
					lclsT_Err_Interface.sDescript = .FieldToClass("sDescript")
					
					Call Add(lclsT_Err_Interface)
					'UPGRADE_NOTE: Object lclsT_Err_Interface may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsT_Err_Interface = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
reaT_Err_Interface_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaT_Err_Interface may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaT_Err_Interface = Nothing
	End Function
End Class






