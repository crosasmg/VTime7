Option Strict Off
Option Explicit On

Imports System.Configuration
Public Class Tab_Provider
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Provider.cls                         $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 3/03/04 6:38p                                $%'
	'% $Revision:: 30                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla TAB_PROVIDER tomada el 26/03/2002 18:41
	'+ Column_Name                                       Type      Length  Prec  Scale  Nullable
	' -----------------------------------------------   ---------- ------- ----- ------ --------
	Public nProvider As Integer ' NUMBER        22     5      0  No
	Public nTypeProv As Integer ' NUMBER        22     5      0  Yes
	Public sClient As String ' CHAR          14               No
	Public dCompdate As Date ' DATE           7               Yes
	Public sStatregt As String ' CHAR           1               Yes
	Public nUsercode As Integer ' NUMBER        22     5      0  Yes
	Public dInpdate As Date ' DATE           7               Yes
	Public dOutdate As Date ' DATE           7               Yes
	Public nOffice As Integer ' NUMBER        22     5      0  Yes
	Public nMax_serv_ord As Integer ' NUMBER        22     5      0  Yes
	Public nTypeSupport As Integer ' NUMBER        22     5      0  Yes
	Public nPer_disc As Single ' NUMBER        22     4      2  Yes
	Public sConcesionary As String ' VARCHAR2       1               No
	Public sLocal As String ' CHAR                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nProv_group As Integer
	Public nAction As Integer
	Public nBranch As Integer
	Public sDescript As String
	Public sDigit As String
	
	Public sTypeProv As String
	Public sOffice As String
	Public sTypeSupport As String
	Public sStatregt_desc As String
	Public nProvZone As Integer
	Public nProvBranch As Integer
	Public nProvGroup As Integer
	
	'+ Auxiliary properties
	'+ Propiedades auxiliares
	Public sCliename As String
	Private mstrCertype As String
	Private mlngProduct As Integer
	Private mlngPolicy As Integer
	Private mlngCertif As Integer
	Private mdtmEffecdate As Date
	Private mlngClaim As Double
	Private mintCase_num As Integer
	Private mintDeman_type As Integer
	Private mlngBene_type As Integer
	
	'+ Define the enumerated list eProvider, to differentiate the record type of the provider table
	'+ Se define la lista enumerada eProvider, para diferenciar el tipo de registro
	'+ de la tabla proveedores.
	Enum eProvider
		clngClinic = 1
		clngWorksh = 2
		clngProfes = 3
    End Enum

    Public sAgencyDesc As String

	'% FindDatesProvider: busca fechas de ingreso y egreso de un proveedor
	Public Function FindDatesProvider(ByVal nProvider As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecrea_dates_provider As eRemoteDB.Execute
		
		On Error GoTo rea_dates_provider_Err
		
		lrecrea_dates_provider = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure rea_dates_provider al 10-16-2002 12:14:11
		'+
		With lrecrea_dates_provider
			.StoredProcedure = "rea_dates_provider"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindDatesProvider = True
				Me.dInpdate = .FieldToClass("dInpdate")
				Me.dOutdate = .FieldToClass("dOutdate")
			Else
				FindDatesProvider = False
			End If
		End With
		
rea_dates_provider_Err: 
		If Err.Number Then
			FindDatesProvider = False
		End If
		lrecrea_dates_provider = Nothing
		On Error GoTo 0
	End Function
	
	'% FindProvider: find the client code of an especific provider
	'% FindProvider: busca el código del cliente de un proveedor específico
	Public Function FindProvider(ByVal nProvider As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_provider As eRemoteDB.Execute
		
		On Error GoTo FindProvider_Err
		
		FindProvider = False
		
		If Me.nProvider <> nProvider Or lblnFind Then
			
			Me.nProvider = nProvider
			lrecreaTab_provider = New eRemoteDB.Execute
			
			'+ Parameter definition for the stored procedure 'insudb.reaTabprovider'
			'+ Data read on 06/01/2000 05:46:50 PM
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_provider'
			'+ Información leída el 01/06/2000 05:46:50 PM
			With lrecreaTab_provider
				.StoredProcedure = "reaTab_provider"
				.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.nTypeSupport = .FieldToClass("nTypeSupport")
					FindProvider = True
					.RCloseRec()
				End If
			End With
			lrecreaTab_provider = Nothing
		End If
		
FindProvider_Err: 
		If Err.Number Then
			FindProvider = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% FinfClient: This function makes the reading of the tab_provider table to validate
	'% if the code client (that pass as a parameter) is associated with provider table
	'% (tab_provider)
	'% FindClient. Esta funcion realiza la lectura a la tabla tab_provider, para validar
	'% si el codigo de cliente (que se pasa como parametro), se encuentra asociado a la tabla de
	'% proveedores (tab_provider)
	Public Function FindClient(ByVal sClient As String, ByVal nTyp_prov As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClient As eRemoteDB.Execute
		
		On Error GoTo FindClient_Err
		
		FindClient = False
		If Me.sClient <> sClient Or Me.nTypeProv <> nTyp_prov Or lblnFind Then
			
			Me.sClient = sClient
			Me.nTypeProv = nTyp_prov
			
			lrecreaClient = New eRemoteDB.Execute
			
			'+ Execute the SP "reaTab_provider_sClient", that makes the reading in the BD to verificate the existence
			'+ of the services provider
			'+ Se ejecuta el SP "reaTab_provider_sClient", que realiza la lectura en la BD para verificar la existencia
			'+ del proveedor de servicios
			With lrecreaClient
				.StoredProcedure = "reaTab_provider_sClient"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_prov", nTyp_prov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nProvider = .FieldToClass("nProvider")
					Me.nTypeSupport = .FieldToClass("nTypeSupport")
					FindClient = True
					.RCloseRec()
				Else
					nProvider = 0
					FindClient = False
				End If
			End With
			lrecreaClient = Nothing
		End If
		
FindClient_Err: 
		If Err.Number Then
			FindClient = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% ValClientProvPol: verifies if the client is associated as a provider
	'% ValClientProvPol: verifica si el cliente está asociado como proveedor
	Public Function ValClientProvPol(ByVal sCerType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nTypeProv As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClientProvPol As eRemoteDB.Execute
		
		On Error GoTo ValClientProvPol_Err
		
		If mstrCertype <> sCerType Or Me.nBranch <> nBranch Or mlngProduct <> nProduct Or mlngPolicy <> nPolicy Or mlngCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or Me.sClient <> sClient Or Me.nTypeProv <> nTypeProv Or lblnFind Then
			
			mstrCertype = sCerType
			Me.nBranch = nBranch
			mlngProduct = nProduct
			mlngPolicy = nPolicy
			mlngCertif = nCertif
			mdtmEffecdate = dEffecdate
			Me.sClient = sClient
			Me.nTypeProv = nTypeProv
			
			lrecreaClientProvPol = New eRemoteDB.Execute
			
			'+ Parameters definition for the stored procedure 'insudb.insvalClientProv'
			'+ Data read on 06/01/2000 10:37:35 AM
			'+ Definición de parámetros para stored procedure 'insudb.insvalClientProv'
			'+ Información leída el 01/06/2000 10:37:35 AM
			With lrecreaClientProvPol
				.StoredProcedure = "insvalClientProv"
				.Parameters.Add("sCertype", sCerType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeProv", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ExistClient", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					If .Parameters.Item("ExistClient").Value = "2" Then
						ValClientProvPol = False
					Else
						ValClientProvPol = True
					End If
					.RCloseRec()
				Else
					ValClientProvPol = False
				End If
				
			End With
			lrecreaClientProvPol = Nothing
		End If
		
ValClientProvPol_Err: 
		If Err.Number Then
			ValClientProvPol = False
		End If
		On Error GoTo 0
	End Function
	
	'% Function ValProvBranch: This function is in charge to verify if the provider number is
	'% associated to the branch on treatment
	'% Funcion ValProvBranch. Esta funcion se encarga de verificar si el numero de proveedor se
	'% encuentra asociado al ramo en tratamiento.
	Public Function ValProvBranch(ByVal nBranch As Integer, ByVal nProvider As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaProv_branch As eRemoteDB.Execute
		
		On Error GoTo ValProvBranch_Err
		
		ValProvBranch = False
		If Me.nBranch <> nBranch Or Me.nProvider <> nProvider Or lblnFind Then
			
			Me.nBranch = nBranch
			Me.nProvider = nProvider
			
			lrecreaProv_branch = New eRemoteDB.Execute
			
			With lrecreaProv_branch
				.StoredProcedure = "reaProv_Branch_o"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ValProvBranch = True
					.RCloseRec()
				End If
			End With
			lrecreaProv_branch = Nothing
		End If
		
ValProvBranch_Err: 
		If Err.Number Then
			ValProvBranch = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% ValProviderCase: the objective of this function is to validate a record in the ClaimBenef table accourding to the beneficiary type (figure).
	'% ValProviderCase: El objetivo de esta función es validar si existe un registro en la tabla ClaimBenef según el tipo de beneficiario (figura).
	Public Function ValProviderCase(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBene_type As Integer, ByVal nProvider As Integer, ByVal nTypeProv As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		On Error GoTo ValProviderCase_Err
		
		If mlngClaim <> nClaim Or mintCase_num <> nCase_num Or mintDeman_type <> nDeman_type Or mlngBene_type <> nBene_type Or Me.nProvider <> nProvider Or Me.nTypeProv <> nTypeProv Or lblnFind Then
			
			mlngClaim = nClaim
			mintCase_num = nCase_num
			mintDeman_type = nDeman_type
			mlngBene_type = nBene_type
			Me.nProvider = nProvider
			Me.nTypeProv = nTypeProv
			
			lrecClaimBenef = New eRemoteDB.Execute
			With lrecClaimBenef
				.StoredProcedure = "valProviderCase"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeProv", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If (.Run) Then
					If .FieldToClass("lCount") > 0 Then
						ValProviderCase = True
					Else
						ValProviderCase = False
					End If
					.RCloseRec()
				Else
					ValProviderCase = False
				End If
			End With
			lrecClaimBenef = Nothing
		End If
		
ValProviderCase_Err: 
		If Err.Number Then
			ValProviderCase = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% ValTab_provider: the objective of this function is to validate if exist a record in the Provider table
	'% ValTab_provider: El objetivo de esta función es validar si existe un registro en la tabla Provider
	Public Function ValTab_provider(ByVal nType_prov As Integer, ByVal nProvider As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecTab_provider As eRemoteDB.Execute
		
		On Error GoTo ValTab_provider_Err
		
		ValTab_provider = False
		If Me.nTypeProv <> nType_prov Or Me.nProvider <> nProvider Or lblnFind Then
			
			Me.nTypeProv = nType_prov
			Me.nProvider = nProvider
			lrecTab_provider = New eRemoteDB.Execute
			
			With lrecTab_provider
				.StoredProcedure = "reaTab_provider_1"
				.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_prov", nType_prov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If (.Run) Then
					Me.sClient = .FieldToClass("sClient")
					Me.sCliename = .FieldToClass("sCliename")
					ValTab_provider = True
					.RCloseRec()
				End If
			End With
			
			lrecTab_provider = Nothing
		End If
		
ValTab_provider_Err: 
		If Err.Number Then
			ValTab_provider = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% FindProviderByCode: makes the reading about client or provider
	'% FindProviderByCode: realiza la lectura por cliente o proveedor
	Public Function FindProviderByCode(ByVal nProvider As Integer, ByVal nTypeProv As Integer, ByVal sClient As String) As Boolean
		Dim lrecreaTab_provider_2 As eRemoteDB.Execute
		Dim lclsClaim As eClaim.Claim
		
		On Error GoTo FindProviderByCode_err
		
		lrecreaTab_provider_2 = New eRemoteDB.Execute
		
		'+ Parameters definition for the stored procedure 'insudb.reaTab_provider_2'
		'+ Data read on 07/14/2001 03:47:53 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_provider_2'
		'+ Información leída el 14/07/2001 03:47:53 p.m.
		With lrecreaTab_provider_2
			.StoredProcedure = "reaTab_provider_2"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_prov", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindProviderByCode = True
				Me.nProvider = .FieldToClass("nProvider")
				Me.nTypeProv = .FieldToClass("nTypeProv")
				Me.sClient = .FieldToClass("sClient")
				sStatregt = .FieldToClass("sStatregt")
				sCliename = .FieldToClass("sCliename")
				If .FieldToClass("sClient") <> String.Empty Then
					lclsClaim = New eClaim.Claim
					Me.sDigit = lclsClaim.CalcDigit(.FieldToClass("sClient"))
					lclsClaim = Nothing
				End If
				.RCloseRec()
			Else
				FindProviderByCode = False
			End If
		End With
		
FindProviderByCode_err: 
		If Err.Number Then
			FindProviderByCode = False
		End If
		On Error GoTo 0
		
		lrecreaTab_provider_2 = Nothing
	End Function
	
	'% insValMSI035_K:Validates the provider's branches
	'% insValMSI035_K: Valida los ramos asociados al proveedor
	Public Function insValMSI035_K(ByVal sCodispl As String) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMSI035_K_Err
		
		Call lclsErrors.ErrorMessage(sCodispl, 1047)
		
		insValMSI035_K = lclsErrors.Confirm
		
insValMSI035_K_Err: 
		If Err.Number Then
			insValMSI035_K = insValMSI035_K & Err.Description
		End If
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insValMSI011_K:Validates the provider
	'% insValMSI011_K: Valida el Proveedor
	Public Function insValMSI011_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nProvider As Integer, ByVal nTypeProv As Integer, ByVal sClient As String, ByVal dInpdate As Date, ByVal dOutdate As Date, ByVal sStatregt As String, ByVal nOffice As Integer, ByVal nMax_serv_ord As Integer, ByVal nTypeSupport As Integer, ByVal nPer_disc As Double, ByVal sConcesionary As String, ByVal nUsercode As Integer, ByVal nExists_reg As Integer, ByVal chkZone As Integer, ByVal chkBranch As Integer, ByVal chkGroup As Integer, ByVal sDigit As String) As String
		Dim lclsNumerator As eGeneral.GeneralFunction
		Dim lclsErrors As eFunctions.Errors
		Dim lvclTime As eClient.ValClient
		Dim FindClient As eClient.Client
		Dim lcolTab_prov_zones As eClaim.Tab_prov_zones
		Dim lclsProvider As eClaim.Tab_Provider
		Dim lblnAll As Boolean
		Dim nAction As Integer
		On Error GoTo insValMSI011_K_err
		lvclTime = New eClient.ValClient
		lclsNumerator = New eGeneral.GeneralFunction
		lclsErrors = New eFunctions.Errors
		FindClient = New eClient.Client
		lcolTab_prov_zones = New eClaim.Tab_prov_zones
		lclsProvider = New eClaim.Tab_Provider
		
		sAction = Trim(sAction)
		
		Select Case sAction
			Case "Add"
				nAction = 301
			Case "Update"
				nAction = 302
			Case "Del"
				nAction = 303
		End Select
		
		lblnAll = True
		
		If sAction = "Del" Then
			If val_nProvider_Claim(nProvider) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10874)
			End If
		Else
			If (nProvider = eRemoteDB.Constants.intNull Or nProvider = 0) Then
				lblnAll = False
				Call lclsErrors.ErrorMessage(sCodispl, 4116)
			End If
			
			If ((nTypeProv <> eRemoteDB.Constants.intNull And nTypeProv <> 0) Or Trim(sClient) <> String.Empty Or Trim(sStatregt) <> String.Empty) And lblnAll Then
				If (nProvider = eRemoteDB.Constants.intNull Or nProvider = 0) Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 1084)
				End If
			End If
			
			If sAction = "Add" And nExists_reg = 1 Then
				If FindProvider(nProvider, True) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
			
			If (nProvider <> eRemoteDB.Constants.intNull And nProvider <> 0) Then
				If (nTypeProv = eRemoteDB.Constants.intNull Or nTypeProv = 0) Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 10873)
				End If
				
				If Trim(sClient) = String.Empty Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 12043)
				End If
				
				If Trim(sDigit) = String.Empty Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 2090)
				End If
				
				If nTypeSupport = eRemoteDB.Constants.intNull Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 5115)
				End If
				
				If dInpdate = eRemoteDB.Constants.dtmNull Then
					lblnAll = False
					Call lclsErrors.ErrorMessage(sCodispl, 9013)
				End If
				
				If dOutdate <> eRemoteDB.Constants.dtmNull Then
					If dOutdate < dInpdate And lblnAll Then
						lblnAll = False
						Call lclsErrors.ErrorMessage(sCodispl, 9006)
					End If
				End If
				
				If Not lcolTab_prov_zones.Find(nProvider) And chkZone = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55960)
				End If
				
				If Not lclsProvider.Valexist_Prov_Branch(nProvider, sClient) And chkBranch = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55961)
				End If
				
				If Not lclsProvider.Valexist_Prov_Group(nProvider) And chkGroup = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55962)
				End If
			End If
		End If
		
		insValMSI011_K = lclsErrors.Confirm
		
		lclsNumerator = Nothing
		lclsErrors = Nothing
		lcolTab_prov_zones = Nothing
		lclsProvider = Nothing
		
