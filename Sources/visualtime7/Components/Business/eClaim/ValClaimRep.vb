Option Strict Off
Option Explicit On
Public Class ValClaimRep
	'%-------------------------------------------------------%'
	'% $Workfile:: ValClaimRep.cls                          $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 31-03-13 18:55                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	'**+This variable indicated the Business Draft in the SIL009 transaction.
	'+Esta variable es indica el Giro del Negocio en la transacción SIL009
	
	Public nIndBDraft As Integer
	Public sKey As String
    Public P_SKEY As String

    '+ Variables a ser utilizadas en la SIL005
    Public nSettlecode As Integer
    Public sFormatname As String
    Public nClaim As Integer
    Public nOrder As Integer


    '- Arreglo para la carga de data en la SIL005
    Public Structure strucSIL005
        Dim nSettlecode As Integer
        Dim sFormatname As String
        Dim nClaim As Integer
        Dim nOrder As Integer
    End Structure

    Public marrSIL005() As strucSIL005
	
	
	'%insValSIL001_k:
	Public Function insValSIL001_k(ByVal sCodispl As String, ByVal dInitialDate As Date, ByVal dFinalDate As Date, Optional ByVal nMode As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCurrency As Integer = 0) As String
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValSIL001_k_Err
		
		If dInitialDate <> eRemoteDB.Constants.dtmNull Then
			If dInitialDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 1966)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		If dFinalDate <> eRemoteDB.Constants.dtmNull Then
			If dFinalDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 1966)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		End If
		
		If dFinalDate <> eRemoteDB.Constants.dtmNull And dInitialDate <> eRemoteDB.Constants.dtmNull Then
			If dInitialDate > dFinalDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
		End If
		
		insValSIL001_k = lclsErrors.Confirm
		
insValSIL001_k_Err: 
		If Err.Number Then
			insValSIL001_k = "insValSIL001_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	'%insValSIL003_k:
	Public Function insValSIL003_k(ByVal sCodispl As String, ByVal dInitialDate As Date, ByVal dFinalDate As Date, ByVal nProfessional As Integer) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsProvider As eClaim.Tab_Provider = New eClaim.Tab_Provider
		
		On Error GoTo insValSIL003_k_err
		
		If dInitialDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		If dFinalDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		Else
			If dFinalDate <= dInitialDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 2795)
			End If
		End If
		
		insValSIL003_k = lclsErrors.Confirm
		
insValSIL003_k_err: 
		If Err.Number Then
			insValSIL003_k = "insValSIL003_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		lclsProvider = Nothing
		
		On Error GoTo 0
		
	End Function
	'insValSIL004_k:
	Public Function insValSIL004_k(ByVal sCodispl As String, ByVal dInitialDate As Date, ByVal dFinalDate As Date) As String
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValSIL004_k_err
		
		If dFinalDate <> eRemoteDB.Constants.dtmNull Then
			If dFinalDate <= dInitialDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
			
			If dFinalDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 4341)
			End If
		End If
		
		insValSIL004_k = lclsErrors.Confirm
		
