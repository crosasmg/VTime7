Option Strict Off
Option Explicit On
Public Class cl_covermas
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: cl_covermas.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclscl_coverma As cl_coverma) As cl_coverma
		mCol.Add(lclscl_coverma)
		
		'+Devolver el objeto creado
		Add = lclscl_coverma
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cl_coverma
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
	
	'%------------------------------------------------------------------------------%'
	Public Function insreacl_coverma(ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Integer, ByVal nCoveru As Integer, ByVal nConceptu As Integer, ByVal iN_nv_gas As Integer, ByVal nV_can As Integer, ByVal nGasttot As Integer) As Boolean
		'%-------------------------------------------------------------------------------%'
		Dim lrecinsReacl_coverma As eRemoteDB.Execute
		Dim lclscl_coverma As cl_coverma
		
		On Error GoTo insReacl_coverma_Err
		
		lrecinsReacl_coverma = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insReacl_coverma al 05-02-2005 14:42:49
		'+
		With lrecinsReacl_coverma
			.StoredProcedure = "insReacl_coverma"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoveru", nCoveru, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConceptu", nConceptu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("iN_nv_gas", iN_nv_gas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nV_can", nV_can, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGasttot", nGasttot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insreacl_coverma = True
				Do While Not .EOF
					lclscl_coverma = New cl_coverma
					lclscl_coverma.sKey = .FieldToClass("sKey")
					lclscl_coverma.nCover = .FieldToClass("nCover")
					lclscl_coverma.nCurrency = .FieldToClass("nCurrency")
					lclscl_coverma.sResstat = .FieldToClass("sResstat")
					lclscl_coverma.nConcept = .FieldToClass("nConcept")
					lclscl_coverma.nLimit = .FieldToClass("nLimit")
					lclscl_coverma.nExces = .FieldToClass("nExces")
					lclscl_coverma.nQuantity = .FieldToClass("nQuantity")
					lclscl_coverma.nAmount = .FieldToClass("nAmount")
					lclscl_coverma.nDeduc = .FieldToClass("nDeduc")
					lclscl_coverma.nIndemrate = .FieldToClass("nIndemrate")
					lclscl_coverma.nReserve = .FieldToClass("nReserve")
					lclscl_coverma.sDescob = .FieldToClass("sDescob")
					lclscl_coverma.nModulec = .FieldToClass("nModulec")
					lclscl_coverma.sDescon = .FieldToClass("sDescon")
					lclscl_coverma.nClcover = .FieldToClass("nClcover")
					lclscl_coverma.nBranch_est = .FieldToClass("nBranch_est")
					lclscl_coverma.nBranch_led = .FieldToClass("nBranch_led")
					lclscl_coverma.nBranch_rei = .FieldToClass("nBranch_rei")
					lclscl_coverma.sAuto_resist = .FieldToClass("sAuto_resist")
					lclscl_coverma.nExchange = .FieldToClass("nExchange")
					lclscl_coverma.nIndiclimcon = .FieldToClass("nIndiclimcon")
					lclscl_coverma.sBudget = .FieldToClass("sBudget")
					lclscl_coverma.dDate_bud = .FieldToClass("dDate_bud")
					lclscl_coverma.nSub_provider = .FieldToClass("nSub_provider")
					lclscl_coverma.sCliename = .FieldToClass("sCliename")
					
					Call Add(lclscl_coverma)
					lclscl_coverma = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insreacl_coverma = False
			End If
		End With
		
insReacl_coverma_Err: 
		If Err.Number Then
			insreacl_coverma = False
		End If
		lrecinsReacl_coverma = Nothing
		On Error GoTo 0
		
	End Function
	'%-------------------------------------------------------------------------------%'
	Public Function insreacover_si025(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOpt_claityp As Integer, ByVal nCover As Double, ByVal sbrancht As String) As Boolean
		'%-------------------------------------------------------------------------------%'
		Dim lrecinsReacover_si025 As eRemoteDB.Execute
		Dim lclscl_coverma As cl_coverma
		
		On Error GoTo insReacover_si025_Err
		
		lrecinsReacover_si025 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insReacover_si025 al 05-04-2005 11:48:20
		'+
		With lrecinsReacover_si025
			.StoredProcedure = "Reacover_si025Pkg.insReacover_si025"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_claityp", nOpt_claityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insreacover_si025 = True
				Do While Not .EOF
					lclscl_coverma = New cl_coverma
					lclscl_coverma.sDescover = .FieldToClass("sDescover")
					lclscl_coverma.nCurrency = .FieldToClass("nCurrency")
					lclscl_coverma.nModulec = .FieldToClass("nModulec")
					lclscl_coverma.nCover = .FieldToClass("nCover")
					lclscl_coverma.nGroup = .FieldToClass("nGroup")
					lclscl_coverma.sReservstat = .FieldToClass("sReservstat")
					lclscl_coverma.nDamages = .FieldToClass("nDamages")
					lclscl_coverma.nFra_amount = .FieldToClass("nFra_amount")
					lclscl_coverma.nReserve = .FieldToClass("nReserve")
					lclscl_coverma.nDamprof = .FieldToClass("nDamprof")
					lclscl_coverma.nExchange = .FieldToClass("nExchange")
					lclscl_coverma.nCapital = .FieldToClass("nCapital")
					'lclscl_coverma.sFrandedi = .FieldToClass("sFrandedi")
					lclscl_coverma.sFrancapl = .FieldToClass("sFrancapl")
					lclscl_coverma.nBranch_est = .FieldToClass("nBranch_est")
					lclscl_coverma.nBranch_led = .FieldToClass("nBranch_led")
					lclscl_coverma.nBranch_rei = .FieldToClass("nBranch_rei")
					lclscl_coverma.nLoc_pay_am = .FieldToClass("nLoc_pay_am")
					lclscl_coverma.nPay_amount = .FieldToClass("nPay_amount")
					lclscl_coverma.sAutomrep = .FieldToClass("sAutomrep")
					lclscl_coverma.nFixamount = .FieldToClass("nFixamount")
					lclscl_coverma.nMaxamount = .FieldToClass("nMaxamount")
					lclscl_coverma.nMinamount = .FieldToClass("nMinamount")
					lclscl_coverma.nRate = .FieldToClass("nRate")
					lclscl_coverma.nMedreser = .FieldToClass("nMedreser")
					lclscl_coverma.sRoureser = .FieldToClass("sRoureser")
					lclscl_coverma.sCacalili = .FieldToClass("sCacalili")
					lclscl_coverma.sCaren_type = .FieldToClass("sCaren_type")
					lclscl_coverma.nCaren_quan = .FieldToClass("nCaren_quan")
					lclscl_coverma.sKey = .FieldToClass("sKey")
					'lclscl_coverma.sSinsurini = .FieldToClass("sSinsurini")
					lclscl_coverma.sClient = .FieldToClass("sClient")
					lclscl_coverma.sBill_ind = .FieldToClass("sBill_ind")
					'lclscl_coverma.sCurrDes = .FieldToClass("sCurrdes")
					'lclscl_coverma.sDesreservstat = .FieldToClass("sDesreservstat")
					'lclscl_coverma.sFrantypedesc = .FieldToClass("sFrantypedesc")
					'lclscl_coverma.sCliename = .FieldToClass("sCliename")
					'lclscl_coverma.sDigit = .FieldToClass("sDigit")
					lclscl_coverma.sAuto_resist = .FieldToClass("sAuto_resist")
					lclscl_coverma.nLimit_h = .FieldToClass("nLimit_h")
					lclscl_coverma.nPay_concep = .FieldToClass("nPay_concep")
					lclscl_coverma.nPrestac = .FieldToClass("nPrestac")
					lclscl_coverma.nAmoun_used = .FieldToClass("nAmoun_used")
					lclscl_coverma.nDed_amount = .FieldToClass("nDed_amount")
					lclscl_coverma.nDed_percen = .FieldToClass("nDed_percen")
					lclscl_coverma.nDed_quanti = .FieldToClass("nDed_quanti")
					lclscl_coverma.nDed_type = .FieldToClass("nDed_type")
					lclscl_coverma.nIndem_rate = .FieldToClass("nIndem_rate")
					lclscl_coverma.nLimit = .FieldToClass("nLimit")
					lclscl_coverma.nLimit_exe = .FieldToClass("nLimit_exe")
					lclscl_coverma.nCount = .FieldToClass("nCount")
					lclscl_coverma.nTyplim = .FieldToClass("nTyplim")
					lclscl_coverma.nPunish = .FieldToClass("nPunish")
					lclscl_coverma.nQuant_used = .FieldToClass("nQuant_used")
					lclscl_coverma.nAmount = .FieldToClass("nAmount")
					Call Add(lclscl_coverma)
					lclscl_coverma = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insreacover_si025 = False
			End If
		End With
		
insReacover_si025_Err: 
		If Err.Number Then
			insreacover_si025 = False
		End If
		lrecinsReacover_si025 = Nothing
		On Error GoTo 0
		
	End Function
End Class






