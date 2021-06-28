Option Strict Off
Option Explicit On
Public Class Move_Acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Move_Acc.cls                             $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 10-05-06 13:23                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according the table in the system until 15/02/2001
	'**- The key fields of the table are: nTyp_acco, sType_acc, sClient, nCurrency, dOperdate and nIdConsec.
	'- Propiedades según la tabla en el sistema al 15/02/2001.
	'- El campo llave de la tabla corresponde a: nTyp_acco, sType_acc, sClient, nCurrency, dOperdate y nIdConsec.
	
	'   Column_name                    Type       Computed  Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public nTyp_acco As Integer 'smallint      no       2           5     0     no           (n/a)               (n/a)
	Public sType_acc As String 'char          no       1                       no           no                  no
	Public sClient As String 'char          no      14                       no           no                  no
	Public nCurrency As Integer 'smallint      no       2           5     0     no           (n/a)               (n/a)
	Public dOperdate As Date 'datetime      no       8                       no           (n/a)               (n/a)
	Public nIdconsec As Integer 'int           no       4           10    0     no           (n/a)               (n/a)
	Public nIntermed As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nAmount As Double 'decimal       no       9           10    2     yes          (n/a)               (n/a)
	Public nBankext As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nBranch As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nCertif As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public sCheque As String 'char          no      10                       yes          no                  yes
	Public nClaim As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nCredit As Double 'decimal       no       9           10    2     yes          (n/a)               (n/a)
	Public nDebit As Double 'decimal       no       9           10    2     yes          (n/a)               (n/a)
	Public sDescript As String 'char          no      30                       yes          no                  yes
	Public sManualMov As String 'char          no       1                       yes          no                  yes
	Public nPaynumbe As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nPolicy As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nReceipt As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public sStatregt As String 'char          no       1                       yes          no                  yes
	Public nTransac As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nTransactio As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nType_move As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nType_pay As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nType_tran As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nUsercode As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nProvince As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nIdDocument As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nRequest_nu As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nBordereaux As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public sProcess As String 'char          no       1                       yes          no                  yes
	Public sNumForm As String 'char          no      12                       yes          no                  yes
	Public nOrigCurr As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public nExchange As Double 'decimal       no       9           10    6     yes          (n/a)               (n/a)
	Public sAutoriza As String 'char          no       1                       yes          no                  yes
	Public dValueDate As Date 'datetime      no       8                       yes          (n/a)               (n/a)
	Public nProduct As Integer 'smallint      no       2           5     0     yes          (n/a)               (n/a)
	Public sNull_recor As String 'char          no       1                       yes          no                  yes
	Public nCashNum As Integer 'smallint      no       2           5           yes          no                  yes
	Public sProcess_ind As String 'char          no       1                       yes          no                  yes
	Public nNoteNum As Integer 'int           no       4           10    0     yes          (n/a)               (n/a)
	Public nProponum As Double 'int           no       4           10    0     yes          (n/a)               (n/a)
	'**- Auxiliaries variables
	'- Variables Auxiliares
	
	Public nIdreturn As Integer
	Public nProcess As Integer
	Public nSta_cheque As Integer
	Public sProductDes As String
	Public sBranchDes As String
	
	'**- Properties required by the method Find_CurrAccInq of Move_Accs
	'- Propiedades requeridas por el método Find_CurrAccInq de Move_Accs
	
	Public sShort_des As String
	Public nBalance As Double
	Public nCreditot As Double
	Public nDebitot As Double
	
	'**- Properties required by the method Find_QPayOrderMov of Move_Accs
	'- Propiedades requeridas por el método Find_QPayOrderMov de Move_Accs
	
	Public sAcc_number As String
	Public sBank_des As String
	
	Public dEffecdate As Date
	
	'**- The enumerate type for the units movements according the table 415 was created
	'-+ Se creó el tipo enumerado para los movimientos de unidades según la tabla 415
	
	'**- Initial buy
	'- Compra inicial
	
	Enum eMovement_Units_f
		esdInitialPurchase_f = 1
		'**-Units buy
		'- Compra de unidades
		esdUnitsPurchase_f = 2
		'**- Policy sale
		'- Venta de la poliza
		esdPolicySale_f = 3
		'**- Mediators sale
		'- Ventas a terceros
		esdThirdsSale_f = 4
	End Enum
	
	'**- The enumerate type for the movement of the account is defined
	'- Se define el tipo enumerado para los movimientos de cuenta
	
	Public Enum eMove_Type
		'**- Account initial movement
		'- Movimiento Inicial de Cuenta
		esdInitialMovementAccount = 10
		'**- Movement for deposits
		'- Movimiento para aportes
		esdPay = 14
		'**- Charges by redirection fund
		'- Cargo por redirección de fondo
		esdChargeFoundReaddress = 15
		'**- Administrative charge
		'- Cargo administrativo
		esdAdminCharge = 33
		'**- Rates
		'- Tasas
		esdRate = 63
		'**-Taxes
		'- Impuestos
		esdTax = 64
		'**-Seal
		'- Sellos
		esdSeal = 65
		'**-In/Out of units
		'- Entrada/Salida de unidades
		esdSwitch = 60
		'**-Units purchase
		'- Compra de unidades
		esdValueUnitsSell = 58
		'**-Units sale
		'- Venta de unidades
		esdValueUnitsBuy = 59
		'**-Switch expense
		'- Costo por switch
		esdSwitchCost = 62
	End Enum
	'**% FindMinOper_date: This function finds the greatest date of operation
	'**% from the current account files
	'% FindMinOper_date: Este procedimiento busca la máxima fecha de operación
	'  en el archivo de cuentas corrientes
	Public Function FindMinOper_date(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer) As Boolean
		
		'**- The variable lrecreaMove_AccMin is defined
		'- Se define la variable lrecreaMove_AccMin
		
		Dim lrecreaMove_AccMin As eRemoteDB.Execute
		
		On Error GoTo FindMinOper_date_Err
		lrecreaMove_AccMin = New eRemoteDB.Execute
		FindMinOper_date = False
		
		'**+ Parameter definitions for stored procedure 'insudb.reaMove_AccMin'
		'+ Definición de parámetros para stored procedure 'insudb.reaMove_AccMin'
		'**+ Data of 02/16/2001 al 06:03:33 p.m.
		'+ Información leída el 16/02/2001 06:03:33 p.m.
		
		With lrecreaMove_AccMin
			.StoredProcedure = "reaMove_AccMin"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dOperdate = .FieldToClass("dOperdate")
				FindMinOper_date = True
				.RCloseRec()
			End If
		End With
		
FindMinOper_date_Err: 
		If Err.Number Then
			FindMinOper_date = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_AccMin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_AccMin = Nothing
		On Error GoTo 0
	End Function
	
	'**%ADD: This method is in charge of adding new records to the table "Move_Acc".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Move_Acc". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		
		'**-The variable is defined lreccreMove_Acc
		'- Se define la variable lreccreMove_Acc
		
		Dim lreccreMove_Acc As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreMove_Acc = New eRemoteDB.Execute
		Add = False
		
		'**+ Definitions for the parameters for the stored procedure 'insudb.creMove_Acc'
		'**+ Data of the 02/15/01 at 06:05:18 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.creMove_Acc'
		'+ Información leída el 15/02/2001 06:05:18 p.m.
		
		With lreccreMove_Acc
			.StoredProcedure = "creMove_Acc"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountDec", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sManualmov", sManualMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_pay", nType_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIddocument", nIdDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumform", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigcurr", nOrigCurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutoriza", sAutoriza, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValuedate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNull_recor", sNull_recor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdreturn", nIdreturn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdconsec", nIdconsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCurr_acc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nNotenum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nIdreturn = .Parameters.Item("nIdreturn").Value
				Add = True
			End If
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%Add_Curr_Acc: This method is in charge of adding new records to the table "Move_Acc".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Add_Curr_Acc: Este método se encarga de agregar nuevos registros a la tabla "Move_Acc". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	'%Si la cuenta del usuario no existe (Curr_acc) se crea
	Public Function Add_Curr_Acc() As Boolean
		Dim lrecinsMove_Acc As eRemoteDB.Execute
		
		On Error GoTo Add_Curr_Acc_Err
		lrecinsMove_Acc = New eRemoteDB.Execute
		Add_Curr_Acc = False
		
		'**+ Parameter definitions for the stored procedure 'insudb.insMove_Acc'
		'**+ Data of the 04/30/2001 at 16:34:57
		'+ Definición de parámetros para stored procedure 'insudb.insMove_Acc'
		'+ Información leída el 30/04/2001 16:34:57
		
		Call Description()
		With lrecinsMove_Acc
			.StoredProcedure = "insMove_Acc"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountDec", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sManualMov", sManualMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_pay", nType_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdDocument", nIdDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_Nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutoriza", sAutoriza, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigCurr", nOrigCurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNull_recor", sNull_recor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRem_number", nIdconsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Add_Curr_Acc = True
				nIdconsec = .Parameters.Item("nRem_number").Value
			End If
		End With
		