insValMSI011_K_err: 
		If Err.Number Then
			insValMSI011_K = insValMSI011_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% insPostMSI011_K: Updates the providers window
	'% insPostMSI011_K: Actualiza la Ventana de Proveedores
    Public Function insPostMSI011_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nProvider As Integer, ByVal nTypeProv As Integer, ByVal sClient As String, ByVal dInpdate As Date, ByVal dOutdate As Date, ByVal sStatregt As String, ByVal nAction As Integer, ByVal nOffice As Integer, ByVal nMax_serv_ord As Integer, ByVal nTypeSupport As Integer, ByVal nPer_disc As Double, ByVal sConcesionary As String, ByVal nUsercode As Integer, ByVal nExists_reg As Integer, Optional ByVal sAgencyDesc As String = eRemoteDB.Constants.strNull) As Boolean
        On Error GoTo insPostMSI011_K_err

        If sAction = "Add" And nExists_reg = 3 Then
            sAction = "Update"
        Else
            sAction = Trim(sAction)
        End If

        With Me
            .nProvider = nProvider
            .nTypeProv = nTypeProv
            .sClient = sClient
            .dInpdate = dInpdate
            .dOutdate = dOutdate
            .sStatregt = sStatregt
            .nUsercode = nUsercode
            .nAction = nAction
            .nOffice = IIf(nOffice = 0, eRemoteDB.Constants.intNull, nOffice)
            .nMax_serv_ord = nMax_serv_ord
            .nTypeSupport = nTypeSupport
            .nPer_disc = nPer_disc
            .sConcesionary = IIf(sConcesionary = "1", "1", "2")
            .sAgencyDesc = sAgencyDesc
        End With

        Select Case sAction

            '+ If the selected option is Register
            '+ Si la opción seleccionada es Registrar
            Case "Add"
                insPostMSI011_K = Add()

                '+ If the selected option is Modify
                '+ Si la opción seleccionada es Modificar
            Case "Update"
                insPostMSI011_K = Update()

                '+ If the selected option is Delete
                '+ Si la opción seleccionada es Eliminar
            Case "Del"
                insPostMSI011_K = Delete()

        End Select

