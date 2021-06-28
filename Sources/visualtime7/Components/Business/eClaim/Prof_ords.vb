Option Strict Off
Option Explicit On
Public Class Prof_ords
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Prof_ords.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private mOldClaim As Double
	'**insMaxServ_order: obtein the number of the maximum service .
	'insMaxServ_order: Obtiene el número de orden de servicio máxima.
	Public Function insMaxServ_order() As Integer
		Dim lrecreaMaxServ_order As eRemoteDB.Execute
		lrecreaMaxServ_order = New eRemoteDB.Execute
		
		With lrecreaMaxServ_order
			.StoredProcedure = "reaMaxServ_order"
			If .Run Then
				insMaxServ_order = .FieldToClass("MaxServ_order") + 1
				.RCloseRec()
			Else
				insMaxServ_order = 1
			End If
		End With
		lrecreaMaxServ_order = Nothing
		
	End Function
	
	
	'**%Add: add an element to the collection
	'% Add: Agrega un elemento a la colección
	Public Function Add(ByVal nAction As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nServ_Order As Double) As Prof_ord
		
		
		Dim objNewMember As eClaim.Prof_ord
		objNewMember = New eClaim.Prof_ord
		
		With objNewMember
			.nAction = nAction
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nServ_Order = nServ_Order
		End With
		
		mCol.Add(objNewMember, "PO" & nClaim & nCase_num & nServ_Order & nAction)
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	'**%Update: updates the collection
	'%Update: Actualizaciones de la colección
	Public Function Update() As Boolean
		Dim lclsProf_ord As eClaim.Prof_ord
		
		Update = True
		
		For	Each lclsProf_ord In mCol
			With lclsProf_ord
				If mOldClaim = eRemoteDB.Constants.intNull Then
					mOldClaim = .nClaim
				End If
				If .nAction = 1 Or .nAction = 2 Then
					Update = .Update_ProfOrdGeneric
				End If
			End With
		Next lclsProf_ord
	End Function
	'**%Find: obtains the data of the class properties
	'%Find: Obtener los datos de las propiedades de la clase
	'------------------------------------------ --------------------------------------------------
	Public Function Find(ByVal nClaim As Double) As Boolean
		Dim lrecProf_ord As eRemoteDB.Execute
		Dim lclsProf_ord As eClaim.Prof_ord
		
		On Error GoTo Find_Err
		lclsProf_ord = New eClaim.Prof_ord
		lrecProf_ord = New eRemoteDB.Execute
		
		With lrecProf_ord
			.StoredProcedure = "reaProf_ord"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				mCol = Nothing
				mCol = New Collection
				Do While Not .EOF
					lclsProf_ord = Add(0, .FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nServ_order"))
					With lclsProf_ord
                        .nDeman_Type = lrecProf_ord.FieldToClass("nDeman_Type")
                        .nCover = lrecProf_ord.FieldToClass("nCover")
                        .nModulec = lrecProf_ord.FieldToClass("nModulec")
                        .nInspector = lrecProf_ord.FieldToClass("nInspector")
                        .nTransac = lrecProf_ord.FieldToClass("nTransac")
						.nServ_Order = lrecProf_ord.FieldToClass("nServ_Order")
						.nAmount = lrecProf_ord.FieldToClass("nAmount")
						.nCurrency = lrecProf_ord.FieldToClass("nCurrency")
						.dDate_done = lrecProf_ord.FieldToClass("dDate_Done")
						.dFec_prog = lrecProf_ord.FieldToClass("dFec_Prog")
						.dAssigndate = lrecProf_ord.FieldToClass("dAssigndate")
						.dMade_date = lrecProf_ord.FieldToClass("dMade_Date")
						.nProvider = lrecProf_ord.FieldToClass("nProvider")
						.sMade_time = lrecProf_ord.FieldToClass("sMade_Time")
						.nStatus_ord = IIf(lrecProf_ord.FieldToClass("nStatus_ord") = eRemoteDB.Constants.intNull, 0, lrecProf_ord.FieldToClass("nStatus_ord"))
						.sTime_prog = lrecProf_ord.FieldToClass("sTime_Prog")
						.nWorksh = lrecProf_ord.FieldToClass("nWorksh")
						.nOrdertype = lrecProf_ord.FieldToClass("nOrderType")
						.nNoteorder = lrecProf_ord.FieldToClass("nNoteorder")
						.sClient = lrecProf_ord.FieldToClass("sClient")
						.sProviderName = lrecProf_ord.FieldToClass("Providername")
						.sWsdeduc = lrecProf_ord.FieldToClass("sWsDeduc")
						.nNotenum = lrecProf_ord.FieldToClass("nNotenum")
						.sCase = lrecProf_ord.FieldToClass("sCase")
						.sWorksh = lrecProf_ord.FieldToClass("nWorkshname")
						.nBranch = eRemoteDB.Constants.intNull
						.nProduct = eRemoteDB.Constants.intNull
						.sCerType = CStr(eRemoteDB.Constants.strNull)
						.nPolicy = eRemoteDB.Constants.intNull
						.nCertif = eRemoteDB.Constants.intNull
						.sStaclaim = CStr(eRemoteDB.Constants.strNull)
						.sStaReserve = CStr(eRemoteDB.Constants.strNull)
						.sBrancht = CStr(eRemoteDB.Constants.strNull)
						.sDes_status = CStr(eRemoteDB.Constants.strNull)
						.sDes_branch = CStr(eRemoteDB.Constants.strNull)
						.sDes_product = CStr(eRemoteDB.Constants.strNull)
					End With
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		lrecProf_ord = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	'**%Find_provider: obtains properties data of the class accourding to the professional
	'%Find_provider: Obtener los datos de las propiedades de la clase de acuerdo al profesional
	'                y los restantes parámetros ingresados en el Header de la transacción SI021
	'------------------------------------------ -----------------------------------------------
	Public Function Find_Provider(ByVal nProvider As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nProponum As Integer, ByVal nCertif As Integer, ByVal nClaim As Double, ByVal nOffice As Integer, ByVal nOrdertype As Integer, ByVal nStatus_ord As Integer, ByVal dFec_prog As Date) As Boolean
		Dim lintIndex As Integer
		Dim lrecProf_ord As eRemoteDB.Execute
		Dim lclsProf_ord As eClaim.Prof_ord
        Dim lstrCertype As String = ""

        On Error GoTo Find_Provider_Err
		
		lrecProf_ord = New eRemoteDB.Execute
		lclsProf_ord = New eClaim.Prof_ord
		
		If nProponum <> 0 Then
			lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypeProposal)
		End If
		
		If nPolicy <> 0 Then
			lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypePolicy)
		End If
		
		If nClaim <> 0 Then
			lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypeClaim)
		End If
		
		If nProvider = eRemoteDB.Constants.intNull Then
			nProvider = 0
		End If
		
		If nBranch = eRemoteDB.Constants.intNull Then
			nBranch = 0
		End If
		
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		
		If nCertif = eRemoteDB.Constants.intNull Then
			nCertif = 0
		End If
		
		If nPolicy = eRemoteDB.Constants.intNull Then
			nPolicy = 0
		End If
		
		If nProponum = eRemoteDB.Constants.intNull Then
			nProponum = 0
		End If
		
		If nClaim = eRemoteDB.Constants.intNull Then
			nClaim = 0
		End If
		
		If nOffice = eRemoteDB.Constants.intNull Then
			nOffice = 0
		End If
		
		If nOrdertype = eRemoteDB.Constants.intNull Then
			nOrdertype = 0
		End If
		
		If nStatus_ord = eRemoteDB.Constants.intNull Then
			nStatus_ord = 0
		End If
		
		With lrecProf_ord
			.StoredProcedure = "insReaProf_ord"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrderType", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				mCol = Nothing
				mCol = New Collection
				Do While Not .EOF
					lclsProf_ord = Add(lintIndex, .FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nServ_order"))
					With lclsProf_ord
						.nProvider = lrecProf_ord.FieldToClass("nProvider")
						.sProviderName = lrecProf_ord.FieldToClass("sProviderName")
						.sOfficeName = lrecProf_ord.FieldToClass("sOffice")
						.nClaim = lrecProf_ord.FieldToClass("nClaim")
						.nOrdClass = lrecProf_ord.FieldToClass("nOrdClass")
						.nBranch = lrecProf_ord.FieldToClass("nBranch")
						.nProduct = lrecProf_ord.FieldToClass("nProduct")
						
						Select Case lstrCertype
							Case CStr(Prof_ord.eOrdClass.cstrCertypeProposal)
								.nProponum = lrecProf_ord.FieldToClass("nPolicy")
								.nPolicy = 0
							Case CStr(Prof_ord.eOrdClass.cstrCertypePolicy)
								.nPolicy = lrecProf_ord.FieldToClass("nPolicy")
								.nProponum = 0
							Case CStr(Prof_ord.eOrdClass.cstrCertypeClaim)
								.nPolicy = lrecProf_ord.FieldToClass("nPolicy")
								.nProponum = 0
						End Select
						
						.nCertif = lrecProf_ord.FieldToClass("nCertif")
						.dFec_prog = lrecProf_ord.FieldToClass("dFec_prog")
						.sTime_prog = lrecProf_ord.FieldToClass("sTime_prog")
						.dMade_date = lrecProf_ord.FieldToClass("dMade_date")
						.sMade_time = lrecProf_ord.FieldToClass("sMade_time")
						.nOrdertype = lrecProf_ord.FieldToClass("nOrderType")
						.sOrderType = lrecProf_ord.FieldToClass("sOrderType")
						.nStatus_ord = lrecProf_ord.FieldToClass("nStatus_ord")
						.nDeman_type = lrecProf_ord.FieldToClass("nDeman_type")
						.nTransac = lrecProf_ord.FieldToClass("nTransac")
						.sStaclaim = lrecProf_ord.FieldToClass("sStaclaim")
						.sStaReserve = lrecProf_ord.FieldToClass("sStaReserve")
						.sDes_branch = lrecProf_ord.FieldToClass("sBranchDesc")
						.sDes_product = lrecProf_ord.FieldToClass("sProductDesc")
						.sCase = lrecProf_ord.FieldToClass("sCase")
						.sClient = lrecProf_ord.FieldToClass("sClient")
						.nCurrency = lrecProf_ord.FieldToClass("nCurrency")
						.sClient_Deman = lrecProf_ord.FieldToClass("sClient_Demandant")
					End With
					.RNext()
					lintIndex = lintIndex + 1
				Loop 
				.RCloseRec()
				Find_Provider = True
			Else
				Find_Provider = False
			End If
		End With
		lrecProf_ord = Nothing
		
Find_Provider_Err: 
		If Err.Number Then
			Find_Provider = False
		End If
	End Function
	
	Public Function Add_OS001(ByVal objClass As Prof_ord) As Prof_ord
		'create a new object
		If objClass Is Nothing Then
			objClass = New Prof_ord
		End If
		
		With objClass
			mCol.Add(objClass, Format(.nServ_Order))
		End With
		
		'return the object created
		Add_OS001 = objClass
		objClass = Nothing
		
	End Function
	
	'%Find: Lee los datos de la tabla para la transacción OS001
	Public Function Find_OS001(ByVal nOrdClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecReaProf_ord_2 As eRemoteDB.Execute
		Dim lclsProf_ord As Prof_ord
		lrecReaProf_ord_2 = New eRemoteDB.Execute
        Dim lstrCertype As String = ""


        On Error GoTo Find_Err
		
		Select Case nOrdClass
			Case 1
				lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypeProposal)
			Case 2
				lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypePolicy)
			Case 3
				lstrCertype = CStr(Prof_ord.eOrdClass.cstrCertypeClaim)
		End Select
		
		'+Definición de parámetros para stored procedure 'ReaProf_ord_2'
		'+Información leída el 13/04/2002
		With lrecReaProf_ord_2
			.StoredProcedure = "ReaProf_ord_2"
			.Parameters.Add("nOrdClass", nOrdClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_OS001 = True
				mCol = Nothing
				mCol = New Collection
				
				Do While Not .EOF
					lclsProf_ord = New Prof_ord
					lclsProf_ord.nOrdClass = .FieldToClass("nOrdClass")
					lclsProf_ord.sCerType = .FieldToClass("sCertype")
					lclsProf_ord.nBranch = .FieldToClass("nBranch")
					lclsProf_ord.nProduct = .FieldToClass("nProduct")
					lclsProf_ord.nPolicy = .FieldToClass("nPolicy")
					lclsProf_ord.nCertif = .FieldToClass("nCertif")
					lclsProf_ord.nClaim = .FieldToClass("nClaim")
					lclsProf_ord.nCase_num = .FieldToClass("nCase_num")
					lclsProf_ord.nDeman_type = .FieldToClass("nDeman_type")
					lclsProf_ord.nServ_Order = .FieldToClass("nServ_order")
					lclsProf_ord.nProvider = .FieldToClass("nProvider")
					lclsProf_ord.dFec_prog = .FieldToClass("dFec_prog")
					lclsProf_ord.dAssigndate = .FieldToClass("dAssignDate")
					lclsProf_ord.sTime_prog = .FieldToClass("sTime_Prog")
					lclsProf_ord.sPlace = .FieldToClass("sPlace")
					lclsProf_ord.nWorksh = .FieldToClass("nWorksh")
					lclsProf_ord.nMunicipality = .FieldToClass("nMunicipality")
					lclsProf_ord.sName_Cont = .FieldToClass("sName_cont")
					lclsProf_ord.sAdd_Contact = .FieldToClass("sAdd_contact")
					lclsProf_ord.sPhone_Cont = .FieldToClass("sPhone_cont")
					lclsProf_ord.nStatus_ord = .FieldToClass("nStatus_ord")
					lclsProf_ord.nOrd_typeCost = .FieldToClass("nOrd_typeCost")
					lclsProf_ord.nOrdertype = .FieldToClass("nOrderType")
					lclsProf_ord.nNotenum = .FieldToClass("nNoteNum")
					lclsProf_ord.nUsercode = .FieldToClass("nUsercode")
					lclsProf_ord.nNoteorder = .FieldToClass("nNoteorder")
					lclsProf_ord.sMade_time = .FieldToClass("sMade_time")
					lclsProf_ord.dMade_date = .FieldToClass("dMade_date")
					
					
					Call Add_OS001(lclsProf_ord)
					.RNext()
					lclsProf_ord = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_OS001 = False
		End If
		lrecReaProf_ord_2 = Nothing
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prof_ord
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






