Option Strict Off
Option Explicit On
Option Compare Text
Public Class Tab_ActiveLife
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_ActiveLife.cls                       $%'
	'% $Author:: Gazuaje                                    $%'
	'% $Date:: 3/07/06 7:54p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla insudb.tab_Activelife al 11-16-2001 12:36:13
	'-     Property                    Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nCapmin As Double ' NUMBER     22   0     12   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nMchainves As Integer ' NUMBER     22   0     5    N
	Public nErrrange As Double ' NUMBER     22   2     10   N
	Public nOption As Integer ' NUMBER     22   2     5    N
	Public nPercent As Double ' NUMBER     22   6     9    N
	Public nMin_prembas As Double ' NUMBER     22   6     9    N

    Public nMax_prembas As Double ' NUMBER     22   6     9    N
    Public nMin_premmin As Double ' NUMBER     22   6     9    N
    Public nMax_premmin As Double ' NUMBER     22   6     9    N
    Public nMin_premexc As Double ' NUMBER     22   6     9    N
    Public nMax_premexc As Double ' NUMBER     22   6     9    N

    Public nMin_premPac As Double
    Public nMax_premPac As Double

    '- Variable auxiliar
	Public nExists As Integer ' Existe registro asociado a tab_modul
	Public sModulecDesc As String ' Descripcion del módulo
	Private mdtmEffecdate As Date
	
	'%InsPostDP607C: Ejecuta el post de la transacción DP607C
	Public Function InsPostDP607C(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nQMonthIni As Integer, ByVal dEffecdate As Date, ByVal nQMonthEnd As Integer, ByVal nPercent As Double, ByVal nUsercode As Integer, ByVal nPerTotSurr As Double, ByVal nPerParSurr As Double, ByVal nChargTSurr As Double, ByVal nChargPSurr As Double, ByVal nQFree_Surr As Integer) As Boolean
		Dim lclsLoad_Surr As Load_surr
		
		On Error GoTo InsPostDP607C_Err
		
		lclsLoad_Surr = New Load_surr
		
		With lclsLoad_Surr
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nQMonthIni = nQMonthIni
			.dEffecdate = dEffecdate
			.nQMonthEnd = nQMonthEnd
			.nPercent = nPercent
			.nUsercode = nUsercode
			.nPerTotSurr = nPerTotSurr
			.nPerParSurr = nPerParSurr
			.nChargTSurr = nChargTSurr
			.nChargPSurr = nChargPSurr
			.nQFree_Surr = nQFree_Surr
			
			Select Case sAction
				Case "Add"
					InsPostDP607C = .Add
				Case "Update"
					InsPostDP607C = .Update
				Case "Del"
					InsPostDP607C = .Delete
			End Select
			
		End With
		
InsPostDP607C_Err: 
		If Err.Number Then
			InsPostDP607C = False
		End If
		'UPGRADE_NOTE: Object lclsLoad_Surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLoad_Surr = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostDP607D: Ejecuta el post de la transacción DP607D
	Public Function InsPostDP607D(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nTypeInvest As Integer, ByVal nIntwarr As Double, ByVal nIntWarrMin As Double, ByVal nIntWarrClear As Double) As Boolean
		Dim lclsPlan_IntWarr As Plan_IntWar
		
		On Error GoTo InsPostDP607D_Err
		
		lclsPlan_IntWarr = New Plan_IntWar
		
		With lclsPlan_IntWarr
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.dEffecdate = dEffecdate
			.nTypeInvest = nTypeInvest
			.nIntwarr = nIntwarr
			.nIntWarrMin = nIntWarrMin
			.nIntWarrClear = nIntWarrClear
			
			Select Case sAction
				Case "Add"
					InsPostDP607D = .Add
				Case "Update"
					InsPostDP607D = .Update
				Case "Del"
					InsPostDP607D = .Delete
			End Select
			
		End With
		
