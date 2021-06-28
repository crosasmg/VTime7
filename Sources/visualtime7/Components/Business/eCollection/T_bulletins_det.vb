Option Strict Off
Option Explicit On
Public Class T_bulletins_det
	'%-------------------------------------------------------%'
	'% $Workfile:: T_bulletins_det.cls                      $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 20/10/03 4:58p                               $%'
	'% $Revision:: 29                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla t_bulletins_det al 02-08-2002 18:16:08
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sSel As String ' CHAR       1    0     0    S
	Public nBulletins As Double ' NUMBER     22   0     10   N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nCollecDocTyp As Integer ' NUMBER     22   0     5    S
	Public dCollectdate As Date ' DATE       7    0     0    S
	Public sCertype As String ' CHAR       1    0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public nPolicy As Double ' NUMBER     22   0     10   S
	Public nCertif As Double ' NUMBER     22   0     10   S
	Public nReceipt As Double ' NUMBER     22   0     10   S
	Public nDigit As Integer ' NUMBER     22   0     5    S
	Public nPaynumbe As Integer ' NUMBER     22   0     5    S
	Public nContrat As Double ' NUMBER     22   0     10   S
	Public nDraft As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    S
	Public sCliename As String ' CHAR       14   0     0    S
	Public sClieDigit As String ' CHAR       14   0     0    S
	Public dStatdate As Date ' DATE       7    0     0    S
	Public dExpirDat As Date ' DATE       7    0     0    S
	Public dLimitdate As Date ' DATE       7    0     0    S
	Public nType As Integer ' NUMBER     22   0     5    S
	Public nTratypei As Integer ' NUMBER     22   0     5    S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public nAmount As Double ' NUMBER     22   2     10   S
	Public nCod_Agree As Integer ' NUMBER     22   0     5    S
	Public sIndColl_exp As String ' CHAR       1    0     0    S
	Public sStyle_Bull As String ' CHAR       1    0     0    S
	Public sQueryOption As String ' CHAR       1    0     0    S
	Public sCollector As String ' CHAR       1    0     0    S
	Public nInsur_area As Integer ' NUMBER     22   0     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'+ Variables públicas a la clase.
	Public sStatus As String
	Public nTotalDoc As Double
	Public nTotalInterest As Double
	Public nTotalColl_exp As Double
	Public nTotalLoans As Double
	Public nTotalClient As Double
	Public nTotalCtaIndiv As Double
	Public nTotalReliqPrima As Double
	Public nTotalBonoRecono As Double
	Public nTotalComplBono As Double
	Public nTotalPolitAdic As Double
	Public nTotalPrimaPriv As Double
	
	Public nTotalGeneral As Double
	
	'%InsUpdT_bulletins_det: Se encarga de actualizar la tabla T_bulletins_det
	Public Function InsUpdT_bulletins_det(ByVal nAction As Integer) As Boolean
		Dim lrecT_bulletins_det As eRemoteDB.Execute
		
		On Error GoTo insUpdt_bulletins_det_Err
		
		lrecT_bulletins_det = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdt_bulletins_det al 02-08-2002 19:44:50
		With lrecT_bulletins_det
			.StoredProcedure = "insUpdt_bulletins_det"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecdoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectDate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimitdate", dLimitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndColl_Exp", sIndColl_exp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_bull", sStyle_Bull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQueryOption", sQueryOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollector", sCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdT_bulletins_det = .Run(False)
		End With
		
insUpdt_bulletins_det_Err: 
		If Err.Number Then
			InsUpdT_bulletins_det = False
		End If
		'UPGRADE_NOTE: Object lrecT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_bulletins_det = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdT_bulletins_det(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdT_bulletins_det(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdT_bulletins_det(3)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete_all(ByVal nBulletins As Double) As Boolean
		Dim lrecT_bulletins_det As eRemoteDB.Execute
		
		On Error GoTo Delete_all_Err
		
		lrecT_bulletins_det = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure Delete_all al 02-21-2002 10:56:50
		'+
		With lrecT_bulletins_det
			.StoredProcedure = "delT_bulletins_det"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_all = .Run(False)
		End With
		