Add_Curr_Acc_Err: 
		If Err.Number Then
			Add_Curr_Acc = False
		End If
		'UPGRADE_NOTE: Object lrecinsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%Description: this function returns the description of the transaction that is excecuted
	'%Description: esta funcion retorna la descripción de la transacción que se esta ejecutando
	Private Function Description() As Boolean
		Dim lclsQuery As eRemoteDB.Query
		
		On Error GoTo Description_Err
		lclsQuery = New eRemoteDB.Query
		Description = False
		'**+ Stored procedure parameter definition 'insudb.reTable401'
		'+ Definición de parámetros para stored procedure 'insudb.reaTable401'
		'**+ Data of the 04/30/2001 at 09:10:36 am
		'+ Información leída el 30/04/2001 09:10:36 AM
		With lclsQuery
			If .OpenQuery("Table401", "sDescript", "nType_Move=" & CStr(nType_move)) Then
				sDescript = .FieldToClass("sDescript")
				Description = True
				.CloseQuery()
			End If
		End With
		
Description_Err: 
		If Err.Number Then
			Description = False
		End If
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindLastMove: Verifies that the transaction  to be deleted
	'**% is the last associated to the current account
	'% FindLastMove: Verifica que el movimiento a eliminar
	'  sea el último asociado a la cuenta corriente
	Public Function FindLastMove(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer) As Boolean
		
		'**- The variable is defined lrecreaLastMove_Acc2
		'- Se define la variable lrecreaLastMove_Acc2
		
		Dim lrecreaLastMove_Acc2 As eRemoteDB.Execute
		
		On Error GoTo FindLastMove_Err
		lrecreaLastMove_Acc2 = New eRemoteDB.Execute
		FindLastMove = False
		
		'**+ Parameter definitions for stored procedure 'insudb.reaLastMove_Acc2'
		'+ Definición de parámetros para stored procedure 'insudb.reaLastMove_Acc2'
		'+ Información leída el 16/02/2001 05:41:28 p.m.
		
		With lrecreaLastMove_Acc2
			.StoredProcedure = "reaLastMove_Acc2"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nIdconsec = .FieldToClass("nIdConsec")
				dOperdate = .FieldToClass("dOperdate")
				FindLastMove = True
				.RCloseRec()
			End If
		End With
		
FindLastMove_Err: 
		If Err.Number Then
			FindLastMove = False
		End If
		'UPGRADE_NOTE: Object lrecreaLastMove_Acc2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLastMove_Acc2 = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindMaxOper_date: This function finds the greatest date of operation
	'**% from the current account files
	'% FindMaxOper_date: Este procedimiento busca la máxima fecha de operación
	'  en el archivo de cuentas corrientes
	Public Function FindMaxOper_date(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer) As Boolean
		
		'**- The variable lrecreaMove_AccMax is defined
		'- Se define la variable lrecreaMove_AccMax
		
		Dim lrecreaMove_AccMax As eRemoteDB.Execute
		
		On Error GoTo FindMaxOper_date_Err
		lrecreaMove_AccMax = New eRemoteDB.Execute
		FindMaxOper_date = False
		
		'**+ Parameter definitions for stored procedure 'insudb.reaMove_AccMax'
		'+ Definición de parámetros para stored procedure 'insudb.reaMove_AccMax'
		'**+ Data of 02/16/2001 al 06:03:33 p.m.
		'+ Información leída el 16/02/2001 06:03:33 p.m.
		
		With lrecreaMove_AccMax
			.StoredProcedure = "reaMove_AccMax"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dOperdate = .FieldToClass("dOperdate")
				FindMaxOper_date = True
				.RCloseRec()
			End If
		End With
		
FindMaxOper_date_Err: 
		If Err.Number Then
			FindMaxOper_date = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_AccMax may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_AccMax = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindMove: Verify the existence of transaction into the file of
	'**% "current account movements" (Move_Acc)
	'% FindMove: Verifica la existencia del movimiento
	'  en el archivo de "movimientos de cuentas corrientes" (Move_Acc)
	Public Function FindMove(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nIdconsec As Integer) As Boolean
		'**- The variable lrecreaMove_Acc_o is defined
		'- Se define la variable lrecreaMove_Acc_o
		Dim lrecreaMove_Acc_o As eRemoteDB.Execute
		
		On Error GoTo FindMove_Err
		lrecreaMove_Acc_o = New eRemoteDB.Execute
		FindMove = False
		
		'**+ Parameter definitions for the stored procedure 'insudb.reaMove_Acc_o'
		'+ Definición de parámetros para stored procedure 'insudb.reaMove_Acc_o'
		'**+ Data of 02/16/2001 at 02:46:13 p.m.
		'+ Información leída el 16/02/2001 02:46:13 p.m.
		
		With lrecreaMove_Acc_o
			.StoredProcedure = "reaMove_Acc_o"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdConsec", nIdconsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nTyp_acco = .FieldToClass("nTyp_acco")
				Me.sType_acc = .FieldToClass("sType_acc")
				Me.sClient = .FieldToClass("sClient")
				Me.nCurrency = .FieldToClass("nCurrency")
				dOperdate = .FieldToClass("dOperdate")
				Me.nIdconsec = .FieldToClass("nIdConsec")
				nIntermed = .FieldToClass("nIntermed")
				nAmount = .FieldToClass("nAmount")
				nBankext = .FieldToClass("nBankext")
				nBranch = .FieldToClass("nBranch")
				nCertif = .FieldToClass("nCertif")
				sCheque = .FieldToClass("sCheque")
				nClaim = .FieldToClass("nClaim")
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				sDescript = .FieldToClass("sDescript")
				sManualMov = .FieldToClass("sManualMov")
				nPaynumbe = .FieldToClass("nPaynumbe")
				nPolicy = .FieldToClass("nPolicy")
				nReceipt = .FieldToClass("nReceipt")
				sStatregt = .FieldToClass("sStatregt")
				nTransac = .FieldToClass("nTransac")
				nTransactio = .FieldToClass("nTransactio")
				nType_move = .FieldToClass("nType_move")
				nType_pay = .FieldToClass("nType_pay")
				nType_tran = .FieldToClass("nType_tran")
				nUsercode = .FieldToClass("nUsercode")
				nProvince = .FieldToClass("nProvince")
				nIdDocument = .FieldToClass("nIdDocument")
				nRequest_nu = .FieldToClass("nRequest_nu")
				nBordereaux = .FieldToClass("nBordereaux")
				sProcess = .FieldToClass("sProcess")
				sNumForm = .FieldToClass("sNumForm")
				nOrigCurr = .FieldToClass("nOrigCurr")
				nExchange = .FieldToClass("nExchange")
				sAutoriza = .FieldToClass("sAutoriza")
				dValueDate = .FieldToClass("dValueDate")
				nProduct = .FieldToClass("nProduct")
				sNull_recor = .FieldToClass("sNull_recor")
				FindMove = True
				.RCloseRec()
			End If
		End With
		
