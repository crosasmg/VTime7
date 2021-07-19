Option Strict Off
Option Explicit On
Public Class TDetail_pre
	'%-------------------------------------------------------%'
	'% $Workfile:: TDetail_pre.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 12/11/04 12:49p                              $%'
	'% $Revision:: 80                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades seg�n la tabla en el sistema 18/01/2001
	
	'+ Column_name         Type
	'+ ------------------- ----------
	Public sKey As String
	Public nCurrency As Integer
	Public nBill_item As Integer
	Public nBranch_est As Integer
	Public nBranch_led As Integer
	Public nBranch_rei As Integer
	Public sAddsuini As String
	Public nModulec As Integer
	Public nCode As Integer
	Public nCommi_rate As Double
	Public nCommision As Double
	Public nTax As Double
	Public nCapi_ini As Double
	Public nCapital As Double
	Public nPremium As Double
	Public nPremium_an As Double
	Public sType_detai As String
	Public nP_NotCons As Double
	Public nP_Outstand As Double
	Public nP_Adjust As Double
	Public nPrem_ini As Double
	Public nPrem_act As Double
	Public nCommi_anu As Double
	Public sAddtax As String
	Public nId_Bill As Integer
	Public nRel_IdBill As Integer
	Public nAmountAf As Double
	Public nAmountEx As Double
	Public sCacalili As String
	Public sCommissi_i As String
	Public nItem As Integer
	Public sDisexpri As String
	Public sShort_des As String
	Public nPremiumA As Double
	Public nPremiumE As Double
	
	Private mstrKey As String
	
	'+ Variables definidas para el eficiente uso de la coleccion creada por insCalreceiptmod
	
	Public dExpirdat As Date
	Public dEffecdate As Date
	Public nReceipt As Double
	Public sDescript As String
	Public sDocument As String
	Public nType As Integer
	Public nDet_code As Integer
	
	'- Almacena todos los n�meros de recibos separados por coma.
	Public sReceipts As String
	
	Public nTratypei As Integer
	Public nTypeReceipt As Integer
	Public dIssuedat As Date
	Public sOrigReceipt As String
	Public sAddtaxin As String
	Public sClient As String
	Public nCommission As Double
	
	'- Objeto para el manejo de los datos de la colecci�n de la clase
	Public mcolTDetail_pre As ePolicy.TDetail_pres
	Public mclsPolicy As ePolicy.Policy
	Public mclsCertificat As ePolicy.Certificat
	Public mclsProduct As eProduct.Product
	Public mclsPremium As eCollection.Premium
	Public mclsPremium2 As eCollection.Premium

	Public bError As Boolean
	Public sExist As String
	
	Public nPrem_det As Short
	Public sPrem_det As String
	Public nAplic_code As String
	Public nAplication As String
	
	'- Variable para almacenar el total de prima a distribuir entre los elementos sobre los
	'- cuales aplica el recargo/descuento/impuesto.  Se utiliza cuando se realiza el desglose
	'- de la prima
	Public nDet_premium As Double
	Public nDet_commision As Double
	Public nDet_commi_rate As Double
	
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	
	Public nProctype As Integer
	Public nUsercode As Integer
	Public dStartdate As Date
	Public nWay_pay As Integer
	
	'- Indica si la ejecucu�n es de forma preliminar = 1 o definitiva = 2
	Public sTypExecute As String
	
	'-Indicador para recibo manual de ajuste
	Public sAdjust As String
	
	'-Numero de recibo ajustado
	Public nAdjReceipt As Double
	
	'- Tipo de pago
	Public nTypepay As Integer ' NUMBER     22   0     3    S
	
	'- Campos para pago a cta cte poliza
	Public sCertypePay As String ' CHAR       1    0     0    N
	Public nBranchpay As Integer ' NUMBER     22   0     5    N
	Public nProductpay As Integer ' NUMBER     22   0     5    N
	Public nPolicypay As Double ' NUMBER     22   0     10   N
	Public nCertifpay As Double ' NUMBER     22   0     10   N

	'-Valores por pago a cuenta de cliente
	Public sClientpay As String ' CHAR       14   0     0    S

	Public nReceiptCollec As Double
	Public nPremium_Origi As Double
	Public nPercent As Double
	Public nAmount As Double
	Public nRole As Integer
	Public sModulec As String

	Public sPolitype As String
	Public sColinvot As String

	'- Indica los n�meros de movimientos pendientes a facturar generados
	Public sOut_moveme As String

	'InsPreCA027: Funci�n que realiza el c�lculo de recibo autom�tico
	Public Function InsPreCA027(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal dLedgerDate As Date, ByVal nUsercode As Integer, ByVal nSessionId As String, Optional ByVal dNulldate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nRateDevo As Double = 0, Optional ByVal nMovement As Integer = 1, Optional ByVal sOptReceipt As String = "", Optional ByVal sOptDev As String = "", Optional ByVal nPayFreq As Integer = 0, Optional ByVal nOption As Integer = 0, Optional ByVal sRehabProc As String = "", Optional ByVal sAdicCover As String = "") As Collection
		Dim lcolTDetail_pres As TDetail_pres
		Dim lclsProduct_li As eProduct.Product
		Dim lstrKey As String
		
		On Error GoTo InsPreCA027_Err
		lclsProduct_li = New eProduct.Product
		
		lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate)
		
		'lclsProduct_li.nProdClas = 7 Then
		If nTransaction = 31 Then
			lcolTDetail_pres = New TDetail_pres
			lstrKey = lcolTDetail_pres.sKey(nUsercode, nSessionId, False)
			'UPGRADE_NOTE: Object lcolTDetail_pres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolTDetail_pres = Nothing
			InsPreCA027 = InsCalReceiptRehabilitate(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTransaction, lstrKey, nOption, nUsercode, sRehabProc, sAdicCover)
		Else
			InsPreCA027 = InsCalReceiptMod(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nTransaction, dLedgerDate, dNulldate, nRateDevo, nMovement, sOptReceipt, sOptDev, nPayFreq, CStr(nOption))
		End If
		
InsPreCA027_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object InsPreCA027 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			InsPreCA027 = Nothing
		End If
		'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_li = Nothing
		On Error GoTo 0
	End Function
	
	'% InsCalReceiptRehabilitate: Calcula el recibo producto de la rehabilitaci�n
	Private Function InsCalReceiptRehabilitate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransactio As Integer, ByVal sKey As String, ByVal nOption As Integer, ByVal nUsercode As Integer, Optional ByVal sRehabProc As String = "", Optional ByVal sAdicCover As String = "") As Collection
		Dim lrecRehabilitate As eRemoteDB.Execute
		Dim lclsReceipt As TDetail_pre
		
		'+ Definici�n de store procedure InsCalReceiptRehabilitate al 05-13-2002 12:27:48
		On Error GoTo InsCalReceiptRehabilitate_Err
		lrecRehabilitate = New eRemoteDB.Execute
		With lrecRehabilitate
			.StoredProcedure = "InsCalReceiptRehabilitate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRehabProc", sRehabProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdicCover", sAdicCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				InsCalReceiptRehabilitate = New Collection
				Do While Not .EOF
					lclsReceipt = New TDetail_pre
					lclsReceipt.sDocument = .FieldToClass("sDocument")
					lclsReceipt.sDescript = .FieldToClass("sDescript")
					lclsReceipt.nBill_item = .FieldToClass("nBill_item")
					lclsReceipt.nPremium = .FieldToClass("nPremium")
					lclsReceipt.nPremium_an = lclsReceipt.nPremium_an + .FieldToClass("nPremium_an")
					lclsReceipt.sType_detai = .FieldToClass("sType_Detai")
					lclsReceipt.nP_NotCons = lclsReceipt.nP_NotCons + .FieldToClass("np_NotCons")
					lclsReceipt.nP_Outstand = lclsReceipt.nP_Outstand + .FieldToClass("np_OutStand")
					lclsReceipt.nP_Adjust = lclsReceipt.nP_Adjust + .FieldToClass("np_Adjust")
					lclsReceipt.nAmountAf = .FieldToClass("nAmountAf")
					lclsReceipt.nAmountEx = .FieldToClass("nAmountEx")
					lclsReceipt.nCommision = .FieldToClass("nCommision")
					lclsReceipt.nCurrency = .FieldToClass("nCurrency")
					lclsReceipt.dExpirdat = .FieldToClass("dExpirdat")
					lclsReceipt.dEffecdate = .FieldToClass("dEffecdate")
					lclsReceipt.nReceipt = .FieldToClass("nReceipt")
					'+ Permite asignar a la propiedad de s�lo lectura "sReceipts" todos los n�meros de recibos
					'+ disponibles separados por coma.
					If InStr(lclsReceipt.sReceipts, CStr(lclsReceipt.nReceipt)) = 0 Then
						lclsReceipt.sReceipts = lclsReceipt.sReceipts & IIf(sReceipts = String.Empty, String.Empty, ",") & lclsReceipt.nReceipt
					End If
					InsCalReceiptRehabilitate.Add(lclsReceipt)
					'UPGRADE_NOTE: Object lclsReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReceipt = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecRehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRehabilitate = Nothing
