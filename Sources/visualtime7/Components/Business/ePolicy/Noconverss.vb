Option Strict Off
Option Explicit On
Public Class Noconverss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Noconverss.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByRef lclsNoconvers As Noconvers) As Noconvers
		With lclsNoconvers
			mCol.Add(lclsNoconvers, "CT" & .nNo_convers & .sDescript & .nAreaWait & .sDevo & .sDisc & .sStatregt & .nExpenses & .sRoutine & .nHealthexp)
		End With
		'+ Devuelve el objeto creado.
		Add = lclsNoconvers
		'UPGRADE_NOTE: Object lclsNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNoconvers = Nothing
	End Function
	'% FindMCA815: Devuelve una coleccion de objetos de tipo Noconvers
	'------------------------------------------------------------
	Public Function Find(ByVal nNo_convers As Integer, Optional ByVal lbFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		Dim lclsNoconvers As Noconvers
		'- Se define la variable lrecNoconvers que se utilizará como cursor.
		Dim lrecNoconvers As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecNoconvers = New eRemoteDB.Execute
		
		Find = True
		
		'+ Se ejecuta el Store procedure que busca los movimientos de un codigo de Causa
		With lrecNoconvers
			.StoredProcedure = "reaNoconvers"
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					lclsNoconvers = New Noconvers
					lclsNoconvers.nNo_convers = .FieldToClass("nNo_convers")
					lclsNoconvers.sDescript = .FieldToClass("sDescript")
					lclsNoconvers.nAreaWait = .FieldToClass("nAreaWait")
					lclsNoconvers.sDevo = .FieldToClass("sDevo")
					lclsNoconvers.sDisc = .FieldToClass("sDisc")
					lclsNoconvers.sStatregt = .FieldToClass("sStatregt")
					lclsNoconvers.nExpenses = .FieldToClass("nExpenses")
					lclsNoconvers.sRoutine = .FieldToClass("sRoutine")
					lclsNoconvers.nHealthexp = .FieldToClass("nHealthexp")
					Call Add(lclsNoconvers)
					'UPGRADE_NOTE: Object lclsNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsNoconvers = Nothing
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Noconvers
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






