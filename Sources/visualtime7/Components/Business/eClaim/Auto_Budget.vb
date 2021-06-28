Option Strict Off
Option Explicit On
Public Class Auto_Budget
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_Budget.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 3/11/03 21.46                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'- Se definen las propiedades principales de la clase correspondientes a la tabla Auto_Budget
	'- El campo llave corresponde a nServ_Order
	
	
	'-    Name                           Type              Null
	'- ------------------------------ ----------     ----------------------
	Public nServ_Order As Double '  NOT NULL NUMBER(10)
	Public sClient As String '  NOT NULL CHAR(14)
	Public nNum_Budget As Double '  NOT NULL NUMBER(10)
	Public dBudget_Date As Date '  NOT NULL DATE
	Public nAmount_Labor As Double '           NUMBER(18,6)
	Public nAmount_Paint As Double '           NUMBER(18,6)
	Public nAmount_Mechan As Double '           NUMBER(18,6)
	Public nAmount_Part As Double '           NUMBER(18,6)
	Public nAmount As Double '           NUMBER(18,6)
	Public nUsercode As Integer '  NOT NULL NUMBER(5)
	Public dCompdate As Date '  NOT NULL DATE
    Public nAmount_Ajus_Ord As Double


    '- Variables auxiliares
    Public sDesVehBrand As String
	Public sDesVehModel As String
	Public nYear As Integer
	Public sChassis As String
	Public nDeduc_amount As Double
	Public nDeprec_amount As Double
	Public nExist As Integer
    Public sWsdeduc As String
    Public nWorksh As Integer
	
	'%Find: Se obtienen los datos asociados a un número de orden de servicio
    Public Function Find(ByVal nServ_Order As Double, ByVal nClaim As Double, Optional ByVal nUsercode As Integer = 0, Optional ByVal nMainAction As Integer = 0) As Boolean
        Dim lrecreaAuto_Budget As eRemoteDB.Execute
        Dim lreProf_ord As New eClaim.Prof_ord
        lrecreaAuto_Budget = New eRemoteDB.Execute

        On Error GoTo Find_Err

        'Definición de parámetros para stored procedure 'insudb.reaAuto_Budget'
        'Información leída el 20/09/1999 08:02:03 AM

        '+ Llamada que actualiza los valores de Audatex
        If nMainAction = eFunctions.Menues.TypeActions.clngActionadd Then
            lreProf_ord.receiveInspectionResult(nUsercode, Today, , nServ_Order)
        End If


        With lrecreaAuto_Budget
            .StoredProcedure = "reaAuto_Budget"
            .Parameters.Add("nServ_Order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nServ_Order = .FieldToClass("nServ_Order")
                sClient = .FieldToClass("sClient")
                nNum_Budget = .FieldToClass("nNum_Budget")
                dBudget_Date = .FieldToClass("dBudget_Date")
                nAmount_Labor = .FieldToClass("nAmount_Labor")
                nAmount_Paint = .FieldToClass("nAmount_Paint")
                nAmount_Mechan = .FieldToClass("nAmount_Mechan")
                nAmount_Part = .FieldToClass("nAmount_Part")
                nAmount = .FieldToClass("nAmount")
                nDeduc_amount = .FieldToClass("nDeduc_amount")
                nDeprec_amount = .FieldToClass("nDeprec_amount")
                nExist = .FieldToClass("nExist")
                sWsdeduc = .FieldToClass("sWsdeduc")
                nWorksh = .FieldToClass("nWorksh")
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        lrecreaAuto_Budget = Nothing
        On Error GoTo 0
    End Function
	
	'%Update: Actualiza todos los campos de un registro en la tabla Auto_Budget
	Public Function Update() As Boolean
		Dim lrecinsAuto_Budget As eRemoteDB.Execute
		
		lrecinsAuto_Budget = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'Definición de parámetros para stored procedure 'insudb.insAuto_Budget'
		'Información leída el 20/09/1999 08:39:08 AM
		
		With lrecinsAuto_Budget
			.StoredProcedure = "insUpdAuto_Budget"
			.Parameters.Add("nServ_Order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNum_Budget", nNum_Budget, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBudget_date", dBudget_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Labor", nAmount_Labor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Paint", nAmount_Paint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Mechan", nAmount_Mechan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Part", nAmount_Part, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAudatex", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_Ajus_Ord", nAmount_Ajus_Ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		lrecinsAuto_Budget = Nothing
	End Function
	
	'% insValSI775_K: Se realizan las validaciones del encabezado de Ingreso de presupuesto
	Public Function insValSI775_K(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nServ_Order As Double, ByVal nMainAction As Short) As String
		Dim lclsClaim As eClaim.Claim
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProf_ord As eClaim.Prof_ord
		Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValSI775_K_Err
		
		lclsClaim = New eClaim.Claim
		lclsErrors = New eFunctions.Errors
		
		lstrSep = "||"
		
		'+ Se valida que el campo "Número de Siniestro" no esté vacio
		If nClaim = eRemoteDB.Constants.intNull Or nClaim = 0 Then
			lstrError = lstrError & lstrSep & "4006"
		Else
			'+ Se valida que el siniestro esté válido (NO en Captura incompleta, ni anulado, ni rechazado)
			
			If lclsClaim.Find(nClaim) Then
				If lclsClaim.sStaclaim = Claim.Estatclaim.eImcomplete Or lclsClaim.sStaclaim = Claim.Estatclaim.eNull Or lclsClaim.sStaclaim = Claim.Estatclaim.eRefuse Then
					lstrError = lstrError & lstrSep & "55759"
				End If
			End If
		End If
		
		If nServ_Order = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "4055"
		Else
			lclsProf_ord = New eClaim.Prof_ord
			If lclsProf_ord.Find_nServ(nServ_Order) Then
				'+ Si la accion es registrar el estado de la orden debe ser "Asignada - No realizada"
                If nMainAction = eFunctions.Menues.TypeActions.clngActionadd And (lclsProf_ord.nStatus_ord = 3 Or lclsProf_ord.nStatus_ord = 11) Then
                    lstrError = lstrError & lstrSep & "55761"
                End If
            Else
                '+ Si la orden de servicio no existe
                lstrError = lstrError & lstrSep & "4056"
            End If
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
			insValSI775_K = lclsErrors.Confirm
		End If
		
insValSI775_K_Err:
        If Err.Number Then
            insValSI775_K = ""
            insValSI775_K = insValSI775_K & Err.Description
        End If
        On Error GoTo 0
		lclsClaim = Nothing
		lclsErrors = Nothing
		lclsProf_ord = Nothing
	End Function
	
	'% insValSI775: Se realizan las validaciones del frame de Ingreso de presupuesto
	Public Function insValSI775(ByVal sCodispl As String, ByVal nServ_Order As Double, ByVal dBudget_Date As Date, ByVal sClient As String, ByVal nNum_Budget As Double, ByVal nAmount_Labor As Double, ByVal nAmount_Paint As Double, ByVal nAmount_Mechan As Double, ByVal nAmount_Part As Double, ByVal nDeduc_amount As Double, ByVal nMainAction As Short, ByVal nAccept As Short) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProf_ord As eClaim.Prof_ord
		Dim lstrSep As String
        Dim lstrError As String = ""
        Dim lblnFind As Boolean
		
		On Error GoTo insValSI775_Err
		
		lclsErrors = New eFunctions.Errors
		lclsProf_ord = New eClaim.Prof_ord
		
		If lclsProf_ord.Find_nServ(nServ_Order) Then
			lblnFind = True
		End If
		
		lstrSep = "||"
		
        If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            '+ Si la accion es aprobar el estado de la orden debe ser "Realizada" o "Aceptada"
            If nAccept = 1 And (lclsProf_ord.nStatus_ord <> 3 And lclsProf_ord.nStatus_ord <> 7) And 1 = 2 Then
                lstrError = lstrError & lstrSep & "55762"
            End If

            '+Si la accion es rechazar el estado de la orden debe ser "Realizada" o "Aceptada"
            If nAccept = 2 And (lclsProf_ord.nStatus_ord <> 3 And lclsProf_ord.nStatus_ord <> 7) And 1 = 2 Then
                lstrError = lstrError & lstrSep & "55763"
            End If
        Else
            '-Se valida que el campo "Fecha" debe estar lleno.

            If dBudget_Date = eRemoteDB.Constants.dtmNull Then
                lstrError = lstrError & lstrSep & "4377"
            End If

            '-Se valida que el campo "Número" debe estar lleno.

            If nNum_Budget = eRemoteDB.Constants.intNull Or nNum_Budget = 0 Then
                lstrError = lstrError & lstrSep & "55756"
            End If

            '-Se valida que el campo "RUC del taller" debe estar lleno.

            If sClient = String.Empty Then
                lstrError = lstrError & lstrSep & "2792"
            End If

            '-Se valida que los campos de "Valores Netos" deben estar llenos.

            If nAmount_Labor = eRemoteDB.Constants.intNull Or nAmount_Paint = eRemoteDB.Constants.intNull Or nAmount_Mechan = eRemoteDB.Constants.intNull Or nAmount_Part = eRemoteDB.Constants.intNull Then
                lstrError = lstrError & lstrSep & "55758"
            End If

            '+ Si en la orden para la cual se elabora el presupuesto se indicó que tiene pago del deducible en el taller,
            '+ el campo "Deducible a pagar" debe estar lleno.
            If lblnFind Then
                If dBudget_Date <> eRemoteDB.Constants.dtmNull Then
                    If dBudget_Date < lclsProf_ord.dInpdate Then
                        lstrError = lstrError & lstrSep & "60528"
                    End If
                End If

                If lclsProf_ord.sWsdeduc = "1" And (nDeduc_amount = eRemoteDB.Constants.intNull Or nDeduc_amount = 0) Then
                    lstrError = lstrError & lstrSep & "60453"
                End If
            End If
        End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
			insValSI775 = lclsErrors.Confirm
		End If
		
insValSI775_Err: 
		If Err.Number Then
			insValSI775 = "insValSI775:" & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsProf_ord = Nothing
	End Function

    Public Function insValSI775_FromDB(ByVal sCodispl As String, ByVal nServ_Order As Double, ByVal nAmount As Double, ByRef nCapitalDisponbile As Double) As String
        Dim lrecInsValSi775 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        Dim nReservaDisponible As Double

        Dim nIndStatus As Integer
        Dim nIndautorizacion As Integer
        Dim sListas As String

        On Error GoTo InsValSi775_Err

        lrecInsValSi775 = New eRemoteDB.Execute


        With lrecInsValSi775
            .StoredProcedure = "InsValSi775"
            .Parameters.Add("nServ_Order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SERRORLIST", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NRESERVA_DISPONIBLE", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCAPITAL_DISPONIBLE", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("SERRORLIST").Value

            nReservaDisponible = .Parameters("NRESERVA_DISPONIBLE").Value
            nCapitalDisponbile = .Parameters("NCAPITAL_DISPONIBLE").Value

            lobjErrors = New eFunctions.Errors

            If lstrError <> String.Empty Then
                With lobjErrors
                    Call .ErrorMessage(sCodispl,   , ,   , "(" & nReservaDisponible.ToString() & ")", , lstrError)
                    Return lobjErrors.Confirm
                End With
                lobjErrors = Nothing
            End If

        End With
InsValSi775_Err:
        If Err.Number Then
            Return "insValSI775_FromDB: " & Err.Description
        End If
        On Error GoTo 0
        lrecInsValSi775 = Nothing
    End Function

    '% insPostSI775: Se realizan las actualizaciones sobre la tabla "Auto_Budget"
    Public Function insPostSI775(ByVal sCodispl As String, ByVal nMainAction As Short, ByVal nAccept As Short, ByVal nServ_Order As Double, ByVal sClient As String, ByVal nNum_Budget As Double, ByVal dBudget_Date As Date, ByVal nAmount_Labor As Double, ByVal nAmount_Paint As Double, ByVal nAmount_Mechan As Double, ByVal nAmount_Part As Double, ByVal nUsercode As Integer, ByVal nAmount As Double, ByVal nIVA As Double, ByVal nDeduc_amount As Double, ByVal nDeprec_amount As Double, ByVal nAmount_Ajus_Ord As Double) As Boolean
        Dim lclsAuto_Budget As eClaim.Auto_Budget
        Dim lclsFire_budget As eClaim.Fire_budget

        On Error GoTo insPostSI775_Err

        Select Case nMainAction
            Case eFunctions.Menues.TypeActions.clngActionadd
                lclsAuto_Budget = New eClaim.Auto_Budget
                With Me
                    .nServ_Order = nServ_Order
                    .sClient = sClient
                    .nNum_Budget = nNum_Budget
                    .dBudget_Date = dBudget_Date
                    .nAmount_Labor = nAmount_Labor
                    .nAmount_Paint = nAmount_Paint
                    .nAmount_Mechan = nAmount_Mechan
                    .nAmount_Part = nAmount_Part
                    .nUsercode = nUsercode
                    .nAmount = nAmount
                    .nAmount_Ajus_Ord = nAmount_Ajus_Ord * -1
                    insPostSI775 = .Update
                End With
                lclsFire_budget = New eClaim.Fire_budget
                insPostSI775 = lclsFire_budget.Updprof_ord_amount(nServ_Order, nIVA, nUsercode, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nDeduc_amount, nDeprec_amount)

            Case eFunctions.Menues.TypeActions.clngActionUpdate
                lclsAuto_Budget = New eClaim.Auto_Budget
                With Me
                    .nServ_Order = nServ_Order
                    .sClient = sClient
                    .nNum_Budget = nNum_Budget
                    .dBudget_Date = dBudget_Date
                    .nAmount_Labor = nAmount_Labor
                    .nAmount_Paint = nAmount_Paint
                    .nAmount_Mechan = nAmount_Mechan
                    .nAmount_Part = nAmount_Part
                    .nUsercode = nUsercode
                    .nAmount = nAmount
                    .nAmount_Ajus_Ord = nAmount_Ajus_Ord * -1
                    insPostSI775 = .Update
                End With
                lclsFire_budget = New eClaim.Fire_budget
                insPostSI775 = lclsFire_budget.Updprof_ord_amount(nServ_Order, nIVA, nUsercode, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nDeduc_amount, nDeprec_amount)

                '	'+ Si se acepta la orden de servicio
                '	If nAccept = 1 Then
                '		lclsFire_budget = New eClaim.Fire_budget
                '		insPostSI775 = lclsFire_budget.InsUpdProf_ord_status(nServ_Order, 8, nUsercode)
                '		'+ Si se rechaza la orden de servicio
                '	Else
                '		lclsFire_budget = New eClaim.Fire_budget
                '		insPostSI775 = lclsFire_budget.InsUpdProf_ord_status(nServ_Order, 9, nUsercode)
                'End If
                insPostSI775 = True
        End Select

insPostSI775_Err:
        If Err.Number Then
            insPostSI775 = False
        End If
        lclsAuto_Budget = Nothing
        lclsFire_budget = Nothing
    End Function

    '%FindDataAuto: Se obtienen los datos del vehículo asegurado asociado al siniestro
    Public Function FindDataAuto(ByVal nClaim As Double) As Boolean
		
		Dim lclsClaim As eClaim.Claim
		Dim lclsAuto As ePolicy.Automobile
		
		lclsClaim = New eClaim.Claim
		lclsAuto = New ePolicy.Automobile
		
		On Error GoTo FindDataAuto_Err
		
		With lclsClaim
			If .Find(nClaim) Then
				If lclsAuto.Find(.sCerType, .nBranch, .nProduct, .nPolicy, .nCertif, Today) Then
					nYear = lclsAuto.nYear
					sChassis = lclsAuto.sChassis
					If lclsAuto.Find_Tab_au_veh(lclsAuto.sVehCode) Then
						sDesVehBrand = lclsAuto.sDesBrand
						sDesVehModel = lclsAuto.sVehModel1
					End If
				End If
			End If
		End With
		
FindDataAuto_Err: 
		If Err.Number Then
			FindDataAuto = False
		End If
		
		lclsClaim = Nothing
		lclsAuto = Nothing
	End Function
End Class






