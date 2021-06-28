Option Strict Off
Option Explicit On
Public Class GroupParams
	'**+Objetive: Clase generada a partir de la tabla 'GROUPPARAMS' que contiene los Parámetros del grupo
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'GROUPPARAMS' Parameters of the group.
	'+Version: $$Revision: 9 $
	
	'**-Objective: Code of the variable group (Correspondence).
	'-Objetivo: Código del grupo de variables (Correspondencia).
	Public nLett_group As Short
	
	'**-Objective:Description of the parameter group
	'-Objetivo:Descripción del grupo de parámetros
	Public sDescript As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Short
	
	'**-Objective:It indicates whether the parameters is required by the
	'**-group Possible values:  0 No required  1 Required
	'-Objetivo: Vector indicador de parámetros requeridos por un grupo.
	'-Valores posibles: 0  No es requerido 1  Es requerido
	Public sParameters As String
	
	'**-Objective:
	'-Objetivo:
	Public nStatusInstance As Short
	
	'**-Objective:Table name from where the variable value is obtained
	'-Objetivo:Nombre de la tabla de la base de datos de donde se obtiene
	'-el valor de la variable
	Public sTableName As String
	
	'**%Objective: Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'**%           tabla "GroupParams". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	'**%Parameters:
	'**%    nLett_Group  - Code of the variable group (Correspondence).
	'%Objetivo: Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%          tabla "GroupParams". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	'%Parámetros:
	'%    nLett_Group - Código del grupo de variables (Correspondencia).
	Public Function Find(ByVal nLett_group As Short) As Boolean
		Dim lrecGroupParams As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecGroupParams = New eRemoteDB.Execute
		
		Find = False
		With lrecGroupParams
			.StoredProcedure = "reaGroupParams"
			.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nLett_group = .FieldToClass("nLett_group")
				sDescript = .FieldToClass("sDescript")
				nUsercode = .FieldToClass("nUsercode")
				sParameters = .FieldToClass("sParameters")
				Find = True
				.RCloseRec()
			End If
		End With
		lrecGroupParams = Nothing
		
		Exit Function
		lrecGroupParams = Nothing
	End Function
	
	'**Objective: Este metodo se encarga de realizar la busqueda de determinado parametro en la propiedad sParameters
	'%Objetivo: Este metodo se encarga de realizar la busqueda de determinado parametro en la propiedad sParameters
	Public Function ExistsParam(ByVal nParam As Short) As Boolean
		
		If Not IsIDEMode Then
		End If
		
		ExistsParam = False
		
		If sParameters <> String.Empty Then
			If Mid(sParameters, nParam + 1, 1) = "1" Then
				ExistsParam = True
			End If
		End If
		
		Exit Function
	End Function
	
	'**%Objective:
	'%Objetivo:
	Private Function Add() As Boolean
		If Not IsIDEMode Then
		End If
		
		Add = insUpdGroupParams(1)
		
		Exit Function
	End Function
	
	'**%Objective: This function is in charge of updating the data in the main table of the class.
	'%Objetivo: Esta función se encarga de actualizar información en la tabla principal de la clase.
	Public Function Update() As Boolean
		If Not IsIDEMode Then
		End If
		
		Update = insUpdGroupParams(2)
		
		Exit Function
	End Function
	
	'**Objective: Delete the information in the main table of the class.
	'%Objetivo: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Private Function Delete() As Boolean
		If Not IsIDEMode Then
		End If
		
		Delete = insUpdGroupParams(3)
		
		Exit Function
	End Function
	
	'**%Objective: Validate the data entered on the header zone for the page.
	'**%Parameters:
	'**%   sCodispl - Code of the window (logical code).
	'**%   nAction  -
	'**%   nGroup   -
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de cabecera para
	'%          la pagina
	'%Parámetros:
	'%   sCodispl  - Codigo de la ventana (Codigo logico).
	'%   sAction   -
	'%   nGroup    -
	Public Function insValMLT001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nGroup As Short) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValid As eFunctions.Values
		
		If Not IsIDEMode Then
		End If
		
		insValMLT001_K = String.Empty
		
		lobjErrors = New eFunctions.Errors
		If nGroup = 0 Or nGroup = intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 8250)
		Else
			lobjValid = New eFunctions.Values
			If lobjValid.IsValid("tabGroupParams", CStr(nGroup)) Then
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					Call lobjErrors.ErrorMessage(sCodispl, 8339)
				End If
			Else
				If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
					Call lobjErrors.ErrorMessage(sCodispl, 3136)
				End If
			End If
			lobjValid = Nothing
		End If
		
		insValMLT001_K = lobjErrors.Confirm
		
		lobjErrors = Nothing
		
		Exit Function
		lobjErrors = Nothing
	End Function
	
	''**%Objective: validate the data entered on the detail zone for the form.
	'**%Parameters:
	'**%   sCodispl      - Code of the window (logical code).
	'**%   sLettDescript -
	'**%   sAction       -
	'**%   sVariable     -
	'**%   sDescript     - Description of the variables groups..
	'**%   sTableName    -
	'**%   sColumName    -
	'**%   nSystemValue  -
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%          forma.
	'%Parámetros:
	'%   sCodispl       - Codigo de la ventana (Codigo logico).
	'%   sLettDescript  -
	'%   sAction        -
	'%   sVariable      -
	'%   sDescript      - Descripción del grupo de variables.
	'%   sTableName     -
	'%   sColumName     -
	'%   nSystemValue   -
	
	Public Function insValMLT001(ByVal sCodispl As String, ByVal sLettDescript As String, ByVal sAction As String, ByVal sVariable As String, ByVal sDescript As String, ByVal sTableName As String, ByVal sColumName As String, ByVal nSystemValue As Short, Optional ByVal nLett_group As Short = -32768) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjGroupVariables As GroupVariables
		Dim lobjValues As eFunctions.Values
		
		If Not IsIDEMode Then
		End If
		
		insValMLT001 = String.Empty
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		
		If sLettDescript = String.Empty Then
            lobjErrors.ErrorMessage("MLT001", 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(811) & ";")
        End If
		
		If sAction <> String.Empty Then
			lobjGroupVariables = New GroupVariables
			insValMLT001 = lobjErrors.Confirm & lobjGroupVariables.insValMLT001(sCodispl, sAction, sVariable, sDescript, sTableName, sColumName, nSystemValue, nLett_group)
			lobjGroupVariables = Nothing
			lobjErrors = Nothing
		Else
			insValMLT001 = lobjErrors.Confirm
		End If
		
		lobjValues = Nothing
		
		Exit Function
		lobjGroupVariables = Nothing
		lobjErrors = Nothing
	End Function
	
	'**%Objective: Update a record to the table "GroupParams"
	'**%Parameters:
	'**%    sAction - Actions of the transaction
	'%Objetivo: Actualiza un registro a la tabla "GroupParams"
	'%Parámetros:
	'%    sAction - Acción de la transacción
	Private Function insUpdGroupParams(ByVal lintAction As Short) As Boolean
		Dim lrecGroupVariables As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecGroupVariables = New eRemoteDB.Execute
		With lrecGroupVariables
			.StoredProcedure = "insUpdGroupParams"
			.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sParameters", sParameters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 7, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdGroupParams = .Run(False)
		End With
		lrecGroupVariables = Nothing
		
		Exit Function
		lrecGroupVariables = Nothing
	End Function
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		sDescript = String.Empty
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function insPostMLT001(ByVal nAction As Integer, ByVal nGroup As Short) As Boolean
		Dim lrecGroupParams As New eRemoteDB.Execute
		Dim lblnTemp As Boolean
		
		If Not IsIDEMode Then
		End If
		
		lrecGroupParams = New eRemoteDB.Execute
		insPostMLT001 = False
		If nAction = 303 Then
			With lrecGroupParams
				lblnTemp = False
'PENDING: Procedure not found
				.StoredProcedure = "ReaLettValues2"
				.Parameters.Add("nLett_group", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nLett_group = .FieldToClass("nLett_group")
					lblnTemp = True
					insPostMLT001 = False
					.RCloseRec()
				End If
			End With
			If Not lblnTemp Then
				With lrecGroupParams
'PENDING: Procedure not found
					.StoredProcedure = "DelGroupParam"
					.Parameters.Add("nLett_group", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					insPostMLT001 = .Run(False)
				End With
			End If
		End If
		lrecGroupParams = Nothing
		
		Exit Function
		lrecGroupParams = Nothing
	End Function
End Class











