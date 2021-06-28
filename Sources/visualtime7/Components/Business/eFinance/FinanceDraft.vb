Option Strict Off
Option Explicit On
Public Class FinanceDraft
	'%-------------------------------------------------------%'
	'% $Workfile:: FinanceDraft.cls                         $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 7/10/04 5:11p                                $%'
	'% $Revision:: 62                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla insudb.financ_dra al 04-04-2002 13:46:10
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nAmount As Double ' NUMBER     22   2     10   S
	Public nStat_draft As Integer ' NUMBER     22   0     5    S
	Public nAmount_net As Double ' NUMBER     22   2     10   S
	Public nClaim As Double ' NUMBER     22   0     10   S
	Public nCommission As Double ' NUMBER     22   2     10   S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nContrat As Double ' NUMBER     22   0     10   N
	Public nDraft As Integer ' NUMBER     22   0     5    N
	Public dExpirdat As Date ' DATE       7    0     0    S
	Public nIntammou As Double ' NUMBER     22   2     10   S
	Public nStatPrint As Integer ' NUMBER     22   0     5    S
	Public nIntermed As Integer ' NUMBER     22   0     10   S
	Public dStat_date As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nAmo_afec As Double ' NUMBER     22   2     10   S
	Public nAmo_exen As Double ' NUMBER     22   2     10   S
	Public nIva As Double ' NUMBER     22   2     10   S
	Public dLimitdate As Date ' DATE       7    0     0    N
	Public nBulletins As Integer ' NUMBER     22   0     10   S
	Public nBordereaux As Integer ' NUMBER     22   0     10   S
	Public dCollsus_ini As Date ' DATE       7    0     0    S
	Public dCollsus_end As Date ' DATE       7    0     0    S
	Public nSus_reason As Integer ' NUMBER     22   0     5    S
	Public sSus_origi As String ' CHAR       1    0     0    S
	Public sIndcheque As String ' CHAR       1    0     0    S
	Public nCollector As Double ' NUMBER     22   0     10   S
	Public nCom_afec As Double ' NUMBER     22   2     10   S
	Public nCom_exen As Double ' NUMBER     22   2     10   S
	Public nWay_Pay As Integer
	
	'+ Recordset definition. This recordset will be used in the class
	'+ Se define el recordset que será utilizado en la clase
	
	Private lrecreaFinanc_dra As eRemoteDB.Execute
	
	'- Variable definition. This variable determines the class status
	'- Se define la variable que determina el estado de la clase
	
	'- MUST BE IN FINANCE_PRE
	'- DEBE IR EN FINANCE_PRE
	Public Enum eStatusInstance
		eftNew = 0
		eftQuery = 1
		eftExist = 1
		eftUpDate = 2
		eftDelete = 3
	End Enum
	
	'- Variable definition. This variable will contain the frequency
	'- Se define la variable que va a contener la frecuencia.
	
	'- MUST BE IN DRAFTHIST
	'- DEBE IR EN DRAFTHIST
	Public Enum eTypeMove
		etmCreation = 1
		etmPayment = 2
		etmRevertPayment = 3
		etmCancel = 4
		etmRevertCancel = 5
		etmUpdPayment = 6
	End Enum
	
	Private lrecreaFinanc_dra_all As eRemoteDB.Execute
	Public nStatInstanc As eStatusInstance
	
	'- Auxiliary properties
	'- Propiedades Auxiliares
	Public nCurrency As Integer
	Public nType As eTypeMove
	Public nExpensive As Double
	Public nInterest As Double
	Public nDscto_pag As Double
	Public sIntermName As String
	Public sClient As String
	Public sCliename As String
	Public sDesCurrency As String
	Public sDesOffice As String
	Public sDesStatusDraft As String
	Public sDesWay_Pay As String
	
	'- Enumerated type definition. Identifies the draft status
	'- Se define el tipo enumerado que identifica el estado del giro.
	Public Enum eStat_Draft
		esdOutStatnding = 1
		esdCollect = 2
		esdCancelled = 3
		esdCollectClaimDiscount = 4
		esdRefinanced = 5
	End Enum
	
	'- Enumerated type definition. This will contain the contract status
	'- Se define el tipo enumerado que contiene el estado del contrato.
	Public Enum eStatPrint
		espPrint = 1
		espOutStatndingPrint = 2
	End Enum
	
	'- Enumerated type definition. This will contain the pay form of the down payment
	'- Se define el tipo enumerado que contiene la forma de pago de la cuota inicial
	Public Enum ePayType
		eptRealCash = 1
		eptBanc = 2
		eptExchange = 3
		eptChqCash = 4
	End Enum
	
	'- Enumerated type definition. This will contain the draft frequency (Monthly or quarterly)
	'- Se define el tipo enumerado que contiene la frecuencia de los giros (Mensual o Trimestral)
	Public Enum eFrecType
		eptMonthly = 1
		eptQuarterly = 3
	End Enum
	
	'- Variable definition. This variable will hold the loan balance after the draft is payed
	'- Variable que permitira almacenar el saldo del préstamo una vez pagado el giro
	Public nBalance As Double
	
	Public nNumber As Double
	Public nInitial As Double
	
	Public nOffice As Integer
	
	Public nReceipt As Double
	Public sDigit As String
	
	Public sStat_draft As String
	
	'% CountDraft: This function verifies if there are some paid drafts
	'% CountDraft: Esta función se encarga de verificar si existe algún giro pagado.
	Public Function CountDraft(ByVal nContrat As Double, ByVal nStat_draft As eStat_Draft) As Boolean
		Dim lrecValFinanc_dra_Stat As eRemoteDB.Execute
		
		On Error GoTo CountDraft_Err
		lrecValFinanc_dra_Stat = New eRemoteDB.Execute
		
		CountDraft = False
		
		'+ Stored procedure parameters definition 'insudb.ValFinanc_dra_Stat'
		'+ Data of 09/10/1999 02:28:46 PM
		'+ Definición de parámetros para stored procedure 'insudb.ValFinanc_dra_Stat'
		'+ Información leída el 10/09/1999 02:28:46 PM
		With lrecValFinanc_dra_Stat
			.StoredProcedure = "ValFinanc_dra_Stat"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				CountDraft = .Parameters("nExists").Value = 1
			End If
		End With
		
