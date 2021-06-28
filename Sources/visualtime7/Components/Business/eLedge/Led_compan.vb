Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class Led_compan
	'%-------------------------------------------------------%'
	'% $Workfile:: Led_compan.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 28/12/1999.
	'+ (Clase importada de la versión Win32 el día 24/05/2001.)
	'+ El campo llave corresponde a: nLed_compan.
	
	'Column_name                      Type                 Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-------------------------------- -------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nLed_compan As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public sAccount_bg As String 'char     no       20                      yes      yes                yes
	Public sAccount_gp As String 'char     no       20                      yes      yes                yes
	Public sBal_actu As String 'char     no       1                       yes      yes                yes
	Public sClose_mont As String 'char     no       1                       yes      yes                yes
	Public sStatregt As String 'char     no       1                       yes      yes                yes
	Public sStruct_uni As String 'char     no       3                       yes      yes                yes
	Public sStructure As String 'char     no       7                       yes      yes                yes
	Public dCompan_dat As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public dDate_end As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public dDate_init As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public nVoucher As Integer 'int      no       4           10    0     yes      (n/a)              (n/a)
	Public nCurrency As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public nYear As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public dIniLedDat As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public dEndLedDat As Date 'datetime no       8                       yes      (n/a)              (n/a)
	
	
	'+ Variables auxiliares
	'+ Propiedades que toman el valor según se requiera de esos campos
	Public valtcdFromLedCompan As Date
	Public valtcdInitLedDate As Date
	Public vallblEndLedDate1 As Date
	Public valtcdFrom As Date
	Public vallblTo1 As Date
	
	'+ Propiedades que toman el valor de Disabled de los campos según se requiera en el insPreCP001
	Public EnachkCopy As Boolean
	Public EnatcdFromLedCompan As Boolean
	Public EnacbeCurrency As Boolean
	Public EnatcdInitLedDate As Boolean
	Public EnatcdFrom As Boolean
	Public EnagmnYear As Boolean
	Public EnagmnNum As Boolean
	Public EnachkUpdate As Boolean
	Public EnagmnCode0 As Boolean
	Public EnagmnCode1 As Boolean
	Public EnagmnCode2 As Boolean
	Public EnagmnCode3 As Boolean
	Public EnagmnCode4 As Boolean
	Public EnagmnCode5 As Boolean
	Public EnagmnCode6 As Boolean
	Public EnagmtLossProfit As Boolean
	Public EnagmtGenBal As Boolean
	Public EnagmnUnit0 As Boolean
	Public EnagmnUnit1 As Boolean
	Public EnagmnUnit2 As Boolean
	Public GridFillblEndLedDate1 As Boolean
	Public GridFillblTo1 As Boolean
	
	
	'+ Variables auxiliares
	Public sClient As String
	
	'- Se define la variable para indicar el estado de cada instancia en la colección
	Public Enum eStatusInstance
		eftNew = 0
		eftQuery = 1
		eftExist = 1
		eftUpDate = 2
		eftDelete = 3
	End Enum
	
	'**+Define the variable that contein the description of the "Accounting Period" company
	'+ Se define la variable que contendrá la descripción de la compañía contable
	Public sDescript As String
	Public nStatusInstance As eStatusInstance
	Private lintLed_CompanAux As Integer
	Private mclsLed_compan As Led_compan
	Public nErrornum As Integer
	
	
	'% Find_Date_Init: Se lee la fecha del último proceso de Asientos Automáticos
	Public Function IsExist_LedCompan(ByVal nLedCompan As Integer) As Boolean
		
		Dim lrecreaLed_compan As eRemoteDB.Execute
		
		On Error GoTo IsExist_LedCompan_Err
		
		lrecreaLed_compan = New eRemoteDB.Execute
		
		IsExist_LedCompan = False
		
		With lrecreaLed_compan
			.StoredProcedure = "reaExist_LedCompan"
			.Parameters.Add("nLed_compan", nLedCompan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("nCount") > 0 Then
					IsExist_LedCompan = True
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLed_compan = Nothing
		
IsExist_LedCompan_Err: 
		If Err.Number Then
			IsExist_LedCompan = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Find_Date_Init: Se lee la fecha del último proceso de Asientos Automáticos
	Public Function Find_Date_Init(ByVal lintLedCompan As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLed_compan As eRemoteDB.Execute
		
		On Error GoTo Find_Date_Init_Err
		
		If nLed_compan = lintLedCompan And Not lblnFind Then
			Find_Date_Init = True
		Else
			lrecreaLed_compan = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaLed_compan'
			'+ Información leída el 30/06/1999 01:25:14 PM
			
			With lrecreaLed_compan
				.StoredProcedure = "reaLed_compan"
				.Parameters.Add("nLed_compan", lintLedCompan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nLed_compan = lintLedCompan
					Me.dDate_init = .FieldToClass("dDate_init", eRemoteDB.Constants.dtmNull)
					Find_Date_Init = True
					.RCloseRec()
				Else
					Find_Date_Init = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLed_compan = Nothing
		End If
		
Find_Date_Init_Err: 
		If Err.Number Then
			Find_Date_Init = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: Permite crear registros en la tabla de Compañías Contables
	Public Function Add() As Boolean
		
		Dim lreccreLed_compan As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreLed_compan = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creLed_compan'
		'+ Información leída el 01/09/2000 10:47:55 p.m.
		
		With lreccreLed_compan
			.StoredProcedure = "creLed_compan"
			.Parameters.Add("dCompan_dat", dCompan_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_init", dDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_gp", sAccount_gp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_bg", sAccount_bg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBal_actu", sBal_actu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClose_mont", sClose_mont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStruct_uni", sStruct_uni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStructure", sStructure, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 7, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIniLedDat", dIniLedDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndLedDat", dEndLedDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLed_compan = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: Permite buscar los datos de una compania contable
	'---------------------------------------------------------
	Public Function Find(ByVal nLed_compan As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'---------------------------------------------------------
		
		Static lblnRead As Boolean
		Dim lrecreaLed_compan As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaLed_compan = New eRemoteDB.Execute
		
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLed_compan'
		'+ Información leída el 01/09/2000 11:19:35 p.m.
		
		With lrecreaLed_compan
			.StoredProcedure = "reaLed_compan"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nLed_compan = .FieldToClass("nLed_compan")
				
				If .FieldToClass("sAccount_gp") <> String.Empty Then
					sAccount_gp = .FieldToClass("sAccount_gp")
				Else
					sAccount_gp = " "
				End If
				
				If .FieldToClass("sAccount_bg") <> String.Empty Then
					sAccount_bg = .FieldToClass("sAccount_bg")
				Else
					sAccount_bg = " "
				End If
				sBal_actu = .FieldToClass("sBal_actu")
				sClose_mont = .FieldToClass("sClose_mont")
				sStatregt = .FieldToClass("sStatregt")
				If .FieldToClass("sStruct_uni") = String.Empty Then
					sStruct_uni = "000"
				Else
					sStruct_uni = .FieldToClass("sStruct_uni")
				End If
				
				If .FieldToClass("sStructure") = String.Empty Then
					sStructure = "0000000"
				Else
					sStructure = .FieldToClass("sStructure")
				End If
				
				dCompan_dat = .FieldToClass("dCompan_dat")
				dDate_end = .FieldToClass("dDate_end")
				dDate_init = .FieldToClass("dDate_init")
				
				If .FieldToClass("nVoucher") <> eRemoteDB.Constants.intNull Then
					nVoucher = .FieldToClass("nVoucher")
				Else
					nVoucher = 0
				End If
				
				If .FieldToClass("nCurrency") <> eRemoteDB.Constants.intNull Then
					nCurrency = .FieldToClass("nCurrency")
				Else
					nCurrency = 0
				End If
				
				If .FieldToClass("nYear") <> eRemoteDB.Constants.intNull Then
					nYear = .FieldToClass("nYear")
				Else
					nYear = 0
				End If
				
				dIniLedDat = .FieldToClass("dIniLedDat")
				dEndLedDat = .FieldToClass("dEndLedDat")
				sClient = .FieldToClass("sClient")
				sDescript = .FieldToClass("sCliename")
				
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLed_compan = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "XXXXXX"
	Public Function Find_ActiveCut(ByVal lintLed_Compan As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaLed_companActiveCut As eRemoteDB.Execute
		
		On Error GoTo Find_ActiveCut_Err
		
		lrecreaLed_companActiveCut = New eRemoteDB.Execute
		
		If lintLed_CompanAux <> lintLed_Compan Or lblnFind Then
			
			lintLed_CompanAux = lintLed_Compan
			nLed_compan = lintLed_Compan
			
			'+ Definición de parámetros para stored procedure 'insudb.reaLed_companActiveCut'
			'+ Información leída el 11/09/2000 03:34:36 p.m.
			
			With lrecreaLed_companActiveCut
				.StoredProcedure = "reaLed_companActiveCut"
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					nLed_compan = .FieldToClass("nLed_compan")
					
					If .FieldToClass("sAccount_gp") <> String.Empty Then
						sAccount_gp = .FieldToClass("sAccount_gp")
					Else
						sAccount_gp = " "
					End If
					
					If .FieldToClass("sAccount_bg") <> String.Empty Then
						sAccount_bg = .FieldToClass("sAccount_bg")
					Else
						sAccount_bg = " "
					End If
					
					sBal_actu = .FieldToClass("sBal_actu")
					sClose_mont = .FieldToClass("sClose_mont")
					sStatregt = .FieldToClass("sStatregt")
					sStruct_uni = .FieldToClass("sStruct_uni")
					sStructure = .FieldToClass("sStructure")
					dCompan_dat = .FieldToClass("dCompan_dat")
					dDate_end = .FieldToClass("dDate_end")
					dDate_init = .FieldToClass("dDate_init")
					
					If .FieldToClass("nVoucher") <> eRemoteDB.Constants.intNull Then
						nVoucher = .FieldToClass("nVoucher")
					Else
						nVoucher = 0
					End If
					
					If .FieldToClass("nCurrency") <> eRemoteDB.Constants.intNull Then
						nCurrency = .FieldToClass("nCurrency")
					Else
						nCurrency = 0
					End If
					
					If .FieldToClass("nYear") <> eRemoteDB.Constants.intNull Then
						nYear = .FieldToClass("nYear")
					Else
						nYear = 0
					End If
					
					dIniLedDat = .FieldToClass("dIniLedDat")
					dEndLedDat = .FieldToClass("dEndLedDat")
					sClient = .FieldToClass("sClient")
					sDescript = .FieldToClass("sCliename")
					Find_ActiveCut = True
					.RCloseRec()
				Else
					Find_ActiveCut = False
					lintLed_CompanAux = 0
				End If
			End With
		Else
			Find_ActiveCut = True
		End If
		'UPGRADE_NOTE: Object lrecreaLed_companActiveCut may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLed_companActiveCut = Nothing
		
Find_ActiveCut_Err: 
		If Err.Number Then
			Find_ActiveCut = False
		End If
		On Error GoTo 0
	End Function
	
	'% Delete: Permite la eliminación física de una compañía contable de la tabla Led_compan
	Public Function Delete() As Boolean
		
		Dim lrecdelLed_compan As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelLed_compan = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delLed_compan'
		'+ Información leída el 01/09/2000 10:14:52 p.m.
		
		With lrecdelLed_compan
			.StoredProcedure = "delLed_compan"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLed_compan = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% Update: Permite la actualización de una compañía contable de la tabla Led_compan
	Public Function Update() As Boolean
		
		Dim lrecupdLed_compan As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdLed_compan = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updLed_compan'
		'+ Información leída el 02/09/2000 01:24:05 a.m.
		
		With lrecupdLed_compan
			.StoredProcedure = "updLed_compan"
			.Parameters.Add("dCompan_dat", dCompan_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_init", dDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_gp", sAccount_gp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_bg", sAccount_bg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBal_actu", sBal_actu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClose_mont", sClose_mont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStruct_uni", sStruct_uni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStructure", sStructure, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 7, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIniLedDat", dIniLedDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndLedDat", dEndLedDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLed_compan = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% CopyCatalogo: Permite copiar el catologo de una Compañia Contable seleccionada
	Public Function CopyCatalogo(ByVal lintLed_Compan As Integer, ByVal lintCopyCompany As Integer) As Boolean
		
		Dim lrecinsCopyCatalogo As eRemoteDB.Execute
		
		On Error GoTo CopyCatalogo_Err
		
		lrecinsCopyCatalogo = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insCopyCatalogo'
		'+ Información leída el 12/09/2000 02:40:12 p.m.
		
		With lrecinsCopyCatalogo
			.StoredProcedure = "insCopyCatalogo"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_CopyCompan", lintCopyCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			CopyCatalogo = .Run(False)
		End With
		
CopyCatalogo_Err: 
		If Err.Number Then
			CopyCatalogo = False
		End If
		On Error GoTo 0
	End Function
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	'% insPreFolder: Esta rutina se encaga de validar todos los datos introducidos en la forma
	Public Function insPreCP001(ByVal lstrAction As Integer, ByVal intLedCompan As Integer) As Boolean
		
		insPreCP001 = True
		
		Dim pclsOpt_ledger As Opt_ledger
		Dim pclsAcc_transa As Acc_transa
		Dim mclsLed_compan As Led_compan
		Dim mclsLedger_acc As LedgerAcc
		Dim mcolTab_cost_cs As Tab_cost_cs
		
		pclsOpt_ledger = New Opt_ledger
		pclsAcc_transa = New Acc_transa
		mclsLed_compan = New Led_compan
		mclsLedger_acc = New LedgerAcc
		mcolTab_cost_cs = New Tab_cost_cs
		
		On Error GoTo insPreCP001_err
		sStruct_uni = "000"
		sStructure = "0000000"
		
		Call Me.Find(intLedCompan)
		
		'+ Se le asignan a las propiedades declaradas para tomar el valor de los campos en la forma de asp.
		valtcdFromLedCompan = Me.dCompan_dat
		valtcdInitLedDate = Me.dIniLedDat
		vallblEndLedDate1 = Me.dEndLedDat
		valtcdFrom = Me.dDate_init
		vallblTo1 = Me.dDate_end
		
		Select Case lstrAction
			
			'+ Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				If pclsOpt_ledger.Find Then
					valtcdFromLedCompan = DateSerial(Year(Today), pclsOpt_ledger.nInitMonth, pclsOpt_ledger.nInitDay)
					valtcdInitLedDate = valtcdFromLedCompan
					vallblEndLedDate1 = DateSerial(Year(CDate(valtcdInitLedDate)) + 1, Month(CDate(valtcdInitLedDate)), VB.Day(CDate(valtcdInitLedDate)) - 1)
					valtcdFrom = valtcdInitLedDate
					vallblTo1 = DateSerial(Year(CDate(valtcdFrom)), Month(CDate(valtcdFrom)) + 1, VB.Day(CDate(valtcdFrom)) - 1)
				End If
				
				'+ Si la opción seleccionada es Modificar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				
				'+ Si la compañía tiene algún movimiento no se podrá cambiar moneda y
				'+ Fecha de inicio de la cía
				If pclsAcc_transa.valVoucherCompanExist(intLedCompan) Then
					EnachkCopy = True
					
					EnatcdFromLedCompan = True
					EnacbeCurrency = True
					
					'+ Si se tiene movimientos para el año contable, la fecha de inicio del año contable
					'+ no se puede modificar ni la fecha de inicio del mes contable.
					If pclsAcc_transa.valInitLedYearMovement(intLedCompan, Me.dIniLedDat, Me.dEndLedDat) Then
						EnatcdInitLedDate = True
						EnatcdFrom = True
					End If
					
					'+ Si se tiene movimientos para el año contable con numeración oficial los campos año y comprobante
					'+ no se pueden modificar.
					If pclsAcc_transa.valOfficialVoucher(intLedCompan, Me.dIniLedDat, Me.dEndLedDat) Then
						EnagmnYear = True
						EnagmnNum = True
					End If
					
					'+ Si se tiene movimientos para el mes contable el campo actualización automática
					'+ no se puede modificar.
					If pclsAcc_transa.valInitLedYearMovement(intLedCompan, Me.dDate_init, Me.dDate_end) Then
						EnachkUpdate = True
					Else
						EnachkUpdate = False
					End If
				End If
				
				'+ Si se tiene cuentas asociadas a la compañía no se puede modificar el campo estructura contable.
				With mclsLedger_acc
					If .ValCompany(intLedCompan) Then
						
						EnagmnCode0 = True
						EnagmnCode1 = True
						EnagmnCode2 = True
						EnagmnCode3 = True
						EnagmnCode4 = True
						EnagmnCode5 = True
						EnagmnCode6 = True
						
						EnachkCopy = True
						
						'+ Si las cuentas de resultado tienen saldo no pueden ser modificadas
						If .Find_Active(intLedCompan, Me.sAccount_gp, String.Empty) Then
							If .nBalance <> 0 Then
								EnagmtLossProfit = True
							End If
						End If
						
						If .Find_Active(intLedCompan, Me.sAccount_bg, String.Empty) Then
							If .nBalance <> 0 Then
								EnagmtGenBal = True
							End If
						End If
					End If
				End With
				
				'+ Si se tiene unidades organizativas asociadas a la compañía no se puede modificar el campo estructura contable de la unidad organizativa.
				If mcolTab_cost_cs.Find(intLedCompan) Then
					EnagmnUnit0 = True
					EnagmnUnit1 = True
					EnagmnUnit2 = True
				End If
		End Select
		
		'UPGRADE_NOTE: Object pclsOpt_ledger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsOpt_ledger = Nothing
		'UPGRADE_NOTE: Object pclsAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsAcc_transa = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mcolTab_cost_cs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_cost_cs = Nothing
		
insPreCP001_err: 
		If Err.Number Then
			insPreCP001 = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% insValCP001_k: Permite realizar las validaciones del encabezado de la transacción
	'CP001 - CInstalación de Compañía Contable.
	Public Function insValCP001_k(ByVal lstrCodispl As String, ByVal plngAction As Integer, ByVal lintLedCompan As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim mclsLed_compan As eLedge.Led_compan
		Dim mclsCompany As eGeneral.Company
		
		mclsLed_compan = New eLedge.Led_compan
		lobjErrors = New eFunctions.Errors
		mclsCompany = New eGeneral.Company
		
		On Error GoTo insValCP001_k_err
		
		insValCP001_k = String.Empty
		
		'+Se efectua la validación del campo compañia contable.
		If lintLedCompan <= 0 Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 7169)
		Else
			'+Si la acción es registrar, no debe estar registrado en el archivo de compañias contables
			If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
				If mclsLed_compan.Find(lintLedCompan, True) Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 36001)
				Else
					'El codigo de la compañia debe existir en la tabla (Company)
					If Not mclsCompany.Find(lintLedCompan, True) Then
						Call lobjErrors.ErrorMessage(lstrCodispl, 60828)
					End If
				End If
			Else
				'+Si la acción no es registrar, debe estar registrado en el archivo de compañias contables
				If Not mclsLed_compan.Find(lintLedCompan, True) Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 36002)
				End If
			End If
		End If
		
		insValCP001_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object mclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCompany = Nothing
		
insValCP001_k_err: 
		If Err.Number Then
			insValCP001_k = "insValCP001_k: " & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'% insValCP001: Permite realizar las validaciones del folder de la transacción CP001_K
	Public Function insValCP001(ByVal lstrCodispl As String, ByVal plngAction As Integer, ByVal lintLedCompan As Integer, ByVal tcdFromLedCompan As Date, ByVal tcdFrom As Date, ByVal tcdInitLedDate As Date, ByVal gmnYear As Integer, ByVal gmnUnit1 As Integer, ByVal gmnUnit2 As Integer, ByVal gmnUnit3 As Integer, ByVal lblEndLedDate1 As Date, ByVal lblTo1 As Date, ByVal gmtLossProfit As String, ByVal gmtGenBal As String, ByVal gmnCode1 As Integer, ByVal gmnCode2 As Integer, ByVal gmnCode3 As Integer, ByVal gmnCode4 As Integer, ByVal gmnCode5 As Integer, ByVal gmnCode6 As Integer, ByVal gmnCode7 As Integer, ByVal lintCurrency As Integer, ByVal lintLedCompanAux As Integer) As String
		Dim lobjErrors As New eFunctions.Errors
		Dim pclsBal_histor As Bal_histor
		
		Dim mclsAcc_transa As Acc_transa
		'    Dim mclsFin700_Lines As Fin700_Lines
		Dim mclsLedger_acc As LedgerAcc
		Dim mclsTab_cost_c As Tab_cost_c
		'    Dim mclsTab_Equal As Tab_equal
		
		
		Dim gmnCode As Integer
		Dim gmnUnit As Integer
		Dim lblnError As Boolean
		Dim lblnIndic As Boolean
		Dim lblnIndic1 As Boolean
		Dim lblnIndic2 As Boolean
		Dim lstrCode As String
		
		pclsBal_histor = New Bal_histor
		mclsAcc_transa = New Acc_transa
		'    Set mclsFin700_Lines = New Fin700_Lines
		mclsLedger_acc = New LedgerAcc
		mclsTab_cost_c = New Tab_cost_c
		'    Set mclsTab_Equal = New Tab_equal
		
		On Error GoTo insValCP001_err
		
		insValCP001 = String.Empty
		
		lblnError = False
		lblnIndic = False
		lblnIndic1 = False
		lblnIndic2 = False
		
		'+Si la accion es eliminar, la compañia indicada no debe poseer informacion registrada en otros archivos
		'+relacionados con compañias contables.
		If plngAction = eFunctions.Menues.TypeActions.clngActioncut Then
			If IsExist_LedCompan(lintLedCompan) Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 100011)
			End If
		Else
			
			'+Se efectua la validación del campo número de periodo contable.
			If Trim(CStr(gmnYear)) = CStr(eRemoteDB.Constants.intNull) Then
				gmnYear = 0
			End If
			
			'+Se valida el campo Ejercicio.
			If CShort(gmnYear) = 0 Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 736024)
			Else
				If CShort(gmnYear) <> 0 And plngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					
					If Not pclsBal_histor.MaxLedYear(lintLedCompan, gmnYear) Then
						Call lobjErrors.ErrorMessage(lstrCodispl, 736025)
					End If
				End If
			End If
			
			'+Se efectua la validación del campo fecha de inicio de la compañía contable.
			If tcdFromLedCompan = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 36203)
			Else
				If Not tcdFromLedCompan = eRemoteDB.Constants.dtmNull Then
					If Not tcdFrom = eRemoteDB.Constants.dtmNull And IsDate(tcdFrom) Then
						If CDate(tcdFromLedCompan) > CDate(tcdFrom) Then
							Call lobjErrors.ErrorMessage(lstrCodispl, 736012)
							lblnError = True
							lblnIndic = True
						End If
					End If
				End If
			End If
			
			'+Si el campo copiar catalogo tiene valor no se realizan las validaciones
			If lintLedCompanAux = eRemoteDB.Constants.intNull Then
				'+Se efectua la validación de los campos estructura y unidades. gmnCode0 - 6 y gmnUnit0 - 2
				gmnCode = gmnCode1 + gmnCode2 + gmnCode3 + gmnCode4 + gmnCode5 + gmnCode6 + gmnCode7
				
				If gmnCode > 0 Then
					If Not insValStructu(gmnCode) Then
						Call lobjErrors.ErrorMessage(lstrCodispl, 736002)
					Else
						lstrCode = CStr(gmnCode1) & CStr(gmnCode2) & CStr(gmnCode3) & CStr(gmnCode4) & CStr(gmnCode5) & CStr(gmnCode6) & CStr(gmnCode7)
						If insValspaceStructu(lstrCode) Then
							Call lobjErrors.ErrorMessage(lstrCodispl, 736003)
						ElseIf insValstructminus(lstrCode) Then 
							Call lobjErrors.ErrorMessage(lstrCodispl, 36012)
						End If
					End If
				Else
					'+Indique la estructura del codigo contable
					Call lobjErrors.ErrorMessage(lstrCodispl, 60829)
				End If
				
				'+Se efectua la validacion de los campos unidad organizativa.
				gmnUnit = gmnUnit1 + gmnUnit2 + gmnUnit3
				If (gmnUnit > 8) Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 736004)
				Else
					If (gmnUnit1 > 0) And (gmnUnit3 > 0) Then
						If (gmnUnit2 = 0) Then
							Call lobjErrors.ErrorMessage(lstrCodispl, 736005)
						End If
					End If
				End If
			End If
			lblnError = False
			
			'+Se efectua la validación del campo Año contable-desde.
			If tcdInitLedDate = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 36004)
			Else
				'+El día siempre debe ser uno(1)
				If VB.Day(CDate(tcdInitLedDate)) <> 1 Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 36218)
				End If
				
				'+Debe ser igual a la fecha inicial de la compañia contable.
				If CDate(tcdFromLedCompan) <> CDate(tcdInitLedDate) Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 36221)
				End If
				
				'+Debe ser anterior o igual a la fecha inicial del mes contable.
				If Not tcdFrom = eRemoteDB.Constants.dtmNull And IsDate(tcdFrom) Then
					If CDate(tcdFrom) < CDate(tcdInitLedDate) Then
						Call lobjErrors.ErrorMessage(lstrCodispl, 736035)
						lblnError = True
						lblnIndic2 = True
					End If
				End If
			End If
			
			lblnError = False
			
			'+Se efectua la validación del campo Mes contable-desde.
			If tcdFrom = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 36005)
			Else
				If Not IsDate(tcdFrom) Then
				Else
					If VB.Day(CDate(tcdFrom)) <> 1 Then
						Call lobjErrors.ErrorMessage(lstrCodispl, 36218)
					End If
				End If
			End If
			
			'+Se efectua la validación del campo cuenta de resultado - Ganancias y perdidas -.
			With mclsLedger_acc
				If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 736008)
				End If
				
				If Trim(gmtLossProfit) <> String.Empty Then
					If plngAction <> eFunctions.Menues.TypeActions.clngActionadd Then
						If Not .Find_Active(lintLedCompan, gmtLossProfit, String.Empty) Then
							Call lobjErrors.ErrorMessage(lstrCodispl, 36010)
						Else
							If .nBalance <> 0 And plngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
								Call lobjErrors.ErrorMessage(lstrCodispl, 36008)
							Else
								If .sType_acc <> "4" And .sType_acc <> "6" Then
									Call lobjErrors.ErrorMessage(lstrCodispl, 36023)
								Else
									If .Val_Structure_Down(lintLedCompan, gmtLossProfit) Then
										Call lobjErrors.ErrorMessage(lstrCodispl, 7129)
									End If
								End If
							End If
						End If
					End If
				End If
				
				
				'+Se efectua la validación del campo cuenta de resultado de Balance general.
				If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 736009)
				End If
				If Trim(gmtGenBal) <> String.Empty Then
					If plngAction <> eFunctions.Menues.TypeActions.clngActionadd Then
						If Not .Find_Active(lintLedCompan, gmtGenBal, String.Empty) Then
							Call lobjErrors.ErrorMessage(lstrCodispl, 36010)
						Else
							If .nBalance <> 0 And plngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
								Call lobjErrors.ErrorMessage(lstrCodispl, 36009)
							Else
								If .sType_acc <> "2" And .sType_acc <> "5" Then
									Call lobjErrors.ErrorMessage(lstrCodispl, 36023)
								Else
									If .Val_Structure_Down(lintLedCompan, gmtGenBal) Then
										Call lobjErrors.ErrorMessage(lstrCodispl, 7129)
									End If
								End If
							End If
						End If
					End If
				End If
			End With
		End If
		
		insValCP001 = lobjErrors.Confirm
		
