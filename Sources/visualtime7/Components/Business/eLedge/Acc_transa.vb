Option Strict Off
Option Explicit On
Public Class Acc_transa
	'%-------------------------------------------------------%'
	'% $Workfile:: Acc_transa.cls                           $%'
	'% $Author:: Nvaplat17                                  $%'
	'% $Date:: 28/11/03 18:53                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'   Column_name                   Type   Computed    Length  Prec  Scale Nullable  TrimTrailingBlanks                  FixedLenNullInSource
	Public nLed_compan As Integer 'smallint  no         2     5    0      no        (n/a)                               (n/a)
	Public nVoucher As Integer 'int       no         4    10    0      no        (n/a)                               (n/a)
	Public nBalance As Double 'decimal   no         9    12    2      yes       (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint  no         2     5    0      yes       (n/a)                               (n/a)
	Public sDescript As String 'char      no        30                 yes       yes                                 yes
	Public dEffecdate As Date 'datetime  no         8                 yes       (n/a)                               (n/a)
	Public sInd_automa As String 'char      no         1                 yes       yes                                 yes
	Public nNoteNum As Integer 'int       no         4    10    0      yes       (n/a)                               (n/a)
	Public sProcess_in As String 'char      no         1                 yes       yes                                 yes
	Public sStatregt As String 'char      no         1                 yes       yes                                 yes
	Public nTot_credit As Double 'decimal   no         9    12    2      yes       (n/a)                               (n/a)
	Public nTot_debit As Double 'decimal   no         9    12    2      yes       (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint  no         2     5    0      yes       (n/a)                               (n/a)
	Public nOffiNum As Integer 'int       no         4    10    0      yes       (n/a)                               (n/a)
	Public nOldBalance As Double 'decimal   no         9    12    2      yes       (n/a)                               (n/a)
	'**-Auxiliary variables
	'- Variables auxiliares
	
	Public dLastAccVoucherDate As Date
	Public mcolAcc_lineses As Acc_lineses
	Public mcolAcc_transa As Collection
	Private mdblEndBalance As Double
	
	'**-Used in insPreCPC002
	'- Usadas en insPreCPC002
	Private mclsAcc_transa As Acc_transa
	Private mobjGrid As eFunctions.Grid
	Private mstrAccount As String
	Private mstrAux_accoun As String
	Private mdblInitBalance As Double
	
	
	'**% AllVoucherExist: Check if exist a voucher for a given company
	'% AllVoucherExist: Evalua si existen comprobantes (asientos) para una compañia dada
	Public Function valVoucherCompanExist(ByVal intLed_compan As Integer) As Boolean
		
		'**-Define the variable lrecreaAcc_transaCompany
		'- Se define la variable lrecreaAcc_transaCompany
		Dim lrecreaAcc_transaCompany As eRemoteDB.Execute
		
		lrecreaAcc_transaCompany = New eRemoteDB.Execute
		
		On Error GoTo valVoucherCompanExist_err
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaCompany'
		'**+Data read on 05/23/2001 15:29:05 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaCompany'
		'+ Informacion leida el 23/05/2001 15:29:05 PM
		
		With lrecreaAcc_transaCompany
			.StoredProcedure = "reaAcc_transaCompany"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valVoucherCompanExist = .Run
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaCompany = Nothing
		
valVoucherCompanExist_err: 
		If Err.Number Then
			valVoucherCompanExist = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% IniLedYearVoucher: check if vouchers exist in the Acc_transa table
	'**% for the first countable year
	'% InitLedYearVoucher: Evalua si existen comprobantes (asientos) en la tabla Acc_transa
	'% para el primer agno contable
	Public Function valInitLedYearMovement(ByVal intLed_compan As Integer, ByVal dtmDateFrom As Date, ByVal dtmDateTo As Date) As Boolean
		
		'**-Define the variable lrecreaAcc_transaExist
		'- Se define la variable lrecreaAcc_transaExist
		Dim lrecreaAcc_transaExist As eRemoteDB.Execute
		
		lrecreaAcc_transaExist = New eRemoteDB.Execute
		
		On Error GoTo valInitLedYearMovement_err
		'**+Parameter definition for the stored procedure 'insudb.reaAcc_transaExist'
		'**+Data read on 05/23/2001 15:15:23 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaExist'
		'+ Informacion leida el 23/05/2001 15:15:23 PM
		
		With lrecreaAcc_transaExist
			.StoredProcedure = "reaAcc_transaExist"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateFrom", dtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dtmDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valInitLedYearMovement = .Run
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaExist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaExist = Nothing
		