CountDraft_Err: 
		If Err.Number Then
			CountDraft = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecValFinanc_dra_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValFinanc_dra_Stat = Nothing
	End Function
	
	'% Find: se leen los datos asociados a una cuota del contrato
	Public Function Find(ByVal nContrat As Double, ByVal nDraft As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		On Error GoTo Find_Err
		
		If Me.nContrat <> nContrat Or Me.nDraft <> nDraft Or bFind Then
			
			lrecreaFinanc_dra = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_dra'
			'+ Información leída el 30/08/1999 10:15:09 AM
			With lrecreaFinanc_dra
				.StoredProcedure = "reaFinanc_dra"
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nAmount = .FieldToClass("nAmount")
					nStat_draft = .FieldToClass("nStat_draft")
					nAmount_net = .FieldToClass("nAmount_net")
					nClaim = .FieldToClass("nClaim")
					nCommission = .FieldToClass("nCommission")
					dCompdate = .FieldToClass("dCompdate")
					Me.nContrat = .FieldToClass("nContrat")
					Me.nDraft = .FieldToClass("nDraft")
					dExpirdat = .FieldToClass("dExpirdat")
					nIntammou = .FieldToClass("nIntammou")
					nStatPrint = .FieldToClass("nStatPrint")
					nIntermed = .FieldToClass("nIntermed")
					dStat_date = .FieldToClass("dStat_date")
					nAmo_afec = .FieldToClass("nAmo_afec")
					nAmo_exen = .FieldToClass("nAmo_exen")
					nIva = .FieldToClass("nIva")
					dLimitdate = .FieldToClass("dLimitDate")
					nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
					nBordereaux = .FieldToClass("nBordereaux", eRemoteDB.Constants.intNull)
					dCollsus_ini = .FieldToClass("dCollsus_ini")
					dCollsus_end = .FieldToClass("dCollsus_end")
					nSus_reason = .FieldToClass("nSus_reason")
					sSus_origi = .FieldToClass("sSus_origi")
					sIndcheque = .FieldToClass("sIndcheque")
					nCollector = .FieldToClass("nCollector", eRemoteDB.Constants.intNull)
					nCom_afec = .FieldToClass("nCom_afec")
					nCom_exen = .FieldToClass("nCom_exen")
					nCurrency = .FieldToClass("nCurrency")
					nOffice = .FieldToClass("nOffice", eRemoteDB.Constants.intNull)
					nDscto_pag = .FieldToClass("nDscto_pag")
					nExpensive = .FieldToClass("nExpensive")
					nInterest = .FieldToClass("nInterest")
					nType = .FieldToClass("nType")
					sIntermName = .FieldToClass("sCliename")
					sClient = .FieldToClass("sClient")
					sDigit = .FieldToClass("sDigit")
					nReceipt = .FieldToClass("nReceipt")
					nWay_Pay = .FieldToClass("nWay_Pay")
					
					'+variables usadas en el reverso de cobro (CO09)
					sDesCurrency = .FieldToClass("SDES_CURRENCY")
					sDesOffice = .FieldToClass("SDES_OFFICE")
					sDesStatusDraft = .FieldToClass("SDES_STATUS")
					nStatInstanc = eStatusInstance.eftExist
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFinanc_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinanc_dra = Nothing
	End Function
	'% UpdnStat_draft: Updates the status of a draft to "Pending"
	'% UpdnStat_draft: Actualiza unicamente el estado el un Giro a pendiente
	Function UpdnStat_draft(ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nStat_draft As Integer, ByVal nUsercode As Integer) As Boolean
		If Find(nContrat, nDraft) Then
			Me.nStat_draft = nStat_draft
			Me.nUsercode = nUsercode
			UpdnStat_draft = Me.UpDate
		End If
		
	End Function
	'% insFirst_Draft: First draft processing
	'% insFirst_Draft: Tratamiento del primer giro
	Public Function insFirst_Draft(ByVal dEffecdate As Date, ByVal nFrequency As Integer, ByVal nQ_draft As Double) As Date
		Dim nFrequen As Integer
		
		'+ First draft expiration date validations
		'+ Validación del vencimiento del primer giro.
		Select Case nFrequency
			Case financeCO.eFrequency.efMonthly
				nFrequen = 1 * nQ_draft
			Case financeCO.eFrequency.efQuarterly
				nFrequen = 3 * nQ_draft
			Case financeCO.eFrequency.efNot_Stand
				nFrequen = 0
		End Select
		
		'+ This variable contains the expiration date of the last draft
		'+ Esta variable contiene la fecha de vencimiento del último giro.
		insFirst_Draft = DateAdd(Microsoft.VisualBasic.DateInterval.Month, nFrequen, dEffecdate)
	End Function
	
	'% Update: This method is in charge of updating records in the table "Finance_Draft".  It returns TRUE or FALSE
	'% depending on whether the stored procedure executed correctly.
	'% Update: Este método se encarga de actualizar registros en la tabla "Finance_Draft". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpDate() As Boolean
		Dim lrecupdFinance_Draft As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecupdFinance_Draft = New eRemoteDB.Execute
		
		'+ Stored procedure parameters definition 'insudb.updFinance_Draft'
		'+ Data of 14/09/1999 05:55:48 PM
		'+ Definición de parámetros para stored procedure 'insudb.updFinance_Draft'
		'+ Información leída el 14/09/1999 05:55:48 PM
		
		With lrecupdFinance_Draft
			.StoredProcedure = "updFinance_Draft"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_net", nAmount_net, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntammou", nIntammou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatPrint", nStatPrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Afec", nAmo_afec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Exen", nAmo_exen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", nIva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimitDate", dLimitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollsus_Ini", dCollsus_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollsus_End", dCollsus_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSus_Reason", nSus_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSus_Origi", sSus_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndCheque", sIndcheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_Afec", nCom_afec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_Exen", nCom_exen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpDate = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			UpDate = False
		End If
		'UPGRADE_NOTE: Object lrecupdFinance_Draft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFinance_Draft = Nothing
		On Error GoTo 0
	End Function
	
	'% Add: This function adds a new draft to the contract
	'% Add: Esta función se encarga de agregar un nuevo giro al contrato.
	Public Function Add() As Boolean
		Dim lreccreFinanc_dra As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreFinanc_dra = New eRemoteDB.Execute
		'+ Stored procedure parameters definition 'insudb.creFinanc_dra'
		'+ Data of 10/28/1999 14:30:03
		'+ Definición de parámetros para stored procedure 'insudb.creFinanc_dra'
		'+ Información leída el 28/10/1999 14:30:03
		With lreccreFinanc_dra
			.StoredProcedure = "creFinanc_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_net", nAmount_net, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntammou", nIntammou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatPrint", nStatPrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Afec", nAmo_afec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Exen", nAmo_exen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", nIva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimitDate", dLimitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollsus_Ini", dCollsus_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollsus_End", dCollsus_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSus_Reason", nSus_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSus_Origi", sSus_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndCheque", sIndcheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_Afec", nCom_afec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_Exen", nCom_exen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreFinanc_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFinanc_dra = Nothing
		On Error GoTo 0
	End Function
	
	'% Delete: se elimina una cuota asociada al contrato
	Public Function Delete() As Boolean
		Dim lRecDelFinanc_dra As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lRecDelFinanc_dra = New eRemoteDB.Execute
		
		With lRecDelFinanc_dra
			.StoredProcedure = "delFinance_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lRecDelFinanc_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecDelFinanc_dra = Nothing
	End Function
	'% Frecuency: Determines the frequency of the drafts (monthly or quarterly)
	'% Frecuency: Determina la frecuencia de los giros (Mensual o Trimestral)
	Public Function Frecuency(ByVal nFrequency As Integer) As Integer
		
		Select Case nFrequency
			Case 2
				Frecuency = eFrecType.eptMonthly
			Case 3
				Frecuency = eFrecType.eptQuarterly
		End Select
	End Function
	
	'% insValFI004: This method validates the page "FI004" as described in the functional specifications
	'% InsValFI004: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'% de la ventana "FI004"
	Public Function insValFI004(ByVal sCodispl As String, ByVal nContrat As Double, ByVal dEffecdate As Date) As String
		Dim lclsFinanceCO As financeCO
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValFI004_Err
		
		lclsFinanceCO = New financeCO
		lclsErrors = New eFunctions.Errors
		
		Call lclsFinanceCO.Find(nContrat, dEffecdate)
		
		'+ No se pueden generar Giros si el Total a financiar es igual a cero (0)
		If lclsFinanceCO.nAmount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 21150)
		End If
		
		insValFI004 = lclsErrors.Confirm
		
