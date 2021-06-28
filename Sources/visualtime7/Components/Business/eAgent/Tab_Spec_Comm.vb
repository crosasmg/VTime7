Option Strict Off
Option Explicit On
Public Class Tab_Spec_Comm
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Spec_Comm.cls                        $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 5/07/04 10:28p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 12/03/2002.
	'+ El campo llave corresponde a nBranch, nProduct, dEffecdate, nSic_Tab_nr.
	
	'+ Column_name              Type       Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ---------- ------ ---- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nSlc_Tab_nr As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nType_comm As Integer
	Public nPolicy_year_ini As Integer
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public nId As Integer
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	Public nCommiss_Pct As Double 'decimal  5      10    2     yes      (n/a)              (n/a)
	Public nUserCode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nPolicy_year_end As Integer
	Public nCover As Integer
	Public nModulec As Integer
	Public nCurrency As Double
	Public nMax_Amount As Double
	Public nTypetable As Integer
	Public nAge_init As Integer
	Public nAge_end As Integer
	
	'-Propiedades auxiliares
	Private bMin_year As Boolean
	Private bMax_year As Boolean
	'
	
	'% Add: Crea un registro en la tabla Tab_Spec_Comm.
	Public Function Add() As Boolean
		Add = insUpdTab_Spec_Comm(1)
	End Function
	
	'% Update: Actualiza los datos de la tabla Tab_Spec_Comm.
	Public Function Update() As Boolean
		Update = insUpdTab_Spec_Comm(2)
	End Function
	
	'**% Delete: Delete information in the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Delete = insUpdTab_Spec_Comm(3)
	End Function
	
	'% insUpdTab_Spec_Comm: Esta función permite actualizar la tabla Tab_Spec_Comm.
	Public Function insUpdTab_Spec_Comm(ByVal nAction As Integer) As Boolean
		Dim lclsTab_Spec_Comm As eRemoteDB.Execute
		
		lclsTab_Spec_Comm = New eRemoteDB.Execute
		
		On Error GoTo insUpdTab_Spec_Comm_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdTab_Goals'. Generated on 17/01/2002 09:51:46 a.m.
		
		With lclsTab_Spec_Comm
			.StoredProcedure = "insUpdTab_Spec_Comm"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSlc_Tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommiss_Pct", nCommiss_Pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_year_ini", nPolicy_year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_year_end", nPolicy_year_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_comm", nType_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Amount", nMax_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypetable", nTypetable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			insUpdTab_Spec_Comm = .Run(False)
		End With
		
insUpdTab_Spec_Comm_Err: 
		If Err.Number Then
			insUpdTab_Spec_Comm = False
		End If
		
		'UPGRADE_NOTE: Object lclsTab_Spec_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Spec_Comm = Nothing
		On Error GoTo 0
	End Function
	
	'% IsExist: Función que verifica si existe el registro duplicado en la tabla 'Tab_Spec_Comm'.
	Public Function IsExist(ByVal nSlc_Tab_nr As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType_comm As Integer, ByVal nPolicy_year_ini As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lclsTab_Spec_Comm As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		
		lclsTab_Spec_Comm = New eRemoteDB.Execute
		
		'+ Define todos los parámetros para el Stored Procedures 'reaTab_Spec_CommExist'.
		
		With lclsTab_Spec_Comm
			.StoredProcedure = "reaTab_Spec_CommExist"
			.Parameters.Add("nSlc_Tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
			.Parameters.Add("nType_comm", nType_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_year_ini", nPolicy_year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nSlc_Tab_nr = nSlc_Tab_nr
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nCommiss_Pct = .FieldToClass("nCommiss_Pct")
				Me.nUserCode = .FieldToClass("nUsercode")
				dNulldate = .FieldToClass("dNulldate")
				
				'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
				Me.nPolicy_year_ini = nPolicy_year_ini
				nPolicy_year_end = .FieldToClass("nPolicy_year_end")
				nModulec = .FieldToClass("nModulec")
				nCover = .FieldToClass("nCover")
				Me.nType_comm = nType_comm
				.RCloseRec()
				IsExist = True
			Else
				IsExist = False
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_Spec_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Spec_Comm = Nothing
	End Function
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'% InsValMAG7000_k: Validación de los datos del encabezado de la página MAG7000.
	Public Function InsValMAG7000_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSlc_Tab_nr As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lclsProduct As eProduct.Product
		Dim lvalField As eFunctions.valField
		Dim ldtmMaxEffecDate As Date
		
		On Error GoTo InsValMAG7000_K_Err
		
		lclsErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lclsProduct = New eProduct.Product
		lvalField = New eFunctions.valField
		
		'**+ The validations of the field "Line of business" are performed.
		'+ Se realizan las validaciones del campo "Ramo".
		
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'**+ The validations of the field "Product" are performed.
		'+ Se realizan las validaciones del campo "Producto".
		
		If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1014)
		Else
			lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
				Call lclsErrors.ErrorMessage(sCodispl, 9066)
			Else
				
				'**+ Validate that the product corresponds to life or combined.
				'+ Se valida que el producto corresponda a vida o combinado.
				
				With lclsProduct
					Call .insValProdMaster(nBranch, nProduct)
					
					If .blnError Then
						If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
							Call lclsErrors.ErrorMessage(sCodispl, 3987)
						End If
					End If
				End With
			End If
		End If
		
		lvalField.objErr = lclsErrors
		
		With lvalField
			.ErrEmpty = 2056
			.ErrInvalid = 7114
			
			If .ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.ValAll) Then
				If nAction <> 401 Then
					If dEffecdate <= Today Then
						Call lclsErrors.ErrorMessage(sCodispl, 70095)
					Else
						ldtmMaxEffecDate = ReaMax_dEffecdate(nBranch, nProduct, nSlc_Tab_nr)
						If dEffecdate < ldtmMaxEffecDate Then
							Call lclsErrors.ErrorMessage(sCodispl, 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmMaxEffecDate & ")")
						End If
					End If
				End If
			End If
		End With
		
		'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
		'+ Tabla: Debe estar llena.
		
		If nSlc_Tab_nr = eRemoteDB.Constants.intNull Or nSlc_Tab_nr = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70093)
		End If
		
		InsValMAG7000_K = lclsErrors.Confirm
		