insValSIL004_k_err: 
		If Err.Number Then
			insValSIL004_k = "insValSIL004_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		
		On Error GoTo 0
		
	End Function
	'%insValSIL005_k:
	Public Function insValSIL005_k(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nClaimNumber As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nFinishContract As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As New ePolicy.Policy
		Dim lclsCertificat As New ePolicy.Certificat
		Dim lclsClaim As eClaim.Claim = New eClaim.Claim
		Dim lclsSettlement As eClaim.Settlement = New eClaim.Settlement
		
		On Error GoTo insValSIL005_k_err
		
		lclsErrors = New eFunctions.Errors
		
		If nBranch = eRemoteDB.Constants.intNull Then nBranch = 0
		If nProduct = eRemoteDB.Constants.intNull Then nProduct = 0
		If nPolicy = eRemoteDB.Constants.intNull Then nPolicy = 0
		If nCertif = eRemoteDB.Constants.intNull Then nCertif = 0
		If nClaimNumber = eRemoteDB.Constants.intNull Then nClaimNumber = 0
		If nFinishContract = eRemoteDB.Constants.intNull Then nFinishContract = 0
		
		If nProduct > 0 Then
			If nBranch = 0 Then
				lclsErrors.ErrorMessage(sCodispl, 1022)
			End If
		End If
		
		If nPolicy > 0 Then
			If nProduct <= 0 Then
				lclsErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
				lclsErrors.ErrorMessage(sCodispl, 3001)
			Else
				If lclsPolicy.sStatus_pol = "2" Or lclsPolicy.sStatus_pol = "3" Then
					lclsErrors.ErrorMessage(sCodispl, 3720)
				End If
			End If
		End If
		
		If nCertif > 0 Then
			If nPolicy <= 0 Then
				lclsErrors.ErrorMessage(sCodispl, 3003)
			End If
			
			If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
				lclsErrors.ErrorMessage(sCodispl, 3010)
			Else
				If lclsCertificat.sStatusva = "2" Or lclsCertificat.sStatusva = "3" Then
					lclsErrors.ErrorMessage(sCodispl, 3883)
				End If
			End If
		End If
		
		If nClaimNumber > 0 Then
			If Not lclsClaim.Find(nClaimNumber) Then
				lclsErrors.ErrorMessage(sCodispl, 4005)
			Else
				If lclsClaim.sStaClaim = Claim.Estatclaim.eNull Or lclsClaim.sStaClaim = Claim.Estatclaim.eRefuse Or lclsClaim.sStaClaim = Claim.Estatclaim.eImcomplete Then
					lclsErrors.ErrorMessage(sCodispl, 55759)
				End If
			End If
		End If
		
		If nFinishContract > 0 Then
			lclsSettlement = New eClaim.Settlement
			If lclsSettlement.Find_Settlement(nClaimNumber, nCase_num, nDeman_type, nFinishContract) Then
				' Si este campo está lleno y el campo caso está lleno, el número de finiquito debe existir para ese caso.
				' Si este campo está lleno, el finiquito no debe estar impreso   04330
				If nCase_num <> 0 And CDbl(lclsSettlement.sStatus_Fin) = 2 Then
					lclsErrors.ErrorMessage(sCodispl, 4330)
				End If
				'Finiquito Si este campo está lleno, debe existir en el archivo de finiquitos   04293
			Else
				lclsErrors.ErrorMessage(sCodispl, 4293)
			End If
			lclsSettlement = Nothing
			
		Else
			
			lclsSettlement = New eClaim.Settlement

            ''Se valida que al menos exista un finiquito pendiente de impresion
            'If Not lclsSettlement.ValExistSettlement(nClaimNumber, nCase_num, nDeman_type) Then
            '	lclsErrors.ErrorMessage(sCodispl, 4242)
            'End If

            'Se valida que al menos exista un finiquito pendiente de impresion
            If Not lclsSettlement.ValExist_CL_Settlement(nClaimNumber, nCase_num, nDeman_type) Then
                lclsErrors.ErrorMessage(sCodispl, 4242)
            End If


            lclsSettlement = Nothing
			
		End If
		
		insValSIL005_k = lclsErrors.Confirm
		
insValSIL005_k_err: 
		If Err.Number Then
			insValSIL005_k = "insValSIL005_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		lclsPolicy = Nothing
		lclsCertificat = Nothing
		lclsClaim = Nothing
		
		On Error GoTo 0
		
	End Function
	'%insValSIL006_k: Validaciones de la transacción SIL006
	Public Function insValSIL006_k(ByVal sCodispl As String, ByVal nClaimNumber As Integer) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsClaim As eClaim.Claim = New eClaim.Claim
		
		On Error GoTo insValSIL006_k_Err
		
		If nClaimNumber <> 0 And nClaimNumber <> eRemoteDB.Constants.intNull Then
			If Not lclsClaim.Find(nClaimNumber) Then
				Call lclsErrors.ErrorMessage(sCodispl, 4005)
			Else
				If lclsClaim.sStaClaim = Claim.Estatclaim.eImcomplete Then
					Call lclsErrors.ErrorMessage(sCodispl, 4051)
				End If
				
				If lclsClaim.sStaClaim = Claim.Estatclaim.eNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 4099)
				End If
			End If
		End If
		
		insValSIL006_k = lclsErrors.Confirm
		
insValSIL006_k_Err: 
		If Err.Number Then
			insValSIL006_k = "insValSIL006_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		lclsClaim = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'%insValSIL010_k:
	Public Function insValSIL010_k(ByVal sCodispl As String, ByVal dInitialDate As Date, ByVal dFinalDate As Date, Optional ByVal nMode As Integer = 0) As String
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValSIL001_k_Err
		
		If dInitialDate <> eRemoteDB.Constants.dtmNull Then
			If dInitialDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 1966)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		If dFinalDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		End If
		
		If dFinalDate <> eRemoteDB.Constants.dtmNull And dInitialDate <> eRemoteDB.Constants.dtmNull Then
			If dInitialDate > dFinalDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
		End If
		
		If nMode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 21086)
		End If
		
		insValSIL010_k = lclsErrors.Confirm
		
insValSIL001_k_Err: 
		If Err.Number Then
			insValSIL010_k = "insValSIL010_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	
	
	'%FindNumDenun: Validaciones de la transacción CAC1024
	Public Function FindNumDenun(ByVal nClaim As Double) As String
		
		Dim lrecFindNumDenun As eRemoteDB.Execute
		lrecFindNumDenun = New eRemoteDB.Execute
		
		On Error GoTo FindNumDenun_err
		
		FindNumDenun = CStr(False)
		
		With lrecFindNumDenun
			.StoredProcedure = "FindNumDenun"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindNumDenun = CStr(True)
				.RCloseRec()
			End If
		End With
		
		lrecFindNumDenun = Nothing
		
FindNumDenun_err: 
		If Err.Number Then
			FindNumDenun = CStr(False)
		End If
		
		On Error GoTo 0
	End Function
	
	
	'%FindCodCober: Validaciones de la transacción CAC1024
	Public Function FindCodCober(ByVal nCover As Double) As String
		Dim lrecFindCodCober As eRemoteDB.Execute
		lrecFindCodCober = New eRemoteDB.Execute
		
		On Error GoTo FindCodCober_err
		
		FindCodCober = CStr(False)
		
		With lrecFindCodCober
			.StoredProcedure = "FindCodCober"
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindCodCober = CStr(True)
				.RCloseRec()
			End If
		End With
		
		lrecFindCodCober = Nothing
		