InsPostDP607D_Err: 
		If Err.Number Then
			InsPostDP607D = False
		End If
		'UPGRADE_NOTE: Object lclsPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPlan_IntWarr = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsValDP607C: Validaciones de la transacción(Folder)
	Public Function InsValDP607C(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nQMonthIni As Integer, ByVal dEffecdate As Date, ByVal nQMonthEnd As Integer, ByVal nPercent As Double, ByVal nPerTotSurr As Double, ByVal nPerParSurr As Double, ByVal nChargTSurr As Double, ByVal nChargPSurr As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLoad_Surr As Load_surr
		
		On Error GoTo InsValDP607C_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Si se está agregando un rango
			If sAction = "Add" Then
				'+ Se valida que el nuevo rango no contenga a los existentes
				lclsLoad_Surr = New Load_surr
				If Not lclsLoad_Surr.InsValRange(nBranch, nProduct, IIf(nModulec < 0, 0, nModulec), nQMonthIni, dEffecdate, nQMonthEnd) Then
					.ErrorMessage(sCodispl, 55714)
				End If
				'UPGRADE_NOTE: Object lclsLoad_Surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsLoad_Surr = Nothing
			End If
			
			'+ Para eliminar ya se validaron los campos de la llave
			'+ Si se actualiza o agrega se requiere validar final de rango
			If sAction <> "Del" Then
				If nQMonthIni >= nQMonthEnd And nQMonthEnd <> 0 Then
					.ErrorMessage(sCodispl, 55713)
				End If
				
				'If nPercent = NumNull Then
				'    .ErrorMessage sCodispl, 60142
				'End If
				
				If nPercent = eRemoteDB.Constants.intNull And nPerTotSurr = eRemoteDB.Constants.intNull And nPerParSurr = eRemoteDB.Constants.intNull And nChargTSurr = eRemoteDB.Constants.intNull And nChargPSurr = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 80091)
				End If
				
			End If
			
			InsValDP607C = .Confirm
		End With
		
