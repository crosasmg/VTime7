Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class Acc_lines
	'%-------------------------------------------------------%'
	'% $Workfile:: Acc_lines.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                  Type         Computed  Length    Prec  Scale Nullable    TrimTrailingBlanks      FixedLenNullInSource
	Public nVoucher As Integer 'int         no        4           10    0     no             (n/a)                    (n/a)
	Public nLed_compan As Integer 'smallint    no        2            5    0     no             (n/a)                    (n/a)
	Public nLine As Integer 'smallint    no        2            5    0     no             (n/a)                    (n/a)
	Public sAccount As String 'char        no       20                       no             yes                      no
	Public sAux_accoun As String 'char        no       20                       no             yes                      no
	Public sClient As String 'char        no       14                       yes            yes                      yes
	Public nCredit As Double 'decimal     no        9           12    2     yes            (n/a)                    (n/a)
	Public dDate_doc As Date 'datetime    no        8                       yes            (n/a)                    (n/a)
	Public nDebit As Double 'decimal     no        9           12    2     yes            (n/a)                    (n/a)
	Public sDescript As String 'char        no       30                       yes            yes                      yes
	Public nDoc_type As Integer 'smallint    no        2            5    0     yes            (n/a)                    (n/a)
	Public nDocNumber As Integer 'int         no        4           10    0     yes            (n/a)                    (n/a)
	Public nNoteNum As Integer 'int         no        4           10    0     yes            (n/a)                    (n/a)
	Public nOri_curr As Integer 'smallint    no        2            5    0     yes            (n/a)                    (n/a)
	Public sStatregt As String 'char        no        1                       yes            yes                      yes
	Public nUsercode As Integer 'smallint    no        2            5    0     yes            (n/a)                    (n/a)
	Public sCost_cente As String 'char        no        8                       yes            yes                      yes
	Public nExchange As Double 'decimal     no        9           10    6     yes            (n/a)                    (n/a)
	Public nOri_amo As Double 'decimal     no        9           12    2     yes            (n/a)                    (n/a)
	
	'**-Auxiliaries variables
	'- Variables auxiliares
	'**-Variable that contein the description of a countable account
	'- Variable que contiene la descripcion de la cuenta contable
	
	Public sDesAccount As String
	
	'**- Variable that contein the auxiliary code description of the countable account
	'- Variable que contiene la descripcion del codigo auxiliar de la cuenta contable
	
	Public sDesAux As String
	
	'**-Variable that contein the client name
	'- Variable que contiene el nombre del cliente
	
	Public sClientName As String
	
	
	'**% AccAcumTotal: returns the total acumulated  of the column DEBT and INCOME of an account for
	'**%a effect given date
	'% AccAcumTotal: Devuelve el total acumulado de las columnas DEBE y HABER de una cuenta para una
	'% fecha de efecto dada
	Public Function AccAcumTotal(ByVal intLed_compan As Integer, ByVal dtmEffecdate As Date, ByVal strAccount As String, ByVal strAux_accoun As String) As Boolean
		
		'**-Define the variable lrecreaAcc_transaAcc_linesAcum
		'- Se define la variable lrecreaAcc_transaAcc_linesAcum
		Dim lrecreaAcc_transaAcc_linesAcum As eRemoteDB.Execute
		
		lrecreaAcc_transaAcc_linesAcum = New eRemoteDB.Execute
		
		On Error GoTo AccAcumTotal_err
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaAcc_linesAcum'
		'**+Data read on 06/19/2001 12:07:01 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaAcc_linesAcum'
		'+ Informacion leida el 19/06/2001 12:07:01 PM
		
		With lrecreaAcc_transaAcc_linesAcum
			.StoredProcedure = "reaAcc_transaAcc_linesAcum"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", strAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", strAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("ldblDebit") = eRemoteDB.Constants.intNull Then
					nDebit = 0
				Else
					nDebit = .FieldToClass("ldblDebit")
				End If
				
				If .FieldToClass("ldblCredit") = eRemoteDB.Constants.intNull Then
					nCredit = 0
				Else
					nCredit = .FieldToClass("ldblCredit")
				End If
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaAcc_linesAcum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaAcc_linesAcum = Nothing
		
AccAcumTotal_err: 
		If Err.Number Then
			AccAcumTotal = False
		End If
		On Error GoTo 0
	End Function
	
	'**% AccMonthTotal: Return the total of the columns DEBT and INCOME of an account for a given month and year
	'% AccMonthTotal: Devuelve el total de las columnas DEBE y HABER de una cuenta para un mes y agno dados
	Public Function AccMonthTotal(ByVal intLed_compan As Integer, ByVal dtmEffecdate As Date, ByVal strAccount As String, ByVal strAux_accoun As String) As Boolean
		
		'**-Define the variable lrecreaAcc_transaAcc_linesMen
		'- Se define la variable lrecreaAcc_transaAcc_linesMen
		Dim lrecreaAcc_transaAcc_linesMen As eRemoteDB.Execute
		
		lrecreaAcc_transaAcc_linesMen = New eRemoteDB.Execute
		
		On Error GoTo AccMonthTotal_err
		'**+ Parameters definition for the stored procedure 'insudb.reaAcc_transaAcc_linesMen'
		'**+Data read on 06/19/2001 12:08:36 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaAcc_linesMen'
		'+ Informacion leida el 19/06/2001 12:08:36 PM
		
		With lrecreaAcc_transaAcc_linesMen
			.StoredProcedure = "reaAcc_transaAcc_linesMen"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", strAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", strAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AccMonthTotal = .Run
			If AccMonthTotal Then
				If .FieldToClass("ldblDebit") = eRemoteDB.Constants.intNull Then
					nDebit = 0
				Else
					nDebit = .FieldToClass("ldblDebit")
				End If
				
				If .FieldToClass("ldblCredit") = eRemoteDB.Constants.intNull Then
					nCredit = 0
				Else
					nCredit = .FieldToClass("ldblCredit")
				End If
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_transaAcc_linesMen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_transaAcc_linesMen = Nothing
		
