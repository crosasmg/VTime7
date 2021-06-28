Option Strict Off
Option Explicit On
Public Class Dir_debit_cli
	'%-------------------------------------------------------%'
	'% $Workfile:: Dir_debit_cli.cls                        $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema 11/01/2000
	'- Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'- Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'- ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public sClient As String
	Public dEffecdate As Date
	Public sAccount As String
	Public dNulldate As Date
	Public sTyp_dirdeb As String
	Public sBankAuth As String
	Public nBankExt As Integer
	Public sCredi_card As String
	Public nCard_Type As Integer
	Public dCardexpir As Date
	Public nUsercode As Integer
	Public nBill_Day As Integer
	
	'-Variables auxiliares
	Public bDisabledChk_Del As Boolean
	Public bDisabledOpt_Bk As Boolean
	Public bDisabledOpt_Cred As Boolean
	Public sTableAccount As String
	Public bDisabledForm As Boolean
	
	Public sCliename As String
	Public sFirstname As String
	Public sLastname As String
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public npolicy As Double
	Public ncertif As Double
	
	'- Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	Private Enum eAction
		clngInsert = 1
		clngUpdate = 2
		clngDelete = 3
	End Enum
	
	'% Find: Busca los datos del pago automático por banco o tarjeta de crédito, asociados al cliente.
	Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaDir_debit_cli As eRemoteDB.Execute
		Dim lobjbk_account As eClient.bk_account
		Dim lobjCred_card As eClient.cred_card
		
		On Error GoTo Find_Err
		lrecreaDir_debit_cli = New eRemoteDB.Execute
		
		If Me.sClient = sClient And Me.dEffecdate = dEffecdate And Not bFind Then
			Find = True
		Else
			With lrecreaDir_debit_cli
				.StoredProcedure = "reaDir_debit_cli"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					sAccount = .FieldToClass("sAccount")
					dNulldate = .FieldToClass("dNulldate")
					sTyp_dirdeb = .FieldToClass("sTyp_DirDeb")
					nBankExt = .FieldToClass("nBankext")
					sBankAuth = .FieldToClass("sBankAuth")
					nBill_Day = .FieldToClass("nBill_day")
					If sTyp_dirdeb = "1" Then
						lobjbk_account = New bk_account
						If lobjbk_account.Find(sClient, nBankExt, sAccount) Then
						End If
						'UPGRADE_NOTE: Object lobjbk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjbk_account = Nothing
					Else
						lobjCred_card = New cred_card
						If lobjCred_card.Find(sClient, nBankExt, sAccount) Then
							nCard_Type = lobjCred_card.nCard_Type
							dCardexpir = lobjCred_card.dCardexpir
							sCredi_card = lobjCred_card.sCredi_card
						End If
						'UPGRADE_NOTE: Object lobjCred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjCred_card = Nothing
					End If
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDir_debit_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDir_debit_cli = Nothing
	End Function
	
	'% Add: Agrega los datos correspondientes para un cliente, año y concepto específico
	Public Function Add() As Boolean
		Add = insUpdDir_debit_cli(eAction.clngInsert)
	End Function
	
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Update = insUpdDir_debit_cli(eAction.clngUpdate)
	End Function
	
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Delete = insUpdDir_debit_cli(eAction.clngDelete)
	End Function
	
	'% insUpdDir_debit_cli: Esta funcion se encarga de realizar las actualizaciones de la tabla
	'%                      Dir_debit_cli, correspodiente a las cuentas del cliente.
	Function insUpdDir_debit_cli(ByVal nAction As Integer) As Boolean
		Dim lobjTime As eRemoteDB.Execute
		
		On Error GoTo insUpdDir_debit_cli_Err
		lobjTime = New eRemoteDB.Execute
		
		With lobjTime
			.StoredProcedure = "insUpdDir_debit_cli"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_DirDeb", sTyp_dirdeb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankAuth", sBankAuth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_Day", nBill_Day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdDir_debit_cli = .Run(False)
		End With
