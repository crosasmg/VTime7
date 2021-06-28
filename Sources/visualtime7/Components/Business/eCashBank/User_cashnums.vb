Option Strict Off
Option Explicit On
Public Class User_cashnums
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	Public Function Add(ByRef objClass As User_cashnum) As User_cashnum
		With objClass
			mCol.Add(objClass, "MOP" & .nCashNum & .nUser & .sStatus & .nOfficeAgen)
		End With
		
		'return the object created
		Add = objClass
		
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		Dim lclsUser_cashnum As User_cashnum
		
		On Error GoTo Find_Err
		
		lrecReaUser_cashnum = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reaUser_cashnum_by_year'
		With lrecReaUser_cashnum
			.StoredProcedure = "reaUser_cashnum"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCashNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsUser_cashnum = New User_cashnum
					lclsUser_cashnum.nCashNum = .FieldToClass("nCashNum")
					lclsUser_cashnum.nUser = .FieldToClass("nUser")
					lclsUser_cashnum.sStatus = .FieldToClass("sStatus")
					lclsUser_cashnum.nOffice = .FieldToClass("nOffice")
					lclsUser_cashnum.sCliename = .FieldToClass("sCliename")
					lclsUser_cashnum.nCashSup = .FieldToClass("nCashSup")
					lclsUser_cashnum.nHeadSup = .FieldToClass("nHeadSup")
					lclsUser_cashnum.nOfficeAgen = .FieldToClass("nOfficeAgen")
					Call Add(lclsUser_cashnum)
					
					.RNext()
					'UPGRADE_NOTE: Object lclsUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsUser_cashnum = Nothing
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
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As User_cashnum
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
			'
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
	
	'%Find_OPC824: Consulta de relaciones por caja
	Public Function Find_OPC824(ByVal dCollect As Date, ByVal nCashNum As Integer, ByVal sStatus As String, ByVal nRow As Integer) As Boolean
		Dim nCount As Object
		Dim lrecColformref As eRemoteDB.Execute
		Dim lclsUser_cashnum As User_cashnum
		
		If sStatus = "0" Then
			sStatus = ""
		End If
		On Error GoTo Find_OPC824_Err
		lrecColformref = New eRemoteDB.Execute
		With lrecColformref
			.StoredProcedure = "reaOPC824"
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCount = 1
				Do While Not .EOF And nCount < nRow
					nCount = nCount + 1
					.RNext()
				Loop 
				Do While Not .EOF And nCount < nRow + 50
					nCount = nCount + 1
					lclsUser_cashnum = New User_cashnum
					lclsUser_cashnum.nCashNum = .FieldToClass("nCashnum")
					lclsUser_cashnum.dCollect = .FieldToClass("dCollect")
					lclsUser_cashnum.sRel_Type = .FieldToClass("sRel_Type")
					lclsUser_cashnum.sDesc_Reltype = .FieldToClass("sDesc_Reltype")
					lclsUser_cashnum.sClient = .FieldToClass("sClient")
					lclsUser_cashnum.sDigit = .FieldToClass("sDigit")
					lclsUser_cashnum.nBordereaux = .FieldToClass("nBordereaux")
					lclsUser_cashnum.nBranch = .FieldToClass("nBranch")
					lclsUser_cashnum.sDesc_Branch = .FieldToClass("sDesc_Branch")
					lclsUser_cashnum.nProduct = .FieldToClass("nProduct")
					lclsUser_cashnum.sDesc_Product = .FieldToClass("sDesc_Product")
					lclsUser_cashnum.nPolicy = .FieldToClass("nPolicy")
					lclsUser_cashnum.nProponum = .FieldToClass("nProponum")
					lclsUser_cashnum.nBulletins = .FieldToClass("nBulletins")
					lclsUser_cashnum.sStatus = .FieldToClass("sStatus")
					lclsUser_cashnum.nReceipt = .FieldToClass("nReceipt")
					lclsUser_cashnum.nDraft = .FieldToClass("nDraft")
					lclsUser_cashnum.dValueDate = .FieldToClass("dValuedate")
					lclsUser_cashnum.nCollecdoctyp = .FieldToClass("nCollecdoctyp")
					lclsUser_cashnum.nSequence = .FieldToClass("nSequence")
					Call AddOPC824(lclsUser_cashnum)
					.RNext()
				Loop 
				.RCloseRec()
				Find_OPC824 = True
			Else
				Find_OPC824 = False
			End If
		End With
		
Find_OPC824_Err: 
		If Err.Number Then
			Find_OPC824 = False
		End If
		'UPGRADE_NOTE: Object lrecColformref may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColformref = Nothing
		On Error GoTo 0
	End Function
	'%AddOPC824: Consulta de relaciones por caja
	Public Function AddOPC824(ByRef objClass As User_cashnum) As User_cashnum
		With objClass
			mCol.Add(objClass, .nCashNum & .dCollect & .sRel_Type & .sDesc_Reltype & .sClient & .sDigit & .nBordereaux & .nBranch & .sDesc_Branch & .nProduct & .sDesc_Product & .nPolicy & .nProponum & .nBulletins & .sStatus & .nReceipt & .nDraft & .dValueDate & .nCollecdoctyp & .nSequence)
		End With
		
		'return the object created
		AddOPC824 = objClass
	End Function
End Class






