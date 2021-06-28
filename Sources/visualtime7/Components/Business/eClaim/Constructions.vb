Option Strict Off
Option Explicit On
Public Class Constructions
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Constructions.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsConstruction As Construction) As Construction
		mCol.Add(lclsConstruction)
		
		'+ Devolver el objeto creado
		Add = lclsConstruction
	End Function
	'%Item: Obtiene un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Construction
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	'%Count: Obtiene la cantidad de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	'%NewEnum: Permite hacer el recorrido de la colección a través del For each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	'%Class_Initialize: Controla la creación de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'%Class_Terminate: Controla la destrucción de la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






