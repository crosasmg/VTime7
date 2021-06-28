Option Strict Off
Option Explicit On
Public Class CollectionRep
	'%-------------------------------------------------------%'
	'% $Workfile:: CollectionRep.cls                        $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 28/10/09 12:11a                              $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	Public lstrKey As String
	Public mdtmLimit_Pay As Date
	Public mdblAmount_pay As Double
	Public nId_Register As Integer
	Public nCommission As Double
	Public mclsError As eFunctions.Errors
	Public nMultiPacAmount As Double
	Public sFileName As String
	Public sFileName1 As String
	Public sProcess As String
	Public sNoProcess As String
	Public sAgreeApvSef As String
	Public sKey As String
	Private nBalance_Cli As Double
	Private nAmount_Pay As Double
	
	
	Private llngUsercode As Integer
	
	'%insGenFilesCOL500: Crea los archivos del proceso COL500
	Public Function insGenFilesCOL500(ByVal dExpirdate As Date, ByVal nWay_Pay As Integer, ByVal nInsur_area As Integer, ByVal sKey As String, ByVal nBank As Double) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lobjGeneral As eGeneral.GeneralFunction
		Dim lobjClient As eClient.Client
		Dim lobjCompany As eGeneral.Company
		
		Dim llngRecCounter As Integer
		Dim ljdblAmountTot As Double
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrCompany As Object
		
		Dim lstrWritTxt As String
        Dim FileName As String = ""
        Dim FileNameCityDet As String
		Dim FileNum As Integer
		
		insGenFilesCOL500 = True
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "ReaCol500"
			.Parameters.Add("dExpirdate", dExpirdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		Dim lclsValue As eFunctions.Values
		Dim nCountReg As Integer
		Dim nAmountTot As Double
		Dim C As Object
		Dim FileNum2 As Integer
		If lrecTime.Run() Then
			
			lobjGeneral = New eGeneral.GeneralFunction
			'+ Se busca la ruta en la que se guardará el archivo de texto
			lstrLoadFile = lobjGeneral.GetLoadFile() & lrecTime.FieldToClass("sCompany")
			'+ Se busca el directorio virtual del archivo a crear
			lclsValue = New eFunctions.Values
			lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
			'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsValue = Nothing
			'+ ------------------------------------------
			'+ TRANSBANK:
			'+          Se envia archivo a 2:Transbank
			'+          Campos faltantes: /
			
			If nWay_Pay = 2 Then
				If Not lrecTime.EOF Then
					FileName = lstrLoadFile & "TRANSBANK_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
					FileNum = FreeFile
					FileOpen(FileNum, FileName, OpenMode.Output)
					Do While Not lrecTime.EOF
						lstrWritTxt = ""
						lstrWritTxt = lstrWritTxt & CStr(Format(lrecTime.FieldToClass("sType"), "0")) & ";"
						lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 13) & ";"
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sDocument"), " ", 19, "Left", "Left") & ";"
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						lstrWritTxt = lstrWritTxt & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dCardexpir")), "00-00", Format(lrecTime.FieldToClass("dCardexpir"), "yy-MM"))) & ";"
						lstrWritTxt = lstrWritTxt & New String(" ", 8) & ";"
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClinamePay"), " ", 40, "Left", "Left") & ";"
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sAddress"), " ", 40, "Left", "Left") & ";"
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy"), " ", 10, "Left", "Left") & ";"
						PrintLine(FileNum, lstrWritTxt)
						lrecTime.RNext()
					Loop 
					FileClose(FileNum)
				End If
			End If
			
			'+ ------------------------------------------
			'+ MULTIPAC SANTIAGO:
			'+          Se envía el archivo a Security y Office Banking (Multipac Santiago)
			'+          Campos:
			'+          Nómina.
			'+          Cuenta corriente.
			'+          Número de factura.
			
			If nWay_Pay = 1 Then
				If nBank = 49 Or nBank = 35 Then
					If Not lrecTime.EOF Then
						If nBank = 35 Then
							FileName = lstrLoadFile & "SANTIAGO_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						Else
							FileName = lstrLoadFile & "SECURITY_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						End If
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						Do While Not lrecTime.EOF
							lstrWritTxt = "135       "
							lstrWritTxt = lstrWritTxt & New String("0", 10)
							lstrWritTxt = lstrWritTxt & CStr(Format(dExpirdate, "yyyyMMdd"))
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nBank_Code"), "0", 3,  , "Right")
							lstrWritTxt = lstrWritTxt & FormatData(nBank, "0", 3,  , "Right")
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sDocument"), " ", 17, "Left", "Left")
							lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 10)
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClient") & lrecTime.FieldToClass("sDigit"), "0", 15)
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy") & IIf(nInsur_area = 2, lrecTime.FieldToClass("sDigitPol"), ""), "0", 20)
							lstrWritTxt = lstrWritTxt & New String("0", 8)
							PrintLine(FileNum, lstrWritTxt)
							lrecTime.RNext()
						Loop 
						FileClose(FileNum)
					End If
				End If
				'+ ------------------------------------------
				'+ BANCO ESTADO:
				'+             Se envía el archivo al Banco - Estado
				'+             Campos:
				'+             Cuenta corriente: CStr(Format(lrecTime.FieldToClass(" "),"0000000000"))
				
				If nBank = 12 Then
					If Not lrecTime.EOF Then
						'+Se busca el código de la compañia dependiendo del área de seguros
						'                            Dim lobjExchange As eGeneral.Exchange
						
						lobjClient = New eClient.Client
						
						lobjCompany = New eGeneral.Company
						'                            Set lobjExchange = New eGeneral.Exchange
						
						If lobjCompany.Find(IIf(nInsur_area = 1, 2, 1)) Then
							lstrCompany = Right(lobjCompany.sClient, 9) & FormatData(lobjCompany.sDigit, "0", 1)
						Else
							lstrCompany = New String("0", 10)
						End If
						'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjCompany = Nothing
						
						FileName = lstrLoadFile & "BANCOESTADO_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						lstrWritTxt = CStr(1) & lstrCompany & IIf(nInsur_area = 1, "00000000023", "00000000018")
						lstrWritTxt = lstrWritTxt & Today.ToString("yyyyMMdd")
						lstrWritTxt = lstrWritTxt & "RECEPT.MDT" & CStr(Format(" ", New String("@", 70) & "!"))
						PrintLine(FileNum, lstrWritTxt)
						nCountReg = 0
						nAmountTot = 0
						Do While Not lrecTime.EOF
							lstrWritTxt = CStr(2) & FormatData(lrecTime.FieldToClass("nPolicy"), "0", 11) & CStr(Format(" ", New String("@", 9) & "!"))
							lstrWritTxt = lstrWritTxt & CStr(Format(" ", New String("@", 5) & "!")) & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 13) & "00"
							'                                Call lobjExchange.Convert(0, lrecTime.FieldToClass("nAmount"), 1, 4, dIncrease, 1)
							'                               lstrWritTxt = lstrWritTxt & FormatData(lobjExchange.pdblResult, "0", 15)
							lstrWritTxt = lstrWritTxt & "000000000000000"
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClient"), "0", 9) & FormatData(lobjClient.GetRUT(lrecTime.FieldToClass("sClient")), "0", 1)
							lstrWritTxt = lstrWritTxt & Format(dExpirdate, "yyyyMMdd") & CStr(Format(" ", New String("@", 4) & "!"))
							lstrWritTxt = lstrWritTxt & "00000000000"
							lstrWritTxt = lstrWritTxt & "00000000 "
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nBank_Code"), "0", 3) & CStr(Format(" ", New String("@", 9) & "!"))
							PrintLine(FileNum, lstrWritTxt)
							nCountReg = nCountReg + 1
							nAmountTot = nAmountTot + lrecTime.FieldToClass("nAmount")
							lrecTime.RNext()
						Loop 
						lstrWritTxt = CStr(3) & FormatData(nCountReg, "0", 7) & FormatData(System.Math.Round(nAmountTot), "0", 15)
						lstrWritTxt = lstrWritTxt & "0000000000000000000000" & CStr(Format(" ", New String("@", 65) & "!"))
						PrintLine(FileNum, lstrWritTxt)
						FileClose(FileNum)
						
						'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjClient = Nothing
						
					End If
				End If
				
				'+ ------------------------------------------
				'+ BANCO BHIF:
				'+          Se envía el archivo al Banco BHIF
				'+          Campos:
				'+          Fillier
				If nBank = 504 Then
					If Not lrecTime.EOF Then
						'+Se busca el código de la compañia dependiendo del área de seguros
						lobjCompany = New eGeneral.Company
						If lobjCompany.Find(IIf(nInsur_area = 1, 2, 1)) Then
							lstrCompany = Right(lobjCompany.sClient, 8) & FormatData(lobjCompany.sDigit, "0", 1)
						Else
							lstrCompany = New String("0", 9)
						End If
						'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjCompany = Nothing
						
						FileName = lstrLoadFile & "BHIF_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						lstrWritTxt = CStr(1) & lstrCompany & "001" & "032"
						lstrWritTxt = lstrWritTxt & Format(dExpirdate, "yyyyMMdd")
						lstrWritTxt = lstrWritTxt & CStr(2) & CStr(Format(" ", New String("@", 230) & "!")) & "C"
						PrintLine(FileNum, lstrWritTxt)
						Do While Not lrecTime.EOF
							lstrWritTxt = CStr(2) & FormatData(lrecTime.FieldToClass("sClient") & lrecTime.FieldToClass("sDigit"), "0", 9)
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy"), "0", 8) & New String(" ", 7)
							lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 22)
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClinamepay"), " ", 45, "Left", "Left")
							lstrWritTxt = lstrWritTxt & "1"
							lstrWritTxt = lstrWritTxt & CStr(Format(" ", New String("@", 162) & "!")) & "C"
							PrintLine(FileNum, lstrWritTxt)
							lrecTime.RNext()
						Loop 
						FileClose(FileNum)
					End If
				End If
				
				'+ ------------------------------------------
				'+ CORPBANCA:
				'+          Se envía el archivo a CorpBanca
				'+          Campos:
				'+          Cuenta corriente
				'+          Fillier
				If nBank = 27 Then
					If Not lrecTime.EOF Then
						FileName = lstrLoadFile & "CORPBANCA_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						Do While Not lrecTime.EOF
							lstrWritTxt = "027" & IIf(nInsur_area = 1, "021", "013")
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy") & IIf(nInsur_area = 2, lrecTime.FieldToClass("sDigitPol"), ""), "0", 12)
							lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 11)
							lstrWritTxt = lstrWritTxt & Format(dExpirdate, "yyMMdd")
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sDocument"), "0", 8, "Left")
							lstrWritTxt = lstrWritTxt & CStr(Format(" ", New String("@", 17) & "!"))
							PrintLine(FileNum, lstrWritTxt)
							lrecTime.RNext()
						Loop 
						FileClose(FileNum)
					End If
				End If
				
				'+ ------------------------------------------
				'+ CITIBANK:
				'+          Se envía el archivo a Citybank
				'+          Campos faltantes:
				'+          Número de registros que se envían en el archivo
				'+          Filler
				'+          Identificador del cargo
				
				If nBank = 33 Then
					If Not lrecTime.EOF Then
						FileNameCityDet = lstrLoadFile & "CITIBANK_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileNameCityDet, OpenMode.Output)
						llngRecCounter = 0
						ljdblAmountTot = 0
						
						Do While Not lrecTime.EOF
							lstrWritTxt = "062" & "001"
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy"), "0", 9) & FormatData(lrecTime.FieldToClass("sdigipol"), "0", 1)
							lstrWritTxt = lstrWritTxt & New String(".", 15)
							lstrWritTxt = lstrWritTxt & New String(" ", 8)
							lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nAmount")), "0", 11)
							lstrWritTxt = lstrWritTxt & CStr(Format(dExpirdate, "yyyyMMdd"))
							lstrWritTxt = lstrWritTxt & New String(".", 4)
							PrintLine(FileNum, lstrWritTxt)
							llngRecCounter = llngRecCounter + 1
							ljdblAmountTot = ljdblAmountTot + CDbl(System.Math.Round(lrecTime.FieldToClass("nAmount")))
							lrecTime.RNext()
						Loop 
						FileClose(FileNum)
						
						'+ Grabar primera linea
						FileName = lstrLoadFile & "CITYBANK_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum2 = FreeFile
						FileOpen(FileNum2, FileName, OpenMode.Output)
						lstrWritTxt = "1" & "062" & "001" & "2"
						lstrWritTxt = lstrWritTxt & CStr(Format(dExpirdate, "yyyyMMdd"))
						lstrWritTxt = lstrWritTxt & FormatData(llngRecCounter, "0", 6) 'nro de registros
						lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(ljdblAmountTot), "0", 13) 'monto total
						lstrWritTxt = lstrWritTxt & New String(".", 28)
						PrintLine(FileNum2, lstrWritTxt)
						
						'+ Grabar detalle a continuación de primera linea en archivo definitivo
						FileNum = FreeFile
						FileOpen(FileNum, FileNameCityDet, OpenMode.Input)
						While Not EOF(FileNum)
							lstrWritTxt = LineInput(FileNum)
							PrintLine(FileNum2, lstrWritTxt)
						End While
						FileClose(FileNum)
						Kill(FileNameCityDet)
						FileClose(FileNum2)
						
						'+ Borrar archivo detalle : FileNameCityDet
					End If
				End If
			End If
			'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjGeneral = Nothing
			
			'+Se retorna el nombre de archivo generado
			If FileName <> String.Empty Then
				sFileName = lstrDirFile & Right(FileName, Len(FileName) - InStrRev(FileName, "\"))
			Else
				sFileName = String.Empty
			End If
		End If
		
		
insGenFilesCOL500_Err: 
		If Err.Number Then
			insGenFilesCOL500 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCompany = Nothing
		'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClient = Nothing
	End Function
	
	Public Function insPostCOL910(ByVal nSucursal As Double, ByVal nOffice As Double, ByVal nAgency As Double, ByVal dDateEnd As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecDate As eRemoteDB.Execute
		
		On Error GoTo insPostCOL910_Err
		
		lrecDate = New eRemoteDB.Execute
		
		lstrKey = Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
		
		With lrecDate
			.StoredProcedure = "InsCol910"
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nSucursal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NOfficeAgen", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCOL910 = .Run(False)
		End With
		
		
insPostCOL910_Err: 
		If Err.Number Then
			insPostCOL910 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDate = Nothing
	End Function
	
	Public Function insPostCOL911(ByVal nSucursal As Double, ByVal nOffice As Double, ByVal nAgency As Double, ByVal dDateEnd As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecDate As eRemoteDB.Execute
		
		On Error GoTo insPostCOL911_Err
		
		lrecDate = New eRemoteDB.Execute
		
		lstrKey = Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
		
		With lrecDate
			.StoredProcedure = "InsCol911"
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nSucursal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NOfficeAgen", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCOL911 = .Run(False)
		End With
		
		
insPostCOL911_Err: 
		If Err.Number Then
			insPostCOL911 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDate = Nothing
	End Function
	'**-Global constants definition. Intermediary type management
	'-Se definen las constantes globales para el manejo del tipo de intermediarios
	
	
	'%insValCOL001: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL001(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nRecOri As Integer, ByVal nCurrency_ As Integer, ByVal nMovType As Integer, ByVal nInfoOrder As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL001_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nRecOri = eRemoteDB.Constants.intNull Then nRecOri = 0
		If nCurrency_ = eRemoteDB.Constants.intNull Then nCurrency_ = 0
		If nMovType = eRemoteDB.Constants.intNull Then nMovType = 0
		If nInfoOrder = eRemoteDB.Constants.intNull Then nInfoOrder = 0
		
		With lobjErrors
			'+ Validacion del campo "Fecha desde"
			If dInitDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3237)
			End If
			
			'+ Validacion del campo "Fecha desde"
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3239)
			End If
			
			'+ Validacion de que la fecha hasta sea mayor que la fecha desde.
			If dInitDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull Then
				If dEndDate <= dInitDate Then
					.ErrorMessage(sCodispl, 12120)
				End If
				If nInfoOrder = 0 Or nInfoOrder = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 5075)
				End If
			End If
			
			'+ Validacion del campo "Tipo de origen del recibo"
			If nRecOri = 0 Then
				.ErrorMessage(sCodispl, 750034)
			End If
			insValCOL001 = .Confirm
		End With
		
insValCOL001_Err: 
		If Err.Number Then
			insValCOL001 = "InsValCOL001: " & insValCOL001 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL002: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL002(ByVal sCodispl As String, ByVal dProcessDate As Date, ByVal nTypBank As Integer, ByVal nCardType As Integer, ByVal nDef As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL002_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nTypBank = eRemoteDB.Constants.intNull Then nTypBank = 0
		If nCardType = eRemoteDB.Constants.intNull Then nCardType = 0
		If nDef = eRemoteDB.Constants.intNull Then nDef = 0
		
		With lobjErrors
			'+ Validacion del campo "Fecha de proceso"
			If dProcessDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5072)
				lblnError = True
			Else
				If dProcessDate > Today Then
					.ErrorMessage(sCodispl, 1002)
					lblnError = True
				End If
			End If
			
			'+ Validacion del campo "Tipo de origen del recibo"
			If nCardType = 0 And nTypBank = 2 Then
				.ErrorMessage(sCodispl, 750034)
				lblnError = True
			End If
			
			insValCOL002 = .Confirm
		End With
		
insValCOL002_Err: 
		If Err.Number Then
			insValCOL002 = "InsValCOL002: " & insValCOL002 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL002: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL832(ByVal sCodispl As String, ByVal nIniMonth As Double, ByVal nIniYear As Double, ByVal nPerMonth As Double, ByVal nPerYear As Double) As String
		Dim lobjErrors As eFunctions.Errors
		On Error GoTo insValCOL832_Err
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			If nIniMonth = eRemoteDB.Constants.intNull Or nIniMonth = 0 Then
				.ErrorMessage(sCodispl, 60340)
			End If
			If nIniYear = eRemoteDB.Constants.intNull Or nIniYear = 0 Then
				.ErrorMessage(sCodispl, 9060)
			End If
			If nPerMonth = eRemoteDB.Constants.intNull Or nPerMonth = 0 Then
				.ErrorMessage(sCodispl, 2034)
			End If
			If nPerYear = eRemoteDB.Constants.intNull Or nPerYear = 0 Then
				.ErrorMessage(sCodispl, 2034)
			End If
			insValCOL832 = .Confirm
		End With
		
insValCOL832_Err: 
		If Err.Number Then
			insValCOL832 = "InsValCOL832: " & insValCOL832 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	'%insValCOL005: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL005(ByRef sCodispl As String, ByRef dProcessDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjOptions As eGeneral.OptionsInstallation
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL005_Err
		
		lobjErrors = New eFunctions.Errors
		lobjOptions = New eGeneral.OptionsInstallation
		
		'**+Process date field validations
		'+Validacion del campo "Fecha de proceso"
		If dProcessDate = eRemoteDB.Constants.dtmNull Then
			lobjErrors.ErrorMessage(sCodispl, 5072)
			lblnError = True
		Else
			If dProcessDate > Today Then
				lobjErrors.ErrorMessage(sCodispl, 1002)
				lblnError = True
			End If
		End If
		
		'+Validacion de la conexión entre cobranza y caja
		
		If lobjOptions.FindOptBank Then
			If lobjOptions.nCollect_pCash = 2 Then
				lobjErrors.ErrorMessage(sCodispl, 750067)
				lblnError = True
			End If
		End If
		
		insValCOL005 = lobjErrors.Confirm
		
insValCOL005_Err: 
		If Err.Number Then
			insValCOL005 = "insValCOL005: " & insValCOL005 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjOptions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjOptions = Nothing
	End Function
	
	'%insValCOL500: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL500(ByVal sCodispl As String, ByVal dExpirDat As Date, ByVal nWay_Pay As Integer, ByVal nInsur_area As Integer, ByVal nBank As Double, ByVal sOptCurrency As String, ByVal dIncrease As Date, ByVal nUsercode As Integer, ByVal nTyp_CreCard As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsMultipac As eCollection.Bank_Agree
		
		On Error GoTo insValCOL500_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nWay_Pay = eRemoteDB.Constants.intNull Then nWay_Pay = 0
		If nInsur_area = eRemoteDB.Constants.intNull Then nInsur_area = 0
		If nUsercode = eRemoteDB.Constants.intNull Then nUsercode = 0
		
		With lobjErrors
			'+ Validacion del campo "Fecha del vencimiento"
			If dExpirDat = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de vencimiento: ")
			End If
			
			'+ Validacion del campo "Vìa del pago"
			If nWay_Pay = 0 Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Vía del pago: ")
			End If
			
			'+ Validacion del campo "Área del seguro"
			If nInsur_area = 0 Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Área del seguro: ")
			End If
			
			'+ Si vía de pago es PAC y el Banco esta en multipac debe ser lider
			If nWay_Pay = 1 Then
				If nBank = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60493)
				Else
					lclsMultipac = New eCollection.Bank_Agree
					If Not lclsMultipac.Find_ExistMult(0, nBank, 1) Then
						If lclsMultipac.Find_ExistMult(0, nBank, 2) Then
							.ErrorMessage(sCodispl, 60501)
						End If
					End If
					'UPGRADE_NOTE: Object lclsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMultipac = Nothing
				End If
            ElseIf nWay_Pay = 2 Then
			'+ Validacion del campo "Tipo de tarjeta de crédito"
			    If nTyp_CreCard <= 0 Then
				    .ErrorMessage(sCodispl, 3864)
			    End If
			End If
			
			'+ Validacion del campo "Fecha de valorización"
			If dIncrease = eRemoteDB.Constants.dtmNull Then
                If sOptCurrency = "2" Or nWay_Pay = 4 Or nWay_Pay = 3 Then
                    .ErrorMessage(sCodispl, 55527)
                End If
			End If
			
			insValCOL500 = .Confirm
		End With
		