InsCalReceiptRehabilitate_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object InsCalReceiptRehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			InsCalReceiptRehabilitate = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'% InsCalReceiptMod: Calcula los recibos producto de la modificaci�n o anulaci�n
	Public Function InsCalReceiptMod(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal dLedgerDate As Date, ByVal dNulldate As Date, ByVal nRateDevo As Double, ByVal nMovement As Integer, ByVal sOptReceipt As String, ByVal sOptDev As String, ByVal nPayFreq As Integer, ByVal sTypeexec As String) As Collection
		Dim lrecReceipt As eRemoteDB.Execute
		Dim lclsReceipt As ePolicy.TDetail_pre
		Dim lclsCertificat As Certificat
		Dim lclsPolicy As Policy
		Dim lclsProduct As eProduct.Product
		Dim llngFact As Integer
		Dim lstrDescript As String
		
		On Error GoTo InsCalReceiptMod_Err
		lrecReceipt = New eRemoteDB.Execute
		InsCalReceiptMod = New Collection
		lclsPolicy = New Policy
		lclsProduct = New eProduct.Product
		lclsCertificat = New Certificat
		
		Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
		Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
		Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		llngFact = IIf(lclsPolicy.sPolitype = "1" Or (nCertif <> 0 And lclsPolicy.sColinvot = "2") Or nCertif = 0, 1, 0)
		
		'+ Si el Recibo de la devoluci�n es autom�tico
		If sOptReceipt = "2" Then
			If sOptDev = "1" Then
				'+ Si la devoluci�n va a ser calculada por el m�todo de Prorrata
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "6", "2")
				
			ElseIf sOptDev = "2" Then 
				'+ Si la devoluci�n va a ser calculada por el m�todo de Corto Plazo
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "5", "3")
				
			ElseIf sOptDev = "3" Then 
				'+ Si la devoluci�n va a ser calculada por un porcentaje fijo
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "7", "4")
            ElseIf sOptDev = "9" Then
                '+ Si la devoluci�n va a ser calculada por un porcentaje fijo
                lclsPolicy.sProrShort = "9"

			End If
		End If
		
		'+ Si la transaccion es cambio de frecuencia de pago (61)
		'+ la frecuencia corresponde a la antigua ya que la nueva se saca de policy
		If nTransaction = 61 Then
			lclsCertificat.nPayFreq = nPayFreq
		End If
		
		'+ Definici�n de par�metros para stored procedure 'insudb.InsCalReceiptMod'
		'+ Informaci�n le�da el 01/12/1999 02:45:24 PM
		With lrecReceipt
			.StoredProcedure = "InsCalReceiptMod"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_rtPol", lclsPolicy.sProrShort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdatePol", lclsPolicy.dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdateCer", lclsCertificat.dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdatPol", lclsPolicy.dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdatCer", lclsCertificat.dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNextReceipPol", IIf((lclsPolicy.dNextReceip = eRemoteDB.Constants.dtmNull), eRemoteDB.Constants.dtmNull, lclsPolicy.dNextReceip), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNextReceipCer", IIf((lclsCertificat.dNextReceip = eRemoteDB.Constants.dtmNull), eRemoteDB.Constants.dtmNull, lclsCertificat.dNextReceip), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNullDateMod", IIf(dNulldate = eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, dNulldate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNull_cover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", lclsProduct.sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFact", llngFact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", lclsCertificat.nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", lclsCertificat.nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", lclsPolicy.sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lclsCertificat.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDirDebit", lclsPolicy.sDirdebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", lclsPolicy.nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", lclsPolicy.nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColinvot", IIf((lclsPolicy.sColinvot = String.Empty), String.Empty, lclsPolicy.sColinvot), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStylePrem", lclsProduct.sStyle_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_comm", lclsProduct.sStyle_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_tax", lclsProduct.sStyle_tax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateDevo", nRateDevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysFQ", lclsPolicy.nDaysFQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysSQ", lclsPolicy.nDaysSQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotes", lclsPolicy.nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nparticip", lclsPolicy.nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedgerDate", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", lclsCertificat.nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeexec", sTypeexec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_Origi", lclsCertificat.dDate_Origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceipt_ind", lclsPolicy.sReceipt_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOficial_p", lclsPolicy.nOficial_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsReceipt = New ePolicy.TDetail_pre
					
SameItem: 
					lstrDescript = .FieldToClass("sDescript")
					lclsReceipt.sDocument = .FieldToClass("sDocument")
					lclsReceipt.sDescript = .FieldToClass("sDescript")
					lclsReceipt.nBill_item = .FieldToClass("nBill_item")
					lclsReceipt.nPremium = .FieldToClass("nPremium")
					lclsReceipt.nPremium_an = lclsReceipt.nPremium_an + .FieldToClass("nPremium_an")
					lclsReceipt.sType_detai = .FieldToClass("sType_Detai")
					lclsReceipt.nP_NotCons = lclsReceipt.nP_NotCons + .FieldToClass("np_NotCons")
					lclsReceipt.nP_Outstand = lclsReceipt.nP_Outstand + .FieldToClass("np_OutStand")
					lclsReceipt.nP_Adjust = lclsReceipt.nP_Adjust + .FieldToClass("np_Adjust")
					lclsReceipt.nAmountAf = lclsReceipt.nAmountAf + .FieldToClass("nAmountAf")
					lclsReceipt.nAmountEx = lclsReceipt.nAmountEx + .FieldToClass("nAmountEx")
					lclsReceipt.nCommision = .FieldToClass("nCommision")
					lclsReceipt.nCurrency = .FieldToClass("nCurrency")
					lclsReceipt.dExpirdat = .FieldToClass("dExpirdat")
					lclsReceipt.dEffecdate = .FieldToClass("dEffecdate")
					lclsReceipt.nReceipt = .FieldToClass("nReceipt")
					lclsReceipt.sKey = .FieldToClass("sKey")
					
					'+ Permite asigna a la propiedad de s�lo lectura "Receipts" todos los n�meros de recibos
					'+ disponibles separados por coma.
					If InStr(lclsReceipt.sReceipts, CStr(lclsReceipt.nReceipt)) = 0 Then
						lclsReceipt.sReceipts = lclsReceipt.sReceipts & IIf(sReceipts = String.Empty, String.Empty, ",") & lclsReceipt.nReceipt
					End If
					.RNext()
					'+ Si el pr�ximo item a tratar ya se ha especificado, se acumulan sus montos
					'+ con el objeto de motrar bajo un mismo item las sumatoria de los datos.
					If Not .EOF Then
						If .FieldToClass("sDescript") = lstrDescript Then GoTo SameItem
					End If
					InsCalReceiptMod.Add(lclsReceipt)
					'UPGRADE_NOTE: Object lclsReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReceipt = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
InsCalReceiptMod_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object InsCalReceiptMod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			InsCalReceiptMod = Nothing
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReceipt = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'% ADD: Agrega un nuevo registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreTDetail_pre As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreTDetail_pre = New eRemoteDB.Execute
		
		
		With lreccreTDetail_pre
			.StoredProcedure = "creTdetail_pre"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddsuini", sAddsuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_rate", nCommi_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommision", nCommision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapi_ini", nCapi_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium_an", nPremium_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_notcons", nP_NotCons, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_outstand", nP_Outstand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_adjust", nP_Adjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_ini", nPrem_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_act", nPrem_act, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_anu", nCommi_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddtax", sAddtax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_bill", nId_Bill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_idbill", nRel_IdBill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxiva", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsucount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxmar", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountiva", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxtarif", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiuma", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiume", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTDetail_pre = Nothing
	End Function
	
	'% Find: Busca los datos en la tabla
	Public Function Find(ByVal sKey As String, ByVal sType_detai As String, ByVal nDet_code As Integer) As Boolean
		Dim lrecreaTDetail_pre As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaTDetail_pre = New eRemoteDB.Execute
		
		With lrecreaTDetail_pre
			.StoredProcedure = "reaTDetail_pre"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDet_code", nDet_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sKey = .FieldToClass("sKey")
				sType_detai = .FieldToClass("sType_detai")
				nDet_code = .FieldToClass("nCode")
				nCode = .FieldToClass("nCode")
				nCurrency = .FieldToClass("nCurrency")
				nBill_item = .FieldToClass("nBill_item")
				nBranch_est = .FieldToClass("nBranch_est")
				nBranch_led = .FieldToClass("nBranch_led")
				nBranch_rei = .FieldToClass("nBranch_rei")
				sAddsuini = .FieldToClass("sAddsuini")
				nModulec = .FieldToClass("nModulec")
				nCommi_rate = .FieldToClass("nCommi_rate")
				nCommision = .FieldToClass("nCommision")
				nTax = .FieldToClass("nTax")
				nCapi_ini = .FieldToClass("nCapi_ini")
				nCapital = .FieldToClass("nCapital")
				nPremium = .FieldToClass("nPremium")
				nPremium_an = .FieldToClass("nPremium_an")
				nP_NotCons = .FieldToClass("nP_NotCons")
				nP_Outstand = .FieldToClass("nP_Outstand")
				nP_Adjust = .FieldToClass("nP_Adjust")
				nPrem_ini = .FieldToClass("nPrem_ini")
				nPrem_act = .FieldToClass("nPrem_act")
				nCommi_anu = .FieldToClass("nCommi_anu")
				sAddtax = .FieldToClass("sAddTax")
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTDetail_pre = Nothing
	End Function
	
	'% Update: Actualiza la informaci�n de la tabla
	Public Function Update() As Boolean
		Dim lrecinsUpdTDetail_pre As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsUpdTDetail_pre = New eRemoteDB.Execute
		
		With lrecinsUpdTDetail_pre
			.StoredProcedure = "insUpdTDetail_pre"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddsuini", sAddsuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_rate", nCommi_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommision", nCommision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapi_ini", nCapi_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium_an", nPremium_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_NotCons", nP_NotCons, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_Outstand", nP_Outstand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_Adjust", nP_Adjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_ini", nPrem_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_act", nPrem_act, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_anu", nCommi_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_Bill", nId_Bill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddTax", sAddtax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumA", nPremiumA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumE", nPremiumE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_det", nPrem_det, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrem_det", sPrem_det, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTDetail_pre = Nothing
	End Function

	'% insPrem_det: se distribuye la prima del rec/desc/imp entre los elementos sobre los cuales
	'%              aplica
	Private Function insPrem_det(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPrem_det_old As Short, Optional ByVal nOrigin As Integer = 0) As Boolean
		Dim lrecRemote As eRemoteDB.Execute

		On Error GoTo insPrem_det_err

		lrecRemote = New eRemoteDB.Execute

		With lrecRemote

			If nOrigin = 2 Then
				.StoredProcedure = "inspostCA080Upd"
			Else
				.StoredProcedure = "inspostCA028Upd"
			End If

			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddsuini", sAddsuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_rate", nCommi_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommision", nCommision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapi_ini", nCapi_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium_an", nPremium_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_NotCons", nP_NotCons, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_Outstand", nP_Outstand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nP_Adjust", nP_Adjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_ini", nPrem_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_act", nPrem_act, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommi_anu", nCommi_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_Bill", nId_Bill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddTax", sAddtax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumA", nPremiumA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumE", nPremiumE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_det", nPrem_det, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_det_old", nPrem_det_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrem_det", sPrem_det, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPrem_det = .Run(False)
		End With

insPrem_det_err:
		If Err.Number Then
			insPrem_det = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRemote = Nothing
	End Function

	'% insValCA028_K: Realiza la validaci�n de los campos del encabezado de la ventana
	Public Function insValCA028_K(ByVal sCodispl As String, ByVal nBranch As String, ByVal nProduct As String, ByVal nPolicy As String, ByVal nCertif As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lclsPrem_annuities As ePolicy.Prem_annuities
		Dim lclsProduct_li As eProduct.Product
		Dim lblnValid As Boolean
		
		On Error GoTo insValCA028_k_Err
		
		lobjErrors = New eFunctions.Errors
		lclsPolicy = New ePolicy.Policy
		lclsCertificat = New ePolicy.Certificat
		lclsPrem_annuities = New Prem_annuities
		lclsProduct_li = New eProduct.Product
		
		lblnValid = True
		
		'+ El ramo debe estar lleno
		If nBranch = CStr(eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 9064)
			lblnValid = False
		End If
		
		'+ El producto debe estar lleno
		If nProduct = CStr(eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
			lblnValid = False
		End If
		
		'+ La p�liza debe estar llena
		If nPolicy = CStr(eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 3003)
		Else
			If lblnValid Then
				With lclsPolicy
					'+ La p�liza debe corresponder con un registro v�lido
					If Not .Find("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy)) Then
						Call lobjErrors.ErrorMessage(sCodispl, 3001)
						lblnValid = False
					Else
						'+ La p�liza no puede estar anulada
						If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3063)
						Else
							'+ La p�liza debe estar en un estado v�lido
							If .sStatus_pol <> "1" And .sStatus_pol <> "4" And .sStatus_pol <> "5" Then
								Call lobjErrors.ErrorMessage(sCodispl, 3882)
							End If
						End If
						
						If nCertif = CStr(eRemoteDB.Constants.intNull) Then
							'+ El certificado debe estar lleno, si no corresponde a una p�liza individual
							If .sPolitype <> "1" Then
								Call lobjErrors.ErrorMessage(sCodispl, 3006)
							End If
						Else
							If CDbl(nCertif) > 0 Then
								'+S�lo se permite certificado si recibo/facturacion es 2-Por Certificado
								If lclsPolicy.sColinvot <> "2" Then
									Call lobjErrors.ErrorMessage(sCodispl, 750043)
								Else
									With lclsCertificat
										If .Find("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy), CDbl(nCertif)) Then
											'+ El certificado debe estar en un estado v�lido
											If .sStatusva <> "1" And .sStatusva <> "4" And .sStatusva <> "5" Then
												Call lobjErrors.ErrorMessage(sCodispl, 3883)
											End If
										Else
											'+ Si el certificado no existe
											Call lobjErrors.ErrorMessage(sCodispl, 3010)
										End If
									End With
								End If
							End If
						End If
					End If
				End With
				
				If lblnValid Then
					If lclsProduct_li.FindProduct_li(CInt(nBranch), CInt(nProduct), Today) Then
						'+ Si el producto es de rentas vitalicias, la p�liza debe tener bono de reconocimiento o
						'+ complemento de bono de reconocimiento (Table5600)
						If lclsProduct_li.nProdClas = 9 Or lclsProduct_li.nProdClas = 10 Then
							If lclsPrem_annuities.valPrem_annuities_Bonus("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy)) Then
								'+ La p�liza debe tener bono de reconocimiento
								If Not lclsPrem_annuities.bBonus Then
									Call lobjErrors.ErrorMessage(sCodispl, 55914)
								End If
								'+ La p�liza debe tener complemento de bono de reconocimiento
								If Not lclsPrem_annuities.bCBonus Then
									Call lobjErrors.ErrorMessage(sCodispl, 55915)
								End If
							End If
						End If
					End If
				End If
			End If
		End If
		
		insValCA028_K = lobjErrors.Confirm
		
insValCA028_k_Err: 
		If Err.Number Then
			insValCA028_K = "insValCA028_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPrem_annuities = Nothing
		'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_li = Nothing
	End Function
	
	'% insValCA028: Realiza la validaci�n de los campos de la zona de detalle de la ventana
	Public Function insValCA028(ByVal sWindowType As String, ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal nRecordCount As Integer = 0, Optional ByVal dStartdate As Date = #12:00:00 AM#, Optional ByVal dExpirdat As Date = #12:00:00 AM#, Optional ByVal nReceipt As Double = 0, Optional ByVal dIssuedate As Date = #12:00:00 AM#, Optional ByVal nSource As Integer = 0, Optional ByVal sOrigReceipt As String = "", Optional ByVal nCapital As Double = 0, Optional ByVal nCommi_rate As Double = 0, Optional ByVal nCommission As Double = 0, Optional ByVal sType As String = "", Optional ByVal sCacalili As String = "", Optional ByVal sCommissi_i As String = "", Optional ByVal nPremiumA As Double = 0, Optional ByVal nPremiumE As Double = 0, Optional ByVal nPrem_det As Short = 0, Optional ByVal sPrem_det As String = "", Optional ByVal nDisexprc As Integer = 0, Optional ByVal sAdjust As String = "", Optional ByVal nAdjReceipt As Double = 0, Optional ByVal nAdjAmount As Double = 0, Optional ByVal sUpdated As String = "") As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCommission As ePolicy.Commission
		Dim lclsPremium As Object
        Dim lclsCertificat As ePolicy.Certificat = New ePolicy.Certificat
        Dim lclsDsex_condi As eProduct.Dsex_condi
		
		Dim lblnCommAsso As Boolean
		Dim lblnPolicyExist As Boolean
		Dim ldblamountmax As Double
		
		On Error GoTo insValCA028_Err
		
		lobjErrors = New eFunctions.Errors
		
		If sWindowType = "PopUp" Then
			'+ Validaciones del capital
			If nCapital = eRemoteDB.Constants.intNull Then
				If sType = "1" And sCacalili = "2" Then
					If nPremiumA <> eRemoteDB.Constants.intNull Or nPremiumE <> eRemoteDB.Constants.intNull Then
						'+ Si se trata de una cobertura, no se indic� capital ilimitado y se indic� importe de prima
						'+ debe estar lleno
						Call lobjErrors.ErrorMessage(sCodispl, 3819)
					End If
				End If
			Else
				If sType = "1" Then
					'+ Si se trata de una cobertura, y se indic� capital ilimitado, no debe tener valor
					If sCacalili = "1" Then
						Call lobjErrors.ErrorMessage(sCodispl, 3818,  , eFunctions.Errors.TextAlign.LeftAling, "Capital:")
					End If
				Else
					'+ Si no se trata de una cobertura, no debe tener valor
					Call lobjErrors.ErrorMessage(sCodispl, 3817,  , eFunctions.Errors.TextAlign.LeftAling, "Capital:")
				End If
			End If
			
			'+ Validaciones del % de comisi�n y monto de comisi�n fija
			If nCommission <> eRemoteDB.Constants.intNull And nCommi_rate <> eRemoteDB.Constants.intNull Then
				'+ Debe indicar % o Monto de comisi�n, no ambos
				Call lobjErrors.ErrorMessage(sCodispl, 5113)
			Else
				If nCommission = eRemoteDB.Constants.intNull And nCommi_rate = eRemoteDB.Constants.intNull Then
					lclsCommission = New ePolicy.Commission
					'+ Se verifica si la p�liza tiene una comisi�n asociada
					lblnCommAsso = lclsCommission.Find_CommAsso("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					If sType = "1" Then
						'+ Si se trata de una cobertura, y la p�liza tiene una comisi�n asociada, debe estar lleno
						If lblnCommAsso Then
							Call lobjErrors.ErrorMessage(sCodispl, 3821)
						End If
					ElseIf sType = "2" Or sType = "3" Then 
						'+ Si se trata de un recargo/descuento, y la p�liza tiene una comisi�n asociada,
						'+ y en el producto se indic� que el recargo/descuento participa en la comisi�n,
						'+ debe estar lleno
						If lblnCommAsso And sCommissi_i = "1" Then
							Call lobjErrors.ErrorMessage(sCodispl, 3821)
						End If
					End If
				Else
					'+ Si no se indic� prima a facturar, el % o el monto de comisi�n no deben estar llenos
					If nPremiumA = eRemoteDB.Constants.intNull And nPremiumE = eRemoteDB.Constants.intNull Then
						If nCommi_rate <> eRemoteDB.Constants.intNull Then
							If sType <> "4" Then
								Call lobjErrors.ErrorMessage(sCodispl, 13865,  , eFunctions.Errors.TextAlign.LeftAling, "% Comisi�n:")
							End If
						End If
						If nCommission <> eRemoteDB.Constants.intNull Then
							If sType <> "4" Then
								Call lobjErrors.ErrorMessage(sCodispl, 13865,  , eFunctions.Errors.TextAlign.LeftAling, "Comisi�n fija:")
							End If
						End If
					End If
					'+ Si se trata de un impuesto, el % o el monto de comisi�n no deben estar llenos
					If sType = "4" Then
						If nCommi_rate <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3820,  , eFunctions.Errors.TextAlign.LeftAling, "% Comisi�n:")
						End If
						If nCommission <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3820,  , eFunctions.Errors.TextAlign.LeftAling, "Comisi�n fija:")
						End If
					End If
				End If
			End If
			
			If nPremiumA = eRemoteDB.Constants.intNull And nPremiumE = eRemoteDB.Constants.intNull Then
				If nPrem_det = 1 Or nPrem_det = 3 Then
					'+ Debe indicarse monto de prima a facturar (Afecta o exenta) si el campo "prima por desglose"
					'+ tiene valor = "Distribuir entre los detalles" o "No hay desglose"
					Call lobjErrors.ErrorMessage(sCodispl, 55614)
				Else
					'+ Si el campo "prima por desglose" tiene valor = "Detallar prima", se debe haber generado el detalle
					sPrem_det = IIf(sPrem_det = String.Empty, "2", sPrem_det)
					If sPrem_det = "2" Then
						Call lobjErrors.ErrorMessage(sCodispl, 56039)
					End If
				End If
			End If
			
			If nPrem_det = 3 Then
				lclsDsex_condi = New eProduct.Dsex_condi
				If lclsDsex_condi.valExist_product(nBranch, nProduct, nDisexprc, dIssuedate) Then
					Call lobjErrors.ErrorMessage(sCodispl, 56172)
				End If
			End If
			
			'+Si no es ventana popup
		Else
			
			lclsPolicy = New ePolicy.Policy
			
			'+ Se debe haber seleccionado una linea
			If nRecordCount = 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 3814)
			End If
			
			lblnPolicyExist = lclsPolicy.Find("2", nBranch, nProduct, nPolicy)
			
			If nCertif <> 0 Then
				lclsCertificat = New ePolicy.Certificat
				Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)
			End If
			
			'+ Fecha de vigencia - hasta debe ser posterior o igual a fecha de vigencia - desde
			If dStartdate > dExpirdat Then
				Call lobjErrors.ErrorMessage(sCodispl, 11425)
			Else
				'+ Validaciones de la fecha de Vigencia - Desde
				Call insvalDatepolicy(sCodispl, lobjErrors, "2", nBranch, nProduct, nPolicy, nCertif, dStartdate, False, lclsPolicy, lclsCertificat, "Vigencia - Desde")
				
				'+ Validaciones de la fecha de Vigencia - Hasta
				Call insvalDatepolicy(sCodispl, lobjErrors, "2", nBranch, nProduct, nPolicy, nCertif, dExpirdat, False, lclsPolicy, lclsCertificat, "Vigencia - Hasta")
			End If
			
			'+ Validaciones del recibo
			If nReceipt <> eRemoteDB.Constants.intNull Then
				lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
				With lclsPremium
					If .Find("2", nReceipt, nBranch, nProduct, 0, 0) Then
						If .nBranch <> nBranch Or .nProduct <> nProduct Or .nPolicy <> nPolicy Or .nCertif <> nCertif Then
							'+ No debe estar registrado en el sistema
							Call lobjErrors.ErrorMessage(sCodispl, 5002)
						End If
					End If
				End With
				'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPremium = Nothing
			End If
			
			'+ Validaciones de la fecha de emisi�n
			'        Call insvalDatepolicy(sCodispl, lobjErrors, "2", _
			''                              nBranch, nProduct, nPolicy, _
			''                              nCertif, dIssuedate, True, _
			''                              lclsPolicy, lclsCertificat, _
			''                              "Emisi�n")
			
			'+ Validaciones del campo Origen
			If nSource = eRemoteDB.Constants.intNull Then
				'+ Debe estar lleno
				Call lobjErrors.ErrorMessage(sCodispl, 3094)
			End If
			
			'+ Validaciones del recibo lider
			If lblnPolicyExist Then
				'+ Si la p�liza corresponde a un negocio aceptado, debe estar lleno
				If lclsPolicy.sBussityp <> "1" Then
					If sOrigReceipt = String.Empty Then
						Call lobjErrors.ErrorMessage(sCodispl, 3096)
					End If
				End If
			End If
			
			'+ Validaciones del ajuste de recibo
			If sAdjust = "1" Then
				
				'+Debe ingresar recibo a ajustar
				If nAdjReceipt = eRemoteDB.Constants.intNull Then
					Call lobjErrors.ErrorMessage(sCodispl, 7021, 0, eFunctions.Errors.TextAlign.RigthAling, " a ajustar")
				Else
					lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
					With lclsPremium
						If .Find("2", nAdjReceipt, nBranch, nProduct, 0, 0) Then
							ldblamountmax = System.Math.Abs(.nPremium)
							If .nBranch <> nBranch Or .nProduct <> nProduct Or .nPolicy <> nPolicy Or .nCertif <> nCertif Then
								'+Debe estar registrado en el sistema para la poliza
								Call lobjErrors.ErrorMessage(sCodispl, 60249, 0, eFunctions.Errors.TextAlign.RigthAling, " (Recibo a ajustar)")
							End If
							
							'+Para recibos de devolucion, el recibo original no puede estar parcialmente imputado
							If sType = "2" And Not (.nPremium = .nBalance Or .nBalance = 0) Then
								
								Call lobjErrors.ErrorMessage(sCodispl, 750141, 0, eFunctions.Errors.TextAlign.RigthAling, "(Relaciones: " & .Find_Imputations("2", nBranch, nProduct, nAdjReceipt, 0, 0) & ")")
							End If
							
						Else
							'+Debe estar registrado en el sistema
							Call lobjErrors.ErrorMessage(sCodispl, 60249, 0, eFunctions.Errors.TextAlign.RigthAling, " (Recibo a ajustar)")
						End If
					End With
					'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPremium = Nothing
				End If
				
				'+Debe ingresar Monto de ajuste
				If nAdjAmount = eRemoteDB.Constants.intNull Then
					Call lobjErrors.ErrorMessage(sCodispl, 60198, 0, eFunctions.Errors.TextAlign.RigthAling, " (Monto de ajuste)")
				Else
					'+Si es recibo de devoluci�n, el max�mo permitido a devolver es el del recibo original
					If sType = "2" Then
						If ldblamountmax > 0 And nAdjAmount > ldblamountmax Then
							Call lobjErrors.ErrorMessage(sCodispl, 5097)
						End If
					End If
				End If
			End If
			
		End If
		
		insValCA028 = lobjErrors.Confirm
		
insValCA028_Err: 
		If Err.Number Then
			insValCA028 = "insValCA028: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCommission = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function

	'%insPostCA028: Se realiza la actualizaci�n de los datos en la ventana CA028 (Folder)
	Public Function inspostCA028Upd(ByVal sCodispl As String, ByVal sKey As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nType As Integer = 0, Optional ByVal nBill_item As Integer = 0, Optional ByVal nBranch_est As Integer = 0, Optional ByVal nBranch_led As Integer = 0, Optional ByVal nBranch_rei As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nItem As Integer = 0, Optional ByVal nCommi_rate As Double = 0, Optional ByVal nCommision As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nPremiumA As Double = 0, Optional ByVal nPremiumE As Double = 0, Optional ByVal sAddsuini As String = "", Optional ByVal sOptType As String = "", Optional ByVal nId_Bill As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal sAddtax As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nSessionId As String = "", Optional ByVal nPrem_det As Short = 0, Optional ByVal nPrem_det_old As Short = 0, Optional ByVal sPrem_det As String = "") As Boolean

		On Error GoTo insPostCA028Upd_Err

		inspostCA028Upd = True


		With Me
			.dEffecdate = dEffecdate
			.sKey = sKey
			.nBill_item = nBill_item
			.nBranch_est = nBranch_est
			.nBranch_led = nBranch_led
			.nBranch_rei = nBranch_rei
			.sAddsuini = IIf(sAddsuini = String.Empty, "2", sAddsuini)
			.nModulec = nModulec
			.nCode = nItem
			.sType_detai = CStr(nType)
			.sClient = sClient

			If sAction = "Del" Then
				nPremium = eRemoteDB.Constants.intNull
				.nCapi_ini = eRemoteDB.Constants.intNull
				.nCapital = eRemoteDB.Constants.intNull
				.nCommi_anu = eRemoteDB.Constants.intNull
				.nCommi_rate = eRemoteDB.Constants.intNull
				.nCommision = eRemoteDB.Constants.intNull
				.nCurrency = eRemoteDB.Constants.intNull
				.nP_Adjust = eRemoteDB.Constants.intNull
				.nP_NotCons = eRemoteDB.Constants.intNull
				.nP_Outstand = eRemoteDB.Constants.intNull
				.nPrem_act = eRemoteDB.Constants.intNull
				.nPrem_ini = eRemoteDB.Constants.intNull
				.nPremium = eRemoteDB.Constants.intNull
				.nPremium_an = eRemoteDB.Constants.intNull
				.nPremiumA = eRemoteDB.Constants.intNull
				.nPremiumE = eRemoteDB.Constants.intNull
				.nTax = eRemoteDB.Constants.intNull
				.nId_Bill = eRemoteDB.Constants.intNull
				.sAddtax = String.Empty
			Else
				nPremium = IIf(nPremiumA = eRemoteDB.Constants.intNull, 0, nPremiumA) + IIf(nPremiumE = eRemoteDB.Constants.intNull, 0, nPremiumE)

				'+ Si se trata de un descuento se coloca la prima en negativo
				If nType = CDbl("3") Then
					nPremium = (nPremium * -1)
				End If

				'+ Si el recibo es de devoluci�n se coloca la prima negativa
				If sOptType = "2" Then
					nPremium = (nPremium * -1)
				End If

				.nCapi_ini = nCapital
				.nCapital = nCapital
				.nCommi_anu = nCommi_rate
				.nCommi_rate = nCommi_rate
				.nCommision = nCommision
				.nCurrency = nCurrency
				.nP_Adjust = 0
				.nP_NotCons = 0
				.nP_Outstand = 0
				.nPrem_act = nPremium
				.nPrem_ini = nPremium
				.nPremium = nPremium
				.nPremium_an = nPremium
				.nPremiumA = nPremiumA
				.nPremiumE = nPremiumE
				.nTax = 0
				.nId_Bill = nId_Bill
				.sAddtax = IIf(sAddtax = String.Empty, "2", "1")
			End If
			.nPrem_det = nPrem_det
			.sPrem_det = sPrem_det

			inspostCA028Upd = insPrem_det(nBranch, nProduct, nPrem_det_old, 1)
		End With

insPostCA028Upd_Err:
		If Err.Number Then
			inspostCA028Upd = False
		End If
		On Error GoTo 0
	End Function

	Public Function inspostCA080Upd(ByVal sCodispl As String, ByVal sKey As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nType As Integer = 0, Optional ByVal nBill_item As Integer = 0, Optional ByVal nBranch_est As Integer = 0, Optional ByVal nBranch_led As Integer = 0, Optional ByVal nBranch_rei As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nItem As Integer = 0, Optional ByVal nCommi_rate As Double = 0, Optional ByVal nCommision As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nPremiumA As Double = 0, Optional ByVal nPremiumE As Double = 0, Optional ByVal sAddsuini As String = "", Optional ByVal sOptType As String = "", Optional ByVal nId_Bill As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal sAddtax As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nSessionId As String = "", Optional ByVal nPrem_det As Short = 0, Optional ByVal nPrem_det_old As Short = 0, Optional ByVal sPrem_det As String = "", Optional ByVal nReceiptCollec As Double = 0) As Boolean
		Dim lcolTDetail_pre As TDetail_pres

		On Error GoTo inspostCA080Upd_err

		inspostCA080Upd = True

		lcolTDetail_pre = New TDetail_pres

		If nCommi_rate < 0 Then
			nCommi_rate = 0
		End If

		With Me
			.dEffecdate = dEffecdate
            '.sKey = lcolTDetail_pre.sKey(nUsercode, nSessionId, False) Modified by DMendoza 16/07/2021
            .sKey = sKey
            .nBill_item = nBill_item
            .nBranch_est = nBranch_est
            .nBranch_led = nBranch_led
            .nBranch_rei = nBranch_rei
            .sAddsuini = IIf(sAddsuini = vbNullString, "2", sAddsuini)
            .nModulec = nModulec
            .nCode = nItem
            .sType_detai = nType
            .sClient = sClient
            .nReceiptCollec = nReceiptCollec

            If sAction = "Del" Then
                nPremium = eRemoteDB.Constants.intNull
                .nCapi_ini = eRemoteDB.Constants.intNull
                .nCapital = eRemoteDB.Constants.intNull
                .nCommi_anu = eRemoteDB.Constants.intNull
                .nCommi_rate = eRemoteDB.Constants.intNull
                .nCommision = eRemoteDB.Constants.intNull
                .nCurrency = eRemoteDB.Constants.intNull
                .nP_Adjust = eRemoteDB.Constants.intNull
                .nP_NotCons = eRemoteDB.Constants.intNull
                .nP_Outstand = eRemoteDB.Constants.intNull
                .nPrem_act = eRemoteDB.Constants.intNull
                .nPrem_ini = eRemoteDB.Constants.intNull
                .nPremium = eRemoteDB.Constants.intNull
                .nPremium_an = eRemoteDB.Constants.intNull
                .nPremiumA = eRemoteDB.Constants.intNull
                .nPremiumE = eRemoteDB.Constants.intNull
                .nTax = eRemoteDB.Constants.intNull
                .nId_Bill = eRemoteDB.Constants.intNull
                .sAddtax = vbNullString
            Else
                nPremium = IIf(nPremiumA = eRemoteDB.Constants.intNull, 0, nPremiumA) + IIf(nPremiumE = eRemoteDB.Constants.intNull, 0, nPremiumE)
                nPremiumA = IIf(nPremiumA = eRemoteDB.Constants.intNull, 0, nPremiumA)
                nPremiumE = IIf(nPremiumE = eRemoteDB.Constants.intNull, 0, nPremiumE)

                '+ Si se trata de un descuento se coloca la prima en negativo
                If nType = "3" Or nType = "6" Then
                    nPremium = (nPremium * -1)
                    nPremiumA = (nPremiumA * -1)
                    nPremiumE = (nPremiumE * -1)
                    If nCommision > 0 Then
                        nCommision = (nCommision * -1)
                    End If
                End If

                '+ Si el recibo es de devoluci�n se coloca la prima negativa
                If sOptType = "2" Then
                    nPremium = (nPremium * -1)
                    nPremiumA = (nPremiumA * -1)
                    nPremiumE = (nPremiumE * -1)
                    If nCommision > 0 Then
                        nCommision = (nCommision * -1)
                    End If
                End If

                .nCapi_ini = nCapital
                .nCapital = nCapital
                .nCommi_anu = (nPremium * nCommi_rate) / 100
                .nCommi_rate = nCommi_rate

                If (nCommision <> 0) And (nCommision <> eRemoteDB.Constants.intNull) Then
                    .nCommision = nCommision
                Else
                    If nType <> 4 Then
                        .nCommision = (nPremium * nCommi_rate) / 100
                    End If
                End If

                .nCurrency = nCurrency
                .nP_Adjust = 0
                .nP_NotCons = 0
                .nP_Outstand = 0
                .nPrem_act = nPremium
                .nPrem_ini = nPremium
                .nPremium = nPremium
                .nPremium_an = nPremium
                .nPremiumA = nPremiumA
                .nPremiumE = nPremiumE
                .nTax = 0
                .nId_Bill = nId_Bill
                .sAddtax = IIf(sAddtax = vbNullString, "2", "1")
            End If
            .nPrem_det = nPrem_det
            .sPrem_det = sPrem_det

            inspostCA080Upd = insPrem_det(nBranch, nProduct, nPrem_det_old, 2)
        End With

inspostCA080Upd_err:
        lcolTDetail_pre = Nothing
    End Function

    '% CreManReceipt: Funci�n que se utiliza para la emisi�n del recibo manual
    Public Function CreManReceipt() As Boolean
        Dim lrecinsManreceipt As eRemoteDB.Execute

        On Error GoTo CreManReceipt_Err

        lrecinsManreceipt = New eRemoteDB.Execute

        With lrecinsManreceipt
            .StoredProcedure = "insManreceipt"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigReceipt", sOrigReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypExecute", sTypExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAdjust", sAdjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAdjReceipt", nAdjReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypePay", nTypepay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertypePay", sCertypePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranchPay", nBranchpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProductPay", nProductpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicyPay", nPolicypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertifPay", nCertifpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientPay", sClientpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            CreManReceipt = .Run(False)
        End With

CreManReceipt_Err:
        If Err.Number Then
            CreManReceipt = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsManreceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsManreceipt = Nothing
    End Function

    '%insPostCA028: Se realiza la actualizaci�n de los datos en la ventana CA028 (Folder)
    Public Function insPostCA028(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dExpirDate As Date, ByVal nCurrency As Integer, ByVal sClient As String, ByVal nReceipt As Double, ByVal nTratypei As Integer, ByVal nType As Integer, ByVal sOrigReceipt As String, ByVal nUsercode As Integer, ByVal sTypExecute As String, ByVal sDelReceipt As String, ByVal sKey As String, ByVal sAdjust As String, ByVal nAdjReceipt As Double, ByVal nAdjAmount As Double, ByVal nTypepay As Integer, Optional ByVal sCertypePay As String = "", Optional ByVal nBranchpay As Integer = 0, Optional ByVal nProductpay As Integer = 0, Optional ByVal nPolicypay As Double = 0, Optional ByVal nCertifpay As Double = 0, Optional ByVal sClientpay As String = "") As Boolean
        Dim lclsGeneral As eGeneral.GeneralFunction

        On Error GoTo insPostCA028_Err

        If sDelReceipt = "1" Then
            insPostCA028 = insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt)
        Else
            If nReceipt <> eRemoteDB.Constants.intNull Then
                Call insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt)
            Else
                lclsGeneral = New eGeneral.GeneralFunction
                nReceipt = lclsGeneral.Find_Numerator(4, 0, nUsercode, sCertype, nBranch, nProduct, 0, 0)
                'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsGeneral = Nothing
            End If

            Me.nReceipt = nReceipt
            Me.sCertype = sCertype
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nPolicy = nPolicy
            Me.nCertif = nCertif
            Me.dEffecdate = dEffecdate
            Me.dExpirdat = dExpirDate
            Me.nCurrency = nCurrency
            Me.sKey = sKey
            Me.nProctype = 61 '?????????????
            Me.sClient = sClient
            Me.nUsercode = nUsercode
            Me.nTratypei = nTratypei
            Me.nType = nType
            Me.sOrigReceipt = sOrigReceipt
            Me.sTypExecute = sTypExecute
            Me.sAdjust = sAdjust
            Me.nAdjReceipt = nAdjReceipt
            Me.nTypepay = nTypepay
            Me.sCertypePay = sCertypePay
            Me.nBranchpay = nBranchpay
            Me.nProductpay = nProductpay
            Me.nPolicypay = nPolicypay
            Me.nCertifpay = nCertifpay
            Me.sClientpay = sClientpay
            insPostCA028 = Me.CreManReceipt
        End If

