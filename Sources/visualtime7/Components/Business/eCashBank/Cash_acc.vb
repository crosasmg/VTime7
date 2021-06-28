Option Strict Off
Option Explicit On
Public Class Cash_acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_acc.cls                             $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 21/08/03 11:16a                              $%'
	'% $Revision:: 34                                       $%'
	'%-------------------------------------------------------%'
	'Propiedades según la tabla en el sistema
	'Column_name                        Type      Computed  Length  Prec  Scale Nullable  TrimTrailingBlanks
	'--------------------------------- --------- --------- ------- ----- ----- --------- -------------------
	Public nAcc_cash As Integer 'smallint    no     2           5     0     no       (n/a)
	Public nOffice As Integer 'smallint    no     2           5     0     no       (n/a)
	Public nLed_compan As Integer 'smallint    no     2           5     0     yes      (n/a)
	Public sAccount As String 'char        no     20                      no        no
	Public nCurrency As Integer 'smallint    no     2           5     0     no       (n/a)
	Public dEffecdate As Date 'datetime    no     8                       no       (n/a)
	Public sAux_accoun As String 'char        no     20                      no        no
	Public nAvailable As Double 'decimal     no     9           14    2     yes      (n/a)
	Public nUsercode As Integer 'smallint    no     2           5     0     no       (n/a)
	Public sStatregt As String 'char        no     1                       yes       no
	Public nCashNum As Integer 'Smallint    no     2            5    0     no        (n/a)
	Public nMin_Amount As Double 'decimal     no     9           14    2     yes      (n/a)
	
	'+Variables auxiliares
	Public nNewOffice As Integer
	Public nNewCurrency As Integer
	Public nAvailable_By_Day As Double 'decimal     no     9           14    2     yes      (n/a)
	
	Public Function Add() As Boolean
		Dim lreccreCash_acc As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreCash_acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.creCash_acc'
		'Información leída el 21/11/2000 11:17:17 AM
		With lreccreCash_acc
			.StoredProcedure = "creCash_acc"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_Amount", nMin_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCash_acc = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Update() As Boolean
		Dim lrecupdCash_acc As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecupdCash_acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updCash_acc'
		'Información leída el 21/11/2000 11:36:26 AM
		With lrecupdCash_acc
			.StoredProcedure = "updCash_acc"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewOffice", nNewOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewCurrency", nNewCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_amount", nMin_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCash_acc = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Find(ByVal Acc_cash As Integer, ByVal Office As Integer, ByVal lintCurrency As Integer, ByVal lintCashNum As Integer, Optional ByVal nCompany As Integer = 0, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaCash_acc_o As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Acc_cash <> nAcc_cash Or Office <> nOffice Or lintCurrency <> nCurrency Or lblnFind Then
			
			nAcc_cash = Acc_cash
			nOffice = Office
			nCurrency = lintCurrency
			
			lrecreaCash_acc_o = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaCash_acc_o'
			'Información leída el 21/11/2000 1:19:19 PM
			With lrecreaCash_acc_o
				.StoredProcedure = "reaCash_acc_o"
				.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCashNum", lintCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nAcc_cash = .FieldToClass("nAcc_cash")
					nOffice = .FieldToClass("nOffice")
					nLed_compan = .FieldToClass("nLed_compan")
					sAccount = .FieldToClass("sAccount")
					nCurrency = .FieldToClass("nCurrency")
					dEffecdate = .FieldToClass("dEffecdate")
					sAux_accoun = .FieldToClass("sAux_accoun")
					nAvailable = .FieldToClass("nAvailable")
					nAvailable_By_Day = .FieldToClass("nAvailable_By_Day")
					sStatregt = .FieldToClass("sStatregt")
					nCashNum = lintCashNum
					nMin_Amount = .FieldToClass("nMin_Amount")
					
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaCash_acc_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaCash_acc_o = Nothing
		Else
			Find = True
		End If
		
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Delete(ByVal Acc_cash As Integer, ByVal Office As Integer, ByVal lintCurrency As Integer, ByVal lintCashNum As Integer) As Boolean
		
		Dim lrecdelCash_acc As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelCash_acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delCash_acc'
		'Información leída el 21/11/2000 2:03:07 PM
		With lrecdelCash_acc
			.StoredProcedure = "delCash_acc"
			.Parameters.Add("nAcc_cash", Acc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", Office, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", lintCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", lintCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCash_acc = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValOP004: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma OP004 (Actualización de cuentas bancarias y de caja).
	Public Function InsValOP004(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sAccCash As String, ByVal nAccBankCash As Integer, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nAvailable As Double, ByVal nAccType As Integer, ByVal nBank As Integer, ByVal nBk_agency As Integer, ByVal nAvailType As Integer, ByVal sAccNumber As String, ByVal nLedCompan As Integer, ByVal sAccLedger As String, ByVal sAuxAccount As String, ByVal nCashNum As Integer, ByVal nMin_Amount As Double, ByVal nCompany As Integer, Optional ByVal sStatus As String = "") As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclsCash_mov As eCashBank.Cash_mov
		Dim lclsLedge As eLedge.Led_compan
		Dim lclsLedger_acc As eLedge.LedgerAcc
		Dim lclsCash_acc As eCashBank.Cash_acc
		Dim lclsBank_acc As eCashBank.Bank_acc
		Dim lblnValidFIeld As Boolean
		Dim lblnAuxAccount As Boolean
        Dim lstrLed_structure As String = ""

        On Error GoTo InsValOP004_Err
		lerrTime = New eFunctions.Errors
		lvalTime = New eFunctions.valField
		lclsCash_mov = New eCashBank.Cash_mov
		lclsLedge = New eLedge.Led_compan
		lclsLedger_acc = New eLedge.LedgerAcc
		lclsCash_acc = New eCashBank.Cash_acc
		lclsBank_acc = New eCashBank.Bank_acc
		
		InsValOP004 = CStr(True)
		lvalTime.objErr = lerrTime
		
		lblnValidFIeld = False
		'+Validación del campo "Cuenta"
		If nAccBankCash = eRemoteDB.Constants.intNull Then
			'+El campo Cuenta debe estar lleno
			Call lerrTime.ErrorMessage(sCodispl, 7002)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
				If sAccCash <> "1" Then
					If Not lclsBank_acc.Find_O(nAccBankCash, True) Then
						Call lerrTime.ErrorMessage(sCodispl, 7013)
					End If
				Else
					If nCashNum = 0 Or nCashNum = eRemoteDB.Constants.intNull Then
						Call lerrTime.ErrorMessage(sCodispl, 60007)
						lblnValidFIeld = True
					End If
					If nOffice = eRemoteDB.Constants.intNull Or nOffice = 0 Then
						Call lerrTime.ErrorMessage(sCodispl, 1040)
						lblnValidFIeld = True
					End If
					
					'+Validación del campo "Moneda"
					If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = eRemoteDB.Constants.intNull Then
						Call lerrTime.ErrorMessage(sCodispl, 10107)
						lblnValidFIeld = True
					End If
					If Not lblnValidFIeld Then
						If Not lclsCash_acc.Find(nAccBankCash, nOffice, nCurrency, nCashNum, nCompany) Then
							Call lerrTime.ErrorMessage(sCodispl, 7013)
						End If
					End If
					lblnValidFIeld = False
				End If
			End If
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				If nAccBankCash = 9998 Or nAccBankCash = 9999 Or nAccBankCash = 9996 Or nAccBankCash = 9997 Then
					If nOffice <> eRemoteDB.Constants.intNull And nCurrency <> eRemoteDB.Constants.intNull Then
						If lclsCash_acc.Find(nAccBankCash, nOffice, nCurrency, nCashNum, nCompany) Then
							lblnValidFIeld = True
						End If
					End If
				Else
					If lclsBank_acc.Find_O(nAccBankCash, True) Then
						lblnValidFIeld = True
					End If
				End If
				
				If sAccCash = "1" Then
					If nCashNum = 0 Or nCashNum = eRemoteDB.Constants.intNull Then
						Call lerrTime.ErrorMessage(sCodispl, 60007)
					End If
					
					If nAccBankCash = 9998 Then
						If nMin_Amount < 0 Then
							Call lerrTime.ErrorMessage(sCodispl, 60219)
						End If
					End If
				End If
				
				'+ Se valida que el campo compañía no está vacío
				If sAccCash = "" Then
					If nCompany = 0 Then
						Call lerrTime.ErrorMessage(sCodispl, 10203)
					End If
				End If
				'+Si la acción en tratamiento ES registrar, NO debe estar registrado en el sistema
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If lblnValidFIeld Then
						Call lerrTime.ErrorMessage(sCodispl, 7028)
					End If
				Else
					
					'+Si la acción en tratamiento NO es registrar, DEBE estar registrado en el sistema
					If Not lblnValidFIeld Then
						Call lerrTime.ErrorMessage(sCodispl, 7013)
					End If
				End If
				
				lclsCash_mov.nCashNum = nCashNum
				lclsCash_mov.nAcc_cash = nAccBankCash
				lclsCash_mov.nOffice = nOffice
				lclsCash_mov.nCurrency = nCurrency
				If lclsCash_mov.ValCash_mov_Acc And nAction = eFunctions.Menues.TypeActions.clngActioncut Then
					'+Si la cuenta tiene asociado algún movimiento se inhabilitan los campos "moneda","disponible" y "Zona", para que no se puedan modificar
					'+Si la acción es eliminar, no deben existir movimientos asociados a la cuenta
					If lclsCash_mov.nResponse <> 0 Then
						Call lerrTime.ErrorMessage(sCodispl, 7238)
					End If
				End If
			End If
		End If
		
		'+Validación del campo "Fecha"
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			lvalTime.ErrEmpty = 7037
			If lvalTime.ValDate(dEffecdate) Then
				If CDate(dEffecdate) > Today Then
					Call lerrTime.ErrorMessage(sCodispl, 7027)
				End If
			End If
		End If
		
		'+Validación del campo "Tipo"
		If nAccType = eRemoteDB.Constants.intNull Then
            If nAction <> eFunctions.Menues.TypeActions.clngActionQuery And sAccCash = "1" And nAccBankCash <> 9996 Then
                Call lerrTime.ErrorMessage(sCodispl, 7030)
            End If
		Else
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery And nAccBankCash <> 9998 And nAccBankCash <> 9999 And (nAccType = 8 Or nAccType = 9) Then
				Call lerrTime.ErrorMessage(sCodispl, 99030)
			End If
		End If
		
		'+Validación del campo "Zona"
		If nOffice = eRemoteDB.Constants.intNull And (nAccBankCash = 9998 Or nAccBankCash = 9999 Or nAccBankCash = 9996 Or nAccBankCash = 9997) And nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			Call lerrTime.ErrorMessage(sCodispl, 1040)
		End If
		
		'+Validación del campo "Moneda"
		If nCurrency = eRemoteDB.Constants.intNull And (nAccBankCash = 9998 Or nAccBankCash = 9999 Or nAccBankCash = 9996 Or nAccBankCash = 9997) And nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			Call lerrTime.ErrorMessage(sCodispl, 10107)
		End If
		
		'+Validación del campo "Disponible"
		'+Si la cuenta no es de caja,debe indicarse el importe disponible
		If sAccCash = "" Then
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery And nAvailable = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 7167)
			End If
		End If
		
		'+Validación del campo "Bancos-Número de cuenta"
		'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
		If sAccCash = "" Then
			If Trim(sAccNumber) = String.Empty Then
				If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
					Call lerrTime.ErrorMessage(sCodispl, 7029)
				End If
			End If
		End If
		
		'+Validación del campo "Bancos-Banco"
		'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
		If sAccCash = "" Then
			If nBank = eRemoteDB.Constants.intNull And nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				Call lerrTime.ErrorMessage(sCodispl, 7004)
			End If
		End If
		
		'+Validación del campo "Bancos-Agencia"
		
		'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
		If sAccCash = "" Then
			If nBk_agency = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 1080)
			End If
		End If
		
		'+Validación del campo "Bancos-Tiempo de Acreditación"
		'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
		If sAccCash = "" Then
			If nAvailType = eRemoteDB.Constants.intNull And nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				Call lerrTime.ErrorMessage(sCodispl, 7249,  ,  , "- Tiempo de acreditación")
			End If
		End If
		
		'+Validación del campo "Relación con Contabilidad-Compañía Contable"
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
			'        If sAccCash = "" Then
			If nLedCompan = eRemoteDB.Constants.intNull Then
				lstrLed_structure = String.Empty
				If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
					Call lerrTime.ErrorMessage(sCodispl, 36084)
				End If
			Else
				If Not lclsLedge.Find(nLedCompan) Then
					lstrLed_structure = String.Empty
					Call lerrTime.ErrorMessage(sCodispl, 36002)
				Else
					lstrLed_structure = lclsLedge.sStructure
				End If
			End If
			'        End If
		End If
		
		'+Validación del campo "Relación con Contabilidad-Cuenta"
		lblnValidFIeld = True
		'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If sAccCash = "" Then
				If Trim(sAccLedger) = String.Empty Then
					lblnValidFIeld = False
					Call lerrTime.ErrorMessage(sCodispl, 7032)
				ElseIf Not nLedCompan = eRemoteDB.Constants.intNull Then 
					If lstrLed_structure <> String.Empty Then
						If Not valLed_structure(lstrLed_structure, Trim(sAccLedger)) Then
							lblnValidFIeld = False
							'+Si el campo esta lleno, debe coincidir con la estructura del código contable definida en el sistema para
							'+la compañía contable
							Call lerrTime.ErrorMessage(sCodispl, 36019)
						Else
							If Not lclsLedger_acc.Find_Account_o(nLedCompan, sAccLedger) Then
								'+Si el campo está lleno, debe estar registrado en el catálogo de cuentas contables
								lblnValidFIeld = False
								Call lerrTime.ErrorMessage(sCodispl, 7033)
							Else
								If CDbl(lclsLedger_acc.sBlock_cre) <> 2 Or CDbl(lclsLedger_acc.sBlock_deb) <> 2 Then
									lblnValidFIeld = False
									Call lerrTime.ErrorMessage(sCodispl, 7035)
								End If
								'+El campo "Cuenta Auxiliar" se habilita si la cuenta contable tiene asociada algun auxiliar
								If lclsLedger_acc.Find_AuxAccount(nLedCompan, sAccLedger, strNull) Then
									If lclsLedger_acc.nAuxCount <> 0 Then
										lblnAuxAccount = True
									Else
										lblnAuxAccount = False
										sAuxAccount = String.Empty
									End If
								Else
									lblnValidFIeld = False
								End If
								If lclsLedger_acc.valLedger_acc_Lastlevel(nLedCompan, sAccLedger, "1", 0) Then
									If lclsLedger_acc.nResponse <> 1 Then
										'+La cuenta contable debe corresponder a una cuenta de último nivel
										lblnValidFIeld = False
										Call lerrTime.ErrorMessage(sCodispl, 7129)
									End If
								End If
							End If
						End If
					End If
				End If
			End If
			
			'+Validación del campo "Relación con Contabilidad-Auxiliar"
			If sAccCash = "" Then
				If (sAuxAccount) = String.Empty Then
					'+Si la cuenta no corresponde a una cuenta de caja, este campo debe estar lleno
					If lblnValidFIeld And lblnAuxAccount Then
						Call lerrTime.ErrorMessage(sCodispl, 36119)
					End If
				ElseIf Not nLedCompan = eRemoteDB.Constants.intNull Then 
					If Not lclsLedger_acc.Find_AuxAccount(nLedCompan, sAccLedger, sAuxAccount) Then
						'+La combinación "Cuenta Contable-Auxiliar" debe existir en el archivo de cuentas contables
						Call lerrTime.ErrorMessage(sCodispl, 36021)
					Else
						If CDbl(lclsLedger_acc.sBlock_cre) <> 2 Or CDbl(lclsLedger_acc.sBlock_deb) <> 2 Then
							'+La combinación "Cuenta Contable-Auxiliar" no puede tener los débitos o créditos bloqueados
							Call lerrTime.ErrorMessage(sCodispl, 7253)
						End If
					End If
				End If
			End If
		End If
		
		'+ Si el tipo de cuenta corresponde a cuentas de caja, se busca la data correspondiente a la cuenta
		'+ en tratamiento - ACM - 10/07/2002
		If nAccBankCash = 9996 Or nAccBankCash = 9997 Or nAccBankCash = 9998 Or nAccBankCash = 9999 Then
			If nOffice <> eRemoteDB.Constants.intNull And nCurrency <> eRemoteDB.Constants.intNull Then
				Call Me.Find(nAccBankCash, nOffice, nCurrency, nCashNum, nCompany)
			End If
		Else
			'+ Si el tipo de cuenta NO corresponde a cuentas de caja, se busca la data correspondiente a la cuenta
			'+ en tratamiento y se asigna a la variable sStatregt de la clase el valor de la variable del mismo
			'+ nombre de la clase "Bank_acc" - ACM - 10/07/2002
			If lclsBank_acc.Find_O(nAccBankCash, True) Then
				Me.sStatregt = lclsBank_acc.sStatregt
			End If
		End If
		'+ Si la variable "sStatregt" es diferente de nulo y diferente de 3 y el parámetro de entrada
		'+ "sStatus" es igual a 3, entonces se envía el mensaje de error respectivo - ACM - 10/07/2002
		If Me.sStatregt <> String.Empty And Me.sStatregt <> "3" And sStatus = "2" Then
			Call lerrTime.ErrorMessage("OP004", 11218)
		End If
		InsValOP004 = lerrTime.Confirm
		