insValCOL500_Err: 
		If Err.Number Then
			insValCOL500 = "InsValCOL500: " & insValCOL500 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insPostCOL500: Esta función se encarga de generar la información solicitada.
    Public Function insPostCOL500(ByVal sCodispl As String, ByVal dExpirdate As Date, ByVal nWay_Pay As Integer, ByVal nInsur_area As Integer, ByVal nCod_Agree As Integer, ByVal nBank As Double, ByVal sOptGenera As String, ByVal sOptCurrency As String, ByVal sOptProcess As String, ByVal dIncrease As Date, ByVal nUsercode As Integer, Optional ByVal sTakeOld As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal nTyp_CreCard As Integer = 0, Optional ByVal nproduct As Integer = 0) As Boolean
        Dim lrecTime As eRemoteDB.Execute

        On Error GoTo insPostCOL500_Err

        lrecTime = New eRemoteDB.Execute

        '+Para el proceso en linea (como es este caso al ser llamado desde la dll)
        '+se crea una llave que comience con 'L*' para después borrar los datos generados
        '+Así se evita eliminar los datos generados por el modo batch (cuya clave comienza con 'T*')
        lstrKey = "L" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

        With lrecTime
            .StoredProcedure = "insCol500"
            .Parameters.Add("dExpirdate", dExpirdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptGenera", sOptGenera, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptCurrency", sOptCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptProcess", sOptProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIncrease", dIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcess", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNoProcess", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTakeOld", sTakeOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAgreeApvSef", "222", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_CreCard", nTyp_CreCard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nproduct", nproduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCOL500 = .Run(False)

            sProcess = .Parameters.Item("sProcess").Value
            sNoProcess = .Parameters.Item("sNoProcess").Value
            sAgreeApvSef = .Parameters.Item("sAgreeApvSef").Value
        End With

        If insPostCOL500 And sProcess = "1" And sOptProcess = "2" And (nWay_Pay = 1 Or nWay_Pay = 2) Then
            insPostCOL500 = insGenFilesCOL500(dExpirdate, nWay_Pay, nInsur_area, lstrKey, nBank)
        End If

insPostCOL500_Err:
        If Err.Number Then
            insPostCOL500 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTime = Nothing
    End Function
	
	
	'%insPostCOL500: Esta función se encarga de generar la información solicitada.
	Public Function insPostCOL723(ByVal sCodispl As String, ByVal dIni_Date As Date, ByVal dEnd_date As Date, ByVal nBranch As Integer, ByVal nReuse As Integer, ByVal nIntention As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lobjGeneral As eGeneral.GeneralFunction
		Dim lstrWritTxt As String
        Dim FileName As String = ""
        Dim FileNum As Integer
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim nCount As Integer
		Dim nBank_ant As Integer
		Dim mclsTables As Object
        Dim lstrDescripIntention As Object = New Object
        On Error GoTo insPostCOL723_Err
		lrecTime = New eRemoteDB.Execute
		
		mclsTables = New eFunctions.Tables
		If mclsTables.GetDescription("Table5641", nIntention) Then
			lstrDescripIntention = mclsTables.Descript
		End If
		
		lstrKey = "t_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
		Dim lclsValue As eFunctions.Values
		With lrecTime
			.StoredProcedure = "Readir_Debitprpar_Mandato"
			.Parameters.Add("dIni_Date", dIni_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_Date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReuse", nReuse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run() Then
				insPostCOL723 = True
				lobjGeneral = New eGeneral.GeneralFunction
				'+ Se busca la ruta en la que se guardará el archivo de texto
				lstrLoadFile = lobjGeneral.GetLoadFile()
				'+ Se busca el directorio virtual del archivo a crear
				lclsValue = New eFunctions.Values
				lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
				'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsValue = Nothing
				If Not .EOF Then
					FileName = lstrLoadFile & "\MANDATOS_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
					FileNum = FreeFile
					FileOpen(FileNum, FileName, OpenMode.Output)
					'imprime encabezado del primer banco seleccionado
					nCount = 1
					lstrWritTxt = "1"
					lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nBankext"), "0", 9)
					lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 1)
					lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nAccount"), "0", 11)
					lstrWritTxt = lstrWritTxt & Today.ToString("yyyyMMdd")
					lstrWritTxt = lstrWritTxt & FormatData(lstrDescripIntention, " ", 10)
					lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 70)
					PrintLine(FileNum, lstrWritTxt)
					nBank_ant = lrecTime.FieldToClass("nBankext")
					Do While Not lrecTime.EOF
						If nBank_ant <> lrecTime.FieldToClass("nBankext") Then
							nBank_ant = lrecTime.FieldToClass("nBankext")
							'imprime encabezado del banco cuando cambia a otro
							lstrWritTxt = "1"
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nBankext"), "0", 9)
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 1)
							lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nAccount"), "0", 11)
							lstrWritTxt = lstrWritTxt & CStr(Format(lrecTime.FieldToClass("dDate_Regist"), "yyyyMMdd"))
							lstrWritTxt = lstrWritTxt & FormatData(lstrDescripIntention, " ", 10)
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 70)
							PrintLine(FileNum, lstrWritTxt)
						End If
						
						lstrWritTxt = "2"
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClient"), "0", 20)
						lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 5)
						lstrWritTxt = lstrWritTxt & FormatData("0", "0", 15)
						lstrWritTxt = lstrWritTxt & FormatData("0", "0", 15)
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClient"), "0", 9)
						lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 1)
						lstrWritTxt = lstrWritTxt & CStr(Format(lrecTime.FieldToClass("dDate_Regist"), "yyyyMMdd"))
						lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 4)
						lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 20)
						lstrWritTxt = lstrWritTxt & FormatData("0", "0", 3)
						lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("sClient"), "0", 9)
						nCount = nCount + 1
						PrintLine(FileNum, lstrWritTxt)
						lrecTime.RNext()
					Loop 
					lstrWritTxt = "3"
					lstrWritTxt = lstrWritTxt & FormatData(nCount, "0", 7)
					lstrWritTxt = lstrWritTxt & FormatData("0", "0", 15)
					lstrWritTxt = lstrWritTxt & FormatData("0", "0", 7)
					lstrWritTxt = lstrWritTxt & FormatData("0", "0", 15)
					lstrWritTxt = lstrWritTxt & New String(" ", 65)
					PrintLine(FileNum, lstrWritTxt)
					FileClose(FileNum)
				End If
				
				'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjGeneral = Nothing
				If FileName <> String.Empty Then
					sFileName = lstrDirFile & Right(FileName, Len(FileName) - Len(lstrLoadFile) - 1)
				Else
					sFileName = String.Empty
				End If
			End If
		End With
		