Delete_all_Err: 
		If Err.Number Then
			Delete_all = False
		End If
		'UPGRADE_NOTE: Object lrecT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_bulletins_det = Nothing
		On Error GoTo 0
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBulletins As Integer, ByVal nCollecDocTyp As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaT_bulletins_det As eRemoteDB.Execute
		
		On Error GoTo reaT_bulletins_det_Err
		
		Find = True
		
		If Me.nBulletins <> nBulletins Or Me.nCollecDocTyp <> nCollecDocTyp Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nReceipt <> nReceipt Or Me.nDigit <> nDigit Or Me.nPaynumbe <> nPaynumbe Or Me.nContrat <> nContrat Or Me.nDraft <> nDraft Or lblnFind Then
			
			lrecreaT_bulletins_det = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaT_bulletins_det al 02-11-2002 16:45:13
			'+
			With lrecreaT_bulletins_det
				.StoredProcedure = "reaT_bulletins_det"
				.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCollecdoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sSel", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Find = True
					Me.sSel = .FieldToClass("sSel")
					Me.nBulletins = .FieldToClass("nBulletins")
					Me.nId = .FieldToClass("nId")
					Me.nCollecDocTyp = .FieldToClass("nCollecdoctyp")
					Me.dCollectdate = .FieldToClass("dCollectDate")
					Me.sCertype = .FieldToClass("sCertype")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy = .FieldToClass("nPolicy")
					Me.nCertif = .FieldToClass("nCertif")
					Me.nReceipt = .FieldToClass("nReceipt")
					Me.nDigit = .FieldToClass("nDigit")
					Me.nPaynumbe = .FieldToClass("nPaynumbe")
					Me.nContrat = .FieldToClass("nContrat")
					Me.nDraft = .FieldToClass("nDraft")
					Me.sClient = .FieldToClass("sClient")
					Me.dStatdate = .FieldToClass("dStatdate")
					Me.dExpirDat = .FieldToClass("dExpirdat")
					Me.dLimitdate = .FieldToClass("dLimitdate")
					Me.nType = .FieldToClass("nType")
					Me.nTratypei = .FieldToClass("nTratypei")
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.nAmount = .FieldToClass("nAmount")
					Me.nCod_Agree = .FieldToClass("nCod_agree")
					Me.sIndColl_exp = .FieldToClass("sQueryOption")
					Me.sStyle_Bull = .FieldToClass("sQueryOption")
					Me.sQueryOption = .FieldToClass("sQueryOption")
					Me.sCollector = .FieldToClass("sQueryOption")
					Me.nInsur_area = .FieldToClass("nInsur_area")
					
				Else
					Find = False
					Me.nBulletins = eRemoteDB.Constants.intNull
					Me.nId = eRemoteDB.Constants.intNull
					Me.nCollecDocTyp = eRemoteDB.Constants.intNull
					Me.sCertype = String.Empty
					Me.nBranch = eRemoteDB.Constants.intNull
					Me.nProduct = eRemoteDB.Constants.intNull
					Me.nPolicy = eRemoteDB.Constants.intNull
					Me.nCertif = eRemoteDB.Constants.intNull
					Me.nReceipt = eRemoteDB.Constants.intNull
					Me.nDigit = eRemoteDB.Constants.intNull
					Me.nPaynumbe = eRemoteDB.Constants.intNull
					Me.nContrat = eRemoteDB.Constants.intNull
					Me.nDraft = eRemoteDB.Constants.intNull
					Me.sClient = String.Empty
				End If
			End With
		End If
		