InsValMAG7000_K_Err: 
		If Err.Number Then
			InsValMAG7000_K = InsValMAG7000_K & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
	End Function
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'% insValMAG7000: Función que realiza la validacion de los datos introducidos en la ventana.
	Public Function insValMAG7000(ByVal sCodispl As String, ByVal sAction As String, ByVal nSlc_Tab_nr As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCommiss_Pct As Double, ByVal nPolicy_year_ini As Integer, ByVal nPolicy_year_end As Integer, ByVal nPolicy_year_end_Aux As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType_comm As Integer, ByVal sExist_Modul As String, ByVal nCurrency As Double, ByVal nMax_Amount As Double, ByVal nTypetable As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo insValMAG7000_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		'+ Comisión: Debe estar lleno.
		
		If nCommiss_Pct = 0 Or nCommiss_Pct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70110)
		End If
		
		'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
		'+ La combinación Tabla, Fecha, Ramo, Producto, Tipo de Comisión y Año inicial  no debe estar repetido.
		
		If sAction = "Add" Then
			If (nSlc_Tab_nr <> 0 And nSlc_Tab_nr <> eRemoteDB.Constants.intNull) Then
				If IsExist(nSlc_Tab_nr, dEffecdate, nBranch, nProduct, nType_comm, nPolicy_year_ini, nModulec, nCover) Then
					Call lclsErrors.ErrorMessage(sCodispl, 70111)
				End If
			End If
		End If
		
		'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
		
		If nType_comm = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70171)
		End If
		
		' Validaciones por años poliza
		If nTypetable = 1 Then
			If nPolicy_year_ini <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 70172)
			End If
			
			If nPolicy_year_end <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 70168)
			End If
			
			If nPolicy_year_ini > nPolicy_year_end Then
				Call lclsErrors.ErrorMessage(sCodispl, 70169)
			End If
		Else
			If nPolicy_year_ini <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 80065)
			End If
			
			If nPolicy_year_end <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 80066)
			End If
			
			If nPolicy_year_ini > nPolicy_year_end Then
				Call lclsErrors.ErrorMessage(sCodispl, 80067)
			End If
			
		End If
		'+ Se aplican validaciones relacionadas con el modulo y
		'+la cobertura solo si el tipo de comisión es "Costo-cobertura"
		If nType_comm = 1 Then
			If sExist_Modul = "1" Then
				If nModulec = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 11296)
				End If
			End If
			
			If nCover = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3552)
			End If
		End If
		
		Call Val_Range(nBranch, nProduct, nSlc_Tab_nr, nPolicy_year_ini, nPolicy_year_end, nType_comm, dEffecdate, nModulec, nCover)
		If sAction <> "Update" Then
			If bMin_year Then
				' Validaciones por años poliza
				If nTypetable = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 70167)
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 80068)
				End If
			End If
		End If
		
		If nPolicy_year_end <> nPolicy_year_end_Aux Then
			If bMax_year Then
				If nTypetable = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 70158)
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 80069)
				End If
			End If
		End If
		
		' Si el tipo de comisión es por vigencia y edad actuarial
		If nTypetable = 3 Then
			If nAge_init = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 11109)
			Else
				If nAge_init < 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55573)
				End If
			End If
			
			If nAge_end = eRemoteDB.Constants.intNull Or nAge_end <= nAge_init Then
				Call lclsErrors.ErrorMessage(sCodispl, 55574)
			End If
		End If
		
		If nMax_Amount <> 0 And nMax_Amount <> eRemoteDB.Constants.intNull Then
			If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 12110)
			End If
		End If
		
		insValMAG7000 = lclsErrors.Confirm
		
