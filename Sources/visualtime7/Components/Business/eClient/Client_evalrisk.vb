Option Strict Off
Option Explicit On
Public Class Client_evalrisk
	
	'-Campos de la tabla
	
	Public sClient As String
	Public dEffecdate As Date
	Public dOtherdate As Date
	Public nSinceyear As Integer
	Public nNumemployers As Integer
	Public nCntryrisk As Integer
	Public nTypecia As Integer
	Public nTypeProduct As Integer
	Public nRisk As Integer
	Public nActbus As Integer
	Public nRefbank As Integer
	Public nRefbus As Integer
	Public nReflaw As Integer
	Public nNumpays As Integer
	Public nOldinsurance As Integer
	Public nPropay As Integer
	Public nCoddicom As Integer
	Public SDesdicom As String
	Public nCreditreason As Integer
	Public nLiqcurrent As Integer
	Public nLiqacd As Integer
	Public nRentability As Integer
	Public nGrowsales As Integer
	Public nEconomic As Integer
	Public nFinancial As Integer
	Public nCodRating As Integer
	Public nDesRating As Integer
	Public nCountry As Integer
	Public sNote1 As String
	Public sNote2 As String
	Public sNote3 As String
	Public sNote4 As String
	Public nBranchCia As Integer
	Public dNulldate As Date
	Public dCompdate As Date
	Public nUsercode As Integer
	Public nLimitCredit As Double
	Public nCurrency As Short
	'% insValBC9000: Realiza las validaciones de la transaccion
	Public Function insValBC9000(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal sClient As String = "", Optional ByVal nCodRating As Integer = 0, Optional ByVal nCurrency As Short = 0, Optional ByVal nLimitCredit As Double = 0) As String
		
		
		Dim lerrTime As eFunctions.Errors
		On Error GoTo insValBC9000_Err
		lerrTime = New eFunctions.Errors
		With lerrTime
			
			'+Validaciones del campo Clasificación Rating
			
			If nCodRating <= 0 Then
				.ErrorMessage(sCodispl, 9000014)
			End If
			'+Si se indico limite de credito, es necesario indicar la moneda asociada
			If nLimitCredit > 0 Then
				If nCurrency <= 0 Then
					.ErrorMessage(sCodispl, 750024)
				End If
			Else
				If nCurrency > 0 Then
					.ErrorMessage(sCodispl, 11417)
				End If
			End If
			
		End With
		
		insValBC9000 = lerrTime.Confirm
		
insValBC9000_Err: 
		If Err.Number Then
			insValBC9000 = insValBC9000 & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
		
		
		
	End Function
	
	'% Find: Esta función es la encarga de buscar si existe informacion en las tablas.
	'%                eval_master - doc_req_cli
	Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date) As Object
		
		Dim lrecreaClient_evalrisk As eRemoteDB.Execute
		Dim lclsreaClient_evalrisk As Client
		
		On Error GoTo Find_err
		lrecreaClient_evalrisk = New eRemoteDB.Execute
		
		With lrecreaClient_evalrisk
			.StoredProcedure = "reaClient_evalrisk"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				sClient = .FieldToClass("sClient")
				dEffecdate = .FieldToClass("dEffecdate")
				dOtherdate = .FieldToClass("dOtherdate")
				nSinceyear = .FieldToClass("nSinceyear", eRemoteDB.Constants.intNull)
				nNumemployers = .FieldToClass("nNumemployers", eRemoteDB.Constants.intNull)
				nCntryrisk = .FieldToClass("nCntryrisk", eRemoteDB.Constants.intNull)
				nTypecia = .FieldToClass("nTypecia", eRemoteDB.Constants.intNull)
				nTypeProduct = .FieldToClass("nTypeProduct", eRemoteDB.Constants.intNull)
				nRisk = .FieldToClass("nRisk", eRemoteDB.Constants.intNull)
				nActbus = .FieldToClass("nActbus", eRemoteDB.Constants.intNull)
				nRefbank = .FieldToClass("nRefbank", eRemoteDB.Constants.intNull)
				nRefbus = .FieldToClass("nRefbus", eRemoteDB.Constants.intNull)
				nReflaw = .FieldToClass("nReflaw", eRemoteDB.Constants.intNull)
				nNumpays = .FieldToClass("nNumpays", eRemoteDB.Constants.intNull)
				nOldinsurance = .FieldToClass("nOldinsurance", eRemoteDB.Constants.intNull)
				nPropay = .FieldToClass("nPropay", eRemoteDB.Constants.intNull)
				nCoddicom = .FieldToClass("nCoddicom", eRemoteDB.Constants.intNull)
				SDesdicom = .FieldToClass("SDesdicom")
				nCreditreason = .FieldToClass("nCreditreason", eRemoteDB.Constants.intNull)
				nLiqcurrent = .FieldToClass("nLiqcurrent", eRemoteDB.Constants.intNull)
				nLiqacd = .FieldToClass("nLiqacd", eRemoteDB.Constants.intNull)
				nRentability = .FieldToClass("nRentability", eRemoteDB.Constants.intNull)
				nGrowsales = .FieldToClass("nGrowsales", eRemoteDB.Constants.intNull)
				nEconomic = .FieldToClass("nEconomic", eRemoteDB.Constants.intNull)
				nFinancial = .FieldToClass("nFinancial", eRemoteDB.Constants.intNull)
				nCodRating = .FieldToClass("nCodrating", eRemoteDB.Constants.intNull)
				nDesRating = .FieldToClass("nDesRating", eRemoteDB.Constants.intNull)
				nCountry = .FieldToClass("nCountry", eRemoteDB.Constants.intNull)
				sNote1 = .FieldToClass("sNote1")
				sNote2 = .FieldToClass("sNote2")
				sNote3 = .FieldToClass("sNote3")
				sNote4 = .FieldToClass("sNote4")
				nBranchCia = .FieldToClass("nBranchCia", eRemoteDB.Constants.intNull)
				nLimitCredit = .FieldToClass("nLimitCredit", eRemoteDB.Constants.intNull)
				nCurrency = .FieldToClass("nCurrency", eRemoteDB.Constants.intNull)
			Else
				Find = False
			End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaClient_evalrisk may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_evalrisk = Nothing
		On Error GoTo 0
	End Function
	
	'% UpdClient_evalrisk: Esta funcion se encarga de realizar las actualizaciones de la tabla
	'%                      Client_evalrisk, correspodiente a las cuentas del cliente.
	Function UpdClient_evalrisk(ByVal nAction As Integer) As Boolean
		Dim lobjTime As eRemoteDB.Execute
		
		On Error GoTo UpdClient_evalrisk_Err
		lobjTime = New eRemoteDB.Execute
		
		With lobjTime
			.StoredProcedure = "insUpdClient_evalrisk"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOtherdate", dOtherdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSinceyear", nSinceyear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumemployers", nNumemployers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCntryrisk", nCntryrisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecia", nTypecia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProduct", nTypeProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRisk", nRisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActbus", nActbus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRefbank", nRefbank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRefbus", nRefbus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReflaw", nReflaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumpays", nNumpays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOldinsurance", nOldinsurance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPropay", nPropay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoddicom", nCoddicom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDesdicom", SDesdicom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCreditreason", nCreditreason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLiqcurrent", nLiqcurrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLiqacd", nLiqacd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRentability", nRentability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGrowsales", nGrowsales, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEconomic", nEconomic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinancial", nFinancial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodrating", nCodRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDesRating", nDesRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNote1", sNote1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNote2", sNote2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNote3", sNote3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNote4", sNote4, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchCia", nBranchCia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimitCredit", nLimitCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdClient_evalrisk = .Run(False)
		End With
		
		