InsValDP607C_Err: 
		If Err.Number Then
			InsValDP607C = "InsValDP607C: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValDP607D: Validaciones de la transacción(Folder)
	Public Function InsValDP607D(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nTypeInvest As Integer, ByVal nIntwarr As Double, ByVal nIntWarrMin As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPlan_IntWarr As Plan_IntWar
		
		On Error GoTo InsValDP607D_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'If nModulec = NumNull Then
			'.ErrorMessage sCodispl, 60135
			'End If
			
			If nTypeInvest <= 0 Then
				.ErrorMessage(sCodispl, 60173)
			End If
			
			If nIntwarr <= 0 Then
				.ErrorMessage(sCodispl, 60162)
			End If
			
			If nIntWarrMin <= 0 Then
				.ErrorMessage(sCodispl, 60163)
			End If
			
			'+ Si se está agregando un rango
			If sAction = "Add" Then
				lclsPlan_IntWarr = New Plan_IntWar
				'+ Se valida que no exista el registro en la tabla
				If lclsPlan_IntWarr.insvalExists(nBranch, nProduct, nModulec, dEffecdate, nTypeInvest) Then
					.ErrorMessage(sCodispl, 56019)
				End If
				
				'UPGRADE_NOTE: Object lclsPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPlan_IntWarr = Nothing
			End If
			
			InsValDP607D = .Confirm
		End With
		
InsValDP607D_Err: 
		If Err.Number Then
			InsValDP607D = "InsValDP607D: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsUpdTab_ActiveLife: Se encarga de actualizar la tabla Tab_ActiveLife
	Private Function InsUpdTab_ActiveLife(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdtab_activelife As eRemoteDB.Execute
		
		On Error GoTo insUpdtab_activelife_Err
		
		lrecinsUpdtab_activelife = New eRemoteDB.Execute
		
		With lrecinsUpdtab_activelife
			.StoredProcedure = "insUpdtab_activelife"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmin", nCapmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMchainves", nMchainves, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrrange", nErrrange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_prembas", nMin_prembas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMax_prembas", nMax_prembas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMin_premmin", nMin_premmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMax_premmin", nMax_premmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMin_premexc", nMin_premexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMax_premexc", nMax_premexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nMin_prempac", nMin_premPac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMax_prempac", nMax_premPac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


			InsUpdTab_ActiveLife = .Run(False)
			
		End With
insUpdtab_activelife_Err: 
		If Err.Number Then
			InsUpdTab_ActiveLife = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdtab_activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdtab_activelife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostDP607A: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(DP607A)
    Public Function InsPostDP607A(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nCapmin As Double, ByVal nUsercode As Integer, ByVal nMchainves As Integer, ByVal nErrrange As Double, ByVal nOption As Integer, ByVal nPercent As Double, ByVal nMin_prembas As Double, ByVal nMax_prembas As Double, ByVal nMin_premmin As Double, ByVal nMax_premmin As Double, ByVal nMin_premexc As Double, ByVal nMax_premexc As Double, ByVal nMin_premPac As Double, ByVal nMax_premPac As Double) As Boolean
        Dim lclsTab_ActiveLife As Tab_ActiveLife
        Dim lclsProd_win As eProduct.Prod_win

        On Error GoTo InsPostDP607A_Err

        lclsTab_ActiveLife = New Tab_ActiveLife
        lclsProd_win = New eProduct.Prod_win

        With lclsTab_ActiveLife
            .nBranch = nBranch
            .nProduct = nProduct
            .nModulec = nModulec
            .dEffecdate = dEffecdate
            .nCurrency = nCurrency
            .nCapmin = nCapmin
            .nUsercode = nUsercode
            .nMchainves = nMchainves
            .nErrrange = nErrrange
            .nOption = nOption
            .nPercent = nPercent
            .nMin_prembas = nMin_prembas
            .nMax_prembas = nMax_prembas
            .nMin_premmin = nMin_premmin
            .nMax_premmin = nMax_premmin
            .nMin_premexc = nMin_premexc
            .nMax_premexc = nMax_premexc
            .nMin_premPac = nMin_premPac
            .nMax_premPac = nMax_premPac

            Select Case sAction
                Case "Add"
                    InsPostDP607A = .Add
                Case "Update"
                    InsPostDP607A = .Update
                Case "Del"
                    InsPostDP607A = .Delete
            End Select
        End With

        If InsPostDP607A Then
            If lclsTab_ActiveLife.FindDP607A(nBranch, nProduct, dEffecdate) Then
                '+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
                Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP607A", "2", nUsercode)
            Else
                Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP607A", "1", nUsercode)
            End If
        End If

InsPostDP607A_Err:
        If Err.Number Then
            InsPostDP607A = False
        End If
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsTab_ActiveLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_ActiveLife = Nothing
        On Error GoTo 0
    End Function

    '%InsPostDP607B: Ejecuta el post de la transacción Tabla de control de prima mínima(DP607B)
    Public Function InsPostDP607B(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeLoad As Integer, ByVal nInitMonth As Integer, ByVal nEndMonth As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nMonths As Integer) As Boolean
        Dim lclsPlan_Loads As Plan_Loads

        On Error GoTo InsPostDP607B_Err

        InsPostDP607B = False

        lclsPlan_Loads = New Plan_Loads
        With lclsPlan_Loads
            .nBranch = nBranch
            .nProduct = nProduct
            .nModulec = nModulec
            .nTypeLoad = nTypeLoad
            .nInitMonth = nInitMonth
            .nEndMonth = nEndMonth
            .nCapStart = nCapStart
            .nCapEnd = nCapEnd
            .dEffecdate = dEffecdate
            .nPercent = nPercent
            .nAmount = nAmount
            .nCurrency = nCurrency
            .nUsercode = nUsercode
            .nMonths = nMonths
            Select Case sAction
                Case "Add"
                    InsPostDP607B = .Add
                Case "Update"
                    InsPostDP607B = .Update
                Case "Del"
                    InsPostDP607B = .Delete
            End Select
        End With

        'UPGRADE_NOTE: Object lclsPlan_Loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPlan_Loads = Nothing

InsPostDP607B_Err:
        If Err.Number Then
            InsPostDP607B = False
        End If
        On Error GoTo 0
    End Function

    '%InsValDP607B: Validaciones de la transacción(Folder). Tabla de control de prima mínima(DP607B)
    Public Function InsValDP607B(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeLoad As Integer, ByVal nInitMonth As Integer, ByVal nEndMonth As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nMonths As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPlan_Loads As Plan_Loads

        On Error GoTo InsValDP607B_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors

            '+ Tipo de cargo debe estar lleno
            If nTypeLoad = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 55863)
            End If

            '+ Módulo debe estar lleno
            If nModulec = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 60135)
            End If

            '+ Número inicial debe ser mayor a cero
            If nInitMonth < 0 Then
                .ErrorMessage(sCodispl, 56015)
            End If

            '+ Número final debe ser mayor a cero
            If nEndMonth < 0 Then
                .ErrorMessage(sCodispl, 56016)
            End If

            '+ Capital inicial debe ser mayor a cero
            If nCapStart < 0 Then
                .ErrorMessage(sCodispl, 11111)
            End If

            '+ Capital final debe ser mayor a cero
            If nCapStart < 0 Then
                .ErrorMessage(sCodispl, 11112)
            End If

            '+ Capital final debe ser mayor a el capital inicial
            If nCapStart > 0 And nCapEnd > 0 Then
                If nCapEnd < nCapStart Then
                    .ErrorMessage(sCodispl, 11113)
                End If
            End If

            If sAction <> "Del" Then
                '+ Número inicial debe ser menor a Fin
                If nInitMonth > nEndMonth Then
                    .ErrorMessage(sCodispl, 56017)
                End If

                '+ Debe ingresar Monto o Tasa
                If nAmount = eRemoteDB.Constants.intNull And nPercent = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 55878)
                End If
            End If

            '+ Si se está agregando un rango
            If sAction = "Add" Then
                '+ Validar que rango a ingresar no exista
                lclsPlan_Loads = New Plan_Loads
                If Not lclsPlan_Loads.InsValRange(nBranch, nProduct, nModulec, nTypeLoad, nInitMonth, nEndMonth, nCapStart, nCapEnd, dEffecdate) Then
                    .ErrorMessage(sCodispl, 56018)
                End If
                'UPGRADE_NOTE: Object lclsPlan_Loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsPlan_Loads = Nothing
            End If

            InsValDP607B = .Confirm

        End With

