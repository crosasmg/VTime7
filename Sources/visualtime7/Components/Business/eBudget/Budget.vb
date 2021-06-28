Option Strict Off
Option Explicit On
Public Class Budget
	'%-------------------------------------------------------%'
	'% $Workfile:: Budget.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:36p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'   Column_name                    Type       Computed   Length      Prec  Scale Nullable   TrimTrailingBlanks  FixedLenNullInSource
	Public nLed_compan As Integer 'smallint      no        2           5     0     no             (n/a)               (n/a)
	Public sBud_code As String 'char          no       12                       no             no                  no
	Public sDescript As String 'char          no       30                       yes            no                  yes
	Public nEnd_month As Integer 'int           no        4           10    0     yes            (n/a)               (n/a)
	Public nInit_month As Integer 'int           no        4           10    0     yes            (n/a)               (n/a)
	Public nNotenum As Integer 'int           no        4           10    0     yes            (n/a)               (n/a)
	Public sStatregt As String 'char          no        1                       yes            no                  yes
	Public nUsercode As Integer 'smallint      no        2           5     0     yes            (n/a)               (n/a)
	Public nYear As Integer 'smallint      no        2           5     0     no             (n/a)               (n/a)
	Public nCurrency As Integer 'smallint      no        2           5     0     no             (n/a)               (n/a)
	
	'- Variables auxiliares
	
	Private mclsLed_compan As eLedge.Led_compan
	
	'% Find: Permite buscar registros en la tabla de presupuestos
	Public Function Find(ByVal nLed_compan As Integer, ByVal sBud_code As String, ByVal nYear As Integer, ByVal nCurrency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		Dim lrecreaBudget As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaBudget = New eRemoteDB.Execute
		
		If nLed_compan <> Me.nLed_compan Or sBud_code <> Me.sBud_code Or nYear <> Me.nYear Or nCurrency <> Me.nCurrency Or lblnFind Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reaBudget'
			'+ Información leída el 17/12/1999 9:50:07
			
			With lrecreaBudget
				.StoredProcedure = "reaBudget"
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nLed_compan = .FieldToClass("nLed_compan")
					sBud_code = .FieldToClass("sBud_code")
					sDescript = .FieldToClass("sDescript")
					nEnd_month = .FieldToClass("nEnd_month")
					nInit_month = .FieldToClass("nInit_month")
					nNotenum = .FieldToClass("nNotenum")
					sStatregt = .FieldToClass("sStatregt")
					nYear = .FieldToClass("nYear")
					nCurrency = .FieldToClass("nCurrency")
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBudget = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	
	'% insValCPC003_K: Valida los datos introducidos para la Consulta Presupuestaria
	Public Function insValCPC003_K(ByVal sCodispl As String, ByVal sBud_code As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nCurrency As Integer, ByVal nLed_compan As Integer) As String
		
		'- Se definen las variables para conseguir los datos a mostrar en el campo "Mes"
		
		Static lstrMonth As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lstrNewMonth As String
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		Call mclsLed_compan.Find(nLed_compan)
		
		'+ Se realiza la validación del campo "Presupuesto"
		'+ Debe estar lleno
		
		If sBud_code = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 36062)
			lblnError = True
		Else
			If Not lclsValues.IsValid("TabBudget", sBud_code) Then
				
				'+ Debe estar registrado en el archivo de presupuestos
				
				Call lclsErrors.ErrorMessage(sCodispl, 36065)
				lblnError = True
			End If
		End If
		
		'+ Se realiza la validación del campo "Ejercicio"
		'+ Debe estar lleno
		
		If nYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36036)
			lblnError = True
		Else
			
			'+ Debe ser un año válido
			
			If nYear < 1900 Then
				Call lclsErrors.ErrorMessage("CP010", 1183)
				lblnError = True
			End If
		End If
		
		'+ Se realiza la validación del campo "Mes"
		'+ Debe estar lleno
		
		If nMonth = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36079)
			lblnError = True
		Else
			
			'+ Debe corresponder a un mes cerrado
			
			If insvalMonth(nMonth, nYear) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36037)
				lblnError = True
			End If
		End If
		
		'+ Se realiza la validación del campo "Moneda"
		'+ Debe estar lleno
		
		If nCurrency = 0 Then
			Call lclsErrors.ErrorMessage("CP008", 10827)
			lblnError = True
		End If
		
		If Not lblnError And Not (nYear = eRemoteDB.Constants.intNull) And Not (nCurrency = 0) And Not (sBud_code = String.Empty) Then
			
			If Me.Find(mclsLed_compan.nLed_compan, sBud_code, nYear, mclsLed_compan.nCurrency) Then
				lstrNewMonth = insMonthValues(nLed_compan, sBud_code, nYear)
				
				If lstrNewMonth <> lstrMonth Then
					lstrMonth = lstrNewMonth
					Call lclsErrors.ErrorMessage(sCodispl, 36079)
				End If
			End If
		End If
		
		'+ Se realiza la lectura de los datos según los datos del encabezado
		
		If Not lblnError Then
			'            If Not insreaBudgetQue Then
			Call lclsErrors.ErrorMessage("CP008", 1073)
			'            End If
		End If
		
