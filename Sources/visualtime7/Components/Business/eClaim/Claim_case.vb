Option Strict Off
Option Explicit On
Public Class Claim_case
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_case.cls                           $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 28-01-13 0:18                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties to a correspondent class in the Claim_case table (01/10/2001)
	'-Se definen las propiedades principales de la clase correspondientes a la tabla Claim_case (10/01/2001)
	'Column_name                        Type       Computed    Length   Prec  Scale Nullable    TrimTrailingBlanks   FixedLenNullInSource
	Public nClaim As Double 'int           no          4        10    0     no          (n/a)                (n/a)
	Public nCase_num As Integer 'smallint      no          2        5     0     no          (n/a)                (n/a)
	Public nDeman_type As Integer 'smallint      no          2        5     0     no          (n/a)                (n/a)
	Public sStaReserve As String 'char          no          1                    yes         no                   yes
	Public nNoteDama As Integer 'int           no          4        10    0     yes         (n/a)                (n/a)
	Public sClaim_affe As String 'char          no          1                    yes         no                   yes
	Public sClient As String 'char                      14
	Public sDemandant As String 'char
	Public sCliename As String 'char
	Public nBene_type As Integer 'int
	Public nId As Integer
	
	Public sDeman_type As String
	Public nRelation As Integer
	Public sDigit As String
	Public sBene_type As String
	Public sRelation As String
	Public sStacase As String
	Public sFirstName As String
	Public sLastName As String
	Public sLastName2 As String
	
	'**-Defined the variable that contein the status of each instance of the class
	'- Se define la variable que contiene el estado de la cada instancia de la clase
	
	Public nStatusInstance As Integer
	
	'**-Defined the variable that contains the process type to make it to the class
	'- Se define la variable que contiene el tipo de proceso a realizar a la clase
	
	Public sTypProcess As String
	
	Public nUsercode As Integer
	
	'**-Defined the enumerate list that contains the claim roles (table184)
	'-Se define la lista enumerada que contendra los roles de siniestro (table184)
	Public Enum eClaimRole
		clngClaimRContract = 1 '**Holder
		'Contratante
		clngClaimRInsured = 2 '**Insuranced
		'Asegurado
		clngClaimRThird = 3 '**Third
		'Tercero
		clngClaimRUsualDriver = 4 '**Usual driver
		'Conductor habitual
		clngClaimRContact = 5 '**Contact
		'Contacto
		clngClaimRContGuar = 6 '**Counter garantor
		'Contragarante
		clngClaimRAddInsured = 7 '**Aditional insurance
		'Asegurado adicional
		clngClaimRBonded = 8 '**Bonded
		'Afianzado
		clngClaimRPrivHosp = 9 '**Hospital
		'Clinica
		clngClaimRWorkShop = 10 '**Workshop
		'Taller
		clngClaimRProfessional = 12 '**Professional
		'Profesional (Perito)
		clngClaimRAgent = 13 '**Agent
		'Agente
		clngClaimRInsuredAffected = 14 '**Affected Insured
		'Asegurado afectado
		clngClaimRBenefic = 16 '**Beneficiary
		'Beneficiario
		clngClaimRFather = 21 '**Father
		'Padre
		clngClaimRDescending = 22 'Descending
		'Descendiente
		clngClaimRConsort = 23
		clngClaimRBrother = 24 '**Brother
		'Hermano
	End Enum
	
	Private mclsClaimBenefs As ClaimBenefs
	
	'**+Public variable is going will be use for the capture of the description of a general table.
	'+ Variable pública para ser utilizada para la captura de descripciones
	'+ de alguna tabla general.
	
	Public sDescript As String
	
	Public sConting As String
	
	Public nGrowth_RateI As Double
	Public nGrowth_RateE As Double
	Public sHas_Surv_Pension_Benefs As String
	Public dSummon As Date
	Public dSummon_Limit As Date
	Public Origins As Collection
	
	Public sAfp_trans_type As String
	Public nStay_bonus As Double
	Public nApv_capital As Double
	Public nApv_balance_bc2052 As Double
	Public nApv_balance_ac2052 As Double
	Public nTransf_amount As Double
	Public nApv_tax As Double
	Public nApv_benef_balance As Double
	
	Public nOption As Integer
	Public nAFP As Integer
	Public nCurrency As Integer
	Public nCoverCapital As Integer
	
	
	'**% Update_ClaimCaseGeneric: make the actualization in the claim case
	'% Update_ClaimCaseGeneric: Realiza las actualizaciones de los casos de un siniestro
	Public Function Update_ClaimCaseGeneric() As Boolean
		Dim lrecupdClaim_case As eRemoteDB.Execute
		
		On Error GoTo Update_ClaimCaseGeneric_Err
		
		lrecupdClaim_case = New eRemoteDB.Execute
		
		With lrecupdClaim_case
			.StoredProcedure = "insUpdClaim_case"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStaReserve", sStaReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteDama", nNoteDama, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDemandant", sDemandant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaim_affe", sClaim_affe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypProcess", sTypProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", IIf(nRelation = 0, eRemoteDB.Constants.intNull, nRelation), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Update_ClaimCaseGeneric = True
			Else
				Update_ClaimCaseGeneric = False
			End If
		End With
		
