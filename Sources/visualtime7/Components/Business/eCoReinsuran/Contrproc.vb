Option Strict Off
Option Explicit On
Public Class Contrproc
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrproc.cls                            $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 27/03/06 19:30                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.contrproc al 04-30-2002 12:44:26
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nAmount As Integer ' NUMBER     22   0     12   S
	Public nFqcy_acc As Integer ' NUMBER     22   0     5    S
	Public nType_rel As Integer ' NUMBER     22   0     5    N
	Public sCap_nom_ri As String ' CHAR       1    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nExcess As Double ' NUMBER     22   0     12   S
	Public nExpenses As Double ' NUMBER     22   2     4    S
	Public nFact_reser As Integer ' NUMBER     22   0     12   S
	Public nFixed_prat As Double ' NUMBER     22   2     4    S
	Public nGroup_bene As Integer ' NUMBER     22   0     10   S
	Public nGroup_co As Integer ' NUMBER     22   0     10   S
	Public nInt_claim As Double ' NUMBER     22   2     4    S
	Public nInt_prem As Double ' NUMBER     22   2     4    S
	Public nLines As Double ' NUMBER     22   0     5    S
	Public nMax_even As Integer ' NUMBER     22   0     12   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nPrem_dep As Double ' NUMBER     22   2     4    S
	Public nProfit_sh As Double ' NUMBER     22   2     4    S
	Public nQuota_sha As Double ' NUMBER     22   2     4    S
	Public nRate_claim As Double ' NUMBER     22   2     4    S
	Public sReser_clai As String ' CHAR       1    0     0    S
	Public nReten_min As Double ' NUMBER     22   0     12   S
	Public nTab_commi As Integer ' NUMBER     22   0     10   S
	Public sTab_limit As String ' CHAR       1    0     0    S
	Public nTran_prem As Double ' NUMBER     22   2     4    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nYear_begin As Integer ' NUMBER     22   0     5    S
	Public nYear_end As Integer ' NUMBER     22   0     5    S
	Public sRetcover As String ' CHAR       1    0     0    N
	Public sLimitCov As String ' CHAR       1    0     0    N
	Public sCessprcov As String ' CHAR       1    0     0    N
	Public sExtraprem As String ' CHAR       1    0     0    N
	Public sGencess As String ' CHAR       1    0     0    N
	Public nRate As Double ' NUMBER     22   6     9    S
	Public nCessprfix As Double ' NUMBER     22   2     10   S
	Public nMincapcess As Integer ' NUMBER     22   0     12   S
	Public nNextmonthp As Integer ' NUMBER     22   0     5    S
	Public nNextyearp As Integer ' NUMBER     22   0     5    S
	Public nNextmonthc As Integer ' NUMBER     22   0     5    S
	Public nNextyearc As Integer ' NUMBER     22   0     5    S
	Public sRouprofit As String ' CHAR       12   0     0    S
	Public sCommCov As String ' CHAR       1    0     0    S
	Public nInterest As Double ' NUMBER     22   2     4    S
	Public sCumultyp As String ' CHAR       1    0     0    S
	Public sCumreint As String ' CHAR       1    0     0    S
	Public sCumulpol As String ' CHAR       1    0     0    S
	Public nFreqpay As Integer ' NUMBER     22   0     5    S
	Public nNextmonthpa As Integer ' NUMBER     22   0     5    S
	Public nNextyearpa As Integer ' NUMBER     22   0     5    S
	Public sRetzone As String ' CHAR       1    0     0    S
	Public sCesscia As String ' CHAR       1    0     0    S
	Public nInd_Age As Integer ' NUMBER     22   0     5    S
    Public nMaxRetAmount As Double

	'+ Propiedades Auxiliares
	
	Public dblRetention As Double
	Public lintType_rel As Integer
	Public nCurrency As Integer
	Public dContrDate As Date
	Public blnRetention As Boolean
	Public nRetention As Double
	Public pcurRetention As Double
	Public sBrancht As String
	
	Private mvarLastModify As Date
	'+ DefaultValues CR301
	Public nOptCumulpol_1 As Integer
	Public nOptCumulpol_2 As Integer
	Public nOptCumulpol_3 As Integer
	
	'*LastModifyDate: Esta propiedad se encarga de la fecha de última modificación del contrato
	Public ReadOnly Property LastModifyDate() As Date
		Get
			LastModifyDate = mvarLastModify
		End Get
	End Property
	
	'%Find: Se realiza la lectura para verificar la existencia del código del contrato proporcional
	Public Function Find(ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaContrproc As eRemoteDB.Execute
		
		lrecreaContrproc = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaContrproc'
		'+ Información leída el 23/05/2001 10:29:00 a.m.
		
		With lrecreaContrproc
			.StoredProcedure = "reaContrproc"
			If nNumber <> 0 Or nType <> 1 Then
				.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nNumber", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nType <> 0 And nType <> eRemoteDB.Constants.intNull Then
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nType", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nNumber = .FieldToClass("nNumber")
				Me.nType = .FieldToClass("nType")
				Me.nBranch = .FieldToClass("nBranch")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				nAmount = .FieldToClass("nAmount")
				nType_rel = .FieldToClass("nType_rel")
				sCap_nom_ri = .FieldToClass("sCap_nom_ri")
				dCompdate = .FieldToClass("dCompdate")
				nExcess = .FieldToClass("nExcess")
				nExpenses = .FieldToClass("nExpenses")
				nFact_reser = .FieldToClass("nFact_reser")
				nFixed_prat = .FieldToClass("nFixed_prat")
				nFqcy_acc = .FieldToClass("nFqcy_acc")
				nGroup_bene = .FieldToClass("nGroup_bene")
				nGroup_co = .FieldToClass("nGroup_co")
				nInt_claim = .FieldToClass("nInt_claim")
				nInt_prem = .FieldToClass("nInt_prem")
				nLines = .FieldToClass("nLines")
				nMax_even = .FieldToClass("nMax_even")
				dNulldate = .FieldToClass("dNulldate")
				nPrem_dep = .FieldToClass("nPrem_dep")
				nProfit_sh = .FieldToClass("nProfit_sh")
				nQuota_sha = .FieldToClass("nQuota_sha")
				nRate_claim = .FieldToClass("nRate_claim")
				sReser_clai = .FieldToClass("sReser_clai")
				nReten_min = .FieldToClass("nReten_min")
				nTab_commi = .FieldToClass("nTab_commi")
				sTab_limit = .FieldToClass("sTab_limit")
				nTran_prem = .FieldToClass("nTran_prem")
				nUsercode = .FieldToClass("nUsercode")
				nYear_begin = .FieldToClass("nYear_begin")
				nYear_end = .FieldToClass("nYear_end")
				Me.sRetcover = .FieldToClass("sRetcover")
				sLimitCov = .FieldToClass("sLimitcov")
				sCumulpol = .FieldToClass("sCumulpol")
				sRetzone = .FieldToClass("sRetzone")
				sCesscia = .FieldToClass("sCesscia")
				sCumultyp = .FieldToClass("sCumultyp")
				sCumreint = .FieldToClass("sCumreint")
				sCessprcov = .FieldToClass("sCessprcov")
				nFqcy_acc = .FieldToClass("nFqcy_acc")
				sCap_nom_ri = .FieldToClass("sCap_nom_ri")
				
				nRate = .FieldToClass("nRate")
				nCessprfix = .FieldToClass("nCessprfix")
				sExtraprem = .FieldToClass("sExtraprem")
				sGencess = .FieldToClass("sGencess")
				sCommCov = .FieldToClass("sCommcov")
				nNextmonthc = .FieldToClass("nNextmonthc")
				nNextyearc = .FieldToClass("nNextyearc")
				nFreqpay = .FieldToClass("nFreqpay")
				nNextmonthp = .FieldToClass("nNextmonthp")
				nNextyearp = .FieldToClass("nNextyearp")
				Me.nInterest = .FieldToClass("nInterest")
				Me.nMincapcess = .FieldToClass("nMincapcess")
				Me.nCurrency = .FieldToClass("nCurrency")
                nInd_Age = .FieldToClass("nInd_Age")
                nMaxRetAmount = .FieldToClass("nMaxRetAmount")
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrproc = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'% FindLastDate: Devuelve la última fecha de efecto
	Public Function FindLastDate(ByVal nNumber As Integer) As Boolean
		Dim lrecreacontrproc_v As eRemoteDB.Execute
		
		On Error GoTo FindLastDate_Err
		
		lrecreacontrproc_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProduct_v'
		'+ Información leída el 21/03/2001 01:29:24 p.m.
		
		With lrecreacontrproc_v
			.StoredProcedure = "reacontrproc_v"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				FindLastDate = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecreacontrproc_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreacontrproc_v = Nothing
		
FindLastDate_Err: 
		If Err.Number Then
			FindLastDate = False
		End If
		'UPGRADE_NOTE: Object lrecreacontrproc_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreacontrproc_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insContrProc: Creación de un registro en el archivo de los contratos de reaseguro
	Public Function insContrProc(ByVal sCodispl As String, Optional ByRef lblnUpdate As Boolean = False) As Boolean
		Dim lrecinsContrproc As eRemoteDB.Execute
		Dim lblnFirstTime As Boolean
		
		lrecinsContrproc = New eRemoteDB.Execute
		
		On Error GoTo insContrProc_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insContrproc'
		'+ Información leída el 23/05/2001 11:51:31 a.m.
		
		With lrecinsContrproc
			.StoredProcedure = "insContrproc"
			
			lblnFirstTime = False
			
			'+Si se trata de los datos del contrato a nivel de cabecera
			If sCodispl = "CR301_K" Then
				If lblnUpdate Then
					lblnFirstTime = False
				Else
					lblnFirstTime = True
				End If
				'+ Se pasan los valores a los parámetros de la clave de la tabla.
				
				.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				
				'+ La primera vez crea el registro con las variables claves y las demás con null.
				If lblnFirstTime Then
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nAmount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sCap_nom_ri", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nExcess", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nExpenses", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nFact_reser", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nFixed_prat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nFqcy_acc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nGroup_bene", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nGroup_co", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nInt_claim", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nInt_prem", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nLines", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nMax_even", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nPrem_dep", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nProfit_sh", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nQuota_sha", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nRate_claim", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sReser_clai", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sCesscia", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nReten_min", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nTab_commi", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sTab_limit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nTran_prem", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nYear_begin", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nYear_end", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nInd_Age", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("nMaxRetAmount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'+ En este caso (no la primera vez) se actualiza la tabla con lo que tenga el recorset de trabajo.
					
					.Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCap_nom_ri", Me.sCap_nom_ri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExcess", Me.nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExpenses", Me.nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFact_reser", Me.nFact_reser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFixed_prat", Me.nFixed_prat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFqcy_acc", Me.nFqcy_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_bene", Me.nGroup_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_co", Me.nGroup_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_claim", Me.nInt_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_prem", Me.nInt_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nLines", Me.nLines, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nMax_even", Me.nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProfit_sh", Me.nProfit_sh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nQuota_sha", Me.nQuota_sha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nRate_claim", Me.nRate_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sReser_clai", Me.sReser_clai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCesscia", sCesscia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nReten_min", Me.nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTab_commi", Me.nTab_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sTab_limit", Me.sTab_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTran_prem", Me.nTran_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_begin", Me.nYear_begin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_end", Me.nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInd_Age", nInd_Age, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRetAmount", Me.nMaxRetAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
			Else
				.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If Not lblnFirstTime Then
				'+Si se trata de la información del contrato proporcional
				If sCodispl = "CR301" Then
					If nType = 1 Then
						.Parameters.Add("nAmount", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Parameters.Add("sCap_nom_ri", Me.sCap_nom_ri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExcess", Me.nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExpenses", Me.nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFact_reser", Me.nFact_reser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFixed_prat", Me.nFixed_prat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFqcy_acc", Me.nFqcy_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_bene", Me.nGroup_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_co", Me.nGroup_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_claim", Me.nInt_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_prem", Me.nInt_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nLines", nLines, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nMax_even", nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProfit_sh", Me.nProfit_sh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nQuota_sha", nQuota_sha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nRate_claim", Me.nRate_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sReser_clai", Me.sReser_clai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCesscia", sCesscia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nReten_min", nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTab_commi", nTab_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sTab_limit", Me.sTab_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTran_prem", Me.nTran_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_begin", Me.nYear_begin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_end", Me.nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRetcover", Me.sRetcover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRetzone", Me.sRetzone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sLimitcov", Me.sLimitCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCumultyp", Me.sCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCumreint", Me.sCumreint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCumulpol", Me.sCumulpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("sCessprcov", Me.sCessprcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sExtraprem", Me.sExtraprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sGencess", Me.sGencess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate", Me.nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCessprfix", Me.nCessprfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCommcov", Me.sCommCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthp", Me.nNextmonthp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearp", Me.nNextyearp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMincapcess", Me.nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("nInd_Age", nInd_Age, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRetAmount", nMaxRetAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				'+Si se trata de la comisión y reserva del contrato
				If sCodispl = "CR302" Then
					.Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If sCap_nom_ri = "1" Then
						.Parameters.Add("sCap_nom_ri", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						.Parameters.Add("sCap_nom_ri", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Parameters.Add("nExcess", Me.nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExpenses", Me.nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFact_reser", nFact_reser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFixed_prat", nFixed_prat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFqcy_acc", nFqcy_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_bene", Me.nGroup_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_co", nGroup_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_claim", nInt_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_prem", nInt_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nLines", Me.nLines, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nMax_even", Me.nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPrem_dep", nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProfit_sh", Me.nProfit_sh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nQuota_sha", Me.nQuota_sha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nRate_claim", Me.nRate_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If sReser_clai = "1" Then
						.Parameters.Add("sReser_clai", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						.Parameters.Add("sReser_clai", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					.Parameters.Add("sCesscia", sCesscia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nReten_min", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTab_commi", nTab_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sTab_limit", Me.sTab_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTran_prem", Me.nTran_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_begin", Me.nYear_begin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_end", Me.nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRetcover", Me.sRetcover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRetzone", Me.sRetzone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sLimitcov", sLimitCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCumultyp", sCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCumreint", sCumreint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCumulpol", sCumulpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					.Parameters.Add("sCessprcov", sCessprcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sExtraprem", sExtraprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sGencess", sGencess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCessprfix", nCessprfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCommcov", sCommCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nNextmonthc", nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nNextyearc", nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFreqpay", nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nNextmonthp", nNextmonthp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nNextyearp", nNextyearp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nMincapcess", Me.nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInd_Age", nInd_Age, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRetAmount", Me.nMaxRetAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				'+Si se trata de la participación en beneficios y traspaso de cartera
				If sCodispl = "CR303" Then
					.Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCap_nom_ri", Me.sCap_nom_ri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExcess", nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFact_reser", Me.nFact_reser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFixed_prat", Me.nFixed_prat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nFqcy_acc", Me.nFqcy_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_bene", nGroup_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nGroup_co", Me.nGroup_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_claim", Me.nInt_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInt_prem", Me.nInt_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nLines", Me.nLines, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nMax_even", Me.nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProfit_sh", nProfit_sh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nQuota_sha", Me.nQuota_sha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nRate_claim", nRate_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sReser_clai", Me.sReser_clai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCesscia", sCesscia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nReten_min", Me.nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTab_commi", Me.nTab_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sTab_limit", Me.sTab_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTran_prem", nTran_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_begin", Me.nYear_begin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRetcover", Me.sRetcover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRetzone", Me.sRetzone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sLimitcov", Me.sLimitCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCumultyp", Me.sCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCumreint", Me.sCumreint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCumulpol", Me.sCumulpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("sCessprcov", Me.sCessprcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sExtraprem", Me.sExtraprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sGencess", Me.sGencess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate", Me.nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCessprfix", Me.nCessprfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCommcov", Me.sCommCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthp", Me.nNextmonthp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearp", Me.nNextyearp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMincapcess", Me.nReten_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("nInd_Age", nInd_Age, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRetAmount", nMaxRetAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



				End If
				
			End If
			insContrProc = .Run(False)
			
		End With
		
insContrProc_Err: 
		If Err.Number Then
			insContrProc = False
		End If
	End Function
	
	'%insPostCR301_k: Esta función se encarga de validar los datos introducidos en la zona de
	'%cabecera.
	Public Function insPostCR301_k(ByVal sCodispl As String, ByVal nAction As String, ByVal nNumber As Integer, ByVal dStartdate As Date, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		
		lclsContrproc = New eCoReinsuran.Contrproc
		lclsContrmaster = New eCoReinsuran.Contrmaster
		
		On Error GoTo insPostCR301_k_Err
		
		insPostCR301_k = True
		
		'+Se inicializan los valores de la llave del contrato
		
		With lclsContrmaster
			.nType_rel = lintType_rel
			.nNumber = nNumber
			.nType = nContraType
			.nBranch = nBranch
			.dStartdate = dStartdate
			.nUsercode = nUsercode
		End With
		
		With lclsContrproc
			.nNumber = nNumber
			.dContrDate = dStartdate
			.dEffecdate = dStartdate
			.nUsercode = nUsercode
			
			If nAction = CStr(eFunctions.Menues.TypeActions.clngActionadd) Or nAction = CStr(eFunctions.Menues.TypeActions.clngActionModify) Then
				.nType = nContraType
				.nBranch = nBranch
			End If
			
			Select Case nAction
				'+Si la opción seleccionada es Registrar
				
				Case CStr(eFunctions.Menues.TypeActions.clngActionadd)
					If lclsContrmaster.creContrMaster Then
						If .insContrProc("CR301_K") Then
							insPostCR301_k = True
						End If
					Else
						insPostCR301_k = False
					End If
					
					'+Si la opción seleccionada es Modificar
				Case CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
					If .insContrProcHeader(nNumber, dStartdate, nContraType, nBranch, nUsercode) Then
						insPostCR301_k = True
					Else
						insPostCR301_k = False
					End If
					
					'+Si la opción seleccionada es Consulta
					
				Case CStr(eFunctions.Menues.TypeActions.clngActionQuery)
					insPostCR301_k = .Find(nNumber, 0, 0, dStartdate, True)
					
					If insPostCR301_k Then
						
						nContraType = .nType
						nBranch = .nBranch
						
						.nType = nContraType
						.nBranch = nBranch
						
						If .Find(0, 1, nBranch, dStartdate) Then
							Me.dblRetention = .nAmount
						End If
					End If
			End Select
		End With
		
		If nContraType = 1 Then
			blnRetention = True
		Else
			blnRetention = False
		End If
		
insPostCR301_k_Err: 
		If Err.Number Then
			insPostCR301_k = False
		End If
	End Function
	
	'%insValCR301_k: Esta función se encarga de validar los datos introducidos en la forma CR301_k (Header).
	Public Function insValCR301_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer) As String
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		Dim lclsErrors As eFunctions.Errors
		Dim lintReten As Integer
		
		lclsContrmaster = New eCoReinsuran.Contrmaster
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR301_k_Err
		
		'+Validacion de la fecha del contrato
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1103)
		End If
		
		'+Si se trata del campo que indica el código del contrato
		If nNumber = 0 Or nNumber = eRemoteDB.Constants.intNull Then
			'+Se valida que el código del contrato este lleno
			Call lclsErrors.ErrorMessage(sCodispl, 6015)
		End If
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			'+Validacion del tipo del contrato
			If nContraType = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6018)
			End If
			'+Validacion del ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1022)
			End If
		End If
		
		'+Si se trata del campo que indica el código del contrato
		If nNumber <> eRemoteDB.Constants.intNull And nNumber <> 0 Then
			'+Si la acción es consulta se valida que el contrato este en el archivo de contratos
			If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
				If Not lclsContrmaster.Find(Me.lintType_rel, nNumber, 0, 0, eRemoteDB.Constants.dtmNull) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6019)
				Else
					Me.nCurrency = lclsContrmaster.CodeCurrency
					If Not Find(nNumber, 0, 0, dEffecdate) Then
						Call lclsErrors.ErrorMessage(sCodispl, 6019)
					End If
				End If
			End If
			
			'+Si la acción es registrar se valida que el contrato no este en el archivo de contratos
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				If lclsContrmaster.Find_Type(Me.lintType_rel, nContraType, dEffecdate, nBranch) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6092)
				End If
				
				If lclsContrmaster.Find_Num(nNumber) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6023)
				End If
				
				'+Si la acción es registrar y el tipo de contrato no es retención
				'+se verifica que exista la retención del mismo
				
				If nContraType <> 1 Then
					If Not Find(0, 1, nBranch, dEffecdate) Then
						Call lclsErrors.ErrorMessage(sCodispl, 6103)
					Else
						lintReten = Me.nNumber
						Me.dblRetention = Me.nAmount
					End If
					'+Se busca el código de la moneda definido en la retención del contrato
					If lclsContrmaster.Find(Me.lintType_rel, lintReten, 1, nBranch, dEffecdate) Then
						Me.nCurrency = lclsContrmaster.CodeCurrency
					End If
				End If
			End If
		End If
		
		'+Si se trata del campo que indica el código del contrato
		If nNumber <> eRemoteDB.Constants.intNull And nNumber <> 0 Then
			'+Si la acción es Modificar se valida que el contrato este en el archivo de contratos
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				'+Si el Ramo o el tipo
				If nType = eRemoteDB.Constants.intNull Or nBranch = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6019)
				Else
					If Not lclsContrmaster.Find(Me.lintType_rel, nNumber, 0, 0, eRemoteDB.Constants.dtmNull) Then
						Call lclsErrors.ErrorMessage(sCodispl, 6019)
					Else
						Me.nCurrency = lclsContrmaster.CodeCurrency
						If Not Find(nNumber, 0, 0, dEffecdate) Then
							Call lclsErrors.ErrorMessage(sCodispl, 6019)
						End If
					End If
				End If
			End If
		End If
		
		'+Si la acción es modificar se valida que la fecha de modificación sea mayor o igual a la de última
		'+modificación
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(dEffecdate) Then
				If insValLastModify(nNumber, nContraType, nBranch) Then
					If Format(Me.LastModifyDate, "yyyyMMdd") > Format(dEffecdate, "yyyyMMdd") Then
						Call lclsErrors.ErrorMessage(sCodispl, 1021,  , eFunctions.Errors.TextAlign.RigthAling, CStr(LastModifyDate))
					End If
				End If
			End If
		End If
		
		
		insValCR301_k = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrmaster = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR301_k_Err: 
		If Err.Number Then
			insValCR301_k = insValCR301_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	'%insPreCR301: Esta función se encarga de realizar la habilitación o inhabilitación de los
	'%campos de la ventana CR301.
	Public Function insPreCR301(ByVal nAction As Integer, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		Dim lclsReinsuran As eCoReinsuran.Reinsuran
		Dim blnCurrency As Boolean
		
		On Error GoTo insPreCR301_Err
		
		insPreCR301 = True
		blnCurrency = True
		
		Call Find(nNumber, nContraType, nBranch, dEffecdate, True)
		
		If nContraType = 1 Then
			Me.nRetention = Me.nAmount
			Me.nAmount = 0
			Me.pcurRetention = Me.nRetention
		Else
			Me.nRetention = Me.dblRetention
			Me.nAmount = Me.nAmount
			Me.pcurRetention = Me.nRetention
		End If
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				If nContraType = 1 Then
					lclsContrmaster = New eCoReinsuran.Contrmaster
					
					If lclsContrmaster.Find_Type(1, nContraType, dEffecdate, nBranch) Then
						blnCurrency = False
					Else
						lclsReinsuran = New eCoReinsuran.Reinsuran
						
						If lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
							blnCurrency = False
						End If
						
						'UPGRADE_NOTE: Object lclsReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsReinsuran = Nothing
					End If
					
					'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsContrmaster = Nothing
				End If
		End Select
		
insPreCR301_Err: 
		If Err.Number Then
			insPreCR301 = False
		End If
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+Se indica que el tipo de contratos a procesar son los proporcionales
		lintType_rel = 1
		
		'+Se indica que el código de la moneda por defecto
		'  nCurrency = 1
		
		dblRetention = 0
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%insValCR301:En esta funcion se realizan las validaciones correspondientes a la forma CR301 (Folder).
	Public Function insValCR301(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCurrency As Integer, ByVal nReten As Double, ByVal nContraType As Integer, ByVal nAmount As Double, ByVal nLines As Double, ByVal nRet_min As Double, ByVal nQuota_sha As Double, ByVal nMax_even As Double, ByVal nLimCover As Integer, ByVal nRetCover As Integer, ByVal nRetZone As Integer, ByVal dStartdate As Date, ByVal dExpirdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR301_Err
		
		'+Se valida la fecha fin de vigencia del contrato
		If dExpirdate <> eRemoteDB.Constants.dtmNull Then
			If dExpirdate < dStartdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 2795)
			End If
		End If
		
		'+Si se trata de un contrato tipo retención se validan ciertos campos
		
		If nContraType = 1 Then
			'+Se realiza la validación del campo moneda
			If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1351)
			End If
			
			'+Se realiza la validación del campo retención
			If nRetZone = eRemoteDB.Constants.intNull And nRetCover = eRemoteDB.Constants.intNull Then
				If nReten = 0 Or nReten = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6110)
				End If
			End If
		End If
		
		'+Si se trata de un contrato tipo cuota parte se validan ciertos campos
		
		If nContraType = 2 Or nContraType = 3 Then
			
			'+Se realiza la validación del campo % cedido
			If nLimCover = eRemoteDB.Constants.intNull Then
				If nQuota_sha = 0 Or nQuota_sha = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6008)
				End If
			End If
			
			'+Se realiza la validación del campo importe límite
			If nLimCover = eRemoteDB.Constants.intNull And sRetcover = "2" And sRetzone = "2" Then
				If nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6020,  ,  , "- Importe límite")
				End If
			End If
			
		End If
		'+Si se trata de un contrato tipo excedente se validan ciertos campos
		
		If nContraType = 5 Or nContraType = 6 Or nContraType = 7 Or nContraType = 8 Then
			
			'+Se realiza la validación del campo excedente plenos
			If nLimCover = eRemoteDB.Constants.intNull Then
				If nLines = 0 Or nLines = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6007)
				End If
			End If
		End If
		
		'+Si se trata de un contrato tipo facultativo se validan ciertos campos
		
		If nContraType = 9 Or nContraType = 10 Then
			
			'+Se realiza la validación del campo retención mínima
			If nLimCover = eRemoteDB.Constants.intNull Then
				If nRet_min = 0 Or nRet_min = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6020,  ,  , "- Retención mínima")
				End If
			End If
			
			'+Se realiza la validación del campo límite máximo
			If nLimCover = eRemoteDB.Constants.intNull Then
				If nMax_even = 0 Or nMax_even = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 6020,  ,  , "- Límite máximo")
				End If
			End If
		End If
		
		insValCR301 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR301_Err: 
		If Err.Number Then
			insValCR301 = insValCR301 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostCR301: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
    Public Function insPostCR301(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nCurrency As Integer, ByVal nReten As Double, ByVal nAmount As Double, ByVal nLines As Double, ByVal nReten_min As Double, ByVal nQuota_sha As Double, ByVal nMax_even As Double, ByVal dExpirdate As Date, ByVal sRetcover As String, ByVal sRetzone As String, ByVal sLimitCov As String, ByVal sCumultyp As String, ByVal sCumreint As String, ByVal sCumulpol As String, ByVal nInterest As Double, ByVal nMaxRetAmount As Double) As Boolean
        Dim lclsContrmaster As eCoReinsuran.Contrmaster
        Dim lclsRetentioncov As eCoReinsuran.Retentioncov
        Dim lclsReinsuran As eCoReinsuran.Reinsuran
        Dim lclsRetentionzone As eCoReinsuran.Retentionzone
        Dim lclsContr_limCov As eCoReinsuran.Contr_LimCov
        Dim lclsContr_cumul As Contr_Cumul
        Dim lcolContr_cumuls As Contr_Cumuls

        lclsContrmaster = New eCoReinsuran.Contrmaster
        lclsRetentioncov = New eCoReinsuran.Retentioncov
        lclsReinsuran = New eCoReinsuran.Reinsuran
        lclsRetentionzone = New eCoReinsuran.Retentionzone
        lclsContr_limCov = New eCoReinsuran.Contr_LimCov
        lclsContr_cumul = New Contr_Cumul
        lcolContr_cumuls = New Contr_Cumuls

        On Error GoTo insPostCR301_Err

        insPostCR301 = True

        '+Si la opción seleccionada es diferente a Consultar
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then

            With Me
                If Not .Find(nNumber, nContraType, nBranch, dEffecdate, True) Then
                    .nFqcy_acc = eRemoteDB.Constants.intNull
                End If
                .nRetention = nReten
                .nCurrency = nCurrency
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nNumber = nNumber
                .nInterest = nInterest
                .nType = nContraType
                .nAmount = IIf(nAmount = eRemoteDB.Constants.intNull, 0, nAmount)
                .nLines = IIf(nLines = eRemoteDB.Constants.intNull, 0, nLines)
                .nMax_even = IIf(nMax_even = eRemoteDB.Constants.intNull, 0, nMax_even)
                .nReten_min = IIf(nReten_min = eRemoteDB.Constants.intNull, 0, nReten_min)
                .nQuota_sha = IIf(nQuota_sha = eRemoteDB.Constants.intNull, 0, nQuota_sha)
                .nUsercode = nUsercode
                .nCurrency = nCurrency
                .nMaxRetAmount = nMaxRetAmount

            End With

            If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                If Me.sRetcover = "1" And sRetcover = String.Empty Then
                    lclsRetentioncov.DelNullRetentioncov(nNumber, dEffecdate, nUsercode, 1)
                End If

                If Me.sRetzone = "1" And sRetzone = String.Empty Then
                    lclsRetentionzone.DelNullRetentionzone(nNumber, dEffecdate, nUsercode, 1)
                End If

                If Me.sLimitCov = "1" And sLimitCov = String.Empty Then
                    lclsContr_limCov.DelNullContr_limCov(nNumber, dEffecdate, nUsercode, 1)
                End If

                If Me.sCumulpol = "3" And Trim(sCumulpol) = "1" Then
                    lclsContr_cumul.DelNullContr_Cumul(nNumber, dEffecdate, nUsercode, 1)
                End If
            Else
                If Me.sRetcover = "1" And sRetcover = String.Empty Then
                    With lclsRetentioncov
                        If Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
                            .DelNullRetentioncov(nNumber, dEffecdate, nUsercode, 3)
                        Else
                            .DelNullRetentioncov(nNumber, dEffecdate, nUsercode, 2)
                        End If
                    End With
                End If
                If Me.sRetzone = "1" And sRetzone = String.Empty Then
                    If Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
                        lclsRetentionzone.DelNullRetentionzone(nNumber, dEffecdate, nUsercode, 3)
                    Else
                        lclsRetentionzone.DelNullRetentionzone(nNumber, dEffecdate, nUsercode, 2)
                    End If
                End If
                If Me.sLimitCov = "1" And sLimitCov = String.Empty Then
                    With lclsContr_limCov
                        If Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
                            .DelNullContr_limCov(nNumber, dEffecdate, nUsercode, 3)
                        Else
                            .DelNullContr_limCov(nNumber, dEffecdate, nUsercode, 2)
                        End If
                    End With
                End If
                If Me.sCumulpol = "3" And Trim(sCumulpol) = "1" Then
                    With lclsContr_cumul
                        If Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
                            .DelNullContr_Cumul(nNumber, dEffecdate, nUsercode, 3)
                        Else
                            .DelNullContr_Cumul(nNumber, dEffecdate, nUsercode, 2)
                        End If
                    End With
                End If
            End If

            With lclsContrmaster
                .nType_rel = 1
                .nCurrency = nCurrency
                .nBranch = nBranch
                .nNumber = nNumber
                .dExpirdate = dExpirdate
                .nType = nContraType
                .nUsercode = nUsercode

                Me.sRetcover = IIf(sRetcover = String.Empty, "2", sRetcover)
                Me.sLimitCov = IIf(sLimitCov = String.Empty, "2", sLimitCov)
                Me.sCumulpol = IIf(sCumultyp = "4", String.Empty, sCumulpol)
                Me.sRetzone = IIf(sRetzone = String.Empty, "2", sRetzone)
                Me.sCumultyp = IIf(Trim(sCumultyp) = "0", String.Empty, sCumultyp)
                Me.sCumreint = IIf(Trim(sCumreint) = "0", String.Empty, sCumreint)

                insPostCR301 = insContrProc("CR301")

                insPostCR301 = .updContrMasterExpirdat(nNumber, dExpirdate, nUsercode)

                If nCurrency <> 0 Or nCurrency <> eRemoteDB.Constants.intNull Then
                    insPostCR301 = .updContrMasterCurrency()
                End If
            End With
        End If
        'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrmaster = Nothing

insPostCR301_Err:
        If Err.Number Then
            insPostCR301 = False
        End If
    End Function
	'%insValCR302:En esta funcion se realizan las validaciones correspondientes al la forma CR302.
	Public Function insValCR302(ByVal sCodispl As String, ByVal nFixed_prat As Double, ByVal nGroup_co As Integer, ByVal nTab_commi As Integer, ByVal nPrem_dep As Double, ByVal nFact_reser As Double, ByVal nInt_prem As Double, ByVal sReser_clai As String, ByVal nInt_claim As Double, ByVal nFqcy_acc As Integer, ByVal sCessprcov As String, ByVal sCesscia As String, ByVal nRate As Double, ByVal nCessprfix As Double, ByVal sCommCov As String, ByVal nCurr_pay As Integer, ByVal nFreqpay As Integer, ByVal sInd_age As String, ByVal nInd_Age As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCR302_Err
		
		lclsErrors = New eFunctions.Errors
		'+se valida cesión por cobertura
		If (nRate = eRemoteDB.Constants.intNull Or nRate = 0) And (nCessprfix = eRemoteDB.Constants.intNull Or nCessprfix = 0) And (sCessprcov = String.Empty) And (sCesscia = String.Empty) And (sInd_age = String.Empty) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60318)
		End If
		
		'+Se realiza la validación del campo para indicar rutina de cálculo por edad del asegurado
		If sInd_age <> String.Empty Then
			If (nInd_Age = 0 Or nInd_Age = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 300017)
			End If
		End If
		
		
		'+Se realiza la validación del campo reserva de siniestros-%Interés
		If sReser_clai = "1" Then
			If nInt_claim = 0 Or nInt_claim = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6098)
			End If
			
			'+ Se valida la moneda de pago
			If nCurr_pay = eRemoteDB.Constants.intNull Or nCurr_pay = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1351)
			End If
		End If
		
		'+ Se realiza la validación la Periodicidad
		
		If nFqcy_acc <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6113)
		End If
		
		'+ se valida ña frecuencia
		
		If nFreqpay <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60320)
		End If
		
		insValCR302 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		
