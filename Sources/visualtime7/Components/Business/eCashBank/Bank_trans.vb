Option Strict Off
Option Explicit On
Public Class Bank_trans
	'%-------------------------------------------------------%'
	'% $Workfile:: Bank_trans.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 29/09/03 3:17p                               $%'
	'% $Revision:: 23                                       $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according to the table in the system on March 12, 2001.
	'**+The key field in the table corresponds to: nAcc_bank, dEffecdate and nMovement.
	'+Propiedades según la tabla en el sistema al 12/03/2001.
	'+El campo llave de la tabla corresponde a: nAcc_bank, dEffecdate y nMovement.
	
	'   Column_name                    Type     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	Public nAcc_bank As Integer 'smallint    no       2           5     0     no          (n/a)               (n/a)
	Public nMovement As Integer 'int         no       4           10    0     no          (n/a)               (n/a)
	Public dEffecdate As Date 'datetime    no       8                       no          (n/a)               (n/a)
	Public nAcc_type As Integer 'smallint    no       2           5     0     no          (n/a)               (n/a)
	Public sAcco_num As String 'char        no      25                       no          no                  no
	Public nUsercode As Integer 'smallint    no       2           5     0     yes         (n/a)               (n/a)
	Public nBank_code As Integer 'int         no       4           10    0     no          (n/a)               (n/a)
	Public nBk_agency As Integer 'int         no       4           10    0     no          (n/a)               (n/a)
	Public sClient As String 'char        no      14                       no          no                  no
	Public nExchange As Double 'decimal     no       9           10    6     yes         (n/a)               (n/a)
	Public nBordereaux As Double 'int         no       4           10    0     yes         (n/a)               (n/a)
	
	'**-Auxiliary Variables
	'-Variables auxiliares
	Public nCurrency As Integer
	Public nAcc_bankDest As Integer
	Public nTypeTrans As Integer
	Public nAmount As Double
	Public nAmountDest As Double
	Public sN_Aba As String
	
	'**-Auxiliary Variables for the handling of the frames of OP012 (Transfers)
	'-Variables auxiliares para el manejo de los frame de la OP012(Transferencias)
	Public sOfficeDesc As String
	Public sAcc_typeDesc As String
	Public sOriCurrencyDesc As String
	Public sCurrencyDest As String
	Public mblnFrameVerify As Boolean
	Public mblnExcToLoc As Boolean
	Public mblnExcToOrig As Boolean
	Public mdblExcFromLoc As Double
	Public mdblExcToLoc As Double
	Public mdblExchange As Double
	Public mdblAmountNew As Double
	
	Private mstrCodispl As String
	
	'**%Update: Updates Bank Movements "Bank_mov", Bank Accounts "Bank_acc",
	'**%and Bank transfers "Bank_trans" tables, according to the action.
	'%Update: Actualiza las tablas de Movimientos Bancarios "Bank_mov", Cuentas Bancarias "Bank_acc"
	'%y transferencias Bancarias "Bank_trans" dependiendo de la acción
	Public Function Update() As Boolean
		
		'**-The variable lrecinsUpdBankTrans is declared
		'-Se define la variable lrecinsUpdBankTrans
		
		Dim lrecinsUpdBankTrans As eRemoteDB.Execute
		lrecinsUpdBankTrans = New eRemoteDB.Execute
		
		'**+Parameters definition for stored procedure 'insudb.insUpdBankTrans'
		'**+Information read on February 20, 2001 05:05:44 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insUpdBankTrans'
		'+Información leída el 20/02/2001 05:05:44 p.m.
		
		With lrecinsUpdBankTrans
			.StoredProcedure = "insUpdBankTrans"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bankDest", nAcc_bankDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcco_num", sAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeTrans", nTypeTrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountDest", nAmountDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_Aba", sN_Aba, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsUpdBankTrans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdBankTrans = Nothing
	End Function
	
	'*** Class_Initialize: controls the creation of each instance of  the class.
	'* Class_Initialize: Se controla la creación de cada instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%insValOP012_K: This method validates the header section of the page "OP012_K" as described in the
	'**%functional specifications
	'%InsValOP012_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OP012_K"
	Public Function insValOP012_k(ByVal sCodispl As String, ByVal tcdTransDate As Date, ByVal valOriAccount As Integer, ByVal tcnAmountTransf As Double, ByVal nUsercode As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclsBank_acc As Bank_acc
		Dim lclsUser_cashnum As User_cashnum
		
		lerrTime = New eFunctions.Errors
		lvalTime = New eFunctions.valField
		lclsBank_acc = New Bank_acc
		lclsUser_cashnum = New User_cashnum
		
		On Error GoTo insValOP012_k_Err
		
		If Not lclsUser_cashnum.Find_nUser(nUsercode, True) Then
			Call lerrTime.ErrorMessage(sCodispl, 60104)
		End If
		'**+Validation of tcdTransDate
		'+Validacion del tcdTransDate
		
		If tcdTransDate = dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7220)
		Else
			If Not lvalTime.ValDate(tcdTransDate) Then
				Call lerrTime.ErrorMessage(sCodispl, 7114)
			Else
				If tcdTransDate > Today Then
					Call lerrTime.ErrorMessage(sCodispl, 1002)
				End If
			End If
		End If
		
		'**+Validations of the "Internal Account" field
		'+Validaciones del campo "Cuenta interna"
		
		If valOriAccount = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7002)
		Else
			If lclsBank_acc.Find_O(valOriAccount) Then
				If lclsBank_acc.nAvailable <= 0 Then
					Call lerrTime.ErrorMessage(sCodispl, 7212)
				End If
			Else
				Call lerrTime.ErrorMessage(sCodispl, 7013)
			End If
		End If
		
		'**+Validations of the "Amount to transfer" field
		'+Validaciones del campo "Monto a transferir"
		If tcnAmountTransf < 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 7211)
		Else
			If valOriAccount <> eRemoteDB.Constants.intNull Then
				If lclsBank_acc.Find_O(valOriAccount, True) Then
					If lclsBank_acc.nAvailable < tcnAmountTransf Then
						Call lerrTime.ErrorMessage(sCodispl, 7087)
					End If
				End If
			End If
		End If
		
		insValOP012_k = lerrTime.Confirm
		
