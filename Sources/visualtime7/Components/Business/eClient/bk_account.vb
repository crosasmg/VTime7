Option Strict Off
Option Explicit On
Public Class bk_account
	'%-------------------------------------------------------%'
	'% $Workfile:: bk_account.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.bk_account al 04-20-2002 13:13:59
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sClient As String ' CHAR       14   0     0    N
	Public nBankExt As Integer ' NUMBER     22   0     10   N
	Public sAccount As String ' CHAR       25   0     0    N
	Public nTyp_acc As Integer ' NUMBER     22   0     5    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sDeposit As String ' CHAR       1    0     0    S
	'- Variables auxiliares
	Public sIndDirDebit As String

    Public test As String

	
	'- Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	Private Enum eAction
		clngInsert = 1
		clngUpdate = 2
		clngDelete = 3
	End Enum
	
	'%Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal sClient As String, ByVal nBankExt As Integer, ByVal sAccount As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		If Me.sClient = sClient And Not bFind Then
			Find = True
		Else
			With lrecreabk_account
				.StoredProcedure = "reabk_account"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.nBankExt = .FieldToClass("nBankext")
					Me.sAccount = .FieldToClass("sAccount")
					Me.sStatregt = .FieldToClass("sStatregt")
					Me.nTyp_acc = .FieldToClass("nTyp_acc")
					Me.sDeposit = .FieldToClass("sDeposit")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreabk_account = Nothing
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Agency: busca los datos correspondientes de la agencia bancaria según el cliente y banco
	Public Function Find_Agency(ByVal sClient As String, ByVal nBankExt As Integer, ByVal sAccount As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		If Me.sClient = sClient And Not bFind Then
			Find_Agency = True
		Else
			With lrecreabk_account
				.StoredProcedure = "reabk_account_o"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.nBankExt = .FieldToClass("nBankext")
					Me.sAccount = .FieldToClass("sAccount")
					Me.sStatregt = .FieldToClass("sStatregt")
					Me.nTyp_acc = .FieldToClass("nTyp_acc")
					Me.sDeposit = .FieldToClass("sDeposit")
					Find_Agency = True
					.RCloseRec()
				Else
					Find_Agency = False
				End If
			End With
			
		End If
Find_Err: 
		If Err.Number Then
			Find_Agency = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreabk_account = Nothing
	End Function
	
	'%Add: Agrega los datos correspondientes para un cliente, año y concepto específico
	Public Function Add() As Boolean
		Add = insUpdBk_Account(eAction.clngInsert)
	End Function
	
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Update = insUpdBk_Account(eAction.clngUpdate)
	End Function
	
	'%Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Delete = insUpdBk_Account(eAction.clngDelete)
	End Function
	
	'%insUpdBk_account: Esta función se encarga de realizar las actualizaciones de la tabla
	'%bk_account, correspodiente a las cuentas del cliente.
	Function insUpdBk_Account(ByVal nAction As Integer) As Boolean
		Dim lobjTime As eRemoteDB.Execute
		
		lobjTime = New eRemoteDB.Execute
		On Error GoTo insUpdBk_Account_Err
		
		With lobjTime
			.StoredProcedure = "insUpdbk_account"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntyp_acc", nTyp_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeposit", sDeposit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdBk_Account = .Run(False)
		End With
		
insUpdBk_Account_Err: 
		If Err.Number Then
			insUpdBk_Account = False
		End If
		'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTime = Nothing
		On Error GoTo 0
	End Function
	
	'%insValBC013Upd: Validaciones de la ventana PopUp de la transacción BC013.
	Public Function insValBC013Upd(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal sClient As String = "", Optional ByVal nBank_Code As Integer = 0, Optional ByVal sAccount As String = "", Optional ByVal sStatregt As String = "", Optional ByVal nTyp_acc As Integer = 0, Optional ByVal sDeposit As String = "") As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsDir_debit_cli As Dir_debit_cli
		
		On Error GoTo insValBC013Upd_Err
		
		lerrTime = New eFunctions.Errors
		lclsDir_debit_cli = New Dir_debit_cli
		
		With lerrTime
			
			'+ Validaciones del campo Banco
			If nBank_Code = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 7004)
			End If
			
			'+ Validaciones del campo Número de la cuenta
			If sAccount = String.Empty Then
				.ErrorMessage(sCodispl, 3058)
			Else
				If nBank_Code <> eRemoteDB.Constants.intNull Then
					If Trim(sAction) <> "Update" Then
						If Me.Find(sClient, nBank_Code, sAccount) Then
							.ErrorMessage(sCodispl, 12101)
						End If
					End If
				End If
			End If
			
			'+ Validacion si ya existe cuenta para Deposito
			If Exist_deposit(sClient, nBank_Code, sAccount, sDeposit) = 1 Then
				.ErrorMessage(sCodispl, 100100)
			End If
			'+Validación del tipo de cuenta
			If nTyp_acc = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 7030)
			End If
			
			'+Validaciones del campo estado
			If sStatregt = String.Empty Then
				.ErrorMessage(sCodispl, 1922)
			End If
			
			insValBC013Upd = .Confirm
		End With
		