InsValOP004_Err: 
		If Err.Number Then
			InsValOP004 = "InsValOP004: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lclsLedge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedge = Nothing
		'UPGRADE_NOTE: Object lclsLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedger_acc = Nothing
		'UPGRADE_NOTE: Object lclsCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_acc = Nothing
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
	End Function
	
	'%valLed_structure: Esta función valida si una cuenta contable corresponde con una estructura
	'%de código contable
	Private Function valLed_structure(ByRef lstrLed_structure As String, ByRef lstrAccLedger As String) As Boolean
		Dim lintStartPlace As Integer
		Dim lintStructureIndex As Integer
		Dim lintAccPosition As Integer
		Dim lintPlacesCount As Integer
		Dim lstrStrucValue As String
		
		lintStartPlace = 1
		lintStructureIndex = 1
		valLed_structure = True
		lintAccPosition = 1
		lstrStrucValue = "VALOR INICIAL"
		Do While lintStartPlace <= Len(lstrAccLedger) And lintAccPosition > 0 And lintStructureIndex <= Len(lstrLed_structure) And Trim(lstrStrucValue) <> String.Empty And valLed_structure
			lintAccPosition = InStr(lintStartPlace, lstrAccLedger, "-", CompareMethod.Binary)
			If lintAccPosition > 0 Then
				lintPlacesCount = lintAccPosition - lintStartPlace
				lintStartPlace = lintAccPosition + 1
				lstrStrucValue = Mid(lstrLed_structure, lintStructureIndex, 1)
				lintStructureIndex = lintStructureIndex + 1
				If Trim(lstrStrucValue) <> String.Empty Then
					If CShort(lstrStrucValue) <> lintPlacesCount Then
						valLed_structure = False
					End If
				End If
			Else
				lintPlacesCount = Len(Mid(lstrAccLedger, lintStartPlace))
				lstrStrucValue = Mid(lstrLed_structure, lintStructureIndex, 1)
				If Trim(lstrStrucValue) <> String.Empty Then
					If CShort(lstrStrucValue) <> lintPlacesCount Then
						valLed_structure = False
					End If
				End If
			End If
		Loop 
	End Function
	
	'%insPostOP004: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostOP004(ByVal nAction As Integer, ByVal nAccBankCash As Integer, ByVal nAccType As Integer, ByVal dEffecdate As Date, ByVal nAvailable As Double, ByVal sStatregt As String, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer, Optional ByVal nOldCurrency As Integer = 0, Optional ByVal nOldOffice As Integer = 0, Optional ByVal sAccNumber As String = "", Optional ByVal nBank As Integer = 0, Optional ByVal nBk_agency As Integer = 0, Optional ByVal nAvail_type As Integer = 0, Optional ByVal nTransit1 As Double = 0, Optional ByVal nTransit2 As Double = 0, Optional ByVal nTransit3 As Double = 0, Optional ByVal nTransit4 As Double = 0, Optional ByVal nTransit5 As Double = 0, Optional ByVal nLed_compan As Integer = 0, Optional ByVal sAcc_ledger As String = "", Optional ByVal sAux_accoun As String = "", Optional ByVal nCashNum As Integer = 0, Optional ByVal nMin_Amount As Double = 0, Optional ByVal nCompany As Integer = 0) As Boolean
		Dim lclsCash_acc As eCashBank.Cash_acc
		Dim lclsBank_acc As eCashBank.Bank_acc
		
		lclsCash_acc = New eCashBank.Cash_acc
		lclsBank_acc = New eCashBank.Bank_acc
		
		On Error GoTo insPostOP004_Err
		
		insPostOP004 = True
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			If nAccBankCash = 9996 Or nAccBankCash = 9997 Or nAccBankCash = 9998 Or nAccBankCash = 9999 Then
				With lclsCash_acc
					.nUsercode = nUsercode
					.nAcc_cash = nAccBankCash
					.dEffecdate = dEffecdate
					.nAvailable = nAvailable
					.nLed_compan = eRemoteDB.Constants.intNull
					.sAccount = "  "
					.sAux_accoun = "  "
					.nMin_Amount = nMin_Amount
					.nCashNum = nCashNum
					
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						.sStatregt = "1"
					Else
						.sStatregt = sStatregt
					End If
					
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						.nOffice = nOffice
						.nCurrency = nCurrency
					Else
						'+Si el usuario modifió la clave de la cuenta de caja
						If (nOldOffice <> 0 And nOldOffice <> eRemoteDB.Constants.intNull And nOffice <> nOldOffice) Or (nOldCurrency <> 0 And nOldCurrency <> eRemoteDB.Constants.intNull And nCurrency <> nOldCurrency) Then
							.nOffice = nOldOffice
							.nCurrency = nOldCurrency
							.nNewOffice = nOffice
							.nNewCurrency = nCurrency
						Else
							.nOffice = nOffice
							.nCurrency = nCurrency
							.nNewOffice = 0
							.nNewCurrency = 0
						End If
					End If
					
					.nCashNum = nCashNum
					
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						If Not .Add Then
							insPostOP004 = False
						End If
					Else
						If Not .Update Then
							insPostOP004 = False
						End If
					End If
				End With
			Else
				With lclsBank_acc
					.nUsercode = nUsercode
					.nAcc_bank = nAccBankCash
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						.sStatregt = "1"
					Else
						.sStatregt = sStatregt
					End If
					.dEffecdate = dEffecdate
					.nAcc_type = nAccType
					.nOffice = nOffice
					.nCurrency = nCurrency
					.nAvailable = nAvailable
					.sAcc_number = sAccNumber
					.nBank_code = nBank
					.nBk_agency = nBk_agency
					.nAvail_type = nAvail_type
					.nTransit_1 = nTransit1
					.nTransit_2 = nTransit2
					.nTransit_3 = nTransit3
					.nTransit_4 = nTransit4
					.nTransit_5 = nTransit5
					.nLed_compan = nLed_compan
					.sAcc_ledger = sAcc_ledger
					.sAux_accoun = sAux_accoun
					.nCompany = nCompany
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						If Not .Add Then
							insPostOP004 = False
						End If
					Else
						If Not .Update Then
							insPostOP004 = False
						End If
					End If
				End With
			End If
			
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActioncut Then
				If nAccBankCash = 9996 Or nAccBankCash = 9997 Or nAccBankCash = 9998 Or nAccBankCash = 9999 Then
					insPostOP004 = lclsCash_acc.Delete(nAccBankCash, nOffice, nCurrency, nCashNum)
				Else
					insPostOP004 = lclsBank_acc.Delete(nAccBankCash)
				End If
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_acc = Nothing
		'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_acc = Nothing
		