valInitLedYearMovement_err: 
		If Err.Number Then
			valInitLedYearMovement = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% InitLedYEarOfficialVoucher: check if voucher exists with official numeration
	'**%in the table Acc_transa for the first countable year
	'% InitLedYearOfficialVoucher: Evalua si existen comprobantes (asientos) con numeracion
	'% oficial en la tabla Acc_transa para el primer agno contable
	Public Function valOfficialVoucher(ByVal intLed_compan As Integer, ByVal dtmDateFrom As Date, ByVal dtmDateTo As Date) As Boolean
		
		'**-Define the variable lrecreaAcc_transaOfficial
		'- Se define la variable lrecreaAcc_transaOfficial
		Dim lrecreaAcc_transaOfficial As eRemoteDB.Execute
		
		lrecreaAcc_transaOfficial = New eRemoteDB.Execute
		
		On Error GoTo valOfficialVoucher_err
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaOfficial'
		'**+Data read on 05/23/2001 15:47:16 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaOfficial'
		'+ Informacion leida el 23/05/2001 15:47:16 AM
		
		With lrecreaAcc_transaOfficial
			.StoredProcedure = "reaAcc_transaOfficial"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateFrom", dtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dtmDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valOfficialVoucher = .Run
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaOfficial may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaOfficial = Nothing
		
valOfficialVoucher_err: 
		If Err.Number Then
			valOfficialVoucher = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% LastAccVoucherDate: return the date of the last establishment made to an acoount
	'% LastAccVoucherDate: Devuelve la fecha del ultimo asiento realizado a una cuenta
	Public Function LastAccVoucherDate(ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sAux_accoun As String) As Boolean
		
		Dim lrecreaAcc_transaAcc_linesDate As eRemoteDB.Execute
		
		On Error GoTo LastAccVoucherDate_Err
		
		lrecreaAcc_transaAcc_linesDate = New eRemoteDB.Execute
		
		With lrecreaAcc_transaAcc_linesDate
			.StoredProcedure = "reaAcc_transaAcc_linesDate"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If Not (.FieldToClass("ldtmDate") = eRemoteDB.Constants.dtmNull) Then
					dLastAccVoucherDate = .FieldToClass("ldtmDate")
					LastAccVoucherDate = True
				Else
					LastAccVoucherDate = False
				End If
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaAcc_linesDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaAcc_linesDate = Nothing
		
