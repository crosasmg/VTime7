Option Strict Off
Option Explicit On
Public Class T_conceptss
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	Public nCount As Integer
	Public nPaidAmount As Double
	Public nTotalAmount As Double
	Public nTotalAmountGen As Double
	
	
	Public Function Add(ByRef lclsT_Concepts As T_concepts) As T_concepts
		With lclsT_Concepts
			mCol.Add(lclsT_Concepts)
		End With
		'return the object created
		Add = lclsT_Concepts
		
	End Function
	
	'%findCO823: Se lee de la tabla de conceptos
	Public Function FindCO823(ByVal nAction As ColformRef.TypeActionsSeqColl, ByVal nBordereaux As Double, ByVal dCollect As Date, ByVal dValueDate As Date, ByVal sRelOrigi As String) As Boolean
		Dim lclsT_Concepts As eCollection.T_concepts
		Dim lrecT_Concepts As eRemoteDB.Execute
		Dim ldblBordereaux_Aux As Double
		Dim llngCount As Integer
		
		On Error GoTo FindCO823_Err
		
		lrecT_Concepts = New eRemoteDB.Execute
		
		With lrecT_Concepts
			.StoredProcedure = "insReaCO823"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValuedate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(True)
			If (.ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK) Then
				FindCO823 = False
				If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
				End If
			Else
				FindCO823 = True
				
				Do While Not (.EOF Or llngCount > 100)
					ldblBordereaux_Aux = .FieldToClass("nBordereaux", 0)
					'+ Tratamiento del registro especial (el primero).
					If ldblBordereaux_Aux = 0 Then
						nPaidAmount = .FieldToClass("nPaidAmount", 0)
						nTotalAmount = .FieldToClass("nTotalAmount", 0)
						nTotalAmountGen = .FieldToClass("nTotalGen", 0)
					Else
						llngCount = llngCount + 1
						lclsT_Concepts = New eCollection.T_concepts
						lclsT_Concepts.nBordereaux = .FieldToClass("nBordereaux")
						lclsT_Concepts.nTransac = .FieldToClass("nTransac")
						lclsT_Concepts.nConcept = .FieldToClass("nConcept")
						lclsT_Concepts.sConcept = .FieldToClass("sConcept")
						lclsT_Concepts.nBranch = .FieldToClass("nBranch")
						lclsT_Concepts.nProduct = .FieldToClass("nProduct")
						lclsT_Concepts.nProponum = .FieldToClass("nProponum")
						lclsT_Concepts.nCertif = .FieldToClass("nCertif")
						lclsT_Concepts.sClient = .FieldToClass("sClient")
						lclsT_Concepts.sCliename = .FieldToClass("sCliename")
						lclsT_Concepts.nOricurr = .FieldToClass("nOricurr")
						lclsT_Concepts.nOriAmount = .FieldToClass("NOriAmount")
						lclsT_Concepts.nCurrency = .FieldToClass("nCurrency")
						lclsT_Concepts.sCurrency = .FieldToClass("sCurrency")
						lclsT_Concepts.nAmount = .FieldToClass("nAmount")
						lclsT_Concepts.nExchange = .FieldToClass("nExchange")
						lclsT_Concepts.dValDate = .FieldToClass("dValDate")
						lclsT_Concepts.nChangeDat = .FieldToClass("nChangeDat")
						lclsT_Concepts.nClaim = .FieldToClass("nClaim")
						lclsT_Concepts.nCase_num = .FieldToClass("nCase_num")
						lclsT_Concepts.nBank_code = .FieldToClass("nBank_code")
						lclsT_Concepts.nBank_Agree = .FieldToClass("nBank_Agree")
						lclsT_Concepts.sBank_agree = .FieldToClass("sBank_Agree")
						lclsT_Concepts.nAgreement = .FieldToClass("nAgreement")
						lclsT_Concepts.sAgreement = .FieldToClass("sAgreement")
						lclsT_Concepts.nNoteNum = .FieldToClass("nNoteNum")
						lclsT_Concepts.Nsuport_Id = .FieldToClass("Nsuport_Id")
						lclsT_Concepts.NtypeSupport = .FieldToClass("NtypeSupport")
						lclsT_Concepts.Dcollection = .FieldToClass("Dcollection")
						lclsT_Concepts.nCash_Id = .FieldToClass("nCash_Id")
						lclsT_Concepts.nTyp_acco = .FieldToClass("nTyp_acco")
						lclsT_Concepts.sTyp_acco = .FieldToClass("sTyp_acco")
						lclsT_Concepts.nIntermed = .FieldToClass("nIntermed")
						lclsT_Concepts.sIntermed = .FieldToClass("sIntermed")
						lclsT_Concepts.nCompany = .FieldToClass("nCompany")
						lclsT_Concepts.nDeman_Type = .FieldToClass("nDeman_type")
						lclsT_Concepts.nAccount = .FieldToClass("nAccount")
						lclsT_Concepts.sAccount = .FieldToClass("sAccount")
						lclsT_Concepts.nBulletins = .FieldToClass("nBulletins")
						lclsT_Concepts.sCaseNum = .FieldToClass("sCaseNum")
						lclsT_Concepts.nLoan = .FieldToClass("nLoan")
						lclsT_Concepts.sLoan = .FieldToClass("sLoan")
						Call Add(lclsT_Concepts)
						'UPGRADE_NOTE: Object lclsT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsT_Concepts = Nothing
						
					End If
					.RNext()
				Loop 
			End If
		End With
		
		nCount = llngCount
		
FindCO823_Err: 
		If Err.Number Then
			FindCO823 = False
		End If
		
		'UPGRADE_NOTE: Object lclsT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_Concepts = Nothing
		'UPGRADE_NOTE: Object lrecT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Concepts = Nothing
		
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_concepts
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
End Class






