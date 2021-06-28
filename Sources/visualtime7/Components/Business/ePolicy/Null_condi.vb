Option Strict Off
Option Explicit On
Public Class Null_condi
	'%-------------------------------------------------------%'
	'% $Workfile:: Null_condi.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 13/10/04 12.41                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according the table in the system on 01/15/2001
	'**+ The key fields are nBranch, nProduct, nNullcode y dEffecdate.
	'+ Propiedades según la tabla en el sistema al 15/01/2001.
	'+ Los campos llave de la tabla corresponden a: nBranch, nProduct, nNullcode y dEffecdate.
	'+ 12-21-2001: Se agrega propiedad sNotrehab, "No permite rehabilitar"
	
	'   Column_name                    Type      Computed Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nBranch As Integer 'smallint     no       2           5     0     no           (n/a)                (n/a)
	Public nProduct As Integer 'smallint     no       2           5     0     no           (n/a)                (n/a)
	Public nNullcode As Integer 'smallint     no       2           5     0     no           (n/a)                (n/a)
	Public dEffecdate As Date 'datetime     no       8                       no           (n/a)                (n/a)
	Public nAmelevel As Integer 'smallint     no       2           5     0     yes          (n/a)                (n/a)
	Public sRegtypen As String 'char         no       1                       yes          no                   yes
	Public sReturn_ind As String 'char         no       1                       yes          no                   yes
	Public nReturn_Rat As Double 'decimal      no       5           5     2     yes          (n/a)                (n/a)
	Public sStatregt As String 'char         no       1                       yes          no                   yes
	Public nUsercode As Integer 'smallint     no       2           5     0     yes          (n/a)                (n/a)
	Public dNulldate As Date 'datetime     no       8                       yes          (n/a)                (n/a)
	Public sNotrehab As String 'char         no       1           0     0     no
	
	'**- Auxiliary variables
	'- Variables Auxiliares
	
	Public sCertype As String
	Public nPolicy As Double
	Public nCertif As Double
	Public nTransactio As Integer
	Public nTypemove As Integer
	Public dReahdate As Date
	Public nNullcode_pre As Integer
	Public nNullcode_pol As Integer
	Public nFlag As Integer
	Public nCount As Integer
	Public nRole As Integer
	Public sClient As String
	Public sCliename As String
	Public nAgency As Integer
	
	'**- Auxiliary property to be use in the page CA033
	'- Propiedad auxiliar utilizada en la página CA33
	
	Public nIntermCode As Integer
	Public sIntermName As String
	Public sBenefExist As String
	Public nPolTransactio As Integer
	Public sBrancht As String
	Public sCancelOutPre As String
	
	Private mblnOptExecuteEnable As Boolean
	Private mblnOptProcessEnable As Boolean
	
	Private mintOptExecuteValue As Integer
	Private mintOptProcessValue As Integer
	
	'% Find: Busca un registro en Null_condi segun su llave
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNullcode As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaNull_condi As eRemoteDB.Execute
		
		On Error GoTo reaNull_condi_Err
		
		lrecreaNull_condi = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaNull_condi al 12-21-2001 10:29:58
		With lrecreaNull_condi
			.StoredProcedure = "reaNull_condi"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nNullcode = nNullcode
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nAmelevel = .FieldToClass("nAmelevel")
				Me.sRegtypen = .FieldToClass("sRegtypen")
				Me.sReturn_ind = .FieldToClass("sReturn_ind")
				Me.nReturn_Rat = .FieldToClass("nReturn_rat")
				Me.sNotrehab = .FieldToClass("sNotrehab")
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaNull_condi_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaNull_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNull_condi = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindClientName: Returns the name of the client
	'% FindClientName: Devuelve el nombre del cliente según clave
	'------------------------------------------------------------
	Public Function FindClientName(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition. lrecreaRoles_a_name
		'- Se define la variable lrecreaRoles_a_name
		
		Dim lrecreaRoles_a_name As eRemoteDB.Execute
		lrecreaRoles_a_name = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaRoles_a_name'
		'**+Data of 01/04/2001 17:00:50
		'+ Definición de parámetros para stored procedure 'insudb.reaRoles_a_name'
		'+ Información leída el 04/01/2001 17:00:50
		
		On Error GoTo FindClientName_Err
		
		With lrecreaRoles_a_name
			.StoredProcedure = "reaRoles_a_name"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sClient = .FieldToClass("sClient")
				sCliename = .FieldToClass("sCliename")
				.RCloseRec()
				
				FindClientName = True
			Else
				FindClientName = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaRoles_a_name may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRoles_a_name = Nothing
		
FindClientName_Err: 
		If Err.Number Then
			FindClientName = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindToCA033: Returns the data of a record of the table of "Annulment conditions of the policy/certificate"
	'**% (Null_condi), this information is going to be used in the window CA033
	'% FindToCA033: Devuelve información de un registro de la tabla de Condiciones de Anulación
	'% de pólizas o certificados (Null_condi), para ser utilizado en la forma CA033
	Public Function FindToCA033(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nNullcode As Integer) As Boolean
		
		'**- Variable defintion. lrecreaNull_CondiCA033
		'- Se define la variable lrecreaNull_CondiCA033
		
		Dim lrecreaNull_CondiCA033 As eRemoteDB.Execute
		lrecreaNull_CondiCA033 = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaNull_CondiCA033'
		'**+Data of 01/04/2001 9:13:29
		'+ Definición de parámetros para stored procedure 'insudb.reaNull_CondiCA033'
		'+ Información leída el 04/01/2001 9:13:29
		
		On Error GoTo FindToCA033_Err
		
		With lrecreaNull_CondiCA033
			.StoredProcedure = "reaNull_CondiCA033"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nAmelevel = .FieldToClass("nAmelevel")
				sRegtypen = .FieldToClass("sRegtypen")
				sReturn_ind = .FieldToClass("sReturn_ind")
				nReturn_Rat = .FieldToClass("nReturn_rat")
				sStatregt = .FieldToClass("sStatregt")
				dNulldate = .FieldToClass("dNulldate")
				
				.RCloseRec()
				FindToCA033 = True
			Else
				FindToCA033 = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaNull_CondiCA033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNull_CondiCA033 = Nothing
		
FindToCA033_Err: 
		If Err.Number Then
			FindToCA033 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdatePremium: This routine anulls the pending premium invoices of the policy or certificate
	'% UpdatePremium: Anula los recibos pendientes de la póliza o certificado
	Public Function UpdatePremium() As Boolean
		
		'**- Variable definition. lrecupdPremiunpre
		'- Se define la variable lrecupdPremiunpre
		
		Dim lrecupdPremiunpre As eRemoteDB.Execute
		lrecupdPremiunpre = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.updPremiunpre'
		'**+Data of 01/04/2001 14:17:11
		'+ Definición de parámetros para stored procedure 'insudb.updPremiunpre'
		'+ Información leída el 04/01/2001 14:17:11
		
		On Error GoTo UpdatePremium_Err
		
		With lrecupdPremiunpre
			.StoredProcedure = "updPremiunpre"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode_pre", nNullcode_pre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode_pol", nNullcode_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFlag", nFlag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdatePremium = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdPremiunpre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPremiunpre = Nothing
		
UpdatePremium_Err: 
		If Err.Number Then
			UpdatePremium = False
		End If
		On Error GoTo 0
	End Function
	
	'**% valPendReceipt: This routine validates if there are pending premium invoices in the policy
	'% valPendReceipt: Valida si existen recibos pendientes en la póliza
	Public Function valPendReceipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCount As Integer) As Boolean
		
		'**- Variable definition. lrecreaCountReceiptPend
		'- Se define la variable lrecreaCountReceiptPend
		
		Dim lrecreaCountReceiptPend As eRemoteDB.Execute
		lrecreaCountReceiptPend = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaCountReceiptPend'
		'**+Data of 01/04/2001 15:57:19
		'+ Definición de parámetros para stored procedure 'insudb.reaCountReceiptPend'
		'+ Información leída el 04/01/2001 15:57:19
		
		On Error GoTo valPendReceipt_Err
		
		With lrecreaCountReceiptPend
			.StoredProcedure = "reaCountReceiptPend"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Me.nCount = .Parameters.Item("nCount").Value
				If Me.nCount > 0 Then
					valPendReceipt = True
				Else
					valPendReceipt = False
				End If
			Else
				valPendReceipt = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCountReceiptPend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCountReceiptPend = Nothing
		