insValCR302_Err: 
		If Err.Number Then
			insValCR302 = insValCR302 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostCR302: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
	Public Function insPostCR302(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nFixed_prat As Double, ByVal nGroup_co As Integer, ByVal nTab_commi As Integer, ByVal nPrem_dep As Double, ByVal nFact_reser As Double, ByVal nInt_prem As Double, ByVal sReser_clai As String, ByVal nInt_claim As Double, ByVal nFqcy_acc As Integer, ByVal sCap_nom_ri As String, ByVal sCessprcov As String, ByVal sCesscia As String, ByVal nRate As Double, ByVal nCessprfix As Double, ByVal sExtraprem As String, ByVal sGencess As String, ByVal sCommCov As String, ByVal nNextmonthc As Integer, ByVal nNextyearc As Integer, ByVal nFreqpay As Integer, ByVal nNextmonthp As Integer, ByVal nNextyearp As Integer, ByVal nCurr_pay As Integer, ByVal sFormpay As String, ByVal nRetenMin As Double, ByVal sInd_age As String, ByVal nInd_Age As Integer) As Boolean
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclscontr_cescov As eCoReinsuran.contr_cescov
		Dim lobjContr_Cescovs As eCoReinsuran.contr_cescovs
		Dim lclsContr_comm As eCoReinsuran.Contr_comm
		Dim lobjcontr_comms As eCoReinsuran.Contr_comms
		Dim lclsReinsuran As eCoReinsuran.Reinsuran
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		Dim NewMonth As Integer
		Dim NewYear As Integer
		
		On Error GoTo insPostCR302_Err
		
		lclsContrproc = New eCoReinsuran.Contrproc
		
		insPostCR302 = True
		
		'+ se busca en los registros de contrproc del numero de contrato asociado
		Call lclsContrproc.Find(nNumber, nType, nBranch, dEffecdate)
		
		'+ Si la accion es registrar se eliminan los registros asociados al contrato
		'+ existentes en cesscov y no se seleciona la opcion en la pagina
		If lclsContrproc.sCessprcov = "1" And sCessprcov <> "1" Then
			
			lobjContr_Cescovs = New eCoReinsuran.contr_cescovs
			lclscontr_cescov = New eCoReinsuran.contr_cescov
			
			'+ si la accion es registrar
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				Call lobjContr_Cescovs.Find(nNumber, nBranch, nType, dEffecdate)
				For	Each lclscontr_cescov In lobjContr_Cescovs
					lclscontr_cescov.Delete()
				Next lclscontr_cescov
			Else
				'+ si la accion es Actualizar
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					lclsReinsuran = New eCoReinsuran.Reinsuran
					
					If Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
						Call lobjContr_Cescovs.Find(nNumber, nBranch, nType, dEffecdate)
						For	Each lclscontr_cescov In lobjContr_Cescovs
							If lclscontr_cescov.dEffecdate < dEffecdate Then
								lclscontr_cescov.Delete()
							Else
								lclscontr_cescov.Annulment()
							End If
						Next lclscontr_cescov
					Else
						Call lobjContr_Cescovs.Find(nNumber, nBranch, nType, dEffecdate)
						For	Each lclscontr_cescov In lobjContr_Cescovs
							lclscontr_cescov.Annulment()
						Next lclscontr_cescov
					End If
					
					'UPGRADE_NOTE: Object lclsReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReinsuran = Nothing
				End If
			End If
			
			'UPGRADE_NOTE: Object lclscontr_cescov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclscontr_cescov = Nothing
			'UPGRADE_NOTE: Object lobjContr_Cescovs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjContr_Cescovs = Nothing
		End If
		
		'+Si Comisión por cobertura esta encendida  y el usuaro no lo seleciona
		If lclsContrproc.sCommCov = "1" And sCommCov <> "1" Then
			
			lclscontr_cescov = New eCoReinsuran.contr_cescov
			lclsContr_comm = New eCoReinsuran.Contr_comm
			lobjcontr_comms = New eCoReinsuran.Contr_comms
			
			'+ si la accion es registrar
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				Call lobjcontr_comms.Find(nNumber, nBranch, nContraType, dEffecdate)
				For	Each lclsContr_comm In lobjcontr_comms
					lclscontr_cescov.Delete()
				Next lclsContr_comm
			End If
			
			lclsReinsuran = New eCoReinsuran.Reinsuran
			
			'+ si la accion es Actualizar
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And Not lclsReinsuran.FindReinsuPolicy(nNumber, dEffecdate, nBranch) Then
				
				Call lobjcontr_comms.Find(nNumber, nBranch, nContraType, dEffecdate)
				For	Each lclsContr_comm In lobjcontr_comms
					If lclsContr_comm.dEffecdate = dEffecdate Then
						lclsContr_comm.Delete()
					Else
						lclsContr_comm.dNulldate = dEffecdate
						lclsContr_comm.Annulment()
					End If
				Next lclsContr_comm
			Else
				Call lobjcontr_comms.Find(nNumber, nBranch, nContraType, dEffecdate)
				For	Each lclsContr_comm In lobjcontr_comms
					lclsContr_comm.dNulldate = dEffecdate
					lclsContr_comm.Annulment()
				Next lclsContr_comm
			End If
			
			'UPGRADE_NOTE: Object lclsContr_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsContr_comm = Nothing
			'UPGRADE_NOTE: Object lobjcontr_comms may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjcontr_comms = Nothing
			'UPGRADE_NOTE: Object lclscontr_cescov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclscontr_cescov = Nothing
			'UPGRADE_NOTE: Object lclsReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsReinsuran = Nothing
		End If
		
		'+Si la opción seleccionada es Consultar
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			
			With Me
				Call .Find(nNumber, nContraType, nBranch, dEffecdate, True)
				.dEffecdate = dEffecdate
				.nBranch = nBranch
				.nNumber = nNumber
				.nType = nContraType
				.nFixed_prat = nFixed_prat
				.nGroup_co = nGroup_co
				.nTab_commi = nTab_commi
				.nPrem_dep = nPrem_dep
				.nFact_reser = nFact_reser
				.nInt_prem = nInt_prem
				.sReser_clai = IIf(sReser_clai = "1", "1", "2")
				.nInt_claim = nInt_claim
				.nUsercode = nUsercode
				.sCap_nom_ri = sCap_nom_ri
				.sCesscia = IIf(sCesscia = "1", "1", "2")
				.sCessprcov = IIf(sCessprcov = "1", "1", "2")
				.nRate = nRate
				.nCessprfix = nCessprfix
				.sExtraprem = IIf(sExtraprem = "1", "1", "2")
				.sGencess = IIf(sGencess = "1", "1", "2")
				.sCommCov = IIf(sCommCov = "1", "1", "2")
				.nReten_min = nRetenMin
				.nInd_Age = IIf(sInd_age = "1", nInd_Age, eRemoteDB.Constants.intNull)
				
				
				'+ Se calcula el mes y el año de la próxima generación de cuenta técnica según pa periodicidad indicada
				Select Case nAction
					'+ Si la acción es registrar:
					Case 301
						NewYear = Year(dEffecdate)
						Select Case nFqcy_acc
							Case 1
								NewMonth = Month(dEffecdate) + 12
							Case 2
								NewMonth = Month(dEffecdate) + 6
							Case 3
								NewMonth = Month(dEffecdate) + 3
							Case 4
								NewMonth = Month(dEffecdate) + 1
						End Select
						If NewMonth > 12 Then
							NewYear = NewYear + 1
							NewMonth = NewMonth - 12
						End If
						.nNextmonthc = NewMonth
						.nNextyearc = NewYear
						'+ Si la acción es actualizar y el usuario cambió la periodicidad:
					Case 302
						NewYear = Year(dEffecdate)
						If .nFqcy_acc <> nFqcy_acc Then
							Select Case nFqcy_acc
								Case 1
									NewMonth = Month(dEffecdate) + 12
								Case 2
									NewMonth = Month(dEffecdate) + 6
								Case 3
									NewMonth = Month(dEffecdate) + 3
								Case 4
									NewMonth = Month(dEffecdate) + 1
							End Select
							If NewMonth > 12 Then
								NewYear = NewYear + 1
								NewMonth = NewMonth - 12
							End If
							.nNextmonthc = NewMonth
							.nNextyearc = NewYear
						End If
				End Select
				'+ Se calcula el mes y el año de la próxima generación de orden de pago según pa periodicidad indicada
				Select Case nAction
					'+ Si la acción es registrar:
					Case 301
						NewYear = Year(dEffecdate)
						Select Case nFreqpay
							Case 1
								NewMonth = Month(dEffecdate) + 12
							Case 2
								NewMonth = Month(dEffecdate) + 6
							Case 3
								NewMonth = Month(dEffecdate) + 3
							Case 4
								NewMonth = Month(dEffecdate) + 1
						End Select
						If NewMonth > 12 Then
							NewYear = NewYear + 1
							NewMonth = NewMonth - 12
						End If
						.nNextmonthpa = NewMonth
						.nNextyearpa = NewYear
						'+ Si la acción es actualizar y el usuario cambió la periodicidad:
					Case 302
						If .nFreqpay <> nFreqpay Then
							NewYear = Year(dEffecdate)
							Select Case nFreqpay
								Case 1
									NewMonth = Month(dEffecdate) + 12
								Case 2
									NewMonth = Month(dEffecdate) + 6
								Case 3
									NewMonth = Month(dEffecdate) + 3
								Case 4
									NewMonth = Month(dEffecdate) + 1
							End Select
							If NewMonth > 12 Then
								NewYear = NewYear + 1
								NewMonth = NewMonth - 12
							End If
							.nNextmonthp = NewMonth
							.nNextyearp = NewYear
						End If
				End Select
				.nFqcy_acc = nFqcy_acc
				.nFreqpay = nFreqpay
			End With
			
			insPostCR302 = Me.insContrProc("CR302")
			
			lclsContrmaster = New eCoReinsuran.Contrmaster
			
			With lclsContrmaster
				Call .Find(1, nNumber, nType, nBranch, dEffecdate)
				.dStartdate = dEffecdate
				.nCurr_pay = nCurr_pay
				.sFormpay = sFormpay
				.insUpdcontrmaster(2)
			End With
		End If
		