insPostCA028_Err:
        If Err.Number Then
            insPostCA028 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGeneral = Nothing
    End Function

    Public Function insPostCA080(ByVal sCertype As String,
                             ByVal nBranch As Integer,
                             ByVal nProduct As Integer,
                             ByVal nPolicy As Double,
                             ByVal nCertif As Double,
                             ByVal dEffecdate As Date,
                             ByVal sPolitype As String,
                             ByVal nCurrency As Integer,
                             ByVal sClient As String,
                             ByVal dExpirDate As Date,
                             ByVal nReceipt As Double,
                             ByVal nRecrelatedcoll As Double,
                             ByVal dIssueDat As Date,
                             ByVal dStarDate As Date,
                             ByVal nProvince As Integer,
                             ByVal nTratypei As Integer,
                             ByVal nType As Integer,
                             ByVal sOrigReceipt As String,
                             ByVal nSessionId As String,
                             ByVal nUsercode As Integer,
                             ByVal sTypExecute As String,
                             ByVal sDelreceipt As String,
                             ByVal sExist As String,
                             Optional ByVal sOnSeq As String = "",
                             Optional ByVal sDevReceipt As String = "2",
                             Optional ByVal nProceedingNum As Double = 0,
                             Optional ByVal nContrat As Double = 0,
                             Optional ByVal nDraft As Integer = 0,
                             Optional ByVal skey As String = "") As Boolean

        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lcolTDetail_pre As ePolicy.TDetail_pres
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim nTransactio_aux As Long

        On Error GoTo insPostCA080_err

        lclsPolicy = New ePolicy.Policy
        lclsPolicy_his = New ePolicy.Policy_his

        If sDelreceipt = "1" Then
            insPostCA080 = insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, 2)
        Else
            If nReceipt <> eRemoteDB.Constants.intNull Then
                Call insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, 2)
            End If

            If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                Me.sPolitype = lclsPolicy.sPolitype
                Me.sColinvot = lclsPolicy.sColinvot
            End If

            If sOnSeq = "1" Or (Me.sPolitype = 2 And Me.sColinvot <> "2") Then
                insPostCA080 = lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif)
                nTransactio_aux = lclsPolicy_his.nTransactio
            Else
                insPostCA080 = lclsPolicy.UpdateLastTransac(sCertype, nBranch, nProduct, nPolicy, nUsercode)
                nTransactio_aux = lclsPolicy.NTRANSACTIO
            End If

            If insPostCA080 Then
                lclsCertificat = New ePolicy.Certificat

                With lclsCertificat
                    If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                        lcolTDetail_pre = New ePolicy.TDetail_pres
                        If nReceipt = eRemoteDB.Constants.intNull And Not (sPolitype = "2" And sColinvot <> "2" And sDevReceipt <> "1") Then
                            lclsGeneral = New eGeneral.GeneralFunction
                            nReceipt = lclsGeneral.Find_Numerator(4, 0, nUsercode, sCertype, nBranch, nProduct, 0, 0)
                        End If
                        Me.nReceipt = nReceipt
                        .sCertype = sCertype
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nPolicy = nPolicy
                        .nCertif = nCertif
                        .dEffecdate = dEffecdate
                        .sPolitype = sPolitype
                        .nCurrency = nCurrency
                        .nTransac = nTransactio_aux
                        .sKey = skey 'lcolTDetail_pre.sKey(nUsercode, nSessionId, False)
                        Me.sKey = .sKey
                        .nPayfreq = 1
                        .nProctype = 61
                        .sClient = sClient
                        .nUsercode = nUsercode
                        .dExpirdat = dExpirDate
                        .nReceipt = nReceipt
                        .nRecrelatedcoll = nRecrelatedcoll
                        .dIssuedate = dIssueDat
                        .dStartdate = dStarDate
                        .nProvince = nProvince
                        .nTratypei = nTratypei
                        .nType = nType
                        .sOrigReceipt = sOrigReceipt
                        .sTypExecute = sTypExecute
                        .sOnSeq = IIf((sOnSeq = vbNullString), "2", sOnSeq)
                        .nMovement = lclsPolicy_his.nMovement
                        .sDevReceipt = sDevReceipt
                        .nProceedingNum = nProceedingNum
                        .nContrat = nContrat
                        .nDraft = nDraft

                        insPostCA080 = .CreManReceiptN

                        If InStr(.sOut_moveme, ",") > 0 Then
                            sOut_moveme = IIf(Left(.sOut_moveme, 3) = " , ", Right(.sOut_moveme, Len(.sOut_moveme) - 3), .sOut_moveme)
                        Else
                            sOut_moveme = .sOut_moveme
                        End If
                    End If
                End With
            End If
        End If

