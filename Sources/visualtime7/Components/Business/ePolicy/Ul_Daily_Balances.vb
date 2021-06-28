Option Strict Off
Option Explicit On
Public Class Ul_Daily_Balances
	
	'+ Column_name                     Type           Nullable
	'+ ------------------------------  -------------- --------
	Public nBranch As Integer 'NUMBER (5)    NOT NULL,
	Public nProduct As Integer 'NUMBER (5)    NOT NULL,
	Public nPolicy As Double 'NUMBER (10)   NOT NULL,
	Public nCertif As Double 'NUMBER (10)   NOT NULL,
	Public dBal_date As Date 'DATE          NOT NULL,
	Public nFunds As Integer 'NUMBER(5)     NOT NULL,
	Public nOrigin As Integer 'NUMBER(5)     NOT NULL,
	Public nQuan_avail As Double 'NUMBER (18,6) NOT NULL,
	Public nUnit_price As Double 'NUMBER (12,6) NOT NULL,
	Public nBalance As Double 'NUMBER (18,8) NOT NULL,
	Public nCurrency_pol As Integer 'NUMBER (5)    NOT NULL,
	Public nBalance_local As Double 'NUMBER (20,8) NOT NULL,
	Public nExchange As Double 'NUMBER (11,6) NOT NULL,
	Public nUsercode As Integer 'NUMBER (5)    NOT NULL,
	Public nCurrency As Integer 'NUMBER (5)    NOT NULL
	
	'% insPostAGL7000: Esta función permite realizar el proceso de Comisiones del producto APV - AGL7000.
	Public Function ValDaily_Balances(ByVal sCodispl As String, ByVal dIni_date As Date, ByVal dEnd_date As Date, ByVal sMonth As String, ByVal sYear As String) As Boolean
		Dim lrecvaldaily_balances As eRemoteDB.Execute
		
		On Error GoTo valdaily_balances_Err
		
		lrecvaldaily_balances = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'valdaily_balances'
		'**+ The Information was read on  17/09/2003
		
		'+ Definición de parámetros para stored procedure 'valdaily_balances'
		'+ Información leída el: 17/09/2003
		
		With lrecvaldaily_balances
			.StoredProcedure = "valdaily_balances"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIni_date", dIni_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMonth", sMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sYear", sYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValDaily_Balances = IIf(.Parameters.Item("sIndicator").Value = "1", True, False)
			End If
		End With
		
valdaily_balances_Err: 
		If Err.Number Then
			ValDaily_Balances = False
		End If
		'UPGRADE_NOTE: Object lrecvaldaily_balances may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvaldaily_balances = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValDaily_Balances: Esta función permite validar si a la fecha indicada por párametro se ha
	'%                       ejecutado el proceso de saldos diarios
	Public Function InsValDaily_Balances(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecInsValDaily_Balances As eRemoteDB.Execute
		
		On Error GoTo InsValDaily_Balances_Err
		
		lrecInsValDaily_Balances = New eRemoteDB.Execute
		
		With lrecInsValDaily_Balances
			.StoredProcedure = "InsValDaily_Balances"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nExists_Balance", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValDaily_Balances = .Parameters("nExists_Balance").Value <> 0
			Else
				InsValDaily_Balances = False
			End If
		End With
		
InsValDaily_Balances_Err: 
		If Err.Number Then
			InsValDaily_Balances = False
		End If
		'UPGRADE_NOTE: Object lrecInsValDaily_Balances may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValDaily_Balances = Nothing
		On Error GoTo 0
	End Function
End Class






