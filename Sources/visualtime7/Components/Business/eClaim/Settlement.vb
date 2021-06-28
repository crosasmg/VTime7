Option Strict Off
Option Explicit On
Public Class Settlement
	'%-------------------------------------------------------%'
	'% $Workfile:: Settlement.cls                           $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 20-08-09 1:50                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public nClaim As Double
    Public nSettlement As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public sClient As String
	Public nAmount As Double
	Public nCurrency As Integer
	Public nPaid_Amoun As Double
	Public dPrinted_Da As Date
	Public dPropou_Dat As Date
	Public sStatus_Fin As String
	Public nUserCode As Integer
	Public nId As Integer
	Public nAction As Integer
    Public nAmount_Add As Double
    Public nSettlecode As Integer
    Public nSettlecode_Aux As Integer
    Public nId_settle As Integer
    Public sFormatname As String
    Public nSettle_num As Integer
	Public nAmount_Settlement As Double
	Public nSettlement_Next As Double
	Public dEffecdate As Date
    Public nPay_concep As Integer
    Public nCover As Integer
    Public Exist As Integer
    Public sTips As String
	'%FindAmount: Realiza la sumatoria de los importes de todos los finiquitos de un siniestro
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnRead As Boolean = True) As Boolean
		Dim lrecSettlement As eRemoteDB.Execute
		
		Static ldblOldClaim As Double
		Static llngOldCase_num As Integer
		Static llngOldDeman_type As Integer
		
		
		On Error GoTo Find
		
		If ldblOldClaim <> nClaim Or llngOldCase_num <> nCase_num Or llngOldDeman_type <> nDeman_type Or lblnRead Then
			
			ldblOldClaim = nClaim
			llngOldCase_num = nCase_num
			llngOldDeman_type = nDeman_type
			
			lrecSettlement = New eRemoteDB.Execute
			
			With lrecSettlement
				.StoredProcedure = "reaSettlement_1" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then 'Listo
					Find = True
				Else
					Find = False
				End If
			End With
			
			lrecSettlement = Nothing
		End If
Find: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%insValSI017: Se realizan las validaciones sobre la solicitud de finiquitos
	Public Function insValSI017(ByVal sCodispl As String, ByVal nCase_num As Integer, ByVal dEffecdate As Date, ByVal nAmount As Double, ByVal dOccurdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSI017_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Validación del campo "Caso"
			If nCase_num = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 4310)
			End If
			
			'+Validación del campo "Fecha"
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 7114)
			Else
				If dEffecdate < dOccurdate Then
					Call .ErrorMessage(sCodispl, 4254)
				End If
			End If
			
			'+Validación del campo "Importe del finiquito"
			If nAmount = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 4187)
			End If
			
			insValSI017 = .Confirm
		End With
		
insValSI017_Err: 
		If Err.Number Then
			insValSI017 = "insValSI017: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function

    '%insValSI764: Se realizan las validaciones sobre la solicitud de finiquitos
    Public Function insValSI764(ByVal sCodispl As String, ByVal nCase_num As Integer, ByVal dEffecdate As Date, ByVal nAmount As Double, ByVal dOccurdate As Date) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSI017_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            '+Validación del campo "Caso"
            If nCase_num = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 4310)
            End If

            '+Validación del campo "Fecha"
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                Call .ErrorMessage(sCodispl, 7114)
            Else
                If dEffecdate < dOccurdate Then
                    Call .ErrorMessage(sCodispl, 4254)
                End If
            End If

            '+Validación del campo "Importe del finiquito"
            If nAmount = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 4187)
            End If

            insValSI764 = .Confirm
        End With

insValSI017_Err:
        If Err.Number Then
            insValSI764 = "insValSI764: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
	'%insValSI017_End: Valida que al aceptar la pagina tenga por lo menos un elemento en el grid
	Public Function insValSI017_End() As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSI017_End_Err
		
		lclsErrors = New eFunctions.Errors
		
		'    Call lclsErrors.ErrorMessage("SI017", 4281)
		
		insValSI017_End = lclsErrors.Confirm
		