insValBC013Upd_Err: 
		If Err.Number Then
			insValBC013Upd = insValBC013Upd & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsDir_debit_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDir_debit_cli = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsPostBC013Upd: Actualización de la ventana PopUp de la transacción BC013.
	Public Function InsPostBC013Upd(ByVal nAction As Integer, ByVal sClient As String, ByVal nBankExt As Integer, ByVal sAccount As String, ByVal sStatregt As String, ByVal nTyp_acc As Integer, ByVal nUsercode As Integer, ByVal sDeposit As String) As Boolean
		Dim lcolBk_account As bk_accounts
		Dim lclsClientWin As ClientWin
		Dim lstrContent As String
		Dim lclsDir_debit_cli As Dir_debit_cli
		
		On Error GoTo InsPostBC013Upd_Err
		
		lclsClientWin = New ClientWin
		
		Me.sClient = sClient
		Me.nBankExt = nBankExt
		Me.sAccount = sAccount
		Me.sStatregt = sStatregt
		Me.nTyp_acc = nTyp_acc
		Me.nUsercode = nUsercode
		Me.sDeposit = sDeposit
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				InsPostBC013Upd = Me.Add
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				InsPostBC013Upd = Me.Update
				If InsPostBC013Upd Then
					If sStatregt = "3" Then
						lclsDir_debit_cli = New Dir_debit_cli
						If lclsDir_debit_cli.Find(sClient, Today) Then
							If lclsDir_debit_cli.sTyp_dirdeb = "1" And lclsDir_debit_cli.nBankExt = nBankExt And lclsDir_debit_cli.sAccount = sAccount Then
								lclsClientWin.insUpdClient_win(sClient, "BC015", "3")
							End If
						End If
					End If
				End If
			Case eFunctions.Menues.TypeActions.clngActioncut
				InsPostBC013Upd = Me.Delete
		End Select
		
		If InsPostBC013Upd Then
			lcolBk_account = New bk_accounts
			lstrContent = IIf(lcolBk_account.Find(sClient), "2", "1")
			lclsClientWin.insUpdClient_win(sClient, "BC013", lstrContent)
		End If
		
InsPostBC013Upd_Err: 
		If Err.Number Then
			InsPostBC013Upd = False
		End If
		'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClientWin = Nothing
		'UPGRADE_NOTE: Object lcolBk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolBk_account = Nothing
		'UPGRADE_NOTE: Object lclsDir_debit_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDir_debit_cli = Nothing
		On Error GoTo 0
	End Function
	
	'%Count: Obtiene el número de cuentas asociadas a un cliente
	Public ReadOnly Property Count(ByVal sClient As String) As Integer
		Get
			Dim lobjTime As eRemoteDB.Execute
			
			lobjTime = New eRemoteDB.Execute
			On Error GoTo Count_Err
			
			With lobjTime
				.StoredProcedure = "InsCountAccount"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Count = .FieldToClass("nCount")
				Else
					Count = 0
				End If
			End With
			
