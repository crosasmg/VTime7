Option Strict Off
Option Explicit On
Public Class Life_claim
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_claim.cls                           $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 22-02-13 11:47                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Public nClaim As Double 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public nCase_num As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nDeman_type As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nAdv_paymen As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public nCapital As Double 'decimal                                                                                                                          no                                  9           18    6     yes                                 (n/a)                               (n/a)
	Public nCla_li_typ As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public dEnd_date As Date 'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public nIn_lif_typ As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nIndemnity As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public dInit_date As Date 'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public nInterest As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public nMonth_amou As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public nSalvage As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public nTransac As Integer 'int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nIni_tran As Integer 'int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nLoc_out_am As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public nPayFreq As Integer
	Public nBalance As Integer
	
	
	
	'**-Auxuliaries properties
	'- Propiedades auxiliares
	Public nPaymen As Double
	Public nQuanMonth As Integer
	Public dDateIndem As Date
	Public sDateIndem As String
	
	Public bFound As Boolean
	Public sbrancht As String
	
	Public nCurrency As Integer
	
	Public nGrowth_RateI As Double
	Public nGrowth_RateE As Double
	
	Public Origins As Collection
	
	Public sAfp_trans_type As String
	Public nStay_bonus As Double
	Public nApv_capital As Double
	Public nApv_balance_bc2052 As Double
	Public nApv_balance_ac2052 As Double
	Public nTransf_amount As Double
	Public nApv_tax As Double
	Public nApv_benef_balance As Double
	Public nCoverCapital As Double
	Public nOption As Integer
	Public nAFP As Integer
	
	'**- Defined type for the claime type, values accourding to table210
	'- Tipo definido para los tipos de siniestro, valores según table210
	
	Public Enum enmClaim
		eDeath = 1
		eIncapacity = 2
		eDeath_acc = 3
		eDeath_traffic = 4
		eSurvival = 5
		eDissability = 6
	End Enum
	
	'**-Defined type for the indemnization type values accourding table211
	'- Tipo definido para los tipos de indemnización, valores según table211
	
	Public Enum enmIndem
		eNormal = 1
		ePension = 2
		eLife_pension = 3
		eReturn_pay_prem = 4
		eLiber_pay_prem = 5
		eReturn_all_prem = 6
		eRescue = 7
	End Enum
	
	'**%Find: Obtains the properties values of the class
	'%Find : Obtiene los valores de las propiedades de la clase
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecLife_claim As eRemoteDB.Execute
		
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lblnRead As Boolean
		
		On Error GoTo Find_Err
		
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_num Or lintOldDeman_type <> nDeman_type Or lblnFind Then
			
			lrecLife_claim = New eRemoteDB.Execute
			
			With lrecLife_claim
				.StoredProcedure = "reaLife_claim" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					lblnRead = True
					nCla_li_typ = .FieldToClass("nCla_li_typ")
					nIn_lif_typ = .FieldToClass("nIn_lif_typ")
					dInit_date = .FieldToClass("dInit_date")
					nInterest = .FieldToClass("nInterest")
					nMonth_amou = .FieldToClass("nMonth_amou")
					nAdv_paymen = .FieldToClass("nAdv_paymen")
					nSalvage = IIf(.FieldToClass("nSalvage") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nSalvage"))
					dEnd_date = .FieldToClass("dEnd_date")
					nTransac = .FieldToClass("nTransac")
					nIni_tran = .FieldToClass("nIni_tran")
					nCapital = .FieldToClass("nCapital")
					nIndemnity = .FieldToClass("nIndemnity")
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			lrecLife_claim = Nothing
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update_SI024: Makes the actualization process in the data base table
	'% Update_SI024: Realiza el proceso de actualizaciones en las tablas de la base de datos
	Public Function Update_SI024(ByVal dDecladat As Date, ByVal nMovement As Integer, ByVal dPosted As Date, ByVal nTransaction As Integer) As Boolean
		Dim lupdLife_cover As eRemoteDB.Execute
		
		On Error GoTo Update_SI024_Err
		lupdLife_cover = New eRemoteDB.Execute
		
		With lupdLife_cover
			.StoredProcedure = "insEndProcSI024" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDecladat", dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCla_li_typ", IIf(nCla_li_typ = 0, eRemoteDB.Constants.intNull, nCla_li_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIn_lif_typ", IIf(nIn_lif_typ = 0, eRemoteDB.Constants.intNull, nIn_lif_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_amou", nMonth_amou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdv_paymenUser", nAdv_paymen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSalvageUser", nSalvage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalUser", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnityUser", nIndemnity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", IIf(nPayFreq = 0, eRemoteDB.Constants.intNull, nPayFreq), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_SI024 = .Run(False) '**Ready
			'Listo
			
		End With
		
		lupdLife_cover = Nothing
		
Update_SI024_Err: 
		If Err.Number Then
			Update_SI024 = False
		End If
		On Error GoTo 0
	End Function
	'**% Update_ClaimDeath: Updates the death date of the insured  affected in the claim
	'% Update_ClaimDeath: Actualiza la fecha de muerte del asegurado afectado en el siniestro
	Public Function Update_ClaimDeath(ByVal dDeath_date As Date) As Boolean
		
		Dim lupdClaim_death As eRemoteDB.Execute
		
		On Error GoTo Update_ClaimDeath_Err
		
		lupdClaim_death = New eRemoteDB.Execute
		
		With lupdClaim_death '**Ready
			.StoredProcedure = "insUpdClaim_death" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDeath_Date", dDeath_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_ClaimDeath = .Run(False)
		End With
		
		lupdClaim_death = Nothing
		
Update_ClaimDeath_Err: 
		If Err.Number Then
			Update_ClaimDeath = False
		End If
		On Error GoTo 0
		
	End Function
	'**%CalculateSI024: Convert the capital amount and payment with local currency to a policy/certificate
	'**%currency
	'%CalculateSI024 : Convierte los importes de capital e indemnización de la moneda local a
	'%la moneda de la póliza/certificado
	Public Function CalculateSI024(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nReturn As Integer, ByVal nCapital As Double, ByVal nIndemnity As Double) As Boolean
		Dim lrecCalculate As eRemoteDB.Execute
		
		On Error GoTo CalculateSI024_Err
		
		lrecCalculate = New eRemoteDB.Execute
		
		With lrecCalculate '**Ready
			.StoredProcedure = "insCalculateSI024" 'Listo
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnity", nIndemnity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReturn", nReturn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				CalculateSI024 = True
				nCapital = .FieldToClass("nCapital")
				nIndemnity = .FieldToClass("nIndemnity")
			Else
				CalculateSI024 = False
			End If
		End With
		
		lrecCalculate = Nothing
		
CalculateSI024_Err: 
		If Err.Number Then
			CalculateSI024 = False
		End If
		On Error GoTo 0
		
	End Function
	'**%CalAmount: Calculate the amount to put in the window SI024.
	'%CalAmount: Calcula los importes a colocar en la ventana SI024.
	Public Function CalAmount(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCla_li_typ As Integer, ByVal nIndemn As Double, Optional ByVal nIndAdjustCapital As Integer = 0, Optional ByVal nAmountAdjustCapital As Double = 0) As Boolean
		Dim lrecLife_cover As New eRemoteDB.Execute
		
		On Error GoTo CalAmount_Err
		
		lrecLife_cover = New eRemoteDB.Execute
		
		With lrecLife_cover '**Ready
			.StoredProcedure = "insProcessSI024" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndCoveruse", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndOnlySelect", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCla_li_typ", IIf(nCla_li_typ = 0, eRemoteDB.Constants.intNull, nCla_li_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndFinalProcess", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnityUser", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIn_lif_typ", IIf(nIn_lif_typ = 0, eRemoteDB.Constants.intNull, nIn_lif_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_amou", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdv_paymenUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSalvageUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", IIf(nPayFreq = 0, eRemoteDB.Constants.intNull, nPayFreq), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateI", nGrowth_RateI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateE", nGrowth_RateE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndAdjustCapital", nIndAdjustCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountAdjustCapital", nAmountAdjustCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run Then
				nAdv_paymen = .FieldToClass("nAdv_paymen")
				nSalvage = IIf(.FieldToClass("nSalvage") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nSalvage"))
				nCapital = .FieldToClass("nCapital")
				'If nIndemn = 0 Then
				If .FieldToClass("nIndemnity") <= 0 Then
					nIndemnity = IIf(.FieldToClass("nCapital") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nCapital"))
				Else
					'If nCla_li_typ <> 2 And _
					''**Incapacity, Dissability
					'    nCla_li_typ <> 6 Then 'Incapacidad, Invalidez
					nIndemnity = .FieldToClass("nIndemnity")
					'End If
				End If
				nPaymen = .FieldToClass("nPaymen")
				nCurrency = .FieldToClass("nCurrency")
				CalAmount = True
			Else
				nAdv_paymen = 0
				nSalvage = 0
				nCapital = 0
				nIndemnity = 0
				nPaymen = 0
				CalAmount = False
				nCurrency = 0
			End If
		End With
		lrecLife_cover = Nothing
		
CalAmount_Err: 
		If Err.Number Then
			CalAmount = False
		End If
		On Error GoTo 0
	End Function
	
	'**%CalMonth_amo: Calculate the amount of a monthly pension to pay to the beneficiary
	'%CalMonth_amo: Calcula los importe de pensión mensual a pagar al beneficiario.
	Public Function CalMonth_amo(ByVal nClaim As Double, ByVal nPenType As Integer, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nInterest As Double, ByVal nIndemn As Double) As Boolean
		
		Dim lrecPension As eRemoteDB.Execute
		
		On Error GoTo CalMonth_amo_Err
		
		lrecPension = New eRemoteDB.Execute
		
		With lrecPension
			
			.StoredProcedure = "insCalMonth_amo" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nPenType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIni_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nPenType = 2 Then
				.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("dEnd_date", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnity", nIndemn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'**Ready
			If .Run Then 'Listo
				CalMonth_amo = True
				nMonth_amou = .FieldToClass("nAmount")
				nQuanMonth = .FieldToClass("nQuanMonth")
				nLoc_out_am = nMonth_amou * nQuanMonth
				.RCloseRec()
			Else
				CalMonth_amo = False
				nMonth_amou = 0
			End If
		End With
		lrecPension = Nothing
		
CalMonth_amo_Err: 
		If Err.Number Then
			CalMonth_amo = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValSI024: This function realize all the corresponding validations for the window fields
	'%insValSI024: En esta función se realizan las validaciones correspondientes a los campos
	'%de la ventana.
	Public Function insValSI024(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCla_li_typ As Integer, ByVal nIn_lif_typ As Integer, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nMonth_amo As Double, ByVal nIndemnity As Double, ByVal nInterest As Double, ByVal ldblIndemni As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nPayFreq As Integer, ByVal nRateDisability As Double, ByVal nGrowth_RateI As Double, ByVal nGrowth_RateE As Double, Optional ByVal nIndAdjustCapital As Integer = 0, Optional ByVal nAmountAdjustCapital As Double = 0) As String
		Dim lclsCertificat As ePolicy.Certificat
        Dim lclsProduct_Li As eProduct.Product  
		Dim lclsErrors As eFunctions.Errors
		Dim lblnOk As Boolean
		Dim ldblPaymen As Double
		Dim lblnFilled As Boolean
		Dim lstrError As String
		Dim lblnError As Boolean
		Dim lstrErrors As String
		Dim oLine As eClaim.Claim_origin
		Dim nLine As Short
        Dim nProdClas As Integer 

		On Error GoTo insValSI024_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nCla_li_typ <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 4153)
		Else
			If nIn_lif_typ <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 4155)
			End If
			'**Surviving
			'    If nCla_li_typ = eSurvival Then 'Supervivencia
			'        If Not ValRoutine(nClaim, nCase_Num, nDeman_Type, nCla_li_typ) Then
			'            Call lclsErrors.ErrorMessage(sCodispl, 4124)
			'        End If
			'    Else
			Select Case nCla_li_typ
				Case enmClaim.eIncapacity '**Incapacity
					'Incapacidad
					lstrError = "Incapacidad: "
				Case enmClaim.eDeath_acc '**Accident
					'Accidente
					lstrError = "Muerte en accidente: "
				Case enmClaim.eDeath_traffic '** Death traffic
					'Muerte en Circulación
					lstrError = "Muerte en circulación: "
				Case enmClaim.eDissability '**Dissability
					'Invalidez
					lstrError = "Invalidez: "
			End Select
			
			'         lblnOk = ValRoutine(nClaim, nCase_num, nDeman_type, nCla_li_typ)
			
			'         If Not lblnOk Then
			'             Call lclsErrors.ErrorMessage(sCodispl, 4154, , LeftAling, lstrError)
			'         Else
			lclsCertificat = New ePolicy.Certificat
			If nCla_li_typ = enmClaim.eIncapacity Then
				If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					If lclsCertificat.sExemption = "1" Then
						Call lclsErrors.ErrorMessage(sCodispl, 4360)
					End If
				End If
			Else
				If nIn_lif_typ <> 0 And nIn_lif_typ <> eRemoteDB.Constants.intNull Then
					If nIn_lif_typ = enmIndem.eLiber_pay_prem Then
						If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
							If lclsCertificat.sExemption = "1" Then
								Call lclsErrors.ErrorMessage(sCodispl, 4360)
							End If
						End If
					End If
					If CalAmount(nClaim, nCase_num, nDeman_type, nCla_li_typ, nIndemnity, nIndAdjustCapital , nAmountAdjustCapital) Then
						lblnFilled = True
					Else
						lblnFilled = False
					End If
					
					ldblPaymen = nPaymen
					
					If nIn_lif_typ <> 5 Then
						If nIndemnity <= 0 Then
							Call lclsErrors.ErrorMessage(sCodispl, 3959)
						End If
					End If
				End If
			End If
			' End If
			' End If
		End If
		If nIn_lif_typ = 2 Or nIn_lif_typ = 3 Then
			If nPayFreq = eRemoteDB.Constants.intNull Or nPayFreq = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 56165)
			End If
		End If
		
		If dInit_date = eRemoteDB.Constants.dtmNull And (nIn_lif_typ = 2 Or nIn_lif_typ = 3) Then '**Pension, pension during life'
			'Pensión, pensión vitalicia
			Call lclsErrors.ErrorMessage(sCodispl, 4160)
		End If
		
		If dEnd_date <> eRemoteDB.Constants.dtmNull Then
			If dInit_date <> eRemoteDB.Constants.dtmNull Then
				If dEnd_date < dInit_date Then
					Call lclsErrors.ErrorMessage(sCodispl, 7165)
				End If
			End If
		Else
			If dInit_date = eRemoteDB.Constants.dtmNull And nIn_lif_typ = 2 Then '**Pension
				'Pensión
				Call lclsErrors.ErrorMessage(sCodispl, 12082)
			End If
		End If
		
		If nMonth_amo = 0 Then
			If (nIn_lif_typ = 2 Or nIn_lif_typ = 3) Then '**Pension, pension during life'
				'Pensión, pensión vitalicia
				Call lclsErrors.ErrorMessage(sCodispl, 4188)
			End If
		End If
		
		If nIndemnity <> 0 Then

            lclsProduct_Li = New eProduct.Product 
            If lclsProduct_Li.FindProduct_li(nBranch,nProduct,Today) Then
                nProdClas = lclsProduct_Li.nProdClas  
            Else
                nProdClas = 0
            End If
            
            If nProdClas <> 4 Then
                If nIndemnity > nAmountAdjustCapital   Then 
				    Call lclsErrors.ErrorMessage(sCodispl, 4198)
			    End If
            End If

            If nCla_li_typ <> 2 And nCla_li_typ <> 6 Then
                If lblnFilled Then
                    If ldblPaymen > nIndemnity Then
                        Call lclsErrors.ErrorMessage(sCodispl, 4600)
                    End If
                End If
            End If
        End If

        If nInterest <> 0 Then
            If nInterest > 100 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1938)
            End If
        End If

        If nRateDisability <> 0 Then
            If nRateDisability > 100 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1938, , eFunctions.Errors.TextAlign.LeftAling, "% Indemnización: ")
            End If
        End If

        lstrErrors = InsValSI024DB(nClaim, nBranch, nProduct, nGrowth_RateI, nGrowth_RateE)
        If Len(lstrErrors) > 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)
        End If

        For Each oLine In Me.Origins
            nLine = nLine + 1
            If oLine.nTransf_percent > 100 Or oLine.nTransf_percent < 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 978091, nLine)
            End If
            If oLine.nTransf_amount > oLine.nVp Then
                Call lclsErrors.ErrorMessage(sCodispl, 978092, nLine)
            End If
        Next oLine

        insValSI024 = lclsErrors.Confirm

