Option Strict Off
Option Explicit On
Public Class Client_IdDocs
	Implements System.Collections.IEnumerable
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	'+
	'+ Estructura de tabla Client_IdDoc al 02-01-2006 18:31:01
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Private mstrClient As String
	Private mintIddoc_type As Integer
	Private mstrIddoc As String
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Client_IdDoc) As Client_IdDoc
		If objClass Is Nothing Then
			objClass = New Client_IdDoc
		End If
		
		With objClass
			mCol.Add(objClass)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Client_IdDoc
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
	
	'%Find: Lee los datos de la tabla
	Public Function FindClient(ByVal sClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecrea_cur_sclient As eRemoteDB.Execute
		Dim lclsClient_IdDoc As Client_IdDoc
		
		On Error GoTo rea_cur_sclient_Err
		
		lrecrea_cur_sclient = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure rea_cur_sclient al 02-02-2006 15:42:42
		'+
		With lrecrea_cur_sclient
			.StoredProcedure = "Client_IdDoc_SQLpkg.rea_cur_sclient"
			.Parameters.Add("p_sclient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindClient = True
				Do While Not .EOF
					lclsClient_IdDoc = New Client_IdDoc
					lclsClient_IdDoc.sClient = .FieldToClass("sClient")
					lclsClient_IdDoc.nIddoc_type = .FieldToClass("nIddoc_type")
					lclsClient_IdDoc.sIddoc = .FieldToClass("sIddoc")
					lclsClient_IdDoc.sIddoc_digit = .FieldToClass("sIddoc_digit")
					lclsClient_IdDoc.nUsercode = .FieldToClass("nUsercode")
					
					Call Add(lclsClient_IdDoc)
					'UPGRADE_NOTE: Object lclsClient_IdDoc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClient_IdDoc = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindClient = False
			End If
		End With
		
rea_cur_sclient_Err: 
		If Err.Number Then
			FindClient = False
		End If
		'UPGRADE_NOTE: Object lrecrea_cur_sclient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecrea_cur_sclient = Nothing
		On Error GoTo 0
	End Function
	
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
End Class