insPostCR302_Err: 
		If Err.Number Then
			insPostCR302 = False
		End If
		
		'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrproc = Nothing
		'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrmaster = Nothing
		
		On Error GoTo 0
	End Function
	
	'%insreaBrancht: Permite saber si el ramo con el que se está trabajando es de vida
	Public Function insreaBrancht(ByVal nBranch As Integer) As Boolean
		Dim lrecreaProdMaster_t As eRemoteDB.Execute
		
		lrecreaProdMaster_t = New eRemoteDB.Execute
		
		On Error GoTo insreaBrancht_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProdMaster_t'
		'+ Información leída el 03/07/2000 01:36:30 PM
		
		With lrecreaProdMaster_t
			.StoredProcedure = "reaProdMaster_t"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sBrancht = .FieldToClass("sBrancht")
				insreaBrancht = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProdMaster_t may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProdMaster_t = Nothing
		
insreaBrancht_Err: 
		If Err.Number Then
			insreaBrancht = False
		End If
	End Function
	
	'%insValCR303:En esta función se realizan las validaciones correspondientes a la forma CR303 (Header).
	Public Function insValCR303(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nYear_begin As Integer, ByVal nTran_prem As Double, ByVal nRate_claim As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		
		lclsErrors = New eFunctions.Errors
		lclsContrmaster = New eCoReinsuran.Contrmaster
		
		
		On Error GoTo insValCR303_Err
		
		'+Se realiza la validación del campo participación de beneficios-Año de comienzo
		
		If nYear_begin <> 0 And nYear_begin <> eRemoteDB.Constants.intNull Then
			If lclsContrmaster.Find(lintType_rel, nNumber, 0, 0, eRemoteDB.Constants.dtmNull) Then
				If nYear_begin < Year(lclsContrmaster.dStartdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6022)
				End If
			End If
		End If
		
		'+Se realiza la validación del campo traspaso de cartera
		
		If ((nTran_prem <> 0 And nTran_prem <> eRemoteDB.Constants.intNull) And (nRate_claim = 0 Or nRate_claim = eRemoteDB.Constants.intNull)) Or ((nTran_prem = 0 Or nTran_prem = eRemoteDB.Constants.intNull) And (nRate_claim <> 0 And nRate_claim <> eRemoteDB.Constants.intNull)) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6106)
		End If
		
		insValCR303 = lclsErrors.Confirm
		