insPostMSI011_K_err:
        If Err.Number Then
            insPostMSI011_K = False
        End If
        On Error GoTo 0

    End Function
	
	'%Valexist_Prov_Branch: verifica si existe un ramo asociado al proveedor
	Public Function Valexist_Prov_Branch(ByVal nProvider As Integer, ByVal sClient As String) As Boolean
		Dim lrecValexist_Prov_Branch As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		On Error GoTo Valexist_Prov_Branch_Err
		lrecValexist_Prov_Branch = New eRemoteDB.Execute
		
		Valexist_Prov_Branch = True
		With lrecValexist_Prov_Branch
			.StoredProcedure = "Valexist_Prov_Branch"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					Valexist_Prov_Branch = True
				Else
					Valexist_Prov_Branch = False
				End If
			Else
				Valexist_Prov_Branch = False
			End If
		End With
		
		
Valexist_Prov_Branch_Err: 
		If Err.Number Then
			Valexist_Prov_Branch = False
		End If
		On Error GoTo 0
		lrecValexist_Prov_Branch = Nothing
	End Function
	'%Valexist_Prov_Group: verifica si existe un ramo asociado al proveedor
	Public Function Valexist_Prov_Group(ByVal nProvider As Integer) As Boolean
		Dim lrecValexist_Prov_Group As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		On Error GoTo Valexist_Prov_Group_Err
		lrecValexist_Prov_Group = New eRemoteDB.Execute
		
		Valexist_Prov_Group = True
		With lrecValexist_Prov_Group
			.StoredProcedure = "Valexist_Prov_Group"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					Valexist_Prov_Group = True
				Else
					Valexist_Prov_Group = False
				End If
			Else
				Valexist_Prov_Group = False
			End If
		End With
		
		
