Option Strict Off
Option Explicit On
Public Class Contrmaster
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrmaster.cls                          $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 27/03/06 19:27                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla CONTRMASTER al 03-04-2002 11:49:01
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nType_rel As Integer ' NUMBER     22   0     0    N
	Public nNumber As Integer ' NUMBER     22   0     0    N
	Public nType As Integer ' NUMBER     22   0     0    N
	Public dStartdate As Date ' DATE       7    0     0    N
	Public dExpirdate As Date ' DATE       7    0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public sStatregt As String ' CHAR       1    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'+ se utilizan para la transacion CR302
	Public nCurr_pay As Integer ' NUMBER     22   0     5    S
	Public sFormpay As String ' CHAR       1    0     0    S
	Public dExpirdat As Date ' DATE       7    0     0    S
	
	
	
	
	'**+ Auxiliary properties
	'+ Propiedades Auxiliares
	Private mvarCodeCurrency As Integer
	Private mvarRetention As Double
	Public nSel As Integer
	Public nAmount As Double
	
	Private Structure udtContrmaster
		Dim nSel As Integer
		Dim nNumber As Integer
		Dim nType As Integer
		Dim nBranch As Integer
		Dim nCurrency As Integer
		Dim dStartdate As Date
		Dim nAmount As Double
		Dim nType_rel As Integer
	End Structure
	
	Private arrContrmaster() As udtContrmaster
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrContrmaster)
		End Get
	End Property
	
	'***CodeCurrency: This property is in charge to capture the currency code
	'*CodeCurrency: Esta propiedad se encarga de capturar el código de la moneda
	Public ReadOnly Property CodeCurrency() As Integer
		Get
			CodeCurrency = mvarCodeCurrency
		End Get
	End Property
	'*** Retention: This property is in charge to capture the retention amount
	'*Retention: Esta propiedad se encarga de capturar el monto de retencion
	Public ReadOnly Property Retention() As Integer
		Get
			Retention = mvarRetention
		End Get
	End Property
	Public Function ItemContrmaster(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrContrmaster) Then
			With arrContrmaster(lintIndex)
				nSel = .nSel
				nNumber = .nNumber
				nType = .nType
				nBranch = .nBranch
				nCurrency = .nCurrency
				dStartdate = .dStartdate
				nAmount = .nAmount
				nType_rel = .nType_rel
			End With
			ItemContrmaster = True
		Else
			ItemContrmaster = False
		End If
	End Function
	
	'**% Find: Makes the reading to verify the existance of the contract code
	'%Find: Se realiza la lectura para verificar de la existencia del código del contrato
	Public Function Find(ByVal nType_rel As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dStartdate As Date) As Boolean
		Dim lrecreaContrmaster As eRemoteDB.Execute
		
		lrecreaContrmaster = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.reaContrmaster'
		'**+ Data read on 05/23/2001 10:04:48 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaContrmaster'
		'+ Información leída el 23/05/2001 10:04:48 a.m.
		
		With lrecreaContrmaster
			.StoredProcedure = "reaContrMaster"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nType <> 0 Then
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nType", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If dStartdate <> eRemoteDB.Constants.dtmNull Then
				.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dStartdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nBranch <> 0 Then
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If .Run Then
				Me.nType_rel = .FieldToClass("nType_rel")
				Me.nNumber = .FieldToClass("nNumber")
				Me.nType = .FieldToClass("nType")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.sStatregt = .FieldToClass("sStatregt")
				Me.dCompdate = .FieldToClass("dCompdate")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.nCurr_pay = .FieldToClass("nCurr_pay")
				Me.sFormpay = .FieldToClass("sFormpay")
				Me.dExpirdat = .FieldToClass("dExpirdat")
				Me.dStartdate = .FieldToClass("dStartdate")
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrmaster = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'**%Find_Type: makes the validation of the existance of the contract code
	'%Find_Type: Se realiza la validación de la existencia del código del contrato
	Public Function Find_Type(ByVal nType_rel As Integer, ByVal nType As Integer, ByVal dStartdate As Date, ByVal nBranch As Integer) As Boolean
		Dim lrecreaContrmaster_type As eRemoteDB.Execute
		
		lrecreaContrmaster_type = New eRemoteDB.Execute
		
		On Error GoTo Find_Type_Err
		
		'**+ Parameters definitio for the stored porcedure 'insudb.reaContrmaster_type'
		'**+ Data read on 05/23/2001 10:44:21 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaContrmaster_type'
		'+ Información leída el 23/05/2001 10:44:21 a.m.
		
		With lrecreaContrmaster_type
			.StoredProcedure = "reaContrmaster_type"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Type = True
				Me.nCurrency = .FieldToClass("nCurrency")
			Else
				Find_Type = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaContrmaster_type may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrmaster_type = Nothing
		