InsValDP607B_Err:
        If Err.Number Then
            InsValDP607B = "InsValDP607B: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '% InsValDP607AMsg: Validaciones masivas de la Tabla de Condiciones Grales de planes de
    '%                  VidActiva(DP607A)
    Public Function InsValDP607AMsg(ByVal sCodispl As String, ByVal nCount As Integer, ByVal sOption As String) As String
        Dim lintOption As Integer
        Dim bLast As Boolean
        Dim lintIndex As Integer
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValDP607AMsg_Err

        lclsErrors = New eFunctions.Errors

        bLast = False
        lintIndex = 1

        '+ Se realizan las validaciones sobre el grid
        Do While Len(nCount) > 0 And Not bLast
            '+ Se descomponen los datos, cada uno viene en una cadena de valores separados por coma.
            If InStr(1, sOption, ",") > 0 Then
                lintOption = CInt(Mid(sOption, 1, InStr(1, sOption, ",") - 1))
                sOption = Trim(Mid(sOption, InStr(1, sOption, ",") + 1))
            Else
                '+ Se trata al último elemento de la cadena o la cadena traía sólo un elemento
                lintOption = CInt(sOption)
                bLast = True
            End If
            If lintOption = eRemoteDB.Constants.intNull Or lintOption = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 56006, lintIndex)
            End If
            lintIndex = lintIndex + 1
        Loop

        InsValDP607AMsg = lclsErrors.Confirm