insPostCOL723_Err: 
		If Err.Number Then
			insPostCOL723 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		'UPGRADE_NOTE: Object mclsTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsTables = Nothing
	End Function
	'%insValCOL502_K: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL502_K(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nWay_Pay As Integer, ByVal dLimit_pay As Date, ByVal dPayDate As Date, ByVal nUsercode As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCollection As eCollection.Bulletin
		Dim lblnError As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insValCOL502_K_Err
		lobjErrors = New eFunctions.Errors
		
		If nInsur_area = eRemoteDB.Constants.intNull Then nInsur_area = 0
		If nWay_Pay = eRemoteDB.Constants.intNull Then nWay_Pay = 0
		If nUsercode = eRemoteDB.Constants.intNull Then nUsercode = 0
		
		lblnError = False
		
		With lobjErrors
			'+ Validacion del campo "Área del seguro"
			If nInsur_area = 0 Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Área del seguro: ")
				lblnError = True
			End If
			
			'+ Validacion del campo "Vìa del pago"
			If nWay_Pay = 0 Then
				.ErrorMessage(sCodispl, 55002,  , eFunctions.Errors.TextAlign.LeftAling, "Vía del pago: ")
				lblnError = True
			End If
			
			'+ Validacion del campo "Fecha de cobranza"
			If dLimit_pay = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5072,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de cobranza: ")
				lblnError = True
			Else
				If dLimit_pay > Today Then
					.ErrorMessage(sCodispl, 1965)
				End If
			End If
			
			'+ Validacion del campo "Fecha de pago"
			If dPayDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de pago: ")
				lblnError = True
			Else
				If dPayDate > Today Then
					.ErrorMessage(sCodispl, 1965)
				End If
			End If
			
			'+ Si no exiten errores se crea la tabla temporal para el manejo del detalle
			If Not lblnError Then
				lrecTime = New eRemoteDB.Execute
				With lrecTime
					.StoredProcedure = "inscreTMP_COL502"
					.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dLimit_pay", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dPayDate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insValCOL502_K = CStr(.Run(False))
				End With
				'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecTime = Nothing
			End If
			
			insValCOL502_K = .Confirm
		End With
		
