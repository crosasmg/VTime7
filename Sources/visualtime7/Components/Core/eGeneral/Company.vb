Option Strict Off
Option Explicit On
Public Class Company
	'%-------------------------------------------------------%'
	'% $Workfile:: Company.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema el 07/11/2000.
	'- El campo llave corresponde a nCompany.
	
	'- Tipo de cliente: Natural o Jurídico
	Public Enum eTypeClient
		Company
		Person
	End Enum
	
	'+  Column name                Type                 Computed Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  -------------------------  -------------------- -------- ------ ---- ----- -------- ------------------ ---------------------
	Public nCompany As Integer 'smallint no       2      5     0    no       (n/a)              (n/a)
	Public sAccount As String 'char     no       25                yes      no                 yes
	Public sClient As String 'char     no       14                yes      no                 yes
	Public sBankname As String 'char     no       30                yes      no                 yes
	
	Public nCompany_br As Integer 'smallint no       2      5     0    no       (n/a)              (n/a)
	Public nCompany_det As Integer 'smallint no       2      5     0    no       (n/a)              (n/a)
	Public dCompdate As Date 'datetime no       8                 yes      (n/a)              (n/a)
	Public dEffecdate As Date 'datetime no       8                 yes      (n/a)              (n/a)
	Public nCountry As Integer 'smallint no       2      5     0    yes      (n/a)              (n/a)
	Public dInpdate As Date 'datetime no       8                 yes      (n/a)              (n/a)
	Public sStatregt As String 'char     no       1                 yes      no                 yes
	Public nTaxrate As Double 'decimal  no       5      4     2    yes      (n/a)              (n/a)
	Public sType As String 'char     no       1                 yes      no                 yes
	Public nUsercode As Integer 'smallint no       2      5     0    yes      (n/a)              (n/a)
	Public sNational As String 'char     no       1                 yes      no                 yes
	'+ Se agregan estos parametros según had 346
	Public sRegsvs As String ' CHAR       10   0     0    S
	Public nClassific As Integer ' NUMBER     22   0     5    S
	Public nClasific As Integer ' NUMBER     22   0     5    S
	
	'+ Propiedades auxiliares
	'- Descripción de la compañía
	Public sDescript As String
	'-Digito verificador de la compañia
	Public sDigit As String
	
	Private Structure udtBroker_det
		Dim nSel As Integer
		Dim sClient As String
		Dim sCliename As String
		Dim nCompany As Integer
		Dim nCompany_det As Integer
		Dim sType As String
		Dim nClasific As Integer
	End Structure
	
	Private arrBroker_det() As udtBroker_det
	
	'% FindClient: Este procedimiento se encarga de verificar la existencia
	'%             del código de cliente en la tabla de compañías (Company)
	Public Function FindClient(ByVal sClient As String) As Boolean
		Dim lrecreaCompany_sClient As eRemoteDB.Execute
		
		On Error GoTo FindClient_Err
		
		lrecreaCompany_sClient = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCompany_sClient'
		'+ Información leída el 16/02/2001 10:16:00 a.m.
		
		With lrecreaCompany_sClient
			.StoredProcedure = "reaCompany_sClient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCompany = .FieldToClass("nCompany")
				sAccount = .FieldToClass("sAccount")
				sClient = .FieldToClass("sClient")
				sBankname = .FieldToClass("sBankname")
				
				nCountry = .FieldToClass("nCountry")
				dInpdate = .FieldToClass("dInpdate")
				sStatregt = .FieldToClass("sStatregt")
				nTaxrate = .FieldToClass("nTaxrate")
				sType = .FieldToClass("sType")
				sNational = .FieldToClass("sNational")
				nCountry = .FieldToClass("nCountry")
				FindClient = True
				.RCloseRec()
			End If
		End With
		
FindClient_Err: 
		If Err.Number Then
			FindClient = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCompany_sClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCompany_sClient = Nothing
	End Function
	
	'%Find: realiza la lectura en la tabla
	Public Function Find_CompanyClient(ByVal nCompany As Integer) As Boolean
		Dim lrecReaCompanyClient As eRemoteDB.Execute
		
		lrecReaCompanyClient = New eRemoteDB.Execute
		
		On Error GoTo Find_CompanyClient_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.ReaCompanyClient'
		'+ Información leída el 07/11/2000 08:38:37 a.m.
		
		With lrecReaCompanyClient
			.StoredProcedure = "ReaCompanyClient"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CompanyClient = True
				Me.nCompany = .FieldToClass("nCompany")
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
			Else
				Find_CompanyClient = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaCompanyClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCompanyClient = Nothing
		