FindMove_Err: 
		If Err.Number Then
			FindMove = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Acc_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_o = Nothing
		On Error GoTo 0
	End Function
	'% FindMove_Prem_first: Verifica la existencia del movimiento
	'  en el archivo de "movimientos de cuentas corrientes" (Move_Acc)
	Public Function FindMove_Prem_first(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
		'**- The variable lrecreaMove_Prem_first is defined
		'- Se define la variable lrecreaMove_Prem_first
		Dim lrecreaMove_Prem_first As eRemoteDB.Execute
		
		On Error GoTo FindMove_Prem_first_Err
		lrecreaMove_Prem_first = New eRemoteDB.Execute
		FindMove_Prem_first = False
		
		'**+ Parameter definitions for the stored procedure 'insudb.reaMove_Acc_Prem_first'
		'+ Definición de parámetros para stored procedure 'insudb.reaMove_Acc_Prem_first'
		'**+ Data of 13/11/2001 at 05:30 PM
		'+ Información leída el 13/11/2001 05:30 PM
		
		With lrecreaMove_Prem_first
			.StoredProcedure = "reaMove_Acc_Prem_first"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindMove_Prem_first = True
				.RCloseRec()
			End If
		End With
		
FindMove_Prem_first_Err: 
		If Err.Number Then
			FindMove_Prem_first = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaMove_Prem_first may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Prem_first = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindMoveByPeriod: This function verifies the existence of transaction into the file
	'**% of "current account movements" (Move_Acc)
	'% FindMoveByPeriod: Este procedimiento verifica la existencia del movimiento
	'  en el archivo de "movimientos de cuentas corrientes" (Move_Acc)
	Public Function FindMoveByPeriod(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nIdconsec As Integer, ByVal nType_move As Integer) As Boolean
		
		'**- The variable lrecreaMove_Acc_2 is defined
		'- Se define la variable lrecreaMove_Acc_2
		
		Dim lrecreaMove_Acc_2 As eRemoteDB.Execute
		
		On Error GoTo FindMoveByPeriod_Err
		lrecreaMove_Acc_2 = New eRemoteDB.Execute
		FindMoveByPeriod = False
		
		'**+ Parameter definitions for the stored procedure 'insudb.reaMove_Acc_2'
		'**+ Data of 02/16/01 at 05:06:44 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaMove_Acc_2'
		'+ Información leída el 16/02/2001 05:06:44 p.m.
		
		With lrecreaMove_Acc_2
			.StoredProcedure = "reaMove_Acc_2"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdConsec", nIdconsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nTyp_acco = .FieldToClass("nTyp_acco")
				Me.sType_acc = .FieldToClass("sType_acc")
				Me.sClient = .FieldToClass("sClient")
				Me.nCurrency = .FieldToClass("nCurrency")
				dOperdate = .FieldToClass("dOperdate")
				Me.nIdconsec = .FieldToClass("nIdConsec")
				nIntermed = .FieldToClass("nIntermed")
				nAmount = .FieldToClass("nAmount")
				nBankext = .FieldToClass("nBankext")
				nBranch = .FieldToClass("nBranch")
				nCertif = .FieldToClass("nCertif")
				sCheque = .FieldToClass("sCheque")
				nClaim = .FieldToClass("nClaim")
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				sDescript = .FieldToClass("sDescript")
				sManualMov = .FieldToClass("sManualMov")
				nPaynumbe = .FieldToClass("nPaynumbe")
				nPolicy = .FieldToClass("nPolicy")
				nReceipt = .FieldToClass("nReceipt")
				sStatregt = .FieldToClass("sStatregt")
				nTransac = .FieldToClass("nTransac")
				nTransactio = .FieldToClass("nTransactio")
				Me.nType_move = .FieldToClass("nType_move")
				nType_pay = .FieldToClass("nType_pay")
				nType_tran = .FieldToClass("nType_tran")
				nUsercode = .FieldToClass("nUsercode")
				nProvince = .FieldToClass("nProvince")
				nIdDocument = .FieldToClass("nIdDocument")
				nRequest_nu = .FieldToClass("nRequest_nu")
				nBordereaux = .FieldToClass("nBordereaux")
				sProcess = .FieldToClass("sProcess")
				sNumForm = .FieldToClass("sNumForm")
				nOrigCurr = .FieldToClass("nOrigCurr")
				nExchange = .FieldToClass("nExchange")
				sAutoriza = .FieldToClass("sAutoriza")
				dValueDate = .FieldToClass("dValueDate")
				nProduct = .FieldToClass("nProduct")
				sNull_recor = .FieldToClass("sNull_recor")
				FindMoveByPeriod = True
				.RCloseRec()
			End If
		End With
		
FindMoveByPeriod_Err: 
		If Err.Number Then
			FindMoveByPeriod = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Acc_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_2 = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find_document: This function searches by transaction and document type
	'% Find_document: Este procedimiento realiza una búsqueda por
	'% tipo de movimiento y documento
	Public Function Find_document(ByVal nType_move As Integer, ByVal nIdDocument As Integer) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		lrecMove_Acc = New eRemoteDB.Execute
		On Error GoTo Find_document_Err
		Find_document = False
		
		With lrecMove_Acc
			.StoredProcedure = "reaMove_AccnIdDocument"
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdDocument", nIdDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nTyp_acco = .FieldToClass("nTyp_acco")
				sType_acc = .FieldToClass("sType_acc")
				sClient = .FieldToClass("sClient")
				nCurrency = .FieldToClass("nCurrency")
				nRequest_nu = .FieldToClass("nRequest_nu")
				sCheque = .FieldToClass("sCheque")
				nSta_cheque = .FieldToClass("nSta_cheque")
				nAmount = .FieldToClass("nAmount")
				nType_pay = .FieldToClass("nType_pay")
				dOperdate = .FieldToClass("dOperdate")
				Find_document = True
				.RCloseRec()
			End If
		End With
		
Find_document_Err: 
		If Err.Number Then
			Find_document = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'% insCalProposalPremPay: Retorna el monto de pago de prima de la propuesta
	Public Function insCalProposalPremPay(ByVal nProponum As Double) As Double
		Dim lrecinsCalproposalprempay As eRemoteDB.Execute
		On Error GoTo insCalproposalprempay_Err
		
		lrecinsCalproposalprempay = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insCalproposalprempay al 03-20-2002 11:39:13
		'+
		With lrecinsCalproposalprempay
			.StoredProcedure = "insCalproposalprempay"
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCalProposalPremPay = .Parameters("nPremium").Value
			Else
				insCalProposalPremPay = 0
			End If
			
		End With
		
insCalproposalprempay_Err: 
		If Err.Number Then
			insCalProposalPremPay = -1
		End If
		'UPGRADE_NOTE: Object lrecinsCalproposalprempay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCalproposalprempay = Nothing
		On Error GoTo 0
	End Function
	'+busca el ultimo mobimiento generado para una fecha dada
	Public Function FindLastMove2(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Object
		Dim lrecreaLastMove_Acc2 As eRemoteDB.Execute
		
		On Error GoTo FindLastMove_Err
		lrecreaLastMove_Acc2 = New eRemoteDB.Execute
		FindLastMove2 = False
		'+
		'+ Definición de store procedure reaMove_Acc_1 al 04-11-2002 11:11:46
		'+
		With lrecreaLastMove_Acc2
			.StoredProcedure = "reaMove_Acc_1"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nIdconsec = .FieldToClass("nIdConsec")
				dOperdate = .FieldToClass("dOperdate")
				FindLastMove2 = True
				.RCloseRec()
			End If
		End With
		
FindLastMove_Err: 
		If Err.Number Then
			FindLastMove2 = False
		End If
		'UPGRADE_NOTE: Object lrecreaLastMove_Acc2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLastMove_Acc2 = Nothing
		On Error GoTo 0
	End Function
	'+Valida que la fecha obtenida sea la de el último movimiento dado un numero de documento
	Public Function insValLastMoveOP091(ByVal nIdDocument As Integer, ByVal dOperdate As Date) As Boolean
		Dim lrecreaLastMove_AccOP091 As eRemoteDB.Execute
		
		On Error GoTo insValLastMoveOP091_Err
		lrecreaLastMove_AccOP091 = New eRemoteDB.Execute
		insValLastMoveOP091 = False
		
		With lrecreaLastMove_AccOP091
			.StoredProcedure = "reaLastMove_AccOP091"
			.Parameters.Add("nIdDocument", nIdDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If dOperdate = .FieldToClass("dMaxdate") Then
					insValLastMoveOP091 = True
				End If
				.RCloseRec()
			End If
		End With
		
insValLastMoveOP091_Err: 
		If Err.Number Then
			insValLastMoveOP091 = False
		End If
		'UPGRADE_NOTE: Object lrecreaLastMove_AccOP091 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLastMove_AccOP091 = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValOPC012_K: This method validates the header section of the page "OPC012" as described in the
	'**%functional specifications
	'%InsValOPC012_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC012"
	Public Function insValOPC012_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal dOperdate As Date, ByVal nType As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As String
		
		On Error GoTo insValOPC012_K_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsvalClient As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim lclsTab_provider As Object
		Dim lclsGeneral As eGeneral.Company
		Dim lclsAgent As Object
		Dim lclsCurr_acc As eCashBank.Curr_acc
		Dim lclsSecur_sche As eSecurity.Secur_sche
		Dim lclsMove_Accs As eCashBank.Move_Accs
		
		'**- Provider Type
		'- Tipo de proveedor
		
		Dim nType_prov As Integer
		
		'**- Type of valid currency
		'- Tipo de moneda valida
		
		Dim lblnValidCurrency As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsvalClient = New eClient.ValClient
		lclsClient = New eClient.Client
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsGeneral = New eGeneral.Company
		lclsAgent = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Intermedia")
		lclsCurr_acc = New eCashBank.Curr_acc
		lclsSecur_sche = New eSecurity.Secur_sche
		lclsMove_Accs = New eCashBank.Move_Accs
		
		'**+ Validation of the field "Date"
		'+Validación del campo "Fecha"
		
		If dOperdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
		Else
			lclsValField.objErr = lclsErrors
			insValOPC012_K = CStr(lclsValField.ValDate(dOperdate))
		End If
		
		'**+ Validation of the field "Type"
		'+Validación del campo "Tipo"
		
		If nType <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
		End If
		
		'**+ Validation of the client field
		'+Validación del campo cliente
		
		If Trim(sClient) = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 7109,  , eFunctions.Errors.TextAlign.RigthAling, " del cliente")
		Else
			
			'**+ Verifies that the client exists in the client table.
			'+Se verifica que el cliente exista en la tabla de clientes.
			
			If Not lclsvalClient.Validate(sClient, nMainAction) Then
				Select Case lclsvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						lclsErrors.ErrorMessage(sCodispl, 2012)
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						lclsErrors.ErrorMessage(sCodispl, 2013)
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						lclsErrors.ErrorMessage(sCodispl, 2228)
				End Select
			Else
				
				If lclsClient.Find(sClient) Then
					
					'**+ Validate that the client code can be found into the provider table (Tab_provider)
					'**+ if the account type is accourding with hospitals, workshops.
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nType = 4 Or nType = 6 Or nType = 7 Then
						Select Case nType
							'**- Professionals
							Case 4 '- Profesionales
								nType_prov = 3
								'**- Hospitals
							Case 6 '- Clínicas
								nType_prov = 1
								'**- Workshops
							Case 7 '- Talleres
								nType_prov = 2
						End Select
						If Not lclsTab_provider.FindClient(sClient, nType_prov) Then
							lclsErrors.ErrorMessage(sCodispl, 4116)
						End If
					End If
					
					'**+ Validates that the identity code exists in the company table of co/reinsurance
					'+Se valida que el código de la entidad se encuentre registrado en la tabla de
					'+compañias de co/reaseguro
					
					If nType = 2 Or nType = 3 Or nType = 8 Then
						If Not lclsGeneral.FindClient(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 3068)
						End If
					End If
					
					'**+ Validates that the client code exists in the intermediary table
					'**+ if the account type is intermedary
					'+Se valida que el código de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nType = 1 Or nType = 10 Then
						If Not lclsAgent.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
						End If
					End If
				Else
					lclsErrors.ErrorMessage(sCodispl, 7050)
				End If
				
				If lclsCurr_acc.FindCountCurrency(nType, "0", sClient) Then
					If lclsCurr_acc.nCount = 1 Then
						If lclsCurr_acc.FindClientCurr_acc(nType, "0", sClient, eRemoteDB.Constants.intNull) Then
						End If
						lblnValidCurrency = False
					Else
						lblnValidCurrency = True
					End If
				Else
					lblnValidCurrency = True
				End If
			End If
		End If
		
		'**+ Validations of the field "currency"
		'+Validaciones del campo "Moneda"
		
		If nCurrency <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 10827)
		Else
			
			'**+ If the transaction type is not a credit note, and the zone fields, (branch office)
			'***+ and the currency are filled, the cash account must exist
			'+ Si el tipo de movimiento no corresponde a nota de credito y los campos
			'+ zona (sucursal) y moneda se encuentran llenos, la cuenta de caja debe
			'+ estar registrada
			
			If Not lclsSecur_sche.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
			End If
		End If
		
		If lblnValidCurrency Then
			
			'**+ Validates that exist a current account with the fields key combinations
			'**+ (client, account type, business type)
			'+ Se valida que exista una cuenta corriente con la combinación de los campos
			'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
			
			If Not lclsCurr_acc.FindClientCurr_acc(nType, "0", sClient, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 7122)
			End If
		End If
		
		'**+ Validates an existing record and send the message that depend on the
		'**+ action where is found (add or query)
		'+Se valida la existencia de un registro previo, y se envia el mensaje correspondiente
		'+dependiendo de la accion en que se encuentre (registrar o consultar)
		
		If Trim(lclsErrors.Confirm) = String.Empty Then
			If Not lclsMove_Accs.FindMoveAcc_OPC012(nType, "0", sClient, nCurrency, dOperdate) Then
				lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
		End If
		
		insValOPC012_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalClient = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lclsAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent = Nothing
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecur_sche = Nothing
		'UPGRADE_NOTE: Object lclsMove_Accs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Accs = Nothing
		
insValOPC012_K_Err: 
		If Err.Number Then
			insValOPC012_K = "insValOPC012_K: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% UpdBymanualMove: Updates the current account tables and current account transactions
	'**% (Curr_acc and Move_Acc) depending on the process type
	'% UpdByManualMove: Actualiza las tablas de Cuentas Corrientes y
	'% Movimientos de Cuentas Corrientes (Curr_acc y Move_Acc) dependiendo
	'% del tipo de proceso
	Public Function UpdByManualMove() As Boolean
		'**- Define the variable lrecinsCreMove_AccOP092
		'- Se define la variable lrecinsCreMove_AccOP092
		
		Dim lrecinsCreMove_AccOP092 As eRemoteDB.Execute
		lrecinsCreMove_AccOP092 = New eRemoteDB.Execute
		
		On Error GoTo UpdByManualMove_Err
		
		'**+ Parameter definitions for the stored procedure 'insudb.insCreMove_AccOP092'
		'**+ Data of 02/19/2001 at 11:36:53 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.insCreMove_AccOP092'
		'+ Información leída el 19/02/2001 11:36:53 a.m.
		
		With lrecinsCreMove_AccOP092
			.StoredProcedure = "insCreMove_AccOP092"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdconsec", nIdconsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValuedate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If nProcess = 1 Then
				If .Run Then
					nIdconsec = .FieldToClass("nIdConsec")
					.RCloseRec()
					UpdByManualMove = True
				Else
					UpdByManualMove = False
				End If
			Else
				If .Run Then
					UpdByManualMove = True
				Else
					UpdByManualMove = False
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsCreMove_AccOP092 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCreMove_AccOP092 = Nothing
		
UpdByManualMove_Err: 
		If Err.Number Then
			UpdByManualMove = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOPC011_K: This method validates the header section of the page "OPC011" as described in the
	'**%functional specifications
	'%InsValOPC011_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC011"
	Public Function insValOPC011_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nTypeAccount As Integer, ByVal sClient As String, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalClient As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim lclsValTime As eFunctions.valField
		Dim lclsTab_provider As Object
		Dim lclsCompany As eGeneral.Company
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsCurr_acc As eCashBank.Curr_acc
		Dim lclsSecurity As eSecurity.Secur_sche
		
		Dim lblnValidCurency As Boolean
		Dim lintType_prov As Integer
		Dim lstrClient As String
		
		lclsErrors = New eFunctions.Errors
		lclsValTime = New eFunctions.valField
		lclsvalClient = New eClient.ValClient
		lclsClient = New eClient.Client
		lclsCompany = New eGeneral.Company
		lclsIntermedia = New eAgent.Intermedia
		lclsCurr_acc = New eCashBank.Curr_acc
		lclsSecurity = New eSecurity.Secur_sche
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_provider")
		
		
		On Error GoTo insValOPC011_K_Err
		
		insValOPC011_K = String.Empty
		
		'**+Validation of the field "Date"
		'+Validacion del campo "Fecha"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
		Else
			lclsValTime.objErr = lclsErrors
			lclsValTime.ValDate(dEffecdate)
		End If
		
		'**+Validation of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
		End If
		
		'**+Validation of the field "Client"
		'+Validacion del campo "Client"
		
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 12043)
		Else
			If Not lclsvalClient.Validate(sClient, nAction) Then
				Select Case lclsvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						lclsErrors.ErrorMessage(sCodispl, 2012)
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						lclsErrors.ErrorMessage(sCodispl, 2013)
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						lclsErrors.ErrorMessage(sCodispl, 2228)
				End Select
			Else
				lstrClient = lclsvalClient.ClientCode
				If lclsClient.Find(lstrClient) Then
					
					'**+ Validates that the client code exists in the providers table (Tab_provider)
					'**+ if the account type correspond to hospitals, workshops
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nTypeAccount = 4 Or nTypeAccount = 6 Or nTypeAccount = 7 Then
						
						Select Case nTypeAccount
							'**- Professionals
							'- Profesionales
							Case 4
								lintType_prov = 3
								'**- Hospitals
								'- Clinicas
							Case 6
								lintType_prov = 1
								'**- Workshops
								'- Talleres
							Case 7
								lintType_prov = 2
							Case Else
								lintType_prov = eRemoteDB.Constants.intNull
						End Select
						
						If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
							lclsErrors.ErrorMessage(sCodispl, 4116)
						End If
					End If
					
					
					'**+ Validates that the entity exists in the companies tables of co/reinsurance
					'+Se valida que el código de la entidad se encuentre registrado en la table de
					'+compañias de co/reaseguro
					
					If nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8 Then
						
						If Not lclsCompany.FindClient(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 3068)
						End If
					End If
					
					'**+ Validates that the client code exists in the indermediary table if the
					'**+ account type is of a indermediary
					'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nTypeAccount = 1 Or nTypeAccount = 10 Then
						If Not lclsIntermedia.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
						End If
					End If
				Else
					
					'**+ The clients module is only called if the account type correspond to clients.
					'**+ In other case send the correspond validation
					'+ Solamente se llama al modulo de clientes, si el tipo de cuente corresponde a
					'+ clientes. En caso contrario se debe enviar la validacion correspondiente
					
					lclsErrors.ErrorMessage(sCodispl, 7050)
				End If
				If lclsCurr_acc.AccCount(nTypeAccount, "0", sClient) Then
					If lclsCurr_acc.nCount = 1 Then
						lblnValidCurency = False
					Else
						lblnValidCurency = True
					End If
					
				Else
					lblnValidCurency = True
				End If
			End If
		End If
		
		'**+Validations of the field "Currency"
		'+Validaciones del campo "Moneda"
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 10827)
		Else
			
			'**+ Must be allowed for the security scheme of the user that makes the query
			'+ Debe estar permitida para el esquema de seguridad del usuario que realiza la consulta
			
			If Not lclsSecurity.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
			End If
			
			If lblnValidCurency Then
				
				'**+ Validates that a current account exist with the field key combinations
				'**+ (client, account type, business type)
				'+ Se valida que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
				
				If Not lclsCurr_acc.FindClientCurr_acc(nTypeAccount, "0", sClient, nCurrency) Then
					lclsErrors.ErrorMessage(sCodispl, 7122)
				End If
			End If
		End If
		
		insValOPC011_K = lclsErrors.Confirm
		