AccMonthTotal_err: 
		If Err.Number Then
			AccMonthTotal = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find: return the detail lines of a determinate list (voucher)
	'% Find: Devuelve las lineas de detalle de un determinado asiento (voucher)
	Public Function Find(ByVal intLed_compan As Integer, ByVal lngVoucher As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		
		'**-Declare the variable that determinate the result of the function (True/False)
		'**'Static lblnRead As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		'Static lblnRead As Boolean
		
		'**-Define the variable lrecreaAcc_lines
		'- Se define la variable lrecreaAcc_lines
		Dim lrecreaAcc_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'**+If the serch is not make...
		'+ Si la busqueda no se ha realizado...
		If nLed_compan <> intLed_compan Or nVoucher <> lngVoucher Or lblnFind Then
			
			lrecreaAcc_lines = New eRemoteDB.Execute
			
			nLed_compan = intLed_compan
			nVoucher = lngVoucher
			
			'**+Parameters definition for the stored procedure 'insudb.reaAcc_lines'
			'**+Data read on 06/19/2001 12:09:30 PM
			'+ Definicion de parametros para stored procedure 'insudb.reaAcc_lines'
			'+ Informacion leida el 19/06/2001 12:09:30 PM
			
			With lrecreaAcc_lines
				.StoredProcedure = "reaAcc_lines"
				.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nVoucher", lngVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nVoucher = .FieldToClass("nVoucher")
					nLed_compan = .FieldToClass("nLed_compan")
					nLine = .FieldToClass("nLine")
					sAccount = .FieldToClass("sAccount")
					sAux_accoun = .FieldToClass("sAux_accoun")
					sClient = .FieldToClass("sClient")
					nCredit = .FieldToClass("nCredit")
					dDate_doc = .FieldToClass("dDate_doc")
					nDebit = .FieldToClass("nDebit")
					sDescript = .FieldToClass("sDescript")
					nDoc_type = .FieldToClass("nDoc_type")
					nDocNumber = .FieldToClass("nDocNumber")
					nNoteNum = .FieldToClass("nNotenum")
					nOri_curr = .FieldToClass("nOri_curr")
					sStatregt = .FieldToClass("sStatregt")
					nUsercode = .FieldToClass("nUsercode")
					sCost_cente = .FieldToClass("sCost_cente")
					nExchange = .FieldToClass("nExchange")
					nOri_amo = .FieldToClass("nOri_amo")
					
					Find = True
					
					.RCloseRec()
				Else
					nLed_compan = 0
					
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_lines = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'*** VoucherInfo: return all the info of a voucher (lodged in a temporal table)
	''* VoucherInfo: Devuelve toda la informacion de un asiento (alojada en una tabla temporal)
	''-----------------------------------------------------------------------------------------
	'Public Function VoucherInfo(ByVal intLed_compan As long, _
	''                            ByVal lngVoucher As Long, _
	''                            ByRef clsAcc_transa As Acc_transa) As Boolean
	''-----------------------------------------------------------------------------------------
	'**-Define the variable lrecreaAcc_transaAcc_linesV
	''- Se define la variable lrecreaAcc_transaAcc_linesV
	'    Dim lrecreaAcc_transaAcc_linesV As eRemotedb.Execute
	'    Dim llngVoucher As Long
	'
	'    Set lrecreaAcc_transaAcc_linesV = New eRemotedb.Execute
	'
	'**+Parameters definition for the stored procedure 'insudb.reaAcc_transaAcc_linesV'
	'**+ Data read on 06/19/2001 12:16:44 PM
	''+ Definicion de parametros para stored procedure 'insudb.reaAcc_transaAcc_linesV'
	''+ Informacion leida el 19/06/2001 12:16:44 PM
	'
	'    With lrecreaAcc_transaAcc_linesV
	'        .StoredProcedure = "reaAcc_transaAcc_linesV"
	'        .Parameters.Add "nLed_compan", intLed_compan, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nVoucher", lngVoucher, rdbParamInput, rdbInteger, 0, 0, 10, rdbParamNullable
	'
	'        VoucherInfo = .Run
	'
	'        If VoucherInfo Then
	'            Do While Not .EOF
	'                If llngVoucher <> .FieldToClass("nVoucher") Then
	'                    llngVoucher = .FieldToClass("nVoucher")
	'                    clsAcc_transa.sProcess_in = .FieldToClass("sProcess_in")
	'                    clsAcc_transa.nVoucher = .FieldToClass("nVoucher")
	'                    clsAcc_transa.nOffiNum = .FieldToClass("nOffiNum")
	'                    clsAcc_transa.dEffecDate = .FieldToClass("dEffecDate")
	'                    clsAcc_transa.nTot_credit = .FieldToClass("nTot_credit")
	'                    clsAcc_transa.nTot_debit = .FieldToClass("nTot_debit")
	'                    clsAcc_transa.sDescript = .FieldToClass("sDescriptA")
	'                    clsAcc_transa.nNotenum = .FieldToClass("nNotenumA")
	'                End If
	'
	'                Call clsAcc_transa.colAcc_lineses.Add(.FieldToClass("nVoucher"), _
	''                                                      intLed_compan, _
	''                                                      .FieldToClass("nLine"), _
	''                                                      .FieldToClass("sAccount"), _
	''                                                      .FieldToClass("sAux_accoun"), _
	''                                                      .FieldToClass("sClient"), _
	''                                                      .FieldToClass("nCredit"), _
	''                                                      .FieldToClass("dDate_doc"), _
	''                                                      .FieldToClass("nDebit"), _
	''                                                      .FieldToClass("sDescript"), _
	''                                                      .FieldToClass("nDoc_type"), _
	''                                                      .FieldToClass("nDocNumber"), _
	''                                                      .FieldToClass("nNotenum"), _
	''                                                      .FieldToClass("nOri_curr"), _
	''                                                      "1", _
	''                                                      nUsercode, _
	''                                                      .FieldToClass("sCost_cente"), _
	''                                                      .FieldToClass("nExchange"), _
	''                                                      .FieldToClass("nOri_amo"))
	'            .RNext
	'            Loop
	'
	'            .RCloseRec
	'        End If
	'    End With
	'
	'    Set lrecreaAcc_transaAcc_linesV = Nothing
	'End Function
	
	'**%insValCP005_k: routine that validate the window header.
	'%insValCP005_k: Rutina de validación del encabezado de la ventana.
	Public Function insValCP005_k(ByVal nVoucher As Integer, ByVal nLed_compan As Integer, ByVal nAction As Integer, ByVal sCodispl As String, ByVal nOffiNum As Integer, ByVal dEffecdate As Date, ByVal chkFutureMonth As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsLed_compan As eLedge.Led_compan
		
		On Error GoTo insValCP005_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsLed_compan = New eLedge.Led_compan
		
		insValCP005_k = CStr(True)
		
		If CInt(nVoucher) = 0 And CInt(nOffiNum) = 0 And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			Call lclsErrors.ErrorMessage(sCodispl, 736031)
		End If
		
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then '13
			If CInt(nVoucher) <> 0 Then '14
				If Not insValVoucherExist(CInt(nVoucher), nLed_compan, False) Then '15
					Call lclsErrors.ErrorMessage(sCodispl, 36042)
				End If '15
			End If '14
		ElseIf nAction = eFunctions.Menues.TypeActions.clngActionadd Then  '13
			If CInt(nVoucher) <> 0 Then '16
				If insValVoucherExist(CInt(nVoucher), nLed_compan, False) Then '17
					Call lclsErrors.ErrorMessage(sCodispl, 36041)
				End If '17
			End If '16
		End If '13
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then '10
			If CInt(nOffiNum) <> 0 Then '11
				If Not insValVoucherExist(CInt(nOffiNum), nLed_compan, True) Then '12
					Call lclsErrors.ErrorMessage(sCodispl, 36042)
				End If '12
			End If '11
		End If '10
		
		'**+Makes the validation of the date voucher fiels .
		'+Se efectua la validación del campo fecha del asiento.
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then '1
			If (dEffecdate = eRemoteDB.Constants.dtmNull) Then '2
				Call lclsErrors.ErrorMessage(sCodispl, 36044)
			Else
				If dEffecdate <> eRemoteDB.Constants.dtmNull Then '3
					If Not IsDate(dEffecdate) Then '4
						Call lclsErrors.ErrorMessage(sCodispl, 7114)
					Else '4
						If mclsLed_compan.Find(nLed_compan) Then '5
							If CDate(dEffecdate) < CDate(mclsLed_compan.dDate_init) Then '6
								If mclsLed_compan.sClose_mont = "2" Then '7
									Call lclsErrors.ErrorMessage(sCodispl, 7057)
								Else '7
									Call lclsErrors.ErrorMessage(sCodispl, 36122)
								End If '7
							Else '6
								If CDate(dEffecdate) > CDate(mclsLed_compan.dDate_end) Then '8
									If chkFutureMonth = "2" Then '9
										Call lclsErrors.ErrorMessage(sCodispl, 7057)
									Else '9
										Call lclsErrors.ErrorMessage(sCodispl, 36128)
									End If '9
								End If '8
							End If '6
						End If '5
						
					End If '4
				End If '3
			End If '2
		End If '1
		
		
		insValCP005_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP005_k_Err: 
		If Err.Number Then
			insValCP005_k = insValCP005_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValVoucherExist: this routine permit to verify the existance a voucher.
	'%insValVoucherExist: Esta rutina permite verificar la existencia de un comprobante.
	Public Function insValVoucherExist(ByVal nVoucher As Integer, ByVal nLed_compan As Integer, ByVal lblnIndicator As Boolean) As Boolean
		Dim pclsAcc_transa As eLedge.Acc_transa
		
		pclsAcc_transa = New eLedge.Acc_transa
		
		On Error GoTo insValVoucherExist_err
		
		If Not lblnIndicator Then
			insValVoucherExist = pclsAcc_transa.Find(nLed_compan, nVoucher, True)
		Else
			insValVoucherExist = pclsAcc_transa.Find_ByOffiNum(nLed_compan, nVoucher, True)
		End If
		
insValVoucherExist_err: 
		If Err.Number Then
			insValVoucherExist = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insValCP005: Routine that validate the header window.
	'%insValCP005: Rutina de validación del encabezado de la ventana.
	Public Function insValCP005(ByVal nLed_compan As Integer, ByVal sWindowType As String, ByVal nAction As Integer, ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal sAux_accoun As String, ByVal dDateDoc As Date, ByVal sSelect As String, ByVal nCredit As Double, ByVal nDebit As Double, ByVal nOri_curr As Integer, ByVal nDoc_type As Integer, ByVal nDocNumber As Integer, ByVal sDescript As String, ByVal sAccount As String, ByVal sUnmat As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsTab_cost_c As eLedge.Tab_cost_c
		Dim mclsLed_compan As eLedge.Led_compan
		
		
		On Error GoTo insValCP005_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsTab_cost_c = New eLedge.Tab_cost_c
		mclsLed_compan = New eLedge.Led_compan
		
		
		insValCP005 = String.Empty
		
		If sWindowType = "Normal" Then
			
			If Trim(sDescript) = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 36046)
			End If
			
		Else
			
			'**+Makes the validation of the countable account.
			'+Se efectúa la validación del campo Cuenta Contable.
			
			If Trim(sAccount) = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 36017)
			Else
				If Trim(sAccount) <> String.Empty Then
					If Not mclsLedger_acc.ValAccountStruc(nLed_compan, sAccount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36019)
					Else
						If Not mclsLedger_acc.Find_AccountActive(nLed_compan, sAccount) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36010)
						Else
							If mclsLedger_acc.Val_Structure_Down(nLed_compan, sAccount) Then
								Call lclsErrors.ErrorMessage(sCodispl, 7129)
							End If
						End If
					End If
				End If
			End If
			
			'**+Makes the validation of the auxiliary field of the countable account.
			'+Se efectúa la validación del campo Auxiliar de Cuenta Contable.
			
			If (sAux_accoun) = String.Empty Then
				If (sAccount) <> String.Empty Then
					If Not mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36021)
					Else
						If mclsLedger_acc.ValAnotherAux(nLed_compan, sAccount) Then
							Call lclsErrors.ErrorMessage(sCodispl, 7129)
						End If
					End If
				End If
			Else
				If (sAux_accoun) <> String.Empty Then
					If (sAccount) <> String.Empty Then
						If Not mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
							Call lclsErrors.ErrorMessage(sCodispl, 36021)
						End If
					End If
				End If
			End If
			
			
			'**+Make the validation of the organizative unity field.
			'+Se efectúa la validación del campo Unidad Organizativa.
			
			If (sCost_cente) = String.Empty Then
				If (sAccount) <> String.Empty Then
					If mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
						If mclsLedger_acc.sOrgan_unit = "1" Then
							Call lclsErrors.ErrorMessage(sCodispl, 36051)
						End If
					End If
				End If
			Else
				If (sCost_cente) <> String.Empty Then
					If Not mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36050)
					Else
						If (sAccount) <> String.Empty Then
							If mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
								If mclsLedger_acc.sOrgan_unit = "2" Then
									Call lclsErrors.ErrorMessage(sCodispl, 36052)
								End If
							End If
						End If
					End If
				End If
			End If
			
			'**+Make the validation of the Debit field.
			'+Se efectúa la validación del campo Débitos.
			
			If nDebit > 0 Then
				If lclsField.ValNumber(nDebit) Then
					If nDebit <> 0 And nDebit <> eRemoteDB.Constants.intNull Then
						If nCredit <> 0 And nCredit <> eRemoteDB.Constants.intNull Then
							Call lclsErrors.ErrorMessage(sCodispl, 36059)
						Else
							If (sAccount) <> String.Empty Then
								If mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
									If mclsLedger_acc.sBlock_deb = "1" Then
										Call lclsErrors.ErrorMessage(sCodispl, 36053)
									End If
								End If
							End If
							
							If (sCost_cente) <> String.Empty Then
								If mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
									If mclsTab_cost_c.sBlock_deb = "1" Then
										Call lclsErrors.ErrorMessage(sCodispl, 36055)
									End If
								End If
							End If
						End If
					End If
				End If
			End If
			'**+Make the validation of the Credit field.
			'+Se efectúa la validación del campo Créditos.
			If nCredit > 0 Then
				If lclsField.ValNumber(nCredit) Then
					If nCredit <> 0 And nCredit <> eRemoteDB.Constants.intNull Then
						If nDebit <> 0 And nDebit <> eRemoteDB.Constants.intNull Then
							Call lclsErrors.ErrorMessage(sCodispl, 36059)
						Else
							If sAccount <> String.Empty Then
								If mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
									If mclsLedger_acc.sBlock_cre = "1" Then
										Call lclsErrors.ErrorMessage(sCodispl, 36054)
									End If
								End If
							End If
							
							If sCost_cente <> String.Empty Then
								If mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
									If mclsTab_cost_c.sBlock_cre = "1" Then
										Call lclsErrors.ErrorMessage(sCodispl, 36055)
									End If
								End If
							End If
						End If
					End If
				End If
			End If
			'**+Make the validation of the Currency field.
			'+Se efectúa la validación del campo Moneda.
			
			If nOri_curr <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10107)
			End If
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(dDateDoc) Then
				If Not IsDate(dDateDoc) Then
					Call lclsErrors.ErrorMessage(sCodispl, 7114)
				End If
			End If
			
			If nDoc_type <> eRemoteDB.Constants.intNull Then
				If nDoc_type = 2 Then
					If nCredit <> 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 36217)
					End If
				Else
					If nDebit <> 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 36217)
					End If
				End If
			End If
			
		End If
		
		
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And Trim(sSelect) = "2" Then '**Cut
			'Cortar
		Else
			'+   Al menos uno de los dos debe tener valor.
			'**+ At least one of the two must have any value.
			If nDebit <= 0 And nCredit = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 36112)
			End If
			
			
			'+ Si se introduce el número, tipo o fecha, se deben introducir los tres campos - Validación 36060
			'**+ If any number, type or date are introduced, you must introduce values
			'**+ into those three fields - Validation number 36060
			
			If sWindowType <> "Normal" Then
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If (nDoc_type <> eRemoteDB.Constants.intNull) And (nDocNumber = 0 Or IsNothing(dDateDoc)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36060)
				End If
				
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If (nDocNumber <> eRemoteDB.Constants.intNull) And (nDoc_type <= 0 Or IsNothing(dDateDoc)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36060)
				End If
				
				If (dDateDoc <> eRemoteDB.Constants.dtmNull) And (nDocNumber <= 0 Or nDoc_type <= 0) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36060)
				End If
			End If
		End If
		
		If nDebit <> nCredit And sWindowType = "Normal" Then
			If sUnmat = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36058) 'Adv. Asiento descuadrado.
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 36057) 'Err. Asiento descuadrado.
			End If
		End If
		
		
		insValCP005 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object mclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsTab_cost_c = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP005_Err: 
		If Err.Number Then
			insValCP005 = insValCP005 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostCP005: This function is incharge to validate all the introduced data in the form
	'%insPostCP005: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCP005(ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal sSel As String, ByVal dEffecdate As Date, ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal lblDebit As Double, ByVal lblCredit As Double, ByVal chkProcess As String, ByVal nVoucher As Integer, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal sClient As String, ByVal dDate_doc As Date, ByVal sDescript As String, ByVal nDoc_type As Integer, ByVal nDocNumber As Integer, ByVal nOri_curr As Integer, ByVal sStatregt As String, ByVal sAction As String, ByVal nLine As Integer, ByVal nExchange As Double, ByVal nOri_amo As Double, ByVal sDescript_transa As String, ByVal nNoteNum_transa As Integer, ByVal nOffiNum As Integer, ByVal sWindowType As String, ByVal sCost_cente As String, ByVal sUnmat As String) As Boolean
		Dim lclsLed_compan As eLedge.Led_compan
		Dim pclsAcc_transa As eLedge.Acc_transa
		Dim lclsAcc_lineses As eLedge.Acc_lineses
		Dim lclsGeneralForm As Object
		
		Dim ldtmInitDate As Date
		Dim ldtmEndDate As Date
		Dim ldtmInitDateOpt As Date
		Dim ldtmEndDateOpt As Date
		Dim llngYear As Integer
		Dim ldtmDate_tmp As Date
		Dim llngVoucher As Integer
		Dim ldblDebit As Double
		Dim ldblCredit As Double
		
		lclsLed_compan = New eLedge.Led_compan
		pclsAcc_transa = New eLedge.Acc_transa
		lclsAcc_lineses = New eLedge.Acc_lineses
		lclsGeneralForm = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Notes")
		
		On Error GoTo insPostCP005_err
		
		insPostCP005 = True
		
		Me.nVoucher = nVoucher
		Me.nLed_compan = nLed_compan
		Me.nLine = nLine
		Me.sAccount = sAccount
		Me.sAux_accoun = sAux_accoun
		Me.sClient = sClient
		Me.nCredit = lblCredit
		Me.dDate_doc = dDate_doc
		Me.nDebit = lblDebit
		Me.sDescript = sDescript
		Me.nDoc_type = nDoc_type
		Me.nDocNumber = nDocNumber
		Me.nNoteNum = nNoteNum
		Me.nOri_curr = nOri_curr
		Me.sStatregt = sStatregt
		Me.nUsercode = nUsercode
		Me.sCost_cente = sCost_cente
		Me.nExchange = nExchange
		Me.nOri_amo = nOri_amo
		
		If sAction <> "" Then
			If sAction = "Add" Then
				nAction = eFunctions.Menues.TypeActions.clngActionadd
			End If
			If sAction = "Update" Then
				nAction = eFunctions.Menues.TypeActions.clngActionUpdate
			End If
			If sAction = "Cut" Then
				nAction = eFunctions.Menues.TypeActions.clngActionCutTable
			End If
		End If
		
		'**+If the selected option is Register
		'+Si la opción seleccionada es Registrar
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				If lclsLed_compan.Find(nLed_compan) Then
					If CDate(dEffecdate) < CDate(lclsLed_compan.dDate_init) Then 'Permite asientos sobre meses cerrados...Reapertura.
						'**+Period included in only one countable month.
						'+Período comprendido en un solo mes contable.
						If VB.Day(lclsLed_compan.dDate_init) = 1 Then
							ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(CDate(dEffecdate)), VB.Day(lclsLed_compan.dDate_init))
						Else
							'**+Period included between two diferent month.
							'+Período comprendido entre dos meses distintos.
							If VB.Day(CDate(dEffecdate)) > VB.Day(lclsLed_compan.dDate_init) Then 'Se encuentra en el primer mes
								ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(CDate(dEffecdate)), VB.Day(lclsLed_compan.dDate_init))
							Else
								ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(System.Date.FromOADate(CDate(dEffecdate).ToOADate - 1)), VB.Day(lclsLed_compan.dDate_init))
							End If
						End If
						ldtmEndDate = DateSerial(Year(ldtmInitDate), Month(ldtmInitDate) + 1, VB.Day(ldtmInitDate) - 1)
						ldtmInitDateOpt = lclsLed_compan.dIniLedDat
						ldtmEndDateOpt = lclsLed_compan.dEndLedDat
						llngYear = lclsLed_compan.nYear
						
						If ldtmInitDate < (lclsLed_compan.dIniLedDat) Then 'Se reabre un período de un ejercicio anterior
							ldtmDate_tmp = lclsLed_compan.dDate_init
							
							Do While ldtmDate_tmp > ldtmInitDate
								ldtmDate_tmp = DateSerial(Year(ldtmDate_tmp) - 1, Month(ldtmDate_tmp), VB.Day(ldtmDate_tmp))
								ldtmInitDateOpt = DateSerial(Year(ldtmInitDateOpt) - 1, Month(ldtmInitDateOpt), VB.Day(ldtmInitDateOpt))
								
								If llngYear > 0 Then
									llngYear = llngYear - 1
								End If
							Loop 
							
							If llngYear = 0 Then
								llngYear = 1
							End If
							
							ldtmEndDateOpt = DateSerial(Year(ldtmInitDateOpt) + 1, Month(ldtmInitDateOpt), VB.Day(ldtmInitDateOpt) - 1)
						End If
						
						Call lclsLed_compan.ReverseMove(nLed_compan, ldtmInitDate, ldtmEndDate, ldtmInitDateOpt, ldtmEndDateOpt, llngYear, Month(ldtmInitDate), Year(ldtmInitDate), nUsercode, lclsLed_compan.sAccount_gp)
					End If
				End If
				
				If sWindowType <> "Normal" Then
					If nLine = 1 Then
						Call insCreAcc_transa(nLed_compan, Me.nVoucher, Me.nCredit, Me.nDebit, nNoteNum_transa, Me.nUsercode, dEffecdate, sStatregt, sDescript_transa, nOffiNum)
					Else
						Call lclsAcc_lineses.FindTotals(nLed_compan, Me.nVoucher)
						With pclsAcc_transa
							.nLed_compan = nLed_compan
							.nVoucher = nVoucher
							.sDescript = Trim(sDescript_transa)
							.nTot_credit = lclsAcc_lineses.nTot_Credits + CDbl(Me.nCredit)
							.nTot_debit = lclsAcc_lineses.nTot_Debits + CDbl(Me.nDebit)
							.nBalance = CDbl(.nTot_credit) - CDbl(.nTot_debit)
							.nUsercode = nUsercode
							.nNoteNum = nNoteNum_transa
							Call .Update()
						End With
					End If
					
					If insCreAcc_lines(nAction, sSel, Me.nDebit, Me.nCredit, dEffecdate, Me.sAccount, Me.sAux_accoun, Me.sCost_cente, sStatregt) Then
						insPostCP005 = True
					Else
						insPostCP005 = False
					End If
				ElseIf sWindowType = "Normal" Then 
					If (nDebit <> nCredit) And (sUnmat = "1") Then
						Call updLedger(nLed_compan, Me.nVoucher, "3")
					ElseIf (nDebit = nCredit) Then 
						Call updLedger(nLed_compan, Me.nVoucher, "1")
					End If
				End If
				'End If
				
				'**+If the selected option is Modify
				'+Si la opción seleccionada es Modificar
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				
				llngVoucher = CInt(nVoucher)
				
				With pclsAcc_transa
					.nLed_compan = nLed_compan
					.nVoucher = llngVoucher
					.nBalance = CDbl(lblDebit) - CDbl(lblCredit)
					.sDescript = Trim(sDescript_transa)
					.nTot_credit = CDbl(lblCredit)
					.nTot_debit = CDbl(lblDebit)
					.nUsercode = nUsercode
					.nNoteNum = nNoteNum_transa
					
					If .Update Then
						If insDelAcc_linesEach(nLed_compan, llngVoucher, Me.nLine) Then 'OJOJOJJOJOJOJ
							If insCreAcc_lines(nAction, sSel, Me.nDebit, Me.nCredit, dEffecdate, Me.sAccount, Me.sAux_accoun, Me.sCost_cente, sStatregt) Then
								insPostCP005 = True
							Else
								insPostCP005 = False
							End If
						End If
					End If
				End With
				
				'**+If the selected option is Voucher Cut
				'+Si la opción seleccionada es Cortar comprobante
				
			Case eFunctions.Menues.TypeActions.clngActionCutTable
				
				llngVoucher = CInt(nVoucher)
				
				If Not chkProcess = "1" Then
					If nNoteNum <> 0 Then
						If lclsGeneralForm.DeleteNote(nNoteNum) Then
						End If
					End If
					
					If insDelAcc_lines(nLed_compan, llngVoucher) Then
						If insDelAcc_transa(nNoteNum, nLed_compan, nVoucher) Then
							insPostCP005 = True
						Else
							insPostCP005 = False
						End If
					End If
				End If
				
		End Select
		
		If sAction = "Reverse" Then
			
			If lclsLed_compan.Find(nLed_compan) Then '**Permit countable establishments over close months...Reopening.
				If CDate(dEffecdate) < CDate(lclsLed_compan.dDate_init) Then 'Permite asientos sobre meses cerrados...Reapertura.
					
					'**+Period included in only one countable month.
					'+Período comprendido en un solo mes contable.
					
					If VB.Day(lclsLed_compan.dDate_init) = 1 Then
						ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(CDate(dEffecdate)), VB.Day(lclsLed_compan.dDate_init))
					Else
						
						'**+Period included between two diferent months.
						'+Período comprendido entre dos meses distintos.
						'**Is in the first month
						If VB.Day(CDate(dEffecdate)) > VB.Day(lclsLed_compan.dDate_init) Then 'Se encuentra en el primer mes
							ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(CDate(dEffecdate)), VB.Day(lclsLed_compan.dDate_init))
						Else
							ldtmInitDate = DateSerial(Year(CDate(dEffecdate)), Month(System.Date.FromOADate(CDate(dEffecdate).ToOADate - 1)), VB.Day(lclsLed_compan.dDate_init))
						End If
					End If
					
					ldtmEndDate = DateSerial(Year(ldtmInitDate), Month(ldtmInitDate) + 1, VB.Day(ldtmInitDate) - 1)
					ldtmInitDateOpt = lclsLed_compan.dIniLedDat
					ldtmEndDateOpt = lclsLed_compan.dEndLedDat
					llngYear = lclsLed_compan.nYear
					'** Is reopen a period of a previous exercise
					If ldtmInitDate < lclsLed_compan.dIniLedDat Then 'Se reabre un período de un ejercicio anterior
						ldtmDate_tmp = lclsLed_compan.dDate_init
						
						Do While ldtmDate_tmp > ldtmInitDate
							ldtmDate_tmp = DateSerial(Year(ldtmDate_tmp) - 1, Month(ldtmDate_tmp), VB.Day(ldtmDate_tmp))
							ldtmInitDateOpt = DateSerial(Year(ldtmInitDateOpt) - 1, Month(ldtmInitDateOpt), VB.Day(ldtmInitDateOpt))
							llngYear = llngYear - 1
						Loop 
						
						ldtmEndDateOpt = DateSerial(Year(ldtmInitDateOpt) + 1, Month(ldtmInitDateOpt), VB.Day(ldtmInitDateOpt) - 1)
					End If
					
					If lclsLed_compan.ReverseMove(nLed_compan, ldtmInitDate, ldtmEndDate, ldtmInitDateOpt, ldtmEndDateOpt, llngYear, Month(ldtmInitDate), Year(ldtmInitDate), nUsercode, lclsLed_compan.sAccount_gp) Then
					End If
				End If
			End If
			
			ldblDebit = Me.nDebit
			ldblCredit = Me.nCredit
			
			Me.nDebit = ldblCredit
			Me.nCredit = ldblDebit
			Me.nOri_curr = 0
			Me.nDoc_type = 0
			Me.nDocNumber = 0
			Me.dDate_doc = eRemoteDB.Constants.dtmNull
			Me.sClient = String.Empty
			Me.nNoteNum = 0
			If pclsAcc_transa.Find(nLed_compan, Me.nVoucher) Then
				If pclsAcc_transa.Update Then
					If insDelAcc_linesEach(nLed_compan, llngVoucher, Me.nLine) Then 'OJOJOJJOJOJOJ
						If insCreAcc_lines(nAction, sSel, Me.nDebit, Me.nCredit, dEffecdate, Me.sAccount, Me.sAux_accoun, Me.sCost_cente, sStatregt) Then
							insPostCP005 = True
						Else
							insPostCP005 = False
						End If
					End If
				End If
			Else
				If insCreAcc_transa(nLed_compan, Me.nVoucher, Me.nCredit, Me.nDebit, nNoteNum_transa, Me.nUsercode, dEffecdate, sStatregt, sDescript_transa, nOffiNum) Then
					If insDelAcc_linesEach(nLed_compan, llngVoucher, Me.nLine) Then 'OJOJOJJOJOJOJ
						If insCreAcc_lines(nAction, sSel, Me.nDebit, Me.nCredit, dEffecdate, Me.sAccount, Me.sAux_accoun, Me.sCost_cente, sStatregt) Then
							insPostCP005 = True
						Else
							insPostCP005 = False
						End If
					End If
				End If
			End If
			
		End If
		
		
