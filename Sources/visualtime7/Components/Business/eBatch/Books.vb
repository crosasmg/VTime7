Option Strict Off
Option Explicit On
Public Class Books
	'%-------------------------------------------------------%'
	'% $Workfile::   Books.cls                              $%'
	'% $Author::   jpleteli                                 $%'
	'% $Date::   Jun 06 2006 09:08:54                       $%'
	'% $Revision::   1.14                                   $%'
	'%-------------------------------------------------------%'
	'+
	'+     Property                Type
	'+--------------------------------------
	Public sFileName_F As String
	Public P_Skey As String
	
	
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
	
	'%insGenFilesSIL850: Crea los archivos del proceso tmp_sil850
	Public Function insGenFilesSIL850(ByVal sKey As String) As String
		Dim lrecinsReatmp_sil850 As eRemoteDB.Execute
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As String
		
        'Dim nCount As Integer
		
		insGenFilesSIL850 = CStr(False)
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		'+ ------------------------------------------
		insGenFilesSIL850 = "SIL850_" & sKey & ".xls"
		FileName = lstrLoadFile & "SIL850_" & sKey & ".xls"
		Me.sFileName_F = insGenFilesSIL850
		FileNum = CStr(FreeFile)
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		PrintLine(CInt(FileNum), "CONSORCIO")
		
		lstrWritTxt = ""
		lstrWritTxt = lstrWritTxt & "COMPAÑIA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA CORTE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RAMO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRODUCTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NRO. PÓLIZA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "COBERTURA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MODULO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "CASO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "TIPO DEMANDANTE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RAMO CONTABLE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC RAMO CONTABLE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "OFICINA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC OFICINA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONEDA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC MONEDA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FACTOR CAMBIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA MOVIMIENTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM TRANSACCION" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA DECLAR SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA OCURRENCIA SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "ESTIMADO DAÑOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DEDUCIBLE MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROVISION MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROVISION PESOS" & Chr(9)
		'+ INFORMACION DE REASEGURO
		lstrWritTxt = lstrWritTxt & "MONTO RETENIDO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO RETENIDO PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO CEDIDO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO CEDIDO PESOS" & Chr(9)
		
		lstrWritTxt = lstrWritTxt & "SALDO PROVISION POR PAGAR MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "SALDO PROVISION POR PAGAR PESOS" & Chr(9)
		
		lstrWritTxt = lstrWritTxt & "PAGADO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "ESTADO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC ESTADO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "IND ORDER SERVICIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "INDICADOR PAGO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "TIPO FRANQUICIA/DEDUCIBLE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC TIPO F/D" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO FIJO FRANQ/DEDUC" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PORCENTAJE FRANQ/DEDUC" & Chr(9)
		lstrWritTxt = lstrWritTxt & "INDICADOR APLICA FRANQ/DEDUC" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC IND F/D" & Chr(9)
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecinsReatmp_sil850 = New eRemoteDB.Execute
		
		With lrecinsReatmp_sil850
			.StoredProcedure = "REATMP_SIL850_ARC"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					lstrWritTxt = ""
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCompany"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDate_end") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDate_end"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nBranch"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nProduct"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPolicy") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPolicy")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nClaim"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCover"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nModulec"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCase_num"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nDeman_type"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nBranch_led"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_nbranch_led"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nOfficeagen"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_nofficeagen"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCurrency"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_ncurrency"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nExchange"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dOperdate") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dOperdate"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("ntransac"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDecladat") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDecladat"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dOccurdat") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dOccurdat"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nDamages") = eRemoteDB.Constants.intNull, "", .FieldToClass("nDamages")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nFra_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nFra_amount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAmount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_amount")) & Chr(9)
					'+ INFORMACION DE REASEGURO
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, IIf(.FieldToClass("nAmount_ret") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAmount_ret"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, IIf(.FieldToClass("nLoc_amount_ret") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_amount_ret"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, IIf(.FieldToClass("nAmount_ced") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAmount_ced"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, IIf(.FieldToClass("nLoc_amount_ced") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_amount_ced"))) & Chr(9)
					
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("namount_topay") = eRemoteDB.Constants.intNull, "", .FieldToClass("namount_topay")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nloc_amount_topay") = eRemoteDB.Constants.intNull, "", .FieldToClass("nloc_amount_topay")) & Chr(9)
					
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPay_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPay_amount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sStaclaim"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_sstaclaim"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nInd_ord"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nInd_pag"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sFrandedi"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_sfrandedi"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nFixamount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nFixamount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nRate") = eRemoteDB.Constants.intNull, "", .FieldToClass("nRate")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sFrancapl"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("des_sfrancapl"), " ", 40, "Left", "Left") & Chr(9)
					
					
					If (lstrWritTxt <> "") Then
						PrintLine(CInt(FileNum), lstrWritTxt)
					End If
					
					.RNext()
				Loop 
				insGenFilesSIL850 = CStr(True)
				
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReatmp_sil850 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReatmp_sil850 = Nothing
		FileClose(CInt(FileNum))
		
	End Function
	
	'% InsCreTmp_COL837: Crea los registros de en la tabla temporal de rezago TMP_COL837.
	Public Function InsCreTmp_COL837(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_end As Date, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsTmp_COL837 As eRemoteDB.Execute
		
		lclsTmp_COL837 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_COL837
			.StoredProcedure = "CRETMP_COL837"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTmp_COL837 = True
			Else
				InsCreTmp_COL837 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_COL837 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_COL837 = Nothing
	End Function
	
	'% InsCreTmp_SIL704: Crea los registros de en la tabla temporal de siniestros devengados TMP_SIL704.
	Public Function InsCreTmp_SIL704(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsTmp_SIL704 As eRemoteDB.Execute
		
		lclsTmp_SIL704 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_SIL704
			.StoredProcedure = "CRETMP_SIL704"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTmp_SIL704 = True
			Else
				InsCreTmp_SIL704 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_SIL704 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_SIL704 = Nothing
	End Function
	
	'%insGenFilesSIL704: Crea los archivos del proceso tmp_sil704
	Public Function insGenFilesSIL704(ByVal sKey As String, ByVal nCompany As Integer) As String
		Dim lrecinsReatmp_SIL704 As eRemoteDB.Execute
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As String
		
        'Dim nCount As Integer
		
		insGenFilesSIL704 = CStr(False)
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		'+ Se genera el archivo
		insGenFilesSIL704 = "SIL704_" & sKey & ".xls"
		FileName = lstrLoadFile & insGenFilesSIL704
		Me.sFileName_F = insGenFilesSIL704
		FileNum = CStr(FreeFile)
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		PrintLine(CInt(FileNum), "CONSORCIO")
		
		lstrWritTxt = ""
		lstrWritTxt = lstrWritTxt & "COMPAÑIA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA INICIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA FIN" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RAMO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRODUCTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RAMO CONTABLE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC RAMO CONTABLE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "OFICINA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC OFICINA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NRO. PÓLIZA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "COBERTURA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUMERO CASO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "TIPO DEMANDANTE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RUT BENEFICIARIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NOMBRE BENEFICIARIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RUT LIQUIDADOR" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NOMBRE LIQUIDADOR" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONEDA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC MONEDA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FACTOR CAMBIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA MOVIMIENTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "ORDEN SERVICIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM TRANSACCION" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA DECLAR SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA OCURRENCIA SINIESTRO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "ESTIMADO DAÑOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DEDUCIBLE MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DEDUCIBLE PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "IVA MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "IVA PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DEDUCIBLE BRUTO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DEDUCIBLE BRUTO PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROVISION MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROVISION PESOS" & Chr(9)
		'MONTOS RETENCION Y CESIÓN
		lstrWritTxt = lstrWritTxt & "MONTO RETENIDO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO RETENIDO PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO CEDIDO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO CEDIDO PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROV+DEDUC MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO PROV+DEDUC PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "ESTADO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC ESTADO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "IND DEV/AJU" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RECUPERO MO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RECUPERO PESOS" & Chr(9)
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecinsReatmp_SIL704 = New eRemoteDB.Execute
		
		With lrecinsReatmp_SIL704
			.StoredProcedure = "REATMP_SIL704_ARC"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					lstrWritTxt = ""
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCompany"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDate_ini") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDate_ini"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDate_end") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDate_end"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nBranch"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nProduct"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nBranch_led"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sbranch_led"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nOfficeagen"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sofficeagen"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPolicy") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPolicy")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nClaim"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCover"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCase_num"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nDeman_type"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient"), " ", 14, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sCliename"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient_ben"), " ", 14, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sCliename_ben"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nCurrency"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("scurrency"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nExchange"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dOperdate") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dOperdate"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nServ_order"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("ntransac"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDecladat") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDecladat"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dOccurdat") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dOccurdat"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nDamages") = eRemoteDB.Constants.intNull, "", .FieldToClass("nDamages")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nFra_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nFra_amount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nFra_amount_loc") = eRemoteDB.Constants.intNull, "", .FieldToClass("nFra_amount_loc")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nDed_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nDed_amount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_ded_am") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_ded_am")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nDed_tot") = eRemoteDB.Constants.intNull, "", .FieldToClass("nDed_tot")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_ded_tot") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_ded_tot")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAmount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_amount")) & Chr(9)
					'monto de retencion y cesion
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount_ret") = eRemoteDB.Constants.intNull, "", IIf(nCompany = 1, System.DBNull.Value, .FieldToClass("nAmount_ret"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_amount_ret") = eRemoteDB.Constants.intNull, "", IIf(nCompany = 1, System.DBNull.Value, .FieldToClass("nLoc_amount_ret"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount_ced") = eRemoteDB.Constants.intNull, "", IIf(nCompany = 1, System.DBNull.Value, .FieldToClass("nAmount_ced"))) & Chr(9)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_amount_ced") = eRemoteDB.Constants.intNull, "", IIf(nCompany = 1, System.DBNull.Value, .FieldToClass("nLoc_amount_ced"))) & Chr(9)
					
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nProv_deduc") = eRemoteDB.Constants.intNull, "", .FieldToClass("nProv_deduc")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nProv_deduc_loc") = eRemoteDB.Constants.intNull, "", .FieldToClass("nProv_deduc_loc")) & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sStaclaim"), " ", 19, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("desc_sstaclaim"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nInd_type") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nRec_amount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nRec_amount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nLoc_rec_am") = eRemoteDB.Constants.intNull, "", .FieldToClass("nLoc_rec_am")) & Chr(9)
					
					If (lstrWritTxt <> "") Then
						PrintLine(CInt(FileNum), lstrWritTxt)
					End If
					
					.RNext()
				Loop 
				insGenFilesSIL704 = CStr(True)
				'UPGRADE_NOTE: Object lrecinsReatmp_SIL704 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecinsReatmp_SIL704 = Nothing
			End If
		End With
		FileClose(CInt(FileNum))
		
	End Function
	
	'insGenFilesCOL837: Crea los archivos del proceso tmp_COL837
	Public Function insGenFilesCOL837(ByVal sKey As String) As String
        'Dim lrecTime As eRemoteDB.Execute
		Dim lrecinsReatmp_COL837 As eRemoteDB.Execute
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As String
        'Dim nCount As Integer
		
		insGenFilesCOL837 = CStr(False)
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		'+ ------------------------------------------
		insGenFilesCOL837 = "COL837_" & sKey & ".xls"
		FileName = lstrLoadFile & insGenFilesCOL837
		Me.sFileName_F = insGenFilesCOL837
		FileNum = CStr(FreeFile)
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		PrintLine(CInt(FileNum), "CONSORCIO")
		
		lstrWritTxt = ""
		lstrWritTxt = lstrWritTxt & "COMPAÑIA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA MOV" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MES" & Chr(9)
		lstrWritTxt = lstrWritTxt & "TIPO MOV" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESC MOV" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RUT CLIENTE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NOMBRE CLIENTE" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONEDA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MONTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FACTOR CAMBIO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "SALDO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "REZAGO PESOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM RELACION" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM CAJA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM POLIZA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "NUM PROPUESTA" & Chr(9)
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecinsReatmp_COL837 = New eRemoteDB.Execute
		
		With lrecinsReatmp_COL837
			.StoredProcedure = "REATMP_COL837_ARC"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					
					lstrWritTxt = ""
					lstrWritTxt = lstrWritTxt & .FieldToClass("nCompany") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDatemove") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDatemove"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDatemove") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("dDatemove"), "MM/YYYY") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nType_conc") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sType_conc"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient"), " ", 14, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sCliename"), " ", 40, "Left", "Left") & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nCurrency") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAmount")) & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nExchange") & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nBalance") = eRemoteDB.Constants.intNull, "", .FieldToClass("nBalance")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nBalance_loc") = eRemoteDB.Constants.intNull, "", .FieldToClass("nBalance_loc")) & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nBordereaux") & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nCashnum") & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("nPolicy") & Chr(9)
					lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("nProponum"), " ", 19, "Left", "Left") & Chr(9)
					If (lstrWritTxt <> "") Then
						PrintLine(CInt(FileNum), lstrWritTxt)
					End If
					
					.RNext()
				Loop 
				insGenFilesCOL837 = CStr(True)
				'UPGRADE_NOTE: Object lrecinsReatmp_COL837 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecinsReatmp_COL837 = Nothing
			End If
		End With
		FileClose(CInt(FileNum))
	End Function
	
	'insGenFilesSIL705: Crea los archivos del proceso TMP_SIL705
	Public Function insGenFilesSIL705(ByVal sKey As String) As String
		Dim lrecinsReatmp_SIL705 As eRemoteDB.Execute
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
        'Dim lstrWritTxt As String
		Dim FileName As String
        'Dim FileNum As String
		Dim nCount As Integer
		
		insGenFilesSIL705 = CStr(False)
		
		Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
		Dim lclsWorksheet1 As Microsoft.Office.Interop.Excel.Worksheet
		Dim lclsWorksheet2 As Microsoft.Office.Interop.Excel.Worksheet
		Dim lclsWorksheet3 As Microsoft.Office.Interop.Excel.Worksheet
		
		lobjGeneral = New eGeneral.GeneralFunction
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As eFunctions.Values
		lclsValue = New eFunctions.Values
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		'+ ------------------------------------------
		insGenFilesSIL705 = "SIL705_" & sKey & ".xls"
		FileName = lstrLoadFile & insGenFilesSIL705
		Me.sFileName_F = insGenFilesSIL705
		
		
		lclsExcelApp = New Microsoft.Office.Interop.Excel.Application
		lclsExcelApp.Visible = False
		lclsExcelApp.Workbooks.Add()
		
		lclsExcelApp.DisplayAlerts = False
		
		'+Se guardan los valores de ramo producto y poliza en la primera hoja del libro y luego se oculta
		
		With lclsExcelApp.Workbooks(1)
			.Sheets(1).Name = "Detalle"
			.Sheets(2).Name = "Pago a proveedores"
			.Sheets(3).Name = "Gastos de suscripción"
			'        .Sheets(3).Visible = False
		End With
		
		
		lclsExcelApp.Workbooks(1).Sheets(1).Activate()
		lclsWorksheet1 = lclsExcelApp.Workbooks(1).Sheets(1)
		
		lclsWorksheet1.Cells._Default(1, 1) = "COMPAÑIA"
		lclsWorksheet1.Cells._Default(1, 2) = "FECHA INICIO"
		lclsWorksheet1.Cells._Default(1, 3) = "FECHA FIN"
		lclsWorksheet1.Cells._Default(1, 4) = "COD RAMO"
		lclsWorksheet1.Cells._Default(1, 5) = "RAMO"
		lclsWorksheet1.Cells._Default(1, 6) = "COD PRODUCTO"
		lclsWorksheet1.Cells._Default(1, 7) = "DESCRIP PRODUCTO"
		lclsWorksheet1.Cells._Default(1, 8) = "NUM POLIZA"
		lclsWorksheet1.Cells._Default(1, 9) = "NUM SINIESTRO"
		lclsWorksheet1.Cells._Default(1, 10) = "RUT BENEFICIARIO"
		lclsWorksheet1.Cells._Default(1, 11) = "NOMBRE BENEFICIARIO"
		lclsWorksheet1.Cells._Default(1, 12) = "FECHA OCURRENCIA"
		lclsWorksheet1.Cells._Default(1, 13) = "FECHA DECLARACION"
		lclsWorksheet1.Cells._Default(1, 14) = "TIPO PAGO"
		lclsWorksheet1.Cells._Default(1, 15) = "DESCRIP TIPO PAGO"
		lclsWorksheet1.Cells._Default(1, 16) = "FECHA PAGO"
		lclsWorksheet1.Cells._Default(1, 17) = "FECHA APROB SINIESTRO"
		lclsWorksheet1.Cells._Default(1, 18) = "FECHA APROB CHEQUE"
		lclsWorksheet1.Cells._Default(1, 19) = "FECHA ULT MOVIMIENTO"
		lclsWorksheet1.Cells._Default(1, 20) = "ORDEN PAGO"
		lclsWorksheet1.Cells._Default(1, 21) = "NUM DOCUMENTO"
		lclsWorksheet1.Cells._Default(1, 22) = "COD FORMA PAGO"
		lclsWorksheet1.Cells._Default(1, 23) = "FORMA PAGO"
		lclsWorksheet1.Cells._Default(1, 24) = "COD TIPO DOCTO"
		lclsWorksheet1.Cells._Default(1, 25) = "TIPO DOCTO"
		lclsWorksheet1.Cells._Default(1, 26) = "COD RAMO CONTABLE"
		lclsWorksheet1.Cells._Default(1, 27) = "RAMO CONTABLE"
		lclsWorksheet1.Cells._Default(1, 28) = "COD MONEDA"
		lclsWorksheet1.Cells._Default(1, 29) = "DESC MONEDA"
		lclsWorksheet1.Cells._Default(1, 30) = "PORC IMPUESTO"
		lclsWorksheet1.Cells._Default(1, 31) = "MONTO IMPUESTO"
		lclsWorksheet1.Cells._Default(1, 32) = "MONTO NETO MO"
		lclsWorksheet1.Cells._Default(1, 33) = "MONTO NETO PESOS"
		' INFORMACION DE MONTO RETENIDO Y CEDIDO
		lclsWorksheet1.Cells._Default(1, 34) = "MONTO RETENIDO MO"
		lclsWorksheet1.Cells._Default(1, 35) = "MONTO RETENIDO PESOS"
		lclsWorksheet1.Cells._Default(1, 36) = "MONTO CEDIDO MO"
		lclsWorksheet1.Cells._Default(1, 37) = "MONTO CEDIDO PESOS"
		
		lclsWorksheet1.Cells._Default(1, 38) = "MONTO PAGO CHEQUE"
		lclsWorksheet1.Cells._Default(1, 39) = "ARCHIVO TESORERIA"
		lclsWorksheet1.Cells._Default(1, 40) = "TIPO ORDEN"
		lclsWorksheet1.Cells._Default(1, 41) = "DESC TIPO ORDEN"
		lclsWorksheet1.Cells._Default(1, 42) = "ORDEN SERVICIO"
		lclsWorksheet1.Cells._Default(1, 43) = "RUT LIQUIDADOR"
		lclsWorksheet1.Cells._Default(1, 44) = "NOMBRE LIQUIDADOR"
		
		lrecinsReatmp_SIL705 = New eRemoteDB.Execute
		
		nCount = 2
		
		With lrecinsReatmp_SIL705
			.StoredProcedure = "REATMP_SIL705"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					lclsWorksheet1.Cells._Default(nCount, 1) = .FieldToClass("nCompany")
					lclsWorksheet1.Cells._Default(nCount, 2) = .FieldToClass("dDate_ini")
					lclsWorksheet1.Cells._Default(nCount, 3) = .FieldToClass("dDate_end")
					lclsWorksheet1.Cells._Default(nCount, 4) = .FieldToClass("nBranch")
					lclsWorksheet1.Cells._Default(nCount, 5) = .FieldToClass("sBranch")
					lclsWorksheet1.Cells._Default(nCount, 6) = .FieldToClass("nProduct")
					lclsWorksheet1.Cells._Default(nCount, 7) = .FieldToClass("sProduct")
					lclsWorksheet1.Cells._Default(nCount, 8) = .FieldToClass("nPolicy")
					lclsWorksheet1.Cells._Default(nCount, 9) = .FieldToClass("nClaim")
					lclsWorksheet1.Cells._Default(nCount, 10) = .FieldToClass("sClient")
					lclsWorksheet1.Cells._Default(nCount, 11) = .FieldToClass("sCliename")
					lclsWorksheet1.Cells._Default(nCount, 12) = .FieldToClass("dOccurdat")
					lclsWorksheet1.Cells._Default(nCount, 13) = .FieldToClass("dDecladat")
					lclsWorksheet1.Cells._Default(nCount, 14) = .FieldToClass("nOper_type")
					lclsWorksheet1.Cells._Default(nCount, 15) = .FieldToClass("sOper_type")
					lclsWorksheet1.Cells._Default(nCount, 16) = .FieldToClass("dIssue_dat")
					lclsWorksheet1.Cells._Default(nCount, 17) = .FieldToClass("dOperdate")
					lclsWorksheet1.Cells._Default(nCount, 18) = .FieldToClass("dDat_propos")
					lclsWorksheet1.Cells._Default(nCount, 19) = .FieldToClass("dCompdate")
					lclsWorksheet1.Cells._Default(nCount, 20) = .FieldToClass("nRequest_nu")
					lclsWorksheet1.Cells._Default(nCount, 21) = .FieldToClass("nBill")
					lclsWorksheet1.Cells._Default(nCount, 22) = .FieldToClass("nPay_form")
					lclsWorksheet1.Cells._Default(nCount, 23) = .FieldToClass("sPay_form")
					lclsWorksheet1.Cells._Default(nCount, 24) = .FieldToClass("nDoc_type")
					lclsWorksheet1.Cells._Default(nCount, 25) = .FieldToClass("sDoc_type")
					lclsWorksheet1.Cells._Default(nCount, 26) = .FieldToClass("nBranch_led")
					lclsWorksheet1.Cells._Default(nCount, 27) = .FieldToClass("sBranch_led")
					lclsWorksheet1.Cells._Default(nCount, 28) = .FieldToClass("nCurrency")
					lclsWorksheet1.Cells._Default(nCount, 29) = .FieldToClass("sCurrency")
					lclsWorksheet1.Cells._Default(nCount, 30) = .FieldToClass("nVa_tax")
					lclsWorksheet1.Cells._Default(nCount, 31) = .FieldToClass("nVat_amount")
					lclsWorksheet1.Cells._Default(nCount, 32) = .FieldToClass("nAmount")
					lclsWorksheet1.Cells._Default(nCount, 33) = .FieldToClass("nLoc_amount")
					' monto de retencion y cesion
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lclsWorksheet1.Cells._Default(nCount, 34) = IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, .FieldToClass("nAmount_ret"))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lclsWorksheet1.Cells._Default(nCount, 35) = IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, .FieldToClass("nLoc_amount_ret"))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lclsWorksheet1.Cells._Default(nCount, 36) = IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, .FieldToClass("nAmount_ced"))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lclsWorksheet1.Cells._Default(nCount, 37) = IIf(.FieldToClass("nCompany") = 1, System.DBNull.Value, .FieldToClass("nLoc_amount_ced"))
					
					lclsWorksheet1.Cells._Default(nCount, 38) = .FieldToClass("nAmountpay")
					lclsWorksheet1.Cells._Default(nCount, 39) = .FieldToClass("sFile_fin700")
					lclsWorksheet1.Cells._Default(nCount, 40) = .FieldToClass("nOrdertype")
					lclsWorksheet1.Cells._Default(nCount, 41) = .FieldToClass("sOrdertype")
					lclsWorksheet1.Cells._Default(nCount, 42) = .FieldToClass("nServ_order")
					lclsWorksheet1.Cells._Default(nCount, 43) = .FieldToClass("sClient_liq")
					lclsWorksheet1.Cells._Default(nCount, 44) = .FieldToClass("sCliename_liq")
					
					nCount = nCount + 1
					.RNext()
				Loop 
				insGenFilesSIL705 = CStr(True)
				
			End If
		End With
		
		lclsExcelApp.Workbooks(1).Sheets(2).Activate()
		lclsWorksheet2 = lclsExcelApp.Workbooks(1).Sheets(2)
		
		lclsWorksheet2.Cells._Default(1, 1) = "COMPAÑIA"
		lclsWorksheet2.Cells._Default(1, 2) = "FECHA INICIO"
		lclsWorksheet2.Cells._Default(1, 3) = "FECHA FIN"
		lclsWorksheet2.Cells._Default(1, 4) = "ORDEN DE PAGO"
		lclsWorksheet2.Cells._Default(1, 5) = "FECHA SOLICITUD"
		lclsWorksheet2.Cells._Default(1, 6) = "FECHA PAGO"
		lclsWorksheet2.Cells._Default(1, 7) = "CONCEPTO"
		lclsWorksheet2.Cells._Default(1, 8) = "POLIZA"
		lclsWorksheet2.Cells._Default(1, 9) = "BENEFICIARIO"
		lclsWorksheet2.Cells._Default(1, 10) = "NOMBRE BENEFICIARIO"
		lclsWorksheet2.Cells._Default(1, 11) = "RAMO"
		lclsWorksheet2.Cells._Default(1, 12) = "PRODUCTO"
		lclsWorksheet2.Cells._Default(1, 13) = "SINIESTRO"
		lclsWorksheet2.Cells._Default(1, 14) = "DESC PAGO"
		lclsWorksheet2.Cells._Default(1, 15) = "CODIGO IMPUESTO"
		lclsWorksheet2.Cells._Default(1, 16) = "MONTO BRUTO"
		lclsWorksheet2.Cells._Default(1, 17) = "MONTO AFECTO"
		lclsWorksheet2.Cells._Default(1, 18) = "MONTO EXENTO"
		lclsWorksheet2.Cells._Default(1, 19) = "MONTO IMPUESTO"
		lclsWorksheet2.Cells._Default(1, 20) = "MONTO PAGO"
		lclsWorksheet2.Cells._Default(1, 21) = "PORC IMPUESTO"
		lclsWorksheet2.Cells._Default(1, 22) = "ARCHIVO TESORERIA"
		lclsWorksheet2.Cells._Default(1, 23) = "CUADRATURA"
		
		nCount = 2
		
		With lrecinsReatmp_SIL705
			.StoredProcedure = "REATMP_SIL705_CHARC"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", 18, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					
					lclsWorksheet2.Cells._Default(nCount, 1) = .FieldToClass("nCompany")
					lclsWorksheet2.Cells._Default(nCount, 2) = .FieldToClass("dDate_ini")
					lclsWorksheet2.Cells._Default(nCount, 3) = .FieldToClass("dDate_end")
					lclsWorksheet2.Cells._Default(nCount, 4) = .FieldToClass("nRequest_nu")
					lclsWorksheet2.Cells._Default(nCount, 5) = .FieldToClass("dDat_propos")
					lclsWorksheet2.Cells._Default(nCount, 6) = .FieldToClass("dIssue_dat")
					lclsWorksheet2.Cells._Default(nCount, 7) = .FieldToClass("nConcept")
					lclsWorksheet2.Cells._Default(nCount, 8) = .FieldToClass("nPolicy")
					lclsWorksheet2.Cells._Default(nCount, 9) = .FieldToClass("sClient")
					lclsWorksheet2.Cells._Default(nCount, 10) = .FieldToClass("sCliename")
					lclsWorksheet2.Cells._Default(nCount, 11) = .FieldToClass("nBranch")
					lclsWorksheet2.Cells._Default(nCount, 12) = .FieldToClass("nProduct")
					lclsWorksheet2.Cells._Default(nCount, 13) = .FieldToClass("nClaim")
					lclsWorksheet2.Cells._Default(nCount, 14) = .FieldToClass("sDescript")
					lclsWorksheet2.Cells._Default(nCount, 15) = .FieldToClass("nTaxcode")
					lclsWorksheet2.Cells._Default(nCount, 16) = .FieldToClass("nAmount")
					lclsWorksheet2.Cells._Default(nCount, 17) = .FieldToClass("nAfect")
					lclsWorksheet2.Cells._Default(nCount, 18) = .FieldToClass("nExent")
					lclsWorksheet2.Cells._Default(nCount, 19) = .FieldToClass("nTax_amount")
					lclsWorksheet2.Cells._Default(nCount, 20) = .FieldToClass("nAmountpay")
					lclsWorksheet2.Cells._Default(nCount, 21) = .FieldToClass("nTax_percent")
					lclsWorksheet2.Cells._Default(nCount, 22) = .FieldToClass("sFile_fin700")
					lclsWorksheet2.Cells._Default(nCount, 23) = .FieldToClass("nAmount_cua")
					
					nCount = nCount + 1
					.RNext()
				Loop 
				insGenFilesSIL705 = CStr(True)
				
			End If
		End With
		
		lclsExcelApp.Workbooks(1).Sheets(3).Activate()
		lclsWorksheet3 = lclsExcelApp.Workbooks(1).Sheets(3)
		
		lclsWorksheet3.Cells._Default(1, 1) = "COMPAÑIA"
		lclsWorksheet3.Cells._Default(1, 2) = "FECHA INICIO"
		lclsWorksheet3.Cells._Default(1, 3) = "FECHA FIN"
		lclsWorksheet3.Cells._Default(1, 4) = "ORDEN DE PAGO"
		lclsWorksheet3.Cells._Default(1, 5) = "FECHA SOLICITUD"
		lclsWorksheet3.Cells._Default(1, 6) = "FECHA PAGO"
		lclsWorksheet3.Cells._Default(1, 7) = "CONCEPTO"
		lclsWorksheet3.Cells._Default(1, 8) = "POLIZA"
		lclsWorksheet3.Cells._Default(1, 9) = "BENEFICIARIO"
		lclsWorksheet3.Cells._Default(1, 10) = "NOMBRE BENEFICIARIO"
		lclsWorksheet3.Cells._Default(1, 11) = "RAMO"
		lclsWorksheet3.Cells._Default(1, 12) = "PRODUCTO"
		lclsWorksheet3.Cells._Default(1, 13) = "SINIESTRO"
		lclsWorksheet3.Cells._Default(1, 14) = "DESC PAGO"
		lclsWorksheet3.Cells._Default(1, 15) = "CODIGO IMPUESTO"
		lclsWorksheet3.Cells._Default(1, 16) = "MONTO BRUTO"
		lclsWorksheet3.Cells._Default(1, 17) = "MONTO AFECTO"
		lclsWorksheet3.Cells._Default(1, 18) = "MONTO EXENTO"
		lclsWorksheet3.Cells._Default(1, 19) = "MONTO IMPUESTO"
		lclsWorksheet3.Cells._Default(1, 20) = "MONTO PAGO"
		lclsWorksheet3.Cells._Default(1, 21) = "PORC IMPUESTO"
		lclsWorksheet3.Cells._Default(1, 22) = "ARCHIVO TESORERIA"
		lclsWorksheet3.Cells._Default(1, 23) = "CUADRATURA"
		
		nCount = 2
		
		With lrecinsReatmp_SIL705
			.StoredProcedure = "REATMP_SIL705_CHARC"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", 20, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				
				Do While Not .EOF
					
					lclsWorksheet3.Cells._Default(nCount, 1) = .FieldToClass("nCompany")
					lclsWorksheet3.Cells._Default(nCount, 2) = .FieldToClass("dDate_ini")
					lclsWorksheet3.Cells._Default(nCount, 3) = .FieldToClass("dDate_end")
					lclsWorksheet3.Cells._Default(nCount, 4) = .FieldToClass("nRequest_nu")
					lclsWorksheet3.Cells._Default(nCount, 5) = .FieldToClass("dDat_propos")
					lclsWorksheet3.Cells._Default(nCount, 6) = .FieldToClass("dIssue_dat")
					lclsWorksheet3.Cells._Default(nCount, 7) = .FieldToClass("nConcept")
					lclsWorksheet3.Cells._Default(nCount, 8) = .FieldToClass("nPolicy")
					lclsWorksheet3.Cells._Default(nCount, 9) = .FieldToClass("sClient")
					lclsWorksheet3.Cells._Default(nCount, 10) = .FieldToClass("sCliename")
					lclsWorksheet3.Cells._Default(nCount, 11) = .FieldToClass("nBranch")
					lclsWorksheet3.Cells._Default(nCount, 12) = .FieldToClass("nProduct")
					lclsWorksheet3.Cells._Default(nCount, 13) = .FieldToClass("nClaim")
					lclsWorksheet3.Cells._Default(nCount, 14) = .FieldToClass("sDescript")
					lclsWorksheet3.Cells._Default(nCount, 15) = .FieldToClass("nTaxcode")
					lclsWorksheet3.Cells._Default(nCount, 16) = .FieldToClass("nAmount")
					lclsWorksheet3.Cells._Default(nCount, 17) = .FieldToClass("nAfect")
					lclsWorksheet3.Cells._Default(nCount, 18) = .FieldToClass("nExent")
					lclsWorksheet3.Cells._Default(nCount, 19) = .FieldToClass("nTax_amount")
					lclsWorksheet3.Cells._Default(nCount, 20) = .FieldToClass("nAmountpay")
					lclsWorksheet3.Cells._Default(nCount, 21) = .FieldToClass("nTax_percent")
					lclsWorksheet3.Cells._Default(nCount, 22) = .FieldToClass("sFile_fin700")
					lclsWorksheet3.Cells._Default(nCount, 23) = .FieldToClass("nAmount_cua")
					
					nCount = nCount + 1
					.RNext()
				Loop 
				insGenFilesSIL705 = CStr(True)
				
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsReatmp_SIL705 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReatmp_SIL705 = Nothing
		
		lclsExcelApp.ActiveWorkbook.SaveAs(FileName)
		lclsExcelApp.ActiveWorkbook.Close()
		lclsExcelApp.Quit()
		
		'UPGRADE_NOTE: Object lclsExcelApp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExcelApp = Nothing
		'UPGRADE_NOTE: Object lclsWorksheet1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWorksheet1 = Nothing
		'UPGRADE_NOTE: Object lclsWorksheet2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWorksheet2 = Nothing
		'UPGRADE_NOTE: Object lclsWorksheet3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWorksheet3 = Nothing
		
	End Function
	
	'% InsCreTmp_SIL705: Crea los registros de en la tabla temporal de siniestros devengados TMP_SIL705.
	Public Function InsCreTmp_SIL705(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsTmp_SIL705 As eRemoteDB.Execute
		
		lclsTmp_SIL705 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_SIL705
			.StoredProcedure = "CRETMP_SIL705"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTmp_SIL705 = True
			Else
				InsCreTmp_SIL705 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_SIL705 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_SIL705 = Nothing
	End Function
	
	'% InsCreTMP_CAL503: Crea los registros de producción en la tabla TMP_CAL503, para luego mostrar el LT de producción.
	Public Function InsCreTMP_CAL503(ByVal p_cod_dia As Integer, ByVal p_area_seguro As Integer, ByVal p_fecha_desde As Date, ByVal p_fecha_hasta As Date) As Boolean
		
		'  ByVal nUsercode As Date   se comenta
		Dim lclsTmp_CAL503 As eRemoteDB.Execute
        Dim sKey As String = ""


        On Error GoTo Add_Err
		lclsTmp_CAL503 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		'+ Se comenta el parametro de entrada nUsercode
		With lclsTmp_CAL503
			.StoredProcedure = "CRETMP_CAL503"
			.Parameters.Add("p_cod_dia", p_cod_dia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_area_seguro", p_area_seguro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_fecha_desde", p_fecha_desde, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_fecha_hasta", p_fecha_hasta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "nUsercode", nUsercode, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			If .Run(False) Then
				P_Skey = .Parameters.Item("sKey").Value
				InsCreTMP_CAL503 = True
			Else
				InsCreTMP_CAL503 = False
			End If
			
		End With
		
Add_Err: 
		If Err.Number Then
			InsCreTMP_CAL503 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsTmp_CAL503 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_CAL503 = Nothing
	End Function
	
	'% InsCreTmp_COL837: Crea los registros de en la tabla temporal de rezago TMP_COL837.
	Public Function InsCreTmp_SIL850(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_end As Date, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsTmp_SIL850 As eRemoteDB.Execute
		
		lclsTmp_SIL850 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_SIL850
			.StoredProcedure = "CRETMP_SIL850"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTmp_SIL850 = True
			Else
				InsCreTmp_SIL850 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_SIL850 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_SIL850 = Nothing
	End Function
	
	'% InsCreTmp_SIL704: Crea los registros de en la tabla temporal de siniestros devengados TMP_SIL704.
	Public Function InsCreTmp_OPL700(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_ini_p As Date, ByVal dDate_end_p As Date, ByVal nCashnum As Integer, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		Dim lclsTmp_OPL700 As eRemoteDB.Execute
		
		lclsTmp_OPL700 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_OPL700
			.StoredProcedure = "CRETMP_OPL700"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_ini_p", dDate_ini_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end_p", dDate_end_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTmp_OPL700 = True
			Else
				InsCreTmp_OPL700 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_OPL700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_OPL700 = Nothing
	End Function

    '%insGenFilesCA891: Crea los archivos del proceso tmp_CA891
    Public Function insGenFilesCA891(ByVal sKey As String, ByVal sInd_Exe As String) As String
        Dim lrecReatmp_CA891 As eRemoteDB.Execute

        Dim lobjGeneral As eGeneral.GeneralFunction

        Dim lstrLoadFile As String
        Dim lstrDirFile As String
        Dim lstrWritTxt As String
        Dim FileName As String
        Dim FileNum As String

        Dim nCount As Integer
        Dim nSum As Integer

        insGenFilesCA891 = CStr(False)

        lobjGeneral = New eGeneral.GeneralFunction
        '+ Se busca la ruta en la que se guardará el archivo de texto
        lstrLoadFile = lobjGeneral.GetLoadFile()

        '+ Se busca el directorio virtual del archivo a crear
        Dim lclsValue As eFunctions.Values
        lclsValue = New eFunctions.Values
        lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
        'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValue = Nothing
        '+ ------------------------------------------
        insGenFilesCA891 = "CA891_" & sKey & ".txt"
        FileName = lstrLoadFile & "CA891_" & sKey & ".txt"
        Me.sFileName_F = insGenFilesCA891
        FileNum = CStr(FreeFile())
        FileOpen(CInt(FileNum), FileName, OpenMode.Output)

        If sInd_Exe = "1" Then
            PrintLine(CInt(FileNum), "DETALLE CAMBIO DE FECHA CONTABLE (PRELIMINAR)")
        Else
            PrintLine(CInt(FileNum), "DETALLE CAMBIO DE FECHA CONTABLE (DEFINITIVO)")
        End If

        PrintLine(CInt(FileNum), "--------------------------------")
        PrintLine(CInt(FileNum), "")
        PrintLine(CInt(FileNum), "|--------------|-------------------------------|---------|-------------|-------------|-------------|-----------|----------------|-------------|-------------|----------|------------|---------------|-------|-------------|-------------------|-------------------|-------------------------------|-------------|-------------|----------------|")

        lstrWritTxt = "|"
        lstrWritTxt = lstrWritTxt & FormatData("TIPO REGISTRO", " ", 14, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("TABLA", " ", 31, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("COD RAMO", " ", 9, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("RAMO", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("COD PRODUCTO", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("PRODUCTO", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NRO PÓLIZA", " ", 11, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NRO CERTIFICADO", " ", 16, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NRO RECIBO", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NRO CONTRATO", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NRO CUOTA", " ", 10, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("NUM TRANSAC", " ", 12, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("RUT CLIENTE", " ", 15, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("MONEDA", " ", 7, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("DESC MONEDA", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("MONTO (MO)", " ", 19, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("MONTO PESOS", " ", 19, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("TIPO MOV", " ", 31, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("FECHA MOV", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("FEC CONT ANT", " ", 13, "Left", "Left") & "|"
        lstrWritTxt = lstrWritTxt & FormatData("FEC CONT ACTUAL", " ", 16, "Left", "Left") & "|"

        PrintLine(CInt(FileNum), lstrWritTxt)

        PrintLine(CInt(FileNum), "|--------------|-------------------------------|---------|-------------|-------------|-------------|-----------|----------------|-------------|-------------|----------|------------|---------------|-------|-------------|-------------------|-------------------|-------------------------------|-------------|-------------|----------------|")


        lrecReatmp_CA891 = New eRemoteDB.Execute

        With lrecReatmp_CA891
            .StoredProcedure = "REAMOV_DATLED"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_exe", sInd_Exe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then

                nCount = 0
                nSum = 0

                Do While Not .EOF

                    lstrWritTxt = .FieldToClass("SLINE")
                    If (lstrWritTxt <> "") Then
                        PrintLine(CInt(FileNum), lstrWritTxt)
                        nCount = nCount + 1
                        nSum = nSum + .FieldToClass("NAMOUNT_LOCAL")
                    End If
                    .RNext()
                Loop
                insGenFilesCA891 = CStr(True)
            Else
                insGenFilesCA891 = CStr(False)
            End If

            PrintLine(CInt(FileNum), "|--------------|-------------------------------|---------|-------------|-------------|-------------|-----------|----------------|-------------|-------------|----------|------------|---------------|-------|-------------|-------------------|-------------------|-------------------------------|-------------|-------------|----------------|")
            PrintLine(CInt(FileNum), "")
            PrintLine(CInt(FileNum), "")
            PrintLine(CInt(FileNum), "Total Movimientos : " & nCount)
            PrintLine(CInt(FileNum), "Total (En Pesos)  : " & nSum)

        End With

        'UPGRADE_NOTE: Object lrecReatmp_CA891 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReatmp_CA891 = Nothing
        FileClose(CInt(FileNum))

    End Function
    '% InsCreTMP_CAL970: Crea los registros de producción en la tabla TMP_CAL970, para luego mostrar el LT de producción.
    Public Function InsCreTMP_CAL970(ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nUsercode As Date, ByVal sKey As String) As Boolean
		Dim lclsTmp_CAL970 As eRemoteDB.Execute
		
		lclsTmp_CAL970 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsTmp_CAL970
			.StoredProcedure = "INSPOST_CAL970"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCreTMP_CAL970 = True
			Else
				InsCreTMP_CAL970 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsTmp_CAL970 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_CAL970 = Nothing
	End Function
	
	
	'%insGenIllustration: Crea los archivos del proceso VP_MONTH
	Public Function insGenIllustration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer) As String
		Dim lrecIlustration As Object
		
		Dim lobjGeneral As Object
		
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
        Dim lstrWritTxt As String = ""
        Dim FileName As String
		Dim FileNum As String
		
		insGenIllustration = CStr(False)
		
		lobjGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
		
		'+ Se busca la ruta en la que se guardará el archivo de texto
		lstrLoadFile = lobjGeneral.GetLoadFile()
		
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
		
		'+ Se busca el directorio virtual del archivo a crear
		Dim lclsValue As Object 'eFunctions.Values
		
		lclsValue = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")
		lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		'+ ------------------------------------------
		insGenIllustration = "VA595_" & nPolicy & ".xls"
		FileName = lstrLoadFile & "VA595_" & nPolicy & ".xls"
		Me.sFileName_F = insGenIllustration
		FileNum = CStr(FreeFile)
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		PrintLine(CInt(FileNum), "Ilustración del valor póliza: " & nPolicy)
		lstrWritTxt = lstrWritTxt & "AÑO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "MES" & Chr(9)
		lstrWritTxt = lstrWritTxt & "FECHA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRIMA PAGADA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRIMA ADICIONAL" & Chr(9)
		lstrWritTxt = lstrWritTxt & "DESCUENTO" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRIMA NETA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "COSTOS COBERTURA" & Chr(9)
		lstrWritTxt = lstrWritTxt & "COSTOS FIJOS" & Chr(9)
		lstrWritTxt = lstrWritTxt & "INTERES" & Chr(9)
		lstrWritTxt = lstrWritTxt & "RESCATES" & Chr(9)
		lstrWritTxt = lstrWritTxt & "PRIMA DE INJECCION" & Chr(9)
		lstrWritTxt = lstrWritTxt & "VP ANTERIOR" & Chr(9)
		lstrWritTxt = lstrWritTxt & "VP ACTUAL" & Chr(9)
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecIlustration = New eRemoteDB.Execute
		
		With lrecIlustration
			.StoredProcedure = "REAVP_MONTH"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run() Then
				Do While Not .EOF
					
					lstrWritTxt = ""
					lstrWritTxt = lstrWritTxt & .FieldToClass("NYEAR") & Chr(9)
					lstrWritTxt = lstrWritTxt & .FieldToClass("NMONTH") & Chr(9)
					lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("DEFFECDATE") = eRemoteDB.Constants.dtmNull, "", "'" & Format(.FieldToClass("DEFFECDATE"), "yyyy/MM/dd") & "'")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NAMOUNT") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NAMOUNT")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NAMOUNTADD") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NAMOUNTADD")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NDISCOUNT") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NDISCOUNT")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NPREMIUMN") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NPREMIUMN")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NCOVERCOST") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NCOVERCOST")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NCHARGES") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NCHARGES")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NINTEREST") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NINTEREST")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NSURRENDER") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NSURRENDER")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NINJPREM") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NINJPREM")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NLAST_VP") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NLAST_VP")) & Chr(9)
					lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("NVP") = eRemoteDB.Constants.intNull, 0, .FieldToClass("NVP")) & Chr(9)
					
					If (lstrWritTxt <> "") Then
						PrintLine(CInt(FileNum), lstrWritTxt)
					End If
					.RNext()
				Loop 
				insGenIllustration = CStr(True)
			Else
				insGenIllustration = CStr(False)
			End If
			
			PrintLine(CInt(FileNum), "")
			PrintLine(CInt(FileNum), "")
			
		End With
		
		'UPGRADE_NOTE: Object lrecIlustration may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIlustration = Nothing
		FileClose(CInt(FileNum))
		
    End Function

    '%insValCol602: Válida transacción de Interfaz Contable de Recaudación
    Public Function insValCol602_k(ByVal sCodispl As String, ByVal dInit As Date, ByVal dEnd As Date, ByVal nOption As Integer) As String
        Dim lclsCtrol_date As eGeneral.Ctrol_date
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCol602_k_err

        lclsCtrol_date = New eGeneral.Ctrol_date
        lclsErrors = New eFunctions.Errors

        'Validación de campos nulos
        If dInit = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 60217)
        End If

        If dEnd = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 60218)
        End If

        If dEnd < dInit Then
            Call lclsErrors.ErrorMessage(sCodispl, 12120)
        End If

        'Control de opción ingresada para generar libro
        If nOption = 2 Then 'Definitivo
            If lclsCtrol_date.Find(211) Then
                If dInit < lclsCtrol_date.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90000077)
                End If
            End If
        End If

        insValCol602_k = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCtrol_date = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValCol602_k_err:
        If Err.Number Then
            insValCol602_k = insValCol602_k & Err.Description
        End If
        On Error GoTo 0
    End Function

    Public Function Cretmp_Col602(ByVal dInit As Date, ByVal dEnd As Date, ByVal nOption As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsTmp_COL602 As eRemoteDB.Execute
        Dim sKey As String = ""

        On Error GoTo Add_Err
        lclsTmp_COL602 = New eRemoteDB.Execute

        With lclsTmp_COL602
            .StoredProcedure = "CRETMP_COL602"
            .Parameters.Add("P_DINIT_DATE", dInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_DEND_DATE", dEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_NEXECUTE", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_NUSERCODE", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                P_Skey = .Parameters.Item("P_SKEY").Value
                Cretmp_Col602 = True
            Else
                Cretmp_Col602 = False
            End If

        End With

Add_Err:
        If Err.Number Then
            Cretmp_Col602 = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsTmp_CAL503 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Cretmp_Col602 = Nothing
    End Function

    '%insValSil604: Válida transacción de Libros de Siniestros
    Public Function insValSil604_k(ByVal sCodispl As String, ByVal dInit As Date, ByVal dEnd As Date, ByVal nOption As Integer) As String
        Dim lclsCtrol_date As eGeneral.Ctrol_date
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSil604_k_err

        lclsCtrol_date = New eGeneral.Ctrol_date
        lclsErrors = New eFunctions.Errors

        'Validación de campos nulos
        If dInit = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 60217)
        End If

        If dEnd = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 60218)
        End If

        If dEnd < dInit Then
            Call lclsErrors.ErrorMessage(sCodispl, 12120)
        End If

        'Control de opción ingresada para generar libro
        If nOption = 2 Then 'Definitivo
            If lclsCtrol_date.Find(214) Then
                If dInit < lclsCtrol_date.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90000077, , , CStr(CDate(lclsCtrol_date.dEffecdate)))
                End If
            End If
        End If

        insValSil604_k = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCtrol_date = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValSil604_k_err:
        If Err.Number Then
            insValSil604_k = insValSil604_k & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% InsCretmp_Sil604: Crea registros del Libro de siniestros
    Public Function Cretmp_Sil604(ByVal dInit As Date, ByVal dEnd As Date, ByVal nOption As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsTmp_SIL604 As eRemoteDB.Execute
        Dim sKey As String = ""

        On Error GoTo Add_Err
        lclsTmp_SIL604 = New eRemoteDB.Execute

        With lclsTmp_SIL604
            .StoredProcedure = "CRETMP_SIL604"
            .Parameters.Add("P_DINIT_DATE", dInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_DEND_DATE", dEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_NEXECUTE", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_NUSERCODE", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                P_Skey = .Parameters.Item("P_SKEY").Value
                Cretmp_Sil604 = True
            Else
                Cretmp_Sil604 = False
            End If

        End With

Add_Err:
        If Err.Number Then
            Cretmp_Sil604 = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsTmp_SIL604 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Cretmp_Sil604 = Nothing
    End Function

End Class