insValOPC011_K_Err: 
		If Err.Number Then
			insValOPC011_K = "insValOPC011_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTime = Nothing
		'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalClient = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurity = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValOPC010_K: This method validates the header section of the page "OPC010" as described in the
	'**%functional specifications
	'%InsValOPC010_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC010"
	Public Function insValOPC010_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date, ByVal nTyp_acco As Integer, ByVal nBussType As Integer, ByVal nCurrency As Integer, ByVal sClient As String, ByVal sTypeCurr_acc As String, ByVal sCertype As String, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicyNum As Integer) As String
		Dim lblnValOPC010_K As Boolean
		Dim lintType_prov As Integer
		Dim lstrType_acc As String
		Dim lstrClient As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_provider As Object
		Dim lclsCompany As eGeneral.Company
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsCurr_acc As Curr_acc
		Dim lclsClient As eClient.Client
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		
		lclsErrors = New eFunctions.Errors
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsCompany = New eGeneral.Company
		lclsIntermedia = New eAgent.Intermedia
		lclsCurr_acc = New Curr_acc
		lclsClient = New eClient.Client
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		
		On Error GoTo insValOPC010_K_Err
		
		lblnValOPC010_K = True
		
		If nTyp_acco = eRemoteDB.Constants.intNull Then
			nTyp_acco = 0
		End If
		
		If nBussType = eRemoteDB.Constants.intNull Then
			nBussType = 0
		End If
		
		If nCurrency = eRemoteDB.Constants.intNull Then
			nCurrency = 0
		End If
		
		If nBranch = eRemoteDB.Constants.intNull Then
			nBranch = 0
		End If
		
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		
		If nPolicy = eRemoteDB.Constants.intNull Then
			nPolicy = 0
		End If
		
		If nCertif = eRemoteDB.Constants.intNull Then
			nCertif = 0
		End If
		
		'**+Validation of the transaction date
		'+Validación del fecha del movimiento
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 4095)
			lblnValOPC010_K = False
		End If
		
		'**+Type of current account
		'+Tipo de cuenta corriente
		
		If nTyp_acco = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
			lblnValOPC010_K = False
		End If
		
		'**+Business type
		'**+ When the current account correspond to coinsuranse or reinsurance
		'+Tipo de negocio
		'+Cuando la cuenta corriente corresponda a coaseguro o reaseguro
		
		If nTyp_acco <> 0 Then
			If nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8 Then
				If nBussType = 0 Then
					lclsErrors.ErrorMessage(sCodispl, 7250)
					lblnValOPC010_K = False
				End If
			End If
		End If
		
		'**+Client code
		'+Código del Cliente
		
		If sTypeCurr_acc <> "1" Then 'Si el tipo de cuenta corriente no es por póliza
			If sClient = String.Empty Then
				lblnValOPC010_K = False
				lclsErrors.ErrorMessage(sCodispl, 2001)
			Else
				If lclsClient.Find(sClient) Then
					
					'**+ Validates that the client code exists in the provider table (Tab_provider)
					'**+ if the current account correspond to Hospitals, Workshops.
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nTyp_acco = 4 Or nTyp_acco = 6 Or nTyp_acco = 7 Then
						Select Case nTyp_acco
							
							'**-Professionals
							'- Profesionales
							Case 4
								lintType_prov = 3
								'**-Hospitals
								'- Clinicas
							Case 6
								lintType_prov = 1
								
								'**-Workshops
								'- Talleres
							Case 7
								lintType_prov = 2
								
							Case Else
								lintType_prov = eRemoteDB.Constants.intNull
						End Select
						
						If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
							
							'+ reaTab_provider_sClient
							lclsErrors.ErrorMessage(sCodispl, 4116)
							lblnValOPC010_K = False
						End If
					End If
					
					'**+Validates that the entity code exists in the companies table of co/reinsurance
					'+Se valida que el código de la entidad se encuentre registrado en la table de
					'+compañias de co/reaseguro
					
					If nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8 Then
						If Not lclsCompany.FindClient(sClient) Then 'reaCompany_sClient
							lclsErrors.ErrorMessage(sCodispl, 3068)
							lblnValOPC010_K = False
						End If
					End If
					
					'**+Validates that the client code exists in the intermediary table if
					'**+the account type is intermediary
					'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nTyp_acco = 1 Or nTyp_acco = 10 Then
						If Not lclsIntermedia.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
							lblnValOPC010_K = False
						End If
					End If
				Else
					
					'**+Error if the client doesn´t exist
					'+ Error en caso de no existir el cliente
					
					lclsErrors.ErrorMessage(sCodispl, 7050)
					lblnValOPC010_K = False
				End If
				
			End If
			
			'**+If the validation is masive, the client code is right and the currency field is correct,
			'**+validate that a number of a current account exist for this condition
			'+Si la validación es masiva, el código del clientes es correcto y el campo moneda es correcto,
			'+se valida que exista un número de cuenta corriente para esta condición
			
			If lblnValOPC010_K = True Then
				
				'**+Validates that a current account exist with the combination of the key fields
				'**+(client, account type, business type)
				'+ Se valida que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
				
				If nBussType = 0 Then
					lstrType_acc = "0"
				Else
					lstrType_acc = CStr(nBussType)
				End If
				
				If Not lclsCurr_acc.AccCount(nTyp_acco, lstrType_acc, sClient) Then
					lblnValOPC010_K = False
					lclsErrors.ErrorMessage(sCodispl, 7111)
				End If
				
				'**+Validate sthat a current account exist with the combination of the key fields
				'**+(client, account type, business type ans currency)
				'+ Se valida que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio y Moneda)
				
				If nBussType = 0 Then
					lstrType_acc = "0"
				Else
					lstrType_acc = CStr(nBussType)
				End If
				
				If Not lclsCurr_acc.FindClientCurr_acc(nTyp_acco, lstrType_acc, sClient, nCurrency) Then 'reaCurr_acc_o
					lblnValOPC010_K = False
					lclsErrors.ErrorMessage(sCodispl, 7259)
				End If
			End If
		End If
		
		'**+If the current account is by policy
		'+Si el tipo de cuenta corriente es por póliza
		
		If sTypeCurr_acc = "1" Then
			
			'**+branch
			'+Ramo
			
			If nBranch = 0 Then
				lblnValOPC010_K = False
				lclsErrors.ErrorMessage(sCodispl, 9064)
			End If
			
			'**+Product
			'+Producto
			
			If nProduct = 0 Then
				lblnValOPC010_K = False
				lclsErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			'**+Policy
			'+Póliza
			
			If nPolicy = 0 Then
				lblnValOPC010_K = False
				lclsErrors.ErrorMessage(sCodispl, 3003)
			Else
				If Not lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
					lblnValOPC010_K = False
					lclsErrors.ErrorMessage(sCodispl, 3001)
				End If
			End If
			
			'**+Certificate
			'+Certificado
			
			If nCertif = 0 And lclsPolicy.sPolitype <> "1" Then
				lblnValOPC010_K = False
				lclsErrors.ErrorMessage(sCodispl, 3006)
			Else
				If nCertif <> 0 Then
					If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						lclsErrors.ErrorMessage(sCodispl, 3010)
						lblnValOPC010_K = False
					End If
				End If
			End If
			
			With lclsCurr_acc
				
				'**+If the validation is masive, the client code is right and the currency field is correct
				'**+validate that a current account number exist for this condition
				'+Si la validación es masiva, el código del clientes es correcto y el campo moneda es correcto,
				'+se valida que exista un número de cuenta corriente para esta condición
				
				If lblnValOPC010_K = True Then
					
					'**+Validates that a current account exist with the combination of the key fields
					'**+(account type, business type, branch, product, policy and certificate)
					'+ Se valida que exista una cuenta corriente con la combinación de los campos
					'+claves (Tipo de cuenta, Tipo de negocio,Ramo,Producto,Póliza y certificado)
					
					.nTyp_acco = nTyp_acco
					
					If nBussType = 0 Then
						lstrType_acc = "0"
					Else
						lstrType_acc = CStr(nBussType)
					End If
					
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = nPolicy
					.nCertif = nCertif
					
					If sTypeCurr_acc <> "1" Then
						If .Curr_CountPol = 0 Then 'reaCurr_acc_countPol
							lblnValOPC010_K = False
							lclsErrors.ErrorMessage(sCodispl, 7111)
						End If
					End If
					
					'**+Validates that a current account exists with the combination of the key fields
					'**+(account type, business type, branch, product, policy and certificate)
					'+ Se valida que exista una cuenta corriente con la combinación de los campos
					'+claves (Tipo de cuenta, Tipo de negocio,Ramo,Producto,Póliza y certificado)
					
					If nBussType = 0 Then
						lstrType_acc = "0"
					Else
						lstrType_acc = CStr(nBussType)
					End If
					
					If Not .Find(nTyp_acco, lstrType_acc, nBranch, nProduct, nPolicy, nCertif, nCurrency) Then 'reaCurr_acc
						lblnValOPC010_K = False
						lclsErrors.ErrorMessage(sCodispl, 15200)
					End If
				End If
			End With
		End If
		
		'**+Currency
		'+Moneda
		
		If nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7132)
			lblnValOPC010_K = False
		End If
		
		
		insValOPC010_K = lclsErrors.Confirm
		
