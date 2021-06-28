Option Strict Off
Option Explicit On
Public Class Tab_am_Bils
	Implements System.Collections.IEnumerable
	Private mCol As Collection
	
	'%Add: añade un elemento a la colección que contiene una impresión de la tabla Tab_am_bil
	Public Function Add(ByRef lclsTab_am_bil As Tab_Am_Bil) As Tab_Am_Bil
		With lclsTab_am_bil
			mCol.Add(lclsTab_am_bil)
		End With
		'+ Return the object created
		Add = lclsTab_am_bil
		'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_bil = Nothing
	End Function
	
	'%Find: lee los elementos de la tabla Tab_am_bil por la clave de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sIllness As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_bil As eRemoteDB.Execute
		Dim lclsTab_am_bil As Tab_Am_Bil
		
		On Error GoTo Find_Err
		
		lrecreaTab_am_bil = New eRemoteDB.Execute
		
		With lrecreaTab_am_bil
			.StoredProcedure = "reaTab_am_bil"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_am_bil = New Tab_Am_Bil
					
					lclsTab_am_bil.sAuto_resist_h = .FieldToClass("sAuto_resist")
					lclsTab_am_bil.nLimit_h = .FieldToClass("nLimit_H", 0)
					lclsTab_am_bil.nPay_concep = .FieldToClass("nPay_concep")
					lclsTab_am_bil.nPrestac = .FieldToClass("nPrestac")
					lclsTab_am_bil.nAmoun_used = .FieldToClass("nAmoun_used")
					lclsTab_am_bil.nDed_amount = .FieldToClass("nDed_amount")
					lclsTab_am_bil.nDed_percen = .FieldToClass("nDed_percen")
					lclsTab_am_bil.nDed_quanti = .FieldToClass("nDed_quanti")
					lclsTab_am_bil.nDed_type = .FieldToClass("nDed_type")
					lclsTab_am_bil.nIndem_rate = .FieldToClass("nIndem_rate")
					lclsTab_am_bil.nLimit = .FieldToClass("nLimit")
					lclsTab_am_bil.nLimit_exe = .FieldToClass("nLimit_exe")
					lclsTab_am_bil.nCount = .FieldToClass("nCount")
					lclsTab_am_bil.nTyplim = .FieldToClass("nTypLim")
					lclsTab_am_bil.nPunish = .FieldToClass("nPunish")
					lclsTab_am_bil.sCaren_Type = .FieldToClass("sCaren_Type")
					lclsTab_am_bil.nCaren_Dur = .FieldToClass("nCaren_Dur")
					lclsTab_am_bil.NDED_QUANTI_2 = .FieldToClass("NDED_QUANTI_2")
					lclsTab_am_bil.NINDEM_RATE_2 = .FieldToClass("NINDEM_RATE_2")
					lclsTab_am_bil.NLIMIT_2 = .FieldToClass("NLIMIT_2")
					lclsTab_am_bil.NTYPLIM_2 = .FieldToClass("NTYPLIM_2")
					lclsTab_am_bil.NCOUNT_2 = .FieldToClass("NCOUNT_2")
					lclsTab_am_bil.NLIMIT_EXE_2 = .FieldToClass("NLIMIT_EXE_2")
					lclsTab_am_bil.NPUNISH_2 = .FieldToClass("NPUNISH_2")
					lclsTab_am_bil.SOTHERLIM = .FieldToClass("SOTHERLIM")
                    lclsTab_am_bil.sPay_concept = .FieldToClass("SPAY_CONCEP")
                    lclsTab_am_bil.sPrestac = .FieldToClass("SPRESTAC")
                    Call Add(lclsTab_am_bil)
                    'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsTab_am_bil = Nothing
                    .RNext()
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
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_bil = Nothing
	End Function
	Public Function reatab_am_bil_si025(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_bil As eRemoteDB.Execute
		Dim lclsTab_am_bil As Tab_Am_Bil
		
		On Error GoTo reatab_am_bil_si025_Err
		
		lrecreaTab_am_bil = New eRemoteDB.Execute
		
		With lrecreaTab_am_bil
			.StoredProcedure = "reatab_am_bil_si025"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reatab_am_bil_si025 = True
				Do While Not .EOF
					lclsTab_am_bil = New Tab_Am_Bil
					
					lclsTab_am_bil.sDescover = .FieldToClass("sDescover")
					lclsTab_am_bil.nCurrency = .FieldToClass("nCurrency")
					lclsTab_am_bil.nModulec = .FieldToClass("nModulec")
					lclsTab_am_bil.nCover = .FieldToClass("nCover")
					lclsTab_am_bil.nGroup = .FieldToClass("nGroup")
					lclsTab_am_bil.sReservstat = .FieldToClass("sReservstat")
					lclsTab_am_bil.nDamages = .FieldToClass("nDamages")
					lclsTab_am_bil.nFra_amount = .FieldToClass("nFra_amount")
					lclsTab_am_bil.nReserve = .FieldToClass("nReserve")
					lclsTab_am_bil.nDamprof = .FieldToClass("nDamprof")
					lclsTab_am_bil.nExchange = .FieldToClass("nExchange")
					lclsTab_am_bil.nCapital = .FieldToClass("nCapital")
					lclsTab_am_bil.sFrancapl = .FieldToClass("sFrancapl")
					lclsTab_am_bil.nBranch_est = .FieldToClass("nBranch_est")
					lclsTab_am_bil.nBranch_led = .FieldToClass("nBranch_led")
					lclsTab_am_bil.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsTab_am_bil.nLoc_pay_am = .FieldToClass("nLoc_pay_am")
					lclsTab_am_bil.nPay_amount = .FieldToClass("nPay_amount")
					lclsTab_am_bil.sAutomrep = .FieldToClass("sAutomrep")
					lclsTab_am_bil.nFixamount = .FieldToClass("nFixamount")
					lclsTab_am_bil.nMaxamount = .FieldToClass("nMaxamount")
					lclsTab_am_bil.nMinamount = .FieldToClass("nMinamount")
					lclsTab_am_bil.nRate = .FieldToClass("nRate")
					lclsTab_am_bil.nMedreser = .FieldToClass("nMedreser")
					lclsTab_am_bil.sRoureser = .FieldToClass("sRoureser")
					lclsTab_am_bil.sCacalili = .FieldToClass("sCacalili")
					lclsTab_am_bil.sKey = .FieldToClass("sKey")
					lclsTab_am_bil.sClient = .FieldToClass("sClient")
					lclsTab_am_bil.sBill_ind = .FieldToClass("sBill_ind")
					lclsTab_am_bil.nLimit_h = .FieldToClass("nLimit_h")
					lclsTab_am_bil.nPay_concep = .FieldToClass("nPay_concep")
					lclsTab_am_bil.nPrestac = .FieldToClass("nPrestac")
					lclsTab_am_bil.nAmoun_used = .FieldToClass("nAmoun_used")
					lclsTab_am_bil.nDed_amount = .FieldToClass("nDed_amount")
					lclsTab_am_bil.nDed_percen = .FieldToClass("nDed_percen")
					lclsTab_am_bil.nDed_quanti = .FieldToClass("nDed_quanti")
					lclsTab_am_bil.nDed_type = .FieldToClass("nDed_type")
					lclsTab_am_bil.nIndem_rate = .FieldToClass("nIndem_rate")
					lclsTab_am_bil.nLimit = .FieldToClass("nLimit")
					lclsTab_am_bil.nLimit_exe = .FieldToClass("nLimit_exe")
					lclsTab_am_bil.nCount = .FieldToClass("nCount")
					lclsTab_am_bil.nTyplim = .FieldToClass("nTyplim")
					lclsTab_am_bil.nPunish = .FieldToClass("nPunish")
					lclsTab_am_bil.sCaren_Type = .FieldToClass("sCaren_type")
					lclsTab_am_bil.nCaren_quan = .FieldToClass("nCaren_quan")
					
					Call Add(lclsTab_am_bil)
					'UPGRADE_NOTE: Object lclsTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_am_bil = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				reatab_am_bil_si025 = False
			End If
		End With
		
reatab_am_bil_si025_Err: 
		If Err.Number Then
			reatab_am_bil_si025 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_bil = Nothing
	End Function
	
	
	'* Item: Obtiene el valor del elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Am_Bil
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la creación de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






