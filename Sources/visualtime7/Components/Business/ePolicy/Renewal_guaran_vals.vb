Option Strict Off
Option Explicit On
Public Class Renewal_guaran_vals
	Implements System.Collections.IEnumerable
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Renewal_guaran_val) As Renewal_guaran_val
		If objClass Is Nothing Then
			objClass = New Renewal_guaran_val
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nGuarsav_year & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Renewal_guaran_val
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
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaRenewal_guaran_val As eRemoteDB.Execute
		Dim lclsRenewal_guaran_val As Renewal_guaran_val
		
		On Error GoTo Find_Err
		Find = True
		
		lrecReaRenewal_guaran_val = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaRenewal_guaran_val_a'
		With lrecReaRenewal_guaran_val
			.StoredProcedure = "InsRenewal_guaran_valpkg.ReaRenewal_guaran_val_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsRenewal_guaran_val = New Renewal_guaran_val
					lclsRenewal_guaran_val.sCertype = .FieldToClass("sCertype")
					lclsRenewal_guaran_val.nBranch = .FieldToClass("nBranch")
					lclsRenewal_guaran_val.nProduct = .FieldToClass("nProduct")
					lclsRenewal_guaran_val.nPolicy = .FieldToClass("nPolicy")
					lclsRenewal_guaran_val.nCertif = .FieldToClass("nCertif")
					lclsRenewal_guaran_val.nGuarsav_year = .FieldToClass("nGuarsav_year")
					lclsRenewal_guaran_val.dEffecdate = .FieldToClass("dEffecdate")
					lclsRenewal_guaran_val.dNulldate = .FieldToClass("dNulldate")
					lclsRenewal_guaran_val.dIniperiod = .FieldToClass("dIniperiod")
					lclsRenewal_guaran_val.dEndperiod = .FieldToClass("dEndperiod")
					lclsRenewal_guaran_val.nCurrentamount = .FieldToClass("nCurrentamount")
					lclsRenewal_guaran_val.nNewamount = .FieldToClass("nNewamount")
					lclsRenewal_guaran_val.nCurrentprem = .FieldToClass("nCurrentprem")
					lclsRenewal_guaran_val.nNewprem = .FieldToClass("nNewprem")
					lclsRenewal_guaran_val.sTypepaid = .FieldToClass("sTypepaid")
					lclsRenewal_guaran_val.sTypepaidDes = .FieldToClass("sTypepaidDes")
					lclsRenewal_guaran_val.sProcess = .FieldToClass("sProcess")
					Call Add(lclsRenewal_guaran_val)
					.RNext()
					'UPGRADE_NOTE: Object lclsRenewal_guaran_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsRenewal_guaran_val = Nothing
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
		'UPGRADE_NOTE: Object lrecReaRenewal_guaran_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRenewal_guaran_val = Nothing
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