insValCPC003_K_Err:
        If Err.Number Then
            insValCPC003_K = ""
            insValCPC003_K = insValCPC003_K & Err.Description
        End If
        On Error GoTo 0
	End Function
	
	'% insvalMonth: Verifica que el mes seleccionado corresponda a un mes cerrado
	Private Function insvalMonth(ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lintYear As Integer
		Dim lintMonth As Integer
		
		On Error GoTo insvalMonth_Err
		
		lintMonth = CInt(Mid(CStr(mclsLed_compan.dDate_init), 4, 2))
		lintYear = CInt(Mid(CStr(mclsLed_compan.dDate_init), 7, 4))
		
		If nYear > lintYear Or (nYear = lintYear And nMonth >= lintMonth) Then
			insvalMonth = True
		Else
			insvalMonth = False
		End If
		
insvalMonth_Err: 
		If Err.Number Then
			insvalMonth = False
		End If
		On Error GoTo 0
	End Function
	
	'% insMonthValues: Devuelve la secuencia de meses a mostrar en el campo Mes
	Public Function insMonthValues(ByVal nLed_compan As Integer, ByVal sBud_code As String, ByVal nYear As Integer) As String
		
		Dim lclsLed_compan As eLedge.Led_compan
        Dim lintMonth As Integer
        Dim varAux As String = ""

        '- Se define la variable para indicar el primer mes a mostrar en el campo Mes

        Dim lintInitMonth As Integer
		
		'- Se define la variable para indicar el último mes a mostrar en el campo Mes
		
		Dim lintEndMonth As Integer

        Try

            lclsLed_compan = New eLedge.Led_compan

            If lclsLed_compan.Find(nLed_compan) Then
                If Me.Find(lclsLed_compan.nLed_compan, sBud_code, nYear, lclsLed_compan.nCurrency) Then
                    lintInitMonth = CShort(Mid(CStr(Me.nInit_month), 5, 2))
                    lintEndMonth = CShort(Mid(CStr(Me.nEnd_month), 5, 2))

                    For lintMonth = lintInitMonth To lintEndMonth
                        varAux = varAux & CStr(lintMonth)
                        If lintMonth < lintEndMonth Then
                            varAux = varAux & ","
                        End If
                    Next lintMonth
                End If
            End If
            Return varAux
        Catch ex As Exception
            Return varAux = varAux & Err.Description
        Finally
            'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsLed_compan = Nothing
        End Try
    End Function
	'%insCalc_Diference: Se calcula el presupuesto mensual dado el total anual
	Public Function insCalc_Month(ByVal nLed_compan As Integer, ByVal sBud_code As String, ByVal nYear As Integer) As Object
        Dim lstrReturnValue As Object = New Object

        Dim lclsLed_compan As eLedge.Led_compan
        'Dim lintMonth As Integer
		
		'- Se define la variable para indicar el primer mes a mostrar en el campo Mes
		
		Dim lintInitMonth As Integer
		Dim lintTop As Integer
		
		'- Se define la variable para indicar el último mes a mostrar en el campo Mes
		
		Dim lintEndMonth As Integer
		
		
		lclsLed_compan = New eLedge.Led_compan
		
		If lclsLed_compan.Find(nLed_compan) Then
			If Me.Find(lclsLed_compan.nLed_compan, sBud_code, nYear, lclsLed_compan.nCurrency) Then
				lintInitMonth = CShort(Mid(CStr(Me.nInit_month), 5, 2))
				lintEndMonth = CShort(Mid(CStr(Me.nEnd_month), 5, 2))
				'+ Se calcula el número de registros que contendrá el arreglo
				lintTop = lintEndMonth - lintInitMonth + 1
				lstrReturnValue = lintTop
			End If
		End If
		
		If lstrReturnValue < 0 Then
			lstrReturnValue = 0
		End If
		
		insCalc_Month = lstrReturnValue
	End Function
	
	''% insreaBudgetQue: realiza la lectura según los datos del encabezado
	''--------------------------------------------------------------------
	'Public Function insreaBudgetQue() As Boolean
	''--------------------------------------------------------------------
	
	''- Se declara la variable para definir la Fecha inicial
	
	'    Dim ldtmInitDate       As Date
	'
	''- Se declara la variable para definir la Fecha final
	
	'    Dim ldtmEndDate        As Date
	'
	'    Set mcolBudgetQue = New Budget_amos
	'
	''+ Se calcula la fecha inicial
	'
	'    If optMonth.Value Then
	
	''+ Si la opción Saldo es Mensual
	
	'        If CStr(cbeMonth.Value) < 10 Then
	'            ldtmInitDate = CDate("01" & "/0" & CStr(cbeMonth.Value) & "/" & CStr(mclsBudget.nYear))
	'        Else
	'            ldtmInitDate = CDate("01" & "/" & CStr(cbeMonth.Value) & "/" & CStr(mclsBudget.nYear))
	'        End If
	'    Else
	
	''+ Si la opción Saldo es Acumulado
	
	'        If Mid(mclsBudget.nInit_month, 5, 1) < 10 Then
	'            ldtmInitDate = CDate("01" & "/0" & CStr(Mid(mclsBudget.nInit_month, 5, 1)) & "/" & CStr(mclsBudget.nYear))
	'        Else
	'            ldtmInitDate = CDate("01" & "/" & CStr(Mid(mclsBudget.nInit_month, 5, 1)) & "/" & CStr(mclsBudget.nYear))
	'        End If
	'    End If
	'
	''+ Se calcula la fecha final
	'
	'    If cbeMonth.Value < 9 Then
	'        ldtmEndDate = CDate("01" & "/0" & CStr(cbeMonth.Value + 1) & "/" & CStr(mclsBudget.nYear))
	'    Else
	'        ldtmEndDate = CDate("01" & "/" & CStr(cbeMonth.Value) & "/" & CStr(mclsBudget.nYear))
	'    End If
	'
	''+ Se leen los datos a mostrar en la zona de detalle
	
	'    If mcolBudgetQue.FindBudgetQue(mclsLedCompan.nLed_compan, _
	''                                   mclsBudget.nCurrency, _
	''                                   mclsBudget.sBud_code, _
	''                                   mclsBudget.nYear, _
	''                                   cbeMonth.Value, _
	''                                   ldtmInitDate, _
	''                                   ldtmEndDate, _
	''                                   optMonth.Value) Then
	'        Call insLoadTreeView
	'        insreaBudgetQue = True
	'    Else
	'        insreaBudgetQue = False
	'    End If
	'End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mclsLed_compan = New eLedge.Led_compan
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%insValCP010_k: Rutina de validación del encabezado de la ventana.
	Public Function insValCP010_k(ByVal nLedCompan As Integer, ByVal Action As Integer, ByVal sCodispl As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsBudget As eBudget.Budget
		Dim mclsLedCompan As eLedge.Led_compan
		Dim lblnExist As Boolean
		
		Dim mblnValueComp As Boolean
		Dim mblnAuxExist As Boolean
		
		On Error GoTo insValCP010_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsBudget = New eBudget.Budget
		mclsLedCompan = New eLedge.Led_compan
		
		If mclsLedCompan.Find(nLedCompan) Then
		End If
		
		'+ Debe estar lleno
		If Action = eFunctions.Menues.TypeActions.clngActionadd Then
			If sBud_code = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36062)
			End If
		End If
		
		'+ Validación del campo Ejercicio
		'+ Debe estar lleno
		If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36036)
		Else
			If nYear < 1900 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1183)
			End If
		End If
		
		'+ Validación del campo Moneda
		If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		End If
		
		'+ Se verifica la existencia de los datos en la tabla de presupuestos
		If Action = eFunctions.Menues.TypeActions.clngActionadd Then
			lblnExist = mclsBudget.Find(mclsLedCompan.nLed_compan, sBud_code, nYear, nCurrency)
			
			'+ Si la opción es registrar, la combinación Código-Ejercicio-Moneda no debe estar registrada
			If lblnExist Then
				Call lclsErrors.ErrorMessage(sCodispl, 36064)
			End If
		Else
			lblnExist = mclsBudget.Find(mclsLedCompan.nLed_compan, sBud_code, nYear, nCurrency)
			
			
			If lblnExist Then
				If Action = eFunctions.Menues.TypeActions.clngActionDuplicate Then
					'+ Si la acción es Duplicar, el presupuesto no debe estar duplicado
					If insvalDupBudget(mclsLedCompan.nLed_compan, sBud_code) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36230)
					End If
				End If
			Else
				'+ Si la opción no es registrar, la combinación Código-Ejercicio-Moneda debe estar registrada
				Call lclsErrors.ErrorMessage(sCodispl, 36065)
			End If
			
		End If
		
		
		insValCP010_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object mclsBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsBudget = Nothing
		'UPGRADE_NOTE: Object mclsLedCompan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedCompan = Nothing
		
