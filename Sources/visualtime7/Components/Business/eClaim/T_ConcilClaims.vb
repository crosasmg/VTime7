Option Strict Off
Option Explicit On
Public Class T_ConcilClaims
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Funció que carga los datos en la colección.
	Public Function Add(ByVal ldblReceipt As Double, ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal ldblPolicy As Double, ByVal llngCertif As Integer, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrClient As String, ByVal llngStatus_pre As Integer, ByVal ldtmEffecdate As Date, ByVal ldblBalance As Double, ByVal ldblPremium As Double, ByVal llngCurrency As Integer, ByVal lstrIndCheque As String, ByVal llngBordereaux As Integer, ByVal llngCashnum As Integer, ByVal lstrDocnumbe As String, ByVal ldblContrat As Double, ByVal llngDraft As Integer, ByVal llngCompany As Integer, ByVal llngBank_code As Integer, ByVal llngCheOpertyp As Integer, ByVal lstrClaimTyp As String, ByVal lstrMark As String) As T_ConcilClaim
		'create a new object
		Dim objNewMember As T_ConcilClaim
		objNewMember = New T_ConcilClaim
		
		
		With objNewMember
			.nReceipt = ldblReceipt
			.sCerType = lstrCertype
			.nBranch = llngBranch
			.nProduct = llngProduct
			.nPolicy = ldblPolicy
			.nCertif = llngCertif
			.nDigit = llngDigit
			.nPaynumbe = llngPaynumbe
			.sClient = lstrClient
			.nStatus_pre = llngStatus_pre
			.dEffecdate = ldtmEffecdate
			.nBalance = ldblBalance
			.nPremium = ldblPremium
			.nCurrency = llngCurrency
			.sIndCheque = lstrIndCheque
			.nBordereaux = llngBordereaux
			.nCashNum = llngCashnum
			.sDocnumbe = lstrDocnumbe
			.nContrat = ldblContrat
			.nDraft = llngDraft
			.nCompany = llngCompany
			.nBank_code = llngBank_code
			.nCheOpertyp = llngCheOpertyp
			.sClaimTyp = lstrClaimTyp
			.sMark = lstrMark
			
			
		End With
		
		mCol.Add(objNewMember, "T_ConcilClaim" & ldblReceipt & lstrDocnumbe & ldblContrat & llngDraft)
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	
	'+ Find: Localiza todos los recibos pendientes de la poliza de un siniestro
	'+       para conciliar el mismo
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dPayDate As Date, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecT_ConcilClaim As eRemoteDB.Execute
		Dim lrecExchange As eGeneral.Exchange
		
		Dim ldblExchange As Double
		Dim ldblBalanceResult As Double
		
		
		Static llngOldClaim As Double
		Static ldtmOldPayDate As Date
		
		Static lblnRead As Boolean
		
		On Error GoTo Find
		
		If llngOldClaim <> nClaim Or ldtmOldPayDate <> dPayDate Then
			
			llngOldClaim = nClaim
			ldtmOldPayDate = dPayDate
			
			lrecT_ConcilClaim = New eRemoteDB.Execute
			lrecExchange = New eGeneral.Exchange
			
			With lrecT_ConcilClaim
				.StoredProcedure = "reaPremium_SI762" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dPayDate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				
				If .Run Then 'Listo
					Do While Not .EOF
						'**+ Add the record to the class - ACM - 01/30/2001
						'+ Se añade el registro a la clase - ACM - 30/01/2001
						ldblExchange = 0
						ldblBalanceResult = 0
						Call lrecExchange.Convert(ldblExchange, .FieldToClass("nBalance"), .FieldToClass("nCurrency"), nCurrency, dPayDate, ldblBalanceResult)
						Call Add(.FieldToClass("nReceipt"), .FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nDigit"), .FieldToClass("nPaynumbe"), .FieldToClass("sClient"), .FieldToClass("nStatus_pre"), .FieldToClass("dEffecdate"), lrecExchange.pdblResult, .FieldToClass("nPremium"), nCurrency, .FieldToClass("sIndCheque"), .FieldToClass("nBordereaux"), .FieldToClass("nCashnum"), .FieldToClass("sDocnumbe"), .FieldToClass("nContrat"), .FieldToClass("nDraft"), .FieldToClass("nCompany"), .FieldToClass("nBank_code"), .FieldToClass("nCheOpertyp"), .FieldToClass("sClaimTyp"), .FieldToClass("sMark"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			
			lrecT_ConcilClaim = Nothing
			lrecExchange = Nothing
		End If
Find: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_ConcilClaim
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