reaT_bulletins_det_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaT_bulletins_det = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValCO632_K: Validaciones de la transacción(Header)
	Public Function InsValCO632_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sQueryOption As String, ByVal nInsur_area As Integer, ByVal dCollectdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal sClient As String, ByVal sStyle_Bull As String, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjPolicy As Object '+ ePolicy.Policy
		Dim lclsPremium As eCollection.Premium
		Dim lobjClient As Object '+ eClient.Client
        Dim lobjvalClient As Object '+ eClient.ValClient
        'TODO: NO existe la clase eClient.eDeathBlock
        'Dim ludtDeathBlock As eClient.eDeathBlock
		
		
		Dim lblnError As Boolean
		
		On Error GoTo InsValCO632_K_Err
		lclsErrors = New eFunctions.Errors
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsPremium = New eCollection.Premium
		lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		lobjvalClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.ValClient")
		
		With lclsErrors
			Select Case sQueryOption
				'+ Validación por póliza
				Case "1"
					'+ Validación ramo
					If nBranch <= 0 Then
						.ErrorMessage(sCodispl, 9064)
						lblnError = True
					End If
					
					'+ Validación producto
					If nProduct <= 0 Then
						.ErrorMessage(sCodispl, 11009)
						lblnError = True
					End If
					
					'+ Validación póliza
					If nPolicy <= 0 Then
						.ErrorMessage(sCodispl, 3003)
						lblnError = True
					End If
					
					'+ Se verifica que la póliza este registrada y se encuentre válida.
					If Not lblnError Then '+ validación del número de póliza
						If Not lobjPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
							.ErrorMessage(sCodispl, 3001)
							lblnError = True
						End If
						
						If Not lblnError Then
							If lobjPolicy.sStatus_pol > CollectionSeq.TypeStatus_Pol.cstrValid And lobjPolicy.sStatus_pol < CollectionSeq.TypeStatus_Pol.cstrPrintPendent Then
								.ErrorMessage(sCodispl, 3720)
								lblnError = True
							End If
						End If
					End If
					
					'+ Validación por cliente
				Case "2"
					If Not lobjvalClient.Validate(sClient, eFunctions.Menues.TypeActions.clngActionUpdate) Then
						Select Case lobjvalClient.Status
							Case eClient.ValClient.eTypeValClientErr.StructInvalid
								.ErrorMessage(sCodispl, 2012)
								lblnError = True
							Case eClient.ValClient.eTypeValClientErr.FieldEmpty
								.ErrorMessage(sCodispl, 2001)
								lblnError = True
							Case eClient.ValClient.eTypeValClientErr.TypeNotFound
								If sClient <> String.Empty Then
									.ErrorMessage(sCodispl, 2013)
									lblnError = True
								End If
							Case eClient.ValClient.eTypeValClientErr.FieldNotFound
								If sClient <> String.Empty Then
									.ErrorMessage(sCodispl, 7050)
									lblnError = True
								End If
						End Select
                    Else
                        'TODO: NO existe la clase eClient.eDeathBlock
                        'ludtDeathBlock = lobjClient.ValBlockDeath(sClient)

                        'If ludtDeathBlock = eClient.eDeathBlock.Death Then
                        '    .ErrorMessage(sCodispl, 2051)
                        '    lblnError = True
                        'End If
						
					End If
					
					'+ Validación por recibo
				Case "3"
					'+ Se incluya el recibo
					If nReceipt <= 0 Then
						.ErrorMessage(sCodispl, 21034)
						lblnError = True
					End If
					
					If Not lblnError Then
						If Not lclsPremium.FindPremiumExist(sCertype, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nReceipt, 0, 0, 1) Then
							.ErrorMessage(sCodispl, 60249)
							lblnError = True
						Else
							'+ Se verifica que el recibo sea válido.
							If lclsPremium.sStatusva = "2" Or lclsPremium.sStatusva = "3" Then
								.ErrorMessage(sCodispl, 36217)
								lblnError = True
							End If
							
							'+ Se verifica de que el estado del recibo sea valido
							If lclsPremium.nStatus_pre <> Premium.StatusReceipt.clngPendent And lclsPremium.nStatus_pre <> Premium.StatusReceipt.clngLodgedPendent And lclsPremium.nStatus_pre <> Premium.StatusReceipt.clngFinanced Then
								.ErrorMessage(sCodispl, 21027)
								lblnError = True
							End If
							
							'+ Se verifica de que el área de seguro del recibo sea la misma que la seleccionada para el proceso.
							If nInsur_area > 0 Then
								If lclsPremium.nInsur_area <> nInsur_area Then
									.ErrorMessage(sCodispl, 60226)
									lblnError = True
								End If
							End If
						End If
					End If
			End Select
			
			'+ Validación área de seguro
			If nInsur_area <= 0 Then
				.ErrorMessage(sCodispl, 55031)
				lblnError = True
			End If
			
			'+ Se valida el Campo Fecha
			If dCollectdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 55559)
				lblnError = True
			End If
			
			'+ Se valida el campo moneda del boletin
			If sStyle_Bull = "1" Then
				If nCurrency = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 12110)
					lblnError = True
				End If
			End If
			
			'+ Si no existe ningún error se verifica si existe información para procesar según el filto ingresado.
			If Not lblnError Then
				'+ Si la acción es registrar
				If nAction = 301 Then
					'+ Si no existe información a procesar según condición
					If Not valExistsCO632_K(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, sClient, nInsur_area, nCurrency, sStyle_Bull) Then
						.ErrorMessage(sCodispl, 1069)
					End If
				End If
			End If
			
			InsValCO632_K = .Confirm
		End With
		
