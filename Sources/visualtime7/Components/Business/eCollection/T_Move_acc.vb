Option Strict Off
Option Explicit On
Public Class T_Move_Acc
	'%-------------------------------------------------------%'
	'% $Workfile:: T_Move_Acc.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/07/04 3:42p                               $%'
	'% $Revision:: 35                                       $%'
	'%-------------------------------------------------------%'
	
	'COLUMN_NAME NULLABLE          DATA_TYPE   DATA_LENGTH DATA_PRECISION  DATA_SCALE
	Public nBordereaux As Double 'N   NUMBER  10  10  0
	Public nSequence As Integer 'N   NUMBER  5   5   0
	Public nTyp_acco As Integer 'Y   NUMBER  5   5   0
	Public nCurrency As Integer 'Y   NUMBER  5   5   0
	Public sClient As String 'Y   CHAR    14  null    null
	Public nCredit As Double 'Y   NUMBER  12  10  2
	Public nDebit As Double 'Y   NUMBER  12  10  2
	Public nType_Move As Integer 'Y   NUMBER  5   5   0
	Public nBranch As Integer 'Y   NUMBER  5   5   0
	Public sAutoriza As String 'Y   CHAR    1   null    null
	Public nExchange As Double 'Y   NUMBER  17  11  6
	Public sNumForm As String 'Y   CHAR    12  null    null
	Public nIntermed As Double 'Y   NUMBER  10  10  0
	Public nProduct As Integer 'Y   NUMBER  5   5   0
	Public nPolicy As Double 'Y   NUMBER  10  10  0
	Public nReceipt As Double 'Y   NUMBER  10  10  0
	Public nCertif As Double
	Public nProponum As Double
	
	'**-Auxiliary variables
	'-Variables auxiliares
	Public sMessage As String
	Public nErrornum As Integer
	
	Public sCliename As String
	Public sDigit As String
	Public sCurrency As String
	
	'**%ADD: This method is in charge of adding new records to the table "t_Move_Acc".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "t_Move_Acc". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add(ByVal nBordereaux As Double, ByVal sClient As String, ByVal nCredit As Double, ByVal nCurrency As Integer, ByVal nDebit As Double, ByVal nType_Move As Integer, ByVal nTyp_acco As Integer, ByVal sNumForm As String, ByVal nBranch As Integer, ByVal sAuthoriza As String, ByVal nExchange As Double, ByVal nIntermed As Double, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nCash_num As Integer) As Boolean
		Dim lreccreT_Move_Acc As eRemoteDB.Execute
		
		lreccreT_Move_Acc = New eRemoteDB.Execute
		
		With lreccreT_Move_Acc
			.StoredProcedure = "creT_Move_Acc"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountDec", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAuthoriza", sAuthoriza, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_Num", nCash_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreT_Move_Acc = Nothing
	End Function
	
	'**%Del: This routine deletes the data from the temporary table of current account transactions
	'%Del: Rutina que borra la información de la tabla temporal de movimientos de cuentas corrientes
	Public Function Del(ByVal nBordereaux As Double, ByVal nSequence As Integer) As Boolean
		Dim lrecdelT_Move_Acc As eRemoteDB.Execute
		
		On Error GoTo Del_err
		
		lrecdelT_Move_Acc = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.delT_Move_Acc'
		'**+Data of 11/23/2000 09:12:37 p.m.
		'+Definición de parámetros para stored procedure 'insudb.delT_Move_Acc'
		'+Información leída el 11/23/2000 09:12:37 p.m.
		
		With lrecdelT_Move_Acc
			.StoredProcedure = "delT_Move_Acc_o"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Del = .Run(False)
		End With
		
Del_err: 
		If Err.Number Then
			Del = False
		End If
		'UPGRADE_NOTE: Object lrecdelT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelT_Move_Acc = Nothing
		On Error GoTo 0
		
	End Function
	
	'**%Del_all: This routine Del_alletes the data from the temporary table of current account transactions
	'%Del_all: Rutina que borra la información de la tabla temporal de movimientos de cuentas corrientes
	Public Function Del_all(ByVal nBordereaux As Double) As Boolean
		Dim lrecT_Move_Acc As eRemoteDB.Execute
		
		On Error GoTo Del_all_Err
		
		lrecT_Move_Acc = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.Del_allT_Move_Acc'
		'**+Data of 11/23/2000 09:12:37 p.m.
		'+Definición de parámetros para stored procedure 'insudb.Del_allT_Move_Acc'
		'+Información leída el 11/23/2000 09:12:37 p.m.
		
		With lrecT_Move_Acc
			.StoredProcedure = "DelT_Move_Acc"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", 18, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Del_all = .Run(False)
		End With
		
