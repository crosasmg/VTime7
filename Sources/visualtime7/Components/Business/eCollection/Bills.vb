Option Strict Off
Option Explicit On
Public Class Bills
	'%-------------------------------------------------------%'
	'% $Workfile:: Bills.cls                                $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 2/03/04 10:18a                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.bills al 04-04-2002 09:27:39
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public sBillType As String ' CHAR       1    0     0    N
	Public sBilling As String ' CHAR       1    0     0    N
	Public nBillnum As Double ' NUMBER     22   0     10   N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public nAmount As Double ' NUMBER     22   2     10   N
	Public nAmo_afec As Double ' NUMBER     22   2     10   S
	Public nAmo_exen As Double ' NUMBER     22   2     10   S
	Public nIva As Double ' NUMBER     22   2     10   S
	Public nCre_note As Integer ' NUMBER     22   0     10   S
	Public nNewbill As Double ' NUMBER     22   0     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nBillstat As Integer ' NUMBER     22   0     5    N
	Public dStatdate As Date ' DATE       7    0     0    N
	
	'+ Declaración de variables públicas para la transacción CO700
	Public sKey As String
	Public nId As Integer
	Public sSel As String
	Public nCollecDocTyp As Integer
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nReceipt As Double
	Public nContrat As Double
	Public nDraft As Integer
	Public nBulletins As Double
	Public dExpirDat As Date
	Public nTransac As Integer
	Public nAgency As Integer
	
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nInsur_area As Integer, ByVal sBillType As String, ByVal nBillnum As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaBills As eRemoteDB.Execute
		
		On Error GoTo reaBills_Err
		
		If Me.nInsur_area = nInsur_area And Me.sBillType = sBillType And Me.nBillnum = nBillnum And Not lblnFind Then
			Find = True
		Else
			lrecReaBills = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaBills al 04-05-2002 16:50:07
			'+
			With lrecReaBills
				.StoredProcedure = "reaBills"
				.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBilltype", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBillnum", nBillnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Find = True
					Me.nInsur_area = .FieldToClass("nInsur_area")
					Me.sBillType = .FieldToClass("sBilltype")
					Me.sBilling = .FieldToClass("sBilling")
					Me.nBillnum = .FieldToClass("nBillnum")
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.sClient = .FieldToClass("sClient")
					Me.nAmount = .FieldToClass("nAmount")
					Me.nAmo_afec = .FieldToClass("nAmo_afec")
					Me.nAmo_exen = .FieldToClass("nAmo_exen")
					Me.nIva = .FieldToClass("nIva")
					Me.nCre_note = .FieldToClass("nCre_note")
					Me.nNewbill = .FieldToClass("nNewbill")
					Me.nBillstat = .FieldToClass("nBillstat")
					Me.dStatdate = .FieldToClass("dStatdate")
				Else
					Find = False
				End If
			End With
		End If
		