InsValCO632_K_Err: 
		If Err.Number Then
			InsValCO632_K = "InsValCO632_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCO632AUpd: Validaciones de la transacción(Folder)
	'%                 Tabla de control de prima mínima(CO632)
	Public Function insValCO632AUpd(ByVal sCodispl As String, ByVal sAction As String, ByVal sReceiptNum As String, ByVal sSel As String, ByVal nBulletins As Double, ByVal nCollecDocTyp As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nInsur_AreaReceipt As Integer, ByVal nStatus_pre As Integer, ByVal nStat_draft As Integer, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nInsur_area As Integer, ByVal nCurrencyBul As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjProdMaster As Object
		Dim lobjPremium As Object
		Dim lobjFinanc_dra As Object
		Dim llngError As Integer
		Dim lblnError As Boolean
		
		Dim lintInsur_area As Integer
		Dim llngBulletins As Integer
		Dim lintCod_agree As Integer
		Dim lstrClient As String
		Dim lintStatus As Integer
		
		On Error GoTo insValCO632AUpd_Err
		lclsErrors = New eFunctions.Errors
		lobjProdMaster = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lobjPremium = New Premium
		lobjFinanc_dra = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
		
		
		With lclsErrors
			'+ Validación Tipo de documento.
			If nCollecDocTyp <= 0 Then
				.ErrorMessage(sCodispl, 60210)
			End If
			
			'+ Validación Moneda.
			If nCurrency <= 0 Then
				.ErrorMessage(sCodispl, 750024)
			End If
			
			'+ Validación Monto.
			If nAmount = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60141)
			End If
			
			'+Validar que no se dupliquen registros
			If sAction = "Add" Then
				'+ Se verifica la existencia del documento
				
				If nCollecDocTyp = CollectionSeq.TypeDocument.clngDocReceipt Or nCollecDocTyp = CollectionSeq.TypeDocument.clngDocLoansInt Then
					
					'+ Si se indico tipo recibo o Intereses por préstamo, se valida que documento sea <> null
					If nReceipt = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 3379)
					Else
						'+Se valida estado del recibo
						If nStatus_pre <> 1 And nStatus_pre <> 8 Then
							.ErrorMessage(sCodispl, 60211)
						End If
						'+Se valida que recibo se encuentre en otro boletín
						llngError = getExistT_bulletins_det(nBulletins, nCollecDocTyp, sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, nDigit, nPaynumbe, nContrat, nDraft, sClient)
						If llngError > 0 Then
							.ErrorMessage(sCodispl, llngError)
							lblnError = True
						End If
					End If
					
				Else
					'+ Si se indico tipo documento contrato/cuota se valida que documento sea <> null
					If nCollecDocTyp = CollectionSeq.TypeDocument.clngDocDraft Then
						
						If nContrat = eRemoteDB.Constants.intNull Then
							.ErrorMessage(sCodispl, 3379)
						Else
							
							If nDraft < 0 Then
								.ErrorMessage(sCodispl, 21063)
								lblnError = True
							Else
								'+Se valida estado de la cuota
								If nStat_draft <> 1 Then
									.ErrorMessage(sCodispl, 60211)
								End If
								'+Se valida que contrato/cuota no se encuentre en otro boletín
								llngError = getExistT_bulletins_det(nBulletins, nCollecDocTyp, sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, nDigit, nPaynumbe, nContrat, nDraft, sClient)
								
								If llngError > 0 Then
									.ErrorMessage(sCodispl, llngError)
									lblnError = True
								End If
								
							End If 'nDraft
						End If 'nCollectDocTyp DocDraft
						
					End If 'nCollectDocTyp DocReceipt
				End If
			End If
			
			'+ Se valida que la moneda del documento sea la misma moneda del boletín
			If nCurrencyBul = 0 Then
				If nCurrency <> 1 Then
					.ErrorMessage(sCodispl, 55807)
					lblnError = True
				End If
			Else
				If nCurrency <> nCurrencyBul Then
					.ErrorMessage(sCodispl, 55807)
					lblnError = True
				End If
			End If
			
			
			'+ Se efectuan las validaciones correspondientes al monto a pagar
			If Not lblnError Then
				'+ Si se está registrando uno nuevo
				If UCase(sAction) = "ADD" Then
					'+ Si existe filtro por área de seguro se verifica que el documento pertenezca a dicha área.
					If nInsur_area > 0 Then
						'+ Solamente para el tipo de documento 1)Recibos, 2)Cuotas y 10)Interes por préstamo.
						If nCollecDocTyp = 1 Or nCollecDocTyp = 2 Or nCollecDocTyp = 10 Then
							'+ Si el campo recibo tiene valor
							If nReceipt >= 0 Then
								If nInsur_AreaReceipt <> eRemoteDB.Constants.intNull Then
									If nInsur_AreaReceipt <> nInsur_area Then
										.ErrorMessage(sCodispl, 60226)
										lblnError = True
									End If
								End If
							End If
						End If
					End If
					'+ Si el documento tiene asociado un boletín este se verifica que no este enviado a cobranzas (campo dSend_domic tenga valor)
				End If
			End If
			insValCO632AUpd = .Confirm
		End With
		
