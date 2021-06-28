Option Strict Off
Option Explicit On
Public Class Disc_riskInsus
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_riskInsus.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	Private mCol As Collection
	
	'% add: se agregan campos a la coleccion realcionada a la tabla Disc_RiskInsus
	Public Function Add(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef dEffecdate As Date, ByRef nCapital_init As Double, ByRef nCapital_end As Double, ByRef nRate As Double, ByRef dNulldate As Date, ByRef nUsercode As Integer) As Disc_riskInsu
		Dim objNewMember As Disc_riskInsu
		objNewMember = New Disc_riskInsu
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nCapital_init = nCapital_init
			.nCapital_end = nCapital_end
			.nRate = nRate
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			
		End With
		mCol.Add(objNewMember)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disc_riskInsu
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
	
	'% Find: Realiza la busqueda para llenar la colección
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreaDisc_riskInsu As eRemoteDB.Execute
		
		lrecreaDisc_riskInsu = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecreaDisc_riskInsu
			.StoredProcedure = "READISC_RISKINSU_GRID"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nCapital_init"), .FieldToClass("nCapital_end"), .FieldToClass("nRate"), .FieldToClass("dNulldate"), .FieldToClass("nUsercode"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisc_riskInsu = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