insValMAG7000_Err: 
		If Err.Number Then
			insValMAG7000 = insValMAG7000 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'% insPostMAG7000: Función que realiza la actualización de la tabla en tratamiento.
	Public Function insPostMAG7000(ByVal sAction As String, ByVal nSlc_Tab_nr As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCommiss_Pct As Double, ByVal nUserCode As Integer, ByVal nPolicy_year_ini As Integer, ByVal nPolicy_year_end As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType_comm As Integer, ByVal nId As Integer, ByVal nCurrency As Double, ByVal nMax_Amount As Double, ByVal nTypetable As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer) As Boolean
		On Error GoTo insPostMAG7000_Err
		
		With Me
			.nSlc_Tab_nr = nSlc_Tab_nr
			.dEffecdate = dEffecdate
			.nBranch = nBranch
			.nProduct = nProduct
			.nCommiss_Pct = nCommiss_Pct
			.nUserCode = nUserCode
			.nPolicy_year_ini = nPolicy_year_ini
			.nPolicy_year_end = nPolicy_year_end
			.nModulec = nModulec
			.nCover = nCover
			.nType_comm = nType_comm
			.nId = nId
			.nCurrency = nCurrency
			.nMax_Amount = nMax_Amount
			.nTypetable = nTypetable
			.nAge_init = nAge_init
			.nAge_end = nAge_end
		End With
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG7000 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG7000 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG7000 = Delete()
		End Select
		
insPostMAG7000_Err: 
		If Err.Number Then
			insPostMAG7000 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'% ReaMax_dEffecdate: Función que Retorna la maxima fecha de efecto por ramo producto de la tabla Tab_Spec_Comm
	Private Function ReaMax_dEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSlc_Tab_nr As Integer) As Date
		Dim lclsReaMaxTab_Spec_CommDate As eRemoteDB.Execute
		
		On Error GoTo ReaMax_dEffecdate_Err
		
		lclsReaMaxTab_Spec_CommDate = New eRemoteDB.Execute
		
		'+ Define todos los parámetros para el Stored Procedures 'reaTab_Spec_CommExist'.
		
		With lclsReaMaxTab_Spec_CommDate
			.StoredProcedure = "ReaMaxTab_Spec_CommDate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSlc_Tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMaxDate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				ReaMax_dEffecdate = .Parameters("dMaxDate").Value
			Else
				ReaMax_dEffecdate = dtmNull
			End If
		End With
		
ReaMax_dEffecdate_Err: 
		If Err.Number Then
			ReaMax_dEffecdate = dtmNull
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsReaMaxTab_Spec_CommDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReaMaxTab_Spec_CommDate = Nothing
	End Function
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'%Val_Range: Validacion del rango de los años de vigencia
	Private Sub Val_Range(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSlc_Tab_nr As Integer, ByVal nPolicy_year_ini As Integer, ByVal nPolicy_year_end As Integer, ByVal nType_comm As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer)
		Dim lrecval_TabSpecComm_Range As eRemoteDB.Execute
		On Error GoTo val_t_apv_w_range_Err
		
		lrecval_TabSpecComm_Range = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'val_TabSpecComm_Range'
		'**+ The Information was read on  10/09/2003
		
		'+ Definición de parámetros para stored procedure 'val_TabSpecComm_Range'
		'+ Información leída el: 10/09/2003
		
		With lrecval_TabSpecComm_Range
			.StoredProcedure = "val_TabSpecComm_Range"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSlc_tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_comm", nType_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_year_ini", nPolicy_year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_year_end", nPolicy_year_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_year_out", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_year_out", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				bMin_year = IIf(.Parameters.Item("nMin_year_out").Value = 1, True, False)
				bMax_year = IIf(.Parameters.Item("nMax_year_out").Value = 1, True, False)
			End If
		End With
val_t_apv_w_range_Err: 
		'UPGRADE_NOTE: Object lrecval_TabSpecComm_Range may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecval_TabSpecComm_Range = Nothing
		On Error GoTo 0
	End Sub
End Class






