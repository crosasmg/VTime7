Option Strict Off
Option Explicit On
Public Class Int_fixval
	'%-------------------------------------------------------%'
	'% $Workfile:: Int_fixval.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on June 11,2001
	'+ Propiedades según la tabla en el sistema el 11/06/2001
	'**+ The field key corresponds to nCode,dEffecdate.
	'+ El campo llave corresponde a nCode, dEffecdate.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nCode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public sDescript As String 'char     30                yes      no                 yes
	Public nAmount As Double 'decimal  9      10   2     yes      (n/a)              (n/a)
	Public nRate As Double 'decimal  5      8    5     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	
	Public nStatusInstance As Integer
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'**% Find: Searches the information of a general commissions table
	'% Find: Busca la información de una tabla de comisiones de generales.
	Public Function Find(ByVal nCode As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaInt_fixval_v As eRemoteDB.Execute
		
		lrecreaInt_fixval_v = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nCode = Me.nCode And dEffecdate = Me.dEffecdate And Not lblnFind Then
			Find = True
		Else
			With lrecreaInt_fixval_v
				
				'**+ Parameter definition for stored procedure 'insudb.reaInt_fixval_v'
				'+Definición de parámetros para stored procedure 'insudb.reaInt_fixval_v'
				'**+ Data of June 15, 2001  10:48.41 a.m.
				'+Información leída el 15/06/2001 10:48:41 a.m.
				
				.StoredProcedure = "reaInt_fixval_v"
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDecimal, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nCode = .FieldToClass("nCode")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.sDescript = .FieldToClass("sDescript")
					Me.nAmount = .FieldToClass("nAmount")
					Me.nRate = .FieldToClass("nRate")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nUsercode = .FieldToClass("nUsercode")
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
		'UPGRADE_NOTE: Object lrecreaInt_fixval_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInt_fixval_v = Nothing
		On Error GoTo 0
	End Function
	
	'**% Update: add/update the information in treat of the main table for the transaction.
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecinsInt_fixval As eRemoteDB.Execute
		
		lrecinsInt_fixval = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameter definitions for stored procedure 'insudb.insInt_fixval'
		'+Definición de parámetros para stored procedure 'insudb.insInt_fixval'
		'**+ Data of June 14,2001 04:12:33 p.m.
		'+Información leída el 14/06/2001 04:12:33 p.m.
		
		With lrecinsInt_fixval
			.StoredProcedure = "insInt_fixval"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDecimal, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsInt_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsInt_fixval = Nothing
		On Error GoTo 0
	End Function
	
	'**% Delete: delete the information of the main table of the class
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Dim lrecinsDelInt_fixval As eRemoteDB.Execute
		
		lrecinsDelInt_fixval = New eRemoteDB.Execute
		On Error GoTo Delete_Err
		
		'**+ Parameter definition for stored procedure 'insudb.insDelInt_fixval'
		'+Definición de parámetros para stored procedure 'insudb.insDelInt_fixval'
		'**+ Data of June 14, 2001 04:12:33 p.m.
		'+Información leída el 14/06/2001 04:12:33 p.m.
		
		With lrecinsDelInt_fixval
			.StoredProcedure = "insDelInt_fixval"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDecimal, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelInt_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelInt_fixval = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find_date. This function is in charge of search the older
	'**% effect date of the records of a commission table.
	'%Find_date. Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de una tabla de  comisiones.
	Public Function Find_date() As Boolean
		Dim lrecreaInt_fixval_date As eRemoteDB.Execute
		
		lrecreaInt_fixval_date = New eRemoteDB.Execute
		On Error GoTo Find_date_Err
		
		'**+Parameter definition for stored procedure 'insudb.reaInt_fixval_date'
		'+Definición de parámetros para stored procedure 'insudb.reaInt_fixval_date'
		'**+ Data of June 14,2001 04:12:33 p.m.
		'+Información leída el 14/06/2001 04:12:33 p.m.
		
		With lrecreaInt_fixval_date
			.StoredProcedure = "reaInt_fixval_date"
			
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
		'UPGRADE_NOTE: Object lrecreaInt_fixval_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInt_fixval_date = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValMAG008_K: validate the data entered on the header form.
	'%insValMAG008_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG008_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nSeleted As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Static lstrValField As String
		
		'**- Variable definition lclsErrors for the erros in the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lcolInt_fixvals As Int_fixvals
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG008_K_Err
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lcolInt_fixvals = New eAgent.Int_fixvals
		
		lblnInquiry = False
		lblnModify = False
		lblnErrors = False
		
		'**+ Validation of the field Date
		'+Validacion del campo FECHA
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If dEffecdate <= Today Then
					lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
					Call lclsErrors.ErrorMessage(sCodispl, 10869)
					lblnErrors = True
					lblnModify = False
					lblnInquiry = True
				End If
				
				Call Find_date()
				
				ldtmMaxDate = Me.dEffecdate
				
				If ldtmMaxDate >= dEffecdate Then
					lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
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
		
		insValMAG008_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValMAG008_K_Err: 
		If Err.Number Then
			insValMAG008_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG008: validate the data entered in the detail zone for the form.
	'%insValMAG008: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMAG008(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCode As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal nAmount As Double = 0, Optional ByVal nRate As Double = 0) As String
		
		'**- Variable definition lclsErrors for the errors in the window sending.
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsInt_fixval As eAgent.Int_fixval
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		
		On Error GoTo insValMAG008_Err
		
		'**+ Validation of the field Fixed Charge concept
		'+ Validación del campo Código del concepto de cargo fijo
		
		If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10865)
		Else
			'**+ Validation of the field Fixed charge concept description
			'+ Validación del campo Descripción del concepto de cargo fijo
			
			If sDescript = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10071)
			End If
			
			'**+ Validation of the field Amount and Percentage of the concept of fixed charge.
			'+ Validación del campo Monto y Porcentaje del concepto de cargo fijo
			
			If (nAmount = eRemoteDB.Constants.intNull Or nAmount = 0) And (nRate = eRemoteDB.Constants.intNull Or nRate = 0) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10124)
			End If
			
		End If
		
		'**+ Validate that the introduced data does not exist
		'+Se valida que los valores introducidos no estén registrados
		
		If nCode <> eRemoteDB.Constants.intNull And nCode <> 0 And dEffecdate <> dtmNull And sAction = "Add" Then
			lclsInt_fixval = New eAgent.Int_fixval
			If lclsInt_fixval.Find(nCode, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10004)
			End If
			'UPGRADE_NOTE: Object lclsInt_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsInt_fixval = Nothing
		End If
		
		insValMAG008 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsInt_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInt_fixval = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValMAG008_Err: 
		If Err.Number Then
			insValMAG008 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% InsPostMAG008: create/update the correspondent records in the Int_fixval table.
	'%InsPostMAG008: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla de Int_fixval
	Public Function insPostMAG008(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCode As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal nAmount As Double = 0, Optional ByVal nRate As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMAG008_err
		
		Me.dEffecdate = dEffecdate
		Me.nCode = nCode
		Me.sDescript = sDescript
		Me.nAmount = nAmount
		Me.nRate = nRate
		Me.nUsercode = nUsercode
		
		insPostMAG008 = True
		
		Select Case sAction
			
			'**+ If the selected option is Register
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG008 = Update()
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG008 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG008 = Delete()
				
		End Select
		
insPostMAG008_err: 
		If Err.Number Then
			insPostMAG008 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