insValCP001_err: 
		If Err.Number Then
			insValCP001 = "insValCP001: " & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%insPostCP001: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "CP001"
	Public Function insPostCP001(ByVal sCodispl As String, ByVal plngAction As Integer, ByVal nLed_compan As Integer, ByVal sAccount_bg As String, ByVal sAccount_gp As String, ByVal sBal_actu As String, ByVal sClose_mont As String, ByVal gmnUnit1 As String, ByVal gmnUnit2 As String, ByVal gmnUnit3 As String, ByVal gmnCode1 As String, ByVal gmnCode2 As String, ByVal gmnCode3 As String, ByVal gmnCode4 As String, ByVal gmnCode5 As String, ByVal gmnCode6 As String, ByVal gmnCode7 As String, ByVal dCompan_dat As Date, ByVal dDate_end As Date, ByVal dDate_init As Date, ByVal nVoucher As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nYear As Integer, ByVal dIniLedDat As Date, ByVal dEndLedDat As Date, ByVal nLed_companAux As Integer, ByVal sStatregt As String) As Boolean
		On Error GoTo insPostCP001_Err
		
		Me.nLed_compan = nLed_compan
		Me.sAccount_bg = sAccount_bg
		Me.sAccount_gp = sAccount_gp
		
		If sBal_actu <> "1" Then
			Me.sBal_actu = "2"
		Else
			Me.sBal_actu = sBal_actu
		End If
		
		If sClose_mont <> "1" Then
			Me.sClose_mont = "2"
		Else
			Me.sClose_mont = sClose_mont
		End If
		
		Me.dCompan_dat = dCompan_dat
		Me.dDate_end = dDate_end
		Me.dDate_init = dDate_init
		Me.nVoucher = nVoucher
		Me.nCurrency = nCurrency
		Me.nUsercode = nUsercode
		Me.nYear = nYear
		Me.dIniLedDat = dIniLedDat
		Me.dEndLedDat = dEndLedDat
		
		Me.sStructure = Trim(gmnCode1) & Trim(gmnCode2) & Trim(gmnCode3) & Trim(gmnCode4) & Trim(gmnCode5) & Trim(gmnCode6) & Trim(gmnCode7)
		Me.sStruct_uni = Trim(gmnUnit1) & Trim(gmnUnit2) & Trim(gmnUnit3)
		
		Select Case plngAction
			
			'+Si la opción seleccionada es Registrar
			Case eFunctions.Menues.TypeActions.clngActionadd
				Me.sStatregt = sStatregt
				
				'+Se agrega la compañia contable.
				insPostCP001 = Add
				
				If nLed_companAux > eRemoteDB.Constants.intNull Then
					'+Se copia la estructura de la compañia seleccionada a la que se quiere agregar.
					insPostCP001 = Me.CopyCatalogo(Me.nLed_compan, nLed_companAux)
				End If
				
				'+Si la opción seleccionada es Modificar
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				Me.sStatregt = sStatregt
				
				insPostCP001 = Update
				
				If nLed_companAux > eRemoteDB.Constants.intNull Then
					'+Se actualiza la estructura de la compañia seleccionada a la que se quiere actualizar.
					insPostCP001 = Me.CopyCatalogo(Me.nLed_compan, nLed_companAux)
				End If
				
				'+Si la opción seleccionada es Eliminar
			Case eFunctions.Menues.TypeActions.clngActioncut
				insPostCP001 = Delete
		End Select
		
