Option Strict Off
Option Explicit On
Public Class Budget_amo
	'%-------------------------------------------------------%'
	'% $Workfile:: Budget_amo.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:36p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 22/06/2001
	'+ Los campos llave de la tabla corresponden a: nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth.
	
	'   Column_name                    Type       Computed Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public nLed_compan As Integer 'smallint   no         2           5     0     no           (n/a)                (n/a)
	Public nCurrency As Integer 'smallint   no         2           5     0     no           (n/a)                (n/a)
	Public sBud_code As String 'char       no        12                       no           no                   no
	Public sAccount As String 'char       no        20                       no           no                   no
	Public sAux_accoun As String 'char       no        20                       no           no                   no
	Public sCost_cente As String 'char       no         8                       no           no                   no
	Public nYear As Integer 'smallint   no         2           5     0     no           (n/a)                (n/a)
	Public nMonth As Integer 'smallint   no         2           5     0     no           (n/a)                (n/a)
	Public nBalance As Double 'decimal    no         9           12    2     yes          (n/a)                (n/a)
	Public nUsercode As Integer 'smallint   no         2           5     0     yes          (n/a)                (n/a)
	
	'- Se definen las propiedades para la consulta presupuestaria
	
	Public sDescript As String
	Public nDebit As Double
	Public nCredit As Double
	Public nStatusInstance As String
	
	Private Const CN_SELECTED As String = "1"
	Private Const CN_NSELECTED As String = "2"
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Dim lreccreBudget_amo As eRemoteDB.Execute
		
		lreccreBudget_amo = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		If Me.sCost_cente = "" Then
			Me.sCost_cente = "        "
		End If
		'    If Me.sAux_accoun = "" Then
		'        Me.sAux_accoun = "0"
		'    End If
		
		'+ Definición de parámetros para stored procedure 'insudb.creBudget_amo'
		'+ Información leída el 10/07/2001 10:17:36
		
		With lreccreBudget_amo
			.StoredProcedure = "creBudget_amo"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreBudget_amo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreBudget_amo = Nothing
Add_err: 
		If Err.Number Then
			Add = False
		End If
	End Function
	
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Dim lrecupdBudget_amo As eRemoteDB.Execute
		
		lrecupdBudget_amo = New eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		'+ Definición de parámetros para stored procedure 'insudb.updBudget_amo'
		'+ Información leída el 10/07/2001 10:42:15
		
		With lrecupdBudget_amo
			.StoredProcedure = "updBudget_amo"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdBudget_amo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBudget_amo = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	'% updBudget_amo_nBalance: Permite eliminar la cantidad presupuestada para un mes especifico
	Public Function updBudget_amo_nBalance() As Boolean
		Dim lrecupdBudget_amo_nBalance As eRemoteDB.Execute
		
		lrecupdBudget_amo_nBalance = New eRemoteDB.Execute
		
		On Error GoTo updBudget_amo_nBalance_err
		
		'+ Definición de parámetros para stored procedure 'insudb.updBudget_amo_nBalance'
		'+ Información leída el 10/07/2001 14:42:31
		
		With lrecupdBudget_amo_nBalance
			.StoredProcedure = "updBudget_amo_nBalance"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updBudget_amo_nBalance = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdBudget_amo_nBalance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBudget_amo_nBalance = Nothing
		
