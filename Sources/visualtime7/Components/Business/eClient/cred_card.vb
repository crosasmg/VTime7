Option Strict Off
Option Explicit On
Public Class cred_card
	'%-------------------------------------------------------%'
	'% $Workfile:: cred_card.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema 11/01/2000
	'+ Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'+ Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	
	Public sClient As String
	Public nBankExt As Integer
	Public sCredi_card As String
	Public nCard_Type As Integer
	Public sStatregt As String
	Public dCardexpir As Date
	Public nUsercode As Integer
	
	'- Variables auxiliares
	Public sIndDirDebit As String
	
	'- Se define la variable que contiene el estado de la cada instancia de la clase
	
	Public nStatusInstance As Integer
	
	Private Enum eAction
		clngInsert = 1
		clngUpdate = 2
		clngDelete = 3
	End Enum
	
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal sClient As String, ByVal nBankExt As Integer, ByVal sCredi_card As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreacred_card As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreacred_card = New eRemoteDB.Execute
		
		If sClient = Me.sClient And Not bFind Then
			Find = True
		Else
			With lrecreacred_card
				.StoredProcedure = "reacred_card"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.nBankExt = .FieldToClass("nBankext")
					Me.sCredi_card = .FieldToClass("sCredi_card")
					Me.nCard_Type = .FieldToClass("nCard_type")
					Me.sStatregt = .FieldToClass("sStatregt")
					Me.dCardexpir = .FieldToClass("dCardExpir")
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
		'UPGRADE_NOTE: Object lrecreacred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreacred_card = Nothing
	End Function
	
	'% Add: Agrega los datos correspondientes para un cliente, año y concepto específico
	Public Function Add() As Boolean
		Add = insUpdcred_card(eAction.clngInsert)
	End Function
	
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Update = insUpdcred_card(eAction.clngUpdate)
	End Function
	
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Delete = insUpdcred_card(eAction.clngDelete)
	End Function
	
	'% insUpdcred_card: Esta funcion se encarga de realizar las actualizaciones de la tabla
	'%                  cred_card, correspodiente a las cuentas del cliente.
	Function insUpdcred_card(ByRef Action As Object) As Boolean
		Dim lobjTime As eRemoteDB.Execute
		
		On Error GoTo insUpdcred_card_Err
		lobjTime = New eRemoteDB.Execute
		
		With lobjTime
			.StoredProcedure = "insUpdcred_card"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_type", nCard_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCardexpir", dCardexpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", Action, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdcred_card = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lobjTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTime = Nothing