insValSI024_Err:
        If Err.Number Then
            insValSI024 = "insValSI024: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsCertificat = Nothing
	End Function
	
	'**%Valroutine: Validate that a coverage exists and has associated a particular routine
	'**%Only used in the data of the life claim
	'%ValRoutine: Valida que una cobertura exista y tenga asociada una rutina en particular.
	'% Usada sólo en datos del siniestro de vida
	Public Function ValRoutine(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCla_li_typ As Integer) As Boolean
		Dim lrecLife_cover As eRemoteDB.Execute
		
		On Error GoTo ValRoutine_Err
		lrecLife_cover = New eRemoteDB.Execute
		
		ValRoutine = False
		
		With lrecLife_cover
			
			.StoredProcedure = "insProcessSI024" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If nCla_li_typ = 5 Then
				.Parameters.Add("nIndCoveruse", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nIndCoveruse", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("nIndOnlySelect", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCla_li_typ", IIf(nCla_li_typ = 0, eRemoteDB.Constants.intNull, nCla_li_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Select Case nCla_li_typ
				Case 2 'Incapacidad
					.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", "sClincapi", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 3 'Accidente
					.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", "sClaccidi", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 4 'Circulación
					.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", "sClvehaci", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 5 'Supervivencia
					.Parameters.Add("sRoutineSurvii", "sClsurvii", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 6 'Invalidez
					.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", "sClinvali", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case Else
					.Parameters.Add("sRoutineSurvii", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClincapi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClvehaci", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClaccidi", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutineClinvali", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End Select
			.Parameters.Add("nIndFinalProcess", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnityUser", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIn_lif_typ", IIf(nIn_lif_typ = 0, eRemoteDB.Constants.intNull, nIn_lif_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nInterest", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_amou", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdv_paymenUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSalvageUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalUser", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", IIf(nPayFreq = 0, eRemoteDB.Constants.intNull, nPayFreq), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateI", nGrowth_RateI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateE", nGrowth_RateE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndAdjustCapital", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountAdjustCapital", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then 'Listo
				ValRoutine = True
				.RCloseRec()
			End If
		End With
		lrecLife_cover = Nothing
		
ValRoutine_Err: 
		If Err.Number Then
			ValRoutine = False
		End If
		
		On Error GoTo 0
	End Function
	'**% insPostSI024: Makes the final update in the data base table
	'% insPostSI024: Realiza las actualizaciones finales en las tablas de la base de datos
	Public Function insPostSI024(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCla_li_typ As Integer, ByVal nIn_lif_typ As Integer, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nInterest As Double, ByVal nMonth_amo As Double, ByVal nAdv_paymen As Double, ByVal nSalvage As Double, ByVal nCapital As Double, ByVal nIndemnity As Double, ByVal dDecladat As Date, ByVal nMovement As Integer, ByVal dPosted As Date, ByVal nTransaction As Integer, ByVal sbrancht As String, ByVal dOccurdat As Date, ByVal nUsercode As Integer, ByVal nPayFreq As Integer, ByVal nGrowth_RateI As Double, ByVal nGrowth_RateE As Double, Optional ByVal nIndAdjustCapital As Integer = 0, Optional ByVal nAmountAdjustCapital As Double = 0) As Boolean
		Dim lrecSI024 As eRemoteDB.Execute
		Dim lintValid As Short
		Dim oLine As eClaim.Claim_origin
		
		On Error GoTo InsPostSI024_err
		
		lrecSI024 = New eRemoteDB.Execute
		
		With lrecSI024
			.StoredProcedure = "insPostSI024"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCla_li_typ", nCla_li_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIn_lif_typ", nIn_lif_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_amo", nMonth_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdv_paymen", nAdv_paymen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSalvage", nSalvage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemnity", nIndemnity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDecladat", dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdat", dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateI", nGrowth_RateI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateE", nGrowth_RateE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverCapital", nCoverCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApv_balance_ac2052", nApv_balance_ac2052, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApv_balance_bc2052", nApv_balance_bc2052, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransf_amount", nTransf_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApv_tax", nApv_tax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApv_benef_balance", nApv_benef_balance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAFP", nAFP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStay_bonus", nStay_bonus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndAdjustCapital", nIndAdjustCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountAdjustCapital", nAmountAdjustCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostSI024 = .Run(False)
		End With
		
		
		For	Each oLine In Me.Origins
			With lrecSI024
				.StoredProcedure = "insPostSI024_Origins"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOrigin", oLine.nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransf_percent", oLine.nTransf_percent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransf_amount", oLine.nTransf_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTax_amount", oLine.nTax_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBalance", oLine.nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				insPostSI024 = .Run(False)
			End With
		Next oLine
		
		
InsPostSI024_err: 
		If Err.Number Then
			insPostSI024 = False
		End If
		On Error GoTo 0
		lrecSI024 = Nothing
	End Function
	
	'**%CalculateDateIndem: calculate the date to is going to make a indemnizate
	'%CalculateDateIndem  : Calcula la fecha hasta la cual se puede indemnizar a un beneficiario
	Public Function CalculateDateIndem(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecCalculate As eRemoteDB.Execute
		
		Dim ldblRentAmount As Double
		Dim ldtmDateRent As Date
		
		On Error GoTo CalculateDateIndem_Err
		
		lrecCalculate = New eRemoteDB.Execute
		
		ldblRentAmount = 0
		ldtmDateRent = Today
		
		With lrecCalculate
			.StoredProcedure = "INSCAL_RESEEM"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NRENTAMOUNT", ldblRentAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("DDATERENT", ldtmDateRent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				CalculateDateIndem = True
				'dDateIndem = Format(.Parameters.Item("DDATERENT").Value, "yyyy/MM/dd")
                dDateIndem = .Parameters.Item("DDATERENT").Value
			Else
				CalculateDateIndem = False
			End If
		End With
		
		lrecCalculate = Nothing
		
CalculateDateIndem_Err: 
		If Err.Number Then
			CalculateDateIndem = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPreSI024: Obtains the values of the transaction SI024.
	'%insPreSI024 : Obtiene los valores de de la transacción SI024
	Public Function insPreSI024(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecLife_claim As eRemoteDB.Execute
		Dim lintValue As Short
		Dim ldblValue As Double
		
		On Error GoTo insPreSI024_Err
		
		lrecLife_claim = New eRemoteDB.Execute
		
		insPreSI024 = True
		
		With lrecLife_claim
			.StoredProcedure = "insPreSI024" 'Listo
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run()
			
			bFound = .FieldToClass("nFound") = 1
			sbrancht = .FieldToClass("sBrancht")
			nAdv_paymen = .FieldToClass("nAdv_paymen")
			nCapital = .FieldToClass("nCapital")
			nCla_li_typ = .FieldToClass("nCla_li_typ")
			nIn_lif_typ = .FieldToClass("nIn_lif_typ")
			nIndemnity = .FieldToClass("nIndemnity")
			dInit_date = .FieldToClass("dInit_date")
			dEnd_date = .FieldToClass("dEnd_date")
			nInterest = .FieldToClass("nInterest")
			nMonth_amou = .FieldToClass("nMonth_amou")
			nPayFreq = .FieldToClass("nPayfreq")
			nSalvage = .FieldToClass("nSalvage")
			nCurrency = .FieldToClass("nCurrency")
			nGrowth_RateI = .FieldToClass("nGrowth_RateI")
			nGrowth_RateE = .FieldToClass("nGrowth_RateE")
			Me.sAfp_trans_type = .FieldToClass("sAfp_trans_type")
			Me.nStay_bonus = .FieldToClass("nStay_bonus")
			Me.nApv_capital = .FieldToClass("nApv_capital")
			Me.nApv_balance_bc2052 = .FieldToClass("nApv_balance_bc2052")
			Me.nApv_balance_ac2052 = .FieldToClass("nApv_balance_ac2052")
			Me.nTransf_amount = .FieldToClass("nTransf_amount")
			Me.nApv_tax = .FieldToClass("nApv_tax")
			Me.nApv_benef_balance = .FieldToClass("nApv_benef_balance")
			Me.nOption = .FieldToClass("nOption")
			Me.nAFP = .FieldToClass("nAFP")
			Me.nCoverCapital = .FieldToClass("nCoverCapital")
		End With
		
insPreSI024_Err: 
		If Err.Number Then
			insPreSI024 = False
		End If
		On Error GoTo 0
		lrecLife_claim = Nothing
	End Function
	'**%CalcLoans_Reserv: Calculate the amount to put in the window SI024.
	'%CalcLoans_Reserv: Calcula los importes a colocar en la ventana SI024.
	Public Function CalcLoans_Reserv(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecLoans_cover As New eRemoteDB.Execute
		
		On Error GoTo CalcLoans_Reserv_Err
		
		lrecLoans_cover = New eRemoteDB.Execute
		
		With lrecLoans_cover
			.StoredProcedure = "Inscalcloans_reserv"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			If .Run(False) Then
				CalcLoans_Reserv = True
				nBalance = .Parameters("nBalance").Value
			Else
				CalcLoans_Reserv = False
			End If
			
		End With
		lrecLoans_cover = Nothing
		
CalcLoans_Reserv_Err: 
		If Err.Number Then
			CalcLoans_Reserv = False
		End If
		On Error GoTo 0
	End Function
	
	'%Objetivo: Esta función permite realizar validaciones con acceso a la base de datos.
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    sPolitype    - Tipo de póliza
	'%    sCodispl     - Código de la ventana (lógico)
	'%    nCurrency    - Código de la moneda
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    nStage       - Número que identifica la etapa
	'%    dEffecdate   - Fecha efectiva del registro
	'%    dDestindat   - Fecha de llegada al lugar de destino
	'%    dOrigindat   - Fecha de salida del lugar de origen
	'%    nTyproute    - Tipo de ruta asegurada
	'%    nTransptype  - Tipo de transporte
	'%    sName_licen  - Nombre o matrícula del medio de transporte
	'%    sOrigen      - Ciudad origen de la ruta
	'%    sDestination - Ciudad destino de la ruta
	Public Function InsValSI024DB(ByVal nClaim As Double, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGrowth_RateI As Double, ByVal nGrowth_RateE As Double) As String
		
		Dim lrecSI024 As eRemoteDB.Execute
		InsValSI024DB = String.Empty
		
		On Error GoTo InsValSI024DB_err
		
		lrecSI024 = New eRemoteDB.Execute
		
		With lrecSI024
			.StoredProcedure = "ValTransSI024"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateI", nGrowth_RateI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowth_RateE", nGrowth_RateE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrorList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValSI024DB = Trim(.Parameters("sErrorList").Value)
			End If
		End With
		
InsValSI024DB_err: 
		If Err.Number Then
			InsValSI024DB = CStr(False)
		End If
		On Error GoTo 0
		lrecSI024 = Nothing
	End Function
	
	
	
	'% FindChildren: Se verifica la existencia de información relacionada al siniestro-caso
	Public Function FindOrigins(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer, ByVal sClient As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsOrigin As Claim_origin
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaClaimOrigin"
			.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindOrigins = True
				Do While Not .EOF
					lclsOrigin = New Claim_origin
					lclsOrigin.nClaim = .FieldToClass("nClaim")
					lclsOrigin.nCase_num = .FieldToClass("nCase_num")
					lclsOrigin.nDeman_type = .FieldToClass("nDeman_type")
					lclsOrigin.nOrigin = .FieldToClass("nOrigin")
					lclsOrigin.nTax_benefit = .FieldToClass("nTax_benefit")
					lclsOrigin.dValuedate = .FieldToClass("dValuedate")
					lclsOrigin.nVp = .FieldToClass("nVp")
					lclsOrigin.nTransf_percent = .FieldToClass("nTransf_percent")
					lclsOrigin.nTransf_amount = .FieldToClass("nTransf_amount")
					lclsOrigin.nTax_amount = .FieldToClass("nTax_amount")
					lclsOrigin.nBalance = .FieldToClass("nBalance")
					lclsOrigin.nExchange = .FieldToClass("nExchange")
					lclsOrigin.sOriginDescript = .FieldToClass("sOriginDescript")
                    lclsOrigin.sOriginAttributes = .FieldToClass("sOriginAttributes")
                    lclsOrigin.nOrigin_Account = .FieldToClass("nOrigin_Account")
					
					Call Me.Origins.Add(lclsOrigin)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		lclsRemote = Nothing
		lclsOrigin = Nothing
	End Function


	Private Sub Class_Initialize_Renamed()
		Me.Origins = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






