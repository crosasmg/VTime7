Option Strict Off
Option Explicit On
Public Class Bulletin
	'%-------------------------------------------------------%'
	'% $Workfile:: Bulletin.cls                             $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 4/08/04 4:04p                                $%'
	'% $Revision:: 39                                       $%'
	'%-------------------------------------------------------%'
	
	'-Name                                                   Null    Type
	' ----------------------------------------------------- -------- ------------------------------------
	Public nBulletins As Double 'NOT NULL NUMBER(10)
	Public sClient As String 'NOT NULL CHAR(14)
	Public nInsur_area As Integer '         Number(5)
	Public dLimit_pay As Date '         Date
	Public nCurrency As Integer '         Number(5)
	Public nWay_pay As Integer '         number(5)
	Public ncod_agree As Integer '         number(5)
	Public nAmount As Double '         number(10, 2)
	Public nStatus As Integer '         number(5)
	Public nCancel_Cod As Integer '         number(5)
	Public nBordereaux As Double '         number(10)
	Public dStatdate As Date
	Public nRejectCause As Integer '         number(5)
	Public nBank_code As Double '         number(10)
	Public dPayDate As Date
	Public nExchange As Double '         number(11, 6)
	Public nCurrpay As Integer '         number(5)
	Public sKeyaddress As String '         char(20)
	Public sInd_domic As String '         char(1)
	Public dSend_domic As Date
	Public sDocument As String '         char(15)
	Public nUsercode As Integer '         number(5)
	
	'- Variables auxiliares
	
	Public sCliename As String
	Public sAccount As String
	Public nBullAmount As Double
	Public nLocalAmount As Double
	Public sSel As String
	Public nBranch As Integer
	Public sBranch As String
	Public nProduct As Integer
	Public sProduct As String
	Public nPolicy As Double
	Public nReceipt As Double
	Public nDraft As Integer
	Public nQuantity As Integer
	Public nQuanReje As Integer
	Public nAmouReje As Double
	
	'- Se define la variable que determina el estado de la clase
	
	Public Enum eStatusInstance
		eftNew = 0
		eftQuery = 1
		eftExist = 1
		eftUpDate = 2
		eftDelete = 3
	End Enum
	
	Public nStatInstanc As eStatusInstance
	
	'% Find: busca los datos de un boletín específico
	Public Function Find(ByVal nBulletins As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaBulletins As eRemoteDB.Execute
		
		On Error GoTo Err_Find
		
		lreaBulletins = New eRemoteDB.Execute
		
		With lreaBulletins
			.StoredProcedure = "reaBulletins_o"
			
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nBulletins = .FieldToClass("nBulletins")
				sClient = .FieldToClass("sClient")
				sCliename = .FieldToClass("sCliename")
				nInsur_area = .FieldToClass("nInsur_area")
				dLimit_pay = .FieldToClass("dLimit_pay")
				nCurrency = .FieldToClass("nCurrency")
				nWay_pay = .FieldToClass("nWay_pay")
				ncod_agree = .FieldToClass("nCod_agree")
				nAmount = .FieldToClass("nAmount")
				nStatus = .FieldToClass("nStatus")
				nCancel_Cod = .FieldToClass("nCancel_cod", eRemoteDB.Constants.intNull)
				nBordereaux = .FieldToClass("nBordereaux")
				dStatdate = .FieldToClass("dStatdate")
				nRejectCause = .FieldToClass("nRejectcause")
				nBank_code = .FieldToClass("nBank_code")
				dPayDate = .FieldToClass("dPaydate")
				nExchange = .FieldToClass("nExchange")
				nCurrpay = .FieldToClass("nCurrpay")
				sKeyaddress = .FieldToClass("sKeyaddress")
				sDocument = .FieldToClass("sDocument")
				nLocalAmount = .FieldToClass("nLocalAmount")
				nExchange = .FieldToClass("nExchange")
				sInd_domic = .FieldToClass("sInd_domic")
				dSend_domic = .FieldToClass("dSend_domic")
				
				Find = True
				
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Err_Find: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins = Nothing
		
	End Function
	
	'% Find_Collect_Gen: verifica que boletin exista en cobranza generada.
	Public Function FindCollect_Gen(ByVal nBulletin As Double) As Boolean
		Dim lreCollect_Gen As eRemoteDB.Execute
		
		lreCollect_Gen = New eRemoteDB.Execute
		
		On Error GoTo FindCollect_GenError
		
		With lreCollect_Gen
			.StoredProcedure = "reaCollect_Gen"
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCollect_Gen = True
			Else
				FindCollect_Gen = False
			End If
		End With
		
FindCollect_GenError: 
		If Err.Number Then
			FindCollect_Gen = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreCollect_Gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreCollect_Gen = Nothing
	End Function
	
	'% Update: Esta función modifica el Estado del Boletin.
	Public Function Update(ByVal nBulletin As Double) As Boolean
		Dim lclsBulletin As Bulletin
		Dim lrecupdBulletin_Stat As eRemoteDB.Execute
		
		On Error GoTo Err_Update
		
		lrecupdBulletin_Stat = New eRemoteDB.Execute
		
		With lrecupdBulletin_Stat
			.StoredProcedure = "updBulletin_StatReject"
			
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRejectCause", nRejectCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Stat = Nothing
		
Err_Update: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
	End Function
	'% UpdCollect_Gen: Esta función modifica el estado del boletin en Collect_Gen.
	Public Function UpdCollect_Gen(ByVal nBulletin As Double, ByVal nUsercode As Integer, ByVal nType As Integer) As Boolean
		Dim lclsBulletin As Bulletin
		Dim lrecupdBulletin_Stat As eRemoteDB.Execute
		
		On Error GoTo Err_UpdCollect_Gen
		
		lrecupdBulletin_Stat = New eRemoteDB.Execute
		
		With lrecupdBulletin_Stat
			.StoredProcedure = "updCollect_Gen"
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdCollect_Gen = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Stat = Nothing
		
Err_UpdCollect_Gen: 
		If Err.Number Then
			UpdCollect_Gen = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% UpdateStatBulletin: Esta función modifica el Estado del Boletin.
	Public Function UpdateCancel_code(ByVal nBulletin As Double) As Boolean
		Dim lrecupdBulletin_Cancel As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateCancel_code
		
		lrecupdBulletin_Cancel = New eRemoteDB.Execute
		
		With lrecupdBulletin_Cancel
			.StoredProcedure = "updBulletin_CancelCode"
			
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCancel_code", nCancel_Cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCancel_code = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Cancel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Cancel = Nothing
		
Err_UpdateCancel_code: 
		If Err.Number Then
			UpdateCancel_code = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% UpdateStatBulletin: Esta función modifica el Estado del Boletin.
	Public Function UpdateStatBulletin(ByVal nBordereaux As Double, ByVal nBulletin As Double, ByRef nStatus As Integer) As Boolean
		Dim lclsBulletin As Bulletin
		Dim lrecupdBulletin_Stat As eRemoteDB.Execute
		
		On Error GoTo UpdateStatBulletin_Err
		
		lrecupdBulletin_Stat = New eRemoteDB.Execute
		
		With lrecupdBulletin_Stat
			.StoredProcedure = "updBulletin_Status"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrPay", nCurrpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateStatBulletin = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Stat = Nothing
		
UpdateStatBulletin_Err: 
		If Err.Number Then
			UpdateStatBulletin = False
		End If
		
		On Error GoTo 0
	End Function
	'%UpdateBulletin_Des: desmarca causa para un boletin ya anulado.
	'Genera movimiento en la premium_mo.
	Public Function UpdBulletin_Des(ByVal nBulletin As Double, ByVal nRejectCause As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsBulletin As Bulletin
		Dim lrecupdBulletin_Des As eRemoteDB.Execute
		
		On Error GoTo UpdBulletin_Des_Err
		
		lrecupdBulletin_Des = New eRemoteDB.Execute
		
		With lrecupdBulletin_Des
			.StoredProcedure = "UpdBulletin_Des"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRejectCause", nRejectCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdBulletin_Des = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Des may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Des = Nothing
		
UpdBulletin_Des_Err: 
		If Err.Number Then
			UpdBulletin_Des = False
		End If
		
		On Error GoTo 0
	End Function
	'% findBulletin_amount: Esta función lee el importe del boletin
	Public Function findBulletin_amount(ByRef ldtmEffecdate As Date, ByRef lintWay_pay As Integer, ByRef ldblBank As Double, ByRef llngInsur_area As Integer) As Boolean
		Dim lrecreaBulletins As eRemoteDB.Execute
		
		On Error GoTo Err_findBulletin_amount
		
		lrecreaBulletins = New eRemoteDB.Execute
		
		With lrecreaBulletins
			.StoredProcedure = "reaBulletins_amount"
			
			.Parameters.Add("dEffectdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", lintWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", ldblBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", llngInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If Not .EOF Then
					findBulletin_amount = True
					Me.nBullAmount = .FieldToClass("nAmount", 0)
				End If
			End If
		End With
		
Err_findBulletin_amount: 
		If Err.Number Then
			findBulletin_amount = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% findBulletin_anul: Esta función lee el boletin anulado
	Public Function findBulletin_anul(ByVal nBulletin As Double) As Boolean
		Dim lrecreaBulletins As eRemoteDB.Execute
		
		On Error GoTo Err_findBulletin_anul
		
		lrecreaBulletins = New eRemoteDB.Execute
		
		With lrecreaBulletins
			.StoredProcedure = "reaBulletins_anulado"
			.Parameters.Add("nBulletins", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				findBulletin_anul = True
			Else
				findBulletin_anul = False
			End If
			
		End With
		
Err_findBulletin_anul: 
		If Err.Number Then
			findBulletin_anul = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValCO513: Se efectuan las validaciones de la pestaña de boletines en la secuencia.
	Public Function insValCO513(ByVal sCodispl As String, Optional ByVal nBulletin As Double = 0, Optional ByVal nBordereauxP As Double = 0, Optional ByVal nLine As Integer = 0, Optional ByVal sOption As String = "") As String
		Dim lobjError As Object '+ eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCO513_Err
		
		lobjError = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		insValCO513 = String.Empty
		
		If Trim(CStr(nBulletin)) = String.Empty Then
			nBulletin = 0
		End If
		
		lblnError = False
		
		If nBulletin = 0 Then
			lobjError.ErrorMessage(sCodispl, 55016, nLine)
			lblnError = True
		End If
		
		If Not lblnError Then
			If nBulletin <> 0 Then
				
				'+ Se verifica la existencia de boletin y que el mismo sea valido.
				
				If Not Find(nBulletin, True) Then
					lobjError.ErrorMessage(sCodispl, 55016, nLine)
					lblnError = True
				Else
					If nStatus <> 1 And nStatus <> 4 Then
						lobjError.ErrorMessage(sCodispl, 55017, nLine)
						lblnError = True
					End If
					
					If insBulletinExist(nBulletin) Then
						If nBordereaux <> nBordereauxP Then
							lobjError.ErrorMessage(sCodispl, 55018, nLine, eFunctions.Errors.TextAlign.RigthAling, "(Relación: " & CStr(nBordereaux) & ")")
							lblnError = True
						Else
							If sOption = "PopUp" Then
								lobjError.ErrorMessage(sCodispl, 5010, nLine)
								lblnError = True
							End If
						End If
					End If
				End If
			End If
		End If
		
		insValCO513 = lobjError.Confirm
		
		'UPGRADE_NOTE: Object lobjError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjError = Nothing
		
insValCO513_Err: 
		If Err.Number Then
			insValCO513 = insValCO513 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'% insBulletinExist: Verifica la existencia de un boletin en otra relación.
	Public Function insBulletinExist(ByVal Bulletins As Double) As Boolean
		Dim lreaBulletins As eRemoteDB.Execute
		
		On Error GoTo insBulletinExist_Err
		
		lreaBulletins = New eRemoteDB.Execute
		
		With lreaBulletins
			.StoredProcedure = "REABULLETINS_EXIST"
			
			.Parameters.Add("nBulletins", Bulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nBordereaux = .FieldToClass("nBordereaux")
				
				insBulletinExist = True
				
				.RCloseRec()
			Else
				insBulletinExist = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins = Nothing
		
insBulletinExist_Err: 
		If Err.Number Then
			insBulletinExist = False
		End If
		
		On Error GoTo 0
	End Function
	'%insValCO513: Se efectuan las validaciones de la pestaña de boletines en la secuencia.
	Public Function insValCO501_K(ByVal sCodispl As String, ByVal dExpirDat As Date, ByVal nWay_pay As Integer, ByVal nBank As Double, ByVal ncod_agree As Integer) As String
		Dim lerrTime As eFunctions.Errors '+ eFunctions.Errors
		Dim clsMultipac As eCollection.Bank_Agree
		
		On Error GoTo insValCO501_K_Err
		
		lerrTime = New eFunctions.Errors
		clsMultipac = New eCollection.Bank_Agree
		
		With lerrTime
			'+Validacion del campo "Fecha del vencimiento"
			If dExpirDat = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 7116)
			End If
			
			'+Validacion del campo "Vìa del pago"
			If nWay_pay <= 0 Then
				.ErrorMessage(sCodispl, 55008)
			End If
			
			'+Validacion del campo "Código del banco"
			If nWay_pay > 0 Then
				If nBank <= 0 And nWay_pay = 1 Then
					.ErrorMessage(sCodispl, 55000)
				Else
					'+Valida que si banco esta en multipac debe ser lider
					If Not clsMultipac.Find_ExistMult(0, nBank, 1) Then
						If clsMultipac.Find_ExistMult(0, nBank, 2) Then
							.ErrorMessage(sCodispl, 60501)
						End If
					End If
				End If
			End If
			'+ valida que si via de pago es descuetno por planilla se ingrese el convenio
			If nWay_pay = 3 And ncod_agree <= 0 Then
				.ErrorMessage(sCodispl, 55004)
			End If
			
			
			insValCO501_K = .Confirm
		End With
		
insValCO501_K_Err: 
		If Err.Number Then
			insValCO501_K = insValCO501_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	
	'%insValCO501Upd: Se efectuan las validaciones del la CO501
	Public Function insValCO501Upd(ByVal sCodispl As String, ByVal nBulletin As Double, ByVal nRejectCause As Integer, ByVal sKey As String, ByVal nPolicy As Double, ByVal sAction As String) As String
		Dim lerrTime As eFunctions.Errors '+ eFunctions.Errors
		
		
		On Error GoTo insValCO501Upd_Err
		
		lerrTime = New eFunctions.Errors
		
		
		With lerrTime
			'+Validación del campo "Causa del rechazo"
			If nRejectCause <= 0 Then
				.ErrorMessage(sCodispl, 55001)
			End If
			
			'+Validación del registro existente en la ventana
			If sAction = "Add" Then
				If ExistCO501(sKey, nPolicy) Then
					.ErrorMessage(sCodispl, 10284)
				End If
			End If
			
			'+Validación de póliza existente en esa cobranza
			If nBulletin = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 56169)
			End If
			
			insValCO501Upd = .Confirm
		End With
		
insValCO501Upd_Err: 
		If Err.Number Then
			insValCO501Upd = insValCO501Upd & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'%insValCO501: Se efectuan las validaciones del la CO501
	Public Function insValCO501(ByVal sCodispl As String, ByVal sKey As String) As String
		Dim lerrTime As eFunctions.Errors '+ eFunctions.Errors
		Dim lrecinsCO501 As eRemoteDB.Execute
		
		On Error GoTo insValCO501_Err
		
		lerrTime = New eFunctions.Errors
		lrecinsCO501 = New eRemoteDB.Execute
		
		With lrecinsCO501
			.StoredProcedure = "insCO501pkg.InsValSelBulletins"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
			End If
			insValCO501 = lerrTime.Confirm
		End With
		
insValCO501_Err: 
		If Err.Number Then
			insValCO501 = insValCO501 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lrecinsCO501 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCO501 = Nothing
	End Function
	'% insPostCO501Upd_Des : Desmarca un rechazo de boletin ya anulado
	Public Function insPostCO501Upd_Des(ByVal nBulletin As Double, ByVal nRejectCause As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostCO501Upd_Des_Err
		
		With Me
			.nBulletins = nBulletin
			.nRejectCause = nRejectCause
			.nUsercode = nUsercode
		End With
		
		'+ Desmarca rechazo de Bulletins ya anulado
		insPostCO501Upd_Des = UpdBulletin_Des(nBulletin, nRejectCause, nUsercode)
		
insPostCO501Upd_Des_Err: 
		If Err.Number Then
			insPostCO501Upd_Des = False
		End If
		On Error GoTo 0
	End Function
	'% insPostCO501Upd: Crea/actualiza los registros correspondientes en la tabla
	Public Function insPostCO501Upd(ByVal sKey As String, ByVal sAction As String, ByVal nBulletins As Double, ByVal nRejectCause As Integer, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal sClient As String, ByVal nBank_code As Integer, ByVal sDocument As String, ByVal nAmount As Double, ByVal nDraft As Double, ByVal sProcess As String, ByVal nWay_pay As Integer) As Boolean
		On Error GoTo insPostCO501Upd_Err
		
		Dim lrecinsCO501 As eRemoteDB.Execute
		lrecinsCO501 = New eRemoteDB.Execute
		
		'+
		'+ Definición de stored procedure insUpdco501 al 02-27-2002 12:10:27
		'+
		With lrecinsCO501
			.StoredProcedure = "insCO501pkg.InsPostCO501Upd"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRejectCause", nRejectCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO501Upd = .Run(False)
		End With
		
insPostCO501Upd_Err: 
		If Err.Number Then
			insPostCO501Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostCO501: Crea/actualiza los registros correspondientes a la transacción CO501.
	Public Function insPostCO501(ByVal sKey As String, ByVal nWay_pay As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsUpdCO501 As eRemoteDB.Execute
		
		On Error GoTo insPostCO501_Err
		
		lrecinsUpdCO501 = New eRemoteDB.Execute
		
		'+
		'+ Definición de stored procedure insUpdco501 al 02-27-2002 12:10:27
		'+
		With lrecinsUpdCO501
			.StoredProcedure = "insUpdCO501"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO501 = .Run(False)
		End With
		
insPostCO501_Err: 
		If Err.Number Then
			insPostCO501 = False
		End If
		On Error GoTo 0
	End Function
	
	'%valBulletins_SendDomic: Verifica si el boletín fue enviado a la cobranza.
	Public Function valBulletins_SendDomic(ByVal nBulletins As Double) As Boolean
		Dim lrecBulletins As eRemoteDB.Execute
		Dim ldtmSend_Domic As Date
		
		On Error GoTo valBulletins_SendDomic_Err
		
		lrecBulletins = New eRemoteDB.Execute
		
		With lrecBulletins
			.StoredProcedure = "valBulletins_SendDomic"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dSend_Domic", ldtmSend_Domic, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valBulletins_SendDomic = (.Parameters("dSend_Domic").Value <> eRemoteDB.Constants.dtmNull)
		End With
		
valBulletins_SendDomic_Err: 
		If Err.Number Then
			valBulletins_SendDomic = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBulletins = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nBulletins = eRemoteDB.Constants.intNull
		Me.sClient = String.Empty
		Me.nInsur_area = eRemoteDB.Constants.intNull
		Me.dLimit_pay = eRemoteDB.Constants.dtmNull
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.nWay_pay = eRemoteDB.Constants.intNull
		Me.ncod_agree = eRemoteDB.Constants.intNull
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nStatus = eRemoteDB.Constants.intNull
		Me.nCancel_Cod = eRemoteDB.Constants.intNull
		Me.nBordereaux = eRemoteDB.Constants.intNull
		Me.dStatdate = eRemoteDB.Constants.dtmNull
		Me.nRejectCause = eRemoteDB.Constants.intNull
		Me.nBank_code = eRemoteDB.Constants.intNull
		Me.dPayDate = eRemoteDB.Constants.dtmNull
		Me.nExchange = eRemoteDB.Constants.intNull
		Me.nCurrpay = eRemoteDB.Constants.intNull
		Me.sKeyaddress = String.Empty
		Me.sInd_domic = String.Empty
		Me.dSend_domic = eRemoteDB.Constants.dtmNull
		Me.sDocument = String.Empty
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.sCliename = String.Empty
		Me.sAccount = String.Empty
		Me.nBullAmount = eRemoteDB.Constants.intNull
		Me.nLocalAmount = eRemoteDB.Constants.intNull
		Me.sSel = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% getBulletins_nStatus: Obtiene el estado del boletín.
	Public Function getBulletins_nStatus(ByVal nBulletin As Double) As Integer
		Dim lrecBulletins As eRemoteDB.Execute
		Dim lintStatus As Integer
		
		On Error GoTo Err_getBulletins_nStatus
		
		lrecBulletins = New eRemoteDB.Execute
		
		With lrecBulletins
			.StoredProcedure = "getBulletins_nStatus"
			.Parameters.Add("nBulletins", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", lintStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getBulletins_nStatus = .Parameters("nStatus").Value
			
		End With
		
Err_getBulletins_nStatus: 
		If Err.Number Then
			getBulletins_nStatus = 0
			On Error GoTo 0
		End If
		
	End Function
	
	'%FindCo501: Busca los datos correspondiente a un boletin en la tabla Bulletins para la transacción CO501.
	Public Function FindCo501(ByVal nWay_pay As Integer, ByVal dEffecdate As Date, ByVal nBank As Double, ByVal nPolicy As Double) As Boolean
		Dim lrecreaFindCo501 As eRemoteDB.Execute
		
		On Error GoTo FindCo501_Err
		
		lrecreaFindCo501 = New eRemoteDB.Execute
		
		With lrecreaFindCo501
			.StoredProcedure = "insCO501pkg.reaBulletins"
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCo501 = True
				Me.nBulletins = .FieldToClass("nBulletins", 0)
				Me.sClient = .FieldToClass("sClient", String.Empty)
				Me.sCliename = .FieldToClass("sCliename", String.Empty)
				Me.nBank_code = .FieldToClass("nBank_code")
				
				If nBank <= 0 Then '+Transbank
					Me.sDocument = .FieldToClass("sDocument")
					Me.sAccount = String.Empty
				Else '+ PAC
					Me.sAccount = .FieldToClass("sDocument")
					Me.sDocument = String.Empty
				End If
				
				Me.nAmount = .FieldToClass("nAmount")
				Me.nRejectCause = .FieldToClass("nRejectCause", eRemoteDB.Constants.intNull)
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nDraft = .FieldToClass("nDraft", eRemoteDB.Constants.intNull)
			End If
		End With
		
FindCo501_Err: 
		If Err.Number Then
			FindCo501 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFindCo501 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFindCo501 = Nothing
	End Function
	'%CalTotAmounts: Calcula los montos de la cobranza generada para la CO501
	Public Function CalTotAmounts(ByVal nWay_pay As Integer, ByVal dEffecdate As Date, ByVal nBank As Double) As Boolean
		Dim lrecCalTotAmounts As eRemoteDB.Execute
		
		On Error GoTo CalTotAmounts_Err
		
		lrecCalTotAmounts = New eRemoteDB.Execute
		
		With lrecCalTotAmounts
			.StoredProcedure = "insCO501pkg.CalTotAmounts"
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuanReje", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmouReje", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nAmount = .Parameters("nAmount").Value
				nQuantity = .Parameters("nQuantity").Value
				nQuanReje = .Parameters("nQuanReje").Value
				nAmouReje = .Parameters("nAmouReje").Value
				CalTotAmounts = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecCalTotAmounts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCalTotAmounts = Nothing
		
CalTotAmounts_Err: 
		If Err.Number Then
			CalTotAmounts = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%ExistCO501: Verifica la existencia de un registro en la temporal TMP_CO501
	Public Function ExistCO501(ByVal sKey As String, ByVal nPolicy As Double) As Boolean
		Dim lrecExistCO501 As eRemoteDB.Execute
		
		On Error GoTo ExistCO501_Err
		
		lrecExistCO501 = New eRemoteDB.Execute
		ExistCO501 = False
		
		With lrecExistCO501
			.StoredProcedure = "insCO501pkg.insValBulletins"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nExist").Value = 1 Then
					ExistCO501 = True
				Else
					ExistCO501 = False
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecExistCO501 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecExistCO501 = Nothing
		
ExistCO501_Err: 
		If Err.Number Then
			ExistCO501 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






