Option Strict Off
Option Explicit On
Public Class CreditSales
	
	'-  Column Name              ID  Pk  Null?   Data Type   Default Histogram
	
	Public sCertype As String '1       N   CHAR (1 Byte)       Yes
	Public nBranch As Integer '2       N   NUMBER (5)          Yes
	Public nProduct As Integer '3       N   NUMBER (5)          Yes
	Public nPolicy As Double '4       N   NUMBER (10)         Yes
	Public nCertif As Double '5       N   NUMBER (10)         Yes
	Public dEffecdate As Date '6       N   DATE                Yes
	Public nConsec As Integer '7       N   NUMBER (5)          Yes
	Public dNulldate As Date '8       Y   DATE                Yes
	Public nUsercode As Integer '9       Y   NUMBER (5)          Yes
	Public dDocdate As Date '11      Y   DATE                Yes
	Public nType As Integer '12      Y   CHAR (1 Byte)       Yes
	Public sNumber As String '13      Y   CHAR (12 Byte)      Yes
	Public NCURRENCY As Integer '14      Y   NUMBER              Yes
	Public nAmount As Double '15      Y   NUMBER (18,6)       Yes
	Public nCountry As Integer '16      Y   NUMBER (5)          Yes
	Public NNOTENUM As Integer '17      Y   NUMBER (5)          Yes
	Public dExpirdoc As Date
	
	'%Find_v: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "CreditSales"
	Public Function Find_v(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nConsec As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecCreditSales As eRemoteDB.Execute
		
		On Error GoTo Find_v_Err
		
		'+Definición de parámetros para stored procedure 'rearrCreditSales_tmp'
		lrecCreditSales = New eRemoteDB.Execute
		With lrecCreditSales
			.StoredProcedure = "reaCreditSales_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_v = True
				.RCloseRec()
			End If
		End With
		
Find_v_Err: 
		If Err.Number Then
			Find_v = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecCreditSales may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreditSales = Nothing
	End Function
	
	
	'**%Objective: CT002 Page validations
	'%Objetivo: Función que permite efectuar las validaciones.
	Public Function insValCT002(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nConsec As Integer = 0, Optional ByVal dDocdate As Date = #12:00:00 AM#, Optional ByVal nType As Integer = 0, Optional ByVal sNumber As String = "", Optional ByVal NCURRENCY As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nCountry As Integer = 0, Optional ByVal NNOTENUM As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sMassive As String = "") As String
		Dim lblnValCT002 As Boolean
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCT002_err
		
		lclsErrors = New eFunctions.Errors
		
		
		
		lblnValCT002 = True
		
		If sMassive = "2" Then
			'+Orden / Consecutivo, debe estar lleno y no se debe repetir
			
			If nConsec = eRemoteDB.Constants.intNull Or nConsec = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1012)
				lblnValCT002 = False
			End If
			
			'+Fecha del documento debe estar lleno y debe ser menor igual a la fecha del dia
			
			If dDocdate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 55952)
				lblnValCT002 = False
			End If
			
			'+Tipo de documento debe estar lleno
			
			If CInt(nType) <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 5115)
				lblnValCT002 = False
			End If
			
			'+Moneda asociada a la venta debe estar lleno
			
			If NCURRENCY <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10107)
				lblnValCT002 = False
			End If
			
			'+Se debe indicar el importe del movimiento
			
			If nAmount <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60198)
				lblnValCT002 = False
			End If
		Else
			'+Se validan los limites
			If lblnValCT002 Then
				If Find_Excess(sCertype, nBranch, nProduct, nPolicy, nCertif, nConsec, dEffecdate, nAmount) Then
					Call lclsErrors.ErrorMessage(sCodispl, 9000030)
				End If
			End If
		End If
		insValCT002 = lclsErrors.Confirm
		
insValCT002_err: 
		If Err.Number Then
			insValCT002 = "insValCT002: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	Public Function insPostCT002(ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nConsec As Integer, ByVal dDocdate As Date, ByVal nType As Integer, ByVal sNumber As String, ByVal NCURRENCY As Integer, ByVal nAmount As Double, ByVal nCountry As Integer, ByVal NNOTENUM As Integer, ByVal nUsercode As Double, ByVal dExpirdoc As Date) As Boolean
		
		insPostCT002 = Upd(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nConsec, dDocdate, nType, sNumber, NCURRENCY, nAmount, nCountry, NNOTENUM, nAction, nUsercode, dExpirdoc)
		
	End Function
	
	'%Upd: Este metodo actualiza la tabla "CreditSales" segun el manejo histórico correspondiente
	Public Function Upd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nConsec As Integer, ByVal dDocdate As Date, ByVal nType As Integer, ByVal sNumber As String, ByVal NCURRENCY As Integer, ByVal nAmount As Double, ByVal nCountry As Integer, ByVal NNOTENUM As Integer, ByVal nAction As Integer, ByVal nUsercode As Double, ByVal dExpirdoc As Date) As Boolean
		Dim lrecCreditSales As eRemoteDB.Execute
		
		On Error GoTo Upd_Err
		
		'+Definición de parámetros para stored procedure 'rearrCreditSales_tmp'
		lrecCreditSales = New eRemoteDB.Execute
		With lrecCreditSales
			.StoredProcedure = "insCreditSales"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocdate", dDocdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumber", sNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", NCURRENCY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", NNOTENUM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdoc", dExpirdoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Upd = .Run(False)
		End With
		
Upd_Err: 
		If Err.Number Then
			Upd = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	
	'%Find_Excess: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "CreditSales"
	Public Function Find_Excess(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nConsec As Double, ByVal dEffecdate As Date, ByVal nAmount As Double) As Boolean
		Dim lrecCreditSales As eRemoteDB.Execute
		
		On Error GoTo Find_Excess_Err
		
		'+Definición de parámetros para stored procedure 'rearrCreditSales_tmp'
		lrecCreditSales = New eRemoteDB.Execute
		With lrecCreditSales
			.StoredProcedure = "REAEXCESS_CREDIT_SALES"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nAmount") + nAmount > .FieldToClass("nLimitCurrent") Then
					Find_Excess = True
				End If
				.RCloseRec()
			End If
		End With
		
Find_Excess_Err: 
		If Err.Number Then
			Find_Excess = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecCreditSales may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreditSales = Nothing
	End Function
End Class