updBudget_amo_nBalance_err: 
		If Err.Number Then
			updBudget_amo_nBalance = False
		End If
		
	End Function
	
	
	
	'% Find_Account: Verifica que se encuentre registrada una cuenta para un determinado
	'%               presupuesto
	'UPGRADE_NOTE: Year was upgraded to Year_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function Find_Account(ByVal Led_Compan As Integer, ByVal Bud_code As String, ByVal Year_Renamed As Integer, ByVal Account As String) As Boolean
		Dim lrecreaBudget_amo_Account As eRemoteDB.Execute
		
		lrecreaBudget_amo_Account = New eRemoteDB.Execute
		
		On Error GoTo Find_Account_Err
		'+ Definición de parámetros para stored procedure 'insudb.reaBudget_amo_Account'
		'+ Información leída el 09/07/2001 11:30:13
		
		With lrecreaBudget_amo_Account
			.StoredProcedure = "reaBudget_amo_Account"
			.Parameters.Add("nLed_compan", Led_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", Bud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", Year_Renamed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", Account, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Account = True
			Else
				Find_Account = False
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecreaBudget_amo_Account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBudget_amo_Account = Nothing
		
Find_Account_Err: 
		If Err.Number Then
			Find_Account = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'% Find_Account: Verifica que se encuentre registrada una cuenta para un determinado
	'%               presupuesto
	'UPGRADE_NOTE: Year was upgraded to Year_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function Find_AuxAccount(ByVal Led_Compan As Integer, ByVal Bud_code As String, ByVal Year_Renamed As Integer, ByVal Account As String, ByVal Aux_accoun As String) As Boolean
		Dim lrecreaBudget_amo_AuxAccount As eRemoteDB.Execute
		
		lrecreaBudget_amo_AuxAccount = New eRemoteDB.Execute
		
		On Error GoTo Find_AuxAccount_Err
		'+ Definición de parámetros para stored procedure 'insudb.reaBudget_amo_AuxAccount'
		'+ Información leída el 09/07/2001 11:39:23
		
		With lrecreaBudget_amo_AuxAccount
			.StoredProcedure = "reaBudget_amo_AuxAccount"
			.Parameters.Add("nLed_compan", Led_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", Bud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", Year_Renamed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_AuxAccount = True
			Else
				Find_AuxAccount = False
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecreaBudget_amo_AuxAccount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBudget_amo_AuxAccount = Nothing
		
Find_AuxAccount_Err: 
		If Err.Number Then
			Find_AuxAccount = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%insCalc_QuantityBudget: Se calcula el presupuesto mensual dado el total anual
	Public Function insCalc_QuantityBudget(ByVal nLedCompan As Integer, ByVal sBud_code As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal chkTotAnnual As String, ByVal tcnAnnualBudget As Double) As Object
		Dim mclsBudgetWork As eBudget.Budget
		Dim lstrReturnValue As Object
		Dim lintInitMonth As Integer
		Dim lintEndMonth As Integer
		Dim lintValue As Integer
		Dim lintTop As Integer
		
		mclsBudgetWork = New eBudget.Budget
		
		If mclsBudgetWork.Find(nLedCompan, sBud_code, nYear, nCurrency) Then
		End If
		
		lintInitMonth = CShort(Mid(CStr(mclsBudgetWork.nInit_month), 5, 2)) '+ Mes inicial
		lintEndMonth = CShort(Mid(CStr(mclsBudgetWork.nEnd_month), 5, 2)) '+ Mes final
		
		lintValue = lintInitMonth
		
		'+ Se calcula el número de registros que contendrá el arreglo
		lintTop = lintEndMonth - lintInitMonth + 1
		
		If chkTotAnnual = "1" Then
			lstrReturnValue = tcnAnnualBudget / lintTop
			'        chkTotAnnual.Value = Unchecked
			'        Call chkTotAnnual_Click
		Else
			lstrReturnValue = 0
		End If
		
		
		insCalc_QuantityBudget = lstrReturnValue
	End Function
	
	'%insCalc_Diference: Se calcula el presupuesto mensual dado el total anual
	Public Function insCalc_Diference(ByVal nAmount As Double, ByVal nQuantity As Double) As Object
		Dim lstrReturnValue As Object
		
		lstrReturnValue = nAmount - nQuantity
		
		If lstrReturnValue < 0 Then
			lstrReturnValue = lstrReturnValue * -1
		End If
		If lstrReturnValue = 0 Or nQuantity = 0 Then
			lstrReturnValue = 0
		Else
			lstrReturnValue = lstrReturnValue * 100 / nQuantity
		End If
		
		insCalc_Diference = lstrReturnValue
	End Function
	
	
	'%insValCP008_k: Rutina de validación del encabezado de la ventana.
	Public Function insValCP008_k(ByVal nLedCompan As Integer, ByVal Action As Integer, ByVal sCodispl As String, ByVal tcnYearWork As Integer, ByVal cbeCurrencyWork As Integer, ByVal valBudgetWork As String, ByVal tcnYearComp As Integer, ByVal valBudgetComp As String, ByVal cbeCurrencyComp As Integer, ByVal valAcount As String, ByVal valAuxAcount As String, ByVal valCost_cente As String, ByVal chkTotAnnual As String, ByVal tcnAnnualBudget As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsBudgetWork As eBudget.Budget
		Dim mclsLedCompan As eLedge.Led_compan
		Dim mclsLedgerAcc As eLedge.LedgerAcc
		Dim mclsBudgetAmo As eBudget.Budget_amo
		
		Dim mblnValueComp As Boolean
		Dim mblnAuxExist As Boolean
		
		On Error GoTo insValCP008_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsBudgetWork = New eBudget.Budget
		mclsLedCompan = New eLedge.Led_compan
		mclsLedgerAcc = New eLedge.LedgerAcc
		mclsBudgetAmo = New eBudget.Budget_amo
		
		If mclsLedCompan.Find(nLedCompan) Then
		End If
		
		'+ Validación del campo Tabajar con - Ejercicio
		'+ Debe estar lleno
		If (tcnYearWork = eRemoteDB.Constants.intNull Or tcnYearWork = 0) Then 'And (valBudgetWork = NumNull Or valBudgetWork = 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 36036)
		End If
		If tcnYearWork <> eRemoteDB.Constants.intNull Then
			'+ Debe ser un año válido
			If tcnYearWork < 1900 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1183)
			End If
		End If
		
		'+ Validación del campo Tabajar con - Moneda
		If cbeCurrencyWork = eRemoteDB.Constants.intNull Or cbeCurrencyWork = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		End If
		
		'+ Validación del campo Tabajar con - Presupuesto
		'+ Debe estar lleno
		If valBudgetWork = "" Then
			Call lclsErrors.ErrorMessage(sCodispl, 36062)
		Else
			'+ La combinación Ejercicio - Presupuesto - Moneda debe estar registrada en el archivo de definición de Presupuestos
			If Not mclsBudgetWork.Find(mclsLedCompan.nLed_compan, valBudgetWork, tcnYearWork, cbeCurrencyWork) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36065)
			End If
		End If
		
		
		'+ Validación de los campos Comparar con - Ejercicio, Comparar con - Presupuesto
		'+ Sólo en caso de Consulta
		If Action = eFunctions.Menues.TypeActions.clngActionQuery Then
			'+ mblnValueComp = true si todos los campos tienen valor
			mblnValueComp = True
			'+ Si alguno de los campos tiene valor, el otro debe tener valor también
			If tcnYearComp = eRemoteDB.Constants.intNull Or tcnYearComp = 0 Then
				mblnValueComp = False
				If (Not (valBudgetComp = "") Or Not (cbeCurrencyComp = eRemoteDB.Constants.intNull Or cbeCurrencyComp = 0)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36071)
				End If
			Else
				'+ Debe ser un año válido
				If tcnYearComp < 1900 Then
					Call lclsErrors.ErrorMessage(sCodispl, 1183)
				End If
				
				If cbeCurrencyComp = eRemoteDB.Constants.intNull Or cbeCurrencyComp = 0 Then
					'+ Si alguno de los campos tiene valor, el otro debe tener valor también
					mblnValueComp = False
					'If lblnAll Then
					Call lclsErrors.ErrorMessage(sCodispl, 36071)
					'End If
				Else
					If valBudgetComp = "" Then
						'mblnValueComp = False
						'If lblnAll Then
						Call lclsErrors.ErrorMessage(sCodispl, 36071)
						'End If
					Else
						'+ El presupuesto debe estar expresado en la moneda en que se lleve la Contabilidad
						If mclsBudgetWork.Find(mclsLedCompan.nLed_compan, valBudgetComp, tcnYearComp, cbeCurrencyComp, True) Then
							If mclsBudgetWork.nCurrency <> mclsLedCompan.nCurrency Then
								Call lclsErrors.ErrorMessage(sCodispl, 36229)
							End If
						End If
					End If
				End If
			End If
			
			If mblnValueComp Then
				'+ La combinación Ejercicio - Moneda - Presupuesto debe estar registrada
				If Not mclsBudgetWork.Find(mclsLedCompan.nLed_compan, valBudgetComp, tcnYearComp, cbeCurrencyComp) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36072)
				End If
			End If
		End If
		
		
		'+ Validación del campo Cuenta
		'+ Debe estar lleno
		If valAcount = "" Then
			
			'+ Si no posee auxiliar, y está vacío, se limpia la descripción de la cuenta
			If valAuxAcount = "" Then
				'tctDescript.Value = Null
				'tctDescript.Locked = False
				'valAuxAcount.Locked = True
			End If
			
			'If lblnAll Then
			Call lclsErrors.ErrorMessage(sCodispl, 36017)
			'End If
		Else
			
			If valAcount <> "" Then
				
				If mclsLedgerAcc.ValAnotherAux(mclsLedCompan.nLed_compan, valAcount) Then
					
					'+ Posee por lo menos una cuenta auxiliar
					mblnAuxExist = True
				Else
					
					'+ No posee ninguna cuenta auxiliar
					mblnAuxExist = False
					
					''+ Se pasan los parámetros al campo Unidad
					'If Not lblnAll Then
					'    If mclsLedgerAcc.sOrgan_unit = CN_SELECTED Then
					'        valCost_cente.Parameters.Add "nLed_compan", mclsLedCompan.nLed_compan, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
					'        valCost_cente.Locked = False
					'    Else
					'        valCost_cente.Value = Null
					'        valCost_cente.Locked = True
					'    End If
					'End If
					
					'                        tctDescript.Value = valAcount.Descript
					'                        tctDescript.Locked = True
					
					Call mclsLedgerAcc.Find_Account(mclsLedCompan.nLed_compan, valAcount)
				End If
				
				'+ A nivel de las cuentas deben permitir asignar con presupuestos
				If mclsLedgerAcc.sBudget_ind <> CN_SELECTED Then
					Call lclsErrors.ErrorMessage(sCodispl, 36206)
				End If
				
				'+ Si la acción no es Consultar, ninguna cuenta de nivel superior que contenga a esta cuenta
				'+ debe tener definido presupuesto
				
				If Action <> eFunctions.Menues.TypeActions.clngActionQuery Then
					If insvalAccount(mclsLedCompan.nLed_compan, valBudgetComp, tcnYearComp, cbeCurrencyComp, valAcount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36205)
					End If
				End If
				
				'+ Si la acción es Registrar
				If Action = eFunctions.Menues.TypeActions.clngActionadd Then
					
					'+ La combinación de Trabajar con - Ejercicio, Trabajar con - Presupuesto y Cuenta no deben
					'+ existir en el archivo de resultados presupuestarios, para la compañía con la que se esté
					'+ trabajando
					
					If Not valBudgetWork = "" And Not valAcount = "" Then
						If mclsBudgetAmo.Find_Account(mclsLedCompan.nLed_compan, valBudgetWork, tcnYearWork, valAcount) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36070)
						End If
					End If
				Else
					
					'+ La combinación de Trabajar con - Ejercicio, Trabajar con - Presupuesto y Cuenta debe
					'+ existir en el archivo de resultados presupuestarios, para la compañía con la que se esté
					'+ trabajando
					If Not mclsBudgetAmo.Find_Account(mclsLedCompan.nLed_compan, valBudgetWork, tcnYearWork, valAcount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36069)
					End If
				End If
			Else
				
				'+ Debe estar registrado en el plan de cuentas para la compañía con la que se está trabajando
				Call lclsErrors.ErrorMessage(sCodispl, 36010)
			End If
			
		End If
		
		
		'+ Validación del campo Auxiliar
		If valAuxAcount = "" Then
			
			'+ Si posee auxiliar, y está vacío, se limpia la descripción de la cuenta
			'If mblnAuxExist Then
			'    tctDescript.Value = Null
			'    tctDescript.Locked = False
			'    valAuxAcount.Locked = False
			'End If
		Else
			
			'+ La combinación Cuenta - Auxiliar debe estar registrado en el plan de cuentas para la
			'+ compañía con la que se está trabajando
			If valAuxAcount <> "" Then
				Call mclsLedgerAcc.Find(mclsLedCompan.nLed_compan, valAcount, valAuxAcount)
				
				'tctDescript.Value = valAuxAcount.Descript
				'tctDescript.Locked = True
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 36021)
			End If
			
			'+ Si la acción es Registrar
			If Action = eFunctions.Menues.TypeActions.clngActionadd Then
				
				'+ La combinación del Ejercicio - Trabajar con, Cuenta y Auxiliar
				'+ no deben existir en el archivo de resultados presupuestarios, para la compañía con
				'+ la que se esté trabajando
				
				If mclsBudgetAmo.Find_AuxAccount(mclsLedCompan.nLed_compan, valBudgetWork, tcnYearWork, valAcount, valAuxAcount) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36070)
				End If
				
				'+ A nivel de las cuentas deben permitir asignar con presupuestos
				If mclsLedgerAcc.sBudget_ind <> "CN_SELECTED" Then
					Call lclsErrors.ErrorMessage(sCodispl, 36206)
				End If
				
			Else
				
				'+ La combinación del Ejercicio - Trabajar con, Cuenta y Auxiliar
				'+ deben existir en el archivo de resultados presupuestarios, para la compañía con
				'+ la que se esté trabajando
				
				If mclsBudgetAmo.Find_AuxAccount(mclsLedCompan.nLed_compan, valBudgetWork, tcnYearWork, valAcount, valAuxAcount) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36069)
				End If
			End If
			
		End If
		
		'+ Validación del campo Unidad
		
		If valCost_cente = "" Then
			'+ Si la acción es Registrar, el indicador de requerimiento de "Unidad Organizativa", del
			'+ archivo de cuentas contables, debe estar seleccionado
			If Action = eFunctions.Menues.TypeActions.clngActionadd And mclsLedgerAcc.sOrgan_unit = "CN_SELECTED" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36051)
			End If
		Else
			If valCost_cente <> "" Then
				'+ El indicador de requerimiento de "Unidad Organizativa", del archivo de cuentas
				'+ contables, no debe estar seleccionado
				If mclsLedgerAcc.sOrgan_unit = "CN_NSELECTED" Then
					Call lclsErrors.ErrorMessage(sCodispl, 36052)
				End If
			Else
				'+ Debe estar registrado en el archivo de unidades organizativas
				Call lclsErrors.ErrorMessage(sCodispl, 36050)
			End If
		End If
		
		
		
		'+ Validación del campo Presupuesto anual
		
		If chkTotAnnual = "1" And tcnAnnualBudget = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36207)
		End If
		
		'        If Action = clngActionQuery Then
		'            If mblnValueComp Then
		'                If Not insreaBudget_amo_comp Then
		'                    If .ObjErrors.ErrorMessage("CP008", 1073) Then
		'                        insValHeader = False
		'                    End If
		'                End If
		'            Else
		'                If Not insreaBudget_AmoModify Then
		'                    If .ObjErrors.ErrorMessage("CP008", 1073) Then
		'                        insValHeader = False
		'                    End If
		'                End If
		'            End If
		'        End If
		
		
		insValCP008_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object mclsBudgetWork may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsBudgetWork = Nothing
		'UPGRADE_NOTE: Object mclsLedCompan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedCompan = Nothing
		'UPGRADE_NOTE: Object mclsLedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedgerAcc = Nothing
		'UPGRADE_NOTE: Object mclsBudgetAmo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsBudgetAmo = Nothing
		