insPostCP001_Err: 
		If Err.Number Then
			insPostCP001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValstructu: Esta rútina permite verificar la estructura de las cuentas contables.
	Private Function insValStructu(ByVal gmnCode As Integer) As Boolean
		insValStructu = True
		
		If gmnCode > 20 Or gmnCode < 3 Then
			insValStructu = False
		End If
	End Function
	
	'%insValspaceStructu: Esta rútina permite verificar que no haya espacios en la estructura de las cuentas contables.
	Private Function insValspaceStructu(ByVal strCode As String) As Boolean
		Dim llngCount As Integer
		Dim lblnFill As Boolean
		Dim lblnSpace As Boolean
		
		insValspaceStructu = False
		
		lblnFill = False
		lblnSpace = False
		
		For llngCount = 1 To 7
			If Mid(strCode, llngCount, 1) <> "0" Then
				lblnFill = True
				
				If lblnFill And lblnSpace Then
					insValspaceStructu = True
					Exit For
				End If
			Else
				lblnSpace = True
			End If
		Next llngCount
	End Function
	
	'%insValstructu: Esta rútina permite verificar si en la estructura de las cuentas contables
	'%se definieron menos de tres niveles
	Private Function insValstructminus(ByVal strCode As String) As Boolean
		Dim lintCount As Integer
		Dim lintLevel As Integer
		
		insValstructminus = False
		
		lintLevel = 0
		
		For lintCount = 1 To 7
			If Mid(strCode, lintCount, 1) <> "0" Then
				lintLevel = lintLevel + 1
			End If
		Next lintCount
		
		If lintLevel < 3 Then
			insValstructminus = True
		End If
	End Function
	
	'% ReverseMove: Devuelve el ultimo agno contable de una compagnia dada
	Public Function ReverseMove(ByVal nLed_compan As Integer, ByVal dDate_init As Date, ByVal dDate_end As Date, ByVal dDate_initOpt As Date, ByVal dDate_endOpt As Date, ByVal nLed_year As Integer, ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nUsercode As Integer, ByVal sAccountGP As String) As Boolean
		Dim lrecinsReOpenLedger As eRemoteDB.Execute
		
		lrecinsReOpenLedger = New eRemoteDB.Execute
		
		On Error GoTo ReverseMove_err
		
		'+Definición de parámetros para stored procedure 'insudb.insReOpenLedger'
		'+Información leída el 20/06/2001 11:20:36 a.m.
		With lrecinsReOpenLedger
			.StoredProcedure = "insReOpenLedger"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_init", dDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_initOpt", dDate_initOpt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_endOpt", dDate_endOpt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Year", nLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccountGP", sAccountGP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReOpenLedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReOpenLedger = Nothing
		
ReverseMove_err: 
		If Err.Number Then
			ReverseMove = False
		End If
		
	End Function
End Class






