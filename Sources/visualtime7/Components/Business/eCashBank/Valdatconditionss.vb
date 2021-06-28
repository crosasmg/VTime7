Option Strict Off
Option Explicit On
Public Class Valdatconditionss
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	Public Function Add(ByRef nId As Integer, ByRef nConcept As Integer, ByRef nBranch As Integer, ByRef nDoctyp As Integer, ByRef nDefaultDat As Integer, ByRef nChangesDat As Integer, Optional ByRef sKey As String = "") As Valdatconditions
		
		'create a new object
		
		Dim objNewMember As Valdatconditions
		objNewMember = New Valdatconditions
		
		'set the properties passed into the method
		objNewMember.nId = nId
		objNewMember.nConcept = nConcept
		objNewMember.nBranch = nBranch
		objNewMember.nDoctyp = nDoctyp
		objNewMember.nDefaultDat = nDefaultDat
		objNewMember.nChangesDat = nChangesDat
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find : Funci�n que permite obtener todas las condiciones de la fecha de valorizaci�n
	Public Function Find() As Boolean
		Dim lrecreaValdatconditions As eRemoteDB.Execute
		
		lrecreaValdatconditions = New eRemoteDB.Execute
		
		'Definici�n de par�metros para stored procedure 'insudb.reaProvince_a'
		'Informaci�n le�da el 20/09/01 01:57:58 p.m.
		
		With lrecreaValdatconditions
			.StoredProcedure = "reaValdatconditions"
			
			If .Run Then
				Do While Not .EOF
					Add(.FieldToClass("nId"), .FieldToClass("nConcept"), .FieldToClass("nBranch"), .FieldToClass("nDoctyp"), .FieldToClass("nDefaultDat"), .FieldToClass("nChangesDat"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaValdatconditions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaValdatconditions = Nothing
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Valdatconditions
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