insValFI004_Err: 
		If Err.Number Then
			insValFI004 = "insValFI004: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	'% insPostFI004. This method updates the database (as described in the functional specifications)
	'% for the page "FI004"
	'% insPostFI004: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'% especificaciones funcionales)de la ventana "FI004"
	Public Function insPostFI004(ByVal sCodispl As String, ByVal nIndex As Integer, ByVal nIndicator As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nStat_draft As Integer, ByVal nAmount As Double, ByVal nAmount_net As Double, ByVal nCommission As Double, ByVal dExpirdat As Date, ByVal nIntermed As Integer, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal nWay_Pay As Integer, Optional ByVal nAmo_afec As Double = 0, Optional ByVal nAmo_exen As Double = 0, Optional ByVal nIva As Double = 0, Optional ByVal dLimitdate As Date = #12:00:00 AM#, Optional ByVal nCom_afec As Double = 0, Optional ByVal nCom_exen As Double = 0) As Boolean
		'- Defines the class used in the method
		'- Se definen las clase usadas en el método
		Dim lclsFinanceCO As financeCO
		Dim lclsFinanceWin As FinanceWin
		Dim lclsDraftHist As DraftHist
		Dim lclsFinanceDraft As FinanceDraft
		
		'- Variables definition. These Variables are used for the calculation of the drafts
		'- Se definen las variables usadas para el cálculo de los giros
		Dim lintIndexInterest As Integer
		Dim ldblInterest As Double
		Dim ldblIntammou As Double
		
		lclsFinanceCO = New financeCO
		lclsFinanceWin = New FinanceWin
		lclsFinanceDraft = New FinanceDraft
		
		ldblInterest = 0
		
		On Error GoTo insPostFI004_Err
		
		Call Find(nContrat, nDraft)
		
		Call lclsFinanceCO.Find(nContrat, dEffecdate)
		
		ldblIntammou = nAmount - nAmount_net
		
		With Me
			If nIndicator = eStatusInstance.eftNew Then
				.nContrat = lclsFinanceCO.nContrat
				.nDraft = nDraft
				.nStat_draft = nStat_draft
				.nAmount = nAmount
				.nAmount_net = nAmount_net
				.nClaim = nClaim
				.nCommission = nCommission
				.dExpirdat = dExpirdat
				.nIntammou = ldblIntammou
				.nIntermed = nIntermed
				.nStatPrint = eStatPrint.espOutStatndingPrint
				.dStat_date = lclsFinanceCO.dEffecdate
				.nUsercode = nUsercode
				.nAmo_afec = nAmo_afec
				.nAmo_exen = nAmo_exen
				.nIva = nIva
				.dLimitdate = dLimitdate
				.nBulletins = nBulletins
				.nBordereaux = nBordereaux
				.dCollsus_ini = dCollsus_ini
				.dCollsus_end = dCollsus_end
				.nSus_reason = nSus_reason
				.sSus_origi = sSus_origi
				.sIndcheque = sIndcheque
				.nCollector = nCollector
				.nCom_afec = nCom_afec
				.nCom_exen = nCom_exen
				.nType = eTypeMove.etmCreation
				.nCurrency = lclsFinanceCO.nCurrency
				.nExpensive = 0
				.nInterest = 0
				.nDscto_pag = 0
				.nWay_Pay = nWay_Pay
				insPostFI004 = Add
				
			ElseIf nIndicator = eStatusInstance.eftUpDate Then 
				.nContrat = lclsFinanceCO.nContrat
				.nDraft = nDraft
				.nStat_draft = nStat_draft
				.nAmount = nAmount
				.nAmount_net = nAmount_net
				.nClaim = nClaim
				.nCommission = nCommission
				.dExpirdat = dExpirdat
				.nIntammou = ldblIntammou
				.nIntermed = nIntermed
				.nStatPrint = eStatPrint.espOutStatndingPrint
				.dStat_date = lclsFinanceCO.dEffecdate
				.nUsercode = lclsFinanceCO.nUsercode
				.nAmo_afec = nAmo_afec
				.nAmo_exen = nAmo_exen
				.nIva = nIva
				.dLimitdate = dLimitdate
				.nBulletins = nBulletins
				.nBordereaux = nBordereaux
				.dCollsus_ini = dCollsus_ini
				.dCollsus_end = dCollsus_end
				.nSus_reason = nSus_reason
				.sSus_origi = sSus_origi
				.sIndcheque = sIndcheque
				.nCollector = nCollector
				.nCom_afec = nCom_afec
				.nCom_exen = nCom_exen
				.nType = eTypeMove.etmUpdPayment
				.nCurrency = lclsFinanceCO.nCurrency
				.nExpensive = 0
				.nInterest = 0
				.nDscto_pag = 0
				
				insPostFI004 = UpDate
				
			Else
				insPostFI004 = True
				
			End If
			
			'+ Updates the values in the table draft_hist
			'+ Se actualizan los valores en la tabla Draft_Hist.
			If insPostFI004 Then
				If nIndicator = eStatusInstance.eftNew Or nIndicator = eStatusInstance.eftUpDate Then
					lclsDraftHist = New DraftHist
					lclsDraftHist.nContrat = .nContrat
					lclsDraftHist.nDraft = .nDraft
					lclsDraftHist.nAmount = .nAmount
					lclsDraftHist.nCurrency = .nCurrency
					lclsDraftHist.nDscto_pag = .nDscto_pag
					lclsDraftHist.nExpensive = .nExpensive
					lclsDraftHist.nInterest = .nInterest
					lclsDraftHist.dStat_date = .dStat_date
					lclsDraftHist.TypeMove = .nType
					lclsDraftHist.nUsercode = .nUsercode
					lclsDraftHist.nCommit = 1
					
					insPostFI004 = lclsDraftHist.Add
					'UPGRADE_NOTE: Object lclsDraftHist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDraftHist = Nothing
				End If
			End If
			
			If nDraft = 1 Then
				If Find(nContrat, nDraft) Then
					lclsFinanceCO.nInitial = .nAmount
					lclsFinanceCO.nInitial_or = .nAmount
					insPostFI004 = lclsFinanceCO.UpDate
				End If
			End If
			
		End With
		
		If insPostFI004 Then
			lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, sCodispl, "2", nUsercode, nTransaction)
		Else
			lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, sCodispl, "1", nUsercode, nTransaction)
		End If
		
		