Valexist_Prov_Group_Err: 
		If Err.Number Then
			Valexist_Prov_Group = False
		End If
		On Error GoTo 0
		lrecValexist_Prov_Group = Nothing
	End Function
	
	'% val_nProvider_Claim: Allows to validate if a claim cause is already registered.
	'% val_nProvider_Claim: Permite validar si una causa de siniestro ya está registrada.
	Private Function val_nProvider_Claim(ByVal nProvider As Integer) As Boolean
		Dim lexeTime As New eRemoteDB.Execute
		
		On Error GoTo val_nProvider_Claim_Err
		
		lexeTime = New eRemoteDB.Execute
		
		lexeTime.StoredProcedure = "valProf_ord_nProvider_a"
		lexeTime.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If lexeTime.Run Then
			val_nProvider_Claim = False
		Else
			val_nProvider_Claim = False
		End If
		
		lexeTime = Nothing
		
val_nProvider_Claim_Err: 
		If Err.Number Then
			val_nProvider_Claim = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Update: Updates the table Tab_Provider
	'% Update: Actualiza la Tabla Tab_Provider
	Private Function Update() As Boolean
		Dim lexeTime As New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lexeTime = New eRemoteDB.Execute
		With lexeTime
			.StoredProcedure = "updTab_provider"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProv", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpDate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOutDate", dOutdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_serv_ord", nMax_serv_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPer_disc", nPer_disc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConcesionary", sConcesionary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAgencyDesc", sAgencyDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lexeTime = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Add: Adds a record in the Tab_Provider table
	'% Add: Añade un registro en la Tabla Tab_Provider
	Private Function Add() As Boolean
		Dim lexeTime As New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lexeTime = New eRemoteDB.Execute
		With lexeTime
			.StoredProcedure = "creTab_provider"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProv", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpDate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOutDate", dOutdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_serv_ord", nMax_serv_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPer_disc", nPer_disc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConcesionary", sConcesionary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAgencyDesc", sAgencyDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		lexeTime = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Delete: Deletes a record of the Tab_Provider table
	'% Delete: Borra un registro de la Tabla Tab_Provider
	Private Function Delete() As Boolean
		Dim lexeTime As New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lexeTime = New eRemoteDB.Execute
		With lexeTime
			.StoredProcedure = "delTab_provider"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lexeTime = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Cre_Prov_Branch: It creates an associated branch to a provider
	'% Cre_Prov_Branch: Crea un Ramo asociado a un Proveedor
	Public Function Cre_Prov_Branch(ByVal nFirst As Object, ByVal nTypeProv As Integer, ByVal nProvider As Integer, ByVal sClient As String, ByVal nBranch As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo Cre_Prov_Branch_Err
		
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "creProv_branch"
			.Parameters.Add("nFirst", nFirst, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProv", nTypeProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Cre_Prov_Branch = .Run(False)
		End With
		
		lexeTime = Nothing
		
Cre_Prov_Branch_Err: 
		If Err.Number Then
			Cre_Prov_Branch = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Class_Initialize: Inicializa las variables
	Private Sub Class_Initialize_Renamed()
		nProvider = eRemoteDB.Constants.intNull
		nTypeProv = eRemoteDB.Constants.intNull
		sClient = String.Empty
		sStatregt = String.Empty
		dInpdate = eRemoteDB.Constants.dtmNull
		dOutdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nOffice = eRemoteDB.Constants.intNull
		nMax_serv_ord = eRemoteDB.Constants.intNull
		nTypeSupport = eRemoteDB.Constants.intNull
		nPer_disc = eRemoteDB.Constants.intNull
		sConcesionary = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: desinicializa las variables
	Private Sub Class_Terminate_Renamed()
		nProvider = eRemoteDB.Constants.intNull
		nTypeProv = eRemoteDB.Constants.intNull
		sClient = String.Empty
		sStatregt = String.Empty
		dInpdate = eRemoteDB.Constants.dtmNull
		dOutdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nOffice = eRemoteDB.Constants.intNull
		nMax_serv_ord = eRemoteDB.Constants.intNull
		nTypeSupport = eRemoteDB.Constants.intNull
		nPer_disc = eRemoteDB.Constants.intNull
		sConcesionary = String.Empty
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