FindCodCober_err: 
		If Err.Number Then
			FindCodCober = CStr(False)
		End If
		
		On Error GoTo 0
	End Function
	
	
	
	
	
	'insPostSIL006_k: Se invoca el reporte correspondiente a la transacción SIL006
	Public Function insPostSIL006_k(ByVal nClaimNumber As Integer, ByVal nUserCode As Integer) As Boolean
		
		Dim lrecinsReaRSIL006 As New eRemoteDB.Execute
		
		On Error GoTo insPostSIL006_k_Err
		
		
		'+ Definición de parámetros para stored procedure 'insudb.insReaRSIL006'
		With lrecinsReaRSIL006
			.StoredProcedure = "insReaRSIL006"
			.Parameters.Add("nClaim", nClaimNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostSIL006_k = .Run
		End With
		
insPostSIL006_k_Err: 
		If Err.Number Then
			insPostSIL006_k = False
		End If
		
		lrecinsReaRSIL006 = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValSIL780_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma insValSIL780_K.
	Public Function insValSIL780_K(ByVal sCodispl As String, ByVal nYear As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSIL780_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+Se realiza la validacion del campo Año (Debe estar lleno)
		If nYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60471)
		End If
		
		insValSIL780_K = lclsErrors.Confirm
		
insValSIL780_K_Err: 
		If Err.Number Then
			insValSIL780_K = insValSIL780_K & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	'% VT00015 GAP 10 Historial del Asegurado
	Public Function insValSIL705(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		Dim lclsErrors As New eFunctions.Errors
		
		insValSIL705 = String.Empty
		
		On Error GoTo insValClaim_Err
		
		'+ Si la fecha Inicial es diferente de vacio continua las validaciones
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		End If
		'+ Se verifica que que la fecha final sea mayor a la fecha inicial
		If dEndDate < dIniDate Then
			Call lclsErrors.ErrorMessage(sCodispl, 4159)
		End If
		'+ Se verifica que la fecha final no sea mayor a la fecha del día
        'If dEndDate > Today Then
        '	Call lclsErrors.ErrorMessage(sCodispl, 4341)
        'End If
		
		insValSIL705 = lclsErrors.Confirm
		
insValClaim_Err: 
		If Err.Number Then
			insValSIL705 = "insValSIL705 : " & Err.Description
		End If
	End Function
	'% VT00017 GAP 11 Reportes de siniestros por estado
	Public Function insValSIL00970(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsPolicy As New ePolicy.Policy
		
		insValSIL00970 = String.Empty
		
		On Error GoTo insValClaim_Err
		
		'+ Debe indicar al menos un parámetro para ejecutar la transacción.
		If nPolicy = 0 Then
			'+ Si la fecha inicial es diferente de vacio continua las validaciones
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9071)
			End If
			'+ Si la fecha final es diferente de vacio continua las validaciones
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9072)
			End If
		End If
		
		If nPolicy > 0 Then
			'+ Si no se seleccionó Ramo
			If nBranch = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11135)
			End If
			'+ Si no se seleccionó Producto
			If nProduct = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			If nBranch > 0 And nProduct > 0 Then
				If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3001)
				End If
			End If
		End If
		
		If (dIniDate <> eRemoteDB.Constants.dtmNull Or dEndDate <> eRemoteDB.Constants.dtmNull) Then
			'+ Si la fecha final es diferente de vacio continua las validaciones
			If dEndDate < dIniDate Then
				'+ Se verifica que que la fecha final sea mayor a la fecha inicial
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
			'+ Se verifica que la fecha final no sea mayor a la fecha del día
			If dEndDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 4341)
			End If
		End If
		
		insValSIL00970 = lclsErrors.Confirm
		
insValClaim_Err: 
		If Err.Number Then
			insValSIL00970 = "insValSIL00970 : " & Err.Description
		End If
	End Function
	'% VT00057 GAP 12 Reportes de documentos solicitados a un siniestro
	Public Function insValSIL00971(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nClaim As Integer) As String
		
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsPolicy As New ePolicy.Policy
		Dim lclsClaim As eClaim.Claim = New eClaim.Claim
		
		insValSIL00971 = String.Empty
		
		On Error GoTo insValClaim_Err
		
		If nPolicy = 0 And nClaim = 0 Then
			'+ Debe indicar al menos un parametro para ejecutar la transacción
			Call lclsErrors.ErrorMessage(sCodispl, 60477)
		End If
		
		If nPolicy > 0 Then
			'+ Si no se seleccionó Ramo
			If nBranch = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11135)
			End If
			'+ Si no se seleccionó Producto
			If nProduct = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			If nBranch > 0 And nProduct > 0 Then
				If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3001)
				End If
			End If
			
		End If
		
		If nClaim > 0 Then
			If lclsClaim.Find(nClaim) Then
				If (lclsClaim.sStaClaim = 1 Or lclsClaim.sStaClaim = 7) Then
					Call lclsErrors.ErrorMessage(sCodispl, 4099)
				ElseIf (lclsClaim.sStaClaim = 6) Then 
					Call lclsErrors.ErrorMessage(sCodispl, 4305)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 4078)
			End If
		End If
		
		insValSIL00971 = lclsErrors.Confirm
		
