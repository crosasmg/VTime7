Option Strict Off
Option Explicit On
Public Class Collector
	'%-------------------------------------------------------%'
	'% $Workfile:: Collector.cls                            $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 29/09/03 11.44                               $%'
	'% $Revision:: 32                                       $%'
	'%-------------------------------------------------------%'
	
	' Desarrollado por: Victor Gajardo
	' Fecha: 27-11-20001
	' Descripcion: Actualización Cobradores
	
	'+
	'+ Estructura de tabla insudb.collector al 11-27-2001 18:50:12
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nCollector As Double ' NUMBER     22   0     10   N
	Public sClient As String ' CHAR       14   0     0    N
	Public nCollectorType As Integer ' NUMBER     22   0     5    N
	Public dInputDate As Date ' DATE       7    0     0    S
	Public nConType As Integer ' NUMBER     22   0     5    N
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nCode As Integer ' NUMBER     22   0     5    N
	Public nLegal_Sch As Integer ' NUMBER     22   0     5    N
	
	'   Variables locales
	Public nAction As Integer
	
	'   Propiedade auxiliares
	Public sCollectorName As String
	'% insValCO685_K: se realizan las validaciones de la pagina  CO685
	'                 de la Tabla de Cobradores Collector
	Public Function insValCO685_K(ByVal sCodispl As String, ByVal nAction As Integer, ByRef nCollector As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		lclsErrors = New eFunctions.Errors
		On Error GoTo insValCO685_K_Err
		
		lblnError = False
		
		If nAction = 401 Or nAction = 302 Or nAction = 303 Then
			If nCollector = 0 Or nCollector = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60272)
				lblnError = True
			Else
				If Not Find(nCollector, "") Then
					Call lclsErrors.ErrorMessage(sCodispl, 10012)
					lblnError = True
				Else
					If nAction = 303 Then
						If Not InsValDelCollector(nCollector) Then
							Call lclsErrors.ErrorMessage(sCodispl, 100011)
						End If
					End If
				End If
			End If
		End If
		
		' Fin validación encabezado
		insValCO685_K = lclsErrors.Confirm
		
insValCO685_K_Err: 
		If Err.Number Then
			insValCO685_K = insValCO685_K & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'% insValCO685: se realizan las validaciones de la pagina  CO685
	'               de la Tabla de Cobradores Collector
	Public Function insValCO685(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nCollector As Double = 0, Optional ByVal sClient As String = "", Optional ByVal nCollectorType As Integer = 0, Optional ByVal dInputDate As Date = #12:00:00 AM#, Optional ByVal nConType As Integer = 0, Optional ByVal nInsur_area As Integer = 0, Optional ByVal nCode As Integer = 0, Optional ByVal nLegal_Sch As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClient As eClient.Client
		Dim lblnError As Boolean
		
		On Error GoTo insValCO685_Err
		lclsErrors = New eFunctions.Errors
		
		lblnError = False
		
		If nAction = 401 Or nAction = 301 Or nAction = 302 Then
			If nCollector = 0 Or nCollector = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60272)
				lblnError = True
			End If
		End If
		If sClient = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60273)
			lblnError = True
		Else
			'+      Debe corresponder a un codigo de cliente registrado en el sistema
			If (Trim(sClient) <> String.Empty) And Not lblnError Then
				lclsClient = New eClient.Client
				If Not (lclsClient.Find(sClient, True)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 1007)
					lblnError = True
				End If
				'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsClient = Nothing
			End If
		End If
		If dInputDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9013)
			lblnError = True
		End If
		If (nCollectorType = 0 Or nCollectorType = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55571)
			lblnError = True
		End If
		If (nConType = 0 Or nConType = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60274)
			lblnError = True
		End If
		If (nInsur_area = 0 Or nInsur_area = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55031)
			lblnError = True
		End If
		'+ Validaciones para el número de tabla
		If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		Else
			If FindTab_Collect(nCode) = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10012)
			End If
		End If
		'+ Validación pata el régimen tributario
		If nLegal_Sch = eRemoteDB.Constants.intNull Or nLegal_Sch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60377)
		End If
		' Fin validación encabezado
		insValCO685 = lclsErrors.Confirm
		
