Option Strict Off
Option Explicit On
Public Class Table5708s
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
	Public Function Add(ByRef lclsTable5708 As Table5708) As Table5708
		With lclsTable5708
			mCol.Add(lclsTable5708, "CT" & .nType_Move & .sDescript & .nType & .sPb_Bmg & .sStatregt)
		End With
		'+ Devuelve el objeto creado.
		Add = lclsTable5708
		lclsTable5708 = Nothing
	End Function
	'% FindMVI5708: Devuelve una coleccion de objetos de tipo Table5708
	'------------------------------------------------------------
	Public Function Find(ByVal nType_Move As Integer, Optional ByVal lbFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		Dim lclsTable5708 As Table5708
		'- Se define la variable lrecTable5708 que se utilizará como cursor.
		Dim lrecTable5708 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTable5708 = New eRemoteDB.Execute
		
		Find = True
		
		'+ Se ejecuta el Store procedure que busca los movimientos de un codigo de Causa
		With lrecTable5708
			.StoredProcedure = "reaTable5708"
			.Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					lclsTable5708 = New Table5708
					lclsTable5708.nType_Move = .FieldToClass("nType_Move")
					lclsTable5708.sDescript = .FieldToClass("sDescript")
					lclsTable5708.sShort_des = .FieldToClass("sShort_des")
					lclsTable5708.nType = .FieldToClass("nType")
					lclsTable5708.sPb_Bmg = .FieldToClass("sPb_Bmg")
					lclsTable5708.sStatregt = .FieldToClass("sStatregt")
					Call Add(lclsTable5708)
					lclsTable5708 = Nothing
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Table5708
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






