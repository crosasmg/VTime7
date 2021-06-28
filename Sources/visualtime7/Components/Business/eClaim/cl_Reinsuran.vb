Option Strict Off
Option Explicit On
Public Class cl_Reinsuran
	'%-------------------------------------------------------%'
	'% $Workfile:: cl_Reinsuran.cls                         $%'
	'% $Author:: Nvaplat31                                  $%'
	'% $Date:: 21/11/03 18:06                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                    Type       Length      Prec  Scale Nullable
	Public nBranch_Rei As Integer
	Public nClaim As Double
	Public nModulec As Integer
	Public nCase_num As Integer
	Public nType_Rein As Integer
	Public nDeman_type As Integer
	Public nCover As Integer
	Public sClient As String
	Public dEffecdate As Date
	Public nCompany As Integer
	Public dAcceDate As Date
	Public nCapital As Double
	Public nCommissi As Double
	Public dCompdate As Date
	Public nCurrency As Integer
	Public sHeap_code As String
	Public nInter_rate As Double
	Public nNumber As Integer
	Public nReser_rate As Double
	Public nShare As Double
	Public nUsercode As Integer
	Public nChange As Integer
	Public nAcep_code As Double
	
	'+Variables auxiliares para controlar los montos
	Public nLoc_reserv As Double
	Public nReserv_Pend As Double
	Public nPay_amount As Double
	Public nLoc_rec_am As Double
	Public nLoc_cos_re As Double
	Public nloc_Reserv_p As Double
	Public nReserv_pend_p As Double
	Public nPay_amount_p As Double
	Public nLoc_rec_am_p As Double
	Public nLoc_cos_re_p As Double
	Public sSel As String
	Public sDesType_Rein As String
	Public sCompany As String
	
	'+Variable para controlar la accion a realizar
	Public nAction As Integer
	
	
	
	'**% insValSI749Upd: make the validation to SI749
	'% insValSI749Upd: se realizan las validaciones a la SI749
	Public Function insValSI749Upd(ByVal sCodispl As String, ByVal nShare As Double, ByVal nType_Rein As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValSI749Upd_Err
		
		'**+ Validation of the field "Percent"
		'+Validacion del campo "Porcentaje"
		
		If nShare = 0 Or nShare = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3069)
		ElseIf nType_Rein = 1 Then 
			If nShare > 100 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11239)
			End If
		ElseIf nShare >= 100 Then 
			Call lclsErrors.ErrorMessage(sCodispl, 9992)
		End If
		
		insValSI749Upd = lclsErrors.Confirm
		
insValSI749Upd_Err: 
		If Err.Number Then
			insValSI749Upd = insValSI749Upd & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValSI749: make the validation to SI749
	'% insValSI749: se realizan las validaciones a la SI749
	Public Function insValSI749(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValSI749_Err
		
		If nCover = 0 Or nCover = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10235)
		Else
			If Not insValidate_Cl_Reinsuran(nClaim, nCase_num, nDeman_type, dEffecdate, nCover) Then
				Call lclsErrors.ErrorMessage(sCodispl, 3070)
			End If
		End If
		
		insValSI749 = lclsErrors.Confirm
		
insValSI749_Err: 
		If Err.Number Then
			insValSI749 = insValSI749 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValShareTotal: make the validation to share
	'% insValShareTotal: se realizan las validaciones del campo porcentaje
	Public Function insValShareTotal(ByVal sCodispl As String, ByVal nShareTotal As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValShareTotal_Err
		
		If nShareTotal <> 100 And nShareTotal <> 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3070)
		End If
		
		insValShareTotal = lclsErrors.Confirm
		