insValCR303_Err: 
		If Err.Number Then
			insValCR303 = insValCR303 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrmaster = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostCR303: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
	Public Function insPostCR303(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nYear_begin As Integer, ByVal nYear_end As Integer, ByVal nTran_prem As Double, ByVal nRate_claim As Double, ByVal nProfit_sh As Double, ByVal nGroup_bene As Integer, ByVal nExpenses As Double, ByVal nExcess As Double) As Boolean
		Dim lclsContrproc As eCoReinsuran.Contrproc
		
		lclsContrproc = New eCoReinsuran.Contrproc
		
		On Error GoTo insPostCR303_Err
		
		insPostCR303 = True
		
		'+Si la opción seleccionada es Consultar
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			
			With Me
				Call .Find(nNumber, nContraType, nBranch, dEffecdate, True)
				.dEffecdate = dEffecdate
				.nBranch = nBranch
				.nNumber = nNumber
				.nType = nContraType
				.nYear_begin = nYear_begin
				.nYear_end = nYear_end
				.nTran_prem = nTran_prem
				.nRate_claim = nRate_claim
				.nProfit_sh = nProfit_sh
				.nGroup_bene = nGroup_bene
				.nExpenses = nExpenses
				.nExcess = nExcess
				.nUsercode = nUsercode
				insPostCR303 = .insContrProc("CR303")
			End With
			
		End If
		
		'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrproc = Nothing
		
