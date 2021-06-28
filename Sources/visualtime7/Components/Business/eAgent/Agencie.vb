Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Agencies_NET.Agencies")> Public Class Agencies
	Implements System.Collections.IEnumerable
	'+local variable to hold collection
	Private mCol As Collection
	
	Public Function Add(ByRef nAgency As Integer, ByRef nOfficeAgen As Integer, ByRef nBran_Off As Integer, ByVal sPay As String, Optional ByRef sKey As String = "") As Agencie
		'+create a new object
		Dim objNewMember As Agencie
		objNewMember = New Agencie
		
		
		'+set the properties passed into the method
		objNewMember.nAgency = nAgency
		objNewMember.nOfficeAgen = nOfficeAgen
		objNewMember.nBran_Off = nBran_Off
		objNewMember.sPay = sPay
		
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		'+return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	Public Function Add_Mop633(ByRef nOfficeAgen As Integer, ByRef nBran_Off As Integer, ByRef nUsercode As Integer, Optional ByRef sKey As String = "") As Agencie
		'+create a new object
		Dim objNewMember As Agencie
		objNewMember = New Agencie
		
		
		'+set the properties passed into the method
		objNewMember.nOfficeAgen = nOfficeAgen
		objNewMember.nBran_Off = nBran_Off
		objNewMember.nUsercode = nUsercode
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		'+return the object created
		Add_Mop633 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Agencie
		Get
			'%used when referencing an element in the collection
			'%vntIndexKey contains either the Index or Key to the collection,
			'%this is why it is declared as a Variant
			'%Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'%used when retrieving the number of elements in the
			'%collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'%this property allows you to enumerate
			'%this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'%used when removing an element from the collection
		'%vntIndexKey contains either the Index or Key, which is why
		'%it is declared as a Variant
		'%Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'%creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'%destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Esta función realiza la lectura y carga de la información  en el grid.
	Public Function Find() As Boolean
		Dim lrecRea_Agencies As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecRea_Agencies = New eRemoteDB.Execute
		
		With lrecRea_Agencies
			.StoredProcedure = "Rea_Agencies"
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nAgency"), .FieldToClass("nOfficeAgen"), .FieldToClass("nBran_Off"), .FieldToClass("sPay"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRea_Agencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_Agencies = Nothing
	End Function
	
	'%Find: Esta función realiza la lectura y carga de la información  en el grid correspondiente
	'       a la transacción MOP633.
	Public Function Find_Mop633(ByVal nBran_Off As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecRea_Agencies As eRemoteDB.Execute
		
		On Error GoTo Find_Mop633_Err
		
		lrecRea_Agencies = New eRemoteDB.Execute
		
		With lrecRea_Agencies
			
			.StoredProcedure = "Rea_Agencies_By_nBran_Off"
			.Parameters.Add("nBran_Off", nBran_Off, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add_Mop633(.FieldToClass("nOfficeAgen"), .FieldToClass("nBran_Off"), .FieldToClass("nUser"))
					.RNext()
				Loop 
				.RCloseRec()
				Find_Mop633 = True
			Else
				Find_Mop633 = False
			End If
		End With
		
Find_Mop633_Err: 
		If Err.Number Then
			Find_Mop633 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRea_Agencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_Agencies = Nothing
	End Function
End Class






