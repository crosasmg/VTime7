Option Strict Off
Option Explicit On
Public Class Bal_histor
	'%-------------------------------------------------------%'
	'% $Workfile:: Bal_histor.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                   Type    Computed  Length Prec Scale Nullable  TrimTrailingBlanks FixedLenNullInSource
	Public nLed_compan As Integer 'smallint   no       2     5     0     no            (n/a)                (n/a)
	Public sAccount As String 'char       no      20                 no            yes                   no
	Public sAux_accoun As String 'char       no      20                 no            yes                   no
	Public sCost_cente As String 'char       no       8                 no            yes                   no
	Public nYear As Integer 'smallint   no       2     5     0     no            (n/a)                (n/a)
	Public nMonth As Integer 'smallint   no       2     5     0     no            (n/a)                (n/a)
	Public nBalance As Double 'decimal    no       9    12     2     yes           (n/a)                (n/a)
	Public nCredit As Double 'decimal    no       9    12     2     yes           (n/a)                (n/a)
	Public nDebit As Double 'decimal    no       9    12     2     yes           (n/a)                (n/a)
	Public sPreliminar As String 'char       no       1                 yes           yes                  yes
	Public sStatregt As String 'char       no       1                 yes           yes                  yes
	Public nUsercode As Integer 'smallint   no       2     5     0     yes           (n/a)                (n/a)
	Public nLed_year As Integer 'smallint   no       2     5     0     yes           (n/a)                (n/a)
	Public nInd_automa As Integer 'smallint   no       2     5     0     yes           (n/a)                (n/a)
	
	'**+Auxiliary variables
	'+ Variables auxiliares
	
	Public nInitYear As Integer
	Public nPeriodInitYear As Integer
	Public nLastMonth As Integer
	Public nLastYear As Integer
	Public nMaxLedYear As Integer
	
	
	'**% MaxLedYear: return the last countable year of a given company
	'% MaxLedYear: Devuelve el ultimo agno contable de una compagnia dada
	Public Function MaxLedYear(ByVal intLed_compan As Integer, ByVal intLed_year As Integer) As Boolean
		
		'**-Define the variable lrecreaBal_historMaxYear
		'- Se define la variable lrecreaBal_historMaxYear
		Dim lrecreaBal_historMaxYear As eRemoteDB.Execute
		
		lrecreaBal_historMaxYear = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'lrecreaBal_historMaxYear'
		'**+Data read on 05/24/2001 01:55:35 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaBal_historMaxYear'
		'+ Informacion leida el 24/05/2001 01:55:35 PM
		
		With lrecreaBal_historMaxYear
			.StoredProcedure = "reaBal_historMaxYear"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("MaxYear", eRemoteDB.Constants.intNull) <> eRemoteDB.Constants.intNull Then
					If .FieldToClass("MaxYear") > CShort(intLed_year) Then
						MaxLedYear = False
					Else
						MaxLedYear = True
					End If
				Else
					MaxLedYear = True
				End If
				.RCloseRec()
			Else
				MaxLedYear = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaBal_historMaxYear may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBal_historMaxYear = Nothing
	End Function
	
	'**% Add: add a new balance (close) of the account to the table Bal_histor
	'% Add: Agrega un nuevo balance (cierre) de cuenta a la tabla Bal_histor
	Public Function Add() As Boolean
		
		'**-Define the variable lreccreBal_Histor
		'- Se define la variable lreccreBal_Histor
		Dim lreccreBal_Histor As eRemoteDB.Execute
		
		lreccreBal_Histor = New eRemoteDB.Execute
		On Error GoTo Add_err
		'**+ Parameters definition for the stored procedure 'insudb.creBal_Histor'
		'**+Data read on 06/12/2001 10:33:15 AM
		'+ Definicion de parametros para stored procedure 'insudb.creBal_Histor'
		'+ Informacion leida el 12/06/2001 10:33:15 AM
		
		With lreccreBal_Histor
			.StoredProcedure = "creBal_Histor"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminar", sPreliminar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_year", nLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_Automa", nInd_automa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreBal_Histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreBal_Histor = Nothing
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find: return the balance (close) of the account to determinate month and year
	'% Find: Devuelve el balance (cierre) de una cuenta para determinado mes y agno
	Public Function Find(ByVal intLed_compan As Integer, ByVal strAccount As String, ByVal strAux As String, ByVal strCost_cente As String, ByVal intYear As Integer, ByVal intMonth As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**-Define the variable lrecreaBal_histor
		'- Se define la variable lrecreaBal_histor
		Dim lrecreaBal_histor As eRemoteDB.Execute
		
		lrecreaBal_histor = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'**+Parameters definition for the stored procedure 'insudb.reaBal_histor'
		'**+Data read on 06/12/2001 10:46:39 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaBal_histor'
		'+ Informacion leida el 12/06/2001 10:46:39 AM
		
		With lrecreaBal_histor
			.StoredProcedure = "reaBal_histor"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", strAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", strAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", strCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", intYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", intMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				sAux_accoun = .FieldToClass("sAux_accoun")
				sCost_cente = .FieldToClass("sCost_cente")
				nYear = .FieldToClass("nYear")
				nMonth = .FieldToClass("nMonth")
				nBalance = .FieldToClass("nBalance")
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				sPreliminar = .FieldToClass("sPreliminar")
				sStatregt = .FieldToClass("sStatregt")
				nLed_year = .FieldToClass("nLed_Year")
				nInd_automa = .FieldToClass("nInd_automa")
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaBal_histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBal_histor = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%DefaultValuesCP003: this function is incharge to make the habilitation or des-habilitation of the
	'**%window fields CP003
	'%DefaultValuesCP003:Esta función se encarga de realizar la habilitación o des-habilitación de los
	'%campos de la ventana CP003
	Public Function DefaultValuesCP003(ByVal nAction As Integer, ByVal sField As String, ByVal nLed_compan As Integer, ByVal sAccount As String) As Object
        Dim lstrReturnValue As Object = Nothing
		Dim mclsLedger_acc As eLedge.LedgerAcc
		mclsLedger_acc = New eLedge.LedgerAcc
		
		If mclsLedger_acc.Find_Account(nLed_compan, sAccount) Then
			Select Case sField
				Case "optStyle1"
					If mclsLedger_acc.sType_acc = "1" Or mclsLedger_acc.sType_acc = "2" Then
						lstrReturnValue = "1"
					Else
						lstrReturnValue = "2"
					End If
				Case "optStyle0"
					If mclsLedger_acc.sType_acc = "1" Or mclsLedger_acc.sType_acc = "2" Then
						lstrReturnValue = "2"
					Else
						lstrReturnValue = "1"
					End If
				Case "valUnit"
					If Trim(mclsLedger_acc.sOrgan_unit) = "2" Then 'Significa que la cta no debe tener unidad org.
						lstrReturnValue = mclsLedger_acc.sOrgan_unit
					End If
				Case "valUnit_disabled"
					If Trim(mclsLedger_acc.sOrgan_unit) = "2" Then 'Significa que la cta no debe tener unidad org.
						lstrReturnValue = "true"
					Else
						lstrReturnValue = "false"
					End If
					'/////////////////////////////////////////////////////////////////
			End Select
		End If
		'**+If the selected option is Add
		'+Si la opción seleccionada es Agregar
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			Select Case sField
				Case "tctPer"
					lstrReturnValue = ""
				Case "tcnPer_disabled"
					lstrReturnValue = "true"
				Case "tcnYear"
					lstrReturnValue = "0"
				Case "tctYear_disabled"
					lstrReturnValue = "false"
				Case "tcnDeb"
					lstrReturnValue = "0"
				Case "tcnDeb_disabled"
					lstrReturnValue = "false"
				Case "tcnCred"
					lstrReturnValue = "0"
				Case "tcnCred_disabled"
					lstrReturnValue = "false"
				Case "tcnBalance"
					lstrReturnValue = "0"
				Case "tcnBalance_disabled"
					lstrReturnValue = "true"
				Case "tcnBal_Comp"
					lstrReturnValue = "0"
				Case "tcnBal_Comp_disabled"
					lstrReturnValue = "true"
				Case "tcnDiference"
					lstrReturnValue = "0"
				Case "tcnDiference_disabled"
					lstrReturnValue = "true"
			End Select
			'**+If the selected option is Modify
			'+Si la opción seleccionada es Modificar
		ElseIf nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then 
			Select Case sField
				Case "tctPer"
					lstrReturnValue = ""
				Case "tcnPer_disabled"
					lstrReturnValue = "true"
				Case "tcnYear"
					lstrReturnValue = "0"
				Case "tctYear_disabled"
					lstrReturnValue = "true"
				Case "tcnDeb"
					lstrReturnValue = "0"
				Case "tcnDeb_disabled"
					lstrReturnValue = "false"
				Case "tcnCred"
					lstrReturnValue = "0"
				Case "tcnCred_disabled"
					lstrReturnValue = "false"
				Case "tcnBalance"
					lstrReturnValue = "0"
				Case "tcnBalance_disabled"
					lstrReturnValue = "true"
				Case "tcnBal_Comp"
					lstrReturnValue = "0"
				Case "tcnBal_Comp_disabled"
					lstrReturnValue = "true"
				Case "tcnDiference"
					lstrReturnValue = "0"
				Case "tcnDiference_disabled"
					lstrReturnValue = "true"
			End Select
		End If
		'**+If the selected option is Consult
		'+Si la opción seleccionada es Consultar
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			'If lclsBal_histor.Find(nLed_compan, sAccount, sAux, sCost_cente, nYear, nMonth) Then
			Select Case sField
				Case "tctPer"
					lstrReturnValue = ""
				Case "tcnPer_disabled"
					lstrReturnValue = "true"
				Case "tcnYear"
					lstrReturnValue = "0"
				Case "tctYear_disabled"
					lstrReturnValue = "true"
				Case "tcnDeb"
					lstrReturnValue = "0"
				Case "tcnDeb_disabled"
					lstrReturnValue = "true"
				Case "tcnCred"
					lstrReturnValue = "0"
				Case "tcnCred_disabled"
					lstrReturnValue = "true"
				Case "tcnBalance"
					lstrReturnValue = "0"
				Case "tcnBalance_disabled"
					lstrReturnValue = "true"
				Case "tcnBal_Comp"
					lstrReturnValue = "0"
				Case "tcnBal_Comp_disabled"
					lstrReturnValue = "true"
				Case "tcnDiference"
					lstrReturnValue = "0"
				Case "tcnDiference_disabled"
					lstrReturnValue = "true"
			End Select
		End If
		'    End If
		DefaultValuesCP003 = lstrReturnValue
	End Function
	
	'**%insValCp003_k: routine to validate the window header.
	'%insValCp003_k: Rutina de validación del encabezado de la ventana.
	Public Function insValCP003_k(ByVal ValAccount As String, ByVal nLed_compan As Integer, ByVal plngAction As Integer, ByVal sCodispl As String, ByVal valAux As String, ByVal cboCompare As Integer, ByVal optSel0 As String, ByVal optStyle0 As String, ByVal valUnit As String, ByVal nLedger_Year As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsTab_cost_c As eLedge.Tab_cost_c
		Dim mclsLed_compan As eLedge.Led_compan
		
		On Error GoTo insValCP003_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsTab_cost_c = New eLedge.Tab_cost_c
		mclsLed_compan = New eLedge.Led_compan
		
		insValCP003_k = CStr(True)
		
		'**+If the validation is executed of the comparison form.
		'+Se efectua la validación del campo forma de comparación.
		
		If plngAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			If cboCompare = eRemoteDB.Constants.intNull Or cboCompare = 0 Then
				If optSel0 = "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 700024)
				End If
			Else
				If Not optSel0 = "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 700001)
				End If
			End If
		End If
		
		'**+Execute the validation of the countable account field.
		'+Se efectua la validación del campo cuenta contable.
		
		If Trim(ValAccount) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 36017)
		Else
			If Trim(ValAccount) <> String.Empty Then
				If Not mclsLedger_acc.Find_Account(nLed_compan, Trim(ValAccount)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36010)
					'**+Validate that is one account of the last level is adding, modifying or deleting.
					'+Se valida que sea una cuenta de último nivel si se está agregando, modificando
					'+o eliminando.
				Else
					If plngAction <> eFunctions.Menues.TypeActions.clngActionQuery And mclsLedger_acc.Val_Structure_Down(nLed_compan, Trim(ValAccount)) Then
						Call lclsErrors.ErrorMessage(sCodispl, 7129)
					End If
				End If
			End If
		End If
		
		'**+Execute the validation of the auxiliary field of the countable account.
		'+Se efectua la validación del campo auxiliar de cuenta contable.
		If Trim(ValAccount) <> String.Empty Then
			If Not mclsLedger_acc.Find(nLed_compan, Trim(ValAccount), Trim(valAux)) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36211)
			Else
				If plngAction <> eFunctions.Menues.TypeActions.clngActionQuery And Trim(valAux) = "" Then
					If mclsLedger_acc.ValAnotherAux(nLed_compan, ValAccount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 7129)
					End If
				End If
			End If
		End If
		'**+Validation of the organizative unity code - valUnit.
		'+Validación de Código de la unidad organizativa - valUnit.
		If Trim(valUnit) = "" Then
			If Trim(ValAccount) <> "" Then
				Call mclsLedger_acc.Find(nLed_compan, Trim(ValAccount), Trim(valAux))
				If Trim(mclsLedger_acc.sOrgan_unit) = "1" Then 'Significa que la cta debe tener unidad org.
					Call lclsErrors.ErrorMessage(sCodispl, 700024)
				End If
			End If
		Else
			If Trim(valUnit) <> "" Then
				If Trim(ValAccount) <> "" Then
					Call mclsLedger_acc.Find(nLed_compan, Trim(ValAccount), Trim(valAux))
					If Trim(mclsLedger_acc.sOrgan_unit) = "2" Then 'Significa que la cta no necesita tener unidad org.
						Call lclsErrors.ErrorMessage(sCodispl, 700014)
					Else
						'**+Get the info of the cost center
						'+Se recupera la información del centro de costo
						If Not mclsTab_cost_c.Val_Unit_Organ_struct(nLed_compan, valUnit) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36073)
						Else
							If Not mclsTab_cost_c.Find(nLed_compan, Trim(valUnit)) Then
								Call lclsErrors.ErrorMessage(sCodispl, 36050)
							End If
						End If
					End If
				End If
			End If
		End If
		
		'**+Execute the validation of the exercise.
		'+Se efectua la validación del ejercicio.
		If nLedger_Year = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36036)
		Else
			If nLedger_Year <> 0 Then
				'**+Validation that only permit to input the closed exercises
				'+Validación que solo permite ingresar ejercicios cerrados
				If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
					
					If nLedger_Year >= Year(mclsLed_compan.dEndLedDat) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36222)
					End If
				End If
			End If
		End If
		'**+Validate that was selected the consult with acumulated balance if the account is
		'**+active or passive
		'+Se valida que se haya seleccionado consulta de saldos acumulados si la cuenta es
		'+de activo o pasivo
		
		If plngAction = eFunctions.Menues.TypeActions.clngActionQuery And (mclsLedger_acc.sType_acc = "1" Or mclsLedger_acc.sType_acc = "2" Or mclsLedger_acc.sType_acc = "7") Then
			If optStyle0 = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36216)
			End If
		End If
		
		
		
		
		insValCP003_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object mclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsTab_cost_c = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP003_k_Err: 
		If Err.Number Then
			insValCP003_k = insValCP003_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	'**%insValCP003: routine of window header validation.
	'%insValCP003: Rutina de validación del encabezado de la ventana.
	Public Function insValCP003(ByVal nLed_compan As Integer, ByVal plngAction As Integer, ByVal sCodispl As String, ByVal valAux As String, ByVal nLedger_Year As Integer, ByVal sAccount As String, ByVal sCost_cente As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nDeb As Double, ByVal nCre As Double, ByVal sInd As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsTab_cost_c As eLedge.Tab_cost_c
		Dim mclsLed_compan As eLedge.Led_compan
		
		On Error GoTo insValCP003_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsTab_cost_c = New eLedge.Tab_cost_c
		mclsLed_compan = New eLedge.Led_compan
		
		insValCP003 = CStr(True)
		
		'**+Validates the year of the first record of the grid (because the rest are generate
		'**+automatically)
		'+Se valida el año del primer registro del grid (porque los demas se generan
		'+automáticamente)
		
		If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
			If Trim(CStr(nYear)) <> CStr(eRemoteDB.Constants.intNull) Then
				If IsNumeric(nYear) Then
					If nYear = 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 700001)
					Else
						If Not insValInit_year(CInt(nYear), nLed_compan, valAux, nLedger_Year, sAccount, sCost_cente, nYear, nMonth) Then
							Call lclsErrors.ErrorMessage(sCodispl, 700001)
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 700007)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 700001)
			End If
		End If
		
		'**+Validate the debit amount
		'+Se valida el importe de débito
		If Trim(CStr(nDeb)) <> CStr(eRemoteDB.Constants.intNull) Then
			If CDbl(nDeb) <> 0 Then
				If mclsLedger_acc.Find_Account(nLed_compan, sAccount) Then
					If mclsLedger_acc.sBlock_deb = "1" Then
						Call lclsErrors.ErrorMessage(sCodispl, 36039)
					Else
						If mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
							If mclsTab_cost_c.sBlock_deb = "1" And Trim(sCost_cente) <> "" Then
								Call lclsErrors.ErrorMessage(sCodispl, 36055)
							End If
						End If
					End If
				End If
			End If
		End If
		
		'**+Validate the credit import
		'+Se valida el importe de crédito
		
		If Trim(CStr(nCre)) <> CStr(eRemoteDB.Constants.intNull) Then
			If CDbl(nCre) <> 0 Then
				If mclsLedger_acc.Find_Account(nLed_compan, sAccount) Then
					If mclsLedger_acc.sBlock_cre = "1" Then
						Call lclsErrors.ErrorMessage(sCodispl, 36040)
					Else
						If mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
							If mclsTab_cost_c.sBlock_cre = "1" And Trim(sCost_cente) <> String.Empty Then
								Call lclsErrors.ErrorMessage(sCodispl, 36056)
							End If
						End If
					End If
				End If
			End If
		End If
		
		'**+If the action is modify validate that the movement is not automatic
		'+Si la acción es modificar se valida que el movimiento no sea automático
		If plngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			If (Trim(sInd)) = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36215)
			End If
		End If
		
		
		insValCP003 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object mclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsTab_cost_c = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP003_Err: 
		If Err.Number Then
			insValCP003 = insValCP003 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'**%insValInit_yaar: Validates the stared year of the exercise
	'%insValInit_year: Se valida el año de inicio del ejercicio
	Private Function insValInit_year(ByVal llngYearV As Integer, ByVal nLed_compan As Integer, ByVal valAux As String, ByVal nLedger_Year As Integer, ByVal sAccount As String, ByVal sCost_cente As String, ByVal nYear As Integer, ByVal nMonth As Integer) As Boolean
		Dim pclsBal_histor As eLedge.Bal_histor
		Dim mclsLed_compan As eLedge.Led_compan
		
		pclsBal_histor = New eLedge.Bal_histor
		mclsLed_compan = New eLedge.Led_compan
		
		On Error GoTo insValInit_year_err
		
		If pclsBal_histor.Find(nLed_compan, sAccount, valAux, sCost_cente, nYear, nMonth) Then
			If mclsLed_compan.Find(nLed_compan) Then
				
				If Not pclsBal_histor.PeriodInitYear(nLed_compan, Trim(sAccount), Trim(valAux), Trim(sCost_cente), CShort(nLedger_Year), Month(mclsLed_compan.dIniLedDat)) Then
					
					'**+Validate against the start year of the previous exercise
					'+ Se valida contra el año de inicio del ejercicio anterior si este está cargado
					
					If Not pclsBal_histor.PeriodInitYear(nLed_compan, Trim(sAccount), Trim(valAux), Trim(sCost_cente), CShort(Trim(CStr(nLedger_Year))) - 1, Month(mclsLed_compan.dIniLedDat)) Then
						
						'**+Validates that doesn´t input different start years for the same exercise
						'+ Se valida que no se ingresen años de inicio distintos para un mismo ejercicio
						If Not pclsBal_histor.InitYear(nLedger_Year) Then
							insValInit_year = True
						Else
							If pclsBal_histor.nInitYear > llngYearV Then
								insValInit_year = False
							Else
								insValInit_year = True
							End If
						End If
					Else
						If pclsBal_histor.nInitYear + 1 > llngYearV Then
							insValInit_year = False
						Else
							insValInit_year = True
						End If
					End If
				End If
			End If
		End If
