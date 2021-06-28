Option Strict Off
Option Explicit On
Public Class Intermedia
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermedia.cls                           $%'
	'% $Author:: Nvaplat9                                   $%'
	'% $Date:: 18/10/04 9:38a                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	
	Private nYear As Integer
	Private nMonth As Integer
	Public sind_process As String
	
	'%insPostAGL772:Ejecuta proceso final de transaccion
	Public Function insPostAGL772(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInterTyp As Integer, ByVal nIntermed As Integer, ByVal dDateFrom As Date, ByVal dDateTo As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsAgl772 As eRemoteDB.Execute
		
		On Error GoTo insAgl772_Err
		
		lrecinsAgl772 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insAgl772 al 09-08-2002 17:23:15
		'+
		With lrecinsAgl772
			.StoredProcedure = "insAgl772"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateto", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostAGL772 = .Run(False)
		End With
		
insAgl772_Err: 
		If Err.Number Then
			insPostAGL772 = False
		End If
		'UPGRADE_NOTE: Object lrecinsAgl772 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsAgl772 = Nothing
		On Error GoTo 0
	End Function
	
	'%insValAGL772: Validaciones de la transaccion de Interfaz de anticipos generados
	Public Function insValAGL772(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dDateFrom As Date, ByVal dDateTo As Date) As String
		Dim lclsError As eFunctions.Errors
		
		
		Dim lblnValidDate As Boolean
		On Error GoTo insValAGL772_Err
		
		lclsError = New eFunctions.Errors
		
		With lclsError
			'+ Validación del area de seguro
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'        If nProduct = NumNull Then
			'            Call .ErrorMessage(sCodispl, 1014)
			'        End If
			
			lblnValidDate = True
			If dDateFrom = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 9071)
				lblnValidDate = False
			End If
			
			If dDateTo = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 9072)
				lblnValidDate = False
			End If
			
			If lblnValidDate Then
				If dDateFrom > dDateTo Then
					Call .ErrorMessage(sCodispl, 736026)
				End If
				If dDateTo > Today Then
					Call .ErrorMessage(sCodispl, 700005,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha final:")
				End If
			End If
			
			insValAGL772 = .Confirm
			
		End With
		
insValAGL772_Err: 
		If Err.Number Then
			insValAGL772 = "insValAGL772: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
	End Function
	
	'% insValLiq_Comm: se realizan las validaciones correspondientes a la transacción
	Public Function insValLiq_Comm(ByVal sCodispl As String, ByVal nInsur_Area As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInterTyp As Integer, ByVal dEffecdate As Date, ByVal dExpirdat As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjProduct As eProduct.Product
		Dim lblnValid As Boolean
		
		On Error GoTo insValLiq_Comm_K_err
		
		lobjErrors = New eFunctions.Errors
		lobjProduct = New eProduct.Product
		
		lblnValid = True
		
		With lobjErrors
			
			'+ Validación campo: Ramo.
			If nInsur_Area = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55031)
			End If
			
			'+ Validación campo: Ramo.
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
				lblnValid = False
			End If
			
			'+ Validación del campo: Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1014)
				lblnValid = False
			End If
			
			'+ Validación del campo: Tipo de intermediario
			If nInterTyp = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10095)
			End If
			
			'+ Validación del campo: Fecha inicio
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 9071)
				lblnValid = False
			End If
			
			'+ Validación del campo: Fecha fin
			If dExpirdat = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 9072)
				lblnValid = False
			Else
				'+ Validación del campo: Fecha fin mayor a dia
				If dExpirdat <> eRemoteDB.Constants.dtmNull And dExpirdat > Today Then
					Call .ErrorMessage(sCodispl, 700005,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha final:")
				End If
			End If
			
			'+ Validación del campo: Fecha inicio no puede ser mayor a fecha fin
			If dEffecdate <> eRemoteDB.Constants.dtmNull And dExpirdat <> eRemoteDB.Constants.dtmNull And dEffecdate >= dExpirdat Then
				Call .ErrorMessage(sCodispl, 736026)
			End If
			
			'+ Valida el tipo de producto
			If lblnValid Then
				Call lobjProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
				If lobjProduct.nProdClas <> 9 And lobjProduct.nProdClas <> 10 Then
					Call .ErrorMessage(sCodispl, 55906)
				End If
			End If
			
			insValLiq_Comm = .Confirm
		End With
		
insValLiq_Comm_K_err: 
		If Err.Number Then
			insValLiq_Comm = "insValLiq_Comm: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProduct = Nothing
	End Function
	
	'%insPostLiq_Comm. Realiza el proceso de transferencia de datos del intermediario
	Public Function insPostLiq_Comm(ByVal nInsur_Area As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInterTyp As Integer, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nUsercode As Integer, ByVal nType_proce As Integer) As Boolean
		Dim lrecinsPostLiq_Comm As eRemoteDB.Execute
		
		On Error GoTo insPostLiq_Comm_Err
		
		lrecinsPostLiq_Comm = New eRemoteDB.Execute
		
		With lrecinsPostLiq_Comm
			.StoredProcedure = "insUpdLiq_comm"
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sind_process", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sind_process = .Parameters("sind_process").Value
				If sind_process = "1" Then
					insPostLiq_Comm = True
				Else
					insPostLiq_Comm = False
				End If
			Else
				insPostLiq_Comm = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostLiq_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostLiq_Comm = Nothing
		
insPostLiq_Comm_Err: 
		If Err.Number Then
			insPostLiq_Comm = False
		End If
		On Error GoTo 0
	End Function
	'% GetEffecDate_APV: Retorna la última fecha de efecto registrada
	'%                   un ramo-producto específico [APV2] - JUP - 14/10/2004
	Public Function GeteEffecDate_APV(ByVal nBranch As Integer, ByVal nProduct As Integer) As Date
		
		Dim lrecreaEffecDate_ctroldate_apv As eRemoteDB.Execute
		
		On Error GoTo GetEffecDate_APV_err
		
		lrecreaEffecDate_ctroldate_apv = New eRemoteDB.Execute
		'+ Definición de parámetros para stored procedure 'realast_date_ctroldate_apv'
		'+ Información leída el: 04/09/2003
		
		With lrecreaEffecDate_ctroldate_apv
			.StoredProcedure = "reaEffecDate_ctroldate_apv"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastEffec_Date", Nothing, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				GeteEffecDate_APV = .Parameters("dLastEffec_Date").Value
			Else
				GeteEffecDate_APV = CDate(Nothing)
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaEffecDate_ctroldate_apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaEffecDate_ctroldate_apv = Nothing
		
GetEffecDate_APV_err: 
		If Err.Number Then
			GeteEffecDate_APV = CDate(Nothing)
			'UPGRADE_NOTE: Object lrecreaEffecDate_ctroldate_apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaEffecDate_ctroldate_apv = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'%insValAGL771: Realiza la validación de la transferencia de datos del intermediario
	Public Function insValAGL771(ByVal nAction As Integer, ByVal nInterm_typ As Integer, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValAGL771_Err
		
		lerrTime = New eFunctions.Errors
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If nInterm_typ = 0 Or nInterm_typ = eRemoteDB.Constants.intNull Then
				lerrTime.ErrorMessage("AGL771", 10095)
			End If
			If dInitDate = eRemoteDB.Constants.dtmNull Then
				lerrTime.ErrorMessage("AGL771", 9071)
			End If
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				lerrTime.ErrorMessage("AGL771", 9072)
			End If
			If dInitDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull Then
				If dInitDate > dEndDate Then
					lerrTime.ErrorMessage("AGL771", 736026)
				End If
				If dEndDate > Today Then
					lerrTime.ErrorMessage("AGL771", 700005,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha final: ")
				End If
				
			End If
		End If
		
		insValAGL771 = lerrTime.Confirm
		
insValAGL771_Err: 
		If Err.Number Then
			insValAGL771 = insValAGL771 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
	End Function
	
	'%insPostAGL771. Realiza el proceso de transferencia de datos del intermediario
	Public Function insPostAGL771(ByVal nInterm_typ As Integer, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostAGL771 As eRemoteDB.Execute
		Dim FileName As String
		Dim FileNameCityDet As String
		Dim FileNum As Integer
		Dim lstrWritTxt As String
		Dim lstrLoadFile As Object
		Dim lstrDirFile As Object
		Dim sInt_status As Object
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		On Error GoTo insPostAGL771_Err
		
		lrecinsPostAGL771 = New eRemoteDB.Execute
		
		Dim lclsValue As eFunctions.Values
		With lrecinsPostAGL771
			.StoredProcedure = "insAGL771"
			.Parameters.Add("dDateini", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateend", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterm_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostAGL771 = .Run(False)
			
			If insPostAGL771 Then
				.StoredProcedure = "ReaAgl771"
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
						If lrecinsPostAGL771.FieldToClass("nInt_status") = 1 Or lrecinsPostAGL771.FieldToClass("nInt_status") = String.Empty Then
							sInt_status = "N"
						Else
							sInt_status = "S"
						End If
						FileName = lstrLoadFile & "\RV_CORREDORES_" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
						FileNum = FreeFile
						FileOpen(FileNum, FileName, OpenMode.Output)
						Do While Not lrecinsPostAGL771.EOF
							lstrWritTxt = ""
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sClient"), " ", 10, "Right", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 5, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sCliename"), " ", 60, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("nOffice"), "0", 5)
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sIntertyp"), "0", 1)
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 10, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sDescadd"), " ", 40, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 20, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 20, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sPhone"), " ", 15, "Left", "Left")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 15, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 40, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 4, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 1, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sIntertyp"), "0", 1)
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("nOffice"), "0", 5)
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("nAgency"), " ", 5)
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("nIntertyp"), " ", 4, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("nUsercode"), " ", 8, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(" ", " ", 8, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sAction"), " ", 1, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(sInt_status, " ", 1)
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("dNulldate"), " ", 8, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sFirstname"), " ", 20, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sLastname"), " ", 20, "", "")
							lstrWritTxt = lstrWritTxt & FormatData(lrecinsPostAGL771.FieldToClass("sLastname2"), " ", 20, "", "")
							PrintLine(FileNum, lstrWritTxt)
							lrecinsPostAGL771.RNext()
						Loop 
						FileClose(FileNum)
					End If
				End If
				'+-------------------------------------------
			End If
			
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostAGL771 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostAGL771 = Nothing
		
