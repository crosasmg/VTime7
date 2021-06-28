Option Strict Off
Option Explicit On
Public Class T_DocTyps
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: T_DocTyps.cls                            $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 18/10/04 6:31p                               $%'
	'% $Revision:: 54                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	Public nSequence_Total As Integer
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByRef lclsT_DocTyp As T_DocTyp) As T_DocTyp
		With lclsT_DocTyp
			mCol.Add(lclsT_DocTyp)
		End With
		
		'return the object created
		Add = lclsT_DocTyp
	End Function
	
	'%Add_sClient: Agrega un elemento a la colección
	Public Function Add_sClient(ByRef lclsT_DocTyp As T_DocTyp) As T_DocTyp
		With lclsT_DocTyp
			mCol.Add(lclsT_DocTyp, "CO" & .sClient)
		End With
		
		'return the object created
		Add_sClient = lclsT_DocTyp
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_DocTyp
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
	
	'%findCO001: Se lee de la tabla de tipos de documentos de cobranzas
	Public Function FindCO001(ByVal nAction As ColformRef.TypeActionsSeqColl, ByVal sReceiptNum As String, ByVal sPolicyNum As String, ByVal dCollect As Date, ByVal sRel_Type As CollectionSeq.TypeOriBordereaux, ByVal nBordereaux As Double, ByVal sStatus As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nAgreement As Integer, ByVal nInsur_area As Integer, ByVal dValueDate As Date, ByVal nOneTime As Integer, ByVal sRent_vital As String, ByVal nFirstRecord As Integer, ByVal nLastRecord As Integer, ByVal sFind As String, ByVal dDateCollect As Date, ByVal sValueDateAll As String) As Boolean
		Dim lclsT_DocTyp As eCollection.T_DocTyp
		Dim lrecT_DocTyp As eRemoteDB.Execute
		Dim ldblAmount As Double
		Dim lintSequence As Integer
		Dim llngCount As Integer
		Dim lintCollecdoctyp As Object
		
		On Error GoTo FindCO001_Err
		
		lrecT_DocTyp = New eRemoteDB.Execute
		
		With lrecT_DocTyp
			.StoredProcedure = "insReaCO001"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeNumeratorR", sReceiptNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeNumeratorP", sPolicyNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRel_Type", sRel_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOneTime", nOneTime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRent_Vital", sRent_vital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstRecord", nFirstRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastRecord", nLastRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFind", sFind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateCollect", dDateCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValueDateAll", sValueDateAll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(True)
			
			If (.ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK) Then
				FindCO001 = False
				If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
				End If
			Else
				FindCO001 = True
				nSequence_Total = .FieldToClass("nSequence_Total")
				llngCount = 1
				
				Do While Not (.EOF Or llngCount > 50)
					lintCollecdoctyp = .FieldToClass("nCollecdoctyp")
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nAmountPay")) Then
						If nAction = ColformRef.TypeActionsSeqColl.cstrQuery And sStatus = CStr(CollectionSeq.TypeStatusSeq.cstrComplete) Then
							ldblAmount = CDbl(.FieldToClass("nAmountPay")) '+Monto del recibo
						Else
							'+ Si el balance es mayor que el monto a pagar (Premium) se toma como el monto el blance (nAmountCol)
							If System.Math.Abs(CDbl(.FieldToClass("nAmountPay"))) > System.Math.Abs(CDbl(.FieldToClass("nAmountCol"))) Then
								'+ Si el tipo de documento corresponde con 4)Prima adicional, 5)Prima exceso y 7)Propuestas se toma el monto a pagar ya que no tiene monto peniente como tal.
								If lintCollecdoctyp = 4 Or lintCollecdoctyp = 5 Or lintCollecdoctyp = 7 Or lintCollecdoctyp = 8 Or lintCollecdoctyp = 9 Then
									ldblAmount = CDbl(.FieldToClass("nAmountPay"))
								Else
									If lintCollecdoctyp = 18 Or lintCollecdoctyp = 19 Or lintCollecdoctyp = 20 Or lintCollecdoctyp = 21 Or lintCollecdoctyp = 22 Or lintCollecdoctyp = 23 Or lintCollecdoctyp = 24 Or lintCollecdoctyp = 11 Or lintCollecdoctyp = 12 Or lintCollecdoctyp = 13 Or lintCollecdoctyp = 14 Or lintCollecdoctyp = 15 Or lintCollecdoctyp = 16 Then
										ldblAmount = CDbl(.FieldToClass("nAmountPay"))
										If ldblAmount = 0 Then
											ldblAmount = CDbl(.FieldToClass("nAmountCol"))
										End If
									Else
										ldblAmount = CDbl(.FieldToClass("nAmountCol"))
										If ldblAmount = 0 Then
											ldblAmount = CDbl(.FieldToClass("nAmountPay"))
										End If
									End If
								End If
								
							Else
								ldblAmount = CDbl(.FieldToClass("nAmountPay")) '+Premio del recibo
							End If
							
						End If
					Else
						ldblAmount = 0
					End If
					
					'* Se verifica que el documento posea saldo para su tratamiento.
					If ldblAmount <> 0 Then
						If nAction = ColformRef.TypeActionsSeqColl.cstrQuery Then
							lintSequence = 0
						Else
							lintSequence = .FieldToClass("nSequence", 0)
						End If
						
						lclsT_DocTyp = New eCollection.T_DocTyp
						
						lclsT_DocTyp.sSel = .FieldToClass("sSel")
						lclsT_DocTyp.nCollecDocTyp = lintCollecdoctyp
						lclsT_DocTyp.sCollecDocTyp = .FieldToClass("sCollecDocTyp", "")
						lclsT_DocTyp.nBordereaux = .FieldToClass("nBordereaux")
						lclsT_DocTyp.nSequence = lintSequence
						lclsT_DocTyp.nBranch = .FieldToClass("nBranch", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.sBranch = .FieldToClass("sBranch")
						lclsT_DocTyp.nProduct = .FieldToClass("nProduct", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.sProduct = .FieldToClass("sProduct")
						lclsT_DocTyp.nPolicy = .FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nCertif = .FieldToClass("nCertif", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nDocument = .FieldToClass("nDocument", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nAmountpay = ldblAmount
						lclsT_DocTyp.nCurrency = .FieldToClass("nCurrency")
						lclsT_DocTyp.sCurrency = .FieldToClass("sCurrency")
						lclsT_DocTyp.nExchange = .FieldToClass("nExchange")
						lclsT_DocTyp.nLocalAmount = System.Math.Round(ldblAmount * .FieldToClass("nExchange"), 0)
						lclsT_DocTyp.nLocalAmountDec = ldblAmount * .FieldToClass("nExchange")
						lclsT_DocTyp.nPaysoondisc = .FieldToClass("nPaySoonDisc", 0)
						lclsT_DocTyp.nInterest_rate = .FieldToClass("nInterest_rate", 0)
						lclsT_DocTyp.nLocalInterest = lclsT_DocTyp.nInterest_rate * .FieldToClass("nExchange", 0)
						lclsT_DocTyp.nAmountCol = .FieldToClass("nAmountCol", 0)
						lclsT_DocTyp.sClient = .FieldToClass("sClient")
						lclsT_DocTyp.sCliename = .FieldToClass("sCliename")
						lclsT_DocTyp.sDigit = .FieldToClass("sDigit")
						lclsT_DocTyp.nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nTypeMove = .FieldToClass("nTypeMove", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.dMovDate = .FieldToClass("dMovDate")
						lclsT_DocTyp.nContrat = .FieldToClass("nContrat", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nDraft = .FieldToClass("nDraft", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nType = .FieldToClass("nType", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nTratypei = .FieldToClass("nTratypei", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nRate_disc = .FieldToClass("nRate_disc", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.nNom_valbon = .FieldToClass("nNom_valbon", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.dIssuedatbon = .FieldToClass("dIssuedatbon")
						lclsT_DocTyp.dExpirdatbon = .FieldToClass("dExpirdatbon")
						
						lclsT_DocTyp.nOrigin = .FieldToClass("nOrigin", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.sOrigin = .FieldToClass("sOrigin")
						lclsT_DocTyp.dDate_Origin = .FieldToClass("dDate_Origin", eRemoteDB.Constants.dtmNull)
						lclsT_DocTyp.nInstitution = .FieldToClass("nInstitution", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.sInstitution = .FieldToClass("sInstitution")
						lclsT_DocTyp.nProdClas = .FieldToClass("nProdClas")
						
						
						lclsT_DocTyp.dValueDate = .FieldToClass("dValueDate")
						lclsT_DocTyp.nChangesDat = .FieldToClass("nChangesDat")
						
						lclsT_DocTyp.nTyp_Profitworker = .FieldToClass("nTyp_Profitworker", eRemoteDB.Constants.intNull)
						lclsT_DocTyp.sTyp_Profitworker = .FieldToClass("sTyp_Profitworker")
						lclsT_DocTyp.sNewReceipt = .FieldToClass("sNewReceipt")
						
						Call Add(lclsT_DocTyp)
						llngCount = llngCount + 1
						'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsT_DocTyp = Nothing
					End If
					.RNext()
				Loop 
			End If
		End With
		
FindCO001_Err: 
		If Err.Number Then
			FindCO001 = False
		End If
		'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_DocTyp = Nothing
		'UPGRADE_NOTE: Object lrecT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_DocTyp = Nothing
		On Error GoTo 0
	End Function
	
	'%FindT_DocTyp: Se lee de la tabla de tipos de documentos de cobranzas
	Public Function FindT_DocTypAll(ByVal nBordereaux As Double, ByVal nCollecDocTyp As Integer) As Boolean
		Dim lclsT_DocTyp As eCollection.T_DocTyp
		Dim lrecT_DocTyp As eRemoteDB.Execute
		Dim llngCount As Integer
		Dim nCollecDocTyp_CO008 As Integer
		
		On Error GoTo FindT_DocTyp_Err
		
		lrecT_DocTyp = New eRemoteDB.Execute
		
		If nCollecDocTyp = 13 Then
			nCollecDocTyp_CO008 = 29
		ElseIf nCollecDocTyp = 14 Then 
			nCollecDocTyp_CO008 = 30
		ElseIf nCollecDocTyp = 15 Then 
			nCollecDocTyp_CO008 = 31
		Else
			nCollecDocTyp_CO008 = eRemoteDB.Constants.intNull
		End If
		
		With lrecT_DocTyp
			.StoredProcedure = "reaT_DocTyp"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp_CO008, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDocument", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nContrat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDraft", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindT_DocTypAll = True
				
				Do While Not .EOF
					lclsT_DocTyp = New eCollection.T_DocTyp
					
					lclsT_DocTyp.sSel = .FieldToClass("sSel")
					lclsT_DocTyp.nCollecDocTyp = .FieldToClass("nCollecDocTyp")
					lclsT_DocTyp.nBordereaux = .FieldToClass("nBordereaux")
					lclsT_DocTyp.nSequence = .FieldToClass("nSequence")
					lclsT_DocTyp.nBranch = .FieldToClass("nBranch")
					lclsT_DocTyp.nProduct = .FieldToClass("nProduct")
					lclsT_DocTyp.nPolicy = .FieldToClass("nPolicy")
					lclsT_DocTyp.nCertif = .FieldToClass("nCertif")
					lclsT_DocTyp.nDocument = .FieldToClass("nDocument")
					lclsT_DocTyp.nAmountpay = .FieldToClass("nAmountPay")
					lclsT_DocTyp.nCurrency = .FieldToClass("nCurrency")
					lclsT_DocTyp.nExchange = .FieldToClass("nExchange")
					lclsT_DocTyp.nLocalAmount = System.Math.Round(.FieldToClass("nAmountPay") * .FieldToClass("nExchange"), 0)
					lclsT_DocTyp.nLocalAmountDec = .FieldToClass("nAmountPay") * .FieldToClass("nExchange")
					lclsT_DocTyp.nPaysoondisc = .FieldToClass("nPaySoonDisc")
					lclsT_DocTyp.nInterest_rate = .FieldToClass("nInterest_rate")
					lclsT_DocTyp.nAmountCol = .FieldToClass("nAmountCol")
					lclsT_DocTyp.sClient = .FieldToClass("sClient")
					lclsT_DocTyp.nProponum = .FieldToClass("nProponum")
					lclsT_DocTyp.nBulletins = .FieldToClass("nBulletins")
					lclsT_DocTyp.nTypeMove = .FieldToClass("nTypeMove")
					lclsT_DocTyp.dMovDate = .FieldToClass("dMovDate")
					lclsT_DocTyp.nContrat = .FieldToClass("nContrat")
					lclsT_DocTyp.nDraft = .FieldToClass("nDraft")
					lclsT_DocTyp.nType = .FieldToClass("nType")
					lclsT_DocTyp.nTratypei = .FieldToClass("nTratypei")
					lclsT_DocTyp.nRate_disc = .FieldToClass("nRate_disc", 0)
					lclsT_DocTyp.nNom_valbon = .FieldToClass("nNom_valbon", 0)
					lclsT_DocTyp.dIssuedatbon = .FieldToClass("dIssuedatbon", eRemoteDB.Constants.dtmNull)
					lclsT_DocTyp.dExpirdatbon = .FieldToClass("dExpirdatbon", eRemoteDB.Constants.dtmNull)
					lclsT_DocTyp.nOrigin = .FieldToClass("nOrigin", eRemoteDB.Constants.intNull)
					lclsT_DocTyp.dDate_Origin = .FieldToClass("dDate_origin", eRemoteDB.Constants.dtmNull)
					lclsT_DocTyp.nInstitution = .FieldToClass("nInstitution", eRemoteDB.Constants.intNull)
					lclsT_DocTyp.dValueDate = .FieldToClass("dValueDate")
					lclsT_DocTyp.nChangesDat = .FieldToClass("nChangesDat")
					
					Call Add(lclsT_DocTyp)
					
					llngCount = llngCount + 1
					'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsT_DocTyp = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindT_DocTyp_Err: 
		If Err.Number Then
			FindT_DocTypAll = False
		End If
		'UPGRADE_NOTE: Object lrecT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_DocTyp = Nothing
		'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_DocTyp = Nothing
		On Error GoTo 0
	End Function
	
	'%FindT_sClient: Se lee de la tabla de tipos de documentos de cobranzas para obtener los clientes de los mismos
	Public Function FindT_sClient(ByVal nBordereaux As Double) As Boolean
		Dim lclsT_DocTyp As eCollection.T_DocTyp
		Dim lrecT_DocTyp As eRemoteDB.Execute
		Dim llngCount As Integer
		
		
		On Error GoTo FindT_sClient_Err
		
		lrecT_DocTyp = New eRemoteDB.Execute
		
		With lrecT_DocTyp
			.StoredProcedure = "reaT_DocTyp_sClient"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindT_sClient = True
				
				Do While Not .EOF
					lclsT_DocTyp = New eCollection.T_DocTyp
					lclsT_DocTyp.sClient = .FieldToClass("sClient")
					Call Add_sClient(lclsT_DocTyp)
					llngCount = llngCount + 1
					'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsT_DocTyp = Nothing
					.RNext()
				Loop 
			End If
		End With
		
FindT_sClient_Err: 
		If Err.Number Then
			FindT_sClient = False
		End If
		'UPGRADE_NOTE: Object lrecT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_DocTyp = Nothing
		'UPGRADE_NOTE: Object lclsT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_DocTyp = Nothing
		On Error GoTo 0
	End Function
End Class






