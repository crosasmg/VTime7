Option Strict Off
Option Explicit On
Public Class TDetail_pre
	'%-------------------------------------------------------%'
	'% $Workfile:: TDetail_pre.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 12/11/04 12:49p                              $%'
	'% $Revision:: 80                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema 18/01/2001
	
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
	
	'- Almacena todos los números de recibos separados por coma.
	Public sReceipts As String
	
	Public nTratypei As Integer
	Public nTypeReceipt As Integer
	Public dIssuedat As Date
	Public sOrigReceipt As String
	Public sAddtaxin As String
	Public sClient As String
	Public nCommission As Double
	
	'- Objeto para el manejo de los datos de la colección de la clase
	Public mcolTDetail_pre As ePolicy.TDetail_pres
	Public mclsPolicy As ePolicy.Policy
	Public mclsCertificat As ePolicy.Certificat
	Public mclsProduct As eProduct.Product
	Public mclsPremium As eCollection.Premium
	
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
	
	'- Indica si la ejecucuón es de forma preliminar = 1 o definitiva = 2
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
	
	'InsPreCA027: Función que realiza el cálculo de recibo automático
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
	
	'% InsCalReceiptRehabilitate: Calcula el recibo producto de la rehabilitación
	Private Function InsCalReceiptRehabilitate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransactio As Integer, ByVal sKey As String, ByVal nOption As Integer, ByVal nUsercode As Integer, Optional ByVal sRehabProc As String = "", Optional ByVal sAdicCover As String = "") As Collection
		Dim lrecRehabilitate As eRemoteDB.Execute
		Dim lclsReceipt As TDetail_pre
		
		'+ Definición de store procedure InsCalReceiptRehabilitate al 05-13-2002 12:27:48
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
					'+ Permite asignar a la propiedad de sólo lectura "sReceipts" todos los números de recibos
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
	
	'% InsCalReceiptMod: Calcula los recibos producto de la modificación o anulación
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
		
		'+ Si el Recibo de la devolución es automático
		If sOptReceipt = "2" Then
			If sOptDev = "1" Then
				'+ Si la devolución va a ser calculada por el método de Prorrata
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "6", "2")
				
			ElseIf sOptDev = "2" Then 
				'+ Si la devolución va a ser calculada por el método de Corto Plazo
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "5", "3")
				
			ElseIf sOptDev = "3" Then 
				'+ Si la devolución va a ser calculada por un porcentaje fijo
				lclsPolicy.sProrShort = IIf(lclsPolicy.sProrShort = "9", "7", "4")
            ElseIf sOptDev = "9" Then
                '+ Si la devolución va a ser calculada por un porcentaje fijo
                lclsPolicy.sProrShort = "9"

			End If
		End If
		
		'+ Si la transaccion es cambio de frecuencia de pago (61)
		'+ la frecuencia corresponde a la antigua ya que la nueva se saca de policy
		If nTransaction = 61 Then
			lclsCertificat.nPayFreq = nPayFreq
		End If
		
		'+ Definición de parámetros para stored procedure 'insudb.InsCalReceiptMod'
		'+ Información leída el 01/12/1999 02:45:24 PM
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
					
					'+ Permite asigna a la propiedad de sólo lectura "Receipts" todos los números de recibos
					'+ disponibles separados por coma.
					If InStr(lclsReceipt.sReceipts, CStr(lclsReceipt.nReceipt)) = 0 Then
						lclsReceipt.sReceipts = lclsReceipt.sReceipts & IIf(sReceipts = String.Empty, String.Empty, ",") & lclsReceipt.nReceipt
					End If
					.RNext()
					'+ Si el próximo item a tratar ya se ha especificado, se acumulan sus montos
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
	
	'% Update: Actualiza la información de la tabla
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
	Private Function insPrem_det(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPrem_det_old As Short) As Boolean
		Dim lrecRemote As eRemoteDB.Execute
		
		On Error GoTo insPrem_det_err
		
		lrecRemote = New eRemoteDB.Execute
		
		With lrecRemote
			.StoredProcedure = "inspostCA028Upd"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
			.Parameters.Add("nPrem_det_old", nPrem_det_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrem_det", sPrem_det, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	'% insValCA028_K: Realiza la validación de los campos del encabezado de la ventana
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
		
		'+ La póliza debe estar llena
		If nPolicy = CStr(eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 3003)
		Else
			If lblnValid Then
				With lclsPolicy
					'+ La póliza debe corresponder con un registro válido
					If Not .Find("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy)) Then
						Call lobjErrors.ErrorMessage(sCodispl, 3001)
						lblnValid = False
					Else
						'+ La póliza no puede estar anulada
						If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3063)
						Else
							'+ La póliza debe estar en un estado válido
							If .sStatus_pol <> "1" And .sStatus_pol <> "4" And .sStatus_pol <> "5" Then
								Call lobjErrors.ErrorMessage(sCodispl, 3882)
							End If
						End If
						
						If nCertif = CStr(eRemoteDB.Constants.intNull) Then
							'+ El certificado debe estar lleno, si no corresponde a una póliza individual
							If .sPolitype <> "1" Then
								Call lobjErrors.ErrorMessage(sCodispl, 3006)
							End If
						Else
							If CDbl(nCertif) > 0 Then
								'+Sólo se permite certificado si recibo/facturacion es 2-Por Certificado
								If lclsPolicy.sColinvot <> "2" Then
									Call lobjErrors.ErrorMessage(sCodispl, 750043)
								Else
									With lclsCertificat
										If .Find("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy), CDbl(nCertif)) Then
											'+ El certificado debe estar en un estado válido
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
						'+ Si el producto es de rentas vitalicias, la póliza debe tener bono de reconocimiento o
						'+ complemento de bono de reconocimiento (Table5600)
						If lclsProduct_li.nProdClas = 9 Or lclsProduct_li.nProdClas = 10 Then
							If lclsPrem_annuities.valPrem_annuities_Bonus("2", CInt(nBranch), CInt(nProduct), CDbl(nPolicy)) Then
								'+ La póliza debe tener bono de reconocimiento
								If Not lclsPrem_annuities.bBonus Then
									Call lobjErrors.ErrorMessage(sCodispl, 55914)
								End If
								'+ La póliza debe tener complemento de bono de reconocimiento
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
	
	'% insValCA028: Realiza la validación de los campos de la zona de detalle de la ventana
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
						'+ Si se trata de una cobertura, no se indicó capital ilimitado y se indicó importe de prima
						'+ debe estar lleno
						Call lobjErrors.ErrorMessage(sCodispl, 3819)
					End If
				End If
			Else
				If sType = "1" Then
					'+ Si se trata de una cobertura, y se indicó capital ilimitado, no debe tener valor
					If sCacalili = "1" Then
						Call lobjErrors.ErrorMessage(sCodispl, 3818,  , eFunctions.Errors.TextAlign.LeftAling, "Capital:")
					End If
				Else
					'+ Si no se trata de una cobertura, no debe tener valor
					Call lobjErrors.ErrorMessage(sCodispl, 3817,  , eFunctions.Errors.TextAlign.LeftAling, "Capital:")
				End If
			End If
			
			'+ Validaciones del % de comisión y monto de comisión fija
			If nCommission <> eRemoteDB.Constants.intNull And nCommi_rate <> eRemoteDB.Constants.intNull Then
				'+ Debe indicar % o Monto de comisión, no ambos
				Call lobjErrors.ErrorMessage(sCodispl, 5113)
			Else
				If nCommission = eRemoteDB.Constants.intNull And nCommi_rate = eRemoteDB.Constants.intNull Then
					lclsCommission = New ePolicy.Commission
					'+ Se verifica si la póliza tiene una comisión asociada
					lblnCommAsso = lclsCommission.Find_CommAsso("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					If sType = "1" Then
						'+ Si se trata de una cobertura, y la póliza tiene una comisión asociada, debe estar lleno
						If lblnCommAsso Then
							Call lobjErrors.ErrorMessage(sCodispl, 3821)
						End If
					ElseIf sType = "2" Or sType = "3" Then 
						'+ Si se trata de un recargo/descuento, y la póliza tiene una comisión asociada,
						'+ y en el producto se indicó que el recargo/descuento participa en la comisión,
						'+ debe estar lleno
						If lblnCommAsso And sCommissi_i = "1" Then
							Call lobjErrors.ErrorMessage(sCodispl, 3821)
						End If
					End If
				Else
					'+ Si no se indicó prima a facturar, el % o el monto de comisión no deben estar llenos
					If nPremiumA = eRemoteDB.Constants.intNull And nPremiumE = eRemoteDB.Constants.intNull Then
						If nCommi_rate <> eRemoteDB.Constants.intNull Then
							If sType <> "4" Then
								Call lobjErrors.ErrorMessage(sCodispl, 13865,  , eFunctions.Errors.TextAlign.LeftAling, "% Comisión:")
							End If
						End If
						If nCommission <> eRemoteDB.Constants.intNull Then
							If sType <> "4" Then
								Call lobjErrors.ErrorMessage(sCodispl, 13865,  , eFunctions.Errors.TextAlign.LeftAling, "Comisión fija:")
							End If
						End If
					End If
					'+ Si se trata de un impuesto, el % o el monto de comisión no deben estar llenos
					If sType = "4" Then
						If nCommi_rate <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3820,  , eFunctions.Errors.TextAlign.LeftAling, "% Comisión:")
						End If
						If nCommission <> eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 3820,  , eFunctions.Errors.TextAlign.LeftAling, "Comisión fija:")
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
			
			'+ Validaciones de la fecha de emisión
			'        Call insvalDatepolicy(sCodispl, lobjErrors, "2", _
			''                              nBranch, nProduct, nPolicy, _
			''                              nCertif, dIssuedate, True, _
			''                              lclsPolicy, lclsCertificat, _
			''                              "Emisión")
			
			'+ Validaciones del campo Origen
			If nSource = eRemoteDB.Constants.intNull Then
				'+ Debe estar lleno
				Call lobjErrors.ErrorMessage(sCodispl, 3094)
			End If
			
			'+ Validaciones del recibo lider
			If lblnPolicyExist Then
				'+ Si la póliza corresponde a un negocio aceptado, debe estar lleno
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
					'+Si es recibo de devolución, el maxímo permitido a devolver es el del recibo original
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
	
	'%insPostCA028: Se realiza la actualización de los datos en la ventana CA028 (Folder)
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

                '+ Si el recibo es de devolución se coloca la prima negativa
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

            inspostCA028Upd = insPrem_det(nBranch, nProduct, nPrem_det_old)
        End With