Find_CompanyClient_Err: 
		If Err.Number Then
			Find_CompanyClient = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: realiza la lectura de la tabla
	Public Function Find(ByVal nCompany As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaCompany As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		Find = True
		
		If nCompany <> Me.nCompany Or lblnFind Then
			Me.nCompany = nCompany
			
			'+ Definición de parámetros para stored procedure 'insudb.reaCompany'
			'+ Información leída el 28/02/2001 18.56.30
			
			lrecreaCompany = New eRemoteDB.Execute
			With lrecreaCompany
				.StoredProcedure = "reaCompany"
				.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nCompany = .FieldToClass("nCompany")
					sAccount = .FieldToClass("sAccount")
					sClient = .FieldToClass("sClient")
					sBankname = .FieldToClass("sBankname")
					
					dInpdate = .FieldToClass("dInpdate")
					sStatregt = .FieldToClass("sStatregt")
					nTaxrate = .FieldToClass("nTaxrate")
					sType = .FieldToClass("sType")
					sRegsvs = .FieldToClass("sRegsvs")
					nClassific = .FieldToClass("nClasific")
					nCountry = .FieldToClass("nCountry")
					sDigit = .FieldToClass("sDigit")
					
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCompany = Nothing
	End Function
	
	'% CountClient: Cuenta los clientes que existen en la tabla Company para diferentes compañías
	Public Function CountClient(ByVal sClient As String) As Boolean
		Dim lrecreaClient_cia_c As eRemoteDB.Execute
		
		On Error GoTo CountClient_Err
		
		lrecreaClient_cia_c = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClient_cia_c'
		'+ Información leída el 11/07/2001 01:51:50 p.m.
		
		With lrecreaClient_cia_c
			.StoredProcedure = "reaClient_cia_c"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nCompany = .FieldToClass("nCompany")
				CountClient = IIf(.FieldToClass("nCounter") > 0, True, False)
				.RCloseRec()
			End If
		End With
		
CountClient_Err: 
		If Err.Number Then
			CountClient = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaClient_cia_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_cia_c = Nothing
	End Function
	
	'% Update: Actualiza un registro en la tabla de Compañías (Company)
	Public Function Update() As Boolean
		Dim lrecinsCompany As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lrecinsCompany = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insCompany'
		'+ Información leída el 12/07/2001 06:20:27 p.m.
		
		With lrecinsCompany
			.StoredProcedure = "insCompany"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankname", sBankname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxrate", nTaxrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNational", sNational, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegsvs", sRegsvs, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassific", nClassific, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecinsCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCompany = Nothing
	End Function
	
	'% Delete: Elimina un registro en la tabla de Compañías (Company)
	Public Function Delete() As Boolean
		Dim lrecdelCompany As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		'+ Definición de parámetros para stored procedure 'delCompany'
		'+ Información leída el 06/06/2002
		lrecdelCompany = New eRemoteDB.Execute
		With lrecdelCompany
			.StoredProcedure = "delCompany"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCompany = Nothing
	End Function
	
	'% insValMS110_K: Valida los datos introducidos en la cabecera de la forma
	Public Function insValMS110_K(ByVal sCodispl As String, ByVal nCompany As Integer, ByVal nMainAction As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS110_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del código de la compañía, campo "Compañía"
		
		If nCompany = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 12092)
		Else
			If nMainAction = eFunctions.Menues.TypeActions.clngActionadd Then
				If Me.Find(nCompany) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6014)
				End If
			Else
				If Not Me.Find(nCompany) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6002)
				End If
			End If
		End If
		
		insValMS110_K = lclsErrors.Confirm
		