Update_ClaimCaseGeneric_Err: 
		If Err.Number Then
			Update_ClaimCaseGeneric = False
		End If
		lrecupdClaim_case = Nothing
		
	End Function
	
	'**%Update_Claim_case_sStaReserve_all: This routine acualized the reserved status of a case
	'% Update_Claim_case_sStaReserve_all:esta rutina actualiza el estado de las reservas de un caso
	Public Function Update_Claim_case_sStaReserve_all(ByVal nClaim As Double, ByVal sStaReserve As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdClaim_case As eRemoteDB.Execute
		
		lrecupdClaim_case = New eRemoteDB.Execute
		
		With lrecupdClaim_case
			'**Parameters definition for the stored procedure 'insudb.updClaim_case_sStarreserve_all'
			'Definición de parámetros para stored procedure 'insudb.updClaim_case_sStareserve_all'
			'**Data read on 01/17/2001 10.17.56
			'Información leída el 17/01/2001 10.17.56
			.StoredProcedure = "updClaim_case_sStareserve_all"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaReserve", sStaReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Claim_case_sStaReserve_all = .Run(False)
		End With
		lrecupdClaim_case = Nothing
	End Function
	
	
	'**%Find: Read an especific claim case
	'% Find: Lee un caso especifico de un siniestro
	Public Function Find(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClaim_case_o As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		Find = True
		If Claim <> nClaim Or Case_num <> nCase_num Or Deman_type <> nDeman_type Or lblnFind Then
			
			nClaim = Claim
			nCase_num = Case_num
			nDeman_type = Deman_type
			
			lrecreaClaim_case_o = New eRemoteDB.Execute
			
			'**+ Parameters definition for the stored procedure 'insdb.reaClaim_case_o'
			'+ Definición de parámetros para stored procedure 'insudb.reaClaim_case_o'
			'**+Data read on 01/23/2001 1:45:28 PM
			'+ Información leída el 23/01/2001 1:45:28 PM
			
			With lrecreaClaim_case_o
				.StoredProcedure = "reaClaim_case_o"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sStaReserve = .FieldToClass("sStaReserve")
					nNoteDama = .FieldToClass("nNoteDama")
					sClaim_affe = .FieldToClass("sClaim_affe")
					sHas_Surv_Pension_Benefs = .FieldToClass("sHas_Surv_Pension_Benefs")
					dSummon = .FieldToClass("dSummon")
					Me.sAfp_trans_type = .FieldToClass("sAfp_trans_type")
					Me.nStay_bonus = .FieldToClass("nStay_bonus")
					Me.nApv_capital = .FieldToClass("nApv_capital")
					Me.nApv_balance_bc2052 = .FieldToClass("nApv_balance_bc2052")
					Me.nApv_balance_ac2052 = .FieldToClass("nApv_balance_ac2052")
					Me.nTransf_amount = .FieldToClass("nTransf_amount")
					Me.nApv_tax = .FieldToClass("nApv_tax")
					Me.nApv_benef_balance = .FieldToClass("nApv_benef_balance")
					Me.nAFP = .FieldToClass("nAFP")
					
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			lrecreaClaim_case_o = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdateNoteDama: Actualize the note number of the case
	'% UpdatenNoteDama: Actualiza el número de nota del caso
	Public Function UpdatenNoteDama(ByVal nClaim As Double, ByVal nDeman_type As Integer, ByVal nCase_num As Integer, ByVal nNoteDama As Integer) As Boolean
		
		Dim lrecupdClaimCaseNote As eRemoteDB.Execute
		
		On Error GoTo UpdatenNoteDama_Err
		lrecupdClaimCaseNote = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.updClaimCaseNote'
		'Definición de parámetros para stored procedure 'insudb.updClaimCaseNote'
		'**Data read on 01/23/2001 2:41:19 PM
		'Información leída el 23/01/2001 2:41:19 PM
		With lrecupdClaimCaseNote
			
			.StoredProcedure = "updClaimCaseNote"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteDama", nNoteDama, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdatenNoteDama = .Run(False)
			
		End With
		
		lrecupdClaimCaseNote = Nothing
		
UpdatenNoteDama_Err: 
		If Err.Number Then
			UpdatenNoteDama = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdateStareserve: Updates the status of a case
	'% UpdatesStareserve: Actualiza el estado de un caso
    Public Function UpdatesStareserve(ByVal Claim As Double, ByVal Deman_type As Integer, ByVal Case_num As Integer, ByVal StaReserve As String, Optional ByVal nUsercode As Integer = 1) As Boolean
        Dim lrecupdClaim_case_sStareserve As eRemoteDB.Execute

        On Error GoTo UpdatesStareserve_Err
        lrecupdClaim_case_sStareserve = New eRemoteDB.Execute

        '**Parameters definition for the stored procedure 'insudb.updClaim_case_sStareserve'
        'Definición de parámetros para stored procedure 'insudb.updClaim_case_sStareserve'
        '**Data read on 01/23/2001 2:30:46 PM
        'Información leída el 23/01/2001 2:30:46 PM
        With lrecupdClaim_case_sStareserve

            .StoredProcedure = "updClaim_case_sStareserve"
            .Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaReserve", StaReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdatesStareserve = .Run(False)

        End With

        lrecupdClaim_case_sStareserve = Nothing

UpdatesStareserve_Err:
        If Err.Number Then
            UpdatesStareserve = False
        End If
        On Error GoTo 0
    End Function
	
	'**%insValClaim_case: The objetive of this function is to obtain the case status
	'%insValClaim_case: El objetivo de esta función es obtener el estado del caso
	Public Function ValClaim_case(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecreaClaim_case_v1 As eRemoteDB.Execute
		
		On Error GoTo insValClaim_case_err
		
		lrecreaClaim_case_v1 = New eRemoteDB.Execute
		ValClaim_case = False
		
		'**+Parameters definition for the stored procedure 'insudb.reaClaim_case_v1'
		'+ Definición de parámetros para stored procedure 'insudb.reaClaim_case_v1'
		'**+Data read on 01/25/2001 4:11:51 PM
		'+ Información leída el 25/01/2001 4:11:51 PM
		
		With lrecreaClaim_case_v1
			.StoredProcedure = "reaClaim_case_v1"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValClaim_case = True
				Me.nClaim = nClaim
				Me.nCase_num = nCase_num
				Me.nDeman_type = nDeman_type
				sStaReserve = .FieldToClass("sStaReserve")
				nCase_num = .FieldToClass("nCase_num")
				nDeman_type = .FieldToClass("nDeman_type")
				nNoteDama = .FieldToClass("nNoteDama")
				nBene_type = .FieldToClass("nBene_type")
				sDeman_type = .FieldToClass("sDeman_type")
				sCliename = .FieldToClass("sCliename")
				sClient = .FieldToClass("sClient")
				.RCloseRec()
			End If
		End With
		lrecreaClaim_case_v1 = Nothing
		
insValClaim_case_err: 
		If Err.Number Then
			ValClaim_case = False
		End If
		On Error GoTo 0
	End Function
	
	Public ReadOnly Property bFullCase() As Boolean
		Get
			Dim lclsCase_win As Cases_win
			Dim lintTop As Integer
			Dim lintCount As Integer
			Dim lstrAuxCodispl As String
			Dim lbytAuxContent As Byte
			
			On Error GoTo bFullCase_Err
			
			bFullCase = True
			lclsCase_win = New Cases_win
			
			With lclsCase_win
				If .Find(nClaim, nCase_num, nDeman_type) Then
					lintTop = Len(Trim(.sV_conclaim)) - 1
					For lintCount = 0 To lintTop
						lstrAuxCodispl = Trim(Mid(.sV_winclaim, lintCount * 8 + 1, 8))
						lbytAuxContent = CByte(Trim(Mid(.sV_conclaim, lintCount + 1, 1)))
						If (lstrAuxCodispl = "SI018" Or lstrAuxCodispl = "SI019" Or lstrAuxCodispl = "SI024" Or lstrAuxCodispl = "SI070" Or lstrAuxCodispl = "SI028") And lbytAuxContent = CDbl("1") Then
							bFullCase = False
							Exit For
						End If
					Next lintCount
				Else
					bFullCase = False
				End If
			End With
			
			lclsCase_win = Nothing
			
bFullCase_Err: 
			If Err.Number Then
				bFullCase = False
			End If
			On Error GoTo 0
		End Get
	End Property
	
	Public ReadOnly Property ClaimBenefs() As ClaimBenefs
		Get
			If mclsClaimBenefs Is Nothing Then
				mclsClaimBenefs = New ClaimBenefs
			End If
			Call mclsClaimBenefs.Find_Benef(nClaim, sClient, nCase_num, nDeman_type, nBene_type)
			ClaimBenefs = mclsClaimBenefs
		End Get
	End Property
	
	'% FindChildren: Se verifica la existencia de información relacionada al siniestro-caso
	Public Function FindChildren(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer, ByVal sClient As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lstrHasChild As String
		
		On Error GoTo FindChildren_err
		
		lstrHasChild = "0"
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valClaimChilds"
			.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHasChild", lstrHasChild, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				FindChildren = Trim(.Parameters("sHasChild").Value) = "1"
			End If
		End With
		
FindChildren_err: 
		If Err.Number Then
			On Error GoTo 0
		End If
		lclsRemote = Nothing
	End Function

	'**%GetClaim_CaseInfo: The objetive of this function is to obtain the case status
	'%GetClaim_CaseInfo: El objetivo de esta función es obtener la informacion desglosada del caso
	Public Function GetClaim_CaseInfo(ByVal sClaim_Case As string) As Boolean
		Dim lrecreaClaim_case As eRemoteDB.Execute
		
		On Error GoTo insValClaim_case_err
		
		lrecreaClaim_case = New eRemoteDB.Execute
		GetClaim_CaseInfo = False
		
		'**+Parameters definition for the stored procedure 'insudb.reaClaim_case_v1'
		'+ Definición de parámetros para stored procedure 'insudb.reaClaim_case_v1'
		'**+Data read on 01/25/2001 4:11:51 PM
		'+ Información leída el 25/01/2001 4:11:51 PM
		
		With lrecreaClaim_case
			.StoredProcedure = "reaClaim_case_Info"
            .Parameters.Add("sClaim_Case", sClaim_Case, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				GetClaim_CaseInfo = True
				nCase_num = .FieldToClass("nCase_num")
				nDeman_type = .FieldToClass("nDeman_type")
				.RCloseRec()
			End If
		End With
		lrecreaClaim_case = Nothing
		
insValClaim_case_err: 
		If Err.Number Then
			GetClaim_CaseInfo = False
		End If
		On Error GoTo 0
	End Function


End Class






