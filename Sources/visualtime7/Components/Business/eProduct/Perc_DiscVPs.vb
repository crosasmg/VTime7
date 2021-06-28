Option Strict Off
Option Explicit On
Public Class Perc_DiscVPs
	Implements System.Collections.IEnumerable
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Perc_DiscVP) As Perc_DiscVP
		If objClass Is Nothing Then
			objClass = New Perc_DiscVP
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nBranch & .nProduct & .nVp_ini & .nVp_end & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Perc_DiscVP
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
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaPerc_DiscVP As eRemoteDB.Execute
		Dim lclsPerc_DiscVP As Perc_DiscVP
		
		On Error GoTo Find_Err
		Find = True
		
		lrecReaPerc_DiscVP = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaPerc_DiscVP_a'
		With lrecReaPerc_DiscVP
			.StoredProcedure = "InsPerc_DiscVPpkg.ReaPerc_DiscVP_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsPerc_DiscVP = New Perc_DiscVP
					lclsPerc_DiscVP.nBranch = .FieldToClass("nBranch")
					lclsPerc_DiscVP.nProduct = .FieldToClass("nProduct")
					lclsPerc_DiscVP.nVp_ini = .FieldToClass("nVp_ini")
					lclsPerc_DiscVP.nVp_end = .FieldToClass("nVp_end")
					lclsPerc_DiscVP.dEffecdate = .FieldToClass("dEffecdate")
					lclsPerc_DiscVP.dNulldate = .FieldToClass("dNulldate")
					lclsPerc_DiscVP.nDisc_perc_vp = .FieldToClass("nDisc_perc_vp")
					Call Add(lclsPerc_DiscVP)
					.RNext()
					'UPGRADE_NOTE: Object lclsPerc_DiscVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPerc_DiscVP = Nothing
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaPerc_DiscVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPerc_DiscVP = Nothing
		On Error GoTo 0
	End Function
	
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