InsValDP607AMsg_Err:
        If Err.Number Then
            InsValDP607AMsg = "InsValDP607AMsg: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function


    '%InsValDP607A: Validaciones de la transacción (PopUp)
    '%              Tabla de Condiciones Grales de planes de VidActiva(DP607A)
    Public Function InsValDP607A(ByVal sCodispl As String, ByVal sAction As String, ByVal nCapmin As Double, ByVal nMchainves As Integer, ByVal nErrrange As Double, ByVal nOption As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValDP607A_Err
        lclsErrors = New eFunctions.Errors

        With lclsErrors

            '+ Capital mínimo
            If nCapmin = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 60158)
            End If

            '+ Máximo de cambios de modalidad de inversión
            If nMchainves = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 60159)
            End If

            '+ Margen de error para prima proyectada
            If nErrrange = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 60160)
            End If

            '+ Opción de indemnización
            If nOption = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 56006)
            End If

            If sAction = "Add" Then
                If Me.Find(nBranch, nProduct, nModulec, nOption, dEffecdate, True) Then
                    Call .ErrorMessage(sCodispl, 10284)
                End If
            End If

            InsValDP607A = .Confirm
        End With

InsValDP607A_Err:
        If Err.Number Then
            InsValDP607A = "InsValDP607A: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function


    '% LoadTabsActLifeCover: Esta función es la encargada de cargar la información necesaria
    '%                      para cada pestaña que será mostrada para coberturas de vida activa
    Public Function LoadTabs(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String) As String
        '- Constante para el número posible de frames en la subsecuencia de características de vida.
        Const CN_FRAMESNUMPRODACTLIFESEQ As Integer = 4

        '- Se define la constante para los codispl en la subsecuencia de cobertura (Cob. VIDA)
        Const CN_WINDOWSPRODACTLIFESEQ As String = "DP607A  DP607B  DP607C  DP607D  "

        Dim lintPageImage As eFunctions.Sequence.etypeImageSequence

        Dim lclsQuery As eRemoteDB.Query
        Dim lclsSequence As eFunctions.Sequence
        Dim lcolTab_ActiveLifes As Tab_ActiveLifes
        Dim lclsTab_ActiveLife As Tab_ActiveLife
        Dim lcolPlan_Loads As Plan_Loadss
        Dim lcolLoad_surrs As Load_surrs
        Dim lcolPlan_IntWarr As Plan_IntWarrs

        Dim llngCount As Integer
        Dim llngAux As Integer
        Dim lvntRequireField As Object = New Object
        Dim lstrHTMLCode As String
        Dim lintAction As Integer
        Dim lblnValid As Boolean
        Dim lstrCodispl As String

        On Error GoTo LoadTabs_Err

        lclsQuery = New eRemoteDB.Query
        lclsSequence = New eFunctions.Sequence

        '-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
        '-extraído de la constante cstrWindows


        lstrHTMLCode = lclsSequence.makeTable

        llngAux = 1
        lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)

        lblnValid = True

        For llngCount = 1 To CN_FRAMESNUMPRODACTLIFESEQ

            '+ Se extrae el código de la ventana
            lstrCodispl = Trim(Mid(CN_WINDOWSPRODACTLIFESEQ, llngAux, 8))
            llngAux = llngAux + 8

            If lblnValid Then
                If lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'") Then

                    Select Case lstrCodispl
                        '+ Se obtiene por cada transacción un campo (requerido) de la misma para identificar
                        '+ si tiene o no contenido

                        '+ Condiciones generales de Planes de VidActiva

                        Case "DP607A"
                            lcolTab_ActiveLifes = New Tab_ActiveLifes
                            If lcolTab_ActiveLifes.Find(nBranch, nProduct, dEffecdate) Then
                                For Each lclsTab_ActiveLife In lcolTab_ActiveLifes
                                    If lclsTab_ActiveLife.nExists = 1 Then
                                        lvntRequireField = 1
                                        Exit For
                                    End If
                                Next lclsTab_ActiveLife
                            End If
                            'UPGRADE_NOTE: Object lcolTab_ActiveLifes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lcolTab_ActiveLifes = Nothing

                            '+ Cargos por Planes de VidActiva
                        Case "DP607B"
                            lcolPlan_Loads = New Plan_Loadss
                            If lcolPlan_Loads.Find_Product(nBranch, nProduct, dEffecdate) Then
                                lvntRequireField = 1
                            End If
                            'UPGRADE_NOTE: Object lcolPlan_Loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lcolPlan_Loads = Nothing

                        Case "DP607C"
                            lcolLoad_surrs = New Load_surrs
                            If lcolLoad_surrs.Find_Product(nBranch, nProduct, dEffecdate) Then
                                lvntRequireField = 1
                            End If
                            'UPGRADE_NOTE: Object lcolLoad_surrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lcolLoad_surrs = Nothing

                        Case "DP607D"
                            lcolPlan_IntWarr = New Plan_IntWarrs
                            If lcolPlan_IntWarr.Find_Product(nBranch, nProduct, dEffecdate) Then
                                lvntRequireField = 1
                            End If
                            'UPGRADE_NOTE: Object lcolPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lcolPlan_IntWarr = Nothing

                        Case Else
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            lvntRequireField = System.DBNull.Value
                    End Select

                    '+ Se asigna la imagen asociada a la página asociada al Codispl
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    If lvntRequireField = eRemoteDB.Constants.intNull Or lvntRequireField = eRemoteDB.Constants.dtmNull Or lvntRequireField = String.Empty Or IsDBNull(lvntRequireField) Then
                        lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                    Else
                        '+ Ventanas con contenido
                        lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                    End If

                    lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintPageImage)
                End If
            End If

            lblnValid = True
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            lvntRequireField = System.DBNull.Value
        Next llngCount

        LoadTabs = lstrHTMLCode & lclsSequence.closeTable()

        'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsQuery = Nothing
        'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSequence = Nothing