insValShareTotal_Err: 
		If Err.Number Then
			insValShareTotal = insValShareTotal & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% InsPostSI749: se actualizan los datos de la tabla cl_Reinsuran
	Public Function InsPostSI749(ByVal sAction As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCompany As Integer, ByVal nShare As Double, ByVal nBranch_Rei As Integer, ByVal nModulec As Integer, ByVal nType_Rein As Integer, ByVal sClient As String, ByVal nUsercode As Integer, Optional ByVal dAcceDate As Date = #12:00:00 AM#, Optional ByVal nCapital As Double = 0, Optional ByVal nCommissi As Double = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sHeap_code As String = "", Optional ByVal nInter_rate As Double = 0, Optional ByVal nNumber As Double = 0, Optional ByVal nReser_rate As Double = 0, Optional ByVal nChange As Integer = 0, Optional ByVal nAcep_code As Integer = 0, Optional ByVal sSel As String = "") As Boolean
		
		Dim lclsClaim_win As eClaim.Claim_win
		lclsClaim_win = New eClaim.Claim_win
		
		On Error GoTo InsPostSI749_Err
		
		Me.nAction = 0
		If sAction = "Update" Then
			Me.nAction = 2
		ElseIf sAction = "Add" Then 
			Me.nAction = 1
		ElseIf sAction = "Delete" Then 
			Me.nAction = 3
		End If
		
		Me.nClaim = nClaim
		Me.nCase_num = nCase_num
		Me.nDeman_type = nDeman_type
		Me.nCover = nCover
		Me.dEffecdate = dEffecdate
		Me.nCompany = nCompany
		Me.nShare = nShare
		Me.nBranch_Rei = nBranch_Rei
		Me.nModulec = nModulec
		Me.nType_Rein = nType_Rein
		Me.sClient = sClient
		Me.nUsercode = nUsercode
		Me.dAcceDate = dAcceDate
		Me.nCapital = nCapital
		Me.nCommissi = nCommissi
		Me.nCurrency = nCurrency
		Me.sHeap_code = sHeap_code
		Me.nInter_rate = nInter_rate
		Me.nNumber = nNumber
		Me.nReser_rate = nReser_rate
		Me.nChange = nChange
		Me.nAcep_code = nAcep_code
		Me.sSel = sSel
		
		If sSel = "1" Then
			If Me.nAction <> 0 Then
				InsPostSI749 = insUpdcl_Reinsuran
			Else
				Me.nAction = 2
				InsPostSI749 = insUpdcl_Reinsuran
			End If
		Else
			If Me.nAction <> 0 Then
				InsPostSI749 = insUpdcl_Reinsuran
			Else
				Me.nAction = 3
				InsPostSI749 = insUpdcl_Reinsuran
			End If
		End If
		
		lclsClaim_win = Nothing
		
InsPostSI749_Err: 
		If Err.Number Then
			InsPostSI749 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'+Actualiza los registros que fueron seleccionados en el grid
	Public Function insUpdcl_Reinsuran() As Boolean
		Dim lrecCl_Reinsuran As eRemoteDB.Execute
		
		On Error GoTo insUpdcl_Reinsuran_Err
		lrecCl_Reinsuran = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insClaim_pay'
		'Información leída el 29/01/2001 6:26:35 PM
		
		With lrecCl_Reinsuran
			.StoredProcedure = "insUpdate_cl_Reinsuran"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_Rei", nBranch_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Rein", nType_Rein, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAccedate", dAcceDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommissi", nCommissi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHeap_code", sHeap_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInter_Rate", nInter_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReser_Rate", nReser_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShare", nShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChange", nChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcep_Code", nAcep_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdcl_Reinsuran = .Run(False)
			
		End With
		lrecCl_Reinsuran = Nothing
		
insUpdcl_Reinsuran_Err: 
		If Err.Number Then
			insUpdcl_Reinsuran = False
		End If
		On Error GoTo 0
	End Function
	
	'+insValidate_Cl_Reinsuran : Verifica que los porcentajes que se encuentren en cl_reinsuran sean el 100%
	Public Function insValidate_Cl_Reinsuran(ByVal ldblClaim As Double, ByVal llngCase_num As Integer, ByRef llngDeman_type As Integer, ByVal ldtmEffecdate As Date, ByVal llngCover As Integer) As Boolean
		Dim lrecCl_Reinsuran As eRemoteDB.Execute
		
		On Error GoTo insValidate_Cl_Reinsuran_Err
		lrecCl_Reinsuran = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insValidate_Cl_Reinsuran'
		'+Información leída el 29/01/2001 6:26:35 PM
		With lrecCl_Reinsuran
			.StoredProcedure = "insValidate_Cl_Reinsuran"
			.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", llngCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", llngDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", llngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insValidate_Cl_Reinsuran = Not .Run(True)
			
		End With
		lrecCl_Reinsuran = Nothing
		
insValidate_Cl_Reinsuran_Err: 
		If Err.Number Then
			insValidate_Cl_Reinsuran = False
		End If
		On Error GoTo 0
	End Function
	'+Find_Cl_Reinsuran_Cover : Busca una cobertura en cl reinsuran
	Public Function Find_Cl_Reinsuran_Cover(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecCl_Reinsuran As eRemoteDB.Execute
		
		On Error GoTo Find_Cl_Reinsuran_Cover_Err
		lrecCl_Reinsuran = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insClaim_pay'
		'+Información leída el 29/01/2001 6:26:35 PM
		With lrecCl_Reinsuran
			.StoredProcedure = "Rea_Cl_Reinsuran_Cover"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Cl_Reinsuran_Cover = True
				nModulec = .FieldToClass("nModulec")
				sClient = .FieldToClass("sClient")
				nCover = .FieldToClass("nCover")
				nBranch_Rei = .FieldToClass("nBranch_Rei")
				nType_Rein = .FieldToClass("nType_Rein")
				nCompany = .FieldToClass("nCompany")
				nCurrency = .FieldToClass("nCurrency")
			Else
				Find_Cl_Reinsuran_Cover = False
			End If
		End With
		lrecCl_Reinsuran = Nothing
		
Find_Cl_Reinsuran_Cover_Err: 
		If Err.Number Then
			Find_Cl_Reinsuran_Cover = False
		End If
		On Error GoTo 0
	End Function
	
	Private Sub Class_Initialize_Renamed()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	Private Sub Class_Terminate_Renamed()
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