insValCP010_k_Err: 
		If Err.Number Then
			insValCP010_k = insValCP010_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'% insvalDupBudget: verifica que el presupuesto no tenga duplicado
	Private Function insvalDupBudget(ByVal nLed_compan As Integer, ByVal sBud_code As String) As Boolean
		Dim lclsBudget As eBudget.Budget
		'- Se declaran las variables locales para la verificar si el presupuesto es un duplicado
		Dim lstrYear As String
		Dim lstrBud_code As String
		Dim lintLenBudget As Integer
		
		lclsBudget = New eBudget.Budget
		
		lintLenBudget = Len(sBud_code)
		
		
		If lintLenBudget > 4 Then
			lstrYear = Mid(sBud_code, lintLenBudget - 3, 4)
			If IsNumeric(lstrYear) Then
				lstrBud_code = Mid(sBud_code, 1, lintLenBudget - 4)
			Else
				insvalDupBudget = False
				Exit Function
			End If
		Else
			insvalDupBudget = False
			Exit Function
		End If
		
		insvalDupBudget = lclsBudget.Find(nLed_compan, lstrBud_code, CShort(lstrYear), 0)
	End Function
	
	
	'%insValCP010: Rutina de validación del encabezado de la ventana.
	Public Function insValCP010(ByVal nLedCompan As Integer, ByVal Action As Integer, ByVal sCodispl As String, ByVal sDescript As String, ByVal nInit_month As Integer, ByVal nEnd_month As Integer, ByVal optCurrency As String, ByVal cbeOtherCurrency As Integer, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim lclsBudget As eBudget.Budget
		Dim mclsLedCompan As eLedge.Led_compan
		Dim lblnExist As Boolean
		
		Dim mblnValueComp As Boolean
		Dim mblnAuxExist As Boolean
		
		On Error GoTo insValCP010_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		lclsBudget = New eBudget.Budget
		mclsLedCompan = New eLedge.Led_compan
		
		If mclsLedCompan.Find(nLedCompan) Then
		End If
		If lclsBudget.Find(nLed_compan, sBud_code, nYear, nCurrency) Then
		End If
		'+ El campo Descripción debe estar lleno
		If sDescript = "" Then
			Call lclsErrors.ErrorMessage(sCodispl, 36063)
		End If
		
		'+ El campo Mes inicial debe estar lleno
		If nInit_month = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36066)
		End If
		
		'+ El campo Mes final debe estar lleno
		If nEnd_month = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36067)
		End If
		
		'+ Validación del campo Mes Final
		'+ Debe ser posterior o igual al mes inicial
		If nEnd_month < nInit_month Then
			Call lclsErrors.ErrorMessage(sCodispl, 36068)
		End If
		
		'+ Validación del campo Moneda
		If optCurrency = "1" Or Action = eFunctions.Menues.TypeActions.clngActionDuplicate Then
			'+ No debe existir un presupuesto duplicado (del mismo presupuesto) con la misma moneda
			If lclsBudget.Find(mclsLedCompan.nLed_compan, lclsBudget.sBud_code & CStr(lclsBudget.nYear), lclsBudget.nYear, IIf(cbeOtherCurrency = CDbl(""), mclsLedCompan.nCurrency, cbeOtherCurrency)) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36228)
			Else
				'+ La moneda del nuevo presupuesto debe ser diferente a la moneda del presupuesto original
				If lclsBudget.nCurrency = cbeOtherCurrency Then
					Call lclsErrors.ErrorMessage(sCodispl, 36226)
				End If
			End If
		End If
		
		insValCP010 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBudget = Nothing
		'UPGRADE_NOTE: Object mclsLedCompan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedCompan = Nothing
		
