Option Strict Off
Option Explicit On
Public Class Homolog_table
	'+
	'+ Estructura de tabla insudb.homolog_table al 06-02-2004
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSystem As Integer ' NUMBER     22   0     5    N
	Public nTable As Integer ' NUMBER     22   0     5    N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public sColumnName_Vt As String ' VARCHAR2   20   0     0    N
	Public sCodValue_Vt As String ' VARCHAR2   20   0     0    N
	Public sValue_Vt As String ' VARCHAR2   60   0     0    S
	Public sTableName As String ' VARCHAR2   20   0     0    S
	Public sColumnName As String ' VARCHAR2   20   0     0    S
	Public sCodValue As String ' VARCHAR2   20   0     0    N
	Public sPredom As String ' CHAR       20   0     1    N
	Public nUsercode As Integer ' NUMERIC    22   0     5    N
	
	'%InsUpdHomolog_Table: Se encarga de actualizar la tabla Homolog_Table
	Private Function InsUpdHomolog_Table(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdHomolog_table As eRemoteDB.Execute
		
		On Error GoTo insUpdHomolog_table_Err
		
		lrecinsUpdHomolog_table = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdHomolog_table al 04-25-2002 16:04:41
		'+
		With lrecinsUpdHomolog_table
			.StoredProcedure = "insUpdHomolog_table"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSystem", nSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnname_Vt", sColumnName_Vt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodvalue_Vt", sCodValue_Vt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue_Vt", sValue_Vt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTablename", sTableName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnname", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodvalue", sCodValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPredom", sPredom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdHomolog_Table = .Run(False)
		End With
		
insUpdHomolog_table_Err: 
		If Err.Number Then
			InsUpdHomolog_Table = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdHomolog_table may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdHomolog_table = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdHomolog_Table(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdHomolog_Table(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdHomolog_Table(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	'Public Function Find(ByVal nServ_Order As Double) As Boolean
	''--------------------------------------------------------------------------'
	'Dim lrecreaHomolog_table As eRemotedb.Execute
	'Dim lclsHomolog_table As Homolog_table
	'
	'On Error GoTo reaHomolog_table_Err
	'
	'    Set lrecreaHomolog_table = New eRemotedb.Execute
	'
	''+
	''+ Definición de store procedure reaHomolog_table al 04-25-2002 16:02:43
	''+
	'    With lrecreaHomolog_table
	'        .StoredProcedure = "reaHomolog_table"
	'        .Parameters.Add "nServ_order", nServ_Order, rdbParamInput, rdbDouble, 22, 0, 10, rdbParamNullable
	'
	'        If .Run(True) Then
	'            Find = True
	'                nServ_Order = nServ_Order
	'                nCon_earthquake = .FieldToClass("nCon_earthquake")
	'                nDamage = .FieldToClass("nDamage")
	'                nContainrisk = .FieldToClass("nContainrisk")
	'                sRiverbed = .FieldToClass("sRiverbed")
	'                nDist_river = .FieldToClass("nDist_river")
	'                sInflu_risk = .FieldToClass("sInflu_risk")
	'                nInundat = .FieldToClass("nInundat")
	'                sStratobj = .FieldToClass("sStratobj")
	'                sTerrefy = .FieldToClass("sTerrefy")
	'                nWaterpipe = .FieldToClass("nWaterpipe")
	'                nDam_waterpipe = .FieldToClass("nDam_waterpipe")
	'                nSewerpipe = .FieldToClass("nSewerpipe")
	'                nDam_sewerpipe = .FieldToClass("nDam_sewerpipe")
	'                nStatroof = .FieldToClass("nStatroof")
	'                nDamroof = .FieldToClass("nDamroof")
	'                sStorm = .FieldToClass("sStorm")
	'                sSnow = .FieldToClass("sSnow")
	'                sShockauto = .FieldToClass("sShockauto")
	'                sFallplane = .FieldToClass("sFallplane")
	'                sWind = .FieldToClass("sWind")
	'                sAirport = .FieldToClass("sAirport")
	'                nDistair = .FieldToClass("nDistair")
	'                sSea = .FieldToClass("sSea")
	'                nDistsea = .FieldToClass("nDistsea")
	'        Else
	'            Find = False
	'        End If
	'    End With
	'
	'reaHomolog_table_Err:
	'    If Err Then
	'        Find = False
	'    End If
	'    Set lrecreaHomolog_table = Nothing
	'    On Error GoTo 0
	'
	'End Function
	
	
	'%InsPostMGI1400: Ejecuta el post de la transacción
	'%               Tabla de Homologacion de Codigos
	Public Function InsPostMGI1400(ByVal sAction As String, ByVal nSystem As Integer, ByVal nTable As Integer, ByVal nId As Integer, ByVal sColumnName_Vt As String, ByVal sCodValue_Vt As String, ByVal sValue_Vt As String, ByVal sTableName As String, ByVal sColumnName As String, ByVal sCodValue As String, ByVal sPredom As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMGI1400_Err
		
		With Me
			.nSystem = nSystem
			.nTable = nTable
			.nId = nId
			.sColumnName_Vt = sColumnName_Vt
			.sCodValue_Vt = sCodValue_Vt
			.sValue_Vt = sValue_Vt
			.sTableName = sTableName
			.sColumnName = sColumnName
			.sCodValue = sCodValue
			If sPredom = strNull Then
				.sPredom = "2"
			Else
				.sPredom = sPredom
			End If
			
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMGI1400 = Add
			Case "Update"
				InsPostMGI1400 = Update
			Case "Del"
				InsPostMGI1400 = Delete
		End Select
		
InsPostMGI1400_Err: 
		If Err.Number Then
			InsPostMGI1400 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nSystem = numNull
		nTable = numNull
		nId = numNull
		sColumnName_Vt = strNull
		sCodValue_Vt = strNull
		sValue_Vt = strNull
		sTableName = strNull
		sColumnName = strNull
		sCodValue = strNull
		sPredom = strNull
		nUsercode = numNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'% insValMGI1400_k: Valida los datos introducidos en el Encabezado
	'-------------------------------------------------------------
	Public Function insValMGI1400_K(ByVal sCodispl As String, ByVal nSystem As Integer, ByVal nTable As Integer) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGI1400_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del campo "Codigo de sistema externo"
		If nSystem = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código de Sistema externo")
		End If
		
		'+ Validación del campo "Codigo de tabla homologada"
		If nTable = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código de Tabla VTime")
		End If
		
		insValMGI1400_K = lclsErrors.Confirm
		
insValMGI1400_K_Err: 
		If Err.Number Then
			insValMGI1400_K = lclsErrors.Confirm & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'% insValMGI1400: Valida los datos introducidos en el Folder
	'-------------------------------------------------------------
	Public Function insValMGI1400(ByVal sCodispl As String, ByVal nId As Integer, ByVal sColumnName_Vt As String, ByVal sCodValue_Vt As String, ByVal sValue_Vt As String, ByVal sTableName As String, ByVal sColumnName As String, ByVal sCodValue As String, ByVal sPredom As String) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		
		
		On Error GoTo insValMGI1400_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del campo "Nombre del campo en vtime"
		If sColumnName_Vt = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre del campo en Visual Time")
		End If
		
		'+ Validación del campo "Valor del campo en vtime"
		If sCodValue_Vt = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código en Visual Time")
		End If
		
		'+ Validación del campo "Valor del campo en Sistema Externo"
		If sCodValue = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código en sistema externo")
		End If
		
		insValMGI1400 = lclsErrors.Confirm
		
insValMGI1400_Err: 
		If Err.Number Then
			insValMGI1400 = lclsErrors.Confirm & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsCalId: Se encarga de Rescatar correlativo desde la tabla Homolog_Table
	Public Function InsCalId(ByVal nSystem As Integer, ByVal nTable As Integer) As Integer
		Dim lrecInsCalId As eRemoteDB.Execute
		
		On Error GoTo InsCalId_Err
		
		lrecInsCalId = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdHomolog_table al 04-25-2002 16:04:41
		'+
		With lrecInsCalId
			.StoredProcedure = "InsCalId"
			.Parameters.Add("nSystem", nSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nId", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			InsCalId = .Parameters("nId").Value
			
		End With
		
InsCalId_Err: 
		If Err.Number Then
			InsCalId = 0
		End If
		'UPGRADE_NOTE: Object lrecInsCalId may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalId = Nothing
		On Error GoTo 0
	End Function
End Class