insValCO632AUpd_Err: 
		If Err.Number Then
			insValCO632AUpd = "insValCO632AUpd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValCO632A: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(CO632)
	Public Function InsValCO632A(ByVal sCodispl As String, ByVal nBulletins As Double, ByVal sStyle_Bull As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim llngError As Integer
		
		On Error GoTo InsValCO632A_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se obtiene el número de error de la validación en caso de haberla
			llngError = getValT_bulletins_det(nBulletins, sStyle_Bull)
			
			If llngError > 0 Then
				If llngError = 4281 Then
					.ErrorMessage(sCodispl, llngError,  , eFunctions.Errors.TextAlign.RigthAling, " (Recibo, Cuota y/o Intereses por préstamo)")
				Else
					'+ Se valida que los documentos esten en la misma moneda sólo cuando el tipo sea Origen
					.ErrorMessage(sCodispl, llngError)
				End If
			End If
			
			InsValCO632A = .Confirm
			
		End With
		
InsValCO632A_Err: 
		If Err.Number Then
			InsValCO632A = "InsValCO632: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostCO632A: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CO632)
	Public Function InsPostCO632AUpd(ByVal sAction As String, ByVal sSel As String, ByVal nBulletins As Double, ByVal nId As Integer, ByVal nCollecDocTyp As Integer, ByVal dCollectdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal sClient As String, ByVal dStatdate As Date, ByVal dExpirDat As Date, ByVal dLimitdate As Date, ByVal nType As Integer, ByVal nTratypei As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nCod_Agree As Integer, ByVal sIndColl_exp As String, ByVal sStyle_Bull As String, ByVal sQueryOption As String, ByVal sCollector As String, ByVal nInsur_area As Integer) As Boolean
		On Error GoTo InsPostCO632AUpd_Err
		
		With Me
			.sSel = sSel
			.nBulletins = nBulletins
			.nId = nId
			.nCollecDocTyp = nCollecDocTyp
			.dCollectdate = dCollectdate
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nReceipt = nReceipt
			.nDigit = nDigit
			.nPaynumbe = nPaynumbe
			.nContrat = nContrat
			.nDraft = nDraft
			.sClient = sClient
			.dStatdate = dStatdate
			.dExpirDat = dExpirDat
			.dLimitdate = dLimitdate
			.nType = nType
			.nTratypei = nTratypei
			.nCurrency = nCurrency
			.nAmount = nAmount
			.nCod_Agree = nCod_Agree
			.sIndColl_exp = sIndColl_exp
			.sStyle_Bull = sStyle_Bull
			.sQueryOption = sQueryOption
			.sCollector = sCollector
			.nInsur_area = nInsur_area
		End With
		
		Select Case sAction
			Case "Add"
				InsPostCO632AUpd = Add
			Case "Update"
				InsPostCO632AUpd = Update
			Case "Del"
				InsPostCO632AUpd = Delete
		End Select
		
InsPostCO632AUpd_Err: 
		If Err.Number Then
			InsPostCO632AUpd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsPostCO632A: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CO632)
	Public Function InsPostCO632A(ByVal nBulletins As Double, ByVal sQueryOption As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsUpdco632 As eRemoteDB.Execute
		
		On Error GoTo InsPostCO632A_Err
		
		lrecinsUpdco632 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdco632 al 02-18-2002 19:34:21
		'+
		With lrecinsUpdco632
			.StoredProcedure = "insUpdco632"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQueryoption", sQueryOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsPostCO632A = .Run(False)
			
		End With
		
InsPostCO632A_Err: 
		If Err.Number Then
			InsPostCO632A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdco632 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdco632 = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sSel = String.Empty
		nBulletins = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nCollecDocTyp = eRemoteDB.Constants.intNull
		dCollectdate = eRemoteDB.Constants.dtmNull
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nReceipt = eRemoteDB.Constants.intNull
		nDigit = eRemoteDB.Constants.intNull
		nPaynumbe = eRemoteDB.Constants.intNull
		nContrat = eRemoteDB.Constants.intNull
		nDraft = eRemoteDB.Constants.intNull
		sClient = String.Empty
		dStatdate = eRemoteDB.Constants.dtmNull
		dExpirDat = eRemoteDB.Constants.dtmNull
		dLimitdate = eRemoteDB.Constants.dtmNull
		nType = eRemoteDB.Constants.intNull
		nTratypei = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nCod_Agree = eRemoteDB.Constants.intNull
		sIndColl_exp = String.Empty
		sStyle_Bull = String.Empty
		sQueryOption = String.Empty
		sCollector = String.Empty
		nInsur_area = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nTotalDoc = eRemoteDB.Constants.intNull
		nTotalInterest = eRemoteDB.Constants.intNull
		nTotalColl_exp = eRemoteDB.Constants.intNull
		nTotalLoans = eRemoteDB.Constants.intNull
		nTotalGeneral = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%valExistsCO632_K: Verifica si existe información para procesar según condición de filtro de la transacción CO632_K.
	Public Function valExistsCO632_K(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal sClient As String, ByVal nInsur_area As Integer, ByVal nCurrency As Integer, ByVal sStyle_Bull As String) As Boolean
		Dim lrecvalExistsCO632_K As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsCO632_K_Err
		
		lrecvalExistsCO632_K = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure valExistsCO632_K al 02-09-2002 14:31:52
		'+
		With lrecvalExistsCO632_K
			.StoredProcedure = "valExistsCO632_K"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_Bull", sStyle_Bull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nExists").Value = 1 Then
					valExistsCO632_K = True
				End If
			End If
		End With
		