insValSI017_End_Err: 
		If Err.Number Then
			insValSI017_End = insValSI017_End & Err.Description
		End If
		
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostSI017: se actualizan los datos de la tabla Settlement
	Public Function InsPostSI017(ByVal sAction As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nSettlement As Double, ByVal sClient As String, ByVal nAmount As Double, ByVal nPaidAmoun As Double, ByVal nUserCode As Integer, ByVal dPropou_Dat As Date, ByVal sStatus_Fin As String, ByVal dEffecdate As Date, Optional ByVal nId As Integer = 0) As Boolean
		On Error GoTo InsPostSI017_Err
		
		Me.nClaim = nClaim
		Me.nCase_num = nCase_num
		Me.nDeman_type = nDeman_type
		Me.nSettlement = nSettlement
		Me.sClient = sClient
		Me.nAmount = nAmount
		Me.nPaid_Amoun = nPaidAmoun
		Me.nUserCode = nUserCode
		Me.dPropou_Dat = dPropou_Dat
		Me.sStatus_Fin = sStatus_Fin
		Me.dEffecdate = dEffecdate
		Me.nId = nId
		
		Select Case sAction
			Case "Add"
				Me.nAction = 1
			Case "Update"
				Me.nAction = 2
			Case "Del"
				Me.nAction = 3
		End Select
		
		InsPostSI017 = insUpdSettlement
		
InsPostSI017_Err: 
		If Err.Number Then
			InsPostSI017 = False
		End If
		On Error GoTo 0
    End Function

    Public Function InsPostSI764(ByVal sAction As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nSettle_num As Double, ByVal sClient As String, ByVal nAmount As Double, ByVal nPaidAmoun As Double, ByVal nUserCode As Integer, ByVal dPropou_Dat As Date, ByVal sStatus_Fin As String, ByVal dEffecdate As Date, ByVal nSettlecode As Integer, ByVal nSettlecode_Aux As Integer, ByVal nPay_concep As Double, ByVal nCover As Integer, Optional ByVal nId As Integer = 0) As Boolean
        On Error GoTo InsPostSI764_Err

        Me.nClaim = nClaim
        Me.nCase_num = nCase_num
        Me.nDeman_type = nDeman_type
        Me.nSettle_num = nSettle_num
        Me.sClient = sClient
        Me.nAmount = nAmount
        Me.nPaid_Amoun = nPaidAmoun
        Me.nUserCode = nUserCode
        Me.dPropou_Dat = dPropou_Dat
        Me.sStatus_Fin = sStatus_Fin
        Me.dEffecdate = dEffecdate
        Me.nSettlecode = nSettlecode
        Me.nSettlecode_Aux = nSettlecode_Aux
        Me.nPay_concep = nPay_concep
        Me.nCover = nCover
        Me.nId = nId

        Select Case sAction
            Case "Add"
                Me.nAction = 1
            Case "Update"
                Me.nAction = 2
            Case "Del"
                Me.nAction = 0
        End Select

        InsPostSI764 = insUpdSettlement()

        If InsPostSI764 Then
            If nAction = 1 Or nAction = 2 Then
                nAction = 1
            End If
            insSettlement_det(nAction, nClaim, nCase_num, nDeman_type, nSettlecode, nSettlecode_Aux, nPay_concep, nCover, nUserCode)
        End If

InsPostSI764_Err:
        If Err.Number Then
            InsPostSI764 = False
        End If
        On Error GoTo 0
    End Function

    '%insSettlement_det: Edita información en tabla Settlement_Det

    Public Function insSettlement_det(ByVal nAction As Integer, ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nSettlecode As Integer, ByVal nSettlecode_Aux As Integer, ByVal nPay_concep As Double, ByVal nCover As Integer, ByVal nUserCode As Integer) As Boolean
        Dim lrecSettlement_det As eRemoteDB.Execute

        On Error GoTo insUpdSettlement_Err

        lrecSettlement_det = New eRemoteDB.Execute

        With lrecSettlement_det
            .StoredProcedure = "InsSettlement_det"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettlecode", nSettlecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettlecode_Aux", nSettlecode_Aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insSettlement_det = .Run(False)
        End With

