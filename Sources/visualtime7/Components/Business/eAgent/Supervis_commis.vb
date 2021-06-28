Option Strict Off
Option Explicit On
Public Class Supervis_commis
	'%-------------------------------------------------------%'
	'% $Workfile:: Supervis_commis.cls                      $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 25/09/03 18:39                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on Dec 14, 2001.
	'+ Propiedades según la tabla en el sistema el 14/12/2001
	'**+ The key field correspond to nInterTyp, nBranch, nProduct, nInterTyp, nLower_level, dEffecdate.
	'+ El campo llave corresponde a nInterTyp, nBranch, nProduct, nInterTyp, nLower_level, dEffecdate.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nInterTyp As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nLower_level As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public nCommiss As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	Public nTypPort As Integer
	
	Public nStatusInstance As Integer
	Public sBranchDes As String
	Public sProductDes As String
	Public sInterTypDes As String
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = insUpdSupervis_commis(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = insUpdSupervis_commis(2)
	End Function
	
	'**% Find: Searches for the information in the general commissions table
	'% Find: Busca la información de una tabla de comisiones de generales.
	Public Function Find(ByVal nInterTyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLower_level As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False, Optional ByVal nTypPort As Integer = 0) As Boolean
		Dim lrecreaSupervis_commis As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nInterTyp = Me.nInterTyp And nBranch = Me.nBranch And nProduct = Me.nProduct And nLower_level = Me.nLower_level And dEffecdate = Me.dEffecdate And Not lblnFind Then
			Find = True
		Else
			lrecreaSupervis_commis = New eRemoteDB.Execute
			With lrecreaSupervis_commis
				.StoredProcedure = "reaSupervis_commis"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInterTyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLower_level", nLower_level, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypPort", nTypPort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Me.nInterTyp = .FieldToClass("nInterTyp")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nLower_level = .FieldToClass("nLower_level")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nCommiss = .FieldToClass("nCommiss")
					Me.nUsercode = .FieldToClass("nUsercode")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nTypPort = .FieldToClass("nTypPort")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaSupervis_commis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSupervis_commis = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'**% insUpdSupervis_commis: update the information in the main table for the transaction.
	'% insUpdSupervis_commis: Esta función se encarga de actualizar la información en tratamiento de la
	'% tabla principal para la transacción.
	Public Function insUpdSupervis_commis(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdSupervis_commis As eRemoteDB.Execute
		
		On Error GoTo insUpdSupervis_commis_Err
		
		lrecinsUpdSupervis_commis = New eRemoteDB.Execute
		
		'**+Parameter definitions for stored procedure 'insudb.insUpdSupervis_commis'
		'+Definición de parámetros para stored procedure 'insudb.insUpdSupervis_commis'
		'**+ Data of Dec 14,2001 02:44:47 p.m.
		'+Información leída el 14/12/2001 02:44:47 p.m.
		
		With lrecinsUpdSupervis_commis
			.StoredProcedure = "insUpdSupervis_commis"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterTyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLower_level", nLower_level, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommiss", nCommiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypPort", nTypPort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdSupervis_commis = .Run(False)
			
		End With
		
insUpdSupervis_commis_Err: 
		If Err.Number Then
			insUpdSupervis_commis = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdSupervis_commis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdSupervis_commis = Nothing
		On Error GoTo 0
	End Function
	
	'**% Delete: Delete information in the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Delete = insUpdSupervis_commis(3)
	End Function
	
	'**% Find_date. This function search the oldest effect date in the supervis_commis table.
	'% Find_date. Esta funcion se encarga de buscar la mayor de las fecha de efecto de los registros
	'% de la tabla supervis_commis
	Public Function Find_date() As Boolean
		
		Dim lrecreaSupervis_commis_date As eRemoteDB.Execute
		
		On Error GoTo Find_date_Err
		
		lrecreaSupervis_commis_date = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.reaSupervis_commis_date'
		'+Definición de parámetros para stored procedure 'insudb.reaSupervis_commis_date'
		'**+ Data of Dec 14, 2001  09:38:52 a.m.
		'+Información leída el 14/12/2001 09:38:52 a.m.
		
		With lrecreaSupervis_commis_date
			.StoredProcedure = "reaSupervis_commis_date"
			.Parameters.Add("nInterTyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				Find_date = True
			Else
				Me.dEffecdate = CDate("01/01/1800")
				Find_date = False
			End If
		End With
Find_date_Err: 
		If Err.Number Then
			Find_date = False
		End If
		'UPGRADE_NOTE: Object lrecreaSupervis_commis_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSupervis_commis_date = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG573_K: Validates the data entered on the header
	'%insValMAG573_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG573_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nInterTyp As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Static lstrValField As String
		
		'**- Variable definition for lclsErrors for the errors of the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsSupervis_commis As Supervis_commis
		Dim lcolSupervis_commiss As Supervis_commiss
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG573_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lcolSupervis_commiss = New eAgent.Supervis_commiss
		
		lblnInquiry = False
		lblnModify = False
		lblnErrors = False
		
		'**+ Validation of the field Date
		'+Validacion del campo FECHA
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10190)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If dEffecdate <= Today Then
					Call lclsErrors.ErrorMessage(sCodispl, 10869)
					lblnErrors = True
					lblnModify = False
					lblnInquiry = True
				End If
				
				Me.nInterTyp = nInterTyp
				Call Find_date()
				
				ldtmMaxDate = Me.dEffecdate
				
				If ldtmMaxDate >= dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 10868,  ,  , CStr(ldtmMaxDate))
					lblnErrors = True
					lblnModify = False
					lblnInquiry = True
				Else
					If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
						lblnInquiry = True
						lblnModify = True
					Else
						lblnInquiry = False
						lblnModify = False
					End If
				End If
			End If
		End If
		
		If nInterTyp = 0 Or nInterTyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		If lblnErrors And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			lcolSupervis_commiss = New Supervis_commiss
			If Not lcolSupervis_commiss.Find(nInterTyp, dEffecdate, 1) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
			'UPGRADE_NOTE: Object lcolSupervis_commiss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolSupervis_commiss = Nothing
		End If
		
		insValMAG573_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValMAG573_K_Err: 
		If Err.Number Then
			insValMAG573_K = "insValMAG573_K: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG573: validate the data entered on the detail zone for the form
	'%insValMAG573: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMAG573(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nInterTyp As Integer = 0, Optional ByVal nLower_level As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCommiss As Double = 0, Optional ByVal nTypPort As Integer = 0) As String
		
		'**- Variable definition lclsErrors for the errors in the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsSupervis_commis As eAgent.Supervis_commis
		Dim lclsValues As eFunctions.Values
		Dim lintProduct As Integer
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValues = New eFunctions.Values
		
		On Error GoTo insValMAG573_Err
		
		With lclsErrors
			
			'+ Validación del campo Grupo Supervisado
			If nLower_level = eRemoteDB.Constants.intNull Or nLower_level = 0 Then
				Call .ErrorMessage(sCodispl, 60383)
			End If
			
			'**+ Validation of the field Line of Business
			'+ Validación del campo Ramo
			
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				Call .ErrorMessage(sCodispl, 9064)
			Else
				If nProduct <> eRemoteDB.Constants.intNull Then
					
					'+ Se va a validar el campo producto
					lclsValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not lclsValues.IsValid("tabProdMaster1", CStr(nProduct), True) Then
						Call .ErrorMessage(sCodispl, 1011)
					End If
				End If
			End If
			
			'**+ Validation of the fields of commission
			'+ Validación de los campos de comisión
			
			If nCommiss = eRemoteDB.Constants.intNull Or nCommiss = 0 Then
				Call .ErrorMessage(sCodispl, 9092)
			End If
			
			'**+ Validate that the values does not exist
			'+Se valida que los valores introducidos no estén registrados
			'+ Validación del campo Grupo Supervisado
			If nTypPort = eRemoteDB.Constants.intNull Or nTypPort = 0 Then
				Call .ErrorMessage(sCodispl, 55138)
			End If
			
			If nInterTyp <> eRemoteDB.Constants.intNull And nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And nLower_level <> eRemoteDB.Constants.intNull And dEffecdate <> dtmNull And sAction = "Add" Then
				If nProduct = eRemoteDB.Constants.intNull Then
					lintProduct = 0
				Else
					lintProduct = nProduct
				End If
				If Me.Find(nInterTyp, nBranch, lintProduct, nLower_level, dEffecdate, True, nTypPort) Then
					Call .ErrorMessage(sCodispl, 55528)
				End If
			End If
			
		End With
		insValMAG573 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsSupervis_commis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSupervis_commis = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
insValMAG573_Err: 
		If Err.Number Then
			insValMAG573 = "insValMAG573: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'*** InsPostMAG573: create/update correspondent
	'*** registrations in the Supervis_commis table
	'*InsPostMAG573: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Supervis_commis
	Public Function insPostMAG573(ByVal sAction As String, Optional ByVal nInterTyp As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nLower_level As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCommiss As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nTypPort As Integer = 0) As Boolean
		On Error GoTo insPostMAG573_err
		Me.nInterTyp = nInterTyp
		Me.nLower_level = nLower_level
		Me.nBranch = nBranch
		If nProduct = eRemoteDB.Constants.intNull Then
			Me.nProduct = 0
		Else
			Me.nProduct = nProduct
		End If
		Me.nCommiss = nCommiss
		Me.dEffecdate = dEffecdate
		Me.nUsercode = nUsercode
		Me.nTypPort = nTypPort
		insPostMAG573 = True
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG573 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG573 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG573 = Delete()
				
		End Select
		
insPostMAG573_err: 
		If Err.Number Then
			insPostMAG573 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






