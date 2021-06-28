Option Strict Off
Option Explicit On
Public Class Adjacences
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Adjacences.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsadjacence As Adjacence) As Adjacence
		mCol.Add(lclsadjacence)
		
		'+ Devolver el objeto creado
		Add = lclsadjacence
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Adjacence
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
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
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
	
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a el ramo, producto, modulo, cobertura y fecha
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaadjacence As eRemoteDB.Execute
		Dim lclsadjacence As Adjacence
		
		On Error GoTo reaAdjacence_Err
		
		lrecreaadjacence = New eRemoteDB.Execute
		
		With lrecreaadjacence
			.StoredProcedure = "reaAdjacence"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsadjacence = New Adjacence
					lclsadjacence.nServ_Order = nServ_Order
					lclsadjacence.nCardinal = .FieldToClass("nCardinal")
					lclsadjacence.sDescript = .FieldToClass("sDescript")
					lclsadjacence.sMat_divid = .FieldToClass("sMat_divid")
					lclsadjacence.nDistant = .FieldToClass("nDistant")
					Call Add(lclsadjacence)
					lclsadjacence = Nothing
					.RNext()
				Loop 
				Find = True
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
End Class






