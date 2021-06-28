Option Strict Off
Option Explicit On
Public Class cl_Reinsurans
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	Public nShare_Total As Double
	
	Public Function Add(ByVal nBranch_Rei As Integer, ByVal nModulec As Integer, ByVal nType_Rein As Integer, ByVal sClient As String, ByVal nCompany As Integer, ByVal dAcceDate As Date, ByVal nCapital As Double, ByVal nCommissi As Double, ByVal nCurrency As Integer, ByVal sHeap_code As String, ByVal nInter_rate As Double, ByVal nShare As Double, ByVal nChange As Integer, ByVal nAcep_code As Integer, ByVal nLoc_reserv As Double, ByVal nReserv_Pend As Double, ByVal nPay_amount As Double, ByVal nLoc_rec_am As Double, ByVal nLoc_cos_re As Double, ByVal nNumber As Integer, ByVal nReser_rate As Double, ByVal sSel As Integer, ByVal sDesType_Rein As String, ByVal sCompany As String) As cl_Reinsuran
		'create a new object
		Dim objNewMember As cl_Reinsuran
		objNewMember = New cl_Reinsuran
		
		With objNewMember
			.nBranch_Rei = nBranch_Rei
			.nModulec = nModulec
			.nType_Rein = nType_Rein
			.sClient = sClient
			.nCompany = nCompany
			.dAcceDate = dAcceDate
			.nCapital = nCapital
			.nCommissi = nCommissi
			.nCurrency = nCurrency
			.sHeap_code = sHeap_code
			.nInter_rate = nInter_rate
			.nNumber = nNumber
			.nReser_rate = nReser_rate
			.nShare = nShare
			.nChange = nChange
			.nAcep_code = nAcep_code
			.nLoc_reserv = nLoc_reserv
			.nReserv_Pend = nReserv_Pend
			.nPay_amount = nPay_amount
			.nLoc_rec_am = nLoc_rec_am
			.nLoc_cos_re = nLoc_cos_re
			.nloc_Reserv_p = (nLoc_reserv * nShare) / 100
			.nReserv_pend_p = (nReserv_Pend * nShare) / 100
			.nPay_amount_p = (nPay_amount * nShare) / 100
			.nLoc_rec_am_p = (nLoc_rec_am * nShare) / 100
			.nLoc_cos_re_p = (nLoc_cos_re * nShare) / 100
			.sDesType_Rein = sDesType_Rein
			.sCompany = sCompany
			.sSel = CStr(sSel)
		End With
		
		mCol.Add(objNewMember)
		
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
		
	End Function
	
	'**% Finf: find the records in cl_reinsuran
	'% Find: busca los registrons en cl_reinsuran
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecreacl_Reinsuran As eRemoteDB.Execute
		
		lrecreacl_Reinsuran = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+Parameters definition for the stored procedure 'insudb.reaT_payclaAll'
		'**Data read on 02/20/2001 04:38:22 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaT_payclaAll'
		'+ Información leída el 20/02/2001 04:38:22 p.m.
		
		With lrecreacl_Reinsuran
			.StoredProcedure = "reacl_Reinsuran2"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nShare_Total = 0
				Do While Not .EOF
					nShare_Total = nShare_Total + .FieldToClass("nShare")
					
					Call Add(.FieldToClass("nBranch_rei"), .FieldToClass("nModulec"), .FieldToClass("nType_rein"), .FieldToClass("sClient"), .FieldToClass("nCompany"), .FieldToClass("dAcceDate"), .FieldToClass("nCapital"), .FieldToClass("nCommissi"), .FieldToClass("nCurrency"), .FieldToClass("sHeap_code"), .FieldToClass("nInter_rate"), .FieldToClass("nShare"), .FieldToClass("nChange"), .FieldToClass("nAcep_code"), .FieldToClass("nLoc_reserv"), .FieldToClass("nReserv_Pend"), .FieldToClass("nPay_amount"), .FieldToClass("nLoc_rec_am"), .FieldToClass("nLoc_cos_re"), .FieldToClass("nNumber"), .FieldToClass("nReser_rate"), .FieldToClass("sSel"), .FieldToClass("nNumber") & " - " & .FieldToClass("sDesType_Rein"), .FieldToClass("sCompany"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		lrecreacl_Reinsuran = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cl_Reinsuran
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