insValInit_year_err: 
		If Err.Number Then
			insValInit_year = False
		End If
		On Error GoTo 0
	End Function
	
	'**% PeriodInitYear: Tnis method excecutes the stored procedure insudb.reaBal_historLed_period and it returns true or false depending of the records
	'% PeriodInitYear: este metodo ejecuta el stored procedure insudb.reaBal_historLed_period y retorna true o false dependiendo de los registros
	Public Function PeriodInitYear(ByVal intLed_compan As Integer, ByVal strAccount As String, ByVal strAux As String, ByVal strCost_cente As String, ByVal intLed_year As Integer, ByVal intMonth As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determines the function result (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead_PeriodInitYear As Boolean
		
		'**-Define the variable lrecreaBal_historLed_period
		'- Se define la variable lrecreaBal_historLed_period
		Dim lrecreaBal_historLed_period As eRemoteDB.Execute
		
		lrecreaBal_historLed_period = New eRemoteDB.Execute
		
		On Error GoTo PeriodInitYear_err
		'**+Parameters definition for the stored procedure 'insudb.reaBal_historLed_period'
		'**+Data read on 06/13/2001 09:02:19 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaBal_historLed_period'
		'+ Informacion leida el 13/06/2001 09:02:19 AM
		
		With lrecreaBal_historLed_period
			.StoredProcedure = "reaBal_historLed_period"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", strAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", strAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", strCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Year", intLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", intMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If CShort(.FieldToClass("nYear")) <> eRemoteDB.Constants.intNull Then
					nPeriodInitYear = .FieldToClass("nYear")
					lblnRead_PeriodInitYear = True
				Else
					nPeriodInitYear = 0
					lblnRead_PeriodInitYear = False
				End If
				.RCloseRec()
			Else
				lblnRead_PeriodInitYear = False
			End If
		End With
		
		PeriodInitYear = lblnRead_PeriodInitYear
		'UPGRADE_NOTE: Object lrecreaBal_historLed_period may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBal_historLed_period = Nothing
		
PeriodInitYear_err: 
		If Err.Number Then
			PeriodInitYear = False
		End If
		On Error GoTo 0
	End Function
	
	'**% InitYear: returns the start year in a countable period
	'% InitYear: Devuelve el agno de inicio de un periodo contable
	Public Function InitYear(ByVal intLed_year As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determine the function result (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead_InitYear As Boolean
		
		'**-Define the variable lrecreaBal_historInitYear
		'- Se define la variable lrecreaBal_historInitYear
		Dim lrecreaBal_historInitYear As eRemoteDB.Execute
		
		lrecreaBal_historInitYear = New eRemoteDB.Execute
		
		On Error GoTo initYear_err
		
		'+ Definicion de parametros para stored procedure 'insudb.reaBal_historInitYear'
		'+ Informacion leida el 13/06/2001 09:12:38 AM
		With lrecreaBal_historInitYear
			.StoredProcedure = "reaBal_historInitYear"
			.Parameters.Add("nLed_Year", intLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nYear") <> eRemoteDB.Constants.intNull Then
					nInitYear = .FieldToClass("nYear")
					lblnRead_InitYear = True
				Else
					nInitYear = 0
					lblnRead_InitYear = False
				End If
				.RCloseRec()
			Else
				lblnRead_InitYear = False
			End If
		End With
		
		
		InitYear = lblnRead_InitYear
		'UPGRADE_NOTE: Object lrecreaBal_historInitYear may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBal_historInitYear = Nothing
initYear_err: 
		If Err.Number Then
			InitYear = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insCalc_Prevbal: Calculate the previous balance
	'** Calculate_column 6 (B) Compare the previous balance
	'%insCalc_PrevBal: Se calculan los saldos anteriores
	' Calcula columna 6 (B) Saldo de comparacion
	'UPGRADE_NOTE: NPer was upgraded to NPer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function insCalc_PrevBal(ByVal optStyle0 As String, ByVal optSel0 As String, ByVal optSel1 As String, ByVal optType0 As String, ByVal optType1 As String, ByVal optType2 As String, ByVal NPer_Renamed As Integer, ByVal gstrAccount As String, ByVal gstrAux As String, ByVal valUnit As String, ByVal nYear As Integer) As Object
        Dim lstrReturnValue As Object = String.Empty
        'Dim llngYear_Previous As Integer
        'Dim llngMonth_Previous As Integer
		Dim llngYear As Integer
		Dim llngMonth As Integer
		Dim lblnRead As Boolean
		Dim lblnAcumulate As Boolean
		
		lblnRead = False
		
		
		If optStyle0 = "1" Then
			lblnAcumulate = False
		Else
			lblnAcumulate = True
		End If
		
		
		'**+Previous year
		'+Año anterior
		
		If optSel0 = "1" And optType0 = "1" Then
			llngYear = nYear - 1
			llngMonth = NPer_Renamed
			
			lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
			
			'**+Previous month
			'+Mes anterior
			
		ElseIf optSel1 = "1" And optType0 = "1" Then 
			If lblnRead Then
				If NPer_Renamed = 1 Then
					llngMonth = 12
					llngYear = nYear - 1
					lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
				Else
					llngMonth = NPer_Renamed - 1
					llngYear = nYear
					lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
				End If
			Else
				lstrReturnValue = nBalance
			End If
			
			'**+Previous adjusted year
			'+Año anterior ajustado
			
		ElseIf optSel0 = "1" And optType2 = "1" Then 
			llngYear = nYear - 1
			llngMonth = NPer_Renamed
			lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
			lstrReturnValue = insCalc_Reval_Year(lstrReturnValue, llngYear, llngMonth, nLed_compan)
			
			'**+Previous adjusted year
			'+Mes anterior ajustado
			
		ElseIf optSel1 = "1" And optType2 = "1" Then 
			If NPer_Renamed = 1 Then
				llngMonth = 12
				llngYear = nYear - 1
			Else
				llngMonth = NPer_Renamed - 1
				llngYear = nYear
			End If
			
			If lblnRead Then
				lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
				lstrReturnValue = insCalc_Reval_Month(lstrReturnValue, llngYear, llngMonth, nLed_compan)
			Else
				lstrReturnValue = nBalance
				lstrReturnValue = insCalc_Reval_Month(lstrReturnValue, llngYear, llngMonth, nLed_compan)
			End If
			
			'**+Previous year in dollars
			'+Año anterior dolarizado
			
		ElseIf optSel0 = "1" And optType1 = "1" Then 
			llngYear = nYear - 1
			llngMonth = NPer_Renamed
			lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
			lstrReturnValue = insCalc_Exchange_Year(lstrReturnValue, llngMonth, llngYear)
			
			'**+Previous month in dollars
			'+Mes anterior dolarizado
			
		ElseIf optSel1 = "1" And optType1 = "1" Then 
			If NPer_Renamed = 1 Then
				llngMonth = 12
				llngYear = nYear - 1
			Else
				llngMonth = NPer_Renamed - 1
				llngYear = nYear
			End If
			
			If lblnRead Then
				lstrReturnValue = insCalc_balance(nLed_compan, llngYear, llngMonth, gstrAccount, gstrAux, Trim(valUnit), lblnAcumulate)
				lstrReturnValue = insCalc_Exchange_Month(llngYear, llngMonth, lstrReturnValue)
			Else
				lstrReturnValue = nBalance
				lstrReturnValue = insCalc_Exchange_Month(llngYear, llngMonth, lstrReturnValue)
			End If
		End If
		
		insCalc_PrevBal = lstrReturnValue
	End Function
	'**%insCalc_Difference: calculates the previous balance
	'** Calculate column 7 Diference (A-B)
	'%insCalc_Diference: Se calculan los saldos anteriores
	' Calcula columna 7 Diferencia (A-B)
	Public Function insCalc_Diference(ByVal nBalance As Integer, ByVal nBal_Comp As Integer) As Object
		Dim lstrReturnValue As Object
		
		
		'**+Diference between the Actual Balance and the comparison
		'+ Diferencia entre Saldo Actual y Saldo de comparación
		
		lstrReturnValue = nBalance - nBal_Comp
		
		insCalc_Diference = lstrReturnValue
	End Function
	'**%insCalc_Porc: Calculate the previous balance
	'%insCalc_Porc: Se calculan los saldos anteriores
	' Calcula columna 8 Porcentaje (%)
	Public Function insCalc_Porc(ByVal nBalance As Integer, ByVal nDiference As Integer) As Object
		Dim lstrReturnValue As Object
		
		If nBalance <> 0 Then
			
			'**+ Diference between the actual balance and the comparative balance / Actual balance * 100
			'+ Diferencia entre Saldo Actual y Saldo de comparación / Saldo actual * 100
			
			lstrReturnValue = (nDiference / nBalance) * 100
		Else
			lstrReturnValue = 0
		End If
		
		insCalc_Porc = lstrReturnValue
	End Function
	
	'**% insCalc_balance: recover the balance in a period
	'%insCalc_balance: Se recupera el saldo de un período
	Public Function insCalc_balance(ByVal nLed_compan As Integer, ByVal llngYearBal As Integer, ByVal llngMonthBal As Integer, ByVal lstrAccount As String, ByVal lstrAux As String, ByVal lstrCost_cente As String, ByVal lblnAccumulate As Boolean) As Double
		Dim pclsBal_histor As eLedge.Bal_histor
		pclsBal_histor = New eLedge.Bal_histor
		
		On Error GoTo insCalc_balance_err
		
		If Not pclsBal_histor.Find(nLed_compan, Trim(lstrAccount), Trim(lstrAux), Trim(lstrCost_cente), llngYearBal, llngMonthBal) Then
			insCalc_balance = 0
		Else
			
			'**+query the month balance
			'+ Se quiere consultar el saldo del mes
			
			If Not lblnAccumulate Then
				If pclsBal_histor.nDebit = 0 Then
					insCalc_balance = pclsBal_histor.nCredit - pclsBal_histor.nDebit
				Else
					insCalc_balance = pclsBal_histor.nDebit - pclsBal_histor.nCredit
				End If
				
				'**+query the acumulated balance
				'+ Se quiere consultar el acumulado
				
			Else
				insCalc_balance = pclsBal_histor.nBalance
			End If
		End If
		
		'UPGRADE_NOTE: Object pclsBal_histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsBal_histor = Nothing
insCalc_balance_err: 
		If Err.Number Then
			insCalc_balance = 0
		End If
		On Error GoTo 0
	End Function
	
	'**%insCalc_Reval_Year: apply the ajustement index by currency overissue
	'%insCalc_Reval_Year: Se aplican los indices de ajuste por inflación
	Private Function insCalc_Reval_Year(ByVal ldblAmmount As Double, ByVal llngYear As Integer, ByVal llngMonth As Integer, ByVal nLed_compan As Integer) As Double
		Dim ldblInd1 As Double
		Dim ldblInd2 As Double
		Dim ldblInd As Double
		Dim llngYear_aux As Integer
		Dim llngMonth_aux As Integer
		
		On Error GoTo insCalc_Reval_Year_err
		
		llngYear_aux = llngYear
		llngMonth_aux = llngMonth
		
		ldblInd1 = insReaReval_fact(llngYear_aux, llngMonth_aux, nLed_compan)
		
		llngYear_aux = llngYear_aux + 1
		ldblInd2 = insReaReval_fact(llngYear_aux, llngMonth_aux, nLed_compan)
		
		If ldblInd1 = 0 Then
			ldblInd = 0
		Else
			ldblInd = ldblInd2 / ldblInd1
		End If
		
		ldblAmmount = ldblAmmount * ldblInd
		
		If ldblAmmount <> 0 Then
			insCalc_Reval_Year = ldblAmmount
		Else
			insCalc_Reval_Year = 0
		End If
		
insCalc_Reval_Year_err: 
		If Err.Number Then
			insCalc_Reval_Year = 0
		End If
		On Error GoTo 0
	End Function
	
	'%insReaReval_fact: Se obtiene el índice por inflación
	Public Function insReaReval_fact(ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nLed_compan As Integer) As Double
		Dim lrecreaReval_FactMonth As eRemoteDB.Execute
		
		lrecreaReval_FactMonth = New eRemoteDB.Execute
		
		On Error GoTo insReaReval_fact_err
		
		'+Definición de parámetros para stored procedure 'insudb.reaReval_FactMonth'
		
		With lrecreaReval_FactMonth
			.StoredProcedure = "reaReval_FactMonth"
			.Parameters.Add("nEcon_area", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If CDbl(.FieldToClass("nIndexfac")) Then
					insReaReval_fact = CDbl(.FieldToClass("nIndexfac"))
				End If
				.RCloseRec()
			Else
				insReaReval_fact = 0
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaReval_FactMonth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaReval_FactMonth = Nothing
		
insReaReval_fact_err: 
		If Err.Number Then
			insReaReval_fact = 0
		End If
		On Error GoTo 0
	End Function
	
	'**insCalc_Reval_Month: the adjustement index are applied by overissue
	'%insCalc_Reval_Month: Se aplican los indices de ajuste por inflación
	Private Function insCalc_Reval_Month(ByVal ldblAmmount As Double, ByVal llngYear As Integer, ByVal llngMonth As Integer, ByVal nLed_compan As Integer) As Double
		Dim ldblInd1 As Double
		Dim ldblInd2 As Double
		Dim ldblInd As Double
		Dim llngYearAux As Integer
		Dim llngMonthAux As Integer
		
		On Error GoTo insCalc_Reval_Month_err
		
		llngYearAux = llngYear
		llngMonthAux = llngMonth
		
		ldblInd1 = insReaReval_fact(llngYearAux, llngMonthAux, nLed_compan)
		
		If llngMonthAux <> 12 Then
			llngMonthAux = llngMonthAux + 1
		Else
			llngMonthAux = 1
			llngYearAux = llngYearAux + 1
		End If
		
		ldblInd2 = insReaReval_fact(llngYearAux, llngMonthAux, nLed_compan)
		
		If ldblInd1 = 0 Then
			ldblInd = 0
		Else
			ldblInd = ldblInd2 / ldblInd1
		End If
		
		ldblAmmount = ldblAmmount * ldblInd
		
		If ldblAmmount <> 0 Then
			insCalc_Reval_Month = ldblAmmount
		Else
			insCalc_Reval_Month = 0
		End If
insCalc_Reval_Month_err: 
		If Err.Number Then
			insCalc_Reval_Month = 0
		End If
		On Error GoTo 0
	End Function
	
	'**%insCalc_Exchange_Year: Tha amount is adjusted according to the dollar value of the previous year
	'%insCalc_Exchange_Year: Se ajusta el importe según el valor del dolar del año anterior
	Private Function insCalc_Exchange_Year(ByVal ldblAmmount As Double, ByVal llngMonth As Integer, ByVal llngYear As Integer) As Double
		Dim ldblAmmount1 As Double
		Dim ldblAmmount2 As Double
		Dim ldblExchange As Double
        Dim lstrDate As String
		Dim lintDays As Integer
		Dim llngYearAux As Integer
		Dim mclsGeneral As eGeneral.Exchange
		
		mclsGeneral = New eGeneral.Exchange
		
		On Error GoTo insCalc_Exchange_Year_err
		
		lintDays = insCalc_days(llngMonth, llngYear)
        lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(llngMonth)) & "/" & Trim(Str(llngYear))
		
        ldblExchange = mclsGeneral.Find(2, CDate(lstrDate))
		
		If ldblExchange = 0 Then
			ldblAmmount1 = 0
		Else
			ldblAmmount1 = ldblAmmount / ldblExchange
		End If
		
		llngYearAux = llngYear + 1
		lintDays = insCalc_days(llngMonth, llngYearAux)
        lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(llngMonth)) & "/" & Trim(Str(llngYearAux))
		
        ldblExchange = mclsGeneral.Find(2, CDate(lstrDate))
		ldblAmmount2 = ldblAmmount1 * ldblExchange
		
		insCalc_Exchange_Year = ldblAmmount2
insCalc_Exchange_Year_err: 
		If Err.Number Then
			insCalc_Exchange_Year = 0
		End If
		On Error GoTo 0
	End Function
	
	'**%insCalc_days: return the month days
	'%insCalc_days: Retorna la cantidad de días del mes
	Private Function insCalc_days(ByVal llngMonthCalc As Integer, ByVal llngYearCalc As Integer) As Integer
		Select Case llngMonthCalc
			Case 1, 3, 5, 7, 8, 10, 12
				insCalc_days = 31
				
			Case 4, 6, 9, 11
				insCalc_days = 30
				
			Case 2
				If (llngYearCalc Mod 4) = 0 Then
					insCalc_days = 29
				Else
					insCalc_days = 28
				End If
		End Select
	End Function
	
	'**%insCAlc_Exchange_Month:The amount adjustement according to the dollar value of the previous month
	'%insCalc_Exchange_Month: Se ajusta el importe según el valor del dolar del mes anterior
	Private Function insCalc_Exchange_Month(ByVal llngYearExch As Integer, ByVal llngMonthExch As Integer, ByVal ldblAmmount As Double) As Double
		Dim ldblAmmount1 As Double
		Dim ldblAmmount2 As Double
		Dim ldblExchange As Double
        Dim lstrDate As String
		Dim lintDays As Integer
		Dim llngYearAux As Integer
		Dim llngMonthAux As Integer
		Dim mclsGeneral As eGeneral.Exchange
		
		mclsGeneral = New eGeneral.Exchange
		
		On Error GoTo insCalc_Exchange_Month_err
		
		llngYearAux = llngYearExch
		llngMonthAux = llngMonthExch
		
		lintDays = insCalc_days(llngMonthAux, llngYearAux)
        lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(llngMonthAux)) & "/" & Trim(Str(llngYearAux))
		
        ldblExchange = mclsGeneral.Find(2, CDate(lstrDate))
		
		If ldblExchange = 0 Then
			ldblAmmount1 = 0
		Else
			ldblAmmount1 = ldblAmmount / ldblExchange
		End If
		
		If llngMonthAux = 12 Then
			llngMonthAux = 1
			llngYearAux = llngYearAux + 1
		Else
			llngMonthAux = llngMonthAux + 1
		End If
		
		lintDays = insCalc_days(llngMonthAux, llngYearAux)
        lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(llngMonthAux)) & "/" & Trim(Str(llngYearAux))
        ldblExchange = mclsGeneral.Find(2, CDate(lstrDate))
		ldblAmmount2 = ldblAmmount1 * ldblExchange
		
		If ldblAmmount2 <> 0 Then
			insCalc_Exchange_Month = ldblAmmount2
		Else
			insCalc_Exchange_Month = 0
		End If
insCalc_Exchange_Month_err: 
		If Err.Number Then
			insCalc_Exchange_Month = 0
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostCP003: Validate all the data entered in the form
	'%insPostCP003: Esta función se encaga de validar todos los datos introducidos en la forma
	'UPGRADE_NOTE: NPer was upgraded to NPer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function insPostCP003(ByVal plngAction As Integer, ByVal NPer_Renamed As Integer, ByVal nYear As Integer, ByVal nDeb As Double, ByVal nCred As Double, ByVal nOrg_Deb As Double, ByVal nOrg_Cred As Double, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal valAux As String, ByVal sCost_cente As String, ByVal nLedger_Year As Integer, ByVal nUsercode As Integer, ByVal sSel As String) As Boolean
		insPostCP003 = True
		
		Select Case plngAction
			
			'**+If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				If insCalc_Bal_Account(plngAction, NPer_Renamed, nYear, nDeb, nCred, nOrg_Deb, nOrg_Cred, nLed_compan, sAccount, valAux, sCost_cente, nLedger_Year, nUsercode, sSel) Then
					insPostCP003 = True
				End If
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				If insCalc_Bal_Account(plngAction, NPer_Renamed, nYear, nDeb, nCred, nOrg_Deb, nOrg_Cred, nLed_compan, sAccount, valAux, sCost_cente, nLedger_Year, nUsercode, sSel) Then
					insPostCP003 = True
				End If
		End Select
	End Function
	'**%insCalc_Bal_Account: Calculate the account balance of the same or future level
	'**%to the one that is been added and with a previous or equal period inputed
	'**%that correspond to manual transactions.
	'%insCalc_Bal_Account: Calcula el saldo de las cuentas de nivel igual o superior
	'%a la que se está registrando y con período posterior o igual al ingresado pero
	'%que correspondan a movimientos manuales
	'UPGRADE_NOTE: NPer was upgraded to NPer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function insCalc_Bal_Account(ByVal plngAction As Integer, ByVal NPer_Renamed As Integer, ByVal nYear As Integer, ByVal nDeb As Double, ByVal nCred As Double, ByVal nOrg_Deb As Double, ByVal nOrg_Cred As Double, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal valAux As String, ByVal valUnit As String, ByVal nLedger_Year As Integer, ByVal nUsercode As Integer, ByVal sSel As String) As Boolean
		Dim lvntParameters(5) As Object
        'Dim llngIndex As Integer
        'Dim llngTop As Integer
        'Dim llngCount As Integer
		
		
		If (NPer_Renamed <> CDbl(String.Empty) And plngAction = eFunctions.Menues.TypeActions.clngActionadd) Or (Trim(sSel) = "1" And plngAction = eFunctions.Menues.TypeActions.clngActionUpdate) Then
			
			'**+Call the routine that create or update the period balance
			'+Se llama a la rutina que crea o actualiza los saldos del período
			
			If insCreUpd_Bal_HistorAccountLevels(plngAction, NPer_Renamed, nYear, nDeb, nCred, nOrg_Deb, nOrg_Cred, nLed_compan, sAccount, valAux, valUnit, nLedger_Year, nUsercode) Then
				insCalc_Bal_Account = True
			End If
			'**+Call the routine that update the balances of the prior periods
			'+Se llama a la rutina que actualiza los saldos de los períodos posteriores
			
			If insUpdBal_HistorAccountUP(plngAction, NPer_Renamed, nYear, nDeb, nCred, nOrg_Deb, nOrg_Cred, nLed_compan, sAccount, valAux, valUnit, nLedger_Year, nUsercode) Then
				insCalc_Bal_Account = True
			End If
		End If
		
	End Function
	
	'**%insCreUpd_Bal_HistorAccountLevels: Create a record of the table or actualized
	'**%the balance (for the accounts of superior level)
	'%insCreUpd_Bal_HistorAccountLevels: Esta rutina permite crear un registro de la tabla o actualizar
	'%el saldo (para las cuentas de niveles superiores inclusive)
	'UPGRADE_NOTE: NPer was upgraded to NPer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function insCreUpd_Bal_HistorAccountLevels(ByVal plngAction As Integer, ByVal NPer_Renamed As Integer, ByVal nYear As Integer, ByVal nDeb As Double, ByVal nCred As Double, ByVal nOrg_Deb As Double, ByVal nOrg_Cred As Double, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal valAux As String, ByVal valUnit As String, ByVal nLedger_Year As Integer, ByVal nUsercode As Integer) As Boolean
		Dim ldblDebit As Double
		Dim ldblCredit As Double
		Dim llngMonth As Integer
		Dim llngYear As Integer
		Dim pclsBal_histor As eLedge.Bal_histor
		Dim mclsLedger_acc As eLedge.LedgerAcc
		
		pclsBal_histor = New eLedge.Bal_histor
		mclsLedger_acc = New eLedge.LedgerAcc
		
		On Error GoTo insCreUpd_Bal_HistorAccountLevels_err
		
		llngMonth = NPer_Renamed
		llngYear = nYear
		
		If nDeb = eRemoteDB.Constants.intNull Then
			ldblDebit = 0
		Else
			ldblDebit = CDbl(Trim(CStr(nDeb)))
		End If
		
		If Trim(CStr(nCred)) = CStr(eRemoteDB.Constants.intNull) Then
			ldblCredit = 0
		Else
			ldblCredit = CDbl(Trim(CStr(nCred)))
		End If
		
		If plngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			ldblDebit = ldblDebit - nOrg_Deb
			ldblCredit = ldblCredit - nOrg_Cred
		End If
		
		'**+Makes the call to the SP that fix the data in the BD
		'+ Se realiza el llamado al SP que fija los datos en la BD
		With pclsBal_histor
			.nLed_compan = nLed_compan
			.sAccount = Trim(sAccount)
			
			If Trim(valAux) = String.Empty Then
				.sAux_accoun = " "
			Else
				.sAux_accoun = Trim(valAux)
			End If
			
			If Trim(valUnit) = String.Empty Then
				.sCost_cente = " "
			Else
				.sCost_cente = Trim(valUnit)
			End If
			
			.nLed_year = CInt(Trim(CStr(nLedger_Year)))
			.nMonth = llngMonth
			.nYear = llngYear
			.nDebit = ldblDebit
			.nCredit = ldblCredit
			.nUsercode = nUsercode
			If mclsLedger_acc.Find_Account(nLed_compan, sAccount) Then
				.UpdateAccountLevels(mclsLedger_acc.sType_acc)
			End If
		End With
		
		'UPGRADE_NOTE: Object pclsBal_histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsBal_histor = Nothing
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		
insCreUpd_Bal_HistorAccountLevels_err: 
		If Err.Number Then
			insCreUpd_Bal_HistorAccountLevels = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdateAccountLevels: This routine creates a record of the table or
	'**%update the balance (for the accounts future level)
	'% UpdateAccountLevels: Esta rutina permite crear un registro de la tabla o
	'% actualizar el saldo (para las cuentas de niveles superiores inclusive)
	Public Function UpdateAccountLevels(ByVal strType_acc As String) As Boolean
		
		'**-Define the variable lrecinsBal_historAccountLevels
		'- Se define la variable lrecinsBal_historAccountLevels
		Dim lrecinsBal_historAccountLevels As eRemoteDB.Execute
		
		lrecinsBal_historAccountLevels = New eRemoteDB.Execute
		
		On Error GoTo UpdateAccountLevels_err
		'**+Parameters definition for the stored procedure 'insudb.insBal_historAccountLevels'
		'**+Data read on 06/13/2001 01:45:36 PM
		'+ Definicion de parametros para stored procedure 'insudb.insBal_historAccountLevels'
		'+ Informacion leida el 13/06/2001 01:45:36 PM
		
		With lrecinsBal_historAccountLevels
			.StoredProcedure = "insBal_historAccountLevels"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_year", nLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", strType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateAccountLevels = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsBal_historAccountLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBal_historAccountLevels = Nothing
		
UpdateAccountLevels_err: 
		If Err.Number Then
			UpdateAccountLevels = False
		End If
	End Function
	
	'**%insUpdBal_HistorAccountUP: Calculate the balance of the accounts with equal or superior levels
	'**%to the one that is registering with the posterior ingreased period but
	'**%correspond to a manual movements.
	'%insUpdBal_HistorAccountUP: Calcula el saldo de las cuentas de nivel igual o superior
	'%a la que se está registrando y con período posterior al ingresado pero
	'%que correspondan a movimientos manuales
	'UPGRADE_NOTE: NPer was upgraded to NPer_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function insUpdBal_HistorAccountUP(ByVal plngAction As Integer, ByVal NPer_Renamed As Integer, ByVal nYear As Integer, ByVal nDeb As Double, ByVal nCred As Double, ByVal nOrg_Deb As Double, ByVal nOrg_Cred As Double, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal valAux As String, ByVal valUnit As String, ByVal nLedger_Year As Integer, ByVal nUsercode As Integer) As Boolean
		Dim ldblDebit As Double
		Dim ldblCredit As Double
		Dim ldblBalance As Double
		Dim llngMonth As Integer
		Dim llngYear As Integer
		Dim pclsBal_histor As eLedge.Bal_histor
		Dim mclsLedger_acc As eLedge.LedgerAcc
		
		pclsBal_histor = New eLedge.Bal_histor
		mclsLedger_acc = New eLedge.LedgerAcc
		
		On Error GoTo insUpdBal_HistorAccountUP_err
		
		llngMonth = NPer_Renamed
		llngYear = nYear
		
		If Trim(CStr(nDeb)) = String.Empty Then
			nDeb = 0
		End If
		
		If Trim(CStr(nCred)) = String.Empty Then
			nCred = 0
		End If
		
		If Trim(CStr(nOrg_Deb)) = String.Empty Then
			nOrg_Deb = 0
		End If
		
		If Trim(CStr(nOrg_Cred)) = String.Empty Then
			nOrg_Cred = 0
		End If
		
		ldblDebit = nDeb - nOrg_Deb
		ldblCredit = nCred - nOrg_Cred
		ldblBalance = ldblDebit - ldblCredit
		
		With pclsBal_histor
			.nLed_compan = nLed_compan
			.sAccount = Trim(sAccount)
			
			If Trim(valAux) = String.Empty Then
				.sAux_accoun = " "
			Else
				.sAux_accoun = Trim(valAux)
			End If
			
			If Trim(valUnit) = String.Empty Then
				.sCost_cente = " "
			Else
				.sCost_cente = Trim(valUnit)
			End If
			
			.nLed_year = CInt(Trim(CStr(nLedger_Year)))
			.nMonth = llngMonth
			.nYear = llngYear
			.nBalance = ldblBalance
			.nUsercode = nUsercode
			
			If mclsLedger_acc.Find_Account(nLed_compan, sAccount) Then
				.UpdateAccount(mclsLedger_acc.sType_acc)
			End If
			
		End With
		'UPGRADE_NOTE: Object pclsBal_histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsBal_histor = Nothing
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		
insUpdBal_HistorAccountUP_err: 
		If Err.Number Then
			insUpdBal_HistorAccountUP = False
		End If
	End Function
	
	'**% UpdateAccount: calculate the balance of the accounts of equal or higher
	'**% level to the one that is been added and with a prior increased period but
	'**%correspond to a manual transactions.
	'% UpdateAccount: Calcula el saldo de las cuentas de nivel igual o superior
	'%a la que se esta registrando y con periodo posterior al ingresado pero
	'%que correspondan a movimientos manuales
	Public Function UpdateAccount(ByVal strType_acc As String) As Boolean
		
		'**-Define the variable lrecinsBal_historUpdBalance
		'- Se define la variable lrecinsBal_historUpdBalance
		Dim lrecinsBal_historUpdBalance As eRemoteDB.Execute
		
		lrecinsBal_historUpdBalance = New eRemoteDB.Execute
		
		On Error GoTo UpdateAccount_err
		
		'**+Parameters definition for the stored procedure 'insudb.insBal_historUpdBalance'
		'**+Data read on 06/13/2001 02:17:14 PM
		'+ Definicion de parametros para stored procedure 'insudb.insBal_historUpdBalance'
		'+ Informacion leida el 13/06/2001 02:17:14 PM
		
		With lrecinsBal_historUpdBalance
			.StoredProcedure = "insBal_historUpdBalance"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_year", nLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", strType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateAccount = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsBal_historUpdBalance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBal_historUpdBalance = Nothing
UpdateAccount_err: 
		If Err.Number Then
			UpdateAccount = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValCPL003_K: This function perform validations over the fields of the CPL003
	'%insValCPL003_K: Esta función se encarga de validar los datos introducidos en la CPL003
	Public Function insValCPL003_K(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal nLevel As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nYearE As Integer, ByVal nMonthE As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLed_compan As eLedge.Led_compan
		Dim dEffecdate As Date
		Dim dEffecdateEnd As Date
		
		lclsErrors = New eFunctions.Errors
		lclsLed_compan = New eLedge.Led_compan
		
		On Error GoTo insValCPL003_K_Err
		
		'**+Validations related to column: nLed_compan
		'+ Se valida la columna: nLed_compan
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		'**+Validations related to column: nLevel
		'+ Se valida la columna: nLevel
		If nLevel = 0 Or nLevel = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70001)
		End If
		
		'**+Validations related to column: nYear
		'+ Se valida la columna: nYear
		If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'**+Validations related to column: nMonth
		'+ Se valida la columna: nMonth
		If nMonth <> 0 Or nMonth <> eRemoteDB.Constants.intNull Then
			dEffecdate = CDate("01/" & nMonth & "/" & nYear)
			If lclsLed_compan.Find(nLed_compan) Then
				If CDate(dEffecdate) > CDate(lclsLed_compan.dDate_end) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36037)
				Else
					If CDate(dEffecdate) < CDate(lclsLed_compan.dIniLedDat) Then
						Call lclsErrors.ErrorMessage(sCodispl, 736118)
					End If
				End If
			End If
		End If
		
		'**+Validations related to column: nYearE
		'+ Se valida la columna: nYearE
		If nYearE = 0 Or nYearE = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'**+Validations related to column: nMonthE
		'+ Se valida la columna: nMonthE
		If nMonthE <> 0 Or nMonthE <> eRemoteDB.Constants.intNull Then
			dEffecdateEnd = CDate("01/" & nMonthE & "/" & nYearE)
			If lclsLed_compan.Find(nLed_compan) Then
				If CDate(dEffecdateEnd) > CDate(lclsLed_compan.dDate_end) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36037)
				Else
					If CDate(dEffecdateEnd) < CDate(lclsLed_compan.dIniLedDat) Then
						Call lclsErrors.ErrorMessage(sCodispl, 736118)
					End If
				End If
			End If
		End If
		
		If (nYear <> 0 Or nYear <> eRemoteDB.Constants.intNull) And (nMonth <> 0 Or nMonth <> eRemoteDB.Constants.intNull) And (nYearE <> 0 Or nYearE <> eRemoteDB.Constants.intNull) And (nMonthE <> 0 Or nMonthE <> eRemoteDB.Constants.intNull) Then
			If CDate("01/" & nMonth & "/" & nYear) > CDate("01/" & nMonthE & "/" & nYearE) Then
				Call lclsErrors.ErrorMessage(sCodispl, 3108)
			End If
		End If
		
		insValCPL003_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLed_compan = Nothing
		
		
insValCPL003_K_Err: 
		If Err.Number Then
			insValCPL003_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
End Class