UpdClient_evalrisk_Err: 
		If Err.Number Then
			UpdClient_evalrisk = False
		End If
		'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTime = Nothing
		On Error GoTo 0
	End Function
	'% InsPostBC9000: Realiza las actualizaciones de la transaccion
	Public Function InsPostBC9000(ByVal nAction As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal dOtherdate As Date, ByVal nSinceyear As Integer, ByVal nNumemployers As Integer, ByVal nCntryrisk As Integer, ByVal nTypecia As Integer, ByVal nTypeProduct As Integer, ByVal nRisk As Integer, ByVal nActbus As Integer, ByVal nRefbank As Integer, ByVal nRefbus As Integer, ByVal nReflaw As Integer, ByVal nNumpays As Integer, ByVal nOldinsurance As Integer, ByVal nPropay As Integer, ByVal nCoddicom As Integer, ByVal SDesdicom As String, ByVal nCreditreason As Integer, ByVal nLiqcurrent As Integer, ByVal nLiqacd As Integer, ByVal nRentability As Integer, ByVal nGrowsales As Integer, ByVal nEconomic As Integer, ByVal nFinancial As Integer, ByVal nCodRating As Integer, ByVal nDesRating As Integer, ByVal nCountry As Integer, ByVal sNote1 As String, ByVal sNote2 As String, ByVal sNote3 As String, ByVal sNote4 As String, ByVal nBranchCia As Object, ByVal nUsercode As Integer, ByVal nLimitCredit As Double, ByVal nCurrency As Short) As Boolean
		Dim lclsClientWin As eClient.ClientWin
		
		Me.sClient = sClient
		Me.dEffecdate = dEffecdate
		Me.dOtherdate = dOtherdate
		Me.nSinceyear = nSinceyear
		Me.nNumemployers = nNumemployers
		Me.nCntryrisk = nCntryrisk
		Me.nTypecia = nTypecia
		Me.nTypeProduct = nTypeProduct
		Me.nRisk = nRisk
		Me.nActbus = nActbus
		Me.nRefbank = nRefbank
		Me.nRefbus = nRefbus
		Me.nReflaw = nReflaw
		Me.nNumpays = nNumpays
		Me.nOldinsurance = nOldinsurance
		Me.nPropay = nPropay
		Me.nCoddicom = nCoddicom
		Me.SDesdicom = SDesdicom
		Me.nCreditreason = nCreditreason
		Me.nLiqcurrent = nLiqcurrent
		Me.nLiqacd = nLiqacd
		Me.nRentability = nRentability
		Me.nGrowsales = nGrowsales
		Me.nEconomic = nEconomic
		Me.nFinancial = nFinancial
		Me.nCodRating = nCodRating
		Me.nDesRating = nDesRating
		Me.nCountry = nCountry
		Me.sNote1 = sNote1
		Me.sNote2 = sNote2
		Me.sNote3 = sNote3
		Me.sNote4 = sNote4
		Me.nBranchCia = nBranchCia
		Me.nLimitCredit = nLimitCredit
		Me.nCurrency = nCurrency
		
		InsPostBC9000 = Me.UpdClient_evalrisk(nAction)
		
		If InsPostBC9000 Then
			lclsClientWin = New eClient.ClientWin
			Call lclsClientWin.insUpdClient_win(sClient, "BC9000", "2",  ,  , nUsercode)
			'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsClientWin = Nothing
		End If
		
	End Function
End Class






