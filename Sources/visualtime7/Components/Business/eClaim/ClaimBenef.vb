Option Strict Off
Option Explicit On
Public Class ClaimBenef
	'%-------------------------------------------------------%'
	'% $Workfile:: ClaimBenef.cls                           $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 14-01-13 6:01                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'

    Private Const C_SOAP_BENEF_NOT_ALLOWED As Integer = -1
    Private Const C_SOAP_BENEF_ALLOWED As Integer = 0

	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public sClient As String
	Public sCliename As String
	Public nBene_type As Integer
	Public dCompdate As Date
	Public sDemandant As String
	Public nUsercode As Integer
	Public nOffice_pay As Integer
	Public nOfficeAgen_pay As Integer
	Public nAgency_pay As Integer
	Public sClient_Rep As String
	Public nId As Integer
	Public nCover As Integer
	Public nModulec As Integer
	Public nCurrency As Integer
	Public nRelation As Integer
	Public nParticip As Double
	Public nPolicy As Double
	Public sLastName As String
	Public sLastName2 As String
	Public dBirthDat As Date
	Public Indic_Benef As Integer
	Public nAmount As Double
	Public dInit_date As Date
	Public dEnd_date As Date
	Public sClieName_Rep As String
	Public sLastName_Rep As String
	Public sLastName2_Rep As String
	Public nAge As Integer
	Public nIncapacity As Integer
	Public nCountBenef As Integer
	Public bClientControl As Boolean
	Public sDigit As String
	Public nRent As Double
	Public nPeriod As Integer
	Public nPayFreq As Integer
	
	Public sShas_Surv_Pension_Benefs As String
	Public dSummon As Date
	Public dSummon_Limit As Date
	Public dShowDate As Date
	Public nNoteNum As Double
	
	'**-Auxiliary properties
	'- Propiedades auxiliares
	Public nProvider As Integer
	Public bClientAsoc As Boolean
	Public sConting As String
    Public sDesign As String
    Public nPerson_typ As Integer

    Public Property nPaymentAddress As Integer
	'+ Del: Esta funcion se encarga de eliminar un Cliente de la tabla de Beneficiarios de un siniestro (ClaimBenef).
	Public Function Del(Byval nClaim As Double, Byval nCase_num As Integer, Byval nDeman_type As Integer, Byval sClient As String, Byval nId As Integer) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		On Error GoTo Del_Err
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		With lrecClaimBenef
			.StoredProcedure = "DelClaimBenef"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Del = .Run(False)
		End With
		
Del_Err: 
		If Err.Number Then
			Del = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	
	'% Find_Demandant:
	Public Function Find_Demandant(ByVal ldblClaim As Double, ByVal llngCase_num As Integer, ByVal llngDeman_type As Integer, Optional ByVal llngId As Integer = 0) As Boolean
		Dim lrecreaClaimBenefDem As eRemoteDB.Execute
		Dim larrClientInfo() As String
		
		On Error GoTo Find_Demandant_err
		lrecreaClaimBenefDem = New eRemoteDB.Execute
		'+Definición de parámetros para stored procedure 'insudb.reaClaimBenefDem'
		'+Información leída el 23/01/2001 13.36.20
		
		With lrecreaClaimBenefDem
			.StoredProcedure = "reaClaimBenefDem"
			.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", llngCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", llngDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", llngId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Demandant = True
				nClaim = ldblClaim
				nCase_num = llngCase_num
				nDeman_type = llngDeman_type
				sClient = .FieldToClass("sClient")
				'+Como campo "sCliedesc" es compuesto, se separa en nombre y digito
				larrClientInfo = Microsoft.VisualBasic.Split(.FieldToClass("sCliedesc"), "|")
				sDigit = larrClientInfo(1)
				sCliename = larrClientInfo(2)
				.RCloseRec()
			Else
				Find_Demandant = True
			End If
		End With
		