insValOP012_k_Err: 
		If Err.Number Then
			insValOP012_k = "insValOP012_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		'UPGRADE_NOTE: Object lclsUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUser_cashnum = Nothing
	End Function
	
	'**%insValFolderOP012E: This function is responsible for validating the data entered (for the External Transfer option)
	'%insValFolderOP012E: Esta función se encarga de validar los datos introducidos (para la opción de transferencia Externa)
	Public Function insValFolderOP012E(ByVal sCodispl As String, ByVal cbeBank As Integer, ByVal valAgency As Integer, ByVal cbeAccount As Integer, ByVal tctExtAccount As String, ByVal dtcClient As String, ByVal tctAbaNum As String) As String
		Dim lerrTime As eFunctions.Errors
		
		
		On Error GoTo insValFolderOP012E_Err
		lerrTime = New eFunctions.Errors
		
		'**+Validates the field "Bank"
		'+Se realizan las validaciones del campo "Banco"
		If cbeBank = eRemoteDB.Constants.intNull Or cbeBank = 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 7221)
		End If
		
		'**+Validates the field "Agency"
		'+Se realizan las validaciones sobre el campo "Agencia"
		If valAgency = eRemoteDB.Constants.intNull Or valAgency = 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 1080)
		End If
		
		'**+Validates the field "Type of Account"
		'+Se realizan las validaciones sobre el campo Tipo de cuenta"
		If cbeAccount = eRemoteDB.Constants.intNull Or cbeAccount = 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 7224)
		End If
		
		'**+Validates the field "Account Number"
		'+Se realizan las validaciones sobre el campo " Nro. de Cuenta "
		If Trim(tctExtAccount) = strNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7223)
		End If
		
		'**+Validates the field "Client's code"
		'+Se realizan las validaciones sobre el campo "Código de cliente"
		If Trim(dtcClient) = strNull Then
			Call lerrTime.ErrorMessage(sCodispl, 13667)
		End If
		
		'**+Validates the field " ABA Number"
		'+Se realizan las validaciones sobre el campo "Numero de Aba"
		If Trim(tctAbaNum) = strNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7246)
		End If
		
		insValFolderOP012E = lerrTime.Confirm
		