insPostCA080_err:

        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lcolTDetail_pre = Nothing
        lclsGeneral = Nothing
        lclsPolicy_his = Nothing

    End Function

    '% insvalDatepolicy: Se realizan las validaciones sobre las fecha de emisi�n y vigencia
    '%                   de la p�liza
    Private Sub insvalDatepolicy(ByVal sCodispl As String, ByRef lclsErrors As eFunctions.Errors, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dGeneralDate As Date, ByVal bValidAll As Boolean, ByVal clsPolicy As ePolicy.Policy, ByVal clsCertificat As ePolicy.Certificat, ByVal sFieldDescript As String)
        Dim lclsctrol_date As eGeneral.Ctrol_date
        Dim lobjObject As Object

        On Error GoTo insvalDatepolicy_Err

        sFieldDescript = sFieldDescript & ":"

        With lclsErrors
            '+ La fecha debe estar llena
            If dGeneralDate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
            Else
                If nCertif = 0 Then
                    lobjObject = clsPolicy
                Else
                    lobjObject = clsCertificat
                End If

                '+ La fecha debe estar dentro del periodo de vigencia de la p�liza
                If Not (dGeneralDate >= lobjObject.dStartdate And (dGeneralDate <= lobjObject.dExpirdat Or lobjObject.dExpirdat = eRemoteDB.Constants.dtmNull)) Then
                    .ErrorMessage(sCodispl, 3086,  , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                Else
                    '+ La fecha debe ser posterior al periodo contable en vigor
                    If bValidAll Then
                        lclsctrol_date = New eGeneral.Ctrol_date
                        If lclsctrol_date.Find(1) Then
                            If Not dGeneralDate >= lclsctrol_date.dEffecdate Then
                                .ErrorMessage(sCodispl, 1006,  , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                            End If
                        End If

                        '+ La fecha debe ser posterior al �ltimo proceso de asientos autom�ticos

                        If lclsctrol_date.Find(1) Then
                            If Not dGeneralDate > lclsctrol_date.dEffecdate Then
                                .ErrorMessage(sCodispl, 1008,  , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                            End If
                        End If
                    End If
                End If
            End If
        End With

insvalDatepolicy_Err:
        If Err.Number Then
            On Error GoTo 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjObject = Nothing
    End Sub

    '% inspreCA028: Se buscan los datos necesarios para el manejo de la transacci�n
    Public Sub inspreCA028Grid(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nReceipt As Double, ByVal nCurrency As Integer, ByVal sReload As String, ByVal sKey As String, ByVal sAdjust As String, ByVal nAdjReceipt As Double, ByVal nAdjAmount As Double)

        mcolTDetail_pre = New TDetail_pres
        sReload = IIf(sReload = String.Empty, "1", sReload)
        Call mcolTDetail_pre.FindManReceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency, 0, sKey, sReload, nReceipt, sAdjust, nAdjReceipt, nAdjAmount)
    End Sub

    '% inspreCA028: Se buscan los datos necesarios para el manejo de la transacci�n
    Public Sub inspreCA028(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dSessionEffecdate As Date, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nTypeReceipt As Integer, ByVal nReceipt As Double, ByVal nCurrency As Integer, ByVal dIssuedat As Date, ByVal nTratypei As Integer, ByVal sOrigReceipt As String, ByVal bSequence As Boolean)
        Dim lblnFind As Boolean
        'Dim lstrKey As String
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim lclsPremium As eCollection.Premium

        mcolTDetail_pre = New TDetail_pres
        mclsPolicy = New Policy
        mclsCertificat = New Certificat
        mclsProduct = New eProduct.Product
        mclsPremium = New eCollection.Premium

        lclsPolicy_his = New ePolicy.Policy_his
        lclsPremium = New eCollection.Premium

        With Me
            .nTypeReceipt = IIf(nTypeReceipt = eRemoteDB.Constants.intNull, 1, nTypeReceipt)
            .nTratypei = nTratypei
            .nReceipt = nReceipt
            .nCurrency = nCurrency
            .dIssuedat = IIf(dIssuedat = eRemoteDB.Constants.dtmNull, Today, dIssuedat)
            .sOrigReceipt = sOrigReceipt
            .dExpirdat = IIf(dSessionEffecdate = eRemoteDB.Constants.dtmNull, dExpirdat, mclsCertificat.dNextReceip)
            .dEffecdate = IIf(dSessionEffecdate = eRemoteDB.Constants.dtmNull, dEffecdate, dSessionEffecdate)
        End With

        bError = False
        sExist = "2"
        If bSequence Then
            If lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                If lclsPolicy_his.nReceipt <> eRemoteDB.Constants.intNull Then
                    If lclsPremium.Find(sCertype, lclsPolicy_his.nReceipt, nBranch, nProduct, 0, 0) Then
                        Me.nTypeReceipt = lclsPremium.nType
                        Me.nReceipt = lclsPremium.nReceipt
                        Me.nCurrency = lclsPremium.nCurrency
                        Me.dIssuedat = lclsPremium.dIssuedat
                        Me.sOrigReceipt = lclsPremium.sOrigReceipt
                        sExist = "1"
                        If lclsPremium.sManauti = "2" Then
                            bError = True
                        End If
                    End If
                End If
            End If
            '+ Si est� dentro de la secuencia, se toma por defecto el origen "Modificaci�n"
            '+ (valores posibles Table24)
            Me.nTratypei = 3
        End If

        If Not bError Then
            If mclsProduct.Find(nBranch, nProduct, Me.dIssuedat, True) Then
                lblnFind = mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)
                Call mclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)
                '+Se busca recibo de emision
                Call mclsPremium.FindPolicyIssue(sCertype, nBranch, nProduct, nPolicy, nCertif)
            End If
            '        If lblnFind Then
            '            sReload = IIf(sReload = String.Empty, "1", "2")
            '            lstrKey = mcolTDetail_pre.sKey(nUsercode, nSessionId, IIf(sReload = "1", True, False))
            '            Call mcolTDetail_pre.FindManReceipt(sCertype, nBranch, nProduct, _
            ''                                                nPolicy, nCertif, _
            ''                                                Me.dIssuedat, _
            ''                                                mclsProduct.sBrancht, _
            ''                                                0, _
            ''                                                mclsPolicy.sPolitype, _
            ''                                                nCurrency, _
            ''                                                0, _
            ''                                                lstrKey, _
            ''                                                sReload, _
            ''                                                Me.nReceipt, sAdjust, nAdjReceipt, nAdjAmount)
            '        End If
        End If

        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing
    End Sub

    '% Delete: Se eliminan los registros asociados a la llave
    Public Function Delete(ByVal sKey As String, Optional ByVal sType_detai As String = "", Optional ByVal nCode As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nSessionId As String = "") As Boolean
        Dim lrecdelTDetail_pre As eRemoteDB.Execute
        Dim lcolTDetail_pre As TDetail_pres

        On Error GoTo Delete_err

        lrecdelTDetail_pre = New eRemoteDB.Execute

        If sKey = String.Empty Then
            lcolTDetail_pre = New TDetail_pres
            sKey = lcolTDetail_pre.sKey(nUsercode, nSessionId, False)
        End If

        With lrecdelTDetail_pre
            .StoredProcedure = "delTDetail_pre"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdelTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelTDetail_pre = Nothing
        'UPGRADE_NOTE: Object lcolTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolTDetail_pre = Nothing
    End Function

    '* Class_Initialize: se inicializan los objetos de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nCapital = eRemoteDB.Constants.intNull
        nPremium = eRemoteDB.Constants.intNull
        nCommision = eRemoteDB.Constants.intNull
        nCommi_rate = eRemoteDB.Constants.intNull
        nWay_pay = eRemoteDB.Constants.intNull
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '* Class_Terminate: se destruyen los objetos de la clase
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mcolTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolTDetail_pre = Nothing
        'UPGRADE_NOTE: Object mclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsPolicy = Nothing
        'UPGRADE_NOTE: Object mclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCertificat = Nothing
        'UPGRADE_NOTE: Object mclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsProduct = Nothing

    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '% inspostCA027A: Se realizan las actualizaciones correspondiente a la p�gina
    Public Function inspostCA027A(ByVal sDelReceipt As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
        inspostCA027A = True
        If sDelReceipt = "1" Then
            inspostCA027A = insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, 1)
        End If
    End Function

    '% insdelReceiptData: Se eliminan los datos asociados al recibo
    Public Function insdelReceiptData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, Optional ByVal nOrigin As Integer = 0) As Boolean
        Dim lclsRemote As eRemoteDB.Execute
        On Error GoTo insdelReceiptData_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote

            If nOrigin = 2 Then
                .StoredProcedure = "delReceiptdataN"
            Else
                .StoredProcedure = "delReceiptdata"
            End If

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insdelReceiptData = .Run(False)
        End With