Find_Demandant_err: 
		If Err.Number Then
			Find_Demandant = False
		End If
		On Error GoTo 0
		lrecreaClaimBenefDem = Nothing
	End Function
	Public Function Find_client(ByVal nClaim As Double, ByVal sClient As String, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnFind As Boolean = False, Optional ByVal nBene_type As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		Static llngOldClaim As Double
		Static lstrOldClient As String
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		
		On Error GoTo Find_client_Err
		
		If llngOldClaim <> nClaim Or lstrOldClient <> sClient Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lblnFind Then
			
			llngOldClaim = nClaim
			lstrOldClient = sClient
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			
			lrecClaimBenef = New eRemoteDB.Execute
			
			With lrecClaimBenef
				.StoredProcedure = "reaClaimBenef" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					lblnRead = True
					nOffice_pay = .FieldToClass("nOffice_pay")
					sClient_Rep = .FieldToClass("sClient_rep")
					Me.nModulec = .FieldToClass("nModulec")
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		Find_client = lblnRead
Find_client_Err: 
		If Err.Number Then
			Find_client = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	
	'**%Find_Workshop: Find the workshop data associated to the claim-case
	'%Find_Workshop: Busca los datos del taller asociado al caso-siniestro
	Public Function Find_workshop(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaim_workshop As eRemoteDB.Execute
		
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		
		On Error GoTo Find_workshop_Err
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			
			lrecClaim_workshop = New eRemoteDB.Execute
			
			With lrecClaim_workshop
				.StoredProcedure = "reaClaim_Taller"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If (.Run) Then
					lblnRead = True
					nProvider = .FieldToClass("nProvider")
					sCliename = .FieldToClass("sCliename")
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find_workshop = lblnRead
		
Find_workshop_Err: 
		If Err.Number Then
			Find_workshop = False
		End If
		On Error GoTo 0
		lrecClaim_workshop = Nothing
	End Function
	Public Function FindClaimBenefChildren(ByVal nClaim As Double, ByVal nCase_num As Double, ByVal nDeman_type As Double, ByVal sClient As String, ByVal nId As Double) As Boolean
		Dim lrecvalClaimChilds As eRemoteDB.Execute
		Dim lstrHasChild As String
		lstrHasChild = "0"
		lrecvalClaimChilds = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.valClaimChilds'
		'Definición de parámetros para stored procedure 'insudb.valClaimChilds'
		'**Data read on 03/29/2001 19:10:03
		'Información leída el 29/03/2001 19:10:03
		With lrecvalClaimChilds
			.StoredProcedure = "valClaimBenefChilds"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHasChild", lstrHasChild, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				FindClaimBenefChildren = Trim(.Parameters("sHasChild").Value) = "1"
			End If
		End With
		lrecvalClaimChilds = Nothing
		
	End Function
	
	'**%ValClaimBenefType: the objetive of this function is to validate that a record exists in the ClaimBenef table according to the beneficiary (figure).
	'%ValClaimBenefType: El objetivo de esta función es validar si existe un registro en la tabla ClaimBenef según el tipo de beneficiario (figura).
	Public Function ValClaimBenefType(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBene_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'**Define the variable lrecClaimBenef to execute the stored procedure
		'Se define la variable lrecClaimBenef para ejecutar el stored procedure
		Dim lrecClaimBenef As eRemoteDB.Execute
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static llngOldBene_type As Integer
		Static lblnRead As Boolean
		
		On Error GoTo ValClaimBenefType_Err
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or llngOldBene_type <> nBene_type Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			llngOldBene_type = nBene_type
			
			lrecClaimBenef = New eRemoteDB.Execute
			
			With lrecClaimBenef
				.StoredProcedure = "valClaimbenefType"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					If .FieldToClass("lCount") > 0 Then
						lblnRead = True
						nCountBenef = .FieldToClass("lCount")
					Else
						lblnRead = False
					End If
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		ValClaimBenefType = lblnRead
		
ValClaimBenefType_Err: 
		If Err.Number Then
			ValClaimBenefType = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	
	'**%insValClaimBenef: the objective of this function is to validate if a record exists in the ClaimBenef table
	'%insValClaimBenef: El objetivo de esta función es validar si existe un registro en la tabla ClaimBenef
	Public Function ValClaimBenef(ByVal ldblClaim As Double, ByVal llngCase As Integer, ByVal llngDeman As Integer, ByVal lstrClient As String, ByVal llngId As Integer, Optional ByVal llngBene_type As Integer = 0) As Boolean
		Dim lrecreaClaimBenef_1 As eRemoteDB.Execute
		
		On Error GoTo ValClaimBenef_Err
		
		lrecreaClaimBenef_1 = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.reaClaimBenef_1'
		'Definición de parámetros para stored procedure 'insudb.reaClaimBenef_1'
		'**Data read on 01/25/2001 10:15:40 AM
		'Información leída el 25/01/2001 10:15:40 AM
		With lrecreaClaimBenef_1
			.StoredProcedure = "reaClaimBenef_1"
			.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", llngCase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", llngDeman, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBene_type", llngBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", llngId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValClaimBenef = True
				.RCloseRec()
			End If
		End With
		
ValClaimBenef_Err: 
		If Err.Number Then
			ValClaimBenef = False
		End If
		On Error GoTo 0
		lrecreaClaimBenef_1 = Nothing
	End Function
	
	'%FindClaimBenef_1:This method returns TRUE or FALSE depending if the records exists in the table "ClaimBenef"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "ClaimBenef"
	Public Function FindClaimBenef_1(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBene_type As Integer, Optional ByVal sShowNum As String = "", Optional ByVal sCondition As String = "") As Boolean
		Dim lrectabClaimBenef_1 As eRemoteDB.Execute
		
		On Error GoTo FindClaimBenef_1_Err
		lrectabClaimBenef_1 = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.tabClaimBenef_1'
		'Definición de parámetros para stored procedure 'insudb.tabClaimBenef_1'
		'**Data read on 01/26/2001 11:27:56 AM
		'Información leída el 26/01/2001 11:27:56 AM
		With lrectabClaimBenef_1
			.StoredProcedure = "tabClaimBenef_1pkg.tabClaimBenef_1"
			.Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                    FindClaimBenef_1 = True
                nCountBenef = 0
				Do While Not .EOF
                    nCountBenef = nCountBenef + 1

                    If nCountBenef > 1 Then
                        Exit Do 
                    End If

                    sClient = .FieldToClass("sClient")
                    sCliename = .FieldToClass("sCliename")
                    nOffice_pay = .FieldToClass("nOffice_pay")
                    nOfficeAgen_pay = .FieldToClass("nOfficeAgen_pay")
                    nAgency_pay = .FieldToClass("nAgency_pay")
                    sClient_Rep = .FieldToClass("sClient_rep")
                    nId = .FieldToClass("nId")

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FindClaimBenef_1 = False
            End If
		End With

FindClaimBenef_1_Err: 
		If Err.Number Then
			FindClaimBenef_1 = False
		End If
		On Error GoTo 0
		lrectabClaimBenef_1 = Nothing
	End Function


    '%FindClaimBenef_SI008:This method returns TRUE or FALSE depending if the records exists in the table "ClaimBenef"
    '%FindClaimBenef_SI008: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "ClaimBenef"
    Public Function FindClaimBenef_SI008(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBene_type As Integer, Optional ByVal sShowNum As String = "", Optional ByVal sCondition As String = "") As Boolean
        Dim lrectabClaimBenef_SI008 As eRemoteDB.Execute

        On Error GoTo FindClaimBenef_SI008_Err
        lrectabClaimBenef_SI008 = New eRemoteDB.Execute

        '**Parameters definition for the stored procedure 'insudb.tabClaimBenef_1'
        'Definición de parámetros para stored procedure 'insudb.tabClaimBenef_1'
        '**Data read on 01/26/2001 11:27:56 AM
        'Información leída el 26/01/2001 11:27:56 AM
        With lrectabClaimBenef_SI008
            .StoredProcedure = "tabClaimBenef_SI008pkg.tabClaimBenef_SI008"
            .Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindClaimBenef_SI008 = True
                nCountBenef = 0
                Do While Not .EOF
                    nCountBenef = nCountBenef + 1

                    If nCountBenef > 1 Then
                        Exit Do
                    End If

                    sClient = .FieldToClass("sClient")
                    sCliename = .FieldToClass("sCliename")
                    nOffice_pay = .FieldToClass("nOffice_pay")
                    nOfficeAgen_pay = .FieldToClass("nOfficeAgen_pay")
                    nAgency_pay = .FieldToClass("nAgency_pay")
                    sClient_Rep = .FieldToClass("sClient_rep")
                    nId = .FieldToClass("nId")

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FindClaimBenef_SI008 = False
            End If
        End With

FindClaimBenef_SI008_Err:
        If Err.Number Then
            FindClaimBenef_SI008 = False
        End If
        On Error GoTo 0
        lrectabClaimBenef_SI008 = Nothing
    End Function

	
	'+ Find_Benef: Localiza todos los beneficiarios de un determinado siniestro y los añade
	'+ a la clase correspondiente (ClaimBenef) - ACM - 26/01/2001
	Public Function FindBenef(ByVal nClaim As Double, ByVal sClient As String, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal nBene_type As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		On Error GoTo FindBenef_Err
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		With lrecClaimBenef
			.StoredProcedure = "reaClaimBenef"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nOffice_pay = .FieldToClass("nOffice_pay")
				nOfficeAgen_pay = .FieldToClass("nOfficeAgen_pay")
				nAgency_pay = .FieldToClass("nAgency_pay")
				sClient_Rep = .FieldToClass("sClient_rep")
				nId = .FieldToClass("nId")
				nCountBenef = .RecordCount
				.RCloseRec()
				FindBenef = True
			Else
				FindBenef = False
			End If
		End With
		
FindBenef_Err: 
		If Err.Number Then
			FindBenef = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	'+ InsPostBenef: Guarda los cambios hechos en claimbenef
	Public Function UpdBenefPay(ByVal nClaim As Double, ByVal sClient As String, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOffice_pay As Integer, ByVal nAgency_pay As Integer, ByVal nOfficeAgen_pay As Integer) As Boolean
		Dim lrecClaimBenefPay As eRemoteDB.Execute
		On Error GoTo UpdBenefPay_Err
		
		lrecClaimBenefPay = New eRemoteDB.Execute
		
		With lrecClaimBenefPay
			.StoredProcedure = "UpdBenefOffpay"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice_pay", nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				UpdBenefPay = True
			Else
				UpdBenefPay = False
			End If
		End With
		
UpdBenefPay_Err: 
		If Err.Number Then
			UpdBenefPay = False
		End If
		On Error GoTo 0
		lrecClaimBenefPay = Nothing
	End Function
	
	'**Find_ClaimBenefAsoc: Allows to built a collection with the associated claim clients
	'%Find_ClaimBenefAsoc: Permite construir una coleccion con los clientes asociados
	'%al siniestro
	Public Function Find_ClaimBenefAsoc(ByVal nClaim As Double, ByVal nCase_num As Double, ByVal nDeman_type As Double, Optional ByVal sBene_type As String = "1,2,4", Optional ByVal sShowNum As String = "1", Optional ByVal sCondition As String = "") As Collection
		Dim lrectabClaimBenef_2 As eRemoteDB.Execute
		
		Dim lclsClaimbenef As ClaimBenef
		Dim lstrBene_type As String
		
		On Error GoTo Find_ClaimBenefAsoc_Err
		
		lrectabClaimBenef_2 = New eRemoteDB.Execute
		
		Find_ClaimBenefAsoc = New Collection
		
		If Me.bClientControl Then
			lstrBene_type = String.Empty
		Else
			lstrBene_type = sBene_type
		End If
		
		'**Parameters definition for the stored procedure 'insudb.tabClaimBenef_2'
		'Definición de parámetros para stored procedure 'insudb.tabClaimBenef_2'
		'**Data read on 02/06/2001 16:30:03
		'Información leída el 06/02/2001 16:30:03
		
		With lrectabClaimBenef_2
			.StoredProcedure = "tabClaimBenef_2pkg.tabClaimBenef_2"
			.Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBene_type", lstrBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsClaimbenef = New ClaimBenef
					lclsClaimbenef.sClient = .FieldToClass("sClient")
					lclsClaimbenef.sCliename = .FieldToClass("sCliename")
					lclsClaimbenef.nBene_type = .FieldToClass("nBene_type")
					lclsClaimbenef.sDigit = .FieldToClass("sDigit")
					Me.bClientAsoc = True
					Find_ClaimBenefAsoc.Add(lclsClaimbenef)
					lclsClaimbenef = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_ClaimBenefAsoc_Err: 
		On Error GoTo 0
		lrectabClaimBenef_2 = Nothing
		
	End Function


    'ValidateSOAPDeathBenefRestriction: Esta función verifica que el beneficario sea permitido para la cobertura de SOAP
    Private Function ValidateSOAPDeathBenefRestriction(ByVal nClaim As Integer, ByVal nCaseNum As Integer, ByVal nDemanType As Integer, ByVal sClientCode As String, ByVal nCover As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
        Dim lclsClaimant As New ClaimBenef
        Dim nRV As Integer = C_SOAP_BENEF_ALLOWED
        Dim lclsProduct As New eProduct.Product

        If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
            '+sBrancht = "6" significa un producto SOAP
            If lclsProduct.sBrancht = "6" And nCover = 1001 Then
                If lclsClaimant.FindBenef(nClaim, sClientCode, nCaseNum, nDemanType, 14) Then
                    nRV = C_SOAP_BENEF_NOT_ALLOWED
                End If
            End If
        Else
            Throw New Exception("El producto no existe (" & nBranch & "," & nProduct & "," & dEffecdate & ")")
        End If
        Return nRV
    End Function

	
	'insValSI004: Esta función realiza las validaciones de la ventana SI004
	Public Function insValSI629(ByVal sCodispl As String, ByVal Action As String, ByVal sSel As String, ByVal nCover As Integer, ByVal sClientCode As String, ByVal sLastName As String, ByVal sLastName2 As String, ByVal sFirstName As String, ByVal dBirthDat As Date, ByVal nRelaship As Integer, ByVal nParticip As Double, ByVal sRepresentCode As String, ByVal sRLastName As String, ByVal sRLastName2 As String, ByVal sRFirstName As String, ByVal nOffice_pay As Integer, ByVal nOfficeAgen_pay As Integer, ByVal nAgency_pay As Integer, ByVal nRent As Double, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sTypeBenef As String, ByVal sPopUp As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nClaim As Double, ByVal dEffecdate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClient As eClient.Client
		Dim lclsValClient As eClient.ValClient
		Dim lclsValField As eFunctions.valField
		Dim lintCount As Integer
		Dim nParticip_Aux As Double
		Dim lrecClaimBenef As eRemoteDB.Execute
        Dim lstrSep As String = ""
        Dim lstrError As String = ""
        Dim lstrError_percent As String
		On Error GoTo insValSI629_err
		
		lstrSep = "||"
		
		lclsErrors = New eFunctions.Errors
		lclsClient = New eClient.Client
		lclsValClient = New eClient.ValClient
		lclsValField = New eFunctions.valField
		
		If Action = "Add" Or Action = "Update" Then
			'+ Validacion del campo "Client"
			If sClientCode = String.Empty Then
				lstrError = lstrError & lstrSep & "4122"
			Else
				If lclsClient.Find(sClientCode) Then
					'+ Se valida que el cliente este vivo
					If lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
						lstrError = lstrError & lstrSep & "2051"
					End If
					'+ Se valida que el cliente no esté bloqueado
					If lclsClient.sBlockade = "1" Then 'Blockade = 1 ---> Bloqueado
						lstrError = lstrError & lstrSep & "2063"
					End If
				Else
					'+ Debe estar lleno el campo "Apellido paterno"
                    '+ El valor 1 representa a las personas naturales 
                    If Trim(sLastName) = String.Empty And Me.nPerson_typ = 1 Then
						lstrError = lstrError & lstrSep & "2807"
					End If
					
					'+ Debe estar lleno el campo "Nombres"
					If Trim(sFirstName) = String.Empty Then
						lstrError = lstrError & lstrSep & "2004"
					End If
				End If
			End If
			
            If ValidateSOAPDeathBenefRestriction(nClaim, nCase_num, nDeman_type, sClientCode, nCover, nBranch, nProduct, dEffecdate) = C_SOAP_BENEF_NOT_ALLOWED Then
                lstrError = lstrError & lstrSep & "978131"
            End If


            If nParticip <= 0 Then
                lstrError = lstrError & lstrSep & "1124"
            End If

			'+ La cobertura es obligatoria
			If nCover = eRemoteDB.Constants.intNull Or nCover = 0 Then
				lstrError = lstrError & lstrSep & "11163"
			ElseIf Action = "Add" Then 
				'+ La combinación beneficiario-cobertura debe ser unica
				If sClientCode <> String.Empty Then
					If InsExists(nClaim, nCase_num, nDeman_type, sClientCode, nCover) Then
						lstrError = lstrError & lstrSep & "55790"
					End If
				End If
			End If

            '+ Validacion sobre el campo Oficina
            If nOfficeAgen_pay = eRemoteDB.Constants.intNull Or nOfficeAgen_pay = 0 Then
                lstrError = lstrError & lstrSep & "55519"
		End If

            '+ Validacion sobre el campo Agencia
            If nAgency_pay = eRemoteDB.Constants.intNull Or nAgency_pay = 0 Then
                lstrError = lstrError & lstrSep & "1080"
            End If
        End If
		'+ Validación para controlar los porcentajes de participacion de los beneficiarios, ya que si los mismos
		'+ no estan asociados a la poliza la sumatoria de los porcentajes debe ser 100%
		If sPopUp = "2" Then
            If String.IsNullOrEmpty(sClientCode) Then
                lstrError = lstrError & lstrSep & "3957"
            Else
			lstrError_percent = insValidate(nClaim, 1)
			If lstrError_percent <> String.Empty Then
				lstrError = lstrError & lstrSep & lstrError_percent
			End If
		End If
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI629",  ,  ,  ,  ,  , lstrError)
				insValSI629 = .Confirm()
			End With
		End If
		
insValSI629_err:
        If Err.Number Then
            insValSI629 = ""
            insValSI629 = insValSI629 & " " & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lclsClient = Nothing
		lclsValClient = Nothing
		lclsValField = Nothing
	End Function
	
	'%insPostSI629: Registra en ClaimBenef los beneficiarios de un siniestro
    Public Function insPostSI629(ByVal sCodispl As String, 
                                       ByVal Action As String, 
                                       ByVal nClaim As Double, 
                                       ByVal nCase_num As Integer, 
                                       ByVal nDeman_type As Integer, 
                                       ByVal sSel As String, 
                                       ByVal nCover As Integer, 
                                       ByVal nModulec As Integer, 
                                       ByVal nCurrency As Integer, 
                                       ByVal sClientCode As String, 
                                       ByVal sDigit As String, 
                                       ByVal sLastName As String, 
                                       ByVal sLastName2 As String, 
                                       ByVal sFirstName As String, 
                                       ByVal dBirthDat As Date, 
                                       ByVal nRelaship As Integer, 
                                       ByVal nParticip As Double, 
                                       ByVal sRepresentCode As String, 
                                       ByVal sRDigit As String, 
                                       ByVal sRLastName As String, 
                                       ByVal sRLastName2 As String, 
                                       ByVal sRFirstName As String, 
                                       ByVal nOffice_pay As Integer, 
                                       ByVal nOfficeAgen_pay As Integer, 
                                       ByVal nAgency_pay As Integer, 
                                       ByVal nRent As Double, 
                                       ByVal dInitDate As Date, 
                                       ByVal dEndDate As Date, 
                                       ByVal nUsercode As Integer, 
                                       ByVal nId As Integer, 
                                       ByVal sShas_Surv_Pension_Benefs As String, 
                                       ByVal dSummon As Date, 
                                       ByVal dSummon_Limit As Date, 
                                       ByVal dShowDate As Date, 
                                       ByVal nNoteNum As Double, 
                                 Optional ByVal sPopup As String = "",
                                 Optional ByVal nPaymentAddress As Integer = 1) As Boolean
        Dim lclsClaim_win As eClaim.Claim_win
        Dim lclsClient As eClient.Client
        Dim lclsClaim As eClaim.Claim

        lclsClaim = New eClaim.Claim

        If Action = "Add" Or Action = "Update" Then

            '+ Se registra el cliente en el sistema, con los datos mínimos.
            lclsClient = New eClient.Client
            With lclsClient
                If Not .Find(sClientCode) Then
                    .sClient = sClientCode
                    .nUsercode = nUsercode
                    If Me.nPerson_typ > 0 Then
                        .nPerson_typ = Me.nPerson_typ
                    Else
                        .nPerson_typ = 1
                    End If

                    'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                    .dBirthdat = IIf(IsNothing(dBirthDat), eRemoteDB.Constants.dtmNull, dBirthDat)
                    .nIncapacity = eRemoteDB.Constants.intNull
                    .nArea = eRemoteDB.Constants.intNull
                    .nIncap_cod = eRemoteDB.Constants.intNull
                    .nNationality = eRemoteDB.Constants.intNull
                    .nHealth_org = eRemoteDB.Constants.intNull
                    .nAfp = eRemoteDB.Constants.intNull
                    .nInvoicing = eRemoteDB.Constants.intNull
                    .nLimitdriv = eRemoteDB.Constants.intNull
                    .nDisability = eRemoteDB.Constants.intNull
                    .nTypDriver = eRemoteDB.Constants.intNull
                    .nHouse_type = eRemoteDB.Constants.intNull
                    .sDigit = lclsClaim.CalcDigit(sClientCode)
                    .sFirstName = sFirstName
                    If .nPerson_typ = 1 Then
                    .sCliename = sLastName & " " & sLastName2 & ", " & sFirstName
                        .sLastName = sLastName
                        .sLastname2 = sLastName2
                    Else
                        .sCliename = sFirstName
                    End If
                    insPostSI629 = .AddClient
                End If
            End With
            lclsClient = Nothing
        End If

        If Action = "Add" Or Action = "Update" Then
            '+ Si se ingresa un representante legal y le mismo no existe en la base de datos de clientes se crea
            If sRepresentCode <> String.Empty And sRepresentCode <> " " Then
                lclsClient = New eClient.Client
                With lclsClient
                    If Not .Find(sRepresentCode) Then
                        .sClient = sRepresentCode
                        If Me.nPerson_typ > 0 Then
                            .nPerson_typ = Me.nPerson_typ
                        Else
                        .nPerson_typ = 1
                        End If
                        .nUsercode = nUsercode
                        .nIncapacity = eRemoteDB.Constants.intNull
                        .nArea = eRemoteDB.Constants.intNull
                        .nIncap_cod = eRemoteDB.Constants.intNull
                        .nNationality = eRemoteDB.Constants.intNull
                        .nHealth_org = eRemoteDB.Constants.intNull
                        .nAfp = eRemoteDB.Constants.intNull
                        .nInvoicing = eRemoteDB.Constants.intNull
                        .nLimitdriv = eRemoteDB.Constants.intNull
                        .nDisability = eRemoteDB.Constants.intNull
                        .nTypDriver = eRemoteDB.Constants.intNull
                        .nHouse_type = eRemoteDB.Constants.intNull
                        .sDigit = lclsClaim.CalcDigit(sRepresentCode)
                        .sLastName = sRLastName
                        .sLastname2 = sRLastName2
                        .sFirstName = sRFirstName
                        .sCliename = sRLastName & "," & sRFirstName
                        insPostSI629 = .AddClient
                    End If
                End With
                lclsClient = Nothing
            End If
        End If

        Me.nClaim = nClaim
        Me.nCase_num = nCase_num
        Me.nDeman_type = nDeman_type
        Me.sClient = sClientCode
        Me.nOffice_pay = IIf(nOffice_pay = 0, eRemoteDB.Constants.intNull, nOffice_pay)
        Me.nUsercode = nUsercode
        Me.nRelation = nRelaship
        Me.nParticip = nParticip
        Me.nAmount = nRent
        Me.dInit_date = dInitDate
        Me.dEnd_date = dEndDate
        Me.sClient_Rep = IIf(sRepresentCode = " ", String.Empty, sRepresentCode)
        Me.nCover = nCover
        Me.nModulec = nModulec
        Me.nCurrency = nCurrency
        Me.nId = nId
        Me.nOffice_pay = nOffice_pay
        Me.nOfficeAgen_pay = nOfficeAgen_pay
        Me.nAgency_pay = nAgency_pay
        Me.nPaymentAddress = nPaymentAddress

        Me.sShas_Surv_Pension_Benefs = sShas_Surv_Pension_Benefs
        Me.dSummon = dSummon
        Me.dSummon_Limit = dSummon_Limit
        Me.dShowDate = dShowDate
        Me.nNoteNum = nNoteNum

        insPostSI629 = UpdateSI629()

        '+ Se actualiza el check "Con Contenido"
        If insPostSI629 And sPopup = "2" Then
            lclsClaim_win = New eClaim.Claim_win
            Call lclsClaim_win.Add_Claim_win(nClaim, "SI629", "2", nUsercode)
        End If
        lclsClaim_win = Nothing

    End Function
	'+ Funcion que se encarga de actualizar la tabla de beneficiarios de un siniestro (ClaimBenef)
	Public Function UpdateSI629() As Boolean
		
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		lrecClaimBenef = New eRemoteDB.Execute
		'**Parameters definition for stored procedure 'insudb.creClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.creClaim_caus'
		'**Infoemation read on October 04 of 2001 06:23:31 p.m.
		'Información leída el 04/10/2001 06:23:31 p.m.
		
		With lrecClaimBenef
			.StoredProcedure = "insClaimBenef"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice_pay", nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", IIf(nRelation = 0, eRemoteDB.Constants.intNull, nRelation), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient_rep", sClient_Rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("sShas_Surv_Pension_Benefs", sShas_Surv_Pension_Benefs, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dSummon", dSummon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dSummon_Limit", dSummon_Limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dShowDate", dShowDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPAYMENTADDRESS", nPaymentAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateSI629 = .Run(False)
		End With
		lrecClaimBenef = Nothing
	End Function
	
	'%InsExists: Verifica la existencia de beneficiarios por poliza
	Public Function InsExists(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClientCode As String, ByVal nCover As Integer) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsExists_Err
		
		nExists = 0
		InsExists = True
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		With lrecClaimBenef
			.StoredProcedure = "insExists_ClaimBenef"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					InsExists = True
				Else
					InsExists = False
				End If
			Else
				InsExists = False
			End If
		End With
		
InsExists_Err: 
		If Err.Number Then
			InsExists = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	'%CalIndemnity: Calculo de indemnización producto universitario
	Public Function CalIndemnity(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nClaimType As Integer, ByVal nIndemnity As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		On Error GoTo CalIndemnity_err
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		With lrecClaimBenef
			.StoredProcedure = "insCalIndemnity"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimType", nClaimType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnity", nIndemnity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				CalIndemnity = .FieldToClass("nExist") = 1
				If CalIndemnity Then
					nAmount = .FieldToClass("nAmount")
					dInit_date = .FieldToClass("dInit_date")
					dEnd_date = .FieldToClass("dEnd_date")
					nCurrency = .FieldToClass("nCurrency")
					nRent = .FieldToClass("nRent")
					nPeriod = .FieldToClass("nPeriod")
					nPayFreq = .FieldToClass("nPayFreq")
				End If
				.RCloseRec()
			End If
		End With
		
CalIndemnity_err: 
		If Err.Number Then
			CalIndemnity = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	
	'%insCalBenefPercent: Verifica el porcentaje de participacion pór caso - cobertura.
	Public Function insCalBenefPercent(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover As Integer, Optional ByVal sClient As String = "") As Double
		Dim lrecClaimBenef As eRemoteDB.Execute
		
		On Error GoTo insCalBenefPercent_err
		
		lrecClaimBenef = New eRemoteDB.Execute
		
		insCalBenefPercent = 0
		
		With lrecClaimBenef
			.StoredProcedure = "ReaClaimBenefPerCover"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				insCalBenefPercent = .FieldToClass("nParticip")
				.RCloseRec()
			End If
		End With
		
insCalBenefPercent_err: 
		If Err.Number Then
			insCalBenefPercent = 0
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
	
	'%insValidate: Valida opcion 1 los porcentajes de participacion de los beneficiarios para todos los casos del siniestro.
	' opcion 2 si existe provision para todos los casos de siniestro
	Public Function insValidate(ByVal nClaim As Double, ByVal nOption As Integer) As String
		Dim lclsValidate As eRemoteDB.Execute
		Dim lstrError As String
		
		On Error GoTo insValidate_Err
		
		lclsValidate = New eRemoteDB.Execute
		With lclsValidate
			.StoredProcedure = "insValPercent_particip"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			If lstrError <> String.Empty Then
				insValidate = lstrError
			End If
			
		End With
		
insValidate_Err: 
		If Err.Number Then
			insValidate = ""
		End If
		On Error GoTo 0
		lclsValidate = Nothing
	End Function
	
	Private Sub Class_Initialize_Renamed()
		nClaim = eRemoteDB.Constants.intNull
		nCase_num = eRemoteDB.Constants.intNull
		nDeman_type = eRemoteDB.Constants.intNull
		sClient = String.Empty
		sCliename = String.Empty
		nBene_type = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		sDemandant = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		nOffice_pay = eRemoteDB.Constants.intNull
		nOfficeAgen_pay = eRemoteDB.Constants.intNull
		nAgency_pay = eRemoteDB.Constants.intNull
		sClient_Rep = String.Empty
		nId = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nRelation = eRemoteDB.Constants.intNull
		nParticip = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		sLastName = String.Empty
		sLastName2 = String.Empty
		dBirthDat = eRemoteDB.Constants.dtmNull
		Indic_Benef = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		dInit_date = eRemoteDB.Constants.dtmNull
		dEnd_date = eRemoteDB.Constants.dtmNull
		sClieName_Rep = String.Empty
		sLastName_Rep = String.Empty
		sLastName2_Rep = String.Empty
		nAge = eRemoteDB.Constants.intNull
		nIncapacity = eRemoteDB.Constants.intNull
		nCountBenef = eRemoteDB.Constants.intNull
		bClientControl = False
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%insPostSI629: Registra en ClaimBenef los beneficiarios de un siniestro
	Public Function insClaimBenefAPV(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOffice_pay As Integer, ByVal nOfficeAgen_pay As Integer, ByVal nAgency_pay As Integer, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffectdate As Date) As Boolean
		Dim lrecClaimBenef As eRemoteDB.Execute
		lrecClaimBenef = New eRemoteDB.Execute
		
		On Error GoTo insClaimBenefAPV_Err
		'**Parameters definition for stored procedure 'insudb.creClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.creClaim_caus'
		'**Infoemation read on October 04 of 2001 06:23:31 p.m.
		'Información leída el 04/10/2001 06:23:31 p.m.
		
		With lrecClaimBenef
			.StoredProcedure = "insClaimBenefAPV"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice_pay", nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen_pay", nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency_pay", nAgency_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffectdate", dEffectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insClaimBenefAPV = .Run(False)
		End With
		
		
insClaimBenefAPV_Err: 
		If Err.Number Then
			insClaimBenefAPV = False
		End If
		On Error GoTo 0
		lrecClaimBenef = Nothing
	End Function
End Class






