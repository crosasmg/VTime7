Option Strict Off
Option Explicit On
Public Class CashBankAccMov
	'%-------------------------------------------------------%'
	'% $Workfile:: CashBankAccMov.cls                       $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/07/04 3:42p                               $%'
	'% $Revision:: 102                                      $%'
	'%-------------------------------------------------------%'
	
	'**-CO008 transaction variables
	'-Variables propias de la transacción CO008
	Public nTypPay As Integer '1
	Public dDoc_date As Date '2
	Public nBankAcc As Integer '3
	Public nBank As Double '8
	Public nTypCreCard As Integer '10
	Public nIntermed As Double '11
	Public nLed_compan As Integer '16
	Public sAccount As String '17
	Public sAux_accoun As String '18
	Public nCodAseg As Integer '21
	Public nTransac As Integer
	Public nChequeLocat As Integer
	Public nCash_Id As Integer
	
	'**-CO010 transaction variables
	'-Variables propias de la transacción CO010
	Public nTypDev As Integer
	Public nAccBankO As Integer
	Public nBankDes As Double
	Public nBk_agency As Integer
	Public nTypAcc As Integer
	Public sAccBankD As String
	
	'**-CO008 and CO010 common variables
	'-Variables comunes a ambas transacciones (CO008 y CO010).
	Public sType As String
	Public nSequence As Integer
	Public nCurrency As Double '4
	Public nExchange As Double '5
	Public nAmount As Double '6
	Public nAmountDec As Double '6
	Public nAmountLoc As Double '7
	Public nAmountUF As Double
	Public sDocNumber As String '9
	Public sClient As String '14
	Public nAcc_Cash As Integer
	Public nCashNumOrd As Integer
	Public nReceiptOrd As Double
	Public nProductOrd As Integer
	Public nBranchOrd As Integer
	
	Public nTotal As Double
	
	Public sMessage As String
	
	Private lstrClient As String
	
	Public nExchangeUF As Double
	
	'Public sType As String
	Public sTypPay As String
	Public sBank As String
	Public sAcc_bank As String
	Public sCliename As String
	Public sIntermed As String
	Public sCurrency As String
	Public sLed_compan As String
	Public sCodAseg As String
	Public sChequeLocat As String
	
	Public sTypDev As String
	Public sAccBankO As String
	Public sBk_agency As String
	Public sTypAcc As String
	
	'%find_DocTypeClient: Busca  si el titular del cliente corresponde a alguno de los titulares
	'% del recibo
	Public Function Find_DocTypeClient(ByVal sClient As String, ByVal nBordereaux As Double) As Boolean
		Dim lreaDocTypeClient As eRemoteDB.Execute
		Dim nExists As Integer
		
		lreaDocTypeClient = New eRemoteDB.Execute
		
		On Error GoTo Find_DocTypeClient_Err
		
		With lreaDocTypeClient
			.StoredProcedure = "Find_DocTypeClient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				nExists = .Parameters("nExists").Value
				If .Parameters("nExists").Value = 1 Then
					Find_DocTypeClient = True
				Else
					Find_DocTypeClient = False
				End If
			End If
			
		End With
		
