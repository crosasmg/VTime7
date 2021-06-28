Option Strict Off
Option Explicit On
Public Class Curr_acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Curr_acc.cls                             $%'
	'% $Author:: Mmmiola                                    $%'
	'% $Date:: 14/10/04 10:53a                              $%'
	'% $Revision:: 44                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.curr_acc al 04-15-2002 12:32:40
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nTyp_acco As Integer ' NUMBER     22   0     5    N
	Public sType_acc As String ' CHAR       1    0     0    N
	Public sClient As String ' CHAR       14   0     0    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nBalance As Double ' NUMBER     22   2     10   S
	Public nCredit As Double ' NUMBER     22   2     12   S
	Public nDebit As Double ' NUMBER     22   2     12   S
	Public dEffecdate As Date ' DATE       7    0     0    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nLed_compan As Integer ' NUMBER     22   0     5    S
	Public sAccount As String ' CHAR       20   0     0    S
	Public sCertype As String ' CHAR       1    0     0    S
	Public nCompany As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public sAux_accoun As String ' CHAR       20   0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nCertif As Double ' NUMBER     22   0     10   S
	Public nPolicy As Double ' NUMBER     22   0     10   S
	Public nInsur_area As Integer ' NUMBER     22   0     5    S
	
	'**- Auxiliary Variables
	'-Variables auxiliares
	Public blnError As Boolean
	Public nCount As Integer
	'UPGRADE_NOTE: Move_Acc was upgraded to Move_Acc_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Move_Acc_Renamed As eCashBank.Move_Acc
	'UPGRADE_NOTE: Comm_det was upgraded to Comm_det_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Comm_det_Renamed As eCashBank.Comm_det
	Public sClienames As String
	Public nIdconsec As Integer
	
	Enum OP092ActVal
		clngOP092Add = 1
		clngOP092Upd = 2
		clngOP092Cut = 3
	End Enum
	'**- DEfine the enumerated list Table403Val, for differencing the window options
	'-Se define la lista enumerada Table403Val, para diferenciar las opciones de la ventana
	
	Enum Table403Val
		clngEntryRemitt = 1
		clngUpdateRemitt = 2
		clngQueryRemitt = 3
		clngCutRemitt = 4
		clngRevertRemitt = 5
	End Enum
	
	Public ReadOnly Property sCliename() As Object
		Get
			sCliename = sClienames
		End Get
	End Property
	
	'**% CurrenAsoc_count: allows to count the currency number associated with the account
	'%CurrenAsoc_count: Permite cuantificar el número de monedas ascociadas al
	'%a la cuenta
	Public ReadOnly Property CurrenAsoc_count() As Integer
		Get
			Dim lrecreaCurr_acc_Curren_Count As eRemoteDB.Execute
			
			lrecreaCurr_acc_Curren_Count = New eRemoteDB.Execute
			
			On Error GoTo CurrenAsoc_count_Err
			
			'**Parameter definition for stored procedure 'insudb.reaCurr_acc_Current_Count'
			'Definición de parámetros para stored procedure 'insudb.reaCurr_acc_Curren_Count'
			'**data of March 19,2001 13:52:01
			'Información leída el 19/03/2001 13:52:01
			
			With lrecreaCurr_acc_Curren_Count
				.StoredProcedure = "reaCurr_acc_Curren_Count"
				.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					CurrenAsoc_count = .FieldToClass("Count")
					.RCloseRec()
				Else
					CurrenAsoc_count = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreaCurr_acc_Curren_Count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaCurr_acc_Curren_Count = Nothing
			
CurrenAsoc_count_Err: 
			If Err.Number Then
				CurrenAsoc_count = 0
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'**% Curr_CountPol: Allows to obtain a valid registration for the passed key as a parameter
	'%Curr_CountPol: Permite obtener el registro  valido para la llave pasada como parametro
	Public ReadOnly Property Curr_CountPol() As Integer
		Get
			Dim lrecreaCurr_acc_countPol As eRemoteDB.Execute
			
			lrecreaCurr_acc_countPol = New eRemoteDB.Execute
			
			On Error GoTo Curr_CountPol_Err
			
			'** Parameter definition for stored procedure 'insudb.reaCurr_acc_countPol'
			'Definición de parámetros para stored procedure 'insudb.reaCurr_acc_countPol'
			'Data of March 19,2001  14:30:09
			'Información leída el 19/03/2001 14:30:09
			
			With lrecreaCurr_acc_countPol
				.StoredProcedure = "reaCurr_acc_countPol"
				.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Curr_CountPol = .FieldToClass("nCount")
					.RCloseRec()
				Else
					Curr_CountPol = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreaCurr_acc_countPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaCurr_acc_countPol = Nothing
			
Curr_CountPol_Err: 
			If Err.Number Then
				Curr_CountPol = 0
			End If
			On Error GoTo 0
		End Get
	End Property
	'**% Add: creates a movement in the file of current accounts (Curr_acc)
	'% Add: Crea un movimiento en el archivo de cuentas corrientes (Curr_acc)
	Public Function Add() As Boolean
		'**- Variable definition lrecreCurr_Acc
		'- Se define la variable lreccreCurr_Acc
		Dim lreccreCurr_Acc As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreCurr_Acc = New eRemoteDB.Execute
		
		'**+ Parameter Definition for stored procedure 'insudb.creCurr_Acc'
		'+ Definición de parámetros para stored procedure 'insudb.creCurr_Acc'
		'**+ data of February 15, 2001 05:42:52 p.m.
		'+ Información leída el 15/02/2001 05:42:52 p.m.
		With lreccreCurr_Acc
			.StoredProcedure = "creCurr_Acc"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreCurr_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCurr_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'**% AccCount: Obtains the current accounts that exist for the client in treat of the Current Account table (Curr_Acc)
	'% AccCount: Obtiene las cuentas corrientes que existen para el cliente en tratamiento
	' de la tabla de Cuentas Corrientes (Curr_acc)
	Public Function AccCount(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String) As Boolean
		
		'**- Variable definition lrecreaCurr_acc_count
		'- Se define la variable lrecreaCurr_acc_count
		Dim lrecreaCurr_acc_count As eRemoteDB.Execute
		
		On Error GoTo AccCount_Err
		lrecreaCurr_acc_count = New eRemoteDB.Execute
		AccCount = False
		
		'**+ Parameter definition for stored procedure 'insudb.reaCurr_acc_count'
		'+ Definición de parámetros para stored procedure 'insudb.reaCurr_acc_count'
		'**+ Data of February 19,2001  09:09:26 a.m.
		'+ Información leída el 19/02/2001 09:09:26 a.m.
		With lrecreaCurr_acc_count
			.StoredProcedure = "reaCurr_acc_count"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCount = .FieldToClass("nCount")
				nCurrency = .FieldToClass("nCurrency")
				AccCount = True
				.RCloseRec()
			End If
		End With
		
