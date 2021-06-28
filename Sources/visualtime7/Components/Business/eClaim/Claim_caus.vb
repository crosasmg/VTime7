Option Strict Off
Option Explicit On
Public Class Claim_caus
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_caus.cls                           $%'
	'% $Author:: Nmoreno                                    $%'
	'% $Date:: 7/07/10 4:40p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties of the correspondent class to the claim_caus table (01/16/2001)
	'**-Column_name
	'-Se definen las propiedades principales de la clase correspondientes a la tabla claim_caus (16/01/2001)
	'Column_name                                Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------------------ -------------------------------------------------------------------------------------------------------------------------------- ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nBranch As Integer 'smallint  no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint  no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nCausecod As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sClaimTyp As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sDescript As String 'char                                                                                                                             no                                  30                      yes                                 no                                  yes
	Public sShort_des As String 'char                                                                                                                             no                                  12                      yes                                 no                                  yes
	Public sStatregt As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nUsercode As Integer
	Public sPartial_loss As String
	Public sTotal_loss As String
	
	'**%Function Find: Find  the claim lost type, considering the value into the claim
	'**%causes table (claim_cause)
	'%Function Find: Busca el tipo de pérdida de siniestro,tomando en cuenta
	'%el valor contenido en el tabla de causas de siniestros (claim_cause)
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCausecod As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClaim_caus As eRemoteDB.Execute
		Static lblnRead As Boolean
		Static lintOldBranch As Integer
		Static lintOldProduct As Integer
		Static lintOldCauseCod As Integer
		
		On Error GoTo Find_Err
		
		If lintOldBranch <> nBranch Or lintOldProduct <> nProduct Or lintOldCauseCod <> nCausecod Or lblnFind Then
			
			lintOldBranch = nBranch
			lintOldProduct = nProduct
			lintOldCauseCod = nCausecod
			
			lrecreaClaim_caus = New eRemoteDB.Execute
			
			With lrecreaClaim_caus
				.StoredProcedure = "reaClaim_caus_o" 'Listo
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCauseCod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then 'Listo
					sClaimTyp = .FieldToClass("sClaimtyp")
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			
			lrecreaClaim_caus = Nothing
			
		End If
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Add: This function add new register to the table CLAIM_CLAUS
	'%Add: Esta función agrega registros a la tabla CLAIM_CAUS
	Public Function Add() As Boolean
		Dim lreccreClaim_caus As eRemoteDB.Execute
		
		lreccreClaim_caus = New eRemoteDB.Execute
		'**Parameters definition for stored procedure 'insudb.creClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.creClaim_caus'
		'**Infoemation read on October 04 of 2001 06:23:31 p.m.
		'Información leída el 04/10/2001 06:23:31 p.m.
		
		With lreccreClaim_caus
			.StoredProcedure = "creClaim_caus"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaimtyp", sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
			
		End With
		lreccreClaim_caus = Nothing
		
	End Function
	'**%Update: This function update data of the table CLAIM_CLAUS
	'%Update: Esta función actualiza registros en la tabla CLAIM_CAUS
	Public Function Update() As Boolean
		Dim lrecupdClaim_caus As eRemoteDB.Execute
		
		lrecupdClaim_caus = New eRemoteDB.Execute
		
		'**Parameters definition for stored procedure 'insudb.updClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.updClaim_caus'
		'**Infoemation read on October 04 of 2001 06:48:22 p.m.
		'Información leída el 04/10/2001 06:48:22 p.m.
		
		With lrecupdClaim_caus
			.StoredProcedure = "updClaim_caus"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaimtyp", sClaimTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		lrecupdClaim_caus = Nothing
		
	End Function
	
	'**%Delete: This function remove registers of the table CLAIM_CLAUS
	'%Delete: Esta función elimina registros de la tabla CLAIM_CAUS
	Public Function Delete() As Boolean
		Dim lrecdelClaim_caus As eRemoteDB.Execute
		
		lrecdelClaim_caus = New eRemoteDB.Execute
		
		'**Parameters definition for stored procedure 'insudb.delClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.delClaim_caus'
		'**Infoemation read on October 04 of 2001 06:52:26 p.m.
		'Información leída el 04/10/2001 06:52:26 p.m.
		
		With lrecdelClaim_caus
			.StoredProcedure = "delClaim_caus"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCausecod", nCausecod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		lrecdelClaim_caus = Nothing
		
	End Function
	'**%valExistClaim_caus: This function validate if there are Claim Causes related to a specific branch
	'%valExistClaim_caus: Valida la existencia de causas asociadas a un rammo el cual es pasado como parámetro.
	Public Function valExistClaim_caus(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecClaim_caus As eRemoteDB.Execute
		
		lrecClaim_caus = New eRemoteDB.Execute
		
		valExistClaim_caus = False
		
		On Error GoTo valExistClaim_caus_Err
		
		With lrecClaim_caus
			.StoredProcedure = "valClaim_caus_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("lCount") > 0 Then
					valExistClaim_caus = True
				End If
			End If
		End With
		lrecClaim_caus = Nothing
		
valExistClaim_caus_Err: 
		If Err.Number Then
			valExistClaim_caus = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValMSI010_K: This function perform validation over the fields of the header
	'%insValMSI010_K: Esta función se encarga de validar los datos introducidos en la cabecera de
	'%la forma.
	Public Function insValMSI010_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMSI010_K_Err
		
		
		If nProduct = eRemoteDB.Constants.intNull Then nProduct = 0
		
		'**+Validation of the field: Insurance branch
		'+Validación del campo: Ramo comercial.
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'**+If the action is Duplicate Table, CLAIM_CAUS for this branch should be empty
		'+ Si la acción es duplicar no debe existir información registrada en la tabla CLAIM_CAUS.
		If nAction = 306 Then
			If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
				If valExistClaim_caus(nBranch, nProduct) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10049)
				End If
			End If
		End If
		
		'**+If the action performed is inquiry, it validated if there is information related to this branch
		'+ Si la acción es consulta se verifica que exista información (causas asociadas al ramo a consultar).
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			If Not valExistClaim_caus(nBranch, nProduct) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10879)
			End If
		End If
		
		If nAction = 302 Or nAction = 306 Then
			If Exist_causes(nBranch, nProduct) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60513)
			End If
		End If
		
		insValMSI010_K = lclsErrors.Confirm
		
		