insUpdSettlement_Err:
        If Err.Number Then
            insSettlement_det = False
        End If
        On Error GoTo 0
        insSettlement_det = Nothing

    End Function


    '% insUpdSettlement: Actualiza los registros que fueron seleccionados en la tabla Settlement
    Private Function insUpdSettlement() As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo insUpdSettlement_Err

        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "InsSettlement"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettle_num", nSettle_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmoun", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaid_Amoun", nPaid_Amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dPrinted_Da", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPropou_Dat", dPropou_Dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatus_Fin", sStatus_Fin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdSettlement = .Run(False)
        End With

insUpdSettlement_Err:
        If Err.Number Then
            insUpdSettlement = False
        End If
        On Error GoTo 0
        lrecSettlement = Nothing
    End Function

    '%FindAmount: Realiza la sumatoria de los importes de todos los finiquitos de un siniestro
    Public Function FindAmount(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo FindAmount_Err

        nAmount_Settlement = 0
        FindAmount = True


        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "ReaAmount_Settlement"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount_Settlement, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nAmount_Settlement = .Parameters.Item("nAmount").Value
            End If
        End With

        lrecSettlement = Nothing

FindAmount_Err:
        If Err.Number Then
            FindAmount = False
        End If
        On Error GoTo 0
    End Function


    '%FindAmount: Realiza la sumatoria de los importes de todos los finiquitos de un siniestro
    Public Function FindSettlement_Next(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo FindSettlement_Next_Err

        nSettlement_Next = 0
        FindSettlement_Next = True


        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "reaSettlement_Number"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettlement_Next", nSettlement_Next, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nSettlement_Next = .Parameters.Item("nSettlement_Next").Value
            End If
        End With

        lrecSettlement = Nothing

FindSettlement_Next_Err:
        If Err.Number Then
            FindSettlement_Next = False
        End If
        On Error GoTo 0
    End Function

    '%FindAmount: Realiza la sumatoria de los importes de todos los finiquitos de un siniestro
    Public Function Find_Settlement(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nSettle_num As Integer) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo Find_Settlement

        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "reaSettlement_v"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettle_num", nSettle_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find_Settlement = True
                If .RecordCount = 1 Then
                    sStatus_Fin = .FieldToClass("sStatus_fin")
                Else
                    sStatus_Fin = .FieldToClass("sStatus_fin")
                    Do While Not .EOF And CDbl(sStatus_Fin) = 2
                        If .FieldToClass("sStatus_fin") = 1 Then
                            sStatus_Fin = .FieldToClass("sStatus_fin")
                        End If
                        .RNext()
                    Loop
                End If
                .RCloseRec()
            Else
                Find_Settlement = False
            End If
        End With
        lrecSettlement = Nothing

Find_Settlement:
        If Err.Number Then
            Find_Settlement = False
        End If
        On Error GoTo 0
    End Function

    '%ValExistSettlement: Realiza la sumatoria de los importes de todos los finiquitos de un siniestro
    Public Function ValExistSettlement(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo ValExistSettlement

        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "ValExistSettlement"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                ValExistSettlement = True
                .RCloseRec()
            Else
                ValExistSettlement = False
            End If
        End With
        lrecSettlement = Nothing

ValExistSettlement:
        If Err.Number Then
            ValExistSettlement = False
        End If
        On Error GoTo 0
    End Function


    Public Function ValExist_CL_Settlement(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        On Error GoTo ValExist_CL_Settlement_err

        lrecSettlement = New eRemoteDB.Execute

        With lrecSettlement
            .StoredProcedure = "ValExist_CL_Settlement"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                ValExist_CL_Settlement = True
                .RCloseRec()
            Else
                ValExist_CL_Settlement = False
            End If
        End With
        lrecSettlement = Nothing

ValExist_CL_Settlement_err:
        If Err.Number Then
            ValExist_CL_Settlement = False
        End If
        On Error GoTo 0
    End Function
End Class






