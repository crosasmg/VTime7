Option Strict Off
Option Explicit On
Public Class Tab_bk_ages
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	
	Private mCol As Collection
	
	Private llngBank_code As Integer
	
	Public Function Add(ByRef nBank_code As Integer, ByRef nBk_agency As Integer, ByRef sDescript As String, ByRef sShort_des As String, ByRef sStatregt As String, ByRef sN_Aba As String, Optional ByRef sKey As String = "") As Tab_bk_age
		
		'create a new object
		
		Dim objNewMember As Tab_bk_age
		objNewMember = New Tab_bk_age
		
		With objNewMember
			.nBank_code = nBank_code
			.nBk_agency = nBk_agency
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.sN_Aba = sN_Aba
		End With
		
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
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-----------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_bk_age
		Get
			'-----------------------------------------------------------
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
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	Public Function Find(ByVal nBank_code As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		Dim lrecreaTab_bk_age1 As eRemoteDB.Execute
		
		If llngBank_code <> nBank_code Or lblnFind Then
			
			lrecreaTab_bk_age1 = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaTab_bk_age1'
			'Información leída el 17/09/2001 2:40:27 PM
			
			With lrecreaTab_bk_age1
				.StoredProcedure = "reaTab_bk_age1"
				.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					lblnRead = True
					Do While Not .EOF
						Call Add(nBank_code, .FieldToClass("nBk_agency"), .FieldToClass("sDescript"), .FieldToClass("sShort_des"), .FieldToClass("sStatregt"), .FieldToClass("sN_Aba"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaTab_bk_age1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaTab_bk_age1 = Nothing
			llngBank_code = nBank_code
		End If
		
		Find = lblnRead
	End Function
End Class