insValMSI010_K_Err: 
		If Err.Number Then
			insValMSI010_K = lclsErrors.Confirm & Err.Description
		End If
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValMSI010: This function perform validations over the fields of the folder
	'%insValMSI010: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function insValMSI010(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCausecod As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sPart_loss As String = "", Optional ByVal sTotal_loss As String = "", Optional ByVal sStatregt As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim_caus As eClaim.Claim_caus
		Dim lclsValues As New eFunctions.Values
		
		lclsErrors = New eFunctions.Errors
		lclsClaim_caus = New eClaim.Claim_caus
		
		On Error GoTo insValMSI010_Err
		
		'**+Validations related to column: Cause
		'+ Se valida la columna: Causa.
		If nCausecod = 0 Or nCausecod = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10872)
		Else
			If sAction = "Add" And lclsClaim_caus.Find(nBranch, nProduct, nCausecod) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10004)
			End If
		End If
		
		'**+Validations related to delete action: The Claim Cause should not be related to a Claim
		'+ Se valida la acción eliminar: La causa no debe estar asociada a un siniestro.
		If sAction = "Del" Then
			If valClaim_nCausecod(nCausecod, nBranch, nProduct) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10874)
				
				insValMSI010 = lclsErrors.Confirm
				
				lclsErrors = Nothing
				lclsClaim_caus = Nothing
				Exit Function
			End If
		Else
			'**+Validations related to column: Description
			'+ Se valida la columna: Descripción larga.
			If nCausecod <> 0 And nCausecod <> eRemoteDB.Constants.intNull Then
				If sDescript = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10005)
				End If
			End If
			
			'**+Validations related to column: Short Description
			'+ Se valida la columna: Descripción corta.
			If nCausecod <> 0 And nCausecod <> eRemoteDB.Constants.intNull Then
				If sShort_des = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10006)
				End If
			End If
			
			'**+Validations related to column: Partial and Total Loss
			'+ Se valida la columna: Perdida Parcial y Total.
			If nCausecod <> 0 And nCausecod <> eRemoteDB.Constants.intNull Then
				If sPart_loss = String.Empty And sTotal_loss = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10871)
				End If
			End If
		End If
		
		If sAction <> "Del" Then
			If sStatregt = String.Empty Or sStatregt = "0" Or lclsValues.StringToType(sStatregt, eFunctions.Values.eTypeData.etdInteger) <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 9089)
			End If
		End If
		
		insValMSI010 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		lclsClaim_caus = Nothing
		