AccCount_Err: 
		If Err.Number Then
			AccCount = False
		End If
		'UPGRADE_NOTE: Object lrecreaCurr_acc_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCurr_acc_count = Nothing
		On Error GoTo 0
	End Function
	
	'**% findClientCurr_acc: this routine allows to read the data in the Curr_acc table.
	'%findClientCurr_acc: Esta rutina permite leer los datos de la tabla Curr_acc.
	Public Function FindClientCurr_acc(ByVal nType_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'**-Variable definition lrec_Curr_acc that will be used as a cursor.
		'-Se define la variable lrec_Curr_acc que se utilizará como cursor.
		Dim lrec_Curr_acc As eRemoteDB.Execute
		
		On Error GoTo FindClientCurr_acc_Err
		lrec_Curr_acc = New eRemoteDB.Execute
		FindClientCurr_acc = False
		
		'**+ Execute the store procedure to verify if the current accoun exists or not.
		'+Se ejecuta el store procedure para verificar si existe o no la cuenta corriente.
		With lrec_Curr_acc
			.StoredProcedure = "reaCurr_acc_o"
			.Parameters.Add("nTyp_acco", nType_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) 'Moneda local
			If .Run Then
				FindClientCurr_acc = True
				Me.blnError = True
				Me.nTyp_acco = .FieldToClass("nTyp_acco")
				Me.sType_acc = .FieldToClass("sType_acc")
				Me.sClient = .FieldToClass("sClient")
				Me.nCurrency = .FieldToClass("nCurrency")
				nBalance = .FieldToClass("nBalance")
				nCredit = .FieldToClass("nCredit")
				nDebit = .FieldToClass("nDebit")
				dEffecdate = .FieldToClass("dEffecdate")
				sStatregt = .FieldToClass("sStatregt")
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				sCertype = .FieldToClass("sCertype")
				nCompany = .FieldToClass("nCompany")
				nProduct = .FieldToClass("nProduct")
				sAux_accoun = .FieldToClass("sAux_accoun")
				nBranch = .FieldToClass("nBranch")
				nCertif = .FieldToClass("nCertif")
				nPolicy = .FieldToClass("nPolicy")
				.RCloseRec()
			End If
		End With
		
FindClientCurr_acc_Err: 
		If Err.Number Then
			FindClientCurr_acc = False
		End If
		'UPGRADE_NOTE: Object lrec_Curr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_Curr_acc = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindCountCurrency: Is in charge of counting how may currencies have the client associated.
	'%FindCountCurrency: Se encarga de contar cuantas monedas tiene asociadas el cliente
	Public Function FindCountCurrency(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String) As Boolean
		'**- Variable definition lrec_Curr_acc that will be used as a cursor.
		'-Se define la variable lrec_Curr_acc que se utilizará como cursor.
		Dim lrec_Curr_acc As eRemoteDB.Execute
		
		On Error GoTo FindCountCurrency_Err
		lrec_Curr_acc = New eRemoteDB.Execute
		FindCountCurrency = False
		
		'**+ Execute the store procedure that searches haw many currencies the clien has.
		'+Se ejecuta el store procedure que busca cuantas monedas tiene el cliente
		With lrec_Curr_acc
			.StoredProcedure = "reaCurr_acc_Curren_Count"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCountCurrency = True
				nCount = .FieldToClass("count")
				.RCloseRec()
			End If
		End With
		
FindCountCurrency_Err: 
		If Err.Number Then
			FindCountCurrency = False
		End If
		'UPGRADE_NOTE: Object lrec_Curr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_Curr_acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostOP090: Creates a registration in the current account, and the movement of this
	'%insPostOP090: Genera un registro en la cuenta corriente y el movimiento inicial de ésta
	Public Function insPostOP090(ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nLedCompan As Integer, ByVal sLedgerAcc As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal sAuxAccount As String = "") As Boolean
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsMove_Acc As eCashBank.Move_Acc
		
		On Error GoTo insPostOP090_Err
		
		lclsQuery = New eRemoteDB.Query
		lclsMove_Acc = New eCashBank.Move_Acc
		
		With Me
			.nTyp_acco = nTypeAccount
			.sType_acc = sBussiType
			.sClient = sClient
			.nCurrency = nCurrency
			.nBalance = 0
			.nCredit = 0
			.nDebit = 0
			.dEffecdate = dEffecdate
			.sStatregt = "1"
			.nUsercode = nUsercode
			.nLed_compan = nLedCompan
			.sAccount = sLedgerAcc
			.sCertype = String.Empty
			.nCompany = eRemoteDB.Constants.intNull
			.nProduct = eRemoteDB.Constants.intNull
			.sAux_accoun = sAuxAccount
			.nBranch = eRemoteDB.Constants.intNull
			.nCertif = eRemoteDB.Constants.intNull
			.nPolicy = eRemoteDB.Constants.intNull
			.Add()
		End With
		
		With lclsMove_Acc
			.nTyp_acco = nTypeAccount
			.sType_acc = sBussiType
			.sClient = sClient
			.nCurrency = nCurrency
			.dOperdate = dEffecdate
			.nIntermed = eRemoteDB.Constants.intNull
			.nAmount = eRemoteDB.Constants.intNull
			.nBankext = eRemoteDB.Constants.intNull
			.nBranch = eRemoteDB.Constants.intNull
			.nCertif = eRemoteDB.Constants.intNull
			.sCheque = String.Empty
			.nClaim = eRemoteDB.Constants.intNull
			.nCredit = 0
			.nDebit = 0
			If lclsQuery.OpenQuery("Table401", "sDescript", "nType_move=10") Then
				.sDescript = lclsQuery.FieldToClass("sDescript")
			Else
				.sDescript = "Mov. inicial en cuenta corrien"
			End If
			.sManualMov = String.Empty
			.nPaynumbe = eRemoteDB.Constants.intNull
			.sStatregt = "1"
			.nTransac = eRemoteDB.Constants.intNull
			.nTransactio = eRemoteDB.Constants.intNull
			.nType_move = 10
			.nType_pay = eRemoteDB.Constants.intNull
			.nType_tran = eRemoteDB.Constants.intNull
			.nIdDocument = eRemoteDB.Constants.intNull
			.nRequest_nu = eRemoteDB.Constants.intNull
			.nBordereaux = eRemoteDB.Constants.intNull
			.sProcess = String.Empty
			.sNumForm = String.Empty
			.nOrigCurr = eRemoteDB.Constants.intNull
			.nExchange = eRemoteDB.Constants.intNull
			.sAutoriza = String.Empty
			.dValueDate = dtmNull
			.nUsercode = nUsercode
			.nProvince = eRemoteDB.Constants.intNull
			.nPolicy = eRemoteDB.Constants.intNull
			.nReceipt = eRemoteDB.Constants.intNull
			.nProduct = eRemoteDB.Constants.intNull
			.sNull_recor = String.Empty
			.nIdreturn = 0
			.nIdconsec = eRemoteDB.Constants.intNull
			insPostOP090 = .Add
		End With
		
insPostOP090_Err: 
		If Err.Number Then
			insPostOP090 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'**% insPostOP091: Makes the updating in the tables corresponding to the remittance.
	'%insPostOP091: Realiza las actualizaciones en las tablas correspondiente a la remesa
    Public Function insPostOP091(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal dEffecdate As Date, ByVal nIdDocument As Integer, ByVal nType_pay As Integer, ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nUsercode As Integer, ByVal nProcess As Integer, ByVal nConsec As Integer, ByVal nConcept As Integer, ByVal sDescript As String, ByVal dIssue_Dat As Date, ByVal dLedger_dat As Date, ByVal sRequest_ty As String, ByVal nSta_cheque As Integer, ByVal dStat_date As Date, ByVal nUser_sol As Integer, ByVal nAux As Integer, ByVal sInter_pay As String, ByVal nAcc_type As Integer, ByVal sAcco_num As String, ByVal nBank_code As Integer, ByVal nBk_agency As Integer, ByVal sN_Aba As String, ByVal nCompany As Integer, ByVal nCurrencyPay As Integer, ByVal nTypesupport As Integer, ByVal nAmountPay As Double, ByVal nDocSupport As Double, ByVal nTaxCode As Integer, ByVal nTax_Percent As Double, ByVal nTax_Amount As Double, ByVal nAfect As Double, ByVal nExcent As Double, ByVal nOfficePay As Integer, ByVal nCurrencyOri As Integer, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0) As Boolean
        Dim lupdCurr_acc As eRemoteDB.Execute
        Dim lclsCash_Num As User_cashnum
        Dim lintCashNum As Integer

        lclsCash_Num = New User_cashnum
        If lclsCash_Num.Find_nUser(nUsercode, True) Then
            lintCashNum = lclsCash_Num.nCashNum
        End If
        'UPGRADE_NOTE: Object lclsCash_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCash_Num = Nothing


        On Error GoTo insPostOP091_Err

        If nProcess <> 3 Then
            If nIdDocument > 0 Then
                lupdCurr_acc = New eRemoteDB.Execute
                With lupdCurr_acc
                    .StoredProcedure = "insUpdCurr_accOP091"
                    .Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nIdDocument", nIdDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nType_pay", nType_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sDescript", Mid(sDescript, 1, 60), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dIssue_dat", dIssue_Dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRequest_ty", sRequest_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If nAux = 9998 Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("nAcc_bank", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        .Parameters.Add("nAcc_bank", nAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                    .Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sAcco_num", sAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sN_Aba", sN_Aba, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrencyPay", nCurrencyPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTypeSupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAmountPay", nAmountPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDocSupport", nDocSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTaxCode", nTaxCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax_Percent", nTax_Percent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax_amount", nTax_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nAfect", nAfect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nExcent", nExcent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nOfficePay", nOfficePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrencyOri", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCashNum", lintCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    insPostOP091 = .Run(False)
                End With
                'UPGRADE_NOTE: Object lupdCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lupdCurr_acc = Nothing
            End If
        End If

insPostOP091_Err:
        If Err.Number Then
            insPostOP091 = False
        End If
        On Error GoTo 0
    End Function
	
	'**%insPostOP092: this function is in charge of validate all the introduced data in the form.
	'%insPostOP092: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostOP092(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nTransac As Integer, ByVal nTypeMov As Integer, ByVal sDescript As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dValDate As Date, ByVal nCredit As Double, ByVal sAmoCreDeb As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nIntermed As Integer) As Boolean
		Dim lclsMove_Acc As eCashBank.Move_Acc
		
		lclsMove_Acc = New eCashBank.Move_Acc
		
		On Error GoTo insPostOP092_Err
		insPostOP092 = True
		
		Select Case nAction
			
			'**+ If the selected option is Register
			'+ Si la opción seleccionada es Registrar
			Case eFunctions.Menues.TypeActions.clngActionadd
				If nTypeMov = 10 Then
					If Not lclsMove_Acc.FindMoveByPeriod(nTypeAccount, sBussiType, sClient, nCurrency, dEffecdate, eRemoteDB.Constants.intNull, nTypeMov) Then
						With lclsMove_Acc
							.nTyp_acco = nTypeAccount
							.sType_acc = sBussiType
							.sClient = sClient
							.nCurrency = nCurrency
							.nType_move = nTypeMov
							.nIdconsec = eRemoteDB.Constants.intNull
							.sDescript = sDescript
							.nBranch = nBranch
							.nProduct = nProduct
							.nPolicy = nPolicy
							.dValueDate = dValDate
							.dOperdate = dEffecdate
							.nUsercode = nUsercode
							.nProcess = OP092ActVal.clngOP092Add
							.nDebit = IIf(sAmoCreDeb = "1", nCredit, 0)
							.nCredit = IIf(sAmoCreDeb = "2", nCredit, 0)
							.nIntermed = nIntermed
							If .UpdByManualMove Then
								insPostOP092 = True
							Else
								insPostOP092 = False
							End If
						End With
					Else
						insPostOP092 = False
					End If
				Else
					With lclsMove_Acc
						.nTyp_acco = nTypeAccount
						.sType_acc = sBussiType
						.sClient = sClient
						.nCurrency = nCurrency
						.nType_move = nTypeMov
						.nIdconsec = eRemoteDB.Constants.intNull
						.sDescript = sDescript
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.dValueDate = dValDate
						.dOperdate = dEffecdate
						.nUsercode = nUsercode
						.nProcess = OP092ActVal.clngOP092Add
						.nDebit = IIf(sAmoCreDeb = "1", nCredit, 0)
						.nCredit = IIf(sAmoCreDeb = "2", nCredit, 0)
						.nIntermed = nIntermed
						If .UpdByManualMove Then
							insPostOP092 = True
						Else
							insPostOP092 = False
						End If
					End With
				End If
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				With lclsMove_Acc
					.nTyp_acco = nTypeAccount
					.sType_acc = sBussiType
					.sClient = sClient
					.nCurrency = nCurrency
					.nType_move = nTypeMov
					.nIdconsec = nTransac
					.sDescript = sDescript
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = nPolicy
					.dValueDate = dValDate
					.dOperdate = dEffecdate
					.nUsercode = nUsercode
					.nProcess = OP092ActVal.clngOP092Upd
					.nDebit = IIf(sAmoCreDeb = "1", nCredit, 0)
					.nCredit = IIf(sAmoCreDeb = "2", nCredit, 0)
					.nIntermed = nIntermed
					If .UpdByManualMove Then
						insPostOP092 = True
					Else
						insPostOP092 = False
					End If
				End With
				
				'**+ If the selected option is Cut
				'+ Si la opción seleccionada es Cortar
			Case eFunctions.Menues.TypeActions.clngActioncut
				If Not lclsMove_Acc.FindLastMove(nTypeAccount, sBussiType, sClient, nCurrency) Then
					insPostOP092 = False
				Else
					With lclsMove_Acc
						.nTyp_acco = nTypeAccount
						.sType_acc = sBussiType
						.sClient = sClient
						.nCurrency = nCurrency
						.nType_move = nTypeMov
						.nIdconsec = nTransac
						.sDescript = sDescript
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.dValueDate = dValDate
						.dOperdate = dEffecdate
						.nUsercode = nUsercode
						.nProcess = OP092ActVal.clngOP092Cut
						.nDebit = IIf(sAmoCreDeb = "1", nCredit, 0)
						.nCredit = IIf(sAmoCreDeb = "2", nCredit, 0)
						.nIntermed = nIntermed
						If .UpdByManualMove Then
							insPostOP092 = True
						Else
							insPostOP092 = False
						End If
					End With
				End If
		End Select
		
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		
insPostOP092_Err: 
		If Err.Number Then
			insPostOP092 = False
		End If
	End Function
	
	'**% insValOP090_K: This function is in charge of validate the introduced data in the  header zone
	'**% for the form
	'%insValOP090_K: Esta función se encarga de validar los datos introducidos en la zona de encabezado
	'%para la forma.
	Public Function insValOP090_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal sClient As String, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_provider As Object
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsCompany As eGeneral.Company
		Dim lclsClient As eClient.Client
		
		Dim lintType_prov As Integer
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsIntermedia = New eAgent.Intermedia
		lclsCompany = New eGeneral.Company
		lclsClient = New eClient.Client
		
		On Error GoTo insValOP090_K_Err
		
		insValOP090_K = String.Empty
		lblnError = False
		
		'**+ Validation of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
			lblnError = True
		End If
		
		'**+Validation of the field "Type of Business"
		'+Validacion del campo "Tipo de Negocio"
		
		If (nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8) And (sBussiType = String.Empty Or sBussiType = "0") Then
			lclsErrors.ErrorMessage(sCodispl, 7250)
			lblnError = True
		End If
		
		'**+ Make the validations on the field "Client's Code"
		'+ Se realizan las validaciones sobre el campo "Código de cliente"
		
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 13667)
			lblnError = True
		Else
			
			'**+ Validate that the client's code is in the providers table
			'**+ (Tab_provider) if the type of account correspond to clinics, hospitals or workshops
			'+ Se valida que el código del cliente se encuentre en la tabla de proveedores
			'+ (Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
			If lclsClient.Find(sClient) Then
				If nTypeAccount = 4 Or nTypeAccount = 6 Or nTypeAccount = 7 Or nTypeAccount = 12 Then
					
					Select Case nTypeAccount
						Case 4 '- Profesionales
							lintType_prov = 3
						Case 6 '- Clinicas
							lintType_prov = 1
						Case 7 '- Talleres
							lintType_prov = 2
						Case 12 '- Proveedores
							lintType_prov = 4
						Case Else
							lintType_prov = eRemoteDB.Constants.intNull
					End Select
					
					If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
						lclsErrors.ErrorMessage(sCodispl, 4116)
						lblnError = True
					End If
				End If
				
				'**+ Validate taht the entity code is registered in the co/reinsurance companies' table
				'+ Se valida que el código de la entidad se encuentre registrado en la table de
				'+ compañias de co/reaseguro
				
				If nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8 Then
					If Not lclsCompany.FindClient(sClient) Then
						lclsErrors.ErrorMessage(sCodispl, 3068)
						lblnError = True
					End If
				End If
				
				'**+ Validate that the client's code is registered in the intermediary table
				'**+ if the type of account is of intermediary
				'+ Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
				'+ si el tipo de cuenta es de intermediario
				
				If nTypeAccount = 1 Or nTypeAccount = 10 Then
					If Not lclsIntermedia.Find_ClientInter(sClient) Then
						lclsErrors.ErrorMessage(sCodispl, 9002)
						lblnError = True
					End If
				End If
			Else
				
				'**- Error, in case the client does not exist
				'+ Error en caso de no existir el cliente
				lclsErrors.ErrorMessage(sCodispl, 7050)
				lblnError = True
			End If
		End If
		
		'**+ Validations of the field "Currency"
		'+Validacion del campo "Moneda"
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 10827)
			lblnError = True
		End If
		
		'**+ Validate the existence of a previous registration, and the correspondent message
		'**+ depending of the action where is in (register or consult)
		'+Se valida la existencia de un registro previo, y se envia el mensaje correspondiente
		'+dependiendo de la accion en que se encuentre (registrar o consultar)
		
		If Not lblnError Then
			If FindClientCurr_acc(nTypeAccount, sBussiType, sClient, nCurrency) Then
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					lclsErrors.ErrorMessage(sCodispl, 7113)
				End If
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
					lclsErrors.ErrorMessage(sCodispl, 7259)
				End If
			End If
			
		End If
		
		insValOP090_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP090_K_Err: 
		If Err.Number Then
			insValOP090_K = "insValOP090_K: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	'**% insValOP090: This function is in charge of validate the introduced data in the detail zone for form.
	'% insValOP090: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'% forma.
	Public Function insValOP090(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nLedCompan As Integer, ByVal sLedgerAcc As String, Optional ByVal sAuxAccount As String = "") As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLedgerAcc As eLedge.LedgerAcc
		Dim lblnFind As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsLedgerAcc = New eLedge.LedgerAcc
		
		On Error GoTo insValOP090_Err
		
		'**+ Validation of the field "date"
		'+Validación de campo "fecha"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7227)
		End If
		
		'**+ Validation of the field "Interface-BookKeeper Company"
		'+Validacion del campo "Interface-Contable Compañia"
		
		If nLedCompan = eRemoteDB.Constants.intNull Or nLedCompan = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7169)
			
			'**+ Validation of the field "Interface bookkeeper-Account"
			'+ Validacion del campo "Interface contable-Cuenta"
			
		Else
			If sLedgerAcc = String.Empty Then
				lclsErrors.ErrorMessage(sCodispl, 1027)
			Else
				If Not lclsLedgerAcc.Find_Account_o(nLedCompan, sLedgerAcc) Then
					lclsErrors.ErrorMessage(sCodispl, 1026)
				Else
					
					If lclsLedgerAcc.Find_AuxAccount(nLedCompan, sLedgerAcc, sAuxAccount) Then
						lblnFind = True
					Else
						lblnFind = False
					End If
					
					If sAuxAccount = String.Empty Then
						If lblnFind And lclsLedgerAcc.nAuxCount <> 0 Then
							lclsErrors.ErrorMessage(sCodispl, 36119)
						End If
					Else
						If Not lblnFind Then
							lclsErrors.ErrorMessage(sCodispl, 36021)
						End If
					End If
				End If
			End If
			
			'**+ Validation of the field "Bookkeeper Interface-auxiliary-account"
			'+Validacion del campo "Interface contable-auxiliar-cuenta"
			
		End If
		
		insValOP090 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsLedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedgerAcc = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP090_Err: 
		If Err.Number Then
			insValOP090 = "insValOP090: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValOP091_K: This funtion is in charge of validate the introduced data in the header zone
	'**% for the form OP091
	'%insValOP091_K: Esta función se encarga de validar los datos introducidos en la zona de encabezado
	'%para la forma OP091.
	Public Function insValOP091_K(ByVal sCodispl As String, ByVal nTypeTrans As Integer, ByVal nRemNum As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsMove_Acc As eCashBank.Move_Acc
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		
		lclsErrors = New eFunctions.Errors
		lclsMove_Acc = New eCashBank.Move_Acc
		lclsCtrol_date = New eGeneral.Ctrol_date
		
		On Error GoTo insValOP091_K_Err
		
		insValOP091_K = CStr(True)
		
		'**+ Validation of the field "Type of transaction"
		'+Validacion del campo "Tipo de transaccion"
		
		If nTypeTrans = eRemoteDB.Constants.intNull Or nTypeTrans = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7133)
		End If
		
		'**+ Validation of the field "Number of the payment remittance"
		'+Validacion del campo "Numero de la remesa de pago "
		
		If nRemNum = 0 Or nRemNum = eRemoteDB.Constants.intNull Then
			If nTypeTrans <> eRemoteDB.Constants.intNull And nTypeTrans <> 0 And nTypeTrans <> Table403Val.clngEntryRemitt Then
				lclsErrors.ErrorMessage(sCodispl, 7135)
			End If
		Else
			
			'**+ Validate the existence of the  remittance number in the table to verify its existence
			'+Se valida la existencia del numero de remesa en la tabla para verificar su existencia
			
			If nTypeTrans <> Table403Val.clngEntryRemitt Then
				If lclsMove_Acc.Find_document(4, nRemNum) Then
					'**+ Validate that if the action is reverse, the check must be printed
					'+Se valida que si la accion es reversar, el cheque debe estar impreso
					
					If nTypeTrans = Table403Val.clngRevertRemitt And lclsMove_Acc.sCheque = "0" Then
						If lclsMove_Acc.nSta_cheque <> 2 Then
							lclsErrors.ErrorMessage(sCodispl, 7251)
						End If
					End If
					
					'**+ Validate that, if the action is "Cut", the check must not be printed
					'+Se valida que, si la accion es "Cortar", el cheque no debe estar impreso
					
					If nTypeTrans = Table403Val.clngCutRemitt And lclsMove_Acc.sCheque <> "0" Then
						If lclsMove_Acc.nSta_cheque = 2 Then
							lclsErrors.ErrorMessage(sCodispl, 7138)
						End If
					End If
					
					'**+ If the action is "Cut" or "Reverse", the book keeper period of the movemente must not be closed
					'+Si la accion es "Cortar" o "Reversar", el periodo contable del movimiento no debe estar
					'+cerrado
					If (nTypeTrans = Table403Val.clngCutRemitt Or nTypeTrans = Table403Val.clngRevertRemitt) Then
						If lclsCtrol_date.Find(8) Then
							If lclsCtrol_date.dEffecdate > lclsMove_Acc.dOperdate Then
								lclsErrors.ErrorMessage(sCodispl, 7139)
							End If
						End If
						If Not lclsMove_Acc.insValLastMoveOP091(nRemNum, lclsMove_Acc.dOperdate) Then
							lclsErrors.ErrorMessage(sCodispl, 55853)
						End If
					End If
				Else
					lclsErrors.ErrorMessage(sCodispl, 7136)
				End If
			End If
		End If
		
		insValOP091_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
		