LoadTabs_Err:
        If Err.Number Then
            LoadTabs = "LoadTabs: " & Err.Description
        End If

        On Error GoTo 0
    End Function

    '% insValDP607: Valida los campos de la página DP607
    '% (Condiciones Generales de Productos de VidActiva)
    Public Function insValDP607(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nPremmin As Double, ByVal nQmonVPN As Integer, ByVal nQmonToVPN As Integer, ByVal nRateReh As Double) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValDP607_Err

        lobjErrors = New eFunctions.Errors

        '+ Validación del campo "Prima Mínima"
        If nPremmin = eRemoteDB.Constants.intNull Or nPremmin <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 60161)
        End If

        '+ Validación del campo "Meses < 0, caducar"
        If nQmonVPN = eRemoteDB.Constants.intNull Or nQmonVPN <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 60164)
        End If

        '+ Validación del campo "Meses < 0, calcular"
        If nQmonToVPN = eRemoteDB.Constants.intNull Or nQmonToVPN <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 60165)
        End If

        '+ Validación del campo "Prima adicional rehabilitación"
        If nRateReh = eRemoteDB.Constants.intNull Or nRateReh <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 60166)
        End If

        insValDP607 = lobjErrors.Confirm

insValDP607_Err:
        If Err.Number Then
            insValDP607 = insValDP607 & Err.Description
        End If

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '% insPostDP607: Realiza el llamado a la rutina que actualiza la tabla Product_li,
    '%               para la página DP607 (Condiciones Generales de Productos de VidActiva)
    Public Function insPostDP607(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nPremmin As Double, ByVal nQmonVPN As Integer, ByVal nQmonToVPN As Integer, ByVal nRateReh As Double, ByVal nDay_bmg As Integer, ByVal nYear_bmg As Integer, ByVal nAge_bmg As Integer) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsProd_win As eProduct.Prod_win

        On Error GoTo insPostDP607_Err

        lclsProduct = New eProduct.Product
        lclsProd_win = New eProduct.Prod_win

        insPostDP607 = True

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .nUsercode = nUsercode
                .nPremmin = nPremmin
                .nQmonVPN = nQmonVPN
                .nQmonToVPN = nQmonToVPN
                .nRateReh = nRateReh
                .dEffecdate = dEffecdate
                .nDay_bmg = nDay_bmg
                .nYear_bmg = nYear_bmg
                .nAge_bmg = nAge_bmg
                insPostDP607 = .insProdActLifeSeq

            End If
        End With