insValMSI010_Err: 
		If Err.Number Then
			insValMSI010 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'*InsPostMSI010: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Claim_caus
	Public Function insPostMSI010(ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCausecod As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "", Optional ByVal sPart_loss As String = "", Optional ByVal sTotal_loss As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMSI010_err
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nCausecod = nCausecod
		Me.sDescript = sDescript
		Me.sShort_des = sShort_des
		Me.sStatregt = sStatregt
		Me.nUsercode = nUsercode
		
		If sPart_loss = "1" Then
			If sTotal_loss = "1" Then
				Me.sClaimTyp = "3"
			Else
				Me.sClaimTyp = "1"
			End If
		Else
			Me.sClaimTyp = "2"
		End If
		
		insPostMSI010 = True
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMSI010 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMSI010 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMSI010 = Delete()
				
		End Select
		
insPostMSI010_err: 
		If Err.Number Then
			insPostMSI010 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%valClaim_nCausecod: This function validates if a Claim Cause is already related to a Claim.
	'%valClaim_nCausecod: Permite validar si una causa de siniestro ya está registrada.
	Private Function valClaim_nCausecod(ByVal nCause_cod As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As Boolean
		Dim lrecClaim_nCausecod As eRemoteDB.Execute
		
		lrecClaim_nCausecod = New eRemoteDB.Execute
		On Error GoTo valClaim_nCausecod_Err
		
		valClaim_nCausecod = False
		
		With lrecClaim_nCausecod
			.StoredProcedure = "valClaim_nCausecod_a"
			.Parameters.Add("nCausecod", nCause_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If lrecClaim_nCausecod.FieldToClass("lCount") > 0 Then
					valClaim_nCausecod = True
				End If
				.RCloseRec()
			End If
		End With
		
		lrecClaim_nCausecod = Nothing
		
valClaim_nCausecod_Err: 
		If Err.Number Then
			valClaim_nCausecod = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Duplicar Rutina que actualiza el ramo destino con los datos proveniente del ramo origen.
	Public Function insDuplicarMSI010(ByVal nLastBranch As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLastProduct As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTab_damage As eRemoteDB.Execute
		
		lrecTab_damage = New eRemoteDB.Execute
		
		On Error GoTo insDuplicarMSI010_Err
		
		insDuplicarMSI010 = False
		
		If nProduct = eRemoteDB.Constants.intNull Then nProduct = 0
		If nLastProduct = eRemoteDB.Constants.intNull Then nLastProduct = 0
		
		'+ Duplica el registro correspondiente en claim_caus
		With lrecTab_damage
			.StoredProcedure = "insDuplicateMSI010"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastBranch", nLastBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastProduct", nLastProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insDuplicarMSI010 = .Run(False)
		End With
		
		lrecTab_damage = Nothing
		
insDuplicarMSI010_Err: 
		If Err.Number Then
			insDuplicarMSI010 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Exist_causes: Retorna la cantidad de causas registradas para el ramo en tratamiento - ACM - 22/08/2002
	Private Function Exist_causes(ByVal nBranch As Integer, ByVal nProduct As Integer) As Integer
		Dim lrecClaim_Caus_Exist As New eRemoteDB.Execute
		Dim llngExist As Integer
		
		On Error GoTo Exist_causes_err
		
		llngExist = 0
		
		'+ Duplica el registro correspondiente en claim_caus
		With lrecClaim_Caus_Exist
			.StoredProcedure = "Claim_Caus_Exist"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", llngExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				llngExist = .Parameters("nExist").Value
			Else
				llngExist = 0
			End If
		End With
		
		Exist_causes = llngExist
		
Exist_causes_err: 
		If Err.Number Then
			Exist_causes = 0
		End If
		lrecClaim_Caus_Exist = Nothing
		On Error GoTo 0
	End Function
End Class






