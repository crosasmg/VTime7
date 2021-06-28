Option Strict Off
Option Explicit On
Public Class Tab_bill_i
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_bill_i.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system 04/03/2001
	'**- The key fields correspond to the properties: nBranch , nProduct, nBill_item, dEffecdate
	'- Propiedades según la tabla en el sistema al 03/04/2001.
	'- Los campos llaves coresponden a las propiedades: nBranch , nProduct, nBill_item, dEffecdate
	
	'   Column_name                        Type         Computed   Length  Prec  Scale  Nullable   TrimTrailingBlanks    FixedLenNullInSource
	Public nBranch As Integer 'smallint    no         2           5     0     no         (n/a)                     (n/a)
	Public nProduct As Integer 'smallint    no         2           5     0     no         (n/a)                     (n/a)
	Public nBill_item As Integer 'smallint    no         2           5     0     no         (n/a)                     (n/a)
	Public dEffecdate As Date 'datetime    no         8                       no         (n/a)                     (n/a)
	Public sDescript As String 'char        no         30                      yes         no                        yes
	Public dNulldate As Date 'datetime    no         8                       yes        (n/a)                     (n/a)
	Public sShort_des As String 'char        no         12                      yes         no                        yes
	Public nUsercode As Integer 'smallint    no         2           5     0     yes        (n/a)                     (n/a)
	
	'**- Auxiliary properties
	'- Variables auxiliares
	Public nAction As Integer
	
	'**% Find_BillItem: Permit to charge the information related to the concept
	'**% of invoice of one product
	'% Find_BillItem:Permite cargar la información relacionada con el concepto
	'% de facturación de un producto
	Public Function Find_BillItem(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nBill_item As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTab_bill_i_2 As eRemoteDB.Execute
		
		On Error GoTo Find_BillItem_Err
		
		lrecreaTab_bill_i_2 = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaTab_bill_i_2'
		'**+Data read on 04/03/2001 14:09:05
		'+Definición de parámetros para stored procedure 'insudb.reaTab_bill_i_2'
		'+Información leída el 03/04/2001 14:09:05
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nBill_item <> nBill_item Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nBill_item = nBill_item
			Me.dEffecdate = dEffecdate
			
			With lrecreaTab_bill_i_2
				.StoredProcedure = "reaTab_bill_i_2"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sDescript = .FieldToClass("sDescript")
					dNulldate = .FieldToClass("dNulldate")
					sShort_des = .FieldToClass("sShort_des")
					Find_BillItem = True
					.RCloseRec()
				Else
					Find_BillItem = False
				End If
			End With
		Else
			Find_BillItem = True
		End If
		
Find_BillItem_Err: 
		If Err.Number Then
			Find_BillItem = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_bill_i_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_bill_i_2 = Nothing
	End Function
	
	'**% ValAssociate: verify if a record is associated to a one charge
	'**% or a one coverage
	'% ValAssociate:Verifica si un determinado concepto esta asociado a un recargo
	'% o una cobertura
	Public Function ValAssociate(ByVal nProdType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nBill_item As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalTabBill_iAssociate As eRemoteDB.Execute
		
		On Error GoTo ValAssociate_Err
		
		lrecvalTabBill_iAssociate = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.valTabBill_iAssociate'
		'**+Data read on 04/03/2001 14:25:04
		'+Definición de parámetros para stored procedure 'insudb.valTabBill_iAssociate'
		'+Información leída el 03/04/2001 14:25:04
		
		With lrecvalTabBill_iAssociate
			.StoredProcedure = "valTabBill_iAssociate"
			.Parameters.Add("nProdType", nProdType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValAssociate = IIf(.FieldToClass("lCount") > 0, True, False)
				.RCloseRec()
			End If
		End With
		
ValAssociate_Err: 
		If Err.Number Then
			ValAssociate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalTabBill_iAssociate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalTabBill_iAssociate = Nothing
	End Function
	
	'**%Update: Updates records in the table "Tab_bill_i".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_bill_i". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecinsTab_bill_i As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsTab_bill_i = New eRemoteDB.Execute
		
		'**+parameters definition for the stored procedure 'insudb.insTab_bill_i'
		'**+Data read on 04/03/2001 16:02:49
		'+Definición de parámetros para stored procedure 'insudb.insTab_bill_i'
		'+Información leída el 03/04/2001 16:02:49
		
		With lrecinsTab_bill_i
			.StoredProcedure = "insTab_bill_i"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsTab_bill_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_bill_i = Nothing
	End Function
	
	'% InsValDP011: Este metodo se encarga de realizar las validaciones de la ventana "DP011"
	Public Function insValDP011(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBill_item As Integer, ByVal sDescript As String, ByVal sShort_des As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP011_Err
		
		lobjErrors = New eFunctions.Errors
		
		'**+Validate the column 1: Concept code
		'+Se valida la columna 1: Código del Concepto.
		If nBill_item = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP011", 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Concepto: ")
		Else
			If sAction = "Add" Then
				If Find_BillItem(nBranch, nProduct, nBill_item, dEffecdate) Then
					Call lobjErrors.ErrorMessage("DP011", 1926,  , eFunctions.Errors.TextAlign.LeftAling, "Concepto: ")
				End If
			End If
		End If
		
		'**+ Validate the column 2: Concept description
		'+ Se valida la columna 2: Descripción del Concepto.
		If sDescript = String.Empty Then
			Call lobjErrors.ErrorMessage("DP011", 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Descripción: ")
		End If
		
		'**+ Validate the column 3: Concept description abreviated
		'+ Se valida la columna 3: Descripción Abreviada del Concepto.
		If sShort_des = String.Empty Then
			Call lobjErrors.ErrorMessage("DP011", 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Descripción abreviada: ")
		End If
		
		insValDP011 = lobjErrors.Confirm
		
insValDP011_Err: 
		If Err.Number Then
			insValDP011 = "insValDP011: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'**%insPostDP011: Updates the database (as described in the functional specifications)
	'**%for the page "DP011"
	'%insPostDP011: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP011"
	Public Function insPostDP011(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBill_item As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As Prod_win
		Dim lcolTab_bill_is As Tab_bill_is
		
		On Error GoTo insPostDP011_Err
		
		lclsProd_win = New Prod_win
		lcolTab_bill_is = New Tab_bill_is
		
		With Me
			.nAction = IIf(sAction = "Del", 2, 1)
			.nBranch = nBranch
			.nProduct = nProduct
			.nBill_item = nBill_item
			.dEffecdate = dEffecdate
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nUsercode = nUsercode
			insPostDP011 = .Update
			If insPostDP011 Then
				If lcolTab_bill_is.Find(nBranch, nProduct, dEffecdate) Then
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
				Else
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "1", nUsercode)
				End If
			End If
		End With
		
insPostDP011_Err: 
		If Err.Number Then
			insPostDP011 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lcolTab_bill_is may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_bill_is = Nothing
	End Function
	
	'% AddDefaultValue: se crean los conceptos de facturación en base a la tabla general
	Public Function AddDefaultValue(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsTab_bill_i As eRemoteDB.Execute
		
		On Error GoTo AddDefaultValue_Err
		
		lrecinsTab_bill_i = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.inscreTab_bill_i'
		'+Información leída el 23/05/2002
		
		With lrecinsTab_bill_i
			.StoredProcedure = "inscreTab_bill_i"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddDefaultValue = .Run(False)
		End With
		
AddDefaultValue_Err: 
		If Err.Number Then
			AddDefaultValue = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsTab_bill_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_bill_i = Nothing
	End Function
End Class






