Option Strict Off
Option Explicit On
Public Class Tab_relats
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_relats.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variable local para contener colección
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tab_relat) As Tab_relat
		If objClass Is Nothing Then
			objClass = New Tab_relat
		End If
		
		With objClass
			mCol.Add(objClass, "BC" & .nRelaship)
			
		End With
		
		Add = objClass
	End Function
	
	'**% Find: Searches for the data of  a client
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find() As Boolean
		'- Se define variable que almacena las propiedades y metodos de la clase principal
		Dim lclsTar_relat As Tab_relat
		Dim lrecreaTab_relat2 As eRemoteDB.Execute
		
		'- Se define variable para realizar operaciones a la BD
		lrecreaTab_relat2 = New eRemoteDB.Execute
		With lrecreaTab_relat2
			.StoredProcedure = "reaTab_relat2"
			If .Run Then
				Do While Not .EOF
					lclsTar_relat = New Tab_relat
					lclsTar_relat.nRelaship = .FieldToClass("nRelaship")
					lclsTar_relat.nRel_target = .FieldToClass("nRel_target")
					lclsTar_relat.sStatregt = .FieldToClass("sStatregt")
					lclsTar_relat.sExist = .FieldToClass("sExist")
					Call Add(lclsTar_relat)
					'UPGRADE_NOTE: Object lclsTar_relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTar_relat = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaTab_relat2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_relat2 = Nothing
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_relat
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: enumera los elementos dentro de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: elimina la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






