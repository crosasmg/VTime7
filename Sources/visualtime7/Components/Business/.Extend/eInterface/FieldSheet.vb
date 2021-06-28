Option Strict Off
Option Explicit On
Public Class FieldSheet
	'+
	'+ Estructura de tabla insudb.FieldToClassheet al 06-22-2004
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSheet As Integer 'NUMBER(5)                     NOT NULL,
	Public nField As Integer 'NUMBER(5)                       NOT NULL,
	Public nFieldType As Integer 'NUMBER(5)                       NOT NULL,
	Public sTable As String 'VARCHAR2(40 BYTE),
	Public nUsercode As Integer 'NUMBER(5)                       NOT NULL,
	Public sFieldDesc As String 'VARCHAR2(30 BYTE)               NOT NULL,
	Public sColumnName As String 'VARCHAR2(30 BYTE),
	Public sValue As String 'VARCHAR2(40 BYTE),
	Public sRutine As String 'VARCHAR2(40 BYTE),
	Public sValueRutine As String 'VARCHAR2(40 BYTE),
	Public nRoworder As Integer 'NUMBER(5),
	Public nFieldOrder As Integer 'NUMBER(5)                       NOT NULL,
	Public sValueslist As String 'VARCHAR2(80 BYTE),
	Public nDataType As Integer 'NUMBER(5),
	Public nFieldLarge As Integer 'NUMBER(5),
	Public nObjtype As Integer 'NUMBER(5),
	Public nTablehomo As Integer 'NUMBER(5),
	Public nOperator As Integer 'NUMBER(5),
	Public nCondit As Integer 'NUMBER(5),
	Public sFieldCommen As String 'VARCHAR2(50 BYTE),
	Public sFieldrel As String 'VARCHAR2(100),
	Public sObligatory As String 'CHAR(1),                       NOT NULL
	Public sLastmove As String 'CHAR(1 BYTE)                    NOT NULL
    Public nDecimal As Integer

    '+ Campos adicionales para el manejo de TABLAS DINAMICAS DE CERTIFICADOS
    Public sValue2 As String
    Public nValue As Double
    Public dValue As Date
	
	'%InsUpdFieldSheet: Se encarga de actualizar la tabla FieldSheet
	Private Function InsUpdFieldSheet(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdFieldSheet As eRemoteDB.Execute
		
		On Error GoTo insUpdFieldSheet_Err
		
		lrecinsUpdFieldSheet = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdFieldSheet
		'+
		With lrecinsUpdFieldSheet
			.StoredProcedure = "insUpdFieldSheet"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldtype", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFielddesc", sFieldDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnname", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRutine", sRutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoworder", nRoworder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldorder", nFieldOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValueslist", sValueslist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDatatype", nDataType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldlarge", nFieldLarge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nObjtype", nObjtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTablehomo", nTablehomo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOperator", nOperator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCondit", nCondit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFieldcommen", sFieldCommen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFieldrel", sFieldrel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sObligatory", sObligatory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastmove", sLastmove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDecimal", nDecimal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdFieldSheet = .Run(False)
		End With
		
insUpdFieldSheet_Err: 
		If Err.Number Then
			InsUpdFieldSheet = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdFieldSheet = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdFieldSheet(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdFieldSheet(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdFieldSheet(3)
	End Function
	
	'%InsPostMGI1407: Ejecuta el post de la transacción
	Public Function InsPostMGI1407(ByVal sAction As String, ByVal nSheet As Integer, ByVal nField As Integer, ByVal nFieldType As Integer, ByVal sTable As String, ByVal nUsercode As Integer, ByVal sFieldDesc As String, ByVal sColumnName As String, ByVal sValue As String, ByVal sRutine As String, ByVal nRoworder As Integer, ByVal nFieldOrder As Integer, ByVal sValueslist As String, ByVal nDataType As Integer, ByVal nFieldLarge As Integer, ByVal nObjtype As Integer, ByVal nTablehomo As Integer, ByVal nOperator As Integer, ByVal nCondit As Integer, ByVal sFieldCommen As String, ByVal sFieldrel As String, ByVal sObligatory As String, ByVal sLastmove As String, Optional ByRef nDecimal As Integer = 0) As Boolean
		On Error GoTo InsPostMGI1407_Err
		With Me
			.nSheet = nSheet
			.nField = nField
			.nFieldType = nFieldType
			.sTable = sTable
			.nUsercode = nUsercode
			.sFieldDesc = sFieldDesc
			.sColumnName = sColumnName
			.sValue = sValue
			.sRutine = sRutine
			.nRoworder = nRoworder
			.nFieldOrder = nFieldOrder
			.sValueslist = sValueslist
			.nDataType = nDataType
			.nFieldLarge = nFieldLarge
			.nObjtype = nObjtype
			.nTablehomo = nTablehomo
			.nOperator = nOperator
			.nCondit = nCondit
			.sFieldCommen = sFieldCommen
			.sFieldrel = sFieldrel
			.sObligatory = sObligatory
			.sLastmove = sLastmove
			.nDecimal = nDecimal
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMGI1407 = Add
			Case "Update"
				InsPostMGI1407 = Update
			Case "Del"
				InsPostMGI1407 = Delete
		End Select
		
InsPostMGI1407_Err: 
		If Err.Number Then
			InsPostMGI1407 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nSheet = numNull
		nField = numNull
		nFieldType = numNull
		sTable = strNull
		nUsercode = numNull
		sFieldDesc = strNull
		sColumnName = strNull
		sValue = strNull
		sRutine = strNull
		nRoworder = numNull
		nFieldOrder = numNull
		sValueslist = strNull
		nDataType = numNull
		nFieldLarge = numNull
		nObjtype = numNull
		nTablehomo = numNull
		nOperator = numNull
		nCondit = numNull
		sFieldCommen = strNull
		sFieldrel = strNull
		sObligatory = strNull
		sLastmove = strNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% insValMGI1407: Valida los datos introducidos
	'-------------------------------------------------------------
	Public Function InsValMGI1407(ByVal sCodispl As String, ByVal sAction As String, ByVal nSheet As Integer, ByVal nField As Integer, ByVal nFieldType As Integer, ByVal sFieldDesc As String, ByVal sTable As String, ByVal sColumnName As String, ByVal nFieldOrder As Integer, ByVal nDataType As Integer, ByVal nFieldLarge As Integer, ByVal sObligatory As String, ByVal sFieldCommen As String, ByVal nOperator As Integer) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGI1407_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del campo "Código de campo en la platilla de interfaz"
		If nField = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Código de campo en la platilla de interfaz")
		Else
			'+ Validación: la combinacion nSheet + nField + nFieldType debe ser unico
			If sAction = "Add" Then
				If InsValnFieldType(nSheet, nField, nFieldType) = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 750102)
				End If
			End If
		End If
		
		'+ Validación del campo "Nombre o descripción del campo"
		If sFieldDesc = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre o descripción del campo")
		End If
		
		'+ Validación del campo "TablaBD", si campoBD tiene valor debe estar lleno
		If sColumnName <> strNull Then
			If sTable = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre de la tabla a la cual pertenece el campo")
			End If
		End If
		
		'+ Validación del campo "CampoBD", si tipo de campo es CONDICION (nFieldType=1) debe estar lleno
		If nFieldType = 1 Then
			If sColumnName = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Nombre que tiene la columna en la base de datos")
			End If
		End If
		
		'+ Validación del campo "Orden campo", debe estar lleno
		If nFieldOrder = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Orden de aparición de la columna en la plantilla")
		End If
		
		'+ Validación del campo "Obligatoriedad"
		If sObligatory = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Obligatoriedad")
		End If
		
		'+ Validación del campo "Comentario", si tipo de campo es PARAMETRO (nFieldType=3) debe estar lleno
		If nFieldType = 3 Then
			If sFieldCommen = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Comentario asociado al campo")
			End If
		End If
		
		'+ Validacion Tamaño, si tipocampo = "dato" y campoBD tiene valor, el tamaño del campo "dato"
		'+ no debe ser mayor al tamaño que efectivamente tiene el campo en la BD.
		If nFieldType = 2 Then
			If sColumnName <> strNull And sTable <> strNull Then
				'+ Si tipo de dato de campo es datetime, el tamaño debe ser 10, esto lo definio el analista 01-09-2004
				If nDataType <> 3 Then
					If nFieldLarge > InsValFieldSheet(sTable, sColumnName) Then
						Call lclsErrors.ErrorMessage(sCodispl, 750101)
					End If
				Else
					If (nFieldLarge <> 10 And nFieldLarge <> 8) Then
						Call lclsErrors.ErrorMessage(sCodispl, 750101)
					End If
				End If
			End If
		End If
		
		'+ Validacion Tamaño, si tipocampo = "dato" el tamaño del campo > CERO
		If nFieldType = 2 Then
			If nFieldLarge < 1 Then
				Call lclsErrors.ErrorMessage(sCodispl, 700011,  , eFunctions.Errors.TextAlign.LeftAling, "Tamaño ")
			End If
		End If
		
		'+ Validacion Tipo de Dato, si tipocampo = "dato" el tipo de dato no puede ser null
		If nFieldType = 2 Then
			If nDataType < 1 Then
				Call lclsErrors.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.RigthAling, "Tipo de dato")
			End If
		End If
		
		InsValMGI1407 = lclsErrors.Confirm
		
insValMGI1407_Err: 
		If Err.Number Then
			InsValMGI1407 = lclsErrors.Confirm & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValFieldSheet: Si TipoCampo="DATO" y CampoBD tiene valor, el Tamaño del campo
	'% "dato" no puede ser > al que efectivamente tiene el campo en la BD.
	Public Function InsValFieldSheet(ByVal sTable As String, ByVal sColumnName As String) As Integer
		Dim lrecInsValFieldSheet As eRemoteDB.Execute
		
		On Error GoTo InsValFieldSheet_Err
		
		lrecInsValFieldSheet = New eRemoteDB.Execute
		
		With lrecInsValFieldSheet
			.StoredProcedure = "InsValFieldSheet"
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nFieldLarge", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			InsValFieldSheet = .Parameters("nFieldLarge").Value
			
		End With
		
InsValFieldSheet_Err: 
		If Err.Number Then
			InsValFieldSheet = 0
		End If
		'UPGRADE_NOTE: Object lrecInsValFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValFieldSheet = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValnFieldType:
	Public Function InsValnFieldType(ByVal nSheet As Integer, ByVal nField As Integer, ByVal nFieldType As Integer) As Short
		Dim lrecInsValnFieldType As eRemoteDB.Execute
		
		On Error GoTo InsValnFieldType_Err
		
		lrecInsValnFieldType = New eRemoteDB.Execute
		
		With lrecInsValnFieldType
			.StoredProcedure = "InsValnFieldType"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldType", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			InsValnFieldType = .Parameters("nExists").Value
			
		End With
		
InsValnFieldType_Err: 
		If Err.Number Then
			InsValnFieldType = 0
		End If
		'UPGRADE_NOTE: Object lrecInsValnFieldType may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValnFieldType = Nothing
		On Error GoTo 0
	End Function
	
	'%LoadTabs: Arma la secuencia Procesadmiento de Interfaces
	Public Function LoadTabsFieldSheet(ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nSheet As Integer) As Object
		Const CN_FIELDSHEET As String = "GI1403  GI1404  GI1406  "
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
		
		Dim ldblnotenum As Double
		Dim ldblImageNum As Double
		
		Dim lobjField As Object
		Dim lclsMasterSheet As MasterSheet
		Dim lblnShow As Boolean
		
		On Error GoTo LoadTabsFieldSheet_Err
		
		
		lclsSecurSche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		lclsMasterSheet = New MasterSheet
		
		lstrHTMLCode = String.Empty
		lstrWindows = CN_FIELDSHEET
		
		lblnRequired = True
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		lclsMasterSheet.Find(nSheet)
		Do While Trim(lstrCodispl) <> String.Empty
			lblnContent = False
			lblnRequired = False
			lstrCodispl = Trim(lstrCodispl)
			lblnShow = True
			
			If lstrCodispl = "GI1403" Then
				lblnShow = lclsMasterSheet.sNogrid <> "1"
			End If
			
			If lblnShow Then
				'+ Se asignan los valores a las variables de descripción
				If lrecWindows.OpenQuery("Windows", "sCodisp, sShort_des", "sCodispl='" & lstrCodispl & "'") Then
					lstrCodisp = lrecWindows.FieldToClass("sCodisp")
					lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
					lrecWindows.CloseQuery()
				End If
				
				'+ Se busca la imagen a colocar en los links
				With lclsSecurSche
					If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
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
							
							'+ GI1403: Datos de entrada/salida del proceso
							Case "GI1403"
								lblnContent = True
								
								'+ GI1404: Homologacion
							Case "GI1404"
								lblnContent = True
								
								'+ GI1406: Errores
							Case "GI1406"
								lblnContent = True
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
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			End If
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		Loop 
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabsFieldSheet = lstrHTMLCode
		
LoadTabsFieldSheet_Err: 
		If Err.Number Then
			LoadTabsFieldSheet = "LoadTabsFieldSheet: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lobjField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjField = Nothing
		'UPGRADE_NOTE: Object lclsMasterSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMasterSheet = Nothing
		
	End Function
	
	'%InsCalFieldSheetId: Se encarga de Rescatar correlativo disponible de la tabla FieldSheet
	Public Function InsCalFieldSheetId(ByVal nSheet As Integer) As Integer
		Dim lrecInsCalFieldSheetId As eRemoteDB.Execute
		
		On Error GoTo InsCalFieldSheetId_Err
		
		lrecInsCalFieldSheetId = New eRemoteDB.Execute
		
		With lrecInsCalFieldSheetId
			.StoredProcedure = "InsCalFieldSheetId"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			InsCalFieldSheetId = .Parameters("nId").Value
			
		End With
		
InsCalFieldSheetId_Err: 
		If Err.Number Then
			InsCalFieldSheetId = 0
		End If
		'UPGRADE_NOTE: Object lrecInsCalFieldSheetId may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalFieldSheetId = Nothing
		On Error GoTo 0
	End Function
End Class