insValOPC010_K_Err: 
		If Err.Number Then
			insValOPC010_K = insValOPC010_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		On Error GoTo 0
	End Function
	
	
	'**%insValOPC015_K: This method validates the header section of the page "OPC015" as described in the
	'**%functional specifications
	'%InsValOPC015_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC015"
	Public Function insValOPC015_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal sClient As String, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalClient As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim lclsValTime As eFunctions.valField
		Dim lclsTab_provider As Object
		Dim lclsCompany As eGeneral.Company
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsCurr_acc As eCashBank.Curr_acc
		Dim lclsSecurity As eSecurity.Secur_sche
		
		Dim lblnValidCurency As Boolean
		Dim lintType_prov As Integer
		Dim lstrClient As String
		
		lclsErrors = New eFunctions.Errors
		lclsValTime = New eFunctions.valField
		lclsvalClient = New eClient.ValClient
		lclsClient = New eClient.Client
		lclsCompany = New eGeneral.Company
		lclsIntermedia = New eAgent.Intermedia
		lclsSecurity = New eSecurity.Secur_sche
		lclsCurr_acc = New eCashBank.Curr_acc
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		
		On Error GoTo insValOPC015_K_Err
		
		insValOPC015_K = String.Empty
		
		'**+Validation of the field "Date"
		'+Validacion del campo "Fecha"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
		Else
			lclsValTime.objErr = lclsErrors
			lclsValTime.ValDate(dEffecdate)
		End If
		
		'**+Validation of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
		End If
		
		'**+Validation of the field "Client"
		'+Validacion del campo "Client"
		
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 12043)
		Else
			If Not lclsvalClient.Validate(sClient, nAction) Then
				Select Case lclsvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						lclsErrors.ErrorMessage(sCodispl, 2012)
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						lclsErrors.ErrorMessage(sCodispl, 2013)
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						lclsErrors.ErrorMessage(sCodispl, 2228)
				End Select
			Else
				lstrClient = lclsvalClient.ClientCode
				If lclsClient.Find(lstrClient) Then
					
					'**+Validates that the client code is into the providers table (Tab_provider)
					'**+if the account type correspond to hospitals, workshops
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nTypeAccount = 4 Or nTypeAccount = 6 Or nTypeAccount = 7 Then
						Select Case nTypeAccount
							'**-Professionals
							'- Profesionales
							Case 4
								lintType_prov = 3
								'**-Hospitals
								'- Clinicas
							Case 6
								lintType_prov = 1
								'**-Workshops
								'- Talleres
							Case 7
								lintType_prov = 2
							Case Else
								lintType_prov = eRemoteDB.Constants.intNull
						End Select
						
						If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
							lclsErrors.ErrorMessage(sCodispl, 4116)
						End If
					End If
					
					'**+Validates that the entity code exists in the companies table of co/reinsurance
					'+Se valida que el código de la entidad se encuentre registrado en la table de
					'+compañias de co/reaseguro
					
					If nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8 Then
						
						If Not lclsCompany.FindClient(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 3068)
						End If
					End If
					
					'**+Validates that the client code exists in the intermediary table if the account type
					'**+is intermediary
					'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nTypeAccount = 1 Or nTypeAccount = 10 Then
						If Not lclsIntermedia.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
						End If
					End If
				Else
					
					'**+The client module is only call if the account type correspond to a clients. In other case
					'**+send the correspond validation
					'+ Solamente se llama al modulo de clientes, si el tipo de cuente corresponde a
					'+ clientes. En caso contrario se debe enviar la validacion correspondiente
					
					lclsErrors.ErrorMessage(sCodispl, 7050)
				End If
			End If
			If lclsCurr_acc.AccCount(nTypeAccount, sBussiType, sClient) Then
				If lclsCurr_acc.nCount = 1 Then
					lblnValidCurency = False
				Else
					lblnValidCurency = True
				End If
				
			Else
				lblnValidCurency = True
			End If
		End If
		
		'**+Validation of the field "Currency"
		'+Validaciones del campo "Moneda"
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 10827)
		Else
			
			'**+It must be allowed for the security scheme of the user that is making the consult
			'+ Debe estar permitida para el esquema de seguridad del usuario que realiza la consulta
			
			If Not lclsSecurity.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
			End If
			
			If lblnValidCurency Then
				
				'**+Validates that a current account exists with the combination of the key fields
				'**+(client, account type, business type)
				'+ Se valid que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
				
				If Not lclsCurr_acc.FindClientCurr_acc(nTypeAccount, sBussiType, sClient, nCurrency) Then
					lclsErrors.ErrorMessage(sCodispl, 7122)
				End If
			End If
		End If
		
		insValOPC015_K = lclsErrors.Confirm
		
