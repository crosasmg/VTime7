Option Strict Off
Option Explicit On
Public Class Premium_mos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Premium_mos.cls                          $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 20/10/03 1:27p                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCompanyType As String
	Private mdtmInitdate As Date
	Private mdtmEnddate As Date
	Private mintCashnum As Integer
	Private mintCurrency As Integer
	Private mintOffice As Integer
	
	'+ Variables para el manejo de movimientos de un recibo
	Private mdblReceipt As Double
	Private mlngProduct As Integer
	Private mlngBranch As Integer
	Private mstrCertype As String
	Private mlngDigit As Integer
	Private mlngPaynumbe As Integer
	
	
	'**%Add: Adds a new class instance to the collection
	'% Add: se añade una nueva instancia de la clase a la colección
	Public Function Add(ByVal objClass As Premium_mo) As Premium_mo
		With objClass
			mCol.Add(objClass, "CO" & .nBranch & .nProduct & .nPolicy & .nReceipt & .nPremium & .nContrat & .nDraft & .nCurrency & .dStatdate & .nType & .nTransac & .sOrigReceipt)
		End With
		
		'return the object created
		Add = objClass
		
	End Function
	
	'% Add_CO009: se añade una nueva instancia de la clase a la colección Premium_mos
	Public Function Add_CO009(ByVal objClass As Premium_mo) As Premium_mo
		With objClass
			mCol.Add(objClass, "CO" & .nIndex & .sString)
		End With
		
		'return the object created
		Add_CO009 = objClass
		
	End Function
	
	
	
	'% Add_COC747: se añade una nueva instancia de la clase a la colección Premium_mos
	Public Function Add_COC009(ByVal objClass As Premium_mo) As Premium_mo
		With objClass
			mCol.Add(objClass, "CO" & .nTransac & .nType & .dStatdate & .nPremium & .sPay_form & .nBordereaux & .nInt_mora)
		End With
		
		'return the object created
		Add_COC009 = objClass
		
	End Function
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Premium_mo
		Get
			
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'+ creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'+ destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Find: This function searches for the records from the table bulletins
	'%Find: Esta función se encarga de buscar los registros en la tabla bulletins
	Public Function Find() As Boolean
		Find = True
	End Function
	
	
	'**%Find_CollecOper: This method reads the data from the General Information of Receipt table (premium)
	'**%and the receipt transactions table (premium_mo)
	'%Find_CollecOper: Permite leer información de la tabla Información general
	'%de recibos (premium) y Movimientos de cobranzas de un recibo ( premium_mo)
	Public Function Find_CollecOper(ByVal sCompanyType As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nCashnum As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, Optional ByVal lblnFind As Boolean = False, Optional ByVal nFirstRec As Integer = 0, Optional ByVal nLastRec As Integer = 0) As Boolean
		Dim lrecreaPremium_moPremium As eRemoteDB.Execute
		Dim lclsPremium As Premium_mo
		
		On Error GoTo Find_CollecOper_Err
		
		Find_CollecOper = True
		
		If mstrCompanyType <> sCompanyType Or mdtmInitdate <> dInitDate Or mdtmEnddate <> dEndDate Or mintCashnum <> nCashnum Or mintCurrency <> nCurrency Or mintOffice <> nOffice Or lblnFind Then
			
			lrecreaPremium_moPremium = New eRemoteDB.Execute
			'**+Stored procedure parameters definition 'insudb.reaPremium_moPremium'
			'**+Data of 27/03/2001 08:56:46
			'+Definición de parámetros para stored procedure 'insudb.reaPremium_moPremium'
			'+Información leída el 27/03/2001 08:56:46
			
			With lrecreaPremium_moPremium
				.StoredProcedure = "reaPremium_moPremium"
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sCompanyType", IIf(sCompanyType = "3", "C", System.DBNull.Value), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dInitdate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCashnum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nFirstRec", nFirstRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLastRec", nLastRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsPremium = New Premium_mo
						
						lclsPremium.nBranch = .FieldToClass("nBranch")
						lclsPremium.sDescript = .FieldToClass("PROsDescript")
						lclsPremium.nProduct = .FieldToClass("nProduct")
						lclsPremium.nReceipt = .FieldToClass("nReceipt")
						lclsPremium.nPremium = .FieldToClass("nPremium", 0)
						lclsPremium.nContrat = .FieldToClass("nContrat", 0)
						lclsPremium.nDraft = .FieldToClass("nDraft", 0)
						lclsPremium.nCurrency = .FieldToClass("nCurrency")
						lclsPremium.nPolicy = .FieldToClass("nPolicy")
						lclsPremium.nTransac = .FieldToClass("nTransac")
						lclsPremium.nType = .FieldToClass("nType")
						lclsPremium.nOffice = .FieldToClass("nOffice")
						lclsPremium.sPay_form = .FieldToClass("sPay_form")
						lclsPremium.nBordereaux = .FieldToClass("nBordereaux")
						lclsPremium.dStatdate = .FieldToClass("dStatdate")
						lclsPremium.nCashnum = .FieldToClass("nCashnum")
						lclsPremium.nCollector = .FieldToClass("nCollector")
						lclsPremium.sCollector = .FieldToClass("sCollector")
						lclsPremium.nWay_Pay = .FieldToClass("nWay_pay")
						lclsPremium.sWay_Pay = .FieldToClass("sWay_pay")
						
						'**+If it is a brokerage company
						'+Si se trata de una compañia de corretaje
						If sCompanyType = "3" Then 'cstrBrokerOrBrokerageFirm
							lclsPremium.sClienname = .FieldToClass("sCliename")
							lclsPremium.sOfficeIns = .FieldToClass("sOfficeIns")
							lclsPremium.sOrigReceipt = .FieldToClass("sOrigReceipt")
						End If
						Call Add(lclsPremium)
						.RNext()
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
					Loop 
					.RCloseRec()
					Find_CollecOper = True
					mstrCompanyType = sCompanyType
					mdtmInitdate = dInitDate
					mdtmEnddate = dEndDate
					mintCashnum = nCashnum
					mintCurrency = nCurrency
					mintOffice = nOffice
				Else
					Find_CollecOper = False
				End If
			End With
		End If