insPostAGL771_Err: 
		If Err.Number Then
			insPostAGL771 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValAGL7000: Realiza la validación de la ventana AGL7000 - Comisiones del producto APV.
	'+[APV2] HAD 1014_BB. CALCULO DE COMISIONES DE APV
	Public Function insValAGL7000(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsUl_Daily_Balances As ePolicy.Ul_Daily_Balances
		Dim lobjProd As eProduct.Product
		Dim lblnError As Boolean
		Dim lstrMonth As String
		Dim lstrYear As String

		On Error GoTo insValAGL7000_Err
		
		lerrTime = New eFunctions.Errors
		lclsUl_Daily_Balances = New ePolicy.Ul_Daily_Balances
		lobjProd = New eProduct.Product
		
		'+ Se valida que el campo Producto.
		If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
			If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
				Call lerrTime.ErrorMessage("AGL7000", 70137)
			Else
				Call lobjProd.insValProdMaster(nBranch, nProduct)
				If lobjProd.blnError Then
					If CStr(lobjProd.sBrancht) <> "1" Then
						Call lerrTime.ErrorMessage("AGL7000", 70132)
					End If
				End If
				
				'+ Se valida que el producto corresponda a unit linked
				If lobjProd.FindProduct_li(nBranch, nProduct, Today) Then
					If lobjProd.nProdClas <> 4 Then
						Call lerrTime.ErrorMessage("AGL7000", 70140)
					End If
				Else
					Call lerrTime.ErrorMessage("AGL7000", 70140)
				End If
			End If
		End If
		
		If nMonth <= 0 Then
			Call lerrTime.ErrorMessage("AGL7000", 70115)
			lblnError = True
		End If
		
		If nYear <= 0 Then
			Call lerrTime.ErrorMessage("AGL7000", 70116)
			lblnError = True
		End If
		
		If Not lblnError Then
			lstrYear = CStr(Year(DateSerial(nYear, nMonth, 1)))
            If Not Len(lstrYear) = 4 Then
                Call lerrTime.ErrorMessage("AGL7000", 70178)
            End If
		End If
		
		insValAGL7000 = lerrTime.Confirm
		
insValAGL7000_Err: 
		If Err.Number Then
			insValAGL7000 = insValAGL7000 & Err.Description
		End If
		
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsUl_Daily_Balances may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUl_Daily_Balances = Nothing
		'UPGRADE_NOTE: Object lobjProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProd = Nothing
	End Function
	
	'% insPostAGL7000: Esta función permite realizar el proceso de Comisiones del producto APV - AGL7000.
	'+[APV2] HAD 1014_BB. CALCULO DE COMISIONES DE APV
	Public Function insPostAGL7000(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sMonth As String, ByVal sYear As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostAGL7000 As eRemoteDB.Execute
		
		On Error GoTo insPostAGL7000_Err
		
		lrecinsPostAGL7000 = New eRemoteDB.Execute
		
		With lrecinsPostAGL7000
			.StoredProcedure = "insAGL7000"
			.Parameters.Add("nBranch", IIf(nBranch = eRemoteDB.Constants.intNull, 0, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMonth", sMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sYear", sYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostAGL7000 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostAGL7000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostAGL7000 = Nothing
		
insPostAGL7000_Err: 
		If Err.Number Then
			insPostAGL7000 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%FormatData: Esta función se encarga de dar formato a los datos a enviar a archivos de texto.
	Private Function FormatData(ByVal sValue As Object, ByVal sChar As String, ByVal nPosition As Integer, Optional ByVal sTrunc As String = "Right", Optional ByVal sAlign As String = "Right") As String
		
		Dim nLength As Integer
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sValue) Then
			sValue = Trim(sValue)
			nLength = Len(sValue)
			If nLength > nPosition Then
				If sTrunc = "Right" Then
					FormatData = Right(sValue, nPosition)
				Else
					FormatData = Left(sValue, nPosition)
				End If
			Else
				If sAlign = "Right" Then
					FormatData = New String(sChar, nPosition - nLength) & sValue
				Else
					FormatData = sValue & New String(sChar, nPosition - nLength)
				End If
			End If
		Else
			FormatData = New String(sChar, nPosition)
		End If
	End Function
End Class






