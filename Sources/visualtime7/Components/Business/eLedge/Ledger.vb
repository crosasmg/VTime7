Option Strict Off
Option Explicit On
Public Class Ledger
	'%-------------------------------------------------------%'
	'% $Workfile:: Ledger.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                       Type        Computed Length  Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'-------------------------------- -------------------- -------- ---- ------ ------- ------------------- ---------------------------
	Public dStart_date As Date 'datetime    no      8                    no      (n/a)               (n/a)
	Public dEnd_date As Date 'datetime    no      8                    yes     (n/a)               (n/a)
	
	Private Const CN_EMPTYAUX As String = "                    "
	
	'**% Find: returns the "Accounting Period" period of the company
	'% Find: Devuelve el periodo contable de la compañía
	Public Function Find(Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Defines the variable lrecreaLedger
		'- Se define la variable lrecreaLedger
		
		Dim lrecreaLedger As eRemoteDB.Execute
		lrecreaLedger = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(dStart_date) Or IsNothing(dEnd_date) Or lblnFind Then
			
			'**+Parameters definition for the stored procedure 'insudb.reaLedger'
			'**+Data read on 11/22/2000 18:06:05
			'+ Definición de parámetros para stored procedure 'insudb.reaLedger'
			'+ Información leída el 22/11/2000 18:06:05
			
			With lrecreaLedger
				.StoredProcedure = "reaLedger"
				If .Run Then
					dStart_date = .FieldToClass("dStart_date")
					dEnd_date = .FieldToClass("dEnd_date")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaLedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	'**insPreCP002: obtain the necesary info for the window handle
	'insPreCP002:Permite obtener la información de necesaria para el manejo de la ventana
	Public Function insPreCP002(ByVal nAction As Integer, ByVal nLed_compan As Integer, ByVal sAccoun As String) As Boolean
		
		Dim lclsLedgerAcc As eLedge.LedgerAcc
		
		lclsLedgerAcc = New eLedge.LedgerAcc
		
		On Error GoTo insPreCP002_Err
		
		With lclsLedgerAcc
			Select Case nAction
				Case eFunctions.Menues.TypeActions.clngActionadd
					If InStr(sAccoun, "-") <> 0 Then
						
						'**+The previous levels are charge in the query grid.
						'+Se cargan los niveles previos para mostrarlos en el grid de consulta.
						
						Call .FullChargePrevLevel(nLed_compan, sAccoun)
						
					End If
			End Select
		End With
		
		
		'UPGRADE_NOTE: Object lclsLedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedgerAcc = Nothing
		
insPreCP002_Err: 
		If Err.Number Then
			insPreCP002 = False
		End If
		On Error GoTo 0
	End Function
    '**%insValAccountNextBudget: verify if exists another auxiliar for the same account.
    '%insValAccountNextBudget: Esta rútina permite verificar si existen otro auxiliar para la misma cuenta.
    Private Function insValAccountNextBudget(ByVal lstrAccount As String, ByVal nLed_compan As Integer) As Boolean
        Dim lclsLedgerAcc As eLedge.LedgerAcc

        lclsLedgerAcc = New eLedge.LedgerAcc

        insValAccountNextBudget = True

        If Not lclsLedgerAcc.ValBudgetDef(nLed_compan, Trim(lstrAccount) & "-") Then
            insValAccountNextBudget = False
        End If

    End Function
    '**%insValMovementAccount: validate if an account had any transactions.
    '%insValMovementAccount: Esta rútina valida si una cuenta ha tenido movimientos.
    Private Function insValMovementAccount(ByVal nLed_compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim lrecreaMovementAccount As eRemoteDB.Execute
		
		lrecreaMovementAccount = New eRemoteDB.Execute
		On Error GoTo insValMovementAccount_err
		'**+parameters definition for the stored procedure 'insudb.reaMovementAccount'
		'**+Data read on 06/06/2001 01:54:11 PM
		'+Definición de parámetros para stored procedure 'insudb.reaMovementAccount'
		'+Información leída el 06/06/2001 01:54:11 PM
		
		With lrecreaMovementAccount
			.StoredProcedure = "reaMovementAccount"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", Trim(lstrAccount), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run()
		End With
		If lrecreaMovementAccount.FieldToClass("nCount") = 0 Then
			insValMovementAccount = False
		Else
			insValMovementAccount = True
		End If
		'UPGRADE_NOTE: Object lrecreaMovementAccount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMovementAccount = Nothing
insValMovementAccount_err: 
		If Err.Number Then
			insValMovementAccount = False
		End If
		On Error GoTo 0
	End Function
	
	'%DefaultValuesCP002:Esta función se encarga de realizar la habilitación o des-habilitación de los
	'%campos de la ventana CP002.
	Public Function DefaultValuesCP002(ByVal nAction As Integer, ByVal sField As String, ByVal nLed_compan As Integer, ByVal sAux_account As String, ByVal sAccoun As String) As Object
        Dim lstrReturnValue As Object = New Object
        Dim lstrTypeLevelPrevious As String
		Dim lclsLedgerAcc As eLedge.LedgerAcc
		
		lclsLedgerAcc = New eLedge.LedgerAcc
		
		If lclsLedgerAcc.Find(nLed_compan, sAccoun, sAux_account) Then
			Select Case sField
				Case "tctDescript"
					lstrReturnValue = lclsLedgerAcc.sDescript
					
				Case "cbeType"
					lstrReturnValue = lclsLedgerAcc.sType_acc
					
				Case "lblTDebit"
					lstrReturnValue = Trim(CStr(lclsLedgerAcc.nTotal_deb))
					
				Case "lblTCredit"
					lstrReturnValue = Trim(CStr(lclsLedgerAcc.nTotal_cre))
					
				Case "chkDebit"
					If lclsLedgerAcc.sBlock_deb = "1" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "2"
					End If
					
				Case "chkCredit"
					If lclsLedgerAcc.sBlock_cre = "1" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "2"
					End If
					
				Case "lblTBalance"
					lstrReturnValue = Trim(CStr(lclsLedgerAcc.nBalance))
					
				Case "chkOrgUnit"
					If lclsLedgerAcc.sOrgan_unit = "1" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "2"
					End If
					
				Case "chkAdjust"
					If lclsLedgerAcc.sAdju_exci = "1" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "2"
					End If
					
				Case "chkBudget"
					If lclsLedgerAcc.sBudget_ind = "1" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "0"
					End If
					
				Case "cbeAux"
					lstrReturnValue = Trim(CStr(lclsLedgerAcc.nAux_create))
					
				Case "ldblTotalCre"
					lstrReturnValue = Trim(CStr(lclsLedgerAcc.nTotal_cre))
			End Select
		End If
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			lstrTypeLevelPrevious = lclsLedgerAcc.ValAccountPreviousType(nLed_compan, sAccoun)
			
			Select Case sField
				Case "cbeType"
					If lstrTypeLevelPrevious <> " " Then
						lstrReturnValue = lstrTypeLevelPrevious
						If lstrReturnValue = "3" Or lstrReturnValue = "4" Then '+ Ingresos o gastos
						Else
							lstrReturnValue = "0"
						End If
					Else
						lstrReturnValue = "0"
					End If
					
				Case "chkBudget_disabled"
					
					'+Si la cuenta es de Gastos o ingresos se verifica que la cuenta no tenga niveles superiores que admiten presupuestos.
					If lstrTypeLevelPrevious <> " " Then
						lstrReturnValue = lstrTypeLevelPrevious
						
						'+ Ingresos o gastos
						If lstrReturnValue = "3" Or lstrReturnValue = "4" Then
							If lclsLedgerAcc.ValAccountPreviousBudget(nLed_compan, sAccoun) Then
								lstrReturnValue = True
							Else
								lstrReturnValue = False
							End If
						Else
							lstrReturnValue = True
						End If
					Else
						lstrReturnValue = False
					End If
					
					'+Si se esta registrando una cuenta con auxiliar el tipo de auxiliar solo puede ser manual
				Case "cbeAux"
					If InStr(sAccoun, "-") <> 0 Then
						If sAux_account <> String.Empty Then
							lstrReturnValue = "2" '+ Manual
						End If
					Else
						lstrReturnValue = "1" '+ No tiene
					End If
					
				Case "cbeAux_disabled"
					If InStr(sAccoun, "-") <> 0 Then
						lstrReturnValue = False
					Else
						
						'**+If the account is the first level disabled the auxiliary control
						'+Si la cuenta es de primer nivel se desabitita el control de auxiliares automàticos
						
						lstrReturnValue = True
					End If
			End Select
			
			'**+If the selected option is Modify
			'+Si la opción seleccionada es Modificar
			
		ElseIf nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then 
			lstrTypeLevelPrevious = lclsLedgerAcc.ValAccountPreviousType(nLed_compan, sAccoun)
			
			Select Case sField
				Case "cbeType_disabled"
					lstrReturnValue = lstrTypeLevelPrevious
					
					'**+If the account type is expenses or incomes the control is activated
					'+Si el tipo de cuenta es de gastos o ingresos se activa el control
					
					If Trim(lstrReturnValue) <> "" Then
						If lstrReturnValue = "3" Or lstrReturnValue = "4" Then '+ Gastos o ingresos '**+Expences or incomes
							
							'**+If the account has higher or lower levels that permit budgets, the control is not habilitated.
							'+Si la cuenta tiene niveles superiores o inferiores que permiten presupuestos no se habilita el control.
							
							If lclsLedgerAcc.ValAccountPreviousBudget(nLed_compan, sAccoun) Or insValAccountNextBudget(sAccoun, nLed_compan) Or lclsLedgerAcc.ValAccountAuxBudget(nLed_compan, sAccoun) Then
								lstrReturnValue = False
							Else
								lstrReturnValue = True
							End If
						Else
							lstrReturnValue = True
						End If
					Else
						lstrReturnValue = False
					End If
					
				Case "cbeAux_disabled"
					If InStr(sAccoun, "-") = 0 Then
						lstrReturnValue = True
					Else
						
						'**+If the account doesn´t have auxiliaries and also is from the last level then you can generate it auxiliaries
						'+Si la cuenta no tiene auxiliares y además es de último nivel se le puede generar auxiliares
						
						If lstrReturnValue = "1" Then '+ No tiene
							'If Not insVal_Structure_Down(sAccoun.Text) Then
							If Not lclsLedgerAcc.Val_Structure_Down(nLed_compan, sAccoun) Then
								lstrReturnValue = False
							Else
								lstrReturnValue = True
							End If
						Else
							
							'**+If the account doesn´t have movements then is allow to modify the "Accounting Period" auxiliary
							'+Si la cuenta no tiene movimientos se le permite modificar el auxiliar contable
							
							If Not insValMovementAccount(nLed_compan, sAccoun) Then
								lstrReturnValue = False
							Else
								lstrReturnValue = True
							End If
						End If
					End If
					
				Case "cbeAux"
					
					'**+If the account is from the first level will disable the auxiliary control
					'+Si la cuenta es de primer nivel se desabitita el control de auxiliares automàticos
					
					If InStr(sAccoun, "-") = 0 Then
						lstrReturnValue = "1"
					End If
					
				Case "chkDebit_disabled"
					
					'**+If the account is not from the last level then it doesn´t update the debits blockade
					'+Si la cuenta no es de último nivel no se podran actualizar los bloqueos de debitos
					
					If lclsLedgerAcc.Val_Structure_Down(nLed_compan, sAccoun) Then
						lstrReturnValue = False
					Else
						If Not lclsLedgerAcc.ValAnotherAux(nLed_compan, sAccoun) Then
							lstrReturnValue = True
						Else
							lstrReturnValue = False
						End If
					End If
					
				Case "chkCredit_disabled"
					
					'**+If the account is not from the last level then it doesn´t update the credits blockade
					'+Si la cuenta no es de último nivel no se podran actualizar los bloqueos de creditos
					
					If lclsLedgerAcc.Val_Structure_Down(nLed_compan, sAccoun) Then
						lstrReturnValue = False
					Else
						If Not lclsLedgerAcc.ValAnotherAux(nLed_compan, sAccoun) Then
							lstrReturnValue = True
						Else
							lstrReturnValue = False
						End If
					End If
					
				Case "chkOrgUnit_disabled"
					
					'**+If the account is not fron the last level then it doesn´t update  the blockades if admit a organizative unit
					'+Si la cuenta no es de último nivel no se podran actualizar los bloqueos si admite unidad organizativa
					
					If lclsLedgerAcc.Val_Structure_Down(nLed_compan, sAccoun) Then
						lstrReturnValue = False
					Else
						If Not lclsLedgerAcc.ValAnotherAux(nLed_compan, sAccoun) Then
							lstrReturnValue = True
						Else
							lstrReturnValue = False
						End If
					End If
			End Select
		End If
		
		DefaultValuesCP002 = lstrReturnValue
	End Function
	
	'**%insValPreviousWith_uni_or_Block: verify is the superior level account has
	'**%unity, debits or blocked credits.
	'%insValPreviousWith_uni_or_Block: Esta rútina permite verificar si la cuenta de nivel superior tiene
	'%unidad, debitos o creditos bloqueados.
	Private Function insValPreviousWith_uni_or_Block(ByVal lstrAccount As String, ByVal nLed_compan As Integer, Optional ByVal lblnAux As Boolean = False) As Boolean
		Dim llngLength As Integer
		Dim llngCount As Integer
		Dim mclsLedger_acc As eLedge.LedgerAcc
		mclsLedger_acc = New eLedge.LedgerAcc
		
		insValPreviousWith_uni_or_Block = False
		
		lstrAccount = Trim(lstrAccount)
		
		With mclsLedger_acc
			
			If lblnAux Then 'Validación del nivel sin auxiliar. '**Level validation without auxiliary
				If .ValBlocked(nLed_compan, lstrAccount) Then
					insValPreviousWith_uni_or_Block = True
				End If
				
			Else
				llngLength = Len(lstrAccount)
				
				For llngCount = llngLength To 1 Step -1
					If Mid(lstrAccount, llngCount, 1) <> "-" Then
						Mid(lstrAccount, llngCount, 1) = " "
					Else
						Mid(lstrAccount, llngCount, 1) = " "
						
						Exit For
					End If
					
				Next llngCount
				
				If Trim(lstrAccount) <> "" Then
					If .ValBlocked(nLed_compan, lstrAccount) Then
						insValPreviousWith_uni_or_Block = True
					End If
				End If
			End If
		End With
		
	End Function
	
	'**%insValCp002_k: Validation routine of the window header.
	'%insValCp002_k: Rutina de validación del encabezado de la ventana.
	Public Function insValCP002_k(ByVal sAccount As String, ByVal nLed_compan As Integer, ByVal nAction As Integer, ByVal sCodispl As String, ByVal sAux_accoun As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsLed_compan As eLedge.Led_compan
		
		On Error GoTo insValCP002_k_Err
		
		lclsErrors = New eFunctions.Errors
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsLed_compan = New eLedge.Led_compan
		
		insValCP002_k = CStr(True)
		
		'**+Makes the validation of the "Accounting Period" account field.
		'+Se efectua la validación del campo cuenta contable.
		With mclsLedger_acc
			If Trim(sAccount) = "" Or Trim(sAccount) = "0" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36017)
			Else
				If mclsLed_compan.Find(nLed_compan) Then
					If Trim(sAccount) <> "" Then
						If Not .ValAccountStruc(nLed_compan, sAccount) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36019)
						Else
							If .Find_Account(nLed_compan, sAccount) Then
								If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
									'No tiene
									'**Doesn´t have
									If Trim(sAux_accoun) = String.Empty Or Trim(sAux_accoun) = "0" Then
										Call lclsErrors.ErrorMessage(sCodispl, 36020)
									Else
										If .nAux_create = 1 Then
											Call lclsErrors.ErrorMessage(sCodispl, 736018)
										End If
									End If
									'Cortada
									'**Cut
									If .sStatregt = "4" Then
										Call lclsErrors.ErrorMessage(sCodispl, 7159)
									End If
									
								End If
							Else
								If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
									Call lclsErrors.ErrorMessage(sCodispl, 36010)
								Else
									If Not .ValAccountPrevious(nLed_compan, sAccount) Then
										Call lclsErrors.ErrorMessage(sCodispl, 36018)
									End If
								End If
							End If
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 12093)
				End If
			End If
			
			'**+Makes the validation of the auxiliary field of the "Accounting Period" account
			'+Se efectua la validación del campo auxiliar de cuenta contable.
			
			If Trim(sAux_accoun) <> "" Then
				If Trim(sAccount) <> "" Then
					If .Find(nLed_compan, sAccount, sAux_accoun) Then
						If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
							Call lclsErrors.ErrorMessage(sCodispl, 36020)
						End If
					Else
						If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
							Call lclsErrors.ErrorMessage(sCodispl, 36010,  , eFunctions.Errors.TextAlign.LeftAling, "(Auxiliar)")
						End If
					End If
				End If
			End If
		End With
		
		insValCP002_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP002_k_Err: 
		If Err.Number Then
			insValCP002_k = insValCP002_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValCP002: validate the window detail.
	'%insValCP002: Rutina de validaciones del detalle de la ventana.
	Public Function insValCP002(ByVal nLed_compan As Integer, ByVal sAccoun As String, ByVal sCodispl As String, ByVal nAction As Integer, ByVal lstrType As String, ByVal sType_accoun As String, ByVal sDescript As String, ByVal sType_aux As String, ByVal sAux_accoun As String, ByVal sBudget As String) As String
		Dim lblnError As Boolean
		Dim lclsErrors As eFunctions.Errors
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsLed_compan As eLedge.Led_compan
		Dim lstrTypeLevelPrevious As String
		
		On Error GoTo insValCP002_Err
		
		lclsErrors = New eFunctions.Errors
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsLed_compan = New eLedge.Led_compan
		
		lblnError = True
		
		If nAction <> eFunctions.Menues.TypeActions.clngActioncut Then
			'+Se efectua la validación del campo tipo de cuenta.
			
			If sType_accoun = "" Or sType_accoun = "0" Or sType_accoun = CStr(eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36022)
			End If
			
			If Not (sType_accoun = "" Or sType_accoun = "0" Or sType_accoun = CStr(eRemoteDB.Constants.intNull)) Then
				
				'+Se valida el tipo de cuenta con respecto a la de nivel superior
				
				lstrTypeLevelPrevious = mclsLedger_acc.ValAccountPreviousType(nLed_compan, sAccoun)
				
				If lstrTypeLevelPrevious <> " " Then
					Select Case sType_accoun
						Case "1"
							'+ Activo
							If lstrTypeLevelPrevious <> "1" And lstrTypeLevelPrevious <> "5" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
							
							'+ Pasivo
						Case "2"
							If lstrTypeLevelPrevious <> "2" And lstrTypeLevelPrevious <> "5" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
							
							'+ Gastos
						Case "3"
							If lstrTypeLevelPrevious <> "3" And lstrTypeLevelPrevious <> "6" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
							
							'+ Ingresos
						Case "4"
							If lstrTypeLevelPrevious <> "4" And lstrTypeLevelPrevious <> "6" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
							
							'+ Variado balance general
						Case "5"
							If lstrTypeLevelPrevious <> "5" And lstrTypeLevelPrevious <> "1" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
							
							'+ Variado ganancias y perdidas
						Case "6"
							If lstrTypeLevelPrevious <> "6" And lstrTypeLevelPrevious <> "4" And lstrTypeLevelPrevious <> "3" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36024)
								lblnError = False
							End If
					End Select
				End If
				
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					
					'+Se valida cuando se ha verificado que ha mandado el error numero 36024
					If lblnError Then
						
						'+Se valida que si se cambia el tipo de cuenta de gastos o ingresos a otro tipo la cuenta no tenga
						'+presupuestos definidos, además si la cuenta tiene niveles inferiores se advierte al usuario que las
						'+cuentas de niveles inferiores cambiaran de tipo.
						If Trim(lstrType) <> Trim(sType_accoun) Then
							With mclsLedger_acc
								If Trim(lstrType) = "3" Or Trim(lstrType) = "4" Then
									If Not .ValBudgetDef(nLed_compan, sAccoun) Then
										If .Val_Structure_Down(nLed_compan, sAccoun) Then
											Call lclsErrors.ErrorMessage(sCodispl, 736017)
										End If
									End If
								Else
									If .Val_Structure_Down(nLed_compan, sAccoun) Then
										Call lclsErrors.ErrorMessage(sCodispl, 736017)
									End If
								End If
							End With
						End If
					End If
					'Si Tipo de auxiliar no corresponde con el definido para la cuenta
					If mclsLedger_acc.Find_Account(nLed_compan, sAccoun) Then
						If mclsLedger_acc.nAux_create <> CDbl(sType_aux) Then
							Call lclsErrors.ErrorMessage(sCodispl, 55991)
						End If
					End If
					
				End If
				
				If sType_accoun <> "3" And sType_accoun <> "4" And sBudget = "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 36034)
				End If
				
			End If
			
			'+Se efectua la validación del campo descripción
			If Trim(sDescript) = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36025)
			End If
			
			'+Se efectua la validación del campo creacion de auxiliar.
			If sType_aux = "" Or sType_aux = "0" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36028)
			End If
			
			'Si la accion es eliminar se valida que la cuenta tenga el saldo en cero.
		Else
			If mclsLedger_acc.Find(nLed_compan, sAccoun, sAux_accoun) Then
				If mclsLedger_acc.nBalance <> 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 36032)
				Else
					If mclsLedger_acc.Val_Structure_Down(nLed_compan, sAccoun) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36033)
					Else
						If Not ((Trim(sAux_accoun) = String.Empty And Not mclsLedger_acc.ValAnotherAux(nLed_compan, sAccoun)) Or Trim(sAux_accoun) <> String.Empty) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36033)
						End If
					End If
				End If
				
			End If
		End If
		
		insValCP002 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP002_Err: 
		If Err.Number Then
			insValCP002 = insValCP002 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	'**%insPostCP002: validate all the data entered in the form
	'%insPostCP002: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCP002(ByVal nAction As Integer, ByVal lstrType As String, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sAux_account As String, ByVal sOrgUnit As String, ByVal sAdjust As String, ByVal sType_creaux As Integer, ByVal sCredit As String, ByVal sDebit As String, ByVal sBudget As String, ByVal sDescript As String, ByVal sType_acc As String, ByVal nUsercode As Integer, ByVal ldblTotalDeb As Double, ByVal ldblTotalCre As Double) As Boolean
		On Error GoTo insPostCP002_Err
		
		insPostCP002 = True
		
		Select Case nAction
			
			'+Si la opción seleccionada es Registrar
			Case eFunctions.Menues.TypeActions.clngActionadd
				Call insCreLedger_acc(nLed_compan, sAccount, sAux_account, sOrgUnit, sAdjust, sType_creaux, sCredit, sDebit, sBudget, sDescript, sType_acc, nUsercode)
				
				'+Si la opción seleccionada es Modificar
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				Call insUpdLedger_acc(lstrType, nLed_compan, sAccount, sAux_account, sOrgUnit, sAdjust, sType_creaux, sCredit, sDebit, sBudget, sDescript, sType_acc, nUsercode)
				
				'+Si la opción seleccionada es Eliminar
			Case eFunctions.Menues.TypeActions.clngActioncut
				Call insDelLedger_acc(ldblTotalDeb, ldblTotalCre, nLed_compan, sAccount, sAux_account)
				
		End Select
		