insValOP091_K_Err: 
		If Err.Number Then
			insValOP091_K = "insValOP091_K: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'**insValOP091: This function is in charge of validate the introduced data in the detail zone for form.
	'%insValOP091: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValOP091(ByVal sCodispl As String, ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nDeb As Integer, ByVal nTypePay As Integer, ByVal nPayAmount As Double, ByVal nAmount As Double, ByVal nUsercode As Integer, ByVal dValDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsMove_Acc As eCashBank.Move_Acc
		
		Dim lclsCompany As eGeneral.Company
		Dim lclsTab_provider As Object
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsSecurity As eSecurity.Secur_sche
		Dim lclsClient As eClient.Client
		
		Dim nCount As Byte
		Dim lintType_prov As Integer
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsMove_Acc = New eCashBank.Move_Acc
		lclsCompany = New eGeneral.Company
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsSecurity = New eSecurity.Secur_sche
		lclsClient = New eClient.Client
		lclsIntermedia = New eAgent.Intermedia
		
		On Error GoTo insValOP091_Err
		
		insValOP091 = String.Empty
		lblnError = False
		
		'**+ Validation of the field "Type of current account"
		'+Validacion del campo "Tipo de Cta. Corriente"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
			lblnError = True
		End If
		
		'**+ Validation of the field "Type of Business"
		'+Validacion del campo "Tipo de Negocio"
		
		If (nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8) And (sBussiType = String.Empty Or sBussiType = "0") Then
			lclsErrors.ErrorMessage(sCodispl, 7250)
			lblnError = True
		End If
		
		'**+ Validation of the field "Effect date"
		'+Validacion del campo "Fecha de efecto"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
			lblnError = True
		Else
			If lclsMove_Acc.FindMaxOper_date(nTypeAccount, IIf(sBussiType = String.Empty, "0", sBussiType), sClient, nCurrency) Then
				If dEffecdate < lclsMove_Acc.dOperdate Then
					lclsErrors.ErrorMessage(sCodispl, 7117)
					lblnError = True
				End If
			End If
		End If
		
		'**+ Make the validations on the field "Client's Code"
		'+ Se realizan las validaciones sobre el campo "Código de cliente"
		
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 13667)
			lblnError = True
		Else
			If lclsClient.Find(sClient) Then
				
				'**+ Validate that the client's code is in the provider's table (Tab_provider)
				'**+ if the type of account corresponds to clinics, hospitals or workshops.
				'+Se valida que el código del cliente se encuentre en la tabla de proveedores
				'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
				
				If nTypeAccount = 4 Or nTypeAccount = 6 Or nTypeAccount = 7 Or nTypeAccount = 12 Then
					Select Case nTypeAccount
						Case 4 '- Profesionales
							lintType_prov = 3
						Case 6 '- Clinicas
							lintType_prov = 1
						Case 7 '- Talleres
							lintType_prov = 2
						Case 12 '- Proveedores
							lintType_prov = 4
						Case Else
							lintType_prov = eRemoteDB.Constants.intNull
					End Select
					If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
						lclsErrors.ErrorMessage(sCodispl, 4116)
						lblnError = True
					End If
				End If
				
				'**+ Validate that the entity's code is registered in the co/reinsurance companies' table.
				'+Se valida que el código de la entidad se encuentre registrado en la tabla de
				'+compañias de co/reaseguro
				
				If nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8 Then
					If Not lclsCompany.FindClient(sClient) Then
						lclsErrors.ErrorMessage(sCodispl, 3068)
						lblnError = True
					End If
				End If
				
				'**+ Validates that the client's code is registered in the intermediaries table
				'**+ if the type of account is of intermediary.
				'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
				'+si el tipo de cuenta es de intermediario
				
				If nTypeAccount = 1 Or nTypeAccount = 10 Then
					If Not lclsIntermedia.Find_ClientInter(sClient) Then
						lclsErrors.ErrorMessage(sCodispl, 9002)
						lblnError = True
					End If
				End If
			Else
				
				
				
				'**+ It is called "Client's Module" only if the type of account corresponds to
				'**+ clients. In other way, you must send the correspondent validation.
				'+ Solamente se llama al modulo de clientes, si el tipo de cuente corresponde a
				'+ clientes. En caso contrario se debe enviar la validacion correspondiente
				
				If nTypeAccount = 5 Then
					lclsErrors.ErrorMessage(sCodispl, 1007)
					lblnError = True
				End If
			End If
		End If
		
		'**+ Validations of the field "Currency"
		'+Validaciones del campo Moneda
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 10827)
			lblnError = True
		Else
			'**+ If the type of movement does not correspond to a credit note and the
			'**+ zone fields (brach office) and currency are not full, the cash account
			'*++ must be registered
			'+ Si el tipo de movimiento no corresponde a nota de credito y los campos
			'+ zona (sucursal) y moneda se encuentran llenos, la cuenta de caja debe
			'+ estar registrada
			If Not lclsSecurity.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
				lblnError = True
			End If
			
			If nCurrency <> 1 And dValDate = dtmNull Then
				lclsErrors.ErrorMessage(sCodispl, 55527)
				lblnError = True
			End If
		End If
		
		If Not lblnError Then
			If Not FindClientCurr_acc(nTypeAccount, sBussiType, sClient, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 7111)
			Else
				If nAmount = 0 Then
					'**+ Validates that the debit amount of the current account is not the same as the  credit amount of the same
					'+Se valida que el importe de débitos de la cuenta corriente no sea igual al importe de créditos de la misma
					lclsErrors.ErrorMessage(sCodispl, 7156)
				Else
					'**+ Validate
					'+Se valida que el importe de débitos de la cuenta corriente no sea superior al importe de créditos de la misma
					If nDeb = 1 Then
						lclsErrors.ErrorMessage(sCodispl, 7157)
					End If
				End If
			End If
		End If
		
		'**+ Validations of the field "Payment-Type"
		'+Validaciones del campo "Pago-tipo"
		
		If nTypePay = eRemoteDB.Constants.intNull Or nTypePay = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 4045)
		End If
		
		'**+ Validations of the field "Payment-Amount"
		'+Validaciones del campo "Pago-Importe"
		
		If nPayAmount = 0 Or nPayAmount = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 7118)
		Else
			
			'**+ Validate that tha amount of payment is not more than the current account balance
			'+Se valida que el monto del pago no sea mayor que el saldo de la cuenta corriente
			
			If nPayAmount <> 0 And nPayAmount > nAmount And nDeb = 2 Then
				lclsErrors.ErrorMessage(sCodispl, 7144)
			End If
		End If
		
		insValOP091 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurity = Nothing
		