Find_Type_Err: 
		If Err.Number Then
			Find_Type = False
		End If
	End Function
	
	'**%Find_Num: makes the validation of the existance of the contract code
	'%Find_Num: Se realiza la validación de la existencia del codigo de del contrato
	Public Function Find_Num(ByVal nNumber As Integer) As Boolean
		Dim lrecreaContrmaster_num As eRemoteDB.Execute
		
		lrecreaContrmaster_num = New eRemoteDB.Execute
		
		On Error GoTo Find_Num_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.reaContrmaster_num'
		'**+ Data read on 05/23/2001 10:51:11 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaContrmaster_num'
		'+ Información leída el 23/05/2001 10:51:11 a.m.
		
		With lrecreaContrmaster_num
			.StoredProcedure = "reaContrmaster_num"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Num = True
			Else
				Find_Num = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaContrmaster_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrmaster_num = Nothing
		
Find_Num_Err: 
		If Err.Number Then
			Find_Num = False
		End If
	End Function
	'**%creContrMaster:  Creation of one record in the master file of the reinsurance contracts
	'%creContrMaster: Creación de un registro en el archivo maestro de los contratos de reaseguro
	Public Function creContrMaster() As Boolean
		
		Dim lreccreContrmaster As eRemoteDB.Execute
		
		lreccreContrmaster = New eRemoteDB.Execute
		
		On Error GoTo creContrMaster_Err
		
		'**+ Parameters definition for the stored porcedure 'insudb.creContrmaster'
		'**+ Data read on 05/23/2001 09:29:48 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.creContrmaster'
		'+ Información leída el 23/05/2001 09:29:48 a.m.
		
		With lreccreContrmaster
			.StoredProcedure = "creContrmaster"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				creContrMaster = True
			Else
				creContrMaster = False
			End If
		End With
		'UPGRADE_NOTE: Object lreccreContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreContrmaster = Nothing
		
creContrMaster_Err: 
		If Err.Number Then
			creContrMaster = False
		End If
	End Function
	
	'**%updContrMasterCurrency: This routine is incharge to update the currency contract
	'%updContrMasterCurrency: Esta rutina se encarga de actualizar la moneda del contrato
	Public Function updContrMasterCurrency() As Boolean
		Dim lrecupdContrmaster_currency As eRemoteDB.Execute
		
		lrecupdContrmaster_currency = New eRemoteDB.Execute
		
		On Error GoTo updContrMasterCurrency_Err
		
		'**+ Parameters definition for the stored porcedure 'insudb.updContrmaster_currency'
		'**+ Data read on 05/25/2001 04:23:19 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.updContrmaster_currency'
		'+ Información leída el 25/05/2001 04:23:19 p.m.
		
		With lrecupdContrmaster_currency
			.StoredProcedure = "updContrmaster_currency"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurr_pay", nCurr_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updContrMasterCurrency = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdContrmaster_currency may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContrmaster_currency = Nothing
		