Find_CollecOper_Err: 
		If Err.Number Then
			Find_CollecOper = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_moPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_moPremium = Nothing
	End Function
	
	'%FindCOC009: Permite obtener el movimiento de un recibo
	Public Function FindCOC009(ByVal nReceipt As Double, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal sCertype As String, ByVal nDigit As Integer, ByVal nPaynumbe As Integer) As Boolean
		Dim lrecreaPremium_mo_o As eRemoteDB.Execute
		Dim lclsPremium_mo As Premium_mo
		
		lrecreaPremium_mo_o = New eRemoteDB.Execute
		
		FindCOC009 = True
		
		On Error GoTo FindCOC009_Err
		
		If mdblReceipt <> nReceipt Or mlngProduct <> nProduct Or mlngBranch <> nBranch Or mstrCertype <> sCertype Or mlngDigit <> nDigit Or mlngPaynumbe <> nPaynumbe Then
			
			'+
			'+ Definición de store procedure reaPremium_mo_o al 03-25-2002 10:18:16
			'+
			With lrecreaPremium_mo_o
				.StoredProcedure = "reaPremium_mo_o"
				.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mdblReceipt = nReceipt
					mlngProduct = nProduct
					mlngBranch = nBranch
					mstrCertype = sCertype
					mlngDigit = nDigit
					mlngPaynumbe = nPaynumbe
					
					Do While Not .EOF
						lclsPremium_mo = New Premium_mo
						With lclsPremium_mo
							.nReceipt = lrecreaPremium_mo_o.FieldToClass("nReceipt")
							.nProduct = lrecreaPremium_mo_o.FieldToClass("nProduct")
							.nBranch = lrecreaPremium_mo_o.FieldToClass("nBranch")
							.sCertype = lrecreaPremium_mo_o.FieldToClass("sCertype")
							.nDigit = lrecreaPremium_mo_o.FieldToClass("nDigit")
							.nPaynumbe = lrecreaPremium_mo_o.FieldToClass("nPaynumbe")
							.nTransac = lrecreaPremium_mo_o.FieldToClass("nTransac")
							.nAmount = lrecreaPremium_mo_o.FieldToClass("nAmount")
							.nCard_type = lrecreaPremium_mo_o.FieldToClass("nCard_type")
							.sAux_accoun = lrecreaPremium_mo_o.FieldToClass("sAux_accoun")
							.nBalance = lrecreaPremium_mo_o.FieldToClass("nBalance")
							.nBank_code = lrecreaPremium_mo_o.FieldToClass("nBank_code")
							.nBordereaux = lrecreaPremium_mo_o.FieldToClass("nBordereaux")
							.dCar_datexp = lrecreaPremium_mo_o.FieldToClass("dCar_datexp")
							.sCard_num = lrecreaPremium_mo_o.FieldToClass("sCard_num")
							.nCash_mov = lrecreaPremium_mo_o.FieldToClass("nCash_mov")
							.nCause_amen = lrecreaPremium_mo_o.FieldToClass("nCause_amen")
							.sCessicoi = lrecreaPremium_mo_o.FieldToClass("sCessicoi")
							.sChang_acc = lrecreaPremium_mo_o.FieldToClass("sChang_acc")
							.dCompdate = lrecreaPremium_mo_o.FieldToClass("dCompdate")
							.nCurrency = lrecreaPremium_mo_o.FieldToClass("nCurrency")
							.sDocnumbe = lrecreaPremium_mo_o.FieldToClass("sDocnumbe")
							.sInd_rever = lrecreaPremium_mo_o.FieldToClass("sInd_rever")
							.nInt_mora = lrecreaPremium_mo_o.FieldToClass("nInt_mora")
							.sIntermei = lrecreaPremium_mo_o.FieldToClass("sIntermei")
							.nNullcode = lrecreaPremium_mo_o.FieldToClass("nNullcode")
							.sPay_form = lrecreaPremium_mo_o.FieldToClass("sPay_form")
							.dPosted = lrecreaPremium_mo_o.FieldToClass("dPosted")
							.nPremium = lrecreaPremium_mo_o.FieldToClass("nPremium")
							.nReceipt_fa = lrecreaPremium_mo_o.FieldToClass("nReceipt_fa")
							.dStatdate = lrecreaPremium_mo_o.FieldToClass("dStatdate")
							.sStatisi = lrecreaPremium_mo_o.FieldToClass("sStatisi")
							.nUsercode = lrecreaPremium_mo_o.FieldToClass("nUsercode")
							.dLedgerdat = lrecreaPremium_mo_o.FieldToClass("dLedgerdat")
							.nType = lrecreaPremium_mo_o.FieldToClass("nType")
							.nExchange = lrecreaPremium_mo_o.FieldToClass("nExchange")
							.sIndAssocPro = lrecreaPremium_mo_o.FieldToClass("sIndassocpro")
							.nPaysoondisc = lrecreaPremium_mo_o.FieldToClass("nPaysoondisc")
							.nBulletins = lrecreaPremium_mo_o.FieldToClass("nBulletins")
							.nCashnum = lrecreaPremium_mo_o.FieldToClass("nCashnum")
							.nBillnum = lrecreaPremium_mo_o.FieldToClass("nBillnum")
							.sBillType = lrecreaPremium_mo_o.FieldToClass("sBilltype")
							.nCollector = lrecreaPremium_mo_o.FieldToClass("nCollector")
							.sIndcheque = lrecreaPremium_mo_o.FieldToClass("sIndcheque")
						End With
						Call Add_COC009(lclsPremium_mo)
						.RNext()
						'UPGRADE_NOTE: Object lclsPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium_mo = Nothing
					Loop 
					.RCloseRec()
				Else
					FindCOC009 = False
					
					mdblReceipt = 0
					mlngProduct = 0
					mlngBranch = 0
					mstrCertype = ""
					mlngDigit = 0
					mlngPaynumbe = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreaPremium_mo_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPremium_mo_o = Nothing
		End If
		
