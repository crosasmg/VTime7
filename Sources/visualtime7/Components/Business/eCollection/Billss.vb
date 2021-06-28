Option Strict Off
Option Explicit On
Public Class Billss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Billss.cls                               $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 2/03/04 10:18a                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bills
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
	
	'%Add_CO700: Agrega un nuevo registro a la colección
	Public Function Add_CO700(ByVal objClass As Bills) As Bills
		If objClass Is Nothing Then
			objClass = New Bills
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .sKey & .nId)
		End With
		
		'Return the object created
		Add_CO700 = objClass
		
	End Function
	
	'%Find_CO700: Lee los datos de la tabla tmp_co700
	Public Function Find_CO700(ByVal sKey As String, ByVal nAction As Integer, ByVal sDocType As String, ByVal sBillType As String, ByVal nBillnum As Double, ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sClient As String, ByVal nInsur_area As Integer, ByVal nUsercode As Integer, ByVal dValDate As Date) As Boolean
		Dim lrecBills As eRemoteDB.Execute
		Dim lclsBills As eCollection.Bills
		
		On Error GoTo Find_Err
		
		lrecBills = New eRemoteDB.Execute
		
		'+ Si es proforma (sBillType=3) se le asigna "4"
		If sBillType = "3" Then
			sBillType = "4"
		End If
		
		'+ Si el tipo de documento es nota de crédito (sDocType="2") se le asigna "3"
		If sDocType = "2" Then
			sBillType = "3"
		End If
		
		With lrecBills
			.StoredProcedure = "insReaco700"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBilltype", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBillnum", nBillnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateini", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CO700 = True
				Do While Not .EOF
					lclsBills = New eCollection.Bills
					lclsBills.sKey = sKey
					lclsBills.nId = .FieldToClass("nId")
					lclsBills.sSel = .FieldToClass("sSel")
					lclsBills.nCollecDocTyp = .FieldToClass("nCollecdoctyp")
					lclsBills.nBranch = .FieldToClass("nBranch")
					lclsBills.nProduct = .FieldToClass("nProduct")
					lclsBills.nPolicy = .FieldToClass("nPolicy")
					lclsBills.nReceipt = .FieldToClass("nReceipt")
					lclsBills.nContrat = .FieldToClass("nContrat")
					lclsBills.nDraft = .FieldToClass("nDraft")
					lclsBills.nBulletins = .FieldToClass("nBulletins")
					lclsBills.nCurrency = .FieldToClass("nCurrency")
					lclsBills.nAmo_afec = .FieldToClass("nAmountafe")
					lclsBills.nAmo_exen = .FieldToClass("nAmountexe")
					lclsBills.nIva = .FieldToClass("nAmountiva")
					lclsBills.dStatdate = .FieldToClass("dStatdate")
					lclsBills.dExpirDat = .FieldToClass("dExpirdat")
					lclsBills.sClient = .FieldToClass("sClient")
					lclsBills.nTransac = .FieldToClass("nTransac")
					lclsBills.nAgency = .FieldToClass("nAgency")
					lclsBills.nBillnum = .FieldToClass("nBillnum")
					
					Call Add_CO700(lclsBills)
					'UPGRADE_NOTE: Object lclsBills may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsBills = Nothing
					.RNext()
				Loop 
				
			Else
				Find_CO700 = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_CO700 = False
		End If
		'UPGRADE_NOTE: Object lrecBills may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBills = Nothing
		On Error GoTo 0
	End Function
End Class






