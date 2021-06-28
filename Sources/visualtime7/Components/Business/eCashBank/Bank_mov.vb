Option Strict Off
Option Explicit On
Public Class Bank_mov
	'%-------------------------------------------------------%'
	'% $Workfile:: Bank_mov.cls                             $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 29/09/03 3:17p                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	'Guber
	'**-Properties according to the table in the system as of February 8,2001.
	'**-The key field of the table corresponds to: nAcc_bank, dEffecdate and nMovement.
	'-Propiedades según la tabla en el sistema al 08/02/2001.
	'-El campo llave de la tabla corresponde a: nAcc_bank, dEffecdate y nMovement.
	
	'   Column_name                    Type      Computed  Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public sDep_number As String 'char         no       12                      yes           no                   yes
	Public sNull_movem As String 'char         no       1                       yes           no                   yes
	Public sNull_recor As String 'char         no       1                       yes           no                   yes
	Public dEffecdate As Date 'datetime     no       8                       no            (n/a)                (n/a)
	Public dDoc_date As Date 'datetime     no       8                       yes           (n/a)                (n/a)
	Public nCash_amoun As Double 'decimal      no       9           14    2     yes           (n/a)                (n/a)
	Public nCheq_amoun As Double 'decimal      no       9           14    2     yes           (n/a)                (n/a)
	Public nMovement As Integer 'int          no       4           10    0     no            (n/a)                (n/a)
	Public nYear_month As Integer 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public nBordereaux As Double 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public sDocnumbe As String 'char         no       10                      yes           no                   yes
	Public nConcept As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public nAcc_bank As Integer 'smallint     no       2           5     0     no            (n/a)                (n/a)
	Public sClient As String 'char         no       14                      yes           no                   yes
	Public nType_mov As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public nIntermed As Integer 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public nUsercode As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public sNumForm As String 'char         no       12                      yes           no                   yes
	Public nClaim As Double 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public nVoucher_le As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public nContrat As Integer 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public nDraft As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public nCompanyc As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public nVoucher As Integer 'int          no       4           10    0     yes           (n/a)                (n/a)
	Public nTyp_acco As Integer 'smallint     no       2           5     0     yes           (n/a)                (n/a)
	Public sType_acc As String 'char         no       1                       yes           no                   yes
	Public dPosted As Date 'datetime     no       8                       yes           (n/a)                (n/a)
	'+ agregado para OP002 jgt
	Public nCashNum As Integer 'int          no       5           5     0     no            (n/a)
	Public dRealDep As Date
	
	'**-Auxiliary Variables
	'-Variables Auxiliares
	
	Public dDepDate As Date
	Public nOffice As Integer
	Public nCurrency As Integer
	Public nMov_type As Integer
	Public nAmount As Double
	Public nCash_Amount As Double
	Public nBank_code As Integer
	
	'**%AddBankDeposit: Deposits cash, checks or vouchers of credit cards on different accounts owned by the enterprise.
	'%AddBankDeposit: Deposita efectivo, cheques o vouchers de tarjetas de crédito
	'%en las distintas cuentas que posee la empresa
	Public Function AddBankDeposit() As Boolean
		
		'**-Variable definition lrecinsBankDeposit
		'-Se define la variable lrecinsBankDeposit
		
		Dim lrecinsBankDeposit As eRemoteDB.Execute
		On Error GoTo AddBankDeposit_Err
		lrecinsBankDeposit = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.insBankDEposit'
		'**+Information read on February 8, 2001  15:07:30
		'+Definición de parámetros para stored procedure 'insudb.insBankDeposit'
		'+Información leída el 08/02/2001 15:07:30
		
		With lrecinsBankDeposit
			.StoredProcedure = "insBankDeposit"
			.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDepDate", dDepDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_Type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_Amount", nCash_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompanyc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRealDep", dRealDep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddBankDeposit = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsBankDeposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBankDeposit = Nothing
		
AddBankDeposit_Err: 
		If Err.Number Then
			AddBankDeposit = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**%Find_sDep_number: Deposits cash, checks or vouchers of credit cards on different accounts owned by the enterprise.
	'%Find_sDep_number: Obtiene la información según número de depósito.
	Public Function Find_sDep_number(ByVal sDep_number As String, ByVal nType_mov As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Variable definition lrecinsBankDeposit
		'-Se define la variable lrecinsBankDeposit
		
		Dim lrecinsBankDeposit As eRemoteDB.Execute
		On Error GoTo Find_sDep_number_Err
		lrecinsBankDeposit = New eRemoteDB.Execute
		
		
		Find_sDep_number = True
		
		If Me.sDep_number <> sDep_number Or Me.nType_mov <> nType_mov Or lblnFind Then
			
			With lrecinsBankDeposit
				.StoredProcedure = "reaBank_mov_sDepNumber"
				.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_mov", nType_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find_sDep_number = .Run()
				If Find_sDep_number Then
					Me.sDep_number = .FieldToClass("sDep_number")
					Me.sNull_movem = .FieldToClass("sNull_movem")
					Me.sNull_recor = .FieldToClass("sNull_recor")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.dDoc_date = .FieldToClass("dDoc_date")
					Me.nCash_amoun = .FieldToClass("nCash_amoun")
					Me.nCheq_amoun = .FieldToClass("nCheq_amoun")
					Me.nMovement = .FieldToClass("nMovement")
					Me.nYear_month = .FieldToClass("nYear_month")
					Me.nBordereaux = .FieldToClass("nBordereaux")
					Me.sDocnumbe = .FieldToClass("sDocnumbe")
					Me.nConcept = .FieldToClass("nConcept")
					Me.nAcc_bank = .FieldToClass("nAcc_bank")
					Me.sClient = .FieldToClass("sClient")
					Me.nType_mov = .FieldToClass("nType_mov")
					Me.nIntermed = .FieldToClass("nIntermed")
					Me.sNumForm = .FieldToClass("sNumForm")
					Me.nClaim = .FieldToClass("nClaim")
					Me.nVoucher_le = .FieldToClass("nVoucher_le")
					Me.nContrat = .FieldToClass("nContrat")
					Me.nDraft = .FieldToClass("nDraft")
					Me.nCompanyc = .FieldToClass("nCompanyc")
					Me.nVoucher = .FieldToClass("nVoucher")
					Me.nTyp_acco = .FieldToClass("nTyp_acco")
					Me.sType_acc = .FieldToClass("sType_acc")
					Me.dPosted = .FieldToClass("dPosted")
					Me.nCurrency = .FieldToClass("nCurrency")
				End If
			End With
		End If
		
Find_sDep_number_Err: 
		If Err.Number Then
			Find_sDep_number = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsBankDeposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBankDeposit = Nothing
	End Function
	
	'% FindLastMovement: Obtiene el último movimiento registrado en la tabla Bank_mov
	'**% FindLastMovement: Gets the last movement recorded into table Bank_mov
	Public Function FindLastMovement(ByVal nAcc_bank As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecFindLastMovement As eRemoteDB.Execute
		
		'+ Definición de store procedure FindLastMovement al 04-24-2002 22:13:59
		On Error GoTo FindLastMovement_Err
		lrecFindLastMovement = New eRemoteDB.Execute
		With lrecFindLastMovement
			.StoredProcedure = "FindLastMovement"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				FindLastMovement = .FieldToClass("nMovement")
			Else
				FindLastMovement = 0
			End If
		End With
FindLastMovement_Err: 
		If Err.Number Then
			FindLastMovement = 0
		End If
		'UPGRADE_NOTE: Object lrecFindLastMovement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindLastMovement = Nothing
		On Error GoTo 0
	End Function
End Class






