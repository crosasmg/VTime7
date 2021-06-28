Option Strict Off
Option Explicit On
Public Class OptFinance
	'%-------------------------------------------------------%'
	'% $Workfile:: OptFinance.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	'-
	'- Estructura de tabla Opt_financ al 09-19-2002
	'-  Property                       Type         DBType   Size Scale  Prec  Null
	Public nOpt_draft As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_dra As String ' CHAR       1    0     0    S
	Public nLevel_dra As Integer ' NUMBER     22   0     5    S
	Public nOpt_null As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_nul As String ' CHAR       1    0     0    S
	Public nLevel_nul As Integer ' NUMBER     22   0     5    S
	Public nDefaulti As Double ' NUMBER     22   2     4    S
	Public sCh_up As String ' CHAR       1    0     0    S
	Public nInt_up As Double ' NUMBER     22   2     6    S
	Public sCh_down As String ' CHAR       1    0     0    S
	Public nInt_down As Double ' NUMBER     22   2     6    S
	Public nLevel_fin As Integer ' NUMBER     22   0     5    S
	Public sOpt_intere As String ' CHAR       1    0     0    S
	Public sCh_opt_int As String ' CHAR       1    0     0    S
	Public nLevel_initial As Integer ' NUMBER     22   0     5    S
	Public sInterest_e As String ' CHAR       1    0     0    S
	Public sTime_exa As String ' CHAR       1    0     0    S
	Public nIntdelay As Double ' NUMBER     22   2     4    S
	Public sCh_del_up As String ' CHAR       1    0     0    S
	Public nInt_del_up As Double ' NUMBER     22   2     6    S
	Public sCh_del_down As String ' CHAR       1    0     0    S
	Public nInt_del_down As Double ' NUMBER     22   2     6    S
	Public nLevel_delay As Integer ' NUMBER     22   0     5    S
	Public nOpt_comm As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_com As String ' CHAR       1    0     0    S
	Public nLevel_comm As Integer ' NUMBER     22   0     5    S
	Public nDscto_pag As Double ' NUMBER     22   2     4    S
	Public nDscto_amo As Double ' NUMBER     22   2     10   S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public sCh_pay_up As String ' CHAR       1    0     0    S
	Public nPay_up As Double ' NUMBER     22   2     6    S
	Public sCh_pay_down As String ' CHAR       1    0     0    S
	Public nPay_down As Double ' NUMBER     22   2     6    S
	Public nLevel_pay As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'-Se define el tipo enumerado que indica el tiempo a utilizar "Exacto" ó "Aproximado".
	
	Public Enum eTime_exa
		etExact = 1
		etApproximate = 2
	End Enum
	
	'-Se define la variable para el estado de los permisos
	
	Public Enum PermissionState
		Affirmative = 1
		Negative = 2
	End Enum
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Opt_financ" devolviendo Verdadero o
	'%Falso, dependiendo de la existencia de los registros.
	Public Function Find() As Boolean
		Dim lrecreaOpt_financ As eRemoteDB.Execute
		
		lrecreaOpt_financ = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClient'
		'+ Información leída el 11/08/1999 09:10:00 PM
		
		With lrecreaOpt_financ
			.StoredProcedure = "reaOpt_financ"
			
			If .Run Then
				
				nOpt_draft = .FieldToClass("nOpt_draft")
				sCh_opt_dra = .FieldToClass("sCh_opt_dra")
				nLevel_dra = .FieldToClass("nLevel_dra")
				nOpt_null = .FieldToClass("nOpt_null")
				sCh_opt_nul = .FieldToClass("sCh_opt_nul")
				nLevel_nul = .FieldToClass("nLevel_nul")
				nDefaulti = .FieldToClass("nDefaulti")
				sCh_up = .FieldToClass("sCh_up")
				nInt_up = .FieldToClass("nInt_up")
				sCh_down = .FieldToClass("sCh_down")
				nInt_down = .FieldToClass("nInt_down")
				nLevel_fin = .FieldToClass("nLevel_fin")
				sOpt_intere = .FieldToClass("sOpt_intere")
				sCh_opt_int = .FieldToClass("sCh_opt_int")
				nLevel_initial = .FieldToClass("nLevel_Initial")
				sInterest_e = .FieldToClass("sInterest_e")
				sTime_exa = .FieldToClass("sTime_exa")
				nIntdelay = .FieldToClass("nIntDelay")
				sCh_del_up = .FieldToClass("sCh_del_up")
				nInt_del_up = .FieldToClass("nInt_del_up")
				sCh_del_down = .FieldToClass("sCh_del_down")
				nInt_del_down = .FieldToClass("nInt_del_down")
				nLevel_delay = .FieldToClass("nLevel_delay")
				nOpt_comm = .FieldToClass("nOpt_comm")
				sCh_opt_com = .FieldToClass("sCh_opt_com")
				nLevel_comm = .FieldToClass("nLevel_comm")
				nDscto_pag = .FieldToClass("nDscto_pag")
				nDscto_amo = .FieldToClass("nDscto_amo")
				nCurrency = .FieldToClass("nCurrency")
				sCh_pay_up = .FieldToClass("sCh_pay_up")
				nPay_up = .FieldToClass("nPay_up")
				sCh_pay_down = .FieldToClass("sCh_pay_down")
				nPay_down = .FieldToClass("nPay_down")
				nLevel_pay = .FieldToClass("nLevel_pay")
				
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaOpt_financ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_financ = Nothing
	End Function
	'insValMFI023 : Se valida valores para la tabla de opciones de financiamiento
	Public Function insValMFI023(ByVal nDefaulti As Double, ByVal nDscto_amo As Double, ByVal nCurrency As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValMFI023_Err
		
		lobjErrors = New eFunctions.Errors
		
		insValMFI023 = String.Empty
		
		With lobjErrors
			
			'+ Se valida que el porcentaje de interés sea ditinto de null.
			
			If nDefaulti = eRemoteDB.Constants.intNull Then
				.ErrorMessage("MFI023", 21008,  , eFunctions.Errors.TextAlign.LeftAling)
			End If
			
			'+ Si la forma de cálculo corresponde a "% fijo" debe estar lleno el interés de mora
			
			If nDscto_amo <> eRemoteDB.Constants.intNull Then
				If nCurrency = eRemoteDB.Constants.intNull Then
					.ErrorMessage("MCO001", 1351,  , eFunctions.Errors.TextAlign.LeftAling)
				End If
			End If
			
			insValMFI023 = .Confirm
			
		End With
		
insValMFI023_Err: 
		If Err.Number Then
			insValMFI023 = "insValMFI023: " & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
	End Function
	'%insPostMMCO001: realiza las actualizaciones pertinentes luego de aceptar la ventana MCO001
	Public Function insPostMFI023(ByVal nOptdraft As Integer, ByVal sChopt_dra As String, ByVal nLeveldra As Integer, ByVal nOptnull As Integer, ByVal sChopt_nul As String, ByVal nLevelnul As Integer, ByVal nDefaulti As Double, ByVal sChup As String, ByVal nIntup As Double, ByVal sChdown As String, ByVal nIntdown As Double, ByVal nLevelfin As Integer, ByVal sOptintere As String, ByVal sChopt_int As String, ByVal nLevelinitial As Integer, ByVal sIntereste As String, ByVal sTimeexa As String, ByVal nIntdela As Double, ByVal sChdel_up As String, ByVal nIntdel_up As Double, ByVal sChdel_down As String, ByVal nIntdel_down As Double, ByVal nLeveldelay As Integer, ByVal nOptcomm As Integer, ByVal sChopt_com As String, ByVal nLevelcomm As Integer, ByVal nDsctopag As Double, ByVal nDsctoamo As Double, ByVal nCurren As Integer, ByVal sChpay_up As String, ByVal nPayup As Double, ByVal sChpay_down As String, ByVal nPaydown As Double, ByVal nLevelpay As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsOpt_Finance As eRemoteDB.Execute
		
		On Error GoTo insPostMFI023_Err
		
		lrecinsOpt_Finance = New eRemoteDB.Execute
		
		With Me
			
			If nOptdraft <> eRemoteDB.Constants.intNull Then
				.nOpt_draft = nOptdraft
			End If
			
			.sCh_opt_dra = IIf(sChopt_dra <> String.Empty, "1", "2")
			
			If nLeveldra <> eRemoteDB.Constants.intNull Then
				.nLevel_dra = nLeveldra
			End If
			
			If nOptnull <> eRemoteDB.Constants.intNull Then
				.nOpt_null = nOptnull
			End If
			
			.sCh_opt_nul = IIf(sChopt_nul <> String.Empty, "1", "2")
			
			If nLevelnul <> eRemoteDB.Constants.intNull Then
				.nLevel_nul = nLevelnul
			End If
			
			If nDefaulti <> eRemoteDB.Constants.intNull Then
				.nDefaulti = nDefaulti
			End If
			
			.sCh_up = IIf(sChup <> String.Empty, "1", "2")
			
			If nIntup <> eRemoteDB.Constants.intNull Then
				.nInt_up = nIntup
			End If
			
			.sCh_down = IIf(sChdown <> String.Empty, "1", "2")
			
			If nIntdown <> eRemoteDB.Constants.intNull Then
				.nInt_down = nIntdown
			End If
			
			If nLevelfin <> eRemoteDB.Constants.intNull Then
				.nLevel_fin = nLevelfin
			End If
			
			.sOpt_intere = IIf(sOptintere <> String.Empty, "1", "2")
			.sCh_opt_int = IIf(sChopt_int <> String.Empty, "1", "2")
			
			If nLevelinitial <> eRemoteDB.Constants.intNull Then
				.nLevel_initial = nLevelinitial
			End If
			
			.sInterest_e = sIntereste
			.sTime_exa = sTimeexa
			
			If nIntdela <> eRemoteDB.Constants.intNull Then
				.nIntdelay = nIntdela
			End If
			
			.sCh_del_up = IIf(sChdel_up <> String.Empty, "1", "2")
			
			If nIntdel_up <> eRemoteDB.Constants.intNull Then
				.nInt_del_up = nIntdel_up
			End If
			
			.sCh_del_down = IIf(sChdel_down <> String.Empty, "1", "2")
			
			If nIntdel_down <> eRemoteDB.Constants.intNull Then
				.nInt_del_down = nIntdel_down
			End If
			
			If nLeveldelay <> eRemoteDB.Constants.intNull Then
				.nLevel_delay = nLeveldelay
			End If
			
			.sCh_opt_com = IIf(sChopt_com <> String.Empty, "1", "2")
			
			If nOptcomm <> eRemoteDB.Constants.intNull Then
				.nOpt_comm = nOptcomm
			End If
			
			If nLevelcomm <> eRemoteDB.Constants.intNull Then
				.nLevel_comm = nLevelcomm
			End If
			
			If nDsctopag <> eRemoteDB.Constants.intNull Then
				.nDscto_pag = nDsctopag
			End If
			
			If nDsctoamo <> eRemoteDB.Constants.intNull Then
				.nDscto_amo = nDsctoamo
			End If
			
			If nCurren <> eRemoteDB.Constants.intNull Then
				.nCurrency = nCurren
			End If
			
			.sCh_pay_up = IIf(sChpay_up <> String.Empty, "1", "2")
			
			If nPayup <> eRemoteDB.Constants.intNull Then
				.nPay_up = nPayup
			End If
			
			.sCh_pay_down = IIf(sChpay_down <> String.Empty, "1", "2")
			
			If nPaydown <> eRemoteDB.Constants.intNull Then
				.nPay_down = nPaydown
			End If
			
			If nLevelpay <> eRemoteDB.Constants.intNull Then
				.nLevel_pay = nLevelpay
			End If
			
		End With
		
		'+ Definición de parámetros para stored procedure 'insOpt_Finance'
		
		With lrecinsOpt_Finance
			.StoredProcedure = "insOpt_Financ"
			.Parameters.Add("nopt_draft", Me.nOpt_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_opt_dra", Me.sCh_opt_dra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_dra", Me.nLevel_dra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nopt_null", Me.nOpt_null, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_opt_null", Me.sCh_opt_nul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_null", Me.nLevel_nul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ndefaulti", Me.nDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_up", Me.sCh_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nint_up", Me.nInt_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_down", Me.sCh_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nint_down", Me.nInt_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_fin", Me.nLevel_fin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sopt_intere", Me.sOpt_intere, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_opt_int", Me.sCh_opt_int, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_initial", Me.nLevel_initial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sinterest_e", Me.sInterest_e, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("stime_exa", Me.sTime_exa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nintdelay", Me.nIntdelay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_del_up", Me.sCh_del_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nint_del_up", Me.nInt_del_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_del_down", Me.sCh_del_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nint_del_down", Me.nInt_del_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_delay", Me.nLevel_delay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nopt_comm", Me.nOpt_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_opt_comm", Me.sCh_opt_com, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_comm", Me.nLevel_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ndscto_pag", Me.nDscto_pag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ndscto_amo", Me.nDscto_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncurrency", Me.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_pay_up", Me.sCh_pay_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npay_up", Me.nPay_up, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sch_pay_down", Me.sCh_pay_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npay_down", Me.nPay_down, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevel_pay", Me.nLevel_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostMFI023 = .Run(False)
		End With
		
insPostMFI023_Err: 
		If Err.Number Then
			insPostMFI023 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsOpt_Finance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpt_Finance = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nOpt_draft = eRemoteDB.Constants.intNull
		sCh_opt_dra = String.Empty
		nLevel_dra = eRemoteDB.Constants.intNull
		nOpt_null = eRemoteDB.Constants.intNull
		sCh_opt_nul = String.Empty
		nLevel_nul = eRemoteDB.Constants.intNull
		nDefaulti = eRemoteDB.Constants.intNull
		sCh_up = String.Empty
		nInt_up = eRemoteDB.Constants.intNull
		sCh_down = String.Empty
		nInt_down = eRemoteDB.Constants.intNull
		nLevel_fin = eRemoteDB.Constants.intNull
		sOpt_intere = String.Empty
		sCh_opt_int = String.Empty
		nLevel_initial = eRemoteDB.Constants.intNull
		sInterest_e = String.Empty
		sTime_exa = String.Empty
		nIntdelay = eRemoteDB.Constants.intNull
		sCh_del_up = String.Empty
		nInt_del_up = eRemoteDB.Constants.intNull
		sCh_del_down = String.Empty
		nInt_del_down = eRemoteDB.Constants.intNull
		nLevel_delay = eRemoteDB.Constants.intNull
		nOpt_comm = eRemoteDB.Constants.intNull
		sCh_opt_com = String.Empty
		nLevel_comm = eRemoteDB.Constants.intNull
		nDscto_pag = eRemoteDB.Constants.intNull
		nDscto_amo = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		sCh_pay_up = String.Empty
		nPay_up = eRemoteDB.Constants.intNull
		sCh_pay_down = String.Empty
		nPay_down = eRemoteDB.Constants.intNull
		nLevel_pay = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






