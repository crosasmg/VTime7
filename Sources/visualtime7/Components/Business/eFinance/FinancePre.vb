Option Strict Off
Option Explicit On
Public Class FinancePre
	'%-------------------------------------------------------%'
	'% $Workfile:: FinancePre.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 25/10/04 3:07p                               $%'
	'% $Revision:: 33                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Properties according to the table in the system on September 16,1999.
	'+ Propiedades según la tabla en el sistema el 16/09/1999.
	'+ The key fields corresponds to a nContract and nReceipt.
	'+ Los campos llaves corresponden a nContrat y nReceipt.
	
	'+  Column name               Type                            Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  ------------------------- ------------------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public nBranch As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nCommission As Double 'decimal     6      10    2     yes      (n/a)              (n/a)
	Public dCompdate As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nContrat As Double 'int         4      10    0     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public dStartdate As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nExchange As Double 'decimal     6      10    6     yes      (n/a)              (n/a)
	Public dExpirdat As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nIntermed As Integer 'int         4      10    0     yes      (n/a)              (n/a)
	Public nOffice As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nPolicy As Double 'int         4      10    0     yes      (n/a)              (n/a)
	Public nPremium As Double 'decimal     6      10    2     yes      (n/a)              (n/a)
	Public nReceipt As Integer 'int         4      10    0     no       (n/a)              (n/a)
	Public sStat_finpr As String 'char        1                  yes      yes                yes
	Public sStatregt As String 'char        1                  yes      yes                yes
	Public sClient As String 'char        14                 yes      yes                yes
	Public nUsercode As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nProduct As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nCompany As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public sExtReceipt As String 'char        1                  yes      yes                yes
	
	'- Auxiliary properties
	'- Propiedades auxiliares
	Public sCliename As String 'Descripción del cliente
	Public sCurrency As String 'Descripción de la moneda
	Public sProduct As String 'Descripción del producto
	Public sCompCliename As String 'Descripción de la compañía
	Public sOffice As String 'Descripción de la sucursal
	Public sIntermed As String 'Descripción del intermediario
	
	
	'- Defines the variable to know if it must be created or not in financ_com
	'- Se definen la variable para saber si se debe crear o no en financ_com
	Public ncreFinanc_com As Integer
	
	'- Defines the variable that determines the status of the class.
	'- Se define la variable que determina el estado de la clase
	Public nStatInstanc As FinanceDraft.eStatusInstance
	Public nStat_draft As Integer
	
	'- Defines the recordset that will be used in the class.
	'- Se define el recordset que será utilizado en la clase
	Private lrecFinanc_pre As eRemoteDB.Execute
	
	'- Private lclsPremium As eCollection.Premium
	Private lclsPremium As eCollection.Premium
	
	
	'% Find: This method returns TRUE or FALSE depending if the records exists in the table "Financ_pre"
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%       tabla "Financ_pre"
	Public Function Find(ByVal nContrat As Double, ByVal nReceipt As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		If (Me.nContrat = nContrat And Me.nReceipt = nReceipt) Or lblnFind Then
			Find = True
		Else
			
			lrecFinanc_pre = New eRemoteDB.Execute
			Me.nContrat = nContrat
			Me.nReceipt = nReceipt
			
			'+ Parameter definition for stored procedure 'insudb.insreaFinanc_pre'
			'+ Definición de parámetros para stored procedure 'insudb.insreaFinanc_pre'
			'+ Information read on August 19,1999  10:05:22 a.m.
			'+ Información leída el 19/08/1999 10:05:22 AM
			
			With lrecFinanc_pre
				.StoredProcedure = "insreaFinanc_prepkg.insreaFinanc_pre"
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nContrat = .FieldToClass("nContrat")
					Me.nReceipt = .FieldToClass("nReceipt")
					nBranch = .FieldToClass("nBranch")
					nCommission = .FieldToClass("nCommission")
					nCurrency = .FieldToClass("nCurrency")
					dStartdate = .FieldToClass("dStartdate")
					nExchange = .FieldToClass("nExchange")
					dExpirdat = .FieldToClass("dExpirdat")
					nIntermed = .FieldToClass("nIntermed")
					nOffice = .FieldToClass("nOffice")
					nPolicy = .FieldToClass("nPolicy")
					nPremium = .FieldToClass("nPremium")
					sStat_finpr = .FieldToClass("sStat_finpr")
					sStatregt = .FieldToClass("sStatregt")
					sClient = .FieldToClass("sClient")
					sCliename = .FieldToClass("sCliename")
					sCurrency = .FieldToClass("Currency_sDescript")
					nProduct = .FieldToClass("nProduct")
					sProduct = .FieldToClass("Product_sDescript")
					nCompany = .FieldToClass("nCompany")
					sExtReceipt = .FieldToClass("sExtReceipt")
					sCompCliename = .FieldToClass("sCompCliename")
					sOffice = .FieldToClass("Office_sDescript")
					sIntermed = .FieldToClass("sIntCliename")
					
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFinanc_pre = Nothing
		
	End Function
	
	'%insPreLoadFI002 : realiza la generación de datos para la transaccion
	Public Function insPreLoadFI002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nRegen As Integer) As Boolean
		Dim lrecinsPreloadfi002 As eRemoteDB.Execute
		On Error GoTo insPreloadfi002_Err
		
		lrecinsPreloadfi002 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insPreloadfi002 al 04-08-2004 16:41:42
		'+
		With lrecinsPreloadfi002
			.StoredProcedure = "insPreloadFI002"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRegen", nRegen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPreLoadFI002 = .Run(False)
		End With
		