FindCOC009_Err: 
		If Err.Number Then
			FindCOC009 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Find_CO009: Obtiene el o los recibos mas recientes (últimos que se han pagado de una relación)
	
	Public Function Find_CO009(ByVal nBordereaux As Double, ByVal nAll As Integer) As Boolean
		Dim lrecVdocument_pay_b As eRemoteDB.Execute
		Dim lclsPremium_mo As eCollection.Premium_mo
		Dim lclsValues As eFunctions.Values
		Dim llngIndex As Integer
		Dim lstrString As String = String.Empty
		Dim lstrStringOri As String
        Dim lstrStringMed As String = ""
        Dim lstrStringDes As String
		Dim lstrStringEnd As String
		Dim lstrCertype As String
		Dim ldblBordereaux As Double
		Dim ldblBordereauxPay As Double
		Dim lintCollecdoctyp As Integer
		Dim lintCollecdoctypDocPay As Integer
		Dim lintCollecDocTypDoc As Integer
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		Dim llngPolicy As Integer
		Dim llngCertif As Integer
		Dim llngReceipt As Integer
		Dim llngContrat As Integer
		Dim lintDraft As Integer
		Dim llngBulletins As Integer
		Dim llngProponum As Integer
		Dim lintIdmov As Integer
        Dim sDocumentOri As String = ""
        Dim sDocumentDes As String
		Dim lintPosIni As Integer
		Dim lintPosEnd As Integer
		Dim lintPosMed As Integer
		Dim lintReady As Integer
		
		On Error GoTo Find_CO009_Err
		
		lrecVdocument_pay_b = New eRemoteDB.Execute
		lclsValues = New eFunctions.Values
		'+
		'+ Definición de store procedure reaVdocument_pay_b al 03-25-2002 18:03:38
		'+
		With lrecVdocument_pay_b
			.StoredProcedure = "reaVdocument_pay_b"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				llngIndex = 1
				Do While Not .EOF
					lclsPremium_mo = New eCollection.Premium_mo
					ldblBordereaux = .FieldToClass("nBordereaux")
					lintCollecdoctyp = .FieldToClass("nCollecdoctyp")
					lintCollecDocTypDoc = .FieldToClass("nCollecDocTypDoc")
					lstrCertype = .FieldToClass("sCertype")
					lintBranch = .FieldToClass("nBranch")
					lintProduct = .FieldToClass("nProduct")
					llngPolicy = .FieldToClass("nPolicy")
					llngCertif = .FieldToClass("nCertif")
					llngReceipt = .FieldToClass("nReceipt")
					llngContrat = .FieldToClass("nContrat")
					lintDraft = .FieldToClass("nDraft")
					llngBulletins = .FieldToClass("nBulletins")
					llngProponum = .FieldToClass("nProponum")
					lintIdmov = .FieldToClass("nIdmov")
					
					lstrString = getInfDocumentLastPay(lstrCertype, lintBranch, lintProduct, llngPolicy, llngCertif, llngReceipt, llngContrat, lintDraft, llngBulletins, llngProponum, lintIdmov, ldblBordereaux, lintCollecdoctyp)
					
					Select Case lintCollecdoctyp
						Case 1 '+Recibo
							sDocumentOri = CStr(llngReceipt)
						Case 2 '+Cuota
							sDocumentOri = llngReceipt & "-" & llngContrat & "-" & lintDraft
						Case 3 '+Boletín
							sDocumentOri = llngBulletins & "(" & llngReceipt & ")"
						Case 4, 5 '+Prima adicional o Prima exceso
							sDocumentOri = lintBranch & "-" & lintProduct & "-" & llngPolicy & "-" & llngCertif
						Case 6 'Abono a préstamo
							sDocumentOri = lintBranch & "-" & lintProduct & "-" & llngPolicy & "-" & llngCertif & "-" & lintIdmov
						Case 7 'Propuesta
							sDocumentOri = lintBranch & "-" & lintProduct & "-" & llngProponum
					End Select
					
					lstrStringOri = lclsValues.getMessage(lintCollecdoctyp, "table5587")
					
					If lstrString <> String.Empty Then
						Find_CO009 = True
						
						lintPosIni = InStr(1, lstrString, ":")
						lintReady = CShort(Mid(lstrString, 2, lintPosIni - 2))


                        Select Case lintReady
                            Case 1 'Existen documentos posteriores del mismo (pagos parciales o cuotas subsiguientes)
                                lstrStringMed = eFunctions.Values.GetMessage(266)
                            Case 2 'Existen documentos con vía de pago PAC o Trasbank y que no han sido cobrados
                                lstrStringMed = eFunctions.Values.GetMessage(266)
                            Case 3 'Existen documentos posteriores de otro documento (documentos asociados a la póliza-certificado)
                                lstrStringMed = eFunctions.Values.GetMessage(266)
                        End Select
						
						lintPosMed = InStr(1, lstrString, "-")
						ldblBordereauxPay = CInt(Mid(lstrString, lintPosIni + 1, lintPosMed - (lintPosIni + 1)))
						lintPosEnd = InStr(1, lstrString, ")")
						
						lintCollecdoctypDocPay = CInt(Mid(lstrString, lintPosMed + 1, lintPosEnd - (lintPosMed + 1)))
						sDocumentDes = Mid(lstrString, lintPosEnd + 1, Len(lstrString) - (lintPosEnd))
						lstrStringDes = lclsValues.getMessage(lintCollecdoctypDocPay, "table5587")
						
						If ldblBordereaux <> ldblBordereauxPay Then
                            lclsPremium_mo.sString = eFunctions.Values.GetMessage(265) & ": " & lstrStringOri & "(" & sDocumentOri & ")   -> " & lstrStringMed & ": " & eFunctions.Values.GetMessage(229) & "(" & ldblBordereauxPay & ")"
                        Else
                            lclsPremium_mo.sString = eFunctions.Values.GetMessage(265) & ": " & lstrStringOri & "(" & sDocumentOri & ")   -> " & lstrStringMed & ": " & lstrStringOri & "(" & sDocumentDes & ")"
                        End If
					Else
                        lclsPremium_mo.sString = eFunctions.Values.GetMessage(264) & ": " & lstrStringOri & "(" & sDocumentOri & ")"
                    End If
					
					lclsPremium_mo.nIndex = llngIndex
					llngIndex = llngIndex + 1
					Call Add_CO009(lclsPremium_mo)
					'UPGRADE_NOTE: Object lclsPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPremium_mo = Nothing
					'+ Si se desea procesar todos los documentos encontrados (nAll = 1); si se desea procesar el primero que se encuentre (nAll = 0)
					If nAll <> 1 Then
						Exit Do
					End If
					.RNext()
				Loop 
			End If
		End With
		
