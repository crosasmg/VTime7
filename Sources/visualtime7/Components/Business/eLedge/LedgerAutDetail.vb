Option Strict Off
Option Explicit On
Public Class LedgerAutDetail
	'%-------------------------------------------------------%'
	'% $Workfile:: LedgerAutDetail.cls                      $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 16/11/04 3:57p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Auxiliar variable to store date is defined.
	'- Definición de variable auxiliar para almacenar fecha.
	
	Public dEffecdate As Date
	Public P_SKEY As String
	
	'**%insLedAutClaim: This function is in charge of call the automatic posting premium process
	'%  insLedAutClaim: Esta función se encarga de llamar al proceso de asientos automáticos
	'%  de siniestros.
	Public Function insLedAutClaim(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutClaim As eRemoteDB.Execute
		
		lrecLedAutClaim = New eRemoteDB.Execute
		
		insLedAutClaim = False
		
		With lrecLedAutClaim
			.StoredProcedure = "InsLedAutClaim"
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutClaim = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutClaim = Nothing
	End Function
	
	'**%insLedAutCurr_acc: This function is in charge of call the automatic posting current_account process
	'%  insLedAutClaim: Esta función se encarga de llamar al proceso de asientos automáticos
	'%  de cuentas corrientes.
	Public Function insLedAutCurr_acc(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutCurr_acc As eRemoteDB.Execute
		
		lrecLedAutCurr_acc = New eRemoteDB.Execute
		
		insLedAutCurr_acc = False
		
		With lrecLedAutCurr_acc
			.StoredProcedure = "InsLedAutCurr_acc"
			
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutCurr_acc = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutCurr_acc = Nothing
	End Function
	
	'**%insLedAutReinsuran: This function is in charge of call the automatic posting reinsuran process
	'%  insLedAutClaim: Esta función se encarga de llamar al proceso de asientos automáticos
	'%  de Co/reaseguro.
	Public Function insLedAutCuentecn(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutCuentecn As eRemoteDB.Execute
		
		lrecLedAutCuentecn = New eRemoteDB.Execute
		
		insLedAutCuentecn = False
		
		With lrecLedAutCuentecn
			.StoredProcedure = "InsLedAutCuentecn"
			
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutCuentecn = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutCuentecn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutCuentecn = Nothing
	End Function
	
	'**%insLedAutExpCash: This function is in charge of call the automatic posting out cash
	'%  insLedAutExpCash: Esta función se encarga de llamar a los procesos de asientos
	'%  automáticos de caja egreso.
	Public Function insLedAutExpCash(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutExpCash As eRemoteDB.Execute
		
		lrecLedAutExpCash = New eRemoteDB.Execute
		
		insLedAutExpCash = False
		
		With lrecLedAutExpCash
			.StoredProcedure = "InsLedAutExpCash"
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutExpCash = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutExpCash may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutExpCash = Nothing
	End Function
	'**insLedAutIncCash: This function is in charge of call the automatic posting cash out
	'%  insLedAutIncCash: Esta función se encarga de llamar a los procesos de asientos
	'%  entrada de dinero en caja.
	Public Function insLedAutIncCash(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutIncCash As eRemoteDB.Execute
		
		lrecLedAutIncCash = New eRemoteDB.Execute
		
		insLedAutIncCash = False
		
		With lrecLedAutIncCash
			.StoredProcedure = "InsLedAutIncCash"
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutIncCash = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutIncCash may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutIncCash = Nothing
	End Function
	
	'**%insLedAutPremium: This function is in charge of call the automatic posting premium process
	'%  insLedAutPremium: Esta función se encarga de llamar a los procesos de asientos
	'%  automáticos de primas.
	Public Function insLedAutPremium(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutPremium As eRemoteDB.Execute
		
		lrecLedAutPremium = New eRemoteDB.Execute
		
		insLedAutPremium = False
		
		With lrecLedAutPremium
			.StoredProcedure = "insLedAutPremium"
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutPremium = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutPremium = Nothing
	End Function
	
	'**%insPostCPL999_k: This function is in charge of call the automatic posting process
	'**%"Premiums, Claims, Current Accounts.
	'%  insPostCPL999_k: Esta función se encarga de llamar a los procesos de asientos
	'%  automáticos "Primas, Siniestros, Cuentas corrientes".
	Public Function insPostCPL999_K(ByVal sCodispl As String, ByVal nArea_Led As Integer, ByVal dCtrol_date As Date, ByVal dTo_date As Date, ByVal sType_process As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostCPL999_K_Err
		
		
		Select Case nArea_Led
			
			'**+ Automatic premium entries.
			'+ Asientos automáticos de "Primas".
			
			Case 1 '+ Primas
				insPostCPL999_K = insLedAutPremium(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'**+ Automatic claim entries.
				'+ Asientos automáticos de "Siniestros".
				
			Case 2 '+ Siniestros
				insPostCPL999_K = insLedAutClaim(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'**+ Automatic current account entries.
				'+ Asientos automáticos de "Cuentas corrientes".
				
			Case 3 '+ Cuentas corrientes
				insPostCPL999_K = insLedAutCurr_acc(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'**+ Automatic current reinsuran.
				'+ Asientos automáticos de "Cuentas corrientes".
				
			Case 4 '+ reaseguro
				insPostCPL999_K = insLedAutCuentecn(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'+ Asientos automáticos de "Caja ingreso".
				
			Case 5 '+ Caja ingreso
				insPostCPL999_K = insLedAutIncCash(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'+ Asientos automáticos de "Caja egreso".
				
			Case 6 '+ Caja egreso
				insPostCPL999_K = insLedAutExpCash(dTo_date, dCtrol_date, nUsercode, nArea_Led, CInt(sType_process))
				
				'**+ Automatic current account entries - APV.
				'+ Asientos automáticos de "Cuentas corrientes" - APV.
				
			Case 40
				insPostCPL999_K = insLedAutCurr_accAPV(dTo_date, dCtrol_date, nUsercode, CInt("3"), CInt(sType_process))
				
		End Select
		
insPostCPL999_K_Err: 
		If Err.Number Then
			insPostCPL999_K = False
		End If
		
		On Error GoTo 0
	End Function
	'**% insValCPL999_K: Validates the introduced data for the automatic entries process.
	'% insValCPL999_K: Valida los datos introducidos para el proceso de asientos automáticos.
	Public Function insValCPL999_K(ByVal sCodispl As String, ByVal nArea_Led As Integer, ByVal dTo_date As Date, ByVal dIni_date As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		
		On Error GoTo insValCPL999_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCtrol_date = New eGeneral.Ctrol_date
		
		
		'+ Campo Área: Debe estar lleno
		If nArea_Led = 0 Or nArea_Led = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36200)
		End If
		
		
		'+ Campo Fecha hasta: Debe estar lleno
		If dTo_date = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3239)
		Else
			If nArea_Led <> 0 And nArea_Led <> eRemoteDB.Constants.intNull Then
				
				If dIni_date = eRemoteDB.Constants.dtmNull Then
					If lclsCtrol_date.Find(nArea_Led) Then
						If dTo_date <= lclsCtrol_date.dEffecdate Then
							Call lclsErrors.ErrorMessage(sCodispl, 700043)
						End If
					End If
				Else
					If dTo_date <= dIni_date Then
						Call lclsErrors.ErrorMessage(sCodispl, 700043)
					End If
				End If
				
			End If
		End If
		
		insValCPL999_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCPL999_K_Err: 
		If Err.Number Then
			insValCPL999_K = insValCPL999_K & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insLedAutCurr_accAPV: This function is in charge of call the automatic posting APV - current_account process
	'%  insLedAutClaim: Esta función se encarga de llamar al proceso de asientos automáticos
	'%  de cuentas corrientes - APV.
	Public Function insLedAutCurr_accAPV(ByVal dTo_date As Date, ByVal dCtrol_date As Date, ByVal nUsercode As Integer, ByVal nArea_Led As Integer, ByVal sType_process As Integer) As Boolean
		Dim lrecLedAutCurr_accAPV As eRemoteDB.Execute
		
		lrecLedAutCurr_accAPV = New eRemoteDB.Execute
		
		insLedAutCurr_accAPV = False
		
		With lrecLedAutCurr_accAPV
			.StoredProcedure = "InsLedAutCurr_accAPV"
			
			.Parameters.Add("dTo_Date", dTo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCtrol_Date", dCtrol_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminary", sType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insLedAutCurr_accAPV = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedAutCurr_accAPV may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedAutCurr_accAPV = Nothing
	End Function
	
	'% InsCreTmp_Cal503: Crea los registros de producción en la tabla tmp_Cal503, para luego mostrar el LT de producción.
	Public Function InsCreTmp_Cal503(ByVal llngCompany As Integer, ByVal llngInsur_Area As Integer, ByVal ldtmDateFrom As Date, ByVal ldtmDateTo As Date) As Boolean
		Dim lclsTmp_Cal503 As eRemoteDB.Execute
		
		lclsTmp_Cal503 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_Cal503
			.StoredProcedure = "CreTmp_Cal503"
			.Parameters.Add("P_COD_CIA", llngCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_AREA_SEGURO", llngInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_DESDE", ldtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_HASTA", ldtmDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsCreTmp_Cal503 = True
			Else
				InsCreTmp_Cal503 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_Cal503 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_Cal503 = Nothing
	End Function
	
	'% InsCreTmp_Agl776: Crea los registros de intereses devengados en la tabla TMP_AGL776, para luego mostrar el LT de Comisiones devengadas.
	Public Function InsCreTMP_AGL776(ByVal llngCompany As Integer, ByVal ldtmDateFrom As Date, ByVal ldtmDateTo As Date) As Boolean
		Dim lclsTmp_Agl776 As eRemoteDB.Execute
		
		lclsTmp_Agl776 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_Agl776
			.StoredProcedure = "CreTmp_Agl776"
			.Parameters.Add("P_COD_CIA", llngCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_DESDE", ldtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_HASTA", ldtmDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsCreTMP_AGL776 = True
			Else
				InsCreTMP_AGL776 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_Agl776 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_Agl776 = Nothing
	End Function
	
	'% InsCreTmp_Lrecaudacion: Crea los registros de producción en la tabla tmp_Lrecaudacion, para luego mostrar el LT de recaudacion.
    Public Function InsCreTmp_Lrecaudacion(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
        Dim lclsTmp_Lrecaudacion As eRemoteDB.Execute

        lclsTmp_Lrecaudacion = New eRemoteDB.Execute

        '**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
        '+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..

        With lclsTmp_Lrecaudacion
            .StoredProcedure = "CreTmp_Lrecaudacion"
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsCreTmp_Lrecaudacion = True
            Else
                InsCreTmp_Lrecaudacion = False
            End If

        End With

        'UPGRADE_NOTE: Object lclsTmp_Lrecaudacion may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTmp_Lrecaudacion = Nothing
    End Function
	
	'% InsCreTmp_Sil704: Crea los registros de producción en la tabla tmp_Cal503, para luego mostrar el LT de producción.
	Public Function InsCreTmp_Sil704(ByVal llngCompany As Integer, ByVal llngInsur_Area As Integer, ByVal ldtmDateFrom As Date, ByVal ldtmDateTo As Date) As Boolean
		Dim lclsTmp_Sil704 As eRemoteDB.Execute
		
		lclsTmp_Sil704 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_Sil704
			.StoredProcedure = "CreTmp_Sil704"
			.Parameters.Add("P_COD_CIA", llngCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_AREA_SEGURO", llngInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_DESDE", ldtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_FECHA_HASTA", ldtmDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsCreTmp_Sil704 = True
			Else
				InsCreTmp_Sil704 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_Sil704 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_Sil704 = Nothing
	End Function
	
	
	'InsValSIL704: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function InsValSIL704(ByVal sCodispl As String, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ValidateSIL704_Err
		
		lclsErrors = New eFunctions.Errors
		
		If dEffecdateIni = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60217)
		End If
		
		If dEffecdateEnd = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60218)
		End If
		
		If dEffecdateEnd <> eRemoteDB.Constants.dtmNull And dEffecdateIni <> eRemoteDB.Constants.dtmNull Then
			If dEffecdateIni > dEffecdateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 55006)
			End If
		End If
		
		InsValSIL704 = lclsErrors.Confirm
		
ValidateSIL704_Err: 
		If Err.Number Then
			InsValSIL704 = InsValSIL704 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
End Class






