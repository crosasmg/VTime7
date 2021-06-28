Option Strict Off
Option Explicit On
Public Class TConvertionss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TConvertionss.cls                        $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 14-08-09 11:24                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As TConvertions) As TConvertions
		If objClass Is Nothing Then
			objClass = New TConvertions
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nIndex & .nBranch & .nProduct & .nPolicy & .nCertif & .nServ_order)
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TConvertions
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Find: Lee los datos de las tablas certificat, policy, policy_his, prof_ord
	Public Function Find(ByVal sKey As String, Optional ByVal nFirstRecord As Integer = 0, Optional ByVal nLastRecord As Integer = 0) As Boolean
		Dim lrecReaTConvertions As eRemoteDB.Execute
		Dim lclsTConvertions As TConvertions
		Dim lstrCertype As String
		
		On Error GoTo Find_Err
		
		Find = True
		
		lrecReaTConvertions = New eRemoteDB.Execute
		
		With lrecReaTConvertions
			.StoredProcedure = "insReaPropQuotPkg.insReaPropQuot"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstRecord", nFirstRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastRecord", nLastRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTConvertions = New TConvertions
					lclsTConvertions.nIndex = .FieldToClass("nIndex")
					lclsTConvertions.nProponum = .FieldToClass("nProponum")
					lclsTConvertions.nPolicy = .FieldToClass("nPolicy")
					lclsTConvertions.sPen_doc = .FieldToClass("sPen_doc")
					lclsTConvertions.nNum_doc = .FieldToClass("nNum_doc")
					lclsTConvertions.dDate_init = .FieldToClass("dDate_init")
					lclsTConvertions.nStatus = .FieldToClass("nStatus")
					lclsTConvertions.sStatus = .FieldToClass("sStatus")
					lclsTConvertions.dStat_date = .FieldToClass("dStat_date")
					lclsTConvertions.nNullcode = .FieldToClass("nNullcode")
					lclsTConvertions.dEffecdate = .FieldToClass("dEffecdate")
					lclsTConvertions.dExpirdat = .FieldToClass("dExpirdat")
					lclsTConvertions.dLimit_date = .FieldToClass("dLimit_date")
					lclsTConvertions.sObserv = .FieldToClass("sObserv")
					lclsTConvertions.nServ_order = .FieldToClass("nServ_order")
					lclsTConvertions.nStatus_ord = .FieldToClass("nStatus_ord")
					lclsTConvertions.sStatus_ord = .FieldToClass("sStatus_ord")
					lclsTConvertions.nBordereaux = .FieldToClass("nBordereaux")
					lclsTConvertions.nFirst_prem = .FieldToClass("nFirst_prem")
					lclsTConvertions.nPrem_curr = .FieldToClass("nPrem_curr")
					lclsTConvertions.sPrem_currDesc = .FieldToClass("sPrem_currdesc")
					lclsTConvertions.sPrem_che = .FieldToClass("sPrem_che")
					lclsTConvertions.sPay_order = .FieldToClass("sPay_order")
					lclsTConvertions.nExpenses = .FieldToClass("nExpenses")
					lclsTConvertions.sDevolut = .FieldToClass("sDevolut")
					lclsTConvertions.sCertype = .FieldToClass("sCertype")
					lclsTConvertions.nBranch = .FieldToClass("nBranch")
					lclsTConvertions.nProduct = .FieldToClass("nProduct")
					lclsTConvertions.nOrigin = .FieldToClass("nOrigin")
					lclsTConvertions.sClient = .FieldToClass("sClient")
					lclsTConvertions.nCertif = .FieldToClass("nCertif")
					lclsTConvertions.sCliename = .FieldToClass("sCliename")
					lclsTConvertions.nNo_convers = .FieldToClass("nNo_convers")
					lclsTConvertions.sCon_descript = .FieldToClass("sCon_descript")
					lclsTConvertions.nWait_code = .FieldToClass("nWait_code")
					lclsTConvertions.sWai_descript = .FieldToClass("sWai_descript")
					lclsTConvertions.nPol_quot = .FieldToClass("nPol_Quot")
					lclsTConvertions.nType_amend = .FieldToClass("nType_amend")
					lclsTConvertions.sType_amend = .FieldToClass("sType_amend")
					lclsTConvertions.nExchange = .FieldToClass("nExchange")
					lclsTConvertions.nOrig_prem = .FieldToClass("nOrig_prem")
					lclsTConvertions.sOrig_curr = .FieldToClass("sOrig_curr")
					lclsTConvertions.nOffice = .FieldToClass("nOffice")
					lclsTConvertions.nOfficeAgen = .FieldToClass("nOfficeAgen")
					lclsTConvertions.nAgency = .FieldToClass("nAgency")
					lclsTConvertions.spenstatus_pol = .FieldToClass("spenstatus_pol")
					Call Add(lclsTConvertions)
					.RNext()
					'UPGRADE_NOTE: Object lclsTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTConvertions = Nothing
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
		'UPGRADE_NOTE: Object lrecReaTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTConvertions = Nothing
		'UPGRADE_NOTE: Object lclsTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTConvertions = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: elimina la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Lee los datos de las tablas certificat, policy, policy_his, prof_ord
	Public Function CreTconvertions(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal nOrigin As Integer = 0, Optional ByVal sTypeDoc As String = "", Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nProponum As Double = 0, Optional ByVal sClient As String = "", Optional ByVal nStatus As Integer = 0, Optional ByVal nIntermed As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sExpired As String = "", Optional ByVal nOperat As Integer = 0, Optional ByVal bFind As Boolean = False, Optional ByVal nWait_code As Integer = 0, Optional ByVal nFirstRecord As Integer = 0, Optional ByVal nLastRecord As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sCodispl As String = "", Optional ByVal sCodispl_orig As String = "", Optional ByVal dLastdate As Date = #12:00:00 AM#, Optional ByVal sApplyCostFP As String = "", Optional ByVal nExpenses As Integer = 0, Optional ByVal nRoutine As Integer = 0, Optional ByVal nHealthexp As Integer = 0) As String
		Dim lrecReaTConvertions As eRemoteDB.Execute
		Dim lclsTConvertions As TConvertions
		Dim lstrCertype As String
		Dim sKey As String
		
		On Error GoTo CreTconvertions_Err
		
		CreTconvertions = CStr(True)
		
		lrecReaTConvertions = New eRemoteDB.Execute
		
		'+ Se crea el sCertype para la lectura
		If nOrigin <> 0 And nOrigin <> eRemoteDB.Constants.intNull Then
			lclsTConvertions = New TConvertions
			lstrCertype = lclsTConvertions.CertypeByOrigin(nOrigin, sTypeDoc)
		Else
			'+Cotizacion
			If sTypeDoc = "1" Then
				lstrCertype = CStr(Constantes.ePolCertype.cstrQuotation)
				'+Propuesta
			Else
				lstrCertype = CStr(eCollection.Premium.TypeRecord.cstrRequest)
			End If
		End If
		With lrecReaTConvertions
			.StoredProcedure = "insReaPropQuotPkg.Cre_tconvertions"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExpired", sExpired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstRecord", nFirstRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastRecord", nLastRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl_orig", sCodispl_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastdate", dLastdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApplyCostFP", sApplyCostFP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoutine", nRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				sKey = .Parameters("skey").Value
				CreTconvertions = sKey
			Else
				CreTconvertions = ""
			End If
		End With
		
CreTconvertions_Err: 
		If Err.Number Then
			CreTconvertions = ""
		End If
		'UPGRADE_NOTE: Object lrecReaTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTConvertions = Nothing
		'UPGRADE_NOTE: Object lclsTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTConvertions = Nothing
		On Error GoTo 0
	End Function
End Class






