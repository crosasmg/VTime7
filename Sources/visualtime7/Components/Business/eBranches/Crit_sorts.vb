Option Strict Off
Option Explicit On
Public Class Crit_sorts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Crit_sorts.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByRef lclsCrit_sort As Crit_sort) As Crit_sort
		With lclsCrit_sort
			mCol.Add(lclsCrit_sort, "CT" & .nCrthecni & .nRandom & .sSolic & .nCount & .sStatregt)
		End With
		
		'+ Devuelve el objeto creado.
		Add = lclsCrit_sort
		'UPGRADE_NOTE: Object lclsCrit_sort may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCrit_sort = Nothing
	End Function
	
	'% FindMVI816: Devuelve una coleccion de objetos de tipo Crit_sort
	Public Function Find(ByVal nCrthecni As Integer, Optional ByVal lbFind As Boolean = False) As Boolean
		Dim lclsCrit_sort As Crit_sort
		'- Se define la variable lrecCrit_sort que se utilizará como cursor.
		Dim lrecCrit_sort As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecCrit_sort = New eRemoteDB.Execute
		
		Find = True
		
		'+ Se ejecuta el Store procedure que busca los exámenes
		With lrecCrit_sort
			.StoredProcedure = "reaCrit_sort"
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					lclsCrit_sort = New Crit_sort
					lclsCrit_sort.nCrthecni = .FieldToClass("nCrthecni")
					lclsCrit_sort.nRandom = .FieldToClass("nRandom")
					lclsCrit_sort.sSolic = .FieldToClass("sSolic")
					lclsCrit_sort.nCount = .FieldToClass("nCount")
					lclsCrit_sort.sStatregt = .FieldToClass("sStatregt")
					Call Add(lclsCrit_sort)
					'UPGRADE_NOTE: Object lclsCrit_sort may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCrit_sort = Nothing
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCrit_sort may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCrit_sort = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Crit_sort
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
End Class






