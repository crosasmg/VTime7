Option Strict Off
Option Explicit On
Public Class Fire_budget
	'%-------------------------------------------------------%'
	'% $Workfile:: Fire_budget.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 3/11/03 21.46                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.fire_budget al 08-13-2003 00:59:35
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0     10   N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public dBudg_date As Date ' DATE       7    0     0    S
	Public sItem As String ' CHAR       60   0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nAmount As Double ' NUMBER     22   6     18   S
	Public nNumimages As Double ' NUMBER     22   0     10   S
	Public nNotenum As Double ' NUMBER     22   0     10   S
	'
	'+ Campos de tabla PROF_ORD
	Public nIVA As Double
	Public nMat_amount As Double
	Public nHand_amount As Double
	Public nDeduc_amount As Double
	Public nDeprec_amount As Double
	Public nExist As Integer
	'
	
	'%Find : Esta función se encarga de de buscar el registro en Fire_budget - SI775_A.aspx
	Public Function Find_Budget(ByVal nServ_Order As Double, ByVal nClaim As Double) As Boolean
		Dim lrecreaFire_budget As eRemoteDB.Execute
		
		On Error GoTo Find_Budget_Err
		
		lrecreaFire_budget = New eRemoteDB.Execute
		
		With lrecreaFire_budget
			.StoredProcedure = "reaFire_budget_pord"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nServ_Order = nServ_Order
				dBudg_date = .FieldToClass("dBudg_date")
				sItem = .FieldToClass("sItem")
				nAmount = .FieldToClass("nAmount")
				nNumimages = .FieldToClass("nNumimages")
				nNotenum = .FieldToClass("nNotenum")
				nIVA = .FieldToClass("nIva")
				nMat_amount = .FieldToClass("nMat_amount")
				nHand_amount = .FieldToClass("nHand_amount")
				nDeduc_amount = .FieldToClass("nDeduc_amount")
				nDeprec_amount = .FieldToClass("nDeprec_amount")
				nExist = .FieldToClass("nExist")
				Find_Budget = True
			Else
				Find_Budget = False
			End If
		End With
		
Find_Budget_Err: 
		If Err.Number Then
			Find_Budget = False
		End If
		On Error GoTo 0
		lrecreaFire_budget = Nothing
	End Function
	
	'%Find : Esta función se encarga de de buscar el registro en Fire_budget
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaFire_budget As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaFire_budget = New eRemoteDB.Execute
		
		With lrecreaFire_budget
			.StoredProcedure = "reaFire_budget"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nServ_Order = nServ_Order
				dBudg_date = .FieldToClass("dBudg_date")
				sItem = .FieldToClass("sItem")
				nAmount = .FieldToClass("nAmount")
				nNumimages = .FieldToClass("nNumimages")
				nNotenum = .FieldToClass("nNotenum")
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecreaFire_budget = Nothing
	End Function
	
	'% insValSI775_A: Validaciones pertinentes al cuerpo de la transacción SI775_A
	Public Function insValSI775_A(ByVal sCodispl As String, ByVal nServ_Order As Double, ByVal sItem As String, ByVal nAmount As Double, ByVal nMainAction As Short, ByVal nAccept As Short) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsProf_ord As eClaim.Prof_ord
		
		On Error GoTo insValSI775_A_Err
		
		If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			lclsProf_ord = New eClaim.Prof_ord
			If lclsProf_ord.Find_nServ(nServ_Order) Then
				'+ Si la accion es aprobar el estado de la orden debe ser "Realizada" o "Aceptada"
				If nAccept = 1 And (lclsProf_ord.nStatus_ord <> 3 And lclsProf_ord.nStatus_ord <> 7) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55762)
				End If
				
				'+Si la accion es rechazar el estado de la orden debe ser "Realizada" o "Aceptada"
				If nAccept = 2 And (lclsProf_ord.nStatus_ord <> 3 And lclsProf_ord.nStatus_ord <> 7) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55763)
				End If
			End If
		Else
			If nAmount = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  ,  , "Monto Total")
			End If
			If sItem = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  ,  , "Descripción")
			End If
		End If
		insValSI775_A = lclsErrors.Confirm
		
insValSI775_A_Err: 
		If Err.Number Then
			insValSI775_A = "insValSI775_A: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsProf_ord = Nothing
	End Function
	
	'%InsPostSI775_A: Ejecuta el post de la transacción
	'%                Ingreso de presupuesto para incendio(SI775_A)
	Public Function InsPostSI775_A(ByVal sAction As String, ByVal nServ_Order As Double, ByVal dBudg_date As Date, ByVal nNotenum As Double, ByVal nImagesNum As Double, ByVal sItem As String, ByVal nAmount As Double, ByVal nIVA As Double, ByVal nTotal As Double, ByVal nUsercode As Integer, ByVal nAccept As Short, ByVal nMat_amount As Double, ByVal nHand_amount As Double, ByVal nDeduc_amount As Double, ByVal nDeprec_amount As Double, ByVal nMainAction As Short) As Boolean
		On Error GoTo InsPostSI775_A_Err
		
		With Me
			.nServ_Order = nServ_Order
			.dBudg_date = dBudg_date
			.nNotenum = nNotenum
			.nNumimages = nImagesNum
			.sItem = sItem
			.nAmount = nAmount
			.nIVA = nIVA
			.nUsercode = nUsercode
			.nMat_amount = nMat_amount
			.nHand_amount = nHand_amount
			.nDeduc_amount = nDeduc_amount
			.nDeprec_amount = nDeprec_amount
		End With
		
		Select Case nMainAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				InsPostSI775_A = InsUpdProf_ord_status(nServ_Order, 3, nUsercode)
				If sAction = "Add" Then
					InsPostSI775_A = InsUpdFire_budget(1)
				ElseIf sAction = "Update" Then 
					InsPostSI775_A = InsUpdFire_budget(2)
				End If
				InsPostSI775_A = Updprof_ord_amount(nServ_Order, nIVA, nUsercode, nMat_amount, nHand_amount, nDeduc_amount, nDeprec_amount)
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				'+ Si se aprueba la orden de servicios profesionales
				If nAccept = 1 Then
					InsPostSI775_A = InsUpdProf_ord_status(nServ_Order, 8, nUsercode)
					'+ Si se rechaza la orden de servicios profesionales
				ElseIf nAccept = 2 Then 
					InsPostSI775_A = InsUpdProf_ord_status(nServ_Order, 9, nUsercode)
				End If
		End Select
		