Find_CO009_Err: 
		If Err.Number Then
			Find_CO009 = False
		End If
		'UPGRADE_NOTE: Object lrecVdocument_pay_b may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecVdocument_pay_b = Nothing
		On Error GoTo 0
	End Function
	
	'%getInfDocumentLastPay: Obtiene información del último documento pagado.
	Public Function getInfDocumentLastPay(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nBulletins As Double, ByVal nProponum As Double, ByVal nIdmov As Integer, ByVal nBordereaux As Double, ByVal nCollecDocTyp As Integer) As String
		Dim lrecPremium_mo As eRemoteDB.Execute
		
		On Error GoTo getInfDocumentLastPay_Err
		lrecPremium_mo = New eRemoteDB.Execute
		
		getInfDocumentLastPay = String.Empty
		
		With lrecPremium_mo
			.StoredProcedure = "reaDocument_LastMovPay"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdmov", nIdmov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecdoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReady", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereauxout", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecdoctypout", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocument", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				'+ Si se encontró información
				If .Parameters("nReady").Value > 0 Then
					'+ Se concatena la información. Formato ->  "(#nReady:#Relación-Tipo de documento)#Documento
					'+                                    1)Recibo  -> #recibo                                 */
					'+                                    2)Cuota   -> #Recibo-#Contrato-#Cuota                */
					'+                                    3)Boletin -> No se presenta, se trata como recibo/cuo*/
					'+                                    4)Prima a -> #Ramo-#Prod-#Poli-#Certif-#Movimiento   */
					'+                                    5)Prima e -> #Ramo-#Prod-#Poli-#Certif-#Mov-#Recibo  */
					'+                                    6)Abono   -> #Ramo-#Prod-#Poli-#Certif-#Mov          */
					'+                                    7)Propuest-> #Ramo-#Prod-#Propuesta                  */
					getInfDocumentLastPay = "(" & .Parameters("nReady").Value & ":" & .Parameters("nBordereauxOut").Value & "-" & .Parameters("nCollecDocTypOut").Value & ")" & .Parameters("sDocument").Value
				End If
			End If
		End With
		
getInfDocumentLastPay_Err: 
		If Err.Number Then
			getInfDocumentLastPay = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium_mo = Nothing
		On Error GoTo 0
	End Function
End Class