valPendReceipt_Err: 
		If Err.Number Then
			valPendReceipt = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdPolicyCA033: Updates the information of the main table in the transaction
	'% UpdPolicyCA033: Actualiza la información en tratamiento de la tabla principal para la transacción.
	Public Function UpdPolicyCA033(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nCertif As Double, ByVal nTransacion As Integer, ByVal nUsercode As Integer, ByVal strChkNullReceipt As String, ByVal nAgency As Integer) As Boolean
		Dim lclsNull_condi As Null_condi
		Dim lclsPolicy_his As Policy_his
		On Error GoTo UpdPolicyCA033_Err
		lclsNull_condi = New Null_condi
		lclsPolicy_his = New Policy_his
		
		UpdPolicyCA033 = True
		
		With lclsPolicy_his
			.sCertype = "2"
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nNullcode = nNullcode
			.dNulldate = dNulldate
			.nCertif = nCertif
			.nCertificat = nCertif
			.nTransactio = nTransacion
			.nUsercode = nUsercode
			.nAgency = nAgency
			If nCertif = 0 Then
				.nType = 29
			Else
				.nType = 30
			End If
			.dReahdate = dNulldate
		End With
		
		If lclsPolicy_his.Update_PolCerti Then
			With lclsNull_condi
				.sCertype = "2"
				.nBranch = nBranch
				.nProduct = nProduct
				.nPolicy = nPolicy
				.nNullcode_pre = 12
				.nNullcode_pol = nNullcode
				.dNulldate = dNulldate
				.nCertif = nCertif
				.nUsercode = nUsercode
				.nFlag = 0
				If strChkNullReceipt = "1" Then
					.nFlag = 1
				End If
				.UpdatePremium()
			End With
		End If
		