updContrMasterCurrency_Err: 
		If Err.Number Then
			updContrMasterCurrency = False
		End If
	End Function
	
	'**%FindTreaties: This function is incharge to show the information of all the existance
	'**%contracts in the system
	'%FindTreaties: Esta función se encarga de mostrar la información de todos los contratos
	'%existentes en el sistema
	Public Function FindTreaties(ByVal lstrSQL As String) As Boolean
		
		Dim lrecreaContrmaster_a As eRemoteDB.Execute
		Dim lclsValues As eFunctions.Values
		Dim lintCount As Integer
		
		lrecreaContrmaster_a = New eRemoteDB.Execute
		lclsValues = New eFunctions.Values
		
		On Error GoTo FindTreaties_Err
		
		FindTreaties = True
		
		'**+ Prepare and execute the consult "StoredProcedure" of the error message
		'+Se prepara y ejecuta el "StoredProcedure" de consulta de los mensajes de error
		With lrecreaContrmaster_a
			If Trim(lstrSQL) = String.Empty Then
				.StoredProcedure = "reaContrmaster_a"
			Else
				.Sql = lstrSQL
			End If
			
			'**+ Parameters definition for the stored porcedure 'insudb.reaContrmaster_a'
			'**+ Data read on 06/15/2001 02:16:55 p.m.
			'+ Definición de parámetros para stored procedure 'insudb.reaContrmaster_a'
			'+ Información leída el 15/06/2001 02:16:55 p.m.
			
			FindTreaties = .Run
			If FindTreaties Then
				ReDim arrContrmaster(80)
				lintCount = 0
				Do While Not .EOF
					arrContrmaster(lintCount).nSel = 1
					arrContrmaster(lintCount).nNumber = lclsValues.StringToType(.FieldToClass("nNumber"), eFunctions.Values.eTypeData.etdInteger)
					arrContrmaster(lintCount).nType = lclsValues.StringToType(.FieldToClass("nType"), eFunctions.Values.eTypeData.etdInteger)
					arrContrmaster(lintCount).nBranch = lclsValues.StringToType(.FieldToClass("nBranch"), eFunctions.Values.eTypeData.etdInteger)
					arrContrmaster(lintCount).nCurrency = lclsValues.StringToType(.FieldToClass("nCurrency"), eFunctions.Values.eTypeData.etdInteger)
					arrContrmaster(lintCount).dStartdate = lclsValues.StringToType(.FieldToClass("dStartdate"), eFunctions.Values.eTypeData.etdDate)
					
					If lstrSQL = String.Empty Then
						If lclsValues.StringToType(.FieldToClass("nType_rel"), eFunctions.Values.eTypeData.etdInteger) = 1 Then
							arrContrmaster(lintCount).nAmount = lclsValues.StringToType(.FieldToClass("nAmountProc"), eFunctions.Values.eTypeData.etdDouble)
						Else
							arrContrmaster(lintCount).nAmount = lclsValues.StringToType(.FieldToClass("nAmountnPro"), eFunctions.Values.eTypeData.etdDouble)
						End If
					End If
					
					arrContrmaster(lintCount).nType_rel = lclsValues.StringToType(.FieldToClass("nType_rel"), eFunctions.Values.eTypeData.etdInteger)
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrContrmaster(lintCount)
			Else
				FindTreaties = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaContrmaster_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrmaster_a = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
FindTreaties_Err: 
		If Err.Number Then
			FindTreaties = False
		End If
	End Function
	
	'**%Delete:This function is in charge of deleting the contract of co/reinsurance just created
	'%Delete: Esta funcion se encarga de eliminar el contrato de co/reaseguro recien creado
	Public Function Delete(ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal dStartdate As Date, ByVal nBranch As Integer) As Boolean
		Dim lrecdelContrmaster As eRemoteDB.Execute
		
		lrecdelContrmaster = New eRemoteDB.Execute
		
		Dim lintType_rel As Integer
		
		On Error GoTo Delete_Err
		
		If sCodispl_CR = "CR301_k" Then
			lintType_rel = 1
		ElseIf sCodispl_CR = "CR304_k" Then 
			lintType_rel = 2
		End If
		
		'**+ Parameters definition for the stored procedure 'insudb.delContrmaster'
		'**+ Data read on 07/14/2001 12:12:55 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.delContrmaster'
		'+ Información leída el 14/07/2001 12:12:55 p.m.
		
		With lrecdelContrmaster
			.StoredProcedure = "delContrmaster"
			.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelContrmaster = Nothing
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**%updContrMasterStatregt: This routine is in charge to update the contract status
	'%updContrMasterStatregt: Esta rutina se encarga de actualizar el estado del contrato
	Public Function updContrMasterStatregt(ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal sStatregt As String) As Boolean
		Dim lrecupdContrmaster_statregt As eRemoteDB.Execute
		
		lrecupdContrmaster_statregt = New eRemoteDB.Execute
		
		Dim lintType_rel As Integer
		
		On Error GoTo updContrMasterStatregt_Err
		
		If sCodispl_CR = "CR301_k" Then
			lintType_rel = 1
		ElseIf sCodispl_CR = "CR304_k" Then 
			lintType_rel = 2
		End If
		
		'**+ Parameters definition for the stored procedure 'insudb.updContrmaster_statregt'
		'**+ Data read on 07/14/2001 12:18:55 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.updContrmaster_statregt'
		'+ Información leída el 14/07/2001 12:18:55 p.m.
		
		With lrecupdContrmaster_statregt
			.StoredProcedure = "updContrmaster_statregt"
			.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updContrMasterStatregt = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdContrmaster_statregt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContrmaster_statregt = Nothing
		