LastAccVoucherDate_Err: 
		If Err.Number Then
			LastAccVoucherDate = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% Find: return the info of a voucher according to the temporary voucher number
	'% Find: Devuelve la informacion de un determinado comprobante (asiento)
	'% segun numero de comprobante temporal dado
	Public Function Find(ByVal nLed_compan As Integer, ByVal nVoucher As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		'Static lblnRead As Boolean
		
		'**-Define the variable lrecreaAcc_transa
		'- Se define la variable lrecreaAcc_transa
		Dim lrecreaAcc_transa As eRemoteDB.Execute
		
		lrecreaAcc_transa = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transa'
		'**+Data read on 06/19/2001 02:41:49 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transa'
		'+ Informacion leida el 19/06/2001 02:41:49 PM
		
		With lrecreaAcc_transa
			.StoredProcedure = "reaAcc_transa"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nVoucher = .FieldToClass("nVoucher")
				nOffiNum = .FieldToClass("nOffiNum")
				nNoteNum = .FieldToClass("nNotenum")
				sDescript = .FieldToClass("sDescript")
				Find = True
				.RCloseRec()
			Else
				nLed_compan = 0
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transa = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% Find_ByOffiNum: return the info of a voucher according to the official given number
	'% Find_ByOffiNum: Devuelve la informacion de un determinado comprobante (asiento)
	'% segun numero oficial dado
	Public Function Find_ByOffiNum(ByVal nLed_compan As Integer, ByVal nOffiNum As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		'Static lblnRead_ByOffiNum As Boolean
		
		'**-Define the variable lrecreaAcc_transaOffi
		'- Se define la variable lrecreaAcc_transaOffi
		Dim lrecreaAcc_transaOffi As eRemoteDB.Execute
		
		lrecreaAcc_transaOffi = New eRemoteDB.Execute
		
		On Error GoTo Find_ByOffiNum_err
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaOffi'
		'**+Data read on 09/01/2000 02:01:09 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaOffi'
		'+ Informacion leida el 01/09/2000 02:01:09 PM
		
		With lrecreaAcc_transaOffi
			.StoredProcedure = "reaAcc_transaOffi"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffiNum", nOffiNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nVoucher = .FieldToClass("nVoucher")
				nOffiNum = .FieldToClass("nOffiNum")
				nNoteNum = .FieldToClass("nNotenum")
				Find_ByOffiNum = True
				.RCloseRec()
			Else
				nLed_compan = 0
				Find_ByOffiNum = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaOffi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaOffi = Nothing
		
Find_ByOffiNum_err: 
		If Err.Number Then
			Find_ByOffiNum = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: update a voucher of the Acc_transa table
	'% Update: Actualiza un comprobante de la tabla Acc_transa
	'---------------------------------------------------------
	Public Function Update() As Boolean
		'---------------------------------------------------------
		
		'**-Define the variable lrecupAcc_transa
		'- Se define la variable lrecupdAcc_transa
		Dim lrecupdAcc_transa As eRemoteDB.Execute
		
		lrecupdAcc_transa = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaOffi'
		'**+Data read on 06/20/2001 11:06:07 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaOffi'
		'+ Informacion leida el 20/06/2001 11:06:07 AM
		
		With lrecupdAcc_transa
			.StoredProcedure = "updAcc_transa"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_credit", nTot_credit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_debit", nTot_debit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdAcc_transa = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	'**% Delete: delete a voucher from de Acc_transa table
	'% Delete: Elimina un comprobante de la tabla Acc_transa
	'-------------------------------------------------------
	Public Function Delete() As Boolean
		'-------------------------------------------------------
		
		'**-Define the variable lrecdelAcc_transa
		'- Se define la variable lrecdelAcc_transa
		Dim lrecdelAcc_transa As eRemoteDB.Execute
		
		lrecdelAcc_transa = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		'**+Parameters definition for the stored procedure 'insudb.delAcc_transa'
		'**+Data read on 09/20/2001 02:21:50 PM
		'+ Definicion de parametros para stored procedure 'insudb.delAcc_transa'
		'+ Informacion leida el 20/09/2001 02:21:50 PM
		
		With lrecdelAcc_transa
			.StoredProcedure = "delAcc_transa"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelAcc_transa = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add: add a voucher to the Acc_transa table
	'% Add: Añade un comprobante a la tabla Acc_transa
	'--------------------------------------------------
	Public Function Add() As Boolean
		'--------------------------------------------------
		
		'**-Define the variable lreccreAcc_transa
		'- Se define la variable lreccreAcc_transa
		Dim lreccreAcc_transa As eRemoteDB.Execute
		
		lreccreAcc_transa = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		'**+Parameters definition for the stored procedure 'insudb.creAcc_transa'
		'**+Data read on 06/20/2001 03:32:58 PM
		'+ Definicion de parametros para stored procedure 'insudb.creAcc_transa'
		'+ Informacion leida el 20/06/2001 03:32:58 PM
		
		With lreccreAcc_transa
			.StoredProcedure = "creAcc_transa"
			
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_automa", sInd_automa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess_in", sProcess_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_credit", nTot_credit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_debit", nTot_debit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffiNum", nOffiNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreAcc_transa = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% AccVoucherDetailByDate: return the countable establishment according to the effective date "From"
	'% AccVoucherDetailByDate: Devuelve los asientos contables según una fecha de efecto "Desde" dada
	Public Function AccVoucherDetailByDate(ByVal nLed_compan As Integer, ByVal dEffecdate As Date, ByVal sAccount As String, ByVal sAux_accoun As String) As Boolean
		
		Dim lrecreaAcc_transaAcc_lines As eRemoteDB.Execute
		Dim lclsAcc_transa As Acc_transa
		Dim lcolAcc_transa As Collection
		Dim llngVoucher As Integer
		Dim ldblDebit As Double
		Dim ldblCredit As Double
		
		On Error GoTo AccVoucherDetailByDate_Err
		
		lrecreaAcc_transaAcc_lines = New eRemoteDB.Execute
		lcolAcc_transa = New Collection
		
		'**+Parameter definition for the sotred procedure 'insudb.reaAcc_transaAcc_lines'
		'**+Data read on 09/08/2000 04:37:09 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaAcc_lines'
		'+ Informacion leida el 08/09/2000 04:37:09 PM
		
		With lrecreaAcc_transaAcc_lines
			.StoredProcedure = "reaAcc_transaAcc_lines"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AccVoucherDetailByDate = .Run
			
			If AccVoucherDetailByDate Then
				Do While Not .EOF
					'PVA:                If llngVoucher <> .FieldToClass("nVoucher") Then
					'PVA:                    If llngVoucher <> 0 Then
					'PVA:                        Set lclsAcc_transa = Nothing
					'PVA:                    End If
					llngVoucher = .FieldToClass("nVoucher")
					
					'**+ Transaction info
					'+ Información del Asiento
					
					lclsAcc_transa = New Acc_transa
					
					lclsAcc_transa.nLed_compan = .FieldToClass("nLed_compan")
					lclsAcc_transa.nVoucher = .FieldToClass("nVoucher")
					lclsAcc_transa.nBalance = .FieldToClass("nBalance")
					lclsAcc_transa.sDescript = .FieldToClass("sDescript")
					lclsAcc_transa.dEffecdate = .FieldToClass("dEffecdate")
					lclsAcc_transa.sInd_automa = .FieldToClass("sInd_automa")
					lclsAcc_transa.nNoteNum = .FieldToClass("nNotenum")
					lclsAcc_transa.sProcess_in = .FieldToClass("sProcess_in")
					lclsAcc_transa.sStatregt = .FieldToClass("sStatregt")
					lclsAcc_transa.nTot_credit = .FieldToClass("nTot_credit")
					lclsAcc_transa.nTot_debit = .FieldToClass("nTot_debit")
					lclsAcc_transa.nOffiNum = .FieldToClass("nOffiNum")
					
					'**+Transaction collection
					'+ Colección de Asientos
					
					lcolAcc_transa.Add(lclsAcc_transa)
					'PVA:                End If
					
					'**+ Information of the transaction line
					'+ Información de la línea del Asiento
					
					lclsAcc_transa.mcolAcc_lineses = New Acc_lineses
					Call lclsAcc_transa.mcolAcc_lineses.Add(.FieldToClass("nVoucher"), .FieldToClass("nLed_compan"), .FieldToClass("nLine"), .FieldToClass("sAccount"), .FieldToClass("sAux_accoun"), .FieldToClass("sClient"), .FieldToClass("nCredit"), .FieldToClass("dDate_doc"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("nDoc_type"), .FieldToClass("nDocnumber"), eRemoteDB.Constants.intNull, .FieldToClass("nOri_curr"), "1", eRemoteDB.Constants.intNull, .FieldToClass("sCost_cente"), .FieldToClass("nExchange"), .FieldToClass("nOri_amo"))
					
					If .FieldToClass("nDebit") <> eRemoteDB.Constants.intNull Then
						ldblDebit = ldblDebit + .FieldToClass("nDebit")
						'PVA:               Debug.Print "Débito: " & ldblDebit
					End If
					
					If .FieldToClass("nCredit") <> eRemoteDB.Constants.intNull Then
						ldblCredit = ldblCredit + .FieldToClass("nCredit")
						'PVA:               Debug.Print "Crédito: " & ldblCredit
					End If
					
					.RNext()
				Loop 
				
				mdblEndBalance = CDbl(Format(ldblDebit, "##,###,##0.00")) - CDbl(Format(ldblCredit, "##,###,##0.00"))
				
				Debug.Print("Balance: " & mdblEndBalance)
				
				.RCloseRec()
			End If
		End With
		
		'**+Making public the transaction collection
		'+ Haciendo pública la colección de Asientos
		
		mcolAcc_transa = lcolAcc_transa
		
AccVoucherDetailByDate_Err: 
		If Err.Number Then
			AccVoucherDetailByDate = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lcolAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAcc_transa = Nothing
		'UPGRADE_NOTE: Object lrecreaAcc_transaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaAcc_lines = Nothing
	End Function
	
	'**% VoucherIniBal: makes the calculation of the initial balance of a claim
	'% VoucherInitBal: Realiza el cálculo del saldo inicial de un asiento
	Public Function VoucherInitBal(ByVal nLed_compan As Integer, ByVal dEffecdate As String, ByVal sAccount As String, ByVal sAux_accoun As String) As Double
		
		'**-Define the variable ldblDebit used to keep the debit balance
		'- Se define la variable ldblDebit utilizada para almacenar el saldo débito
		Dim ldblDebit As Double
		
		'**-Define the variable ldblCredit used to keep the credit balance
		'- Se define la variable ldblCredit utilizada para almacenar el saldo crédito
		Dim ldblCredit As Double
		
		'**-Define the variable lrecreaAcc_transaAcc_linesInitBal
		'- Se define la variable lrecreaAcc_transaAcc_linesInitBal
		Dim lrecreaAcc_transaAcc_linesInitBal As eRemoteDB.Execute
		
		On Error GoTo VoucherInitBal_Err
		
		lrecreaAcc_transaAcc_linesInitBal = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaAcc_linesInitBal'
		'**+Data read on 09/08/2000 11:37:41 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaAcc_linesInitBal'
		'+ Informacion leida el 08/09/2000 11:37:41 AM
		
		With lrecreaAcc_transaAcc_linesInitBal
			.StoredProcedure = "reaAcc_transaAcc_linesInitBal"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("ldblDebit") = eRemoteDB.Constants.intNull Then
					ldblDebit = 0
				Else
					ldblDebit = .FieldToClass("ldblDebit")
				End If
				
				If .FieldToClass("ldblCredit") = eRemoteDB.Constants.intNull Then
					ldblCredit = 0
				Else
					ldblCredit = .FieldToClass("ldblCredit")
				End If
				
				VoucherInitBal = ldblDebit - ldblCredit
				.RCloseRec()
			Else
				VoucherInitBal = eRemoteDB.Constants.intNull
			End If
		End With
		
VoucherInitBal_Err: 
		If Err.Number Then
			VoucherInitBal = eRemoteDB.Constants.intNull
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaAcc_linesInitBal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaAcc_linesInitBal = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'    Set mclsAcc_transa = New Acc_transa
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mcolAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolAcc_transa = Nothing
		'UPGRADE_NOTE: Object mclsAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsAcc_transa = Nothing
		'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjGrid = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**@@@@@@@@@@@@@@@@@@ FUNCTIONS OF VALIDATION AND EXECUTION (VAL AND POST) @@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	'**%insPreCPC002: Initializes all of the properties utilized by the page "CPC002"
	'%insPreCPC002: Este metodo inicializa todas las propiedades utilizada por la pagina "CPC002"
	Public Function insPreCPC002(ByVal nLed_compan As Integer, ByVal dEffecdate As Date, ByVal sAccount As String, ByVal sAux_accoun As String) As String
		
		Me.nLed_compan = nLed_compan
		Me.dEffecdate = dEffecdate
		mstrAccount = sAccount
		mstrAux_accoun = sAux_accoun
        insPreCPC002 = String.Empty
		insPreCPC002 = insPreCPC002 & "<SCRIPT>" & "var mdblInitBalance;" & "var mdblEndBalance;" & "</SCRIPT>"
		
		Call insReaAcc_transaAcc_lines()
		If insReaAcc_transaAcc_lines <> String.Empty Then
			insPreCPC002 = insPreCPC002 & insReaAcc_transaAcc_lines
			
			'**+ Initial balance
			'+ Saldo Inicial
			mdblInitBalance = mclsAcc_transa.VoucherInitBal(Me.nLed_compan, CStr(Me.dEffecdate), mstrAccount, mstrAux_accoun)
			If mdblInitBalance <> eRemoteDB.Constants.intNull Then
				insPreCPC002 = insPreCPC002 & "<SCRIPT>" & "mdblInitBalance = " & mdblInitBalance & ";" & "mdblInitBalance = VTFormat(mdblInitBalance,'','','',2);" & "UpdateDiv('lblInitBalance',mdblInitBalance,'Normal');" & "mdblEndBalance = mdblInitBalance + mdblEndBalance;" & "</SCRIPT>"
			End If
		End If
	End Function
	
	'**% insReaAcc_transaAcc_lines: read the information of the countables accounts (Ledger_Acc)
	'**% of many accounts
	'% insReaAcc_transaAcc_lines: Lee información de la tabla de Cuentas Contables (Ledger_acc)
	'% de varias cuentas
	Private Function insReaAcc_transaAcc_lines() As String
		
		Dim lclsAcc_transa As eLedge.Acc_transa
		Dim lclsAcc_lines As eLedge.Acc_lines
		
		'**- Define the variable ldblDeb used to keep the debit value
		'- Se define la variable ldblDeb utilizada para almacenar el valor de los débitos
		Dim ldblDeb As Double
		
		'**-Defien the variable ldblCre used to keep the credit value
		'- Se define la variable ldblCre utilizada para almacenar el valor de los créditos
		Dim ldblCre As Double
		
        On Error GoTo insReaAcc_transaAcc_lines_Err

        insReaAcc_transaAcc_lines = String.Empty
		
		lclsAcc_transa = New eLedge.Acc_transa
		lclsAcc_lines = New eLedge.Acc_lines
		
		Call insDefineHeader()
		
		If mclsAcc_transa.AccVoucherDetailByDate(Me.nLed_compan, Me.dEffecdate, mstrAccount, mstrAux_accoun) Then
			For	Each lclsAcc_transa In mclsAcc_transa.mcolAcc_transa
				With mobjGrid
					.Columns("tcdDate").DefValue = CStr(lclsAcc_transa.dEffecdate)
					
					If lclsAcc_transa.nOffiNum = eRemoteDB.Constants.intNull Then
						.Columns("tctOffiNum").DefValue = CStr(0)
					Else
						.Columns("tctOffiNum").DefValue = CStr(lclsAcc_transa.nOffiNum)
					End If
					
					.Columns("tctVoucher").DefValue = CStr(lclsAcc_transa.nVoucher)
					
					For	Each lclsAcc_lines In lclsAcc_transa.mcolAcc_lineses
						.Columns("tcnAcc_lineses").DefValue = CStr(lclsAcc_lines.nLine)
						
						If lclsAcc_lines.nDebit = eRemoteDB.Constants.intNull Then
							.Columns("tcnDebit").DefValue = CStr(0)
						Else
							.Columns("tcnDebit").DefValue = CStr(lclsAcc_lines.nDebit)
							ldblDeb = ldblDeb + CDbl(.Columns("tcnDebit").DefValue)
						End If
						
						If lclsAcc_lines.nCredit = eRemoteDB.Constants.intNull Then
							.Columns("tcnCredit").DefValue = CStr(0)
						Else
							.Columns("tcnCredit").DefValue = CStr(lclsAcc_lines.nCredit)
							ldblCre = ldblCre + CDbl(.Columns("tcnCredit").DefValue)
						End If
						.Columns("tctDescript").DefValue = lclsAcc_lines.sDescript
					Next lclsAcc_lines
					
					'**+ Draw the grid lines
                    '+ Se dibujan las líneas del grid

					insReaAcc_transaAcc_lines = insReaAcc_transaAcc_lines & .DoRow
				End With
			Next lclsAcc_transa
			
			'**+Call to the CloseTable property, to finish the creation of the table (Grid)
			'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
			insReaAcc_transaAcc_lines = insReaAcc_transaAcc_lines & mobjGrid.closeTable
			mdblEndBalance = ldblDeb - ldblCre
			
			'**+ Final balance
			'+ Saldo Final
			insReaAcc_transaAcc_lines = insReaAcc_transaAcc_lines & "<SCRIPT>" & "mdblEndBalance = " & mdblEndBalance & ";" & "mdblEndBalance = VTFormat(mdblEndBalance, '', '', '',2);" & "UpdateDiv('lblEndBalance',mdblEndBalance,'Normal');" & "</SCRIPT>"
		Else
			insReaAcc_transaAcc_lines = String.Empty
		End If
		
insReaAcc_transaAcc_lines_Err: 
		If Err.Number Then
			insReaAcc_transaAcc_lines = String.Empty
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAcc_lines = Nothing
		'UPGRADE_NOTE: Object lclsAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAcc_transa = Nothing
	End Function

    '**% insDefineHeader: Define the Grid columns
    '% insDefineHeader: Define las columnas del Grid
    Private Sub insDefineHeader()

        Dim lclsValues As eFunctions.Values

        mobjGrid = New eFunctions.Grid
        lclsValues = New eFunctions.Values

        '**+Define the grid columns
        '+ Se definen las columnas del grid

        With mobjGrid.Columns
            Call .AddTextColumn(0, eFunctions.Values.GetMessage(883), "tcdDate", 10, "",  , eFunctions.Values.GetMessage(890))
            Call .AddTextColumn(0, eFunctions.Values.GetMessage(884), "tctOffiNum", 10, "",  , eFunctions.Values.GetMessage(891))
            Call .AddTextColumn(0, eFunctions.Values.GetMessage(885), "tctVoucher", 10, "",  , eFunctions.Values.GetMessage(892))
            Call .AddNumericColumn(0, eFunctions.Values.GetMessage(886), "tcnAcc_lineses", 10, "",  , eFunctions.Values.GetMessage(893),  , 0)
            Call .AddNumericColumn(0, eFunctions.Values.GetMessage(887), "tcnDebit", 10, CStr(0),  , eFunctions.Values.GetMessage(894), True, 2)
            Call .AddNumericColumn(0, eFunctions.Values.GetMessage(888), "tcnCredit", 10, CStr(0),  , eFunctions.Values.GetMessage(895), True, 2)
            Call .AddTextColumn(0, eFunctions.Values.GetMessage(889), "tctDescript", 30, "",  , eFunctions.Values.GetMessage(896))
        End With

        '**+Define the general grid properties
        '+ Se definen las propiedades generales del grid

        With mobjGrid
            .Codispl = "CPC002"
            .AddButton = False
            .DeleteButton = False
            .Top = 70
            .Width = 330
            .Height = 400
            .Columns("Sel").GridVisible = False
        End With
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
    End Sub

    '**%insValCPL002_K: This function perform validations over the fields of the CPL002
    '%insValCPL002_K: Esta función se encarga de validar los datos introducidos en la CPL002
    Public Function insValCPL002_K(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal dCloseDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsAcc_transa As eRemoteDB.Execute
		
		lclsErrors = New eFunctions.Errors
		lclsAcc_transa = New eRemoteDB.Execute
		
		On Error GoTo insValCPL002_K_Err
		
		'**+Validations related to column: nLed_compan
		'+ Se valida la columna: nLed_compan
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		'**+Validations related to column: dCloseDate
		'+ Se valida la columna: dCloseDate
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Not IsNothing(dCloseDate) Then
			With lclsAcc_transa
				.StoredProcedure = "valAcc_transa"
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dCloseDate", dCloseDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					If .FieldToClass("lcount") > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 36113)
					End If
					.RCloseRec()
				End If
			End With
		End If
		
		insValCPL002_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsAcc_transa may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAcc_transa = Nothing
		
		
insValCPL002_K_Err: 
		If Err.Number Then
			insValCPL002_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	
	
	
	'**% AccReaOldBalance: return the countable establishment according to the effective date "From"
	'% AccReaOldBalance: Devuelve el saldo anterior a la fecha dada para una cuenta contable
	Public Function AccReaOldBalance(ByVal nLed_compan As Integer, ByVal dEffecdate As Date, ByVal sAccount As String, ByVal sAux_accoun As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo AccReaOldBalance_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaAcc_transaAcc_balance"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nOldBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				AccReaOldBalance = True
				Me.nOldBalance = .Parameters("nBalance").Value
			End If
		End With
		
AccReaOldBalance_err: 
		If Err.Number Then
			AccReaOldBalance = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