insValOPC015_K_Err: 
		If Err.Number Then
			insValOPC015_K = "insValOPC015_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTime = Nothing
		'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalClient = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurity = Nothing
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValOPC014_K: This method validates the header section of the page "OPC014" as described in the
	'**%functional specifications
	'%InsValOPC014_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC014"
	Public Function insValOPC014_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dOperdate As Date, ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As String
		
		Dim lstrClient As String
		Dim lintType_prov As Integer
		Dim lblnValOPC014_k As Boolean
		Dim lblnValidCurency As Boolean
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_provider As Object
		Dim lclsCompany As eGeneral.Company
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsCurr_acc As Curr_acc
		Dim lclsvalClient As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim lclsValField As eFunctions.valField
		Dim lclsMove_Accs As eCashBank.Move_Accs
		Dim lclsSecurity As eSecurity.Secur_sche
		
		lclsErrors = New eFunctions.Errors
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsCompany = New eGeneral.Company
		lclsIntermedia = New eAgent.Intermedia
		lclsCurr_acc = New Curr_acc
		lclsvalClient = New eClient.ValClient
		lclsClient = New eClient.Client
		lclsValField = New eFunctions.valField
		lclsMove_Accs = New eCashBank.Move_Accs
		lclsSecurity = New eSecurity.Secur_sche
		
		On Error GoTo insValOPC014_K_Err
		
		lblnValOPC014_k = True
		
		'**+Validation of the field "Date"
		'+Validacion del campo "Fecha"
		
		If dOperdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
			lblnValOPC014_k = False
		Else
			lclsValField.objErr = lclsErrors
			lblnValOPC014_k = lclsValField.ValDate(dOperdate)
		End If
		
		'**+Validation of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTyp_acco = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
			lblnValOPC014_k = False
		End If
		
		'**+Business Type"
		'+Tipo de negocio
		'**+When the current account correspond to a coisurance or reinsurance
		'+Cuando la cuenta corriente corresponda a coaseguro o reaseguro
		
		If nTyp_acco <> 0 Then
			If nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8 Then
				If sType_acc = "0" Then
					lclsErrors.ErrorMessage(sCodispl, 7250)
					lblnValOPC014_k = False
				End If
			End If
		End If
		
		'**+Validation of the field "Client"
		'+Validacion del campo "Client"
		
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 12043)
			lblnValOPC014_k = False
		Else
			If Not lclsvalClient.Validate(sClient, nAction) Then
				Select Case lclsvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						lblnValOPC014_k = False
						lclsErrors.ErrorMessage(sCodispl, 2012)
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						lblnValOPC014_k = False
						lclsErrors.ErrorMessage(sCodispl, 2013)
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						lblnValOPC014_k = False
						lclsErrors.ErrorMessage(sCodispl, 2228)
				End Select
			Else
				lstrClient = lclsvalClient.ClientCode
				If lclsClient.Find(lstrClient) Then
					
					'**+Validates that the client code is in the providers table (Tab_provider)
					'**+if the account type correspond to hospitals, workshops.
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nTyp_acco = 4 Or nTyp_acco = 6 Or nTyp_acco = 7 Then
						Select Case nTyp_acco
							
							'**-Professionals
							'- Profesionales
							Case 4
								lintType_prov = 3
								
								'**-Hospitals
								'- Clinicas
							Case 6
								lintType_prov = 1
								
								'**- Workshops
								'- Talleres
							Case 7
								lintType_prov = 2
							Case Else
								lintType_prov = eRemoteDB.Constants.intNull
						End Select
						If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
							
							'**+ reaTab_provider_sClient
							
							lclsErrors.ErrorMessage(sCodispl, 4116)
							lblnValOPC014_k = False
						End If
					End If
					
					'**+Validates that the entity code exists in the companies table of co/reinsurance
					'+Se valida que el código de la entidad se encuentre registrado en la table de
					'+compañias de co/reaseguro
					
					If nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8 Then
						If Not lclsCompany.FindClient(sClient) Then 'reaCompany_sClient
							lclsErrors.ErrorMessage(sCodispl, 3068)
							lblnValOPC014_k = False
						End If
					End If
					
					'**+Validates that the client code exists in the intermediaries table if the
					'**+account type is intermediary
					'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nTyp_acco = 1 Or nTyp_acco = 10 Then
						If Not lclsIntermedia.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
							lblnValOPC014_k = False
						End If
					End If
				Else
					
					'**+The client module is only call if the account correspond to a clients. In other case
					'**+send the correspond validation.
					'+ Solamente se llama al modulo de clientes, si el tipo de cuente corresponde a
					'+ clientes. En caso contrario se debe enviar la validacion correspondiente
					
					lblnValOPC014_k = False
					lclsErrors.ErrorMessage(sCodispl, 7050)
				End If
			End If
			With lclsCurr_acc
				.nTyp_acco = nTyp_acco
				.sType_acc = sType_acc
				.sClient = sClient
				If .CurrenAsoc_count = 1 Then 'reaCurr_acc_Curren_Count
					lblnValidCurency = False
				Else
					lblnValidCurency = True
				End If
			End With
		End If
		
		'**+Validation of the field "Currency"
		'+Validaciones del campo "Moneda"
		
		If nCurrency = 0 Then
			lblnValOPC014_k = False
			lclsErrors.ErrorMessage(sCodispl, 10827)
		Else
			
			'**+ If the transaction type doesn`t correspond to a credit note and the zone fields (branch office)
			'**+ and the currency are fill, the cash account must be registered
			'+ Si el tipo de movimiento no corresponde a nota de credito y los campos
			'+ zona (sucursal) y moneda se encuentran llenos, la cuenta de caja debe
			'+ estar registrada
			
			If Not lclsSecurity.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
				lblnValOPC014_k = False
			End If
			
			If lblnValidCurency Then
				
				'**+Validates that a current account exist with the key fields combination
				'**+ (Client, Account Type, Business Type)
				'+ Se valida que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
				
				If Not lclsCurr_acc.FindClientCurr_acc(nTyp_acco, "0", sClient, nCurrency) Then
					lblnValOPC014_k = False
					lclsErrors.ErrorMessage(sCodispl, 7122)
				End If
			End If
		End If
		
		'**+Validates the existance of a previous record and send the message according to the action
		'**+where is found (register or consulting)
		'+Se valida la existencia de un registro previo, y se envia el mensaje correspondiente
		'+dependiendo de la accion en que se encuentre (registrar o consultar)
		
		If lblnValOPC014_k Then
			If Not lclsMove_Accs.Find_QPayOrderMov(nTyp_acco, sType_acc, sClient, nCurrency, dOperdate) Then
				lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
		End If
		
		insValOPC014_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalClient = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsMove_Accs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Accs = Nothing
		'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurity = Nothing
		