insPostCP002_Err: 
		If Err.Number Then
			insPostCP002 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insCreLedger_acc: This routine inserts info in the "Accounting Period" accounts table Ledger_acc
	'%insCreLedger_acc: Esta rútina permite insertar información en la tabla de cuentas
	'contables Ledger_acc
	Private Function insCreLedger_acc(ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal gmtAux As String, ByVal sOrgan_unit As String, ByVal sAdju_exci As String, ByVal nAux_create As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sBudget_ind As String, ByVal sDescript As String, ByVal sType_acc As String, ByVal nUsercode As Integer) As Boolean
        'Dim llngCount As Integer
		Dim lclsLedger_Acc As LedgerAcc
		lclsLedger_Acc = New LedgerAcc
		
		insCreLedger_acc = False
		
		With lclsLedger_Acc
			'+ nLed_compan
			.nLed_compan = nLed_compan
			'+ sAccount
			.sAccount = Trim(sAccount)
			
			If Trim(gmtAux) <> "" Then
				'+ sAux_accoun
				.sAux_accoun = Trim(gmtAux)
			Else
				.sAux_accoun = CN_EMPTYAUX
			End If
			
			If Trim(sOrgan_unit) = "1" Then
				'+ sOrgan_unit
				.sOrgan_unit = "1"
			Else
				.sOrgan_unit = "2"
			End If
			
			If Trim(sAdju_exci) = "1" Then
				'+ sAdju_exci
				.sAdju_exci = "1"
			Else
				.sAdju_exci = "2"
			End If
			
			'+ nAux_create
			.nAux_create = CInt(nAux_create)
			
			If Trim(sBlock_cre) = "1" Then
				'+ sBlock_cre
				.sBlock_cre = "1"
			Else
				.sBlock_cre = "2"
			End If
			
			If Trim(sBlock_deb) = "1" Then
				'+ sBlock_deb
				.sBlock_deb = "1"
			Else
				.sBlock_deb = "2"
			End If
			
			If Trim(sBudget_ind) = "1" Then
				'+ sBudget_ind
				.sBudget_ind = "1"
			Else
				.sBudget_ind = "2"
			End If
			
			'+sDescript
			.sDescript = Trim(sDescript)
			'+sStatregt
			.sStatregt = "1"
			'+sType_acc
			.sType_acc = Trim(Str(CDbl(sType_acc)))
			'+nUsercode
			.nUsercode = CInt(nUsercode)
			
			'**+Doesn´t have or manual
			'+No tiene o manual
			If nAux_create <> 1 And nAux_create <> 2 Then
				.sOrgan_unit = "2"
				.sBlock_cre = "2"
				.sBlock_deb = "2"
				.sBudget_ind = "2"
			End If
			
			If .Add Then
				insCreLedger_acc = True
				
				Select Case nAux_create
					
					'+ Sucursal
					Case 3
						.CreAux(LedgerAcc.eTypeAux.eSucursal)
						'+ Ramo
					Case 4
						.CreAux(LedgerAcc.eTypeAux.eRamo)
						'+ Intermediario"
					Case 5
						.CreAux(LedgerAcc.eTypeAux.eIntermediario)
						'+ Banco"
					Case 6
						.CreAux(LedgerAcc.eTypeAux.eBanco)
						'+ Departamento"
					Case 7
						.CreAux(LedgerAcc.eTypeAux.eDepartamento)
						'+ Moneda"
					Case 8
						.CreAux(LedgerAcc.eTypeAux.eMoneda)
						'+ Co/Reaseguradores
					Case 9
						.CreAux(LedgerAcc.eTypeAux.eCo_Reaseguradores)
						'+ Ramos contables
					Case 10
						.CreAux(LedgerAcc.eTypeAux.eRamos_contables)
						
						'+ Control de Cambio: "cuentas complementarias"
						
						'+ Moneda/Sucursal
					Case 11
						.CreAux(LedgerAcc.eTypeAux.eMonedaSucursal)
						
						'+ Moneda/Sucursal/Ramo
					Case 12
						.CreAux(LedgerAcc.eTypeAux.eMonedaSucursalRamo)
						
						'+ CoReasegurador/Moneda
					Case 13
						.CreAux(LedgerAcc.eTypeAux.eCo_ReaseguradorMoneda)
						
						'+ Moneda/CoReasegurador
					Case 14
						.CreAux(LedgerAcc.eTypeAux.eMonedaCo_Reasegurador)
						
						'+Ramo/Sucursal/Moneda
					Case 15
						.CreAux(LedgerAcc.eTypeAux.eRamoSucursalMoneda)
						
						'+Ramo/Moneda
					Case 16
						.CreAux(LedgerAcc.eTypeAux.eRamoMoneda)
						
						'+CoReaseguradores/Ramo/Moneda
					Case 17
						.CreAux(LedgerAcc.eTypeAux.eCo_ReaseguradoresRamoMoneda)
						
						'+Sucursal/Ramo
					Case 18
						.CreAux(LedgerAcc.eTypeAux.SucursalRamo)
						
				End Select
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedger_Acc = Nothing
	End Function
	
	'**%insUpdLedger_acc: update the "Accounting Period" accounts table Ledger_acc
	'%insUpdLedger_acc: esta rútina permite actualizar la tabla de cuentas contables
	'%Ledger_acc
	Private Function insUpdLedger_acc(ByVal lstrType As String, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal gmtAux As String, ByVal sOrgan_unit As String, ByVal sAdju_exci As String, ByVal nAux_create As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sBudget_ind As String, ByVal sDescript As String, ByVal sType_acc As String, ByVal nUsercode As Integer) As Boolean
        'Dim llngCount As Integer
		Dim lclsLedger_Acc As LedgerAcc
		
		lclsLedger_Acc = New LedgerAcc
		
		insUpdLedger_acc = False
		
		With lclsLedger_Acc
			.nLed_compan = nLed_compan
			.sAccount = Trim(sAccount)
			
			If Trim(gmtAux) <> "" Then
				.sAux_accoun = Trim(gmtAux)
			Else
				.sAux_accoun = CN_EMPTYAUX
			End If
			
			If Trim(sOrgan_unit) = "1" Then
				.sOrgan_unit = "1"
			Else
				.sOrgan_unit = "2"
			End If
			
			If Trim(sAdju_exci) = "1" Then
				.sAdju_exci = "1"
			Else
				.sAdju_exci = "2"
			End If
			
			.nAux_create = CInt(nAux_create)
			
			If Trim(sBlock_cre) = "1" Then
				.sBlock_cre = "1"
			Else
				.sBlock_cre = "2"
			End If
			
			If Trim(sBlock_deb) = "1" Then
				.sBlock_deb = "1"
			Else
				.sBlock_deb = "2"
			End If
			
			'+ Gastos o ingresos '**+Expenses or incomes
			If Trim(sType_acc) = "3" Or Trim(sType_acc) = "4" Then
				If Trim(sBudget_ind) = "1" Then
					.sBudget_ind = "1"
				Else
					.sBudget_ind = "2"
				End If
			Else
				.sBudget_ind = "2"
			End If
			
			.sDescript = Trim(sDescript)
			.sStatregt = "1"
			.sType_acc = CStr(sType_acc)
			.nUsercode = CInt(nUsercode)
			
			'**+Update the account type of the lower levels in case it has it
			'**+if the previous type was expenses or incomes then update the indicator
			'**%that admit budget
			'+Se actualiza el tipo de cuenta de los niveles inferiores en caso de tenerlos
			'+si el tipo anterior era de gastos o ingresos se actualiza también el indicador
			'+de admite presupuestos
			
			If Trim(lstrType) <> "" Then
				If Trim(lstrType) <> Trim(sType_acc) Then
					If .Val_Structure_Down(nLed_compan, sAccount) Then
						If Trim(sType_acc) <> "3" And Trim(sType_acc) <> "4" Then
							.Update_Type_BudgetLevelDown(nLed_compan, sAccount, CStr(sType_acc), "2")
						Else
							.Update_TypeLevelDown(nLed_compan, sAccount, CStr(sType_acc))
						End If
					Else
						If .ValAnotherAux(nLed_compan, sAccount) Then
							If Trim(sType_acc) <> "3" And Trim(sType_acc) <> "4" Then
								.Update_Type_BudgetLevelDown(nLed_compan, sAccount, CStr(sType_acc), "2")
							Else
								.Update_TypeLevelDown(nLed_compan, sAccount, CStr(sType_acc))
							End If
						End If
					End If
				End If
			End If
			
			If nAux_create <> 1 And nAux_create <> 2 Then 'No tiene o manual '**Doesn´t has or manual
				If Trim(gmtAux) = "" Then
					.sOrgan_unit = "2"
					.sBlock_cre = "2"
					.sBlock_deb = "2"
					.sBudget_ind = "2"
				End If
			End If
			
			'If nAux_create <> llngAuxPrev Then
			'    .DelAuxiliars nLed_compan, sAccount
			'End If
			
			If .Update Then
				insUpdLedger_acc = True
				
				'**+if the updated account is father only without auxiliary then generate the automatic auxiliaries
				'+Solo si la cuenta actualizada es padre sin auxiliar se generan los auxiliares automáticos
				
				If Trim(gmtAux) = "" Then
					
					'**+Creation of the automatic auxiliaries
					'+ Creación de auxiliares automáticos.
					
					Select Case nAux_create
						'**+Branch office
						'+ Sucursal
						Case 3
							.CreAux(LedgerAcc.eTypeAux.eSucursal)
							'**+Branch
							'+ Ramo
						Case 4
							.CreAux(LedgerAcc.eTypeAux.eRamo)
							'**+Intermediary
							'+ Intermediario"
						Case 5
							.CreAux(LedgerAcc.eTypeAux.eIntermediario)
							'**+Bank
							'+ Banco"
						Case 6
							.CreAux(LedgerAcc.eTypeAux.eBanco)
							'**+Departament
							'+ Departamento"
						Case 7
							.CreAux(LedgerAcc.eTypeAux.eDepartamento)
							'**+Currency
							'+ Moneda"
						Case 8
							.CreAux(LedgerAcc.eTypeAux.eMoneda)
							'**+Co/Reainsuranced
							'+ Co/Reaseguradores
						Case 9
							.CreAux(LedgerAcc.eTypeAux.eCo_Reaseguradores)
							'**+Accounting Period" branch
							'+ Ramos contables
						Case 10
							.CreAux(LedgerAcc.eTypeAux.eRamos_contables)
							'+ Control de Cambio: "cuentas complementarias"
							
							'+ Moneda/Sucursal
						Case 11
							.CreAux(LedgerAcc.eTypeAux.eMonedaSucursal)
							
							'+ Moneda/Sucursal/Ramo
						Case 12
							.CreAux(LedgerAcc.eTypeAux.eMonedaSucursalRamo)
							
							'+ CoReasegurador/Moneda
						Case 13
							.CreAux(LedgerAcc.eTypeAux.eCo_ReaseguradorMoneda)
							
							'+ Moneda/CoReasegurador
						Case 14
							.CreAux(LedgerAcc.eTypeAux.eMonedaCo_Reasegurador)
							
							'+Ramo/Sucursal/Moneda
						Case 15
							.CreAux(LedgerAcc.eTypeAux.eRamoSucursalMoneda)
							
							'+Ramo/Moneda
						Case 16
							.CreAux(LedgerAcc.eTypeAux.eRamoMoneda)
							
							'+CoReaseguradores/Ramo/Moneda
						Case 17
							.CreAux(LedgerAcc.eTypeAux.eCo_ReaseguradoresRamoMoneda)
							
							'+Sucursal/Ramo
						Case 18
							.CreAux(LedgerAcc.eTypeAux.SucursalRamo)
							
					End Select
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedger_Acc = Nothing
	End Function
	
	'**%insDelLedger_acc: delete physically one "Accounting Period" account of the Ledger_acc table
	'%insDelLedger_acc: Esta rútina permite eliminar fisicamente una cuentas contables
	'%de la tabla Ledger_acc
	Private Function insDelLedger_acc(ByVal ldblTotalDeb As Double, ByVal ldblTotalCre As Double, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sAux_accoun As String) As Boolean
		Dim lclsLedger_Acc As LedgerAcc
		
		lclsLedger_Acc = New LedgerAcc
		On Error GoTo insDelLedger_acc_err
		insDelLedger_acc = False
		
		'**Cannot delete one account that has balance or lower levels
		'+ No se podrá eliminar una cuenta que tenga saldos o que tenga niveles inferiores
		
		With lclsLedger_Acc
			.nLed_compan = nLed_compan
			.sAccount = sAccount
			.sAux_accoun = sAux_accoun
			If .Delete Then
				insDelLedger_acc = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedger_Acc = Nothing
		
insDelLedger_acc_err: 
		If Err.Number Then
			insDelLedger_acc = False
		End If
		
		On Error GoTo 0
	End Function
End Class






