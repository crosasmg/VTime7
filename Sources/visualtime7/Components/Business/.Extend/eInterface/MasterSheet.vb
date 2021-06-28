Option Strict Off
Option Explicit On
Public Class MasterSheet
	'+
	'+ Estructura de tabla insudb.mastersheet al 06-14-2004
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSheet As Integer 'NUMBER (5)    NOT NULL,
	Public sDescript As String 'VARCHAR2 (30) NOT NULL,
	Public sShortDesc As String 'VARCHAR2 (8)  NOT NULL,
	Public nIntertype As Integer 'NUMBER (5)    NOT NULL,
	Public nOpertype As Integer 'NUMBER (5),
	Public sOpertype As String
	Public sProcess As String 'VARCHAR2 (30),
	Public nFormat As Integer 'NUMBER (5)    NOT NULL,
	Public sFormat As String
	Public nSystem As Integer 'NUMBER (5)    NOT NULL,
	Public sAutomatic As String 'VARCHAR2(1)    NOT NULL,
	Public sOnLine As String 'CHAR(1)    NOT NULL,
	Public sGroupby As String 'VARCHAR2(1)    NOT NULL,
	Public sSelect As String 'VARCHAR2 (500),
	Public nPeriod As Integer 'NUMBER (5),
	Public sPeriod As String
	Public nUseroper As Integer 'NUMBER (5),
	Public sStatusSheet As String 'CHAR(1),
	Public ssStatussheet As String
	Public nUsercode As Integer 'NUMBER (5),
	Public sPrefix_fname As String 'CHAR(9)
	Public sSeparator As String 'CHAR(1)
	Public sSpace As String 'char(1)
	Public sHeader As String 'CHAR(1)
	Public sTotal As String 'CHAR(1)
	Public nAling As Short 'int
	Public nPosition As Short
	Public sMassive As String
	Public sNogrid As String
	Public sView_interface As String
	Public sView_Report As String
	Public sReport As String
	Public sSheet_father As String
	Public sFile_unique As String
	Public sQuery As String
	Public sXsl As String
	Public sQuery_xsl As String
	Public sName_routine As String
	Public sOut_routine As String
	Public sWorkflowname As String
    Public sFolder As String

    Public sQueQuery As String
    Public sQueProcess As String
	
	
	
	
	'%InsUpdMasterSheet: Se encarga de actualizar la tabla MasterSheet
	Private Function ValExist_MasterSheet(ByVal nSheet As Integer) As Short
		'0:NOEXISTE --- 1:EXISTE --- 2:ERROR
		Dim lrecValExist_MasterSheet As eRemoteDB.Execute
		
		On Error GoTo ValExist_MasterSheet_Err
		
		lrecValExist_MasterSheet = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdMasterSheet
		'+
		With lrecValExist_MasterSheet
			.StoredProcedure = "ValExist_MasterSheet"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			ValExist_MasterSheet = .Parameters("nExists").Value
			
		End With
		
