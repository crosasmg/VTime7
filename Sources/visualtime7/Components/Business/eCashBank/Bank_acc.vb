Option Strict Off
Option Explicit On
Public Class Bank_acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Bank_acc.cls                             $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 3/11/03 7:19p                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	'Column_name                   Type       Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nAcc_bank As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public sAcc_ledger As String 'char         20                      yes                                 no                                  yes
	Public nBank_code As Integer 'int          4           10    0     no                                  (n/a)                               (n/a)
	Public nBk_agency As Integer 'int          4           10    0     no                                  (n/a)                               (n/a)
	Public sAcc_number As String 'char         25                      yes                                 no                                  yes
	Public nAcc_type As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public sAux_accoun As String 'char         20                      yes                                 no                                  yes
	Public nAvail_type As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nAvailable As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public nLed_compan As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nOffice As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nTransit_1 As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nTransit_2 As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nTransit_3 As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nTransit_4 As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nTransit_5 As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public sStatregt As String 'char         1                       yes                                 no                                  yes
	Public nCompany As Integer 'smallint     2           5     0     yes
	
	'+Variables auxiliares
	Public sDescript As String
	Public sShort_des As String
	Public sAgencyDesc As String
	Public sCliename As String
	Public sAccDesc As String
	Public sAuxDesc As String
	Public sCurrDescript As String
	Private mstrCondition As String
	Public mclsCheque As Cheque
	
	Public ReadOnly Property Sql() As Object
		Get
			Sql = mstrCondition
		End Get
	End Property
	
	' %Find: Funcion que busca registros en la tabla Bank_acc
	Public Function Find(ByVal lintAcc_bank As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaBank_acc As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If lintAcc_bank <> nAcc_bank Or lblnFind Then
			nAcc_bank = lintAcc_bank
			lrecreaBank_acc = New eRemoteDB.Execute
			With lrecreaBank_acc
				'Definición de parámetros para stored procedure 'insudb.reaBank_acc'
				'Información leída el 07/11/2000 10:14:03 AM
				.StoredProcedure = "reaBank_acc"
				.Parameters.Add("nAcc_bank", lintAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nAcc_bank = .FieldToClass("nAcc_bank")
					sAcc_ledger = .FieldToClass("sAcc_ledger")
					nBank_code = .FieldToClass("nBank_code")
					nBk_agency = .FieldToClass("nBk_agency")
					sAcc_number = .FieldToClass("sAcc_number")
					nAcc_type = .FieldToClass("nAcc_type")
					sAux_accoun = .FieldToClass("sAux_accoun")
					nAvail_type = .FieldToClass("nAvail_type")
					nAvailable = .FieldToClass("nAvailable")
					nCurrency = .FieldToClass("nCurrency")
					dEffecdate = .FieldToClass("dEffecdate")
					nLed_compan = .FieldToClass("nLed_compan")
					nOffice = .FieldToClass("nOffice")
					nTransit_1 = .FieldToClass("nTransit_1")
					nTransit_2 = .FieldToClass("nTransit_2")
					nTransit_3 = .FieldToClass("nTransit_3")
					nTransit_4 = .FieldToClass("nTransit_4")
					nTransit_5 = .FieldToClass("nTransit_5")
					sStatregt = .FieldToClass("sStatregt")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaBank_acc = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Find_Account_by_client(ByVal lstrClient As String, ByVal lstrAccount As String) As Boolean
		Dim lrectabBk_Account As eRemoteDB.Execute
		
		On Error GoTo Find_Account_by_client_Err
		lrectabBk_Account = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.tabBk_Account'
		'Información leída el 09/11/2000 5:44:52 PM
		With lrectabBk_Account
			.StoredProcedure = "tabBk_Account"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sShowNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", "sAccount = " & lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Account_by_client = True
				.RCloseRec()
			Else
				Find_Account_by_client = False
			End If
		End With
		'UPGRADE_NOTE: Object lrectabBk_Account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectabBk_Account = Nothing
		
Find_Account_by_client_Err: 
		If Err.Number Then
			Find_Account_by_client = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Add() As Boolean
		
		Dim lrecCreBank_acc As eRemoteDB.Execute
		
		Dim lintBK_agency As Integer
		
		On Error GoTo Add_Err
		
		If nBk_agency <= 0 Or nBk_agency = eRemoteDB.Constants.intNull Then
			lintBK_agency = eRemoteDB.Constants.intNull
		Else
			lintBK_agency = nBk_agency
		End If
		lrecCreBank_acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreBank_acc'
		'Información leída el 17/11/2000 5:36:27 PM
		
		With lrecCreBank_acc
			.StoredProcedure = "CreBank_acc"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcc_Number", sAcc_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBK_agency", lintBK_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvail_type", nAvail_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_1", nTransit_1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_2", nTransit_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_3", nTransit_3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_4", nTransit_4, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_5", nTransit_5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcc_ledger", sAcc_ledger, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecCreBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreBank_acc = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	
	Public Function Update() As Boolean
		Dim lrecUpdBank_acc As eRemoteDB.Execute
		
		Dim lintBK_agency As Integer
		
		On Error GoTo Update_Err
		lrecUpdBank_acc = New eRemoteDB.Execute
		
		If nBk_agency <= 0 Or nBk_agency = eRemoteDB.Constants.intNull Then
			lintBK_agency = eRemoteDB.Constants.intNull
		Else
			lintBK_agency = nBk_agency
		End If
		
		
		'Definición de parámetros para stored procedure 'insudb.UpdBank_acc'
		'Información leída el 17/11/2000 5:38:46 PM
		With lrecUpdBank_acc
			.StoredProcedure = "UpdBank_acc"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcc_Number", sAcc_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBK_agency", lintBK_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvail_type", nAvail_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_1", nTransit_1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_2", nTransit_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_3", nTransit_3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_4", nTransit_4, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransit_5", nTransit_5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcc_ledger", sAcc_ledger, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecUpdBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdBank_acc = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	Public Function Delete(ByRef lintAcc_bank As Integer) As Boolean
		Dim lrecdelBank_acc As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelBank_acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delBank_acc'
		'Información leída el 17/11/2000 5:42:53 PM
		With lrecdelBank_acc
			.StoredProcedure = "delBank_acc"
			.Parameters.Add("nAcc_bank", lintAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelBank_acc = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Find_O(ByVal lintAcc_bank As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaBank_acc As eRemoteDB.Execute
		
		On Error GoTo Find_O_Err
		If lintAcc_bank <> nAcc_bank Or lblnFind Then
			nAcc_bank = lintAcc_bank
			lrecreaBank_acc = New eRemoteDB.Execute
			With lrecreaBank_acc
				'Definición de parámetros para stored procedure 'insudb.reaBank_acc'
				'Información leída el 07/11/2000 10:14:03 AM
				.StoredProcedure = "reaBank_acc_o"
				.Parameters.Add("nAcc_bank", lintAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sAcc_number = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sDescript = .FieldToClass("sDescript")
					sAgencyDesc = .FieldToClass("sAgencyDesc")
					sCliename = .FieldToClass("sCliename")
					sAccDesc = .FieldToClass("sAccDesc")
					sAuxDesc = .FieldToClass("sAuxDesc")
					nAcc_bank = .FieldToClass("nAcc_bank")
					sAcc_ledger = .FieldToClass("sAcc_ledger")
					nBank_code = .FieldToClass("nBank_code")
					nBk_agency = .FieldToClass("nBk_agency")
					nAcc_type = .FieldToClass("nAcc_type")
					sAux_accoun = .FieldToClass("sAux_accoun")
					nAvail_type = .FieldToClass("nAvail_type")
					nAvailable = .FieldToClass("nAvailable")
					nCurrency = .FieldToClass("nCurrency")
					dEffecdate = .FieldToClass("dEffecdate")
					nLed_compan = .FieldToClass("nLed_compan")
					nOffice = .FieldToClass("nOffice")
					nTransit_1 = .FieldToClass("nTransit_1")
					nTransit_2 = .FieldToClass("nTransit_2")
					nTransit_3 = .FieldToClass("nTransit_3")
					nTransit_4 = .FieldToClass("nTransit_4")
					nTransit_5 = .FieldToClass("nTransit_5")
					sStatregt = .FieldToClass("sStatregt")
					nCompany = .FieldToClass("nCompany")
					Find_O = True
					.RCloseRec()
				Else
					Find_O = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaBank_acc = Nothing
		End If
		
Find_O_Err: 
		If Err.Number Then
			Find_O = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindCurrency: Devuelve la moneda asociada a la cuenta bancaria
	Public Function FindCurrency(ByVal lngBank_acc As Integer) As Boolean
		'- Se define la variable lrecreaBank_accCurrency
		
		Dim lrecreaBank_accCurrency As eRemoteDB.Execute
		lrecreaBank_accCurrency = New eRemoteDB.Execute
		
		On Error GoTo FindCurrency_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaBank_accCurrency'
		'+ Información leída el 07/02/2001 16:24:52
		
		With lrecreaBank_accCurrency
			.StoredProcedure = "reaBank_accCurrency"
			.Parameters.Add("nBank_acc", lngBank_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCurrency = .FieldToClass("nCurrency")
				sCurrDescript = .FieldToClass("sDescript")
				FindCurrency = True
				.RCloseRec()
			Else
				FindCurrency = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaBank_accCurrency may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBank_accCurrency = Nothing
		
FindCurrency_Err: 
		If Err.Number Then
			FindCurrency = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%insValOPC002: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValOPC002(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nAccount As Double, ByVal optInfType As Integer, ByVal sCheque As String, ByVal nRequest_nu As Double, ByVal nSta_cheque As Integer, ByVal nAmount As Double, ByVal dDat_propos As Date, ByVal dIssue_Dat As Date, ByVal nConcept As Integer, ByVal sClient As String, ByVal sCliename As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsBank_acc As eCashBank.Bank_acc
		
		On Error GoTo insValOPC002_Err
		lclsErrors = New eFunctions.Errors
		lclsBank_acc = New eCashBank.Bank_acc
		
		If nAccount = 0 Or nAccount = eRemoteDB.Constants.intNull Then
			If optInfType = 2 Then
				Call lclsErrors.ErrorMessage(sCodispl, 7002)
			End If
		Else
			If Not lclsBank_acc.Find_O(nAccount) Then
				Call lclsErrors.ErrorMessage(sCodispl, 5043)
			End If
		End If
		
		insValOPC002 = lclsErrors.Confirm
		
insValOPC002_Err: 
		If Err.Number Then
			insValOPC002 = insValOPC002 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		
		On Error GoTo 0
	End Function
	
	'%insPostFolder: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostOPC002() As Boolean
		insPostOPC002 = True
		' Call insStateFolder(False, False)
		' Call insStateGrid(False)
		' Call insPrepaQuery
	End Function
	
	'% Find_v: Raliza la lectura correspondiente  a la tabla de cuentas bancarias,para
	' validar si la agencia enviada como parámetro tiene cuentas asociadas
	Public Function Find_v(ByVal nBank_code As Integer, ByVal nBk_agency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaBank_acc_v
		
		Dim lrecreaBank_acc_v As eRemoteDB.Execute
		
		If Me.nBank_code <> nBank_code Or Me.nBk_agency <> nBk_agency Or lblnFind Then
			
			lrecreaBank_acc_v = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaBank_acc_v'
			'Información leída el 19/09/2001 9:25:21
			
			With lrecreaBank_acc_v
				.StoredProcedure = "reaBank_acc_v"
				
				.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nAcc_bank = .FieldToClass("nAcc_bank")
					Me.sAcc_ledger = .FieldToClass("sAcc_ledger")
					Me.nBank_code = .FieldToClass("nBank_code")
					Me.nBk_agency = .FieldToClass("nBk_agency")
					
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaBank_acc_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaBank_acc_v = Nothing
		End If
		Find_v = lblnRead
	End Function
End Class