insPostCP005_err: 
		If Err.Number Then
			insPostCP005 = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'**%insDelAcc_lines: function that delete records in the Acc_lines table.
	'%insDelAcc_lines:Función que permite borrar los registros en la tabla Acc_lines.
	Private Function insDelAcc_lines(ByVal nLed_compan As Integer, ByVal nVoucher As Integer) As Boolean
		Dim lrecdelAcc_lines As eRemoteDB.Execute
		
		lrecdelAcc_lines = New eRemoteDB.Execute
		
		insDelAcc_lines = False
		On Error GoTo insDelAcc_lines_err
		'**+ Excecute the delete store procedure
		'+ Se ejecuta el store procedure de eliminación
		
		With lrecdelAcc_lines
			.StoredProcedure = "delAcc_lines"
			
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelAcc_lines = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecdelAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelAcc_lines = Nothing
		
insDelAcc_lines_err: 
		If Err.Number Then
			insDelAcc_lines = False
		End If
	End Function
	
	'**%insCreAcc_lines: function that create records in the Acc_lines table.
	'%insCreAcc_lines:Función que permite crear los registros en la tabla Acc_lines.
	Private Function insCreAcc_lines(ByVal nAction As Integer, ByVal sSel As String, ByVal lblDebit As Double, ByVal lblCredit As Double, ByVal dEffecdate As Date, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal sCost_cente As String, ByVal sStatregt As String) As Boolean
		
		'**-Define the parameters to pass to the stored procedure.
		'-Se define el arreglo de parámetro a pasar al store procedure.
		
		Dim ldblSalGast As Double
		Dim ldblSalIng As Double
		Dim ldblSalVGP As Double
		Dim llngCount As Integer
		Dim llngLine As Integer
		
		Dim lreccreAcc_lines As eRemoteDB.Execute
		Dim mclsLed_compan As eLedge.Led_compan
		Dim mclsLedger_acc As eLedge.LedgerAcc
		
		lreccreAcc_lines = New eRemoteDB.Execute
		mclsLed_compan = New eLedge.Led_compan
		mclsLedger_acc = New eLedge.LedgerAcc
		
		On Error GoTo insCreAcc_lines_err
		insCreAcc_lines = False
		
		ldblSalGast = 0
		ldblSalIng = 0
		ldblSalVGP = 0
		
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And Trim(sSel) = "2" Then 'Cortado
			'llngLine = llngLine - 1
		Else
			
			'**+Parameters definition for the stored procedure 'insudb.creAcc_lines'
			'**+Data read on 06/20/2001 12:14:53 PM
			'+Definición de parámetros para stored procedure 'insudb.creAcc_lines'
			'+Información leída el 20/06/2001 12:14:53 PM
			Call mclsLed_compan.Find(nLed_compan)
			With lreccreAcc_lines
				.StoredProcedure = "creAcc_lines"
				
				.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLine", nLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If sAux_accoun = String.Empty Then
					.Parameters.Add("sAux_accoun", "                    ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					.Parameters.Add("sAux_accoun", Trim(sAux_accoun), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dDate_doc", dDate_doc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDoc_type", nDoc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDocnumber", nDocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Trim(CStr(nOri_curr)) <> CStr(eRemoteDB.Constants.intNull) Then
					.Parameters.Add("nOri_curr", nOri_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					If mclsLed_compan.nCurrency <> 0 Then
						.Parameters.Add("nOri_curr", mclsLed_compan.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						.Parameters.Add("nOri_curr", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
				End If
				.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOri_amo", nOri_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(False) Then
					insCreAcc_lines = True
				End If
			End With
			
			If CDbl(lblDebit) = CDbl(lblCredit) And mclsLed_compan.sBal_actu = "1" And CDate(dEffecdate) >= mclsLed_compan.dDate_init And CDate(dEffecdate) <= mclsLed_compan.dDate_end Then
				
				Call mclsLedger_acc.UpdateBalance(nLed_compan, CDate(dEffecdate), Trim(sAccount), Trim(sAux_accoun), Trim(sCost_cente), CDbl(nDebit), CDbl(nCredit), mclsLed_compan.nYear, mclsLed_compan.dIniLedDat, "2", mclsLed_compan.dDate_init)
				
				If Trim(sAccount) <> mclsLed_compan.sAccount_bg And Trim(sAccount) <> mclsLed_compan.sAccount_gp Then
					If mclsLedger_acc.sType_acc = "3" Or mclsLedger_acc.sType_acc = "4" Or mclsLedger_acc.sType_acc = "5" Then
						ldblSalGast = ldblSalGast + CDbl(nDebit)
						ldblSalIng = ldblSalIng + CDbl(nCredit)
					End If
				End If
			End If
		End If
		
		'UPGRADE_NOTE: Object lreccreAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreAcc_lines = Nothing
		
		If ldblSalGast <> 0 Or ldblSalIng <> 0 Then
			If mclsLed_compan.sAccount_bg <> " " Then
				Call mclsLedger_acc.UpdateBalance(nLed_compan, CDate(dEffecdate), mclsLed_compan.sAccount_bg, "", "", ldblSalGast, ldblSalIng, mclsLed_compan.nYear, mclsLed_compan.dIniLedDat, "2", mclsLed_compan.dDate_init)
			End If
			If mclsLed_compan.sAccount_gp <> " " Then
				Call mclsLedger_acc.UpdateBalance(nLed_compan, CDate(dEffecdate), mclsLed_compan.sAccount_gp, "", "", ldblSalGast, ldblSalIng, mclsLed_compan.nYear, mclsLed_compan.dIniLedDat, "2", mclsLed_compan.dDate_init)
			End If
			
		End If
		
insCreAcc_lines_err: 
		If Err.Number Then
			insCreAcc_lines = False
		End If
	End Function
	
	'**%insDelAcc_transa: function that delete records in the Acc_transa table.
	'%insDelAcc_transa:Función que permite borrar los registros en la tabla Acc_transa.
	Private Function insDelAcc_transa(ByVal gintNoteNum As Integer, ByVal glngLed_compan As Integer, ByVal llngVoucher As Integer) As Boolean
		
		Dim lclsNotes As Object
		Dim pclsAcc_transa As eLedge.Acc_transa
		
		lclsNotes = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Notes")
		
		pclsAcc_transa = New eLedge.Acc_transa
		
		insDelAcc_transa = False
		
		If gintNoteNum <> 0 Then
			
			With lclsNotes
				.nNoteNum = gintNoteNum
				If .DeleteNote(gintNoteNum) Then
					insDelAcc_transa = True
				End If
			End With
		End If
		
		'**+Excecute the Eliminate stored procedure
		'+ Se ejecuta el store procedure de eliminación
		With pclsAcc_transa
			.nLed_compan = glngLed_compan
			.nVoucher = llngVoucher
			
			If .Delete Then
				insDelAcc_transa = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNotes = Nothing
	End Function
	
	'**%insCreAcc_transa: function that create records in the Acc_transa table.
	'%insCreAcc_transa:Función que permite crear los registros en la tabla Acc_transa.
	Public Function insCreAcc_transa(ByVal nLed_compan As Integer, ByVal llngVoucher As Integer, ByVal lblCredit As Double, ByVal lblDebit As Double, ByVal nNoteNum_transa As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String, ByVal sDescript_transa As String, ByVal nOffiNum As Integer) As Boolean
		Dim pclsAcc_transa As eLedge.Acc_transa
		Dim mclsLed_compan As eLedge.Led_compan
		pclsAcc_transa = New eLedge.Acc_transa
		mclsLed_compan = New eLedge.Led_compan
		On Error GoTo insCreAcc_transa_err
		
		Call mclsLed_compan.Find(nLed_compan)
		With pclsAcc_transa
			.nLed_compan = nLed_compan
			.nVoucher = llngVoucher
			.nBalance = CDbl(lblDebit) - CDbl(lblCredit)
			.sDescript = Trim(sDescript_transa)
			.dEffecdate = CDate(dEffecdate)
			.sInd_automa = "2"
			.nNoteNum = nNoteNum_transa
			.sStatregt = sStatregt
			.nTot_credit = CDbl(lblCredit)
			.nTot_debit = CDbl(lblDebit)
			.nUsercode = nUsercode
			.nOffiNum = nOffiNum
			
			If CDbl(lblDebit) = CDbl(lblCredit) And mclsLed_compan.sBal_actu = "1" And CDate(dEffecdate) >= mclsLed_compan.dDate_init And CDate(dEffecdate) <= mclsLed_compan.dDate_end Then
				.sProcess_in = "1"
			Else
				.sProcess_in = "2"
			End If
			
			If .Add Then
				insCreAcc_transa = True
			End If
		End With
		
insCreAcc_transa_err: 
		If Err.Number Then
			insCreAcc_transa = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**%insDelAcc_linesEach: function to delete records in the Acc_lines table.
	'%insDelAcc_linesEach:Función que permite borrar los registros en la tabla Acc_lines.
	Public Function insDelAcc_linesEach(ByVal nLed_compan As Integer, ByVal nVoucher As Integer, ByVal nLine As Integer) As Boolean
		Dim lrecdelAcc_linesEach As eRemoteDB.Execute
		
		lrecdelAcc_linesEach = New eRemoteDB.Execute
		
		insDelAcc_linesEach = False
		On Error GoTo insDelAcc_linesEach_err
		
		
		'**+Parameters definition for the stored procedure 'insudb.delAcc_linesEach'
		'**+Data read on 06/27/2001 10:19:27 a.m.
		'+Definición de parámetros para stored procedure 'insudb.delAcc_linesEach'
		'+Información leída el 27/06/2001 10:19:27 a.m.
		
		With lrecdelAcc_linesEach
			.StoredProcedure = "delAcc_linesEach"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLine", nLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelAcc_linesEach = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelAcc_linesEach may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelAcc_linesEach = Nothing
		
insDelAcc_linesEach_err: 
		If Err.Number Then
			insDelAcc_linesEach = False
		End If
	End Function
	
	'**%insValCPL004_K: This function perform validations over the fields of the CPL004
	'%insValCPL004_K: Esta función se encarga de validar los datos introducidos en la CPL004
	Public Function insValCPL004_K(ByVal sCodispl As String, ByVal nLed_compan As Integer, Optional ByVal nYear As Integer = 0, Optional ByVal nMonth As Integer = 0, Optional ByVal noptProcess As Integer = 0, Optional ByVal dInitDate As Date = #12:00:00 AM#, Optional ByVal dEnddate As Date = #12:00:00 AM#, Optional ByVal sAccount As String = "", Optional ByVal sAux_accoun As String = "", Optional ByVal sCost_cente As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim mclsLedger_acc As eLedge.LedgerAcc
		Dim mclsTab_cost_c As eLedge.Tab_cost_c
		Dim mclsLed_compan As eLedge.Led_compan
		Dim dEffecdate As Date
		
		
		lclsErrors = New eFunctions.Errors
		mclsLedger_acc = New eLedge.LedgerAcc
		mclsTab_cost_c = New eLedge.Tab_cost_c
		mclsLed_compan = New eLedge.Led_compan
		
		
		On Error GoTo insValCPL004_K_Err
		
		'**+Validations related to column: nLed_compan
		'+ Se valida la columna: nLed_compan
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		'**+Validations related to column: nYear
		'+ Se valida la columna: nYear
		If (nYear = 0 Or nYear = eRemoteDB.Constants.intNull) And noptProcess = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'**+Validations related to column: nMonth
		'+ Se valida la columna: nMonth
		If (nMonth <> 0 Or nMonth <> eRemoteDB.Constants.intNull) And noptProcess = 0 Then
			dEffecdate = CDate("01/" & nMonth & "/" & nYear)
			If mclsLed_compan.Find(nLed_compan) Then
				If CDate(dEffecdate) > CDate(mclsLed_compan.dDate_end) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36037)
				Else
					If CDate(dEffecdate) < CDate(mclsLed_compan.dIniLedDat) Then
						Call lclsErrors.ErrorMessage(sCodispl, 736118)
					End If
				End If
			End If
		End If
		
		'+ Se valida la columna: dInitdate
		If noptProcess = 1 Then
			If dInitDate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9071)
			End If
			'+ Se valida la columna: dEnddate
			If dEnddate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7164)
			End If
		End If
		
		'**+Validations related to column: sAccount
		'+ Se valida la columna: sAccount
		If sAccount <> String.Empty Then
			If Trim(sAccount) <> "" Then
				If Not mclsLedger_acc.Find_AccountActive(nLed_compan, sAccount) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36010)
				Else
					If Not mclsLedger_acc.ValAccountStruc(nLed_compan, sAccount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36019)
					End If
				End If
			End If
		End If
		
		'**+Validations related to column: sAux_accoun
		'+ Se valida la columna: sAux_accoun
		If Trim(sAux_accoun) <> String.Empty Then
			If (sAccount) <> String.Empty Then
				If Not mclsLedger_acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36021)
				End If
			End If
		End If
		
		'**+Validations related to column: sCost_cente
		'+ Se valida la columna: sCost_cente
		If (sCost_cente) <> String.Empty Then
			If Not mclsTab_cost_c.Find(nLed_compan, sCost_cente) Then
				Call lclsErrors.ErrorMessage(sCodispl, 36050)
			Else
				If Not mclsTab_cost_c.Val_Unit_Organ_struct(nLed_compan, sCost_cente) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36073)
				End If
			End If
		End If
		
insValCPL004_K_Err: 
		If Err.Number Then
			insValCPL004_K = lclsErrors.Confirm & Err.Description
		End If
		
		On Error GoTo 0
		insValCPL004_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	
	'**% updLedger: update the state of the records from tables "Acc_transa" and "Acc_lines"
	'% updLedger: Actualiza el estado de los registros de las tablas "Acc_transa" y "Acc_lines"
	Public Function updLedger(ByVal nLed_compan As Integer, ByVal nVoucher As Integer, ByVal sStatregt As String) As Boolean
		
		Dim lrecupdLedger As eRemoteDB.Execute
		lrecupdLedger = New eRemoteDB.Execute
		
		On Error GoTo updLedger_Err
		
		updLedger = True
		'+ Definición de parámetros para stored procedure 'insudb.updLedger'
		'+ Información leída el 26/09/2001 03:24:32 p.m.
		
		With lrecupdLedger
			.StoredProcedure = "updLedger"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updLedger = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdLedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLedger = Nothing
		
updLedger_Err: 
		If Err.Number Then
			updLedger = False
		End If
	End Function
End Class