valExistsCO632_K_Err: 
		If Err.Number Then
			valExistsCO632_K = False
		End If
		'UPGRADE_NOTE: Object lrecvalExistsCO632_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalExistsCO632_K = Nothing
		On Error GoTo 0
	End Function
	
	'%insReaExistsCO632_K: Obtiene los datos de la condición a la hora de modificar o consultar la información de la transacción CO632.
	Public Function insReaExistsCO632_K(ByVal nAction As Integer, ByVal nBulletins As Double) As Boolean
		Dim lrecinsReaExistsCO632_K As eRemoteDB.Execute
		
		On Error GoTo insReaExistsCO632_K_Err
		
		lrecinsReaExistsCO632_K = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insReaExistsCO632_K al 02-09-2002 14:31:52
		'+
		With lrecinsReaExistsCO632_K
			.StoredProcedure = "reaExistsCO632_K"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nExists") = 1 Then
					insReaExistsCO632_K = True
					Me.nBulletins = nBulletins
					Me.dCollectdate = .FieldToClass("dCollectDate", eRemoteDB.Constants.dtmNull)
					Me.nBranch = .FieldToClass("nBranch", eRemoteDB.Constants.intNull)
					Me.nProduct = .FieldToClass("nProduct", eRemoteDB.Constants.intNull)
					Me.nPolicy = .FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
					Me.sClient = .FieldToClass("sClient", String.Empty)
					Me.nReceipt = .FieldToClass("nReceipt", eRemoteDB.Constants.intNull)
					Me.nInsur_area = .FieldToClass("nInsur_area", eRemoteDB.Constants.intNull)
					Me.sIndColl_exp = .FieldToClass("sIndColl_Exp", "0")
					Me.sStyle_Bull = .FieldToClass("sStyle_bull", "0")
					Me.sQueryOption = .FieldToClass("sQueryOption", "0")
					Me.sStatus = .FieldToClass("sStatus", String.Empty)
					Me.nCurrency = .FieldToClass("nCurrency", 1)
				End If
			End If
		End With
		
