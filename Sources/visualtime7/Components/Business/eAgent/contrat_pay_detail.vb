Option Strict Off
Option Explicit On
Public Class contrat_pay_detail
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrat_Pay_Detail.cls                   $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name           Type
	'-------------------------------
	Public nContrat_Pay As Integer
	
	Public sClient As String
	Public sDescript As String
	Public dStartdate As Date
	Public nType_Calc As Integer
	Public nPercent As Double
	Public nAmount As Double
	Public nCurrency As Integer
	Public nAply As Integer
	Public sTaxin As String
	Public sStatregt As String
	
	Public nSeq As Double
	Public nCode As Integer
	Public nInit_Dur As Integer
	Public nEnd_Dur As Integer
	Public nPercent_detail As Double
	Private mlngUsercode As Integer
	
	'%IsExist: Valida la existencia de un código.
	Public Function IsExist_contrat(ByVal nContrat_Pay As Integer) As Boolean
		Dim lrecContrat_Pay As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecContrat_Pay = New eRemoteDB.Execute
		With lrecContrat_Pay
			.StoredProcedure = "valContrat_Pay"
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist_contrat = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist_contrat = False
		End If
		'UPGRADE_NOTE: Object lrecContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecContrat_Pay = Nothing
		On Error GoTo 0
	End Function
	
	'%IsExist_nEnd_Dur: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%                   tabla "Contrat_Pay_Detail"
	Public Function IsExist_nEnd_Dur(ByVal nContrat_Pay As Integer, ByVal nCode As Integer, ByVal nEnd_Dur As Integer) As Boolean
		Dim lrecreaContrat_Pay_Detail As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecreaContrat_Pay_Detail = New eRemoteDB.Execute
		With lrecreaContrat_Pay_Detail
			.StoredProcedure = "reaContrat_Pay_Detail_nEnd_Dur"
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Dur", nEnd_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist_nEnd_Dur = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist_nEnd_Dur = False
		End If
		'UPGRADE_NOTE: Object lrecreaContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrat_Pay_Detail = Nothing
		On Error GoTo 0
	End Function
	'%IsExist_nInit_Dur: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%                   tabla "Contrat_Pay_Detail"
	Public Function IsExist_nInit_Dur(ByVal nContrat_Pay As Integer, ByVal nCode As Integer, ByVal nInit_Dur As Integer) As Boolean
		Dim lrecreaContrat_Pay_Detail As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecreaContrat_Pay_Detail = New eRemoteDB.Execute
		With lrecreaContrat_Pay_Detail
			.StoredProcedure = "reaContrat_Pay_Detail_v"
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Dur", nInit_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist_nInit_Dur = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist_nInit_Dur = False
		End If
		'UPGRADE_NOTE: Object lrecreaContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrat_Pay_Detail = Nothing
		On Error GoTo 0
	End Function
	
	
	'%ValRange: Este metodo retorna VERDADERO o FALSO dependiendo si el rango está
	'% incluido o no dentro de otro rango"
	Public Function ValRange(ByVal nContrat_Pay As Integer, ByVal nCode As Integer, ByVal nInit_Dur As Integer, ByVal nEnd_Dur As Integer) As Boolean
		Dim lrecreaContrat_Pay_Detail As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecreaContrat_Pay_Detail = New eRemoteDB.Execute
		With lrecreaContrat_Pay_Detail
			.StoredProcedure = "valContrat_Pay_Detail_range"
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Dur", nInit_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Dur", nEnd_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			ValRange = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			ValRange = False
		End If
		'UPGRADE_NOTE: Object lrecreaContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrat_Pay_Detail = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdContrat_Pay: Actualiza la informacion de la tabla de vehiculos
	Private Function InsUpdContrat_Pay(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdContrat_Pay As eRemoteDB.Execute
		
		On Error GoTo insUpdContrat_Pay_Err
		lrecinsUpdContrat_Pay = New eRemoteDB.Execute
		'+ Definición de store procedure insUpdContrat_Pay al 10-03-2002 15:57:37
		With lrecinsUpdContrat_Pay
			.StoredProcedure = "insUpdContrat_Pay"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Calc", nType_Calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTaxin", sTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdContrat_Pay = .Run(False)
		End With
		
insUpdContrat_Pay_Err: 
		If Err.Number Then
			InsUpdContrat_Pay = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdContrat_Pay = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsUpdTab_au_val: Actualiza la informacion de la tabla
	Private Function InsUpdContrat_Pay_Detail(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdContrat_Pay_Detail As eRemoteDB.Execute
		
		On Error GoTo insUpdContrat_Pay_Detail_Err
		lrecinsUpdContrat_Pay_Detail = New eRemoteDB.Execute
		'+ Definición de store procedure insUpdtab_au_val al 10-03-2002 16:40:43
		With lrecinsUpdContrat_Pay_Detail
			.StoredProcedure = "insUpdContrat_Pay_Detail"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Dur", nInit_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Dur", nEnd_Dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent_detail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdContrat_Pay_Detail = .Run(False)
		End With
		
insUpdContrat_Pay_Detail_Err: 
		If Err.Number Then
			InsUpdContrat_Pay_Detail = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdContrat_Pay_Detail = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Esta función agrega registros a la tabla TAB_AU_VAL
	Public Function Add_Detail() As Boolean
		If Not IsExist_contrat(nContrat_Pay) Then
			If InsUpdContrat_Pay(1) Then
				Add_Detail = InsUpdContrat_Pay_Detail(1)
			End If
		Else
			If InsUpdContrat_Pay(2) Then
				Add_Detail = InsUpdContrat_Pay_Detail(1)
			End If
		End If
	End Function
	
	'%Update: Esta función actualiza registros en la tabla TAB_AU_VAL
	Public Function Update_Detail() As Boolean
		
		If InsUpdContrat_Pay(2) Then
			Update_Detail = InsUpdContrat_Pay_Detail(2)
		End If
		
	End Function
	
	'%Delete: Esta función elimina registros de la tabla TAB_AU_VAL
	Public Function Delete_Detail() As Boolean
		Delete_Detail = InsUpdContrat_Pay_Detail(3)
	End Function
	
	'%InsValAG954Upd_Detail: Esta función se encarga de validar los datos introducidos en la zona de detalle
	
	Public Function InsValAG954Upd_Detail(ByVal sCodispl As String, ByVal sAction As String, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nSeq As Double, ByVal nCode As Integer, ByVal nInit_Dur As Integer, ByVal nEnd_Dur As Integer, ByVal nPercent_detail As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo InsValAG954Upd_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			If sClient = String.Empty Then
				.ErrorMessage(sCodispl, 2001)
			End If
			
			If sDescript = String.Empty Then
				.ErrorMessage(sCodispl, 10071)
			End If
			
			If dStartdate = dtmNull Then
				.ErrorMessage(sCodispl, 7114)
			End If
			
			If nType_Calc = eRemoteDB.Constants.intNull Or nType_Calc = 0 Then
				Call .ErrorMessage(sCodispl, 1012,  ,  , ": Tipo de cálculo")
			Else
				If nType_Calc = 1 Then 'Porcentaje fijo
					If nPercent = eRemoteDB.Constants.intNull Or nPercent = 0 Then
						.ErrorMessage(sCodispl, 55540)
					End If
					If nCurrency <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 11417)
					End If
					If nAmount <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100123)
					End If
				End If
				If nType_Calc = 2 Then 'Monto fijo
					If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
						Call .ErrorMessage(sCodispl, 1012,  ,  , ": Monto")
					End If
					If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
						.ErrorMessage(sCodispl, 750024)
					End If
					If nPercent <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100124)
					End If
				End If
				If nType_Calc = 3 Then 'Según tabla
					If nCurrency <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 11417)
					End If
					If nAmount <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100123)
					End If
					If nPercent <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100124)
					End If
				End If
				If nType_Calc = 4 Then 'Según Metas
					If nCurrency <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 11417)
					End If
					If nAmount <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100123)
					End If
					If nPercent <> eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 100124)
					End If
				End If
			End If
			
			If nAply = eRemoteDB.Constants.intNull Or nAply = 0 Then
				Call .ErrorMessage(sCodispl, 1012,  ,  , ": Elemento a aplicar")
			End If
			
			If nType_Calc = 1 And nAply <> 1 And nAply <> 2 Then
				.ErrorMessage(sCodispl, 100131)
			End If
			
			If nType_Calc = 2 And nAply <> 3 And nAply <> 4 Then
				.ErrorMessage(sCodispl, 100130)
			End If
			
			If nType_Calc = 3 And nAply <> 1 And nAply <> 2 Then
				.ErrorMessage(sCodispl, 100132)
			End If
			
			If nType_Calc = 4 And nAply <> 5 Then
				.ErrorMessage(sCodispl, 100134)
			End If
			
			If sTaxin = String.Empty Then
				sTaxin = "2"
			End If
			If sStatregt = String.Empty Then
				.ErrorMessage(sCodispl, 9089)
			End If
			
			If nType_Calc = 4 Then
				If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
					Call .ErrorMessage(sCodispl, 1012,  ,  , ": Tabla de metas")
				End If
			Else
				If nCode > 0 Then
					.ErrorMessage(sCodispl, 100133)
				End If
			End If
			
			'+ Se valida la columna: nInit_Dur
			If nInit_Dur = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1012,  ,  , ": Duración inicial")
			End If
			
			If sAction = "Add" Then
				If IsExist_nInit_Dur(nContrat_Pay, nCode, nInit_Dur) Then
					.ErrorMessage(sCodispl, 100126)
				End If
			End If
			
			If nEnd_Dur = eRemoteDB.Constants.intNull Or nEnd_Dur = 0 Then
				Call .ErrorMessage(sCodispl, 1012,  ,  , ": Duración final")
			End If
			
			If sAction = "Add" Then
				If IsExist_nEnd_Dur(nContrat_Pay, nCode, nEnd_Dur) Then
					.ErrorMessage(sCodispl, 100127)
				End If
			End If
			
			If nInit_Dur >= nEnd_Dur Then
				.ErrorMessage(sCodispl, 100125)
			End If
			
			If sAction = "Add" Then
				If ValRange(nContrat_Pay, nCode, nInit_Dur, nEnd_Dur) Then
					.ErrorMessage(sCodispl, 60214)
				End If
			End If
			
			If nPercent_detail = eRemoteDB.Constants.intNull Or nPercent_detail = 0 Then
				.ErrorMessage(sCodispl, 55540)
			End If
			
			InsValAG954Upd_Detail = lclsErrors.Confirm
		End With
		