insValOP091_Err: 
		If Err.Number Then
			insValOP091 = "insValOP091: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOPC013_K: This function is in charge of validate the introduced data in the header form
	'%insValOPC013_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'                 forma.
	Public Function insValOPC013_K(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nTypeAccount As Integer, ByVal nIntermed As Integer, ByVal nCurrency As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsIntermedia As eAgent.Intermedia
		
		Dim nCount As Integer
		Dim lblnValidCurency As Boolean
		
		lclsIntermedia = New eAgent.Intermedia
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOPC013_K_Err
		
		'**+ Validation of the field "Date"
		'+Validacion del campo "Fecha"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
		End If
		
		'**+ Validations of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
		End If
		
		'**+ Make the correspondent validations of the field "Support-Intermediary"
		'+ Se realizan las validaciones correspondientes al campo "Soporte-Intermediario"
		
		If nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 7020)
		Else
			
			'**+Validate that the intermediary is registered in the file of intermediaries
			'+ Se valida que el intermediario se encuentre registrado en el archivo de
			'+ intermediarios
			
			If lclsIntermedia.Find(nIntermed) Then
				sClient = lclsIntermedia.sClient
				sClienames = lclsIntermedia.sCliename
				If FindCountCurrency(nTypeAccount, "0", CStr(nIntermed)) Then
					If nCount = 1 Then
						Call FindClientCurr_acc(nTypeAccount, "0", CStr(nIntermed), eRemoteDB.Constants.intNull)
						lblnValidCurency = False
					Else
						lblnValidCurency = True
					End If
				Else
					lblnValidCurency = True
				End If
			End If
		End If
		
		'**+ Validations of the field "Currency"
		'+Validaciones del campo "Moneda"
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7132)
		Else
			
			'**+ If the type of movement does not correspond to a credit note, and the
			'**+ zone fields (brach office) and currency are full, the cash account must
			'**+ be registered
			'+ Si el tipo de movimiento no corresponde a nota de credito y los campos
			'+ zona (sucursal) y moneda se encuentran llenos, la cuenta de caja debe
			'+ estar registrada
			Call AccCount(nTypeAccount, "0", lclsIntermedia.sClient)
			
			If lblnValidCurency Then
				'**+ Validate the existence of a current account with the key fields combinations
				'**+ (Client, Type of account, Type of business)
				'+ Se valida que exista una cuenta corriente con la combinación de los campos
				'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
				If Not FindClientCurr_acc(nTypeAccount, "0", lclsIntermedia.sClient, nCurrency) Then
					lclsErrors.ErrorMessage(sCodispl, 7122)
				End If
			End If
		End If
		
		insValOPC013_K = lclsErrors.Confirm
		
		