insValCP008_k_Err: 
		If Err.Number Then
			insValCP008_k = insValCP008_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insvalAccount: Verifica que ninguna cuenta de nivel superior que contenga la cuenta
	'%                tenga definido presupuesto
	Private Function insvalAccount(ByVal nLedCompan As Integer, ByVal sBudCode As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal lstrAccount As String) As Boolean
		'- Se define la variable que contiene la longitud de la cuenta
		Dim lintLen As Integer
		
		'- Se define la variable que contiene la longitud de la cuenta
		Dim lintPos As Integer
		
		Dim mclsBudgetAmo As eBudget.Budget_amo
		Dim mclsLedCompan As eLedge.Led_compan
		Dim mclsBudgetWork As eBudget.Budget
		mclsBudgetAmo = New eBudget.Budget_amo
		mclsLedCompan = New eLedge.Led_compan
		mclsBudgetWork = New eBudget.Budget
		
		If mclsLedCompan.Find(nLedCompan) Then
		End If
		If mclsBudgetWork.Find(nLedCompan, sBudCode, nYear, nCurrency) Then
		End If
		
		lintLen = Len(lstrAccount)
		lstrAccount = Trim(lstrAccount)
		
		For lintPos = lintLen To 1 Step -1
			If Mid(lstrAccount, lintPos, 1) <> "-" Then
				Mid(lstrAccount, lintPos, 1) = " "
			Else
				Mid(lstrAccount, lintPos, 1) = " "
				
				Exit For
			End If
		Next lintPos
		lstrAccount = Trim(lstrAccount)
		
		If lstrAccount <> String.Empty Then
			insvalAccount = mclsBudgetAmo.Find_Account(mclsLedCompan.nLed_compan, mclsBudgetWork.sBud_code, mclsBudgetWork.nYear, lstrAccount)
		End If
		
	End Function
	'%insPostCP008: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCP008(ByVal plngAction As Integer, ByVal sAction As String, ByVal optSumm As String, ByVal nLed_compan As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nBalance As Double, ByVal nUsercode As Integer, ByVal sDescript As String, Optional ByVal sCost_cente As String = "") As Boolean
		Dim mcolBudgetWork As eBudget.Budget_amos
		Dim lclsBudgetWork As eBudget.Budget_amo
		mcolBudgetWork = New eBudget.Budget_amos
		lclsBudgetWork = New eBudget.Budget_amo
		
		insPostCP008 = True
		On Error GoTo insPostCP008_err
		
		With Me
			.nBalance = nBalance
			.nCurrency = nCurrency
			.nLed_compan = nLed_compan
			.nMonth = nMonth
			.nStatusInstance = sAction
			.nUsercode = nUsercode
			.nYear = nYear
			.sAccount = sAccount
			.sAux_accoun = sAux_accoun
			.sBud_code = sBud_code
			.sCost_cente = sCost_cente
			.sDescript = sDescript
		End With
		'- Se define la variable que contendrá la Cantidad Presupuestaria
		Dim ldblAmount As Double
		
		Select Case plngAction
			
			'+Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				'+ Si la opción Saldo - Acumulado está seleccionada
				If optSumm = "1" Then
					ldblAmount = ldblAmount + nBalance
					lclsBudgetWork.nBalance = ldblAmount
				End If
				'If mcolBudgetWork.Add(clngActionadd, nLed_compan, nCurrency, sBud_code, sAccount, sAux_accoun, nYear, nMonth, nBalance, sCost_cente) Then
				'    insPostCP008 = True
				'Else
				'    insPostCP008 = False
				'End If
				
				'        Case clngActionUpdate
				
		End Select
		
		If Me.UpdateBudget_amo(sAction) Then
			insPostCP008 = True
		Else
			insPostCP008 = False
		End If
		
insPostCP008_err: 
		If Err.Number Then
			insPostCP008 = False
		End If
	End Function
	
	
	
	'% Update: realiza el tratamiento de cada instancia de la clase en la colección
	Public Function UpdateBudget_amo(ByVal nStatusInstance As String) As Boolean
		UpdateBudget_amo = True
		
		'For Each lclsBudgetAmo In mCol
		With Me
			
			Select Case nStatusInstance
				
				'+ Si la acción es Agregar
				Case "Add"
					UpdateBudget_amo = .Add()
					.nStatusInstance = CStr(1)
					
					'+ Si la acción es Actualizar
				Case "Update"
					If Find_Account(Me.nLed_compan, Me.sBud_code, Me.nYear, Me.sAccount) = False Then
						UpdateBudget_amo = .Add()
					Else
						UpdateBudget_amo = .Update()
					End If
					
					'+ Si la acción es Eliminar
				Case "Delete"
					UpdateBudget_amo = .updBudget_amo_nBalance()
					
			End Select
		End With
		'Next lclsBudgetAmo
		
	End Function
End Class