insPostCA028Upd_Err:
        If Err.Number Then
            inspostCA028Upd = False
        End If
        On Error GoTo 0
    End Function
	
	'% CreManReceipt: Función que se utiliza para la emisión del recibo manual
	Public Function CreManReceipt() As Boolean
		Dim lrecinsManreceipt As eRemoteDB.Execute
		
		On Error GoTo CreManReceipt_Err
		
		lrecinsManreceipt = New eRemoteDB.Execute
		
		With lrecinsManreceipt
			.StoredProcedure = "insManreceipt"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrigReceipt", sOrigReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypExecute", sTypExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdjust", sAdjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdjReceipt", nAdjReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypePay", nTypepay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertypePay", sCertypePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchPay", nBranchpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProductPay", nProductpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicyPay", nPolicypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertifPay", nCertifpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientPay", sClientpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	
	'%insPostCA028: Se realiza la actualización de los datos en la ventana CA028 (Folder)
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
	'% insvalDatepolicy: Se realizan las validaciones sobre las fecha de emisión y vigencia
	'%                   de la póliza
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
				
				'+ La fecha debe estar dentro del periodo de vigencia de la póliza
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
						
						'+ La fecha debe ser posterior al último proceso de asientos automáticos
						
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
	
	'% inspreCA028: Se buscan los datos necesarios para el manejo de la transacción
	Public Sub inspreCA028Grid(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nReceipt As Double, ByVal nCurrency As Integer, ByVal sReload As String, ByVal sKey As String, ByVal sAdjust As String, ByVal nAdjReceipt As Double, ByVal nAdjAmount As Double)
		
		mcolTDetail_pre = New TDetail_pres
		sReload = IIf(sReload = String.Empty, "1", sReload)
		Call mcolTDetail_pre.FindManReceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency, 0, sKey, sReload, nReceipt, sAdjust, nAdjReceipt, nAdjAmount)
	End Sub
	
	'% inspreCA028: Se buscan los datos necesarios para el manejo de la transacción
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
			'+ Si está dentro de la secuencia, se toma por defecto el origen "Modificación"
			'+ (valores posibles Table24)
			Me.nTratypei = 3
		End If
		
		If Not bError Then
			If mclsProduct.Find(nBranch, nProduct, Me.dIssuedat, True) Then
				lblnFind = mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)
				Call mclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)
				'+Se busca recibo de emision
				'           Call mclsPremium.FindPolicyIssue(sCertype, nBranch, nProduct, nPolicy, nCertif)
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
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	'% inspostCA027A: Se realizan las actualizaciones correspondiente a la página
	Public Function inspostCA027A(ByVal sDelReceipt As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
		inspostCA027A = True
		If sDelReceipt = "1" Then
			inspostCA027A = insdelReceiptData(sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt)
		End If
	End Function
	
	'% insdelReceiptData: Se eliminan los datos asociados al recibo
	Public Function insdelReceiptData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		On Error GoTo insdelReceiptData_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "delReceiptdata"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	'InsPreCA027: Función que realiza el cálculo de recibo automático
	Public Function InsValPreCA027A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Integer
		Dim lclsRemote As eRemoteDB.Execute
		'    On Error GoTo InsValPreCA027A_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insvalpreca027a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	'% insValCA028_1: Realiza la validación de los campos de la zona de detalle de la ventana
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
			.Parameters.Add("sType_detai", sType_detai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	'% insValCA028_1: Realiza la validación de los campos de la zona de detalle de la ventana
	Public Function Val_nreceiptauto(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecval_nreceiptauto As eRemoteDB.Execute
		
		On Error GoTo val_nreceiptauto_Err
		
		lrecval_nreceiptauto = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure val_nreceiptauto al 05-17-2004 12:33:28
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
End Class