insUpdcred_card_Err: 
		If Err.Number Then
			insUpdcred_card = False
		End If
		On Error GoTo 0
	End Function
	
	'% InsPostBC016: Realiza las actualizaciones de la transaccion
	Public Function InsPostBC016(ByVal nAction As Integer, ByVal sClient As String, ByVal nBankExt As Integer, ByVal nCard_Type As Integer, ByVal sCredi_card As String, ByVal dCardexpir As Date, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lcolCred_Card As cred_cards
		Dim lclsClientWin As ClientWin
		Dim lstrContent As String
		Dim lclsDir_debit_cli As Dir_debit_cli
		
		On Error GoTo InsPostBC016_Err
		
		lcolCred_Card = New cred_cards
		lclsClientWin = New ClientWin
		
		Me.sClient = sClient
		Me.nBankExt = nBankExt
		Me.nCard_Type = nCard_Type
		Me.sCredi_card = sCredi_card
		Me.dCardexpir = dCardexpir
		Me.sStatregt = sStatregt
		Me.nUsercode = nUsercode
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				InsPostBC016 = Me.Add
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				InsPostBC016 = Me.Update
				If InsPostBC016 Then
					If sStatregt = "3" Then
						lclsDir_debit_cli = New Dir_debit_cli
						If lclsDir_debit_cli.Find(sClient, Today) Then
							If lclsDir_debit_cli.sTyp_dirdeb = "2" And lclsDir_debit_cli.nBankExt = nBankExt And lclsDir_debit_cli.sAccount = sCredi_card Then
								lclsClientWin.insUpdClient_win(sClient, "BC015", "3")
							End If
						End If
					End If
				End If
			Case eFunctions.Menues.TypeActions.clngActioncut
				InsPostBC016 = Me.Delete
		End Select
		
		If InsPostBC016 Then
			lstrContent = IIf(lcolCred_Card.Find(sClient), "2", "1")
			lclsClientWin.insUpdClient_win(sClient, "BC016", lstrContent)
		End If
		
InsPostBC016_Err: 
		If Err.Number Then
			InsPostBC016 = False
		End If
		'UPGRADE_NOTE: Object lcolCred_Card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCred_Card = Nothing
		'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClientWin = Nothing
		'UPGRADE_NOTE: Object lclsDir_debit_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDir_debit_cli = Nothing
		On Error GoTo 0
	End Function
	
	'% insValBC016: Realiza las validaciones de la transaccion
	Public Function insValBC016(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal sClient As String = "", Optional ByVal nBank_Code As Integer = 0, Optional ByVal nCard_Type As Integer = 0, Optional ByVal sCredi_card As String = "", Optional ByVal dCardexpir As Date = #12:00:00 AM#, Optional ByVal sStatregt As String = "") As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsCred_card As cred_card
		
		On Error GoTo insValBC016_Err
		lerrTime = New eFunctions.Errors
		lclsCred_card = New cred_card
		
		With lerrTime
			
			'+Validaciones del campo Banco
			If nBank_Code = eRemoteDB.Constants.intNull Or Trim(CStr(nBank_Code)) = "0" Then
				.ErrorMessage(sCodispl, 7004)
			End If
			
			'+Validaciones del campo oficina
			If nCard_Type = eRemoteDB.Constants.intNull Or Trim(CStr(nCard_Type)) = "0" Then
				.ErrorMessage(sCodispl, 5047)
			End If
			
			'+Validaciones del campo Número de la cuenta
			If Trim(sCredi_card) = String.Empty Then
				.ErrorMessage(sCodispl, 3865)
			Else
				If nBank_Code <> eRemoteDB.Constants.intNull And Trim(CStr(nBank_Code)) <> "0" Then
					If Trim(sAction) <> "Update" Then
						If Me.Find(sClient, nBank_Code, sCredi_card) Then
							.ErrorMessage(sCodispl, 12101)
						End If
					End If
				End If
			End If
			
			'+Validaciones del campo fecha de vencimiento
			If dCardexpir = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 5050)
			Else
				If dCardexpir <= Today Then
					.ErrorMessage(sCodispl, 1964)
				End If
			End If
			
			'+Validaciones del campo estado
			If Trim(sStatregt) = String.Empty Or Trim(sStatregt) = "0" Then
				.ErrorMessage(sCodispl, 1922)
			End If
			
			insValBC016 = .Confirm
		End With
		
insValBC016_Err: 
		If Err.Number Then
			insValBC016 = insValBC016 & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsCred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCred_card = Nothing
		On Error GoTo 0
	End Function
	
	'% GetQuantityCred_cards: Lee la tarjetas de credito del cliente
	Public ReadOnly Property GetQuantityCred_cards(ByVal sClient As String) As Integer
		Get
			Dim lrecreabk_account As eRemoteDB.Execute
			
			On Error GoTo GetQuantityCred_cards_Err
			lrecreabk_account = New eRemoteDB.Execute
			
			GetQuantityCred_cards = 0
			
			With lrecreabk_account
				.StoredProcedure = "QuantityCred_cards"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					GetQuantityCred_cards = .FieldToClass("Count")
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreabk_account = Nothing
GetQuantityCred_cards_Err: 
			If Err.Number Then
				GetQuantityCred_cards = 0
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'% DeleteAll: Realiza el borrado en transaccion
	Public Function DeleteAll(ByVal sClient As String) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		With lrecreabk_account
			.StoredProcedure = "Delcred_card"
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
End Class