reaBills_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaBills may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaBills = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValCO700_K: Validaciones de la transacción(Header)
	Public Function InsValCO700_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nInsur_area As Integer, ByVal sDocType As String, ByVal sBillType As String, ByVal sProcess As String, ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal dDatePrint As Date, ByVal sMode As String, ByVal nBillnum As Double, ByVal sClient As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nAgency As Integer, ByVal dValDate As Date, Optional ByVal sIndAnticip As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsBills_num As eCollection.Bills_Num
		Dim lclsAddress As Object
		Dim lobjPolicy As Object '+ ePolicy.Policy
		Dim lobjClient As Object '+ eClient.Client
		Dim lblnError As Boolean
		Dim ldtmDate As Date
		Dim lintRecowner As Integer
		
		On Error GoTo InsValCO700_K_Err
		lclsErrors = New eFunctions.Errors
		lclsBills_num = New eCollection.Bills_Num
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		
		With lclsErrors
			'+ Se valida el Campo: Área de seguro.
			If nInsur_area <= 0 Then
				.ErrorMessage(sCodispl, 55031)
				lblnError = True
			End If
			
			If sBillType <> "3" Then
				
				If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
					
					'+ Se valida el Campo: Fecha inicial.
					If dDateIni = eRemoteDB.Constants.dtmNull Then
						.ErrorMessage(sCodispl, 9071)
						lblnError = True
					End If
					
					'+ Se valida el Campo: Fecha final.
					If dDateEnd = eRemoteDB.Constants.dtmNull Then
						.ErrorMessage(sCodispl, 9072)
						lblnError = True
					End If
					
					
					'+ Se valida que la Fecha final sea mayor que la fecha inicial.
					If dDateIni <> eRemoteDB.Constants.dtmNull And dDateEnd <> eRemoteDB.Constants.dtmNull Then
						If dDateIni > dDateEnd Then
							.ErrorMessage(sCodispl, 3240)
							lblnError = True
						End If
					End If
				End If
				
				'+ Se valida el Campo: Fecha de impresión.
				If dDatePrint = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 55546)
					lblnError = True
				Else
					If dDatePrint > Today Then
						.ErrorMessage(sCodispl, 55544)
						lblnError = True
					End If
				End If
			End If
			
			'+ Si la acción es consulta o el tipo de documento es nota.
			If nAction = eFunctions.Menues.TypeActions.clngActionQuery Or sDocType = "2" Then
				'+ Se valida el Campo: Número de documento.
				If nBillnum <= 0 Then
					.ErrorMessage(sCodispl, 55542)
					lblnError = True
				End If
			End If
			
			If Not lblnError Then
				'+ Si el tipo de factura es Afecta o exenta.
				If sBillType = "1" Or sBillType = "2" Then
					ldtmDate = lclsBills_num.getLastClosed(nInsur_area, sBillType)
					If ldtmDate <> eRemoteDB.Constants.dtmNull Then
						'+ La fecha de impresión debe estar dentro del período de facturación.
						If dDatePrint <= ldtmDate Then
							.ErrorMessage(sCodispl, 55545)
							lblnError = True
						End If
					End If
				End If
			End If
			
			If nPolicy > 0 Then
				'+Validacion del ramo comercial
				If nBranch <= 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 1022)
					lblnError = True
				End If
				
				'+Validacion del producto
				If nProduct <= 0 Then
					Call .ErrorMessage(sCodispl, 3635)
					lblnError = True
				End If
			End If
			
			'+ Validación del campo póliza
			If Not lblnError Then
				If nPolicy > 0 Then
					If Not lobjPolicy.Find("2", nBranch, nProduct, nPolicy) Then
						Call .ErrorMessage(sCodispl, 3001)
						lblnError = True
					End If
					
					If lobjPolicy.sStatus_pol = CollectionSeq.TypeStatus_Pol.cstrIncomplete Then
						Call .ErrorMessage(sCodispl, 3720)
						lblnError = True
					End If
					
				End If
			End If
			
			If Not lblnError Then
				'+ Si el proceso es puntual
				If sProcess = "1" Then
					'+ Debe indicarse o cliente o póliza.
					If nPolicy <= 0 And sClient = String.Empty Then
						.ErrorMessage(sCodispl, 55848)
						lblnError = True
					End If
				End If
			End If
			
			If Not lblnError Then
				'+ Si no se indicó ramo, producto, póliza y si cliente.
				If nPolicy <= 0 And nBranch <= 0 And nProduct <= 0 Then
					If sClient <> String.Empty Then
						'+ Se valida el cliente
						If lobjClient.ValidateClientStruc(sClient) Then
							If Not lobjClient.Find(sClient, True) Then
								.ErrorMessage(sCodispl, 1007)
								lblnError = True
							End If
						Else
							.ErrorMessage(sCodispl, 1007)
							lblnError = True
						End If
					End If
				End If
			End If
			
			'+ Se verifica de que la información a procesar tenga dirección de envio.
			If Not lblnError Then
				'+ Si el proceso no es puntual
				If sProcess = "1" Then
					lclsAddress = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Address")
					If nPolicy > 0 Then
						lintRecowner = 1
					Else
						If sClient <> String.Empty Then
							lintRecowner = 2
						End If
					End If
					If lintRecowner > 0 Then
						If Not lclsAddress.valAddress_send(lintRecowner, sCertype, nBranch, nProduct, nPolicy, 0, dDatePrint, sClient) Then
							.ErrorMessage(sCodispl, 60370,  , eFunctions.Errors.TextAlign.LeftAling, IIf(lintRecowner = 1, "Póliza: ", "Cliente: "))
							lblnError = True
						End If
					End If
					'UPGRADE_NOTE: Object lclsAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsAddress = Nothing
				End If
			End If
			
			'+ Si no se está consultando y el tipo de factura es proforma se valida la fecha de Valorización
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				If Not lblnError Then
					If sBillType = "3" Then
						If dValDate = eRemoteDB.Constants.dtmNull Then
							.ErrorMessage(sCodispl, 55527)
							lblnError = True
						End If
					End If
				End If
			End If
			
			'+ Se verifica que exista información para procesar
			If Not lblnError Then
				If Not valExistsCO700_K(nAction, nInsur_area, sDocType, sBillType, dDateIni, dDateEnd, nBillnum, sClient, sCertype, nBranch, nProduct, nPolicy, dValDate, sIndAnticip) Then
					.ErrorMessage(sCodispl, 1069)
					lblnError = True
				End If
			End If
			
			InsValCO700_K = .Confirm
		End With
		
