Option Strict Off
Option Explicit On
Public Class Cheq_book
	'%-------------------------------------------------------%'
	'% $Workfile:: Cheq_book.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according to the table in the system as of March 1st, 2001.
	'**+The key fields of the table correspond to: "nAcc_bank" and "dEffecdate"
	'+Propiedades según la tabla en el sistema al 1/03/2001.
	'+Los campos llave de la tabla corresponden a: "nAcc_bank" y "dEffecdate"
	
	'   Column_name                    Type      Computed  Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nAcc_bank As Integer 'smallint     no       2           5     0     no           (n/a)                (n/a)
	Public dEffecdate As Date 'datetime     no       8                       no           (n/a)                (n/a)
	Public sCheque_end As String 'char         no      10                       yes          no                   yes
	Public sCheque_las As String 'char         no      10                       yes          no                   yes
	Public sCheque_sta As String 'char         no      10                       yes          no                   yes
	Public dNulldate As Date 'datetime     no       8                       yes          (n/a)                (n/a)
	Public nQ_che_dama As Integer 'smallint     no       2           5     0     yes          (n/a)                (n/a)
	Public nQ_che_null As Integer 'smallint     no       2           5     0     yes          (n/a)                (n/a)
	Public nUsercode As Integer 'smallint     no       2           5     0     yes          (n/a)                (n/a)
	
	'**-Auxiliary variables
	'-Variables auxiliares
	
	Public nResponse As Integer
	
	
	'**-Utilized in the routines that are found inside of "insPostOP010"
	'-Utilizadas en las rutinas que se encuentran dentro de "insPostOP010"
	
	Private mdtmEffecdate As Date
	Private mintAcc_bank As Integer
	Private mlngCheque_sta As Integer
	Private mlngCheque_end As Integer
	Private mlngCheque_las As Integer
	Private mintQ_che_dama As Integer
	Private mintQ_che_null As Integer
	Private mintUsercode As Integer
	Private mblnCheqRangeChange As Boolean
	
	'**%Find: Restores the general information of a specific checkbook
	'**%(associated with a bank account code)
	'%Find: Devuelve la información general de una chequera específica
	'%(asociada a un código de cuenta bancaria)
	Public Function Find(ByVal nAcc_bank As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-The variable that determines the result of the function (True/False) is declared
		'-Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'**-The variable lrecreaCheq_book is declared
		'-Se define la variable lrecreaCheq_book
		
		Dim lrecreaCheq_book As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCheq_book = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaCheq_book1
		'**+Information read on March 1st, 2001  20:27:45 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaCheq_book'
		'+Información leída el 01/03/2001 10:27:45 a.m.
		
		If Me.nAcc_bank <> nAcc_bank Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			Me.nAcc_bank = nAcc_bank
			Me.dEffecdate = dEffecdate
			
			With lrecreaCheq_book
				.StoredProcedure = "reaCheq_book"
				.Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sCheque_sta = .FieldToClass("sCheque_sta")
					sCheque_end = .FieldToClass("sCheque_end")
					sCheque_las = .FieldToClass("sCheque_las")
					nQ_che_dama = .FieldToClass("nQ_che_dama", 0)
					nQ_che_null = .FieldToClass("nQ_che_null", 0)
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaCheq_book may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheq_book = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindLastEffecdate: Validates that a date is the same or after the date of the last
	'**%modification to the checkbook
	'%FindLastEffecdate: Valida si una fecha es posterior o igual
	'%a la fecha de última modificacion de la chequera
	Public Function FindLastEffecdate(ByVal nAcc_bank As Integer, ByVal dUserdate As Date) As Boolean
		
		'**-The variable lrecreaCheq_book_lastEffecdate is declared
		'-Se define la variable lrecreaCheq_book_lastEffecdate
		
		Dim lrecreaCheq_book_lastEffecdate As eRemoteDB.Execute
		
		On Error GoTo FindLastEffecdate_Err
		
		lrecreaCheq_book_lastEffecdate = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaCheq_book_lastEffecdate'
		'**+Information read on March 1st, 2001  11:34:28 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaCheq_book_lastEffecdate'
		'+Información leída el 01/03/2001 11:34:28 a.m.
		
		With lrecreaCheq_book_lastEffecdate
			.StoredProcedure = "reaCheq_book_lastEffecdate"
			.Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dUserdate", dUserdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nResponse = .FieldToClass("nResponse")
				If nResponse <> 1 Then
					FindLastEffecdate = False
				Else
					FindLastEffecdate = True
				End If
				.RCloseRec()
			Else
				FindLastEffecdate = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCheq_book_lastEffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheq_book_lastEffecdate = Nothing
		
FindLastEffecdate_Err: 
		If Err.Number Then
			FindLastEffecdate = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Cheq_book".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Cheq_book". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		'**-The variable lrecupdCheq_book is declared
		'-Se define la variable lrecupdCheq_book
		
		Dim lrecupdCheq_book As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdCheq_book = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updCheq_book'
		'**+Information read on March 1st, 2001  01:44:43 p.m.
		'+Definición de parámetros para stored procedure 'insudb.updCheq_book'
		'+Información leída el 01/03/2001 01:44:43 p.m.
		
		With lrecupdCheq_book
			.StoredProcedure = "updCheq_book"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_sta", sCheque_sta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_end", sCheque_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_las", sCheque_las, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_che_dama", nQ_che_dama, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_che_null", nQ_che_null, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdCheq_book may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCheq_book = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOP010_K: This method validates the header section of the page "OP010_K" as described in the
	'**%functional specifications
	'%InsValOP010_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OP010_K"
	Public Function insValOP010_K(ByVal sCodispl As String, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lstrAccDesc As String
		
		On Error GoTo insValOP010_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+Validation of the field "Bank Account"
		'+Validación del campo "Cuenta Bancaria"
		
		If nAcc_bank = 0 Then
			
			'**+The field "Account" should not be null
			'+El campo "Cuenta" debe estar lleno
			
			Call lclsErrors.ErrorMessage(sCodispl, 7002)
		Else
			lstrAccDesc = insReaBank_acc(nAcc_bank)
			If Trim(lstrAccDesc) = String.Empty Then
				
				'**+Should be registered in the system
				'+Debe estar registrado en el sistema
				
				Call lclsErrors.ErrorMessage(sCodispl, 7013)
			ElseIf Trim(lstrAccDesc) = "IT IS NOT CURRENT ACCOUNT" Then 
				Call lclsErrors.ErrorMessage(sCodispl, 7146)
			End If
		End If
		
		'**+Validation of the field "Date"
		'+Validación del campo "Fecha"
		
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7079)
		End If
		
		'**+The date must be the same or after the date of the last modification of the record
		'+La fecha debe ser posterior o igual a la fecha de última modificación del registro
		
		If Not FindLastEffecdate(nAcc_bank, dEffecdate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7038)
		End If
		
		insValOP010_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP010_K_Err: 
		If Err.Number Then
			insValOP010_K = insValOP010_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOP010: This method validates the page "OP010" as described in the functional specifications
	'%InsValOP010: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "OP010"
	Public Function insValOP010(ByVal sCodispl As String, Optional ByVal nCheque_sta As Integer = 0, Optional ByVal nCheque_end As Integer = 0, Optional ByVal nCheque_las As Integer = 0, Optional ByVal bWarning As Boolean = False) As String
		
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOP010_Err
		
		'**+Validation of the field "Check number- First"
		'+Validación del campo "Número de Cheque-Inicial"
		
		If nCheque_sta = eRemoteDB.Constants.intNull Then
			
			'**+This field should not be null
			'+Este campo debe estar lleno
			
			Call lclsErrors.ErrorMessage(sCodispl, 7080)
		End If
		
		'**+Validation of the field "Check number- Final"
		'+Validación del campo "Número de Cheque-Final"
		
		If nCheque_end = eRemoteDB.Constants.intNull Then
			
			'**+This field should not be null
			'+Este campo debe estar lleno
			
			Call lclsErrors.ErrorMessage(sCodispl, 7081)
		ElseIf nCheque_sta <> eRemoteDB.Constants.intNull Then 
			
			'**+Should be greater than the number of the first check
			'+Debe ser posterior al número de cheque inicial
			
			If CDbl(nCheque_end) <= CDbl(nCheque_sta) Then
				Call lclsErrors.ErrorMessage(sCodispl, 7082)
			End If
		End If
		
		'**+Validation of the field "Check number- Last printed"
		'+Validación del campo "Número de Cheque-Último Impreso"
		
		If nCheque_las <> eRemoteDB.Constants.intNull And nCheque_las <> 0 Then
			If nCheque_sta <> eRemoteDB.Constants.intNull Then
				If CDbl(nCheque_las) < CDbl(nCheque_sta) Then
					
					'**+If this field should be required, the number should be the same as or greater than the number of the first check
					'+Si este campo debe estar lleno, debe ser posterior o igual al número de cheque inicial
					
					Call lclsErrors.ErrorMessage(sCodispl, 7083)
				ElseIf nCheque_end <> eRemoteDB.Constants.intNull Then 
					If CDbl(nCheque_las) > CDbl(nCheque_end) Then
						
						'**+If this field is not null, the number should be the same as or less than the number of the first check
						'+Si este campo está lleno, debe ser anterior o igual al número de cheque final
						
						Call lclsErrors.ErrorMessage(sCodispl, 7084)
					End If
				End If
			End If
		End If
		
		'**+A warning is sent if the user has changed the range corresponding to the checks (first and/or final)
		'+Se envía una advertencia si el usuario cambió el rango correspondiente a los cheques (inicial y/o final)
		
		If bWarning Then
			Call lclsErrors.ErrorMessage(sCodispl, 7085)
		End If
		
		insValOP010 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP010_Err: 
		If Err.Number Then
			insValOP010 = insValOP010 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostOP010_K: This method updates the database (as described in the functional specifications)
	'**%for the page "OP010_K"
	'%insPostOP010_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP010_K"
	Public Function insPostOP010_K() As Boolean
		
		insPostOP010_K = True
	End Function
	
	'**%insPostOP010: This method updates the database (as described in the functional specifications)
	'**%for the page "OP010"
	'%insPostOP010: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP010"
	Public Function insPostOP010(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal nCheque_sta As Integer = 0, Optional ByVal nCheque_end As Integer = 0, Optional ByVal nCheque_las As Integer = 0, Optional ByVal nQ_che_dama As Integer = 0, Optional ByVal nQ_che_null As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal bCheqRangeChange As Boolean = False) As Boolean
		
		On Error GoTo insPostOP010_Err
		
		mdtmEffecdate = dEffecdate
		mintAcc_bank = nAcc_bank
		mlngCheque_sta = nCheque_sta
		mlngCheque_end = nCheque_end
		mlngCheque_las = nCheque_las
		mintQ_che_dama = nQ_che_dama
		mintQ_che_null = nQ_che_null
		mintUsercode = nUsercode
		mblnCheqRangeChange = bCheqRangeChange
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				insPostOP010 = insUpdCheq_book()
			Case eFunctions.Menues.TypeActions.clngActionQuery
				'            insPostOP010 = insReaCheq_book()
			Case Else
				insPostOP010 = False
		End Select
		
		If sCodispl <> "OP010" Then
			insPostOP010 = insUpdCheq_book()
		End If
		
insPostOP010_Err: 
		If Err.Number Then
			insPostOP010 = False
		End If
		On Error GoTo 0
	End Function
	
	'**@@@@@@@@@@@@@@@@@@@@ NECCESARY ROUTINES FOR THE EXCECUTION @@@@@@@@@@@@@@@@@@@@
	'**@@@@@@@@@@@@@@@@@@@@ OF THE FUNCTIONS VAL AND POST         @@@@@@@@@@@@@@@@@@@@
	
	'@@@@@@@@@@@@@@@@@@@@ RUTINAS NECESARIAS PARA LA EJECUCIÓN DE @@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@ DE LAS FUNCIONES VAL Y POST             @@@@@@@@@@@@@@@@@@@@
	
	'**%insReaBank_acc: This function is in charge of reading the bank account information
	'**%to validate if it is registered in the system
	'%insReaBank_acc: Es la encargada de leer la información de las cuentas bancarias
	'%para validar si está registrada en el sistema
	Public Function insReaBank_acc(ByVal intAcc_bank As Integer) As String
		
		Dim lclsBank_acc As eCashBank.Bank_acc
		
		On Error GoTo insReaBank_acc_Err
		
		lclsBank_acc = New eCashBank.Bank_acc
		
		If Not lclsBank_acc.Find_O(intAcc_bank) Then
			insReaBank_acc = String.Empty
		Else
			If lclsBank_acc.nAcc_type <> 2 Then
				insReaBank_acc = "IT IS NOT CURRENT ACCOUNT"
			Else
				insReaBank_acc = lclsBank_acc.sShort_des & " " & lclsBank_acc.sDescript
			End If
		End If
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		
insReaBank_acc_Err: 
		If Err.Number Then
			insReaBank_acc = String.Empty
		End If
		On Error GoTo 0
	End Function
	
	'**%insUpdCheq_book: Updates the general information of the companys check books
	'%insUpdCheq_book: Actualiza la información general de las chequeras de la empresa
	Private Function insUpdCheq_book() As Boolean
		
		Dim lclsCheq_book As eCashBank.Cheq_book
		Dim lintCheqCancel As Integer
		
		On Error GoTo insUpdCheq_book_Err
		
		lclsCheq_book = New eCashBank.Cheq_book
		
		insUpdCheq_book = True
		
		'**+When the user makes a change to the range of checks associated to the checkbook, the amount
		'**+of checks annuled from that range in the file of applied checks is recalculated
		'+Cuando el usuario realiza un cambio en el rango de cheques asociado a la chequera, se recalcula
		'+la cantidad de cheques anulados de ese rango en el archivo de cheques solicitados
		
		If mblnCheqRangeChange Then
			lintCheqCancel = insReaCheqCancel(mintAcc_bank, CStr(mlngCheque_sta), CStr(mlngCheque_end))
			If lintCheqCancel = -1 Then
				insUpdCheq_book = False
			End If
		End If
		
		If insUpdCheq_book Then
			With lclsCheq_book
				.nAcc_bank = mintAcc_bank
				.dEffecdate = mdtmEffecdate
				.sCheque_sta = CStr(mlngCheque_sta)
				.sCheque_end = CStr(mlngCheque_end)
				.sCheque_las = CStr(mlngCheque_las)
				.nQ_che_dama = mintQ_che_dama
				.nQ_che_null = mintQ_che_null
				.nUsercode = mintUsercode
				
				If Not .Update Then
					insUpdCheq_book = False
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lclsCheq_book may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCheq_book = Nothing
		
insUpdCheq_book_Err: 
		If Err.Number Then
			insUpdCheq_book = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insReaCheqCancel: this function is in charge of calculating the amount of canceled checks in a checkbook
	'**%associated to a bank account, within a range of specific checks
	'%insReaCheqCancel: esta función calcula la cantidad de cheques cancelados de una chequera asociada
	'%a una cuenta bancaria, dentro de un rango de cheques específico
	Public Function insReaCheqCancel(ByVal intAcc_bank As Integer, ByVal strCheqInit As String, ByVal strCheqEnd As String) As Integer
		
		Dim lclsCheque As eCashBank.Cheque
		lclsCheque = New eCashBank.Cheque
		
		If Not lclsCheque.CountCanceled(intAcc_bank, strCheqInit, strCheqEnd) Then
			insReaCheqCancel = -1
		Else
			insReaCheqCancel = lclsCheque.nCount_canceled
		End If
		
		'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCheque = Nothing
	End Function
	
	'**%insReaCheqIssue: This function calculates the amount of checks emmitted from a checkbook associated
	'**%to a bank account, inside of a range of specific checks
	'%insReaCheqIssue: Esta función calcula la cantidad de cheques emitidos de una chequera asociada
	'%a una cuenta bancaria, dentro de un rango de cheques específico
	Public Function insReaCheqIssue(ByVal nAcc_bank As Integer, ByVal sCheqInit As String, ByVal sCheqEnd As String) As Integer
		Dim lclsCheque As Cheque
		lclsCheque = New Cheque
		
		If Not lclsCheque.CountEmited(nAcc_bank, sCheqInit, sCheqEnd) Then
			insReaCheqIssue = -1
		Else
			insReaCheqIssue = lclsCheque.nCount_emited
		End If
		
		'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCheque = Nothing
	End Function
End Class






