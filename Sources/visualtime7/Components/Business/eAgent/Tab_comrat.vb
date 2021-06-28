Option Strict Off
Option Explicit On
Public Class Tab_comrat
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_comrat.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on May 14,2001
	'+ Propiedades según la tabla en el sistema el 14/05/2001
	'**+ The key field corresponds to nTable_cod, nCurrency, sType_infor, nBrach, nProduct, dEffecdate, nPrem_init.
	'+ El campo llave corresponde a nTable_cod, nCurrency, sType_infor, nBranch, nProduct, dEffecdate, nPrem_init.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nTable_cod As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public sType_Infor As String 'char     1                 no       no                 no
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nPrem_init As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public nComrate As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nPrem_end As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	
	Public nStatusInstance As Integer
	Public nPrem_init_key As Double
	Public sProductDes As String
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'**% Find: Searches for the information in the over-commission table.
	'% Find: Busca la información de una tabla de sobre-comisiones.
	Public Function Find(ByVal nTable_cod As Integer, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPrem_init As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_comrat_v As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nTable_cod = Me.nTable_cod And nCurrency = Me.nCurrency And sType_Infor = Me.sType_Infor And nBranch = Me.nBranch And nProduct = Me.nProduct And nPrem_init = Me.nPrem_init And dEffecdate = Me.dEffecdate And Not lblnFind Then
			Find = True
		Else
			lrecreaTab_comrat_v = New eRemoteDB.Execute
			With lrecreaTab_comrat_v
				
				.StoredProcedure = "reaTab_comrat_v"
				.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPrem_init_key", nPrem_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nTable_cod = .FieldToClass("nTable_cod")
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.sType_Infor = .FieldToClass("sType_infor")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPrem_init = .FieldToClass("nPrem_init")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nComrate = .FieldToClass("nComrate")
					Me.nPrem_end = .FieldToClass("nPrem_end")
					Me.dNulldate = .FieldToClass("dNulldate")
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
		'UPGRADE_NOTE: Object lrecreaTab_comrat_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comrat_v = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Table: Esta función determina si el número de tabla que es suministrado
	'%             por el usuario se encuentra o no registrado en la base de datos
	Public Function Find_Table(ByVal nTable_cod As Integer) As Boolean
		Dim lrecreaTab_excomm_v As eRemoteDB.Execute
		
		On Error GoTo Find_Table_err
		
		lrecreaTab_excomm_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_excomm_v'
		'+ Información leída el 26/04/2002 10:55:18 a.m.
		
		With lrecreaTab_excomm_v
			.StoredProcedure = "reaTab_excomm_v"
			.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_Table = .Run(True)
			.RCloseRec()
		End With
		
Find_Table_err: 
		If Err.Number Then
			Find_Table = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_excomm_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_excomm_v = Nothing
		On Error GoTo 0
		
	End Function
	
	'**% Update: add/update the information in the main table for the transaction.
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecinsTab_comrat As eRemoteDB.Execute
		
		lrecinsTab_comrat = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameter definition for stored procedure 'insudb.insTab_comrat'
		'+Definición de parámetros para stored procedure 'insudb.insTab_comrat'
		'**+ Information read on May 14,2001 02:44:47 p.m.
		'+Información leída el 14/05/2001 02:44:47 p.m.
		
		With lrecinsTab_comrat
			.StoredProcedure = "insTab_comrat"
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_init", nPrem_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_init_key", nPrem_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_end", nPrem_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComrate", nComrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsTab_comrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_comrat = Nothing
		On Error GoTo 0
	End Function
	
	'**% Delete: delete information of the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Dim lrecinsDelTab_comrat As eRemoteDB.Execute
		
		lrecinsDelTab_comrat = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+ Parameter definition for stored procedure 'insudb.insDelTab_comrat'
		'+Definición de parámetros para stored procedure 'insudb.insDelTab_comrat'
		'**+ Information read on May 14, 2001  03:14:47 p.m.
		'+Información leída el 14/05/2001 03:14:47 p.m.
		
		With lrecinsDelTab_comrat
			.StoredProcedure = "insDelTab_comrat"
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_init_key", nPrem_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelTab_comrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelTab_comrat = Nothing
		On Error GoTo 0
		
	End Function
	
	'**% Find_date. This function searches the oldest effect date of the record in the over-commission table.
	'%Find_date. Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de una tabla de sobre-comisiones.
	Public Function Find_date() As Boolean
		
		Dim lrecreaTab_comrat_date As eRemoteDB.Execute
		
		lrecreaTab_comrat_date = New eRemoteDB.Execute
		
		On Error GoTo Find_date_Err
		
		'**+ Parameter definition for stored procedure 'insudb.reaTab_comrat_date'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_comrat_date'
		'**+ Information read on May 14,2001 03:38:52 p.m.
		'+Información leída el 14/05/2001 03:38:52 p.m.
		
		With lrecreaTab_comrat_date
			.StoredProcedure = "reaTab_comrat_date"
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
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
		'UPGRADE_NOTE: Object lrecreaTab_comrat_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comrat_date = Nothing
		On Error GoTo 0
		
	End Function
	
	'**% MISSING
	'%Find_range. Esta funcion se encarga de buscar si el rango de primas colocado
	'%en la ventana, existe dentro de otro rango registrado en la tabla tab_comrat.
	Public Function Find_range() As Boolean
		
		Dim lrecreaTab_comrat_range As eRemoteDB.Execute
		
		On Error GoTo Find_range_Err
		
		lrecreaTab_comrat_range = New eRemoteDB.Execute
		
		'**+ Parameter deifinition for stored procedure 'insudb.reaTab_comrat_range'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_comrat_range'
		'**+ Information read on May 14, 2001 03:36:16 p.m.
		'+Información leída el 14/05/2001 03:36:18 p.m.
		
		With lrecreaTab_comrat_range
			.StoredProcedure = "reaTab_comrat_range"
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_init", nPrem_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_end", nPrem_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("nExist") = 1 Then
					Find_range = True
				End If
			End If
		End With
Find_range_Err: 
		If Err.Number Then
			Find_range = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_comrat_range may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comrat_range = Nothing
	End Function
	
	'**% insValMAG004_K: validate the data entered on the header form
	'%insValMAG004_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG004_K(ByVal sCodispl As String, ByVal nAction As eFunctions.Menues.TypeActions, Optional ByVal nSeleted As Integer = 0, Optional ByVal nTable_cod As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCurrency As Integer = 0, Optional ByVal sType_Infor As String = "") As String
		
		Static lstrValField As String
		
		'**- Variable definition leclsErrors for the error sending of the window.
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_comrat As eAgent.Tab_comrat
		Dim lcolTab_comrats As eAgent.Tab_comrats
		Dim lclsValField As eFunctions.valField
		
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG004_K_Err
		lclsErrors = New eFunctions.Errors
		lclsTab_comrat = New eAgent.Tab_comrat
		lcolTab_comrats = New eAgent.Tab_comrats
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		lblnInquiry = True
		lblnModify = True
		lblnErrors = False
		
		'**+ Validation of the field: Table
		'+Validacion del campo: Tabla.
		
		If nTable_cod = eRemoteDB.Constants.intNull Or nTable_cod = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		Else
			lclsValField.Min = 1
			lclsValField.Max = 32767
			Call lclsValField.ValNumber(nTable_cod)
			If nTable_cod < 32768 Then
				Me.nTable_cod = CShort(nTable_cod)
				lclsTab_comrat.nTable_cod = CShort(nTable_cod)
				If Not lclsTab_comrat.Find_Table(nTable_cod) Then
					Call lclsErrors.ErrorMessage(sCodispl, 9047)
				End If
			End If
		End If
		
		'**+ Validation of the field: Date
		'+ Validación del campo: Fecha.
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10190)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If dEffecdate <= Today Then
					lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
					Call lclsErrors.ErrorMessage(sCodispl, 10869)
					lblnErrors = True
					lblnModify = False
					lblnInquiry = True
				End If
				
				Me.nTable_cod = nTable_cod
				Me.nCurrency = nCurrency
				Me.sType_Infor = sType_Infor
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
		
		'**+ Validation of the field: Currency
		'+Validación del campo: Moneda.
		
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		End If
		
		'**+ Validation of the field: Type of information.
		'+Validacion del campo: Tipo de información.
		
		If sType_Infor = strNull Or CDbl(sType_Infor) = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10178)
		End If
		
		'**+ If there hasn't been an error and the validation is massive, verifies the existence for the repetitive part.
		'+ Si no hubo error y la validación es masiva; se verifica la existencia de información para la parte repetitiva.
		If lblnErrors And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			lcolTab_comrats = New Tab_comrats
			If Not lcolTab_comrats.Find(nTable_cod, nCurrency, sType_Infor, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
			'UPGRADE_NOTE: Object lcolTab_comrats may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolTab_comrats = Nothing
		End If
		
		insValMAG004_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG004_K_Err: 
		If Err.Number Then
			insValMAG004_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG004: validate the data entered on the detail zone for the form
	'%insValMAG004: Esta función se encarga de validar los datos introducidos en la zona de
	'% detalle para forma.
	Public Function insValMAG004(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nTable_cod As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sType_Infor As String = "", Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPrem_init As Double = 0, Optional ByVal nPrem_end As Double = 0, Optional ByVal nComrate As Double = 0) As String
		
		'**- Variable definition lclsErrors for the errors in the window sending.
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsTab_comrat As eAgent.Tab_comrat
		
		Dim lblnRightPrem_init As Boolean
		
		On Error GoTo insValMAG004_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsTab_comrat = New eAgent.Tab_comrat
		lclsValField.objErr = lclsErrors
		
		'**+ Start the validation cycle.
		'+Se da inicio al ciclo de validaciones.
		lblnRightPrem_init = True
		
		'**+ Validation of Line of business
		'+ Validación del ramo
		
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
		End If
		
		'**+ Validation of the initial premium
		'+Validación de la prima inicial
		
		If nPrem_init = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10182)
		Else
			lclsValField.Min = 0#
			lclsValField.Max = 9999999999.99
			lclsValField.Descript = "Prima inicial"
			
			If Not lclsValField.ValNumber(nPrem_init) Then
				lblnRightPrem_init = False
			Else
				If nBranch <> eRemoteDB.Constants.intNull Then
					If sAction = "Add" Then
						If lclsTab_comrat.Find(nTable_cod, nCurrency, sType_Infor, nBranch, nProduct, nPrem_init, dEffecdate) Then
							Call lclsErrors.ErrorMessage(sCodispl, 10870)
						End If
					End If
				End If
			End If
		End If
		
		'**+ Validation of the fineal premium
		'+ Validación de la prima final
		
		If nPrem_end = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10183)
		Else
			lclsValField.Min = 0#
			lclsValField.Max = 9999999999.99
			lclsValField.Descript = "Prima final"
			
			If lclsValField.ValNumber(nPrem_end) Then
				If lblnRightPrem_init Then
					If nPrem_init >= nPrem_end Then
						Call lclsErrors.ErrorMessage(sCodispl, 10184)
					Else
						If nBranch <> eRemoteDB.Constants.intNull And nPrem_init <> eRemoteDB.Constants.intNull And nPrem_end <> eRemoteDB.Constants.intNull Then
							
							'**+ Validate that the value is not previously registered in the Tab_comrat table
							'+ Se valida que el valor no se encuentra previamente registrado en la tabla Tab_comrat
							With lclsTab_comrat
								.nTable_cod = nTable_cod
								.nCurrency = nCurrency
								.sType_Infor = sType_Infor
								.dEffecdate = dEffecdate
								.nBranch = nBranch
								.nProduct = nProduct
								.nPrem_init = nPrem_init
								.nPrem_init_key = nPrem_init
								.nPrem_end = nPrem_end
								If .Find_range() Then
									Call lclsErrors.ErrorMessage(sCodispl, 10185,  ,  , " [" & .nPrem_init & "," & .nPrem_end & "] ")
								End If
							End With
						End If
					End If
				End If
			End If
		End If
		
		'**+ Validation of the over-commission
		'+ Validación de la sobre-comisión
		
		If nComrate = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10186)
		Else
			lclsValField.Min = 0.01
			lclsValField.Max = 99.99
			lclsValField.Descript = "Sobre-comisión"
			
			If lclsValField.ValNumber(nComrate) Then
				insValMAG004 = CStr(False)
			End If
		End If
		
		insValMAG004 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsTab_comrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_comrat = Nothing
		