insValOPC014_K_Err: 
		If Err.Number Then
			insValOPC014_k = insValOPC014_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insUpdateMove_AccVI010: This function actualize the Move_Acc file and also return the operation number
	'**%for it to be keep in the move_fund
	'%insUpdateMove_AccVI010: Funcion que actualiza el archivo de Move_Acc y ademas retorna
	'% el número de la operación para que sea almacenada en el move_fund
	'%---------------------------------------------------------------------------
	Public Function insUpdateMove_AccVI010(ByRef nType_move As Integer, ByRef nUnitsChange As Double) As Integer
		'%---------------------------------------------------------------------------
		
		'**+ Records are generate in the account movements file Move_Acc
		'+ Se generan registros en el archivo de movimientos de cuentas
		'+ Move_Acc
		
		With Me
			.nBranch = nBranch
			.nCertif = nCertif
			.nPolicy = nPolicy
			.nProduct = nProduct
			.nUsercode = nUsercode
			.nTyp_acco = CInt("5")
			.sType_acc = CStr(0)
			.nPolicy = nPolicy
			.sClient = sClient
			.nCurrency = nCurrency
			.dOperdate = dOperdate
			.dValueDate = dEffecdate
			.sStatregt = "1"
			
			'**+The SWITCH movement is genereded by the units entry
			'+ Genera el movimiento de SWITCH por entrada de unidades
			
			If (nType_move = eMovement_Units_f.esdUnitsPurchase_f Or nType_move = 68) Then '68 corresponde a la compra de unidades
				.nAmount = nUnitsChange
				.nCredit = nUnitsChange
				.nDebit = 0
				.nType_move = eMove_Type.esdSwitch
				
				'**+ The SWITCH movement is generated by the units setting
				'+ Genera el movimiento de SWITCH por salida de unidades
				
			ElseIf (nType_move = eMovement_Units_f.esdPolicySale_f Or nType_move = 69) Then  '**68 correspond to the units sale
				'68 corresponde a la venta de unidades
				.nAmount = nUnitsChange
				.nCredit = 0
				.nDebit = nUnitsChange
				.nType_move = eMove_Type.esdSwitch
			End If
			
			'** Add the switches record
			'+ Agrega el registro de switches
			.nCashNum = nCashNum
			.Add_Curr_Acc()
			
			'**+ Returns the generated value in Move_Acc to related with move_fund by the concept
			'**+ of entry or setting units
			'+ Se retorna el valor generado en Move_Acc para relacionarlo con
			'+ move_fund por concepto de entrada o salida de unidades
			
			insUpdateMove_AccVI010 = .nIdconsec
		End With
		
	End Function
	
	'**%FindLastMoveF: Function that finds the latest cuurent account transactions
	'%FindLastMoveF: Función para buscar el ultimo de los movimientos de
	'%cuenta corriente
	Public Function FindLastMoveF() As Boolean
		FindLastMoveF = False
		
		Dim lrecReaLastMove_Acc As eRemoteDB.Execute
		
		lrecReaLastMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo FindLastMoveF_Err
		
		'**+ Parameter definitions for the stored procedure 'insudb.ReaLastMove_Acc'
		'**+ Data of 11/03/1999 09:03:40 AM
		'+ Definición de parámetros para stored procedure 'insudb.ReaLastMove_Acc'
		'+ Información leída el 03/11/1999 09:03:40 AM
		
		With lrecReaLastMove_Acc
			.StoredProcedure = "ReaLastMove_Acc"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nTyp_acco = .FieldToClass("nTyp_acco")
				sType_acc = .FieldToClass("sType_acc")
				nAmount = .FieldToClass("nAmount")
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				sDescript = .FieldToClass("sDescript")
				nTransac = .FieldToClass("nTransac")
				nTransactio = .FieldToClass("nTransactio")
				nType_pay = .FieldToClass("nType_pay")
				nType_tran = .FieldToClass("nType_tran")
				FindLastMoveF = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaLastMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLastMove_Acc = Nothing
		