insPostFI004_Err: 
		If Err.Number Then
			insPostFI004 = False
		End If
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceWin = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValFI011: se realizan las validaciones de la ventana
	Public Function insValFI011(ByVal sCodispl As String, ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nDraft As Integer, ByVal nAmount As Double, ByVal dExpirdat As Date, ByVal dPrevExpirdat As Date, ByVal nLengthArray As Integer, ByVal nIndex As Integer) As String
		Dim lclsFinanceCO As financeCO
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValFI011_Err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceCO = New financeCO
		
		With lclsErrors
			If dExpirdat = eRemoteDB.Constants.dtmNull Then
				'+ La fecha de vencimiento debe estar llena
				Call .ErrorMessage(sCodispl, 21048)
			End If
			
			If lclsFinanceCO.Find(nContrat, dEffecdate) Then
				'+ La fecha de vencimiento debe ser mayor a la fecha de efecto del contrato
				If lclsFinanceCO.dEffecdate >= dExpirdat Then
					Call .ErrorMessage(sCodispl, 21036)
				End If
				
				If nLengthArray > 0 Then
					If nIndex <> 0 Then
						'+ La fecha de vencimiento debe ser posterior a la fecha de vencimiento de la línea anterior
						If dExpirdat <= dPrevExpirdat Then
							Call .ErrorMessage(sCodispl, 21049)
						End If
					End If
				End If
				
				'+ La fecha de vencimiento debe ser menor o igual a la fecha de vencimiento del contrato
				If DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, lclsFinanceCO.dEffecdate) < dExpirdat Then
					Call .ErrorMessage(sCodispl, 21050)
				End If
			End If
			
			If nAmount = eRemoteDB.Constants.intNull Then
				'+ El monto del giro debe estar lleno
				Call .ErrorMessage(sCodispl, 21051)
			End If
			
			insValFI011 = lclsErrors.Confirm
		End With
		
insValFI011_Err: 
		If Err.Number Then
			insValFI011 = "insValFI011: " & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insValFI014_K: This method validates the header section of the page "FI014_K" as described in the
	'% functional specifications
	'% InsValFI014_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'% descritas en el funcional de la ventana "FI014_K"
	Public Function insValFI014_K(ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nCause As Integer, ByVal dOpe_date As Date, ByVal nCompany As Integer) As String
		Dim errorNumber As Integer
		Dim lclsErrors As eFunctions.Errors
		Dim lclsFinanceCO As financeCO
		Dim lclsGeneral As eGeneral.Ctrol_date
		Dim lclsLedge As Object
		
		On Error GoTo insValFI014_K_err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceCO = New financeCO
		lclsGeneral = New eGeneral.Ctrol_date
		lclsLedge = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_Compan")
		
		
		'+ Validación del Número de Contrato
		'+ Verifica que el campo este lleno
		If nContrat = 0 Or Fix(nContrat) = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI014", 21062)
		Else
			'+ Verifica que el contarto este registrado
			If Not lclsFinanceCO.Find_Contrat(nContrat) Then
				lclsErrors.ErrorMessage("FI014", 21002)
			Else
				'+ Verifica que el contrato este en VIGOR
				Select Case lclsFinanceCO.nStat_contr
					Case financeCO.Estat_contr.Eannul
						errorNumber = 21005
						
					Case financeCO.Estat_contr.Eincompletecapture
						errorNumber = 21134
				End Select
				
				If errorNumber <> 0 Then
					lclsErrors.ErrorMessage("FI014", errorNumber)
				End If
				
			End If
		End If
		'+ Draft number validation
		'+ Validación del Número de Giro
		
		
		If nDraft <> eRemoteDB.Constants.intNull Then
			'+ Sends the warning message about the collected next drafts
			'+ Manda el mensaje de advertencia referente a giros posteriores cobrados
			If ValAfterDraft(nContrat, nDraft, eStat_Draft.esdCollect) Then
				lclsErrors.ErrorMessage("FI014", 21067)
			End If
			'+ Verifies that the draft is registered in the drafts file
			'+ Verifica que el giro esta registrado en el archivo de giros
			If Not Find(nContrat, nDraft) Then
				lclsErrors.ErrorMessage("FI014", 21041)
			Else
				'+ The draft must be collected
				'+ El giro debe estar cobrado
				If Me.nStat_draft <> eStat_Draft.esdCollect And Me.nStat_draft <> eStat_Draft.esdCollectClaimDiscount Then
					lclsErrors.ErrorMessage("FI014", 21066)
				End If
			End If
		Else
			lclsErrors.ErrorMessage("FI014", 21063)
		End If
		
		'+ Reverse reason validations
		'+ Validacion de la Causa de Reverso
		
		If nCause = 0 Or nCause = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI014", 21070)
		End If
		
		'+ Reverse date validations
		'+ Validación de la Fecha de Reverso
		
		If dOpe_date = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage("FI014", 21068)
			'+ The date must be equal or greater than the collect date
			'+ Validar que la fecha sea posterior o igual a la fecha de cobro
		Else
			'+ The date must be equal or minor than today
			'+ La fecha debe ser menor o igual a la fecha del día en curso
			If dOpe_date > Today Then
				lclsErrors.ErrorMessage("FI014", 1965)
			Else
				If dOpe_date < Me.dStat_date Then
					lclsErrors.ErrorMessage("FI014", 21069)
				End If
				
				'+ Validates the reverse date in reference to the date of the begining of the standing accounting period
				'+ Se valida la fecha de reverso con respecto a la de inicio del período contable en vigor
				If lclsGeneral.Find(1) Then
					If dOpe_date < lclsGeneral.dEffecdate Then
						lclsErrors.ErrorMessage("FI004", 1006)
					End If
				End If
				'+ The reverse date must be greater than the date of the last automatic entries process.
				'+ Validar que la fecha de reverso sea posterior al último proceso
				'+ de asientos automáticos. gintCompany es traida por la funcion
				'+ insGetRegistry()
				If lclsLedge.Find_Date_Init(nCompany) Then
					If dOpe_date < lclsLedge.dDate_init Then
						lclsErrors.ErrorMessage("FI014", 1008)
					End If
				End If
			End If
		End If
		
		insValFI014_K = lclsErrors.Confirm
		