insValFolderOP012E_Err: 
		If Err.Number Then
			insValFolderOP012E = "insValFolderOP012E: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValFolderOP012E: This function is in charge of validating the data introduced (for the Internal Transfer option)
	'%insValFolderOP012E: Esta función se encarga de validar los datos introducidos (para la opción de transferencia interna)
	Public Function insValFolderOP012I(ByVal sCodispl As String, ByVal valIntAccount As Integer, ByVal valOriAccount As Integer, ByVal tcnExchangeToLocal As Double, ByVal gintCurrencyOrig As Integer, ByVal tcdTransDate As Date, ByVal tcnExchangeFromLocal As Double) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsBank_acc As eCashBank.Bank_acc
		
		Dim lintCurrencyDest As Integer
		Dim ldblExchange As Double
		
		lerrTime = New eFunctions.Errors
		
		'insValFolderOP012I = True
		
		On Error GoTo insValFolderOP012I_Err
		
		
		'**+Validations of the field "Internal Account"
		'+Validaciones del campo "Cuenta interna"
		lintCurrencyDest = 0
		If valIntAccount = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7002)
		Else
			If valOriAccount = valIntAccount Then
				Call lerrTime.ErrorMessage(sCodispl, 7090)
			Else
				lclsBank_acc = New eCashBank.Bank_acc
				If lclsBank_acc.Find_O(valIntAccount, True) Then
					lintCurrencyDest = lclsBank_acc.nCurrency
				Else
					Call lerrTime.ErrorMessage(sCodispl, 7013)
				End If
			End If
		End If
		
		'**+Validations of the field "Factor to convert the original currency into local"
		'+Validaciones del campo "Factor para convertir de Moneda Original a local"
		If tcnExchangeToLocal = eRemoteDB.Constants.intNull And gintCurrencyOrig <> lintCurrencyDest Then
			Call lerrTime.ErrorMessage(sCodispl, 1944)
		Else
			If gintCurrencyOrig <> lintCurrencyDest Then
				If tcnExchangeToLocal <> insReaExchange(gintCurrencyOrig, tcdTransDate) Then
					Call lerrTime.ErrorMessage(sCodispl, 1945)
				End If
			End If
		End If
		
		'**+Validations of the field " Factor to convert the local currency into original"
		'+Validaciones del campo "Factor para convertir de Moneda local a  Original"
		If tcnExchangeFromLocal = eRemoteDB.Constants.intNull And gintCurrencyOrig <> lintCurrencyDest Then
			Call lerrTime.ErrorMessage(sCodispl, 1946)
		Else
			If gintCurrencyOrig <> lintCurrencyDest Then
				ldblExchange = insReaExchange(lintCurrencyDest, tcdTransDate)
				If ldblExchange <> 0 Then
					ldblExchange = CDbl(Format(1 / ldblExchange, "##.######"))
				End If
				If tcnExchangeFromLocal <> ldblExchange And lintCurrencyDest <> 1 Then
					Call lerrTime.ErrorMessage(sCodispl, 1947)
				End If
			End If
		End If
		
		insValFolderOP012I = lerrTime.Confirm
		
insValFolderOP012I_Err: 
		If Err.Number Then
			insValFolderOP012I = "insValFolderOP012I: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%insReaExchange: This method reads the exchange rate
	'%insReaExchange: Se define esta funcion para leer el factor de cambio
	Private Function insReaExchange(ByVal pintCurrency As Integer, ByVal pdatEffecdate As Date) As Double
		
		Dim lclsExchange As eGeneral.Exchange
		lclsExchange = New eGeneral.Exchange
		
		On Error GoTo insReaExchange_err
		
		insReaExchange = 0
		If lclsExchange.Find(pintCurrency, pdatEffecdate) Then
			insReaExchange = lclsExchange.nExchange
		End If
		
		'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchange = Nothing
		
insReaExchange_err: 
		If Err.Number Then
			'insDriveError ("insReaExchange")
			insReaExchange = 0
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostFolderOP012: This function is in charge of validating all of the data from the form
	'%insPostFolderOP012: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostFolderOP012(ByVal valOriAccount As Integer, ByVal valIntAccount As Integer, ByVal tcdTransDate As Date, ByVal cbeAccount As Integer, ByVal tctExtAccount As String, ByVal cbeBank As Integer, ByVal gintAgency As Integer, ByVal dtcClient As String, ByVal tcnExchange As Double, ByVal tcnAmountTransf As Double, ByVal tcnAmountNew As Double, ByVal tctAbaNum As String, ByVal gintUser As Integer, ByVal optTrans As String) As Boolean
		
		On Error GoTo insPostFolderOP012_Err
		'**+If the selected option is Register
		'+Si la opción seleccionada es Registrar
		insPostFolderOP012 = insUpdBankTrans(valOriAccount, valIntAccount, tcdTransDate, cbeAccount, tctExtAccount, cbeBank, gintAgency, dtcClient, tcnExchange, tcnAmountTransf, tcnAmountNew, tctAbaNum, gintUser, optTrans)
		