insUpdDir_debit_cli_Err: 
		If Err.Number Then
			insUpdDir_debit_cli = False
		End If
		'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTime = Nothing
		On Error GoTo 0
	End Function
	
	'% FindPol: Busca los datos del pago automático por banco o tarjeta de crédito, asociados a la póliza.
	Public Function FindPol(ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaDir_debit_Pol As eRemoteDB.Execute
		
		On Error GoTo FindPol_Err
		lrecreaDir_debit_Pol = New eRemoteDB.Execute
		
		If Me.sClient = sClient And Not bFind Then
			FindPol = True
		Else
			With lrecreaDir_debit_Pol
				.StoredProcedure = "valdir_debit_pol"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					FindPol = True
				Else
					FindPol = False
				End If
			End With
		End If
		
FindPol_Err: 
		If Err.Number Then
			FindPol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDir_debit_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDir_debit_Pol = Nothing
	End Function
	
	'% insValBC015: Valida campos de Domicialización Bancaria de secuencia Clientes
	Public Function insValBC015(ByVal sCodispl As String, ByVal sClient As String, Optional ByVal sType_Dir As String = "", Optional ByVal nBank_Code As Integer = 0, Optional ByVal sAccount As String = "", Optional ByVal sBankAuth As String = "", Optional ByVal sDelDir_debit As String = "", Optional ByVal nBill_Day As Integer = 0) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsBk_account As bk_account
		Dim lclsCred_card As cred_card
		Dim lblnErr As Boolean
		
		On Error GoTo insValBC015_Err
		lerrTime = New eFunctions.Errors
		
		lblnErr = False
		
		With lerrTime
			'+Si no se desea eliminar la domiciliación
			If sDelDir_debit <> "1" Then
				
				'+ Se valida el Banco
				If nBank_Code = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 7004)
					lblnErr = True
				End If
				
				'+ Se realizan las validaciones del número de cuenta bancaria o tarjeta
				'+ Numero de cuenta debe estar activa
				If sAccount = String.Empty Then
					.ErrorMessage(sCodispl, 3058)
				ElseIf Not lblnErr Then 
					'+ Si la domiliciación es por Banco
					If sType_Dir = "1" Then
						lclsBk_account = New bk_account
						If Not lclsBk_account.Find_Agency(sClient, nBank_Code, sAccount) Then
							.ErrorMessage(sCodispl, 7093)
						Else
							If CDbl(lclsBk_account.sStatregt) <> 1 Then
								.ErrorMessage(sCodispl, 55027)
							End If
						End If
						'UPGRADE_NOTE: Object lclsBk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBk_account = Nothing
					Else
						'+ Si la domiliciación es por Tarjeta de Credito
						lclsCred_card = New cred_card
						If Not lclsCred_card.Find(sClient, nBank_Code, sAccount) Then
							.ErrorMessage(sCodispl, 7093)
						Else
							If CDbl(lclsCred_card.sStatregt) <> 1 Then
								.ErrorMessage(sCodispl, 55027)
							End If
						End If
						'UPGRADE_NOTE: Object lclsCred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCred_card = Nothing
					End If
				End If
				
				'+ Se realizan las validaciones del número de mandato
				If sBankAuth = String.Empty Then
					.ErrorMessage(sCodispl, 55007)
				End If
				'+ Se realizan las validaciones del día de Pago
				If nBill_Day = eRemoteDB.Constants.intNull Or nBill_Day = 0 Then
					.ErrorMessage(sCodispl, 55646)
				Else
					If nBill_Day > 30 Or nBill_Day < 1 Then
						.ErrorMessage(sCodispl, 55841)
					End If
				End If
				
			Else
				If FindPol(sClient) Then
					.ErrorMessage(sCodispl, 2815)
				End If
			End If
			
			insValBC015 = .Confirm
		End With
		
insValBC015_Err: 
		If Err.Number Then
			insValBC015 = insValBC015 & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'% insPostBC015: Modificación de registros de Domiciliación
	Public Function insPostBC015(ByVal nAction As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal sTyp_dirdeb As String, ByVal nBank_Code As Integer, ByVal sAccount As String, ByVal sBankAuth As String, ByVal sDelDir_debit As String, ByVal nUsercode As Integer, ByVal nBill_Day As Integer) As Boolean
		Dim lclsClientWin As ClientWin
		Dim lclsClient As Client
		Dim lstrContent As String
		
		On Error GoTo InsPostBC015_Err
		
		lstrContent = "2"
		With Me
			If sDelDir_debit = "1" Then
				.sClient = sClient
				.dEffecdate = dEffecdate
				.sAccount = sAccount
				.sTyp_dirdeb = sTyp_dirdeb
				.sBankAuth = sBankAuth
				.nUsercode = nUsercode
				.nBankExt = nBank_Code
				.sCredi_card = sAccount
				.nBill_Day = nBill_Day
				lstrContent = "1"
				insPostBC015 = Delete
			Else
				.sClient = sClient
				.nUsercode = nUsercode
				.nBankExt = nBank_Code
				.sBankAuth = sBankAuth
				.nBill_Day = nBill_Day
				If sTyp_dirdeb = "1" Then '+ Domiciliación por cuenta bancaria
					.sAccount = sAccount
					.sTyp_dirdeb = CStr(1)
				Else
					.sCredi_card = sAccount
					.sTyp_dirdeb = CStr(2)
				End If
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					lclsClient = New Client
					lclsClient.Find(sClient)
					If lclsClient.dInpdate = eRemoteDB.Constants.dtmNull Then
						.dEffecdate = Today
					Else
						.dEffecdate = lclsClient.dInpdate
					End If
					'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClient = Nothing
					insPostBC015 = Add
				Else
					.dEffecdate = Today
					insPostBC015 = Update
				End If
			End If
		End With
		
		If insPostBC015 Then
			lclsClientWin = New ClientWin
			lclsClientWin.insUpdClient_win(sClient, "BC015", lstrContent)
			'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsClientWin = Nothing
		End If
		