insdelReceiptData_Err:
        If Err.Number Then
            insdelReceiptData = False
        End If
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
        On Error GoTo 0
    End Function

    'InsPreCA027: Funci�n que realiza el c�lculo de recibo autom�tico
    Public Function InsValPreCA027A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Integer
        Dim lclsRemote As eRemoteDB.Execute
        '    On Error GoTo InsValPreCA027A_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insvalpreca027a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nErrornum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsValPreCA027A = .Parameters("nErrornum").Value
        End With

InsValPreCA027A_Err:
        If Err.Number Then
            InsValPreCA027A = 0
        End If
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
        On Error GoTo 0
    End Function

    '% insValCA028_1: Realiza la validaci�n de los campos de la zona de detalle de la ventana
    Public Function insValCA028_1(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPremiumA As Double, ByVal nPremiumE As Double, ByVal nCommi_rate As Double, ByVal nCommission As Double, ByVal sType_detai As String, ByVal nDisexprc As Integer) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA028_1 As eRemoteDB.Execute

        On Error GoTo insValCA028_1_Err

        lrecinsvalCA028_1 = New eRemoteDB.Execute

        With lrecinsvalCA028_1
            .StoredProcedure = "insValCA028_1"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremiumA", nPremiumA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 18, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremiumE", nPremiumE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 18, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommi_rate", nCommi_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 16, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 18, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
            End If
            insValCA028_1 = .Confirm
        End With