UpdPolicyCA033_Err: 
		If Err.Number Then
			UpdPolicyCA033 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsNull_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNull_condi = Nothing
	End Function
	
	
	'**%insPreCA033:  Gets the necessary information for the window handling
	'%insPreCA033:Permite obtener la información de necesaria para el manejo de la ventana
	Public Function insPreCA033(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lintRole As Integer
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsClient As eClient.Client
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsProduct As eProduct.Product
		Dim lclsBeneficiar As ePolicy.Beneficiar
		
		On Error GoTo insPreCA033_Err
		lclsIntermedia = New eAgent.Intermedia
		lclsClient = New eClient.Client
		lclsPolicy = New ePolicy.Policy
		lclsBeneficiar = New ePolicy.Beneficiar
		lclsProduct = New eProduct.Product
		
		If lclsProduct.FindProdMaster(nBranch, nProduct) Then
			sBrancht = CStr(lclsProduct.sBrancht)
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
		If sBrancht = "1" Then
			nNullcode = 72
			sCancelOutPre = "1"
		Else
			nNullcode = eRemoteDB.Constants.intNull
			sCancelOutPre = "2"
		End If
		
		Call lclsPolicy.Find("2", nBranch, nProduct, nPolicy)
		
		nPolTransactio = lclsPolicy.nTransactio
		
		If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
			If lclsIntermedia.Find(lclsPolicy.nIntermed) Then
				nIntermCode = lclsPolicy.nIntermed
				If lclsClient.FindClientName(lclsIntermedia.sClient) Then
					lclsIntermedia.sCliename = lclsClient.sCliename
				Else
					lclsIntermedia.sCliename = String.Empty
				End If
				sIntermName = lclsIntermedia.sCliename
			End If
		End If
		
		'**+ It searches the client of the policy
		'+ Se busca el cliente de la póliza
		If lclsPolicy.sPolitype = "1" Or nCertif <> 0 Then
			lintRole = 2
		Else
			lintRole = 1
		End If
		
		Call FindClientName("2", nBranch, nProduct, nPolicy, nCertif, lintRole, lclsPolicy.dStartdate)
		
		'**+ It verifires if the policy has beneficiaries
		'+ Se verifica si la póliza tiene beneficiarios
		
		If lclsBeneficiar.valExist("2", nBranch, nProduct, nPolicy, nCertif, lclsPolicy.dStartdate, "0") Then
			sBenefExist = "1"
		Else
			sBenefExist = "0"
		End If
		
insPreCA033_Err: 
		If Err.Number Then
			insPreCA033 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBeneficiar = Nothing
	End Function
	
	'% insPreCA033_K: Permite obtener la información de necesaria para el manejo de la ventana
	Public Sub insPreCA033_k(ByVal sCodispl As String, ByVal sOperat As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
		Dim lclsRequest As Request
		
		mblnOptExecuteEnable = sCodispl <> "CA767"
		mblnOptProcessEnable = sCodispl <> "CA767"
		
		mintOptExecuteValue = 1
		mintOptProcessValue = 2
		
		If sCodispl = "CA034" Then
			mintOptExecuteValue = 2 ' Definitva se coloca por Defecto para la rehabilitacion
		End If
		
		If sCodispl = "CA767" Then
			If sOperat = "5" Then ' Actualizar
				mintOptExecuteValue = 1 ' Preliminar
			Else
				mintOptExecuteValue = 2 ' Definitiva
			End If
			
			lclsRequest = New Request
			If lclsRequest.Find("8", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				nAgency = lclsRequest.nAgency
			End If
		End If
		'UPGRADE_NOTE: Object lclsRequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRequest = Nothing
	End Sub

    '% DefaultValueCA033: Retorna valores por defecto de la transaccion CA033
    Public Function DefaultValueCA033(ByVal strKey As String) As Object
        Dim caseAux As Object = New Object
        Select Case strKey
            Case "optExecutePre"
                caseAux = IIf(mintOptExecuteValue = 1, "1", "2")
            Case "optExecuteDef"
                caseAux = IIf(mintOptExecuteValue = 2, "1", "2")

            Case "optProcRehab"
                caseAux = IIf(mintOptProcessValue = 1, "1", "2")
            Case "optProcReact"
                caseAux = IIf(mintOptProcessValue = 2, "1", "2")

            Case "optExecuteEnabled"
                caseAux = mblnOptExecuteEnable
            Case "optProcessEnabled"
                caseAux = mblnOptProcessEnable
        End Select
        Return caseAux
    End Function

    '* Class_Initialize: se inicializan las propiedades de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
		nAgency = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