insValOPC013_K_Err: 
		If Err.Number Then
			insValOPC013_K = insValOPC013_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	'**% insValOP092_K: This funtion is in charge of validate the introduced data in the header zone
	'**% for the form.
	'% insValOP092_K: Esta función se encarga de validar los datos introducidos en la zona de encabezado
	'% para la forma.
	Public Function insValOP092_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nTypeAccount As Integer, ByVal sBussiType As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nTransact As Integer, ByVal dEffecdate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lvalClient As eClient.ValClient
		Dim lClienTime As eClient.Client
		Dim lclsTab_provider As Object
		Dim lclsCompany As eGeneral.Company
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsSecur_sche As eSecurity.Secur_sche
		Dim lclsMove_Acc As eCashBank.Move_Acc
		Dim lintType_prov As Integer
		Dim lblnError As Boolean
		
		lvalClient = New eClient.ValClient
		lClienTime = New eClient.Client
		lclsTab_provider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
		lclsCompany = New eGeneral.Company
		lclsIntermedia = New eAgent.Intermedia
		lclsSecur_sche = New eSecurity.Secur_sche
		lclsMove_Acc = New eCashBank.Move_Acc
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOP092_K_Err
		
		insValOP092_K = String.Empty
		lblnError = False
		
		'**+ Validation of the field "Type"
		'+Validacion del campo "Tipo"
		
		If nTypeAccount = eRemoteDB.Constants.intNull Or nTypeAccount = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7107)
			lblnError = True
		End If
		
		'**+ Validations of the field "Type of business"
		'+Validacion del campo "Tipo de Negocio"
		
		If (nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8) And (sBussiType = String.Empty Or sBussiType = "0") Then
			lclsErrors.ErrorMessage(sCodispl, 7250)
			lblnError = True
		End If
		If sClient = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 13667)
			lblnError = True
		Else
			If Not lvalClient.Validate(sClient, nAction) Then
				Select Case lvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						lclsErrors.ErrorMessage(sCodispl, 2012)
						lblnError = True
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						lclsErrors.ErrorMessage(sCodispl, 2013)
						lblnError = True
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						lclsErrors.ErrorMessage(sCodispl, 2228)
						lblnError = True
				End Select
			Else
				If lClienTime.Find(sClient) Then
					
					'**+ Validate that the client's code is in the provider's table (Tab_Provider)
					'**+ if the type of account corresponds to clinics, hospitals or workshops
					'+Se valida que el código del cliente se encuentre en la tabla de proveedores
					'+(Tab_provider) si el tipo de cuenta corresponde a clinicas,hospitales o talleres
					
					If nTypeAccount = 4 Or nTypeAccount = 6 Or nTypeAccount = 7 Or nTypeAccount = 12 Then
						
						Select Case nTypeAccount
							Case 4 '- Profesionales
								lintType_prov = 3
							Case 6 '- Clinicas
								lintType_prov = 1
							Case 7 '- Talleres
								lintType_prov = 2
							Case 12 '- Proveedores
								lintType_prov = 4
							Case Else
								lintType_prov = eRemoteDB.Constants.intNull
						End Select
						
						If Not lclsTab_provider.FindClient(sClient, lintType_prov) Then
							lclsErrors.ErrorMessage(sCodispl, 4116)
							lblnError = True
						End If
					End If
					
					'**+ Validate that the entity's code is registered in the co/reinsurance companies table.
					'+Se valida que el código de la entidad se encuentre registrado en la table de
					'+compañias de co/reaseguro
					
					If nTypeAccount = 2 Or nTypeAccount = 3 Or nTypeAccount = 8 Then
						
						If Not lclsCompany.FindClient(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 3068)
							lblnError = True
						End If
					End If
					
					'**+ Validate that the client's code is registered in the intermediaries table
					'**+ if the type of account is of intermediary
					'+Se valida que el codigo de cliente se encuentre registrado en la tabla de intermediarios
					'+si el tipo de cuenta es de intermediario
					
					If nTypeAccount = 1 Or nTypeAccount = 10 Then
						If Not lclsIntermedia.Find_ClientInter(sClient) Then
							lclsErrors.ErrorMessage(sCodispl, 9002)
							lblnError = True
						End If
					End If
				Else
					
					'**+ It is called Client's Module only if the type of account corresponds to
					'**+ clients. In other way, the correspondent validation must be sent
					'+ Solamente se llama al modulo de clientes, si el tipo de cuenta corresponde a
					'+ clientes. En caso contrario se debe enviar la validacion correspondiente
					
					If nTypeAccount = 5 Or nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
						lclsErrors.ErrorMessage(sCodispl, 1007)
						lblnError = True
					End If
				End If
			End If
		End If
		'**+ Validations of the Currency Table
		'+Validaciones del campo Moneda
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7132)
			lblnError = True
		Else
			
			'**+ If the type of movement does not correspond to a credit note and the
			'**+ zone fields (branch office) and currecny are full, the cash account must be
			'**+ registred
			'+ Si el tipo de movimiento no corresponde a nota de credito y los campos
			'+ zona (sucursal) y moneda se encuentran llenos, la cuenta de caja debe
			'+ estar registrada
			
			If Not lclsSecur_sche.valCurrency_Schema(nUsercode, nCurrency) Then
				lclsErrors.ErrorMessage(sCodispl, 99024)
				lblnError = True
			End If
			
			'**+ Validate that a current account exists with the key fields combinations
			'**+ (Client,Type of account, Type of business)
			'+ Se valida que exista una cuenta corriente con la combinación de los campos
			'+claves (Cliente,Tipo de cuenta, Tipo de negocio)
			If Not lblnError Then
				If Not FindClientCurr_acc(nTypeAccount, sBussiType, sClient, nCurrency) Then
					lclsErrors.ErrorMessage(sCodispl, 7111)
					lblnError = True
				End If
			End If
		End If
		
		'**+ Validations of the field "Effect date"
		'+Validacion del campo "Fecha de efecto"
		
		If dEffecdate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
			lblnError = True
		Else
			
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				
				'**+ It must not be after the day's date
				'+No debe ser mayor a la fecha del dia
				
				If dEffecdate > Today Then
					lclsErrors.ErrorMessage(sCodispl, 7161)
					lblnError = True
				End If
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					'+ En actualización, no puede ser anterior a la fecha del ultimo movimiento de la cuenta corriente
					If lclsMove_Acc.FindMinOper_date(nTypeAccount, sBussiType, sClient, nCurrency) Then
						If dEffecdate < lclsMove_Acc.dOperdate Then
							lclsErrors.ErrorMessage(sCodispl, 7117)
							lblnError = True
						End If
					End If
					
				End If
			End If
		End If
		
		'**+ Validation of the field "Movement"
		'+Validacion del campo "Movimiento"
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
			If nTransact = 0 Or nTransact = eRemoteDB.Constants.intNull Then
				lclsErrors.ErrorMessage(sCodispl, 13255)
				lblnError = True
			Else
				If Not lblnError Then
					
					'**+ Make the call to the SP "reaMove_Acc_o" for validating the existence of the account's movement
					'+ Se realiza el llamado al SP "reaMove_Acc_o", para validar la existencia del movimiento de la cuenta
					
					If lclsMove_Acc.FindMove(nTypeAccount, sBussiType, sClient, nCurrency, dEffecdate, nTransact) Then
						If lclsMove_Acc.sManualMov <> "1" Then
							lclsErrors.ErrorMessage(sCodispl, 7254)
						End If
					Else
						lclsErrors.ErrorMessage(sCodispl, 1073)
					End If
				End If
			End If
		End If
		
		insValOP092_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalClient = Nothing
		'UPGRADE_NOTE: Object lClienTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lClienTime = Nothing
		'UPGRADE_NOTE: Object lclsTab_provider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_provider = Nothing
		'UPGRADE_NOTE: Object lclsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCompany = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecur_sche = Nothing
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP092_K_Err: 
		If Err.Number Then
			insValOP092_K = "insValOP092_K: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValOP092: This function is in charge of validate the introduced data in the detail zone for the form OP092
	'%insValOP092: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma OP092.
	Public Function insValOP092(ByVal sCodispl As String, ByVal nTypeMov As Integer, ByVal sDescript As String, ByVal dValDate As Date, ByVal nCredit As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByRef nTypeAccount As Integer = 0, Optional ByRef sBussiType As String = "", Optional ByRef sClient As String = "", Optional ByRef nCurrency As Integer = 0, Optional ByRef dEffecdate As Date = #12:00:00 AM#, Optional ByRef nIntermed As Integer = 0) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsMove_Acc As eCashBank.Move_Acc
		Dim lclsPolicy As Object
		Dim lclsCommission As Object
		Dim lcolCommission As Object
		Dim lclsIntermedia As eAgent.Intermedia
		
		lclsErrors = New eFunctions.Errors
		lclsMove_Acc = New eCashBank.Move_Acc
		
		On Error GoTo insValOP092_Err
		
		insValOP092 = CStr(True)
		
		'**+ Validation of the field "Movement-Type"
		'+Validacion del campo "Movimiento-Tipo"
		
		If nTypeMov = eRemoteDB.Constants.intNull Or nTypeMov = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7125)
		Else
			If nTypeMov = 10 Then
				If Not lclsMove_Acc.FindMoveByPeriod(nTypeAccount, sBussiType, sClient, nCurrency, dEffecdate, eRemoteDB.Constants.intNull, nTypeMov) Then
					lclsErrors.ErrorMessage(sCodispl, 6038)
				End If
			ElseIf nTypeMov = 322 Then 
				If Not (nIntermed > 0) Then
					lclsErrors.ErrorMessage(sCodispl, 750119)
				End If
			End If
		End If
		
		'**+ Validation of the field "Movement-Description"
		'+Validacion del campo "Movimiento-Descripción"
		
		If sDescript = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 10010)
		End If
		
		'**+ Validations of the field "Movement-Date-Value"
		'+Validacion del campo "Movimiento-Fecha-valor"
		
		If dValDate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 7116)
		End If
		
		
		'+Validación del campo Ramo
		
		If nPolicy <> eRemoteDB.Constants.intNull And nBranch = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'+Validación del campo Producto
		
		If nPolicy <> eRemoteDB.Constants.intNull And nProduct = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 3635)
		End If
		
		'+Validación del campo Póliza
		
		If nPolicy <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nBranch <> eRemoteDB.Constants.intNull Then
			
			lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
			
			If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
				lclsErrors.ErrorMessage(sCodispl, 8071)
			Else
				Select Case nTypeAccount
					Case 1, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
						
						lcolCommission = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Commissions")
						lclsCommission = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Commission")
						If lcolCommission.Find("2", nBranch, nProduct, nPolicy, dEffecdate, 0, True) Then
							lclsIntermedia = New eAgent.Intermedia
							For	Each lclsCommission In lcolCommission
								If lclsIntermedia.Find(lclsCommission.nIntermed, True) Then
									If lclsIntermedia.sClient <> sClient Then
										lclsErrors.ErrorMessage(sCodispl, 60000)
									End If
								End If
							Next lclsCommission
							'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsIntermedia = Nothing
						End If
						'UPGRADE_NOTE: Object lclsCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCommission = Nothing
						'UPGRADE_NOTE: Object lcolCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lcolCommission = Nothing
					Case Else
						If lclsPolicy.sClient <> sClient Then
							lclsErrors.ErrorMessage(sCodispl, 55846)
						End If
				End Select
			End If
			'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy = Nothing
		End If
		
		'+Si el Movimiento corresponde a Abono de prima futura y póliza no esta lleno
		
		If nTypeMov = 323 And nPolicy = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 3003)
		End If
		
		'**+ Validation of the field "Amount"
		'+Validacion del campo "Importe"
		
		If nCredit = 0 Or nCredit = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 7118)
		End If
		
		insValOP092 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMove_Acc = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP092_Err: 
		If Err.Number Then
			insValOP092 = "insValOP092: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'**%Find: Its in charge of making the correspondent reading of the curent accounts table
	'**% to obtain the valid registration
	'%Find: Se encarga de realizar la lectura  correspondiente  a la tabla de cuentas      */
	'%corrientes, para obtener el registro  valido
	Public Function Find(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaCurr_acc As eRemoteDB.Execute
		
		lrecreaCurr_acc = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nTyp_acco <> nTyp_acco Or Me.sType_acc <> sType_acc Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nCurrency <> nCurrency Or lblnFind Then
			
			Me.nTyp_acco = nTyp_acco
			Me.sType_acc = sType_acc
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nPolicy = nPolicy
			Me.nCertif = nCertif
			Me.nCurrency = nCurrency
			
			'** Parameter definition for stored procedure 'insudb.reaCurr_acc'
			'Definición de parámetros para stored procedure 'insudb.reaCurr_acc'
			'** Data of March 19, 2001  14:59:03
			'Información leída el 19/03/2001 14:59:03
			
			With lrecreaCurr_acc
				.StoredProcedure = "reaCurr_acc"
				.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sClient = .FieldToClass("sClient")
					nBalance = .FieldToClass("nBalance")
					nCredit = .FieldToClass("nCredit")
					nDebit = .FieldToClass("nDebit")
					dEffecdate = .FieldToClass("dEffecdate")
					sStatregt = .FieldToClass("sStatregt")
					nLed_compan = .FieldToClass("nLed_compan")
					sAccount = .FieldToClass("sAccount")
					sCertype = .FieldToClass("sCertype")
					nCompany = .FieldToClass("nCompany")
					sAux_accoun = .FieldToClass("sAux_accoun")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCurr_acc = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'----------------------------------------------------
	Public Function insvalLastMovement(ByVal nTypeAccount As Integer, ByVal nBussinesType As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Boolean
		'----------------------------------------------------
		Dim lrecreaLastMove_Acc2 As eRemoteDB.Execute
		
		On Error GoTo insvalLastMovement_Err
		
		insvalLastMovement = False
		
		lrecreaLastMove_Acc2 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaLastMove_Acc2'
		'Información leída el 11/07/2000 16:28:12
		
		With lrecreaLastMove_Acc2
			.StoredProcedure = "reaLastMove_Acc2"
			.Parameters.Add("nTyp_acco", nTypeAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", nBussinesType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("dOperdate") = dOperdate Then
					nIdconsec = .FieldToClass("nIdConsec") + 1
				Else
					nIdconsec = 1
				End If
				insvalLastMovement = True
				.RCloseRec()
			End If
		End With
		
insvalLastMovement_Err: 
		If Err.Number Then
			insvalLastMovement = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaLastMove_Acc2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLastMove_Acc2 = Nothing
		
	End Function
End Class






