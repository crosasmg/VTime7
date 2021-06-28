Option Strict Off
Option Explicit On
Public Class Margin_master
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_master.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 29/06/04 9:44a                               $%'
	'% $Revision:: 35                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables segun campos en la tabla al 22/05/2003
	
	'+ Nombre               Tipo                    ¿Nulo?
	'+ -------------------- ----------------------- ------
	Public nInsur_area As Integer ' NUMBER(5)     NO
	Public dInitdate As Date ' DATE          NO
	Public nIdtable As Integer ' NUMBER(10)    NO
	Public nTabletyp As Integer ' NUMBER(1)     NO
	Public nSource As Integer ' NUMBER(1)     NO
	Public nClaimClass As Integer ' NUMBER(1)     YES
	Public dEndDate As Date ' DATE          NO
	Public nUsercode As Integer ' NUMBER(5)     YES
	
	'+ Variables auxiliares
	'- Se almacenan la lista de valores para un período dado
	Public sTableTyp As String
	Public sSource As String
	Public sClaimclass As String
	Public sKey As String
	'- Indica si se muestra o no la clasificación de siniestros
	Public bClaimclass As Boolean
	
	'% Find_list: se obtienen las listas de valores para el tipo de tabla, origen de la tabla,
	'%            clasificación de siniestros, para un período dado
	Public Function Find_list(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal dEndDate As Date, ByVal sTableTyp As String, ByVal nTabletyp As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Find_Period_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaMargin_master_list"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitdate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTabletyp", nTabletyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTabletyp", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSource", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaimclass", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Me.sTableTyp = .Parameters("sTabletyp").Value
				Me.sSource = .Parameters("sSource").Value
				Me.sClaimclass = .Parameters("sClaimclass").Value
				'+ Si el tipo de tabla es pasivo
				If sTableTyp = "5" And Me.sSource = String.Empty Then
					'+ El origen sólo puede ser Corto plazo, Largo plazo e Indirecto
					Me.sSource = "4,5,6"
				ElseIf sTableTyp <> "5" And Me.sSource = String.Empty And sTableTyp <> "" Then 
					Me.sSource = "1,2,3"
				End If
				'+ La clasificación de siniestros sólo se muestra si el tipo de tabla es siniestros
				bClaimclass = True
				If Me.sTableTyp > String.Empty Then
					If Mid(Me.sTableTyp, 1) <> "2" Then
						bClaimclass = False
					End If
				End If
				
				Find_list = True
			End If
		End With
		
Find_Period_Err: 
		If Err.Number Then
			Find_list = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insvalMGS001_K: se realizan las validaciones del encabezado de la página
	Public Function insvalMGS001_K(ByVal sCodispl As String, ByVal nMainAction As Short, ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal dEndDate As Date, ByVal nTabletyp As Integer, ByVal nSource As Integer, ByVal nClaimClass As Integer) As String
		Dim lblnError As Boolean
		Dim lintExists As Short
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		
		On Error GoTo insvalMGS001_K_err
		
		lobjErrors = New eFunctions.Errors
		
		lblnError = False
		With lobjErrors
			'+ El área del seguro debe estar lleno
			'    If nInsur_area = NumNull Then
			'       Call .ErrorMessage(sCodispl, 55031)
			'      lblnError = True
			'  End If
			
			'+ La fecha de inicio del período debe estar lleno
			If dInitdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 3237)
				lblnError = True
			End If
			
			'+ La fecha de fin del período debe estar lleno
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 60218)
				lblnError = True
			Else
				'+ La fecha de fin del período debe ser mayor a la fecha de inicio del período
				If dInitdate <> eRemoteDB.Constants.dtmNull Then
					If dEndDate <= dInitdate Then
						Call .ErrorMessage(sCodispl, 4158)
						lblnError = True
					End If
					
					lclsCtrol_date = New eGeneral.Ctrol_date
					If lclsCtrol_date.Find(41) Then
						'+ Si se han realizado cálculos del margen de solvencia para la fecha, si corresponde al
						'+ último proceso de margen de solvencia se envía como advertencia, sino, como error
						If dInitdate = lclsCtrol_date.dEffecdate Then
							Call lobjErrors.ErrorMessage(sCodispl, 55916)
						ElseIf dInitdate < lclsCtrol_date.dEffecdate Then 
							Call lobjErrors.ErrorMessage(sCodispl, 56173)
						End If
						
					End If
				End If
			End If
			
			If nMainAction = eFunctions.Menues.TypeActions.clngActionadd Then
				If Not lblnError Then
					lintExists = insvalExist(nInsur_area, dInitdate, dEndDate, nTabletyp, nSource, nClaimClass)
					'+ No debe estar registrado para la combinación ingresada en los campos del encabezado
					If lintExists = 1 Then
						Call .ErrorMessage(sCodispl, 56035)
						'+ No debe estar registrado dentro de otro período, para la combinación ingresada en
						'+ los campos del encabezado
					ElseIf lintExists = 2 Then 
						Call .ErrorMessage(sCodispl, 56036)
					End If
				End If
			End If
			
			insvalMGS001_K = .Confirm
		End With
		