insPostFolderOP012_Err: 
		If Err.Number Then
			insPostFolderOP012 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insUpdBankTrans: This function makes the call to the stored procedure
	'**%that is in charge of updating the tables with the data from the transfers.
	'%insUpdBankTrans: Se define esta funcion para realizar el llamado al
	'%Stored Procedure que se encarga de actualizar las tablas con los datos de las tranferencias
	Private Function insUpdBankTrans(ByVal valOriAccount As Integer, ByVal valIntAccount As Integer, ByVal tcdTransDate As Date, ByVal cbeAccount As Integer, ByVal tctExtAccount As String, ByVal cbeBank As Integer, ByVal gintAgency As Integer, ByVal dtcClient As String, ByVal tcnExchange As Double, ByVal tcnAmountTransf As Double, ByVal tcnAmountNew As Double, ByVal tctAbaNum As String, ByVal gintUser As Integer, ByVal optTrans As String) As Boolean
		Dim lclsBank_trans As eCashBank.Bank_trans
		
		
		On Error GoTo insUpdBankTrans_err
		lclsBank_trans = New eCashBank.Bank_trans
		
		'**+If the type of transfer is internal, you make the call to the SP (Stored Procedure),
		'**+if not, you make the writing inside the registry, so that it makes the call in the "mother" window
		'**+(the first in the sub sequence)
		'+Si el tipo de transferencia es interna, se realiza el llamado al SP (Stored Procedure),
		'+en caso contrario, se realiza la escritura dentro del registry, para que se realize el
		'+llamado en la ventana "madre" (la primera de la sub-sequencia)
		With lclsBank_trans
			.nAcc_bank = valOriAccount
			.nAcc_bankDest = valIntAccount
			.dEffecdate = tcdTransDate
			.nAcc_type = cbeAccount
			.sAcco_num = tctExtAccount
			.nBank_code = cbeBank
			.nBk_agency = gintAgency
			.sClient = dtcClient
			.nExchange = tcnExchange
			.nTypeTrans = CInt(optTrans)
			.nAmount = tcnAmountTransf
			.nAmountDest = tcnAmountNew
			.sN_Aba = tctAbaNum
			.nUsercode = gintUser
			
			If .Update Then
				insUpdBankTrans = True
			End If
		End With
		
		