insValFI014_K_err: 
		If Err.Number Then
			insValFI014_K = "insValFI014_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lclsLedge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedge = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostFI014. This method updates the database (as described in the functional specifications)
	'% for the page "FI014"
	'% insPostFI014: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'% especificaciones funcionales)de la ventana "FI014"
	Public Function insPostFI014(ByVal nContrat As Double, ByVal nDraft As Integer, ByVal dEffecdate As Date, ByVal dOpe_date As Date, ByVal nCurr_cont As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinanceDraft As FinanceDraft
		Dim lclsRefinanceDraft As RefinanceDraft
		Dim lclsFinanceCO As financeCO
		Dim lclsFinancePre As FinancePre
		
		On Error GoTo insPostFI014_err
		
		lclsFinanceDraft = New FinanceDraft
		lclsRefinanceDraft = New RefinanceDraft
		lclsFinanceCO = New financeCO
		lclsFinancePre = New FinancePre
		
		'+ Change the status of the draft to "PENDING" besides a "reverse of collect" movement must be in the table draft_hist
		'+ cambiar el estado del giro a PENDIENTE además debe aparecer un movimiento de "Reverso de Cobro" en draft_hist
		With lclsFinanceDraft
			Call .Find(nContrat, nDraft)
			.nContrat = nContrat
			.nDraft = nDraft
			.nType = eTypeMove.etmRevertPayment
			.nUsercode = nUsercode
			.nStat_draft = eStat_Draft.esdOutStatnding
			insPostFI014 = .UpDate
		End With
		
		'+ If it is the last draft the system assigns the status "STANDING" to the contract
		'+ Si es la ultima cuota se pasa el estado del contrato a "EN VIGOR"
		With lclsFinanceCO
			Call .Find_Contrat(nContrat)
			If .nQ_draft = nDraft Then
				.nStat_contr = financeCO.Estat_contr.Evigour
				.UpDate()
			End If
			If .nQ_draft = 0 Then
				.nStat_contr = financeCO.Estat_contr.Einitialwait
				If .UpDate Then
					
					'+ If it is a reverse of the collect of the down payment the system must change the
					'+ status of the premium invoices to "Pending"
					'+ Si se reverso la cuota inicial hay que colocar los recibos nuevamente
					'+ como pendientes de cobro
					
					'+ Change the premium invoices status to "Pending"
					'+ Se cambia el estado de los recibos a Pendientes en caso de existir
					With lclsFinancePre
						.nContrat = nContrat
						.nUsercode = nUsercode
						.nStat_draft = eStat_Draft.esdOutStatnding
						.ReverseCollectPremium()
					End With
					
					'+ Change the status of the refinanced drafts
					'+ Se cambia el estado de los giros refinanciados en caso de existir
					With lclsRefinanceDraft
						.nContrat = nContrat
						.nStatus_pre = eStat_Draft.esdOutStatnding
						.nUsercode = nUsercode
						.dExpirdat = dOpe_date
						.nCurrency = nCurr_cont
						.ChangeStatus()
					End With
				End If
			End If
		End With
		
insPostFI014_err: 
		If Err.Number Then
			insPostFI014 = False
		End If
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRefinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
		On Error GoTo 0
	End Function
	
	'% ValAfterDraft: This function validates if there are drafts with a specific status
	'%                and with a posterior expiration date
	'% ValAfterDraft: Esta función se encarga de validar si existen giros con un estado
	'%                posteriores a un giro dado para un contrato determinado
	Public Function ValAfterDraft(ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nStat As Integer) As Boolean
		
		Dim lrecvalMaxDraft As eRemoteDB.Execute
		
		On Error GoTo ValAfterDraft_Err
		lrecvalMaxDraft = New eRemoteDB.Execute
		ValAfterDraft = False
		
		'+ Stored procedure parameters definition 'insudb.valDraft_collect'
		'+ Data of 09/10/1999 02:28:46 PM
		'+ Definición de parámetros para stored procedure 'insudb.valDraft_collect'
		'+ Información leída el 10/09/1999 02:28:46 PM
		
		With lrecvalMaxDraft
			.StoredProcedure = "valMaxDraft"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValAfterDraft = .FieldToClass("nCount") > 0
				.RCloseRec()
			End If
		End With
		
ValAfterDraft_Err: 
		If Err.Number Then
			ValAfterDraft = False
		End If
		'UPGRADE_NOTE: Object lrecvalMaxDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalMaxDraft = Nothing
	End Function
	
	'% ValBeforeDraft: This function validates if there are drafts with a specific status
	'%                and with a previous expiration date
	'% ValBeforeDraft: Esta función se encarga de validar si existen giros con un estado
	'%                 anteriores a un giro dado para un contrato determinado
	Public Function ValBeforeDraft(ByVal nContrat As Double, ByVal nQ_draft As Double, ByVal nStatus As Integer) As Boolean
		Dim lrecvalDraft_collect As eRemoteDB.Execute
		
		lrecvalDraft_collect = New eRemoteDB.Execute
		
		On Error GoTo ValBeforeDraft_Err
		
		'+ Stored procedure parameters definition 'insudb.valDraft_collect'
		'+ Data of 06/22/2001 01:57:42 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.valDraft_collect'
		'+ Información leída el 22/06/2001 01:57:42 p.m.
		
		With lrecvalDraft_collect
			.StoredProcedure = "valDraft_collect"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nQ_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValBeforeDraft = .FieldToClass("nCount") > 0
				.RCloseRec()
			End If
		End With
		
ValBeforeDraft_Err: 
		If Err.Number Then
			ValBeforeDraft = False
		End If
		'UPGRADE_NOTE: Object lrecvalDraft_collect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDraft_collect = Nothing
	End Function
	
	'% insPostFI011: se realizan las actualizaciones de la ventana
	Public Function insPostFI011(ByVal sAction As String, ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nCurrency As Integer, ByVal nDraft As Integer, ByVal dExpirdat As Date, ByVal nAmount As Double, ByVal nAmount_net As Double, ByVal nIntammou As Double, ByVal nInitial As Double, ByVal nUsercode As Integer, ByVal nWay_Pay As Integer) As Boolean
		Dim lclsFinanceCO As financeCO
		Dim lclsFinanceWin As FinanceWin
		Dim lclsDraft_hist As DraftHist
		Dim lcolFinance_draf As FinanceDrafts
		Dim lblnUpdFW As Boolean
		Dim lstrContent As String
		
		On Error GoTo insPostFI011_Err
		
		Call Find(nContrat, nDraft)
		
		With Me
			.nContrat = nContrat
			.nDraft = nDraft
			.nStat_draft = eStat_Draft.esdOutStatnding
			.nAmount = nAmount
			.nAmount_net = nAmount_net
			.dExpirdat = dExpirdat
			.nIntammou = nIntammou
			.nStatPrint = eStatPrint.espOutStatndingPrint
			.dStat_date = dEffecdate
			.nUsercode = nUsercode
			.nType = eTypeMove.etmCreation
			.nCurrency = nCurrency
			.dLimitdate = dExpirdat
			.nWay_Pay = nWay_Pay
			
			lstrContent = "2"
			
			If sAction = "Add" Then
				If .Add Then
					lclsDraft_hist = New DraftHist
					'+ Se crea el movimiento inicial en la historia de la cuota
					lclsDraft_hist.nContrat = nContrat
					lclsDraft_hist.nDraft = nDraft
					lclsDraft_hist.nAmount = nAmount
					lclsDraft_hist.nCurrency = nCurrency
					lclsDraft_hist.nType = 1
					lclsDraft_hist.nUsercode = nUsercode
					lclsDraft_hist.nCommit = 1
					If lclsDraft_hist.Add Then
						lblnUpdFW = True
						insPostFI011 = True
					End If
					'UPGRADE_NOTE: Object lclsDraft_hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDraft_hist = Nothing
				End If
				
			ElseIf sAction = "Update" Then 
				insPostFI011 = .UpDate
				'+Se marca financ_win por si staba sin contenido
				lblnUpdFW = True
				
			ElseIf sAction = "Del" Then 
				If .Delete Then
					lcolFinance_draf = New FinanceDrafts
					insPostFI011 = True
					If lcolFinance_draf.Find(nContrat) Then
						'+ Si sólo queda la cuota inicial, se marca sin contenido, ya que ésta no se muestra
						'+ en la página
						If lcolFinance_draf.Count = 1 Then
							lblnUpdFW = True
							lstrContent = "1"
						End If
						'UPGRADE_NOTE: Object lcolFinance_draf may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lcolFinance_draf = Nothing
					Else
						lblnUpdFW = True
						lstrContent = "1"
					End If
				End If
			End If
		End With
		
		If insPostFI011 Then
			lclsFinanceCO = New financeCO
			With lclsFinanceCO
				If .Find(nContrat, dEffecdate) Then
					.nInitial = nInitial
					.nStat_contr = financeCO.Estat_contr.Eincompletecapture
					insPostFI011 = .UpDate
				End If
			End With
			'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanceCO = Nothing
			
			If lblnUpdFW And insPostFI011 Then
				lclsFinanceWin = New FinanceWin
				
				insPostFI011 = lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI011", lstrContent, nUsercode, nTransaction)
				
				'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsFinanceWin = Nothing
			End If
		End If
		
insPostFI011_Err: 
		If Err.Number Then
			insPostFI011 = False
		End If
		On Error GoTo 0
	End Function
	'% CalAmount_ToFinance:This routine calculates the balance and then moves the result to the tcnNumber1 control
	'% CalAmount_ToFinance:Este procedimieto se encarga de realizar el cálculo de saldo, luego se coloca
	'% en el control tcnNumber1
	Public Sub CalAmount_ToFinance(ByVal bCalcInitial As Boolean, ByVal nIndex As Integer, ByVal nContrat As Double, ByVal nInitial As Double, ByVal dEffecdate As Date)
		
		'- Variable definition. This variable is used to calculate the interest rate to be applied to the down payment
		'- Se define la variable para calcular el interés a aplicar a la cuota inicial
		Dim ldblInterest As Double
		
		'- Variable definition. This variable is used to calculate the interest amount of the down payment
		'- Se define la variable paa calcular el importe de interés de la cuota inicial
		Dim ldblIntammou As Double
		
		'- Variable definition. This variable will contain the total amount
		'- Se define la variable que va a contener la suma del importe
		
		Dim ldblAmount As Double
		
		Dim lintIndex As Integer
		
		Dim lclsFinanceCO As financeCO
		Dim lcolFinanceDrafts As FinanceDrafts
		
		'- Variable defintion. This variable is used to know if the quantity of drafts that were indicated in the FI001 window was reached
		'- Se define la variable que indica si se llegó a la cantidad de giros indicada en FI001.
		Dim lblnQFI001 As Boolean
		
		'- Variable definition. Indicates if the "last draft calculated" process can be executed
		'- Se define la variable que indica si se puede ejecutar el proceso de "último giro calculado"
		Dim lblnLast_Calc As Boolean
		
		lclsFinanceCO = New financeCO
		lcolFinanceDrafts = New FinanceDrafts
		
		nIndex = nIndex + 1
		
		Call lclsFinanceCO.Find(nContrat, dEffecdate)
		Call lcolFinanceDrafts.Find(nContrat)
		
		If nIndex = lclsFinanceCO.nQ_draft Then
			lblnQFI001 = True
			lblnLast_Calc = True
		ElseIf nIndex = lclsFinanceCO.nQ_draft - 1 Then 
			lblnLast_Calc = True
		End If
		
		'+ Assigns the value of the row to the variable
		'+ Se asigna el valor de la fila a la variable
		
		If bCalcInitial Then
			For lintIndex = 2 To lcolFinanceDrafts.Count
				ldblAmount = ldblAmount + lcolFinanceDrafts.Item(lintIndex).nAmount
			Next 
			
			If lblnQFI001 Then
				
				'+ If the interest are paid in the down payment
				'+ Si los intereses se pagan con la cuota inicial
				If lclsFinanceCO.sPayment_in = financeCO.EPayment_in.eafirmative Then
					ldblInterest = lclsFinanceCO.CalInterest(lclsFinanceCO.dEffecdate, lcolFinanceDrafts.Item(1).dExpirdat)
					ldblIntammou = ldblAmount * ldblInterest
					If ldblAmount <= lclsFinanceCO.nAmount Then
						nInitial = lclsFinanceCO.nAmount - ldblAmount + ldblIntammou
						'Else
						'    pmnuTime.ObjErrors.ErrorMessage "FI011", 21156, lintIndex
					End If
				Else
					If ldblAmount < lclsFinanceCO.nAmount Then
						nInitial = lclsFinanceCO.nAmount - ldblAmount
					ElseIf ldblAmount > lclsFinanceCO.nAmount Then 
						'    pmnuTime.ObjErrors.ErrorMessage "FI011", 21156, lintIndex
					Else
						nInitial = lclsFinanceCO.nInitial
					End If
				End If
				nNumber = 0
			Else
				If (lclsFinanceCO.nAmount - (ldblAmount + nInitial) > 0) Then
					nNumber = lclsFinanceCO.nAmount - (ldblAmount + nInitial)
				ElseIf lclsFinanceCO.nAmount - ldblAmount > 0 Then 
					nInitial = lclsFinanceCO.nAmount - ldblAmount
					nNumber = lclsFinanceCO.nAmount - (ldblAmount + nInitial)
				Else
					'pmnuTime.ObjErrors.ErrorMessage "FI011", 21156, lintIndex
				End If
			End If
		Else
			If lblnLast_Calc Then
				nNumber = 0
			Else
				For lintIndex = 2 To lcolFinanceDrafts.Count
					If Not lintIndex = lclsFinanceCO.nQ_draft - 1 Then
						ldblAmount = ldblAmount + lcolFinanceDrafts.Item(lintIndex).nAmount
					End If
				Next 
				nNumber = lclsFinanceCO.nAmount - (ldblAmount + nInitial)
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lcolFinanceDrafts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFinanceDrafts = Nothing
	End Sub
	
	'% getDraft_Old: Obtiene el número del giro más antiguo del contrato pasado como parámetro.
	Public Function getDraft_Old(ByVal nContrat As Double) As Integer
		Dim lclsT_DocTyp As eCollection.T_DocTyp
		
		Dim lrecT_DocTyp As eRemoteDB.Execute
		
		On Error GoTo getDraft_Old_Err
		
		lrecT_DocTyp = New eRemoteDB.Execute
		
		getDraft_Old = -1
		
		With lrecT_DocTyp
			.StoredProcedure = "reaFinanc_dra_Old"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				getDraft_Old = .FieldToClass("nDraft")
			End If
			
		End With
		
getDraft_Old_Err: 
		If Err.Number Then
			getDraft_Old = -1
		End If
		'UPGRADE_NOTE: Object lrecT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_DocTyp = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Financ_CO675: Obtiene el primer número del giro en estado pendiente
	Public Function Find_Financ_CO675(ByVal nContrat As Double) As Boolean
		Dim lrecFind_Financ_CO675 As eRemoteDB.Execute
		
		On Error GoTo Find_Financ_CO675_Err
		
		lrecFind_Financ_CO675 = New eRemoteDB.Execute
		
		With lrecFind_Financ_CO675
			.StoredProcedure = "REAFINANC_DRA_CO675"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nDraft = .Parameters.Item("nDraft").Value
				Find_Financ_CO675 = True
			End If
		End With
		
Find_Financ_CO675_Err: 
		If Err.Number Then
			Find_Financ_CO675 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFind_Financ_CO675 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFind_Financ_CO675 = Nothing
	End Function
	
	'% Find_Commission_FI004: Recupera la comisión de las cuotas a refinanciar
	Public Function Find_Commission_FI004(ByVal nContrat As Double) As Boolean
		Dim lrecFind_Commission_FI004 As eRemoteDB.Execute
		
		On Error GoTo Find_Commission_FI004_Err
		
		lrecFind_Commission_FI004 = New eRemoteDB.Execute
		
		With lrecFind_Commission_FI004
			.StoredProcedure = "REAFINANC_DRA_FI004"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_afec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCom_exen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nCom_afec = .Parameters.Item("nCom_afec").Value
				nCom_exen = .Parameters.Item("nCom_exen").Value
				Find_Commission_FI004 = True
			End If
		End With
		
Find_Commission_FI004_Err: 
		If Err.Number Then
			Find_Commission_FI004 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFind_Commission_FI004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFind_Commission_FI004 = Nothing
	End Function
	
	'% ValIntervdraft: Esta función se encarga de validar si existen giros cobrados dentro
	'%                 del intervalo dado por lintnFirst_draft y lintnLast_draft
	Public Function ValIntervdraft(ByVal llngContrat As Double, ByVal lintnFirst_draft As Integer, ByVal lintnLast_draft As Integer, ByVal lintStat_draft As Integer) As Boolean
		Dim lrecvalInterv_draft As eRemoteDB.Execute
		
		lrecvalInterv_draft = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.valInterv_draft'
		'+ Información leída el 10/09/1999 02:28:46 PM
		With lrecvalInterv_draft
			.StoredProcedure = "valInterv_draft"
			.Parameters.Add("nContrat", llngContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirst_draft", lintnFirst_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLast_draft", lintnLast_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", lintStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nCount") > 0 Then
					ValIntervdraft = True
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalInterv_draft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalInterv_draft = Nothing
	End Function
	'% UpdDraft: Realiza la modificación del encargado de cobro para un intervalo de giros dado
	Public Function UpdDraft(ByVal nContrat As Double, ByVal nFirstDraft As Integer, ByVal nLastDraft As Integer, ByVal nType As eTypeMove, ByVal nUsercode As Integer, Optional ByVal nIntermed As Integer = 0, Optional ByVal nCommission As Double = 0, Optional ByVal nAmountx As Double = 0, Optional ByVal nsStatDraft As eStat_Draft = 0, Optional ByVal nClaim As Double = 0) As Boolean
		Dim lrecupdCollectAgent As eRemoteDB.Execute
		
		lrecupdCollectAgent = New eRemoteDB.Execute
		
		UpdDraft = False
		
		'+ Definición de parámetros para stored procedure 'insudb.updCollectAgent'
		'+ Información leída el 21/09/1999 11:39:01 AM
		With lrecupdCollectAgent
			.StoredProcedure = "updCollectAgent"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstDraft", nFirstDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastDraft", nLastDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountx", nAmountx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatDraft", nsStatDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdDraft = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCollectAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCollectAgent = Nothing
	End Function
	
	'% Delete_All: se eliminan todas las cuotas asociadas al contrato
	Public Function Delete_All(ByVal nContrat As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Delete_All_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "delFinance_dra_All"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_All = .Run(False)
		End With
		
Delete_All_err: 
		If Err.Number Then
			Delete_All = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% reaSumUpdDraftPeriod: función que devuelve la sumatoria de los importes de los giros
	'                        que se pasen como parametros
	Public Function reaSumUpdDraftPeriod(ByVal nContrat As Double, ByVal nFirstDraft As Integer, ByVal nLastDraft As Integer, Optional ByVal nDscto_amo As Integer = 0, Optional ByVal nDscto_pag As Double = 0) As Double
		Dim lrecreaSumDraftPeriod As eRemoteDB.Execute
		
		lrecreaSumDraftPeriod = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaSumDraftPeriod'
		'+ Información leída el 17/09/1999 12:01:40 PM
		With lrecreaSumDraftPeriod
			.StoredProcedure = "reaSumDraftPeriod"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstDraft", nFirstDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastDraft", nLastDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDscto_amo", nDscto_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDscto_pag", nDscto_pag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaSumUpdDraftPeriod = .FieldToClass("nAmount")
				.RCloseRec()
			Else
				reaSumUpdDraftPeriod = 0
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSumDraftPeriod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSumDraftPeriod = Nothing
	End Function
	'% Class_Initialize: Initializes the properties of the class
	'% Class_Initialize: Inicializa las propiedades de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAmount = eRemoteDB.Constants.intNull
		nStat_draft = eRemoteDB.Constants.intNull
		nAmount_net = eRemoteDB.Constants.intNull
		nClaim = eRemoteDB.Constants.intNull
		nCommission = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nContrat = eRemoteDB.Constants.intNull
		nDraft = eRemoteDB.Constants.intNull
		dExpirdat = eRemoteDB.Constants.dtmNull
		nIntammou = eRemoteDB.Constants.intNull
		nStatPrint = eRemoteDB.Constants.intNull
		nIntermed = eRemoteDB.Constants.intNull
		dStat_date = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nAmo_afec = eRemoteDB.Constants.intNull
		nAmo_exen = eRemoteDB.Constants.intNull
		nIva = eRemoteDB.Constants.intNull
		dLimitdate = eRemoteDB.Constants.dtmNull
		nBulletins = eRemoteDB.Constants.intNull
		nBordereaux = eRemoteDB.Constants.intNull
		dCollsus_ini = eRemoteDB.Constants.dtmNull
		dCollsus_end = eRemoteDB.Constants.dtmNull
		nSus_reason = eRemoteDB.Constants.intNull
		sSus_origi = ""
		sIndcheque = ""
		nCollector = eRemoteDB.Constants.intNull
		nCom_afec = eRemoteDB.Constants.intNull
		nCom_exen = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find_Co634: busca el monto a pagar y la moneda de la última cuota a pagada o de la
	'%             próxima cuota a pagar de un contrato
	Public Function Find_Co634(ByVal nContrat As Double, ByVal nStat_draft As eStat_Draft) As Boolean
		On Error GoTo Find_Co634
		
		Dim lrecreaFinanc_dra_Co634 As eRemoteDB.Execute
		
		lrecreaFinanc_dra_Co634 = New eRemoteDB.Execute
		
		With lrecreaFinanc_dra_Co634
			.StoredProcedure = "reaFinanc_dra_Co634"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nAmount = .FieldToClass("nAmount")
				nDraft = .FieldToClass("nDraft")
				nCurrency = .FieldToClass("nCurrency")
				Find_Co634 = True
				.RCloseRec()
			Else
				Find_Co634 = False
			End If
		End With
		
Find_Co634: 
		If Err.Number Then
			Find_Co634 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFinanc_dra_Co634 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinanc_dra_Co634 = Nothing
	End Function
	
	'% InsValDate_Draft: Esta función se encarga de verificar si existe alguna cuota con fecha
	'%                   fuera de la vigencia de la poliza
	Public Function InsValDate_Draft(ByVal nContrat As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nQuotaCer As Short) As String
		Dim lrecInsValDate_Draft As eRemoteDB.Execute
		
		On Error GoTo InsValDate_Draft_Err
		lrecInsValDate_Draft = New eRemoteDB.Execute
		
		With lrecInsValDate_Draft
			.StoredProcedure = "InsValDate_Draft"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuota_Cer", nQuotaCer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDraft", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValDate_Draft = .Parameters("sDraft").Value
			Else
				InsValDate_Draft = ""
			End If
		End With
		
InsValDate_Draft_Err: 
		If Err.Number Then
			InsValDate_Draft = ""
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsValDate_Draft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValDate_Draft = Nothing
	End Function
End Class