insValCP010_Err: 
		If Err.Number Then
			insValCP010 = insValCP010 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'% insPostFolder: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostFolder(ByVal Action As Integer, ByVal nLed_compan As Integer, ByVal sBud_code As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal sDescript As String, ByVal nInit_month As Integer, ByVal nEnd_month As Integer, ByVal nNotenum As Integer, ByVal cbeStatregt As String, ByVal optCurrency As String, ByVal cbeOtherCurrency As Integer) As Boolean
		Dim mclsBudget As eBudget.Budget
		Dim mclsLedCompan As eLedge.Led_compan
		mclsBudget = New eBudget.Budget
		mclsLedCompan = New eLedge.Led_compan
		
		insPostFolder = True
		
		If mclsLedCompan.Find(nLed_compan) Then
		End If
		
		If mclsBudget.Find(nLed_compan, sBud_code, nYear, nCurrency) Then
		End If
		'+ Se asignan los valores de los campos a las variables públicas de la clase
		With mclsBudget
			.nLed_compan = nLed_compan
			.sDescript = sDescript
			.nInit_month = CInt(CStr(mclsBudget.nYear) & CStr(nInit_month))
			.nEnd_month = CInt(CStr(mclsBudget.nYear) & CStr(nEnd_month))
			.nNotenum = nNotenum
			.sStatregt = IIf(cbeStatregt = "", "2", cbeStatregt)
			If Action = eFunctions.Menues.TypeActions.clngActionDuplicate Then
				If optCurrency = "1" Then
					.nCurrency = IIf(cbeOtherCurrency = CDbl(""), mclsLedCompan.nCurrency, cbeOtherCurrency)
				End If
			Else
				.nCurrency = nCurrency
			End If
			
			Select Case Action
				
				'+ Se agrega el registro a la tabla de presupuestos
				Case eFunctions.Menues.TypeActions.clngActionadd
					insPostFolder = mclsBudget.Add
					
					'+ Se actualiza el registro a la tabla de presupuestos
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					insPostFolder = mclsBudget.Update
					
					'+ Se crea un duplicado de los datos del registro en la tabla de presupuestos
				Case eFunctions.Menues.TypeActions.clngActionDuplicate
					mclsBudget.sBud_code = sBud_code & CStr(nYear)
					insPostFolder = mclsBudget.Add
					
					'+ Se elimina el registro a la tabla de presupuestos
				Case eFunctions.Menues.TypeActions.clngActioncut
					insPostFolder = mclsBudget.Delete
			End Select
			
		End With
		
	End Function
	
	
	'% Add: Permite añadir registros en la tabla de presupuestos
	Public Function Add() As Boolean
		Dim lreccreBudget As eRemoteDB.Execute
		
		lreccreBudget = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'creBudget'
		'+ Información leída el 14/07/2001 02:03:11
		
		With lreccreBudget
			.StoredProcedure = "creBudget"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_month", nEnd_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_month", nInit_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			Else
				Add = False
			End If
			
		End With
		'UPGRADE_NOTE: Object lreccreBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreBudget = Nothing
	End Function
	
	'% Update: Permite modificar registros en la tabla de presupuestos
	Public Function Update() As Boolean
		Dim lrecupdBudget As eRemoteDB.Execute
		
		lrecupdBudget = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updBudget'
		'+ Información leída el 14/07/2001 9:52:32
		
		With lrecupdBudget
			.StoredProcedure = "updBudget"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_month", nEnd_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_month", nInit_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			Else
				Update = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecupdBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBudget = Nothing
	End Function
	
	'% Eliminar: Permite eliminar registros en la tabla de presupuestos
	Public Function Delete() As Boolean
		Dim lrecdelBudget As eRemoteDB.Execute
		
		lrecdelBudget = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delBudget'
		'+ Información leída el 17/12/1999 9:53:15
		
		With lrecdelBudget
			.StoredProcedure = "delBudget"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			Else
				Delete = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelBudget = Nothing
	End Function
End Class