insValMAG004_Err: 
		If Err.Number Then
			insValMAG004 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insPostMAG004: This function is in charge of create/update the correspondent
	'**% record in the Tab_comrat table.
	'% insPostMAG004: Esta función se encarga de crear/actualizar los registros
	'% correspondientes en la tabla de Tab_comrat
	Public Function insPostMAG004(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nTable_cod As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sType_Infor As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPrem_init As Double = 0, Optional ByVal nPrem_end As Double = 0, Optional ByVal nComrate As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMAG004_err
		
		Me.nTable_cod = nTable_cod
		Me.nCurrency = nCurrency
		Me.sType_Infor = sType_Infor
		Me.nBranch = nBranch
		
		If nProduct = eRemoteDB.Constants.intNull Then
			Me.nProduct = 0
		Else
			Me.nProduct = nProduct
		End If
		
		Me.nPrem_init = nPrem_init
		Me.nPrem_end = nPrem_end
		Me.nComrate = nComrate
		Me.dEffecdate = dEffecdate
		Me.nUsercode = nUsercode
		
		insPostMAG004 = True
		
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG004 = Update()
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG004 = Update()
				
				'**+ If the selected option is Remove
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG004 = Delete()
				
		End Select
		
insPostMAG004_err: 
		If Err.Number Then
			insPostMAG004 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