ValExist_MasterSheet_Err: 
		If Err.Number Then
			ValExist_MasterSheet = 2
		End If
		'UPGRADE_NOTE: Object lrecValExist_MasterSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValExist_MasterSheet = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsUpdMasterSheet: Se encarga de actualizar la tabla MasterSheet
	Private Function InsUpdMasterSheet_K(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdMasterSheet_K As eRemoteDB.Execute
		
		On Error GoTo insUpdMasterSheet_K_Err
		
		lrecinsUpdMasterSheet_K = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdMasterSheet
		'+
		With lrecinsUpdMasterSheet_K
			.StoredProcedure = "insUpdMasterSheet_k"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShortDesc", sShortDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertype", nIntertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpertype", nOpertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFormat", nFormat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSystem", nSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomatic", sAutomatic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOnline", sOnLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGroupby", sGroupby, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSelect", sSelect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriod", nPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUseroper", nUseroper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatussheet", sStatusSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrefix_fname", sPrefix_fname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 9, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSeparator", sSeparator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Alig", nAling, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSpace", sSpace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHeader", sHeader, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTotal", sTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNogrid", sNogrid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sView_interface", sView_interface, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sView_Report", sView_Report, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSheet_father", sSheet_father, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFile_unique", sFile_unique, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQuery", sQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sXsl", sXsl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sQuery_xsl", sQuery_xsl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_routine", sName_routine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOut_routine", sOut_routine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWorkflowname", sWorkflowname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFolder", sFolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sQueProcess", sQueProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sQueQuery", sQueQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdMasterSheet_K = .Run(False)
		End With
		
insUpdMasterSheet_K_Err: 
		If Err.Number Then
			InsUpdMasterSheet_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdMasterSheet_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdMasterSheet_K = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla MasterSheet
	Public Function Add_K() As Boolean
		Add_K = InsUpdMasterSheet_K(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla MasterSheet
	Public Function Update_K() As Boolean
		Update_K = InsUpdMasterSheet_K(2)
	End Function
	
	'%Delete: Borra un registro en la tabla MasterSheet
	Public Function Delete_K() As Boolean
		Delete_K = InsUpdMasterSheet_K(3)
	End Function
	
	'%InsPostMGI1401_K: Ejecuta el post de la transacción
	'%                 Mantencion de Interfaces (HEADER)
	Public Function InsPostMGI1401_K(ByVal nAction As Integer, ByVal nSheet As Integer, ByVal sDescript As String, ByVal sShortDesc As String, ByVal nIntertype As Integer, ByVal nOpertype As Integer, ByVal sProcess As String, ByVal nFormat As Integer, ByVal nSystem As Integer, ByVal sAutomatic As String, ByVal sOnLine As String, ByVal sGroupby As String, ByVal nUsercode As Integer, ByVal nPeriod As Integer, Optional ByVal sSelect As String = "", Optional ByVal nUseroper As Integer = 0) As Boolean
		On Error GoTo InsPostMGI1401_K_Err
		
		With Me
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				.Find(nSheet)
			End If
			.nSheet = nSheet
			.sDescript = sDescript
			.sShortDesc = sShortDesc
			.nIntertype = nIntertype
			.nOpertype = nOpertype
			.sProcess = sProcess
			.nFormat = nFormat
			.nSystem = nSystem
			.sAutomatic = IIf(sAutomatic = strNull, "2", sAutomatic)
			.sOnLine = IIf(sOnLine = strNull, "2", sOnLine)
			.sGroupby = IIf(sGroupby = strNull, "2", sGroupby)
			.sSelect = sSelect
			.nPeriod = nPeriod
			.nUseroper = nUseroper
			.nUsercode = nUsercode
			
			Select Case nAction
				Case eFunctions.Menues.TypeActions.clngActionadd
					.sStatusSheet = "2"
					InsPostMGI1401_K = Add_K
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					InsPostMGI1401_K = Update_K
				Case eFunctions.Menues.TypeActions.clngActioncut
					InsPostMGI1401_K = Delete_K
			End Select
		End With
		
		
InsPostMGI1401_K_Err: 
		If Err.Number Then
			InsPostMGI1401_K = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nSheet = numNull
		sDescript = strNull
		sShortDesc = strNull
		nIntertype = numNull
		nOpertype = numNull
		sOpertype = strNull
		sProcess = strNull
		nFormat = numNull
		sFormat = strNull
		nSystem = numNull
		sAutomatic = strNull
		sOnLine = strNull
		sGroupby = strNull
		sSelect = strNull
		nPeriod = numNull
		sPeriod = strNull
		nUseroper = numNull
		sStatusSheet = strNull
		nUsercode = numNull
		sSeparator = strNull
		sSpace = strNull
		sTotal = strNull
		sHeader = strNull
		nAling = intNull
		nPosition = intNull
		sMassive = strNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'% insValMGI1401_k: Valida los datos introducidos en el Header
	'-------------------------------------------------------------
	Public Function insValMGI1401_K(ByVal sCodispl As String, ByVal nSheet As Integer, ByVal sDescript As String, ByVal nFormat As Integer, ByVal nAction As Integer, ByVal nPeriod As Integer, ByVal sAutomatic As String, ByVal sShortDesc As String) As String
		'-------------------------------------------------------------
		Dim lclsErrors As eFunctions.Errors
		Dim lintExist As Short
		
		On Error GoTo insValMGI1401_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Validacion de Existencia del nSheet
			lintExist = ValExist_MasterSheet(nSheet)
			If nAction = eFunctions.Menues.TypeActions.clngActionadd And lintExist = 1 Then
				.ErrorMessage(sCodispl, 10004)
				
			ElseIf nAction = eFunctions.Menues.TypeActions.clngActionUpdate And lintExist = 0 Then 
				.ErrorMessage(sCodispl, 10012)
			End If
			
			'+ Validación del campo "Codigo de Interfaz"
			If nSheet = numNull Then
				.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código de Interfaz")
			End If
			
			'+ Validación del campo "Nombre de Interfaz"
			If sDescript = strNull Then
				.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre de Interfaz")
			End If
			
			'+ Validación del campo "Descripción corta de la plantilla de interfaz"
			If sShortDesc = strNull Then
				.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Descripción corta de la plantilla de interfaz")
			End If
			
			'+ Validación del campo "Formato"
			If nFormat = numNull Then
				.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Formato de Interfaz")
			End If
			
			'+ Validacion, Si la interfaz es automatica la Periodicidad debe estar llena.
			If sAutomatic = "1" Then
				If nPeriod = numNull Then
					.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Periodicidad")
				End If
			End If
			
			insValMGI1401_K = .Confirm
		End With
		
insValMGI1401_K_Err: 
		If Err.Number Then
			insValMGI1401_K = "insValMGI1401_K: " & Err.Description
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
	
	
	'%LoadTabs: Arma la secuencia Mantenimiento de Interfaces
	Public Function LoadTabsMasterSheet(ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nSheet As Integer) As Object
		Const CN_MASTERSHEET As String = "MGI1405 MGI1406 MGI1407 MGI1410 MGI1408"
		Dim lrecWindows As eRemoteDB.Query
		Dim lclsSecurSche As Object
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lintCountWindows As Integer
        Dim lstrCodisp As String
        Dim lstrCodispl As String
        Dim lstrShort_desc As String
        Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		Dim lstrHTMLCode As String
		Dim lclsSequence As eFunctions.Sequence
		Dim lstrWindows As String
		Dim lblnShow As Boolean
		
		Dim ldblnotenum As Double
		Dim ldblImageNum As Double
		
		Dim lobjTables As Object
		
		On Error GoTo LoadTabsMasterSheet_Err
		
		
		lclsSecurSche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		
		lstrHTMLCode = String.Empty
		lstrWindows = CN_MASTERSHEET
		
		lblnRequired = True
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		
		Do While Trim(lstrCodispl) <> String.Empty
			lblnShow = True
			lblnContent = False
			lblnRequired = False
			lstrCodispl = Trim(lstrCodispl)
			
			lblnRequired = lstrCodispl = "MGI1405" Or lstrCodispl = "MGI1407"
			
			'+ Se asignan los valores a las variables de descripción
			If lrecWindows.OpenQuery("Windows", "sCodisp, sShort_des", "sCodispl='" & lstrCodispl & "'") Then
				lstrCodisp = lrecWindows.FieldToClass("sCodisp")
				lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
				lrecWindows.CloseQuery()
			End If
			
			'+ Se busca la imagen a colocar en los links
			With lclsSecurSche
				If Not .valTransAccess(sUserSchema, lstrCodisp, "2") Then
					If lblnContent Then
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
					Else
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
						End If
					End If
				Else
					
					'+ Se verifica contenido de las ventanas
					Select Case lstrCodispl
						
						'+ MGI1405: Datos generales de Interfaz
						Case "MGI1405"
							Me.Find(nSheet)
							lblnContent = Me.sPrefix_fname <> String.Empty
							
							'+ MGI1406: Tablas de Interfaz
						Case "MGI1406"
							lobjTables = New TableSheets
							lblnContent = lobjTables.Find(nSheet)
							
							'+ MGI1407: Campos de Interfaz
						Case "MGI1407"
							lobjTables = New FieldSheets
							lblnContent = lobjTables.Find(nSheet, numNull)
							
							'+ MGI1410: Interfaces dependientes
						Case "MGI1410"
							lblnShow = Me.sSheet_father = "1"
							If lblnShow Then
								lobjTables = New Depend_Sheets
								lblnContent = lobjTables.Find(nSheet)
								lblnRequired = True
							End If
							
							'+ MGI1408: Calendario
						Case "MGI1408"
							lobjTables = New Calends
							lblnContent = lobjTables.Find(nSheet)
							
					End Select
					
					If Not lblnContent Then
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						End If
					Else
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					End If
				End If
			End With
			If lblnShow Then
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			End If
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		Loop 
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabsMasterSheet = lstrHTMLCode
		
LoadTabsMasterSheet_Err: 
		If Err.Number Then
			LoadTabsMasterSheet = "LoadTabsMasterSheet: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTables = Nothing
		
	End Function
	
	
	'%Find: Lee los datos de la tabla MasterSheet
	Public Function Find(ByVal nSheet As Double) As Boolean
		Dim lrecreaMasterSheet As eRemoteDB.Execute
		Dim lclsMasterSheet As MasterSheet
		
		On Error GoTo reaMasterSheet_Err
		lrecreaMasterSheet = New eRemoteDB.Execute
		With lrecreaMasterSheet
			.StoredProcedure = "reaMasterSheet"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Find = True
				Me.nSheet = nSheet
				Me.nIntertype = .FieldToClass("nIntertype")
				Me.sProcess = .FieldToClass("sProcess")
				Me.nFormat = .FieldToClass("nFormat")
				Me.sFormat = .FieldToClass("sFormat")
				Me.nSystem = .FieldToClass("nSystem")
				Me.sSelect = .FieldToClass("sSelect")
				Me.nPeriod = .FieldToClass("nPeriod")
				Me.sPeriod = .FieldToClass("sPeriod")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.sDescript = .FieldToClass("sDescript")
				Me.nUseroper = .FieldToClass("nUseroper")
				Me.sAutomatic = .FieldToClass("sAutomatic")
				Me.sOnLine = .FieldToClass("sOnline")
				Me.sShortDesc = .FieldToClass("sShortDesc")
				Me.sGroupby = .FieldToClass("sGroupby")
				Me.sStatusSheet = .FieldToClass("sStatussheet")
				Me.ssStatussheet = .FieldToClass("ssstatussheet")
				Me.nOpertype = .FieldToClass("nOpertype")
				Me.sOpertype = .FieldToClass("sOpertype")
				Me.sPrefix_fname = .FieldToClass("sPrefix_fname")
				Me.sSeparator = .FieldToClass("sSeparator")
				Me.sSpace = .FieldToClass("sSpace")
				Me.sTotal = .FieldToClass("sTotal")
				Me.sHeader = .FieldToClass("sHeader")
				Me.nAling = .FieldToClass("nType_align")
				Me.nPosition = .FieldToClass("nPosition")
				Me.sMassive = .FieldToClass("sMassive")
				Me.sNogrid = .FieldToClass("sNogrid")
				Me.sView_interface = .FieldToClass("sView_interface")
				Me.sView_Report = .FieldToClass("sView_Report")
				Me.sReport = .FieldToClass("sReport")
				Me.sSheet_father = .FieldToClass("sSheet_father")
				Me.sFile_unique = .FieldToClass("sFile_unique")
				Me.sQuery = .FieldToClass("sQuery")
				Me.sXsl = .FieldToClass("sXsl")
				Me.sQuery_xsl = .FieldToClass("sQuery_xsl")
				Me.sName_routine = .FieldToClass("sName_routine")
				Me.sOut_routine = .FieldToClass("sOut_routine")
				Me.sWorkflowname = .FieldToClass("sWorkflowname")
                Me.sFolder = .FieldToClass("sFolder")
                Me.sQueProcess = .FieldToClass("sQueProcess")
                Me.sQueQuery = .FieldToClass("sQueQuery")
			End If
		End With
reaMasterSheet_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaMasterSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMasterSheet = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMGI1405: Valida los datos introducidos en el Folder
	'-------------------------------------------------------------
	Public Function insValMGI1405(ByVal sCodispl As String, ByVal nSheet As Integer, ByVal sSheet_father As String, ByVal sStatusSheet As String, ByVal sPrefix_fname As String) As String
		'-------------------------------------------------------------
		Dim lclsErrors As eFunctions.Errors
		Dim lcolDepend_Sheets As Depend_Sheets
		
		On Error GoTo insValMGI1405_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Validación del campo "Estado general del registro"
			If sStatusSheet = strNull Or sStatusSheet = "0" Then
				.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Estado")
			End If
			
			'+Validacion del campo pre-sufijo
			If sPrefix_fname = String.Empty Then
				.ErrorMessage(sCodispl, 55026,  , eFunctions.Errors.TextAlign.RigthAling, "Prefijo|Sufijo")
			End If
			
			'+La interfaz es padre y tiene interfaces dependientes debe eliminarlas
			If sSheet_father = String.Empty Then
				lcolDepend_Sheets = New Depend_Sheets
				If lcolDepend_Sheets.Find(CStr(nSheet)) Then
					.ErrorMessage(sCodispl, 11231)
				End If
				'UPGRADE_NOTE: Object lcolDepend_Sheets may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolDepend_Sheets = Nothing
			End If
		End With
		
		insValMGI1405 = lclsErrors.Confirm
		
insValMGI1405_Err: 
		If Err.Number Then
			insValMGI1405 = "insValMGI1405: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMGI1405: Ejecuta el post de la transacción
    Public Function InsPostMGI1405(ByVal nSheet As Integer, ByVal sStatusSheet As String, ByVal sPrefix_fname As String, ByVal sSeparator As String, ByVal sSpace As String, ByVal nAling As Integer, ByVal sHeader As String, ByVal sTotal As String, ByVal nPosition As Integer, ByVal sMassive As String, ByVal nUsercode As Integer, ByVal sNogrid As String, ByVal sView_interface As String, ByVal sView_Report As String, ByVal sReport As String, ByVal sSheet_father As String, ByVal sFile_unique As String, ByVal sQuery As String, ByVal sXsl As String, ByVal sQuery_xsl As String, ByVal sName_routine As String, ByVal sOut_routine As String, ByVal sWorkflowname As String, ByVal sFolder As String, ByVal sQueProcess As String, ByVal sQueQuery As String) As Boolean
        On Error GoTo InsPostMGI1405_Err

        With Me
            If .Find(nSheet) Then
                .sStatusSheet = sStatusSheet
                .sPrefix_fname = sPrefix_fname
                .sSeparator = sSeparator
                .sSpace = sSpace
                .nAling = nAling
                .sHeader = sHeader
                .sTotal = sTotal
                .nPosition = nPosition
                .sMassive = sMassive
                .nUsercode = nUsercode
                .sNogrid = sNogrid
                .sView_interface = sView_interface
                .sView_Report = sView_Report
                .sReport = sReport
                .sSheet_father = sSheet_father
                .sFile_unique = sFile_unique
                .sQuery = sQuery
                .sXsl = sXsl
                .sQuery_xsl = sQuery_xsl
                .sName_routine = sName_routine
                .sOut_routine = sOut_routine
                .sWorkflowname = sWorkflowname
                .sFolder = sFolder
                .sQueProcess = sQueProcess
                .sQueQuery = sQueQuery
                InsPostMGI1405 = .Update_K
            End If
        End With


InsPostMGI1405_Err:
        If Err.Number Then
            InsPostMGI1405 = False
        End If
        On Error GoTo 0
    End Function
End Class






