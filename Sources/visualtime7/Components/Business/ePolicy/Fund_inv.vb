Option Strict Off
Option Explicit On
Public Class Fund_inv
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_inv.cls                             $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 31/10/03 11.38                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name                     Type        Computed   Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'+ ------------------------------  ----------- ---------- ----------- ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nFunds As Integer 'smallint   no         2           5     0     no                                  (n/a)                               (n/a)
	Public sDescript As String 'char       no         30                      yes                                 no                                  yes
	Public nQuan_avail As Double
	Public nQuan_max As Double
	Public dInpdate As Date 'datetime   no         8                       yes                                 (n/a)                               (n/a)
	Public nQuan_min As Double
	Public sStatregt As String 'char       no         1                       yes                                 no                                  yes
	Public nUsercode As Integer 'smallint   no         2           5     0     no                                  (n/a)                               (n/a)
    Public nCountry As Integer
    Public nSeries As Double
    Public nRun As Double
    Public sRoutine As String
    Public sGuaranteed As String
    Public sTicker As String
    Public sISIN_code As String
	'**- The varible to determines the clase status is defined
	'- Se define la variable que determina el estado de la clase
	
	Public Enum eStatusInstance_f
		eftNew_f = 0
		eftQuery_f = 1
		eftExist_f = 1
		eftUpDate_f = 2
		eftDelete_f = 3
	End Enum
	
	Public nStatInstanc As eStatusInstance_f
	
	'**- The global variable that have the fund code and this variable is used
	'**- to determines is the read was performed
	'- Se define la variable global que tiene el código del fondo
	'- y es utilizada para no volver a realizar la lectura
	
	Private mintFunds As Integer
	
	'**% Add: Add record to the Fund_inv table
	'% Add: Permite registrar un registro en la tabla Fund_inv
	Public Function Add() As Boolean
		Dim lreccreFund_inv As eRemoteDB.Execute
		
		lreccreFund_inv = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		Add = True
		
		With lreccreFund_inv
			.StoredProcedure = "creFund_inv"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_min", nQuan_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_max", nQuan_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_avail", nQuan_avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeries", nSeries, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRun", nRun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sGuaranteed", sGuaranteed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTicker", sTicker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sISIN_code", sISIN_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFund_inv = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
	End Function
	
	'**% Update: Update the fund_inv table
	'% Update: Permite actualizar un registro en la tabla Fund_inv
	Public Function Update() As Boolean
		Dim lrecupdFund_inv As eRemoteDB.Execute
		
		lrecupdFund_inv = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		Update = True

        With lrecupdFund_inv
            .StoredProcedure = "updFund_inv"

            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_min", nQuan_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_max", nQuan_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_avail", nQuan_avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeries", nSeries, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRun", nRun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sGuaranteed", IIf(IsNothing(sGuaranteed), "", sGuaranteed), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTicker", sTicker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sISIN_code", sISIN_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With


        'UPGRADE_NOTE: Object lrecupdFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdFund_inv = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	'**% UpdateQuan_avail: Updates the availabity units of the table Fund_inv
	'% UpdateQuan_avail: Permite actualizar las unidades disponibles de la tabla Fund_inv
	Public Function UpdateQuan_avail() As Boolean
		Dim lrecupdFund_inv_1 As eRemoteDB.Execute
		
		lrecupdFund_inv_1 = New eRemoteDB.Execute
		
		On Error GoTo UpdateQuan_avail_Err
		
		UpdateQuan_avail = True
		
		With lrecupdFund_inv_1
			.StoredProcedure = "updFund_inv_1"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuan_avail", nQuan_avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateQuan_avail = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdFund_inv_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFund_inv_1 = Nothing
		
