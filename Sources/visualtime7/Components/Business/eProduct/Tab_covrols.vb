Option Strict Off
Option Explicit On
Public Class Tab_covrols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_covrols.cls                          $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 6/02/06 11:00                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByRef objClass As Tab_covrol) As Tab_covrol
		If objClass Is Nothing Then
			objClass = New Tab_covrol
		End If
		
		With objClass
			mCol.Add(objClass, "T" & .nBranch & .nProduct & .nModulec & .nCover & .nRole & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaTab_covrol_a As eRemoteDB.Execute
		Dim lclsTab_covrol As Tab_covrol
		
		On Error GoTo Find_Err
		
		lrecReaTab_covrol_a = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTab_covrol_a'
		'+Información leída el 31/10/01
		With lrecReaTab_covrol_a
			.StoredProcedure = "ReaTab_covrol_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_covrol = New Tab_covrol
					lclsTab_covrol.nRole = .FieldToClass("nRole")
					lclsTab_covrol.sDescrole = .FieldToClass("sDescrole")
					lclsTab_covrol.sSel = .FieldToClass("sSel")
					lclsTab_covrol.nBranch = .FieldToClass("nBranch")
					lclsTab_covrol.nProduct = .FieldToClass("nProduct")
					lclsTab_covrol.nModulec = .FieldToClass("nModulec")
					lclsTab_covrol.nCover = .FieldToClass("nCover")
					lclsTab_covrol.dEffecdate = .FieldToClass("dEffecdate")
					lclsTab_covrol.nRolcap = .FieldToClass("nRolcap")
					lclsTab_covrol.sRequired = .FieldToClass("sRequired")
					lclsTab_covrol.sDefaulti = .FieldToClass("sDefaulti")
					lclsTab_covrol.sRoupremi = .FieldToClass("sRoupremi")
					lclsTab_covrol.nAgemininsm = .FieldToClass("nAgemininsm")
					lclsTab_covrol.nAgemaxinsm = .FieldToClass("nAgemaxinsm")
					lclsTab_covrol.sRout_pay = .FieldToClass("sRout_pay")
					lclsTab_covrol.nAgemaxperm = .FieldToClass("nAgemaxperm")
					lclsTab_covrol.nAgemininsf = .FieldToClass("nAgemininsf")
					lclsTab_covrol.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
					lclsTab_covrol.nAgemaxperf = .FieldToClass("nAgemaxperf")
					lclsTab_covrol.dNulldate = .FieldToClass("dNulldate")
					lclsTab_covrol.nCacalcov = .FieldToClass("nCacalcov")
					lclsTab_covrol.nCacalfix = .FieldToClass("nCacalfix")
					lclsTab_covrol.sCacaltyp = .FieldToClass("sCacaltyp")
					lclsTab_covrol.nCacalmul = .FieldToClass("nCacalmul")
					lclsTab_covrol.nCapbaspe = .FieldToClass("nCapbaspe")
					lclsTab_covrol.nCapmaxim = .FieldToClass("nCapmaxim")
					lclsTab_covrol.nCapminim = .FieldToClass("nCapminim")
					lclsTab_covrol.nCover_in = .FieldToClass("nCover_in")
					lclsTab_covrol.nRolprem = .FieldToClass("nRolprem")
					lclsTab_covrol.nPremirat = .FieldToClass("nPremirat")
					lclsTab_covrol.nNotenum = .FieldToClass("nNotenum")
					lclsTab_covrol.nDuratInd = .FieldToClass("nDuratind")
					lclsTab_covrol.sRechapri = .FieldToClass("sRechapri")
					lclsTab_covrol.sRenewali = .FieldToClass("sRenewali")
					lclsTab_covrol.sRouchaca = .FieldToClass("sRouchaca")
					lclsTab_covrol.sRouchapr = .FieldToClass("sRouchapr")
					lclsTab_covrol.nDuratPay = .FieldToClass("nDuratpay")
					lclsTab_covrol.sRevIndex = .FieldToClass("sRevindex")
					lclsTab_covrol.sRouprcal = .FieldToClass("sRouprcal")
					lclsTab_covrol.nFrancFix = .FieldToClass("nFrancfix")
					lclsTab_covrol.sFrancApl = .FieldToClass("sFrancapl")
					lclsTab_covrol.nFrancMax = .FieldToClass("nFrancmax")
					lclsTab_covrol.nFrancMin = .FieldToClass("nFrancmin")
					lclsTab_covrol.nFrancrat = .FieldToClass("nFrancrat")
					lclsTab_covrol.sRoufranc = .FieldToClass("sRoufranc")
					lclsTab_covrol.sFrantype = .FieldToClass("sFrantype")
					lclsTab_covrol.sFDRequire = .FieldToClass("sFdrequire")
					lclsTab_covrol.sFDChantyp = .FieldToClass("sFdchantyp")
					lclsTab_covrol.nFDUserLev = .FieldToClass("nFduserlev")
					lclsTab_covrol.nFDRateAdd = .FieldToClass("nFdrateadd")
					lclsTab_covrol.nFDRateSub = .FieldToClass("nFdratesub")
					lclsTab_covrol.nCamaxper = .FieldToClass("nCamaxper")
					lclsTab_covrol.nCamaxcov = .FieldToClass("nCamaxcov")
					lclsTab_covrol.nCamaxrol = .FieldToClass("nCamaxrol")
					lclsTab_covrol.sRoutineCC = .FieldToClass("sRoutinecc")
					lclsTab_covrol.nRateCC = .FieldToClass("nRatecc")
					lclsTab_covrol.nAmountCC = .FieldToClass("nAmountcc")
					lclsTab_covrol.sApplyCC = .FieldToClass("sApplycc")
					lclsTab_covrol.nChPreLev = .FieldToClass("nChprelev")
					lclsTab_covrol.nChCapLev = .FieldToClass("nChcaplev")
					lclsTab_covrol.nRateCapAdd = .FieldToClass("nRatecapadd")
					lclsTab_covrol.nRateCapSub = .FieldToClass("nRatecapsub")
					lclsTab_covrol.sChtypcap = .FieldToClass("sChtypcap")
					lclsTab_covrol.nRatePreAdd = .FieldToClass("nRatepreadd")
					lclsTab_covrol.nRatePreSub = .FieldToClass("nRatepresub")
					lclsTab_covrol.sChangetyp = .FieldToClass("sChangetyp")
					lclsTab_covrol.sStatregt = .FieldToClass("sStatregt")
					lclsTab_covrol.sClaccidi = .FieldToClass("sClaccidi")
					lclsTab_covrol.sCldeathi = .FieldToClass("sCldeathi")
					lclsTab_covrol.sClincapi = .FieldToClass("sClincapi")
					lclsTab_covrol.sClinvali = .FieldToClass("sClinvali")
					lclsTab_covrol.sClsurvii = .FieldToClass("sClsurvii")
					lclsTab_covrol.sClvehaci = .FieldToClass("sClvehaci")
					lclsTab_covrol.sCliIllness = .FieldToClass("sCliillness")
					lclsTab_covrol.nTypdurpay = .FieldToClass("nTypdurpay")
					lclsTab_covrol.nTypdurins = .FieldToClass("nTypdurins")
					lclsTab_covrol.sCaren_type = .FieldToClass("sCaren_type")
					lclsTab_covrol.nCaren_quan = .FieldToClass("nCaren_quan")
					lclsTab_covrol.nMax_role = .FieldToClass("nMax_role")
					lclsTab_covrol.nMaxrent = .FieldToClass("nMaxrent")
					lclsTab_covrol.nPremifix = .FieldToClass("nPremifix")
					lclsTab_covrol.nPremimax = .FieldToClass("nPremimax")
					lclsTab_covrol.nRolActiv_rel = .FieldToClass("nRolActiv_rel")
					lclsTab_covrol.nCovActiv_rel = .FieldToClass("nCovActiv_rel")
					lclsTab_covrol.sLeg = .FieldToClass("sLeg")
					lclsTab_covrol.nQmonth_vig = .FieldToClass("nQmonth_vig")
					lclsTab_covrol.nQbetweenmod = .FieldToClass("nQbetweenmod")
					lclsTab_covrol.nQmax_mod = .FieldToClass("nQmax_mod")
                    lclsTab_covrol.sRourate = .FieldToClass("sRourate")

                    Call Add(lclsTab_covrol)
					'UPGRADE_NOTE: Object lclsTab_covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_covrol = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTab_covrol_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_covrol_a = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_covrol
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