insPostOP004_Err: 
		If Err.Number Then
			insPostOP004 = False
		End If
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAcc_cash = eRemoteDB.Constants.intNull
		nOffice = eRemoteDB.Constants.intNull
		nLed_compan = eRemoteDB.Constants.intNull
		sAccount = strNull
		nCurrency = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		sAux_accoun = strNull
		nAvailable = dblNull
		nUsercode = eRemoteDB.Constants.intNull
		sStatregt = strNull
		nCashNum = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'+ funcion que retorna si existe una la cuenta corriente para la cual se
	'+ desea crear un movimiento
	Public Function Find_move(ByVal nCurrency As Integer, ByVal nCashNum As Integer, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecreaCash_acc_1 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCash_acc_1 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaCash_acc_1'
		'Información leída el 21/11/2000 1:19:19 PM
		With lrecreaCash_acc_1
			.StoredProcedure = "reaCash_acc_1"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nAcc_cash = .FieldToClass("nAcc_cash")
				nOffice = .FieldToClass("nOffice")
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				nCurrency = .FieldToClass("nCurrency")
				dEffecdate = .FieldToClass("dEffecdate")
				sAux_accoun = .FieldToClass("sAux_accoun")
				nAvailable = .FieldToClass("nAvailable")
				sStatregt = .FieldToClass("sStatregt")
				nCashNum = nCashNum
				nMin_Amount = .FieldToClass("nMin_Amount")
				
				Find_move = True
				.RCloseRec()
			Else
				Find_move = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCash_acc_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_acc_1 = Nothing
		
		
Find_Err: 
		If Err.Number Then
			Find_move = False
		End If
		On Error GoTo 0
	End Function
	
	'+ funcion que retorna si existe una la cuenta corriente para la cual se
	'+ desea crear un movimiento
	Public Function Find_move_cash(ByVal nCurrency As Integer, ByVal nCashNum As Integer, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecreaCash_acc_1 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCash_acc_1 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaCash_acc_1'
		'Información leída el 21/11/2000 1:19:19 PM
		With lrecreaCash_acc_1
			.StoredProcedure = "reaCash_acc_2"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nAcc_cash = .FieldToClass("nAcc_cash")
				nOffice = .FieldToClass("nOffice")
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				nCurrency = .FieldToClass("nCurrency")
				dEffecdate = .FieldToClass("dEffecdate")
				sAux_accoun = .FieldToClass("sAux_accoun")
				nAvailable = .FieldToClass("nAvailable")
				sStatregt = .FieldToClass("sStatregt")
				nCashNum = nCashNum
				nMin_Amount = .FieldToClass("nMin_Amount")
				
				Find_move_cash = True
				.RCloseRec()
			Else
				Find_move_cash = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCash_acc_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_acc_1 = Nothing
		
		
Find_Err: 
		If Err.Number Then
			Find_move_cash = False
		End If
		On Error GoTo 0
	End Function
End Class






