Option Strict Off
Option Explicit On
Public Class T_bulletins_dets
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: T_bulletins_dets.cls                     $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 20/10/03 4:58p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add_CO632(ByVal objClass As T_bulletins_det) As T_bulletins_det
		If objClass Is Nothing Then
			objClass = New T_bulletins_det
		End If
		
		With objClass
			mCol.Add(objClass, "CO" & .nBulletins & .nId)
		End With
		
		'Return the object created
		Add_CO632 = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_bulletins_det
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
	
	'%Find: Lee los datos de la tabla
	Public Function Find_CO632(ByVal nAction As Integer, ByVal dCollectdate As Date, ByVal sIndColl_exp As String, ByVal sStyle_bull As String, ByVal sQueryOption As String, ByVal nBulletins As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nReceipt As Double, ByVal nInsur_area As Integer, ByVal nOneTime As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lrecreaT_bulletins_det As eRemoteDB.Execute
		Dim lclsT_bulletins_det As eCollection.T_bulletins_det
		Dim llngCount As Integer
		Dim sArrClient() As String
		
		On Error GoTo Find_CO632_Err
		
		Find_CO632 = True
		
		lrecreaT_bulletins_det = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaT_bulletins_det_a'
		With lrecreaT_bulletins_det
			.StoredProcedure = "insReaCO632"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectDate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndColl_exp", sIndColl_exp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_bull", sStyle_bull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQueryOption", sQueryOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOnetime", nOneTime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not (.EOF Or llngCount > 100)
					
					lclsT_bulletins_det = New eCollection.T_bulletins_det
					
					lclsT_bulletins_det.sSel = .FieldToClass("sSel")
					lclsT_bulletins_det.nBulletins = .FieldToClass("nBulletins")
					lclsT_bulletins_det.nId = .FieldToClass("nId")
					lclsT_bulletins_det.nCollecDocTyp = .FieldToClass("nCollecdoctyp")
					lclsT_bulletins_det.dCollectdate = .FieldToClass("dCollectDate")
					lclsT_bulletins_det.sCertype = .FieldToClass("sCertype")
					lclsT_bulletins_det.nBranch = .FieldToClass("nBranch")
					lclsT_bulletins_det.nProduct = .FieldToClass("nProduct")
					lclsT_bulletins_det.nPolicy = .FieldToClass("nPolicy")
					lclsT_bulletins_det.nCertif = .FieldToClass("nCertif")
					lclsT_bulletins_det.nReceipt = .FieldToClass("nReceipt")
					lclsT_bulletins_det.nDigit = .FieldToClass("nDigit")
					lclsT_bulletins_det.nPaynumbe = .FieldToClass("nPaynumbe")
					lclsT_bulletins_det.nContrat = .FieldToClass("nContrat")
					lclsT_bulletins_det.nDraft = .FieldToClass("nDraft")
					lclsT_bulletins_det.sClient = .FieldToClass("sClient")
					sArrClient = Microsoft.VisualBasic.Split(.FieldToClass("sClieInfo"), "|")
					lclsT_bulletins_det.sCliename = sArrClient(2)
					lclsT_bulletins_det.sClieDigit = sArrClient(1)
					lclsT_bulletins_det.dStatdate = .FieldToClass("dStatdate")
					lclsT_bulletins_det.dExpirDat = .FieldToClass("dExpirdat")
					lclsT_bulletins_det.dLimitdate = .FieldToClass("dLimitdate")
					lclsT_bulletins_det.nType = .FieldToClass("nType")
					lclsT_bulletins_det.nTratypei = .FieldToClass("nTratypei")
					lclsT_bulletins_det.nCurrency = .FieldToClass("nCurrency")
					lclsT_bulletins_det.nAmount = .FieldToClass("nAmount")
					lclsT_bulletins_det.nCod_Agree = .FieldToClass("nCod_agree")
					lclsT_bulletins_det.sIndColl_exp = .FieldToClass("sIndColl_exp")
					lclsT_bulletins_det.sStyle_bull = .FieldToClass("sStyle_bull")
					lclsT_bulletins_det.sQueryOption = .FieldToClass("sQueryOption")
					lclsT_bulletins_det.sCollector = .FieldToClass("sCollector")
					lclsT_bulletins_det.nInsur_area = .FieldToClass("nInsur_area")
					
					Call Add_CO632(lclsT_bulletins_det)
					llngCount = llngCount + 1
					'UPGRADE_NOTE: Object lclsT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsT_bulletins_det = Nothing
					.RNext()
				Loop 
			Else
				Find_CO632 = False
			End If
		End With
		
Find_CO632_Err: 
		If Err.Number Then
			Find_CO632 = False
		End If
		'UPGRADE_NOTE: Object lrecreaT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaT_bulletins_det = Nothing
		On Error GoTo 0
	End Function
	
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






