Option Strict Off
Option Explicit On
Public Class ClaimBenefs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: ClaimBenefs.cls                          $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 14-01-13 6:01                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	Private mOldClaim As Double
	'**Add:  It adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal llngClaim As Double, ByVal lintCase_num As Integer, ByVal lintDeman_type As Integer, ByVal lstrsClient As String, ByVal lstrsCliename As String, ByVal lintBene_type As Integer, ByVal ldtmCompdate As Date, ByVal lstrsDemandant As String, ByVal lintUsercode As Integer, ByVal lintnId As Integer, Optional ByVal lintnRelation As Integer = 0, Optional ByVal nOffice_pay As Integer = 0, Optional ByVal nOfficeAgen_pay As Integer = 0, Optional ByVal nAgency_pay As Integer = 0) As ClaimBenef
		
		Dim objNewMember As New ClaimBenef
		
		With objNewMember
			.nClaim = llngClaim
			.nCase_num = lintCase_num
			.nDeman_type = lintDeman_type
			.sClient = lstrsClient
			.sCliename = lstrsCliename
			.nBene_type = lintBene_type
			.dCompdate = ldtmCompdate
			.sDemandant = lstrsDemandant
			.nUsercode = lintUsercode
			.nRelation = lintnRelation
			.nId = lintnId
			.nOffice_pay = nOffice_pay
			.nOfficeAgen_pay = nOfficeAgen_pay
			.nAgency_pay = nAgency_pay
		End With
		
		mCol.Add(objNewMember, "CB" & llngClaim & lintCase_num & lintDeman_type & lstrsClient & lintnId)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	'**Add_Si629:  It adds a new element to the collection
	'% Add_Si629: añade un nuevo elemento a la colección
    Public Function Add_Si629(ByVal lintModulec As Integer,
                              ByVal lintCurrency As Integer,
                              ByVal lintCover As Integer,
                              ByVal lstrClient As String,
                              ByVal lintRelation As Integer,
                              ByVal ldblParticip As Double,
                              ByVal lstrCliename As String,
                              ByVal lstrLastName As String,
                              ByVal lstrLastName2 As String,
                              ByVal ldtmBirthdat As Date,
                              ByVal lintIndic_Benef As Integer,
                              ByVal lstrClient_rep As String,
                              ByVal lintOffice_pay As Integer,
                              ByVal lintOfficeAgen_pay As Integer,
                              ByVal lintAgency_pay As Integer,
                              ByVal ldblAmount As Double,
                              ByVal ldtmInit_date As Date,
                              ByVal ldtmEnd_date As Date,
                              ByVal lstrClieName_Rep As String,
                              ByVal lstrLastName_Rep As String,
                              ByVal lstrLastName2_Rep As String,
                              ByVal lintAge As Integer,
                              ByVal lintIncapacity As Integer,
                              ByVal lintId As Integer,
                              ByVal lstrConting As String,
                              ByVal dShowDate As Date,
                              ByVal nNoteNum As Double,
                              Optional ByVal nPerson_typ As Integer = 1,
                              Optional ByVal nPaymentAddress As Integer = 1) As ClaimBenef


        Dim objNewMember As New ClaimBenef

        With objNewMember
            .nModulec = lintModulec
            .nCurrency = lintCurrency
            .nCover = lintCover
            .sClient = lstrClient
            .nRelation = lintRelation
            .nParticip = ldblParticip
            .sCliename = lstrCliename
            .sLastName = lstrLastName
            .sLastName2 = lstrLastName2
            .dBirthDat = ldtmBirthdat
            .Indic_Benef = lintIndic_Benef
            .sClient_Rep = IIf(lstrClient_rep = String.Empty, " ", lstrClient_rep)
            .nOffice_pay = IIf(lintOffice_pay = eRemoteDB.Constants.intNull, 0, lintOffice_pay)
            .nOfficeAgen_pay = IIf(lintOfficeAgen_pay = eRemoteDB.Constants.intNull, 0, lintOfficeAgen_pay)
            .nAgency_pay = IIf(lintAgency_pay = eRemoteDB.Constants.intNull, 0, lintAgency_pay)
            .nAmount = ldblAmount
            .dInit_date = ldtmInit_date
            .dEnd_date = ldtmEnd_date
            .sClieName_Rep = lstrClieName_Rep
            .sLastName_Rep = lstrLastName_Rep
            .sLastName2_Rep = lstrLastName2_Rep
            .nAge = lintAge
            .nIncapacity = IIf(lintIncapacity = eRemoteDB.Constants.intNull, 0, lintIncapacity)
            .nId = lintId
            .sConting = lstrConting
            .dShowDate = dShowDate
            .nNoteNum = nNoteNum
            .nPerson_typ = nPerson_typ
            .nPaymentAddress = nPaymentAddress
        End With

        mCol.Add(objNewMember, "CB" & lintModulec & lintCover & lstrClient & lintRelation & lintId & lstrConting)

        Add_Si629 = objNewMember
        objNewMember = Nothing

    End Function
	
	'% FindClientOutStandClaim: Indica si asegurado de poliza/certificado
	'%                          posee algun siniestros pendiente
	Public Function FindClientOutStandClaim(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsReaClientOutStandClaim As eRemoteDB.Execute
		
		On Error GoTo insReaClientOutStandClaim_Err
		
		lrecinsReaClientOutStandClaim = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insReaclientoutstandclaim al 12-26-2001 13:44:46
		'+
		With lrecinsReaClientOutStandClaim
			.StoredProcedure = "insReaclientOutStandClaim"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			FindClientOutStandClaim = .Run
			.RCloseRec()
		End With
		
insReaClientOutStandClaim_Err: 
		If Err.Number Then
			FindClientOutStandClaim = False
		End If
		On Error GoTo 0
		lrecinsReaClientOutStandClaim = Nothing
	End Function
	
	Public Function Update() As Boolean
		'    Dim lclsClaimBenef As ClaimBenef
		'
		'**+ Possibles values for nStatusInstance
		''+ Valores posibles para nStatusInstance
		'**+ 0: The record is new
		''+ 0: El registro es nuevo
		'**+ 1: the record exist in the table
		''+ 1: El registro ya existe en la tabla
		'**+ 2: the record exist it has to be actualized
		''+ 2: El registro ya existe, hay que actualizarlo
		'**+ 3: the record exist it has to be deleled
		''+ 3: El registro ya existe, hay que eliminarlo
		'    Update = True
		'
		'    For Each lclsClaimBenef In mCol
		'        With lclsClaimBenef
		'            If mOldClaim = vbNull Then
		'                mOldClaim = .nClaim
		'            End If
		'
		'            Update = .Update_ClaimCaseGeneric
		'            Select Case .nStatusInstance
		'                Case 0
		'                    .nStatusInstance = 1
		'                Case 3
		'                    mCol.Remove ("CB" & .nClaim & .nCase_num & .nDeman_type)
		'            End Select
		'        End With
		'    Next lclsClaim_case
	End Function
	'**+ Find_Benef: locate all the beneficiaries of an especific claim and add them to the
	'**+correponding class (ClaimBenef) - ACM - 01/26/2001
	'+ Find_Benef: Localiza todos los beneficiarios de un determinado siniestro y los añade
	'+ a la clase correspondiente (ClaimBenef) - ACM - 26/01/2001
	Public Function Find_Benef(ByVal nClaim As Double, ByVal sClient As String, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal nBene_type As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		Dim lclsClient As New eClient.Client
		Static llngOldClaim As Double
		Static lstrOldClient As String
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		Dim lstrClientName As String
		
		On Error GoTo Find_Benef_Err
		
		If llngOldClaim <> nClaim Or lstrOldClient <> sClient Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Then
			
			llngOldClaim = nClaim
			lstrOldClient = sClient
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			
			lrecClaimBenef = New eRemoteDB.Execute
			
			With lrecClaimBenef
				.StoredProcedure = "reaClaimBenef"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lstrClientName = String.Empty
						If lclsClient.FindClientName(sClient, True) Then
							lstrClientName = lclsClient.sCliename
						End If
						'**+ Add the record to the class - ACM - 01/30/2001
						'+ Se añade el registro a la clase - ACM - 30/01/2001
						Call Add(nClaim, .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), sClient, lstrClientName, .FieldToClass("nBene_type"), Today, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nRelation"), .FieldToClass("nOffice_pay"), .FieldToClass("nOfficeAgen_pay"), .FieldToClass("nAgency_pay"))
						.RNext()
					Loop 
					.RCloseRec()
					Find_Benef = True
				Else
					Find_Benef = False
				End If
			End With
		End If
Find_Benef_Err: 
		If Err.Number Then
			Find_Benef = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
		lclsClient = Nothing
	End Function
	'**+ Find_Benef: locate all the beneficiaries of an especific claim and add them to the
	'**+correponding class (ClaimBenef) - ACM - 01/26/2001
	'+ Find_Benef: Localiza todos los beneficiarios de un determinado siniestro y los añade
	'+ a la clase correspondiente (ClaimBenef) - ACM - 26/01/2001
	Public Function Find_Benef_SI629(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lintOldBranch As Integer
		Static lintOldProduct As Integer
		Static lintOldPolicy As Double
		Static lintOldCertif As Double
		Static lintOldEffecdate As Date
		
		Static lblnRead As Boolean
		
		On Error GoTo Find_Benef_Err
		
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lintOldBranch <> nBranch Or lintOldProduct <> nProduct Or lintOldPolicy <> nPolicy Or lintOldCertif <> nCertif Or lintOldEffecdate <> dEffecdate Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			lintOldBranch = nBranch
			lintOldProduct = nProduct
			lintOldPolicy = nPolicy
			lintOldCertif = nCertif
			lintOldEffecdate = dEffecdate
			
			lrecClaimBenef = New eRemoteDB.Execute
			
			With lrecClaimBenef
				.StoredProcedure = "ReaClaimBenef_SI629"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then 'Listo
					Do While Not .EOF
						'**+ Add the record to the class - ACM - 01/30/2001
						'+ Se añade el registro a la clase - ACM - 30/01/2001
                        Call Add_Si629(.FieldToClass("nModulec"),
                                       .FieldToClass("nCurrency"),
                                       .FieldToClass("nCover"),
                                       .FieldToClass("sClient"),
                                       .FieldToClass("nRelation"),
                                       .FieldToClass("nParticip"),
                                       .FieldToClass("sFirstname"),
                                       .FieldToClass("sLastName"),
                                       .FieldToClass("sLastName2"),
                                       .FieldToClass("dBirthdat"),
                                       .FieldToClass("Indic_Benef"),
                                       .FieldToClass("sClient_Rep"),
                                       .FieldToClass("nOffice_Pay"),
                                       .FieldToClass("nOfficeAgen_Pay"),
                                       .FieldToClass("nAgency_Pay"),
                                       .FieldToClass("nAmount"),
                                       .FieldToClass("dInit_Date"),
                                       .FieldToClass("dEnd_Date"),
                                       .FieldToClass("sFirstname_Rep"),
                                       .FieldToClass("sLastName_Rep"),
                                       .FieldToClass("sLastName2_Rep"),
                                       .FieldToClass("nAge"),
                                       .FieldToClass("nIncapacity"),
                                       .FieldToClass("nId"),
                                       .FieldToClass("sConting"),
                                       .FieldToClass("dShowDate"),
                                       .FieldToClass("nNoteNum"),
                                       .FieldToClass("NPERSON_TYP"),
                                       .FieldToClass("NPAYMENTADDRESS"))
						.RNext()
					Loop 
					.RCloseRec()
					Find_Benef_SI629 = True
				Else
					Find_Benef_SI629 = False
				End If
			End With
		End If
Find_Benef_Err: 
		If Err.Number Then
			Find_Benef_SI629 = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	'+ Find_BenefByClaim: Localiza todos los beneficiarios de un determinado siniestro y los añade
	'+ a la clase correspondiente (ClaimBenef)
	Public Function Find_BenefByClaim(ByVal nClaim As Double, Optional ByVal nBenef_type As Integer = 0, Optional ByVal nRownum As Short = 0) As Boolean
		
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		On Error GoTo Find_BenefByClaim_Err
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		With lrecClaimBenef
			.StoredProcedure = "reaClaimBenefByClaim" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenef_type", nBenef_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRownum", nRownum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					'+ Se añade el registro a la clase.
					Call Add(nClaim, .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("sClient"), "", .FieldToClass("nBene_type"), Today, "", eRemoteDB.Constants.intNull, .FieldToClass("nId", 0), eRemoteDB.Constants.intNull, .FieldToClass("nOffice_pay"), .FieldToClass("nOfficeAgen_pay"), .FieldToClass("nAgency_pay"))
					.RNext()
				Loop 
				.RCloseRec()
				Find_BenefByClaim = True
			Else
				Find_BenefByClaim = False
			End If
		End With
		
		
Find_BenefByClaim_Err: 
		If Err.Number Then
			Find_BenefByClaim = False
		End If
		lrecClaimBenef = Nothing
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ClaimBenef
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






