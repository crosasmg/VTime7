Option Strict Off
Option Explicit On
Public Class Covers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Covers.cls                               $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 31/05/04 8:00p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Double
	Private mlngCertif As Double
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Cover) As Cover
		If objClass Is Nothing Then
			objClass = New Cover
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nGroup_insu & .nModulec & .nCover & .sClient & .dEffecdate.ToString("yyyyMMdd") & .sFree_premi)
			
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'%Find: Busca las coberturas de la poliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaCover_a As eRemoteDB.Execute
		Dim lclsCover As Cover
		
		On Error GoTo Find_Err
		
		If sCertype <> mstrCertype Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nPolicy <> mlngPolicy Or nCertif <> mlngCertif Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			
			'+ Definición de store procedure reaCover_a al 04-25-2002 17:28:03
			lrecreaCover_a = New eRemoteDB.Execute
			With lrecreaCover_a
				.StoredProcedure = "ReaCover_a"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Do While Not .EOF
						lclsCover = New Cover
						lclsCover.sCertype = .FieldToClass("sCertype")
						lclsCover.nBranch = .FieldToClass("nBranch")
						lclsCover.nProduct = .FieldToClass("nProduct")
						lclsCover.nPolicy = .FieldToClass("nPolicy")
						lclsCover.nCertif = .FieldToClass("nCertif")
						lclsCover.nGroup_insu = .FieldToClass("nGroup_insu")
						lclsCover.nModulec = .FieldToClass("nModulec")
						lclsCover.nCover = .FieldToClass("nCover")
						lclsCover.dEffecdate = .FieldToClass("dEffecdate")
						lclsCover.sClient = .FieldToClass("sClient")
						lclsCover.nRole = .FieldToClass("nRole")
						lclsCover.nCapital = .FieldToClass("nCapital")
						lclsCover.nCapitali = .FieldToClass("nCapitali")
						lclsCover.sChange = .FieldToClass("sChange")
						lclsCover.sFrandedi = .FieldToClass("sFrandedi")
						lclsCover.nCurrency = .FieldToClass("nCurrency")
						lclsCover.nDiscount = .FieldToClass("nDiscount")
						lclsCover.nFixamount = .FieldToClass("nFixamount")
						lclsCover.nMaxamount = .FieldToClass("nMaxamount")
						lclsCover.sFree_premi = .FieldToClass("sFree_premi")
						lclsCover.nMinamount = .FieldToClass("nMinamount")
						lclsCover.dNulldate = .FieldToClass("dNulldate")
						lclsCover.nPremium = .FieldToClass("nPremium")
						lclsCover.nRate = .FieldToClass("nRate")
						lclsCover.nWait_quan = .FieldToClass("nWait_quan")
						lclsCover.nRatecove = .FieldToClass("nRatecove")
						lclsCover.sWait_type = .FieldToClass("sWait_type")
						lclsCover.sFrancapl = .FieldToClass("sFrancapl")
						lclsCover.nDisc_Amoun = .FieldToClass("nDisc_amoun")
						lclsCover.nTypDurins = .FieldToClass("nTypdurins")
						lclsCover.nDurinsur = .FieldToClass("nDurinsur")
						lclsCover.nAgeminins = .FieldToClass("nAgeminins")
						lclsCover.nAgemaxins = .FieldToClass("nAgemaxins")
						lclsCover.nAgemaxper = .FieldToClass("nAgemaxper")
						lclsCover.nTypDurpay = .FieldToClass("nTypdurpay")
						lclsCover.nDurpay = .FieldToClass("nDurpay")
						lclsCover.nCauseupd = .FieldToClass("nCauseupd")
						lclsCover.nCapital_wait = .FieldToClass("nCapital_wait")
						lclsCover.nAgelimit = .FieldToClass("nAgelimit")
						lclsCover.nAge_per = .FieldToClass("nAge_per")
						lclsCover.dAniversary = .FieldToClass("dAniversary")
						lclsCover.dSeektar = .FieldToClass("dSeektar")
						lclsCover.dFer = .FieldToClass("dFer")
						lclsCover.nBranch_rei = .FieldToClass("nBranch_rei")
						lclsCover.nRetarif = .FieldToClass("nRetarif")
						lclsCover.sDepend = .FieldToClass("sDepend")
						lclsCover.sDescript = .FieldToClass("sDescript")
						
						Call Add(lclsCover)
						'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCover = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = dEffecdate
				End If
			End With
		Else
			Find = True
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaCover_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCover_a = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_CovSI001: Busca las coberturas de la poliza
	Public Function Find_CovSI001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaCover_a As eRemoteDB.Execute
		Dim lclsCover As Cover
		
		On Error GoTo Find_Err
		
		lrecreaCover_a = New eRemoteDB.Execute
		With lrecreaCover_a
			.StoredProcedure = "REACOVER_DATA"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CovSI001 = True
				Do While Not .EOF
					lclsCover = New Cover
					lclsCover.nCover = .FieldToClass("nCover")
					lclsCover.nCapital = .FieldToClass("nCapital")
					lclsCover.sShort_Des = .FieldToClass("sCurrency")
					lclsCover.sDescript = .FieldToClass("Descrip")
					
					Call AddCover(lclsCover)
					'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_CovSI001 = False
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find_CovSI001 = False
		End If
		'UPGRADE_NOTE: Object lrecreaCover_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCover_a = Nothing
		On Error GoTo 0
	End Function
	
	'%AddCover: Agrega un nuevo registro a la colección
	Public Function AddCover(ByRef objClass As Cover) As Cover
		If objClass Is Nothing Then
			objClass = New Cover
		End If
		
		With objClass
			mCol.Add(objClass, .nCover)
		End With
		
		'Return the object created
		AddCover = objClass
	End Function
	
	'%FindSI813: Obtiene las coberturas de la póliza para la SI813
	Public Function FindSI813(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sInd As String, ByVal nUsercode As Integer, ByVal nSessionId As String) As Boolean
		Dim lrecinsReasi813 As eRemoteDB.Execute
		Dim lcolCovers As TCovers
		Dim lclsCover As Cover
		Dim lstrKey As String
		
		On Error GoTo FindSI813_Err
		lcolCovers = New TCovers
		lstrKey = lcolCovers.sKey(nUsercode, nSessionId)
		'UPGRADE_NOTE: Object lcolCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCovers = Nothing
		
		'+ Definición de store procedure insReasi813 al 04-26-2002 12:56:13
		lrecinsReasi813 = New eRemoteDB.Execute
		With lrecinsReasi813
			.StoredProcedure = "InsReaSI813"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd", sInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindSI813 = True
				Do While Not .EOF
					lclsCover = New Cover
					lclsCover.sCertype = .FieldToClass("sCertype")
					lclsCover.nBranch = .FieldToClass("nBranch")
					lclsCover.nProduct = .FieldToClass("nProduct")
					lclsCover.nPolicy = .FieldToClass("nPolicy")
					lclsCover.nCertif = .FieldToClass("nCertif")
					lclsCover.nGroup = .FieldToClass("nGroup")
					lclsCover.nModulec = .FieldToClass("nModulec")
					lclsCover.nCover = .FieldToClass("nCover")
					lclsCover.dEffecdate = .FieldToClass("dEffecdate")
					lclsCover.nCapital = .FieldToClass("nCapital")
					lclsCover.nCurrency = .FieldToClass("nCurrency")
					lclsCover.sDescript = .FieldToClass("sDescript")
					lclsCover.nRole = .FieldToClass("nRole")
					lclsCover.sClient = .FieldToClass("sClient")
					lclsCover.sDefaulti = .FieldToClass("sDefaulti")
					lclsCover.sDepend = .FieldToClass("sDepend")
					lclsCover.nActionCov = .FieldToClass("nActioncov")
					lclsCover.sFree_premi = .FieldToClass("sFree_premi")
					
					Call Add(lclsCover)
					'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindSI813_Err: 
		If Err.Number Then
			FindSI813 = False
		End If
		'UPGRADE_NOTE: Object lrecinsReasi813 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReasi813 = Nothing
		'UPGRADE_NOTE: Object lcolCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCovers = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cover
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