insValClaim_Err: 
		If Err.Number Then
			insValSIL00971 = "insValSIL00971 : " & Err.Description
		End If
	End Function
	
	
	'insPostSIL780_K: Se invoca el reporte correspondiente a la transacción SIL780_K
	Public Function insPostSIL780_K(ByVal nYear As Integer, ByVal nBranch As Double) As Boolean
		Dim lrecSIL780 As New eRemoteDB.Execute
		
		On Error GoTo insPostSIL780_K_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insReaRSIL006'
		With lrecSIL780
			.StoredProcedure = "INSCLAIM_SIL780"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostSIL780_K = True
				Me.sKey = .Parameters("sKey").Value
			Else
				insPostSIL780_K = False
			End If
		End With
		
insPostSIL780_K_Err: 
		If Err.Number Then
			insPostSIL780_K = False
		End If
		On Error GoTo 0
		lrecSIL780 = Nothing
	End Function
	
    '% insValSil974: Se válida la invoción del reporte correspondiente a la transacción SIL974
    Public Function insValSIL974(ByVal sCodispl As String, ByVal nRequest_Nu As Double) As String
        Dim lclsClaim As eClaim.Claim = New eClaim.Claim
        Dim lclsErrors As New eFunctions.Errors
        Dim lclsResult As Boolean

        insValSIL974 = String.Empty

        On Error GoTo insValClaim_Err

        '+ Validación de cheques aprobados
        lclsResult = lclsClaim.Reastatus_Cheques(nRequest_Nu)

        '+ Si el número de órden es distinto de nulo y vacío se aprueba la validación
        If nRequest_Nu = eRemoteDB.Constants.intNull Or nRequest_Nu = -32768.0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 12036)
        End If

        If lclsResult = False Then
            Call lclsErrors.ErrorMessage(sCodispl, 8034, , eFunctions.Errors.TextAlign.RigthAling, "para órdenes de pago")
        End If

        insValSIL974 = lclsErrors.Confirm

