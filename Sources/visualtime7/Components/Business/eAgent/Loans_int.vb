Option Strict Off
Option Explicit On
Public Class Loans_int
	'%-------------------------------------------------------%'
	'% $Workfile:: Loans_int.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 31                                       $%'
	'%-------------------------------------------------------%'
	
	'%Propiedades según la tabla 'Loans_int' en el sistema 19/12/2001 02:52:37 p.m.
	
	'%       Column name              Type
	'%  ------------------------- ------------
	Public nIntermed As Integer
	Public nLoan As Integer
	Public dCompdate As Date
	Public nCuoMonth As Double
	Public nCurrency As Integer
	Public dDate_pay As Date
	Public dDateLoan As Date
	Public nFor_pay As Integer
	Public nAmoLoan As Double
	Public nAmoLastPay As Double
	Public nRate_int As Double
	Public nRate_ret As Double
	Public nRequest_nu As Integer
	Public nBalanLoan As Double
	Public nTypeLoan As Integer
	Public nUsercode As Integer
	Public sStatLoan As String
	Public sPayOrder As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	
	Public nCodmodpay As Integer
	Public nCommBase As Double
	Public nLoan_perc As Double
	Public nAmount_loans As Double
	Public nCurr_amount As Integer
	'- Variable para el manejo de la tabla TMP_AGLl004
	Public sKey As String
	
	'- Variables de tabla comm_pol
	Public nId As Integer
	Public nTypMov_Comm As Integer
	Public sTyp_Comm As String
	Public nTotal_Com As Double
	
	
	'- Variables que indica si la poliza es de Rentas Vitalicias
	'- 1.- Es de Renta Vitalicia
	Public nRentVita As Short
	
	Enum eTypLoanType
		clngLoan = 1 'Préstamo
		clngAdvanced = 2 'Anticipo
		CLNGSAVECIRCLE = 3 'Círculo cerrado de ahorro
		clngLoanPartner = 4 'Préstamo a socio
		clngLoanPayAOSS = 5 'Préstamo por pago AOSS
	End Enum
	
	'% Find: Busca la información de un determinado intermediario
	Public Function Find(ByVal IntermediaCode As Integer, ByVal Loan As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLoans_int As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If IntermediaCode = nIntermed And Loan = nLoan And Not lblnFind Then
			Find = True
		Else
			lrecreaLoans_int = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'insudb.reaLoans_int'
			
			With lrecreaLoans_int
				.StoredProcedure = "reaLoans_int"
				.Parameters.Add("nIntermed", IntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLoan", Loan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					nIntermed = IntermediaCode
					nLoan = Loan
					nCuoMonth = .FieldToClass("nCuoMonth")
					nCurrency = .FieldToClass("nCurrency")
					dDate_pay = .FieldToClass("dDate_pay")
					dDateLoan = .FieldToClass("dDateLoan")
					nFor_pay = .FieldToClass("nFor_pay")
					nAmoLoan = .FieldToClass("nAmoLoan")
					nAmoLastPay = .FieldToClass("nAmoLastPay")
					nRate_int = .FieldToClass("nRate_int")
					nRate_ret = .FieldToClass("nRate_ret")
					nRequest_nu = .FieldToClass("nRequest_nu")
					nBalanLoan = .FieldToClass("nBalanLoan")
					nTypeLoan = .FieldToClass("nTypeLoan")
					sStatLoan = .FieldToClass("sStatLoan")
					sPayOrder = .FieldToClass("sPayOrder")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nPolicy = .FieldToClass("nPolicy")
					nCommBase = .FieldToClass("nCommBase")
					nLoan_perc = .FieldToClass("nLoan_perc")
					nCodmodpay = .FieldToClass("nCodmodpay")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaLoans_int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLoans_int = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% ADD: Este método se encarga de agregar nuevos registros a la tabla 'Loans_int'. Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreLoans_int As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreLoans_int = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creLoans_int'
		With lreccreLoans_int
			.StoredProcedure = "creLoans_int"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan", nLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateLoan", dDateLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmoLoan", nAmoLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalanLoan", nBalanLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreLoans_int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLoans_int = Nothing
	End Function
	
	'% Update: Este método se encarga de actualizar registros en la tabla 'Loans_int'. Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdLoans_int As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecupdLoans_int = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecupdLoans_int
			.StoredProcedure = "updLoans_int"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan", nLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCuoMonth", nCuoMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_pay", dDate_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateLoan", dDateLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFor_pay", nFor_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmoLoan", nAmoLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmoLastPay", nAmoLastPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_int", nRate_int, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_ret", nRate_ret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalanLoan", nBalanLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeLoan", nTypeLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPayOrder", sPayOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatLoan", sStatLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodmodpay", nCodmodpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommBase", nCommBase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan_perc", nLoan_perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdLoans_int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLoans_int = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% Delete: Este método se encarga de eliminar registros en la tabla 'Loans_int'. Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete(ByVal IntermediaCode As Integer, ByVal Loan As Integer, ByVal dDateLoan As Date, ByVal nAmoLoan As Double, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecdelLoans_int As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelLoans_int = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delLoans_int'
		
		With lrecdelLoans_int
			.StoredProcedure = "delLoans_int"
			.Parameters.Add("nIntermed", IntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan", Loan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateLoan", dDateLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmoLoan", nAmoLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelLoans_int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLoans_int = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%New_Number. Este metodo devuelve el ultimo numero asignado al intermediario en la tabla "Loans_int"
	Public ReadOnly Property New_Number() As Integer
		Get
			Dim lrecreaLoans_intNumLoan As eRemoteDB.Execute
			
			On Error GoTo New_Number_Err
			lrecreaLoans_intNumLoan = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'insudb.reaLoans_intNumLoan'
			
			With lrecreaLoans_intNumLoan
				.StoredProcedure = "reaLoans_intNumLoan"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNumber", -1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					New_Number = .Parameters("nNumber").Value
				Else
					New_Number = -1
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaLoans_intNumLoan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLoans_intNumLoan = Nothing
			
New_Number_Err: 
			If Err.Number Then
				New_Number = False
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'%InsValAG004_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "AG004"
	Public Function insValAG004_k(ByVal nAction As Integer, ByVal nIntermed As Integer, ByVal nLoanId As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsAgent As eAgent.Intermedia
		Dim lintTypeLoan As Integer
		Static lstrValField As String
		
		lerrTime = New eFunctions.Errors
		lclsAgent = New eAgent.Intermedia
		
		On Error GoTo insValAG004_k_Err
		
		insValAG004_k = String.Empty
		
		'+Validacion del campo: Intermediario.
		'+ Si el campo intermediario está vacío.
		
		If nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage("AG004_K", 9036)
		End If
		
		'+Validacion del campo: Préstamo.
		'+Si no hay error se envia el mensaje del préstamo.
		If insValAG004_k = String.Empty Then
			'+ Si no se está registrando.
			If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
				'+ Si el número del préstamo está vacío.
				If nLoanId = 0 Or nLoanId = eRemoteDB.Constants.intNull Then
					Call lerrTime.ErrorMessage("AG004", 9035)
				Else
					'+ Se valida que el número de préstamo esté registrado en el sistema.
					If Not valLoans_int(nIntermed, nLoanId) Then
						Call lerrTime.ErrorMessage("AG004", 9034)
					End If
				End If
			End If
		End If
		
		'+ Si la acción es actualizar.
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActioncut Then
			If Find(nIntermed, nLoanId) Then
				'+ El préstamo no debe estar en proceso de pagos (saldo = importe).
				If Me.nAmoLoan <> Me.nBalanLoan Then
					Call lerrTime.ErrorMessage("AG004", 9032)
				End If
				'+Si está anulado y la acción es eliminar
				If Me.sStatLoan = "5" And nAction = eFunctions.Menues.TypeActions.clngActioncut Then
					Call lerrTime.ErrorMessage("AG004", 7252)
				End If
			End If
		End If
		
		insValAG004_k = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent = Nothing
		
insValAG004_k_Err: 
		If Err.Number Then
			insValAG004_k = insValAG004_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% valLoans_int: Rutina que valida la existencia de un intermediario, cuyo código es pasado como parámetro.
	Public Function valLoans_int(ByVal llngIntermed As Integer, Optional ByVal lintLoan As Integer = -1) As Boolean
		Dim lrecLoans_int As New eRemoteDB.Execute
		
		On Error GoTo valLoans_int_Err
		
		If lintLoan = -1 Then
			lrecLoans_int.StoredProcedure = "valLoans_int_a"
			lrecLoans_int.Parameters.Add("nIntermed", llngIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			lrecLoans_int.StoredProcedure = "valLoans_int_o"
			lrecLoans_int.Parameters.Add("nIntermed", llngIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecLoans_int.Parameters.Add("nLoan", lintLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		
		If lrecLoans_int.Run Then
			If lrecLoans_int.FieldToClass("Count") > 0 Then
				valLoans_int = True
			Else
				valLoans_int = False
			End If
		Else
			valLoans_int = False
		End If
valLoans_int_Err: 
		If Err.Number Then
			valLoans_int = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValAG004: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'% forma.
	Public Function insValAG004(ByVal nAction As Integer, ByVal nIntermed As Integer, ByVal dEffecdate As Date, ByVal nLoanType As eTypLoanType, ByVal nLoanSta As String, ByVal nCurrency As Integer, ByVal nLoanBalance As Double, ByVal nLoanAmount As Double, ByVal nPayForm As Integer, ByVal nPayOrder As Integer, ByVal nPercent As Double, ByVal nInterest As Double, ByVal nMonthly As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCodmodpay As Integer, ByVal nCommBase As Double, ByVal nCurrCommBase As Integer, ByVal nLoan_perc As Double, ByVal nUsercode As Integer, ByVal sCodispl As String) As String
		Dim lrecinsValAG004 As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrors As String = ""

        '+Definición de parámetros para stored procedure 'InsValOP001'
        '+Información leída el 10/04/2003
        On Error GoTo insValAG004_Err
		lrecinsValAG004 = New eRemoteDB.Execute
		lclsErrors = New eFunctions.Errors
		
		With lrecinsValAG004
			.StoredProcedure = "insValAG004"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoanType", nLoanType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLoanSta", CStr(nLoanSta), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoanBalance", nLoanBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoanAmount", nLoanAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayForm", nPayForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayOrder", nPayOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthly", nMonthly, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", IIf(nBranch = eRemoteDB.Constants.intNull, 0, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodmodpay", IIf(nCodmodpay = eRemoteDB.Constants.intNull, 0, nCodmodpay), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommBase", nCommBase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrCommBase", nCurrCommBase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan_perc", nLoan_perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrors)
		
		insValAG004 = lclsErrors.Confirm
		
insValAG004_Err: 
		If Err.Number Then
			insValAG004 = "InsValAG004: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lrecinsValAG004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValAG004 = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'%insPostAG004: Rutina que actualiza un préstamo de un determinado intermediario.
	Public Function insPostAG004(ByVal nMainAction As Integer, ByVal nIntermed As Integer, ByVal nLoanId As Integer, ByVal nMonthly As Double, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nPayForm As Integer, ByVal nLoanAmount As Double, ByVal nLoanBalance As Double, ByVal nInterest As Double, ByVal nPercent As Double, ByVal nReqCheq As Integer, ByVal nLoanType As Integer, ByVal nUsercode As Integer, ByVal nPayOrder As String, ByVal nLoanSta As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCodmodpay As Integer, ByVal nCommBase As Double, ByVal nLoan_perc As Double) As Boolean
		Dim lstrPayOrder As String
		
		On Error GoTo insPostAG004_Err
		lstrPayOrder = CStr(nPayOrder)
		
		With Me
			.nIntermed = nIntermed
			.nLoan = nLoanId
			.nCuoMonth = nMonthly
			.nCurrency = nCurrency
			.dDate_pay = dtmNull
			.dDateLoan = dEffecdate
			.nFor_pay = nPayForm
			.nAmoLoan = nLoanAmount
			.nAmoLastPay = nLoanBalance
			.nRate_int = nInterest
			.nRate_ret = nPercent
			.nRequest_nu = nReqCheq
			.nBalanLoan = nLoanBalance
			.nTypeLoan = nLoanType
			.nUsercode = nUsercode
			.sPayOrder = nPayOrder
			.sStatLoan = CStr(nLoanSta)
			.nBranch = IIf(nBranch = 0, eRemoteDB.Constants.intNull, nBranch)
			.nProduct = IIf(nProduct = 0, eRemoteDB.Constants.intNull, nProduct)
			.nPolicy = IIf(nPolicy = 0, eRemoteDB.Constants.intNull, nPolicy)
			.nCodmodpay = IIf(nCodmodpay = 0, eRemoteDB.Constants.intNull, nCodmodpay)
			.nCommBase = IIf(nCommBase = 0, eRemoteDB.Constants.intNull, nCommBase)
			.nLoan_perc = IIf(nLoan_perc = 0, eRemoteDB.Constants.intNull, nLoan_perc)
			
			Select Case nMainAction
				Case 301
					insPostAG004 = .Add
					insPostAG004 = .Update
				Case 302
					insPostAG004 = .Update
				Case 303
					insPostAG004 = .Delete(nIntermed, nLoanId, dEffecdate, nLoanAmount, nCurrency, nUsercode)
			End Select
		End With
		
insPostAG004_Err: 
		If Err.Number Then
			insPostAG004 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		dDateLoan = Today
		nAmoLoan = CDbl(Nothing)
		nBalanLoan = CDbl(Nothing)
		nCodmodpay = CInt(Nothing)
		nCommBase = CDbl(Nothing)
		nLoan_perc = CDbl(Nothing)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'% Find_Commiss: Calcula el porcentaje de anticipo a otorgar al intermediario
	Public Function Find_Commiss(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nIntermed As Integer, ByVal nCurrLoan As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lreaPremiumCommiss As eRemoteDB.Execute
		
		On Error GoTo Find_Commiss_Err
		
		lreaPremiumCommiss = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.lreaPremiumCommiss'
		'+Información leída el 01/08/2002
		
		With lreaPremiumCommiss
			.StoredProcedure = "Inscalpremium_commiss_pr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrLoan", nCurrLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_loans", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurr_amount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRentVita", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nAmount").Value > 0 Then
					Find_Commiss = True
					nCommBase = .Parameters("nAmount").Value
					nAmount_loans = .Parameters("nAmount_loans").Value
					nCurr_amount = .Parameters("nCurr_amount").Value
					nRentVita = .Parameters("nRentVita").Value
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lreaPremiumCommiss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaPremiumCommiss = Nothing
		
Find_Commiss_Err: 
		If Err.Number Then
			Find_Commiss = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindComm_Pol: Rescta los datos desde tabla comm_pol
	Public Function FindComm_Pol(ByVal nIntermed As Double, ByVal nCurrency As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTypMov_Comm As Integer) As Boolean
		Dim lrecFindComm_Pol As eRemoteDB.Execute
		
		On Error GoTo FindComm_Pol_Err
		
		lrecFindComm_Pol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecFindComm_Pol
			.StoredProcedure = "reaComm_Pol"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypMov_Comm", nTypMov_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				nId = .FieldToClass("nid")
				nTypMov_Comm = .FieldToClass("nTypMov_Comm")
				sTyp_Comm = .FieldToClass("sTyp_Comm")
				nTotal_Com = .FieldToClass("nTotal_Com")
				.RCloseRec()
				FindComm_Pol = True
			Else
				FindComm_Pol = False
			End If
		End With
		
FindComm_Pol_Err: 
		If Err.Number Then
			FindComm_Pol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFindComm_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindComm_Pol = Nothing
	End Function
	
	'% InsCreTmp_Agl004: crea la tabla temporal para el reporte de Anticipos de Comisión
	Public Function InsCreTmp_Agl004(ByVal nIntermed As Integer, ByVal nLoanId As Integer) As Boolean
		Dim lreaPremiumCommiss As eRemoteDB.Execute
		
		On Error GoTo InsCreTmp_Agl004_Err
		
		lreaPremiumCommiss = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.lreaPremiumCommiss'
		'+Información leída el 01/08/2002
		
		With lreaPremiumCommiss
			.StoredProcedure = "InsCreTmp_Agl004"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoanId", nLoanId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("skey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsCreTmp_Agl004 = True
				sKey = .Parameters("skey").Value
			End If
		End With
		'UPGRADE_NOTE: Object lreaPremiumCommiss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaPremiumCommiss = Nothing
		
InsCreTmp_Agl004_Err: 
		If Err.Number Then
			InsCreTmp_Agl004 = False
		End If
		On Error GoTo 0
	End Function
End Class