Find_DocTypeClient_Err: 
		If Err.Number Then
			Find_DocTypeClient = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lreaDocTypeClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaDocTypeClient = Nothing
	End Function
	'%Find_UserCashAcc:
	Public Function Find_UserCashAcc(ByVal nCurrency As Integer, ByVal nCashnum As Integer, ByVal nAcc_Cash As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lreaFind_UserCashAcc As eRemoteDB.Execute
		
		lreaFind_UserCashAcc = New eRemoteDB.Execute
		
		On Error GoTo Find_UserCashAcc_Err
		
		With lreaFind_UserCashAcc
			.StoredProcedure = "Find_UserCashAcc"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Cash", nAcc_Cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_UserCashAcc = True
			Else
				Find_UserCashAcc = False
			End If
			
		End With
		
Find_UserCashAcc_Err: 
		If Err.Number Then
			Find_UserCashAcc = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lreaFind_UserCashAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaFind_UserCashAcc = Nothing
	End Function
	
	'%Find_CashNumId: Busca los datos para el número de comprobante de caja en CASH_MOV
	Public Function Find_CashNumId(ByVal nCash_Id As Double, ByVal dValueDate As Date) As Boolean
		Dim lreaFind_CashNumId As eRemoteDB.Execute
		Dim lobjExchange As eGeneral.Exchange
		
		lreaFind_CashNumId = New eRemoteDB.Execute
		lobjExchange = New eGeneral.Exchange
		
		On Error GoTo Find_CashNumId_Err
		
		With lreaFind_CashNumId
			.StoredProcedure = "Find_CashNumId"
			.Parameters.Add("nCash_Id", nCash_Id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sType = .FieldToClass("sTable")
				On Error Resume Next
				Select Case .FieldToClass("sTable")
					Case "CASH"
						nExchange = .FieldToClass("nExchange")
						Select Case .FieldToClass("nTypPay")
							'+Efectivo
							Case 1
								nTypPay = 1
								'+Cheque corriente
							Case 2
								nTypPay = 2
								'+Tarjeta de crédito
							Case 5
								nTypPay = 5
								'+Cheque diferido
							Case 10
								nTypPay = 10
								'+Bono Jujuy
							Case 11
								nTypPay = 7
								'+Bono Tucumán/Bono Jujuy
							Case 12
								nTypPay = 8
								'+Bono Cecor
							Case 13
								nTypPay = 9
								'+Vale Vista
							Case 28
								nTypPay = 28
								'+Bono de reconocimiento
							Case 29
								nTypPay = 29
								'+Complemento bono reconocimiento
							Case 30
								nTypPay = 30
								'+Bono exonerado político y adicional
							Case 31
								nTypPay = 31
								'+Primera renta privada
							Case 32
								nTypPay = 32
						End Select
						
					Case "BANK"
						
						nTypPay = .FieldToClass("nTypPay")
						
						If lobjExchange.Find(.FieldToClass("nCurrency"), dValueDate) Then
							nExchange = lobjExchange.nExchange
						End If
						
				End Select
				
				nBankAcc = .FieldToClass("nAcc_bank")
				nCurrency = .FieldToClass("nCurrency")
				'+Fecha del documento
				dDoc_date = .FieldToClass("dDoc_date")
				'+Monto
				nAmount = .FieldToClass("nAmount")
				'+Monto en moneda local
				nAmountLoc = .FieldToClass("nAmount") * nExchange
				nAmountLoc = System.Math.Round(nAmountLoc, 0)
				
				nTotal = nTotal + nAmountLoc
				'+Banco
				nBank = .FieldToClass("nBank")
				'+No. del documento
				sDocNumber = IIf(.FieldToClass("sDocNumbe") = "0", String.Empty, .FieldToClass("sDocNumbe"))
				'+Tipo de tarjeta
				nTypCreCard = .FieldToClass("nTypCreCard")
				'+Compañía contable
				nLed_compan = .FieldToClass("nLed_compan")
				'+Cuenta contable
				sAccount = .FieldToClass("sAccount")
				'+Cuenta auxiliar contable
				sAux_accoun = .FieldToClass("sAux_accoun")
				'+Código del Asegurador
				nCodAseg = .FieldToClass("nCodAseg")
				nTransac = .FieldToClass("nTransac")
				nChequeLocat = .FieldToClass("nChequeLocat", 0)
				'+Número de caja del comprobante de caja
				nCashNumOrd = .FieldToClass("nCashNum")
				nReceiptOrd = .FieldToClass("nReceipt")
				nBranchOrd = .FieldToClass("nBranch")
				nProductOrd = .FieldToClass("nProduct")
				
				.RCloseRec()
				Find_CashNumId = True
			Else
				Find_CashNumId = False
			End If
		End With
		
Find_CashNumId_Err: 
		If Err.Number Then
			Find_CashNumId = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lreaFind_CashNumId may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaFind_CashNumId = Nothing
	End Function
	'%insPostCO008Upd: Este método se encarga de actualizar registros en la tabla "T_cash_mov". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function insPostCO008Upd(ByVal sCodispl As String, ByVal nBordereaux As Double, ByVal dCollect As Date, ByVal sType As String, ByVal Sequence As Integer, ByVal TypPay As Integer, ByVal Doc_date As Date, ByVal BankAcc As Integer, ByVal Currency_ As Double, ByVal Exchange As Double, ByVal Amount As Double, ByVal AmountDec As Double, ByVal AmountLoc As Double, ByVal Bank As Double, ByVal DocNumber As String, ByVal TypCreCard As Integer, ByVal Intermed As Double, ByVal Client As String, ByVal Led_compan As Integer, ByVal Account As String, ByVal Aux_accoun As String, ByVal CodAseg As Integer, ByVal nTransac As Integer, ByVal nCash_num As Integer, ByVal nCashNumId As Integer, ByVal nChequeLocat As Integer, ByVal nUsercode As Integer, ByVal nOffice As Integer) As Boolean
		Dim lclsColformRef As ColformRef
		Dim lobjOpt_Premiu As Object
		Dim lrecCreTMov As eRemoteDB.Execute
		Dim lreccreT_bank_mov As eRemoteDB.Execute
		Dim lreccreT_Move_Acc As eRemoteDB.Execute
		
		'-Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento.
		
		Dim llngCount As Integer
		Dim llngSeqCash As Integer
		Dim llngSeqBank As Integer
		Dim lstrType As String
		Dim ldblDiffTotal As Double
		
		insPostCO008Upd = True
		
		On Error GoTo insPostCO008Upd_Err
		
		'+Se eliminan todos los datos de las tablas temporales.
		
		If sType = String.Empty Then
			Sequence = SelMaxSequence(nBordereaux, TypPay, "1")
		Else
			DelCashBankAccMov(nBordereaux, sType, Sequence, "1")
		End If
		
		Select Case TypPay
			
			
			'+ Movimientos que van a Cash_mov
			
			
            '+Efectivo, Cheque, Tarjeta crédito, Bonos , Cheques diferidos, Tarjeta de debito
            Case 1, 2, 5, 7, 8, 9, 10, 28, 29, 31, 30, 32, 7

                lrecCreTMov = New eRemoteDB.Execute

                '+Definición de parámetros para stored procedure 'insudb.creT_cash_mov'
                '+Información leída el 14/08/2000 03:20:36 p.m.

                With lrecCreTMov
                    .StoredProcedure = "creT_cash_mov"
                    .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAmount", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If TypPay = 5 Then
                        .Parameters.Add("sCard_num", DocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("sCard_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nCard_typ", IIf(TypCreCard = eRemoteDB.Constants.intNull, System.DBNull.Value, TypCreCard), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    '+ Si es 2)Cheque 10)Cheque a fecha 29)Bono de reconocimiento 30)Complemento bono reconocimiento 31)Bono exonerado político y adic  .
                    If TypPay = 2 Or TypPay = 10 Or TypPay = 29 Or TypPay = 30 Or TypPay = 31 Or TypPay = 7 Or TypPay = 28 Then
                        .Parameters.Add("sDocnumbe", DocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("sDocnumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                    If IsDate(Doc_date) Then
                        .Parameters.Add("dDoc_date", Doc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("dDoc_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                    .Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    Select Case TypPay
                        Case CDec("1") 'Efectivo
                            .Parameters.Add("nMov_type", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("2") 'Cheque
                            .Parameters.Add("nMov_type", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9999, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("5") 'Tarjeta crédito
                            .Parameters.Add("nMov_type", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9996, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("10") 'Cheque diferido
                            .Parameters.Add("nMov_type", 10, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9997, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("28") 'Vale Vista
                            .Parameters.Add("nMov_type", 28, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9999, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("29") 'Bono de Reconocimiento
                            .Parameters.Add("nMov_type", 29, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("30") 'Complemento bono  reconocimiento
                            .Parameters.Add("nMov_type", 30, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("31") 'Bono exonerado político  y adic.
                            .Parameters.Add("nMov_type", 31, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        Case CDec("32") 'Primera renta privada
                            .Parameters.Add("nMov_type", 32, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Case CDec("7") 'Tarjeta debito
                            .Parameters.Add("nMov_type", 7, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nAcc_cash", 9996, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    End Select

                    If Bank = eRemoteDB.Constants.intNull Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("nBank_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        .Parameters.Add("nBank_code", Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If

                    '+Si viene número de comprobante de caja, entonces guarda el valor que viene en nCashNum en t_cash_mov
                    If nCash_num = eRemoteDB.Constants.intNull Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("nCash_Num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        .Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If


                    If TypPay = 25 Then
                        .Parameters.Add("nCompany", CodAseg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("nCompany", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If

                    .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCash_Id", nCashNumId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    llngSeqCash = llngSeqCash + 1

                    insPostCO008Upd = .Run(False)

                End With
                'UPGRADE_NOTE: Object lrecCreTMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lrecCreTMov = Nothing


                '+Movimientos que van a "Bank_mov"

                '+Boleta de depósito
			Case 3
				
				lreccreT_bank_mov = New eRemoteDB.Execute
				
				'+Definición de parámetros para stored procedure 'insudb.creT_bank_mov'
				'+Información leída el 01/02/2001 02:39:29 p.m.
				
				With lreccreT_bank_mov
					.StoredProcedure = "creT_bank_mov"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAcc_bank", BankAcc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmount", AmountLoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If IsDate(Doc_date) Then
						.Parameters.Add("dDoc_date", Doc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Parameters.Add("dDoc_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Parameters.Add("sDep_number", DocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					'+Si viene número de comprobante de caja, entonces guarda el valor que viene en nCashNum en t_bank_mov
					
					Select Case TypPay
						Case 3 'Boleta de depósito
							.Parameters.Add("nType_mov", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						Case Else
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nType_mov", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End Select
					
					.Parameters.Add("nMovement", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCash_Id", nCashNumId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					insPostCO008Upd = .Run(False)
				End With
				'UPGRADE_NOTE: Object lreccreT_bank_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_bank_mov = Nothing
				
				'+Movimientos que van a Mov_acc
				'+Cta. Cte. intermediarios, Cta. Cte. cliente, Cta. Cte. convenio, Utilización de Sobrantes
				
			Case 11, 12, 13, 16, 17
				
				lreccreT_Move_Acc = New eRemoteDB.Execute
				
				'+Definición de parámetros para stored procedure 'insudb.creT_Move_Acc'
				'+Información leída el 01/02/2001 02:39:44 p.m.
				
				With lreccreT_Move_Acc
					.StoredProcedure = "creT_Move_Acc"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					Select Case TypPay
						
						'**+Intermediaries
						'+Intermediarios
						
						Case 11
							Call findIntermediaClient(Intermed, Dir_debit.Interm_typ.clngProducer, Today)
							
							.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCredit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nDebit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nType_move", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nTyp_acco", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sAuthoriza", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nIntermed", Intermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							
							'+Cliente
						Case CDec("12")
							.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCredit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nDebit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nType_move", 16, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nTyp_acco", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("sAuthoriza", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nIntermed", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'**+Partner loan
							'+Prestamo a socio
						Case CDec("13")
							.Parameters.Add("sClient", Intermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCredit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nDebit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nType_move", 303, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nTyp_acco", 10, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sAuthoriza", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nIntermed", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'**+Utilization of surpluses
							'+Utilización de sobrantes
							
						Case CDec("16")
							.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCredit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nDebit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nType_move", 46, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nTyp_acco", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("sAuthoriza", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nIntermed", Intermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'**+Agreement
							'+convenio
						Case CDec("17")
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCredit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nDebit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nAmountDec", AmountDec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nType_move", 310, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nTyp_acco", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("sAuthoriza", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nIntermed", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End Select
					insPostCO008Upd = .Run(False)
					
				End With
				'UPGRADE_NOTE: Object lreccreT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_Move_Acc = Nothing
		End Select
		
insPostCO008Upd_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCO008Upd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCreTMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreTMov = Nothing
		'UPGRADE_NOTE: Object lreccreT_bank_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreT_bank_mov = Nothing
		'UPGRADE_NOTE: Object lreccreT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreT_Move_Acc = Nothing
	End Function
	
	'**%insPostCO008: This method is in charge of updating records in the table "T_cash_mov".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%insPostCO008: Este método se encarga de actualizar registros en la tabla "T_cash_mov". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function insPostCO008(ByVal nBordereaux As Double, ByVal nItems As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecColFormRef As eRemoteDB.Execute
		Dim lclsColformRef As ColformRef
		Dim lintValue As Short
		
		On Error GoTo insPostCO008_Err
		
		insPostCO008 = True
		
		' Si existe información selecionada se actualiza la ventana con contenido en caso contrario se coloca como requerida.
		If nItems > 0 Then
			lrecColFormRef = New eRemoteDB.Execute
			With lrecColFormRef
				.StoredProcedure = "insCO008PKG.insPostCO008"
				.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nValid", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				insPostCO008 = .Run(False)
			End With
		Else
			lclsColformRef = New ColformRef
			lclsColformRef.UpdateConWinPos(nBordereaux, 2, "3")
		End If
		
insPostCO008_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCO008 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	'**%insPostCO010Upd: This routine creates the records in the temporary tables
	'%insPostCO010Upd: Rutina que crea los registros en laa tablas temporales
	Public Function insPostCO010Upd(ByVal sCodispl As String, ByVal nBordereaux As Double, ByVal dCollect As Date, ByVal Type_ As String, ByVal Sequence As Integer, ByVal TypDev As Integer, ByVal AccBankO As Integer, ByVal Currency_ As Integer, ByVal Amount As Double, ByVal Exchange As Double, ByVal AmountLoc As Double, ByVal Client As String, ByVal DocNumber As String, ByVal BankDes As Double, ByVal Bk_agency As Integer, ByVal TypAcc As Integer, ByVal AccBankD As String) As Boolean
		Dim lclsColformRef As ColformRef
		Dim lreccreT_bank_mov As eRemoteDB.Execute
		Dim lreccreT_bank_trans As eRemoteDB.Execute
		Dim lreccreT_Move_Acc As eRemoteDB.Execute
		Dim lreccreT_cheques As eRemoteDB.Execute
		Dim lreccreT_cash_mov As eRemoteDB.Execute
		
		'**-Variable definition: llngCount. This variable contains the index of the vector.
		'-Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento.
		
		Dim llngCount As Integer
		Dim llngSeqCash As Integer
		Dim llngSeqBank As Integer
		Dim lstrType As String
		
		insPostCO010Upd = True
		
		On Error GoTo insPostCO010Upd_Err
		
		'+ Se eliminan todos los datos de las tablas temporales.
		If Type_ = String.Empty Then
			Sequence = SelMaxSequence(nBordereaux, TypDev, "2")
		Else
			DelCashBankAccMov(nBordereaux, Type_, Sequence, "2")
		End If
		
		Select Case TypDev
			
			'+ Movimientos que van a Cash_mov
			'+ Solicitud, Cheque manual, transferencia bancaria, Orden de pago en efectivo
			Case 1, 2, 4
				lreccreT_cheques = New eRemoteDB.Execute
				With lreccreT_cheques
					.StoredProcedure = "creT_cheques"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmount", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCheque", DocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRequest_ty", IIf(TypDev = 1, "2", "1"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insPostCO010Upd = .Run(False)
					
				End With
				'UPGRADE_NOTE: Object lreccreT_cheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_cheques = Nothing
				
				'+ Transferencia bancaria
			Case 3
				lreccreT_cheques = New eRemoteDB.Execute
				With lreccreT_cheques
					.StoredProcedure = "creT_cheques"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmount", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCheque", DocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRequest_ty", CStr(TypDev), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insPostCO010Upd = .Run(False)
				End With
				'UPGRADE_NOTE: Object lreccreT_cheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_cheques = Nothing
				
				lreccreT_bank_mov = New eRemoteDB.Execute
				With lreccreT_bank_mov
					.StoredProcedure = "creT_bank_mov"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAcc_bank", AccBankO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmount", AmountLoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If IsDate(dCollect) Then
						.Parameters.Add("dDoc_date", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Parameters.Add("dDoc_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Parameters.Add("sDep_number", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					.Parameters.Add("nType_mov", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '2 -> Transferencia
					.Parameters.Add("nMovement", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					insPostCO010Upd = .Run(False)
				End With
				'UPGRADE_NOTE: Object lreccreT_bank_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_bank_mov = Nothing
				
				lreccreT_bank_trans = New eRemoteDB.Execute
				With lreccreT_bank_trans
					.StoredProcedure = "creT_bank_trans"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAcc_bank", AccBankO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAcc_type", TypAcc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sAcco_num", AccBankD, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBank_code", BankDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBk_agency", Bk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insPostCO010Upd = .Run(False)
				End With
				'UPGRADE_NOTE: Object lreccreT_bank_trans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_bank_trans = Nothing
				
				'+ Movimientos que van a Mov_acc
			Case 5 'Cta. Cte. cliente
				lreccreT_Move_Acc = New eRemoteDB.Execute
				
				'+ Definición de parámetros para stored procedure 'insudb.creT_Move_Acc'
				'+ Información leída el 01/02/2001 02:39:44 p.m.
				
				With lreccreT_Move_Acc
					.StoredProcedure = "creT_Move_Acc"
					.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCredit", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCurrency", Currency_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDebit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmountDec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nType_move", 19, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTyp_acco", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sNumForm", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sAuthoriza", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExchange", Exchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nReceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insPostCO010Upd = .Run(False)
				End With
				'UPGRADE_NOTE: Object lreccreT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lreccreT_Move_Acc = Nothing
		End Select
		
insPostCO010Upd_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCO010Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostCO010: This routine creates the records in the temporary tables
	'%insPostCO010: Rutina que crea los registros en laa tablas temporales
	Public Function insPostCO010(ByVal nBordereaux As Double, ByVal nItems As Integer) As Boolean
		Dim lclsColformRef As ColformRef
		
		insPostCO010 = True
		
		On Error GoTo insPostCO010_Err
		
		lclsColformRef = New ColformRef
		'+ Si existen registros seleccionados se coloca la ventana con contenido sino requerida.
		If nItems > 0 Then
			lclsColformRef.UpdateConWinPos(nBordereaux, 2, "1")
		Else
			lclsColformRef.UpdateConWinPos(nBordereaux, 2, "3")
		End If
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
		
insPostCO010_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCO010 = False
		End If
		On Error GoTo 0
	End Function
	
	
	Private Function DelAllCashBankAccMov(ByRef nRelation As Double) As Boolean
		
		
		Dim lrecdelCO008 As eRemoteDB.Execute
		
		lrecdelCO008 = New eRemoteDB.Execute
		
		'**+Stored procedure parameter definition 'insudb.delCO008'
		'**+Data of 02/01/2001 02:39:16 p.m.
		'+Definición de parámetros para stored procedure 'insudb.delCO008'
		'+Información leída el 01/02/2001 02:39:16 p.m.
		
		With lrecdelCO008
			.StoredProcedure = "delCO008"
			.Parameters.Add("nBordereaux", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelAllCashBankAccMov = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelCO008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCO008 = Nothing
		
	End Function
	
	'**%Function: DelCashBankAccMov. Deletes the transactions from the temporary tables
	'%Función: DelCashBankAccMov. Borra los movimientos de las tablas temporales
	Public Function DelCashBankAccMov(ByVal nRelation As Double, ByVal Type_ As String, ByVal Sequence As Integer, ByVal sTypeTran As String) As Boolean
		Dim lrecdelCO008 As eRemoteDB.Execute
		
		lrecdelCO008 = New eRemoteDB.Execute
		
		With lrecdelCO008
			.StoredProcedure = "delCashMovAccMovs"
			.Parameters.Add("sType", Type_, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", Sequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypetran", sTypeTran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			DelCashBankAccMov = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelCO008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCO008 = Nothing
		
	End Function
	
	'**%insValCO008Upd: This routine validates the frame fields
	'%insValCO008Upd: Rutina que permite validar los campos del frame.
	Public Function insValCO008Upd(ByVal sCodispl As String, ByVal nBordereaux As Double, ByVal dCollect As Date, ByVal nTypPay As Integer, ByVal dDoc_date As Date, ByVal nBankAcc As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nBank As Double, ByVal sDocNumber As String, ByVal nTypCreCard As Integer, ByVal nIntermed As Double, ByVal sClient As String, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal nUsercode As Integer, ByVal nCashnum As Integer, ByVal nChequeLocat As Integer, ByVal sRelOrigi As String, ByVal nCash_Id As Double, ByVal dValDate As Date) As String
		Dim lrecInsValCO008Upd As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
		
		On Error GoTo insValCO008Upd_Err
		
		lrecInsValCO008Upd = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		With lrecInsValCO008Upd
			.StoredProcedure = "insCO008PKG.InsValCO008Upd"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypPay", nTypPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDoc_date", dDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocNumber", sDocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankAcc", nBankAcc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypCreCard", nTypCreCard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_Id", nCash_Id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
			insValCO008Upd = .Confirm
		End With
		
insValCO008Upd_Err: 
		If Err.Number Then
			insValCO008Upd = insValCO008Upd & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lrecInsValCO008Upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValCO008Upd = Nothing
	End Function
	
	'% insValCO008: Se efectuan las validaciones de la ventana CO008.
	Public Function insValCO008(ByVal nBordereaux As Double, ByVal nItems As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsColformRef As ColformRef
		
		lclsErrors = New eFunctions.Errors
		lclsColformRef = New ColformRef
		
		On Error GoTo insValCO008_Err
		
		'+ Si no existen registros
		If nItems <= 0 Then
			lclsErrors.ErrorMessage("CO008", 3015)
			lclsColformRef = New ColformRef
			lclsColformRef.UpdateConWinPos(nBordereaux, 2, "3")
			'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsColformRef = Nothing
		End If
		
		insValCO008 = lclsErrors.Confirm
		
insValCO008_Err: 
		If Err.Number Then
			insValCO008 = insValCO008 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	
	'**%Function insValCO010Upd: This routine validates the frame fields
	'%Función insValCO010Upd: Rutina que permite validar los campos del frame.
	Public Function insValCO010Upd(ByVal sCodispl As String, ByVal nBordereaux As Double, ByVal dCollect As Date, ByVal Type_ As String, ByVal Sequence As Integer, ByVal TypDev As Integer, ByVal AccBankO As Integer, ByVal Currency_ As Integer, ByVal Amount As Double, ByVal Exchange As Double, ByVal AmountLoc As Double, ByVal Client As String, ByVal BankDes As Double, ByVal Bk_agency As Integer, ByVal TypAcc As Integer, ByVal AccBankD As String) As String
		Dim lclsColformRef As ColformRef
		Dim lobjExchange As eGeneral.Exchange
		Dim lobjErrors As eFunctions.Errors
		Dim lobjClient As eClient.Client
		Dim lblnError As Boolean
		
		On Error GoTo insValCO010Upd_Err
		
		lobjExchange = New eGeneral.Exchange
		lobjErrors = New eFunctions.Errors
		lobjClient = New eClient.Client
		
		If TypDev = eRemoteDB.Constants.intNull Then
			TypDev = 0
		End If
		If AccBankO = eRemoteDB.Constants.intNull Then
			AccBankO = 0
		End If
		
		If Currency_ = eRemoteDB.Constants.intNull Then
			Currency_ = 0
		End If
		
		If Amount = eRemoteDB.Constants.intNull Then
			Amount = 0
		End If
		
		If Exchange = eRemoteDB.Constants.intNull Then
			Exchange = 0
		End If
		
		If AmountLoc = eRemoteDB.Constants.intNull Then
			AmountLoc = 0
		End If
		
		If BankDes = eRemoteDB.Constants.intNull Then
			BankDes = 0
		End If
		
		If Bk_agency = eRemoteDB.Constants.intNull Then
			Bk_agency = 0
		End If
		
		If TypAcc = eRemoteDB.Constants.intNull Then
			TypAcc = 0
		End If
		
		'+ Se efectuan las validaciones correspondientes al tipo de devolución
		If TypDev = 0 Then
			lobjErrors.ErrorMessage(sCodispl, 750080)
		Else
			
			'+ Se efectuan las validaciones correspondientes a la cuenta bancaria
			If Not lblnError Then
				If AccBankO = 0 And TypDev = 3 Then
					lobjErrors.ErrorMessage(sCodispl, 750070)
				End If
			End If
			
			'+ Se efectuan las validaciones correspondientes a la moneda
			If Not lblnError Then
				If Currency_ = 0 Then
					lobjErrors.ErrorMessage(sCodispl, 750011)
					nExchange = 0
				Else
					If Currency_ = 1 Then
						Exchange = 1
					Else
						If lobjExchange.Find(Currency_, Today) Then
							Exchange = lobjExchange.nExchange
						End If
					End If
					If CDbl(Amount) <> 0 Then
						AmountLoc = CDbl(Amount) * CDbl(Exchange)
					Else
						AmountLoc = 0
					End If
				End If
			End If
			
			'+ Se efectuan las validaciones correspondientes al importe
			If Amount <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 750081)
			Else
				nAmountLoc = Amount * Exchange
			End If
			
			'+ Se efectuan las validaciones correspondientes al cliente
			If Client = String.Empty Then
				lobjErrors.ErrorMessage(sCodispl, 12043)
			Else
				If Not lobjClient.Find(Client) Then
					lobjErrors.ErrorMessage(sCodispl, 1007)
				Else
					'+ El cliente debe corresponder a algunos de los titulares
					'  incluidos previamente en la ventana de documentos
					If Not Find_DocTypeClient(Client, nBordereaux) Then
						lobjErrors.ErrorMessage(sCodispl, 750082)
					End If
				End If
			End If
			
			
			'+ Se efectuan las validaciones correspondientes al banco
			If BankDes <> 0 And TypDev <> 3 Then
				lobjErrors.ErrorMessage(sCodispl, 750062)
				lblnError = True
			End If
			
			If Not lblnError Then
				If BankDes = 0 And TypDev = 3 Then
					lobjErrors.ErrorMessage(sCodispl, 7004)
				End If
			End If
			
			'+ Se efectuan las validaciones correspondientes a la agencia bancaria
			lblnError = False
			
			If Bk_agency <> 0 And TypDev <> 3 Then
				lobjErrors.ErrorMessage(sCodispl, 750083)
				lblnError = True
			End If
			
			If Not lblnError Then
				If Bk_agency = 0 And TypDev = 3 Then
					lobjErrors.ErrorMessage(sCodispl, 3875)
				End If
			End If
			
			'+ Se efectuan las validaciones correspondientes al tipo de cuenta
			lblnError = False
			
			If TypAcc <> 0 Then
				If TypDev <> 3 Then
					lobjErrors.ErrorMessage(sCodispl, 750084)
					lblnError = True
				End If
			End If
			
			If Not lblnError Then
				If TypAcc <> 0 And TypDev <> 3 Then
					lobjErrors.ErrorMessage(sCodispl, 750084)
					lblnError = True
				End If
			End If
			
			If Not lblnError Then
				If TypAcc = 0 And TypDev = 3 Then
					lobjErrors.ErrorMessage(sCodispl, 36022)
				End If
			End If
			
			'+ Se efectuan las validaciones correspondientes a la cuenta bancaria destino
			lblnError = False
			
			If AccBankD <> String.Empty And TypDev <> 3 Then
				lobjErrors.ErrorMessage(sCodispl, 750086)
				lblnError = True
			End If
			
			If Not lblnError Then
				If AccBankD = String.Empty And TypDev = 3 Then
					lobjErrors.ErrorMessage(sCodispl, 750085)
				End If
			End If
		End If
		
		insValCO010Upd = lobjErrors.Confirm
		
insValCO010Upd_Err: 
		If Err.Number Then
			insValCO010Upd = insValCO010Upd & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insValCO010: Se efectuan las validaciones de la ventana CO010.
	Public Function insValCO010(ByVal nBordereaux As Double, ByVal nItems As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsColformRef As ColformRef
		
		lclsErrors = New eFunctions.Errors
		lclsColformRef = New ColformRef
		
		On Error GoTo insValCO010_Err
		
		'+ Si no existen registros
		If nItems <= 0 Then
			lclsErrors.ErrorMessage("CO010", 750055)
			lclsColformRef = New ColformRef
			lclsColformRef.UpdateConWinPos(nBordereaux, 2, "3")
			'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsColformRef = Nothing
		End If
		
		insValCO010 = lclsErrors.Confirm
		
insValCO010_Err: 
		If Err.Number Then
			insValCO010 = insValCO010 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	'**%SelMaxSequence: This function gets the maximum plus 1 according to the table read
	'**%SType: 1)Pay, 2)Refund
	'%SelMaxSequence: Obtiene el máximo más uno según la tabla leida
	'%SType: 1)Pago, 2)Devolución
	Public Function SelMaxSequence(ByVal nBordereaux As Double, ByVal nType As Integer, Optional ByVal sType As String = "") As Integer
		
		Dim lrecdelCO008 As eRemoteDB.Execute
		
		lrecdelCO008 = New eRemoteDB.Execute
		
		With lrecdelCO008
			.StoredProcedure = "reaCashBankAccMov"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeqCash", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeqBank", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeqMovAcc", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeqCheck", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			SelMaxSequence = .Run(False)
			
			'**+Type of pay
			'+Tipo de pago
			If sType = "1" Then
				Select Case nType
					Case 1, 2, 5, 7, 8, 9, 10, 28, 29, 31, 30, 32
						SelMaxSequence = .Parameters("nSeqCash").Value
					Case 3
						SelMaxSequence = .Parameters("nSeqBank").Value
					Case Else
						SelMaxSequence = .Parameters("nSeqMovAcc").Value
				End Select
			End If
			
			'**+Type of refund
			'+Tipo de devolución
			If sType = "2" Then
				Select Case nType
					Case 1, 2, 3, 4
						SelMaxSequence = .Parameters("nSeqCheck").Value
					Case Else
						SelMaxSequence = .Parameters("nSeqMovAcc").Value
				End Select
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecdelCO008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCO008 = Nothing
		
	End Function
	
	'**%findIntermediaClient: This routine determines if the intermediary exists in the intermediary table
	'%findIntermediaClient: Esta rutina permite validar si el intermediario ingresado existe en la tabla intermedia
	Private Function findIntermediaClient(ByVal nIntermed As Double, ByVal nIntertyp As Integer, ByVal dEffecdate As Date) As Boolean
		
		'**-Variable definition. lrec_intermed. It will be used as a cursor
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		
		Dim lrec_Intermed As eRemoteDB.Execute
		
		lrec_Intermed = New eRemoteDB.Execute
		
		'**+Stored procedure parameter definition. 'insudb.insValCheques'
		'**+Data as of 11/15/2000 04:49:59 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaIntermediaClient'
		'+Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reaIntermediaClient"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngOK Then
					findIntermediaClient = True
					lstrClient = .FieldToClass("sClient")
					.RCloseRec()
				End If
			End If
		End With
		
	End Function
	
	'**%findClientCurr_acc: This method reads the data from the table "Curr_acc"
	'%findClientCurr_acc: Esta rutina permite leer los datos de la tabla Curr_acc
	Private Function findClientCurr_acc(ByVal nType_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Variable definition: lrec_Curr_acc. It will be used as a cursor
		'-Se define la variable lrec_Curr_acc que se utilizará como cursor.
		
		Dim lrec_Curr_acc As eRemoteDB.Execute
		lrec_Curr_acc = New eRemoteDB.Execute
		
		'**+The stored procedure is executed to verify if the current account exists.
		'+Se ejecuta el store procedure para verificar si existe o no la cuenta corriente.
		
		With lrec_Curr_acc
			.StoredProcedure = "reaCurr_acc_o"
			.Parameters.Add("nTyp_acco", nType_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'**+Local currency
			'+Moneda local
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				findClientCurr_acc = True
				nAmount = .FieldToClass("nBalance")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrec_Curr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_Curr_acc = Nothing
		
	End Function
	
	'**%insValCheques: This routine verifies if a cheque exist in the cheques table
	'%insValCheques: Verifica si un cheque esta en la tabla de cheques.
	Private Function insValCheques(ByVal lstrCheques As String) As Boolean
		Dim lrecinsValCheques As eRemoteDB.Execute
		
		lrecinsValCheques = New eRemoteDB.Execute
		
		insValCheques = False
		
		'**+Stored procedure parameter definition. 'insudb.insValCheques'
		'**+Data as of 08/11/2000 11:59:57 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insValCheques'
		'+Información leída el 11/08/20  00 11:59:57 p.m.
		
		With lrecinsValCheques
			.StoredProcedure = "insValCheques"
			.Parameters.Add("sCheque", lstrCheques, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProce", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nProce").Value = 0 Then
					insValCheques = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsValCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValCheques = Nothing
	End Function
	
	'**%valOnlyCheques: This routine verifies if a cheque exist in the cheques table
	'%valOnlyCheques: Verifica que solamente existan registros de tipo de pago cheques o cheque a fecha.
	Private Function valOnlyCheques(ByVal nBordereaux As Double) As Boolean
		Dim lrecvalOnlyCheques As eRemoteDB.Execute
		
		On Error GoTo valOnlyCheques_Err
		
		lrecvalOnlyCheques = New eRemoteDB.Execute
		valOnlyCheques = False
		
		'**+Stored procedure parameter definition. 'insudb.valOnlyCheques'
		'**+Data as of 08/11/2000 11:59:57 p.m.
		With lrecvalOnlyCheques
			.StoredProcedure = "valCashBankAccMov"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOk", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nOk").Value = 0 Then
					valOnlyCheques = True
				End If
			End If
		End With
		
valOnlyCheques_Err: 
		If Err.Number Then
			valOnlyCheques = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalOnlyCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalOnlyCheques = Nothing
	End Function
	
	
	'%valCashBankAccMov: Verifica si el convenio a tratar es del tipo SEF.
	Public Function valCashBankAccMov(ByVal nBordereaux As Double) As Boolean
		Dim lrecCashBankAccMov As eRemoteDB.Execute
		
		On Error GoTo valCashBankAccMov_Err
		
		lrecCashBankAccMov = New eRemoteDB.Execute
		
		valCashBankAccMov = True
		
		With lrecCashBankAccMov
			.StoredProcedure = "reaCashBankAccMov_all"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nCount").Value = 0 Then
					valCashBankAccMov = False
				End If
			Else
				valCashBankAccMov = False
			End If
		End With
		
valCashBankAccMov_Err: 
		If Err.Number Then
			valCashBankAccMov = True
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCashBankAccMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCashBankAccMov = Nothing
	End Function
End Class