insUpdBankTrans_err: 
		If Err.Number Then
			insUpdBankTrans = False
		End If
		'UPGRADE_NOTE: Object lclsBank_trans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_trans = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsPreOP012I: This method initializes all of the properties utilized by the page "OP012I"
	'%InsPreOP012I: Este metodo inicializa todas las propiedades utilizada por la pagina "OP012I"
	Public Function insPreOP012I(ByVal lintOriAccount As Integer, ByVal lintOriCurrency As Integer, ByVal ldtmTransdate As Date, ByVal ldblAmountTransf As Double, ByVal lintDestAccount As Integer) As Boolean
		Dim lclsBank_acc As eCashBank.Bank_acc
		Dim lclsExchange As eGeneral.Exchange
		Dim lclsValues As eFunctions.Values
		Dim lclsQuery As eRemoteDB.Query
		
		On Error GoTo InsPreOP012I_Err
		
		lclsBank_acc = New eCashBank.Bank_acc
		lclsValues = New eFunctions.Values
		lclsExchange = New eGeneral.Exchange
		
		insPreOP012I = True
		
		With lclsBank_acc
			If .Find_O(lintDestAccount) Then
				lclsQuery = New eRemoteDB.Query
				If lclsQuery.OpenQuery("table9", "sDescript", "nOffice=" & .nOffice) Then
					sOfficeDesc = lclsQuery.FieldToClass("sDescript")
				End If
				'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsQuery = Nothing
				sAcc_typeDesc = lclsValues.getMessage(.nAcc_type, "table190")
				sCurrencyDest = lclsValues.getMessage(.nCurrency, "table11")
				sOriCurrencyDesc = lclsValues.getMessage(lintOriCurrency, "table11")
				nCurrency = .nCurrency
				If nCurrency = lintOriCurrency Then
					mdblExcFromLoc = 1
					mdblExcToLoc = 1
				Else
					mblnFrameVerify = True
					If lintOriCurrency = 1 Or lintOriCurrency = 0 Then
						mdblExcToLoc = 1
					Else
						If Not mblnExcToLoc Then
							mblnExcToLoc = True
							If lclsExchange.Find(lintOriCurrency, ldtmTransdate) Then
								mdblExcToLoc = lclsExchange.nExchange
							Else
								mdblExcToLoc = 0
							End If
						End If
					End If
					If nCurrency = 1 Or nCurrency = 0 Then
						mdblExcFromLoc = 1
					Else
						If Not mblnExcToOrig Then
							mblnExcToOrig = True
							If lclsExchange.Find(nCurrency, ldtmTransdate) Then
								mdblExchange = lclsExchange.nExchange
							Else
								mdblExchange = 0
							End If
							If mdblExchange = 0 Then
								mdblExcFromLoc = 0
							Else
								mdblExcFromLoc = 1 / mdblExchange
							End If
						End If
					End If
				End If
				
				mdblExchange = mdblExcFromLoc * mdblExcToLoc
				mdblAmountNew = ldblAmountTransf * mdblExcFromLoc * mdblExcToLoc
			End If
		End With
		
		If mdblAmountNew > CDbl(999999999999#) Then
			insPreOP012I = False
		End If
		
		
InsPreOP012I_Err: 
		If Err.Number Then
			insPreOP012I = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchange = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'% Find_v: Realiza la lectura  correspondiente  a la tabla de transacciones bancarias, para
	' validar si la agencia enviada  como parámetro tiene cuentas asociadas.
	Public Function Find_v(ByVal nBank_code As Integer, ByVal nBk_agency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Static lblnRead As Boolean
		
		Dim lrecreaBank_trans_v As eRemoteDB.Execute
		
		If Me.nBank_code <> nBank_code Or Me.nBk_agency <> nBk_agency Or lblnFind Then
			
			lrecreaBank_trans_v = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaBank_trans_v'
			'Información leída el 19/09/2001 9:41:33
			
			With lrecreaBank_trans_v
				.StoredProcedure = "reaBank_trans_v"
				
				.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nAcc_bank = .FieldToClass("nAcc_bank")
					Me.nMovement = .FieldToClass("nMovement")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nAcc_type = .FieldToClass("nAcc_type")
					Me.sAcco_num = .FieldToClass("sAcco_num")
					Me.nBank_code = .FieldToClass("nBank_code")
					Me.nBk_agency = .FieldToClass("nBk_agency")
					Me.sClient = .FieldToClass("sClient")
					Me.nExchange = .FieldToClass("nExchange")
					Me.nBordereaux = .FieldToClass("nBordereaux")
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaBank_trans_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaBank_trans_v = Nothing
		End If
		Find_v = lblnRead
	End Function
	
	'%insPreOP012I_K: Función que asigna los valores a los campos de la ventana OP012I
	Public Function insPreOP012I_K(ByVal sCodispl As String) As Boolean
		mstrCodispl = sCodispl
	End Function
	
	'**% DefaultValueOP012I. This function is in charge of making the fields in the window OP012I able or desable.
	'%DefaultValueOP012I. Esta función se encarga de realizar la habilitación o des-habilitación de los
	'%campos de la ventana OP012I.
	Public Function DefaultValueOP012I(ByRef sField As Object) As Object
        Dim lvarReturnValue As Object = New Object

        Select Case sField
			'**+ Unable the fields
			'+Deshabilita los campos "Fecha" y "Monto a transferir"
			Case "tcdTransDate_disabled", "tcnAmountTransf_disabled"
				Select Case mstrCodispl
					Case "OP06-1", "OP06-6"
						lvarReturnValue = "true"
					Case Else
						lvarReturnValue = "false"
				End Select
		End Select
		DefaultValueOP012I = lvarReturnValue
	End Function
	
	'**%ClearFields: This function initializes all of the properties in the class with null values
	'%ClearFields: Esta funcion se encarga de inicializar todas las propiedades de la clase con valores nulos
	Private Sub ClearFields()
		nAcc_bank = eRemoteDB.Constants.intNull
		nMovement = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nAcc_type = eRemoteDB.Constants.intNull
		sAcco_num = strNull
		nUsercode = eRemoteDB.Constants.intNull
		nBank_code = eRemoteDB.Constants.intNull
		nBk_agency = eRemoteDB.Constants.intNull
		sClient = strNull
		nExchange = eRemoteDB.Constants.intNull
		nBordereaux = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nAcc_bankDest = eRemoteDB.Constants.intNull
		nTypeTrans = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nAmountDest = eRemoteDB.Constants.intNull
		sN_Aba = strNull
		sOfficeDesc = strNull
		sAcc_typeDesc = strNull
		sOriCurrencyDesc = strNull
		sCurrencyDest = strNull
		mblnFrameVerify = False
		mblnExcToLoc = False
		mblnExcToOrig = False
		mdblExcFromLoc = eRemoteDB.Constants.intNull
		mdblExcToLoc = eRemoteDB.Constants.intNull
		mdblExchange = eRemoteDB.Constants.intNull
		mdblAmountNew = eRemoteDB.Constants.intNull
	End Sub
End Class






