Option Strict Off
Option Explicit On
Public Class Tab_prov_zones
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_prov_zones.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Public Function Add(ByVal objClass As Tab_prov_zone) As Tab_prov_zone
		'create a new object
		If objClass Is Nothing Then
			objClass = New Tab_prov_zone
		End If
		
		With objClass
			mCol.Add(objClass, .nProvider & .nZone)
		End With
		
		'return the object created
		Add = objClass
		objClass = Nothing
		
	End Function
	
	'%Find: Lee los datos de la tabla para la transacción MSI647 (Zonas asociadas a un proveedor)
	Public Function Find(ByVal nProvider As Integer) As Boolean
		Dim lrecReaTab_prov_zone_a As eRemoteDB.Execute
		Dim lclsTab_prov_zone As Tab_prov_zone
		lrecReaTab_prov_zone_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+Definición de parámetros para stored procedure 'ReaTab_prov_zone_a'
		'+Información leída el 02/04/2002
		With lrecReaTab_prov_zone_a
			.StoredProcedure = "ReaTab_prov_zone_a"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_prov_zone = New Tab_prov_zone
					lclsTab_prov_zone.nProvider = .FieldToClass("nProvider")
					lclsTab_prov_zone.nZone = .FieldToClass("nZone")
					lclsTab_prov_zone.nOrder = .FieldToClass("nOrder")
					Call Add(lclsTab_prov_zone)
					.RNext()
					lclsTab_prov_zone = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecReaTab_prov_zone_a = Nothing
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_prov_zone
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
End Class