Count_Err: 
			If Err.Number Then
				Count = 0
			End If
			'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjTime = Nothing
			On Error GoTo 0
		End Get
	End Property
	
	'%GetQuantityAccounts: Obtiene la cantidad de cuentas de un cliente
	Public ReadOnly Property GetQuantityAccounts(ByVal sClient As String) As Integer
		Get
			Dim lrecreabk_account As eRemoteDB.Execute
			
			On Error GoTo GetQuantityAccounts_Err
			lrecreabk_account = New eRemoteDB.Execute
			GetQuantityAccounts = 0
			
			With lrecreabk_account
				.StoredProcedure = "QuantityAccounts"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					GetQuantityAccounts = .FieldToClass("Count")
				End If
			End With
			
GetQuantityAccounts_Err: 
			If Err.Number Then
				GetQuantityAccounts = 0
			End If
			On Error GoTo 0
			'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreabk_account = Nothing
		End Get
	End Property
	
	'%DeleteAll: Elimina las cuentas asociadas a un cliente
	Public Function DeleteAll(ByVal sClient As String) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		With lrecreabk_account
			.StoredProcedure = "Delbk_Account"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteAll = .Run(False)
		End With
		
DeleteAll_Err: 
		If Err.Number Then
			DeleteAll = False
		End If
		'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreabk_account = Nothing
		On Error GoTo 0
	End Function
	'%Fid_count: retorna verdadero cuando existen cuentas asociadas al cliente
	'%           tipo de cuenta
	Public Function Find_count(ByVal sClient As String, ByVal nTyp_acc As Double) As Boolean
		Dim lrecrea_bk_account_count As eRemoteDB.Execute
		On Error GoTo rea_bk_account_count_Err
		
		lrecrea_bk_account_count = New eRemoteDB.Execute
		
		With lrecrea_bk_account_count
			.StoredProcedure = "rea_bk_account_count"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acc", nTyp_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nCount").Value > 0 Then
					Find_count = True
				Else
					Find_count = False
				End If
			Else
				Find_count = False
			End If
		End With
		
rea_bk_account_count_Err: 
		If Err.Number Then
			Find_count = False
		End If
		'UPGRADE_NOTE: Object lrecrea_bk_account_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecrea_bk_account_count = Nothing
		On Error GoTo 0
		
		
	End Function
	'% Exist_deposit: retorna si existe o no una cuenta deposito para un cliente
	Private Function Exist_deposit(ByVal sClient As String, ByVal nBankExt As Integer, ByVal sAccount As String, ByVal sDeposit As String) As Integer
		Dim lrecBk_Account_Exist As New eRemoteDB.Execute
		Dim llngExist As Integer
		
		On Error GoTo Exist_deposit_err
		
		llngExist = 0
		
		'+ Duplica el registro correspondiente en claim_caus
		With lrecBk_Account_Exist
			.StoredProcedure = "Bk_Account_Exist"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeposit", sDeposit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", llngExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				llngExist = .Parameters("nExist").Value
			Else
				llngExist = 0
			End If
		End With
		
		Exist_deposit = llngExist
		
Exist_deposit_err: 
		If Err.Number Then
			Exist_deposit = 0
		End If
		'UPGRADE_NOTE: Object lrecBk_Account_Exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBk_Account_Exist = Nothing
		On Error GoTo 0
	End Function
	'%Class_Initialize: Inicialización de las variables públicas
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sClient = String.Empty
		nBankExt = eRemoteDB.Constants.intNull
		sAccount = String.Empty
		sStatregt = String.Empty
		nTyp_acc = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sIndDirDebit = String.Empty
		sDeposit = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