InsPostSI775_A_Err: 
		If Err.Number Then
			InsPostSI775_A = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdFire_budget: Se encarga de actualizar la tabla Fire_budget
	Private Function InsUpdFire_budget(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdFire_budget As eRemoteDB.Execute
		
		On Error GoTo InsUpdFire_budget_Err
		
		lrecInsUpdFire_budget = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdadjacence al 04-25-2002 17:55:43
		With lrecInsUpdFire_budget
			.StoredProcedure = "insUpdFire_budget"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBudg_date", dBudg_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumimages", nNumimages, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sItem", sItem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdFire_budget = .Run(False)
		End With
		
InsUpdFire_budget_Err: 
		If Err.Number Then
			InsUpdFire_budget = False
		End If
		On Error GoTo 0
		lrecInsUpdFire_budget = Nothing
	End Function
	
	'%InsValBranch_prof_ord: Valida que el ramo sea de incendio y el siniestro
	'%                    Ingreso de presupuesto para incendio(SI775_A)
	Public Function InsValBranch_prof_ord(ByVal nServ_Order As Double, ByVal nClaim As Double) As Boolean
		Dim lrecInsValBranch_prof_ord As eRemoteDB.Execute
		
		On Error GoTo InsValBranch_prof_ord_Err
		
		lrecInsValBranch_prof_ord = New eRemoteDB.Execute
		
		'+ Definición de store procedure InsValBranch_prof_ord
		With lrecInsValBranch_prof_ord
			.StoredProcedure = "InsValBranch_prof_ord"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValBranch_prof_ord = .Parameters("nExists").Value = 1
			Else
				InsValBranch_prof_ord = False
			End If
		End With
		
InsValBranch_prof_ord_Err: 
		If Err.Number Then
			InsValBranch_prof_ord = False
		End If
		On Error GoTo 0
		lrecInsValBranch_prof_ord = Nothing
	End Function
	
	'%InsUpdProf_ord_status: Actualiza estado de la orden de servicio
	Public Function InsUpdProf_ord_status(ByVal nServ_Order As Double, ByVal nStatus_ord As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsUpdProf_ord_status As eRemoteDB.Execute
		
		On Error GoTo InsUpdProf_ord_status_Err
		
		lrecInsUpdProf_ord_status = New eRemoteDB.Execute
		
		'+ Definición de store procedure InsValBranch_prof_ord
		With lrecInsUpdProf_ord_status
			.StoredProcedure = "UpdProf_ord_status"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdProf_ord_status = .Run(False)
		End With
		
InsUpdProf_ord_status_Err: 
		If Err.Number Then
			InsUpdProf_ord_status = False
		End If
		On Error GoTo 0
		lrecInsUpdProf_ord_status = Nothing
	End Function
	'% Updprof_ord_amount: Actualiza el iva en la tabla prof_ord
	Public Function Updprof_ord_amount(ByVal nServ_Order As Double, ByVal nIVA As Double, ByVal nUsercode As Integer, ByVal nMat_amount As Double, ByVal nHand_amount As Double, ByVal nDeduc_amount As Double, ByVal nDeprec_amount As Double) As Boolean
		Dim lrecUpdprof_ord_amount As eRemoteDB.Execute
		
		On Error GoTo Updprof_ord_amount_Err
		
		lrecUpdprof_ord_amount = New eRemoteDB.Execute
		
		With lrecUpdprof_ord_amount
			.StoredProcedure = "Updprof_ord_amount"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", nIVA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMat_amount", nMat_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHand_amount", nHand_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc_amount", nDeduc_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeprec_amount", nDeprec_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Updprof_ord_amount = .Run(False)
		End With
		
Updprof_ord_amount_Err: 
		If Err.Number Then
			Updprof_ord_amount = False
		End If
		On Error GoTo 0
		lrecUpdprof_ord_amount = Nothing
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		dBudg_date = eRemoteDB.Constants.dtmNull
		sItem = String.Empty
		nAmount = eRemoteDB.Constants.intNull
		nNumimages = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nIVA = eRemoteDB.Constants.intNull
		nMat_amount = eRemoteDB.Constants.intNull
		nHand_amount = eRemoteDB.Constants.intNull
		nDeduc_amount = eRemoteDB.Constants.intNull
		nDeprec_amount = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