insValCA028_1_Err:
        If Err.Number Then
            insValCA028_1 = "insValCA028_1: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalCA028_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalCA028_1 = Nothing
    End Function

    '% insValCA028_1: Realiza la validaci�n de los campos de la zona de detalle de la ventana
    Public Function Val_nreceiptauto(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecval_nreceiptauto As eRemoteDB.Execute

        On Error GoTo val_nreceiptauto_Err

        lrecval_nreceiptauto = New eRemoteDB.Execute

        '+
        '+ Definici�n de store procedure val_nreceiptauto al 05-17-2004 12:33:28
        '+
        With lrecval_nreceiptauto
            .StoredProcedure = "val_nreceiptauto"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Val_nreceiptauto = IIf((.Parameters("nOption").Value = 1), True, False)
            Else
                Val_nreceiptauto = True
            End If
        End With

val_nreceiptauto_Err:
        If Err.Number Then
            Val_nreceiptauto = True
        End If
        'UPGRADE_NOTE: Object lrecval_nreceiptauto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecval_nreceiptauto = Nothing
        On Error GoTo 0

    End Function


    '% insValCA080_K: Realiza la validaci�n de los campos del encabezado de la ventana
    '--------------------------------------------------------------------------------    
    Public Function insValCA080_K(ByVal sCodispl As String,
                                  ByVal nBranch As String,
                                  ByVal nProduct As String,
                                  ByVal nPolicy As String,
                                  ByVal nCertif As String,
                                  Optional ByVal sSche_Code As String = "",
                                  Optional ByVal nProceedingNum As Double = 0) As String
        '--------------------------------------------------------------------------------
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPrem_annuities As ePolicy.Prem_annuities
        Dim lclsProduct_li As eProduct.Product
        Dim lblnValid As Boolean
        Dim sValid As String

        'Dim lclsValPolCliBlock As ePolicy.ValPolCliBlock 

        Dim larrCadena() As String
        Dim sErrorsCadena As String
        Dim x As Integer

        On Error GoTo insValCA080_K_err

        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsPrem_annuities = New Prem_annuities
        lclsProduct_li = New eProduct.Product
        'lclsValPolCliBlock = New ePolicy.ValPolCliBlock

        lblnValid = True

        '+ El ramo debe estar lleno
        If nBranch = CStr(eRemoteDB.Constants.intNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 9064)
            lblnValid = False
        End If

        '+ El producto debe estar lleno
        If nProduct = CStr(eRemoteDB.Constants.intNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
            lblnValid = False
        End If

        '+ La p�liza debe estar llena
        If nPolicy = CStr(eRemoteDB.Constants.intNull) Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        Else
            If lblnValid Then
                With lclsPolicy
                    '+ La p�liza debe corresponder con un registro v�lido
                    If Not .Find("2", nBranch, nProduct, nPolicy) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3001)
                        lblnValid = False
                    Else
                        'If lclsValPolCliBlock.InsValPoliCertBlock("2", nBranch, nProduct, nPolicy, nCertif) Then
                        ' Call lobjErrors.ErrorMessage(sCodispl, 94941)
                        'Else
                        '+ La p�liza no puede estar anulada
                        If .nNullcode <> 0 And
                                .nNullcode <> CStr(eRemoteDB.Constants.intNull) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3063)
                        Else
                            '+ La p�liza debe estar en un estado v�lido
                            If .sStatus_pol <> "1" And
                                    .sStatus_pol <> "4" And
                                    .sStatus_pol <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3882)
                            End If
                        End If

                        If nCertif <= 0 Then
                            '+ El certificado debe estar lleno, si corresponde a una p�liza colectiva
                            If .sPolitype = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3006)
                            End If
                        Else
                            If nCertif > 0 Then
                                With lclsCertificat
                                    If .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                                        '+ El certificado debe estar en un estado v�lido
                                        If .sStatusva <> "1" And
                                               .sStatusva <> "4" And
                                               .sStatusva <> "5" Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 3883)
                                        End If
                                    Else
                                        '+ Si el certificado no existe
                                        Call lobjErrors.ErrorMessage(sCodispl, 3010)
                                    End If
                                End With
                            End If
                        End If
                        'End If
                    End If
                End With
            End If
        End If

        insValCA080_K = lobjErrors.Confirm

        lobjErrors = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lclsPrem_annuities = Nothing
        lclsProduct_li = Nothing

        'lclsValPolCliBlock = Nothing