UpdateQuan_avail_Err: 
		If Err.Number Then
			UpdateQuan_avail = False
		End If
	End Function
	
	'**% Delete: This function delete the records of the table Fund_inv
	'% Delete: Permite eliminar un registro de la tabla Fund_inv
	Public Function Delete() As Boolean
		Dim lrecdelFund_inv As eRemoteDB.Execute
		
		lrecdelFund_inv = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		Delete = True
		
		With lrecdelFund_inv
			.StoredProcedure = "delFund_inv"
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFund_inv = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
	End Function
	
	'**% Find: read the information of the an investment funds
	'% Find: Permite seleccionar la información de un fondo
    Public Function Find(ByVal lintFunds As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaFund_inv_1 As eRemoteDB.Execute

        lrecreaFund_inv_1 = New eRemoteDB.Execute

        On Error GoTo Find_Err

        Find = True

        If lintFunds <> mintFunds Or lblnFind Then
            With lrecreaFund_inv_1
                .StoredProcedure = "reaFund_inv_1"
                .Parameters.Add("nFunds", lintFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    mintFunds = lintFunds
                    nFunds = lintFunds
                    nQuan_max = .FieldToClass("nQuan_max", eRemoteDB.Constants.intNull)
                    nQuan_min = .FieldToClass("nQuan_min")
                    nQuan_avail = .FieldToClass("nQuan_avail")
                    dInpdate = .FieldToClass("dInpdate")
                    sDescript = .FieldToClass("sDescript")
                    sStatregt = .FieldToClass("sStatregt")
                    nCountry = .FieldToClass("nCountry")
                    nSeries = .FieldToClass("nSeries")
                    nRun = .FieldToClass("nRun")
                    sRoutine = .FieldToClass("sRoutine")
                    sGuaranteed = .FieldToClass("sGuaranteed")
                    sTicker = .FieldToClass("sTicker")
                    sISIN_code = .FieldToClass("sISIN_code")

                    .RCloseRec()
                Else
                    Find = False
                End If
            End With

            'UPGRADE_NOTE: Object lrecreaFund_inv_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaFund_inv_1 = Nothing
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
	
	'**% FindFunds: This function verifies that the fund has not information related in table Funds_pol
	'% FindFunds: Verifica que el fondo no tenga información relacionada en la tabla Funds o en la tabla Funds_pol
    Public Function FindFunds(ByVal lintFunds As Integer) As Boolean
        Dim lrecvalFunds As eRemoteDB.Execute
        Dim lintExist As Integer

        lrecvalFunds = New eRemoteDB.Execute

        On Error GoTo FindFunds_Err

        lintExist = 0
        With lrecvalFunds
            .StoredProcedure = "valFunds"

            .Parameters.Add("nFunds", lintFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                FindFunds = IIf(.Parameters.Item("nExist").Value = 0, False, True)
            End If
        End With

        'UPGRADE_NOTE: Object lrecvalFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalFunds = Nothing

FindFunds_Err:
        If Err.Number Then
            FindFunds = False
        End If
    End Function
	
	'**% insValMVI003_k: This function performed the validation according to the functional specification
	'% insValMVI003_k: Realiza las validaciones correspondientes, según lo indica el funcional de
	'% la transacción
	Public Function insValMVI003_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date) As String
		On Error GoTo insValMVI003_k_Err
		
		Dim ldtmFindDateMax As Date
		Dim lclsError As eFunctions.Errors
		Dim lclsvalfield As eFunctions.valField
		Dim lcolFund_invs As ePolicy.Fund_invs
		
		lclsError = New eFunctions.Errors
		lclsvalfield = New eFunctions.valField
		lcolFund_invs = New ePolicy.Fund_invs
		lclsvalfield.objErr = lclsError
		
		'**+ Date validation
		'+ Validación de la fecha
		
		'**+ Verifies that the date should be valid
		'+ Se verifica que la fecha sea válida
		
		If Not IsDate(dEffecdate) Or dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsError.ErrorMessage(sCodispl, 4003)
		Else
			
			'**+ Verifies that the date should be posterior to the day date
			'+ Se verifica que la fecha sea posterior a la fecha del día
			
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				
				'**+ Verifies that the data should be posterior to the last transaction
				'+ Se verifica que la fecha sea posterior a la de la última transacción
				
				ldtmFindDateMax = lcolFund_invs.FindDateMax
				
				If ldtmFindDateMax <> eRemoteDB.Constants.dtmNull Then
					If dEffecdate <= ldtmFindDateMax Then
						lclsError.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
						Call lclsError.ErrorMessage(sCodispl, 10868)
					End If
				End If
				'**+ Verifies that the effect date should be posterior to the system
				'+ Se verifica que la fecha de efecto sea postterior a la del sistema.
				
				If dEffecdate <= Today Then
					lclsError.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
					Call lclsError.ErrorMessage(sCodispl, 10869)
				End If
			End If
		End If
		
		insValMVI003_k = lclsError.Confirm
		
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lcolFund_invs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_invs = Nothing
		
insValMVI003_k_Err: 
		If Err.Number Then
			insValMVI003_k = "insValMVI003_k: " & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValMVI003: This function performed the validation of the page.
	'% insValMVI003: Realiza las validaciones propias de la transacción.
    Public Function insValMVI003(ByVal sCodispl As String, ByVal sAction As String, ByVal nFunds As Integer, ByVal nQuan_min As Double, ByVal nQuan_max As Double, ByVal nQuan_avail As Double, ByVal sDescript As String, ByVal sStatregt As String, ByVal dInpdate As Date, ByVal nUsercode As Integer, ByVal nSeries As Double, ByVal nRun As Double, Optional ByVal nCountry As Integer = 0, Optional ByVal sRoutine As String = "") As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalfield As eFunctions.valField
        Dim lrecinsinsValMVi003 As Object

        '**- The variable to show the validation related to status of the record only when
        '**- the user entered the rest of the field is defined
        '- Variable que permitira mostrar la validación asociada al estado del registro solo
        '- cuando se indiquen el resto de los campos

        Dim lblnError As Object
        Dim lstrErrors As String

        lclsErrors = New eFunctions.Errors
        lclsvalfield = New eFunctions.valField
        lclsvalfield.objErr = lclsErrors
        On Error GoTo insValMVI003_Err

        If nFunds <= 0 Then
            lclsErrors.ErrorMessage(sCodispl, 17001)
            lblnError = True
        End If

        'Valida Fecha de ingreso del fondo al sistema
        If dInpdate = eRemoteDB.Constants.dtmNull Then
            lclsErrors.ErrorMessage(sCodispl, 4148)
            lblnError = True
        End If

        '**+ Description field validation.
        '+ Validación de la "Descripción".

        If Trim(sDescript) = String.Empty Then
            lclsErrors.ErrorMessage(sCodispl, 10071)
            lblnError = True
        End If

        '**+ Minimum units field validation.
        '+ Validación de las "Unidades-Mínimas".

        If nQuan_min <= 0 Then
            lclsErrors.ErrorMessage(sCodispl, 11159)
            lblnError = True
        Else
            If nQuan_max > 0 Then
                If nQuan_min >= nQuan_max Then
                    lclsErrors.ErrorMessage(sCodispl, 10824)
                End If
            End If

            If nQuan_min > nQuan_avail Then
                lclsErrors.ErrorMessage(sCodispl, 10083)
            End If
        End If

        '**+ maximun units field validation.
        '+ Validación de las "Unidades-Máximas".

        If nQuan_max > 0 Then
            If nQuan_max < nQuan_avail Then
                lclsErrors.ErrorMessage(sCodispl, 10823)
            End If
        End If

        If Find(nFunds) And sAction = "Add" Then
            lclsErrors.ErrorMessage(sCodispl, 10260)
        End If

        '**+ Status field validation.
        '+ Validación del campo "Estado".

        If sStatregt = "0" Or sStatregt = String.Empty And Not lblnError Then
            lclsErrors.ErrorMessage(sCodispl, 13423)

            '+No se puede restringir un fondo con información asociada
        ElseIf sStatregt = "3" And sAction = "Update" And Me.sStatregt = "1" Then
            If Me.FindFunds(nFunds) Then
                lclsErrors.ErrorMessage(sCodispl, 11241)
            End If
        End If
        '+ Validación del pais
        If nCountry <= 0 Then
            lclsErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.RigthAling, "(País)")
            lblnError = True
        End If

        '+ Validación si existen feriado para el pais indicado
        '------------------

        lrecinsinsValMVi003 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        With lrecinsinsValMVi003
            .StoredProcedure = "INSVALMVI003"
            .Parameters.Add("NCOUNTRY", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage("sCodispl", , , , , , lstrErrors)

        '+ Validación del campo "Rutina".
        If sRoutine = String.Empty Then
            lclsErrors.ErrorMessage(sCodispl, 800201)
        End If

        insValMVI003 = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalfield = Nothing

insValMVI003_Err:
        If Err.Number Then
            insValMVI003 = insValMVI003 & Err.Description
        End If

        On Error GoTo 0
    End Function

    '**% insPostMVI003: Update the page information.
    '% insPostMVI003: Actualiza los datos de la forma.
    Public Function insPostMVI003(
                                 ByVal sAction As String,
                                 ByVal nFunds As Integer,
                                 ByVal nQuan_min As Double,
                                 ByVal nQuan_max As Double,
                                 ByVal nQuan_avail As Double,
                                 ByVal sDescript As String,
                                 ByVal sStatregt As String,
                                 ByVal dInpdate As Date,
                                 ByVal nUsercode As Integer,
                                 ByVal nSeries As Double,
                                 ByVal nRun As Double,
                                 Optional ByVal nCountry As Integer = 0,
                                 Optional ByVal sRoutine As String = "",
                                 Optional ByVal sGuaranteed As String = "",
                                 Optional ByVal sTicker As String = "",
                                 Optional ByVal sISIN_code As String = "") As Boolean
        Dim lclsFund_inv As ePolicy.Fund_inv
        lclsFund_inv = New ePolicy.Fund_inv

        On Error GoTo insPostMVI003_err

        With lclsFund_inv
            .nFunds = nFunds
            .sDescript = sDescript
            .nQuan_min = nQuan_min
            .nQuan_max = nQuan_max
            .nQuan_avail = nQuan_avail
            .sDescript = sDescript
            .sStatregt = sStatregt
            .dInpdate = dInpdate
            .nUsercode = nUsercode
            .nSeries = nSeries
            .nRun = nRun
            .nCountry = nCountry
            .sRoutine = sRoutine
            .sGuaranteed = sGuaranteed
            .sTicker = sTicker
            .sISIN_code = sISIN_code

            If sAction = "Add" Then
                insPostMVI003 = .Add
            ElseIf sAction = "Update" Then
                insPostMVI003 = .Update
            ElseIf sAction = "Del" Then
                insPostMVI003 = .Delete
            End If
        End With

        'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund_inv = Nothing

insPostMVI003_err:
        If Err.Number Then
            insPostMVI003 = False
        End If

        On Error GoTo 0
    End Function
End Class