insReaExistsCO632_K_Err: 
		If Err.Number Then
			insReaExistsCO632_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsReaExistsCO632_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaExistsCO632_K = Nothing
		On Error GoTo 0
	End Function
	
	'------------------------------------------
	Public Function calTotalsBulletins() As Boolean
		'------------------------------------------
		Dim ldblGeneric As Double
		Dim lrecT_bulletins_det As eRemoteDB.Execute
		
		On Error GoTo Err_calTotalsBulletins
		
		lrecT_bulletins_det = New eRemoteDB.Execute
		
		With lrecT_bulletins_det
			.StoredProcedure = "calBulletinsAmounts"
			
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectDate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC1_2", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC8", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC9", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC10", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC11", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC12", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC13", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC14", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC15", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC16", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalDOC17", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			nTotalDoc = CDbl(.Parameters("nTotalDOC1_2").Value)
			nTotalInterest = CDbl(.Parameters("nTotalDOC8").Value)
			nTotalColl_exp = CDbl(.Parameters("nTotalDOC9").Value)
			nTotalLoans = CDbl(.Parameters("nTotalDOC10").Value)
			nTotalClient = CDbl(.Parameters("nTotalDOC17").Value)
			nTotalCtaIndiv = CDbl(.Parameters("nTotalDOC11").Value)
			nTotalReliqPrima = CDbl(.Parameters("nTotalDOC12").Value)
			nTotalBonoRecono = CDbl(.Parameters("nTotalDOC13").Value)
			nTotalComplBono = CDbl(.Parameters("nTotalDOC14").Value)
			nTotalPolitAdic = CDbl(.Parameters("nTotalDOC15").Value)
			nTotalPrimaPriv = CDbl(.Parameters("nTotalDOC16").Value)
			nTotalGeneral = nTotalDoc + nTotalInterest + nTotalColl_exp + nTotalLoans + nTotalClient + nTotalCtaIndiv + nTotalReliqPrima + nTotalBonoRecono + nTotalComplBono + nTotalPolitAdic + nTotalPrimaPriv
			
		End With
		
		'UPGRADE_NOTE: Object lrecT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_bulletins_det = Nothing
		
Err_calTotalsBulletins: 
		If Err.Number Then
			calTotalsBulletins = False
		End If
		On Error GoTo 0
	End Function
	
	'%getExistT_bulletins_det: Verifica si existe información seleccionada para el número de relación pasado como parámetro.
	Public Function getExistT_bulletins_det(ByVal nBulletins As Double, ByVal nCollecDocTyp As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal sClient As String) As Integer
		Dim lclsT_bulletins_det As eCollection.T_bulletins_det
		Dim lrecT_bulletins_det As eRemoteDB.Execute
		
		On Error GoTo getExistT_bulletins_det_Err
		
		lrecT_bulletins_det = New eRemoteDB.Execute
		
		getExistT_bulletins_det = -1
		
		With lrecT_bulletins_det
			.StoredProcedure = "reaT_bulletins_det"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBulletins", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecdoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sSel", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					If .FieldToClass("nBulletins") = nBulletins Then
						If .FieldToClass("sSel") = "1" Then
							getExistT_bulletins_det = 55734
						Else
							getExistT_bulletins_det = 55735
						End If
						Exit Do
					Else
						If .FieldToClass("sSel") = "1" Then
							getExistT_bulletins_det = 55802
							Exit Do
						End If
					End If
					.RNext()
				Loop 
			End If
		End With
		
