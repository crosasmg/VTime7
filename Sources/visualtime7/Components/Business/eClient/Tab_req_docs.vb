Option Strict Off
Option Explicit On
Public Class Tab_req_docs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_req_docs.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByRef nTypedoc As Integer, ByRef sRequire As String, ByRef nQDays As Integer, ByRef nCost As Double, ByRef sStatregt As String, Optional ByRef sKey As String = "") As Tab_req_doc
		
		'- Se define variable que almacena las propiedades y metodos de la clase principal
		Dim objNewMember As Tab_req_doc
		objNewMember = New Tab_req_doc
		
		With objNewMember
			.nTypedoc = nTypedoc
			.sRequire = sRequire
			.nQDays = nQDays
			.nCost = nCost
			.sStatregt = sStatregt
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	'% Find: Busca los documentos posibles a solicitar a un cliente
	Public Function Find(Optional ByVal bFind As Boolean = False) As Boolean
		
		'- Se define la variable lrecTab_req_doc que se utilizará como cursor.
		Dim lrecTab_req_doc As eRemoteDB.Execute
		
		lrecTab_req_doc = New eRemoteDB.Execute
		On Error GoTo Find_Err
		
		Find = True
		
		'+ Se ejecuta el Store procedure que busca los documentos a solicitar
		With lrecTab_req_doc
			.StoredProcedure = "reaTab_req_doc_a"
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nTypeDoc"), .FieldToClass("sRequire"), .FieldToClass("nQDays"), .FieldToClass("nCost"), .FieldToClass("sStatregt"))
					.RNext()
				Loop 
			End If
		End With
		'UPGRADE_NOTE: Object lrecTab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_req_doc = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_req_doc
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Se usa para obtener el numero de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Obtiene un item de la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'------------------------------------------------------------
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Se usa para remover elementos de la colección
	'------------------------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'------------------------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Crea la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'------------------------------------------------------------
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'------------------------------------------------------------
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






