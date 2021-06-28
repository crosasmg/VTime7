Option Strict Off
Option Explicit On
Public Class Tab_Am_Bab
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Am_Bab.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.tab_am_bab al 06-25-2002 15:17:17
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nGroup As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nTariff As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public sAuto_resist As String ' CHAR       1    0     0    S
	Public nRole As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public sIllness As String ' CHAR       8    0     0    N
	Public nLimit As Double ' DECIMAL    22   6     18   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'- Variables auxiliares de trabajo
	Public nHeader As Integer
	Public nLimitH As Double
	Public nPay_concep As Integer
	Public sDescript As String
	Public nDed_type As Integer
	Public nDed_percen As Double
	Public nDed_amount As Double
	Public nDed_quanti As Integer
	Public nIndem_rate As Double
	Public nLimitD As Double
	Public nLimit_exe As Double
	Public sIndic As String
	
	'%insCreUpdTab_am_bab: Rutina que permite leer la información de la tabla de datos básicos de
	'%Cobertura en la Tarifa del Ramo de Atención Médica.
	Public Function insCreUpdTab_am_bab(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, ByVal nTariff As Integer, ByVal sAutRestit As String, ByVal nLimit As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sIllness As String, ByVal nGroup As Integer) As Boolean
		Dim lrecTab_am_bab As eRemoteDB.Execute
		
		On Error GoTo insCreUpdTab_am_bab_Err
		
		lrecTab_am_bab = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insCreupdtab_am_bab al 03-09-2002 08:08:30
		'+
		With lrecTab_am_bab
			.StoredProcedure = "insCreUpdTab_am_bab"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAuto_resist", sAutRestit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit", nLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ Si se trata de una emisión
			If nTransaction = 1 Or nTransaction = 6 Or nTransaction = 4 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 7 Or nTransaction = 5 Or nTransaction = 19 Or nTransaction = 3 Or nTransaction = 2 Then
				.Parameters.Add("dNulldate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'+ Si se trata de una modificación normal
				If nTransaction = 12 Or nTransaction = 14 Then
					.Parameters.Add("dNulldate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nIndic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'+ Si se trata de una modificación temporal
					If nTransaction = 15 Or nTransaction = 13 Then
						.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nIndic", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
				End If
			End If
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insCreUpdTab_am_bab = .Run(False)
		End With
		
insCreUpdTab_am_bab_Err: 
		If Err.Number Then
			insCreUpdTab_am_bab = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_bab = Nothing
	End Function
	
	'%FindDeftValues: Obtiene los valores asociados a la tarifa a mostrar por defecto.
	Public Function FindDeftValues(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTab_am_bab As eRemoteDB.Execute
		Dim lintValue As Integer
        Dim lstrValue As String = ""

        On Error GoTo FindDeftValues_Err
		
		If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or nCertif <> Me.nCertif Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecTab_am_bab = New eRemoteDB.Execute
			
			With lrecTab_am_bab
				.StoredProcedure = "getTab_am_bab_defvalue"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup_o", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec_o", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover_o", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff_o", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole_o", lintValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient_o", lstrValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sIllness_o", lstrValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(False) Then
					If .Parameters("nTariff_o").Value > 0 Then
						Me.nTariff = .Parameters("nTariff_o").Value
						Me.nGroup = .Parameters("nGroup_o").Value
						Me.nModulec = .Parameters("nModulec_o").Value
						Me.nCover = .Parameters("nCover_o").Value
						Me.nRole = .Parameters("nRole_o").Value
						Me.sClient = Trim(.Parameters("sClient_o").Value & String.Empty)
						Me.sIllness = Trim(.Parameters("sIllness_o").Value & String.Empty)
						
						FindDeftValues = True
						
						Me.sCertype = sCertype
						Me.nBranch = nBranch
						Me.nProduct = nProduct
						Me.nPolicy = nPolicy
						Me.nCertif = nCertif
						Me.dEffecdate = dEffecdate
					End If
				End If
			End With
		Else
			FindDeftValues = True
		End If
		
FindDeftValues_Err: 
		If Err.Number Then
			FindDeftValues = False
		End If
		'UPGRADE_NOTE: Object lrecTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_bab = Nothing
	End Function
	
	'%insDelTab_am_babAll: Elimina la información del maestro cuando no existe información en el detalle.
	Public Function insDelTab_am_babAll(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTab_am_bab As eRemoteDB.Execute
		
		On Error GoTo insDelTab_am_babAll_Err
		
		lrecTab_am_bab = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insDelTab_am_babAll al 03-09-2002 08:08:30
		'+
		With lrecTab_am_bab
			.StoredProcedure = "insDelTab_am_babAll"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ Si se trata de una emisión
			If nTransaction = 1 Or nTransaction = 6 Or nTransaction = 4 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 7 Or nTransaction = 5 Or nTransaction = 19 Or nTransaction = 3 Or nTransaction = 2 Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'+ Si se trata de una modificación normal
				If nTransaction = 12 Or nTransaction = 14 Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nIndic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'+ Si se trata de una modificación temporal
					If nTransaction = 15 Or nTransaction = 13 Then
						.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nIndic", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
				End If
			End If
			insDelTab_am_babAll = .Run(False)
		End With
		
insDelTab_am_babAll_Err: 
		If Err.Number Then
			insDelTab_am_babAll = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_bab = Nothing
	End Function
	
	'%valExistsTab_am_babAll: Esta rutina es la encargada de verificar si existe información de conceptos de cobertura para una póliza/certificado.
	Public Function valExistsTab_am_babAll(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_am_bab As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsTab_am_babAll_Err
		
		lrecTab_am_bab = New eRemoteDB.Execute
		
		With lrecTab_am_bab
			.StoredProcedure = "valExistsTab_am_babAll"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsTab_am_babAll = (.Parameters("nExists").Value = 1)
		End With
		
valExistsTab_am_babAll_Err: 
		If Err.Number Then
			valExistsTab_am_babAll = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_am_bab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_bab = Nothing
	End Function
End Class






