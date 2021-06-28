Option Strict Off
Option Explicit On
Public Class ErrFuncts
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	
	'% Add: Añade los Registros de la tabla ErrFunct asociados al error en tratamiento a la colección.
	Public Function Add(ByVal nErrornum As Integer, ByVal nFunctspec As Short, ByVal nStatrequest As Short, ByVal sVersion As String, ByVal sDs_Text As String) As ErrFunct
		
		'create a new object
		Dim objNewMember As ErrFunct
		objNewMember = New ErrFunct
		
		With objNewMember
			.nErrornum = nErrornum
			.nFunctspec = nFunctspec
			.nStatrequest = nStatrequest
			.sVersion = sVersion
			.sDs_Text = sDs_Text
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
	End Function
	
	
	'%Find:Levanta el Recordset con los registros de la tabla ErrFunct asociados al error en tratamiento
	Public Function Find(ByVal nErrornum As Integer) As Boolean
		Dim lrecreaErrFunct As eRemoteDB.Execute
		
		lrecreaErrFunct = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaErrFunct'
		mCol = Nothing
		mCol = New Collection
		
		With lrecreaErrFunct
			.StoredProcedure = "reaErrFunct"
			.Parameters.Add("nErrorNum", nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nFunctspec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				While Not .EOF
					Call Add(.FieldToClass("nErrorNum"), .FieldToClass("nFunctspec"), .FieldToClass("nStatrequest"), .FieldToClass("sVersion"), .FieldToClass("tDs_text"))
					.RNext()
				End While
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lrecreaErrFunct = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ErrFunct
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
	
	
	
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