Del_all_Err: 
		If Err.Number Then
			Del_all = False
		End If
		'UPGRADE_NOTE: Object lrecT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Move_Acc = Nothing
		On Error GoTo 0
		
	End Function
	
	'**%find: This routine verifies if the premium invoice is not in another relation in unfinished status or
	'**%if the premium invoice was already collected
	'%find: Esta rutina permite verificar si el recibo no esta en otra relación en captura incompleta o ya fue pagado
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sNumForm As String, ByVal nBordereaux As Double) As Boolean
		Dim lrecinsValT_Move_AccReq As eRemoteDB.Execute
		Dim nBordereaux_out As Double
		Dim nPolicy_out As Integer
		
		On Error GoTo Find_Err
		
		lrecinsValT_Move_AccReq = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.insValT_Move_AccReq'
		'**+Data of 11/23/2000 09:23:35 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insValT_Move_AccReq'
		'+Información leída el 23/11/2000 09:23:35 p.m.
		
		With lrecinsValT_Move_AccReq
			.StoredProcedure = "insValT_Move_AccReq"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux_out", nBordereaux_out, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_out", nPolicy_out, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If nBordereaux_out <> 0 Or nPolicy_out <> 0 Then
					If nBordereaux_out <> 0 Then
						sMessage = Trim(CStr(CDbl(nBordereaux_out)))
						nErrornum = 750025
					Else
						If nPolicy_out <> 0 Then
							sMessage = Trim(CStr(CDbl(nPolicy_out)))
							nErrornum = 750068
						End If
					End If
					sMessage = ""
					nErrornum = 0
					Find = True
				End If
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecinsValT_Move_AccReq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValT_Move_AccReq = Nothing
		On Error GoTo 0
	End Function
	
	'**%find_sClient: This routine verifies if the premium invoice is not in another relation in unfinished status or
	'**%if the premium invoice was already collected
	'%find_sClient: Esta rutina permite verificar si el recibo no esta en otra relación en captura incompleta o ya fue pagado
	Public Function Find_sClient(ByVal nBordereaux As Double, ByVal sClient As String) As Boolean
		Dim lrecT_Move_Acc As eRemoteDB.Execute
		
		On Error GoTo Find_sClient_Err
		
		lrecT_Move_Acc = New eRemoteDB.Execute
		
		With lrecT_Move_Acc
			.StoredProcedure = "reaT_Move_Acc_O"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_sClient = .Run
			If Find_sClient Then
				Me.nBordereaux = nBordereaux
				nSequence = .FieldToClass("nSequence")
				nTyp_acco = .FieldToClass("nTyp_acco")
				nCurrency = .FieldToClass("sClient")
				sClient = sClient
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				nType_Move = .FieldToClass("nType_move")
				nBranch = .FieldToClass("nBranch")
				sAutoriza = .FieldToClass("sAutoriza")
				nExchange = .FieldToClass("nExchange")
				sNumForm = .FieldToClass("sNumForm")
				nIntermed = .FieldToClass("nIntermed")
				nProduct = .FieldToClass("nProduct")
				nPolicy = .FieldToClass("nPolicy")
				nReceipt = .FieldToClass("nReceipt")
				nCertif = .FieldToClass("nCertif")
				nProponum = .FieldToClass("nProponum")
			End If
		End With
		
Find_sClient_Err: 
		If Err.Number Then
			Find_sClient = False
		End If
		'UPGRADE_NOTE: Object lrecT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Move_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValCO012Upd: This routine validates the frame fields (CO012)
	'%insValCO012Upd: Rutina que permite validar los campos del frame.
	Public Function insValCO012Upd(ByVal nBordereaux As Double, ByVal nAmount As Double, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nExchange As Double, ByVal nOldAmountl As Double, ByVal nSaldoTot As Double, ByVal nBalance As Double, ByVal nBalanceTotal As Double) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCO012Upd_Err
		
		lobjErrors = New eFunctions.Errors
		
		'+Se efectuan las validaciones correspondientes al importe
		If nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull Then
			lblnError = True
			lobjErrors.ErrorMessage("CO012", 5061)
		End If
		
		'+ El importe no puede ser mayor al saldo
		If nBalance <> 0 Then
			If (nBalanceTotal - nAmount) < 0 Then
				lblnError = True
				lobjErrors.ErrorMessage("CO012", 55158)
			End If
		End If
		
		'+Se efectuan las validaciones correspondientes a la moneda
		If nCurrency = 0 Then
			lblnError = True
			lobjErrors.ErrorMessage("CO012", 750011)
		End If
		
		insValCO012Upd = lobjErrors.Confirm
		