InsValAG954Upd_Err: 
		If Err.Number Then
			InsValAG954Upd_Detail = "InsValAG954Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'*InsPostAG954Upd_Detail: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla Tab_au_val
	Public Function InsPostAG954Upd_detail(ByVal sAction As String, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nSeq As Double, ByVal nCode As Integer, ByVal nInit_Dur As Integer, ByVal nEnd_Dur As Integer, ByVal nPercent_detail As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostAG954Upd_Err
		
		With Me
			.nContrat_Pay = nContrat_Pay
			.sClient = sClient
			.sDescript = sDescript
			.dStartdate = dStartdate
			.nType_Calc = nType_Calc
			.nPercent = nPercent
			.nAmount = nAmount
			.nCurrency = nCurrency
			.nAply = nAply
			.sTaxin = sTaxin
			.sStatregt = sStatregt
			.nSeq = nSeq
			.nCode = nCode
			.nInit_Dur = nInit_Dur
			.nEnd_Dur = nEnd_Dur
			.nPercent_detail = nPercent_detail
			mlngUsercode = nUsercode
			
			InsPostAG954Upd_detail = True
			Select Case sAction
				'+Si la opción seleccionada es Registrar
				Case "Add"
					InsPostAG954Upd_detail = .Add_Detail()
					
					'+Si la opción seleccionada es Modificar
				Case "Update"
					InsPostAG954Upd_detail = .Update_Detail()
					
					'+Si la opción seleccionada es Eliminar
				Case "Del"
					InsPostAG954Upd_detail = .Delete_Detail()
			End Select
		End With
		
InsPostAG954Upd_Err: 
		If Err.Number Then
			InsPostAG954Upd_detail = False
		End If
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sClient = String.Empty
		sDescript = String.Empty
		dStartdate = dtmNull
		nType_Calc = eRemoteDB.Constants.intNull
		nPercent = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nAply = eRemoteDB.Constants.intNull
		sTaxin = String.Empty
		sStatregt = String.Empty
		nSeq = eRemoteDB.Constants.intNull
		nCode = eRemoteDB.Constants.intNull
		nInit_Dur = eRemoteDB.Constants.intNull
		nEnd_Dur = eRemoteDB.Constants.intNull
		nPercent_detail = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