insValClaim_Err:
        If Err.Number Then
            insValSIL974 = "insValSIL010_k: " & Err.Description
        End If

        lclsErrors = Nothing
        On Error GoTo 0

    End Function

    'insPostSIL974: Se invoca el reporte correspondiente a la transacción SIL974
    '------------------------------------------------------------------------------------------------
    Public Function insPostSIL974(ByVal nRequest_Nu As Double) As Boolean
        '------------------------------------------------------------------------------------------------
        Dim lrecSIL974 As New eRemoteDB.Execute
        On Error GoTo insPostSIL974_Err

        '+ Definición de parámetros para stored procedure 'insudb.insReaRSIL006'
        With lrecSIL974
            .StoredProcedure = "INSSIL974"
            .Parameters.Add("nRequest_Nu", nRequest_Nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostSIL974 = True
                Me.sKey = .Parameters("sKey").Value
            Else
                insPostSIL974 = False
            End If
        End With

insPostSIL974_Err:
        If Err.Number Then
            insPostSIL974 = False
        End If
        On Error GoTo 0
        lrecSIL974 = Nothing
    End Function

	'**% insValTranSIL009: It validate some specific transaction for the page SIL009_K
	'% insValTranSIL009: Valida algunas transacciones especificas para la transaccion SIL009_K
	Public Function insValTranSIL009(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		On Error GoTo insValTranSIL009_err
		
		nIndBDraft = 0
		insValTranSIL009 = True
		
		If valExistSequenPol(nBranch, nProduct, "IN010") Then
			nIndBDraft = 1
		Else
			If valExistSequenPol(nBranch, nProduct, "RO001") Then
				nIndBDraft = 1
			Else
				If valExistSequenPol(nBranch, nProduct, "LCC001") Then
					nIndBDraft = 1
				Else
					If valExistSequenPol(nBranch, nProduct, "MU001") Then
						nIndBDraft = 1
					Else
						nIndBDraft = 0
					End If
				End If
			End If
		End If
		
insValTranSIL009_err: 
		If Err.Number Then
			insValTranSIL009 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% valExistSequenPol: It Verify if a specific transaction is in the windows sequence of a product
	'% valExistSequenPol: Permite verificar la existencia de la ventana de datos particulares de un ramo en un producto dado.
	Private Function valExistSequenPol(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCodispl As String) As Boolean
		
		'**- Varible lrecSequenPol is defined in order to execute de stored procedure
		'- Se define la variable lrecSequenPol para ejecutar el store procedure
		
		Dim lrecSequenPol As eRemoteDB.Execute
		lrecSequenPol = New eRemoteDB.Execute
		
		On Error GoTo valExistSequenPol_err
		
		valExistSequenPol = False
		
		With lrecSequenPol
			.StoredProcedure = "valExistSequenPol"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				valExistSequenPol = True
				.RCloseRec()
			End If
		End With
		
		lrecSequenPol = Nothing
		
valExistSequenPol_err: 
		If Err.Number Then
			valExistSequenPol = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% insPostSIL1076: Reporte Provisión de Siniestros
	Public Function insPostSIL1076(ByVal dDateFrom As Date, ByVal dDateTo As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecinsPostSIL1076 As eRemoteDB.Execute
		
		lrecinsPostSIL1076 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1076
			.StoredProcedure = "rea_sil1076"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1076 = True
			Else
				insPostSIL1076 = False
			End If
		End With
		
		lrecinsPostSIL1076 = Nothing
		
	End Function
	
	Public Function insPostSIL1075(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		
		Dim lrecinsPostSIL1075 As eRemoteDB.Execute
		
		lrecinsPostSIL1075 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil1075'
		With lrecinsPostSIL1075
			.StoredProcedure = "rea_sil1075"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1075 = True
			Else
				insPostSIL1075 = False
			End If
			
		End With
		
		lrecinsPostSIL1075 = Nothing
		
	End Function
	
	
	'insPostSIL006_k: Se invoca el reporte correspondiente a la transacción SIL006
	Public Function insPostSIL009_k(ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nTyperep As Integer, ByVal nOffice As Integer, ByVal nDetOffice As Integer, ByVal nBranch As Integer, ByVal nDetBranch As Integer, ByVal nProduct As Integer, ByVal nDetProduct As Integer, ByVal nType_mov As Integer, ByVal nDetMov As Integer, ByVal nCause As Integer, ByVal nDetCause As Integer, ByVal nDraft As Integer, ByVal nDetDraft As Integer, ByVal nIndic As Integer) As Boolean
		
		Dim lrecinsReaRSIL009 As New eRemoteDB.Execute
		
		On Error GoTo insPostSIL009_k_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insReaRSIL006'
		With lrecinsReaRSIL009
			.StoredProcedure = "insReaRSIL009"
			
			.Parameters.Add("dInitdate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperep", nTyperep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetOffice", nDetOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetBranch", nDetBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetProduct", nDetProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Mov", nType_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetMov", nDetMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetCause", nDetCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetDraft", nDetDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndic", nIndic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostSIL009_k = True
				Me.sKey = .Parameters("sKey").Value
			Else
				insPostSIL009_k = False
			End If
			
		End With
		
insPostSIL009_k_Err: 
		If Err.Number Then
			insPostSIL009_k = False
		End If
		
		lrecinsReaRSIL009 = Nothing
		On Error GoTo 0
		
	End Function
	
	
	'% insPostSIL1067:
	Public Function insPostSIL1067(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal dOperdate As Date) As Boolean
		
		Dim lrecinsPostSIL1067 As eRemoteDB.Execute
		
		lrecinsPostSIL1067 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1067
			.StoredProcedure = "rea_sil1067"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("doperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1067 = True
			Else
				insPostSIL1067 = False
			End If
			
		End With
		
		lrecinsPostSIL1067 = Nothing
		
	End Function
	
	
	Public Function insPostSIL1070(ByVal dDateRep As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		Dim lrecinsPostSIL1070 As eRemoteDB.Execute
		
		lrecinsPostSIL1070 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1070
			.StoredProcedure = "rea_sil1070"
			.Parameters.Add("dDateRep", dDateRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1070 = True
			Else
				insPostSIL1070 = False
			End If
			
		End With
		
		lrecinsPostSIL1070 = Nothing
		
	End Function
	
	
	'% insPostSIL1066:
	Public Function insPostSIL1066(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		
		Dim lrecinsPostSIL1066 As eRemoteDB.Execute
		
		lrecinsPostSIL1066 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1066
			.StoredProcedure = "rea_sil1066"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1066 = True
			Else
				insPostSIL1066 = False
			End If
			
		End With
		
		lrecinsPostSIL1066 = Nothing
		
	End Function
	
	'% insPostSIL1065:
	Public Function insPostSIL1065(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		
		Dim lrecinsPostSIL1065 As eRemoteDB.Execute
		
		lrecinsPostSIL1065 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1065
			.StoredProcedure = "rea_sil1065"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1065 = True
			Else
				insPostSIL1065 = False
			End If
			
		End With
		
		lrecinsPostSIL1065 = Nothing
		
	End Function
	
	
	Public Function insPostSIL833(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL833 As eRemoteDB.Execute
		
		lrecinsPostSIL833 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil1075'
		With lrecinsPostSIL833
			.StoredProcedure = "rea_sil833"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL833 = True
			Else
				insPostSIL833 = False
			End If
			
		End With
		
		lrecinsPostSIL833 = Nothing
		
	End Function
	Public Function insPostSIL834(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL834 As eRemoteDB.Execute
		
		lrecinsPostSIL834 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil1075'
		With lrecinsPostSIL834
			.StoredProcedure = "rea_sil834"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL834 = True
			Else
				insPostSIL834 = False
			End If
			
		End With
		
		lrecinsPostSIL834 = Nothing
		
	End Function
	
	
	Public Function insPostSIL901(ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL901 As eRemoteDB.Execute
		
		lrecinsPostSIL901 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil1075'
		With lrecinsPostSIL901
			.StoredProcedure = "rea_sil901"
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL901 = True
			Else
				insPostSIL901 = False
			End If
			
		End With
		
		lrecinsPostSIL901 = Nothing
		
	End Function
	
	Public Function insPostSIL902(ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nClaim As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL902 As eRemoteDB.Execute
		
		lrecinsPostSIL902 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil1075'
		With lrecinsPostSIL902
			.StoredProcedure = "rea_sil902"
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL902 = True
			Else
				insPostSIL902 = False
			End If
			
		End With
		
		lrecinsPostSIL902 = Nothing
		
	End Function
	
	Public Function insPostSIL837(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL837 As eRemoteDB.Execute
		
		lrecinsPostSIL837 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostSIL837
			.StoredProcedure = "rea_sil837"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL837 = True
			Else
				insPostSIL837 = False
			End If
			
		End With
		
		lrecinsPostSIL837 = Nothing
		
	End Function
	
	Public Function insPostSIL838(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL838 As eRemoteDB.Execute
		
		lrecinsPostSIL838 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostSIL838
			.StoredProcedure = "rea_sil838"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL838 = True
			Else
				insPostSIL838 = False
			End If
			
		End With
		
		lrecinsPostSIL838 = Nothing
		
	End Function
	
	Public Function insPostSIL840(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostSIL840 As eRemoteDB.Execute
		
		lrecinsPostSIL840 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostSIL840
			.StoredProcedure = "rea_sil840"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL840 = True
			Else
				insPostSIL840 = False
			End If
			
		End With
		
		lrecinsPostSIL840 = Nothing
		
	End Function
	
	
	
	Public Function insPostCAC1024(ByVal nPolicy As Integer, ByVal nClaim As Integer, ByVal nCover As Integer) As Boolean
		
		Dim lrecinsPostCAC1024 As eRemoteDB.Execute
		
		lrecinsPostCAC1024 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCAC1024
			.StoredProcedure = "rea_CAC1024"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAC1024 = True
			Else
				insPostCAC1024 = False
			End If
			
		End With
		
		lrecinsPostCAC1024 = Nothing
		
	End Function
	
	
	'% insPostSIL1072: Reporte Informe Histórico de Pólizas
	Public Function insPostSIL1072(ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal nPolicy As Double, ByVal sClient As String, ByVal nTypeper As Double) As Boolean
		Dim lrecinsPostSIL1072 As eRemoteDB.Execute
		
		lrecinsPostSIL1072 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1072
			.StoredProcedure = "rea_sil1072"
			.Parameters.Add("nTypeper", nTypeper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostSIL1072 = True
			Else
				insPostSIL1072 = False
			End If
		End With
		
		lrecinsPostSIL1072 = Nothing
		
	End Function
	
	Public Function insValSI957_k(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nRate As Double) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		
		On Error GoTo insValSI957_k_Err
		
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4003)
		Else
			lclsCtrol_Date = New eGeneral.Ctrol_date
			Call lclsCtrol_Date.Find(79)
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If Not IsDbNull(lclsCtrol_Date.dEffecdate) And lclsCtrol_Date.dEffecdate <> eRemoteDB.Constants.dtmNull Then
				If dEffecdate <= lclsCtrol_Date.dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 9122)
				End If
			End If
			
			lclsCtrol_Date = Nothing
		End If
		
		If nRate = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60140)
		End If
		
		insValSI957_k = lclsErrors.Confirm
		
insValSI957_k_Err: 
		If Err.Number Then
			insValSI957_k = "insValSI957_k: " & Err.Description
		End If
		
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insPostSI957: Reserva matemática de siniestros
	Public Function insPostSI957(ByVal dEffecdate As Date, ByVal nRate As Double, ByVal nUserCode As Integer, ByVal nSessionId As String, ByVal sExecute As String) As Boolean
		Dim lrecinsPostSI957 As eRemoteDB.Execute
		
		lrecinsPostSI957 = New eRemoteDB.Execute
		
		Me.sKey = "TMP" & nSessionId & nUserCode
		With lrecinsPostSI957
			.StoredProcedure = "INSSI957PKG.insPostSI957"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostSI957 = True
			Else
				insPostSI957 = False
			End If
		End With
		
		lrecinsPostSI957 = Nothing
	End Function
	
	'%insValSIL705_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma SIL705.
	Public Function InsValSIL705_k(ByVal sCodispl As String, ByVal dDate_ini As Date, ByVal dDate_end As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSIL705_k_Err

		lclsErrors = New eFunctions.Errors
		
		'+Se realiza la validacion del campo Fecha de Inicio
		If dDate_ini = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 5072)
		End If
		
		'+Se valida la fecha final
		If dDate_end = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1097)
		End If
		
		'+Se valida que la fecha inicial no sea mayor que la fecha final
		If dDate_ini <> eRemoteDB.Constants.dtmNull And dDate_end <> eRemoteDB.Constants.dtmNull Then
			If dDate_ini > dDate_end Then
				Call lclsErrors.ErrorMessage(sCodispl, 11425)
			End If
		End If
		
insValSIL705_k_Err:
        If Err.Number Then
            InsValSIL705_k = ""
            InsValSIL705_k = InsValSIL705_k & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	
	'%insValSIL705_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma SIL1001.
	Public Function InsValSIL1001_k(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		
		On Error GoTo insValSIL1001_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		'+Se realiza la validacion del campo de siniestro, si esta vacio
		If nClaim = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4006)
		Else
			If Not lclsClaim.Find(nClaim) Then
				Call lclsErrors.ErrorMessage(sCodispl, 4005)
			Else
				If lclsClaim.sStaClaim = Claim.Estatclaim.eNull Or lclsClaim.sStaClaim = Claim.Estatclaim.eRefuse Or lclsClaim.sStaClaim = Claim.Estatclaim.eImcomplete Then
					Call lclsErrors.ErrorMessage(sCodispl, 55759)
				End If
			End If
		End If
		
		'+Validacion del campo caso
		If nCase_num <= 0 Or nCase_num = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4289)
		End If
		
		InsValSIL1001_k = lclsErrors.Confirm
		
insValSIL1001_k_Err: 
		If Err.Number Then
			InsValSIL1001_k = InsValSIL1001_k & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
		
	End Function
	
	
	'%insValSIL978_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma SIL978.
	Public Function InsValSIL978_k(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		
		On Error GoTo insValSIL978_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		'+Se realiza la validacion del campo de siniestro, si esta vacio
		If nClaim = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4006)
		Else
			If Not lclsClaim.Find(nClaim) Then
				Call lclsErrors.ErrorMessage(sCodispl, 4005)
			Else
				If lclsClaim.sStaClaim = Claim.Estatclaim.eNull Or lclsClaim.sStaClaim = Claim.Estatclaim.eRefuse Or lclsClaim.sStaClaim = Claim.Estatclaim.eImcomplete Then
					Call lclsErrors.ErrorMessage(sCodispl, 55759)
				End If
			End If
		End If
		
		'+Validacion del campo caso
		If nCase_num <= 0 Or nCase_num = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4289)
		End If
		
		InsValSIL978_k = lclsErrors.Confirm
		
insValSIL978_k_Err: 
		If Err.Number Then
			InsValSIL978_k = InsValSIL978_k & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
		
	End Function
	
	
	'%insValSIL1002: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma SIL1002.
	Public Function InsValSIL1002(ByVal sCodispl As String, ByVal sDate As String, ByVal sStaClaim As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValSIL1002_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+Se realiza la validacion del campo estado de siniestro
		If sStaClaim = String.Empty Or sStaClaim = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.RigthAling, " - Estado del siniestro")
		End If
		
		'+Validacion del campo Fecha Inicial
		If dInitDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.RigthAling, " - Fecha inicial")
		End If
		
		'+Validacion del campo Fecha Final
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.RigthAling, " - Fecha final")
		End If
		
		InsValSIL1002 = lclsErrors.Confirm
		
InsValSIL1002_Err: 
		If Err.Number Then
			InsValSIL1002 = InsValSIL1002 & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		
	End Function
    '%InsValSIL1065_k: Esta función se encarga de validar las fechas inicio/fin del encabezado
    Public Function InsValSIL1065_k(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValSIL1065_k_Err

        lclsErrors = New eFunctions.Errors

        '+Validacion del campo Fecha Inicial
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 5072, , eFunctions.Errors.TextAlign.RigthAling)
        End If

        '+Validacion del campo Fecha Final
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1097, , eFunctions.Errors.TextAlign.RigthAling)
        End If

        InsValSIL1065_k = lclsErrors.Confirm

InsValSIL1065_k_Err:
        If Err.Number Then
            InsValSIL1065_k = InsValSIL1065_k & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing

    End Function

	'insPostSIL1002: Esta función se encarga de ejecutar el proceso de la forma SIL1002.
	Public Function insPostSIL1002(ByVal sDate As String, ByVal sStaClaim As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal sFileName As String, ByVal sPath As String) As Boolean
		
		Dim lrecinsPostSIL1002 As eRemoteDB.Execute
		
        Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
		Dim lclsWorksheet As Microsoft.Office.Interop.Excel.Worksheet
		Dim lclsValue As eFunctions.Values
		Dim lintRow As Short
		Dim lintExist As Short
		Dim lstrFile As String
		Dim lstrFileName As String
		Dim lintlength As Short
		
		lclsExcelApp = New Microsoft.Office.Interop.Excel.Application
		
		lintExist = InStr(1, UCase(sFileName), ".XLS")
		If lintExist > 0 Then
			lstrFile = Mid(sFileName, 1, lintExist - 1)
		Else
			lstrFile = sFileName
		End If
		
		lclsValue = New eFunctions.Values
		
		lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
		If lstrFileName = String.Empty Then
			lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "Config")))
		End If
		
		lintlength = Len(lstrFileName)
		If Mid(lstrFileName, lintlength, 1) <> "\" Then
			lstrFileName = lstrFileName & "\"
		End If
		
		lstrFileName = lstrFileName & Trim(lstrFile) & ".XLS"
		
		With lclsExcelApp
			.DisplayAlerts = False
			.Workbooks.Add()
			.Workbooks(1).Sheets(1).Name = "Extracción de datos"
			.Workbooks(1).Sheets(2).Delete()
			.Workbooks(1).Sheets(1).Activate()
			lclsWorksheet = .Workbooks(1).Sheets(1)
		End With
		
		lrecinsPostSIL1002 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostSIL1002
			.StoredProcedure = "REASIL1002"
			.Parameters.Add("sDate", sDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStaClaim", sStaClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				insPostSIL1002 = True
				
				
				lclsWorksheet.Cells._Default(5, 1) = "Número de siniestro"
				lclsWorksheet.Cells._Default(5, 2) = "Número de póliza"
				lclsWorksheet.Cells._Default(5, 3) = "Nombre asegurado afectado"
                lclsWorksheet.Cells._Default(5, 4) = "Rut asegurado afectado"
				lclsWorksheet.Cells._Default(5, 5) = "Descripción producto"
				lclsWorksheet.Cells._Default(5, 6) = "Código de cobertura"
				lclsWorksheet.Cells._Default(5, 7) = "Descripción de cobertura"
				lclsWorksheet.Cells._Default(5, 8) = "Monto a indemnizar por cobertura"
				lclsWorksheet.Cells._Default(5, 9) = "Ramo contable"
				lclsWorksheet.Cells._Default(5, 10) = "Monto UF / $ Coberturas indemnizadas"
				lclsWorksheet.Cells._Default(5, 11) = "Monto UF / Reaseguro"
				
				lintRow = 6
				
				Do While Not .EOF
					
					lclsWorksheet.Cells._Default(lintRow, 1) = .FieldToClass("NCLAIM")
					lclsWorksheet.Cells._Default(lintRow, 2) = .FieldToClass("NPOLICY")
					lclsWorksheet.Cells._Default(lintRow, 3) = .FieldToClass("SCLIENAME_BENEF_CAUS")
					'lclsWorksheet.Cells(lintRow, 3).HorizontalAlignment = xlLeft
					lclsWorksheet.Cells._Default(lintRow, 4) = .FieldToClass("SRUT_BENEF_CAUS")
					lclsWorksheet.Cells._Default(lintRow, 5) = .FieldToClass("SPRODUCT")
					lclsWorksheet.Cells._Default(lintRow, 6) = .FieldToClass("NCOVER")
					lclsWorksheet.Cells._Default(lintRow, 7) = .FieldToClass("SCOVER")
					lclsWorksheet.Cells._Default(lintRow, 8) = .FieldToClass("NDAMAGES")
					lclsWorksheet.Cells._Default(lintRow, 9) = .FieldToClass("NBRANCH_LED")
					lclsWorksheet.Cells._Default(lintRow, 10) = .FieldToClass("NPAY_AMOUNT")
					lclsWorksheet.Cells._Default(lintRow, 11) = .FieldToClass("NREA_AMOUNT")
					
					lintRow = lintRow + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insPostSIL1002 = False
			End If
		End With
		
		With lclsWorksheet
			For lintRow = 1 To 11
				.Cells._Default(5, lintRow).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
				.Cells._Default(5, lintRow).Font.Bold = True
				.Columns._Default(lintRow).EntireColumn.AutoFit()
			Next 
		End With
		
		
		With lclsExcelApp
			.ActiveWorkbook.SaveAs(lstrFileName)
			.ActiveWorkbook.Close()
			.Quit()
		End With
		
		lclsExcelApp = Nothing
		
		lrecinsPostSIL1002 = Nothing
		
		
    End Function


    '%insValSIL7482_K: Esta función se encarga de validar los datos introducidos en la zona de
    '%detalle de la forma SIL7482.
    Public Function insValSIL7482_K(ByVal sCodispl As String, ByVal nClaim As Double, ByVal dValDate As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClaim As eClaim.Claim


        lclsErrors = New eFunctions.Errors
        lclsClaim = New eClaim.Claim

        '+Se realiza la validacion del campo de siniestro, si esta vacio
        If nClaim = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4006)
        Else
            If Not lclsClaim.Find(nClaim) Then
                Call lclsErrors.ErrorMessage(sCodispl, 4005)
            Else
                If lclsClaim.sStaclaim = Claim.Estatclaim.eNull Or lclsClaim.sStaclaim = Claim.Estatclaim.eRefuse Or lclsClaim.sStaclaim = Claim.Estatclaim.eImcomplete Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55759)
                End If
            End If
        End If

        '+Validacion de la fecha de valorización
        If dValDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 55527)
        End If

        insValSIL7482_K = lclsErrors.Confirm

    End Function


    '% Count_SIL005: Obtiene la cantidad de registros almacenados en el arreglo
    Public ReadOnly Property Count_SIL005() As Integer
        Get
            Count_SIL005 = UBound(marrSIL005)
        End Get
    End Property

    '% Item_SIL005: Asigna a cada propiedad de la clase el valor correspondiente en el arreglo [APV2] - ACM - 01/09/2003
    Public Function Item_SIL005(ByVal nIndex As Integer) As Boolean

        On Error GoTo Item_SIL005_err

        If nIndex <= UBound(marrSIL005) Then
            With marrSIL005(nIndex)
                Me.nSettlecode = .nSettlecode
                Me.sFormatname = .sFormatname
                Me.nClaim = .nClaim
                Me.nOrder = .nOrder

            End With
            Item_SIL005 = True
        Else
            Item_SIL005 = False
        End If

Item_SIL005_err:
        If Err.Number Then
            Item_SIL005 = False
        End If
    End Function

    Public Function insReaSIL005(ByVal sCodispl As String, ByVal nBranch As Integer, Optional ByVal nOffice As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Integer = 0, Optional ByVal nCase_num As Integer = 0, Optional ByVal nDeman_type As Integer = 0, Optional ByVal nSettle_num As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nPay_concep As Integer = 0) As Boolean
        Dim lrecreaSIL005 As eRemoteDB.Execute
        'On Error GoTo insPreCA995_Err
        lrecreaSIL005 = New eRemoteDB.Execute
        Dim lintCount As Integer


        With lrecreaSIL005
            .StoredProcedure = "REASIL005"
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettle_num", nSettle_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Try
                If .Run(True) Then
                    ReDim marrSIL005(100)
                    lintCount = 1
                    Do While Not .EOF
                        marrSIL005(lintCount).nSettlecode = .FieldToClass("nSettlecode")
                        marrSIL005(lintCount).nClaim = .FieldToClass("nClaim")
                        marrSIL005(lintCount).sFormatname = .FieldToClass("sFormatname")
                        marrSIL005(lintCount).nOrder = .FieldToClass("nOrder")

                        lintCount = lintCount + 1
                        .RNext()
                        ReDim Preserve marrSIL005(lintCount)
                    Loop

                    insReaSIL005 = True
                Else
                    insReaSIL005 = False
                End If
            Catch ex As Exception
                insReaSIL005 = False
            End Try
        End With
        lrecreaSIL005 = Nothing
    End Function

End Class