insValCA080_K_err:
        If Err.Number Then
            insValCA080_K = "insValCA080_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPrem_annuities = Nothing
        'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct_li = Nothing
    End Function

    '% insValCA080: Realiza la validaci�n de los campos de la zona de detalle de la ventana
    '--------------------------------------------------------------------------------
    Public Function insValCA080(ByVal sWindowType As String,
                                ByVal sCodispl As String,
                                ByVal nBranch As Integer,
                                ByVal nProduct As Integer,
                                ByVal nPolicy As Double,
                                ByVal nCertif As Double,
                                Optional ByVal nRecordCount As Integer = 0,
                                Optional ByVal dStartdate As Date = #12:00:00 AM#,
                                Optional ByVal dExpirdat As Date = #12:00:00 AM#,
                                Optional ByVal nReceipt As Double = 0,
                                Optional ByVal dIssuedate As Date = #12:00:00 AM#,
                                Optional ByVal nSource As Integer = 0,
                                Optional ByVal sOrigReceipt As String = "",
                                Optional ByVal nCapital As Double = 0,
                                Optional ByVal nCommi_rate As Double = 0,
                                Optional ByVal nCommission As Double = 0,
                                Optional ByVal sType As String = "",
                                Optional ByVal sCacalili As String = "",
                                Optional ByVal sCommissi_i As String = "",
                                Optional ByVal nPremiumA As Double = 0,
                                Optional ByVal nPremiumE As Double = 0,
                                Optional ByVal nPremium As Double = 0,
                                Optional ByVal nPrem_det As Double = 0,
                                Optional ByVal sPrem_det As String = "",
                                Optional ByVal nDisexprc As Integer = 0,
                                Optional ByVal nRecrelatedcoll As Double = 0,
                                Optional ByVal nPremRelatedColl As Double = 0,
                                Optional ByVal nPremiumTot_All As Double = 0,
                                Optional ByVal nPremium_All As Double = 0,
                                Optional ByVal nTypeReceipt As Integer = 0,
                                Optional sDelreceipt As String = "",
                                Optional sClient As String = "",
                                Optional ByVal sCodisplOrigin As String = "",
                                Optional ByVal nPercent As Double = 0,
                                Optional ByVal nUsercode As Double = 0,
                                Optional ByVal nSessionId As String = "",
                                Optional ByVal sDevReceipt As String = "2",
                                Optional ByVal nContrat As Double = 0,
                                Optional ByVal nCoupon As Integer = 0,
                                Optional ByVal nCouponAmount As Double = 0#) As String
        '--------------------------------------------------------------------------------
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCommission As ePolicy.Commission
        Dim lclsPremium As eCollection.Premium
        Dim lclsCertificat As ePolicy.Certificat
        Dim lblnCommAsso As Boolean
        Dim lblnPolicyExist As Boolean
        Dim lclsDsex_condi As eProduct.Dsex_condi
        Dim lclsRoles As ePolicy.Roles
        Dim lclsClient As eClient.Client
        'Dim lclsClient_BlockHis As eClient.Client_blockHis
        Dim lstrErrors As String
        Dim nCertif_aux As Double
        Dim lcolTDetail_pre As ePolicy.TDetail_pres

        'Dim lclsValPolCliBlock As ePolicy.ValPolCliBlock 
        Dim lstrError As String

        On Error GoTo insValCA080_err

        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsRoles = New ePolicy.Roles
        lclsClient = New eClient.Client
        lclsPremium = New eCollection.Premium
        'lclsClient_BlockHis = New eClient.Client_blockHis
        'lclsValPolCliBlock = New ePolicy.ValPolCliBlock

        If sWindowType = "PopUp" Then

            If nTypeReceipt = 2 And nRecrelatedcoll = CStr(eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 90339)
            End If

            '+ Validaciones del capital
            If nCapital = CStr(eRemoteDB.Constants.intNull) Then
                If sType = "1" And
               sCacalili = "2" Then
                    If nPremiumA <> CStr(eRemoteDB.Constants.intNull) Or
                   nPremiumE <> CStr(eRemoteDB.Constants.intNull) Then
                        '+ Si se trata de una cobertura, no se indic� capital ilimitado y se indic� importe de prima
                        '+ debe estar lleno
                        Call lobjErrors.ErrorMessage(sCodispl, 3819)
                    End If
                End If
            Else
                If sType = "1" Then
                    '+ Si se trata de una cobertura, y se indic� capital ilimitado, no debe tener valor
                    If sCacalili = "1" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3818, , eFunctions.Errors.TextAlign.LeftAling, , 1103, ": ")
                    End If
                Else
                    '+ Si no se trata de una cobertura, no debe tener valor
                    Call lobjErrors.ErrorMessage(sCodispl, 3817, , eFunctions.Errors.TextAlign.LeftAling, , 1103, ": ")
                End If
            End If

            '+ Validaciones del % de comisi�n y monto de comisi�n fija
            If nCommission <> CStr(eRemoteDB.Constants.intNull) And
           nCommi_rate <> CStr(eRemoteDB.Constants.intNull) Then
                '+ Debe indicar % o Monto de comisi�n, no ambos
                Call lobjErrors.ErrorMessage(sCodispl, 5113)
            Else
                If nCommission = CStr(eRemoteDB.Constants.intNull) And
               nCommi_rate = CStr(eRemoteDB.Constants.intNull) Then
                    lclsCommission = New ePolicy.Commission
                    '+ Se verifica si la p�liza tiene una comisi�n asociada
                    lblnCommAsso = lclsCommission.Find_CommAsso("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate)
                    If sType = "1" Then
                        '+ Si se trata de una cobertura, y la p�liza tiene una comisi�n asociada, debe estar lleno
                        If lblnCommAsso Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3821)
                        End If
                    ElseIf sType = "2" Or
                       sType = "3" Then
                        '+ Si se trata de un recargo/descuento, y la p�liza tiene una comisi�n asociada,
                        '+ y en el producto se indic� que el recargo/descuento participa en la comisi�n,
                        '+ debe estar lleno
                        If lblnCommAsso And
                       sCommissi_i = "1" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3821)
                        End If
                    End If
                Else
                    '+ Si no se indic� prima a facturar, el % o el monto de comisi�n no deben estar llenos
                    If nPremiumA = CStr(eRemoteDB.Constants.intNull) And
                   nPremiumE = CStr(eRemoteDB.Constants.intNull) Then
                        If nCommi_rate <> CStr(eRemoteDB.Constants.intNull) Then
                            If sType <> "4" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 13865, , eFunctions.Errors.TextAlign.LeftAling, "% ", 1037, ": ")
                            End If
                        End If
                        If nCommission <> CStr(eRemoteDB.Constants.intNull) Then
                            If sType <> "4" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 13865, , eFunctions.Errors.TextAlign.LeftAling, , 1038, ": ")
                            End If
                        End If
                    End If
                    '+ Si se trata de un impuesto, el % o el monto de comisi�n no deben estar llenos
                    If sType = "4" Then
                        If nCommi_rate <> CStr(eRemoteDB.Constants.intNull) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3820, , eFunctions.Errors.TextAlign.LeftAling, "% ", 1037, ": ")
                        End If
                        If nCommission <> CStr(eRemoteDB.Constants.intNull) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3820, , eFunctions.Errors.TextAlign.LeftAling, , 1038, ":")
                        End If
                    End If
                End If
            End If

            If nPremiumA = CStr(eRemoteDB.Constants.intNull) And
           nPremiumE = CStr(eRemoteDB.Constants.intNull) Then
                If nPrem_det = 1 Or
              nPrem_det = 3 Then
                    '+ Debe indicarse monto de prima a facturar (Afecta o exenta) si el campo "prima por desglose"
                    '+ tiene valor = "Distribuir entre los detalles" o "No hay desglose"
                    Call lobjErrors.ErrorMessage(sCodispl, 55614)
                Else
                    '+ Si el campo "prima por desglose" tiene valor = "Detallar prima", se debe haber generado el detalle
                    sPrem_det = IIf(sPrem_det = vbNullString, "2", sPrem_det)
                    If sPrem_det = "2" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 56039)
                    End If
                End If
            Else
                If nPremium = CStr(eRemoteDB.Constants.intNull) Then
                    nPremium = 0
                End If

                If sType = "1" Then
                    If nPrem_det = 3 And nTypeReceipt = 2 Then

                        lstrErrors = ValPremiumCA080("2", nBranch,
                                                nProduct, nPolicy, nCertif,
                                                dStartdate, nPremiumA, nSessionId,
                                                nUsercode, nRecrelatedcoll)

                        Call lobjErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)


                    End If
                End If

                'El mensaje 3316 s�lo se debe enviar para los ramos que no est�n configurados
                'en la condici�n 276 de Condition_Serv (Ramo 58: Agrario)
                If nPremiumA <> 0 And nPremiumA <> CStr(eRemoteDB.Constants.intNull) Then
                    If System.Math.Abs(nPremiumA) > System.Math.Abs(nPremium) And nPrem_det <> 2 And sType = "1" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3316)
                    End If
                End If

                If nPremiumE <> 0 And nPremiumE <> CStr(eRemoteDB.Constants.intNull) Then
                    If System.Math.Abs(nPremiumE) > System.Math.Abs(nPremium) And nPrem_det <> 2 And sType = "1" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3316)
                    End If
                End If

                If Not (valDisco_Expr(nBranch, nProduct, dEffecdate, nDisexprc) And sType = "4" And nTypeReceipt = 2) Then
                    If nPremiumA <= 0 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60198)
                    End If
                End If
            End If

            If nPrem_det = 3 And
           sType <> "1" And
           sType <> "7" Then
                lclsDsex_condi = New eProduct.Dsex_condi
                If lclsDsex_condi.valExist_product(nBranch, nProduct, nDisexprc, dStartdate) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 56172)
                End If
            End If
        Else
            '+ Si no se trata de la poppup

            '+ Se valida que el porcentaje de comision no sea mayor a 100%
            If nPercent >= 100 Then
                Call lobjErrors.ErrorMessage(sCodispl, 90528)
            End If

            If sDelreceipt <> "1" Then

                lblnPolicyExist = lclsPolicy.Find("2", nBranch, nProduct, nPolicy)

                'If nCertif <> 0 Then
                Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)
                'End If

                '+ Validaciones sobre el campo "Titular"
                If sClient = vbNullString And sCodispl <> "CA080A" Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3016)
                Else
                    '+ Si es una p�liza colectiva o multilocalidad/innominada y la facturacion es por poliza,
                    '+ por situaci�n de riesgo o por contratante/certificado se debe buscar el rol asociado a la p�liza
                    '+ matriz dado a que el recibo esta asociado a ella
                    If lclsPolicy.sPolitype <> "1" And
                   lclsPolicy.sColinvot <> "2" And
                   lclsPolicy.sColinvot <> "4" Then
                        nCertif_aux = 0
                    Else
                        nCertif_aux = nCertif
                    End If

                    'Primero se va a buscar a nivel de certificado y si no se encuentra a ese nivel, buscar a nivel de matriz
                    If Not lclsRoles.valExistsRoles("2", nBranch, nProduct, nPolicy, nCertif, CStr(eRemoteDB.Constants.intNull), sClient, dStartdate) Then
                        '+ Si el cliente no forma parte de los clientes de la p�liza no se puede colocar como titular del recibo
                        If Not lclsRoles.valExistsRoles("2", nBranch, nProduct, nPolicy, nCertif_aux, CStr(eRemoteDB.Constants.intNull), sClient, dStartdate) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3343)
                        End If
                    End If
                    '}
                End If

                '+ Se valida que el n�mero del recibo de cobro asociado pertenezca a la p�liza
                If nRecrelatedcoll <> CStr(eRemoteDB.Constants.intNull) Then
                    'lclsPremium = CreateObject("eCollection.Premium")
                    With lclsPremium

                        'Find(ByVal certype As String, ByVal Receipt As Double, ByVal branch As Integer, ByVal product As Integer, ByVal Digit As Integer, ByVal Paynumbe As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
                        'If Not .Find("2", nRecrelatedcoll, nBranch, nProduct, 0, 0, , 1) Then
                        If Not .Find("2", nRecrelatedcoll, nBranch, nProduct, 0, 0, False) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 9046)
                        Else
                            If .nType <> 1 Then
                                Call lobjErrors.ErrorMessage(sCodispl, 90339)
                            Else
                                If nPremiumTot_All <> 0 And nPremiumTot_All <> CStr(eRemoteDB.Constants.intNull) And nPremRelatedColl <> 0 And nPremRelatedColl <> CStr(eRemoteDB.Constants.intNull) Then
                                    nPremium_All = nPremium_All + GetPremiumMPF(nBranch, nProduct, nPolicy, nCertif, nReceipt, nRecrelatedcoll)
                                    If System.Math.Abs(nPremRelatedColl) < System.Math.Abs(nPremium_All) Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 60590)
                                    End If
                                End If

                                If (dStartdate < lclsPremium.dEffecdate) Or (dExpirdat > lclsPremium.dExpirDat) Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 767096)
                                End If

                                If dStartdate < .dEffecdate Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 90369)
                                End If
                            End If
                        End If
                    End With
                    lclsPremium = Nothing
                Else
                    If nTypeReceipt = 2 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 90339)
                    End If
                End If

                '+ La prima total no debe ser 0
                If nPremiumTot_All = 0 Or nPremiumTot_All = CStr(eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 767030)
                End If

                '+ La prima no debe ser 0
                If nPremium_All = 0 Or nPremium_All = CStr(eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 767029)
                End If


                '+ Se debe haber seleccionado una linea
                If nRecordCount = 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3814)
                End If

                lblnPolicyExist = lclsPolicy.Find("2", nBranch, nProduct, nPolicy)

                If nCertif <> 0 Then
                    Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)
                End If

                '+ Validaciones de la fecha de Vigencia - Desde

                Call insvalDatepolicy(sCodispl, lobjErrors, "2",
                                  nBranch, nProduct, nPolicy,
                                  nCertif, dStartdate, False,
                                  lclsPolicy, lclsCertificat,
                                  "Vigencia - Desde") ', sCodisplOrigin)

                '+ Validaciones de la fecha de Vigencia - Hasta
                Call insvalDatepolicy(sCodispl, lobjErrors, "2",
                                  nBranch, nProduct, nPolicy,
                                  nCertif, dExpirdat, False,
                                  lclsPolicy, lclsCertificat,
                                  "Vigencia - Hasta") ', sCodisplOrigin)

                '+s Origin = Me.GetCertificat_Origin(nBranch, nPolicy, nCertif)
                Dim lValPeriod As Integer
                lValPeriod = Me.insvalPolicyPeriod(nBranch, nPolicy, nCertif, dStartdate, dExpirdat)

                If lValPeriod <> 1 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 94924)
                End If

                '+ Fecha de vigencia - hasta debe ser posterior o igual a fecha de vigencia - desde
                If dStartdate <> CStr(eRemoteDB.Constants.dtmNull) And
               dExpirdat <> CStr(eRemoteDB.Constants.dtmNull) Then
                    If dStartdate > dExpirdat Then
                        Call lobjErrors.ErrorMessage(sCodispl, 11425)
                    End If

                    '+ Fecha de vigencia - hasta no debe ser igual a la fecha de vigencia - desde
                    If dStartdate = dExpirdat Then
                        Call lobjErrors.ErrorMessage(sCodispl, 90529)
                    End If

                End If


                '+ Validaciones del recibo
                If nReceipt <> CStr(eRemoteDB.Constants.intNull) Then
                    'lclsPremium = CreateObject("eCollection.Premium")
                    lclsPremium = New eCollection.Premium 'Added by DMendoza 19/07/2021
                    With lclsPremium
                        If sCodispl = "CA080A" Then
                            If .Find("2", nReceipt, 0, 0, 0, 0) Then
                                If sDelreceipt <> "1" And (dStartdate <> .dEffecdate Or nPolicy <> .nPolicy) Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 5002)
                                End If
                            End If
                        Else
                            If .Find("2", nReceipt, 0, 0, 0, 0) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 5002)
                            End If
                        End If
                    End With
                    lclsPremium = Nothing
                End If

                '+ Validaciones de la fecha de emisi�n
                Call insvalDatepolicy_CA080(sCodispl, lobjErrors, "2",
                                        nBranch, nProduct, nPolicy,
                                        nCertif, dStartdate, True,
                                        lclsPolicy, lclsCertificat,
                                        "Emisi�n")

                '+ Validaciones del campo Origen
                If nSource = CStr(eRemoteDB.Constants.intNull) Then
                    '+ Debe estar lleno
                    Call lobjErrors.ErrorMessage(sCodispl, 3094)
                End If

                '+ Validaciones del recibo lider
                If lblnPolicyExist Then
                    '+ Si la p�liza corresponde a un negocio aceptado, debe estar lleno
                    If lclsPolicy.sBussityp <> "1" Then
                        If sOrigReceipt = vbNullString Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3096)
                        End If
                    End If
                End If

            End If
            lstrErrors = insValCA080DB("2", nBranch,
                                   nProduct, nPolicy, nCertif,
                                   dStartdate, dExpirdat, sWindowType,
                                   nTypeReceipt, sDelreceipt)

            Call lobjErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)

            'Valida si se ha ingresado el 19% de IGV para el monto afecto
            If ((lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "2") Or (lclsPolicy.sPolitype = "1" Or lclsPolicy.sPolitype = "3") Or (sDevReceipt = "1")) And lclsPolicy.sBussityp = "1" Then
                lcolTDetail_pre = New TDetail_pres
                If Not ValTaxIGV(nBranch, nProduct, dStartdate, lcolTDetail_pre.sKey(nUsercode, nSessionId, False), nRecrelatedcoll) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 94632)
                End If
            End If

            If nTypeReceipt = 2 Then 'Si es devolucion
                If nContrat > 0 And nCoupon < 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 95084)
                End If
            End If

            If nTypeReceipt = 2 Then 'Si es devolucion
                If nContrat > 0 And nCoupon > 0 Then 'Si ingreso un contrato
                    Dim nTotalPrima As Double
                    nTotalPrima = nPremiumTot_All * -1
                    If nCouponAmount < nTotalPrima Then
                        Call lobjErrors.ErrorMessage(sCodispl, 767106)
                    End If
                End If
            End If

        End If

        insValCA080 = lobjErrors.Confirm