insValCO685_Err: 
		If Err.Number Then
			insValCO685 = insValCO685 & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	
	'% FindTab_Collect: se valida que la tabla exista
	Public Function FindTab_Collect(ByVal nCode As Integer) As Integer
		Dim lrecreaCollector As eRemoteDB.Execute
		Dim nCode_aux As Integer
		
		FindTab_Collect = 0
		
		On Error GoTo Find_Err
		
		lrecreaCollector = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCollector'
		'+ Información leída el 28/11/2001 17:00:00
		
		With lrecreaCollector
			.StoredProcedure = "reatab_collectcomm"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_Aux", nCode_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				FindTab_Collect = .Parameters.Item("nCode_aux").Value
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			FindTab_Collect = 0
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCollector = Nothing
		
	End Function
	
	'% insPostCO685: se realizan la actualizacion del registro de la pagina CO685
	'               de la Tabla de Cobradores Collector
	Public Function insPostCO685(ByVal nAction As Integer, Optional ByVal nCollector As Double = 0, Optional ByVal sClient As String = "", Optional ByVal nCollectorType As Integer = 0, Optional ByVal dInputDate As Date = #12:00:00 AM#, Optional ByVal nConType As Integer = 0, Optional ByVal nInsur_area As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nCode As Integer = 0, Optional ByVal nLegal_Sch As Integer = 0) As Boolean
		On Error GoTo insPostCO685_Err
		Me.nAction = nAction
		Me.nCollector = nCollector
		Me.sClient = sClient
		Me.nCollectorType = nCollectorType
		Me.dInputDate = dInputDate
		Me.nConType = nConType
		Me.nInsur_area = nInsur_area
		Me.nUsercode = nUsercode
		Me.nCode = nCode
		Me.nLegal_Sch = nLegal_Sch
		
		Select Case nAction
			
			Case 301
				insPostCO685 = Add()
			Case 302
				insPostCO685 = Update()
			Case 303
				insPostCO685 = Del()
			Case Else
				insPostCO685 = True
		End Select
		
		
insPostCO685_Err: 
		If Err.Number Then
			insPostCO685 = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'% Find: Busca la información de un determinado cobrador en tabla Collector
	'%       esta puede ser por codigo cobrador o rut cliente.
	Public Function Find(Optional ByVal nCollector As Double = 0, Optional ByVal sClient As String = "") As Boolean
		Dim lrecreaCollector As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCollector = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCollector'
		'+ Información leída el 28/11/2001 17:00:00
		
		With lrecreaCollector
			.StoredProcedure = "reaCollector"
			
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nCollector = .FieldToClass("nCollector")
				Me.sClient = .FieldToClass("sClient")
				Me.nCollectorType = .FieldToClass("nCollectortype")
				Me.dInputDate = .FieldToClass("dInputdate")
				Me.nConType = .FieldToClass("nContype")
				Me.nInsur_area = .FieldToClass("nInsur_area")
				Me.dCompdate = .FieldToClass("dCompdate")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.sCollectorName = .FieldToClass("sCliename")
				Me.nCode = .FieldToClass("nCode")
				Me.nLegal_Sch = .FieldToClass("nLegal_Sch")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCollector = Nothing
		
	End Function
	
	
	'% Add: Agregar registro en tabla Collector
	Public Function Add() As Boolean
		Dim lrecCollector As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecCollector = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creCollector'
		'+ Información leída el 28/11/2001 17:00:00
		
		With lrecCollector
			.StoredProcedure = "creCollector"
			
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInputdate", dInputDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLegal_Sch", nLegal_Sch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
			
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollector = Nothing
		
	End Function
	'% Del :  Borra un registro de Collector
	Public Function Del() As Boolean
		
		Dim lrecCollector As eRemoteDB.Execute
		
		On Error GoTo Del_err
		
		lrecCollector = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delCollector'
		'+ Información leída el 28/11/2001 17:00:00
		
		With lrecCollector
			.StoredProcedure = "delCollector"
			
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Del = .Run(False)
			
		End With
		
Del_err: 
		If Err.Number Then
			Del = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollector = Nothing
		
	End Function
	
	
	'% Update: Modifica un registro en tabla Collector
	Public Function Update() As Boolean
		
		Dim lrecCollector As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecCollector = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updCollector'
		'+ Información leída el 28/11/2001 17:00:00
		
		With lrecCollector
			.StoredProcedure = "updCollector"
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInputdate", dInputDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLegal_Sch", nLegal_Sch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollector = Nothing
		
	End Function
	
	'% GetNewCollectorCode: obtiene el nuevo número del Cobrador .
	Public Function GetNewCollectorCode(ByVal nUsercode As Integer) As Integer
		Dim lclsGeneral As eGeneral.GeneralFunction
		Dim llngCollector As Double
		
		On Error GoTo GetNewCollectorCode_Err
		lclsGeneral = New eGeneral.GeneralFunction
		llngCollector = lclsGeneral.Find_Numerator(61, 0, nUsercode)
		GetNewCollectorCode = llngCollector
		
GetNewCollectorCode_Err: 
		If Err.Number Then
			GetNewCollectorCode = -1
		End If
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		On Error GoTo 0
	End Function
	'% InsValDelCollector :  Verifica que se pueda eliminar un cobrador
	Public Function InsValDelCollector(ByVal nCollector As Double) As Boolean
		Dim lrecInsValDelCollector As eRemoteDB.Execute
		On Error GoTo InsValDelCollector_err
		lrecInsValDelCollector = New eRemoteDB.Execute
		
		With lrecInsValDelCollector
			.StoredProcedure = "InsValDelCollector"
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCanDelete", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValDelCollector = .Parameters("nCanDelete").Value > 0
			Else
				InsValDelCollector = False
			End If
		End With
		
InsValDelCollector_err: 
		
		If Err.Number Then
			InsValDelCollector = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsValDelCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValDelCollector = Nothing
		
	End Function
End Class