insValMS110_K_Err: 
		If Err.Number Then
			insValMS110_K = insValMS110_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insValMS110: Valida los datos introducidos en la zona de detalle para forma
	Public Function insValMS110(ByVal sCodispl As String, ByVal nCompany As Integer, ByVal sClient As String, ByVal nMainAction As Integer, ByVal dInputDate As Date, ByVal sStatregt As String, ByVal sType As String, ByVal nTaxrate As Double, ByVal sBankname As String, ByVal sAccount As String, ByVal nCountry As Integer, ByVal sRegsvs As String, ByVal nClassific As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalClient As Object
		Dim lclsClient As Object
		
		On Error GoTo insValMS110_Err
		
		lclsErrors = New eFunctions.Errors
		lclsvalClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.ValClient")
		lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		
		'+ Validación del campo "Identificación fiscal"
		If sClient = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 2001)
		Else
			If lclsvalClient.Validate(sClient, nMainAction) Then
				'+ Se valida que el código del cliente corresponda a un ente jurídico
				If lclsvalClient.ClientType = eTypeClient.Person Then
					Call lclsErrors.ErrorMessage(sCodispl, 6136)
				End If
				'+ Se valida que el código del cliente sea único en el archivo de compañías
				If Me.CountClient(sClient) Then
					If Me.nCompany <> nCompany Then
						Call lclsErrors.ErrorMessage(sCodispl, 6137)
					End If
				End If
				'+ El cliente debe tener dirección de cobro asociada
				If Not lclsClient.AddressClient(sClient) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3269)
				End If
			ElseIf lclsvalClient.Status = 2 Then 
				'+ Se manda el mensaje correspondiente a la estructura del cliente
				Call lclsErrors.ErrorMessage(sCodispl, 2012)
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 2013)
			End If
		End If
		'+ Validación de la fecha de ingreso
		If dInputDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de ingreso:")
		Else
			If Format(dInputDate, "yyyyMMdd") > Today.ToString("yyyyMMdd") Then
				Call lclsErrors.ErrorMessage(sCodispl, 1002)
			End If
		End If
		'+ Validación del estado
		If sStatregt = CStr(eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6138)
		End If
		
		'+ Validación del tipo de compañía
		If sType = CStr(eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6005)
		End If
		
		'+ Validación el país debe estar lleno
		If nCountry = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60312)
		End If
		
		'+ Validación el numero inscripcion SVS debe estar lleno
		If sRegsvs = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60313)
		End If
		
		'+ Validación el Clasificación debe estar lleno
		If nClassific = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60121)
		End If
		
		'+ Validación del impuesto
		If nTaxrate = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6010)
		End If
		
		'+ Validación del banco y la cuenta
		If (sBankname = String.Empty And sAccount <> String.Empty) Or (sBankname <> String.Empty And sAccount = String.Empty) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6009)
		End If
		
		insValMS110 = lclsErrors.Confirm
		
insValMS110_Err: 
		If Err.Number Then
			insValMS110 = insValMS110 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalClient = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'% insValMS110_Upd: Valida los datos introducidos en la cabecera de la forma
	Public Function insValMS110_Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nCompany As Integer, ByVal nCompany_det As Integer, ByVal nCompanyType As Integer, ByVal nClasific As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS110_Upd_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del código de la compañía, campo "Compañía"
		If nCompany_det = eRemoteDB.Constants.intNull Or nCompany_det = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 12092)
		Else
			If sAction = "Add" Then
				
				'+ Valida que el código de la cía ingresada no este asociada a otra cia broker
				If Me.Find_Broker_Other_Cia(nCompany, nCompany_det) Then
					Call lclsErrors.ErrorMessage(sCodispl, 100108)
				End If
				
				'+ Valida que el código de la cía ingresada no este asociada a la misma cia broker
				If Me.Find_Broker_Cia(nCompany, nCompany_det) Then
					Call lclsErrors.ErrorMessage(sCodispl, 100109)
				End If
				
				'+ Valida que el código de la cía ingresada no sea igual a la cia broker
				If nCompany = nCompany_det Then
					Call lclsErrors.ErrorMessage(sCodispl, 100110)
				End If
				
			End If
			
			'        Else
			'           If Not Me.Find(nCompany_det) Then
			'              Call lclsErrors.ErrorMessage(sCodispl, 6002)
			'         End If
			
			If sAction = "Del" Then
				If Me.Find_Broker_Contr(nCompany_det) Then
					Call lclsErrors.ErrorMessage(sCodispl, 100111)
				End If
				
			End If
		End If
		
		insValMS110_Upd = lclsErrors.Confirm
		