InsValCO700_K_Err: 
		If Err.Number Then
			InsValCO700_K = "InsValCO700_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBills_num = Nothing
		
	End Function
	
	'%InsValCO700: Validaciones de la transacción(Folder)(CO700)
	Public Function InsValCO700A(ByVal sCodispl As String, ByVal sKey As String, ByVal nMainAction As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValCO700A_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Validar que se seleccione por lo menos un recibo
			If nMainAction = 301 Then
				If Not valExistsCO700(sKey, "1") Then
					.ErrorMessage(sCodispl, 55543)
				End If
			End If
			
			InsValCO700A = .Confirm
		End With
		
InsValCO700A_Err: 
		If Err.Number Then
			InsValCO700A = "InsValCO700A: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostCO700A: Ejecuta el post de la transacción (CO700)
	Public Function insPostCO700A(ByVal sKey As String, ByVal sProcess As String, ByVal sMode As String, ByVal dDatePrint As Date, ByVal sDocType As String, ByVal sBillType As String, ByVal nBillnum As Double, ByVal nPolicy As Double, ByVal sClient As String, ByVal nInsur_area As Integer, ByVal nUsercode As Integer, ByVal dValDate As Date) As Boolean
		Dim lrecinsUpdco700 As eRemoteDB.Execute
		
		
		On Error GoTo insPostCO700A_Err
		
		'+ Si es proforma (sBillType=3) se le asigna "4"
		If sBillType = "3" Then
			sBillType = "4"
		End If
		
		'+ Si el tipo de documento es nota de crédito (sDocType="2") se le asigna "3"
		If sDocType = "2" Then
			sBillType = "3"
		End If
		
		lrecinsUpdco700 = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdco700 al 05-04-2002 16:32:39
		'+
		With lrecinsUpdco700
			.StoredProcedure = "insUpdco700"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMode", sMode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateprint", dDatePrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBilltype", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBillnum", nBillnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO700A = .Run(False)
		End With
		
insPostCO700A_Err: 
		If Err.Number Then
			insPostCO700A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdco700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdco700 = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nInsur_area = eRemoteDB.Constants.intNull
		Me.sBillType = String.Empty
		Me.sBilling = String.Empty
		Me.nBillnum = eRemoteDB.Constants.intNull
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.sClient = String.Empty
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nAmo_afec = eRemoteDB.Constants.intNull
		Me.nAmo_exen = eRemoteDB.Constants.intNull
		Me.nIva = eRemoteDB.Constants.intNull
		Me.nCre_note = eRemoteDB.Constants.intNull
		Me.nNewbill = eRemoteDB.Constants.intNull
		Me.dCompdate = eRemoteDB.Constants.dtmNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nBillstat = eRemoteDB.Constants.intNull
		Me.dStatdate = eRemoteDB.Constants.dtmNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		
		Me.sKey = String.Empty
		Me.sSel = String.Empty
		Me.nCollecDocTyp = eRemoteDB.Constants.intNull
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.nPolicy = eRemoteDB.Constants.intNull
		Me.nReceipt = eRemoteDB.Constants.intNull
		Me.nContrat = eRemoteDB.Constants.intNull
		Me.nDraft = eRemoteDB.Constants.intNull
		Me.nBulletins = eRemoteDB.Constants.intNull
		Me.dExpirDat = eRemoteDB.Constants.dtmNull
		Me.nTransac = eRemoteDB.Constants.intNull
		Me.nAgency = eRemoteDB.Constants.intNull
		Me.nId = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%valExistsCO700_K: Verifica si existe información para procesar según condición de filtro de la transacción CO001_K.
	Public Function valExistsCO700_K(ByVal nAction As Integer, ByVal nInsur_area As Integer, ByVal sDocType As String, ByVal sBillType As String, ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal nBillnum As Double, ByVal sClient As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dValDate As Date, ByVal sIndAnticip As String) As Boolean
		Dim lrecvalExistsCO700_K As eRemoteDB.Execute
		
		On Error GoTo valExistsCO700_K_Err
		
		'+ Si es proforma (sBillType=3) se le asigna "4"
		If sBillType = "3" Then
			sBillType = "4"
		End If
		
		'+ Si el tipo de documento es nota de crédito (sDocType="2") se le asigna "3"
		If sDocType = "2" Then
			sBillType = "3"
		End If
		
		lrecvalExistsCO700_K = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valExistsCO700_K al 02-09-2002 14:31:52
		'+
		With lrecvalExistsCO700_K
			.StoredProcedure = "valExistsCO700_k"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBilltype", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBillnum", nBillnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SINDANTICIPADO", sIndAnticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nExists").Value = 1 Then
					valExistsCO700_K = True
					If .Parameters("sKey").Value <> String.Empty Then
						sKey = .Parameters("sKey").Value
					Else
						sKey = String.Empty
					End If
				End If
			End If
		End With
		
