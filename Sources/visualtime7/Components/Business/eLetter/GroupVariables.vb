Option Strict Off
Option Explicit On
Public Class GroupVariables
	'**+Objetive: Clase generada a partir de la tabla 'GROUPVARIABLES' que contiene Grupos de variables de correspondencia.
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'GROUPVARIABLES' Groups of correspondence variables.
	'+Version: $$Revision: 9 $
	
	'**-Objective: Code of the variable group(Correspondence)
	'-Objetivo: Código del grupo de variables. (Correspondencia)
	Public nLett_group As Short
	
	'**-Objective: Name of The Variable used in Correspondence.
	'-Objetivo: Nombre de la variable utilizada en correspondencia.
	Public sVariable As String
	
	'**-Objective: Description of the variable.
	'-Objetivo: Descripción de la variable.
	Public sDescript As String
	
	'**-Objective: Table name from where the variable value is obtained. Possible values as per system table (sysobjects)
	'-Objetivo: Nombre de la tabla de la base de datos de donde se obtiene el valor de la variable.Valores posibles según archivo de tablas de la base de datos.
	Public sTableName As String
	
	'**-Objective: Field name that contains the value handled by the variablePossible values as per system column table (syscolumns)
	'-Objetivo: Nombre del campo que contiene el valor o información que maneja la variable.Valores posibles según archivo de campos de la base de datos.
	Public sColumName As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Short
	
	'**-Objective: Type of variable  - Correspondence.Sole values.0 - Database 1 - System
	'-Objetivo: Tipo de variable de correspondencia.Valores únicos.0 - Base de datos 1 - Sistema
	Public nTypVariable As Short
	
	'**-Objective: Table associated to obtain the descriptive values of the field
	'-Objetivo: Tabla asociada para obtener los valores descriptivos del campo.
	Public sTabValue As String
	
	'**-Objective: Alias to be used to get the variable value  in correspondence request processing
	'-Objetivo: Alias a usar  para recuperar los valores de las variables en el proceso de solicitud de correspondencia
	Public sAliasTable As String
	
	'**-Objective: Alias to be used to return the variable values
	'-Objetivo: Alias con el que se devolverán los valores de las variables
	Public sAliasColumn As String
	
	'**-Objective: Type of variable - Correspondence Sole values. 0 - Database 1 - System
	'-Objetivo: Tipo de variable - Correspondencia. Unicos valores. 0 - Base de Datos 1 - Sistema
	Public bTypVariable As Boolean
	
	'**-Objective: State of the instance. Possible values 1, 2 and 3
	'-Objetivo: Estado de la instancia. Posibles valores 1, 2 y 3
	Public nStatusInstance As Short
	
	Public sGroupDescript As String
	
	'**-Objective: Temporary variable. Name of the variable used in correspondence
	'-Objetivo: Variable temporal. Nombre de la variable utilizada en correspondencia
	Private mstrVariable As String
	
	'**-Objective: Code of the variable group (Correspondence)
	'-Objetivo: Código del grupos de variables (Correspondencia)
	Private mintLett_group As Short
	
	'**-Objective: Property of internal use for the process of 'MergeDocument'
	'-Objetivo: Propiedad de uso interno para el proceso de 'MergeDocument'
	Public sFldSource As String
	
	'**-Objective: Property of internal use for the process of 'MergeDocument'
	'-Objetivo: Propiedad de uso interno para el proceso de 'MergeDocument'
	Public sFldValue As String
	
	
	'**%Objective: It comes back true or false depending on the existence or not on the information
	'**%Parameters:
	'**%  nLett_group - Code of the variable group(Correspondence)
	'**%  sVariable   - Name of The Variable used in Correspondence.
	'**%  lblnAll     - Variable of boolean condition.
	'%Objetivo: Devuelve verdadero o falso dependiendo de la existencia o no de los datos
	'%Parámetros:
	'%  nLett_group   - Código del grupo de variables. (Correspondencia)
	'%  sVariable     - Nombre de la variable utilizada en correspondencia.
	'%  lblnAll       - Variable de condición booleana.
	Public Function Find(ByVal nLett_group As Short, ByVal sVariable As String, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecGroupVariables As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecGroupVariables = New eRemoteDB.Execute
		
		Find = False
		With lrecGroupVariables
			.StoredProcedure = "reaGroupVariables"
			.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVariable", sVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nLett_group = .FieldToClass("nLett_group")
				sVariable = .FieldToClass("sVariable")
				sDescript = .FieldToClass("sDescript")
				sTableName = .FieldToClass("sTableName")
				sColumName = .FieldToClass("sColumName")
				nUsercode = .FieldToClass("nUsercode")
				nTypVariable = .FieldToClass("nTypVariable")
				bTypVariable = IIf(nTypVariable = 1, True, False)
				sTabValue = .FieldToClass("sTabValue")
				sAliasTable = .FieldToClass("sAliasTable")
				sAliasColumn = .FieldToClass("sAliasColumn")
				
				Find = True
				.RCloseRec()
			End If
		End With
		
		lrecGroupVariables = Nothing
		
		Exit Function
		lrecGroupVariables = Nothing
	End Function
	
	'**%Objective: Obtain the number of the group of parameters to which a variable belongs
	'**%Parameters:
	'**%  sVariable - Name of The Variable used in Correspondence.
	'%Objetivo: Obtener el numero del grupo de parametros a la cual pertence una variable
	'%Parámetros:
	'%  sVariable   - Nombre de la variable utilizada en correspondencia.
	Public Function FindGroupParams(ByVal sVariable As String) As GroupParams
		Dim lrecreaGroupParamsbyVar As eRemoteDB.Execute
		Dim lstrVariable As String
		
		If Not IsIDEMode Then
        End If

        FindGroupParams = Nothing
		
		lrecreaGroupParamsbyVar = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.reaGroupParamsbyVar'
		'Información leída el 09/07/2001 13:01:44
		
		lstrVariable = Trim(CleanLetter(sVariable))
		
		With lrecreaGroupParamsbyVar
			.StoredProcedure = "reaGroupParamsbyVar"
			.Parameters.Add("sVariable", lstrVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindGroupParams = New GroupParams
				FindGroupParams.nLett_group = .FieldToClass("nLett_group")
				FindGroupParams.sDescript = .FieldToClass("sDescript")
				FindGroupParams.nUsercode = .FieldToClass("nUsercode")
				FindGroupParams.sParameters = .FieldToClass("sParameters")
				FindGroupParams.sTableName = .FieldToClass("sTableName")
				.RCloseRec()
			End If
		End With
		
		lrecreaGroupParamsbyVar = Nothing
		
		Exit Function
		lrecreaGroupParamsbyVar = Nothing
		FindGroupParams = Nothing
	End Function
	
	'**%Objective: Adds a new registry to the GroupVariables table
	'%Objetivo: Adiciona un nuevo registro a la tabla GroupVariables
	Public Function Add() As Boolean
		
		If Not IsIDEMode Then
		End If
		
		Add = insUpdGroupVariables(1)
		
		Exit Function
	End Function
	
	'**%Objective: Update a registry to the GroupVariables table
	'%Objetivo: Actualiza un registro a la tabla GroupVariables
	Public Function Update() As Boolean
		
		If Not IsIDEMode Then
		End If
		
		Update = insUpdGroupVariables(2)
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry to the GroupVariables table
	'%Objetivo: Elimina un registro a la tabla GroupVariables
	Public Function Delete() As Boolean
		
		If Not IsIDEMode Then
		End If
		
		Delete = insUpdGroupVariables(3)
		
		Exit Function
	End Function
	
	'**%Objective: This method entrusts carrying out the search of the corresponding information for name
	'**%Parameters:
	'**%  sVariable  - Name of The Variable used in Correspondence.
	'**%  lblnAll    - Variable de condición booleana.
	'%Objetivo: Este metodo se encarga de realizar la busqueda de los datos correspondientes por nombre
	'%Parámetros:
	'%  sVariable   - Nombre de la variable utilizada en correspondencia.
	'%  lblnAll     - Variable de condición booleana.
    Public Function FindByName(ByVal sVariable As String, Optional ByVal nLett_group As Short = -32768, Optional ByVal lblnAll As Boolean = False) As Boolean
        Dim lrecReaGroupVariables As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If

        lrecReaGroupVariables = New eRemoteDB.Execute

        If mstrVariable <> sVariable Or lblnAll Then

            With lrecReaGroupVariables
                .StoredProcedure = "reaGroupVariables"
                .Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sVariable", Trim(sVariable), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    FindByName = True
                    Me.nLett_group = .FieldToClass("nLett_group")
                    Me.sVariable = .FieldToClass("sVariable")
                    sDescript = .FieldToClass("sDescript")
                    sTableName = .FieldToClass("sTableName")
                    sColumName = .FieldToClass("sColumName")
                    nUsercode = .FieldToClass("nUsercode")
                    nTypVariable = .FieldToClass("nTypVariable")
                    bTypVariable = IIf(Me.nTypVariable = 1, True, False)
                    mstrVariable = Me.sVariable
                    mintLett_group = Me.nLett_group
                    sTabValue = .FieldToClass("sTabValue")
                    sAliasTable = .FieldToClass("sAliasTable")
                    sAliasColumn = .FieldToClass("sAliasColumn")
                    .RCloseRec()
                Else
                    FindByName = False
                End If
            End With
        Else
            FindByName = True
        End If

        Exit Function
    End Function
	
	'**%Objective: Executes the action that is indicated to him, depending on the sent variable
	'**%Parameters:
	'    lintAction  - Action to execute. Only values 1. - Add, 2. - Update and  3. - Eliminate
	'%Objetivo: Ejecuta la acción que se le indica, dependiendo de la variable enviada
	'%Parámetros:
	'%   lintAction   - Acción a ejecutar. Unicos valores 1.- Adicionar, 2.- Actualizar y 3.- Eliminar
	Private Function insUpdGroupVariables(ByVal lintAction As Short) As Boolean
		Dim lrecGroupVariables As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecGroupVariables = New eRemoteDB.Execute
		With lrecGroupVariables
			.StoredProcedure = "insUpdGroupVariables"
            .Parameters.Add("nAction", lintAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVariable", sVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTablename", sTableName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCOLUMNAME", sColumName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_NTYPVARIABLE", nTypVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAliasTable", sAliasTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_SALIASCOLUMN", sAliasColumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdGroupVariables = .Run(False)
		End With
		lrecGroupVariables = Nothing
		
		Exit Function
		lrecGroupVariables = Nothing
	End Function
	
	'**%Objective: Validate the content of the page to valMantLetter.
	'**%Parameters:
	'**%    sCodispl      - Logical code of the page in execution
	'**%    sAction       - Description of the action to make
	'**%    sVariable     - Name of The Variable used in Correspondence.
	'**%    sDescript     - Description of the variable.
	'**%    sTableName    - Table name from where the variable value is obtained. Possible values as per system table (sysobjects)
	'**%    sColumName    - Field name that contains the value handled by the variablePossible values as per system column table (syscolumns)
	'**%    nSystemValue  - Code of the value of the system.
	'%Objetivo: Validar el contenido de la página valMantLetter.
	'%Parámetros:
	'%      sCodispl      - Código lógico de la página en ejecución
	'%      sAction       - Descripción de la acción a realizar
	'%      sVariable     - Nombre de la variable utilizada en correspondencia.
	'%      sDescript     - Descripción de la variable.
	'%      sTableName    - Nombre de la tabla de la base de datos de donde se obtiene el valor de la variable.Valores posibles según archivo de tablas de la base de datos.
	'%      sColumName    - Nombre del campo que contiene el valor o información que maneja la variable.Valores posibles según archivo de campos de la base de datos.
	'%      nSystemValue  - Código del valor del sistema
	Public Function insValMLT001(ByVal sCodispl As String, ByVal sAction As String, ByVal sVariable As String, ByVal sDescript As String, ByVal sTableName As String, ByVal sColumName As String, ByVal nSystemValue As Short, Optional ByVal nLett_group As Short = -32768) As String
		Dim lobjErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		
		insValMLT001 = String.Empty
		
		lobjErrors = New eFunctions.Errors
		
		If sVariable = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 8396)
		Else
			If sDescript = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 8074)
			End If
			If UCase(sAction) = "ADD" Then
				If FindByName(sVariable, nLett_group) Then
					Call lobjErrors.ErrorMessage(sCodispl, 10256)
				End If
			End If
		End If
		If nSystemValue <> 1 Then
			If sTableName = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 8301)
			End If
			If sColumName = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 8302)
			End If
		End If
		insValMLT001 = lobjErrors.Confirm
		lobjErrors = Nothing
		
		Exit Function
		lobjErrors = Nothing
	End Function
	
	'**%Objective: Executes the action to keep, to update or to eliminate if the validation process were correct.
	'**%Parameters:
	'**% sAction       - Description of the action to execute
	'**% nLett_group   - Code of the variable group(Correspondence)
	'**% sLettDescript - Description of the variable.
	'**% sParameters   - It indicates whether the parameters is required by the group Possible values: 0 No required 1 Required
	'**% sVariable     - Name of The Variable used in Correspondence.
	'**% sDescript     - Description of the variable.
	'**% sTableName    - Table name from where the variable value is obtained. Possible values as per system table (sysobjects)
	'**% sColumnName   - Field name that contains the value handled by the variable Possible values as per system column table
	'**% nSystemTable  - Type of variable - Correspondence Sole values. 0 - Database 1 - System
	'**% nUsercode     - Code of the user creating or updating the record.
	'**% sAliasTable   - Alias to be used to get the variable value  in correspondence request processing
	'**% sAliasColumn  - Alias to be used to return the variable values
	'%Objetivo: Ejecuta la acción de guardar, actualizar o eliminar si el proceso de validación estuvo correcto.
	'%Parámetros:
	'%   sAction       - Descripción de la acción a ejecutarse
	'%   nLett_group   - Código del grupo de variables. (Correspondencia)
	'%   sLettDescript - Descripción de la variable.
	'%   sParameters   - Indica si los parámetros son requeridos los valores posibles: 0 No requeridos, 1 Requeridos.
	'%   sVariable     - Nombre de la variable utilizada en correspondencia.
	'%   sDescript     - Description of the variable.
	'%   sTableName    - Nombre de la tabla de la base de datos de donde se obtiene el valor de la variable.Valores posibles según archivo de tablas de la base de datos.
	'%   sColumnName   - Nombre del campo que contiene el valor de la variable, Possible según el vector de la columna del sistema
	'%   nSystemTable  - Tipo de variable de correspondecia. Únicos valores: 0.- Base de datos y 1.- Sistema.
	'%   nUsercode     - Código del usuario que crea o actualiza el registro.
	'%   sAliasTable   - Alias a usar para recuperar los valores de las variables en el proceso de solicitud de correspondencia
	'%   sAliasColumn  - Alias con el que se devolverán los valores de las variables
	Public Function insPostMLT001(ByVal sAction As String, ByVal nLett_group As Short, ByVal sLettDescript As String, ByVal sParameters As String, ByVal sVariable As String, ByVal sDescript As String, ByVal sTableName As String, ByVal sColumnName As String, ByVal nSystemTable As Short, ByVal nUsercode As Short, ByVal sAliasTable As String, ByVal sAliasColumn As String) As Boolean
		Dim lobjGroupParam As GroupParams
		
		If Not IsIDEMode Then
		End If
		
		If sAction <> String.Empty Then
			With Me
				.nLett_group = nLett_group
				.sVariable = sVariable
				.sDescript = sDescript
				.sTableName = sTableName
				.sColumName = sColumnName
				.nUsercode = nUsercode
				.nTypVariable = nSystemTable
				If .nTypVariable = 1 Then
					.sTableName = strNull
					.sColumName = strNull
				End If
				.sAliasTable = sAliasTable
				.sAliasColumn = sAliasColumn
				.nStatusInstance = IIf(UCase(sAction) = "ADD", 1, 2)
			End With
			If UCase(sAction) <> "DEL" Then
				lobjGroupParam = New GroupParams
				With lobjGroupParam
					.nLett_group = nLett_group
					.sDescript = sLettDescript
					.sParameters = sParameters
					.nUsercode = nUsercode
					.nStatusInstance = IIf(UCase(sAction) = "ADD", 1, 2)
					If .Update() Then
						insPostMLT001 = Update()
					Else
						insPostMLT001 = False
					End If
				End With
				lobjGroupParam = Nothing
			Else
				Me.nStatusInstance = 3
				insPostMLT001 = Delete()
			End If
		Else
			lobjGroupParam = New GroupParams
			With lobjGroupParam
				.nLett_group = nLett_group
				.sDescript = sLettDescript
				.sParameters = sParameters
				.nUsercode = nUsercode
				.nStatusInstance = 2
				
				insPostMLT001 = .Update()
			End With
			lobjGroupParam = Nothing
		End If
		
		Exit Function
		lobjGroupParam = Nothing
	End Function
	
	'**%Objective: Initialize the class
	'%Objetivo: Inicializar la clase.
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mstrVariable = String.Empty
		mintLett_group = intNull
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class











