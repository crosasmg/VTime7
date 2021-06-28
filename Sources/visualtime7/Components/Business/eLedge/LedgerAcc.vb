Option Strict Off
Option Explicit On
Public Class LedgerAcc
	'%-------------------------------------------------------%'
	'% $Workfile:: LedgerAcc.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 28/12/1999.
	'+ Los campos llaves corresponden a nLed_compan, sAccount y sAux_accoun
	
	'Column_name                      Type                    Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-------------------------------- ----------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nLed_compan As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public sAccount As String 'char     no       20                      no       yes                no
	Public sAux_accoun As String 'char     no       20                      no       yes                no
	Public sAdju_exci As String 'char     no       1                       yes      yes                yes
	Public nAux_create As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public nBalance As Double 'decimal  no       9           12    2     yes      (n/a)              (n/a)
	Public sBlock_cre As String 'char     no       1                       yes      yes                yes
	Public sBlock_deb As String 'char     no       1                       yes      yes                yes
	Public sBudget_ind As String 'char     no       1                       yes      yes                yes
	Public sDescript As String 'char     no       50                      yes      yes                yes
	Public sOrgan_unit As String 'char     no       1                       yes      yes                yes
	Public sStatregt As String 'char     no       1                       yes      yes                yes
	Public nTotal_cre As Double 'decimal  no       9           12    2     yes      (n/a)              (n/a)
	Public nTotal_deb As Double 'decimal  no       9           12    2     yes      (n/a)              (n/a)
	Public sType_acc As String 'char     no       1                       yes      yes                yes
	Public nUsercode As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	
	'- Variable auxiliares
	'- Se define el tipo enumerado  para indicar el Tipo de Cuenta auxiliar a crear
	
	Public Enum eTypeAux
		eBanco = 1
		eCo_Reaseguradores = 2
		eDepartamento = 3
		eIntermediario = 4
		eMoneda = 5
		eRamo = 6
		eRamos_contables = 7
		eSucursal = 8
		eMonedaSucursal = 11
		eMonedaSucursalRamo = 12
		eCo_ReaseguradorMoneda = 13
		eMonedaCo_Reasegurador = 14
		eRamoSucursalMoneda = 15
		eRamoMoneda = 16
		eCo_ReaseguradoresRamoMoneda = 17
		SucursalRamo = 18
	End Enum
	
	Private nTypeAux As eTypeAux
	
	Public nAuxCount As Integer
	Public nResponse As Integer
	
	'- Se definen las variables para el funcionamiento del método Full_LevelCatalog
	
	Public dLastDate As Date
	Public nAmount As Double
	Private lintLed_CompanAux As Integer
	Private lstrAccountAux As String
	Private lstrAux_accounAux As String
	Private mclsLed_compan As Led_compan
	Public nLevel As Integer
	Public nLast_level As Integer
	
	'% Find_Account_o: Permite buscar los datos de una determinada cuenta contable
	Public Function Find_Account_o(ByVal nLed_compan As Integer, ByVal sAccount As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLedger_acc_sAccount_o As eRemoteDB.Execute
		
		lrecreaLedger_acc_sAccount_o = New eRemoteDB.Execute
		On Error GoTo Find_Account_o_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_AccAccount'
		'+ Información leída el 21/11/2000 10:30:25
		With lrecreaLedger_acc_sAccount_o
			.StoredProcedure = "reaLedger_acc_sAccount_o"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sDescript = .FieldToClass("sDescript")
				sBlock_cre = .FieldToClass("sBlock_cre")
				sBlock_deb = .FieldToClass("sBlock_deb")
				sAccount = .FieldToClass("sAccount")
				Find_Account_o = True
				.RCloseRec()
			Else
				Find_Account_o = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLedger_acc_sAccount_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_acc_sAccount_o = Nothing
		
Find_Account_o_Err: 
		If Err.Number Then
			Find_Account_o = False
		End If
		On Error GoTo 0
		
	End Function
	'% Find_Account: Permite buscar los datos de una determinada cuenta contable
	Public Function Find_Account(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaLedger_AccAccount As eRemoteDB.Execute
		Dim lintLed_CompanAux As Integer
        Dim lstrAccountAux As String = ""
        lrecreaLedger_AccAccount = New eRemoteDB.Execute
		
		On Error GoTo Find_Account_Err
		
		lstrAccount = Trim(lstrAccount)
		
		If lintLed_CompanAux <> lintLed_Compan Or lstrAccountAux <> lstrAccount Or lblnFind Then
			lintLed_CompanAux = lintLed_Compan
			lstrAccountAux = lstrAccount
			
			nLed_compan = lintLed_Compan
			sAccount = lstrAccount
			
			'+ Definición de parámetros para stored procedure 'insudb.reaLedger_AccAccount'
			'+ Información leída el 06/06/2001 10:22:31
			
			With lrecreaLedger_AccAccount
				.StoredProcedure = "reaLedger_AccAccount"
				.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nLed_compan = .FieldToClass("nLed_compan")
					sAccount = .FieldToClass("sAccount")
					sAux_accoun = .FieldToClass("sAux_accoun")
					sAdju_exci = .FieldToClass("sAdju_exci")
					nAux_create = .FieldToClass("nAux_create")
					
					If .FieldToClass("nBalance") <> eRemoteDB.Constants.intNull Then
						nBalance = .FieldToClass("nBalance")
					Else
						nBalance = 0
					End If
					
					sBlock_cre = .FieldToClass("sBlock_cre")
					sBlock_deb = .FieldToClass("sBlock_deb")
					sBudget_ind = .FieldToClass("sBudget_ind")
					sDescript = .FieldToClass("sDescript")
					sOrgan_unit = .FieldToClass("sOrgan_unit")
					sStatregt = .FieldToClass("sStatregt")
					
					If .FieldToClass("nTotal_cre") <> eRemoteDB.Constants.intNull Then
						nTotal_cre = .FieldToClass("nTotal_cre")
					Else
						nTotal_cre = 0
					End If
					
					If .FieldToClass("nTotal_deb") <> eRemoteDB.Constants.intNull Then
						nTotal_deb = .FieldToClass("nTotal_deb")
					Else
						nTotal_deb = 0
					End If
					
					sType_acc = .FieldToClass("sType_acc")
					.RCloseRec()
					Find_Account = True
				Else
					Find_Account = False
					lintLed_CompanAux = 0
				End If
			End With
		Else
			Find_Account = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaLedger_AccAccount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_AccAccount = Nothing
		
Find_Account_Err: 
		If Err.Number Then
			Find_Account = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Find_AuxAccount: Permite buscar los datos del auxiliar de una cuenta contable
	Public Function Find_AuxAccount(ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sAux_accoun As String) As Boolean
		Dim lrecreaLedger_acc_sAuxAccount As eRemoteDB.Execute
		lrecreaLedger_acc_sAuxAccount = New eRemoteDB.Execute
		
		On Error GoTo Find_AuxAccount_Err
		'Definición de parámetros para stored procedure 'insudb.reaLedger_acc_sAuxAccount'
		'Información leída el 21/11/2000 10:39:43 AM
		
		With lrecreaLedger_acc_sAuxAccount
			.StoredProcedure = "reaLedger_acc_sAuxAccount"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If sAux_accoun <> String.Empty Then
					sDescript = .FieldToClass("sDescript")
					sBlock_cre = .FieldToClass("sBlock_cre")
					sBlock_deb = .FieldToClass("sBlock_deb")
				Else
					nAuxCount = .FieldToClass("nAuxCount")
				End If
				Find_AuxAccount = True
				.RCloseRec()
			Else
				Find_AuxAccount = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLedger_acc_sAuxAccount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_acc_sAuxAccount = Nothing
		
Find_AuxAccount_Err: 
		If Err.Number Then
			Find_AuxAccount = False
		End If
		On Error GoTo 0
		
	End Function
	
	Public Function valLedger_acc_Lastlevel(ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sRunType As String, ByVal nResponse As Integer) As Boolean
		
		Dim lrecvalLedger_acc_Lastlevel As eRemoteDB.Execute
		
		On Error GoTo valLedger_acc_Lastlevel_Err
		
		lrecvalLedger_acc_Lastlevel = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.valLedger_acc_Lastlevel'
		'Información leída el 21/11/2000 10:56:52 AM
		
		With lrecvalLedger_acc_Lastlevel
			.StoredProcedure = "valLedger_acc_Lastlevel"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRunType", sRunType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResponse", nResponse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nResponse = .FieldToClass("nResponse")
				valLedger_acc_Lastlevel = True
				.RCloseRec()
			Else
				valLedger_acc_Lastlevel = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalLedger_acc_Lastlevel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalLedger_acc_Lastlevel = Nothing
		
valLedger_acc_Lastlevel_Err: 
		If Err.Number Then
			valLedger_acc_Lastlevel = False
		End If
		On Error GoTo 0
	End Function
	
	'%ValCompany: Permite verificar si existe información en la tabla Ledger_acc para la compañia contable.
	Public Function ValCompany(ByVal lintLed_Compan As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLedger_accCompanyActive As eRemoteDB.Execute
		
		lrecreaLedger_accCompanyActive = New eRemoteDB.Execute
		
		On Error GoTo ValCompany_err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_accCompanyActive'
		'+ Información leída el 23/05/2001 03:40:17 p.m.
		
		ValCompany = True
		
		With lrecreaLedger_accCompanyActive
			.StoredProcedure = "reaLedger_accCompanyActive"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				ValCompany = False
			Else
				ValCompany = True
			End If
			
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_accCompanyActive may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accCompanyActive = Nothing
		
ValCompany_err: 
		If Err.Number Then
			ValCompany = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Find_Active: Permite buscar registros en la tabla de Cuentas Contables
	Public Function Find_Active(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String, ByVal lstrAux_accoun As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLedger_acc As eRemoteDB.Execute
		
		lrecreaLedger_acc = New eRemoteDB.Execute
		
		On Error GoTo Find_Active_err
		
		lstrAccount = Trim(lstrAccount)
		lstrAux_accoun = Trim(lstrAux_accoun)
		
		If lstrAux_accoun = String.Empty Then
			lstrAux_accoun = "                    "
		End If
		
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_acc'
		'+ Información leída el 23/05/2001 03:51:49 p.m.
		
		With lrecreaLedger_acc
			.StoredProcedure = "reaLedger_acc"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", lstrAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				sAux_accoun = .FieldToClass("sAux_accoun")
				sAdju_exci = .FieldToClass("sAdju_exci")
				nAux_create = .FieldToClass("nAux_create")
				If .FieldToClass("nBalance") <> eRemoteDB.Constants.intNull Then
					nBalance = .FieldToClass("nBalance")
				Else
					nBalance = 0
				End If
				sBlock_cre = .FieldToClass("sBlock_cre")
				sBlock_deb = .FieldToClass("sBlock_deb")
				sBudget_ind = .FieldToClass("sBudget_ind")
				sDescript = .FieldToClass("sDescript")
				sOrgan_unit = .FieldToClass("sOrgan_unit")
				sStatregt = .FieldToClass("sStatregt")
				If .FieldToClass("nTotal_cre") <> eRemoteDB.Constants.intNull Then
					nTotal_cre = .FieldToClass("nTotal_cre")
				Else
					nTotal_cre = 0
				End If
				If .FieldToClass("nTotal_deb") <> eRemoteDB.Constants.intNull Then
					nTotal_deb = .FieldToClass("nTotal_deb")
				Else
					nTotal_deb = 0
				End If
				sType_acc = .FieldToClass("sType_acc")
				.RCloseRec()
				Find_Active = True
			Else
				Find_Active = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_acc = Nothing
		
Find_Active_err: 
		If Err.Number Then
			Find_Active = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'%Val_Structure_Down: Permite verificar si la cuenta contable tiene niveles inferiores.
	Public Function Val_Structure_Down(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim lrecreaLedger_accDown As eRemoteDB.Execute
		
		lrecreaLedger_accDown = New eRemoteDB.Execute
		
		Val_Structure_Down = False
		
		lstrAccount = Trim(lstrAccount) & "-"
		
		If Len(lstrAccount) <= 19 Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reaLedger_accDown'
			'+ Información leída el 24/05/2001 02:29:01 a.m.
			
			With lrecreaLedger_accDown
				.StoredProcedure = "reaLedger_accDown"
				.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Val_Structure_Down = .Run
				.RCloseRec()
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaLedger_accDown may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accDown = Nothing
	End Function
	
	'% Full_LevelCatalog: Devuelve el Catálogo de cuentas de una compañía contable
	Public Function Full_LevelCatalog(ByVal lintLed_Compan As Integer, ByVal lintlevelQuant As Integer, ByVal lintTypeQuery As Integer, ByVal ldtmEffecdate As Date, Optional ByVal lintFirstRecord As Integer = 0, Optional ByVal lintLastRecord As Integer = 0) As Collection
		
		Dim lrec_LedgerAcc As eRemoteDB.Execute
		Dim lclsLedgerAcc As LedgerAcc
		
		Dim lintRecordsAdd As Integer
		Dim lintTotalRecords As Integer
		
		lrec_LedgerAcc = New eRemoteDB.Execute
		lclsLedgerAcc = New LedgerAcc
		Full_LevelCatalog = New Collection
		
		lintRecordsAdd = 0
		
		lintTotalRecords = 0
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_acc_levelCatalog'
		'+ Información leída el 24/05/2000 08:38:58 am
		
		With lrec_LedgerAcc
			.StoredProcedure = "reaLedger_acc_levelCatalog"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevelQuant", lintlevelQuant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeQuery", lintTypeQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				
				Do While Not .EOF
					lintTotalRecords = lintTotalRecords + 1
					
					'+ Determina si se especificó o no un rango con el objeto de
					'+ cargar el rango o todos los componentes.
					If (lintFirstRecord > 0) And (lintLastRecord > 0) And (lintFirstRecord <= lintLastRecord) Then
						
						'+ Carga a la colección sólo los datos que corresponden a un rango.
						If (lintTotalRecords >= lintFirstRecord) And (lintTotalRecords <= lintLastRecord) Then
							
							lclsLedgerAcc = New LedgerAcc
							
							lclsLedgerAcc.sAccount = .FieldToClass("sAccount")
							lclsLedgerAcc.sAux_accoun = .FieldToClass("sAux_accoun")
							lclsLedgerAcc.sDescript = .FieldToClass("sDescript")
							
							If .FieldToClass("nAmount") <> eRemoteDB.Constants.intNull Then
								lclsLedgerAcc.nAmount = .FieldToClass("nAmount")
							Else
								lclsLedgerAcc.nAmount = 0
							End If
							
							If .FieldToClass("dLastDate") <> eRemoteDB.Constants.dtmNull Then
								lclsLedgerAcc.dLastDate = .FieldToClass("dLastDate")
							Else
								lclsLedgerAcc.dLastDate = eRemoteDB.Constants.dtmNull
							End If
							
							Full_LevelCatalog.Add(lclsLedgerAcc)
							
							'UPGRADE_NOTE: Object lclsLedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsLedgerAcc = Nothing
							
							lintRecordsAdd = lintRecordsAdd + 1
							
							'+ Se termina el ciclo cuando se alcanza el tope.
							If lintTotalRecords >= lintLastRecord Then Exit Do
						End If
					Else
						lclsLedgerAcc = New LedgerAcc
						
						lclsLedgerAcc.sAccount = .FieldToClass("sAccount")
						lclsLedgerAcc.sAux_accoun = .FieldToClass("sAux_accoun")
						lclsLedgerAcc.sDescript = .FieldToClass("sDescript")
						
						If .FieldToClass("nAmount") <> eRemoteDB.Constants.intNull Then
							lclsLedgerAcc.nAmount = .FieldToClass("nAmount")
						Else
							lclsLedgerAcc.nAmount = 0
						End If
						
						If .FieldToClass("dLastDate") <> eRemoteDB.Constants.dtmNull Then
							lclsLedgerAcc.dLastDate = .FieldToClass("dLastDate")
						Else
							lclsLedgerAcc.dLastDate = eRemoteDB.Constants.dtmNull
						End If
						
						Full_LevelCatalog.Add(lclsLedgerAcc)
						
						'UPGRADE_NOTE: Object lclsLedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsLedgerAcc = Nothing
						
						lintRecordsAdd = lintRecordsAdd + 1
					End If
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrec_LedgerAcc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_LedgerAcc = Nothing
	End Function
	
	
	
	Public Function Find_AccountActive(ByVal nLed_compan As Integer, ByVal sAccount As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaLedger_AccAccountActive As eRemoteDB.Execute
		
		'Static lblnRead As Boolean
		'Static lintLed_CompanAux As long
		'Static lstrAccountAux As String
		
		lrecreaLedger_AccAccountActive = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_AccAccountActive'
		'+ Información leída el 02/10/2000 08:35:50 a.m.
		
		With lrecreaLedger_AccAccountActive
			.StoredProcedure = "reaLedger_AccAccountActive"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				sAux_accoun = .FieldToClass("sAux_accoun")
				sAdju_exci = .FieldToClass("sAdju_exci")
				nAux_create = .FieldToClass("nAux_create")
				
				If .FieldToClass("nBalance") <> eRemoteDB.Constants.intNull Then
					nBalance = .FieldToClass("nBalance")
				Else
					nBalance = 0
				End If
				
				sBlock_cre = .FieldToClass("sBlock_cre")
				sBlock_deb = .FieldToClass("sBlock_deb")
				sBudget_ind = .FieldToClass("sBudget_ind")
				sDescript = .FieldToClass("sDescript")
				sOrgan_unit = .FieldToClass("sOrgan_unit")
				sStatregt = .FieldToClass("sStatregt")
				
				If .FieldToClass("nTotal_cre") <> eRemoteDB.Constants.intNull Then
					nTotal_cre = .FieldToClass("nTotal_cre")
				Else
					nTotal_cre = 0
				End If
				
				If .FieldToClass("nTotal_deb") <> eRemoteDB.Constants.intNull Then
					nTotal_deb = .FieldToClass("nTotal_deb")
				Else
					nTotal_deb = 0
				End If
				
				sType_acc = .FieldToClass("sType_acc")
				Find_AccountActive = True
				.RCloseRec()
			Else
				lintLed_CompanAux = 0
				Find_AccountActive = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_AccAccountActive may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_AccAccountActive = Nothing
	End Function
	
	'% UpdateBalance: Permite actualizar los saldos de las cuentas.
	Public Function UpdateBalance(ByVal lintLed_Compan As Integer, ByVal ldateEffecdate As Date, ByVal lstrAccount As String, ByVal lstrAux_accoun As String, ByVal lstrCost_cente As String, ByVal ldblDebit As Double, ByVal ldblCredit As Double, ByVal lintYear As Integer, ByVal ldateIniLedDat As Date, ByVal lstrPreliminar As String, ByVal ldateDate_init As Date) As Boolean
		Dim lrecinsUpdBalance As eRemoteDB.Execute
		
		lrecinsUpdBalance = New eRemoteDB.Execute
		
		On Error GoTo UpdateBalance_err
		With lrecinsUpdBalance
			.StoredProcedure = "insUpdBalance"
			
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCloseDate", ldateEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", lstrAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", lstrCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", ldblDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", ldblCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_year", lintYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIniLedDat", ldateIniLedDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreliminar", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitDate", ldateDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				UpdateBalance = True
			Else
				UpdateBalance = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsUpdBalance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdBalance = Nothing
		
UpdateBalance_err: 
		If Err.Number Then
			UpdateBalance = False
		End If
		On Error GoTo 0
	End Function
	
	
	Public Function FullChargePrevLevel(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String, Optional ByVal lblnFind As Boolean = False) As Collection
		
		Dim lrecinsLedger_accChargePrevLevel As eRemoteDB.Execute
		Dim lclsLedger_Acc As LedgerAcc
		
		lstrAccount = Trim(lstrAccount)
		
		lclsLedger_Acc = New LedgerAcc
		lrecinsLedger_accChargePrevLevel = New eRemoteDB.Execute
		FullChargePrevLevel = New Collection
		
		'+ Definición de parámetros para stored procedure 'insudb.insLedger_accChargePrevLevel'
		'+ Información leída el 06/06/2001 09:53:46 a.m.
		
		With lrecinsLedger_accChargePrevLevel
			.StoredProcedure = "insLedger_accChargePrevLevel"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lclsLedger_Acc = New LedgerAcc
					lclsLedger_Acc.sAccount = .FieldToClass("sAccount")
					lclsLedger_Acc.sDescript = .FieldToClass("sDescript")
					FullChargePrevLevel.Add(lclsLedger_Acc)
					'UPGRADE_NOTE: Object lclsLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsLedger_Acc = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsLedger_accChargePrevLevel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsLedger_accChargePrevLevel = Nothing
	End Function
	
	'% ValAccountPreviousType: Permite verificar si la cuenta contable tiene niveles previos y obtener su tipo
	Public Function ValAccountPreviousType(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As String
		Dim llngLength As Integer
		Dim llngCount As Integer
		
		ValAccountPreviousType = " "
		
		lstrAccount = Trim(lstrAccount)
		llngLength = Len(lstrAccount)
		
		For llngCount = llngLength To 1 Step -1
			If Mid(lstrAccount, llngCount, 1) <> "-" Then
				Mid(lstrAccount, llngCount, 1) = " "
			Else
				Mid(lstrAccount, llngCount, 1) = " "
				
				Exit For
			End If
		Next llngCount
		
		If Trim(lstrAccount) <> "" Then
			If Find_Account(lintLed_Compan, lstrAccount) Then
				ValAccountPreviousType = sType_acc
			End If
		End If
	End Function
	
	'%ValAccountPreviousBudget: Permite verificar si existen niveles anteriores con
	'%presupuestos.
	Public Function ValAccountPreviousBudget(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		Dim lrecinsLedger_accPrevBudget_ind As eRemoteDB.Execute
		
		ValAccountPreviousBudget = False
		
		lrecinsLedger_accPrevBudget_ind = New eRemoteDB.Execute
		
		On Error GoTo ValAccountPreviousBudget_err
		lstrAccount = Trim(lstrAccount)
		
		ValAccountPreviousBudget = True
		
		'+ Definición de parámetros para stored procedure 'insudb.insLedger_accPrevBudget_ind'
		'+ Información leída el 06/06/2001 11:15:20 p.m.
		
		With lrecinsLedger_accPrevBudget_ind
			.StoredProcedure = "insLedger_accPrevBudget_ind"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("sBudget_ind") = "2" Then
					ValAccountPreviousBudget = False
				End If
			Else
				ValAccountPreviousBudget = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsLedger_accPrevBudget_ind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsLedger_accPrevBudget_ind = Nothing
		
ValAccountPreviousBudget_err: 
		If Err.Number Then
			ValAccountPreviousBudget = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% ValAccountAuxBudget: Permite verificar si existen otro auxiliar para la misma cuenta.
	Public Function ValAccountAuxBudget(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		Dim lrecreaLedger_accAnotherAuxBudget As eRemoteDB.Execute
		
		lrecreaLedger_accAnotherAuxBudget = New eRemoteDB.Execute
		On Error GoTo ValAccountAuxBudget_Err
		lstrAccount = Trim(lstrAccount)
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_accAnotherAuxBudget'
		'+ Información leída el 06/06/2001 11:55:01 a.m.
		
		With lrecreaLedger_accAnotherAuxBudget
			.StoredProcedure = "reaLedger_accAnotherAuxBudget"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValAccountAuxBudget = True
			Else
				ValAccountAuxBudget = False
			End If
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_accAnotherAuxBudget may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accAnotherAuxBudget = Nothing
ValAccountAuxBudget_Err: 
		If Err.Number Then
			ValAccountAuxBudget = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'% ValBudgetDef: Permite verificar si la cuenta de más bajo nivel tiene
	'% presupuestos definidos
	Public Function ValBudgetDef(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		Dim lrecreaLedger_AccBudgetDef As eRemoteDB.Execute
		
		lrecreaLedger_AccBudgetDef = New eRemoteDB.Execute
		On Error GoTo ValBudgetDef_Err
		lstrAccount = Trim(lstrAccount)
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_AccBudgetDef'
		'+ Información leída el 06/06/2001 12:05:29 a.m.
		
		With lrecreaLedger_AccBudgetDef
			.StoredProcedure = "reaLedger_AccBudgetDef"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValBudgetDef = True
			Else
				ValBudgetDef = False
			End If
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_AccBudgetDef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_AccBudgetDef = Nothing
ValBudgetDef_Err: 
		If Err.Number Then
			ValBudgetDef = False
		End If
		On Error GoTo 0
		
	End Function
	
	Public Function ValAnotherAux(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim lrecreaLedger_accAnotherAux As eRemoteDB.Execute
		
		lrecreaLedger_accAnotherAux = New eRemoteDB.Execute
		On Error GoTo ValAnotherAux_err
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_accAnotherAux'
		'+ Información leída el 06/06/2001 02:25:09 p.m.
		
		With lrecreaLedger_accAnotherAux
			.StoredProcedure = "reaLedger_accAnotherAux"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValAnotherAux = True
			Else
				ValAnotherAux = False
			End If
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_accAnotherAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accAnotherAux = Nothing
ValAnotherAux_err: 
		If Err.Number Then
			ValAnotherAux = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% ValAccountStruc: Permite verificar la estructura de la cuenta contable
	Public Function ValAccountStruc(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim llngNum As Integer
		Dim llngPos As Integer
		Dim llngCount As Integer
		Dim lclsLed_compan As Led_compan
		Dim nLevelAux As Integer
		
		lclsLed_compan = New Led_compan
		
		llngNum = 0
		llngPos = 1
		
		ValAccountStruc = True
		
		lstrAccount = Trim(lstrAccount)
		
		lclsLed_compan.Find(lintLed_Compan)
		
		nLevelAux = InStr(1, lclsLed_compan.sStructure, "0") - 1
		
		nLast_level = IIf(nLevelAux = -1, 7, nLevelAux)
		
		For llngCount = 1 To 7
			With lclsLed_compan
				If CDbl(Mid(.sStructure, llngCount, 1)) = 0 Then
					If llngPos <> Len(lstrAccount) Then
						ValAccountStruc = False
					End If
					Exit For
				End If
				
				If Trim(Mid(.sStructure, llngCount, 1)) = "" Then
					Exit For
				End If
				
				Do While Mid(lstrAccount, llngPos, 1) <> "-" And llngPos <= Len(lstrAccount)
					llngNum = llngNum + 1
					llngPos = llngPos + 1
					
					If llngPos > Len(lstrAccount) Then
						Exit Do
					End If
				Loop 
				
				If llngPos > Len(lstrAccount) Then
					If llngNum <> CDbl(Mid(.sStructure, llngCount, 1)) Then
						ValAccountStruc = False
					End If
					Exit For
				Else
					If llngNum <> CDbl(Mid(.sStructure, llngCount, 1)) Then
						ValAccountStruc = False
						Exit For
					Else
						llngNum = 0
						llngPos = llngPos + 1
					End If
				End If
			End With
		Next llngCount
		
		If ValAccountStruc Then
			nLevel = llngCount
		Else
			nLevel = 0
		End If
		
		'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLed_compan = Nothing
	End Function
	
	'%ValAccountPrevious: Permite verificar si la cuenta contable tiene niveles previos
	Public Function ValAccountPrevious(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim llngLength As Integer
		Dim llngCount As Integer
		
		ValAccountPrevious = False
		
		lstrAccount = Trim(lstrAccount)
		llngLength = Len(lstrAccount)
		
		For llngCount = llngLength To 1 Step -1
			If Mid(lstrAccount, llngCount, 1) <> "-" Then
				Mid(lstrAccount, llngCount, 1) = " "
			Else
				Mid(lstrAccount, llngCount, 1) = " "
				
				Exit For
			End If
		Next llngCount
		
		If Trim(lstrAccount) <> "" Then
			If Find_Account(lintLed_Compan, lstrAccount) Then
				ValAccountPrevious = True
			End If
		Else
			ValAccountPrevious = True
		End If
	End Function
	
	'%ValAccountPreviousAux: Permite verificar si la cuenta de nivel superior tiene
	'%auxiliares.
	Public Function ValAccountPreviousAux(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		Dim llngLength As Integer
		Dim llngCount As Integer
		Dim lrecreaLedger_accAnotherAux As eRemoteDB.Execute
		lrecreaLedger_accAnotherAux = New eRemoteDB.Execute
		ValAccountPreviousAux = False
		
		lstrAccount = Trim(lstrAccount)
		llngLength = Len(lstrAccount)
		
		For llngCount = llngLength To 1 Step -1
			If Mid(lstrAccount, llngCount, 1) <> "-" Then
				Mid(lstrAccount, llngCount, 1) = " "
			Else
				Mid(lstrAccount, llngCount, 1) = " "
				
				Exit For
			End If
		Next llngCount
		
		If Trim(lstrAccount) <> "" Then
			ValAccountPreviousAux = ValAnotherAux(lintLed_Compan, lstrAccount)
		End If
	End Function
	
	'% ValPreviousWith_uni_or_Block: Permite verificar si la cuenta de nivel superior tiene
	'% unidad, debitos o creditos bloqueados.
	Public Function ValBlocked(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		Dim lrecreaLedger_accBlocked As eRemoteDB.Execute
		
		lrecreaLedger_accBlocked = New eRemoteDB.Execute
		
		On Error GoTo ValBlocked_err
		lstrAccount = Trim(lstrAccount)
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLedger_accBlocked'
		'+ Información leída el 06/06/2001 03:29:35 p.m.
		
		With lrecreaLedger_accBlocked
			.StoredProcedure = "reaLedger_accBlocked"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValBlocked = True
			Else
				ValBlocked = False
			End If
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_accBlocked may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accBlocked = Nothing
ValBlocked_err: 
		If Err.Number Then
			ValBlocked = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: Permite buscar registros en la tabla de Cuentas Contables
	Public Function Find(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String, ByVal lstrAux_accoun As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaLedger_accAll As eRemoteDB.Execute
		
		lrecreaLedger_accAll = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lstrAccount = Trim(lstrAccount)
		lstrAux_accoun = Trim(lstrAux_accoun)
		
		If lstrAux_accoun = "" Then
			lstrAux_accoun = "                    "
		End If
		
		'+Definición de parámetros para stored procedure 'insudb.reaLedger_accAll'
		'+Información leída el 06/06/2001 03:39:50 p.m.
		
		With lrecreaLedger_accAll
			.StoredProcedure = "reaLedger_accAll"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", lstrAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nLed_compan = .FieldToClass("nLed_compan")
				sAccount = .FieldToClass("sAccount")
				sAux_accoun = .FieldToClass("sAux_accoun")
				sAdju_exci = .FieldToClass("sAdju_exci")
				nAux_create = .FieldToClass("nAux_create")
				
				If .FieldToClass("nBalance") <> eRemoteDB.Constants.intNull Then
					nBalance = .FieldToClass("nBalance")
				Else
					nBalance = 0
				End If
				
				sBlock_cre = .FieldToClass("sBlock_cre")
				sBlock_deb = .FieldToClass("sBlock_deb")
				sBudget_ind = .FieldToClass("sBudget_ind")
				sDescript = .FieldToClass("sDescript")
				sOrgan_unit = .FieldToClass("sOrgan_unit")
				sStatregt = .FieldToClass("sStatregt")
				
				If .FieldToClass("nTotal_cre") <> eRemoteDB.Constants.intNull Then
					nTotal_cre = .FieldToClass("nTotal_cre")
				Else
					nTotal_cre = 0
				End If
				
				If .FieldToClass("nTotal_deb") <> eRemoteDB.Constants.intNull Then
					nTotal_deb = .FieldToClass("nTotal_deb")
				Else
					nTotal_deb = 0
				End If
				
				sType_acc = .FieldToClass("sType_acc")
				.RCloseRec()
				Find = True
			Else
				lintLed_CompanAux = 0
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaLedger_accAll may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLedger_accAll = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Add() As Boolean
		
		Dim lreccreLedger_Acc As eRemoteDB.Execute
		
		lreccreLedger_Acc = New eRemoteDB.Execute
		On Error GoTo Add_err
		'+ Definición de parámetros para stored procedure 'insudb.creLedger_Acc'
		'+ Información leída el 07/06/2001 10:21:34 a.m.
		
		With lreccreLedger_Acc
			.StoredProcedure = "creLedger_Acc"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrgan_unit", sOrgan_unit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdju_exci", sAdju_exci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAux_create", nAux_create, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_cre", sBlock_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_deb", sBlock_deb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBudget_ind", sBudget_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLedger_Acc = Nothing
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Update_Type_BudgetLevelDown: Permite actualizar El tipo de cuenta y el indicador
	'% de presupuesto de los niveles inferiores de una cuenta contable
	Public Function Update_Type_BudgetLevelDown(ByVal lintLedCompan As Integer, ByVal lstrAccount As String, ByVal lstrType_acc As String, ByVal lstrBudget_ind As String) As Boolean
		
		Dim lrecupdLedger_accTypeLevelDown As eRemoteDB.Execute
		
		lrecupdLedger_accTypeLevelDown = New eRemoteDB.Execute
		On Error GoTo Update_Type_BudgetLevelDown_err
		'+ Definición de parámetros para stored procedure 'insudb.updLedger_accTypeLevelDown'
		'+ Información leída el 07/06/2001 10:34:28 a.m.
		
		lstrAccount = Trim(lstrAccount)
		lstrType_acc = Trim(lstrType_acc)
		lstrBudget_ind = Trim(lstrBudget_ind)
		
		With lrecupdLedger_accTypeLevelDown
			.StoredProcedure = "updLedger_accTypeLevelDown"
			.Parameters.Add("nLed_compan", lintLedCompan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", lstrType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBudget_ind", lstrBudget_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Type_BudgetLevelDown = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdLedger_accTypeLevelDown may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLedger_accTypeLevelDown = Nothing
		
Update_Type_BudgetLevelDown_err: 
		If Err.Number Then
			Update_Type_BudgetLevelDown = False
		End If
		On Error GoTo 0
	End Function
	
	
	Public Function Update_TypeLevelDown(ByVal lintLedCompan As Integer, ByVal lstrAccount As String, ByVal lstrType_acc As String) As Boolean
		
		Dim lrecupdLedger_accTypeLevelDown_1 As eRemoteDB.Execute
		
		lrecupdLedger_accTypeLevelDown_1 = New eRemoteDB.Execute
		On Error GoTo Update_TypeLevelDown_err
		'+ Definición de parámetros para stored procedure 'insudb.updLedger_accTypeLevelDown_1'
		'+ Información leída el 07/06/2001 10:37:11 a.m.
		
		lstrAccount = Trim(lstrAccount)
		lstrType_acc = Trim(lstrType_acc)
		
		With lrecupdLedger_accTypeLevelDown_1
			.StoredProcedure = "updLedger_accTypeLevelDown_1"
			.Parameters.Add("nLed_compan", lintLedCompan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", lstrType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_TypeLevelDown = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdLedger_accTypeLevelDown_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLedger_accTypeLevelDown_1 = Nothing
		
Update_TypeLevelDown_err: 
		If Err.Number Then
			Update_TypeLevelDown = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% DelAuxiliars: Permite
	Public Function DelAuxiliars(ByVal lintLed_Compan As Integer, ByVal lstrAccount As String) As Boolean
		
		Dim lrecdelAuxiliars As eRemoteDB.Execute
		
		lrecdelAuxiliars = New eRemoteDB.Execute
		On Error GoTo DelAuxiliars_err
		'+ Definición de parámetros para stored procedure 'insudb.delAuxiliars'
		'+ Información leída el 07/06/2001 10:39:21 a.m.
		
		With lrecdelAuxiliars
			.StoredProcedure = "delAuxiliars"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", lstrAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			DelAuxiliars = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelAuxiliars may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelAuxiliars = Nothing
		
DelAuxiliars_err: 
		If Err.Number Then
			DelAuxiliars = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%CreAux: Permite crear los auxiliares automàticos
	Public Function CreAux(ByVal nTypeAux As eTypeAux) As Boolean
		
		Dim lrecinsCreLedger_accAux As eRemoteDB.Execute
		
		lrecinsCreLedger_accAux = New eRemoteDB.Execute
		
		With lrecinsCreLedger_accAux
			Select Case nTypeAux
				Case eTypeAux.eBanco '1
					.StoredProcedure = "insCreLedger_accTable7"
					
				Case eTypeAux.eCo_Reaseguradores '2
					.StoredProcedure = "insCreLedger_accCompany"
					
				Case eTypeAux.eDepartamento '3
					.StoredProcedure = "insCreLedger_accTable84"
					
				Case eTypeAux.eIntermediario '4
					.StoredProcedure = "insCreLedger_accIntermedia"
					
				Case eTypeAux.eMoneda '5
					.StoredProcedure = "insCreLedger_accTable11"
					
				Case eTypeAux.eRamo '6
					.StoredProcedure = "insCreLedger_accTable10"
					
				Case eTypeAux.eRamos_contables '7
					.StoredProcedure = "insCreLedger_accTable75"
					
				Case eTypeAux.eSucursal '8
					.StoredProcedure = "insCreLedger_accTable9"
					
				Case eTypeAux.eMonedaSucursal '11
					.StoredProcedure = "insCreLedger_accCurrencyOffice"
					
				Case eTypeAux.eMonedaSucursalRamo ' 12
					.StoredProcedure = "insCreLedger_accCurrOfficBran"
					
				Case eTypeAux.eCo_ReaseguradorMoneda ' 13
					.StoredProcedure = "insCreLedger_acccoreincurrency"
					
				Case eTypeAux.eMonedaCo_Reasegurador ' 14
					.StoredProcedure = "insCreLedger_accCurrencyCorein"
					
				Case eTypeAux.eRamoSucursalMoneda ' 15
					.StoredProcedure = "insCreLedger_accBranOfficCurr"
					
				Case eTypeAux.eRamoMoneda ' 16
					.StoredProcedure = "insCreLedger_accBranchCurrency"
					
				Case eTypeAux.eCo_ReaseguradoresRamoMoneda ' 17
					.StoredProcedure = "insCreLedger_accCoreinBranCurr"
					
				Case eTypeAux.SucursalRamo ' 18
					.StoredProcedure = "insCreLedger_accOfficeBranch"
					
			End Select
			
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrgan_unit", sOrgan_unit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdju_exci", sAdju_exci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAux_create", nAux_create, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_cre", sBlock_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_deb", sBlock_deb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBudget_ind", sBudget_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			CreAux = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsCreLedger_accAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCreLedger_accAux = Nothing
	End Function
	
	Public Function Update() As Boolean
		Dim lrecupdLedger_Acc As eRemoteDB.Execute
		
		lrecupdLedger_Acc = New eRemoteDB.Execute
		On Error GoTo Update_Err
		'+ Definición de parámetros para stored procedure 'insudb.updLedger_Acc'
		'+ Información leída el 07/06/2001 10:43:39 p.m.
		
		With lrecupdLedger_Acc
			.StoredProcedure = "updLedger_Acc"
			
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrgan_unit", sOrgan_unit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdju_exci", sAdju_exci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAux_create", nAux_create, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_cre", sBlock_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_deb", sBlock_deb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBudget_ind", sBudget_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLedger_Acc = Nothing
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	Public Function Delete() As Boolean
		Dim lrecdelLedger_Acc As eRemoteDB.Execute
		
		lrecdelLedger_Acc = New eRemoteDB.Execute
		On Error GoTo Delete_err
		'+ Definición de parámetros para stored procedure 'insudb.delLedger_Acc'
		'+ Información leída el 07/06/2001 11:49:57 p.m.
		
		With lrecdelLedger_Acc
			.StoredProcedure = "delLedger_Acc"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLedger_Acc = Nothing
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% ValIndentation: Se controla la indentación de cada nivel inferior perteneciente a un nivel dado
	Public Function ValIndentation(ByVal sAccount As String) As String
		
		'- Se define la variable llngLine utilizada para almacenar el valor del nivel
		
		Dim llngLevel As Integer
		
		'- Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento
		
        'Dim llngCount As Integer
		
		'- Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento
		
		Dim llngCount1 As Integer
		
		'- Se define la variable lstrAux_account utilizada para almacenar el valor de la cuenta contable
		
		Dim lstrAccount_aux As String
		
		lstrAccount_aux = sAccount
		llngLevel = insCal_line_account(lstrAccount_aux)
		
		If llngLevel <> 0 Then
			sAccount = " "
			For llngCount1 = 1 To llngLevel
				sAccount = "   " & sAccount
			Next llngCount1
			
			sAccount = sAccount & Trim(lstrAccount_aux)
		End If
		
		ValIndentation = sAccount
	End Function
	
	'% insCal_line_account: Calcula el número de caracteres iguales a "-"
	'% en la variable pasada como parámetro,
	'% para obtener el nivel de un código de una cuenta contable
	Private Function insCal_line_account(ByVal sAccount_tmp As String) As Integer
		
		'- Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento
		
		Dim llngCount As Integer
		
		'- Se define la variable llngCountLine utilizada para almacenar el valor del nivel
		
		Dim llngCountLine As Integer
		
		For llngCount = 1 To 20
			If Mid(Trim(sAccount_tmp), llngCount, 1) = "" Then
				Exit For
			ElseIf Mid(Trim(sAccount_tmp), llngCount, 1) = "-" Then 
				llngCountLine = llngCountLine + 1
			End If
		Next llngCount
		
		insCal_line_account = llngCountLine
	End Function
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	
	'% insValCPC001_K: Valida los datos introducidos para la consulta del Catálogo de Cuentas.
	Public Function insValCPC001_K(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal nTypeQuery As Integer, ByVal dEffecdate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		
		On Error GoTo insValCPC001_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		mclsLed_compan = New Led_compan
		
		Call mclsLed_compan.Find(nLed_compan)
		
		'+ Se efectua la validación del campo fecha de transacción.
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 736020)
		Else
			If Not lclsField.ValDate(dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 7114)
			Else
				
				'+ Si no se ha realizado el cierre correspondiente se valida solamente
				'+ contra la fecha de inicio.
				If CDate(mclsLed_compan.dDate_end) = eRemoteDB.Constants.dtmNull Then
					If dEffecdate < CDate(mclsLed_compan.dDate_init) Then
						Call lclsErrors.ErrorMessage(sCodispl, 736022)
					End If
				Else
					If dEffecdate > CDate(mclsLed_compan.dDate_end) Or dEffecdate < CDate(mclsLed_compan.dDate_init) Then
						Call lclsErrors.ErrorMessage(sCodispl, 736022)
					End If
				End If
			End If
		End If
		
		insValCPC001_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object mclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsLed_compan = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCPC001_K_Err: 
		If Err.Number Then
			insValCPC001_K = insValCPC001_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insValCPC002_K: Valida los datos introducidos para la consulta de Asientos de una Cuenta.
	Public Function insValCPC002_K(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal dInitDate As Date) As String
		''-------------------------------------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLedger_Acc As eLedge.LedgerAcc
		Dim lblnIndic As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsLedger_Acc = New eLedge.LedgerAcc
		
		'+ Validación del campo "Fecha Inicial"
		If dInitDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36077)
		End If
		
		'+ Validación del campo "Cuenta Contable"
		If sAccount = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 36017)
		Else
			If lclsLedger_Acc.ValAccountStruc(nLed_compan, sAccount) Then
				If lclsLedger_Acc.nLast_level <> lclsLedger_Acc.nLevel Then
                    'Call lclsErrors.ErrorMessage(sCodispl, 7129)
					lblnIndic = True
				End If
			Else
				
			End If
			
			'            If Not mclsLedger_acc.Find_AccountActive(nLed_compan, sAccount) Then
			'                Call lclsErrors.ErrorMessage(sCodispl, 36010)
			'            Else
			'                lblDescript.Caption = mclsLedger_acc.sDescript
			'                If insVal_Structure_Down(sAccount) Then
			'                    Call lclsErrors.ErrorMessage(sCodispl, 7129)
			'                End If
			'            End If
			'        End If
		End If
		
		
		'+ Validación del campo "Auxiliar de cuenta contable"
		If sAux_accoun = String.Empty Then
			If sAccount <> String.Empty And Not lblnIndic Then
				If Not lclsLedger_Acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36021)
				Else
					If lclsLedger_Acc.ValAnotherAux(nLed_compan, sAccount) Then
						Call lclsErrors.ErrorMessage(sCodispl, 1012)
					End If
				End If
			End If
		Else
			If sAccount <> String.Empty Then
				If Not lclsLedger_Acc.Find_Active(nLed_compan, sAccount, sAux_accoun) Then
					Call lclsErrors.ErrorMessage(sCodispl, 36021)
				End If
			End If
		End If
		
		insValCPC002_K = lclsErrors.Confirm
		
insValCPC002_K_Err: 
		If Err.Number Then
			insValCPC002_K = insValCPC002_K & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsLedger_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedger_Acc = Nothing
	End Function
	
	'**%insValCPL001_K: This function perform validations over the fields of the CPL001
	'%insValCPL001_K: Esta función se encarga de validar los datos introducidos en la CPL001
	Public Function insValCPL001_K(ByVal sCodispl As String, ByVal nLevel As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCPL001_K_Err
		
		'**+Validations related to column: nLevel
		'+ Se valida la columna: nLevel
		If nLevel = 0 Or nLevel = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36106)
		End If
		
		insValCPL001_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCPL001_K_Err: 
		If Err.Number Then
			insValCPL001_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%--------------------------------------------------------------------------------
	Public Function insConstructAccount(ByVal lstrAccount As String, ByVal lstrStructure As String, ByVal lstrType As String) As String
		Dim llngArrStructure(7) As Integer
		Dim llngLength As Integer
		Dim llngCount As Integer
		Dim llngPos As Integer
		
		If lstrType = "1" Then
			llngPos = 7
		Else
			llngPos = 3
		End If
		
		lstrAccount = Trim(lstrAccount)
		insConstructAccount = lstrAccount
		
		If InStr(1, lstrAccount, "-", 1) > 0 Then
			Exit Function
		End If
		
		For llngCount = 1 To llngPos
			llngArrStructure(llngCount) = CInt(Mid(lstrStructure, llngCount, 1))
		Next llngCount
		
		llngLength = Len(lstrAccount)
		
		For llngCount = 1 To llngPos
			If (llngArrStructure(llngCount) > llngLength Or llngArrStructure(llngCount) = 0) Then
				If llngCount = 1 Then
					Exit Function
				Else
					If llngLength <> 0 Then
						insConstructAccount = Trim(insConstructAccount) & "-" & lstrAccount
					End If
					
					Exit Function
				End If
			Else
				If llngCount = 1 Then
					insConstructAccount = Mid(lstrAccount, 1, llngArrStructure(llngCount))
				Else
					insConstructAccount = Trim(insConstructAccount) & "-" & Mid(lstrAccount, 1, llngArrStructure(llngCount))
				End If
				
				lstrAccount = Mid(lstrAccount, llngArrStructure(llngCount) + 1, llngLength - llngArrStructure(llngCount))
				llngLength = Len(lstrAccount)
			End If
		Next llngCount
	End Function
End Class