valExistsCO700_K_Err: 
		If Err.Number Then
			valExistsCO700_K = False
		End If
		'UPGRADE_NOTE: Object lrecvalExistsCO700_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalExistsCO700_K = Nothing
		On Error GoTo 0
	End Function
	
	'%valExistsCO700: Verifica si existe información para procesar según condición de filtro de la transacción CO001_K.
	Public Function valExistsCO700(ByVal sKey As String, ByVal sSel As String) As Boolean
		Dim lrecvalExistsCO700 As eRemoteDB.Execute
		
		On Error GoTo valExistsCO700_Err
		
		lrecvalExistsCO700 = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valExistsCO700 al 02-09-2002 14:31:52
		'+
		With lrecvalExistsCO700
			.StoredProcedure = "valExistsCO700"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExists").Value = 1 Then
					valExistsCO700 = True
				End If
			End If
		End With
		
valExistsCO700_Err: 
		If Err.Number Then
			valExistsCO700 = False
		End If
		'UPGRADE_NOTE: Object lrecvalExistsCO700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalExistsCO700 = Nothing
		On Error GoTo 0
	End Function
	
	'%insUpdTmp_CO700_sSel: Se actualiza la columna sSel de la tabla tmp_co700.
	Public Function insUpdTmp_CO700_sSel(ByVal sKey As String, ByVal nId As Integer, ByVal sSel As String) As Boolean
		Dim lrecTmp_co700_sSel As eRemoteDB.Execute
		
		On Error GoTo insUpdTmp_CO700_sSEL_Err
		
		lrecTmp_co700_sSel = New eRemoteDB.Execute
		
		With lrecTmp_co700_sSel
			.StoredProcedure = "updTmp_co700_ssel"
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdTmp_CO700_sSel = True
			End If
		End With
		
insUpdTmp_CO700_sSEL_Err: 
		If Err.Number Then
			insUpdTmp_CO700_sSel = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTmp_co700_sSel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTmp_co700_sSel = Nothing
	End Function
	
	'% Delete: Elimina los datos correspondientes para una consulta específica.
	Public Function Delete(ByVal sKey As String) As Boolean
		Dim ldelTmp_co700 As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		ldelTmp_co700 = New eRemoteDB.Execute
		
		With ldelTmp_co700
			.StoredProcedure = "delTmp_co700"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object ldelTmp_co700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelTmp_co700 = Nothing
	End Function
End Class






