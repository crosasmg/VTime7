Option Strict Off
Option Explicit On
Public Class T_ConcilClaim
	'%-------------------------------------------------------%'
	'% $Workfile:: T_ConcilClaim.cls                        $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public nReceipt As Double
	Public sCerType As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nDigit As Integer
	Public nPaynumbe As Integer
	Public sClient As String
	Public nStatus_pre As Integer
	Public dEffecdate As Date
	Public nBalance As Double
	Public nPremium As Double
	Public nCurrency As Integer
	Public sIndCheque As String
	Public nBordereaux As Integer
	Public nCashNum As Integer
	Public sDocnumbe As String
	Public nContrat As Double
	Public nDraft As Integer
	Public nCompany As Integer
	Public nBank_code As Integer
	Public nCheOpertyp As Integer
	Public sClaimTyp As String
	Public sMark As String
	
	Public sReceipt As String
	Public sBalance As String
	Public sSel As String
	Public sContrat As String
	Public sDraft As String
	
	'%Find_pendingpremium: Realiza la lectura de T_Concilclaim y obtiene el total de
	Public Function Find_pendingpremium(ByRef nClaim As Double) As Boolean
		Dim lrecT_ConcilClaim As eRemoteDB.Execute
		
		Static llngOldClaim As Double
		Static ldtmOldPayDate As Date
		
		Static lblnRead As Boolean
		
		On Error GoTo Find_pendingpremium_err
		
		Find_pendingpremium = False
		
		If llngOldClaim <> nClaim Then
			
			llngOldClaim = nClaim
			lrecT_ConcilClaim = New eRemoteDB.Execute
			
			With lrecT_ConcilClaim
				.StoredProcedure = "reaT_ConcilClaim_premium"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then 'Listo
					Me.nPremium = .FieldToClass("nPremium", 0)
					.RCloseRec()
					Find_pendingpremium = True
				End If
			End With
			
			lrecT_ConcilClaim = Nothing
		End If
Find_pendingpremium_err: 
		If Err.Number Then
			Find_pendingpremium = False
		End If
		On Error GoTo 0
		
	End Function
	'%   insValSI762: se realizan las validaciones del frame de los recibos a conciliar
	Public Function insValSI762(ByVal sCodispl As String, ByVal nCountSel As Integer, ByVal nBalance As Double) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSI762_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nCountSel = eRemoteDB.Constants.intNull Or nCountSel = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11380)
		End If
		
		insValSI762 = lclsErrors.Confirm
		
insValSI762_Err: 
		If Err.Number Then
			insValSI762 = insValSI762 & Err.Description
		End If
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostSI762: se actualizan los datos de la tabla t_ConcilClaim
	Public Function InsPostSI762(ByVal ldblClaim As Double, ByVal llngCase_num As Integer, ByVal llngDeman_type As Integer, ByVal lstrReceipt As String, ByVal lstrBalance As String, ByVal lstrSel As String, ByVal lstrContrat As String, ByVal lstrDraft As String, ByVal llngUserCode As Integer) As Boolean
		
		Dim lclsClaim_win As eClaim.Claim_win
		lclsClaim_win = New eClaim.Claim_win
		
		On Error GoTo InsPostSI762_Err
		
		nClaim = ldblClaim
		nCase_num = llngCase_num
		nDeman_type = llngDeman_type
		sReceipt = lstrReceipt
		sBalance = lstrBalance
		sSel = lstrSel
		sContrat = lstrContrat
		sDraft = lstrDraft
		InsPostSI762 = insUpdT_ConcilClaim
		
		'+ Actualiza el estado de la ventana
		If InsPostSI762 Then
			Call lclsClaim_win.Add_Claim_win(140305, "SI762", "2", llngUserCode)
		End If
		
		
		lclsClaim_win = Nothing
		
InsPostSI762_Err: 
		If Err.Number Then
			InsPostSI762 = False
		End If
		On Error GoTo 0
		
	End Function
	'+Actualiza los registros que fueron seleccionados enm la tabla T_ConcilClaim
	Public Function insUpdT_ConcilClaim() As Boolean
		Dim lrecT_ConcilClaim As eRemoteDB.Execute
		
		On Error GoTo insUpdT_ConcilClaim_Err
		lrecT_ConcilClaim = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insClaim_pay'
		'Información leída el 29/01/2001 6:26:35 PM
		With lrecT_ConcilClaim
			.StoredProcedure = "insUpdT_ConcilClaim"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReceipt", sReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBalance", sBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sContrat", sContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDraft", sDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdT_ConcilClaim = .Run(False)
			
		End With
		lrecT_ConcilClaim = Nothing
		
insUpdT_ConcilClaim_Err: 
		If Err.Number Then
			insUpdT_ConcilClaim = False
		End If
		On Error GoTo 0
	End Function
	
	'+Da por cobrados todos los registros seleccionados de la tabla T_ConcilClaim
	Public Function insPayT_ConcilClaim(ByVal ldblClaim As Double, ByVal llngCase_num As Integer, ByVal llngDeman_type As Integer, ByVal ldtmPayDate As Date, ByVal llngUserCode As Integer) As Boolean
		Dim lrecT_ConcilClaim As eRemoteDB.Execute
		
		On Error GoTo insPayT_ConcilClaim_Err
		lrecT_ConcilClaim = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insPay_T_ConcilClaim'
		'Información leída el 29/01/2001 6:26:35 PM
		With lrecT_ConcilClaim
			.StoredProcedure = "insPay_T_ConcilClaim"
			.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", llngCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", llngDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayDate", ldtmPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", llngUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPayT_ConcilClaim = .Run(False)
			
			
		End With
		lrecT_ConcilClaim = Nothing
		
insPayT_ConcilClaim_Err: 
		If Err.Number Then
			insPayT_ConcilClaim = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsExists: Verifica la existencia de recibos marcados en t_concilclaim
	Public Function InsExists(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecT_ConcilClaim As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsExists_Err
		
		nExists = 0
		InsExists = True
		
		lrecT_ConcilClaim = New eRemoteDB.Execute
		
		With lrecT_ConcilClaim
			.StoredProcedure = "FIND_T_CONCILCLAIM"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
		
		lrecT_ConcilClaim = Nothing
		
InsExists_Err: 
		If Err.Number Then
			InsExists = False
		End If
		On Error GoTo 0
	End Function
End Class






