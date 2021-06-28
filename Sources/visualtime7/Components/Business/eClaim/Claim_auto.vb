Option Strict Off
Option Explicit On
Imports System.Configuration
Public Class Claim_auto
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_auto.cls                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 12/11/03 16.47                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties of the correspond class to the claim_auto table (01/15/2001)
	'-Se definen las propiedades principales de la clase correspondientes a la tabla claim_auto (15/01/2001)
	
	'Column_name                         Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nClaim As Double 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public nAuto_quant As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nCase_num As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sBlame As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nDeman_type As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sDriver_cod As String 'char                                                                                                                             no                                  14                      yes                                 no                                  yes
	Public sInfraction As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sSummary As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sPoliceDem As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sInd_EIR As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLocation As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nWorksh As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer
	Public sDriver_claim As String
	Public nFine As Double
	Public sCourt As String
	Public dDemand_date As Date
	Public sPolStat_deman As String
	Public nPage As Integer
	Public nParagraph As Integer
	Public sPol_Station As String
	Public nPoliceDoc As Double
	Public dPoldoc_date As Date
	Public sAlcoholic As String
	Public nNotenum As Double
	Public sWitness As String
    Public dDoccurdat As Date
    Public dDecladat As Date
	
	
	'-Auxiliaries property
	'- Propiedades auxiliares
	Public sCliename As String
	Public sLastName As String
	Public sDesWorksh As String
	Public dDriverDat As Date
	Public sLicense As String
	
	'**%Find: Obtain the values of the properties of the class
	'%Find : Obtiene los valores de las propiedades de la clase
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaim_auto As eRemoteDB.Execute
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		
		On Error GoTo Find_Err
		
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			
			'+ Realiza la lectura de los datos asociados al siniestro
			lrecClaim_auto = New eRemoteDB.Execute
			With lrecClaim_auto
				.StoredProcedure = "reaClaim_Auto"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If (.Run) Then
					lblnRead = True
					sBlame = IIf(.FieldToClass("nBlame") = eRemoteDB.Constants.intNull, String.Empty, CStr(.FieldToClass("nBlame")))
					sInfraction = IIf(.FieldToClass("nInfraction") = eRemoteDB.Constants.intNull, String.Empty, CStr(.FieldToClass("nInfraction")))
					nAuto_quant = .FieldToClass("nAuto_quant")
					sPoliceDem = .FieldToClass("sPoliceDem")
					sInd_EIR = .FieldToClass("sInd_EIR")
					nLocation = .FieldToClass("nLocation")
					sSummary = .FieldToClass("sSummary")
					sDriver_cod = .FieldToClass("sClient")
					sCliename = .FieldToClass("sCliename")
					sLicense = .FieldToClass("sLicense")
					dDriverDat = .FieldToClass("dDriverdat")
					nWorksh = .FieldToClass("nProvider")
					sDesWorksh = .FieldToClass("sDesWorksh")
					nUsercode = .FieldToClass("nUsercode")
					sDriver_claim = .FieldToClass("sDriver_claim")
					nFine = .FieldToClass("nFine")
					sCourt = .FieldToClass("sCourt")
					dDemand_date = .FieldToClass("dDemand_date")
					sPolStat_deman = .FieldToClass("sPolStat_deman")
					nPage = .FieldToClass("nPage")
					nParagraph = .FieldToClass("nParagraph")
					sPol_Station = .FieldToClass("sPol_Station")
					nPoliceDoc = .FieldToClass("nPoliceDoc")
					dPoldoc_date = .FieldToClass("dPoldoc_date")
					sAlcoholic = .FieldToClass("sAlcoholic")
					nNotenum = .FieldToClass("nNotenum")
					sWitness = .FieldToClass("sWitness")
					.RCloseRec()
				Else
					lblnRead = False
				End If
				
			End With
			lrecClaim_auto = Nothing
		End If
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
			Find = False
		End If
		On Error GoTo 0
	End Function
	'**%Update: Updates the data in the claim_auto table
	'%Update: Actualiza los datos en la tabla claim_auto
	Public Function Update() As Boolean
		Dim lrecClaim_auto As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecClaim_auto = New eRemoteDB.Execute
		
		With lrecClaim_auto
			.StoredProcedure = "insClaim_Auto"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAuto_quant", nAuto_quant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Trim(sBlame) = String.Empty Then
				.Parameters.Add("nBlame", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nBlame", CShort(sBlame), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sClient", sDriver_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Trim(sInfraction) = String.Empty Then
				.Parameters.Add("nInfraction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nInfraction", CInt(sInfraction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sSummary", sSummary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPoliceDem", sPoliceDem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Eir", sInd_EIR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocation", nLocation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", IIf(nWorksh = 0, eRemoteDB.Constants.intNull, nWorksh), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("sDriver_claim", sDriver_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFine", nFine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCourt", sCourt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDemand_date", dDemand_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolStat_deman", sPolStat_deman, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPage", nPage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParagraph", nParagraph, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPol_Station", sPol_Station, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPoliceDoc", nPoliceDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPoldoc_date", dPoldoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAlcoholic", sAlcoholic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWitness", sWitness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		lrecClaim_auto = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**%ValClient_auto: The object of this function is to validate that the record exists in the table Claim_auto.
	'**If the client is passed it will search the specific client, otherwise, it verifies
	'**%if the table has info related to the claim case treatment.
	'%ValClientClaim_auto: El objetivo de esta función es validar si existe un registro en la tabla Claim_auto.
	'%Si se le pasa el Cliente busca ese determinado cliente, sino verifica
	'%si la tabla tiene información relacionada al caso del siniestro en tratamiento.
	Public Function ValClientClaim_auto(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal sClient As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		'**Define the lrecClaimBenef variable for execute the stored procedure
		'Se define la variable lrecClaimBenef para ejecutar el store procedure
		Dim lrecClaim_auto As eRemoteDB.Execute
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		Static lstrOldClient As String
		
		On Error GoTo ValClientClaim_auto_Err
		
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lstrOldClient <> sClient Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_num
			lintOldDeman_type = nDeman_type
			If sClient <> String.Empty Then
				lstrOldClient = sClient
			End If
			
			lrecClaim_auto = New eRemoteDB.Execute
			
			With lrecClaim_auto
				.StoredProcedure = "valClientClaim_auto" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If sClient <> String.Empty Then
					.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If .Run Then
					If .FieldToClass("lCount") > 0 Then
						lblnRead = True
					Else
						lblnRead = False
					End If
					.RCloseRec()
				Else
					lblnRead = False
				End If
				
			End With
			lrecClaim_auto = Nothing
		End If
		
		ValClientClaim_auto = lblnRead
		
ValClientClaim_auto_Err: 
		If Err.Number Then
			lblnRead = False
			ValClientClaim_auto = False
		End If
		On Error GoTo 0
	End Function
	
	'**insValSI018: Validates the sequence of the cases actualization
	'insValSI018:Valida el frame de secuencia de actualizacion de casos
	Public Function insValSI018(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBlame As Integer, ByVal nInfraction As Integer, ByVal nAuto_quant As Integer, ByVal sDriverCod As String, ByVal nWorksh As Integer, ByVal sDriver_claim As String, ByVal sLastName As String, ByVal sCliename As String, ByVal sPoliceDem As String, ByVal sSummary As String, ByVal sInd_EIR As String, ByVal nFine As Double, ByVal sCourt As String, ByVal dDemand_date As Date, ByVal sPolStat_deman As String, ByVal nPage As Integer, ByVal nParagraph As Integer, ByVal sPol_Station As String, ByVal nPoliceDoc As Double, ByVal dPoldoc_date As Date, ByVal sAlcoholic As String, ByVal nNotenum As Double, ByVal sWitness As String, ByVal sLastNameWitness As String, ByVal sClienameWitness As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjObject As Object
		Dim lobjClient As eClient.ValClient
		Dim lclsClient As eClient.Client
        Dim lclsProduct As New eProduct.Product
        Dim lclsClaim As New eClaim.Claim

		Dim lstrSep As String
        Dim lstrError As String = String.Empty
		
		lclsErrors = New eFunctions.Errors
		lclsClient = New eClient.Client
		
		On Error GoTo insValSI018_err
		
		lstrSep = "||"
		
		'**+Validation of the FIELD driver fault
		'+Validacion del CAMPO culpabilidad del conductor
		If nBlame <= 0 Then
			'Call lclsErrors.ErrorMessage(sCodispl, 4126)
            If lclsClaim.Find(nClaim) Then
                If lclsProduct.Find(lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.dOccurdat) Then
                    If lclsProduct.sBrancht <> eProduct.Product.pmBrancht.pmSegurosProvisionales Then
			lstrError = lstrError & lstrSep & "4126"
		End If
                Else
                    Throw New Exception("El producto no existe.")
                End If
            Else
                Throw New Exception("El siniestro no existe.")
            End If
        End If
		
		'**+Validation of the FIELD driver fault
		'+Validacion del CAMPO culpabilidad del conductor
		If nInfraction <= 0 Then
			'Call lclsErrors.ErrorMessage(sCodispl, 4128)
			lstrError = lstrError & lstrSep & "4128"
		End If
		
		'**+Validates the field number of vehicle involved
		'+ Se valida el campo de numeros de vehiculos involucrados
		If nAuto_quant <= 0 Then
			'Call lclsErrors.ErrorMessage(sCodispl, 4125)
			lstrError = lstrError & lstrSep & "4125"
		End If
		
		If Trim(sDriver_claim) = String.Empty Then
			'Call lclsErrors.ErrorMessage(sCodispl, 2001)
            lstrError = lstrError & lstrSep & "782001"
		Else
			lobjClient = New eClient.ValClient
			If Not lclsClient.Find(sDriver_claim) Then
                '+ Se valida que el apellido paterno tenga información siempre y cuando el rut no
				'+ esté registrado en la base de datos.
				If Trim(sLastName) = String.Empty Then
					'Call lclsErrors.ErrorMessage(sCodispl, 2807)
					lstrError = lstrError & lstrSep & "2807"
				End If
                '+ Se valida que los nombres tengan información siempre y cuando el rut no
				'+ esté registrado en la base de datos.
				If Trim(sCliename) = String.Empty Then
					'Call lclsErrors.ErrorMessage(sCodispl, 2004)
					lstrError = lstrError & lstrSep & "2004"
                End If
            Else
                If Not lclsClient.bNatural Then
                    lstrError = lstrError & lstrSep & "55974"
                End If

            End If
		End If
		
		If Trim(sWitness) <> String.Empty Then
			lobjClient = New eClient.ValClient
			If Not lclsClient.Find(sWitness) Then
				'+ Se valida que el apellido paterno tenga información siempre y cuando el ruc no
				'+ esté registrado en la base de datos.
				If Trim(sLastNameWitness) = String.Empty Then
					'Call lclsErrors.ErrorMessage(sCodispl, 2807)
					lstrError = lstrError & lstrSep & "2807"
				End If
				'+ Se valida que los nombres tengan información siempre y cuando el ruc no
				'+ esté registrado en la base de datos.
				If Trim(sClienameWitness) = String.Empty Then
					'Call lclsErrors.ErrorMessage(sCodispl, 2004)
					lstrError = lstrError & lstrSep & "2004"
				End If
			End If
		End If
		
		
		If nWorksh > 0 Then
			'**+Validate that exist a selected provider.
			'+ Se valida que exista el Proveedor seleccionado.
			lobjObject = New Tab_Provider
			If Not lobjObject.ValTab_provider(Tab_Provider.eProvider.clngWorksh, nWorksh) Then
				'Call lclsErrors.ErrorMessage(sCodispl, 4119)
				lstrError = lstrError & lstrSep & "4119"
			End If
			
			If Not lobjObject.ValProviderCase(nClaim, nCase_num, nDeman_type, Claim_case.eClaimRole.clngClaimRWorkShop, nWorksh, Tab_Provider.eProvider.clngWorksh) Then
				'Call lclsErrors.ErrorMessage(sCodispl, 4336)
				lstrError = lstrError & lstrSep & "4336"
			End If
		End If
		
		'insValSI018 = lclsErrors.Confirm
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
			insValSI018 = lclsErrors.Confirm
		End If
		
		lobjObject = Nothing
		lclsErrors = Nothing
		lobjClient = Nothing
		lclsClient = Nothing
		
insValSI018_err: 
		If Err.Number Then
			insValSI018 = "insValSI018: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	Public Function insPostSI018(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nAuto_quant As Integer, ByVal sBlame As String, ByVal sDriver_cod As String, ByVal sInfraction As String, ByVal sSummary As String, ByVal sPoliceDem As String, ByVal sInd_EIR As String, ByVal nWorksh As Integer, ByVal nUsercode As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sDriver_claim As String, ByVal sDriver_Digit As String, ByVal sCliename As String, ByVal sLastName As String, ByVal sLastName2 As String, ByVal nFine As Double, ByVal sCourt As String, ByVal dDemand_date As Date, ByVal sPolStat_deman As String, ByVal nPage As Integer, ByVal nParagraph As Integer, ByVal sPol_Station As String, ByVal nPoliceDoc As Double, ByVal dPoldoc_date As Date, ByVal sAlcoholic As String, ByVal nNotenum As Double, ByVal sLicense As String, ByVal dDriverDate As Date, ByVal sWitness As String, ByVal sWitness_Digit As String, ByVal sLastNameWitness As String, ByVal sLastName2Witness As String, ByVal sClienameWitness As String, Optional ByVal dBirthDat As Date = #12:00:00 AM#) As Boolean
		
		Dim lrecinsPostSI018 As eRemoteDB.Execute
		
		lrecinsPostSI018 = New eRemoteDB.Execute
		
		With lrecinsPostSI018
			.StoredProcedure = "INSSI018PKG.INSPOSTSI018"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAuto_quant", nAuto_quant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Trim(sBlame) = String.Empty Or Trim(sBlame) = "0" Then
                .Parameters.Add("nBlame", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nBlame", CShort(sBlame), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
			.Parameters.Add("sDriver_cod", sDriver_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Trim(sInfraction) = String.Empty Or Trim(sInfraction) = "0" Then
                .Parameters.Add("nInfraction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nInfraction", CInt(sInfraction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
			.Parameters.Add("sSummary", sSummary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPoliceDem", sPoliceDem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Eir", sInd_EIR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDriver_claim", sDriver_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFine", nFine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCourt", sCourt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDemand_date", dDemand_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolStat_deman", sPolStat_deman, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPage", nPage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParagraph", nParagraph, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPol_Station", sPol_Station, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPoliceDoc", nPoliceDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPoldoc_date", dPoldoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAlcoholic", sAlcoholic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense", sLicense, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDriverDate", dDriverDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWitness", sWitness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastNameWitness", sLastNameWitness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName2Witness", sLastName2Witness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClienameWitness", sClienameWitness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBirthdat", dBirthDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostSI018 = (.Parameters("nStatus").Value = 1)
			Else
				insPostSI018 = False
            End If

		End With
	End Function
	
	'insPostSI018_Old: Realiza las actualizaciones correspondientes sobre la tabla claim_auto
	Public Function insPostSI018_Old(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nAuto_quant As Integer, ByVal sBlame As String, ByVal sDriver_cod As String, ByVal sInfraction As String, ByVal sSummary As String, ByVal sPoliceDem As String, ByVal sInd_EIR As String, ByVal nWorksh As Integer, ByVal nUsercode As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sDriver_claim As String, ByVal sDriver_Digit As String, ByVal sCliename As String, ByVal sLastName As String, ByVal sLastName2 As String, ByVal nFine As Double, ByVal sCourt As String, ByVal dDemand_date As Date, ByVal sPolStat_deman As String, ByVal nPage As Integer, ByVal nParagraph As Integer, ByVal sPol_Station As String, ByVal nPoliceDoc As Double, ByVal dPoldoc_date As Date, ByVal sAlcoholic As String, ByVal nNotenum As Double, ByVal sLicense As String, ByVal dDriverDate As Date, ByVal sWitness As String, ByVal sWitness_Digit As String, ByVal sLastNameWitness As String, ByVal sLastName2Witness As String, ByVal sClienameWitness As String, Optional ByVal dBirthDat As Date = #12:00:00 AM#) As Boolean
		Dim lclsClaim_auto As eClaim.Claim_auto
		Dim lclsCases_win As eClaim.Cases_win
		Dim lclsClient As eClient.Client
		Dim lclsClaim As eClaim.Claim
		
		On Error GoTo insPostSI018_Old_Err
		
		lclsClaim_auto = New Claim_auto
		lclsClient = New eClient.Client
		lclsClaim = New eClaim.Claim
		
		'+ Incluye un nuevo registro en la tabla clientes en caso de no existir
		With lclsClient
			If Not lclsClient.Find(sDriver_claim) Then
				.nUsercode = nUsercode
				.nPerson_typ = IIf(.nPerson_typ <> CDbl("2"), "1", .nPerson_typ)
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
				.sClient = sDriver_claim
				.sDigit = lclsClaim.CalcDigit(sDriver_claim)
				.sFirstName = sCliename
				.sLastName = sLastName
				.sLastName2 = sLastName2
				.sCliename = sLastName & " " & sLastName2 & ", " & sCliename
				.dBirthDat = dBirthDat
				.AddClient()
			End If
		End With
		
		If sLicense <> "" Then
			Me.sLicense = sLicense
			Me.dDriverDat = dDriverDate
			Me.sDriver_claim = sDriver_claim
			Call UpdateDriver()
		End If
		
		If sWitness <> String.Empty Then
			'+Se crea el registro correspondiente en Client cuando se incluya informacion en los datos del testigo siempre y cuando no exista
			With lclsClient
				If Not lclsClient.Find(sWitness) Then
					.nUsercode = nUsercode
					.nPerson_typ = IIf(.nPerson_typ <> CDbl("2"), "1", .nPerson_typ)
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
					.sClient = sWitness
					.sDigit = lclsClaim.CalcDigit(sWitness)
					.sFirstName = sClienameWitness
					.sLastName = sLastNameWitness
					.sLastName2 = sLastName2Witness
					.sCliename = sLastNameWitness & " " & sLastName2Witness & "," & sClienameWitness
					.AddClient()
				End If
			End With
		End If
		
		'+ Incluye un nuevo registro en claim_auto
		With lclsClaim_auto
			.nClaim = nClaim
			.nAuto_quant = nAuto_quant
			.sBlame = sBlame
			.sDriver_cod = sDriver_cod
			.sInfraction = sInfraction
			.sSummary = IIf(sSummary = "", "2", sSummary)
			.sPoliceDem = IIf(sPoliceDem = "", "2", sPoliceDem)
			.sInd_EIR = IIf(sInd_EIR = "", "2", sInd_EIR)
			.nLocation = eRemoteDB.Constants.intNull
			.nWorksh = nWorksh
			.nUsercode = nUsercode
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.sDriver_claim = sDriver_claim
			.nFine = nFine
			.sCourt = sCourt
			.dDemand_date = dDemand_date
			.sPolStat_deman = sPolStat_deman
			.nPage = nPage
			.nParagraph = nParagraph
			.sPol_Station = sPol_Station
			.nPoliceDoc = nPoliceDoc
			.dPoldoc_date = dPoldoc_date
			.sAlcoholic = IIf(sAlcoholic = "", "2", sAlcoholic)
			.nNotenum = nNotenum
			.sWitness = sWitness
			
			lclsCases_win = New Cases_win
			If .Update Then
				'+ Actualiza el estado de la ventana
				insPostSI018_Old = lclsCases_win.Add_Cases_win(nClaim, nCase_num, nDeman_type, sCodispl, "2", nUsercode)
			Else
				insPostSI018_Old = False
			End If
			lclsCases_win = Nothing
		End With
		
		lclsClaim_auto = Nothing
		lclsClaim = Nothing
		
		
insPostSI018_Old_Err: 
		If Err.Number Then
			insPostSI018_Old = False
		End If
		On Error GoTo 0
	End Function
	Public Function UpdateDriver() As Boolean
		Dim lrecClient As eRemoteDB.Execute
		
		On Error GoTo UpdateDriver_err
		
		lrecClient = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insClaim_attm'
		'+ Información leída el 14/07/2001 05:59:55 p.m.
		
		With lrecClient
			
			.StoredProcedure = "UpdDriver"
			.Parameters.Add("sClient", sDriver_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDriverDat", dDriverDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense", sLicense, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateDriver = .Run(False)
		End With
		
UpdateDriver_err: 
		If Err.Number Then
			UpdateDriver = False
		End If
		On Error GoTo 0
		lrecClient = Nothing
	End Function
	
	'**% UpdateNoteNum: Actualize the note number in the table Claim_Auto
	'%   UpdateNoteNum: Actualiza el número en la tabla Claim_Auto
	Public Function UpdateNoteNum(ByVal nClaim As Double, ByVal nDeman_type As Integer, ByVal nCase_num As Integer, ByVal nNoteDama As Integer) As Boolean
		
		Dim lrecupdClaimAutoNote As eRemoteDB.Execute
		
		On Error GoTo UpdateNoteNum_Err
		lrecupdClaimAutoNote = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.updClaim_AutoNote'
		'Definición de parámetros para stored procedure 'insudb.updClaim_AutoNote'
		'**Data read on 01/23/2001 2:41:19 PM
		'Información leída el 23/01/2001 2:41:19 PM
		With lrecupdClaimAutoNote
			.StoredProcedure = "updClaim_AutoNote"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteDama", nNoteDama, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateNoteNum = .Run(False)
			
		End With
		
		lrecupdClaimAutoNote = Nothing
		
UpdateNoteNum_Err: 
		If Err.Number Then
			UpdateNoteNum = False
		End If
		On Error GoTo 0
    End Function
  '**%Find2: Obtain the values of the properties of the class
    '%Find2 : Obtiene los valores de las propiedades de la clase
    Public Function Find2(ByVal nClaim As Double, Optional ByVal lblnFind2 As Boolean = False) As Boolean
        Dim lrecClaim_auto As eRemoteDB.Execute
        Static llngOldClaim As Double
        Static lintOldCase_num As Integer
        Static lintOldDeman_type As Integer
        Static lblnRead As Boolean

        On Error GoTo Find_Err

        If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lblnFind2 Then

            llngOldClaim = nClaim
            lintOldCase_num = nCase_num
            lintOldDeman_type = nDeman_type

            '+ Realiza la lectura de los datos asociados al siniestro
            lrecClaim_auto = New eRemoteDB.Execute
            With lrecClaim_auto
                .StoredProcedure = "reaClaim_Auto2"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If (.Run) Then
                    lblnRead = True
                    sBlame = IIf(.FieldToClass("nBlame") = eRemoteDB.Constants.intNull, String.Empty, CStr(.FieldToClass("nBlame")))
                    sInfraction = IIf(.FieldToClass("nInfraction") = eRemoteDB.Constants.intNull, String.Empty, CStr(.FieldToClass("nInfraction")))
                    nAuto_quant = .FieldToClass("nAuto_quant")
                    sPoliceDem = .FieldToClass("sPoliceDem")
                    sInd_EIR = .FieldToClass("sInd_EIR")
                    nLocation = .FieldToClass("nLocation")
                    sSummary = .FieldToClass("sSummary")
                    'sLicense = .FieldToClass("sLicense")
                    'dDriverDat = .FieldToClass("dDriverdat")
                    nWorksh = .FieldToClass("nProvider")
                    'sDesWorksh = .FieldToClass("sDesWorksh")
                    nUsercode = .FieldToClass("nUsercode")
                    sDriver_claim = .FieldToClass("sDriver_claim")
                    nFine = .FieldToClass("nFine")
                    sCourt = .FieldToClass("sCourt")
                    dDemand_date = .FieldToClass("dDemand_date")
                    sPolStat_deman = .FieldToClass("sPolStat_deman")
                    nPage = .FieldToClass("nPage")
                    nParagraph = .FieldToClass("nParagraph")
                    sPol_Station = .FieldToClass("sPol_Station")
                    nPoliceDoc = .FieldToClass("nPoliceDoc")
                    dPoldoc_date = .FieldToClass("dPoldoc_date")
                    sAlcoholic = .FieldToClass("sAlcoholic")
                    nNotenum = .FieldToClass("nNotenum")
                    sWitness = .FieldToClass("sWitness")
                    nCase_num = .FieldToClass("NCASE_NUM")
                    nFine = .FieldToClass("NFINE")
                    dDoccurdat = .FieldToClass("DOCCURDAT")
                    dDecladat = .FieldToClass("DDECLADAT")


                Else
                    lblnRead = False
                End If

            End With
            lrecClaim_auto = Nothing
        End If
        Find2 = lblnRead

Find_Err:
        If Err.Number Then
            lblnRead = False
            Find2 = False
        End If
        On Error GoTo 0
    End Function

    '%   insGarage_Assigned: Se lleva el registro de asignaciones de talleres.
    Private Function insGarage_Assigned(ByVal nAssigncode As Long, _
                                        ByVal nClaim As Integer, _
                                        ByVal nCase_num As Integer, _
                                        ByVal nDeman_type As Integer, _
                                        ByVal nGarageagencode As Long, _
                                        ByVal sGarageagenname As String, _
                                        ByVal sAdvisername As String, _
                                        ByVal sGarageaddress As String, _
                                        ByVal sGaragephone As String, _
                                        ByVal sGarageemail As String, _
                                        ByVal sPrefer_applied As String, _
                                        ByVal sResultind As String, _
                                        ByVal sResultmessage As String, _
                                        ByVal nUsercode As Integer, _
                                        ByVal sIdTipoDireccionador As String) As Boolean


    End Function
End Class