insValCO012Upd_Err: 
		If Err.Number Then
			insValCO012Upd = CStr(False)
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insValCO012: Se efectuan las validaciones de la ventana CO012.
	Public Function insValCO012(ByVal nBordereaux As Double, ByVal nItems As Integer) As String
		Dim lrecInsValCO012 As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
		
		On Error GoTo insValCO012_Err
		
		lrecInsValCO012 = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		With lrecInsValCO012
			.StoredProcedure = "InsValCO012"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("CO012",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValCO012 = .Confirm
		End With
		
insValCO012_Err: 
		If Err.Number Then
			insValCO012 = insValCO012 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lrecInsValCO012 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValCO012 = Nothing
	End Function
	
	'**%insPostCO012Upd: This method updates the database (as described in the functional specifications)
	'**%for the page "CO012"
	'%insPostCO012Upd: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "CO012"
	Public Function insPostCO012Upd(ByVal nBordereaux As Double, ByVal nAmount As Double, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nExchange As Double, ByVal dCollectdate As Date, ByVal nSequence As Integer, ByVal nType_Move As Integer, ByVal nCash_num As Integer) As Boolean
		On Error GoTo insPostCO012Upd_Err
		
		If nSequence = eRemoteDB.Constants.intNull Then
			insPostCO012Upd = Add(nBordereaux, sClient, nAmount, nCurrency, 0, nType_Move, 5, String.Empty, eRemoteDB.Constants.intNull, String.Empty, nExchange, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nCash_num)
		Else
			insPostCO012Upd = Update(nBordereaux, sClient, nAmount, nCurrency, 0, nType_Move, 5, String.Empty, eRemoteDB.Constants.intNull, String.Empty, nExchange, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nSequence)
		End If
		
insPostCO012Upd_Err: 
		If Err.Number Then
			insPostCO012Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostCO012: This method updates the database (as described in the functional specifications)
	'**%for the page "CO012"
	'%insPostCO012: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "CO012"
	Public Function insPostCO012(ByVal nBordereaux As Double, ByRef nItems As Integer) As Boolean
		Dim lclsColformRef As ColformRef
		
		On Error GoTo insPostCO012_Err
		
		insPostCO012 = True
		'+ Si existen registros en la ventana
		If nItems > 0 Then
			'+ Se actualiza la ventana con contenido
			lclsColformRef = New ColformRef
			lclsColformRef.UpdateConWinPos(nBordereaux, 3, "1")
		End If
		
insPostCO012_Err: 
		If Err.Number Then
			insPostCO012 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	
	'**%Update: This method is in charge of updating records in the table "t_Move_Acc".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "t_Move_Acc". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update(ByVal nBordereaux As Double, ByVal sClient As String, ByVal nCredit As Double, ByVal nCurrency As Integer, ByVal nDebit As Double, ByVal nType_Move As Integer, ByVal nTyp_acco As Integer, ByVal sNumForm As String, ByVal nBranch As Integer, ByVal sAuthoriza As String, ByVal nExchange As Double, ByVal nIntermed As Double, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByRef nSequence As Integer) As Boolean
		Dim lrecupdt_Move_Acc As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdt_Move_Acc = New eRemoteDB.Execute
		
		With lrecupdt_Move_Acc
			.StoredProcedure = "updt_Move_Acc"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutoriza", sAuthoriza, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumForm", sNumForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdt_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdt_Move_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'%valExistT_Move_Acc: Esta rutina permite verificar la existencia de registros en la tabla t_Move_Acc.
	Public Function valExistT_Move_Acc(ByVal nBordereaux As Double) As Boolean
		Dim lrecT_Move_Acc As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valExistT_Move_Acc_Err
		
		lrecT_Move_Acc = New eRemoteDB.Execute
		
		With lrecT_Move_Acc
			.StoredProcedure = "valT_Move_Acc_O"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistT_Move_Acc = (.Parameters("nExists").Value = 1)
		End With
		
valExistT_Move_Acc_Err: 
		If Err.Number Then
			valExistT_Move_Acc = False
		End If
		'UPGRADE_NOTE: Object lrecT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Move_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'%valT_Move_Acc_sClient: Esta rutina permite verificar la existencia de registros en la tabla t_Move_Acc.
	Public Function valT_Move_Acc_sClient(ByVal nBordereaux As Double, Optional ByVal sClient As String = "") As Boolean
		Dim lrecT_Move_Acc As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valT_Move_Acc_sClient_Err
		
		lrecT_Move_Acc = New eRemoteDB.Execute
		
		With lrecT_Move_Acc
			.StoredProcedure = "valT_Move_Acc_O"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valT_Move_Acc_sClient = (.Parameters("nExists").Value = 1)
		End With
		
valT_Move_Acc_sClient_Err: 
		If Err.Number Then
			valT_Move_Acc_sClient = False
		End If
		'UPGRADE_NOTE: Object lrecT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Move_Acc = Nothing
		On Error GoTo 0
	End Function
End Class