getExistT_bulletins_det_Err: 
		If Err.Number Then
			getExistT_bulletins_det = -1
		End If
		'UPGRADE_NOTE: Object lrecT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_bulletins_det = Nothing
		On Error GoTo 0
	End Function
	
	'%getValT_bulletins_det: Obtiene el número de error dependiende de la validación masiva.
	Public Function getValT_bulletins_det(ByVal nBulletins As Double, ByVal sStyle_Bull As String) As Integer
		Dim lrecgetValT_bulletins_det As eRemoteDB.Execute
		Dim llngError As Integer
        Dim lstrCertype As String = ""
        Dim lintBranch As Integer
		Dim lintProduct As Integer
		Dim llngPolicy As Integer
		Dim lintCurrency As Integer
		Dim lintCod_agree As Integer
        Dim lstrClient As String = ""
        Dim ldblTotal As Double
		
		Dim lblnOneTime As Boolean
		Dim lblnOneTimeTyp As Boolean
		Dim lblnPolicy As Boolean
		Dim lblnClient As Boolean
		Dim lblnCod_agree As Boolean
		Dim lblnCurrency As Boolean
		Dim lblnOk As Boolean
		
		On Error GoTo getValT_bulletins_det_Err
		
		lrecgetValT_bulletins_det = New eRemoteDB.Execute
		
		getValT_bulletins_det = -1
		
		ldblTotal = 0
		'+
		'+ Definición de store procedure getValT_bulletins_det al 02-09-2002 14:31:52
		'+
		With lrecgetValT_bulletins_det
			.StoredProcedure = "reaT_bulletins_det"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCollecdoctyp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCertype", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDigit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nPaynumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nContrat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDraft", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lblnOk = True
					If Not lblnOneTime Then
						lblnOneTime = True
						lintCurrency = .FieldToClass("nCurrency")
					End If
					
					'+ Se verifica si hay cambio de moneda
					If lintCurrency <> .FieldToClass("nCurrency") Then
						lblnCurrency = True
					End If
					
					'+ Tratamiento del tipo de documento: Recibo(1) y Cuota(2, Saldo a Favor del cliente (17)
					If .FieldToClass("nCollecDocTyp") = 1 Or .FieldToClass("nCollecDocTyp") = 2 Or .FieldToClass("nCollecDocTyp") = 17 Or .FieldToClass("nCollecDocTyp") = 10 Then
						If Not lblnOneTimeTyp Then
							lstrCertype = .FieldToClass("sCertype")
							lintBranch = .FieldToClass("nBranch")
							lintProduct = .FieldToClass("nProduct")
							llngPolicy = .FieldToClass("nPolicy")
							lstrClient = .FieldToClass("sClient")
							lintCod_agree = .FieldToClass("nCod_agree", eRemoteDB.Constants.intNull)
							lblnOneTimeTyp = True
						End If
						
						'+ Se verifica si hay cambio de póliza
						If lstrCertype <> .FieldToClass("sCertype") Or lintBranch <> .FieldToClass("nBranch") Or lintProduct <> .FieldToClass("nProduct") Or llngPolicy <> .FieldToClass("nPolicy") Then
							lblnPolicy = True
						End If
						'+ Se verifica si hay cambio de cliente
						If lstrClient <> .FieldToClass("sClient") Then
							lblnClient = True
						End If
						
						'+ Se verifica si hay cambio de convenio
						If lintCod_agree <> .FieldToClass("nCod_agree", eRemoteDB.Constants.intNull) Then
							lblnCod_agree = True
						End If
					End If
					
					ldblTotal = ldblTotal + .FieldToClass("nAmount")
					.RNext()
				Loop 
			End If
		End With
		
		If lblnOk Then
			
			'+ Validación de la moneda
			If lblnCurrency And sStyle_Bull = "1" Then
				getValT_bulletins_det = 55807
			Else
				
				'+ Validación del tipo de boletín si es por póliza o por cliente.
				If lblnPolicy And lblnClient Then
					getValT_bulletins_det = 55806
				Else
					
					'+ Validación del convenio
					If lblnCod_agree Then
						getValT_bulletins_det = 55805
					End If
				End If
			End If
		End If
		
		'+ Validación del monto total del boletín.
		If ldblTotal <= 0 Then
			getValT_bulletins_det = 55804
		Else
			'+ Se debe seleccionar por lo menos un documento (Recibo, Cuota y/o Interes sobre préstamo)
			If Not lblnOneTimeTyp Then
				getValT_bulletins_det = 4281
			End If
		End If
		
getValT_bulletins_det_Err: 
		If Err.Number Then
			getValT_bulletins_det = False
		End If
		
		'UPGRADE_NOTE: Object lrecgetValT_bulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecgetValT_bulletins_det = Nothing
		On Error GoTo 0
	End Function
End Class






