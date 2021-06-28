Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Tmp_Crl847As_NET.Tmp_Crl847As")> Public Class Tmp_Crl847As
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tmp_Crl747As.cls                         $%'
	'% $Author:: Nvaplat17                                  $%'
	'% $Date:: 7/01/04 10:51                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsTmp_Crl847A As Tmp_Crl847A) As Tmp_Crl847A
		mCol.Add(lclsTmp_Crl847A)
		
		'+ Devolver el objeto creado
		Add = lclsTmp_Crl847A
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tmp_Crl847A
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
	
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a el ramo, producto, modulo, cobertura y fecha
	Public Function Find(ByRef sKey As String) As Boolean
		Dim lrecreaTmp_Crl847A As eRemoteDB.Execute
		Dim lclsTmp_Crl847A As Tmp_Crl847A
		
		On Error GoTo reaTmp_Crl847A_Err
		
		lrecreaTmp_Crl847A = New eRemoteDB.Execute
		
		With lrecreaTmp_Crl847A
			.StoredProcedure = "reaCrl847_1"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTmp_Crl847A = New Tmp_Crl847A
					lclsTmp_Crl847A.sCod_cumulo = .FieldToClass("sCod_cumulo")
					lclsTmp_Crl847A.nVal_max_ces_uf = .FieldToClass("nVal_max_ces_uf")
					Call Add(lclsTmp_Crl847A)
					'UPGRADE_NOTE: Object lclsTmp_Crl847A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTmp_Crl847A = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
reaTmp_Crl847A_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTmp_Crl847A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTmp_Crl847A = Nothing
	End Function
End Class






