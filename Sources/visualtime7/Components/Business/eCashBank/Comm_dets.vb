Option Strict Off
Option Explicit On
Public Class Comm_dets
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Comm_dets.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**-local variable to hold collection
	'-se define la variable mCol, para contener la coleccion
	
	Private mCol As Collection
	
	'**%Add: adds a new instance of the "Comm_det" class to the collection
	'%Add: Añade una nueva instancia de la clase "Comm_det" a la colección
	Public Function Add(ByVal nTyp_acco As Integer, ByVal nIdconsec As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal dOperdate As Date, ByVal dCompdate As Date, ByVal sType_acc As String, ByVal nReceipt As Integer, ByVal nAcco_Key As Integer, ByVal nType_tran As Integer, ByVal nIntermed As Integer, ByVal sInd_credeb As String, ByVal nProvince As Integer, ByVal nTyp_amount As Integer, ByVal sClient As String, ByVal nAmount As Double, ByVal nUsercode As Integer) As Comm_det
		'**+ create a new object
		'*+Se crea on objeto nuevo
		
		Dim objNewMember As Comm_det
		objNewMember = New Comm_det
		
		With objNewMember
			.nTyp_acco = nTyp_acco
			.nIdconsec = nIdconsec
			.nBranch = nBranch
			.nProduct = nProduct
			.nCurrency = nCurrency
			.dOperdate = dOperdate
			.dCompdate = dCompdate
			.sType_acc = sType_acc
			.nReceipt = nReceipt
			.nAcco_Key = nAcco_Key
			.nType_tran = nType_tran
			.nIntermed = nIntermed
			.sInd_credeb = sInd_credeb
			.nProvince = nProvince
			.nTyp_amount = nTyp_amount
			.sClient = sClient
			.nAmount = nAmount
			.nUsercode = nUsercode
		End With
		
		'**+ set the properties passed into the method
		'+se genera un nuevo elemento en la coleccion
		
		mCol.Add(objNewMember)
		
		'**+ returns the object created
		'+Se devuelve el objeto recien creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Comm_det
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






