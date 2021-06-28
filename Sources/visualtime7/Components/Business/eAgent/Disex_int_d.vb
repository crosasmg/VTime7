Option Strict Off
Option Explicit On
Public Class Disex_int_d
	'%-------------------------------------------------------%'
	'% $Workfile:: Disex_int_d.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on June 11,2001.
	'+ Propiedades según la tabla en el sistema el 11/06/2001
	'**+ The key field corresponds to nEco_sche, nBranch, nProduct, dEffecdate.
	'+ El campo llave corresponde a nEco_sche, nBranch, nProduct, dEffecdate.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nEco_sche As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public sDisexpri As String 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nPercent As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	
	Public nStatusInstance As Integer
	Public sProductDes As String
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'**% Find: Searches the information in a general commissions table.
	'% Find: Busca la información de una tabla de comisiones de generales.
	Public Function Find(ByVal nEco_sche As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaDisex_int_d_v As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nEco_sche = Me.nEco_sche And nBranch = Me.nBranch And nProduct = Me.nProduct And dEffecdate = Me.dEffecdate And Not lblnFind Then
			Find = True
		Else
			lrecreaDisex_int_d_v = New eRemoteDB.Execute
			With lrecreaDisex_int_d_v
				.StoredProcedure = "reaDisex_int_d_v"
				.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nEco_sche = .FieldToClass("nEco_sche")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.sDisexpri = .FieldToClass("sDisexpri")
					Me.nPercent = .FieldToClass("nPercent")
					Me.nUsercode = .FieldToClass("nUsercode")
					Me.dNulldate = .FieldToClass("dNulldate")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaDisex_int_d_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaDisex_int_d_v = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Update:add/update the information in the main table for the transaction.
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		
		Dim lrecinsDisex_int_d As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsDisex_int_d = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.insDisex_int_d'
		'+Definición de parámetros para stored procedure 'insudb.insDisex_int_d'
		'**+ Data of June 12, 2001 02:18:15 p.m.
		'+Información leída el 12/06/2001 02:18:15 p.m.
		
		With lrecinsDisex_int_d
			.StoredProcedure = "insDisex_int_d"
			.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisexpri", sDisexpri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsDisex_int_d may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDisex_int_d = Nothing
		
		On Error GoTo 0
	End Function
	
	'**% Delete: delete information in the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Dim lrecinsDelDisex_int_d As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecinsDelDisex_int_d = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.insDelDisex_int_d'
		'+Definición de parámetros para stored procedure 'insudb.insDelDisex_int_d'
		'**+ Data of June 11,2001  03:15:30 p.m.
		'+Información leída el 11/06/2001 03:15:30 p.m.
		
		With lrecinsDelDisex_int_d
			.StoredProcedure = "insDelDisex_int_d"
			.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelDisex_int_d may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelDisex_int_d = Nothing
		
		On Error GoTo 0
	End Function
	
	'**% Find_Date: search the oldest effect date in the records of a commissions table
	'%Find_date(). Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de una tabla de  comisiones.
	Public Function Find_date() As Boolean
		
		Dim lrecreaDisex_int_d_date As eRemoteDB.Execute
		
		On Error GoTo Find_date_Err
		
		lrecreaDisex_int_d_date = New eRemoteDB.Execute
		
		'**+ Parameter definitions for store procedure 'insudb.reaDisex_int_d_date'
		'+Definición de parámetros para stored procedure 'insudb.reaDisex_int_d_date'
		'**+ data of June 11,2001  04:38:52 p.m.
		'+Información leída el 11/06/2001 04:38:52 p.m.
		
		With lrecreaDisex_int_d_date
			.StoredProcedure = "reaDisex_int_d_date"
			.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
		'UPGRADE_NOTE: Object lrecreaDisex_int_d_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisex_int_d_date = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG007_K: validate the data entered on the header form.
	'%insValMAG007_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG007_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nSeleted As Integer = 0, Optional ByVal nEco_sche As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Static lstrValField As String
		
		'**- Variable definition for lclsErrors for the errors in the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsDisex_int_d As Disex_int_d
		Dim lcolDisex_int_ds As Disex_int_ds
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG007_K_Err
		
		'+ Se instancias las clases a ser utilizadas en esta rutina y se inicializan las variables
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lcolDisex_int_ds = New eAgent.Disex_int_ds
		
		lblnInquiry = False
		lblnModify = False
		lblnErrors = False
		
		'**+ Validation of the field Table
		'+Validacion del campo TABLA
		
		If nEco_sche = eRemoteDB.Constants.intNull Or nEco_sche = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		End If
		
		'**+ Validation of the field Date
		'+Validacion del campo FECHA
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 7116)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				'+ Si la acción es ACTUALIZAR y la fecha es menor a la fecha del día, error #10869
				'If dEffecdate <= Today Then
				'	lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
				'	Call lclsErrors.ErrorMessage(sCodispl, 10868)
				'	lblnErrors = True
				'	lblnModify = False
				'	lblnInquiry = True
				'End If
				
				Me.nEco_sche = nEco_sche
				Call Find_date()
				
				ldtmMaxDate = Me.dEffecdate
				
				'+ Si la fecha máxima es mayo a la fecha de efecto, error #10868
				If ldtmMaxDate >= dEffecdate Then
					lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
					Call lclsErrors.ErrorMessage(sCodispl, 10869,  ,  , CStr(ldtmMaxDate))
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
		
		
		insValMAG007_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValMAG007_K_Err: 
		If Err.Number Then
			insValMAG007_K = "insValMAG007_K: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG007: validate the data entered on the detail zone for the form.
	'%insValMAG007: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMAG007(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nEco_sche As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal sDisexpri As String = "", Optional ByVal nPercent As Double = 0) As String
		
		'**- Variable definition lclsErrors for the errors in the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsDisex_int_d As eAgent.Disex_int_d
		
		On Error GoTo insValMAG007_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		
		'**+ Validation of the field Line of Business
		'+ Validación del campo Ramo
		
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
		Else
			'**+ Validation of the economic scheme type (recharge/discount)
			'+ Validación del campo tipo de esquema económico (recargo/descuento)
			
			If sDisexpri = strNull Or CDbl(sDisexpri) = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11334)
			End If
			
			'**+ Validation of the field recharge percentage/discount
			'+ Validación del campo de porcentaje de recargo/descuento
			
			If nPercent = eRemoteDB.Constants.intNull Or nPercent = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11196)
			End If
			
		End If

        '+ El porcentaje debe estár entre 0 y 100
		If nPercent < 0 or nPercent > 100 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1938)
		End If
		
		'**+ Validate the values entered does not exist
		'+Se valida que los valores introducidos no estén registrados
		
		If nEco_sche <> eRemoteDB.Constants.intNull And nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And dEffecdate <> dtmNull And sAction = "Add" Then
			lclsDisex_int_d = New eAgent.Disex_int_d
			If lclsDisex_int_d.Find(nEco_sche, nBranch, nProduct, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8307)
			End If
			'UPGRADE_NOTE: Object lclsDisex_int_d may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsDisex_int_d = Nothing
		End If
		
		insValMAG007 = lclsErrors.Confirm
		
insValMAG007_Err: 
		If Err.Number Then
			insValMAG007 = "insValMAG007: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsDisex_int_d may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisex_int_d = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'*** InsPostMAG007: This function is in charge of creating/updating the records
	'*** correspondent to the Disex_int_d table
	'*InsPostMAG007: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Disex_int_d
	Public Function insPostMAG007(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nEco_sche As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal sDisexpri As String = "", Optional ByVal nPercent As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMAG007_err
		
		Me.nEco_sche = nEco_sche
		Me.nBranch = nBranch
		
		If nProduct = eRemoteDB.Constants.intNull Then
			Me.nProduct = 0
		Else
			Me.nProduct = nProduct
		End If
		
		Me.nPercent = nPercent
		Me.sDisexpri = sDisexpri
		Me.dEffecdate = dEffecdate
		Me.nUsercode = nUsercode
		
		insPostMAG007 = True
		
		Select Case sAction
			
			'**+ If the selected option is Register
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG007 = Update()
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG007 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG007 = Delete()
				
		End Select
		
insPostMAG007_err: 
		If Err.Number Then
			insPostMAG007 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'*** Class_Initialize: controls the opening of the class
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