insvalMGS001_K_err: 
		If Err.Number Then
			insvalMGS001_K = "insvalMGS001_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
	End Function
	
	'% insvalMGS001: se realizan las validaciones de la zona masiva de la página
	Public Function insvalMGS001(ByVal sCodispl As String, ByVal nCount As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGS001_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Debe existir por lo menos un registro en la grilla
			If nCount = 0 Then
				Call .ErrorMessage(sCodispl, 55920)
			End If
			
			insvalMGS001 = .Confirm
		End With
		
insvalMGS001_err: 
		If Err.Number Then
			insvalMGS001 = "insvalMGS001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insvalExist: verifica la existencia del período, o si éste se encuentra dentro de uno
	'%              ya registrado
	Private Function insvalExist(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal dEndDate As Date, ByVal nTabletyp As Integer, ByVal nSource As Integer, ByVal nClaimClass As Integer) As Short
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalExist_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valExist_Margin_master"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitdate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTableTyp", nTabletyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExist = .Parameters("nExists").Value
			End If
		End With
		
insvalExist_err: 
		If Err.Number Then
			insvalExist = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insvalMGSL002: se realizan las validacion de la transaccion MGSL002
	Public Function insvalMGSL002(ByVal dDateProcess As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGSL002_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateProcess = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL002", 55581)
			End If
			
			insvalMGSL002 = .Confirm
		End With
		
insvalMGSL002_err: 
		If Err.Number Then
			insvalMGSL002 = "insvalMGSL002: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	'% inspostMGSL002: realiza el llamado al proceso
	Public Function inspostMGSL002(ByVal dDateProcess As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL002_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "CREMGSL002"
			.Parameters.Add("dDateProcess", dDateProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			inspostMGSL002 = .Run(False)
		End With
		
inspostMGSL002_err: 
		If Err.Number Then
			inspostMGSL002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insvalMGSL001: se realizan las validacion de la transaccion MGSL002
	Public Function insvalMGSL001(ByVal dDateInit As Date, ByVal dDateEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGSL001_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateInit = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL001", 9071)
			End If
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL001", 9072)
			End If
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateInit > dDateEnd Then
				Call .ErrorMessage("MGSL001", 3240)
			End If
			
			insvalMGSL001 = .Confirm
		End With
		
insvalMGSL001_err: 
		If Err.Number Then
			insvalMGSL001 = "insvalMGSL001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	'% inspostMGSL001: realiza el llamado al proceso
	Public Function inspostMGSL001(ByVal dDateInit As Date, ByVal dDateEnd As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL001_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "CREMGSL001"
			.Parameters.Add("dDateInit", dDateInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			inspostMGSL001 = .Run(False)
		End With
		
inspostMGSL001_err: 
		If Err.Number Then
			inspostMGSL001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	'% inspostMGSL004: realiza el llamado al proceso
	Public Function inspostMGSL004(ByVal dDateInit As Date, ByVal dDateEnd As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL004_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "CREMGSL004"
			.Parameters.Add("dDateInit", dDateInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			inspostMGSL004 = .Run(False)
		End With
		
inspostMGSL004_err: 
		If Err.Number Then
			inspostMGSL004 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	
	
	'% insvalMGSL003: se realizan las validacion de la transaccion MGSL003
	Public Function insvalMGSL003(ByVal dDateProcess As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGSL003_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateProcess = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL003", 55581)
			End If
			
			insvalMGSL003 = .Confirm
		End With
		
insvalMGSL003_err: 
		If Err.Number Then
			insvalMGSL003 = "insvalMGSL003: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	'% inspostMGSL003: realiza el llamado al proceso
	Public Function inspostMGSL003(ByVal dDateProcess As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL003_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "CREMGSL003"
			.Parameters.Add("dDateProcess", dDateProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			inspostMGSL003 = .Run(False)
		End With
		
inspostMGSL003_err: 
		If Err.Number Then
			inspostMGSL003 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	
	
	'% insvalMGSL004: se realizan las validacion de la transaccion MGSL002
	Public Function insvalMGSL007(ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim dDate As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValMGSL007_Err
		
		'+ Se valida que el campo año este lleno
		If nYear = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("MGSL007", 36227)
		End If
		
		'+ Se validar que el campo mes este lleno
		If (nMonth = eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage("MGSL007", 36227)
		End If
		
		'+ Se valida que los campos mes y año sean válidos, se utiliza 28 por si es Febrero
		dDate = "28/" & nMonth & "/" & nYear
		If (Not IsDate(dDate)) Then
			Call lobjErrors.ErrorMessage("MGSL007", 1023)
		End If
		
		insvalMGSL007 = lobjErrors.Confirm
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValMGSL007_Err: 
		If Err.Number Then
			insvalMGSL007 = "insvalMGSL007: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	Public Function inspostMGSL006(ByVal dProcessIni As Date, ByVal dProcessEnd As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL006_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "CREMGSL006"
			.Parameters.Add("dProcessIni", dProcessIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProcessEnd", dProcessEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				inspostMGSL006 = True
				Me.sKey = .Parameters("sKey").Value
			End If
		End With
		
inspostMGSL006_err: 
		If Err.Number Then
			inspostMGSL006 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	
	'% inspostMGSL007: realiza el llamado al proceso
	Public Function inspostMGSL007(ByVal nInsurArea As Integer, ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nTabletyp As Integer, ByVal nNMode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL007_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "INSTMP_MGSL007"
			.Parameters.Add("nInsurArea", nInsurArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTableTyp", nTabletyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNMode", nNMode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				inspostMGSL007 = True
				Me.sKey = .Parameters("sKey").Value
			End If
		End With
		
inspostMGSL007_err: 
		If Err.Number Then
			inspostMGSL007 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% inspostMGSL009: realiza el llamado al proceso
	Public Function inspostMGSL009(ByVal nTypeProc As Integer, ByVal nUsercode As Integer, ByVal dProcessIni As Date, ByVal dProcessEnd As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL009_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insUpdCRL001"
			.Parameters.Add("dDateTo", dProcessEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateInit", dProcessIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProc", nTypeProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				inspostMGSL009 = True
			End If
		End With
		
inspostMGSL009_err: 
		If Err.Number Then
			inspostMGSL009 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	
	'% insvalMGSL001: se realizan las validacion de la transaccion MGSL002
	Public Function insvalMGSL010(ByVal dDateInit As Date, ByVal dDateEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGSL010_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateInit = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL010", 9071)
			End If
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage("MGSL010", 9072)
			End If
			
			'+ Debe existir por lo menos un registro en la grilla
			If dDateInit > dDateEnd Then
				Call .ErrorMessage("MGSL010", 3240)
			End If
			
			insvalMGSL010 = .Confirm
		End With
		
insvalMGSL010_err: 
		If Err.Number Then
			insvalMGSL010 = "insvalMGSL010: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	Public Function inspostMGSL010(ByVal dDateInit As Date, ByVal dDateEnd As Date, ByVal nUsercode As Integer, ByVal nCompany As Integer, ByVal sExecute As String, ByVal nTypeProc As Integer, ByVal nInsur_area As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo inspostMGSL010_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insUpdCRL002_2"
			.Parameters.Add("dDateFrom", dDateInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProc", nTypeProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				inspostMGSL010 = True
				Me.sKey = .Parameters("sKey").Value
			End If
		End With
		
inspostMGSL010_err: 
		If Err.Number Then
			inspostMGSL010 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