insPostCR303_Err: 
		If Err.Number Then
			insPostCR303 = False
		End If
	End Function
	
	'%insValLastModify: Se realiza la validación de la fecha de última modificación del contrato
	Public Function insValLastModify(ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer) As Boolean
		Dim lrecreaContrproc_effecdate As eRemoteDB.Execute
		Dim lclsValues As New eFunctions.Values
		
		lrecreaContrproc_effecdate = New eRemoteDB.Execute
		
		On Error GoTo insValLastModify_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaContrproc_effecdate'
		'+ Información leída el 07/06/2001 10:21:36 a.m.
		
		With lrecreaContrproc_effecdate
			.StoredProcedure = "reaContrproc_effecdate"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If Not IsNothing(.FieldToClass("LastDate")) Then
					mvarLastModify = .FieldToClass("LastDate")
					insValLastModify = True
				Else
					insValLastModify = False
				End If
			Else
				insValLastModify = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaContrproc_effecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrproc_effecdate = Nothing
		
insValLastModify_Err: 
		If Err.Number Then
			insValLastModify = False
		End If
	End Function

    Public Sub defaulValuesCR301(ByVal sCumulpol As String)
        If sCumulpol = "3" Then
            Me.nOptCumulpol_1 = 0
            Me.nOptCumulpol_2 = 0
            Me.nOptCumulpol_3 = 1
        Else
            If sCumulpol = "2" Then
                Me.nOptCumulpol_1 = 0
                Me.nOptCumulpol_2 = 1
                Me.nOptCumulpol_3 = 0
            Else
                Me.nOptCumulpol_1 = 1
                Me.nOptCumulpol_2 = 0
                Me.nOptCumulpol_3 = 0
            End If
        End If
    End Sub
    '%insContrProcHeader: Creación de un registro en el archivo de los contratos de reaseguro cuando se modifica
    Public Function insContrProcHeader(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsContrProcHeader As eRemoteDB.Execute
		
		lrecinsContrProcHeader = New eRemoteDB.Execute
		
		On Error GoTo insContrProcHeader_Err
		
		With lrecinsContrProcHeader
			.StoredProcedure = "insContrProcHeader"
			.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nContraType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insContrProcHeader = .Run(False)
		End With
		
insContrProcHeader_Err: 
		If Err.Number Then
			insContrProcHeader = False
		End If
	End Function
End Class