InsPostBC015_Err: 
		If Err.Number Then
			insPostBC015 = False
		End If
	End Function
	
	' InsPreBC015: Obtiene información de la ventana de domiciliación
	Public Function InsPreBC015(ByVal sClient As String, ByVal nAction As Integer) As Boolean
		Dim lclsClient As eClient.Client
		Dim ldtmEffecdate As Date
		Dim lclsCred_card As eClient.cred_card
		Dim lclsBk_account As eClient.bk_account
		
		On Error GoTo InsPreBC015_Err
		lclsBk_account = New eClient.bk_account
		lclsCred_card = New eClient.cred_card
		
		InsPreBC015 = True
		bDisabledChk_Del = True
		ldtmEffecdate = eRemoteDB.Constants.dtmNull
		bDisabledOpt_Bk = False
		bDisabledOpt_Cred = False
		bDisabledForm = False
		sTyp_dirdeb = "1"
		sTableAccount = "tabbk_account"
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			lclsClient = New eClient.Client
			lclsClient.Find(sClient)
			ldtmEffecdate = lclsClient.dInpdate
			'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsClient = Nothing
		End If
		
		If Find(sClient, dEffecdate) Then
			bDisabledChk_Del = False
			If sTyp_dirdeb = "1" Then
				sTableAccount = "tabbk_account"
				If lclsCred_card.GetQuantityCred_cards(sClient) = 0 Then
					bDisabledOpt_Cred = True
				End If
			Else
				sTableAccount = "tabcred_card"
				If lclsBk_account.GetQuantityAccounts(sClient) = 0 Then
					bDisabledOpt_Bk = True
				End If
			End If
		Else
			If lclsBk_account.GetQuantityAccounts(sClient) = 0 Then
				bDisabledOpt_Bk = True
				sTyp_dirdeb = "2"
				sTableAccount = "tabcred_card"
			End If
			If lclsCred_card.GetQuantityCred_cards(sClient) = 0 Then
				bDisabledOpt_Cred = True
				If bDisabledOpt_Bk Then
					bDisabledForm = True
					sTyp_dirdeb = "0"
				End If
			End If
		End If
		
InsPreBC015_Err: 
		If Err.Number Then
			InsPreBC015 = False
		End If
		'UPGRADE_NOTE: Object lclsBk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBk_account = Nothing
		'UPGRADE_NOTE: Object lclsCred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCred_card = Nothing
		On Error GoTo 0
	End Function
	
	'% insFindDirDebitCli: Esta funcion se encarga de validar la existencia de la cuenta/tarjeta en la
	'%                     domiciliación del cliente
	Public Function insFindDirdebitCli(ByVal sClient As String, ByVal nBank As Integer, ByVal sAccount As String, ByVal sTyp_dirdeb As String) As Boolean
		Dim lobjdir_debit_cli As Dir_debit_cli
		
		On Error GoTo insFindDirdebitCli_Err
		lobjdir_debit_cli = New Dir_debit_cli
		
		With lobjdir_debit_cli
			If .Find(sClient, Today) Then
				If .sTyp_dirdeb = sTyp_dirdeb And .nBankExt = nBank And .sAccount = sAccount Then
					insFindDirdebitCli = True
				End If
			End If
		End With
		
insFindDirdebitCli_Err: 
		If Err.Number Then
			insFindDirdebitCli = False
		End If
		'UPGRADE_NOTE: Object lobjdir_debit_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjdir_debit_cli = Nothing
	End Function
	
	'% DeleteAll: Elimina las direcciones asociadas a un cliente
	Public Function DeleteAll(ByVal sClient As String) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		With lrecreabk_account
			.StoredProcedure = "Deldir_Debit_Cli"
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
	
	'% Class_Initialize: Inicializa las variables públicas de la clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sClient = String.Empty
		dEffecdate = eRemoteDB.Constants.dtmNull
		sAccount = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
		sTyp_dirdeb = String.Empty
		sBankAuth = String.Empty
		nBankExt = eRemoteDB.Constants.intNull
		sCredi_card = String.Empty
		nCard_Type = eRemoteDB.Constants.intNull
		dCardexpir = eRemoteDB.Constants.dtmNull
		nBill_Day = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