insPostDP607_Err:
        If Err.Number Then
            insPostDP607 = False
        End If

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function


    '%Add: Crea un registro en la tabla
    Public Function Add() As Boolean
        Add = InsUpdTab_ActiveLife(1)
    End Function

    '%Update: Actualiza un registro en la tabla
    Public Function Update() As Boolean
        Update = InsUpdTab_ActiveLife(2)
    End Function

    '%Delete: Borra un registro en la tabla
    Public Function Delete() As Boolean
        Delete = InsUpdTab_ActiveLife(3)
    End Function

    '%Find: Lee los datos de la tabla
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nOption As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaTab_activelife As eRemoteDB.Execute

        On Error GoTo Find_Err
        If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nOption <> nOption Or mdtmEffecdate <> dEffecdate Or bFind Then

            '+ Definición de los parámetros Stored Procedure reaTab_activelife
            lrecreaTab_activelife = New eRemoteDB.Execute
            With lrecreaTab_activelife
                .StoredProcedure = "reaTab_activelife"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nModulec = nModulec
                    Me.dEffecdate = .FieldToClass("dEffecdate")
                    mdtmEffecdate = Me.dEffecdate
                    Me.nCurrency = .FieldToClass("nCurrency")
                    Me.nCapmin = .FieldToClass("nCapmin")
                    Me.nUsercode = .FieldToClass("nUsercode")
                    Me.nMchainves = .FieldToClass("nMchainves")
                    Me.nErrrange = .FieldToClass("nErrrange")
                    Me.nOption = nOption
                    Me.nPercent = .FieldToClass("nPercent")
                    Me.nMin_prembas = .FieldToClass("nMin_prembas")
                    Me.nMax_prembas = .FieldToClass("nMax_prembas")
                    Me.nMin_premmin = .FieldToClass("nMin_premmin")
                    Me.nMax_premmin = .FieldToClass("nMax_premmin")
                    Me.nMin_premexc = .FieldToClass("nMin_premexc")
                    Me.nMax_premexc = .FieldToClass("nMax_premexc")

                    Me.nMin_premPac = .FieldToClass("nMin_premPac")
                    Me.nMax_premPac = .FieldToClass("nMax_premPac")

                    Find = True
                    .RCloseRec()
                End If
            End With
        Else
            Find = True
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaTab_activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_activelife = Nothing
        On Error GoTo 0
    End Function

    '%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nModulec = eRemoteDB.Constants.intNull
        dEffecdate = eRemoteDB.Constants.dtmNull
        nCurrency = eRemoteDB.Constants.intNull
        nCapmin = eRemoteDB.Constants.intNull
        nUsercode = eRemoteDB.Constants.intNull
        nMchainves = eRemoteDB.Constants.intNull
        nErrrange = eRemoteDB.Constants.intNull
        nExists = eRemoteDB.Constants.intNull
        mdtmEffecdate = eRemoteDB.Constants.dtmNull
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%Find: Lee los datos de la tabla
    Public Function FindDP607A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaTab_activelife_o As eRemoteDB.Execute

        On Error GoTo reaTab_activelife_o_Err

        lrecreaTab_activelife_o = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaTab_activelife_o al 06-20-2002 16:55:07
        '+
        With lrecreaTab_activelife_o
            .StoredProcedure = "reaTab_activelife_o"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FindDP607A = True
            Else
                FindDP607A = False
            End If
        End With

reaTab_activelife_o_Err:
        If Err.Number Then
            FindDP607A = False
        End If
        'UPGRADE_NOTE: Object lrecreaTab_activelife_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_activelife_o = Nothing
        On Error GoTo 0

    End Function
End Class