insValMS110_Upd_Err: 
		If Err.Number Then
			insValMS110_Upd = insValMS110_Upd & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	
	
	'% insPostMS110: Valida todos los datos introducidos en la forma
	Public Function InsPostMS110(ByVal sClient As String, ByVal nMainAction As Integer, ByVal dInpdate As Date, ByVal sStatregt As String, ByVal sType As String, ByVal nTaxrate As Double, ByVal sBankname As String, ByVal sAccount As String, ByVal nCompany As Integer, ByVal nUsercode As Integer, ByVal nCountry As Integer, ByVal sRegsvs As String, ByVal nClassific As Integer) As Boolean
		With Me
			.sClient = sClient
			.dInpdate = dInpdate
			.sStatregt = sStatregt
			.sType = sType
			.nTaxrate = nTaxrate
			.sBankname = sBankname
			.sAccount = sAccount
			.nCompany = nCompany
			.nCountry = nCountry
			.nUsercode = nUsercode
			.sNational = "2"
			.sRegsvs = sRegsvs
			.nClassific = nClassific
		End With
		
		Select Case nMainAction
			Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionUpdate
				InsPostMS110 = Update
		End Select
	End Function
	
	
	''%Find_Broker_det: Función que realiza la lectura de la tabla Broker_Det
	''-----------------------------------------------------------------------------------------
	'Public Function Find_Broker_det(ByVal nCompany As Long, _
	''                     Optional ByVal lblnFind As Boolean = False) As Boolean
	''--------------------------------------------------------------------------------------------
	'
	'    Dim lrecreaBroker_det As eRemoteDB.Execute
	'    Dim lclsValues    As eFunctions.Values
	'    Dim lintCount     As Long
	'
	'    On Error GoTo Find_Broker_det_err
	'
	'    Set lclsValues = New eFunctions.Values
	'
	'    Find_Broker_det = True
	'
	'    If nCompany <> Me.nCompany Or _
	''       lblnFind Then
	'        Me.nCompany = nCompany
	'
	''+ Definición de parámetros para stored procedure 'insudb.reaFind_Broker_det'
	''+ Información leída el 28/02/2001 18.56.30
	'
	'       Set lrecreaBroker_det = New eRemoteDB.Execute
	'        With lrecreaBroker_det
	'            .StoredProcedure = "reaFind_Broker_det"
	'            .Parameters.Add "nCompany", nCompany, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'
	'            If .Run Then
	'                sClient = .FieldToClass("sClient")
	'                nCompany_det = .FieldToClass("nCompany_det")
	'                sType = .FieldToClass("sType")
	'                nClasific = .FieldToClass("nClasific")
	'
	'                .RCloseRec
	'            Else
	'                Find_Broker_det = False
	'            End If
	'        End With
	'    End If
	'
	'Find_Broker_det_err:
	'    If Err Then
	'        Find_Broker_det = False
	'    End If
	'    On Error GoTo 0
	'    Set lrecreaBroker_det = Nothing
	'End Function
	'
	
	'% Update: Actualiza un registro en la tabla de Compañías (Company)
	Public Function Upd_Broker_det() As Boolean
		Dim lrecinsBroker_det As eRemoteDB.Execute
		
		On Error GoTo Upd_Broker_err
		
		lrecinsBroker_det = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insCompany'
		'+ Información leída el 12/07/2001 06:20:27 p.m.
		
		With lrecinsBroker_det
			.StoredProcedure = "upd_broker_det"
			.Parameters.Add("nCompany_br", nCompany_br, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Upd_Broker_det = .Run(False)
		End With
Upd_Broker_err: 
		If Err.Number Then
			Upd_Broker_det = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecinsBroker_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBroker_det = Nothing
	End Function
	
	
	'%Find_Broker_Cia: VERIFICA QUE LA COMPANIA ESTE ASOCIADA A UN CONTRATO
	Public Function Find_Broker_Contr(ByVal nCompany As Integer) As Boolean
		Dim lrecReaBroker_Contr As eRemoteDB.Execute
		
		lrecReaBroker_Contr = New eRemoteDB.Execute
		
		On Error GoTo Find_Broker_Contr_Err
		
		With lrecReaBroker_Contr
			.StoredProcedure = "REA_BROKER_CONTR"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Broker_Contr = True
				Me.nCompany = .FieldToClass("nCompany")
				.RCloseRec()
			Else
				Find_Broker_Contr = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaBroker_Contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaBroker_Contr = Nothing
		
Find_Broker_Contr_Err: 
		If Err.Number Then
			Find_Broker_Contr = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Broker_Other_Cia: VERIFICA QUE LA COMPANIA NO ESTE ASOCIADA A OTRA CIA BROKER
	Public Function Find_Broker_Other_Cia(ByVal nCompany_br As Integer, ByVal nCompany As Integer) As Boolean
		Dim lrecReaBroker_Other_Cia As eRemoteDB.Execute
		
		lrecReaBroker_Other_Cia = New eRemoteDB.Execute
		
		On Error GoTo Find_Broker_Other_Cia_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.ReaCompanyClient'
		'+ Información leída el 07/11/2000 08:38:37 a.m.
		
		With lrecReaBroker_Other_Cia
			.StoredProcedure = "REA_BROKER_OTHER_CIA"
			.Parameters.Add("nCompany_br", nCompany_br, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Broker_Other_Cia = True
				nCompany_det = .FieldToClass("nCompany_det")
				.RCloseRec()
			Else
				Find_Broker_Other_Cia = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaBroker_Other_Cia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaBroker_Other_Cia = Nothing
		
Find_Broker_Other_Cia_Err: 
		If Err.Number Then
			Find_Broker_Other_Cia = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Broker_Cia: VERIFICA QUE LA COMPANIA ESTE ASOCIADA A LA CIA BROKER
	Public Function Find_Broker_Cia(ByVal nCompany_br As Integer, ByVal nCompany As Integer) As Boolean
		Dim lrecReaBroker_Cia As eRemoteDB.Execute
		
		lrecReaBroker_Cia = New eRemoteDB.Execute
		
		On Error GoTo Find_Broker_Cia_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.ReaCompanyClient'
		'+ Información leída el 07/11/2000 08:38:37 a.m.
		
		With lrecReaBroker_Cia
			.StoredProcedure = "REA_BROKER_CIA"
			.Parameters.Add("nCompany_br", nCompany_br, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Broker_Cia = True
				nCompany_det = .FieldToClass("nCompany_det")
				.RCloseRec()
			Else
				Find_Broker_Cia = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaBroker_Cia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaBroker_Cia = Nothing
		
Find_Broker_Cia_Err: 
		If Err.Number Then
			Find_Broker_Cia = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsPostMS110Upd: Actualizaciones de la transacción MS110, según especificaciones funcionales
	Public Function InsPostMS110Upd(ByVal sAction As String, ByVal nCompany_br As Integer, ByVal nCompany As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMS110Upd_Err
		
		With Me
			.nCompany_br = nCompany_br
			.nCompany = nCompany
			.nUsercode = nUsercode
			
			Select Case sAction
				Case "Add"
					InsPostMS110Upd = .Add_Broker
				Case "Del"
					InsPostMS110Upd = .Delete_Broker
			End Select
			
		End With
InsPostMS110Upd_Err: 
		If Err.Number Then
			InsPostMS110Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%Add_Broker: Agrega datos de la tabla broker_det
	Public Function Add_Broker() As Boolean
		Add_Broker = Ins_Upd_Broker_det(1)
	End Function
	
	
	'%Delete_Broker: Borra los datos de la tabla broker_det
	Public Function Delete_Broker() As Boolean
		Delete_Broker = Ins_Upd_Broker_det(2)
	End Function
	
	
	'%InsUpdlife_p_speci: Realiza la actualización de la tabla
	Private Function Ins_Upd_Broker_det(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdBroker_det As eRemoteDB.Execute
		
		On Error GoTo Ins_Upd_Broker_det_Err
		'+ Definición de store procedure InsUpdBroker_det al 27-03-2007
		lrecInsUpdBroker_det = New eRemoteDB.Execute
		With lrecInsUpdBroker_det
			.StoredProcedure = "Ins_Upd_Broker_det"
			.Parameters.Add("nCompany_br", nCompany_br, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Ins_Upd_Broker_det = .Run(False)
		End With
		
		
Ins_Upd_Broker_det_Err: 
		If Err.Number Then
			Ins_Upd_Broker_det = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdBroker_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdBroker_det = Nothing
		On Error GoTo 0
	End Function
End Class