insValCOL502_K_Err: 
		If Err.Number Then
			insValCOL502_K = insValCOL502_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL502: Se efectuan las validaciones del la COL502
	Public Function insValCOL502(ByVal sCodispl As String, ByVal nCount As Integer, ByVal nInsur_area As Integer, ByVal nWay_Pay As Integer, ByVal dLimit_pay As Date, ByVal nBank As Double, ByVal nAmount As Double, ByVal nCommiss As Double) As String
		Dim lrecreaBulletins As eRemoteDB.Execute
		Dim mobjOpt_Premiu As eGeneral.opt_premiu
		
		lrecreaBulletins = New eRemoteDB.Execute
		mobjOpt_Premiu = New eGeneral.opt_premiu
		
		Dim ldblAmount As Double
		Dim ldblAmount_tot As Double
		
		Dim ldblBalance As Double
			
		On Error GoTo insValCOL502_Err
		
		'+ Debe existir al menos una linea selecionada
		If nCommiss = eRemoteDB.Constants.intNull Then
			nCommiss = 0
		End If
		
		With lrecreaBulletins
			.StoredProcedure = "reaBulletins_Amount_mupkg.reaBulletins_Amount"
			.Parameters.Add("dEffecDate", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				ldblAmount = .FieldToClass("nAmount")
				ldblAmount_tot = nAmount + nCommiss
				
				ldblBalance = ldblAmount - ldblAmount_tot
				
				'+ Si existe un sobrante de dinero
				'            If ldblBalance < 0 Then
				
				'+ se verifica que ese sobrante esté entre los límites de tolerancia (para el sobrante)
				'                If Abs(ldblBalance) > mobjOpt_Premiu.nUpper_lim Then
				'                   mclsError.ErrorMessage sCodispl, 750073, , RigthAling, " (" & Abs(ldblBalance) & ")"
				'              End If
				'           End If
				
				'+ Si existe un faltante de dinero
				If ldblBalance > 0 Then
					
					'+ se verifica que ese faltante esté entre los límites de tolerancia (para el faltante)
					If System.Math.Abs(ldblBalance) > mobjOpt_Premiu.nLower_lim Then
						mclsError.ErrorMessage(sCodispl, 750077,  , eFunctions.Errors.TextAlign.RigthAling, " (" & System.Math.Abs(ldblBalance) & ")")
					End If
				End If
				.RCloseRec()
			End If
		End With
		
		'+  Fin validación
		insValCOL502 = mclsError.Confirm
		
insValCOL502_Err: 
		If Err.Number Then
			insValCOL502 = insValCOL502 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBulletins = Nothing
	End Function
	
	'%insValCOL502Upd: Valida que a lo menos se seleccione un Banco
	Public Function insValCOL502Upd(ByVal sCodispl As String, ByVal nCount As Integer) As String
		On Error GoTo insValCOL502Upd_Err
		
		'+ Debe existir al menos una linea selecionada
		If nCount < 1 Or nCount = eRemoteDB.Constants.intNull Then
			mclsError.ErrorMessage(sCodispl, 60263)
		End If
		
		'+  Fin validación
		insValCOL502Upd = mclsError.Confirm
		
insValCOL502Upd_Err: 
		If Err.Number Then
			insValCOL502Upd = insValCOL502Upd & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insFindMultAmount: Se recupera valor de boletines de bancos Multipac
	Public Function insFindMultLess(ByVal nInsur_area As Integer, ByVal nWay_Pay As Integer, ByVal dLimit_pay As Date, ByVal nBank As Double, ByVal nAmount As Double) As Boolean
		Dim lrecreaBulletins As eRemoteDB.Execute
		Dim lIsMultipac As Boolean
		Dim lclsMultipac As eCollection.Bank_Agree
		
		lrecreaBulletins = New eRemoteDB.Execute
		
		On Error GoTo insFindMultLess_Err
		lIsMultipac = False
		
		'+ Si la vía de pago es PAC se verifica si el banco pertenece a multipac
		If nWay_Pay = 1 Then
			lclsMultipac = New eCollection.Bank_Agree
			If lclsMultipac.Find_ExistMult(0, nBank, 1) Then
				lIsMultipac = True
			Else
				If lclsMultipac.Find_ExistMult(0, nBank, 2) Then
					lIsMultipac = True
				End If
			End If
			'UPGRADE_NOTE: Object lclsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsMultipac = Nothing
		End If
		
		'+ Si no es multipac verifico el monto de la prima
		If Not lIsMultipac Then
			With lrecreaBulletins
				.StoredProcedure = "reaBulletins_Amount_mupkg.reaBulletins_Amount"
				.Parameters.Add("dEffecDate", dLimit_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					If nAmount < .FieldToClass("nAmount") Then
						insFindMultLess = True
					Else
						insFindMultLess = False
					End If
				End If
				.RCloseRec()
			End With
		Else
			insFindMultLess = False
		End If
		
insFindMultLess_Err: 
		If Err.Number Then
			insFindMultLess = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBulletins = Nothing
	End Function
	
	'% insPostTCOL502Upd: Actualiza los registros correspondientes en la tabla
	Public Function insPostTCOL502Upd(ByVal sAction As String, ByVal nId_Register As Integer, ByVal nCommiss As Double) As Boolean
		Dim lrecTCOL502upd As eRemoteDB.Execute
		
		On Error GoTo insPostTCOL502Upd_Err
		
		lrecTCOL502upd = New eRemoteDB.Execute
		
		With lrecTCOL502upd
			.StoredProcedure = "insupdTMP_COL502"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_Reg", nId_Register, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommiss", nCommiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostTCOL502Upd = .Run(False)
		End With
		
insPostTCOL502Upd_Err: 
		If Err.Number Then
			insPostTCOL502Upd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTCOL502upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTCOL502upd = Nothing
	End Function
	
	'%insValCOL507: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL507(ByVal sCodispl As String, ByVal nBank As Double, ByVal nAcc_number As Integer, ByVal dPayDate As Date, ByVal dLimit_pay As Date, ByVal sFile As String, ByVal nAmountpay As Double, ByVal nUsercode As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim llngError As Integer
		Dim lrecBank As eRemoteDB.Execute
        Dim sDesc_Bank As String = ""

        On Error GoTo insValCOL507_Err
		
		lobjErrors = New eFunctions.Errors
		lrecBank = New eRemoteDB.Execute
		
		If nBank = eRemoteDB.Constants.intNull Then nBank = 0
		If nAcc_number = eRemoteDB.Constants.intNull Then nAcc_number = 0
		If nAmountpay = eRemoteDB.Constants.intNull Then nAmountpay = 0
		If nUsercode = eRemoteDB.Constants.intNull Then nUsercode = 0
		
		With lobjErrors
			'+ Validacion del campo "Código del banco"
			If nBank = 0 Then
				.ErrorMessage(sCodispl, 7004)
				lblnError = True
			Else
				With lrecBank
					.StoredProcedure = "reaGeneralPKG.reaBank"
					.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDesc_Bank", sDesc_Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If .Run(False) Then
						sDesc_Bank = .Parameters("sDesc_Bank").Value
						If sDesc_Bank = "0" Then
							lobjErrors.ErrorMessage(sCodispl, 6057)
							lblnError = True
						End If
					End If
				End With
			End If
			'UPGRADE_NOTE: Object lrecBank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecBank = Nothing
			
			'+ Validacion del campo "Cuenta corriente"
			If nAcc_number = 0 Then
				.ErrorMessage(sCodispl, 3058)
				lblnError = True
			End If
			
			'+ Validacion del campo "Nombre del archivo a procesar"
			If sFile = String.Empty Then
				.ErrorMessage(sCodispl, 55026)
				lblnError = True
			End If
			
			'+ Validacion del campo "Fecha de pago"
			If dPayDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 2056)
				lblnError = True
			Else
				If dPayDate > Today Then
					.ErrorMessage(sCodispl, 1002)
					lblnError = True
				End If
			End If
			
			'+ Si no hay errores; es decir, toda la información requerida fue introducida.
			If Not lblnError Then
				llngError = loadTmp_bulls_bank(sFile, nBank)
				If llngError = 0 Then
					.ErrorMessage(sCodispl, 55030)
				End If
				If llngError < 0 Then
					If llngError = -3 Then
						.ErrorMessage(sCodispl, 98011)
					Else
						.ErrorMessage(sCodispl, 1949)
					End If
				End If
			End If
			
			insValCOL507 = .Confirm
			
		End With
		
insValCOL507_Err: 
		If Err.Number Then
			insValCOL507 = "insValCOL507: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL511: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL511(ByVal sCodispl As String, ByVal nBank As Double, ByVal sName As String, ByVal nErrornum As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL511_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nBank = eRemoteDB.Constants.intNull Then nBank = 0
		If nErrornum = eRemoteDB.Constants.intNull Then nErrornum = 0
		
		With lobjErrors
			'+ Validacion del campo "Código del banco"
			If nBank = 0 Then
				.ErrorMessage(sCodispl, 10828)
				lblnError = True
			End If
			
			'+  Validacion del campo "Nombre del archivo a procesar"
			If sName = String.Empty Then
				.ErrorMessage(sCodispl, 98007)
				lblnError = True
			Else
				'+          Validacion del campo "Archivo a procesar"
				If nErrornum <> 0 Then
					.ErrorMessage(sCodispl, nErrornum)
					lblnError = True
				End If
			End If
			
			insValCOL511 = .Confirm
			
		End With
		
insValCOL511_Err: 
		If Err.Number Then
			insValCOL511 = "InsValCOL511: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insPostCOL511: Esta función se encarga de generar la información solicitada.
	Public Function insPostCOL511(ByVal sCodispl As String, ByVal nBank As Double, ByVal sName As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lclsParam_univ As eCollection.Param_universe
		Dim lclsBank_univ As eCollection.Bank_Universe
        Dim lstrCR As String = ""
        Dim lblnProc As Boolean
		Dim lblnProcCli As Boolean
		Dim lintCounter As Integer
		Dim lstrClient As String
		
		On Error GoTo insPostCOL511_Err
		
		lrecTime = New eRemoteDB.Execute
		lclsParam_univ = New eCollection.Param_universe
		lclsBank_univ = New eCollection.Bank_Universe
		
		llngUsercode = nUsercode
		If lclsParam_univ.Find(nBank) Then
			FileOpen(1, sName, OpenMode.Input)
			lintCounter = 1
			Do While Not EOF(1)
				Input(1, lstrCR)
				lblnProc = False
				With lclsParam_univ
					'+ Se identifica si el registro debe procesarse o no
					If .nPosIniReg = 0 Or .nPosIniReg = eRemoteDB.Constants.intNull Then
						
						'+ Caso cuando la posición inicial es nula y el indicador del registro de totales
						'+ es "Primero", el primer registro no se procesa
						If .sIndTypTot = "1" And lintCounter <> 1 Then
							lblnProc = True
						End If
						
						'+ Caso cuando la posición inicial es nula y el indicador del registro de totales
						'+ es "Ultimo", el ultimo registro no se procesa
						If (.sIndTypTot = "2") Then
							lblnProc = True
						End If
						
						'+ Caso cuando la posición inicial es nula y el indicador del registro de totales
						'+ es "No tiene", se procesan todos los registros
						If (.sIndTypTot = "3") Then
							lblnProc = True
						End If
					Else
						If .nPosIniReg <> 0 And .nPosIniReg <> eRemoteDB.Constants.intNull Then
							'+ Si el registro contiene el tipo de registro indicado en el campo tipo de registro
							If RTrim(CStr(Mid(lstrCR, .nPosIniReg, (.nPosEndReg - .nPosIniReg) + 1))) = RTrim(.sIndTypReg) Then
								lblnProc = True
							End If
						End If
					End If
					
					'+ Si el registro debe procesarse
					If lblnProc Then
						lblnProcCli = False
						
						'+ Se identifica si el cliente debe procesarse
						
						'+ Si no se indica estado para el cliente
						If .nPosIniStat = 0 Or .nPosIniStat = eRemoteDB.Constants.intNull Then
							lblnProcCli = True
						Else
							'+ Si se indica estado para el cliente , se verifica que coincida con el estado del registro
							If .nPosIniStat <> 0 Then
								If RTrim(CStr(Mid(lstrCR, .nPosIniStat, (.nPosEndStat - .nPosIniStat) + 1))) = RTrim(.sIndStat) Then
									lblnProcCli = True
								End If
							End If
						End If
						
						'+ Si corresponde procesar el cliente
						If lblnProcCli Then
							lstrClient = Mid(lstrCR, .nPosIniCli, (.nPosEndCli - .nPosIniCli) + 1)
							
							'+ Si no existe el registro previamente, se inserta
							With lclsBank_univ
								.sClient = lstrClient
								.nBank_code = nBank
								.nUsercode = llngUsercode
								If Not .Find(nBank, lstrClient, True) Then
									.sClient = lstrClient
									.nBank_code = nBank
									.Add()
								End If
							End With
						End If
					End If
				End With
				lintCounter = lintCounter + 1
			Loop 
			FileClose(1)
		End If
		
insPostCOL511_Err: 
		If Err.Number Then
			insPostCOL511 = False
		End If
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		'UPGRADE_NOTE: Object lclsParam_univ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsParam_univ = Nothing
		'UPGRADE_NOTE: Object lclsBank_univ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_univ = Nothing
	End Function
	
	'%insPostCOL502: Esta función se encarga de realizar la imputación solicitada.
	Public Function insPostCOL502(ByVal sKey As String, ByVal nInsur_area As Integer, ByVal nWay_Pay As Integer, ByVal dLimit_Date As Date, ByVal dPayDate As Date, ByVal nBank As Double, ByVal nAcc_Bank As Integer, ByVal dEffecdate As Date, ByVal nMovement As Integer, ByVal nAmount_Dep As Double, ByVal nCommiss As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insPostCOL502_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "insImputationPac"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimit_Date", dLimit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayDate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Bank", nAcc_Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Deposit", nAmount_Dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '+OJO
			.Parameters.Add("nCommiss", nCommiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '+OJO
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostCOL502 = .Run(False)
		End With
		If insPostCOL502 Then
			insPostCOL502 = insGenFilesCOL502(sKey)
			insPostCOL502 = True
		Else
			insPostCOL502 = False
		End If
		
insPostCOL502_Err: 
		If Err.Number Then
			insPostCOL502 = False
		End If
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%insValCOL556: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL556(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal ProcessTyp As Integer, ByVal dOperdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL556_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ Validación del campo "Area de seguro"
			If nInsur_area <= 0 Then
				.ErrorMessage(sCodispl, 55031)
			End If
			
			'+ Validación del campo "Fecha de cobro"
			If dOperdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 21059)
			End If
			
			insValCOL556 = .Confirm
		End With
		
insValCOL556_Err: 
		If Err.Number Then
			insValCOL556 = "insValCOL556: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL585: Validaciones de entrada de parametros del Pareo de Mandatos
	Public Function insValCOL585(ByVal sCodispl As String, ByVal dProcDate As Date, ByVal nBank As Double, ByVal sNameFile As String, ByVal nErrornum As Integer, ByVal nUsercode As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsMultipac As eCollection.Bank_Agree
		Dim lblnError As Boolean
		Dim llngError As Integer
		
		On Error GoTo insValCOL585_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nBank = eRemoteDB.Constants.intNull Then nBank = 0
		If nErrornum = eRemoteDB.Constants.intNull Then nErrornum = 0
		If nUsercode = eRemoteDB.Constants.intNull Then nUsercode = 0
		
		With lobjErrors
			'+ Validacion del campo "Fecha de proceso"
			If dProcDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 7116)
				lblnError = True
			End If
			
			'+ Validacion del campo "Código del banco"
			If nBank = 0 Then
				.ErrorMessage(sCodispl, 10828)
				lblnError = True
			Else
				lclsMultipac = New eCollection.Bank_Agree
				If Not lclsMultipac.Find_ExistMult(0, nBank, 1) Then
					If lclsMultipac.Find_ExistMult(0, nBank, 2) Then
						.ErrorMessage(sCodispl, 60501)
						lblnError = True
					End If
				End If
				'UPGRADE_NOTE: Object lclsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsMultipac = Nothing
			End If
			
			'+ Validacion del campo "Nombre del archivo a procesar"
			If sNameFile = String.Empty Then
				.ErrorMessage(sCodispl, 55026)
				lblnError = True
			Else
				'+      Validacion del campo "Archivo a procesar"
				If nErrornum <> 0 Then
					.ErrorMessage(sCodispl, nErrornum)
					lblnError = True
				End If
			End If
			
			'+ Si no hay errores; es decir, toda la información requerida fue introducida.
			If Not lblnError Then
				llngError = loadTmp_Auth_bank(dProcDate, nBank, sNameFile)
				If llngError = 0 Then
					.ErrorMessage(sCodispl, 55030)
				End If
				If llngError < 0 Then
					If llngError = -3 Then
						.ErrorMessage(sCodispl, 98011)
					Else
						.ErrorMessage(sCodispl, 1949)
					End If
				End If
			End If
			
			insValCOL585 = .Confirm
		End With
		
insValCOL585_Err: 
		If Err.Number Then
			insValCOL585 = "insValCOL585: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL594: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL594(ByVal sCodispl As String, ByVal nOper As Integer, ByVal nWay_Pay As Integer, ByVal nAgreement As Integer, ByVal dCollDate As Date, ByVal nBullStart As Double, ByVal nBullEnd As Double, ByVal nCancel_Cod As Integer) As String
		'+ Variables: nOper       : Operación
		'+            nWay_pay    : via de pago
		'+            nAgreement  : convenio
		'+            dCollDate   : fecha cobranza
		'+            nBullStart  : boletin inicial
		'+            nBullEnd    : boletin final
		'+            nCancel_Cod : causa de anulación
		
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL594_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nWay_Pay = eRemoteDB.Constants.intNull Then nWay_Pay = 0
		If nOper = eRemoteDB.Constants.intNull Then nOper = 0
		If nAgreement = eRemoteDB.Constants.intNull Then nAgreement = 0
		If nBullStart = eRemoteDB.Constants.intNull Then nBullStart = 0
		If nBullEnd = eRemoteDB.Constants.intNull Then nBullEnd = 0
		If nCancel_Cod = eRemoteDB.Constants.intNull Then nCancel_Cod = 0
		
		
		With lobjErrors
			'+ Debe inidicarse al menos un criterio de selección
			If nWay_Pay = 0 Then
				If nBullStart = 0 Or nBullEnd = 0 Then
					.ErrorMessage(sCodispl, 60105)
					lblnError = True
				End If
			End If
			
			'+  Boletin Final no debe ser menor a Boletin Inicial
			If nBullStart > nBullEnd Then
				.ErrorMessage(sCodispl, 60106)
				lblnError = True
			End If
			
			'+  Si Boletin Inicial tiene valor Boletin Final debe ser mayor que 0
			If nBullStart > 0 And nBullEnd = 0 Then
				.ErrorMessage(sCodispl, 60107)
				lblnError = True
			End If
			
			'+ Validacion del campo "Fecha de Cobranza", debe estar lleno si via de pago posee información
			If nWay_Pay <> 0 Then
				If dCollDate = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 55559)
					lblnError = True
				End If
			End If
			
			'+ Validacion del campo "Convenio"
			If nOper = 2 Then
				If nCancel_Cod = 0 Then
					.ErrorMessage(sCodispl, 9101)
					lblnError = True
				End If
			End If
			
			insValCOL594 = .Confirm
		End With
		
insValCOL594_Err: 
		If Err.Number Then
			insValCOL594 = "insValCOL594: " & Err.Description
		End If
		On Error GoTo 0
		lobjErrors = New eFunctions.Errors
		
	End Function
	
	'%insPostCOL594: Esta función se encarga de generar la información solicitada.
	Public Function insPostCOL594(ByVal sCodispl As String, ByVal nOper As Integer, ByVal nWay_Pay As Integer, ByVal nAgreement As Integer, ByVal dCollDate As Date, ByVal nBullStart As Double, ByVal nBullEnd As Double, ByVal nCancel_Cod As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insPostCOL594_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "insBulletinsNull"
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimit_Pay", dCollDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBullStart", nBullStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBullEnd", nBullEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCancel_Cod", nCancel_Cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCommit", "S", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCOL594 = True
			End If
		End With
		
insPostCOL594_Err: 
		If Err.Number Then
			insPostCOL594 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%insValCOL626: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL626(ByVal sCodispl As String, ByVal nCod_Agree As Integer, ByVal dProcessDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL626_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nCod_Agree = eRemoteDB.Constants.intNull Then
			nCod_Agree = 0
		End If
		
		With lobjErrors
			'+ Validacion del campo "Fecha de Ejecucion"
			If dProcessDate = eRemoteDB.Constants.dtmNull Then
				lobjErrors.ErrorMessage(sCodispl, 1967,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Ejecución: ")
			End If
			
			'+ Validacion del campo "Convenio"
			If nCod_Agree = 0 Then
				.ErrorMessage(sCodispl, 55004,  , eFunctions.Errors.TextAlign.LeftAling, "Convenio: ")
			End If
			
			insValCOL626 = .Confirm
		End With
		
insValCOL626_Err: 
		If Err.Number Then
			insValCOL626 = "InsValCOL626: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL684: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL684(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nCollectorPre As Double, ByVal nCollectorNew As Double, ByVal dProcessDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lobjCollector As eCollection.Collector
		
		On Error GoTo insValCOL684_Err
		
		lobjErrors = New eFunctions.Errors
		lobjCollector = New eCollection.Collector
		
		If nInsur_area = eRemoteDB.Constants.intNull Then
			nInsur_area = 0
		End If
		If nCollectorPre = eRemoteDB.Constants.intNull Then
			nCollectorPre = 0
		End If
		If nCollectorNew = eRemoteDB.Constants.intNull Then
			nCollectorNew = 0
		End If
		
		With lobjErrors
			'+ Validacion del campo "Area de Seguro"
			If nInsur_area = 0 Then
				.ErrorMessage(sCodispl, 55031)
			End If
			
			'+ Validacion del campo "Cobrador actual"
			If nCollectorPre = 0 Then
				.ErrorMessage(sCodispl, 60346)
			Else
				If Not lobjCollector.Find(nCollectorPre, CStr(eRemoteDB.Constants.strNull)) Then
					.ErrorMessage(sCodispl, 60347,  , eFunctions.Errors.TextAlign.LeftAling, "Cobrador Actual: ")
				End If
			End If
			
			'+ Validacion del campo "Cobrador nuevo"
			If nCollectorNew = 0 Then
				.ErrorMessage(sCodispl, 60348)
			Else
				If Not lobjCollector.Find(nCollectorNew, CStr(eRemoteDB.Constants.strNull)) Then
					.ErrorMessage(sCodispl, 60347,  , eFunctions.Errors.TextAlign.LeftAling, "Cobrador Nuevo: ")
				Else
					If nCollectorNew = nCollectorPre Then
						.ErrorMessage(sCodispl, 55808)
					End If
				End If
			End If
			
			'+ Validacion del campo "Fecha del proceso"
			If dProcessDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 55581)
			End If
			
			insValCOL684 = .Confirm
		End With
		
insValCOL684_Err: 
		If Err.Number Then
			insValCOL684 = "insValCOL684: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCollector = Nothing
	End Function
	
	'%insValCOL704: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL704(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dCollectdate As Date, ByVal nWayPay As Integer, ByVal nBank As Double, ByVal sNameFile As String, ByVal nErrornum As Integer, ByVal nUsercode As Integer, ByVal nCod_Agree As Short) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjCollector As eCollection.Collector
		Dim lobjGeneralFunction As eGeneral.GeneralFunction
		Dim lclsMultipac As eCollection.Bank_Agree
		Dim lblnError As Boolean
		Dim llngError As Integer
		
		On Error GoTo insValCOL704_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nInsur_area = eRemoteDB.Constants.intNull Then
			nInsur_area = 0
		End If
		If nWayPay = eRemoteDB.Constants.intNull Then
			nWayPay = 0
		End If
		If nBank = eRemoteDB.Constants.intNull Then
			nBank = 0
		End If
		lblnError = False
		
		With lobjErrors
			'+  Validacion del campo "Area de Seguro"
			If nInsur_area = 0 Then
				.ErrorMessage(sCodispl, 55031)
				lblnError = True
			End If
			
			'+  Validacion del campo "Via de pago"
			If nWayPay = 0 Then
				.ErrorMessage(sCodispl, 55008)
				lblnError = True
			Else
				If nWayPay = 1 Then
					If nBank = 0 Then
						.ErrorMessage(sCodispl, 55000)
						lblnError = True
					Else
						lclsMultipac = New eCollection.Bank_Agree
						
						'+ Si banco esta en Multipac debe ser lider
						If Not lclsMultipac.Find_ExistMult(0, nBank, 1) Then ' Lider
							If lclsMultipac.Find_ExistMult(0, nBank, 2) Then ' Asociado
								.ErrorMessage(sCodispl, 60501)
							End If
						End If
						'UPGRADE_NOTE: Object lclsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsMultipac = Nothing
					End If
				End If
				
				'+ Si la via de pago es descuento por planilla
				If nWayPay = 3 Then
					If nCod_Agree = 0 Or nCod_Agree = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 60117)
					End If
				End If
			End If
			
			'+ Validación del campo "Fecha de cobranza"
			If dCollectdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 55559)
				lblnError = True
			End If
			
			''+ Validacion del campo "Nombre del archivo a procesar"
			'        If sNameFile = String.Empty Then
			'            .ErrorMessage sCodispl, 55026
			'            lblnError = True
			'        Else
			'
			''+ Validacion del campo "Archivo a procesar"
			'            If nErrornum <> 0 Then
			'                .ErrorMessage sCodispl, nErrornum
			'                lblnError = True
			'            End If
			'        End If
			
			'        If Not lblnError Then
			'            Set lobjGeneralFunction = New eGeneral.GeneralFunction
			'            lstrKey = lobjGeneralFunction.getsKey(nUsercode)
			'            Set lobjGeneralFunction = Nothing
			'
			'            llngError = loadTmp_Reject_bank(nInsur_area, _
			''                                            nWayPay, _
			''                                            nBank, _
			''                                            dCollectdate, _
			''                                            sNameFile, _
			''                                            lstrKey)
			'            If llngError = 0 Then
			'                .ErrorMessage sCodispl, 55030
			'            End If
			'            If llngError < 0 Then
			'                If llngError = -3 Then
			'                    .ErrorMessage sCodispl, 98011
			'                Else
			'                    .ErrorMessage sCodispl, 1949
			'                End If
			'            End If
			'        End If
			
			insValCOL704 = .Confirm
		End With
		
insValCOL704_Err: 
		If Err.Number Then
			insValCOL704 = "insValCOL704: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL723: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL723(ByVal sCodispl As String, ByVal dInit_date As Date, ByVal dEnd_date As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		Dim lintInit As Integer
		Dim lintEnd As Integer
		
		On Error GoTo insValCOL723_Err
		
		lobjErrors = New eFunctions.Errors
		
		lintInit = 0
		lintEnd = 0
		
		With lobjErrors
			'+  Validacion del campo "Fecha de inicio"
			If dInit_date = eRemoteDB.Constants.dtmNull Then
				lintInit = 1
				.ErrorMessage(sCodispl, 9071)
			End If
			
			'+  Validacion del campo "Fecha final"
			If dEnd_date = eRemoteDB.Constants.dtmNull Then
				lintEnd = 1
				.ErrorMessage(sCodispl, 9072)
			End If
			
			'+  Validacion del rango de fechas
			If lintInit = 0 And lintEnd = 0 Then
				If dEnd_date <= dInit_date Then
					.ErrorMessage(sCodispl, 60113)
				End If
			End If
			
			insValCOL723 = .Confirm
		End With
		
insValCOL723_Err: 
		If Err.Number Then
			insValCOL723 = "insValCOL723: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL742: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL742(ByVal sCodispl As String, ByVal nInsurArea As Integer, ByVal dCollectdate As Date, ByVal nCod_Agree As Integer) As String
		Dim lrecAgree As eCollection.Agreement
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL742_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+  Validacion del campo "Area de seguros"
			If nInsurArea = eRemoteDB.Constants.intNull Or nInsurArea = 0 Then
				.ErrorMessage(sCodispl, 55031,  , eFunctions.Errors.TextAlign.LeftAling, "Area de seguros: ")
			End If
			
			'+  Validacion del campo "Fecha de cobranzas"
			If dCollectdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 55559,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de cobranzas: ")
			End If
			
			'+  Validacion del campo "Convenio"
			If nCod_Agree <> eRemoteDB.Constants.intNull And nCod_Agree <> 0 Then
				lrecAgree = New eCollection.Agreement
				If Not lrecAgree.Find(nCod_Agree) Then
					.ErrorMessage(sCodispl, 55011,  , eFunctions.Errors.TextAlign.LeftAling, "Convenio: ")
				End If
				'UPGRADE_NOTE: Object lrecAgree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecAgree = Nothing
			End If
			
			'+ Se verifica si el titular del convenio tiene dinero disponible para el pago
			If InsVerifyAmount_Disp(nInsurArea, dCollectdate, nCod_Agree) Then
				.ErrorMessage(sCodispl, 80154,  , eFunctions.Errors.TextAlign.LeftAling, "Saldo: " & CStr(nBalance_Cli) & " / Pago: " & CStr(nAmount_Pay))
			End If
			
			insValCOL742 = .Confirm
		End With
		
insValCOL742_Err: 
		If Err.Number Then
			insValCOL742 = "insValCOL742: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL777: Se realizan las validaciones de la transacción de proceso de transferencia
	'               de recaudaciones.
	Public Function insValCOL777(ByVal sCodispl As String, ByVal dCollectIni As Date, ByVal dCollectEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL777_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+  Validacion del campo "Fecha de Inicio de Proceso"
			If dCollectIni = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha desde : ")
			End If
			
			'+  Validacion del campo "Fecha de Fin de proceso"
			If dCollectEnd = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha hasta : ")
			Else
				'+  Fecha hasta no debe ser menor a fecha desde
				If dCollectEnd < dCollectIni Then
					.ErrorMessage(sCodispl, 3108)
				End If
			End If
			
			insValCOL777 = .Confirm
		End With
		
insValCOL777_Err: 
		If Err.Number Then
			insValCOL777 = "InsValCOL777: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL910: Esta función se encarga de realizar el proceso de transferencia
	'                de recaudaciones.
	Public Function insValCOL910(ByVal sCodispl As String, ByVal nSucursal As Double, ByVal nOffice As Double, ByVal nAgency As Double, ByVal dDateEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL910_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+  Validacion del campo "Fecha Hasta"
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1097)
			End If
			
			insValCOL910 = .Confirm
		End With
		
		
insValCOL910_Err: 
		If Err.Number Then
			insValCOL910 = "InsValCOL910: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	Public Function insPostCOL777(ByVal dCollectIni As Date, ByVal dCollectEnd As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecColFormRef As eRemoteDB.Execute
		Dim FileName As String
		Dim FileNameCityDet As String
		Dim FileNum As Integer
		Dim lstrWritTxt As String
		Dim lstrLoadFile As Object
		Dim lstrDirFile As Object
		Dim sTyperec As Object
		Dim nposition As Object
		Dim nLeng As Object
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		On Error GoTo insPostCOL777_Err
		
		lrecColFormRef = New eRemoteDB.Execute
		
		Dim lclsValue As eFunctions.Values
		With lrecColFormRef
			.StoredProcedure = "insProTransBenlar"
			.Parameters.Add("dCollectIni", dCollectIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectEnd", dCollectEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCOL777 = .Run(False)
			If insPostCOL777 Then
				.StoredProcedure = "ReaCol777"
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run() Then
					lobjGeneral = New eGeneral.GeneralFunction
					'+ Se busca la ruta en la que se guardará el archivo de texto
					lstrLoadFile = lobjGeneral.GetLoadFile()
					'+ Se busca el directorio virtual del archivo a crear
					lclsValue = New eFunctions.Values
					lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
					'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsValue = Nothing
					'+ ------------------------------------------
					If Not .EOF Then
						FileName = lstrLoadFile & "\PRIPAG_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						Do While Not lrecColFormRef.EOF
							'+ se hace la traducion de los codigos de tipo de recaudación segun lo solicitado
							Select Case lrecColFormRef.FieldToClass("nIndRecDep")
								Case "3"
									sTyperec = "CI"
								Case "8"
									sTyperec = "CA"
								Case "5"
									sTyperec = "B1"
								Case "6"
									sTyperec = "B2"
								Case "7"
									sTyperec = "B3"
								Case Else
									sTyperec = "  "
							End Select
							nposition = InStr(1, lrecColFormRef.FieldToClass("nOri_amount"), ",")
							nLeng = Len(lrecColFormRef.FieldToClass("nOri_amount"))
							lstrWritTxt = ""
							lstrWritTxt = lstrWritTxt & FormatData(lrecColFormRef.FieldToClass("nPolicy"), " ", 10,  , "left")
							lstrWritTxt = lstrWritTxt & FormatData(sTyperec, " ", 2)
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							lstrWritTxt = lstrWritTxt & CStr(IIf(IsDbNull(lrecColFormRef.FieldToClass("dCollect")), "000000", Format(lrecColFormRef.FieldToClass("dCollect"), "yyyyMMdd")))
							If nposition = 0 Then
								lstrWritTxt = lstrWritTxt & FormatData(lrecColFormRef.FieldToClass("nOri_amount"), "0", 5, "Left")
								lstrWritTxt = lstrWritTxt & FormatData("0", "0", 2, "Left")
							Else
								lstrWritTxt = lstrWritTxt & FormatData(Mid(lrecColFormRef.FieldToClass("nOri_amount"), 1, nposition - 1), "0", 5)
								lstrWritTxt = lstrWritTxt & FormatData(Mid(lrecColFormRef.FieldToClass("nOri_amount"), nposition + 1, nLeng), "0", 2, "Left", "Left")
							End If
							lstrWritTxt = lstrWritTxt & FormatData(lrecColFormRef.FieldToClass("nAmount"), "0", 9)
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 23)
							PrintLine(FileNum, lstrWritTxt)
							lrecColFormRef.RNext()
						Loop 
						FileClose(FileNum)
					End If
				End If
				'+-------------------------------------------
			End If
		End With
		
insPostCOL777_Err: 
		If Err.Number Then
			insPostCOL777 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
	End Function
	'% insPostCOL832:
	Public Function insPostCOL832(ByVal nBranch As Double, ByVal nProduct As Double, ByVal nMonth As Double, ByVal nYear As Double, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecinsPostCOL832 As eRemoteDB.Execute
        lrecinsPostCOL832 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		lstrKey = "L" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
		
		With lrecinsPostCOL832
			.StoredProcedure = "rea_col832"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npermonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nperyear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCOL832 = True
			Else
				insPostCOL832 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL832 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL832 = Nothing
		
	End Function
	'% insPostCOL832_Res:
	Public Function insPostCOL832_Res(ByVal nBranch As Double, ByVal nProduct As Double, ByVal nIniMonth As Double, ByVal nIniYear As Double, ByVal nPerMonth As Double, ByVal nPerYear As Double, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecinsPostCOL832_Res As eRemoteDB.Execute
		
		lrecinsPostCOL832_Res = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		lstrKey = "L" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
		
		With lrecinsPostCOL832_Res
			.StoredProcedure = "Rea_Col832_6"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInimonth", nIniMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIniyear", nIniYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermonth", nPerMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeryear", nPerYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCOL832_Res = True
			Else
				insPostCOL832_Res = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL832_Res may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL832_Res = Nothing
		
	End Function
	
	
	'%valTmp_rejectBulletins: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Private Function valTmp_rejectBulletins(ByVal slstrKey As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "valTmp_rejectBulletins"
			.Parameters.Add("sKey", slstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nExist") = "1" Then
					valTmp_rejectBulletins = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%valTmp_Batch_err: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function valTmp_Batch_err(ByVal slstrKey As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "valTmp_Batch_err"
			.Parameters.Add("sKey", slstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nExist") = "1" Then
					valTmp_Batch_err = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'*loadTmp_bulls_bank: Se encarga de cargar la información de un archivo plano a una tabla temporal del sistema.
	Private Function loadTmp_bulls_bank(ByVal slstrFile As String, ByVal ldblBank_code As Double) As Integer
		Dim lrecTmp_bulls_bank As eRemoteDB.Execute
		Dim lstrInputData As String
		Dim llngPipe1 As Integer
		Dim llngPipe2 As Integer
		Dim llngPipe3 As Integer
		Dim llngPipe4 As Integer
		Dim llngPipe5 As Integer
		Dim llngPipe6 As Integer
		Dim llngPipe7 As Integer
		Dim ldblBank_codeF As Double
		Dim lstrClient As String
		Dim ldblBulletins As Double
		Dim ldtmPaydate As Date
		Dim ldtmVendate As Date
		Dim ldblAmount As Double
		Dim ldblAmountPay As Double
		Dim lblnOk As Boolean
		Dim lblnOk1 As Boolean
		Dim llngError As Integer
		
		lrecTmp_bulls_bank = New eRemoteDB.Execute
		
		llngError = 0
		
		On Error GoTo ErrorHandler
		
		FileOpen(1, slstrFile, OpenMode.Input)
		
		lblnOk = False
		lblnOk1 = False
		'+  Check for end of file.
		Do While Not EOF(1)
			'+      Read line of data.
			lstrInputData = LineInput(1)
			llngPipe1 = InStr(1, lstrInputData, "|")
			llngPipe2 = InStr(llngPipe1 + 1, lstrInputData, "|")
			llngPipe3 = InStr(llngPipe2 + 1, lstrInputData, "|")
			llngPipe4 = InStr(llngPipe3 + 1, lstrInputData, "|")
			llngPipe5 = InStr(llngPipe4 + 1, lstrInputData, "|")
			llngPipe6 = InStr(llngPipe5 + 1, lstrInputData, "|")
			llngPipe7 = InStr(llngPipe6 + 1, lstrInputData, "|")
			
			ldblBank_codeF = CDbl(Mid(lstrInputData, 1, llngPipe1 - 1))
			If ldblBank_codeF = ldblBank_code Then
				lstrClient = Mid(lstrInputData, llngPipe1 + 1, llngPipe2 - llngPipe1 - 1)
				ldblBulletins = CDbl(Mid(lstrInputData, llngPipe2 + 1, llngPipe3 - llngPipe2 - 1))
				ldtmPaydate = CDate(Mid(lstrInputData, llngPipe3 + 1, llngPipe4 - llngPipe3 - 1))
				ldblAmount = CDbl(Mid(lstrInputData, llngPipe4 + 1, llngPipe5 - llngPipe4 - 1))
				
				If llngPipe6 > 0 And llngPipe7 > 0 Then
					If Not (lblnOk1) Then
						mdtmLimit_Pay = CDate(Mid(lstrInputData, llngPipe5 + 1, llngPipe6 - llngPipe5 - 1))
						mdblAmount_pay = CDbl(Mid(lstrInputData, llngPipe6 + 1, llngPipe7 - llngPipe6 - 1))
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						If Not IsDbNull(ldtmVendate) And Not IsDbNull(ldblAmountPay) Then
							lblnOk1 = True
						End If
					End If
				End If
				
				With lrecTmp_bulls_bank
					.StoredProcedure = "creTmp_bulls_bank"
					.Parameters.Add("nBank_code", ldblBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBulletins", ldblBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dPaydate", ldtmPaydate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmount", ldblAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not lblnOk Then
						.Parameters.Add("sDelete", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lblnOk = True
					Else
						.Parameters.Add("sDelete", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Run(False)
				End With
			End If
		Loop 
		'+  Close file.
		FileClose(1)
		
		If lblnOk Then
			llngError = 1
		End If
		
		'UPGRADE_NOTE: Object lrecTmp_bulls_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTmp_bulls_bank = Nothing
		loadTmp_bulls_bank = llngError
		
ErrorHandler: 
		If Err.Number <> 0 Then
			'+      Evaluate error number.
			Select Case Err.Number
				'+          "File already open" error.
				Case 55
					llngError = -1
					'+              Close open file.
					FileClose(1)
				Case 53
					'+              "File already open" error.
					llngError = -2
					FileClose(1)
				Case 5
					llngError = -3
					FileClose(1)
				Case Else
					'+              Handle other situations here...
					llngError = -4
			End Select
			'+      Turn off error trapping.
			On Error GoTo 0
			loadTmp_bulls_bank = llngError
		End If
	End Function
	
	'*loadTmp_Auth_bank: Se encarga de cargar la información de Mandatos un archivo plano a una tabla temporal del sistema.
	Private Function loadTmp_Auth_bank(ByVal dldatProcDate As Date, ByVal nllngBank As Double, ByVal slstrFileName As String) As Integer
		Dim lrecTmp_Auth_bank As eRemoteDB.Execute
		
		Dim lblnOk As Boolean
		Dim lstrInputData As String
		Dim ldblPolicy As Double
		Dim llngError As Integer
		Dim sNose1 As String
		
		On Error GoTo loadTmp_Auth_bank_err
		
		lrecTmp_Auth_bank = New eRemoteDB.Execute
		
		llngError = 0
		
		'+  Suponiendo que el largo del numero de poliza es los
		'+        ultimos 10 caracteres de la linea
		'+  Suponiendo que el encabezado no se considera
		
		FileOpen(1, slstrFileName, OpenMode.Input)
		
		lblnOk = False
		'+  Read line Head.
		lstrInputData = LineInput(1)
		'+  Check for end of file.
		Do While Not EOF(1)
			'+      Read line of data.
			lstrInputData = LineInput(1)
			sNose1 = Mid(lstrInputData, 1, 8)
			ldblPolicy = Val(Mid(lstrInputData, 9, 8))
			
			With lrecTmp_Auth_bank
				.StoredProcedure = "creTmp_Auth_bank"
				.Parameters.Add("nBank", nllngBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dProcDate", dldatProcDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", ldblPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not lblnOk Then
					.Parameters.Add("sDelete", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					lblnOk = True
				Else
					.Parameters.Add("sDelete", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				.Run(False)
			End With
		Loop 
		'+  Close file.
		FileClose(1)
		
		If lblnOk Then
			llngError = 1
		End If
		
loadTmp_Auth_bank_err: 
		If Err.Number Then
			'+      Evaluate error number.
			Select Case Err.Number
				'+          "File already open" error.
				Case 55
					llngError = -1
					'+              Close open file.
					FileClose(1)
					'+          "File already open" error.
				Case 53
					llngError = -2
					FileClose(1)
				Case 5
					llngError = -3
					FileClose(1)
				Case Else
					'+              Handle other situations here...
					llngError = -4
			End Select
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTmp_Auth_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTmp_Auth_bank = Nothing
		loadTmp_Auth_bank = llngError
		
	End Function
	
	'%insValCOL628: se realizan las validaciones para la ventana
	'               Proceso de Cierre de Facturación
	Public Function insValCOL628(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dLastclosed As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		Dim ldtmLastClosedArea As Date
		
		On Error GoTo insValCOL628_Err
		
		lobjErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		With lobjErrors
			'+  El area de seguro debe estar llena : 55031
			If nInsur_area = 0 Or nInsur_area = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55031)
			End If
			
			'+  Fecha de Cierre debe estar lleno : 60108
			If dLastclosed = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 60108)
			Else
				With lclsvalField
					.Min = String.Empty
					.Max = String.Empty
					.EqualMax = True
					.ErrRange = 13282
					If Not .ValDate(dLastclosed,  , eFunctions.valField.eTypeValField.onlyvalid) Then
						Call lobjErrors.ErrorMessage(sCodispl, 7114)
					Else
						'+              Debe ser Posterior a ultima fecha de cierre registrada
						'+              para el área de seguro en tratamiento
						ldtmLastClosedArea = find_dLastClosed_area(nInsur_area)
						If dLastclosed <= ldtmLastClosedArea Then
							Call lobjErrors.ErrorMessage(sCodispl, 60257,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmLastClosedArea & ")")
						End If
					End If
				End With
			End If
			
			'+  Fin validación
			insValCOL628 = .Confirm
		End With
		
insValCOL628_Err: 
		If Err.Number Then
			insValCOL628 = "insValCOL628: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
	End Function
	
	'%find_dLastClosed_area: Busca la ultima fecha de cierre para un area determinada
	Public Function find_dLastClosed_area(ByVal nInsur_area As Integer) As Date
		
		Dim lvalBills_Num As eRemoteDB.Execute
		
		On Error GoTo find_dLastClosed_area_err
		
		lvalBills_Num = New eRemoteDB.Execute
		
		With lvalBills_Num
			.StoredProcedure = "REABILLS_NUM_DLAST"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				find_dLastClosed_area = .FieldToClass("dLast")
			Else
				find_dLastClosed_area = eRemoteDB.Constants.dtmNull
			End If
		End With
		
find_dLastClosed_area_err: 
		If Err.Number Then
			find_dLastClosed_area = eRemoteDB.Constants.dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lvalBills_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalBills_Num = Nothing
	End Function
	
	'%insPostCOL628: se realizan carga de la fecha de cierre por area
	'                Proceso de Cierre de Facturación en bills_num
	Public Function insPostCOL628(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dLastclosed As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insPostCOL628_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "UPDBILLS_NUM_DLAST"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastClosed", dLastclosed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCOL628 = True
			End If
		End With
		
insPostCOL628_Err: 
		If Err.Number Then
			insPostCOL628 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%insPostCOL742: Esta función se encarga de realizar el proceso de la transacción.
	Public Function insPostCOL742(ByVal nInsurArea As Integer, ByVal dCollectdate As Date, ByVal nAgreement As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecAgree As eRemoteDB.Execute
		
		On Error GoTo insPostCOL742_Err
		
		lrecAgree = New eRemoteDB.Execute
		
		With lrecAgree
			.StoredProcedure = "insProcess_COL742"
			.Parameters.Add("nInsur_area", nInsurArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectdate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostCOL742 = .Run(False)
		End With
		
		
insPostCOL742_Err: 
		If Err.Number Then
			insPostCOL742 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecAgree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecAgree = Nothing
	End Function
	
	'%insPostCOL704: Esta función se encarga de realizar el proceso de la transacción.
	Public Function insPostCOL704(ByVal anWayPay As Integer, ByVal anBank As Double, ByVal anUserCode As Integer, ByVal sKey As String, ByVal nAgreement As Double, ByVal dPayDate As Date, ByVal nInsur_area As Double) As Boolean
		Dim lrecAgree As eRemoteDB.Execute
		
		On Error GoTo insPostCOL704_Err
		
		lrecAgree = New eRemoteDB.Execute
		
		With lrecAgree
			.StoredProcedure = "insCOL704pkg.inscol704"
			.Parameters.Add("nWayPay", anWayPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", anBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", anUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayDate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostCOL704 = .Run(False)
		End With
		
insPostCOL704_Err: 
		If Err.Number Then
			insPostCOL704 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecAgree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecAgree = Nothing
	End Function
	
	Public Function loadTmp_Reject_bank(ByVal anInsurArea As Integer, ByVal anWayPay As Integer, ByVal anBank As Double, ByVal adPayDate As Date, ByVal asFileName As String, ByVal sKey As String) As Integer
		Dim lrecTmp_Reject_bank As eRemoteDB.Execute
		
		Dim lblnOk As Boolean
		Dim lstrInputData As String
		Dim llngPolicy As Integer
		Dim llngError As Integer
		
		Dim lstrSeparate As String
		Dim lstrRegister As String
		Dim lintInsert As Short
        Dim lstrArray(,) As String
		Dim lintColumn As Short
		Dim lintRow As Short
		Dim lintMaxRow As Short
		Dim lintFileNum As Short
		Dim lintRow_end As Short
		Dim lintCountReg As Short
		On Error GoTo loadTmp_Reject_bank_err
		
		lrecTmp_Reject_bank = New eRemoteDB.Execute
		
		llngError = 0
		lstrSeparate = "|"
		loadTmp_Reject_bank = 1
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Len(Dir(asFileName, FileAttribute.Archive)) > 0 Then
			
			On Error Resume Next
			lintFileNum = FreeFile
			FileOpen(lintFileNum, asFileName, OpenMode.Input)
			
			If Err.Number Then
				FileClose(lintFileNum)
				FileOpen(lintFileNum, asFileName, OpenMode.Input)
			End If
			
			'+  Read line Head.
			lstrInputData = LineInput(lintFileNum)
			
			'+ Check for end of file.
			lintRow = 0
			lstrRegister = String.Empty
			
			'+Se redefine matriz al bloque máximo
			
			lintMaxRow = 100
			ReDim lstrArray(10, lintMaxRow)
			
			Do While Not EOF(lintFileNum)
				'+      Read line of data.
				If lintRow <> 0 Then
					lstrInputData = LineInput(lintFileNum)
				End If
				
				If anWayPay = 2 Then
					lstrArray(1, lintRow) = ""
				Else
					lstrArray(1, lintRow) = Mid(lstrInputData, 1, 3)
				End If
				
				If anWayPay = 2 Then
					lstrArray(2, lintRow) = ""
				Else
					lstrArray(2, lintRow) = Mid(lstrInputData, 4, 30)
				End If
				
				If anWayPay = 2 Then
					lstrArray(3, lintRow) = ""
				Else
					lstrArray(3, lintRow) = Mid(lstrInputData, 34, 17)
				End If
				
				'+      Monto
				
				If anWayPay = 2 Then
					lstrArray(4, lintRow) = Mid(lstrInputData, 9, 9)
				Else
					lstrArray(4, lintRow) = Mid(lstrInputData, 51, 10)
				End If
				
				If anWayPay = 2 Then
					lstrArray(5, lintRow) = ""
				Else
					lstrArray(5, lintRow) = Mid(lstrInputData, 61, 15)
				End If
				
				'+      Numero de poliza.
				
				If anWayPay = 2 Then
					lstrArray(6, lintRow) = Mid(lstrInputData, 1, 8)
				Else
					lstrArray(6, lintRow) = Mid(lstrInputData, 76, 20)
				End If
				
				'+      Codigo del rechazo.
				
				If anWayPay = 2 Then
					lstrArray(7, lintRow) = Trim(Mid(lstrInputData, 18, 3))
				Else
					lstrArray(7, lintRow) = Mid(lstrInputData, 96, 2)
				End If
				
				If anWayPay = 2 Then
					lstrArray(8, lintRow) = ""
				Else
					lstrArray(8, lintRow) = Mid(lstrInputData, 98, 20)
				End If
				
				If anWayPay = 2 Then
					lstrArray(9, lintRow) = ""
				Else
					lstrArray(9, lintRow) = Mid(lstrInputData, 118, 8)
				End If
				
				If anWayPay = 2 Then
					lstrArray(10, lintRow) = ""
				Else
					lstrArray(10, lintRow) = Mid(lstrInputData, 126, 8)
				End If
				
				lintRow = lintRow + 1
				If lintMaxRow = lintRow Then
					lintMaxRow = lintMaxRow + 100
					ReDim Preserve lstrArray(10, lintMaxRow)
				End If
				
			Loop 
			
			'+  Close file.
			FileClose(lintFileNum)
			
			lintCountReg = 0
			lintRow_end = lintRow - 1
			
			For lintRow = 0 To lintRow_end
				lstrRegister = lstrRegister & lstrArray(1, lintRow) & lstrSeparate & lstrArray(2, lintRow) & lstrSeparate & lstrArray(3, lintRow) & lstrSeparate & lstrArray(4, lintRow) & lstrSeparate & lstrArray(5, lintRow) & lstrSeparate & lstrArray(6, lintRow) & lstrSeparate & lstrArray(7, lintRow) & lstrSeparate & lstrArray(8, lintRow) & lstrSeparate & lstrArray(9, lintRow) & lstrSeparate & lstrArray(10, lintRow) & "&&"
				lintCountReg = lintCountReg + 1
				If lintCountReg = 200 Then
					lintCountReg = 0
					
					If Not AddTmp_Reject_bank(anInsurArea, anWayPay, anBank, adPayDate, sKey, lstrRegister) Then
						loadTmp_Reject_bank = 0
					End If
					
					lstrRegister = String.Empty
				End If
			Next 
			
			If Trim(lstrRegister) <> String.Empty Then
				If Not AddTmp_Reject_bank(anInsurArea, anWayPay, anBank, adPayDate, sKey, lstrRegister) Then
					loadTmp_Reject_bank = 0
				End If
			End If
		End If
		
loadTmp_Reject_bank_err: 
		If Err.Number Then
			loadTmp_Reject_bank = 0
		End If
		On Error GoTo 0
	End Function
	
	'%**insUser_Schema: He verifies if the user has the permissions to work
	'%**               with the information of the zone
	'%  insUser_Schema: Verifica si el usuario tiene los permisos para trabajar
	'%                 con la información de la zona
	Public Function insUser_Schema(ByVal nUsercode As Integer, ByVal nOffice As Integer) As Boolean
		Dim lvalOffice_schema As eRemoteDB.Execute
		
		lvalOffice_schema = New eRemoteDB.Execute
		
		On Error GoTo insExistClaim_Err
		
		insUser_Schema = False
		
		With lvalOffice_schema
			.StoredProcedure = "valOffice_schema"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nValidOffice") = 1 Then
					insUser_Schema = True
				End If
			End If
			
			.RCloseRec()
		End With
		
insExistClaim_Err: 
		If Err.Number Then
			insUser_Schema = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lvalOffice_schema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalOffice_schema = Nothing
	End Function
	
	'%insValCOL003: Esta función se encarga de validar los datos introducidos en la página COL003.
	Public Function insValCOL003(ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal nZone As Integer, ByVal nIntermed As Double, ByVal nSupervis As Double, ByVal nCodAgree As Double, ByVal nWayPay As Integer, ByVal nStatusPre As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsAgents As Object
		Dim lclsAgreement As Agreement
		
		lobjErrors = New eFunctions.Errors
		lclsAgents = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Agents")
		
		On Error GoTo insValCOL003_Err
		
		With lobjErrors
			'+      Validación del Intermediario
			If (nIntermed <> eRemoteDB.Constants.intNull) And (nSupervis = eRemoteDB.Constants.intNull) Then
				If Not lclsAgents.findIntermediaClient(nIntermed, Dir_debit.Interm_typ.clngProducer, Today) Then
					Call .ErrorMessage(sCodispl, 750021)
				End If
			End If
			
			'+      Validación del Supervisor
			If (nSupervis <> eRemoteDB.Constants.intNull) And (nIntermed = eRemoteDB.Constants.intNull) Then
				If Not lclsAgents.findIntermediaClient(nSupervis, Dir_debit.Interm_typ.clngOrganizer, Today) Then
					Call .ErrorMessage(sCodispl, 750022)
				End If
			End If
			
			'+      Validación del Estado
			If (nStatusPre = eRemoteDB.Constants.intNull) Then
				Call .ErrorMessage(sCodispl, 1922)
			End If
			
			'+      Validación del Convenio.
			'+      Se realiza si vía de pago es Planilla.
			
			If nWayPay = 3 Then
				
				If (nCodAgree <> eRemoteDB.Constants.intNull) Then
					lclsAgreement = New Agreement
					If Not lclsAgreement.Find(nCodAgree) Then
						Call .ErrorMessage(sCodispl, 9999)
					End If
					'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsAgreement = Nothing
				End If
			End If
			insValCOL003 = .Confirm
		End With
		
		
insValCOL003_Err: 
		If Err.Number Then
			insValCOL003 = "insValCOL003: " & insValCOL003 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsAgents may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgents = Nothing
	End Function
	
	'%insValCOL009: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL009(ByVal sCodispl As String, ByVal dProcessDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		
		On Error GoTo insValCOL009_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Validacion del campo "Fecha de proceso"
			If dProcessDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5072)
			Else
				lclsCtrol_Date = New eGeneral.Ctrol_date
				If Not lclsCtrol_Date.InsValdLedgerdat(1, dProcessDate) Then
					.ErrorMessage(sCodispl, 1006)
				End If
				If dProcessDate < Today Then
					.ErrorMessage(sCodispl, 55860)
				End If
			End If
			
			insValCOL009 = .Confirm
		End With
		
insValCOL009_Err: 
		If Err.Number Then
			insValCOL009 = "insValCOL009: " & insValCOL009 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_Date = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValCOL011: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL011(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCOL011_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Validacion del campo "Fecha de proceso"
			If dInitDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3237)
				lblnError = True
			Else
				If dEndDate = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 3239)
					lblnError = True
				Else
					If dInitDate < dEndDate Then
						.ErrorMessage(sCodispl, 12120)
						lblnError = True
					End If
				End If
			End If
			
			insValCOL011 = .Confirm
		End With
		
insValCOL011_Err: 
		If Err.Number Then
			insValCOL011 = "InsValCOL011: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%FormatData: Esta función se encarga de dar formato a los datos a enviar a archivos de texto.
	Private Function FormatData(ByVal sValue As Object, ByVal sChar As String, ByVal nposition As Integer, Optional ByVal sTrunc As String = "Right", Optional ByVal sAlign As String = "Right") As String
		Dim nLength As Integer
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sValue) Then
			sValue = Trim(sValue)
			nLength = Len(sValue)
			If nLength > nposition Then
				If sTrunc = "Right" Then
					FormatData = Right(sValue, nposition)
				Else
					FormatData = Left(sValue, nposition)
				End If
			Else
				If sAlign = "Right" Then
					FormatData = New String(sChar, nposition - nLength) & sValue
				Else
					FormatData = sValue & New String(sChar, nposition - nLength)
				End If
			End If
		Else
			FormatData = New String(sChar, nposition)
		End If
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mclsError = New eFunctions.Errors
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsError = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%AddTmp_Reject_bank: Llamado al procedure que incluye los registros en el temporal de la col704.
	Private Function AddTmp_Reject_bank(ByVal nInsurArea As Integer, ByVal nWayPay As Integer, ByVal nBank As Double, ByVal dPayDate As Date, ByVal sKey As String, ByVal sValue As String) As Boolean
		Dim lreccreTmp_Reject_bank As eRemoteDB.Execute
		
		On Error GoTo AddTmp_Reject_bank_err
		
		lreccreTmp_Reject_bank = New eRemoteDB.Execute
		
		
		With lreccreTmp_Reject_bank
			.StoredProcedure = "creTmp_Reject_bank"
			.Parameters.Add("nInsurArea", nInsurArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayDate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWayPay", nWayPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32767, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddTmp_Reject_bank = .Run(False)
		End With
		
AddTmp_Reject_bank_err: 
		If Err.Number Then
			AddTmp_Reject_bank = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTmp_Reject_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTmp_Reject_bank = Nothing
		
	End Function
	
	'%insPostCOL009: Realiza las actualizaciones correspondientes a la anulación automática.
	Public Function insPostCOL009(ByVal nTypeProce As Integer, ByVal dProcessDate As Date, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insPostCOL009_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "insAutomAnul1"
			.Parameters.Add("nTypeProce", nTypeProce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDateProcec", dProcessDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_Aux", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrKey = .Parameters("sKey_Aux").Value
				insPostCOL009 = True
			Else
				insPostCOL009 = False
			End If
		End With
		
insPostCOL009_Err: 
		If Err.Number Then
			insPostCOL009 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%insPostCOL556: Esta función se encarga de realizar la conciliación de primas recaudadas.
	Public Function insPostCOL556(ByVal sKey As String, ByVal nInsur_area As Integer, ByVal nProcessTyp As Short, ByVal dOperdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo insPostCOL556_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "insUpdCol556"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProcessTyp", nProcessTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperDate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCOL556 = .Run(False)
		End With
		
		If insPostCOL556 Then
			insPostCOL556 = insGenFilesCOL556(sKey)
		End If
		
insPostCOL556_Err: 
		If Err.Number Then
			insPostCOL556 = False
		End If
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%valDataGenCOL500:Valida si se generaron datos en la transaccion COL500
	'                  Realiza busqueda en tablas TMP_COLLECTION y TMP_BATCH_ERR
	Public Function valDataGenCOL500(ByVal sKey As String) As Boolean
		Dim lrecvalDatagencol500 As eRemoteDB.Execute
		On Error GoTo valDatagencol500_Err
		
		lrecvalDatagencol500 = New eRemoteDB.Execute
		
		Me.sProcess = "0"
		Me.sNoProcess = "0"
		Me.sAgreeApvSef = "000"
		
		'+
		'+ Definición de store procedure valDatagencol500 al 12-11-2003 13:44:48
		'+
		With lrecvalDatagencol500
			.StoredProcedure = "valDataGenCOL500"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNoprocess", sNoProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgreeApvSef", sAgreeApvSef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valDataGenCOL500 = .Run(False)
			If valDataGenCOL500 Then
				Me.sProcess = .Parameters("sProcess").Value
				Me.sNoProcess = .Parameters("sNoprocess").Value
				Me.sAgreeApvSef = .Parameters("sAgreeApvSef").Value
			End If
		End With
		
valDatagencol500_Err: 
		If Err.Number Then
			valDataGenCOL500 = False
		End If
		'UPGRADE_NOTE: Object lrecvalDatagencol500 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDatagencol500 = Nothing
		On Error GoTo 0
	End Function
	
	'%insGenFilesCOL556: Crea los archivos del proceso COL556
	Public Function insGenFilesCOL556(ByVal sKey As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lobjGeneral As eGeneral.GeneralFunction
		Dim lobjClient As eClient.Client
		Dim lobjCompany As eGeneral.Company
		
		Dim llngRecCounter As Integer
		Dim ljdblAmountTot As Double
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrCompany As Object
		
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNameCityDet As String
		Dim FileNum As Integer
		
		Dim ldblAmountPre As Double
		Dim ldblExchangePre As Double
		Dim ldblAmountmov As Double
		Dim ldblExchangemov As Double
		
		insGenFilesCOL556 = True
		
		lrecTime = New eRemoteDB.Execute
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		With lrecTime
			.StoredProcedure = "ReaCol556"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		If lrecTime.Run() Then
			FileName = lstrLoadFile & "COL556_Rec" & sKey & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".xls"
			FileNum = FreeFile
			FileOpen(FileNum, FileName, OpenMode.Output)
			PrintLine(FileNum, "")
			lstrWritTxt = "Conciliación de Primas Recaudadas" & Chr(9) & Chr(9) & Chr(9) & Chr(9) & "Fecha de Ejecución" & Chr(9) & Today
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			lstrWritTxt = "Fecha Proceso" & Chr(9) & lrecTime.FieldToClass("dOperDate") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Area de Seguro" & Chr(9) & lrecTime.FieldToClass("sInsur_Area") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Tipo de Ejecución" & Chr(9) & IIf(lrecTime.FieldToClass("nProcessTyp") = 1, "Preliminar", "Definitiva") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Informe de Cobros" & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			PrintLine(FileNum, "")
			lstrWritTxt = ""
			lstrWritTxt = lstrWritTxt & "Ramo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Producto" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número de Propuesta" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número de Póliza" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Certificado" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Rut Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Nombre Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Relación" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Recibo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Contrato" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Cuota" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Moneda Origen  del Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Moneda Origen del Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Factor de Cambio Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Documento en Pesos" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Moneda Origen  Movimiento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Recaudado M.O. (Moneda Origen)" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Factor de Cambio Movimiento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Recaudado en Pesos" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Fecha Recaudación (Fecha de ingreso a caja)" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Diferencia" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Observación" & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			
			Do While Not lrecTime.EOF
				lstrWritTxt = ""
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sBranch") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sProduct") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nProponum") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nProponum")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nPolicy") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nPolicy")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("nCertif") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sClient") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCliename") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("nBordereaux") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nReceipt") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nReceipt")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nContrat") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nContrat")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nDraft") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nDraft")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCurrencypre") & Chr(9)
				ldblAmountPre = IIf(FormatNumber(lrecTime.FieldToClass("nAmountpre"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nAmountpre"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & ldblAmountPre & Chr(9)
				ldblExchangePre = IIf(FormatNumber(lrecTime.FieldToClass("nExchangepre"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nExchangepre"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblExchangePre) & " " & Chr(9)
				lstrWritTxt = lstrWritTxt & CStr(FormatNumber(ldblAmountPre * ldblExchangePre, 6,  ,  , TriState.True)) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCurrencymov") & Chr(9)
				ldblAmountmov = IIf(FormatNumber(lrecTime.FieldToClass("nAmountmov"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nAmountmov"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblAmountmov) & Chr(9)
				ldblExchangemov = IIf(FormatNumber(lrecTime.FieldToClass("nExchangemov"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nExchangemov"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblExchangemov) & Chr(9)
				lstrWritTxt = lstrWritTxt & CStr(FormatNumber(ldblAmountmov * ldblExchangemov, 6,  ,  , TriState.True)) & " " & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("dDaterec") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nAmountDif") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nAmountDif")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sError") & Chr(9)
				PrintLine(FileNum, lstrWritTxt)
				lrecTime.RNext()
			Loop 
			FileClose(FileNum)
			'+Se retorna el nombre de archivo generado
			If FileName <> String.Empty Then
				sFileName = lstrDirFile & Right(FileName, Len(FileName) - Len(lstrLoadFile))
			Else
				sFileName = String.Empty
			End If
		End If
		
		With lrecTime
			.StoredProcedure = "ReaCol556"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		If lrecTime.Run() Then
			FileName = lstrLoadFile & "COL556_Inc" & sKey & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".xls"
			FileNum = FreeFile
			FileOpen(FileNum, FileName, OpenMode.Output)
			PrintLine(FileNum, "")
			lstrWritTxt = "Conciliación de Primas Recaudadas" & Chr(9) & Chr(9) & Chr(9) & Chr(9) & "Fecha de Ejecución" & Chr(9) & Today
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			lstrWritTxt = "Fecha Proceso" & Chr(9) & lrecTime.FieldToClass("dOperDate") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Area de Seguro" & Chr(9) & lrecTime.FieldToClass("sInsur_Area") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Tipo de Ejecución" & Chr(9) & IIf(lrecTime.FieldToClass("nProcessTyp") = 1, "Preliminar", "Definitiva") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Informe de Incidencias" & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			PrintLine(FileNum, "")
			lstrWritTxt = ""
			lstrWritTxt = lstrWritTxt & "Ramo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Producto" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número de Propuesta" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número de Póliza" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Certificado" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Rut Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Nombre Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Relación" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Recibo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Contrato" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Cuota" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Moneda Origen  del Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Moneda Origen del Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Factor de Cambio Documento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Documento en Pesos" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Moneda Origen  Movimiento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Recaudado M.O. (Moneda Origen)" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Factor de Cambio Movimiento" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Recaudado en Pesos" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Fecha Recaudación (Fecha de ingreso a caja)" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Motivo de Rechazo" & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			Do While Not lrecTime.EOF
				lstrWritTxt = ""
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sBranch") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sProduct") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nProponum") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nProponum")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nPolicy") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nPolicy")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("nCertif") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sClient") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCliename") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("nBordereaux") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nReceipt") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nReceipt")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nContrat") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nContrat")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nDraft") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nDraft")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCurrencypre") & Chr(9)
				ldblAmountPre = IIf(FormatNumber(lrecTime.FieldToClass("nAmountpre"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nAmountpre"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & ldblAmountPre & Chr(9)
				ldblExchangePre = IIf(FormatNumber(lrecTime.FieldToClass("nExchangepre"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nExchangepre"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblExchangePre) & " " & Chr(9)
				lstrWritTxt = lstrWritTxt & CStr(FormatNumber(ldblAmountPre * ldblExchangePre, 6,  ,  , TriState.True)) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sCurrencymov") & Chr(9)
				ldblAmountmov = IIf(FormatNumber(lrecTime.FieldToClass("nAmountmov"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nAmountmov"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblAmountmov) & Chr(9)
				ldblExchangemov = IIf(FormatNumber(lrecTime.FieldToClass("nExchangemov"), 6,  ,  , TriState.True) = CStr(eRemoteDB.Constants.intNull), 0, FormatNumber(lrecTime.FieldToClass("nExchangemov"), 6,  ,  , TriState.True))
				lstrWritTxt = lstrWritTxt & CStr(ldblExchangemov) & Chr(9)
				lstrWritTxt = lstrWritTxt & CStr(FormatNumber(ldblAmountmov * ldblExchangemov, 6,  ,  , TriState.True)) & " " & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("dDaterec") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sError") & Chr(9)
				PrintLine(FileNum, lstrWritTxt)
				lrecTime.RNext()
			Loop 
			FileClose(FileNum)
			
			'+Se retorna el nombre de archivo generado
			If FileName <> String.Empty Then
				sFileName1 = lstrDirFile & Right(FileName, Len(FileName) - Len(lstrLoadFile))
			Else
				sFileName1 = String.Empty
			End If
		End If
		
insGenFilesCOL556_Err: 
		If Err.Number Then
			insGenFilesCOL556 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCompany = Nothing
		'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClient = Nothing
	End Function
	
	
	'%insGenFilesCOL502: Crea los archivos del proceso COL502
	Public Function insGenFilesCOL502(ByVal sKey As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lobjGeneral As eGeneral.GeneralFunction
		Dim lobjClient As eClient.Client
		Dim lobjCompany As eGeneral.Company
		
		Dim llngRecCounter As Integer
		Dim ljdblAmountTot As Double
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrCompany As Object
		
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNameCityDet As String
		Dim FileNum As Integer
		
		Dim ldblAmountPre As Double
		Dim ldblExchangePre As Double
		Dim ldblAmountmov As Double
		Dim ldblExchangemov As Double
		
		insGenFilesCOL502 = True
		
		lrecTime = New eRemoteDB.Execute
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		With lrecTime
			.StoredProcedure = "ReaCol502"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRej_Exe", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		If lrecTime.Run() Then
			FileName = lstrLoadFile & "COL502_Rec" & sKey & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".xls"
			FileNum = FreeFile
			FileOpen(FileNum, FileName, OpenMode.Output)
			PrintLine(FileNum, "")
			lstrWritTxt = "IMPUTACIONES DE PAC/TRANSBANK RECHAZADAS" & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Today & Chr(9) & Chr(9) & Chr(9) & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Area de Seguros" & " " & lrecTime.FieldToClass("Desc_InsurArea") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			PrintLine(FileNum, "")
			PrintLine(FileNum, "")
			lstrWritTxt = "Recaudación del " & Chr(9) & lrecTime.FieldToClass("dCollectDate") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			lstrWritTxt = "Total de Pesos a ser Imputados" & Chr(9) & IIf(lrecTime.FieldToClass("nAmount_Imp") = eRemoteDB.Constants.intNull, 0, lrecTime.FieldToClass("nAmount_Imp")) & Chr(9) & Chr(9) & "Total de Pólizas a ser Imputadas" & Chr(9) & lrecTime.FieldToClass("ncont") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Total de Pesos Imputados" & Chr(9) & IIf(lrecTime.FieldToClass("nAmo_Imp") = eRemoteDB.Constants.intNull, 0, lrecTime.FieldToClass("nAmo_Imp")) & Chr(9) & Chr(9) & "Total de Pólizas Imputadas" & Chr(9) & lrecTime.FieldToClass("nCont_Imp") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Total de Pesos No Imputados" & Chr(9) & IIf(lrecTime.FieldToClass("nAmo_NoImp") = eRemoteDB.Constants.intNull, 0, lrecTime.FieldToClass("nAmo_NoImp")) & Chr(9) & Chr(9) & "Total de Pólizas Sin Imputar" & Chr(9) & lrecTime.FieldToClass("nCont_NoImp") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Sobrante de Prima en Pesos" & Chr(9) & IIf(lrecTime.FieldToClass("nPrem_Sob") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nPrem_Sob")) & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Número de Relación a Imputar" & Chr(9) & IIf(lrecTime.FieldToClass("nbordereaux") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nbordereaux")) & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			lstrWritTxt = "Fecha de Ejecución" & Chr(9) & lrecTime.FieldToClass("dCompDate") & Chr(9) & "Código del Proceso Generado" & Chr(9) & Chr(9) & lrecTime.FieldToClass("sKey") & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			PrintLine(FileNum, "")
			PrintLine(FileNum, "")
			lstrWritTxt = ""
			lstrWritTxt = lstrWritTxt & "Banco" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Póliza" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Rut Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Moneda" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Recibo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Contrato" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Cuota" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Boletín" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto Origen" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Monto $" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Nombre Cliente" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Ramo" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Producto" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Certificado" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Tipo Cuenta" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Número Cuenta" & Chr(9)
			lstrWritTxt = lstrWritTxt & "Causa Rechazo" & Chr(9)
			PrintLine(FileNum, lstrWritTxt)
			
			Do While Not lrecTime.EOF
				lstrWritTxt = ""
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Desc_Bank") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("npolicy") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("npolicy")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sClient") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Desc_Currency") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nreceipt") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nreceipt")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("ncontrat") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("ncontrat")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("ndraft") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("ndraft")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("nbulletins") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nbulletins")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("namount") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("namount")) & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("namount_loc") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("namount_loc")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("scliename") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("desc_branch") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sProduct") & Chr(9)
				lstrWritTxt = lstrWritTxt & IIf(lrecTime.FieldToClass("ncertificat") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("ncertificat")) & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("desc_acctype") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("sAcc_Number") & Chr(9)
				lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("scause_reject") & Chr(9)
				PrintLine(FileNum, lstrWritTxt)
				lrecTime.RNext()
			Loop 
			FileClose(FileNum)
			'+Se retorna el nombre de archivo generado
			If FileName <> String.Empty Then
				sFileName = lstrDirFile & Right(FileName, Len(FileName) - Len(lstrLoadFile))
			Else
				sFileName = String.Empty
				insGenFilesCOL502 = False
			End If
		Else
			insGenFilesCOL502 = False
		End If
		
insGenFilesCOL502_Err: 
		If Err.Number Then
			insGenFilesCOL502 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCompany = Nothing
		'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClient = Nothing
	End Function
	
	'%valDataGenCOL502:Valida si se generaron datos en la transaccion COL502
	'                  Realiza busqueda en tablas TMP_IMPCOL502 y TMP_BATCH_ERR
	Public Function valDataGenCOL502(ByVal sKey As String) As Boolean
		Dim lrecvalDatagencol502 As eRemoteDB.Execute
		On Error GoTo valDatagencol502_Err
		
		lrecvalDatagencol502 = New eRemoteDB.Execute
		
		Me.sProcess = "0"
		Me.sNoProcess = "0"
		
		'+
		'+ Definición de store procedure valDatagencol502
		'+
		With lrecvalDatagencol502
			.StoredProcedure = "valDataGenCOL502"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNoprocess", sNoProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valDataGenCOL502 = .Run(False)
			If valDataGenCOL502 Then
				Me.sProcess = .Parameters("sProcess").Value
				Me.sNoProcess = .Parameters("sNoprocess").Value
			End If
		End With
		
valDatagencol502_Err: 
		If Err.Number Then
			valDataGenCOL502 = False
		End If
		'UPGRADE_NOTE: Object lrecvalDatagencol502 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDatagencol502 = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCOL002: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL836(ByVal sCodispl As String, ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal nWay_Pay As Short, ByVal nBank As Short, ByVal nBranch As Short, ByVal Agency As Short) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL836_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			If dDateIni = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5072)
			End If
			
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1097)
			End If
			
			If dDateIni > dDateEnd Then
				.ErrorMessage(sCodispl, 11425)
			End If
			
			insValCOL836 = .Confirm
		End With
		
insValCOL836_Err: 
		If Err.Number Then
			insValCOL836 = "InsValCOL836: " & insValCOL836 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	'%insValCOL889_K: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCOL889_K(ByVal sCodispl As String, ByVal dDateIni As Date, ByVal dDateEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCOL889_K_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			If dDateIni = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 9071)
			End If
			
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 9072)
			End If
			
			If dDateIni > dDateEnd Then
				.ErrorMessage(sCodispl, 60113)
			End If
			
			insValCOL889_K = .Confirm
		End With
		
insValCOL889_K_Err: 
		If Err.Number Then
			insValCOL889_K = "InsValCOL889_K: " & insValCOL889_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% InsVerifyAmount_Disp :  Verifica si el titular del convenio tiene dinero disponible para
	' realizar el pago.
	Public Function InsVerifyAmount_Disp(ByVal nInsur_area As Double, ByVal dCollectdate As Date, ByVal nCod_Agree As Double) As Boolean
		Dim lrecInsVerifyAmount_Disp As eRemoteDB.Execute
		
		On Error GoTo InsVerifyAmount_Disp_err
		lrecInsVerifyAmount_Disp = New eRemoteDB.Execute
		
		With lrecInsVerifyAmount_Disp
			.StoredProcedure = "InsVerifyAmount_Disp"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectdate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance_Cli", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Pay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsVerifyAmount_Disp = (.Parameters("nExists").Value = 1)
                nBalance_Cli = CDbl(.Parameters("nBalance_Cli").Value)
                nAmount_Pay = CDbl(.Parameters("nAmount_Pay").Value)
			Else
				InsVerifyAmount_Disp = False
				nBalance_Cli = 0
				nAmount_Pay = 0
			End If
		End With
		
InsVerifyAmount_Disp_err: 
		If Err.Number Then
			InsVerifyAmount_Disp = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsVerifyAmount_Disp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsVerifyAmount_Disp = Nothing
	End Function
End Class