updContrMasterStatregt_Err: 
		If Err.Number Then
			updContrMasterStatregt = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPreparedQuery: This routine prepare the instruction that it must execute according to
	'**%the data introduced by the user fo the condition
	'%insPreparedQuery: Esta rutina prepara la instrucción que de debe ejecutar según los datos
	'%puestos por el usuario para la condición.
	Public Function insPreparedQuery(Optional ByVal nNumber As String = "", Optional ByVal nType As String = "", Optional ByVal nBranch As String = "", Optional ByVal nCurrency As String = "", Optional ByVal dStartdate As Date = #12:00:00 AM#) As Boolean
		Dim lstrQuery As String
        Dim lexeConstruct As eRemoteDB.ConstructSelect = New eRemoteDB.ConstructSelect

        If lexeConstruct Is Nothing Then
			lexeConstruct = New eRemoteDB.ConstructSelect
		End If
		
		With lexeConstruct
			.SelectClause("NTYPE_REL, NNUMBER, NTYPE, DSTARTDATE, NBRANCH, NCURRENCY, SSTATREGT, DCOMPDATE, NUSERCODE, DEXPIRDAT, NCURR_PAY, SFORMPAY")
			.NameFatherTable("contrmaster", "cmas")
			
			If Trim(nNumber) <> String.Empty Then
				Call .WhereClause("cmas.nNumber", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, nNumber)
			End If
			
			If Trim(nType) <> String.Empty Then
				Call .WhereClause("cmas.nType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, nType, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If Trim(nBranch) <> String.Empty Then
				Call .WhereClause("cmas.nBranch", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, nBranch, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If Trim(nCurrency) <> String.Empty Then
				Call .WhereClause("cmas.nCurrency", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, nCurrency, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If dStartdate <> eRemoteDB.Constants.dtmNull Then
				Call .WhereClause("cmas.dStartdate", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, CStr(dStartdate))
			End If
			
			.OrderBy(" ORDER BY cmas.nNumber")
			
			lstrQuery = .Answer
			
			If InStr(1, lstrQuery, "WHERE cmas.dStartdate") Then
				lstrQuery = Replace(lstrQuery, "cmas.dStartdate = '" & Format(CStr(dStartdate), "yyyyMMdd") & "'", "cmas.dStartdate = " & "'" & CStr(dStartdate) & "'")
			Else
				lstrQuery = Replace(lstrQuery, "cmas.dStartdate = '" & Format(CStr(dStartdate), "yyyyMMdd") & "'", "AND cmas.dStartdate = " & "'" & CStr(dStartdate) & "'")
			End If
		End With
		'UPGRADE_NOTE: Object lexeConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeConstruct = Nothing
		
		'**+Execute the routine that read the reinsurance contracts
		'+Se ejecuta la rutina que lee los contratos de reaseguro
		If FindTreaties(lstrQuery) Then
			insPreparedQuery = True
		Else
			insPreparedQuery = False
		End If
	End Function
	
	'%InsUpdcontr_cescov: Se encarga de actualizar la tabla contrmaster
	Public Function insUpdcontrmaster(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdcontrmaster As eRemoteDB.Execute
		On Error GoTo insUpdcontrmaster_Err
		
		lrecinsUpdcontrmaster = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdcontrmaster al 05-04-2002 12:27:51
		'+
		With lrecinsUpdcontrmaster
			.StoredProcedure = "insUpdcontrmaster"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurr_pay", nCurr_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFormpay", sFormpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdcontrmaster = .Run(False)
		End With
		
insUpdcontrmaster_Err: 
		If Err.Number Then
			insUpdcontrmaster = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdcontrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdcontrmaster = Nothing
		On Error GoTo 0
	End Function
	
	'%updContrMasterExpirdat: Esta rutina se encarga de actualizar la fecha de expiración del contrato
	Public Function updContrMasterExpirdat(ByVal nNumber As Integer, ByVal dExpirdat As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdContrMasterExpirdat As eRemoteDB.Execute
		
		lrecupdContrMasterExpirdat = New eRemoteDB.Execute
		
		On Error GoTo updContrMasterExpirdat_Err
		
		With lrecupdContrMasterExpirdat
			.StoredProcedure = "updContrMaster_Expirdat"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updContrMasterExpirdat = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdContrMasterExpirdat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContrMasterExpirdat = Nothing
		
updContrMasterExpirdat_Err: 
		If Err.Number Then
			updContrMasterExpirdat = False
		End If
	End Function
End Class