FindLastMoveF_Err: 
		If Err.Number Then
			FindLastMoveF = False
		End If
	End Function
	
	'**% Find_nProponum:
	'% lee si existe movimiento para de ingreso de primera prima
	Public Function Find_nProponum(ByVal nProponum As Double) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		lrecMove_Acc = New eRemoteDB.Execute
		On Error GoTo Find_nProponum_Err
		Find_nProponum = False
		
		With lrecMove_Acc
			.StoredProcedure = "reaMove_AccPropoNum"
			.Parameters.Add("nPropoNum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dOperdate = .FieldToClass("dOperdate")
				Find_nProponum = True
				.RCloseRec()
			End If
		End With
		
Find_nProponum_Err: 
		If Err.Number Then
			Find_nProponum = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_nProponum_o: Obtiene la información del movimiento de propuestas en la tabla Move_Acc.
	Public Function Find_nProponum_o(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		lrecMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo Find_nProponum_o_Err
		
		With lrecMove_Acc
			.StoredProcedure = "reaMove_AccPropoNum_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPropoNum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nTyp_acco = .FieldToClass("nTyp_acco")
				Me.sType_acc = .FieldToClass("sType_acc")
				Me.nType_move = .FieldToClass("nType_move")
				Me.dOperdate = .FieldToClass("dOperdate")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.sProcess_ind = .FieldToClass("sProcess_ind")
				Me.nAmount = .FieldToClass("nAmount")
				Me.sClient = .FieldToClass("sClient")
				Me.nBordereaux = .FieldToClass("nBordereaux")
				Find_nProponum_o = True
				.RCloseRec()
			End If
		End With
		
Find_nProponum_o_Err: 
		If Err.Number Then
			Find_nProponum_o = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_nProponum_a: Permite obtener el registro  valido para la llave pasada como parametro
	Public Function Find_nProponum_a(ByVal nProponum As Double) As Boolean
		Dim lrecreaMove_Accproponum_a As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo reaMove_Accproponum_a_Err
		
		lrecreaMove_Accproponum_a = New eRemoteDB.Execute
		
		With lrecreaMove_Accproponum_a
			.StoredProcedure = "reaMove_Accproponum_a"
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_nProponum_a = (.Parameters("nExists").Value = 1)
		End With
		
reaMove_Accproponum_a_Err: 
		If Err.Number Then
			Find_nProponum_a = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Accproponum_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Accproponum_a = Nothing
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nTyp_acco = eRemoteDB.Constants.intNull
		Me.sType_acc = String.Empty
		Me.sClient = String.Empty
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.dOperdate = dtmNull
		Me.nIdconsec = eRemoteDB.Constants.intNull
		Me.nIntermed = eRemoteDB.Constants.intNull
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nBankext = eRemoteDB.Constants.intNull
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nCertif = eRemoteDB.Constants.intNull
		Me.sCheque = String.Empty
		Me.nClaim = eRemoteDB.Constants.intNull
		Me.nCredit = eRemoteDB.Constants.intNull
		Me.nDebit = eRemoteDB.Constants.intNull
		Me.sDescript = String.Empty
		Me.sManualMov = String.Empty
		Me.nPaynumbe = eRemoteDB.Constants.intNull
		Me.nPolicy = eRemoteDB.Constants.intNull
		Me.nReceipt = eRemoteDB.Constants.intNull
		Me.sStatregt = String.Empty
		Me.nTransac = eRemoteDB.Constants.intNull
		Me.nTransactio = eRemoteDB.Constants.intNull
		Me.nType_move = eRemoteDB.Constants.intNull
		Me.nType_pay = eRemoteDB.Constants.intNull
		Me.nType_tran = eRemoteDB.Constants.intNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nProvince = eRemoteDB.Constants.intNull
		Me.nIdDocument = eRemoteDB.Constants.intNull
		Me.nRequest_nu = eRemoteDB.Constants.intNull
		Me.nBordereaux = eRemoteDB.Constants.intNull
		Me.sProcess = String.Empty
		Me.sNumForm = String.Empty
		Me.nOrigCurr = eRemoteDB.Constants.intNull
		Me.nExchange = eRemoteDB.Constants.intNull
		Me.sAutoriza = String.Empty
		Me.dValueDate = dtmNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.sNull_recor = String.Empty
		Me.nCashNum = eRemoteDB.Constants.intNull
		Me.sProcess_ind = String.Empty
		
		Me.nIdreturn = eRemoteDB.Constants.intNull
		Me.nProcess = eRemoteDB.Constants.intNull
		Me.nSta_cheque = eRemoteDB.Constants.intNull
		Me.sProductDes = String.Empty
		
		Me.sShort_des = String.Empty
		Me.nBalance = eRemoteDB.Constants.intNull
		Me.nCreditot = eRemoteDB.Constants.intNull
		Me.nDebitot = eRemoteDB.Constants.intNull
		
		Me.sAcc_number = String.Empty
		Me.sBank_des = String.Empty
		
		Me.dEffecdate = dtmNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find_Move_Acc_CA001: rescata el monto asociado a una Propuesta/cliente
	Public Function Find_Move_Acc_CA001(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
		Dim lrecreaMove_Acc_ca001 As eRemoteDB.Execute
		
		On Error GoTo reaMove_Acc_ca001_Err
		lrecreaMove_Acc_ca001 = New eRemoteDB.Execute
		With lrecreaMove_Acc_ca001
			.StoredProcedure = "reaMove_Acc_ca001"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Me.nAmount = .Parameters("nAmount").Value
				Find_Move_Acc_CA001 = True
			End If
		End With
		
reaMove_Acc_ca001_Err: 
		If Err.Number Then
			Find_Move_Acc_CA001 = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Acc_ca001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_ca001 = Nothing
		On Error GoTo 0
	End Function
	'% insRegMove_Acc: regulariza un pago de primera prima, al momento de
	'%                 regularizar una propuesta
	Public Function insRegMove_Acc(ByVal nProponum As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nprop_reg As Integer) As Boolean
		Dim lrecinsRegMove_Acc As eRemoteDB.Execute
		
		On Error GoTo insRegMove_Acc_Err
		
		lrecinsRegMove_Acc = New eRemoteDB.Execute
		
		insRegMove_Acc = False
		
		With lrecinsRegMove_Acc
			.StoredProcedure = "insRegMove_Acc"
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProp_reg", nprop_reg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insRegMove_Acc = True
			End If
		End With
		
insRegMove_Acc_Err: 
		If Err.Number Then
			insRegMove_Acc = False
		End If
		'UPGRADE_NOTE: Object lrecinsRegMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsRegMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_nProponum_Dev: Obtiene la información si la propuesta ya tiene movimientos de devolucion
	Public Function Find_nProponum_type(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double, ByVal nType_move As Short) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		Dim lintExists As Short
		
		lrecMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo Find_nProponum_o_Err
		
		With lrecMove_Acc
			.StoredProcedure = "reaMove_AccPropoNum_type"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPropoNum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_nProponum_type = (.Parameters("nExists").Value = 1)
			
		End With
		
Find_nProponum_o_Err: 
		If Err.Number Then
			Find_nProponum_type = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_nProponum_Amount: Obtiene la información del monto y la moneda de la propuesta
	Public Function Find_nProponum_Amount(ByVal nProponum As Double) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		Dim lintExists As Short
		
		lrecMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo Find_nProponum_o_Err
		
		With lrecMove_Acc
			.StoredProcedure = "ReaMove_Accproponum_amoun"
			.Parameters.Add("nPropoNum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_pay", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_nProponum_Amount = .Run(False)
			nAmount = .Parameters("nAmount").Value
			nCurrency = .Parameters("nCurrency").Value
			nBalance = .Parameters("nAmount_pay").Value
		End With
		
Find_nProponum_o_Err: 
		If Err.Number Then
			Find_nProponum_Amount = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'% UpdMove_Acc_rev: Realiza el reverso de Move_Acc para los rechazos
	Public Function UpdMove_Acc_rev(ByVal nProponum As Double, ByVal nUsercode As Double, ByVal nDevolution As Double) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		lrecMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo UpdMove_Acc_rev_Err
		
		With lrecMove_Acc
			.StoredProcedure = "UpdMove_Acc_rev"
			.Parameters.Add("nPropoNum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_sCertype", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDevolution", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdMove_Acc_rev = .Run(False)
		End With
		
UpdMove_Acc_rev_Err: 
		If Err.Number Then
			UpdMove_Acc_rev = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find_sclient:
	'% lee si existe movimiento para de ingreso de primera prima
	Public Function Find_sClient(ByVal sClient As String) As Boolean
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		lrecMove_Acc = New eRemoteDB.Execute
		On Error GoTo Find_sClient_Err
		Find_sClient = False
		
		With lrecMove_Acc
			.StoredProcedure = "reaMove_AccPropoNum"
			.Parameters.Add("nPropoNum", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dOperdate = .FieldToClass("dOperdate")
				Find_sClient = True
				.RCloseRec()
			End If
		End With
		
Find_sClient_Err: 
		If Err.Number Then
			Find_sClient = False
		End If
		'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Acc = Nothing
		On Error GoTo 0
	End Function
End Class