insPreloadfi002_Err: 
		If Err.Number Then
			insPreLoadFI002 = False
		End If
		'UPGRADE_NOTE: Object lrecinsPreloadfi002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPreloadfi002 = Nothing
		On Error GoTo 0
	End Function
	
	'% insValFI007: This method validates the page "FI007" as described in the functional specifications
	'% InsValFI007: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%              de la ventana "FI007"
	Public Function insValFI007(ByVal sAction As String, ByVal nContrat As Double, ByVal nCompany As Integer, ByVal nOffice As Integer, ByVal nIntermed As Integer, ByVal nReceipt As Double, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nCurrency As Integer, ByVal nExchange As Double, ByVal nCommisssion As Double, ByVal nPremium As Double) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsFinanceCO As financeCO
		Dim lclsValField As eFunctions.valField
		Dim lclsGeneral As eGeneral.GeneralFunction
		
		On Error GoTo insValFI007_err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceCO = New financeCO
		lclsValField = New eFunctions.valField
		lclsGeneral = New eGeneral.GeneralFunction
		
		
		Call lclsFinanceCO.Find(nContrat, dEffecdate)
		'+ Verifies that the company is full
		'+ Se Verifica que la compañía esta llena
		If nCompany = 0 Or nCompany = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 1046)
		End If
		
		
		'+ Verifies that the zone is full
		'+ Se Verifica que la zona esta llena
		If (nOffice = 0 Or nOffice = eRemoteDB.Constants.intNull) And lclsGeneral.Find_Officeins(nCompany) Then
			lclsErrors.ErrorMessage("FI007", 1040)
		End If
		
		If nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 21038)
		End If
		
		If nReceipt = 0 Or nReceipt = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 21034)
		Else
			If lclsValField.ValNumber(nReceipt,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If sAction = "Add" Then
					If (nCompany <> 0 And nCompany <> eRemoteDB.Constants.intNull And nReceipt <> 0 And nReceipt <> eRemoteDB.Constants.intNull And nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull) Then
						'+ Can't be included in the grid
						'+ No puede estar incluído en el grid
						If Find(nContrat, nReceipt) Then
							lclsErrors.ErrorMessage("FI007", 21030)
						Else
							'+ Can't be financed for the same contract (even if it is of the user company)
							'+ No puede estar financiado para el mismo contrato  (aunque sea de la compañía usuaria)
							If Find_Receipt(nReceipt) Then
								lclsErrors.ErrorMessage("FI007", 21154)
							End If
						End If
					End If
				End If
			End If
		End If
		
		If nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 21033)
		Else
			Call lclsValField.ValNumber(nPolicy,  , eFunctions.valField.eTypeValField.onlyvalid)
		End If
		
		
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 1022)
		Else
			Call lclsValField.ValNumber(nBranch,  , eFunctions.valField.eTypeValField.onlyvalid)
		End If
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage("FI007", 21006)
		End If
		
		If dExpirdat = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage("FI007", 21035)
			'+ If it is not empty, must be posterior to the effective date
			'+ Si no está vacío, debe ser posterior a la fecha de efecto
		Else
			If CDate(dExpirdat) <= CDate(dEffecdate) Then
				lclsErrors.ErrorMessage("FI007", 21036)
			End If
		End If
		
		
		'+ If the receipt's currency is different than the contract's currency, it can't be empty.
		'+ Si la moneda del recibo es diferente a la moneda del contrato, no puede estar vacío
		If nCurrency <> 0 And nCurrency <> eRemoteDB.Constants.intNull Then
			If nCurrency <> lclsFinanceCO.nCurrency Then
				If nExchange = 0 Or nExchange = eRemoteDB.Constants.intNull Then
					lclsErrors.ErrorMessage("FI007", 4100)
				Else
					Call lclsValField.ValNumber(nExchange,  , eFunctions.valField.eTypeValField.onlyvalid)
				End If
			End If
		End If
		
		'+ If it is not empty, it must be minor than the receipt's Total.
		'+ Si no está vacío, debe ser menor que el Total del recibo
		If nCommission <> 0 And Fix(nCommission) <> eRemoteDB.Constants.intNull And nPremium <> 0 And Fix(nPremium) <> eRemoteDB.Constants.intNull Then
			If lclsValField.ValNumber(nCommission,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nCommission >= nPremium Then
					lclsErrors.ErrorMessage("FI007", 21040)
				End If
			End If
		End If
		
		
		If nPremium = 0 Or Fix(nPremium) = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("FI007", 21037)
		Else
			If lclsValField.ValNumber(nPremium,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nCommission <> 0 Or Fix(nCommission) <> eRemoteDB.Constants.intNull Then
					If CInt(nCommission) >= nPremium Then
						lclsErrors.ErrorMessage("FI007", 21040)
					End If
				End If
			End If
		End If
		insValFI007 = lclsErrors.Confirm
insValFI007_err: 
		If Err.Number Then
			insValFI007 = Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
	End Function
	
	'% insPostFI007: This method updates the database (as described in the functional specifications)
	'%               for the page "FI007"
	'% insPostFI007: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%               especificaciones funcionales)de la ventana "FI007"
	Public Function insPostFI007(ByVal sAction As String, ByVal nTransaction As Integer, ByVal nOffice As Integer, ByVal sClient As String, ByVal nReceipt As Double, ByVal nPremium As Double, ByVal nPolicy As Double, ByVal nIntermed As Integer, ByVal dExpirdat As Date, ByVal dStartdate As Date, ByVal nExchange As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nContrat As Double, ByVal nCommission As Double, ByVal nBranch As Integer, ByVal nCompany As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinancePre As eFinance.FinancePre
		Dim lclsFinanco As financeCO
		Dim lclsFinanWin As FinanceWin
		Dim lstrStat_finpr As String
		Dim lstrStatregt As String
		
		On Error GoTo insPostFI007_err
		
		lclsFinancePre = New eFinance.FinancePre
		lclsFinanco = New financeCO
		lclsFinanWin = New FinanceWin
		
		insPostFI007 = True
		
		'- Charge the data in the Financo table, to update the receipt's amount
		'- Carga de datos de la tabla Financo, Para actualizar el importe del recibo
		Call lclsFinanco.Find(nContrat, dEffecdate)
		
		If sAction = "Add" Then
			
			'+ Status of the receipt: "1" To finance or "2" Financed
			'+ Estado del recibo: "1" Por financiar o "2" Financiado
			lstrStat_finpr = "1"
			
			'+ General status of the record: Active (see table with identification 26)
			'+ Estado general del registro : Activo (ver tabla con identificativo 26)
			lstrStatregt = "1"
			If nOffice = eRemoteDB.Constants.intNull Then
				nOffice = 0
			End If
			With lclsFinancePre
				.nStatInstanc = FinanceDraft.eStatusInstance.eftNew
				.nContrat = nContrat
				.nReceipt = nReceipt
				.nBranch = nBranch
				.nCommission = nCommission
				.nCurrency = nCurrency
				.nExchange = nExchange
				.dStartdate = dStartdate
				.dExpirdat = dExpirdat
				.nIntermed = nIntermed
				.nOffice = nOffice
				.nPolicy = nPolicy
				.nPremium = nPremium
				.sStat_finpr = lstrStat_finpr
				.sStatregt = lstrStatregt
				.sClient = sClient
				.nUsercode = nUsercode
				.nProduct = nProduct
				.nCompany = nCompany
				.ncreFinanc_com = 1
				.sExtReceipt = "1"
				insPostFI007 = .Add(1)
			End With
		ElseIf sAction = "Update" Then 
			Call lclsFinancePre.Find(nContrat, nReceipt)
			With lclsFinancePre
				.nStatInstanc = FinanceDraft.eStatusInstance.eftUpDate
				.nBranch = nBranch
				.nReceipt = nReceipt
				.nPolicy = nPolicy
				.sOffice = sOffice
				.dStartdate = dEffecdate
				.dExpirdat = dExpirdat
				.nExchange = nExchange
				.nCommission = nCommission
				.nPremium = nPremium
				.nCompany = nCompany
				.nOffice = nOffice
				.nCurrency = nCurrency
				.nIntermed = nIntermed
				.sExtReceipt = "1"
				.ncreFinanc_com = 1
				insPostFI007 = .UpDate(1)
			End With
		Else
			With lclsFinancePre
				.nStatInstanc = FinanceDraft.eStatusInstance.eftDelete
				.nContrat = nContrat
				.nReceipt = nReceipt
				insPostFI007 = .Delete
			End With
		End If
		
		'- Update in the Finance_co table with the Amount field.
		'- Actualiza en la tabla Finance_co el campo Importe
		If insPostFI007 Then
			With lclsFinanco
				If .nAmount > 0 Then
					.nAmount = .nAmount + nPremium
				Else
					.nAmount = nPremium
				End If
				.nUsercode = nUsercode
				Call .UpDate()
			End With
			
		End If
		
		'- Update in the finan_win table the content of the FI007 screen
		'- Actualiza en la tabla finan_win el contenido de la pantalla FI007
		With lclsFinancePre
			'- If it was iserted or updated and executed perfectly
			'- Si inserto o actualizo y se ejecuto correctamente
			If .nStatInstanc <> FinanceDraft.eStatusInstance.eftDelete And insPostFI007 Then
				Call lclsFinanWin.Add_Finan_win(nContrat, dEffecdate, "FI007", "2", nUsercode, nTransaction)
			Else
				If .Find(nContrat, nReceipt) Then
					Call lclsFinanWin.Add_Finan_win(nContrat, dEffecdate, "FI007", "2", nUsercode, nTransaction)
				Else
					Call lclsFinanWin.Add_Finan_win(nContrat, dEffecdate, "FI007", "1", nUsercode, nTransaction)
				End If
			End If
		End With
		
insPostFI007_err: 
		If Err.Number Then
			insPostFI007 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
		'UPGRADE_NOTE: Object lclsFinanco may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanco = Nothing
		'UPGRADE_NOTE: Object lclsFinanWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanWin = Nothing
	End Function
	'% Find_Receipt: verifies that the receipt is not financed in other contract
	'% Find_Receipt: verifica que el recibo no se encuentre financiado en otro contrato
	Public Function Find_Receipt(ByVal nReceipt As Double) As Boolean
		Dim lclsFinanceCO As financeCO
		Dim lrecinsreaFinanc_pre As eRemoteDB.Execute
		
		lclsFinanceCO = New financeCO
		lrecinsreaFinanc_pre = New eRemoteDB.Execute
		
		'+ Parameter definition for stored procedure 'insudb.insreaFinanc_pre'
		'+ Information read on Novemeber 08,1999  11:41:54
		'+ Definición de parámetros para stored procedure 'insudb.insreaFinanc_pre'
		'+ Información leída el 08/11/1999 11:41:54
		
		With lrecinsreaFinanc_pre
			.StoredProcedure = "insreaFinanc_prepkg.insreaFinanc_pre"
			.Parameters.Add("nContrat", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If lclsFinanceCO.Find_Stat_contr(.FieldToClass("nContrat")) Then
						If lclsFinanceCO.nStat_contr <> financeCO.Estat_contr.Eannul Then
							nContrat = .FieldToClass("nContrat")
							Find_Receipt = True
							Exit Do
						Else
							Find_Receipt = False
						End If
					End If
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsreaFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsreaFinanc_pre = Nothing
		
	End Function
	
	'% ADD: This method is in charge of adding new records to the table "Financ_pre".  It returns TRUE or FALSE
	'%      depending on whether the stored procedure executed correctly.
	'% ADD: Este método se encarga de agregar nuevos registros a la tabla "Financ_pre". Devolviendo verdadero o
	'%      falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add(ByVal ncreFinanc_com As Integer) As Boolean
		lrecFinanc_pre = New eRemoteDB.Execute
		
		'+ Parameter definition or stored procedure 'insud.creFinanc_pre'
		'+ Definición de parámetros para stored procedure 'insudb.creFinanc_pre'
		'+ Information read on August 11,1999  09:04:35 a.m.
		'+ Información leída el 11/08/1999 09:04:35 AM
		With lrecFinanc_pre
			.StoredProcedure = "inscreFinanc_pre"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_finpr", sStat_finpr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncreFinanc_com", ncreFinanc_com, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExtReceipt", sExtReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUpdpremium", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFinanc_pre = Nothing
		
	End Function
	
	'% Update: This method is in charge of updating records in the table "Financ_pre".  It returns TRUE or FALSE
	'%         depending on whether the stored procedure executed correctly.
	'% Update: Este método se encarga de actualizar registros en la tabla "Financ_pre". Devolviendo verdadero o
	'%         falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpDate(ByVal ncreFinanc_com As Integer) As Boolean
		lrecFinanc_pre = New eRemoteDB.Execute
		
		'+ Parameter definition for stored procedure 'insudb.updFinanc_pre'
		'+ Definición de parámetros para stored procedure 'insudb.updFinanc_pre'
		'+ Information read on august 13,1999  10:20:49 a.m.
		'+ Información leída el 13/08/1999 10:20:49 AM
		With lrecFinanc_pre
			.StoredProcedure = "updFinanc_pre"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_finpr", sStat_finpr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncreFinanc_com", ncreFinanc_com, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExtReceipt", sExtReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUpdpremium", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpDate = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFinanc_pre = Nothing
		
	End Function
	
	'% Delete: This method is in charge of Deleting records in the table "Finance_pre_Receipt".  It returns TRUE or FALSE
	'%         depending on whether the stored procedure executed correctly.
	'% Delete: Este método se encarga de eliminar registros en la tabla "Finance_pre_Receipt". Devolviendo verdadero o
	'%         falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		lrecFinanc_pre = New eRemoteDB.Execute
		
		'+ Parameter definition for stored procedure 'insudb.delFinance_pre_Receipt'
		'+ Definición de parámetros para stored procedure 'insudb.delFinance_pre_Receipt'
		'+ Information read on September 06,199 10:02:25 a.m.
		'+ Información leída el 06/09/1999 10:02:25 AM
		
		With lrecFinanc_pre
			.StoredProcedure = "delFinance_pre_Receipt"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFinanc_pre = Nothing
		
	End Function
	
	'% ReverseCollectPremium: This function is in charge of marking the receipt with the
	'%                        wanted status.
	'% ReverseCollectPremium: Esta funci¢n se encarga de marcar los recibos
	'%                        con el estado deseado.
	Public Function ReverseCollectPremium() As Boolean
		ReverseCollectPremium = False
		
		Dim lrecinsChangeStatusPremium As eRemoteDB.Execute
		
		lrecinsChangeStatusPremium = New eRemoteDB.Execute
		
		'+ Parameter definition for stored procedure 'insudb.insChangeStatusPremium'
		'+ Definición de parámetros para stored procedure 'insudb.insChangeStatusPremium'
		'+ Information read on Septemeber 29, 199 01:32:17 p.m.
		'+ Información leída el 29/09/1999 01:32:17 PM
		With lrecinsChangeStatusPremium
			.StoredProcedure = "insChangeStatusPremium"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_Pre", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ReverseCollectPremium = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsChangeStatusPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsChangeStatusPremium = Nothing
		
	End Function
	'% insValFI002: This method validates the page "FI002" as described in the functional specifications
	'% InsValFI002: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%              de la ventana "FI002"
	Public Function insValFI002(ByVal sCodispl As String, ByVal nSelected As String, ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nAmount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsFinanceCO As financeCO
		
		On Error GoTo insValFI002_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ At least one of the lines of the window (inthe RECEIPT field) must have content
		'+ Al menos una de las lineas de la ventana (en el campo RECIBO) debe tener contenido
		If CDbl(nSelected) = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 21025)
		Else
			If nAmount > 0 Then
				lclsFinanceCO = New financeCO
				
				Call lclsFinanceCO.Find(nContrat, dEffecdate)
				
				'+ Validate that the initial quote is less than the total to finance.
				'+ Se Valida que la cuota inicial sea menor que el total a financiar
				If nAmount <= lclsFinanceCO.nInitial_or Then
					lclsErrors.ErrorMessage(sCodispl, 21148)
				End If
				'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsFinanceCO = Nothing
			End If
		End If
		
		insValFI002 = lclsErrors.Confirm
		
insValFI002_Err: 
		If Err.Number Then
			insValFI002 = insValFI002 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'% insValFI002Upd: Make the validations to the correspondent fields to the form.
	'% insValFI002Upd: Se realizan las validaciones de los campos correspondientes a la forma.
	Public Function insValFI002Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nContrat As Double, ByVal nCompany As Integer, ByVal nExchange As Double, ByVal nFirstIntermed As Integer, ByVal nIntermed As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nUserSystemCompany As Object) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolFinancePres As FinancePres
		Dim lclsPremium As Object
		Dim lclsFinanceCO As financeCO
		Dim lclsValField As eFunctions.valField
		Dim lclsProduct As Object
		
		lclsFinanceCO = New financeCO
		lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValFI002Upd_Err
		
		Call Find(nContrat, nReceipt)
		Call lclsFinanceCO.Find(nContrat, dEffecdate)
		
		'+ If the fiels is not empty
		'+ Si el campo no está vacío
		If (nReceipt <> 0 And nReceipt <> eRemoteDB.Constants.intNull) Then
			
			lclsValField = New eFunctions.valField
			lclsValField.objErr = lclsErrors
			If lclsValField.ValNumber(nReceipt,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				
				'+ If it is in the Premium table.
				'+ Si se encuentra en la tabla Premium
				With lclsPremium
					If .Find("2", nReceipt, nBranch, nProduct, 0, 0) Then
						
						'+ Must have "pending for collect" status: nStatus_pre must be equal to 1
						'+ Debe tener estado "pendiente de cobro": nStatus_pre debe ser igual a 1
						If .nStatus_pre <> 1 Then
							lclsErrors.ErrorMessage(sCodispl, 21027)
						End If
						
						'+ The expiring date must be inside the standing period of the contract
						'+ La fecha de vencimiento debe estar dentro del período de vigencia del contrato
						If .dExpirdat < lclsFinanceCO.dEffecdate Or .dExpirdat > DateAdd(Microsoft.VisualBasic.DateInterval.Year, 2, lclsFinanceCO.dFirst_draf) Then
							lclsErrors.ErrorMessage(sCodispl, 21028)
						End If
						
						'+ The intermediary must be the same as the first recipt.
						'+ El intermediario debe ser el mismo del primer recibo
						If nFirstIntermed <> nIntermed Then
							
							'+ If the intermediary is different in all the receipts keep 0 in Financ_pre
							'+ Si el intermediario es diferente en todos los recibos se guarda 0 en Financ_dra
							lclsErrors.ErrorMessage(sCodispl, 21029)
						End If
						
						'+ The client field must correspond with the contract's bearer
						'+ El campo Cliente debe corresponder con el titular del contrato
						If lclsFinanceCO.sClient <> .sClient Then
							lclsErrors.ErrorMessage(sCodispl, 21111)
						End If
						
						'+ No debe financiar productos de vida no tradicional (vidactiv)
						lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
						Call lclsProduct.FindProduct_li(nBranch, nProduct, Today)
						If lclsProduct.nProdClas = 3 Or lclsProduct.nProdClas = 4 Or lclsProduct.nProdClas = 5 Then
							lclsErrors.ErrorMessage(sCodispl, 55554)
						End If
						'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsProduct = Nothing
					Else
						'+ Debe existir en la base de datos
						lclsErrors.ErrorMessage(sCodispl, 21026)
					End If
				End With
			End If
			'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsValField = Nothing
		End If
		
		'+ In case that the action to make is add
		'+ En caso que la acción a efectuar sea agregar
		If sAction = "Add" Then
			lcolFinancePres = New FinancePres
			Call lcolFinancePres.Find(nContrat)
			
			'+ Can't include twice the same receipt in a contract:
			'+ No puede incluirse dos veces el mismo recibo en un contrato:
			
			'+ Can't be financed for the same contract (even if it is not of the user company)
			'+ No puede estar financiado para el mismo contrato (aunque no sea de la compañía usuaria)
			If lcolFinancePres.FoundReceipt(nReceipt, nContrat) Then
				lclsErrors.ErrorMessage(sCodispl, 21030)
			End If
			
			'+ Can't be in differents contracts
			'+ No puede estar en diferentes contratos
			If Find_Receipt(nReceipt) Then
				If Me.nContrat <> lclsFinanceCO.nContrat Then
					lclsErrors.ErrorMessage(sCodispl, 21100)
				End If
			End If
			
			'+ No pueden financiar recibos de distintas polizas en el mismo contrato
			If lcolFinancePres.FindPolicy(nPolicy) Then
				lclsErrors.ErrorMessage(sCodispl, 55556)
			End If
			'UPGRADE_NOTE: Object lcolFinancePres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolFinancePres = Nothing
		End If
		
		'+ Validation of the Exchange Rate field
		'+ Validación del campo Factor de cambio
		
		'+ If the currency does not correspond to the contract, must be full
		'+ Si la moneda no corresponde a la del contrato, debe estar lleno
		If lclsPremium.nCurrency <> lclsFinanceCO.nCurrency Then
			If nExchange = 0 Or nExchange = eRemoteDB.Constants.intNull Then
				lclsErrors.ErrorMessage(sCodispl, 21031)
			End If
		End If
		
		insValFI002Upd = lclsErrors.Confirm
		
insValFI002Upd_Err: 
		If Err.Number Then
			insValFI002Upd = insValFI002Upd & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostFI002Upd: This method updates the database (as described in the functional specifications)
	'%                  for the page "FI002"
	'% insPostFI002Upd: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%                  especificaciones funcionales)de la ventana "FI002"
	Public Function insPostFI002Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nContrat As Double, ByVal nReceipt As Double, ByVal nPremium As Double, ByVal sProduct As String, ByVal nCommission As Double, ByVal nCurrency As Integer, ByVal sClient As String, ByVal nIntermed As Integer, ByVal nOffice As Integer, ByVal nCompany As Integer, ByVal nExchange As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsFinancePres As FinancePres
		Dim lclsFinanceWin As FinanceWin
		
		On Error GoTo insPostFI002Upd_Err
		
		With Me
			If sAction <> "Del" Then
				.nContrat = nContrat
				.nReceipt = nReceipt
				.nBranch = nBranch
				.nCommission = nCommission
				.nCurrency = nCurrency
				.nExchange = nExchange
				.dStartdate = dStartdate
				.dExpirdat = dExpirdat
				.nIntermed = nIntermed
				.nOffice = nOffice
				.nPolicy = nPolicy
				.nPremium = nPremium
				.sStat_finpr = "1"
				.sStatregt = "1"
				.sClient = sClient
				.nUsercode = nUsercode
				.nProduct = nProduct
				.nCompany = nCompany
				.ncreFinanc_com = 1
				.sExtReceipt = "2"
				If sAction = "Add" Then
					.nStatInstanc = FinanceDraft.eStatusInstance.eftNew
					insPostFI002Upd = Add(.ncreFinanc_com)
				Else
					.nStatInstanc = FinanceDraft.eStatusInstance.eftUpDate
					insPostFI002Upd = UpDate(.ncreFinanc_com)
				End If
				
				If insPostFI002Upd Then
					
					lclsFinanceWin = New FinanceWin
					If sAction = "Add" Then
						'+Se actualiza la ventana con contenido
						Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI002", "2", nUsercode, nTransaction)
					End If
					'+Se deja requerida la ventana de refinanciamiento
					Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI003", "1", nUsercode, nTransaction)
					'+Se deja requerida la ventana de cuotas
					Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI004", "1", nUsercode, nTransaction)
					'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinanceWin = Nothing
				End If
				
			Else
				.nStatInstanc = FinanceDraft.eStatusInstance.eftDelete
				.nContrat = nContrat
				.nReceipt = nReceipt
				insPostFI002Upd = Delete
				
				If insPostFI002Upd Then
					lclsFinanceWin = New FinanceWin
					
					lclsFinancePres = New FinancePres
					If Not lclsFinancePres.Find_DataReceipt(nContrat, dEffecdate, True) Then
						'+Se actualiza la ventana sin contenido
						Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI002", "1", nUsercode, nTransaction)
					End If
					'UPGRADE_NOTE: Object lclsFinancePres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinancePres = Nothing
					
					'+Se deja requerida la ventana de refinanciamiento para forzar a recargarla
					Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI003", "1", nUsercode, nTransaction)
					'+Se deja requerida la ventana de cuotas para forzar a recargarla
					Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI004", "1", nUsercode, nTransaction)
					
					'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinanceWin = Nothing
				End If
			End If
		End With
		
insPostFI002Upd_Err: 
		If Err.Number Then
			insPostFI002Upd = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% InsPostFI002: This method updates the database (as described in the functional specifications)
	'%               for the page "FI002"
	'% InsPostFI002: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%               especificaciones funcionales)de la ventana "FI002"
	Public Function InsPostFI002(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nAmount As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinanceCO As financeCO
		Dim lcolFinanceDrafts As FinanceDrafts
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsFinancePres As FinancePres
		Dim lclsFinanceWin As FinanceWin
		Dim ldblFactor As Double
		Dim strContent As String
		
		On Error GoTo insPostFI002_Err
		
		InsPostFI002 = True
		
		If nAmount > 0 Then
			lclsFinanceCO = New financeCO
			lcolFinanceDrafts = New FinanceDrafts
			
			With lclsFinanceCO
				If .Find(nContrat, dEffecdate) Then
					
					'+Se aplica porcentaje de descuento por pronto pago
					If .nDscto_pag > 0 Then
						nAmount = nAmount - (nAmount * .nDscto_pag / 100)
					End If
					
					'+Se aplica factor asociado a porcentaje de interes
					ldblFactor = lcolFinanceDrafts.SearchFactor(.nQ_draft, .nInterest, dEffecdate)
					If ldblFactor > 0 Then
						.nAmount = nAmount * ldblFactor * .nQ_draft
					Else
						ldblFactor = 1 / .nQ_draft
						.nAmount = nAmount * ldblFactor * .nQ_draft
					End If
					.nAmount_d = .nAmount
					.nUsercode = nUsercode
					
					If .UpDate Then
						InsPostFI002 = True
					End If
				End If
				
			End With
			'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanceCO = Nothing
			'UPGRADE_NOTE: Object lcolFinanceDrafts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolFinanceDrafts = Nothing
		Else
			InsPostFI002 = True
		End If
		
		'+Como se actualizó información del contrato,
		'+se eliminan la relacion recibo contrato
		lclsFinanceDraft = New eFinance.FinanceDraft
		Call lclsFinanceDraft.Delete_All(nContrat)
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		
		lclsFinancePres = New FinancePres
		If lclsFinancePres.Find(nContrat, True) Then
			strContent = "2"
		Else
			strContent = "1"
		End If
		'UPGRADE_NOTE: Object lclsFinancePres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePres = Nothing
		
		'+Se actualiza la ventana segun contenido
		lclsFinanceWin = New FinanceWin
		Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI002", strContent, nUsercode, nTransaction)
		'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceWin = Nothing
		
insPostFI002_Err: 
		If Err.Number Then
			InsPostFI002 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% FindnExchange: Search the value of the exchange rate accordin to the introduced currency
	'% FindnExchange: Busca el valor del factor de cambio de acuerdo a la moneda introducida
	Public Function FindnExchange(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer) As Double
		Dim lclsExchange As eGeneral.Exchange
		Dim lclsFinanceCO As eFinance.financeCO
		
		lclsExchange = New eGeneral.Exchange
		lclsFinanceCO = New eFinance.financeCO
		
		If lclsFinanceCO.Find(nContrat, dEffecdate) Then
			If lclsFinanceCO.nCurrency <> nCurrency Then
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, 1, nCurrency, 1, dEffecdate, 0)
				If lclsExchange.pdblExchange <> -1 Then
					FindnExchange = lclsExchange.pdblExchange
				Else
					FindnExchange = 0
				End If
			Else
				FindnExchange = 1
			End If
		Else
			FindnExchange = 0
		End If
	End Function
	
	'% inscalTotal: calculates the total to finance and the commission total of a receipt.
	'% inscalTotal: calcula el total a financiar y el total de comisión de un recibo
	Private Function inscalTotal(ByVal nCurrency_CO As Integer, ByVal dEffecdate_CO As Date, ByVal nAmount_CO As Double) As Double
		
		Dim lclsExchange As eGeneral.Exchange
		lclsExchange = New eGeneral.Exchange
		
		If nCurrency_CO <> lclsPremium.nCurrency Then
			'+ Calculate the equivalent of the total to finance to the currency of the contract.
			'+ Se calcula el equivalente del Total a financiar a la moneda del contrato
			Call lclsExchange.Convert(eRemoteDB.Constants.intNull, lclsPremium.nPremium, lclsPremium.nCurrency, nCurrency_CO, dEffecdate_CO, 0)
			inscalTotal = nAmount_CO + lclsExchange.pdblResult
			
		Else
			inscalTotal = nAmount_CO + lclsPremium.nPremium
		End If
	End Function
End Class