insValCA080_err:
        lobjErrors = Nothing
        lclsCommission = Nothing
        lclsDsex_condi = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lclsRoles = Nothing
        lcolTDetail_pre = Nothing
    End Function



    'ValPremiumCA080: Este metodo se encarga de realizar las validaciones de los montos a la coberturas
    '%                  descritas en el funcional de la ventana "CA080"
    '--------------------------------------------------------------------------------
    Private Function ValPremiumCA080(ByVal sCertype As String,
                                     ByVal nBranch As Long,
                                     ByVal nProduct As Long,
                                     ByVal nPolicy As Double,
                                     ByVal nCertif As Double,
                                     ByVal dEffecdate As Date,
                                     ByVal nPremium As Double,
                                     ByVal nSessionId As Double,
                                     ByVal nUsercode As Long,
                                     ByVal nReceiptdev As Long) As String
        Dim lrecValPremiumCA080 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo ValPremiumCA080_err

        lrecValPremiumCA080 = New eRemoteDB.Execute
        mcolTDetail_pre = New TDetail_pres
        lstrKey = mcolTDetail_pre.sKey(nUsercode, nSessionId, False)

        If nReceiptdev < 0 Then
            nReceiptdev = 0
        End If

        With lrecValPremiumCA080
            .StoredProcedure = "ValPremiumCA080"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("namountcov", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 18, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nreceiptdev", nReceiptdev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("skey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                ValPremiumCA080 = .Parameters("Arrayerrors").Value
            End If

        End With

ValPremiumCA080_err:
        If Err.Number Then
            ValPremiumCA080 = "ValPremiumCA080: " & Err.Description
        End If
        On Error GoTo 0
        lrecValPremiumCA080 = Nothing
    End Function

    Public Function valDisco_Expr(ByVal nBranch As Long, ByVal nProduct As Long,
                              ByVal dEffecdate As Date, ByVal nDisexprc As Integer) As Boolean
        Dim lrecvalDisco_Expr As eRemoteDB.Execute

        On Error GoTo valDisco_Expr_err

        lrecvalDisco_Expr = New eRemoteDB.Execute

        With lrecvalDisco_Expr
            .StoredProcedure = "VALDISCO_EXPR"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisExprc", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReturn", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valDisco_Expr = IIf(.Parameters("nReturn").Value = 1, True, False)
        End With

valDisco_Expr_err:
        lrecvalDisco_Expr = Nothing
    End Function

    '% insvalDatepolicy_CA080: Se realizan las validaciones sobre las fecha de emisi�n y vigencia
    '%                   de la p�liza
    '--------------------------------------------------------------------------------
    Private Sub insvalDatepolicy_CA080(ByVal sCodispl As String,
                                       ByRef lclsErrors As eFunctions.Errors,
                                       ByVal sCertype As String,
                                       ByVal nBranch As Long,
                                       ByVal nProduct As Long,
                                       ByVal nPolicy As Double,
                                       ByVal nCertif As Double,
                                       ByVal dGeneralDate As Date,
                                       ByVal bValidAll As Boolean,
                                       ByVal clsPolicy As ePolicy.Policy,
                                       ByVal clsCertificat As ePolicy.Certificat,
                                       ByVal sFieldDescript As String)
        '--------------------------------------------------------------------------------
        Dim lclsctrol_date As eGeneral.Ctrol_date
        Dim lobjObject As Object

        On Error GoTo insvalDatepolicy_CA080_err

        sFieldDescript = sFieldDescript & ":"

        With lclsErrors
            '+ La fecha debe estar llena
            If dGeneralDate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
            Else
                If nCertif = 0 Then
                    lobjObject = clsPolicy
                Else
                    lobjObject = clsCertificat
                End If

                '+ La fecha debe estar dentro del periodo de vigencia de la p�liza
                If Not (dGeneralDate >= lobjObject.dStartDate And
                       (dGeneralDate <= lobjObject.dExpirdat Or
                       lobjObject.dExpirdat = eRemoteDB.Constants.dtmNull)) Then
                    .ErrorMessage(sCodispl, 90370, , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                Else
                    '+ La fecha debe ser posterior al periodo contable en vigor
                    If bValidAll Then
                        lclsctrol_date = New eGeneral.Ctrol_date
                        If lclsctrol_date.Find(1) Then
                            If Not dGeneralDate >= lclsctrol_date.dEffecdate Then
                                .ErrorMessage(sCodispl, 1006, , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                            End If
                        End If

                        '+ La fecha debe ser posterior al �ltimo proceso de asientos autom�ticos

                        If lclsctrol_date.Find(1) Then
                            If Not dGeneralDate > lclsctrol_date.dEffecdate Then
                                .ErrorMessage(sCodispl, 1008, , eFunctions.Errors.TextAlign.LeftAling, sFieldDescript)
                            End If
                        End If
                    End If
                End If
            End If
        End With

insvalDatepolicy_CA080_err:
        lobjObject = Nothing
        lclsctrol_date = Nothing
    End Sub

    '%insvalPolicyPeriod: Valida que las fechas ingresadas pertenezcan al mismo periodo de vigencia de la poliza
    '--------------------------------------------------------------------------------
    Public Function insvalPolicyPeriod(ByVal nBranch As Long,
                                ByVal nPolicy As Double,
                                ByVal nCertif As Double,
                                ByVal dStartDate As Date,
                                ByVal dExpirdat As Date) As Integer
        '--------------------------------------------------------------------------------
        Dim lrecreapolcer_client As eRemoteDB.Execute

        On Error GoTo insvalPolicyPeriod_err

        lrecreapolcer_client = New eRemoteDB.Execute

        '+ Definici�n de par�metros para stored procedured 'VALPOLICY_PERIOD'
        With lrecreapolcer_client
            .StoredProcedure = "VALPOLICY_PERIOD"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirDat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValid", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insvalPolicyPeriod = .Parameters("nValid").Value
        End With

insvalPolicyPeriod_err:
        lrecreapolcer_client = Nothing
    End Function

    '%insValCA028DB: Este metodo se encarga de realizar las validaciones que son accesando la BD
    '%                  descritas en el funcional de la ventana "CA028"
    '--------------------------------------------------------------------------------
    Private Function insValCA080DB(ByVal sCertype As String,
                                   ByVal nBranch As Long,
                                   ByVal nProduct As Long,
                                   ByVal nPolicy As Double,
                                   ByVal nCertif As Double,
                                   ByVal dStartDate As Date,
                                   ByVal dExpirdat As Date,
                                   ByVal sWindowType As String,
                                   ByVal nTypeReceipt As Long,
                                   ByVal sDelreceipt As String) As String
        '--------------------------------------------------------------------------------
        Dim lrecinsValCA080DB As eRemoteDB.Execute

        '+Definici�n de par�metros para stored procedure 'InsValCA010'

        On Error GoTo insValCA080DB_err

        lrecinsValCA080DB = New eRemoteDB.Execute
        With lrecinsValCA080DB
            '.StoredProcedure = "InsValCA028"
            .StoredProcedure = "INSVALCA080" 'Added by DMendoza 19/07/2021
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sWindowType", sWindowType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeReceipt", nTypeReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDelreceipt", sDelreceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insValCA080DB = .Parameters("Arrayerrors").Value
        End With

insValCA080DB_err:
        lrecinsValCA080DB = Nothing
    End Function

    Public Function ValTaxIGV(ByVal nBranch As Long, ByVal nProduct As Long,
                              ByVal dEffecdate As Date, sKey As String,
                              Optional ByVal nRecrelatedcoll As Double = 0) As Boolean
        Dim lFindTaxIGV As eRemoteDB.Execute

        On Error GoTo ValTaxIGV_err

        lFindTaxIGV = New eRemoteDB.Execute
        With lFindTaxIGV
            .StoredProcedure = "INSVALTAXIGV"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nRecrelatedcoll", nRecrelatedcoll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecrelatedColl", nRecrelatedcoll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) 'Added by DMendoza 19/07/2021
            '.Parameters.Add("nResult", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResult", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) 'Added by DMendoza 19/07/2021
            .Run(False)
            ValTaxIGV = IIf(.Parameters("nResult").Value = 2, False, True)
        End With

ValTaxIGV_err:
        lFindTaxIGV = Nothing
    End Function

    Public Function GetPremiumMPF(ByVal nBranch As Long, ByVal nProduct As Long,
                                  ByVal nPolicy As Double, ByVal nCertif As Double,
                                  ByVal nReceipt As Double, ByVal nRecrelatedcoll As Double) As Double
        Dim lGetPremiumMPF As eRemoteDB.Execute

        On Error GoTo GetPremiumMPF_err

        lGetPremiumMPF = New eRemoteDB.Execute
        With lGetPremiumMPF
            .StoredProcedure = "REAPENDINGMOVEMENTSBYCERTIF"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecrelatedColl", nRecrelatedcoll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            GetPremiumMPF = .Parameters("nPremium").Value
        End With

GetPremiumMPF_err:
        lGetPremiumMPF = Nothing
    End Function

    '% inspreCA080: Se buscan los datos necesarios para el manejo de la transacci�n
    '--------------------------------------------------------------------------------
    Public Sub inspreCA080(ByVal sCertype As String,
                           ByVal nBranch As Integer,
                           ByVal nProduct As Integer,
                           ByVal nPolicy As Double,
                           ByVal nCertif As Double,
                           ByVal dSessionEffecdate As Date,
                           ByVal dEffecdate As Date,
                           ByVal dExpirdat As Date,
                           ByVal nTypeReceipt As Integer,
                           ByVal nReceipt As Double,
                           ByVal nCurrency As Integer,
                           ByVal dIssueDat As Date,
                           ByVal nTratypei As Integer,
                           ByVal sOrigReceipt As String,
                           ByVal nSessionId As String,
                           ByVal nUsercode As Integer,
                           ByVal sReload As String,
                           ByVal nReceiptCollec As Double,
                           ByVal bSequence As Boolean,
                           ByVal nRecDevEqualColl As Double,
                           ByVal sClient As String)
        '--------------------------------------------------------------------------------
        Dim lblnFind As Boolean
        Dim lstrKey As String
        Dim lclsPolicy_his As ePolicy.Policy_his
        Dim lclsPremium As eCollection.Premium
        Dim lclsOut_Moveme As eCollection.Out_moveme
        Dim lclsClient As eClient.Client

        On Error GoTo inspreCA080_err

        mcolTDetail_pre = New TDetail_pres
        mclsPolicy = New Policy
        mclsCertificat = New Certificat
        mclsProduct = New eProduct.Product
        mclsPremium = New eCollection.Premium
        mclsPremium2 = New eCollection.Premium
        lclsPolicy_his = New ePolicy.Policy_his
        lclsPremium = New eCollection.Premium
        lclsOut_Moveme = New eCollection.Out_moveme
        lclsClient = New eClient.Client

        With Me
            .nTypeReceipt = IIf(nTypeReceipt = CStr(eRemoteDB.Constants.intNull), 1, nTypeReceipt)
            .nTratypei = nTratypei
            .nReceipt = nReceipt
            .nReceiptCollec = nReceiptCollec
            .nCurrency = nCurrency
            .dIssuedat = IIf(dIssueDat = eRemoteDB.Constants.dtmNull, Today, dIssueDat)
            .sOrigReceipt = sOrigReceipt
            .dExpirdat = IIf(dSessionEffecdate = eRemoteDB.Constants.dtmNull, dExpirdat, mclsCertificat.dNextReceip)
            .dEffecdate = IIf(dSessionEffecdate = eRemoteDB.Constants.dtmNull, dEffecdate, dSessionEffecdate)
        End With

        bError = False
        sExist = "2"
        If bSequence Then

            If lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                If lclsPolicy_his.nReceipt <> CStr(eRemoteDB.Constants.intNull) Then
                    If lclsPremium.Find2(sCertype, lclsPolicy_his.nReceipt, nBranch, nProduct, 0, 0, , 2) Then
                        Me.nTypeReceipt = lclsPremium.nType
                        Me.nReceipt = lclsPremium.nReceipt
                        Me.nReceiptCollec = lclsPremium.nRecrelatedcoll
                        Me.nCurrency = lclsPremium.nCurrency
                        Me.dIssuedat = lclsPremium.dIssuedat
                        Me.sOrigReceipt = lclsPremium.sOrigReceipt
                        sExist = "1"
                        If lclsPremium.sManauti = "2" Then
                            bError = True
                        End If
                    End If
                End If
            End If

            If mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True) Then
                If mclsPolicy.sColinvot <> "2" Then
                    If lclsOut_Moveme.reaMaxDateOutMoveme("2",
                                                          nBranch,
                                                          nProduct,
                                                          nPolicy,
                                                          nCertif,
                                                          1) Then
                        If lclsOut_Moveme.dMaxEffecdate = dSessionEffecdate Then
                            bError = True
                        End If
                    End If
                End If
            End If
            '+ Si est� dentro de la secuencia, se toma por defecto el origen "Modificaci�n"
            '+ (valores posibles Table24)
            Me.nTratypei = 3
        End If

        If Not bError Then
            If mclsProduct.Find(nBranch, nProduct, Me.dIssuedat, True) Then
                lblnFind = mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)

                If mclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True) Then

                    If lclsClient.Find(mclsCertificat.sClient) Then
                        If lclsClient.sBlockade = "1" Or lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
                            mclsCertificat.sClient = vbNullString
                        End If
                    End If

                End If

                Call mclsPremium2.Find2(sCertype, Me.nReceiptCollec, nBranch, nProduct, 0, 0, , 2)
                Me.nTratypei = mclsPremium2.nTratypei

                Call mclsPremium.FindPolicyIssue(sCertype, nBranch, nProduct, nPolicy, nCertif)
            End If

            If lblnFind Then

                If sReload = vbNullString Then
                    sReload = "1"
                End If

                'sReload = IIf(sReload = vbNullString, "1", "2")

                If dEffecdate = eRemoteDB.Constants.dtmNull Then
                    Me.dEffecdate = mclsPolicy.dStartdate
                End If

                'INICIO DMendoza 15/07/2021
                If Me.sKey = "" Then
                    lstrKey = mcolTDetail_pre.sKey(nUsercode, nSessionId, IIf(sReload = "1", True, False))
                    Me.sKey = lstrKey
                Else
                    lstrKey = Me.sKey
                End If
                'FIN DMendoza 15/07/2021

                Call mcolTDetail_pre.FindManReceiptN(sCertype,
                                                     nBranch,
                                                     nProduct,
                                                     nPolicy,
                                                     nCertif,
                                                     Me.dEffecdate,
                                                     mclsProduct.sBrancht,
                                                     0,
                                                     mclsPolicy.sPolitype,
                                                     nCurrency,
                                                     0,
                                                     lstrKey,
                                                     sReload,
                                                     Me.nReceipt,
                                                     nReceiptCollec,
                                                     nRecDevEqualColl,
                                                     sClient)
            End If
        End If

inspreCA080_err:
		lclsPolicy_his = Nothing
		lclsPremium = Nothing
		lclsClient = Nothing
	End Sub

End Class






